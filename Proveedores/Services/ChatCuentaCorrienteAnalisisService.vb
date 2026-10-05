Imports System.Data
Imports DSM = DataSourceManager.Lib.DataSourceManager

Partial Public Class ChatCuentaCorrienteService

    Public Function ObtenerAnalisisDeudaTotal(Optional limiteDetalle As Integer = 50) As ChatAnalisisDeudaTotal
        limiteDetalle = LimitarCantidad(limiteDetalle, 1, 200)
        Dim proveedores = CargarProveedoresAnalisis()
        Dim saldos = CalcularSaldos(CargarMovimientosAnalisis(Nothing, Date.Today.AddDays(1)))
        Dim detalle = proveedores.Select(Function(p) New ChatDeudaProveedor With {
            .NroCuenta = p.NroCuenta, .Nombre = p.Nombre, .Cuit = p.Cuit,
            .Saldo = If(saldos.ContainsKey(p.NroCuenta), saldos(p.NroCuenta), 0D)}).ToList()

        Dim resultado As New ChatAnalisisDeudaTotal With {
            .DeudaBruta = detalle.Where(Function(p) p.Saldo > 0D).Sum(Function(p) p.Saldo),
            .CreditosAFavor = Math.Abs(detalle.Where(Function(p) p.Saldo < 0D).Sum(Function(p) p.Saldo)),
            .PosicionNeta = detalle.Sum(Function(p) p.Saldo),
            .CantidadConDeuda = detalle.Where(Function(p) p.Saldo > 0D).Count(),
            .CantidadConCredito = detalle.Where(Function(p) p.Saldo < 0D).Count()}

        For Each proveedor In detalle.Where(Function(p) p.Saldo > 0D).OrderByDescending(Function(p) p.Saldo).Take(limiteDetalle)
            proveedor.PorcentajeDeuda = If(resultado.DeudaBruta = 0D, 0D,
                                           Math.Round(proveedor.Saldo * 100D / resultado.DeudaBruta, 2))
            resultado.Proveedores.Add(proveedor)
        Next
        Return resultado
    End Function

    Public Function ObtenerPromedioComprasMensuales(desde As DateTime,
                                                     hasta As DateTime) As ChatAnalisisComprasMensuales
        ValidarPeriodo(desde, hasta)
        desde = desde.Date
        hasta = hasta.Date
        Dim resultado As New ChatAnalisisComprasMensuales With {.Desde = desde, .Hasta = hasta}
        Dim porMes As New Dictionary(Of DateTime, Decimal)
        Dim mes = New Date(desde.Year, desde.Month, 1)
        Dim ultimoMes = New Date(hasta.Year, hasta.Month, 1)
        While mes <= ultimoMes
            porMes(mes) = 0D
            mes = mes.AddMonths(1)
        End While

        Dim movimientos = CargarMovimientosAnalisis(desde, hasta.AddDays(1))
        Dim autocancelables = CargarAutocancelables(movimientos)
        If movimientos IsNot Nothing Then
            For Each fila As DataRow In movimientos.Rows
                Dim impacto = CalcularImpacto(fila, autocancelables)
                If impacto > 0D Then
                    Dim fecha = Convert.ToDateTime(fila("Fecha"))
                    porMes(New Date(fecha.Year, fecha.Month, 1)) += impacto
                End If
            Next
        End If

        resultado.Meses = porMes.OrderBy(Function(x) x.Key).
            Select(Function(x) New ChatComprasMes With {.Mes = x.Key, .TotalCompras = x.Value}).ToList()
        resultado.TotalPeriodo = resultado.Meses.Sum(Function(x) x.TotalCompras)
        resultado.PromedioMensual = If(resultado.Meses.Count = 0, 0D, resultado.TotalPeriodo / resultado.Meses.Count)
        Return resultado
    End Function

    Public Function ListarProveedoresMasPagados(desde As DateTime, hasta As DateTime,
                                                Optional limite As Integer = 10) As List(Of ChatPagoProveedor)
        ValidarPeriodo(desde, hasta)
        limite = LimitarCantidad(limite, 1, 100)
        Dim proveedores = CargarProveedoresAnalisis().ToDictionary(Function(p) p.NroCuenta)
        Dim movimientos = CargarMovimientosAnalisis(desde.Date, hasta.Date.AddDays(1))
        Dim autocancelables = CargarAutocancelables(movimientos)
        Dim pagos As New Dictionary(Of Integer, Decimal)
        If movimientos IsNot Nothing Then
            For Each fila As DataRow In movimientos.Rows
                Dim impacto = CalcularImpacto(fila, autocancelables)
                If impacto < 0D Then
                    Dim cuenta = Convert.ToInt32(fila("NroCuenta"))
                    pagos(cuenta) = If(pagos.ContainsKey(cuenta), pagos(cuenta), 0D) + Math.Abs(impacto)
                End If
            Next
        End If
        Dim totalPagado = pagos.Values.Sum()
        Return pagos.Select(Function(x)
            Dim proveedor As ChatProveedorInfo = Nothing
            proveedores.TryGetValue(x.Key, proveedor)
            Return CrearPagoProveedor(x.Key, x.Value, totalPagado, proveedor)
        End Function).OrderByDescending(Function(x) x.TotalPagado).Take(limite).ToList()
    End Function

    Public Function ObtenerEvolucionMensual(desde As DateTime, hasta As DateTime) As List(Of ChatEvolucionDeudaMes)
        ValidarPeriodo(desde, hasta)
        Dim primerMes = New Date(desde.Year, desde.Month, 1)
        Dim ultimoMes = New Date(hasta.Year, hasta.Month, 1)
        Dim movimientos = CargarMovimientosAnalisis(Nothing, hasta.Date.AddDays(1))
        Dim autocancelables = CargarAutocancelables(movimientos)
        Dim saldos As New Dictionary(Of Integer, Decimal)
        Dim actividad As New Dictionary(Of DateTime, List(Of Tuple(Of Integer, Decimal)))
        If movimientos IsNot Nothing Then
            For Each fila As DataRow In movimientos.Rows
                AcumularMovimientoMensual(fila, desde.Date, hasta.Date, autocancelables, saldos, actividad)
            Next
        End If
        Dim resultado As New List(Of ChatEvolucionDeudaMes)
        Dim mes = primerMes
        While mes <= ultimoMes
            Dim compras As Decimal = 0D
            Dim pagos As Decimal = 0D
            If actividad.ContainsKey(mes) Then
                For Each movimiento In actividad(mes)
                    saldos(movimiento.Item1) = If(saldos.ContainsKey(movimiento.Item1), saldos(movimiento.Item1), 0D) + movimiento.Item2
                    If movimiento.Item2 > 0D Then compras += movimiento.Item2
                    If movimiento.Item2 < 0D Then pagos += Math.Abs(movimiento.Item2)
                Next
            End If
            resultado.Add(CrearEvolucionMes(mes, compras, pagos, saldos.Values))
            mes = mes.AddMonths(1)
        End While
        Return resultado
    End Function

    Public Function ListarDiferenciasSaldo(Optional tolerancia As Decimal = 1D,
                                           Optional limite As Integer = 100) As List(Of ChatDiferenciaSaldoProveedor)
        tolerancia = Math.Abs(tolerancia)
        limite = LimitarCantidad(limite, 1, 500)
        Dim saldos = CalcularSaldos(CargarMovimientosAnalisis(Nothing, Date.Today.AddDays(1)))
        Return CargarProveedoresAnalisis().Select(Function(p) CrearDiferencia(p, saldos)).
            Where(Function(x) Math.Abs(x.Diferencia) >= tolerancia).
            OrderByDescending(Function(x) Math.Abs(x.Diferencia)).Take(limite).ToList()
    End Function

    Private Shared Function CrearPagoProveedor(cuenta As Integer, monto As Decimal, total As Decimal,
                                                proveedor As ChatProveedorInfo) As ChatPagoProveedor
        Dim item As New ChatPagoProveedor
        item.NroCuenta = cuenta
        item.Nombre = If(proveedor Is Nothing, cuenta.ToString(), proveedor.Nombre)
        item.Cuit = If(proveedor Is Nothing, String.Empty, proveedor.Cuit)
        item.TotalPagado = monto
        item.PorcentajeDelTotal = If(total = 0D, 0D, Math.Round(monto * 100D / total, 2))
        Return item
    End Function

    Private Shared Function CrearDiferencia(p As ChatProveedorInfo,
                                             saldos As Dictionary(Of Integer, Decimal)) As ChatDiferenciaSaldoProveedor
        Dim calculado = If(saldos.ContainsKey(p.NroCuenta), saldos(p.NroCuenta), 0D)
        Dim item As New ChatDiferenciaSaldoProveedor
        item.NroCuenta = p.NroCuenta
        item.Nombre = p.Nombre
        item.SaldoCalculado = calculado
        item.SaldoActualMaestro = p.SaldoActualMaestro
        item.Diferencia = calculado - p.SaldoActualMaestro
        Return item
    End Function

    Private Shared Sub AcumularMovimientoMensual(fila As DataRow,
                                                  desde As DateTime, hasta As DateTime,
                                                  autocancelables As Dictionary(Of String, Integer),
                                                  saldos As Dictionary(Of Integer, Decimal),
                                                  actividad As Dictionary(Of DateTime, List(Of Tuple(Of Integer, Decimal))))
        Dim fecha = Convert.ToDateTime(fila("Fecha")).Date
        Dim cuenta = Convert.ToInt32(fila("NroCuenta"))
        Dim impacto = CalcularImpacto(fila, autocancelables)
        If fecha < desde Then
            saldos(cuenta) = If(saldos.ContainsKey(cuenta), saldos(cuenta), 0D) + impacto
        ElseIf fecha >= desde AndAlso fecha <= hasta Then
            Dim mes = New Date(fecha.Year, fecha.Month, 1)
            If Not actividad.ContainsKey(mes) Then actividad(mes) = New List(Of Tuple(Of Integer, Decimal))
            actividad(mes).Add(Tuple.Create(cuenta, impacto))
        End If
    End Sub

    Private Shared Function CrearEvolucionMes(mes As DateTime, compras As Decimal, pagos As Decimal,
                                               saldos As IEnumerable(Of Decimal)) As ChatEvolucionDeudaMes
        Dim item As New ChatEvolucionDeudaMes
        item.Mes = mes
        item.Compras = compras
        item.Pagos = pagos
        item.VariacionNeta = compras - pagos
        item.DeudaBrutaAlCierre = saldos.Where(Function(x) x > 0D).Sum()
        item.CreditosAFavorAlCierre = Math.Abs(saldos.Where(Function(x) x < 0D).Sum())
        item.PosicionNetaAlCierre = saldos.Sum()
        Return item
    End Function

    Private Shared Sub ValidarPeriodo(desde As DateTime, hasta As DateTime)
        If desde.Date > hasta.Date Then Throw New ArgumentException(NameOf(desde))
    End Sub

    Private Shared Function CalcularImpacto(fila As DataRow,
                                             autocancelables As Dictionary(Of String, Integer)) As Decimal
        Dim impacto As Decimal = 0D
        AplicarImporteAlSaldo(impacto, Convert.ToInt32(fila("IdImputacion")),
                              Texto(fila("CtaMonto")), DecimalSeguro(fila("Monto")), autocancelables)
        Return impacto
    End Function

    Private Shared Function CalcularSaldos(movimientos As DataTable) As Dictionary(Of Integer, Decimal)
        Dim saldos As New Dictionary(Of Integer, Decimal)
        Dim autocancelables = CargarAutocancelables(movimientos)
        If movimientos Is Nothing Then Return saldos
        For Each fila As DataRow In movimientos.Rows
            Dim cuenta = Convert.ToInt32(fila("NroCuenta"))
            Dim saldo = If(saldos.ContainsKey(cuenta), saldos(cuenta), 0D)
            saldos(cuenta) = saldo + CalcularImpacto(fila, autocancelables)
        Next
        Return saldos
    End Function

    Private Shared Function CargarProveedoresAnalisis() As List(Of ChatProveedorInfo)
        Dim sql As String = "SELECT NroCuenta, Nombre, ISNULL(Cuit, '') AS Cuit, ISNULL(FechaAlta, '2000-01-01') AS FechaAlta, ISNULL(SaldoActual, 0) AS SaldoActual, ISNULL(SaldoDto, 0) AS SaldoDto FROM MaeCtaCte"
        Return MapearProveedores(DSM.ExecuteQuery(DSM.Proveedores, sql, Nothing))
    End Function

    Private Shared Function CargarMovimientosAnalisis(desde As DateTime?,
                                                      hastaExclusivo As DateTime?) As DataTable
        Dim filtroActual As String = " WHERE IdImputacion NOT IN (5,56)"
        Dim filtroAnual As String = " WHERE IdImputacion NOT IN (5,56)"
        Dim parametros As New Dictionary(Of String, Object)
        If desde.HasValue Then
            filtroActual &= " AND Fecha >= @Desde"
            filtroAnual &= " AND Fecha >= @Desde"
            parametros("@Desde") = desde.Value.Date
        End If
        If hastaExclusivo.HasValue Then
            filtroActual &= " AND Fecha < @HastaExclusivo"
            filtroAnual &= " AND Fecha < @HastaExclusivo"
            parametros("@HastaExclusivo") = hastaExclusivo.Value.Date
        End If

        Dim sql As String =
            "SELECT NroCuenta, Fecha, IdImputacion, CtaMonto, Monto " &
            "FROM DetaCtaCte" & filtroActual &
            " UNION ALL SELECT NroCuenta, Fecha, IdImputacion, CtaMonto, Monto " &
            "FROM DetaCtaCteAnual" & filtroAnual
        Return DSM.ExecuteQuery(DSM.Proveedores, sql, parametros)
    End Function

End Class

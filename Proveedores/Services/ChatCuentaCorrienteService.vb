Imports System.Data
Imports DSM = DataSourceManager.Lib.DataSourceManager

''' <summary>
''' Consultas de cuenta corriente destinadas exclusivamente al chatbot.
''' No modifica datos ni utiliza las tablas temporales del resumen histórico.
''' </summary>
Partial Public Class ChatCuentaCorrienteService

    Public Function BuscarProveedores(texto As String, Optional limite As Integer = 20) As List(Of ChatProveedorInfo)
        limite = LimitarCantidad(limite, 1, 100)

        Dim sql As String =
            "SELECT TOP (@Limite) NroCuenta, Nombre, ISNULL(Cuit, '') AS Cuit, " &
            "ISNULL(FechaAlta, '2000-01-01') AS FechaAlta, " &
            "ISNULL(SaldoActual, 0) AS SaldoActual, ISNULL(SaldoDto, 0) AS SaldoDto " &
            "FROM MaeCtaCte WHERE FechaBaja IS NULL"

        Dim parametros = CmdParams("@Limite", limite)
        Dim filtro As String = If(texto, String.Empty).Trim()
        If filtro <> String.Empty Then
            Dim nroCuenta As Integer
            If Integer.TryParse(filtro, nroCuenta) Then
                sql &= " AND (NroCuenta = @NroCuenta OR Nombre LIKE '%' + @Texto + '%' OR Cuit LIKE '%' + @Texto + '%')"
                parametros("@NroCuenta") = nroCuenta
            Else
                sql &= " AND (Nombre LIKE '%' + @Texto + '%' OR Cuit LIKE '%' + @Texto + '%')"
            End If
            parametros("@Texto") = filtro
        End If

        sql &= " ORDER BY Nombre"
        Return MapearProveedores(DSM.ExecuteQuery(DSM.Proveedores, sql, parametros))
    End Function

    Public Function ObtenerResumenHistorico(nroCuenta As Integer, desde As DateTime, hasta As DateTime) As ChatResumenCuenta
        If nroCuenta <= 0 Then Throw New ArgumentOutOfRangeException(NameOf(nroCuenta))
        If desde.Date > hasta.Date Then Throw New ArgumentException("La fecha desde no puede ser posterior a la fecha hasta.")

        Dim proveedor = ObtenerProveedor(nroCuenta)
        If proveedor Is Nothing Then
            Throw New InvalidOperationException($"No existe el proveedor con número de cuenta {nroCuenta}.")
        End If

        Dim sql As String =
            "SELECT NroCuenta, NroFactura, NroComprobante, NombreComprobante, Condicion, Fecha, " &
            "IdImputacion, CtaMonto, Monto, ISNULL(ACuenta, 0) AS ACuenta, Comentario, " &
            "ISNULL(Cobrado, 0) AS Cobrado, PuntoDeVenta, 'Actual' AS Origen " &
            "FROM DetaCtaCte WHERE NroCuenta = @NroCuenta AND Fecha < @HastaExclusivo " &
            "AND IdImputacion NOT IN (5,56) " &
            "UNION ALL " &
            "SELECT NroCuenta, NroFactura, NroComprobante, NombreComprobante, Condicion, Fecha, " &
            "IdImputacion, CtaMonto, Monto, ISNULL(ACuenta, 0) AS ACuenta, Comentario, " &
            "ISNULL(Cobrado, 0) AS Cobrado, PuntoDeVenta, 'Anual' AS Origen " &
            "FROM DetaCtaCteAnual WHERE NroCuenta = @NroCuenta AND Fecha < @HastaExclusivo " &
            "AND IdImputacion NOT IN (5,56) " &
            "ORDER BY Fecha, IdImputacion, NroComprobante"

        Dim parametros = CmdParams(
            "@NroCuenta", nroCuenta,
            "@HastaExclusivo", hasta.Date.AddDays(1))
        Dim tabla = DSM.ExecuteQuery(DSM.Proveedores, sql, parametros)
        Dim autocancelables = CargarAutocancelables(tabla)

        Dim resultado As New ChatResumenCuenta With {
            .Proveedor = proveedor,
            .Desde = desde.Date,
            .Hasta = hasta.Date
        }

        Dim saldo As Decimal = 0D
        If tabla IsNot Nothing Then
            For Each fila As DataRow In tabla.Rows
                Dim movimiento = MapearMovimiento(fila)
                Dim saldoPrevio = saldo
                AplicarMovimiento(saldo, movimiento, autocancelables)

                If movimiento.Fecha.Date < desde.Date Then
                    resultado.SaldoInicial = saldo
                Else
                    movimiento.Saldo = saldo
                    resultado.Movimientos.Add(movimiento)
                End If
            Next
        End If

        resultado.SaldoCalculado = saldo
        resultado.DiferenciaConSaldoActual = saldo - proveedor.SaldoActualMaestro
        Return resultado
    End Function

    Public Function ObtenerSaldoActualCalculado(nroCuenta As Integer) As ChatResumenCuenta
        Dim proveedor = ObtenerProveedor(nroCuenta)
        If proveedor Is Nothing Then
            Throw New InvalidOperationException($"No existe el proveedor con número de cuenta {nroCuenta}.")
        End If

        Return ObtenerResumenHistorico(nroCuenta, proveedor.FechaAlta.Date, Date.Today)
    End Function

    Public Function ListarFacturasPendientes(nroCuenta As Integer, Optional limite As Integer = 100) As List(Of ChatFacturaPendiente)
        If nroCuenta <= 0 Then Throw New ArgumentOutOfRangeException(NameOf(nroCuenta))
        limite = LimitarCantidad(limite, 1, 500)

        Dim sql As String =
            "SELECT TOP (@Limite) Q.NroCuenta, M.Nombre, Q.Fecha, Q.FechaVto, Q.PuntoDeVenta, " &
            "Q.NroFactura, Q.NroComprobante, Q.NombreComprobante, Q.Monto, Q.ACuenta, " &
            "Q.Monto - Q.ACuenta AS SaldoPendiente, Q.Origen " &
            "FROM (" &
            "SELECT NroCuenta, Fecha, FechaVto, PuntoDeVenta, NroFactura, NroComprobante, " &
            "NombreComprobante, Monto, ISNULL(ACuenta, 0) AS ACuenta, 'Actual' AS Origen " &
            "FROM DetaCtaCte WHERE NroCuenta = @NroCuenta AND IdImputacion IN (1,2,10) " &
            "AND ISNULL(Cobrado, 0) = 0 AND Monto - ISNULL(ACuenta, 0) > 0 " &
            "UNION ALL " &
            "SELECT NroCuenta, Fecha, FechaVto, PuntoDeVenta, NroFactura, NroComprobante, " &
            "NombreComprobante, Monto, ISNULL(ACuenta, 0) AS ACuenta, 'Anual' AS Origen " &
            "FROM DetaCtaCteAnual WHERE NroCuenta = @NroCuenta AND IdImputacion IN (1,2,10) " &
            "AND ISNULL(Cobrado, 0) = 0 AND Monto - ISNULL(ACuenta, 0) > 0" &
            ") Q INNER JOIN MaeCtaCte M ON M.NroCuenta = Q.NroCuenta " &
            "ORDER BY Q.Fecha, Q.NroFactura"

        Dim tabla = DSM.ExecuteQuery(
            DSM.Proveedores,
            sql,
            CmdParams("@Limite", limite, "@NroCuenta", nroCuenta))

        Dim resultado As New List(Of ChatFacturaPendiente)
        If tabla Is Nothing Then Return resultado

        For Each fila As DataRow In tabla.Rows
            Dim fechaVencimiento As DateTime? = Nothing
            If Not EsNulo(fila("FechaVto")) Then fechaVencimiento = Convert.ToDateTime(fila("FechaVto"))

            resultado.Add(New ChatFacturaPendiente With {
                .NroCuenta = Convert.ToInt32(fila("NroCuenta")),
                .Proveedor = Texto(fila("Nombre")),
                .Fecha = Convert.ToDateTime(fila("Fecha")),
                .FechaVencimiento = fechaVencimiento,
                .PuntoDeVenta = Texto(fila("PuntoDeVenta")),
                .NroFactura = Texto(fila("NroFactura")),
                .NroComprobante = Texto(fila("NroComprobante")),
                .NombreComprobante = Texto(fila("NombreComprobante")),
                .Monto = DecimalSeguro(fila("Monto")),
                .ACuenta = DecimalSeguro(fila("ACuenta")),
                .SaldoPendiente = DecimalSeguro(fila("SaldoPendiente")),
                .Origen = Texto(fila("Origen"))
            })
        Next

        Return resultado
    End Function

    Public Function ListarMayoresDeudas(Optional limite As Integer = 10) As List(Of ChatSaldoProveedor)
        limite = LimitarCantidad(limite, 1, 100)

        Dim sqlProveedores As String =
            "SELECT NroCuenta, Nombre, ISNULL(Cuit, '') AS Cuit, " &
            "ISNULL(FechaAlta, '2000-01-01') AS FechaAlta, " &
            "ISNULL(SaldoActual, 0) AS SaldoActual, ISNULL(SaldoDto, 0) AS SaldoDto " &
            "FROM MaeCtaCte WHERE FechaBaja IS NULL"
        Dim proveedores = MapearProveedores(DSM.ExecuteQuery(DSM.Proveedores, sqlProveedores, Nothing))

        Dim sqlMovimientos As String =
            "SELECT NroCuenta, IdImputacion, CtaMonto, SUM(Monto) AS Monto " &
            "FROM (" &
            "SELECT NroCuenta, IdImputacion, CtaMonto, Monto FROM DetaCtaCte WHERE IdImputacion NOT IN (5,56) " &
            "UNION ALL " &
            "SELECT NroCuenta, IdImputacion, CtaMonto, Monto FROM DetaCtaCteAnual WHERE IdImputacion NOT IN (5,56)" &
            ") T GROUP BY NroCuenta, IdImputacion, CtaMonto"
        Dim movimientos = DSM.ExecuteQuery(DSM.Proveedores, sqlMovimientos, Nothing)
        Dim autocancelables = CargarAutocancelables(movimientos)
        Dim saldos As New Dictionary(Of Integer, Decimal)

        If movimientos IsNot Nothing Then
            For Each fila As DataRow In movimientos.Rows
                Dim cuenta = Convert.ToInt32(fila("NroCuenta"))
                Dim saldo = If(saldos.ContainsKey(cuenta), saldos(cuenta), 0D)
                AplicarImporteAlSaldo(
                    saldo,
                    Convert.ToInt32(fila("IdImputacion")),
                    Texto(fila("CtaMonto")),
                    DecimalSeguro(fila("Monto")),
                    autocancelables)
                saldos(cuenta) = saldo
            Next
        End If

        Return proveedores.
            Select(Function(p)
                       Dim calculado = If(saldos.ContainsKey(p.NroCuenta), saldos(p.NroCuenta), 0D)
                       Return New ChatSaldoProveedor With {
                           .NroCuenta = p.NroCuenta,
                           .Nombre = p.Nombre,
                           .Cuit = p.Cuit,
                           .SaldoCalculado = calculado,
                           .SaldoActualMaestro = p.SaldoActualMaestro,
                           .Diferencia = calculado - p.SaldoActualMaestro
                       }
                   End Function).
            Where(Function(p) p.SaldoCalculado > 0D).
            OrderByDescending(Function(p) p.SaldoCalculado).
            Take(limite).
            ToList()
    End Function

    Private Function ObtenerProveedor(nroCuenta As Integer) As ChatProveedorInfo
        Dim sql As String =
            "SELECT NroCuenta, Nombre, ISNULL(Cuit, '') AS Cuit, " &
            "ISNULL(FechaAlta, '2000-01-01') AS FechaAlta, " &
            "ISNULL(SaldoActual, 0) AS SaldoActual, ISNULL(SaldoDto, 0) AS SaldoDto " &
            "FROM MaeCtaCte WHERE NroCuenta = @NroCuenta"
        Return MapearProveedores(
            DSM.ExecuteQuery(DSM.Proveedores, sql, CmdParams("@NroCuenta", nroCuenta))).
            FirstOrDefault()
    End Function

    Private Shared Function MapearProveedores(tabla As DataTable) As List(Of ChatProveedorInfo)
        Dim resultado As New List(Of ChatProveedorInfo)
        If tabla Is Nothing Then Return resultado

        For Each fila As DataRow In tabla.Rows
            resultado.Add(New ChatProveedorInfo With {
                .NroCuenta = Convert.ToInt32(fila("NroCuenta")),
                .Nombre = Texto(fila("Nombre")),
                .Cuit = Texto(fila("Cuit")),
                .FechaAlta = Convert.ToDateTime(fila("FechaAlta")),
                .SaldoActualMaestro = DecimalSeguro(fila("SaldoActual")),
                .SaldoDto = DecimalSeguro(fila("SaldoDto"))
            })
        Next
        Return resultado
    End Function

    Private Shared Function MapearMovimiento(fila As DataRow) As ChatMovimientoCuenta
        Return New ChatMovimientoCuenta With {
            .Fecha = Convert.ToDateTime(fila("Fecha")),
            .PuntoDeVenta = Texto(fila("PuntoDeVenta")),
            .NroFactura = Texto(fila("NroFactura")),
            .NroComprobante = Texto(fila("NroComprobante")),
            .NombreComprobante = Texto(fila("NombreComprobante")),
            .Condicion = Texto(fila("Condicion")),
            .IdImputacion = Convert.ToInt32(fila("IdImputacion")),
            .CtaMonto = Texto(fila("CtaMonto")),
            .Comentario = Texto(fila("Comentario")),
            .Cobrado = BooleanSeguro(fila("Cobrado")),
            .Monto = DecimalSeguro(fila("Monto")),
            .ACuenta = DecimalSeguro(fila("ACuenta")),
            .Origen = Texto(fila("Origen"))
        }
    End Function

    Private Shared Sub AplicarMovimiento(ByRef saldo As Decimal,
                                         movimiento As ChatMovimientoCuenta,
                                         autocancelables As Dictionary(Of String, Integer))
        If movimiento.IdImputacion < 50 Then
            movimiento.Haber = movimiento.Monto
        Else
            movimiento.Debe = movimiento.Monto
        End If

        AplicarImporteAlSaldo(
            saldo,
            movimiento.IdImputacion,
            movimiento.CtaMonto,
            movimiento.Monto,
            autocancelables)
    End Sub

    Private Shared Sub AplicarImporteAlSaldo(ByRef saldo As Decimal,
                                             idImputacion As Integer,
                                             ctaMonto As String,
                                             monto As Decimal,
                                             autocancelables As Dictionary(Of String, Integer))
        If idImputacion < 50 Then
            If String.Equals(ctaMonto, "2.1.1", StringComparison.OrdinalIgnoreCase) AndAlso idImputacion <> 6 Then
                saldo += monto
            End If
            Return
        End If

        Dim autocancelable As Integer = 0
        If Not String.IsNullOrWhiteSpace(ctaMonto) AndAlso autocancelables.ContainsKey(ctaMonto) Then
            autocancelable = autocancelables(ctaMonto)
        End If

        If autocancelable = 0 OrElse idImputacion = 54 Then
            saldo -= monto
        ElseIf String.Equals(ctaMonto, "1.1.1", StringComparison.OrdinalIgnoreCase) Then
            saldo -= monto
        End If
    End Sub

    Private Shared Function CargarAutocancelables(movimientos As DataTable) As Dictionary(Of String, Integer)
        Dim resultado As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
        If movimientos Is Nothing OrElse Not movimientos.Columns.Contains("CtaMonto") Then Return resultado

        Dim cuentas As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        For Each fila As DataRow In movimientos.Rows
            Dim cuenta = Texto(fila("CtaMonto"))
            If cuenta <> String.Empty Then cuentas.Add(cuenta)
        Next
        If cuentas.Count = 0 Then Return resultado

        Dim parametros As New Dictionary(Of String, Object)
        Dim nombres As New List(Of String)
        Dim indice As Integer = 0
        For Each cuenta In cuentas
            Dim nombre = "@Cuenta" & indice
            nombres.Add(nombre)
            parametros(nombre) = cuenta
            indice += 1
        Next

        Dim sql = "SELECT CodContable, ISNULL(Autocancelable, 0) AS Autocancelable " &
                  "FROM PlanCuentas WHERE CodContable IN (" & String.Join(",", nombres) & ")"
        Dim tabla = DSM.ExecuteQuery(DSM.Contabilidad, sql, parametros)
        If tabla Is Nothing Then Return resultado

        For Each fila As DataRow In tabla.Rows
            Dim cuenta = Texto(fila("CodContable"))
            If cuenta <> String.Empty Then resultado(cuenta) = Convert.ToInt32(fila("Autocancelable"))
        Next
        Return resultado
    End Function

    Private Shared Function LimitarCantidad(valor As Integer, minimo As Integer, maximo As Integer) As Integer
        Return Math.Max(minimo, Math.Min(maximo, valor))
    End Function

    Private Shared Function Texto(valor As Object) As String
        If EsNulo(valor) Then Return String.Empty
        Return Convert.ToString(valor).Trim()
    End Function

    Private Shared Function DecimalSeguro(valor As Object) As Decimal
        If EsNulo(valor) Then Return 0D
        Return Convert.ToDecimal(valor)
    End Function

    Private Shared Function BooleanSeguro(valor As Object) As Boolean
        If EsNulo(valor) Then Return False
        Return Convert.ToBoolean(valor)
    End Function

    Private Shared Function EsNulo(valor As Object) As Boolean
        Return valor Is Nothing OrElse Convert.IsDBNull(valor)
    End Function

End Class

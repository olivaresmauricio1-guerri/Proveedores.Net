Imports System.Data
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class ChatVehiculosService
    Public Function ResolverIdsPorPatentes(patentes As IEnumerable(Of String)) As List(Of Integer)
        Dim ids As New List(Of Integer)
        For Each patente In patentes.Distinct(StringComparer.OrdinalIgnoreCase)
            Dim normalizada = patente.Trim().Replace(" ", String.Empty).Replace("-", String.Empty).ToUpperInvariant()
            If normalizada.Length = 0 OrElse normalizada.Length > 15 Then
                Throw New ArgumentException("La patente debe contener entre 1 y 15 caracteres.")
            End If
            Dim tabla = DSM.ExecuteQuery(DSM.Proveedores,
                "SELECT IdVehiculo FROM Vehiculos WHERE UPPER(REPLACE(REPLACE(LTRIM(RTRIM(Patente)), ' ', ''), '-', '')) = @Patente",
                CmdParams("@Patente", normalizada))
            If tabla Is Nothing OrElse tabla.Rows.Count = 0 Then
                Throw New ArgumentException("No se encontró un vehículo con patente " & patente & ".")
            End If
            If tabla.Rows.Count > 1 Then
                Throw New ArgumentException("La patente " & patente & " corresponde a varios vehículos. Seleccione el IdVehiculo del listado.")
            End If
            ids.Add(Convert.ToInt32(tabla.Rows(0)("IdVehiculo")))
        Next
        Return ids.Distinct().ToList()
    End Function

    Public Function ListarVehiculos(Optional texto As String = "",
                                    Optional soloActivos As Boolean = True,
                                    Optional limite As Integer = 200) As List(Of ChatVehiculoInfo)
        limite = Math.Max(1, Math.Min(500, limite))
        Dim sql As String =
            "SELECT TOP (@Limite) IdVehiculo, Patente, ISNULL(Marca,'') AS Marca, " &
            "ISNULL(Modelo,'') AS Modelo, ISNULL(Anio,0) AS Anio, ISNULL(KmActual,0) AS KmActual, " &
            "ISNULL(IdSucursal,0) AS IdSucursal, ISNULL(Activo,0) AS Activo FROM Vehiculos WHERE 1=1"
        Dim parametros = CmdParams("@Limite", limite)
        If soloActivos Then sql &= " AND Activo=1"
        If Not String.IsNullOrWhiteSpace(texto) Then
            sql &= " AND (Patente LIKE '%' + @Texto + '%' OR Marca LIKE '%' + @Texto + '%' OR Modelo LIKE '%' + @Texto + '%')"
            parametros("@Texto") = texto.Trim()
        End If
        sql &= " ORDER BY Patente"

        Dim tabla = DSM.ExecuteQuery(DSM.Proveedores, sql, parametros)
        Dim sucursales = CargarSucursales()
        Dim resultado As New List(Of ChatVehiculoInfo)
        If tabla Is Nothing Then Return resultado
        For Each fila As DataRow In tabla.Rows
            Dim idSucursal = Convert.ToInt32(fila("IdSucursal"))
            ' El parámetro texto oculta la función Texto (VB no distingue mayúsculas).
            Dim patente = ChatVehiculosService.Texto(fila("Patente"))
            Dim marca = ChatVehiculosService.Texto(fila("Marca"))
            Dim modelo = ChatVehiculosService.Texto(fila("Modelo"))
            resultado.Add(New ChatVehiculoInfo With {
                .IdVehiculo = Convert.ToInt32(fila("IdVehiculo")), .Patente = patente,
                .Marca = marca, .Modelo = modelo, .Descripcion = DescribirVehiculo(patente, marca, modelo),
                .Anio = Convert.ToInt32(fila("Anio")), .KmActual = Convert.ToInt32(fila("KmActual")),
                .IdSucursal = idSucursal,
                .Sucursal = If(sucursales.ContainsKey(idSucursal), sucursales(idSucursal), String.Empty),
                .Activo = Convert.ToBoolean(fila("Activo"))})
        Next
        Return resultado
    End Function

    Public Function ListarServiciosRealizados(desde As DateTime, hasta As DateTime,
                                              idsVehiculo As List(Of Integer),
                                              Optional limite As Integer = 500) As List(Of ChatServicioRealizado)
        ValidarPeriodo(desde, hasta)
        limite = Math.Max(1, Math.Min(1000, limite))
        Dim parametros = CmdParams("@Desde", desde.Date, "@HastaExclusivo", hasta.Date.AddDays(1))
        Dim filtro = CrearFiltroVehiculos(idsVehiculo, parametros, "s.IdVehiculo")
        Dim sql As String =
            "SELECT s.IdServicio, s.FechaServicio, s.IdVehiculo, s.KmServicio, " &
            "ISNULL(s.ProveedorNombre,'') AS ProveedorNombre, ISNULL(s.PuntoDeVenta,'') AS PuntoDeVenta, " &
            "ISNULL(s.NroFactura,'') AS NroFactura, ISNULL(s.TotalServicio,0) AS TotalServicio, " &
            "ISNULL(s.Observaciones,'') AS Observaciones, v.Patente, ISNULL(v.Marca,'') AS Marca, " &
            "ISNULL(v.Modelo,'') AS Modelo, ISNULL(s.IdSucursal,ISNULL(v.IdSucursal,0)) AS IdSucursal, " &
            "COALESCE(tsi.Descripcion,tsh.Descripcion,'Sin tipo indicado') AS TipoServicio " &
            "FROM Servicios s INNER JOIN Vehiculos v ON v.IdVehiculo=s.IdVehiculo " &
            "LEFT JOIN ServiciosItems si ON si.IdServicio=s.IdServicio AND si.TipoItem='S' " &
            "LEFT JOIN TipoServicio tsi ON tsi.IdTipoServicio=si.IdTipoServicio " &
            "LEFT JOIN TipoServicio tsh ON tsh.IdTipoServicio=s.IdTipoServicio " &
            "WHERE s.FechaServicio>=@Desde AND s.FechaServicio<@HastaExclusivo" & filtro &
            " ORDER BY s.FechaServicio DESC, s.IdServicio DESC"
        Dim tabla = DSM.ExecuteQuery(DSM.Proveedores, sql, parametros)
        Dim sucursales = CargarSucursales()
        Dim servicios As New Dictionary(Of Integer, ChatServicioRealizado)
        Dim tipos As New Dictionary(Of Integer, HashSet(Of String))
        If tabla Is Nothing Then Return New List(Of ChatServicioRealizado)
        For Each fila As DataRow In tabla.Rows
            Dim id = Convert.ToInt32(fila("IdServicio"))
            If Not servicios.ContainsKey(id) Then
                Dim patente = Texto(fila("Patente"))
                Dim marca = Texto(fila("Marca"))
                Dim modelo = Texto(fila("Modelo"))
                Dim idSucursal = Convert.ToInt32(fila("IdSucursal"))
                servicios(id) = New ChatServicioRealizado With {
                    .IdServicio = id, .FechaServicio = Convert.ToDateTime(fila("FechaServicio")),
                    .IdVehiculo = Convert.ToInt32(fila("IdVehiculo")),
                    .Vehiculo = DescribirVehiculo(patente, marca, modelo),
                    .KmServicio = Convert.ToInt32(fila("KmServicio")),
                    .Sucursal = If(sucursales.ContainsKey(idSucursal), sucursales(idSucursal), String.Empty),
                    .Proveedor = Texto(fila("ProveedorNombre")),
                    .Comprobante = DescribirComprobante(fila("PuntoDeVenta"), fila("NroFactura")),
                    .TotalServicio = Convert.ToDecimal(fila("TotalServicio")),
                    .Observaciones = Texto(fila("Observaciones"))}
                tipos(id) = New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
            End If
            tipos(id).Add(Texto(fila("TipoServicio")))
        Next
        For Each item In servicios
            item.Value.TiposServicio = String.Join(", ", tipos(item.Key).Where(Function(x) x <> String.Empty))
        Next
        Return servicios.Values.OrderByDescending(Function(x) x.FechaServicio).
            ThenByDescending(Function(x) x.IdServicio).Take(limite).ToList()
    End Function

    Public Function CompararServiciosVehiculos(desde As DateTime, hasta As DateTime,
                                               idsVehiculo As List(Of Integer)) As List(Of ChatComparacionServicioVehiculo)
        ValidarPeriodo(desde, hasta)
        If idsVehiculo Is Nothing OrElse idsVehiculo.Count = 0 Then Return New List(Of ChatComparacionServicioVehiculo)
        Dim tabla = CargarFilasTiposServicio(desde, hasta, idsVehiculo)
        Dim acumuladores As New Dictionary(Of String, ComparacionAcumulador)(StringComparer.OrdinalIgnoreCase)
        If tabla Is Nothing Then Return New List(Of ChatComparacionServicioVehiculo)
        For Each fila As DataRow In tabla.Rows
            Dim idVehiculo = Convert.ToInt32(fila("IdVehiculo"))
            Dim tipo = Texto(fila("TipoServicio"))
            Dim clave = idVehiculo.ToString() & "|" & tipo
            If Not acumuladores.ContainsKey(clave) Then
                acumuladores(clave) = New ComparacionAcumulador With {
                    .Resultado = New ChatComparacionServicioVehiculo With {
                        .IdVehiculo = idVehiculo, .Vehiculo = Texto(fila("Vehiculo")), .TipoServicio = tipo}}
            End If
            Dim acumulador = acumuladores(clave)
            acumulador.Servicios.Add(Convert.ToInt32(fila("IdServicio")))
            acumulador.Resultado.CantidadRealizada += Convert.ToDecimal(fila("Cantidad"))
            acumulador.Resultado.ImporteItems += Convert.ToDecimal(fila("Importe"))
            Dim fecha = Convert.ToDateTime(fila("FechaServicio"))
            If Not acumulador.Resultado.UltimoServicio.HasValue OrElse fecha > acumulador.Resultado.UltimoServicio.Value Then
                acumulador.Resultado.UltimoServicio = fecha
            End If
        Next
        For Each acumulador In acumuladores.Values
            acumulador.Resultado.CantidadServicios = acumulador.Servicios.Count
        Next
        Return acumuladores.Values.Select(Function(x) x.Resultado).
            OrderBy(Function(x) x.Vehiculo).ThenByDescending(Function(x) x.CantidadServicios).
            ThenBy(Function(x) x.TipoServicio).ToList()
    End Function

    Public Function RankingServicios(desde As DateTime, hasta As DateTime,
                                     agrupacion As String, Optional limite As Integer = 50) As List(Of ChatRankingServicio)
        ValidarPeriodo(desde, hasta)
        limite = Math.Max(1, Math.Min(500, limite))
        agrupacion = If(agrupacion, String.Empty).Trim().ToLowerInvariant()
        If agrupacion <> "sucursal" AndAlso agrupacion <> "vehiculo" Then agrupacion = "total"
        Dim tabla = CargarFilasTiposServicio(desde, hasta, Nothing)
        Dim sucursales = CargarSucursales()
        Dim acumuladores As New Dictionary(Of String, RankingAcumulador)(StringComparer.OrdinalIgnoreCase)
        If tabla Is Nothing Then Return New List(Of ChatRankingServicio)
        For Each fila As DataRow In tabla.Rows
            Dim grupo As String
            Dim codigoGrupo As String
            If agrupacion = "sucursal" Then
                Dim idSucursal = Convert.ToInt32(fila("IdSucursal"))
                codigoGrupo = idSucursal.ToString()
                grupo = If(sucursales.ContainsKey(idSucursal), sucursales(idSucursal), "Sin sucursal")
            ElseIf agrupacion = "vehiculo" Then
                codigoGrupo = Convert.ToInt32(fila("IdVehiculo")).ToString()
                grupo = Texto(fila("Vehiculo"))
            Else
                codigoGrupo = "TOTAL"
                grupo = "Total"
            End If
            Dim tipo = Texto(fila("TipoServicio"))
            Dim clave = codigoGrupo & "|" & tipo
            If Not acumuladores.ContainsKey(clave) Then
                acumuladores(clave) = New RankingAcumulador With {
                    .Resultado = New ChatRankingServicio With {
                        .Agrupacion = agrupacion, .Grupo = grupo, .TipoServicio = tipo}}
            End If
            Dim acumulador = acumuladores(clave)
            acumulador.Servicios.Add(Convert.ToInt32(fila("IdServicio")))
            acumulador.Vehiculos.Add(Convert.ToInt32(fila("IdVehiculo")))
            acumulador.Resultado.CantidadRealizada += Convert.ToDecimal(fila("Cantidad"))
            acumulador.Resultado.ImporteItems += Convert.ToDecimal(fila("Importe"))
        Next
        Dim filas = acumuladores.Values.Select(Function(x)
            x.Resultado.CantidadServicios = x.Servicios.Count
            x.Resultado.VehiculosAtendidos = x.Vehiculos.Count
            Return x.Resultado
        End Function)
        Dim resultado As List(Of ChatRankingServicio)
        If agrupacion = "total" Then
            resultado = filas.OrderByDescending(Function(x) x.CantidadServicios).
                ThenByDescending(Function(x) x.CantidadRealizada).Take(limite).ToList()
        Else
            resultado = filas.OrderBy(Function(x) x.Grupo).
                ThenByDescending(Function(x) x.CantidadServicios).
                ThenByDescending(Function(x) x.CantidadRealizada).Take(limite).ToList()
        End If
        Dim posiciones As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
        For Each item In resultado
            Dim claveGrupo = If(agrupacion = "total", "TOTAL", item.Grupo)
            Dim posicion = If(posiciones.ContainsKey(claveGrupo), posiciones(claveGrupo) + 1, 1)
            posiciones(claveGrupo) = posicion
            item.Posicion = posicion
        Next
        Return resultado
    End Function

    Private Function CargarFilasTiposServicio(desde As DateTime, hasta As DateTime,
                                               idsVehiculo As List(Of Integer)) As DataTable
        Dim parametros = CmdParams("@Desde", desde.Date, "@HastaExclusivo", hasta.Date.AddDays(1))
        Dim filtro = CrearFiltroVehiculos(idsVehiculo, parametros, "s.IdVehiculo")
        Dim sql As String =
            "SELECT s.IdServicio, s.FechaServicio, s.IdVehiculo, " &
            "v.Patente + ' - ' + LTRIM(RTRIM(ISNULL(v.Marca,'') + ' ' + ISNULL(v.Modelo,''))) AS Vehiculo, " &
            "ISNULL(s.IdSucursal,ISNULL(v.IdSucursal,0)) AS IdSucursal, " &
            "COALESCE(tsi.Descripcion,tsh.Descripcion,'Sin tipo indicado') AS TipoServicio, " &
            "ISNULL(si.Cantidad,1) AS Cantidad, ISNULL(si.SubtotalItem,0) AS Importe " &
            "FROM Servicios s INNER JOIN Vehiculos v ON v.IdVehiculo=s.IdVehiculo " &
            "LEFT JOIN ServiciosItems si ON si.IdServicio=s.IdServicio AND si.TipoItem='S' " &
            "LEFT JOIN TipoServicio tsi ON tsi.IdTipoServicio=si.IdTipoServicio " &
            "LEFT JOIN TipoServicio tsh ON tsh.IdTipoServicio=s.IdTipoServicio " &
            "WHERE s.FechaServicio>=@Desde AND s.FechaServicio<@HastaExclusivo" & filtro
        Return DSM.ExecuteQuery(DSM.Proveedores, sql, parametros)
    End Function

    Private Shared Function CrearFiltroVehiculos(idsVehiculo As List(Of Integer),
                                                  parametros As Dictionary(Of String, Object),
                                                  campo As String) As String
        If idsVehiculo Is Nothing Then Return String.Empty
        Dim ids = idsVehiculo.Where(Function(x) x > 0).Distinct().Take(100).ToList()
        If ids.Count = 0 Then Return String.Empty
        Dim nombres As New List(Of String)
        For i As Integer = 0 To ids.Count - 1
            Dim nombre = "@Vehiculo" & i
            nombres.Add(nombre)
            parametros(nombre) = ids(i)
        Next
        Return " AND " & campo & " IN (" & String.Join(",", nombres) & ")"
    End Function

    Private Shared Function CargarSucursales() As Dictionary(Of Integer, String)
        Dim resultado As New Dictionary(Of Integer, String)
        Dim tabla = DSM.ExecuteQuery(DSM.Stock,
            "SELECT IdSucursal, Descripcion FROM Sucursales ORDER BY Descripcion", Nothing)
        If tabla Is Nothing Then Return resultado
        For Each fila As DataRow In tabla.Rows
            resultado(Convert.ToInt32(fila("IdSucursal"))) = Texto(fila("Descripcion"))
        Next
        Return resultado
    End Function

    Private Shared Function DescribirVehiculo(patente As String, marca As String, modelo As String) As String
        Dim detalle = String.Join(" ", New String() {marca, modelo}.
                                  Where(Function(x) Not String.IsNullOrWhiteSpace(x))).Trim()
        If detalle = String.Empty Then Return patente
        Return patente & " - " & detalle
    End Function

    Private Shared Function DescribirComprobante(puntoVenta As Object, nroFactura As Object) As String
        Dim pv = Texto(puntoVenta)
        Dim numero = Texto(nroFactura)
        If pv = String.Empty OrElse pv = "0" Then Return numero
        If numero = String.Empty OrElse numero = "0" Then Return pv
        Return pv & "-" & numero
    End Function

    Private Shared Function Texto(valor As Object) As String
        If valor Is Nothing OrElse Convert.IsDBNull(valor) Then Return String.Empty
        Return Convert.ToString(valor).Trim()
    End Function

    Private Shared Sub ValidarPeriodo(desde As DateTime, hasta As DateTime)
        If desde.Date > hasta.Date Then
            Throw New ArgumentException("La fecha desde no puede ser posterior a la fecha hasta.")
        End If
    End Sub

    Private Class ComparacionAcumulador
        Public Property Resultado As ChatComparacionServicioVehiculo
        Public Property Servicios As New HashSet(Of Integer)
    End Class

    Private Class RankingAcumulador
        Public Property Resultado As ChatRankingServicio
        Public Property Servicios As New HashSet(Of Integer)
        Public Property Vehiculos As New HashSet(Of Integer)
    End Class
End Class

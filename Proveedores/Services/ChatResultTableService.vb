Imports System.Data
Imports System.Text.Json

''' <summary>Adapta JSON del servidor a la grilla sin copiar DTO ni reglas contables.</summary>
Public Class ChatResultTableService
    Private Shared ReadOnly Colecciones As String() = {"filas", "facturas", "movimientos", "proveedores", "meses", "vehiculos", "servicios", "comparacion", "ranking"}

    Public Shared Function CrearTabla(datos As JsonElement, Optional nombreHerramienta As String = Nothing) As DataTable
        If datos.ValueKind <> JsonValueKind.Object Then Return Nothing
        If RequiereSeleccion(nombreHerramienta, datos) Then Return Nothing
        For Each nombre In Colecciones
            For Each propiedad In datos.EnumerateObject()
                If String.Equals(propiedad.Name, nombre, StringComparison.OrdinalIgnoreCase) AndAlso propiedad.Value.ValueKind = JsonValueKind.Array Then
                    Dim tabla = CrearFilas(propiedad.Value.EnumerateArray().ToList())
                    Dim agrupacion As JsonElement, ordenRanking As JsonElement
                    If datos.TryGetProperty("Agrupacion", agrupacion) AndAlso datos.TryGetProperty("OrdenRanking", ordenRanking) Then
                        Dim visibles As New List(Of String) From {"Posicion", If(agrupacion.GetString() = "articulo", "Articulo", "ClaveGrupo"), "Descripcion", "Cantidad", "Importe"}
                        If tabla.Columns.Contains("Periodo") AndAlso tabla.Rows.Cast(Of DataRow)().Any(Function(r) Not r.IsNull("Periodo") AndAlso r("Periodo").ToString() <> "") Then visibles.Insert(0, "Periodo")
                        Dim columnas = visibles.Where(Function(c) tabla.Columns.Contains(c)).ToArray()
                        If columnas.Length > 0 Then Return tabla.DefaultView.ToTable(False, columnas)
                    End If
                    Return tabla
                End If
            Next
        Next
        Return CrearFilas(New List(Of JsonElement) From {datos})
    End Function

    Public Shared Function RequiereSeleccion(nombre As String, datos As JsonElement) As Boolean
        If nombre <> "buscar_clientes" AndAlso nombre <> "buscar_proveedores" Then Return False
        If datos.ValueKind <> JsonValueKind.Object Then Return False
        For Each p In datos.EnumerateObject()
            If p.Name.Equals("CantidadTotal", StringComparison.OrdinalIgnoreCase) OrElse p.Name.Equals("cantidad_total", StringComparison.OrdinalIgnoreCase) Then
                Dim cantidad As Long
                If p.Value.ValueKind = JsonValueKind.Number AndAlso p.Value.TryGetInt64(cantidad) AndAlso cantidad > 1 Then Return True
            End If
            If p.Value.ValueKind = JsonValueKind.Array AndAlso p.Value.GetArrayLength() > 1 Then Return True
        Next
        Return False
    End Function

    Private Shared Function EsColumnaVisible(p As JsonProperty) As Boolean
        If p.Value.ValueKind = JsonValueKind.Array OrElse p.Value.ValueKind = JsonValueKind.Object Then Return False
        If p.Name.StartsWith("__", StringComparison.Ordinal) Then Return False
        Return Not {"exito", "fuente", "cantidadtotal", "cantidad_total", "cantidad_mostrada", "detalle_truncado", "advertencia"}.Contains(p.Name.ToLowerInvariant())
    End Function

    Private Shared Function CrearFilas(filas As List(Of JsonElement)) As DataTable
        Dim tabla As New DataTable() With {.Locale = Globalization.CultureInfo.CurrentCulture}
        For Each fila In filas
            If fila.ValueKind <> JsonValueKind.Object Then Continue For
            For Each p In fila.EnumerateObject()
                If EsColumnaVisible(p) AndAlso Not tabla.Columns.Contains(p.Name) Then tabla.Columns.Add(p.Name, GetType(Object))
            Next
        Next
        For Each fila In filas
            If fila.ValueKind <> JsonValueKind.Object Then Continue For
            Dim nueva = tabla.NewRow()
            For Each p In fila.EnumerateObject()
                If tabla.Columns.Contains(p.Name) Then nueva(p.Name) = ValorCelda(p.Value)
            Next
            If tabla.Columns.Count > 0 Then tabla.Rows.Add(nueva)
        Next
        Return tabla
    End Function

    Private Shared Function ValorCelda(valor As JsonElement) As Object
        Select Case valor.ValueKind
            Case JsonValueKind.Null, JsonValueKind.Undefined : Return DBNull.Value
            Case JsonValueKind.True : Return True
            Case JsonValueKind.False : Return False
            Case JsonValueKind.Number
                Dim numero As Decimal
                If valor.TryGetDecimal(numero) Then Return numero
                Return valor.GetRawText()
            Case JsonValueKind.String
                Dim fecha As DateTime
                If valor.TryGetDateTime(fecha) Then Return fecha
                Return valor.GetString()
            Case Else : Return valor.GetRawText()
        End Select
    End Function
End Class

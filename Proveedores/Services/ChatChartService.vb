Imports System.Data
Imports System.Globalization
Imports System.Text.Json

''' <summary>Selecciona columnas del resultado MCP. No recibe importes del modelo ni agrega filas.</summary>
Public Class ChatChartService
    Public Shared Function Crear(datos As JsonElement, argumentos As JsonElement) As ChatChartData
        If argumentos.ValueKind <> JsonValueKind.Object Then Throw New ArgumentException("Indicá tipo, eje_x y series para el gráfico.")
        For Each propiedad In argumentos.EnumerateObject()
            If Not {"tipo", "titulo", "eje_x", "series"}.Contains(propiedad.Name, StringComparer.Ordinal) Then Throw New ArgumentException("El gráfico solo acepta tipo, titulo, eje_x y series; los valores se toman del resultado MCP.")
        Next
        Dim tipo = Texto(argumentos, "tipo").ToLowerInvariant().Replace("í", "i")
        If Not {"lineas", "columnas", "barras"}.Contains(tipo) Then Throw New ArgumentException("Tipos disponibles: lineas, columnas o barras.")
        Dim tabla = ChatResultTableService.CrearTabla(datos)
        If tabla Is Nothing OrElse tabla.Rows.Count = 0 Then Throw New ArgumentException("Primero consultá un resultado con filas para graficar.")
        If tabla.Rows.Count > 120 Then Throw New ArgumentException("El resultado supera 120 filas. Pedí una consulta más acotada antes de graficar; no se recortarán datos silenciosamente.")
        Dim eje = ResolverColumna(tabla, Texto(argumentos, "eje_x"))
        Dim series As JsonElement
        If Not argumentos.TryGetProperty("series", series) OrElse series.ValueKind <> JsonValueKind.Array OrElse series.GetArrayLength() < 1 OrElse series.GetArrayLength() > 4 Then
            Throw New ArgumentException("Elegí entre una y cuatro columnas numéricas en series.")
        End If
        Dim titulo = Texto(argumentos, "titulo")
        If titulo.Length > 120 Then Throw New ArgumentException("El título admite hasta 120 caracteres.")
        Dim grafico As New ChatChartData With {.Tipo = tipo, .Titulo = If(titulo = "", "Gráfico de la consulta", titulo), .EjeX = eje.ColumnName}
        Dim nombres As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        For Each valor In series.EnumerateArray()
            If valor.ValueKind <> JsonValueKind.String Then Throw New ArgumentException("Series debe contener nombres de columnas, no importes.")
            Dim columna = ResolverColumna(tabla, valor.GetString())
            If Not nombres.Add(columna.ColumnName) Then Throw New ArgumentException("No repitas una columna en series.")
            Dim serie As New ChatChartSeries With {.Nombre = columna.ColumnName}
            For Each fila As DataRow In tabla.Rows
                Dim celda = fila(columna)
                If celda Is DBNull.Value OrElse celda Is Nothing Then
                    serie.Valores.Add(Nothing)
                ElseIf EsNumero(celda) Then
                    serie.Valores.Add(Convert.ToDecimal(celda, CultureInfo.InvariantCulture))
                Else
                    Throw New ArgumentException($"La columna {columna.ColumnName} no es numérica. Los textos, fechas y booleanos no se convierten en importes.")
                End If
            Next
            If Not serie.Valores.Any(Function(v) v.HasValue) Then Throw New ArgumentException($"La columna {columna.ColumnName} no tiene valores numéricos.")
            grafico.Series.Add(serie)
        Next
        For Each fila As DataRow In tabla.Rows
            Dim valor = fila(eje)
            If valor Is DBNull.Value OrElse valor Is Nothing Then
                grafico.Categorias.Add("(sin dato)")
            ElseIf TypeOf valor Is DateTime Then
                grafico.Categorias.Add(CDate(valor).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
            Else
                grafico.Categorias.Add(Convert.ToString(valor, CultureInfo.CurrentCulture))
            End If
        Next
        For Each p In datos.EnumerateObject()
            If String.Equals(p.Name, "detalle_truncado", StringComparison.OrdinalIgnoreCase) AndAlso p.Value.ValueKind = JsonValueKind.True Then grafico.DetalleTruncado = True
        Next
        Return grafico
    End Function

    Private Shared Function Texto(json As JsonElement, nombre As String) As String
        Dim valor As JsonElement
        If Not json.TryGetProperty(nombre, valor) OrElse valor.ValueKind = JsonValueKind.Null Then Return ""
        If valor.ValueKind <> JsonValueKind.String Then Throw New ArgumentException($"{nombre} debe ser texto.")
        Return If(valor.GetString(), "").Trim()
    End Function

    Private Shared Function ResolverColumna(tabla As DataTable, nombre As String) As DataColumn
        Dim encontrada = tabla.Columns.Cast(Of DataColumn)().FirstOrDefault(Function(c) String.Equals(c.ColumnName, nombre, StringComparison.OrdinalIgnoreCase))
        If encontrada Is Nothing Then Throw New ArgumentException("La columna solicitada no existe. Columnas disponibles: " & String.Join(", ", tabla.Columns.Cast(Of DataColumn)().Select(Function(c) c.ColumnName)))
        Return encontrada
    End Function

    Private Shared Function EsNumero(valor As Object) As Boolean
        Select Case Type.GetTypeCode(valor.GetType())
            Case TypeCode.Byte, TypeCode.SByte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32,
                 TypeCode.Int64, TypeCode.UInt64, TypeCode.Decimal, TypeCode.Double, TypeCode.Single
                Return True
            Case Else : Return False
        End Select
    End Function
End Class

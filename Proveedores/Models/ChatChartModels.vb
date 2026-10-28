Public Class ChatChartData
    Public Property Tipo As String
    Public Property Titulo As String
    Public Property EjeX As String
    Public Property Categorias As New List(Of String)
    Public Property Series As New List(Of ChatChartSeries)
    Public Property DetalleTruncado As Boolean
End Class

Public Class ChatChartSeries
    Public Property Nombre As String
    Public Property Valores As New List(Of Decimal?)
End Class

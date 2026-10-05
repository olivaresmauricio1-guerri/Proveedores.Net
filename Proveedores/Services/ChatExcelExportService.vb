Imports System.Runtime.InteropServices
Imports Microsoft.Office.Interop

Public Class ChatExcelExportService
    Public Sub Exportar(grid As DataGridView, rutaArchivo As String, Optional nombreHoja As String = "Consulta")
        If grid Is Nothing Then Throw New ArgumentNullException(NameOf(grid))
        If String.IsNullOrWhiteSpace(rutaArchivo) Then Throw New ArgumentException("La ruta de destino es obligatoria.", NameOf(rutaArchivo))

        Dim columnas = grid.Columns.Cast(Of DataGridViewColumn)().
            Where(Function(c) c.Visible).
            OrderBy(Function(c) c.DisplayIndex).
            ToList()
        Dim filas = grid.Rows.Cast(Of DataGridViewRow)().Where(Function(r) Not r.IsNewRow).ToList()

        If columnas.Count = 0 Then Throw New InvalidOperationException("No hay columnas visibles para exportar.")
        If filas.Count = 0 Then Throw New InvalidOperationException("No hay resultados para exportar.")

        Dim excelApp As Excel.Application = Nothing
        Dim libro As Excel.Workbook = Nothing
        Dim hoja As Excel.Worksheet = Nothing
        Dim rango As Excel.Range = Nothing
        Dim encabezado As Excel.Range = Nothing

        Try
            excelApp = New Excel.Application With {
                .Visible = False,
                .DisplayAlerts = False
            }
            libro = excelApp.Workbooks.Add()
            hoja = CType(libro.Worksheets(1), Excel.Worksheet)
            hoja.Name = NormalizarNombreHoja(nombreHoja)

            Dim valores(filas.Count, columnas.Count - 1) As Object
            For columna As Integer = 0 To columnas.Count - 1
                valores(0, columna) = columnas(columna).HeaderText
            Next

            For fila As Integer = 0 To filas.Count - 1
                For columna As Integer = 0 To columnas.Count - 1
                    Dim valor = filas(fila).Cells(columnas(columna).Index).Value
                    valores(fila + 1, columna) = If(valor Is Nothing OrElse Convert.IsDBNull(valor), String.Empty, valor)
                Next
            Next

            rango = hoja.Range("A1").Resize(filas.Count + 1, columnas.Count)
            rango.Value2 = valores
            rango.Borders.LineStyle = Excel.XlLineStyle.xlContinuous
            rango.Borders.Weight = Excel.XlBorderWeight.xlThin
            rango.Columns.AutoFit()

            encabezado = hoja.Range("A1").Resize(1, columnas.Count)
            encabezado.Font.Bold = True
            encabezado.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(31, 78, 121))
            encabezado.Font.Color = ColorTranslator.ToOle(Color.White)

            libro.SaveAs(rutaArchivo, Excel.XlFileFormat.xlOpenXMLWorkbook)
        Finally
            If libro IsNot Nothing Then libro.Close(False)
            If excelApp IsNot Nothing Then excelApp.Quit()

            LiberarCom(encabezado)
            LiberarCom(rango)
            LiberarCom(hoja)
            LiberarCom(libro)
            LiberarCom(excelApp)

            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try
    End Sub

    Private Shared Function NormalizarNombreHoja(nombre As String) As String
        Dim resultado = If(nombre, "Consulta").Trim()
        For Each caracter In New Char() {":"c, "\"c, "/"c, "?"c, "*"c, "["c, "]"c}
            resultado = resultado.Replace(caracter, "_"c)
        Next
        If resultado = String.Empty Then resultado = "Consulta"
        If resultado.Length > 31 Then resultado = resultado.Substring(0, 31)
        Return resultado
    End Function

    Private Shared Sub LiberarCom(valor As Object)
        If valor IsNot Nothing AndAlso Marshal.IsComObject(valor) Then
            Marshal.FinalReleaseComObject(valor)
        End If
    End Sub
End Class

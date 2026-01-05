Option Strict On
Option Explicit On

Imports System
Imports System.Data
Imports System.Diagnostics
Imports System.Globalization
Imports System.IO
Imports System.Security.Cryptography

Imports PdfSharpCore.Drawing
Imports PdfSharpCore.Pdf

Imports DSM = DataSourceManager.Lib.DataSourceManager

' =========================================================
' OrdenPagoPDF.vb
' Traducción directa de VB6 -> VB.NET + PdfSharpCore
' - Corrige errores: usar System.IO.Path / System.IO.Directory
' - Mantiene el layout por coordenadas en cm (origen abajo-izquierda)
' =========================================================
Public Module OrdenPagoPDF

    ' Ajustá esto si tu DSM usa otra firma / provider.
    ' La idea es que NO cambies tu arquitectura: sólo traducimos.
    ' Se asume que existe DSM.ExecuteQuery(conexion, sql, params?) -> DataTable.
    ' Si tu ExecuteQuery es diferente, adaptá SOLO esas 2 llamadas.
    Private ReadOnly CultureAR As CultureInfo = New CultureInfo("es-AR")

    Public Sub OrdenPagoPDF(idPropio As Long)
        Dim linea As Double
        Dim numero As String
        Dim reg As String
        Dim strfile As String
        Dim suma As Double = 0

        ' =========================
        ' 1) Traer datos (VB6: ADODB)
        ' =========================
        Dim sqlOrden As String = "Select * FROM OrdenPago Where IdPropio = " & idPropio.ToString(CultureInfo.InvariantCulture)
        Dim dtOrden As DataTable = DSM.ExecuteQuery(DSM.Proveedores, sqlOrden)

        If dtOrden Is Nothing OrElse dtOrden.Rows.Count = 0 Then
            Throw New Exception("No se encontró la OrdenPago para IdPropio=" & idPropio)
        End If

        Dim row0 As DataRow = dtOrden.Rows(0)

        Dim nroCuenta As String = Convert.ToString(row0("NroCuenta"))
        numero = Convert.ToString(row0("NroComprobante"))

        Dim sqlCta As String = "Select * FROM MaeCtacte Where NroCuenta = " & nroCuenta
        Dim dtCta As DataTable = DSM.ExecuteQuery(DSM.Proveedores, sqlCta)

        Dim nombreProveedor As String = ""
        If dtCta IsNot Nothing AndAlso dtCta.Rows.Count > 0 Then
            nombreProveedor = Convert.ToString(dtCta.Rows(0)("Nombre")).Trim()
        End If

        ' =========================
        ' 2) Definir salida 
        ' =========================
        Dim outDir As String = General.RutaProveedoresOP
        If Not Directory.Exists(outDir) Then
            Directory.CreateDirectory(outDir)
        End If

        strfile = Path.Combine(outDir, $"OrdenPago N {numero}.pdf")

        ' =========================
        ' 3) Crear PDF (PdfSharpCore)
        ' =========================
        Dim doc As New PdfDocument()
        doc.Info.Title = $"OrdenPago N° {numero}"

        Dim page As PdfPage = doc.AddPage()
        page.Size = PdfSharpCore.PageSize.A4
        page.Orientation = PdfSharpCore.PageOrientation.Portrait

        Using gfx As XGraphics = XGraphics.FromPdfPage(page)

            ' Fuentes (VB6: Fnt1..Fnt4)
            Dim fnt3 As XFont = CreateFontSafe("Arial", 9, XFontStyle.Italic)
            Dim fnt2 As XFont = CreateFontSafe("Arial", 8, XFontStyle.Regular)
            Dim fnt1 As XFont = CreateFontSafe("Arial", 9, XFontStyle.Bold)
            Dim fnt4 As XFont = CreateFontSafe("Arial", 11, XFontStyle.BoldItalic)

            ' Helpers de alineación
            Dim alignLeft As XStringFormat = XStringFormats.TopLeft
            Dim alignRight As XStringFormat = XStringFormats.TopRight
            Dim alignCenter As XStringFormat = XStringFormats.TopCenter

            ' =========================
            ' Encabezado (VB6: DrawText 15,27 etc)
            ' =========================
            DrawTextCm(gfx, page, 15, 27.2, "Orden de Pago Nro: ", fnt3, alignLeft)
            DrawTextCm(gfx, page, 18, 27.2, numero, CreateFontSafe("Arial", 9, XFontStyle.Bold), alignLeft)
            DrawTextCm(gfx, page, 1.7, 27.2, "GUERRINI Neumaticos S.A. ", fnt4, alignLeft)

            reg = DateTime.Now.ToString("dd/MM/yyyy", CultureAR)
            Dim hora As String = DateTime.Now.ToString("H:mm", CultureAR)
            DrawTextCm(gfx, page, 15, 26.7, $"{reg} - {hora} hs", fnt3, alignLeft)

            DrawTextCm(gfx, page, 0.5, 23.7, "Proveedor: ", fnt3, alignLeft)
            DrawTextCm(gfx, page, 2.5, 23.7, $"{nroCuenta} - {nombreProveedor}", CreateFontSafe("Arial", 9, XFontStyle.BoldItalic), alignLeft)

            Dim fechaEmisionStr As String = ""
            If Not IsDBNull(row0("Fecha")) Then
                fechaEmisionStr = Convert.ToString(row0("Fecha"))
            End If
            DrawTextCm(gfx, page, 0.5, 23.2, $"Emision: {fechaEmisionStr}", fnt3, alignLeft)

            ' Títulos
            DrawTextCm(gfx, page, 0.5, 22.2, "Forma de Pago", fnt1, alignLeft)
            DrawTextCm(gfx, page, 4.5, 22.2, "Importe", fnt1, alignLeft)
            DrawTextCm(gfx, page, 7.5, 22.2, "Fecha Vto", fnt1, alignLeft)
            DrawTextCm(gfx, page, 9.5, 22.2, "Nro Cheque", fnt1, alignLeft)
            DrawTextCm(gfx, page, 12, 22.2, "Reg Interno", fnt1, alignLeft)
            DrawTextCm(gfx, page, 14, 22.2, "Banco", fnt1, alignLeft)

            ' =========================
            ' Detalle
            ' =========================
            linea = 21.7

            For Each r As DataRow In dtOrden.Rows
                Dim condicion As String = If(IsDBNull(r("Condicion")), "", Convert.ToString(r("Condicion")))
                DrawTextCm(gfx, page, 0.5, linea, condicion, fnt2, alignLeft)

                Dim monto As Double = 0
                If Not IsDBNull(r("monto")) Then monto = Convert.ToDouble(r("monto"))
                Dim montoStr As String = monto.ToString("##,##0.00", CultureAR)
                DrawTextCm(gfx, page, 7, linea, montoStr, fnt2, alignRight)

                If Not IsDBNull(r("fechaVto")) Then
                    ' solo dia mes y año
                    Dim fechaVto As String = Convert.ToDateTime(r("fechaVto")).ToString().Substring(0, 10)
                    DrawTextCm(gfx, page, 7.5, linea, fechaVto, fnt2, alignLeft)
                End If

                Dim nroCheque As String = If(IsDBNull(r("NroCheque")), "", Convert.ToString(r("NroCheque")))
                DrawTextCm(gfx, page, 11, linea, nroCheque, fnt2, alignRight)

                Dim regInterno As String = If(IsDBNull(r("RegInterno")), "", Convert.ToString(r("RegInterno")))
                DrawTextCm(gfx, page, 13.5, linea, regInterno, fnt2, alignRight)

                If Not IsDBNull(r("Banco")) Then
                    DrawTextCm(gfx, page, 14, linea, Convert.ToString(r("Banco")), fnt2, alignLeft)
                End If

                linea -= 0.5
                suma += monto
            Next

            ' =========================
            ' Líneas / marco detalle
            ' =========================
            Dim penThin As New XPen(XColors.Black, CmToPt(0.02))
            Dim penMed As New XPen(XColors.Black, CmToPt(0.03))

            linea -= 0.4

            ' horizontal detalles
            DrawLineCm(gfx, page, penThin, 0.3, linea + 0.4, 20.3, linea + 0.4)

            ' vertical extremos
            DrawLineCm(gfx, page, penMed, 0.3, 22.3, 0.3, linea + 0.4)
            DrawLineCm(gfx, page, penMed, 20.3, 22.3, 20.3, linea + 0.4)

            ' vertical columnas
            DrawLineCm(gfx, page, penMed, 4.3, 22.3, 4.3, linea + 0.4)   ' importe
            DrawLineCm(gfx, page, penMed, 7.3, 22.3, 7.3, linea + 0.4)   ' fecha vto
            DrawLineCm(gfx, page, penMed, 9.3, 22.3, 9.3, linea + 0.4)   ' nro cheque
            DrawLineCm(gfx, page, penMed, 11.7, 22.3, 11.7, linea + 0.4) ' reg interno
            DrawLineCm(gfx, page, penMed, 13.8, 22.3, 13.8, linea + 0.4) ' banco

            ' Total
            linea -= 0.3
            DrawTextCm(gfx, page, 0.5, linea, "Total Orden de Pago:", fnt1, alignLeft)
            DrawTextCm(gfx, page, 6, linea, suma.ToString("##,##0.00", CultureAR), fnt1, alignCenter)

            ' Facturas imputadas
            linea -= 1
            DrawTextCm(gfx, page, 0.5, linea, "Facturas Imputadas:", fnt3, alignLeft)

            Dim imputadas As String = ""
            If dtOrden.Rows.Count > 0 AndAlso dtOrden.Columns.Contains("Imputa") AndAlso Not IsDBNull(dtOrden.Rows(0)("Imputa")) Then
                imputadas = Convert.ToString(dtOrden.Rows(0)("Imputa"))
            End If
            DrawTextCm(gfx, page, 5, linea, imputadas, fnt2, alignLeft)

            ' marco facturas imputadas
            linea -= 0.29
            DrawLineCm(gfx, page, penThin, 0.3, linea + 0.3, 20.3, linea + 0.3)
            DrawLineCm(gfx, page, penThin, 0.3, linea - 0.1, 20.3, linea - 0.1)
            DrawLineCm(gfx, page, penMed, 0.3, linea + 0.3, 0.3, linea - 0.1)
            DrawLineCm(gfx, page, penMed, 20.3, linea + 0.3, 20.3, linea - 0.1)

            ' Comentario
            linea -= 1.2
            Dim comentario As String = ""
            If dtOrden.Rows.Count > 0 AndAlso dtOrden.Columns.Contains("comentario") AndAlso Not IsDBNull(dtOrden.Rows(0)("comentario")) Then
                comentario = Convert.ToString(dtOrden.Rows(0)("comentario"))
            End If
            DrawTextCm(gfx, page, 0.5, linea, comentario, fnt2, alignLeft)

            ' marco comentarios
            linea -= 0.29
            DrawLineCm(gfx, page, penThin, 0.3, linea + 0.3, 20.3, linea + 0.3)
            DrawLineCm(gfx, page, penThin, 0.3, linea - 0.1, 20.3, linea - 0.1)
            DrawLineCm(gfx, page, penMed, 0.3, linea + 0.3, 0.3, linea - 0.1)
            DrawLineCm(gfx, page, penMed, 20.3, linea + 0.3, 20.3, linea - 0.1)

            ' líneas horizontales títulos (igual VB6)
            DrawLineCm(gfx, page, penThin, 0.3, 21.8, 20.3, 21.8)
            DrawLineCm(gfx, page, penThin, 0.3, 22.3, 20.3, 22.3)

        End Using

        doc.Save(strfile)

        ' VB6: Shell("rundll32.exe url.dll,FileProtocolHandler ...")
        ' VB.NET:
        Try
            Process.Start(New ProcessStartInfo With {
              .FileName = strfile,
              .UseShellExecute = True
            })
        Catch
            ' no romper si no puede abrir
        End Try
    End Sub

    ' =========================
    ' Helpers (cm -> points / origen abajo)
    ' =========================
    Private Function CmToPt(cm As Double) As Double
        ' 1 inch = 2.54 cm ; 1 inch = 72 points
        Return (cm / 2.54R) * 72.0R
    End Function

    Private Function YFromBottom(page As PdfPage, yCm As Double) As Double
        ' En VB6 la librería trabajaba como si y=0 fuera abajo.
        ' PdfSharp usa y=0 arriba. Invertimos.
        Dim yPt As Double = CmToPt(yCm)
        Return page.Height.Point - yPt
    End Function

    Private Sub DrawTextCm(gfx As XGraphics, page As PdfPage, xCm As Double, yCm As Double, text As String, font As XFont, fmt As XStringFormat)
        Dim xPt As Double = CmToPt(xCm)
        Dim yPt As Double = YFromBottom(page, yCm)
        gfx.DrawString(text, font, XBrushes.Black, New XPoint(xPt, yPt), fmt)
    End Sub

    Private Sub DrawLineCm(gfx As XGraphics, page As PdfPage, pen As XPen, x1Cm As Double, y1Cm As Double, x2Cm As Double, y2Cm As Double)
        Dim x1 As Double = CmToPt(x1Cm)
        Dim y1 As Double = YFromBottom(page, y1Cm)
        Dim x2 As Double = CmToPt(x2Cm)
        Dim y2 As Double = YFromBottom(page, y2Cm)
        gfx.DrawLine(pen, x1, y1, x2, y2)
    End Sub

    Private Function CreateFontSafe(family As String, size As Double, style As XFontStyle) As XFont
        Try
            Return New XFont(family, size, style)
        Catch
            ' fallback típico si no existe la fuente en el server/PC
            Return New XFont("Arial", size, style)
        End Try
    End Function

End Module

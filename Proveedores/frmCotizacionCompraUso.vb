Imports System.Diagnostics
Imports System.Globalization
Imports System.IO
Imports System.Linq

Public Class frmCotizacionCompraUso
    Private servicio As SolicitudCompraUsoService
    Private editable As Boolean = True
    Private original As New SolicitudCompraUsoCotizacion()
    Private ReadOnly adjuntos As New List(Of SolicitudCompraUsoPdf)
    Private cuenta As Integer?
    Private cargando As Boolean
    Public Property Resultado As SolicitudCompraUsoCotizacion

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(cotizacion As SolicitudCompraUsoCotizacion, puedeEditar As Boolean, datos As SolicitudCompraUsoService)
        Me.New()
        original = cotizacion
        editable = puedeEditar
        servicio = datos
        cargando = True
        Try
            TxtProveedor.Text = cotizacion.ProveedorNombre
            cuenta = cotizacion.NroCuentaProveedor
            TxtNumero.Text = cotizacion.NumeroCotizacion
            DtpFecha.Value = cotizacion.FechaCotizacion
            DtpVencimiento.Value = If(cotizacion.FechaVencimiento, Date.Today)
            DtpVencimiento.Checked = cotizacion.FechaVencimiento.HasValue
            CmbMoneda.Text = cotizacion.Moneda
            TxtNeto.Text = Formato(cotizacion.ImporteNeto)
            TxtIVA.Text = Formato(cotizacion.ImporteIVA)
            TxtOtros.Text = Formato(cotizacion.OtrosImportes)
            TxtTotal.Text = Formato(cotizacion.ImporteTotal)
            TxtPago.Text = cotizacion.CondicionPago
            TxtEntrega.Text = cotizacion.PlazoEntrega
            TxtObservaciones.Text = cotizacion.Observaciones
            For Each pdf In cotizacion.Adjuntos
                adjuntos.Add(New SolicitudCompraUsoPdf With {.IdAdjunto = pdf.IdAdjunto, .NombreArchivo = pdf.NombreArchivo, .TamanoBytes = pdf.TamanoBytes, .Contenido = pdf.Contenido})
            Next
            RefrescarArchivos()
            AplicarEdicion()
        Finally
            cargando = False
        End Try
    End Sub

    Private Sub AplicarEdicion()
        Text = If(editable, "Cotización de compra", "Consultar cotización")
        For Each caja In New TextBox() {TxtProveedor, TxtNumero, TxtNeto, TxtIVA, TxtOtros, TxtTotal, TxtPago, TxtEntrega, TxtObservaciones}
            caja.ReadOnly = Not editable
        Next
        DtpFecha.Enabled = editable
        DtpVencimiento.Enabled = editable
        CmbMoneda.Enabled = editable
        BtnBuscarProveedor.Enabled = editable
        BtnAdjuntar.Enabled = editable
        BtnQuitar.Enabled = editable
        BtnAceptar.Visible = editable
        BtnCerrar.Text = If(editable, "Cancelar", "Cerrar")
        AcceptButton = If(editable, BtnAceptar, Nothing)
    End Sub

    Private Sub TxtProveedor_TextChanged(sender As Object, e As EventArgs) Handles TxtProveedor.TextChanged
        If Not cargando Then cuenta = Nothing
    End Sub

    Private Sub BtnBuscarProveedor_Click(sender As Object, e As EventArgs) Handles BtnBuscarProveedor.Click
        Using dlg As New frmProveedoresSelector()
            If dlg.ShowDialog(Me) = DialogResult.OK AndAlso dlg.Seleccion IsNot Nothing Then
                TxtProveedor.Text = Convert.ToString(dlg.Seleccion("Nombre"))
                cuenta = Convert.ToInt32(dlg.Seleccion("NroCuenta"))
            End If
        End Using
    End Sub

    Private Function CampoImporte(nombre As String) As TextBox
        Select Case nombre
            Case "Neto" : Return TxtNeto
            Case "IVA" : Return TxtIVA
            Case "Otros" : Return TxtOtros
            Case "Total" : Return TxtTotal
            Case Else : Throw New ArgumentException("Importe no reconocido.")
        End Select
    End Function

    Private Shared Function Formato(importe As Decimal?) As String
        Return If(importe.HasValue, importe.GetValueOrDefault().ToString("N2"), "")
    End Function

    Private Function LeerImporte(nombre As String) As Decimal?
        Dim texto = CampoImporte(nombre).Text.Trim()
        If texto = "" Then Return Nothing
        Dim resultado As Decimal
        If Not Decimal.TryParse(texto, NumberStyles.Number, CultureInfo.CurrentCulture, resultado) Then Throw New InvalidOperationException("Revise el importe de " & nombre & ".")
        Return resultado
    End Function

    Private Sub Aceptar(sender As Object, e As EventArgs) Handles BtnAceptar.Click
        Try
            Dim cot As New SolicitudCompraUsoCotizacion With {
                .IdCotizacion = original.IdCotizacion, .NroCuentaProveedor = cuenta,
                .ProveedorNombre = TxtProveedor.Text.Trim(), .NumeroCotizacion = TxtNumero.Text.Trim(),
                .FechaCotizacion = DtpFecha.Value.Date, .FechaVencimiento = If(DtpVencimiento.Checked, CType(DtpVencimiento.Value.Date, Date?), Nothing),
                .Moneda = CmbMoneda.Text.Trim().ToUpperInvariant(), .ImporteNeto = LeerImporte("Neto"), .ImporteIVA = LeerImporte("IVA"),
                .OtrosImportes = LeerImporte("Otros"), .ImporteTotal = LeerImporte("Total"),
                .CondicionPago = TxtPago.Text.Trim(), .PlazoEntrega = TxtEntrega.Text.Trim(),
                .Observaciones = TxtObservaciones.Text.Trim(), .Adjuntos = adjuntos}
            SolicitudCompraUsoService.ValidarCotizacion(cot)
            Resultado = cot
            DialogResult = DialogResult.OK
            Close()
        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, "Cotización", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub RefrescarArchivos()
        LstArchivos.DataSource = Nothing
        LstArchivos.DataSource = adjuntos.ToList()
    End Sub

    Private Sub Adjuntar(sender As Object, e As EventArgs) Handles BtnAdjuntar.Click
        Using dlg As New OpenFileDialog With {.Title = "Adjuntar cotización", .Filter = "Archivos PDF (*.pdf)|*.pdf", .Multiselect = True}
            If dlg.ShowDialog(Me) <> DialogResult.OK Then Return
            For Each ruta In dlg.FileNames
                Try
                    Dim bytes = File.ReadAllBytes(ruta)
                    SolicitudCompraUsoService.ValidarPdf(bytes)
                    adjuntos.Add(New SolicitudCompraUsoPdf With {.NombreArchivo = Path.GetFileName(ruta), .TamanoBytes = bytes.LongLength, .Contenido = bytes})
                Catch ex As Exception
                    MessageBox.Show(Me, Path.GetFileName(ruta) & ": " & ex.Message, "Adjuntar PDF", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Try
            Next
            RefrescarArchivos()
        End Using
    End Sub

    Private Sub Quitar(sender As Object, e As EventArgs) Handles BtnQuitar.Click
        Dim pdf = TryCast(LstArchivos.SelectedItem, SolicitudCompraUsoPdf)
        If pdf Is Nothing Then Return
        If MessageBox.Show(Me, "¿Quitar " & pdf.NombreArchivo & " de la cotización?", "PDF", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
        adjuntos.Remove(pdf)
        RefrescarArchivos()
    End Sub

    Private Function Contenido(pdf As SolicitudCompraUsoPdf) As Byte()
        Return If(pdf.Contenido, servicio.LeerPdf(pdf.IdAdjunto, original.IdCotizacion))
    End Function

    Private Sub Abrir(sender As Object, e As EventArgs) Handles BtnAbrirPdf.Click
        Dim pdf = TryCast(LstArchivos.SelectedItem, SolicitudCompraUsoPdf)
        If pdf Is Nothing Then Return
        Try
            Dim carpeta = Path.Combine(Path.GetTempPath(), "Proveedores", "Cotizaciones", Guid.NewGuid().ToString("N"))
            Directory.CreateDirectory(carpeta)
            Dim ruta = Path.Combine(carpeta, Path.GetFileName(pdf.NombreArchivo))
            File.WriteAllBytes(ruta, Contenido(pdf))
            Process.Start(New ProcessStartInfo(ruta) With {.UseShellExecute = True})
        Catch ex As Exception
            MessageBox.Show(Me, "No se pudo abrir el PDF. " & ex.Message, "PDF", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Exportar(sender As Object, e As EventArgs) Handles BtnExportar.Click
        Dim pdf = TryCast(LstArchivos.SelectedItem, SolicitudCompraUsoPdf)
        If pdf Is Nothing Then Return
        Using dlg As New SaveFileDialog With {.Filter = "Archivos PDF (*.pdf)|*.pdf", .FileName = Path.GetFileName(pdf.NombreArchivo), .DefaultExt = "pdf", .AddExtension = True}
            If dlg.ShowDialog(Me) <> DialogResult.OK Then Return
            Try
                File.WriteAllBytes(dlg.FileName, Contenido(pdf))
            Catch ex As Exception
                MessageBox.Show(Me, "No se pudo guardar el PDF. " & ex.Message, "PDF", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub
End Class

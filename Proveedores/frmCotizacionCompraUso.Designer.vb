<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCotizacionCompraUso
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        PanelPrincipal = New TableLayoutPanel()
        PanelDatos = New TableLayoutPanel()
        LblProveedor = New Label()
        PanelProveedor = New TableLayoutPanel()
        TxtProveedor = New TextBox()
        BtnBuscarProveedor = New Button()
        LblTxtNumero = New Label()
        TxtNumero = New TextBox()
        LblDtpFecha = New Label()
        DtpFecha = New DateTimePicker()
        LblCmbMoneda = New Label()
        CmbMoneda = New ComboBox()
        LblDtpVencimiento = New Label()
        DtpVencimiento = New DateTimePicker()
        LblTxtNeto = New Label()
        TxtNeto = New TextBox()
        LblTxtIVA = New Label()
        TxtIVA = New TextBox()
        LblTxtOtros = New Label()
        TxtOtros = New TextBox()
        LblTxtTotal = New Label()
        TxtTotal = New TextBox()
        LblTxtPago = New Label()
        TxtPago = New TextBox()
        LblTxtEntrega = New Label()
        TxtEntrega = New TextBox()
        LblTxtObservaciones = New Label()
        TxtObservaciones = New TextBox()
        LblImportes = New Label()
        GrupoPdf = New GroupBox()
        PanelPdf = New TableLayoutPanel()
        LstArchivos = New ListBox()
        BarraPdf = New FlowLayoutPanel()
        BtnAdjuntar = New Button()
        BtnQuitar = New Button()
        BtnAbrirPdf = New Button()
        BtnExportar = New Button()
        BarraAcciones = New FlowLayoutPanel()
        BtnCerrar = New Button()
        BtnAceptar = New Button()
        PanelPrincipal.SuspendLayout()
        PanelDatos.SuspendLayout()
        PanelProveedor.SuspendLayout()
        GrupoPdf.SuspendLayout()
        PanelPdf.SuspendLayout()
        BarraPdf.SuspendLayout()
        BarraAcciones.SuspendLayout()
        SuspendLayout()
        ' 
        ' PanelPrincipal
        ' 
        PanelPrincipal.ColumnCount = 1
        PanelPrincipal.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 20F))
        PanelPrincipal.Controls.Add(PanelDatos, 0, 0)
        PanelPrincipal.Controls.Add(GrupoPdf, 0, 1)
        PanelPrincipal.Controls.Add(BarraAcciones, 0, 2)
        PanelPrincipal.Dock = DockStyle.Fill
        PanelPrincipal.Location = New Point(0, 0)
        PanelPrincipal.Name = "PanelPrincipal"
        PanelPrincipal.Padding = New Padding(10)
        PanelPrincipal.RowCount = 3
        PanelPrincipal.RowStyles.Add(New RowStyle(SizeType.Absolute, 324F))
        PanelPrincipal.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        PanelPrincipal.RowStyles.Add(New RowStyle(SizeType.Absolute, 42F))
        PanelPrincipal.Size = New Size(773, 705)
        PanelPrincipal.TabIndex = 0
        ' 
        ' PanelDatos
        ' 
        PanelDatos.ColumnCount = 4
        PanelDatos.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 140F))
        PanelDatos.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        PanelDatos.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 110F))
        PanelDatos.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        PanelDatos.Controls.Add(LblProveedor, 0, 0)
        PanelDatos.Controls.Add(PanelProveedor, 1, 0)
        PanelDatos.Controls.Add(LblTxtNumero, 0, 1)
        PanelDatos.Controls.Add(TxtNumero, 1, 1)
        PanelDatos.Controls.Add(LblDtpFecha, 2, 1)
        PanelDatos.Controls.Add(DtpFecha, 3, 1)
        PanelDatos.Controls.Add(LblCmbMoneda, 0, 2)
        PanelDatos.Controls.Add(CmbMoneda, 1, 2)
        PanelDatos.Controls.Add(LblDtpVencimiento, 2, 2)
        PanelDatos.Controls.Add(DtpVencimiento, 3, 2)
        PanelDatos.Controls.Add(LblTxtNeto, 0, 3)
        PanelDatos.Controls.Add(TxtNeto, 1, 3)
        PanelDatos.Controls.Add(LblTxtIVA, 2, 3)
        PanelDatos.Controls.Add(TxtIVA, 3, 3)
        PanelDatos.Controls.Add(LblTxtOtros, 0, 4)
        PanelDatos.Controls.Add(TxtOtros, 1, 4)
        PanelDatos.Controls.Add(LblTxtTotal, 2, 4)
        PanelDatos.Controls.Add(TxtTotal, 3, 4)
        PanelDatos.Controls.Add(LblTxtPago, 0, 5)
        PanelDatos.Controls.Add(TxtPago, 1, 5)
        PanelDatos.Controls.Add(LblTxtEntrega, 0, 6)
        PanelDatos.Controls.Add(TxtEntrega, 1, 6)
        PanelDatos.Controls.Add(LblTxtObservaciones, 0, 7)
        PanelDatos.Controls.Add(TxtObservaciones, 1, 7)
        PanelDatos.Controls.Add(LblImportes, 0, 8)
        PanelDatos.Dock = DockStyle.Fill
        PanelDatos.Location = New Point(13, 13)
        PanelDatos.Name = "PanelDatos"
        PanelDatos.RowCount = 9
        PanelDatos.RowStyles.Add(New RowStyle(SizeType.Absolute, 36F))
        PanelDatos.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        PanelDatos.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        PanelDatos.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        PanelDatos.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        PanelDatos.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        PanelDatos.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        PanelDatos.RowStyles.Add(New RowStyle(SizeType.Absolute, 60F))
        PanelDatos.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        PanelDatos.Size = New Size(747, 318)
        PanelDatos.TabIndex = 0
        ' 
        ' LblProveedor
        ' 
        LblProveedor.AutoSize = True
        LblProveedor.Location = New Point(3, 7)
        LblProveedor.Margin = New Padding(3, 7, 3, 3)
        LblProveedor.Name = "LblProveedor"
        LblProveedor.Size = New Size(61, 15)
        LblProveedor.TabIndex = 0
        LblProveedor.Text = "Proveedor"
        ' 
        ' PanelProveedor
        ' 
        PanelProveedor.ColumnCount = 2
        PanelDatos.SetColumnSpan(PanelProveedor, 3)
        PanelProveedor.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        PanelProveedor.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 140F))
        PanelProveedor.Controls.Add(TxtProveedor, 0, 0)
        PanelProveedor.Controls.Add(BtnBuscarProveedor, 1, 0)
        PanelProveedor.Dock = DockStyle.Fill
        PanelProveedor.Location = New Point(140, 0)
        PanelProveedor.Margin = New Padding(0)
        PanelProveedor.Name = "PanelProveedor"
        PanelProveedor.RowCount = 1
        PanelProveedor.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        PanelProveedor.Size = New Size(607, 36)
        PanelProveedor.TabIndex = 1
        ' 
        ' TxtProveedor
        ' 
        TxtProveedor.Dock = DockStyle.Fill
        TxtProveedor.Location = New Point(3, 3)
        TxtProveedor.MaxLength = 150
        TxtProveedor.Name = "TxtProveedor"
        TxtProveedor.Size = New Size(461, 23)
        TxtProveedor.TabIndex = 0
        ' 
        ' BtnBuscarProveedor
        ' 
        BtnBuscarProveedor.Cursor = Cursors.Hand
        BtnBuscarProveedor.Dock = DockStyle.Fill
        BtnBuscarProveedor.FlatStyle = FlatStyle.Flat
        BtnBuscarProveedor.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BtnBuscarProveedor.Location = New Point(470, 3)
        BtnBuscarProveedor.Name = "BtnBuscarProveedor"
        BtnBuscarProveedor.Size = New Size(134, 30)
        BtnBuscarProveedor.TabIndex = 1
        BtnBuscarProveedor.Text = "Buscar proveedor"
        ' 
        ' LblTxtNumero
        ' 
        LblTxtNumero.AutoSize = True
        LblTxtNumero.Location = New Point(3, 43)
        LblTxtNumero.Margin = New Padding(3, 7, 3, 3)
        LblTxtNumero.Name = "LblTxtNumero"
        LblTxtNumero.Size = New Size(78, 15)
        LblTxtNumero.TabIndex = 2
        LblTxtNumero.Text = "N° cotización"
        ' 
        ' TxtNumero
        ' 
        TxtNumero.Dock = DockStyle.Fill
        TxtNumero.Location = New Point(143, 39)
        TxtNumero.MaxLength = 50
        TxtNumero.Name = "TxtNumero"
        TxtNumero.Size = New Size(242, 23)
        TxtNumero.TabIndex = 3
        ' 
        ' LblDtpFecha
        ' 
        LblDtpFecha.AutoSize = True
        LblDtpFecha.Location = New Point(391, 43)
        LblDtpFecha.Margin = New Padding(3, 7, 3, 3)
        LblDtpFecha.Name = "LblDtpFecha"
        LblDtpFecha.Size = New Size(38, 15)
        LblDtpFecha.TabIndex = 4
        LblDtpFecha.Text = "Fecha"
        ' 
        ' DtpFecha
        ' 
        DtpFecha.Dock = DockStyle.Fill
        DtpFecha.Format = DateTimePickerFormat.Short
        DtpFecha.Location = New Point(501, 39)
        DtpFecha.Name = "DtpFecha"
        DtpFecha.Size = New Size(243, 23)
        DtpFecha.TabIndex = 5
        ' 
        ' LblCmbMoneda
        ' 
        LblCmbMoneda.AutoSize = True
        LblCmbMoneda.Location = New Point(3, 73)
        LblCmbMoneda.Margin = New Padding(3, 7, 3, 3)
        LblCmbMoneda.Name = "LblCmbMoneda"
        LblCmbMoneda.Size = New Size(51, 15)
        LblCmbMoneda.TabIndex = 6
        LblCmbMoneda.Text = "Moneda"
        ' 
        ' CmbMoneda
        ' 
        CmbMoneda.BackColor = Color.White
        CmbMoneda.Dock = DockStyle.Fill
        CmbMoneda.Items.AddRange(New Object() {"ARS", "USD", "EUR"})
        CmbMoneda.Location = New Point(143, 69)
        CmbMoneda.MaxLength = 3
        CmbMoneda.Name = "CmbMoneda"
        CmbMoneda.Size = New Size(242, 23)
        CmbMoneda.TabIndex = 7
        ' 
        ' LblDtpVencimiento
        ' 
        LblDtpVencimiento.AutoSize = True
        LblDtpVencimiento.Location = New Point(391, 73)
        LblDtpVencimiento.Margin = New Padding(3, 7, 3, 3)
        LblDtpVencimiento.Name = "LblDtpVencimiento"
        LblDtpVencimiento.Size = New Size(73, 15)
        LblDtpVencimiento.TabIndex = 8
        LblDtpVencimiento.Text = "Vencimiento"
        ' 
        ' DtpVencimiento
        ' 
        DtpVencimiento.Checked = False
        DtpVencimiento.Dock = DockStyle.Fill
        DtpVencimiento.Format = DateTimePickerFormat.Short
        DtpVencimiento.Location = New Point(501, 69)
        DtpVencimiento.Name = "DtpVencimiento"
        DtpVencimiento.ShowCheckBox = True
        DtpVencimiento.Size = New Size(243, 23)
        DtpVencimiento.TabIndex = 9
        ' 
        ' LblTxtNeto
        ' 
        LblTxtNeto.AutoSize = True
        LblTxtNeto.Location = New Point(3, 103)
        LblTxtNeto.Margin = New Padding(3, 7, 3, 3)
        LblTxtNeto.Name = "LblTxtNeto"
        LblTxtNeto.Size = New Size(33, 15)
        LblTxtNeto.TabIndex = 10
        LblTxtNeto.Text = "Neto"
        ' 
        ' TxtNeto
        ' 
        TxtNeto.Dock = DockStyle.Fill
        TxtNeto.Location = New Point(143, 99)
        TxtNeto.MaxLength = 30
        TxtNeto.Name = "TxtNeto"
        TxtNeto.Size = New Size(242, 23)
        TxtNeto.TabIndex = 11
        TxtNeto.TextAlign = HorizontalAlignment.Right
        ' 
        ' LblTxtIVA
        ' 
        LblTxtIVA.AutoSize = True
        LblTxtIVA.Location = New Point(391, 103)
        LblTxtIVA.Margin = New Padding(3, 7, 3, 3)
        LblTxtIVA.Name = "LblTxtIVA"
        LblTxtIVA.Size = New Size(24, 15)
        LblTxtIVA.TabIndex = 12
        LblTxtIVA.Text = "IVA"
        ' 
        ' TxtIVA
        ' 
        TxtIVA.Dock = DockStyle.Fill
        TxtIVA.Location = New Point(501, 99)
        TxtIVA.MaxLength = 30
        TxtIVA.Name = "TxtIVA"
        TxtIVA.Size = New Size(243, 23)
        TxtIVA.TabIndex = 13
        TxtIVA.TextAlign = HorizontalAlignment.Right
        ' 
        ' LblTxtOtros
        ' 
        LblTxtOtros.AutoSize = True
        LblTxtOtros.Location = New Point(3, 133)
        LblTxtOtros.Margin = New Padding(3, 7, 3, 3)
        LblTxtOtros.Name = "LblTxtOtros"
        LblTxtOtros.Size = New Size(86, 15)
        LblTxtOtros.TabIndex = 14
        LblTxtOtros.Text = "Otros importes"
        ' 
        ' TxtOtros
        ' 
        TxtOtros.Dock = DockStyle.Fill
        TxtOtros.Location = New Point(143, 129)
        TxtOtros.MaxLength = 30
        TxtOtros.Name = "TxtOtros"
        TxtOtros.Size = New Size(242, 23)
        TxtOtros.TabIndex = 15
        TxtOtros.TextAlign = HorizontalAlignment.Right
        ' 
        ' LblTxtTotal
        ' 
        LblTxtTotal.AutoSize = True
        LblTxtTotal.Location = New Point(391, 133)
        LblTxtTotal.Margin = New Padding(3, 7, 3, 3)
        LblTxtTotal.Name = "LblTxtTotal"
        LblTxtTotal.Size = New Size(81, 15)
        LblTxtTotal.TabIndex = 16
        LblTxtTotal.Text = "Total cotizado"
        ' 
        ' TxtTotal
        ' 
        TxtTotal.Dock = DockStyle.Fill
        TxtTotal.Location = New Point(501, 129)
        TxtTotal.MaxLength = 30
        TxtTotal.Name = "TxtTotal"
        TxtTotal.Size = New Size(243, 23)
        TxtTotal.TabIndex = 17
        TxtTotal.TextAlign = HorizontalAlignment.Right
        ' 
        ' LblTxtPago
        ' 
        LblTxtPago.AutoSize = True
        LblTxtPago.Location = New Point(3, 163)
        LblTxtPago.Margin = New Padding(3, 7, 3, 3)
        LblTxtPago.Name = "LblTxtPago"
        LblTxtPago.Size = New Size(108, 15)
        LblTxtPago.TabIndex = 18
        LblTxtPago.Text = "Condición de pago"
        ' 
        ' TxtPago
        ' 
        PanelDatos.SetColumnSpan(TxtPago, 3)
        TxtPago.Dock = DockStyle.Fill
        TxtPago.Location = New Point(143, 159)
        TxtPago.MaxLength = 200
        TxtPago.Name = "TxtPago"
        TxtPago.Size = New Size(601, 23)
        TxtPago.TabIndex = 19
        ' 
        ' LblTxtEntrega
        ' 
        LblTxtEntrega.AutoSize = True
        LblTxtEntrega.Location = New Point(3, 193)
        LblTxtEntrega.Margin = New Padding(3, 7, 3, 3)
        LblTxtEntrega.Name = "LblTxtEntrega"
        LblTxtEntrega.Size = New Size(94, 15)
        LblTxtEntrega.TabIndex = 20
        LblTxtEntrega.Text = "Plazo de entrega"
        ' 
        ' TxtEntrega
        ' 
        PanelDatos.SetColumnSpan(TxtEntrega, 3)
        TxtEntrega.Dock = DockStyle.Fill
        TxtEntrega.Location = New Point(143, 189)
        TxtEntrega.MaxLength = 200
        TxtEntrega.Name = "TxtEntrega"
        TxtEntrega.Size = New Size(601, 23)
        TxtEntrega.TabIndex = 21
        ' 
        ' LblTxtObservaciones
        ' 
        LblTxtObservaciones.AutoSize = True
        LblTxtObservaciones.Location = New Point(3, 223)
        LblTxtObservaciones.Margin = New Padding(3, 7, 3, 3)
        LblTxtObservaciones.Name = "LblTxtObservaciones"
        LblTxtObservaciones.Size = New Size(84, 15)
        LblTxtObservaciones.TabIndex = 22
        LblTxtObservaciones.Text = "Observaciones"
        ' 
        ' TxtObservaciones
        ' 
        PanelDatos.SetColumnSpan(TxtObservaciones, 3)
        TxtObservaciones.Dock = DockStyle.Fill
        TxtObservaciones.Location = New Point(143, 219)
        TxtObservaciones.MaxLength = 1000
        TxtObservaciones.Multiline = True
        TxtObservaciones.Name = "TxtObservaciones"
        TxtObservaciones.ScrollBars = ScrollBars.Vertical
        TxtObservaciones.Size = New Size(601, 54)
        TxtObservaciones.TabIndex = 23
        ' 
        ' LblImportes
        ' 
        LblImportes.AutoSize = True
        PanelDatos.SetColumnSpan(LblImportes, 4)
        LblImportes.Location = New Point(3, 280)
        LblImportes.Margin = New Padding(3, 4, 3, 3)
        LblImportes.Name = "LblImportes"
        LblImportes.Size = New Size(482, 15)
        LblImportes.TabIndex = 24
        LblImportes.Text = "Importes opcionales. Cargue los valores de la cotización; no se calculan automáticamente."
        ' 
        ' GrupoPdf
        ' 
        GrupoPdf.Controls.Add(PanelPdf)
        GrupoPdf.Dock = DockStyle.Fill
        GrupoPdf.Location = New Point(13, 337)
        GrupoPdf.Name = "GrupoPdf"
        GrupoPdf.Padding = New Padding(8)
        GrupoPdf.Size = New Size(747, 313)
        GrupoPdf.TabIndex = 1
        GrupoPdf.TabStop = False
        GrupoPdf.Text = "Archivos PDF de esta cotización"
        ' 
        ' PanelPdf
        ' 
        PanelPdf.ColumnCount = 1
        PanelPdf.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 20F))
        PanelPdf.Controls.Add(LstArchivos, 0, 0)
        PanelPdf.Controls.Add(BarraPdf, 0, 1)
        PanelPdf.Dock = DockStyle.Fill
        PanelPdf.Location = New Point(8, 24)
        PanelPdf.Name = "PanelPdf"
        PanelPdf.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        PanelPdf.RowStyles.Add(New RowStyle())
        PanelPdf.Size = New Size(731, 281)
        PanelPdf.TabIndex = 0
        ' 
        ' LstArchivos
        ' 
        LstArchivos.BackColor = Color.White
        LstArchivos.Dock = DockStyle.Fill
        LstArchivos.Font = New Font("Segoe UI", 8F)
        LstArchivos.ForeColor = Color.Black
        LstArchivos.IntegralHeight = False
        LstArchivos.ItemHeight = 13
        LstArchivos.Location = New Point(3, 3)
        LstArchivos.Name = "LstArchivos"
        LstArchivos.Size = New Size(725, 233)
        LstArchivos.TabIndex = 0
        ' 
        ' BarraPdf
        ' 
        BarraPdf.AutoSize = True
        BarraPdf.Controls.Add(BtnAdjuntar)
        BarraPdf.Controls.Add(BtnQuitar)
        BarraPdf.Controls.Add(BtnAbrirPdf)
        BarraPdf.Controls.Add(BtnExportar)
        BarraPdf.Dock = DockStyle.Fill
        BarraPdf.Location = New Point(3, 242)
        BarraPdf.Name = "BarraPdf"
        BarraPdf.Size = New Size(725, 36)
        BarraPdf.TabIndex = 1
        BarraPdf.WrapContents = False
        ' 
        ' BtnAdjuntar
        ' 
        BtnAdjuntar.Cursor = Cursors.Hand
        BtnAdjuntar.FlatStyle = FlatStyle.Flat
        BtnAdjuntar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BtnAdjuntar.Location = New Point(3, 3)
        BtnAdjuntar.Name = "BtnAdjuntar"
        BtnAdjuntar.Size = New Size(110, 30)
        BtnAdjuntar.TabIndex = 0
        BtnAdjuntar.Text = "Adjuntar PDF"
        ' 
        ' BtnQuitar
        ' 
        BtnQuitar.Cursor = Cursors.Hand
        BtnQuitar.FlatStyle = FlatStyle.Flat
        BtnQuitar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BtnQuitar.Location = New Point(119, 3)
        BtnQuitar.Name = "BtnQuitar"
        BtnQuitar.Size = New Size(100, 30)
        BtnQuitar.TabIndex = 1
        BtnQuitar.Text = "Quitar PDF"
        ' 
        ' BtnAbrirPdf
        ' 
        BtnAbrirPdf.Cursor = Cursors.Hand
        BtnAbrirPdf.FlatStyle = FlatStyle.Flat
        BtnAbrirPdf.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BtnAbrirPdf.Location = New Point(225, 3)
        BtnAbrirPdf.Name = "BtnAbrirPdf"
        BtnAbrirPdf.Size = New Size(100, 30)
        BtnAbrirPdf.TabIndex = 2
        BtnAbrirPdf.Text = "Abrir PDF"
        ' 
        ' BtnExportar
        ' 
        BtnExportar.Cursor = Cursors.Hand
        BtnExportar.FlatStyle = FlatStyle.Flat
        BtnExportar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BtnExportar.Location = New Point(331, 3)
        BtnExportar.Name = "BtnExportar"
        BtnExportar.Size = New Size(120, 30)
        BtnExportar.TabIndex = 3
        BtnExportar.Text = "Guardar copia"
        ' 
        ' BarraAcciones
        ' 
        BarraAcciones.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        BarraAcciones.AutoSize = True
        BarraAcciones.AutoSizeMode = AutoSizeMode.GrowAndShrink
        BarraAcciones.Controls.Add(BtnCerrar)
        BarraAcciones.Controls.Add(BtnAceptar)
        BarraAcciones.FlowDirection = FlowDirection.RightToLeft
        BarraAcciones.Location = New Point(504, 656)
        BarraAcciones.Name = "BarraAcciones"
        BarraAcciones.Size = New Size(256, 36)
        BarraAcciones.TabIndex = 2
        BarraAcciones.WrapContents = False
        ' 
        ' BtnCerrar
        ' 
        BtnCerrar.BackColor = Color.IndianRed
        BtnCerrar.Cursor = Cursors.Hand
        BtnCerrar.DialogResult = DialogResult.Cancel
        BtnCerrar.FlatStyle = FlatStyle.Flat
        BtnCerrar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BtnCerrar.ForeColor = Color.White
        BtnCerrar.Location = New Point(165, 3)
        BtnCerrar.Name = "BtnCerrar"
        BtnCerrar.Size = New Size(88, 30)
        BtnCerrar.TabIndex = 0
        BtnCerrar.Text = "Cancelar"
        BtnCerrar.UseVisualStyleBackColor = False
        ' 
        ' BtnAceptar
        ' 
        BtnAceptar.Cursor = Cursors.Hand
        BtnAceptar.FlatStyle = FlatStyle.Flat
        BtnAceptar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BtnAceptar.Location = New Point(3, 3)
        BtnAceptar.Name = "BtnAceptar"
        BtnAceptar.Size = New Size(156, 30)
        BtnAceptar.TabIndex = 1
        BtnAceptar.Text = "Aceptar cotización"
        ' 
        ' frmCotizacionCompraUso
        ' 
        AcceptButton = BtnAceptar
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        CancelButton = BtnCerrar
        ClientSize = New Size(773, 705)
        Controls.Add(PanelPrincipal)
        Font = New Font("Segoe UI", 9F)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(789, 744)
        Name = "frmCotizacionCompraUso"
        StartPosition = FormStartPosition.CenterParent
        Text = "Cotización de compra"
        PanelPrincipal.ResumeLayout(False)
        PanelPrincipal.PerformLayout()
        PanelDatos.ResumeLayout(False)
        PanelDatos.PerformLayout()
        PanelProveedor.ResumeLayout(False)
        PanelProveedor.PerformLayout()
        GrupoPdf.ResumeLayout(False)
        PanelPdf.ResumeLayout(False)
        PanelPdf.PerformLayout()
        BarraPdf.ResumeLayout(False)
        BarraAcciones.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents PanelPrincipal As TableLayoutPanel
    Friend WithEvents PanelDatos As TableLayoutPanel
    Friend WithEvents LblProveedor As Label
    Friend WithEvents PanelProveedor As TableLayoutPanel
    Friend WithEvents TxtProveedor As TextBox
    Friend WithEvents BtnBuscarProveedor As Button
    Friend WithEvents LblTxtNumero As Label
    Friend WithEvents TxtNumero As TextBox
    Friend WithEvents LblDtpFecha As Label
    Friend WithEvents DtpFecha As DateTimePicker
    Friend WithEvents LblCmbMoneda As Label
    Friend WithEvents CmbMoneda As ComboBox
    Friend WithEvents LblDtpVencimiento As Label
    Friend WithEvents DtpVencimiento As DateTimePicker
    Friend WithEvents LblTxtNeto As Label
    Friend WithEvents TxtNeto As TextBox
    Friend WithEvents LblTxtIVA As Label
    Friend WithEvents TxtIVA As TextBox
    Friend WithEvents LblTxtOtros As Label
    Friend WithEvents TxtOtros As TextBox
    Friend WithEvents LblTxtTotal As Label
    Friend WithEvents TxtTotal As TextBox
    Friend WithEvents LblTxtPago As Label
    Friend WithEvents TxtPago As TextBox
    Friend WithEvents LblTxtEntrega As Label
    Friend WithEvents TxtEntrega As TextBox
    Friend WithEvents LblTxtObservaciones As Label
    Friend WithEvents TxtObservaciones As TextBox
    Friend WithEvents LblImportes As Label
    Friend WithEvents GrupoPdf As GroupBox
    Friend WithEvents PanelPdf As TableLayoutPanel
    Friend WithEvents LstArchivos As ListBox
    Friend WithEvents BarraPdf As FlowLayoutPanel
    Friend WithEvents BtnAdjuntar As Button
    Friend WithEvents BtnQuitar As Button
    Friend WithEvents BtnAbrirPdf As Button
    Friend WithEvents BtnExportar As Button
    Friend WithEvents BarraAcciones As FlowLayoutPanel
    Friend WithEvents BtnCerrar As Button
    Friend WithEvents BtnAceptar As Button
End Class

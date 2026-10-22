<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSolicitudCompraUso
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
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Raiz = New TableLayoutPanel()
        Cabecera = New TableLayoutPanel()
        LblTxtNumero = New Label()
        TxtNumero = New TextBox()
        LblTxtEstado = New Label()
        TxtEstado = New TextBox()
        LblCmbSector = New Label()
        CmbSector = New ComboBox()
        LblTxtSolicitante = New Label()
        TxtSolicitante = New TextBox()
        LblDtpFecha = New Label()
        DtpFecha = New DateTimePicker()
        LblDtpNecesidad = New Label()
        DtpNecesidad = New DateTimePicker()
        LblCmbPrioridad = New Label()
        CmbPrioridad = New ComboBox()
        LblTxtResolucion = New Label()
        TxtResolucion = New TextBox()
        LblTxtMotivo = New Label()
        TxtMotivo = New TextBox()
        LblTxtObservaciones = New Label()
        TxtObservaciones = New TextBox()
        Pestanas = New TabControl()
        TabDetalle = New TabPage()
        GridDetalle = New DataGridView()
        BarraDetalle = New FlowLayoutPanel()
        BtnAgregarItem = New Button()
        BtnQuitarItem = New Button()
        TabCotizaciones = New TabPage()
        GridCotizaciones = New DataGridView()
        BarraCotizaciones = New FlowLayoutPanel()
        BtnAgregarCot = New Button()
        BtnEditarCot = New Button()
        BtnQuitarCot = New Button()
        BarraAcciones = New FlowLayoutPanel()
        BtnNueva = New Button()
        BtnAbrir = New Button()
        BtnGuardar = New Button()
        BtnEnviar = New Button()
        BtnCerrar = New Button()
        Raiz.SuspendLayout()
        Cabecera.SuspendLayout()
        Pestanas.SuspendLayout()
        TabDetalle.SuspendLayout()
        CType(GridDetalle, ComponentModel.ISupportInitialize).BeginInit()
        BarraDetalle.SuspendLayout()
        TabCotizaciones.SuspendLayout()
        CType(GridCotizaciones, ComponentModel.ISupportInitialize).BeginInit()
        BarraCotizaciones.SuspendLayout()
        BarraAcciones.SuspendLayout()
        SuspendLayout()
        ' 
        ' Raiz
        ' 
        Raiz.ColumnCount = 1
        Raiz.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 20F))
        Raiz.Controls.Add(Cabecera, 0, 0)
        Raiz.Controls.Add(Pestanas, 0, 1)
        Raiz.Controls.Add(BarraAcciones, 0, 2)
        Raiz.Location = New Point(0, 0)
        Raiz.Name = "Raiz"
        Raiz.Padding = New Padding(10)
        Raiz.RowCount = 3
        Raiz.RowStyles.Add(New RowStyle(SizeType.Absolute, 220F))
        Raiz.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        Raiz.RowStyles.Add(New RowStyle(SizeType.Absolute, 42F))
        Raiz.Size = New Size(761, 705)
        Raiz.TabIndex = 0
        ' 
        ' Cabecera
        ' 
        Cabecera.ColumnCount = 4
        Cabecera.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 105F))
        Cabecera.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        Cabecera.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 172F))
        Cabecera.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        Cabecera.Controls.Add(LblTxtNumero, 0, 0)
        Cabecera.Controls.Add(TxtNumero, 1, 0)
        Cabecera.Controls.Add(LblTxtEstado, 2, 0)
        Cabecera.Controls.Add(TxtEstado, 3, 0)
        Cabecera.Controls.Add(LblCmbSector, 0, 1)
        Cabecera.Controls.Add(CmbSector, 1, 1)
        Cabecera.Controls.Add(LblTxtSolicitante, 2, 1)
        Cabecera.Controls.Add(TxtSolicitante, 3, 1)
        Cabecera.Controls.Add(LblDtpFecha, 0, 2)
        Cabecera.Controls.Add(DtpFecha, 1, 2)
        Cabecera.Controls.Add(LblDtpNecesidad, 2, 2)
        Cabecera.Controls.Add(DtpNecesidad, 3, 2)
        Cabecera.Controls.Add(LblCmbPrioridad, 0, 3)
        Cabecera.Controls.Add(CmbPrioridad, 1, 3)
        Cabecera.Controls.Add(LblTxtResolucion, 2, 3)
        Cabecera.Controls.Add(TxtResolucion, 3, 3)
        Cabecera.Controls.Add(LblTxtMotivo, 0, 4)
        Cabecera.Controls.Add(TxtMotivo, 1, 4)
        Cabecera.Controls.Add(LblTxtObservaciones, 0, 5)
        Cabecera.Controls.Add(TxtObservaciones, 1, 5)
        Cabecera.Location = New Point(13, 13)
        Cabecera.Name = "Cabecera"
        Cabecera.RowCount = 6
        Cabecera.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        Cabecera.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        Cabecera.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        Cabecera.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        Cabecera.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        Cabecera.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        Cabecera.Size = New Size(735, 214)
        Cabecera.TabIndex = 0
        ' 
        ' LblTxtNumero
        ' 
        LblTxtNumero.AutoSize = True
        LblTxtNumero.Location = New Point(3, 7)
        LblTxtNumero.Margin = New Padding(3, 7, 3, 3)
        LblTxtNumero.Name = "LblTxtNumero"
        LblTxtNumero.Size = New Size(51, 15)
        LblTxtNumero.TabIndex = 0
        LblTxtNumero.Text = "Número"
        ' 
        ' TxtNumero
        ' 
        TxtNumero.BackColor = Color.White
        TxtNumero.Location = New Point(108, 3)
        TxtNumero.Name = "TxtNumero"
        TxtNumero.ReadOnly = True
        TxtNumero.Size = New Size(201, 23)
        TxtNumero.TabIndex = 1
        TxtNumero.TabStop = False
        ' 
        ' LblTxtEstado
        ' 
        LblTxtEstado.AutoSize = True
        LblTxtEstado.Location = New Point(337, 7)
        LblTxtEstado.Margin = New Padding(3, 7, 3, 3)
        LblTxtEstado.Name = "LblTxtEstado"
        LblTxtEstado.Size = New Size(42, 15)
        LblTxtEstado.TabIndex = 2
        LblTxtEstado.Text = "Estado"
        ' 
        ' TxtEstado
        ' 
        TxtEstado.BackColor = Color.White
        TxtEstado.Location = New Point(509, 3)
        TxtEstado.Name = "TxtEstado"
        TxtEstado.ReadOnly = True
        TxtEstado.Size = New Size(218, 23)
        TxtEstado.TabIndex = 3
        TxtEstado.TabStop = False
        ' 
        ' LblCmbSector
        ' 
        LblCmbSector.AutoSize = True
        LblCmbSector.Location = New Point(3, 37)
        LblCmbSector.Margin = New Padding(3, 7, 3, 3)
        LblCmbSector.Name = "LblCmbSector"
        LblCmbSector.Size = New Size(40, 15)
        LblCmbSector.TabIndex = 4
        LblCmbSector.Text = "Sector"
        ' 
        ' CmbSector
        ' 
        CmbSector.BackColor = Color.White
        CmbSector.DropDownStyle = ComboBoxStyle.DropDownList
        CmbSector.Location = New Point(108, 33)
        CmbSector.Name = "CmbSector"
        CmbSector.Size = New Size(201, 23)
        CmbSector.TabIndex = 5
        ' 
        ' LblTxtSolicitante
        ' 
        LblTxtSolicitante.AutoSize = True
        LblTxtSolicitante.Location = New Point(337, 37)
        LblTxtSolicitante.Margin = New Padding(3, 7, 3, 3)
        LblTxtSolicitante.Name = "LblTxtSolicitante"
        LblTxtSolicitante.Size = New Size(62, 15)
        LblTxtSolicitante.TabIndex = 6
        LblTxtSolicitante.Text = "Solicitante"
        ' 
        ' TxtSolicitante
        ' 
        TxtSolicitante.BackColor = Color.White
        TxtSolicitante.Location = New Point(509, 33)
        TxtSolicitante.Name = "TxtSolicitante"
        TxtSolicitante.ReadOnly = True
        TxtSolicitante.Size = New Size(218, 23)
        TxtSolicitante.TabIndex = 7
        TxtSolicitante.TabStop = False
        ' 
        ' LblDtpFecha
        ' 
        LblDtpFecha.AutoSize = True
        LblDtpFecha.Location = New Point(3, 67)
        LblDtpFecha.Margin = New Padding(3, 7, 3, 3)
        LblDtpFecha.Name = "LblDtpFecha"
        LblDtpFecha.Size = New Size(38, 15)
        LblDtpFecha.TabIndex = 8
        LblDtpFecha.Text = "Fecha"
        ' 
        ' DtpFecha
        ' 
        DtpFecha.Format = DateTimePickerFormat.Short
        DtpFecha.Location = New Point(108, 63)
        DtpFecha.Name = "DtpFecha"
        DtpFecha.Size = New Size(118, 23)
        DtpFecha.TabIndex = 9
        ' 
        ' LblDtpNecesidad
        ' 
        LblDtpNecesidad.AutoSize = True
        LblDtpNecesidad.Location = New Point(337, 67)
        LblDtpNecesidad.Margin = New Padding(3, 7, 3, 3)
        LblDtpNecesidad.Name = "LblDtpNecesidad"
        LblDtpNecesidad.Size = New Size(110, 15)
        LblDtpNecesidad.TabIndex = 10
        LblDtpNecesidad.Text = "Fecha de necesidad"
        ' 
        ' DtpNecesidad
        ' 
        DtpNecesidad.Checked = False
        DtpNecesidad.Format = DateTimePickerFormat.Short
        DtpNecesidad.Location = New Point(509, 63)
        DtpNecesidad.Name = "DtpNecesidad"
        DtpNecesidad.ShowCheckBox = True
        DtpNecesidad.Size = New Size(218, 23)
        DtpNecesidad.TabIndex = 11
        ' 
        ' LblCmbPrioridad
        ' 
        LblCmbPrioridad.AutoSize = True
        LblCmbPrioridad.Location = New Point(3, 97)
        LblCmbPrioridad.Margin = New Padding(3, 7, 3, 3)
        LblCmbPrioridad.Name = "LblCmbPrioridad"
        LblCmbPrioridad.Size = New Size(55, 15)
        LblCmbPrioridad.TabIndex = 12
        LblCmbPrioridad.Text = "Prioridad"
        ' 
        ' CmbPrioridad
        ' 
        CmbPrioridad.BackColor = Color.White
        CmbPrioridad.DropDownStyle = ComboBoxStyle.DropDownList
        CmbPrioridad.Items.AddRange(New Object() {"Normal", "Urgente"})
        CmbPrioridad.Location = New Point(108, 93)
        CmbPrioridad.Name = "CmbPrioridad"
        CmbPrioridad.Size = New Size(201, 23)
        CmbPrioridad.TabIndex = 13
        ' 
        ' LblTxtResolucion
        ' 
        LblTxtResolucion.AutoSize = True
        LblTxtResolucion.Location = New Point(337, 97)
        LblTxtResolucion.Margin = New Padding(3, 7, 3, 3)
        LblTxtResolucion.Name = "LblTxtResolucion"
        LblTxtResolucion.Size = New Size(100, 15)
        LblTxtResolucion.TabIndex = 14
        LblTxtResolucion.Text = "Última resolución"
        ' 
        ' TxtResolucion
        ' 
        TxtResolucion.BackColor = Color.White
        TxtResolucion.Location = New Point(509, 93)
        TxtResolucion.Name = "TxtResolucion"
        TxtResolucion.ReadOnly = True
        TxtResolucion.Size = New Size(218, 23)
        TxtResolucion.TabIndex = 15
        TxtResolucion.TabStop = False
        ' 
        ' LblTxtMotivo
        ' 
        LblTxtMotivo.AutoSize = True
        LblTxtMotivo.Location = New Point(3, 127)
        LblTxtMotivo.Margin = New Padding(3, 7, 3, 3)
        LblTxtMotivo.Name = "LblTxtMotivo"
        LblTxtMotivo.Size = New Size(45, 15)
        LblTxtMotivo.TabIndex = 16
        LblTxtMotivo.Text = "Motivo"
        ' 
        ' TxtMotivo
        ' 
        Cabecera.SetColumnSpan(TxtMotivo, 3)
        TxtMotivo.Location = New Point(108, 123)
        TxtMotivo.MaxLength = 500
        TxtMotivo.Multiline = True
        TxtMotivo.Name = "TxtMotivo"
        TxtMotivo.ScrollBars = ScrollBars.Vertical
        TxtMotivo.Size = New Size(619, 41)
        TxtMotivo.TabIndex = 17
        ' 
        ' LblTxtObservaciones
        ' 
        LblTxtObservaciones.AutoSize = True
        LblTxtObservaciones.Location = New Point(3, 174)
        LblTxtObservaciones.Margin = New Padding(3, 7, 3, 3)
        LblTxtObservaciones.Name = "LblTxtObservaciones"
        LblTxtObservaciones.Size = New Size(84, 15)
        LblTxtObservaciones.TabIndex = 18
        LblTxtObservaciones.Text = "Observaciones"
        ' 
        ' TxtObservaciones
        ' 
        Cabecera.SetColumnSpan(TxtObservaciones, 3)
        TxtObservaciones.Location = New Point(108, 170)
        TxtObservaciones.MaxLength = 1000
        TxtObservaciones.Multiline = True
        TxtObservaciones.Name = "TxtObservaciones"
        TxtObservaciones.ScrollBars = ScrollBars.Vertical
        TxtObservaciones.Size = New Size(619, 41)
        TxtObservaciones.TabIndex = 19
        ' 
        ' Pestanas
        ' 
        Pestanas.Controls.Add(TabDetalle)
        Pestanas.Controls.Add(TabCotizaciones)
        Pestanas.Location = New Point(13, 233)
        Pestanas.Name = "Pestanas"
        Pestanas.SelectedIndex = 0
        Pestanas.Size = New Size(735, 417)
        Pestanas.TabIndex = 1
        ' 
        ' TabDetalle
        ' 
        TabDetalle.Controls.Add(GridDetalle)
        TabDetalle.Controls.Add(BarraDetalle)
        TabDetalle.Location = New Point(4, 24)
        TabDetalle.Name = "TabDetalle"
        TabDetalle.Padding = New Padding(3)
        TabDetalle.Size = New Size(727, 389)
        TabDetalle.TabIndex = 0
        TabDetalle.Text = "Artículos y servicios"
        ' 
        ' GridDetalle
        ' 
        GridDetalle.AllowUserToAddRows = False
        GridDetalle.AllowUserToDeleteRows = False
        GridDetalle.AllowUserToResizeColumns = False
        GridDetalle.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = Color.FloralWhite
        GridDetalle.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = SystemColors.Control
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.True
        GridDetalle.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = Color.White
        DataGridViewCellStyle3.Font = New Font("Segoe UI", 8F)
        DataGridViewCellStyle3.ForeColor = Color.Black
        DataGridViewCellStyle3.SelectionBackColor = Color.SteelBlue
        DataGridViewCellStyle3.SelectionForeColor = Color.White
        DataGridViewCellStyle3.WrapMode = DataGridViewTriState.False
        GridDetalle.DefaultCellStyle = DataGridViewCellStyle3
        GridDetalle.EnableHeadersVisualStyles = False
        GridDetalle.Location = New Point(3, 3)
        GridDetalle.MultiSelect = False
        GridDetalle.Name = "GridDetalle"
        GridDetalle.RowHeadersWidth = 20
        GridDetalle.SelectionMode = DataGridViewSelectionMode.CellSelect
        GridDetalle.Size = New Size(721, 329)
        GridDetalle.TabIndex = 0
        ' 
        ' BarraDetalle
        ' 
        BarraDetalle.AutoSize = True
        BarraDetalle.Controls.Add(BtnAgregarItem)
        BarraDetalle.Controls.Add(BtnQuitarItem)
        BarraDetalle.Location = New Point(3, 335)
        BarraDetalle.Name = "BarraDetalle"
        BarraDetalle.Size = New Size(720, 36)
        BarraDetalle.TabIndex = 1
        ' 
        ' BtnAgregarItem
        ' 
        BtnAgregarItem.AutoSize = True
        BtnAgregarItem.Cursor = Cursors.Hand
        BtnAgregarItem.FlatStyle = FlatStyle.Flat
        BtnAgregarItem.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BtnAgregarItem.Location = New Point(3, 3)
        BtnAgregarItem.Name = "BtnAgregarItem"
        BtnAgregarItem.Size = New Size(110, 30)
        BtnAgregarItem.TabIndex = 0
        BtnAgregarItem.Text = "Agregar renglón"
        ' 
        ' BtnQuitarItem
        ' 
        BtnQuitarItem.AutoSize = True
        BtnQuitarItem.Cursor = Cursors.Hand
        BtnQuitarItem.FlatStyle = FlatStyle.Flat
        BtnQuitarItem.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BtnQuitarItem.Location = New Point(119, 3)
        BtnQuitarItem.Name = "BtnQuitarItem"
        BtnQuitarItem.Size = New Size(100, 30)
        BtnQuitarItem.TabIndex = 1
        BtnQuitarItem.Text = "Quitar renglón"
        ' 
        ' TabCotizaciones
        ' 
        TabCotizaciones.Controls.Add(GridCotizaciones)
        TabCotizaciones.Controls.Add(BarraCotizaciones)
        TabCotizaciones.Location = New Point(4, 24)
        TabCotizaciones.Name = "TabCotizaciones"
        TabCotizaciones.Padding = New Padding(3)
        TabCotizaciones.Size = New Size(727, 389)
        TabCotizaciones.TabIndex = 1
        TabCotizaciones.Text = "Cotizaciones y PDF"
        ' 
        ' GridCotizaciones
        ' 
        GridCotizaciones.AllowUserToAddRows = False
        GridCotizaciones.AllowUserToDeleteRows = False
        GridCotizaciones.AllowUserToResizeColumns = False
        GridCotizaciones.AllowUserToResizeRows = False
        DataGridViewCellStyle4.BackColor = Color.FloralWhite
        GridCotizaciones.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = SystemColors.Control
        DataGridViewCellStyle5.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        DataGridViewCellStyle5.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = DataGridViewTriState.True
        GridCotizaciones.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = Color.White
        DataGridViewCellStyle6.Font = New Font("Segoe UI", 8F)
        DataGridViewCellStyle6.ForeColor = Color.Black
        DataGridViewCellStyle6.SelectionBackColor = Color.SteelBlue
        DataGridViewCellStyle6.SelectionForeColor = Color.White
        DataGridViewCellStyle6.WrapMode = DataGridViewTriState.False
        GridCotizaciones.DefaultCellStyle = DataGridViewCellStyle6
        GridCotizaciones.EnableHeadersVisualStyles = False
        GridCotizaciones.Location = New Point(3, 3)
        GridCotizaciones.MultiSelect = False
        GridCotizaciones.Name = "GridCotizaciones"
        GridCotizaciones.ReadOnly = True
        GridCotizaciones.RowHeadersWidth = 20
        GridCotizaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        GridCotizaciones.Size = New Size(721, 329)
        GridCotizaciones.TabIndex = 0
        ' 
        ' BarraCotizaciones
        ' 
        BarraCotizaciones.AutoSize = True
        BarraCotizaciones.Controls.Add(BtnAgregarCot)
        BarraCotizaciones.Controls.Add(BtnEditarCot)
        BarraCotizaciones.Controls.Add(BtnQuitarCot)
        BarraCotizaciones.Location = New Point(3, 335)
        BarraCotizaciones.Name = "BarraCotizaciones"
        BarraCotizaciones.Size = New Size(717, 36)
        BarraCotizaciones.TabIndex = 1
        ' 
        ' BtnAgregarCot
        ' 
        BtnAgregarCot.AutoSize = True
        BtnAgregarCot.Cursor = Cursors.Hand
        BtnAgregarCot.FlatStyle = FlatStyle.Flat
        BtnAgregarCot.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BtnAgregarCot.Location = New Point(3, 3)
        BtnAgregarCot.Name = "BtnAgregarCot"
        BtnAgregarCot.Size = New Size(123, 30)
        BtnAgregarCot.TabIndex = 0
        BtnAgregarCot.Text = "Agregar cotización"
        ' 
        ' BtnEditarCot
        ' 
        BtnEditarCot.AutoSize = True
        BtnEditarCot.Cursor = Cursors.Hand
        BtnEditarCot.FlatStyle = FlatStyle.Flat
        BtnEditarCot.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BtnEditarCot.Location = New Point(132, 3)
        BtnEditarCot.Name = "BtnEditarCot"
        BtnEditarCot.Size = New Size(141, 30)
        BtnEditarCot.TabIndex = 1
        BtnEditarCot.Text = "Ver / editar cotización"
        ' 
        ' BtnQuitarCot
        ' 
        BtnQuitarCot.AutoSize = True
        BtnQuitarCot.Cursor = Cursors.Hand
        BtnQuitarCot.FlatStyle = FlatStyle.Flat
        BtnQuitarCot.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BtnQuitarCot.Location = New Point(279, 3)
        BtnQuitarCot.Name = "BtnQuitarCot"
        BtnQuitarCot.Size = New Size(113, 30)
        BtnQuitarCot.TabIndex = 2
        BtnQuitarCot.Text = "Quitar cotización"
        ' 
        ' BarraAcciones
        ' 
        BarraAcciones.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        BarraAcciones.AutoSize = True
        BarraAcciones.AutoSizeMode = AutoSizeMode.GrowAndShrink
        BarraAcciones.Controls.Add(BtnNueva)
        BarraAcciones.Controls.Add(BtnAbrir)
        BarraAcciones.Controls.Add(BtnGuardar)
        BarraAcciones.Controls.Add(BtnEnviar)
        BarraAcciones.Controls.Add(BtnCerrar)
        BarraAcciones.Location = New Point(78, 656)
        BarraAcciones.Name = "BarraAcciones"
        BarraAcciones.Size = New Size(670, 36)
        BarraAcciones.TabIndex = 2
        BarraAcciones.WrapContents = False
        ' 
        ' BtnNueva
        ' 
        BtnNueva.Cursor = Cursors.Hand
        BtnNueva.FlatStyle = FlatStyle.Flat
        BtnNueva.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BtnNueva.Location = New Point(3, 3)
        BtnNueva.Name = "BtnNueva"
        BtnNueva.Size = New Size(132, 30)
        BtnNueva.TabIndex = 0
        BtnNueva.Text = "Nueva solicitud"
        ' 
        ' BtnAbrir
        ' 
        BtnAbrir.Cursor = Cursors.Hand
        BtnAbrir.FlatStyle = FlatStyle.Flat
        BtnAbrir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BtnAbrir.Location = New Point(141, 3)
        BtnAbrir.Name = "BtnAbrir"
        BtnAbrir.Size = New Size(132, 30)
        BtnAbrir.TabIndex = 1
        BtnAbrir.Text = "Abrir solicitud"
        ' 
        ' BtnGuardar
        ' 
        BtnGuardar.Cursor = Cursors.Hand
        BtnGuardar.FlatStyle = FlatStyle.Flat
        BtnGuardar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BtnGuardar.Location = New Point(279, 3)
        BtnGuardar.Name = "BtnGuardar"
        BtnGuardar.Size = New Size(132, 30)
        BtnGuardar.TabIndex = 2
        BtnGuardar.Text = "Guardar borrador"
        ' 
        ' BtnEnviar
        ' 
        BtnEnviar.Cursor = Cursors.Hand
        BtnEnviar.FlatStyle = FlatStyle.Flat
        BtnEnviar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BtnEnviar.Location = New Point(417, 3)
        BtnEnviar.Name = "BtnEnviar"
        BtnEnviar.Size = New Size(156, 30)
        BtnEnviar.TabIndex = 3
        BtnEnviar.Text = "Enviar a autorización"
        ' 
        ' BtnCerrar
        ' 
        BtnCerrar.BackColor = Color.IndianRed
        BtnCerrar.Cursor = Cursors.Hand
        BtnCerrar.FlatStyle = FlatStyle.Flat
        BtnCerrar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BtnCerrar.ForeColor = Color.White
        BtnCerrar.Location = New Point(579, 3)
        BtnCerrar.Name = "BtnCerrar"
        BtnCerrar.Size = New Size(88, 30)
        BtnCerrar.TabIndex = 4
        BtnCerrar.Text = "Salir"
        BtnCerrar.UseVisualStyleBackColor = False
        ' 
        ' frmSolicitudCompraUso
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        ClientSize = New Size(773, 705)
        Controls.Add(Raiz)
        Font = New Font("Segoe UI", 9F)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(789, 744)
        Name = "frmSolicitudCompraUso"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Solicitud Compras"
        Raiz.ResumeLayout(False)
        Raiz.PerformLayout()
        Cabecera.ResumeLayout(False)
        Cabecera.PerformLayout()
        Pestanas.ResumeLayout(False)
        TabDetalle.ResumeLayout(False)
        TabDetalle.PerformLayout()
        CType(GridDetalle, ComponentModel.ISupportInitialize).EndInit()
        BarraDetalle.ResumeLayout(False)
        BarraDetalle.PerformLayout()
        TabCotizaciones.ResumeLayout(False)
        TabCotizaciones.PerformLayout()
        CType(GridCotizaciones, ComponentModel.ISupportInitialize).EndInit()
        BarraCotizaciones.ResumeLayout(False)
        BarraCotizaciones.PerformLayout()
        BarraAcciones.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents Raiz As TableLayoutPanel
    Friend WithEvents Cabecera As TableLayoutPanel
    Friend WithEvents LblTxtNumero As Label
    Friend WithEvents TxtNumero As TextBox
    Friend WithEvents LblTxtEstado As Label
    Friend WithEvents TxtEstado As TextBox
    Friend WithEvents LblCmbSector As Label
    Friend WithEvents CmbSector As ComboBox
    Friend WithEvents LblTxtSolicitante As Label
    Friend WithEvents TxtSolicitante As TextBox
    Friend WithEvents LblDtpFecha As Label
    Friend WithEvents DtpFecha As DateTimePicker
    Friend WithEvents LblDtpNecesidad As Label
    Friend WithEvents DtpNecesidad As DateTimePicker
    Friend WithEvents LblCmbPrioridad As Label
    Friend WithEvents CmbPrioridad As ComboBox
    Friend WithEvents LblTxtResolucion As Label
    Friend WithEvents TxtResolucion As TextBox
    Friend WithEvents LblTxtMotivo As Label
    Friend WithEvents TxtMotivo As TextBox
    Friend WithEvents LblTxtObservaciones As Label
    Friend WithEvents TxtObservaciones As TextBox
    Friend WithEvents Pestanas As TabControl
    Friend WithEvents TabDetalle As TabPage
    Friend WithEvents TabCotizaciones As TabPage
    Friend WithEvents GridDetalle As DataGridView
    Friend WithEvents GridDetalleColTipoItem As DataGridViewComboBoxColumn
    Friend WithEvents GridDetalleColDescripcion As DataGridViewTextBoxColumn
    Friend WithEvents GridDetalleColCantidad As DataGridViewTextBoxColumn
    Friend WithEvents GridDetalleColUnidad As DataGridViewTextBoxColumn
    Friend WithEvents GridDetalleColFechaNecesidad As DataGridViewTextBoxColumn
    Friend WithEvents GridDetalleColObservaciones As DataGridViewTextBoxColumn
    Friend WithEvents GridCotizaciones As DataGridView
    Friend WithEvents GridCotizacionesColProveedorNombre As DataGridViewTextBoxColumn
    Friend WithEvents GridCotizacionesColNumeroCotizacion As DataGridViewTextBoxColumn
    Friend WithEvents GridCotizacionesColFechaCotizacion As DataGridViewTextBoxColumn
    Friend WithEvents GridCotizacionesColArchivosPdf As DataGridViewTextBoxColumn
    Friend WithEvents GridCotizacionesColMoneda As DataGridViewTextBoxColumn
    Friend WithEvents GridCotizacionesColImporteTotal As DataGridViewTextBoxColumn
    Friend WithEvents BarraDetalle As FlowLayoutPanel
    Friend WithEvents BtnAgregarItem As Button
    Friend WithEvents BtnQuitarItem As Button
    Friend WithEvents BarraCotizaciones As FlowLayoutPanel
    Friend WithEvents BtnAgregarCot As Button
    Friend WithEvents BtnEditarCot As Button
    Friend WithEvents BtnQuitarCot As Button
    Friend WithEvents BarraAcciones As FlowLayoutPanel
    Friend WithEvents BtnNueva As Button
    Friend WithEvents BtnAbrir As Button
    Friend WithEvents BtnGuardar As Button
    Friend WithEvents BtnEnviar As Button
    Friend WithEvents BtnCerrar As Button
End Class

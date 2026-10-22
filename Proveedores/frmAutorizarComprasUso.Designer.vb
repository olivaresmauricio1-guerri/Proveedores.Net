<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAutorizarComprasUso
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
        Dim DataGridViewCellStyle7 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As DataGridViewCellStyle = New DataGridViewCellStyle()
        LblDesde = New Label()
        DtpDesde = New DateTimePicker()
        LblHasta = New Label()
        DtpHasta = New DateTimePicker()
        LblSector = New Label()
        CmbSector = New ComboBox()
        LblEstado = New Label()
        CmbEstado = New ComboBox()
        LblBuscar = New Label()
        TxtBuscar = New TextBox()
        LblSolicitante = New Label()
        TxtSolicitante = New TextBox()
        BtnBuscar = New Button()
        TxtResumen = New TextBox()
        Pestanas = New TabControl()
        TabDetalle = New TabPage()
        GridDetalle = New DataGridView()
        TabCotizaciones = New TabPage()
        BtnVerCotizacion = New Button()
        GridCotizaciones = New DataGridView()
        GridCotizacionesColCotizacionAutorizada = New DataGridViewTextBoxColumn()
        TabHistorial = New TabPage()
        GridHistorial = New DataGridView()
        LblResultado = New Label()
        BtnCerrar = New Button()
        GridSolicitudes = New DataGridView()
        GrupoResolucion = New GroupBox()
        LblCotizacion = New Label()
        CmbCotizacionAutorizar = New ComboBox()
        LblResolucion = New Label()
        TxtResolucion = New TextBox()
        BtnAutorizar = New Button()
        BtnRechazar = New Button()
        BtnDevolver = New Button()
        Pestanas.SuspendLayout()
        TabDetalle.SuspendLayout()
        CType(GridDetalle, ComponentModel.ISupportInitialize).BeginInit()
        TabCotizaciones.SuspendLayout()
        CType(GridCotizaciones, ComponentModel.ISupportInitialize).BeginInit()
        TabHistorial.SuspendLayout()
        CType(GridHistorial, ComponentModel.ISupportInitialize).BeginInit()
        CType(GridSolicitudes, ComponentModel.ISupportInitialize).BeginInit()
        GrupoResolucion.SuspendLayout()
        SuspendLayout()
        ' 
        ' LblDesde
        ' 
        LblDesde.AutoSize = True
        LblDesde.Location = New Point(10, 17)
        LblDesde.Margin = New Padding(3, 7, 3, 3)
        LblDesde.Name = "LblDesde"
        LblDesde.Size = New Size(39, 15)
        LblDesde.TabIndex = 0
        LblDesde.Text = "Desde"
        ' 
        ' DtpDesde
        ' 
        DtpDesde.Checked = False
        DtpDesde.Format = DateTimePickerFormat.Short
        DtpDesde.Location = New Point(58, 13)
        DtpDesde.Margin = New Padding(3, 3, 12, 3)
        DtpDesde.Name = "DtpDesde"
        DtpDesde.ShowCheckBox = True
        DtpDesde.Size = New Size(125, 23)
        DtpDesde.TabIndex = 1
        ' 
        ' LblHasta
        ' 
        LblHasta.AutoSize = True
        LblHasta.Location = New Point(198, 17)
        LblHasta.Margin = New Padding(3, 7, 3, 3)
        LblHasta.Name = "LblHasta"
        LblHasta.Size = New Size(37, 15)
        LblHasta.TabIndex = 2
        LblHasta.Text = "Hasta"
        ' 
        ' DtpHasta
        ' 
        DtpHasta.Checked = False
        DtpHasta.Format = DateTimePickerFormat.Short
        DtpHasta.Location = New Point(241, 13)
        DtpHasta.Margin = New Padding(3, 3, 12, 3)
        DtpHasta.Name = "DtpHasta"
        DtpHasta.ShowCheckBox = True
        DtpHasta.Size = New Size(125, 23)
        DtpHasta.TabIndex = 3
        ' 
        ' LblSector
        ' 
        LblSector.AutoSize = True
        LblSector.Location = New Point(381, 17)
        LblSector.Margin = New Padding(3, 7, 3, 3)
        LblSector.Name = "LblSector"
        LblSector.Size = New Size(40, 15)
        LblSector.TabIndex = 4
        LblSector.Text = "Sector"
        ' 
        ' CmbSector
        ' 
        CmbSector.BackColor = Color.White
        CmbSector.DropDownStyle = ComboBoxStyle.DropDownList
        CmbSector.Location = New Point(427, 13)
        CmbSector.Margin = New Padding(3, 3, 12, 3)
        CmbSector.Name = "CmbSector"
        CmbSector.Size = New Size(200, 23)
        CmbSector.TabIndex = 5
        ' 
        ' LblEstado
        ' 
        LblEstado.AutoSize = True
        LblEstado.Location = New Point(642, 17)
        LblEstado.Margin = New Padding(3, 7, 3, 3)
        LblEstado.Name = "LblEstado"
        LblEstado.Size = New Size(42, 15)
        LblEstado.TabIndex = 6
        LblEstado.Text = "Estado"
        ' 
        ' CmbEstado
        ' 
        CmbEstado.BackColor = Color.White
        CmbEstado.DropDownStyle = ComboBoxStyle.DropDownList
        CmbEstado.Items.AddRange(New Object() {"TODOS", "BORRADOR", "PENDIENTE", "DEVUELTA", "AUTORIZADA", "RECHAZADA", "ANULADA"})
        CmbEstado.Location = New Point(690, 13)
        CmbEstado.Name = "CmbEstado"
        CmbEstado.Size = New Size(130, 23)
        CmbEstado.TabIndex = 7
        ' 
        ' LblBuscar
        ' 
        LblBuscar.AutoSize = True
        LblBuscar.Location = New Point(10, 51)
        LblBuscar.Margin = New Padding(3, 7, 3, 3)
        LblBuscar.Name = "LblBuscar"
        LblBuscar.Size = New Size(200, 15)
        LblBuscar.TabIndex = 0
        LblBuscar.Text = "Número, motivo, sector o proveedor"
        ' 
        ' TxtBuscar
        ' 
        TxtBuscar.BackColor = Color.White
        TxtBuscar.Location = New Point(219, 47)
        TxtBuscar.Name = "TxtBuscar"
        TxtBuscar.Size = New Size(330, 23)
        TxtBuscar.TabIndex = 1
        ' 
        ' LblSolicitante
        ' 
        LblSolicitante.AutoSize = True
        LblSolicitante.Location = New Point(555, 51)
        LblSolicitante.Margin = New Padding(3, 7, 3, 3)
        LblSolicitante.Name = "LblSolicitante"
        LblSolicitante.Size = New Size(62, 15)
        LblSolicitante.TabIndex = 2
        LblSolicitante.Text = "Solicitante"
        ' 
        ' TxtSolicitante
        ' 
        TxtSolicitante.BackColor = Color.White
        TxtSolicitante.Location = New Point(623, 47)
        TxtSolicitante.MaxLength = 12
        TxtSolicitante.Name = "TxtSolicitante"
        TxtSolicitante.Size = New Size(110, 23)
        TxtSolicitante.TabIndex = 3
        ' 
        ' BtnBuscar
        ' 
        BtnBuscar.Cursor = Cursors.Hand
        BtnBuscar.FlatStyle = FlatStyle.Flat
        BtnBuscar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BtnBuscar.Location = New Point(739, 43)
        BtnBuscar.Name = "BtnBuscar"
        BtnBuscar.Size = New Size(156, 30)
        BtnBuscar.TabIndex = 4
        BtnBuscar.Text = "Buscar / actualizar"
        ' 
        ' TxtResumen
        ' 
        TxtResumen.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TxtResumen.BackColor = Color.White
        TxtResumen.ForeColor = Color.Black
        TxtResumen.Location = New Point(10, 228)
        TxtResumen.Multiline = True
        TxtResumen.Name = "TxtResumen"
        TxtResumen.ReadOnly = True
        TxtResumen.ScrollBars = ScrollBars.Vertical
        TxtResumen.Size = New Size(891, 48)
        TxtResumen.TabIndex = 14
        TxtResumen.TabStop = False
        ' 
        ' Pestanas
        ' 
        Pestanas.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Pestanas.Controls.Add(TabDetalle)
        Pestanas.Controls.Add(TabCotizaciones)
        Pestanas.Controls.Add(TabHistorial)
        Pestanas.Location = New Point(10, 284)
        Pestanas.Name = "Pestanas"
        Pestanas.SelectedIndex = 0
        Pestanas.Size = New Size(891, 146)
        Pestanas.TabIndex = 15
        ' 
        ' TabDetalle
        ' 
        TabDetalle.Controls.Add(GridDetalle)
        TabDetalle.Location = New Point(4, 24)
        TabDetalle.Name = "TabDetalle"
        TabDetalle.Padding = New Padding(3)
        TabDetalle.Size = New Size(883, 118)
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
        GridDetalle.Dock = DockStyle.Fill
        GridDetalle.EnableHeadersVisualStyles = False
        GridDetalle.Location = New Point(3, 3)
        GridDetalle.MultiSelect = False
        GridDetalle.Name = "GridDetalle"
        GridDetalle.ReadOnly = True
        GridDetalle.RowHeadersWidth = 20
        GridDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        GridDetalle.Size = New Size(877, 112)
        GridDetalle.TabIndex = 0
        ' 
        ' TabCotizaciones
        ' 
        TabCotizaciones.Controls.Add(GridCotizaciones)
        TabCotizaciones.Location = New Point(4, 24)
        TabCotizaciones.Name = "TabCotizaciones"
        TabCotizaciones.Padding = New Padding(3)
        TabCotizaciones.Size = New Size(883, 118)
        TabCotizaciones.TabIndex = 1
        TabCotizaciones.Text = "Cotizaciones y PDF"
        ' 
        ' BtnVerCotizacion
        ' 
        BtnVerCotizacion.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        BtnVerCotizacion.Cursor = Cursors.Hand
        BtnVerCotizacion.FlatStyle = FlatStyle.Flat
        BtnVerCotizacion.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BtnVerCotizacion.Location = New Point(12, 432)
        BtnVerCotizacion.Name = "BtnVerCotizacion"
        BtnVerCotizacion.Size = New Size(156, 30)
        BtnVerCotizacion.TabIndex = 2
        BtnVerCotizacion.Text = "Ver cotización / PDF"
        ' 
        ' GridCotizaciones
        ' 
        GridCotizaciones.AllowUserToAddRows = False
        GridCotizaciones.AllowUserToDeleteRows = False
        GridCotizaciones.AllowUserToResizeColumns = False
        GridCotizaciones.AllowUserToResizeRows = False
        DataGridViewCellStyle4.BackColor = Color.FloralWhite
        GridCotizaciones.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        GridCotizaciones.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = SystemColors.Control
        DataGridViewCellStyle5.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        DataGridViewCellStyle5.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = DataGridViewTriState.True
        GridCotizaciones.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        GridCotizaciones.Columns.AddRange(New DataGridViewColumn() {GridCotizacionesColCotizacionAutorizada})
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
        GridCotizaciones.Size = New Size(874, 109)
        GridCotizaciones.TabIndex = 0
        ' 
        ' GridCotizacionesColCotizacionAutorizada
        ' 
        GridCotizacionesColCotizacionAutorizada.HeaderText = "Autorizada"
        GridCotizacionesColCotizacionAutorizada.Name = "GridCotizacionesColCotizacionAutorizada"
        GridCotizacionesColCotizacionAutorizada.ReadOnly = True
        GridCotizacionesColCotizacionAutorizada.SortMode = DataGridViewColumnSortMode.NotSortable
        GridCotizacionesColCotizacionAutorizada.Width = 85
        ' 
        ' TabHistorial
        ' 
        TabHistorial.Controls.Add(GridHistorial)
        TabHistorial.Location = New Point(4, 24)
        TabHistorial.Name = "TabHistorial"
        TabHistorial.Padding = New Padding(3)
        TabHistorial.Size = New Size(883, 118)
        TabHistorial.TabIndex = 2
        TabHistorial.Text = "Historial"
        ' 
        ' GridHistorial
        ' 
        GridHistorial.AllowUserToAddRows = False
        GridHistorial.AllowUserToDeleteRows = False
        GridHistorial.AllowUserToResizeColumns = False
        GridHistorial.AllowUserToResizeRows = False
        DataGridViewCellStyle7.BackColor = Color.FloralWhite
        GridHistorial.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle7
        DataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = SystemColors.Control
        DataGridViewCellStyle8.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        DataGridViewCellStyle8.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = DataGridViewTriState.True
        GridHistorial.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        DataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = Color.White
        DataGridViewCellStyle9.Font = New Font("Segoe UI", 8F)
        DataGridViewCellStyle9.ForeColor = Color.Black
        DataGridViewCellStyle9.SelectionBackColor = Color.SteelBlue
        DataGridViewCellStyle9.SelectionForeColor = Color.White
        DataGridViewCellStyle9.WrapMode = DataGridViewTriState.False
        GridHistorial.DefaultCellStyle = DataGridViewCellStyle9
        GridHistorial.Dock = DockStyle.Fill
        GridHistorial.EnableHeadersVisualStyles = False
        GridHistorial.Location = New Point(3, 3)
        GridHistorial.MultiSelect = False
        GridHistorial.Name = "GridHistorial"
        GridHistorial.ReadOnly = True
        GridHistorial.RowHeadersWidth = 20
        GridHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        GridHistorial.Size = New Size(877, 112)
        GridHistorial.TabIndex = 0
        ' 
        ' LblResultado
        ' 
        LblResultado.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        LblResultado.Location = New Point(10, 599)
        LblResultado.Margin = New Padding(3, 7, 3, 3)
        LblResultado.Name = "LblResultado"
        LblResultado.Size = New Size(785, 36)
        LblResultado.TabIndex = 16
        ' 
        ' BtnCerrar
        ' 
        BtnCerrar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        BtnCerrar.BackColor = Color.IndianRed
        BtnCerrar.Cursor = Cursors.Hand
        BtnCerrar.FlatStyle = FlatStyle.Flat
        BtnCerrar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BtnCerrar.ForeColor = Color.White
        BtnCerrar.Location = New Point(813, 605)
        BtnCerrar.Name = "BtnCerrar"
        BtnCerrar.Size = New Size(88, 30)
        BtnCerrar.TabIndex = 17
        BtnCerrar.Text = "Salir"
        BtnCerrar.UseVisualStyleBackColor = False
        ' 
        ' GridSolicitudes
        ' 
        GridSolicitudes.AllowUserToAddRows = False
        GridSolicitudes.AllowUserToDeleteRows = False
        GridSolicitudes.AllowUserToResizeColumns = False
        GridSolicitudes.AllowUserToResizeRows = False
        DataGridViewCellStyle10.BackColor = Color.FloralWhite
        GridSolicitudes.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle10
        GridSolicitudes.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        DataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = SystemColors.Control
        DataGridViewCellStyle11.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        DataGridViewCellStyle11.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle11.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = DataGridViewTriState.True
        GridSolicitudes.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle11
        DataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = Color.White
        DataGridViewCellStyle12.Font = New Font("Segoe UI", 8F)
        DataGridViewCellStyle12.ForeColor = Color.Black
        DataGridViewCellStyle12.SelectionBackColor = Color.SteelBlue
        DataGridViewCellStyle12.SelectionForeColor = Color.White
        DataGridViewCellStyle12.WrapMode = DataGridViewTriState.False
        GridSolicitudes.DefaultCellStyle = DataGridViewCellStyle12
        GridSolicitudes.EnableHeadersVisualStyles = False
        GridSolicitudes.Location = New Point(10, 83)
        GridSolicitudes.MultiSelect = False
        GridSolicitudes.Name = "GridSolicitudes"
        GridSolicitudes.ReadOnly = True
        DataGridViewCellStyle13.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle13.BackColor = SystemColors.Control
        DataGridViewCellStyle13.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle13.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle13.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle13.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle13.WrapMode = DataGridViewTriState.True
        GridSolicitudes.RowHeadersDefaultCellStyle = DataGridViewCellStyle13
        GridSolicitudes.RowHeadersWidth = 20
        GridSolicitudes.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        GridSolicitudes.Size = New Size(884, 139)
        GridSolicitudes.TabIndex = 13
        ' 
        ' GrupoResolucion
        ' 
        GrupoResolucion.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        GrupoResolucion.Controls.Add(LblCotizacion)
        GrupoResolucion.Controls.Add(CmbCotizacionAutorizar)
        GrupoResolucion.Controls.Add(LblResolucion)
        GrupoResolucion.Controls.Add(TxtResolucion)
        GrupoResolucion.Controls.Add(BtnAutorizar)
        GrupoResolucion.Controls.Add(BtnRechazar)
        GrupoResolucion.Controls.Add(BtnDevolver)
        GrupoResolucion.Location = New Point(10, 477)
        GrupoResolucion.Name = "GrupoResolucion"
        GrupoResolucion.Size = New Size(891, 112)
        GrupoResolucion.TabIndex = 16
        GrupoResolucion.TabStop = False
        GrupoResolucion.Text = "Resolución de la solicitud seleccionada"
        ' 
        ' LblCotizacion
        ' 
        LblCotizacion.AutoSize = True
        LblCotizacion.Location = New Point(9, 19)
        LblCotizacion.Margin = New Padding(3, 7, 3, 3)
        LblCotizacion.Name = "LblCotizacion"
        LblCotizacion.Size = New Size(302, 15)
        LblCotizacion.TabIndex = 0
        LblCotizacion.Text = "Cotización a autorizar (selección obligatoria al autorizar)"
        ' 
        ' CmbCotizacionAutorizar
        ' 
        CmbCotizacionAutorizar.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        CmbCotizacionAutorizar.BackColor = Color.White
        CmbCotizacionAutorizar.DropDownStyle = ComboBoxStyle.DropDownList
        CmbCotizacionAutorizar.DropDownWidth = 700
        CmbCotizacionAutorizar.Location = New Point(9, 36)
        CmbCotizacionAutorizar.Name = "CmbCotizacionAutorizar"
        CmbCotizacionAutorizar.Size = New Size(711, 23)
        CmbCotizacionAutorizar.TabIndex = 1
        ' 
        ' LblResolucion
        ' 
        LblResolucion.AutoSize = True
        LblResolucion.Location = New Point(9, 62)
        LblResolucion.Margin = New Padding(3, 7, 3, 3)
        LblResolucion.Name = "LblResolucion"
        LblResolucion.Size = New Size(272, 15)
        LblResolucion.TabIndex = 2
        LblResolucion.Text = "Observación (obligatoria para rechazar o devolver)"
        ' 
        ' TxtResolucion
        ' 
        TxtResolucion.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        TxtResolucion.BackColor = Color.White
        TxtResolucion.Location = New Point(9, 79)
        TxtResolucion.MaxLength = 1000
        TxtResolucion.Multiline = True
        TxtResolucion.Name = "TxtResolucion"
        TxtResolucion.ScrollBars = ScrollBars.Vertical
        TxtResolucion.Size = New Size(711, 27)
        TxtResolucion.TabIndex = 3
        ' 
        ' BtnAutorizar
        ' 
        BtnAutorizar.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        BtnAutorizar.Cursor = Cursors.Hand
        BtnAutorizar.FlatStyle = FlatStyle.Flat
        BtnAutorizar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BtnAutorizar.Location = New Point(729, 15)
        BtnAutorizar.Name = "BtnAutorizar"
        BtnAutorizar.Size = New Size(156, 30)
        BtnAutorizar.TabIndex = 4
        BtnAutorizar.Text = "Autorizar"
        ' 
        ' BtnRechazar
        ' 
        BtnRechazar.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        BtnRechazar.Cursor = Cursors.Hand
        BtnRechazar.FlatStyle = FlatStyle.Flat
        BtnRechazar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BtnRechazar.Location = New Point(729, 47)
        BtnRechazar.Name = "BtnRechazar"
        BtnRechazar.Size = New Size(156, 30)
        BtnRechazar.TabIndex = 5
        BtnRechazar.Text = "Rechazar"
        ' 
        ' BtnDevolver
        ' 
        BtnDevolver.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        BtnDevolver.Cursor = Cursors.Hand
        BtnDevolver.FlatStyle = FlatStyle.Flat
        BtnDevolver.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BtnDevolver.Location = New Point(729, 79)
        BtnDevolver.Name = "BtnDevolver"
        BtnDevolver.Size = New Size(156, 30)
        BtnDevolver.TabIndex = 6
        BtnDevolver.Text = "Devolver para corregir"
        ' 
        ' frmAutorizarComprasUso
        ' 
        AcceptButton = BtnBuscar
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        ClientSize = New Size(911, 641)
        Controls.Add(BtnVerCotizacion)
        Controls.Add(LblDesde)
        Controls.Add(DtpDesde)
        Controls.Add(LblHasta)
        Controls.Add(DtpHasta)
        Controls.Add(LblSector)
        Controls.Add(CmbSector)
        Controls.Add(LblEstado)
        Controls.Add(CmbEstado)
        Controls.Add(LblBuscar)
        Controls.Add(TxtBuscar)
        Controls.Add(LblSolicitante)
        Controls.Add(TxtSolicitante)
        Controls.Add(BtnBuscar)
        Controls.Add(TxtResumen)
        Controls.Add(Pestanas)
        Controls.Add(LblResultado)
        Controls.Add(BtnCerrar)
        Controls.Add(GrupoResolucion)
        Controls.Add(GridSolicitudes)
        Font = New Font("Segoe UI", 9F)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(927, 593)
        Name = "frmAutorizarComprasUso"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Autorizar Compras"
        Pestanas.ResumeLayout(False)
        TabDetalle.ResumeLayout(False)
        CType(GridDetalle, ComponentModel.ISupportInitialize).EndInit()
        TabCotizaciones.ResumeLayout(False)
        CType(GridCotizaciones, ComponentModel.ISupportInitialize).EndInit()
        TabHistorial.ResumeLayout(False)
        CType(GridHistorial, ComponentModel.ISupportInitialize).EndInit()
        CType(GridSolicitudes, ComponentModel.ISupportInitialize).EndInit()
        GrupoResolucion.ResumeLayout(False)
        GrupoResolucion.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents LblDesde As Label
    Friend WithEvents DtpDesde As DateTimePicker
    Friend WithEvents LblHasta As Label
    Friend WithEvents DtpHasta As DateTimePicker
    Friend WithEvents LblSector As Label
    Friend WithEvents CmbSector As ComboBox
    Friend WithEvents LblEstado As Label
    Friend WithEvents CmbEstado As ComboBox
    Friend WithEvents LblBuscar As Label
    Friend WithEvents TxtBuscar As TextBox
    Friend WithEvents LblSolicitante As Label
    Friend WithEvents TxtSolicitante As TextBox
    Friend WithEvents BtnBuscar As Button
    Friend WithEvents GridSolicitudesColIdSolicitud As DataGridViewTextBoxColumn
    Friend WithEvents GridSolicitudesColFechaSolicitud As DataGridViewTextBoxColumn
    Friend WithEvents GridSolicitudesColSector As DataGridViewTextBoxColumn
    Friend WithEvents GridSolicitudesColUsuarioSolicitante As DataGridViewTextBoxColumn
    Friend WithEvents GridSolicitudesColEstado As DataGridViewTextBoxColumn
    Friend WithEvents GridSolicitudesColPrioridad As DataGridViewTextBoxColumn
    Friend WithEvents GridSolicitudesColFechaNecesidad As DataGridViewTextBoxColumn
    Friend WithEvents GridSolicitudesColMotivo As DataGridViewTextBoxColumn
    Friend WithEvents GridSolicitudesColRenglones As DataGridViewTextBoxColumn
    Friend WithEvents GridSolicitudesColCotizaciones As DataGridViewTextBoxColumn
    Friend WithEvents GridSolicitudesColUsuarioResolucion As DataGridViewTextBoxColumn
    Friend WithEvents GridSolicitudesColFechaResolucion As DataGridViewTextBoxColumn
    Friend WithEvents GridSolicitudesColProveedorAutorizado As DataGridViewTextBoxColumn
    Friend WithEvents GridSolicitudesColMonedaAutorizada As DataGridViewTextBoxColumn
    Friend WithEvents GridSolicitudesColImporteAutorizado As DataGridViewTextBoxColumn
    Friend WithEvents TxtResumen As TextBox
    Friend WithEvents Pestanas As TabControl
    Friend WithEvents TabDetalle As TabPage
    Friend WithEvents GridDetalle As DataGridView
    Friend WithEvents GridDetalleColTipoItem As DataGridViewTextBoxColumn
    Friend WithEvents GridDetalleColDescripcion As DataGridViewTextBoxColumn
    Friend WithEvents GridDetalleColCantidad As DataGridViewTextBoxColumn
    Friend WithEvents GridDetalleColUnidad As DataGridViewTextBoxColumn
    Friend WithEvents GridDetalleColFechaNecesidad As DataGridViewTextBoxColumn
    Friend WithEvents GridDetalleColObservaciones As DataGridViewTextBoxColumn
    Friend WithEvents TabCotizaciones As TabPage
    Friend WithEvents GridCotizaciones As DataGridView
    Friend WithEvents GridCotizacionesColCotizacionAutorizada As DataGridViewTextBoxColumn
    Friend WithEvents GridCotizacionesColProveedorNombre As DataGridViewTextBoxColumn
    Friend WithEvents GridCotizacionesColNumeroCotizacion As DataGridViewTextBoxColumn
    Friend WithEvents GridCotizacionesColFechaCotizacion As DataGridViewTextBoxColumn
    Friend WithEvents GridCotizacionesColArchivosPdf As DataGridViewTextBoxColumn
    Friend WithEvents GridCotizacionesColFechaVencimiento As DataGridViewTextBoxColumn
    Friend WithEvents GridCotizacionesColMoneda As DataGridViewTextBoxColumn
    Friend WithEvents GridCotizacionesColImporteNeto As DataGridViewTextBoxColumn
    Friend WithEvents GridCotizacionesColImporteIVA As DataGridViewTextBoxColumn
    Friend WithEvents GridCotizacionesColOtrosImportes As DataGridViewTextBoxColumn
    Friend WithEvents GridCotizacionesColImporteTotal As DataGridViewTextBoxColumn
    Friend WithEvents GridCotizacionesColCondicionPago As DataGridViewTextBoxColumn
    Friend WithEvents GridCotizacionesColPlazoEntrega As DataGridViewTextBoxColumn
    Friend WithEvents GridCotizacionesColObservaciones As DataGridViewTextBoxColumn
    Friend WithEvents TabHistorial As TabPage
    Friend WithEvents GridHistorial As DataGridView
    Friend WithEvents GridHistorialColFechaAccion As DataGridViewTextBoxColumn
    Friend WithEvents GridHistorialColUsuario As DataGridViewTextBoxColumn
    Friend WithEvents GridHistorialColAccion As DataGridViewTextBoxColumn
    Friend WithEvents GridHistorialColEstadoAnterior As DataGridViewTextBoxColumn
    Friend WithEvents GridHistorialColEstadoNuevo As DataGridViewTextBoxColumn
    Friend WithEvents GridHistorialColObservaciones As DataGridViewTextBoxColumn
    Friend WithEvents LblResultado As Label
    Friend WithEvents BtnCerrar As Button
    Friend WithEvents GridSolicitudes As DataGridView
    Friend WithEvents BtnVerCotizacion As Button

    Friend WithEvents GrupoResolucion As GroupBox
    Friend WithEvents LblCotizacion As Label
    Friend WithEvents CmbCotizacionAutorizar As ComboBox
    Friend WithEvents LblResolucion As Label
    Friend WithEvents TxtResolucion As TextBox
    Friend WithEvents BtnAutorizar As Button
    Friend WithEvents BtnRechazar As Button
    Friend WithEvents BtnDevolver As Button
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmServicios
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        gbCabecera = New GroupBox()
        txtTotalServicio = New TextBox()
        LblTotalServicio = New Label()
        txtSubTotalItems = New TextBox()
        LblSubTotalItems = New Label()
        txtOtrosGastos = New TextBox()
        LblOtrosGastos = New Label()
        txtObservaciones = New TextBox()
        LblObservaciones = New Label()
        dtpFechaFactura = New DateTimePicker()
        LblFechaFactura = New Label()
        txtPuntoDeVenta = New TextBox()
        LblPuntoDeVenta = New Label()
        txtNroFactura = New TextBox()
        LblNroFactura = New Label()
        txtKmServicio = New TextBox()
        LblKmServicio = New Label()
        dtpFechaServicio = New DateTimePicker()
        LblFechaServicio = New Label()
        txtNombreProv = New TextBox()
        LblNombreProv = New Label()
        txtNroCuentaProv = New TextBox()
        LblNroCuentaProv = New Label()
        btnBuscarProv = New Button()
        cboVehiculo = New ComboBox()
        LblVehiculo = New Label()
        txtIdServicio = New TextBox()
        LblCodigo = New Label()
        gbItems = New GroupBox()
        DgvItems = New DataGridView()
        btnAgregarItem = New Button()
        btnQuitarItem = New Button()
        chkEncabezados = New CheckBox()
        lnkCopiar = New LinkLabel()
        gbBusqueda = New GroupBox()
        cboSucursal = New ComboBox()
        LblSucursal = New Label()
        dtpBusqHasta = New DateTimePicker()
        LblBusqHasta = New Label()
        dtpBusqDesde = New DateTimePicker()
        LblBusqDesde = New Label()
        cboBusqVehiculo = New ComboBox()
        LblBusqVehiculo = New Label()
        btnBuscar = New Button()
        DgvListado = New DataGridView()
        CmdAgregar = New Button()
        btnModificar = New Button()
        CmdBorrar = New Button()
        cmdAceptar = New Button()
        CmdCancelar = New Button()
        CmdSalir = New Button()
        gbCabecera.SuspendLayout()
        gbItems.SuspendLayout()
        CType(DgvItems, ComponentModel.ISupportInitialize).BeginInit()
        gbBusqueda.SuspendLayout()
        CType(DgvListado, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' gbCabecera
        ' 
        gbCabecera.Controls.Add(txtTotalServicio)
        gbCabecera.Controls.Add(LblTotalServicio)
        gbCabecera.Controls.Add(txtSubTotalItems)
        gbCabecera.Controls.Add(LblSubTotalItems)
        gbCabecera.Controls.Add(txtOtrosGastos)
        gbCabecera.Controls.Add(LblOtrosGastos)
        gbCabecera.Controls.Add(txtObservaciones)
        gbCabecera.Controls.Add(LblObservaciones)
        gbCabecera.Controls.Add(dtpFechaFactura)
        gbCabecera.Controls.Add(LblFechaFactura)
        gbCabecera.Controls.Add(txtPuntoDeVenta)
        gbCabecera.Controls.Add(LblPuntoDeVenta)
        gbCabecera.Controls.Add(txtNroFactura)
        gbCabecera.Controls.Add(LblNroFactura)
        gbCabecera.Controls.Add(txtKmServicio)
        gbCabecera.Controls.Add(LblKmServicio)
        gbCabecera.Controls.Add(dtpFechaServicio)
        gbCabecera.Controls.Add(LblFechaServicio)
        gbCabecera.Controls.Add(txtNombreProv)
        gbCabecera.Controls.Add(LblNombreProv)
        gbCabecera.Controls.Add(txtNroCuentaProv)
        gbCabecera.Controls.Add(LblNroCuentaProv)
        gbCabecera.Controls.Add(btnBuscarProv)
        gbCabecera.Controls.Add(cboVehiculo)
        gbCabecera.Controls.Add(LblVehiculo)
        gbCabecera.Controls.Add(txtIdServicio)
        gbCabecera.Controls.Add(LblCodigo)
        gbCabecera.Location = New Point(10, 200)
        gbCabecera.Name = "gbCabecera"
        gbCabecera.Size = New Size(1000, 165)
        gbCabecera.TabIndex = 0
        gbCabecera.TabStop = False
        gbCabecera.Text = "Datos del Servicio"
        ' 
        ' txtTotalServicio
        ' 
        txtTotalServicio.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        txtTotalServicio.Location = New Point(870, 125)
        txtTotalServicio.Name = "txtTotalServicio"
        txtTotalServicio.ReadOnly = True
        txtTotalServicio.Size = New Size(110, 23)
        txtTotalServicio.TabIndex = 29
        txtTotalServicio.TextAlign = HorizontalAlignment.Right
        ' 
        ' LblTotalServicio
        ' 
        LblTotalServicio.AutoSize = True
        LblTotalServicio.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LblTotalServicio.Location = New Point(790, 128)
        LblTotalServicio.Name = "LblTotalServicio"
        LblTotalServicio.Size = New Size(65, 15)
        LblTotalServicio.TabIndex = 28
        LblTotalServicio.Text = "Total Serv."
        ' 
        ' txtSubTotalItems
        ' 
        txtSubTotalItems.Location = New Point(870, 71)
        txtSubTotalItems.Name = "txtSubTotalItems"
        txtSubTotalItems.ReadOnly = True
        txtSubTotalItems.Size = New Size(110, 23)
        txtSubTotalItems.TabIndex = 27
        txtSubTotalItems.TextAlign = HorizontalAlignment.Right
        ' 
        ' LblSubTotalItems
        ' 
        LblSubTotalItems.AutoSize = True
        LblSubTotalItems.Location = New Point(790, 74)
        LblSubTotalItems.Name = "LblSubTotalItems"
        LblSubTotalItems.Size = New Size(83, 15)
        LblSubTotalItems.TabIndex = 26
        LblSubTotalItems.Text = "Subtotal Items"
        ' 
        ' txtOtrosGastos
        ' 
        txtOtrosGastos.Location = New Point(870, 98)
        txtOtrosGastos.Name = "txtOtrosGastos"
        txtOtrosGastos.Size = New Size(110, 23)
        txtOtrosGastos.TabIndex = 25
        txtOtrosGastos.TextAlign = HorizontalAlignment.Right
        ' 
        ' LblOtrosGastos
        ' 
        LblOtrosGastos.AutoSize = True
        LblOtrosGastos.Location = New Point(790, 101)
        LblOtrosGastos.Name = "LblOtrosGastos"
        LblOtrosGastos.Size = New Size(74, 15)
        LblOtrosGastos.TabIndex = 24
        LblOtrosGastos.Text = "Otros Gastos"
        ' 
        ' txtObservaciones
        ' 
        txtObservaciones.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtObservaciones.Location = New Point(100, 103)
        txtObservaciones.Name = "txtObservaciones"
        txtObservaciones.Size = New Size(660, 23)
        txtObservaciones.TabIndex = 23
        ' 
        ' LblObservaciones
        ' 
        LblObservaciones.AutoSize = True
        LblObservaciones.Location = New Point(10, 106)
        LblObservaciones.Name = "LblObservaciones"
        LblObservaciones.Size = New Size(84, 15)
        LblObservaciones.TabIndex = 22
        LblObservaciones.Text = "Observaciones"
        ' 
        ' dtpFechaFactura
        ' 
        dtpFechaFactura.Format = DateTimePickerFormat.Short
        dtpFechaFactura.Location = New Point(546, 74)
        dtpFechaFactura.Name = "dtpFechaFactura"
        dtpFechaFactura.Size = New Size(150, 23)
        dtpFechaFactura.TabIndex = 21
        ' 
        ' LblFechaFactura
        ' 
        LblFechaFactura.AutoSize = True
        LblFechaFactura.Location = New Point(466, 77)
        LblFechaFactura.Name = "LblFechaFactura"
        LblFechaFactura.Size = New Size(70, 15)
        LblFechaFactura.TabIndex = 20
        LblFechaFactura.Text = "Fec. Factura"
        ' 
        ' txtPuntoDeVenta
        ' 
        txtPuntoDeVenta.Location = New Point(406, 74)
        txtPuntoDeVenta.Name = "txtPuntoDeVenta"
        txtPuntoDeVenta.Size = New Size(50, 23)
        txtPuntoDeVenta.TabIndex = 19
        txtPuntoDeVenta.TextAlign = HorizontalAlignment.Right
        ' 
        ' LblPuntoDeVenta
        ' 
        LblPuntoDeVenta.AutoSize = True
        LblPuntoDeVenta.Location = New Point(336, 77)
        LblPuntoDeVenta.Name = "LblPuntoDeVenta"
        LblPuntoDeVenta.Size = New Size(60, 15)
        LblPuntoDeVenta.TabIndex = 18
        LblPuntoDeVenta.Text = "Pto. Venta"
        ' 
        ' txtNroFactura
        ' 
        txtNroFactura.Location = New Point(276, 74)
        txtNroFactura.Name = "txtNroFactura"
        txtNroFactura.Size = New Size(50, 23)
        txtNroFactura.TabIndex = 17
        txtNroFactura.TextAlign = HorizontalAlignment.Right
        ' 
        ' LblNroFactura
        ' 
        LblNroFactura.AutoSize = True
        LblNroFactura.Location = New Point(196, 77)
        LblNroFactura.Name = "LblNroFactura"
        LblNroFactura.Size = New Size(69, 15)
        LblNroFactura.TabIndex = 16
        LblNroFactura.Text = "Nro Factura"
        ' 
        ' txtKmServicio
        ' 
        txtKmServicio.Location = New Point(532, 17)
        txtKmServicio.Name = "txtKmServicio"
        txtKmServicio.Size = New Size(70, 23)
        txtKmServicio.TabIndex = 11
        txtKmServicio.TextAlign = HorizontalAlignment.Right
        ' 
        ' LblKmServicio
        ' 
        LblKmServicio.AutoSize = True
        LblKmServicio.Location = New Point(473, 20)
        LblKmServicio.Name = "LblKmServicio"
        LblKmServicio.Size = New Size(53, 15)
        LblKmServicio.TabIndex = 10
        LblKmServicio.Text = "Km Serv."
        ' 
        ' dtpFechaServicio
        ' 
        dtpFechaServicio.Format = DateTimePickerFormat.Short
        dtpFechaServicio.Location = New Point(87, 74)
        dtpFechaServicio.Name = "dtpFechaServicio"
        dtpFechaServicio.Size = New Size(103, 23)
        dtpFechaServicio.TabIndex = 9
        ' 
        ' LblFechaServicio
        ' 
        LblFechaServicio.AutoSize = True
        LblFechaServicio.Location = New Point(10, 79)
        LblFechaServicio.Name = "LblFechaServicio"
        LblFechaServicio.Size = New Size(72, 15)
        LblFechaServicio.TabIndex = 8
        LblFechaServicio.Text = "Fec. Servicio"
        ' 
        ' txtNombreProv
        ' 
        txtNombreProv.Location = New Point(406, 44)
        txtNombreProv.Name = "txtNombreProv"
        txtNombreProv.ReadOnly = True
        txtNombreProv.Size = New Size(420, 23)
        txtNombreProv.TabIndex = 7
        ' 
        ' LblNombreProv
        ' 
        LblNombreProv.AutoSize = True
        LblNombreProv.Location = New Point(292, 48)
        LblNombreProv.Name = "LblNombreProv"
        LblNombreProv.Size = New Size(108, 15)
        LblNombreProv.TabIndex = 6
        LblNombreProv.Text = "Nombre Proveedor"
        ' 
        ' txtNroCuentaProv
        ' 
        txtNroCuentaProv.Location = New Point(125, 45)
        txtNroCuentaProv.Name = "txtNroCuentaProv"
        txtNroCuentaProv.Size = New Size(80, 23)
        txtNroCuentaProv.TabIndex = 5
        txtNroCuentaProv.TextAlign = HorizontalAlignment.Right
        ' 
        ' LblNroCuentaProv
        ' 
        LblNroCuentaProv.AutoSize = True
        LblNroCuentaProv.Location = New Point(10, 49)
        LblNroCuentaProv.Name = "LblNroCuentaProv"
        LblNroCuentaProv.Size = New Size(108, 15)
        LblNroCuentaProv.TabIndex = 4
        LblNroCuentaProv.Text = "Nro Cta. Proveedor"
        ' 
        ' btnBuscarProv
        ' 
        btnBuscarProv.FlatStyle = FlatStyle.Flat
        btnBuscarProv.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnBuscarProv.Location = New Point(211, 44)
        btnBuscarProv.Name = "btnBuscarProv"
        btnBuscarProv.Size = New Size(70, 25)
        btnBuscarProv.TabIndex = 3
        btnBuscarProv.Text = "Buscar"
        btnBuscarProv.UseVisualStyleBackColor = True
        ' 
        ' cboVehiculo
        ' 
        cboVehiculo.DropDownStyle = ComboBoxStyle.DropDownList
        cboVehiculo.FormattingEnabled = True
        cboVehiculo.Location = New Point(100, 15)
        cboVehiculo.Name = "cboVehiculo"
        cboVehiculo.Size = New Size(230, 23)
        cboVehiculo.TabIndex = 2
        ' 
        ' LblVehiculo
        ' 
        LblVehiculo.AutoSize = True
        LblVehiculo.Location = New Point(10, 18)
        LblVehiculo.Name = "LblVehiculo"
        LblVehiculo.Size = New Size(52, 15)
        LblVehiculo.TabIndex = 1
        LblVehiculo.Text = "Vehículo"
        ' 
        ' txtIdServicio
        ' 
        txtIdServicio.Location = New Point(406, 15)
        txtIdServicio.Name = "txtIdServicio"
        txtIdServicio.ReadOnly = True
        txtIdServicio.Size = New Size(60, 23)
        txtIdServicio.TabIndex = 0
        txtIdServicio.TextAlign = HorizontalAlignment.Right
        ' 
        ' LblCodigo
        ' 
        LblCodigo.AutoSize = True
        LblCodigo.Location = New Point(356, 19)
        LblCodigo.Name = "LblCodigo"
        LblCodigo.Size = New Size(46, 15)
        LblCodigo.TabIndex = 30
        LblCodigo.Text = "Código"
        ' 
        ' gbItems
        ' 
        gbItems.Controls.Add(DgvItems)
        gbItems.Controls.Add(btnAgregarItem)
        gbItems.Controls.Add(btnQuitarItem)
        gbItems.Controls.Add(chkEncabezados)
        gbItems.Controls.Add(lnkCopiar)
        gbItems.Location = New Point(10, 375)
        gbItems.Name = "gbItems"
        gbItems.Size = New Size(1000, 265)
        gbItems.TabIndex = 1
        gbItems.TabStop = False
        gbItems.Text = "Items del Servicio"
        ' 
        ' DgvItems
        ' 
        DgvItems.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DgvItems.Location = New Point(10, 22)
        DgvItems.Name = "DgvItems"
        DgvItems.Size = New Size(900, 216)
        DgvItems.TabIndex = 0
        ' 
        ' btnAgregarItem
        ' 
        btnAgregarItem.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnAgregarItem.FlatStyle = FlatStyle.Flat
        btnAgregarItem.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnAgregarItem.Location = New Point(916, 22)
        btnAgregarItem.Name = "btnAgregarItem"
        btnAgregarItem.Size = New Size(75, 28)
        btnAgregarItem.TabIndex = 1
        btnAgregarItem.Text = "Agregar"
        btnAgregarItem.UseVisualStyleBackColor = True
        ' 
        ' btnQuitarItem
        ' 
        btnQuitarItem.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnQuitarItem.FlatStyle = FlatStyle.Flat
        btnQuitarItem.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnQuitarItem.Location = New Point(916, 56)
        btnQuitarItem.Name = "btnQuitarItem"
        btnQuitarItem.Size = New Size(75, 28)
        btnQuitarItem.TabIndex = 2
        btnQuitarItem.Text = "Quitar"
        btnQuitarItem.UseVisualStyleBackColor = True
        ' 
        ' chkEncabezados
        ' 
        chkEncabezados.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        chkEncabezados.AutoSize = True
        chkEncabezados.Location = New Point(810, 239)
        chkEncabezados.Name = "chkEncabezados"
        chkEncabezados.Size = New Size(119, 19)
        chkEncabezados.TabIndex = 4
        chkEncabezados.Text = "Con encabezados"
        chkEncabezados.UseVisualStyleBackColor = True
        ' 
        ' lnkCopiar
        ' 
        lnkCopiar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        lnkCopiar.AutoSize = True
        lnkCopiar.LinkColor = Color.Black
        lnkCopiar.Location = New Point(710, 241)
        lnkCopiar.Name = "lnkCopiar"
        lnkCopiar.Size = New Size(94, 15)
        lnkCopiar.TabIndex = 3
        lnkCopiar.TabStop = True
        lnkCopiar.Text = "Copiar selección"
        ' 
        ' gbBusqueda
        ' 
        gbBusqueda.Controls.Add(cboSucursal)
        gbBusqueda.Controls.Add(LblSucursal)
        gbBusqueda.Controls.Add(dtpBusqHasta)
        gbBusqueda.Controls.Add(LblBusqHasta)
        gbBusqueda.Controls.Add(dtpBusqDesde)
        gbBusqueda.Controls.Add(LblBusqDesde)
        gbBusqueda.Controls.Add(cboBusqVehiculo)
        gbBusqueda.Controls.Add(LblBusqVehiculo)
        gbBusqueda.Controls.Add(btnBuscar)
        gbBusqueda.Controls.Add(DgvListado)
        gbBusqueda.Location = New Point(10, 10)
        gbBusqueda.Name = "gbBusqueda"
        gbBusqueda.Size = New Size(1000, 184)
        gbBusqueda.TabIndex = 2
        gbBusqueda.TabStop = False
        gbBusqueda.Text = "Búsqueda de Servicios"
        ' 
        ' cboSucursal
        ' 
        cboSucursal.DropDownStyle = ComboBoxStyle.DropDownList
        cboSucursal.FormattingEnabled = True
        cboSucursal.Location = New Point(271, 22)
        cboSucursal.Name = "cboSucursal"
        cboSucursal.Size = New Size(131, 23)
        cboSucursal.TabIndex = 15
        ' 
        ' LblSucursal
        ' 
        LblSucursal.AutoSize = True
        LblSucursal.Location = New Point(214, 25)
        LblSucursal.Name = "LblSucursal"
        LblSucursal.Size = New Size(51, 15)
        LblSucursal.TabIndex = 14
        LblSucursal.Text = "Sucursal"
        ' 
        ' dtpBusqHasta
        ' 
        dtpBusqHasta.Format = DateTimePickerFormat.Short
        dtpBusqHasta.Location = New Point(657, 22)
        dtpBusqHasta.Name = "dtpBusqHasta"
        dtpBusqHasta.Size = New Size(140, 23)
        dtpBusqHasta.TabIndex = 6
        ' 
        ' LblBusqHasta
        ' 
        LblBusqHasta.AutoSize = True
        LblBusqHasta.Location = New Point(612, 25)
        LblBusqHasta.Name = "LblBusqHasta"
        LblBusqHasta.Size = New Size(37, 15)
        LblBusqHasta.TabIndex = 5
        LblBusqHasta.Text = "Hasta"
        ' 
        ' dtpBusqDesde
        ' 
        dtpBusqDesde.Format = DateTimePickerFormat.Short
        dtpBusqDesde.Location = New Point(462, 22)
        dtpBusqDesde.Name = "dtpBusqDesde"
        dtpBusqDesde.Size = New Size(140, 23)
        dtpBusqDesde.TabIndex = 4
        ' 
        ' LblBusqDesde
        ' 
        LblBusqDesde.AutoSize = True
        LblBusqDesde.Location = New Point(417, 25)
        LblBusqDesde.Name = "LblBusqDesde"
        LblBusqDesde.Size = New Size(39, 15)
        LblBusqDesde.TabIndex = 3
        LblBusqDesde.Text = "Desde"
        ' 
        ' cboBusqVehiculo
        ' 
        cboBusqVehiculo.DropDownStyle = ComboBoxStyle.DropDownList
        cboBusqVehiculo.FormattingEnabled = True
        cboBusqVehiculo.Location = New Point(68, 21)
        cboBusqVehiculo.Name = "cboBusqVehiculo"
        cboBusqVehiculo.Size = New Size(137, 23)
        cboBusqVehiculo.TabIndex = 2
        ' 
        ' LblBusqVehiculo
        ' 
        LblBusqVehiculo.AutoSize = True
        LblBusqVehiculo.Location = New Point(10, 25)
        LblBusqVehiculo.Name = "LblBusqVehiculo"
        LblBusqVehiculo.Size = New Size(52, 15)
        LblBusqVehiculo.TabIndex = 1
        LblBusqVehiculo.Text = "Vehículo"
        ' 
        ' btnBuscar
        ' 
        btnBuscar.FlatStyle = FlatStyle.Flat
        btnBuscar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnBuscar.Location = New Point(807, 20)
        btnBuscar.Name = "btnBuscar"
        btnBuscar.Size = New Size(75, 25)
        btnBuscar.TabIndex = 0
        btnBuscar.Text = "Buscar"
        btnBuscar.UseVisualStyleBackColor = True
        ' 
        ' DgvListado
        ' 
        DgvListado.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DgvListado.Location = New Point(10, 50)
        DgvListado.MultiSelect = False
        DgvListado.Name = "DgvListado"
        DgvListado.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DgvListado.Size = New Size(980, 125)
        DgvListado.TabIndex = 7
        ' 
        ' CmdAgregar
        ' 
        CmdAgregar.FlatStyle = FlatStyle.Flat
        CmdAgregar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdAgregar.Location = New Point(449, 651)
        CmdAgregar.Name = "CmdAgregar"
        CmdAgregar.Size = New Size(85, 33)
        CmdAgregar.TabIndex = 3
        CmdAgregar.Text = "&Agregar"
        CmdAgregar.UseVisualStyleBackColor = True
        ' 
        ' btnModificar
        ' 
        btnModificar.FlatStyle = FlatStyle.Flat
        btnModificar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnModificar.Location = New Point(635, 651)
        btnModificar.Name = "btnModificar"
        btnModificar.Size = New Size(85, 33)
        btnModificar.TabIndex = 4
        btnModificar.Text = "&Modificar"
        btnModificar.UseVisualStyleBackColor = True
        ' 
        ' CmdBorrar
        ' 
        CmdBorrar.FlatStyle = FlatStyle.Flat
        CmdBorrar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdBorrar.Location = New Point(542, 651)
        CmdBorrar.Name = "CmdBorrar"
        CmdBorrar.Size = New Size(85, 33)
        CmdBorrar.TabIndex = 5
        CmdBorrar.Text = "&Borrar"
        CmdBorrar.UseVisualStyleBackColor = True
        ' 
        ' cmdAceptar
        ' 
        cmdAceptar.FlatStyle = FlatStyle.Flat
        cmdAceptar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdAceptar.Location = New Point(728, 651)
        cmdAceptar.Name = "cmdAceptar"
        cmdAceptar.Size = New Size(85, 33)
        cmdAceptar.TabIndex = 6
        cmdAceptar.Text = "Ac&eptar"
        cmdAceptar.UseVisualStyleBackColor = True
        ' 
        ' CmdCancelar
        ' 
        CmdCancelar.FlatStyle = FlatStyle.Flat
        CmdCancelar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdCancelar.Location = New Point(821, 651)
        CmdCancelar.Name = "CmdCancelar"
        CmdCancelar.Size = New Size(85, 33)
        CmdCancelar.TabIndex = 7
        CmdCancelar.Text = "C&ancelar"
        CmdCancelar.UseVisualStyleBackColor = True
        ' 
        ' CmdSalir
        ' 
        CmdSalir.BackColor = Color.IndianRed
        CmdSalir.FlatStyle = FlatStyle.Flat
        CmdSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdSalir.ForeColor = Color.White
        CmdSalir.Location = New Point(914, 651)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(85, 33)
        CmdSalir.TabIndex = 8
        CmdSalir.Text = "Salir"
        CmdSalir.UseVisualStyleBackColor = False
        ' 
        ' frmServicios
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1020, 695)
        Controls.Add(CmdCancelar)
        Controls.Add(cmdAceptar)
        Controls.Add(CmdSalir)
        Controls.Add(CmdBorrar)
        Controls.Add(btnModificar)
        Controls.Add(CmdAgregar)
        Controls.Add(gbBusqueda)
        Controls.Add(gbItems)
        Controls.Add(gbCabecera)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmServicios"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Servicios de Mantenimiento"
        gbCabecera.ResumeLayout(False)
        gbCabecera.PerformLayout()
        gbItems.ResumeLayout(False)
        gbItems.PerformLayout()
        CType(DgvItems, ComponentModel.ISupportInitialize).EndInit()
        gbBusqueda.ResumeLayout(False)
        gbBusqueda.PerformLayout()
        CType(DgvListado, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents gbCabecera As GroupBox
    Friend WithEvents cboVehiculo As ComboBox
    Friend WithEvents LblVehiculo As Label
    Friend WithEvents txtIdServicio As TextBox
    Friend WithEvents LblCodigo As Label
    Friend WithEvents txtNombreProv As TextBox
    Friend WithEvents LblNombreProv As Label
    Friend WithEvents txtNroCuentaProv As TextBox
    Friend WithEvents LblNroCuentaProv As Label
    Friend WithEvents btnBuscarProv As Button
    Friend WithEvents dtpFechaServicio As DateTimePicker
    Friend WithEvents LblFechaServicio As Label
    Friend WithEvents txtKmServicio As TextBox
    Friend WithEvents LblKmServicio As Label
    Friend WithEvents txtPuntoDeVenta As TextBox
    Friend WithEvents LblPuntoDeVenta As Label
    Friend WithEvents txtNroFactura As TextBox
    Friend WithEvents LblNroFactura As Label
    Friend WithEvents dtpFechaFactura As DateTimePicker
    Friend WithEvents LblFechaFactura As Label
    Friend WithEvents txtObservaciones As TextBox
    Friend WithEvents LblObservaciones As Label
    Friend WithEvents txtTotalServicio As TextBox
    Friend WithEvents LblTotalServicio As Label
    Friend WithEvents txtSubTotalItems As TextBox
    Friend WithEvents LblSubTotalItems As Label
    Friend WithEvents txtOtrosGastos As TextBox
    Friend WithEvents LblOtrosGastos As Label
    Friend WithEvents gbItems As GroupBox
    Friend WithEvents DgvItems As DataGridView
    Friend WithEvents btnAgregarItem As Button
    Friend WithEvents btnQuitarItem As Button
    Friend WithEvents chkEncabezados As CheckBox
    Friend WithEvents lnkCopiar As LinkLabel
    Friend WithEvents gbBusqueda As GroupBox
    Friend WithEvents DgvListado As DataGridView
    Friend WithEvents btnBuscar As Button
    Friend WithEvents cboBusqVehiculo As ComboBox
    Friend WithEvents LblBusqVehiculo As Label
    Friend WithEvents dtpBusqHasta As DateTimePicker
    Friend WithEvents LblBusqHasta As Label
    Friend WithEvents dtpBusqDesde As DateTimePicker
    Friend WithEvents LblBusqDesde As Label
    Friend WithEvents CmdAgregar As Button
    Friend WithEvents btnModificar As Button
    Friend WithEvents CmdBorrar As Button
    Friend WithEvents cmdAceptar As Button
    Friend WithEvents CmdCancelar As Button
    Friend WithEvents CmdSalir As Button
    Friend WithEvents cboSucursal As ComboBox
    Friend WithEvents LblSucursal As Label
End Class

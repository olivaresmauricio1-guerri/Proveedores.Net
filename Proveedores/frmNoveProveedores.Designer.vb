<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNoveProveedores
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNoveProveedores))
        pnlTop = New Panel()
        lblFechaHasta = New Label()
        dtpFechaHasta = New DateTimePicker()
        lblFechaDesde = New Label()
        dtpFechaDesde = New DateTimePicker()
        lblBuscar = New Label()
        TxtBuscar = New TextBox()
        DgvListado = New DataGridView()
        grpEdicion = New GroupBox()
        TableLayoutPanel1 = New TableLayoutPanel()
        Panel1 = New Panel()
        btnBuscarProveedor = New Button()
        Label2 = New Label()
        Label1 = New Label()
        txtCuit = New TextBox()
        dtpFecha = New DateTimePicker()
        cmbComprobante = New ComboBox()
        chkDolar = New CheckBox()
        lblDolar = New Label()
        txtDolar = New TextBox()
        lblCAI = New Label()
        txtCAI = New TextBox()
        lblNroComprobante = New Label()
        txtNroComprobante = New TextBox()
        lblFondoFijo = New Label()
        txtFondoFijo = New TextBox()
        lblComprobante = New Label()
        lblFecha = New Label()
        lblDespacho = New Label()
        txtDespacho = New TextBox()
        lblNroFactura = New Label()
        txtNroFactura = New TextBox()
        lblPuntoVenta = New Label()
        txtPuntoVenta = New TextBox()
        cmbProveedor = New ComboBox()
        lblProveedor = New Label()
        cmbSucursal = New ComboBox()
        txtNroCuenta = New TextBox()
        lblSucursal = New Label()
        Panel2 = New Panel()
        txtCuentaIngresosBrutos6 = New TextBox()
        txtIngresosBrutos6 = New TextBox()
        lblIngresosBrutos6 = New Label()
        txtCuentaIngresosBrutos5 = New TextBox()
        txtIngresosBrutos5 = New TextBox()
        lblIngresosBrutos5 = New Label()
        txtCuentaIngresosBrutos4 = New TextBox()
        txtIngresosBrutos4 = New TextBox()
        lblIngresosBrutos4 = New Label()
        txtCuentaIngresosBrutos3 = New TextBox()
        txtIngresosBrutos3 = New TextBox()
        lblIngresosBrutos3 = New Label()
        txtCuentaIngresosBrutos2 = New TextBox()
        txtIngresosBrutos2 = New TextBox()
        lblIngresosBrutos2 = New Label()
        txtCuentaIngresosBrutos1 = New TextBox()
        txtIngresosBrutos1 = New TextBox()
        lblIngresosBrutos1 = New Label()
        txtCuentaRetPerIVA = New TextBox()
        txtRetPerIVA = New TextBox()
        lblRetPerIVA = New Label()
        txtCuentaGanancia = New TextBox()
        txtGanancia = New TextBox()
        lblGanancia = New Label()
        txtCuentaIVA = New TextBox()
        txtIVA = New TextBox()
        lblIVA = New Label()
        txtCuentaExentos = New TextBox()
        txtExentos = New TextBox()
        lblExentos = New Label()
        txtCuentaNGrav27 = New TextBox()
        txtNGrav27 = New TextBox()
        lblNGrav27 = New Label()
        txtCuentaNGrav21 = New TextBox()
        txtNGrav21 = New TextBox()
        lblNGrav21 = New Label()
        txtCuentaNGrav105 = New TextBox()
        txtNGrav105 = New TextBox()
        lblNGrav105 = New Label()
        Label12 = New Label()
        txtCuentaComprasRNI = New TextBox()
        txtComprasRNI = New TextBox()
        lblComprasRNI = New Label()
        Panel3 = New Panel()
        btnConsultarCuenta = New Button()
        cmbCuentaMonto3 = New ComboBox()
        cmbCuentaMonto2 = New ComboBox()
        cmbCuentaMonto1 = New ComboBox()
        boxPlanCuentas = New GroupBox()
        dgvListadoCuenta = New DataGridView()
        txtBuscarCuenta = New TextBox()
        txtComentario = New TextBox()
        lblComentario = New Label()
        chkNoGeneraAsiento = New CheckBox()
        txtMonto3 = New TextBox()
        lblMonto = New Label()
        txtMonto1 = New TextBox()
        Label26 = New Label()
        txtMonto2 = New TextBox()
        lnkCopiar = New LinkLabel()
        chkEncabezados = New CheckBox()
        CmdSalir = New Button()
        CmdAgregar = New Button()
        CmdModificar = New Button()
        CmdBorrar = New Button()
        cmdAceptar = New Button()
        CmdCancelar = New Button()
        pnlTop.SuspendLayout()
        CType(DgvListado, ComponentModel.ISupportInitialize).BeginInit()
        grpEdicion.SuspendLayout()
        TableLayoutPanel1.SuspendLayout()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        Panel3.SuspendLayout()
        boxPlanCuentas.SuspendLayout()
        CType(dgvListadoCuenta, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' pnlTop
        ' 
        pnlTop.Controls.Add(lblFechaHasta)
        pnlTop.Controls.Add(dtpFechaHasta)
        pnlTop.Controls.Add(lblFechaDesde)
        pnlTop.Controls.Add(dtpFechaDesde)
        pnlTop.Controls.Add(lblBuscar)
        pnlTop.Controls.Add(TxtBuscar)
        pnlTop.Dock = DockStyle.Top
        pnlTop.Location = New Point(0, 0)
        pnlTop.Name = "pnlTop"
        pnlTop.Padding = New Padding(8)
        pnlTop.Size = New Size(1055, 47)
        pnlTop.TabIndex = 2
        ' 
        ' lblFechaHasta
        ' 
        lblFechaHasta.AutoSize = True
        lblFechaHasta.Location = New Point(762, 13)
        lblFechaHasta.Name = "lblFechaHasta"
        lblFechaHasta.Size = New Size(40, 15)
        lblFechaHasta.TabIndex = 4
        lblFechaHasta.Text = "Hasta:"
        ' 
        ' dtpFechaHasta
        ' 
        dtpFechaHasta.Format = DateTimePickerFormat.Short
        dtpFechaHasta.Location = New Point(806, 9)
        dtpFechaHasta.Name = "dtpFechaHasta"
        dtpFechaHasta.Size = New Size(96, 23)
        dtpFechaHasta.TabIndex = 5
        ' 
        ' lblFechaDesde
        ' 
        lblFechaDesde.AutoSize = True
        lblFechaDesde.Location = New Point(604, 13)
        lblFechaDesde.Name = "lblFechaDesde"
        lblFechaDesde.Size = New Size(42, 15)
        lblFechaDesde.TabIndex = 2
        lblFechaDesde.Text = "Desde:"
        ' 
        ' dtpFechaDesde
        ' 
        dtpFechaDesde.Format = DateTimePickerFormat.Short
        dtpFechaDesde.Location = New Point(648, 9)
        dtpFechaDesde.Name = "dtpFechaDesde"
        dtpFechaDesde.Size = New Size(96, 23)
        dtpFechaDesde.TabIndex = 3
        ' 
        ' lblBuscar
        ' 
        lblBuscar.AutoSize = True
        lblBuscar.Location = New Point(15, 13)
        lblBuscar.Name = "lblBuscar"
        lblBuscar.Size = New Size(42, 15)
        lblBuscar.TabIndex = 0
        lblBuscar.Text = "Buscar"
        ' 
        ' TxtBuscar
        ' 
        TxtBuscar.Location = New Point(63, 9)
        TxtBuscar.Name = "TxtBuscar"
        TxtBuscar.Size = New Size(519, 23)
        TxtBuscar.TabIndex = 1
        ' 
        ' DgvListado
        ' 
        DgvListado.AllowUserToAddRows = False
        DgvListado.AllowUserToDeleteRows = False
        DgvListado.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DgvListado.Location = New Point(0, 47)
        DgvListado.Name = "DgvListado"
        DgvListado.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DgvListado.Size = New Size(1055, 147)
        DgvListado.TabIndex = 6
        ' 
        ' grpEdicion
        ' 
        grpEdicion.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        grpEdicion.Controls.Add(TableLayoutPanel1)
        grpEdicion.Location = New Point(0, 206)
        grpEdicion.Name = "grpEdicion"
        grpEdicion.Padding = New Padding(0)
        grpEdicion.Size = New Size(1055, 451)
        grpEdicion.TabIndex = 100
        grpEdicion.TabStop = False
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 3
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 34F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33F))
        TableLayoutPanel1.Controls.Add(Panel1, 0, 0)
        TableLayoutPanel1.Controls.Add(Panel2, 1, 0)
        TableLayoutPanel1.Controls.Add(Panel3, 2, 0)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(0, 16)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 1
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.Size = New Size(1055, 435)
        TableLayoutPanel1.TabIndex = 0
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(btnBuscarProveedor)
        Panel1.Controls.Add(Label2)
        Panel1.Controls.Add(Label1)
        Panel1.Controls.Add(txtCuit)
        Panel1.Controls.Add(dtpFecha)
        Panel1.Controls.Add(cmbComprobante)
        Panel1.Controls.Add(chkDolar)
        Panel1.Controls.Add(lblDolar)
        Panel1.Controls.Add(txtDolar)
        Panel1.Controls.Add(lblCAI)
        Panel1.Controls.Add(txtCAI)
        Panel1.Controls.Add(lblNroComprobante)
        Panel1.Controls.Add(txtNroComprobante)
        Panel1.Controls.Add(lblFondoFijo)
        Panel1.Controls.Add(txtFondoFijo)
        Panel1.Controls.Add(lblComprobante)
        Panel1.Controls.Add(lblFecha)
        Panel1.Controls.Add(lblDespacho)
        Panel1.Controls.Add(txtDespacho)
        Panel1.Controls.Add(lblNroFactura)
        Panel1.Controls.Add(txtNroFactura)
        Panel1.Controls.Add(lblPuntoVenta)
        Panel1.Controls.Add(txtPuntoVenta)
        Panel1.Controls.Add(cmbProveedor)
        Panel1.Controls.Add(lblProveedor)
        Panel1.Controls.Add(cmbSucursal)
        Panel1.Controls.Add(txtNroCuenta)
        Panel1.Controls.Add(lblSucursal)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(3, 3)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(352, 429)
        Panel1.TabIndex = 0
        ' 
        ' btnBuscarProveedor
        ' 
        btnBuscarProveedor.Anchor = AnchorStyles.Right
        btnBuscarProveedor.Cursor = Cursors.Hand
        btnBuscarProveedor.FlatStyle = FlatStyle.Flat
        btnBuscarProveedor.Location = New Point(293, 54)
        btnBuscarProveedor.Name = "btnBuscarProveedor"
        btnBuscarProveedor.Size = New Size(56, 23)
        btnBuscarProveedor.TabIndex = 106
        btnBuscarProveedor.Text = "Buscar"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(5, 115)
        Label2.Name = "Label2"
        Label2.Size = New Size(29, 15)
        Label2.TabIndex = 28
        Label2.Text = "Cuit"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(5, 86)
        Label1.Name = "Label1"
        Label1.Size = New Size(45, 15)
        Label1.TabIndex = 27
        Label1.Text = "Cuenta"
        ' 
        ' txtCuit
        ' 
        txtCuit.Enabled = False
        txtCuit.Location = New Point(92, 112)
        txtCuit.Name = "txtCuit"
        txtCuit.Size = New Size(151, 23)
        txtCuit.TabIndex = 26
        ' 
        ' dtpFecha
        ' 
        dtpFecha.Format = DateTimePickerFormat.Short
        dtpFecha.Location = New Point(92, 286)
        dtpFecha.Name = "dtpFecha"
        dtpFecha.Size = New Size(107, 23)
        dtpFecha.TabIndex = 25
        ' 
        ' cmbComprobante
        ' 
        cmbComprobante.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        cmbComprobante.FormattingEnabled = True
        cmbComprobante.Location = New Point(92, 315)
        cmbComprobante.Name = "cmbComprobante"
        cmbComprobante.Size = New Size(257, 23)
        cmbComprobante.TabIndex = 24
        ' 
        ' chkDolar
        ' 
        chkDolar.AutoSize = True
        chkDolar.Location = New Point(71, 377)
        chkDolar.Name = "chkDolar"
        chkDolar.Size = New Size(15, 14)
        chkDolar.TabIndex = 23
        chkDolar.UseVisualStyleBackColor = True
        ' 
        ' lblDolar
        ' 
        lblDolar.AutoSize = True
        lblDolar.Location = New Point(5, 376)
        lblDolar.Name = "lblDolar"
        lblDolar.Size = New Size(35, 15)
        lblDolar.TabIndex = 22
        lblDolar.Text = "Dolar"
        ' 
        ' txtDolar
        ' 
        txtDolar.Location = New Point(92, 373)
        txtDolar.Name = "txtDolar"
        txtDolar.Size = New Size(107, 23)
        txtDolar.TabIndex = 21
        txtDolar.TextAlign = HorizontalAlignment.Right
        ' 
        ' lblCAI
        ' 
        lblCAI.AutoSize = True
        lblCAI.Location = New Point(5, 347)
        lblCAI.Name = "lblCAI"
        lblCAI.Size = New Size(35, 15)
        lblCAI.TabIndex = 20
        lblCAI.Text = "C.A.I."
        ' 
        ' txtCAI
        ' 
        txtCAI.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtCAI.Location = New Point(92, 344)
        txtCAI.Name = "txtCAI"
        txtCAI.Size = New Size(257, 23)
        txtCAI.TabIndex = 19
        ' 
        ' lblNroComprobante
        ' 
        lblNroComprobante.AutoSize = True
        lblNroComprobante.Location = New Point(5, 231)
        lblNroComprobante.Name = "lblNroComprobante"
        lblNroComprobante.Size = New Size(87, 15)
        lblNroComprobante.TabIndex = 18
        lblNroComprobante.Text = "Nro. Comprob."
        ' 
        ' txtNroComprobante
        ' 
        txtNroComprobante.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtNroComprobante.Location = New Point(92, 228)
        txtNroComprobante.Name = "txtNroComprobante"
        txtNroComprobante.Size = New Size(257, 23)
        txtNroComprobante.TabIndex = 17
        ' 
        ' lblFondoFijo
        ' 
        lblFondoFijo.AutoSize = True
        lblFondoFijo.Location = New Point(5, 202)
        lblFondoFijo.Name = "lblFondoFijo"
        lblFondoFijo.Size = New Size(63, 15)
        lblFondoFijo.TabIndex = 16
        lblFondoFijo.Text = "Fondo Fijo"
        ' 
        ' txtFondoFijo
        ' 
        txtFondoFijo.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtFondoFijo.Location = New Point(92, 199)
        txtFondoFijo.Name = "txtFondoFijo"
        txtFondoFijo.Size = New Size(257, 23)
        txtFondoFijo.TabIndex = 15
        ' 
        ' lblComprobante
        ' 
        lblComprobante.AutoSize = True
        lblComprobante.Location = New Point(5, 318)
        lblComprobante.Name = "lblComprobante"
        lblComprobante.Size = New Size(81, 15)
        lblComprobante.TabIndex = 14
        lblComprobante.Text = "Comprobante"
        ' 
        ' lblFecha
        ' 
        lblFecha.AutoSize = True
        lblFecha.Location = New Point(5, 289)
        lblFecha.Name = "lblFecha"
        lblFecha.Size = New Size(38, 15)
        lblFecha.TabIndex = 12
        lblFecha.Text = "Fecha"
        ' 
        ' lblDespacho
        ' 
        lblDespacho.AutoSize = True
        lblDespacho.Location = New Point(5, 260)
        lblDespacho.Name = "lblDespacho"
        lblDespacho.Size = New Size(59, 15)
        lblDespacho.TabIndex = 10
        lblDespacho.Text = "Despacho"
        ' 
        ' txtDespacho
        ' 
        txtDespacho.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtDespacho.Location = New Point(92, 257)
        txtDespacho.Name = "txtDespacho"
        txtDespacho.Size = New Size(257, 23)
        txtDespacho.TabIndex = 9
        ' 
        ' lblNroFactura
        ' 
        lblNroFactura.AutoSize = True
        lblNroFactura.Location = New Point(5, 173)
        lblNroFactura.Name = "lblNroFactura"
        lblNroFactura.Size = New Size(72, 15)
        lblNroFactura.TabIndex = 8
        lblNroFactura.Text = "Nro. Factura"
        ' 
        ' txtNroFactura
        ' 
        txtNroFactura.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtNroFactura.Location = New Point(92, 170)
        txtNroFactura.Name = "txtNroFactura"
        txtNroFactura.Size = New Size(257, 23)
        txtNroFactura.TabIndex = 7
        ' 
        ' lblPuntoVenta
        ' 
        lblPuntoVenta.AutoSize = True
        lblPuntoVenta.Location = New Point(5, 144)
        lblPuntoVenta.Name = "lblPuntoVenta"
        lblPuntoVenta.Size = New Size(71, 15)
        lblPuntoVenta.TabIndex = 6
        lblPuntoVenta.Text = "Punto Venta"
        ' 
        ' txtPuntoVenta
        ' 
        txtPuntoVenta.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtPuntoVenta.Location = New Point(92, 141)
        txtPuntoVenta.Name = "txtPuntoVenta"
        txtPuntoVenta.Size = New Size(257, 23)
        txtPuntoVenta.TabIndex = 5
        ' 
        ' cmbProveedor
        ' 
        cmbProveedor.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        cmbProveedor.FormattingEnabled = True
        cmbProveedor.Location = New Point(92, 54)
        cmbProveedor.Name = "cmbProveedor"
        cmbProveedor.Size = New Size(195, 23)
        cmbProveedor.TabIndex = 4
        ' 
        ' lblProveedor
        ' 
        lblProveedor.AutoSize = True
        lblProveedor.Location = New Point(5, 57)
        lblProveedor.Name = "lblProveedor"
        lblProveedor.Size = New Size(61, 15)
        lblProveedor.TabIndex = 3
        lblProveedor.Text = "Proveedor"
        ' 
        ' cmbSucursal
        ' 
        cmbSucursal.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        cmbSucursal.BackColor = Color.White
        cmbSucursal.FormattingEnabled = True
        cmbSucursal.Location = New Point(92, 25)
        cmbSucursal.Name = "cmbSucursal"
        cmbSucursal.Size = New Size(257, 23)
        cmbSucursal.TabIndex = 2
        ' 
        ' txtNroCuenta
        ' 
        txtNroCuenta.Enabled = False
        txtNroCuenta.Location = New Point(92, 83)
        txtNroCuenta.Name = "txtNroCuenta"
        txtNroCuenta.Size = New Size(107, 23)
        txtNroCuenta.TabIndex = 1
        ' 
        ' lblSucursal
        ' 
        lblSucursal.AutoSize = True
        lblSucursal.Location = New Point(5, 28)
        lblSucursal.Name = "lblSucursal"
        lblSucursal.Size = New Size(51, 15)
        lblSucursal.TabIndex = 0
        lblSucursal.Text = "Sucursal"
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(txtCuentaIngresosBrutos6)
        Panel2.Controls.Add(txtIngresosBrutos6)
        Panel2.Controls.Add(lblIngresosBrutos6)
        Panel2.Controls.Add(txtCuentaIngresosBrutos5)
        Panel2.Controls.Add(txtIngresosBrutos5)
        Panel2.Controls.Add(lblIngresosBrutos5)
        Panel2.Controls.Add(txtCuentaIngresosBrutos4)
        Panel2.Controls.Add(txtIngresosBrutos4)
        Panel2.Controls.Add(lblIngresosBrutos4)
        Panel2.Controls.Add(txtCuentaIngresosBrutos3)
        Panel2.Controls.Add(txtIngresosBrutos3)
        Panel2.Controls.Add(lblIngresosBrutos3)
        Panel2.Controls.Add(txtCuentaIngresosBrutos2)
        Panel2.Controls.Add(txtIngresosBrutos2)
        Panel2.Controls.Add(lblIngresosBrutos2)
        Panel2.Controls.Add(txtCuentaIngresosBrutos1)
        Panel2.Controls.Add(txtIngresosBrutos1)
        Panel2.Controls.Add(lblIngresosBrutos1)
        Panel2.Controls.Add(txtCuentaRetPerIVA)
        Panel2.Controls.Add(txtRetPerIVA)
        Panel2.Controls.Add(lblRetPerIVA)
        Panel2.Controls.Add(txtCuentaGanancia)
        Panel2.Controls.Add(txtGanancia)
        Panel2.Controls.Add(lblGanancia)
        Panel2.Controls.Add(txtCuentaIVA)
        Panel2.Controls.Add(txtIVA)
        Panel2.Controls.Add(lblIVA)
        Panel2.Controls.Add(txtCuentaExentos)
        Panel2.Controls.Add(txtExentos)
        Panel2.Controls.Add(lblExentos)
        Panel2.Controls.Add(txtCuentaNGrav27)
        Panel2.Controls.Add(txtNGrav27)
        Panel2.Controls.Add(lblNGrav27)
        Panel2.Controls.Add(txtCuentaNGrav21)
        Panel2.Controls.Add(txtNGrav21)
        Panel2.Controls.Add(lblNGrav21)
        Panel2.Controls.Add(txtCuentaNGrav105)
        Panel2.Controls.Add(txtNGrav105)
        Panel2.Controls.Add(lblNGrav105)
        Panel2.Controls.Add(Label12)
        Panel2.Controls.Add(txtCuentaComprasRNI)
        Panel2.Controls.Add(txtComprasRNI)
        Panel2.Controls.Add(lblComprasRNI)
        Panel2.Dock = DockStyle.Fill
        Panel2.Location = New Point(361, 3)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(342, 429)
        Panel2.TabIndex = 1
        ' 
        ' txtCuentaIngresosBrutos6
        ' 
        txtCuentaIngresosBrutos6.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        txtCuentaIngresosBrutos6.Location = New Point(263, 402)
        txtCuentaIngresosBrutos6.Name = "txtCuentaIngresosBrutos6"
        txtCuentaIngresosBrutos6.Size = New Size(76, 23)
        txtCuentaIngresosBrutos6.TabIndex = 67
        ' 
        ' txtIngresosBrutos6
        ' 
        txtIngresosBrutos6.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtIngresosBrutos6.Location = New Point(89, 402)
        txtIngresosBrutos6.Name = "txtIngresosBrutos6"
        txtIngresosBrutos6.Size = New Size(168, 23)
        txtIngresosBrutos6.TabIndex = 66
        txtIngresosBrutos6.TextAlign = HorizontalAlignment.Right
        ' 
        ' lblIngresosBrutos6
        ' 
        lblIngresosBrutos6.AutoSize = True
        lblIngresosBrutos6.Location = New Point(3, 405)
        lblIngresosBrutos6.Name = "lblIngresosBrutos6"
        lblIngresosBrutos6.Size = New Size(27, 15)
        lblIngresosBrutos6.TabIndex = 65
        lblIngresosBrutos6.Text = "IIBB"
        ' 
        ' txtCuentaIngresosBrutos5
        ' 
        txtCuentaIngresosBrutos5.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        txtCuentaIngresosBrutos5.Location = New Point(263, 373)
        txtCuentaIngresosBrutos5.Name = "txtCuentaIngresosBrutos5"
        txtCuentaIngresosBrutos5.Size = New Size(76, 23)
        txtCuentaIngresosBrutos5.TabIndex = 64
        ' 
        ' txtIngresosBrutos5
        ' 
        txtIngresosBrutos5.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtIngresosBrutos5.Location = New Point(89, 373)
        txtIngresosBrutos5.Name = "txtIngresosBrutos5"
        txtIngresosBrutos5.Size = New Size(168, 23)
        txtIngresosBrutos5.TabIndex = 63
        txtIngresosBrutos5.TextAlign = HorizontalAlignment.Right
        ' 
        ' lblIngresosBrutos5
        ' 
        lblIngresosBrutos5.AutoSize = True
        lblIngresosBrutos5.Location = New Point(3, 376)
        lblIngresosBrutos5.Name = "lblIngresosBrutos5"
        lblIngresosBrutos5.Size = New Size(27, 15)
        lblIngresosBrutos5.TabIndex = 62
        lblIngresosBrutos5.Text = "IIBB"
        ' 
        ' txtCuentaIngresosBrutos4
        ' 
        txtCuentaIngresosBrutos4.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        txtCuentaIngresosBrutos4.Location = New Point(263, 344)
        txtCuentaIngresosBrutos4.Name = "txtCuentaIngresosBrutos4"
        txtCuentaIngresosBrutos4.Size = New Size(76, 23)
        txtCuentaIngresosBrutos4.TabIndex = 61
        ' 
        ' txtIngresosBrutos4
        ' 
        txtIngresosBrutos4.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtIngresosBrutos4.Location = New Point(89, 344)
        txtIngresosBrutos4.Name = "txtIngresosBrutos4"
        txtIngresosBrutos4.Size = New Size(168, 23)
        txtIngresosBrutos4.TabIndex = 60
        txtIngresosBrutos4.TextAlign = HorizontalAlignment.Right
        ' 
        ' lblIngresosBrutos4
        ' 
        lblIngresosBrutos4.AutoSize = True
        lblIngresosBrutos4.Location = New Point(3, 347)
        lblIngresosBrutos4.Name = "lblIngresosBrutos4"
        lblIngresosBrutos4.Size = New Size(27, 15)
        lblIngresosBrutos4.TabIndex = 59
        lblIngresosBrutos4.Text = "IIBB"
        ' 
        ' txtCuentaIngresosBrutos3
        ' 
        txtCuentaIngresosBrutos3.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        txtCuentaIngresosBrutos3.Location = New Point(263, 315)
        txtCuentaIngresosBrutos3.Name = "txtCuentaIngresosBrutos3"
        txtCuentaIngresosBrutos3.Size = New Size(76, 23)
        txtCuentaIngresosBrutos3.TabIndex = 58
        ' 
        ' txtIngresosBrutos3
        ' 
        txtIngresosBrutos3.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtIngresosBrutos3.Location = New Point(89, 315)
        txtIngresosBrutos3.Name = "txtIngresosBrutos3"
        txtIngresosBrutos3.Size = New Size(168, 23)
        txtIngresosBrutos3.TabIndex = 57
        txtIngresosBrutos3.TextAlign = HorizontalAlignment.Right
        ' 
        ' lblIngresosBrutos3
        ' 
        lblIngresosBrutos3.AutoSize = True
        lblIngresosBrutos3.Location = New Point(3, 318)
        lblIngresosBrutos3.Name = "lblIngresosBrutos3"
        lblIngresosBrutos3.Size = New Size(27, 15)
        lblIngresosBrutos3.TabIndex = 56
        lblIngresosBrutos3.Text = "IIBB"
        ' 
        ' txtCuentaIngresosBrutos2
        ' 
        txtCuentaIngresosBrutos2.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        txtCuentaIngresosBrutos2.Location = New Point(263, 286)
        txtCuentaIngresosBrutos2.Name = "txtCuentaIngresosBrutos2"
        txtCuentaIngresosBrutos2.Size = New Size(76, 23)
        txtCuentaIngresosBrutos2.TabIndex = 55
        ' 
        ' txtIngresosBrutos2
        ' 
        txtIngresosBrutos2.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtIngresosBrutos2.Location = New Point(89, 286)
        txtIngresosBrutos2.Name = "txtIngresosBrutos2"
        txtIngresosBrutos2.Size = New Size(168, 23)
        txtIngresosBrutos2.TabIndex = 54
        txtIngresosBrutos2.TextAlign = HorizontalAlignment.Right
        ' 
        ' lblIngresosBrutos2
        ' 
        lblIngresosBrutos2.AutoSize = True
        lblIngresosBrutos2.Location = New Point(3, 289)
        lblIngresosBrutos2.Name = "lblIngresosBrutos2"
        lblIngresosBrutos2.Size = New Size(27, 15)
        lblIngresosBrutos2.TabIndex = 53
        lblIngresosBrutos2.Text = "IIBB"
        ' 
        ' txtCuentaIngresosBrutos1
        ' 
        txtCuentaIngresosBrutos1.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        txtCuentaIngresosBrutos1.Location = New Point(263, 257)
        txtCuentaIngresosBrutos1.Name = "txtCuentaIngresosBrutos1"
        txtCuentaIngresosBrutos1.Size = New Size(76, 23)
        txtCuentaIngresosBrutos1.TabIndex = 52
        ' 
        ' txtIngresosBrutos1
        ' 
        txtIngresosBrutos1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtIngresosBrutos1.Location = New Point(89, 257)
        txtIngresosBrutos1.Name = "txtIngresosBrutos1"
        txtIngresosBrutos1.Size = New Size(168, 23)
        txtIngresosBrutos1.TabIndex = 51
        txtIngresosBrutos1.TextAlign = HorizontalAlignment.Right
        ' 
        ' lblIngresosBrutos1
        ' 
        lblIngresosBrutos1.AutoSize = True
        lblIngresosBrutos1.Location = New Point(3, 260)
        lblIngresosBrutos1.Name = "lblIngresosBrutos1"
        lblIngresosBrutos1.Size = New Size(27, 15)
        lblIngresosBrutos1.TabIndex = 50
        lblIngresosBrutos1.Text = "IIBB"
        ' 
        ' txtCuentaRetPerIVA
        ' 
        txtCuentaRetPerIVA.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        txtCuentaRetPerIVA.Location = New Point(263, 228)
        txtCuentaRetPerIVA.Name = "txtCuentaRetPerIVA"
        txtCuentaRetPerIVA.Size = New Size(76, 23)
        txtCuentaRetPerIVA.TabIndex = 49
        ' 
        ' txtRetPerIVA
        ' 
        txtRetPerIVA.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtRetPerIVA.Location = New Point(89, 228)
        txtRetPerIVA.Name = "txtRetPerIVA"
        txtRetPerIVA.Size = New Size(168, 23)
        txtRetPerIVA.TabIndex = 48
        txtRetPerIVA.TextAlign = HorizontalAlignment.Right
        ' 
        ' lblRetPerIVA
        ' 
        lblRetPerIVA.AutoSize = True
        lblRetPerIVA.Location = New Point(3, 231)
        lblRetPerIVA.Name = "lblRetPerIVA"
        lblRetPerIVA.Size = New Size(70, 15)
        lblRetPerIVA.TabIndex = 47
        lblRetPerIVA.Text = "Ret. Per. IVA"
        ' 
        ' txtCuentaGanancia
        ' 
        txtCuentaGanancia.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        txtCuentaGanancia.Location = New Point(263, 199)
        txtCuentaGanancia.Name = "txtCuentaGanancia"
        txtCuentaGanancia.Size = New Size(76, 23)
        txtCuentaGanancia.TabIndex = 46
        ' 
        ' txtGanancia
        ' 
        txtGanancia.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtGanancia.Location = New Point(89, 199)
        txtGanancia.Name = "txtGanancia"
        txtGanancia.Size = New Size(168, 23)
        txtGanancia.TabIndex = 45
        txtGanancia.TextAlign = HorizontalAlignment.Right
        ' 
        ' lblGanancia
        ' 
        lblGanancia.AutoSize = True
        lblGanancia.Location = New Point(3, 202)
        lblGanancia.Name = "lblGanancia"
        lblGanancia.Size = New Size(56, 15)
        lblGanancia.TabIndex = 44
        lblGanancia.Text = "Ganancia"
        ' 
        ' txtCuentaIVA
        ' 
        txtCuentaIVA.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        txtCuentaIVA.Location = New Point(263, 170)
        txtCuentaIVA.Name = "txtCuentaIVA"
        txtCuentaIVA.Size = New Size(76, 23)
        txtCuentaIVA.TabIndex = 43
        ' 
        ' txtIVA
        ' 
        txtIVA.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtIVA.Location = New Point(89, 170)
        txtIVA.Name = "txtIVA"
        txtIVA.Size = New Size(168, 23)
        txtIVA.TabIndex = 42
        txtIVA.TextAlign = HorizontalAlignment.Right
        ' 
        ' lblIVA
        ' 
        lblIVA.AutoSize = True
        lblIVA.Location = New Point(3, 173)
        lblIVA.Name = "lblIVA"
        lblIVA.Size = New Size(24, 15)
        lblIVA.TabIndex = 41
        lblIVA.Text = "IVA"
        ' 
        ' txtCuentaExentos
        ' 
        txtCuentaExentos.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        txtCuentaExentos.Location = New Point(263, 141)
        txtCuentaExentos.Name = "txtCuentaExentos"
        txtCuentaExentos.Size = New Size(76, 23)
        txtCuentaExentos.TabIndex = 40
        ' 
        ' txtExentos
        ' 
        txtExentos.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtExentos.Location = New Point(89, 141)
        txtExentos.Name = "txtExentos"
        txtExentos.Size = New Size(168, 23)
        txtExentos.TabIndex = 39
        txtExentos.TextAlign = HorizontalAlignment.Right
        ' 
        ' lblExentos
        ' 
        lblExentos.AutoSize = True
        lblExentos.Location = New Point(3, 144)
        lblExentos.Name = "lblExentos"
        lblExentos.Size = New Size(47, 15)
        lblExentos.TabIndex = 38
        lblExentos.Text = "Exentos"
        ' 
        ' txtCuentaNGrav27
        ' 
        txtCuentaNGrav27.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        txtCuentaNGrav27.Location = New Point(263, 112)
        txtCuentaNGrav27.Name = "txtCuentaNGrav27"
        txtCuentaNGrav27.Size = New Size(76, 23)
        txtCuentaNGrav27.TabIndex = 37
        ' 
        ' txtNGrav27
        ' 
        txtNGrav27.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtNGrav27.Location = New Point(89, 112)
        txtNGrav27.Name = "txtNGrav27"
        txtNGrav27.Size = New Size(168, 23)
        txtNGrav27.TabIndex = 36
        txtNGrav27.TextAlign = HorizontalAlignment.Right
        ' 
        ' lblNGrav27
        ' 
        lblNGrav27.AutoSize = True
        lblNGrav27.Location = New Point(3, 115)
        lblNGrav27.Name = "lblNGrav27"
        lblNGrav27.Size = New Size(61, 15)
        lblNGrav27.TabIndex = 35
        lblNGrav27.Text = "N. Grav 27"
        ' 
        ' txtCuentaNGrav21
        ' 
        txtCuentaNGrav21.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        txtCuentaNGrav21.Location = New Point(263, 83)
        txtCuentaNGrav21.Name = "txtCuentaNGrav21"
        txtCuentaNGrav21.Size = New Size(76, 23)
        txtCuentaNGrav21.TabIndex = 34
        ' 
        ' txtNGrav21
        ' 
        txtNGrav21.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtNGrav21.Location = New Point(89, 83)
        txtNGrav21.Name = "txtNGrav21"
        txtNGrav21.Size = New Size(168, 23)
        txtNGrav21.TabIndex = 33
        txtNGrav21.TextAlign = HorizontalAlignment.Right
        ' 
        ' lblNGrav21
        ' 
        lblNGrav21.AutoSize = True
        lblNGrav21.Location = New Point(3, 86)
        lblNGrav21.Name = "lblNGrav21"
        lblNGrav21.Size = New Size(64, 15)
        lblNGrav21.TabIndex = 32
        lblNGrav21.Text = "N. Grav. 21"
        ' 
        ' txtCuentaNGrav105
        ' 
        txtCuentaNGrav105.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        txtCuentaNGrav105.Location = New Point(263, 54)
        txtCuentaNGrav105.Name = "txtCuentaNGrav105"
        txtCuentaNGrav105.Size = New Size(76, 23)
        txtCuentaNGrav105.TabIndex = 31
        ' 
        ' txtNGrav105
        ' 
        txtNGrav105.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtNGrav105.Location = New Point(89, 54)
        txtNGrav105.Name = "txtNGrav105"
        txtNGrav105.Size = New Size(168, 23)
        txtNGrav105.TabIndex = 30
        txtNGrav105.TextAlign = HorizontalAlignment.Right
        ' 
        ' lblNGrav105
        ' 
        lblNGrav105.AutoSize = True
        lblNGrav105.Location = New Point(3, 57)
        lblNGrav105.Name = "lblNGrav105"
        lblNGrav105.Size = New Size(73, 15)
        lblNGrav105.TabIndex = 29
        lblNGrav105.Text = "N. Grav. 10,5"
        ' 
        ' Label12
        ' 
        Label12.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Label12.AutoSize = True
        Label12.Location = New Point(177, 7)
        Label12.Name = "Label12"
        Label12.Size = New Size(80, 15)
        Label12.TabIndex = 28
        Label12.Text = "Cuentas DEBE"
        ' 
        ' txtCuentaComprasRNI
        ' 
        txtCuentaComprasRNI.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        txtCuentaComprasRNI.Location = New Point(263, 25)
        txtCuentaComprasRNI.Name = "txtCuentaComprasRNI"
        txtCuentaComprasRNI.Size = New Size(76, 23)
        txtCuentaComprasRNI.TabIndex = 27
        ' 
        ' txtComprasRNI
        ' 
        txtComprasRNI.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtComprasRNI.Location = New Point(89, 25)
        txtComprasRNI.Name = "txtComprasRNI"
        txtComprasRNI.Size = New Size(168, 23)
        txtComprasRNI.TabIndex = 26
        txtComprasRNI.TextAlign = HorizontalAlignment.Right
        ' 
        ' lblComprasRNI
        ' 
        lblComprasRNI.AutoSize = True
        lblComprasRNI.Location = New Point(3, 28)
        lblComprasRNI.Name = "lblComprasRNI"
        lblComprasRNI.Size = New Size(77, 15)
        lblComprasRNI.TabIndex = 1
        lblComprasRNI.Text = "Compras RNI"
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(btnConsultarCuenta)
        Panel3.Controls.Add(cmbCuentaMonto3)
        Panel3.Controls.Add(cmbCuentaMonto2)
        Panel3.Controls.Add(cmbCuentaMonto1)
        Panel3.Controls.Add(boxPlanCuentas)
        Panel3.Controls.Add(txtComentario)
        Panel3.Controls.Add(lblComentario)
        Panel3.Controls.Add(chkNoGeneraAsiento)
        Panel3.Controls.Add(txtMonto3)
        Panel3.Controls.Add(lblMonto)
        Panel3.Controls.Add(txtMonto1)
        Panel3.Controls.Add(Label26)
        Panel3.Controls.Add(txtMonto2)
        Panel3.Dock = DockStyle.Fill
        Panel3.Location = New Point(709, 3)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(343, 429)
        Panel3.TabIndex = 2
        ' 
        ' btnConsultarCuenta
        ' 
        btnConsultarCuenta.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        btnConsultarCuenta.FlatStyle = FlatStyle.Flat
        btnConsultarCuenta.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnConsultarCuenta.Location = New Point(52, 231)
        btnConsultarCuenta.Name = "btnConsultarCuenta"
        btnConsultarCuenta.Size = New Size(252, 30)
        btnConsultarCuenta.TabIndex = 106
        btnConsultarCuenta.Text = "Consultar Plan de Cuentas"
        ' 
        ' cmbCuentaMonto3
        ' 
        cmbCuentaMonto3.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        cmbCuentaMonto3.FormattingEnabled = True
        cmbCuentaMonto3.Location = New Point(264, 83)
        cmbCuentaMonto3.Name = "cmbCuentaMonto3"
        cmbCuentaMonto3.Size = New Size(76, 23)
        cmbCuentaMonto3.TabIndex = 108
        ' 
        ' cmbCuentaMonto2
        ' 
        cmbCuentaMonto2.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        cmbCuentaMonto2.FormattingEnabled = True
        cmbCuentaMonto2.Location = New Point(264, 54)
        cmbCuentaMonto2.Name = "cmbCuentaMonto2"
        cmbCuentaMonto2.Size = New Size(76, 23)
        cmbCuentaMonto2.TabIndex = 107
        ' 
        ' cmbCuentaMonto1
        ' 
        cmbCuentaMonto1.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        cmbCuentaMonto1.FormattingEnabled = True
        cmbCuentaMonto1.Location = New Point(264, 25)
        cmbCuentaMonto1.Name = "cmbCuentaMonto1"
        cmbCuentaMonto1.Size = New Size(76, 23)
        cmbCuentaMonto1.TabIndex = 26
        ' 
        ' boxPlanCuentas
        ' 
        boxPlanCuentas.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        boxPlanCuentas.Controls.Add(dgvListadoCuenta)
        boxPlanCuentas.Controls.Add(txtBuscarCuenta)
        boxPlanCuentas.Location = New Point(4, 156)
        boxPlanCuentas.Name = "boxPlanCuentas"
        boxPlanCuentas.Size = New Size(335, 211)
        boxPlanCuentas.TabIndex = 73
        boxPlanCuentas.TabStop = False
        boxPlanCuentas.Visible = False
        ' 
        ' dgvListadoCuenta
        ' 
        dgvListadoCuenta.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvListadoCuenta.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvListadoCuenta.Location = New Point(0, 31)
        dgvListadoCuenta.Name = "dgvListadoCuenta"
        dgvListadoCuenta.ReadOnly = True
        dgvListadoCuenta.Size = New Size(335, 177)
        dgvListadoCuenta.TabIndex = 27
        ' 
        ' txtBuscarCuenta
        ' 
        txtBuscarCuenta.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtBuscarCuenta.Location = New Point(0, 8)
        txtBuscarCuenta.Name = "txtBuscarCuenta"
        txtBuscarCuenta.PlaceholderText = "Buscar..."
        txtBuscarCuenta.Size = New Size(335, 23)
        txtBuscarCuenta.TabIndex = 26
        ' 
        ' txtComentario
        ' 
        txtComentario.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtComentario.Location = New Point(4, 402)
        txtComentario.Name = "txtComentario"
        txtComentario.Size = New Size(336, 23)
        txtComentario.TabIndex = 63
        ' 
        ' lblComentario
        ' 
        lblComentario.AutoSize = True
        lblComentario.Location = New Point(5, 381)
        lblComentario.Name = "lblComentario"
        lblComentario.Size = New Size(70, 15)
        lblComentario.TabIndex = 62
        lblComentario.Text = "Comentario"
        ' 
        ' chkNoGeneraAsiento
        ' 
        chkNoGeneraAsiento.AutoSize = True
        chkNoGeneraAsiento.Location = New Point(90, 114)
        chkNoGeneraAsiento.Name = "chkNoGeneraAsiento"
        chkNoGeneraAsiento.RightToLeft = RightToLeft.Yes
        chkNoGeneraAsiento.Size = New Size(127, 19)
        chkNoGeneraAsiento.TabIndex = 72
        chkNoGeneraAsiento.Text = "NO Genera Asiento"
        ' 
        ' txtMonto3
        ' 
        txtMonto3.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtMonto3.Location = New Point(90, 83)
        txtMonto3.Name = "txtMonto3"
        txtMonto3.Size = New Size(168, 23)
        txtMonto3.TabIndex = 70
        txtMonto3.TextAlign = HorizontalAlignment.Right
        ' 
        ' lblMonto
        ' 
        lblMonto.AutoSize = True
        lblMonto.Location = New Point(4, 28)
        lblMonto.Name = "lblMonto"
        lblMonto.Size = New Size(43, 15)
        lblMonto.TabIndex = 62
        lblMonto.Text = "Monto"
        ' 
        ' txtMonto1
        ' 
        txtMonto1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtMonto1.Location = New Point(90, 25)
        txtMonto1.Name = "txtMonto1"
        txtMonto1.Size = New Size(168, 23)
        txtMonto1.TabIndex = 63
        txtMonto1.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label26
        ' 
        Label26.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Label26.AutoSize = True
        Label26.Location = New Point(168, 7)
        Label26.Name = "Label26"
        Label26.Size = New Size(90, 15)
        Label26.TabIndex = 65
        Label26.Text = "Cuentas HABER"
        ' 
        ' txtMonto2
        ' 
        txtMonto2.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtMonto2.Location = New Point(90, 54)
        txtMonto2.Name = "txtMonto2"
        txtMonto2.Size = New Size(168, 23)
        txtMonto2.TabIndex = 67
        txtMonto2.TextAlign = HorizontalAlignment.Right
        ' 
        ' lnkCopiar
        ' 
        lnkCopiar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        lnkCopiar.AutoSize = True
        lnkCopiar.Location = New Point(830, 197)
        lnkCopiar.Name = "lnkCopiar"
        lnkCopiar.Size = New Size(97, 15)
        lnkCopiar.TabIndex = 7
        lnkCopiar.TabStop = True
        lnkCopiar.Text = "Copiar selección:"
        ' 
        ' chkEncabezados
        ' 
        chkEncabezados.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        chkEncabezados.AutoSize = True
        chkEncabezados.Location = New Point(933, 197)
        chkEncabezados.Name = "chkEncabezados"
        chkEncabezados.Size = New Size(119, 19)
        chkEncabezados.TabIndex = 8
        chkEncabezados.Text = "Con encabezados"
        ' 
        ' CmdSalir
        ' 
        CmdSalir.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        CmdSalir.BackColor = Color.IndianRed
        CmdSalir.Cursor = Cursors.Hand
        CmdSalir.FlatStyle = FlatStyle.Flat
        CmdSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdSalir.ForeColor = Color.White
        CmdSalir.Location = New Point(954, 663)
        CmdSalir.Margin = New Padding(4, 3, 4, 3)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(88, 30)
        CmdSalir.TabIndex = 105
        CmdSalir.Text = "Salir"
        CmdSalir.UseVisualStyleBackColor = False
        ' 
        ' CmdAgregar
        ' 
        CmdAgregar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        CmdAgregar.Cursor = Cursors.Hand
        CmdAgregar.FlatStyle = FlatStyle.Flat
        CmdAgregar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdAgregar.Location = New Point(483, 663)
        CmdAgregar.Name = "CmdAgregar"
        CmdAgregar.Size = New Size(88, 30)
        CmdAgregar.TabIndex = 101
        CmdAgregar.Text = "Agregar"
        ' 
        ' CmdModificar
        ' 
        CmdModificar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        CmdModificar.Cursor = Cursors.Hand
        CmdModificar.FlatStyle = FlatStyle.Flat
        CmdModificar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdModificar.Location = New Point(577, 663)
        CmdModificar.Name = "CmdModificar"
        CmdModificar.Size = New Size(88, 30)
        CmdModificar.TabIndex = 102
        CmdModificar.Text = "Modificar"
        ' 
        ' CmdBorrar
        ' 
        CmdBorrar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        CmdBorrar.Cursor = Cursors.Hand
        CmdBorrar.FlatStyle = FlatStyle.Flat
        CmdBorrar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdBorrar.Location = New Point(671, 663)
        CmdBorrar.Name = "CmdBorrar"
        CmdBorrar.Size = New Size(88, 30)
        CmdBorrar.TabIndex = 103
        CmdBorrar.Text = "Borrar"
        ' 
        ' cmdAceptar
        ' 
        cmdAceptar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        cmdAceptar.Cursor = Cursors.Hand
        cmdAceptar.FlatStyle = FlatStyle.Flat
        cmdAceptar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdAceptar.Location = New Point(765, 663)
        cmdAceptar.Name = "cmdAceptar"
        cmdAceptar.Size = New Size(88, 30)
        cmdAceptar.TabIndex = 22
        cmdAceptar.Text = "Aceptar"
        ' 
        ' CmdCancelar
        ' 
        CmdCancelar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        CmdCancelar.Cursor = Cursors.Hand
        CmdCancelar.FlatStyle = FlatStyle.Flat
        CmdCancelar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdCancelar.Location = New Point(859, 663)
        CmdCancelar.Name = "CmdCancelar"
        CmdCancelar.Size = New Size(88, 30)
        CmdCancelar.TabIndex = 23
        CmdCancelar.Text = "Cancelar"
        ' 
        ' frmNoveProveedores
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1055, 705)
        Controls.Add(lnkCopiar)
        Controls.Add(chkEncabezados)
        Controls.Add(grpEdicion)
        Controls.Add(DgvListado)
        Controls.Add(pnlTop)
        Controls.Add(CmdSalir)
        Controls.Add(CmdCancelar)
        Controls.Add(cmdAceptar)
        Controls.Add(CmdBorrar)
        Controls.Add(CmdAgregar)
        Controls.Add(CmdModificar)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MinimizeBox = False
        MinimumSize = New Size(929, 653)
        Name = "frmNoveProveedores"
        Text = "Novedades Proveedores"
        pnlTop.ResumeLayout(False)
        pnlTop.PerformLayout()
        CType(DgvListado, ComponentModel.ISupportInitialize).EndInit()
        grpEdicion.ResumeLayout(False)
        TableLayoutPanel1.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        boxPlanCuentas.ResumeLayout(False)
        boxPlanCuentas.PerformLayout()
        CType(dgvListadoCuenta, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    ' Controles
    Friend WithEvents pnlTop As Panel
    Friend WithEvents lblBuscar As Label
    Friend WithEvents TxtBuscar As TextBox

    Friend WithEvents DgvListado As DataGridView
    Friend WithEvents grpEdicion As GroupBox
    Friend WithEvents chkMarcado As CheckBox
    Friend WithEvents cmdAceptar As Button
    Friend WithEvents CmdCancelar As Button
    Friend WithEvents CmdAgregar As Button
    Friend WithEvents CmdModificar As Button
    Friend WithEvents CmdBorrar As Button
    Friend WithEvents CmdSalir As Button
    Friend WithEvents lnkCopiar As LinkLabel
    Friend WithEvents chkEncabezados As CheckBox
    Friend WithEvents lblFechaHasta As Label
    Friend WithEvents dtpFechaHasta As DateTimePicker
    Friend WithEvents lblFechaDesde As Label
    Friend WithEvents dtpFechaDesde As DateTimePicker
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents lblComprobante As Label
    Friend WithEvents lblFecha As Label
    Friend WithEvents lblDespacho As Label
    Friend WithEvents txtDespacho As TextBox
    Friend WithEvents lblNroFactura As Label
    Friend WithEvents txtNroFactura As TextBox
    Friend WithEvents lblPuntoVenta As Label
    Friend WithEvents txtPuntoVenta As TextBox
    Friend WithEvents cmbProveedor As ComboBox
    Friend WithEvents lblProveedor As Label
    Friend WithEvents cmbSucursal As ComboBox
    Friend WithEvents txtNroCuenta As TextBox
    Friend WithEvents lblSucursal As Label
    Friend WithEvents dtpFecha As DateTimePicker
    Friend WithEvents cmbComprobante As ComboBox
    Friend WithEvents chkDolar As CheckBox
    Friend WithEvents lblDolar As Label
    Friend WithEvents txtDolar As TextBox
    Friend WithEvents lblCAI As Label
    Friend WithEvents txtCAI As TextBox
    Friend WithEvents lblNroComprobante As Label
    Friend WithEvents txtNroComprobante As TextBox
    Friend WithEvents lblFondoFijo As Label
    Friend WithEvents txtFondoFijo As TextBox
    Friend WithEvents txtCuentaComprasRNI As TextBox
    Friend WithEvents txtComprasRNI As TextBox
    Friend WithEvents lblComprasRNI As Label
    Friend WithEvents txtCuentaIngresosBrutos4 As TextBox
    Friend WithEvents txtIngresosBrutos4 As TextBox
    Friend WithEvents lblIngresosBrutos4 As Label
    Friend WithEvents txtCuentaIngresosBrutos3 As TextBox
    Friend WithEvents txtIngresosBrutos3 As TextBox
    Friend WithEvents lblIngresosBrutos3 As Label
    Friend WithEvents txtCuentaIngresosBrutos2 As TextBox
    Friend WithEvents txtIngresosBrutos2 As TextBox
    Friend WithEvents lblIngresosBrutos2 As Label
    Friend WithEvents txtCuentaIngresosBrutos1 As TextBox
    Friend WithEvents txtIngresosBrutos1 As TextBox
    Friend WithEvents lblIngresosBrutos1 As Label
    Friend WithEvents txtCuentaRetPerIVA As TextBox
    Friend WithEvents txtRetPerIVA As TextBox
    Friend WithEvents lblRetPerIVA As Label
    Friend WithEvents txtCuentaGanancia As TextBox
    Friend WithEvents txtGanancia As TextBox
    Friend WithEvents lblGanancia As Label
    Friend WithEvents txtCuentaIVA As TextBox
    Friend WithEvents txtIVA As TextBox
    Friend WithEvents lblIVA As Label
    Friend WithEvents txtCuentaExentos As TextBox
    Friend WithEvents txtExentos As TextBox
    Friend WithEvents lblExentos As Label
    Friend WithEvents txtCuentaNGrav27 As TextBox
    Friend WithEvents txtNGrav27 As TextBox
    Friend WithEvents lblNGrav27 As Label
    Friend WithEvents txtCuentaNGrav21 As TextBox
    Friend WithEvents txtNGrav21 As TextBox
    Friend WithEvents lblNGrav21 As Label
    Friend WithEvents txtCuentaNGrav105 As TextBox
    Friend WithEvents txtNGrav105 As TextBox
    Friend WithEvents lblNGrav105 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents txtMonto3 As TextBox
    Friend WithEvents lblMonto As Label
    Friend WithEvents txtMonto1 As TextBox
    Friend WithEvents Label26 As Label
    Friend WithEvents txtMonto2 As TextBox
    Friend WithEvents txtComentario As TextBox
    Friend WithEvents lblComentario As Label
    Friend WithEvents chkNoGeneraAsiento As CheckBox
    Friend WithEvents boxPlanCuentas As GroupBox
    Friend WithEvents txtBuscarCuenta As TextBox
    Friend WithEvents dgvListadoCuenta As DataGridView
    Friend WithEvents btnConsultarCuenta As Button
    Friend WithEvents cmbCuentaMonto2 As ComboBox
    Friend WithEvents cmbCuentaMonto1 As ComboBox
    Friend WithEvents cmbCuentaMonto3 As ComboBox
    Friend WithEvents txtCuit As TextBox
    Friend WithEvents txtCuentaIngresosBrutos6 As TextBox
    Friend WithEvents txtIngresosBrutos6 As TextBox
    Friend WithEvents lblIngresosBrutos6 As Label
    Friend WithEvents txtCuentaIngresosBrutos5 As TextBox
    Friend WithEvents txtIngresosBrutos5 As TextBox
    Friend WithEvents lblIngresosBrutos5 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents btnBuscarProveedor As Button
End Class

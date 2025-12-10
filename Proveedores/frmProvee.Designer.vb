<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmProvee
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProvee))
        PanelDatos = New GroupBox()
        btnBuscarCuenta = New Button()
        txtcorreo = New TextBox()
        txtObsv = New TextBox()
        chkExterior = New CheckBox()
        CmbPagos = New ComboBox()
        chkAutoshop = New CheckBox()
        CmbRubros = New ComboBox()
        chkIBrutos = New CheckBox()
        chkLista = New CheckBox()
        chkAsiento = New CheckBox()
        CmbProvincias = New ComboBox()
        txtcbu = New TextBox()
        txtCodContable = New TextBox()
        txtNroCuenta = New TextBox()
        txtSaldoDto = New TextBox()
        txtUltimoResumen = New TextBox()
        txtSaldoActual = New TextBox()
        txtCodigoPostal = New TextBox()
        txtLocalidad = New TextBox()
        txtDepartamento = New TextBox()
        txtCalleNro = New TextBox()
        txtPublico = New TextBox()
        txtPMinimo = New TextBox()
        txtTipo = New TextBox()
        txtNombre = New TextBox()
        CmbIva = New ComboBox()
        CmbJurisdiccion = New ComboBox()
        Label15 = New Label()
        Label3 = New Label()
        Label7 = New Label()
        Label6 = New Label()
        Label22 = New Label()
        Label21 = New Label()
        lblSinDoc = New Label()
        Label16 = New Label()
        Label11 = New Label()
        Label9 = New Label()
        Label8 = New Label()
        Label2 = New Label()
        titulo = New Label()
        Label19 = New Label()
        Label18 = New Label()
        Label17 = New Label()
        Label14 = New Label()
        Label13 = New Label()
        Label12 = New Label()
        Label10 = New Label()
        Label5 = New Label()
        Label4 = New Label()
        Label1 = New Label()
        fraCriterio = New GroupBox()
        txtBuscar = New TextBox()
        Label26 = New Label()
        dgvProveedores = New DataGridView()
        cmdAgregar = New Button()
        cmdBorrar = New Button()
        cmdAceptar = New Button()
        cmdModificar = New Button()
        cmdCancelar = New Button()
        dbcEmpresas = New ComboBox()
        CmdSalir = New Button()
        lnkCopiar = New LinkLabel()
        chkEncabezados = New CheckBox()
        PanelDatos.SuspendLayout()
        fraCriterio.SuspendLayout()
        CType(dgvProveedores, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' PanelDatos
        ' 
        PanelDatos.Controls.Add(btnBuscarCuenta)
        PanelDatos.Controls.Add(txtcorreo)
        PanelDatos.Controls.Add(txtObsv)
        PanelDatos.Controls.Add(chkExterior)
        PanelDatos.Controls.Add(CmbPagos)
        PanelDatos.Controls.Add(chkAutoshop)
        PanelDatos.Controls.Add(CmbRubros)
        PanelDatos.Controls.Add(chkIBrutos)
        PanelDatos.Controls.Add(chkLista)
        PanelDatos.Controls.Add(chkAsiento)
        PanelDatos.Controls.Add(CmbProvincias)
        PanelDatos.Controls.Add(txtcbu)
        PanelDatos.Controls.Add(txtCodContable)
        PanelDatos.Controls.Add(txtNroCuenta)
        PanelDatos.Controls.Add(txtSaldoDto)
        PanelDatos.Controls.Add(txtUltimoResumen)
        PanelDatos.Controls.Add(txtSaldoActual)
        PanelDatos.Controls.Add(txtCodigoPostal)
        PanelDatos.Controls.Add(txtLocalidad)
        PanelDatos.Controls.Add(txtDepartamento)
        PanelDatos.Controls.Add(txtCalleNro)
        PanelDatos.Controls.Add(txtPublico)
        PanelDatos.Controls.Add(txtPMinimo)
        PanelDatos.Controls.Add(txtTipo)
        PanelDatos.Controls.Add(txtNombre)
        PanelDatos.Controls.Add(CmbIva)
        PanelDatos.Controls.Add(CmbJurisdiccion)
        PanelDatos.Controls.Add(Label15)
        PanelDatos.Controls.Add(Label3)
        PanelDatos.Controls.Add(Label7)
        PanelDatos.Controls.Add(Label6)
        PanelDatos.Controls.Add(Label22)
        PanelDatos.Controls.Add(Label21)
        PanelDatos.Controls.Add(lblSinDoc)
        PanelDatos.Controls.Add(Label16)
        PanelDatos.Controls.Add(Label11)
        PanelDatos.Controls.Add(Label9)
        PanelDatos.Controls.Add(Label8)
        PanelDatos.Controls.Add(Label2)
        PanelDatos.Controls.Add(titulo)
        PanelDatos.Controls.Add(Label19)
        PanelDatos.Controls.Add(Label18)
        PanelDatos.Controls.Add(Label17)
        PanelDatos.Controls.Add(Label14)
        PanelDatos.Controls.Add(Label13)
        PanelDatos.Controls.Add(Label12)
        PanelDatos.Controls.Add(Label10)
        PanelDatos.Controls.Add(Label5)
        PanelDatos.Controls.Add(Label4)
        PanelDatos.Controls.Add(Label1)
        PanelDatos.Font = New Font("Segoe UI", 9F)
        PanelDatos.ForeColor = Color.Black
        PanelDatos.Location = New Point(0, 249)
        PanelDatos.Margin = New Padding(4, 3, 4, 3)
        PanelDatos.Name = "PanelDatos"
        PanelDatos.Padding = New Padding(4, 3, 4, 3)
        PanelDatos.Size = New Size(841, 343)
        PanelDatos.TabIndex = 26
        PanelDatos.TabStop = False
        PanelDatos.Text = "Datos"
        ' 
        ' btnBuscarCuenta
        ' 
        btnBuscarCuenta.FlatStyle = FlatStyle.Flat
        btnBuscarCuenta.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnBuscarCuenta.Location = New Point(166, 84)
        btnBuscarCuenta.Name = "btnBuscarCuenta"
        btnBuscarCuenta.Size = New Size(48, 23)
        btnBuscarCuenta.TabIndex = 64
        btnBuscarCuenta.Text = "Plan"
        btnBuscarCuenta.UseVisualStyleBackColor = True
        ' 
        ' txtcorreo
        ' 
        txtcorreo.Location = New Point(84, 277)
        txtcorreo.Margin = New Padding(4, 3, 4, 3)
        txtcorreo.MaxLength = 50
        txtcorreo.Name = "txtcorreo"
        txtcorreo.Size = New Size(458, 23)
        txtcorreo.TabIndex = 63
        ' 
        ' txtObsv
        ' 
        txtObsv.Location = New Point(84, 306)
        txtObsv.Margin = New Padding(4, 3, 4, 3)
        txtObsv.Name = "txtObsv"
        txtObsv.Size = New Size(458, 23)
        txtObsv.TabIndex = 18
        ' 
        ' chkExterior
        ' 
        chkExterior.CheckAlign = ContentAlignment.MiddleRight
        chkExterior.Font = New Font("Segoe UI", 9F)
        chkExterior.ForeColor = Color.Black
        chkExterior.Location = New Point(224, 55)
        chkExterior.Margin = New Padding(4, 3, 4, 3)
        chkExterior.Name = "chkExterior"
        chkExterior.Size = New Size(169, 20)
        chkExterior.TabIndex = 60
        chkExterior.Text = "Proveedor del Exterior"
        chkExterior.UseVisualStyleBackColor = True
        ' 
        ' CmbPagos
        ' 
        CmbPagos.DropDownStyle = ComboBoxStyle.DropDownList
        CmbPagos.FormattingEnabled = True
        CmbPagos.Location = New Point(84, 222)
        CmbPagos.Margin = New Padding(4, 3, 4, 3)
        CmbPagos.Name = "CmbPagos"
        CmbPagos.Size = New Size(187, 23)
        CmbPagos.TabIndex = 12
        ' 
        ' chkAutoshop
        ' 
        chkAutoshop.CheckAlign = ContentAlignment.MiddleRight
        chkAutoshop.Font = New Font("Segoe UI", 9F)
        chkAutoshop.ForeColor = Color.Black
        chkAutoshop.Location = New Point(224, 74)
        chkAutoshop.Margin = New Padding(4, 3, 4, 3)
        chkAutoshop.Name = "chkAutoshop"
        chkAutoshop.Size = New Size(169, 20)
        chkAutoshop.TabIndex = 58
        chkAutoshop.Text = "AutoShop"
        chkAutoshop.UseVisualStyleBackColor = True
        ' 
        ' CmbRubros
        ' 
        CmbRubros.DropDownStyle = ComboBoxStyle.DropDownList
        CmbRubros.FormattingEnabled = True
        CmbRubros.Location = New Point(84, 194)
        CmbRubros.Margin = New Padding(4, 3, 4, 3)
        CmbRubros.Name = "CmbRubros"
        CmbRubros.Size = New Size(187, 23)
        CmbRubros.TabIndex = 11
        ' 
        ' chkIBrutos
        ' 
        chkIBrutos.CheckAlign = ContentAlignment.MiddleRight
        chkIBrutos.Font = New Font("Segoe UI", 9F)
        chkIBrutos.ForeColor = Color.Black
        chkIBrutos.Location = New Point(560, 111)
        chkIBrutos.Margin = New Padding(4, 3, 4, 3)
        chkIBrutos.Name = "chkIBrutos"
        chkIBrutos.Size = New Size(95, 20)
        chkIBrutos.TabIndex = 57
        chkIBrutos.Text = "I.Brutos:"
        chkIBrutos.UseVisualStyleBackColor = True
        ' 
        ' chkLista
        ' 
        chkLista.CheckAlign = ContentAlignment.MiddleRight
        chkLista.Font = New Font("Segoe UI", 9F)
        chkLista.ForeColor = Color.Black
        chkLista.Location = New Point(224, 18)
        chkLista.Margin = New Padding(4, 3, 4, 3)
        chkLista.Name = "chkLista"
        chkLista.Size = New Size(169, 20)
        chkLista.TabIndex = 56
        chkLista.Text = "No Aparece en listados"
        chkLista.UseVisualStyleBackColor = True
        ' 
        ' chkAsiento
        ' 
        chkAsiento.CheckAlign = ContentAlignment.MiddleRight
        chkAsiento.Font = New Font("Segoe UI", 9F)
        chkAsiento.ForeColor = Color.Black
        chkAsiento.Location = New Point(224, 37)
        chkAsiento.Margin = New Padding(4, 3, 4, 3)
        chkAsiento.Name = "chkAsiento"
        chkAsiento.Size = New Size(169, 20)
        chkAsiento.TabIndex = 55
        chkAsiento.Text = "No genera asiento"
        chkAsiento.UseVisualStyleBackColor = True
        ' 
        ' CmbProvincias
        ' 
        CmbProvincias.DropDownStyle = ComboBoxStyle.DropDownList
        CmbProvincias.FormattingEnabled = True
        CmbProvincias.Location = New Point(84, 166)
        CmbProvincias.Margin = New Padding(4, 3, 4, 3)
        CmbProvincias.Name = "CmbProvincias"
        CmbProvincias.Size = New Size(187, 23)
        CmbProvincias.TabIndex = 10
        ' 
        ' txtcbu
        ' 
        txtcbu.Location = New Point(354, 166)
        txtcbu.Margin = New Padding(4, 3, 4, 3)
        txtcbu.Name = "txtcbu"
        txtcbu.Size = New Size(187, 23)
        txtcbu.TabIndex = 14
        ' 
        ' txtCodContable
        ' 
        txtCodContable.Location = New Point(84, 84)
        txtCodContable.Margin = New Padding(4, 3, 4, 3)
        txtCodContable.Name = "txtCodContable"
        txtCodContable.Size = New Size(75, 23)
        txtCodContable.TabIndex = 52
        txtCodContable.Text = "2.1.1"
        ' 
        ' txtNroCuenta
        ' 
        txtNroCuenta.Location = New Point(84, 30)
        txtNroCuenta.Margin = New Padding(4, 3, 4, 3)
        txtNroCuenta.Name = "txtNroCuenta"
        txtNroCuenta.Size = New Size(75, 23)
        txtNroCuenta.TabIndex = 1
        ' 
        ' txtSaldoDto
        ' 
        txtSaldoDto.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtSaldoDto.Location = New Point(644, 249)
        txtSaldoDto.Margin = New Padding(4, 3, 4, 3)
        txtSaldoDto.Name = "txtSaldoDto"
        txtSaldoDto.Size = New Size(178, 20)
        txtSaldoDto.TabIndex = 21
        txtSaldoDto.TextAlign = HorizontalAlignment.Right
        ' 
        ' txtUltimoResumen
        ' 
        txtUltimoResumen.Location = New Point(644, 222)
        txtUltimoResumen.Margin = New Padding(4, 3, 4, 3)
        txtUltimoResumen.Name = "txtUltimoResumen"
        txtUltimoResumen.Size = New Size(178, 23)
        txtUltimoResumen.TabIndex = 20
        txtUltimoResumen.TextAlign = HorizontalAlignment.Right
        ' 
        ' txtSaldoActual
        ' 
        txtSaldoActual.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtSaldoActual.Location = New Point(644, 166)
        txtSaldoActual.Margin = New Padding(4, 3, 4, 3)
        txtSaldoActual.Name = "txtSaldoActual"
        txtSaldoActual.Size = New Size(178, 20)
        txtSaldoActual.TabIndex = 19
        txtSaldoActual.TextAlign = HorizontalAlignment.Right
        ' 
        ' txtCodigoPostal
        ' 
        txtCodigoPostal.Location = New Point(644, 137)
        txtCodigoPostal.Margin = New Padding(4, 3, 4, 3)
        txtCodigoPostal.MaxLength = 4
        txtCodigoPostal.Name = "txtCodigoPostal"
        txtCodigoPostal.Size = New Size(56, 23)
        txtCodigoPostal.TabIndex = 9
        ' 
        ' txtLocalidad
        ' 
        txtLocalidad.Location = New Point(354, 139)
        txtLocalidad.Margin = New Padding(4, 3, 4, 3)
        txtLocalidad.MaxLength = 25
        txtLocalidad.Name = "txtLocalidad"
        txtLocalidad.Size = New Size(188, 23)
        txtLocalidad.TabIndex = 8
        ' 
        ' txtDepartamento
        ' 
        txtDepartamento.Location = New Point(84, 138)
        txtDepartamento.Margin = New Padding(4, 3, 4, 3)
        txtDepartamento.MaxLength = 25
        txtDepartamento.Name = "txtDepartamento"
        txtDepartamento.Size = New Size(187, 23)
        txtDepartamento.TabIndex = 7
        ' 
        ' txtCalleNro
        ' 
        txtCalleNro.Location = New Point(354, 110)
        txtCalleNro.Margin = New Padding(4, 3, 4, 3)
        txtCalleNro.MaxLength = 35
        txtCalleNro.Name = "txtCalleNro"
        txtCalleNro.Size = New Size(188, 23)
        txtCalleNro.TabIndex = 6
        ' 
        ' txtPublico
        ' 
        txtPublico.Location = New Point(355, 249)
        txtPublico.Margin = New Padding(4, 3, 4, 3)
        txtPublico.Name = "txtPublico"
        txtPublico.Size = New Size(187, 23)
        txtPublico.TabIndex = 17
        ' 
        ' txtPMinimo
        ' 
        txtPMinimo.Location = New Point(355, 194)
        txtPMinimo.Margin = New Padding(4, 3, 4, 3)
        txtPMinimo.MaxLength = 12
        txtPMinimo.Name = "txtPMinimo"
        txtPMinimo.Size = New Size(187, 23)
        txtPMinimo.TabIndex = 15
        ' 
        ' txtTipo
        ' 
        txtTipo.Location = New Point(84, 57)
        txtTipo.Margin = New Padding(4, 3, 4, 3)
        txtTipo.MaxLength = 15
        txtTipo.Name = "txtTipo"
        txtTipo.Size = New Size(103, 23)
        txtTipo.TabIndex = 2
        ' 
        ' txtNombre
        ' 
        txtNombre.Location = New Point(84, 111)
        txtNombre.Margin = New Padding(4, 3, 4, 3)
        txtNombre.MaxLength = 50
        txtNombre.Name = "txtNombre"
        txtNombre.Size = New Size(187, 23)
        txtNombre.TabIndex = 5
        ' 
        ' CmbIva
        ' 
        CmbIva.DropDownStyle = ComboBoxStyle.DropDownList
        CmbIva.FormattingEnabled = True
        CmbIva.Location = New Point(84, 249)
        CmbIva.Margin = New Padding(4, 3, 4, 3)
        CmbIva.Name = "CmbIva"
        CmbIva.Size = New Size(187, 23)
        CmbIva.TabIndex = 13
        ' 
        ' CmbJurisdiccion
        ' 
        CmbJurisdiccion.DropDownStyle = ComboBoxStyle.DropDownList
        CmbJurisdiccion.FormattingEnabled = True
        CmbJurisdiccion.Location = New Point(355, 222)
        CmbJurisdiccion.Margin = New Padding(4, 3, 4, 3)
        CmbJurisdiccion.Name = "CmbJurisdiccion"
        CmbJurisdiccion.Size = New Size(187, 23)
        CmbJurisdiccion.TabIndex = 16
        ' 
        ' Label15
        ' 
        Label15.Font = New Font("Segoe UI", 9F)
        Label15.ForeColor = Color.Black
        Label15.Location = New Point(9, 280)
        Label15.Margin = New Padding(4, 0, 4, 0)
        Label15.Name = "Label15"
        Label15.Size = New Size(57, 20)
        Label15.TabIndex = 62
        Label15.Text = "Correo E:"
        ' 
        ' Label3
        ' 
        Label3.Font = New Font("Segoe UI", 9F)
        Label3.ForeColor = Color.Black
        Label3.Location = New Point(9, 309)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(113, 20)
        Label3.TabIndex = 61
        Label3.Text = "Comentarios:"
        ' 
        ' Label7
        ' 
        Label7.Font = New Font("Segoe UI", 9F)
        Label7.ForeColor = Color.Black
        Label7.Location = New Point(279, 169)
        Label7.Margin = New Padding(4, 0, 4, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(57, 20)
        Label7.TabIndex = 53
        Label7.Text = "CBU:"
        ' 
        ' Label6
        ' 
        Label6.Font = New Font("Segoe UI", 9F)
        Label6.ForeColor = Color.Black
        Label6.Location = New Point(9, 84)
        Label6.Margin = New Padding(4, 0, 4, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(66, 20)
        Label6.TabIndex = 51
        Label6.Text = "Cod.Conta:"
        ' 
        ' Label22
        ' 
        Label22.Font = New Font("Segoe UI", 9F)
        Label22.ForeColor = Color.Black
        Label22.Location = New Point(560, 139)
        Label22.Margin = New Padding(4, 0, 4, 0)
        Label22.Name = "Label22"
        Label22.Size = New Size(41, 20)
        Label22.TabIndex = 50
        Label22.Text = "C.P.:"
        ' 
        ' Label21
        ' 
        Label21.Font = New Font("Segoe UI", 9F)
        Label21.ForeColor = Color.Black
        Label21.Location = New Point(9, 30)
        Label21.Margin = New Padding(4, 0, 4, 0)
        Label21.Name = "Label21"
        Label21.Size = New Size(78, 20)
        Label21.TabIndex = 49
        Label21.Text = "NroCuenta:"
        ' 
        ' lblSinDoc
        ' 
        lblSinDoc.BorderStyle = BorderStyle.FixedSingle
        lblSinDoc.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblSinDoc.ForeColor = Color.Red
        lblSinDoc.Location = New Point(644, 195)
        lblSinDoc.Margin = New Padding(4, 0, 4, 0)
        lblSinDoc.Name = "lblSinDoc"
        lblSinDoc.Size = New Size(178, 19)
        lblSinDoc.TabIndex = 48
        lblSinDoc.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label16
        ' 
        Label16.Font = New Font("Segoe UI", 9F)
        Label16.ForeColor = Color.Black
        Label16.Location = New Point(560, 194)
        Label16.Margin = New Padding(4, 0, 4, 0)
        Label16.Name = "Label16"
        Label16.Size = New Size(76, 20)
        Label16.TabIndex = 47
        Label16.Text = "Sin Doc:"
        ' 
        ' Label11
        ' 
        Label11.Font = New Font("Segoe UI", 9F)
        Label11.ForeColor = Color.Black
        Label11.Location = New Point(9, 166)
        Label11.Margin = New Padding(4, 0, 4, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(57, 20)
        Label11.TabIndex = 46
        Label11.Text = "Provincia:"
        ' 
        ' Label9
        ' 
        Label9.Font = New Font("Segoe UI", 9F)
        Label9.ForeColor = Color.Black
        Label9.Location = New Point(280, 142)
        Label9.Margin = New Padding(4, 0, 4, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(64, 20)
        Label9.TabIndex = 45
        Label9.Text = "Localidad:"
        ' 
        ' Label8
        ' 
        Label8.Font = New Font("Segoe UI", 9F)
        Label8.ForeColor = Color.Black
        Label8.Location = New Point(9, 138)
        Label8.Margin = New Padding(4, 0, 4, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(66, 20)
        Label8.TabIndex = 44
        Label8.Text = "Depto.:"
        ' 
        ' Label2
        ' 
        Label2.Font = New Font("Segoe UI", 9F)
        Label2.ForeColor = Color.Black
        Label2.Location = New Point(280, 114)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(94, 20)
        Label2.TabIndex = 43
        Label2.Text = "Calle y Nro.:"
        ' 
        ' titulo
        ' 
        titulo.Font = New Font("Segoe UI", 9F)
        titulo.ForeColor = Color.Black
        titulo.Location = New Point(9, 194)
        titulo.Margin = New Padding(4, 0, 4, 0)
        titulo.Name = "titulo"
        titulo.Size = New Size(66, 20)
        titulo.TabIndex = 42
        titulo.Text = "Rubro :"
        ' 
        ' Label19
        ' 
        Label19.Font = New Font("Segoe UI", 9F)
        Label19.ForeColor = Color.Black
        Label19.Location = New Point(560, 249)
        Label19.Margin = New Padding(4, 0, 4, 0)
        Label19.Name = "Label19"
        Label19.Size = New Size(76, 20)
        Label19.TabIndex = 38
        Label19.Text = "Documentos:"
        ' 
        ' Label18
        ' 
        Label18.Font = New Font("Segoe UI", 9F)
        Label18.ForeColor = Color.Black
        Label18.Location = New Point(560, 222)
        Label18.Margin = New Padding(4, 0, 4, 0)
        Label18.Name = "Label18"
        Label18.Size = New Size(66, 20)
        Label18.TabIndex = 37
        Label18.Text = "Resumen:"
        ' 
        ' Label17
        ' 
        Label17.Font = New Font("Segoe UI", 9F)
        Label17.ForeColor = Color.Black
        Label17.Location = New Point(560, 166)
        Label17.Margin = New Padding(4, 0, 4, 0)
        Label17.Name = "Label17"
        Label17.Size = New Size(48, 20)
        Label17.TabIndex = 36
        Label17.Text = "Saldo  :"
        ' 
        ' Label14
        ' 
        Label14.Font = New Font("Segoe UI", 9F)
        Label14.ForeColor = Color.Black
        Label14.Location = New Point(280, 222)
        Label14.Margin = New Padding(4, 0, 4, 0)
        Label14.Name = "Label14"
        Label14.Size = New Size(76, 20)
        Label14.TabIndex = 35
        Label14.Text = "Juridiccion:"
        ' 
        ' Label13
        ' 
        Label13.Font = New Font("Segoe UI", 9F)
        Label13.ForeColor = Color.Black
        Label13.Location = New Point(280, 249)
        Label13.Margin = New Padding(4, 0, 4, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(57, 20)
        Label13.TabIndex = 34
        Label13.Text = "Baja :"
        ' 
        ' Label12
        ' 
        Label12.Font = New Font("Segoe UI", 9F)
        Label12.ForeColor = Color.Black
        Label12.Location = New Point(280, 194)
        Label12.Margin = New Padding(4, 0, 4, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(66, 20)
        Label12.TabIndex = 33
        Label12.Text = "Telefono:"
        ' 
        ' Label10
        ' 
        Label10.Font = New Font("Segoe UI", 9F)
        Label10.ForeColor = Color.Black
        Label10.Location = New Point(9, 249)
        Label10.Margin = New Padding(4, 0, 4, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(66, 20)
        Label10.TabIndex = 32
        Label10.Text = "Iva:"
        ' 
        ' Label5
        ' 
        Label5.Font = New Font("Segoe UI", 9F)
        Label5.ForeColor = Color.Black
        Label5.Location = New Point(9, 57)
        Label5.Margin = New Padding(4, 0, 4, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(57, 20)
        Label5.TabIndex = 31
        Label5.Text = "Cuit :"
        ' 
        ' Label4
        ' 
        Label4.Font = New Font("Segoe UI", 9F)
        Label4.ForeColor = Color.Black
        Label4.Location = New Point(9, 111)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(66, 20)
        Label4.TabIndex = 30
        Label4.Text = "Nombre :"
        ' 
        ' Label1
        ' 
        Label1.Font = New Font("Segoe UI", 9F)
        Label1.ForeColor = Color.Black
        Label1.Location = New Point(9, 222)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(57, 20)
        Label1.TabIndex = 29
        Label1.Text = "Pagos :"
        ' 
        ' fraCriterio
        ' 
        fraCriterio.Controls.Add(txtBuscar)
        fraCriterio.Controls.Add(Label26)
        fraCriterio.Font = New Font("Segoe UI", 9F)
        fraCriterio.ForeColor = Color.Black
        fraCriterio.Location = New Point(0, 0)
        fraCriterio.Margin = New Padding(4, 3, 4, 3)
        fraCriterio.Name = "fraCriterio"
        fraCriterio.Padding = New Padding(4, 3, 4, 3)
        fraCriterio.Size = New Size(841, 57)
        fraCriterio.TabIndex = 39
        fraCriterio.TabStop = False
        fraCriterio.Text = "Criterios de Busqueda"
        ' 
        ' txtBuscar
        ' 
        txtBuscar.Location = New Point(66, 24)
        txtBuscar.Margin = New Padding(4, 3, 4, 3)
        txtBuscar.Name = "txtBuscar"
        txtBuscar.Size = New Size(767, 23)
        txtBuscar.TabIndex = 0
        ' 
        ' Label26
        ' 
        Label26.Font = New Font("Segoe UI", 9F)
        Label26.ForeColor = Color.Black
        Label26.Location = New Point(10, 25)
        Label26.Margin = New Padding(4, 0, 4, 0)
        Label26.Name = "Label26"
        Label26.Size = New Size(57, 20)
        Label26.TabIndex = 40
        Label26.Text = "Buscar:"
        ' 
        ' dgvProveedores
        ' 
        dgvProveedores.AllowUserToAddRows = False
        dgvProveedores.AllowUserToDeleteRows = False
        dgvProveedores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvProveedores.Location = New Point(0, 65)
        dgvProveedores.Margin = New Padding(4, 3, 4, 3)
        dgvProveedores.Name = "dgvProveedores"
        dgvProveedores.ReadOnly = True
        dgvProveedores.Size = New Size(841, 160)
        dgvProveedores.TabIndex = 54
        ' 
        ' cmdAgregar
        ' 
        cmdAgregar.FlatStyle = FlatStyle.Flat
        cmdAgregar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdAgregar.Location = New Point(291, 600)
        cmdAgregar.Margin = New Padding(4, 3, 4, 3)
        cmdAgregar.Name = "cmdAgregar"
        cmdAgregar.Size = New Size(85, 33)
        cmdAgregar.TabIndex = 22
        cmdAgregar.Text = "&Agregar"
        cmdAgregar.UseVisualStyleBackColor = True
        ' 
        ' cmdBorrar
        ' 
        cmdBorrar.FlatStyle = FlatStyle.Flat
        cmdBorrar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdBorrar.Location = New Point(384, 600)
        cmdBorrar.Margin = New Padding(4, 3, 4, 3)
        cmdBorrar.Name = "cmdBorrar"
        cmdBorrar.Size = New Size(85, 33)
        cmdBorrar.TabIndex = 23
        cmdBorrar.Text = "&Borrar"
        cmdBorrar.UseVisualStyleBackColor = True
        ' 
        ' cmdAceptar
        ' 
        cmdAceptar.Enabled = False
        cmdAceptar.FlatStyle = FlatStyle.Flat
        cmdAceptar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdAceptar.Location = New Point(570, 600)
        cmdAceptar.Margin = New Padding(4, 3, 4, 3)
        cmdAceptar.Name = "cmdAceptar"
        cmdAceptar.Size = New Size(85, 33)
        cmdAceptar.TabIndex = 25
        cmdAceptar.Text = "Ac&eptar"
        cmdAceptar.UseVisualStyleBackColor = True
        ' 
        ' cmdModificar
        ' 
        cmdModificar.FlatStyle = FlatStyle.Flat
        cmdModificar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdModificar.Location = New Point(477, 600)
        cmdModificar.Margin = New Padding(4, 3, 4, 3)
        cmdModificar.Name = "cmdModificar"
        cmdModificar.Size = New Size(85, 33)
        cmdModificar.TabIndex = 24
        cmdModificar.Text = "&Modificar"
        cmdModificar.UseVisualStyleBackColor = True
        ' 
        ' cmdCancelar
        ' 
        cmdCancelar.Enabled = False
        cmdCancelar.FlatStyle = FlatStyle.Flat
        cmdCancelar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdCancelar.Location = New Point(663, 600)
        cmdCancelar.Margin = New Padding(4, 3, 4, 3)
        cmdCancelar.Name = "cmdCancelar"
        cmdCancelar.Size = New Size(85, 33)
        cmdCancelar.TabIndex = 27
        cmdCancelar.Text = "C&ancelar"
        cmdCancelar.UseVisualStyleBackColor = True
        ' 
        ' dbcEmpresas
        ' 
        dbcEmpresas.FormattingEnabled = True
        dbcEmpresas.Location = New Point(392, 674)
        dbcEmpresas.Margin = New Padding(4, 3, 4, 3)
        dbcEmpresas.Name = "dbcEmpresas"
        dbcEmpresas.Size = New Size(187, 23)
        dbcEmpresas.TabIndex = 59
        dbcEmpresas.Visible = False
        ' 
        ' CmdSalir
        ' 
        CmdSalir.BackColor = Color.IndianRed
        CmdSalir.FlatStyle = FlatStyle.Flat
        CmdSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdSalir.ForeColor = Color.White
        CmdSalir.Location = New Point(754, 599)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(85, 33)
        CmdSalir.TabIndex = 60
        CmdSalir.Text = "Salir"
        CmdSalir.UseVisualStyleBackColor = False
        ' 
        ' lnkCopiar
        ' 
        lnkCopiar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        lnkCopiar.AutoSize = True
        lnkCopiar.LinkColor = Color.Black
        lnkCopiar.Location = New Point(620, 233)
        lnkCopiar.Name = "lnkCopiar"
        lnkCopiar.Size = New Size(94, 15)
        lnkCopiar.TabIndex = 62
        lnkCopiar.TabStop = True
        lnkCopiar.Text = "Copiar selección"
        ' 
        ' chkEncabezados
        ' 
        chkEncabezados.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        chkEncabezados.AutoSize = True
        chkEncabezados.Location = New Point(720, 231)
        chkEncabezados.Name = "chkEncabezados"
        chkEncabezados.Size = New Size(119, 19)
        chkEncabezados.TabIndex = 61
        chkEncabezados.Text = "Con encabezados"
        chkEncabezados.UseVisualStyleBackColor = True
        ' 
        ' frmProvee
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(851, 644)
        Controls.Add(lnkCopiar)
        Controls.Add(chkEncabezados)
        Controls.Add(CmdSalir)
        Controls.Add(PanelDatos)
        Controls.Add(fraCriterio)
        Controls.Add(dgvProveedores)
        Controls.Add(cmdAgregar)
        Controls.Add(cmdBorrar)
        Controls.Add(cmdAceptar)
        Controls.Add(cmdModificar)
        Controls.Add(cmdCancelar)
        Controls.Add(dbcEmpresas)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmProvee"
        Text = "Mantenimiento de Proveedores"
        PanelDatos.ResumeLayout(False)
        PanelDatos.PerformLayout()
        fraCriterio.ResumeLayout(False)
        fraCriterio.PerformLayout()
        CType(dgvProveedores, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents PanelDatos As System.Windows.Forms.GroupBox
    Friend WithEvents txtcorreo As System.Windows.Forms.TextBox
    Friend WithEvents txtObsv As System.Windows.Forms.TextBox
    Friend WithEvents chkExterior As System.Windows.Forms.CheckBox
    Friend WithEvents CmbPagos As System.Windows.Forms.ComboBox
    Friend WithEvents chkAutoshop As System.Windows.Forms.CheckBox
    Friend WithEvents CmbRubros As System.Windows.Forms.ComboBox
    Friend WithEvents chkIBrutos As System.Windows.Forms.CheckBox
    Friend WithEvents chkLista As System.Windows.Forms.CheckBox
    Friend WithEvents chkAsiento As System.Windows.Forms.CheckBox
    Friend WithEvents CmbProvincias As System.Windows.Forms.ComboBox
    Friend WithEvents txtcbu As System.Windows.Forms.TextBox
    Friend WithEvents txtCodContable As System.Windows.Forms.TextBox
    Friend WithEvents txtNroCuenta As System.Windows.Forms.TextBox
    Friend WithEvents txtSaldoDto As System.Windows.Forms.TextBox
    Friend WithEvents txtUltimoResumen As System.Windows.Forms.TextBox
    Friend WithEvents txtSaldoActual As System.Windows.Forms.TextBox
    Friend WithEvents txtCodigoPostal As System.Windows.Forms.TextBox
    Friend WithEvents txtLocalidad As System.Windows.Forms.TextBox
    Friend WithEvents txtDepartamento As System.Windows.Forms.TextBox
    Friend WithEvents txtCalleNro As System.Windows.Forms.TextBox
    Friend WithEvents txtPublico As System.Windows.Forms.TextBox
    Friend WithEvents txtPMinimo As System.Windows.Forms.TextBox
    Friend WithEvents txtTipo As System.Windows.Forms.TextBox
    Friend WithEvents txtNombre As System.Windows.Forms.TextBox
    Friend WithEvents CmbIva As System.Windows.Forms.ComboBox
    Friend WithEvents CmbJurisdiccion As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents lblSinDoc As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents titulo As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents fraCriterio As System.Windows.Forms.GroupBox
    Friend WithEvents txtBuscar As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents dgvProveedores As System.Windows.Forms.DataGridView
    Friend WithEvents cmdAgregar As System.Windows.Forms.Button
    Friend WithEvents cmdBorrar As System.Windows.Forms.Button
    Friend WithEvents cmdAceptar As System.Windows.Forms.Button
    Friend WithEvents cmdModificar As System.Windows.Forms.Button
    Friend WithEvents cmdCancelar As System.Windows.Forms.Button
    Friend WithEvents dbcEmpresas As System.Windows.Forms.ComboBox
    Friend WithEvents CmdSalir As Button
    Friend WithEvents lnkCopiar As LinkLabel
    Friend WithEvents chkEncabezados As CheckBox
    Friend WithEvents btnBuscarCuenta As Button
End Class

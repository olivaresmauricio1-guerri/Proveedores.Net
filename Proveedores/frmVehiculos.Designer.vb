<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVehiculos
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        LblBuscar = New Label()
        TxtBuscar = New TextBox()
        chkEncabezados = New CheckBox()
        lnkCopiar = New LinkLabel()
        DgvListado = New DataGridView()
        GroupBoxDatos = New GroupBox()
        txtComentario = New TextBox()
        LblComentario = New Label()
        chkActivo = New CheckBox()
        dtpFechaAlta = New DateTimePicker()
        LblFechaAlta = New Label()
        cboSucursal = New ComboBox()
        LblSucursal = New Label()
        txtKmActual = New TextBox()
        LblKmActual = New Label()
        txtAnio = New TextBox()
        LblAnio = New Label()
        txtModelo = New TextBox()
        LblModelo = New Label()
        txtMarca = New TextBox()
        LblMarca = New Label()
        txtPatente = New TextBox()
        LblPatente = New Label()
        LblCodigo = New Label()
        TxtCodigo = New TextBox()
        CmdAgregar = New Button()
        btnModificar = New Button()
        CmdBorrar = New Button()
        cmdAceptar = New Button()
        CmdCancelar = New Button()
        CmdSalir = New Button()
        CType(DgvListado, ComponentModel.ISupportInitialize).BeginInit()
        GroupBoxDatos.SuspendLayout()
        SuspendLayout()
        ' 
        ' LblBuscar
        ' 
        LblBuscar.AutoSize = True
        LblBuscar.Location = New Point(7, 9)
        LblBuscar.Name = "LblBuscar"
        LblBuscar.Size = New Size(42, 15)
        LblBuscar.TabIndex = 0
        LblBuscar.Text = "Buscar"
        ' 
        ' TxtBuscar
        ' 
        TxtBuscar.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TxtBuscar.Location = New Point(56, 6)
        TxtBuscar.Name = "TxtBuscar"
        TxtBuscar.Size = New Size(560, 23)
        TxtBuscar.TabIndex = 1
        ' 
        ' chkEncabezados
        ' 
        chkEncabezados.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        chkEncabezados.AutoSize = True
        chkEncabezados.Location = New Point(491, 284)
        chkEncabezados.Name = "chkEncabezados"
        chkEncabezados.Size = New Size(119, 19)
        chkEncabezados.TabIndex = 2
        chkEncabezados.Text = "Con encabezados"
        chkEncabezados.UseVisualStyleBackColor = True
        ' 
        ' lnkCopiar
        ' 
        lnkCopiar.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lnkCopiar.AutoSize = True
        lnkCopiar.LinkColor = Color.Black
        lnkCopiar.Location = New Point(391, 284)
        lnkCopiar.Name = "lnkCopiar"
        lnkCopiar.Size = New Size(94, 15)
        lnkCopiar.TabIndex = 3
        lnkCopiar.TabStop = True
        lnkCopiar.Text = "Copiar selección"
        ' 
        ' DgvListado
        ' 
        DgvListado.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DgvListado.Location = New Point(2, 35)
        DgvListado.MultiSelect = False
        DgvListado.Name = "DgvListado"
        DgvListado.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DgvListado.Size = New Size(614, 243)
        DgvListado.TabIndex = 4
        ' 
        ' GroupBoxDatos
        ' 
        GroupBoxDatos.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBoxDatos.Controls.Add(txtComentario)
        GroupBoxDatos.Controls.Add(LblComentario)
        GroupBoxDatos.Controls.Add(chkActivo)
        GroupBoxDatos.Controls.Add(dtpFechaAlta)
        GroupBoxDatos.Controls.Add(LblFechaAlta)
        GroupBoxDatos.Controls.Add(cboSucursal)
        GroupBoxDatos.Controls.Add(LblSucursal)
        GroupBoxDatos.Controls.Add(txtKmActual)
        GroupBoxDatos.Controls.Add(LblKmActual)
        GroupBoxDatos.Controls.Add(txtAnio)
        GroupBoxDatos.Controls.Add(LblAnio)
        GroupBoxDatos.Controls.Add(txtModelo)
        GroupBoxDatos.Controls.Add(LblModelo)
        GroupBoxDatos.Controls.Add(txtMarca)
        GroupBoxDatos.Controls.Add(LblMarca)
        GroupBoxDatos.Controls.Add(txtPatente)
        GroupBoxDatos.Controls.Add(LblPatente)
        GroupBoxDatos.Controls.Add(LblCodigo)
        GroupBoxDatos.Controls.Add(TxtCodigo)
        GroupBoxDatos.Location = New Point(10, 308)
        GroupBoxDatos.Name = "GroupBoxDatos"
        GroupBoxDatos.Size = New Size(606, 204)
        GroupBoxDatos.TabIndex = 5
        GroupBoxDatos.TabStop = False
        GroupBoxDatos.Text = "Datos del Vehículo"
        ' 
        ' txtComentario
        ' 
        txtComentario.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtComentario.Location = New Point(90, 165)
        txtComentario.Name = "txtComentario"
        txtComentario.Size = New Size(500, 23)
        txtComentario.TabIndex = 17
        ' 
        ' LblComentario
        ' 
        LblComentario.AutoSize = True
        LblComentario.Location = New Point(12, 168)
        LblComentario.Name = "LblComentario"
        LblComentario.Size = New Size(70, 15)
        LblComentario.TabIndex = 16
        LblComentario.Text = "Comentario"
        ' 
        ' chkActivo
        ' 
        chkActivo.AutoSize = True
        chkActivo.Checked = True
        chkActivo.CheckState = CheckState.Checked
        chkActivo.Location = New Point(540, 138)
        chkActivo.Name = "chkActivo"
        chkActivo.Size = New Size(60, 19)
        chkActivo.TabIndex = 15
        chkActivo.Text = "Activo"
        chkActivo.UseVisualStyleBackColor = True
        ' 
        ' dtpFechaAlta
        ' 
        dtpFechaAlta.Format = DateTimePickerFormat.Short
        dtpFechaAlta.Location = New Point(430, 49)
        dtpFechaAlta.Name = "dtpFechaAlta"
        dtpFechaAlta.Size = New Size(160, 23)
        dtpFechaAlta.TabIndex = 7
        ' 
        ' LblFechaAlta
        ' 
        LblFechaAlta.AutoSize = True
        LblFechaAlta.Location = New Point(360, 52)
        LblFechaAlta.Name = "LblFechaAlta"
        LblFechaAlta.Size = New Size(52, 15)
        LblFechaAlta.TabIndex = 14
        LblFechaAlta.Text = "Fec. Alta"
        ' 
        ' cboSucursal
        ' 
        cboSucursal.DropDownStyle = ComboBoxStyle.DropDownList
        cboSucursal.FormattingEnabled = True
        cboSucursal.Location = New Point(90, 136)
        cboSucursal.Name = "cboSucursal"
        cboSucursal.Size = New Size(200, 23)
        cboSucursal.TabIndex = 13
        ' 
        ' LblSucursal
        ' 
        LblSucursal.AutoSize = True
        LblSucursal.Location = New Point(12, 139)
        LblSucursal.Name = "LblSucursal"
        LblSucursal.Size = New Size(51, 15)
        LblSucursal.TabIndex = 12
        LblSucursal.Text = "Sucursal"
        ' 
        ' txtKmActual
        ' 
        txtKmActual.Location = New Point(90, 107)
        txtKmActual.Name = "txtKmActual"
        txtKmActual.Size = New Size(200, 23)
        txtKmActual.TabIndex = 11
        ' 
        ' LblKmActual
        ' 
        LblKmActual.AutoSize = True
        LblKmActual.Location = New Point(12, 111)
        LblKmActual.Name = "LblKmActual"
        LblKmActual.Size = New Size(62, 15)
        LblKmActual.TabIndex = 10
        LblKmActual.Text = "Km Actual"
        ' 
        ' txtAnio
        ' 
        txtAnio.Location = New Point(430, 21)
        txtAnio.Name = "txtAnio"
        txtAnio.Size = New Size(160, 23)
        txtAnio.TabIndex = 3
        ' 
        ' LblAnio
        ' 
        LblAnio.AutoSize = True
        LblAnio.Location = New Point(390, 24)
        LblAnio.Name = "LblAnio"
        LblAnio.Size = New Size(29, 15)
        LblAnio.TabIndex = 8
        LblAnio.Text = "Año"
        ' 
        ' txtModelo
        ' 
        txtModelo.Location = New Point(90, 77)
        txtModelo.Name = "txtModelo"
        txtModelo.Size = New Size(200, 23)
        txtModelo.TabIndex = 5
        ' 
        ' LblModelo
        ' 
        LblModelo.AutoSize = True
        LblModelo.Location = New Point(12, 80)
        LblModelo.Name = "LblModelo"
        LblModelo.Size = New Size(48, 15)
        LblModelo.TabIndex = 6
        LblModelo.Text = "Modelo"
        ' 
        ' txtMarca
        ' 
        txtMarca.Location = New Point(90, 48)
        txtMarca.Name = "txtMarca"
        txtMarca.Size = New Size(200, 23)
        txtMarca.TabIndex = 4
        ' 
        ' LblMarca
        ' 
        LblMarca.AutoSize = True
        LblMarca.Location = New Point(12, 51)
        LblMarca.Name = "LblMarca"
        LblMarca.Size = New Size(40, 15)
        LblMarca.TabIndex = 4
        LblMarca.Text = "Marca"
        ' 
        ' txtPatente
        ' 
        txtPatente.CharacterCasing = CharacterCasing.Upper
        txtPatente.Location = New Point(280, 19)
        txtPatente.Name = "txtPatente"
        txtPatente.Size = New Size(90, 23)
        txtPatente.TabIndex = 1
        ' 
        ' LblPatente
        ' 
        LblPatente.AutoSize = True
        LblPatente.Location = New Point(202, 22)
        LblPatente.Name = "LblPatente"
        LblPatente.Size = New Size(47, 15)
        LblPatente.TabIndex = 2
        LblPatente.Text = "Patente"
        ' 
        ' LblCodigo
        ' 
        LblCodigo.AutoSize = True
        LblCodigo.Location = New Point(12, 22)
        LblCodigo.Name = "LblCodigo"
        LblCodigo.Size = New Size(46, 15)
        LblCodigo.TabIndex = 0
        LblCodigo.Text = "Código"
        ' 
        ' TxtCodigo
        ' 
        TxtCodigo.Location = New Point(90, 19)
        TxtCodigo.Name = "TxtCodigo"
        TxtCodigo.ReadOnly = True
        TxtCodigo.Size = New Size(80, 23)
        TxtCodigo.TabIndex = 0
        ' 
        ' CmdAgregar
        ' 
        CmdAgregar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        CmdAgregar.FlatStyle = FlatStyle.Flat
        CmdAgregar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdAgregar.Location = New Point(136, 518)
        CmdAgregar.Name = "CmdAgregar"
        CmdAgregar.Size = New Size(75, 28)
        CmdAgregar.TabIndex = 6
        CmdAgregar.Text = "Agregar"
        CmdAgregar.UseVisualStyleBackColor = True
        ' 
        ' btnModificar
        ' 
        btnModificar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnModificar.FlatStyle = FlatStyle.Flat
        btnModificar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnModificar.Location = New Point(217, 518)
        btnModificar.Name = "btnModificar"
        btnModificar.Size = New Size(75, 28)
        btnModificar.TabIndex = 16
        btnModificar.Text = "Modificar"
        btnModificar.UseVisualStyleBackColor = True
        ' 
        ' CmdBorrar
        ' 
        CmdBorrar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        CmdBorrar.FlatStyle = FlatStyle.Flat
        CmdBorrar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdBorrar.Location = New Point(298, 518)
        CmdBorrar.Name = "CmdBorrar"
        CmdBorrar.Size = New Size(75, 28)
        CmdBorrar.TabIndex = 7
        CmdBorrar.Text = "Borrar"
        CmdBorrar.UseVisualStyleBackColor = True
        ' 
        ' cmdAceptar
        ' 
        cmdAceptar.Anchor = AnchorStyles.Bottom
        cmdAceptar.FlatStyle = FlatStyle.Flat
        cmdAceptar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdAceptar.Location = New Point(379, 518)
        cmdAceptar.Name = "cmdAceptar"
        cmdAceptar.Size = New Size(75, 28)
        cmdAceptar.TabIndex = 8
        cmdAceptar.Text = "Aceptar"
        cmdAceptar.UseVisualStyleBackColor = True
        ' 
        ' CmdCancelar
        ' 
        CmdCancelar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        CmdCancelar.FlatStyle = FlatStyle.Flat
        CmdCancelar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdCancelar.Location = New Point(460, 518)
        CmdCancelar.Name = "CmdCancelar"
        CmdCancelar.Size = New Size(75, 28)
        CmdCancelar.TabIndex = 15
        CmdCancelar.Text = "Cancelar"
        CmdCancelar.UseVisualStyleBackColor = True
        ' 
        ' CmdSalir
        ' 
        CmdSalir.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        CmdSalir.BackColor = Color.IndianRed
        CmdSalir.FlatStyle = FlatStyle.Flat
        CmdSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdSalir.ForeColor = Color.White
        CmdSalir.Location = New Point(541, 518)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(75, 28)
        CmdSalir.TabIndex = 9
        CmdSalir.Text = "Salir"
        CmdSalir.UseVisualStyleBackColor = False
        ' 
        ' frmVehiculos
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(618, 558)
        Controls.Add(CmdCancelar)
        Controls.Add(cmdAceptar)
        Controls.Add(CmdSalir)
        Controls.Add(CmdBorrar)
        Controls.Add(btnModificar)
        Controls.Add(CmdAgregar)
        Controls.Add(GroupBoxDatos)
        Controls.Add(DgvListado)
        Controls.Add(lnkCopiar)
        Controls.Add(chkEncabezados)
        Controls.Add(TxtBuscar)
        Controls.Add(LblBuscar)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmVehiculos"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Vehículos"
        CType(DgvListado, ComponentModel.ISupportInitialize).EndInit()
        GroupBoxDatos.ResumeLayout(False)
        GroupBoxDatos.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents LblBuscar As Label
    Friend WithEvents TxtBuscar As TextBox
    Friend WithEvents chkEncabezados As CheckBox
    Friend WithEvents lnkCopiar As LinkLabel
    Friend WithEvents DgvListado As DataGridView
    Friend WithEvents GroupBoxDatos As GroupBox
    Friend WithEvents txtComentario As TextBox
    Friend WithEvents LblComentario As Label
    Friend WithEvents chkActivo As CheckBox
    Friend WithEvents dtpFechaAlta As DateTimePicker
    Friend WithEvents LblFechaAlta As Label
    Friend WithEvents cboSucursal As ComboBox
    Friend WithEvents LblSucursal As Label
    Friend WithEvents txtKmActual As TextBox
    Friend WithEvents LblKmActual As Label
    Friend WithEvents txtAnio As TextBox
    Friend WithEvents LblAnio As Label
    Friend WithEvents txtModelo As TextBox
    Friend WithEvents LblModelo As Label
    Friend WithEvents txtMarca As TextBox
    Friend WithEvents LblMarca As Label
    Friend WithEvents txtPatente As TextBox
    Friend WithEvents LblPatente As Label
    Friend WithEvents LblCodigo As Label
    Friend WithEvents TxtCodigo As TextBox
    Friend WithEvents CmdAgregar As Button
    Friend WithEvents btnModificar As Button
    Friend WithEvents CmdBorrar As Button
    Friend WithEvents cmdAceptar As Button
    Friend WithEvents CmdCancelar As Button
    Friend WithEvents CmdSalir As Button
End Class

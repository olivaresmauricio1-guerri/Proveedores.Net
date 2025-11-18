<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBancos
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBancos))
        LblBuscar = New Label()
        TxtBuscar = New TextBox()
        chkEncabezados = New CheckBox()
        lnkCopiar = New LinkLabel()
        DgvListado = New DataGridView()
        GroupBoxDatos = New GroupBox()
        Label1 = New Label()
        txtNroCuenta = New TextBox()
        LblCodigo = New Label()
        TxtCodigo = New TextBox()
        LblDescripcion = New Label()
        TxtDescripcion = New TextBox()
        CmdAgregar = New Button()
        CmdBorrar = New Button()
        CmdSalir = New Button()
        cmdAceptar = New Button()
        CmdCancelar = New Button()
        CType(DgvListado, ComponentModel.ISupportInitialize).BeginInit()
        GroupBoxDatos.SuspendLayout()
        SuspendLayout()
        ' 
        ' LblBuscar
        ' 
        LblBuscar.AutoSize = True
        LblBuscar.Location = New Point(14, 17)
        LblBuscar.Margin = New Padding(4, 0, 4, 0)
        LblBuscar.Name = "LblBuscar"
        LblBuscar.Size = New Size(42, 15)
        LblBuscar.TabIndex = 0
        LblBuscar.Text = "Buscar"
        ' 
        ' TxtBuscar
        ' 
        TxtBuscar.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TxtBuscar.Location = New Point(71, 14)
        TxtBuscar.Margin = New Padding(4, 3, 4, 3)
        TxtBuscar.Name = "TxtBuscar"
        TxtBuscar.Size = New Size(398, 23)
        TxtBuscar.TabIndex = 1
        ' 
        ' chkEncabezados
        ' 
        chkEncabezados.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        chkEncabezados.AutoSize = True
        chkEncabezados.Location = New Point(349, 289)
        chkEncabezados.Margin = New Padding(4, 3, 4, 3)
        chkEncabezados.Name = "chkEncabezados"
        chkEncabezados.Size = New Size(119, 19)
        chkEncabezados.TabIndex = 2
        chkEncabezados.Text = "Con encabezados"
        chkEncabezados.UseVisualStyleBackColor = True
        ' 
        ' lnkCopiar
        ' 
        lnkCopiar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        lnkCopiar.AutoSize = True
        lnkCopiar.LinkColor = Color.Black
        lnkCopiar.Location = New Point(247, 289)
        lnkCopiar.Margin = New Padding(4, 0, 4, 0)
        lnkCopiar.Name = "lnkCopiar"
        lnkCopiar.Size = New Size(94, 15)
        lnkCopiar.TabIndex = 3
        lnkCopiar.TabStop = True
        lnkCopiar.Text = "Copiar selección"
        ' 
        ' DgvListado
        ' 
        DgvListado.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DgvListado.Location = New Point(2, 46)
        DgvListado.Margin = New Padding(4, 3, 4, 3)
        DgvListado.MultiSelect = False
        DgvListado.Name = "DgvListado"
        DgvListado.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DgvListado.Size = New Size(466, 234)
        DgvListado.TabIndex = 4
        ' 
        ' GroupBoxDatos
        ' 
        GroupBoxDatos.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBoxDatos.Controls.Add(Label1)
        GroupBoxDatos.Controls.Add(txtNroCuenta)
        GroupBoxDatos.Controls.Add(LblCodigo)
        GroupBoxDatos.Controls.Add(TxtCodigo)
        GroupBoxDatos.Controls.Add(LblDescripcion)
        GroupBoxDatos.Controls.Add(TxtDescripcion)
        GroupBoxDatos.Location = New Point(2, 316)
        GroupBoxDatos.Margin = New Padding(4, 3, 4, 3)
        GroupBoxDatos.Name = "GroupBoxDatos"
        GroupBoxDatos.Padding = New Padding(4, 3, 4, 3)
        GroupBoxDatos.Size = New Size(466, 88)
        GroupBoxDatos.TabIndex = 5
        GroupBoxDatos.TabStop = False
        GroupBoxDatos.Text = "Datos de Banco"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(6, 56)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(71, 15)
        Label1.TabIndex = 6
        Label1.Text = "Nro Cuenta:"
        ' 
        ' txtNroCuenta
        ' 
        txtNroCuenta.Location = New Point(85, 53)
        txtNroCuenta.Margin = New Padding(4, 3, 4, 3)
        txtNroCuenta.Name = "txtNroCuenta"
        txtNroCuenta.Size = New Size(69, 23)
        txtNroCuenta.TabIndex = 7
        ' 
        ' LblCodigo
        ' 
        LblCodigo.AutoSize = True
        LblCodigo.Location = New Point(6, 27)
        LblCodigo.Margin = New Padding(4, 0, 4, 0)
        LblCodigo.Name = "LblCodigo"
        LblCodigo.Size = New Size(49, 15)
        LblCodigo.TabIndex = 2
        LblCodigo.Text = "Código:"
        ' 
        ' TxtCodigo
        ' 
        TxtCodigo.Location = New Point(85, 24)
        TxtCodigo.Margin = New Padding(4, 3, 4, 3)
        TxtCodigo.Name = "TxtCodigo"
        TxtCodigo.Size = New Size(69, 23)
        TxtCodigo.TabIndex = 3
        ' 
        ' LblDescripcion
        ' 
        LblDescripcion.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        LblDescripcion.AutoSize = True
        LblDescripcion.Location = New Point(165, 27)
        LblDescripcion.Margin = New Padding(4, 0, 4, 0)
        LblDescripcion.Name = "LblDescripcion"
        LblDescripcion.Size = New Size(72, 15)
        LblDescripcion.TabIndex = 4
        LblDescripcion.Text = "Descripción:"
        ' 
        ' TxtDescripcion
        ' 
        TxtDescripcion.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TxtDescripcion.Location = New Point(245, 22)
        TxtDescripcion.Margin = New Padding(4, 3, 4, 3)
        TxtDescripcion.Name = "TxtDescripcion"
        TxtDescripcion.Size = New Size(205, 23)
        TxtDescripcion.TabIndex = 5
        ' 
        ' CmdAgregar
        ' 
        CmdAgregar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        CmdAgregar.FlatStyle = FlatStyle.Flat
        CmdAgregar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdAgregar.Location = New Point(63, 425)
        CmdAgregar.Margin = New Padding(4, 3, 4, 3)
        CmdAgregar.Name = "CmdAgregar"
        CmdAgregar.Size = New Size(75, 30)
        CmdAgregar.TabIndex = 6
        CmdAgregar.Text = "Agregar"
        CmdAgregar.UseVisualStyleBackColor = True
        ' 
        ' CmdBorrar
        ' 
        CmdBorrar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        CmdBorrar.FlatStyle = FlatStyle.Flat
        CmdBorrar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdBorrar.Location = New Point(146, 425)
        CmdBorrar.Margin = New Padding(4, 3, 4, 3)
        CmdBorrar.Name = "CmdBorrar"
        CmdBorrar.Size = New Size(75, 30)
        CmdBorrar.TabIndex = 8
        CmdBorrar.Text = "Borrar"
        CmdBorrar.UseVisualStyleBackColor = True
        ' 
        ' CmdSalir
        ' 
        CmdSalir.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        CmdSalir.BackColor = Color.IndianRed
        CmdSalir.FlatStyle = FlatStyle.Flat
        CmdSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdSalir.ForeColor = Color.White
        CmdSalir.Location = New Point(393, 425)
        CmdSalir.Margin = New Padding(4, 3, 4, 3)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(75, 30)
        CmdSalir.TabIndex = 11
        CmdSalir.Text = "Salir"
        CmdSalir.UseVisualStyleBackColor = False
        ' 
        ' cmdAceptar
        ' 
        cmdAceptar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        cmdAceptar.FlatStyle = FlatStyle.Flat
        cmdAceptar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdAceptar.Location = New Point(229, 425)
        cmdAceptar.Margin = New Padding(4, 3, 4, 3)
        cmdAceptar.Name = "cmdAceptar"
        cmdAceptar.Size = New Size(75, 30)
        cmdAceptar.TabIndex = 12
        cmdAceptar.Text = "Aceptar"
        cmdAceptar.UseVisualStyleBackColor = True
        ' 
        ' CmdCancelar
        ' 
        CmdCancelar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        CmdCancelar.FlatStyle = FlatStyle.Flat
        CmdCancelar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdCancelar.Location = New Point(311, 425)
        CmdCancelar.Name = "CmdCancelar"
        CmdCancelar.Size = New Size(75, 30)
        CmdCancelar.TabIndex = 13
        CmdCancelar.Text = "Cancelar"
        CmdCancelar.UseVisualStyleBackColor = True
        ' 
        ' frmBancos
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(472, 462)
        Controls.Add(CmdCancelar)
        Controls.Add(cmdAceptar)
        Controls.Add(CmdSalir)
        Controls.Add(CmdBorrar)
        Controls.Add(CmdAgregar)
        Controls.Add(GroupBoxDatos)
        Controls.Add(DgvListado)
        Controls.Add(lnkCopiar)
        Controls.Add(chkEncabezados)
        Controls.Add(TxtBuscar)
        Controls.Add(LblBuscar)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(420, 397)
        Name = "frmBancos"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Bancos"
        CType(DgvListado, ComponentModel.ISupportInitialize).EndInit()
        GroupBoxDatos.ResumeLayout(False)
        GroupBoxDatos.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents LblBuscar As System.Windows.Forms.Label
    Friend WithEvents TxtBuscar As System.Windows.Forms.TextBox
    Friend WithEvents chkEncabezados As System.Windows.Forms.CheckBox
    Friend WithEvents lnkCopiar As System.Windows.Forms.LinkLabel
    Friend WithEvents DgvListado As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBoxDatos As System.Windows.Forms.GroupBox
    Friend WithEvents LblCodigo As System.Windows.Forms.Label
    Friend WithEvents TxtCodigo As System.Windows.Forms.TextBox
    Friend WithEvents LblDescripcion As System.Windows.Forms.Label
    Friend WithEvents TxtDescripcion As System.Windows.Forms.TextBox
    Friend WithEvents CmdAgregar As System.Windows.Forms.Button
    Friend WithEvents CmdBorrar As System.Windows.Forms.Button
    Friend WithEvents CmdSalir As System.Windows.Forms.Button
    Friend WithEvents cmdAceptar As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents txtNroCuenta As TextBox
    Friend WithEvents CmdCancelar As Button

End Class
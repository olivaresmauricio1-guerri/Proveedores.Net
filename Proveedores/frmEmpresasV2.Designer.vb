<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmEmpresasV2
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        LblBuscar = New Label()
        TxtBuscar = New TextBox()
        chkEncabezados = New CheckBox()
        lnkCopiar = New LinkLabel()
        DgvListado = New DataGridView()
        GroupBoxDatos = New GroupBox()
        LblCodigo = New Label()
        TxtCodigo = New TextBox()
        LblDescripcion = New Label()
        TxtDescripcion = New TextBox()
        LblCUIT = New Label()
        TxtCUIT = New TextBox()
        LblConMulti = New Label()
        TxtConMulti = New TextBox()
        CmdAgregar = New Button()
        CmdBorrar = New Button()
        CmdSalir = New Button()
        CmdAceptar = New Button()
        CmdCancelar = New Button()
        CType(DgvListado, ComponentModel.ISupportInitialize).BeginInit()
        GroupBoxDatos.SuspendLayout()
        SuspendLayout()
        ' LblBuscar
        LblBuscar.AutoSize = True
        LblBuscar.Location = New Point(7, 9)
        LblBuscar.Name = "LblBuscar"
        LblBuscar.Size = New Size(42, 15)
        LblBuscar.Text = "Buscar"
        ' TxtBuscar
        TxtBuscar.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TxtBuscar.Location = New Point(56, 6)
        TxtBuscar.Name = "TxtBuscar"
        TxtBuscar.Size = New Size(356, 23)
        TxtBuscar.TabIndex = 1
        ' chkEncabezados
        chkEncabezados.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        chkEncabezados.AutoSize = True
        chkEncabezados.Location = New Point(497, 268)
        chkEncabezados.Name = "chkEncabezados"
        chkEncabezados.Size = New Size(119, 19)
        chkEncabezados.Text = "Con encabezados"
        chkEncabezados.UseVisualStyleBackColor = True
        ' lnkCopiar
        lnkCopiar.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lnkCopiar.AutoSize = True
        lnkCopiar.LinkColor = Color.Black
        lnkCopiar.Location = New Point(397, 269)
        lnkCopiar.Name = "lnkCopiar"
        lnkCopiar.Size = New Size(94, 15)
        lnkCopiar.Text = "Copiar selección"
        ' DgvListado
        DgvListado.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DgvListado.Location = New Point(2, 35)
        DgvListado.MultiSelect = False
        DgvListado.Name = "DgvListado"
        DgvListado.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DgvListado.Size = New Size(614, 227)
        DgvListado.TabIndex = 4
        ' GroupBoxDatos
        GroupBoxDatos.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBoxDatos.Controls.Add(LblCodigo)
        GroupBoxDatos.Controls.Add(TxtCodigo)
        GroupBoxDatos.Controls.Add(LblDescripcion)
        GroupBoxDatos.Controls.Add(TxtDescripcion)
        GroupBoxDatos.Controls.Add(LblCUIT)
        GroupBoxDatos.Controls.Add(TxtCUIT)
        GroupBoxDatos.Controls.Add(LblConMulti)
        GroupBoxDatos.Controls.Add(TxtConMulti)
        GroupBoxDatos.Location = New Point(10, 288)
        GroupBoxDatos.Name = "GroupBoxDatos"
        GroupBoxDatos.Size = New Size(606, 104)
        GroupBoxDatos.TabStop = False
        GroupBoxDatos.Text = "Datos de Empresa"
        ' LblCodigo
        LblCodigo.AutoSize = True
        LblCodigo.Location = New Point(12, 22)
        LblCodigo.Name = "LblCodigo"
        LblCodigo.Text = "Código"
        ' TxtCodigo
        TxtCodigo.Location = New Point(64, 19)
        TxtCodigo.Name = "TxtCodigo"
        TxtCodigo.Size = New Size(80, 23)
        TxtCodigo.TabIndex = 1
        ' LblDescripcion
        LblDescripcion.AutoSize = True
        LblDescripcion.Location = New Point(153, 22)
        LblDescripcion.Name = "LblDescripcion"
        LblDescripcion.Text = "Descripción"
        ' TxtDescripcion
        TxtDescripcion.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TxtDescripcion.Location = New Point(231, 19)
        TxtDescripcion.Name = "TxtDescripcion"
        TxtDescripcion.Size = New Size(360, 23)
        TxtDescripcion.TabIndex = 3
        ' LblCUIT
        LblCUIT.AutoSize = True
        LblCUIT.Location = New Point(12, 50)
        LblCUIT.Name = "LblCUIT"
        LblCUIT.Text = "CUIT"
        ' TxtCUIT
        TxtCUIT.Location = New Point(64, 47)
        TxtCUIT.Name = "TxtCUIT"
        TxtCUIT.Size = New Size(160, 23)
        TxtCUIT.TabIndex = 5
        ' LblConMulti
        LblConMulti.AutoSize = True
        LblConMulti.Location = New Point(230, 50)
        LblConMulti.Name = "LblConMulti"
        LblConMulti.Text = "ConMulti"
        ' TxtConMulti
        TxtConMulti.Location = New Point(300, 47)
        TxtConMulti.Name = "TxtConMulti"
        TxtConMulti.Size = New Size(120, 23)
        TxtConMulti.TabIndex = 7
        ' CmdAgregar
        CmdAgregar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        CmdAgregar.FlatStyle = FlatStyle.Flat
        CmdAgregar.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        CmdAgregar.Location = New Point(217, 399)
        CmdAgregar.Name = "CmdAgregar"
        CmdAgregar.Size = New Size(75, 28)
        CmdAgregar.Text = "Agregar"
        CmdAgregar.UseVisualStyleBackColor = True
        ' CmdBorrar
        CmdBorrar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        CmdBorrar.FlatStyle = FlatStyle.Flat
        CmdBorrar.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        CmdBorrar.Location = New Point(298, 399)
        CmdBorrar.Name = "CmdBorrar"
        CmdBorrar.Size = New Size(75, 28)
        CmdBorrar.Text = "Borrar"
        CmdBorrar.UseVisualStyleBackColor = True
        ' CmdSalir
        CmdSalir.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        CmdSalir.BackColor = Color.IndianRed
        CmdSalir.FlatStyle = FlatStyle.Flat
        CmdSalir.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        CmdSalir.ForeColor = Color.White
        CmdSalir.Location = New Point(541, 398)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(75, 28)
        CmdSalir.Text = "Salir"
        CmdSalir.UseVisualStyleBackColor = False
        ' CmdAceptar
        CmdAceptar.Anchor = AnchorStyles.Bottom
        CmdAceptar.FlatStyle = FlatStyle.Flat
        CmdAceptar.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        CmdAceptar.Location = New Point(379, 399)
        CmdAceptar.Name = "CmdAceptar"
        CmdAceptar.Size = New Size(75, 28)
        CmdAceptar.Text = "Aceptar"
        CmdAceptar.UseVisualStyleBackColor = True
        ' CmdCancelar
        CmdCancelar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        CmdCancelar.FlatStyle = FlatStyle.Flat
        CmdCancelar.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        CmdCancelar.Location = New Point(460, 399)
        CmdCancelar.Name = "CmdCancelar"
        CmdCancelar.Size = New Size(75, 28)
        CmdCancelar.Text = "Cancelar"
        CmdCancelar.UseVisualStyleBackColor = True
        ' frmEmpresasV2
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(618, 432)
        Controls.Add(CmdCancelar)
        Controls.Add(CmdAceptar)
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
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmEmpresasV2"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Empresas (V2)"
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
    Friend WithEvents LblCodigo As Label
    Friend WithEvents TxtCodigo As TextBox
    Friend WithEvents LblDescripcion As Label
    Friend WithEvents TxtDescripcion As TextBox
    Friend WithEvents LblCUIT As Label
    Friend WithEvents TxtCUIT As TextBox
    Friend WithEvents LblConMulti As Label
    Friend WithEvents TxtConMulti As TextBox
    Friend WithEvents CmdAgregar As Button
    Friend WithEvents CmdBorrar As Button
    Friend WithEvents CmdSalir As Button
    Friend WithEvents CmdAceptar As Button
    Friend WithEvents CmdCancelar As Button
End Class
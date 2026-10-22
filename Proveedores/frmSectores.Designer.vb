<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSectores
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
        LblBuscar = New Label()
        TxtBuscar = New TextBox()
        DgvListado = New DataGridView()
        ColSectorCodigo = New DataGridViewTextBoxColumn()
        ColSectorDescripcion = New DataGridViewTextBoxColumn()
        ColSectorActivo = New DataGridViewCheckBoxColumn()
        lnkCopiar = New LinkLabel()
        chkEncabezados = New CheckBox()
        GroupBoxDatos = New GroupBox()
        LblCodigo = New Label()
        TxtCodigo = New TextBox()
        LblDescripcion = New Label()
        TxtDescripcion = New TextBox()
        ChkActivo = New CheckBox()
        CmdAgregar = New Button()
        btnModificar = New Button()
        CmdEstado = New Button()
        CmdAceptar = New Button()
        CmdCancelar = New Button()
        CmdSalir = New Button()
        CType(DgvListado, ComponentModel.ISupportInitialize).BeginInit()
        GroupBoxDatos.SuspendLayout()
        SuspendLayout()
        ' 
        ' LblBuscar
        ' 
        LblBuscar.AutoSize = True
        LblBuscar.Location = New Point(12, 15)
        LblBuscar.Name = "LblBuscar"
        LblBuscar.Size = New Size(42, 15)
        LblBuscar.TabIndex = 0
        LblBuscar.Text = "Buscar"
        ' 
        ' TxtBuscar
        ' 
        TxtBuscar.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TxtBuscar.Location = New Point(62, 12)
        TxtBuscar.Name = "TxtBuscar"
        TxtBuscar.Size = New Size(586, 23)
        TxtBuscar.TabIndex = 0
        ' 
        ' DgvListado
        ' 
        DgvListado.AutoGenerateColumns = False
        DgvListado.Columns.AddRange(New DataGridViewColumn() {ColSectorCodigo, ColSectorDescripcion, ColSectorActivo})
        ColSectorCodigo.Name = "IdSector"
        ColSectorCodigo.DataPropertyName = "IdSector"
        ColSectorCodigo.HeaderText = "Código"
        ColSectorCodigo.Width = 80
        ColSectorDescripcion.Name = "Descripcion"
        ColSectorDescripcion.DataPropertyName = "Descripcion"
        ColSectorDescripcion.HeaderText = "Descripción"
        ColSectorDescripcion.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        ColSectorDescripcion.MinimumWidth = 180
        ColSectorActivo.Name = "Activo"
        ColSectorActivo.DataPropertyName = "Activo"
        ColSectorActivo.HeaderText = "Activo"
        ColSectorActivo.Width = 65
        DgvListado.AllowUserToAddRows = False
        DgvListado.AllowUserToDeleteRows = False
        DgvListado.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DgvListado.Location = New Point(12, 44)
        DgvListado.MultiSelect = False
        DgvListado.Name = "DgvListado"
        DgvListado.ReadOnly = True
        DgvListado.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DgvListado.Size = New Size(636, 232)
        DgvListado.TabIndex = 1
        ' 
        ' lnkCopiar
        ' 
        lnkCopiar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        lnkCopiar.AutoSize = True
        lnkCopiar.LinkColor = Color.Black
        lnkCopiar.Location = New Point(421, 286)
        lnkCopiar.Name = "lnkCopiar"
        lnkCopiar.Size = New Size(94, 15)
        lnkCopiar.TabIndex = 2
        lnkCopiar.TabStop = True
        lnkCopiar.Text = "Copiar selección"
        ' 
        ' chkEncabezados
        ' 
        chkEncabezados.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        chkEncabezados.AutoSize = True
        chkEncabezados.Checked = True
        chkEncabezados.CheckState = CheckState.Checked
        chkEncabezados.Location = New Point(529, 285)
        chkEncabezados.Name = "chkEncabezados"
        chkEncabezados.Size = New Size(119, 19)
        chkEncabezados.TabIndex = 3
        chkEncabezados.Text = "Con encabezados"
        chkEncabezados.UseVisualStyleBackColor = True
        ' 
        ' GroupBoxDatos
        ' 
        GroupBoxDatos.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBoxDatos.Controls.Add(LblCodigo)
        GroupBoxDatos.Controls.Add(TxtCodigo)
        GroupBoxDatos.Controls.Add(LblDescripcion)
        GroupBoxDatos.Controls.Add(TxtDescripcion)
        GroupBoxDatos.Controls.Add(ChkActivo)
        GroupBoxDatos.Location = New Point(12, 310)
        GroupBoxDatos.Name = "GroupBoxDatos"
        GroupBoxDatos.Size = New Size(636, 97)
        GroupBoxDatos.TabIndex = 4
        GroupBoxDatos.TabStop = False
        GroupBoxDatos.Text = "Datos del sector"
        ' 
        ' LblCodigo
        ' 
        LblCodigo.AutoSize = True
        LblCodigo.Location = New Point(12, 25)
        LblCodigo.Name = "LblCodigo"
        LblCodigo.Size = New Size(46, 15)
        LblCodigo.TabIndex = 0
        LblCodigo.Text = "Código"
        ' 
        ' TxtCodigo
        ' 
        TxtCodigo.Location = New Point(96, 22)
        TxtCodigo.Name = "TxtCodigo"
        TxtCodigo.ReadOnly = True
        TxtCodigo.Size = New Size(100, 23)
        TxtCodigo.TabIndex = 0
        TxtCodigo.TabStop = False
        ' 
        ' LblDescripcion
        ' 
        LblDescripcion.AutoSize = True
        LblDescripcion.Location = New Point(12, 60)
        LblDescripcion.Name = "LblDescripcion"
        LblDescripcion.Size = New Size(69, 15)
        LblDescripcion.TabIndex = 1
        LblDescripcion.Text = "Descripción"
        ' 
        ' TxtDescripcion
        ' 
        TxtDescripcion.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TxtDescripcion.Location = New Point(96, 57)
        TxtDescripcion.MaxLength = 100
        TxtDescripcion.Name = "TxtDescripcion"
        TxtDescripcion.Size = New Size(526, 23)
        TxtDescripcion.TabIndex = 1
        ' 
        ' ChkActivo
        ' 
        ChkActivo.AutoSize = True
        ChkActivo.Location = New Point(220, 24)
        ChkActivo.Name = "ChkActivo"
        ChkActivo.Size = New Size(60, 19)
        ChkActivo.TabIndex = 2
        ChkActivo.Text = "Activo"
        ChkActivo.UseVisualStyleBackColor = True
        ' 
        ' CmdAgregar
        ' 
        CmdAgregar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        CmdAgregar.FlatStyle = FlatStyle.Flat
        CmdAgregar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdAgregar.Location = New Point(12, 420)
        CmdAgregar.Name = "CmdAgregar"
        CmdAgregar.Size = New Size(98, 29)
        CmdAgregar.TabIndex = 5
        CmdAgregar.Text = "Agregar"
        CmdAgregar.UseVisualStyleBackColor = True
        ' 
        ' btnModificar
        ' 
        btnModificar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnModificar.FlatStyle = FlatStyle.Flat
        btnModificar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnModificar.Location = New Point(118, 420)
        btnModificar.Name = "btnModificar"
        btnModificar.Size = New Size(98, 29)
        btnModificar.TabIndex = 6
        btnModificar.Text = "Modificar"
        btnModificar.UseVisualStyleBackColor = True
        ' 
        ' CmdEstado
        ' 
        CmdEstado.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        CmdEstado.FlatStyle = FlatStyle.Flat
        CmdEstado.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdEstado.Location = New Point(224, 420)
        CmdEstado.Name = "CmdEstado"
        CmdEstado.Size = New Size(98, 29)
        CmdEstado.TabIndex = 7
        CmdEstado.Text = "Eliminar"
        CmdEstado.UseVisualStyleBackColor = True
        ' 
        ' CmdAceptar
        ' 
        CmdAceptar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        CmdAceptar.FlatStyle = FlatStyle.Flat
        CmdAceptar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdAceptar.Location = New Point(330, 420)
        CmdAceptar.Name = "CmdAceptar"
        CmdAceptar.Size = New Size(98, 29)
        CmdAceptar.TabIndex = 8
        CmdAceptar.Text = "Aceptar"
        CmdAceptar.UseVisualStyleBackColor = True
        ' 
        ' CmdCancelar
        ' 
        CmdCancelar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        CmdCancelar.FlatStyle = FlatStyle.Flat
        CmdCancelar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdCancelar.Location = New Point(436, 420)
        CmdCancelar.Name = "CmdCancelar"
        CmdCancelar.Size = New Size(98, 29)
        CmdCancelar.TabIndex = 9
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
        CmdSalir.Location = New Point(542, 420)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(106, 29)
        CmdSalir.TabIndex = 10
        CmdSalir.Text = "Salir"
        CmdSalir.UseVisualStyleBackColor = False
        ' 
        ' frmSectores
        ' 
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        ClientSize = New Size(660, 461)
        Controls.Add(LblBuscar)
        Controls.Add(TxtBuscar)
        Controls.Add(DgvListado)
        Controls.Add(lnkCopiar)
        Controls.Add(chkEncabezados)
        Controls.Add(GroupBoxDatos)
        Controls.Add(CmdAgregar)
        Controls.Add(btnModificar)
        Controls.Add(CmdEstado)
        Controls.Add(CmdAceptar)
        Controls.Add(CmdCancelar)
        Controls.Add(CmdSalir)
        MinimumSize = New Size(676, 500)
        Name = "frmSectores"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Sectores"
        CType(DgvListado, ComponentModel.ISupportInitialize).EndInit()
        GroupBoxDatos.ResumeLayout(False)
        GroupBoxDatos.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents ColSectorCodigo As DataGridViewTextBoxColumn
    Friend WithEvents ColSectorDescripcion As DataGridViewTextBoxColumn
    Friend WithEvents ColSectorActivo As DataGridViewCheckBoxColumn
    Friend WithEvents LblBuscar As Label
    Friend WithEvents TxtBuscar As TextBox
    Friend WithEvents DgvListado As DataGridView
    Friend WithEvents lnkCopiar As LinkLabel
    Friend WithEvents chkEncabezados As CheckBox
    Friend WithEvents GroupBoxDatos As GroupBox
    Friend WithEvents LblCodigo As Label
    Friend WithEvents TxtCodigo As TextBox
    Friend WithEvents LblDescripcion As Label
    Friend WithEvents TxtDescripcion As TextBox
    Friend WithEvents ChkActivo As CheckBox
    Friend WithEvents CmdAgregar As Button
    Friend WithEvents btnModificar As Button
    Friend WithEvents CmdEstado As Button
    Friend WithEvents CmdAceptar As Button
    Friend WithEvents CmdCancelar As Button
    Friend WithEvents CmdSalir As Button
End Class

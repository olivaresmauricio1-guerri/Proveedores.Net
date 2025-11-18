<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCondicionVenta
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
        LblCtaContable = New Label()
        txtCtaContable = New TextBox()
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
        TxtBuscar.Size = New Size(356, 23)
        TxtBuscar.TabIndex = 1
        ' 
        ' chkEncabezados
        ' 
        chkEncabezados.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        chkEncabezados.AutoSize = True
        chkEncabezados.Location = New Point(482, 304)
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
        lnkCopiar.Location = New Point(382, 305)
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
        DgvListado.Size = New Size(614, 264)
        DgvListado.TabIndex = 4
        ' 
        ' GroupBoxDatos
        ' 
        GroupBoxDatos.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBoxDatos.Controls.Add(LblCodigo)
        GroupBoxDatos.Controls.Add(TxtCodigo)
        GroupBoxDatos.Controls.Add(LblDescripcion)
        GroupBoxDatos.Controls.Add(TxtDescripcion)
        GroupBoxDatos.Controls.Add(LblCtaContable)
        GroupBoxDatos.Controls.Add(txtCtaContable)
        GroupBoxDatos.Location = New Point(10, 329)
        GroupBoxDatos.Name = "GroupBoxDatos"
        GroupBoxDatos.Size = New Size(606, 78)
        GroupBoxDatos.TabIndex = 5
        GroupBoxDatos.TabStop = False
        GroupBoxDatos.Text = "Datos de Condición de Venta"
        ' 
        ' LblCodigo
        ' 
        LblCodigo.AutoSize = True
        LblCodigo.Location = New Point(6, 22)
        LblCodigo.Name = "LblCodigo"
        LblCodigo.Size = New Size(46, 15)
        LblCodigo.TabIndex = 0
        LblCodigo.Text = "Código"
        ' 
        ' TxtCodigo
        ' 
        TxtCodigo.Location = New Point(84, 19)
        TxtCodigo.Name = "TxtCodigo"
        TxtCodigo.Size = New Size(80, 23)
        TxtCodigo.TabIndex = 1
        ' 
        ' LblDescripcion
        ' 
        LblDescripcion.AutoSize = True
        LblDescripcion.Location = New Point(6, 52)
        LblDescripcion.Name = "LblDescripcion"
        LblDescripcion.Size = New Size(69, 15)
        LblDescripcion.TabIndex = 2
        LblDescripcion.Text = "Descripción"
        ' 
        ' TxtDescripcion
        ' 
        TxtDescripcion.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TxtDescripcion.Location = New Point(84, 49)
        TxtDescripcion.Name = "TxtDescripcion"
        TxtDescripcion.Size = New Size(360, 23)
        TxtDescripcion.TabIndex = 3
        ' 
        ' LblCtaContable
        ' 
        LblCtaContable.AutoSize = True
        LblCtaContable.Location = New Point(184, 22)
        LblCtaContable.Name = "LblCtaContable"
        LblCtaContable.Size = New Size(96, 15)
        LblCtaContable.TabIndex = 4
        LblCtaContable.Text = "Cuenta Contable"
        ' 
        ' txtCtaContable
        ' 
        txtCtaContable.Location = New Point(284, 19)
        txtCtaContable.Name = "txtCtaContable"
        txtCtaContable.Size = New Size(160, 23)
        txtCtaContable.TabIndex = 5
        ' 
        ' CmdAgregar
        ' 
        CmdAgregar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        CmdAgregar.FlatStyle = FlatStyle.Flat
        CmdAgregar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdAgregar.Location = New Point(217, 413)
        CmdAgregar.Name = "CmdAgregar"
        CmdAgregar.Size = New Size(75, 28)
        CmdAgregar.TabIndex = 6
        CmdAgregar.Text = "Agregar"
        CmdAgregar.UseVisualStyleBackColor = True
        ' 
        ' CmdBorrar
        ' 
        CmdBorrar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        CmdBorrar.FlatStyle = FlatStyle.Flat
        CmdBorrar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdBorrar.Location = New Point(298, 413)
        CmdBorrar.Name = "CmdBorrar"
        CmdBorrar.Size = New Size(75, 28)
        CmdBorrar.TabIndex = 7
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
        CmdSalir.Location = New Point(541, 413)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(75, 28)
        CmdSalir.TabIndex = 9
        CmdSalir.Text = "Salir"
        CmdSalir.UseVisualStyleBackColor = False
        ' 
        ' cmdAceptar
        ' 
        cmdAceptar.Anchor = AnchorStyles.Bottom
        cmdAceptar.FlatStyle = FlatStyle.Flat
        cmdAceptar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdAceptar.Location = New Point(379, 413)
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
        CmdCancelar.Location = New Point(460, 413)
        CmdCancelar.Name = "CmdCancelar"
        CmdCancelar.Size = New Size(75, 28)
        CmdCancelar.TabIndex = 14
        CmdCancelar.Text = "Cancelar"
        CmdCancelar.UseVisualStyleBackColor = True
        ' 
        ' frmCondicionVenta
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(618, 447)
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
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmCondicionVenta"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Condiciones de Venta"
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
    Friend WithEvents LblCtaContable As System.Windows.Forms.Label
    Friend WithEvents txtCtaContable As System.Windows.Forms.TextBox
    Friend WithEvents CmdAgregar As System.Windows.Forms.Button
    Friend WithEvents CmdBorrar As System.Windows.Forms.Button
    Friend WithEvents CmdSalir As System.Windows.Forms.Button
    Friend WithEvents cmdAceptar As System.Windows.Forms.Button
    Friend WithEvents CmdCancelar As Button

End Class

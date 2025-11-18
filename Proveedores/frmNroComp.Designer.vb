<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNroComp
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
        LblBuscar = New Label()
        TxtBuscar = New TextBox()
        chkEncabezados = New CheckBox()
        lnkCopiar = New LinkLabel()
        DgvListado = New DataGridView()
        GroupBoxDatos = New GroupBox()
        LblIdComprob = New Label()
        TxtIdComprob = New TextBox()
        LblNroComprob = New Label()
        TxtNroComprob = New TextBox()
        LblDescripcion = New Label()
        TxtDescripcion = New TextBox()
        LblSRC = New Label()
        TxtSRC = New TextBox()
        LblAlCierre = New Label()
        ChkAlCierre = New CheckBox()
        LblImputaStk = New Label()
        LblImputaCC = New Label()
        CmdAgregar = New Button()
        CmdBorrar = New Button()
        CmdSalir = New Button()
        cmdAceptar = New Button()
        CmdCancelar = New Button()
        txtImputaStk = New TextBox()
        txtImputaCC = New TextBox()
        CType(DgvListado, ComponentModel.ISupportInitialize).BeginInit()
        GroupBoxDatos.SuspendLayout()
        SuspendLayout()
        ' 
        ' LblBuscar
        ' 
        LblBuscar.AutoSize = True
        LblBuscar.Location = New Point(12, 9)
        LblBuscar.Name = "LblBuscar"
        LblBuscar.Size = New Size(42, 15)
        LblBuscar.TabIndex = 0
        LblBuscar.Text = "Buscar"
        ' 
        ' TxtBuscar
        ' 
        TxtBuscar.Location = New Point(63, 6)
        TxtBuscar.Name = "TxtBuscar"
        TxtBuscar.Size = New Size(320, 23)
        TxtBuscar.TabIndex = 1
        ' 
        ' chkEncabezados
        ' 
        chkEncabezados.AutoSize = True
        chkEncabezados.Location = New Point(653, 284)
        chkEncabezados.Name = "chkEncabezados"
        chkEncabezados.Size = New Size(119, 19)
        chkEncabezados.TabIndex = 2
        chkEncabezados.Text = "Con encabezados"
        chkEncabezados.UseVisualStyleBackColor = True
        ' 
        ' lnkCopiar
        ' 
        lnkCopiar.AutoSize = True
        lnkCopiar.LinkColor = Color.Black
        lnkCopiar.Location = New Point(553, 284)
        lnkCopiar.Name = "lnkCopiar"
        lnkCopiar.Size = New Size(94, 15)
        lnkCopiar.TabIndex = 3
        lnkCopiar.TabStop = True
        lnkCopiar.Text = "Copiar selección"
        lnkCopiar.VisitedLinkColor = Color.Black
        ' 
        ' DgvListado
        ' 
        DgvListado.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        DgvListado.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DgvListado.Location = New Point(12, 35)
        DgvListado.Name = "DgvListado"
        DgvListado.Size = New Size(760, 240)
        DgvListado.TabIndex = 4
        ' 
        ' GroupBoxDatos
        ' 
        GroupBoxDatos.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBoxDatos.Controls.Add(txtImputaCC)
        GroupBoxDatos.Controls.Add(txtImputaStk)
        GroupBoxDatos.Controls.Add(LblIdComprob)
        GroupBoxDatos.Controls.Add(TxtIdComprob)
        GroupBoxDatos.Controls.Add(LblNroComprob)
        GroupBoxDatos.Controls.Add(TxtNroComprob)
        GroupBoxDatos.Controls.Add(LblDescripcion)
        GroupBoxDatos.Controls.Add(TxtDescripcion)
        GroupBoxDatos.Controls.Add(LblSRC)
        GroupBoxDatos.Controls.Add(TxtSRC)
        GroupBoxDatos.Controls.Add(LblAlCierre)
        GroupBoxDatos.Controls.Add(ChkAlCierre)
        GroupBoxDatos.Controls.Add(LblImputaStk)
        GroupBoxDatos.Controls.Add(LblImputaCC)
        GroupBoxDatos.Location = New Point(12, 309)
        GroupBoxDatos.Name = "GroupBoxDatos"
        GroupBoxDatos.Size = New Size(760, 92)
        GroupBoxDatos.TabIndex = 5
        GroupBoxDatos.TabStop = False
        GroupBoxDatos.Text = "Datos"
        ' 
        ' LblIdComprob
        ' 
        LblIdComprob.AutoSize = True
        LblIdComprob.Location = New Point(12, 28)
        LblIdComprob.Name = "LblIdComprob"
        LblIdComprob.Size = New Size(45, 15)
        LblIdComprob.TabIndex = 0
        LblIdComprob.Text = "Id Cbte"
        ' 
        ' TxtIdComprob
        ' 
        TxtIdComprob.Location = New Point(87, 25)
        TxtIdComprob.Name = "TxtIdComprob"
        TxtIdComprob.Size = New Size(80, 23)
        TxtIdComprob.TabIndex = 1
        ' 
        ' LblNroComprob
        ' 
        LblNroComprob.AutoSize = True
        LblNroComprob.Location = New Point(180, 28)
        LblNroComprob.Name = "LblNroComprob"
        LblNroComprob.Size = New Size(104, 15)
        LblNroComprob.TabIndex = 2
        LblNroComprob.Text = "Nro Comprobante"
        ' 
        ' TxtNroComprob
        ' 
        TxtNroComprob.Location = New Point(290, 25)
        TxtNroComprob.Name = "TxtNroComprob"
        TxtNroComprob.Size = New Size(90, 23)
        TxtNroComprob.TabIndex = 3
        ' 
        ' LblDescripcion
        ' 
        LblDescripcion.AutoSize = True
        LblDescripcion.Location = New Point(12, 57)
        LblDescripcion.Name = "LblDescripcion"
        LblDescripcion.Size = New Size(69, 15)
        LblDescripcion.TabIndex = 4
        LblDescripcion.Text = "Descripcion"
        ' 
        ' TxtDescripcion
        ' 
        TxtDescripcion.Location = New Point(87, 54)
        TxtDescripcion.Name = "TxtDescripcion"
        TxtDescripcion.Size = New Size(293, 23)
        TxtDescripcion.TabIndex = 5
        ' 
        ' LblSRC
        ' 
        LblSRC.AutoSize = True
        LblSRC.Location = New Point(386, 28)
        LblSRC.Name = "LblSRC"
        LblSRC.Size = New Size(28, 15)
        LblSRC.TabIndex = 6
        LblSRC.Text = "SRC"
        ' 
        ' TxtSRC
        ' 
        TxtSRC.Location = New Point(471, 22)
        TxtSRC.Name = "TxtSRC"
        TxtSRC.Size = New Size(90, 23)
        TxtSRC.TabIndex = 7
        ' 
        ' LblAlCierre
        ' 
        LblAlCierre.AutoSize = True
        LblAlCierre.Location = New Point(567, 25)
        LblAlCierre.Name = "LblAlCierre"
        LblAlCierre.Size = New Size(49, 15)
        LblAlCierre.TabIndex = 8
        LblAlCierre.Text = "AlCierre"
        ' 
        ' ChkAlCierre
        ' 
        ChkAlCierre.Location = New Point(631, 25)
        ChkAlCierre.Name = "ChkAlCierre"
        ChkAlCierre.Size = New Size(15, 14)
        ChkAlCierre.TabIndex = 9
        ChkAlCierre.UseVisualStyleBackColor = True
        ' 
        ' LblImputaStk
        ' 
        LblImputaStk.AutoSize = True
        LblImputaStk.Location = New Point(388, 57)
        LblImputaStk.Name = "LblImputaStk"
        LblImputaStk.Size = New Size(77, 15)
        LblImputaStk.TabIndex = 10
        LblImputaStk.Text = "Imputa Stock"
        ' 
        ' LblImputaCC
        ' 
        LblImputaCC.AutoSize = True
        LblImputaCC.Location = New Point(567, 57)
        LblImputaCC.Name = "LblImputaCC"
        LblImputaCC.Size = New Size(84, 15)
        LblImputaCC.TabIndex = 12
        LblImputaCC.Text = "Imputa CtaCte"
        ' 
        ' CmdAgregar
        ' 
        CmdAgregar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        CmdAgregar.FlatStyle = FlatStyle.Flat
        CmdAgregar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdAgregar.Location = New Point(358, 407)
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
        CmdBorrar.Location = New Point(439, 407)
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
        CmdSalir.Location = New Point(682, 407)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(75, 28)
        CmdSalir.TabIndex = 10
        CmdSalir.Text = "Salir"
        CmdSalir.UseVisualStyleBackColor = False
        ' 
        ' cmdAceptar
        ' 
        cmdAceptar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        cmdAceptar.FlatStyle = FlatStyle.Flat
        cmdAceptar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdAceptar.Location = New Point(520, 407)
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
        CmdCancelar.Location = New Point(601, 407)
        CmdCancelar.Name = "CmdCancelar"
        CmdCancelar.Size = New Size(75, 28)
        CmdCancelar.TabIndex = 9
        CmdCancelar.Text = "Cancelar"
        CmdCancelar.UseVisualStyleBackColor = True
        ' 
        ' txtImputaStk
        ' 
        txtImputaStk.Location = New Point(471, 54)
        txtImputaStk.Name = "txtImputaStk"
        txtImputaStk.Size = New Size(90, 23)
        txtImputaStk.TabIndex = 13
        ' 
        ' txtImputaCC
        ' 
        txtImputaCC.Location = New Point(657, 54)
        txtImputaCC.Name = "txtImputaCC"
        txtImputaCC.Size = New Size(90, 23)
        txtImputaCC.TabIndex = 14
        ' 
        ' frmNroComp
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(784, 446)
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
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmNroComp"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Números Comprobantes"
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
    Friend WithEvents LblIdComprob As System.Windows.Forms.Label
    Friend WithEvents TxtIdComprob As System.Windows.Forms.TextBox
    Friend WithEvents LblNroComprob As System.Windows.Forms.Label
    Friend WithEvents TxtNroComprob As System.Windows.Forms.TextBox
    Friend WithEvents LblDescripcion As System.Windows.Forms.Label
    Friend WithEvents TxtDescripcion As System.Windows.Forms.TextBox
    Friend WithEvents LblSRC As System.Windows.Forms.Label
    Friend WithEvents TxtSRC As System.Windows.Forms.TextBox
    Friend WithEvents LblAlCierre As System.Windows.Forms.Label
    Friend WithEvents ChkAlCierre As System.Windows.Forms.CheckBox
    Friend WithEvents LblImputaStk As System.Windows.Forms.Label
    Friend WithEvents LblImputaCC As System.Windows.Forms.Label
    Friend WithEvents CmdAgregar As System.Windows.Forms.Button
    Friend WithEvents CmdBorrar As System.Windows.Forms.Button
    Friend WithEvents CmdSalir As System.Windows.Forms.Button
    Friend WithEvents cmdAceptar As System.Windows.Forms.Button
    Friend WithEvents CmdCancelar As System.Windows.Forms.Button
    Friend WithEvents txtImputaCC As TextBox
    Friend WithEvents txtImputaStk As TextBox
End Class

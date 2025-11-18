<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIva
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
        LblIva = New Label()
        TxtIva = New TextBox()
        LblNoinscripto = New Label()
        LblIBrutos = New Label()
        CmdAgregar = New Button()
        CmdBorrar = New Button()
        CmdSalir = New Button()
        cmdAceptar = New Button()
        CmdCancelar = New Button()
        TxtNoInscripto = New TextBox()
        TxtIBrutos = New TextBox()
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
        chkEncabezados.Location = New Point(653, 283)
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
        lnkCopiar.Location = New Point(553, 283)
        lnkCopiar.Name = "lnkCopiar"
        lnkCopiar.Size = New Size(94, 15)
        lnkCopiar.TabIndex = 3
        lnkCopiar.TabStop = True
        lnkCopiar.Text = "Copiar selección"
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
        GroupBoxDatos.Controls.Add(TxtIBrutos)
        GroupBoxDatos.Controls.Add(TxtNoInscripto)
        GroupBoxDatos.Controls.Add(LblCodigo)
        GroupBoxDatos.Controls.Add(TxtCodigo)
        GroupBoxDatos.Controls.Add(LblDescripcion)
        GroupBoxDatos.Controls.Add(TxtDescripcion)
        GroupBoxDatos.Controls.Add(LblIva)
        GroupBoxDatos.Controls.Add(TxtIva)
        GroupBoxDatos.Controls.Add(LblNoinscripto)
        GroupBoxDatos.Controls.Add(LblIBrutos)
        GroupBoxDatos.Location = New Point(12, 308)
        GroupBoxDatos.Name = "GroupBoxDatos"
        GroupBoxDatos.Size = New Size(760, 93)
        GroupBoxDatos.TabIndex = 5
        GroupBoxDatos.TabStop = False
        GroupBoxDatos.Text = "Datos"
        ' 
        ' LblCodigo
        ' 
        LblCodigo.AutoSize = True
        LblCodigo.Location = New Point(12, 28)
        LblCodigo.Name = "LblCodigo"
        LblCodigo.Size = New Size(46, 15)
        LblCodigo.TabIndex = 0
        LblCodigo.Text = "Codigo"
        ' 
        ' TxtCodigo
        ' 
        TxtCodigo.Location = New Point(67, 25)
        TxtCodigo.Name = "TxtCodigo"
        TxtCodigo.Size = New Size(90, 23)
        TxtCodigo.TabIndex = 1
        ' 
        ' LblDescripcion
        ' 
        LblDescripcion.AutoSize = True
        LblDescripcion.Location = New Point(163, 28)
        LblDescripcion.Name = "LblDescripcion"
        LblDescripcion.Size = New Size(69, 15)
        LblDescripcion.TabIndex = 2
        LblDescripcion.Text = "Descripcion"
        ' 
        ' TxtDescripcion
        ' 
        TxtDescripcion.Location = New Point(241, 25)
        TxtDescripcion.Name = "TxtDescripcion"
        TxtDescripcion.Size = New Size(250, 23)
        TxtDescripcion.TabIndex = 3
        ' 
        ' LblIva
        ' 
        LblIva.AutoSize = True
        LblIva.Location = New Point(12, 54)
        LblIva.Name = "LblIva"
        LblIva.Size = New Size(22, 15)
        LblIva.TabIndex = 4
        LblIva.Text = "Iva"
        ' 
        ' TxtIva
        ' 
        TxtIva.Location = New Point(67, 54)
        TxtIva.Name = "TxtIva"
        TxtIva.Size = New Size(90, 23)
        TxtIva.TabIndex = 5
        ' 
        ' LblNoinscripto
        ' 
        LblNoinscripto.AutoSize = True
        LblNoinscripto.Location = New Point(163, 57)
        LblNoinscripto.Name = "LblNoinscripto"
        LblNoinscripto.Size = New Size(72, 15)
        LblNoinscripto.TabIndex = 6
        LblNoinscripto.Text = "No Inscripto"
        ' 
        ' LblIBrutos
        ' 
        LblIBrutos.AutoSize = True
        LblIBrutos.Location = New Point(337, 57)
        LblIBrutos.Name = "LblIBrutos"
        LblIBrutos.Size = New Size(44, 15)
        LblIBrutos.TabIndex = 8
        LblIBrutos.Text = "IBrutos"
        ' 
        ' CmdAgregar
        ' 
        CmdAgregar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        CmdAgregar.FlatStyle = FlatStyle.Flat
        CmdAgregar.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        CmdAgregar.Location = New Point(374, 407)
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
        CmdBorrar.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        CmdBorrar.Location = New Point(455, 407)
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
        CmdSalir.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        CmdSalir.ForeColor = Color.White
        CmdSalir.Location = New Point(698, 407)
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
        cmdAceptar.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        cmdAceptar.Location = New Point(536, 407)
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
        CmdCancelar.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        CmdCancelar.Location = New Point(617, 407)
        CmdCancelar.Name = "CmdCancelar"
        CmdCancelar.Size = New Size(75, 28)
        CmdCancelar.TabIndex = 9
        CmdCancelar.Text = "Cancelar"
        CmdCancelar.UseVisualStyleBackColor = True
        ' 
        ' TxtNoInscripto
        ' 
        TxtNoInscripto.Location = New Point(241, 54)
        TxtNoInscripto.Name = "TxtNoInscripto"
        TxtNoInscripto.Size = New Size(90, 23)
        TxtNoInscripto.TabIndex = 10
        ' 
        ' TxtIBrutos
        ' 
        TxtIBrutos.Location = New Point(387, 54)
        TxtIBrutos.Name = "TxtIBrutos"
        TxtIBrutos.Size = New Size(90, 23)
        TxtIBrutos.TabIndex = 11
        ' 
        ' frmIva
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
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
        Name = "frmIva"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Tipos de Iva"
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
    Friend WithEvents LblIva As System.Windows.Forms.Label
    Friend WithEvents TxtIva As System.Windows.Forms.TextBox
    Friend WithEvents LblNoinscripto As System.Windows.Forms.Label
    Friend WithEvents LblIBrutos As System.Windows.Forms.Label
    Friend WithEvents CmdAgregar As System.Windows.Forms.Button
    Friend WithEvents CmdBorrar As System.Windows.Forms.Button
    Friend WithEvents CmdSalir As System.Windows.Forms.Button
    Friend WithEvents cmdAceptar As System.Windows.Forms.Button
    Friend WithEvents CmdCancelar As System.Windows.Forms.Button
    Friend WithEvents TxtIBrutos As TextBox
    Friend WithEvents TxtNoInscripto As TextBox
End Class

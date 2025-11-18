<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSucursales
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
        LblCodigo = New Label()
        TxtCodigo = New TextBox()
        LblDeposito = New Label()
        TxtDeposito = New TextBox()
        LblDescripcion = New Label()
        TxtDescripcion = New TextBox()
        LblDomicilio = New Label()
        TxtDomicilio = New TextBox()
        LblProvincia = New Label()
        TxtProvincia = New TextBox()
        LblEstablecimiento = New Label()
        TxtEstablecimiento = New TextBox()
        LblTimbrado = New Label()
        TxtTimbrado = New TextBox()
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
        chkEncabezados.Location = New Point(653, 281)
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
        lnkCopiar.Location = New Point(553, 282)
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
        GroupBoxDatos.Controls.Add(LblCodigo)
        GroupBoxDatos.Controls.Add(TxtCodigo)
        GroupBoxDatos.Controls.Add(LblDeposito)
        GroupBoxDatos.Controls.Add(TxtDeposito)
        GroupBoxDatos.Controls.Add(LblDescripcion)
        GroupBoxDatos.Controls.Add(TxtDescripcion)
        GroupBoxDatos.Controls.Add(LblDomicilio)
        GroupBoxDatos.Controls.Add(TxtDomicilio)
        GroupBoxDatos.Controls.Add(LblProvincia)
        GroupBoxDatos.Controls.Add(TxtProvincia)
        GroupBoxDatos.Controls.Add(LblEstablecimiento)
        GroupBoxDatos.Controls.Add(TxtEstablecimiento)
        GroupBoxDatos.Controls.Add(LblTimbrado)
        GroupBoxDatos.Controls.Add(TxtTimbrado)
        GroupBoxDatos.Location = New Point(12, 304)
        GroupBoxDatos.Name = "GroupBoxDatos"
        GroupBoxDatos.Size = New Size(760, 123)
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
        TxtCodigo.Location = New Point(75, 25)
        TxtCodigo.Name = "TxtCodigo"
        TxtCodigo.Size = New Size(82, 23)
        TxtCodigo.TabIndex = 1
        ' 
        ' LblDeposito
        ' 
        LblDeposito.AutoSize = True
        LblDeposito.Location = New Point(163, 28)
        LblDeposito.Name = "LblDeposito"
        LblDeposito.Size = New Size(54, 15)
        LblDeposito.TabIndex = 2
        LblDeposito.Text = "Deposito"
        ' 
        ' TxtDeposito
        ' 
        TxtDeposito.Location = New Point(225, 25)
        TxtDeposito.Name = "TxtDeposito"
        TxtDeposito.Size = New Size(90, 23)
        TxtDeposito.TabIndex = 3
        ' 
        ' LblDescripcion
        ' 
        LblDescripcion.AutoSize = True
        LblDescripcion.Location = New Point(321, 28)
        LblDescripcion.Name = "LblDescripcion"
        LblDescripcion.Size = New Size(69, 15)
        LblDescripcion.TabIndex = 4
        LblDescripcion.Text = "Descripcion"
        ' 
        ' TxtDescripcion
        ' 
        TxtDescripcion.Location = New Point(399, 25)
        TxtDescripcion.Name = "TxtDescripcion"
        TxtDescripcion.Size = New Size(250, 23)
        TxtDescripcion.TabIndex = 5
        ' 
        ' LblDomicilio
        ' 
        LblDomicilio.AutoSize = True
        LblDomicilio.Location = New Point(9, 57)
        LblDomicilio.Name = "LblDomicilio"
        LblDomicilio.Size = New Size(58, 15)
        LblDomicilio.TabIndex = 6
        LblDomicilio.Text = "Domicilio"
        ' 
        ' TxtDomicilio
        ' 
        TxtDomicilio.Location = New Point(75, 54)
        TxtDomicilio.Name = "TxtDomicilio"
        TxtDomicilio.Size = New Size(240, 23)
        TxtDomicilio.TabIndex = 7
        ' 
        ' LblProvincia
        ' 
        LblProvincia.AutoSize = True
        LblProvincia.Location = New Point(321, 57)
        LblProvincia.Name = "LblProvincia"
        LblProvincia.Size = New Size(56, 15)
        LblProvincia.TabIndex = 8
        LblProvincia.Text = "Provincia"
        ' 
        ' TxtProvincia
        ' 
        TxtProvincia.Location = New Point(399, 54)
        TxtProvincia.Name = "TxtProvincia"
        TxtProvincia.Size = New Size(115, 23)
        TxtProvincia.TabIndex = 9
        ' 
        ' LblEstablecimiento
        ' 
        LblEstablecimiento.AutoSize = True
        LblEstablecimiento.Location = New Point(9, 86)
        LblEstablecimiento.Name = "LblEstablecimiento"
        LblEstablecimiento.Size = New Size(91, 15)
        LblEstablecimiento.TabIndex = 10
        LblEstablecimiento.Text = "Establecimiento"
        ' 
        ' TxtEstablecimiento
        ' 
        TxtEstablecimiento.Location = New Point(102, 83)
        TxtEstablecimiento.Name = "TxtEstablecimiento"
        TxtEstablecimiento.Size = New Size(115, 23)
        TxtEstablecimiento.TabIndex = 11
        ' 
        ' LblTimbrado
        ' 
        LblTimbrado.AutoSize = True
        LblTimbrado.Location = New Point(321, 86)
        LblTimbrado.Name = "LblTimbrado"
        LblTimbrado.Size = New Size(59, 15)
        LblTimbrado.TabIndex = 12
        LblTimbrado.Text = "Timbrado"
        ' 
        ' TxtTimbrado
        ' 
        TxtTimbrado.Location = New Point(399, 83)
        TxtTimbrado.Name = "TxtTimbrado"
        TxtTimbrado.Size = New Size(115, 23)
        TxtTimbrado.TabIndex = 13
        ' 
        ' CmdAgregar
        ' 
        CmdAgregar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        CmdAgregar.FlatStyle = FlatStyle.Flat
        CmdAgregar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdAgregar.Location = New Point(373, 433)
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
        CmdBorrar.Location = New Point(454, 433)
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
        CmdSalir.Location = New Point(697, 433)
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
        cmdAceptar.Location = New Point(535, 433)
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
        CmdCancelar.Location = New Point(616, 433)
        CmdCancelar.Name = "CmdCancelar"
        CmdCancelar.Size = New Size(75, 28)
        CmdCancelar.TabIndex = 9
        CmdCancelar.Text = "Cancelar"
        CmdCancelar.UseVisualStyleBackColor = True
        ' 
        ' frmSucursales
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(784, 472)
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
        Name = "frmSucursales"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Sucursales"
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
    Friend WithEvents LblDeposito As System.Windows.Forms.Label
    Friend WithEvents TxtDeposito As System.Windows.Forms.TextBox
    Friend WithEvents LblDescripcion As System.Windows.Forms.Label
    Friend WithEvents TxtDescripcion As System.Windows.Forms.TextBox
    Friend WithEvents LblDomicilio As System.Windows.Forms.Label
    Friend WithEvents TxtDomicilio As System.Windows.Forms.TextBox
    Friend WithEvents LblProvincia As System.Windows.Forms.Label
    Friend WithEvents TxtProvincia As System.Windows.Forms.TextBox
    Friend WithEvents LblEstablecimiento As System.Windows.Forms.Label
    Friend WithEvents TxtEstablecimiento As System.Windows.Forms.TextBox
    Friend WithEvents LblTimbrado As System.Windows.Forms.Label
    Friend WithEvents TxtTimbrado As System.Windows.Forms.TextBox
    Friend WithEvents CmdAgregar As System.Windows.Forms.Button
    Friend WithEvents CmdBorrar As System.Windows.Forms.Button
    Friend WithEvents CmdSalir As System.Windows.Forms.Button
    Friend WithEvents cmdAceptar As System.Windows.Forms.Button
    Friend WithEvents CmdCancelar As System.Windows.Forms.Button
End Class

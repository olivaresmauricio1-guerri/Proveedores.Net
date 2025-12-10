<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmResumen
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmResumen))
        TxtBuscar = New TextBox()
        DgvProveedores = New DataGridView()
        GroupBoxResumen = New GroupBox()
        txtSaldoDto = New TextBox()
        txtSaldoActual2 = New TextBox()
        txtUltimoResumen = New TextBox()
        txtCUIT = New TextBox()
        txtProveedor = New TextBox()
        txtNroCuenta = New TextBox()
        LblProveedorTitle = New Label()
        LblCUITTitle = New Label()
        LblCuentaTitle = New Label()
        LblUltResTitle = New Label()
        LblSaldoActualTitle = New Label()
        LblSaldoDtosTitle = New Label()
        LblSaldoSinDocTitle = New Label()
        TxtSaldoSinDoc = New TextBox()
        GroupBoxSaldoActual = New GroupBox()
        LblSaldoActualFrm2 = New Label()
        TxtSaldoActual = New TextBox()
        GroupBoxObservaciones = New GroupBox()
        TxtObsv = New TextBox()
        dgvDeta = New DataGridView()
        CmdSalir = New Button()
        optTodos = New RadioButton()
        optImpago = New RadioButton()
        lnkCopiar = New LinkLabel()
        chkEncabezados = New CheckBox()
        btnImprimir = New Button()
        CType(DgvProveedores, ComponentModel.ISupportInitialize).BeginInit()
        GroupBoxResumen.SuspendLayout()
        GroupBoxSaldoActual.SuspendLayout()
        GroupBoxObservaciones.SuspendLayout()
        CType(dgvDeta, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' TxtBuscar
        ' 
        TxtBuscar.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TxtBuscar.Font = New Font("Segoe UI", 10F)
        TxtBuscar.Location = New Point(10, 10)
        TxtBuscar.Name = "TxtBuscar"
        TxtBuscar.Size = New Size(697, 25)
        TxtBuscar.TabIndex = 0
        ' 
        ' DgvProveedores
        ' 
        DgvProveedores.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        DgvProveedores.Location = New Point(10, 40)
        DgvProveedores.MultiSelect = False
        DgvProveedores.Name = "DgvProveedores"
        DgvProveedores.ReadOnly = True
        DgvProveedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DgvProveedores.Size = New Size(911, 200)
        DgvProveedores.TabIndex = 1
        ' 
        ' GroupBoxResumen
        ' 
        GroupBoxResumen.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBoxResumen.Controls.Add(txtSaldoDto)
        GroupBoxResumen.Controls.Add(txtSaldoActual2)
        GroupBoxResumen.Controls.Add(txtUltimoResumen)
        GroupBoxResumen.Controls.Add(txtCUIT)
        GroupBoxResumen.Controls.Add(txtProveedor)
        GroupBoxResumen.Controls.Add(txtNroCuenta)
        GroupBoxResumen.Controls.Add(LblProveedorTitle)
        GroupBoxResumen.Controls.Add(LblCUITTitle)
        GroupBoxResumen.Controls.Add(LblCuentaTitle)
        GroupBoxResumen.Controls.Add(LblUltResTitle)
        GroupBoxResumen.Controls.Add(LblSaldoActualTitle)
        GroupBoxResumen.Controls.Add(LblSaldoDtosTitle)
        GroupBoxResumen.Controls.Add(LblSaldoSinDocTitle)
        GroupBoxResumen.Controls.Add(TxtSaldoSinDoc)
        GroupBoxResumen.Location = New Point(10, 245)
        GroupBoxResumen.Name = "GroupBoxResumen"
        GroupBoxResumen.Size = New Size(911, 113)
        GroupBoxResumen.TabIndex = 3
        GroupBoxResumen.TabStop = False
        GroupBoxResumen.Text = "Resumen"
        ' 
        ' txtSaldoDto
        ' 
        txtSaldoDto.BackColor = Color.White
        txtSaldoDto.BorderStyle = BorderStyle.FixedSingle
        txtSaldoDto.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        txtSaldoDto.Location = New Point(362, 74)
        txtSaldoDto.Name = "txtSaldoDto"
        txtSaldoDto.ReadOnly = True
        txtSaldoDto.Size = New Size(160, 23)
        txtSaldoDto.TabIndex = 18
        txtSaldoDto.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtSaldoActual2
        ' 
        txtSaldoActual2.BackColor = Color.White
        txtSaldoActual2.BorderStyle = BorderStyle.FixedSingle
        txtSaldoActual2.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        txtSaldoActual2.Location = New Point(362, 45)
        txtSaldoActual2.Name = "txtSaldoActual2"
        txtSaldoActual2.ReadOnly = True
        txtSaldoActual2.Size = New Size(160, 23)
        txtSaldoActual2.TabIndex = 17
        txtSaldoActual2.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtUltimoResumen
        ' 
        txtUltimoResumen.BackColor = Color.White
        txtUltimoResumen.BorderStyle = BorderStyle.FixedSingle
        txtUltimoResumen.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        txtUltimoResumen.Location = New Point(114, 45)
        txtUltimoResumen.Name = "txtUltimoResumen"
        txtUltimoResumen.ReadOnly = True
        txtUltimoResumen.Size = New Size(160, 23)
        txtUltimoResumen.TabIndex = 16
        txtUltimoResumen.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtCUIT
        ' 
        txtCUIT.BackColor = Color.White
        txtCUIT.BorderStyle = BorderStyle.FixedSingle
        txtCUIT.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        txtCUIT.ForeColor = Color.FromArgb(CByte(0), CByte(0), CByte(192))
        txtCUIT.Location = New Point(770, 16)
        txtCUIT.Name = "txtCUIT"
        txtCUIT.ReadOnly = True
        txtCUIT.Size = New Size(135, 23)
        txtCUIT.TabIndex = 15
        txtCUIT.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtProveedor
        ' 
        txtProveedor.BackColor = Color.White
        txtProveedor.BorderStyle = BorderStyle.FixedSingle
        txtProveedor.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtProveedor.ForeColor = Color.FromArgb(CByte(0), CByte(0), CByte(192))
        txtProveedor.Location = New Point(362, 16)
        txtProveedor.Name = "txtProveedor"
        txtProveedor.ReadOnly = True
        txtProveedor.Size = New Size(335, 23)
        txtProveedor.TabIndex = 14
        ' 
        ' txtNroCuenta
        ' 
        txtNroCuenta.BackColor = Color.White
        txtNroCuenta.BorderStyle = BorderStyle.FixedSingle
        txtNroCuenta.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        txtNroCuenta.ForeColor = Color.FromArgb(CByte(0), CByte(0), CByte(192))
        txtNroCuenta.Location = New Point(114, 16)
        txtNroCuenta.Name = "txtNroCuenta"
        txtNroCuenta.ReadOnly = True
        txtNroCuenta.Size = New Size(96, 23)
        txtNroCuenta.TabIndex = 13
        txtNroCuenta.TextAlign = HorizontalAlignment.Center
        ' 
        ' LblProveedorTitle
        ' 
        LblProveedorTitle.AutoSize = True
        LblProveedorTitle.Location = New Point(280, 20)
        LblProveedorTitle.Name = "LblProveedorTitle"
        LblProveedorTitle.Size = New Size(64, 15)
        LblProveedorTitle.TabIndex = 0
        LblProveedorTitle.Text = "Proveedor:"
        ' 
        ' LblCUITTitle
        ' 
        LblCUITTitle.AutoSize = True
        LblCUITTitle.Location = New Point(728, 20)
        LblCUITTitle.Name = "LblCUITTitle"
        LblCUITTitle.Size = New Size(36, 15)
        LblCUITTitle.TabIndex = 2
        LblCUITTitle.Text = "CUIT:"
        ' 
        ' LblCuentaTitle
        ' 
        LblCuentaTitle.AutoSize = True
        LblCuentaTitle.Location = New Point(10, 20)
        LblCuentaTitle.Name = "LblCuentaTitle"
        LblCuentaTitle.Size = New Size(71, 15)
        LblCuentaTitle.TabIndex = 4
        LblCuentaTitle.Text = "Nro.Cuenta:"
        ' 
        ' LblUltResTitle
        ' 
        LblUltResTitle.AutoSize = True
        LblUltResTitle.Location = New Point(10, 51)
        LblUltResTitle.Name = "LblUltResTitle"
        LblUltResTitle.Size = New Size(98, 15)
        LblUltResTitle.TabIndex = 6
        LblUltResTitle.Text = "Último Resumen:"
        ' 
        ' LblSaldoActualTitle
        ' 
        LblSaldoActualTitle.AutoSize = True
        LblSaldoActualTitle.Location = New Point(280, 48)
        LblSaldoActualTitle.Name = "LblSaldoActualTitle"
        LblSaldoActualTitle.Size = New Size(76, 15)
        LblSaldoActualTitle.TabIndex = 8
        LblSaldoActualTitle.Text = "Saldo Actual:"
        ' 
        ' LblSaldoDtosTitle
        ' 
        LblSaldoDtosTitle.AutoSize = True
        LblSaldoDtosTitle.Location = New Point(280, 76)
        LblSaldoDtosTitle.Name = "LblSaldoDtosTitle"
        LblSaldoDtosTitle.Size = New Size(69, 15)
        LblSaldoDtosTitle.TabIndex = 10
        LblSaldoDtosTitle.Text = "Saldo Dtos.:"
        ' 
        ' LblSaldoSinDocTitle
        ' 
        LblSaldoSinDocTitle.AutoSize = True
        LblSaldoSinDocTitle.Location = New Point(10, 78)
        LblSaldoSinDocTitle.Name = "LblSaldoSinDocTitle"
        LblSaldoSinDocTitle.Size = New Size(83, 15)
        LblSaldoSinDocTitle.TabIndex = 12
        LblSaldoSinDocTitle.Text = "Saldo sin doc.:"
        ' 
        ' TxtSaldoSinDoc
        ' 
        TxtSaldoSinDoc.BackColor = Color.White
        TxtSaldoSinDoc.BorderStyle = BorderStyle.FixedSingle
        TxtSaldoSinDoc.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        TxtSaldoSinDoc.Location = New Point(114, 74)
        TxtSaldoSinDoc.Name = "TxtSaldoSinDoc"
        TxtSaldoSinDoc.ReadOnly = True
        TxtSaldoSinDoc.Size = New Size(160, 23)
        TxtSaldoSinDoc.TabIndex = 4
        TxtSaldoSinDoc.TextAlign = HorizontalAlignment.Center
        ' 
        ' GroupBoxSaldoActual
        ' 
        GroupBoxSaldoActual.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBoxSaldoActual.Controls.Add(LblSaldoActualFrm2)
        GroupBoxSaldoActual.Controls.Add(TxtSaldoActual)
        GroupBoxSaldoActual.Location = New Point(372, 649)
        GroupBoxSaldoActual.Name = "GroupBoxSaldoActual"
        GroupBoxSaldoActual.Size = New Size(285, 51)
        GroupBoxSaldoActual.TabIndex = 4
        GroupBoxSaldoActual.TabStop = False
        GroupBoxSaldoActual.Text = "Saldo"
        ' 
        ' LblSaldoActualFrm2
        ' 
        LblSaldoActualFrm2.AutoSize = True
        LblSaldoActualFrm2.Location = New Point(13, 26)
        LblSaldoActualFrm2.Name = "LblSaldoActualFrm2"
        LblSaldoActualFrm2.Size = New Size(76, 15)
        LblSaldoActualFrm2.TabIndex = 0
        LblSaldoActualFrm2.Text = "Saldo Actual:"
        ' 
        ' TxtSaldoActual
        ' 
        TxtSaldoActual.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        TxtSaldoActual.ForeColor = Color.Red
        TxtSaldoActual.Location = New Point(103, 22)
        TxtSaldoActual.Name = "TxtSaldoActual"
        TxtSaldoActual.ReadOnly = True
        TxtSaldoActual.Size = New Size(170, 25)
        TxtSaldoActual.TabIndex = 5
        TxtSaldoActual.TextAlign = HorizontalAlignment.Right
        ' 
        ' GroupBoxObservaciones
        ' 
        GroupBoxObservaciones.Controls.Add(TxtObsv)
        GroupBoxObservaciones.Location = New Point(10, 649)
        GroupBoxObservaciones.Name = "GroupBoxObservaciones"
        GroupBoxObservaciones.Size = New Size(356, 51)
        GroupBoxObservaciones.TabIndex = 5
        GroupBoxObservaciones.TabStop = False
        GroupBoxObservaciones.Text = "Observaciones"
        ' 
        ' TxtObsv
        ' 
        TxtObsv.ForeColor = Color.Blue
        TxtObsv.Location = New Point(6, 22)
        TxtObsv.Name = "TxtObsv"
        TxtObsv.ReadOnly = True
        TxtObsv.Size = New Size(338, 23)
        TxtObsv.TabIndex = 6
        ' 
        ' dgvDeta
        ' 
        dgvDeta.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvDeta.Location = New Point(8, 364)
        dgvDeta.Name = "dgvDeta"
        dgvDeta.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvDeta.Size = New Size(911, 254)
        dgvDeta.TabIndex = 2
        ' 
        ' CmdSalir
        ' 
        CmdSalir.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        CmdSalir.BackColor = Color.IndianRed
        CmdSalir.FlatStyle = FlatStyle.Flat
        CmdSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdSalir.ForeColor = Color.White
        CmdSalir.Location = New Point(834, 658)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(85, 33)
        CmdSalir.TabIndex = 11
        CmdSalir.Text = "Salir"
        CmdSalir.UseVisualStyleBackColor = False
        ' 
        ' optTodos
        ' 
        optTodos.AutoSize = True
        optTodos.Checked = True
        optTodos.Location = New Point(734, 12)
        optTodos.Name = "optTodos"
        optTodos.Size = New Size(57, 19)
        optTodos.TabIndex = 12
        optTodos.TabStop = True
        optTodos.Text = "Todos"
        optTodos.UseVisualStyleBackColor = True
        ' 
        ' optImpago
        ' 
        optImpago.AutoSize = True
        optImpago.Location = New Point(797, 12)
        optImpago.Name = "optImpago"
        optImpago.Size = New Size(66, 19)
        optImpago.TabIndex = 13
        optImpago.Text = "Impago"
        optImpago.UseVisualStyleBackColor = True
        ' 
        ' lnkCopiar
        ' 
        lnkCopiar.AutoSize = True
        lnkCopiar.LinkColor = Color.Black
        lnkCopiar.Location = New Point(700, 624)
        lnkCopiar.Name = "lnkCopiar"
        lnkCopiar.Size = New Size(94, 15)
        lnkCopiar.TabIndex = 15
        lnkCopiar.TabStop = True
        lnkCopiar.Text = "Copiar selección"
        lnkCopiar.VisitedLinkColor = Color.Black
        ' 
        ' chkEncabezados
        ' 
        chkEncabezados.AutoSize = True
        chkEncabezados.Location = New Point(800, 624)
        chkEncabezados.Name = "chkEncabezados"
        chkEncabezados.Size = New Size(119, 19)
        chkEncabezados.TabIndex = 14
        chkEncabezados.Text = "Con encabezados"
        chkEncabezados.UseVisualStyleBackColor = True
        ' 
        ' btnImprimir
        ' 
        btnImprimir.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnImprimir.BackColor = SystemColors.Control
        btnImprimir.FlatStyle = FlatStyle.Flat
        btnImprimir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnImprimir.ForeColor = Color.Black
        btnImprimir.Location = New Point(738, 658)
        btnImprimir.Name = "btnImprimir"
        btnImprimir.Size = New Size(85, 33)
        btnImprimir.TabIndex = 16
        btnImprimir.Text = "Imprimir"
        btnImprimir.UseVisualStyleBackColor = False
        ' 
        ' frmResumen
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(931, 708)
        Controls.Add(btnImprimir)
        Controls.Add(lnkCopiar)
        Controls.Add(chkEncabezados)
        Controls.Add(optImpago)
        Controls.Add(optTodos)
        Controls.Add(CmdSalir)
        Controls.Add(GroupBoxResumen)
        Controls.Add(dgvDeta)
        Controls.Add(GroupBoxObservaciones)
        Controls.Add(GroupBoxSaldoActual)
        Controls.Add(DgvProveedores)
        Controls.Add(TxtBuscar)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmResumen"
        Text = "Resumen de Proveedores"
        CType(DgvProveedores, ComponentModel.ISupportInitialize).EndInit()
        GroupBoxResumen.ResumeLayout(False)
        GroupBoxResumen.PerformLayout()
        GroupBoxSaldoActual.ResumeLayout(False)
        GroupBoxSaldoActual.PerformLayout()
        GroupBoxObservaciones.ResumeLayout(False)
        GroupBoxObservaciones.PerformLayout()
        CType(dgvDeta, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents TxtBuscar As System.Windows.Forms.TextBox
    Friend WithEvents DgvProveedores As System.Windows.Forms.DataGridView
    Friend WithEvents dgvDeta As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBoxResumen As System.Windows.Forms.GroupBox
    Friend WithEvents LblProveedorTitle As System.Windows.Forms.Label
    Friend WithEvents LblCUITTitle As System.Windows.Forms.Label
    Friend WithEvents LblCuentaTitle As System.Windows.Forms.Label
    Friend WithEvents LblUltResTitle As System.Windows.Forms.Label
    Friend WithEvents LblSaldoActualTitle As System.Windows.Forms.Label
    Friend WithEvents LblSaldoDtosTitle As System.Windows.Forms.Label
    Friend WithEvents LblSaldoSinDocTitle As System.Windows.Forms.Label
    Friend WithEvents TxtSaldoSinDoc As System.Windows.Forms.TextBox
    Friend WithEvents GroupBoxSaldoActual As System.Windows.Forms.GroupBox
    Friend WithEvents LblSaldoActualFrm2 As System.Windows.Forms.Label
    Friend WithEvents TxtSaldoActual As System.Windows.Forms.TextBox
    Friend WithEvents GroupBoxObservaciones As System.Windows.Forms.GroupBox
    Friend WithEvents TxtObsv As System.Windows.Forms.TextBox
    Friend WithEvents txtNroCuenta As TextBox
    Friend WithEvents txtSaldoActual2 As TextBox
    Friend WithEvents txtUltimoResumen As TextBox
    Friend WithEvents txtCUIT As TextBox
    Friend WithEvents txtProveedor As TextBox
    Friend WithEvents txtSaldoDto As TextBox
    Friend WithEvents CmdSalir As Button
    Friend WithEvents optTodos As RadioButton
    Friend WithEvents optImpago As RadioButton
    Friend WithEvents lnkCopiar As LinkLabel
    Friend WithEvents chkEncabezados As CheckBox
    Friend WithEvents btnImprimir As Button
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmResumenHistorico
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
        DgvProveedores = New DataGridView()
        GroupBoxRango = New GroupBox()
        txtSaldoAnterior = New TextBox()
        LblSaldoAnterior = New Label()
        TxtNroCuenta = New TextBox()
        Label1 = New Label()
        txtCUIT = New TextBox()
        LblCUIT = New Label()
        TxtBuscar = New TextBox()
        dtHasta = New DateTimePicker()
        dtDesde = New DateTimePicker()
        LblHasta = New Label()
        LblDesde = New Label()
        DgvDeta = New DataGridView()
        CmdSalir = New Button()
        CmdImprimir = New Button()
        GroupBoxTotales = New GroupBox()
        txtSaldoMaestro = New TextBox()
        LblSaldoMaestro = New Label()
        txtDebe = New TextBox()
        LblDebe = New Label()
        txtHaber = New TextBox()
        LblHaber = New Label()
        lnkCopiar = New LinkLabel()
        chkEncabezados = New CheckBox()
        CType(DgvProveedores, ComponentModel.ISupportInitialize).BeginInit()
        GroupBoxRango.SuspendLayout()
        CType(DgvDeta, ComponentModel.ISupportInitialize).BeginInit()
        GroupBoxTotales.SuspendLayout()
        SuspendLayout()
        ' 
        ' DgvProveedores
        ' 
        DgvProveedores.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        DgvProveedores.Location = New Point(8, 90)
        DgvProveedores.MultiSelect = False
        DgvProveedores.Name = "DgvProveedores"
        DgvProveedores.ReadOnly = True
        DgvProveedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DgvProveedores.Size = New Size(1141, 131)
        DgvProveedores.TabIndex = 1
        ' 
        ' GroupBoxRango
        ' 
        GroupBoxRango.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBoxRango.Controls.Add(txtSaldoAnterior)
        GroupBoxRango.Controls.Add(LblSaldoAnterior)
        GroupBoxRango.Controls.Add(TxtNroCuenta)
        GroupBoxRango.Controls.Add(Label1)
        GroupBoxRango.Controls.Add(txtCUIT)
        GroupBoxRango.Controls.Add(LblCUIT)
        GroupBoxRango.Controls.Add(TxtBuscar)
        GroupBoxRango.Controls.Add(dtHasta)
        GroupBoxRango.Controls.Add(dtDesde)
        GroupBoxRango.Controls.Add(LblHasta)
        GroupBoxRango.Controls.Add(LblDesde)
        GroupBoxRango.Location = New Point(10, 12)
        GroupBoxRango.Name = "GroupBoxRango"
        GroupBoxRango.Size = New Size(1141, 72)
        GroupBoxRango.TabIndex = 2
        GroupBoxRango.TabStop = False
        GroupBoxRango.Text = "Buscar"
        ' 
        ' txtSaldoAnterior
        ' 
        txtSaldoAnterior.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        txtSaldoAnterior.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtSaldoAnterior.ForeColor = Color.FromArgb(CByte(0), CByte(0), CByte(192))
        txtSaldoAnterior.Location = New Point(1006, 15)
        txtSaldoAnterior.Name = "txtSaldoAnterior"
        txtSaldoAnterior.ReadOnly = True
        txtSaldoAnterior.Size = New Size(130, 23)
        txtSaldoAnterior.TabIndex = 12
        txtSaldoAnterior.TextAlign = HorizontalAlignment.Center
        ' 
        ' LblSaldoAnterior
        ' 
        LblSaldoAnterior.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        LblSaldoAnterior.AutoSize = True
        LblSaldoAnterior.Location = New Point(918, 20)
        LblSaldoAnterior.Name = "LblSaldoAnterior"
        LblSaldoAnterior.Size = New Size(82, 15)
        LblSaldoAnterior.TabIndex = 11
        LblSaldoAnterior.Text = "Saldo Anterior"
        ' 
        ' TxtNroCuenta
        ' 
        TxtNroCuenta.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        TxtNroCuenta.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        TxtNroCuenta.ForeColor = Color.Navy
        TxtNroCuenta.Location = New Point(569, 14)
        TxtNroCuenta.Name = "TxtNroCuenta"
        TxtNroCuenta.ReadOnly = True
        TxtNroCuenta.Size = New Size(127, 23)
        TxtNroCuenta.TabIndex = 10
        TxtNroCuenta.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label1
        ' 
        Label1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Label1.AutoSize = True
        Label1.Location = New Point(498, 18)
        Label1.Name = "Label1"
        Label1.Size = New Size(65, 15)
        Label1.TabIndex = 9
        Label1.Text = "NroCuenta"
        ' 
        ' txtCUIT
        ' 
        txtCUIT.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        txtCUIT.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtCUIT.ForeColor = Color.Navy
        txtCUIT.Location = New Point(569, 43)
        txtCUIT.Name = "txtCUIT"
        txtCUIT.ReadOnly = True
        txtCUIT.Size = New Size(127, 23)
        txtCUIT.TabIndex = 8
        txtCUIT.TextAlign = HorizontalAlignment.Center
        ' 
        ' LblCUIT
        ' 
        LblCUIT.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        LblCUIT.AutoSize = True
        LblCUIT.Location = New Point(498, 47)
        LblCUIT.Name = "LblCUIT"
        LblCUIT.Size = New Size(33, 15)
        LblCUIT.TabIndex = 7
        LblCUIT.Text = "CUIT"
        ' 
        ' TxtBuscar
        ' 
        TxtBuscar.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TxtBuscar.Font = New Font("Segoe UI", 10F)
        TxtBuscar.Location = New Point(5, 22)
        TxtBuscar.Name = "TxtBuscar"
        TxtBuscar.Size = New Size(475, 25)
        TxtBuscar.TabIndex = 6
        ' 
        ' dtHasta
        ' 
        dtHasta.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        dtHasta.Format = DateTimePickerFormat.Short
        dtHasta.Location = New Point(760, 43)
        dtHasta.Name = "dtHasta"
        dtHasta.Size = New Size(120, 23)
        dtHasta.TabIndex = 5
        ' 
        ' dtDesde
        ' 
        dtDesde.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        dtDesde.Format = DateTimePickerFormat.Short
        dtDesde.Location = New Point(760, 12)
        dtDesde.Name = "dtDesde"
        dtDesde.Size = New Size(120, 23)
        dtDesde.TabIndex = 3
        ' 
        ' LblHasta
        ' 
        LblHasta.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        LblHasta.AutoSize = True
        LblHasta.Location = New Point(705, 47)
        LblHasta.Name = "LblHasta"
        LblHasta.Size = New Size(40, 15)
        LblHasta.TabIndex = 4
        LblHasta.Text = "Hasta:"
        ' 
        ' LblDesde
        ' 
        LblDesde.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        LblDesde.AutoSize = True
        LblDesde.Location = New Point(705, 16)
        LblDesde.Name = "LblDesde"
        LblDesde.Size = New Size(42, 15)
        LblDesde.TabIndex = 2
        LblDesde.Text = "Desde:"
        ' 
        ' DgvDeta
        ' 
        DgvDeta.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DgvDeta.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DgvDeta.EditMode = DataGridViewEditMode.EditOnF2
        DgvDeta.Location = New Point(8, 241)
        DgvDeta.Name = "DgvDeta"
        DgvDeta.Size = New Size(1141, 281)
        DgvDeta.TabIndex = 6
        ' 
        ' CmdSalir
        ' 
        CmdSalir.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        CmdSalir.BackColor = Color.IndianRed
        CmdSalir.FlatStyle = FlatStyle.Flat
        CmdSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdSalir.ForeColor = Color.White
        CmdSalir.Location = New Point(1064, 609)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(85, 33)
        CmdSalir.TabIndex = 11
        CmdSalir.Text = "Salir"
        CmdSalir.UseVisualStyleBackColor = False
        ' 
        ' CmdImprimir
        ' 
        CmdImprimir.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        CmdImprimir.BackColor = SystemColors.Control
        CmdImprimir.FlatStyle = FlatStyle.Flat
        CmdImprimir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdImprimir.ForeColor = Color.Black
        CmdImprimir.Location = New Point(973, 609)
        CmdImprimir.Name = "CmdImprimir"
        CmdImprimir.Size = New Size(85, 33)
        CmdImprimir.TabIndex = 8
        CmdImprimir.Text = "Imprimir"
        CmdImprimir.UseVisualStyleBackColor = False
        ' 
        ' GroupBoxTotales
        ' 
        GroupBoxTotales.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBoxTotales.Controls.Add(txtSaldoMaestro)
        GroupBoxTotales.Controls.Add(LblSaldoMaestro)
        GroupBoxTotales.Controls.Add(txtDebe)
        GroupBoxTotales.Controls.Add(LblDebe)
        GroupBoxTotales.Controls.Add(txtHaber)
        GroupBoxTotales.Controls.Add(LblHaber)
        GroupBoxTotales.Location = New Point(8, 543)
        GroupBoxTotales.Name = "GroupBoxTotales"
        GroupBoxTotales.Size = New Size(1137, 60)
        GroupBoxTotales.TabIndex = 12
        GroupBoxTotales.TabStop = False
        GroupBoxTotales.Text = "Totales"
        ' 
        ' txtSaldoMaestro
        ' 
        txtSaldoMaestro.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        txtSaldoMaestro.ForeColor = Color.Navy
        txtSaldoMaestro.Location = New Point(885, 23)
        txtSaldoMaestro.Name = "txtSaldoMaestro"
        txtSaldoMaestro.ReadOnly = True
        txtSaldoMaestro.Size = New Size(130, 23)
        txtSaldoMaestro.TabIndex = 0
        txtSaldoMaestro.TextAlign = HorizontalAlignment.Center
        ' 
        ' LblSaldoMaestro
        ' 
        LblSaldoMaestro.AutoSize = True
        LblSaldoMaestro.Location = New Point(791, 27)
        LblSaldoMaestro.Name = "LblSaldoMaestro"
        LblSaldoMaestro.Size = New Size(82, 15)
        LblSaldoMaestro.TabIndex = 0
        LblSaldoMaestro.Text = "Saldo Maestro"
        ' 
        ' txtDebe
        ' 
        txtDebe.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        txtDebe.ForeColor = Color.Navy
        txtDebe.Location = New Point(406, 21)
        txtDebe.Name = "txtDebe"
        txtDebe.ReadOnly = True
        txtDebe.Size = New Size(130, 23)
        txtDebe.TabIndex = 2
        txtDebe.TextAlign = HorizontalAlignment.Center
        ' 
        ' LblDebe
        ' 
        LblDebe.AutoSize = True
        LblDebe.Location = New Point(364, 25)
        LblDebe.Name = "LblDebe"
        LblDebe.Size = New Size(34, 15)
        LblDebe.TabIndex = 0
        LblDebe.Text = "Debe"
        ' 
        ' txtHaber
        ' 
        txtHaber.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        txtHaber.ForeColor = Color.Navy
        txtHaber.Location = New Point(623, 22)
        txtHaber.Name = "txtHaber"
        txtHaber.ReadOnly = True
        txtHaber.Size = New Size(130, 23)
        txtHaber.TabIndex = 3
        txtHaber.TextAlign = HorizontalAlignment.Center
        ' 
        ' LblHaber
        ' 
        LblHaber.AutoSize = True
        LblHaber.Location = New Point(579, 26)
        LblHaber.Name = "LblHaber"
        LblHaber.Size = New Size(39, 15)
        LblHaber.TabIndex = 0
        LblHaber.Text = "Haber"
        ' 
        ' lnkCopiar
        ' 
        lnkCopiar.AutoSize = True
        lnkCopiar.LinkColor = Color.Black
        lnkCopiar.Location = New Point(932, 529)
        lnkCopiar.Name = "lnkCopiar"
        lnkCopiar.Size = New Size(94, 15)
        lnkCopiar.TabIndex = 14
        lnkCopiar.TabStop = True
        lnkCopiar.Text = "Copiar selección"
        lnkCopiar.VisitedLinkColor = Color.Black
        ' 
        ' chkEncabezados
        ' 
        chkEncabezados.AutoSize = True
        chkEncabezados.Location = New Point(1032, 528)
        chkEncabezados.Name = "chkEncabezados"
        chkEncabezados.Size = New Size(119, 19)
        chkEncabezados.TabIndex = 13
        chkEncabezados.Text = "Con encabezados"
        chkEncabezados.UseVisualStyleBackColor = True
        ' 
        ' frmResumenHistorico
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1161, 651)
        Controls.Add(lnkCopiar)
        Controls.Add(chkEncabezados)
        Controls.Add(GroupBoxTotales)
        Controls.Add(CmdImprimir)
        Controls.Add(CmdSalir)
        Controls.Add(DgvDeta)
        Controls.Add(GroupBoxRango)
        Controls.Add(DgvProveedores)
        MinimumSize = New Size(1177, 690)
        Name = "frmResumenHistorico"
        Text = "Resumen de cuentas Histórico"
        CType(DgvProveedores, ComponentModel.ISupportInitialize).EndInit()
        GroupBoxRango.ResumeLayout(False)
        GroupBoxRango.PerformLayout()
        CType(DgvDeta, ComponentModel.ISupportInitialize).EndInit()
        GroupBoxTotales.ResumeLayout(False)
        GroupBoxTotales.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents DgvProveedores As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBoxRango As System.Windows.Forms.GroupBox
    Friend WithEvents dtHasta As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtDesde As System.Windows.Forms.DateTimePicker
    Friend WithEvents LblHasta As System.Windows.Forms.Label
    Friend WithEvents LblDesde As System.Windows.Forms.Label
    Friend WithEvents DgvDeta As System.Windows.Forms.DataGridView
    Friend WithEvents CmdSalir As System.Windows.Forms.Button
    Friend WithEvents CmdImprimir As System.Windows.Forms.Button
    Friend WithEvents GroupBoxTotales As System.Windows.Forms.GroupBox
    Friend WithEvents txtSaldoMaestro As System.Windows.Forms.TextBox
    Friend WithEvents LblSaldoMaestro As System.Windows.Forms.Label
    Friend WithEvents txtDebe As System.Windows.Forms.TextBox
    Friend WithEvents LblDebe As System.Windows.Forms.Label
    Friend WithEvents txtHaber As System.Windows.Forms.TextBox
    Friend WithEvents LblHaber As System.Windows.Forms.Label
    Friend WithEvents TxtNroCuenta As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtCUIT As TextBox
    Friend WithEvents LblCUIT As Label
    Friend WithEvents TxtBuscar As TextBox
    Friend WithEvents txtSaldoAnterior As TextBox
    Friend WithEvents LblSaldoAnterior As Label
    Friend WithEvents lnkCopiar As LinkLabel
    Friend WithEvents chkEncabezados As CheckBox
End Class

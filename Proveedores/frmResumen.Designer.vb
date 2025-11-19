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
        TxtBuscar = New TextBox()
        DgvProveedores = New DataGridView()
        GroupBoxResumen = New GroupBox()
        LblProveedorTitle = New Label()
        LblProveedorValue = New Label()
        LblCUITTitle = New Label()
        LblCUITValue = New Label()
        LblCuentaTitle = New Label()
        LblCuentaValue = New Label()
        LblUltResTitle = New Label()
        LblUltResValue = New Label()
        LblSaldoActualTitle = New Label()
        LblSaldoActualValue = New Label()
        LblSaldoDtosTitle = New Label()
        LblSaldoDtosValue = New Label()
        LblSaldoSinDocTitle = New Label()
        TxtSaldoSinDoc = New TextBox()
        GroupBoxSaldoActual = New GroupBox()
        LblSaldoActualFrm2 = New Label()
        TxtSaldoActual = New TextBox()
        GroupBoxObservaciones = New GroupBox()
        TxtObsv = New TextBox()
        DBGdeta = New DataGridView()
        CType(DgvProveedores, ComponentModel.ISupportInitialize).BeginInit()
        GroupBoxResumen.SuspendLayout()
        GroupBoxSaldoActual.SuspendLayout()
        GroupBoxObservaciones.SuspendLayout()
        CType(DBGdeta, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' TxtBuscar
        ' 
        TxtBuscar.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TxtBuscar.Font = New Font("Segoe UI", 10F)
        TxtBuscar.Location = New Point(10, 10)
        TxtBuscar.Name = "TxtBuscar"
        TxtBuscar.Size = New Size(764, 25)
        TxtBuscar.TabIndex = 0
        ' 
        ' DgvProveedores
        ' 
        DgvProveedores.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        DgvProveedores.Location = New Point(10, 40)
        DgvProveedores.MultiSelect = False
        DgvProveedores.Name = "DgvProveedores"
        DgvProveedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DgvProveedores.Size = New Size(764, 200)
        DgvProveedores.TabIndex = 1
        ' 
        ' GroupBoxResumen
        ' 
        GroupBoxResumen.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBoxResumen.Controls.Add(LblProveedorTitle)
        GroupBoxResumen.Controls.Add(LblProveedorValue)
        GroupBoxResumen.Controls.Add(LblCUITTitle)
        GroupBoxResumen.Controls.Add(LblCUITValue)
        GroupBoxResumen.Controls.Add(LblCuentaTitle)
        GroupBoxResumen.Controls.Add(LblCuentaValue)
        GroupBoxResumen.Controls.Add(LblUltResTitle)
        GroupBoxResumen.Controls.Add(LblUltResValue)
        GroupBoxResumen.Controls.Add(LblSaldoActualTitle)
        GroupBoxResumen.Controls.Add(LblSaldoActualValue)
        GroupBoxResumen.Controls.Add(LblSaldoDtosTitle)
        GroupBoxResumen.Controls.Add(LblSaldoDtosValue)
        GroupBoxResumen.Controls.Add(LblSaldoSinDocTitle)
        GroupBoxResumen.Controls.Add(TxtSaldoSinDoc)
        GroupBoxResumen.Location = New Point(10, 245)
        GroupBoxResumen.Name = "GroupBoxResumen"
        GroupBoxResumen.Size = New Size(764, 100)
        GroupBoxResumen.TabIndex = 3
        GroupBoxResumen.TabStop = False
        GroupBoxResumen.Text = "Resumen"
        ' 
        ' LblProveedorTitle
        ' 
        LblProveedorTitle.AutoSize = True
        LblProveedorTitle.Location = New Point(10, 20)
        LblProveedorTitle.Name = "LblProveedorTitle"
        LblProveedorTitle.Size = New Size(64, 15)
        LblProveedorTitle.TabIndex = 0
        LblProveedorTitle.Text = "Proveedor:"
        ' 
        ' LblProveedorValue
        ' 
        LblProveedorValue.AutoEllipsis = True
        LblProveedorValue.Location = New Point(90, 20)
        LblProveedorValue.Name = "LblProveedorValue"
        LblProveedorValue.Size = New Size(300, 15)
        LblProveedorValue.TabIndex = 1
        ' 
        ' LblCUITTitle
        ' 
        LblCUITTitle.AutoSize = True
        LblCUITTitle.Location = New Point(10, 40)
        LblCUITTitle.Name = "LblCUITTitle"
        LblCUITTitle.Size = New Size(36, 15)
        LblCUITTitle.TabIndex = 2
        LblCUITTitle.Text = "CUIT:"
        ' 
        ' LblCUITValue
        ' 
        LblCUITValue.Location = New Point(90, 40)
        LblCUITValue.Name = "LblCUITValue"
        LblCUITValue.Size = New Size(150, 15)
        LblCUITValue.TabIndex = 3
        ' 
        ' LblCuentaTitle
        ' 
        LblCuentaTitle.AutoSize = True
        LblCuentaTitle.Location = New Point(260, 40)
        LblCuentaTitle.Name = "LblCuentaTitle"
        LblCuentaTitle.Size = New Size(71, 15)
        LblCuentaTitle.TabIndex = 4
        LblCuentaTitle.Text = "Nro.Cuenta:"
        ' 
        ' LblCuentaValue
        ' 
        LblCuentaValue.Location = New Point(340, 40)
        LblCuentaValue.Name = "LblCuentaValue"
        LblCuentaValue.Size = New Size(120, 15)
        LblCuentaValue.TabIndex = 5
        ' 
        ' LblUltResTitle
        ' 
        LblUltResTitle.AutoSize = True
        LblUltResTitle.Location = New Point(480, 20)
        LblUltResTitle.Name = "LblUltResTitle"
        LblUltResTitle.Size = New Size(98, 15)
        LblUltResTitle.TabIndex = 6
        LblUltResTitle.Text = "Último Resumen:"
        ' 
        ' LblUltResValue
        ' 
        LblUltResValue.Location = New Point(590, 20)
        LblUltResValue.Name = "LblUltResValue"
        LblUltResValue.Size = New Size(160, 15)
        LblUltResValue.TabIndex = 7
        ' 
        ' LblSaldoActualTitle
        ' 
        LblSaldoActualTitle.AutoSize = True
        LblSaldoActualTitle.Location = New Point(10, 65)
        LblSaldoActualTitle.Name = "LblSaldoActualTitle"
        LblSaldoActualTitle.Size = New Size(76, 15)
        LblSaldoActualTitle.TabIndex = 8
        LblSaldoActualTitle.Text = "Saldo Actual:"
        ' 
        ' LblSaldoActualValue
        ' 
        LblSaldoActualValue.Location = New Point(90, 65)
        LblSaldoActualValue.Name = "LblSaldoActualValue"
        LblSaldoActualValue.Size = New Size(120, 15)
        LblSaldoActualValue.TabIndex = 9
        ' 
        ' LblSaldoDtosTitle
        ' 
        LblSaldoDtosTitle.AutoSize = True
        LblSaldoDtosTitle.Location = New Point(230, 65)
        LblSaldoDtosTitle.Name = "LblSaldoDtosTitle"
        LblSaldoDtosTitle.Size = New Size(69, 15)
        LblSaldoDtosTitle.TabIndex = 10
        LblSaldoDtosTitle.Text = "Saldo Dtos.:"
        ' 
        ' LblSaldoDtosValue
        ' 
        LblSaldoDtosValue.Location = New Point(310, 65)
        LblSaldoDtosValue.Name = "LblSaldoDtosValue"
        LblSaldoDtosValue.Size = New Size(120, 15)
        LblSaldoDtosValue.TabIndex = 11
        ' 
        ' LblSaldoSinDocTitle
        ' 
        LblSaldoSinDocTitle.AutoSize = True
        LblSaldoSinDocTitle.Location = New Point(480, 65)
        LblSaldoSinDocTitle.Name = "LblSaldoSinDocTitle"
        LblSaldoSinDocTitle.Size = New Size(83, 15)
        LblSaldoSinDocTitle.TabIndex = 12
        LblSaldoSinDocTitle.Text = "Saldo sin doc.:"
        ' 
        ' TxtSaldoSinDoc
        ' 
        TxtSaldoSinDoc.Location = New Point(575, 62)
        TxtSaldoSinDoc.Name = "TxtSaldoSinDoc"
        TxtSaldoSinDoc.ReadOnly = True
        TxtSaldoSinDoc.Size = New Size(175, 23)
        TxtSaldoSinDoc.TabIndex = 4
        TxtSaldoSinDoc.TextAlign = HorizontalAlignment.Right
        ' 
        ' GroupBoxSaldoActual
        ' 
        GroupBoxSaldoActual.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBoxSaldoActual.Controls.Add(LblSaldoActualFrm2)
        GroupBoxSaldoActual.Controls.Add(TxtSaldoActual)
        GroupBoxSaldoActual.Location = New Point(338, 649)
        GroupBoxSaldoActual.Name = "GroupBoxSaldoActual"
        GroupBoxSaldoActual.Size = New Size(279, 51)
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
        TxtSaldoActual.ForeColor = Color.Red
        TxtSaldoActual.Location = New Point(103, 22)
        TxtSaldoActual.Name = "TxtSaldoActual"
        TxtSaldoActual.ReadOnly = True
        TxtSaldoActual.Size = New Size(170, 23)
        TxtSaldoActual.TabIndex = 5
        TxtSaldoActual.TextAlign = HorizontalAlignment.Right
        ' 
        ' GroupBoxObservaciones
        ' 
        GroupBoxObservaciones.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBoxObservaciones.Controls.Add(TxtObsv)
        GroupBoxObservaciones.Location = New Point(10, 649)
        GroupBoxObservaciones.Name = "GroupBoxObservaciones"
        GroupBoxObservaciones.Size = New Size(322, 51)
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
        TxtObsv.Size = New Size(308, 23)
        TxtObsv.TabIndex = 6
        ' 
        ' DBGdeta
        ' 
        DBGdeta.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DBGdeta.Location = New Point(10, 351)
        DBGdeta.MultiSelect = False
        DBGdeta.Name = "DBGdeta"
        DBGdeta.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DBGdeta.Size = New Size(764, 292)
        DBGdeta.TabIndex = 2
        ' 
        ' frmResumen
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(784, 708)
        Controls.Add(GroupBoxResumen)
        Controls.Add(DBGdeta)
        Controls.Add(GroupBoxObservaciones)
        Controls.Add(GroupBoxSaldoActual)
        Controls.Add(DgvProveedores)
        Controls.Add(TxtBuscar)
        Name = "frmResumen"
        Text = "Resumen de Proveedores"
        CType(DgvProveedores, ComponentModel.ISupportInitialize).EndInit()
        GroupBoxResumen.ResumeLayout(False)
        GroupBoxResumen.PerformLayout()
        GroupBoxSaldoActual.ResumeLayout(False)
        GroupBoxSaldoActual.PerformLayout()
        GroupBoxObservaciones.ResumeLayout(False)
        GroupBoxObservaciones.PerformLayout()
        CType(DBGdeta, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents TxtBuscar As System.Windows.Forms.TextBox
    Friend WithEvents DgvProveedores As System.Windows.Forms.DataGridView
    Friend WithEvents DBGdeta As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBoxResumen As System.Windows.Forms.GroupBox
    Friend WithEvents LblProveedorTitle As System.Windows.Forms.Label
    Friend WithEvents LblProveedorValue As System.Windows.Forms.Label
    Friend WithEvents LblCUITTitle As System.Windows.Forms.Label
    Friend WithEvents LblCUITValue As System.Windows.Forms.Label
    Friend WithEvents LblCuentaTitle As System.Windows.Forms.Label
    Friend WithEvents LblCuentaValue As System.Windows.Forms.Label
    Friend WithEvents LblUltResTitle As System.Windows.Forms.Label
    Friend WithEvents LblUltResValue As System.Windows.Forms.Label
    Friend WithEvents LblSaldoActualTitle As System.Windows.Forms.Label
    Friend WithEvents LblSaldoActualValue As System.Windows.Forms.Label
    Friend WithEvents LblSaldoDtosTitle As System.Windows.Forms.Label
    Friend WithEvents LblSaldoDtosValue As System.Windows.Forms.Label
    Friend WithEvents LblSaldoSinDocTitle As System.Windows.Forms.Label
    Friend WithEvents TxtSaldoSinDoc As System.Windows.Forms.TextBox
    Friend WithEvents GroupBoxSaldoActual As System.Windows.Forms.GroupBox
    Friend WithEvents LblSaldoActualFrm2 As System.Windows.Forms.Label
    Friend WithEvents TxtSaldoActual As System.Windows.Forms.TextBox
    Friend WithEvents GroupBoxObservaciones As System.Windows.Forms.GroupBox
    Friend WithEvents TxtObsv As System.Windows.Forms.TextBox
End Class

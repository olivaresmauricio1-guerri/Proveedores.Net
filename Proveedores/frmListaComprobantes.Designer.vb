<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmListaComprobantes
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmListaComprobantes))
        GroupBox1 = New GroupBox()
        chkImpagas = New CheckBox()
        cmbTipoCte = New ComboBox()
        CmdSalir = New Button()
        CmdImprimir = New Button()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(chkImpagas)
        GroupBox1.Controls.Add(cmbTipoCte)
        GroupBox1.Location = New Point(12, 12)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(317, 56)
        GroupBox1.TabIndex = 0
        GroupBox1.TabStop = False
        GroupBox1.Text = "Seleccione tipo de Comprobante"
        ' 
        ' chkImpagas
        ' 
        chkImpagas.AutoSize = True
        chkImpagas.Location = New Point(243, 24)
        chkImpagas.Name = "chkImpagas"
        chkImpagas.Size = New Size(71, 19)
        chkImpagas.TabIndex = 1
        chkImpagas.Text = "Impagas"
        chkImpagas.UseVisualStyleBackColor = True
        ' 
        ' cmbTipoCte
        ' 
        cmbTipoCte.FormattingEnabled = True
        cmbTipoCte.Location = New Point(6, 22)
        cmbTipoCte.Name = "cmbTipoCte"
        cmbTipoCte.Size = New Size(212, 23)
        cmbTipoCte.TabIndex = 0
        ' 
        ' CmdSalir
        ' 
        CmdSalir.BackColor = Color.IndianRed
        CmdSalir.FlatStyle = FlatStyle.Flat
        CmdSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdSalir.ForeColor = Color.White
        CmdSalir.Location = New Point(245, 74)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(85, 33)
        CmdSalir.TabIndex = 3
        CmdSalir.Text = "Salir"
        CmdSalir.UseVisualStyleBackColor = False
        ' 
        ' CmdImprimir
        ' 
        CmdImprimir.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        CmdImprimir.FlatStyle = FlatStyle.Flat
        CmdImprimir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdImprimir.Location = New Point(154, 74)
        CmdImprimir.Name = "CmdImprimir"
        CmdImprimir.Size = New Size(85, 33)
        CmdImprimir.TabIndex = 4
        CmdImprimir.Text = "Imprimir"
        CmdImprimir.UseVisualStyleBackColor = True
        ' 
        ' frmListaComprobantes
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(342, 114)
        Controls.Add(CmdImprimir)
        Controls.Add(CmdSalir)
        Controls.Add(GroupBox1)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmListaComprobantes"
        Text = "Listado de Comprobantes"
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents chkImpagas As CheckBox
    Friend WithEvents cmbTipoCte As ComboBox
    Friend WithEvents CmdSalir As Button
    Friend WithEvents CmdImprimir As Button
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSifere
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        btnGenerar = New Button()
        btnSalir = New Button()
        SuspendLayout()
        ' 
        ' btnGenerar
        ' 
        btnGenerar.Location = New Point(14, 14)
        btnGenerar.Margin = New Padding(4, 3, 4, 3)
        btnGenerar.Name = "btnGenerar"
        btnGenerar.Size = New Size(169, 57)
        btnGenerar.TabIndex = 1
        btnGenerar.Text = "&Generar"
        btnGenerar.UseVisualStyleBackColor = True
        ' 
        ' btnSalir
        ' 
        btnSalir.Location = New Point(205, 14)
        btnSalir.Margin = New Padding(4, 3, 4, 3)
        btnSalir.Name = "btnSalir"
        btnSalir.Size = New Size(160, 57)
        btnSalir.TabIndex = 0
        btnSalir.Text = "&Salir"
        btnSalir.UseVisualStyleBackColor = True
        ' 
        ' frmSifere
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(379, 87)
        Controls.Add(btnSalir)
        Controls.Add(btnGenerar)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmSifere"
        StartPosition = FormStartPosition.CenterScreen
        Text = "S.I.F.E.R.E"
        ResumeLayout(False)

    End Sub

    Friend WithEvents btnGenerar As System.Windows.Forms.Button
    Friend WithEvents btnSalir As System.Windows.Forms.Button
End Class
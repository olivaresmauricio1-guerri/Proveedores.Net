<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmListaProveedores
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer
    Friend WithEvents Option8 As RadioButton
    Friend WithEvents Option7 As RadioButton
    Friend WithEvents Frame3 As GroupBox
    Friend WithEvents txtCuenta As TextBox
    Friend WithEvents txtFecha As TextBox
    Friend WithEvents datSucursales As Object
    Friend WithEvents cmdSalir As Button
    Friend WithEvents cmdVer As Button
    Friend WithEvents Frame1 As GroupBox
    Friend WithEvents Option9 As RadioButton
    Friend WithEvents Option5 As RadioButton
    Friend WithEvents Frame2 As GroupBox
    Friend WithEvents Option6 As RadioButton
    Friend WithEvents Option4 As RadioButton
    Friend WithEvents Option3 As RadioButton
    Friend WithEvents Option2 As RadioButton
    Friend WithEvents Option1 As RadioButton

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
        Option8 = New RadioButton()
        Option7 = New RadioButton()
        Frame3 = New GroupBox()
        txtCuenta = New TextBox()
        txtFecha = New TextBox()
        cmdSalir = New Button()
        cmdVer = New Button()
        Frame1 = New GroupBox()
        Option9 = New RadioButton()
        Option5 = New RadioButton()
        Frame2 = New GroupBox()
        Option6 = New RadioButton()
        Option4 = New RadioButton()
        Option3 = New RadioButton()
        Option2 = New RadioButton()
        Option1 = New RadioButton()
        Frame3.SuspendLayout()
        Frame1.SuspendLayout()
        Frame2.SuspendLayout()
        SuspendLayout()
        ' 
        ' Option8
        ' 
        Option8.AutoSize = True
        Option8.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        Option8.Location = New Point(12, 304)
        Option8.Name = "Option8"
        Option8.Size = New Size(176, 21)
        Option8.TabIndex = 14
        Option8.TabStop = True
        Option8.Text = "Listado por Proveedores"
        Option8.UseVisualStyleBackColor = True
        ' 
        ' Option7
        ' 
        Option7.AutoSize = True
        Option7.Checked = True
        Option7.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        Option7.Location = New Point(12, 277)
        Option7.Name = "Option7"
        Option7.Size = New Size(224, 21)
        Option7.TabIndex = 13
        Option7.TabStop = True
        Option7.Text = "Listado por días de vencimiento"
        Option7.UseVisualStyleBackColor = True
        ' 
        ' Frame3
        ' 
        Frame3.Controls.Add(txtCuenta)
        Frame3.Controls.Add(txtFecha)
        Frame3.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        Frame3.Location = New Point(12, 180)
        Frame3.Name = "Frame3"
        Frame3.Size = New Size(282, 80)
        Frame3.TabIndex = 10
        Frame3.TabStop = False
        Frame3.Text = "Recalcular Saldos al"
        ' 
        ' txtCuenta
        ' 
        txtCuenta.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        txtCuenta.ForeColor = Color.Blue
        txtCuenta.Location = New Point(182, 32)
        txtCuenta.Name = "txtCuenta"
        txtCuenta.Size = New Size(85, 25)
        txtCuenta.TabIndex = 12
        txtCuenta.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtFecha
        ' 
        txtFecha.Location = New Point(12, 32)
        txtFecha.Name = "txtFecha"
        txtFecha.Size = New Size(164, 25)
        txtFecha.TabIndex = 11
        ' 
        ' cmdSalir
        ' 
        cmdSalir.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        cmdSalir.BackColor = Color.IndianRed
        cmdSalir.FlatStyle = FlatStyle.Flat
        cmdSalir.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        cmdSalir.ForeColor = Color.White
        cmdSalir.Location = New Point(209, 342)
        cmdSalir.Name = "cmdSalir"
        cmdSalir.Size = New Size(85, 33)
        cmdSalir.TabIndex = 4
        cmdSalir.Text = "&Salir"
        cmdSalir.UseVisualStyleBackColor = False
        ' 
        ' cmdVer
        ' 
        cmdVer.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        cmdVer.FlatStyle = FlatStyle.Flat
        cmdVer.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        cmdVer.Location = New Point(118, 342)
        cmdVer.Name = "cmdVer"
        cmdVer.Size = New Size(85, 33)
        cmdVer.TabIndex = 3
        cmdVer.Text = "&Ver"
        cmdVer.UseVisualStyleBackColor = False
        ' 
        ' Frame1
        ' 
        Frame1.Controls.Add(Option9)
        Frame1.Controls.Add(Option5)
        Frame1.Controls.Add(Frame2)
        Frame1.Controls.Add(Option2)
        Frame1.Controls.Add(Option1)
        Frame1.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Frame1.ForeColor = Color.Black
        Frame1.Location = New Point(12, 12)
        Frame1.Name = "Frame1"
        Frame1.Size = New Size(282, 150)
        Frame1.TabIndex = 0
        Frame1.TabStop = False
        Frame1.Text = "Criterio de Búsqueda"
        ' 
        ' Option9
        ' 
        Option9.AutoSize = True
        Option9.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        Option9.ForeColor = Color.Black
        Option9.Location = New Point(169, 108)
        Option9.Name = "Option9"
        Option9.Size = New Size(83, 21)
        Option9.TabIndex = 15
        Option9.TabStop = True
        Option9.Text = "211 Recal"
        Option9.UseVisualStyleBackColor = True
        ' 
        ' Option5
        ' 
        Option5.AutoSize = True
        Option5.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        Option5.ForeColor = Color.Black
        Option5.Location = New Point(169, 80)
        Option5.Name = "Option5"
        Option5.Size = New Size(55, 21)
        Option5.TabIndex = 8
        Option5.TabStop = True
        Option5.Text = "2.1.1"
        Option5.UseVisualStyleBackColor = True
        ' 
        ' Frame2
        ' 
        Frame2.Controls.Add(Option6)
        Frame2.Controls.Add(Option4)
        Frame2.Controls.Add(Option3)
        Frame2.Location = New Point(12, 24)
        Frame2.Name = "Frame2"
        Frame2.Size = New Size(124, 94)
        Frame2.TabIndex = 5
        Frame2.TabStop = False
        ' 
        ' Option6
        ' 
        Option6.AutoSize = True
        Option6.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        Option6.ForeColor = Color.Black
        Option6.Location = New Point(6, 65)
        Option6.Name = "Option6"
        Option6.Size = New Size(63, 21)
        Option6.TabIndex = 9
        Option6.TabStop = True
        Option6.Text = "Todos"
        Option6.UseVisualStyleBackColor = True
        ' 
        ' Option4
        ' 
        Option4.AutoSize = True
        Option4.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        Option4.ForeColor = Color.Black
        Option4.Location = New Point(6, 41)
        Option4.Name = "Option4"
        Option4.Size = New Size(86, 21)
        Option4.TabIndex = 7
        Option4.TabStop = True
        Option4.Text = "Autoshop"
        Option4.UseVisualStyleBackColor = True
        ' 
        ' Option3
        ' 
        Option3.AutoSize = True
        Option3.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        Option3.ForeColor = Color.Black
        Option3.Location = New Point(6, 17)
        Option3.Name = "Option3"
        Option3.Size = New Size(76, 21)
        Option3.TabIndex = 6
        Option3.TabStop = True
        Option3.Text = "Guerrini"
        Option3.UseVisualStyleBackColor = True
        ' 
        ' Option2
        ' 
        Option2.AutoSize = True
        Option2.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        Option2.ForeColor = Color.Black
        Option2.Location = New Point(169, 56)
        Option2.Name = "Option2"
        Option2.Size = New Size(74, 21)
        Option2.TabIndex = 2
        Option2.TabStop = True
        Option2.Text = "C/Saldo"
        Option2.UseVisualStyleBackColor = True
        ' 
        ' Option1
        ' 
        Option1.AutoSize = True
        Option1.Checked = True
        Option1.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        Option1.ForeColor = Color.Black
        Option1.Location = New Point(169, 32)
        Option1.Name = "Option1"
        Option1.Size = New Size(63, 21)
        Option1.TabIndex = 1
        Option1.TabStop = True
        Option1.Text = "Todos"
        Option1.UseVisualStyleBackColor = True
        ' 
        ' frmListaProveedores
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(307, 387)
        Controls.Add(Option8)
        Controls.Add(Option7)
        Controls.Add(Frame3)
        Controls.Add(cmdSalir)
        Controls.Add(cmdVer)
        Controls.Add(Frame1)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmListaProveedores"
        Text = "Listado de Proveedores x Sucursal"
        Frame3.ResumeLayout(False)
        Frame3.PerformLayout()
        Frame1.ResumeLayout(False)
        Frame1.PerformLayout()
        Frame2.ResumeLayout(False)
        Frame2.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub
End Class


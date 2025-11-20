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
        Me.TxtBuscar = New System.Windows.Forms.TextBox()
        Me.DgvProveedores = New System.Windows.Forms.DataGridView()
        Me.DBGdeta = New System.Windows.Forms.DataGridView()
        CType(Me.DgvProveedores, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DBGdeta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        ' 
        ' TxtBuscar
        ' 
        Me.TxtBuscar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtBuscar.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular)
        Me.TxtBuscar.Location = New System.Drawing.Point(10, 10)
        Me.TxtBuscar.Name = "TxtBuscar"
        Me.TxtBuscar.Size = New System.Drawing.Size(764, 25)
        Me.TxtBuscar.TabIndex = 0
        ' 
        ' DgvProveedores
        ' 
        Me.DgvProveedores.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DgvProveedores.Location = New System.Drawing.Point(10, 40)
        Me.DgvProveedores.MultiSelect = False
        Me.DgvProveedores.Name = "DgvProveedores"
        Me.DgvProveedores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvProveedores.Size = New System.Drawing.Size(764, 200)
        Me.DgvProveedores.TabIndex = 1
        ' 
        ' DBGdeta
        ' 
        Me.DBGdeta.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DBGdeta.Location = New System.Drawing.Point(10, 250)
        Me.DBGdeta.MultiSelect = False
        Me.DBGdeta.Name = "DBGdeta"
        Me.DBGdeta.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DBGdeta.Size = New System.Drawing.Size(764, 340)
        Me.DBGdeta.TabIndex = 2
        ' 
        ' frmResumen
        ' 
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 601)
        Me.Controls.Add(Me.DBGdeta)
        Me.Controls.Add(Me.DgvProveedores)
        Me.Controls.Add(Me.TxtBuscar)
        Me.Name = "frmResumen"
        Me.Text = "Resumen de Proveedores"
        CType(Me.DgvProveedores, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DBGdeta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents TxtBuscar As System.Windows.Forms.TextBox
    Friend WithEvents DgvProveedores As System.Windows.Forms.DataGridView
    Friend WithEvents DBGdeta As System.Windows.Forms.DataGridView
End Class

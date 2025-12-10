<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmListadoNovedades
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmListadoNovedades))
        GroupBox1 = New GroupBox()
        CmdImprimir = New Button()
        CmdSalir = New Button()
        cmbSucursales = New ComboBox()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(CmdImprimir)
        GroupBox1.Controls.Add(CmdSalir)
        GroupBox1.Controls.Add(cmbSucursales)
        GroupBox1.Location = New Point(2, 0)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(263, 101)
        GroupBox1.TabIndex = 0
        GroupBox1.TabStop = False
        GroupBox1.Text = "Sucursal"
        ' 
        ' CmdImprimir
        ' 
        CmdImprimir.FlatStyle = FlatStyle.Flat
        CmdImprimir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdImprimir.Location = New Point(100, 64)
        CmdImprimir.Name = "CmdImprimir"
        CmdImprimir.Size = New Size(75, 30)
        CmdImprimir.TabIndex = 15
        CmdImprimir.Text = "Imprimir"
        CmdImprimir.UseVisualStyleBackColor = True
        ' 
        ' CmdSalir
        ' 
        CmdSalir.BackColor = Color.IndianRed
        CmdSalir.FlatStyle = FlatStyle.Flat
        CmdSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdSalir.ForeColor = Color.White
        CmdSalir.Location = New Point(182, 64)
        CmdSalir.Margin = New Padding(4, 3, 4, 3)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(75, 30)
        CmdSalir.TabIndex = 14
        CmdSalir.Text = "Salir"
        CmdSalir.UseVisualStyleBackColor = False
        ' 
        ' cmbSucursales
        ' 
        cmbSucursales.FormattingEnabled = True
        cmbSucursales.Location = New Point(6, 22)
        cmbSucursales.Name = "cmbSucursales"
        cmbSucursales.Size = New Size(251, 23)
        cmbSucursales.TabIndex = 0
        ' 
        ' frmListadoNovedades
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(266, 106)
        Controls.Add(GroupBox1)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmListadoNovedades"
        Text = "Listado de Novedades"
        GroupBox1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents cmbSucursales As ComboBox
    Friend WithEvents CmdImprimir As Button
    Friend WithEvents CmdSalir As Button
End Class

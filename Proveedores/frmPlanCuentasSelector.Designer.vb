<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPlanCuentasSelector
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPlanCuentasSelector))
        LblBuscar = New Label()
        TxtBuscar = New TextBox()
        DgvListado = New DataGridView()
        CmdAceptar = New Button()
        CmdSalir = New Button()
        CType(DgvListado, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' LblBuscar
        ' 
        LblBuscar.Location = New Point(12, 12)
        LblBuscar.Name = "LblBuscar"
        LblBuscar.Size = New Size(47, 23)
        LblBuscar.TabIndex = 0
        LblBuscar.Text = "Buscar:"
        ' 
        ' TxtBuscar
        ' 
        TxtBuscar.Location = New Point(61, 9)
        TxtBuscar.Name = "TxtBuscar"
        TxtBuscar.Size = New Size(513, 23)
        TxtBuscar.TabIndex = 1
        ' 
        ' DgvListado
        ' 
        DgvListado.AllowUserToAddRows = False
        DgvListado.AllowUserToDeleteRows = False
        DgvListado.AllowUserToResizeColumns = False
        DgvListado.AllowUserToResizeRows = False
        DgvListado.Location = New Point(12, 40)
        DgvListado.Name = "DgvListado"
        DgvListado.ReadOnly = True
        DgvListado.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DgvListado.Size = New Size(562, 250)
        DgvListado.TabIndex = 2
        ' 
        ' CmdAceptar
        ' 
        CmdAceptar.Cursor = Cursors.Hand
        CmdAceptar.FlatStyle = FlatStyle.Flat
        CmdAceptar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdAceptar.Location = New Point(418, 296)
        CmdAceptar.Name = "CmdAceptar"
        CmdAceptar.Size = New Size(75, 30)
        CmdAceptar.TabIndex = 23
        CmdAceptar.Text = "&Aceptar"
        ' 
        ' CmdSalir
        ' 
        CmdSalir.BackColor = Color.IndianRed
        CmdSalir.Cursor = Cursors.Hand
        CmdSalir.FlatStyle = FlatStyle.Flat
        CmdSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdSalir.ForeColor = Color.White
        CmdSalir.Location = New Point(499, 296)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(75, 30)
        CmdSalir.TabIndex = 25
        CmdSalir.Text = "&Salir"
        CmdSalir.UseVisualStyleBackColor = False
        ' 
        ' frmPlanCuentasSelector
        ' 
        ClientSize = New Size(586, 339)
        Controls.Add(LblBuscar)
        Controls.Add(TxtBuscar)
        Controls.Add(DgvListado)
        Controls.Add(CmdAceptar)
        Controls.Add(CmdSalir)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmPlanCuentasSelector"
        Text = "Selector de Plan de Cuentas"
        CType(DgvListado, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents LblBuscar As Label
    Friend WithEvents TxtBuscar As TextBox
    Friend WithEvents DgvListado As DataGridView
    Friend WithEvents CmdAceptar As Button
    Friend WithEvents CmdSalir As Button
End Class

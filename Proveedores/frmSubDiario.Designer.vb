<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSubDiario
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSubDiario))
        GroupBoxParametros = New GroupBox()
        Label1 = New Label()
        txtNro = New TextBox()
        Label4 = New Label()
        dbcEmpresa = New ComboBox()
        Label3 = New Label()
        dbcSucursal = New ComboBox()
        Label5 = New Label()
        dbcMeses = New ComboBox()
        Label6 = New Label()
        txtAno = New TextBox()
        GroupBoxCierre = New GroupBox()
        CmdOKCierre = New Button()
        LblFecha = New Label()
        txtFecha = New TextBox()
        dtFecha = New DateTimePicker()
        cmdVer = New Button()
        CmdDecreto = New Button()
        CmdLibroElectronico = New Button()
        CmdSIFERE = New Button()
        CmdArmaArchivo = New Button()
        CmdSalir = New Button()
        GroupBoxParametros.SuspendLayout()
        GroupBoxCierre.SuspendLayout()
        SuspendLayout()
        ' 
        ' GroupBoxParametros
        ' 
        GroupBoxParametros.Controls.Add(Label1)
        GroupBoxParametros.Controls.Add(txtNro)
        GroupBoxParametros.Controls.Add(Label4)
        GroupBoxParametros.Controls.Add(dbcEmpresa)
        GroupBoxParametros.Controls.Add(Label3)
        GroupBoxParametros.Controls.Add(dbcSucursal)
        GroupBoxParametros.Controls.Add(Label5)
        GroupBoxParametros.Controls.Add(dbcMeses)
        GroupBoxParametros.Controls.Add(Label6)
        GroupBoxParametros.Controls.Add(txtAno)
        GroupBoxParametros.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        GroupBoxParametros.ForeColor = Color.Black
        GroupBoxParametros.Location = New Point(0, 3)
        GroupBoxParametros.Name = "GroupBoxParametros"
        GroupBoxParametros.Size = New Size(399, 151)
        GroupBoxParametros.TabIndex = 0
        GroupBoxParametros.TabStop = False
        GroupBoxParametros.Text = "Parámetros de Emisión"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label1.Location = New Point(12, 30)
        Label1.Name = "Label1"
        Label1.Size = New Size(79, 15)
        Label1.TabIndex = 0
        Label1.Text = "Nro. de Libro"
        ' 
        ' txtNro
        ' 
        txtNro.Font = New Font("Segoe UI", 9F)
        txtNro.Location = New Point(120, 27)
        txtNro.Name = "txtNro"
        txtNro.Size = New Size(129, 23)
        txtNro.TabIndex = 1
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label4.Location = New Point(12, 59)
        Label4.Name = "Label4"
        Label4.Size = New Size(54, 15)
        Label4.TabIndex = 2
        Label4.Text = "Empresa"
        ' 
        ' dbcEmpresa
        ' 
        dbcEmpresa.DropDownStyle = ComboBoxStyle.DropDownList
        dbcEmpresa.Font = New Font("Segoe UI", 9F)
        dbcEmpresa.Location = New Point(120, 56)
        dbcEmpresa.Name = "dbcEmpresa"
        dbcEmpresa.Size = New Size(267, 23)
        dbcEmpresa.TabIndex = 2
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label3.Location = New Point(12, 88)
        Label3.Name = "Label3"
        Label3.Size = New Size(53, 15)
        Label3.TabIndex = 4
        Label3.Text = "Sucursal"
        ' 
        ' dbcSucursal
        ' 
        dbcSucursal.DropDownStyle = ComboBoxStyle.DropDownList
        dbcSucursal.Font = New Font("Segoe UI", 9F)
        dbcSucursal.Location = New Point(120, 85)
        dbcSucursal.Name = "dbcSucursal"
        dbcSucursal.Size = New Size(267, 23)
        dbcSucursal.TabIndex = 3
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label5.Location = New Point(12, 117)
        Label5.Name = "Label5"
        Label5.Size = New Size(30, 15)
        Label5.TabIndex = 6
        Label5.Text = "Mes"
        ' 
        ' dbcMeses
        ' 
        dbcMeses.DropDownStyle = ComboBoxStyle.DropDownList
        dbcMeses.Font = New Font("Segoe UI", 9F)
        dbcMeses.Location = New Point(120, 114)
        dbcMeses.Name = "dbcMeses"
        dbcMeses.Size = New Size(129, 23)
        dbcMeses.TabIndex = 4
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label6.Location = New Point(267, 117)
        Label6.Name = "Label6"
        Label6.Size = New Size(29, 15)
        Label6.TabIndex = 8
        Label6.Text = "Año"
        ' 
        ' txtAno
        ' 
        txtAno.Font = New Font("Segoe UI", 9F)
        txtAno.Location = New Point(307, 114)
        txtAno.Name = "txtAno"
        txtAno.Size = New Size(80, 23)
        txtAno.TabIndex = 5
        ' 
        ' GroupBoxCierre
        ' 
        GroupBoxCierre.Controls.Add(CmdOKCierre)
        GroupBoxCierre.Controls.Add(LblFecha)
        GroupBoxCierre.Controls.Add(txtFecha)
        GroupBoxCierre.Controls.Add(dtFecha)
        GroupBoxCierre.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        GroupBoxCierre.ForeColor = Color.Black
        GroupBoxCierre.Location = New Point(12, 292)
        GroupBoxCierre.Name = "GroupBoxCierre"
        GroupBoxCierre.Size = New Size(387, 67)
        GroupBoxCierre.TabIndex = 1
        GroupBoxCierre.TabStop = False
        GroupBoxCierre.Text = "Fecha de cierre del Sub-Diario"
        ' 
        ' CmdOKCierre
        ' 
        CmdOKCierre.FlatStyle = FlatStyle.Flat
        CmdOKCierre.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdOKCierre.Location = New Point(287, 27)
        CmdOKCierre.Name = "CmdOKCierre"
        CmdOKCierre.Size = New Size(88, 32)
        CmdOKCierre.TabIndex = 13
        CmdOKCierre.Text = "OK"
        CmdOKCierre.UseVisualStyleBackColor = True
        ' 
        ' LblFecha
        ' 
        LblFecha.AutoSize = True
        LblFecha.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LblFecha.Location = New Point(12, 36)
        LblFecha.Name = "LblFecha"
        LblFecha.Size = New Size(39, 15)
        LblFecha.TabIndex = 0
        LblFecha.Text = "Fecha"
        ' 
        ' txtFecha
        ' 
        txtFecha.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        txtFecha.Location = New Point(60, 32)
        txtFecha.MaxLength = 10
        txtFecha.Name = "txtFecha"
        txtFecha.ReadOnly = True
        txtFecha.Size = New Size(91, 23)
        txtFecha.TabIndex = 11
        ' 
        ' dtFecha
        ' 
        dtFecha.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        dtFecha.Format = DateTimePickerFormat.Short
        dtFecha.Location = New Point(157, 32)
        dtFecha.Name = "dtFecha"
        dtFecha.Size = New Size(104, 23)
        dtFecha.TabIndex = 12
        ' 
        ' cmdVer
        ' 
        cmdVer.FlatStyle = FlatStyle.Flat
        cmdVer.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdVer.Location = New Point(57, 207)
        cmdVer.Name = "cmdVer"
        cmdVer.Size = New Size(120, 32)
        cmdVer.TabIndex = 7
        cmdVer.Text = "SubDiario"
        cmdVer.UseVisualStyleBackColor = True
        ' 
        ' CmdDecreto
        ' 
        CmdDecreto.FlatStyle = FlatStyle.Flat
        CmdDecreto.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdDecreto.Location = New Point(237, 207)
        CmdDecreto.Name = "CmdDecreto"
        CmdDecreto.Size = New Size(120, 32)
        CmdDecreto.TabIndex = 8
        CmdDecreto.Text = "Decreto 1361"
        CmdDecreto.UseVisualStyleBackColor = True
        ' 
        ' CmdLibroElectronico
        ' 
        CmdLibroElectronico.FlatStyle = FlatStyle.Flat
        CmdLibroElectronico.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdLibroElectronico.Location = New Point(57, 245)
        CmdLibroElectronico.Name = "CmdLibroElectronico"
        CmdLibroElectronico.Size = New Size(120, 32)
        CmdLibroElectronico.TabIndex = 9
        CmdLibroElectronico.Text = "Libro Electrónico"
        CmdLibroElectronico.UseVisualStyleBackColor = True
        ' 
        ' CmdSIFERE
        ' 
        CmdSIFERE.FlatStyle = FlatStyle.Flat
        CmdSIFERE.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdSIFERE.Location = New Point(237, 245)
        CmdSIFERE.Name = "CmdSIFERE"
        CmdSIFERE.Size = New Size(120, 32)
        CmdSIFERE.TabIndex = 10
        CmdSIFERE.Text = "S.I.F.E.R.E."
        CmdSIFERE.UseVisualStyleBackColor = True
        ' 
        ' CmdArmaArchivo
        ' 
        CmdArmaArchivo.FlatStyle = FlatStyle.Flat
        CmdArmaArchivo.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdArmaArchivo.Location = New Point(57, 169)
        CmdArmaArchivo.Name = "CmdArmaArchivo"
        CmdArmaArchivo.Size = New Size(300, 32)
        CmdArmaArchivo.TabIndex = 6
        CmdArmaArchivo.Text = "Armar Archivo del mes solicitado"
        CmdArmaArchivo.UseVisualStyleBackColor = True
        ' 
        ' CmdSalir
        ' 
        CmdSalir.BackColor = Color.IndianRed
        CmdSalir.FlatStyle = FlatStyle.Flat
        CmdSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdSalir.ForeColor = Color.White
        CmdSalir.Location = New Point(299, 365)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(88, 32)
        CmdSalir.TabIndex = 14
        CmdSalir.Text = "Salir"
        CmdSalir.UseVisualStyleBackColor = False
        ' 
        ' frmSubDiario
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(404, 408)
        Controls.Add(CmdSalir)
        Controls.Add(CmdArmaArchivo)
        Controls.Add(CmdDecreto)
        Controls.Add(CmdSIFERE)
        Controls.Add(CmdLibroElectronico)
        Controls.Add(cmdVer)
        Controls.Add(GroupBoxCierre)
        Controls.Add(GroupBoxParametros)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmSubDiario"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Emisión de Sub-Diarios IVA"
        GroupBoxParametros.ResumeLayout(False)
        GroupBoxParametros.PerformLayout()
        GroupBoxCierre.ResumeLayout(False)
        GroupBoxCierre.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents GroupBoxParametros As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtNro As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents dbcEmpresa As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents dbcSucursal As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents dbcMeses As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtAno As TextBox
    Friend WithEvents GroupBoxCierre As GroupBox
    Friend WithEvents LblFecha As Label
    Friend WithEvents txtFecha As TextBox
    Friend WithEvents dtFecha As DateTimePicker
    Friend WithEvents cmdVer As Button
    Friend WithEvents CmdDecreto As Button
    Friend WithEvents CmdLibroElectronico As Button
    Friend WithEvents CmdSIFERE As Button
    Friend WithEvents CmdArmaArchivo As Button
    Friend WithEvents CmdSalir As Button
    Friend WithEvents CmdOKCierre As Button
End Class


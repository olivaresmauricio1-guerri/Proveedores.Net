<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOrdenPago
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
        btnBuscarProveedor = New Button()
        cmbProveedor = New ComboBox()
        lblProveedor = New Label()
        DgvListado = New DataGridView()
        Label1 = New Label()
        ComboBox1 = New ComboBox()
        ComboBox2 = New ComboBox()
        Label2 = New Label()
        txtNroCuenta = New TextBox()
        Label3 = New Label()
        Label4 = New Label()
        TextBox1 = New TextBox()
        Label5 = New Label()
        TextBox2 = New TextBox()
        ComboBox3 = New ComboBox()
        Label6 = New Label()
        ComboBox4 = New ComboBox()
        Button1 = New Button()
        Label7 = New Label()
        DateTimePicker1 = New DateTimePicker()
        Label8 = New Label()
        TextBox3 = New TextBox()
        ComboBox5 = New ComboBox()
        Label9 = New Label()
        TextBox4 = New TextBox()
        DataGridView1 = New DataGridView()
        CType(DgvListado, ComponentModel.ISupportInitialize).BeginInit()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btnBuscarProveedor
        ' 
        btnBuscarProveedor.Cursor = Cursors.Hand
        btnBuscarProveedor.FlatStyle = FlatStyle.Flat
        btnBuscarProveedor.Location = New Point(569, 6)
        btnBuscarProveedor.Name = "btnBuscarProveedor"
        btnBuscarProveedor.Size = New Size(56, 23)
        btnBuscarProveedor.TabIndex = 109
        btnBuscarProveedor.Text = "Buscar"
        ' 
        ' cmbProveedor
        ' 
        cmbProveedor.FormattingEnabled = True
        cmbProveedor.Location = New Point(82, 6)
        cmbProveedor.Name = "cmbProveedor"
        cmbProveedor.Size = New Size(481, 23)
        cmbProveedor.TabIndex = 108
        ' 
        ' lblProveedor
        ' 
        lblProveedor.AutoSize = True
        lblProveedor.Location = New Point(12, 9)
        lblProveedor.Name = "lblProveedor"
        lblProveedor.Size = New Size(64, 15)
        lblProveedor.TabIndex = 107
        lblProveedor.Text = "Proveedor:"
        ' 
        ' DgvListado
        ' 
        DgvListado.AllowUserToAddRows = False
        DgvListado.AllowUserToDeleteRows = False
        DgvListado.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DgvListado.Location = New Point(12, 35)
        DgvListado.Name = "DgvListado"
        DgvListado.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DgvListado.Size = New Size(987, 233)
        DgvListado.TabIndex = 110
        ' 
        ' Label1
        ' 
        Label1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label1.AutoSize = True
        Label1.Location = New Point(12, 271)
        Label1.Name = "Label1"
        Label1.Size = New Size(46, 15)
        Label1.TabIndex = 111
        Label1.Text = "Factura"
        ' 
        ' ComboBox1
        ' 
        ComboBox1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        ComboBox1.FormattingEnabled = True
        ComboBox1.Location = New Point(12, 289)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(121, 23)
        ComboBox1.TabIndex = 112
        ' 
        ' ComboBox2
        ' 
        ComboBox2.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        ComboBox2.FormattingEnabled = True
        ComboBox2.Location = New Point(139, 289)
        ComboBox2.Name = "ComboBox2"
        ComboBox2.Size = New Size(170, 23)
        ComboBox2.TabIndex = 114
        ' 
        ' Label2
        ' 
        Label2.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label2.AutoSize = True
        Label2.Location = New Point(139, 271)
        Label2.Name = "Label2"
        Label2.Size = New Size(87, 15)
        Label2.TabIndex = 113
        Label2.Text = "Forma de Pago"
        ' 
        ' txtNroCuenta
        ' 
        txtNroCuenta.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        txtNroCuenta.Enabled = False
        txtNroCuenta.Location = New Point(315, 289)
        txtNroCuenta.Name = "txtNroCuenta"
        txtNroCuenta.Size = New Size(82, 23)
        txtNroCuenta.TabIndex = 115
        ' 
        ' Label3
        ' 
        Label3.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label3.AutoSize = True
        Label3.Location = New Point(315, 271)
        Label3.Name = "Label3"
        Label3.Size = New Size(45, 15)
        Label3.TabIndex = 116
        Label3.Text = "Interno"
        ' 
        ' Label4
        ' 
        Label4.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label4.AutoSize = True
        Label4.Location = New Point(403, 271)
        Label4.Name = "Label4"
        Label4.Size = New Size(49, 15)
        Label4.TabIndex = 118
        Label4.Text = "Importe"
        ' 
        ' TextBox1
        ' 
        TextBox1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        TextBox1.Enabled = False
        TextBox1.Location = New Point(403, 289)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(82, 23)
        TextBox1.TabIndex = 117
        ' 
        ' Label5
        ' 
        Label5.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label5.AutoSize = True
        Label5.Location = New Point(491, 271)
        Label5.Name = "Label5"
        Label5.Size = New Size(35, 15)
        Label5.TabIndex = 120
        Label5.Text = "Dolar"
        ' 
        ' TextBox2
        ' 
        TextBox2.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        TextBox2.Enabled = False
        TextBox2.Location = New Point(491, 289)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(82, 23)
        TextBox2.TabIndex = 119
        ' 
        ' ComboBox3
        ' 
        ComboBox3.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        ComboBox3.FormattingEnabled = True
        ComboBox3.Location = New Point(579, 289)
        ComboBox3.Name = "ComboBox3"
        ComboBox3.Size = New Size(82, 23)
        ComboBox3.TabIndex = 122
        ' 
        ' Label6
        ' 
        Label6.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label6.AutoSize = True
        Label6.Location = New Point(579, 271)
        Label6.Name = "Label6"
        Label6.Size = New Size(45, 15)
        Label6.TabIndex = 121
        Label6.Text = "Cuenta"
        ' 
        ' ComboBox4
        ' 
        ComboBox4.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        ComboBox4.FormattingEnabled = True
        ComboBox4.Location = New Point(667, 289)
        ComboBox4.Name = "ComboBox4"
        ComboBox4.Size = New Size(257, 23)
        ComboBox4.TabIndex = 123
        ' 
        ' Button1
        ' 
        Button1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button1.Cursor = Cursors.Hand
        Button1.FlatStyle = FlatStyle.Flat
        Button1.Location = New Point(930, 288)
        Button1.Name = "Button1"
        Button1.Size = New Size(69, 68)
        Button1.TabIndex = 124
        Button1.Text = "Grabar"
        ' 
        ' Label7
        ' 
        Label7.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label7.AutoSize = True
        Label7.Location = New Point(12, 315)
        Label7.Name = "Label7"
        Label7.Size = New Size(28, 15)
        Label7.TabIndex = 125
        Label7.Text = "Vto."
        ' 
        ' DateTimePicker1
        ' 
        DateTimePicker1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        DateTimePicker1.Format = DateTimePickerFormat.Short
        DateTimePicker1.Location = New Point(12, 333)
        DateTimePicker1.Name = "DateTimePicker1"
        DateTimePicker1.Size = New Size(102, 23)
        DateTimePicker1.TabIndex = 126
        ' 
        ' Label8
        ' 
        Label8.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label8.AutoSize = True
        Label8.Location = New Point(120, 315)
        Label8.Name = "Label8"
        Label8.Size = New Size(61, 15)
        Label8.TabIndex = 128
        Label8.Text = "Nro. Talón"
        ' 
        ' TextBox3
        ' 
        TextBox3.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        TextBox3.Enabled = False
        TextBox3.Location = New Point(120, 333)
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(147, 23)
        TextBox3.TabIndex = 127
        ' 
        ' ComboBox5
        ' 
        ComboBox5.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        ComboBox5.FormattingEnabled = True
        ComboBox5.Location = New Point(273, 333)
        ComboBox5.Name = "ComboBox5"
        ComboBox5.Size = New Size(170, 23)
        ComboBox5.TabIndex = 130
        ' 
        ' Label9
        ' 
        Label9.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label9.AutoSize = True
        Label9.Location = New Point(273, 315)
        Label9.Name = "Label9"
        Label9.Size = New Size(40, 15)
        Label9.TabIndex = 129
        Label9.Text = "Banco"
        ' 
        ' TextBox4
        ' 
        TextBox4.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        TextBox4.Enabled = False
        TextBox4.Location = New Point(449, 333)
        TextBox4.Name = "TextBox4"
        TextBox4.Size = New Size(475, 23)
        TextBox4.TabIndex = 131
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = False
        DataGridView1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataGridView1.Location = New Point(12, 362)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.Size = New Size(987, 222)
        DataGridView1.TabIndex = 132
        ' 
        ' frmOrdenPago
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1011, 652)
        Controls.Add(DataGridView1)
        Controls.Add(TextBox4)
        Controls.Add(ComboBox5)
        Controls.Add(Label9)
        Controls.Add(Label8)
        Controls.Add(TextBox3)
        Controls.Add(DateTimePicker1)
        Controls.Add(Label7)
        Controls.Add(Button1)
        Controls.Add(ComboBox4)
        Controls.Add(ComboBox3)
        Controls.Add(Label6)
        Controls.Add(Label5)
        Controls.Add(TextBox2)
        Controls.Add(Label4)
        Controls.Add(TextBox1)
        Controls.Add(Label3)
        Controls.Add(txtNroCuenta)
        Controls.Add(ComboBox2)
        Controls.Add(Label2)
        Controls.Add(ComboBox1)
        Controls.Add(Label1)
        Controls.Add(DgvListado)
        Controls.Add(btnBuscarProveedor)
        Controls.Add(cmbProveedor)
        Controls.Add(lblProveedor)
        MinimizeBox = False
        Name = "frmOrdenPago"
        Text = "Emisión Ordenes de Pago"
        CType(DgvListado, ComponentModel.ISupportInitialize).EndInit()
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btnBuscarProveedor As Button
    Friend WithEvents cmbProveedor As ComboBox
    Friend WithEvents lblProveedor As Label
    Friend WithEvents DgvListado As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtNroCuenta As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents ComboBox3 As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents ComboBox4 As ComboBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Label8 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents ComboBox5 As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents DataGridView1 As DataGridView
End Class

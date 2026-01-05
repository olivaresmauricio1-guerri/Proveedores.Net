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
        dgvComprobantes = New DataGridView()
        Label1 = New Label()
        cmbFactura = New ComboBox()
        cmbFormaPago = New ComboBox()
        Label2 = New Label()
        txtInterno = New TextBox()
        Label3 = New Label()
        Label4 = New Label()
        txtImporte = New TextBox()
        Label5 = New Label()
        txtDolar = New TextBox()
        cmbCuenta = New ComboBox()
        Label6 = New Label()
        cmbRubro = New ComboBox()
        btnGrabar = New Button()
        Label7 = New Label()
        dtpVto = New DateTimePicker()
        Label8 = New Label()
        txtTalon = New TextBox()
        cmbBancos = New ComboBox()
        Label9 = New Label()
        txtCtaCli = New TextBox()
        dgvOrden = New DataGridView()
        txtImputaConta = New TextBox()
        dtpFecha = New DateTimePicker()
        txtNroOrden = New TextBox()
        lblConfecciona = New Label()
        txtConfecciona = New TextBox()
        txtTotal = New TextBox()
        btnBorrar = New Button()
        btnImportarValores = New Button()
        btnImprimir = New Button()
        btnSalir = New Button()
        lblRubro = New Label()
        lblExterior = New Label()
        cmbPagado = New ComboBox()
        Label10 = New Label()
        CType(dgvComprobantes, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvOrden, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btnBuscarProveedor
        ' 
        btnBuscarProveedor.Cursor = Cursors.Hand
        btnBuscarProveedor.FlatStyle = FlatStyle.Flat
        btnBuscarProveedor.Location = New Point(319, 4)
        btnBuscarProveedor.Name = "btnBuscarProveedor"
        btnBuscarProveedor.Size = New Size(109, 23)
        btnBuscarProveedor.TabIndex = 109
        btnBuscarProveedor.Text = "Buscar Proveedor"
        ' 
        ' cmbProveedor
        ' 
        cmbProveedor.FormattingEnabled = True
        cmbProveedor.Location = New Point(82, 5)
        cmbProveedor.Name = "cmbProveedor"
        cmbProveedor.Size = New Size(231, 23)
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
        ' dgvComprobantes
        ' 
        dgvComprobantes.AllowUserToAddRows = False
        dgvComprobantes.AllowUserToDeleteRows = False
        dgvComprobantes.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvComprobantes.Location = New Point(12, 35)
        dgvComprobantes.Name = "dgvComprobantes"
        dgvComprobantes.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvComprobantes.Size = New Size(987, 209)
        dgvComprobantes.TabIndex = 110
        ' 
        ' Label1
        ' 
        Label1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label1.AutoSize = True
        Label1.Location = New Point(12, 250)
        Label1.Name = "Label1"
        Label1.Size = New Size(46, 15)
        Label1.TabIndex = 111
        Label1.Text = "Factura"
        ' 
        ' cmbFactura
        ' 
        cmbFactura.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        cmbFactura.FormattingEnabled = True
        cmbFactura.Location = New Point(12, 268)
        cmbFactura.Name = "cmbFactura"
        cmbFactura.Size = New Size(121, 23)
        cmbFactura.TabIndex = 112
        ' 
        ' cmbFormaPago
        ' 
        cmbFormaPago.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        cmbFormaPago.DropDownStyle = ComboBoxStyle.DropDownList
        cmbFormaPago.FormattingEnabled = True
        cmbFormaPago.Location = New Point(139, 268)
        cmbFormaPago.Name = "cmbFormaPago"
        cmbFormaPago.Size = New Size(170, 23)
        cmbFormaPago.TabIndex = 114
        ' 
        ' Label2
        ' 
        Label2.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label2.AutoSize = True
        Label2.Location = New Point(139, 250)
        Label2.Name = "Label2"
        Label2.Size = New Size(87, 15)
        Label2.TabIndex = 113
        Label2.Text = "Forma de Pago"
        ' 
        ' txtInterno
        ' 
        txtInterno.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        txtInterno.Enabled = False
        txtInterno.Location = New Point(315, 268)
        txtInterno.Name = "txtInterno"
        txtInterno.Size = New Size(82, 23)
        txtInterno.TabIndex = 115
        ' 
        ' Label3
        ' 
        Label3.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label3.AutoSize = True
        Label3.Location = New Point(315, 250)
        Label3.Name = "Label3"
        Label3.Size = New Size(45, 15)
        Label3.TabIndex = 116
        Label3.Text = "Interno"
        ' 
        ' Label4
        ' 
        Label4.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label4.AutoSize = True
        Label4.Location = New Point(403, 250)
        Label4.Name = "Label4"
        Label4.Size = New Size(49, 15)
        Label4.TabIndex = 118
        Label4.Text = "Importe"
        ' 
        ' txtImporte
        ' 
        txtImporte.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        txtImporte.Enabled = False
        txtImporte.Location = New Point(403, 268)
        txtImporte.Name = "txtImporte"
        txtImporte.Size = New Size(82, 23)
        txtImporte.TabIndex = 117
        ' 
        ' Label5
        ' 
        Label5.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label5.AutoSize = True
        Label5.Location = New Point(491, 250)
        Label5.Name = "Label5"
        Label5.Size = New Size(35, 15)
        Label5.TabIndex = 120
        Label5.Text = "Dolar"
        ' 
        ' txtDolar
        ' 
        txtDolar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        txtDolar.Enabled = False
        txtDolar.Location = New Point(491, 268)
        txtDolar.Name = "txtDolar"
        txtDolar.Size = New Size(82, 23)
        txtDolar.TabIndex = 119
        ' 
        ' cmbCuenta
        ' 
        cmbCuenta.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        cmbCuenta.FormattingEnabled = True
        cmbCuenta.Location = New Point(579, 268)
        cmbCuenta.Name = "cmbCuenta"
        cmbCuenta.Size = New Size(82, 23)
        cmbCuenta.TabIndex = 122
        ' 
        ' Label6
        ' 
        Label6.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label6.AutoSize = True
        Label6.Location = New Point(579, 250)
        Label6.Name = "Label6"
        Label6.Size = New Size(45, 15)
        Label6.TabIndex = 121
        Label6.Text = "Cuenta"
        ' 
        ' cmbRubro
        ' 
        cmbRubro.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        cmbRubro.DropDownStyle = ComboBoxStyle.DropDownList
        cmbRubro.FormattingEnabled = True
        cmbRubro.Location = New Point(667, 268)
        cmbRubro.Name = "cmbRubro"
        cmbRubro.Size = New Size(257, 23)
        cmbRubro.TabIndex = 123
        ' 
        ' btnGrabar
        ' 
        btnGrabar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnGrabar.Cursor = Cursors.Hand
        btnGrabar.FlatStyle = FlatStyle.Flat
        btnGrabar.Location = New Point(930, 267)
        btnGrabar.Name = "btnGrabar"
        btnGrabar.Size = New Size(69, 68)
        btnGrabar.TabIndex = 124
        btnGrabar.Text = "Grabar"
        ' 
        ' Label7
        ' 
        Label7.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label7.AutoSize = True
        Label7.Location = New Point(12, 294)
        Label7.Name = "Label7"
        Label7.Size = New Size(28, 15)
        Label7.TabIndex = 125
        Label7.Text = "Vto."
        ' 
        ' dtpVto
        ' 
        dtpVto.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        dtpVto.Format = DateTimePickerFormat.Short
        dtpVto.Location = New Point(12, 312)
        dtpVto.Name = "dtpVto"
        dtpVto.Size = New Size(102, 23)
        dtpVto.TabIndex = 126
        ' 
        ' Label8
        ' 
        Label8.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label8.AutoSize = True
        Label8.Location = New Point(120, 294)
        Label8.Name = "Label8"
        Label8.Size = New Size(61, 15)
        Label8.TabIndex = 128
        Label8.Text = "Nro. Talón"
        ' 
        ' txtTalon
        ' 
        txtTalon.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        txtTalon.Enabled = False
        txtTalon.Location = New Point(120, 312)
        txtTalon.Name = "txtTalon"
        txtTalon.Size = New Size(147, 23)
        txtTalon.TabIndex = 127
        ' 
        ' cmbBancos
        ' 
        cmbBancos.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        cmbBancos.FormattingEnabled = True
        cmbBancos.Location = New Point(273, 312)
        cmbBancos.Name = "cmbBancos"
        cmbBancos.Size = New Size(170, 23)
        cmbBancos.TabIndex = 130
        ' 
        ' Label9
        ' 
        Label9.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label9.AutoSize = True
        Label9.Location = New Point(273, 294)
        Label9.Name = "Label9"
        Label9.Size = New Size(40, 15)
        Label9.TabIndex = 129
        Label9.Text = "Banco"
        ' 
        ' txtCtaCli
        ' 
        txtCtaCli.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        txtCtaCli.Enabled = False
        txtCtaCli.Location = New Point(449, 312)
        txtCtaCli.Name = "txtCtaCli"
        txtCtaCli.Size = New Size(475, 23)
        txtCtaCli.TabIndex = 131
        ' 
        ' dgvOrden
        ' 
        dgvOrden.AllowUserToAddRows = False
        dgvOrden.AllowUserToDeleteRows = False
        dgvOrden.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvOrden.Location = New Point(12, 352)
        dgvOrden.Name = "dgvOrden"
        dgvOrden.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvOrden.Size = New Size(987, 172)
        dgvOrden.TabIndex = 132
        ' 
        ' txtImputaConta
        ' 
        txtImputaConta.Location = New Point(605, 5)
        txtImputaConta.Name = "txtImputaConta"
        txtImputaConta.Size = New Size(100, 23)
        txtImputaConta.TabIndex = 133
        txtImputaConta.TextAlign = HorizontalAlignment.Center
        ' 
        ' dtpFecha
        ' 
        dtpFecha.Format = DateTimePickerFormat.Short
        dtpFecha.Location = New Point(711, 5)
        dtpFecha.Name = "dtpFecha"
        dtpFecha.Size = New Size(99, 23)
        dtpFecha.TabIndex = 134
        ' 
        ' txtNroOrden
        ' 
        txtNroOrden.Location = New Point(816, 5)
        txtNroOrden.Name = "txtNroOrden"
        txtNroOrden.ReadOnly = True
        txtNroOrden.Size = New Size(100, 23)
        txtNroOrden.TabIndex = 135
        ' 
        ' lblConfecciona
        ' 
        lblConfecciona.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        lblConfecciona.AutoSize = True
        lblConfecciona.Location = New Point(10, 547)
        lblConfecciona.Name = "lblConfecciona"
        lblConfecciona.Size = New Size(74, 15)
        lblConfecciona.TabIndex = 136
        lblConfecciona.Text = "Confecciona"
        ' 
        ' txtConfecciona
        ' 
        txtConfecciona.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        txtConfecciona.Enabled = False
        txtConfecciona.Location = New Point(90, 543)
        txtConfecciona.Name = "txtConfecciona"
        txtConfecciona.ReadOnly = True
        txtConfecciona.Size = New Size(82, 23)
        txtConfecciona.TabIndex = 137
        ' 
        ' txtTotal
        ' 
        txtTotal.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        txtTotal.Enabled = False
        txtTotal.Location = New Point(178, 543)
        txtTotal.Name = "txtTotal"
        txtTotal.ReadOnly = True
        txtTotal.Size = New Size(105, 23)
        txtTotal.TabIndex = 138
        ' 
        ' btnBorrar
        ' 
        btnBorrar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnBorrar.Cursor = Cursors.Hand
        btnBorrar.FlatStyle = FlatStyle.Flat
        btnBorrar.Location = New Point(472, 539)
        btnBorrar.Name = "btnBorrar"
        btnBorrar.Size = New Size(69, 30)
        btnBorrar.TabIndex = 139
        btnBorrar.Text = "Borrar"
        ' 
        ' btnImportarValores
        ' 
        btnImportarValores.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnImportarValores.Cursor = Cursors.Hand
        btnImportarValores.FlatStyle = FlatStyle.Flat
        btnImportarValores.Location = New Point(579, 539)
        btnImportarValores.Name = "btnImportarValores"
        btnImportarValores.Size = New Size(133, 30)
        btnImportarValores.TabIndex = 140
        btnImportarValores.Text = "Importar Valores"
        ' 
        ' btnImprimir
        ' 
        btnImprimir.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnImprimir.Cursor = Cursors.Hand
        btnImprimir.FlatStyle = FlatStyle.Flat
        btnImprimir.Location = New Point(746, 539)
        btnImprimir.Name = "btnImprimir"
        btnImprimir.Size = New Size(106, 30)
        btnImprimir.TabIndex = 141
        btnImprimir.Text = "Imprimir"
        ' 
        ' btnSalir
        ' 
        btnSalir.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnSalir.BackColor = Color.IndianRed
        btnSalir.Cursor = Cursors.Hand
        btnSalir.FlatStyle = FlatStyle.Flat
        btnSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnSalir.ForeColor = Color.White
        btnSalir.Location = New Point(915, 539)
        btnSalir.Name = "btnSalir"
        btnSalir.Size = New Size(84, 30)
        btnSalir.TabIndex = 142
        btnSalir.Text = "Salir"
        btnSalir.UseVisualStyleBackColor = False
        ' 
        ' lblRubro
        ' 
        lblRubro.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        lblRubro.AutoSize = True
        lblRubro.Location = New Point(667, 250)
        lblRubro.Name = "lblRubro"
        lblRubro.Size = New Size(0, 15)
        lblRubro.TabIndex = 143
        ' 
        ' lblExterior
        ' 
        lblExterior.AutoSize = True
        lblExterior.Location = New Point(922, 9)
        lblExterior.Name = "lblExterior"
        lblExterior.Size = New Size(0, 15)
        lblExterior.TabIndex = 144
        ' 
        ' cmbPagado
        ' 
        cmbPagado.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        cmbPagado.DropDownStyle = ComboBoxStyle.DropDownList
        cmbPagado.FormattingEnabled = True
        cmbPagado.Location = New Point(469, 4)
        cmbPagado.Name = "cmbPagado"
        cmbPagado.Size = New Size(79, 23)
        cmbPagado.TabIndex = 145
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Location = New Point(434, 9)
        Label10.Name = "Label10"
        Label10.Size = New Size(29, 15)
        Label10.TabIndex = 146
        Label10.Text = "Ver: "
        ' 
        ' frmOrdenPago
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1011, 584)
        Controls.Add(Label10)
        Controls.Add(cmbPagado)
        Controls.Add(lblExterior)
        Controls.Add(lblRubro)
        Controls.Add(btnSalir)
        Controls.Add(btnImprimir)
        Controls.Add(btnImportarValores)
        Controls.Add(btnBorrar)
        Controls.Add(txtTotal)
        Controls.Add(txtConfecciona)
        Controls.Add(lblConfecciona)
        Controls.Add(txtNroOrden)
        Controls.Add(dtpFecha)
        Controls.Add(txtImputaConta)
        Controls.Add(dgvOrden)
        Controls.Add(txtCtaCli)
        Controls.Add(cmbBancos)
        Controls.Add(Label9)
        Controls.Add(Label8)
        Controls.Add(txtTalon)
        Controls.Add(dtpVto)
        Controls.Add(Label7)
        Controls.Add(btnGrabar)
        Controls.Add(cmbRubro)
        Controls.Add(cmbCuenta)
        Controls.Add(Label6)
        Controls.Add(Label5)
        Controls.Add(txtDolar)
        Controls.Add(Label4)
        Controls.Add(txtImporte)
        Controls.Add(Label3)
        Controls.Add(txtInterno)
        Controls.Add(cmbFormaPago)
        Controls.Add(Label2)
        Controls.Add(cmbFactura)
        Controls.Add(Label1)
        Controls.Add(dgvComprobantes)
        Controls.Add(btnBuscarProveedor)
        Controls.Add(cmbProveedor)
        Controls.Add(lblProveedor)
        MinimizeBox = False
        MinimumSize = New Size(950, 600)
        Name = "frmOrdenPago"
        Text = "Emisión Ordenes de Pago"
        CType(dgvComprobantes, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvOrden, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btnBuscarProveedor As Button
    Friend WithEvents cmbProveedor As ComboBox
    Friend WithEvents lblProveedor As Label
    Friend WithEvents dgvComprobantes As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbFactura As ComboBox
    Friend WithEvents cmbFormaPago As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtInterno As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txtImporte As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtDolar As TextBox
    Friend WithEvents cmbCuenta As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents cmbRubro As ComboBox
    Friend WithEvents btnGrabar As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents dtpVto As DateTimePicker
    Friend WithEvents Label8 As Label
    Friend WithEvents txtTalon As TextBox
    Friend WithEvents cmbBancos As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtCtaCli As TextBox
    Friend WithEvents dgvOrden As DataGridView
    Friend WithEvents txtImputaConta As TextBox
    Friend WithEvents dtpFecha As DateTimePicker
    Friend WithEvents txtNroOrden As TextBox
    Friend WithEvents lblConfecciona As Label
    Friend WithEvents txtConfecciona As TextBox
    Friend WithEvents txtTotal As TextBox
    Friend WithEvents btnBorrar As Button
    Friend WithEvents btnImportarValores As Button
    Friend WithEvents btnImprimir As Button
    Friend WithEvents btnSalir As Button
    Friend WithEvents lblRubro As Label
    Friend WithEvents lblExterior As Label
    Friend WithEvents cmbPagado As ComboBox
    Friend WithEvents Label10 As Label
End Class

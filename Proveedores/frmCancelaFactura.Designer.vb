<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCancelaFactura
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer
    Friend WithEvents grpBusqueda As System.Windows.Forms.GroupBox
    Friend WithEvents lblNroFactura As System.Windows.Forms.Label
    Friend WithEvents txtNroFactura As System.Windows.Forms.TextBox
    Friend WithEvents lblCuenta As System.Windows.Forms.Label
    Friend WithEvents txtNroCuenta As System.Windows.Forms.TextBox
    Friend WithEvents lblSucursal As System.Windows.Forms.Label
    Friend WithEvents cmbSucursal As System.Windows.Forms.ComboBox
    Friend WithEvents btnBuscar As System.Windows.Forms.Button
    Friend WithEvents lblImporte As System.Windows.Forms.Label
    Friend WithEvents txtImporte As System.Windows.Forms.TextBox
    Friend WithEvents lblACuenta As System.Windows.Forms.Label
    Friend WithEvents txtACuenta As System.Windows.Forms.TextBox
    Friend WithEvents lblFecha As System.Windows.Forms.Label
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkCobrado As System.Windows.Forms.CheckBox
    Friend WithEvents lblOPago As System.Windows.Forms.Label
    Friend WithEvents txtOPago As System.Windows.Forms.TextBox
    Friend WithEvents btnActualizar As System.Windows.Forms.Button
    Friend WithEvents btnSalir As System.Windows.Forms.Button

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCancelaFactura))
        grpBusqueda = New GroupBox()
        lblNroFactura = New Label()
        txtNroFactura = New TextBox()
        lblCuenta = New Label()
        txtNroCuenta = New TextBox()
        lblSucursal = New Label()
        cmbSucursal = New ComboBox()
        btnBuscar = New Button()
        lblImporte = New Label()
        txtImporte = New TextBox()
        lblACuenta = New Label()
        txtACuenta = New TextBox()
        lblFecha = New Label()
        dtpFecha = New DateTimePicker()
        chkCobrado = New CheckBox()
        lblOPago = New Label()
        txtOPago = New TextBox()
        btnActualizar = New Button()
        btnSalir = New Button()
        grpBusqueda.SuspendLayout()
        SuspendLayout()
        ' 
        ' grpBusqueda
        ' 
        grpBusqueda.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        grpBusqueda.Controls.Add(lblNroFactura)
        grpBusqueda.Controls.Add(txtNroFactura)
        grpBusqueda.Controls.Add(lblCuenta)
        grpBusqueda.Controls.Add(txtNroCuenta)
        grpBusqueda.Controls.Add(lblSucursal)
        grpBusqueda.Controls.Add(cmbSucursal)
        grpBusqueda.Location = New Point(12, 12)
        grpBusqueda.Name = "grpBusqueda"
        grpBusqueda.Size = New Size(564, 73)
        grpBusqueda.TabIndex = 0
        grpBusqueda.TabStop = False
        grpBusqueda.Text = "Búsqueda"
        ' 
        ' lblNroFactura
        ' 
        lblNroFactura.AutoSize = True
        lblNroFactura.Location = New Point(12, 22)
        lblNroFactura.Name = "lblNroFactura"
        lblNroFactura.Size = New Size(72, 15)
        lblNroFactura.TabIndex = 0
        lblNroFactura.Text = "Nro. Factura"
        ' 
        ' txtNroFactura
        ' 
        txtNroFactura.Location = New Point(12, 40)
        txtNroFactura.Name = "txtNroFactura"
        txtNroFactura.Size = New Size(120, 23)
        txtNroFactura.TabIndex = 1
        ' 
        ' lblCuenta
        ' 
        lblCuenta.AutoSize = True
        lblCuenta.Location = New Point(150, 22)
        lblCuenta.Name = "lblCuenta"
        lblCuenta.Size = New Size(45, 15)
        lblCuenta.TabIndex = 2
        lblCuenta.Text = "Cuenta"
        ' 
        ' txtNroCuenta
        ' 
        txtNroCuenta.Location = New Point(150, 40)
        txtNroCuenta.Name = "txtNroCuenta"
        txtNroCuenta.Size = New Size(120, 23)
        txtNroCuenta.TabIndex = 3
        ' 
        ' lblSucursal
        ' 
        lblSucursal.AutoSize = True
        lblSucursal.Location = New Point(288, 22)
        lblSucursal.Name = "lblSucursal"
        lblSucursal.Size = New Size(51, 15)
        lblSucursal.TabIndex = 4
        lblSucursal.Text = "Sucursal"
        ' 
        ' cmbSucursal
        ' 
        cmbSucursal.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSucursal.Location = New Point(288, 40)
        cmbSucursal.Name = "cmbSucursal"
        cmbSucursal.Size = New Size(266, 23)
        cmbSucursal.TabIndex = 5
        ' 
        ' btnBuscar
        ' 
        btnBuscar.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnBuscar.FlatStyle = FlatStyle.Flat
        btnBuscar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnBuscar.Location = New Point(582, 19)
        btnBuscar.Name = "btnBuscar"
        btnBuscar.Size = New Size(85, 33)
        btnBuscar.TabIndex = 6
        btnBuscar.Text = "Buscar"
        btnBuscar.UseVisualStyleBackColor = True
        ' 
        ' lblImporte
        ' 
        lblImporte.AutoSize = True
        lblImporte.Location = New Point(10, 89)
        lblImporte.Name = "lblImporte"
        lblImporte.Size = New Size(49, 15)
        lblImporte.TabIndex = 7
        lblImporte.Text = "Importe"
        ' 
        ' txtImporte
        ' 
        txtImporte.Location = New Point(10, 107)
        txtImporte.Name = "txtImporte"
        txtImporte.Size = New Size(140, 23)
        txtImporte.TabIndex = 8
        ' 
        ' lblACuenta
        ' 
        lblACuenta.AutoSize = True
        lblACuenta.Location = New Point(162, 89)
        lblACuenta.Name = "lblACuenta"
        lblACuenta.Size = New Size(56, 15)
        lblACuenta.TabIndex = 9
        lblACuenta.Text = "A Cuenta"
        ' 
        ' txtACuenta
        ' 
        txtACuenta.Location = New Point(162, 107)
        txtACuenta.Name = "txtACuenta"
        txtACuenta.Size = New Size(160, 23)
        txtACuenta.TabIndex = 10
        ' 
        ' lblFecha
        ' 
        lblFecha.AutoSize = True
        lblFecha.Location = New Point(342, 89)
        lblFecha.Name = "lblFecha"
        lblFecha.Size = New Size(38, 15)
        lblFecha.TabIndex = 11
        lblFecha.Text = "Fecha"
        ' 
        ' dtpFecha
        ' 
        dtpFecha.Format = DateTimePickerFormat.Short
        dtpFecha.Location = New Point(342, 107)
        dtpFecha.Name = "dtpFecha"
        dtpFecha.Size = New Size(140, 23)
        dtpFecha.TabIndex = 12
        ' 
        ' chkCobrado
        ' 
        chkCobrado.AutoSize = True
        chkCobrado.Location = New Point(10, 148)
        chkCobrado.Name = "chkCobrado"
        chkCobrado.Size = New Size(72, 19)
        chkCobrado.TabIndex = 13
        chkCobrado.Text = "Cobrado"
        chkCobrado.UseVisualStyleBackColor = True
        ' 
        ' lblOPago
        ' 
        lblOPago.AutoSize = True
        lblOPago.Location = New Point(162, 148)
        lblOPago.Name = "lblOPago"
        lblOPago.Size = New Size(89, 15)
        lblOPago.TabIndex = 14
        lblOPago.Text = "Orden de  Pago"
        ' 
        ' txtOPago
        ' 
        txtOPago.Location = New Point(162, 166)
        txtOPago.Name = "txtOPago"
        txtOPago.Size = New Size(140, 23)
        txtOPago.TabIndex = 15
        ' 
        ' btnActualizar
        ' 
        btnActualizar.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnActualizar.FlatStyle = FlatStyle.Flat
        btnActualizar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnActualizar.Location = New Point(582, 58)
        btnActualizar.Name = "btnActualizar"
        btnActualizar.Size = New Size(85, 33)
        btnActualizar.TabIndex = 16
        btnActualizar.Text = "Actualizar"
        btnActualizar.UseVisualStyleBackColor = True
        ' 
        ' btnSalir
        ' 
        btnSalir.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnSalir.BackColor = Color.IndianRed
        btnSalir.FlatStyle = FlatStyle.Flat
        btnSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnSalir.ForeColor = Color.White
        btnSalir.Location = New Point(582, 97)
        btnSalir.Name = "btnSalir"
        btnSalir.Size = New Size(85, 33)
        btnSalir.TabIndex = 17
        btnSalir.Text = "Salir"
        btnSalir.UseVisualStyleBackColor = False
        ' 
        ' frmCancelaFactura
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(679, 194)
        Controls.Add(btnSalir)
        Controls.Add(btnActualizar)
        Controls.Add(txtOPago)
        Controls.Add(lblOPago)
        Controls.Add(chkCobrado)
        Controls.Add(dtpFecha)
        Controls.Add(lblFecha)
        Controls.Add(txtACuenta)
        Controls.Add(lblACuenta)
        Controls.Add(txtImporte)
        Controls.Add(lblImporte)
        Controls.Add(btnBuscar)
        Controls.Add(grpBusqueda)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmCancelaFactura"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Cancelación de Facturas"
        grpBusqueda.ResumeLayout(False)
        grpBusqueda.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub
End Class

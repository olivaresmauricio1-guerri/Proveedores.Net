Imports System.Globalization
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmOrdenPago
    Private _suspenderAccionFiltros As Boolean = False
    Private Shared instancia As frmOrdenPago
    Dim suma As Double

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmOrdenPago()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmOrdenPago_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmOrdenPago_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _suspenderAccionFiltros = True
        Dim nroorden As Integer
        Dim senal As Integer

        NumericTextBehavior.Attach(txtTotal, 0D)
        NumericTextBehavior.Attach(txtDolar, 0D)

        FormHabilitarControles(False)

        If General.propio > 0 Then
            Dim sql As String = "Select * from OrdenPago WHERE IDPROPIO = " & propio & ";"
            Dim dt As DataTable = DSM.ExecuteQuery(DSM.Proveedores, sql)
            dgvOrden.DataSource = dt
        End If

        CargarComboBancos()
        CargarComboFormaPago()
        CargarComboProveedores()

        cmbCuenta.Items.Add("1.1.1")
        cmbCuenta.Items.Add("1.1.3")
        cmbCuenta.SelectedIndex = -1

        CargarCombos(cmbRubro, "Rubro", "Descripcion", "descripcion")
        cmbRubro.SelectedIndex = -1

        txtConfecciona.Text = General.UsuarioActual
        _suspenderAccionFiltros = False
    End Sub

    Private Sub cmbProveedor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProveedor.SelectedIndexChanged
        If _suspenderAccionFiltros Then Exit Sub

        Dim sqlProveedor As String = "Select * from MaeCtaCte WHERE NroCuenta = @NroCuenta"
        Dim parsProveedor = CmdParams("@NroCuenta", cmbProveedor.SelectedValue)
        Dim dtProveedor As DataTable = DSM.ExecuteQuery(DSM.Proveedores, sqlProveedor, parsProveedor)

        If dtProveedor.Rows.Count > 0 And Not String.IsNullOrEmpty(dtProveedor.Rows(0)("CodContable")) Then
            txtImputaConta.Text = dtProveedor.Rows(0)("CodContable").ToString()
        Else
            MessageBox.Show("Proveedor sin Codigo Contable", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If txtImputaConta.Text <> "2.1.1" Then
            txtImputaConta.BackColor = Color.IndianRed
            txtImputaConta.ForeColor = Color.White
        End If

        If Not String.IsNullOrEmpty(dtProveedor.Rows(0)("Rubro")) Then
            lblRubro.Text = dtProveedor.Rows(0)("Rubro").ToString()
        End If

        If dtProveedor.Rows(0)("Exterior") Then
            lblExterior.Text = "EXTERIOR !!"
            txtDolar.Visible = True
        Else
            lblExterior.Text = ""
            txtDolar.Visible = False
        End If

        Dim sqlComprobantes As String = "Select * from detactacte where nrocuenta = @NroCuenta and cobrado = 0 and (idimputacion = 1 or idimputacion = 2 or idimputacion = 10) order by fecha, nrocomprobante ;"
        Dim parsComprobantes = CmdParams("@NroCuenta", cmbProveedor.SelectedValue)
        Dim dtComprobantes As DataTable = DSM.ExecuteQuery(DSM.Proveedores, sqlComprobantes, parsComprobantes)
        dgvComprobantes.DataSource = dtComprobantes
        GridComprobantesConfigurarColumnas()

        If dtComprobantes.Rows.Count > 0 Then
            cmbProveedor.Enabled = False
            btnBuscarProveedor.Enabled = False
            FormHabilitarControles(True)
            dtpFecha.Value = Date.Now
            cmbFactura.Focus()
        End If
    End Sub

    Private Sub btnBuscarProveedor_Click(sender As Object, e As EventArgs) Handles btnBuscarProveedor.Click
        Using frm As New frmProveedoresSelector()
            If frm.ShowDialog(Me) = DialogResult.OK Then
                Dim proveedor = frm.Seleccion
                cmbProveedor.SelectedIndex = If(proveedor IsNot Nothing, cmbProveedor.FindStringExact(proveedor.Item("Nombre").ToString()), -1)
            End If
        End Using
    End Sub

    ' -------------------------------------------------------------------------------------------------------------------------------

    Private Sub CargarComboProveedores()
        Dim sql As String = "Select * from MaeCtaCte Order By Nombre"
        Dim proveedores As DataTable = DSM.ExecuteQuery(DSM.Proveedores, sql)

        cmbProveedor.DataSource = proveedores
        cmbProveedor.DisplayMember = "Nombre"
        cmbProveedor.ValueMember = "NroCuenta"
        cmbProveedor.SelectedIndex = -1
    End Sub

    Private Sub CargarComboBancos()
        Dim sql As String = "Select DISTINCT Banco from [MaestroBancos] order by banco"
        Dim bancos As DataTable = DSM.ExecuteQuery(DSM.Bancos, sql)

        cmbBancos.DataSource = bancos
        cmbBancos.DisplayMember = "Banco"
        cmbBancos.ValueMember = "Banco"
        cmbBancos.SelectedIndex = -1
    End Sub

    Private Sub CargarComboFormaPago()
        Dim sql As String = "Select * from [CondicionVenta]order by descripcion"
        Dim formaspago As DataTable = DSM.ExecuteQuery(DSM.Proveedores, sql)

        cmbFormaPago.DataSource = formaspago
        cmbFormaPago.DisplayMember = "Descripcion"
        cmbFormaPago.ValueMember = "Descripcion"
        cmbFormaPago.SelectedIndex = -1
    End Sub

    Private Sub GridComprobantesConfigurarColumnas()
        ConfigurarEstiloGrid(dgvComprobantes)

        dgvComprobantes.AllowUserToOrderColumns = False
        dgvComprobantes.ReadOnly = True

        For Each col As DataGridViewColumn In dgvComprobantes.Columns
            col.Visible = False
        Next

        With dgvComprobantes
            .Columns("NroCuenta").Visible = True
            .Columns("NroCuenta").HeaderText = "Nro. Cta."
            .Columns("NroCuenta").Width = 75
            .Columns("NroCuenta").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("NroFactura").Visible = True
            .Columns("NroFactura").HeaderText = "Nro. Fact."
            .Columns("NroFactura").Width = 75
            .Columns("NroFactura").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("NombreComprobante").Visible = True
            .Columns("NombreComprobante").HeaderText = "Nombre Comprobante"
            .Columns("NombreComprobante").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Columns("Fecha").Visible = True
            .Columns("Fecha").HeaderText = "Fecha"
            .Columns("Fecha").Width = 80
            .Columns("Fecha").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Fecha").DefaultCellStyle.Format = "dd/MM/yyyy"
            .Columns("Monto").Visible = True
            .Columns("Monto").HeaderText = "Monto"
            .Columns("Monto").Width = 100
            .Columns("Monto").DefaultCellStyle.Format = "N2"
            .Columns("Monto").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("ACuenta").Visible = True
            .Columns("ACuenta").HeaderText = "A Cuenta"
            .Columns("ACuenta").Width = 100
            .Columns("ACuenta").DefaultCellStyle.Format = "N2"
            .Columns("ACuenta").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Sucursal").Visible = True
            .Columns("Sucursal").HeaderText = "Sucursal"
            .Columns("Sucursal").Width = 130
            .Columns("Cobrado").Visible = True
            .Columns("Cobrado").HeaderText = "Pagado"
            .Columns("Cobrado").Width = 50
            .Columns("Cobrado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Anterior").Visible = True
            .Columns("Anterior").HeaderText = "Anterior"
            .Columns("Anterior").Width = 50
            .Columns("Anterior").DefaultCellStyle.Format = "N2"
        End With
    End Sub

    Private Sub FormHabilitarControles(enabled)
        txtImputaConta.Enabled = enabled
        dtpFecha.Enabled = enabled
        cmbFactura.Enabled = enabled
        cmbFormaPago.Enabled = enabled
        txtInterno.Enabled = enabled
        txtImporte.Enabled = enabled
        txtDolar.Enabled = enabled
        cmbCuenta.Enabled = enabled
        cmbRubro.Enabled = enabled
        dtpVto.Enabled = enabled
        txtTalon.Enabled = enabled
        cmbBancos.Enabled = enabled
        txtCtaCli.Enabled = enabled
        btnGrabar.Enabled = enabled
        btnBorrar.Enabled = enabled
        btnImportarValores.Enabled = enabled
        btnImprimir.Enabled = enabled
    End Sub
End Class
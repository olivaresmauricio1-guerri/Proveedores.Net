Imports System.Globalization
Imports DSM = DataSourceManager.Lib.DataSourceManager

Partial Public Class frmNoveProveedores
    Private _suspenderAccionFiltros As Boolean = False
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1
    Private Shared instancia As frmNoveProveedores

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmNoveProveedores()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmNoveBancos_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Public Sub frmNoveBancos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _suspenderAccionFiltros = True

        CargarCombosDatos()
        ' GridConfigurarColumnas()
        dtpFechaDesde.Value = New Date(Date.Today.Year, 1, 1)
        dtpFechaHasta.Value = New Date(Date.Today.Year, 12, 31)

        NumericTextBehavior.Attach(txtDolar, 0D)
        NumericTextBehavior.Attach(txtComprasRNI, 0D)
        NumericTextBehavior.Attach(txtNGrav105, 0D)
        NumericTextBehavior.Attach(txtNGrav21, 0D)
        NumericTextBehavior.Attach(txtNGrav27, 0D)
        NumericTextBehavior.Attach(txtExentos, 0D)
        NumericTextBehavior.Attach(txtIVA, 0D)
        NumericTextBehavior.Attach(txtGanancia, 0D)
        NumericTextBehavior.Attach(txtRetPerIVA, 0D)
        NumericTextBehavior.Attach(txtIngresosBrutos1, 0D)
        NumericTextBehavior.Attach(txtIngresosBrutos2, 0D)
        NumericTextBehavior.Attach(txtIngresosBrutos3, 0D)
        NumericTextBehavior.Attach(txtIngresosBrutos4, 0D)
        NumericTextBehavior.Attach(txtMonto1, 0D)
        NumericTextBehavior.Attach(txtMonto2, 0D)
        NumericTextBehavior.Attach(txtMonto3, 0D)


        FormModoConsulta()
        FormLimpiarSeleccionado()
        DgvListado.ReadOnly = True
        GridBuscar()

        _suspenderAccionFiltros = False
        Me.KeyPreview = True
    End Sub

    Private Sub TxtBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtBuscar.TextChanged
        If _suspenderAccionFiltros Then Exit Sub
        _suspenderAccionFiltros = True
        FormModoConsulta()
        FormLimpiarSeleccionado()
        GridBuscar()
        _suspenderAccionFiltros = False
    End Sub

    Private Sub dtpFechaDesde_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaDesde.ValueChanged
        If _suspenderAccionFiltros Then Exit Sub
        _suspenderAccionFiltros = True
        FormModoConsulta()
        FormLimpiarSeleccionado()
        GridBuscar()
        _suspenderAccionFiltros = False
    End Sub

    Private Sub dtpFechaHasta_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaHasta.ValueChanged
        If _suspenderAccionFiltros Then Exit Sub
        _suspenderAccionFiltros = True
        FormModoConsulta()
        FormLimpiarSeleccionado()
        GridBuscar()
        _suspenderAccionFiltros = False
    End Sub

    Private Sub DgvListado_KeyDown(sender As Object, e As KeyEventArgs) Handles DgvListado.KeyDown
        If _suspenderAccionFiltros Then Exit Sub
        _suspenderAccionFiltros = True

        If e.Control AndAlso e.KeyCode = Keys.C Then
            CopiarDataGrid(DgvListado, chkEncabezados.Checked)
            e.Handled = True
        End If

        _suspenderAccionFiltros = False
    End Sub

    Private Sub lnkCopiar_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        CopiarDataGrid(DgvListado, chkEncabezados.Checked)
    End Sub

    Private Sub DgvListado_SelectionChanged(sender As Object, e As EventArgs) Handles DgvListado.SelectionChanged
        If _suspenderAccionFiltros Then Exit Sub

        If DgvListado.SelectedRows.Count > 0 Then
            AplicarSeleccionActual()
        End If
    End Sub

    Private Sub btnConsultarCuenta_Click(sender As Object, e As EventArgs) Handles btnConsultarCuenta.Click
        btnConsultarCuenta.Visible = False
        boxPlanCuentas.Visible = True
        txtBuscarCuenta.Focus()
        GridBuscarCuenta()
    End Sub

    Private Sub txtBuscarCuenta_TextChanged(sender As Object, e As EventArgs) Handles txtBuscarCuenta.TextChanged
        If _suspenderAccionFiltros Then Exit Sub
        _suspenderAccionFiltros = True
        GridBuscarCuenta()
        _suspenderAccionFiltros = False
    End Sub

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        _suspenderAccionFiltros = True
        DgvListado.ClearSelection()
        filaActual = Nothing
        filaActualIndice = -1
        FormModoEdicion()
        FormLimpiarSeleccionado()
        _suspenderAccionFiltros = False
    End Sub

    Private Sub CmdModificar_Click(sender As Object, e As EventArgs) Handles CmdModificar.Click
        _suspenderAccionFiltros = True
        If filaActual Is Nothing Then Return
        FormModoEdicion()
        _suspenderAccionFiltros = False
    End Sub

    Private Sub CmdBorrar_Click(sender As Object, e As EventArgs) Handles CmdBorrar.Click
        If filaActual Is Nothing Then Return
        If MessageBox.Show("¿Está seguro de que desea eliminar esta novedad?", "Confirmar borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            FormModoConsulta()
        End If
    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        _suspenderAccionFiltros = True
        FormModoConsulta()
        FormLimpiarSeleccionado()
        GridBuscar()
        _suspenderAccionFiltros = False
    End Sub

    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Close()
    End Sub

    Private Sub cmdAceptar_Click(sender As Object, e As EventArgs) Handles cmdAceptar.Click
        _suspenderAccionFiltros = True


        _suspenderAccionFiltros = False
    End Sub

    Private Sub SeleccionarUltimaFila()
        GridBuscar()
        If DgvListado.Rows.Count > 0 Then
            Dim ultimaFila As DataGridViewRow = DgvListado.Rows(DgvListado.Rows.Count - 1)
            DgvListado.ClearSelection()
            ultimaFila.Selected = True
            filaActual = ultimaFila
            filaActualIndice = ultimaFila.Index
            FormObtenerSeleccionado()
            ' scroll para que se vea la fila seleccionada
            DgvListado.FirstDisplayedScrollingRowIndex = ultimaFila.Index
        End If
    End Sub

    Private Sub SeleccionarFilaActual(Optional id = Nothing)
        GridBuscar()

        '' seleccionar la fila correspondiente al IdNoveBancos
        'If IdNoveBancos IsNot Nothing Then
        '    For Each row As DataGridViewRow In DgvListado.Rows
        '        If Convert.ToInt32(row.Cells("IdNoveBancos").Value) = IdNoveBancos Then
        '            DgvListado.ClearSelection()
        '            row.Selected = True
        '            filaActual = row
        '            filaActualIndice = row.Index
        '            FormObtenerSeleccionado()
        '            Exit For
        '        End If
        '    Next
        'End If

        '' scroll para que se vea la fila seleccionada
        'If filaActual IsNot Nothing Then
        '    DgvListado.FirstDisplayedScrollingRowIndex = filaActual.Index
        'End If
    End Sub

    Private Sub GridBuscar()
        DgvListado.DataSource = Nothing
        Dim texto As String = TxtBuscar.Text.Trim()
        Dim whereTexto As String = "1=1"
        If texto <> String.Empty Then
            whereTexto = "(NroCuenta LIKE @Texto OR NroFactura LIKE @Texto OR NroComprobante LIKE @Texto)"
        End If
        Dim sql = $"
            SELECT * FROM NoveCtaCte as prov
            WHERE {whereTexto}
                AND (Fecha >= @FechaDesde AND Fecha <= @FechaHasta)"
        Dim parametros = CmdParams(
            "@Texto", "%" & texto & "%",
            "@FechaDesde", dtpFechaDesde.Value.Date,
            "@FechaHasta", dtpFechaHasta.Value.Date
        )
        Dim dt = DSM.ExecuteQuery(DSM.Proveedores, sql, parametros)
        DgvListado.DataSource = dt

        If dt.Rows.Count = 0 Then
            filaActualIndice = -1
            filaActual = Nothing
            FormLimpiarSeleccionado()
            Return
        End If

        GridConfigurarColumnas()
    End Sub

    Public Sub GridConfigurarColumnas()
        Dim grid = DgvListado

        For Each col As DataGridViewColumn In grid.Columns
            col.Visible = False
        Next

        grid.Columns("NroCuenta").Visible = True
        grid.Columns("NroCuenta").HeaderText = "Nro.Cta."
        grid.Columns("NroCuenta").Width = 80
        grid.Columns("NroCuenta").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grid.Columns("NroCuenta").DisplayIndex = 0

        grid.Columns("NroFactura").Visible = True
        grid.Columns("NroFactura").HeaderText = "Nro.Fact."
        grid.Columns("NroFactura").Width = 80
        grid.Columns("NroFactura").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grid.Columns("NroFactura").DisplayIndex = 1

        grid.Columns("Monto").Visible = True
        grid.Columns("Monto").HeaderText = "Monto"
        grid.Columns("Monto").Width = 100
        grid.Columns("Monto").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grid.Columns("Monto").DefaultCellStyle.Format = "N2"
        grid.Columns("Monto").DisplayIndex = 2

        grid.Columns("NroComprobante").Visible = True
        grid.Columns("NroComprobante").HeaderText = "Nro.Comp."
        grid.Columns("NroComprobante").Width = 80
        grid.Columns("NroComprobante").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grid.Columns("NroComprobante").DisplayIndex = 3

        grid.Columns("NombreComprobante").Visible = True
        grid.Columns("NombreComprobante").HeaderText = "Tipo Comp."
        grid.Columns("NombreComprobante").Width = 120
        grid.Columns("NombreComprobante").DisplayIndex = 4

        grid.Columns("Condicion").Visible = True
        grid.Columns("Condicion").HeaderText = "Condición"
        grid.Columns("Condicion").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        grid.Columns("Condicion").DisplayIndex = 5

        grid.Columns("Fecha").Visible = True
        grid.Columns("Fecha").HeaderText = "Fecha"
        grid.Columns("Fecha").Width = 80
        grid.Columns("Fecha").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grid.Columns("Fecha").DefaultCellStyle.Format = "dd/MM/yyyy"
        grid.Columns("Fecha").DisplayIndex = 6

        grid.Columns("IdImputacion").Visible = True
        grid.Columns("IdImputacion").HeaderText = "Imp"
        grid.Columns("IdImputacion").Width = 50
        grid.Columns("IdImputacion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grid.Columns("IdImputacion").DisplayIndex = 7

        grid.Columns("CtaMonto").Visible = True
        grid.Columns("CtaMonto").HeaderText = "Cta.Monto"
        grid.Columns("CtaMonto").Width = 80
        grid.Columns("CtaMonto").DisplayIndex = 8

        ConfigurarEstiloGrid(grid)
    End Sub

    Private Sub FormLimpiarSeleccionado()
        cmbSucursal.SelectedIndex = -1
        cmbProveedor.SelectedIndex = -1
        txtNroCuenta.Text = String.Empty
        txtPuntoVenta.Text = String.Empty
        txtNroFactura.Text = String.Empty
        txtFondoFijo.Text = String.Empty
        txtNroComprobante.Text = String.Empty
        txtDespacho.Text = String.Empty
        dtpFecha.Value = Date.Today
        cmbComprobante.SelectedIndex = -1
        chkDolar.Checked = False
        NumericTextBehavior.SetValue(txtDolar, 0D)

        NumericTextBehavior.SetValue(txtComprasRNI, 0D)
        txtCuentaComprasRNI.Text = String.Empty
        NumericTextBehavior.SetValue(txtNGrav105, 0D)
        txtCuentaNGrav105.Text = String.Empty
        NumericTextBehavior.SetValue(txtNGrav21, 0D)
        txtCuentaNGrav21.Text = String.Empty
        NumericTextBehavior.SetValue(txtNGrav27, 0D)
        txtCuentaNGrav27.Text = String.Empty
        NumericTextBehavior.SetValue(txtExentos, 0D)
        txtCuentaExentos.Text = String.Empty
        NumericTextBehavior.SetValue(txtIVA, 0D)
        txtCuentaIVA.Text = String.Empty
        NumericTextBehavior.SetValue(txtGanancia, 0D)
        txtCuentaGanancia.Text = String.Empty
        NumericTextBehavior.SetValue(txtRetPerIVA, 0D)
        txtCuentaRetPerIVA.Text = String.Empty
        NumericTextBehavior.SetValue(txtIngresosBrutos1, 0D)
        txtCuentaIngresosBrutos1.Text = String.Empty
        NumericTextBehavior.SetValue(txtIngresosBrutos2, 0D)
        txtCuentaIngresosBrutos2.Text = String.Empty
        NumericTextBehavior.SetValue(txtIngresosBrutos3, 0D)
        txtCuentaIngresosBrutos3.Text = String.Empty
        NumericTextBehavior.SetValue(txtIngresosBrutos4, 0D)
        txtCuentaIngresosBrutos4.Text = String.Empty


        NumericTextBehavior.SetValue(txtMonto1, 0D)
        cmbCuentaMonto1.SelectedIndex = -1
        NumericTextBehavior.SetValue(txtMonto2, 0D)
        cmbCuentaMonto2.SelectedIndex = -1
        NumericTextBehavior.SetValue(txtMonto3, 0D)
        cmbCuentaMonto3.SelectedIndex = -1

        chkNoGeneraAsiento.Checked = False

        dgvListadoCuenta.DataSource = Nothing
        txtComentario.Text = String.Empty
    End Sub

    Private Sub FormObtenerSeleccionado()
        If filaActual Is Nothing Then Return

        cmbSucursal.SelectedIndex = cmbSucursal.FindStringExact(filaActual.Cells("Sucursal").Value.ToString())
        cmbProveedor.SelectedValue = If(filaActual.Cells("IdCtaCte").Value IsNot DBNull.Value, Convert.ToInt32(filaActual.Cells("IdCtaCte").Value), -1)
        txtNroCuenta.Text = If(filaActual.Cells("NroCuenta").Value IsNot DBNull.Value, filaActual.Cells("NroCuenta").Value.ToString(), String.Empty)
        txtPuntoVenta.Text = If(filaActual.Cells("PuntodeVenta").Value IsNot DBNull.Value, filaActual.Cells("PuntodeVenta").Value.ToString(), String.Empty)
        txtNroFactura.Text = If(filaActual.Cells("NroFactura").Value IsNot DBNull.Value, filaActual.Cells("NroFactura").Value.ToString(), String.Empty)
        txtFondoFijo.Text = If(filaActual.Cells("FondoFijo").Value IsNot DBNull.Value, filaActual.Cells("FondoFijo").Value.ToString(), String.Empty)
        txtNroComprobante.Text = If(filaActual.Cells("NroComprobante").Value IsNot DBNull.Value, filaActual.Cells("NroComprobante").Value.ToString(), String.Empty)
        txtDespacho.Text = If(filaActual.Cells("NroDespacho").Value IsNot DBNull.Value, filaActual.Cells("NroDespacho").Value.ToString(), String.Empty)
        dtpFecha.Value = If(filaActual.Cells("Fecha").Value IsNot DBNull.Value, Convert.ToDateTime(filaActual.Cells("Fecha").Value), Date.Today)
        cmbComprobante.SelectedIndex = cmbComprobante.FindStringExact(filaActual.Cells("NombreComprobante").Value.ToString())
        txtCAI.Text = If(filaActual.Cells("CAI").Value IsNot DBNull.Value, filaActual.Cells("CAI").Value.ToString(), String.Empty)
        chkDolar.Checked = False
        NumericTextBehavior.SetValue(txtDolar, Convert.ToDouble(filaActual.Cells("Dolar").Value))

        'NumericTextBehavior.SetValue(txtComprasRNI, Convert.ToDouble(filaActual.Cells("ComprasRNI").Value))
        'txtCuentaComprasRNI.Text = If(filaActual.Cells("CuentaComprasRNI").Value IsNot DBNull.Value, filaActual.Cells("CtaRNI").Value.ToString(), String.Empty)
        'NumericTextBehavior.SetValue(txtNGrav105, Convert.ToDouble(filaActual.Cells("Monto").Value))
        'txtCuentaNGrav105.Text = If(filaActual.Cells("CuentaNGrav105").Value IsNot DBNull.Value, filaActual.Cells("CuentaNGrav105").Value.ToString(), String.Empty)
        'NumericTextBehavior.SetValue(txtNGrav21, Convert.ToDouble(filaActual.Cells("Monto").Value))
        'txtCuentaNGrav21.Text = If(filaActual.Cells("CuentaNGrav21").Value IsNot DBNull.Value, filaActual.Cells("CuentaNGrav21").Value.ToString(), String.Empty)
        'NumericTextBehavior.SetValue(txtNGrav27, Convert.ToDouble(filaActual.Cells("Monto").Value))
        'txtCuentaNGrav27.Text = If(filaActual.Cells("CuentaNGrav27").Value IsNot DBNull.Value, filaActual.Cells("CuentaNGrav27").Value.ToString(), String.Empty)
        'NumericTextBehavior.SetValue(txtExentos, Convert.ToDouble(filaActual.Cells("Monto").Value))
        'txtCuentaExentos.Text = If(filaActual.Cells("CuentaExentos").Value IsNot DBNull.Value, filaActual.Cells("CuentaExentos").Value.ToString(), String.Empty)
        'NumericTextBehavior.SetValue(txtIVA, Convert.ToDouble(filaActual.Cells("Monto").Value))
        'txtCuentaIVA.Text = If(filaActual.Cells("CuentaIVA").Value IsNot DBNull.Value, filaActual.Cells("CuentaIVA").Value.ToString(), String.Empty)
        'NumericTextBehavior.SetValue(txtGanancia, Convert.ToDouble(filaActual.Cells("Monto").Value))


    End Sub

    Private Sub AplicarSeleccionActual()
        If DgvListado Is Nothing OrElse DgvListado.CurrentRow Is Nothing Then Return
        If DgvListado.SelectedRows.Count > 1 Then
            filaActualIndice = -1
            filaActual = Nothing
            FormLimpiarSeleccionado()
            Return
        End If
        Dim idx = DgvListado.CurrentRow.Index
        If idx < 0 OrElse idx = filaActualIndice Then Return
        FormModoConsulta()

        filaActualIndice = idx
        filaActual = DgvListado.CurrentRow
        FormObtenerSeleccionado()
    End Sub


    Private Sub CargarCombosDatos()

        General.CargarCombos(cmbSucursal, "Sucursales", "Descripcion", "Descripcion", "Descripcion")
        General.CargarCombos(cmbProveedor, "MaeCtaCte", "Nombre", "Nombre", "IdCtaCte")
        General.CargarCombos(cmbComprobante, "NumerosComprobantes", "Descripcion", "Descripcion", "ImputaCC")

        ' cargar en cmbCuentaMonto1: 2.1.1, 1.2.9, 1.2.1, 2.2.9, 2.1.13, 2.2.13, 2.1.14, 2.1.15, 2.7.7
        cmbCuentaMonto1.Items.Clear()
        cmbCuentaMonto1.Items.Add("")
        cmbCuentaMonto1.Items.Add("2.1.1")
        cmbCuentaMonto1.Items.Add("1.2.9")
        cmbCuentaMonto1.Items.Add("1.2.1")
        cmbCuentaMonto1.Items.Add("2.2.9")
        cmbCuentaMonto1.Items.Add("2.1.13")
        cmbCuentaMonto1.Items.Add("2.2.13")
        cmbCuentaMonto1.Items.Add("2.1.14")
        cmbCuentaMonto1.Items.Add("2.1.15")
        cmbCuentaMonto1.Items.Add("2.7.7")

        ' cargar en cmbCuentaMonto2: 2.1.1, 1.2.9, 1.2.1, 2.2.9
        cmbCuentaMonto2.Items.Clear()
        cmbCuentaMonto2.Items.Add("")
        cmbCuentaMonto2.Items.Add("2.1.1")
        cmbCuentaMonto2.Items.Add("1.2.9")
        cmbCuentaMonto2.Items.Add("1.2.1")
        cmbCuentaMonto2.Items.Add("2.2.9")

        ' cargar en cmbCuentaMonto3: 2.1.1, 1.2.9, 1.2.1, 2.2.9
        cmbCuentaMonto3.Items.Clear()
        cmbCuentaMonto3.Items.Add("")
        cmbCuentaMonto3.Items.Add("2.1.1")
        cmbCuentaMonto3.Items.Add("1.2.9")
        cmbCuentaMonto3.Items.Add("1.2.1")
        cmbCuentaMonto3.Items.Add("2.2.9")
    End Sub

    Public Sub FormModoConsulta()
        SetControlesEnabled(True, CmdAgregar, CmdModificar, CmdBorrar)
        SetControlesEnabled(False, cmdAceptar, CmdCancelar)
    End Sub

    Public Sub FormModoEdicion()
        SetControlesEnabled(True, cmdAceptar, CmdCancelar)
        SetControlesEnabled(False, CmdAgregar, CmdModificar, CmdBorrar)
    End Sub

    Private Sub SeleccionarFila(numero As Integer)
        If DgvListado.Rows.Count > 0 AndAlso numero >= 0 AndAlso numero < DgvListado.Rows.Count Then
            DgvListado.Rows(numero).Selected = True
        End If
        AplicarSeleccionActual()
    End Sub

    Private Sub GridBuscarCuenta()
        dgvListadoCuenta.DataSource = Nothing
        Dim texto As String = txtBuscarCuenta.Text.Trim()
        Dim sql As String = "SELECT CodContable AS Cuenta, Descripcion FROM PlanCuentas WHERE CodContable LIKE @Texto OR Descripcion LIKE @Texto ORDER BY CodContable"
        Dim parametros = CmdParams("@Texto", "%" & texto & "%")
        Dim dt = DSM.ExecuteQuery(DSM.Contabilidad, sql, parametros)
        dgvListadoCuenta.DataSource = dt
        ConfigurarEstiloGrid(dgvListadoCuenta)
        dgvListadoCuenta.Columns("Cuenta").Width = 100
        dgvListadoCuenta.Columns("Descripcion").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
    End Sub
End Class
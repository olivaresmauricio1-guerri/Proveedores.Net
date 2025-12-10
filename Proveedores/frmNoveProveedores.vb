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

        NumericTextBehavior.Attach(txtFondoFijo, 0D)
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
        NumericTextBehavior.Attach(txtIngresosBrutos5, 0D)
        NumericTextBehavior.Attach(txtIngresosBrutos6, 0D)
        NumericTextBehavior.Attach(txtMonto1, 0D)
        NumericTextBehavior.Attach(txtMonto2, 0D)
        NumericTextBehavior.Attach(txtMonto3, 0D)


        FormModoConsulta()
        FormLimpiarSeleccionado()
        DgvListado.ReadOnly = True
        GridBuscar()
        FormObtenerSeleccionado()
        ConfigurarTxtCuentas()

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

    Private Sub lnkCopiar_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCopiar.LinkClicked
        CopiarDataGrid(DgvListado, chkEncabezados.Checked)
    End Sub

    Private Sub DgvListado_SelectionChanged(sender As Object, e As EventArgs) Handles DgvListado.SelectionChanged
        If _suspenderAccionFiltros Then Exit Sub

        'If DgvListado.SelectedRows.Count > 0 Then
        AplicarSeleccionActual()
        'End If
    End Sub

    Private Sub btnConsultarCuenta_Click(sender As Object, e As EventArgs) Handles btnConsultarCuenta.Click
        btnConsultarCuenta.Visible = False
        boxPlanCuentas.Visible = True
        txtBuscarCuenta.Focus()
        GridBuscarCuenta()
    End Sub

    ' por cada txtcuentaALGO, configurar un handler para buscar la cuenta al modificar o hacer foco
    ' por ejemplo al txtCuentaComprasRNI, al hacer foco y modificacion, llamara a buscarCuenta(txtCuentaComprasRNI(text)
    Private Sub ConfigurarTxtCuentas()
        Dim cuentasDebe() As TextBox = {
            txtCuentaComprasRNI,
            txtCuentaNGrav105,
            txtCuentaNGrav21,
            txtCuentaNGrav27,
            txtCuentaExentos,
            txtCuentaIVA,
            txtCuentaGanancia,
            txtCuentaRetPerIVA,
            txtCuentaIngresosBrutos1,
            txtCuentaIngresosBrutos2,
            txtCuentaIngresosBrutos3,
            txtCuentaIngresosBrutos4,
            txtCuentaIngresosBrutos5,
            txtCuentaIngresosBrutos6
        }

        For Each txt As TextBox In cuentasDebe
            AddHandler txt.Enter, AddressOf CuentaFocusOrChanged
            AddHandler txt.KeyUp, AddressOf CuentaFocusOrChanged
        Next

        Dim cuentasHaber() As ComboBox = {
            cmbCuentaMonto1,
            cmbCuentaMonto2,
            cmbCuentaMonto3
        }

        For Each cmb As ComboBox In cuentasHaber
            AddHandler cmb.Enter, AddressOf CuentaFocusOrChanged2
            AddHandler cmb.KeyUp, AddressOf CuentaFocusOrChanged2
            AddHandler cmb.SelectionChangeCommitted, AddressOf CuentaFocusOrChanged2
        Next
    End Sub

    Private Sub CuentaFocusOrChanged(sender As Object, e As EventArgs)
        Dim txt As TextBox = TryCast(sender, TextBox)
        If txt Is Nothing Then Exit Sub
        buscarCuenta(txt.Text)
    End Sub

    Private Sub CuentaFocusOrChanged2(sender As Object, e As EventArgs)
        Dim cmb As ComboBox = TryCast(sender, ComboBox)
        If cmb Is Nothing Then Exit Sub
        BeginInvoke(Sub() buscarCuenta(cmb.Text))
    End Sub

    Private Sub buscarCuenta(cuenta As String)
        btnConsultarCuenta.Visible = False
        boxPlanCuentas.Visible = True
        txtBuscarCuenta.Text = cuenta
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

        cmbSucursal.Text = "Casa Central"
        cmbComprobante.SelectedText = "Factura"
        txtCuentaIVA.Text = "1.3.7"
        txtCuentaGanancia.Text = "1.3.1"
        txtCuentaRetPerIVA.Text = "1.3.2"
        txtCuentaIngresosBrutos1.Text = "1.3.30"

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
            Dim sql = "DELETE FROM NoveCtaCte WHERE IdDetaCtaCte = @IdDetaCtaCte"
            Dim parametros = CmdParams("@IdDetaCtaCte", Convert.ToInt32(filaActual.Cells("IdDetaCtaCte").Value))
            DSM.Execute(DSM.Proveedores, sql, parametros)
            FormModoConsulta()
        End If
    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        _suspenderAccionFiltros = True
        FormModoConsulta()
        FormLimpiarSeleccionado()
        GridBuscar()
        FormObtenerSeleccionado()
        'If DgvListado.Rows.Count > 0 Then
        '    SeleccionarFila(0)
        'End If
        _suspenderAccionFiltros = False
    End Sub

    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Close()
    End Sub

    Private Sub cmdAceptar_Click(sender As Object, e As EventArgs) Handles cmdAceptar.Click
        _suspenderAccionFiltros = True

        Try
            ' 1) Validaciones básicas
            Dim nroFactura As Integer = Val(txtNroFactura.Text)

            If nroFactura = 0 Then
                MessageBox.Show("Nro. de Factura no puede ser cero...", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                _suspenderAccionFiltros = False
                Return
            End If

            If (cmbCuentaMonto1.Text = "1.1.40" Or cmbCuentaMonto1.Text = "1.1.41" Or cmbCuentaMonto1.Text = "1.1.44") AndAlso Val(txtFondoFijo.Text) = 0 Then
                MessageBox.Show("DEBE INGRESAR NRO. DE LOTE PARA LA CUENTA MONTO IMPUTADA. ¡GRACIAS!", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtFondoFijo.Focus()
                _suspenderAccionFiltros = False
                Return
            End If

            If cmbProveedor.SelectedValue Is Nothing OrElse Val(cmbProveedor.SelectedValue.ToString()) < 1 Then
                MessageBox.Show("Debe seleccionar un proveedor (la cuenta está en cero).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                _suspenderAccionFiltros = False
                Return
            End If

            ' Fecha válida
            Dim fecha As Date = dtpFecha.Value.Date

            If fecha.Year <> Date.Today.Year Then
                MessageBox.Show("El año difiere del corriente...", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            ' Validar contra cierre IVA
            Dim fechaCierreIva = ObtenerFechaCierreIva()
            If fechaCierreIva.HasValue AndAlso fecha < fechaCierreIva.Value.Date Then
                MessageBox.Show("Fecha inválida: libro de IVA de ese período cerrado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                _suspenderAccionFiltros = False
                Return
            End If

            ' 2) Traer datos de proveedor / cuenta corriente
            Dim idCtaCte As Integer = Convert.ToInt32(cmbProveedor.SelectedValue)
            Dim proveedor = ObtenerProveedor(idCtaCte)
            If proveedor Is Nothing Then
                MessageBox.Show("No se pudo obtener la cuenta del proveedor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                _suspenderAccionFiltros = False
                Return
            End If

            Dim nroCuenta As String = proveedor.Item("NroCuenta").ToString()
            txtNroCuenta.Text = nroCuenta
            txtCuit.Text = proveedor.Item("Cuit").ToString()

            ' 3) Obtener todos los importes como Decimal
            Dim comprasRNI As Decimal = NumericTextBehavior.GetValue(txtComprasRNI)
            Dim neto105 As Decimal = NumericTextBehavior.GetValue(txtNGrav105)
            Dim neto21 As Decimal = NumericTextBehavior.GetValue(txtNGrav21)
            Dim neto27 As Decimal = NumericTextBehavior.GetValue(txtNGrav27)
            Dim exentos As Decimal = NumericTextBehavior.GetValue(txtExentos)
            Dim iva As Decimal = NumericTextBehavior.GetValue(txtIVA)
            Dim ganancias As Decimal = NumericTextBehavior.GetValue(txtGanancia)
            Dim rpi As Decimal = NumericTextBehavior.GetValue(txtRetPerIVA)
            Dim ib1 As Decimal = NumericTextBehavior.GetValue(txtIngresosBrutos1)
            Dim ib2 As Decimal = NumericTextBehavior.GetValue(txtIngresosBrutos2)
            Dim ib3 As Decimal = NumericTextBehavior.GetValue(txtIngresosBrutos3)
            Dim ib4 As Decimal = NumericTextBehavior.GetValue(txtIngresosBrutos4)
            Dim ib5 As Decimal = NumericTextBehavior.GetValue(txtIngresosBrutos5)
            Dim ib6 As Decimal = NumericTextBehavior.GetValue(txtIngresosBrutos6)

            Dim monto1 As Decimal = NumericTextBehavior.GetValue(txtMonto1)
            Dim monto2 As Decimal = NumericTextBehavior.GetValue(txtMonto2)
            Dim monto3 As Decimal = NumericTextBehavior.GetValue(txtMonto3)

            Dim dolar As Decimal = NumericTextBehavior.GetValue(txtDolar)
            Dim fondoFijo As Decimal = NumericTextBehavior.GetValue(txtFondoFijo)

            ' 4) Consistencia de cuentas (similar al VB6, simplificado)
            '    Si hay monto >0 y cuenta vacía -> error
            '    Si hay cuenta cargada y monto =0 -> error
            '    Si hay ambos -> VerificarCuenta

            If Not ValidarImporteCuenta(comprasRNI, txtCuentaComprasRNI, "Compras RNI") Then GoTo Fin
            If Not ValidarImporteCuenta(neto105, txtCuentaNGrav105, "Neto Gravado 10,5") Then GoTo Fin
            If Not ValidarImporteCuenta(neto21, txtCuentaNGrav21, "Neto Gravado 21") Then GoTo Fin
            If Not ValidarImporteCuenta(neto27, txtCuentaNGrav27, "Neto Gravado 27") Then GoTo Fin
            If Not ValidarImporteCuenta(exentos, txtCuentaExentos, "Exentos") Then GoTo Fin
            If Not ValidarImporteCuenta(ganancias, txtCuentaGanancia, "Ganancias") Then GoTo Fin
            If Not ValidarImporteCuenta(rpi, txtCuentaRetPerIVA, "Ret. / Per. IVA") Then GoTo Fin
            If Not ValidarImporteCuenta(ib1, txtCuentaIngresosBrutos1, "Ingresos Brutos 1") Then GoTo Fin
            If Not ValidarImporteCuenta(ib2, txtCuentaIngresosBrutos2, "Ingresos Brutos 2") Then GoTo Fin
            If Not ValidarImporteCuenta(ib3, txtCuentaIngresosBrutos3, "Ingresos Brutos 3") Then GoTo Fin
            If Not ValidarImporteCuenta(ib4, txtCuentaIngresosBrutos4, "Ingresos Brutos 4") Then GoTo Fin
            If Not ValidarImporteCuenta(ib5, txtCuentaIngresosBrutos5, "Ingresos Brutos 5") Then GoTo Fin
            If Not ValidarImporteCuenta(ib6, txtCuentaIngresosBrutos6, "Ingresos Brutos 6") Then GoTo Fin

            ' Monto1 / CuentaMonto1
            If Not ValidarImporteCuenta(monto1, cmbCuentaMonto1, "Monto 1") Then GoTo Fin
            If Not ValidarImporteCuenta(monto2, cmbCuentaMonto2, "Monto 2") Then GoTo Fin
            If Not ValidarImporteCuenta(monto3, cmbCuentaMonto3, "Monto 3") Then GoTo Fin

            ' 5) Cálculo de IVA (si no es Despacho)
            If Not String.Equals(cmbComprobante.Text, "Despacho", StringComparison.OrdinalIgnoreCase) Then
                iva = 0D
                If neto21 > 0D Then iva += neto21 * 0.21D
                If neto27 > 0D Then iva += neto27 * 0.27D
                If neto105 > 0D Then iva += neto105 * 0.105D
                NumericTextBehavior.SetValue(txtIVA, iva)
            End If

            ' 6) Armado del monto (si Monto1 == 0, lo calculamos como en VB6)
            If monto1 = 0D Then
                Dim totalDebe As Decimal =
                    comprasRNI + neto21 + neto27 + neto105 + exentos +
                    iva + ganancias + rpi + ib1 + ib2 + ib3 + ib4 + ib5 + ib6

                monto1 = totalDebe
                NumericTextBehavior.SetValue(txtMonto1, monto1)
            End If

            ' 7) Consistir DEBE vs HABER
            Dim debe As Decimal =
              comprasRNI + neto21 + neto27 + neto105 + exentos +
              iva + ganancias + rpi + ib1 + ib2 + ib3 + ib4 + ib5 + ib6

            Dim haber As Decimal = monto1 + monto2 + monto3

            Dim diff As Decimal = Math.Truncate(haber) - Math.Truncate(debe)
            If diff > 1D OrElse diff < 0D Then
                MessageBox.Show("El asiento no cuadra. Revise por favor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                _suspenderAccionFiltros = False
                Return
            End If

            ' 8) Validaciones de comprobante / sucursal / imputación
            If String.IsNullOrWhiteSpace(cmbComprobante.Text) Then
                MessageBox.Show("Indique tipo de comprobante.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                _suspenderAccionFiltros = False
                Return
            End If

            If cmbComprobante.SelectedValue Is Nothing OrElse Val(cmbComprobante.SelectedValue.ToString()) <= 0 Then
                MessageBox.Show("Mal imputado el comprobante.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                _suspenderAccionFiltros = False
                Return
            End If

            If String.IsNullOrWhiteSpace(cmbSucursal.Text) Then
                MessageBox.Show("Mal indicada la sucursal.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                _suspenderAccionFiltros = False
                Return
            End If

            Dim idImputacion As Integer = Convert.ToInt32(cmbComprobante.SelectedValue)
            Dim sucursal As String = cmbSucursal.Text
            Dim puntoVenta As Integer = Val(txtPuntoVenta.Text)
            Dim nroComprobante As String = txtNroComprobante.Text.Trim()
            Dim nroDespacho As String = txtDespacho.Text.Trim()
            Dim nombreComprobante As String = cmbComprobante.Text
            Dim cai As String = txtCAI.Text.Trim()
            Dim comentario As String = txtComentario.Text.Trim()

            ' 9) Validar duplicados de factura (solo en alta, no en modificación)
            If filaActual Is Nothing Then
                If FacturaDuplicada(idImputacion, nroCuenta, nroFactura, puntoVenta) Then
                    _suspenderAccionFiltros = False
                    Return
                End If
            End If

            ' 10) Cálculo de dólar si aplica
            If chkDolar.Checked Then
                Dim totalHaber As Decimal = monto1 + monto2 + monto3
                If totalHaber = 0D Then
                    MessageBox.Show("Monto del haber no puede ser cero para operación en dólares.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    _suspenderAccionFiltros = False
                    Return
                End If

                If dolar = 0D Then
                    MessageBox.Show("El valor de dólar no puede ser cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    _suspenderAccionFiltros = False
                    Return
                End If

                ' Recalcular txtDolar como en VB6: monto / dolar
                Dim nuevoDolar As Decimal = totalHaber / dolar
                NumericTextBehavior.SetValue(txtDolar, nuevoDolar)
                dolar = nuevoDolar
            End If

            ' 11) UPSERT en NoveCtaCte (INSERT / UPDATE)
            Dim sql As String
            Dim parametros As Object

            If filaActual Is Nothing Then
                ' INSERT
                sql =
                    "INSERT INTO NoveCtaCte (" &
                    "IdCtaCte, NroCuenta, Sucursal, PuntoDeVenta, NroFactura, FondoFijo, " &
                    "NroComprobante, NroDespacho, Fecha, NombreComprobante, IdImputacion, CAI, Dolar, " &
                    "ComprasRNI, CtaRNI, Neto105, CtaNeto105, Neto21, Cta21, Neto27, Cta27, " &
                    "Exento, CtaExento, IVA, CtaIva, Ganancias, CtaGanancia, " &
                    "Retenciva, CtaRetencion, IngresosB, CtaIB, IngresosB2, CtaIB2, " &
                    "IngresosB3, CtaIB3, IngresosB4, CtaIB4, IngresosB5, CtaIB5, IngresosB6, CtaIB6, " &
                    "Monto, CtaMonto, Monto1, CtaMonto1, Monto2, CtaMonto2, Comentario" &
                    ") VALUES (" &
                    "@IdCtaCte, @NroCuenta, @Sucursal, @PuntoDeVenta, @NroFactura, @FondoFijo, " &
                    "@NroComprobante, @NroDespacho, @Fecha, @NombreComprobante, @IdImputacion, @CAI, @Dolar, " &
                    "@ComprasRNI, @CtaRNI, @Neto105, @CtaNeto105, @Neto21, @Cta21, @Neto27, @Cta27, " &
                    "@Exento, @CtaExento, @IVA, @CtaIva, @Ganancias, @CtaGanancia, " &
                    "@Retenciva, @CtaRetencion, @IngresosB, @CtaIB, @IngresosB2, @CtaIB2, " &
                    "@IngresosB3, @CtaIB3, @IngresosB4, @CtaIB4, @IngresosB5, @CtaIB5, @IngresosB6, @CtaIB6, " &
                    "@Monto, @CtaMonto, @Monto1, @CtaMonto1, @Monto2, @CtaMonto2, @Comentario" &
                    ")"

            Else
                ' UPDATE
                Dim idDeta As Integer = Convert.ToInt32(filaActual.Cells("IdDetaCtaCte").Value)
                sql =
                    "UPDATE NoveCtaCte SET " &
                    "IdCtaCte = @IdCtaCte, " &
                    "NroCuenta = @NroCuenta, " &
                    "Sucursal = @Sucursal, " &
                    "PuntoDeVenta = @PuntoDeVenta, " &
                    "NroFactura = @NroFactura, " &
                    "FondoFijo = @FondoFijo, " &
                    "NroComprobante = @NroComprobante, " &
                    "NroDespacho = @NroDespacho, " &
                    "Fecha = @Fecha, " &
                    "NombreComprobante = @NombreComprobante, " &
                    "IdImputacion = @IdImputacion, " &
                    "CAI = @CAI, " &
                    "Dolar = @Dolar, " &
                    "ComprasRNI = @ComprasRNI, " &
                    "CtaRNI = @CtaRNI, " &
                    "Neto105 = @Neto105, " &
                    "CtaNeto105 = @CtaNeto105, " &
                    "Neto21 = @Neto21, " &
                    "Cta21 = @Cta21, " &
                    "Neto27 = @Neto27, " &
                    "Cta27 = @Cta27, " &
                    "Exento = @Exento, " &
                    "CtaExento = @CtaExento, " &
                    "IVA = @IVA, " &
                    "CtaIva = @CtaIva, " &
                    "Ganancias = @Ganancias, " &
                    "CtaGanancia = @CtaGanancia, " &
                    "Retenciva = @Retenciva, " &
                    "CtaRetencion = @CtaRetencion, " &
                    "IngresosB = @IngresosB, " &
                    "CtaIB = @CtaIB, " &
                    "IngresosB2 = @IngresosB2, " &
                    "CtaIB2 = @CtaIB2, " &
                    "IngresosB3 = @IngresosB3, " &
                    "CtaIB3 = @CtaIB3, " &
                    "IngresosB4 = @IngresosB4, " &
                    "CtaIB4 = @CtaIB4, " &
                    "IngresosB5 = @IngresosB5, " &
                    "CtaIB5 = @CtaIB5, " &
                    "IngresosB6 = @IngresosB6, " &
                    "CtaIB6 = @CtaIB6, " &
                    "Monto = @Monto, " &
                    "CtaMonto = @CtaMonto, " &
                    "Monto1 = @Monto1, " &
                    "CtaMonto1 = @CtaMonto1, " &
                    "Monto2 = @Monto2, " &
                    "CtaMonto2 = @CtaMonto2, " &
                    "Comentario = @Comentario " &
                    "WHERE IdDetaCtaCte = @IdDetaCtaCte"

                parametros = CmdParams(
                    "@IdCtaCte", idCtaCte,
                    "@NroCuenta", nroCuenta,
                    "@Sucursal", sucursal,
                    "@PuntoDeVenta", puntoVenta,
                    "@NroFactura", nroFactura,
                    "@FondoFijo", fondoFijo,
                    "@NroComprobante", nroComprobante,
                    "@NroDespacho", nroDespacho,
                    "@Fecha", fecha,
                    "@NombreComprobante", nombreComprobante,
                    "@IdImputacion", idImputacion,
                    "@CAI", cai,
                    "@Dolar", dolar,
                    "@ComprasRNI", comprasRNI,
                    "@CtaRNI", txtCuentaComprasRNI.Text.Trim(),
                    "@Neto105", neto105,
                    "@CtaNeto105", txtCuentaNGrav105.Text.Trim(),
                    "@Neto21", neto21,
                    "@Cta21", txtCuentaNGrav21.Text.Trim(),
                    "@Neto27", neto27,
                    "@Cta27", txtCuentaNGrav27.Text.Trim(),
                    "@Exento", exentos,
                    "@CtaExento", txtCuentaExentos.Text.Trim(),
                    "@IVA", iva,
                    "@CtaIva", txtCuentaIVA.Text.Trim(),
                    "@Ganancias", ganancias,
                    "@CtaGanancia", txtCuentaGanancia.Text.Trim(),
                    "@Retenciva", rpi,
                    "@CtaRetencion", txtCuentaRetPerIVA.Text.Trim(),
                    "@IngresosB", ib1,
                    "@CtaIB", txtCuentaIngresosBrutos1.Text.Trim(),
                    "@IngresosB2", ib2,
                    "@CtaIB2", txtCuentaIngresosBrutos2.Text.Trim(),
                    "@IngresosB3", ib3,
                    "@CtaIB3", txtCuentaIngresosBrutos3.Text.Trim(),
                    "@IngresosB4", ib4,
                    "@CtaIB4", txtCuentaIngresosBrutos4.Text.Trim(),
                    "@IngresosB5", ib5,
                    "@CtaIB5", txtCuentaIngresosBrutos5.Text.Trim(),
                    "@IngresosB6", ib6,
                    "@CtaIB6", txtCuentaIngresosBrutos6.Text.Trim(),
                    "@Monto", monto1,
                    "@CtaMonto", cmbCuentaMonto1.Text.Trim(),
                    "@Monto1", monto2,
                    "@CtaMonto1", cmbCuentaMonto2.Text.Trim(),
                    "@Monto2", monto3,
                    "@CtaMonto2", cmbCuentaMonto3.Text.Trim(),
                    "@Comentario", comentario,
                    "@IdDetaCtaCte", idDeta
                  )

                DSM.Execute(DSM.Proveedores, sql, parametros)
                FormModoConsulta()
                SeleccionarFilaActual() ' vuelve a recargar el grid
                _suspenderAccionFiltros = False
                Return
            End If

            ' Si es INSERT, armamos parámetros y ejecutamos
            parametros = CmdParams(
              "@IdCtaCte", idCtaCte,
              "@NroCuenta", nroCuenta,
              "@Sucursal", sucursal,
              "@PuntoDeVenta", puntoVenta,
              "@NroFactura", nroFactura,
              "@FondoFijo", fondoFijo,
              "@NroComprobante", nroComprobante,
              "@NroDespacho", nroDespacho,
              "@Fecha", fecha,
              "@NombreComprobante", nombreComprobante,
              "@IdImputacion", idImputacion,
              "@CAI", cai,
              "@Dolar", dolar,
              "@ComprasRNI", comprasRNI,
              "@CtaRNI", txtCuentaComprasRNI.Text.Trim(),
              "@Neto105", neto105,
              "@CtaNeto105", txtCuentaNGrav105.Text.Trim(),
              "@Neto21", neto21,
              "@Cta21", txtCuentaNGrav21.Text.Trim(),
              "@Neto27", neto27,
              "@Cta27", txtCuentaNGrav27.Text.Trim(),
              "@Exento", exentos,
              "@CtaExento", txtCuentaExentos.Text.Trim(),
              "@IVA", iva,
              "@CtaIva", txtCuentaIVA.Text.Trim(),
              "@Ganancias", ganancias,
              "@CtaGanancia", txtCuentaGanancia.Text.Trim(),
              "@Retenciva", rpi,
              "@CtaRetencion", txtCuentaRetPerIVA.Text.Trim(),
              "@IngresosB", ib1,
              "@CtaIB", txtCuentaIngresosBrutos1.Text.Trim(),
              "@IngresosB2", ib2,
              "@CtaIB2", txtCuentaIngresosBrutos2.Text.Trim(),
              "@IngresosB3", ib3,
              "@CtaIB3", txtCuentaIngresosBrutos3.Text.Trim(),
              "@IngresosB4", ib4,
              "@CtaIB4", txtCuentaIngresosBrutos4.Text.Trim(),
              "@IngresosB5", ib5,
              "@CtaIB5", txtCuentaIngresosBrutos5.Text.Trim(),
              "@IngresosB6", ib6,
              "@CtaIB6", txtCuentaIngresosBrutos6.Text.Trim(),
              "@Monto", monto1,
              "@CtaMonto", cmbCuentaMonto1.Text.Trim(),
              "@Monto1", monto2,
              "@CtaMonto1", cmbCuentaMonto2.Text.Trim(),
              "@Monto2", monto3,
              "@CtaMonto2", cmbCuentaMonto3.Text.Trim(),
              "@Comentario", comentario
            )

            DSM.Execute(DSM.Proveedores, sql, parametros)

            FormModoConsulta()
            SeleccionarUltimaFila()

Fin:
            _suspenderAccionFiltros = False

        Catch ex As Exception
            _suspenderAccionFiltros = False
            MessageBox.Show("No se ha podido realizar la tarea. Causa: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ObtenerFechaCierreIva() As Date?
        Dim sql = "SELECT TOP 1 Cierre FROM CierreIva ORDER BY Cierre DESC"
        Dim dt = DSM.ExecuteQuery(DSM.Proveedores, sql, Nothing)
        If dt.Rows.Count = 0 Then Return Nothing
        Return Convert.ToDateTime(dt.Rows(0)("Cierre"))
    End Function

    Private Function VerificarCuenta(codContable As String) As Boolean
        If String.IsNullOrWhiteSpace(codContable) Then Return False
        Dim sql = "SELECT TOP 1 CodContable FROM PlanCuentas WHERE CodContable = @Cod"
        Dim dt = DSM.ExecuteQuery(DSM.Contabilidad, sql, CmdParams("@Cod", codContable))
        Return dt.Rows.Count > 0
    End Function

    Private Function ValidarImporteCuenta(importe As Decimal, txtCuenta As TextBox, descripcion As String) As Boolean
        Dim cuenta = txtCuenta.Text.Trim()

        If importe > 0D AndAlso cuenta <> "" Then
            If Not VerificarCuenta(cuenta) Then
                MessageBox.Show($"La cuenta para {descripcion} no existe.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtCuenta.Focus()
                Return False
            End If
        Else
            If importe = 0D AndAlso cuenta <> "" Then
                MessageBox.Show($"Debe especificar un monto para {descripcion}.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If
            If importe > 0D AndAlso cuenta = "" Then
                MessageBox.Show($"Debe especificar una cuenta para {descripcion}.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtCuenta.Focus()
                Return False
            End If
        End If

        Return True
    End Function

    Private Function ValidarImporteCuenta(importe As Decimal, cmbCuenta As ComboBox, descripcion As String) As Boolean
        Dim cuenta = cmbCuenta.Text.Trim()

        If importe > 0D AndAlso cuenta <> "" Then
            If Not VerificarCuenta(cuenta) Then
                MessageBox.Show($"La cuenta para {descripcion} no existe.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cmbCuenta.Focus()
                Return False
            End If
        Else
            If importe <> 0D AndAlso cuenta = "" Then
                MessageBox.Show($"Debe especificar una cuenta para {descripcion}.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cmbCuenta.Focus()
                Return False
            End If
        End If

        Return True
    End Function

    Private Function FacturaDuplicada(idImputacion As Integer, nroCuenta As String, nroFactura As Integer, puntoVenta As Integer) As Boolean
        If nroFactura <= 0 Then Return False

        Dim sqlNove =
            "SELECT COUNT(*) AS Cnt " &
            "FROM NoveCtaCte " &
            "WHERE IdImputacion = @IdImputacion " &
            "AND NroCuenta = @NroCuenta " &
            "AND NroFactura = @NroFactura " &
            "AND PuntoDeVenta = @PuntoDeVenta"

        Dim p = CmdParams(
            "@IdImputacion", idImputacion,
            "@NroCuenta", nroCuenta,
            "@NroFactura", nroFactura,
            "@PuntoDeVenta", puntoVenta
          )

        Dim dtNove = DSM.ExecuteQuery(DSM.Proveedores, sqlNove, p)
        Dim existeNove As Boolean = Convert.ToInt32(dtNove.Rows(0)("Cnt")) > 0

        If existeNove Then
            MessageBox.Show("Este Nro. de Factura ya existe en el NOVE de esta cuenta en este mes.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtNroFactura.Focus()
            Return True
        End If

        Dim sqlDeta =
            "SELECT COUNT(*) AS Cnt " &
            "FROM DetaCtaCte " &
            "WHERE IdImputacion = @IdImputacion " &
            "AND NroCuenta = @NroCuenta " &
            "AND NroFactura = @NroFactura " &
            "AND PuntoDeVenta = @PuntoDeVenta"

        Dim dtDeta = DSM.ExecuteQuery(DSM.Proveedores, sqlDeta, p)
        If Convert.ToInt32(dtDeta.Rows(0)("Cnt")) > 0 Then
            MessageBox.Show("Este Nro. de Factura ya existe en el DETALLE en esta cuenta en este mes.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtNroFactura.Focus()
            Return True
        End If

        Dim sqlDetaAnual =
            "SELECT COUNT(*) AS Cnt " &
            "FROM DetaCtaCteAnual " &
            "WHERE IdImputacion = @IdImputacion " &
            "AND NroCuenta = @NroCuenta " &
            "AND NroFactura = @NroFactura " &
            "AND PuntoDeVenta = @PuntoDeVenta"

        Dim dtDetaAnual = DSM.ExecuteQuery(DSM.Proveedores, sqlDetaAnual, p)
        If Convert.ToInt32(dtDetaAnual.Rows(0)("Cnt")) > 0 Then
            MessageBox.Show("Este Nro. de Factura ya existe en el DETALLE ANUAL en esta cuenta.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtNroFactura.Focus()
            Return True
        End If

        Return False
    End Function


    'Private Sub cmdAceptar_Click(sender As Object, e As EventArgs) Handles cmdAceptar.Click
    '    _suspenderAccionFiltros = True

    '    Dim Resultado As Double = 0D
    '    Dim resultado2 As Double = 0D

    '    ' si la factura es cero, alertar
    '    If Val(txtNroFactura.Text) = 0 Then
    '        MessageBox.Show("Nro. de Factura no puede ser cero...")

    '        _suspenderAccionFiltros = False
    '        Return
    '    End If

    '    If (cmbCuentaMonto1.Text = "1.1.40" Or cmbCuentaMonto1.Text = "1.1.41" Or cmbCuentaMonto1.Text = "1.1.44") And Val(txtFondoFijo) = 0 Then
    '        MessageBox.Show("DEBE INGRESAR NRO. DE LOTE PARA LA CUENTA MONTO  IMPUTADA GRACIAS !!!")
    '        txtFondoFijo.Focus()
    '        _suspenderAccionFiltros = False
    '        Return
    '    End If



    '    MessageBox.Show("en construccion...")
    '    _suspenderAccionFiltros = False
    'End Sub

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
            whereTexto = "(prov.NroCuenta LIKE @Texto OR NroFactura LIKE @Texto OR NroComprobante LIKE @Texto OR MaeCtaCte.Nombre LIKE @Texto OR prov.Comentario LIKE @Texto)"
        End If
        Dim sql = $"
            SELECT prov.*, MaeCtaCte.Nombre AS NombreProveedor
            FROM NoveCtaCte as prov
            LEFT JOIN MaeCtaCte ON prov.NroCuenta = MaeCtaCte.NroCuenta
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
        SeleccionarFila(0)

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

        grid.Columns("NombreProveedor").Visible = True
        grid.Columns("NombreProveedor").HeaderText = "Proveedor"
        grid.Columns("NombreProveedor").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        grid.Columns("NombreProveedor").DisplayIndex = 1

        grid.Columns("NroFactura").Visible = True
        grid.Columns("NroFactura").HeaderText = "Nro.Fact."
        grid.Columns("NroFactura").Width = 80
        grid.Columns("NroFactura").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grid.Columns("NroFactura").DisplayIndex = 2

        grid.Columns("Monto").Visible = True
        grid.Columns("Monto").HeaderText = "Monto"
        grid.Columns("Monto").Width = 100
        grid.Columns("Monto").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grid.Columns("Monto").DefaultCellStyle.Format = "N2"
        grid.Columns("Monto").DisplayIndex = 3

        grid.Columns("NroComprobante").Visible = True
        grid.Columns("NroComprobante").HeaderText = "Nro.Comp."
        grid.Columns("NroComprobante").Width = 80
        grid.Columns("NroComprobante").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grid.Columns("NroComprobante").DisplayIndex = 4

        grid.Columns("NombreComprobante").Visible = True
        grid.Columns("NombreComprobante").HeaderText = "Tipo Comp."
        grid.Columns("NombreComprobante").Width = 90
        grid.Columns("NombreComprobante").DisplayIndex = 5

        grid.Columns("Condicion").Visible = True
        grid.Columns("Condicion").HeaderText = "Condición"
        grid.Columns("Condicion").Width = 130
        grid.Columns("Condicion").DisplayIndex = 6

        grid.Columns("Fecha").Visible = True
        grid.Columns("Fecha").HeaderText = "Fecha"
        grid.Columns("Fecha").Width = 80
        grid.Columns("Fecha").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grid.Columns("Fecha").DefaultCellStyle.Format = "dd/MM/yyyy"
        grid.Columns("Fecha").DisplayIndex = 7

        grid.Columns("Comentario").Visible = True
        grid.Columns("Comentario").HeaderText = "Comentario"
        grid.Columns("Comentario").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        grid.Columns("Comentario").DisplayIndex = 9


        'grid.Columns("IdImputacion").Visible = True
        'grid.Columns("IdImputacion").HeaderText = "Imp"
        'grid.Columns("IdImputacion").Width = 50
        'grid.Columns("IdImputacion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'grid.Columns("IdImputacion").DisplayIndex = 7

        'grid.Columns("CtaMonto").Visible = True
        'grid.Columns("CtaMonto").HeaderText = "Cta.Monto"
        'grid.Columns("CtaMonto").Width = 80
        'grid.Columns("CtaMonto").DisplayIndex = 8

        ConfigurarEstiloGrid(grid)

        grid.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect
    End Sub

    Private Sub FormLimpiarSeleccionado()
        cmbSucursal.SelectedIndex = -1
        cmbProveedor.SelectedIndex = -1
        txtNroCuenta.Text = String.Empty
        txtPuntoVenta.Text = String.Empty
        txtNroFactura.Text = String.Empty
        'txtFondoFijo.Text = String.Empty
        NumericTextBehavior.SetValue(txtFondoFijo, 0D)
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
        NumericTextBehavior.SetValue(txtIngresosBrutos5, 0D)
        txtCuentaIngresosBrutos5.Text = String.Empty
        NumericTextBehavior.SetValue(txtIngresosBrutos6, 0D)
        txtCuentaIngresosBrutos6.Text = String.Empty


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

    Private Function ObtenerProveedor(Optional idctacte = Nothing, Optional nrocta = Nothing) As DataRow

        If idctacte IsNot Nothing Then
            Dim sql = "SELECT NroCuenta, Nombre, Cuit from MaeCtaCte WHERE IdCtaCte = @IdCtaCte"
            Dim parametros = CmdParams("@IdCtaCte", idctacte)
            Dim dt = DSM.ExecuteQuery(DSM.Proveedores, sql, parametros)
            If dt.Rows.Count = 0 Then
                Return Nothing
            End If
            Return dt.Rows(0)
        End If

        If nrocta IsNot Nothing Then
            Dim sql = "SELECT NroCuenta, Nombre, Cuit from MaeCtaCte WHERE NroCuenta = @NroCuenta"
            Dim parametros = CmdParams("@NroCuenta", nrocta)
            Dim dt = DSM.ExecuteQuery(DSM.Proveedores, sql, parametros)
            If dt.Rows.Count = 0 Then
                Return Nothing
            End If
            Return dt.Rows(0)
        End If

        Return Nothing
    End Function

    Private Sub FormObtenerSeleccionado()
        If filaActual Is Nothing Then Return

        cmbSucursal.SelectedIndex = cmbSucursal.FindStringExact(filaActual.Cells("Sucursal").Value.ToString())
        'cmbProveedor.SelectedValue = If(filaActual.Cells("IdCtaCte").Value IsNot DBNull.Value, Convert.ToInt32(filaActual.Cells("IdCtaCte").Value), -1)
        txtNroCuenta.Text = If(filaActual.Cells("NroCuenta").Value IsNot DBNull.Value, filaActual.Cells("NroCuenta").Value.ToString(), String.Empty)
        Dim nroCta = txtNroCuenta.Text.Trim()
        Dim proveedor = ObtenerProveedor(Nothing, nroCta)
        cmbProveedor.SelectedIndex = If(proveedor IsNot Nothing, cmbProveedor.FindStringExact(proveedor.Item("Nombre").ToString()), -1)
        txtNroCuenta.Text = If(proveedor IsNot Nothing, proveedor.Item("NroCuenta").ToString(), String.Empty)
        txtCuit.Text = If(proveedor IsNot Nothing, proveedor.Item("Cuit").ToString(), String.Empty)
        txtPuntoVenta.Text = If(filaActual.Cells("PuntodeVenta").Value IsNot DBNull.Value, filaActual.Cells("PuntodeVenta").Value.ToString(), String.Empty)
        txtNroFactura.Text = If(filaActual.Cells("NroFactura").Value IsNot DBNull.Value, filaActual.Cells("NroFactura").Value.ToString(), String.Empty)
        'txtFondoFijo.Text = If(filaActual.Cells("FondoFijo").Value IsNot DBNull.Value, filaActual.Cells("FondoFijo").Value.ToString(), String.Empty)
        NumericTextBehavior.SetValue(txtFondoFijo, Convert.ToDouble(filaActual.Cells("FondoFijo").Value))
        txtNroComprobante.Text = If(filaActual.Cells("NroComprobante").Value IsNot DBNull.Value, filaActual.Cells("NroComprobante").Value.ToString(), String.Empty)
        txtDespacho.Text = If(filaActual.Cells("NroDespacho").Value IsNot DBNull.Value, filaActual.Cells("NroDespacho").Value.ToString(), String.Empty)
        dtpFecha.Value = If(filaActual.Cells("Fecha").Value IsNot DBNull.Value, Convert.ToDateTime(filaActual.Cells("Fecha").Value), Date.Today)
        cmbComprobante.SelectedIndex = cmbComprobante.FindStringExact(filaActual.Cells("NombreComprobante").Value.ToString())
        txtCAI.Text = If(filaActual.Cells("CAI").Value IsNot DBNull.Value, filaActual.Cells("CAI").Value.ToString(), String.Empty)
        chkDolar.Checked = False
        NumericTextBehavior.SetValue(txtDolar, Convert.ToDouble(filaActual.Cells("Dolar").Value))

        NumericTextBehavior.SetValue(txtComprasRNI, Convert.ToDouble(filaActual.Cells("ComprasRNI").Value))
        txtCuentaComprasRNI.Text = If(filaActual.Cells("CtaRNI").Value IsNot DBNull.Value, filaActual.Cells("CtaRNI").Value.ToString(), String.Empty)
        NumericTextBehavior.SetValue(txtNGrav105, Convert.ToDouble(filaActual.Cells("Neto105").Value))
        txtCuentaNGrav105.Text = If(filaActual.Cells("CtaNeto105").Value IsNot DBNull.Value, filaActual.Cells("CtaNeto105").Value.ToString(), String.Empty)
        NumericTextBehavior.SetValue(txtNGrav21, Convert.ToDouble(filaActual.Cells("Neto21").Value))
        txtCuentaNGrav21.Text = If(filaActual.Cells("Cta21").Value IsNot DBNull.Value, filaActual.Cells("Cta21").Value.ToString(), String.Empty)
        NumericTextBehavior.SetValue(txtNGrav27, Convert.ToDouble(filaActual.Cells("Neto27").Value))
        txtCuentaNGrav27.Text = If(filaActual.Cells("Cta27").Value IsNot DBNull.Value, filaActual.Cells("Cta27").Value.ToString(), String.Empty)
        NumericTextBehavior.SetValue(txtExentos, Convert.ToDouble(filaActual.Cells("Exento").Value))
        txtCuentaExentos.Text = If(filaActual.Cells("CtaExento").Value IsNot DBNull.Value, filaActual.Cells("CtaExento").Value.ToString(), String.Empty)
        NumericTextBehavior.SetValue(txtIVA, Convert.ToDouble(filaActual.Cells("IVA").Value))
        txtCuentaIVA.Text = If(filaActual.Cells("CtaIva").Value IsNot DBNull.Value, filaActual.Cells("CtaIva").Value.ToString(), String.Empty)
        NumericTextBehavior.SetValue(txtGanancia, Convert.ToDouble(filaActual.Cells("Ganancias").Value))
        txtCuentaGanancia.Text = If(filaActual.Cells("CtaGanancia").Value IsNot DBNull.Value, filaActual.Cells("CtaGanancia").Value.ToString(), String.Empty)
        NumericTextBehavior.SetValue(txtRetPerIVA, Convert.ToDouble(filaActual.Cells("Retenciva").Value))
        txtCuentaRetPerIVA.Text = If(filaActual.Cells("CtaRetencion").Value IsNot DBNull.Value, filaActual.Cells("CtaRetencion").Value.ToString(), String.Empty)
        NumericTextBehavior.SetValue(txtIngresosBrutos1, Convert.ToDouble(filaActual.Cells("IngresosB").Value))
        txtCuentaIngresosBrutos1.Text = If(filaActual.Cells("CtaIB").Value IsNot DBNull.Value, filaActual.Cells("CtaIB").Value.ToString(), String.Empty)
        NumericTextBehavior.SetValue(txtIngresosBrutos2, Convert.ToDouble(filaActual.Cells("IngresosB2").Value))
        txtCuentaIngresosBrutos2.Text = If(filaActual.Cells("CtaIb2").Value IsNot DBNull.Value, filaActual.Cells("CtaIb2").Value.ToString(), String.Empty)
        NumericTextBehavior.SetValue(txtIngresosBrutos3, Convert.ToDouble(filaActual.Cells("IngresosB3").Value))
        txtCuentaIngresosBrutos3.Text = If(filaActual.Cells("CtaIB3").Value IsNot DBNull.Value, filaActual.Cells("CtaIB3").Value.ToString(), String.Empty)
        NumericTextBehavior.SetValue(txtIngresosBrutos4, Convert.ToDouble(filaActual.Cells("IngresosB4").Value))
        txtCuentaIngresosBrutos4.Text = If(filaActual.Cells("CtaIB4").Value IsNot DBNull.Value, filaActual.Cells("CtaIB4").Value.ToString(), String.Empty)
        NumericTextBehavior.SetValue(txtIngresosBrutos5, Convert.ToDouble(filaActual.Cells("IngresosB5").Value))
        txtCuentaIngresosBrutos5.Text = If(filaActual.Cells("CtaIB5").Value IsNot DBNull.Value, filaActual.Cells("CtaIB5").Value.ToString(), String.Empty)
        NumericTextBehavior.SetValue(txtIngresosBrutos6, Convert.ToDouble(filaActual.Cells("IngresosB6").Value))
        txtCuentaIngresosBrutos6.Text = If(filaActual.Cells("CtaIB6").Value IsNot DBNull.Value, filaActual.Cells("CtaIB6").Value.ToString(), String.Empty)

        NumericTextBehavior.SetValue(txtMonto1, Convert.ToDouble(filaActual.Cells("Monto").Value))
        'cmbCuentaMonto1.SelectedIndex = cmbCuentaMonto1.FindStringExact(filaActual.Cells("CtaMonto").Value.ToString())
        cmbCuentaMonto1.Text = If(filaActual.Cells("CtaMonto").Value IsNot DBNull.Value, filaActual.Cells("CtaMonto").Value.ToString(), String.Empty)
        NumericTextBehavior.SetValue(txtMonto2, Convert.ToDouble(filaActual.Cells("Monto1").Value))
        cmbCuentaMonto2.SelectedIndex = cmbCuentaMonto2.FindStringExact(filaActual.Cells("CtaMonto1").Value.ToString())
        NumericTextBehavior.SetValue(txtMonto3, Convert.ToDouble(filaActual.Cells("Monto2").Value))
        cmbCuentaMonto3.SelectedIndex = cmbCuentaMonto3.FindStringExact(filaActual.Cells("CtaMonto2").Value.ToString())

        txtComentario.Text = If(filaActual.Cells("Comentario").Value IsNot DBNull.Value, filaActual.Cells("Comentario").Value.ToString(), String.Empty)
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
        SetControlesEnabled(
            False, cmbSucursal, cmbProveedor, btnBuscarProveedor, txtPuntoVenta, txtNroFactura, txtFondoFijo,
            txtNroComprobante, txtDespacho, dtpFecha, cmbComprobante, txtCAI, chkDolar, txtDolar,
            txtComprasRNI, txtCuentaComprasRNI, txtNGrav105, txtCuentaNGrav105, txtNGrav21, txtCuentaNGrav21,
            txtNGrav27, txtCuentaNGrav27, txtExentos, txtCuentaExentos, txtIVA, txtCuentaIVA,
            txtGanancia, txtCuentaGanancia, txtRetPerIVA, txtCuentaRetPerIVA, txtIngresosBrutos1, txtCuentaIngresosBrutos1,
            txtIngresosBrutos2, txtCuentaIngresosBrutos2, txtIngresosBrutos3, txtCuentaIngresosBrutos3,
            txtIngresosBrutos4, txtCuentaIngresosBrutos4, txtIngresosBrutos5, txtCuentaIngresosBrutos5,
            txtIngresosBrutos6, txtCuentaIngresosBrutos6,
            txtMonto1, cmbCuentaMonto1, txtMonto2, cmbCuentaMonto2, txtMonto3, cmbCuentaMonto3, chkNoGeneraAsiento, txtComentario,
            cmdAceptar, CmdCancelar)
    End Sub

    Public Sub FormModoEdicion()
        SetControlesEnabled(False, CmdAgregar, CmdModificar, CmdBorrar)
        SetControlesEnabled(
            True, cmbSucursal, cmbProveedor, btnBuscarProveedor, txtPuntoVenta, txtNroFactura, txtFondoFijo,
            txtNroComprobante, txtDespacho, dtpFecha, cmbComprobante, txtCAI, chkDolar, txtDolar,
            txtComprasRNI, txtCuentaComprasRNI, txtNGrav105, txtCuentaNGrav105, txtNGrav21, txtCuentaNGrav21,
            txtNGrav27, txtCuentaNGrav27, txtExentos, txtCuentaExentos, txtIVA, txtCuentaIVA,
            txtGanancia, txtCuentaGanancia, txtRetPerIVA, txtCuentaRetPerIVA, txtIngresosBrutos1, txtCuentaIngresosBrutos1,
            txtIngresosBrutos2, txtCuentaIngresosBrutos2, txtIngresosBrutos3, txtCuentaIngresosBrutos3,
            txtIngresosBrutos4, txtCuentaIngresosBrutos4, txtIngresosBrutos5, txtCuentaIngresosBrutos5,
            txtIngresosBrutos6, txtCuentaIngresosBrutos6,
            txtMonto1, cmbCuentaMonto1, txtMonto2, cmbCuentaMonto2, txtMonto3, cmbCuentaMonto3, chkNoGeneraAsiento, txtComentario,
            cmdAceptar, CmdCancelar)
    End Sub

    Private Sub SeleccionarFila(numero As Integer)
        If DgvListado.Rows.Count > 0 AndAlso numero >= 0 AndAlso numero < DgvListado.Rows.Count Then
            DgvListado.Rows(numero).Selected = True
        End If
        filaActualIndice = numero
        filaActual = DgvListado.Rows(numero)
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

    Private Sub cmbProveedor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProveedor.SelectedIndexChanged
        ' If _suspenderAccionFiltros Then Exit Sub

        txtCuit.Text = String.Empty
        If cmbProveedor.SelectedValue IsNot Nothing Then
            Dim idctacte = cmbProveedor.SelectedValue.ToString()
            Dim proveedor = ObtenerProveedor(idctacte)
            If proveedor IsNot Nothing Then
                txtCuit.Text = proveedor.Item("Cuit").ToString()
            End If
        End If
    End Sub

    Private Sub btnBuscarProveedor_Click(sender As Object, e As EventArgs) Handles btnBuscarProveedor.Click
        Using frm As New frmProveedoresSelector()
            If frm.ShowDialog(Me) = DialogResult.OK Then
                Dim cuenta = frm.Seleccion
                Dim proveedor = frm.Seleccion
                cmbProveedor.SelectedIndex = If(proveedor IsNot Nothing, cmbProveedor.FindStringExact(proveedor.Item("Nombre").ToString()), -1)
                txtNroCuenta.Text = If(proveedor IsNot Nothing, proveedor.Item("NroCuenta").ToString(), String.Empty)
                txtCuit.Text = If(proveedor IsNot Nothing, proveedor.Item("Cuit").ToString(), String.Empty)
            End If
        End Using
    End Sub

    Private Sub Form1_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        LimpiarSeleccionCombos(Me)
    End Sub

    Private Sub LimpiarSeleccionCombos(parent As Control)
        For Each c As Control In parent.Controls
            If TypeOf c Is ComboBox Then
                Dim cb = DirectCast(c, ComboBox)
                cb.SelectionStart = cb.Text.Length
                cb.SelectionLength = 0
            End If

            If c.HasChildren Then
                LimpiarSeleccionCombos(c)
            End If
        Next
    End Sub
End Class
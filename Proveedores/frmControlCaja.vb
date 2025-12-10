Imports DSM = DataSourceManager.Lib.DataSourceManager
Public Class frmControlCaja
    Inherits Form
    Private Shared instancia As frmControlCaja
    Private tabla As New DataTable()
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1
    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmControlCaja()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub
    Private Sub frmControlCaja_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub
    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Close()
    End Sub

    Private Sub lnkCopiar_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCopiar.LinkClicked
        CopiarDataGrid(dgvListado, chkEncabezados.Checked)
    End Sub
    Private Sub dtpFecha_ValueChanged(sender As Object, e As EventArgs) Handles dtpFecha.ValueChanged
        Dim sql As String
        Dim parametros As Dictionary(Of String, Object)
        Dim fechaStr As String = dtpFecha.Value.ToString("dd/MM/yyyy")
        sql = "SELECT * FROM bkpnove WHERE Cierre LIKE @prefijo ORDER BY IdDetaCtaCte"
        parametros = CmdParams("@prefijo", fechaStr & "%")
        tabla = DSM.ExecuteQuery(DSM.Proveedores, sql, parametros)
        dgvListado.DataSource = tabla
        ConfigurarGrid()
        ConfigurarEstiloGrid(dgvListado)
        dgvListado.MultiSelect = True

    End Sub
    Private Sub ConfigurarGrid()
        If dgvListado.Columns.Count = 0 Then Exit Sub

        For Each col As DataGridViewColumn In dgvListado.Columns
            col.Visible = False
        Next

        If dgvListado.Columns.Contains("NroCuenta") Then
            dgvListado.Columns("NroCuenta").Visible = True
            dgvListado.Columns("NroCuenta").HeaderText = "NroCuenta"
            dgvListado.Columns("NroCuenta").Width = 100
        End If
        If dgvListado.Columns.Contains("NroFactura") Then
            dgvListado.Columns("NroFactura").Visible = True
            dgvListado.Columns("NroFactura").HeaderText = "NroFactura"
            dgvListado.Columns("NroFactura").Width = 100
        End If
        If dgvListado.Columns.Contains("Monto") Then
            dgvListado.Columns("Monto").Visible = True
            dgvListado.Columns("Monto").HeaderText = "Monto"
            dgvListado.Columns("Monto").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvListado.Columns("Monto").DefaultCellStyle.Format = "N2"
            dgvListado.Columns("Monto").Width = 110
        End If
        If dgvListado.Columns.Contains("NroComprobante") Then
            dgvListado.Columns("NroComprobante").Visible = True
            dgvListado.Columns("NroComprobante").HeaderText = "NroComprobante"
            dgvListado.Columns("NroComprobante").Width = 100
        End If
        If dgvListado.Columns.Contains("NroDespacho") Then
            dgvListado.Columns("NroDespacho").Visible = True
            dgvListado.Columns("NroDespacho").HeaderText = "NroDespacho"
            dgvListado.Columns("NroDespacho").Width = 100
        End If
        If dgvListado.Columns.Contains("NombreComprobante") Then
            dgvListado.Columns("NombreComprobante").Visible = True
            dgvListado.Columns("NombreComprobante").HeaderText = "NombreComprobante"
            dgvListado.Columns("NombreComprobante").Width = 140
        End If
        If dgvListado.Columns.Contains("Condicion") Then
            dgvListado.Columns("Condicion").Visible = True
            dgvListado.Columns("Condicion").HeaderText = "Condicion"
            dgvListado.Columns("Condicion").Width = 120
        End If
        If dgvListado.Columns.Contains("Fecha") Then
            dgvListado.Columns("Fecha").Visible = True
            dgvListado.Columns("Fecha").HeaderText = "Fecha"
            dgvListado.Columns("Fecha").Width = 80
        End If
        If dgvListado.Columns.Contains("IdImputacion") Then
            dgvListado.Columns("IdImputacion").Visible = True
            dgvListado.Columns("IdImputacion").HeaderText = "IdImputacion"
            dgvListado.Columns("IdImputacion").Width = 90
        End If
        If dgvListado.Columns.Contains("CtaMonto") Then
            dgvListado.Columns("CtaMonto").Visible = True
            dgvListado.Columns("CtaMonto").HeaderText = "CtaMonto"
            dgvListado.Columns("CtaMonto").Width = 90
        End If
        If dgvListado.Columns.Contains("Monto1") Then
            dgvListado.Columns("Monto1").Visible = True
            dgvListado.Columns("Monto1").HeaderText = "Monto1"
            dgvListado.Columns("Monto1").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvListado.Columns("Monto1").DefaultCellStyle.Format = "N2"
            dgvListado.Columns("Monto1").Width = 110
        End If
        If dgvListado.Columns.Contains("CtaMonto1") Then
            dgvListado.Columns("CtaMonto1").Visible = True
            dgvListado.Columns("CtaMonto1").HeaderText = "CtaMonto1"
            dgvListado.Columns("CtaMonto1").Width = 90
        End If
        If dgvListado.Columns.Contains("Monto2") Then
            dgvListado.Columns("Monto2").Visible = True
            dgvListado.Columns("Monto2").HeaderText = "Monto2"
            dgvListado.Columns("Monto2").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvListado.Columns("Monto2").DefaultCellStyle.Format = "N2"
            dgvListado.Columns("Monto2").Width = 110
        End If
        If dgvListado.Columns.Contains("CtaMonto2") Then
            dgvListado.Columns("CtaMonto2").Visible = True
            dgvListado.Columns("CtaMonto2").HeaderText = "CtaMonto2"
            dgvListado.Columns("CtaMonto2").Width = 90
        End If
        If dgvListado.Columns.Contains("ComprasRNI") Then
            dgvListado.Columns("ComprasRNI").Visible = True
            dgvListado.Columns("ComprasRNI").HeaderText = "ComprasRNI"
            dgvListado.Columns("ComprasRNI").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvListado.Columns("ComprasRNI").DefaultCellStyle.Format = "N2"
            dgvListado.Columns("ComprasRNI").Width = 110
        End If
        If dgvListado.Columns.Contains("CtaRNI") Then
            dgvListado.Columns("CtaRNI").Visible = True
            dgvListado.Columns("CtaRNI").HeaderText = "CtaRNI"
            dgvListado.Columns("CtaRNI").Width = 90
        End If
        If dgvListado.Columns.Contains("Neto105") Then
            dgvListado.Columns("Neto105").Visible = True
            dgvListado.Columns("Neto105").HeaderText = "Neto105"
            dgvListado.Columns("Neto105").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvListado.Columns("Neto105").DefaultCellStyle.Format = "N2"
            dgvListado.Columns("Neto105").Width = 110
        End If
        If dgvListado.Columns.Contains("CtaNeto105") Then
            dgvListado.Columns("CtaNeto105").Visible = True
            dgvListado.Columns("CtaNeto105").HeaderText = "CtaNeto105"
            dgvListado.Columns("CtaNeto105").Width = 90
        End If
        If dgvListado.Columns.Contains("Neto21") Then
            dgvListado.Columns("Neto21").Visible = True
            dgvListado.Columns("Neto21").HeaderText = "Neto21"
            dgvListado.Columns("Neto21").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvListado.Columns("Neto21").DefaultCellStyle.Format = "N2"
            dgvListado.Columns("Neto21").Width = 110
        End If
        If dgvListado.Columns.Contains("Cta21") Then
            dgvListado.Columns("Cta21").Visible = True
            dgvListado.Columns("Cta21").HeaderText = "Cta21"
            dgvListado.Columns("Cta21").Width = 90
        End If
        If dgvListado.Columns.Contains("Neto27") Then
            dgvListado.Columns("Neto27").Visible = True
            dgvListado.Columns("Neto27").HeaderText = "Neto27"
            dgvListado.Columns("Neto27").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvListado.Columns("Neto27").DefaultCellStyle.Format = "N2"
            dgvListado.Columns("Neto27").Width = 110
        End If
        If dgvListado.Columns.Contains("Cta27") Then
            dgvListado.Columns("Cta27").Visible = True
            dgvListado.Columns("Cta27").HeaderText = "Cta27"
            dgvListado.Columns("Cta27").Width = 90
        End If
        If dgvListado.Columns.Contains("Exento") Then
            dgvListado.Columns("Exento").Visible = True
            dgvListado.Columns("Exento").HeaderText = "Exento"
            dgvListado.Columns("Exento").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvListado.Columns("Exento").DefaultCellStyle.Format = "N2"
            dgvListado.Columns("Exento").Width = 110
        End If
        If dgvListado.Columns.Contains("CtaExento") Then
            dgvListado.Columns("CtaExento").Visible = True
            dgvListado.Columns("CtaExento").HeaderText = "CtaExento"
            dgvListado.Columns("CtaExento").Width = 90
        End If
        If dgvListado.Columns.Contains("IVA") Then
            dgvListado.Columns("IVA").Visible = True
            dgvListado.Columns("IVA").HeaderText = "IVA"
            dgvListado.Columns("IVA").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvListado.Columns("IVA").DefaultCellStyle.Format = "N2"
            dgvListado.Columns("IVA").Width = 110
        End If
        If dgvListado.Columns.Contains("CtaIva") Then
            dgvListado.Columns("CtaIva").Visible = True
            dgvListado.Columns("CtaIva").HeaderText = "CtaIva"
            dgvListado.Columns("CtaIva").Width = 90
        End If
        If dgvListado.Columns.Contains("Ganancias") Then
            dgvListado.Columns("Ganancias").Visible = True
            dgvListado.Columns("Ganancias").HeaderText = "Ganancias"
            dgvListado.Columns("Ganancias").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvListado.Columns("Ganancias").DefaultCellStyle.Format = "N2"
            dgvListado.Columns("Ganancias").Width = 110
        End If
        If dgvListado.Columns.Contains("CtaGanancia") Then
            dgvListado.Columns("CtaGanancia").Visible = True
            dgvListado.Columns("CtaGanancia").HeaderText = "CtaGanancia"
            dgvListado.Columns("CtaGanancia").Width = 90
        End If
        If dgvListado.Columns.Contains("Retenciva") Then
            dgvListado.Columns("Retenciva").Visible = True
            dgvListado.Columns("Retenciva").HeaderText = "Retenciva"
            dgvListado.Columns("Retenciva").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvListado.Columns("Retenciva").DefaultCellStyle.Format = "N2"
            dgvListado.Columns("Retenciva").Width = 110
        End If
        If dgvListado.Columns.Contains("CtaRetencion") Then
            dgvListado.Columns("CtaRetencion").Visible = True
            dgvListado.Columns("CtaRetencion").HeaderText = "CtaRetencion"
            dgvListado.Columns("CtaRetencion").Width = 90
        End If
        If dgvListado.Columns.Contains("IngresosB") Then
            dgvListado.Columns("IngresosB").Visible = True
            dgvListado.Columns("IngresosB").HeaderText = "IngresosB"
            dgvListado.Columns("IngresosB").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvListado.Columns("IngresosB").DefaultCellStyle.Format = "N2"
            dgvListado.Columns("IngresosB").Width = 110
        End If
        If dgvListado.Columns.Contains("CtaIB") Then
            dgvListado.Columns("CtaIB").Visible = True
            dgvListado.Columns("CtaIB").HeaderText = "CtaIB"
            dgvListado.Columns("CtaIB").Width = 90
        End If
        If dgvListado.Columns.Contains("ACuenta") Then
            dgvListado.Columns("ACuenta").Visible = True
            dgvListado.Columns("ACuenta").HeaderText = "ACuenta"
            dgvListado.Columns("ACuenta").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvListado.Columns("ACuenta").DefaultCellStyle.Format = "N2"
            dgvListado.Columns("ACuenta").Width = 110
        End If
        If dgvListado.Columns.Contains("FechaVto") Then
            dgvListado.Columns("FechaVto").Visible = True
            dgvListado.Columns("FechaVto").HeaderText = "FechaVto"
            dgvListado.Columns("FechaVto").Width = 80
        End If
        If dgvListado.Columns.Contains("TipoValor") Then
            dgvListado.Columns("TipoValor").Visible = True
            dgvListado.Columns("TipoValor").HeaderText = "TipoValor"
            dgvListado.Columns("TipoValor").Width = 100
        End If
        If dgvListado.Columns.Contains("NroCheque") Then
            dgvListado.Columns("NroCheque").Visible = True
            dgvListado.Columns("NroCheque").HeaderText = "NroCheque"
            dgvListado.Columns("NroCheque").Width = 100
        End If
        If dgvListado.Columns.Contains("RegInterno") Then
            dgvListado.Columns("RegInterno").Visible = True
            dgvListado.Columns("RegInterno").HeaderText = "RegInterno"
            dgvListado.Columns("RegInterno").Width = 90
        End If
        If dgvListado.Columns.Contains("Sucursal") Then
            dgvListado.Columns("Sucursal").Visible = True
            dgvListado.Columns("Sucursal").HeaderText = "Sucursal"
            dgvListado.Columns("Sucursal").Width = 90
        End If
        If dgvListado.Columns.Contains("Cobrado") Then
            Dim idx = dgvListado.Columns("Cobrado").Index
            Dim chk As New DataGridViewCheckBoxColumn()
            chk.Name = "Cobrado"
            chk.HeaderText = "Cobrado"
            chk.DataPropertyName = "Cobrado"
            chk.Width = 80
            chk.ReadOnly = True
            chk.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dgvListado.Columns.Remove("Cobrado")
            dgvListado.Columns.Insert(idx, chk)
            dgvListado.Columns("Cobrado").Visible = True
        End If
        If dgvListado.Columns.Contains("Asiento") Then
            Dim idx = dgvListado.Columns("Asiento").Index
            Dim chk As New DataGridViewCheckBoxColumn()
            chk.Name = "Asiento"
            chk.HeaderText = "Asiento"
            chk.DataPropertyName = "Asiento"
            chk.Width = 80
            chk.ReadOnly = True
            chk.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dgvListado.Columns.Remove("Asiento")
            dgvListado.Columns.Insert(idx, chk)
            dgvListado.Columns("Asiento").Visible = True
        End If
        If dgvListado.Columns.Contains("ActSaldo") Then
            Dim idx = dgvListado.Columns("ActSaldo").Index
            Dim chk As New DataGridViewCheckBoxColumn()
            chk.Name = "ActSaldo"
            chk.HeaderText = "ActSaldo"
            chk.DataPropertyName = "ActSaldo"
            chk.Width = 70
            chk.ReadOnly = True
            chk.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dgvListado.Columns.Remove("ActSaldo")
            dgvListado.Columns.Insert(idx, chk)
            dgvListado.Columns("ActSaldo").Visible = True
        End If
        If dgvListado.Columns.Contains("ActDeta") Then
            Dim idx = dgvListado.Columns("ActDeta").Index
            Dim chk As New DataGridViewCheckBoxColumn()
            chk.Name = "ActDeta"
            chk.HeaderText = "ActDeta"
            chk.DataPropertyName = "ActDeta"
            chk.Width = 70
            chk.ReadOnly = True
            chk.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dgvListado.Columns.Remove("ActDeta")
            dgvListado.Columns.Insert(idx, chk)
            dgvListado.Columns("ActDeta").Visible = True
        End If
        If dgvListado.Columns.Contains("FondoFijo") Then
            dgvListado.Columns("FondoFijo").Visible = True
            dgvListado.Columns("FondoFijo").HeaderText = "FondoFijo"
            dgvListado.Columns("FondoFijo").Width = 90
        End If
        If dgvListado.Columns.Contains("Cierre") Then
            dgvListado.Columns("Cierre").Visible = True
            dgvListado.Columns("Cierre").HeaderText = "Cierre"
            dgvListado.Columns("Cierre").Width = 80
        End If
        If dgvListado.Columns.Contains("Usuario") Then
            dgvListado.Columns("Usuario").Visible = True
            dgvListado.Columns("Usuario").HeaderText = "Usuario"
            dgvListado.Columns("Usuario").Width = 120
        End If

    End Sub

    Private Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        Dim criterio As String
        If dgvListado.Columns.Count = 0 Then Exit Sub

        criterio = "(left({bkpNove.Cierre}, 10)='" & dtpFecha.Value.ToString("dd/MM/yyyy") & "')"

        Process.Start(General.ReportesPath, "Proveedores LisNovBKP RecordSelectionFormula """ & criterio & """")
    End Sub
End Class

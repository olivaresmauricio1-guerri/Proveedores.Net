Imports System.Diagnostics.Eventing.Reader
Imports DSM = DataSourceManager.Lib.DataSourceManager

Partial Public Class frmResumen
    Inherits Form
    Private Shared instancia As frmResumen
    Private tablaProv As New DataTable()
    Private dtResumen As DataTable
    Private currentNroCuenta As Integer

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmResumen()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmResumen_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmResumen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        ConfigurarEstiloGrid(DgvProveedores)
        ConfigurarEstiloGrid(dgvDeta)
        CargarProveedores()
    End Sub

    Private Sub TxtBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtBuscar.TextChanged
        CargarProveedores(TxtBuscar.Text.Trim())
    End Sub

    Private Sub optTodos_CheckedChanged(sender As Object, e As EventArgs) Handles optTodos.CheckedChanged
        If optTodos.Checked AndAlso currentNroCuenta > 0 Then
            MostrarResumen(currentNroCuenta)
        End If
    End Sub

    Private Sub optImpago_CheckedChanged(sender As Object, e As EventArgs) Handles optImpago.CheckedChanged
        If optImpago.Checked AndAlso currentNroCuenta > 0 Then
            MostrarResumen(currentNroCuenta)
        End If
    End Sub
    Private Sub DgvListado_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvDeta.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.C Then
            CopiarDataGrid(dgvDeta, chkEncabezados.Checked)
            e.Handled = True
        End If

    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Dim Criterio As String = "({DetaCtaCte.NroCuenta}=" & currentNroCuenta & ")AND({DetaCtaCte.IDIMPUTACION}<>5)AND({DetaCtaCte.IDIMPUTACION}<>56)"
        Process.Start(General.ReportesPath, "Proveedores resumenprove RecordSelectionFormula " & Criterio)
    End Sub

    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Close()
    End Sub
    Private Sub CargarProveedores(Optional filtro As String = "")
        Dim sql As String = "SELECT NroCuenta, Nombre, CodContable, Rubro, Telefono, Cuit, Provincia FROM MaeCtaCte"
        Dim prms As Dictionary(Of String, Object) = Nothing
        If filtro <> "" Then
            Dim n As Integer
            If Integer.TryParse(filtro, n) Then
                sql &= " WHERE NroCuenta = @n"
                prms = CmdParams("@n", n)
            Else
                sql &= " WHERE Nombre LIKE '%' + @t + '%'"
                prms = CmdParams("@t", filtro)
            End If
        End If
        sql &= " ORDER BY Nombre"
        tablaProv = DSM.ExecuteQuery(DSM.Proveedores, sql, prms)
        DgvProveedores.DataSource = tablaProv
        ConfigurarListadoProveedores()
    End Sub

    Private Sub ConfigurarListadoProveedores()
        If DgvProveedores Is Nothing OrElse DgvProveedores.Columns.Count = 0 Then Return
        For Each col As DataGridViewColumn In DgvProveedores.Columns
            col.Visible = False
        Next
        If DgvProveedores.Columns.Contains("NroCuenta") Then
            DgvProveedores.Columns("NroCuenta").Visible = True
            DgvProveedores.Columns("NroCuenta").HeaderText = "NroCta"
            DgvProveedores.Columns("NroCuenta").Width = 80
        End If
        If DgvProveedores.Columns.Contains("Nombre") Then
            DgvProveedores.Columns("Nombre").Visible = True
            DgvProveedores.Columns("Nombre").HeaderText = "Proveedor"
            DgvProveedores.Columns("Nombre").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If
        If DgvProveedores.Columns.Contains("CodContable") Then
            DgvProveedores.Columns("CodContable").Visible = True
            DgvProveedores.Columns("CodContable").HeaderText = "CodContable"
            DgvProveedores.Columns("CodContable").Width = 100
        End If
        If DgvProveedores.Columns.Contains("Rubro") Then
            DgvProveedores.Columns("Rubro").Visible = True
            DgvProveedores.Columns("Rubro").HeaderText = "Rubro"
            DgvProveedores.Columns("Rubro").Width = 120
        End If
        If DgvProveedores.Columns.Contains("Telefono") Then
            DgvProveedores.Columns("Telefono").Visible = True
            DgvProveedores.Columns("Telefono").HeaderText = "Teléfono"
            DgvProveedores.Columns("Telefono").Width = 80
        End If
        If DgvProveedores.Columns.Contains("Cuit") Then
            DgvProveedores.Columns("Cuit").Visible = True
            DgvProveedores.Columns("Cuit").HeaderText = "CUIT/CUIL"
            DgvProveedores.Columns("Cuit").Width = 120
        End If
        If DgvProveedores.Columns.Contains("Provincia") Then
            DgvProveedores.Columns("Provincia").Visible = True
            DgvProveedores.Columns("Provincia").HeaderText = "Provincia"
            DgvProveedores.Columns("Provincia").Width = 120
        End If
    End Sub

    Private Sub DgvProveedores_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvProveedores.CellClick
        If e.RowIndex < 0 Then Return
        Dim row = DgvProveedores.Rows(e.RowIndex)
        If row Is Nothing OrElse row.Cells("NroCuenta").Value Is Nothing Then Return
        Dim nroCuenta As Integer = Convert.ToInt32(row.Cells("NroCuenta").Value)
        currentNroCuenta = nroCuenta
        MostrarResumen(nroCuenta)
    End Sub
    Private Sub lnkCopiar_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCopiar.LinkClicked
        CopiarDataGrid(dgvDeta, chkEncabezados.Checked)
    End Sub

    Private Sub DgvProveedores_SelectionChanged(sender As Object, e As EventArgs) Handles DgvProveedores.SelectionChanged
        If DgvProveedores.CurrentRow Is Nothing Then Return
        If DgvProveedores.CurrentRow.Cells("NroCuenta").Value Is Nothing Then Return
        Dim nroCuenta As Integer = Convert.ToInt32(DgvProveedores.CurrentRow.Cells("NroCuenta").Value)
        If nroCuenta = currentNroCuenta Then Return
        currentNroCuenta = nroCuenta
        MostrarResumen(nroCuenta)
    End Sub

    Private Sub dgvDeta_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDeta.CellDoubleClick
        If e.RowIndex < 0 Then Return
        Dim row = dgvDeta.Rows(e.RowIndex)
        If row Is Nothing Then Return
        Dim tipo As String = Convert.ToString(row.Cells("NombreComprobante").Value)
        If String.Equals(tipo, "Orden de Pago", StringComparison.OrdinalIgnoreCase) Then
            Dim nro As String = Convert.ToString(row.Cells("NroComprobante").Value)
            Dim path As String = "F:\PROVEEDORES\OP\" & "OrdenPago N " & nro & ".pdf"
            Try
                Process.Start(New ProcessStartInfo(path) With {.UseShellExecute = True})
            Catch
            End Try
        Else
            MessageBox.Show("Solo puede ver Ordenes de Pago")
        End If
    End Sub
    Private Sub MostrarResumen(nroCuenta As Integer)
        Dim infoDt = DSM.ExecuteQuery(DSM.Proveedores, "SELECT Nombre, Cuit, NroCuenta, ISNULL(ULTIMORESUMEN,0) AS SaldoAnterior, ISNULL(SALDOACTUAL,0) AS SaldoActual, ISNULL(SALDODTO,0) AS SaldoDtos, ISNULL(Comentario,'') AS Comentario FROM MaeCtaCte WHERE NroCuenta = @NroCuenta", CmdParams("@NroCuenta", nroCuenta))
        Dim saldoAnterior As Decimal = 0D
        Dim saldoActualDb As Decimal = 0D
        Dim saldoDtos As Decimal = 0D
        Dim nombreProv As String = String.Empty
        Dim cuitProv As String = String.Empty
        If infoDt IsNot Nothing AndAlso infoDt.Rows.Count > 0 Then
            Dim r = infoDt.Rows(0)
            nombreProv = Convert.ToString(r("Nombre"))
            cuitProv = Convert.ToString(r("Cuit"))
            saldoAnterior = Convert.ToDecimal(r("SaldoAnterior"))
            saldoActualDb = Convert.ToDecimal(r("SaldoActual"))
            saldoDtos = Convert.ToDecimal(r("SaldoDtos"))
            txtProveedor.Text = nombreProv
            txtCUIT.Text = cuitProv
            txtNroCuenta.Text = nroCuenta.ToString()
            txtUltimoResumen.Text = saldoAnterior.ToString("N2")
            txtSaldoActual2.Text = saldoActualDb.ToString("N2")
            txtSaldoDto.Text = saldoDtos.ToString("N2")
            TxtSaldoSinDoc.Text = (saldoActualDb - saldoDtos).ToString("N2")
            TxtSaldoActual.Text = (saldoActualDb - saldoDtos).ToString("N2")
            TxtObsv.Text = Convert.ToString(r("Comentario"))
        Else
            txtProveedor.Text = ""
            txtCUIT.Text = ""
            txtNroCuenta.Text = nroCuenta.ToString()
            txtUltimoResumen.Text = saldoAnterior.ToString("N2")
            txtSaldoActual2.Text = saldoActualDb.ToString("N2")
            txtSaldoDto.Text = saldoDtos.ToString("N2")
            TxtSaldoSinDoc.Text = (saldoActualDb - saldoDtos).ToString("N2")
            TxtSaldoActual.Text = (saldoActualDb - saldoDtos).ToString("N2")
            TxtObsv.Text = ""
        End If
        Dim sqlResumen As String = "SELECT idDetaCtaCte, NroFactura, NroComprobante, NombreComprobante, Fecha, Monto, CtaMonto, Cobrado, Comentario FROM DetaCtaCte "
        If optTodos.Checked Then
            sqlResumen &= "WHERE DetaCtaCte.NroCuenta = @NroCuenta  AND (IDIMPUTACION <> 5) AND (IDIMPUTACION <> 56) "
        Else
            sqlResumen &= " WHERE DetaCtaCte.NroCuenta = @NroCuenta AND (IDIMPUTACION in(1,2,10)) and cobrado = 0 "
        End If
        sqlResumen &= " ORDER BY Fecha, NroFactura"

        dtResumen = DSM.ExecuteQuery(DSM.Proveedores, sqlResumen, CmdParams("@NroCuenta", nroCuenta))
        dgvDeta.DataSource = dtResumen
        ConfiguraColListadoResumen()
    End Sub

    Private Sub ConfiguraColListadoResumen()
        If dgvDeta.Columns.Count = 0 Then Return
        For Each col As DataGridViewColumn In dgvDeta.Columns
            col.Visible = False
            col.ReadOnly = True
        Next


        If dgvDeta.Columns.Contains("IdDetaCtaCte") Then
            dgvDeta.Columns("IdDetaCtaCte").Visible = False
            dgvDeta.Columns("IdDetaCtaCte").HeaderText = "IdDetaCtaCte"
            dgvDeta.Columns("IdDetaCtaCte").Width = 8
            dgvDeta.Columns("IdDetaCtaCte").DisplayIndex = 0
        End If
        If dgvDeta.Columns.Contains("NroFactura") Then
            dgvDeta.Columns("NroFactura").Visible = True
            dgvDeta.Columns("NroFactura").HeaderText = "Factura"
            dgvDeta.Columns("NroFactura").Width = 80
            dgvDeta.Columns("NroFactura").DisplayIndex = 1
        End If
        If dgvDeta.Columns.Contains("NroComprobante") Then
            dgvDeta.Columns("NroComprobante").Visible = True
            dgvDeta.Columns("NroComprobante").HeaderText = "Comprobante"
            dgvDeta.Columns("NroComprobante").Width = 100
            dgvDeta.Columns("NroComprobante").DisplayIndex = 2
        End If
        If dgvDeta.Columns.Contains("NombreComprobante") Then
            dgvDeta.Columns("NombreComprobante").Visible = True
            dgvDeta.Columns("NombreComprobante").HeaderText = "Tipo Comprobante"
            dgvDeta.Columns("NombreComprobante").Width = 120
            dgvDeta.Columns("NombreComprobante").DisplayIndex = 3
        End If
        If dgvDeta.Columns.Contains("Fecha") Then
            dgvDeta.Columns("Fecha").Visible = True
            dgvDeta.Columns("Fecha").HeaderText = "Fecha"
            dgvDeta.Columns("Fecha").Width = 80
            dgvDeta.Columns("Fecha").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dgvDeta.Columns("Fecha").DisplayIndex = 4
        End If
        If dgvDeta.Columns.Contains("Monto") Then
            dgvDeta.Columns("Monto").Visible = True
            dgvDeta.Columns("Monto").HeaderText = "Monto"
            dgvDeta.Columns("Monto").Width = 120
            dgvDeta.Columns("Monto").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvDeta.Columns("Monto").DefaultCellStyle.Format = "N2"
            dgvDeta.Columns("Monto").DisplayIndex = 5
        End If
        If dgvDeta.Columns.Contains("CtaMonto") Then
            dgvDeta.Columns("CtaMonto").Visible = True
            dgvDeta.Columns("CtaMonto").HeaderText = "Cod Contable"
            dgvDeta.Columns("CtaMonto").Width = 50
            dgvDeta.Columns("CtaMonto").DisplayIndex = 6
            dgvDeta.Columns("CtaMonto").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End If

        If dgvDeta.Columns.Contains("Pagado") Then
            dgvDeta.Columns.Remove("Pagado")
        End If
        If dgvDeta.Columns.Contains("Comentario") Then
            dgvDeta.Columns("Comentario").Visible = True
            dgvDeta.Columns("Comentario").HeaderText = "Comentario"
            dgvDeta.Columns("Comentario").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            dgvDeta.Columns("Comentario").ReadOnly = False
            'DBGdeta.Columns("Comentario").DisplayIndex = 7
        End If
        Dim chk As New DataGridViewCheckBoxColumn()
        chk.Name = "Pagado"
        chk.HeaderText = "Pagado"
        chk.DataPropertyName = "Pagado"
        chk.Width = 50
        chk.DisplayIndex = 7
        dgvDeta.Columns.Add(chk)
    End Sub

    Private Sub DdgvDeta_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDeta.CellEndEdit
        Dim r = dgvDeta.Rows(e.RowIndex)
        Dim idDetaCtaCte = Convert.ToInt32(r.Cells("idDetaCtaCte").Value)
        Dim comentario = r.Cells("Comentario").Value?.ToString()

        Dim sql = "UPDATE DetaCtaCte SET Comentario = @Comentario WHERE IdDetaCtaCte = @Codigo"
        Dim p = CmdParams(
            "@Comentario", If(String.IsNullOrWhiteSpace(comentario), DBNull.Value, comentario),
            "@Codigo", idDetaCtaCte
        )
        DSM.Execute(DSM.Proveedores, sql, p, True)
    End Sub

End Class
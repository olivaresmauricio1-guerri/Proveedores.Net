Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmResumenHistorico
    Inherits Form
    Private tablaProv As DataTable
    Private dtResumen As DataTable
    Private currentNroCuenta As Integer
    Private currentNombreProv As String
    Private Shared instancia As frmResumenHistorico
    Dim saldoAnterior As Decimal = 0D
    Dim saldoActualDb As Decimal = 0D
    Dim saldoDtos As Decimal = 0D
    Private _suspenderAccionFiltros As Boolean = False

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmResumenHistorico()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmResumenHistorico_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub
    Private Sub frmResumenHistorico_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConfigurarEstiloGrid(DgvProveedores)
        ConfigurarEstiloGrid(DgvDeta)
        DgvDeta.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect
        CargarProveedores()
        dtHasta.Value = Date.Now
        dtDesde.Value = New Date(Date.Now.Year, 1, 1)
    End Sub

    Private Sub TxtBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtBuscar.TextChanged
        CargarProveedores(TxtBuscar.Text.Trim)
    End Sub

    Private Sub DgvDeta_KeyDown(sender As Object, e As KeyEventArgs) Handles DgvDeta.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.C Then
            CopiarDataGrid(DgvDeta, chkEncabezados.Checked)
            e.Handled = True
        End If
    End Sub
    Private Sub lnkCopiar_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCopiar.LinkClicked
        CopiarDataGrid(DgvDeta, chkEncabezados.Checked)
    End Sub
    Private Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        Dim Criterio As String = "({wresumenanual.usuario}='" & UsuarioActual & "')"
        'Dim Prove As String = Replace(currentNombreProv, " ", "_")
        Dim Anterior As Decimal = Convert.ToDecimal(txtSaldoAnterior.Text)
        Dim comando As String = $"Proveedores resumenanual RecordSelectionFormula " & Criterio & $" proveparametro ""{currentNombreProv}"" anteriorparametro {Anterior}"

        Process.Start(General.ReportesPath, comando)
    End Sub

    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Close()
    End Sub

    Private Sub CargarProveedores(Optional filtro As String = "")
        Dim sql As String = "SELECT NroCuenta, Nombre, CodContable, Rubro, Telefono, Cuit, Provincia FROM MaeCtaCte"
        Dim parametros As Dictionary(Of String, Object) = Nothing
        If filtro <> "" Then
            Dim n As Integer
            If Integer.TryParse(filtro, n) Then
                sql &= " WHERE NroCuenta = @n"
                parametros = CmdParams("@n", n)
            Else
                sql &= " WHERE Nombre LIKE '%' + @t + '%'"
                parametros = CmdParams("@t", filtro)
            End If
        End If
        sql &= " ORDER BY Nombre"
        tablaProv = DSM.ExecuteQuery(DSM.Proveedores, sql, parametros)
        DgvProveedores.DataSource = tablaProv
        ConfigurarListadoProveedores()
    End Sub

    Private Sub DgvProveedores_SelectionChanged(sender As Object, e As EventArgs) Handles DgvProveedores.SelectionChanged
        If DgvProveedores.CurrentRow Is Nothing Then Return
        If DgvProveedores.CurrentRow.Cells("NroCuenta").Value Is Nothing Then Return
        Dim nroCuenta As Integer = Convert.ToInt32(DgvProveedores.CurrentRow.Cells("NroCuenta").Value)
        currentNombreProv = DgvProveedores.CurrentRow.Cells("Nombre").Value.ToString()
        If nroCuenta = currentNroCuenta Then Return
        currentNroCuenta = nroCuenta
        TxtNroCuenta.Text = nroCuenta.ToString()
        MostrarResumen(nroCuenta)
    End Sub
    Private Sub MostrarResumen(nroCuenta As Integer)
        Dim infoDt = DSM.ExecuteQuery(DSM.Proveedores, "SELECT Nombre, Cuit, NroCuenta, ISNULL(ULTIMORESUMEN,0) AS SaldoAnterior, ISNULL(SALDOACTUAL,0) AS SaldoActual, ISNULL(SALDODTO,0) AS SaldoDtos FROM MaeCtaCte WHERE NroCuenta = @NroCuenta", CmdParams("@NroCuenta", nroCuenta))

        Dim cuitProv As String = String.Empty
        If infoDt IsNot Nothing AndAlso infoDt.Rows.Count > 0 Then
            Dim r = infoDt.Rows(0)
            cuitProv = Convert.ToString(r("Cuit"))
            saldoActualDb = Convert.ToDecimal(r("SaldoActual"))
            saldoDtos = Convert.ToDecimal(r("SaldoDtos"))
        End If
        txtCUIT.Text = cuitProv
        txtSaldoMaestro.Text = (saldoActualDb - saldoDtos).ToString("N2")
        currentNombreProv = DgvProveedores.CurrentRow.Cells("Nombre").Value.ToString()

        Dim sqlSaldoAnterior As String = "SELECT SUM(CASE " &
                                         "WHEN idimputacion < 50 AND Fecha < @Desde AND ctamonto = '2.1.1' AND idimputacion <> 6 THEN monto " &
                                         "WHEN idimputacion >= 50 AND Fecha < @Desde THEN -monto " &
                                         "ELSE 0 END) AS SaldoAnterior " &
                                         "FROM (" &
                                         "SELECT idimputacion, fecha, monto, ctamonto FROM DetaCtaCteAnual WHERE NroCuenta = @NroCuenta AND IDIMPUTACION NOT IN (5,56) " &
                                         "UNION ALL " &
                                         "SELECT idimputacion, fecha, monto, ctamonto FROM DetaCtaCte WHERE NroCuenta = @NroCuenta AND IDIMPUTACION NOT IN (5,56)" &
                                         ") T"
        Dim dtSaldoAnterior As DataTable = DSM.ExecuteQuery(DSM.Proveedores, sqlSaldoAnterior, CmdParams("@NroCuenta", nroCuenta, "@Desde", dtDesde.Value.Date))
        If dtSaldoAnterior IsNot Nothing AndAlso dtSaldoAnterior.Rows.Count > 0 AndAlso Not IsDBNull(dtSaldoAnterior.Rows(0)("SaldoAnterior")) Then
            saldoAnterior = Convert.ToDecimal(dtSaldoAnterior.Rows(0)("SaldoAnterior"))
        Else
            saldoAnterior = 0D
        End If
        txtSaldoAnterior.Text = saldoAnterior.ToString("N2")

        Dim sqlDel As String = "DELETE FROM wresumenanual WHERE usuario = @Usuario"
        DSM.Execute(DSM.Proveedores, sqlDel, CmdParams("@Usuario", General.UsuarioActual), True)

        Dim sqlIns As String = ""
        sqlIns = "INSERT INTO wresumenanual (NroCuenta ,NroFactura, NroComprobante, NombreComprobante, Condicion, Fecha, IdImputacion, CtaMonto, monto, comentario, usuario, pagado, puntodeventa) " &
                 "SELECT NroCuenta, NroFactura, NroComprobante, NombreComprobante, Condicion, Fecha, IdImputacion, CtaMonto, Monto, Comentario, @Usuario, Cobrado, Puntodeventa FROM DetaCtaCte " &
                 "WHERE NroCuenta = @NroCuenta AND Fecha BETWEEN @Desde AND @Hasta AND IDIMPUTACION NOT IN (5,56) " &
                 "UNION ALL " &
                 "SELECT NroCuenta, NroFactura, NroComprobante, NombreComprobante, Condicion, Fecha, IdImputacion, CtaMonto, Monto, Comentario, @Usuario, Cobrado, Puntodeventa FROM DetaCtaCteAnual " &
                 "WHERE NroCuenta = @NroCuenta AND Fecha BETWEEN @Desde AND @Hasta AND IDIMPUTACION NOT IN (5,56)"
        DSM.Execute(DSM.Proveedores, sqlIns, CmdParams("@Usuario", General.UsuarioActual, "@NroCuenta", nroCuenta, "@Desde", dtDesde.Value.Date, "@Hasta", dtHasta.Value.Date), True)

        Dim saldoCalc As Decimal = saldoAnterior
        Dim dtW = DSM.ExecuteQuery(DSM.Proveedores, "SELECT NroCuenta, NroFactura, NroComprobante, Fecha, IdImputacion, CtaMonto, Monto FROM wresumenanual WHERE usuario = @Usuario ORDER BY Fecha, IdImputacion, NroFactura", CmdParams("@Usuario", General.UsuarioActual))
        If dtW IsNot Nothing AndAlso dtW.Rows.Count > 0 Then
            For Each rw As DataRow In dtW.Rows
                Dim idImp As Integer = Convert.ToInt32(rw("IdImputacion"))
                Dim cta As String = Convert.ToString(rw("CtaMonto"))
                Dim monto As Decimal = Convert.ToDecimal(rw("Monto"))
                Dim autocancelable As Integer = 0
                Dim dtPlan = DSM.ExecuteQuery(DSM.Contabilidad, "SELECT autocancelable FROM plancuentas WHERE codcontable = @cod", CmdParams("@cod", cta))
                If dtPlan IsNot Nothing AndAlso dtPlan.Rows.Count > 0 Then
                    autocancelable = Convert.ToInt32(dtPlan.Rows(0)("autocancelable"))
                End If
                Dim debe As Decimal = 0D
                Dim haber As Decimal = 0D
                If idImp < 50 Then
                    haber = monto
                    If cta = "2.1.1" AndAlso idImp <> 6 Then
                        saldoCalc += haber
                    End If
                Else
                    debe = monto
                    If autocancelable = 0 Then
                        saldoCalc -= debe
                    End If
                End If
                Dim sqlUpd As String = "UPDATE wresumenanual SET debe=@Debe, haber=@Haber, saldo=@Saldo WHERE usuario=@Usuario AND NroCuenta=@NroCuenta AND NroFactura=@NroFactura AND NroComprobante=@NroComprobante AND Fecha=@Fecha"
                DSM.Execute(DSM.Proveedores, sqlUpd, CmdParams(
                    "@Debe", debe,
                    "@Haber", haber,
                    "@Saldo", saldoCalc,
                    "@Usuario", General.UsuarioActual,
                    "@NroCuenta", rw("NroCuenta"),
                    "@NroFactura", rw("NroFactura"),
                    "@NroComprobante", rw("NroComprobante"),
                    "@Fecha", Convert.ToDateTime(rw("Fecha"))
                ), True)
            Next
        End If

        Dim sqlSel As String = "SELECT Fecha, Puntodeventa AS PV, NroFactura, NroComprobante, NombreComprobante, CtaMonto, Comentario, pagado AS Cobrado, Condicion, Debe, Haber, Saldo FROM wresumenanual WHERE usuario = @Usuario ORDER BY Fecha, IdImputacion, NroFactura"
        dtResumen = DSM.ExecuteQuery(DSM.Proveedores, sqlSel, CmdParams("@Usuario", General.UsuarioActual))
        DgvDeta.DataSource = dtResumen

        ConfigurarGridDetalle()
        CalcularTotales()
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
            DgvProveedores.Columns("CodContable").HeaderText = "Cuenta"
            DgvProveedores.Columns("CodContable").Width = 100
        End If
        If DgvProveedores.Columns.Contains("Rubro") Then
            DgvProveedores.Columns("Rubro").Visible = True
            DgvProveedores.Columns("Rubro").HeaderText = "Rubro"
            DgvProveedores.Columns("Rubro").Width = 150
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


    Private Sub ConfigurarGridDetalle()
        If DgvDeta.Columns.Count = 0 Then Return
        For Each col As DataGridViewColumn In DgvDeta.Columns
            col.Visible = False
        Next
        If DgvDeta.Columns.Contains("idDetaCtaCte") Then
            DgvDeta.Columns("idDetaCtaCte").Visible = False
        End If
        If DgvDeta.Columns.Contains("Fecha") Then
            DgvDeta.Columns("Fecha").Visible = True
            DgvDeta.Columns("Fecha").HeaderText = "Fecha"
            DgvDeta.Columns("Fecha").Width = 60
            DgvDeta.Columns("Fecha").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvDeta.Columns("Fecha").DisplayIndex = 0
        End If
        If DgvDeta.Columns.Contains("NroFactura") Then
            DgvDeta.Columns("NroFactura").Visible = True
            DgvDeta.Columns("NroFactura").HeaderText = "NroFactura"
            DgvDeta.Columns("NroFactura").Width = 80
            DgvDeta.Columns("NroFactura").DisplayIndex = 1
        End If
        If DgvDeta.Columns.Contains("PV") Then
            DgvDeta.Columns("PV").Visible = True
            DgvDeta.Columns("PV").HeaderText = "P.V."
            DgvDeta.Columns("PV").Width = 40
            DgvDeta.Columns("PV").DisplayIndex = 2
        End If
        If DgvDeta.Columns.Contains("NroComprobante") Then
            DgvDeta.Columns("NroComprobante").Visible = True
            DgvDeta.Columns("NroComprobante").HeaderText = "NroComprob"
            DgvDeta.Columns("NroComprobante").Width = 90
            DgvDeta.Columns("NroComprobante").DisplayIndex = 3
        End If
        If DgvDeta.Columns.Contains("NombreComprobante") Then
            DgvDeta.Columns("NombreComprobante").Visible = True
            DgvDeta.Columns("NombreComprobante").HeaderText = "Comprobante"
            DgvDeta.Columns("NombreComprobante").Width = 110
            DgvDeta.Columns("NombreComprobante").DisplayIndex = 4
        End If
        If DgvDeta.Columns.Contains("Cobrado") Then
            DgvDeta.Columns("Cobrado").Visible = True
            DgvDeta.Columns("Cobrado").HeaderText = "Pagado"
            DgvDeta.Columns("Cobrado").Width = 50
            DgvDeta.Columns("Cobrado").DisplayIndex = 5
        End If
        If DgvDeta.Columns.Contains("Condicion") Then
            DgvDeta.Columns("Condicion").Visible = True
            DgvDeta.Columns("Condicion").HeaderText = "Condicion"
            DgvDeta.Columns("Condicion").Width = 100
            DgvDeta.Columns("Condicion").DisplayIndex = 6
        End If
        If DgvDeta.Columns.Contains("CtaMonto") Then
            DgvDeta.Columns("CtaMonto").Visible = True
            DgvDeta.Columns("CtaMonto").HeaderText = "Cuenta"
            DgvDeta.Columns("CtaMonto").Width = 70
            DgvDeta.Columns("CtaMonto").DisplayIndex = 7
        End If
        If DgvDeta.Columns.Contains("Debe") Then
            DgvDeta.Columns("Debe").Visible = True
            DgvDeta.Columns("Debe").HeaderText = "Debe"
            DgvDeta.Columns("Debe").Width = 100
            DgvDeta.Columns("Debe").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvDeta.Columns("Debe").DefaultCellStyle.Format = "N2"
            DgvDeta.Columns("Debe").DisplayIndex = 8
        End If
        If DgvDeta.Columns.Contains("Haber") Then
            DgvDeta.Columns("Haber").Visible = True
            DgvDeta.Columns("Haber").HeaderText = "Haber"
            DgvDeta.Columns("Haber").Width = 100
            DgvDeta.Columns("Haber").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvDeta.Columns("Haber").DefaultCellStyle.Format = "N2"
            DgvDeta.Columns("Haber").DisplayIndex = 9
        End If
        If DgvDeta.Columns.Contains("Saldo") Then
            DgvDeta.Columns("Saldo").Visible = True
            DgvDeta.Columns("Saldo").HeaderText = "Saldo"
            DgvDeta.Columns("Saldo").Width = 100
            DgvDeta.Columns("Saldo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvDeta.Columns("Saldo").DefaultCellStyle.Format = "N2"
            DgvDeta.Columns("Saldo").DisplayIndex = 10
        End If
        If DgvDeta.Columns.Contains("Comentario") Then
            DgvDeta.Columns("Comentario").Visible = True
            DgvDeta.Columns("Comentario").HeaderText = "Comentario"
            DgvDeta.Columns("Comentario").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DgvDeta.Columns("Comentario").DisplayIndex = 11
        End If
    End Sub

    Private Sub CalcularTotales()
        Dim debeTotal As Decimal = 0D
        Dim haberTotal As Decimal = 0D
        If dtResumen Is Nothing Then Return
        For Each r As DataRow In dtResumen.Rows
            If Not IsDBNull(r("Debe")) Then debeTotal += Convert.ToDecimal(r("Debe"))
            If Not IsDBNull(r("Haber")) Then haberTotal += Convert.ToDecimal(r("Haber"))
        Next
        txtDebe.Text = debeTotal.ToString("N2")
        txtHaber.Text = haberTotal.ToString("N2")
    End Sub

    Private Sub DgvDeta_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DgvDeta.CellEndEdit
        If e.RowIndex < 0 Then Return
        Dim r = DgvDeta.Rows(e.RowIndex)
        If r Is Nothing Then Return
        Dim comentario = r.Cells("Comentario").Value?.ToString()
        Dim nroCuenta = Convert.ToInt32(TxtNroCuenta.Text)
        Dim nroFactura = Convert.ToInt32(r.Cells("NroFactura").Value)
        Dim nroComprob = Convert.ToInt32(r.Cells("NroComprobante").Value)
        Dim fecha As Date = Convert.ToDateTime(r.Cells("Fecha").Value)

        Dim sql = "UPDATE DetaCtaCte SET Comentario = @Comentario WHERE NroCuenta=@NroCuenta AND NroFactura=@NroFactura AND NroComprobante=@NroComprobante AND Fecha=@Fecha;" &
                  "IF @@ROWCOUNT = 0 UPDATE DetaCtaCteAnual SET Comentario = @Comentario WHERE NroCuenta=@NroCuenta AND NroFactura=@NroFactura AND NroComprobante=@NroComprobante AND Fecha=@Fecha;"
        Dim p = CmdParams(
            "@Comentario", If(String.IsNullOrWhiteSpace(comentario), DBNull.Value, comentario),
            "@NroCuenta", nroCuenta,
            "@NroFactura", nroFactura,
            "@NroComprobante", nroComprob,
            "@Fecha", fecha
        )
        DSM.Execute(DSM.Proveedores, sql, p, True)
    End Sub

    Private Sub DgvDeta_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvDeta.CellDoubleClick
        If e.RowIndex < 0 Then Return
        Dim row = DgvDeta.Rows(e.RowIndex)
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

End Class

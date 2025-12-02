Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmBuscaFactura
    Inherits Form
    Private Shared instancia As frmBuscaFactura
    Private dtResult As DataTable
    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmBuscaFactura()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmBuscaFactura_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmBuscaFactura_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        optCorriente.Checked = True
        BuscaFactura()
        ConfigurarEstiloGrid(DgvBusca)
    End Sub
    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Close()
    End Sub

    Private Sub lnkCopiar_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCopiar.LinkClicked
        CopiarDataGrid(DgvBusca, chkEncabezados.Checked)
    End Sub

    Private Sub TxtBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtBuscar.TextChanged
        BuscaFactura(TxtBuscar.Text.Trim())
    End Sub
    Private Sub optCorriente_CheckedChanged(sender As Object, e As EventArgs) Handles optCorriente.CheckedChanged
        BuscaFactura(TxtBuscar.Text.Trim())
    End Sub

    Private Sub optAnual_CheckedChanged(sender As Object, e As EventArgs) Handles optAnual.CheckedChanged
        BuscaFactura(TxtBuscar.Text.Trim())
    End Sub
    Private Sub BuscaFactura(Optional filtro As String = "")
        Dim tabla As String = If(optCorriente.Checked, "DetaCtaCte", "DetaCtaCteAnual")
        Dim nFactura As Integer
        Dim sql As String
        Dim parametros As Dictionary(Of String, Object) = Nothing

        sql = $"SELECT TOP (100) * FROM {tabla} "
        If filtro <> "" Then
            If Integer.TryParse(filtro, nFactura) AndAlso nFactura > 0 Then
                sql &= " WHERE NroFactura = @val OR NroComprobante = @val "
                parametros = CmdParams("@val", nFactura)
            End If
        End If
        sql &= " ORDER BY Fecha"
        dtResult = DSM.ExecuteQuery(DSM.Proveedores, sql, parametros)
        DgvBusca.DataSource = dtResult
        ConfigurarGrid()
    End Sub
    Private Sub ConfigurarGrid()
        If DgvBusca.Columns.Count = 0 Then Return
        For Each col As DataGridViewColumn In DgvBusca.Columns
            col.Visible = False
        Next
        If DgvBusca.Columns.Contains("Fecha") Then
            DgvBusca.Columns("Fecha").Visible = True
            DgvBusca.Columns("Fecha").HeaderText = "Fecha"
            DgvBusca.Columns("Fecha").Width = 80
        End If
        If DgvBusca.Columns.Contains("NroFactura") Then
            DgvBusca.Columns("NroFactura").Visible = True
            DgvBusca.Columns("NroFactura").HeaderText = "NroFactura"
            DgvBusca.Columns("NroFactura").Width = 100
        End If
        If DgvBusca.Columns.Contains("NroComprobante") Then
            DgvBusca.Columns("NroComprobante").Visible = True
            DgvBusca.Columns("NroComprobante").HeaderText = "NroComprob"
            DgvBusca.Columns("NroComprobante").Width = 100
        End If
        If DgvBusca.Columns.Contains("NombreComprobante") Then
            DgvBusca.Columns("NombreComprobante").Visible = True
            DgvBusca.Columns("NombreComprobante").HeaderText = "Comprobante"
            DgvBusca.Columns("NombreComprobante").Width = 140
        End If
        If DgvBusca.Columns.Contains("Monto") Then
            DgvBusca.Columns("Monto").Visible = True
            DgvBusca.Columns("Monto").HeaderText = "Monto"
            DgvBusca.Columns("Monto").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvBusca.Columns("Monto").DefaultCellStyle.Format = "N2"
            DgvBusca.Columns("Monto").Width = 110
        End If
        If DgvBusca.Columns.Contains("CtaMonto") Then
            DgvBusca.Columns("CtaMonto").Visible = True
            DgvBusca.Columns("CtaMonto").HeaderText = "Cuenta"
            DgvBusca.Columns("CtaMonto").Width = 90
        End If
        If DgvBusca.Columns.Contains("Comentario") Then
            DgvBusca.Columns("Comentario").Visible = True
            DgvBusca.Columns("Comentario").HeaderText = "Comentario"
            DgvBusca.Columns("Comentario").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If
    End Sub

End Class

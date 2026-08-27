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
        ConfigurarGrid()
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
        If TxtBuscar.Text.Trim() <> "" Then
            BuscaFactura(TxtBuscar.Text.Trim())
        Else
            BuscaFactura()
        End If
    End Sub
    Private Sub BuscaFactura(Optional filtro As String = "")

        Dim tabla As String = If(optCorriente.Checked, "DetaCtaCte", "DetaCtaCteAnual")
        Dim nFactura As Integer
        Dim fecha As DateTime
        Dim sql As String
        Dim parametros As Dictionary(Of String, Object) = Nothing

        sql = $"SELECT TOP (100) Fecha, NroFactura, NroComprobante, NombreComprobante, CtaMonto, Monto, Comentario FROM {tabla}"

        Dim condiciones As New List(Of String)

        If filtro <> "" Then

            ' Buscar por número de factura/comprobante
            If Integer.TryParse(filtro, nFactura) AndAlso nFactura > 0 Then

                condiciones.Add("(NroFactura = @val OR NroComprobante = @val)")
                parametros = CmdParams("@val", nFactura)

                ' Buscar por fecha
            ElseIf DateTime.TryParse(filtro, fecha) Then

                condiciones.Add("Fecha >= @fecha AND Fecha < DATEADD(DAY, 1, @fecha)")
                parametros = CmdParams("@fecha", fecha.Date)

                ' Buscar por tipo de comprobante
            Else

                condiciones.Add("NombreComprobante LIKE @val")
                parametros = CmdParams("@val", "%" & filtro & "%")

            End If

        End If

        If condiciones.Count > 0 Then
            sql &= " WHERE " & String.Join(" AND ", condiciones)
        End If

        sql &= " ORDER BY Fecha DESC"

        dtResult = DSM.ExecuteQuery(DSM.Proveedores, sql, parametros)

        DgvBusca.DataSource = dtResult
        ConfigurarGrid()

    End Sub
    Private Sub ConfigurarGrid()
        If DgvBusca.Columns.Count = 0 Then Return

        If DgvBusca.Columns.Contains("Fecha") Then
            DgvBusca.Columns("Fecha").Width = 80
            DgvBusca.Columns("Fecha").HeaderText = "Fecha"
        End If
        If DgvBusca.Columns.Contains("NroFactura") Then
            DgvBusca.Columns("NroFactura").Width = 100
            DgvBusca.Columns("NroFactura").HeaderText = "NroFactura"
        End If
        If DgvBusca.Columns.Contains("NroComprobante") Then
            DgvBusca.Columns("NroComprobante").Width = 100
            DgvBusca.Columns("NroComprobante").HeaderText = "NroComprob"
        End If
        If DgvBusca.Columns.Contains("NombreComprobante") Then
            DgvBusca.Columns("NombreComprobante").Width = 140
            DgvBusca.Columns("NombreComprobante").HeaderText = "Comprobante"
        End If
        If DgvBusca.Columns.Contains("CtaMonto") Then
            DgvBusca.Columns("CtaMonto").Width = 90
            DgvBusca.Columns("CtaMonto").HeaderText = "Cuenta"
        End If
        If DgvBusca.Columns.Contains("Monto") Then
            DgvBusca.Columns("Monto").Width = 110
            DgvBusca.Columns("Monto").HeaderText = "Monto"
            DgvBusca.Columns("Monto").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvBusca.Columns("Monto").DefaultCellStyle.Format = "N2"
        End If
        If DgvBusca.Columns.Contains("Comentario") Then
            DgvBusca.Columns("Comentario").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DgvBusca.Columns("Comentario").HeaderText = "Comentario"
        End If
    End Sub

End Class

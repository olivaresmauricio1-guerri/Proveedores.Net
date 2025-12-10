Imports DSM = DataSourceManager.Lib.DataSourceManager
Public Class frmListaComprobantes
    Inherits Form
    Private Shared instancia As frmListaComprobantes

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmListaComprobantes()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmListaComprobantes_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub
    Private Sub frmListaComprobantes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarCombosCtes(cmbTipoCte, "DetaCtaCteAnual", "NombreComprobante", "NombreComprobante")
    End Sub
    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Close()
    End Sub

    Private Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        Dim Criterio As String
        If cmbTipoCte Is Nothing OrElse cmbTipoCte.SelectedIndex < 0 Then
            MsgBox("Debe seleccionar un tipo de comprobante.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If cmbTipoCte.Text = "Factura" And chkImpagas.Checked Then
            Criterio = "({DetaCtaCte.NombreComprobante}='" & cmbTipoCte.Text & "')AND({DetaCtaCte.Cobrado}=false)"
        Else
            Criterio = "({DetaCtaCte.NombreComprobante}='" & cmbTipoCte.Text & "')"
        End If

        Process.Start(General.ReportesPath, "Proveedores lstcpte RecordSelectionFormula """ & Criterio & """")
    End Sub
End Class
Imports System.Globalization
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmOrdenPago
    Private Shared instancia As frmOrdenPago

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

    End Sub
End Class
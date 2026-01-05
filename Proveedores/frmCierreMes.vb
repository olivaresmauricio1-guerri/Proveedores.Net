Imports DSM = DataSourceManager.Lib.DataSourceManager
Imports DBM = DataSourceManager.Lib.DatabaseManager
Imports System.Runtime.CompilerServices

Public Class frmCierreMes
    Private Shared instancia As frmCierreMes

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmCierreMes()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmCierreMes_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmCierreMes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpFechaHasta.Value = Date.Now
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs)
        btnCerrar.Enabled = False
        btnSalir.Enabled = False
        Actualizar()
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs)
        Close()
    End Sub

    Private Sub Actualizar()
        ' en construccion
        MessageBox.Show("En construccion")
    End Sub
End Class
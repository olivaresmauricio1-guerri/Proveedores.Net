Imports DSM = DataSourceManager.Lib.DataSourceManager
Imports DBM = DataSourceManager.Lib.DatabaseManager
Imports System.Runtime.CompilerServices

Public Class frmActualiza
    Private Shared instancia As frmActualiza

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmActualiza()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmActualiza_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmActualiza_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpFechaHasta.Value = Date.Now
    End Sub
    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        btnActualizar.Enabled = False
        btnSalir.Enabled = False
        Actualizar()
    End Sub

    Private Sub Actualizar()
        ' en construccion
        MessageBox.Show("En construccion")
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class
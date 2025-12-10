Imports DSM = DataSourceManager.Lib.DataSourceManager
Public Class frmListadoNovedades
    Private Shared instancia As frmListadoNovedades

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmListadoNovedades()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmlistadonovedades_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Close()
    End Sub

    Private Sub frmListadoNovedades_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarComboSucursales()
    End Sub

    Private Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        Dim Criterio As String = ""
        If cmbSucursales Is Nothing OrElse cmbSucursales.SelectedIndex < 0 Then
            MsgBox("Debe seleccionar una Sucursal", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If cmbSucursales.Text <> "(Todas)" Then
            Criterio = "({NoveCtaCte.Sucursal}='" & cmbSucursales.Text & "')"
        End If

        Process.Start(General.ReportesPath, "Proveedores lisnov2 RecordSelectionFormula """ & Criterio & """")

    End Sub
    Private Sub CargarComboSucursales()
        Dim sql As String = "Select * from Sucursales order by Descripcion"
        Dim sucursales As DataTable = DSM.ExecuteQuery(DSM.Proveedores, sql)

        cmbSucursales.DataSource = sucursales
        cmbSucursales.DisplayMember = "Descripcion"
        cmbSucursales.ValueMember = "Codigo"

        cmbSucursales.SelectedValue = 7
    End Sub
End Class
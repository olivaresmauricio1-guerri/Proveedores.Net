Public Class frmSeleccionSolicitudCompraUso
    Private servicio As SolicitudCompraUsoService
    Public Property IdSeleccionado As Integer?

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(datos As SolicitudCompraUsoService)
        Me.New()
        servicio = datos
    End Sub

    Private Sub Ventana_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If ComprasUsoDiseno.EstaEnDiseno(Me) Then Return
        If servicio Is Nothing Then servicio = New SolicitudCompraUsoService()
        Buscar()
    End Sub

    Private Sub Buscar()
        Try
            GridSolicitudes.DataSource = servicio.Buscar(TxtBuscar.Text)
            BtnAbrir.Enabled = GridSolicitudes.Rows.Count > 0
        Catch ex As Exception
            GridSolicitudes.DataSource = Nothing
            BtnAbrir.Enabled = False
            MessageBox.Show(Me, ex.Message, "Solicitudes", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        Buscar()
    End Sub

    Private Sub BtnAbrir_Click(sender As Object, e As EventArgs) Handles BtnAbrir.Click
        AbrirSeleccion()
    End Sub

    Private Sub GridSolicitudes_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridSolicitudes.CellDoubleClick
        If e.RowIndex >= 0 Then AbrirSeleccion()
    End Sub

    Private Sub AbrirSeleccion()
        If GridSolicitudes.CurrentRow Is Nothing Then Return
        IdSeleccionado = Convert.ToInt32(GridSolicitudes.CurrentRow.Cells("IdSolicitud").Value)
        DialogResult = DialogResult.OK
        Close()
    End Sub
End Class

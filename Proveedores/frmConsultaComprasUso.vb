Imports System.Data
Imports System.Linq

Public Class frmConsultaComprasUso
    Private Shared instancia As frmConsultaComprasUso
    Private servicio As SolicitudCompraUsoService
    Private seleccionado As SolicitudCompraUsoDocumento
    Private cargando As Boolean
    Private inicializado As Boolean

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(datos As SolicitudCompraUsoService)
        Me.New()
        servicio = datos
    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)
        If Not ComprasUsoDiseno.EstaEnDiseno(Me) AndAlso Not inicializado Then
            inicializado = True
            If servicio Is Nothing Then servicio = New SolicitudCompraUsoService()
            CmbEstado.SelectedItem = "TODOS"
            AddHandler BtnBuscar.Click, Sub() ActualizarListado()
            AddHandler GridSolicitudes.SelectionChanged, AddressOf CambiarSeleccion
            AddHandler GridCotizaciones.SelectionChanged, Sub() BtnVerCotizacion.Enabled = GridCotizaciones.CurrentRow IsNot Nothing
            AddHandler GridCotizaciones.CellFormatting, AddressOf FormatearCotizacion
            AddHandler GridCotizaciones.CellDoubleClick, Sub(sender, celda)
                                                             If celda.RowIndex >= 0 Then VerCotizacion()
                                                         End Sub
            AddHandler BtnVerCotizacion.Click, Sub() VerCotizacion()
            LimpiarDetalle()
            CargarSectores()
            ActualizarListado()
        End If
        MyBase.OnLoad(e)
    End Sub

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then instancia = New frmConsultaComprasUso() With {.MdiParent = mdiParent}
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles BtnCerrar.Click
        Close()
    End Sub

    Private Sub Ventana_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If instancia Is Me Then instancia = Nothing
    End Sub

    Private Sub CargarSectores()
        Try
            Dim anterior = If(CmbSector.SelectedValue Is Nothing, 0, Convert.ToInt32(CmbSector.SelectedValue))
            Dim datos = servicio.SectoresConsulta()
            Dim todos = datos.NewRow()
            todos("IdSector") = 0
            todos("Descripcion") = "Todos"
            datos.Rows.InsertAt(todos, 0)
            CmbSector.DataSource = Nothing
            CmbSector.DisplayMember = "Descripcion"
            CmbSector.ValueMember = "IdSector"
            CmbSector.DataSource = datos
            CmbSector.SelectedValue = anterior
            If CmbSector.SelectedIndex < 0 Then CmbSector.SelectedIndex = 0
        Catch ex As Exception
            MessageBox.Show(Me, "No se pudieron cargar los sectores. " & ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function LeerFiltros() As SolicitudCompraUsoFiltro
        Dim sector = If(CmbSector.SelectedValue Is Nothing, 0, Convert.ToInt32(CmbSector.SelectedValue))
        Return New SolicitudCompraUsoFiltro With {
            .Desde = If(DtpDesde.Checked, CType(DtpDesde.Value.Date, Date?), Nothing),
            .Hasta = If(DtpHasta.Checked, CType(DtpHasta.Value.Date, Date?), Nothing),
            .IdSector = If(sector > 0, CType(sector, Integer?), Nothing),
            .Estado = If(CmbEstado.Text = "TODOS", "", CmbEstado.Text),
            .Texto = TxtBuscar.Text.Trim(), .Solicitante = TxtSolicitante.Text.Trim()}
    End Function

    Private Sub ActualizarListado()
        Dim idAnterior = If(seleccionado Is Nothing, 0, seleccionado.IdSolicitud)
        Try
            UseWaitCursor = True
            BtnBuscar.Enabled = False
            Dim datos = servicio.Consultar(LeerFiltros())
            VincularListado(datos, idAnterior)
            LblResultado.Text = datos.Rows.Count & " solicitud(es). Seleccione una para revisar su detalle, cotizaciones e historial."
            MostrarSeleccion()
        Catch ex As Exception
            VincularListado(New DataTable(), 0)
            LimpiarDetalle()
            LblResultado.Text = "Consulta sin actualizar. Revise los filtros y vuelva a buscar."
            MessageBox.Show(Me, ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            UseWaitCursor = False
            BtnBuscar.Enabled = True
        End Try
    End Sub

    ' Separado de la consulta para poder verificar grillas con datos en memoria.
    Friend Sub VincularListado(datos As DataTable, id As Integer)
        cargando = True
        Try
            GridSolicitudes.DataSource = datos
            If datos.Columns.Contains("IdSolicitud") AndAlso id > 0 Then
                For Each fila As DataGridViewRow In GridSolicitudes.Rows
                    If Convert.ToInt32(fila.Cells("IdSolicitud").Value) = id Then
                        GridSolicitudes.CurrentCell = fila.Cells("IdSolicitud")
                        fila.Selected = True
                        Exit For
                    End If
                Next
            End If
        Finally
            cargando = False
        End Try
    End Sub

    Private Sub CambiarSeleccion(sender As Object, e As EventArgs)
        If Not cargando Then MostrarSeleccion()
    End Sub

    Private Sub MostrarSeleccion()
        LimpiarDetalle()
        Dim fila = GridSolicitudes.CurrentRow
        If fila Is Nothing OrElse Not GridSolicitudes.Columns.Contains("IdSolicitud") Then Return
        Try
            Dim doc = servicio.Cargar(Convert.ToInt32(fila.Cells("IdSolicitud").Value))
            Dim historial = servicio.ConsultarHistorial(doc.IdSolicitud)
            MostrarDetalle(doc, historial)
        Catch ex As Exception
            LimpiarDetalle()
            MessageBox.Show(Me, "No se pudo cargar el detalle. " & ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Friend Sub MostrarDetalle(doc As SolicitudCompraUsoDocumento, historial As DataTable)
        seleccionado = doc
        TxtResumen.Text = "Solicitud " & doc.IdSolicitud & " - " & doc.Estado & " | Motivo: " & doc.Motivo & Environment.NewLine &
                          "Observaciones: " & doc.Observaciones & If(doc.Resolucion = "", "", Environment.NewLine & "Última resolución: " & doc.Resolucion)
        If doc.IdCotizacionAutorizada.HasValue Then
            Dim aprobada = doc.Cotizaciones.FirstOrDefault(Function(c) c.IdCotizacion = doc.IdCotizacionAutorizada.Value)
            If aprobada IsNot Nothing Then TxtResumen.AppendText(Environment.NewLine & "Cotización autorizada: " & DescribirCotizacion(aprobada))
        ElseIf doc.Estado = "AUTORIZADA" Then
            TxtResumen.AppendText(Environment.NewLine & "Cotización autorizada: sin seleccionar (autorización anterior).")
        End If
        GridDetalle.DataSource = doc.Items
        GridCotizaciones.DataSource = doc.Cotizaciones
        GridHistorial.DataSource = historial
        BtnVerCotizacion.Enabled = GridCotizaciones.CurrentRow IsNot Nothing
    End Sub

    Private Sub LimpiarDetalle()
        seleccionado = Nothing
        TxtResumen.Clear()
        GridDetalle.DataSource = Nothing
        GridCotizaciones.DataSource = Nothing
        GridHistorial.DataSource = Nothing
        BtnVerCotizacion.Enabled = False
    End Sub

    Private Shared Function DescribirCotizacion(cot As SolicitudCompraUsoCotizacion) As String
        Return cot.ProveedorNombre & If(String.IsNullOrWhiteSpace(cot.NumeroCotizacion), "", " | N° " & cot.NumeroCotizacion) &
               " | " & cot.Moneda & " " & If(cot.ImporteTotal.HasValue, cot.ImporteTotal.GetValueOrDefault().ToString("N2"), "sin importe cargado")
    End Function

    Private Sub FormatearCotizacion(sender As Object, e As DataGridViewCellFormattingEventArgs)
        ' Compara la columna real del Designer, no un nombre que puede ser diferente.
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 OrElse GridCotizaciones.Columns(e.ColumnIndex) IsNot GridCotizacionesColCotizacionAutorizada Then Return
        Dim cot = TryCast(GridCotizaciones.Rows(e.RowIndex).DataBoundItem, SolicitudCompraUsoCotizacion)
        e.Value = If(cot IsNot Nothing AndAlso seleccionado IsNot Nothing AndAlso seleccionado.IdCotizacionAutorizada.HasValue AndAlso cot.IdCotizacion = seleccionado.IdCotizacionAutorizada.Value, "Sí", "")
        e.FormattingApplied = True
    End Sub

    Private Sub VerCotizacion()
        Dim cot = TryCast(GridCotizaciones.CurrentRow?.DataBoundItem, SolicitudCompraUsoCotizacion)
        If cot Is Nothing Then Return
        Using dlg As New frmCotizacionCompraUso(cot, False, servicio)
            dlg.ShowDialog(Me)
        End Using
    End Sub

End Class

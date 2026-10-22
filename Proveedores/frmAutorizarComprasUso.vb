Imports System.Data
Imports System.Linq

Public Class frmAutorizarComprasUso
    Private Shared instancia As frmAutorizarComprasUso
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
            CmbEstado.SelectedItem = "PENDIENTE"
            AddHandler BtnBuscar.Click, Sub() ActualizarListado()
            AddHandler GridSolicitudes.SelectionChanged, AddressOf CambiarSeleccion
            AddHandler GridCotizaciones.SelectionChanged, Sub() BtnVerCotizacion.Enabled = GridCotizaciones.CurrentRow IsNot Nothing
            AddHandler GridCotizaciones.CellFormatting, AddressOf FormatearCotizacion
            AddHandler GridCotizaciones.CellDoubleClick, Sub(sender, celda)
                                                            If celda.RowIndex >= 0 Then VerCotizacion()
                                                        End Sub
            AddHandler BtnVerCotizacion.Click, Sub() VerCotizacion()
            AddHandler BtnAutorizar.Click, Sub() ResolverSeleccion("AUTORIZADA")
            AddHandler BtnRechazar.Click, Sub() ResolverSeleccion("RECHAZADA")
            AddHandler BtnDevolver.Click, Sub() ResolverSeleccion("DEVUELTA")
            LimpiarDetalle()
            CargarSectores()
            ActualizarListado()
        End If
        MyBase.OnLoad(e)
    End Sub

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then instancia = New frmAutorizarComprasUso() With {.MdiParent = mdiParent}
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
        CargarCotizacionesAutorizacion(doc)
        If doc.IdCotizacionAutorizada.HasValue Then
            Dim aprobada = doc.Cotizaciones.FirstOrDefault(Function(c) c.IdCotizacion = doc.IdCotizacionAutorizada.Value)
            If aprobada IsNot Nothing Then TxtResumen.AppendText(Environment.NewLine & "Cotización autorizada: " & DescribirCotizacion(aprobada))
        ElseIf doc.Estado = "AUTORIZADA" Then
            TxtResumen.AppendText(Environment.NewLine & "Cotización autorizada: sin seleccionar (autorización anterior).")
        End If
        GridDetalle.DataSource = doc.Items
        GridCotizaciones.DataSource = doc.Cotizaciones
        GridHistorial.DataSource = historial
        HabilitarResolucion()
        BtnVerCotizacion.Enabled = GridCotizaciones.CurrentRow IsNot Nothing
    End Sub

    Private Sub LimpiarDetalle()
        seleccionado = Nothing
        If CmbCotizacionAutorizar IsNot Nothing Then CmbCotizacionAutorizar.DataSource = Nothing
        TxtResumen.Clear()
        If TxtResolucion IsNot Nothing Then TxtResolucion.Clear()
        GridDetalle.DataSource = Nothing
        GridCotizaciones.DataSource = Nothing
        GridHistorial.DataSource = Nothing
        BtnVerCotizacion.Enabled = False
        HabilitarResolucion()
    End Sub

    Private Sub HabilitarResolucion()
        Dim pendiente = seleccionado IsNot Nothing AndAlso seleccionado.Estado = "PENDIENTE"
        If BtnAutorizar IsNot Nothing Then BtnAutorizar.Enabled = pendiente
        If BtnRechazar IsNot Nothing Then BtnRechazar.Enabled = pendiente
        If BtnDevolver IsNot Nothing Then BtnDevolver.Enabled = pendiente
        If TxtResolucion IsNot Nothing Then TxtResolucion.Enabled = pendiente
        If CmbCotizacionAutorizar IsNot Nothing Then CmbCotizacionAutorizar.Enabled = pendiente
    End Sub

    Private Class OpcionCotizacion
        Public Property Id As Integer
        Public Property Descripcion As String
    End Class

    Private Shared Function DescribirCotizacion(cot As SolicitudCompraUsoCotizacion) As String
        Return cot.ProveedorNombre & If(String.IsNullOrWhiteSpace(cot.NumeroCotizacion), "", " | N° " & cot.NumeroCotizacion) &
               " | " & cot.Moneda & " " & If(cot.ImporteTotal.HasValue, cot.ImporteTotal.GetValueOrDefault().ToString("N2"), "sin importe cargado")
    End Function

    Private Sub CargarCotizacionesAutorizacion(doc As SolicitudCompraUsoDocumento)
        Dim opciones As New List(Of OpcionCotizacion) From {New OpcionCotizacion With {.Id = 0, .Descripcion = "Seleccione una cotización..."}}
        For Each cot In doc.Cotizaciones
            opciones.Add(New OpcionCotizacion With {.Id = cot.IdCotizacion, .Descripcion = DescribirCotizacion(cot)})
        Next
        If CmbCotizacionAutorizar IsNot Nothing Then CmbCotizacionAutorizar.DataSource = Nothing
        CmbCotizacionAutorizar.DisplayMember = "Descripcion"
        CmbCotizacionAutorizar.ValueMember = "Id"
        CmbCotizacionAutorizar.DataSource = opciones
        CmbCotizacionAutorizar.SelectedValue = doc.IdCotizacionAutorizada.GetValueOrDefault()
        If CmbCotizacionAutorizar.SelectedIndex < 0 Then CmbCotizacionAutorizar.SelectedIndex = 0
    End Sub

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

    Private Sub ResolverSeleccion(estadoNuevo As String)
        If seleccionado Is Nothing OrElse seleccionado.Estado <> "PENDIENTE" Then Return
        Dim nota = TxtResolucion.Text.Trim()
        If estadoNuevo <> "AUTORIZADA" AndAlso nota.Length = 0 Then
            MessageBox.Show(Me, "Indique el motivo del rechazo o de la devolución.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtResolucion.Focus()
            Return
        End If
        Dim accion = If(estadoNuevo = "AUTORIZADA", "autorizar", If(estadoNuevo = "RECHAZADA", "rechazar", "devolver para corregir"))
        Dim id = seleccionado.IdSolicitud
        Dim idCotizacion As Integer? = Nothing
        Dim detalleCotizacion As String = ""
        If estadoNuevo = "AUTORIZADA" Then
            Dim opcion = TryCast(CmbCotizacionAutorizar.SelectedItem, OpcionCotizacion)
            If opcion Is Nothing OrElse opcion.Id <= 0 Then
                MessageBox.Show(Me, "Seleccione la cotización que desea autorizar. Si falta cargarla, devuelva la solicitud para corregirla.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                CmbCotizacionAutorizar.Focus()
                Return
            End If
            idCotizacion = opcion.Id
            detalleCotizacion = Environment.NewLine & Environment.NewLine & opcion.Descripcion
        End If
        If MessageBox.Show(Me, "¿Desea " & accion & " la solicitud " & id & "?" & detalleCotizacion, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
        Try
            Enabled = False
            UseWaitCursor = True
            seleccionado = servicio.Resolver(id, seleccionado.VersionFila, estadoNuevo, nota, General.UsuarioActual, idCotizacion)
            HabilitarResolucion()
            MessageBox.Show(Me, "Solicitud " & id & ": " & estadoNuevo & ".", Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ActualizarListado()
        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' Una ventana desactualizada no debe conservar botones de resolucion.
            ActualizarListado()
        Finally
            Enabled = True
            UseWaitCursor = False
        End Try
    End Sub

End Class

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
            AddHandler CmbSector.SelectedIndexChanged, Sub() ActualizarListado()
            AddHandler CmbEstado.SelectedIndexChanged, Sub() ActualizarListado()
            AddHandler GridSolicitudes.SelectionChanged, AddressOf CambiarSeleccion
            AddHandler GridSolicitudes.Click, AddressOf CambiarSeleccion
            AddHandler GridCotizaciones.SelectionChanged, Sub() BtnVerCotizacion.Enabled = GridCotizaciones.CurrentRow IsNot Nothing
            AddHandler GridCotizaciones.CellFormatting, AddressOf FormatearCotizacion
            AddHandler GridCotizaciones.DataBindingComplete, Sub() ConfigurarGridCotizacion()
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
            ConfigurarGridSolicitudes()
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
            ConfigurarGridDetalle()
            ConfigurarGridHistorial()
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

    Private Sub FormatearEstado(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles GridSolicitudes.CellFormatting
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 OrElse GridSolicitudes.Columns(e.ColumnIndex).DataPropertyName <> "Estado" Then Return
        ComprasUsoColores.AplicarCelda(e.CellStyle, Convert.ToString(e.Value))
    End Sub

    Private Sub FormatearCotizacion(sender As Object, e As DataGridViewCellFormattingEventArgs)
        ' Compara la columna real del Designer, no un nombre que puede ser diferente.
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 OrElse GridCotizaciones.Columns(e.ColumnIndex) IsNot GridCotizacionesColCotizacionAutorizada Then Return
        Dim cot = TryCast(GridCotizaciones.Rows(e.RowIndex).DataBoundItem, SolicitudCompraUsoCotizacion)
        e.Value = If(cot IsNot Nothing AndAlso seleccionado IsNot Nothing AndAlso seleccionado.IdCotizacionAutorizada.HasValue AndAlso cot.IdCotizacion = seleccionado.IdCotizacionAutorizada.Value, "Sí", "")
        If CStr(e.Value) = "Sí" Then ComprasUsoColores.AplicarCelda(e.CellStyle, "AUTORIZADA")
        e.FormattingApplied = True
    End Sub

    Private Sub VerCotizacion()
        Dim cot = TryCast(GridCotizaciones.CurrentRow?.DataBoundItem, SolicitudCompraUsoCotizacion)
        If cot Is Nothing OrElse seleccionado Is Nothing Then Return
        Using dlg As New frmSolicitudCompraUso(seleccionado, cot.IdCotizacion, servicio)
            dlg.ShowDialog(Me)
        End Using
    End Sub

    Private Sub ConfigurarGridDetalle()
        If GridDetalle Is Nothing OrElse GridDetalle.Columns.Count = 0 Then Return
        For Each col As DataGridViewColumn In GridDetalle.Columns
            col.Visible = False
        Next
        If GridDetalle.Columns.Contains("TipoItem") Then
            GridDetalle.Columns("TipoItem").Visible = True
            GridDetalle.Columns("TipoItem").HeaderText = "TipoItem"
            GridDetalle.Columns("TipoItem").Width = 80
        End If
        If GridDetalle.Columns.Contains("Descripcion") Then
            GridDetalle.Columns("Descripcion").Visible = True
            GridDetalle.Columns("Descripcion").HeaderText = "Descripcion"
            GridDetalle.Columns("Descripcion").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If
        If GridDetalle.Columns.Contains("Cantidad") Then
            GridDetalle.Columns("Cantidad").Visible = True
            GridDetalle.Columns("Cantidad").HeaderText = "Cantidad"
            GridDetalle.Columns("Cantidad").Width = 80
            'la cantidad sin decimales y alineacion derecha
            GridDetalle.Columns("Cantidad").DefaultCellStyle.Format = "N0"
            GridDetalle.Columns("Cantidad").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
        If GridDetalle.Columns.Contains("FechaNecesidad") Then
            GridDetalle.Columns("FechaNecesidad").Visible = True
            GridDetalle.Columns("FechaNecesidad").HeaderText = "Fecha de Necesidad"
            GridDetalle.Columns("FechaNecesidad").Width = 150
        End If
        If GridDetalle.Columns.Contains("Observaciones") Then
            GridDetalle.Columns("Observaciones").Visible = True
            GridDetalle.Columns("Observaciones").HeaderText = "Observaciones"
            GridDetalle.Columns("Observaciones").Width = 200
        End If
    End Sub

    Private Sub ConfigurarGridSolicitudes()
        If GridSolicitudes Is Nothing OrElse GridSolicitudes.Columns.Count = 0 Then Return
        For Each col As DataGridViewColumn In GridSolicitudes.Columns
            col.Visible = False
        Next
        If GridSolicitudes.Columns.Contains("FechaSolicitud") Then
            GridSolicitudes.Columns("FechaSolicitud").Visible = True
            GridSolicitudes.Columns("FechaSolicitud").HeaderText = "Fecha de Solicitud"
            GridSolicitudes.Columns("FechaSolicitud").Width = 120
        End If
        If GridSolicitudes.Columns.Contains("Sector") Then
            GridSolicitudes.Columns("Sector").Visible = True
            GridSolicitudes.Columns("Sector").HeaderText = "Sector"
            GridSolicitudes.Columns("Sector").Width = 100
        End If
        If GridSolicitudes.Columns.Contains("UsuarioSolicitante") Then
            GridSolicitudes.Columns("UsuarioSolicitante").Visible = True
            GridSolicitudes.Columns("UsuarioSolicitante").HeaderText = "Solicitante"
            GridSolicitudes.Columns("UsuarioSolicitante").Width = 70
        End If
        If GridSolicitudes.Columns.Contains("Prioridad") Then
            GridSolicitudes.Columns("Prioridad").Visible = True
            GridSolicitudes.Columns("Prioridad").HeaderText = "Prioridad"
            GridSolicitudes.Columns("Prioridad").Width = 80
        End If
        If GridSolicitudes.Columns.Contains("Motivo") Then
            GridSolicitudes.Columns("Motivo").Visible = True
            GridSolicitudes.Columns("Motivo").HeaderText = "Motivo"
            GridSolicitudes.Columns("Motivo").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If
        If GridSolicitudes.Columns.Contains("ProveedorAutorizado") Then
            GridSolicitudes.Columns("ProveedorAutorizado").Visible = True
            GridSolicitudes.Columns("ProveedorAutorizado").HeaderText = "Proveedor Autorizado"
            GridSolicitudes.Columns("ProveedorAutorizado").Width = 150
        End If
        If GridSolicitudes.Columns.Contains("MonedaAutorizada") Then
            GridSolicitudes.Columns("MonedaAutorizada").Visible = True
            GridSolicitudes.Columns("MonedaAutorizada").HeaderText = "Moneda"
            GridSolicitudes.Columns("MonedaAutorizada").Width = 70
        End If
        If GridSolicitudes.Columns.Contains("Estado") Then
            GridSolicitudes.Columns("Estado").Visible = True
            GridSolicitudes.Columns("Estado").HeaderText = "Estado"
            GridSolicitudes.Columns("Estado").Width = 90
        End If
    End Sub

    Private Sub ConfigurarGridHistorial()
        If GridHistorial Is Nothing OrElse GridHistorial.Columns.Count = 0 Then Return
        For Each col As DataGridViewColumn In GridHistorial.Columns
            col.Visible = False
        Next
        If GridHistorial.Columns.Contains("FechaAccion") Then
            GridHistorial.Columns("FechaAccion").Visible = True
            GridHistorial.Columns("FechaAccion").HeaderText = "Fecha"
            GridHistorial.Columns("FechaAccion").Width = 120
        End If
        If GridHistorial.Columns.Contains("Usuario") Then
            GridHistorial.Columns("Usuario").Visible = True
            GridHistorial.Columns("Usuario").HeaderText = "Usuario"
            GridHistorial.Columns("Usuario").Width = 100
        End If
        If GridHistorial.Columns.Contains("Accion") Then
            GridHistorial.Columns("Accion").Visible = True
            GridHistorial.Columns("Accion").HeaderText = "Acción"
            GridHistorial.Columns("Accion").Width = 100
        End If
        If GridHistorial.Columns.Contains("EstadoAnterior") Then
            GridHistorial.Columns("EstadoAnterior").Visible = True
            GridHistorial.Columns("EstadoAnterior").HeaderText = "Estado Anterior"
            GridHistorial.Columns("EstadoAnterior").Width = 100
        End If
        If GridHistorial.Columns.Contains("EstadoNuevo") Then
            GridHistorial.Columns("EstadoNuevo").Visible = True
            GridHistorial.Columns("EstadoNuevo").HeaderText = "Estado Nuevo"
            GridHistorial.Columns("EstadoNuevo").Width = 100
        End If
        If GridHistorial.Columns.Contains("Observaciones") Then
            GridHistorial.Columns("Observaciones").Visible = True
            GridHistorial.Columns("Observaciones").HeaderText = "Observaciones"
            GridHistorial.Columns("Observaciones").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If
    End Sub

    ' Busca la columna por nombre interno o por el campo enlazado del modelo.
    Private Function BuscarColumnaCotizacion(nombre As String) As DataGridViewColumn
        For Each col As DataGridViewColumn In GridCotizaciones.Columns
            If String.Equals(col.Name, nombre, StringComparison.OrdinalIgnoreCase) OrElse
               String.Equals(col.DataPropertyName, nombre, StringComparison.OrdinalIgnoreCase) Then
                Return col
            End If
        Next
        Return Nothing
    End Function

    Private Sub ConfigurarGridCotizacion()
        If GridCotizaciones Is Nothing OrElse GridCotizaciones.Columns.Count = 0 Then Return
        For Each col As DataGridViewColumn In GridCotizaciones.Columns
            col.Visible = False
        Next
        ' Autorizada es la unica columna declarada en el Designer y no esta enlazada:
        ' el valor lo calcula FormatearCotizacion al dibujar la fila.
        GridCotizacionesColCotizacionAutorizada.Visible = True
        GridCotizacionesColCotizacionAutorizada.HeaderText = "Autorizada"
        GridCotizacionesColCotizacionAutorizada.DisplayIndex = 0
        GridCotizacionesColCotizacionAutorizada.Width = 85

        ' El resto son columnas autogeneradas a partir de las propiedades del modelo.
        Dim campos = {"ProveedorNombre", "NumeroCotizacion", "FechaCotizacion",
                      "Moneda", "ImporteTotal", "CondicionPago", "PlazoEntrega", "Observaciones", "ArchivosPdf"}
        Dim titulos = {"Proveedor", "NroCotización", "Fecha", "Moneda", "Importe", "Condición", "Plazo de Entrega", "Observaciones", "Archivos PDF"}
        Dim anchos = {150, 100, 70, 60, 100, 120, 120, 220, 90}
        For i = 0 To campos.Length - 1
            Dim col = BuscarColumnaCotizacion(campos(i))
            If col Is Nothing Then Continue For
            col.Visible = True
            col.HeaderText = titulos(i)
            col.DisplayIndex = i + 1
            col.Width = anchos(i)
            If campos(i) = "FechaCotizacion" Then col.DefaultCellStyle.Format = "dd/MM/yyyy"
            If campos(i) = "ImporteTotal" Then
                'columna importetotal formato moneda y alineacion derecha sin decimales
                col.DefaultCellStyle.Format = "N2"
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If
        Next
    End Sub


End Class

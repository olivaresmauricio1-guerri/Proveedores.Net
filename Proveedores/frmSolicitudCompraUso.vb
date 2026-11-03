Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Globalization
Imports System.IO
Imports System.Linq

Public Class frmSolicitudCompraUso
    ' Referencia compartida para abrir una sola ventana de solicitudes en el MDI.
    Private Shared instancia As frmSolicitudCompraUso
    ' Servicio de consultas y guardado; el formulario no ejecuta SQL directamente.
    Private servicio As SolicitudCompraUsoService
    ' Documento en memoria, con su cabecera, renglones y cotizaciones.
    Private documento As SolicitudCompraUsoDocumento
    ' Evita marcar cambios cuando los controles se completan desde el documento.
    Private cargando As Boolean
    ' Indica que hay cambios pendientes y permite avisar antes de descartarlos.
    Private modificado As Boolean
    Private soloConsulta As Boolean
    Private documentoConsulta As SolicitudCompraUsoDocumento
    Private idCotizacionConsulta As Integer
    Private cotizacionActual As SolicitudCompraUsoCotizacion
    Private editandoCotizacion As Boolean
    Private cotizacionModificada As Boolean
    Private cargandoCotizacion As Boolean
    Private cuentaCotizacion As Integer?
    Private ReadOnly adjuntosCotizacion As New List(Of SolicitudCompraUsoPdf)

    ' Crea los controles y conecta los eventos que detectan cambios en la cabecera.
    Public Sub New()
        InitializeComponent()
        AddHandler TxtMotivo.TextChanged, AddressOf MarcarCambio
        AddHandler TxtObservaciones.TextChanged, AddressOf MarcarCambio
        AddHandler CmbSector.SelectedValueChanged, AddressOf MarcarCambio
        AddHandler CmbPrioridad.SelectedIndexChanged, AddressOf MarcarCambio
        AddHandler DtpFecha.ValueChanged, AddressOf MarcarCambio
        AddHandler DtpNecesidad.ValueChanged, AddressOf MarcarCambio
    End Sub

    ' Constructor alternativo para recibir un servicio ya creado, por ejemplo en pruebas.
    Public Sub New(datos As SolicitudCompraUsoService)
        Me.New()
        servicio = datos
    End Sub

    ' Consulta y Autorizacion usan la misma pestaña, sin permitir modificar el documento.
    Public Sub New(doc As SolicitudCompraUsoDocumento, idCotizacion As Integer, datos As SolicitudCompraUsoService)
        Me.New(datos)
        soloConsulta = True
        documentoConsulta = doc
        idCotizacionConsulta = idCotizacion
    End Sub

    ' Crea la ventana si no existe; de lo contrario muestra y enfoca la existente.
    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then instancia = New frmSolicitudCompraUso() With {.MdiParent = mdiParent}
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    ' Al abrir, prepara una solicitud nueva sin guardarla todavia en la base.
    Private Sub frmSolicitudCompraUso_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' No inicializa datos cuando la ventana se esta mostrando en el diseñador.
        If ComprasUsoDiseno.EstaEnDiseno(Me) Then Return
        ' Conserva el servicio recibido por constructor o crea el predeterminado.
        Try
            If servicio Is Nothing Then servicio = New SolicitudCompraUsoService()
            If soloConsulta Then
                MostrarDocumento(documentoConsulta)
                SeleccionarCotizacion(documento.Cotizaciones.FirstOrDefault(Function(c) c.IdCotizacion = idCotizacionConsulta))
                Pestanas.SelectedTab = TabCotizaciones
            Else
                NuevaSolicitud()
            End If
        Catch ex As Exception
            ' Si falla la preparacion inicial, informa el motivo y cierra la ventana.
            MessageBox.Show(Me, "No se pudo iniciar la solicitud. " & ex.Message, "Solicitud Compras", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    ' Libera la referencia compartida para permitir abrir otra instancia mas adelante.
    Private Sub frmSolicitudCompraUso_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If instancia Is Me Then instancia = Nothing
    End Sub

    ' Crea un documento con valores iniciales y el usuario conectado como solicitante.
    Private Sub NuevaSolicitud()
        Dim nuevo As New SolicitudCompraUsoDocumento With {.UsuarioSolicitante = General.UsuarioActual}
        MostrarDocumento(nuevo)
        TxtMotivo.Focus()
    End Sub

    ' Traslada el documento a los controles y enlaza sus listas a las grillas.
    Private Sub MostrarDocumento(doc As SolicitudCompraUsoDocumento)
        ' Cargar primero el catalogo: un error no debe descartar el documento actual.
        Dim sectores = servicio.Sectores(doc.IdSector)
        cargando = True
        Try
            documento = doc
            ' El combo muestra la descripcion, pero guarda el ID del sector.
            CmbSector.DataSource = Nothing
            CmbSector.DisplayMember = "Descripcion"
            CmbSector.ValueMember = "IdSector"
            CmbSector.DataSource = sectores
            If doc.IdSector > 0 Then
                CmbSector.SelectedValue = doc.IdSector
            Else
                ' Una solicitud nueva obliga al usuario a elegir un sector.
                CmbSector.SelectedIndex = -1
            End If
            ' Cabecera y datos informativos del documento.
            TxtNumero.Text = If(doc.IdSolicitud = 0, "Nueva", doc.IdSolicitud.ToString())
            TxtEstado.Text = doc.Estado
            TxtSolicitante.Text = doc.UsuarioSolicitante
            DtpFecha.Value = doc.FechaSolicitud
            ' La casilla del selector indica si la fecha de necesidad esta informada.
            DtpNecesidad.Value = If(doc.FechaNecesidad, Date.Today)
            DtpNecesidad.Checked = doc.FechaNecesidad.HasValue
            CmbPrioridad.SelectedIndex = If(doc.Prioridad = "U", 1, 0)
            TxtMotivo.Text = doc.Motivo
            TxtObservaciones.Text = doc.Observaciones
            TxtResolucion.Text = doc.Resolucion
            ' Las grillas trabajan sobre las listas del documento en memoria.
            GridDetalle.DataSource = doc.Items
            editandoCotizacion = False
            cotizacionActual = Nothing
            GridCotizaciones.DataSource = doc.Cotizaciones
            ConfigurarGridCotizaciones()
            MostrarCotizacionSeleccionada()
            modificado = False
            AplicarEstado()
            ConfigurarGridDetalle()
        Finally
            ' Reactiva la deteccion de cambios incluso si la carga de controles falla.
            cargando = False
        End Try
    End Sub

    ' Permite editar solo documentos en BORRADOR o DEVUELTA.
    ' Las cotizaciones se editan en los campos de su pestaña, no en la grilla.
    Private Sub AplicarEstado()
        Dim editar = Not soloConsulta AndAlso documento IsNot Nothing AndAlso documento.Editable
        For Each c As Control In New Control() {CmbSector, DtpFecha, DtpNecesidad, CmbPrioridad, BtnAgregarItem, BtnQuitarItem, BtnAgregarCot, BtnQuitarCot, BtnGuardar, BtnEnviar}
            c.Enabled = editar
        Next
        TxtMotivo.ReadOnly = Not editar
        TxtObservaciones.ReadOnly = Not editar
        GridDetalle.ReadOnly = Not editar
        GridCotizaciones.ReadOnly = True
        BtnNueva.Enabled = Not soloConsulta
        BtnAbrir.Enabled = Not soloConsulta
        AplicarEstadoCotizacion()
    End Sub

    Private Sub TxtEstado_TextChanged(sender As Object, e As EventArgs) Handles TxtEstado.TextChanged
        TxtEstado.BackColor = ComprasUsoColores.FondoEstado(TxtEstado.Text)
        TxtEstado.ForeColor = Color.FromArgb(35, 35, 35)
    End Sub

    ' Marca cambios del usuario, ignorando cargas programaticas y documentos no editables.
    Private Sub MarcarCambio(sender As Object, e As EventArgs)
        If Not cargando AndAlso Not soloConsulta AndAlso documento IsNot Nothing AndAlso documento.Editable Then modificado = True
    End Sub

    ' Incluye los cambios de celdas del detalle en el aviso de datos pendientes.
    Private Sub GridDetalle_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles GridDetalle.CellValueChanged
        MarcarCambio(sender, EventArgs.Empty)
    End Sub

    ' Intercepta errores de conversion de la grilla e indica que se revise el renglon.
    Private Sub GridDetalle_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles GridDetalle.DataError
        e.ThrowException = False
        e.Cancel = True
        MessageBox.Show(Me, "Revise el valor del renglón: cantidad numérica, fecha válida o tipo de artículo/servicio.", "Detalle", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    ' Finaliza la edicion actual, agrega un renglon y abre su descripcion para escribir.
    Private Sub BtnAgregarItem_Click(sender As Object, e As EventArgs) Handles BtnAgregarItem.Click
        If soloConsulta OrElse documento Is Nothing OrElse Not documento.Editable Then Return
        If Not GridDetalle.EndEdit() Then Return
        documento.Items.Add(New SolicitudCompraUsoItem())
        GridDetalle.CurrentCell = GridDetalle.Rows(GridDetalle.Rows.Count - 1).Cells("Descripcion")
        GridDetalle.Focus()
        GridDetalle.BeginEdit(True)
        modificado = True
    End Sub

    ' Quita el renglon seleccionado de la lista en memoria, previa confirmacion.
    ' La eliminacion en SQL se realiza al guardar la solicitud.
    Private Sub BtnQuitarItem_Click(sender As Object, e As EventArgs) Handles BtnQuitarItem.Click
        If soloConsulta OrElse documento Is Nothing OrElse Not documento.Editable Then Return
        Dim item = TryCast(GridDetalle.CurrentRow?.DataBoundItem, SolicitudCompraUsoItem)
        If item Is Nothing Then Return
        If MessageBox.Show(Me, "¿Quitar el renglón seleccionado?", "Detalle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
        GridDetalle.CancelEdit()
        documento.Items.Remove(item)
        modificado = True
    End Sub

    ' Prepara una cotizacion nueva; se incorpora al documento al aplicar o guardar.
    Private Sub BtnAgregarCot_Click(sender As Object, e As EventArgs) Handles BtnAgregarCot.Click
        If soloConsulta OrElse documento Is Nothing OrElse Not documento.Editable OrElse editandoCotizacion Then Return
        editandoCotizacion = True
        MostrarCotizacion(New SolicitudCompraUsoCotizacion())
        Pestanas.SelectedTab = TabCotizaciones
        TxtCotProveedor.Focus()
    End Sub

    ' El boton y el doble clic comparten la misma apertura de cotizacion.
    Private Sub BtnEditarCot_Click(sender As Object, e As EventArgs) Handles BtnEditarCot.Click
        EditarCotizacion()
    End Sub

    ' Ignora el doble clic en encabezados; abre la cotizacion de una fila de datos.
    Private Sub GridCotizaciones_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridCotizaciones.CellDoubleClick
        If e.RowIndex >= 0 Then EditarCotizacion()
    End Sub

    ' Habilita la edicion de una copia; cancelar no modifica la cotizacion original.
    Private Sub EditarCotizacion()
        If soloConsulta OrElse documento Is Nothing OrElse Not documento.Editable OrElse editandoCotizacion Then Return
        Dim cot = TryCast(GridCotizaciones.CurrentRow?.DataBoundItem, SolicitudCompraUsoCotizacion)
        If cot Is Nothing Then Return
        editandoCotizacion = True
        MostrarCotizacion(cot)
        TxtCotProveedor.Focus()
    End Sub

    ' Quita de memoria la cotizacion y sus adjuntos; SQL se sincroniza al guardar.
    Private Sub BtnQuitarCot_Click(sender As Object, e As EventArgs) Handles BtnQuitarCot.Click
        If soloConsulta OrElse documento Is Nothing OrElse Not documento.Editable OrElse editandoCotizacion Then Return
        Dim cot = TryCast(GridCotizaciones.CurrentRow?.DataBoundItem, SolicitudCompraUsoCotizacion)
        If cot Is Nothing Then Return
        If MessageBox.Show(Me, "¿Quitar esta cotización y sus PDF de la solicitud?", "Cotización", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
        documento.Cotizaciones.Remove(cot)
        MostrarCotizacionSeleccionada()
        modificado = True
    End Sub

    ' Guarda sin enviar a autorizacion, conservando el estado editable actual.
    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        Guardar(False)
    End Sub

    ' Guarda y solicita el cambio a PENDIENTE, que bloquea nuevas ediciones.
    Private Sub BtnEnviar_Click(sender As Object, e As EventArgs) Handles BtnEnviar.Click
        Guardar(True)
    End Sub

    ' Flujo comun de guardado: termina la edicion, actualiza el modelo y delega al servicio.
    Private Sub Guardar(enviar As Boolean)
        If soloConsulta OrElse documento Is Nothing OrElse Not documento.Editable Then Return
        If Not AplicarCotizacion() Then Return
        ' Confirma la celda y el elemento enlazado antes de validar sus valores.
        If Not GridDetalle.EndEdit() Then Return
        BindingContext(documento.Items).EndCurrentEdit()
        ' Copia la cabecera; una fecha de necesidad desmarcada se guarda como ausente.
        documento.IdSector = If(CmbSector.SelectedValue Is Nothing, 0, Convert.ToInt32(CmbSector.SelectedValue))
        documento.FechaSolicitud = DtpFecha.Value.Date
        documento.FechaNecesidad = If(DtpNecesidad.Checked, CType(DtpNecesidad.Value.Date, Date?), Nothing)
        documento.Motivo = TxtMotivo.Text.Trim()
        documento.Observaciones = TxtObservaciones.Text.Trim()
        documento.Prioridad = If(CmbPrioridad.SelectedIndex = 1, "U", "N")
        Try
            ' Valida antes de pedir confirmacion y antes de escribir en la base.
            SolicitudCompraUsoService.Validar(documento, General.UsuarioActual)
            If enviar AndAlso MessageBox.Show(Me, "¿Enviar la solicitud a autorización? Quedará pendiente y no podrá editarse hasta que sea devuelta.", "Solicitud Compras", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
            ' Bloquea la ventana durante el guardado sincrono para evitar acciones simultaneas.
            Enabled = False
            UseWaitCursor = True
            Dim guardado = servicio.Guardar(documento, enviar, General.UsuarioActual)
            ' Adoptar inmediatamente los IDs confirmados, incluso si falla luego
            ' la recarga del combo: reintentar nunca debe crear otra solicitud.
            documento = guardado
            modificado = False
            MostrarDocumento(guardado)
            MessageBox.Show(Me, "Solicitud " & guardado.IdSolicitud & If(enviar, " enviada a autorización.", " guardada."), "Solicitud Compras", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, "No se pudo completar la operación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Restablece la ventana y aplica el estado devuelto, aun si hubo un error.
            Enabled = True
            UseWaitCursor = False
            AplicarEstado()
        End Try
    End Sub

    ' Comprueba tambien la celda todavia en edicion y pide permiso para perder cambios.
    Private Function PuedeDescartar() As Boolean
        If GridDetalle.IsCurrentCellDirty Then modificado = True
        Return Not (modificado OrElse cotizacionModificada) OrElse MessageBox.Show(Me, "Hay cambios sin guardar. ¿Desea descartarlos?", "Solicitud Compras", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes
    End Function

    ' Antes de reemplazar el documento por uno nuevo, protege los cambios pendientes.
    Private Sub BtnNueva_Click(sender As Object, e As EventArgs) Handles BtnNueva.Click
        If soloConsulta Then Return
        If Not PuedeDescartar() Then Return
        Try
            NuevaSolicitud()
        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, "Solicitud Compras", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Abre el selector y carga la solicitud elegida si el usuario acepta.
    Private Sub BtnAbrir_Click(sender As Object, e As EventArgs) Handles BtnAbrir.Click
        If soloConsulta Then Return
        If Not PuedeDescartar() Then Return
        Using dlg As New frmSeleccionSolicitudCompraUso(servicio)
            If dlg.ShowDialog(Me) = DialogResult.OK AndAlso dlg.IdSeleccionado.HasValue Then AbrirSolicitud(dlg.IdSeleccionado.Value)
        End Using
    End Sub

    ' Consulta el documento completo por ID y lo muestra; informa errores de carga.
    Public Sub AbrirSolicitud(id As Integer)
        If soloConsulta Then Return
        Try
            MostrarDocumento(servicio.Cargar(id))
        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, "Abrir solicitud", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Inicia el cierre normal; FormClosing decide si hay cambios que proteger.
    Private Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles BtnCerrar.Click
        Close()
    End Sub

    ' Cancela cualquier intento de cierre si el usuario no acepta descartar los cambios.
    Private Sub frmSolicitudCompraUso_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = Not PuedeDescartar()
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

    Private Sub ConfigurarGridCotizaciones()
        For Each col As DataGridViewColumn In GridCotizaciones.Columns
            col.Visible = False
        Next
        Dim campos = {"ProveedorNombre", "NumeroCotizacion", "FechaCotizacion", "Moneda", "ImporteTotal", "ArchivosPdf"}
        Dim titulos = {"Proveedor", "Cotización", "Fecha", "Moneda", "Total", "PDF"}
        For i = 0 To campos.Length - 1
            Dim col = BuscarColumnaCotizacion(campos(i))
            If col Is Nothing Then Continue For
            col.Visible = True
            col.HeaderText = titulos(i)
            col.DisplayIndex = i
            col.Width = If(i = 1 OrElse i = 4, 100, 65)
            If i = 0 Then
                ' Fill puede fallar en algunos momentos del bind del DataGridView;
                ' calculamos el ancho manualmente porque la grilla tiene tamaño fijo.
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                col.Width = Math.Max(180, GridCotizaciones.ClientSize.Width - 430)
            End If
            If i = 2 Then col.DefaultCellStyle.Format = "dd/MM/yyyy"
            If i = 4 Then
                col.DefaultCellStyle.Format = "N2"
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If
        Next
    End Sub

    Private Function BuscarColumnaCotizacion(nombre As String) As DataGridViewColumn
        For Each col As DataGridViewColumn In GridCotizaciones.Columns
            If String.Equals(col.Name, nombre, StringComparison.OrdinalIgnoreCase) OrElse
               String.Equals(col.DataPropertyName, nombre, StringComparison.OrdinalIgnoreCase) Then
                Return col
            End If
        Next
        Return Nothing
    End Function

    Private Sub GridCotizaciones_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles GridCotizaciones.DataBindingComplete
        ConfigurarGridCotizaciones()
    End Sub

    ' CurrentCellChanged ocurre cuando CurrentRow ya apunta a la nueva cotizacion.
    Private Sub GridCotizaciones_CurrentCellChanged(sender As Object, e As EventArgs) Handles GridCotizaciones.CurrentCellChanged
        If Not cargando AndAlso Not editandoCotizacion Then MostrarCotizacionSeleccionada()
    End Sub

    Private Sub MostrarCotizacionSeleccionada()
        MostrarCotizacion(TryCast(GridCotizaciones.CurrentRow?.DataBoundItem, SolicitudCompraUsoCotizacion))
    End Sub

    Private Sub SeleccionarCotizacion(cot As SolicitudCompraUsoCotizacion)
        Dim colProveedor = BuscarColumnaCotizacion("ProveedorNombre")
        For Each fila As DataGridViewRow In GridCotizaciones.Rows
            If fila.DataBoundItem Is cot Then
                GridCotizaciones.CurrentCell = If(colProveedor Is Nothing, Nothing, fila.Cells(colProveedor.Index))
                fila.Selected = True
                Exit For
            End If
        Next
        MostrarCotizacionSeleccionada()
    End Sub

    Private Sub MostrarCotizacion(cot As SolicitudCompraUsoCotizacion)
        cargandoCotizacion = True
        Try
            cotizacionActual = cot
            Dim datos = If(cot, New SolicitudCompraUsoCotizacion())
            cuentaCotizacion = datos.NroCuentaProveedor
            TxtCotProveedor.Text = datos.ProveedorNombre
            TxtCotNumero.Text = datos.NumeroCotizacion
            DtpCotFecha.Value = datos.FechaCotizacion
            DtpCotVencimiento.Value = If(datos.FechaVencimiento, Date.Today)
            DtpCotVencimiento.Checked = datos.FechaVencimiento.HasValue
            CmbCotMoneda.Text = datos.Moneda
            TxtCotTotal.Text = If(datos.ImporteTotal.HasValue, datos.ImporteTotal.GetValueOrDefault().ToString("N2"), "")
            TxtCotPago.Text = datos.CondicionPago
            TxtCotEntrega.Text = datos.PlazoEntrega
            TxtCotObservaciones.Text = datos.Observaciones
            adjuntosCotizacion.Clear()
            For Each pdf In datos.Adjuntos
                adjuntosCotizacion.Add(New SolicitudCompraUsoPdf With {
                    .IdAdjunto = pdf.IdAdjunto, .NombreArchivo = pdf.NombreArchivo,
                    .TamanoBytes = pdf.TamanoBytes, .Contenido = pdf.Contenido})
            Next
            cotizacionModificada = False
            RefrescarPdf()
            AplicarEstadoCotizacion()
        Finally
            cargandoCotizacion = False
        End Try
    End Sub

    Private Sub AplicarEstadoCotizacion()
        Dim editable = Not soloConsulta AndAlso documento IsNot Nothing AndAlso documento.Editable
        Dim editar = editable AndAlso editandoCotizacion
        For Each caja In New TextBox() {TxtCotProveedor, TxtCotNumero, TxtCotTotal, TxtCotPago, TxtCotEntrega, TxtCotObservaciones}
            caja.ReadOnly = Not editar
        Next
        DtpCotFecha.Enabled = editar
        DtpCotVencimiento.Enabled = editar
        CmbCotMoneda.Enabled = editar
        BtnCotBuscarProveedor.Enabled = editar
        BtnCotAdjuntar.Enabled = editar
        BtnCotQuitarPdf.Enabled = editar AndAlso LstCotArchivos.SelectedItem IsNot Nothing
        BtnAplicarCot.Enabled = editar
        BtnCancelarCot.Enabled = editar
        GridCotizaciones.Enabled = Not editandoCotizacion
        BtnAgregarCot.Enabled = editable AndAlso Not editandoCotizacion
        BtnEditarCot.Enabled = editable AndAlso Not editandoCotizacion AndAlso cotizacionActual IsNot Nothing
        BtnQuitarCot.Enabled = BtnEditarCot.Enabled
        BtnCotAbrirPdf.Enabled = LstCotArchivos.SelectedItem IsNot Nothing
        BtnCotExportarPdf.Enabled = BtnCotAbrirPdf.Enabled
    End Sub

    Private Sub CambiarCotizacion(sender As Object, e As EventArgs) Handles TxtCotProveedor.TextChanged, TxtCotNumero.TextChanged, TxtCotTotal.TextChanged, TxtCotPago.TextChanged, TxtCotEntrega.TextChanged, TxtCotObservaciones.TextChanged, CmbCotMoneda.TextChanged, DtpCotFecha.ValueChanged, DtpCotVencimiento.ValueChanged
        If cargandoCotizacion OrElse Not editandoCotizacion Then Return
        If sender Is TxtCotProveedor Then cuentaCotizacion = Nothing
        cotizacionModificada = True
    End Sub

    Private Sub BtnCotBuscarProveedor_Click(sender As Object, e As EventArgs) Handles BtnCotBuscarProveedor.Click
        If Not editandoCotizacion Then Return
        Using dlg As New frmProveedoresSelector()
            If dlg.ShowDialog(Me) = DialogResult.OK AndAlso dlg.Seleccion IsNot Nothing Then
                TxtCotProveedor.Text = Convert.ToString(dlg.Seleccion("Nombre"))
                cuentaCotizacion = Convert.ToInt32(dlg.Seleccion("NroCuenta"))
                cotizacionModificada = True
            End If
        End Using
    End Sub

    ' Aplica la copia a la lista en memoria. El servicio y la transaccion SQL no cambian.
    Private Function AplicarCotizacion() As Boolean
        If Not editandoCotizacion Then Return True
        Try
            Dim total As Decimal? = Nothing
            If TxtCotTotal.Text.Trim() <> "" Then
                Dim valor As Decimal
                If Not Decimal.TryParse(TxtCotTotal.Text, NumberStyles.Number, CultureInfo.CurrentCulture, valor) Then
                    Throw New InvalidOperationException("Revise el total de la cotización.")
                End If
                total = valor
            End If
            Dim cot As New SolicitudCompraUsoCotizacion With {
                .IdCotizacion = cotizacionActual.IdCotizacion, .NroCuentaProveedor = cuentaCotizacion,
                .ProveedorNombre = TxtCotProveedor.Text.Trim(), .NumeroCotizacion = TxtCotNumero.Text.Trim(),
                .FechaCotizacion = DtpCotFecha.Value.Date,
                .FechaVencimiento = If(DtpCotVencimiento.Checked, CType(DtpCotVencimiento.Value.Date, Date?), Nothing),
                .Moneda = CmbCotMoneda.Text.Trim().ToUpperInvariant(), .ImporteTotal = total,
                .CondicionPago = TxtCotPago.Text.Trim(), .PlazoEntrega = TxtCotEntrega.Text.Trim(),
                .Observaciones = TxtCotObservaciones.Text.Trim(), .Adjuntos = adjuntosCotizacion.ToList(),
                .ImporteNeto = cotizacionActual.ImporteNeto, .ImporteIVA = cotizacionActual.ImporteIVA,
                .OtrosImportes = cotizacionActual.OtrosImportes}
            SolicitudCompraUsoService.ValidarCotizacion(cot)
            Dim indice = documento.Cotizaciones.IndexOf(cotizacionActual)
            If indice < 0 Then
                documento.Cotizaciones.Add(cot)
            Else
                documento.Cotizaciones(indice) = cot
            End If
            editandoCotizacion = False
            cotizacionModificada = False
            modificado = True
            SeleccionarCotizacion(cot)
            Return True
        Catch ex As Exception
            Pestanas.SelectedTab = TabCotizaciones
            MessageBox.Show(Me, ex.Message, "Cotización", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End Try
    End Function

    Private Sub BtnAplicarCot_Click(sender As Object, e As EventArgs) Handles BtnAplicarCot.Click
        AplicarCotizacion()
    End Sub

    Private Sub BtnCancelarCot_Click(sender As Object, e As EventArgs) Handles BtnCancelarCot.Click
        If cotizacionModificada AndAlso MessageBox.Show(Me, "¿Descartar los cambios de esta cotización?", "Cotización", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
        editandoCotizacion = False
        MostrarCotizacionSeleccionada()
    End Sub

    Private Sub RefrescarPdf()
        LstCotArchivos.DataSource = Nothing
        LstCotArchivos.DataSource = adjuntosCotizacion.ToList()
    End Sub

    Private Sub LstCotArchivos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstCotArchivos.SelectedIndexChanged
        AplicarEstadoCotizacion()
    End Sub

    Private Sub BtnCotAdjuntar_Click(sender As Object, e As EventArgs) Handles BtnCotAdjuntar.Click
        If Not editandoCotizacion Then Return
        Using dlg As New OpenFileDialog With {.Title = "Adjuntar cotización", .Filter = "Archivos PDF (*.pdf)|*.pdf", .Multiselect = True}
            If dlg.ShowDialog(Me) <> DialogResult.OK Then Return
            For Each ruta In dlg.FileNames
                Try
                    Dim bytes = File.ReadAllBytes(ruta)
                    SolicitudCompraUsoService.ValidarPdf(bytes)
                    adjuntosCotizacion.Add(New SolicitudCompraUsoPdf With {.NombreArchivo = Path.GetFileName(ruta), .TamanoBytes = bytes.LongLength, .Contenido = bytes})
                    cotizacionModificada = True
                Catch ex As Exception
                    MessageBox.Show(Me, Path.GetFileName(ruta) & ": " & ex.Message, "PDF", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Try
            Next
            RefrescarPdf()
        End Using
    End Sub

    Private Sub BtnCotQuitarPdf_Click(sender As Object, e As EventArgs) Handles BtnCotQuitarPdf.Click
        If Not editandoCotizacion Then Return
        Dim pdf = TryCast(LstCotArchivos.SelectedItem, SolicitudCompraUsoPdf)
        If pdf Is Nothing Then Return
        If MessageBox.Show(Me, "¿Quitar " & pdf.NombreArchivo & "?", "PDF", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
        adjuntosCotizacion.Remove(pdf)
        cotizacionModificada = True
        RefrescarPdf()
    End Sub

    Private Function ContenidoPdf(pdf As SolicitudCompraUsoPdf) As Byte()
        Return If(pdf.Contenido, servicio.LeerPdf(pdf.IdAdjunto, cotizacionActual.IdCotizacion))
    End Function

    Private Sub BtnCotAbrirPdf_Click(sender As Object, e As EventArgs) Handles BtnCotAbrirPdf.Click
        Dim pdf = TryCast(LstCotArchivos.SelectedItem, SolicitudCompraUsoPdf)
        If pdf Is Nothing Then Return
        Try
            Dim carpeta = Path.Combine(Path.GetTempPath(), "Proveedores", "Cotizaciones", Guid.NewGuid().ToString("N"))
            Directory.CreateDirectory(carpeta)
            Dim ruta = Path.Combine(carpeta, Path.GetFileName(pdf.NombreArchivo))
            File.WriteAllBytes(ruta, ContenidoPdf(pdf))
            Process.Start(New ProcessStartInfo(ruta) With {.UseShellExecute = True})
        Catch ex As Exception
            MessageBox.Show(Me, "No se pudo abrir el PDF. " & ex.Message, "PDF", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnCotExportarPdf_Click(sender As Object, e As EventArgs) Handles BtnCotExportarPdf.Click
        Dim pdf = TryCast(LstCotArchivos.SelectedItem, SolicitudCompraUsoPdf)
        If pdf Is Nothing Then Return
        Using dlg As New SaveFileDialog With {.Filter = "Archivos PDF (*.pdf)|*.pdf", .FileName = Path.GetFileName(pdf.NombreArchivo), .DefaultExt = "pdf", .AddExtension = True}
            If dlg.ShowDialog(Me) <> DialogResult.OK Then Return
            Try
                File.WriteAllBytes(dlg.FileName, ContenidoPdf(pdf))
            Catch ex As Exception
                MessageBox.Show(Me, "No se pudo exportar el PDF. " & ex.Message, "PDF", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub


End Class

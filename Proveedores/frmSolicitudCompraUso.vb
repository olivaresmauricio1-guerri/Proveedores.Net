Imports System.ComponentModel

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
        If servicio Is Nothing Then servicio = New SolicitudCompraUsoService()
        Try
            NuevaSolicitud()
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
            GridCotizaciones.DataSource = doc.Cotizaciones
            modificado = False
            AplicarEstado()
            ConfigurarGridDetalle()
        Finally
            ' Reactiva la deteccion de cambios incluso si la carga de controles falla.
            cargando = False
        End Try
    End Sub

    ' Permite editar solo documentos en BORRADOR o DEVUELTA.
    ' Las cotizaciones se editan desde su ventana, no directamente en la grilla.
    Private Sub AplicarEstado()
        Dim editar = documento IsNot Nothing AndAlso documento.Editable
        For Each c As Control In New Control() {CmbSector, DtpFecha, DtpNecesidad, CmbPrioridad, BtnAgregarItem, BtnQuitarItem, BtnAgregarCot, BtnQuitarCot, BtnGuardar, BtnEnviar}
            c.Enabled = editar
        Next
        TxtMotivo.ReadOnly = Not editar
        TxtObservaciones.ReadOnly = Not editar
        GridDetalle.ReadOnly = Not editar
        GridCotizaciones.ReadOnly = True
        BtnEditarCot.Text = If(editar, "Ver / editar cotización", "Ver cotización / PDF")
        BtnEditarCot.Enabled = documento IsNot Nothing
    End Sub

    ' Marca cambios del usuario, ignorando cargas programaticas y documentos no editables.
    Private Sub MarcarCambio(sender As Object, e As EventArgs)
        If Not cargando AndAlso documento IsNot Nothing AndAlso documento.Editable Then modificado = True
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
        If documento Is Nothing OrElse Not documento.Editable Then Return
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
        If documento Is Nothing OrElse Not documento.Editable Then Return
        Dim item = TryCast(GridDetalle.CurrentRow?.DataBoundItem, SolicitudCompraUsoItem)
        If item Is Nothing Then Return
        If MessageBox.Show(Me, "¿Quitar el renglón seleccionado?", "Detalle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
        GridDetalle.CancelEdit()
        documento.Items.Remove(item)
        modificado = True
    End Sub

    ' Abre una cotizacion nueva y la incorpora al documento solo si se acepta el dialogo.
    Private Sub BtnAgregarCot_Click(sender As Object, e As EventArgs) Handles BtnAgregarCot.Click
        If documento Is Nothing OrElse Not documento.Editable Then Return
        Using dlg As New frmCotizacionCompraUso(New SolicitudCompraUsoCotizacion(), True, servicio)
            If dlg.ShowDialog(Me) = DialogResult.OK Then
                documento.Cotizaciones.Add(dlg.Resultado)
                modificado = True
            End If
        End Using
    End Sub

    ' El boton y el doble clic comparten la misma apertura de cotizacion.
    Private Sub BtnEditarCot_Click(sender As Object, e As EventArgs) Handles BtnEditarCot.Click
        EditarCotizacion()
    End Sub

    ' Ignora el doble clic en encabezados; abre la cotizacion de una fila de datos.
    Private Sub GridCotizaciones_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridCotizaciones.CellDoubleClick
        If e.RowIndex >= 0 Then EditarCotizacion()
    End Sub

    ' Abre la cotizacion seleccionada en edicion o consulta segun el estado del documento.
    ' Si se acepta una edicion, reemplaza el elemento por el resultado del dialogo.
    Private Sub EditarCotizacion()
        If documento Is Nothing Then Return
        Dim cot = TryCast(GridCotizaciones.CurrentRow?.DataBoundItem, SolicitudCompraUsoCotizacion)
        If cot Is Nothing Then Return
        Using dlg As New frmCotizacionCompraUso(cot, documento.Editable, servicio)
            If dlg.ShowDialog(Me) = DialogResult.OK Then
                Dim indice = documento.Cotizaciones.IndexOf(cot)
                documento.Cotizaciones(indice) = dlg.Resultado
                modificado = True
            End If
        End Using
    End Sub

    ' Quita de memoria la cotizacion y sus adjuntos; SQL se sincroniza al guardar.
    Private Sub BtnQuitarCot_Click(sender As Object, e As EventArgs) Handles BtnQuitarCot.Click
        If documento Is Nothing OrElse Not documento.Editable Then Return
        Dim cot = TryCast(GridCotizaciones.CurrentRow?.DataBoundItem, SolicitudCompraUsoCotizacion)
        If cot Is Nothing Then Return
        If MessageBox.Show(Me, "¿Quitar esta cotización y sus PDF de la solicitud?", "Cotización", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
        documento.Cotizaciones.Remove(cot)
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
        If documento Is Nothing OrElse Not documento.Editable Then Return
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
        Return Not modificado OrElse MessageBox.Show(Me, "Hay cambios sin guardar. ¿Desea descartarlos?", "Solicitud Compras", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes
    End Function

    ' Antes de reemplazar el documento por uno nuevo, protege los cambios pendientes.
    Private Sub BtnNueva_Click(sender As Object, e As EventArgs) Handles BtnNueva.Click
        If Not PuedeDescartar() Then Return
        Try
            NuevaSolicitud()
        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, "Solicitud Compras", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Abre el selector y carga la solicitud elegida si el usuario acepta.
    Private Sub BtnAbrir_Click(sender As Object, e As EventArgs) Handles BtnAbrir.Click
        If Not PuedeDescartar() Then Return
        Using dlg As New frmSeleccionSolicitudCompraUso(servicio)
            If dlg.ShowDialog(Me) = DialogResult.OK AndAlso dlg.IdSeleccionado.HasValue Then AbrirSolicitud(dlg.IdSeleccionado.Value)
        End Using
    End Sub

    ' Consulta el documento completo por ID y lo muestra; informa errores de carga.
    Public Sub AbrirSolicitud(id As Integer)
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
            GridDetalle.Columns("Descripcion").HeaderText = "Proveedor"
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


End Class

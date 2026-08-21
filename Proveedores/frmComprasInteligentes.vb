Imports System.Net.Http
Imports System.Net.Http.Json
Imports System.Text
Imports System.Text.Json

Public Class FrmComprasInteligentes

    ' URL de la API — cambiar en producción
    Private Const API_URL As String = "http://localhost:5017/api/compras"
    Private ReadOnly _httpClient As New HttpClient()

    ' Clases para deserializar respuestas
    Private Class ProveedorDto
        Public Property Nombre As String
        Public Property Producto As String
        Public Property Precio As String
        Public Property Url As String
        Public Property Observaciones As String
        Public Property EsRecomendado As Boolean
    End Class

    Private Class BusquedaResponse
        Public Property BusquedaId As Integer
        Public Property Insumo As String
        Public Property AnalisisIA As String
        Public Property Proveedores As List(Of ProveedorDto)
        Public Property Recomendacion As String
        Public Property Fecha As DateTime
    End Class

    Private Class BusquedaHistorial
        Public Property Id As Integer
        Public Property Insumo As String
        Public Property Categoria As String
        Public Property FechaBusqueda As DateTime
        Public Property Usuario As String
    End Class

    Private Class OrdenCompra
        Public Property Id As Integer
        Public Property Insumo As String
        Public Property Proveedor As String
        Public Property Precio As String
        Public Property Estado As String
        Public Property FechaCreacion As DateTime
    End Class

#Region "Controles del formulario"

    Private WithEvents TabControl1 As New TabControl()
    Private WithEvents TabBusqueda As New TabPage("🔍 Buscar Precios")
    Private WithEvents TabHistorial As New TabPage("📋 Historial")
    Private WithEvents TabOrdenes As New TabPage("📦 Órdenes")

    ' Tab Búsqueda
    Private WithEvents TxtInsumo As New TextBox()
    Private WithEvents CmbCategoria As New ComboBox()
    Private WithEvents TxtCantidad As New TextBox()
    Private WithEvents TxtObservaciones As New TextBox()
    Private WithEvents BtnBuscar As New Button()
    Private WithEvents BtnLimpiar As New Button()
    Private WithEvents LblEstado As New Label()
    Private WithEvents PnlResultado As New Panel()
    Private WithEvents TxtAnalisis As New RichTextBox()
    Private WithEvents LvwProveedores As New ListView()
    Private WithEvents TxtRecomendacion As New RichTextBox()
    Private WithEvents BtnCrearOrden As New Button()
    Private WithEvents PrgBusqueda As New ProgressBar()

    ' Tab Historial
    Private WithEvents LvwHistorial As New ListView()
    Private WithEvents BtnRefrescarHistorial As New Button()

    ' Tab Órdenes
    Private WithEvents LvwOrdenes As New ListView()
    Private WithEvents BtnRefrescarOrdenes As New Button()
    Private WithEvents CmbEstado As New ComboBox()
    Private WithEvents BtnActualizarEstado As New Button()

    ' Resultado actual
    Private _ultimaBusqueda As BusquedaResponse = Nothing

#End Region

    Private Sub FrmComprasInteligentes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Guerrini Neumáticos — Compras Inteligentes con IA"
        Me.Size = New Size(1100, 750)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.FromArgb(245, 245, 245)
        Me.Font = New Font("Segoe UI", 9)

        ConfigurarFormulario()
        CargarHistorial()
        CargarOrdenes()
    End Sub

    Private Sub ConfigurarFormulario()

        ' ── TABCONTROL ─────────────────────────────────────────
        TabControl1.Dock = DockStyle.Fill
        TabControl1.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        TabControl1.Controls.Add(TabBusqueda)
        TabControl1.Controls.Add(TabHistorial)
        TabControl1.Controls.Add(TabOrdenes)
        Me.Controls.Add(TabControl1)

        ' ── TAB BÚSQUEDA ───────────────────────────────────────
        Dim pnlForm As New Panel()
        pnlForm.Dock = DockStyle.Top
        pnlForm.Height = 205
        pnlForm.Padding = New Padding(10)
        pnlForm.BackColor = Color.White


        ' Título
        Dim lblTitulo As New Label()
        lblTitulo.Text = "Búsqueda Inteligente de Precios"
        lblTitulo.Font = New Font("Segoe UI", 14, FontStyle.Bold)
        lblTitulo.ForeColor = Color.FromArgb(31, 78, 121)
        lblTitulo.Location = New Point(10, 10)
        lblTitulo.Size = New Size(500, 30)
        pnlForm.Controls.Add(lblTitulo)

        Dim lblSub As New Label()
        lblSub.Text = "Claude buscará los mejores precios en internet para el insumo indicado"
        lblSub.ForeColor = Color.Gray
        lblSub.Location = New Point(10, 42)
        lblSub.Size = New Size(600, 20)
        pnlForm.Controls.Add(lblSub)

        ' Insumo
        AgregarLabel(pnlForm, "Insumo / Servicio a cotizar *", 10, 70)
        TxtInsumo.Location = New Point(10, 88)
        TxtInsumo.Size = New Size(400, 28)
        TxtInsumo.Font = New Font("Segoe UI", 10)
        TxtInsumo.PlaceholderText = "Ej: Resmas A4, flete Mendoza-Buenos Aires, servicio limpieza..."
        pnlForm.Controls.Add(TxtInsumo)

        ' Categoría
        AgregarLabel(pnlForm, "Categoría *", 430, 70, 180)
        CmbCategoria.Location = New Point(430, 88)
        CmbCategoria.Size = New Size(180, 28)
        CmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList
        CmbCategoria.Items.AddRange(New String() {
            "Insumos de oficina",
            "Fletes y logística",
            "Servicios generales",
            "Limpieza e higiene",
            "Informática y tecnología",
            "Mantenimiento",
            "Otros"
        })
        CmbCategoria.SelectedIndex = 0
        pnlForm.Controls.Add(CmbCategoria)

        ' Cantidad
        AgregarLabel(pnlForm, "Cantidad / Frecuencia", 630, 70, 180)
        TxtCantidad.Location = New Point(630, 88)
        TxtCantidad.Size = New Size(170, 28)
        TxtCantidad.PlaceholderText = "Ej: 10 resmas, mensual..."
        pnlForm.Controls.Add(TxtCantidad)

        ' Observaciones
        AgregarLabel(pnlForm, "Observaciones adicionales", 10, 120)
        TxtObservaciones.Location = New Point(10, 138)
        TxtObservaciones.Size = New Size(600, 28)
        TxtObservaciones.PlaceholderText = "Marca preferida, requisitos especiales, zona de entrega..."
        pnlForm.Controls.Add(TxtObservaciones)

        ' Botones
        BtnBuscar.Text = "🔍 Buscar Mejores Precios"
        BtnBuscar.Location = New Point(630, 132)
        BtnBuscar.Size = New Size(200, 36)
        BtnBuscar.BackColor = Color.FromArgb(31, 78, 121)
        BtnBuscar.ForeColor = Color.White
        BtnBuscar.FlatStyle = FlatStyle.Flat
        BtnBuscar.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        pnlForm.Controls.Add(BtnBuscar)

        BtnLimpiar.Text = "Limpiar"
        BtnLimpiar.Location = New Point(840, 132)
        BtnLimpiar.Size = New Size(80, 36)
        BtnLimpiar.FlatStyle = FlatStyle.Flat
        pnlForm.Controls.Add(BtnLimpiar)

        ' Progress bar
        PrgBusqueda.Location = New Point(10, 168)
        PrgBusqueda.Size = New Size(910, 6)
        PrgBusqueda.Style = ProgressBarStyle.Marquee
        PrgBusqueda.Visible = False
        pnlForm.Controls.Add(PrgBusqueda)

        ' Estado
        LblEstado.Location = New Point(10, 180)
        LblEstado.Size = New Size(910, 20)
        LblEstado.ForeColor = Color.Gray
        pnlForm.Controls.Add(LblEstado)

        ' Panel resultado
        PnlResultado.Dock = DockStyle.Fill
        PnlResultado.Padding = New Padding(10)
        TabBusqueda.Controls.Add(PnlResultado)


        TabBusqueda.Controls.Add(pnlForm)

        ' Análisis IA
        Dim lblAnalisis As New Label()
        lblAnalisis.Text = "📊 Análisis de mercado:"
        lblAnalisis.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblAnalisis.ForeColor = Color.FromArgb(31, 78, 121)
        lblAnalisis.Location = New Point(10, 5)
        lblAnalisis.Size = New Size(200, 20)
        PnlResultado.Controls.Add(lblAnalisis)

        TxtAnalisis.Location = New Point(10, 28)
        TxtAnalisis.Size = New Size(1060, 60)
        TxtAnalisis.BackColor = Color.FromArgb(240, 248, 255)
        TxtAnalisis.BorderStyle = BorderStyle.FixedSingle
        TxtAnalisis.ReadOnly = True
        TxtAnalisis.Font = New Font("Segoe UI", 9)
        PnlResultado.Controls.Add(TxtAnalisis)

        ' ListView proveedores
        Dim lblProveedores As New Label()
        lblProveedores.Text = "🏪 Proveedores encontrados:"
        lblProveedores.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblProveedores.ForeColor = Color.FromArgb(31, 78, 121)
        lblProveedores.Location = New Point(10, 100)
        lblProveedores.Size = New Size(300, 20)
        PnlResultado.Controls.Add(lblProveedores)

        LvwProveedores.Location = New Point(10, 123)
        LvwProveedores.Size = New Size(1060, 180)
        LvwProveedores.View = View.Details
        LvwProveedores.FullRowSelect = True
        LvwProveedores.GridLines = True
        LvwProveedores.Font = New Font("Segoe UI", 9)
        LvwProveedores.Columns.Add("Proveedor", 200)
        LvwProveedores.Columns.Add("Producto", 250)
        LvwProveedores.Columns.Add("Precio", 120)
        LvwProveedores.Columns.Add("Recomendado", 90)
        LvwProveedores.Columns.Add("Observaciones", 280)
        LvwProveedores.Columns.Add("URL", 120)
        PnlResultado.Controls.Add(LvwProveedores)

        ' Recomendación
        Dim lblRec As New Label()
        lblRec.Text = "✅ Recomendación de la IA:"
        lblRec.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblRec.ForeColor = Color.FromArgb(30, 113, 69)
        lblRec.Location = New Point(10, 315)
        lblRec.Size = New Size(300, 20)
        PnlResultado.Controls.Add(lblRec)

        TxtRecomendacion.Location = New Point(10, 338)
        TxtRecomendacion.Size = New Size(860, 55)
        TxtRecomendacion.BackColor = Color.FromArgb(235, 247, 240)
        TxtRecomendacion.BorderStyle = BorderStyle.FixedSingle
        TxtRecomendacion.ReadOnly = True
        TxtRecomendacion.Font = New Font("Segoe UI", 9)
        PnlResultado.Controls.Add(TxtRecomendacion)

        BtnCrearOrden.Text = "📦 Crear Orden de Compra"
        BtnCrearOrden.Location = New Point(882, 338)
        BtnCrearOrden.Size = New Size(188, 55)
        BtnCrearOrden.BackColor = Color.FromArgb(30, 113, 69)
        BtnCrearOrden.ForeColor = Color.White
        BtnCrearOrden.FlatStyle = FlatStyle.Flat
        BtnCrearOrden.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        BtnCrearOrden.Enabled = False
        PnlResultado.Controls.Add(BtnCrearOrden)

        ' ── TAB HISTORIAL ──────────────────────────────────────────
        Dim pnlHistHeader As New Panel()
        pnlHistHeader.Dock = DockStyle.Top
        pnlHistHeader.Height = 50
        pnlHistHeader.Padding = New Padding(10, 10, 10, 0)

        Dim lblHistTitulo As New Label()
        lblHistTitulo.Text = "Historial de búsquedas"
        lblHistTitulo.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        lblHistTitulo.ForeColor = Color.FromArgb(31, 78, 121)
        lblHistTitulo.Location = New Point(10, 10)
        lblHistTitulo.Size = New Size(300, 28)
        pnlHistHeader.Controls.Add(lblHistTitulo)

        BtnRefrescarHistorial.Text = "🔄 Refrescar"
        BtnRefrescarHistorial.Location = New Point(900, 8)
        BtnRefrescarHistorial.Size = New Size(120, 32)
        BtnRefrescarHistorial.FlatStyle = FlatStyle.Flat
        BtnRefrescarHistorial.BackColor = Color.FromArgb(31, 78, 121)
        BtnRefrescarHistorial.ForeColor = Color.White
        pnlHistHeader.Controls.Add(BtnRefrescarHistorial)

        LvwHistorial.Dock = DockStyle.Fill
        LvwHistorial.View = View.Details
        LvwHistorial.FullRowSelect = True
        LvwHistorial.GridLines = True
        LvwHistorial.Font = New Font("Segoe UI", 9)
        LvwHistorial.Columns.Add("ID", 50)
        LvwHistorial.Columns.Add("Insumo", 350)
        LvwHistorial.Columns.Add("Categoría", 180)
        LvwHistorial.Columns.Add("Usuario", 120)
        LvwHistorial.Columns.Add("Fecha", 160)

        ' ORDEN CRÍTICO: primero Top, último Fill
        TabHistorial.Controls.Add(LvwHistorial)
        TabHistorial.Controls.Add(pnlHistHeader)

        ' ── TAB ÓRDENES ────────────────────────────────────────────
        Dim pnlOrdHeader As New Panel()
        pnlOrdHeader.Dock = DockStyle.Top
        pnlOrdHeader.Height = 50
        pnlOrdHeader.Padding = New Padding(10, 10, 10, 0)

        Dim lblOrdTitulo As New Label()
        lblOrdTitulo.Text = "Órdenes de compra"
        lblOrdTitulo.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        lblOrdTitulo.ForeColor = Color.FromArgb(31, 78, 121)
        lblOrdTitulo.Location = New Point(10, 10)
        lblOrdTitulo.Size = New Size(300, 28)
        pnlOrdHeader.Controls.Add(lblOrdTitulo)

        CmbEstado.Location = New Point(620, 10)
        CmbEstado.Size = New Size(150, 28)
        CmbEstado.DropDownStyle = ComboBoxStyle.DropDownList
        CmbEstado.Items.AddRange(New String() {
            "Pendiente", "Aprobada", "En proceso", "Completada", "Cancelada"
        })
        CmbEstado.SelectedIndex = 0
        pnlOrdHeader.Controls.Add(CmbEstado)

        BtnActualizarEstado.Text = "Actualizar Estado"
        BtnActualizarEstado.Location = New Point(780, 8)
        BtnActualizarEstado.Size = New Size(140, 32)
        BtnActualizarEstado.FlatStyle = FlatStyle.Flat
        BtnActualizarEstado.BackColor = Color.FromArgb(31, 78, 121)
        BtnActualizarEstado.ForeColor = Color.White
        pnlOrdHeader.Controls.Add(BtnActualizarEstado)

        BtnRefrescarOrdenes.Text = "🔄 Refrescar"
        BtnRefrescarOrdenes.Location = New Point(930, 8)
        BtnRefrescarOrdenes.Size = New Size(100, 32)
        BtnRefrescarOrdenes.FlatStyle = FlatStyle.Flat
        pnlOrdHeader.Controls.Add(BtnRefrescarOrdenes)

        LvwOrdenes.Dock = DockStyle.Fill
        LvwOrdenes.View = View.Details
        LvwOrdenes.FullRowSelect = True
        LvwOrdenes.GridLines = True
        LvwOrdenes.Font = New Font("Segoe UI", 9)
        LvwOrdenes.Columns.Add("ID", 50)
        LvwOrdenes.Columns.Add("Insumo", 280)
        LvwOrdenes.Columns.Add("Proveedor", 200)
        LvwOrdenes.Columns.Add("Precio", 120)
        LvwOrdenes.Columns.Add("Estado", 100)
        LvwOrdenes.Columns.Add("Fecha", 160)

        ' ORDEN CRÍTICO: primero Fill, después Top
        ' WinForms procesa en orden inverso los Dock
        TabOrdenes.Controls.Add(LvwOrdenes)
        TabOrdenes.Controls.Add(pnlOrdHeader)

    End Sub

    Private Sub AgregarLabel(parent As Control, texto As String, x As Integer, y As Integer, Optional ancho As Integer = 300)
        Dim lbl As New Label()
        lbl.Text = texto
        lbl.Location = New Point(x, y)
        lbl.Size = New Size(ancho, 18)
        lbl.ForeColor = Color.FromArgb(80, 80, 80)
        lbl.Font = New Font("Segoe UI", 8, FontStyle.Bold)
        parent.Controls.Add(lbl)
    End Sub

#Region "Eventos de búsqueda"

    Private Async Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        If String.IsNullOrWhiteSpace(TxtInsumo.Text) Then
            MessageBox.Show("Por favor ingresá el insumo a buscar.", "Campo requerido",
                MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtInsumo.Focus()
            Return
        End If

        ' Mostrar estado de carga
        BtnBuscar.Enabled = False
        PrgBusqueda.Visible = True
        LblEstado.Text = "⏳ Consultando precios en internet con IA... esto puede tardar unos segundos."
        LblEstado.ForeColor = Color.FromArgb(31, 78, 121)
        LimpiarResultados()

        Try
            Dim request = New With {
                .insumo = TxtInsumo.Text.Trim(),
                .categoria = If(CmbCategoria.SelectedItem Is Nothing, "Otros", CmbCategoria.SelectedItem.ToString()),
                .cantidadRequerida = If(String.IsNullOrWhiteSpace(TxtCantidad.Text), Nothing, TxtCantidad.Text.Trim()),
                .observaciones = If(String.IsNullOrWhiteSpace(TxtObservaciones.Text), Nothing, TxtObservaciones.Text.Trim()),
                .usuario = Environment.UserName
            }

            Dim json = JsonSerializer.Serialize(request)
            Dim content = New StringContent(json, Encoding.UTF8, "application/json")

            Dim response = Await _httpClient.PostAsync($"{API_URL}/buscar", content)
            Dim responseJson = Await response.Content.ReadAsStringAsync()

            If Not response.IsSuccessStatusCode Then
                Throw New Exception($"Error de API: {response.StatusCode} — {responseJson}")
            End If

            Dim options = New JsonSerializerOptions() With {
                .PropertyNameCaseInsensitive = True
            }
            _ultimaBusqueda = JsonSerializer.Deserialize(Of BusquedaResponse)(responseJson, options)

            MostrarResultados(_ultimaBusqueda)

            LblEstado.Text = $"✅ Búsqueda completada — {If(_ultimaBusqueda.Proveedores Is Nothing, 0, _ultimaBusqueda.Proveedores.Count)} proveedores encontrados"
            LblEstado.ForeColor = Color.FromArgb(30, 113, 69)
            BtnCrearOrden.Enabled = _ultimaBusqueda.Proveedores?.Count > 0

        Catch ex As Exception
            LblEstado.Text = $"⚠️ Error: {ex.Message}"
            LblEstado.ForeColor = Color.Red
            MessageBox.Show($"Error al buscar precios:{Environment.NewLine}{ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            BtnBuscar.Enabled = True
            PrgBusqueda.Visible = False
        End Try
    End Sub

    Private Sub MostrarResultados(resultado As BusquedaResponse)
        If resultado Is Nothing Then Return

        TxtAnalisis.Text = resultado.AnalisisIA
        TxtRecomendacion.Text = resultado.Recomendacion

        LvwProveedores.Items.Clear()
        If resultado.Proveedores IsNot Nothing Then
            For Each prov In resultado.Proveedores
                Dim item As New ListViewItem(prov.Nombre)
                item.SubItems.Add(prov.Producto)
                item.SubItems.Add(prov.Precio)
                item.SubItems.Add(If(prov.EsRecomendado, "⭐ Sí", ""))
                item.SubItems.Add(If(prov.Observaciones, ""))
                item.SubItems.Add(If(prov.Url, ""))

                If prov.EsRecomendado Then
                    item.BackColor = Color.FromArgb(235, 247, 240)
                    item.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                End If

                LvwProveedores.Items.Add(item)
            Next
        End If
    End Sub

    Private Sub LimpiarResultados()
        TxtAnalisis.Clear()
        TxtRecomendacion.Clear()
        LvwProveedores.Items.Clear()
        BtnCrearOrden.Enabled = False
        _ultimaBusqueda = Nothing
    End Sub

    Private Sub BtnLimpiar_Click(sender As Object, e As EventArgs) Handles BtnLimpiar.Click
        TxtInsumo.Clear()
        TxtCantidad.Clear()
        TxtObservaciones.Clear()
        CmbCategoria.SelectedIndex = 0
        LimpiarResultados()
        LblEstado.Text = ""
        TxtInsumo.Focus()
    End Sub

#End Region

#Region "Crear Orden de Compra"

    Private Async Sub BtnCrearOrden_Click(sender As Object, e As EventArgs) Handles BtnCrearOrden.Click
        If _ultimaBusqueda Is Nothing Then Return

        ' Si hay un proveedor seleccionado usarlo, sino usar el recomendado
        Dim proveedorSeleccionado As ProveedorDto = Nothing

        If LvwProveedores.SelectedItems.Count > 0 Then
            Dim idx = LvwProveedores.SelectedIndices(0)
            proveedorSeleccionado = _ultimaBusqueda.Proveedores(idx)
        Else
            proveedorSeleccionado = _ultimaBusqueda.Proveedores.FirstOrDefault(Function(p) p.EsRecomendado)
            If proveedorSeleccionado Is Nothing Then
                proveedorSeleccionado = _ultimaBusqueda.Proveedores.FirstOrDefault()
            End If
        End If

        If proveedorSeleccionado Is Nothing Then
            MessageBox.Show("No hay proveedor para crear la orden.", "Sin proveedor",
                MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim confirm = MessageBox.Show(
            $"¿Crear orden de compra para:{Environment.NewLine}" &
            $"Insumo: {_ultimaBusqueda.Insumo}{Environment.NewLine}" &
            $"Proveedor: {proveedorSeleccionado.Nombre}{Environment.NewLine}" &
            $"Precio: {proveedorSeleccionado.Precio}",
            "Confirmar orden",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)

        If confirm <> DialogResult.Yes Then Return

        Try
            Dim request = New With {
                .busquedaCompraId = _ultimaBusqueda.BusquedaId,
                .insumo = _ultimaBusqueda.Insumo,
                .proveedor = proveedorSeleccionado.Nombre,
                .precio = proveedorSeleccionado.Precio,
                .cantidad = TxtCantidad.Text.Trim(),
                .observaciones = TxtObservaciones.Text.Trim()
            }

            Dim json = JsonSerializer.Serialize(request)
            Dim content = New StringContent(json, Encoding.UTF8, "application/json")
            Dim response = Await _httpClient.PostAsync($"{API_URL}/ordenes", content)

            If response.IsSuccessStatusCode Then
                MessageBox.Show("✅ Orden de compra creada correctamente.",
                    "Orden creada", MessageBoxButtons.OK, MessageBoxIcon.Information)
                CargarOrdenes()
                TabControl1.SelectedTab = TabOrdenes
            Else
                Throw New Exception(Await response.Content.ReadAsStringAsync())
            End If

        Catch ex As Exception
            MessageBox.Show($"Error al crear la orden:{Environment.NewLine}{ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Historial"

    Private Async Sub CargarHistorial()
        Try
            Dim response = Await _httpClient.GetAsync($"{API_URL}/historial?cantidad=100")
            Dim json = Await response.Content.ReadAsStringAsync()
            Dim options = New JsonSerializerOptions() With {.PropertyNameCaseInsensitive = True}
            Dim historial = JsonSerializer.Deserialize(Of List(Of BusquedaHistorial))(json, options)

            LvwHistorial.Items.Clear()
            If historial IsNot Nothing Then
                For Each item In historial
                    Dim lvi As New ListViewItem(item.Id.ToString())
                    lvi.SubItems.Add(item.Insumo)
                    lvi.SubItems.Add(item.Categoria)
                    lvi.SubItems.Add(item.Usuario)
                    lvi.SubItems.Add(item.FechaBusqueda.ToString("dd/MM/yyyy HH:mm"))
                    LvwHistorial.Items.Add(lvi)
                Next
            End If
        Catch ex As Exception
            ' Silencioso al cargar
        End Try
    End Sub

    Private Sub BtnRefrescarHistorial_Click(sender As Object, e As EventArgs) Handles BtnRefrescarHistorial.Click
        CargarHistorial()
    End Sub

#End Region

#Region "Órdenes"

    Private Async Sub CargarOrdenes()
        Try
            Dim response = Await _httpClient.GetAsync($"{API_URL}/ordenes")
            Dim json = Await response.Content.ReadAsStringAsync()
            Dim options = New JsonSerializerOptions() With {.PropertyNameCaseInsensitive = True}
            Dim ordenes = JsonSerializer.Deserialize(Of List(Of OrdenCompra))(json, options)

            LvwOrdenes.Items.Clear()
            If ordenes IsNot Nothing Then
                For Each orden In ordenes
                    Dim lvi As New ListViewItem(orden.Id.ToString())
                    lvi.SubItems.Add(orden.Insumo)
                    lvi.SubItems.Add(orden.Proveedor)
                    lvi.SubItems.Add(orden.Precio)
                    lvi.SubItems.Add(orden.Estado)
                    lvi.SubItems.Add(orden.FechaCreacion.ToString("dd/MM/yyyy HH:mm"))

                    Select Case orden.Estado
                        Case "Completada"
                            lvi.BackColor = Color.FromArgb(235, 247, 240)
                        Case "Cancelada"
                            lvi.BackColor = Color.FromArgb(255, 235, 235)
                        Case "Aprobada"
                            lvi.BackColor = Color.FromArgb(235, 244, 255)
                    End Select

                    LvwOrdenes.Items.Add(lvi)
                Next
            End If
        Catch ex As Exception
            ' Silencioso al cargar
        End Try
    End Sub

    Private Sub BtnRefrescarOrdenes_Click(sender As Object, e As EventArgs) Handles BtnRefrescarOrdenes.Click
        CargarOrdenes()
    End Sub

    Private Async Sub BtnActualizarEstado_Click(sender As Object, e As EventArgs) Handles BtnActualizarEstado.Click
        If LvwOrdenes.SelectedItems.Count = 0 Then
            MessageBox.Show("Seleccioná una orden primero.", "Sin selección",
                MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim id = Integer.Parse(LvwOrdenes.SelectedItems(0).Text)
        Dim nuevoEstado = CmbEstado.SelectedItem?.ToString()

        Try
            Dim json = JsonSerializer.Serialize(nuevoEstado)
            Dim content = New StringContent(json, Encoding.UTF8, "application/json")
            Dim response = Await _httpClient.PutAsync($"{API_URL}/ordenes/{id}/estado", content)

            If response.IsSuccessStatusCode Then
                CargarOrdenes()
            Else
                MessageBox.Show("Error al actualizar el estado.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            _httpClient?.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

End Class
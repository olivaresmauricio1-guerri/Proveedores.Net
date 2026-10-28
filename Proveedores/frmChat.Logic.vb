
Imports System.Threading

Partial Public Class frmChat
    Private Shared _instancia As frmChat
    Private _orquestador As ChatOrchestratorService
    Private _cancelacion As CancellationTokenSource
    Private _interfazInicializada As Boolean

    Private ReadOnly _txtConversacion As New RichTextBox()
    Private ReadOnly _txtPregunta As New TextBox()
    Private ReadOnly _btnEnviar As New Button()
    Private ReadOnly _btnCancelar As New Button()
    Private ReadOnly _btnLimpiar As New Button()
    Private ReadOnly _btnExportar As New Button()
    Private ReadOnly _lblEstado As New Label()
    Private ReadOnly _dgvResultados As New DataGridView()
    Private ReadOnly _tabsResultados As New TabControl()
    Private ReadOnly _tabDatos As New TabPage("Datos")
    Private ReadOnly _tabGrafico As New TabPage("Gráfico")
    Private ReadOnly _panelGrafico As New Panel With {.AutoScroll = True, .Dock = DockStyle.Fill}
    Private ReadOnly _vistaGrafico As New ChatChartControl()
    Private ReadOnly _btnGuardarGrafico As New Button()
    Private _contenedorChat As SplitContainer

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If _instancia Is Nothing OrElse _instancia.IsDisposed Then
            _instancia = New frmChat() With {.MdiParent = mdiParent}
        End If
        _instancia.Show()
        _instancia.BringToFront()
        _instancia.Focus()
    End Sub

    Private Async Sub frmChat_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If _interfazInicializada Then Return
        _interfazInicializada = True

        ConstruirInterfaz()
        _lblEstado.Text = "Comprobando Ollama y MCP..."
        Try
            _orquestador = New ChatOrchestratorService()
            Dim disponible = Await _orquestador.Ollama.EstaDisponibleAsync()
            If IsDisposed Then Return
            If Not disponible Then
                _lblEstado.Text = $"Ollama no disponible en {_orquestador.Ollama.BaseUrl}"
                _lblEstado.ForeColor = Color.DarkRed
                Return
            End If
            Dim cantidad = Await _orquestador.InicializarAsync()
            If IsDisposed Then Return
            _lblEstado.Text = $"{ChatMcpClientService.NombreEntorno} · {_orquestador.Ollama.Model} · MCP: {cantidad} consultas"
            _lblEstado.ForeColor = Color.DarkGreen
            _txtPregunta.Focus()
        Catch ex As Exception
            If IsDisposed Then Return
            _lblEstado.Text = "No se pudo conectar al asistente"
            _lblEstado.ForeColor = Color.DarkRed
            AppendMensaje("Sistema", ex.Message)
        End Try
    End Sub

    Private Sub ConstruirInterfaz()
        Text = "Asistente IA de Proveedores"
        MinimumSize = New Size(900, 600)
        Size = New Size(1100, 720)

        Dim contenedor As New SplitContainer With {
            .Dock = DockStyle.Fill,
            .Orientation = Orientation.Horizontal,
            .SplitterDistance = 390,
            .Panel1MinSize = 250,
            .Panel2MinSize = 150
        }
        Dim panelConversacion As New Panel With {.Dock = DockStyle.Fill, .Padding = New Padding(10)}
        _contenedorChat = contenedor

        _lblEstado.Dock = DockStyle.Top
        _lblEstado.Height = 24
        _lblEstado.TextAlign = ContentAlignment.MiddleLeft

        _txtConversacion.Dock = DockStyle.Fill
        _txtConversacion.ReadOnly = True
        _txtConversacion.BackColor = Color.White
        _txtConversacion.Font = New Font("Segoe UI", 10.0F)
        _txtConversacion.BorderStyle = BorderStyle.FixedSingle

        Dim panelEntrada As New Panel With {.Dock = DockStyle.Bottom, .Height = 82, .Padding = New Padding(0, 8, 0, 0)}
        _txtPregunta.Multiline = True
        _txtPregunta.AcceptsReturn = True
        _txtPregunta.ScrollBars = ScrollBars.Vertical
        _txtPregunta.Dock = DockStyle.Fill
        _txtPregunta.Font = New Font("Segoe UI", 10.0F)
        AddHandler _txtPregunta.KeyDown, AddressOf TxtPregunta_KeyDown

        Dim panelBotones As New FlowLayoutPanel With {
            .Dock = DockStyle.Right,
            .Width = 375,
            .FlowDirection = FlowDirection.LeftToRight,
            .Padding = New Padding(8, 0, 0, 0)
        }
        ConfigurarBoton(_btnEnviar, "Enviar", 82)
        ConfigurarBoton(_btnCancelar, "Cancelar", 82)
        ConfigurarBoton(_btnLimpiar, "Limpiar", 82)
        ConfigurarBoton(_btnExportar, "Excel", 82)
        _btnCancelar.Enabled = False
        _btnExportar.Enabled = False
        AddHandler _btnEnviar.Click, AddressOf BtnEnviar_Click
        AddHandler _btnCancelar.Click, AddressOf BtnCancelar_Click
        AddHandler _btnLimpiar.Click, AddressOf BtnLimpiar_Click
        AddHandler _btnExportar.Click, AddressOf BtnExportar_Click
        panelBotones.Controls.AddRange(New Control() {_btnEnviar, _btnCancelar, _btnLimpiar, _btnExportar})

        panelEntrada.Controls.Add(_txtPregunta)
        panelEntrada.Controls.Add(panelBotones)
        panelConversacion.Controls.Add(_txtConversacion)
        panelConversacion.Controls.Add(panelEntrada)
        panelConversacion.Controls.Add(_lblEstado)
        contenedor.Panel1.Controls.Add(panelConversacion)

        _dgvResultados.Dock = DockStyle.Fill
        _dgvResultados.ReadOnly = True
        _dgvResultados.AllowUserToAddRows = False
        _dgvResultados.AllowUserToDeleteRows = False
        _dgvResultados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
        _dgvResultados.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        _dgvResultados.BackgroundColor = Color.White
        contenedor.Panel2.Padding = New Padding(10, 0, 10, 10)
        _tabsResultados.Dock = DockStyle.Fill
        _tabDatos.Controls.Add(_dgvResultados)
        _tabDatos.Padding = New Padding(4)
        _tabsResultados.TabPages.AddRange(New TabPage() {_tabDatos, _tabGrafico})
        Dim barraGrafico As New FlowLayoutPanel With {.Dock = DockStyle.Top, .Height = 40, .Padding = New Padding(4)}
        ConfigurarBoton(_btnGuardarGrafico, "Guardar PNG", 120)
        _btnGuardarGrafico.Enabled = False
        AddHandler _btnGuardarGrafico.Click, AddressOf BtnGuardarGrafico_Click
        barraGrafico.Controls.Add(_btnGuardarGrafico)
        _panelGrafico.Controls.Add(_vistaGrafico)
        AddHandler _panelGrafico.Resize, Sub() AjustarGrafico()
        _tabGrafico.Controls.Add(_panelGrafico)
        _tabGrafico.Controls.Add(barraGrafico)
        contenedor.Panel2.Controls.Add(_tabsResultados)

        Controls.Add(contenedor)
        AppendMensaje("Sistema", "Podés consultar proveedores, facturas pendientes, vehículos y servicios. También podés comparar vehículos, pedir rankings y exportar cualquier listado a Excel.")
    End Sub

    Private Shared Sub ConfigurarBoton(boton As Button, texto As String, ancho As Integer)
        boton.Text = texto
        boton.Width = ancho
        boton.Height = 34
        boton.Margin = New Padding(3, 0, 3, 0)
    End Sub

    Private Async Sub BtnEnviar_Click(sender As Object, e As EventArgs)
        Dim pregunta = _txtPregunta.Text.Trim()
        If pregunta = String.Empty OrElse _orquestador Is Nothing Then Return

        _txtPregunta.Clear()
        AppendMensaje("Usuario", pregunta)
        CambiarEstadoConsulta(True)
        _cancelacion = New CancellationTokenSource()

        Try
            Dim respuesta = Await _orquestador.EnviarAsync(pregunta, _cancelacion.Token)
            If IsDisposed Then Return
            AppendMensaje("Asistente", respuesta.Texto)
            MostrarResultadoTabular(respuesta)
            If respuesta.Grafico IsNot Nothing Then
                _vistaGrafico.Mostrar(respuesta.Grafico)
                _btnGuardarGrafico.Enabled = True
                _tabsResultados.SelectedTab = _tabGrafico
                _contenedorChat.SplitterDistance = Math.Min(_contenedorChat.SplitterDistance, Math.Max(_contenedorChat.Panel1MinSize, _contenedorChat.Height \ 3))
                AjustarGrafico()
                _txtConversacion.ScrollToCaret()
            ElseIf respuesta.Resultados.Any(Function(r) r.Nombre = "mostrar_grafico") Then
                LimpiarGrafico()
            End If
            If respuesta.SolicitarExportacionExcel Then
                ExportarResultadoActual()
            End If
            _lblEstado.Text = If(respuesta.HerramientasEjecutadas.Count = 0,
                                 "Listo",
                                 $"Listo · {String.Join(", ", respuesta.HerramientasEjecutadas)}")
            _lblEstado.ForeColor = If(respuesta.Resultados.Any(Function(r) Not r.Exito), Color.DarkRed, Color.DarkGreen)
        Catch ex As OperationCanceledException
            If IsDisposed Then Return
            AppendMensaje("Sistema", "Consulta cancelada.")
            _lblEstado.Text = "Consulta cancelada"
            _lblEstado.ForeColor = Color.DarkOrange
        Catch ex As Exception
            If IsDisposed Then Return
            _dgvResultados.DataSource = Nothing
            _btnExportar.Enabled = False
            LimpiarGrafico()
            AppendMensaje("Error", ex.Message)
            _lblEstado.Text = "Error al procesar la consulta"
            _lblEstado.ForeColor = Color.DarkRed
        Finally
            _cancelacion?.Dispose()
            _cancelacion = Nothing
            CambiarEstadoConsulta(False)
        End Try
    End Sub

    Private Sub MostrarResultadoTabular(respuesta As ChatOrchestratorResponse)
        If respuesta Is Nothing Then Return
        For Each resultado In respuesta.Resultados.AsEnumerable().Reverse().OrderBy(Function(r) If(r.Nombre = "ranking_ventas_stock" AndAlso r.Exito, 0, 1))
            If {"solicitar_exportacion_excel", "mostrar_grafico"}.Contains(resultado.Nombre) Then Continue For
            LimpiarGrafico()
            _tabsResultados.SelectedTab = _tabDatos
            _dgvResultados.DataSource = If(resultado.Exito, ChatResultTableService.CrearTabla(resultado.Datos, resultado.Nombre), Nothing)
            _btnExportar.Enabled = _dgvResultados.Rows.Cast(Of DataGridViewRow)().Any(Function(r) Not r.IsNewRow)
            Return
        Next
    End Sub

    Private Sub AppendMensaje(autor As String, mensaje As String)
        If _txtConversacion.TextLength > 0 Then _txtConversacion.AppendText(Environment.NewLine & Environment.NewLine)
        _txtConversacion.SelectionFont = New Font(_txtConversacion.Font, FontStyle.Bold)
        _txtConversacion.AppendText(autor & ":" & Environment.NewLine)
        _txtConversacion.SelectionFont = New Font(_txtConversacion.Font, FontStyle.Regular)
        _txtConversacion.AppendText(If(mensaje, String.Empty))
        _txtConversacion.SelectionStart = _txtConversacion.TextLength
        _txtConversacion.ScrollToCaret()
    End Sub

    Private Sub BtnExportar_Click(sender As Object, e As EventArgs)
        ExportarResultadoActual()
    End Sub

    Private Sub CambiarEstadoConsulta(consultando As Boolean)
        If IsDisposed Then Return
        _btnEnviar.Enabled = Not consultando
        _btnCancelar.Enabled = consultando
        _btnLimpiar.Enabled = Not consultando
        _txtPregunta.Enabled = Not consultando
        If consultando Then
            _lblEstado.Text = "Consultando..."
            _lblEstado.ForeColor = Color.DarkBlue
        Else
            _txtPregunta.Focus()
        End If
    End Sub

    Private Sub BtnCancelar_Click(sender As Object, e As EventArgs)
        _cancelacion?.Cancel()
    End Sub

    Private Sub BtnLimpiar_Click(sender As Object, e As EventArgs)
        _orquestador?.ReiniciarConversacion()
        _txtConversacion.Clear()
        _dgvResultados.DataSource = Nothing
        _btnExportar.Enabled = False
        LimpiarGrafico()
        _tabsResultados.SelectedTab = _tabDatos
        AppendMensaje("Sistema", "Conversación reiniciada.")
    End Sub

    Private Sub TxtPregunta_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter AndAlso Not e.Shift Then
            e.SuppressKeyPress = True
            _btnEnviar.PerformClick()
        End If
    End Sub

    Private Sub ExportarResultadoActual()
        Dim tieneFilas = _dgvResultados.Rows.Cast(Of DataGridViewRow)().Any(Function(r) Not r.IsNewRow)
        If Not tieneFilas Then
            MessageBox.Show("No hay un resultado tabular para exportar.", "Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Using dialogo As New SaveFileDialog With {
            .Title = "Exportar resultado a Excel",
            .Filter = "Libro de Excel (*.xlsx)|*.xlsx",
            .DefaultExt = "xlsx",
            .AddExtension = True,
            .FileName = $"ConsultaProveedores_{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
        }
            If dialogo.ShowDialog(Me) <> DialogResult.OK Then Return
            Try
                Dim exportador As New ChatExcelExportService()
                exportador.Exportar(_dgvResultados, dialogo.FileName, "Consulta proveedores")
                MessageBox.Show("El resultado se exportó correctamente.", "Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show($"No se pudo exportar el resultado:{Environment.NewLine}{ex.Message}", "Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub AjustarGrafico()
        _vistaGrafico.Location = New Point(0, 0)
        _vistaGrafico.Width = Math.Max(300, _panelGrafico.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 4)
        _vistaGrafico.Height = Math.Max(_vistaGrafico.AlturaRecomendada, _panelGrafico.ClientSize.Height - 4)
    End Sub

    Private Sub LimpiarGrafico()
        _vistaGrafico.Mostrar(Nothing)
        _btnGuardarGrafico.Enabled = False
    End Sub

    Private Sub BtnGuardarGrafico_Click(sender As Object, e As EventArgs)
        Using dialogo As New SaveFileDialog With {.Title = "Guardar gráfico", .Filter = "Imagen PNG (*.png)|*.png", .DefaultExt = "png", .AddExtension = True, .FileName = $"GraficoProveedores_{DateTime.Now:yyyyMMdd_HHmm}.png"}
            If dialogo.ShowDialog(Me) <> DialogResult.OK Then Return
            Try
                _vistaGrafico.GuardarPng(dialogo.FileName)
            Catch ex As Exception
                MessageBox.Show("No se pudo guardar el gráfico: " & ex.Message, "Guardar gráfico", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Async Sub frmChat_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        _cancelacion?.Cancel()
        _instancia = Nothing
        If _orquestador IsNot Nothing Then
            Try
                Await _orquestador.DisposeAsync()
            Catch
                Diagnostics.Debug.WriteLine("No se pudo completar el cierre del cliente MCP.")
            End Try
        End If
    End Sub
End Class

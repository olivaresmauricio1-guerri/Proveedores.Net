Imports System.Reflection
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
        _orquestador = New ChatOrchestratorService()
        _lblEstado.Text = "Comprobando Ollama..."

        Dim disponible = Await _orquestador.Ollama.EstaDisponibleAsync()
        If disponible Then
            _lblEstado.Text = $"Conectado · {_orquestador.Ollama.Model}"
            _lblEstado.ForeColor = Color.DarkGreen
            _txtPregunta.Focus()
        Else
            _lblEstado.Text = $"Ollama no disponible en {_orquestador.Ollama.BaseUrl}"
            _lblEstado.ForeColor = Color.DarkRed
        End If
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
        contenedor.Panel2.Controls.Add(_dgvResultados)

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
            AppendMensaje("Asistente", respuesta.Texto)
            MostrarResultadoTabular(respuesta)
            If respuesta.SolicitarExportacionExcel Then
                ExportarResultadoActual()
            End If
            _lblEstado.Text = If(respuesta.HerramientasEjecutadas.Count = 0,
                                 "Listo",
                                 $"Listo · {String.Join(", ", respuesta.HerramientasEjecutadas)}")
            _lblEstado.ForeColor = Color.DarkGreen
        Catch ex As OperationCanceledException
            AppendMensaje("Sistema", "Consulta cancelada.")
            _lblEstado.Text = "Consulta cancelada"
            _lblEstado.ForeColor = Color.DarkOrange
        Catch ex As Exception
            AppendMensaje("Error", ex.Message)
            _lblEstado.Text = "Error al procesar la consulta"
            _lblEstado.ForeColor = Color.DarkRed
        Finally
            _cancelacion.Dispose()
            _cancelacion = Nothing
            CambiarEstadoConsulta(False)
        End Try
    End Sub

    Private Sub MostrarResultadoTabular(respuesta As ChatOrchestratorResponse)
        If respuesta Is Nothing OrElse respuesta.Resultados.Count = 0 Then Return

        For i As Integer = respuesta.Resultados.Count - 1 To 0 Step -1
            Dim datos = respuesta.Resultados(i).Datos
            If datos Is Nothing Then Continue For

            Dim propiedad As PropertyInfo = Nothing
            For Each nombrePropiedad In New String() {"facturas", "movimientos", "proveedores", "meses",
                                                        "vehiculos", "servicios", "comparacion", "ranking"}
                propiedad = datos.GetType().GetProperty(
                    nombrePropiedad,
                    BindingFlags.Public Or BindingFlags.Instance Or BindingFlags.IgnoreCase)
                If propiedad IsNot Nothing Then Exit For
            Next
            If propiedad IsNot Nothing Then
                _dgvResultados.DataSource = propiedad.GetValue(datos)
                _btnExportar.Enabled = _dgvResultados.Rows.Cast(Of DataGridViewRow)().Any(Function(r) Not r.IsNewRow)
                Return
            End If

            If Not String.Equals(respuesta.Resultados(i).Nombre, "solicitar_exportacion_excel", StringComparison.OrdinalIgnoreCase) Then
                _dgvResultados.DataSource = New List(Of Object) From {datos}
                _btnExportar.Enabled = True
                Return
            End If
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

    Private Sub frmChat_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        _cancelacion?.Cancel()
        _cancelacion?.Dispose()
        _orquestador?.Dispose()
        _instancia = Nothing
    End Sub
End Class

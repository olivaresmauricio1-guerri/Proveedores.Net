Imports System.Net.Http
Imports System.Net
Imports System.Net.Sockets
Imports System.IO
Imports System.Text.Json
Imports System.Threading
Imports ModelContextProtocol.Client
Imports ModelContextProtocol.Protocol

Public Interface IChatMcpClient
    Inherits IAsyncDisposable
    Function ObtenerHerramientasAsync(token As CancellationToken) As Task(Of List(Of OllamaToolDefinition))
    Function EjecutarAsync(nombre As String, argumentos As JsonElement, token As CancellationToken) As Task(Of ChatToolResult)
End Interface

''' <summary>Cliente del MCP publicado. No contiene consultas SQL ni cálculos contables.</summary>
Public Class ChatMcpClientService
    Implements IChatMcpClient

#If DEBUG Then
    Public Const EntornoMcp As String = "Pruebas"
    Public Const NombreEntorno As String = "Pruebas"
    Public Const UrlPredeterminada As String = "https://SRVTS01:5128/mcp"
    Public Const VariableUrl As String = "PROVEEDORES_MCP_URL_PRUEBAS"
    Public Const VariableToken As String = "PROVEEDORES_MCP_TOKEN_PRUEBAS"
#Else
    Public Const EntornoMcp As String = "Production"
    Public Const NombreEntorno As String = "Producción"
    Public Const UrlPredeterminada As String = "https://SRVTS01:5129/mcp"
    Public Const VariableUrl As String = "PROVEEDORES_MCP_URL_PRODUCCION"
    Public Const VariableToken As String = "PROVEEDORES_MCP_TOKEN_PRODUCCION"
#End If
    Private ReadOnly _endpoint As Uri
    Private ReadOnly _obtenerToken As Func(Of String)
    Private ReadOnly _permitirDemo As Boolean
    Private ReadOnly _entornoEsperado As String
    Private ReadOnly _permitidas As New HashSet(Of String)(StringComparer.Ordinal)
    Private _cliente As McpClient
    Private _http As HttpClient
    Private _disposed As Boolean

    Public Sub New(Optional endpoint As Uri = Nothing, Optional obtenerToken As Func(Of String) = Nothing, Optional permitirDemo As Boolean = False)
        Dim url = Environment.GetEnvironmentVariable(VariableUrl, EnvironmentVariableTarget.User)
        If String.IsNullOrWhiteSpace(url) Then url = UrlPredeterminada
        _endpoint = If(endpoint, New Uri(url, UriKind.Absolute))
        If _endpoint.Scheme <> Uri.UriSchemeHttps AndAlso Not (_endpoint.Scheme = Uri.UriSchemeHttp AndAlso _endpoint.IsLoopback) Then
            Throw New ArgumentException("La conexión MCP requiere HTTPS; HTTP solo se admite en localhost para pruebas.")
        End If
        If Not String.IsNullOrEmpty(_endpoint.UserInfo) Then Throw New ArgumentException("La URL MCP no debe incluir credenciales.")
        _obtenerToken = If(obtenerToken, AddressOf LeerCredencial)
        _entornoEsperado = If(endpoint Is Nothing, EntornoMcp, Nothing)
        _permitirDemo = permitirDemo
    End Sub

    Private Shared Function LeerCredencial() As String
        Dim valor = Environment.GetEnvironmentVariable(VariableToken, EnvironmentVariableTarget.User)
#If DEBUG Then
        ' Compatibilidad con el token del piloto, exclusivamente en Debug.
        If String.IsNullOrWhiteSpace(valor) Then valor = Environment.GetEnvironmentVariable("PROVEEDORES_MCP_TOKEN", EnvironmentVariableTarget.User)
#End If
        Return valor
    End Function

    Public Async Function ObtenerHerramientasAsync(token As CancellationToken) As Task(Of List(Of OllamaToolDefinition)) Implements IChatMcpClient.ObtenerHerramientasAsync
        ObjectDisposedException.ThrowIf(_disposed, Me)
        Using plazo = CancellationTokenSource.CreateLinkedTokenSource(token)
            plazo.CancelAfter(TimeSpan.FromSeconds(20))
            Try
                Await ConectarAsync(plazo.Token).ConfigureAwait(False)
                Dim catalogo = Await _cliente.ListToolsAsync(cancellationToken:=plazo.Token).ConfigureAwait(False)
                Dim resultado As New List(Of OllamaToolDefinition)
                _permitidas.Clear()
                For Each herramienta In catalogo
                    Dim anotaciones = herramienta.ProtocolTool.Annotations
                    If anotaciones Is Nothing OrElse Not Object.Equals(anotaciones.ReadOnlyHint, True) OrElse Object.Equals(anotaciones.DestructiveHint, True) Then Continue For
                    If {"solicitar_exportacion_excel", "mostrar_grafico"}.Contains(herramienta.Name, StringComparer.Ordinal) Then Continue For
                    If Not _permitidas.Add(herramienta.Name) Then Throw New InvalidOperationException("El catálogo MCP contiene nombres duplicados.")
                    resultado.Add(New OllamaToolDefinition With {.Function = New OllamaToolFunctionDefinition With {
                        .Name = herramienta.Name, .Description = herramienta.Description, .Parameters = herramienta.JsonSchema.Clone()}})
                Next
                If resultado.Count = 0 Then Throw New InvalidOperationException("El servidor MCP no ofrece consultas de lectura disponibles.")
                Return resultado
            Catch ex As OperationCanceledException When Not token.IsCancellationRequested
                Throw New TimeoutException("Se agotó el tiempo para conectar al MCP. Comprobá la red o VPN y el servicio.")
            Catch ex As HttpRequestException
                Throw New InvalidOperationException("No se pudo conectar al MCP. Comprobá la red o VPN, el certificado y la credencial del usuario Windows.")
            Catch ex As ModelContextProtocol.McpException
                Throw New InvalidOperationException("El MCP rechazó la conexión o el descubrimiento de herramientas. Revisá la credencial y el servicio.")
            End Try
        End Using
    End Function

    Private Async Function ConectarAsync(token As CancellationToken) As Task
        If _cliente IsNot Nothing Then Return
        Dim credencial = _obtenerToken()
        If String.IsNullOrWhiteSpace(credencial) OrElse credencial.Any(Function(c) Char.IsControl(c)) Then
            Throw New InvalidOperationException("Falta una credencial MCP válida en " & VariableToken & " del usuario Windows (entorno " & EntornoMcp & ").")
        End If
        ' El piloto escucha HTTPS en IPv4. SRVTS01 también publica una IPv6 link-local
        ' que no atiende este puerto: preferir IPv4 sin cambiar DNS ni omitir TLS.
        Dim handler As New SocketsHttpHandler With {
            .AllowAutoRedirect = False, .UseProxy = False, .ConnectTimeout = TimeSpan.FromSeconds(15),
            .ConnectCallback = Function(contexto, ct) New ValueTask(Of Stream)(ConectarSocketAsync(contexto.DnsEndPoint, ct))}
        _http = New HttpClient(handler) With {.Timeout = Timeout.InfiniteTimeSpan}
        Dim transporte As New HttpClientTransport(New HttpClientTransportOptions With {
            .Endpoint = _endpoint, .Name = "Proveedores.Net", .TransportMode = HttpTransportMode.StreamableHttp,
            .AdditionalHeaders = New Dictionary(Of String, String) From {{"Authorization", "Bearer " & credencial.Trim()}}}, _http, ownsHttpClient:=False)
        Try
            If _entornoEsperado IsNot Nothing Then
                Using peticion As New HttpRequestMessage(HttpMethod.Get, New Uri(_endpoint, "./estado"))
                    peticion.Headers.Authorization = New System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", credencial.Trim())
                    Using estado = Await _http.SendAsync(peticion, token).ConfigureAwait(False)
                        If Not (estado.StatusCode = HttpStatusCode.NotFound AndAlso _entornoEsperado = "Pruebas") Then
                            estado.EnsureSuccessStatusCode()
                            Using documento = JsonDocument.Parse(Await estado.Content.ReadAsStringAsync(token).ConfigureAwait(False))
                                If Not EstadoCompatible(documento.RootElement, _entornoEsperado) Then
                                    Throw New InvalidOperationException("La dirección MCP no corresponde al entorno " & NombreEntorno & " en modo Sql. No se habilitará el chat con ese servidor.")
                                End If
                            End Using
                        End If
                    End Using
                End Using
            End If
            _cliente = Await McpClient.CreateAsync(transporte, New McpClientOptions With {
                .ClientInfo = New Implementation With {.Name = "Proveedores.Net", .Version = "2.1.0"}}, cancellationToken:=token).ConfigureAwait(False)
        Catch
            _http.Dispose()
            _http = Nothing
            Throw
        End Try
    End Function

    Private Shared Async Function ConectarSocketAsync(destino As DnsEndPoint, token As CancellationToken) As Task(Of Stream)
        Dim direcciones = Await Dns.GetHostAddressesAsync(destino.Host, token).ConfigureAwait(False)
        For Each direccion In direcciones.OrderBy(Function(ip) If(ip.AddressFamily = AddressFamily.InterNetwork, 0, 1))
            Dim socket As New Socket(direccion.AddressFamily, SocketType.Stream, ProtocolType.Tcp) With {.NoDelay = True}
            Using intento = CancellationTokenSource.CreateLinkedTokenSource(token)
                intento.CancelAfter(TimeSpan.FromSeconds(5))
                Try
                    Await socket.ConnectAsync(direccion, destino.Port, intento.Token).ConfigureAwait(False)
                    Return New NetworkStream(socket, ownsSocket:=True)
                Catch ex As OperationCanceledException When Not token.IsCancellationRequested
                    socket.Dispose()
                Catch ex As SocketException
                    socket.Dispose()
                Catch
                    socket.Dispose()
                    Throw
                End Try
            End Using
        Next
        token.ThrowIfCancellationRequested()
        Throw New HttpRequestException("No se pudo establecer la conexión con el servidor MCP.")
    End Function

    Public Async Function EjecutarAsync(nombre As String, argumentos As JsonElement, token As CancellationToken) As Task(Of ChatToolResult) Implements IChatMcpClient.EjecutarAsync
        ObjectDisposedException.ThrowIf(_disposed, Me)
        If _cliente Is Nothing OrElse Not _permitidas.Contains(nombre) Then Return ErrorConsulta(nombre, "La consulta no está disponible en el catálogo MCP actual.")
        If argumentos.ValueKind <> JsonValueKind.Object AndAlso argumentos.ValueKind <> JsonValueKind.Undefined Then Return ErrorConsulta(nombre, "Los argumentos de la consulta deben ser un objeto JSON.")
        Dim valores As New Dictionary(Of String, Object)(StringComparer.Ordinal)
        If argumentos.ValueKind = JsonValueKind.Object Then
            For Each propiedad In argumentos.EnumerateObject()
                If valores.ContainsKey(propiedad.Name) Then Return ErrorConsulta(nombre, "La consulta contiene parámetros repetidos.")
                valores.Add(propiedad.Name, propiedad.Value.Clone())
            Next
        End If
        Using plazo = CancellationTokenSource.CreateLinkedTokenSource(token)
            plazo.CancelAfter(TimeSpan.FromSeconds(60))
            Try
                Dim respuesta = Await _cliente.CallToolAsync(nombre, valores, cancellationToken:=plazo.Token).ConfigureAwait(False)
                If respuesta.IsError = True Then Return ErrorConsulta(nombre, "El servidor MCP no pudo ejecutar la consulta.")
                Dim json = JsonSerializer.SerializeToElement(respuesta.StructuredContent)
                If json.ValueKind <> JsonValueKind.Object Then
                    Dim texto = respuesta.Content.OfType(Of TextContentBlock)().FirstOrDefault()
                    If texto Is Nothing Then Return ErrorConsulta(nombre, "El MCP devolvió una respuesta sin datos JSON.")
                    Using documento = JsonDocument.Parse(texto.Text)
                        json = documento.RootElement.Clone()
                    End Using
                End If
                Return InterpretarRespuesta(nombre, json, _permitirDemo, _entornoEsperado)
            Catch ex As OperationCanceledException When Not token.IsCancellationRequested
                Return ErrorConsulta(nombre, "La consulta MCP agotó el tiempo permitido. No se obtuvieron resultados.")
            Catch ex As OperationCanceledException
                Throw
            Catch ex As Exception
                Return ErrorConsulta(nombre, "No se pudo completar la consulta MCP. Comprobá la conexión y el servicio.")
            End Try
        End Using
    End Function

    Public Shared Function EstadoCompatible(estado As JsonElement, entornoEsperado As String) As Boolean
        Dim entorno, modo As JsonElement
        Return BuscarPropiedad(estado, "entorno", entorno) AndAlso entorno.ValueKind = JsonValueKind.String AndAlso
            String.Equals(entorno.GetString(), entornoEsperado, StringComparison.Ordinal) AndAlso
            BuscarPropiedad(estado, "modo", modo) AndAlso modo.ValueKind = JsonValueKind.String AndAlso modo.GetString() = "Sql"
    End Function

    Public Shared Function InterpretarRespuesta(nombre As String, respuesta As JsonElement, Optional permitirDemo As Boolean = False, Optional entornoEsperado As String = Nothing) As ChatToolResult
        Dim exito As JsonElement
        If Not BuscarPropiedad(respuesta, "Exito", exito) OrElse exito.ValueKind <> JsonValueKind.True Then
            Dim mensaje As JsonElement
            Dim texto = "El MCP no pudo completar la consulta."
            If BuscarPropiedad(respuesta, "ErrorMensaje", mensaje) AndAlso mensaje.ValueKind = JsonValueKind.String AndAlso Not String.IsNullOrWhiteSpace(mensaje.GetString()) Then texto = mensaje.GetString()
            Return ErrorConsulta(nombre, texto)
        End If
        Dim modo As JsonElement
        If Not BuscarPropiedad(respuesta, "Modo", modo) OrElse modo.ValueKind <> JsonValueKind.String OrElse
            (modo.GetString() <> "Sql" AndAlso Not (permitirDemo AndAlso modo.GetString() = "Demo")) Then
            Return ErrorConsulta(nombre, "El MCP no devolvió datos SQL reales. No se mostrará una demostración como información del sistema.")
        End If
        If Not String.IsNullOrWhiteSpace(entornoEsperado) Then
            Dim entorno As JsonElement
            Dim tieneEntorno = BuscarPropiedad(respuesta, "Entorno", entorno) AndAlso entorno.ValueKind = JsonValueKind.String
            ' El piloto 2.0.1 no informaba entorno; solo Debug admite ese contrato.
            If (Not tieneEntorno AndAlso entornoEsperado <> "Pruebas") OrElse
                (tieneEntorno AndAlso Not String.Equals(entorno.GetString(), entornoEsperado, StringComparison.Ordinal)) Then
                Return ErrorConsulta(nombre, "El entorno del MCP no coincide con " & entornoEsperado & ". Revisá la dirección configurada; no se mostrarán esos datos.")
            End If
        End If
        Dim datos As JsonElement
        If Not BuscarPropiedad(respuesta, "Datos", datos) OrElse datos.ValueKind <> JsonValueKind.Object Then Return ErrorConsulta(nombre, "El MCP devolvió un contrato de datos inválido.")
        Return New ChatToolResult With {.Nombre = nombre, .Exito = True, .Datos = datos.Clone()}
    End Function

    Public Shared Function ErrorConsulta(nombre As String, mensaje As String) As ChatToolResult
        Return New ChatToolResult With {.Nombre = nombre, .Exito = False, .Datos = JsonSerializer.SerializeToElement(New With {.exito = False, .error = mensaje})}
    End Function

    Private Shared Function BuscarPropiedad(json As JsonElement, nombre As String, ByRef valor As JsonElement) As Boolean
        If json.ValueKind <> JsonValueKind.Object Then Return False
        For Each p In json.EnumerateObject()
            If String.Equals(p.Name, nombre, StringComparison.OrdinalIgnoreCase) Then
                valor = p.Value
                Return True
            End If
        Next
        Return False
    End Function

    Public Function DisposeAsync() As ValueTask Implements IAsyncDisposable.DisposeAsync
        Return New ValueTask(CerrarAsync())
    End Function
    Private Async Function CerrarAsync() As Task
        If _disposed Then Return
        _disposed = True
        Try
            If _cliente IsNot Nothing Then Await _cliente.DisposeAsync().ConfigureAwait(False)
        Finally
            _http?.Dispose()
        End Try
    End Function
End Class

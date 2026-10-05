Imports System.Net.Http
Imports System.Text
Imports System.Text.Json
Imports System.Text.Json.Serialization
Imports System.Threading

Public Class OllamaClientService
    Implements IDisposable

    Public Const ModeloPredeterminado As String = "orcarouter/Qwen3.8-27B-Uncensored:q4_K_M"
    Public Const UrlPredeterminada As String = "http://localhost:11434"

    Private ReadOnly _httpClient As HttpClient
    Private ReadOnly _disposeHttpClient As Boolean
    Private ReadOnly _jsonOptions As JsonSerializerOptions

    Public ReadOnly Property Model As String
    Public ReadOnly Property BaseUrl As String

    Public Sub New(Optional baseUrl As String = UrlPredeterminada,
                   Optional model As String = ModeloPredeterminado,
                   Optional httpClient As HttpClient = Nothing)
        If String.IsNullOrWhiteSpace(baseUrl) Then Throw New ArgumentException("La URL de Ollama es obligatoria.", NameOf(baseUrl))
        If String.IsNullOrWhiteSpace(model) Then Throw New ArgumentException("El modelo de Ollama es obligatorio.", NameOf(model))

        _BaseUrl = baseUrl.Trim().TrimEnd("/"c)
        _Model = model.Trim()

        If httpClient Is Nothing Then
            _httpClient = New HttpClient() With {.Timeout = TimeSpan.FromMinutes(5)}
            _disposeHttpClient = True
        Else
            _httpClient = httpClient
        End If

        _jsonOptions = New JsonSerializerOptions With {
            .PropertyNameCaseInsensitive = True,
            .DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        }
    End Sub

    Public Async Function EstaDisponibleAsync(Optional cancellationToken As CancellationToken = Nothing) As Task(Of Boolean)
        Try
            Using response = Await _httpClient.GetAsync(BaseUrl & "/api/version", cancellationToken).ConfigureAwait(False)
                Return response.IsSuccessStatusCode
            End Using
        Catch ex As OperationCanceledException When Not cancellationToken.IsCancellationRequested
            Return False
        Catch ex As HttpRequestException
            Return False
        End Try
    End Function

    Public Async Function EnviarAsync(messages As List(Of OllamaChatMessage),
                                      tools As List(Of OllamaToolDefinition),
                                      Optional cancellationToken As CancellationToken = Nothing) As Task(Of OllamaChatResponse)
        If messages Is Nothing OrElse messages.Count = 0 Then
            Throw New ArgumentException("Debe enviarse al menos un mensaje.", NameOf(messages))
        End If

        Dim request As New OllamaChatRequest With {
            .Model = Model,
            .Messages = messages,
            .Tools = tools,
            .Stream = False,
            .Think = False
        }
        Dim json = JsonSerializer.Serialize(request, _jsonOptions)

        Using content As New StringContent(json, Encoding.UTF8, "application/json")
            Using response = Await _httpClient.PostAsync(BaseUrl & "/api/chat", content, cancellationToken).ConfigureAwait(False)
                Dim responseJson = Await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(False)
                If Not response.IsSuccessStatusCode Then
                    Throw New HttpRequestException($"Ollama respondió {CInt(response.StatusCode)}: {responseJson}")
                End If

                Dim resultado = JsonSerializer.Deserialize(Of OllamaChatResponse)(responseJson, _jsonOptions)
                If resultado Is Nothing OrElse resultado.Message Is Nothing Then
                    Throw New InvalidOperationException("Ollama devolvió una respuesta vacía o inválida.")
                End If
                Return resultado
            End Using
        End Using
    End Function

    Public Sub Dispose() Implements IDisposable.Dispose
        If _disposeHttpClient Then _httpClient.Dispose()
    End Sub
End Class

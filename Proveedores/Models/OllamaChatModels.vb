Imports System.Text.Json
Imports System.Text.Json.Serialization

Public Class OllamaChatMessage
    <JsonPropertyName("role")>
    Public Property Role As String = String.Empty

    <JsonPropertyName("content")>
    Public Property Content As String = String.Empty

    <JsonPropertyName("thinking")>
    <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
    Public Property Thinking As String

    <JsonPropertyName("tool_calls")>
    <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
    Public Property ToolCalls As List(Of OllamaToolCall)

    <JsonPropertyName("tool_name")>
    <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
    Public Property ToolName As String
End Class

Public Class OllamaToolCall
    <JsonPropertyName("type")>
    <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
    Public Property Type As String

    <JsonPropertyName("function")>
    Public Property [Function] As OllamaFunctionCall
End Class

Public Class OllamaFunctionCall
    <JsonPropertyName("index")>
    <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingDefault)>
    Public Property Index As Integer

    <JsonPropertyName("name")>
    Public Property Name As String = String.Empty

    <JsonPropertyName("arguments")>
    Public Property Arguments As JsonElement
End Class

Public Class OllamaToolDefinition
    <JsonPropertyName("type")>
    Public Property Type As String = "function"

    <JsonPropertyName("function")>
    Public Property [Function] As OllamaToolFunctionDefinition
End Class

Public Class OllamaToolFunctionDefinition
    <JsonPropertyName("name")>
    Public Property Name As String = String.Empty

    <JsonPropertyName("description")>
    Public Property Description As String = String.Empty

    <JsonPropertyName("parameters")>
    Public Property Parameters As JsonElement
End Class

Public Class OllamaChatRequest
    <JsonPropertyName("model")>
    Public Property Model As String = String.Empty

    <JsonPropertyName("messages")>
    Public Property Messages As List(Of OllamaChatMessage)

    <JsonPropertyName("tools")>
    <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
    Public Property Tools As List(Of OllamaToolDefinition)

    <JsonPropertyName("stream")>
    Public Property Stream As Boolean

    <JsonPropertyName("think")>
    Public Property Think As Boolean

    <JsonPropertyName("keep_alive")>
    Public Property KeepAlive As String = "10m"
End Class

Public Class OllamaChatResponse
    <JsonPropertyName("model")>
    Public Property Model As String = String.Empty

    <JsonPropertyName("message")>
    Public Property Message As OllamaChatMessage

    <JsonPropertyName("done")>
    Public Property Done As Boolean

    <JsonPropertyName("done_reason")>
    Public Property DoneReason As String = String.Empty
End Class

Public Class ChatToolResult
    Public Property Nombre As String = String.Empty
    Public Property Datos As JsonElement
    Public Property Exito As Boolean
End Class

Public Class ChatOrchestratorResponse
    Public Property Texto As String = String.Empty
    Public Property HerramientasEjecutadas As New List(Of String)
    Public Property Resultados As New List(Of ChatToolResult)
    Public Property SolicitarExportacionExcel As Boolean
    Public Property Grafico As ChatChartData
End Class

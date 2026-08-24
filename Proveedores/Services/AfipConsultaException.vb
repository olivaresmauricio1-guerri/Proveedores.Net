Public Enum TipoErrorAfip
    CredencialesNoEncontradas
    ComunicacionFallida
    RespuestaInesperada
    Desconocido
End Enum

Public Class AfipConsultaException
    Inherits Exception

    Public ReadOnly Property Tipo As TipoErrorAfip
    Public ReadOnly Property DetalleTecnico As String

    Public Sub New(tipo As TipoErrorAfip, mensajeUsuario As String,
                   detalleTecnico As String, Optional innerEx As Exception = Nothing)
        MyBase.New(mensajeUsuario, innerEx)
        Me.Tipo = tipo
        Me.DetalleTecnico = detalleTecnico
    End Sub
End Class
Imports tempuri.org
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class AfipProviderService

    Private Shared ReadOnly rnd As New Random()

    Public Async Function ConsultarProveedor(cuitTexto As String) As Task(Of String)

        Try
            ' Limpiar CUIT
            Dim cuitLimpio As String =
                cuitTexto.Replace("-", "").Replace(" ", "").Trim()

            Dim cuit As Long

            If Not Long.TryParse(cuitLimpio, cuit) Then
                Return "CUIT_INVALIDO"
            End If

            ' Obtener credenciales AFIP desde Stock
            Dim sql As String =
                "SELECT TOP 1 token, sign, CuitDelegada " &
                "FROM loginafip " &
                "WHERE CAST(servicio AS VARCHAR(MAX)) = 'wsapoc'"

            Dim dt = DSM.ExecuteQuery(DSM.Stock, sql)

            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                Throw New Exception("No se encontraron las credenciales de AFIP.")
            End If

            Dim credencial As New Credencial With {
                .Token = dt.Rows(0)("token").ToString(),
                .Sign = dt.Rows(0)("sign").ToString(),
                .CUITDelegado = dt.Rows(0)("CuitDelegada").ToString()
            }

            ' Si tenés columna de fecha de generación del TA, descomentar:
            ' Dim fechaGeneracion As DateTime = CDate(dt.Rows(0)("FechaGeneracion"))
            ' If DateTime.Now.Subtract(fechaGeneracion).TotalHours >= 12 Then
            '     Throw New Exception("El token de AFIP está vencido. Debe regenerarse.")
            ' End If

            ' =========================================================
            ' CONSULTA AFIP CON REINTENTOS (backoff exponencial + jitter)
            ' Solo reintenta ante errores transitorios de comunicación
            ' =========================================================

            Dim respuesta = Nothing
            Dim ultimoError As Exception = Nothing
            Const maxIntentos As Integer = 5

            For intento As Integer = 1 To maxIntentos

                Dim cliente As ServiceClient = Nothing
                Dim fallo As Boolean = False
                Dim reintentar As Boolean = True

                Try
                    cliente = New ServiceClient(
                        ServiceClient.EndpointConfiguration.ServiceSoap)

                    respuesta = Await cliente.GetPublicacionAPOCAsync(
                        credencial,
                        cuit
                    )

                    cliente.Close()
                    cliente = Nothing

                    Exit For

                Catch ex As Exception

                    ultimoError = ex
                    fallo = True

                    If cliente IsNot Nothing Then
                        Try
                            cliente.Abort()
                        Catch
                            ' Ignorar error al abortar
                        End Try
                        cliente = Nothing
                    End If

                    ' Si no es un error transitorio, no tiene sentido seguir reintentando
                    reintentar = TypeOf ex Is System.ServiceModel.ProtocolException OrElse
                                 TypeOf ex Is System.ServiceModel.CommunicationException OrElse
                                 TypeOf ex Is TimeoutException

                End Try

                If fallo AndAlso Not reintentar Then
                    Exit For
                End If

                If fallo AndAlso intento < maxIntentos Then
                    Dim esperaMs As Integer = (1000 * CInt(2 ^ (intento - 1))) + rnd.Next(0, 500)
                    Await Task.Delay(esperaMs)
                End If

            Next

            ' =========================================================
            ' TODOS LOS INTENTOS FALLARON
            ' =========================================================

            If respuesta Is Nothing Then
                Throw New Exception(
                    $"No se pudo obtener una respuesta válida de AFIP después de {maxIntentos} intentos.",
                    ultimoError)
            End If

            ' =========================================================
            ' PROCESAR RESPUESTA
            ' =========================================================

            If respuesta.Body Is Nothing Then
                Return "SIN_RESPUESTA"
            End If

            Dim resultado = respuesta.Body.GetPublicacionAPOCResult

            If resultado Is Nothing Then
                Return "SIN_RESPUESTA"
            End If

            If resultado.resultados Is Nothing OrElse
               resultado.resultados.Length = 0 Then
                Return "NO_APOCRIFO"
            End If

            Return "APOCRIFO"

        Catch ex As Exception

            Dim mensaje As String =
                "Error consultando AFIP: " & ex.Message &
                If(ex.InnerException IsNot Nothing,
                   Environment.NewLine & "Inner: " & ex.InnerException.Message,
                   "")

            Throw New Exception(mensaje, ex)

        End Try

    End Function

End Class
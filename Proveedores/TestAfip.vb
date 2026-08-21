Imports tempuri.org

Public Class TestAfip

    Public Shared Sub Probar()

        Dim client As New ServiceClient(
            ServiceClient.EndpointConfiguration.ServiceSoap)

        Try
            Dim respuesta = client.DummyAsync().GetAwaiter().GetResult()

            Dim datos = respuesta.Body.DummyResult

            MessageBox.Show(
                "AppServer: " & datos.appserver & vbCrLf &
                "DBServer: " & datos.dbserver & vbCrLf &
                "AuthServer: " & datos.authserver,
                "Respuesta AFIP")

            client.Close()

        Catch ex As Exception

            MessageBox.Show(
                ex.ToString(),
                "Error AFIP",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)

            client.Abort()

        End Try

    End Sub

End Class
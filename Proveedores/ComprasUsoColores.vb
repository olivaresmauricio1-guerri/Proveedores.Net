Friend Module ComprasUsoColores
    Public Function FondoEstado(estado As String) As Color
        Select Case If(estado, "").Trim().ToUpperInvariant()
            Case "BORRADOR"
                Return Color.FromArgb(235, 235, 235)
            Case "PENDIENTE"
                Return Color.FromArgb(255, 243, 184)
            Case "AUTORIZADA"
                Return Color.FromArgb(210, 239, 218)
            Case "DEVUELTA"
                Return Color.FromArgb(255, 221, 179)
            Case "RECHAZADA"
                Return Color.FromArgb(250, 210, 210)
            Case Else
                Return Color.White
        End Select
    End Function

    Public Sub AplicarCelda(estilo As DataGridViewCellStyle, estado As String)
        Dim fondo = FondoEstado(estado)
        estilo.BackColor = fondo
        estilo.ForeColor = Color.FromArgb(35, 35, 35)
        ' Conserva el codigo de color cuando la fila esta seleccionada.
        estilo.SelectionBackColor = fondo
        estilo.SelectionForeColor = estilo.ForeColor
    End Sub
End Module

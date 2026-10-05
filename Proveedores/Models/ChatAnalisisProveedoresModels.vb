Public Class ChatDeudaProveedor
    Public Property NroCuenta As Integer
    Public Property Nombre As String = String.Empty
    Public Property Cuit As String = String.Empty
    Public Property Saldo As Decimal
    Public Property PorcentajeDeuda As Decimal
End Class

Public Class ChatAnalisisDeudaTotal
    Public Property DeudaBruta As Decimal
    Public Property CreditosAFavor As Decimal
    Public Property PosicionNeta As Decimal
    Public Property CantidadConDeuda As Integer
    Public Property CantidadConCredito As Integer
    Public Property Proveedores As New List(Of ChatDeudaProveedor)
End Class

Public Class ChatComprasMes
    Public Property Mes As DateTime
    Public Property TotalCompras As Decimal
End Class

Public Class ChatAnalisisComprasMensuales
    Public Property Desde As DateTime
    Public Property Hasta As DateTime
    Public Property TotalPeriodo As Decimal
    Public Property PromedioMensual As Decimal
    Public Property Meses As New List(Of ChatComprasMes)
End Class

Public Class ChatPagoProveedor
    Public Property NroCuenta As Integer
    Public Property Nombre As String = String.Empty
    Public Property Cuit As String = String.Empty
    Public Property TotalPagado As Decimal
    Public Property PorcentajeDelTotal As Decimal
End Class

Public Class ChatEvolucionDeudaMes
    Public Property Mes As DateTime
    Public Property Compras As Decimal
    Public Property Pagos As Decimal
    Public Property VariacionNeta As Decimal
    Public Property DeudaBrutaAlCierre As Decimal
    Public Property CreditosAFavorAlCierre As Decimal
    Public Property PosicionNetaAlCierre As Decimal
End Class

Public Class ChatDiferenciaSaldoProveedor
    Public Property NroCuenta As Integer
    Public Property Nombre As String = String.Empty
    Public Property SaldoCalculado As Decimal
    Public Property SaldoActualMaestro As Decimal
    Public Property Diferencia As Decimal
End Class

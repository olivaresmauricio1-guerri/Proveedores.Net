Public Class ChatProveedorInfo
    Public Property NroCuenta As Integer
    Public Property Nombre As String = String.Empty
    Public Property Cuit As String = String.Empty
    Public Property FechaAlta As DateTime
    Public Property SaldoActualMaestro As Decimal
    Public Property SaldoDto As Decimal
End Class

Public Class ChatMovimientoCuenta
    Public Property Fecha As DateTime
    Public Property PuntoDeVenta As String = String.Empty
    Public Property NroFactura As String = String.Empty
    Public Property NroComprobante As String = String.Empty
    Public Property NombreComprobante As String = String.Empty
    Public Property Condicion As String = String.Empty
    Public Property IdImputacion As Integer
    Public Property CtaMonto As String = String.Empty
    Public Property Comentario As String = String.Empty
    Public Property Cobrado As Boolean
    Public Property Monto As Decimal
    Public Property ACuenta As Decimal
    Public Property Debe As Decimal
    Public Property Haber As Decimal
    Public Property Saldo As Decimal
    Public Property Origen As String = String.Empty
End Class

Public Class ChatResumenCuenta
    Public Property Proveedor As ChatProveedorInfo
    Public Property Desde As DateTime
    Public Property Hasta As DateTime
    Public Property SaldoInicial As Decimal
    Public Property SaldoCalculado As Decimal
    Public Property DiferenciaConSaldoActual As Decimal
    Public Property Movimientos As New List(Of ChatMovimientoCuenta)
End Class

Public Class ChatFacturaPendiente
    Public Property NroCuenta As Integer
    Public Property Proveedor As String = String.Empty
    Public Property Fecha As DateTime
    Public Property FechaVencimiento As DateTime?
    Public Property PuntoDeVenta As String = String.Empty
    Public Property NroFactura As String = String.Empty
    Public Property NroComprobante As String = String.Empty
    Public Property NombreComprobante As String = String.Empty
    Public Property Monto As Decimal
    Public Property ACuenta As Decimal
    Public Property SaldoPendiente As Decimal
    Public Property Origen As String = String.Empty
End Class

Public Class ChatSaldoProveedor
    Public Property NroCuenta As Integer
    Public Property Nombre As String = String.Empty
    Public Property Cuit As String = String.Empty
    Public Property SaldoCalculado As Decimal
    Public Property SaldoActualMaestro As Decimal
    Public Property Diferencia As Decimal
End Class

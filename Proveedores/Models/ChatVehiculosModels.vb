Public Class ChatFacturaPendientePeriodo
    Public Property NroCuenta As Integer
    Public Property Proveedor As String = String.Empty
    Public Property Fecha As DateTime
    Public Property Sucursal As String = String.Empty
    Public Property PuntoDeVenta As String = String.Empty
    Public Property NroFactura As String = String.Empty
    Public Property NombreComprobante As String = String.Empty
    Public Property Monto As Decimal
    Public Property ACuenta As Decimal
    Public Property SaldoPendiente As Decimal
    Public Property Origen As String = String.Empty
End Class

Public Class ChatVehiculoInfo
    Public Property IdVehiculo As Integer
    Public Property Patente As String = String.Empty
    Public Property Descripcion As String = String.Empty
    Public Property Marca As String = String.Empty
    Public Property Modelo As String = String.Empty
    Public Property Anio As Integer
    Public Property KmActual As Integer
    Public Property IdSucursal As Integer
    Public Property Sucursal As String = String.Empty
    Public Property Activo As Boolean
End Class

Public Class ChatServicioRealizado
    Public Property IdServicio As Integer
    Public Property FechaServicio As DateTime
    Public Property IdVehiculo As Integer
    Public Property Vehiculo As String = String.Empty
    Public Property KmServicio As Integer
    Public Property Sucursal As String = String.Empty
    Public Property TiposServicio As String = String.Empty
    Public Property Proveedor As String = String.Empty
    Public Property Comprobante As String = String.Empty
    Public Property TotalServicio As Decimal
    Public Property Observaciones As String = String.Empty
End Class

Public Class ChatComparacionServicioVehiculo
    Public Property IdVehiculo As Integer
    Public Property Vehiculo As String = String.Empty
    Public Property TipoServicio As String = String.Empty
    Public Property CantidadServicios As Integer
    Public Property CantidadRealizada As Decimal
    Public Property UltimoServicio As DateTime?
    Public Property ImporteItems As Decimal
End Class

Public Class ChatRankingServicio
    Public Property Posicion As Integer
    Public Property Agrupacion As String = String.Empty
    Public Property Grupo As String = String.Empty
    Public Property TipoServicio As String = String.Empty
    Public Property CantidadServicios As Integer
    Public Property CantidadRealizada As Decimal
    Public Property VehiculosAtendidos As Integer
    Public Property ImporteItems As Decimal
End Class

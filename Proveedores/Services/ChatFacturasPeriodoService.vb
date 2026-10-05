Imports System.Data
Imports DSM = DataSourceManager.Lib.DataSourceManager

Partial Public Class ChatCuentaCorrienteService
    Public Function ListarFacturasPendientesPeriodo(desde As DateTime, hasta As DateTime,
                                                    Optional nroCuenta As Integer = 0,
                                                    Optional limite As Integer = 500) As List(Of ChatFacturaPendientePeriodo)
        If desde.Date > hasta.Date Then Throw New ArgumentException("La fecha desde no puede ser posterior a la fecha hasta.")
        limite = LimitarCantidad(limite, 1, 1000)
        Dim filtroProveedor = If(nroCuenta > 0, " AND NroCuenta = @NroCuenta", String.Empty)
        Dim sql As String =
            "SELECT TOP (@Limite) Q.NroCuenta, M.Nombre AS Proveedor, Q.Fecha, Q.Sucursal, " &
            "Q.PuntoDeVenta, Q.NroFactura, Q.NombreComprobante, Q.Monto, Q.ACuenta, " &
            "Q.Monto - Q.ACuenta AS SaldoPendiente, Q.Origen FROM (" &
            "SELECT NroCuenta, Fecha, Sucursal, PuntoDeVenta, NroFactura, NombreComprobante, " &
            "Monto, ISNULL(ACuenta,0) AS ACuenta, 'Actual' AS Origen FROM DetaCtaCte " &
            "WHERE Fecha >= @Desde AND Fecha < @HastaExclusivo AND IdImputacion IN (1,2,10) " &
            "AND ISNULL(Cobrado,0)=0 AND Monto-ISNULL(ACuenta,0)>0" & filtroProveedor &
            " UNION ALL SELECT NroCuenta, Fecha, Sucursal, PuntoDeVenta, NroFactura, NombreComprobante, " &
            "Monto, ISNULL(ACuenta,0) AS ACuenta, 'Anual' AS Origen FROM DetaCtaCteAnual " &
            "WHERE Fecha >= @Desde AND Fecha < @HastaExclusivo AND IdImputacion IN (1,2,10) " &
            "AND ISNULL(Cobrado,0)=0 AND Monto-ISNULL(ACuenta,0)>0" & filtroProveedor &
            ") Q INNER JOIN MaeCtaCte M ON M.NroCuenta=Q.NroCuenta " &
            "ORDER BY Q.Fecha DESC, M.Nombre, Q.NroFactura"
        Dim parametros = CmdParams("@Limite", limite, "@Desde", desde.Date,
                                   "@HastaExclusivo", hasta.Date.AddDays(1), "@NroCuenta", nroCuenta)
        Dim tabla = DSM.ExecuteQuery(DSM.Proveedores, sql, parametros)
        Dim resultado As New List(Of ChatFacturaPendientePeriodo)
        If tabla Is Nothing Then Return resultado
        For Each fila As DataRow In tabla.Rows
            resultado.Add(New ChatFacturaPendientePeriodo With {
                .NroCuenta = Convert.ToInt32(fila("NroCuenta")), .Proveedor = Texto(fila("Proveedor")),
                .Fecha = Convert.ToDateTime(fila("Fecha")), .Sucursal = Texto(fila("Sucursal")),
                .PuntoDeVenta = Texto(fila("PuntoDeVenta")), .NroFactura = Texto(fila("NroFactura")),
                .NombreComprobante = Texto(fila("NombreComprobante")), .Monto = DecimalSeguro(fila("Monto")),
                .ACuenta = DecimalSeguro(fila("ACuenta")), .SaldoPendiente = DecimalSeguro(fila("SaldoPendiente")),
                .Origen = Texto(fila("Origen"))})
        Next
        Return resultado
    End Function
End Class

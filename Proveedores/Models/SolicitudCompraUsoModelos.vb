Imports System.ComponentModel

Public Class SolicitudCompraUsoDocumento
    Public Property IdSolicitud As Integer
    Public Property FechaSolicitud As Date = Date.Today
    Public Property IdSector As Integer
    Public Property UsuarioSolicitante As String = ""
    Public Property Motivo As String = ""
    Public Property Prioridad As String = "N"
    Public Property FechaNecesidad As Date?
    Public Property Estado As String = "BORRADOR"
    Public Property Observaciones As String = ""
    Public Property Resolucion As String = ""
    Public Property IdCotizacionAutorizada As Integer?
    Public Property VersionFila As Byte()
    Public Property Items As New BindingList(Of SolicitudCompraUsoItem)
    Public Property Cotizaciones As New BindingList(Of SolicitudCompraUsoCotizacion)
    Public ReadOnly Property Editable As Boolean
        Get
            Return Estado = "BORRADOR" OrElse Estado = "DEVUELTA"
        End Get
    End Property
End Class

Public Class SolicitudCompraUsoItem
    Public Property IdDetalleSolicitud As Integer
    Public Property TipoItem As String = "A"
    Public Property Descripcion As String = ""
    Public Property Cantidad As Decimal = 1D
    Public Property Unidad As String = "Unidad"
    Public Property FechaNecesidad As Date?
    Public Property Observaciones As String = ""
End Class

Public Class SolicitudCompraUsoCotizacion
    Public Property IdCotizacion As Integer
    Public Property NroCuentaProveedor As Integer?
    Public Property ProveedorNombre As String = ""
    Public Property NumeroCotizacion As String = ""
    Public Property FechaCotizacion As Date = Date.Today
    Public Property FechaVencimiento As Date?
    Public Property Moneda As String = "ARS"
    Public Property ImporteNeto As Decimal?
    Public Property ImporteIVA As Decimal?
    Public Property OtrosImportes As Decimal?
    Public Property ImporteTotal As Decimal?
    Public Property CondicionPago As String = ""
    Public Property PlazoEntrega As String = ""
    Public Property Observaciones As String = ""
    Public Property Adjuntos As New List(Of SolicitudCompraUsoPdf)
    Public ReadOnly Property ArchivosPdf As Integer
        Get
            Return Adjuntos.Count
        End Get
    End Property
End Class

Public Class SolicitudCompraUsoPdf
    Public Property IdAdjunto As Integer
    Public Property NombreArchivo As String = ""
    Public Property TamanoBytes As Long
    ' Nothing para un adjunto ya guardado: se descarga solamente al abrirlo.
    Public Property Contenido As Byte()
    Public Overrides Function ToString() As String
        Return NombreArchivo & " (" & (TamanoBytes / 1024D).ToString("N1") & " KB)"
    End Function
End Class

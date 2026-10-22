Imports Microsoft.Data.SqlClient
Imports System.Data
Imports System.Linq

Public Class SolicitudCompraUsoFiltro
    Public Property Desde As Date?
    Public Property Hasta As Date?
    Public Property IdSector As Integer?
    Public Property Estado As String = ""
    Public Property Solicitante As String = ""
    Public Property Texto As String = ""
End Class

Partial Public Class SolicitudCompraUsoService
    Public Function SectoresConsulta() As DataTable
        Using cn As New SqlConnection(cadena)
            cn.Open()
            Return Tabla(cn, Nothing, "SELECT IdSector,Descripcion + CASE WHEN Activo=0 THEN N' (inactivo)' ELSE N'' END AS Descripcion FROM dbo.Sectores ORDER BY Descripcion;")
        End Using
    End Function

    Public Function Consultar(filtro As SolicitudCompraUsoFiltro) As DataTable
        Using cn As New SqlConnection(cadena)
            cn.Open()
            Return ConsultarEnConexion(cn, Nothing, filtro)
        End Using
    End Function

    Friend Shared Function ConsultarEnConexion(cn As SqlConnection, tr As SqlTransaction, filtro As SolicitudCompraUsoFiltro) As DataTable
        If filtro Is Nothing Then Throw New ArgumentNullException(NameOf(filtro))
        If filtro.Desde.HasValue AndAlso filtro.Hasta.HasValue AndAlso filtro.Desde.Value.Date > filtro.Hasta.Value.Date Then Throw New InvalidOperationException("La fecha desde no puede ser posterior a la fecha hasta.")
        Return Tabla(cn, tr,
            "SELECT S.IdSolicitud,S.FechaSolicitud,T.Descripcion AS Sector,S.UsuarioSolicitante,S.Estado," &
            "CASE S.Prioridad WHEN 'U' THEN N'Urgente' ELSE N'Normal' END AS Prioridad,S.FechaNecesidad,S.Motivo," &
            "(SELECT COUNT(*) FROM dbo.SolicitudCompraUsoDetalle D WHERE D.IdSolicitud=S.IdSolicitud) AS Renglones," &
            "(SELECT COUNT(*) FROM dbo.CotizacionCompraUso C WHERE C.IdSolicitud=S.IdSolicitud) AS Cotizaciones," &
            "S.UsuarioResolucion,S.FechaResolucion,CA.ProveedorNombre AS ProveedorAutorizado,CA.Moneda AS MonedaAutorizada,CA.ImporteTotal AS ImporteAutorizado " &
            "FROM dbo.SolicitudCompraUso S JOIN dbo.Sectores T ON T.IdSector=S.IdSector " &
            "LEFT JOIN dbo.CotizacionCompraUso CA ON CA.IdSolicitud=S.IdSolicitud AND CA.IdCotizacion=S.IdCotizacionAutorizada " &
            "WHERE (@Desde IS NULL OR S.FechaSolicitud>=@Desde) AND (@Hasta IS NULL OR S.FechaSolicitud<=@Hasta) " &
            "AND (@Sector IS NULL OR S.IdSector=@Sector) AND (@Estado='' OR S.Estado=@Estado) " &
            "AND (@Solicitante='' OR S.UsuarioSolicitante LIKE @PatronSolicitante) " &
            "AND (@Texto=N'' OR CONVERT(varchar(11),S.IdSolicitud)=@Texto OR S.Motivo LIKE @Patron " &
            "OR S.Observaciones LIKE @Patron OR T.Descripcion LIKE @Patron " &
            "OR EXISTS (SELECT 1 FROM dbo.CotizacionCompraUso C WHERE C.IdSolicitud=S.IdSolicitud AND C.ProveedorNombre LIKE @Patron)) " &
            "ORDER BY S.IdSolicitud DESC;",
            "@Desde", filtro.Desde?.Date, "@Hasta", filtro.Hasta?.Date, "@Sector", filtro.IdSector,
            "@Estado", If(filtro.Estado, ""), "@Solicitante", If(filtro.Solicitante, "").Trim(),
            "@PatronSolicitante", "%" & If(filtro.Solicitante, "").Trim() & "%",
            "@Texto", If(filtro.Texto, "").Trim(), "@Patron", "%" & If(filtro.Texto, "").Trim() & "%")
    End Function

    Public Function ConsultarHistorial(id As Integer) As DataTable
        Using cn As New SqlConnection(cadena)
            cn.Open()
            Return HistorialEnConexion(cn, Nothing, id)
        End Using
    End Function

    Friend Shared Function HistorialEnConexion(cn As SqlConnection, tr As SqlTransaction, id As Integer) As DataTable
        Return Tabla(cn, tr,
            "SELECT FechaAccion,Usuario,Accion,EstadoAnterior,EstadoNuevo,Observaciones FROM dbo.SolicitudCompraUsoHistorial WHERE IdSolicitud=@Id ORDER BY FechaAccion DESC,IdHistorial DESC;", "@Id", id)
    End Function

    Public Function Resolver(id As Integer, version As Byte(), estadoNuevo As String, observacion As String, usuario As String, Optional idCotizacionAutorizada As Integer? = Nothing) As SolicitudCompraUsoDocumento
        Using cn As New SqlConnection(cadena)
            cn.Open()
            Using tr = cn.BeginTransaction()
                Dim resultado = ResolverEnTransaccion(cn, tr, id, version, estadoNuevo, observacion, usuario, idCotizacionAutorizada)
                tr.Commit()
                Return resultado
            End Using
        End Using
    End Function

    Friend Shared Function ResolverEnTransaccion(cn As SqlConnection, tr As SqlTransaction, id As Integer, version As Byte(), estadoNuevo As String, observacion As String, usuario As String, Optional idCotizacionAutorizada As Integer? = Nothing) As SolicitudCompraUsoDocumento
        If id <= 0 OrElse version Is Nothing OrElse version.Length <> 8 Then Throw New InvalidOperationException("Seleccione y actualice una solicitud antes de resolverla.")
        If String.IsNullOrWhiteSpace(usuario) OrElse usuario.Length > 12 Then Throw New InvalidOperationException("No hay un usuario conectado válido.")
        Dim accion As String
        Select Case estadoNuevo
            Case "AUTORIZADA" : accion = "AUTORIZAR"
            Case "RECHAZADA" : accion = "RECHAZAR"
            Case "DEVUELTA" : accion = "DEVOLVER"
            Case Else : Throw New InvalidOperationException("La resolución indicada no es válida.")
        End Select
        Dim nota = If(observacion, "").Trim()
        If nota.Length > 1000 Then Throw New InvalidOperationException("La observación admite hasta 1000 caracteres.")
        If estadoNuevo <> "AUTORIZADA" AndAlso nota.Length = 0 Then Throw New InvalidOperationException("Indique el motivo del rechazo o de la devolución.")
        Dim cotizacion As Integer? = Nothing
        Dim descripcionCotizacion As String = ""
        If estadoNuevo = "AUTORIZADA" Then
            If Not idCotizacionAutorizada.HasValue OrElse idCotizacionAutorizada.Value <= 0 Then Throw New InvalidOperationException("Seleccione la cotización que desea autorizar.")
            cotizacion = idCotizacionAutorizada
        End If
        Dim actual = Tabla(cn, tr, "SELECT Estado,VersionFila FROM dbo.SolicitudCompraUso WITH (UPDLOCK,HOLDLOCK) WHERE IdSolicitud=@Id;", "@Id", id)
        If actual.Rows.Count <> 1 OrElse CStr(actual.Rows(0)("Estado")) <> "PENDIENTE" OrElse Not version.SequenceEqual(DirectCast(actual.Rows(0)("VersionFila"), Byte())) Then Throw New InvalidOperationException("La solicitud ya fue procesada o modificada por otro usuario. Actualice la bandeja y revise su estado.")
        If cotizacion.HasValue Then
            Dim presupuestos = Tabla(cn, tr, "SELECT ProveedorNombre,Moneda,ImporteTotal FROM dbo.CotizacionCompraUso WITH (HOLDLOCK) WHERE IdSolicitud=@Id AND IdCotizacion=@Cot;", "@Id", id, "@Cot", cotizacion.Value)
            If presupuestos.Rows.Count <> 1 Then Throw New InvalidOperationException("La cotización elegida no pertenece a esta solicitud o ya no existe.")
            Dim p = presupuestos.Rows(0)
            descripcionCotizacion = "Cotización aprobada: " & CStr(p("ProveedorNombre")) & " | " & CStr(p("Moneda")) & " " & If(p.IsNull("ImporteTotal"), "sin importe cargado", Convert.ToDecimal(p("ImporteTotal")).ToString("N2"))
        End If
        ' El UPDATE condicional es atomico: dos ventanas con la misma version
        ' nunca pueden confirmar dos resoluciones para el mismo envio.
        Dim procesada = Escalar(cn, tr,
            "UPDATE dbo.SolicitudCompraUso SET Estado=@Estado,IdCotizacionAutorizada=@Cot,UsuarioResolucion=@Usuario,FechaResolucion=SYSDATETIME()," &
            "ObservacionResolucion=@Nota,UsuarioModificacion=@Usuario,FechaModificacion=SYSDATETIME() " &
            "OUTPUT INSERTED.IdSolicitud WHERE IdSolicitud=@Id AND Estado='PENDIENTE' AND VersionFila=@Version;",
            "@Estado", estadoNuevo, "@Cot", cotizacion, "@Usuario", usuario, "@Nota", nota, "@Id", id, "@Version", version)
        If procesada Is Nothing OrElse procesada Is DBNull.Value Then Throw New InvalidOperationException("La solicitud ya fue procesada o modificada por otro usuario. Actualice la bandeja y revise su estado.")
        Historial(cn, tr, id, usuario, accion, "PENDIENTE", estadoNuevo, If(descripcionCotizacion = "", nota, descripcionCotizacion & If(nota = "", "", Environment.NewLine & nota)))
        Return CargarEnTransaccion(cn, tr, id)
    End Function
End Class

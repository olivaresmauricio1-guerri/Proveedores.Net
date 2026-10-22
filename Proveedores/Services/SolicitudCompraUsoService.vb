Imports System.ComponentModel
Imports System.Data
Imports System.IO
Imports System.Linq
Imports Microsoft.Data.SqlClient

' Usa la misma cadena SQL configurada en General. Las escrituras no pasan por
' consultas HTTP GET: todos los datos y PDF se guardan en una transaccion SQL.
Partial Public Class SolicitudCompraUsoService
    ' Configuracion compartida por las operaciones; no conserva una conexion abierta.
    Private ReadOnly cadena As String

    ' Permite proporcionar otra conexion (por ejemplo, para pruebas).
    ' Si no se proporciona, usa la configuracion de Proveedores definida en General.
    Public Sub New(Optional cadenaConexion As String = Nothing)
        cadena = If(cadenaConexion, General.ProveedoresConnectionString)
    End Sub

    ' SQL representa los valores nulos con DBNull, no con Nothing.
    Private Shared Function Valor(valorObjeto As Object) As Object
        Return If(valorObjeto, DBNull.Value)
    End Function

    ' Prepara SQL parametrizado sobre la conexion y transaccion del llamador.
    ' pares contiene nombres y valores alternados: "@Id", id, "@Usuario", usuario.
    Private Shared Function Comando(cn As SqlConnection, tr As SqlTransaction, sql As String, ParamArray pares() As Object) As SqlCommand
        Dim cmd As New SqlCommand(sql, cn, tr) With {.CommandTimeout = 60}
        For i = 0 To pares.Length - 1 Step 2
            Dim nombre = CStr(pares(i))
            Dim dato = Valor(pares(i + 1))
            If TypeOf dato Is Byte() Then
                ' Los PDF y las versiones de fila se envian como datos binarios.
                ' -1 permite usar VARBINARY(MAX), necesario para archivos grandes.
                cmd.Parameters.Add(nombre, SqlDbType.VarBinary, -1).Value = dato
            Else
                cmd.Parameters.AddWithValue(nombre, dato)
            End If
        Next
        Return cmd
    End Function

    ' Ejecuta una consulta y devuelve sus filas en memoria; libera comando y adaptador.
    Private Shared Function Tabla(cn As SqlConnection, tr As SqlTransaction, sql As String, ParamArray pares() As Object) As DataTable
        Using cmd = Comando(cn, tr, sql, pares), da As New SqlDataAdapter(cmd)
            Dim resultado As New DataTable()
            da.Fill(resultado)
            Return resultado
        End Using
    End Function

    ' Ejecuta INSERT, UPDATE o DELETE sin devolver un conjunto de filas.
    Private Shared Sub Ejecutar(cn As SqlConnection, tr As SqlTransaction, sql As String, ParamArray pares() As Object)
        Using cmd = Comando(cn, tr, sql, pares)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    ' Devuelve la primera columna de la primera fila: un COUNT, un ID o un PDF.
    Private Shared Function Escalar(cn As SqlConnection, tr As SqlTransaction, sql As String, ParamArray pares() As Object) As Object
        Using cmd = Comando(cn, tr, sql, pares)
            Return cmd.ExecuteScalar()
        End Using
    End Function

    ' Lista sectores activos y conserva el sector del documento aunque este inactivo.
    ' Using libera la conexion al terminar, tambien si ocurre una excepcion.
    Public Function Sectores(Optional incluirId As Integer = 0) As DataTable
        Using cn As New SqlConnection(cadena)
            cn.Open()
            Return Tabla(cn, Nothing, "SELECT IdSector, Descripcion + CASE WHEN Activo=0 THEN N' (inactivo)' ELSE N'' END AS Descripcion FROM dbo.Sectores WHERE Activo=1 OR IdSector=@Id ORDER BY Descripcion;", "@Id", incluirId)
        End Using
    End Function

    ' Busca por numero exacto o por coincidencias en motivo, sector y solicitante.
    ' Con texto vacio devuelve todas las solicitudes, ordenadas de la mas nueva a la mas vieja.
    Public Function Buscar(texto As String) As DataTable
        Using cn As New SqlConnection(cadena)
            cn.Open()
            Return Tabla(cn, Nothing,
                "SELECT S.IdSolicitud, S.FechaSolicitud, T.Descripcion AS Sector, S.UsuarioSolicitante, S.Estado, S.Motivo " &
                "FROM dbo.SolicitudCompraUso S JOIN dbo.Sectores T ON T.IdSector=S.IdSector " &
                "WHERE @Texto=N'' OR CONVERT(varchar(11),S.IdSolicitud)=@Texto OR S.Motivo LIKE @Patron " &
                "OR T.Descripcion LIKE @Patron OR S.UsuarioSolicitante LIKE @Patron ORDER BY S.IdSolicitud DESC;",
                "@Texto", texto.Trim(), "@Patron", "%" & texto.Trim() & "%")
        End Using
    End Function

    ' Lee el documento completo dentro de una transaccion.
    ' RepeatableRead evita que cambien las filas ya leidas mientras se arma el documento.
    Public Function Cargar(id As Integer) As SolicitudCompraUsoDocumento
        Using cn As New SqlConnection(cadena)
            cn.Open()
            Using tr = cn.BeginTransaction(IsolationLevel.RepeatableRead)
                Dim doc = CargarEnTransaccion(cn, tr, id)
                tr.Commit()
                Return doc
            End Using
        End Using
    End Function

    ' Convierte un campo SQL de texto; un NULL se muestra como cadena vacia.
    Private Shared Function Texto(r As DataRow, columna As String) As String
        Return If(r.IsNull(columna), "", Convert.ToString(r(columna)))
    End Function

    ' Conserva NULL como fecha ausente y descarta la hora en fechas informativas.
    Private Shared Function FechaOpcional(r As DataRow, columna As String) As Date?
        If r.IsNull(columna) Then Return Nothing
        Return Convert.ToDateTime(r(columna)).Date
    End Function

    ' Distingue un importe sin informar (Nothing) de un importe informado en cero.
    Private Shared Function Importe(r As DataRow, columna As String) As Decimal?
        If r.IsNull(columna) Then Return Nothing
        Return Convert.ToDecimal(r(columna))
    End Function

    ' Arma los modelos usando una transaccion existente, sin confirmarla aqui.
    ' Se reutiliza para abrir documentos y devolver el resultado actualizado del guardado.
    Friend Shared Function CargarEnTransaccion(cn As SqlConnection, tr As SqlTransaction, id As Integer) As SolicitudCompraUsoDocumento
        ' Cabecera y version usada luego para detectar modificaciones de otros usuarios.
        Dim cabecera = Tabla(cn, tr, "SELECT * FROM dbo.SolicitudCompraUso WHERE IdSolicitud=@Id;", "@Id", id)
        If cabecera.Rows.Count <> 1 Then Throw New InvalidOperationException("La solicitud no existe.")
        Dim r = cabecera.Rows(0)
        Dim doc As New SolicitudCompraUsoDocumento With {
            .IdSolicitud = id, .FechaSolicitud = CDate(r("FechaSolicitud")), .IdSector = CInt(r("IdSector")),
            .UsuarioSolicitante = Texto(r, "UsuarioSolicitante"), .Motivo = Texto(r, "Motivo"),
            .Prioridad = Texto(r, "Prioridad"), .FechaNecesidad = FechaOpcional(r, "FechaNecesidad"),
            .Estado = Texto(r, "Estado"), .Observaciones = Texto(r, "Observaciones"),
            .VersionFila = DirectCast(r("VersionFila"), Byte())}
        If Not r.IsNull("IdCotizacionAutorizada") Then doc.IdCotizacionAutorizada = CInt(r("IdCotizacionAutorizada"))
        If Not r.IsNull("FechaResolucion") Then
            ' Texto de consulta con el usuario, momento y observacion de la resolucion.
            doc.Resolucion = Texto(r, "UsuarioResolucion") & " - " & CDate(r("FechaResolucion")).ToString("dd/MM/yyyy HH:mm") & ": " & Texto(r, "ObservacionResolucion")
        End If
        ' Renglones solicitados, respetando su orden de carga.
        For Each item As DataRow In Tabla(cn, tr, "SELECT * FROM dbo.SolicitudCompraUsoDetalle WHERE IdSolicitud=@Id ORDER BY Renglon;", "@Id", id).Rows
            doc.Items.Add(New SolicitudCompraUsoItem With {
                .IdDetalleSolicitud = CInt(item("IdDetalleSolicitud")), .TipoItem = Texto(item, "TipoItem"),
                .Descripcion = Texto(item, "Descripcion"), .Cantidad = CDec(item("Cantidad")), .Unidad = Texto(item, "Unidad"),
                .FechaNecesidad = FechaOpcional(item, "FechaNecesidad"), .Observaciones = Texto(item, "Observaciones")})
        Next
        ' Cotizaciones con sus datos comerciales e importes opcionales.
        For Each c As DataRow In Tabla(cn, tr, "SELECT * FROM dbo.CotizacionCompraUso WHERE IdSolicitud=@Id ORDER BY IdCotizacion;", "@Id", id).Rows
            Dim cot As New SolicitudCompraUsoCotizacion With {
                .IdCotizacion = CInt(c("IdCotizacion")), .ProveedorNombre = Texto(c, "ProveedorNombre"),
                .NumeroCotizacion = Texto(c, "NumeroCotizacion"), .FechaCotizacion = CDate(c("FechaCotizacion")),
                .FechaVencimiento = FechaOpcional(c, "FechaVencimiento"), .Moneda = Texto(c, "Moneda"),
                .ImporteNeto = Importe(c, "ImporteNeto"), .ImporteIVA = Importe(c, "ImporteIVA"),
                .OtrosImportes = Importe(c, "OtrosImportes"), .ImporteTotal = Importe(c, "ImporteTotal"),
                .CondicionPago = Texto(c, "CondicionPago"), .PlazoEntrega = Texto(c, "PlazoEntrega"), .Observaciones = Texto(c, "Observaciones")}
            If Not c.IsNull("NroCuentaProveedor") Then cot.NroCuentaProveedor = CInt(c("NroCuentaProveedor"))
            ' Solo carga los datos identificatorios del PDF, no su contenido.
            ' Los bytes se consultan con LeerPdf cuando el usuario necesita el archivo.
            For Each a As DataRow In Tabla(cn, tr, "SELECT IdAdjunto,NombreArchivo,TamanoBytes FROM dbo.CotizacionCompraUsoAdjunto WHERE IdCotizacion=@Id ORDER BY IdAdjunto;", "@Id", cot.IdCotizacion).Rows
                cot.Adjuntos.Add(New SolicitudCompraUsoPdf With {.IdAdjunto = CInt(a("IdAdjunto")), .NombreArchivo = Texto(a, "NombreArchivo"), .TamanoBytes = CLng(a("TamanoBytes"))})
            Next
            doc.Cotizaciones.Add(cot)
        Next
        Return doc
    End Function

    ' Recupera los bytes de un PDF comprobando que pertenece a la cotizacion indicada.
    Public Function LeerPdf(idAdjunto As Integer, idCotizacion As Integer) As Byte()
        Using cn As New SqlConnection(cadena)
            cn.Open()
            Dim contenido = Escalar(cn, Nothing, "SELECT Contenido FROM dbo.CotizacionCompraUsoAdjunto WHERE IdAdjunto=@Id AND IdCotizacion=@Cot;", "@Id", idAdjunto, "@Cot", idCotizacion)
            If contenido Is Nothing OrElse contenido Is DBNull.Value Then Throw New InvalidOperationException("El PDF ya no está disponible. Actualice la solicitud.")
            Return DirectCast(contenido, Byte())
        End Using
    End Function

    ' Reglas del documento en memoria, sin consultar SQL.
    ' Si un dato no cumple las reglas, lanza una excepcion para impedir el guardado.
    Public Shared Sub Validar(doc As SolicitudCompraUsoDocumento, usuario As String)
        ' Usuario, estado editable, sector y campos obligatorios de la cabecera.
        If String.IsNullOrWhiteSpace(usuario) OrElse usuario.Length > 12 Then Throw New InvalidOperationException("No hay un usuario conectado válido.")
        If Not doc.Editable Then Throw New InvalidOperationException("Sólo pueden modificarse solicitudes en borrador o devueltas.")
        If doc.IdSector <= 0 Then Throw New InvalidOperationException("Seleccione un sector.")
        ValidarTexto(doc.Motivo, 500, "motivo", True)
        ValidarTexto(doc.Observaciones, 1000, "observaciones")
        If doc.Prioridad <> "N" AndAlso doc.Prioridad <> "U" Then Throw New InvalidOperationException("Seleccione una prioridad válida.")
        If doc.Items.Count = 0 Then Throw New InvalidOperationException("Agregue al menos un artículo o servicio.")
        ' Detecta IDs existentes repetidos; varios renglones nuevos pueden tener ID cero.
        If doc.Items.Where(Function(x) x.IdDetalleSolicitud > 0).GroupBy(Function(x) x.IdDetalleSolicitud).Any(Function(g) g.Count() > 1) Then Throw New InvalidOperationException("Hay renglones repetidos.")
        For Each item In doc.Items
            ' Limites de texto, tipo A/S y cantidad compatible con la precision SQL.
            ValidarTexto(item.Descripcion, 300, "descripción del renglón", True)
            ValidarTexto(item.Unidad, 20, "unidad", True)
            ValidarTexto(item.Observaciones, 500, "observaciones del renglón")
            If item.TipoItem <> "A" AndAlso item.TipoItem <> "S" Then Throw New InvalidOperationException("Indique artículo o servicio en cada renglón.")
            If item.Cantidad <= 0 OrElse item.Cantidad > 99999999999999.9999D OrElse Decimal.Round(item.Cantidad, 4) <> item.Cantidad Then Throw New InvalidOperationException("La cantidad debe ser positiva y admitir hasta cuatro decimales.")
        Next
        ' Verifica que no se repitan cotizaciones existentes y valida cada una.
        If doc.Cotizaciones.Where(Function(x) x.IdCotizacion > 0).GroupBy(Function(x) x.IdCotizacion).Any(Function(g) g.Count() > 1) Then Throw New InvalidOperationException("Hay cotizaciones repetidas.")
        For Each cot In doc.Cotizaciones
            ValidarCotizacion(cot)
        Next
    End Sub

    ' Valida proveedor, textos, moneda, fechas, importes opcionales y adjuntos.
    Public Shared Sub ValidarCotizacion(cot As SolicitudCompraUsoCotizacion)
        ValidarTexto(cot.ProveedorNombre, 150, "proveedor de la cotización", True)
        ValidarTexto(cot.NumeroCotizacion, 50, "número de cotización")
        ValidarTexto(cot.CondicionPago, 200, "condición de pago")
        ValidarTexto(cot.PlazoEntrega, 200, "plazo de entrega")
        ValidarTexto(cot.Observaciones, 1000, "observaciones de la cotización")
        If Not System.Text.RegularExpressions.Regex.IsMatch(If(cot.Moneda, ""), "^[A-Z]{3}$") Then Throw New InvalidOperationException("La moneda debe tener tres letras: por ejemplo ARS o USD.")
        If cot.FechaVencimiento.HasValue AndAlso cot.FechaVencimiento.Value.Date < cot.FechaCotizacion.Date Then Throw New InvalidOperationException("El vencimiento de una cotización no puede ser anterior a su fecha.")
        For Each monto In New Decimal?() {cot.ImporteNeto, cot.ImporteIVA, cot.OtrosImportes, cot.ImporteTotal}
            If monto.HasValue AndAlso (monto.Value < 0 OrElse monto.Value > 9999999999999999.99D OrElse Decimal.Round(monto.Value, 2) <> monto.Value) Then Throw New InvalidOperationException("Los importes deben ser no negativos y admitir hasta dos decimales.")
        Next
        If cot.Adjuntos.Where(Function(x) x.IdAdjunto > 0).GroupBy(Function(x) x.IdAdjunto).Any(Function(g) g.Count() > 1) Then Throw New InvalidOperationException("Hay adjuntos repetidos.")
        For Each pdf In cot.Adjuntos
            ' Acepta un nombre PDF sin ruta; exige contenido valido solo para archivos nuevos.
            ValidarTexto(pdf.NombreArchivo, 255, "nombre del PDF", True)
            If Path.GetFileName(pdf.NombreArchivo) <> pdf.NombreArchivo OrElse Not pdf.NombreArchivo.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase) Then Throw New InvalidOperationException("El adjunto debe tener un nombre de archivo PDF válido.")
            If pdf.IdAdjunto = 0 Then ValidarPdf(pdf.Contenido)
        Next
    End Sub

    ' Control comun de obligatoriedad y longitud para los campos de texto.
    Private Shared Sub ValidarTexto(texto As String, maximo As Integer, nombre As String, Optional obligatorio As Boolean = False)
        If obligatorio AndAlso String.IsNullOrWhiteSpace(texto) Then Throw New InvalidOperationException("Complete " & nombre & ".")
        If texto IsNot Nothing AndAlso texto.Length > maximo Then Throw New InvalidOperationException("El campo " & nombre & " admite hasta " & maximo & " caracteres.")
    End Sub

    ' Comprueba la firma inicial %PDF-; no analiza la estructura completa del archivo.
    Public Shared Sub ValidarPdf(contenido As Byte())
        If contenido Is Nothing OrElse contenido.Length < 5 OrElse System.Text.Encoding.ASCII.GetString(contenido, 0, 5) <> "%PDF-" Then Throw New InvalidOperationException("El archivo seleccionado no tiene una cabecera PDF válida.")
    End Sub

    ' Punto de entrada del guardado: todos los cambios comparten conexion y transaccion.
    ' enviar=True guarda y pasa a PENDIENTE; False conserva el estado editable.
    ' Sin Commit, la transaccion se revierte al liberarse si la operacion falla.
    Public Function Guardar(doc As SolicitudCompraUsoDocumento, enviar As Boolean, usuario As String) As SolicitudCompraUsoDocumento
        Validar(doc, usuario)
        Using cn As New SqlConnection(cadena)
            cn.Open()
            Using tr = cn.BeginTransaction()
                Dim guardado = GuardarEnTransaccion(cn, tr, doc, enviar, usuario)
                tr.Commit()
                Return guardado
            End Using
        End Using
    End Function

    ' Registra quien hizo la accion y sus estados; se confirma junto con el documento.
    Private Shared Sub Historial(cn As SqlConnection, tr As SqlTransaction, id As Integer, usuario As String, accion As String, anterior As String, nuevo As String, nota As String)
        Ejecutar(cn, tr, "INSERT INTO dbo.SolicitudCompraUsoHistorial (IdSolicitud,Usuario,Accion,EstadoAnterior,EstadoNuevo,Observaciones) VALUES (@Id,@Usuario,@Accion,@Anterior,@Nuevo,@Nota);",
                 "@Id", id, "@Usuario", usuario, "@Accion", accion, "@Anterior", anterior, "@Nuevo", nuevo, "@Nota", nota)
    End Sub

    ' No confirma la transaccion: el llamador confirma solamente si toda la
    ' operacion termina bien. Permite verificar el servicio con rollback.
    Friend Shared Function GuardarEnTransaccion(cn As SqlConnection, tr As SqlTransaction, doc As SolicitudCompraUsoDocumento, enviar As Boolean, usuario As String) As SolicitudCompraUsoDocumento
        Validar(doc, usuario)
        Dim id = doc.IdSolicitud
        Dim anterior As String = Nothing
        If id > 0 Then
            ' Bloquea la cabecera existente hasta terminar y vuelve a comprobar su estado.
            ' Compara VersionFila para no sobrescribir un documento modificado desde otra ventana.
            Dim actual = Tabla(cn, tr, "SELECT Estado,VersionFila,UsuarioSolicitante FROM dbo.SolicitudCompraUso WITH (UPDLOCK,HOLDLOCK) WHERE IdSolicitud=@Id;", "@Id", id)
            If actual.Rows.Count <> 1 Then Throw New InvalidOperationException("La solicitud ya no existe.")
            anterior = CStr(actual.Rows(0)("Estado"))
            If anterior <> "BORRADOR" AndAlso anterior <> "DEVUELTA" Then Throw New InvalidOperationException("La solicitud ya fue enviada o resuelta. Actualice la consulta.")
            If doc.VersionFila Is Nothing OrElse Not doc.VersionFila.SequenceEqual(DirectCast(actual.Rows(0)("VersionFila"), Byte())) Then Throw New InvalidOperationException("Otro usuario modificó la solicitud. Vuelva a abrirla antes de guardar.")
        End If
        ' Revalida el sector en SQL; pudo desactivarse despues de cargar el combo.
        If CInt(Escalar(cn, tr, "SELECT COUNT(*) FROM dbo.Sectores WITH (HOLDLOCK) WHERE IdSector=@Id AND Activo=1;", "@Id", doc.IdSector)) <> 1 Then Throw New InvalidOperationException("El sector está inactivo o no existe. Seleccione un sector activo.")
        If id = 0 Then
            ' Alta: SQL devuelve el nuevo ID, necesario para guardar los datos relacionados.
            id = CInt(Escalar(cn, tr, "INSERT INTO dbo.SolicitudCompraUso (FechaSolicitud,IdSector,UsuarioSolicitante,Motivo,Prioridad,FechaNecesidad,Observaciones) OUTPUT INSERTED.IdSolicitud VALUES (@Fecha,@Sector,@Usuario,@Motivo,@Prioridad,@Necesidad,@Obs);",
                "@Fecha", doc.FechaSolicitud.Date, "@Sector", doc.IdSector, "@Usuario", usuario, "@Motivo", doc.Motivo.Trim(), "@Prioridad", doc.Prioridad, "@Necesidad", doc.FechaNecesidad, "@Obs", doc.Observaciones))
            Historial(cn, tr, id, usuario, "CREAR", Nothing, "BORRADOR", "Solicitud creada.")
        Else
            ' Modificacion: actualiza la cabecera y deja constancia del usuario y momento.
            Ejecutar(cn, tr, "UPDATE dbo.SolicitudCompraUso SET FechaSolicitud=@Fecha,IdSector=@Sector,Motivo=@Motivo,Prioridad=@Prioridad,FechaNecesidad=@Necesidad,Observaciones=@Obs,UsuarioModificacion=@Usuario,FechaModificacion=SYSDATETIME() WHERE IdSolicitud=@Id;",
                "@Fecha", doc.FechaSolicitud.Date, "@Sector", doc.IdSector, "@Motivo", doc.Motivo.Trim(), "@Prioridad", doc.Prioridad, "@Necesidad", doc.FechaNecesidad, "@Obs", doc.Observaciones, "@Usuario", usuario, "@Id", id)
        End If

        ' Sincroniza cotizaciones: rechaza IDs ajenos y elimina las quitadas del documento.
        ' Antes de borrar una cotizacion elimina sus adjuntos y su desglose.
        Dim cotActuales = Tabla(cn, tr, "SELECT IdCotizacion,ProveedorNombre FROM dbo.CotizacionCompraUso WHERE IdSolicitud=@Id;", "@Id", id)
        Dim idsCot = New HashSet(Of Integer)(cotActuales.AsEnumerable().Select(Function(r) CInt(r("IdCotizacion"))))
        If doc.Cotizaciones.Any(Function(c) c.IdCotizacion > 0 AndAlso Not idsCot.Contains(c.IdCotizacion)) Then Throw New InvalidOperationException("Una cotización no pertenece a esta solicitud.")
        For Each r As DataRow In cotActuales.Rows
            Dim cotId = CInt(r("IdCotizacion"))
            If doc.Cotizaciones.Any(Function(c) c.IdCotizacion = cotId) Then Continue For
            Historial(cn, tr, id, usuario, "QUITAR_COTIZACION", If(anterior, "BORRADOR"), If(anterior, "BORRADOR"), "Cotización quitada: " & CStr(r("ProveedorNombre")))
            Ejecutar(cn, tr, "DELETE FROM dbo.CotizacionCompraUsoAdjunto WHERE IdCotizacion=@Cot; DELETE FROM dbo.CotizacionCompraUsoDetalle WHERE IdCotizacion=@Cot; DELETE FROM dbo.CotizacionCompraUso WHERE IdCotizacion=@Cot AND IdSolicitud=@Id;", "@Cot", cotId, "@Id", id)
        Next
        ' Sincroniza renglones: rechaza IDs ajenos y borra los quitados,
        ' salvo que tengan un desglose de cotizacion asociado.
        Dim detalleActual = Tabla(cn, tr, "SELECT IdDetalleSolicitud FROM dbo.SolicitudCompraUsoDetalle WHERE IdSolicitud=@Id;", "@Id", id)
        Dim idsDetalle = New HashSet(Of Integer)(detalleActual.AsEnumerable().Select(Function(r) CInt(r("IdDetalleSolicitud"))))
        If doc.Items.Any(Function(i) i.IdDetalleSolicitud > 0 AndAlso Not idsDetalle.Contains(i.IdDetalleSolicitud)) Then Throw New InvalidOperationException("Un renglón no pertenece a esta solicitud.")
        For Each r As DataRow In detalleActual.Rows
            Dim itemId = CInt(r("IdDetalleSolicitud"))
            If doc.Items.Any(Function(i) i.IdDetalleSolicitud = itemId) Then Continue For
            If CInt(Escalar(cn, tr, "SELECT COUNT(*) FROM dbo.CotizacionCompraUsoDetalle WHERE IdDetalleSolicitud=@Item;", "@Item", itemId)) > 0 Then Throw New InvalidOperationException("No puede quitar un renglón que tiene un desglose de cotización asociado.")
            Ejecutar(cn, tr, "DELETE FROM dbo.SolicitudCompraUsoDetalle WHERE IdSolicitud=@Id AND IdDetalleSolicitud=@Item;", "@Id", id, "@Item", itemId)
        Next
        ' Evita colisiones del indice unico al reordenar renglones existentes.
        Dim maxRenglon = CInt(Escalar(cn, tr, "SELECT ISNULL(MAX(Renglon),0) FROM dbo.SolicitudCompraUsoDetalle WHERE IdSolicitud=@Id;", "@Id", id))
        Ejecutar(cn, tr, "UPDATE dbo.SolicitudCompraUsoDetalle SET Renglon=Renglon+@Desplazamiento WHERE IdSolicitud=@Id;", "@Desplazamiento", maxRenglon + doc.Items.Count + 1, "@Id", id)
        Dim renglon = 0
        ' Asigna el orden final: inserta renglones nuevos y actualiza los existentes.
        For Each item In doc.Items
            renglon += 1
            Dim parametros As Object() = {"@Id", id, "@Renglon", renglon, "@Tipo", item.TipoItem, "@Descripcion", item.Descripcion.Trim(), "@Cantidad", item.Cantidad, "@Unidad", item.Unidad.Trim(), "@Necesidad", item.FechaNecesidad, "@Obs", item.Observaciones, "@Item", item.IdDetalleSolicitud}
            If item.IdDetalleSolicitud = 0 Then
                Ejecutar(cn, tr, "INSERT INTO dbo.SolicitudCompraUsoDetalle (IdSolicitud,Renglon,TipoItem,Descripcion,Cantidad,Unidad,FechaNecesidad,Observaciones) VALUES (@Id,@Renglon,@Tipo,@Descripcion,@Cantidad,@Unidad,@Necesidad,@Obs);", parametros)
            Else
                Ejecutar(cn, tr, "UPDATE dbo.SolicitudCompraUsoDetalle SET Renglon=@Renglon,TipoItem=@Tipo,Descripcion=@Descripcion,Cantidad=@Cantidad,Unidad=@Unidad,FechaNecesidad=@Necesidad,Observaciones=@Obs WHERE IdSolicitud=@Id AND IdDetalleSolicitud=@Item;", parametros)
            End If
        Next

        ' Guarda los datos comerciales de cada cotizacion, obteniendo el ID de las nuevas.
        For Each cot In doc.Cotizaciones
            Dim cotId = cot.IdCotizacion
            Dim pares As Object() = {"@Id", id, "@Cuenta", cot.NroCuentaProveedor, "@Proveedor", cot.ProveedorNombre.Trim(), "@Numero", cot.NumeroCotizacion, "@Fecha", cot.FechaCotizacion.Date, "@Vto", cot.FechaVencimiento, "@Moneda", cot.Moneda,
                "@Neto", cot.ImporteNeto, "@IVA", cot.ImporteIVA, "@Otros", cot.OtrosImportes, "@Total", cot.ImporteTotal, "@Pago", cot.CondicionPago, "@Entrega", cot.PlazoEntrega, "@Obs", cot.Observaciones, "@Usuario", usuario, "@Cot", cotId}
            If cotId = 0 Then
                cotId = CInt(Escalar(cn, tr, "INSERT INTO dbo.CotizacionCompraUso (IdSolicitud,NroCuentaProveedor,ProveedorNombre,NumeroCotizacion,FechaCotizacion,FechaVencimiento,Moneda,ImporteNeto,ImporteIVA,OtrosImportes,ImporteTotal,CondicionPago,PlazoEntrega,Observaciones,UsuarioAlta) OUTPUT INSERTED.IdCotizacion VALUES (@Id,@Cuenta,@Proveedor,@Numero,@Fecha,@Vto,@Moneda,@Neto,@IVA,@Otros,@Total,@Pago,@Entrega,@Obs,@Usuario);", pares))
            Else
                Ejecutar(cn, tr, "UPDATE dbo.CotizacionCompraUso SET NroCuentaProveedor=@Cuenta,ProveedorNombre=@Proveedor,NumeroCotizacion=@Numero,FechaCotizacion=@Fecha,FechaVencimiento=@Vto,Moneda=@Moneda,ImporteNeto=@Neto,ImporteIVA=@IVA,OtrosImportes=@Otros,ImporteTotal=@Total,CondicionPago=@Pago,PlazoEntrega=@Entrega,Observaciones=@Obs WHERE IdSolicitud=@Id AND IdCotizacion=@Cot;", pares)
            End If
            Historial(cn, tr, id, usuario, If(cot.IdCotizacion = 0, "AGREGAR_COTIZACION", "MODIFICAR_COTIZACION"), If(anterior, "BORRADOR"), If(anterior, "BORRADOR"), "Cotización: " & cot.ProveedorNombre)
            ' Mantiene los PDF existentes seleccionados y elimina los que el usuario quito.
            ' Rechaza IDs de archivos que no pertenezcan a esta cotizacion.
            Dim existentes = Tabla(cn, tr, "SELECT IdAdjunto,NombreArchivo FROM dbo.CotizacionCompraUsoAdjunto WHERE IdCotizacion=@Cot;", "@Cot", cotId)
            Dim idsPdf = New HashSet(Of Integer)(existentes.AsEnumerable().Select(Function(r) CInt(r("IdAdjunto"))))
            If cot.Adjuntos.Any(Function(a) a.IdAdjunto > 0 AndAlso Not idsPdf.Contains(a.IdAdjunto)) Then Throw New InvalidOperationException("Un PDF no pertenece a esta cotización.")
            For Each r As DataRow In existentes.Rows
                Dim pdfId = CInt(r("IdAdjunto"))
                If cot.Adjuntos.Any(Function(a) a.IdAdjunto = pdfId) Then Continue For
                Ejecutar(cn, tr, "DELETE FROM dbo.CotizacionCompraUsoAdjunto WHERE IdAdjunto=@Pdf AND IdCotizacion=@Cot;", "@Pdf", pdfId, "@Cot", cotId)
                Historial(cn, tr, id, usuario, "QUITAR_PDF", If(anterior, "BORRADOR"), If(anterior, "BORRADOR"), CStr(r("NombreArchivo")))
            Next
            ' Inserta solo PDF nuevos; los existentes no se vuelven a transferir.
            For Each pdf In cot.Adjuntos.Where(Function(a) a.IdAdjunto = 0)
                Ejecutar(cn, tr, "INSERT INTO dbo.CotizacionCompraUsoAdjunto (IdCotizacion,NombreArchivo,Contenido,UsuarioAlta) VALUES (@Cot,@Nombre,@Contenido,@Usuario);", "@Cot", cotId, "@Nombre", pdf.NombreArchivo, "@Contenido", pdf.Contenido, "@Usuario", usuario)
                Historial(cn, tr, id, usuario, "ADJUNTAR_PDF", If(anterior, "BORRADOR"), If(anterior, "BORRADOR"), pdf.NombreArchivo)
            Next
        Next
        ' Registra la modificacion general y, si se solicito, el envio a autorizacion.
        If doc.IdSolicitud > 0 Then Historial(cn, tr, id, usuario, "MODIFICAR", anterior, anterior, "Solicitud guardada con " & doc.Items.Count & " renglones y " & doc.Cotizaciones.Count & " cotizaciones.")
        If enviar Then
            Ejecutar(cn, tr, "UPDATE dbo.SolicitudCompraUso SET Estado='PENDIENTE',UsuarioModificacion=@Usuario,FechaModificacion=SYSDATETIME() WHERE IdSolicitud=@Id;", "@Usuario", usuario, "@Id", id)
            Historial(cn, tr, id, usuario, "ENVIAR", If(anterior, "BORRADOR"), "PENDIENTE", "Enviada a autorización.")
        End If
        ' Devuelve el documento con IDs, estado y version actualizados por SQL.
        ' El llamador recibe este resultado solo despues de confirmar la transaccion.
        Return CargarEnTransaccion(cn, tr, id)
    End Function
End Class

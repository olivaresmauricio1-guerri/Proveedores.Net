Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class AfipPadronImporterService

    Public Shared Sub ImportarPadronApocrifos(rutaArchivo As String)

        If Not System.IO.File.Exists(rutaArchivo) Then
            Throw New System.IO.FileNotFoundException(
                "No se encontró el archivo de padrón de AFIP.", rutaArchivo)
        End If

        Dim lineas = System.IO.File.ReadAllLines(rutaArchivo, System.Text.Encoding.UTF8)

        DSM.Execute(DSM.Proveedores, "TRUNCATE TABLE CuitApocrifo")

        Dim fechaImportacion As DateTime = DateTime.Now
        Dim insertados As Integer = 0
        Dim omitidos As Integer = 0

        For Each linea In lineas

            If String.IsNullOrWhiteSpace(linea) OrElse linea.TrimStart().StartsWith("#") Then
                Continue For
            End If

            Dim campos = linea.Split(","c)

            If campos.Length < 3 Then
                omitidos += 1
                Continue For
            End If

            Dim cuitTexto = campos(0).Trim()
            Dim cuit As Long

            If Not Long.TryParse(cuitTexto, cuit) Then
                omitidos += 1
                Continue For
            End If

            Dim fechaCondicion As DateTime? = ParsearFecha(campos(1))
            Dim fechaPublicacion As DateTime? = ParsearFecha(campos(2))
            Dim descripcion As String = If(campos.Length > 3, campos(3).Trim(), "")

            Dim sql As String =
                "INSERT INTO CuitApocrifo (Cuit, FechaCondicion, FechaPublicacion, Descripcion, FechaImportacion) " &
                "VALUES (@Cuit, @FechaCondicion, @FechaPublicacion, @Descripcion, @FechaImportacion)"

            Dim parametros As New Dictionary(Of String, Object) From {
                {"@Cuit", cuit},
                {"@FechaCondicion", CType(IIf(fechaCondicion.HasValue, fechaCondicion.Value, DBNull.Value), Object)},
                {"@FechaPublicacion", CType(IIf(fechaPublicacion.HasValue, fechaPublicacion.Value, DBNull.Value), Object)},
                {"@Descripcion", descripcion},
                {"@FechaImportacion", fechaImportacion}
            }

            DSM.Execute(DSM.Proveedores, sql, parametros)
            insertados += 1

        Next

    End Sub

    Private Shared Function ParsearFecha(texto As String) As DateTime?
        Dim fecha As DateTime
        If DateTime.TryParseExact(texto.Trim(), "dd/MM/yyyy",
                                   Globalization.CultureInfo.InvariantCulture,
                                   Globalization.DateTimeStyles.None, fecha) Then
            Return fecha
        End If
        Return Nothing
    End Function

End Class
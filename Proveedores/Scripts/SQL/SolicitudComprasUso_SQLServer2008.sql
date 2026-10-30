/*
Solicitud Compras: instalacion inicial para SQL Server 2008 / 2008 R2.
Ejecutar en SSMS sobre la base indicada por General.ProveedoresConnectionString.
No modifica cuenta corriente, stock, proveedores ni tablas de compras existentes.
No es una migracion: si ya existe una tabla de compras, se detiene sin cambios.
Sectores se reutiliza si existe y contiene IdSector, Descripcion y Activo.
No se cargan sectores automaticamente: usar los sectores reales de la empresa.
*/
SET NOCOUNT ON;
SET XACT_ABORT ON;

IF OBJECT_ID(N'dbo.SolicitudCompraUso', N'U') IS NOT NULL
   OR OBJECT_ID(N'dbo.SolicitudCompraUsoDetalle', N'U') IS NOT NULL
   OR OBJECT_ID(N'dbo.CotizacionCompraUso', N'U') IS NOT NULL
   OR OBJECT_ID(N'dbo.CotizacionCompraUsoDetalle', N'U') IS NOT NULL
   OR OBJECT_ID(N'dbo.CotizacionCompraUsoAdjunto', N'U') IS NOT NULL
   OR OBJECT_ID(N'dbo.SolicitudCompraUsoHistorial', N'U') IS NOT NULL
BEGIN
    RAISERROR(N'Ya existen tablas de compras. Este script es solo para instalacion inicial; no se realizaron cambios.', 16, 1);
    RETURN;
END;

IF OBJECT_ID(N'dbo.Sectores', N'U') IS NOT NULL
   AND (COL_LENGTH(N'dbo.Sectores', N'IdSector') IS NULL
        OR COL_LENGTH(N'dbo.Sectores', N'Descripcion') IS NULL
        OR COL_LENGTH(N'dbo.Sectores', N'Activo') IS NULL)
BEGIN
    RAISERROR(N'Sectores existe pero no contiene todos los campos requeridos. Revisar su estructura antes de instalar.', 16, 1);
    RETURN;
END;

BEGIN TRY
    BEGIN TRANSACTION;

    IF OBJECT_ID(N'dbo.Sectores', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.Sectores
        (
            IdSector INT IDENTITY(1,1) NOT NULL
                CONSTRAINT PK_Sectores PRIMARY KEY,
            Descripcion NVARCHAR(100) NOT NULL,
            Activo BIT NOT NULL CONSTRAINT DF_Sectores_Activo DEFAULT (1)
        );
    END;

    CREATE TABLE dbo.SolicitudCompraUso
    (
        IdSolicitud INT IDENTITY(1,1) NOT NULL
            CONSTRAINT PK_SolicitudCompraUso PRIMARY KEY,
        FechaSolicitud DATE NOT NULL,
        IdSector INT NOT NULL,
        UsuarioSolicitante NVARCHAR(12) NOT NULL,
        Motivo NVARCHAR(500) NOT NULL,
        Prioridad CHAR(1) NOT NULL
            CONSTRAINT DF_SolicitudCompraUso_Prioridad DEFAULT ('N'),
        FechaNecesidad DATE NULL,
        Estado VARCHAR(12) NOT NULL
            CONSTRAINT DF_SolicitudCompraUso_Estado DEFAULT ('BORRADOR'),
        Observaciones NVARCHAR(1000) NULL,
        IdCotizacionAutorizada INT NULL,
        UsuarioResolucion NVARCHAR(12) NULL,
        FechaResolucion DATETIME2(7) NULL,
        ObservacionResolucion NVARCHAR(1000) NULL,
        FechaAlta DATETIME2(7) NOT NULL
            CONSTRAINT DF_SolicitudCompraUso_FechaAlta DEFAULT (SYSDATETIME()),
        UsuarioModificacion NVARCHAR(12) NULL,
        FechaModificacion DATETIME2(7) NULL,
        -- SQL genera y cambia esta version en cada UPDATE de la cabecera.
        VersionFila ROWVERSION NOT NULL,
        CONSTRAINT FK_SolicitudCompraUso_Sector FOREIGN KEY (IdSector)
            REFERENCES dbo.Sectores (IdSector),
        CONSTRAINT CK_SolicitudCompraUso_Prioridad CHECK (Prioridad IN ('N','U')),
        CONSTRAINT CK_SolicitudCompraUso_Estado CHECK
            (Estado IN ('BORRADOR','PENDIENTE','AUTORIZADA','RECHAZADA','DEVUELTA'))
    );

    CREATE TABLE dbo.SolicitudCompraUsoDetalle
    (
        IdDetalleSolicitud INT IDENTITY(1,1) NOT NULL
            CONSTRAINT PK_SolicitudCompraUsoDetalle PRIMARY KEY,
        IdSolicitud INT NOT NULL,
        -- Debe ser INT: el servicio desplaza temporalmente los renglones al reordenar.
        Renglon INT NOT NULL,
        TipoItem CHAR(1) NOT NULL,
        Descripcion NVARCHAR(300) NOT NULL,
        Cantidad DECIMAL(18,4) NOT NULL,
        Unidad NVARCHAR(20) NOT NULL,
        FechaNecesidad DATE NULL,
        Observaciones NVARCHAR(500) NULL,
        CONSTRAINT FK_SolicitudCompraUsoDetalle_Solicitud FOREIGN KEY (IdSolicitud)
            REFERENCES dbo.SolicitudCompraUso (IdSolicitud),
        CONSTRAINT UQ_SolicitudCompraUsoDetalle_Renglon UNIQUE (IdSolicitud,Renglon),
        CONSTRAINT CK_SolicitudCompraUsoDetalle_Tipo CHECK (TipoItem IN ('A','S')),
        CONSTRAINT CK_SolicitudCompraUsoDetalle_Cantidad CHECK (Cantidad > 0),
        CONSTRAINT CK_SolicitudCompraUsoDetalle_Renglon CHECK (Renglon > 0)
    );

    CREATE TABLE dbo.CotizacionCompraUso
    (
        IdCotizacion INT IDENTITY(1,1) NOT NULL
            CONSTRAINT PK_CotizacionCompraUso PRIMARY KEY,
        IdSolicitud INT NOT NULL,
        -- Referencia informativa a MaeCtaCte; se permite proveedor escrito a mano.
        NroCuentaProveedor INT NULL,
        ProveedorNombre NVARCHAR(150) NOT NULL,
        NumeroCotizacion NVARCHAR(50) NULL,
        FechaCotizacion DATE NOT NULL,
        FechaVencimiento DATE NULL,
        Moneda CHAR(3) NOT NULL,
        ImporteNeto DECIMAL(18,2) NULL,
        ImporteIVA DECIMAL(18,2) NULL,
        OtrosImportes DECIMAL(18,2) NULL,
        ImporteTotal DECIMAL(18,2) NULL,
        CondicionPago NVARCHAR(200) NULL,
        PlazoEntrega NVARCHAR(200) NULL,
        Observaciones NVARCHAR(1000) NULL,
        UsuarioAlta NVARCHAR(12) NOT NULL,
        FechaAlta DATETIME2(7) NOT NULL
            CONSTRAINT DF_CotizacionCompraUso_FechaAlta DEFAULT (SYSDATETIME()),
        CONSTRAINT FK_CotizacionCompraUso_Solicitud FOREIGN KEY (IdSolicitud)
            REFERENCES dbo.SolicitudCompraUso (IdSolicitud),
        CONSTRAINT UQ_CotizacionCompraUso_SolicitudCotizacion UNIQUE (IdSolicitud,IdCotizacion),
        CONSTRAINT CK_CotizacionCompraUso_Vencimiento CHECK
            (FechaVencimiento IS NULL OR FechaVencimiento >= FechaCotizacion),
        CONSTRAINT CK_CotizacionCompraUso_Importes CHECK
            ((ImporteNeto IS NULL OR ImporteNeto >= 0)
             AND (ImporteIVA IS NULL OR ImporteIVA >= 0)
             AND (OtrosImportes IS NULL OR OtrosImportes >= 0)
             AND (ImporteTotal IS NULL OR ImporteTotal >= 0))
    );

    -- Impide seleccionar una cotizacion perteneciente a otra solicitud.
    -- Sin cascadas: el servicio realiza las eliminaciones explicitamente.
    ALTER TABLE dbo.SolicitudCompraUso ADD
        CONSTRAINT FK_SolicitudCompraUso_CotizacionAutorizada
        FOREIGN KEY (IdSolicitud,IdCotizacionAutorizada)
        REFERENCES dbo.CotizacionCompraUso (IdSolicitud,IdCotizacion);

    /*
    Desglose reservado para precios por renglon.
    El servicio actual solo consulta IdDetalleSolicitud y elimina por IdCotizacion;
    no inserta ni edita el desglose. Por eso esta tabla permanecera vacia.
    */
    CREATE TABLE dbo.CotizacionCompraUsoDetalle
    (
        IdDetalleCotizacion INT IDENTITY(1,1) NOT NULL
            CONSTRAINT PK_CotizacionCompraUsoDetalle PRIMARY KEY,
        IdCotizacion INT NOT NULL,
        IdDetalleSolicitud INT NOT NULL,
        Cantidad DECIMAL(18,4) NOT NULL,
        PrecioUnitario DECIMAL(18,2) NOT NULL,
        Subtotal DECIMAL(18,2) NULL,
        Observaciones NVARCHAR(500) NULL,
        CONSTRAINT FK_CotizacionCompraUsoDetalle_Cotizacion FOREIGN KEY (IdCotizacion)
            REFERENCES dbo.CotizacionCompraUso (IdCotizacion),
        CONSTRAINT FK_CotizacionCompraUsoDetalle_Renglon FOREIGN KEY (IdDetalleSolicitud)
            REFERENCES dbo.SolicitudCompraUsoDetalle (IdDetalleSolicitud),
        CONSTRAINT UQ_CotizacionCompraUsoDetalle_Item UNIQUE (IdCotizacion,IdDetalleSolicitud),
        CONSTRAINT CK_CotizacionCompraUsoDetalle_Importes CHECK
            (Cantidad > 0 AND PrecioUnitario >= 0 AND (Subtotal IS NULL OR Subtotal >= 0))
    );

    CREATE TABLE dbo.CotizacionCompraUsoAdjunto
    (
        IdAdjunto INT IDENTITY(1,1) NOT NULL
            CONSTRAINT PK_CotizacionCompraUsoAdjunto PRIMARY KEY,
        IdCotizacion INT NOT NULL,
        NombreArchivo NVARCHAR(255) NOT NULL,
        Contenido VARBINARY(MAX) NOT NULL,
        -- El INSERT del servicio no envia TamanoBytes; se calcula desde el PDF.
        TamanoBytes AS (CONVERT(BIGINT,DATALENGTH(Contenido))) PERSISTED,
        UsuarioAlta NVARCHAR(12) NOT NULL,
        FechaAlta DATETIME2(7) NOT NULL
            CONSTRAINT DF_CotizacionCompraUsoAdjunto_FechaAlta DEFAULT (SYSDATETIME()),
        CONSTRAINT FK_CotizacionCompraUsoAdjunto_Cotizacion FOREIGN KEY (IdCotizacion)
            REFERENCES dbo.CotizacionCompraUso (IdCotizacion)
    );

    CREATE TABLE dbo.SolicitudCompraUsoHistorial
    (
        IdHistorial INT IDENTITY(1,1) NOT NULL
            CONSTRAINT PK_SolicitudCompraUsoHistorial PRIMARY KEY,
        IdSolicitud INT NOT NULL,
        FechaAccion DATETIME2(7) NOT NULL
            CONSTRAINT DF_SolicitudCompraUsoHistorial_Fecha DEFAULT (SYSDATETIME()),
        Usuario NVARCHAR(12) NOT NULL,
        Accion VARCHAR(30) NOT NULL,
        EstadoAnterior VARCHAR(12) NULL,
        EstadoNuevo VARCHAR(12) NOT NULL,
        Observaciones NVARCHAR(2000) NULL,
        CONSTRAINT FK_SolicitudCompraUsoHistorial_Solicitud FOREIGN KEY (IdSolicitud)
            REFERENCES dbo.SolicitudCompraUso (IdSolicitud)
    );

    CREATE INDEX IX_SolicitudCompraUso_Fecha
        ON dbo.SolicitudCompraUso (FechaSolicitud);
    CREATE INDEX IX_SolicitudCompraUso_EstadoFecha
        ON dbo.SolicitudCompraUso (Estado,FechaSolicitud);
    CREATE INDEX IX_SolicitudCompraUso_Sector
        ON dbo.SolicitudCompraUso (IdSector);
    CREATE INDEX IX_CotizacionCompraUsoDetalle_Renglon
        ON dbo.CotizacionCompraUsoDetalle (IdDetalleSolicitud);
    CREATE INDEX IX_CotizacionCompraUsoAdjunto_Cotizacion
        ON dbo.CotizacionCompraUsoAdjunto (IdCotizacion);
    CREATE INDEX IX_SolicitudCompraUsoHistorial_Solicitud
        ON dbo.SolicitudCompraUsoHistorial (IdSolicitud,FechaAccion DESC,IdHistorial DESC);

    COMMIT TRANSACTION;
    PRINT N'Tablas de Solicitud Compras creadas. Cargar sectores antes de crear solicitudes.';
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    DECLARE @Mensaje NVARCHAR(4000);
    SET @Mensaje = ERROR_MESSAGE();
    RAISERROR(N'No se completo la instalacion: %s', 16, 1, @Mensaje);
END CATCH;

/*
Ejemplo de carga manual si Sectores se creo con este script:

INSERT INTO dbo.Sectores (Descripcion,Activo) VALUES (N'Administracion',1);
INSERT INTO dbo.Sectores (Descripcion,Activo) VALUES (N'Mantenimiento',1);

No ejecutar estos ejemplos si ya existen esos sectores.
*/

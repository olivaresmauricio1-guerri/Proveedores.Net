-----------------------------------------------------------------------------
-- Script: Mantenimiento de Vehículos - Tablas para Proveedores
-- Aplicar en la base de datos: Proveedores (conexión DSM.Proveedores)
-- Si la tabla Sucursales está en otra base (ej: Stock), reemplazar la FK
-- o dejar IdSucursal como INT sin FK declarada.
-----------------------------------------------------------------------------
SET NOCOUNT ON
GO

IF OBJECT_ID('dbo.ServiciosItems', 'U') IS NOT NULL DROP TABLE dbo.ServiciosItems
IF OBJECT_ID('dbo.Servicios',       'U') IS NOT NULL DROP TABLE dbo.Servicios
IF OBJECT_ID('dbo.Repuestos',       'U') IS NOT NULL DROP TABLE dbo.Repuestos
IF OBJECT_ID('dbo.TipoServicio',    'U') IS NOT NULL DROP TABLE dbo.TipoServicio
IF OBJECT_ID('dbo.Vehiculos',       'U') IS NOT NULL DROP TABLE dbo.Vehiculos
GO

-----------------------------------------------------------------------------
-- 1) Vehículos - identificador natural: Patente
-----------------------------------------------------------------------------
CREATE TABLE dbo.Vehiculos (
    IdVehiculo        INT IDENTITY(1,1) NOT NULL,
    Patente           VARCHAR(15)   NOT NULL,
    Marca             VARCHAR(40)   NULL,
    Modelo            VARCHAR(40)   NULL,
    Anio              SMALLINT      NULL,
    KmActual          INT           NOT NULL CONSTRAINT DF_Vehiculos_KmActual DEFAULT ((0)),
    IdSucursal        INT           NULL,
    FechaAlta         DATE          NULL CONSTRAINT DF_Vehiculos_FechaAlta DEFAULT (GETDATE()),
    FechaBaja         DATE          NULL,
    Activo            BIT           NOT NULL CONSTRAINT DF_Vehiculos_Activo DEFAULT ((1)),
    Comentario        VARCHAR(255)  NULL,
    IdUsuarioMod      VARCHAR(20)   NULL,
    FechaHoraMod      DATETIME      NULL,
    CONSTRAINT PK_Vehiculos PRIMARY KEY CLUSTERED (IdVehiculo ASC)
)
GO
CREATE UNIQUE NONCLUSTERED INDEX IX_Vehiculos_Patente ON dbo.Vehiculos (Patente ASC)
GO
CREATE NONCLUSTERED INDEX IX_Vehiculos_Sucursal ON dbo.Vehiculos (IdSucursal ASC)
GO
-- Si tu tabla Sucursales vive en la misma base, descomentar:
-- ALTER TABLE dbo.Vehiculos ADD CONSTRAINT FK_Vehiculos_Sucursales
--   FOREIGN KEY (IdSucursal) REFERENCES Stock.dbo.Sucursales (IdSucursal)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Patente (identificador legible)', @level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'Vehiculos',@level2type=N'COLUMN',@level2name=N'Patente'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Km actuales (se actualiza al ingresar servicio)', @level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'Vehiculos',@level2type=N'COLUMN',@level2name=N'KmActual'
GO

-----------------------------------------------------------------------------
-- 2) Tipos de Servicio
-----------------------------------------------------------------------------
CREATE TABLE dbo.TipoServicio (
    IdTipoServicio    INT IDENTITY(1,1) NOT NULL,
    Descripcion       VARCHAR(60)   NOT NULL,
    CostoRef          DECIMAL(14,2) NULL,
    KmRecomendado     INT           NULL,
    DiasRecomendados  SMALLINT      NULL,
    Activo            BIT           NOT NULL CONSTRAINT DF_TipoServicio_Activo DEFAULT ((1)),
    CONSTRAINT PK_TipoServicio PRIMARY KEY CLUSTERED (IdTipoServicio ASC)
)
GO
CREATE UNIQUE NONCLUSTERED INDEX IX_TipoServicio_Descripcion ON dbo.TipoServicio (Descripcion ASC)
GO

-----------------------------------------------------------------------------
-- 3) Repuestos / Artículos
-----------------------------------------------------------------------------
CREATE TABLE dbo.Repuestos (
    IdRepuesto        INT IDENTITY(1,1) NOT NULL,
    Codigo            VARCHAR(30)   NULL,
    Descripcion       VARCHAR(255)  NOT NULL,
    Rubro             VARCHAR(40)   NULL,
    MarcaArticulo     VARCHAR(40)   NULL,
    CostoRef          DECIMAL(14,2) NULL,
    Activo            BIT           NOT NULL CONSTRAINT DF_Repuestos_Activo DEFAULT ((1)),
    CONSTRAINT PK_Repuestos PRIMARY KEY CLUSTERED (IdRepuesto ASC)
)
GO
CREATE UNIQUE NONCLUSTERED INDEX IX_Repuestos_Codigo ON dbo.Repuestos (Codigo ASC) WHERE Codigo IS NOT NULL
GO
CREATE NONCLUSTERED INDEX IX_Repuestos_Descripcion ON dbo.Repuestos (Descripcion ASC)
GO

-----------------------------------------------------------------------------
-- 4) Servicios (cabecera)
-----------------------------------------------------------------------------
CREATE TABLE dbo.Servicios (
    IdServicio        INT IDENTITY(1,1) NOT NULL,
    IdVehiculo        INT           NOT NULL,
    NroCuentaProv     INT           NULL,
    ProveedorNombre   VARCHAR(120)  NULL,
    FechaServicio     DATE          NOT NULL,
    KmServicio        INT           NOT NULL CONSTRAINT DF_Servicios_KmServicio DEFAULT ((0)),
    IdTipoServicio    INT           NULL,
    Estado            VARCHAR(10)   NOT NULL CONSTRAINT DF_Servicios_Estado DEFAULT ('Abierto'),
    NroFactura        INT           NULL,
    NroComprobante    INT           NULL,
    PuntoDeVenta      SMALLINT      NULL,
    FechaFactura      DATE          NULL,
    Observaciones     VARCHAR(500)  NULL,
    SubTotalItems     DECIMAL(14,2) NOT NULL CONSTRAINT DF_Servicios_SubTotalItems DEFAULT ((0)),
    OtrosGastos       DECIMAL(14,2) NOT NULL CONSTRAINT DF_Servicios_OtrosGastos   DEFAULT ((0)),
    TotalServicio     DECIMAL(14,2) NOT NULL CONSTRAINT DF_Servicios_TotalServicio DEFAULT ((0)),
    IdUsuarioCrea     VARCHAR(20)   NULL,
    FechaHoraCrea     DATETIME      NULL CONSTRAINT DF_Servicios_FechaHoraCrea DEFAULT (GETDATE()),
    IdUsuarioMod      VARCHAR(20)   NULL,
    FechaHoraMod      DATETIME      NULL,
    CONSTRAINT PK_Servicios PRIMARY KEY CLUSTERED (IdServicio ASC),
    CONSTRAINT FK_Servicios_Vehiculos    FOREIGN KEY (IdVehiculo)     REFERENCES dbo.Vehiculos    (IdVehiculo),
    CONSTRAINT FK_Servicios_TipoServicio FOREIGN KEY (IdTipoServicio) REFERENCES dbo.TipoServicio (IdTipoServicio),
    CONSTRAINT CK_Servicios_Estado CHECK (Estado IN ('Abierto','Cerrado'))
)
GO
CREATE NONCLUSTERED INDEX IX_Servicios_VehiculoFecha ON dbo.Servicios (IdVehiculo ASC, FechaServicio DESC)
GO
CREATE NONCLUSTERED INDEX IX_Servicios_Proveedor    ON dbo.Servicios (NroCuentaProv ASC)
GO
CREATE NONCLUSTERED INDEX IX_Servicios_Factura      ON dbo.Servicios (NroFactura ASC, PuntoDeVenta ASC)
GO

-----------------------------------------------------------------------------
-- 5) Items del Servicio (maestro-detalle)
-----------------------------------------------------------------------------
CREATE TABLE dbo.ServiciosItems (
    IdItem            INT IDENTITY(1,1) NOT NULL,
    IdServicio        INT           NOT NULL,
    NroRenglon        SMALLINT      NOT NULL CONSTRAINT DF_ServiciosItems_NroRenglon DEFAULT ((1)),
    TipoItem          CHAR(1)       NOT NULL,
    IdTipoServicio    INT           NULL,
    IdRepuesto        INT           NULL,
    DescripcionItem   VARCHAR(255)  NULL,
    Cantidad          DECIMAL(10,2) NOT NULL CONSTRAINT DF_ServiciosItems_Cantidad DEFAULT ((1)),
    PrecioUnitario    DECIMAL(14,2) NOT NULL CONSTRAINT DF_ServiciosItems_PrecioUnitario DEFAULT ((0)),
    SubtotalItem      DECIMAL(14,2) NOT NULL CONSTRAINT DF_ServiciosItems_SubtotalItem   DEFAULT ((0)),
    GarantiaKm        INT           NULL,
    GarantiaMeses     SMALLINT      NULL,
    CONSTRAINT PK_ServiciosItems PRIMARY KEY CLUSTERED (IdItem ASC),
    CONSTRAINT FK_ServiciosItems_Servicio     FOREIGN KEY (IdServicio)     REFERENCES dbo.Servicios    (IdServicio) ON DELETE CASCADE,
    CONSTRAINT FK_ServiciosItems_TipoServicio FOREIGN KEY (IdTipoServicio) REFERENCES dbo.TipoServicio (IdTipoServicio),
    CONSTRAINT FK_ServiciosItems_Repuesto     FOREIGN KEY (IdRepuesto)     REFERENCES dbo.Repuestos    (IdRepuesto),
    CONSTRAINT CK_ServiciosItems_TipoItem CHECK (TipoItem IN ('S','R'))
)
GO
CREATE NONCLUSTERED INDEX IX_ServiciosItems_Servicio ON dbo.ServiciosItems (IdServicio ASC, NroRenglon ASC)
GO

-----------------------------------------------------------------------------
-- Datos de ejemplo (opcional, comentar si no se quiere)
-----------------------------------------------------------------------------
-- INSERT dbo.TipoServicio (Descripcion, CostoRef, KmRecomendado, DiasRecomendados) VALUES
--   ('Cambio de aceite y filtro',  NULL, 10000, 180),
--   ('Cambio de frenos (pastillas)', NULL, 30000, NULL),
--   ('Cambio de correa distribucion', NULL, 90000, NULL),
--   ('Alineacion y balanceo', NULL, 10000, NULL),
--   ('Cambio de neumatico', NULL, NULL, NULL)
-- GO

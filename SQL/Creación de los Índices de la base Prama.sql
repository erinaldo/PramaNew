-- Activamos la base de datos para poder crear la tablas
USE PramaSQL
GO
--------------------------------------------------------
--            CREACIÓN DE TODOS LOS ÍNDICES           --
--------------------------------------------------------

-- TABLA LOCALIDAD
CREATE INDEX IDX_PorLocalidad on Localidades (Localidad)
CREATE INDEX IDX_PorCP on Localidades (CP)
CREATE INDEX IDX_PorIdProvincia on Localidades (IdProvincia)
CREATE INDEX IDX_PorActivo on Localidades (Activo)

-- TABLA USUARIOS
CREATE INDEX IDX_PorUsuario on Usuarios (Usuario)
CREATE INDEX IDX_PorActivo on Usuarios (Activo)

-- TABLA COMPROBANTESCOMPRAS --
CREATE INDEX IDX_PorTipoComprobante on ComprobantesCompras (IdTipoComprobanteCompra)
CREATE INDEX IDX_PorIdProveedor on ComprobantesCompras (IdProveedor)
CREATE INDEX IDX_PorIdPuntoVenta on ComprobantesCompras (IdPuntoVenta)
CREATE INDEX IDX_PorIdCondicionCompra on ComprobantesCompras (IdCondicionCompra)
USE [PramaSQL]
GO

/****** Object:  StoredProcedure [dbo].[CargarArticulos]    Script Date: 09/25/2016 17:56:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- Procedimiento para buscar artículos
CREATE PROCEDURE [dbo].[CargarArticulos] (@id_tipoarticulo INT)
AS
BEGIN
SELECT *, sa.IdRubroArticulo FROM Articulos a, SubrubrosArticulos sa
WHERE a.IdSubrubroArticulo = sa.IdSubrubroArticulo AND
a.IdTipoArticulo = @id_tipoarticulo;
END;

GO
-- Procedimiento para cargar las localidades
CREATE PROCEDURE CargarLocalidades (@id_Provincia INT)
AS
BEGIN
SELECT * FROM Localidades
WHERE IdProvincia = @id_Provincia;
END;

-- Procedimiento para cargar los códigos de los artículos de los proveedores
CREATE PROCEDURE CodigoArticuloProveedor (@IdProveedor INT, @IdArticulo INT)
AS
BEGIN
SELECT CodigoProveedor FROM ArticulosProveedoresCodigos
WHERE IdProveedor = @IdProveedor
AND IdArticulo = @IdArticulo;
END;
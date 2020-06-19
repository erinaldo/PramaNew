USE [PramaSQL]
GO

/****** Object:  View [dbo].[Vista_StockFabricacion]    Script Date: 02/07/2017 19:33:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Vista_StockFabricacion]
AS
SELECT     dbo.StockFabricacion.IdFabricacion, dbo.StockFabricacion.IdUsuario, dbo.Productos.IdProducto, dbo.Productos.IdArticulo, dbo.StockFabricacion.Fecha, dbo.Articulos.CodigoArticulo, 
                      dbo.Articulos.Articulo, dbo.StockFabricacion.Cantidad, dbo.Usuarios.Usuario, dbo.StockFabricacion.Activo, dbo.StockFabricacion.sFecha
FROM         dbo.StockFabricacion INNER JOIN
                      dbo.Productos ON dbo.StockFabricacion.IdProducto = dbo.Productos.IdProducto INNER JOIN
                      dbo.Articulos ON dbo.Productos.IdArticulo = dbo.Articulos.IdArticulo INNER JOIN
                      dbo.Usuarios ON dbo.StockFabricacion.IdUsuario = dbo.Usuarios.IdUsuario


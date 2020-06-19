USE [PramaSQL]
GO

/****** Object:  View [dbo].[Vista_StockFabricacion]    Script Date: 02/01/2017 22:12:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[Vista_StockFabricacion]
AS
SELECT     dbo.StockFabricacion.IdFabricacion, dbo.StockFabricacion.IdUsuario, dbo.Productos.IdProducto, dbo.Productos.IdArticulo, dbo.StockFabricacion.Fecha, dbo.Articulos.CodigoArticulo, 
                      dbo.Articulos.Articulo, dbo.StockFabricacion.Cantidad, dbo.Usuarios.Usuario
FROM         dbo.StockFabricacion INNER JOIN
                      dbo.Productos ON dbo.StockFabricacion.IdProducto = dbo.Productos.IdProducto INNER JOIN
                      dbo.Articulos ON dbo.Productos.IdArticulo = dbo.Articulos.IdArticulo INNER JOIN
                      dbo.Usuarios ON dbo.StockFabricacion.IdUsuario = dbo.Usuarios.IdUsuario

GO


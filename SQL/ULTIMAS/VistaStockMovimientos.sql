USE [PramaSQL]
GO

/****** Object:  View [dbo].[Vista_StockMovimientos]    Script Date: 02/07/2017 19:34:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Vista_StockMovimientos]
AS
SELECT     SM.IdStockMovimiento, SM.Fecha, SM.IdArticulo, VAIP.Tabla, a.CodigoArticulo, a.Articulo, ST.Entrada, SM.IdStockMotivo, ST.StockMotivo, SM.Cantidad, SM.IdUsuario, U.Usuario, SM.sFecha
FROM         dbo.Usuarios AS U INNER JOIN
                      dbo.StockMovimientos AS SM INNER JOIN
                      dbo.Articulos AS a ON SM.IdArticulo = a.IdArticulo INNER JOIN
                      dbo.StockMotivos AS ST ON SM.IdStockMotivo = ST.IdStockMotivo INNER JOIN
                      dbo.Articulos_Insumos_Productos AS VAIP ON SM.IdArticulo = VAIP.IdArticulo ON U.IdUsuario = SM.IdUsuario
WHERE     (SM.Activo = 1)
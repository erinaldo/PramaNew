USE [PramaSQL]
GO

/****** Object:  View [dbo].[BuscarArticulos_Productos]    Script Date: 03/15/2017 21:14:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[BuscarArticulos_Productos]
AS
SELECT     a.IdArticulo, a.IdSubrubroArticulo, a.IdUnidadMedida, a.CodigoArticulo, a.Articulo, a.Unidades, a.Precio, a.PrecioAnterior, a.UltimoCostoCompra, a.UltimoProveedor, 
                      a.UltimaCompra, a.LlevaStock, a.Facturable, a.Stock, a.StockMinimo, a.StockMaximo, a.StockPuntoPedido, a.PorcentajeIva, a.Activo, sa.IdRubroArticulo, p.IdProducto, 
                      p.IdAreaProduccion, p.IdCoeficienteArticulo, p.CostoAcumulado, p.CostoGastos, p.CostoInsumos, p.Tanda, u.UnidadMedida, a.IncListaRes, a.IncListaPre
FROM         dbo.Articulos AS a INNER JOIN
                      dbo.SubrubrosArticulos AS sa ON a.IdSubrubroArticulo = sa.IdSubrubroArticulo INNER JOIN
                      dbo.Productos AS p ON a.IdArticulo = p.IdArticulo INNER JOIN
                      dbo.UnidadesMedida AS u ON a.IdUnidadMedida = u.IdUnidadMedida

GO


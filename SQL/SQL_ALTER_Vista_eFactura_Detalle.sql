USE [PramaSQL]
GO

/****** Object:  View [dbo].[Vista_eFactura_Detalle]    Script Date: 03/06/2017 21:40:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[Vista_eFactura_Detalle]
AS
SELECT     dbo.eFacturaDetalle.IdFacturaDetalle, dbo.eFacturaDetalle.IdFactura, dbo.eFacturaDetalle.Cantidad, dbo.eFacturaDetalle.Alicuota, dbo.eFacturaDetalle.IdArticulo, 
                      dbo.Articulos.CodigoArticulo, dbo.Articulos.Articulo, dbo.eFacturaDetalle.IdProducto, dbo.Articulos.IdUnidadMedida, dbo.UnidadesMedida.AbreviaturaUnidad, 
                      dbo.eFacturaDetalle.Precio, dbo.eFacturaDetalle.Dto, dbo.eFacturaDetalle.SubTotalDto, dbo.eFacturaDetalle.IVA, dbo.eFacturaDetalle.Subtotal
FROM         dbo.eFacturaDetalle INNER JOIN
                      dbo.Articulos ON dbo.eFacturaDetalle.IdArticulo = dbo.Articulos.IdArticulo INNER JOIN
                      dbo.UnidadesMedida ON dbo.Articulos.IdUnidadMedida = dbo.UnidadesMedida.IdUnidadMedida

GO


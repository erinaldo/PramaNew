USE [PramaSQL]
GO

/****** Object:  View [dbo].[Vista_eFactura_Detalle]    Script Date: 02/09/2017 10:42:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[Vista_eFactura_Detalle]
AS
SELECT     dbo.eFacturaDetalle.IdFacturaDetalle, dbo.eFacturaDetalle.IdFactura, dbo.eFacturaDetalle.Cantidad, dbo.eFacturaDetalle.Alicuota, dbo.eFacturaDetalle.IdArticulo, 
                      dbo.Articulos.CodigoArticulo, dbo.Articulos.Articulo, dbo.eFacturaDetalle.IdProducto, dbo.eFacturaDetalle.Precio, dbo.eFacturaDetalle.IVA, 
                      dbo.eFacturaDetalle.Subtotal
FROM         dbo.eFacturaDetalle INNER JOIN
                      dbo.Articulos ON dbo.eFacturaDetalle.IdArticulo = dbo.Articulos.IdArticulo

GO


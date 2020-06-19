USE [PramaSQL]
GO

/****** Object:  View [dbo].[Vista_Detalle_Pedido_ABM]    Script Date: 03/26/2017 18:49:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[Vista_Detalle_Pedido_ABM]
AS
SELECT     dbo.DetallePedidos.IdArticulo, dbo.DetallePedidos.Codigo_Articulo AS CodigoArticulo, dbo.DetallePedidos.Cantidad, dbo.DetallePedidos.Descripcion AS Articulo, 
                      dbo.Productos.IdProducto AS IdPropio, 'PRODUCTOS' AS Tabla, dbo.DetallePedidos.PrecioUnitario AS Precio, dbo.Articulos.PorcentajeIva AS IVA, dbo.Productos.IdCoeficienteArticulo AS Coeficiente, 
                      dbo.CoeficientesArticulos.CoeficientePublico AS Pub, dbo.CoeficientesArticulos.CoeficienteDistribuidor AS Dist, dbo.CoeficientesArticulos.CoeficienteRevendedor AS Rev, 
                      dbo.DetallePedidos.IdDetallePedido, dbo.DetallePedidos.IdPedido, dbo.DetallePedidos.Excel
FROM         dbo.DetallePedidos INNER JOIN
                      dbo.Productos ON dbo.DetallePedidos.IdArticulo = dbo.Productos.IdArticulo INNER JOIN
                      dbo.CoeficientesArticulos ON dbo.Productos.IdCoeficienteArticulo = dbo.CoeficientesArticulos.IdCoeficienteArticulo INNER JOIN
                      dbo.Articulos ON dbo.DetallePedidos.IdArticulo = dbo.Articulos.IdArticulo

GO


USE [PramaSQL]
GO

/****** Object:  View [dbo].[Vista_Detalle_Presu_ABM]    Script Date: 03/26/2017 18:49:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[Vista_Detalle_Presu_ABM]
AS
SELECT     dbo.DetallePresupuestos.IdArticulo, dbo.DetallePresupuestos.Codigo_Articulo AS CodigoArticulo, dbo.DetallePresupuestos.Cantidad, dbo.DetallePresupuestos.Descripcion AS Articulo, 
                      dbo.Productos.IdProducto AS IdPropio, 'PRODUCTOS' AS Tabla, dbo.DetallePresupuestos.PrecioUnitario AS Precio, dbo.Articulos.PorcentajeIva AS IVA, 
                      dbo.Productos.IdCoeficienteArticulo AS Coeficiente, dbo.CoeficientesArticulos.CoeficientePublico AS Pub, dbo.CoeficientesArticulos.CoeficienteDistribuidor AS Dist, 
                      dbo.CoeficientesArticulos.CoeficienteRevendedor AS Rev, dbo.DetallePresupuestos.IdDetPresupuesto, dbo.DetallePresupuestos.IdPresupuesto, dbo.UnidadesMedida.AbreviaturaUnidad, 
                      dbo.UnidadesMedida.UnidadMedida, dbo.Presupuestos.Dto, dbo.DetallePresupuestos.Excel
FROM         dbo.DetallePresupuestos INNER JOIN
                      dbo.Productos ON dbo.DetallePresupuestos.IdArticulo = dbo.Productos.IdArticulo INNER JOIN
                      dbo.CoeficientesArticulos ON dbo.Productos.IdCoeficienteArticulo = dbo.CoeficientesArticulos.IdCoeficienteArticulo INNER JOIN
                      dbo.Articulos ON dbo.DetallePresupuestos.IdArticulo = dbo.Articulos.IdArticulo INNER JOIN
                      dbo.UnidadesMedida ON dbo.Articulos.IdUnidadMedida = dbo.UnidadesMedida.IdUnidadMedida INNER JOIN
                      dbo.Presupuestos ON dbo.DetallePresupuestos.IdPresupuesto = dbo.Presupuestos.IdPresupuesto

GO


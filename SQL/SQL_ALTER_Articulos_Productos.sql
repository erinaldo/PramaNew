USE [PramaSQL]
GO

/****** Object:  View [dbo].[Articulos_Productos]    Script Date: 02/19/2017 19:12:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[Articulos_Productos]
AS
SELECT     a.IdArticulo, a.CodigoArticulo, a.Articulo, P.IdProducto AS IdPropio, 'PRODUCTOS' AS Tabla, a.Unidades, U.AbreviaturaUnidad, a.Precio, a.PorcentajeIva AS IVA, 
                      P.IdCoeficienteArticulo AS Coeficiente, CF.CoeficienteArticulo, CF.CoeficientePublico AS Pub, CF.CoeficienteDistribuidor AS Dist, 
                      CF.CoeficienteRevendedor AS Rev
FROM         dbo.Productos AS P INNER JOIN
                      dbo.Articulos AS a ON P.IdArticulo = a.IdArticulo INNER JOIN
                      dbo.UnidadesMedida AS U ON a.IdUnidadMedida = U.IdUnidadMedida INNER JOIN
                      dbo.CoeficientesArticulos AS CF ON P.IdCoeficienteArticulo = CF.IdCoeficienteArticulo
WHERE     (a.Activo = 1)

GO


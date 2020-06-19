USE [PramaSQL]
GO

/****** Object:  StoredProcedure [dbo].[CargarDetComposicionById]    Script Date: 11/13/2016 21:39:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[CargarDetComposicionById] (@Id_Producto INT)
AS
select a.CodigoArticulo as Codigo, a.Articulo as Descripcion, pi.Cantidad as Cantidad,
um.UnidadMedida as Unidad, pi.Costo as CU, (pi.Cantidad*pi.Costo)as Costo, pi.IdInsumo
from articulos a, ProductosInsumos pi, Insumos i, UnidadesMedida um where i.IdInsumo = pi.IdInsumo and
a.IdUnidadMedida = um.IdUnidadMedida and a.IdArticulo = i.IdArticulo and pi.IdProducto = @Id_Producto



GO


USE [PramaSQL]
GO

/****** Object:  StoredProcedure [dbo].[CargarDetGastosComp]    Script Date: 11/13/2016 22:03:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[CargarDetGastosComp] (@Id_Producto INT)
AS
select g.Codigo, g.GastoFijo as Descripcion, pg.Cantidad as Cantidad,
um.UnidadMedida as Unidad, pg.Costo as CU, pg.IdGastoFijo 
from ProductosGastosFijos pg, GastosFijos g, UnidadesMedida um where g.IdGastoFijo = pg.IdGastoFijo and
g.IdUnidadMedida = um.IdUnidadMedida and pg.IdProducto = @Id_Producto

GO


USE [PramaSQL]
GO

/****** Object:  StoredProcedure [dbo].[CargarArticulos]    Script Date: 02/09/2017 10:43:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[CargarArticulos] (@id_tipoarticulo INT)
AS
BEGIN
  IF (@id_tipoarticulo=1)	
	BEGIN 
		SELECT a.*, sa.IdRubroArticulo, i.*, u.UnidadMedida FROM Articulos a,Insumos i, SubrubrosArticulos sa, UnidadesMedida u
		WHERE a.IdSubrubroArticulo = sa.IdSubrubroArticulo AND i.IdArticulo = a.IdArticulo AND a.IdUnidadMedida = u.IdUnidadMedida
		AND a.Activo = 1;
	END
  IF (@id_tipoarticulo=2)
	BEGIN 	
		SELECT a.*, sa.IdRubroArticulo, p.*, u.UnidadMedida FROM Articulos a,Productos p, SubrubrosArticulos sa, UnidadesMedida u
		WHERE a.IdSubrubroArticulo = sa.IdSubrubroArticulo AND p.IdArticulo = a.IdArticulo AND a.IdUnidadMedida = u.IdUnidadMedida
		AND a.Activo = 1;
	END
END;


GO


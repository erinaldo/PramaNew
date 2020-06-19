USE [PramaSQL]
GO

/****** Object:  StoredProcedure [dbo].[CargarArticulos]    Script Date: 10/03/2016 12:04:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CargarArticulos] (@id_tipoarticulo INT)
AS
BEGIN
SELECT *, sa.IdRubroArticulo FROM Articulos a, SubrubrosArticulos sa
WHERE a.IdSubrubroArticulo = sa.IdSubrubroArticulo AND
a.IdTipoArticulo = @id_tipoarticulo;
END;

GO


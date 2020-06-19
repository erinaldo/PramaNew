USE [PramaSQL]
GO

/****** Object:  StoredProcedure [dbo].[CargarProductoByIDArticulo]    Script Date: 11/13/2016 21:38:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[CargarProductoByIDArticulo] (@Id_Articulo INT)
AS
select p.* from Productos p where IdArticulo = @Id_Articulo
GO


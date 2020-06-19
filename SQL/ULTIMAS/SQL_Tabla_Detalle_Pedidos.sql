USE [PramaSQL]
GO

/****** Object:  Table [dbo].[DetallePedidos]    Script Date: 02/09/2017 10:38:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DetallePedidos]') AND type in (N'U'))
DROP TABLE [dbo].[DetallePedidos]
GO

USE [PramaSQL]
GO

/****** Object:  Table [dbo].[DetallePedidos]    Script Date: 02/09/2017 10:38:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DetallePedidos](
	[IdDetallePedido] [int] NULL,
	[IdPedido] [int] NULL,
	[IdArticulo] [int] NULL,
	[Codigo_Articulo] [varchar](10) NULL,
	[Cantidad] [int] NULL,
	[Descripcion] [varchar](255) NULL,
	[PrecioUnitario] [decimal](13, 2) NULL,
	[Activo] [bit] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


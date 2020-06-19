USE [PramaSQL]
GO

/****** Object:  Table [dbo].[DetallePedidos]    Script Date: 03/26/2017 18:47:54 ******/
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
	[PrecioUnitario] [decimal](13, 5) NULL,
	[Activo] [bit] NULL,
	[Excel] [int] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[DetallePedidos] ADD  CONSTRAINT [DF_DetallePedidos_Activo]  DEFAULT ((0)) FOR [Activo]
GO


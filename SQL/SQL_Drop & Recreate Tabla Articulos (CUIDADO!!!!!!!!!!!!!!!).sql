USE [PramaSQL]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ArticulosSubrubrosArticulos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Articulos]'))
ALTER TABLE [dbo].[Articulos] DROP CONSTRAINT [FK_ArticulosSubrubrosArticulos]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ArticulosUnidadesMedida]') AND parent_object_id = OBJECT_ID(N'[dbo].[Articulos]'))
ALTER TABLE [dbo].[Articulos] DROP CONSTRAINT [FK_ArticulosUnidadesMedida]
GO

USE [PramaSQL]
GO

/****** Object:  Table [dbo].[Articulos]    Script Date: 03/06/2017 23:03:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Articulos]') AND type in (N'U'))
DROP TABLE [dbo].[Articulos]
GO

USE [PramaSQL]
GO

/****** Object:  Table [dbo].[Articulos]    Script Date: 03/06/2017 23:03:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Articulos](
	[IdArticulo] [int] IDENTITY(1,1) NOT NULL,
	[IdSubrubroArticulo] [int] NOT NULL,
	[IdUnidadMedida] [int] NOT NULL,
	[CodigoArticulo] [varchar](10) NOT NULL,
	[Articulo] [varchar](70) NOT NULL,
	[Unidades] [decimal](11, 2) NOT NULL,
	[Precio] [decimal](11, 5) NULL,
	[PrecioAnterior] [decimal](11, 5) NULL,
	[UltimoCostoCompra] [decimal](11, 5) NULL,
	[UltimoProveedor] [varchar](70) NULL,
	[UltimaCompra] [varchar](10) NULL,
	[LlevaStock] [bit] NULL,
	[Facturable] [bit] NULL,
	[Stock] [decimal](11, 2) NULL,
	[StockMinimo] [decimal](11, 2) NULL,
	[StockMaximo] [decimal](11, 2) NULL,
	[StockPuntoPedido] [decimal](11, 2) NULL,
	[PorcentajeIva] [decimal](5, 2) NOT NULL,
	[Activo] [bit] NOT NULL,
	[IncListaPre] [int] NULL,
	[IncListaRes] [int] NULL,
 CONSTRAINT [PK_Articulos] PRIMARY KEY CLUSTERED 
(
	[IdArticulo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Articulos]  WITH CHECK ADD  CONSTRAINT [FK_ArticulosSubrubrosArticulos] FOREIGN KEY([IdSubrubroArticulo])
REFERENCES [dbo].[SubrubrosArticulos] ([IdSubrubroArticulo])
GO

ALTER TABLE [dbo].[Articulos] CHECK CONSTRAINT [FK_ArticulosSubrubrosArticulos]
GO

ALTER TABLE [dbo].[Articulos]  WITH CHECK ADD  CONSTRAINT [FK_ArticulosUnidadesMedida] FOREIGN KEY([IdUnidadMedida])
REFERENCES [dbo].[UnidadesMedida] ([IdUnidadMedida])
GO

ALTER TABLE [dbo].[Articulos] CHECK CONSTRAINT [FK_ArticulosUnidadesMedida]
GO


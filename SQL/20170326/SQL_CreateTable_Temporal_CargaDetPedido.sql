USE [PramaSQL]
GO

/****** Object:  Table [dbo].[Temporal_CargaDetPedido]    Script Date: 03/26/2017 18:50:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Temporal_CargaDetPedido](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Item] [int] NULL,
	[IdArticulo] [int] NULL,
	[CodigoAnt] [int] NULL,
	[CodigoArticulo] [varchar](10) NULL,
	[Cantidad] [int] NULL,
	[Articulo] [varchar](70) NULL,
	[Precio] [decimal](13, 5) NULL,
	[PrecioCoef] [decimal](13, 5) NULL,
	[SubTotal] [decimal](18, 2) NULL,
	[IdCoeficiente] [int] NULL,
	[Pub] [decimal](5, 2) NULL,
	[Dist] [decimal](5, 2) NULL,
	[Rev] [decimal](5, 2) NULL,
	[Excel] [int] NULL,
 CONSTRAINT [PK_Temporal_CargaDetPedido] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


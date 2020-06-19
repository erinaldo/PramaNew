USE [PramaSQL]
GO

/****** Object:  Table [dbo].[DetallePresupuestos]    Script Date: 03/26/2017 18:48:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DetallePresupuestos](
	[IdDetPresupuesto] [int] NULL,
	[IdPresupuesto] [int] NULL,
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

ALTER TABLE [dbo].[DetallePresupuestos] ADD  CONSTRAINT [DF_DetallePresupuestos_Activo]  DEFAULT ((0)) FOR [Activo]
GO


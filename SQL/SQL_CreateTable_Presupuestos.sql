USE [PramaSQL]
GO

/****** Object:  Table [dbo].[Presupuestos]    Script Date: 03/26/2017 18:48:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Presupuestos](
	[IdPresupuesto] [int] NULL,
	[IdCliente] [int] NULL,
	[IdFormaPago] [int] NULL,
	[Punto] [int] NULL,
	[Nro] [int] NULL,
	[PuntoNro] [varchar](13) NULL,
	[IdTransporte] [int] NULL,
	[Fecha] [date] NULL,
	[Comentario] [varchar](255) NULL,
	[Dto] [decimal](6, 2) NULL,
	[Flete] [decimal](13, 2) NULL,
	[Activo] [bit] NULL,
	[Facturado] [int] NULL,
	[Excel] [int] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Presupuestos] ADD  CONSTRAINT [DF_Presupuestos_Facturado]  DEFAULT ((0)) FOR [Facturado]
GO

ALTER TABLE [dbo].[Presupuestos] ADD  CONSTRAINT [DF_Presupuestos_Excel]  DEFAULT ((0)) FOR [Excel]
GO


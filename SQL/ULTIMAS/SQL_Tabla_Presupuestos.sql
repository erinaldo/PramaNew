USE [PramaSQL]
GO

/****** Object:  Table [dbo].[Presupuestos]    Script Date: 02/09/2017 10:39:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Presupuestos]') AND type in (N'U'))
DROP TABLE [dbo].[Presupuestos]
GO

USE [PramaSQL]
GO

/****** Object:  Table [dbo].[Presupuestos]    Script Date: 02/09/2017 10:39:37 ******/
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
	[Activo] [bit] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


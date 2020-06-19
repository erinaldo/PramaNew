USE [PramaSQL]
GO

/****** Object:  Table [dbo].[Pedidos]    Script Date: 02/09/2017 10:39:56 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pedidos]') AND type in (N'U'))
DROP TABLE [dbo].[Pedidos]
GO

USE [PramaSQL]
GO

/****** Object:  Table [dbo].[Pedidos]    Script Date: 02/09/2017 10:39:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Pedidos](
	[IdPedido] [int] NULL,
	[IdCliente] [int] NULL,
	[IdFormaPago] [int] NULL,
	[Punto] [int] NULL,
	[Nro] [int] NULL,
	[PuntoNro] [varchar](13) NULL,
	[Entrada] [varchar](70) NULL,
	[Recibio] [varchar](70) NULL,
	[IdTransporte] [int] NULL,
	[Fecha] [date] NULL,
	[Comentario] [varchar](255) NULL,
	[Finalizado] [date] NULL,
	[Dto] [decimal](6, 2) NULL,
	[Flete] [decimal](13, 2) NULL,
	[Cerrado] [int] NULL,
	[Activo] [bit] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


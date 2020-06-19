USE [PramaSQL]
GO

/****** Object:  Table [dbo].[Pedidos]    Script Date: 02/01/2017 22:13:22 ******/
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


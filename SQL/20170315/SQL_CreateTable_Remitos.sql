USE [PramaSQL]
GO

/****** Object:  Table [dbo].[Remitos]    Script Date: 03/15/2017 21:14:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Remitos](
	[IdRemito] [int] NULL,
	[Punto] [int] NULL,
	[Nro] [int] NULL,
	[NroRemito] [varchar](20) NULL,
	[IdCliente] [int] NULL,
	[CUIT] [varchar](13) NULL,
	[IdTransporte] [int] NULL,
	[Comprobante] [varchar](13) NULL,
	[IdFormaPago] [int] NULL,
	[IdFormaPagoMerc] [int] NULL,
	[Bultos] [int] NULL,
	[Seguro] [numeric](12, 2) NULL,
	[Anulado] [int] NULL,
	[Activo] [bit] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


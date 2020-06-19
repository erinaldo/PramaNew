USE [PramaSQL]
GO

/****** Object:  Table [dbo].[eFactura]    Script Date: 05/03/2017 17:46:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[eFactura]') AND type in (N'U'))
DROP TABLE [dbo].[eFactura]
GO

USE [PramaSQL]
GO

/****** Object:  Table [dbo].[eFactura]    Script Date: 05/03/2017 17:46:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[eFactura](
	[IdFactura] [int] NULL,
	[Fecha] [date] NULL,
	[IdTipoComprobante] [int] NULL,
	[Punto] [int] NULL,
	[Nro] [int] NULL,
	[PuntoNro] [varchar](20) NULL,
	[IdFormaPago] [int] NULL,
	[FechaVencPago] [date] NULL,
	[IdCliente] [int] NULL,
	[CUIT] [varchar](13) NULL,
	[IncluyeProd] [int] NULL,
	[IncluyeServ] [int] NULL,
	[CAE] [varchar](20) NULL,
	[VecCAE] [date] NULL,
	[Resultado] [int] NULL,
	[OtrosTributos] [decimal](13, 2) NULL,
	[IdTransporte] [int] NULL,
	[Neto] [decimal](13, 2) NULL,
	[Dto] [decimal](6, 2) NULL,
	[Flete] [decimal](13, 2) NULL,
	[Subtotal] [decimal](13, 2) NULL,
	[Exento] [decimal](13, 2) NULL,
	[IVA21] [decimal](13, 2) NULL,
	[IVA10] [decimal](13, 2) NULL,
	[Total] [decimal](13, 2) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


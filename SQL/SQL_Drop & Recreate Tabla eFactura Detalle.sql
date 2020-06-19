USE [PramaSQL]
GO

/****** Object:  Table [dbo].[eFacturaDetalle]    Script Date: 03/06/2017 21:41:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[eFacturaDetalle]') AND type in (N'U'))
DROP TABLE [dbo].[eFacturaDetalle]
GO

USE [PramaSQL]
GO

/****** Object:  Table [dbo].[eFacturaDetalle]    Script Date: 03/06/2017 21:41:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[eFacturaDetalle](
	[IdFacturaDetalle] [int] NULL,
	[IdFactura] [int] NULL,
	[Cantidad] [decimal](12, 2) NULL,
	[Alicuota] [decimal](6, 2) NULL,
	[IdArticulo] [int] NULL,
	[IdProducto] [int] NULL,
	[Precio] [decimal](12, 2) NULL,
	[Dto] [decimal](6, 2) NULL,
	[SubTotalDto] [decimal](12, 2) NULL,
	[IVA] [decimal](12, 2) NULL,
	[Subtotal] [decimal](12, 2) NULL
) ON [PRIMARY]

GO


USE [PramaSQL]
GO

/****** Object:  Table [dbo].[StockMovimientos]    Script Date: 02/06/2017 17:52:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[StockMovimientos](
	[IdStockMovimiento] [int] IDENTITY(1,1) NOT NULL,
	[IdArticulo] [int] NOT NULL,
	[IdStockMotivo] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
	[Cantidad] [decimal](12, 5) NOT NULL,
	[IdUsuario] [int] NOT NULL,
 CONSTRAINT [PK_StockMovimientos] PRIMARY KEY CLUSTERED 
(
	[IdStockMovimiento] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[StockMovimientos]  WITH CHECK ADD  CONSTRAINT [FK_StockMovimientos_Articulos] FOREIGN KEY([IdArticulo])
REFERENCES [dbo].[Articulos] ([IdArticulo])
GO

ALTER TABLE [dbo].[StockMovimientos] CHECK CONSTRAINT [FK_StockMovimientos_Articulos]
GO

ALTER TABLE [dbo].[StockMovimientos]  WITH CHECK ADD  CONSTRAINT [FK_StockMovimientos_StockMotivos] FOREIGN KEY([IdStockMotivo])
REFERENCES [dbo].[StockMotivos] ([IdStockMotivo])
GO

ALTER TABLE [dbo].[StockMovimientos] CHECK CONSTRAINT [FK_StockMovimientos_StockMotivos]
GO



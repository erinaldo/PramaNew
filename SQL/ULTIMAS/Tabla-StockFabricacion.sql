USE [PramaSQL]
GO

/****** Object:  Table [dbo].[StockFabricacion]    Script Date: 02/01/2017 12:25:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[StockFabricacion](
	[IdFabricacion] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [date] NOT NULL,
	[IdProducto] [int] NOT NULL,
	[Cantidad] [decimal](11, 5) NOT NULL,
	[IdUsuario] [int] NOT NULL,
 CONSTRAINT [PK_StockFabricacion] PRIMARY KEY CLUSTERED 
(
	[IdFabricacion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[StockFabricacion]  WITH CHECK ADD  CONSTRAINT [FK_StockFabricacion_Productos] FOREIGN KEY([IdProducto])
REFERENCES [dbo].[Productos] ([IdProducto])
GO

ALTER TABLE [dbo].[StockFabricacion] CHECK CONSTRAINT [FK_StockFabricacion_Productos]
GO



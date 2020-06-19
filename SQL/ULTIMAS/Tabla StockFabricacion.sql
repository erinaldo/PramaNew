USE [PramaSQL]
GO

/****** Object:  Table [dbo].[StockFabricacion]    Script Date: 02/07/2017 19:31:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[StockFabricacion](
	[IdFabricacion] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [date] NOT NULL,
	[sFecha] [varchar](50) NULL,
	[IdProducto] [int] NOT NULL,
	[Cantidad] [decimal](11, 2) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[Activo] [bit] NULL,
 CONSTRAINT [PK_StockFabricacion] PRIMARY KEY CLUSTERED 
(
	[IdFabricacion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[StockFabricacion]  WITH CHECK ADD  CONSTRAINT [FK_StockFabricacion_Productos] FOREIGN KEY([IdProducto])
REFERENCES [dbo].[Productos] ([IdProducto])
GO

ALTER TABLE [dbo].[StockFabricacion] CHECK CONSTRAINT [FK_StockFabricacion_Productos]
GO



USE [PramaSQL]
GO

/****** Object:  Table [dbo].[StockMotivos]    Script Date: 02/06/2017 13:13:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[StockMotivos](
	[IdStockMotivo] [int] IDENTITY(1,1) NOT NULL,
	[StockMotivo] [varchar](50) NOT NULL,
	[Entrada] [bit] NULL,
	[Activo] [bit] NULL,
 CONSTRAINT [PK_StockMotivos] PRIMARY KEY CLUSTERED 
(
	[IdStockMotivo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



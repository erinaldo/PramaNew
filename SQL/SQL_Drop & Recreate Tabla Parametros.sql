USE [PramaSQL]
GO

/****** Object:  Table [dbo].[Parametros]    Script Date: 03/06/2017 18:12:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Parametros]') AND type in (N'U'))
DROP TABLE [dbo].[Parametros]
GO

USE [PramaSQL]
GO

/****** Object:  Table [dbo].[Parametros]    Script Date: 03/06/2017 18:12:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Parametros](
	[IdParametro] [int] NOT NULL,
	[RazonSocial] [varchar](100) NULL,
	[NombreFantasia] [varchar](100) NULL,
	[Direccion] [varchar](70) NULL,
	[Telefono] [varchar](20) NULL,
	[Mail] [varchar](100) NULL,
	[Web] [varchar](100) NULL,
	[CUIT] [varchar](13) NULL,
	[Localidad] [varchar](100) NULL,
	[CondicionIva] [varchar](100) NULL,
	[Iva] [decimal](4, 2) NULL,
	[Fondo] [image] NULL,
	[Icono] [image] NULL,
	[Splash] [image] NULL,
	[Impresion] [image] NULL,
	[Presupuestos] [int] NULL,
	[Facturas] [int] NULL,
	[Remitos] [int] NULL,
	[Recibos] [int] NULL,
	[Pedidos] [int] NULL,
	[CaducidadPresupuestos] [int] NULL,
	[CaducidadPedidos] [int] NULL,
	[Imprimir] [bit] NULL,
	[PtoVtaPorDefecto] [int] NULL,
	[AlmacenPorDefecto] [int] NULL,
	[ControlLoginOff] [int] NULL,
	[IconInTaskBar] [int] NULL,
	[AutoLoad] [int] NULL,
	[ModoFactura] [int] NULL,
 CONSTRAINT [PK_Parametros] PRIMARY KEY CLUSTERED 
(
	[IdParametro] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


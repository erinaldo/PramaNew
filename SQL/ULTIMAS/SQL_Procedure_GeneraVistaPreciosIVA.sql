USE [PramaSQL]
GO

/****** Object:  StoredProcedure [dbo].[GeneraVistaPreciosIVA]    Script Date: 02/01/2017 11:10:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[GeneraVistaPreciosIVA] @Valor decimal(11,5), @IVA decimal(5,2)
as
Select cf.CoeficienteArticulo, CAST(cf.CoeficientePublico * @Valor as decimal(11,5)) as 'PcioPub', 
		  CAST(cf.CoeficienteDistribuidor * @Valor as decimal(11,5)) as 'PcioDist', 
		  CAST(cf.CoeficienteRevendedor * @Valor as decimal(11,5)) as 'PcioRev',
		  CAST((((cf.CoeficientePublico * @Valor) * @IVA)/100)+(cf.CoeficientePublico * @Valor) as decimal(11,5))  as 'PubIVA',
		  CAST((((cf.CoeficienteDistribuidor * @Valor) * @IVA)/100)+(cf.CoeficienteDistribuidor * @Valor) as decimal(11,5)) as 'DistIVA',
		  CAST((((cf.CoeficienteRevendedor * @Valor) * @IVA)/100)+(cf.CoeficienteRevendedor * @Valor) as decimal(11,5)) as 'RevIVA' from CoeficientesArticulos as cf
GO


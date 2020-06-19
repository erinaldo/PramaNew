USE [PramaSQL]
GO

/****** Object:  StoredProcedure [dbo].[GeneraVistaPrecios]    Script Date: 02/01/2017 11:10:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[GeneraVistaPrecios] (@Valor decimal(11,5))
as
Select *,CAST(cf.CoeficientePublico * @Valor as decimal(11,5)) as 'PcioPub', 
		 CAST(cf.CoeficienteDistribuidor * @Valor as decimal(11,5)) as 'PcioDist', 
		 CAST(cf.CoeficienteRevendedor * @Valor as decimal(11,5)) as 'PcioRevend' from CoeficientesArticulos as cf
GO


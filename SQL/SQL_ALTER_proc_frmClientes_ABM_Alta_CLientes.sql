USE [PramaSQL]
GO

/****** Object:  StoredProcedure [dbo].[proc_frmClientesABM_Alta_CLiente]    Script Date: 02/19/2017 19:29:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER Procedure [dbo].[proc_frmClientesABM_Alta_CLiente]
@IdCondicionIva int,@RazonSocial varchar (70),@Cuit varchar (13),@Direccion varchar (70),
@Barrio varchar (70),@IdLocalidad int,@IdProvincia int,@CP varchar (20),@Telefono varchar (15),
@Celular varchar (15),@Fax varchar (15),@Mail varchar (100),@Web varchar (100),@FechaNac date,
@Observaciones varchar (500), @Alta date, @IdTipoCliente int
as
insert into Clientes (IdCondicionIva, RazonSocial,Cuit,Direccion,Barrio,IdLocalidad,IdProvincia,CP,Telefono,
Celular,Fax,Mail,Web,FechaNac,Observaciones,Alta,IdTipoCliente)
values (@IdCondicionIva, @RazonSocial, @Cuit, @Direccion, @Barrio, @IdLocalidad, @IdProvincia, @CP,
@Telefono, @Celular, @Fax, @Mail, @Web, @FechaNac, @Observaciones, @Alta, @IdTipoCliente)






GO


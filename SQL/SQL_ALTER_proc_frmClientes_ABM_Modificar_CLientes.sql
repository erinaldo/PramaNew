USE [PramaSQL]
GO

/****** Object:  StoredProcedure [dbo].[proc_frmClientesABM_Modificar_CLiente]    Script Date: 02/19/2017 19:29:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER Procedure [dbo].[proc_frmClientesABM_Modificar_CLiente]
@IdCliente int,@IdCondicionIva int,@RazonSocial varchar (70), @Cuit varchar (13),@Direccion varchar (70),
@Barrio varchar (70),@IdLocalidad int,@IdProvincia int,@CP varchar (20),@Telefono varchar (15),
@Celular varchar (15),@Fax varchar (15),@Mail varchar (100),@Web varchar (100),@FechaNac date,
@Observaciones varchar (500), @Alta date, @IdTipoCliente int
as
update Clientes 
set IdCondicionIva = @IdCondicionIva,RazonSocial=@RazonSocial,Cuit=@Cuit, Direccion=@Direccion,
Barrio = @Barrio, IdLocalidad=@IdLocalidad, IdProvincia=@IdProvincia, CP=@CP, Telefono=@Telefono,
Celular=@Celular, Fax=@Fax, Mail=@Mail, Web=@Web, FechaNac=@FechaNac, IdTipoCliente=@IdTipoCliente,
Observaciones=@Observaciones, Alta=@Alta
where IdCliente= @IdCliente




GO


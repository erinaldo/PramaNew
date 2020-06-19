USE [PramaSQL]
GO

/****** Object:  View [dbo].[Vista_Clientes]    Script Date: 02/01/2017 22:12:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[Vista_Clientes]
AS
SELECT     dbo.Clientes.IdCliente AS Codigo, dbo.Clientes.RazonSocial, dbo.Clientes.Cuit, dbo.Clientes.Direccion, dbo.Clientes.Barrio, dbo.Localidades.IdLocalidad, 
                      dbo.Localidades.Localidad, dbo.Clientes.CP, dbo.Provincias.IdProvincia, dbo.Provincias.Provincia, dbo.Clientes.Telefono, dbo.Clientes.Celular, dbo.Clientes.Fax, 
                      dbo.Clientes.Mail, dbo.Clientes.Web, dbo.Clientes.FechaNac AS Nacimiento, dbo.TiposClientes.IdTipoCliente AS IdTipo, dbo.TiposClientes.TipoCliente AS Tipo, 
                      dbo.TiposClientes.Descuento1, dbo.TiposClientes.Descuento2, dbo.Clientes.Observaciones, dbo.Clientes.Alta, dbo.Clientes.IdCondicionCompra, 
                      dbo.Clientes.IdCondicionIva, dbo.Clientes.IdTransporte, dbo.Transportes.RazonSocial AS Transporte
FROM         dbo.Clientes INNER JOIN
                      dbo.Localidades ON dbo.Clientes.IdLocalidad = dbo.Localidades.IdLocalidad INNER JOIN
                      dbo.TiposClientes ON dbo.Clientes.IdTipoCliente = dbo.TiposClientes.IdTipoCliente INNER JOIN
                      dbo.Provincias ON dbo.Localidades.IdProvincia = dbo.Provincias.IdProvincia INNER JOIN
                      dbo.Transportes ON dbo.Clientes.IdTransporte = dbo.Transportes.IdTransporte

GO


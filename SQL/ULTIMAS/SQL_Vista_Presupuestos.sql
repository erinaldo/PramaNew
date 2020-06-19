USE [PramaSQL]
GO

/****** Object:  View [dbo].[Vista_Presupuestos]    Script Date: 02/09/2017 10:41:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[Vista_Presupuestos]
AS
SELECT     dbo.Presupuestos.IdPresupuesto, dbo.Presupuestos.IdFormaPago, dbo.Presupuestos.IdCliente, dbo.Clientes.Cuit, dbo.Clientes.IdCondicionCompra, 
                      dbo.Presupuestos.Punto, dbo.Presupuestos.Nro, dbo.Presupuestos.PuntoNro, dbo.Presupuestos.IdTransporte, dbo.Transportes.RazonSocial, 
                      dbo.Presupuestos.Fecha, CONVERT(varchar(10), CONVERT(date, dbo.Presupuestos.Fecha, 106), 103) AS FechPed, dbo.Presupuestos.Dto, dbo.Presupuestos.Flete, 
                      dbo.Presupuestos.Activo, dbo.Clientes.RazonSocial AS RazonSoCli, dbo.CondicionesCompra.CondicionCompra, dbo.Clientes.IdTipoCliente AS IdTipo, 
                      dbo.Clientes.Direccion, dbo.Localidades.Localidad, dbo.Provincias.Provincia, dbo.Clientes.Telefono, dbo.Localidades.CP
FROM         dbo.Presupuestos INNER JOIN
                      dbo.Transportes ON dbo.Presupuestos.IdTransporte = dbo.Transportes.IdTransporte INNER JOIN
                      dbo.Clientes ON dbo.Presupuestos.IdCliente = dbo.Clientes.IdCliente INNER JOIN
                      dbo.CondicionesCompra ON dbo.Presupuestos.IdFormaPago = dbo.CondicionesCompra.IdCondicionCompra INNER JOIN
                      dbo.Localidades ON dbo.Clientes.IdLocalidad = dbo.Localidades.IdLocalidad INNER JOIN
                      dbo.Provincias ON dbo.Clientes.IdProvincia = dbo.Provincias.IdProvincia AND dbo.Localidades.IdProvincia = dbo.Provincias.IdProvincia

GO


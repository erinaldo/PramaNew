USE [PramaSQL]
GO

/****** Object:  View [dbo].[Vista_Pedidos]    Script Date: 03/26/2017 20:35:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[Vista_Pedidos]
AS
SELECT     dbo.Pedidos.IdPedido, dbo.Pedidos.IdFormaPago, dbo.Pedidos.IdCliente, dbo.Clientes.Cuit, dbo.Clientes.IdCondicionCompra, dbo.Pedidos.Punto, dbo.Pedidos.Nro, dbo.Pedidos.PuntoNro, 
                      dbo.Pedidos.Entrada, dbo.Pedidos.IdTransporte, dbo.Transportes.RazonSocial, dbo.Pedidos.Fecha, CONVERT(varchar(10), CONVERT(date, dbo.Pedidos.Fecha, 106), 103) AS FechPed, 
                      CONVERT(varchar(10), CONVERT(date, dbo.Pedidos.Finalizado, 106), 103) AS FechFin, dbo.Pedidos.Comentario, dbo.Pedidos.Finalizado, dbo.Pedidos.Dto, dbo.Pedidos.Flete, dbo.Pedidos.Cerrado, 
                      dbo.Pedidos.Activo, dbo.Pedidos.Recibio, dbo.Clientes.RazonSocial AS RazonSoCli, dbo.CondicionesCompra.CondicionCompra, dbo.Clientes.IdTipoCliente AS IdTipo, dbo.Clientes.Direccion, 
                      dbo.Localidades.Localidad, dbo.Provincias.Provincia, dbo.Clientes.Telefono, dbo.Localidades.CP, dbo.Pedidos.Excel
FROM         dbo.Pedidos INNER JOIN
                      dbo.Transportes ON dbo.Pedidos.IdTransporte = dbo.Transportes.IdTransporte INNER JOIN
                      dbo.Clientes ON dbo.Pedidos.IdCliente = dbo.Clientes.IdCliente INNER JOIN
                      dbo.CondicionesCompra ON dbo.Pedidos.IdFormaPago = dbo.CondicionesCompra.IdCondicionCompra INNER JOIN
                      dbo.Localidades ON dbo.Clientes.IdLocalidad = dbo.Localidades.IdLocalidad INNER JOIN
                      dbo.Provincias ON dbo.Clientes.IdProvincia = dbo.Provincias.IdProvincia AND dbo.Localidades.IdProvincia = dbo.Provincias.IdProvincia

GO


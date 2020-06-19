USE [Depurativo]
GO

/****** Object:  View [dbo].[Vista_CajaBcosPendientes]    Script Date: 11/18/2019 17:57:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[Vista_CajaBcosPendientes]
AS
SELECT     dbo.Recibos.PuntoNro, dbo.CajaBcoPendientes.FechaMov, dbo.CajaBcoPendientes.sNroOp, dbo.CajaBcoPendientes.sTarjeta, dbo.CajaBcoPendientes.Importe, dbo.CajaBcoPendientes.Pendiente, 
                      dbo.Clientes.RazonSocial, dbo.TipoMovimientos.Descripcion, dbo.TipoMovimientos.IdTipoMovimiento, dbo.CajaBcoPendientes.IdCajaBcoPendiente, dbo.CajaBcoPendientes.IdCajaAsociacion
FROM         dbo.CajaBcoPendientes INNER JOIN
                      dbo.Recibos ON dbo.CajaBcoPendientes.IdRecibo = dbo.Recibos.IdRecibo INNER JOIN
                      dbo.Clientes ON dbo.Recibos.IdCliente = dbo.Clientes.IdCliente INNER JOIN
                      dbo.TipoMovimientos ON dbo.CajaBcoPendientes.iTipoOp = dbo.TipoMovimientos.IdTipoMovimiento

GO


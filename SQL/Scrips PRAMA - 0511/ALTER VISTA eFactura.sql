USE [PramaSQL]
GO

/****** Object:  View [dbo].[Vista_eFactura]    Script Date: 05/09/2017 15:00:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[Vista_eFactura]
AS
SELECT     dbo.eFactura.IdFactura, dbo.eFactura.Fecha, dbo.eFactura.Punto, dbo.eFactura.Nro, dbo.eFactura.PuntoNro, CONVERT(varchar(10), CONVERT(date, dbo.eFactura.Fecha, 106), 103) AS FechaF, 
                      dbo.eFactura.IdTipoComprobante, dbo.TipoComprobantes.Comprobante, dbo.eFactura.IdFormaPago, dbo.FormasPago.FormaPago, dbo.eFactura.IdCliente, dbo.eFactura.CUIT, 
                      dbo.eFactura.IncluyeProd, dbo.eFactura.IncluyeServ, dbo.eFactura.CAE, dbo.eFactura.VecCAE, CONVERT(varchar(10), CONVERT(date, dbo.eFactura.VecCAE, 106), 103) AS VecCAEF, 
                      dbo.eFactura.Resultado, dbo.eFactura.OtrosTributos, dbo.eFactura.IdTransporte, dbo.Clientes.Direccion, dbo.TipoResponsables.TipoResponsable, dbo.Localidades.Localidad, 
                      dbo.Provincias.Provincia, dbo.Clientes.CP, dbo.Clientes.RazonSocial, dbo.Transportes.RazonSocial AS Transporte, dbo.eFactura.Neto, dbo.eFactura.Dto, dbo.eFactura.Flete, dbo.eFactura.Subtotal, 
                      dbo.eFactura.Exento, dbo.eFactura.IVA21, dbo.eFactura.IVA10, dbo.eFactura.Total
FROM         dbo.eFactura INNER JOIN
                      dbo.TipoComprobantes ON dbo.eFactura.IdTipoComprobante = dbo.TipoComprobantes.IdTipoComprobante INNER JOIN
                      dbo.FormasPago ON dbo.eFactura.IdFormaPago = dbo.FormasPago.IdFormaPago INNER JOIN
                      dbo.Clientes ON dbo.eFactura.IdCliente = dbo.Clientes.IdCliente INNER JOIN
                      dbo.TipoResponsables ON dbo.Clientes.IdCondicionIva = dbo.TipoResponsables.IdTipoResponsable INNER JOIN
                      dbo.Localidades ON dbo.Clientes.IdLocalidad = dbo.Localidades.IdLocalidad INNER JOIN
                      dbo.Transportes ON dbo.eFactura.IdTransporte = dbo.Transportes.IdTransporte INNER JOIN
                      dbo.Provincias ON dbo.Clientes.IdProvincia = dbo.Provincias.IdProvincia AND dbo.Localidades.IdProvincia = dbo.Provincias.IdProvincia


GO


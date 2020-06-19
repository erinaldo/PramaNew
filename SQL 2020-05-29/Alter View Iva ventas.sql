USE [PramaSAS]
GO

/****** Object:  View [dbo].[Vista_eFactura_IVAVta]    Script Date: 05/29/2020 10:38:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[Vista_eFactura_IVAVta]
AS
SELECT  dbo.eFactura.IdFactura, dbo.eFactura.Fecha, dbo.eFactura.Punto, dbo.eFactura.Nro, dbo.eFactura.PuntoNro, CONVERT(varchar(10), CONVERT(date, dbo.eFactura.Fecha, 106), 103) AS FechaF, 
                      dbo.eFactura.IdTipoComprobante, dbo.TipoComprobantes.Comprobante, dbo.eFactura.IdFormaPago, dbo.FormasPago.FormaPago, dbo.eFactura.IdCliente, dbo.eFactura.CUIT, 
                      dbo.eFactura.IncluyeProd, dbo.eFactura.IncluyeServ, dbo.eFactura.CAE, dbo.eFactura.VecCAE, CONVERT(varchar(10), CONVERT(date, dbo.eFactura.VecCAE, 106), 103) AS VecCAEF, 
                      dbo.eFactura.Resultado, dbo.eFactura.OtrosTributos, dbo.eFactura.IdTransporte, dbo.Clientes.Direccion, dbo.TipoResponsables.TipoResponsable, dbo.Localidades.Localidad, 
                      dbo.Provincias.Provincia, dbo.Clientes.CP, dbo.Clientes.RazonSocial, dbo.Transportes.RazonSocial AS Transporte, 
                      (case when dbo.eFactura.IdTipoComprobante =3 or dbo.eFactura.IdTipoComprobante=8 then (dbo.eFactura.Neto*-1)
					  else (dbo.eFactura.Neto) end) As 'Neto',  
                      dbo.eFactura.Dto, 
                      dbo.eFactura.Flete, 
                      (case when dbo.eFactura.IdTipoComprobante =3 or dbo.eFactura.IdTipoComprobante=8 then (dbo.eFactura.Subtotal*-1)
					  else (dbo.eFactura.Subtotal) end) As 'Subtotal',                                               
                      (case when dbo.eFactura.IdTipoComprobante =3 or dbo.eFactura.IdTipoComprobante=8 then (dbo.eFactura.Exento*-1)
					  else (dbo.eFactura.Exento) end) As 'Exento',                                                                      
                      (case when dbo.eFactura.IdTipoComprobante =3 or dbo.eFactura.IdTipoComprobante=8 then (dbo.eFactura.IVA21*-1)
					  else (dbo.eFactura.IVA21) end) As 'IVA21', 
                      (case when dbo.eFactura.IdTipoComprobante =3 or dbo.eFactura.IdTipoComprobante=8 then (dbo.eFactura.IVA10*-1)
					  else (dbo.eFactura.IVA10) end) As 'IVA10',  
                      (case when dbo.eFactura.IdTipoComprobante =3 or dbo.eFactura.IdTipoComprobante=8 then (dbo.eFactura.Total*-1)
					  else (dbo.eFactura.Total) end) As 'Total',
                      (case when dbo.eFactura.IdTipoComprobante =3 or dbo.eFactura.IdTipoComprobante=8 then (dbo.eFactura.NetoIvaVta*-1)
					  else (dbo.eFactura.NetoIvaVta) end) As 'NetoIvaVta', 
                      dbo.eFactura.Saldo, 
                      dbo.eFactura.Codigo_Correo,
                      dbo.Provincias.IdProvincia, dbo.TipoResponsables.IdTipoResponsable
FROM         dbo.eFactura INNER JOIN
                      dbo.TipoComprobantes ON dbo.eFactura.IdTipoComprobante = dbo.TipoComprobantes.IdTipoComprobante INNER JOIN
                      dbo.FormasPago ON dbo.eFactura.IdFormaPago = dbo.FormasPago.IdFormaPago INNER JOIN
                      dbo.Clientes ON dbo.eFactura.IdCliente = dbo.Clientes.IdCliente INNER JOIN
                      dbo.TipoResponsables ON dbo.Clientes.IdCondicionIva = dbo.TipoResponsables.IdTipoResponsable INNER JOIN
                      dbo.Localidades ON dbo.Clientes.IdLocalidad = dbo.Localidades.IdLocalidad INNER JOIN
                      dbo.Transportes ON dbo.eFactura.IdTransporte = dbo.Transportes.IdTransporte INNER JOIN
                      dbo.Provincias ON dbo.Clientes.IdProvincia = dbo.Provincias.IdProvincia AND dbo.Localidades.IdProvincia = dbo.Provincias.IdProvincia


GO



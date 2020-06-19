USE PramaSQL
GO
CREATE VIEW Vista_ComprobantesCompras
AS
SELECT comp.IdComprobanteCompra AS Id, comp.IdTipoComprobanteCompra AS IdTipo, t.TipoComprobanteCompra AS Comprobante, 
		comp.IdProveedor, prov.NombreFantasia AS Proveedor, comp.IdPuntoVenta AS IdPunto, punt.PuntoVenta, 
		comp.IdCondicionCompra AS IdCondicion, cond.CondicionCompra AS Condicion, comp.Fecha, comp.Numero, comp.Vence,
		comp.CantidadArticulos AS Cantidad, comp.Neto,Comp.Total, comp.Saldo, comp.Activo
FROM ComprobantesCompras AS comp, TiposComprobantesCompras AS t, Proveedores AS prov, PuntosVentas AS punt, CondicionesCompra AS cond
WHERE comp.IdTipoComprobanteCompra=t.IdTipoComprobanteCompra
AND comp.IdProveedor = prov.IdProveedor
AND comp.IdPuntoVenta = punt.IdPuntoVenta
AND comp.IdCondicionCompra = cond.IdCondicionCompra
AND comp.Activo=1;
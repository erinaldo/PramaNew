create proc ComprasDetalleComprobantes (@IdComprobante int, @IdTipo int)
as
select a.CodigoArticulo, a.Articulo, d.Cantidad, U.AbreviaturaUnidad, a.Precio, a.PorcentajeIva, c.IdProveedor
from ComprobantesCompras C, DetallesComprobantesCompras D, Articulos A, UnidadesMedida U, Proveedores P
where c.IdComprobanteCompra = d.IdComprobanteCompra
and d.IdArticulo = a.IdArticulo
and a.IdUnidadMedida = u.IdUnidadMedida
and c.IdProveedor = p.IdProveedor
and c.IdComprobanteCompra = @IdComprobante
and c.IdTipoComprobanteCompra = @IdTipo


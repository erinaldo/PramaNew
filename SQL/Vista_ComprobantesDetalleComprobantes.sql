create view Vista_ComprasDetalleComprobantes
as
select a.IdArticulo, a.CodigoArticulo, a.Articulo, d.IdComprobanteCompra, d.Cantidad, U.AbreviaturaUnidad, D.Precio, a.PorcentajeIva, c.IdProveedor, CAST((d.Cantidad*d.Precio) as decimal(11,2)) as Total, d.TipoArticulo as Tabla
from ComprobantesCompras C, DetallesComprobantesCompras D, Articulos A, UnidadesMedida U, Proveedores P
where c.IdComprobanteCompra = d.IdComprobanteCompra
and d.IdArticulo = a.IdArticulo
and a.IdUnidadMedida = u.IdUnidadMedida
and c.IdProveedor = p.IdProveedor

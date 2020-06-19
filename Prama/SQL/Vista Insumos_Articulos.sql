create view Insumos_Articulos
as
select i.IdInsumo, i.IdArticulo, i.Costo,
	   a.IdSubrubroArticulo, a.IdUnidadMedida, a.CodigoArticulo, a.Articulo, a.Unidades, a.Precio, a.UltimoCostoCompra,
	   a.UltimoProveedor, a.UltimaCompra, a.LlevaStock, a.Facturable, a.Stock, a.StockMinimo, a.StockMaximo,
	   a.StockPuntoPedido, a.PorcentajeIva	
from Insumos I, Articulos a
where i.IdArticulo = a.IdArticulo
and a.Activo = 1
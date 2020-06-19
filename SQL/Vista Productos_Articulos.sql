create view Productos_Articulos
as
select p.IdProducto, p.IdArticulo, p.CostoAcumulado, p.CostoInsumos, p.CostoGastos, p.IdAreaProduccion, p.IdCoeficienteArticulo,
	   a.IdSubrubroArticulo, a.IdUnidadMedida, a.CodigoArticulo, a.Articulo, a.Unidades, a.Precio, a.UltimoCostoCompra,
	   a.UltimoProveedor, a.UltimaCompra, a.LlevaStock, a.Facturable, a.Stock, a.StockMinimo, a.StockMaximo,
	   a.StockPuntoPedido, a.PorcentajeIva
from Productos p, Articulos a 
where p.IdArticulo = a.IdArticulo
and a.Activo = 1;
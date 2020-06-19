create view Articulos_Insumos_Productos
as
select a.IdArticulo as IdArticulo, a.CodigoArticulo as Codigo, a.Articulo as Articulo, p.IdProducto as IdPropio, 
		'Producto' as Tabla, a.Unidades as Unidades, u.AbreviaturaUnidad as Abreviatura
from Productos P, Articulos a, UnidadesMedida U
where p.IdArticulo = a.IdArticulo
and a.IdUnidadMedida = u.IdUnidadMedida
and a.Activo = 1
union all
select a.IdArticulo as IdArticulo,a.CodigoArticulo as Codigo, a.Articulo as Articulo, i.IdInsumo as IdPropio, 
		'Insumo' as Tabla, a.Unidades as Unidades, u.AbreviaturaUnidad as Abreviatura
from Insumos I, Articulos a, UnidadesMedida U
where i.IdArticulo = a.IdArticulo
and a.IdUnidadMedida = u.IdUnidadMedida
and a.Activo = 1

select * from Articulos



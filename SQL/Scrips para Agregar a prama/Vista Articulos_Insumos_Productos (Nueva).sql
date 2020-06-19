create view Articulos_Insumos_Productos
as
SELECT     a.IdArticulo AS IdArticulo, a.CodigoArticulo AS CodigoArticulo, a.Articulo AS Articulo, 
					p.IdProducto AS IdPropio, 'PRODUCTOS' AS Tabla, a.Unidades AS Unidades, 
                      u.AbreviaturaUnidad AS AbreviaturaUnidad, a.Precio AS Precio, a.PorcentajeIva AS IVA
FROM         Productos P, Articulos a, UnidadesMedida U
WHERE     p.IdArticulo = a.IdArticulo AND a.IdUnidadMedida = u.IdUnidadMedida AND a.Activo = 1
UNION ALL
SELECT     a.IdArticulo AS IdArticulo, a.CodigoArticulo AS CodigoArticulo, a.Articulo AS Articulo, 
					i.IdInsumo AS IdPropio, 'INSUMOS' AS Tabla, a.Unidades AS Unidades, 
                      u.AbreviaturaUnidad AS AbreviaturaUnidad, i.Costo AS Precio, a.PorcentajeIva AS IVA
FROM         Insumos I, Articulos a, UnidadesMedida U
WHERE     i.IdArticulo = a.IdArticulo AND a.IdUnidadMedida = u.IdUnidadMedida AND a.Activo = 1
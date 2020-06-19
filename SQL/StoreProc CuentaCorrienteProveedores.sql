use PramaSQL
go
create proc CCProvedores (@IdProveedor int)
as
select cc.IdComprobanteCompra as IdComprobante, cc.Fecha as Fecha, 'FACTURA' as TipoComprobante, 
		cc.Numero as Numero, cc.Usuario as Comprador, cc.Saldo as Debe, 0 as Haber
from ComprobantesCompras as CC
where cc.IdProveedor = 1

union all

select op.IdOrdenPago as IdComprobante, op.Fecha as Fecha, 'ORDEN DE PAGO' as TipoComprobante,
		op.Numero as Numero, op.Usuario as Comprador, 0 as Debe, op.Total as Haber
from OrdenesPago as OP
where op.IdProveedor = 1

order by Fecha desc
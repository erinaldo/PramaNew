Use PramaSAS
go
EXEC sp_MSforeachtable @command1="ALTER TABLE ? NOCHECK CONSTRAINT ALL"
GO
delete from Articulos
Delete from Bancos
delete from Caja
delete from CajaAsociacionesCuentas
delete from CajaBancos
delete from CajaBcoPendientes
delete from CajaCredito
delete from CajaCuentas
delete from CajaDebito
delete from CajaEfectivo
delete from CajaMP
delete from CajaOtrosTipos
delete from CajaTransferencias
delete from Cheques
delete from ChequesOrdenesPago
delete from ChequesPropios
delete from ChequesRecibos
delete from ChequesTerceros
delete from Clientes
delete from CoeficientesArticulos
delete from ComprobantesCompras
delete from DetallePedidos
delete from DetallesComprobantesCompras
delete from DetallePresupuestos
delete from eFactura
delete from eFacturaDetalle
delete from MenuOpciones
delete from MenuOpcionesUser
delete from MenuUser
delete from MenuSistema
delete from MigrarCodigos
delete from OPDetallePropios
delete from OPDetalleTerceros
delete from OPDetalleTransferencias
delete from Pedidos
delete from Parametros
delete from Presupuestos
delete from Productos
delete from ProductosCompuestos
delete from ProductosGastosFijos
delete from ProductosInsumos
delete from Insumos
delete from InsumosCompuestos
delete from Proveedores
delete from PuntosVentaAFIP
delete from PuntosVentas
delete from Recibos
delete from RecibosDetalle
delete from Remitos
delete from RubrosArticulos
delete from SaldoCliProv
delete from StockFabricacion
delete from StockMovimientoInternoDetalle
delete from StockMovimientos
delete from StockMovimientosInternos
delete from SubrubrosArticulos
delete from Temporal_CargaDetPedido
delete from Temporal_CargaDetPresu
delete from Temporal_DetalleCheques
delete from Temporal_DetallePagoCaja
delete from Usuarios
GO
-- SQL enable all constraints - enable all constraints sql server
-- sp_MSforeachtable is an undocumented system stored procedure
EXEC sp_MSforeachtable @command1="ALTER TABLE ? CHECK CONSTRAINT ALL"
GO
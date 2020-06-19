--------------------------------------------------------
--            CREACIÓN DE LA BASE DE DATOS            --
--------------------------------------------------------

CREATE DATABASE PramaSQL
GO
-- Activamos la base de datos para poder crear la tablas
USE PramaSQL
GO
--------------------------------------------------------
--            CREACIÓN DE TODAS LAS TABLAS            --
--------------------------------------------------------

CREATE TABLE Usuarios
(
IdUsuario INT IDENTITY (1,1) NOT NULL,
Usuario VARCHAR (50) NOT NULL,
Clave VARCHAR (50) NOT NULL,
Nivel INT NOT NULL,
Activo BIT,
Imagen IMAGE,

CONSTRAINT PK_Usuarios PRIMARY KEY (IdUsuario)
)

CREATE TABLE Provincias
(
IdProvincia INT IDENTITY (1,1) NOT NULL,
Provincia VARCHAR(30) NOT NULL,

CONSTRAINT PK_Provincias PRIMARY KEY (IdProvincia)
)

CREATE TABLE Localidades
(
IdLocalidad INT IDENTITY (1,1) NOT NULL,
Localidad VARCHAR(50) NOT NULL,
CP varchar(8),
IdProvincia INT NOT NULL,
Activo BIT,

CONSTRAINT PK_Localidades PRIMARY KEY (IdLocalidad),
CONSTRAINT FK_LocalidadesProvincias FOREIGN KEY (IdProvincia) REFERENCES Provincias (IdProvincia)
)

CREATE TABLE CondicionesIva
(
IdCondicionIva INT IDENTITY (1,1) NOT NULL,
CondicionIva VARCHAR (50),
Activo BIT,

CONSTRAINT PK_CondicionesIva PRIMARY KEY (IdCondicionIva)
)

CREATE TABLE PuntosVentas
(
IdPuntoVenta INT IDENTITY (1,1) NOT NULL,
PuntoVenta VARCHAR (50) NOT NULL,
Activo BIT,

CONSTRAINT PK_PuntosVentas PRIMARY KEY (IdPuntoVenta)
)

CREATE TABLE Almacenes
(
IdAlmacen INT IDENTITY (1,1) NOT NULL,
Almacen VARCHAR (50) NOT NULL,
Activo BIT,

CONSTRAINT PK_Almacenes PRIMARY KEY (IdAlmacen)
)

----------------------------------------------------------
--       SECCION RELACIONADA CON LOS PROVEEDORES
----------------------------------------------------------

CREATE TABLE CondicionesCompra
(
IdCondicionCompra INT IDENTITY (1,1) NOT NULL,
CondicionCompra VARCHAR(50) NOT NULL,
RecargaPrecios BIT,
PorcentajeRecargo DECIMAL (5,2),
PorcentajeDescuento DECIMAL (5,2),
PlazoPago INT NOT NULL,
Activo BIT,

CONSTRAINT PK_CondicionesCompra PRIMARY KEY (IdCondicionCompra)
)

CREATE TABLE Proveedores
(
IdProveedor INT IDENTITY (1,1) NOT NULL,
NombreFantasia VARCHAR (70),
RazonSocial VARCHAR (70) NOT NULL,
IdCondicionIva INT NOT NULL,
CUIT VARCHAR (13) NOT NULL,
IngresosBrutos VARCHAR (20),
FechaInicioActividad VARCHAR (10),
IdCondicionCompra INT NOT NULL,
Direccion VARCHAR (70) NOT NULL,
IdProvincia INT NOT NULL,
IdLocalidad INT NOT NULL,
Telefono VARCHAR (30),
Fax VARCHAR (30),
Celular VARCHAR (30),
MailEmpresa VARCHAR (70),
Web VARCHAR (70),
Contacto VARCHAR (50),
MailContacto VARCHAR (70),
CelularContacto VARCHAR (30),
Observaciones VARCHAR (500),
Activo BIT,

CONSTRAINT PK_Proveedores PRIMARY KEY (IdProveedor),
CONSTRAINT FK_ProveedoresCondicionesIva FOREIGN KEY (IdCondicionIva) REFERENCES CondicionesIva (IdCondicionIva),
CONSTRAINT FK_ProveedoresConsicionesCompra FOREIGN KEY (IdCondicionCompra) REFERENCES CondicionesCompra (IdCondicionCompra),
CONSTRAINT FK_ProveedoresProvincias FOREIGN KEY (IdProvincia) REFERENCES Provincias (IdProvincia),
CONSTRAINT FK_ProveedoresLocalidades FOREIGN KEY (IdLocalidad) REFERENCES Localidades (IdLocalidad)
)

----------------------------------------------------------
--  SECCION RELACIONADA CON LOS ARTICULOS
----------------------------------------------------------

CREATE TABLE RubrosArticulos
(
IdRubroArticulo INT IDENTITY (1,1) NOT NULL,
RubroArticulo VARCHAR (50) NOT NULL,
Activo BIT,

CONSTRAINT PK_RubrosArticulos PRIMARY KEY (IdRubroArticulo)
)

CREATE TABLE SubrubrosArticulos
(
IdSubrubroArticulo INT IDENTITY (1,1) NOT NULL,
SubrubroArticulo VARCHAR (50) NOT NULL,
IdRubroArticulo INT NOT NULL,
Activo BIT,

CONSTRAINT PK_SubrubrosArticulos PRIMARY KEY (IdSubrubroArticulo),
CONSTRAINT FK_SubrubrosArticulosRubrosArticulos FOREIGN KEY (IdRubroArticulo) REFERENCES RubrosArticulos (IdRubroArticulo)
)

CREATE TABLE TiposArticulos
(
IdTipoArticulo INT IDENTITY (1,1) NOT NULL,
TipoArticulo VARCHAR (50) NOT NULL,
CoeficientePublico DECIMAL (5,2) NOT NULL,
CoeficienteDistribuidor DECIMAL (5,2) NOT NULL,
CoeficienteRevendedor DECIMAL (5,2) NOT NULL,
Activo BIT,

CONSTRAINT PK_TiposArticulos PRIMARY KEY (IdTipoArticulo)
)

CREATE TABLE UnidadesMedida
(
IdUnidadMedida INT IDENTITY (1,1) NOT NULL,
UnidadMedida VARCHAR (50) NOT NULL,
AbreviaturaUnidad VARCHAR (5),
Activo BIT,

CONSTRAINT PK_UnidadMedida PRIMARY KEY (IdUnidadMedida)
)

CREATE TABLE Articulos
(
IdArticulo INT IDENTITY (1,1) NOT NULL,
IdSubrubroArticulo INT NOT NULL,
IdTipoArticulo INT NOT NULL,
IdUnidadMedida INT NOT NULL,
CodigoArticulo VARCHAR (10) NOT NULL,
Articulo VARCHAR (70) NOT NULL,
Unidades DECIMAL (11,2) NOT NULL,
Precio DECIMAL (11,2) NOT NULL,
UltimoCostoCompra DECIMAL (11,2),
UltimoProveedor VARCHAR (70), -- IdProveedor + RazonSocial
UltimaCompra VARCHAR (10),
LlevaStock BIT,
Facturable BIT,
Stock DECIMAL (11,2),
StockMinimo DECIMAL (11,2),
StockMaximo DECIMAL (11,2),
StockPuntoPedido DECIMAL (11,2),
PorcentajeIva DECIMAL (5,2) NOT NULL,
Activo BIT,

CONSTRAINT PK_Articulos PRIMARY KEY (IdArticulo),
CONSTRAINT FK_ArticulosSubrubrosArticulos FOREIGN KEY (IdSubrubroArticulo) REFERENCES SubrubrosArticulos (IdSubrubroArticulo),
CONSTRAINT FK_ArticulosTiposArticulos FOREIGN KEY (IdTipoArticulo) REFERENCES TiposArticulos (IdTipoArticulo),
CONSTRAINT FK_ArticulosUnidadesMedida FOREIGN KEY (IdUnidadMedida) REFERENCES UnidadesMedida (IdUnidadMedida)
)

CREATE TABLE ArticulosProveedoresCodigos
(
IdArticuloProveedorCodigo INT IDENTITY (1,1) NOT NULL,
IdArticulo INT NOT NULL,
IdProveedor INT NOT NULL,
CodigoProveedor VARCHAR (50),

CONSTRAINT PK_ArticulosProveedoresCodigos PRIMARY KEY (IdArticuloProveedorCodigo),
CONSTRAINT FK_ArticulosProveedoresCodigosArticulos FOREIGN KEY (IdArticulo) REFERENCES Articulos (IdArticulo),
CONSTRAINT FK_ArticulosProveedoresCodigosProveedores FOREIGN KEY (IdProveedor) REFERENCES Proveedores (IdProveedor)
)

CREATE TABLE Insumos
(
IdInsumo INT IDENTITY (1,1) NOT NULL,
IdArticulo INT NOT NULL,
Costo DECIMAL (11,2) NOT NULL,

CONSTRAINT PK_Insumos PRIMARY KEY (IdInsumo),
CONSTRAINT FK_InsumosArticulos FOREIGN KEY (IdArticulo) REFERENCES Articulos (IdArticulo)
)

CREATE TABLE AreasProduccion
(
IdAreaProduccion INT IDENTITY (1,1) NOT NULL,
AreasProduccion VARCHAR (50) NOT NULL,
Activo BIT,

CONSTRAINT PK_AreasProduccion PRIMARY KEY (IdAreaProduccion)
)

CREATE TABLE Productos
(
IdProducto INT IDENTITY (1,1) NOT NULL,
IdArticulo INT NOT NULL,
CostoAcumulado DECIMAL (11,2),
CostoInsumos DECIMAL (11,2),
CostoGastos DECIMAL (11,2),
CostoIngredientes DECIMAL (11,2),
IdAreaProduccion INT NOT NULL,

CONSTRAINT PK_Productos PRIMARY KEY (IdProducto),
CONSTRAINT FK_ProductosArticulos FOREIGN KEY (IdArticulo) REFERENCES Articulos (IdArticulo),
CONSTRAINT FK_ProductosAreasProduccion FOREIGN KEY (IdAreaProduccion) REFERENCES AreasProduccion (IdAreaProduccion)
)

CREATE TABLE Ingredientes
(
IdIngrediente INT IDENTITY (1,1) NOT NULL,
IdArticulo INT NOT NULL,
Costo DECIMAL (11,2) NOT NULL,

CONSTRAINT PK_Ingredientes PRIMARY KEY (IdIngrediente),
CONSTRAINT FK_IngredientesArticulos FOREIGN KEY (IdArticulo) REFERENCES Articulos (IdArticulo)
)

CREATE TABLE ProductosInsumos
(
IdProductoInsumo INT IDENTITY (1,1) NOT NULL,
IdProducto INT NOT NULL,
IdInsumo INT NOT NULL,
Cantidad DECIMAL (11,2) NOT NULL,
Activo BIT,

CONSTRAINT PK_ProductosInsumos PRIMARY KEY (IdProductoInsumo),
CONSTRAINT FK_ProductosInsumosProductos FOREIGN KEY (IdProducto) REFERENCES Productos (IdProducto),
CONSTRAINT FK_ProductosInsumosInsumos FOREIGN KEY (IdInsumo) REFERENCES Insumos (IdInsumo)
)

CREATE TABLE ProductosIngredientes
(
IdProductoIngrediente INT IDENTITY (1,1) NOT NULL,
IdProducto INT NOT NULL,
IdIngrediente INT NOT NULL,
Cantidad DECIMAL (11,2) NOT NULL,
Activo BIT,

CONSTRAINT PK_ProductosIngredientes PRIMARY KEY (IdProductoIngrediente),
CONSTRAINT FK_ProductosIngredientesProductos FOREIGN KEY (IdProducto) REFERENCES Productos (IdProducto),
CONSTRAINT FK_ProductosIngredientesIngredientes FOREIGN KEY (IdIngrediente) REFERENCES Ingredientes (IdIngrediente)
)

CREATE TABLE GastosFijos
(
IdGastoFijo INT IDENTITY (1,1) NOT NULL,
GastoFijo VARCHAR (70) NOT NULL,
Fecha VARCHAR (10) NOT NULL,
Importe DECIMAL (11,2) NOT NULL,
Activo BIT,

CONSTRAINT PK_GastosFijos PRIMARY KEY (IdGastoFijo)
)

CREATE TABLE ProductosGastosFijos
(
IdProductoGastoFijo INT IDENTITY (1,1) NOT NULL,
IdProducto INT NOT NULL,
IdGastoFijo INT NOT NULL,
Coeficiente DECIMAL (11,2) NOT NULL,
Activo BIT,

CONSTRAINT PK_ProductosGastosFijos PRIMARY KEY (IdProductoGastoFijo),
CONSTRAINT FK_ProductosGastosFijosProductos FOREIGN KEY (IdProducto) REFERENCES	Productos (IdProducto),
CONSTRAINT FK_ProductosGastosFijosGastosFijos FOREIGN KEY (IdGastoFijo) REFERENCES GastosFijos (IdGastoFijo)
)


----------------------------------------------------------
--  SECCION RELACIONADA CON LOS COMPROBANTES DE COMPRAS
----------------------------------------------------------

CREATE TABLE TiposComprobantesCompras
(
IdTipoComprobanteCompra INT IDENTITY (1,1) NOT NULL,
TipoComprobanteCompra VARCHAR (50) NOT NULL,
Activo BIT,

CONSTRAINT PK_TiposComprobantesCompras PRIMARY KEY (IdTipoComprobanteCompra)
)

CREATE TABLE Imputaciones
(
IdImputacion INT IDENTITY (1,1) NOT NULL,
Imputacion VARCHAR (70),

CONSTRAINT PK_Imputaciones PRIMARY KEY (IdImputacion)
)

CREATE TABLE ComprobantesCompras
(
IdComprobanteCompra INT IDENTITY (1,1) NOT NULL,
IdTipoComprobanteCompra INT NOT NULL,
IdProveedor INT NOT NULL,
IdPuntoVenta INT NOT NULL,
IdCondicionCompra INT NOT NULL,
Fecha VARCHAR (10) NOT NULL,
Numero VARCHAR (30) NOT NULL,
Vence VARCHAR (10),
CantidadArticulos INT NOT NULL,
Neto DECIMAL (11,2),
Total DECIMAL (11,2),
Saldo DECIMAL (11,2),
Activo BIT,

CONSTRAINT PK_ComprobantesCompras PRIMARY KEY (IdComprobanteCompra),
CONSTRAINT FK_ComprobantesComprasTiposComprobantesCompras FOREIGN KEY (IdTipoComprobanteCompra) REFERENCES TiposComprobantesCompras (IdTipoComprobanteCompra),
CONSTRAINT FK_ComprobantesComprasProveedores FOREIGN KEY (IdProveedor) REFERENCES Proveedores (IdProveedor),
CONSTRAINT FK_ComprobantesComprasPuntosVentas FOREIGN KEY (IdPuntoVenta) REFERENCES PuntosVentas (IdPuntoVenta),
CONSTRAINT FK_ComprabantesComprasCondicionesCompra FOREIGN KEY (IdCondicionCompra) REFERENCES CondicionesCompra (IdCondicionCompra)
)

CREATE TABLE DetallesComprobantesCompras
(
IdDetalleComprobanteCompra INT IDENTITY (1,1) NOT NULL,
IdArticulo INT NOT NULL,
IdComprobanteCompra INT NOT NULL,
IdAlmacen INT NOT NULL,
Cantidad DECIMAL (11,2) NOT NULL,
Precio DECIMAL (11,2)NOT NULL,
Activo BIT,

CONSTRAINT PK_DetallesComprobantesCompras PRIMARY KEY (IdDetalleComprobanteCompra),
CONSTRAINT FK_DetallesComprobantesComprasArticulos FOREIGN KEY (IdArticulo) REFERENCES Articulos (IdArticulo),
CONSTRAINT FK_DetallesComprobantesComprasComprobantesCompra FOREIGN KEY (IdComprobanteCompra) REFERENCES ComprobantesCompras (IdComprobanteCompra)
CONSTRAINT FK_DetallesComprobantesComprasAlmacenes FOREIGN KEY (IdAlmacen) REFERENCES Almacenes (IdAlmacen)
)

CREATE TABLE OrdenesPago
(
IdOrdenPago INT IDENTITY (1,1) NOT NULL,
Fecha VARCHAR (10) NOT NULL,
Numero VARCHAR (30) NOT NULL,
ChequesPropios DECIMAL (11,2),
ChequesTerceros DECIMAL (11,2),
Efectivo DECIMAL (11,2),
Transferencia DECIMAL (11,2),
Activo BIT,

CONSTRAINT PK_OrdenesPago PRIMARY KEY (IdOrdenPago)
)

CREATE TABLE OrdenesPagoComprobantes
(
IdOrdenPagoComprobante INT IDENTITY (1,1) NOT NULL,
IdComprobanteCompra INT NOT NULL,
IdOrdenPago INT NOT NULL,
Importe DECIMAL (11,2),
Activo BIT,

CONSTRAINT PK_OrdenesPagoComprobantes PRIMARY KEY (IdOrdenPagoComprobante),
CONSTRAINT FK_OrdenesPagoComprobantesComprobanteCompra FOREIGN KEY (IdComprobanteCompra) REFERENCES ComprobantesCompras (IdComprobanteCompra),
CONSTRAINT FK_OrdenesPagoComprobantesOrdenPago FOREIGN KEY (IdOrdenPago) REFERENCES OrdenesPago (IdOrdenPago)
)

CREATE TABLE TiposCheques
(
IdTipoCheque INT IDENTITY (1,1) NOT NULL,
TipoCheque VARCHAR (50) NOT NULL,
Activo BIT,

CONSTRAINT PK_TiposCheques PRIMARY KEY (IdTipoCheque)
)

CREATE TABLE Bancos
(
IdBanco INT IDENTITY (1,1) NOT NULL,
Banco VARCHAR (70) NOT NULL,
Codigo INT,
Activo BIT,

CONSTRAINT PK_Bancos PRIMARY KEY (IdBanco)
)

CREATE TABLE Cheques
(
IdCheque INT IDENTITY (1,1) NOT NULL,
Numero INT NOT NULL,
FechaEmision VARCHAR (10) NOT NULL,
FechaCobro VARCHAR (10) NOT NULL,
Importe DECIMAL (11,2) NOT NULL,
Activo BIT,

CONSTRAINT PK_Cheques PRIMARY KEY (IdCheque)
)

CREATE TABLE CuentasCorrientesPropias
(
IdCuentaCorriente INT IDENTITY (1,1) NOT NULL,
CuentaCorriente VARCHAR (50) NOT NULL,
IdBanco INT NOT NULL,
Activo BIT,

CONSTRAINT PK_CuentasCorrientesPropias PRIMARY KEY (IdCuentaCorriente),
CONSTRAINT FK_CuentasCorrientesPropiasBancos FOREIGN KEY (IdBanco) REFERENCES Bancos (IdBanco)
)

CREATE TABLE ChequesPropios
(
IdChequePropio INT IDENTITY (1,1) NOT NULL,
IdCheque INT NOT NULL,
IdCuentaCorriente INT NOT NULL,

CONSTRAINT PK_ChequesPropios PRIMARY KEY (IdChequePropio),
CONSTRAINT FK_ChequesPropiosCheques FOREIGN KEY (IdCheque) REFERENCES Cheques (IdCheque),
CONSTRAINT FK_ChequesPropiosCuentasCorrientesPropias FOREIGN KEY (IdCuentaCorriente) REFERENCES CuentasCorrientesPropias (IdCuentaCorriente)
)

CREATE TABLE ChequesTerceros
(
IdChequeTercero INT IDENTITY (1,1) NOT NULL,
IdTipoCheque INT NOT NULL,
IdBanco INT NOT NULL,
IdCheque INT NOT NULL,
CuitTitular VARCHAR (13),
Cuenta VARCHAR (50),
EnCartera BIT,

CONSTRAINT PK_ChequesTerceros PRIMARY KEY (IdChequeTercero),
CONSTRAINT FK_ChequesTercerosTiposCheques FOREIGN KEY (IdTipoCheque) REFERENCES TiposCheques (IdTipoCheque),
CONSTRAINT FK_ChequesTercerosBancos FOREIGN KEY (IdBanco) REFERENCES Bancos (IdBanco),
CONSTRAINT FK_ChequesTercerosCheques FOREIGN KEY (IdCheque) REFERENCES Cheques (IdCheque)
)

CREATE TABLE OPDetalleTerceros
(
IdOPDetalleTer INT IDENTITY (1,1) NOT NULL,
IdOrdenPago INT NOT NULL,
IdChequeTercero INT NOT NULL,
Activo BIT,

CONSTRAINT PK_OPDetalleTerceros PRIMARY KEY (IdOPDetalleTer),
CONSTRAINT FK_OPDetalleTercerosOrdenesPago FOREIGN KEY (IdOrdenPago) REFERENCES OrdenesPago (IdOrdenPago),
CONSTRAINT FK_OPDetalleTercerosChequesTerceros FOREIGN KEY (IdChequeTercero) REFERENCES ChequesTerceros (IdChequeTercero)
)

CREATE TABLE OPDetallePropios
(
IdOPDetallePropio INT IDENTITY (1,1) NOT NULL,
IdOrdenPago INT NOT NULL,
IdChequePropio INT NOT NULL,
Activo BIT,

CONSTRAINT PK_OPDetallePropios PRIMARY KEY (IdOPDetallePropio),
CONSTRAINT FK_OPDetallePropiosOrdenesPago FOREIGN KEY (IdOrdenPago) REFERENCES OrdenesPago (IdOrdenPago),
CONSTRAINT FK_OPDetallePropiosChequesPropios FOREIGN KEY (IdChequePropio) REFERENCES ChequesPropios (IdChequePropio)
)

CREATE TABLE OPDetalleTransferencias
(
IdOPDetalleTransferencia INT IDENTITY (1,1) NOT NULL,
IdOrdenPago INT NOT NULL,
IdCuentaCorriente INT NOT NULL,
Importe DECIMAL (11,2) NOT NULL,
Activo BIT,

CONSTRAINT PK_OPDetalleTransferencias PRIMARY KEY (IdOPDetalleTransferencia),
CONSTRAINT FK_OPDetalleTransferenciasOrdenesPago FOREIGN KEY (IdOrdenPago) REFERENCES OrdenesPago (IdOrdenPago),
CONSTRAINT FK_OPDetalleTransferenciasCuentasCorrientesPropias FOREIGN KEY (IdCuentaCorriente) REFERENCES CuentasCorrientesPropias (IdCuentaCorriente)
)



----------------------------------------------------------
--  SECCION RELACIONADA CON LOS PARÁMETROS DEL SISTEMA  --
----------------------------------------------------------

CREATE TABLE Parametros
(
RazonSocial varchar(70),
NombreFantasia varchar (70),
Direccion varchar (70),
Telefono varchar (10),
Mail varchar (70),
Web varchar (70),
CUIT varchar (13),
Localidad varchar (70),
CondicionIva varchar (70),
Iva decimal (4,2),
Fondo image,
Icono image,
Splash image,
Impresion image,
Presupuestos int,
Facturas int,
Remitos int,
Recibos int,
Pedidos int,
CaducidadPresupuestos int,
CaducidadPedidos int,
Imprimir bit,
)
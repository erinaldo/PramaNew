USE PramaSQL
GO

------------------------------------------------
-- INSERCIÓN DE LOS DATOS GENERALES A LA BASE --
------------------------------------------------

-- TABLA USUARIOS --
INSERT INTO Usuarios (Usuario, Clave, Nivel, Activo) VALUES ('PROGRAMADORES','a', 5, 1) 
INSERT INTO Usuarios (Usuario, Clave, Nivel, Activo) VALUES ('ALINA','a', 4, 1) 
INSERT INTO Usuarios (Usuario, Clave, Nivel, Activo) VALUES ('FRANCO','a', 4, 1) 
INSERT INTO Usuarios (Usuario, Clave, Nivel, Activo) VALUES ('MATEO','a', 3, 1) 

-- TABLA CONDICIONES DE COMPRA --

INSERT INTO CondicionesCompra (CondicionCompra, RecargaPrecios, PorcentajeDescuento, PorcentajeRecargo, PlazoPago, Activo) VALUES ('CONTADO',0,0,0,0,1)
INSERT INTO CondicionesCompra (CondicionCompra, RecargaPrecios, PorcentajeDescuento, PorcentajeRecargo, PlazoPago, Activo) VALUES ('CONTADO EFECTIVO',0,5,0,0,1)
INSERT INTO CondicionesCompra (CondicionCompra, RecargaPrecios, PorcentajeDescuento, PorcentajeRecargo, PlazoPago, Activo) VALUES ('CUENTA CORRIENTE 7 DÍAS',0,0,0,7,1)
INSERT INTO CondicionesCompra (CondicionCompra, RecargaPrecios, PorcentajeDescuento, PorcentajeRecargo, PlazoPago, Activo) VALUES ('CUENTA CORRIENTE 15 DÍAS',0,0,0,15,1)
INSERT INTO CondicionesCompra (CondicionCompra, RecargaPrecios, PorcentajeDescuento, PorcentajeRecargo, PlazoPago, Activo) VALUES ('CUENTA CORRIENTE 30 DÍAS',0,0,0,30,1)
INSERT INTO CondicionesCompra (CondicionCompra, RecargaPrecios, PorcentajeDescuento, PorcentajeRecargo, PlazoPago, Activo) VALUES ('CUENTA CORRIENTE 60 DÍAS',1,0,5,7,1)

-- TABLA CONDICIONES DE IVA --

INSERT INTO CondicionesIva (CondicionIva, Activo) VALUES ('IVA RESPONSABLE INSCRIPTO',1)
INSERT INTO CondicionesIva (CondicionIva, Activo) VALUES ('IVA RESPONSABLE NO INSCRIPTO',1)
INSERT INTO CondicionesIva (CondicionIva, Activo) VALUES ('IVA NO RESPONSABLE',1)
INSERT INTO CondicionesIva (CondicionIva, Activo) VALUES ('IVA SUJETO EXENTO',1)
INSERT INTO CondicionesIva (CondicionIva, Activo) VALUES ('CONSUMIDOR FINAL',1)
INSERT INTO CondicionesIva (CondicionIva, Activo) VALUES ('RESPONSABLE MONOTRIBUTO',1)

-- TABLA COMPROBANTES DE COMPRAS --

INSERT INTO TiposComprobantesCompras (TipoComprobanteCompra, Activo) VALUES ('COTIZACIÓN', 1)
INSERT INTO TiposComprobantesCompras (TipoComprobanteCompra, Activo) VALUES ('ORDEN DE COMPRA', 1)
INSERT INTO TiposComprobantesCompras (TipoComprobanteCompra, Activo) VALUES ('FACTURA', 1)


-- TABLA DE PUNTOS DE VENTAS --

INSERT INTO PuntosVentas (PuntoVenta, Activo) VALUES ('PRAMA',1)
INSERT INTO PuntosVentas (PuntoVenta, Activo) VALUES ('ESPACIO DEPURATIVO',1)

-- TABLA ALMACENES

INSERT INTO Almacenes (Almacen, Activo) VALUES ('PRAMA',1)
INSERT INTO Almacenes (Almacen, Activo) VALUES ('ESPACIO DEPURATIVO',1)

-- TABLA RUBROS ARTÍCULOS --

INSERT INTO RubrosArticulos (RubroArticulo, Activo) VALUES ('ALIMENTARIOS FISIOLOGICOS',1)
INSERT INTO RubrosArticulos (RubroArticulo, Activo) VALUES ('OTRAS HERRAMIENTAS DEPURATIVAS',1)
INSERT INTO RubrosArticulos (RubroArticulo, Activo) VALUES ('NO ALIMENTARIOS',1)
INSERT INTO RubrosArticulos (RubroArticulo, Activo) VALUES ('LIBROS',1)

-- TABLA SUBRUBROS ARTICULOS --

INSERT INTO SubrubrosArticulos (SubrubroArticulo, IdRubroArticulo, Activo) VALUES ('ACEITES Y ACEITUNAS',1,1)
INSERT INTO SubrubrosArticulos (SubrubroArticulo, IdRubroArticulo, Activo) VALUES ('ALGAS',1,1)
INSERT INTO SubrubrosArticulos (SubrubroArticulo, IdRubroArticulo, Activo) VALUES ('CONDIMENTOS',1,1)
INSERT INTO SubrubrosArticulos (SubrubroArticulo, IdRubroArticulo, Activo) VALUES ('DESHIDRATADOS',1,1)
INSERT INTO SubrubrosArticulos (SubrubroArticulo, IdRubroArticulo, Activo) VALUES ('HIERBAS',2,1)
INSERT INTO SubrubrosArticulos (SubrubroArticulo, IdRubroArticulo, Activo) VALUES ('HIERBAS - DERIVADOS',2,1)
INSERT INTO SubrubrosArticulos (SubrubroArticulo, IdRubroArticulo, Activo) VALUES ('PRODUCTOS APÍCOLAS',2,1)
INSERT INTO SubrubrosArticulos (SubrubroArticulo, IdRubroArticulo, Activo) VALUES ('PRODUCTOS VARIOS',2,1)
INSERT INTO SubrubrosArticulos (SubrubroArticulo, IdRubroArticulo, Activo) VALUES ('BOTELLAS VIDRIO',3,1)
INSERT INTO SubrubrosArticulos (SubrubroArticulo, IdRubroArticulo, Activo) VALUES ('BOTIQUÍN HOMEOPÁTICO',3,1)
INSERT INTO SubrubrosArticulos (SubrubroArticulo, IdRubroArticulo, Activo) VALUES ('COSMÉTICA NATURAL',3,1)
INSERT INTO SubrubrosArticulos (SubrubroArticulo, IdRubroArticulo, Activo) VALUES ('PRODUCTOS APÍCOLAS',3,1)
INSERT INTO SubrubrosArticulos (SubrubroArticulo, IdRubroArticulo, Activo) VALUES ('ANTROPOSOFÍA',4,1)
INSERT INTO SubrubrosArticulos (SubrubroArticulo, IdRubroArticulo, Activo) VALUES ('ANTROPOSOFIA - AGRICULTURA BIODINAMICA',4,1)
INSERT INTO SubrubrosArticulos (SubrubroArticulo, IdRubroArticulo, Activo) VALUES ('EDICIONES GEA',4,1)
INSERT INTO SubrubrosArticulos (SubrubroArticulo, IdRubroArticulo, Activo) VALUES ('EDICIONES PROPIAS',4,1)

-- TABLA TIPOS DE ARTICULOS --

INSERT INTO TiposArticulos (TipoArticulo, CoeficientePublico,CoeficienteDistribuidor, CoeficienteRevendedor, Activo) VALUES ('PRODUCTOS FRACCIONADOS',1.50,1.35,1.35,1)
INSERT INTO TiposArticulos (TipoArticulo, CoeficientePublico,CoeficienteDistribuidor, CoeficienteRevendedor, Activo) VALUES ('ELABORACION PROPIA',2.30,1.84,1.84,1)
INSERT INTO TiposArticulos (TipoArticulo, CoeficientePublico,CoeficienteDistribuidor, CoeficienteRevendedor, Activo) VALUES ('PUBLICACION PROPIA',2,1.4,1.4,1)
   
-- TABLA UNIDADES DE MEDIDA --

INSERT INTO UnidadesMedida (UnidadMedida, AbreviaturaUnidad, Activo) VALUES ('UNIDAD', 'UN.',1)
INSERT INTO UnidadesMedida (UnidadMedida, AbreviaturaUnidad, Activo) VALUES ('GRAMOS', 'GRS.',1)
INSERT INTO UnidadesMedida (UnidadMedida, AbreviaturaUnidad, Activo) VALUES ('KILOGRAMOS', 'KGS.',1)
INSERT INTO UnidadesMedida (UnidadMedida, AbreviaturaUnidad, Activo) VALUES ('CENTÍMETROS CÚBICOS', 'CM3',1)
INSERT INTO UnidadesMedida (UnidadMedida, AbreviaturaUnidad, Activo) VALUES ('LITROS', 'LTS.',1)
INSERT INTO UnidadesMedida (UnidadMedida, AbreviaturaUnidad, Activo) VALUES ('PÁGINAS', 'PAG.',1)

-- TABLA PROVEEDORES --

INSERT INTO Proveedores (NombreFantasia,RazonSocial,IdCondicionIva,CUIT,IngresosBrutos,FechaInicioActividad,
			IdCondicionCompra,Direccion,IdProvincia,IdLocalidad,Telefono,Fax,Celular,MailEmpresa,Web,Contacto,MailContacto,
			CelularContacto,Observaciones,Activo)
VALUES ('TRANSCARGO','MOAN MEDITERRANEA SRL',1,'30-71405042-3','','15/10/2012',5,'JUAN B. JUSTO 2825 - BARRIO : ALTA CORDOBA',
        6,21414,'3514735271','','','','www.grupotranscargo.com.ar','','','','',1) 
INSERT INTO Proveedores (NombreFantasia,RazonSocial,IdCondicionIva,CUIT,IngresosBrutos,FechaInicioActividad,
			IdCondicionCompra,Direccion,IdProvincia,IdLocalidad,Telefono,Fax,Celular,MailEmpresa,Web,Contacto,MailContacto,
			CelularContacto,Observaciones,Activo)
VALUES ('GRACÍA GIRASOL','GARCIA EDUARDO DANIEL',1,'20-11759368-2','','01/08/1990',5,'DRUMOND 1558 D2',
        1,21592,'','','','','www.grupotranscargo.com.ar','','','','',1) 			


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace Prama
{
    public partial class frmComprasOrdenes : Form
    {

        #region Variables del formulario

        // Variable que almacena el Id de la cotización para cuando se hace el llamado desde las cotizaciones
        int IdCotizacion = 0;
        // Declaro la variable que va a almacenar las sentencias SQL
        string myCadenaSQL = "";
        // Declaro una bandera que va a controlar el comportamiento de la grilla
        bool BanderaCombo = false;
        // DEclaro una bandera que va a controlar si el proceso de intento de guardado es correcto
        bool BanderaGuardar = false;
        // Variable que almacena el tipo de comprobante Cotización
        int tipoComprobante = 2;
        // Declaro e instancio las clases que voy a usar en el formulario
        // Instancio un objeto de la clase comprobantes de compras para pasarle los datos
        clsComprobantesCompras myComprobanteNuevo = new clsComprobantesCompras();
        // Instancio un objeto de la clase comprobantes de compras para pasarle los datos
        clsComprobantesCompras myComprobanteViejo = new clsComprobantesCompras();
        // Instancio un objeto de la calse proveedores para pasarle los datos que me devuleve la consulta
        clsProveedores myProveedor = new clsProveedores();
        // Instancio un objeto de la clase Localidades
        clsLocalidades myLocalidad = new clsLocalidades();

        #endregion

        #region Constructor del formulario

        public frmComprasOrdenes(int IdCot)
        {
            // Paso a la variable el Id de la cotización que viene por parámetro
            IdCotizacion = IdCot;

            // Inicializo los controles del formulario
            InitializeComponent();
        }

        #endregion

        #region Métodos del Formulario

        #region Método que carga los toolsTip al formulario

        private void CargarToolsTip()
        {
            toolTip1.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip2.SetToolTip(this.btnCancelar, "Cancelar");
            toolTip3.SetToolTip(this.btnAgregarArt, "Agregar Artículo");
            toolTip4.SetToolTip(this.btnQuitarArt, "Quitar Artículo");
            toolTip5.SetToolTip(this.btnBuscar, "Buscar Proveedor");
        }

        #endregion

        #region Método que carga los combos del formulario

        private void CargarCombos()
        {
            // Cargo el combo de los puntos de venta
            clsDataBD.CargarCombo(cboPunto, "PuntosVentas", "PuntoVenta", "IdPuntoVenta", "Activo = 1");
            // Cargo el combo de los almacenes
            clsDataBD.CargarCombo(cboAlmacen, "Almacenes", "Almacen", "IdAlmacen", "Activo = 1");
            // Cargo el combo de las condiciones de compra
            clsDataBD.CargarCombo(cboCondCompra, "CondicionesCompra", "CondicionCompra", "IdCondicionCompra", "Activo = 1");
        }

        #endregion

        #region Método que carga los datos de la cotización

        private void CargarDatosCotizacion() // Pasa los datos de las tablas a las clases y después al formulario
        {
            // Cargo los datos de la cotización
            // Armo la cadena SQL
            myCadenaSQL = "select * from Vista_ComprobantesCompras where Id = " + IdCotizacion;
            // Creo una tabla que me va a almacenar el resultado de la consulta
            DataTable myTablaComprobantes = clsDataBD.GetSql(myCadenaSQL);
            // Recorro la tabla y le asigo los datos de la cotización al objeto myComprobante
            foreach (DataRow rowComp in myTablaComprobantes.Rows)
            {
                myComprobanteViejo.IdComprasCotizaciones = Convert.ToInt32(rowComp["Id"]);
                myComprobanteViejo.IdTipoComprobanteCompra = Convert.ToInt32(rowComp["IdTipo"]);
                myComprobanteViejo.IdProveedor = Convert.ToInt32(rowComp["IdProveedor"]);
                myComprobanteViejo.IdPuntoVenta = Convert.ToInt32(rowComp["IdPunto"]);
                myComprobanteViejo.IdAlmacen = Convert.ToInt32(rowComp["IdAlmacen"]);
                myComprobanteViejo.IdCondicionCompra = Convert.ToInt32(rowComp["IdCondicion"]);
                myComprobanteViejo.NumReferencia = (rowComp["NumReferencia"]).ToString();
                myComprobanteViejo.CantidadArticulos = Convert.ToInt32(rowComp["Cantidad"]);
                myComprobanteViejo.Fecha = (rowComp["Fecha"]).ToString();
                myComprobanteViejo.Numero = (rowComp["Numero"]).ToString();
                myComprobanteViejo.Vence = (rowComp["Vence"]).ToString();
                myComprobanteViejo.Neto = Convert.ToDouble(rowComp["Neto"]);
                myComprobanteViejo.Iva210 = Convert.ToDouble(rowComp["Iva"]);
                myComprobanteViejo.Total = Convert.ToDouble(rowComp["Total"]);
                myComprobanteViejo.Saldo = Convert.ToDouble(rowComp["Saldo"]);
                myComprobanteViejo.Activo = Convert.ToInt32(rowComp["Activo"]);

                myComprobanteViejo.PercepcionesIB = Convert.ToInt32(rowComp["PercepcionesIB"]);
                myComprobanteViejo.PercepcionesIva = Convert.ToInt32(rowComp["PercepcionesIva"]);
                myComprobanteViejo.PercepcionesVarias = Convert.ToInt32(rowComp["PercepcionesVarias"]);
                myComprobanteViejo.Exento = Convert.ToInt32(rowComp["NoGravados"]);
                myComprobanteViejo.Flete = Convert.ToInt32(rowComp["Flete"]);

            }
            // Busco los datos del proveedor en la tabla proveedores
            CargarProveedores(myComprobanteViejo.IdProveedor);

            // Paso los datos de las clases al formulario
            PasarDatosAlFormulario();
        }

        private void PasarDatosAlFormulario()
        {
            // Paso los datos del proveedor al formulario
            this.txtCodigoProv.Text = myProveedor.IdProveedor.ToString();
            this.txtRSoc.Text = myProveedor.RazonSocial;
            this.txtCUIT.Text = myProveedor.CUIT;
            this.txtCondicionIva.Text = myProveedor.CondicionIva;
            this.txtTel.Text = myProveedor.Telefono;
            this.txtFax.Text = myProveedor.Fax;
            this.txtDir.Text = myProveedor.Direccion;
            this.txtCp.Text = myProveedor.Cp;
            this.txtReferencia.Text = myComprobanteViejo.NumReferencia;
            this.txtLocalidad.Text = myProveedor.Localidad;
            this.txtProvincia.Text = myProveedor.Provincia;

            // si el Id de la catozación es = 0, cargo los puntos de venta´y almacén por defecto
            if (IdCotizacion == 0)
            {
                // El punto de venta
                cboPunto.SelectedValue = clsGlobales.cParametro.PtoVtaPorDefecto;
                // El almacén
                cboAlmacen.SelectedValue = clsGlobales.cParametro.AlmacenPorDefecto;
                // La condición de compra
                cboCondCompra.SelectedValue = myProveedor.IdCondicionCompra;
            }
            else
            {
                // El punto de venta
                cboPunto.SelectedValue = myComprobanteViejo.IdPuntoVenta;
                // El almacén
                cboAlmacen.SelectedValue = myComprobanteViejo.IdAlmacen;
                // La condición de compra
                cboCondCompra.SelectedValue = myComprobanteViejo.IdCondicionCompra;
            }

            // Cargo el detalle de la cotización a la grilla
            // Vacío la grilla
            dgvDetalleOrden.DataSource = null;
            // Evito que el dgvUsuarios genere columnas automáticas
            dgvDetalleOrden.AutoGenerateColumns = false;
            // Armo la cadena SQL para ejecutar el procedieminto almacenado que trae el detalle del comprobante
            string newMyCadenaSql = "exec ComprasDetalleComprobantes " + myComprobanteViejo.IdComprasCotizaciones + ", " + myComprobanteViejo.IdTipoComprobanteCompra;
            // Creo un datatable y le paso los datos de la consulta SQL
            DataTable myTabla = clsDataBD.GetSql(newMyCadenaSql);
            // Asigno a la grilla el source
            dgvDetalleOrden.DataSource = myTabla;
            // Número de Item para la grilla
            int fila = 1;
            // Cantidad de Insumos para el vector
            int insumos = 0;
            // Cantidad de productos para el vector
            int productos = 0;
            // Paso los datos a los vectores
            foreach (DataGridViewRow row in dgvDetalleOrden.Rows)
            {
                // Le asigno al campo Item de la grilla el valor de la variable
                row.Cells["Item"].Value = fila;
                // Si el artículo es un insumo, redimensiono la matriz y le paso el dato del Id del Artpiculo y la cantidad
                if (row.Cells["Tabla"].Value.ToString() == "INSUMOS")
                {
                    // Redimensiono el tamaño de la matriz de Insumos
                    clsGlobales.InsumosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.InsumosSeleccionados, new int[] { insumos + 1, 2 });
                    // A la posición creada le asigno el Id seleccionado y la cantidad cargada
                    clsGlobales.InsumosSeleccionados[insumos, 0] = Convert.ToDouble(row.Cells["IdArticulo"].Value);
                    clsGlobales.InsumosSeleccionados[insumos, 1] = Convert.ToDouble(row.Cells["Cantidad"].Value);
                    // Aumento el contador de insumos
                    insumos++;
                }
                else
                {
                    // Redimensiono el tamaño de la matriz de Insumos
                    clsGlobales.ProductosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.ProductosSeleccionados, new int[] { productos + 1, 2 });
                    // A la posición creada le asigno el Id seleccionado y la cantidad cargada
                    clsGlobales.ProductosSeleccionados[productos, 0] = Convert.ToDouble(row.Cells["IdArticulo"].Value);
                    clsGlobales.ProductosSeleccionados[productos, 1] = Convert.ToDouble(row.Cells["Cantidad"].Value);
                    // Aumento el contador de insumos
                    productos++;
                }

                fila++;
            }
            // Si la grilla tiene datos
            if (dgvDetalleOrden.RowCount > 0)
            {
                // Pongo el foco en la grilla y en el campo Cantidad
                dgvDetalleOrden.Focus();
                dgvDetalleOrden.CurrentCell = dgvDetalleOrden["Cantidad", dgvDetalleOrden.CurrentRow.Index];
                // Calculo el subtotal por fila de artículos
                CalculcarSubtotal();
                // Calculo el total del comprobante
                CalcularTotal();
            }
            
        }

        #endregion

        #region Método que deshabilita el reordenamiento de las grilla desde sus cabeceras

        private void DeshabilitarOrdenGrillas()
        {
            // Deshabilito la reordenación de las columnas de las grillas
            foreach (DataGridViewColumn dgvCol in dgvDetalleOrden.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;
                
            }
        }

        #endregion

        #region Método que calcula el subtotal de los artículos por fila

        private void CalculcarSubtotal()
        {
            // Variable que guarda el resultado de la multiplicación
            double subTotal = 0;
            // Recorro la grilla y hago el cálculo
            foreach (DataGridViewRow row in dgvDetalleOrden.Rows)
            {
                subTotal = Convert.ToDouble(row.Cells["Cantidad"].Value) * Convert.ToDouble(row.Cells["PrecioFinal"].Value);
                // Doy formato a las celdas de cantidad y precio
                row.Cells["Cantidad"].Value = Convert.ToDouble(row.Cells["Cantidad"].Value).ToString("0#.00");
                row.Cells["PrecioFinal"].Value = Convert.ToDouble(row.Cells["PrecioFinal"].Value).ToString("0#.00000");
                // Paso el dato a la grilla
                row.Cells["TotalArt"].Value = subTotal.ToString("0#.00");

            }
        }


        #endregion

        #region Método que calcula el Total del comprobante y enumera las filas

        private void CalcularTotal()
        {
            // Variable que va a almacenar la suma de los subtotales
            double TotalAcumulado = 0;
            // Vaiable que guarda el número de la fila
            int fila = 1;
            // Recorro la grilla y sumo los subtotales de lo artículos
            foreach (DataGridViewRow row in dgvDetalleOrden.Rows)
            {
                // Pongo el número del item
                row.Cells["Item"].Value = fila;
                // Acumulo los subtotales
                TotalAcumulado = TotalAcumulado + Convert.ToDouble(row.Cells["TotalArt"].Value);
                //Aumento el enumerador de los items
                fila++;

            }
            // Muestro el resultado en el textbox
            this.txtNeto.Text = TotalAcumulado.ToString("#0.00");
        }

        #endregion

        #region Método que inhabilita los controles del formulario

        private void InhabilitarControles()
        {
            this.txtCodigoProv.Enabled = false;
            this.txtComprador.Enabled = false;
            this.txtReferencia.Enabled = true;
            this.txtCondicionIva.Enabled = false;
            this.txtCp.Enabled = false;
            this.txtCUIT.Enabled = false;
            this.txtDir.Enabled = false;
            this.txtFax.Enabled = false;
            this.txtLocalidad.Enabled = false;
            this.txtNeto.Enabled = false;
            this.txtProvincia.Enabled = false;
            this.txtRSoc.Enabled = false;
            this.txtTel.Enabled = false;
        }

        #endregion

        #region Método que habilita los controles del formulario

        private void HabilitarControles()
        {
            // Habilito los campos de búsqueda
            this.txtCodigoProv.Enabled = false;
            this.txtRSoc.Enabled = false;
            this.txtCUIT.Enabled = false;
            // Dejo vacóa la selección de los combos
            cboAlmacen.SelectedIndex = -1;
            cboCondCompra.SelectedIndex = -1;
            cboPunto.SelectedIndex = -1;
            // Deshabilito el botón quitar articulos
            this.btnQuitarArt.Enabled = false;
        }

        #endregion

        #region Método que trae el proveedor para una nueva OC

        private void CargarProveedorNuevo()
        {
            // Si el vector tiene ,ás de un proveedor seleccionado
            if (clsGlobales.ProveedoresSeleccionados.GetLength(0) > 1)
            {
                // Informo que solo se puede seleccionar un proveedor
                MessageBox.Show("Solo puede seleccionar un proveedor", "VERIFICAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Vuelvo a abrir el formulario de búsqueda de proveedores
                // LLamo al formulario que busca los proveedores
                frmProveedoresBuscar myForm = new frmProveedoresBuscar(true, true);
                // Lo muestro
                myForm.ShowDialog();
            }
            // Si hay algún proveedor seleccionado
            if (clsGlobales.ProveedoresSeleccionados.GetLength(0) > 0)
            {
                // Recorro el vector hasta que encuentro un Id de proveedor y lo paso a los controles del formulario
                for (int i = 0; i <= clsGlobales.ProveedoresSeleccionados.GetLength(0); i++)
                {
                    // Si la posición tiene un ID de proveedor, busco los datos del mismo
                    if (clsGlobales.ProveedoresSeleccionados[0] > 0)
                    {
                        // Cargo los datos del proveedor
                        CargarProveedores(clsGlobales.ProveedoresSeleccionados[0]);
                        // Los paso al formulario
                        PasarDatosAlFormulario();
                    }
                }
            }
            
        }

        #endregion

        #region Método que carga los datos de los proveedores a la clase

        private void CargarProveedores(int Id)
        {
            // Armo la cadena SQL
            myCadenaSQL = "select * from Vista_Proveedores where IdProveedor = " + Id;
            // Creo una tabla que me va a almacenar el resultado de la consulta
            DataTable myTablaProveedores = clsDataBD.GetSql(myCadenaSQL);
            // Recorro la tabla y le asigo los datos del proveedor al objeto
            foreach (DataRow rowProv in myTablaProveedores.Rows)
            {
                // Primero los Int
                myProveedor.IdProveedor = Convert.ToInt32(rowProv["IdProveedor"]);
                myProveedor.IdCondicionIva = Convert.ToInt32(rowProv["IdCondicionIva"]);
                myProveedor.IdCondicionCompra = Convert.ToInt32(rowProv["IdCondicionCompra"]);
                myProveedor.IdProvincia = Convert.ToInt32(rowProv["IdProvincia"]);
                myProveedor.IdLocalidad = Convert.ToInt32(rowProv["IdLocalidad"]);
                myProveedor.ProvIns = Convert.ToInt32(rowProv["Insumos"]);
                myProveedor.ProvProd = Convert.ToInt32(rowProv["Productos"]);

                // Los string
                myProveedor.NombreFantasia = rowProv["NombreFantasia"].ToString();
                myProveedor.RazonSocial = rowProv["RazonSocial"].ToString();
                myProveedor.CondicionIva = rowProv["CondicionIva"].ToString();
                myProveedor.CUIT = rowProv["CUIT"].ToString();
                myProveedor.IngresosBrutos = rowProv["IngresosBrutos"].ToString();
                myProveedor.FechaInicioActividad = rowProv["FechaInicioActividad"].ToString();
                myProveedor.Direccion = rowProv["Direccion"].ToString();
                myProveedor.Localidad = rowProv["Localidad"].ToString();
                myProveedor.Provincia = rowProv["Provincia"].ToString();
                myProveedor.Cp = rowProv["CP"].ToString();
                myProveedor.Telefono = rowProv["Telefono"].ToString();
                myProveedor.Fax = rowProv["Fax"].ToString();
                myProveedor.Celular = rowProv["Celular"].ToString();
                myProveedor.MailEmpresa = rowProv["MailEmpresa"].ToString();
                myProveedor.Web = rowProv["Web"].ToString();
                myProveedor.Contacto = rowProv["Contacto"].ToString();
                myProveedor.MailContacto = rowProv["MailContacto"].ToString();
                myProveedor.CelularContacto = rowProv["CelularContacto"].ToString();
                myProveedor.Observaciones = rowProv["Observaciones"].ToString();
            }
        }

        #endregion

        #region Método que vacía los vectores globales para nuevo uso

        private void VaciarVectoresGlobales()
        {
            // Vacío de datos el vector de los proveedores
            clsGlobales.ProveedoresSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ProveedoresSeleccionados, 0);
            // Vacío de datos el vector de los Insumos
            clsGlobales.InsumosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.InsumosSeleccionados, new int[] { 0, 2 });
            // Vacío de datos el vector de los insumos
            clsGlobales.ProductosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.ProductosSeleccionados, new int[] { 0, 2 });
        }

        #endregion

        #region Método que graba las cotizaciones

        private void GrabarOrden()
        {
            // Verifico que los datos de las grilla estén completos
            bool controlGrillas = ValidarGrillas();
            // Si los datos de la grilla no son correctos, devuelvo el control al formulario saliendo del evento
            if (!(controlGrillas))
            {
                // Informo que faltan datos
                MessageBox.Show("Debe completar los datos para poder continuar!", "VERIFICAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Si es una nueva orden de compra
            if (IdCotizacion == 0)
            {
                // Grabo la nueva orden
                GrabarOrdenesNuevas();
            }
            // Si es una orden de una cotización
            else
            {
                // Se presinó el botón pedir desde los comprobantes de compra
                if (clsGlobales.bCompras)
                {
                    // Grabo la orden como si fuera nueva
                    GrabarOrdenesNuevas();
                }
                // Si no, es que se está editando la orden
                else
                {
                    GrabarOrdenEditada();
                }
                
            }

            // Creo una variable para guardar el singular o el plural del mensaje
            string numero = "";
            // Creo una variable que almacena si es un proveedor o son proveedores
            string prov = "";
            // Si tengo un solo proveedor
            if (clsGlobales.ProductosSeleccionados.Length == 1)
            {
                numero = "el ";
                prov = " Proveedor ?";
            }
            else
            {
                numero = "los ";
                prov = " Proveedores ?";
            }

            // Armo la cadena SQL para guardar los datos en la base

            /*
            // Muestro un mensaje preguntando si se desean enviar las cotizaciones por mail
            DialogResult respuestaImpresion = MessageBox.Show("Desea enviar por mail las solicitudes a " + numero + prov, "CONFIRMACIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Si decide enviarlas
            if (respuestaImpresion == DialogResult.Yes)
            {

            }
            // Si no desea enviarlas
            else
            {
                MessageBox.Show("No Aceptado");
            }
            */

            // Vacío los vectore para reusarlos
            VaciarVectoresGlobales();
            // Cambio el estado de la bandera de los formularios de compras
            clsGlobales.bCompras = false;
            // Cierro el formulario
            this.Close();
        }

        #endregion

        #region Método que verifica los datos de la grilla

        private bool ValidarGrillas()
        {
            // Variable que controla que los datos sean correctos en las grilla
            bool verificado = false;
            // Si hay proveedores cargados, verifico que haya artículos en la grilla
            if (dgvDetalleOrden.Rows.Count > 0)
            {
                // Controlo que los artículos de la grilla tengan cantidad
                foreach (DataGridViewRow row in dgvDetalleOrden.Rows)
                {
                    if (!(Convert.ToDouble(row.Cells["Cantidad"].Value) > 0) || !(Convert.ToDouble(row.Cells["PrecioFinal"].Value) > 0))
                    {
                        // Cambio el estado de la bandera 
                        verificado = false;
                    }
                    else
                    {
                        verificado = true;
                    }
                }
            }
            return verificado;
        }

        #endregion

        #region Método que graba las Ordenes nuevas

        private void GrabarOrdenesNuevas()
        {
            // Busco el próximo número a imprimir
            // Armo la cadena para buscar el número
            string myCadenaNumero = "select * from TiposComprobantesCompras where IdTipoComprobanteCompra = " + tipoComprobante;
            // Creo una tabla que me va a almacenar el resultado de la consulta
            DataTable myTabla = clsDataBD.GetSql(myCadenaNumero);
            // Creo la variable que va a almacenar el último número usado
            int proximoNumero = 0;
            // Recorro la tabla para almacenar el último numero usado y sumarle 1
            foreach (DataRow row in myTabla.Rows)
            {
                // Almaceno en la variable el número que corresponde a la próxima cotización
                proximoNumero = ((Convert.ToInt32(row["Numero"])) + 1);
            }
            // Paso los datos del formulario a la clase para grabarlos
            //myComprobanteNuevo.IdComprasCotizaciones = Convert.ToInt32(rowComp["Id"]);
            myComprobanteNuevo.IdTipoComprobanteCompra = tipoComprobante;
            myComprobanteNuevo.IdProveedor = myProveedor.IdProveedor;
            myComprobanteNuevo.IdPuntoVenta = Convert.ToInt32(cboPunto.SelectedValue);
            myComprobanteNuevo.IdAlmacen = Convert.ToInt32(cboAlmacen.SelectedValue);
            myComprobanteNuevo.IdCondicionCompra = Convert.ToInt32(cboCondCompra.SelectedValue);
            myComprobanteNuevo.NumReferencia = txtReferencia.Text;
            myComprobanteNuevo.CantidadArticulos = dgvDetalleOrden.RowCount;
            myComprobanteNuevo.Fecha = clsValida.ConvertirFecha(DateTime.Now);
            myComprobanteNuevo.Numero = clsValida.ConvertirNumeroComprobante(proximoNumero.ToString(), proximoNumero.ToString().Length);
            myComprobanteNuevo.Vence = clsValida.ConvertirFechaVencimiento("Compras", myComprobanteNuevo.IdCondicionCompra);
            myComprobanteNuevo.Neto = Convert.ToDouble(txtNeto.Text);
            myComprobanteNuevo.Iva210 = 0;
            myComprobanteNuevo.Total = 0;
            myComprobanteNuevo.Saldo = 0;
            myComprobanteNuevo.Activo = 1;
            myComprobanteNuevo.PercepcionesVarias = 0;
            myComprobanteNuevo.PercepcionesIva = 0;
            myComprobanteNuevo.PercepcionesIB = 0;
            myComprobanteNuevo.Exento = 0;
            myComprobanteNuevo.Flete = 0;
            // Armo la cadena SQL para grabar en la tabla Comprobantes de compras
            myCadenaSQL = "insert into ComprobantesCompras (IdTipoComprobanteCompra, IdProveedor, IdPuntoVenta, IdAlmacen, IdCondicionCompra," +
                                                                  "Fecha, Numero, Vence, CantidadArticulos, Neto, IVA, Total, Saldo, Activo," +
                                                                  "PercepcionesVarias,PercepcionesIva, PercepcionesIB, NoGravados, Flete) values(" +
                                                                  myComprobanteNuevo.IdTipoComprobanteCompra + ", " +
                                                                  myComprobanteNuevo.IdProveedor + ", " +
                                                                  myComprobanteNuevo.IdPuntoVenta + ", " +
                                                                  myComprobanteNuevo.IdAlmacen + ", " +
                                                                  myComprobanteNuevo.IdCondicionCompra + ", '" +
                                                                  myComprobanteNuevo.Fecha + "', '" +
                                                                  myComprobanteNuevo.Numero + "', '" +
                                                                  myComprobanteNuevo.Vence + "', " +
                                                                  myComprobanteNuevo.CantidadArticulos + ", " +
                                                                  myComprobanteNuevo.Neto + ", " +
                                                                  myComprobanteNuevo.Iva210 + ", " +
                                                                  myComprobanteNuevo.Total + ", " +
                                                                  myComprobanteNuevo.Saldo + ", " +
                                                                  myComprobanteNuevo.Activo +", " +
                                                                  myComprobanteNuevo.PercepcionesVarias + ", " +
                                                                  myComprobanteNuevo.PercepcionesIva+ ", " +
                                                                  myComprobanteNuevo.PercepcionesIB+ ", " +
                                                                  myComprobanteNuevo.Exento+ ", " +
                                                                  myComprobanteNuevo.Flete + ")";
            // Grabo en la tabla los datos del comprobante
            clsDataBD.GetSql(myCadenaSQL);
            // Grabo en la tabla tipos de comprobantes el número que usé
            string myCadenaNumeroTipoComprobante = "update TiposComprobantesCompras set Numero= " + proximoNumero + " where IdTipoComprobanteCompra= " + tipoComprobante;
            // Grabo en la tabla el número utilizado
            clsDataBD.GetSql(myCadenaNumeroTipoComprobante);
            // Grabo el detalle de la cotización
            GrabarDetalleOrdenesNuevas();
            
        }

        #endregion

        #region Método que graba las Ordenes editadas

        private void GrabarOrdenEditada()
        {
            // Paso los datos del formulario que pueden haber cambiada a la clase para grabarlos
            myComprobanteViejo.IdPuntoVenta = Convert.ToInt32(cboPunto.SelectedValue);
            myComprobanteViejo.IdAlmacen = Convert.ToInt32(cboAlmacen.SelectedValue);
            myComprobanteViejo.IdCondicionCompra = Convert.ToInt32(cboCondCompra.SelectedValue);
            myComprobanteViejo.NumReferencia = txtReferencia.Text;
            myComprobanteViejo.CantidadArticulos = dgvDetalleOrden.RowCount;
            myComprobanteViejo.Fecha = clsValida.ConvertirFecha(DateTime.Now);
            myComprobanteViejo.Vence = clsValida.ConvertirFechaVencimiento("Compras", myComprobanteViejo.IdCondicionCompra);
            myComprobanteViejo.Neto = Convert.ToDouble(txtNeto.Text);

            // Armo la cadena SQl para grabar los datos editados
            myCadenaSQL = "Update ComprobantesCompras set IdPuntoVenta = " + myComprobanteViejo.IdPuntoVenta +
                                                         ", IdAlmacen = " + myComprobanteViejo.IdAlmacen +
                                                         ", IdCondicionCompra = " + myComprobanteViejo.IdCondicionCompra +
                                                         ", Fecha = '" + myComprobanteViejo.Fecha +
                                                         "', Vence = '" + myComprobanteViejo.Vence +
                                                         "', NumReferencia = '" + myComprobanteViejo.NumReferencia +
                                                         "', CantidadArticulos = " + myComprobanteViejo.CantidadArticulos +
                                                         ", Neto = " + myComprobanteViejo.Neto +
                                                         " where IdComprobanteCompra = " + IdCotizacion;
            // Grabo en la tabla los datos del comprobante
            clsDataBD.GetSql(myCadenaSQL);
            // Grabo el nuevo detalle del comprobante
            GrabarDetalleOrdenesEditadas();

        }

        #endregion

        #region Método que graba el detalle de las ordenes nuevas

        private void GrabarDetalleOrdenesNuevas()
        {
            // Variable que almacena el Id del comrpbante
            int ultimoComprobante = 0;
            // Si es una nueva cotizacion busco el último número de id del comprobante
            if (IdCotizacion == 0 || clsGlobales.bCompras==true)
            {
                // Obtengo el último Id de la tabla comprobantes de compras del tipo cotización
                ultimoComprobante = clsDataBD.RetornarUltimoId("ComprobantesCompras", "IdComprobanteCompra");
            }
            // Si estoy editando uno, le paso el número del id del comprobante
            else
            {
                // Obtengo el último Id de la tabla comprobantes de compras del tipo cotización
                ultimoComprobante = IdCotizacion;
            }
            
            // Variable que guarda el Id del Artículo
            int IdArticulo = 0;
            // Variable que almacena la cantidad del artículo
            double CantidadArticulo = 0;
            // Variable que almacena el precio del artículo
            double PrecioArticulo = 0;
            // Variable que almacena el tipo de articulo
            string tipoArt = "";
            // Recorro la grilla de los artículos para grabar el detalle
            foreach (DataGridViewRow row in dgvDetalleOrden.Rows)
            {
                // Almaceno el id del artículo
                IdArticulo = Convert.ToInt32(row.Cells["IdArticulo"].Value);
                // Almaceno la cantidad del artículo
                CantidadArticulo = Convert.ToDouble(row.Cells["Cantidad"].Value);
                // Almaceno el precio del artículo
                PrecioArticulo = Convert.ToDouble(row.Cells["PrecioFinal"].Value);
                // Almaceno el tipo de artículo
                tipoArt = row.Cells["Tabla"].Value.ToString();
                // Armo la cadena SQL
                myCadenaSQL = "insert into DetallesComprobantesCompras (IdArticulo, IdComprobanteCompra, Cantidad, Precio, TipoArticulo, Activo) values (" +
                                IdArticulo + ", " +
                                ultimoComprobante + ", " +
                                CantidadArticulo + ", " +
                                PrecioArticulo + ", '" +
                                tipoArt + "',1)";
                // Grabo en la tabla
                clsDataBD.GetSql(myCadenaSQL);
            }
        }

        #endregion

        #region Método que graba el detalle de las órdenes editadas

        private void GrabarDetalleOrdenesEditadas()
        {
            // Elimino el detalle anterios
            myCadenaSQL = "delete from DetallesComprobantesCompras where IdComprobanteCompra = " + IdCotizacion;
            // Borro el detalle viejo en la tabla
            clsDataBD.GetSql(myCadenaSQL);
            // Grabo el nuevo detalle
            GrabarDetalleOrdenesNuevas();
        }

        #endregion

        #endregion

        #region Eventos del Formulario

        #region Evento Load del formulario

        private void frmComprasOrdenes_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            //ToolTips
            this.CargarToolsTip();
            // Cargo los combos con sus datos
            CargarCombos();
            // Pongo en su correspondiente textbox al comprador (Usuario logueado)
            txtComprador.Text = clsGlobales.UsuarioLogueado.Usuario;

            //Punto de compra / venta y Almacen N. 24-11
            this.cboPunto.SelectedValue = Convert.ToInt32(clsGlobales.cParametro.PtoVtaPorDefecto);
            this.cboAlmacen.SelectedValue = Convert.ToInt32(clsGlobales.cParametro.AlmacenPorDefecto);

            // Deshabilito el reordenamiento de las grillas por sus cabeceras
            DeshabilitarOrdenGrillas();
            // Dehabilito los controles del formulario
            InhabilitarControles();
            // Si el Id de la cotización no es 0, lo cargo
            if (!(IdCotizacion == 0))
            {
                // Llamo al método que carga los datos de la cotización
                CargarDatosCotizacion();
            }
            else
            {
                // Habilito los campos de búesqueda
                HabilitarControles();
               
            }
            this.Text = clsGlobales.cFormato.getTituloVentana() + " - ORDENES DE COMPRA";
        }

        #endregion

        #region Eventos de los botones

        #region Evento Click del botón btnAceptar

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Grabo la orden
            GrabarOrden();
            // Si se pudo guardar
            if (BanderaGuardar)
            {
                // Salgo del formulario
                btnCancelar.PerformClick();
            }

        }

        #endregion

        #region Evento Click del botón btnCancelar

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Vacío los vectores
            VaciarVectoresGlobales();
            // Cierro e formulario
            this.Close();
        }

        #endregion
        
        #region Evento Click del botón btnQuitarArt

        private void btnQuitarArt_Click(object sender, EventArgs e)
        {
            // Creo una variable que guarda el largo del vector de Ids
            int LargoInsumos = clsGlobales.InsumosSeleccionados.GetLength(0);
            int LargoProductos = clsGlobales.ProductosSeleccionados.GetLength(0);

            // Verifico si el articulo que voy a borrar es Insumo
            if (dgvDetalleOrden.CurrentRow.Cells["Tabla"].Value.ToString() == "INSUMOS")
            {
                // Recorro el vector
                for (int i = 0; i < LargoInsumos; i++)
                {
                    // Si el insumo que quiero borrar está en el vector
                    if (clsGlobales.InsumosSeleccionados[i, 0] == Convert.ToDouble(dgvDetalleOrden.CurrentRow.Cells["IdArticulo"].Value))
                    {
                        // Elimino el insumo del vector
                        clsGlobales.InsumosSeleccionados[i, 0] = 0;
                        // Elimino su cantidad
                        clsGlobales.InsumosSeleccionados[i, 1] = 0;
                        // Salgo del for
                        break;
                    }
                }
            }
            // Verifico si el articulo que voy a borrar es Producto
            if (dgvDetalleOrden.CurrentRow.Cells["Tabla"].Value.ToString() == "PRODUCTOS")
            {
                // Recorro el vector
                for (int i = 0; i < LargoProductos; i++)
                {
                    // Si el producto que quiero borrar está en el vector
                    if (clsGlobales.ProductosSeleccionados[i, 0] == Convert.ToDouble(dgvDetalleOrden.CurrentRow.Cells["IdArticulo"].Value))
                    {
                        // Elimino el prodcuto del vector
                        clsGlobales.ProductosSeleccionados[i, 0] = 0;
                        // Elimino su cantidad
                        clsGlobales.ProductosSeleccionados[i, 1] = 0;
                        // Salgo del for
                        break;
                    }
                }
            }

            // Elimino la fila de la grilla
            dgvDetalleOrden.Rows.RemoveAt(dgvDetalleOrden.CurrentRow.Index);
            // Si ya no quedan artículos en la grilla, secativo el botón de quitar artículos
            if (dgvDetalleOrden.RowCount == 0)
            {
                // Deshabilito el botón quitar
                btnQuitarArt.Enabled = false;
                // Deshabilito el botón aceptar
                btnAceptar.Enabled = false;
                // Pongo a 0 el valor del campo txtNeto
                this.txtNeto.Text = "0.00";
            }
            else
            {
                // Recalculo el total de la orden de compra
                CalcularTotal();
            }
        }

        #endregion

        #region Evento Click del botón btnAgregarArt

        private void btnAgregarArt_Click(object sender, EventArgs e)
        {
            // Verifico que se hayan elegido punto de compra y almacén para poder continuar
            if (cboPunto.SelectedIndex == -1 || cboAlmacen.SelectedIndex == -1 || cboCondCompra.SelectedIndex == -1)
            {
                // Informo que faltan los datos y devuelvo el control al usuario
                MessageBox.Show("Debe seleccionar Punto de Venta, Almacén y Condición de compra", "VERIFICAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Creo una variable que guarda el largo del vector de Ids
            int LargoInsumos = clsGlobales.InsumosSeleccionados.GetLength(0);
            int LargoProductos = clsGlobales.ProductosSeleccionados.GetLength(0);

            // Si la grilla tiene filas, cargo a las matrices las cantidades cargadas
            if (!(dgvDetalleOrden.RowCount == 0))
            {
                // Comienzo a recorrer la grilla
                foreach (DataGridViewRow row in dgvDetalleOrden.Rows)
                {
                    // Si el campo Cantidad no es nulo o 0, asigo el valor al elemento que corresponde en la matriz de ese elemento
                    if (!(row.Cells["Cantidad"].Value == null || Convert.ToDecimal(row.Cells["Cantidad"].Value) == 0))
                    {
                        // Si el artículo es de la tabla insumos
                        if (row.Cells["Tabla"].Value.Equals("INSUMOS"))
                        {
                            // Recorro la matriz y cuando el Id del artículo coincida, le paso la cantidad
                            for (int i = 0; i < LargoInsumos; i++)
                            {
                                // Si encuentro el Id de la fila en la matriz
                                if (Convert.ToDouble(row.Cells["IdArticulo"].Value) == clsGlobales.InsumosSeleccionados[i, 0])
                                {
                                    // Asigo en valor de cantidad del vector a la columna de la grilla
                                    clsGlobales.InsumosSeleccionados[i, 1] = Convert.ToDouble(row.Cells["Cantidad"].Value);
                                    // salgo del for
                                    break;
                                }
                            }
                        }

                        // Si el artículo es de la tabla insumos
                        if (row.Cells["Tabla"].Value.Equals("PRODUCTOS"))
                        {
                            // Recorro la matriz y cuando el Id del artículo coincida, le paso la cantidad
                            for (int i = 0; i < LargoProductos; i++)
                            {
                                // Si encuentro el Id de la fila en la matriz
                                if (Convert.ToDouble(row.Cells["IdArticulo"].Value) == clsGlobales.ProductosSeleccionados[i, 0])
                                {
                                    // Asigo en valor de cantidad del vector a la columna de la grilla
                                    clsGlobales.ProductosSeleccionados[i, 1] = Convert.ToDouble(row.Cells["Cantidad"].Value);
                                    // salgo del for
                                    break;
                                }
                            }
                        }

                    }

                }
            }
            // Vacío el source de la grilla
            dgvDetalleOrden.DataSource = null;

            // Creo un nuevo formulario de la clase y lo instancio
            frmArticulosBuscar myForm = new frmArticulosBuscar(Convert.ToBoolean(myProveedor.ProvIns), Convert.ToBoolean(myProveedor.ProvProd));
            // Muestro el formulario
            myForm.ShowDialog();
            // Creo una variable que va a almacenar los Id de los proveedores seleccionados
            string sArt = "";
            // Creo una variable que guarda el largo del vector de Ids
            LargoInsumos = clsGlobales.InsumosSeleccionados.GetLength(0);
            LargoProductos = clsGlobales.ProductosSeleccionados.GetLength(0);
            // Verifico el largo del vector con los Id para ver si tiene datos
            if (!(LargoInsumos == 0))
            {
                // Le agrego una como a la cadena para agregarles nuevos
                // sArt = sArt + ",";
                // Recorro el vector y le paso los datos a mi string de Ids
                for (int i = 0; i < LargoInsumos; i++)
                {
                    // Si no es el último
                    if (!(i == LargoInsumos - 1))
                    {
                        // Paso a string el Id del proveedor y lo ingreso a la cadena
                        sArt = sArt + clsGlobales.InsumosSeleccionados[i, 0].ToString() + ",";
                    }
                    // Si es el último
                    else
                    {
                        // Paso a string el Id del proveedor y lo ingreso a la cadena
                        sArt = sArt + clsGlobales.InsumosSeleccionados[i, 0].ToString();
                    }

                }

            }

            if (!(LargoProductos == 0))
            {
                // Pregunto si hay insumos en su vector
                if (LargoInsumos > 0)
                {
                    // Le agrego una como a la cadena para agregarles nuevos
                    sArt = sArt + ",";
                }

                // Recorro el vector y le paso los datos a mi string de Ids
                for (int i = 0; i < LargoProductos; i++)
                {
                    // Si no es el último
                    if (!(i == LargoProductos - 1))
                    {
                        // Paso a string el Id del proveedor y lo ingreso a la cadena
                        sArt = sArt + clsGlobales.ProductosSeleccionados[i, 0].ToString() + ",";
                    }
                    // Si es el último
                    else
                    {
                        // Paso a string el Id del proveedor y lo ingreso a la cadena
                        sArt = sArt + clsGlobales.ProductosSeleccionados[i, 0].ToString();
                    }

                }

            }

            // si hay cargados productos o insumos en los vectores
            if (!(clsGlobales.InsumosSeleccionados.Length == 0) || !(clsGlobales.ProductosSeleccionados.Length == 0))
            {
                // Armo la cadena SQL
                myCadenaSQL = "select * from Articulos_Insumos_Productos where IdArticulo in (" + sArt + ")";
                // Creo una tabla que me va a almacenar el resultado de la consulta
                DataTable myTabla = clsDataBD.GetSql(myCadenaSQL);
                // Evito que el dgv genere columnas automáticas
                dgvDetalleOrden.AutoGenerateColumns = false;
                // Asigno la tabla al source de la grilla de proveedores
                dgvDetalleOrden.DataSource = myTabla;

                // Habilito la carga de artículos
                if (dgvDetalleOrden.RowCount > 0 && !(Convert.ToInt32(cboPunto.SelectedValue) == -1) && !(Convert.ToInt32(cboAlmacen.SelectedValue) == -1) && !(Convert.ToInt32(cboCondCompra.SelectedValue) == -1))
                {
                    gpbArticulos.Enabled = true;
                }
                // Si la grilla tiene artículos
                if (dgvDetalleOrden.RowCount > 0)
                {
                    // Habilito el botón quitar
                    btnQuitarArt.Enabled = true;
                    // Creo un contador
                    int fila = 1;
                    // Recorro la grilla
                    foreach (DataGridViewRow row in dgvDetalleOrden.Rows)
                    {
                        // Asigno el valor del contador al Item de la fila
                        row.Cells["Item"].Value = fila;

                        // Si el artículo es de la tabla insumos
                        if (row.Cells["Tabla"].Value.Equals("INSUMOS"))
                        {
                            // Recorro el vector de insumos
                            for (int i = 0; i < LargoInsumos; i++)
                            {
                                // Si el Id del articulo coincide con el del vector, le cargo la cartidad
                                if (Convert.ToDouble(row.Cells["IdArticulo"].Value) == clsGlobales.InsumosSeleccionados[i, 0])
                                {
                                    // Asigo en valor de cantidad del vector a la columna de la grilla
                                    row.Cells["Cantidad"].Value = clsGlobales.InsumosSeleccionados[i, 1].ToString("#0.00");
                                    // salgo del for
                                    break;
                                }
                            }
                        }
                        // Si el artículo es de la tabla productos
                        if (row.Cells["Tabla"].Value.Equals("PRODUCTOS"))
                        {
                            // Recorro el vector de insumos
                            for (int i = 0; i < LargoProductos; i++)
                            {
                                // Si el Id del articulo coincide con el del vector, le cargo la cartidad
                                if (Convert.ToDouble(row.Cells["IdArticulo"].Value) == clsGlobales.ProductosSeleccionados[i, 0])
                                {
                                    // Asigo en valor de cantidad del vector a la columna de la grilla
                                    row.Cells["Cantidad"].Value = clsGlobales.ProductosSeleccionados[i, 1].ToString("#0.00");
                                    // salgo del for
                                    break;
                                }
                            }
                        }

                        // Incremento el contador
                        fila++;
                    }
                    // Habilito el botón aceptar
                    btnAceptar.Enabled = true;
                    // Paso el foco a la grilla
                    dgvDetalleOrden.Focus();
                    // Pongo el foco en el campo cantidad
                    dgvDetalleOrden.CurrentCell = dgvDetalleOrden["Cantidad", 0];
                }
            }
        }

        #endregion

        #region Evento Click del Boton Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Vacío el vector de los proveedores
            VaciarVectoresGlobales();
            // LLamo al formulario que busca los proveedores
            frmProveedoresBuscar myForm = new frmProveedoresBuscar(true, true);
            // Lo muestro
            myForm.ShowDialog();
            // Cargo los datos del proveedor en el formulario
            CargarProveedorNuevo();
        }

        #endregion

        #endregion

        #region Eventos DoubleClick de los campos de búsqueda de proveedor

        #region Evento DoubleClick del txtCodigoProv

        private void txtCodigoProv_DoubleClick(object sender, EventArgs e)
        {
            // Vacío el vector de los proveedores
            VaciarVectoresGlobales();
            // LLamo al formulario que busca los proveedores
            frmProveedoresBuscar myForm = new frmProveedoresBuscar(true, true);
            // Lo muestro
            myForm.ShowDialog();
            // Cargo los datos del proveedor en el formulario
            CargarProveedorNuevo();
        }

        #endregion

        #region Evento DoubleClick del txtRsoc

        private void txtRSoc_DoubleClick(object sender, EventArgs e)
        {
            // Vacío el vector de los proveedores
            VaciarVectoresGlobales();
            // LLamo al formulario que busca los proveedores
            frmProveedoresBuscar myForm = new frmProveedoresBuscar(true, true);
            // Lo muestro
            myForm.ShowDialog();
            // Cargo los datos del proveedor en el formulario
            CargarProveedorNuevo();
        }

        #endregion

        #region Evento DoubleClick del txtCuit

        private void txtCUIT_DoubleClick(object sender, EventArgs e)
        {
            // Vacío el vector de los proveedores
            VaciarVectoresGlobales();
            // LLamo al formulario que busca los proveedores
            frmProveedoresBuscar myForm = new frmProveedoresBuscar(true, true);
            // Lo muestro
            myForm.ShowDialog();
            // Cargo los datos del proveedor en el formulario
            CargarProveedorNuevo();
            // Inhabilito los controles
        }

        #endregion

        #endregion

        #region Eventos de la Grilla

        #region Evento SelectionChanged de la grilla

        private void dgvDetalleOrden_SelectionChanged(object sender, EventArgs e)
        {
            // Si la grilla tiene artículos
            if (dgvDetalleOrden.RowCount > 0)
            {
                // Almaceno én una variable la posición de fila en la que me encuentro
                int fila = dgvDetalleOrden.CurrentRow.Index;
                // Pongo el foco de la fila en la columna cantidad
                // dgvDetalleOrden.CurrentCell = dgvDetalleOrden["Cantidad", fila];
            }
        }

        #endregion

        #region Evento Click de la grilla

        private void dgvDetalleOrden_Click(object sender, EventArgs e)
        {
            // Cambio el estado de la bandera que controla la grilla
            BanderaCombo = true;

        }

        #endregion

        #region Evento CellEndEdit de la grilla

        private void dgvDetalleOrden_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Calculo el subtotal de la fila
            CalculcarSubtotal();
            // Calculo el total de la orden
            CalcularTotal();
        }

        #endregion

        #region Evento CellClick de la grilla

        private void dgvDetalleOrden_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Armo un switch para las columnas de la grilla
            switch (this.dgvDetalleOrden.Columns[this.dgvDetalleOrden.CurrentCell.ColumnIndex].Name)
            {
                // Para todas las columnas menos para cantidad y pecio
                case "Item":
                case "Codigo":
                case "CodigoProveedor":
                case "Articulo":
                case "Unidad":
                case "TotalArt":
                    // Pongo el foco en la columna Canidad    
                    this.dgvDetalleOrden.CurrentCell = dgvDetalleOrden["Cantidad", dgvDetalleOrden.CurrentRow.Index];
                    // Salgo del switch
                    break;
            }
        }

        #endregion

        #region Evento KeyUp de la grilla

        private void dgvDetalleOrden_KeyUp(object sender, KeyEventArgs e)
        {
            // Armo un switch para las columnas de la grilla
            switch (this.dgvDetalleOrden.Columns[this.dgvDetalleOrden.CurrentCell.ColumnIndex].Name)
            {
                // Para todas las columnas menos para cantidad y pecio
                case "Item":
                case "Codigo":
                case "CodigoProveedor":
                case "Articulo":
                case "Unidad":
                case "TotalArt":
                    // Pongo el foco en la columna Canidad       
                    this.dgvDetalleOrden.CurrentCell = dgvDetalleOrden["Cantidad", dgvDetalleOrden.CurrentRow.Index];
                    // Salgo del switch
                    break;
            }
        }

        #endregion

        

        #endregion

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prama
{
    public partial class frmComprasCotizaciones : Form
    {
        
        #region Variables de formulario

        // Variable que almacena las cadenas SQL del formulario
        string myCadenaSQL = "";
        // Variable que almacena el tipo de comprobante Cotización
        int tipoComprobante = 1;
        // Variable que almacena si es un alta o una baja de cotizaciones
        int IdCotizacion;
        // Instancio un nuevo objeto de la clase Comprobantes de compras para las nuevas cotizaciones
        clsComprobantesCompras myCotizacionNueva = new clsComprobantesCompras();
        // Instancio un nuevo objeto de la clase Comprobantes de compras para las ediciones de cotizaciones
        clsComprobantesCompras myCotizacionVieja = new clsComprobantesCompras();
        // Instancio un objeto de la calse proveedores para pasarle los datos que me devuleve la consulta
        clsProveedores myProveedor = new clsProveedores();

        #endregion

        #region Constructor del formulario

        public frmComprasCotizaciones(int IdCot)
        {
            // Almaceno en la variable del formulario el tipo de movimiento del form
            IdCotizacion = IdCot;
            // Inicializo los componentes del formulario
            InitializeComponent();
        }

        #endregion

        #region Métodos del Formulario

        #region Método que carga los combos del formulario

        private void CargarCombos()
        {
            // Cargo el combo de los puntos de venta
            clsDataBD.CargarCombo(cboPunto, "PuntosVentas", "PuntoVenta", "IdPuntoVenta", "Activo = 1");
            // Dejo vacía la selección
            cboPunto.SelectedIndex = -1;
            // Cargo el combo de los almacenes
            clsDataBD.CargarCombo(cboAlmacen, "Almacenes", "Almacen", "IdAlmacen", "Activo = 1");
            // Dejo vacía la selección
            cboAlmacen.SelectedIndex = -1;

        }

        #endregion

        #region Método que verifica los datos de la grilla

        private bool ValidarGrillas()
        {
            // Variable que controla que los datos sean correctos en las grilla
            bool verificado = false;
            // Verifico si la grilla de los proveedores tiene datos
            if (dgvProveedores.Rows.Count > 0)
            {
                // Cambio el estado de la bandera
                verificado = true;
            }
            // Si hay proveedores cargados, verifico que haya artículos en la grilla
            if (verificado == true && dgvArticulos.Rows.Count > 0)
            {
                // Controlo que los artículos de la grilla tengan cantidad
                foreach (DataGridViewRow row in dgvArticulos.Rows)
                {
                    if (!(Convert.ToDouble(row.Cells["Cantidad"].Value) > 0))
                    {
                        // Cambio el estado de la bandera 
                        verificado = false;
                    }
                }
            }
            return verificado;
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

        #region Método que deshabilita el reordenamiento de las grilla desde sus cabeceras

        private void DeshabilitarOrdenGrillas()
        {
            // Deshabilito la reordenación de las columnas de las grillas
            foreach (DataGridViewColumn dgvCol in dgvProveedores.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn dgvCol in dgvArticulos.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        #endregion

        #region Método que graba las solicitudes de cotización nuevas

        private void GrabarCotizacionesNuevas()
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
            
            // Paso a la propiedades los valores que son iguales para todos los proveedores
            myCotizacionNueva.IdTipoComprobanteCompra = tipoComprobante;
            myCotizacionNueva.IdPuntoVenta = Convert.ToInt32(cboPunto.SelectedValue);
            myCotizacionNueva.IdAlmacen = Convert.ToInt32(cboAlmacen.SelectedValue);
            myCotizacionNueva.Fecha = clsValida.ConvertirFecha(DateTime.Now);
            myCotizacionNueva.CantidadArticulos = dgvArticulos.Rows.Count;
            myCotizacionNueva.Activo = 1;
            myCotizacionNueva.PercepcionesIB = 0;
            myCotizacionNueva.PercepcionesIva = 0;
            myCotizacionNueva.PercepcionesVarias = 0;
            myCotizacionNueva.Exento = 0;
            myCotizacionNueva.Flete = 0;
            // Recorro la grilla de proveedores para completar los datos de cada presupuesto
            foreach (DataGridViewRow row in dgvProveedores.Rows)
            {
                // Convierto el número de la cotización a string para la base de datos
                myCotizacionNueva.Numero = clsValida.ConvertirNumeroComprobante(proximoNumero.ToString(),proximoNumero.ToString().Length);
                // Asigno el Id del proveedor
                myCotizacionNueva.IdProveedor = Convert.ToInt32(row.Cells["IdProveedor"].Value);
                // Asigno el Id de la condición de compra
                myCotizacionNueva.IdCondicionCompra = Convert.ToInt32(row.Cells["IdCondicionCompra"].Value);
                // Calculo la fecha de vencimiento para cada proveedor
                myCotizacionNueva.Vence = clsValida.ConvertirFechaVencimiento("Compras",myCotizacionNueva.IdCondicionCompra);
                // Armo la cadena SQL para grabar en la tabla Comprobantes de compras
                myCadenaSQL = "insert into ComprobantesCompras (IdTipoComprobanteCompra, IdProveedor, IdPuntoVenta, IdAlmacen, IdCondicionCompra," +
                                                                "Fecha, Numero, Vence, CantidadArticulos, Neto, IVA, Total, Saldo, Activo, PercepcionesIB, " +
                                                                "PercepcionesIva, PercepcionesVarias, NoGravados, Flete) values(" +
                                                                      myCotizacionNueva.IdTipoComprobanteCompra + ", " +
                                                                      myCotizacionNueva.IdProveedor + ", " +
                                                                      myCotizacionNueva.IdPuntoVenta + ", " +
                                                                      myCotizacionNueva.IdAlmacen + ", " +
                                                                      myCotizacionNueva.IdCondicionCompra + ", '" +
                                                                      myCotizacionNueva.Fecha + "', '" +
                                                                      myCotizacionNueva.Numero + "', '" +
                                                                      myCotizacionNueva.Vence + "', " +
                                                                      myCotizacionNueva.CantidadArticulos + ", " +
                                                                      myCotizacionNueva.Neto + ", " +
                                                                      myCotizacionNueva.Iva210 + ", " +
                                                                      myCotizacionNueva.Total + ", " +
                                                                      myCotizacionNueva.Saldo + ", " +
                                                                      myCotizacionNueva.Activo + ", " +
                                                                      myCotizacionNueva.PercepcionesIB + ", " +
                                                                      myCotizacionNueva.PercepcionesIva + ", " +
                                                                      myCotizacionNueva.PercepcionesVarias + ", " +
                                                                      myCotizacionNueva.Exento + ", " +
                                                                      myCotizacionNueva.Flete + ")";
                // Grabo en la tabla los datos del comprobante
                clsDataBD.GetSql(myCadenaSQL);
                // Grabo en la tabla tipos de comprobantes el número que usé
                string myCadenaNumeroTipoComprobante = "update TiposComprobantesCompras set Numero= " + proximoNumero + " where IdTipoComprobanteCompra= " + tipoComprobante;
                // Grabo en la tabla el número utilizado
                clsDataBD.GetSql(myCadenaNumeroTipoComprobante);
                // Grabo el detalle de la cotización
                GrabarDetalleCotizacionesNuevas();
                // Incremento en 1 el número del comprobante
                proximoNumero++;
            }
        }

        #endregion

        #region Método que graba las solicitudes de cotización Editadas

        private void GrabarCotizacionesEditadas()
        {
            // Paso a la clase los datos del formulario que cambian para actualizarlo
            myCotizacionVieja.IdPuntoVenta = Convert.ToInt32(cboPunto.SelectedValue);
            myCotizacionVieja.IdAlmacen = Convert.ToInt32(cboAlmacen.SelectedValue);
            myCotizacionVieja.Fecha = clsValida.ConvertirFecha(DateTime.Now);
            myCotizacionVieja.Vence = clsValida.ConvertirFechaVencimiento("Compras", myCotizacionVieja.IdCondicionCompra);
            myCotizacionVieja.CantidadArticulos = dgvArticulos.Rows.Count;

            // Armo la cadena SQL para hacer el update a la tabla
            myCadenaSQL = "Update ComprobantesCompras set IdPuntoVenta = " + myCotizacionVieja.IdPuntoVenta + ", " +
                                                     "IdAlmacen = " + myCotizacionVieja.IdAlmacen + ", " +
                                                     "Fecha = '" + myCotizacionVieja.Fecha + "', " +
                                                     "Vence = '" + myCotizacionVieja.Vence + "', " +
                                                     "CantidadArticulos = " + myCotizacionVieja.CantidadArticulos +
                                                     " where IdComprobanteCompra = " + myCotizacionVieja.IdComprasCotizaciones;
            // Grabo las modificaciones del comprobante en la tabla
            clsDataBD.GetSql(myCadenaSQL);
            // Grabo el detalle de la cotización
            GrabarDetalleCotizacionesEditadas();
        }

        #endregion

        #region Método que graba el detalle de las cotizaciones nuevas

        private void GrabarDetalleCotizacionesNuevas()
        {
            int ultimoComprobante = 0;
            if (IdCotizacion == 0)
            {
                // Obtengo el último Id de la tabla comprobantes de compras del tipo cotización
                ultimoComprobante = clsDataBD.RetornarUltimoId("ComprobantesCompras", "IdComprobanteCompra");
            }
            else
            {
                ultimoComprobante = myCotizacionVieja.IdComprasCotizaciones;

            }
            // Obtengo el último Id de la tabla comprobantes de compras del tipo cotización
            
            // Variable que guarda el Id del Artículo
            int IdArticulo = 0;
            // Variable que almacena la cantidad del artículo
            double CantidadArticulo = 0;
            // Variable que almacena el precio del artículo
            double PrecioArt = 0;
            // Variable que almacena el tipo de articulo
            string tipoArt = "";
            // Recorro la grilla de los artículos para grabar el detalle
            foreach (DataGridViewRow row in dgvArticulos.Rows)
            {
                // Almaceno el id del artículo
                IdArticulo = Convert.ToInt32(row.Cells["IdArticulo"].Value);
                // Almaceno la cantidad del artículo
                CantidadArticulo = Convert.ToDouble(row.Cells["Cantidad"].Value);
                // Almaceno el precio del artículo
                PrecioArt = Convert.ToDouble(row.Cells["Precio"].Value);
                // Almaceno el tipo de artículo
                tipoArt = row.Cells["Tabla"].Value.ToString();
                // Armo la cadena SQL
                myCadenaSQL = "insert into DetallesComprobantesCompras (IdArticulo, IdComprobanteCompra, Cantidad, Precio, TipoArticulo, Activo) values (" +
                                IdArticulo + ", " +
                                ultimoComprobante + ", " +
                                CantidadArticulo + ", " +
                                PrecioArt + ", '" +
                                tipoArt + "',1)";
                // Grabo en la tabla
                clsDataBD.GetSql(myCadenaSQL);
            }
        }

        #endregion

        #region Método que graba el detalle de las cotizaciones nuevas

        private void GrabarDetalleCotizacionesEditadas()
        {
            // Elimino primero los datos del detalle anterior
            myCadenaSQL = "delete DetallesComprobantesCompras where IdComprobanteCompra = " + IdCotizacion;
            // Elimino los registros viejos
            clsDataBD.GetSql(myCadenaSQL);
            // Grabo el nuevo detalle
            GrabarDetalleCotizacionesNuevas();
        }

        #endregion

        #region Método que CargarToolTips

        private void CargarToolTips()
        {
            toolTip1.SetToolTip(this.btnAgregarProv, "Agregar Proveedor");
            toolTip2.SetToolTip(this.btnQuitarProv, "Quitar Proveedor");
            toolTip3.SetToolTip(this.btnAceptar, "Grabar");
            toolTip4.SetToolTip(this.btnCancelar, "Cancelar");
            toolTip5.SetToolTip(this.btnAgregarArt, "Agregar Artículo");
            toolTip6.SetToolTip(this.btnQuitarArt, "Quitar Artículo");
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
                myCotizacionVieja.IdComprasCotizaciones = Convert.ToInt32(rowComp["Id"]);
                myCotizacionVieja.IdTipoComprobanteCompra = Convert.ToInt32(rowComp["IdTipo"]);
                myCotizacionVieja.IdProveedor = Convert.ToInt32(rowComp["IdProveedor"]);
                myCotizacionVieja.IdPuntoVenta = Convert.ToInt32(rowComp["IdPunto"]);
                myCotizacionVieja.IdAlmacen = Convert.ToInt32(rowComp["IdAlmacen"]);
                myCotizacionVieja.IdCondicionCompra = Convert.ToInt32(rowComp["IdCondicion"]);
                myCotizacionVieja.NumReferencia = (rowComp["NumReferencia"]).ToString();
                myCotizacionVieja.CantidadArticulos = Convert.ToInt32(rowComp["Cantidad"]);
                myCotizacionVieja.Fecha = (rowComp["Fecha"]).ToString();
                myCotizacionVieja.Numero = (rowComp["Numero"]).ToString();
                myCotizacionVieja.Vence = (rowComp["Vence"]).ToString();
                myCotizacionVieja.Neto = Convert.ToDouble(rowComp["Neto"]);
                myCotizacionVieja.Iva210 = Convert.ToDouble(rowComp["Iva"]);
                myCotizacionVieja.Total = Convert.ToDouble(rowComp["Total"]);
                myCotizacionVieja.Saldo = Convert.ToDouble(rowComp["Saldo"]);
                myCotizacionVieja.Activo = Convert.ToInt32(rowComp["Activo"]);
            }

            
            
            // Paso los datos al formulario
            PasarDatosAlFormulario();
        }

        private void PasarDatosAlFormulario()
        {
            // Deshabilito los checkbox y los marco
            this.chkInsumos.Visible = false;
            this.chkProductos.Visible = false;
            this.chkInsumos.Checked = true;
            this.chkProductos.Checked = false;
            // Habilito el grupo de los artículos
            this.gpbArticulos.Enabled = true;
            // Habilito los botones agregar artículo y quitar artículo            
            this.btnAgregarArt.Enabled = true;
            if (this.dgvArticulos.Rows.Count > 0)
            {
                this.btnQuitarArt.Enabled = true;
                this.btnAceptar.Enabled = true;
            }
            // Cargo los combos
            // El punto de venta
            cboPunto.SelectedValue = myCotizacionVieja.IdPuntoVenta;
            // El almacén
            cboAlmacen.SelectedValue = myCotizacionVieja.IdAlmacen;
            // Busco los datos del proveedor en la tabla proveedores
            // Armo la cadena SQL
            myCadenaSQL = "select * from Vista_Proveedores where IdProveedor = " + myCotizacionVieja.IdProveedor;
            // Creo una tabla que me va a almacenar el resultado de la consulta
            DataTable myTablaProveedores = clsDataBD.GetSql(myCadenaSQL);
            dgvProveedores.DataSource = null;
            // Evito que el dgvUsuarios genere columnas automáticas
            dgvProveedores.AutoGenerateColumns = false;
            // Armo la cadena SQL para ejecutar el procedieminto almacenado que trae el detalle del comprobante
            // Asigno a la grilla el source
            dgvProveedores.DataSource = myTablaProveedores;

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

            // Cargo el detalle de la cotización a la grilla
            // Vacío la grilla
            dgvArticulos.DataSource = null;
            // Evito que el dgvUsuarios genere columnas automáticas
            dgvArticulos.AutoGenerateColumns = false;
            // Armo la cadena SQL para ejecutar el procedieminto almacenado que trae el detalle del comprobante
            string newMyCadenaSql = "exec ComprasDetalleComprobantes " + myCotizacionVieja.IdComprasCotizaciones + ", " + myCotizacionVieja.IdTipoComprobanteCompra;
            // Creo un datatable y le paso los datos de la consulta SQL
            DataTable myTabla = clsDataBD.GetSql(newMyCadenaSql);
            // Asigno a la grilla el source
            dgvArticulos.DataSource = myTabla;
            // Número de Item para la grilla
            int fila = 1;
            // Cantidad de Insumos para el vector
            int insumos = 0;
            // Cantidad de productos para el vector
            int productos = 0;
            // Recorro la grilla para cargar el item
            foreach (DataGridViewRow row in dgvArticulos.Rows)
            {
                // Le asigno al campo Item de la grilla el valor de la variable
                row.Cells["Item"].Value = fila;
                // Si el artículo es un insumo, redimensiono la matriz y le paso el dato del Id del Artpiculo y la cantidad
                if (row.Cells["Tabla"].Value.ToString() == "INSUMOS")
                {
                    // Redimensiono el tamaño de la matriz de Insumos
                    clsGlobales.InsumosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.InsumosSeleccionados, new int[] {insumos + 1, 2 });
                    // A la posición creada le asigno el Id seleccionado y la cantidad cargada
                    clsGlobales.InsumosSeleccionados[insumos, 0] = Convert.ToDouble(row.Cells["IdArticulo"].Value);
                    clsGlobales.InsumosSeleccionados[insumos, 1] = Convert.ToDouble(row.Cells["Cantidad"].Value);
                    // Aumento el contador de insumos
                    insumos++;
                }
                else
                {
                    // Redimensiono el tamaño de la matriz de Insumos
                    clsGlobales.ProductosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.ProductosSeleccionados, new int[] {productos + 1, 2 });
                    // A la posición creada le asigno el Id seleccionado y la cantidad cargada
                    clsGlobales.ProductosSeleccionados[productos, 0] = Convert.ToDouble(row.Cells["IdArticulo"].Value);
                    clsGlobales.ProductosSeleccionados[productos, 1] = Convert.ToDouble(row.Cells["Cantidad"].Value);
                    // Aumento el contador de insumos
                    productos++;
                }
                
                fila++;
            }

            //Verificar si hay articulos cargados, habilitar boton aceptar
            if (this.dgvArticulos.Rows.Count > 0)
            {
                this.btnAceptar.Enabled = true;
                this.btnQuitarArt.Enabled = true;
            }
            
        }

        #endregion

        #endregion

        #region Eventos del Formulario

        #region Evento Click del botón Cancelar

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Vacío los vectores globales para un nuevo uso
            VaciarVectoresGlobales();
            
            // Cierro el formulario
            this.Close();
        }

        #endregion

        #region Evento Load del formulario

        private void frmComprasCotizaciones_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            // Cargo los combos con sus datos
            CargarCombos();

            //Punto de compra / venta y Almacen segun parametros N. 24-11-2016 
                this.cboPunto.SelectedValue = Convert.ToInt32(clsGlobales.cParametro.PtoVtaPorDefecto);
                this.cboAlmacen.SelectedValue = Convert.ToInt32(clsGlobales.cParametro.AlmacenPorDefecto);
            //

            // Cargo los toolsTip
            CargarToolTips();
            // Pongo en su correspondiente textbox al comprador (Usuario logueado)
            txtComprador.Text = clsGlobales.UsuarioLogueado.Usuario;
            // Deshabilito el reordenamiento de las grillas por sus cabeceras
            DeshabilitarOrdenGrillas();
            // Si el Id de la cotización que viene por parámetro no es 0, estoy modificando una cotización
            if (!(IdCotizacion == 0))
            {
                // Llamo al método que carga la cotización al formulario
                CargarDatosCotizacion();
                // Habilito el botón aceptar por si solo qurían ver la cotización
                this.btnAceptar.Enabled = true;
            }

            this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;
        }

        #endregion

        #region Evento Click del botón Agregar Proveedores

        private void btnAgregarProv_Click(object sender, EventArgs e)
        {
            bool bInsumosChk = chkInsumos.Checked;
            bool bProductos = chkProductos.Checked;
            // Creo un nuevo formulario de la clase y lo instancio
            frmProveedoresBuscar myForm = new frmProveedoresBuscar(bInsumosChk,bProductos);
            // Muestro el formulario
            myForm.ShowDialog();
            // Creo una variable que va a almacenar los Id de los proveedores seleccionados
            string sProv = "";
            // Creo una variable que guarda el largo del vector de Ids
            int Largo = clsGlobales.ProveedoresSeleccionados.Length;
            // Verifico el largo del vector con los Id para ver si tiene datos
            if (!(Largo == 0))
            {
                // Recorro el vector y le paso los datos a mi string de Ids
                for (int i = 0; i < Largo; i++)
                {
                    // Si no es el último
                    if (!(i == Largo - 1))
                    {
                        // Paso a string el Id del proveedor y lo ingreso a la cadena
                        sProv = sProv + clsGlobales.ProveedoresSeleccionados[i].ToString() + ",";
                    }
                    // Si es el último
                    else
                    {
                        // Paso a string el Id del proveedor y lo ingreso a la cadena
                        sProv = sProv + clsGlobales.ProveedoresSeleccionados[i].ToString();
                    }
                }

                // Armo la cadena SQL
                myCadenaSQL = "select * from Vista_Proveedores where IdProveedor in (" + sProv + ")";
                // Creo una tabla que me va a almacenar el resultado de la consulta
                DataTable myTabla = clsDataBD.GetSql(myCadenaSQL);
                // Evito que el dgv genere columnas automáticas
                dgvProveedores.AutoGenerateColumns = false;
                // Asigno la tabla al source de la grilla de proveedores
                dgvProveedores.DataSource = myTabla;
                // Habilito la carga de artículos
                if (dgvProveedores.RowCount > 0 && !(Convert.ToInt32(cboPunto.SelectedValue)==-1) && !(Convert.ToInt32(cboAlmacen.SelectedValue)==-1))
                {
                    gpbArticulos.Enabled = true;
                }
                // Si hay proveedores en la grilla y el botón quitar proveedores está desativado lo activo
                if (!(dgvProveedores.RowCount == 0) && btnQuitarProv.Enabled == false)
                {
                    btnQuitarProv.Enabled = true;
                }

            }
        }

        #endregion

        #region Eventos CheckedChanged de los CheckBox del formulario

        private void chkInsumos_CheckedChanged(object sender, EventArgs e)
        {
            // Al cambiar el tipo de proveedor vacío la grilla de los proveedores y el vector
            dgvProveedores.DataSource = null;
            // Vacío la grilla de artículos
            dgvArticulos.DataSource = null;
            // Vacío los vectores
            VaciarVectoresGlobales();
            
            // Si hay algún chk marcado habilito el grupo de proveedores
            if (chkInsumos.Checked || chkProductos.Checked)
            {
                gpbProveedores.Enabled = true;
            }
            else
            {
                gpbProveedores.Enabled = false;
            }
        }

        private void chkProductos_CheckedChanged(object sender, EventArgs e)
        {
            // Al cambiar el tipo de proveedor vacío la grilla de los proveedores
            dgvProveedores.DataSource = null;
            // Vacío la grilla de artículos
            dgvArticulos.DataSource = null;
            // Vacío los vectores
            VaciarVectoresGlobales();
            
            // Si hay algún chk marcado habilito el grupo de proveedores
            if (chkInsumos.Checked || chkProductos.Checked)
            {
                gpbProveedores.Enabled = true;
            }
            else
            {
                gpbProveedores.Enabled = false;
            }
        }

        #endregion

        #region Evento Click del botón Quitar Proveedor

        private void btnQuitarProv_Click(object sender, EventArgs e)
        {
            // Recorro el vector
            for (int i = 0; i < clsGlobales.ProveedoresSeleccionados.Length; i++)
            {
                // Si el proveedor que quiero borrar está en el vector
                if (clsGlobales.ProveedoresSeleccionados[i] == Convert.ToInt32(dgvProveedores.CurrentRow.Cells["IdProveedor"].Value))
                {
                    // Elimino el proveedor del vector
                    clsGlobales.ProveedoresSeleccionados[i] = 0;
                    // Salgo del for
                    break;
                }
            }
            // Elimino la fila de la grilla
            dgvProveedores.Rows.RemoveAt(dgvProveedores.CurrentRow.Index);
            // Si ya no quedan proveedores en la grilla, desactivo el botón quitar proveedores
            if (dgvProveedores.Rows.Count == 0)
            {
                this.btnQuitarProv.Enabled = false;
                this.gpbArticulos.Enabled = false;
                this.btnAceptar.Enabled = false;
            }
            
        }

        #endregion

        #region Evento Click del botón Agregar Artículo

        private void btnAgregarArt_Click(object sender, EventArgs e)
        {
            // Verifico que se hayan elegido punto de compra y almacén para poder continuar
            if (cboPunto.SelectedIndex == -1 || cboAlmacen.SelectedIndex == -1)
            {
                // Informo que faltan los datos y devuelvo el control al usuario
                MessageBox.Show("Debe seleccionar Punto de Venta y Almacén", "VERIFICAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool bInsumosChk = chkInsumos.Checked;
            bool bProductos = chkProductos.Checked;

            // Creo una variable que guarda el largo del vector de Ids
            int LargoInsumos = clsGlobales.InsumosSeleccionados.GetLength(0);
            int LargoProductos = clsGlobales.ProductosSeleccionados.GetLength(0);

            // Si la grilla tiene filas, cargo a las matrices las cantidades cargadas
            if (!(dgvArticulos.RowCount == 0))
            {
                // Comienzo a recorrer la grilla
                foreach (DataGridViewRow row in dgvArticulos.Rows)
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
            dgvArticulos.DataSource = null;
            if (!(IdCotizacion == 0))
            {
                // Creo un nuevo formulario de la clase y lo instancio
                frmArticulosBuscar myForm = new frmArticulosBuscar(Convert.ToBoolean(myProveedor.ProvIns), Convert.ToBoolean(myProveedor.ProvProd));
                // Muestro el formulario
                myForm.ShowDialog();
            }
            else
            {
                // Creo un nuevo formulario de la clase y lo instancio
                frmArticulosBuscar myForm = new frmArticulosBuscar(bInsumosChk, bProductos);
                // Muestro el formulario
                myForm.ShowDialog();
            }
            
            
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
                        sArt = sArt + clsGlobales.InsumosSeleccionados[i,0].ToString() + ",";
                    }
                    // Si es el último
                    else
                    {
                        // Paso a string el Id del proveedor y lo ingreso a la cadena
                        sArt = sArt + clsGlobales.InsumosSeleccionados[i,0].ToString();
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
                        sArt = sArt + clsGlobales.ProductosSeleccionados[i,0].ToString() + ",";
                    }
                    // Si es el último
                    else
                    {
                        // Paso a string el Id del proveedor y lo ingreso a la cadena
                        sArt = sArt + clsGlobales.ProductosSeleccionados[i,0].ToString();
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
                dgvArticulos.AutoGenerateColumns = false;
                // Asigno la tabla al source de la grilla de proveedores
                dgvArticulos.DataSource = myTabla;

                // Habilito la carga de artículos
                if (dgvArticulos.RowCount > 0 && !(Convert.ToInt32(cboPunto.SelectedValue) == -1) && !(Convert.ToInt32(cboAlmacen.SelectedValue) == -1))
                {
                    gpbArticulos.Enabled = true;
                }
                // Si la grilla tiene artículos
                if (dgvArticulos.RowCount > 0)
                {
                    // Habilito el botón quitar
                    btnQuitarArt.Enabled = true;
                    // Creo un contador
                    int fila = 1;
                    // Recorro la grilla
                    foreach (DataGridViewRow row in dgvArticulos.Rows)
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
                                    row.Cells["Cantidad"].Value = clsGlobales.InsumosSeleccionados[i, 1];
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
                                    row.Cells["Cantidad"].Value = clsGlobales.ProductosSeleccionados[i, 1];
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
                    dgvArticulos.Focus();
                }
            }

        }

        #endregion

        #region Evento Click del botón Quitar Articulo

        private void btnQuitarArt_Click(object sender, EventArgs e)
        {
            // Creo una variable que guarda el largo del vector de Ids
            int LargoInsumos = clsGlobales.InsumosSeleccionados.GetLength(0);
            int LargoProductos = clsGlobales.ProductosSeleccionados.GetLength(0);
            
            // Verifico si el articulo que voy a borrar es Insumo
            if (dgvArticulos.CurrentRow.Cells["Tabla"].Value.ToString() == "INSUMOS")
            {
                // Recorro el vector
                for (int i = 0; i < LargoInsumos; i++)
                {
                    // Si el insumo que quiero borrar está en el vector
                    if (clsGlobales.InsumosSeleccionados[i,0] == Convert.ToDouble(dgvArticulos.CurrentRow.Cells["IdArticulo"].Value))
                    {
                        // Elimino el insumo del vector
                        clsGlobales.InsumosSeleccionados[i,0] = 0;
                        // Elimino su cantidad
                        clsGlobales.InsumosSeleccionados[i,1] = 0;
                        // Salgo del for
                        break;
                    }
                }
            }
            // Verifico si el articulo que voy a borrar es Producto
            if (dgvArticulos.CurrentRow.Cells["Tabla"].Value.ToString() == "PRODUCTOS")
            {
                // Recorro el vector
                for (int i = 0; i < LargoProductos; i++)
                {
                    // Si el producto que quiero borrar está en el vector
                    if (clsGlobales.ProductosSeleccionados[i,0] == Convert.ToDouble(dgvArticulos.CurrentRow.Cells["IdArticulo"].Value))
                    {
                        // Elimino el prodcuto del vector
                        clsGlobales.ProductosSeleccionados[i,0] = 0;
                        // Elimino su cantidad
                        clsGlobales.ProductosSeleccionados[i,1] = 0;
                        // Salgo del for
                        break;
                    }
                }
            }
            
            // Elimino la fila de la grilla
            dgvArticulos.Rows.RemoveAt(dgvArticulos.CurrentRow.Index);
            // Si ya no quedan artículos en la grilla, secativo el botón de quitar artículos
            if (dgvArticulos.Rows.Count == 0)
            {
                // Deshabilito el botón quitar
                btnQuitarArt.Enabled = false;
                // Deshabilito el botón aceptar
                btnAceptar.Enabled = false;
            }
        }

        #endregion

        #region Evento SelectionChanged de la grilla

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            // Si la grilla tiene artículos
            if (dgvArticulos.RowCount > 0)
            {
                // Almaceno én una variable la posición de fila en la que me encuentro
                int fila = dgvArticulos.CurrentRow.Index;
                // Pongo el foco de la fila en la columna cantidad
                dgvArticulos.CurrentCell = dgvArticulos["Cantidad", fila];
            }
        }

        #endregion

        #region Evento Click del botón Aceptar

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Verifico que los datos de las grilla estén completos
            bool controlGrillas = ValidarGrillas();
            // Si los datos de la grilla no son correctos, devuelvo el control al formulario saliendo del evento
            if (!(controlGrillas))
            {
                // Informo que faltan datos
                MessageBox.Show("Debe completar los datos para poder continuar", "VERIFICAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Si estoy grabando una cotización nueva
            if (IdCotizacion == 0)
            {
                // Grabo los datos de la nueva cotización en la base de datos
                GrabarCotizacionesNuevas();
            }
            // Si estoy grabando una edición
            else
            {
                // Grabo los datos de la nueva cotización en la base de datos
                GrabarCotizacionesEditadas();
            }
            // Vacío los vectores para un nuevo uso
            VaciarVectoresGlobales();

            // Paso los datos de la cabecera de impresión a la clase
            
            
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
            // Cierro el formulario
            this.Close();
        }

        #endregion

        private void dgvArticulos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            double Cantidad = Convert.ToDouble(this.dgvArticulos.CurrentRow.Cells["Cantidad"].Value);
            this.dgvArticulos.CurrentRow.Cells["Cantidad"].Value = Cantidad.ToString("#0.00");
        }
        #endregion

    }
}

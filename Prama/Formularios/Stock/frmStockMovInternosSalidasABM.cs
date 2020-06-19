using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Prama.Formularios.Stock
{
    public partial class frmStockMovInternosSalidasABM : Form
    {

        #region Variables del formulario

        // Declaro la variable que va a almacenar las sentencias SQL
        string myCadenaSQL = "";
        // Declaro una bandera que va a controlar el comportamiento de la grilla
        bool BanderaCombo = false;
        // DEclaro una bandera que va a controlar si el proceso de intento de guardado es correcto
        bool BanderaGuardar = false;
        // Variable que almacena el Id del Movimiento
        int IdMovimientoInterno = 0;
        // Variable que almacena el tio de comprobante que es el movimiento interno
        int tipoComprobante = 6;
        // Bandera que almacena si es una alta o una edición (0,1)
        int iTipoMovimiento;
        // Creo la variable que va a almacenar el último número usado
        int proximoNumero = 0;
        // Variable que guardo el total para imprimir
        double dNeto = 0;

        #endregion

        #region Constructor del formulario

        public frmStockMovInternosSalidasABM()
        {
            InitializeComponent();
            
        }

        #endregion

        #region Eventos del Fromulario

        #region Evento Load del formulario

        private void frmStockMovInternosSalidasABM_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            //ToolTips
            this.CargarToolsTip();
            // Cargo los combos con sus datos
            CargarCombos();
            // Pongo en su correspondiente textbox al comprador (Usuario logueado)
            txtUsuario.Text = clsGlobales.UsuarioLogueado.Usuario;

            //Punto de compra / venta y Almacen N. 24-11
            this.cboPunto.SelectedValue = Convert.ToInt32(clsGlobales.cParametro.PtoVtaPorDefecto);
            this.cboAlmacen.SelectedValue = Convert.ToInt32(clsGlobales.cParametro.AlmacenPorDefecto);

            // Deshabilito el reordenamiento de las grillas por sus cabeceras
            DeshabilitarOrdenGrillas();
            // Dehabilito los controles del formulario
            InhabilitarControles();
            //Boton Eliminar
            if (dgvDetalleOrden.Rows.Count == 0)
            {
                this.btnQuitarArt.Enabled = false;
            }
            //Titulo
            this.Text = clsGlobales.cFormato.getTituloVentana() + " - MOVIMIENTOS INTERNOS DE STOCK - SALIDAS - ABM";

        }

        #endregion

        #region Eventos de la grilla

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

        private void dgvDetalleOrden_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Calculo el subtotal de la fila
            CalculcarSubtotal();
            // Calculo el total de la orden
            CalcularTotal();
        }

        private void dgvDetalleOrden_Click(object sender, EventArgs e)
        {
            // Cambio el estado de la bandera que controla la grilla
            BanderaCombo = true;
        }

        #endregion

        #region eventos de los botones

        #region Evento Click del botón btnAgregarArt

        private void btnAgregarArt_Click(object sender, EventArgs e)
        {
            // Verifico que se hayan elegido punto de compra y almacén para poder continuar
            if (cboPunto.SelectedIndex == -1 || cboAlmacen.SelectedIndex == -1 || cboMotivos.SelectedIndex == -1)
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
            frmArticulosBuscar myForm = new frmArticulosBuscar(true, true);
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
                if (dgvDetalleOrden.RowCount > 0 && !(Convert.ToInt32(cboPunto.SelectedValue) == -1) && !(Convert.ToInt32(cboAlmacen.SelectedValue) == -1) && !(Convert.ToInt32(cboMotivos.SelectedValue) == -1))
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

        #region Evento Click del botón Aceptar

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Grabo la orden
            GrabarOrden();
            // Si se pudo guardar
            if (BanderaGuardar)
            {
                // Grabo los cambios al stock y los movimientos
                ActualizarStock();
                // Imprimo el movimiento
                ImprimirMov();
                // Salgo del formulario
                btnCancelar.PerformClick();
            }
            
        }

        #endregion

        #region Evento Click del botón Cancelar

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Vacío los vectores
            VaciarVectoresGlobales();
            // Cierro e formulario
            this.Close();
        }

        #endregion

        #endregion

        #endregion

        #region Métodos del formulario

        #region Método que carga los toolsTip al formulario

        private void CargarToolsTip()
        {
            toolTip1.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip2.SetToolTip(this.btnCancelar, "Cancelar");
            toolTip3.SetToolTip(this.btnAgregarArt, "Agregar Artículo");
            toolTip4.SetToolTip(this.btnQuitarArt, "Quitar Artículo");
            toolTip5.SetToolTip(this.btnQuitarArt, "Agregar desde Excel");
        }

        #endregion

        #region Método que carga los combos del formulario

        private void CargarCombos()
        {
            // Cargo el combo de los puntos de venta
            clsDataBD.CargarCombo(cboPunto, "PuntosVentas", "PuntoVenta", "IdPuntoVenta", "Activo = 1");
            // Cargo el combo de los almacenes
            clsDataBD.CargarCombo(cboAlmacen, "Almacenes", "Almacen", "IdAlmacen", "Activo = 1");
            // Cargo el combo de los Motivos que se pueden mostrar
            clsDataBD.CargarCombo(cboMotivos, "StockMotivos", "StockMotivo", "IdStockMotivo", "Show = 1");
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

        #region Método que inhabilita los controles del formulario

        private void InhabilitarControles()
        {
            this.txtNeto.Enabled = false;
        }

        #endregion

        #region Método que habilita los controles del formulario

        private void HabilitarControles()
        {
            // Habilito los campos de búsqueda
            this.txtNeto.Enabled = false;
            
            // Deshabilito el botón quitar articulos
            this.btnQuitarArt.Enabled = false;
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

        #region Método que graba los movimientos

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
            // Grabo la nueva orden
            GrabarOrdenesNuevas();

            // Vacío los vectore para reusarlos
            VaciarVectoresGlobales();
           
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

        #region Método que graba los nuevos movimentos

        private void GrabarOrdenesNuevas()
        {
            // Busco el próximo número a imprimir
            // Armo la cadena para buscar el número
            string myCadenaNumero = "select * from TiposComprobantesCompras where IdTipoComprobanteCompra = " + tipoComprobante;
            // Creo una tabla que me va a almacenar el resultado de la consulta
            DataTable myTabla = clsDataBD.GetSql(myCadenaNumero);
            
            // Recorro la tabla para almacenar el último numero usado y sumarle 1
            foreach (DataRow row in myTabla.Rows)
            {
                // Almaceno en la variable el número que corresponde a la próxima cotización
                proximoNumero = ((Convert.ToInt32(row["Numero"])) + 1);
            }
            // Paso los datos del formulario a la clase para grabarlos

            DateTime dFechaReal = DateTime.Now;
            string sFecha = clsValida.ConvertirFecha(DateTime.Now);
            int iMotivo = Convert.ToInt32(cboMotivos.SelectedValue);
            int iPunto = Convert.ToInt32(cboPunto.SelectedValue);
            int iAlmacen = Convert.ToInt32(cboAlmacen.SelectedValue);
            string sDescripcion = txtDescripcion.Text;
            int iFilas = dgvDetalleOrden.Rows.Count;
            dNeto = Convert.ToDouble(txtNeto.Text);
            string sObservaciones = txtObs.Text;

            
            // Armo la cadena SQL para grabar en la tabla Comprobantes de compras
            myCadenaSQL = "insert into StockMovimientosInternos (FechaReal, Fecha, Numero, IdStockMotivo, IdPuntoVenta," +
                                                                  "IdAlmacen, Salida, Descripcion, Articulos, Valor, Observaciones, Activo) values('" +
                                                                  dFechaReal + "', '" +
                                                                  sFecha + "', " +
                                                                  proximoNumero + ", " +
                                                                  iMotivo + ", " +
                                                                  iPunto + ", " +
                                                                  iAlmacen + ", 1 ,'" +
                                                                  sDescripcion + "', " +
                                                                  iFilas + ", " +
                                                                  dNeto + ", '" +
                                                                  sObservaciones + "', 1)";
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

        #region Método que graba el detalle de los nuevos movimientos

        private void GrabarDetalleOrdenesNuevas()
        {
            // Variable que almacena el Id del comrpbante
            int ultimoComprobante = 0;
            // Si es una nueva cotizacion busco el último número de id del comprobante
            if (IdMovimientoInterno == 0)
            {
                // Obtengo el último Id de la tabla comprobantes de compras del tipo cotización
                ultimoComprobante = clsDataBD.RetornarUltimoId("StockMovimientosInternos", "IdMovimientoInterno");
            }
            // Si estoy editando uno, le paso el número del id del comprobante
            else
            {
                // Obtengo el último Id de la tabla comprobantes de compras del tipo cotización
                ultimoComprobante = IdMovimientoInterno;
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
                myCadenaSQL = "insert into StockMovimientoInternoDetalle (IdArticulo, IdMovimientoInterno, Cantidad, Precio, TipoArticulo, Activo) values (" +
                                IdArticulo + ", " +
                                ultimoComprobante + ", " +
                                CantidadArticulo + ", " +
                                PrecioArticulo + ", '" +
                                tipoArt + "',1)";
                // Grabo en la tabla
                clsDataBD.GetSql(myCadenaSQL);
            }

            this.BanderaGuardar = true;
        }

        #endregion

        #region Método que graba el detalle de los movimientos editados

        private void GrabarDetalleOrdenesEditadas()
        {
            // Elimino el detalle anterios
            myCadenaSQL = "delete from StockMovimientoInternoDetalle where IdMovimientoInterno = " + IdMovimientoInterno;
            // Borro el detalle viejo en la tabla
            clsDataBD.GetSql(myCadenaSQL);
            // Grabo el nuevo detalle
            GrabarDetalleOrdenesNuevas();
        }

        #endregion

        #region Método que graba los cambios  a los stock de los artículos

        private void ActualizarStock()
        {
            // Mensaje de confirmación
            DialogResult myRespuesta = MessageBox.Show("Confirma el Movimiento " + cboMotivos.Text + " ?",
                                        "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Si confirma el movimiento
            if (myRespuesta == DialogResult.Yes)
            {
                // Verifico si es entrada
                bool bEntrada = RetornarEntrada(Convert.ToInt32(cboMotivos.SelectedValue));
                // Variable auxiliares para cálculos
                double stockOriginal = 0;
                double dCantMov = 0;
                double dCant = 0;

                // Cuento las filas de la grilla
                int iFilas = Convert.ToInt32(dgvDetalleOrden.Rows.Count);
                int iFilasUsadas = 0;
                // Creo un vector para almacenar las cadenas sql para el archivo que se va a exportar
                string [] sLineaSQL = new string[iFilas] ;
                string sCadenaMov = "";

                // Recorro la grilla
                foreach (DataGridViewRow NewRow in dgvDetalleOrden.Rows)
                {
                    if (NewRow.Cells["Cantidad"].Value != null)
                    {
                        // Variable que almacena la cantidad en formato double
                        dCant = Convert.ToDouble(NewRow.Cells["Cantidad"].Value);
                    }
                    else
                    {
                        dCant = 0;
                    }

                    dCantMov = dCant;
                    // Busco el artículo para sumarle el stock
                    string myCadenaSQL = "select * from Articulos where IdArticulo = " + Convert.ToInt32(NewRow.Cells["IdArticulo"].Value);
                    // Ejecuto la consulta y paso los datos a una tabla
                    DataTable myTablaArticulos = clsDataBD.GetSql(myCadenaSQL);
                    // Recorro la tabla y tomo el valor del stock original
                    foreach (DataRow row in myTablaArticulos.Rows)
                    {
                        // Paso a la variable el stock actual
                        stockOriginal = Convert.ToDouble(row["Stock"]);
                    }
                    // Si el motivo del movimiento es entrada
                    if (bEntrada)
                    {
                        // al stock original le sumo la cantidad del movimiento
                        stockOriginal = stockOriginal + dCant;
                    }
                    // Si el motivo del movimiento no es entrada
                    else
                    {
                        // al stock original le resto la cantidad del movimiento
                        stockOriginal = stockOriginal - dCant;
                        //dCantMov = dCantMov * -1;
                    }

                    // Actualizo el artículo con el nuevo stock ( BASE 1)
                    myCadenaSQL = "update Articulos set Stock = " + stockOriginal + " where IdArticulo = " + Convert.ToInt32(NewRow.Cells["IdArticulo"].Value);
                    // Ejecuto la consulta
                    clsDataBD.GetSql(myCadenaSQL);

                    // Grabo el movimiento de stock
                    GrabarMovimientoStock(Convert.ToInt32(NewRow.Cells["IdArticulo"].Value), Convert.ToInt32(cboMotivos.SelectedValue), DateTime.Now, dCantMov);

                    // ARCHIVO PLANO
                    sCadenaMov = "update Articulos set Stock = Stock + " + dCant + " where IdArticulo = " + Convert.ToInt32(NewRow.Cells["IdArticulo"].Value);
                    // Grabo la linea para el archivo de texto
                    sLineaSQL[iFilasUsadas] = sCadenaMov;
                    // Aumento el valor de la variable
                    iFilasUsadas++;
                }

                // Grabo los datos en el archivo plano
                GrabarArchivoPlano(sLineaSQL);
            }
        }

        #endregion

        #region Método que retorna si el Motivo elegido es entrada (ALTA)

        private bool RetornarEntrada(int IdMot)
        {
            // Variable de retorno
            bool aux = false; ;
            // Armo la cadena SQL
            string myCadenaSql = "select * from StockMotivos where IdStockMotivo = " + IdMot;
            // Ejecuto la consulta y lleno la tabla
            DataTable myTabla = clsDataBD.GetSql(myCadenaSql);
            // Reccorro la tabla y toma el valor en la variabe
            foreach (DataRow row in myTabla.Rows)
            {
                // Si es entrada
                if (Convert.ToBoolean(row["Entrada"]))
                {
                    // Paso el valor a la variable de retorno
                    aux = true;
                }
            }
            // Devuelvo el valor
            return aux;
        }


        #endregion

        #region Método que graba el movimiento de Stock en la tabla StockMovimientos

        private void GrabarMovimientoStock(int IdArt, int IdMot, DateTime Fec, double Cant)
        {
            // Variable que almacena la cadena SQL
            string myCadenaSql = "insert into StockMovimientos (IdArticulo, IdStockMotivo, Fecha, sFecha, Cantidad, IdUsuario, Activo) values ("
                                + IdArt + ", "
                                + IdMot + ", '"
                                + Fec + "', '"
                                + clsValida.ConvertirFecha(Fec) + "', "
                                + Cant + ", "
                                + clsGlobales.UsuarioLogueado.IdUsuario + ", 1)";
            // Ejecuto la consulta
            clsDataBD.GetSql(myCadenaSql);
        }

        #endregion

        #region Método que imprime el movimiento

        private void ImprimirMov()
        {
            //Data Set
            dsReportes oDsArt = new dsReportes();
            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = dgvDetalleOrden.Rows.Count;

            for (int i = 0; i < dgvFilas; i++)
            {

                oDsArt.Tables["dtMovimientoInterno"].Rows.Add
                (new object[] 
                        { 
                            dgvDetalleOrden["Codigo",i].Value.ToString(),
                            dgvDetalleOrden["Articulo",i].Value.ToString(),
                            dgvDetalleOrden["Cantidad",i].Value.ToString(),
                            dgvDetalleOrden["Unidad",i].Value.ToString(),
                            dgvDetalleOrden["PrecioFinal",i].Value.ToString(),
                            dgvDetalleOrden["TotalArt",i].Value.ToString(),
                        }
                );
            }

            //Objeto Reporte
            rptMovimientoInterno oRepArt = new rptMovimientoInterno();
            //Cargar Reporte            
            //    oRepTipoCli.Load(Application.StartupPath + "\\rptTipoCli.rpt");
            oRepArt.Load(Application.StartupPath + "\\rptMovimientoInterno.rpt");
            //Establecer el DataSet como DataSource
            oRepArt.SetDataSource(oDsArt);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepArt;

            oRepArt.DataDefinition.FormulaFields["TipoComprobante"].Text = "'X'";
            oRepArt.DataDefinition.FormulaFields["codigoComp"].Text = "'Remito Interno'";

            // Fecha del día
            string sFecha = clsValida.ConvertirFecha(DateTime.Now);
            oRepArt.DataDefinition.FormulaFields["Fecha"].Text ="'" + sFecha + "'";

            oRepArt.DataDefinition.FormulaFields["DescComp"].Text = "'Mov. Interno : 0010-'";

            // Numero
            string sPNum = proximoNumero.ToString();
            int CantPos = sPNum.Length;
            string sNumero = clsValida.ConvertirNumeroComprobante(sPNum, CantPos);
            oRepArt.DataDefinition.FormulaFields["NroComp"].Text = "'" + sNumero + "'";

            // Razón social
            string sRazon = cboAlmacen.Text;
            oRepArt.DataDefinition.FormulaFields["RazonSocial"].Text = "'" + sRazon + "'";

            // Total
            string sTotal = dNeto.ToString("#0.00");
            oRepArt.DataDefinition.FormulaFields["Total"].Text = "'" + sTotal + "'";

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();
        }

        #endregion

        #region Método que escribe los datos en un texto plano para actualizar el stock del espacio

        private void GrabarArchivoPlano(string[] Cadena)
        {
            try
            {
                string sFolder = "";

                //Elegir carpeta destino
                if (FolderBrowser.ShowDialog() == DialogResult.OK)
                {
                    sFolder = FolderBrowser.SelectedPath;
                }

                //Empty?
                if (sFolder == "") { return; }

                string iAño = DateTime.Now.Year.ToString();
                string iMes = DateTime.Now.Month.ToString();
                string iDia = DateTime.Now.Day.ToString();
                string iHora = DateTime.Now.Hour.ToString();
                string iMinuto = DateTime.Now.Minute.ToString();
                string iSegundo = DateTime.Now.Second.ToString();

                string sFecha = iAño + "-" + iMes + "-" + iDia + "-" + iHora + "-" + iMinuto + "-" + iSegundo;

                string sArchivo = sFolder + "\\MovInterno_" + sFecha + ".SQL";

                StreamWriter sw = new StreamWriter(sArchivo);

                for (int i = 0; i < Cadena.Length; i++)
                {
                    sw.WriteLine(Cadena[i]);
                }
                sw.Close();

                MessageBox.Show("Archivo procesado con ÉXITO !!!", "PROCESADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR " + e.Message);
            }
        }

        #endregion

        #region Codigo BtnAgregarArtExcel

        private void btnAgregaArtExcl_Click(object sender, EventArgs e)
        {
            //Variables
            int CantColumnExcel = 0;

            //abrimos el dialogo para poder obtener el nombre la ubicacion del archivo
            ofdAbrirArchivo.Filter = "Archivos Excel (*.xls) |*.xls|All Files (*.*)|*.*";
            ofdAbrirArchivo.FilterIndex = 1;

            //Leer Archivo xls
            if (ofdAbrirArchivo.ShowDialog() == DialogResult.OK)
            {
                //OBTENER NOMBRE ARCHIVO EXCEL 
                string sArchivo = ofdAbrirArchivo.FileName;

                //VARIABLE PARA GUARDAR RESULTADO
                bool bResult = false;

                //En .F. porque no se sabe que tipo de archivo viene.
                clsGlobales.bNuevoExcel = false;

                //CAMBIAR PUNTERO MOUSE
                Cursor.Current = Cursors.WaitCursor;

                //Obtener la cantidad de columnas del archivo excel
                CantColumnExcel = clsGlobales.cFormato.RetornarCantColumnExcel(sArchivo);                

                //LEER EL EXCEL
                bResult = clsGlobales.cFormato.LeerExcel(sArchivo, CantColumnExcel, false, false);

                //RETORNAR CURSOR (PUNTERO MOUSE)
                Cursor.Current = Cursors.Default;

                //SI TODO SALIO BIEN...
                if (bResult)
                {
                    //CARGAR VECTOR EXCEL A TABLA TEMPORAL
                    CargarVecExcelAVec();

                    //Apagar Flag
                    clsGlobales.bNuevoExcel = false;
                }
                else
                {
                    //Apagar Flag
                    clsGlobales.bNuevoExcel = false;
                }

                // Creo una variable que va a almacenar los Id de los proveedores seleccionados
                string sArt = "";
                // Creo una variable que guarda el largo del vector de Ids
                int LargoProductos = clsGlobales.ProductosSeleccionados.GetLength(0);

                if (!(LargoProductos == 0))
                {

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
                if (!(clsGlobales.ProductosSeleccionados.Length == 0))
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
                    if (dgvDetalleOrden.RowCount > 0 && !(Convert.ToInt32(cboPunto.SelectedValue) == -1) && !(Convert.ToInt32(cboAlmacen.SelectedValue) == -1) && !(Convert.ToInt32(cboMotivos.SelectedValue) == -1))
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
                        // Calculo el subtotal de la fila
                        CalculcarSubtotal();
                        // Calculo el total de la orden
                        CalcularTotal();
                    }
                }
            }
        }

        #endregion

        #region Metodo: CargarVecExcelAVec

        //METODO QUE CARGA EL VECTOR DE EXCEL A LA GRILLA
        private void CargarVecExcelAVec()
        {
            // Redimensiono el tamaño de la matriz
            clsGlobales.ProductosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.ProductosSeleccionados, new int[] {clsGlobales.ArtExcel.GetLength(0), 2 });

            //Mostrar Datos
            for (int iterador = 0; iterador < clsGlobales.ArtExcel.GetLength(0); iterador++)
            {

               // A la posición creada le asigno el Id seleccionado
               clsGlobales.ProductosSeleccionados[iterador, 0] = Convert.ToDouble(clsGlobales.ArtExcel[iterador, 1].ToString());
               clsGlobales.ProductosSeleccionados[iterador, 1] = Convert.ToDouble(clsGlobales.ArtExcel[iterador, 3].ToString());                                               
            }      
        }

        #endregion

    }

    #endregion
}

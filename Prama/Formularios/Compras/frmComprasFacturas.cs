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
    public partial class frmComprasFacturas : Form
    {

        #region Variables del formulario

        // Variable que almacena el Id de la cotización para cuando se hace el llamado desde las cotizaciones
        int IdFactura = 0;
        // Variable que almacena el proveedor
        int IdProveedor = 0;
        // Declaro la variable que va a almacenar las sentencias SQL
        string myCadenaSQL = "";
        // Declaro un string que va a ir acumulando los número de OC que cargamos en las factura
        string Referencias = "";
        // Declaro una bandera que va a controlar el comportamiento de la grilla
       /* bool BanderaCombo = false;*/
        // DEclaro una bandera que va a controlar si el proceso de intento de guardado es correcto
        bool BanderaGuardar = false;
        // Bandera que controla el formateo de la tabla la primera vez que pasa
        bool banderaFormateo = false;
        // Variable que almacena el tipo de comprobante Cotización
        int tipoComprobante = 3;
        // Creo un vector de objetos de la clase clsComprobantesCompras
        clsComprobantesCompras [] myComprobantes = new clsComprobantesCompras[clsGlobales.ComprobantesSeleccionados.GetLength(0)]; 
        // Declaro e instancio las clases que voy a usar en el formulario
        // Instancio un objeto de la clase comprobantes de compras para pasarle los datos
        clsComprobantesCompras myComprobanteNuevo = new clsComprobantesCompras();
        // Instancio un objeto de la clase comprobantes de compras para pasarle los datos
        clsComprobantesCompras myComprobanteViejo = new clsComprobantesCompras();
        // Instancio un objeto de la calse proveedores para pasarle los datos que me devuleve la consulta
        clsProveedores myProveedor = new clsProveedores();
        // Creo un datatable para el source de la grilla cuando se cargan comprobantes
        DataTable myTabla = new DataTable();
        // Creo un datatable para el source de la grilla paa cuando es una factura sin órdenes
        DataTable nuevaFactura = new DataTable();

        #endregion

        #region Constructor del formulario

        public frmComprasFacturas(int[] Comprobantes, int IdFact, int IdProv)
        {
            // Paso a la variable el Id de la cotización que viene por parámetro
            clsGlobales.ComprobantesSeleccionados = Comprobantes;
            // Paso a la variable el Id de la factura a modificar
            IdFactura = IdFact;
            // Paso a la variable el Id del proveedor
            IdProveedor = IdProv;
            // Inicializo los controles del formulario
            InitializeComponent();
        }

        #endregion

        #region Eventos del Formulario

        #region Evento Load del formulario

        private void frmComprasFacturas_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            // Cargo los toolstip de los botones
            CargarToolsTip();
            // Cargo los combos con sus datos
            CargarCombos();
            // Pongo en su correspondiente textbox al comprador (Usuario logueado)
            txtComprador.Text = clsGlobales.UsuarioLogueado.Usuario;
            //Punto de compra / venta y Almacen
            this.cboPunto.SelectedValue = Convert.ToInt32(clsGlobales.cParametro.PtoVtaPorDefecto);
            this.cboAlmacen.SelectedValue = Convert.ToInt32(clsGlobales.cParametro.AlmacenPorDefecto);
            // Deshabilito el reordenamiento de las grillas por sus cabeceras
            DeshabilitarOrdenGrillas();
            // Dehabilito los controles del formulario
            InhabilitarControles();
            // Si estoy modificando una factura
            if (!(IdFactura == 0))
            {
                // Cargo los datos de la factura a editar
                CargarFacturaEditar();
                // deshabilito todo
                InhabilitarTodo();

            }
            // Si no estoy modificando una factura
            else
            {
                // Si estoy llamando al formulario desde la cuenta corriente del proveedor
                if (!(IdProveedor == 0))
                {
                    CargarProveedorNuevo();
                }
                else
                {
                    // Si el Id de la cotización no es 0, cargo los datos del proveedor y las órdenes de compra
                    if (!(clsGlobales.ComprobantesSeleccionados.GetLength(0) == 0))
                    {
                        // Llamo al método que carga los datos de la cotización
                        CargarDatosOrden();

                    }
                    else
                    {
                        // Seteo los TextBox a 0 para hacer los cálculos
                        SetearTextBox();
                    }
                }
                
            }

            //Activar Botones
            this.ActivarBotones();                
            this.Text = clsGlobales.cFormato.getTituloVentana() + " - FACTURAS DE COMPRA";

            //.T. o .F. Actualizar Composicion
            this.chkComposicion.Enabled = this.ObtenerPermiso(8,1,clsGlobales.UsuarioLogueado.IdUsuario);
        }

        #endregion

        #region Eventos de los botones

        #region Evento Click del Boton btnBuscar

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

        #region Evento Click del botón btnAgregarArt

        private void btnAgregarArt_Click(object sender, EventArgs e)
        {
            // Verifico que se hayan elegido punto de compra y almacén para poder continuar
            if (cboPunto.SelectedIndex == -1 || cboAlmacen.SelectedIndex == -1)
            {
                // Informo que faltan los datos y devuelvo el control al usuario
                MessageBox.Show("Debe seleccionar Punto de Venta y Almacén", "VERIFICAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool bInsumosChk = Convert.ToBoolean(myProveedor.ProvIns);
            bool bProductos = Convert.ToBoolean(myProveedor.ProvProd);

            // Creo una variable que guarda el largo del vector de Ids
            int LargoInsumos = clsGlobales.InsumosSeleccionados.GetLength(0);
            int LargoProductos = clsGlobales.ProductosSeleccionados.GetLength(0);

            // Si la grilla tiene filas, cargo a las matrices las cantidades cargadas
            if (!(dgvDetalleFactura.RowCount == 0))
            {
                // Comienzo a recorrer la grilla
                foreach (DataGridViewRow row in dgvDetalleFactura.Rows)
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

            if (IdProveedor == 0)
            {
                //dgvDetalleFactura.DataSource = null;
                if (!(clsGlobales.ComprobantesSeleccionados[0] == 0))
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
            // Creo una variable para los id de comprobantes
            string Scomp = "";
            // Recorro el vector de comprobantes para tomar sus Ids para la consulta
            for (int i = 0; i < clsGlobales.ComprobantesSeleccionados.GetLength(0); i++)
            {
                // Si no es el último
                if (!(i == myComprobantes.GetLength(0) - 1))
                {
                    // Paso a string el Id del proveedor y lo ingreso a la cadena
                    Scomp = Scomp + clsGlobales.ComprobantesSeleccionados[i].ToString() + ",";
                }
                // Si es el último
                else
                {
                    // Paso a string el Id del proveedor y lo ingreso a la cadena
                    Scomp = Scomp + clsGlobales.ComprobantesSeleccionados[i].ToString();
                }
            }

            // si hay cargados productos o insumos en los vectores
            if (!(clsGlobales.InsumosSeleccionados.Length == 0) || !(clsGlobales.ProductosSeleccionados.Length == 0))
            {
                // Armo la cadena SQL
                myCadenaSQL = "select * from Articulos_Insumos_Productos where IdArticulo in (" + sArt + ")";
                // Creo una tabla que me va a almacenar el resultado de la consulta
                DataTable myTablaAux = clsDataBD.GetSql(myCadenaSQL);
                // Asigno la tabla al source de la grilla de proveedores
                /*int x = 0;*/
                // Variables para formatear el pasaje de precio y cantidad a los datable y de ahí a la grilla
                double auxPrecio = 0;

                //Limpiar grilla detalle
                this.dgvDetalleFactura.Rows.Clear();

                // Bandera que me dice si el Id del artículo original está en el vector
                bool banderaOriginal = false;

                // recorro la nueva tabla de artículos y compara los Ids con los originales
                foreach (DataRow row in myTablaAux.Rows)
                {
                    banderaOriginal = false;
                    // Recorro la tabla original y compar los Id
                    foreach (DataRow rowO in myTabla.Rows)
                    {
                        // Verifico los Ids de la grilla original con los datos de la selección de artículos
                        // Si son iguales, quiere decir que el artículo seleccionado ya estaba en la grilla originial
                        if (Convert.ToInt32(row["IdArticulo"]) == Convert.ToInt32(rowO["IdArticulo"]))
                        {
                            // Cambio el estado de la bandera
                            banderaOriginal = true;
                        }
                    }
                    // Si es una factura con órdenes de compra
                    if (!(clsGlobales.ComprobantesSeleccionados[0] == 0))
                    {
                        // Si el artículo no estaba en la grilla original
                        if (!(banderaOriginal))
                        {
                            auxPrecio = Convert.ToDouble(row["Precio"]);

                            // Creo una nueva fila para la grilla
                            DataRow rowNueva = myTabla.NewRow();
                            rowNueva["IdArticulo"] = row["IdArticulo"];
                            rowNueva["CodigoArticulo"] = row["CodigoArticulo"];
                            rowNueva["Articulo"] = row["Articulo"];
                            rowNueva["AbreviaturaUnidad"] = row["AbreviaturaUnidad"];
                            rowNueva["Cantidad"] = "1.00";
                            rowNueva["Precio"] = auxPrecio.ToString("#0.00000");
                            rowNueva["PorcentajeIva"] = row["IVA"];
                            rowNueva["Tabla"] = row["Tabla"];
                            // La agrego a la tabla original
                            myTabla.Rows.Add(rowNueva);
                        }
                    }
                    else
                    {
                        // Si paso la primera vez, formateo la tabla
                        if (!(banderaFormateo))
                        {
                            // Formateo la tabla para que acepte los artículos nuevos
                            // FormatearTabla();
                            // Cambio la bandera para eu no vuelva a intentar formatear la tabla
                               banderaFormateo = true;
                        }

                            auxPrecio = Convert.ToDouble(row["Precio"]);

                            //La cargo la consulta vacia al datatable NACHO. 2012-12-03
                            string auxStrSQL = "exec ComprasDetalleComprobantes 0, 0";
                            this.nuevaFactura = clsDataBD.GetSql(auxStrSQL);

                            // Creo una nueva fila para la grilla                         
                            
                            dgvDetalleFactura.Rows.Add();

                            // Cuento las filas de la grilla
                            int Filas = dgvDetalleFactura.Rows.Count;

                            // Si la grilla no está vacía
                            if (Filas > 0)
                            {
                             // Posiciono la grilla en la última fila
                                dgvDetalleFactura.CurrentCell = dgvDetalleFactura[0, Filas - 1];
                            }

                            //Cargo los datos a la grilla       
                            dgvDetalleFactura.CurrentRow.Cells[1].Value = row["IdArticulo"];
                            dgvDetalleFactura.CurrentRow.Cells[2].Value = row["CodigoArticulo"];
                            dgvDetalleFactura.CurrentRow.Cells[4].Value = row["Articulo"];
                            dgvDetalleFactura.CurrentRow.Cells[5].Value = row["AbreviaturaUnidad"];
                            dgvDetalleFactura.CurrentRow.Cells[6].Value = "1.00";
                            dgvDetalleFactura.CurrentRow.Cells[7].Value = auxPrecio.ToString("#0.00000");
                            dgvDetalleFactura.CurrentRow.Cells[8].Value = row["IVA"];
                            dgvDetalleFactura.CurrentRow.Cells[11].Value = row["Tabla"];


                            //Pasar el contenido de la grilla al DataTable
                            DataRow rowNueva = nuevaFactura.NewRow();
                            rowNueva["IdArticulo"] = dgvDetalleFactura.CurrentRow.Cells[1].Value; ;
                            rowNueva["CodigoArticulo"] = dgvDetalleFactura.CurrentRow.Cells[2].Value;
                            rowNueva["Articulo"] = dgvDetalleFactura.CurrentRow.Cells[4].Value;
                            rowNueva["AbreviaturaUnidad"] = dgvDetalleFactura.CurrentRow.Cells[5].Value;
                            //rowNueva["Cantidad"] = "1.00";
                            rowNueva["Precio"] = auxPrecio.ToString("#0.00000");
                            rowNueva["PorcentajeIva"] = dgvDetalleFactura.CurrentRow.Cells[8].Value;
                            rowNueva["Tabla"] = dgvDetalleFactura.CurrentRow.Cells[11].Value;
                            //La agrego a la tabla original
                            nuevaFactura.Rows.Add(rowNueva);
                    }
                    
                }
                

                if (!(clsGlobales.ComprobantesSeleccionados[0] == 0))
                {
                    // Asigno el source a la grilla
                    dgvDetalleFactura.DataSource = myTabla;
                }
               //NO PASO EL CONTENIDO AL DATASOURCE A LA GRILLA
                else  
                {
               /*    dgvDetalleFactura.DataSource = nuevaFactura;*/
                }
                
                // Habilito la carga de artículos
                if (dgvDetalleFactura.RowCount > 0 && !(Convert.ToInt32(cboPunto.SelectedValue) == -1) && !(Convert.ToInt32(cboAlmacen.SelectedValue) == -1))
                {
                    gpbArticulos.Enabled = true;
                }
                // Si la grilla tiene artículos
                if (dgvDetalleFactura.RowCount > 0)
                {
                    // Habilito el botón quitar
                    btnQuitarArt.Enabled = true;
                    // Creo un contador
                    int fila = 1;
                    // Recorro la grilla
                    foreach (DataGridViewRow row in dgvDetalleFactura.Rows)
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
                    //Activar Botones
                    this.ActivarBotones();
                    // Hago el cálculo de la fila
                    CalculcarSubtotal();
                    // Hago el cálculo del total de la factura
                    CalcularTotal();
                    // Paso el foco a la grilla
                    dgvDetalleFactura.Focus();
                }
            }
        }

        #endregion

        #region Evento Click del botón Cancelar

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Vacío el vector de comprobantes
            clsGlobales.ComprobantesSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ComprobantesSeleccionados, 0);
            // Vacío los vectores de insumos y productos
            VaciarVectoresGlobales();
            // Cambiar el estado de la bandera de los comprobantes de compra
            clsGlobales.bCompras = false;
            // Cierro el formulario
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
            if (dgvDetalleFactura.CurrentRow.Cells["Tabla"].Value.ToString() == "INSUMOS")
            {
                // Recorro el vector
                for (int i = 0; i < LargoInsumos; i++)
                {
                    // Si el insumo que quiero borrar está en el vector
                    if (clsGlobales.InsumosSeleccionados[i, 0] == Convert.ToDouble(dgvDetalleFactura.CurrentRow.Cells["IdArticulo"].Value))
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
            if (dgvDetalleFactura.CurrentRow.Cells["Tabla"].Value.ToString() == "PRODUCTOS")
            {
                // Recorro el vector
                for (int i = 0; i < LargoProductos; i++)
                {
                    // Si el producto que quiero borrar está en el vector
                    if (clsGlobales.ProductosSeleccionados[i, 0] == Convert.ToDouble(dgvDetalleFactura.CurrentRow.Cells["IdArticulo"].Value))
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
            dgvDetalleFactura.Rows.RemoveAt(dgvDetalleFactura.CurrentRow.Index);
            // Si ya no quedan artículos en la grilla, secativo el botón de quitar artículos
            if (dgvDetalleFactura.RowCount == 0)
            {
                // Deshabilito el botón quitar
                btnQuitarArt.Enabled = false;
                // Deshabilito el botón aceptar
                btnAceptar.Enabled = false;
                // Pongo en 0 los txt del formulario
                SetearTextBox();
            }
            else
            {
                // Recalculo el total de la orden de compra
                CalcularTotal();
            }
            //Activar Botones
            this.ActivarBotones();
        }

        #endregion

        #region Evento Click del botón Aceptar

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Grabo la factura
            GrabarFactura();
            // Si se pudo guardar
            if (BanderaGuardar)
            {
                // Salgo del formulario
                btnCancelar.PerformClick();
            }
        }

        #endregion

        #endregion

        #region Eventos de la grilla

        #region Evento CellContentClick de la grilla

        private void dgvDetalleFactura_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalleFactura.Rows.Count > 0)
            {
                // Armo un switch para las columnas de la grilla
                switch (this.dgvDetalleFactura.Columns[this.dgvDetalleFactura.CurrentCell.ColumnIndex].Name)
                {
                    // Para todas las columnas menos para cantidad y pecio
                    case "Item":
                    case "CodigoArticulo":
                    case "CodigoProveedor":
                    case "Articulo":
                    case "Unidad":
                    case "TotalArt":
                    case "Alicuota":
                    case "IVA":
                    case "SubTotal":
                        // Pongo el foco en la columna Canidad    
                        this.dgvDetalleFactura.CurrentCell = dgvDetalleFactura["Cantidad", dgvDetalleFactura.CurrentRow.Index];
                        // Salgo del switch
                        break;
                }
            }
        }

        #endregion

        #region Evento CellEndEdit de la grilla

        private void dgvDetalleFactura_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Calculo el subtotal de la fila
            CalculcarSubtotal();
            // Calculo el total de la orden
            CalcularTotal();
        }

        #endregion

        #region Evento KeyUp de la grilla

        private void dgvDetalleFactura_KeyUp(object sender, KeyEventArgs e)
        {
            if (dgvDetalleFactura.Rows.Count > 0)
            {
                // Armo un switch para las columnas de la grilla
                switch (this.dgvDetalleFactura.Columns[this.dgvDetalleFactura.CurrentCell.ColumnIndex].Name)
                {
                    // Para todas las columnas menos para cantidad y pecio
                    case "Item":
                    case "CodigoArticulo":
                    case "CodigoProveedor":
                    case "Articulo":
                    case "Unidad":
                    case "TotalArt":
                    case "Alicuota":
                    case "IVA":
                    case "SubTotal":
                        // Pongo el foco en la columna Canidad    
                        this.dgvDetalleFactura.CurrentCell = dgvDetalleFactura["Cantidad", dgvDetalleFactura.CurrentRow.Index];
                        // Salgo del switch
                        break;
                }
            }
        }

        #endregion

        #endregion

        #region Eventos de los TextBox

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

        #region Evento DoubleClick del txtRSoc

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

        #region EventoDoubleClick del txtCuit

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
        }

        #endregion

        #region Evento KeyPress y Leave del txtFlete

        private void txtFlete_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 44)
            {
                e.KeyChar = Convert.ToChar(46);
                ch = e.KeyChar;

            }
            //PUNTO DECIMAL. N.
            if (ch == 46 && base.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            //NUMEROS. N.
            if (!char.IsDigit(ch) && ch != 8 && ch != 46 && ch != 13 && ch != 9)
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                this.btnAceptar.Focus();
            }
        }

        private void txtFlete_Leave(object sender, EventArgs e)
        {
            if (!(this.txtFlete.Text == ""))
            {
                this.txtFlete.Text = Convert.ToDouble(this.txtFlete.Text).ToString("#0.00");
            }
        }

        #endregion

        #region Evento KeyPress y Leave del txtPercVarias

        private void txtPercVarias_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 44)
            {
                e.KeyChar = Convert.ToChar(46);
                ch = e.KeyChar;

            }
            //PUNTO DECIMAL. N.
            if (ch == 46 && base.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            //NUMEROS. N.
            if (!char.IsDigit(ch) && ch != 8 && ch != 46 && ch != 13 && ch != 9)
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                this.btnAceptar.Focus();
            }
        }

        private void txtPercVarias_Leave(object sender, EventArgs e)
        {
            if (!(this.txtPercVarias.Text == ""))
            {
                // Reclaculo el saldo de la factura
                CalcularTotal();
                // Formateo el dato en el texbox
                this.txtPercVarias.Text = Convert.ToDouble(this.txtPercVarias.Text).ToString("#0.00");
            }
        }

        #endregion

        #region Evento KeyPress y Leave del txtPercIva

        private void txtPercIva_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 44)
            {
                e.KeyChar = Convert.ToChar(46);
                ch = e.KeyChar;

            }
            //PUNTO DECIMAL. N.
            if (ch == 46 && base.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            //NUMEROS. N.
            if (!char.IsDigit(ch) && ch != 8 && ch != 46 && ch != 13 && ch != 9)
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                this.btnAceptar.Focus();
            }
        }

        private void txtPercIva_Leave(object sender, EventArgs e)
        {
            if (!(this.txtPercIva.Text == ""))
            {
                // Reclaculo el saldo de la factura
                CalcularTotal();
                // Formateo el dato en el texbox
                this.txtPercIva.Text = Convert.ToDouble(this.txtPercIva.Text).ToString("#0.00");
            }
        }

        #endregion

        #region Evento KeyPress y Leave del txtPercIB

        private void txtPercIB_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 44)
            {
                e.KeyChar = Convert.ToChar(46);
                ch = e.KeyChar;

            }
            //PUNTO DECIMAL. N.
            if (ch == 46 && base.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            //NUMEROS. N.
            if (!char.IsDigit(ch) && ch != 8 && ch != 46 && ch != 13 && ch != 9)
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                this.btnAceptar.Focus();
            }
        }

        private void txtPercIB_Leave(object sender, EventArgs e)
        {
            if (!(this.txtPercIB.Text == ""))
            {
                // Reclaculo el saldo de la factura
                CalcularTotal();
                // Formateo el dato en el texbox
                this.txtPercIB.Text = Convert.ToDouble(this.txtPercIB.Text).ToString("#0.00");
            }
        }

        #endregion

        #region Evento KeyPress y Leave del txtNoGrav

        private void txtNoGrav_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 44)
            {
                e.KeyChar = Convert.ToChar(46);
                ch = e.KeyChar;

            }
            //PUNTO DECIMAL. N.
            if (ch == 46 && base.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            //NUMEROS. N.
            if (!char.IsDigit(ch) && ch != 8 && ch != 46 && ch != 13 && ch != 9)
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                this.btnAceptar.Focus();
            }
        }

        private void txtNoGrav_Leave(object sender, EventArgs e)
        {
            if (!(this.txtNoGrav.Text == ""))
            {
                this.txtNoGrav.Text = Convert.ToDouble(this.txtNoGrav.Text).ToString("#0.00");
            }
        }

        #endregion

        #endregion

        #endregion

        #region Métodos del formulario

        #region Método que carga los combos del formulario

        private void CargarCombos()
        {
            // Cargo el combo de los puntos de venta
            clsDataBD.CargarCombo(cboPunto, "PuntosVentas", "PuntoVenta", "IdPuntoVenta","Activo = 1");
            // Dejo vacía la selección
            cboPunto.SelectedIndex = -1;
            // Cargo el combo de los almacenes
            clsDataBD.CargarCombo(cboAlmacen, "Almacenes", "Almacen", "IdAlmacen", "Activo = 1");
            // Dejo vacía la selección
            cboAlmacen.SelectedIndex = -1;
            // Cargo el combo de las condiciones de compra
            clsDataBD.CargarCombo(cboCondCompra, "CondicionesCompra", "CondicionCompra", "IdCondicionCompra", "Activo = 1");
            // Dejo vacía la selección
            cboCondCompra.SelectedIndex = -1;
            // Cargo el combo de imputaciones
            clsDataBD.CargarCombo(cboImputacion, "Imputaciones", "Imputacion", "IdImputacion", "CodigoInterno > 51000 and CodigoInterno != 52000 and Activo=1");
            // Dejo vacía la selección
            cboImputacion.SelectedIndex = -1;

        }

        #endregion

        #region Método que deshabilita el reordenamiento de las grilla desde sus cabeceras

        private void DeshabilitarOrdenGrillas()
        {
            // Deshabilito la reordenación de las columnas de las grillas
            foreach (DataGridViewColumn dgvCol in dgvDetalleFactura.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;

            }
        }

        #endregion

        #region Método que inhabilita los controles del formulario menos para Ver

        private void InhabilitarControles()
        {
            this.txtCodigoProv.Enabled = false;
            this.txtComprador.Enabled = false;
            this.txtCUIT.Enabled = false;
            this.txtCondicionIva.Enabled = false;
            this.txtRSoc.Enabled = false;

            this.btnAgregarArt.TabStop = true;
            this.btnAgregarArt.Enabled = true;
            this.btnQuitarArt.TabStop = true;
            this.btnQuitarArt.Enabled = true;        
        }

        #endregion

        #region Método que inhabilita todos los controles del formulario btnEditar

        private void InhabilitarTodo()
        {
            this.gpbCabecera.Enabled = false;
            this.gpbProveedores.Enabled = false;
            this.gpbActualizaciones.Enabled = false;
            this.gpbArticulos.Enabled = false;
            this.gpbFlete.Enabled = false;
            this.gpbPie.Enabled = false;
            this.btnAceptar.Enabled = false;
        }

        #endregion

        #region Método que habilita los controles del formulario

        private void HabilitarControles()
        {
            this.txtNumero.Enabled = true;
            this.txtCodigoProv.Enabled = true;
            this.txtRSoc.Enabled = true;
            this.txtCUIT.Enabled = true;
            
        }

        #endregion

        #region Método que controla botones del formulario

        private void ActivarBotones()
        {
            // Si no hay un proveedor seleccionado
            if (clsGlobales.ProveedoresSeleccionados.GetLength(0) == 0)
            {
                this.btnAgregarArt.TabStop = false;
                this.btnAgregarArt.Enabled = false;
                this.btnQuitarArt.TabStop = false;
                this.btnQuitarArt.Enabled = false;
            }
            // Si hay un proveedor seleccionado
            else 
            {
                this.btnAgregarArt.TabStop = true;
                this.btnAgregarArt.Enabled = true;
                this.btnQuitarArt.TabStop = true;
                this.btnQuitarArt.Enabled = true;
            }

            this.btnAceptar.TabStop = true;
            this.btnAceptar.Enabled = true;
            this.btnQuitarArt.TabStop = true;
            this.btnQuitarArt.Enabled = true;

            // Si la grilla no tiene detalle
            if (dgvDetalleFactura.Rows.Count == 0)
            {
                this.btnAceptar.TabStop = false;
                this.btnAceptar.Enabled = false;
                this.btnQuitarArt.TabStop = false;
                this.btnQuitarArt.Enabled = false;
            }
            // Si estoy viendo una factura, deshabilito el botón Aceptar
            if (!(IdFactura == 0))
            {
                this.btnAceptar.Visible = false;
            }
        }

        #endregion

        #region Método que carga los datos de la orden de compras

        private void CargarDatosOrden()
        {
            
            // creo un datatable para agregar los siguientes detalles de los comprobantes
            DataTable myTablaComprobantes = new DataTable();
            // Cargo los datos de las cotizaciones cargadas en el vector de las cotizaciones seleccionadas
            for (int i = 0; i < clsGlobales.ComprobantesSeleccionados.GetLength(0); i++)
            {
                // Armo la cadena SQL
                myCadenaSQL = "select * from Vista_ComprobantesCompras where Id = " + clsGlobales.ComprobantesSeleccionados[i];
                // Creo la tabla para la grilla
               
                // le paso los datos de la consulta SQL
                myTablaComprobantes = clsDataBD.GetSql(myCadenaSQL);

                foreach (DataRow rowComp in myTablaComprobantes.Rows)
                {
                    myComprobanteViejo = new clsComprobantesCompras();
                    
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
                    myComprobanteViejo.Activo = Convert.ToInt32(rowComp["Activo"]);
                    // Campos exclusivos de la factura de compras
                    if (rowComp["IVA105"] == DBNull.Value)
                    {
                        myComprobanteViejo.Iva105 = 0;
                    }
                    else
                    {
                        myComprobanteViejo.Iva105 = Convert.ToDouble(rowComp["IVA105"]);
                    }

                    if (rowComp["IVA210"] == DBNull.Value)
                    {
                        myComprobanteViejo.Iva210 = 0;
                    }
                    else
                    {
                        myComprobanteViejo.Iva210 = Convert.ToDouble(rowComp["IVA210"]);
                    }
                    
                    myComprobanteViejo.Total = Convert.ToDouble(rowComp["Total"]);
                    myComprobanteViejo.Saldo = Convert.ToDouble(rowComp["Saldo"]);
                    myComprobanteViejo.PercepcionesVarias = Convert.ToDouble(rowComp["PercepcionesVarias"]);
                    myComprobanteViejo.PercepcionesIva = Convert.ToDouble(rowComp["PercepcionesIva"]);
                    myComprobanteViejo.PercepcionesIB = Convert.ToDouble(rowComp["PercepcionesIB"]);
                    myComprobanteViejo.Exento = Convert.ToDouble(rowComp["NoGravados"]);
                    if (rowComp["IdImputacion"] == DBNull.Value)
                    {
                        myComprobanteViejo.IdImputacion = 103;
                    }
                    else
                    {
                        myComprobanteViejo.IdImputacion = Convert.ToInt32(rowComp["IdImputacion"]);
                    }
                    
                    myComprobanteViejo.Flete = Convert.ToDouble(rowComp["Flete"]);

                    if (myComprobanteViejo.IdTipoComprobanteCompra == 2)
                    {
                        if (Referencias == "")
                        {
                            Referencias = "O/C : " + myComprobanteViejo.Numero.ToString();
                        }
                        else
                        {
                            Referencias = Referencias + " / " + myComprobanteViejo.Numero.ToString();
                        }
                    }

                }
                // Cargo la cotización al vector de cotizaciones
                myComprobantes[i] = myComprobanteViejo;
                myTablaComprobantes.Rows.Clear();
                myTablaComprobantes.Columns.Clear();
                
            }
            // Busco los datos del proveedor en la tabla proveedores
            CargarProveedores(myComprobanteViejo.IdProveedor);
            // Paso los datos de las clases al formulario
            PasarDatosAlFormulario();
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

        #region Método que pasa los datos al formulario

        private void PasarDatosAlFormulario()
        {
            // Paso los datos del proveedor al formulario
            this.txtCodigoProv.Text = myProveedor.IdProveedor.ToString();
            this.txtRSoc.Text = myProveedor.RazonSocial;
            this.txtCUIT.Text = myProveedor.CUIT;
            this.txtCondicionIva.Text = myProveedor.CondicionIva;
            this.txtReferencia.Text = Referencias;

            if (this.IdFactura == 0)
            {
                // El punto de venta
                cboPunto.SelectedValue = clsGlobales.cParametro.PtoVtaPorDefecto;
                // El almacén
                cboAlmacen.SelectedValue = clsGlobales.cParametro.AlmacenPorDefecto;
                // La condición de compra
                cboCondCompra.SelectedValue = myProveedor.IdCondicionCompra;
                                
            }
            
            // Si hago la llamada desde la cuenta corriente del proveedor
            if (IdProveedor == 0)
            {
                if (!(clsGlobales.ComprobantesSeleccionados[0] == 0))
                {
                    // Paso los datos del pie del comprobante
                    if (myComprobanteViejo.IdTipoComprobanteCompra == 2)
                    {
                        this.txtNumero.Text = "";
                    }
                    else
                    {
                        this.txtNumero.Text = myComprobanteViejo.Numero.ToString();
                    }

                    this.txtFlete.Text = myComprobanteViejo.Flete.ToString("#0.00");
                    this.txtSubtotal.Text = myComprobanteViejo.Neto.ToString("#0.00");
                    this.txtIva105.Text = myComprobanteViejo.Iva105.ToString("#0.00");
                    this.txtIva210.Text = myComprobanteViejo.Iva210.ToString("#0.00");
                    this.txtPercVarias.Text = myComprobanteViejo.PercepcionesVarias.ToString("#0.00");
                    this.txtPercIva.Text = myComprobanteViejo.PercepcionesIva.ToString("#0.00");
                    this.txtPercIB.Text = myComprobanteViejo.PercepcionesIB.ToString("#0.00");
                    this.txtNoGrav.Text = myComprobanteViejo.Exento.ToString("#0.00");
                    this.txtTotal.Text = myComprobanteViejo.Total.ToString("#0.00");

                    //FAVOR MIRAR ACA GABI!

                    // Cargo los combos
                    // El punto de venta
                    cboPunto.SelectedValue = myComprobanteViejo.IdPuntoVenta;
                    // El almacén
                    cboAlmacen.SelectedValue = myComprobanteViejo.IdAlmacen;
                    // La condición de compra
                    cboCondCompra.SelectedValue = myComprobanteViejo.IdCondicionCompra;
                    // La imputación contable
                    cboImputacion.SelectedValue = myComprobanteViejo.IdImputacion;

                    // Cargo el detalle de la cotización a la grilla
                    // Vacío la grilla
                    dgvDetalleFactura.DataSource = null;
                    // Evito que el dgvUsuarios genere columnas automáticas
                    dgvDetalleFactura.AutoGenerateColumns = false;

                    //////////////////////////////////////////////

                    // creo un datatable para agregar los siguientes detalles de los comprobantes
                    DataTable myTablaAux = new DataTable();

                    // Recorro el vector de comrpbantes
                    for (int i = 0; i < myComprobantes.GetLength(0); i++)
                    {
                        if (clsGlobales.ConB == null)
                        {
                            // Armo la cadena SQL para ejecutar el procedieminto almacenado que trae el detalle del comprobante
                            string newMyCadenaSql = "exec ComprasDetalleComprobantes " + myComprobantes[i].IdComprasCotizaciones + ", " + myComprobantes[i].IdTipoComprobanteCompra;
                            // En la primera iteracion le paso los datos a la tabla para que me arme las cabeceras
                            if (i == 0)
                            {
                                // le paso los datos de la consulta SQL
                                myTabla = clsDataBD.GetSql(newMyCadenaSql);
                            }
                            else
                            {
                                // le paso los datos de la consulta SQL
                                myTablaAux = clsDataBD.GetSql(newMyCadenaSql);
                                // Recorro la tabla auxiliar y paso los datos de la fila a un datarow para después agregarlo al datatable de la grilla
                                foreach (DataRow row in myTablaAux.Rows)
                                {
                                    myTabla.ImportRow(row);
                                }
                            }
                        }
                        else
                        {
                            if (myComprobantes[i].IdTipoComprobanteCompra < 3)
                            {
                                // Armo la cadena SQL para ejecutar el procedieminto almacenado que trae el detalle del comprobante
                                string newMyCadenaSql = "exec ComprasDetalleComprobantes " + myComprobantes[i].IdComprasCotizaciones + ", " + myComprobantes[i].IdTipoComprobanteCompra;
                                // En la primera iteracion le paso los datos a la tabla para que me arme las cabeceras
                                if (i == 0)
                                {
                                    // le paso los datos de la consulta SQL
                                    myTabla = clsDataBD.GetSql(newMyCadenaSql);
                                }
                                else
                                {
                                    // le paso los datos de la consulta SQL
                                    myTablaAux = clsDataBD.GetSql(newMyCadenaSql);
                                    // Recorro la tabla auxiliar y paso los datos de la fila a un datarow para después agregarlo al datatable de la grilla
                                    foreach (DataRow row in myTablaAux.Rows)
                                    {
                                        myTabla.ImportRow(row);
                                    }
                                }
                            }
                            else
                            {
                                // Armo la cadena SQL para ejecutar el procedieminto almacenado que trae el detalle del comprobante
                                string newMyCadenaSql = "exec ComprasDetalleComprobantes2 " + myComprobantes[i].IdComprasCotizaciones + ", " + myComprobantes[i].IdTipoComprobanteCompra;
                                // En la primera iteracion le paso los datos a la tabla para que me arme las cabeceras
                                if (i == 0)
                                {
                                    // le paso los datos de la consulta SQL
                                    myTabla = clsDataBD.GetSqlB(newMyCadenaSql);
                                }
                                else
                                {
                                    // le paso los datos de la consulta SQL
                                    myTablaAux = clsDataBD.GetSqlB(newMyCadenaSql);
                                    // Recorro la tabla auxiliar y paso los datos de la fila a un datarow para después agregarlo al datatable de la grilla
                                    foreach (DataRow row in myTablaAux.Rows)
                                    {
                                        myTabla.ImportRow(row);
                                    }
                                }
                            }
                        }
                    }

                    // Asigno a la grilla el source
                    dgvDetalleFactura.DataSource = myTabla;
                    // Número de Item para la grilla
                    int fila = 1;
                    // Cantidad de Insumos para el vector
                    int insumos = 0;
                    // Cantidad de productos para el vector
                    int productos = 0;
                    // Paso los datos a los vectores
                    foreach (DataGridViewRow row in dgvDetalleFactura.Rows)
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
                    if (dgvDetalleFactura.RowCount > 0)
                    {
                        // Pongo el foco en la grilla y en el campo Cantidad
                        dgvDetalleFactura.Focus();
                        dgvDetalleFactura.CurrentCell = dgvDetalleFactura["Cantidad", dgvDetalleFactura.CurrentRow.Index];
                        // Calculo el subtotal por fila de artículos
                        CalculcarSubtotal();
                        // Calculo el total del comprobante
                        CalcularTotal();
                    }
                }
            }

        }

        #endregion

        #region Método que calcula el subtotal de los artículos por fila

        private void CalculcarSubtotal()
        {
            // Variable que guarda el resultado de la multiplicación
            double subTotal = 0;
            // Recorro la grilla y hago el cálculo
            foreach (DataGridViewRow row in dgvDetalleFactura.Rows)
            {
                // Calculo el subtotal del Artículo
                subTotal = Convert.ToDouble(row.Cells["Cantidad"].Value) * Convert.ToDouble(row.Cells["Precio"].Value);
                // Calculo el total del Iva del producto
                double CalcIva = subTotal * ((Convert.ToDouble(row.Cells["Alicuota"].Value)) * 0.01);
                // Paso a variables los datos de la fila para poder formatearlos
                double Cant = Convert.ToDouble(row.Cells["Cantidad"].Value);
                double Pre = Convert.ToDouble(row.Cells["Precio"].Value);
                double Ali = Convert.ToDouble(row.Cells["Alicuota"].Value);
                // Formtaeo los valores de las columnas
                
                row.Cells["Cantidad"].Value = Cant.ToString("#0.00");
                               
                row.Cells["Alicuota"].Value = Ali.ToString("#0.00");
                // Asigno el valor a la celda
                row.Cells["SubTotal"].Value = subTotal.ToString("#0.00");
                row.Cells["IVA"].Value = CalcIva.ToString("#0.00");
                row.Cells["Precio"].Value = Pre.ToString("#0.00000");

            }
        }


        #endregion

        #region Método que calcula el Total del comprobante y enumera las filas

        private void CalcularTotal()
        {
            // Variable que va a almacenar la suma de los subtotales
            double SubTotalAcumulado = 0;
            // Variable que almacena la suma de los Ivas y el excento
            double Exento = 0;
            double TotalIva105 = 0;
            double TotalIva210 = 0;
            double Grav21 = 0;
            double Grav10 = 0;
            // Variable que guarda la alícuota del artículo
            double dAlicuota = 0;
            // Vaiable que guarda el número de la fila
            int fila = 1;
            // Recorro la grilla y sumo los subtotales de lo artículos
            foreach (DataGridViewRow row in dgvDetalleFactura.Rows)
            {
                // Pongo el número del item
                row.Cells["Item"].Value = fila;
                // Según la alícuota, acumulo en variables
                dAlicuota = Convert.ToDouble(row.Cells["Alicuota"].Value);
                // Si la alícuota es 0, es excento
                if (dAlicuota == 0)
                {
                    Exento = Exento + Convert.ToDouble(row.Cells["SubTotal"].Value);
                }
                // Si la alícuota es del 10.5%
                if (dAlicuota == 10.5)
                {
                    TotalIva105 = TotalIva105 + Convert.ToDouble(row.Cells["IVA"].Value);
                    // Acumulo los subtotales
                    SubTotalAcumulado = SubTotalAcumulado + Convert.ToDouble(row.Cells["SubTotal"].Value);
                    // Grabado 10
                    Grav10 += Convert.ToDouble(row.Cells["SubTotal"].Value); 

                }
                // Si la alícuota es del 21%
                if (dAlicuota == 21)
                {
                    TotalIva210 = TotalIva210 + Convert.ToDouble(row.Cells["IVA"].Value);
                    // Acumulo los subtotales
                    SubTotalAcumulado = SubTotalAcumulado + Convert.ToDouble(row.Cells["SubTotal"].Value);
                    // Grabado 21
                    Grav21 += Convert.ToDouble(row.Cells["SubTotal"].Value);

                }
                
                //Aumento el enumerador de los items
                fila++;

            }
            // Muestro el resultado en el textbox
            this.txtSubtotal.Text = SubTotalAcumulado.ToString("#0.00");
            txtNeto10.Text = Grav10.ToString("#0.00");
            txtNeto21.Text = Grav21.ToString("#0.00");

            // Hago el cálculo para el total de la factura
            double TotalFactura = 0;
            if (string.IsNullOrEmpty(txtPercVarias.Text)) { txtPercVarias.Text = "0"; }
            if (string.IsNullOrEmpty(txtPercIva.Text)) { txtPercIva.Text = "0"; }
            if (string.IsNullOrEmpty(txtPercIB.Text)) { txtPercIB.Text = "0"; }
            //if (string.IsNullOrEmpty(txtNoGrav.Text)) { txtNoGrav.Text = "0"; }

            if (clsGlobales.ConB == null)
            {
                this.txtIva105.Text = TotalIva105.ToString("#0.00");
                this.txtIva210.Text = TotalIva210.ToString("#0.00");

                TotalFactura = Convert.ToDouble(txtPercVarias.Text) + Convert.ToDouble(txtPercIva.Text) + Convert.ToDouble(txtPercIB.Text) +
                           Exento + SubTotalAcumulado + TotalIva210 + TotalIva105;
            }
            else
            {
                this.txtIva105.Text = "0.00";
                this.txtIva210.Text = "0.00";

                TotalFactura = Convert.ToDouble(txtPercVarias.Text) + Convert.ToDouble(txtPercIva.Text) + Convert.ToDouble(txtPercIB.Text) +
                           Exento + SubTotalAcumulado;
            }

            this.txtNoGrav.Text = Exento.ToString("#0.00");
            
            this.txtTotal.Text = TotalFactura.ToString("#0.00");
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
            //Activar Botones
            this.ActivarBotones();
            // Si hago la llamada desde el form de Cuentas corrientes
            if (!(IdProveedor == 0))
            {
                // Deshabilito el botón para buscar los proveedores
                this.btnBuscar.Enabled = false;
            }
        }

        #endregion

        #region Método que da formato a la tabla de artículos para las órdenes nuevas

        private void FormatearTabla()
        {
            
            // Formateo de las columnas de la tabla
            nuevaFactura.Columns.Add("IdArticulo", typeof(string));
            nuevaFactura.Columns.Add("CodigoArticulo", typeof(string));
            nuevaFactura.Columns.Add("Articulo", typeof(string));
            nuevaFactura.Columns.Add("Tabla", typeof(string));
            nuevaFactura.Columns.Add("AbreviaturaUnidad", typeof(string));
            nuevaFactura.Columns.Add("Precio", typeof(string));
            nuevaFactura.Columns.Add("PorcentajeIva", typeof(string));
        }

        #endregion

        #region Método que setea los TextBox del formulario para las facturas sin órdenes

        private void SetearTextBox()
        {
            this.txtFlete.Text = "0";
            this.txtSubtotal.Text = "0";
            this.txtIva105.Text = "0";
            this.txtIva210.Text = "0";
            this.txtPercVarias.Text = "0";
            this.txtPercIva.Text = "0";
            this.txtPercIB.Text = "0";
            this.txtNoGrav.Text = "0";
            this.txtTotal.Text = "0";
        }

        #endregion

        #region Método que graba la factura

        private void GrabarFactura()
        {
          
            // Si estoy modificando una factura
            if (!(IdFactura == 0))
            {
                GrabarFacturaEditada();
            }
            // Si no estoy modificando una factura
            else
            {
             //Grabo la orden como si fuera nueva
               GrabarFacturaNueva();
            }
            
        }

        #endregion

        #region Método que graba las facturas nuevas

        private void GrabarFacturaNueva()
        {
            //validador numero factura 06-01
            if (string.IsNullOrEmpty(txtNumero.Text))
            {
                MessageBox.Show("Por favor, complete el N° de Factura!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
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
            myComprobanteNuevo.Usuario = txtComprador.Text;
            myComprobanteNuevo.NumReferencia = txtReferencia.Text;
            myComprobanteNuevo.CantidadArticulos = dgvDetalleFactura.RowCount;
            myComprobanteNuevo.Fecha = clsValida.ConvertirFecha(dtpFecha.Value);
            myComprobanteNuevo.FechaReal = DateTime.Now;
            myComprobanteNuevo.Numero = txtNumero.Text;
            myComprobanteNuevo.Vence = clsValida.ConvertirFechaVencimiento("Compras", myComprobanteNuevo.IdCondicionCompra);
            myComprobanteNuevo.Neto = Convert.ToDouble(txtSubtotal.Text);
            myComprobanteNuevo.Iva105 = Convert.ToDouble(txtIva105.Text);
            myComprobanteNuevo.Iva210 = Convert.ToDouble(txtIva210.Text);

            myComprobanteNuevo.Total = Convert.ToDouble(txtTotal.Text);
            myComprobanteNuevo.Saldo = Convert.ToDouble(txtTotal.Text);
            myComprobanteNuevo.Activo = 1;
            myComprobanteNuevo.PercepcionesVarias = Convert.ToDouble(txtPercVarias.Text);
            myComprobanteNuevo.PercepcionesIva = Convert.ToDouble(txtPercIva.Text);
            myComprobanteNuevo.PercepcionesIB = Convert.ToDouble(txtPercIB.Text);
            myComprobanteNuevo.Exento = Convert.ToDouble(txtNoGrav.Text);
            myComprobanteNuevo.IdImputacion = Convert.ToInt32(cboImputacion.SelectedValue);
            myComprobanteNuevo.Compra = 1;
            myComprobanteNuevo.Flete = Convert.ToDouble(txtFlete.Text);

            //VECTOR DE ERRORES
            string[] cErrores = myComprobanteNuevo.cValidaComprobantes();
            //VALIDAR EL OBJETO Y VER SI HAY ERRORES. N.           
            if (!(cErrores[0] == null))
            {
                frmShowErrores myForm = new frmShowErrores();
                myForm.myTitulo = this.Text;
                myForm.miserrores = cErrores.Length;
                myForm.myVector = cErrores;
                myForm.CargarVector();
                myForm.CargarTitulo();
                myForm.ShowDialog();
                return;
            }   

            // Armo la cadena SQL para grabar en la tabla Comprobantes de compras
            myCadenaSQL = "insert into ComprobantesCompras (IdTipoComprobanteCompra, IdProveedor, IdPuntoVenta, IdAlmacen, " + 
                            "IdCondicionCompra, Fecha, FechaReal, Numero, Vence, Usuario, NumReferencia, CantidadArticulos, Neto, " + 
                            "IVA25, IVA50, IVA105, IVA210, IVA270, Total, Saldo, Activo, PercepcionesVarias,PercepcionesIva, " + 
                            "PercepcionesIB, NoGravados, IdImputacion, Compra, Flete) values(" + myComprobanteNuevo.IdTipoComprobanteCompra + ", " +
                                               myComprobanteNuevo.IdProveedor + ", " +
                                               myComprobanteNuevo.IdPuntoVenta + ", " +
                                               myComprobanteNuevo.IdAlmacen + ", " +
                                               myComprobanteNuevo.IdCondicionCompra + ", '" +
                                               myComprobanteNuevo.Fecha + "', '" +
                                               myComprobanteNuevo.FechaReal + "', '" +
                                               myComprobanteNuevo.Numero + "', '" +
                                               myComprobanteNuevo.Vence + "', '" +
                                               myComprobanteNuevo.Usuario + "', '" +
                                               myComprobanteNuevo.NumReferencia + "', " +
                                               myComprobanteNuevo.CantidadArticulos + ", " +
                                               myComprobanteNuevo.Neto + ", 0, 0, " +
                                               myComprobanteNuevo.Iva105 + ", " +
                                               myComprobanteNuevo.Iva210 + ", 0, " +
                                               myComprobanteNuevo.Total + ", " +
                                               myComprobanteNuevo.Saldo + ", " +
                                               myComprobanteNuevo.Activo + ", " +
                                               myComprobanteNuevo.PercepcionesVarias + ", " +
                                               myComprobanteNuevo.PercepcionesIva + ", " +
                                               myComprobanteNuevo.PercepcionesIB + ", " +
                                               myComprobanteNuevo.Exento + ", " +
                                               myComprobanteNuevo.IdImputacion + ", " +
                                               myComprobanteNuevo.Compra + ", " +
                                               myComprobanteNuevo.Flete + ")";
            if (clsGlobales.ConB == null)
            {
                // Grabo en la tabla los datos del comprobante
                clsDataBD.GetSql(myCadenaSQL);
            }
            else
            {
                // Grabo en la tabla los datos del comprobante
                clsDataBD.GetSqlB(myCadenaSQL);
            }
            
            // Grabo en la tabla tipos de comprobantes el número que usé
            string myCadenaNumeroTipoComprobante = "update TiposComprobantesCompras set Numero= " + proximoNumero + " where IdTipoComprobanteCompra= " + tipoComprobante;
            // Grabo en la tabla el número utilizado
            clsDataBD.GetSql(myCadenaNumeroTipoComprobante);
            // Actualizo el saldo del proveedor
            ActualizarSaldoProveedor(myComprobanteNuevo.IdProveedor, myComprobanteNuevo.Total);
            // Grabo el detalle de la cotización
            GrabarDetalleFacturasNuevas();
        }

        #endregion

        #region Método que graba el detalle de las ordenes nuevas

        private void GrabarDetalleFacturasNuevas()
        {
            // Pregunto si desean actualizar los costos de lo comprado
            // DialogResult respuesta = MessageBox.Show("Desea actualizar los costos del los artículos comprados", "CONFIRMACIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            bool bActCosto = Convert.ToBoolean(chkCosto.CheckState);
            bool bActComp = Convert.ToBoolean(this.chkComposicion.CheckState);
            //bool bActPrecio = Convert.ToBoolean(chkPrecio.CheckState);

            // Variable que almacena el Id del comrpbante
            int ultimoComprobante = 0;
            // Si estoy por grabar el detalle de una factura editada
            if (!(IdFactura == 0))
            {
                // Le asigno a la variable últimoComprobante el Id de la factura que estoy modificando
                ultimoComprobante = IdFactura;
            }
            else
            {
                if (clsGlobales.ConB == null)
                {
                    // Obtengo el último Id de la tabla comprobantes de compras del tipo cotización
                    ultimoComprobante = clsDataBD.RetornarUltimoId("ComprobantesCompras", "IdComprobanteCompra");
                }
                else
                {
                    // Obtengo el último Id de la tabla comprobantes de compras del tipo cotización
                    ultimoComprobante = clsDataBD.RetornarUltimoIdB("ComprobantesCompras", "IdComprobanteCompra");
                }
                
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
            foreach (DataGridViewRow row in dgvDetalleFactura.Rows)
            {
                // Almaceno el id del artículo
                IdArticulo = Convert.ToInt32(row.Cells["IdArticulo"].Value);
                // Almaceno la cantidad del artículo
                CantidadArticulo = Convert.ToDouble(row.Cells["Cantidad"].Value);
                // Almaceno el precio del artículo
                PrecioArticulo = Convert.ToDouble(row.Cells["Precio"].Value);
                // Almaceno el tipo de artículo
                tipoArt = row.Cells["Tabla"].Value.ToString();
                // Armo la cadena SQL
                myCadenaSQL = "insert into DetallesComprobantesCompras (IdArticulo, IdComprobanteCompra, Cantidad, Precio, TipoArticulo, Activo) values (" +
                                IdArticulo + ", " +
                                ultimoComprobante + ", " +
                                CantidadArticulo + ", " +
                                PrecioArticulo + ", '" +
                                tipoArt + "',1)";
                if (clsGlobales.ConB == null)
                {
                    // Grabo en la tabla el detalle
                    clsDataBD.GetSql(myCadenaSQL);
                }
                else
                {
                    // Grabo en la tabla el detalle
                    clsDataBD.GetSqlB(myCadenaSQL);
                }
                
                // Actualizo el Stock del artículo
                ActualizarStock(IdArticulo, CantidadArticulo, PrecioArticulo, myComprobanteNuevo.IdAlmacen);
                // Grabo el movimiento de stock
                GrabarMovimientoStock(IdArticulo, 1, myComprobanteNuevo.FechaReal, CantidadArticulo);
                // Si desean actualizar los costos de lo comprado
                if (bActCosto && bActComp) //.T. y .T.
                {
                    ActualizarCostos(IdArticulo, tipoArt, PrecioArticulo);
                }
                else if (bActCosto && (!(bActComp))) //.T. y .F.
                {
                    ActualizarCostos(IdArticulo, tipoArt, PrecioArticulo);
                }
                else if (!(bActCosto) && bActComp) //.F. Y .T.
                {
                    ActualizarComposicion(IdArticulo, tipoArt);
                }
                
            }
            // Salgo del foermulario
            btnCancelar.PerformClick();
        }

        #endregion

        #region Método que graba las facturas editadas
                        
        private void GrabarFacturaEditada()
        {
            
            //VECTOR DE ERRORES
            string[] cErrores = myComprobanteViejo.cValidaComprobantes();
            //VALIDAR EL OBJETO Y VER SI HAY ERRORES. N.           
            if (!(cErrores[0] == null))
            {
                frmShowErrores myForm = new frmShowErrores();
                myForm.myTitulo = this.Text;
                myForm.miserrores = cErrores.Length;
                myForm.myVector = cErrores;
                myForm.CargarVector();
                myForm.CargarTitulo();
                myForm.ShowDialog();
                return;
            }   
            
            // Paso los datos del formulario que pueden haber cambiada a la clase para grabarlos
            myComprobanteViejo.IdPuntoVenta = Convert.ToInt32(cboPunto.SelectedValue);
            myComprobanteViejo.IdAlmacen = Convert.ToInt32(cboAlmacen.SelectedValue);
            myComprobanteViejo.IdCondicionCompra = Convert.ToInt32(cboCondCompra.SelectedValue);
            myComprobanteViejo.Numero = txtNumero.Text;
            myComprobanteViejo.NumReferencia = txtReferencia.Text;
            myComprobanteViejo.CantidadArticulos = dgvDetalleFactura.RowCount;
            myComprobanteViejo.Fecha = clsValida.ConvertirFecha(dtpFecha.Value);
            myComprobanteViejo.Vence = clsValida.ConvertirFechaVencimiento("Compras", myComprobanteViejo.IdCondicionCompra);
            myComprobanteViejo.Neto = Convert.ToDouble(txtSubtotal.Text);
            myComprobanteViejo.Iva105 = Convert.ToDouble(txtIva105.Text);
            myComprobanteViejo.Iva210 = Convert.ToDouble(txtIva210.Text);
            myComprobanteViejo.Total = Convert.ToDouble(txtTotal.Text);
            myComprobanteViejo.Saldo = Convert.ToDouble(txtTotal.Text);
            myComprobanteViejo.Activo = 1;
            myComprobanteViejo.PercepcionesVarias = Convert.ToDouble(txtPercVarias.Text);
            myComprobanteViejo.PercepcionesIva = Convert.ToDouble(txtPercIva.Text);
            myComprobanteViejo.PercepcionesIB = Convert.ToDouble(txtPercIB.Text);
            myComprobanteViejo.Exento = Convert.ToDouble(txtNoGrav.Text);
            myComprobanteViejo.IdImputacion = Convert.ToInt32(cboImputacion.SelectedValue);
            myComprobanteViejo.Compra = 1;
            myComprobanteViejo.Flete = Convert.ToDouble(txtFlete.Text);

            // Armo la cadena SQl para grabar los datos editados
            myCadenaSQL = "Update ComprobantesCompras set IdPuntoVenta = " + myComprobanteViejo.IdPuntoVenta +
                                                         ", IdAlmacen = " + myComprobanteViejo.IdAlmacen +
                                                         ", IdCondicionCompra = " + myComprobanteViejo.IdCondicionCompra +
                                                         ", Fecha = '" + myComprobanteViejo.Fecha +
                                                         "', Vence = '" + myComprobanteViejo.Vence +
                                                         "', Numero = '" + myComprobanteViejo.Numero +
                                                         "', NumReferencia = '" + myComprobanteViejo.NumReferencia +
                                                         "', CantidadArticulos = " + myComprobanteViejo.CantidadArticulos +
                                                         ", Neto = " + myComprobanteViejo.Neto +
                                                         ", IVA105 = " + myComprobanteViejo.Iva105 + 
                                                         ", IVA210 = " + myComprobanteViejo.Iva210 + 
                                                         ", Total = " + myComprobanteViejo.Total +
                                                         ", Saldo = " + myComprobanteViejo.Saldo +
                                                         ", PercepcionesVarias = " + myComprobanteViejo.PercepcionesVarias +
                                                         ", PercepcionesIva = " + myComprobanteViejo.PercepcionesIva +
                                                         ", PercepcionesIB = " + myComprobanteViejo.PercepcionesIB +
                                                         ", NoGravados = " + myComprobanteViejo.Exento +
                                                         ", IdImputacion = " + myComprobanteViejo.Exento +
                                                         ", Compra = " + myComprobanteViejo.Exento +
                                                         ", Flete = " + myComprobanteViejo.Flete +
                                                         " where IdComprobanteCompra = " + IdFactura;
            if (clsGlobales.ConB == null)
            {
                // Grabo en la tabla los datos del comprobante
                clsDataBD.GetSql(myCadenaSQL);
            }
            else
            {
                // Grabo en la tabla los datos del comprobante
                clsDataBD.GetSqlB(myCadenaSQL);
            }
            
            // Grabo el nuevo detalle del comprobante
            GrabarDetalleFacturaEditadas();
        }
        
        #endregion

        #region Método que graba el detalle de las órdenes editadas

        private void GrabarDetalleFacturaEditadas()
        {
            // Descuento el stock del detalle viejo
            DescontarStock();
            // Elimino el detalle anterior
            myCadenaSQL = "delete from DetallesComprobantesCompras where IdComprobanteCompra = " + IdFactura;
            if (clsGlobales.ConB == null)
            {
                // Borro el detalle viejo en la tabla
                clsDataBD.GetSql(myCadenaSQL);
            }
            else
            {
                // Borro el detalle viejo en la tabla
                clsDataBD.GetSqlB(myCadenaSQL);
            }
            
            // Grabo el nuevo detalle
            GrabarDetalleFacturasNuevas();
        }

        #endregion

        #region Método que verifica los datos de la grilla

        private bool ValidarGrillas()
        {
            // Variable que controla que los datos sean correctos en las grilla
            bool verificado = false;
            // Si hay proveedores cargados, verifico que haya artículos en la grilla
            if (dgvDetalleFactura.Rows.Count > 0)
            {
                // Controlo que los artículos de la grilla tengan cantidad
                foreach (DataGridViewRow row in dgvDetalleFactura.Rows)
                {
                    if (!(Convert.ToDouble(row.Cells["Cantidad"].Value) > 0) || !(Convert.ToDouble(row.Cells["Precio"].Value) > 0))
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

        #region Método que carga los datos de la factura a modificar

        private void CargarFacturaEditar()
        {
            DataTable myTablaComprobante;
            
            if (clsGlobales.ConB == null)
            {
                // Armo la cadena SQL
                myCadenaSQL = "select * from Vista_ComprobantesCompras where Id = " + IdFactura;
                // le paso los datos de la consulta SQL
                myTablaComprobante = clsDataBD.GetSql(myCadenaSQL);
            }
            else
            {
                // Armo la cadena SQL
                myCadenaSQL = "select * from Vista_ComprobantesCompras2 where Id = " + IdFactura;
                // le paso los datos de la consulta SQL
                myTablaComprobante = clsDataBD.GetSqlB(myCadenaSQL);
            }

            foreach (DataRow rowComp in myTablaComprobante.Rows)
            {
                myComprobanteViejo = new clsComprobantesCompras();

                myComprobanteViejo.IdComprasCotizaciones = Convert.ToInt32(rowComp["Id"]);
                myComprobanteViejo.IdTipoComprobanteCompra = Convert.ToInt32(rowComp["IdTipo"]);
                myComprobanteViejo.IdProveedor = Convert.ToInt32(rowComp["IdProveedor"]);
                myComprobanteViejo.IdPuntoVenta = Convert.ToInt32(rowComp["IdPunto"]);
                myComprobanteViejo.IdAlmacen = Convert.ToInt32(rowComp["IdAlmacen"]);
                myComprobanteViejo.IdCondicionCompra = Convert.ToInt32(rowComp["IdCondicion"]);
                myComprobanteViejo.Usuario = (rowComp["Usuario"]).ToString();
                myComprobanteViejo.NumReferencia = (rowComp["NumReferencia"]).ToString();
                myComprobanteViejo.CantidadArticulos = Convert.ToInt32(rowComp["Cantidad"]);
                myComprobanteViejo.Fecha = (rowComp["Fecha"]).ToString();
                myComprobanteViejo.Numero = (rowComp["Numero"]).ToString();
                myComprobanteViejo.Vence = (rowComp["Vence"]).ToString();
                myComprobanteViejo.Neto = Convert.ToDouble(rowComp["Neto"]);
                myComprobanteViejo.Activo = Convert.ToInt32(rowComp["Activo"]);
                // Campos exclusivos de la factura de compras
                myComprobanteViejo.Iva105 = Convert.ToDouble(rowComp["IVA105"]);
                myComprobanteViejo.Iva210 = Convert.ToDouble(rowComp["IVA210"]);
                myComprobanteViejo.Total = Convert.ToDouble(rowComp["Total"]);
                myComprobanteViejo.Saldo = Convert.ToDouble(rowComp["Saldo"]);
                myComprobanteViejo.PercepcionesVarias = Convert.ToDouble(rowComp["PercepcionesVarias"]);
                myComprobanteViejo.PercepcionesIva = Convert.ToDouble(rowComp["PercepcionesIva"]);
                myComprobanteViejo.PercepcionesIB = Convert.ToDouble(rowComp["PercepcionesIB"]);
                myComprobanteViejo.Exento = Convert.ToDouble(rowComp["NoGravados"]);
                myComprobanteViejo.IdImputacion = Convert.ToInt32(rowComp["IdImputacion"]);
                myComprobanteViejo.Flete = Convert.ToDouble(rowComp["Flete"]);

                if (Referencias == "")
                {
                    Referencias = myComprobanteViejo.NumReferencia.ToString();
                }
                else
                {
                    Referencias = Referencias + " / " + myComprobanteViejo.NumReferencia.ToString();
                }
                // Cargo la cotización al vector de cotizaciones
                myComprobantes[0] = myComprobanteViejo;

            }
            // Busco los datos del proveedor en la tabla proveedores
            CargarProveedores(myComprobanteViejo.IdProveedor);
            // Paso los datos de las clases al formulario
            PasarDatosAlFormulario();
        }

        #endregion

        #region Método que carga los toolsTip al formulario

        private void CargarToolsTip()
        {
            toolTip1.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip2.SetToolTip(this.btnCancelar, "Cancelar");
            toolTip3.SetToolTip(this.btnAgregarArt, "Agregar Artículo");
            toolTip4.SetToolTip(this.btnQuitarArt, "Quitar Artículo");
            toolTip4.SetToolTip(this.btnBuscar, "Buscar Proveedor");
        }

        #endregion

        #region Método que descuenta el Stock de las facturas Editadas

        private void DescontarStock()
        {
            // Armo la cadena SQL con el viejo detalle de la factura de compra
            myCadenaSQL = "select * from DetallesComprobantesCompras where IdComprobanteCompra = " + IdFactura + " and Activo = 1";
            // Paso a una tabla el detalle viejo de la factura
            DataTable myTablaDetalleViejo = clsDataBD.GetSql(myCadenaSQL);
            // Recorro la tabla 
            foreach (DataRow row in myTablaDetalleViejo.Rows)
            {
                // Asigno a una variable el Id del artículo
                int IdArt = Convert.ToInt32(row["IdArticulo"]);
                // Asigno a una Variable la cantidad del Artículo comprado
                double CantArt = Convert.ToDouble(row["Cantidad"]);
                // Variable que va a contener el stock actual del artículo
                double StockActual = 0;
                // Cargo a una tabla el Artículo para tomar el valor actual del stock
                string myCadenaSQLArticulo = "select * from Articulos where IdArticulo = " + IdArt;
                // Ejecuto la consulta y paso los datos a una tabla
                DataTable myArticuloViejo = clsDataBD.GetSql(myCadenaSQLArticulo);
                // Recorro la tabla que tiene el artículo y tomo el valor de su stock actual
                foreach (DataRow rowArticulo in myArticuloViejo.Rows)
                {
                    // Si el artículo lleva stock
                    if (Convert.ToInt32(rowArticulo["LlevaStock"]) == 1)
                    {
                        // Paso a la variable el stock actual
                        StockActual = Convert.ToDouble(rowArticulo["Stock"]);
                        // Le resto al stock actual la cantidad comprada
                        StockActual = StockActual - CantArt;
                        // Armo la cadena SQL para actualizar el stock del artículo
                        myCadenaSQLArticulo = "update Articulos set Stock = " + StockActual + " where IdArticulo = " + IdArt;
                        // Ejecuto la consulta
                        clsDataBD.GetSql(myCadenaSQLArticulo);
                    }
                    
                }
            }
        }

        #endregion

        #region Método que actualiza el stock del artículo comprado

        private void ActualizarStock(int IdArt, double CantArt, double PrecioA, int Almacen)
        {
            // Variable que almacena el sotck por almacen
            double StockAlmacen = 0;
            // Variable que va a contener el stock actual del artículo
            double StockActual = 0;
            // Cargo a una tabla el Artículo para tomar el valor actual del stock
            string myCadenaSQLArticulo = "select * from Articulos where IdArticulo = " + IdArt;
            // Ejecuto la consulta y paso los datos a una tabla
            DataTable myArticuloViejo = clsDataBD.GetSql(myCadenaSQLArticulo);
            // Recorro la tabla que tiene el artículo y tomo el valor de su stock actual
            foreach (DataRow rowArticulo in myArticuloViejo.Rows)
            {
                // Si el artículo lleva stock
                if (Convert.ToInt32(rowArticulo["LlevaStock"]) == 1)
                {
                    // Paso a la variable el stock actual
                    StockActual = Convert.ToDouble(rowArticulo["Stock"]);
                    // Le resto al stock actual la cantidad comprada
                    StockActual = StockActual + CantArt;
                    // Armo la cadena SQL para actualizar el stock del artículo
                    myCadenaSQLArticulo = "update Articulos set Stock = " + StockActual +
                                          ", UltimoCostoCompra = " + PrecioA +
                                          ", UltimoProveedor = '" + myProveedor.RazonSocial +
                                          "', UltimaCompra = " + myComprobanteNuevo.Fecha +
                                          " where IdArticulo = " + IdArt;

                    // Ejecuto la consulta
                    clsDataBD.GetSql(myCadenaSQLArticulo);

                    /**********************************************************************
                             Sector que graba el stock del artículo por almacen
                    **********************************************************************/ 
                    // Recorro la tabla par ver si ya el artículo para ese almacen tiene cargado stock
                    myCadenaSQLArticulo = "select * from ArticulosAlmacenesStock where IdArticulo = " + IdArt + " and IdAlmacen = " + myComprobanteNuevo.IdAlmacen;
                    // Ejecuto la consulta y paso los datos a una tabla
                    DataTable myTablaAlmacen = clsDataBD.GetSql(myCadenaSQLArticulo);
                    // Si la tabla tiene el artículo, lo modifico, si no ingreso un registro nuevo
                    if (myTablaAlmacen.Rows.Count > 0)
                    {
                        // Variable que almacena el Id del registro
                        int IdArticulosAlmacenesStock = 0;
                        // Recorro la tabla
                        foreach (DataRow row in myTablaAlmacen.Rows)
                        {
                            // Tomo el valor del Stock actual y le sumo el comprado
                            IdArticulosAlmacenesStock = Convert.ToInt32(row["IdArticuloAlmacenStock"]);
                            // Tomo el valor del Stock actual y le sumo el comprado
                            StockAlmacen = Convert.ToDouble(row["Stock"]);
                        }
                        // Al stock del artículo le sumo lo comprado
                        StockAlmacen = StockAlmacen + CantArt;
                        // Armo la cadena SQL
                        myCadenaSQLArticulo = "update ArticulosAlmacenesStock set Stock = " + StockAlmacen 
                                            + " where IdArticuloAlmacenStock = " + IdArticulosAlmacenesStock;
                    }
                    else
                    {
                        // Armo la cadena SQL para insertar un nuevo registro
                        myCadenaSQLArticulo = "insert into ArticulosAlmacenesStock (IdArticulo, IdAlmacen, Stock) values (" +
                                               IdArt + ", " +
                                               Almacen + ", " +
                                               CantArt + ")"; 
                    }
                    // Ejecuto la consulta
                    clsDataBD.GetSql(myCadenaSQLArticulo);
                }
            }
        }

        #endregion

        #region Método que actualiza los costos de los artículos comprados

        private void ActualizarCostos(int IdArt, string tipoAr, double PrecioArt)
        {

            //VERIFICAR SI EL INSUMO QUE SE COMPRA ES COMPUESTO O NO //////////////////

            // Variable que almacenea si el insumo es compuesto
            bool bEsCompuesto = false;
            // Verifico si el artículo es insumo compuesto
            string myCadenaSQL = "select * from Articulos where IdArticulo = " + IdArt;
            // Paso los datos a una tabla
            DataTable myTablaArt = clsDataBD.GetSql(myCadenaSQL);
            // Recorro la tabla
            foreach (DataRow rowArt in myTablaArt.Rows)
            {
                // Paso el dato a la variable
                bEsCompuesto = Convert.ToBoolean(rowArt["CompIns"]);
            }
            /////////////////////////////////////////////////////////////////////////


            //ES INSUMO
            if (tipoAr == "INSUMOS")
            {

                //ES INSUMO NO COMPUESTO
                if (!(bEsCompuesto))
                {
                    //ACTUALIZA COSTO INSUMO ( ARTICULOS Y EN INSUMOS )
                    ///////////////////////////////////////////////////
                    // Armo la cadena SQL para actualizar el precio del artículo
                    string myCadenaSQLArticulo = "update Articulos set Precio = " + PrecioArt + " where IdArticulo = " + IdArt;
                    // Ejecuto la consulta
                    clsDataBD.GetSql(myCadenaSQLArticulo);
                    // Si es un INSUMO, actualizo su costo
                    // Armo la cadena SQL para actualizar el precio del artículo
                    myCadenaSQLArticulo = "update Insumos set Costo = " + PrecioArt + " where IdArticulo = " + IdArt;
                    // Ejecuto la consulta y paso los datos a una tabla
                    clsDataBD.GetSql(myCadenaSQLArticulo);

                    //ACTUALIZA LA COMPOSICION DE LOS PRODUCTOS QUE TENGAN ESE INSUMO
                    /////////////////////////////////////////////////////////////////
                    // Si desean actualizar la composición
                    bool bActCompo = Convert.ToBoolean(chkComposicion.CheckState);
                    // La actualizo
                    if (bActCompo)
                    {
                        ActualizarComposicion(IdArt, tipoAr);
                    }
                    /////////////////////////////////////////////////////////////////

                    //INSUMOS COMPUESTOS ///////////////////////////////////////////////////////////////////////////////////////
                    if (bActCompo)
                    {
                        // Recorro la tabla Insumos para encontrar el IdInsumo por el IdArticulo
                        myCadenaSQL = "select * from Insumos where IdArticulo = " + IdArt;
                        // Paso los datos a una tabla
                        DataTable myTablaSQL = clsDataBD.GetSql(myCadenaSQL);
                        // Variable que almacena el Id del insumo
                        int iIdInsumo = 0;
                        // recorro la tabla y asigno el Id del insumo a una variable
                        foreach (DataRow row in myTablaSQL.Rows)
                        {
                            // Asigno a la variable el Id del insumo
                            iIdInsumo = Convert.ToInt32(row["IdInsumo"]);
                        }

                        string sCadInsComp = "UPDATE InsumosCompuestos SET Costo = " + PrecioArt + " WHERE IdInsCompone = " + iIdInsumo;
                        clsDataBD.GetSql(sCadInsComp);

                        // Recorro la tabla Insumos para encontrar el IdInsumo por el IdArticulo
                        myCadenaSQL = "select * from InsumosCompuestos Where IdInsCompone = " + iIdInsumo;
                        // Paso los datos a una tabla
                        DataTable myTabla = clsDataBD.GetSql(myCadenaSQL);
                        // Variable que almacena el Id del insumo
                        int IdInsOrigen = 0;

                        // Variable que almacena el nuevo costo del insumo
                        double dCostoNuevo = 0;
                        double cTanda = 0;

                        // recorro la tabla y asigno el Id del insumo a una variable
                        foreach (DataRow row in myTabla.Rows)
                        {
                            // Asigno a la variable el Id del insumo
                            IdInsOrigen = Convert.ToInt32(row["IdInsOrigen"]);
                            dCostoNuevo = 0;
                            cTanda = 0;

                            // Recorro la tabla Insumos para encontrar el IdInsumo por el IdArticulo
                            myCadenaSQL = "select * from InsumosCompuestos where IdInsOrigen = " + IdInsOrigen;
                            // Paso los datos a una tabla
                            DataTable myTabla1 = clsDataBD.GetSql(myCadenaSQL);

                            // recorro la tabla y asigno el Id del insumo a una variable
                            foreach (DataRow row1 in myTabla1.Rows)
                            {
                                // Asigno a la variable el nuevo costo del insumo
                                dCostoNuevo += Convert.ToDouble(row1["Costo"]) * Convert.ToDouble(row1["Cantidad"]);
                                cTanda = Convert.ToDouble(row1["cTanda"]);
                            }


                            // Armo la cadena SQL para actualizar el precio del artículo
                            myCadenaSQL = "update Articulos set Precio = " + (dCostoNuevo / cTanda) + " where IdArticulo = " + IdInsOrigen;
                            // Ejecuto la consulta
                            clsDataBD.GetSql(myCadenaSQL);
                            // Si es un INSUMO, actualizo su costo
                            // Armo la cadena SQL para actualizar el precio del artículo
                            myCadenaSQL = "update Insumos set Costo = " + (dCostoNuevo / cTanda) + " where IdArticulo = " + IdInsOrigen;
                            // Ejecuto la consulta y paso los datos a una tabla
                            clsDataBD.GetSql(myCadenaSQL);

                            //ACTUALIZAR COMPOSICION PRODUCTOS QUE TENGAN INSUMO COMPUESTO
                            ActualizarComposicionIC(IdInsOrigen, tipoAr);
                            //////////////////////////////////////////////////////////////
                        }


                        //PRODUCTOS COMPUESTOS ///////////////////////////////////////////////////////////////////////////////////////
                        if (bActCompo)
                        {
                            //VARIABLES
                            int IdProdOrigen = 0;
                            double cPrecio = 0;
                            int IdProdCompone = 0;

                            // Recorro la tabla Insumos para encontrar el IdInsumo por el IdArticulo
                            myCadenaSQL = "select distinct IdProdOrigen FROM ProductosCompuestos";
                            // Paso los datos a una tabla
                            DataTable myTabla2 = clsDataBD.GetSql(myCadenaSQL);

                            // recorro la tabla y asigno el Id del insumo a una variable
                            foreach (DataRow row2 in myTabla2.Rows)
                            {
                                // Asigno a la variable el nuevo costo del insumo
                                IdProdOrigen = Convert.ToInt32(row2["IdProdOrigen"]);

                                // Recorro la tabla Insumos para encontrar el IdInsumo por el IdArticulo
                                myCadenaSQL = "select * from ProductosCompuestos Where IdProdOrigen = " + IdProdOrigen;
                                // Paso los datos a una tabla
                                DataTable myTabla3 = clsDataBD.GetSql(myCadenaSQL);

                                foreach (DataRow row3 in myTabla3.Rows)
                                {

                                    // Asigno a la variable el nuevo costo del insumo
                                    IdProdCompone = Convert.ToInt32(row3["IdProdCompone"]);

                                    myCadenaSQL = "Select precio from Articulos Where IdArticulo = " + IdProdCompone;
                                    DataTable myTabla4 = clsDataBD.GetSql(myCadenaSQL);

                                    foreach (DataRow row4 in myTabla4.Rows)
                                    {
                                        cPrecio += Convert.ToDouble(row4["Precio"]) * Convert.ToDouble(row3["Cantidad"]);
                                    }
                                }

                                //UPDATE
                                myCadenaSQL = "UPDATE Articulos SET Precio = " + cPrecio + " Where IdArticulo = " + IdProdOrigen;
                                clsDataBD.GetSql(myCadenaSQL);
                            }
                        }
                    }                    
                }             
            }                
        }

        #endregion

        #region Método que actualiza los Precios de los artículos comprados

        private void ActualizarPrecios(int IdArt, double PrecioArt)
        {
            // Armo la cadena SQL para actualizar el precio del artículo
            string myCadenaSQLArticulo = "update Articulos set Precio = " + PrecioArt + " where IdArticulo = " + IdArt;
            // Ejecuto la consulta
            //clsDataBD.GetSql(myCadenaSQLArticulo);
        }

        #endregion

        #region Método que actualiza la composición de los productos

        private void ActualizarComposicion(int IdArt, string sTabla)
        {

            
            // Variable que almacenea si el insumo es compuesto
            bool bEsCompuesto = false;
            // Verifico si el artículo es insumo compuesto
            string myCadenaSQL = "select * from Articulos where IdArticulo = " + IdArt;
            // Paso los datos a una tabla
            DataTable myTablaArt = clsDataBD.GetSql(myCadenaSQL);
            // Recorro la tabla
            foreach (DataRow rowArt in myTablaArt.Rows)
            {
                // Paso el dato a la variable
                bEsCompuesto = Convert.ToBoolean(rowArt["CompIns"]);
            }

            // Recorro la tabla Insumos para encontrar el IdInsumo por el IdArticulo
            myCadenaSQL = "select * from Insumos where IdArticulo = " + IdArt;
            // Paso los datos a una tabla
            DataTable myTabla = clsDataBD.GetSql(myCadenaSQL);
            // Variable que almacena el Id del insumo
            int iIdInsumo = 0;
            
            // Variable que almacena el nuevo costo del insumo
            double dCostoNuevo = 0;
            // recorro la tabla y asigno el Id del insumo a una variable
            foreach (DataRow row in myTabla.Rows)
            {
                // Asigno a la variable el Id del insumo
                iIdInsumo = Convert.ToInt32(row["IdInsumo"]);
                // Asigno a la variable el nuevo costo del insumo
                dCostoNuevo = Convert.ToDouble(row["Costo"]);
            }

            if (!(bEsCompuesto))
            {
                // COn el datos del Id del insumo, recorro los datos de la tabla que tiene la composición de los productos
                myCadenaSQL = "select * from ProductosInsumos where IdInsumo = " + iIdInsumo;
                // Paso los datos a una tabla
                DataTable myTablaComp = clsDataBD.GetSql(myCadenaSQL);
                // Variable que almacena el Id de la composición
                int iIdComp = 0;
                // Variable que almacena el Id del Producto
                int iIdProd = 0;
                // Variable que almacena el costo viejo del insumo en la composición
                double dCostoViejo = 0;
                // Cantidad del insumo en la composición
                double dCant = 0;
                // Variable que almacena la diferencia de los costos
                double dDiferenciaCostos = 0;
                
                // Si 
                // Recorro la tabla para tomar el Id del prodcuto que en su composicion tiene el Insumo que actualizó su costo
                foreach (DataRow rowComp in myTablaComp.Rows)
                {
                    // Paso el Id de la composición a la variable
                    iIdComp = Convert.ToInt32(rowComp["IdProductoInsumo"]);
                    // Paso el dato del Id del producto a la variable
                    iIdProd = Convert.ToInt32(rowComp["IdProducto"]);
                    // Tomo en la variable el costo viejo del insumo
                    dCostoViejo = Convert.ToDouble(rowComp["Costo"]);
                    // Tomo en la variable la cantidad del insumo en la composicion
                    dCant = Convert.ToDouble(rowComp["Cantidad"]);
                    // al nuevo costo le resto el anterior
                    dDiferenciaCostos = (dCostoNuevo - dCostoViejo) * dCant;
                    // Cambio el costo en la composición 
                    myCadenaSQL = "update ProductosInsumos set Costo = " + dCostoNuevo + " where IdProductoInsumo = " + iIdComp;
                    // Ejecuto la consulta
                    clsDataBD.GetSql(myCadenaSQL);

                    ///////////////////////////////////////////////////////////////////////
                    if (iIdProd != 0)
                    {
                        // Actualizo el costo acumulado
                        CalcularComposicion(iIdProd, false);
                    }
                }                
            }            
        }

        #endregion

        #region Método que actualiza la composicion de productos en base a IC

        private void ActualizarComposicionIC(int IdArt, string sTabla)
        {

            // Recorro la tabla Insumos para encontrar el IdInsumo por el IdArticulo
            myCadenaSQL = "select * from Insumos where IdArticulo = " + IdArt;
            // Paso los datos a una tabla
            DataTable myTabla = clsDataBD.GetSql(myCadenaSQL);
            // Variable que almacena el Id del insumo
            int iIdInsumo = 0;

            // Variable que almacena el nuevo costo del insumo
            double dCostoNuevo = 0;
            // recorro la tabla y asigno el Id del insumo a una variable
            foreach (DataRow row in myTabla.Rows)
            {
                // Asigno a la variable el Id del insumo
                iIdInsumo = Convert.ToInt32(row["IdInsumo"]);
                // Asigno a la variable el nuevo costo del insumo
                dCostoNuevo = Convert.ToDouble(row["Costo"]);
            }

            // COn el datos del Id del insumo, recorro los datos de la tabla que tiene la composición de los productos
            myCadenaSQL = "select * from ProductosInsumos where IdInsumo = " + iIdInsumo;
            // Paso los datos a una tabla
            DataTable myTablaComp = clsDataBD.GetSql(myCadenaSQL);
            // Variable que almacena el Id de la composición
            int iIdComp = 0;
            // Variable que almacena el Id del Producto
            int iIdProd = 0;
            // Variable que almacena el costo viejo del insumo en la composición
            double dCostoViejo = 0;
            // Cantidad del insumo en la composición
            double dCant = 0;
            // Variable que almacena la diferencia de los costos
            double dDiferenciaCostos = 0;

            // Si 
            // Recorro la tabla para tomar el Id del prodcuto que en su composicion tiene el Insumo que actualizó su costo
            foreach (DataRow rowComp in myTablaComp.Rows)
            {
                // Paso el Id de la composición a la variable
                iIdComp = Convert.ToInt32(rowComp["IdProductoInsumo"]);
                // Paso el dato del Id del producto a la variable
                iIdProd = Convert.ToInt32(rowComp["IdProducto"]);
                // Tomo en la variable el costo viejo del insumo
                dCostoViejo = Convert.ToDouble(rowComp["Costo"]);
                // Tomo en la variable la cantidad del insumo en la composicion
                dCant = Convert.ToDouble(rowComp["Cantidad"]);
                // al nuevo costo le resto el anterior
                dDiferenciaCostos = (dCostoNuevo - dCostoViejo) * dCant;
                // Cambio el costo en la composición 
                myCadenaSQL = "update ProductosInsumos set Costo = " + dCostoNuevo + " where IdProductoInsumo = " + iIdComp;
                // Ejecuto la consulta
                clsDataBD.GetSql(myCadenaSQL);

                ///////////////////////////////////////////////////////////////////////
                if (iIdProd != 0)
                {
                    // Actualizo el costo acumulado
                    CalcularComposicion(iIdProd, false);
                }
            }
        }

        #endregion

        #region Método que actualiza el saldo del proveedor

        private void ActualizarSaldoProveedor(int IdProv, double Tot)
        {
            // Variable que almacena el saldo del proveedor
            double SaldoProv = 0;
            // Pregunto si está abierta la conexión B
            if (clsGlobales.ConB == null)
            {
                // Armo la cadena SQl para traer el saldo anterior del proveedor
                string myCadenaSQLSaldo = "select * from Proveedores where IdProveedor = " + IdProv;
                // Ejecuto la consulta y paso los datos a la tabla
                DataTable mySaldoProveedor = clsDataBD.GetSql(myCadenaSQLSaldo);
                // Recorro la tabla para obtener el saldo inicial del proveedor
                foreach (DataRow rowProv in mySaldoProveedor.Rows)
                {
                    // Paso a la variable el saldo anterior
                    SaldoProv = Convert.ToDouble(rowProv["Saldo"]);
                    // Actualizo el saldo
                    SaldoProv = SaldoProv + Tot;
                }
                // Armo la consulta para actualizar el dato
                myCadenaSQLSaldo = "update Proveedores set Saldo = " + SaldoProv + " where IdProveedor = " + IdProv;
                // Actualizo el saldo
                clsDataBD.GetSql(myCadenaSQLSaldo);
            }
            else
            {

                // Armo la cadena SQl para traer el saldo anterior del proveedor
                string myCadenaSQLSaldo = "select * from SaldoCliProv where IdProveedor = " + IdProv;
                string myCad = "";
                // Ejecuto la consulta y paso los datos a la tabla
                DataTable mySaldoProveedor = clsDataBD.GetSqlB(myCadenaSQLSaldo);

                //No existe... lo crea
                if (mySaldoProveedor.Rows.Count == 0)
                {
                    myCad = "insert into SaldoCliProv (IdCliente, SaldoCli, IdProveedor, SaldoProv, SaldoInicial, SaldoAFavor) values (" +
                                    "0, 0," + IdProv + ",0, 0, 0)";
                    // Ejecuto la consulta 
                    clsDataBD.GetSqlB(myCad);

                    //Update saldo
                    myCadenaSQLSaldo = "update SaldoCliProv set SaldoProv = " + Tot + " where IdProveedor = " + IdProv;
                    // Actualizo el saldo
                    clsDataBD.GetSqlB(myCadenaSQLSaldo);

                }
                else //Recorre y updatea
                {
                    // Recorro la tabla para obtener el saldo inicial del proveedor
                    foreach (DataRow rowProv in mySaldoProveedor.Rows)
                    {
                        // Paso a la variable el saldo anterior
                        SaldoProv = Convert.ToDouble(rowProv["SaldoProv"]);
                        // Actualizo el saldo
                        SaldoProv = SaldoProv + Tot;
                    }
                    // Armo la consulta para actualizar el dato
                    myCadenaSQLSaldo = "update SaldoCliProv set SaldoProv = " + SaldoProv + " where IdProveedor = " + IdProv;
                    // Actualizo el saldo
                    clsDataBD.GetSqlB(myCadenaSQLSaldo);
                }

            }
            
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

            if (clsGlobales.ConB == null)
            {
                // Ejecuto la consulta
                clsDataBD.GetSql(myCadenaSql);
            }
            else
            {
                // Ejecuto la consulta
                clsDataBD.GetSqlB(myCadenaSql);
            }
        }

        #endregion

        #region Método que Actualiza el valor de la composición de un producto

        private void CalcularComposicion(int idPr, bool bInsC)
        {
            if (!(bInsC))
            {
                // Recorro la tabla de composición y traigo todos los insumos que componen el producto
                myCadenaSQL = "select * from ProductosInsumos where IdProducto = " + idPr;
                // Paso los datos a una tabla
                DataTable myTablaComposicion = clsDataBD.GetSql(myCadenaSQL);

                // Variable que va acumulando el costeo
                double dCostoParcial = 0;
                // Variable que almacena el costo total
                double dCostoTotal = 0;
                // Variable que almacena la cantidad de la tanda
                double iTanda = 0;
                // Variable que almacena el costo de los Gastos
                double dGastosAcumulados = 0;
                // Variable que almacena el Id del artículo
                int iIdArt = 0;                
                // Recorro la tabla y sumo los costos de todos los insumos
                foreach (DataRow row in myTablaComposicion.Rows)
                {
                    dCostoParcial = dCostoParcial + Convert.ToDouble(row["Cantidad"]) * Convert.ToDouble(row["Costo"]);
                }

                // Recorro la tabla productos
                myCadenaSQL = "select * from Productos where IdProducto = " + idPr;
                // Ejecuto la consulta y paso los datos a una tabla
                DataTable myTablaProductos = clsDataBD.GetSql(myCadenaSQL);

                // Recorro la tabla y paso los valores a las variables
                foreach (DataRow rowProd in myTablaProductos.Rows)
                {
                    // Asigno a la variable la cantidad de la tanda
                    iTanda = Convert.ToInt32(rowProd["Tanda"]);
                    // Asigno a la variable el valor de los costos de gastos del producto
                    dGastosAcumulados = Convert.ToInt32(rowProd["CostoGastos"]);
                    // Paso a la variable el Id del artículo
                    iIdArt = Convert.ToInt32(rowProd["IdArticulo"]);

                }
                // Calculo el costo total acumulado del producto
                dCostoTotal = dCostoParcial;

                // Hago el calculo final del acumulado
                double dFinalAcumulado = dCostoTotal + dGastosAcumulados;

                // Actualizo el costo acumulado de los insumos y del producto
                myCadenaSQL = "update Productos set CostoInsumos = " + dCostoTotal + ", CostoAcumulado = " + dFinalAcumulado +
                            " where IdProducto = " + idPr;
                // ejecuto la consulta
                clsDataBD.GetSql(myCadenaSQL);
                // Actualizo el precio del producto
                myCadenaSQL = "update Articulos set Precio = " + (dFinalAcumulado / iTanda) + " where IdArticulo = " + iIdArt;
                // ejecuto la consulta
                clsDataBD.GetSql(myCadenaSQL);
            }
           
        }

        #endregion

        #region Metodo ObtenerPermiso

        private bool ObtenerPermiso(int p_Det = 0, int p_Menu = 0, int p_IdUser = 0)
        {
            bool bRetorno = false;

            string myCad = "Select Habilitado from MenuOpcionesUser Where IdDetMenu = " + p_Det + " AND IdMenu = " + p_Menu
                            + " AND IdUser = " + p_IdUser;
            DataTable myDataVal = clsDataBD.GetSql(myCad);

            foreach (DataRow row in myDataVal.Rows)
            {
                if (Convert.ToInt32(row["Habilitado"].ToString()) == 1)
                { bRetorno = true; }
                else { bRetorno = false; }

            }

            //Retornar valor obtenido
            return bRetorno;
        }

        #endregion

        #endregion

    }
}

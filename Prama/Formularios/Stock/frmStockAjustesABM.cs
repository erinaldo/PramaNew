using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prama.Formularios.Stock
{
    public partial class frmStockAjustesABM : Form
    {
       
        #region Variables del formulario

        // Variable que almacena las cadenas SQL
        string myCadenaSQL = "";
        // Creo un datatable para el source de la grilla cuando se cargan comprobantes
        DataTable myTabla = new DataTable();

        #endregion

        #region Constructor

        public frmStockAjustesABM()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos del Formulario

        #region Evento Load

        private void frmStockAjustesABM_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            // Cargo el combo de Coeficiente
            clsDataBD.CargarCombo(cboMotivo, "StockMotivos", "StockMotivo", "IdStockMotivo", "Show = 1");
            // Vacío el combo
            cboMotivo.SelectedIndex = -1;
            // Cargo los toolstip de los botones
            CargarToolsTip();
            // Deshabilito el reordenamiento de las grillas por sus cabeceras
            DeshabilitarOrdenGrillas();
            //Activar Botones
            this.ActivarBotones();
            //Titulo Ventana
            this.Text = clsGlobales.cFormato.getTituloVentana() + " - AJUSTE MANUAL DE STOCK";
            // Cargo el usuario al textBox
            txtUsuario.Text = clsGlobales.UsuarioLogueado.Usuario;
            //Titulo Ventana
            this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;

        }

        #endregion

        #region Eventos de los botones

        #region Evento Click del botón AgregarArt

        private void btnAgregarArt_Click(object sender, EventArgs e)
        {

            // Creo una variable que guarda el largo del vector de Ids
            int LargoInsumos = clsGlobales.InsumosSeleccionados.GetLength(0);
            int LargoProductos = clsGlobales.ProductosSeleccionados.GetLength(0);

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
                myCadenaSQL = "select * from Articulos_Insumos_Productos_1 where IdArticulo in (" + sArt + ")";
                // Creo una tabla que me va a almacenar el resultado de la consulta
                DataTable myTablaAux = clsDataBD.GetSql(myCadenaSQL);
                // Asigno la tabla al source de la grilla de proveedores
                /*int x = 0;*/
                // Variables para formatear el pasaje de precio y cantidad a los datable y de ahí a la grilla
                double auxPrecio = 0;

                //Limpiar grilla detalle
                this.dgvDetalle.Rows.Clear();

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

                    auxPrecio = Convert.ToDouble(row["Precio"]);

                    // Creo una nueva fila para la grilla                         

                    dgvDetalle.Rows.Add();

                    // Cuento las filas de la grilla
                    int Filas = dgvDetalle.Rows.Count;

                    // Si la grilla no está vacía
                    if (Filas > 0)
                    {
                        // Posiciono la grilla en la última fila
                        dgvDetalle.CurrentCell = dgvDetalle[0, Filas - 1];
                    }

                    //Cargo los datos a la grilla       
                    dgvDetalle.CurrentRow.Cells["IdArticulo"].Value = row["IdArticulo"];
                    dgvDetalle.CurrentRow.Cells["CodigoArticulo"].Value = row["CodigoArticulo"];
                    dgvDetalle.CurrentRow.Cells["Articulo"].Value = row["Articulo"];
                    dgvDetalle.CurrentRow.Cells["Unidad"].Value = row["AbreviaturaUnidad"];
                    dgvDetalle.CurrentRow.Cells["Stock"].Value = row["Stock"];
                    dgvDetalle.CurrentRow.Cells["Tabla"].Value = row["Tabla"];
                    // Si es una factura con órdenes de compra

                }

                // Asigno el source a la grilla
                //dgvDetalleFactura.DataSource = myTabla;

                // Si la grilla tiene artículos
                if (dgvDetalle.RowCount > 0)
                {
                    // Habilito la carga de artículos
                    gpbArticulos.Enabled = true;
                    // Habilito el botón quitar
                    btnQuitarArt.Enabled = true;
                    // Creo un contador
                    int fila = 1;
                    // Recorro la grilla
                    foreach (DataGridViewRow row in dgvDetalle.Rows)
                    {
                        // Asigno el valor del contador al Item de la fila
                        row.Cells["Item"].Value = fila;
                        // Incremento el contador
                        fila++;
                    }
                    //Activar Botones
                    this.ActivarBotones();
                    // Paso el foco a la grilla
                    dgvDetalle.Focus();
                }
            }
        }

        #endregion

        #region Evento Click del botón QuitarArt

        private void btnQuitarArt_Click(object sender, EventArgs e)
        {
            // Creo una variable que guarda el largo del vector de Ids
            int LargoInsumos = clsGlobales.InsumosSeleccionados.GetLength(0);
            int LargoProductos = clsGlobales.ProductosSeleccionados.GetLength(0);

            // Verifico si el articulo que voy a borrar es Insumo
            if (dgvDetalle.CurrentRow.Cells["Tabla"].Value.ToString() == "INSUMOS")
            {
                // Recorro el vector
                for (int i = 0; i < LargoInsumos; i++)
                {
                    // Si el insumo que quiero borrar está en el vector
                    if (clsGlobales.InsumosSeleccionados[i, 0] == Convert.ToDouble(dgvDetalle.CurrentRow.Cells["IdArticulo"].Value))
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

            // Elimino la fila de la grilla
            dgvDetalle.Rows.RemoveAt(dgvDetalle.CurrentRow.Index);
            // Si ya no quedan artículos en la grilla, secativo el botón de quitar artículos
            if (dgvDetalle.RowCount == 0)
            {
                // Deshabilito el botón quitar
                btnQuitarArt.Enabled = false;
                // Deshabilito el botón aceptar
                btnAceptar.Enabled = false;

            }
            //Activar Botones
            this.ActivarBotones();
        
        }

        #endregion

        #region Evento Click del botón Aceptar

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Valido los datos
            bool bValidado = ValidadDatos();
            // Si los datos están bien, grabo
            if (bValidado)
            {
                // Grabo primero el ajuste
                GrabarAjuste();
                // Grabo su detalle
                GrabarDetalle();
                // Grabo los cambios al stock y los movimientos
                ActualizarStock();
                // Salgo del formulario a través del evento del botón cancelar
                btnCancelar.PerformClick();
            }
            else
            {
                // Informo que los datos no son correctos
                MessageBox.Show("Complete los datos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        #endregion

        #region Evento Click del botón Cancelar

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Vacío los vectores de insumos y productos
            VaciarVectoresGlobales();
            // Cierro el formulario
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
        }

        #endregion

        #region Método que deshabilita el reordenamiento de las grilla desde sus cabeceras

        private void DeshabilitarOrdenGrillas()
        {
            // Deshabilito la reordenación de las columnas de las grillas
            foreach (DataGridViewColumn dgvCol in dgvDetalle.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;

            }
        }

        #endregion

        #region Método que controla botones del formulario

        private void ActivarBotones()
        {
            this.btnAceptar.TabStop = true;
            this.btnAceptar.Enabled = true;
            this.btnQuitarArt.TabStop = true;
            this.btnQuitarArt.Enabled = true;

            // Si la grilla no tiene detalle
            if (dgvDetalle.Rows.Count == 0)
            {
                this.btnAceptar.TabStop = false;
                this.btnAceptar.Enabled = false;
                this.btnQuitarArt.TabStop = false;
                this.btnQuitarArt.Enabled = false;
            }

        }

        #endregion

        #region Método que vacía los vectores globales para nuevo uso

        private void VaciarVectoresGlobales()
        {
            // Vacío de datos el vector de los Insumos
            clsGlobales.InsumosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.InsumosSeleccionados, new int[] { 0, 2 });
            // Vacío de datos el vector de los Insumos
            clsGlobales.ProductosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.ProductosSeleccionados, new int[] { 0, 2 });
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

        #region Método que graba el ajuste

        private void GrabarAjuste()
        {
            // Guardo los datos del ajuste
            myCadenaSQL = "insert into StockAjustes (Fecha, FechaReal, IdStockMotivo, Usuario, Descripcion, CantItems, Activo) values ('" +
                            clsValida.ConvertirFecha(dtpFecha.Value) + "', '" +
                            dtpFecha.Value + "', " +
                            cboMotivo.SelectedValue + ", '" +
                            clsGlobales.UsuarioLogueado.Usuario + "', '" +
                            txtDescripcion.Text + "', " +
                            dgvDetalle.Rows.Count + ",1)";
            // Ejecuto la consulta
            clsDataBD.GetSqlB(myCadenaSQL);
        }

        #endregion

        #region Método que graba el detalle del ajuste

        private void GrabarDetalle()
        {
            // Variable que guarda el Id del ajuste
            int IdAjusteStock = clsDataBD.RetornarUltimoIdB("StockAjustes", "IdStockAjuste");
            // Variable que guarda el Id del Artículo
            int IdArticulo = 0;
            // Variable que almacena la cantidad
            double dCantidad = 0;
            
            // Recorro la grilla de los artículos para grabar el detalle
            foreach (DataGridViewRow row in dgvDetalle.Rows)
            {
                // Almaceno el id del artículo
                IdArticulo = Convert.ToInt32(row.Cells["IdArticulo"].Value);
                dCantidad = Convert.ToDouble(row.Cells["Cantidad"].Value);

                // Armo la cadena sql
                myCadenaSQL = "insert into StockAjusteDetalles (IdStockAjuste, IdArticulo, Cantidad, Activo) values (" +
                            IdAjusteStock + "," +
                            IdArticulo + "," +
                            dCantidad + ",1)";
                // Ejecuto la consulta
                clsDataBD.GetSqlB(myCadenaSQL);

            }
        }

        #endregion

        #region Método que valida los datos antes de grabar

        private bool ValidadDatos()
        {
            bool aux = false;

            // Valido los datos
            if ((dtpFecha.Value <= DateTime.Now) && (cboMotivo.SelectedIndex != -1) && (txtDescripcion.Text != "") && (dgvDetalle.Rows.Count > 0))
            {
                aux = true;
            }
            // Retorno la variable
            return aux;
        }

        #endregion

        #region Método que graba los cambios  a los stock de los artículos

        private void ActualizarStock()
        {
            // Mensaje de confirmación
            DialogResult myRespuesta = MessageBox.Show("Confirma el Movimiento " + cboMotivo.Text + " ?",
                                        "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Si confirma el movimiento
            if (myRespuesta == DialogResult.Yes)
            {
                // Verifico si es entrada
                bool bEntrada = RetornarEntrada(Convert.ToInt32(cboMotivo.SelectedValue));
                // Variable auxiliares para cálculos
                double stockOriginal = 0;
                double dCantMov = 0;
                double dCant = 0;
                
                // Recorro la grilla
                foreach (DataGridViewRow NewRow in dgvDetalle.Rows)
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
                    GrabarMovimientoStock(Convert.ToInt32(NewRow.Cells["IdArticulo"].Value), Convert.ToInt32(cboMotivo.SelectedValue), DateTime.Now, dCantMov);
                }
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
            clsDataBD.GetSqlB(myCadenaSql);
        }

        #endregion

        #endregion

    }
}

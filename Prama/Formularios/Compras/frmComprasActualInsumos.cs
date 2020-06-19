using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prama.Formularios.Compras
{
    public partial class frmComprasActualInsumos : Form
    {

        #region Variables del formulario

        // Variable que almacena las cadenas SQL
        string myCadenaSQL = "";
        // Creo un datatable para el source de la grilla cuando se cargan comprobantes
        DataTable myTabla = new DataTable();

        #endregion

        #region Constructor del formulario

        public frmComprasActualInsumos()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos del formulario

        #region Evento Load del formulario

        private void frmComprasActualInsumos_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            // Cargo los toolstip de los botones
            CargarToolsTip();
            // Vaciar Vectores Globales
            VaciarVectoresGlobales();
            // Deshabilito el reordenamiento de las grillas por sus cabeceras
            DeshabilitarOrdenGrillas();
            //Activar Botones
            this.ActivarBotones();
            this.Text = clsGlobales.cFormato.getTituloVentana() + " - ACTUALIZACIÓN COSTOS DE INSUMOS";

            //.T. o .F. Actualizar Composicion
            this.chkComposicion.Enabled = this.ObtenerPermiso(8, 1, clsGlobales.UsuarioLogueado.IdUsuario);
        }

        #endregion

        #region Evento Click del botón btnAgregarArt

        private void btnAgregarArt_Click(object sender, EventArgs e)
        {
            // bool bInsumosChk = Convert.ToBoolean(myProveedor.ProvIns);
            // bool bProductos = Convert.ToBoolean(myProveedor.ProvProd);

            // Creo una variable que guarda el largo del vector de Ids
            int LargoInsumos = clsGlobales.InsumosSeleccionados.GetLength(0);
            int LargoProductos = clsGlobales.ProductosSeleccionados.GetLength(0);

            // Creo un nuevo formulario de la clase y lo instancio
            frmArticulosBuscar myForm = new frmArticulosBuscar(true, false);
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
                    dgvDetalle.CurrentRow.Cells["PrecioActual"].Value = auxPrecio.ToString("#0.00000");
                    dgvDetalle.CurrentRow.Cells["PrecioNuevo"].Value = auxPrecio.ToString("#0.00000");
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

        #region Evento Click del botón btnQuitarArt

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
            // Paso a la variable el estado del chk actualizar costo
            bool bActCosto = Convert.ToBoolean(chkCosto.CheckState);
            // Si desean actualizar la composición
            bool bActCompo = Convert.ToBoolean(chkComposicion.CheckState);

            // Variable que guarda el Id del Artículo
            int IdArticulo = 0;
            // Variable que almacena el precio del artículo
            double PrecioNuevo = 0;
            // Variable que almacena el precio anterior de artículo
            double PrecioAnterior = 0;
            // Variable que almacena el tipo de articulo
            string tipoArt = "";
            // Recorro la grilla de los artículos para grabar el detalle
            foreach (DataGridViewRow row in dgvDetalle.Rows)
            {
                // Almaceno el id del artículo
                IdArticulo = Convert.ToInt32(row.Cells["IdArticulo"].Value);
                // Almaceno el nuevo precio del artículo
                PrecioNuevo = Convert.ToDouble(row.Cells["PrecioNuevo"].Value);
                // Almaceno el precio original del artículo
                PrecioAnterior = Convert.ToDouble(row.Cells["PrecioActual"].Value);
                // Almaceno el tipo de artículo
                tipoArt = row.Cells["Tabla"].Value.ToString();

                //if (bActCosto)
                //{
                //    ActualizarCostos(IdArticulo, tipoArt, PrecioNuevo, PrecioAnterior);                
                //}

                // Si desean actualizar los costos de lo comprado
                if (bActCosto && bActCompo) //.T. y .T.
                {
                    ActualizarCostos(IdArticulo, tipoArt, PrecioNuevo, PrecioAnterior);
                }
                else if (bActCosto && (!(bActCompo))) //.T. y .F.
                {
                    ActualizarCostos(IdArticulo, tipoArt, PrecioNuevo, PrecioAnterior);
                }
                else if (!(bActCosto) && bActCompo) //.F. Y .T.
                {
                    ActualizarComposicion(IdArticulo, tipoArt);
                }

            }

            ///////////////////////////////////////////////////////////////////////////

            //////////////////////////////////////////////////////////////////////////
            
            // Salgo del formulario a través del evento del botón cancelar
            btnCancelar.PerformClick();
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

        #region Métodos del Formulario

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

        #region Método que actualiza los costos de los insumos

        private void ActualizarCostos(int IdArt, string tipoAr, double PrecioNue, double PrecioAnt)
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

            if (tipoAr == "INSUMOS")
            {
                string myCadenaSQLArticulo = "";

                //ES INSUMO NO COMPUESTO
                if (!(bEsCompuesto))
                {

                    if (PrecioNue == PrecioAnt)
                    {
                        // Armo la cadena SQL para actualizar el precio del artículo
                        myCadenaSQLArticulo = "update Articulos set Precio = " + PrecioNue + " where IdArticulo = " + IdArt;
                    }
                    else
                    {
                        // Armo la cadena SQL para actualizar el precio del artículo
                        myCadenaSQLArticulo = "update Articulos set Precio = " + PrecioNue + ", PrecioAnterior = " +
                            PrecioAnt + " where IdArticulo = " + IdArt;
                    }

                    // Ejecuto la consulta
                    clsDataBD.GetSql(myCadenaSQLArticulo);
                    // Si es un INSUMO, actualizo su costo
                    // Armo la cadena SQL para actualizar el precio del artículo
                    myCadenaSQLArticulo = "update Insumos set Costo = " + PrecioNue + " where IdArticulo = " + IdArt;
                    // Ejecuto la consulta y paso los datos a una tabla
                    clsDataBD.GetSql(myCadenaSQLArticulo);

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

                        string sCadInsComp = "UPDATE InsumosCompuestos SET Costo = " + PrecioNue + " WHERE IdInsCompone = " + iIdInsumo;
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
                            int IdProdCompone = 0;
                            double cPrecio = 0;

                            // Recorro la tabla Insumos para encontrar el IdInsumo por el IdArticulo
                            myCadenaSQL = "select distinct IdProdOrigen FROM ProductosCompuestos";
                            // Paso los datos a una tabla
                            DataTable myTabla2 = clsDataBD.GetSql(myCadenaSQL);

                            // recorro la tabla y asigno el Id del insumo a una variable
                            foreach (DataRow row2 in myTabla2.Rows)
                            {
                                // Asigno a la variable el nuevo costo del insumo
                                IdProdOrigen = Convert.ToInt32(row2["IdProdOrigen"]);

                                //Vaciar
                                cPrecio = 0;

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

        #region Método que actualiza la composición de los productos

        private void ActualizarComposicion(int IdArt, string sTabla)
        {
            // Recorro la tabla Insumos para encontrar el IdInsumo por el IdArticulo
            string myCadenaSQL = "select * from Insumos where IdArticulo = " + IdArt;
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

        #region Método que vacía los vectores globales para nuevo uso

        private void VaciarVectoresGlobales()
        {
            // Vacío de datos el vector de los Insumos
            clsGlobales.InsumosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.InsumosSeleccionados, new int[] { 0, 2 });
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

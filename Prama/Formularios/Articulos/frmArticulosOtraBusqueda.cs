using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prama.Formularios.Articulos
{
    public partial class frmArticulosOtraBusqueda : Form
    {
        // Declaro las variables que me permiten filtrar por el tipo de proveedor
        bool bInsumosChk;
        bool bProductosChk;
        string sArt = "";
        string sTabla = "";
        bool bHayData = false;
        bool EsPresupuesto = false;

        public frmArticulosOtraBusqueda(bool ins, bool Prod, string Tabla="", bool pHayData = false)
        {
            InitializeComponent();

            // Asigno los parámetros a las variables
            bInsumosChk = ins;
            bProductosChk = Prod;
            sTabla = Tabla;
            bHayData = pHayData;
            if (sTabla == "Temporal_CargaDetPresu")
            {
                EsPresupuesto = true;
            }
            else
            {
                EsPresupuesto = false;
            }
        }

        #region Método para cargar la grilla

        private void CargarArticulos(string Buscar, string Campo, bool bElegidos = false)
        {
            // Variable para la cadena SQL
            string myCadena = "";
            double[,] ProductosBusqueda = new double[0,2] {};
            int item = 0;

            //Hay datos ya cargados? cargar al vector
            //Traer la vista con los datos ya cargados
            //myCadena = "select * from Vista_" + sTabla + " Where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario + " Order by Orden ASC";
                if (!(EsPresupuesto))
                {
                    myCadena = "select * from Vista_" + sTabla + " Where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario + " Order by Orden ASC";
                }
                else
                {

                    myCadena = "select * from Vista_" + sTabla + " Order by Orden ASC";
                }

                DataTable mData = new DataTable();
                mData = clsDataBD.GetSql(myCadena);

                //Recorrer y subir al vector
                foreach (DataRow myRow in mData.Rows)
                {
                    // Redimensiono el tamaño de la matriz
                        ProductosBusqueda = (double[,])clsValida.ResizeMatriz(ProductosBusqueda, new int[] { item + 1, 2 });
                    // A la posición creada le asigno el Id seleccionado
                        ProductosBusqueda[item, 0] = Convert.ToDouble(myRow["IdArticulo"].ToString());
                        ProductosBusqueda[item, 1] = Convert.ToInt32(myRow["Cantidad"].ToString());
                    //Proximo
                    item++;
                }

             //sArt
                this.sArt = "";

                int LargoProductos = ProductosBusqueda.GetLength(0);

                //RECORRER EL VECTOR PARA CARGAR LOS ID'S.
                for (int i = 0; i < LargoProductos; i++)
                {
                    // Si no es el último
                    if (!(i == LargoProductos - 1))
                    {
                        // Paso a string el Id del proveedor y lo ingreso a la cadena
                        sArt = sArt + ProductosBusqueda[i, 0].ToString() + ",";
                    }
                    // Si es el último
                    else
                    {
                        // Paso a string el Id del proveedor y lo ingreso a la cadena
                        sArt = sArt + ProductosBusqueda[i, 0].ToString();
                    }

                }

                //A False...
                bHayData = false;


                //QUE PARAMETRO VIENE? BUSCAR SIN LOS ELEGIDOS...
                if (Buscar == "" && bElegidos == false)
                {
                    // Cadena SQL 
                    myCadena = "select * from Articulos_Productos where Tabla = 'PRODUCTOS'";
                }
                else if (Buscar == "" && bElegidos == true) //BUSCAR SOLO ELEGIDOS!
                {
                    myCadena = "select * from Articulos_Productos WHERE Tabla = 'PRODUCTOS' AND IdArticulo in (" + sArt + ")";
                }
                else //BUSCAR POR CAMPO ESPECIFICO
                {
                    // Cadena SQL 
                    myCadena = "select * from Articulos_Productos where Tabla = 'PRODUCTOS' and " + Campo + " like '" + Buscar + "%' order by " + Campo;
                }

                // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
                DataTable mDtTable = new DataTable();
                mDtTable = clsDataBD.GetSql(myCadena);
                // Evito que el dgv genere columnas automáticas
                dgvArticulos.AutoGenerateColumns = false;
                // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
                dgvArticulos.DataSource = mDtTable;

                // Cuento la cantidad de filas de la grilla
                int Filas = dgvArticulos.Rows.Count;

                // Posiciono la grilla en la última fila
                int filasMatrizProductosSeleccionados = ProductosBusqueda.GetLength(0);

                //// Recorro la grilla para marcar los articulos ya seleccionados
                foreach (DataGridViewRow row in dgvArticulos.Rows)
                {
                    // Verifico si el id del producto ya está en el vector
                    for (int i = 0; i < filasMatrizProductosSeleccionados; i++)
                    {
                        if (Convert.ToDouble(row.Cells["IdArticulo"].Value) == ProductosBusqueda[i, 0])
                        {
                            //ARMAR SQL DE LA VISTA
                            if (!(EsPresupuesto))
                            {
                                myCadena = "select * from Vista_" + sTabla + " WHERE IdArticulo = " + ProductosBusqueda[i, 0] + " AND IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario;
                            }
                            else
                            {
                                myCadena = "select * from Vista_" + sTabla + " WHERE IdArticulo = " + ProductosBusqueda[i, 0];
                            }

                            //CARGAR DATABLE A LA GRILLA
                            DataTable myDataRenewCant = clsDataBD.GetSql(myCadena);

                            if (myDataRenewCant.Rows.Count > 0)
                            {
                                foreach (DataRow myRow in myDataRenewCant.Rows)
                                {
                                    //Cargar Cantidad
                                    row.Cells["Cantidad"].Value = myRow["Cantidad"].ToString();
                                }
                            }
                            else
                            {
                                //Cargar Cantidad
                                row.Cells["Cantidad"].Value = ProductosBusqueda[i, 1];
                            }

                            //Check
                            row.Cells["chkElegido"].Value = true;
                        }
                    }
                }
        }

        #endregion

        #region Metodo: YaCargado()

        private bool YaCargado(int p_IdArt)
        {
               //Si ya existe no se modifica    
               bool bExiste = false;

               //ARMAR SQL DE LA VISTA
               string myCadena = "";
               if (!(EsPresupuesto))
               {
                   myCadena = "select * from Vista_" + sTabla + " WHERE IdArticulo = " + p_IdArt + " and IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario;
               }
               else
               {
                   myCadena = "select * from Vista_" + sTabla + " WHERE IdArticulo = " + p_IdArt;
                   
               }
                   //CARGAR DATABLE A LA GRILLA
               DataTable myDataRenewCant = clsDataBD.GetSql(myCadena);
               if (myDataRenewCant.Rows.Count > 0)
               {
                   bExiste = true;
               }

               return bExiste;
        }

        #endregion

        #region CargarToolTips

        private void CargarToolTips()
        {
            toolTip3.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip4.SetToolTip(this.btnSalir, "Salir");

        }

        #endregion

        #region Eventos Enter de los TextBox del formulario

        private void txtCodigo_Enter(object sender, EventArgs e)
        {
            // Limpio los demás textbox
            txtArticulo.Text = "";
        }

        private void txtArticulo_Enter(object sender, EventArgs e)
        {
            // Limpio los demás textbox
            txtCodigo.Text = "";
        }

        #endregion

        #region Eventos TextChanged de los TextBox del formulario

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            //ELEGIDOS A .F.
            chkElegidos.Checked = false;

            // Cargo los Proveedores filtradas por la búsqueda
            CargarArticulos(txtCodigo.Text, "CodigoArticulo");
        }

        private void txtArticulo_TextChanged(object sender, EventArgs e)
        {
            //ELEGIDOS A .F.
            chkElegidos.Checked = false;

            // Cargo los Proveedores filtradas por la búsqueda
            CargarArticulos(txtArticulo.Text, "Articulo");
        }

        #endregion

        #region Eventos Botones

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //No hay datos, vaciar.
            if (clsGlobales.ProductosSeleccionados.GetLength(0) == 0)
            {
                clsGlobales.ProductosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.ProductosSeleccionados, new int[] {0, 2 });
            }

            // Cierro el formulario
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //No hay datos, vaciar.
            if (clsGlobales.ProductosSeleccionados.GetLength(0) == 0)
            {
                clsGlobales.ProductosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.ProductosSeleccionados, new int[] { 0, 2 });
            }

            // Cierro el formulario con los datos de los proveedores en el vector
            this.Close();
        }

        #endregion

        #region Evento Load

        private void frmArticulosOtraBusqueda_Load(object sender, EventArgs e)
        {

			//icon
            clsFormato.SetIconForm(this); 
			
            //Hay datos?
            if (bHayData)
            {
                // Llamo al método cargar Proveedores para rellenar la grilla. G.
                CargarArticulos("", "", true);

                //.T.
                chkElegidos.Checked = true;
            }
            else
            {
                // Llamo al método cargar Proveedores para rellenar la grilla. G.
                CargarArticulos("", "");
            }

            // Cargo los ToolTips
            CargarToolTips();

            // Deshabilito la reordenación de las columnas de la grilla
            foreach (DataGridViewColumn dgvCol in dgvArticulos.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //Titulo Ventana
            this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;
        }

        #endregion

        #region Metodo AgregarItem

        private void AgregarItem()
        {
            // Creo una bandera para detectar si el artículo ya se cargó
            bool myBandera = false;
            // Variables que guardan los largos de los vectores
            int filasMatrizProductosSeleccionados = clsGlobales.ProductosSeleccionados.GetLength(0);
            // Verifico si es producto
          //  if (dgvArticulos.CurrentRow.Cells["Tabla"].Value.ToString() == "PRODUCTOS")
          //  {
                // Verifico si el vector ya tiene datos cargados
                if (filasMatrizProductosSeleccionados > 0)
                {
                    // Recorro el vector para verificar que el datos no se duplique
                    for (int i = 0; i < filasMatrizProductosSeleccionados; i++)
                    {
                        // SI el proveedor ya fue seleccionado
                        if (clsGlobales.ProductosSeleccionados[i, 0] == Convert.ToDouble(dgvArticulos.CurrentRow.Cells["IdArticulo"].Value))
                        {
                            // Pongo su valor a 0 para que al pasar los datos al otro formulario no lo cargue a su grilla de proveedores
                            clsGlobales.ProductosSeleccionados[i, 1] = Convert.ToDouble(dgvArticulos.CurrentRow.Cells["Cantidad"].Value);
                            dgvArticulos.CurrentRow.Cells["Cantidad"].Value = Convert.ToDouble(dgvArticulos.CurrentRow.Cells["Cantidad"].Value);

                            // Cambio el estado de la bandera
                            myBandera = true;
                        }
                    }

                    // Sino esta cargado...
                    if (myBandera == false)
                    {
                        // Redimensiono el tamaño de la matriz
                        clsGlobales.ProductosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.ProductosSeleccionados, new int[] { filasMatrizProductosSeleccionados + 1, 2 });
                        // A la posición creada le asigno el Id seleccionado
                        clsGlobales.ProductosSeleccionados[filasMatrizProductosSeleccionados, 0] = Convert.ToDouble(dgvArticulos.CurrentRow.Cells["IdArticulo"].Value);
                        clsGlobales.ProductosSeleccionados[filasMatrizProductosSeleccionados, 1] = 1;
                        //Cargo cantidad base
                        dgvArticulos.CurrentRow.Cells["Cantidad"].Value = Convert.ToDouble(clsGlobales.ProductosSeleccionados[filasMatrizProductosSeleccionados, 1]);
                    }

                }
                // Si no tiene todavía ningún ID
                else
                {
                    // Redimensiono el tamaño de la matriz
                    clsGlobales.ProductosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.ProductosSeleccionados, new int[] { 1, 2 });
                    // A la posición creada le asigno el Id seleccionado
                    clsGlobales.ProductosSeleccionados[filasMatrizProductosSeleccionados, 0] = Convert.ToDouble(dgvArticulos.CurrentRow.Cells["IdArticulo"].Value);
                    clsGlobales.ProductosSeleccionados[filasMatrizProductosSeleccionados, 1] = 1;

                    //Cargo cantidad base
                    dgvArticulos.CurrentRow.Cells["Cantidad"].Value = Convert.ToDouble(clsGlobales.ProductosSeleccionados[filasMatrizProductosSeleccionados, 1]);

                }
            //}

            // Marco el check
            dgvArticulos.CurrentRow.Cells["chkElegido"].Value = true;


        }

        #endregion

        #region Metodo QuitarItem

        private void QuitarItem()
        {
            // Variables que guardan los largos de los vectores
            int filasMatrizProductosSeleccionados = clsGlobales.ProductosSeleccionados.Length / 2;

            // Desmarco el check
            dgvArticulos.CurrentRow.Cells["chkElegido"].Value = false;
            // Verifico si es Producto
           // if (dgvArticulos.CurrentRow.Cells["Tabla"].Value.ToString() == "PRODUCTOS")
           // {
                // Elimino el del vector
                for (int i = 0; i < filasMatrizProductosSeleccionados; i++)
                {
                    // Si el proveedor quitado es el del vector
                    if (clsGlobales.ProductosSeleccionados[i, 0] == Convert.ToDouble(dgvArticulos.CurrentRow.Cells["IdArticulo"].Value))
                    {
                        // Pongo su valor a 0 para que al pasar los datos al otro formulario no lo cargue a su grilla de proveedores
                        clsGlobales.ProductosSeleccionados[i, 0] = 0;
                        clsGlobales.ProductosSeleccionados[i, 1] = 0;
                    }
                }
           // }

            //Vaciar Cantidad
            dgvArticulos.CurrentRow.Cells["Cantidad"].Value = null;


        }

        #endregion

        #region Otros Eventos ( Grilla, etc )

        private void dgvArticulos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Si ya esta previamente cargado, salir.
            bool bRetorno = YaCargado(Convert.ToInt32(dgvArticulos.CurrentRow.Cells["IdArticulo"].Value.ToString()));
            if (bRetorno)
            {
                return;
            }

            //Chk
            if (e.ColumnIndex == 8)
            {
                if (dgvArticulos.CurrentRow.Cells["chkElegido"].Value == null || Convert.ToBoolean(dgvArticulos.CurrentRow.Cells["chkElegido"].Value) == false)
                {
                    this.AgregarItem();

                    // Pongo el foco de la fila en la columna cantidad
                    dgvArticulos.CurrentCell = dgvArticulos["Cantidad", e.RowIndex];
                }
                else
                {
                    this.QuitarItem();
                }
            }
        }

        private void dgvArticulos_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Si ya esta previamente cargado, salir.
            bool bRetorno = YaCargado(Convert.ToInt32(dgvArticulos.CurrentRow.Cells["IdArticulo"].Value.ToString()));
            if (bRetorno)
            {
                return;
            }

            if (e.KeyChar == 32)
            {
                int fila = dgvArticulos.CurrentRow.Index;

                //Chk
                if (this.dgvArticulos.CurrentCell.ColumnIndex == 8)
                {
                    if (dgvArticulos.CurrentRow.Cells["chkElegido"].Value == null || Convert.ToBoolean(dgvArticulos.CurrentRow.Cells["chkElegido"].Value) == false)
                    {
                        this.AgregarItem();

                        // Pongo el foco de la fila en la columna cantidad
                        dgvArticulos.CurrentCell = dgvArticulos["Cantidad", fila];

                    }
                    else
                    {
                        this.QuitarItem();

                        // Pongo el foco de la fila en la columna cantidad
                        dgvArticulos.CurrentCell = dgvArticulos["chkElegido", fila];
                    }
                }
            }
        }

        private void dgvArticulos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //Declaracion de Variables (Costo Unitario y Cantidad)
            DataGridViewRow row = dgvArticulos.CurrentRow;

            //Null? Salir
            if (row.Cells["chkElegido"].Value == null || Convert.ToBoolean(row.Cells["chkElegido"].Value) == false)
            {

                //Null al costo final
                dgvArticulos.CurrentRow.Cells["Cantidad"].Value = null;
                //Retornar  
                return;
            }

            //No esta elegida? salir...
            if (!(Convert.ToBoolean(row.Cells["chkElegido"].Value)) == true)
            {
                //Null al costo final
                dgvArticulos.CurrentRow.Cells["Cantidad"].Value = null;
                //Retornar
                return;
            }
            else
            {
                if (!(row.Cells["Cantidad"].Value == null))
                {
                    if (Convert.ToDouble(row.Cells["Cantidad"].Value) == 0)
                    {
                        //Null al costo final
                        dgvArticulos.CurrentRow.Cells["Cantidad"].Value = 1;
                        //Agrego Item
                        this.AgregarItem();
                    }
                    else
                    {
                        //Agrego Item
                        this.AgregarItem();

                    }
                }
                else
                {
                    //Null al costo final
                    dgvArticulos.CurrentRow.Cells["Cantidad"].Value = 1;
                    //Agrego Item
                    this.AgregarItem();
                }
            }
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            // Si la grilla tiene artículos
            if (dgvArticulos.RowCount > 0)
            {
                // Almaceno én una variable la posición de fila en la que me encuentro
                int fila = dgvArticulos.CurrentRow.Index;
            }
        }

        private void chkElegidos_CheckedChanged(object sender, EventArgs e)
        {
            //Clean
            sArt = "";
            //Check
            if (chkElegidos.Checked)
            {
                int LargoProductos = clsGlobales.ProductosSeleccionados.GetLength(0);

                //VARIABLE QUE CONTIENE VECTOR DE PRODUCTOS
                LargoProductos = clsGlobales.ProductosSeleccionados.GetLength(0);

                //SI EL HAY DATOS
                if (!(LargoProductos == 0))
                {
                    //RECORRER EL VECTOR PARA CARGAR LOS ID'S.
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
                else
                {
                    //SALIR
                    return;
                }

                //Cargar la Grilla
                this.CargarArticulos("", "", true);
            }
            else
            {
                //Cargar la Grilla
                this.CargarArticulos("", "", false);
            }
        }

        #endregion
    }
}
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
    public partial class frmProductosCompuestos : Form
    {

        //VARIABLES QUE VA A ALMACENAR LOS ID'S DE PRODUCTOS
        #region VARIABLES

        string sArt = "";
        int IdArt = 0;
        string Estado = "";
        TextBox myTextBox;
        int Coef = 0;
        double cIva = 0;
        double pub = 0;
        double dist = 0;
        double rev = 0;

        #endregion

        #region CONSTRUCTOR

        public frmProductosCompuestos(int p_IdArt = 0, string p_Estado = "", TextBox p_myTextBox = null, int p_Coef=0, double p_Iva=0)
        {
            InitializeComponent();

            IdArt = p_IdArt;
            Estado = p_Estado;
            myTextBox = p_myTextBox;
            Coef = p_Coef;
            cIva = p_Iva;
        }

        #endregion

        #region Metodo ObtenerCoeficientes

        private void ObtenerCoeficientes(int p_IdCoef = 0)
        {
            string sCadSQL = "Select * from CoeficientesArticulos WHERE IdCoeficienteArticulo = " + p_IdCoef;

            DataTable myData = clsDataBD.GetSql(sCadSQL);

            //Guardar Coeficientes en variables
            foreach (DataRow row in myData.Rows)
            {
                //Coeficiente publico y revendedor
                pub = Convert.ToDouble(row["CoeficientePublico"]);
                dist = Convert.ToDouble(row["CoeficienteDistribuidor"]);
                rev = Convert.ToDouble(row["CoeficienteRevendedor"]);
            }
        }

        #endregion

        #region Metodo AgregarItem

        private void AgregarItem()
        {
            // Creo una bandera para detectar si el artículo ya se cargó
            bool myBandera = false;
            // Variables que guardan los largos de los vectores
            int filasMatrizProductosSeleccionados = clsGlobales.ProdSelCompuesto.Length / 3;
            // Verifico si es producto
            if (dgvArticulos.CurrentRow.Cells["Tabla"].Value.ToString() == "PRODUCTOS")
            {
                // Verifico si el vector ya tiene datos cargados
                if (filasMatrizProductosSeleccionados > 0)
                {
                    // Recorro el vector para verificar que el datos no se duplique
                    for (int i = 0; i < filasMatrizProductosSeleccionados; i++)
                    {
                        // SI el proveedor ya fue seleccionado
                        if (clsGlobales.ProdSelCompuesto[i, 0] == Convert.ToDouble(dgvArticulos.CurrentRow.Cells["IdArticulo"].Value))
                        {
                            // Pongo su valor a 0 para que al pasar los datos al otro formulario no lo cargue a su grilla de proveedores
                               clsGlobales.ProdSelCompuesto[i, 1] = Convert.ToDouble(dgvArticulos.CurrentRow.Cells["Cantidad"].Value);
                               clsGlobales.ProdSelCompuesto[i, 2] = Convert.ToDouble(dgvArticulos.CurrentRow.Cells["Precio"].Value);
                               dgvArticulos.CurrentRow.Cells["Cantidad"].Value = Convert.ToDouble(dgvArticulos.CurrentRow.Cells["Cantidad"].Value);
                               //Multiplico Precio por Cantidad
                               dgvArticulos.CurrentRow.Cells["Total"].Value = (Convert.ToDouble(dgvArticulos.CurrentRow.Cells["Cantidad"].Value) * Convert.ToDouble(dgvArticulos.CurrentRow.Cells["Precio"].Value)).ToString("#0.00");

                            // Cambio el estado de la bandera
                               myBandera = true;
                        }
                    }

                    // Sino esta cargado...
                    if (myBandera == false)
                    {
                        // Redimensiono el tamaño de la matriz
                        clsGlobales.ProdSelCompuesto = (double[,])clsValida.ResizeMatriz(clsGlobales.ProdSelCompuesto, new int[] { filasMatrizProductosSeleccionados + 1, 3 });
                        // A la posición creada le asigno el Id seleccionado
                        clsGlobales.ProdSelCompuesto[filasMatrizProductosSeleccionados, 0] = Convert.ToDouble(dgvArticulos.CurrentRow.Cells["IdArticulo"].Value);
                        clsGlobales.ProdSelCompuesto[filasMatrizProductosSeleccionados, 1] = 1;
                        clsGlobales.ProdSelCompuesto[filasMatrizProductosSeleccionados, 2] = Convert.ToDouble(dgvArticulos.CurrentRow.Cells["Precio"].Value);
                        //Cargo cantidad base
                        dgvArticulos.CurrentRow.Cells["Cantidad"].Value = Convert.ToDouble(clsGlobales.ProdSelCompuesto[filasMatrizProductosSeleccionados, 1]);
                        //Multiplico Precio por Cantidad
                        dgvArticulos.CurrentRow.Cells["Total"].Value = (Convert.ToDouble(dgvArticulos.CurrentRow.Cells["Cantidad"].Value) * Convert.ToDouble(dgvArticulos.CurrentRow.Cells["Precio"].Value)).ToString("#0.00");
                    }

                }
                // Si no tiene todavía ningún ID
                else
                {
                    // Redimensiono el tamaño de la matriz
                    clsGlobales.ProdSelCompuesto = (double[,])clsValida.ResizeMatriz(clsGlobales.ProdSelCompuesto, new int[] { 1, 3 });
                    // A la posición creada le asigno el Id seleccionado
                    clsGlobales.ProdSelCompuesto[filasMatrizProductosSeleccionados, 0] = Convert.ToDouble(dgvArticulos.CurrentRow.Cells["IdArticulo"].Value);
                    clsGlobales.ProdSelCompuesto[filasMatrizProductosSeleccionados, 1] = 1;
                    clsGlobales.ProdSelCompuesto[filasMatrizProductosSeleccionados, 2] = Convert.ToDouble(dgvArticulos.CurrentRow.Cells["Precio"].Value);
                    
                    //Cargo cantidad base
                    dgvArticulos.CurrentRow.Cells["Cantidad"].Value = Convert.ToDouble(clsGlobales.ProdSelCompuesto[filasMatrizProductosSeleccionados, 1]);
                    //Multiplico Precio por Cantidad
                    dgvArticulos.CurrentRow.Cells["Total"].Value = (Convert.ToDouble(dgvArticulos.CurrentRow.Cells["Cantidad"].Value) * Convert.ToDouble(dgvArticulos.CurrentRow.Cells["Precio"].Value)).ToString("#0.00");                    

                }
            }

            // Marco el check
            dgvArticulos.CurrentRow.Cells["chkElegido"].Value = true;

            //Costo Total
            this.CalcularCostoTotal();

        }

        #endregion

        #region Metodo QuitarItem

        private void QuitarItem()
        {
            // Variables que guardan los largos de los vectores
            int filasMatrizProductosSeleccionados = clsGlobales.ProdSelCompuesto.Length / 3;

            // Desmarco el check
            dgvArticulos.CurrentRow.Cells["chkElegido"].Value = false;
            // Verifico si es Producto
            if (dgvArticulos.CurrentRow.Cells["Tabla"].Value.ToString() == "PRODUCTOS")
            {
                // Elimino el del vector
                for (int i = 0; i < filasMatrizProductosSeleccionados; i++)
                {
                    // Si el proveedor quitado es el del vector
                    if (clsGlobales.ProdSelCompuesto[i, 0] == Convert.ToDouble(dgvArticulos.CurrentRow.Cells["IdArticulo"].Value))
                    {
                        // Pongo su valor a 0 para que al pasar los datos al otro formulario no lo cargue a su grilla de proveedores
                        clsGlobales.ProdSelCompuesto[i, 0] = 0;
                        clsGlobales.ProdSelCompuesto[i, 1] = 0;
                        clsGlobales.ProdSelCompuesto[i, 2] = 0;
                    }
                }
            }

            //Vaciar Cantidad
            dgvArticulos.CurrentRow.Cells["Cantidad"].Value = null;
            //Multiplico Precio por Cantidad
            dgvArticulos.CurrentRow.Cells["Total"].Value = null;

            //Costo Total
            this.CalcularCostoTotal();

        }

        #endregion

        #region CargarToolTips

        private void CargarToolTips()
        {
            toolTip3.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip4.SetToolTip(this.btnSalir, "Salir");

        }

        #endregion

        #region Evento Load

        private void frmProductosCompuestos_Load(object sender, EventArgs e)
        {
            //icon
            clsFormato.SetIconForm(this);
            //Cargar Combo
            // Cargo el combo de Coeficiente
            clsDataBD.CargarCombo(cboCoeficiente, "CoeficientesArticulos", "CoeficienteArticulo", "IdCoeficienteArticulo");
            // Dejo vacío el combo
            cboCoeficiente.SelectedValue = Coef;
            //Coeficientes
            ObtenerCoeficientes(Coef);
            //Cargar Vector Con Seleccionados
            CargarArticulosAVector(); 
            // Llamo al método cargar Proveedores para rellenar la grilla. G.
            CargarArticulos("", "");            
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

        #region Metodo: CalcularCostoTotal

        private void CalcularCostoTotal()
        {
            double cTotal = 0;
            double cIva = 0;

          //Recorro la grilla para marcar los articulos ya seleccionados
            foreach (DataGridViewRow row in dgvArticulos.Rows)
            {
                if (!(row.Cells["Total"].Value==null || string.IsNullOrEmpty(row.Cells["Total"].Value.ToString())))
                {
                    if (Convert.ToDouble(row.Cells["Total"].Value) != 0)
                    {
                        cTotal += Convert.ToDouble(row.Cells["Total"].Value);
                    }
                }
             }

             //Cantidad Seleccionados
             this.txtTotal.Text = cTotal.ToString("#0.00");      
      
            //Mostrar Coeficiente
             this.txtPub.Text = this.pub.ToString("#0.00");
             this.txtDist.Text = this.dist.ToString("#0.00");
             this.txtRev.Text = this.rev.ToString("#0.00");

            //Precio con Coeficiente
             this.txtPcioPub.Text = (cTotal * this.pub).ToString("#0.00");
             this.txtPcioDist.Text = (cTotal * this.dist).ToString("#0.00");
             this.txtPcioRev.Text = (cTotal * this.rev).ToString("#0.00");

            //IVA
             cIva = 1 + (this.cIva / 100);
             this.txtPubIva.Text = (Convert.ToDouble(txtPcioPub.Text) * cIva).ToString("#0.00");
             this.txtDistIva.Text = (Convert.ToDouble(txtPcioDist.Text) * cIva).ToString("#0.00");
             this.txtRevIva.Text = (Convert.ToDouble(txtPcioRev.Text) * cIva).ToString("#0.00");

        }

        #endregion

        #region Metodo: CargarArticulosAVector()

        private void CargarArticulosAVector()
        {
            string myCad = "";

            //Si hay datos pre-cargados porque estoy modificando
            if (IdArt != 0 && Estado != "A" && clsGlobales.bCargoProdCompto == false)
            {
                //Traer la composicion y subirla al vector
                myCad = "Select * from ProductosCompuestos WHERE IdProdOrigen = " + IdArt;
                DataTable myData = new DataTable();
                myData = clsDataBD.GetSql(myCad);

                int Item = 0;

                // Recorro la grilla para marcar los articulos ya seleccionados
                foreach (DataRow row in myData.Rows)
                {
                    clsGlobales.ProdSelCompuesto = (double[,])clsValida.ResizeMatriz(clsGlobales.ProdSelCompuesto, new int[] {Item + 1, 3 });
                    //A la posición creada le asigno el Id seleccionado
                    clsGlobales.ProdSelCompuesto[Item, 0] = Convert.ToDouble(row["IdProdCompone"].ToString());
                    clsGlobales.ProdSelCompuesto[Item, 1] = Convert.ToDouble(row["Cantidad"].ToString());
                    clsGlobales.ProdSelCompuesto[Item, 2] = 0;

                    //Cambiar Item
                    Item++;
                }

                //Boolean
                clsGlobales.bCargoProdCompto = true;
            }
        }

        #endregion

        #region Método para cargar la grilla

        private void CargarArticulos(string Buscar, string Campo, bool bElegidos = false)
        {
            // Variable para la cadena SQL
            string myCadena = "";
            if (Buscar == "" && bElegidos == false)
            {
              // Cadena SQL 
                 myCadena = "select * from Articulos_Productos where Tabla = 'PRODUCTOS' AND Not IdArticulo in " + 
                            "(select distinct IdProdOrigen from ProductosCompuestos)";
            }
            else if (Buscar == "" &&  bElegidos == true)
            {
                 myCadena = "select * from Articulos_Productos where IdArticulo in (" + sArt + ")";
            }
            else
            {
               // Cadena SQL 
                myCadena = "select * from Articulos_Productos where Tabla = 'PRODUCTOS'  AND Not IdArticulo in " + 
                            "(select distinct IdProdOrigen from ProductosCompuestos) and " + Campo + " like '" + Buscar + "%' order by " + Campo;
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
            int filasMatrizProductosSeleccionados = clsGlobales.ProdSelCompuesto.Length / 3;
            // Recorro la grilla para marcar los articulos ya seleccionados
            foreach (DataGridViewRow row in dgvArticulos.Rows)
            {
                if (dgvArticulos.CurrentRow.Cells["Tabla"].Value.ToString() == "PRODUCTOS")
                {
                    // Verifico si el id del producto ya está en el vector
                    for (int i = 0; i < filasMatrizProductosSeleccionados; i++)
                    {
                        if (Convert.ToDouble(row.Cells["IdArticulo"].Value) == clsGlobales.ProdSelCompuesto[i, 0])
                        {
                            //Check
                            row.Cells["chkElegido"].Value = true;
                            //Cargar Cantidad
                            row.Cells["Cantidad"].Value = clsGlobales.ProdSelCompuesto[i, 1];
                            //Cargar Cantidad
                            row.Cells["Total"].Value = (Convert.ToDouble(row.Cells["Precio"].Value.ToString()) * clsGlobales.ProdSelCompuesto[i, 1]).ToString("#0.00");
                            //Fin
                            break;
                        }
                    }
                }

                //Costo Total
                this.CalcularCostoTotal();
            }
        }

        #endregion

        #region Eventos CellDoubleClick, SelectedChanged, TextChanged, KeyPress, CheckedChanged

        private void dgvArticulos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Chk
            if (e.ColumnIndex == 8)
            {
                if (dgvArticulos.CurrentRow.Cells["chkElegido"].Value == null || Convert.ToBoolean(dgvArticulos.CurrentRow.Cells["chkElegido"].Value) == false)
                {
                    this.AgregarItem();
                }
                else
                {
                    this.QuitarItem();
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
                // Pongo el foco de la fila en la columna cantidad
               // dgvArticulos.CurrentCell = dgvArticulos["Cantidad", fila];

            }
        }

        private void txtArticulo_TextChanged(object sender, EventArgs e)
        {
            // Cargo los Proveedores filtradas por la búsqueda
            CargarArticulos(txtArticulo.Text, "Articulo");
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            // Cargo los Proveedores filtradas por la búsqueda
            CargarArticulos(txtCodigo.Text, "CodigoArticulo");
        }

        private void chkElegidos_CheckedChanged(object sender, EventArgs e)
        {
            //Clean
            sArt = "";
            //Check
            if (chkElegidos.Checked)
            {
                int LargoProductos = clsGlobales.ProdSelCompuesto.GetLength(0);

                //VARIABLE QUE CONTIENE VECTOR DE PRODUCTOS
                LargoProductos = clsGlobales.ProdSelCompuesto.GetLength(0);

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
                            sArt = sArt + clsGlobales.ProdSelCompuesto[i, 0].ToString() + ",";
                        }
                        // Si es el último
                        else
                        {
                            // Paso a string el Id del proveedor y lo ingreso a la cadena
                            sArt = sArt + clsGlobales.ProdSelCompuesto[i, 0].ToString();
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

        private void dgvArticulos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 32)
            {
                int fila = dgvArticulos.CurrentRow.Index;

                //Chk
                if (this.dgvArticulos.CurrentCell.ColumnIndex == 8)
                {
                    if (dgvArticulos.CurrentRow.Cells["chkElegido"].Value == null || Convert.ToBoolean(dgvArticulos.CurrentRow.Cells["chkElegido"].Value)==false)
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
            if (row.Cells["chkElegido"].Value == null || Convert.ToBoolean(row.Cells["chkElegido"].Value)==false) 
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

        #endregion

        #region Botones Click

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Validar las cantidades
            bool bRetorno = bValidaCantidades();

            //Verificar Cantidades
            if (!(bRetorno))
            { return; }

            //Verificar Total
            if (Convert.ToDouble(txtTotal.Text) == 0)
            {
               // Redimensiono el tamaño de la matriz
               clsGlobales.ProdSelCompuesto = (double[,])clsValida.ResizeMatriz(clsGlobales.ProdSelCompuesto, new int[] { 0, 3 });
            }
            
            //Precio
            this.myTextBox.Text = Convert.ToDouble(txtTotal.Text).ToString("#0.00");

            //Salir
            this.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //Validar las cantidades
            bool bRetorno = bValidaCantidades();

            //Verificar Cantidades
            if (!(bRetorno))
            { return; }

            //Verificar Total
            if (Convert.ToDouble(txtTotal.Text) == 0)
            {
                // Redimensiono el tamaño de la matriz
                clsGlobales.ProdSelCompuesto = (double[,])clsValida.ResizeMatriz(clsGlobales.ProdSelCompuesto, new int[] { 0, 3 });
            }

            //Precio
            this.myTextBox.Text = Convert.ToDouble(txtTotal.Text).ToString("#0.00");

            //Salir
            this.Close();
        }

        #endregion

        #region Metodo bValidaCantidades

        private bool bValidaCantidades()        
        {
            bool bRetorno = true;

            //Recorrer la Matriz
            for (int iterador = 0; iterador < clsGlobales.ProdSelCompuesto.GetLength(0); iterador++)
            {

                //Si esta seleccionado y no tiene cantidad....
                if (clsGlobales.ProdSelCompuesto[iterador, 0] == 1 && clsGlobales.ProdSelCompuesto[iterador, 1]==0)
                {
                    MessageBox.Show("Verifique haber completado la 'Cantidad' para todos los Productos elegidos!", "Advertencia!",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    bRetorno = false;
                    return bRetorno;
                }

            }

            //Retornar
            return bRetorno;
        }

        #endregion

        private void cboCoeficiente_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(cboCoeficiente.SelectedIndex==-1))
            {
                //Coeficientes
                ObtenerCoeficientes(Convert.ToInt32(cboCoeficiente.SelectedValue));

                //Calcular
                this.CalcularCostoTotal();
            }
        }

    }
}

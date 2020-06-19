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
    public partial class frmInsumosComposicion : Form
    {

        public string sEstado = ""; //A o M (estado)
        public int nIdArt = 0;      //IdArticulo
        TextBox txtcArt;

        public frmInsumosComposicion(string p_Estado, int p_IdArticulo, TextBox p_txtCantArt=null)
        {
            InitializeComponent();
            this.sEstado = p_Estado;
            this.nIdArt = p_IdArticulo;
            txtcArt = p_txtCantArt;
        }

        private void frmInsumosComposicion_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 
            //Cargar Grilla Insumos
            CargarGrillaInsumos();

            //Habilitar boton agregar
            this.btnAgregar.TabStop = true;
            this.btnAgregar.Enabled = true;

            //Mostrar Datos en Matriz y Vector 
            if (this.sEstado == "A")
            {

                //Por si esta yendo y viniendo entre forms
                //con datos ya cargados (utiliza los vectores)
                //en el ALTA.
                MostrarDatosPreCargados();

                //Boton Aceptar
                if (dgvDatos.Rows.Count > 0)
                {
                    this.btnAceptar.TabStop = true;
                    this.btnAceptar.Enabled = true;
                    this.btnBorrar.TabStop = true;
                    this.btnBorrar.Enabled = true;
                }
                else
                {
                    this.btnAceptar.TabStop = false;
                    this.btnAceptar.Enabled = false;
                    this.btnBorrar.TabStop = false;
                    this.btnBorrar.Enabled = false;
                }

            }
            else if (this.sEstado == "M")
            {
                int longitud = clsGlobales.ProductoComposicion.GetLength(0);

                //Cambio algo? verificamos si al modificar se cambio algo o no en la composicion.
                if (longitud >0)
                {
                   //Muestra los datos que estan en los vectores en memoria
                     MostrarDatosPreCargados();
                }
                else
                {
                   //Esta modificando, los datos vienen de la base
                     CargarDetalleComposicion();
                }

                //Boton Aceptar
                if (dgvDatos.Rows.Count > 0)
                {
                    this.btnAceptar.TabStop = true;
                    this.btnAceptar.Enabled = true;
                    this.btnBorrar.TabStop = true;
                    this.btnBorrar.Enabled = true;
                   
                }
                else
                {
                    this.btnBorrar.TabStop = false;
                    this.btnBorrar.Enabled = false;
                    this.btnAceptar.TabStop = false;
                    this.btnAceptar.Enabled = false;
                }
            }
            
            //Costo Final
            SetCostoFinal();

            // Cargo los toolsTip
            CargarToolTips();

            //Titulo Ventana
            this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;
        }

        #region SetCostoFinal

        private void SetCostoFinal()
        {
            //Recorrer y totalizar
            double CostoFinal = 0;

            //Totalizamos desde vector
            for (int iterador = 0; iterador < clsGlobales.ProductoComposicion.GetLength(0); iterador++)
            {
                CostoFinal += Convert.ToDouble(clsGlobales.ProductoComposicion[iterador, 5]);
            }

            //Asignar Total
            txtTotal.Text = CostoFinal.ToString("#0.00000"); 

            //Pasar datos de la grilla al vector                
            int iterar = 0;
            int filas = dgvDatos.Rows.Count;
            double cTotal = 0;

            //La grilla tiene datos?
            if (filas>0)
            {
                //Recorro Matriz
                foreach (DataGridViewRow row in dgvDatos.Rows)
                {
                    if (!(row.Cells["Costo"].Value == null))
                    {
                      cTotal += Convert.ToDouble(row.Cells["Costo"].Value.ToString());
                    }

                    //Aumentar iterador
                    iterar++;
                }
    
                //Asignar Total
                txtTotal.Text = cTotal.ToString("#0.00000"); 
            }
        }

        #endregion


        #region Metodo CargarDetalleComposicion

        private void CargarDetalleComposicion()
        {
            bool bCharge = false;

            //Traer primero Datos de ProductoInsumo (esto a lo mejor se puede hacer con un solo Select en vez de 2)
            string cadSQL = "exec CargarDetCompInsumoById " + this.nIdArt;

            //Variable para contador de filas grilla
            int filas = 0;

            //DataTable
            DataTable myDataTable = clsDataBD.GetSql(cadSQL);

            //Mostrar Datos
            foreach (DataRow fila in myDataTable.Rows)
            {
                /*Agregar Fila*/
                dgvDatos.Rows.Add();

                // Cuento las filas de la grilla
                filas = dgvDatos.Rows.Count;

                // Si la grilla no está vacía
                if (filas > 0)
                {
                    //Posiciono la grilla en la última fila
                    dgvDatos.CurrentCell = dgvDatos[2, filas - 1];
                }

                //Cargar cTanda
                if (!(bCharge))
                {                    
                    txtCantArt.Text = fila["cTanda"].ToString();

                    bCharge = true;
                }

                //Cargar Datos a la fila                
                dgvDatos.CurrentRow.Cells["Codigo"].Value = fila["Codigo"].ToString();
                dgvDatos.CurrentRow.Cells["Descripcion"].Value = fila["Descripcion"].ToString();
                dgvDatos.CurrentRow.Cells["Cantidad"].Value = fila["Cantidad"].ToString();
                dgvDatos.CurrentRow.Cells["Unidad"].Value = fila["Unidad"].ToString();
                dgvDatos.CurrentRow.Cells["CU"].Value = fila["CU"].ToString();
                dgvDatos.CurrentRow.Cells["Costo"].Value = (Convert.ToDouble(fila["Cantidad"]) * Convert.ToDouble(fila["CU"])).ToString("#0.00000"); 
                dgvDatos.CurrentRow.Cells["Tipo"].Value = "I";
                dgvDatos.CurrentRow.Cells["IdGastoFijo"].Value = "0";
                dgvDatos.CurrentRow.Cells["IdInsumo"].Value = fila["IdInsumo"].ToString();
            }
        }

        #endregion

        #region Mertodo CargarToolTips

        private void CargarToolTips()
        {
            toolTip1.SetToolTip(this.btnAgregar, "Agregar");
            toolTip2.SetToolTip(this.btnBorrar, "Quitar");
            toolTip3.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip4.SetToolTip(this.btnSalir, "Salir!");
        }

        #endregion

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //Modifica y hay datos?... subir todo a vectores
            if (this.sEstado == "M" || this.dgvDatos.Rows.Count > 0)
            {
                btnAceptar.PerformClick();

            }
            else
            {
                //Cerrar
                this.Close();
            }
        }

        #region MostrarDatosPreCargados

        private void MostrarDatosPreCargados()
        {

            //Variable
            int Filas = 0;
            int longitud = clsGlobales.ProductoComposicion.GetLength(0);

            //No hay datos, salir
            if (clsGlobales.ProductoComposicion.GetLength(0) == 0) { return; }

            //Matriz de la Grilla
            for (int iterador = 0; iterador < longitud; iterador++)
            {

                //Agaregar Fila 
                dgvDatos.Rows.Add();

                // Cuento las filas de la grilla
                Filas = dgvDatos.Rows.Count;

                // Si la grilla no está vacía
                if (Filas > 0)
                {
                    // Posiciono la grilla en la última fila
                    dgvDatos.CurrentCell = dgvDatos[2, Filas - 1];
                }

                //Show cTanda pre loaded
                this.txtCantArt.Text = txtcArt.Text;

                //Cargar Datos a la fila                
                dgvDatos.CurrentRow.Cells["Codigo"].Value = clsGlobales.ProductoComposicion[iterador, 0];
                dgvDatos.CurrentRow.Cells["Descripcion"].Value = clsGlobales.ProductoComposicion[iterador, 1];
                dgvDatos.CurrentRow.Cells["Cantidad"].Value = clsGlobales.ProductoComposicion[iterador, 2];
                dgvDatos.CurrentRow.Cells["Unidad"].Value = clsGlobales.ProductoComposicion[iterador, 3];
                dgvDatos.CurrentRow.Cells["CU"].Value = clsGlobales.ProductoComposicion[iterador, 4];
                dgvDatos.CurrentRow.Cells["Costo"].Value = clsGlobales.ProductoComposicion[iterador, 5];
                dgvDatos.CurrentRow.Cells["Tipo"].Value = clsGlobales.ProductoComposicion[iterador, 6];
                dgvDatos.CurrentRow.Cells["IdGastoFijo"].Value = clsGlobales.ProductoComposicion[iterador, 7];
                dgvDatos.CurrentRow.Cells["IdInsumo"].Value = clsGlobales.ProductoComposicion[iterador, 8];


            }

        }

        #endregion

        #region CargarGrillaInsumos

        private void CargarGrillaInsumos()        
        { 
            string myCadena = "";
           // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
             DataTable mDtTable = new DataTable();

           //Cadena SQL
             myCadena = "Select * from Vista_InsumosComposicion";
             mDtTable = clsDataBD.GetSql(myCadena);
           // Evito que el dgv genere columnas automáticas
             dgvArt.AutoGenerateColumns = false;
           // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
             dgvArt.DataSource = mDtTable;
        }

        #endregion

        #region TextChanged

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
           CargarInsumoGto(txtCodigo.Text, "CodigoArticulo", "I");
        }

        private void txtDescrip_TextChanged(object sender, EventArgs e)
        {
           CargarInsumoGto(txtDescrip.Text, "Articulo", "I");
        }

        #endregion

        #region CargarInsumoGto

        private void CargarInsumoGto(string strTextoBuscar, string strCampo, string strTipo)
        {
            string myCadena = "";
         /*Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta*/
            DataTable mDtTable = new DataTable();

        // Cadena SQL 
            myCadena = "SELECT a.*, sa.IdRubroArticulo, i.*, u.UnidadMedida FROM Articulos a,Insumos i," + 
                    "SubrubrosArticulos sa, UnidadesMedida u WHERE a.IdSubrubroArticulo = sa.IdSubrubroArticulo AND" +
                    " i.IdArticulo = a.IdArticulo AND a.IdUnidadMedida = u.IdUnidadMedida ";

            //Sino esta vacio el texto a buscar, agrego.
            if (!(strTextoBuscar == ""))
            {
                if (strCampo == "CodigoArticulo")
                {
                    myCadena += " AND a." + strCampo + " = '" + strTextoBuscar + "' AND a.Activo = 1 AND CompIns=0";
                }
                else
                {
                    myCadena += " AND a." + strCampo + " like '" + strTextoBuscar + "%' AND a.Activo = 1 AND CompIns=0";
                }
            }
            else
            {
                myCadena += " AND a.Activo = 1 AND CompIns=0";
            }

            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.                     
            mDtTable = clsDataBD.GetSql(myCadena);
            // Evito que el dgv genere columnas automáticas
            dgvArt.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvArt.DataSource = mDtTable;            
        }

        #endregion

        #region Metodo para evaluar si un elemento esta en la grilla
        /*******************************************/
        /* Metodo    : bExisteEnGrilla()
         * Proposito : Verifica si un elemento fue cargado
         *             a la grilla de composicion.
         * Autor     : Developer
         * Fecha     : 13/11/2016
         * Paràmetros: Ninguno
         * Retorna   : .T. o .F.
         * ***************************************/
        private bool bExisteEnGrilla(string p_Codigo)
        {
            bool bExiste = false;

            foreach (DataGridViewRow Fila in dgvDatos.Rows)
            {
                if (Fila.Cells["Codigo"].Value.ToString() == p_Codigo)
                {
                    bExiste = true;
                    break;
                }
            }

            return bExiste;
        }

        #endregion

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string sCodigo = "";

            sCodigo = dgvArt.CurrentRow.Cells["CodigoArticulo"].Value.ToString();

            //Si ya se cargo a la grilla, salir.
            if (bExisteEnGrilla(sCodigo)) { return; }

            //Agaregar Fila 
            dgvDatos.Rows.Add();

            // Cuento las filas de la grilla
            int Filas = dgvDatos.Rows.Count;
            // Si la grilla no está vacía
            if (Filas > 0)
            {
                // Posiciono la grilla en la última fila
                dgvDatos.CurrentCell = dgvDatos[2, Filas - 1];
            }

            //Variable de tipo DatagridViewRow para traer datos de las grillas
            DataGridViewRow row = null;

            //Cargar fila actual
            row = dgvArt.CurrentRow;

           //Cargar los datos
            dgvDatos.CurrentRow.Cells["Codigo"].Value = row.Cells["CodigoArticulo"].Value.ToString();
            dgvDatos.CurrentRow.Cells["Descripcion"].Value = row.Cells["Articulo"].Value.ToString();
            dgvDatos.CurrentRow.Cells["Unidad"].Value = row.Cells["UnidadMedida"].Value.ToString();
            dgvDatos.CurrentRow.Cells["CU"].Value = row.Cells["Precio"].Value.ToString();
            dgvDatos.CurrentRow.Cells["Tipo"].Value = "I";
            dgvDatos.CurrentRow.Cells["IdGastoFijo"].Value = "0";
            dgvDatos.CurrentRow.Cells["IdInsumo"].Value = row.Cells["Id_Insumo"].Value.ToString();


 
            //Controlar boton Quitar
            if (dgvDatos.Rows.Count > 0)
            {
                this.btnBorrar.TabStop = true;
                this.btnBorrar.Enabled = true;
                this.btnAceptar.Enabled = true;
            }

            //Cambio
            if (this.sEstado == "M")
            {
                clsGlobales.bCambio = true;
            }
            else
            {
                clsGlobales.bCambio = false; //.N. 13-06-2018
            }

            //Foco
            dgvDatos.Focus();
        }

        private void dgvDatos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //Declaracion de Variables (Costo Unitario y Cantidad)
            DataGridViewRow row = dgvDatos.CurrentRow;

            //Null? salir....
            if (row.Cells["Cantidad"].Value == null)
            {
                //Establecer null al costo final
                dgvDatos.CurrentRow.Cells["Costo"].Value = null;

                //Seteo CostoFinal
                this.SetCostoFinal();

                //Volver       
                return;
            }
            else if (Convert.ToDouble(row.Cells["Cantidad"].Value) == 0)
            {
                //Null al costo final
                dgvDatos.CurrentRow.Cells["Cantidad"].Value = null;
                dgvDatos.CurrentRow.Cells["Costo"].Value = null;

                //Seteo CostoFinal
                this.SetCostoFinal();

                //Volver
                return;
            }

            //Variables para el calculo del costo final ******
            double CU = double.Parse(row.Cells["CU"].Value.ToString());
            double CA = double.Parse(row.Cells["Cantidad"].Value.ToString());

            //Calcular Costo Final ******
            dgvDatos.CurrentRow.Cells["Cantidad"].Value = CA.ToString("#0.00000");
            dgvDatos.CurrentRow.Cells["Costo"].Value = (CU * CA).ToString("#0.00000");

            //Seteo CostoFinal
            this.SetCostoFinal();

        }

        private void dgvDatos_SelectionChanged(object sender, EventArgs e)
        {
            // Si la grilla tiene artículos
            if (dgvDatos.RowCount > 0)
            {
                // Almaceno én una variable la posición de fila en la que me encuentro
                int fila = dgvDatos.CurrentRow.Index;
                // Pongo el foco de la fila en la columna cantidad
                dgvDatos.CurrentCell = dgvDatos["Cantidad", fila];
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            //Quitar Fila
            if (dgvDatos.Rows.Count > 0)
            {
                //Resize Vector para eliminados
                clsGlobales.ProductosEliminados = (string[,])clsValida.ResizeMatriz(clsGlobales.ProductosEliminados, new int[] { clsGlobales.ProductosEliminados.GetLength(0) + 1, 2 });

                //IdInsumo
                clsGlobales.ProductosEliminados[clsGlobales.ProductosEliminados.GetLength(0) - 1, 0] = dgvDatos.CurrentRow.Cells["IdInsumo"].Value.ToString();

                //Guardar "Tipo"
                clsGlobales.ProductosEliminados[clsGlobales.ProductosEliminados.GetLength(0) - 1, 1] = dgvDatos.CurrentRow.Cells["Tipo"].Value.ToString();

                //Remover fila
                dgvDatos.Rows.RemoveAt(dgvDatos.CurrentRow.Index);

                //Cambio
                if (this.sEstado == "M")
                {
                    clsGlobales.bCambio = true;
                }
                else
                {
                    clsGlobales.bCambio = false; //.N. 13-06-2018
                }

            }

            //Controlar boton Quitar
            if (dgvDatos.Rows.Count == 0)
            {
                this.btnBorrar.TabStop = false;
                this.btnBorrar.Enabled = false;
                this.btnAceptar.TabStop = false;
                this.btnAceptar.Enabled = false;
            }

            //Foco
            this.dgvDatos.Focus();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Solo si esta habilitado
            if (!(this.btnAceptar.Enabled))
            {
                return;
            }

            //Validaciones //****************************************/
            if (string.IsNullOrEmpty(txtCantArt.Text))
            {
                MessageBox.Show("Debe completar 'Cantidad por Tanda'!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCantArt.Focus();
                return; 
            }
            else if (!(clsGlobales.cValida.IsNumeric(txtCantArt.Text))) 
            {
                MessageBox.Show("Debe completar 'Cantidad por Tanda'!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCantArt.Focus();
                return;  
            }
            else if (Convert.ToInt32(txtCantArt.Text)==0)
            {
                MessageBox.Show("La 'Cantidad por Tanda' debe ser mayor a 0!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCantArt.Focus();
                return; 
            }

            //Si la grilla esta vacia... salir
            if (dgvDatos.Rows.Count == 0)
            {
                MessageBox.Show("Debe completar los 'Insumos' para la composición del nuevo Insumo!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dgvDatos.Focus();
                return;
            }
            /*******************************************************/

            //Si no ha completado cantidades para todos los casos, mensaje y salir
            foreach (DataGridViewRow row in dgvDatos.Rows)
            {

                if (row.Cells["Cantidad"].Value == null)
                {
                    MessageBox.Show("Debe completar, para todos los casos, la Cantidad!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgvDatos.Focus();
                    return;
                }

            }
            /*****************************************************/


            //Pasar datos de la grilla al vector                
            int iterador = 0;
            int filas = dgvDatos.Rows.Count;

            //Hago Resize
            clsGlobales.ProductoComposicion = (string[,])clsValida.ResizeMatriz(clsGlobales.ProductoComposicion, new int[] { filas, 9 });

            //Recorro Matriz
            foreach (DataGridViewRow row in dgvDatos.Rows)
            {
                /*Guardar en Matriz*/
                clsGlobales.ProductoComposicion[iterador, 0] = row.Cells["Codigo"].Value.ToString();
                clsGlobales.ProductoComposicion[iterador, 1] = row.Cells["Descripcion"].Value.ToString();
                clsGlobales.ProductoComposicion[iterador, 2] = row.Cells["Cantidad"].Value.ToString();
                clsGlobales.ProductoComposicion[iterador, 3] = row.Cells["Unidad"].Value.ToString();
                clsGlobales.ProductoComposicion[iterador, 4] = row.Cells["CU"].Value.ToString();
                clsGlobales.ProductoComposicion[iterador, 5] = row.Cells["Costo"].Value.ToString();
                clsGlobales.ProductoComposicion[iterador, 6] = row.Cells["Tipo"].Value.ToString();
                clsGlobales.ProductoComposicion[iterador, 7] = row.Cells["IdGastoFijo"].Value.ToString();
                clsGlobales.ProductoComposicion[iterador, 8] = row.Cells["IdInsumo"].Value.ToString();

                //Aumentar iterador
                iterador++;
            }
            
            //Sino es null
            if (!(txtcArt == null))
            {
                //Tanda
                txtcArt.Text = txtCantArt.Text;
            }

            //Salir
            this.Close();

        }

        private void txtCantArt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

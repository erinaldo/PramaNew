using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Prama.Formularios.Articulos;

namespace Prama
{
    public partial class frmArticulosComposicion : Form
    {

        #region Declaracion Variables a nivel de formulario
        
        public string sTipo = "";   //(I)nsumo o (G)asto (para grilla)
        public string sEstado = ""; //A o M (estado)
        public int nIdArt = 0;      //IdArticulo
        public int IdProd = 0;      //IdProducto
        public double pIva = 0;     //IVA

        #endregion

        #region Constructor

        public frmArticulosComposicion(string p_Estado, int p_IdArticulo, double p_iva)
        {
            InitializeComponent();
            this.sTipo = "";
            this.sEstado = p_Estado;
            this.nIdArt = p_IdArticulo;
            this.pIva = p_iva;
        }

        #endregion

        #region Evento Load del Formulario

        private void frmArticulosComposicion_Load(object sender, EventArgs e)
        {
			
			//icon
            clsFormato.SetIconForm(this); 
			
            //Inicializars  
            this.sTipo = "I";

            // Llamo al método cargar CargarCambo para cboAlmacen N.
            clsDataBD.CargarCombo(this.cboArea, "AreasProduccion", "AreaProduccion", "IdAreaProduccion");


            //Area por defecto
            cboArea.SelectedValue = 1;
            cboArea.TabStop = false;
            cboArea.Enabled = false;
                
            //Titulo Ventana
            this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;

            //Cargar Grilla Formulario
            getCargarGrilla();

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
                }
                else
                {
                    this.btnAceptar.TabStop = false;
                    this.btnAceptar.Enabled = false;
                }

            }
            else if (this.sEstado == "M")
            {

                if (clsGlobales.ProductoDatosST[0] != "")
                {

                    //Mostrar Datos Vector 
                    txtCantArt.Text = clsGlobales.ProductoDatosST[0].ToString();
                    txtCostoTanda.Text = clsGlobales.ProductoDatosST[1].ToString();
                    txtCostoArt.Text = (Double.Parse(txtCostoTanda.Text) / Double.Parse(txtCantArt.Text)).ToString("#0.00000");
                    cboArea.SelectedValue = Convert.ToInt32(clsGlobales.ProductoDatosST[5]);

                    //Cambio algo? verificamos si al modificar se cambio algo o no en la composicion.
                    if (clsGlobales.bCambio)
                    {
                        //Muestra los datos que estan en los vectores en memoria
                        MostrarDatosPreCargados();
                    }
                    else
                    {
                        //Esta modificando, los datos vienen de la base
                        CargarDatosComposicion();
                    }
                }
                else //Esta modificando pero sin datos en la base.
                {
                    if (clsGlobales.ProductoDatos[0] != 0)
                    {
                        //Mostrar Datos Vector String
                        txtCantArt.Text = clsGlobales.ProductoDatos[0].ToString();
                        txtCostoTanda.Text = clsGlobales.ProductoDatos[1].ToString();
                        txtCostoArt.Text = (Double.Parse(txtCostoTanda.Text) / Double.Parse(txtCantArt.Text)).ToString("#0.00000");
                        cboArea.SelectedValue = Convert.ToInt32(clsGlobales.ProductoDatos[5]);

                    }

                    //Por si esta yendo y viniendo entre forms
                    //con datos ya cargados (utiliza los vectores)
                    //modificando.
                    MostrarDatosPreCargados();
                }

                //Boton Aceptar
                if (dgvDatos.Rows.Count > 0)
                {
                    this.btnAceptar.TabStop = true;
                    this.btnAceptar.Enabled = true;
                }
                else
                {
                    this.btnAceptar.TabStop = false;
                    this.btnAceptar.Enabled = false;
                }
            }
            //Iva que viene desde el formulario anterior
            txtPorcIVA.Text = pIva.ToString("#0.00");

            //Cargar Datos Precios
            CargarVistaPrecios();

            // Cargo los toolsTip
            CargarToolTips();

            //Foco
            txtCantArt.Focus();
        }

        #endregion

        #region Metodo para filtrar la grilla de articulos o gastos

        private void CargarInsumoGto(string strTextoBuscar, string strCampo, string strTipo)
        {
            string myCadena = "";
         /*Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta*/
            DataTable mDtTable = new DataTable();

            //EVALUAR SI ES INSUMO O GASTO
            switch(strTipo)
            {
                case "I": //INSUMOS

                    // Cadena SQL 
                     myCadena = "SELECT a.*, sa.IdRubroArticulo, i.*, u.UnidadMedida FROM Articulos a,Insumos i," + 
                                "SubrubrosArticulos sa, UnidadesMedida u WHERE a.IdSubrubroArticulo = sa.IdSubrubroArticulo AND" +
                                " i.IdArticulo = a.IdArticulo AND a.IdUnidadMedida = u.IdUnidadMedida ";

                     //Sino esta vacio el texto a buscar, agrego.
                     if (!(strTextoBuscar == "")) {
                         if (strCampo == "CodigoArticulo")
                         {
                             myCadena += " AND a." + strCampo + " = '" + strTextoBuscar + "' AND a.Activo = 1";
                         }
                         else
                         {
                             myCadena += " AND a." + strCampo + " like '" + strTextoBuscar + "%' AND a.Activo = 1";
                         }
                     }

                     // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.                     
                     mDtTable = clsDataBD.GetSql(myCadena);
                     // Evito que el dgv genere columnas automáticas
                     dgvArt.AutoGenerateColumns = false;
                     // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
                     dgvArt.DataSource = mDtTable;

                     break;
                
                case "G": //GASTOS

                     // Cadena SQL 
                     myCadena = "SELECT gf.*, um.UnidadMedida, um.AbreviaturaUnidad FROM GastosFijos gf, UnidadesMedida um " +
	                            " WHERE gf.IdUnidadMedida = um.IdUnidadMedida ";

                     //Sino esta vacio el texto a buscar, agrego.
                     if (!(strTextoBuscar == ""))
                     {
                         if (strCampo == "Codigo")
                         {
                             myCadena += " AND gf." + strCampo + " = '" + strTextoBuscar + "'";
                         }
                         else
                         {
                             myCadena += " AND gf." + strCampo + " like '" + strTextoBuscar + "%'";
                         }
                     }

                     // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.                     
                     mDtTable = clsDataBD.GetSql(myCadena);
                     // Evito que el dgv genere columnas automáticas
                     dgvGastos.AutoGenerateColumns = false;
                     // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
                     dgvGastos.DataSource = mDtTable;

                     break;  
            }
            
        }

        #endregion

        #region Método para cargar la grilla

        /*******************************************/
        /* Metodo    : getCargarGrilla
         * Proposito : Obtiene los artìculos cargados
         * Autor     : Developer
         * Fecha     : 13/11/2016
         * Paràmetros: Ninguno
         * Retorna   : Nada.
         * ***************************************/
        private void getCargarGrilla()
        {

            string myCadena = "";
            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();

            // Cadena SQL 
            if (this.rdoInsumo.Checked) //Insumos
            {
                //Cadena SQL
                    myCadena = "exec CargarArticulos 1";
                    mDtTable = clsDataBD.GetSql(myCadena);
                // Evito que el dgv genere columnas automáticas
                    dgvArt.AutoGenerateColumns = false;
                // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
                    dgvArt.DataSource = mDtTable;
            }
            else //Gastos
            {
                myCadena = "exec CargarGastosFijos";
                // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
                mDtTable = clsDataBD.GetSql(myCadena);
                // Evito que el dgv genere columnas automáticas
                dgvGastos.AutoGenerateColumns = false;
                // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
                dgvGastos.DataSource = mDtTable;
            }    

            
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
                    bExiste=true;
                    break;
                }
            }

            return bExiste;
        }

        #endregion

        #region Metodo que calcula total de la tanda
        /*******************************************/
        /* Metodo    : setSumarTanda()
         * Proposito : Calcula el total de la tanda
         *             sumando los costos finales
         * Autor     : Developer
         * Fecha     : 13/11/2016
         * Paràmetros: Ninguno
         * Retorna   : Nada
         * ***************************************/
        private void setSumarTanda()
        {
            double dTotal = 0;
            double dCostoArt = 0;

          //Si es 0 o vacio
            if (Int32.Parse(txtCantArt.Text) == 0) { return; }

          //Calcular total de la tanda   
            foreach (DataGridViewRow Fila in dgvDatos.Rows)
            {
                if (!(Fila.Cells["Costo"].Value==null)) //Sino es nulo el costo final
                {
                    dTotal += Double.Parse(Fila.Cells["Costo"].Value.ToString());
                }                
            } 

          //Mostrar total de la tanda
            this.txtCostoTanda.Text = dTotal.ToString("#0.00000");

         //Costo por Artìculo
            dCostoArt = (dTotal / Convert.ToInt32(this.txtCantArt.Text));
            if (dCostoArt > 0)
            {
                this.txtCostoArt.Text = dCostoArt.ToString("#0.00000");
            }
            else
            {
                this.txtCostoArt.Text = "0.00000";
            }

        }

        #endregion

        #region MostrarDatosPreCargados

        private void MostrarDatosPreCargados()
        {

            //Variable
            int Filas = 0;
            int longitud = clsGlobales.ProductoComposicion.GetLength(0);

            //No hay datos, salir
            if (clsGlobales.ProductoComposicion.GetLength(0) == 0) { return; }

            //Matriz de la Grilla
            for (int iterador=0; iterador < longitud; iterador++)
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

            //Mostrar Cantidad de Articulos por Tanda
            if (clsGlobales.ProductoDatos[0] == 0)
            {
                txtCantArt.Text = clsGlobales.ProductoDatosST[0].ToString();
                txtCostoTanda.Text = clsGlobales.ProductoDatosST[1].ToString();
                txtCostoArt.Text = (Convert.ToDouble(txtCostoTanda.Text) / Convert.ToInt32(this.txtCantArt.Text)).ToString("#0.00000");
            }
            else
            {
                txtCantArt.Text = clsGlobales.ProductoDatos[0].ToString();
                txtCostoTanda.Text = clsGlobales.ProductoDatos[1].ToString("#0.00000");
                txtCostoArt.Text = (Convert.ToDouble(txtCostoTanda.Text) / Convert.ToInt32(this.txtCantArt.Text)).ToString("#0.00000");
            }
        }

        #endregion

        #region Metodo CargarDatosComposicion

        /*******************************************/
        /* Metodo    : CargarDatosComposicion()
         * Proposito : Carga los datos de la composicion
         *             a partir de los datos almacenados
         * Autor     : Developer
         * Fecha     : 13/11/2016
         * Paràmetros: Ninguno
         * Retorna   : Nada
         * ***************************************/
        private void CargarDatosComposicion()
        { 

            //Datos Producto
                MostrarDatosProducto();

            //Mostrar Detalle Composicion en grilla
                CargarDetalleComposicion();
               
        }

        #endregion
        
        #region Metodo CargarDetalleComposicion

        private void CargarDetalleComposicion()
        {
            //Traer primero Datos de ProductoInsumo (esto a lo mejor se puede hacer con un solo Select en vez de 2)
                string cadSQL = "exec CargarDetComposicionById " + this.IdProd;

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

                        //Cargar Datos a la fila                
                            dgvDatos.CurrentRow.Cells["Codigo"].Value = fila["Codigo"].ToString();
                            dgvDatos.CurrentRow.Cells["Descripcion"].Value = fila["Descripcion"].ToString();
                            dgvDatos.CurrentRow.Cells["Cantidad"].Value = fila["Cantidad"].ToString();
                            dgvDatos.CurrentRow.Cells["Unidad"].Value = fila["Unidad"].ToString();
                            dgvDatos.CurrentRow.Cells["CU"].Value = fila["CU"].ToString();
                            dgvDatos.CurrentRow.Cells["Costo"].Value = fila["Costo"].ToString();
                            dgvDatos.CurrentRow.Cells["Tipo"].Value = "I";
                            dgvDatos.CurrentRow.Cells["IdGastoFijo"].Value = "0";
                            dgvDatos.CurrentRow.Cells["IdInsumo"].Value = fila["IdInsumo"].ToString();
                }   

             //CadSQL
                cadSQL = "exec CargarDetGastosComp " + this.IdProd;

             //DataTable
                DataTable mDataTable = clsDataBD.GetSql(cadSQL);

            //Mostrar Datos
                foreach (DataRow fila in mDataTable.Rows)
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

                        //Cargar Datos a la fila                
                            dgvDatos.CurrentRow.Cells["Codigo"].Value = fila["Codigo"].ToString();
                            dgvDatos.CurrentRow.Cells["Descripcion"].Value = fila["Descripcion"].ToString();
                            dgvDatos.CurrentRow.Cells["Cantidad"].Value = fila["Cantidad"].ToString();
                            dgvDatos.CurrentRow.Cells["Unidad"].Value = fila["Unidad"].ToString();
                            dgvDatos.CurrentRow.Cells["CU"].Value = fila["CU"].ToString();
                            dgvDatos.CurrentRow.Cells["Costo"].Value = (Convert.ToDouble(fila["CU"])*Convert.ToDouble(fila["Cantidad"])).ToString("#0.00000");
                            dgvDatos.CurrentRow.Cells["Tipo"].Value = "G";
                            dgvDatos.CurrentRow.Cells["IdGastoFijo"].Value = fila["IdGastoFijo"].ToString();
                            dgvDatos.CurrentRow.Cells["IdInsumo"].Value = "0";
                }

        }

        #endregion

        #region Metodo MostrarDatosProducto

        /*******************************************/
        /* Metodo    : MostrarDatosProducto)
         * Proposito : Muestra los datos del Producto
         *             que se corresponden con la 
         *             tanda establecida.
         * Autor     : Developer
         * Fecha     : 13/11/2016
         * Paràmetros: Ninguno
         * Retorna   : Nada
         * ***************************************/
        private void MostrarDatosProducto()
        { 
          //Traer Datos del Producto (Tanda, Cantidad de Articulos, etc)
            string cadSQL = "exec CargarProductoByIDArticulo " + this.nIdArt;

          //DataTable
            DataTable myDataTable = clsDataBD.GetSql(cadSQL);

          //Mostrar Datos
            foreach (DataRow fila in myDataTable.Rows)
            {
                IdProd = Convert.ToInt32(fila["IdProducto"].ToString()); //Guardar IdProducto para Grilla

                //Mostrar datos del Producto relacionados con la Tanda.
                txtCantArt.Text = fila["Tanda"].ToString();
                txtCostoTanda.Text = fila["CostoAcumulado"].ToString();
                if (!(txtCantArt.Text == "" || txtCostoTanda.Text == ""))
                {
                    txtCostoArt.Text = (Convert.ToDouble(txtCostoTanda.Text) / Convert.ToInt32(txtCantArt.Text)).ToString("#0.00000");
                }
                else
                {
                    txtCostoArt.Text = "0.00000";
                }

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
            toolTip5.SetToolTip(this.btnInsumos, "ABM Insumos");
        }

        #endregion

        #region Metodo: CargarVistaPrecios

        private void CargarVistaPrecios()
        {
            double dValor = 0;

            //Salir si no hay dato
            if (string.IsNullOrEmpty(txtCostoArt.Text))
            {
                dValor = 0;
            }
            else
            {
                dValor = Convert.ToDouble(this.txtCostoArt.Text);
            }    
            
            //Traer la vista de coeficientes y precios
            String mySQL = "exec GeneraVistaPrecios " + dValor;
            dgvPrecios.AutoGenerateColumns = false;
            DataTable myDataVistaPrecio = new DataTable();
            myDataVistaPrecio = clsDataBD.GetSql(mySQL);
            dgvPrecios.DataSource = myDataVistaPrecio;

            //Traer la Vista de precios con IVA
            String mySQL_Iva = "exec GeneraVistaPreciosIVA " + dValor + "," + Convert.ToDouble(txtPorcIVA.Text);
            dgvPreciosIVA.AutoGenerateColumns = false;
            DataTable myDataVistaPrecioIVA = new DataTable();
            myDataVistaPrecioIVA = clsDataBD.GetSql(mySQL_Iva);
            dgvPreciosIVA.DataSource = myDataVistaPrecioIVA;

        }

        #endregion


        #region Eventos de botones

        #region btnAgregar: Click

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string sCodigo = "";
            
        //Tomar el còdigo de Insumo o Gasto segun se trate.
            if (rdoInsumo.Checked)
            {
                sCodigo = dgvArt.CurrentRow.Cells["CodigoArticulo"].Value.ToString();
            }
            else 
            {
                sCodigo = dgvGastos.CurrentRow.Cells["CodGasto"].Value.ToString(); 
            }

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

        //Insumos
                if (rdoInsumo.Checked)
                {
                //Cargar fila actual
                    row = dgvArt.CurrentRow;

                //Cargar los datos
                    dgvDatos.CurrentRow.Cells["Codigo"].Value = row.Cells["CodigoArticulo"].Value.ToString();
                    dgvDatos.CurrentRow.Cells["Descripcion"].Value = row.Cells["Articulo"].Value.ToString();
                    dgvDatos.CurrentRow.Cells["Unidad"].Value = row.Cells["UnidadMedida"].Value.ToString();
                    dgvDatos.CurrentRow.Cells["CU"].Value = row.Cells["Precio"].Value.ToString();
                    dgvDatos.CurrentRow.Cells["Tipo"].Value = this.sTipo;
                    dgvDatos.CurrentRow.Cells["IdGastoFijo"].Value = "0";
                    dgvDatos.CurrentRow.Cells["IdInsumo"].Value = row.Cells["Id_Insumo"].Value.ToString();  

                       
                }
                else //Gastos
                {
                //Cargar Fila   
                    row = dgvGastos.CurrentRow;
                //Cargar los datos
                    dgvDatos.CurrentRow.Cells["Codigo"].Value = row.Cells["CodGasto"].Value.ToString();
                    dgvDatos.CurrentRow.Cells["Descripcion"].Value = row.Cells["GastoFijo"].Value.ToString();
                    dgvDatos.CurrentRow.Cells["Unidad"].Value = row.Cells["UM"].Value.ToString();
                    dgvDatos.CurrentRow.Cells["CU"].Value = row.Cells["CostoUnitario"].Value.ToString();
                    dgvDatos.CurrentRow.Cells["Tipo"].Value = sTipo;
                    dgvDatos.CurrentRow.Cells["IdGastoFijo"].Value = row.Cells["IdGasto"].Value.ToString();
                    dgvDatos.CurrentRow.Cells["IdInsumo"].Value = "0";  

                }
            
                
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

       //Foco
            dgvDatos.Focus();
    }

    #endregion

        #region btnBorrar: Click

        private void btnBorrar_Click(object sender, EventArgs e)
        {
          //Quitar Fila
            if (dgvDatos.Rows.Count > 0)
            {
                //Resize Vector para eliminados
                  clsGlobales.ProductosEliminados = (string[,])clsValida.ResizeMatriz(clsGlobales.ProductosEliminados, new int[] {clsGlobales.ProductosEliminados.GetLength(0)+1, 2 });

                //Segun el "Tipo" guardar en matriz IdInsumo o IdGastoFijo
                    if (dgvDatos.CurrentRow.Cells["Tipo"].Value.ToString()=="I")
                    {
                       clsGlobales.ProductosEliminados[clsGlobales.ProductosEliminados.GetLength(0)-1,0]=dgvDatos.CurrentRow.Cells["IdInsumo"].Value.ToString();
                    }
                    else
                    {
                        clsGlobales.ProductosEliminados[clsGlobales.ProductosEliminados.GetLength(0)-1,0]=dgvDatos.CurrentRow.Cells["IdGastoFijo"].Value.ToString();
                    }

                //Guardar "Tipo"
                  clsGlobales.ProductosEliminados[clsGlobales.ProductosEliminados.GetLength(0)-1,1]= dgvDatos.CurrentRow.Cells["Tipo"].Value.ToString();

               //Remover fila
                  dgvDatos.Rows.RemoveAt(dgvDatos.CurrentRow.Index);

              //Cambio
                  if (this.sEstado == "M")
                  {
                      clsGlobales.bCambio = true;
                  }
                    
            }

            //Controlar boton Quitar
            if (dgvDatos.Rows.Count == 0)
            {
               this.btnBorrar.TabStop = false;
               this.btnBorrar.Enabled = false;
               this.btnAceptar.Enabled = false;
            }

          //Sumar la tanda
            this.setSumarTanda();

          //Foco
            this.dgvDatos.Focus();
        }

        #endregion

        #region btnAceptar: Click

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Solo si esta habilitado
            if (!(this.btnAceptar.Enabled))
            { 
                return;
            }

            //Validaciones //****************************************/

            //Si la grilla esta vacia... salir
            if (dgvDatos.Rows.Count == 0)
            {
                MessageBox.Show("Debe completar los 'Insumos' Y/o 'Gastos' para la composición del Producto!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dgvDatos.Focus();
                return;
            }
            /*******************************************************/

            //Si esta vacia la cantidad de articulos o es igual a 0, salir.
            if (txtCantArt.Text == "")
            {
                MessageBox.Show("Debe completar 'Cantidad de Artículos por Tanda' para la composición del Producto!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCantArt.Focus();
                return;
            }
            else if (Convert.ToInt32(txtCantArt.Text) == 0)
            {
                MessageBox.Show("La 'Cantidad de Artículos por Tanda' para la composición del Producto no puede ser 0!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCantArt.Focus();
                return;
            }

            //Area Produccion
            if (Convert.ToInt32(cboArea.SelectedValue) == 0)
            {
                MessageBox.Show("Por favor, especifique el 'Area de Producciòn'", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboArea.Focus();
                return;
            }
            
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
            double dCostoIns = 0;
            double dCostoGto = 0;

            //Hago Resize
            clsGlobales.ProductoDatos = (double[])clsValida.ResizeVector(clsGlobales.ProductoDatos, 6);
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

                //Calcular Costos por Gastos e Insumos
                if (row.Cells["Tipo"].Value.ToString() == "G")
                {
                    dCostoGto += Double.Parse(row.Cells["Costo"].Value.ToString());
                }
                else
                {
                    dCostoIns += Double.Parse(row.Cells["Costo"].Value.ToString());
                }

                //Aumentar iterador
                iterador++;
            }

            /* Guardar en Vector */
            clsGlobales.ProductoDatos[0] = Double.Parse(txtCantArt.Text);               // Cantidad Artìculos por Tanda.
            clsGlobales.ProductoDatos[1] = Double.Parse(txtCostoTanda.Text);            // Costo de la tanda (acumulado)
            clsGlobales.ProductoDatos[2] = dCostoIns;                                   // Costo Insumos
            clsGlobales.ProductoDatos[3] = dCostoGto;                                   // Costo Gastos
            clsGlobales.ProductoDatos[4] = Double.Parse(txtCostoArt.Text);              // Costo Articulo
            clsGlobales.ProductoDatos[5] = Convert.ToDouble(cboArea.SelectedValue);     // Almacen

            clsGlobales.ProductoDatosST = (string[])clsValida.ResizeVector(clsGlobales.ProductoDatosST, 6);

            /* Guardar en Vector ST */
            clsGlobales.ProductoDatosST[0] = txtCantArt.Text;                       // Cantidad Artìculos por Tanda.
            clsGlobales.ProductoDatosST[1] = txtCostoTanda.Text;                    // Costo de la tanda (acumulado)
            clsGlobales.ProductoDatosST[2] = dCostoIns.ToString("#0.00000");        // Costo Insumos
            clsGlobales.ProductoDatosST[3] = dCostoGto.ToString("#0.00000");        // Costo Gastos
            clsGlobales.ProductoDatosST[4] = txtCostoArt.Text;                      // Costo Articulo
            clsGlobales.ProductoDatosST[5] = cboArea.SelectedValue.ToString();   // Almacen
            //Salir
            this.Close();

        }

        #endregion

        #region btnSalir: Click

        private void btnSalir_Click(object sender, EventArgs e)
        {
          //Modifica y hay datos?... subir todo a vectores
            if (this.sEstado == "M" || this.dgvDatos.Rows.Count > 0)
            {
                btnAceptar.PerformClick();

              //Cerrar
                this.Close();

            }
            else
            {
              //Cerrar
                this.Close();
            }

        }

        #endregion

        #endregion

        #region Eventos de txtCantArt

        private void txtCantArt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Solo enteros
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }            

        }

        private void txtCantArt_Leave(object sender, EventArgs e)
        {
            //Si cargo la cantidad de la tanda...
            if (this.txtCantArt.Text.Length > 0)
            {
                this.btnAgregar.Enabled = true;
                this.btnBorrar.Enabled = true;

                //Validar por 0
                if (Int32.Parse(this.txtCantArt.Text) == 0)
                {
                    txtCantArt.Text = "1";
                }

                //Sumar la tanda
                this.setSumarTanda();

            }
            else
            {
                this.btnAgregar.Enabled = false;
                this.btnBorrar.Enabled = false;
            }

        }

        private void txtCantArt_TextChanged(object sender, EventArgs e)
        {
            //Si cargo la cantidad de la tanda...
            if (this.txtCantArt.Text.Length > 0)
            {
                this.btnAgregar.Enabled = true;
                this.btnBorrar.Enabled = true;
            }
            else
            {
                this.btnAgregar.Enabled = false;
                this.btnBorrar.Enabled = false;
            }
        }

        #endregion
        
        #region Evento SelectedChanged de la Grilla dgvDatos

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

        #endregion

        #region Eventos radio buttons

        private void rdoInsumo_CheckedChanged(object sender, EventArgs e)
        {
            //Mostrar u ocultar grids
              this.dgvArt.Visible = true;
              this.dgvGastos.Visible = false;
              this.lblDescrip.Text = "Insumo:";
            
            //Establecer Tipo (para luego cargar en columna de grilla)
              this.sTipo = "I";

              this.txtCodigo.Text = "";
              this.txtDescrip.Text = "";

            //Grilla
              getCargarGrilla();
        }

        private void rdoGastos_CheckedChanged(object sender, EventArgs e)
        {
           //Mostrar u ocultar grids
             this.dgvArt.Visible = false;
             this.dgvGastos.Visible = true;
             this.lblDescrip.Text = "Gasto Fijo:";

          //Establecer Tipo (para luego cargar en columna de grilla)
             this.sTipo = "G";
             this.txtCodigo.Text = "";
             this.txtDescrip.Text = "";   
          //Grilla
             getCargarGrilla();
        }

        #endregion

        #region Evento CellEndEdit (calcula el Costo Final)

        private void dgvDatos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //Declaracion de Variables (Costo Unitario y Cantidad)
                DataGridViewRow row = dgvDatos.CurrentRow;

            //Null? salir....
                if (row.Cells["Cantidad"].Value == null)
                {
                   //Establecer null al costo final
                    dgvDatos.CurrentRow.Cells["Costo"].Value = null;
                  //Sumar Tanda
                    this.setSumarTanda();
                  //Volver       
                    return;
                }
                else if (Convert.ToDouble(row.Cells["Cantidad"].Value) == 0)
                {
                  //Null al costo final
                    dgvDatos.CurrentRow.Cells["Cantidad"].Value = null;
                    dgvDatos.CurrentRow.Cells["Costo"].Value = null;
                  //Sumar Tanda
                    this.setSumarTanda();
                  //Volver
                    return;
                }
           
            //Variables para el calculo del costo final ******
                double CU = double.Parse(row.Cells["CU"].Value.ToString());
                double  CA = double.Parse(row.Cells["Cantidad"].Value.ToString());

            //Calcular Costo Final ******
                dgvDatos.CurrentRow.Cells["Cantidad"].Value = CA.ToString("#0.00000");
                dgvDatos.CurrentRow.Cells["Costo"].Value = (CU * CA).ToString("#0.00000");

            //Cambio
                if (this.sEstado == "M")
                {
                    clsGlobales.bCambio = true;
                }

            //Sumar la tanda ************
                this.setSumarTanda();

        }

        #endregion
        
        #region Evento TextChanged (txtCodigo)

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            if (rdoInsumo.Checked)
            {
             // Cargo los Insumos filtrados
                CargarInsumoGto(txtCodigo.Text, "CodigoArticulo","I");
            }
            else
            {
             //Cargo los Gastos filtrados
                CargarInsumoGto(txtCodigo.Text, "Codigo","G");
            }
        }

        #endregion

        #region Evento TextChanged (txtRazon)

        private void txtRazon_TextChanged(object sender, EventArgs e)
        {
            if (rdoInsumo.Checked)
            {
                // Cargo los Insumos filtrados
                CargarInsumoGto(txtDescrip.Text, "Articulo", "I");
            }
            else
            {
                //Cargo los Gastos filtrados
                CargarInsumoGto(txtDescrip.Text, "Descrip", "G");
            }
        }

        #endregion 
        
        #region Evento Click boton Quitar Filtro

        private void btnQuitarFiltro_Click(object sender, EventArgs e)
        {
            if (rdoInsumo.Checked)
            {
                // Quitar Filtro
                CargarInsumoGto("", "", "I");
            }
            else
            {
                // Quitar Filtro
                CargarInsumoGto("", "", "G");
            }
        }

        #endregion
        
        #region Eventos Enter (cuadros de busqueda)

        private void txtCodigo_Enter(object sender, EventArgs e)
        {
            // Limpio los demás textbox
            txtDescrip.Text = "";
        }

        private void txtDescrip_Enter(object sender, EventArgs e)
        {
            //Clean
            txtCodigo.Text = "";
        }

        #endregion

        #region Eventos KeyPress

        private void txtPorcIVA_KeyPress(object sender, KeyPressEventArgs e)
        {
            string senderText = (sender as TextBox).Text;
            string senderName = (sender as TextBox).Name;
            string[] splitByDecimal = senderText.Split('.');
            int cursorPosition = (sender as TextBox).SelectionStart;

            char ch = e.KeyChar;

            if (ch == 44)
            {
                e.KeyChar = Convert.ToChar(46);
                ch = e.KeyChar;

            }
            //PUNTO DECIMAL. N.
            if (ch == 46 && senderText.IndexOf('.') != -1)
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

            //CONTROLAR CANTIDAD DE DECIMALES LUEGO DEL SEPARADOR DECIMAL. N.
            if (!char.IsControl(e.KeyChar)
                && senderText.IndexOf('.') < cursorPosition
                && splitByDecimal.Length > 1
                && splitByDecimal[1].Length == 2)
            {
                e.Handled = true;
            }
        }
        
        #endregion

        #region Eventos Enter

        private void txtPorcIVA_Enter(object sender, EventArgs e)
        {
            txtPorcIVA.SelectionStart = 0;
            txtPorcIVA.SelectionLength = txtPorcIVA.Text.Length;
        }

        #endregion

        #region Eventos Click de Otros Controles

        private void txtPorcIVA_Click(object sender, EventArgs e)
        {
            txtPorcIVA.SelectAll();
        }

        private void btnInsumos_Click(object sender, EventArgs e)
        {
           // MessageBox.Show("Funcionalidad a implementar en futura BETA!", "Informaciòn!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            frmInsumos myForm = new frmInsumos(1);
            myForm.ShowDialog();

          //Cargar nuevamente la grilla de insumos
            string myCadena = "exec CargarArticulos 1";
            DataTable mDtTable = clsDataBD.GetSql(myCadena);
            // Evito que el dgv genere columnas automáticas
            dgvArt.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvArt.DataSource = mDtTable;

        }

        #endregion

        private void txtCostoArt_TextChanged(object sender, EventArgs e)
        {
            //VALIDAR IVA Y REFRESCAR LISTA PRECIOS
            if (!(string.IsNullOrEmpty(txtPorcIVA.Text)))
            {
                //Cargar Datos Precios
                CargarVistaPrecios();
            }
        }
    }
}

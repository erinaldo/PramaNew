using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace Prama.Formularios.Articulos
{
    public partial class frmInsumos : Form
    {
        //Para capturar por donde ingresa al formulario (Insumo, Materia Prima, etc)
        int nTipArt = 0;
        string myEstado = "C";
        int indexFila = 0;
        int LastIns = 0;

        //Constructor 

        public frmInsumos(int nValue)
        {
            InitializeComponent();
            nTipArt = nValue;
        }

        private void frmInsumos_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            //Estado
            this.myEstado = "C";
            //Combos Rubro
            setCargarRubro();
            //Unidad de Medida
            setCargarUnidadMedida();
            //Unidad de Medida
            setCargarCoeficiente();
            //Cargar Grilla
            getCargarGrilla();
            // Llamo al método activar los botones del formulario. N.
            setActivarBotones();
            // Llamo al método habilitar controles del formulario. N.
            setHabilitarControles();
            // Cargar ToolTips
            setCargarToolTips();
            // Otros
            setOtrosItems();
            // Cuento la cantidad de filas de la grilla
            int filas = dgvArt.Rows.Count;
            // Actualizo el valor de la fila para que sea la última de la grilla
            this.indexFila = filas - 1;
            // Pongo el foco de la fila
            PosicionarFocoFila();
            //Titulo Ventana
            this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;
        }

        #region Método para activar los botones del formulario
        //--------------------------------------------------------------
        //ACTIVAR BOTONES  
        //SEGUN EL ESTADO (A, M, C) - MUESTRA U OCULTA BOTONES
        //--------------------------------------------------------------
        private void setActivarBotones()
        {
            switch (this.myEstado)
            {
                case "A":
                case "M":
                    this.btnAgregar.TabStop = false;
                    this.btnAgregar.Visible = false;
                    this.btnModificar.TabStop = false;
                    this.btnModificar.Visible = false;
                    this.btnBuscar.TabStop = false;
                    this.btnBuscar.Visible = false;
                    this.btnAceptar.TabStop = true;
                    this.btnAceptar.Visible = true;
                    this.btnCancelar.TabStop = true;
                    this.btnCancelar.Visible = true;
                    this.btnSalir.TabStop = false;
                    this.btnSalir.Visible = false;
                    this.btnBorrar.TabStop = false;
                    this.btnBorrar.Visible = false;
                    this.btnImprimir.TabStop = false;
                    this.btnImprimir.Visible = false;
                    return;
                case "B":
                    this.btnAgregar.TabStop = false;
                    this.btnAgregar.Visible = false;
                    this.btnModificar.TabStop = false;
                    this.btnModificar.Visible = false;
                    this.btnBuscar.TabStop = false;
                    this.btnBuscar.Visible = false;
                    this.btnAceptar.TabStop = true;
                    this.btnAceptar.Visible = true;
                    this.btnCancelar.TabStop = true;
                    this.btnCancelar.Visible = true;
                    this.btnSalir.TabStop = false;
                    this.btnSalir.Visible = false;
                    this.btnImprimir.TabStop = false;
                    this.btnImprimir.Visible = false;
                    this.btnBorrar.TabStop = false;
                    this.btnBorrar.Visible = false;
                    break;
                case "C":
                    this.btnAgregar.TabStop = true;
                    this.btnAgregar.Visible = true;
                    this.btnModificar.TabStop = true && (dgvArt.RowCount != 0);
                    this.btnModificar.Visible = true && (dgvArt.RowCount != 0);
                    this.btnBuscar.TabStop = true && (dgvArt.RowCount != 0);
                    this.btnBuscar.Visible = true && (dgvArt.RowCount != 0);
                    this.btnAceptar.TabStop = false;
                    this.btnAceptar.Visible = false;
                    this.btnCancelar.TabStop = false;
                    this.btnCancelar.Visible = false;
                    this.btnSalir.TabStop = true;
                    this.btnSalir.Visible = true;
                    // Si el nivel del usuario es el 5, habilito el botón borrar
                    if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja)
                    {
                        this.btnBorrar.TabStop = true && (dgvArt.RowCount != 0);
                        this.btnBorrar.Visible = true && (dgvArt.RowCount != 0);
                    }
                    else
                    {
                        this.btnBorrar.TabStop = false;
                        this.btnBorrar.Visible = false;
                    }
                    this.btnImprimir.TabStop = true && (dgvArt.RowCount != 0);
                    this.btnImprimir.Visible = true && (dgvArt.RowCount != 0);
                    return;
            }
        }
        #endregion

        #region Método para habilitar los Controles del formulario
        //--------------------------------------------------------------
        //SEGUN EL ESTADO (A, ALTA, M, MODIFICACION O C, EN ESPERA.
        //HABILITA O INHABILITA LOS CONTROLES DEL FORMULARIO
        //(PUEDE SER PROPIEDAD ENABLED (C# / VB) O READONLY (C#)
        //--------------------------------------------------------------
        private void setHabilitarControles()
        {
            //Usamos un switch para evaluar en que estado estamos 
            //A = Alta, M = Modificacion, "C" = En espera
            switch (this.myEstado)
            {
                case "A":
                case "M":
                    foreach (Control c in this.Controls)
                    {
                        //Textbox || CheckBox || DateTimePicker
                        if (c is TextBox || c is CheckBox || c is DateTimePicker)
                        {
                            c.TabStop = true;
                            c.Enabled = true;

                            //En Modificacion, CodArt no se puede cambiar
                            if (this.myEstado == "M" || this.myEstado == "A" && c.Name == "txtCodArt")
                            {
                                c.TabStop = false;
                                c.Enabled = false;
                            }

                            //Producto? tiene que detallar composicion
                            if (this.nTipArt == 2 && c.Name == "txtPrecio")
                            {
                                c.TabStop = false; c.Enabled = false;
                                btnPrecio.Visible = true;
                            }

                            //Lleva Stock? No?... deshabilito campos
                            if (chkLlevaStock.Checked == false)
                            {
                                switch (c.Name)
                                {
                                    case "txtStock":
                                    case "txtStockMin":
                                    case "txtStockMax":
                                    case "txtStockPed":
                                        c.TabStop = false;
                                        c.Enabled = false;
                                        break;
                                }
                            }
                        }

                        //CHECKBOX CASOS ESPECIALES
                        if (c is CheckBox)
                        {
                            if (nTipArt == 2)
                            {
                                if (c.Name == "chkListaGral" || c.Name == "chkListaRes")
                                {
                                    c.TabStop = true;
                                    c.Visible = true;
                                    c.Enabled = true;
                                }
                            }
                            if (nTipArt == 1)
                            {
                                if (c.Name == "chkListaGral" || c.Name == "chkListaRes")
                                {
                                    c.TabStop = false;
                                    c.Visible = false;
                                    c.Enabled = false;
                                }
                            }
                        }

                        //Buton
                        if (c is Button)
                        {
                            if (nTipArt == 2 && c.Name == "btnCoef")
                            {
                                c.TabStop = true;
                                c.Enabled = true;
                                c.Visible = true;
                            }
                            else if (nTipArt != 2 && c.Name == "btnCoef")
                            {
                                c.TabStop = false;
                                c.Enabled = false;
                                c.Visible = false;
                            }
                        }

                        //ComboBox       
                        if (c is ComboBox)
                        {
                            ComboBox cb = (ComboBox)c;
                            cb.TabStop = true;
                            cb.Enabled = true;
                            if (c.Name == "cboSubRubro")
                            {
                                cb.TabStop = true & (cboRubro.SelectedIndex > -1);
                                cb.Enabled = true & (cboRubro.SelectedIndex > -1);
                            }

                            //Coeficiente, solo si es producto
                            if (this.nTipArt == 2 && c.Name == "cboCoeficiente")
                            {
                                cb.TabStop = true;
                                cb.Enabled = true;
                            }

                            else if (this.nTipArt != 2 && c.Name == "cboCoeficiente")
                            {
                                cb.TabStop = false;
                                cb.Enabled = false;
                            }
                        }


                        this.dgvArt.TabStop = false;
                        this.dgvArt.Enabled = false;
                    }

                    //Habilitar busqueda
                    this.dgvArt.Size = new System.Drawing.Size(762, 280);
                    this.gpbBusquedas.Visible = false;

                    break;
                case "B":

                    foreach (Control c in this.Controls)
                    {
                        //Textbox || CheckBox
                        if (c is TextBox || c is CheckBox || c is DateTimePicker)
                        {
                            c.TabStop = false;
                            c.Enabled = false;
                        }
                        //ComboBox       
                        if (c is ComboBox)
                        {
                            ComboBox cb = (ComboBox)c;
                            cb.TabStop = false;
                            cb.Enabled = false;
                        }

                        //Buton
                        if (c is Button)
                        {
                            if (c.Name == "btnCoef")
                            {
                                c.TabStop = false;
                                c.Enabled = false;
                                c.Visible = false;
                            }
                        }

                        //CHECKBOX CASOS ESPECIALES
                        if (c is CheckBox)
                        {
                            if (nTipArt == 2)
                            {
                                if (c.Name == "chkListaGral" || c.Name == "chkListaRes")
                                {
                                    c.TabStop = false;
                                    c.Visible = false;
                                    c.Enabled = false;
                                }
                            }
                            if (nTipArt == 1)
                            {
                                if (c.Name == "chkListaGral" || c.Name == "chkListaRes")
                                {
                                    c.TabStop = false;
                                    c.Visible = false;
                                    c.Enabled = false;
                                }
                            }
                        }
                    }

                    //Habilitar busqueda

                    this.dgvArt.Size = new System.Drawing.Size(762, 390);
                    this.gpbBusquedas.Visible = true;

                    this.dgvArt.TabStop = true && (dgvArt.RowCount > 0);
                    this.dgvArt.Enabled = true && (dgvArt.RowCount > 0);
                    btnPrecio.Visible = false;

                    break;
                case "C":

                    foreach (Control c in this.Controls)
                    {
                        //Textbox || CheckBox
                        if (c is TextBox || c is CheckBox || c is DateTimePicker)
                        {
                            c.TabStop = false;
                            c.Enabled = false;
                        }
                        //ComboBox       
                        if (c is ComboBox)
                        {
                            ComboBox cb = (ComboBox)c;
                            cb.TabStop = false;
                            cb.Enabled = false;
                        }

                        //Buton
                        if (c is Button)
                        {
                            if (c.Name == "btnCoef")
                            {
                                c.TabStop = false;
                                c.Enabled = false;
                                c.Visible = false;
                            }
                        }

                        //CHECKBOX CASOS ESPECIALES
                        if (c is CheckBox)
                        {
                            if (nTipArt == 2)
                            {
                                if (c.Name == "chkListaGral" || c.Name == "chkListaRes")
                                {
                                    c.TabStop = false;
                                    c.Visible = true;
                                    c.Enabled = false;
                                }
                            }
                            if (nTipArt == 1)
                            {
                                if (c.Name == "chkListaGral" || c.Name == "chkListaRes")
                                {
                                    c.TabStop = false;
                                    c.Visible = false;
                                    c.Enabled = false;
                                }
                            }
                        }

                    }
                    this.dgvArt.Size = new System.Drawing.Size(762, 280);
                    this.gpbBusquedas.Visible = false;

                    this.dgvArt.TabStop = true && (dgvArt.RowCount > 0);
                    this.dgvArt.Enabled = true && (dgvArt.RowCount > 0);
                    btnPrecio.Visible = false;

                    break;
            }
        }
        #endregion

        #region Método para limpiar los controles del formulario
        //LIMPIA LA PROPIEDAD TEXT DE LOS CONTROLES DEL FORMULARIO. N.
        private void setLimpiarControlesForm()
        {

            foreach (Control c in this.Controls)
            {

                //Textbox  
                if (c is TextBox) { c.Text = ""; }
                //CheckBox
                if (c is CheckBox)
                {
                    CheckBox cb = (CheckBox)c;
                    cb.Checked = false;
                }

                //ComboBox       
                if (c is ComboBox)
                {
                    ComboBox cb = (ComboBox)c;
                    if (cb != null)
                    {
                        cb.SelectedIndex = -1;
                    }
                }

                //DateTimePicker
                if (c is DateTimePicker)
                {
                    DateTimePicker dtp = (DateTimePicker)c;
                    if (dtp != null)
                    {
                        dtp.Text = "1950/01/01";
                    }
                }

            }

            foreach (Control c in this.gpbBusquedas.Controls)
            {

                //Textbox  
                if (c is TextBox) { c.Text = ""; }
                //CheckBox
                if (c is CheckBox)
                {
                    CheckBox cb = (CheckBox)c;
                    cb.Checked = false;
                }
            }

        }
        #endregion

        #region Método para cargar Rubro

        private void setCargarRubro()
        {
            // Cargo el combo de las provincias
            clsDataBD.CargarCombo(cboRubro, "RubrosArticulos", "RubroArticulo", "IdRubroArticulo");
            // Dejo vacío el combo
            cboRubro.SelectedIndex = -1;

        }
        #endregion

        #region Método para cargar SubRubro

        private void setCargarSubRubro()
        {
            // Cargo el combo de Condicion IVA
            clsDataBD.CargarCombo(cboSubRubro, "SubrubrosArticulos", "SubrubroArticulo", "IdSubrubroArticulo");
            // Dejo vacío el combo
            cboSubRubro.SelectedIndex = -1;

        }
        #endregion

        #region Método para cargar Unidad de Medida

        private void setCargarUnidadMedida()
        {
            // Cargo el combo de Condicion Compra
            clsDataBD.CargarCombo(cboUMedida, "UnidadesMedida", "UnidadMedida", "IdUnidadMedida");
            // Dejo vacío el combo
            cboUMedida.SelectedIndex = -1;

        }
        #endregion

        #region Método para cargar Coeficientes

        private void setCargarCoeficiente()
        {
            // Cargo el combo de Coeficiente
            clsDataBD.CargarCombo(cboCoeficiente, "CoeficientesArticulos", "CoeficienteArticulo", "IdCoeficienteArticulo");
            // Dejo vacío el combo
            cboCoeficiente.SelectedIndex = -1;

        }
        #endregion

        #region Método para cargar la grilla

        private void getCargarGrilla()
        {
            // Cadena SQL 
            string myCadena = "exec CargarArticulos " + nTipArt;
            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            mDtTable = clsDataBD.GetSql(myCadena);
            // Evito que el dgv genere columnas automáticas
            dgvArt.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvArt.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvArt.Rows.Count;
            // Posiciono la grilla en la última fila, si hay
            if (Filas > 0)
            {
                //Posicionar Grilla       
                int r = dgvArt.CurrentCell.RowIndex;
                int c = dgvArt.CurrentCell.ColumnIndex;
                dgvArt.CurrentCell = dgvArt.Rows[r].Cells[c];
                //Mostrar datos
                getMostrarDatos();
            }
        }

        #endregion

        #region Metodo: setCargarToolTips

        private void setCargarToolTips()
        {
            toolTip1.SetToolTip(this.btnAgregar, "Agregar");
            toolTip2.SetToolTip(this.btnModificar, "Modificar");
            toolTip3.SetToolTip(this.btnBorrar, "Borrar");
            toolTip4.SetToolTip(this.btnImprimir, "Imprimir");
            toolTip5.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip6.SetToolTip(this.btnCancelar, "Cancelar");
            toolTip7.SetToolTip(this.btnSalir, "Salir");
            toolTip8.SetToolTip(this.btnBuscar, "Buscar");
            toolTip9.SetToolTip(this.btnCoef, "Ver Coeficientes");
            toolTip10.SetToolTip(this.btnPrecio, "Composición Precio");
        }

        #endregion

        #region Metodo: setOtrosItems

        /**************************************************************************
         * Metodo   : setOtrosItems
         * Proposito: establecer valores a objetos en virtud del tipo de articulo
         * Retorna  : Nada
         * Autor    : N.
         * Fecha    : 25-09-2016
         *************************************************************************/
        private void setOtrosItems()
        {
            switch (this.nTipArt)
            {
                case 1: //Insumos / Ingredientes
                    this.lblPrecio.Text = "Costo:";
                    this.Text = " - INSUMOS ";
                    this.dgvArt.Columns[7].Visible = false;
                    this.dgvArt.Columns[8].Visible = true;
                    break;
                case 2: //Precio - Productos
                    this.lblPrecio.Text = "Precio:";
                    this.Text = " - PRODUCTOS ";
                    this.dgvArt.Columns[7].Visible = true;
                    this.dgvArt.Columns[8].Visible = false;
                    break;
            }
        }

        #endregion

        #region Metodo: VaciarVectorGlobales

        private void VaciarVectorGlobales()
        {
            // Vacío de datos el vector de datos composicion
            clsGlobales.ProductoDatos = (double[])clsValida.ResizeVector(clsGlobales.ProductoDatos, 0);
            clsGlobales.ProductoDatosST = (string[])clsValida.ResizeVector(clsGlobales.ProductoDatosST, 0);

            // Vacío de datos el vector de detalle la composicion
            clsGlobales.ProductoComposicion = (string[,])clsValida.ResizeMatriz(clsGlobales.ProductoComposicion, new int[] { 0, 9 });

            // Vecio de datos del vector de productos eliminados
            clsGlobales.ProductosEliminados = (string[,])clsValida.ResizeMatriz(clsGlobales.ProductosEliminados, new int[] { 0, 2 });
        }

        #endregion

        #region Metodo: CargarGrillaBusqueda

        private void CargarGrillaBusqueda(string Buscar, string Campo)
        {
            // Variable para la cadena SQL
            string myCadena = "";
            if (Buscar == "")
            {
                // Cadena SQL 
                myCadena = "exec CargarArticulos " + nTipArt;
            }
            else
            {
                if (nTipArt == 1)
                {
                    myCadena = "Select * from BuscarArticulos_Insumos Where " + Campo + " like '" + Buscar + "%' AND Activo = 1";
                }
                else
                {
                    myCadena = "Select * from BuscarArticulos_Productos Where " + Campo + " like '" + Buscar + "%' AND Activo = 1";
                }
            }

            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            mDtTable = clsDataBD.GetSql(myCadena);
            // Evito que el dgv genere columnas automáticas
            dgvArt.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvArt.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvArt.Rows.Count;
            // Posiciono la grilla en la última fila, si hay
            if (Filas > 0)
            {
                //Posicionamiento grilla
                int r = dgvArt.CurrentCell.RowIndex;
                int c = dgvArt.CurrentCell.ColumnIndex;
                dgvArt.CurrentCell = dgvArt.Rows[r].Cells[c];

                // Tomo la posición actual de la fila con foco
                this.indexFila = dgvArt.CurrentRow.Index;
            }

            //Mostrar datos  
            this.getMostrarDatos();
        }

        #endregion

        #region Metodo: setComboSubRubro

        private void setComboSubRubro()
        {

            //Clean Combobox
            cboSubRubro.DataSource = null;
            cboSubRubro.DataBindings.Clear();

            // Cargo el combo de las Localidades N.
            string strSQL = " IdRubroArticulo = " + cboRubro.SelectedValue;
            clsDataBD.CargarCombo(cboSubRubro, "SubrubrosArticulos", "SubrubroArticulo", "IdSubrubroArticulo", strSQL);

            //Establecer el valor                       
            DataGridViewRow row = dgvArt.CurrentRow;
            cboSubRubro.SelectedValue = Convert.ToInt32(row.Cells["IdSubrubroArticulo"].Value.ToString());

        }

        #endregion

        #region Metodo: getMostrarDatos()

        //************************************************************
        //Metodo    : getMostrarDatos 
        //Fecha     : 24-09-2016
        //Autor     : N
        //Proposito : Mostrar los datos de la grilla en los controles
        //************************************************************
        private void getMostrarDatos()
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvArt.RowCount == 0)
            {
                return;
            }
            else
            {
                //TRAER LA FILA ACTUAL. N.
                DataGridViewRow row = dgvArt.CurrentRow;

                txtCodArt.Text = row.Cells["CodigoArticulo"].Value.ToString();

                txtArt.Text = row.Cells["Articulo"].Value.ToString();

                cboRubro.SelectedValue = Convert.ToInt32(row.Cells["IdRubroArticulo"].Value.ToString());

                //Si cargo un Rubro
                if (Convert.ToInt32(cboRubro.SelectedValue) > 0)
                {
                    setComboSubRubro();
                }

                cboUMedida.SelectedValue = Convert.ToInt32(row.Cells["IdUnidadMedida"].Value.ToString());

                txtUnidad.Text = row.Cells["Unidades"].Value.ToString();

                //Evaluar precio o costo
                if (nTipArt == 2)
                {
                    txtPrecio.Text = row.Cells["Precio"].Value.ToString();
                }
                else if (nTipArt == 1)
                {
                    txtPrecio.Text = row.Cells["Costo"].Value.ToString();
                }

                txtUltCosto.Text = row.Cells["UltimoCostoCompra"].Value.ToString();

                txtProv.Text = row.Cells["UltimoProveedor"].Value.ToString();

                txtStock.Text = row.Cells["Stock"].Value.ToString();

                txtStockMin.Text = row.Cells["StockMinimo"].Value.ToString();

                txtStockMax.Text = row.Cells["StockMaximo"].Value.ToString();

                txtStockPed.Text = row.Cells["StockPuntoPedido"].Value.ToString();

                txtPorcIVA.Text = row.Cells["PorcentajeIVA"].Value.ToString();

                chkLlevaStock.Checked = (bool)row.Cells["LlevaStock"].Value;

                chkFacturable.Checked = (bool)row.Cells["Facturable"].Value;

                chkListaGral.Checked = Convert.ToBoolean(row.Cells["IncListaPre"].Value);

                chkListaRes.Checked = Convert.ToBoolean(row.Cells["IncListaRes"].Value);

                if (nTipArt == 2) //Productos
                {
                    cboCoeficiente.SelectedValue = Convert.ToInt32(row.Cells["IdCoeficienteArticulo"].Value.ToString());
                }

                if (!(row.Cells["UltimaCompra"].Value.ToString() == "0"))
                {

                    dtUltCompra.Value = Convert.ToDateTime(row.Cells["UltimaCompra"].Value);
                }

                txtPrecioAnterior.Text = row.Cells["PrecioAnterior"].Value.ToString();
            }
        }

        #endregion

        #region Eventos KeyPress

        #region Evento KeyPress txtUnidad

        private void txtUnidad_KeyPress(object sender, KeyPressEventArgs e)
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

        #region Evento KeyPress txtPrecio
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
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
                && splitByDecimal[1].Length == 5)
            {
                e.Handled = true;
            }

        }
        #endregion

        #region Evento Keypress txtUltCosto

        private void txtUltCosto_KeyPress(object sender, KeyPressEventArgs e)
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
                && splitByDecimal[1].Length == 5)
            {
                e.Handled = true;
            }

        }
        #endregion

        #region Evento Keypress txtStock
        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 44)
            {
                e.KeyChar = Convert.ToChar(46);
                ch = e.KeyChar;

            }
            //PUNTO DECIMAL. N.
            if (ch == 46 && txtStock.Text.IndexOf('.') != -1)
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
            if (!char.IsControl(e.KeyChar))
            {
                if (txtStock.Text.IndexOf('.') > -1 &&
                    txtStock.Text.Substring(txtStock.Text.IndexOf('.')).Length >= (2 + 1))
                {
                    e.Handled = true;
                }
            }
        }
        #endregion

        #region Evento Keypress txtStockMin
        private void txtStockMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 44)
            {
                e.KeyChar = Convert.ToChar(46);
                ch = e.KeyChar;

            }
            //PUNTO DECIMAL. N.
            if (ch == 46 && txtStockMin.Text.IndexOf('.') != -1)
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
            if (!char.IsControl(e.KeyChar))
            {
                if (txtStockMin.Text.IndexOf('.') > -1 &&
                    txtStockMin.Text.Substring(txtStockMin.Text.IndexOf('.')).Length >= (2 + 1))
                {
                    e.Handled = true;
                }
            }
        }

        #endregion

        #region Evento Keypress txtStockMax
        private void txtStockMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 44)
            {
                e.KeyChar = Convert.ToChar(46);
                ch = e.KeyChar;

            }
            //PUNTO DECIMAL. N.
            if (ch == 46 && txtStockMax.Text.IndexOf('.') != -1)
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
            if (!char.IsControl(e.KeyChar))
            {
                if (txtStockMax.Text.IndexOf('.') > -1 &&
                    txtStockMax.Text.Substring(txtStockMax.Text.IndexOf('.')).Length >= (2 + 1))
                {
                    e.Handled = true;
                }
            }
        }
        #endregion

        #region Evento Keypress txtStockPed
        private void txtStockPed_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 44)
            {
                e.KeyChar = Convert.ToChar(46);
                ch = e.KeyChar;

            }
            //PUNTO DECIMAL. N.
            if (ch == 46 && txtStockPed.Text.IndexOf('.') != -1)
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
            if (!char.IsControl(e.KeyChar))
            {
                if (txtStockPed.Text.IndexOf('.') > -1 &&
                    txtStockPed.Text.Substring(txtStockPed.Text.IndexOf('.')).Length >= (2 + 1))
                {
                    e.Handled = true;
                }
            }
        }
        #endregion

        #region Evento Kepress txtPorcIVA
        private void txtPorcIVA_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 44)
            {
                e.KeyChar = Convert.ToChar(46);
                ch = e.KeyChar;

            }
            //PUNTO DECIMAL. N.
            if (ch == 46 && txtPorcIVA.Text.IndexOf('.') != -1)
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
            if (!char.IsControl(e.KeyChar))
            {
                if (txtPorcIVA.Text.IndexOf('.') > -1 &&
                    txtPorcIVA.Text.Substring(txtPorcIVA.Text.IndexOf('.')).Length >= (2 + 1))
                {
                    e.Handled = true;
                }
            }
        }
        #endregion

        #endregion

        #region Eventos de la Grilla

        #region Evento KeyDown de la Grilla de Proveedores
        private void dgvArt_KeyDown(object sender, KeyEventArgs e)
        {
            //PRESIONO DEL?
            if (e.KeyCode == Keys.Delete)
            {
                this.btnBorrar.PerformClick(); //LLAMAR A EVENTO CLICK DEL BOTON BORRAR
            }
        }

        #endregion

        #region Evento CellContentClick de la grilla

        private void dgvArt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvArt.RowCount == 0)
            {
                return;
            }
            else
            {
                //Mostrar los datos
                getMostrarDatos();

            }
        }

        #endregion

        #region Evento SelectionChanged de la Grilla de Articulos

        private void dgvArt_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArt.RowCount == 0)
            {
                return;
            }
            else
            {

                //Mostrar los datos
                getMostrarDatos();

           /*     if (this.nTipArt == 2)
                {
                    //Vaciar Vectores Gloables
                    VaciarVectorGlobales();

                    //Composicion
                    getComposicionProducto();
                }*/
            }
        }

        #endregion

        #endregion

        #region Evento SelectedValueChanged cboRubro

        private void cboRubro_SelectedValueChanged(object sender, EventArgs e)
        {
            //Solo si estoy editando o modificando N.
            if (!(this.myEstado == "C"))
            {
                if (!(Convert.ToInt32(cboRubro.SelectedValue) > 0)) { return; }

                //Clean Combobox
                cboSubRubro.DataSource = null;
                cboSubRubro.DataBindings.Clear();

                // Cargo el combo de las Localidades N.
                string strSQL = " IdRubroArticulo = " + cboRubro.SelectedValue;
                clsDataBD.CargarCombo(cboSubRubro, "SubrubrosArticulos", "SubrubroArticulo", "IdSubrubroArticulo", strSQL);

                // Establezco el combo localidad a la primera opcion N.
                cboSubRubro.TabStop = true;
                cboSubRubro.Enabled = true;
                //No mostrar dato alguno
                cboSubRubro.SelectedIndex = -1;
            }
            else
            {

                //Clean Combobox
                cboSubRubro.DataSource = null;
                cboSubRubro.DataBindings.Clear();
                //Desactivar
                cboSubRubro.TabStop = false;
                cboSubRubro.Enabled = false;
                //No mostrar dato alguno
                cboSubRubro.SelectedIndex = -1;
            }

        }

        #endregion

        #region Botones

        #region Boton Agregar

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {


            //Precio Anterio Vacio
            txtPrecioAnterior.Text = "0";

            // Pregunto si el usuario actual tiene nivel mayor a 2, lo dejo agregar. N.
            if (clsGlobales.UsuarioLogueado.Nivel > 2)
            {
                // Tomo la posición actual de la fila con foco
                this.indexFila = dgvArt.CurrentRow.Index;

                // Cambio el estado del formulario a agregar. N.
                this.myEstado = "A";
                // Limpio los controles del formulario. N.    
                setLimpiarControlesForm();
                // Activo los botones para este estado. N.
                setActivarBotones();
                // Habilito los controles para este estado. N.
                setHabilitarControles();
                // Habilito lupa composicion precio
                if (this.nTipArt == 2)
                {
                    btnPrecio.Visible = true;
                }
                //Cargar la configuracion de porcentaje de Iva
                txtPorcIVA.Text = clsGlobales.cParametro.Iva.ToString("#0.00");

                //Alta? hay datos? limpiar vectores
                if (dgvArt.Rows.Count > 0 && this.nTipArt == 2)
                {
                    this.VaciarVectorGlobales();
                }

                //Codigo Automatico
                if (this.nTipArt == 1)
                {
                    LastIns = clsDataBD.getUltComp("Ult_Ins", clsGlobales.cParametro.PtoVtaPorDefecto, 0);
                    string newIns = "INS";

                    if (LastIns == 0)
                    {
                        LastIns = (clsGlobales.cParametro.UltIns + 1);
                        //Nuevo Insumo
                        newIns = newIns + (clsGlobales.cParametro.UltIns+1).ToString("D5");
                    }
                    else
                    {
                        //Suma 1 y arma el codeigo
                        LastIns += 1;
                        //Nuevo Codigo
                        newIns = newIns + LastIns.ToString("D5");
                    }

                    //Asignar
                    txtCodArt.Text = newIns;
                }

                // Posiciono el foco sobre el primer textbox      
                txtArt.Focus();
            }
            // Pregunto si el usuario actual tiene nivel menor o igual a 2, No lo dejo agregar. G.
            else
            {
                // vuelvo el formulario al estado anterior. G.
                this.myEstado = "C";
                // El usuario no es de nivel 3, por lo que no puede crear un nuevo usuario. N.
                MessageBox.Show("Usted no tiene los permisos para dar de Alta un Artículo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Boton Modificar

        private void btnModificar_Click(object sender, EventArgs e)
        {

            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvArt.CurrentRow;

            // // Pregunto si el usuario actual tiene nivel mayor a 2, lo dejo agregar. N.
            if (clsGlobales.UsuarioLogueado.Nivel > 2)
            {
                // Tomo la posición actual de la fila con foco
                this.indexFila = dgvArt.CurrentRow.Index;

                // Cambio el estado del formulario a Modificar. N.
                this.myEstado = "M";
                // Activo los botones para este estado. N.
                setActivarBotones();
                // Habilito los controles para este estado. N.
                setHabilitarControles();
                //Combo SubRubro
                setComboSubRubro();
                // Posiciono el foco sobre el primer textbox
                txtArt.Focus();
            }
            else
            {
                // Pregunto si el usuario actual tiene nivel menor o igual a 2, No lo dejo agregar. N.
                this.myEstado = "C";
                // El usuario no es de nivel 3, por lo que no puede crear un nuevo usuario. N.
                MessageBox.Show("Usted no tiene los permisos para Modificar un Articulo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. N.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Boton Salir

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //Vaciar Vectores
            //VaciarVectorGlobales();

            //Cerrar
            this.Close();
        }

        #endregion

        #region Boton Cancelar

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Cambio el estado del formulario a agregar. N.
            this.myEstado = "C";
            // Limpio los controles del formulario. N.    
            setLimpiarControlesForm();
            // Activar o desactivar botones N
            setActivarBotones();
            // Habilito los controles para este estado. N.
            setHabilitarControles();
            // Inhabilito lupa composicion precio
            if (this.nTipArt == 2)
            {
                btnPrecio.Visible = false;
            }
            //Bandera a false
            clsGlobales.bCambio = false;
            //Cargar Grilla
            getCargarGrilla();
            //Reposicionar grilla 
            PosicionarFocoFila();
            //Llamo al evento SelectionChanged de la grilla. N.
            //this.dgvArt_SelectionChanged(sender, e);

        }

        #endregion

        #region Boton Aceptar

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            //Busqueda Activa?...
            if (this.myEstado == "B")
            {
                // Cambio mi estado
                this.myEstado = "C";
                // Activo todos los botones
                setActivarBotones();
                // Habilito los controles
                setHabilitarControles();
                //Boton Cancelar Visible
                this.btnCancelar.Visible = true;
                // Pongo el foco en la fila desde donde se hizo la llamada
                PosicionarFocoFila();
                //Retornar
                return;
            }


            // Creo nuevo Articulo . N.
            clsArticulos NuevoArticulo = new clsArticulos();
            // Tomo la línea actual de la grilla. N.
            DataGridViewRow row = dgvArt.CurrentRow;
            // Verifico el estado del formulario para saber si estoy creando o modificando. N.
            // Paso los datos del formulario al nuevo Articulo
            if (dgvArt.Rows.Count > 0)
            {
                NuevoArticulo.IdArticulo = Convert.ToInt32(row.Cells["IdArticulo"].Value);
            }
            NuevoArticulo.IdSubrubroArticulo = Convert.ToInt32(cboSubRubro.SelectedValue);
            NuevoArticulo.IdUnidadMedida = Convert.ToInt32(cboUMedida.SelectedValue);
            NuevoArticulo.CodigoArticulo = txtCodArt.Text;
            NuevoArticulo.Articulo = txtArt.Text;
            NuevoArticulo.LlevaStock = Convert.ToInt32(chkLlevaStock.Checked);
            NuevoArticulo.Facturable = Convert.ToInt32(chkFacturable.Checked);

            if (chkListaGral.Checked)
            { NuevoArticulo.incListaPre = 1; }
            else { NuevoArticulo.incListaPre = 0; }

            if (chkListaRes.Checked)
            { NuevoArticulo.incListaRes = 1; }
            else { NuevoArticulo.incListaRes = 0; }

            NuevoArticulo.chkSombreado = 0;
            NuevoArticulo.rbtColor = 0;             
            NuevoArticulo.CompIns = 0;
            NuevoArticulo.chkSProd = 0;

            if (txtPrecio.Text == "")
            {
                NuevoArticulo.Precio = 0;
            }
            else
            {
                NuevoArticulo.Precio = Convert.ToDouble(txtPrecio.Text);
            }

            if (txtPorcIVA.Text == "")
            {
                NuevoArticulo.PorcentajeIva = 0;
            }
            else { NuevoArticulo.PorcentajeIva = Convert.ToDouble(txtPorcIVA.Text); }

            if (txtStock.Text == "") { NuevoArticulo.Stock = 0; }
            else
            {
                NuevoArticulo.Stock = Convert.ToDouble(txtStock.Text);
            }

            if (txtStockMin.Text == "") { NuevoArticulo.StockMinimo = 0; }
            else
            {
                NuevoArticulo.StockMinimo = Convert.ToDouble(txtStockMin.Text);
            }

            if (txtStockMax.Text == "") { NuevoArticulo.StockMaximo = 0; }
            else
            {
                NuevoArticulo.StockMaximo = Convert.ToDouble(txtStockMax.Text);
            }

            if (txtStockPed.Text == "") { NuevoArticulo.StockPuntoPedido = 0; }
            else
            {
                NuevoArticulo.StockPuntoPedido = Convert.ToDouble(txtStockPed.Text);
            }

            if (txtUltCosto.Text == "") { NuevoArticulo.UltimoCostoCompra = 0; }
            else
            {
                NuevoArticulo.UltimoCostoCompra = Convert.ToDouble(txtUltCosto.Text);
            }

            NuevoArticulo.UltimaCompra = dtUltCompra.Text;
            NuevoArticulo.UltimoProveedor = txtProv.Text;

            if (txtUnidad.Text == "") { NuevoArticulo.Unidades = 0; }
            else
            {
                NuevoArticulo.Unidades = Convert.ToDouble(txtUnidad.Text);
            }

            NuevoArticulo.Activo = 1;
            NuevoArticulo.UltimaCompra = this.dtUltCompra.Value.ToShortDateString();

            if (txtPrecioAnterior.Text == "")
            {
                txtPrecioAnterior.Text = "0";
            }
 
            NuevoArticulo.PrecioAnterior = Convert.ToDouble(txtPrecioAnterior.Text);

            //Vector Errores
            string[] cErrores = NuevoArticulo.cValidaArticulo();

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

            //Coeficiente para producto esta ok?
            if (nTipArt == 2)
            {
                if (Convert.ToInt32(cboCoeficiente.SelectedValue) == 0)
                {
                    MessageBox.Show("Debe completar el campo 'Coeficiente'!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cboCoeficiente.Focus();
                    return;
                }

            }

            //Si esta modificando....
            if (this.myEstado == "M")
            {

                //Verificar Tipo (Insumo / Producto)
                switch (nTipArt)
                {
                    case 1: //Insumo

                        this.ModificaArticuloInsumo(NuevoArticulo);
                        break;

           /*         case 2: //Producto

                        //Update del Articulo
                        this.ModificaArticuloProducto(NuevoArticulo);

                        //Update de Producto                                             
                        this.UpdateProducto();

                        //Udate Composicion de Producto (ProductoInsumo y ProductoGastosFijos)
                        this.UpdateComposicionProducto();

                        //Vaciar Vectores Globales
                        VaciarVectorGlobales();

                        //Fikn
                        break;*/
                }


            }
            else if (this.myEstado == "A") //ALTA
            {
                //Guardar Nuevo Articulo
                GuardarNuevoArticulo(NuevoArticulo);

                switch (nTipArt)
                {
                    case 1: //Insumo

                        //Update Codigo Automatico
                        string mySQL = "UPDATE PuntosVentaAFIP SET Ult_Ins = " + LastIns + " WHERE Punto = " + clsGlobales.cParametro.PtoVtaPorDefecto;
                        clsDataBD.GetSql(mySQL);

                        GuardarNuevoInsumo(NuevoArticulo);
                        break;

                /*    case 2: //Producto

                        //Guardar Producto
                        this.GuardarNuevoProducto();

                        //Guardar Composicion de Producto (ProductoInsumo y ProductoGastosFijos)
                        this.GuardarComposicionProducto();

                        //Vaciar Vectores Globales
                        this.VaciarVectorGlobales();

                        //Fin
                        break;*/
                }

            }

            //Bandera a false
            clsGlobales.bCambio = false;

            //Lleno nuevamente la grilla
            getCargarGrilla();

            //Regreso el formulario a su estado inicial
            this.btnCancelar.PerformClick();

        }

        #endregion

        #region Boton Borrar

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            // Pregunto si el usuario actual es el mismo que se quiere modificar. G.
            if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja)
            {
                // Tomo la posición actual de la fila con foco
                this.indexFila = dgvArt.CurrentRow.Index;

                //TRAER LA FILA ACTUAL. N.
                DataGridViewRow row = dgvArt.CurrentRow;
                // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
                int Id = Convert.ToInt32(row.Cells["IdArticulo"].Value);
                string strArt = row.Cells["Articulo"].Value.ToString();
                int Niv = clsGlobales.UsuarioLogueado.Nivel;
                string sTipo = "";

                //Definir que elimino
                sTipo = "INSUMO";
                if (nTipArt == 2)
                {
                    sTipo = "PRODUCTO";
                }

                //Verficar nivel de usuario
               if (Niv < clsGlobales.cParametro.NivelBaja)
                {
                    // El usuario no es de nivel 5, por lo que no puede crear un nuevo usuario. G.
                    MessageBox.Show("Usted no tiene los permisos para Eliminar este Artículo!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Ejecuto el botón cancelar. G.
                    this.btnCancelar.PerformClick();
                }
                else
                {
                    // Confirma eliminacion? cambio de estado Activo = .F.
                    DialogResult dlResult = MessageBox.Show("Desea Eliminar el " + sTipo + ": " + strArt + " ?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    // Si confirma... cambiar estado
                    if (dlResult == DialogResult.Yes)
                    {
                        //Previo validar....
                        bool bValida = cValidaBaja();
                        if (bValida)
                        {
                            string myCadena = "UPDATE Articulos SET Activo = 0 WHERE IdArticulo =" + Id;
                            clsDataBD.GetSql(myCadena);
                        }
                        else
                        {
                            MessageBox.Show("No se puede eliminar el " + sTipo + ": " + strArt + ", porque se encuentra referenciado en otras tablas del Sistema!", "Informaciòn!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        //Refresco la grilla
                        getCargarGrilla();
                    }
                }

            }
            else
            {
                // El usuario no es de nivel 5, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para Eliminar un Insumo/Producto", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #endregion

        #region Eventos Click

        #region Evento Click boton Precio

        private void btnPrecio_Click(object sender, EventArgs e)
        {
            //Todo Ok, llamo a Composicion

            if (this.myEstado == "A")
            {
                frmArticulosComposicion myForm = new frmArticulosComposicion(this.myEstado, 0, clsGlobales.cParametro.Iva);
                myForm.ShowDialog();
            }
            else if (this.myEstado == "M")
            {
                frmArticulosComposicion myForm = new frmArticulosComposicion(this.myEstado, Convert.ToInt32(dgvArt.CurrentRow.Cells["IdArticulo"].Value), Convert.ToDouble(txtPorcIVA.Text));
                myForm.ShowDialog();
            }

            //Precio
            if (clsGlobales.ProductoDatos.Length > 0 && clsGlobales.ProductoDatos[4] != 0)
            {
                this.txtPrecio.Text = clsGlobales.ProductoDatos[4].ToString("#0.00000");
            }
        }

        #endregion

        #region Evento Click Boton Coeficiente

        private void btnCoef_Click(object sender, EventArgs e)
        {
            frmArticulosCoeficientes myForm = new frmArticulosCoeficientes(1);
            myForm.ShowDialog();
        }

        #endregion

        #region Evento Click BtnBuscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Tomo la posición actual de la fila con foco
            this.indexFila = dgvArt.CurrentRow.Index;

            //Preparo todo para habilitar la busqueda
            this.myEstado = "B";
            this.setActivarBotones();
            this.setHabilitarControles();
            this.setLimpiarControlesForm();
            this.txtCodigo.Focus();
        }

        #endregion

        #region Evento btnImprimir Click

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //Data Set
            dsReportes oDsArt = new dsReportes();
            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = dgvArt.Rows.Count;

            string sLleva = "";
            string sFactura = "";

            for (int i = 0; i < dgvFilas; i++)
            {

                if (Convert.ToInt32(dgvArt[12, i].Value) == 1)
                { sLleva = "Sí"; }
                else { sLleva = "No"; }

                if (Convert.ToInt32(dgvArt[13, i].Value) == 1)
                { sFactura = "Sí"; }
                else { sFactura = "No"; }

                oDsArt.Tables["DtArt"].Rows.Add
                (new object[] { dgvArt[4,i].Value.ToString(),
                dgvArt[5,i].Value.ToString(),
                dgvArt[6,i].Value.ToString(),
                dgvArt[7,i].Value.ToString(),
                sLleva,
                sFactura,
                dgvArt[14,i].Value.ToString(),
                });

            }

            //Objeto Reporte
            rptArticulos oRepArt = new rptArticulos();
            //Cargar Reporte            
            //    oRepTipoCli.Load(Application.StartupPath + "\\rptTipoCli.rpt");
            oRepArt.Load(Application.StartupPath + "\\rptArticulos.rpt");
            //Establecer el DataSet como DataSource
            oRepArt.SetDataSource(oDsArt);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepArt;
            if (nTipArt == 1)
            {
                oRepArt.DataDefinition.FormulaFields["InsProd"].Text = "'" + " - INSUMOS / INGREDIENTES" + "'";
            }
            else
            {
                oRepArt.DataDefinition.FormulaFields["InsProd"].Text = "'" + " - PRODUCTOS" + "'";
            }

            oRepArt.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepArt.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
            oRepArt.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
            oRepArt.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
            oRepArt.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
            oRepArt.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepArt.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
            oRepArt.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();
        }

        #endregion

        #endregion

        #region Método que devuelve el foco a la fila desde donde se presionó algún botón

        private void PosicionarFocoFila()
        {
            if (dgvArt.Rows.Count != 0 && dgvArt.Rows.Count > this.indexFila)
            {
                // Devuelvo el foco a la fila de la grilla desde donde se llamó
                this.dgvArt.CurrentCell = dgvArt[4, this.indexFila];

                // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                EventArgs ea = new EventArgs();
                this.dgvArt_SelectionChanged(this.dgvArt, ea);
            }
        }

        #endregion

        #region METODOS

        #region Metodo: cValidaBaja

        private bool cValidaBaja()
        {
            string myCadena = "";

            //DataTable's
            DataTable myDataComposicion = new DataTable();
            DataTable myDataCompra = new DataTable();

            DataTable myDataPed = new DataTable();
            DataTable myDataPresu = new DataTable();
            DataTable myDataFactu = new DataTable();

            int myValor = 0;

            DataGridViewRow row = dgvArt.CurrentRow; //Para Datatable's

            bool cBaja = true; // .T.

            //Insumos
            if (nTipArt == 1)
            {
                //Ver sino esta en composicion
                myCadena = "Select count(*) as Elementos from ProductosInsumos where IdInsumo = " + Convert.ToInt32(row.Cells["IdArticulo"].Value);
                //Armar la consulta
                myDataComposicion = clsDataBD.GetSql(myCadena);
                //Ver el valor
                foreach (DataRow rows in myDataComposicion.Rows)
                {
                    if (rows["Elementos"] == DBNull.Value) { myValor = 0; }
                    else
                    {
                        myValor = Convert.ToInt32(rows["Elementos"]);
                        if (myValor > 0)
                        {
                            cBaja = false;
                            return cBaja;
                        }
                    }
                }

                //Ver si el Insumo no esta en compras
                myCadena = "Select count(*) as Elementos from DetallesComprobantesCompras where IdArticulo = " + Convert.ToInt32(row.Cells["IdArticulo"].Value);
                //Armar la consulta
                myDataCompra = clsDataBD.GetSql(myCadena);
                //Ver el valor
                foreach (DataRow rows in myDataCompra.Rows)
                {
                    if (rows["Elementos"] == DBNull.Value) { myValor = 0; }
                    else
                    {
                        myValor = Convert.ToInt32(rows["Elementos"]);
                        if (myValor > 0)
                        {
                            cBaja = false;
                            return cBaja;
                        }
                    }
                }

            }

            if (nTipArt == 2) //Productos
            {
                //Ver en Pedidos
                myCadena = "Select count(*) as Elementos from DetallePedidos where IdArticulo = " + Convert.ToInt32(row.Cells["IdArticulo"].Value);
                //Armar la consulta
                myDataComposicion = clsDataBD.GetSql(myCadena);
                //Ver el valor
                foreach (DataRow rows in myDataComposicion.Rows)
                {
                    if (rows["Elementos"] == DBNull.Value) { myValor = 0; }
                    else
                    {
                        myValor = Convert.ToInt32(rows["Elementos"]);
                        if (myValor > 0)
                        {
                            cBaja = false;
                            return cBaja;
                        }
                    }
                }

                //Ver en Presupuestos
                myCadena = "Select count(*) as Elementos from DetallePresupuestos where IdArticulo = " + Convert.ToInt32(row.Cells["IdArticulo"].Value);
                //Armar la consulta
                myDataComposicion = clsDataBD.GetSql(myCadena);
                //Ver el valor
                foreach (DataRow rows in myDataComposicion.Rows)
                {
                    if (rows["Elementos"] == DBNull.Value) { myValor = 0; }
                    else
                    {
                        myValor = Convert.ToInt32(rows["Elementos"]);
                        if (myValor > 0)
                        {
                            cBaja = false;
                            return cBaja;
                        }
                    }
                }
                //Ver en Detalle Facturas
                myCadena = "Select count(*) as Elementos from eFacturaDetalle where IdArticulo = " + Convert.ToInt32(row.Cells["IdArticulo"].Value);
                //Armar la consulta
                myDataComposicion = clsDataBD.GetSql(myCadena);
                //Ver el valor
                foreach (DataRow rows in myDataComposicion.Rows)
                {
                    if (rows["Elementos"] == DBNull.Value) { myValor = 0; }
                    else
                    {
                        myValor = Convert.ToInt32(rows["Elementos"]);
                        if (myValor > 0)
                        {
                            cBaja = false;
                            return cBaja;
                        }
                    }
                }
            }

            //Retornar valor final
            return cBaja;
        }

        #endregion

        #region Metodo GuardarNuevoArticulo

        /*************************************/
        /*Metodo    : GuardarArticuloInsumo
         *Proposito : Insert de Articulo 
         *By        : Developer 
         *Date      : 13/11/16 updated
         *Parameters: None
         *Return    : Nothing
         ************************************/
        private void GuardarNuevoArticulo(clsArticulos NuevoArticulo)
        {
            //Variable
            string myCadena = "";

            try
            {

                //Alta de Articulos
                myCadena = "INSERT INTO Articulos (IdSubrubroArticulo," +
                                                 " IdUnidadMedida," +
                                                 " CodigoArticulo," +
                                                 " Articulo," +
                                                 " Unidades," +
                                                 " Precio," +
                                                 " PrecioAnterior," +
                                                 " UltimoCostoCompra," +
                                                 " UltimoProveedor," +
                                                 " UltimaCompra," +
                                                 " LlevaStock," +
                                                 " Facturable," +
                                                 " Stock," +
                                                 " StockMinimo," +
                                                 " StockMaximo," +
                                                 " StockPuntoPedido," +
                                                 " PorcentajeIva," +
                                                 " Activo," +
                                                 " IncListaPre," +
                                                 " IncListaRes," +
                                                 " chkSombreado," +
                                                 " rbtColor," +
                                                 " chkSProd," +
                                                 " CompIns" + 
                                                 ") values (" + NuevoArticulo.IdSubrubroArticulo + ","
                                                                + NuevoArticulo.IdUnidadMedida + ",'"
                                                                + NuevoArticulo.CodigoArticulo + "','"
                                                                + NuevoArticulo.Articulo + "',"
                                                                + NuevoArticulo.Unidades.ToString().Replace(",", ".") + ","
                                                                + NuevoArticulo.Precio.ToString().Replace(",", ".") + ","
                                                                + NuevoArticulo.PrecioAnterior.ToString().Replace(",", ".") + ","
                                                                + NuevoArticulo.UltimoCostoCompra.ToString().Replace(",", ".") + ",'"
                                                                + NuevoArticulo.UltimoProveedor + "','"
                                                                + NuevoArticulo.UltimaCompra + "',"
                                                                + NuevoArticulo.LlevaStock + ","
                                                                + NuevoArticulo.Facturable + ","
                                                                + NuevoArticulo.Stock.ToString().Replace(",", ".") + ","
                                                                + NuevoArticulo.StockMinimo.ToString().Replace(",", ".") + ","
                                                                + NuevoArticulo.StockMaximo.ToString().Replace(",", ".") + ","
                                                                + NuevoArticulo.StockPuntoPedido.ToString().Replace(",", ".") + ","
                                                                + NuevoArticulo.PorcentajeIva.ToString().Replace(",", ".") + ","
                                                                + NuevoArticulo.Activo + ","
                                                                + NuevoArticulo.incListaPre + ","
                                                                + NuevoArticulo.incListaRes + ","
                                                                + NuevoArticulo.chkSombreado + ","
                                                                + NuevoArticulo.rbtColor + "," +
                                                                + NuevoArticulo.chkSProd + "," +
                                                                + NuevoArticulo.CompIns + ")";

                //GUARDAR EN ARTICULOS
                clsDataBD.GetSql(myCadena);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        #endregion

        #region Metodo ModificaArticuloInsumo

        /*************************************/
        /*Metodo    : ModificaArticuloInsumo
         *Proposito : Update del Insumo 
         *By        : Developer 
         *Date      : 13/11/16 updated
         *Parameters: None
         *Return    : Nothing
         ************************************/
        private void ModificaArticuloInsumo(clsArticulos NuevoArticulo)
        {
            String myCadena = "";

            //Preparar cadena SQL modificacion de Insumo  
            myCadena = "UPDATE Insumos SET Costo = " + NuevoArticulo.Precio.ToString().Replace(",", ".") + " Where " +
                       "IdArticulo = " + NuevoArticulo.IdArticulo;

            //Ejecutar modificacion Insumo
            clsDataBD.GetSql(myCadena);

            // Modificacion de Articulo                        
            NuevoArticulo.Precio = 0; //No es producto
            myCadena = "UPDATE Articulos SET IdSubrubroArticulo = " + NuevoArticulo.IdSubrubroArticulo + "," +
                                             " IdUnidadMedida = " + NuevoArticulo.IdUnidadMedida + "," +
                                             " CodigoArticulo = '" + NuevoArticulo.CodigoArticulo + "'," +
                                             " Articulo = '" + NuevoArticulo.Articulo + "'," +
                                             " Unidades = " + NuevoArticulo.Unidades.ToString().Replace(",", ".") + "," +
                                             " Precio = " + NuevoArticulo.Precio.ToString().Replace(",", ".") + "," +
                                             " PrecioAnterior = " + NuevoArticulo.Precio.ToString().Replace(",", ".") + "," +
                                             " UltimoCostoCompra = " + NuevoArticulo.UltimoCostoCompra.ToString().Replace(",", ".") + "," +
                                             " UltimoProveedor = '" + NuevoArticulo.UltimoProveedor + "'," +
                                             " UltimaCompra = " + NuevoArticulo.UltimaCompra + "," +
                                             " LlevaStock = " + NuevoArticulo.LlevaStock + "," +
                                             " Facturable = " + NuevoArticulo.Facturable + "," +
                                             " Stock = " + NuevoArticulo.Stock.ToString().Replace(",", ".") + "," +
                                             " StockMinimo = " + NuevoArticulo.StockMinimo.ToString().Replace(",", ".") + "," +
                                             " StockMaximo = " + NuevoArticulo.StockMaximo.ToString().Replace(",", ".") + "," +
                                             " StockPuntoPedido = " + NuevoArticulo.StockPuntoPedido.ToString().Replace(",", ".") + "," +
                                             " PorcentajeIva = " + NuevoArticulo.PorcentajeIva.ToString().Replace(",", ".") + "," +
                                             " Activo = " + NuevoArticulo.Activo + "," +
                                             " IncListaPre = " + NuevoArticulo.incListaPre + "," +
                                             " IncListaRes = " + NuevoArticulo.incListaRes +
                                             " WHERE IdArticulo = " + NuevoArticulo.IdArticulo;

            //Ejecutar modificacion del artículo
            clsDataBD.GetSql(myCadena);
        }

        #endregion

        #region Metodo GuardarNuevoInsumo

        /*************************************/
        /*Metodo    : GuardarNuevoInsumo
         *Proposito : Insert en Insumos
         *By        : Developer 
         *Date      : 13/11/16 updated
         *Parameters: None
         *Return    : Nothing
         ************************************/
        private void GuardarNuevoInsumo(clsArticulos NuevoArticulo)
        {
            //Variable
            string myCadena = "";
            //Preparar datos para guardar Insumo
            clsArticulosInsumos oInsumo = new clsArticulosInsumos();
            oInsumo.IdArticulo = clsDataBD.RetornarUltimoId("Articulos", "IdArticulo");

            //Cargar el costo del insumo al objeto
            oInsumo.Costo = NuevoArticulo.Precio;

            //Cadena SQL 
            myCadena = "INSERT INTO Insumos (IdArticulo, Costo) values (" + oInsumo.IdArticulo + ","
                                                                          + oInsumo.Costo.ToString().Replace(",", ".") + ")";
            //Guardar en Insumos
            clsDataBD.GetSql(myCadena);
        }

        #endregion

        #endregion

        #region Evento CheckedChanged del ChkLlevaStock

        private void chkLlevaStock_CheckedChanged(object sender, EventArgs e)
        {

            if (!(this.myEstado == "C"))
            {
                /*Si lo chequea, habilitar controles*/
                if (chkLlevaStock.Checked)
                {
                    txtStock.TabStop = true;
                    txtStock.Enabled = true;
                    txtStockMin.TabStop = true;
                    txtStockMin.Enabled = true;
                    txtStockMax.TabStop = true;
                    txtStockMax.Enabled = true;
                    txtStockPed.TabStop = true;
                    txtStockPed.Enabled = true;
                }
                else
                {
                    txtStock.TabStop = false;
                    txtStock.Enabled = false;
                    txtStock.Text = "";
                    txtStockMin.TabStop = false;
                    txtStockMin.Enabled = false;
                    txtStockMin.Text = "";
                    txtStockMax.TabStop = false;
                    txtStockMax.Enabled = false;
                    txtStockMax.Text = "";
                    txtStockPed.TabStop = false;
                    txtStockPed.Enabled = false;
                    txtStockPed.Text = "";
                }
            }
        }

        #endregion

        #region Eventos Leave

        #region Evento Leave Codigo Articulo
        private void txtCodArt_Leave(object sender, EventArgs e)
        {
            txtCodArt.Text = txtCodArt.Text.ToUpper();
        }

        #endregion

        #region Evento Leave Articulo

        private void txtArt_Leave(object sender, EventArgs e)
        {
            txtArt.Text = txtArt.Text.ToUpper();
        }

        #endregion

        #region Evento Leave Proveedor Ultimo

        private void txtProv_Leave(object sender, EventArgs e)
        {
            txtProv.Text = txtProv.Text.ToUpper();
        }

        #endregion

        #region Evento Leave PorcIVA

        private void txtPorcIVA_Leave(object sender, EventArgs e)
        {
            if (!(txtPorcIVA.Text == ""))
            {
                this.txtPorcIVA.Text = Convert.ToDouble(txtPorcIVA.Text, CultureInfo.InvariantCulture).ToString("#0.00", CultureInfo.InvariantCulture);
            }
        }

        #endregion

        #endregion

        #region Evento TextChanged txtArticulo

        private void txtArticulo_TextChanged(object sender, EventArgs e)
        {
            CargarGrillaBusqueda(this.txtArticulo.Text, "Articulo");
        }

        #endregion

        #region Eventos TextChanged

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            CargarGrillaBusqueda(this.txtCodigo.Text, "CodigoArticulo");
        }

        #endregion

    }
}

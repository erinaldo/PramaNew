using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Prama.Formularios.Articulos;

namespace Prama
{
    public partial class frmArticulos : Form
    {
        #region VARIABLES A NIVEL FORM

        //Para capturar por donde ingresa al formulario (Insumo, Materia Prima, etc)
        int nTipArt = 0;
        //Otras
        int indexFila = 0;
        string myEstado = "C";
        int LastIns = 0;
        int LastProd = 0;
        int IdArtRepos = 0; //Para reposicionamiento
        bool bSearch = false;
       
        #endregion

        #region Constructor Formulario

        public frmArticulos(int nValue)
        {
            InitializeComponent();
            nTipArt = nValue;
        }

        #endregion

        #region Metodos de la Clase

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
                    if (clsGlobales.UsuarioLogueado.Nivel >=clsGlobales.cParametro.NivelBaja)
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
                            if (this.myEstado == "M" || this.myEstado == "A")
                            {
                                if (c.Name == "txtCodArt")
                                {
                                   c.TabStop = false;
                                   c.Enabled = false;
                                   break;
                                }
                            }

                            //Producto? tiene que detallar composicion; si es de insumos y gastos precio no puede editar
                            if (this.nTipArt == 2 && c.Name == "txtPrecio" && this.chkSProd.Checked==false)
                            {
                                c.TabStop = false; c.Enabled = false;
                                btnPrecio.Visible = true;
                            }
                            //Idem anterior, pero se compone de productos, puede editar precio.
                            else if (this.nTipArt == 2 && c.Name == "txtPrecio" && this.chkSProd.Checked == true)
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
                            else
                            {
                                if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelStock)
                                {
                                    switch (c.Name)
                                    {
                                        case "txtStock":
                                        case "txtStockMin":
                                        case "txtStockMax":
                                        case "txtStockPed":
                                            c.TabStop = true;
                                            c.Enabled = true;
                                            break;
                                    }
                                }
                                else
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
                        }

                        //RADIOBUTTON
                        if (c is RadioButton)
                        {
                            if (nTipArt == 2)
                            {
                                if (c.Name == "rbtNaranja" || c.Name == "rbtVerde")
                                {
                                    c.TabStop = false;
                                    c.Visible = true;
                                    c.Enabled = false;
                                }
                            }
                            else
                            {
                                if (c.Name == "rbtNaranja" || c.Name == "rbtVerde")
                                {
                                    c.TabStop = false;
                                    c.Visible = false;
                                    c.Enabled = false;
                                }
                            }
                        }

                        //CHECKBOX CASOS ESPECIALES
                        if (c is CheckBox)
                        {
                            if (nTipArt == 2)
                            {
                                if (c.Name == "chkListaGral" || c.Name == "chkListaRes" || c.Name == "chkSombreado" || c.Name == "chkSProd")
                                {
                                    c.TabStop = true;
                                    c.Visible = true;
                                    c.Enabled = true;
                                }
                            }
                            if (nTipArt == 1) 
                            {
                                if (c.Name == "chkListaGral" || c.Name == "chkListaRes" || c.Name == "chkSombreado" || c.Name == "chkSProd")
                                {
                                    c.TabStop = false;
                                    c.Visible = false;
                                    c.Enabled = false;
                                }
                            }

                            //Compone Insumos
                            if (nTipArt == 1)
                            {
                                if (c.Name == "chkcInsumos")
                                {
                                    c.TabStop = true;
                                    c.Visible = true;
                                    c.Enabled = true;
                                }
                            }
                            else if (nTipArt == 2)                            
                            { 
                                if (c.Name == "chkcInsumos")
                                {
                                    c.TabStop = false;
                                    c.Visible = false;
                                    c.Enabled = false;
                                }
                            }

                            if (c.Name == "chkFacturado" || c.Name == "chkLlevaStock")
                            {
                                c.TabStop = true;
                                c.Visible = true;
                                c.Enabled = true;
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

                            if (this.nTipArt == 1 && c.Name == "btnCompIns" && this.chkcInsumos.Checked)
                            {
                                c.TabStop = true; c.Enabled = true;
                                btnCompIns.Visible = true;
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
                    this.dgvArt.Size = new System.Drawing.Size(762, 250);
                    this.gpbBusquedas.Visible = false;

                    //HABILITACION DE RADIO BUTTONS
                    if (this.myEstado == "M")
                    {
                        //Solo productos deshabilitado
                        this.chkSProd.Enabled = false;

                        //RADIOBUTTON
                        if (this.chkListaGral.Checked == true || this.chkListaRes.Checked == true)
                        {
                            if (nTipArt == 2)
                            {
                                if (this.chkSombreado.Checked)
                                {
                                    this.rbtNaranja.Enabled = true;
                                    this.rbtVerde.TabStop = true;
                                    this.rbtNaranja.Enabled = true;
                                    this.rbtVerde.Enabled = true;
                                }
                                else
                                {
                                    this.rbtNaranja.Enabled = false;
                                    this.rbtVerde.TabStop = false;
                                    this.rbtNaranja.Enabled = false;
                                    this.rbtVerde.Enabled = false; 
                                }
                            }                            
                        }
                        else
                        {
                            if (nTipArt == 2)
                            {
                                this.rbtNaranja.Enabled = false;
                                this.rbtVerde.TabStop = false;
                                this.rbtNaranja.Enabled = false;
                                this.rbtVerde.Enabled = false;
                            }
                        }
                    }
                    else if (this.myEstado == "A")
                    {
                        //.T. (Solo Productos)
                        this.chkSProd.Enabled = true;

                        this.rbtNaranja.Enabled = false;
                        this.rbtVerde.TabStop = false;
                        this.rbtNaranja.Enabled = false;
                        this.rbtVerde.Enabled = false;
                    }

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

                        //RADIOBUTTON
                        if (c is RadioButton)
                        {
                            if (nTipArt == 2)
                            {
                                if (c.Name == "rbtNaranja" || c.Name == "rbtVerde")
                                {
                                    c.TabStop = false;
                                    c.Visible = false;
                                    c.Enabled = false;
                                }
                            }
                            else
                            {
                                if (c.Name == "rbtNaranja" || c.Name == "rbtVerde")
                                {
                                    c.TabStop = false;
                                    c.Visible = false;
                                    c.Enabled = false;
                                }
                            }
                        }

                        //CHECKBOX CASOS ESPECIALES
                        if (c is CheckBox)
                        {
                            if (nTipArt == 2)
                            {
                                if (c.Name == "chkListaGral" || c.Name == "chkListaRes" || c.Name == "chkSombreado")
                                {
                                    c.TabStop = false;
                                    c.Visible = false;
                                    c.Enabled = false;
                                }
                            }
                            if (nTipArt == 1)
                            {
                                if (c.Name == "chkListaGral" || c.Name == "chkListaRes" || c.Name == "chkSombreado")
                                {
                                    c.TabStop = false;
                                    c.Visible = false;
                                    c.Enabled = false;
                                }
                            }
                        }
                    }

                    //Habilitar busqueda

                    this.dgvArt.Size = new System.Drawing.Size(762, 360);                    
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

                        //RADIOBUTTON
                        if (c is RadioButton)
                        {
                            if (nTipArt == 2)
                            {
                                if (c.Name == "rbtNaranja" || c.Name == "rbtVerde")
                                {
                                    c.TabStop = false;
                                    c.Visible = true;
                                    c.Enabled = false;
                                }
                            }
                            else
                            {
                                if (c.Name == "rbtNaranja" || c.Name == "rbtVerde")
                                {
                                    c.TabStop = false;
                                    c.Visible = false;
                                    c.Enabled = false;
                                }
                            }
                        }

                        //CHECKBOX CASOS ESPECIALES
                        if (c is CheckBox)
                        {
                            if (nTipArt == 2)
                            {
                                if (c.Name == "chkListaGral" || c.Name == "chkListaRes" || c.Name == "chkSombreado" || c.Name == "chkSProd")
                                {
                                    c.TabStop = false;
                                    c.Visible = true;
                                    c.Enabled = false;
                                }
                            }
                            if (nTipArt == 1)
                            {
                                if (c.Name == "chkListaGral" || c.Name == "chkListaRes" || c.Name == "chkSombreado" || c.Name == "chkSProd")
                                {
                                    c.TabStop = false;
                                    c.Visible = false;
                                    c.Enabled = false;
                                }
                            }

                            if (nTipArt == 2)
                            {
                                if (c.Name == "chkcInsumos")
                                {
                                    c.TabStop = false;
                                    c.Visible = false;
                                    c.Enabled = false;
                                }
                            }
                        }

                    }
                    this.dgvArt.Size = new System.Drawing.Size(762, 250);
                    this.gpbBusquedas.Visible = false;

                    this.dgvArt.TabStop = true && (dgvArt.RowCount > 0);
                    this.dgvArt.Enabled = true && (dgvArt.RowCount > 0);

                    this.txtPrecio.Enabled = false;
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
        
            // Vector composicion compuesto
               clsGlobales.ProdSelCompuesto = (double[,])clsValida.ResizeMatriz(clsGlobales.ProdSelCompuesto, new int[] { 0, 2 });
        }

        #endregion


        #region Metodo: CargarArticulosAVector()

        private void CargarArticulosAVector()
        {
            string myCad = "";
            
            //Traer la composicion y subirla al vector
            myCad = "Select * from ProductosCompuestos WHERE IdProdOrigen = " + Convert.ToInt32(dgvArt.CurrentRow.Cells["IdArticulo"].Value);
            DataTable myData = new DataTable();
            myData = clsDataBD.GetSql(myCad);

            int Item = 0;

            // Recorro la grilla para marcar los articulos ya seleccionados
            foreach (DataRow row in myData.Rows)
            {
                clsGlobales.ProdSelCompuesto = (double[,])clsValida.ResizeMatriz(clsGlobales.ProdSelCompuesto, new int[] { Item + 1, 3 });
                //A la posición creada le asigno el Id seleccionado
                clsGlobales.ProdSelCompuesto[Item, 0] = Convert.ToDouble(row["IdProdCompone"].ToString());
                clsGlobales.ProdSelCompuesto[Item, 1] = Convert.ToDouble(row["Cantidad"].ToString());
                clsGlobales.ProdSelCompuesto[Item, 2] = 0;

                //Cambiar Item
                Item++;
            }

        }

        #endregion

        #region Metodo: getComposicionProducto

        //Carga los datos de la composicion en los vectores
        private void getComposicionProducto()
        {
            int iterador = 0;

            //DATOS DEL PRODUCTO /////////////////////////////////////////
            //Traer Datos del Producto (Tanda, Cantidad de Articulos, etc)
            string cadSQL = "exec CargarProductoByIDArticulo " + Convert.ToInt32(dgvArt.CurrentRow.Cells["IdArticulo"].Value);

            int IdProducto = 0;

            //DataTable
            DataTable myDataTable = clsDataBD.GetSql(cadSQL);

            //Si hay datos continuar, sino salir
            if (myDataTable.Rows.Count == 0) { return; }

            //Redimensionar vector y dejar lo vacío
            clsGlobales.ProductoDatosST = (string[])clsValida.ResizeVector(clsGlobales.ProductoDatosST, 6);

           /* string Tanda = "";
            string CA = "";
            string CI = "";
            string CG = "";*/
            //Mostrar Datos
            foreach (DataRow fila in myDataTable.Rows)
            {

                //IdProducto
                IdProducto = Convert.ToInt32(fila["IdProducto"].ToString());

                //Producto
                clsGlobales.ProductoDatosST[0] = fila["Tanda"].ToString();
                clsGlobales.ProductoDatosST[1] = fila["CostoAcumulado"].ToString();
                clsGlobales.ProductoDatosST[2] = fila["CostoInsumos"].ToString();
                clsGlobales.ProductoDatosST[3] = fila["CostoGastos"].ToString();
                clsGlobales.ProductoDatosST[4] = "0";
                clsGlobales.ProductoDatosST[5] = fila["IdAreaProduccion"].ToString();

            }

            //Redimensionar vector y dejar lo vacío
            clsGlobales.ProductoDatos = (double[])clsValida.ResizeVector(clsGlobales.ProductoDatos, 6);

            //DETALLE DE COMPOSICION DE INSUMOS //////////////////////////////////
            //Traer Datos del detalle de la composicion by IdProducto (sin gastos)
            cadSQL = "exec CargarDetComposicionById " + IdProducto;

            int Elementos = 0;

            //DataTable
            DataTable mDataTable = clsDataBD.GetSql(cadSQL);

            //DATOS DEL DETALLE DE COMPOSICION GASTOS ////////////////////////////
            //Traer Datos del detalle de la composicion gastos
            cadSQL = "exec CargarDetGastosComp " + IdProducto;

            //DataTable
            DataTable xDataTable = clsDataBD.GetSql(cadSQL);

            //Si hay datos...
            if (mDataTable.Rows.Count != 0 && xDataTable.Rows.Count != 0)
            {

                //Elementos
                Elementos = mDataTable.Rows.Count + xDataTable.Rows.Count;

                //Cargar Vector con datos de la grilla de composicion
                clsGlobales.ProductoComposicion = (string[,])clsValida.ResizeMatriz(clsGlobales.ProductoComposicion, new int[] { Elementos, 9 });

                //Recorro Matriz
                foreach (DataRow filas in mDataTable.Rows)
                {
                    /*Guardar en Matriz*/
                    clsGlobales.ProductoComposicion[iterador, 0] = filas["Codigo"].ToString();
                    clsGlobales.ProductoComposicion[iterador, 1] = filas["Descripcion"].ToString();
                    clsGlobales.ProductoComposicion[iterador, 2] = filas["Cantidad"].ToString();
                    clsGlobales.ProductoComposicion[iterador, 3] = filas["Unidad"].ToString();
                    clsGlobales.ProductoComposicion[iterador, 4] = filas["CU"].ToString();
                    clsGlobales.ProductoComposicion[iterador, 5] = filas["Costo"].ToString();
                    clsGlobales.ProductoComposicion[iterador, 6] = "I";
                    clsGlobales.ProductoComposicion[iterador, 7] = "0";
                    clsGlobales.ProductoComposicion[iterador, 8] = filas["IdInsumo"].ToString();

                    //Aumentar iterador
                    iterador++;
                }

                //Recorro Matriz
                foreach (DataRow sfilas in xDataTable.Rows)
                {
                    /*Guardar en Matriz*/
                    clsGlobales.ProductoComposicion[iterador, 0] = sfilas["Codigo"].ToString();
                    clsGlobales.ProductoComposicion[iterador, 1] = sfilas["Descripcion"].ToString();
                    clsGlobales.ProductoComposicion[iterador, 2] = sfilas["Cantidad"].ToString();
                    clsGlobales.ProductoComposicion[iterador, 3] = sfilas["Unidad"].ToString();
                    clsGlobales.ProductoComposicion[iterador, 4] = sfilas["CU"].ToString();
                    clsGlobales.ProductoComposicion[iterador, 5] = (Convert.ToDouble(sfilas["CU"]) * Convert.ToDouble(sfilas["Cantidad"])).ToString("#0.00000");
                    clsGlobales.ProductoComposicion[iterador, 6] = "G";
                    clsGlobales.ProductoComposicion[iterador, 7] = sfilas["IdGastoFijo"].ToString();
                    clsGlobales.ProductoComposicion[iterador, 8] = "0";

                    //Aumentar iterador
                    iterador++;
                }

            }
            else if (mDataTable.Rows.Count != 0 && xDataTable.Rows.Count == 0)
            {
                //Elementos
                Elementos = mDataTable.Rows.Count;

                //Cargar Vector con datos de la grilla de composicion
                clsGlobales.ProductoComposicion = (string[,])clsValida.ResizeMatriz(clsGlobales.ProductoComposicion, new int[] { Elementos, 9 });

                //Recorro Matriz
                foreach (DataRow filas in mDataTable.Rows)
                {
                    /*Guardar en Matriz*/
                    clsGlobales.ProductoComposicion[iterador, 0] = filas["Codigo"].ToString();
                    clsGlobales.ProductoComposicion[iterador, 1] = filas["Descripcion"].ToString();
                    clsGlobales.ProductoComposicion[iterador, 2] = filas["Cantidad"].ToString();
                    clsGlobales.ProductoComposicion[iterador, 3] = filas["Unidad"].ToString();
                    clsGlobales.ProductoComposicion[iterador, 4] = filas["CU"].ToString();
                    clsGlobales.ProductoComposicion[iterador, 5] = filas["Costo"].ToString();
                    clsGlobales.ProductoComposicion[iterador, 6] = "I";
                    clsGlobales.ProductoComposicion[iterador, 7] = "0";
                    clsGlobales.ProductoComposicion[iterador, 8] = filas["IdInsumo"].ToString();

                    //Aumentar iterador
                    iterador++;
                }
            }
            else if (mDataTable.Rows.Count == 0 && xDataTable.Rows.Count != 0)
            {
                //Elementos
                Elementos = xDataTable.Rows.Count;

                //Cargar Vector con datos de la grilla de composicion
                clsGlobales.ProductoComposicion = (string[,])clsValida.ResizeMatriz(clsGlobales.ProductoComposicion, new int[] { Elementos, 9 });

                //Recorro Matriz
                foreach (DataRow sfilas in xDataTable.Rows)
                {
                    /*Guardar en Matriz*/
                    clsGlobales.ProductoComposicion[iterador, 0] = sfilas["Codigo"].ToString();
                    clsGlobales.ProductoComposicion[iterador, 1] = sfilas["Descripcion"].ToString();
                    clsGlobales.ProductoComposicion[iterador, 2] = sfilas["Cantidad"].ToString();
                    clsGlobales.ProductoComposicion[iterador, 3] = sfilas["Unidad"].ToString();
                    clsGlobales.ProductoComposicion[iterador, 4] = sfilas["CU"].ToString();
                    clsGlobales.ProductoComposicion[iterador, 5] = sfilas["CU"].ToString();
                    clsGlobales.ProductoComposicion[iterador, 6] = "G";
                    clsGlobales.ProductoComposicion[iterador, 7] = sfilas["IdGastoFijo"].ToString();
                    clsGlobales.ProductoComposicion[iterador, 8] = "0";

                    //Aumentar iterador
                    iterador++;
                }
            }

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

                //.T.            
                bSearch = true;
            }

            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            dgvArt.DataSource = null;
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
            }

            //Mostrar datos  
            this.getMostrarDatos();
        }

        #endregion

        #endregion

        #region Eventos del Formulario

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
                    txtPrecioAnterior.Text = row.Cells["PrecioAnterior"].Value.ToString();
                }
                else if (nTipArt == 1)
                {
                    txtPrecio.Text = row.Cells["Costo"].Value.ToString();
                    txtPrecioAnterior.Text = row.Cells["PrecioAnterior"].Value.ToString();
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
                
                //Sombra
                chkSombreado.Checked = Convert.ToBoolean(row.Cells["Sombreado"].Value);

                chkcInsumos.Checked = (bool)row.Cells["CompIns"].Value;

                //Color
                if (Convert.ToBoolean(row.Cells["Color"].Value)==true)
                {
                    rbtNaranja.Checked = false;
                    rbtVerde.Checked = true;
                }
                else
                {
                    rbtNaranja.Checked = true;
                    rbtVerde.Checked = false;
                }

                //Sombra
                chkSProd.Checked = Convert.ToBoolean(row.Cells["chkSoloProd"].Value);

                if (nTipArt == 2) //Productos
                {
                    cboCoeficiente.SelectedValue = Convert.ToInt32(row.Cells["IdCoeficienteArticulo"].Value.ToString());
                }

                if (!(row.Cells["UltimaCompra"].Value.ToString() == "0"))
                {

                    dtUltCompra.Value = Convert.ToDateTime(row.Cells["UltimaCompra"].Value);
                }
            }
        }

        #endregion

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
            //char ch = e.KeyChar;
            //if (ch == 44)
            //{
            //    e.KeyChar = Convert.ToChar(46);
            //    ch = e.KeyChar;

            //}
            ////PUNTO DECIMAL. N.
            //if (ch == 46 && txtStock.Text.IndexOf('.') != -1)
            //{
            //    e.Handled = true;
            //    return;
            //}
            ////NUMEROS. N.
            //if (!char.IsDigit(ch) && ch != 8 && ch != 46 && ch != 13 && ch != 9)
            //{
            //    e.Handled = true;
            //    return;
            //}
            ////CONTROLAR CANTIDAD DE DECIMALES LUEGO DEL SEPARADOR DECIMAL. N.
            //if (!char.IsControl(e.KeyChar))
            //{
            //    if (txtStock.Text.IndexOf('.') > -1 &&
            //        txtStock.Text.Substring(txtStock.Text.IndexOf('.')).Length >= (2 + 1))
            //    {
            //        e.Handled = true;
            //    }
            //}

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

        #region Evento Keypress txtStockMin
        private void txtStockMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            //char ch = e.KeyChar;
            //if (ch == 44)
            //{
            //    e.KeyChar = Convert.ToChar(46);
            //    ch = e.KeyChar;

            //}
            ////PUNTO DECIMAL. N.
            //if (ch == 46 && txtStockMin.Text.IndexOf('.') != -1)
            //{
            //    e.Handled = true;
            //    return;
            //}
            ////NUMEROS. N.
            //if (!char.IsDigit(ch) && ch != 8 && ch != 46 && ch != 13 && ch != 9)
            //{
            //    e.Handled = true;
            //    return;
            //}
            ////CONTROLAR CANTIDAD DE DECIMALES LUEGO DEL SEPARADOR DECIMAL. N.
            //if (!char.IsControl(e.KeyChar))
            //{
            //    if (txtStockMin.Text.IndexOf('.') > -1 &&
            //        txtStockMin.Text.Substring(txtStockMin.Text.IndexOf('.')).Length >= (2 + 1))
            //    {
            //        e.Handled = true;
            //    }
            //}

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

        #region Evento Keypress txtStockMax

        private void txtStockMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            //char ch = e.KeyChar;
            //if (ch == 44)
            //{
            //    e.KeyChar = Convert.ToChar(46);
            //    ch = e.KeyChar;

            //}
            ////PUNTO DECIMAL. N.
            //if (ch == 46 && txtStockMax.Text.IndexOf('.') != -1)
            //{
            //    e.Handled = true;
            //    return;
            //}
            ////NUMEROS. N.
            //if (!char.IsDigit(ch) && ch != 8 && ch != 46 && ch != 13 && ch != 9)
            //{
            //    e.Handled = true;
            //    return;
            //}
            ////CONTROLAR CANTIDAD DE DECIMALES LUEGO DEL SEPARADOR DECIMAL. N.
            //if (!char.IsControl(e.KeyChar))
            //{
            //    if (txtStockMax.Text.IndexOf('.') > -1 &&
            //        txtStockMax.Text.Substring(txtStockMax.Text.IndexOf('.')).Length >= (2 + 1))
            //    {
            //        e.Handled = true;
            //    }
            //}

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

        #region Evento Keypress txtStockPed
        private void txtStockPed_KeyPress(object sender, KeyPressEventArgs e)
        {
            //char ch = e.KeyChar;
            //if (ch == 44)
            //{
            //    e.KeyChar = Convert.ToChar(46);
            //    ch = e.KeyChar;

            //}
            ////PUNTO DECIMAL. N.
            //if (ch == 46 && txtStockPed.Text.IndexOf('.') != -1)
            //{
            //    e.Handled = true;
            //    return;
            //}
            ////NUMEROS. N.
            //if (!char.IsDigit(ch) && ch != 8 && ch != 46 && ch != 13 && ch != 9)
            //{
            //    e.Handled = true;
            //    return;
            //}
            ////CONTROLAR CANTIDAD DE DECIMALES LUEGO DEL SEPARADOR DECIMAL. N.
            //if (!char.IsControl(e.KeyChar))
            //{
            //    if (txtStockPed.Text.IndexOf('.') > -1 &&
            //        txtStockPed.Text.Substring(txtStockPed.Text.IndexOf('.')).Length >= (2 + 1))
            //    {
            //        e.Handled = true;
            //    }
            //}

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

        #region Evento Kepress txtPorcIVA

        private void txtPorcIVA_KeyPress(object sender, KeyPressEventArgs e)
        {
            //char ch = e.KeyChar;
            //if (ch == 44)
            //{
            //    e.KeyChar = Convert.ToChar(46);
            //    ch = e.KeyChar;

            //}
            ////PUNTO DECIMAL. N.
            //if (ch == 46 && txtPorcIVA.Text.IndexOf('.') != -1)
            //{
            //    e.Handled = true;
            //    return;
            //}
            ////NUMEROS. N.
            //if (!char.IsDigit(ch) && ch != 8 && ch != 46 && ch != 13 && ch != 9)
            //{
            //    e.Handled = true;
            //    return;
            //}
            ////CONTROLAR CANTIDAD DE DECIMALES LUEGO DEL SEPARADOR DECIMAL. N.
            //if (!char.IsControl(e.KeyChar))
            //{
            //    if (txtPorcIVA.Text.IndexOf('.') > -1 &&
            //        txtPorcIVA.Text.Substring(txtPorcIVA.Text.IndexOf('.')).Length >= (2 + 1))
            //    {
            //        e.Handled = true;
            //    }
            //}

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

        #endregion

        #region Evento Load del Form

        private void frmArticulos_Load(object sender, EventArgs e)
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
            //Nueva columna
            if (nTipArt == 2)
            {
                //Achicar columna Articulo
                 this.dgvArt.Columns[5].Width = 200;
                 //Achicar columna Articulo
                 this.dgvArt.Columns[6].Width = 60;
                //Visible Columna Pcio Publi con Iva
                 this.dgvArt.Columns[12].Visible = true;
                //Agregar Pcio Publico Iva
                 SetPcioPubIva();
            }
        }

        #endregion

        #region Metodo SetPcioPubIva

        private void SetPcioPubIva()
        {

            double dImporte = 0;
            double calculoiva=0;

            foreach (DataGridViewRow row in this.dgvArt.Rows)
            {
                 calculoiva = 1 + (Convert.ToDouble(row.Cells["PorcentajeIva"].Value.ToString()) / 100);
                 dImporte = Convert.ToDouble(row.Cells["Precio"].Value.ToString()) * calculoiva;
                 row.Cells["PcioPubIva"].Value = dImporte.ToString("#0.00000");
            }
        
        }

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

             //ES UN PRODUCTO
                if (this.nTipArt == 2)
                {
                    //VACIAR VECTORES
                    VaciarVectorGlobales();

                    //TRAER COMPOSICION DE PRODUCTO
                    getComposicionProducto();

                    //ES PRODUCTO COMPUESTO?....
                    if (chkSProd.Checked)
                    {
                        //TRAER COMPOSICION PRODUCTO COMPUESTO
                        this.CargarArticulosAVector();
                    }
                }
                else if (this.nTipArt==1) //ES INSUMO
                {
                    //VACIAR VECTORES
                    VaciarVectorGlobales();

                    //TRAER COMPOSICION
                    if (chkcInsumos.Checked)
                    {
                      this.CargarCompInsAVector();
                    }

                }
            }
        }

        #endregion

        #endregion

        #region METODO: CargarCompInsAVector

        //METODO: CargarCompInsAVector
        private void CargarCompInsAVector()
        {
            //Traer primero Datos de ProductoInsumo (esto a lo mejor se puede hacer con un solo Select en vez de 2)
            string cadSQL = "exec CargarDetCompInsumoById " + Convert.ToInt32(dgvArt.CurrentRow.Cells["IdArticulo"].Value);

            //Variable para contador de filas grilla
            int iterador = 0;

            //DataTable
            DataTable myDataTable = clsDataBD.GetSql(cadSQL);

            int filas = myDataTable.Rows.Count;

            //Hago Resize
            clsGlobales.ProductoComposicion = (string[,])clsValida.ResizeMatriz(clsGlobales.ProductoComposicion, new int[] { filas, 9 });

            //Recorrer
            foreach (DataRow row in myDataTable.Rows)
            {
                /*Guardar en Matriz*/
                clsGlobales.ProductoComposicion[iterador, 0] = row["Codigo"].ToString();
                clsGlobales.ProductoComposicion[iterador, 1] = row["Descripcion"].ToString();
                clsGlobales.ProductoComposicion[iterador, 2] = row["Cantidad"].ToString();
                clsGlobales.ProductoComposicion[iterador, 3] = row["Unidad"].ToString();
                clsGlobales.ProductoComposicion[iterador, 4] = row["CU"].ToString();
                clsGlobales.ProductoComposicion[iterador, 5] = row["Costo"].ToString();
                clsGlobales.ProductoComposicion[iterador, 6] = row["Tipo"].ToString();
                clsGlobales.ProductoComposicion[iterador, 7] = "0";
                clsGlobales.ProductoComposicion[iterador, 8] = row["IdInsumo"].ToString();

                if (string.IsNullOrEmpty(txtCantArt.Text))
                {
                    txtCantArt.Text = row["cTanda"].ToString();
                }
                
                //Aumentar iterador
                iterador++;
            }
        }

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
                    if (dgvArt.Rows.Count > 0)
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
                        LastIns+=1;
                        //Nuevo Codigo
                        newIns = newIns + LastIns.ToString("D5");
                    }

                    //Asignar
                    txtCodArt.Text = newIns;
                }
                else
                {
                    LastProd = clsDataBD.getUltComp("Ult_Prod", clsGlobales.cParametro.PtoVtaPorDefecto, 0);
                    string newProd = "PRO";

                    if (LastProd == 0)
                    {
                        LastProd = clsGlobales.cParametro.UltProd + 1;
                       //Nuevo Insumo
                        newProd = newProd + (clsGlobales.cParametro.UltProd+1).ToString("D5");
                    }
                    else
                    {
                        //Suma 1 y arma el codeigo
                        LastProd += 1;
                        //Nuevo Codigo
                        newProd = newProd + LastProd.ToString("D5");
                    }

                    //Asignar
                    txtCodArt.Text = newProd;
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
            VaciarVectorGlobales();

         //Cerrar
            this.Close();
        }

        #endregion

        #region Boton Cancelar

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Recargar
            if (this.myEstado == "B" && bSearch)
            {
                // Cambio el estado del formulario a agregar. G.
                this.myEstado = "C";
                // Lleno nuevamente la grilla
                this.getCargarGrilla();
                // Limpio los controles del formulario. N.    
                setLimpiarControlesForm();
                // Activo todos los botones
                setActivarBotones();
                // Habilito los controles
                setHabilitarControles();
                //Foco
                PosicionarFocoFila();
                //.F.            
                bSearch = false;

            }
            else
            {
                // Cambio el estado del formulario a agregar. G.
                this.myEstado = "C";
                // Activo todos los botones
                setActivarBotones();
                // Limpio los controles del formulario. N.    
                setLimpiarControlesForm();
                // Habilito los controles
                setHabilitarControles();
                //Foco
                PosicionarFocoFila();

            }

            // Inhabilito lupa composicion precio
            if (this.nTipArt == 2)
            {
                btnPrecio.Visible = false;
            }

          //.F.            
            bSearch = false;

            //.F.
            clsGlobales.bCargoProdCompto = false;

          //Bandera a false
            clsGlobales.bCambio = false;
        }

        #endregion

        #region Reposicionar Grilal por Id

        //REPOSICIONA GRILLA POR ID
        private void ReposicionarById()
        {
            //recorrer
            foreach (DataGridViewRow myRow in this.dgvArt.Rows)
            {
                if (Convert.ToInt32(myRow.Cells["IdArticulo"].Value) == IdArtRepos)
                {
                    // Devuelvo el foco a la fila de la grilla desde donde se llamó
                    this.dgvArt.CurrentCell = dgvArt[4, myRow.Index];

                    // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                    EventArgs ea = new EventArgs();
                    this.dgvArt_SelectionChanged(this.dgvArt, ea);
                }
            }
        }

        #endregion

        #region Boton Aceptar

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            //Busqueda Activa?...
            if (this.myEstado == "B")
            {
                //IdArtRepos
                IdArtRepos = Convert.ToInt32(dgvArt.CurrentRow.Cells["IdArticulo"].Value.ToString());
                // Cambio mi estado
                this.myEstado = "C";
                // Lleno nuevamente la grilla
                this.getCargarGrilla();
                // Activo todos los botones
                setActivarBotones();
                // Habilito los controles
                setHabilitarControles();
                 //Id >0? Solo cuando busca reposiciona por ID
                if (!(IdArtRepos == 0 && bSearch))
                {
                   ReposicionarById();
                   IdArtRepos = 0;
                }
                else
                {
                 //Foco
                   PosicionarFocoFila();
                }
                //.F.
                clsGlobales.bCargoProdCompto = false;
                //.F.
                bSearch = false;                
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
            { NuevoArticulo.incListaPre = 1;} else { NuevoArticulo.incListaPre = 0; }

            if (chkListaRes.Checked)
            { NuevoArticulo.incListaRes = 1; } else { NuevoArticulo.incListaRes = 0; }

            if (chkSombreado.Checked)
            {
                //Marca y Color
                NuevoArticulo.chkSombreado = 1;
                if (rbtNaranja.Checked)
                { NuevoArticulo.rbtColor = 0; }
                else
                { NuevoArticulo.rbtColor = 1; }
            }
            else
            {
                NuevoArticulo.chkSombreado = 0;
                NuevoArticulo.rbtColor = 0; 
            }

            if (chkcInsumos.Checked)
            { NuevoArticulo.CompIns = 1; }
            else
            { NuevoArticulo.CompIns = 0; }

            if (chkSProd.Checked)
            { NuevoArticulo.chkSProd = 1; }
            else
            { NuevoArticulo.chkSProd = 0; }
            
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
            
            if (txtPrecioAnterior.Text=="")
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

                        //Composicion Insumos
                        if (this.chkcInsumos.Checked == true)
                        {
                            //2018-06-21 Veririficar vector no este vacìo al modificar Insumo Compuesto.
                            if (clsGlobales.ProductoComposicion.GetLength(0) == 0)
                            {
                                MessageBox.Show("Ha surgido un problema irrecuperable de datos. El proceso no puede finalizar correctamente!. " +
                                                "Consulte al Administrador del Sistema", "Error de datos!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return; 
                            }

                            //Guardar Composicion de Producto (ProductoInsumo y ProductoGastosFijos)
                            this.ModificaInsumoComposicion(NuevoArticulo);

                            //Vaciar Vectores Globales
                            VaciarVectorGlobales();
                        }
                        else                       
                        {
                            //ELIMINAR COMPOSICION
                            string CadSQL = "DELETE FROM InsumosCompuestos WHERE IdInsOrigen = " + NuevoArticulo.IdArticulo;
                            //EJCUTAR CADSQL
                            clsDataBD.GetSql(CadSQL);
                        }

                        break;

                    case 2: //Producto


                        //Update del Articulo
                          this.ModificaArticuloProducto(NuevoArticulo);

                        //Composicion normal
                          if (chkSProd.Checked == false)
                          {
                              //Update de Producto                                             
                              this.UpdateProducto();

                              //Udate Composicion de Producto (ProductoInsumo y ProductoGastosFijos)
                              this.UpdateComposicionProducto();

                              //Vaciar Vectores Globales
                              VaciarVectorGlobales();
                          }
                          else //Compuesta de Productos
                          {
                                
                              //Updatear el registro en blanco en Tabla Producto
                              this.UpdateProductoComp();

                              //Update Composicion Compuesta
                              ModificaCompProd(NuevoArticulo);

                              //Vaciar Vector Global
                              clsGlobales.ProdSelCompuesto = (double[,])clsValida.ResizeMatriz(clsGlobales.ProdSelCompuesto, new int[] { 0, 3 });

                          }

                       //Fikn
                         break;
                }


            }
            else if (this.myEstado == "A") //ALTA
            {
                //Guardar Nuevo Articulo
                GuardarNuevoArticulo(NuevoArticulo);

                string mySQL = "";

                switch (nTipArt)
                {
                    case 1: //Insumo

                        //Composicion Insumos
                        if (this.chkcInsumos.Checked == true)
                        {

                            //2018-06-21 Veririficar vector no este vacìo al modificar Insumo Compuesto.
                            if (clsGlobales.ProductoComposicion.GetLength(0) == 0)
                            {
                                MessageBox.Show("Error de Datos. El proceso no puede finalizar correctamente!. " +
                                                "Consulte al Administrador del Sistema", "Error de datos!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            //Guardar Composicion de Producto (ProductoInsumo y ProductoGastosFijos)
                            this.GuardaComposicionInsumo();

                            //Vaciar Vectores Globales
                            VaciarVectorGlobales();
                        }

                        
                        //Update Codigo Automatico
                        mySQL = "UPDATE PuntosVentaAFIP SET Ult_Ins = " + LastIns + " WHERE Punto = " + clsGlobales.cParametro.PtoVtaPorDefecto;
                        clsDataBD.GetSql(mySQL);

                        //GUARDA SOLO EL INSUMO EN ARTICULOS
                        GuardarNuevoInsumo(NuevoArticulo);
                        
                        break;

                    case 2: //Producto

                        //Update Codigo Automatico
                        mySQL = "UPDATE PuntosVentaAFIP SET Ult_Prod = " + LastProd + " WHERE Punto = " + clsGlobales.cParametro.PtoVtaPorDefecto;
                        clsDataBD.GetSql(mySQL);

                        //Composicion normal
                        if (chkSProd.Checked == false)
                        {
                            //Guardar Producto
                            this.GuardarNuevoProducto();

                            //Guardar Composicion de Producto (ProductoInsumo y ProductoGastosFijos)
                            this.GuardarComposicionProducto();

                            //Vaciar Vectores Globales
                            this.VaciarVectorGlobales();
                        }
                        else                        
                        {
                            //Guardar Nuevo Vacio para que lo encuentro
                            this.GuardaNvoProdEmpty(Convert.ToInt32(this.cboCoeficiente.SelectedValue));

                            //Guardar Composicion Compuesta ( Producto de Productos ).
                            this.GuardaCompProd();

                            //Liberar vector Global
                            clsGlobales.ProdSelCompuesto = (double[,])clsValida.ResizeMatriz(clsGlobales.ProdSelCompuesto, new int[] { 0, 2 });
                        }

                        //Fin
                        break;
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

        #region METODOS que tiene que ver con la composicion de producto de productos!!!! 15-06 ***

        #region Metodo: ModificaInsumoComposicion 

        //ELIMINA Y VUELVE A GUARDAR LA COMPOSICION DE PRODUCTO DE PRODUCTOS
        private void ModificaInsumoComposicion(clsArticulos NuevoArticulo)
        {

            string CadSQL = "";

            int LastIdDet = 0;

            //ELIMINAR COMPOSICION
            CadSQL = "DELETE FROM InsumosCompuestos WHERE IdInsOrigen = " + NuevoArticulo.IdArticulo;
            //EJCUTAR CADSQL
            clsDataBD.GetSql(CadSQL);

            //Recorrer la Matriz
            for (int iterador = 0; iterador < clsGlobales.ProductoComposicion.GetLength(0); iterador++)
            {

                CadSQL = "";

                //Si esta seleccionado
                if (clsGlobales.ProductoComposicion[iterador, 6] == "I")
                {
                    LastIdDet = clsDataBD.RetornarUltimoId("InsumosCompuestos", "IdDetInsComp") + 1;

                    CadSQL = "INSERT INTO InsumosCompuestos (IdDetInsComp, IdInsOrigen, IdInsCompone, Cantidad, Costo, cTanda) " 
                                                       + " values ("
                                                       + LastIdDet + ","
                                                       + NuevoArticulo.IdArticulo + ","
                                                       + clsGlobales.ProductoComposicion[iterador, 8] + ","
                                                       + clsGlobales.ProductoComposicion[iterador, 2] + ","
                                                       + clsGlobales.ProductoComposicion[iterador, 4] + ","
                                                       + Convert.ToInt32(txtCantArt.Text) + ")";
                    
                    //Guardar
                    clsDataBD.GetSql(CadSQL);
                }
            }
        }

        #endregion

        #region Metodo: GuardaComposicionInsumo ( Guarda composicion de Insumo de Insumo 22-11-17 )

        //GUARDA LA COMPOSICION DE INSUMO DE INSUMO NEW!
        private void GuardaComposicionInsumo()
        {

            string CadSQL = "";

            int LastIdDet = 0;

            //Recorrer la Matriz
            for (int iterador = 0; iterador < clsGlobales.ProductoComposicion.GetLength(0); iterador++)
            {

                CadSQL = "";

                //Si esta seleccionado
                if (clsGlobales.ProductoComposicion[iterador, 6] == "I")
                {
                    LastIdDet = clsDataBD.RetornarUltimoId("InsumosCompuestos", "IdDetInsComp") + 1;

                    CadSQL = "INSERT INTO InsumosCompuestos (IdDetInsComp, IdInsOrigen, IdInsCompone, Cantidad, Costo, cTanda) Values ("
                                                       + LastIdDet + ','
                                                       + clsDataBD.RetornarUltimoId("Articulos", "IdArticulo") + ','
                                                       + clsGlobales.ProductoComposicion[iterador, 8] + ","
                                                       + clsGlobales.ProductoComposicion[iterador, 2] + ","
                                                       + clsGlobales.ProductoComposicion[iterador, 4] + ","
                                                       + Convert.ToInt32(txtCantArt.Text) + ")";

                    //Guardar
                    clsDataBD.GetSql(CadSQL);
                }

            }

        }

        #endregion

        #region Metodo GuardaNvoProdEmpty ( 14-06 )

        //LO GUARDAMOS EN BLANCO PARA QUE LO PUEDA ENCONTRAR EL PROCEDIMIENTO DESPUES
        //QUE TRAE LOS PRODUCTOS, SINO ES MUY CARO EL CAMBIO
        private void GuardaNvoProdEmpty(int p_IdCoef = 0)
        {
            //Variable
            string myCadena = "";

            try
            {

                //Preparar datos para guardar Productos
                clsProductos cProducto = new clsProductos();
                cProducto.IdArticulo = clsDataBD.RetornarUltimoId("Articulos", "IdArticulo");
                cProducto.IdAreaProduccion = 1;
                cProducto.IdCoeficienteArticulo = p_IdCoef;
                cProducto.CostoAcumulado = 0;
                cProducto.CostoGastos = 0;
                cProducto.CostoInsumos = 0;
                cProducto.Tanda = 0;

                //Cadena SQL para insertar registro en Productos
                myCadena = "INSERT INTO Productos (IdArticulo," +
                                                "CostoAcumulado," +
                                                "CostoInsumos," +
                                                "CostoGastos," +
                                                "IdAreaProduccion," +
                                                "IdCoeficienteArticulo," +
                                                "Tanda ) values ("
                                                                               + cProducto.IdArticulo + ","
                                                                               + cProducto.CostoAcumulado.ToString().Replace(",", ".") + ","
                                                                               + cProducto.CostoInsumos.ToString().Replace(",", ".") + ","
                                                                               + cProducto.CostoGastos.ToString().Replace(",", ".") + ","
                                                                               + cProducto.IdAreaProduccion + ","
                                                                               + cProducto.IdCoeficienteArticulo + ","
                                                                               + cProducto.Tanda + ")";
                //Guardar en Insumos
                clsDataBD.GetSql(myCadena);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        #endregion

        #region Metodo: GuardaCompProd ( Guarda composicion de producto de productos 14-06 )

        //GUARDA LA COMPOSICION DE PRODUCTO DE PRODUCTOS 14-06-2017 NEW!
        private void GuardaCompProd()
        {

            string CadSQL = "";

            int LastIdDet = 0;

            //Recorrer la Matriz
            for (int iterador = 0; iterador < clsGlobales.ProdSelCompuesto.GetLength(0); iterador++)
            {

                CadSQL = "";

                //Si esta seleccionado
                if (!(clsGlobales.ProdSelCompuesto[iterador, 0] == 0))
                {
                    LastIdDet = clsDataBD.RetornarUltimoId("ProductosCompuestos", "IdDetProductoCompuesto") + 1;

                    CadSQL = "INSERT INTO ProductosCompuestos (IdDetProductoCompuesto," +
                                                       "IdProdOrigen," +
                                                       "IdProdCompone," +
                                                       "Cantidad) Values ("
                                                       + LastIdDet + ","
                                                       + clsDataBD.RetornarUltimoId("Articulos", "IdArticulo") + ","
                                                       + clsGlobales.ProdSelCompuesto[iterador, 0] + ","
                                                       + clsGlobales.ProdSelCompuesto[iterador, 1] + ")";

                    //Guardar
                    clsDataBD.GetSql(CadSQL);
                }

            }

        }

        #endregion

        #region Metodo: ModificaCompProd ( Modifica la composicion de producto de productos 14-06 )

        //ELIMINA Y VUELVE A GUARDAR LA COMPOSICION DE PRODUCTO DE PRODUCTOS
        private void ModificaCompProd(clsArticulos NuevoArticulo)
        {

            string CadSQL = "";

            int LastIdDet = 0;

            //ELIMINAR COMPOSICION
            CadSQL = "DELETE FROM ProductosCompuestos WHERE IdProdOrigen = " + NuevoArticulo.IdArticulo;
            //EJCUTAR CADSQL
            clsDataBD.GetSql(CadSQL);

            //Recorrer la Matriz
            for (int iterador = 0; iterador < clsGlobales.ProdSelCompuesto.GetLength(0); iterador++)
            {

                CadSQL = "";

                //Si esta seleccionado
                if (!(clsGlobales.ProdSelCompuesto[iterador, 0] == 0))
                {
                    LastIdDet = clsDataBD.RetornarUltimoId("ProductosCompuestos", "IdDetProductoCompuesto") + 1;

                    CadSQL = "INSERT INTO ProductosCompuestos (IdDetProductoCompuesto," +
                                                       "IdProdOrigen," +
                                                       "IdProdCompone," +
                                                       "Cantidad) Values ("
                                                       + LastIdDet + ","
                                                       + NuevoArticulo.IdArticulo + ","
                                                       + clsGlobales.ProdSelCompuesto[iterador, 0] + ","
                                                       + clsGlobales.ProdSelCompuesto[iterador, 1] + ")";

                    //Guardar
                    clsDataBD.GetSql(CadSQL);
                }

            }

        }

        #endregion

        #region Metodo UpdateProductoComp

        /*************************************/
        /*Metodo    : UpdateProducto
         *Proposito : Update en Productos (Por composicion)
         *By        : Developer 
         *Date      : 17/11/16 updated
         *Parameters: None
         *Return    : Nothing
         ************************************/
        private void UpdateProductoComp()
        {
            String myCadena = "";

            try
            {
                //Preparar datos para guardar Productos
                clsProductos cProducto = new clsProductos();
                cProducto.IdArticulo = Convert.ToInt32(dgvArt.CurrentRow.Cells["IdArticulo"].Value);

                //Definir cadena string
                myCadena = "UPDATE Productos SET CostoAcumulado = 0"  + "," +
                                               " CostoInsumos = 0" +  "," +
                                               " CostoGastos = 0" +  "," +
                                               " IdAreaProduccion = 1" +  "," +
                                               " IdCoeficienteArticulo = " + Convert.ToInt32(this.cboCoeficiente.SelectedValue) + "," +
                                               " Tanda = 0" + 
                                               " WHERE IdArticulo = " + cProducto.IdArticulo;

                //Ejecutar modificacion del artículo
                clsDataBD.GetSql(myCadena);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        #endregion

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
                       DialogResult dlResult = MessageBox.Show("Desea Eliminar el " +  sTipo + ": " +   strArt + " ?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                        //Pongo el foco en la fila desde donde se hizo la llamada
                        PosicionarFocoFila();
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
            if (!(txtArticulo.Text==""))
            {
                CargarGrillaBusqueda(this.txtArticulo.Text, "Articulo");
            }
        }

        #endregion

        #region Eventos Click

        #region Evento Click boton Precio

        private void btnPrecio_Click(object sender, EventArgs e)
        {
            //Completar coeficiente
            if (cboCoeficiente.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar el Coeficiente!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboCoeficiente.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtPorcIVA.Text))
            {
                MessageBox.Show("Debe completar el campo '% IVA'!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPorcIVA.Focus();
                return;
            }

            //ES COMPOSICION DE SOLO PRODUCTOS
            if (this.chkSProd.Checked)
            {
                if (this.myEstado == "A")
                {
                    frmProductosCompuestos myForm = new frmProductosCompuestos(0, this.myEstado,txtPrecio, Convert.ToInt32(cboCoeficiente.SelectedValue),Convert.ToDouble(this.txtPorcIVA.Text));
                    myForm.ShowDialog();
                }
                else
                {
                    frmProductosCompuestos myForm = new frmProductosCompuestos(Convert.ToInt32(dgvArt.CurrentRow.Cells["IdArticulo"].Value), this.myEstado, txtPrecio, Convert.ToInt32(cboCoeficiente.SelectedValue),Convert.ToDouble(this.txtPorcIVA.Text));
                    myForm.ShowDialog(); 
                }
            }
            else
            {
                //ES COMPOSICION ESTANDARD
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
            string sNC = "N/C";

            for (int i = 0; i < dgvFilas; i++)
            {

                if (Convert.ToInt32(dgvArt[13, i].Value)==1)
                { sLleva = "Sí"; } else { sLleva = "No"; }

                if (Convert.ToInt32(dgvArt[14, i].Value) == 1)
                { sFactura = "Sí"; } else { sFactura = "No"; }

                if (nTipArt == 2)
                {
                    sNC = dgvArt[12, i].Value.ToString();
                }

                oDsArt.Tables["DtArt"].Rows.Add
                (new object[] { dgvArt[4,i].Value.ToString(),
                dgvArt[5,i].Value.ToString(),
                dgvArt[6,i].Value.ToString(),
                dgvArt[7,i].Value.ToString(),
                sLleva,
                sFactura,
                dgvArt[15,i].Value.ToString(),
                sNC,
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
            if (nTipArt==1)
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

                //Ver en Productos Compuestos
                myCadena = "Select count(*) as Elementos from ProductosCompuestos where IdProdCompone = " + Convert.ToInt32(row.Cells["IdArticulo"].Value);
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
                                                                + NuevoArticulo.UltimoProveedor + "',"
                                                                + NuevoArticulo.UltimaCompra + ","
                                                                + NuevoArticulo.LlevaStock + ","
                                                                + NuevoArticulo.Facturable + ","
                                                                + NuevoArticulo.Stock.ToString().Replace(",", ".") + ","
                                                                + NuevoArticulo.StockMinimo.ToString().Replace(",", ".") + ","
                                                                + NuevoArticulo.StockMaximo.ToString().Replace(",", ".") + ","
                                                                + NuevoArticulo.StockPuntoPedido.ToString().Replace(",", ".") + ","
                                                                + NuevoArticulo.PorcentajeIva.ToString().Replace(",", ".") + ","
                                                                + NuevoArticulo.Activo + "," 
                                                                + NuevoArticulo.incListaPre + "," 
                                                                + NuevoArticulo.incListaRes +  ","  
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
                                             " PrecioAnterior = " + NuevoArticulo.PrecioAnterior.ToString().Replace(",", ".") + "," +
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
                                             " IncListaRes = " + NuevoArticulo.incListaRes + "," +
                                              "CompIns = " + NuevoArticulo.CompIns + 
                                             " WHERE IdArticulo = " + NuevoArticulo.IdArticulo;

            //Ejecutar modificacion del artículo
            clsDataBD.GetSql(myCadena);
        }

        #endregion

        #region Metodo ModificaArticuloProducto

        /*************************************/
        /*Metodo    : ModificaArticuloProducto
         *Proposito : Update del Articulo (para producto) 
         *By        : Developer 
         *Date      : 17/11/16 updated
         *Parameters: None
         *Return    : Nothing
         ************************************/
        private void ModificaArticuloProducto(clsArticulos NuevoArticulo)
        {
            //Variable String
            String myCadena = "";

            try
            {

                //Cadena SQL
                myCadena = "UPDATE Articulos SET IdSubrubroArticulo = " + NuevoArticulo.IdSubrubroArticulo + "," +
                                                 " IdUnidadMedida = " + NuevoArticulo.IdUnidadMedida + "," +
                                                 " CodigoArticulo = '" + NuevoArticulo.CodigoArticulo + "'," +
                                                 " Articulo = '" + NuevoArticulo.Articulo + "'," +
                                                 " Unidades = " + NuevoArticulo.Unidades.ToString().Replace(",", ".") + "," +
                                                 " Precio = " + NuevoArticulo.Precio.ToString().Replace(",", ".") + "," +
                                                 " PrecioAnterior = " + NuevoArticulo.PrecioAnterior.ToString().Replace(",", ".") + "," +
                                                 " UltimoCostoCompra = " + NuevoArticulo.UltimoCostoCompra.ToString().Replace(",", ".") + "," +
                                                 " UltimoProveedor = '" + NuevoArticulo.UltimoProveedor + "'," +
                                                 " UltimaCompra = '" + NuevoArticulo.UltimaCompra + "'," +
                                                 " LlevaStock = " + NuevoArticulo.LlevaStock + "," +
                                                 " Facturable = " + NuevoArticulo.Facturable + "," +
                                                 " Stock = " + NuevoArticulo.Stock.ToString().Replace(",", ".") + "," +
                                                 " StockMinimo = " + NuevoArticulo.StockMinimo.ToString().Replace(",", ".") + "," +
                                                 " StockMaximo = " + NuevoArticulo.StockMaximo.ToString().Replace(",", ".") + "," +
                                                 " StockPuntoPedido = " + NuevoArticulo.StockPuntoPedido.ToString().Replace(",", ".") + "," +
                                                 " PorcentajeIva = " + NuevoArticulo.PorcentajeIva.ToString().Replace(",", ".") + "," +
                                                 " Activo = " + NuevoArticulo.Activo + "," +
                                                 " IncListaPre = " + NuevoArticulo.incListaPre + "," +
                                                 " IncListaRes = " + NuevoArticulo.incListaRes + "," + 
                                                 " chkSombreado = " + NuevoArticulo.chkSombreado + "," +
                                                 " rbtColor = " + NuevoArticulo.rbtColor + "," +
                                                 " chkSProd = " + NuevoArticulo.chkSProd + 
                                                 " WHERE IdArticulo = " + NuevoArticulo.IdArticulo;

                //Ejecutar modificacion del artículo
                clsDataBD.GetSql(myCadena);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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

        #region Metodo GuardarNuevoProducto

        /*************************************/
        /*Metodo    : GuardarNuevoProducto
         *Proposito : Insert en Productos (Por composicion)
         *By        : Developer 
         *Date      : 13/11/16 updated
         *Parameters: None
         *Return    : Nothing
         ************************************/
        private void GuardarNuevoProducto()
        {
            //Variable
            string myCadena = "";

            try
            {

                //Preparar datos para guardar Productos
                clsProductos cProducto = new clsProductos();
                cProducto.IdArticulo = clsDataBD.RetornarUltimoId("Articulos", "IdArticulo");
                cProducto.IdAreaProduccion = Convert.ToInt32(clsGlobales.ProductoDatos[5]);
                cProducto.IdCoeficienteArticulo = Convert.ToInt32(cboCoeficiente.SelectedValue);
                cProducto.CostoAcumulado = clsGlobales.ProductoDatos[1];
                cProducto.CostoGastos = clsGlobales.ProductoDatos[3];
                cProducto.CostoInsumos = clsGlobales.ProductoDatos[2];
                cProducto.Tanda = Convert.ToInt32(clsGlobales.ProductoDatos[0]);

                //Cadena SQL para insertar registro en Productos
                myCadena = "INSERT INTO Productos (IdArticulo," +
                                                "CostoAcumulado," +
                                                "CostoInsumos," +
                                                "CostoGastos," +
                                                "IdAreaProduccion," +
                                                "IdCoeficienteArticulo," +
                                                "Tanda ) values ("
                                                                               + cProducto.IdArticulo + ","
                                                                               + cProducto.CostoAcumulado.ToString().Replace(",", ".") + ","
                                                                               + cProducto.CostoInsumos.ToString().Replace(",", ".") + ","
                                                                               + cProducto.CostoGastos.ToString().Replace(",", ".") + ","
                                                                               + cProducto.IdAreaProduccion + ","
                                                                               + cProducto.IdCoeficienteArticulo + ","
                                                                               + cProducto.Tanda + ")";
                //Guardar en Insumos
                clsDataBD.GetSql(myCadena);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        #endregion

        #region Metodo UpdateProducto

        /*************************************/
        /*Metodo    : UpdateProducto
         *Proposito : Update en Productos (Por composicion)
         *By        : Developer 
         *Date      : 17/11/16 updated
         *Parameters: None
         *Return    : Nothing
         ************************************/
        private void UpdateProducto()
        {
            String myCadena = "";

            try
            {
                //Preparar datos para guardar Productos
                clsProductos cProducto = new clsProductos();
                cProducto.IdArticulo = Convert.ToInt32(dgvArt.CurrentRow.Cells["IdArticulo"].Value);

                if (clsGlobales.ProductoDatos[5] != 0)
                {
                    cProducto.IdAreaProduccion = Convert.ToInt32(clsGlobales.ProductoDatos[5]);
                    cProducto.IdCoeficienteArticulo = Convert.ToInt32(cboCoeficiente.SelectedValue);
                    cProducto.CostoAcumulado = clsGlobales.ProductoDatos[1];
                    cProducto.CostoGastos = clsGlobales.ProductoDatos[3];
                    cProducto.CostoInsumos = clsGlobales.ProductoDatos[2];
                    cProducto.Tanda = Convert.ToInt32(clsGlobales.ProductoDatos[0]);
                }
                else
                {
                    cProducto.IdAreaProduccion = Convert.ToInt32(clsGlobales.ProductoDatosST[5]);
                    cProducto.IdCoeficienteArticulo = Convert.ToInt32(cboCoeficiente.SelectedValue);
                    cProducto.CostoAcumulado = Double.Parse(clsGlobales.ProductoDatosST[1]);
                    cProducto.CostoGastos = Double.Parse(clsGlobales.ProductoDatosST[3]);
                    cProducto.CostoInsumos = Double.Parse(clsGlobales.ProductoDatosST[2]);
                    cProducto.Tanda = Convert.ToInt32(clsGlobales.ProductoDatosST[0]);
                }

                //Definir cadena string
                myCadena = "UPDATE Productos SET CostoAcumulado = " + cProducto.CostoAcumulado.ToString().Replace(",", ".") + "," +
                                               " CostoInsumos = " + cProducto.CostoInsumos.ToString().Replace(",", ".") + "," +
                                               " CostoGastos = " + cProducto.CostoGastos.ToString().Replace(",", ".") + "," +
                                               " IdAreaProduccion = " + cProducto.IdAreaProduccion + "," +
                                               " IdCoeficienteArticulo = " + cProducto.IdCoeficienteArticulo + "," +
                                               " Tanda = " + cProducto.Tanda + 
                                               " WHERE IdArticulo = " + cProducto.IdArticulo;

                //Ejecutar modificacion del artículo
                clsDataBD.GetSql(myCadena);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        #endregion

        #region Metodo GuardarComposicionProducto

        /*************************************/
        /*Metodo    : GuardarComposicionProducto
         *Proposito : Guarda composicion producto
         *            en ProductoInsumo y 
         *            ProductoGastosFijos
         *By        : Developer 
         *Date      : 13/11/16 updated
         *Parameters: None
         *Return    : Nothing
         ************************************/
        private void GuardarComposicionProducto()
        {

            string CadSQL = "";


            //Recorrer la Matriz
            for (int iterador = 0; iterador < clsGlobales.ProductoComposicion.GetLength(0); iterador++)
            {

                CadSQL = "";

                if (clsGlobales.ProductoComposicion[iterador, 6] == "G")
                {

                    CadSQL = "INSERT INTO ProductosGastosFijos (IdProducto," +
                                                       "IdGastoFijo," +
                                                       "Cantidad," +
                                                       "Costo," +
                                                       "Activo) Values ("
                                                       + clsDataBD.RetornarUltimoId("Productos", "IdProducto") + ","
                                                       + clsGlobales.ProductoComposicion[iterador, 7] + ","
                                                       + clsGlobales.ProductoComposicion[iterador, 2] + ","
                                                       + clsGlobales.ProductoComposicion[iterador, 4] + ","
                                                       + 1 + ")";

                }
                else
                {

                    CadSQL = "INSERT INTO ProductosInsumos (IdProducto," +
                                                       "IdInsumo," +
                                                       "Cantidad," +
                                                       "Costo," +
                                                       "Activo) Values ("
                                                       + clsDataBD.RetornarUltimoId("Productos", "IdProducto") + ","
                                                       + clsGlobales.ProductoComposicion[iterador, 8] + ","
                                                       + clsGlobales.ProductoComposicion[iterador, 2] + ","
                                                       + clsGlobales.ProductoComposicion[iterador, 4] + ","
                                                       + 1 + ")";
                }


                //Guardar
                clsDataBD.GetSql(CadSQL);

            }

        }

        #endregion

        #region Metodo: UpdateComposicionProducto

        /*************************************/
        /* Metodo     : UpdateComposicionProducto
         * Proposito  : Update Composicion de Productos
         * By         : Developer 
         * Date       : 21/11/16
         * Parameters : None
         * Return     : Nothing
         ************************************/
        private void UpdateComposicionProducto()
        {

            string CadSQL = "";

            ////////////////////////////////////
            //Quitar insumos y gastos a eliminar
            ////////////////////////////////////
            for (int iterador = 0; iterador < clsGlobales.ProductosEliminados.GetLength(0); iterador++)
            {
                if (clsGlobales.ProductosEliminados[iterador, 1] == "I")
                {
                    //Eliminar de Productos Insumos
                    CadSQL = "DELETE FROM ProductosInsumos WHERE IdProducto = " + Convert.ToInt32(dgvArt.CurrentRow.Cells["IdProducto"].Value) +
                         " AND IdInsumo = " + clsGlobales.ProductosEliminados[iterador, 0];

                    //Guardar
                    clsDataBD.GetSql(CadSQL);

                }
                else
                {
                    //Eliminar de Productos Insumos
                    CadSQL = "DELETE FROM ProductosGastosFijos WHERE IdProducto = " + Convert.ToInt32(dgvArt.CurrentRow.Cells["IdProducto"].Value) +
                         " AND IdGastoFijo = " + clsGlobales.ProductosEliminados[iterador, 0];

                }

                //Ejecutar
                clsDataBD.GetSql(CadSQL);

            }        

             ////////////////////////////////////////////////////////////////////////////
            //Recorrer la Matriz con los ELEMENTOS DE LA COMPOSICION O NUEVA COMPOSICION
            ////////////////////////////////////////////////////////////////////////////
            for (int iterador = 0; iterador < clsGlobales.ProductoComposicion.GetLength(0); iterador++)
            {

                CadSQL = "";

                //GASTO
                if (clsGlobales.ProductoComposicion[iterador, 6] == "G")
                {

                   //Existe?
                    CadSQL = "Select count(*) as nElementos from ProductosGastosFijos Where IdProducto = " + Convert.ToInt32(dgvArt.CurrentRow.Cells["IdProducto"].Value) +
                             " AND IdGastoFijo = " + clsGlobales.ProductoComposicion[iterador, 7];
                   
                  //Si hay elementos, realizar el  UPDATE.  
                    if (ExisteElemento(CadSQL) > 0)
                    {
                        //Cadena SQL
                        CadSQL = "UPDATE ProductosGastosFijos SET IdGastoFijo = " + clsGlobales.ProductoComposicion[iterador, 7] + "," +
                                                  " Cantidad = " + clsGlobales.ProductoComposicion[iterador, 2] + "," +
                                                  " Costo = " + clsGlobales.ProductoComposicion[iterador, 4] + "," +
                                                  " Activo = " + 1 +
                                                  " WHERE IdProducto = " + Convert.ToInt32(dgvArt.CurrentRow.Cells["IdProducto"].Value) + " AND IdGastoFijo = " + clsGlobales.ProductoComposicion[iterador, 7];

                    }
                    else
                    {
                        CadSQL = "INSERT INTO ProductosGastosFijos (IdProducto," +
                                                      "IdGastoFijo," +
                                                      "Cantidad," +
                                                      "Costo," +
                                                      "Activo) Values ("
                                                      + Convert.ToInt32(dgvArt.CurrentRow.Cells["IdProducto"].Value) + ","
                                                      + clsGlobales.ProductoComposicion[iterador, 7] + ","
                                                      + clsGlobales.ProductoComposicion[iterador, 2] + ","
                                                      + clsGlobales.ProductoComposicion[iterador, 4] + ","
                                                      + 1 + ")";

                    }

                  //Guardar
                    clsDataBD.GetSql(CadSQL);


                }     /////////
                else //Insumos
                {    /////////

                  //Existe?
                    CadSQL = "Select count(*) as nElementos from ProductosInsumos Where IdProducto = " + Convert.ToInt32(dgvArt.CurrentRow.Cells["IdProducto"].Value) +
                            " AND IdInsumo = " + clsGlobales.ProductoComposicion[iterador, 8];

                  //Si hay elementos, realizar el  UPDATE.  
                    if (ExisteElemento(CadSQL) > 0)
                    {
                        //Cadena SQL
                        CadSQL = "UPDATE ProductosInsumos SET IdInsumo = " + clsGlobales.ProductoComposicion[iterador, 8] + "," +
                                                  " Cantidad = " + clsGlobales.ProductoComposicion[iterador, 2] + "," +
                                                  " Costo = " + clsGlobales.ProductoComposicion[iterador, 4] + "," +
                                                  " Activo = " + 1 +
                                                  " WHERE IdProducto = " + Convert.ToInt32(dgvArt.CurrentRow.Cells["IdProducto"].Value) + " AND IdInsumo = " + clsGlobales.ProductoComposicion[iterador, 8];

                    }
                    else
                    {
                        //Sentencia SQL
                         CadSQL = "INSERT INTO ProductosInsumos (IdProducto," +
                                                           "IdInsumo," +
                                                           "Cantidad," +
                                                           "Costo," +
                                                           "Activo) Values ("
                                                           + Convert.ToInt32(dgvArt.CurrentRow.Cells["IdProducto"].Value) + ","
                                                           + clsGlobales.ProductoComposicion[iterador, 8] + ","
                                                           + clsGlobales.ProductoComposicion[iterador, 2] + ","
                                                           + clsGlobales.ProductoComposicion[iterador, 4] + ","
                                                           + 1 + ")";


                    }

                    //Guardar ///////////////
                    clsDataBD.GetSql(CadSQL);

                }

            }

        }

        #endregion

        #region Metodo: ExisteElemento

        private int ExisteElemento(string p_cadSQL)
        {
            //Variable de retorno
                int nElementos=0;

            //Crear el DataTable con la consulta.
                DataTable mDataTable = clsDataBD.GetSql(p_cadSQL);

            //Verificar si hay elementos en la consulta.
                foreach (DataRow row in mDataTable.Rows)
                {
                    nElementos = Convert.ToInt32(row["nElementos"]);  
                }

            return nElementos;
        }

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

        #region Eventos TextChanged

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            if (!(txtCodigo.Text == ""))
            {
                CargarGrillaBusqueda(this.txtCodigo.Text, "CodigoArticulo");
            }
        }

        #endregion

        #region Eventos Click

        private void txtPrecio_Click(object sender, EventArgs e)
        {
            txtPrecio.SelectAll();
        }

        private void txtUltCosto_Click(object sender, EventArgs e)
        {
            txtUltCosto.SelectAll();
        }

        private void txtUltCosto_Enter(object sender, EventArgs e)
        {
            txtUltCosto.SelectionStart = 0;
            txtUltCosto.SelectionLength = txtUltCosto.Text.Length;
        }

        private void txtPrecio_Enter(object sender, EventArgs e)
        {
            txtPrecio.SelectionStart = 0;
            txtPrecio.SelectionLength = txtPrecio.Text.Length;
        }

        #endregion

        #region Chk Checked Changed Events

        private void chkListaGral_CheckedChanged(object sender, EventArgs e)
        {
            if (this.myEstado == "A" || myEstado == "M")
            {
                if (this.chkListaGral.Checked || this.chkListaRes.Checked )
                {
                    this.chkSombreado.Enabled = true;
                    this.chkSombreado.TabStop = true;

                }
                else
                {
                    if (this.chkListaGral.Checked == false && this.chkListaRes.Checked == false)
                    {
                        this.chkSombreado.Enabled = false;
                        this.chkSombreado.TabStop = false;

                    }
                }
            }
        }

        private void chkListaRes_CheckedChanged(object sender, EventArgs e)
        {

          
        }

        private void chkSombreado_CheckedChanged(object sender, EventArgs e)
        {
            //SOMBREADO?
            if (this.myEstado == "A" || myEstado == "M")
            {
                if (chkSombreado.Checked)
                {
                    this.rbtNaranja.Enabled = true;
                    this.rbtNaranja.TabStop = true;
                    this.rbtVerde.Enabled = true;
                    this.rbtVerde.TabStop = true;
                }

                else
                {
                    this.rbtNaranja.Checked = true;
                    this.rbtNaranja.Enabled = false;
                    this.rbtNaranja.TabStop = false;
                    this.rbtVerde.Enabled = false;
                    this.rbtVerde.TabStop = false;

                }
            }
        }

        #endregion

        #endregion

        private void chkSProd_CheckedChanged(object sender, EventArgs e)
        {
            if (this.myEstado == "A" || this.myEstado == "M")
            {
                //Si quita la marca...
                if (chkSProd.Checked == false)
                {
                    if (this.myEstado == "A")
                    {
                      // Redimensiono el tamaño de la matriz
                      clsGlobales.ProdSelCompuesto = (double[,])clsValida.ResizeMatriz(clsGlobales.ProdSelCompuesto, new int[] { 0, 2 });

                      //Inhabilitar
                      txtPrecio.TabStop = false;
                      txtPrecio.Text = "";
                      txtPrecio.Enabled = false;
                      btnPrecio.Visible = true;

                    }
                }
                else
                {
                    if (this.nTipArt == 2)
                    {
                        if (this.myEstado == "A" || this.myEstado == "M")
                        {
                            txtPrecio.TabStop = false;
                            txtPrecio.Enabled = false;
                            btnPrecio.Visible = true;
                        }
                    }
                }
            }
        }

        private void txtStock_Click(object sender, EventArgs e)
        {
            txtStock.SelectionStart = 0;
            txtStock.SelectionLength = txtStock.Text.Length;
        }

        private void txtStockMin_Click(object sender, EventArgs e)
        {
            txtStockMin.SelectionStart = 0;
            txtStockMin.SelectionLength = txtStockMin.Text.Length;
        }

        private void txtStockMax_Click(object sender, EventArgs e)
        {
            txtStockMax.SelectionStart = 0;
            txtStockMax.SelectionLength = txtStockMax.Text.Length;
        }

        private void txtStockPed_Click(object sender, EventArgs e)
        {
            txtStockPed.SelectionStart = 0;
            txtStockPed.SelectionLength = txtStockPed.Text.Length;
        }

        private void txtPorcIVA_Click(object sender, EventArgs e)
        {
            txtPorcIVA.SelectionStart = 0;
            txtPorcIVA.SelectionLength = txtPorcIVA.Text.Length;
        }

        private void txtStock_Enter(object sender, EventArgs e)
        {
            txtStock.SelectionStart = 0;
            txtStock.SelectionLength = txtStock.Text.Length;
        }

        private void txtStockMin_Enter(object sender, EventArgs e)
        {
            txtStockMin.SelectionStart = 0;
            txtStockMin.SelectionLength = txtStockMin.Text.Length;
        }

        private void txtStockMax_Enter(object sender, EventArgs e)
        {
            txtStockMax.SelectionStart = 0;
            txtStockMax.SelectionLength = txtStockMax.Text.Length;
        }

        private void txtStockPed_Enter(object sender, EventArgs e)
        {
            txtStockPed.SelectionStart = 0;
            txtStockPed.SelectionLength = txtStockPed.Text.Length;
        }

        private void txtPorcIVA_Enter(object sender, EventArgs e)
        {
            txtPorcIVA.SelectionStart = 0;
            txtPorcIVA.SelectionLength = txtPorcIVA.Text.Length;
        }

        private void chkListaRes_CheckedChanged_1(object sender, EventArgs e)
        {
            if (this.myEstado == "A" || myEstado == "M")
            {
                if (this.chkListaGral.Checked || this.chkListaRes.Checked)
                {
                    this.chkSombreado.Enabled = true;
                    this.chkSombreado.TabStop = true;

                }
                else
                {
                    if (this.chkListaGral.Checked == false && this.chkListaRes.Checked == false)
                    {
                        this.chkSombreado.Enabled = false;
                        this.chkSombreado.TabStop = false;

                    }
                }
            }
        }

        private void chkcInsumos_CheckedChanged(object sender, EventArgs e)
        {
            if (!(this.myEstado == "C"))
            {
                /*Si lo chequea, habilitar controles*/
                if (chkcInsumos.Checked)
                {
                    this.btnCompIns.TabStop = true;
                    this.btnCompIns.Visible = true;
                }
                else
                {
                    this.btnCompIns.TabStop = false;
                    this.btnCompIns.Visible = false;
                  
                }
            }
            else
            {

                if (!(chkcInsumos.Checked))
                {
                    this.btnCompIns.TabStop = false;
                    this.btnCompIns.Visible = false;

                    //Vaciar Vectores
                    // this.VaciarVectorGlobales();
                }
                
            }
        }

        private void btnCompIns_Click(object sender, EventArgs e)
        {
            if (this.chkcInsumos.Checked)
            {
                if (this.myEstado == "A")
                {
                    //Show del formulario de Insumos Composición en Alta
                    frmInsumosComposicion myForm = new frmInsumosComposicion(this.myEstado, 0, this.txtCantArt);
                    myForm.ShowDialog();
                }
                else
                {
                    //Flag a .F. inicialmente dado que aún no modificó nada.
                    clsGlobales.bCambio = false; // .N. 13-06-2018

                    //Show del formulario de Insumos Composicion
                    frmInsumosComposicion myForm = new frmInsumosComposicion(this.myEstado, Convert.ToInt32(dgvArt.CurrentRow.Cells["IdArticulo"].Value), this.txtCantArt);
                    myForm.ShowDialog();
                }

                //Recorrer y totalizar
                double CostoFinal = 0;

                for (int iterador = 0; iterador < clsGlobales.ProductoComposicion.GetLength(0); iterador++)
                {
                    CostoFinal+= Convert.ToDouble(clsGlobales.ProductoComposicion[iterador, 5]);
                }

                //Costo final dividido cantidad articulos tanda
                if (clsGlobales.cValida.IsNumeric(txtCantArt.Text))
                {
                    CostoFinal = CostoFinal / Convert.ToInt32(txtCantArt.Text);
                }

                if (this.myEstado == "M") //Si modifica y cambio costo final, establecer flag a .T.
                {
                    if (Convert.ToDouble(CostoFinal.ToString("#0.00000")) != Convert.ToDouble(txtPrecio.Text))
                    {
                        clsGlobales.bCambio = true;
                    }
                    else
                    {
                        clsGlobales.bCambio = false;
                    }
                     
                }
                                
                //Asignar Total
                txtPrecio.Text = CostoFinal.ToString("#0.00000");
            }
        }

        private void txtUnidad_Leave(object sender, EventArgs e)
        {
            //Esta vacio?
            if (string.IsNullOrEmpty(txtUnidad.Text))
            {
                return;
            }

            //Recorrer y totalizar
            //double CostoFinal = 0;

            //for (int iterador = 0; iterador < clsGlobales.ProductoComposicion.GetLength(0); iterador++)
            //{
            //    CostoFinal += Convert.ToDouble(clsGlobales.ProductoComposicion[iterador, 5]);
            //}

            //Costo final dividido cantidad unidades
           //  CostoFinal = CostoFinal / Convert.ToDouble(txtUnidad.Text);

            //Asignar Total
           //txtPrecio.Text = CostoFinal.ToString("#0.00000");
        }

    }

  }

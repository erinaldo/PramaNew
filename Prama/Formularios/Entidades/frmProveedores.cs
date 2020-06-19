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
    public partial class frmProveedores : Form
    {

        #region Declaracion Variables Nivel Formulario

        //Otras
        string myEstado = "C";
        int indexFila = 0;
        bool BanderaCombo = false;
        bool bSearch = false;
        int IdProvSearch = 0;

        #endregion

        public frmProveedores()
        {
            InitializeComponent();
        }

        #region Métodos del Formulario
        
        #region Método para activar los botones del formulario
        //--------------------------------------------------------------
        //ACTIVAR BOTONES  
        //SEGUN EL ESTADO (A, M, C) - MUESTRA U OCULTA BOTONES
        //--------------------------------------------------------------
        private void ActivarBotones()
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
                    this.btnBorrar.TabStop = false;
                    this.btnBorrar.Visible = false;
                    this.btnImprimir.TabStop = false;
                    this.btnImprimir.Visible = false;
                    return;
                case "C":
                    this.btnAgregar.TabStop = true;
                    this.btnAgregar.Visible = true;
                    this.btnModificar.TabStop = true && (dgvProv.RowCount != 0);
                    this.btnModificar.Visible = true && (dgvProv.RowCount != 0);
                    this.btnBuscar.TabStop = true && (dgvProv.RowCount != 0);
                    this.btnBuscar.Visible = true && (dgvProv.RowCount != 0);
                    this.btnAceptar.TabStop = false;
                    this.btnAceptar.Visible = false;
                    this.btnCancelar.TabStop = false;
                    this.btnCancelar.Visible = false;
                    this.btnSalir.TabStop = true;
                    this.btnSalir.Visible = true;
                    // Si el nivel del usuario es el 5, habilito el botón borrar
                    if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja)
                    {
                        this.btnBorrar.TabStop = true && (dgvProv.RowCount != 0);
                        this.btnBorrar.Visible = true && (dgvProv.RowCount != 0);
                    }
                    else
                    {
                        this.btnBorrar.TabStop = false;
                        this.btnBorrar.Visible = false;
                    }
                    this.btnImprimir.TabStop = true && (dgvProv.RowCount != 0);
                    this.btnImprimir.Visible = true && (dgvProv.RowCount != 0);
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
        private void HabilitarControles()
        {
            //Usamos un switch para evaluar en que estado estamos 
            //A = Alta, M = Modificacion, "C" = En espera
            switch (this.myEstado)
            {
                case "A":
                case "M":
                    this.txtRSoc.TabStop = true;
                    this.txtRSoc.Enabled = true;
                    this.txtFantasia.TabStop = true;
                    this.txtFantasia.Enabled = true;

                    this.txtDir.TabStop = true;
                    this.txtDir.Enabled = true;
                    this.cboProvincia.TabStop = true;
                    this.cboProvincia.Enabled = true;

                    this.cboLocalidad.TabStop = true & (cboProvincia.SelectedIndex > -1);
                    this.cboLocalidad.Enabled = true & (cboProvincia.SelectedIndex > -1);
                    this.cboLocalidad.Visible = true;

                    this.txtLoc.TabStop = false;
                    this.txtLoc.Enabled = false;
                    this.txtLoc.Visible = false;

                    this.dtIA.TabStop = true;
                    this.dtIA.Enabled = true;
                    this.txtCUIT.TabStop = true;
                    this.txtCUIT.Enabled = true;
                    this.txtIB.TabStop = true;
                    this.txtIB.Enabled = true;
                    this.cboCondIVA.TabStop = true;
                    this.cboCondIVA.Enabled = true;
                    this.cboCondCompra.TabStop = true;
                    this.cboCondCompra.Enabled = true;

                    this.txtTel.TabStop = true;
                    this.txtTel.Enabled = true;
                    this.txtFax.TabStop = true;
                    this.txtFax.Enabled = true;
                    this.txtCelular.TabStop = true;
                    this.txtCelular.Enabled = true;
                    this.txtMail.TabStop = true;
                    this.txtMail.Enabled = true;
                    this.txtWeb.TabStop = true;
                    this.txtWeb.Enabled = true;

                    this.txtSaldo.TabStop = false;
                    this.txtSaldo.Enabled = false;

                    this.txtContacto.TabStop = true;
                    this.txtContacto.Enabled = true;
                    this.txtCelularContacto.TabStop = true;
                    this.txtCelularContacto.Enabled = true;
                    this.txtMailContacto.TabStop = true;
                    this.txtMailContacto.Enabled = true;

                    this.txtAlicuota.TabStop = false;
                    this.txtAlicuota.Enabled = false;

                    this.txtObs.TabStop = true;
                    this.txtObs.Enabled = true;

                    this.chkInsumos.TabStop = true;
                    this.chkInsumos.Enabled = true;
                    this.chkProductos.TabStop = true;
                    this.chkProductos.Enabled = true;

                    dgvProv.TabStop = false;
                    dgvProv.Enabled = false;

                    this.txtObs.Height = 44;
                    this.gpbBusquedas.Visible = false;

                    break;
                case "B":
                    this.txtRSoc.TabStop = false;
                    this.txtRSoc.Enabled = false;
                    this.txtFantasia.TabStop = false;
                    this.txtFantasia.Enabled = false;

                    this.txtSaldo.TabStop = false;
                    this.txtSaldo.Enabled = false;

                    this.txtDir.TabStop = false;
                    this.txtDir.Enabled = false;

                    this.cboLocalidad.TabStop = false;
                    this.cboLocalidad.Enabled = false;
                    this.cboLocalidad.Visible = false;

                    this.cboProvincia.TabStop = false;
                    this.cboProvincia.Enabled = false;

                    this.dtIA.TabStop = false;
                    this.dtIA.Enabled = false;
                    this.txtCUIT.TabStop = false;
                    this.txtCUIT.Enabled = false;
                    this.txtIB.TabStop = false;
                    this.txtIB.Enabled = false;
                    this.cboCondIVA.TabStop = false;
                    this.cboCondIVA.Enabled = false;
                    this.cboCondCompra.TabStop = false;
                    this.cboCondCompra.Enabled = false;

                    this.txtTel.TabStop = false;
                    this.txtTel.Enabled = false;
                    this.txtFax.TabStop = false;
                    this.txtFax.Enabled = false;
                    this.txtCelular.TabStop = false;
                    this.txtCelular.Enabled = false;
                    this.txtMail.TabStop = false;
                    this.txtMail.Enabled = false;
                    this.txtWeb.TabStop = false;
                    this.txtWeb.Enabled = false;

                    this.txtAlicuota.TabStop = false;
                    this.txtAlicuota.Enabled = false;

                    this.txtContacto.TabStop = false;
                    this.txtContacto.Enabled = false;
                    this.txtCelularContacto.TabStop = false;
                    this.txtCelularContacto.Enabled = false;
                    this.txtMailContacto.TabStop = false;
                    this.txtMailContacto.Enabled = false;
                    this.txtObs.TabStop = false;
                    this.txtObs.Enabled = false;

                    this.chkInsumos.TabStop = false;
                    this.chkInsumos.Enabled = false;
                    this.chkProductos.TabStop = false;
                    this.chkProductos.Enabled = false;


                    txtLoc.TabStop = false;
                    txtLoc.Enabled = false;
                    txtLoc.Visible = true;

                    this.dgvProv.TabStop = true && (dgvProv.RowCount > 0);
                    this.dgvProv.Enabled = true && (dgvProv.RowCount > 0);

                    this.txtObs.Height = 44;
                    this.gpbBusquedas.Visible = true;

                    break;
                case "C":
                    this.txtRSoc.TabStop = false;
                    this.txtRSoc.Enabled = false;
                    this.txtFantasia.TabStop = false;
                    this.txtFantasia.Enabled = false;

                    this.txtDir.TabStop = false;
                    this.txtDir.Enabled = false;

                    this.txtSaldo.TabStop = false;
                    this.txtSaldo.Enabled = false;

                    this.cboLocalidad.TabStop = false;
                    this.cboLocalidad.Enabled = false;
                    this.cboLocalidad.Visible = false;

                    this.cboProvincia.TabStop = false;
                    this.cboProvincia.Enabled = false;

                    this.dtIA.TabStop = false;
                    this.dtIA.Enabled = false;
                    this.txtCUIT.TabStop = false;
                    this.txtCUIT.Enabled = false;
                    this.txtIB.TabStop = false;
                    this.txtIB.Enabled = false;
                    this.cboCondIVA.TabStop = false;
                    this.cboCondIVA.Enabled = false;
                    this.cboCondCompra.TabStop = false;
                    this.cboCondCompra.Enabled = false;

                    this.txtTel.TabStop = false;
                    this.txtTel.Enabled = false;
                    this.txtFax.TabStop = false;
                    this.txtFax.Enabled = false;
                    this.txtCelular.TabStop = false;
                    this.txtCelular.Enabled = false;
                    this.txtMail.TabStop = false;
                    this.txtMail.Enabled = false;
                    this.txtWeb.TabStop = false;
                    this.txtWeb.Enabled = false;

                    this.txtAlicuota.TabStop = false;
                    this.txtAlicuota.Enabled = false;

                    this.txtContacto.TabStop = false;
                    this.txtContacto.Enabled = false;
                    this.txtCelularContacto.TabStop = false;
                    this.txtCelularContacto.Enabled = false;
                    this.txtMailContacto.TabStop = false;
                    this.txtMailContacto.Enabled = false;
                    this.txtObs.TabStop = false;
                    this.txtObs.Enabled = false;

                    this.chkInsumos.TabStop = false;
                    this.chkInsumos.Enabled = false;
                    this.chkProductos.TabStop = false;
                    this.chkProductos.Enabled = false;


                    txtLoc.TabStop = false;
                    txtLoc.Enabled = false;
                    txtLoc.Visible = true;

                    this.dgvProv.TabStop = true && (dgvProv.RowCount > 0);
                    this.dgvProv.Enabled = true && (dgvProv.RowCount > 0);

                    this.txtObs.Height = 44;
                    this.gpbBusquedas.Visible = false;

                    break;
            }
        }
        #endregion

        #region Método para limpiar los controles del formulario
        //LIMPIA LA PROPIEDAD TEXT DE LOS CONTROLES DEL FORMULARIO. N.
        private void LimpiarControlesForm()
        {

            foreach (Control c in this.Controls)
    
            {

              //Textbox  
              if (c is TextBox)  {  c.Text = ""; }

             //Checked
              if (c is CheckBox)
              {
                  CheckBox cc = (CheckBox)c;
                  cc.Checked = false;
              }

             //ComboBox       
             if ( c is ComboBox)
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
                    dtp.Text = DateTime.Today.ToShortDateString();
                }         
            }

          }               

        }
        #endregion

        #region Método para cargar las Provincias al cboProvincias

        private void CargarProvincias()
        {
            // Cargo el combo de las provincias
            clsDataBD.CargarCombo(cboProvincia, "Provincias", "Provincia", "IdProvincia");
            // Dejo vacío el combo
            cboProvincia.SelectedIndex = -1;

        }
        #endregion
     
        #region Método para cargar Condicion IVA al MultiComboBox

        private void CargarCondicionIVA()
        {
            // Cargo el combo de Condicion IVA
            clsDataBD.CargarCombo(cboCondIVA, "CondicionesIVA", "CondicionIVA", "IdCondicionIVA");
            // Dejo vacío el combo
            cboCondIVA.SelectedIndex = -1;

        }
        #endregion

        #region Método para cargar Condicion Compra al MultiComboBox

        private void CargarCondicionCompra()
        {
            // Cargo el combo de Condicion Compra
            clsDataBD.CargarCombo(cboCondCompra, "CondicionesCompra", "CondicionCompra", "IdCondicionCompra");
            // Dejo vacío el combo
            cboCondCompra.SelectedIndex = -1;

        }
        #endregion

        #region Método para cargar la grilla

        private void getCargarGrilla()
        {
            // Cadena SQL 
            string myCadena = "select * from Vista_Proveedores_LocaliProv where Activo = 1";
            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            mDtTable = clsDataBD.GetSql(myCadena);
            // Evito que el dgv genere columnas automáticas
            dgvProv.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvProv.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvProv.Rows.Count;
            // Posiciono la grilla en la última fila, si hay
            if (Filas > 0)
            {
               //Posicionamiento grilla
                int r = dgvProv.CurrentCell.RowIndex;
                int c = dgvProv.CurrentCell.ColumnIndex;
                dgvProv.CurrentCell = dgvProv.Rows[r].Cells[c]; 
            }

            //Mostrar datos  
            this.getMostrarDatos();

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
                myCadena = "select * from Vista_Proveedores_LocaliProv where Activo = 1";

                //.F.
                bSearch = false;
            }
            else
            {
                // Cadena SQL 
                myCadena = "select * from Vista_Proveedores_LocaliProv where " + Campo + " like '" + Buscar + "%'  and Activo = 1 order by " + Campo;

                //.T.
                bSearch = true;
            }

            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            mDtTable = clsDataBD.GetSql(myCadena);
            // Evito que el dgv genere columnas automáticas
            dgvProv.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvProv.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvProv.Rows.Count;
            // Posiciono la grilla en la última fila, si hay
            if (Filas > 0)
            {
               //Posicionamiento grilla
                int r = dgvProv.CurrentCell.RowIndex;
                int c = dgvProv.CurrentCell.ColumnIndex;
                dgvProv.CurrentCell = dgvProv.Rows[r].Cells[c]; 
 
            }

            //Mostrar datos  
            getMostrarDatos();
        }

        #endregion

        #region Metodo: setComboLocalidad()
        /***********************************
         * Metodo       : setComboLocalidad
         * Proposito    : Cargar el combo de localidades a partir de un Id de Provincia dado.
         * Retorna      : Nada
         * Parametros   : Ninguno
         * Autor        : N.
         * ********************************/
        private void setComboLocalidad()
        {
            //Clean Combobox
            cboLocalidad.DataSource = null;
            cboLocalidad.DataBindings.Clear();

            // Cargo el combo de las Localidades N.
            int iProvSel = Convert.ToInt32(cboProvincia.SelectedValue);
            clsDataBD.CargarComboStoreProcedure(cboLocalidad, "CargarLocalidades", iProvSel, "Localidad", "IdLocalidad");

            //Establecer el valor                       
            DataGridViewRow row = dgvProv.CurrentRow;
            cboLocalidad.SelectedValue = Convert.ToInt32(row.Cells["IdLocalidad"].Value.ToString());
        }
        #endregion

        #region Metodo: getMostrarDatos()

        //************************************************************
        //Metodo    : getMostrarDatos 
        //Fecha     : 22-09-2016
        //Autor     : N
        //Proposito : Mostrar los datos de la grilla en los controles
        //************************************************************
        private void getMostrarDatos()
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvProv.RowCount == 0)
            {
                this.LimpiarControlesForm();
                return;
            }
            else
            {
                //TRAER LA FILA ACTUAL. N.
                DataGridViewRow row = dgvProv.CurrentRow;

                txtRSoc.Text = row.Cells["RazonSocial"].Value.ToString();
                txtFantasia.Text = row.Cells["NombreFantasia"].Value.ToString();

                cboCondIVA.SelectedValue = Convert.ToInt32(row.Cells["IdCondicionIva"].Value.ToString());
                // Según la condición de iva del combo, trae el porcentaje al campo alícuota
                TraerIvaProveedor();

                txtCUIT.Text = row.Cells["CUIT"].Value.ToString();
                txtIB.Text = row.Cells["IngresosBrutos"].Value.ToString();

                dtIA.Value = DateTime.Parse(row.Cells["FechaInicioActividad"].Value.ToString());

                cboCondCompra.SelectedValue = Convert.ToInt32(row.Cells["IdCondicionCompra"].Value.ToString());

                txtDir.Text = row.Cells["Direccion"].Value.ToString();

                cboProvincia.SelectedValue = Convert.ToInt32(row.Cells["IdProvincia"].Value.ToString());

                //Ocultar Combo Localidad y mostrar localidad
                if (Convert.ToInt32(cboProvincia.SelectedValue) > 0)
                {
                    cboLocalidad.TabStop = false;
                    cboLocalidad.Visible = false;

                    //Mostrar Campo texto para mostrar la localidad elegida
                    txtLoc.Visible = true;
                    txtLoc.TabStop = false;
                    txtLoc.Enabled = false;

                    //Mostrar la localidad elegida
                    txtLoc.Text = row.Cells["Localidad"].Value.ToString();
                }

                txtTel.Text = row.Cells["Telefono"].Value.ToString();
                txtFax.Text = row.Cells["Fax"].Value.ToString();
                txtCelular.Text = row.Cells["Celular"].Value.ToString();
                txtMail.Text = row.Cells["MailEmpresa"].Value.ToString();
                txtWeb.Text = row.Cells["Web"].Value.ToString();

                txtContacto.Text = row.Cells["Contacto"].Value.ToString();
                txtMailContacto.Text = row.Cells["MailContacto"].Value.ToString();
                txtCelularContacto.Text = row.Cells["CelularContacto"].Value.ToString();
                
                txtObs.Text = row.Cells["Observaciones"].Value.ToString();

                chkInsumos.Checked =false;
                if (Convert.ToBoolean(row.Cells["Insumos"].Value.ToString())==true)
                {
                    chkInsumos.Checked=true;
                }

                chkProductos.Checked = false;
                if (Convert.ToBoolean(row.Cells["Productos"].Value.ToString()) == true)
                {
                    chkProductos.Checked = true;
                }

                txtSaldo.Text = Convert.ToDouble(row.Cells["Saldo"].Value).ToString("#0.00");
                
            }
        }

        #endregion

        #region CargarToolTips

        private void CargarToolTips()
        {
            toolTip1.SetToolTip(this.btnAgregar, "Agregar");
            toolTip2.SetToolTip(this.btnModificar, "Modificar");
            toolTip3.SetToolTip(this.btnBorrar, "Borrar");
            toolTip4.SetToolTip(this.btnImprimir, "Imprimir");
            toolTip5.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip6.SetToolTip(this.btnCancelar, "Cancelar");
            toolTip7.SetToolTip(this.btnSalir, "Salir");
            toolTip8.SetToolTip(this.btnBuscar, "Buscar");
        }

        #endregion

        #region Método para limpiar los campos de Búsqueda
        // Limpia los controles de búsqueda del form
        private void LimpiarControlesBusqueda()
        {
            this.txtBuscarRS.Text = "";
            this.txtBuscarCP.Text = "";
        }

        #endregion

        #region Método que devuelve el foco a la fila desde donde se presionó algún botón

        private void PosicionarFocoFila()
        {
            // Devuelvo el foco a la fila de la grilla desde donde se llamó
            this.dgvProv.CurrentCell = dgvProv[0, this.indexFila];

            // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
            EventArgs ea = new EventArgs();
            this.dgvProv_SelectionChanged(this.dgvProv, ea);
        }

        #endregion

        #endregion

        #region Eventos del Formulario
        
        #region Eventos de los botones

        #region Evento Click del botón Salir

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Cierro el formulario. G.
            this.Close();
        }

        #endregion

        #region Evento Click del botón Agregar

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Capturo la posición de la filla desde donde se llamo
            if (dgvProv.Rows.Count > 0)
            {
                this.indexFila = dgvProv.CurrentRow.Index;
            }
            
            // Pregunto si el usuario actual tiene nivel mayor a 2, lo dejo agregar. G.
            if (clsGlobales.UsuarioLogueado.Nivel > 2)
            {
                // Cambio el estado del formulario a agregar. G.
                this.myEstado = "A";
                // Limpio los controles del formulario. G.    
                LimpiarControlesForm();
                // Activo los botones para este estado. G.
                ActivarBotones();
                // Habilito los controles para este estado. G.
                HabilitarControles();
                // Posiciono el foco sobre el primer textbox                
                txtRSoc.Focus();
            }
            // Pregunto si el usuario actual tiene nivel menor o igual a 2, No lo dejo agregar. G.
            else
            {
                // vuelvo el formulario al estado anterior. G.
                this.myEstado = "C";
                // El usuario no es de nivel 3, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para dar de Alta un Proveedor", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento Click del botón Modificar

        private void btnModificar_Click(object sender, EventArgs e)
        {
            // Capturo la posición de la filla desde donde se llamo
            this.indexFila = dgvProv.CurrentRow.Index;
            
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvProv.CurrentRow;
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvProv.RowCount == 0)
            {
                // Salgo de la rutina. G.
                return;
            }
            // // Pregunto si el usuario actual tiene nivel mayor a 2, lo dejo agregar. G.
            if (clsGlobales.UsuarioLogueado.Nivel > 2)
            {
                // Cambio el estado del formulario a Modificar. G.
                    this.myEstado = "M";
                // Activo los botones para este estado. G.
                    ActivarBotones();
                // Habilito los controles para este estado. G.
                    HabilitarControles();
                // Combo Localidades N.
                    setComboLocalidad();
                // Posiciono el foco sobre el primer textbox
                    txtRSoc.Focus();
            }
            else
            {
                // Pregunto si el usuario actual tiene nivel menor o igual a 2, No lo dejo agregar. G.
                this.myEstado = "C";
                // El usuario no es de nivel 3, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para Modificar un Proveedor", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento Click del botón Cancelar

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //// Cambio el estado del formulario a agregar. G.
            //this.myEstado = "C";
            //// Cargo las localidades
            //CargarGrillaBusqueda("", "");
            //// Limpio los controles del formulario. G.    
            //ActivarBotones();
            //// Habilito los controles para este estado. G.
            //HabilitarControles();            
            //// Pongo el foco en la fila desde donde se llamo
            //PosicionarFocoFila();


            //Recargar
            if (this.myEstado == "B" && bSearch)
            {
                // Cargo las localidades
                CargarGrillaBusqueda("", "");
                // Cambio el estado del formulario a agregar. G.
                this.myEstado = "C";
                // Limpio los controles del formulario. G.    
                ActivarBotones();
                // Habilito los controles para este estado. G.
                HabilitarControles();
                //Foco
                PosicionarFocoFila();
                //.F.
                bSearch = false;
            }
            else
            {
                // Cambio el estado del formulario a agregar. G.
                this.myEstado = "C";
                // Limpio los controles del formulario. G.    
                ActivarBotones();
                // Habilito los controles para este estado. G.
                HabilitarControles();
                //Foco
                PosicionarFocoFila();
            }

            //.F.
            bSearch = false;
            
        }

        #endregion

        #region Evento Click del botón Aceptar

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Busqueda Activa?...
            if (this.myEstado == "B")
            {
                //Tomar el Id
                this.IdProvSearch = Convert.ToInt32(this.dgvProv.CurrentRow.Cells["IdProveedor"].Value.ToString());
                // Cambio mi estado
                this.myEstado = "C";
                // Lleno nuevamente la grilla
                this.getCargarGrilla();
                // Activo todos los botones
                ActivarBotones();
                // Habilito los controles
                HabilitarControles();
                //Id >0? Solo cuando busca reposiciona por ID
                if (!(IdProvSearch == 0 && bSearch))
                {
                    ReposicionarById();
                    IdProvSearch = 0;
                }
                else
                {
                    //Foco
                    PosicionarFocoFila();
                }
                //.F.
                bSearch = false;
                //Retornar
                return;
            }

            // Creo la cadena para grabar las Modificaciones del Proveedor
            string myCadena = "";
            // Creo nuevo Proveedor. N.
            clsProveedores NuevoProveedor = new clsProveedores();
            // Tomo la línea actual de la grilla. N.
            DataGridViewRow row = dgvProv.CurrentRow;
            // Verifico el estado del formulario para saber si estoy creando o modificando. N.
            // Paso los datos del formulario al nuevo Proveedor
            if (dgvProv.Rows.Count > 0)
            {
               NuevoProveedor.IdProveedor = Convert.ToInt32(row.Cells["IdProveedor"].Value);
            }
            NuevoProveedor.RazonSocial = txtRSoc.Text;
            NuevoProveedor.NombreFantasia = txtFantasia.Text;
            NuevoProveedor.Direccion = txtDir.Text;
            NuevoProveedor.IdLocalidad = Convert.ToInt32(cboLocalidad.SelectedValue);
            NuevoProveedor.FechaInicioActividad = dtIA.Value.ToShortDateString();
            NuevoProveedor.IngresosBrutos = txtIB.Text;
            NuevoProveedor.CUIT = txtCUIT.Text;
            /*Añadir guiones si hace falta*/
            if (NuevoProveedor.CUIT.Length == 11)
            {
                NuevoProveedor.CUIT = clsGlobales.cFormato.CUITFormateado(txtCUIT.Text);
            }
            else
            {
                NuevoProveedor.CUIT = txtCUIT.Text;
            }
            NuevoProveedor.IdCondicionIva = Convert.ToInt32(cboCondIVA.SelectedValue);
            NuevoProveedor.IdCondicionCompra = Convert.ToInt32(cboCondCompra.SelectedValue);
            NuevoProveedor.IdProvincia = Convert.ToInt32(cboProvincia.SelectedValue);
            NuevoProveedor.IdLocalidad = Convert.ToInt32(cboLocalidad.SelectedValue);
            NuevoProveedor.Telefono = txtTel.Text;
            NuevoProveedor.Fax = txtFax.Text;
            NuevoProveedor.Celular = txtCelular.Text;
            NuevoProveedor.MailEmpresa = txtMail.Text;
            NuevoProveedor.Web = txtWeb.Text;
            NuevoProveedor.Contacto = txtContacto.Text;
            NuevoProveedor.MailContacto = txtMailContacto.Text;
            NuevoProveedor.CelularContacto = txtCelularContacto.Text;
            NuevoProveedor.Observaciones = txtObs.Text;
            if (!(string.IsNullOrEmpty(this.txtSaldo.Text.ToString())))
            {
                NuevoProveedor.Saldo = Convert.ToDouble(txtSaldo.Text);
            }
            else
            {
                NuevoProveedor.Saldo = 0;
            }

            if (this.chkInsumos.Checked)
            {
                NuevoProveedor.ProvIns = 1;
            }

            if (this.chkProductos.Checked)
            {
                NuevoProveedor.ProvProd = 1;
            }


            //Vector Errores
            string[] cErrores = NuevoProveedor.cValidaProveedor();
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

            //INSERTAR O UPDATE           
            if (this.myEstado == "M")
            {
                // Creo la cadena para grabar las Modificaciones de la Localidad
                myCadena = "UPDATE Proveedores SET RazonSocial = '" + NuevoProveedor.RazonSocial +
                                                 "',NombreFantasia = '" + NuevoProveedor.NombreFantasia + "'," +
                                                 " IdCondicionIVA = " + NuevoProveedor.IdCondicionIva + "," +
                                                 " CUIT = '" + NuevoProveedor.CUIT + "'," +
                                                 " IngresosBrutos = '" + NuevoProveedor.IngresosBrutos + "'," +
                                                 " FechaInicioActividad = '" + NuevoProveedor.FechaInicioActividad + "'," +
                                                 " IdCondicionCompra = " + NuevoProveedor.IdCondicionCompra + "," +
                                                 " Direccion = '" + NuevoProveedor.Direccion + "'," +
                                                 " IdProvincia = " + NuevoProveedor.IdProvincia + "," +
                                                 " IdLocalidad = " + NuevoProveedor.IdLocalidad + "," +
                                                 " Telefono = '" + NuevoProveedor.Telefono + "'," +
                                                 " Fax = '" + NuevoProveedor.Fax + "'," +
                                                 " Celular = '" + NuevoProveedor.Celular + "'," +
                                                 " MailEmpresa = '" + NuevoProveedor.MailEmpresa + "'," +
                                                 " Web = '" + NuevoProveedor.Web + "'," +
                                                 " Contacto = '" + NuevoProveedor.Contacto + "'," +
                                                 " MailContacto = '" + NuevoProveedor.MailContacto + "'," +
                                                 " CelularContacto = '" + NuevoProveedor.CelularContacto + "'," +
                                                 " Observaciones = '" + NuevoProveedor.Observaciones + "'," +
                                                 " Insumos  = " + NuevoProveedor.ProvIns + "," +
                                                 " Productos  = " + NuevoProveedor.ProvProd + "," + 
                                                 " Saldo = " + NuevoProveedor.Saldo + 
                                                 " WHERE IdProveedor = " + NuevoProveedor.IdProveedor;
            }
            else
            {
                //INSERT VAN EN 0
                NuevoProveedor.SaldoInicial = 0;
                NuevoProveedor.SaldoAFavor = 0;

                // Creo la cadena para grabar las Modificaciones de la Localidad
                myCadena = "INSERT INTO Proveedores (NombreFantasia," +
                                                   " RazonSocial," +
                                                   " IdCondicionIVA," +
                                                   " CUIT," +
                                                   " IngresosBrutos," +
                                                   " FechaInicioActividad," +
                                                   " IdCondicionCompra," +
                                                   " Direccion," +
                                                   " IdProvincia," +
                                                   " IdLocalidad," +
                                                   " Telefono," +
                                                   " Fax," +
                                                   " Celular," +
                                                   " MailEmpresa," +
                                                   " Web," +
                                                   " Contacto," +
                                                   " MailContacto," +
                                                   " CelularContacto," +
                                                   " Observaciones," +
                                                   " Insumos," +
                                                   " Productos," +
                                                   " Activo," +
                                                   " Saldo," +
                                                   " SaldoInicial," +
                                                   " SaldoAFavor" +
                                                   ") values ('" + NuevoProveedor.NombreFantasia + "','"
                                                                 + NuevoProveedor.RazonSocial + "',"
                                                                 + NuevoProveedor.IdCondicionIva + ",'"
                                                                 + NuevoProveedor.CUIT + "','"
                                                                 + NuevoProveedor.IngresosBrutos + "','"
                                                                 + NuevoProveedor.FechaInicioActividad + "',"
                                                                 + NuevoProveedor.IdCondicionCompra + ",'"
                                                                 + NuevoProveedor.Direccion + "',"
                                                                 + NuevoProveedor.IdProvincia + ","
                                                                 + NuevoProveedor.IdLocalidad + ",'"
                                                                 + NuevoProveedor.Telefono + "','"
                                                                 + NuevoProveedor.Fax + "','"
                                                                 + NuevoProveedor.Celular + "','"
                                                                 + NuevoProveedor.MailEmpresa + "','"
                                                                 + NuevoProveedor.Web + "','"
                                                                 + NuevoProveedor.Contacto + "','"
                                                                 + NuevoProveedor.MailContacto + "','"
                                                                 + NuevoProveedor.CelularContacto + "','"
                                                                 + NuevoProveedor.Observaciones + "',"
                                                                 + NuevoProveedor.ProvIns + "," 
                                                                 + NuevoProveedor.ProvProd + "," 
                                                                 + "1" + "," +
                                                                 + NuevoProveedor.Saldo + ","
                                                                 + NuevoProveedor.SaldoInicial + ","
                                                                 + NuevoProveedor.SaldoAFavor + ")";
                
                }

                // Ejecuto la consulta SQL
                clsDataBD.GetSql(myCadena);
                // Lleno nuevamente la grilla
                getCargarGrilla();
                // Regreso el formulario a su estado inicial
                this.btnCancelar.PerformClick();
            
        }

        #endregion

        #region Reposicionar Grilal por Id

        //REPOSICIONA GRILLA POR ID
        private void ReposicionarById()
        {
            //recorrer
            foreach (DataGridViewRow myRow in this.dgvProv.Rows)
            {
                if (Convert.ToInt32(myRow.Cells["IdProveedor"].Value.ToString()) == IdProvSearch)
                {
                    // Devuelvo el foco a la fila de la grilla desde donde se llamó
                    this.dgvProv.CurrentCell = dgvProv[1, myRow.Index];

                    // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                    EventArgs ea = new EventArgs();
                    this.dgvProv_SelectionChanged(this.dgvProv, ea);

                    //Salir
                    break;
                }
            }
        }

        #endregion


        #region Evento Click del botón Borrar

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            // Capturo la posición de la filla desde donde se llamo
            this.indexFila = dgvProv.CurrentRow.Index;
            
            // Pregunto si el usuario actual es el mismo que se quiere modificar. G.
            if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja)
            {
                //TRAER LA FILA ACTUAL. N.
                DataGridViewRow row = dgvProv.CurrentRow;
                // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
                int Id = Convert.ToInt32(row.Cells["IdProveedor"].Value);
                string Prov = row.Cells["RazonSocial"].Value.ToString();
                int Niv = clsGlobales.UsuarioLogueado.Nivel;

               if (Niv < clsGlobales.cParametro.NivelBaja)
                {
                    // El usuario no es de nivel 5, por lo que no puede crear un nuevo usuario. G.
                    MessageBox.Show("Usted no tiene los permisos para Eliminar este Proveedor", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Ejecuto el botón cancelar. G.
                    this.btnCancelar.PerformClick();
                }
                else
                {
                    // Confirma salir de la aplicación ?
                    DialogResult dlResult = MessageBox.Show("Desea Eliminar al Proveedor " + Prov + " ?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    // Si confirma salir de la aplicación....
                    if (dlResult == DialogResult.Yes)
                    {
                        // Armo la cadena para verificar si el proveedor tiene facturas de compra activas
                        string CadenaFacturasSQL = "select * from ComprobantesCompras where IdProveedor = " 
                                                + Id + " and Activo = 1 and IdTipoComprobanteCompra = 3";
                        // Ejejuto la consulta y paso los datos a una tabla
                        DataTable myTablaComp = clsDataBD.GetSql(CadenaFacturasSQL);
                        // Si la tabla tiene registros, no se puede eliminar el proveedor
                        if (!(myTablaComp.Rows.Count > 0))
                        {
                            string myCadena = "update Proveedores set Activo = 0 WHERE IdProveedor =" + Id;
                            clsDataBD.GetSql(myCadena);
                            // Refresco la grilla
                            getCargarGrilla();
                        }
                        else
                        {
                            MessageBox.Show("El proveedor tiene facturas asociadas por lo que no se puede borrar", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

            }
            else
            {
                // El usuario no es de nivel 5, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para Eliminar un usuario", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #endregion

        #region Eventos de la Grilla

        #region Evento CellContentClick de la Grilla de Proveedores

        private void dgvProv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvProv.RowCount == 0)
            {
                return;
            }           
            else {
                //Mostrar los datos
                getMostrarDatos();
            }
        }

        #endregion

        #region Evento SelectionChanged de la Grilla de Proveedores

        private void dgvProv_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProv.RowCount == 0)
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

        #region Evento KeyDown de la Grilla de Proveedores
        private void dgvProv_KeyDown(object sender, KeyEventArgs e)
        {
            //PRESIONO DEL?
            if (e.KeyCode == Keys.Delete)
            {
                this.btnBorrar.PerformClick(); //LLAMAR A EVENTO CLICK DEL BOTON BORRAR
            }
        }

        #endregion

        #endregion

        #region Evento Load del formulario

        private void frmProveedores_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            //Estado
            this.myEstado = "C";
            // Cargo el combo con las provincias
            CargarProvincias();
            //Cargar Combo Condiciones de IVA
            CargarCondicionIVA();
            //Cargar Combo Condiciones de Compra
            CargarCondicionCompra();
            //Cargar Grilla
            getCargarGrilla();
            // Llamo al método activar los botones del formulario. N.
            ActivarBotones();
            // Llamo al método habilitar controles del formulario. N.
            HabilitarControles();
            // Cargar ToolTips
            CargarToolTips();

            //Titulo Ventana
            this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;
        }

        #endregion

        #region Eventos Leave

        #region Evento Leave de Razon Social
        private void txtRSoc_Leave(object sender, EventArgs e)
        {
            txtRSoc.Text = txtRSoc.Text.ToUpper();
        }

        #endregion

        #region Evento Leave de Nombre Fantasia
        private void txtFantasia_Leave(object sender, EventArgs e)
        {
            txtFantasia.Text = txtFantasia.Text.ToUpper();
        }

        #endregion

        #region Evento Leave de Direccion
        private void txtDir_Leave(object sender, EventArgs e)
        {
            txtDir.Text = txtDir.Text.ToUpper();
        }

        #endregion

        #region Evento Leave de Contacto
        private void txtContacto_Leave(object sender, EventArgs e)
        {
            txtContacto.Text = txtContacto.Text.ToUpper();
        }
        #endregion

        #endregion

        #region Evento del Combo Provincia SelectedValueChanged
        private void cboProvincia_SelectedValueChanged(object sender, EventArgs e)
        {
            //Solo si estoy editando o modificando N.
            if (!(this.myEstado == "C"))
            {
                if (cboProvincia.SelectedValue == null) { return; }

                //Clean Combobox
                cboLocalidad.DataSource = null;
                cboLocalidad.DataBindings.Clear();

                // Cargo el combo de las Localidades N.
                string strSQL = " IdProvincia = " + cboProvincia.SelectedValue;
                clsDataBD.CargarCombo(cboLocalidad, "Localidades", "Localidad", "IdLocalidad", strSQL);

                // Establezco el combo localidad a la primera opcion N.
                cboLocalidad.TabStop = true;
                cboLocalidad.Enabled = true;
                //No mostrar dato alguno
                cboLocalidad.SelectedIndex = -1;
            }
            else
            {

                //Clean Combobox
                cboLocalidad.DataSource = null;
                cboLocalidad.DataBindings.Clear();
                //Desactivar
                cboLocalidad.TabStop = false;
                cboLocalidad.Enabled = false;
                //No mostrar dato alguno
                cboLocalidad.SelectedIndex = -1;
            }
        }

        #endregion

        #region btnBuscar: Click

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Cambio mi estado a Búsqueda
            this.myEstado = "B";
            // Capturo la posición de la filla desde donde se llamo
            this.indexFila = dgvProv.CurrentRow.Index;
            // Habilito los botones según mi estado
            ActivarBotones();
            // Habilito los campos de búsqueda
            HabilitarControles();
            // Limpio los campos de búsqueda
            LimpiarControlesBusqueda();
            // Pongo el foco en el primer control de búsqueda
            txtBuscarRS.Focus();
        }

        #endregion

        #region txtBuscarRS: TextChanged

        private void txtBuscarRS_TextChanged(object sender, EventArgs e)
        {
            if (!(this.txtBuscarRS.Text == ""))
            {
                CargarGrillaBusqueda(this.txtBuscarRS.Text, "RazonSocial");
            }
        }

        #endregion

        #region txtBuscarCP: TextChanged

        private void txtBuscarCP_TextChanged(object sender, EventArgs e)
        {
            if (!(this.txtBuscarCP.Text == ""))
            {
                CargarGrillaBusqueda(this.txtBuscarCP.Text, "IdProveedor");
            }
        }

        #endregion

        #region Evento btnImprimir Click

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //Data Set
            dsReportes oDsProv = new dsReportes();

            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = dgvProv.Rows.Count;

            for (int i = 0; i < dgvFilas; i++)
            {
                oDsProv.Tables["DtProv"].Rows.Add
                (new object[] { dgvProv[0,i].Value.ToString(),
                dgvProv[1,i].Value.ToString(),
                dgvProv[2,i].Value.ToString(), 
                dgvProv[11,i].Value.ToString(),
                dgvProv[14,i].Value.ToString(),
                dgvProv[16,i].Value.ToString() });

            }

            //Objeto Reporte
            rptProveedor oRepProv = new rptProveedor();
            //Cargar Reporte            
            //    oRepTipoCli.Load(Application.StartupPath + "\\rptTipoCli.rpt");
            oRepProv.Load(Application.StartupPath + "\\rptProveedor.rpt");
            //Establecer el DataSet como DataSource
            oRepProv.SetDataSource(oDsProv);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepProv;
            oRepProv.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepProv.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
            oRepProv.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
            oRepProv.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
            oRepProv.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
            oRepProv.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepProv.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
            oRepProv.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();
        }

        #endregion

        

        #endregion

        #region Evento click del combo condiciones de iva

        private void cboCondIVA_Click(object sender, EventArgs e)
        {
            BanderaCombo = true;
        }

        #endregion

        #region Evento SelectIndexChanged del combo de condiciones de IVA

        private void cboCondIVA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BanderaCombo)
            {
                // Paso al txt la alícuota de la condición de IVA del proveedor
                TraerIvaProveedor();
            }
        }

        #endregion

        #region Método que trae el porcentaje de Iva del proveedor

        private void TraerIvaProveedor()
        {
            int IdCond = Convert.ToInt32(cboCondIVA.SelectedValue);
            string CadenaCombo = "Select * from CondicionesIva where IdCondicionIva =" + IdCond;
            DataTable myTabla = clsDataBD.GetSql(CadenaCombo);
            foreach (DataRow row in myTabla.Rows)
            {
                txtAlicuota.Text = row["Porcentaje"].ToString();
            }
        }

        #endregion

        private void txtSaldo_KeyPress(object sender, KeyPressEventArgs e)
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

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prama.Formularios.Caja
{
    public partial class frmAsociacionesCuentas : Form
    {
        #region Declaracion Variables Nivel Formulario

        //VARIABLES
        string myEstado = "C";
        int indexFila = 0;
        bool bSearch = false;
        int IdCuenta = 0;

        #endregion

        #region Constructor del Formulario

        public frmAsociacionesCuentas()
        {
            InitializeComponent();
        }

        #endregion

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
                    this.btnModificar.TabStop = true && (dgvCuentas.RowCount != 0);
                    this.btnModificar.Visible = true && (dgvCuentas.RowCount != 0);
                    this.btnBuscar.TabStop = true && (dgvCuentas.RowCount != 0);;
                    this.btnBuscar.Visible = true && (dgvCuentas.RowCount != 0);;
                    this.btnAceptar.TabStop = false;
                    this.btnAceptar.Visible = false;
                    this.btnCancelar.TabStop = false;
                    this.btnCancelar.Visible = false;
                    this.btnSalir.TabStop = true;
                    this.btnSalir.Visible = true;
                    // Si el nivel del usuario es el 5, habilito el botón borrar
                    if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja)
                    {
                        this.btnBorrar.TabStop = true && (dgvCuentas.RowCount != 0);
                        this.btnBorrar.Visible = true && (dgvCuentas.RowCount != 0);
                    }
                    else
                    {
                        this.btnBorrar.TabStop = false;
                        this.btnBorrar.Visible = false;
                    }
                    this.btnImprimir.TabStop = true && (dgvCuentas.RowCount != 0);
                    this.btnImprimir.Visible = true && (dgvCuentas.RowCount != 0);
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
                    this.txtNombre.TabStop = true;
                    this.txtNombre.Enabled = true;
                    this.cboCuentas.TabStop = true;
                    this.cboCuentas.Enabled = true;
                    this.chkCredito.TabStop = true;
                    this.chkCredito.Enabled = true;
                    this.chkDebito.TabStop = true;
                    this.chkDebito.Enabled = true;
                    this.chkMP.TabStop = true;
                    this.chkMP.Enabled = true;
                    this.chkTransferencias.TabStop = true;
                    this.chkTransferencias.Enabled = true;


                    this.txtBuscarAsociacion.TabStop = false;
                    this.txtBuscarAsociacion.Enabled = false;
                    this.txtBuscarCuenta.TabStop = false;
                    this.txtBuscarCuenta.Enabled = false;

                    this.dgvCuentas.TabStop = false;
                    this.dgvCuentas.Enabled = false;
                    this.dgvCuentas.Height = 240;
                    this.gpbBusquedas.Visible = false;
                    return;
                case "B":
                    this.txtNombre.TabStop = false;
                    this.txtNombre.Enabled = false;
                    this.cboCuentas.TabStop = false;
                    this.cboCuentas.Enabled = false;
                    this.chkCredito.TabStop = false;
                    this.chkCredito.Enabled = false;
                    this.chkDebito.TabStop = false;
                    this.chkDebito.Enabled = false;
                    this.chkMP.TabStop = false;
                    this.chkMP.Enabled = false;
                    this.chkTransferencias.TabStop = false;
                    this.chkTransferencias.Enabled = false;

                    this.txtBuscarAsociacion.TabStop = true;
                    this.txtBuscarAsociacion.Enabled = true;
                    this.txtBuscarCuenta.TabStop = true;
                    this.txtBuscarCuenta.Enabled = true;

                    this.dgvCuentas.TabStop = true && (dgvCuentas.RowCount > 0);
                    this.dgvCuentas.Enabled = true && (dgvCuentas.RowCount > 0);
                    this.dgvCuentas.Height = 180;
                    this.gpbBusquedas.Visible = true;
                    return;
                case "C":
                    this.txtNombre.TabStop = false;
                    this.txtNombre.Enabled = false;
                    this.cboCuentas.TabStop = false;
                    this.cboCuentas.Enabled = false;
                    this.chkCredito.TabStop = false;
                    this.chkCredito.Enabled = false;
                    this.chkDebito.TabStop = false;
                    this.chkDebito.Enabled = false;
                    this.chkMP.TabStop = false;
                    this.chkMP.Enabled = false;
                    this.chkTransferencias.TabStop = false;
                    this.chkTransferencias.Enabled = false;

                    this.txtBuscarAsociacion.TabStop = false;
                    this.txtBuscarAsociacion.Enabled = false;
                    this.txtBuscarCuenta.TabStop = false;
                    this.txtBuscarCuenta.Enabled = false;

                    this.dgvCuentas.TabStop = true && (dgvCuentas.RowCount > 0);
                    this.dgvCuentas.Enabled = true && (dgvCuentas.RowCount > 0);
                    this.dgvCuentas.Height = 240;
                    this.gpbBusquedas.Visible = false;
                    return;
            }
        }
        #endregion

        #region Método para limpiar los controles del formulario
        //LIMPIA LA PROPIEDAD TEXT DE LOS CONTROLES DEL FORMULARIO. N.
        private void LimpiarControlesForm()
        {
            this.txtNombre.Text = "";
            this.cboCuentas.SelectedValue = -1;
            this.chkCredito.Checked = false;
            this.chkDebito.Checked = false;
            this.chkMP.Checked = false;
            this.chkTransferencias.Checked = false;
        }
        #endregion

        #region Método para limpiar los campos de Búsqueda
        // Limpia los controles de búsqueda del form
        private void LimpiarControlesBusqueda()
        {
            this.txtBuscarCuenta.Text = "";
            this.txtBuscarAsociacion.Text = "";
        }

        #endregion

        #region Método para cargar la grilla

        private void CargarGrilla(string Buscar, string Campo)
        {
            // Variable para la cadena SQL
            string myCadena = "";
            if (Buscar == "")
            {
                // Cadena SQL 
                myCadena = "select * from Vista_CajaAsociacionesCuentas ";
            }
            else
            {
                // Cadena SQL 
                myCadena = "select * from Vista_CajaAsociacionesCuentas where " + Campo + " like '" + Buscar + "%' order by " + Campo;

                //Cambiar .T.
                bSearch = true;
            }

            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            if (clsGlobales.ConB == null)
            { mDtTable = clsDataBD.GetSql(myCadena); }
            else
            { mDtTable = clsDataBD.GetSqlB(myCadena); }
            // Evito que el dgv genere columnas automáticas
            dgvCuentas.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvCuentas.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvCuentas.Rows.Count;
            // Posiciono la grilla en la última fila
            //dgvLocalidades.CurrentCell = dgvLocalidades[1, Filas - 1];

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

        #region Método que devuelve el foco a la fila desde donde se presionó algún botón

        private void PosicionarFocoFila()
        {
            // Devuelvo el foco a la fila de la grilla desde donde se llamó
            this.dgvCuentas.CurrentCell = dgvCuentas[1, this.indexFila];

            // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
            EventArgs ea = new EventArgs();
            this.dgvCuentas_SelectionChanged(this.dgvCuentas, ea);
        }

        #endregion

        #region Reposicionar Grilal por Id

        //REPOSICIONA GRILLA POR ID
        private void ReposicionarById()
        {
            //recorrer
            foreach (DataGridViewRow myRow in this.dgvCuentas.Rows)
            {
                if (Convert.ToInt32(myRow.Cells["IdCajaAsociaciones"].Value.ToString()) == IdCuenta)
                {
                    // Devuelvo el foco a la fila de la grilla desde donde se llamó
                    this.dgvCuentas.CurrentCell = dgvCuentas[1, myRow.Index];

                    // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                    EventArgs ea = new EventArgs();
                    this.dgvCuentas_SelectionChanged(this.dgvCuentas, ea);

                    //Salir
                    break;
                }
            }
        }

        #endregion

        #endregion

        #region Eventos del formulario

        #region Eventos de la grilla

        #region Evento CellContentClick de la Grilla

        private void dgvCuentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvCuentas.RowCount == 0)
            {
                return;
            }
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvCuentas.CurrentRow;
            // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
            //TRAER LOS DATOS. H.
            txtNombre.Text = row.Cells["CajaAsociaciones"].Value.ToString().ToUpper();
            cboCuentas.SelectedValue = Convert.ToInt32(row.Cells["IdCajaCuenta"].Value);

            chkCredito.Checked = Convert.ToBoolean(row.Cells["Credito"].Value);
            chkDebito.Checked = Convert.ToBoolean(row.Cells["Debito"].Value);

            chkMP.Checked = Convert.ToBoolean(row.Cells["MercadoPago"].Value);
            chkTransferencias.Checked = Convert.ToBoolean(row.Cells["Transferencias"].Value);

        }

        #endregion

        #region Evento SelectionChanged de la Grilla

        private void dgvCuentas_SelectionChanged(object sender, EventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvCuentas.RowCount == 0)
            {
                return;
            }
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvCuentas.CurrentRow;
            // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
            //TRAER LOS DATOS. H.
            txtNombre.Text = row.Cells["CajaAsociaciones"].Value.ToString().ToUpper();
            cboCuentas.SelectedValue = Convert.ToInt32(row.Cells["IdCajaCuenta"].Value);

            chkCredito.Checked = Convert.ToBoolean(row.Cells["Credito"].Value);
            chkDebito.Checked = Convert.ToBoolean(row.Cells["Debito"].Value);

            chkMP.Checked = Convert.ToBoolean(row.Cells["MercadoPago"].Value);
            chkTransferencias.Checked = Convert.ToBoolean(row.Cells["Transferencias"].Value);
        }

        #endregion

        #region Evento KeyDown de la Grilla

        private void dgvCuentas_KeyDown(object sender, KeyEventArgs e)
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

        private void frmAsociacionesCuentas_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 
			
            this.myEstado = "C";
            clsDataBD.CargarCombo(cboCuentas, "CajaCuentas", "Nombre", "IdCajaCuenta","",1);
            cboCuentas.SelectedIndex = -1;
            // Llamo al método cargar Localidades para rellenar la grilla. G.
            CargarGrilla("", "");
            // Llamo al método activar los botones del formulario. G.
            ActivarBotones();
            // Llamo al método habilitar controles del formulario. G.
            HabilitarControles();
            // Cargar ToolTips
            CargarToolTips();
            //Titulo Ventana
            this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;

            
        }

        #endregion

        #region Eventos de los campos de búsqueda

        private void txtBuscarAsociacion_TextChanged(object sender, EventArgs e)
        {
            if (!(this.txtBuscarAsociacion.Text == ""))
            {
                // Cargo las localidades filtradas por la búsqueda
                CargarGrilla(txtBuscarAsociacion.Text, "CajaAsociaciones");
            }
        }

        private void txtBuscarCuenta_TextChanged(object sender, EventArgs e)
        {
            if (!(this.txtBuscarCuenta.Text == ""))
            {
                // Cargo las localidades filtradas por la búsqueda
                CargarGrilla(txtBuscarCuenta.Text, "Nombre");
            }
        }

        private void txtBuscarAsociacion_Click(object sender, EventArgs e)
        {
            txtBuscarCuenta.Text = "";
          //  CargarGrilla("", "");
        }

        private void txtBuscarCuenta_Click(object sender, EventArgs e)
        {
            txtBuscarAsociacion.Text = "";
           // CargarGrilla("", "");
        }

        #endregion

        #region Eventos de los botones

        #region Evento Click del botón Agregar

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (dgvCuentas.Rows.Count > 0)
            {
                // Capturo la posición de la fila desde donde se llamo
                this.indexFila = dgvCuentas.CurrentRow.Index;
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
                txtNombre.Focus();
            }
            // Pregunto si el usuario actual tiene nivel menor o igual a 2, No lo dejo agregar. G.
            else
            {
                // vuelvo el formulario al estado anterior. G.
                this.myEstado = "C";
                // El usuario no es de nivel 3, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para crear una nueva Asociación", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento CLick del botón Editar

        private void btnModificar_Click(object sender, EventArgs e)
        {
            // Capturo la posición de la fila desde donde se llamo
            this.indexFila = dgvCuentas.CurrentRow.Index;

            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvCuentas.CurrentRow;
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvCuentas.RowCount == 0)
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
                // Cargo los datos de la contraseña en sus correspondientes textbox. G.
                txtNombre.Text = row.Cells["CajaAsociaciones"].Value.ToString();
                cboCuentas.SelectedValue = Convert.ToInt32(row.Cells["IdCajaCuenta"].Value);

                chkCredito.Checked = Convert.ToBoolean(row.Cells["Credito"].Value);
                chkDebito.Checked = Convert.ToBoolean(row.Cells["Debito"].Value);
                chkMP.Checked = Convert.ToBoolean(row.Cells["MercadoPago"].Value);
                chkTransferencias.Checked = Convert.ToBoolean(row.Cells["Transferencias"].Value);
                
                // Posiciono el foco sobre el primer textbox
                txtNombre.Focus();
            }
            else
            {
                // Pregunto si el usuario actual tiene nivel menor o igual a 2, No lo dejo agregar. G.
                this.myEstado = "C";
                // El usuario no es de nivel 3, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para modificar una Asociación", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento Click del botón Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Capturo la posición de la fila desde donde se llamo
            this.indexFila = dgvCuentas.CurrentRow.Index;
            // Cambio mi estado a Búsqueda
            this.myEstado = "B";
            // Habilito los botones según mi estado
            ActivarBotones();
            // Habilito los campos de búsqueda
            HabilitarControles();
            // Limpio los campos de búsqueda
            LimpiarControlesBusqueda();
            // Pongo el foco en el primer control de búsqueda
            txtBuscarAsociacion.Focus();
        }

        #endregion

        #region Evento Click del botón Imprimir

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //Data Set
            dsReportes oDsAsCta = new dsReportes();

            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = dgvCuentas.Rows.Count;

            for (int i = 0; i < dgvFilas; i++)
            {
                oDsAsCta.Tables["dtAsCta"].Rows.Add
                (new object[] { dgvCuentas[1,i].Value.ToString(),
                dgvCuentas[3,i].Value.ToString(),
                dgvCuentas[4,i].Value.ToString(), 
                dgvCuentas[5,i].Value.ToString(),
                dgvCuentas[6,i].Value.ToString(),
                dgvCuentas[7,i].Value.ToString() });

            }

            //Objeto Reporte
            rptAsCta oRepAsCta = new rptAsCta();
            //Cargar Reporte            
            //    oRepTipoCli.Load(Application.StartupPath + "\\rptTipoCli.rpt");
            oRepAsCta.Load(Application.StartupPath + "\\rptAsCta.rpt");
            //Establecer el DataSet como DataSource
            oRepAsCta.SetDataSource(oDsAsCta);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepAsCta;
            oRepAsCta.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepAsCta.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepAsCta.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
            oRepAsCta.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
            oRepAsCta.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
            oRepAsCta.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
            oRepAsCta.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepAsCta.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
            oRepAsCta.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();
        }

        #endregion

        #region Evento Click del botón Aceptar

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Validar
            bool bEsValido;
            bEsValido = ValidarCarga();
            if (bEsValido == false)
            {
                return;
            }

            //// Si la grilla no contiene ninguna fila, salgo del evento
            //if (dgvCuentas.RowCount == 0)
            //{
            //    // Muestro un mensaje indicando que no se encontraron los datos
            //    MessageBox.Show("No se encontraron coincidencias", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    // Regreso el formulario a su estado inicial
            //    this.btnBuscar.PerformClick();
            //    return;
            //}
            // Creo la cadena para grabar las Modificaciones de la Localidad
            string myCadena = "";

            //// Tomo la línea actual de la grilla. G.
            //DataGridViewRow row = dgvCuentas.CurrentRow;
            //// Verifico el estado del formulario para saber si estoy creando o modificando una Localidad. G.
            //// Paso los datos del formulario a la nueva Localidad
            //int iIdCuenta = Convert.ToInt32(row.Cells["IdCajaAsociaciones"].Value);


            string sNombre = txtNombre.Text.ToUpper();
            int iCuenta = Convert.ToInt32(cboCuentas.SelectedValue);
            int iCredito = Convert.ToInt32(chkCredito.Checked);
            int iDebito = Convert.ToInt32(chkDebito.Checked);
            int iMercado = Convert.ToInt32(chkMP.Checked);
            int iTransfer = Convert.ToInt32(chkTransferencias.Checked);


            switch (this.myEstado)
            {
                case "A":

                    // Creo la cadena para grabar el alta de la Localidad
                    myCadena = "INSERT INTO CajaAsociacionesCuentas (CajaAsociaciones, IdCajaCuenta, Credito, Debito,MercadoPago, Transferencias) values ('"
                                    + sNombre + "', "
                                    + iCuenta + ", "
                                    + iCredito + ", "
                                    + iDebito + ", "
                                    + iMercado + ", "
                                    + iTransfer + ")";
                    // Ejecuto la consulta SQL
                    if (clsGlobales.ConB == null)
                    { clsDataBD.GetSql(myCadena); }
                    else
                    { clsDataBD.GetSqlB(myCadena); }
                    // Lleno nuevamente la grilla
                    CargarGrilla("", "");
                    // Regreso el formulario a su estado inicial
                    this.btnCancelar.PerformClick();
                    return;

                case "B":
                    //Tomar el Id
                    IdCuenta = Convert.ToInt32(dgvCuentas.CurrentRow.Cells["IdCajaAsociaciones"].Value.ToString());
                    // Cambio mi estado
                    this.myEstado = "C";
                    // Lleno nuevamente la grilla
                    this.CargarGrilla("", "");
                    // Activo todos los botones
                    ActivarBotones();
                    // Habilito los controles
                    HabilitarControles();
                    //Id >0? Solo cuando busca reposiciona por ID
                    if (!(IdCuenta == 0 && bSearch))
                    {
                        ReposicionarById();
                        IdCuenta = 0;
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

                case "M":
                    //Tomar el Id
                    IdCuenta = Convert.ToInt32(dgvCuentas.CurrentRow.Cells["IdCajaAsociaciones"].Value.ToString());
                    // Creo la cadena para grabar las Modificaciones de la Localidad
                    myCadena = "UPDATE CajaAsociacionesCuentas SET CajaAsociaciones = '" + sNombre + "', IdCajaCuenta = " +
                               iCuenta + ", Credito = " + iCredito + ", Debito = " + iDebito + ", MercadoPago = " + iMercado +
                               ", Transferencias = " + iTransfer +

                               " WHERE IdCajaAsociaciones = " + IdCuenta;
                    // Ejecuto la consulta SQL
                    if (clsGlobales.ConB == null)
                    { clsDataBD.GetSql(myCadena); }
                    else
                    { clsDataBD.GetSqlB(myCadena); }
                    // Lleno nuevamente la grilla
                    CargarGrilla("", "");
                    // Regreso el formulario a su estado inicial
                    this.btnCancelar.PerformClick();
                    return;

            }
        }

        #endregion

        #region Funcion ValidarCarga

        private bool ValidarCarga()
        {
            bool bValida = true;


            //NOMBRE
            if (this.txtNombre.Text == "")
            {
                MessageBox.Show("Debe completar el dato 'Nombre Asociacion'!", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtNombre.Focus();
                bValida = false;
                //reornar valor
                return bValida;

            }

            //CUENTAS
            if (this.cboCuentas.Text == "")
            {
                MessageBox.Show("Debe completar el dato 'Cuenta Bancaria'!", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.cboCuentas.Focus();
                bValida = false;
                //reornar valor
                return bValida;

            }

          
            //METODO CANCELACION
            if (this.chkCredito.Checked ==false && this.chkDebito.Checked == false && this.chkMP.Checked == false && this.chkTransferencias.Checked==false)
            {
                MessageBox.Show("Debe elegir alguno de los métodos de pago indicados (Débito, Crédito, MP o Transferencia)!", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bValida = false;
                //reornar valor
                return bValida;

            }

            //reornar valor
            return bValida;
        }

        #endregion

        #region Evento CLick del botón Cancelar

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Recargar
            if (this.myEstado == "B" && bSearch)
            {
                // Cambio el estado del formulario a agregar. G.
                this.myEstado = "C";
                // Limpio los controles del formulario. G.    
                ActivarBotones();
                // Habilito los controles para este estado. G.
                HabilitarControles();
            }
            else
            {
                // Cambio el estado del formulario a agregar. G.
                this.myEstado = "C";
                // Limpio los controles del formulario. G.    
                ActivarBotones();
                // Habilito los controles para este estado. G.
                HabilitarControles();

            }

            if (indexFila != 0)
            {
                //Foco
                PosicionarFocoFila();
            }

            //.F.
            bSearch = false;
        }

        #endregion

        #region Evento Click del botón Salir

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Cierro el formulario. G.
            this.Close();
        }

        #endregion

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Space))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        #endregion

        #endregion

    }
}

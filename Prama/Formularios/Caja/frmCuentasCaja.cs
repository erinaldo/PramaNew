using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Prama.Clases;

namespace Prama.Formularios.Caja
{
    public partial class frmCuentasCaja : Form
    {
        #region Declaracion Variables Nivel Formulario

        //VARIABLES
        string myEstado = "C";
        int indexFila = 0;
        bool bSearch = false;
        int IdCuenta = 0;

        #endregion

        #region Constructor del formulario

        public frmCuentasCaja()
        {
            InitializeComponent();
        }

        #endregion

        #region Métodos del formulario

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
                    this.btnBuscar.TabStop = true  && (dgvCuentas.RowCount != 0);;
                    this.btnBuscar.Visible = true  && (dgvCuentas.RowCount != 0);;
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
                    this.txtNumero.TabStop = true;
                    this.txtNumero.Enabled = true;
                    this.txtCbu.TabStop = true;
                    this.txtCbu.Enabled = true;
                    this.txtTitular.TabStop = true;
                    this.txtTitular.Enabled = true;
                    this.txtCuit.TabStop = true;
                    this.txtCuit.Enabled = true;


                    this.txtBuscarNombre.TabStop = false;
                    this.txtBuscarNombre.Enabled = false;
                    this.txtBuscarCBU.TabStop = false;
                    this.txtBuscarCBU.Enabled = false;
                    
                    this.dgvCuentas.TabStop = false;
                    this.dgvCuentas.Enabled = false;
                    this.dgvCuentas.Height = 240;
                    this.gpbBusquedas.Visible = false;
                    return;
                case "B":
                    this.txtNombre.TabStop = false;
                    this.txtNombre.Enabled = false;
                    this.txtNumero.TabStop = false;
                    this.txtNumero.Enabled = false;
                    this.txtCbu.TabStop = false;
                    this.txtCbu.Enabled = false;
                    this.txtTitular.TabStop = false;
                    this.txtTitular.Enabled = false;
                    this.txtCuit.TabStop = false;
                    this.txtCuit.Enabled = false;

                    this.txtBuscarNombre.TabStop = true;
                    this.txtBuscarNombre.Enabled = true;
                    this.txtBuscarCBU.TabStop = true;
                    this.txtBuscarCBU.Enabled = true;
                    
                    this.dgvCuentas.TabStop = true && (dgvCuentas.RowCount > 0);
                    this.dgvCuentas.Enabled = true && (dgvCuentas.RowCount > 0);
                    this.dgvCuentas.Height = 180;
                    this.gpbBusquedas.Visible = true;
                    return;
                case "C":
                    this.txtNombre.TabStop = false;
                    this.txtNombre.Enabled = false;
                    this.txtNumero.TabStop = false;
                    this.txtNumero.Enabled = false;
                    this.txtCbu.TabStop = false;
                    this.txtCbu.Enabled = false;
                    this.txtTitular.TabStop = false;
                    this.txtTitular.Enabled = false;
                    this.txtCuit.TabStop = false;
                    this.txtCuit.Enabled = false;

                    this.txtBuscarNombre.TabStop = false;
                    this.txtBuscarNombre.Enabled = false;
                    this.txtBuscarCBU.TabStop = false;
                    this.txtBuscarCBU.Enabled = false;
                    
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
            this.txtNumero.Text = "";
            this.txtCbu.Text = "";
            this.txtTitular.Text = "";
            this.txtCuit.Text = "";
        }
        #endregion

        #region Método para limpiar los campos de Búsqueda
        // Limpia los controles de búsqueda del form
        private void LimpiarControlesBusqueda()
        {
            this.txtBuscarNombre.Text = "";
            this.txtBuscarCBU.Text = "";
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
                myCadena = "select * from CajaCuentas ";
            }
            else
            {
                // Cadena SQL 
                myCadena = "select * from CajaCuentas where " + Campo + " like '" + Buscar + "%' order by " + Campo;

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
                if (Convert.ToInt32(myRow.Cells["IdCajaTransferenciasCuenta"].Value.ToString()) == IdCuenta)
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
        
        #region Evento Load del formulario

        private void frmCuentasCaja_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 
			
            this.myEstado = "C";
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

        #endregion

        #region Eventos de la grilla

        #region Evento CellContentClick de la grilla

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
            txtNombre.Text = row.Cells["Nombre"].Value.ToString();
            txtNumero.Text = row.Cells["Numero"].Value.ToString();
            txtCbu.Text = row.Cells["CBU"].Value.ToString();
            txtTitular.Text = row.Cells["Titular"].Value.ToString();
            txtCuit.Text = row.Cells["Cuit"].Value.ToString();

        }

        #endregion

        #region Evento SelectionChanged de la grilla

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
            txtNombre.Text = row.Cells["Nombre"].Value.ToString();
            txtNumero.Text = row.Cells["Numero"].Value.ToString();
            txtCbu.Text = row.Cells["CBU"].Value.ToString();
            txtTitular.Text = row.Cells["Titular"].Value.ToString();
            txtCuit.Text = row.Cells["Cuit"].Value.ToString();
        }

        #endregion

        #region Evento KeyDown de la grilla

        private void dgvCuentas_KeyDown(object sender, KeyEventArgs e)
        {
            //PRESIONO DEL?
            if (e.KeyCode == Keys.Delete)
            {
                this.btnBorrar.PerformClick(); //LLAMAR A EVENTO CLICK DEL BOTON BORRAR
            }
        }

        #endregion

        #region Eventos de los campos de búsqueda

        private void txtBuscarNombre_TextChanged(object sender, EventArgs e)
        {
            if (!(this.txtBuscarNombre.Text == ""))
            {
                // Cargo las localidades filtradas por la búsqueda
                CargarGrilla(txtBuscarNombre.Text, "Nombre");
            }
            else
            {
                CargarGrilla("", "");
            }

        }

        private void txtBuscarCBU_TextChanged(object sender, EventArgs e)
        {
            if (!(this.txtBuscarCBU.Text == ""))
            {
                // Cargo las localidades filtradas por la búsqueda
                CargarGrilla(txtBuscarCBU.Text, "CBU");
            }
            else
            {
                CargarGrilla("", "");
            }
        }

        private void txtBuscarCBU_Click(object sender, EventArgs e)
        {
           txtBuscarNombre.Text = "";
            //CargarGrilla("", "");
        }

        private void txtBuscarNombre_Click(object sender, EventArgs e)
        {
           txtBuscarCBU.Text = "";
            //CargarGrilla("", "");
        }

        #endregion

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
                MessageBox.Show("Usted no tiene los permisos para crear una nueva Cuenta", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento Click del botón Editar

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvCuentas.Rows.Count != 0)
            {
                // Capturo la posición de la fila desde donde se llamo
                this.indexFila = dgvCuentas.CurrentRow.Index;
            }

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
                txtNombre.Text = row.Cells["Nombre"].Value.ToString();
                txtNumero.Text = row.Cells["Numero"].Value.ToString();
                txtTitular.Text = row.Cells["Titular"].Value.ToString();
                txtCbu.Text = row.Cells["CBU"].Value.ToString();
                txtCuit.Text = row.Cells["Cuit"].Value.ToString();

                // Posiciono el foco sobre el primer textbox
                txtNombre.Focus();
            }
            else
            {
                // Pregunto si el usuario actual tiene nivel menor o igual a 2, No lo dejo agregar. G.
                this.myEstado = "C";
                // El usuario no es de nivel 3, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para modificar una Cuenta", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento Click del botón Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (dgvCuentas.Rows.Count != 0)
            {
                // Capturo la posición de la fila desde donde se llamo
                this.indexFila = dgvCuentas.CurrentRow.Index;
            }
            // Cambio mi estado a Búsqueda
            this.myEstado = "B";
            // Habilito los botones según mi estado
            ActivarBotones();
            // Habilito los campos de búsqueda
            HabilitarControles();
            // Limpio los campos de búsqueda
            LimpiarControlesBusqueda();
            // Pongo el foco en el primer control de búsqueda
            txtBuscarNombre.Focus();
        }

        #endregion

        #region Evento Click del botón Imprimir

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //Data Set
            dsReportes oDsCuentas = new dsReportes();

            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = dgvCuentas.Rows.Count;

            for (int i = 0; i < dgvFilas; i++)
            {
                oDsCuentas.Tables["dtCuentas"].Rows.Add
                (new object[] { dgvCuentas[1,i].Value.ToString(),
                dgvCuentas[2,i].Value.ToString(),
                dgvCuentas[3,i].Value.ToString(),
                dgvCuentas[4,i].Value.ToString(),
                dgvCuentas[5,i].Value.ToString() });

            }

            //Objeto Reporte
            rptCuentas oRepCtas = new rptCuentas();
            //Cargar Reporte            
            //    oRepTipoCli.Load(Application.StartupPath + "\\rptTipoCli.rpt");
            oRepCtas.Load(Application.StartupPath + "\\rptCuentas.rpt");
            //Establecer el DataSet como DataSource
            oRepCtas.SetDataSource(oDsCuentas);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepCtas;
            oRepCtas.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepCtas.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepCtas.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
            oRepCtas.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
            oRepCtas.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
            oRepCtas.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
            oRepCtas.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepCtas.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
            oRepCtas.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";

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
            //int iIdCuenta = Convert.ToInt32(row.Cells["IdCajaTransferenciasCuenta"].Value);

            string sNombre = txtNombre.Text;
            string sNumero = txtNumero.Text;
            string sCbu = txtCbu.Text;
            string sTitular = txtTitular.Text;
            string sCuit = txtCuit.Text;


            switch (this.myEstado)
            {
                case "A":

                    // Creo la cadena para grabar el alta de la Localidad
                    myCadena = "INSERT INTO CajaCuentas (Nombre,Numero,CBU, Titular,Cuit) values ('"
                                    + sNombre + "','"
                                    + sNumero + "','"
                                    + sCbu + "','"
                                    + sTitular + "','"
                                    + sCuit + "')";
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
                    IdCuenta = Convert.ToInt32(dgvCuentas.CurrentRow.Cells["IdCajaTransferenciasCuenta"].Value.ToString());
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
                    IdCuenta = Convert.ToInt32(dgvCuentas.CurrentRow.Cells["IdCajaTransferenciasCuenta"].Value.ToString());
                    // Creo la cadena para grabar las Modificaciones de la Localidad
                    myCadena = "UPDATE CajaCuentas SET Nombre = '" + sNombre + "', Numero = '" +
                               sNumero + "', CBU = '" + sCbu + "', Titular = '" + sTitular + "', Cuit = '" + sCuit +

                               "' WHERE IdCajaCuenta = " + IdCuenta;
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


            //Nombre
            if (this.txtNombre.Text=="")
            {
                MessageBox.Show("Debe completar el dato 'Nombre'!","Atención!",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtNombre.Focus();
                bValida=false;
                //reornar valor
                return bValida;
                
            }

            //Numero
            if (this.txtNumero.Text=="")
            {
                MessageBox.Show("Debe completar el dato 'Número'!","Atención!",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtNumero.Focus();
                bValida=false;
                //reornar valor
                return bValida;
                
            }

            //CBU
            if (this.txtCbu.Text == "")
            {
                MessageBox.Show("Debe completar el dato 'CBU'!", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtCbu.Focus();
                bValida = false;
                //reornar valor 
                return bValida;

            }
            else  if (this.txtCbu.TextLength <22)
            {
                MessageBox.Show("EL largo del dato 'CBU' es incorrecto (ebe tener 22 posiciones)!", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtCbu.Focus();
                bValida = false;
                //reornar valor 
                return bValida;                
            }


            //TITULAR
            if (this.txtTitular.Text == "")
            {
                MessageBox.Show("Debe completar el dato 'Titular'!", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtTitular.Focus();
                bValida = false;
                //reornar valor
                return bValida;

            }

            //CUIT
            if (this.txtCuit.Text == "")
            {
                MessageBox.Show("Debe completar el dato 'CUIT'!", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtCuit.Focus();
                bValida = false;
                //reornar valor
                return bValida;

            }
            else
            { 
                //Validar la CUIT                
                clsCUIT oCUITvalido = new clsCUIT(this.txtCuit.ToString());

                if (!(oCUITvalido.EsValido))
                {
                    MessageBox.Show("El 'CUIT' ingresado es inválido!", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.txtCuit.Focus();
                    bValida = false;
                    //reornar valor
                    return bValida;
                }
                    
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
                // Llamo al método cargar Localidades para rellenar la grilla. G.
                CargarGrilla("", "");
                // Limpio los controles del formulario. G.    
                ActivarBotones();
                // Habilito los controles para este estado. G.
                HabilitarControles();

            }
            else
            {
                // Cambio el estado del formulario a agregar. G.
                this.myEstado = "C";
                // Llamo al método cargar Localidades para rellenar la grilla. G.
                CargarGrilla("", "");
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

        #region Evento Click del Salir

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Cierro el formulario. G.
            this.Close();
        }

        #endregion

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCbu_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtBuscarCBU_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtBuscarNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Space))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtTitular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar !=(char)Keys.Space))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
    }
}

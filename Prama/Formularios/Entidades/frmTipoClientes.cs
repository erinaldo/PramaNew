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
    public partial class frmTipoClientes : Form
    {
        int indexFila = 0;
        string myEstado = "C";

        public frmTipoClientes()
        {
            InitializeComponent();
        }

        #region Eventos del Formulario

        #region Evento Load

        private void frmTipoClientes_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            // Cargo la grilla
            CargarTipoCli();
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

        #region Evento Grillas

        private void dgvTipoCli_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvTipoCli.RowCount == 0)
            {
                return;
            }
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvTipoCli.CurrentRow;
            // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
            //TRAER LOS DATOS. H.
            txtTipo.Text = row.Cells["TipoCliente"].Value.ToString();
        }

        private void dgvTipoCli_SelectionChanged(object sender, EventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvTipoCli.RowCount == 0)
            {
                return;
            }
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvTipoCli.CurrentRow;
            // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
            //TRAER LOS DATOS. H.
            txtTipo.Text = row.Cells["TipoCliente"].Value.ToString();
        }

        #endregion

        #region Eventos Botones

        #region Evento Click del botón btnAgregar

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Capturo la posición de la fila actual
            this.indexFila = dgvTipoCli.CurrentRow.Index;
            
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
                txtTipo.Focus();
            }
            // Pregunto si el usuario actual tiene nivel menor o igual a 2, No lo dejo agregar. G.
            else
            {
                // vuelvo el formulario al estado anterior. G.
                this.myEstado = "C";
                // El usuario no es de nivel 3, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para crear un nuevo Tipo de Cliente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento Click del botón btnModificar

        private void btnModificar_Click(object sender, EventArgs e)
        {
            // Capturo la posición de la fila actual
            this.indexFila = dgvTipoCli.CurrentRow.Index;

            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvTipoCli.CurrentRow;
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvTipoCli.RowCount == 0)
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
                txtTipo.Text = row.Cells["TipoCliente"].Value.ToString();
                // Posiciono el foco sobre el primer textbox
                txtTipo.Focus();
            }
            else
            {
                // Pregunto si el usuario actual tiene nivel menor o igual a 2, No lo dejo agregar. G.
                this.myEstado = "C";
                // El usuario no es de nivel 3, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para modificar un Tipo de Cliente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento Click del botón btnAceptar

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Si la grilla no contiene ninguna fila, salgo del evento
            if (dgvTipoCli.RowCount == 0)
            {
                // Muestro un mensaje indicando que no se encontraron los datos
                MessageBox.Show("No se encontraron coincidencias", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Regreso el formulario a su estado inicial
                this.btnCancelar.PerformClick();
                return;
            }
            // Creo la cadena para grabar las Modificaciones de la Localidad
            string myCadena = "";
            // Creo una nueva Localidad. G.
            clsTiposClientes NuevoTipo = new clsTiposClientes();
            // Tomo la línea actual de la grilla. G.
            DataGridViewRow row = dgvTipoCli.CurrentRow;
            // Verifico el estado del formulario para saber si estoy creando o modificando una Localidad. G.
            // Paso los datos del formulario a la nueva Localidad
            NuevoTipo.IdTipoCliente = Convert.ToInt32(row.Cells["IdTipoCliente"].Value);
            NuevoTipo.TipoCliente = txtTipo.Text; //N

            //Vector Errores
            string[] cErrores = NuevoTipo.cValidaTipo();
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

            switch (this.myEstado)
            {
                case "A":
                    // Creo la cadena para grabar el alta de la Localidad
                    myCadena = "INSERT INTO TiposClientes (TipoCliente) values ('" + NuevoTipo.TipoCliente + "')";
                    // Ejecuto la consulta SQL
                    clsDataBD.GetSql(myCadena);
                    // Lleno nuevamente la grilla
                    this.CargarTipoCli();
                    // Regreso el formulario a su estado inicial
                    this.btnCancelar.PerformClick();
                    return;

                case "M":
                    // Creo la cadena para grabar las Modificaciones de la Localidad
                    myCadena = "UPDATE TiposClientes SET TipoCliente = '" + NuevoTipo.TipoCliente +
                                "' WHERE IdTipoCliente = " + NuevoTipo.IdTipoCliente;
                    // Ejecuto la consulta SQL
                    clsDataBD.GetSql(myCadena);
                    // Lleno nuevamente la grilla
                    this.CargarTipoCli();
                    // Regreso el formulario a su estado inicial
                    this.btnCancelar.PerformClick();
                    return;

            }
        }

        #endregion

        #region Evento Click del botón btnCancelar

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Cambio el estado del formulario a agregar. G.
            this.myEstado = "C";
            // Cargo las localidades
            this.CargarTipoCli();
            // Limpio los controles del formulario. G.    
            ActivarBotones();
            // Habilito los controles para este estado. G.
            HabilitarControles();
            // Pongo el foco en la fila desde donde se llamo
            PosicionarFocoFila();
        }

        #endregion

        #region Evento Click del botón btnSalir

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Cierro el formulario
            this.Close();
        }

        #endregion

        #region Evento Click del botón btnImprimir

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            // Capturo la posición de la fila actual
            this.indexFila = dgvTipoCli.CurrentRow.Index;
            
            //Data Set
            dsReportes oDsTipoCli = new dsReportes();

            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = dgvTipoCli.Rows.Count;

            for (int i = 0; i < dgvFilas; i++)
            {
                oDsTipoCli.Tables["DtTipoCli"].Rows.Add
                (new object[] { dgvTipoCli[1,i].Value.ToString(),
                dgvTipoCli[0,i].Value.ToString() });

            }

            //Objeto Reporte
            rptTipoCli oRepTipoCli = new rptTipoCli();
            //Cargar Reporte            
            //    oRepTipoCli.Load(Application.StartupPath + "\\rptTipoCli.rpt");
            oRepTipoCli.Load(Application.StartupPath + "\\rptTipoCli.rpt");
            //Establecer el DataSet como DataSource
            oRepTipoCli.SetDataSource(oDsTipoCli);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepTipoCli;
            oRepTipoCli.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepTipoCli.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepTipoCli.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
            oRepTipoCli.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
            oRepTipoCli.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
            oRepTipoCli.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
            oRepTipoCli.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepTipoCli.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
            oRepTipoCli.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();

            // Pongo el foco en la fila desde donde se llamo
            PosicionarFocoFila();

        }

        #endregion

        #endregion

        #region Evento Leave del txtTipo

        private void txtTipo_Leave(object sender, EventArgs e)
        {
            this.txtTipo.Text = txtTipo.Text.ToUpper();
        }

        #endregion

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
                    this.btnModificar.TabStop = true && (dgvTipoCli.RowCount != 0);
                    this.btnModificar.Visible = true && (dgvTipoCli.RowCount != 0);
                    this.btnAceptar.TabStop = false;
                    this.btnAceptar.Visible = false;
                    this.btnCancelar.TabStop = false;
                    this.btnCancelar.Visible = false;
                    this.btnSalir.TabStop = true;
                    this.btnSalir.Visible = true;
                    // Si el nivel del usuario es el 5, habilito el botón borrar
                 /*   if (clsGlobales.UsuarioLogueado.Nivel >=5)
                    {
                        this.btnBorrar.TabStop = true && (dgvTipoCli.RowCount != 0);
                        this.btnBorrar.Visible = true && (dgvTipoCli.RowCount != 0);
                    }
                    else
                    {
                        this.btnBorrar.TabStop = false;
                        this.btnBorrar.Visible = false;
                    }*/
                    this.btnImprimir.TabStop = true && (dgvTipoCli.RowCount != 0);
                    this.btnImprimir.Visible = true && (dgvTipoCli.RowCount != 0);
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
                    this.txtTipo.TabStop = true;
                    this.txtTipo.Enabled = true;
                    this.dgvTipoCli.TabStop = false;
                    this.dgvTipoCli.Enabled = false;

                    return;

                case "C":
                    this.txtTipo.TabStop = false;
                    this.txtTipo.Enabled = false;
                    this.dgvTipoCli.TabStop = true && (dgvTipoCli.RowCount > 0);
                    this.dgvTipoCli.Enabled = true && (dgvTipoCli.RowCount > 0);

                    return;
            }
        }
        #endregion

        #region Método para limpiar los controles del formulario
        //LIMPIA LA PROPIEDAD TEXT DE LOS CONTROLES DEL FORMULARIO. N.
        private void LimpiarControlesForm()
        {
            this.txtTipo.Text = "";
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
        }

        #endregion

        #region Método para cargar la grilla

        private void CargarTipoCli()
        {
            try
            {
                // Variable para la cadena SQL
                string myCadena = "";
                // Cadena SQL 
                myCadena = "select * from TiposClientes order by IdTipoCliente";

                // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
                DataTable mDtTable = new DataTable();
                mDtTable = clsDataBD.GetSql(myCadena);
                // Evito que el dgv genere columnas automáticas
                dgvTipoCli.AutoGenerateColumns = false;
                // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
                dgvTipoCli.DataSource = mDtTable;
                // Cuento la cantidad de filas de la grilla
                int Filas = dgvTipoCli.Rows.Count;
                // Posiciono la grilla en la última fila, si hay
                if (Filas > 0)
                {
                    //Posicionamiento grilla
                    int r = dgvTipoCli.CurrentCell.RowIndex;
                    int c = dgvTipoCli.CurrentCell.ColumnIndex;
                    dgvTipoCli.CurrentCell = dgvTipoCli.Rows[r].Cells[c];
                    //Mostrar datos  
                    this.txtTipo.Text = dgvTipoCli.CurrentRow.Cells["TipoCliente"].Value.ToString();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
        }

        #endregion

        #region Método que devuelve el foco a la fila desde donde se presionó algún botón

        private void PosicionarFocoFila()
        {
            // Devuelvo el foco a la fila de la grilla desde donde se llamó
            this.dgvTipoCli.CurrentCell = dgvTipoCli[0, this.indexFila];

            // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
            EventArgs ea = new EventArgs();
            this.dgvTipoCli_SelectionChanged(this.dgvTipoCli, ea);
        }

        #endregion

        #endregion

    }
}

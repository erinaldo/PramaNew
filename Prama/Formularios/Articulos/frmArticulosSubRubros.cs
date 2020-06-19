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
    public partial class frmArticulosSubRubros : Form
    {

        #region Variables Nivel Formulario

        //Variables nivel formulario
        string myEstado = "C";

        #endregion

        public frmArticulosSubRubros()
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
                    this.btnModificar.TabStop = true && (dgvSubRubros.RowCount != 0);
                    this.btnModificar.Visible = true && (dgvSubRubros.RowCount != 0);
                    this.btnBuscar.TabStop = true;
                    this.btnBuscar.Visible = true;
                    this.btnAceptar.TabStop = false;
                    this.btnAceptar.Visible = false;
                    this.btnCancelar.TabStop = false;
                    this.btnCancelar.Visible = false;
                    this.btnSalir.TabStop = true;
                    this.btnSalir.Visible = true;
                    // Si el nivel del usuario es el 5, habilito el botón borrar
                   /* if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja)
                    {
                        this.btnBorrar.TabStop = true && (dgvSubRubros.RowCount != 0);
                        this.btnBorrar.Visible = true && (dgvSubRubros.RowCount != 0);
                    }
                    else
                    {
                        this.btnBorrar.TabStop = false;
                        this.btnBorrar.Visible = false;
                    }*/
                    this.btnImprimir.TabStop = true && (dgvSubRubros.RowCount != 0);
                    this.btnImprimir.Visible = true && (dgvSubRubros.RowCount != 0);
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
                    this.txtSubRubro.TabStop = true;
                    this.txtSubRubro.Enabled = true;
                    this.txtBuscarSubRubro.TabStop = false;
                    this.txtBuscarSubRubro.Enabled = false;
                    this.cboRubros.TabStop = true;
                    this.cboRubros.Enabled = true;
                    this.dgvSubRubros.TabStop = false;
                    this.dgvSubRubros.Enabled = false;
                    this.dgvSubRubros.Height = 240;
                    this.gpbBusquedas.Visible = false;
                    return;
                case "B":
                    this.txtSubRubro.TabStop = false;
                    this.txtSubRubro.Enabled = false;
                    this.txtBuscarSubRubro.TabStop = true;
                    this.txtBuscarSubRubro.Enabled = true;
                    this.cboRubros.TabStop = false;
                    this.cboRubros.Enabled = false;
                    this.dgvSubRubros.TabStop = true && (dgvSubRubros.RowCount > 0);
                    this.dgvSubRubros.Enabled = true && (dgvSubRubros.RowCount > 0);
                    this.dgvSubRubros.Height = 180;
                    this.gpbBusquedas.Visible = true;
                    return;
                case "C":
                    this.txtSubRubro.TabStop = false;
                    this.txtSubRubro.Enabled = false;
                    this.txtBuscarSubRubro.TabStop = false;
                    this.txtBuscarSubRubro.Enabled = false;
                    this.cboRubros.TabStop = false;
                    this.cboRubros.Enabled = false;
                    this.dgvSubRubros.TabStop = true && (dgvSubRubros.RowCount > 0);
                    this.dgvSubRubros.Enabled = true && (dgvSubRubros.RowCount > 0);
                    this.dgvSubRubros.Height = 240;
                    this.gpbBusquedas.Visible = false;
                    return;
            }
        }
        #endregion

        #region Método para limpiar los controles del formulario
        //LIMPIA LA PROPIEDAD TEXT DE LOS CONTROLES DEL FORMULARIO. N.
        private void LimpiarControlesForm()
        {
            this.txtSubRubro.Text = "";
            this.cboRubros.SelectedIndex = -1;
        }
        #endregion

        #region Método para limpiar los campos de Búsqueda
        // Limpia los controles de búsqueda del form
        private void LimpiarControlesBusqueda()
        {
            this.txtBuscarSubRubro.Text = "";
        }

        #endregion

        #region Método para cargar los Rubros al combo cboRubros

        private void CargarProvincias()
        {
            // Cargo el combo
            clsDataBD.CargarCombo(cboRubros, "RubrosArticulos", "RubroArticulo", "IdRubroArticulo");
        }

        #endregion

        #region Método para cargar la grilla

        private void CargarSubRubros(string Buscar, string Campo)
        {
            // Variable para la cadena SQL
            string myCadena = "";
            if (Buscar == "")
            {
                // Cadena SQL 
                myCadena = "select * from Vista_SubRubros_Rubros ORDER BY RubroArticulo ASC";
            }
            else
            {
                // Cadena SQL 
                myCadena = "select * from Vista_SubRubros_Rubros where " + Campo + " like '" + Buscar + "%' order by " + Campo;
            }

            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            mDtTable = clsDataBD.GetSql(myCadena);
            // Evito que el dgv genere columnas automáticas
            dgvSubRubros.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvSubRubros.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvSubRubros.Rows.Count;
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

        #endregion

        #region Eventos del Formulario

        #region Evento Load del Formulario

        private void frmArticulosSubRubros_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            this.myEstado = "C";
            // Cargo el combo con las provincias
            CargarProvincias();
            // Llamo al método cargar Localidades para rellenar la grilla. G.
            CargarSubRubros("", "");
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

        #region Eventos de la grilla

        #region Evento CellContentClick de la grilla

        private void dgvSubRubros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvSubRubros.RowCount == 0)
            {
                return;
            }
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvSubRubros.CurrentRow;
            // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
            //TRAER LOS DATOS. H.
            txtSubRubro.Text = row.Cells["SubRubroArticulo"].Value.ToString();
            cboRubros.SelectedValue = Convert.ToInt32(row.Cells["IdRubroArticulo"].Value);
        }

        #endregion

        #region Evento SelectionChanged de la grilla

        private void dgvSubRubros_SelectionChanged(object sender, EventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvSubRubros.RowCount == 0)
            {
                return;
            }
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvSubRubros.CurrentRow;
            // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
            //TRAER LOS DATOS. H.
            txtSubRubro.Text = row.Cells["SubRubroArticulo"].Value.ToString();
            cboRubros.SelectedValue = Convert.ToInt32(row.Cells["IdRubroArticulo"].Value);
        }

        #endregion

        #region Evento KeyDown de la grilla

        private void dgvSubRubros_KeyDown(object sender, KeyEventArgs e)
        {
            //PRESIONO DEL?
            if (e.KeyCode == Keys.Delete)
            {
                this.btnBorrar.PerformClick(); //LLAMAR A EVENTO CLICK DEL BOTON BORRAR
            }
        }

        #endregion

        #endregion

        #region Eventos de los botones
                
        #region Evento Click del botón Agregar

        private void btnAgregar_Click(object sender, EventArgs e)
        {
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
                txtSubRubro.Focus();
            }
            // Pregunto si el usuario actual tiene nivel menor o igual a 2, No lo dejo agregar. G.
            else
            {
                // vuelvo el formulario al estado anterior. G.
                this.myEstado = "C";
                // El usuario no es de nivel 3, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para crear un nuevo Sub Rubro", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento Click del botón Modificar

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvSubRubros.CurrentRow;
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvSubRubros.RowCount == 0)
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
                txtSubRubro.Text = row.Cells["SubRubroArticulo"].Value.ToString();
                cboRubros.Text = row.Cells["IdRubroArticulo"].Value.ToString();
                // Posiciono el foco sobre el primer textbox
                txtSubRubro.Focus();
            }
            else
            {
                // Pregunto si el usuario actual tiene nivel menor o igual a 2, No lo dejo agregar. G.
                this.myEstado = "C";
                // El usuario no es de nivel 3, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para modificar un Sub Rubro", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento Click del botón Borrar

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            // Pregunto si el usuario actual es el mismo que se quiere modificar. G.
            if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja)
            {
                //TRAER LA FILA ACTUAL. N.
                DataGridViewRow row = dgvSubRubros.CurrentRow;
                // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
                int Id = Convert.ToInt32(row.Cells["IdSubRubroArticulo"].Value);
                string SubRub = row.Cells["SubRubroArticulo"].Value.ToString();
                int Niv = clsGlobales.UsuarioLogueado.Nivel;

               if (Niv < clsGlobales.cParametro.NivelBaja)
                {
                    // El usuario no es de nivel 5, por lo que no puede crear un nuevo usuario. G.
                    MessageBox.Show("Usted no tiene los permisos para Eliminar este Sub Rubro", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Ejecuto el botón cancelar. G.
                    this.btnCancelar.PerformClick();
                }
                else
                {
                    // Confirma eliminar el registro ?
                    DialogResult dlResult = MessageBox.Show("Desea Eliminar el Sub Rubro " + SubRub + " ?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    // Si confirma salir de la aplicación....
                    if (dlResult == DialogResult.Yes)
                    {
                        string myCadena = "UPDATE SubRubrosArticulos SET Activo = 0 WHERE SubRubrosArticulos.IdSubRubroArticulo =" + Id;
                        clsDataBD.GetSql(myCadena);
                        // Refresco la grilla
                        CargarSubRubros("", "");
                    }
                }

            }
            else
            {
                // El usuario no es de nivel 5, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para Eliminar un Sub Rubro", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento Click del botón Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Cambio mi estado a Búsqueda
            this.myEstado = "B";
            // Habilito los botones según mi estado
            ActivarBotones();
            // Habilito los campos de búsqueda
            HabilitarControles();
            // Limpio los campos de búsqueda
            LimpiarControlesBusqueda();
            // Pongo el foco en el primer control de búsqueda
            txtBuscarSubRubro.Focus();
        }

        #endregion

        #region Evento Click del botón Aceptar

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string pWhere = "";

            // Si la grilla no contiene ninguna fila, salgo del evento
            if (dgvSubRubros.RowCount == 0)
            {
                // Muestro un mensaje indicando que no se encontraron los datos
                MessageBox.Show("No se encontraron coincidencias", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Regreso el formulario a su estado inicial
                this.btnBuscar.PerformClick();
                return;
            }
            // Creo la cadena para grabar las Modificaciones de la Localidad
            string myCadena = "";
            // Creo una nueva Localidad. G.
            clsArticulosSubrubros NuevaSubRubro = new clsArticulosSubrubros();
            // Tomo la línea actual de la grilla. G.
            DataGridViewRow row = dgvSubRubros.CurrentRow;
            // Verifico el estado del formulario para saber si estoy creando o modificando una Localidad. G.
            // Paso los datos del formulario a la nueva Localidad
            NuevaSubRubro.IdSubRubroArticulo = Convert.ToInt32(row.Cells["IdSubRubroArticulo"].Value);
            NuevaSubRubro.SubrRubroArticulo = txtSubRubro.Text;
            NuevaSubRubro.IdRubroArticulo = Convert.ToInt32(cboRubros.SelectedValue);

            //Vector Errores
            string[] cErrores = NuevaSubRubro.cValidaSubRubros();
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

                    //Clausula Where 
                    pWhere = "IdRubroArticulo = " + NuevaSubRubro.IdRubroArticulo;

                    //Orden ( traer el maximo para el IdRubroArticulo correspondiente )
                    NuevaSubRubro.Orden = clsDataBD.RetornarMax("SubRubrosArticulos", "Orden",pWhere) + 1;

                    // Creo la cadena para grabar el alta de la Localidad
                    myCadena = "INSERT INTO SubRubrosArticulos (SubRubroArticulo, IdRubroArticulo, Activo, Orden) values ('" + NuevaSubRubro.SubrRubroArticulo +
                                      "', " + NuevaSubRubro.IdRubroArticulo + ",1," + NuevaSubRubro.Orden + ")";
                    // Ejecuto la consulta SQL
                    clsDataBD.GetSql(myCadena);
                    // Lleno nuevamente la grilla
                    CargarSubRubros("", "");
                    // Regreso el formulario a su estado inicial
                    this.btnCancelar.PerformClick();
                    return;

                case "B":
                    // Cambio mi estado
                    this.myEstado = "C";
                    // Activo todos los botones
                    ActivarBotones();
                    // Habilito los controles
                    HabilitarControles();
                    this.btnCancelar.Visible = true;

                    return;

                case "M":
                    // Creo la cadena para grabar las Modificaciones de la Localidad
                    myCadena = "UPDATE SubRubrosArticulos SET SubRubroArticulo = '" + NuevaSubRubro.SubrRubroArticulo + "', IdRubroArticulo = " +
                               NuevaSubRubro.IdRubroArticulo + " WHERE IdSubRubroArticulo = " + NuevaSubRubro.IdSubRubroArticulo;
                    // Ejecuto la consulta SQL
                    clsDataBD.GetSql(myCadena);
                    // Lleno nuevamente la grilla
                    CargarSubRubros("", "");
                    // Regreso el formulario a su estado inicial
                    this.btnCancelar.PerformClick();
                    return;

            }
        }

        #endregion

        #region Evento Click del botón Cancelar

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Cambio el estado del formulario a agregar. G.
            this.myEstado = "C";
            // Cargo las localidades
            CargarSubRubros("", "");
            // Limpio los controles del formulario. G.    
            ActivarBotones();
            // Habilito los controles para este estado. G.
            HabilitarControles();
        }

        #endregion

        #region Evento Click del botón Salir

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Cierro el formulario
            this.Close();
        }

        #endregion

        #endregion

        #region Eventos de los controles

        #region Eventos Leave de los textbox

        private void txtSubRubro_Leave(object sender, EventArgs e)
        {
            // Cuando se sale del campo, pongo el texto a mayúsculas
            txtSubRubro.Text = txtSubRubro.Text.ToUpper();
        }

        #endregion

        #region Eventos KeyPress de los textbox

        // txtSubRubro
        private void txtSubRubro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                this.cboRubros.Focus();
            }
        }

        #endregion

        #region Eventos TextChanged de los textbox

        private void txtBuscarSubRubro_TextChanged(object sender, EventArgs e)
        {
            // Cargo las localidades filtradas por la búsqueda
            CargarSubRubros(txtBuscarSubRubro.Text.ToUpper(), "SubRubroArticulo");
        }

        #endregion

        #region Evento BtnImprimir Click

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //Data Set
            dsReportes oDsSubRubro = new dsReportes();

            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = dgvSubRubros.Rows.Count;

            for (int i = 0; i < dgvFilas; i++)
            {
                oDsSubRubro.Tables["DtSubRubros"].Rows.Add
                (new object[] { dgvSubRubros[1,i].Value.ToString(),
                dgvSubRubros[3,i].Value.ToString() });

            }

            //Objeto Reporte
            rptSubRubros oRepSubRubro = new rptSubRubros();
            //Cargar Reporte            
            //    oRepTipoCli.Load(Application.StartupPath + "\\rptTipoCli.rpt");
            oRepSubRubro.Load( Application.StartupPath + "\\rptSubRubros.rpt");
            //Establecer el DataSet como DataSource
            oRepSubRubro.SetDataSource(oDsSubRubro);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepSubRubro;
            oRepSubRubro.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepSubRubro.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
            oRepSubRubro.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
            oRepSubRubro.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
            oRepSubRubro.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
            oRepSubRubro.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepSubRubro.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
            oRepSubRubro.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();
        }

        #endregion

        #endregion

        #endregion
    }
}

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
    public partial class frmArticulosRubros : Form
    {
        #region Variables Nivel Formulario

        //Variables nivel formulario
        string myEstado = "C";

        #endregion

        public frmArticulosRubros()
        {
            InitializeComponent();
        }

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
                    this.btnModificar.TabStop = true && (dgvRubros.RowCount != 0);
                    this.btnModificar.Visible = true && (dgvRubros.RowCount != 0);
                    this.btnAceptar.TabStop = false;
                    this.btnAceptar.Visible = false;
                    this.btnCancelar.TabStop = false;
                    this.btnCancelar.Visible = false;
                    this.btnSalir.TabStop = true;
                    this.btnSalir.Visible = true;
                    // Si el nivel del usuario es el 5, habilito el botón borrar
                    if (clsGlobales.UsuarioLogueado.Nivel >=clsGlobales.cParametro.NivelBaja)
                    {
                        this.btnBorrar.TabStop = true && (dgvRubros.RowCount != 0);
                        this.btnBorrar.Visible = true && (dgvRubros.RowCount != 0);
                    }
                    else
                    {
                        this.btnBorrar.TabStop = false;
                        this.btnBorrar.Visible = false;
                    }
                    this.btnImprimir.TabStop = true && (dgvRubros.RowCount != 0);
                    this.btnImprimir.Visible = true && (dgvRubros.RowCount != 0);
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
                    this.txtRubro.TabStop = true;
                    this.txtRubro.Enabled = true;
                    this.dgvRubros.TabStop = false;
                    this.dgvRubros.Enabled = false;

                    return;
                case "B":
                    this.txtRubro.TabStop = false;
                    this.txtRubro.Enabled = false;
                    this.dgvRubros.TabStop = true && (dgvRubros.RowCount > 0);
                    this.dgvRubros.Enabled = true && (dgvRubros.RowCount > 0);
                   
                    return;
                case "C":
                    this.txtRubro.TabStop = false;
                    this.txtRubro.Enabled = false;
                    this.dgvRubros.TabStop = true && (dgvRubros.RowCount > 0);
                    this.dgvRubros.Enabled = true && (dgvRubros.RowCount > 0);
                    
                    return;
            }
        }
        #endregion

        #region Método para limpiar los controles del formulario
        //LIMPIA LA PROPIEDAD TEXT DE LOS CONTROLES DEL FORMULARIO. N.
        private void LimpiarControlesForm()
        {
            this.txtRubro.Text = "";
        }
        #endregion

        #region Método para cargar la grilla

        private void CargarRubros()
        {
            // Variable para la cadena SQL
            string myCadena = "";
            // Cadena SQL 
            myCadena = "select * from RubrosArticulos where Activo = 1";

            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            mDtTable = clsDataBD.GetSql(myCadena);
            // Evito que el dgv genere columnas automáticas
            dgvRubros.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvRubros.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvRubros.Rows.Count;
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
        }

        #endregion

        #endregion

        #region Eventos del formulario

        #region Evento Load

        private void frmArticulosRubros_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            // Cargo la grilla
            CargarRubros();
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

        private void dgvRubros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvRubros.RowCount == 0)
            {
                return;
            }
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvRubros.CurrentRow;
            // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
            //TRAER LOS DATOS. H.
            txtRubro.Text = row.Cells["RubroArticulo"].Value.ToString();
            
        }

        #endregion

        #region Evento SelectionChanged de la grilla

        private void dgvRubros_SelectionChanged(object sender, EventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvRubros.RowCount == 0)
            {
                return;
            }
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvRubros.CurrentRow;
            // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
            //TRAER LOS DATOS. H.
            txtRubro.Text = row.Cells["RubroArticulo"].Value.ToString();
        }

        #endregion

        #region Evento KeyDown de la grilla

        private void dgvRubros_KeyDown(object sender, KeyEventArgs e)
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
                txtRubro.Focus();
            }
            // Pregunto si el usuario actual tiene nivel menor o igual a 2, No lo dejo agregar. G.
            else
            {
                // vuelvo el formulario al estado anterior. G.
                this.myEstado = "C";
                // El usuario no es de nivel 3, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para crear un nuevo Rubro", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento Click del botón Modificar

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvRubros.CurrentRow;
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvRubros.RowCount == 0)
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
                txtRubro.Text = row.Cells["RubroArticulo"].Value.ToString();
                // Posiciono el foco sobre el primer textbox
                txtRubro.Focus();
            }
            else
            {
                // Pregunto si el usuario actual tiene nivel menor o igual a 2, No lo dejo agregar. G.
                this.myEstado = "C";
                // El usuario no es de nivel 3, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para modificar un Rubro", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento Click del botón Borrar

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            // Pregunto si el usuario actual es el mismo que se quiere modificar. G.
            if (clsGlobales.UsuarioLogueado.Nivel >=clsGlobales.cParametro.NivelBaja)
            {
                //TRAER LA FILA ACTUAL. N.
                DataGridViewRow row = dgvRubros.CurrentRow;
                // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
                int Id = Convert.ToInt32(row.Cells["IdRubroArticulo"].Value);
                string Rubro = row.Cells["RubroArticulo"].Value.ToString();
                int Niv = clsGlobales.UsuarioLogueado.Nivel;

               if (Niv < clsGlobales.cParametro.NivelBaja)
                {
                    // El usuario no es de nivel 5, por lo que no puede crear un nuevo usuario. G.
                    MessageBox.Show("Usted no tiene los permisos para Eliminar este Rubro", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Ejecuto el botón cancelar. G.
                    this.btnCancelar.PerformClick();
                }
                else
                {
                    // Confirma eliminar el registro ?
                    DialogResult dlResult = MessageBox.Show("Desea Eliminar el Rubro " + Rubro + " ?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    // Si confirma salir de la aplicación....
                    if (dlResult == DialogResult.Yes)
                    {
                        string myCadena = "UPDATE RubrosArticulos SET Activo = 0 WHERE IdRubroArticulo =" + Id;
                        clsDataBD.GetSql(myCadena);
                        // Refresco la grilla
                        CargarRubros();
                    }
                }

            }
            else
            {
                // El usuario no es de nivel 5, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para Eliminar un Rubro", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento Click del botón Aceptar

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Si la grilla no contiene ninguna fila, salgo del evento
            if (dgvRubros.RowCount == 0)
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
            clsArticulosRubros NuevoRubro = new clsArticulosRubros();
            // Tomo la línea actual de la grilla. G.
            DataGridViewRow row = dgvRubros.CurrentRow;
            // Verifico el estado del formulario para saber si estoy creando o modificando una Localidad. G.
            // Paso los datos del formulario a la nueva Localidad
            NuevoRubro.IdRubroArticulo = Convert.ToInt32(row.Cells["IdRubroArticulo"].Value);
            NuevoRubro.RubroArticulo = txtRubro.Text; //N

                
            //Vector Errores
            string[] cErrores = NuevoRubro.cValidaRubro();
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

                    //Orden
                    NuevoRubro.Orden = clsDataBD.RetornarMax("RubrosArticulos", "Orden") + 1;
                    
                    // Creo la cadena para grabar el alta de la Localidad
                    myCadena = "INSERT INTO RubrosArticulos (RubroArticulo,Activo,Orden) values ('" + NuevoRubro.RubroArticulo + "',1," +
                                       NuevoRubro.Orden + ")";
                    // Ejecuto la consulta SQL
                    clsDataBD.GetSql(myCadena);
                    // Lleno nuevamente la grilla
                    CargarRubros();
                    // Regreso el formulario a su estado inicial
                    this.btnCancelar.PerformClick();
                    return;

                case "M":
                    // Creo la cadena para grabar las Modificaciones de la Localidad
                    myCadena = "UPDATE RubrosArticulos SET RubroArticulo = '" + NuevoRubro.RubroArticulo + 
                                "' WHERE IdRubroArticulo = " + NuevoRubro.IdRubroArticulo;
                    // Ejecuto la consulta SQL
                    clsDataBD.GetSql(myCadena);
                    // Lleno nuevamente la grilla
                    CargarRubros();
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
            CargarRubros();
            // Limpio los controles del formulario. G.    
            ActivarBotones();
            // Habilito los controles para este estado. G.
            HabilitarControles();
        }

        #endregion

        #region Evento Click del botón Salir

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Cierro el formulario. G.
            this.Close();
        }

        #endregion
                        
        #endregion

        #region Eventos de los controles

        #region Evento Leave del txtRubro

        private void txtRubro_Leave(object sender, EventArgs e)
        {
            // Pongo en mayúsculas el texto del campo
            txtRubro.Text = txtRubro.Text.ToUpper();
        }

        #endregion

        #region Evento KeyPress del txtRubro

        private void txtRubro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                // Pongo rl foco en el siguiente control
                this.btnAceptar.Focus();
            }
        }

        #endregion

        private void btnImprimir_Click(object sender, EventArgs e)
        {

            //Data Set
            dsReportes oDsRubro = new dsReportes();

            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = dgvRubros.Rows.Count;

            for (int i = 0; i < dgvFilas; i++)
            {
                oDsRubro.Tables["DtRubros"].Rows.Add
                (new object[] { dgvRubros[0,i].Value.ToString(),
                dgvRubros[1,i].Value.ToString() });

            }

            //Objeto Reporte
            rptRubros oRepRubro = new rptRubros();
            //Cargar Reporte            
            //    oRepTipoCli.Load(Application.StartupPath + "\\rptTipoCli.rpt");
            oRepRubro.Load(Application.StartupPath + "\\rptRubros.rpt");
            //Establecer el DataSet como DataSource
            oRepRubro.SetDataSource(oDsRubro);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepRubro;
            oRepRubro.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepRubro.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
            oRepRubro.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
            oRepRubro.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
            oRepRubro.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
            oRepRubro.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepRubro.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
            oRepRubro.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();

        }

        #endregion

        #endregion
    }
}

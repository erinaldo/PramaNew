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
    public partial class frmArticulosAreasProduccion : Form
    {
        #region Variables Nivel Formulario

        //Variables nivel formulario
        string myEstado = "C";


        #endregion

        public frmArticulosAreasProduccion()
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
                    this.btnModificar.TabStop = true && (dgvAreasProduccion.RowCount != 0);
                    this.btnModificar.Visible = true && (dgvAreasProduccion.RowCount != 0);
                    this.btnAceptar.TabStop = false;
                    this.btnAceptar.Visible = false;
                    this.btnCancelar.TabStop = false;
                    this.btnCancelar.Visible = false;
                    this.btnSalir.TabStop = true;
                    this.btnSalir.Visible = true;
                    // Si el nivel del usuario es el 5, habilito el botón borrar
                    if (clsGlobales.UsuarioLogueado.Nivel >=clsGlobales.cParametro.NivelBaja)
                    {
                        this.btnBorrar.TabStop = true && (dgvAreasProduccion.RowCount != 0);
                        this.btnBorrar.Visible = true && (dgvAreasProduccion.RowCount != 0);
                    }
                    else
                    {
                        this.btnBorrar.TabStop = false;
                        this.btnBorrar.Visible = false;
                    }
                    this.btnImprimir.TabStop = true && (dgvAreasProduccion.RowCount != 0);
                    this.btnImprimir.Visible = true && (dgvAreasProduccion.RowCount != 0);
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
                    this.txtArea.TabStop = true;
                    this.txtArea.Enabled = true;
                    this.dgvAreasProduccion.TabStop = false;
                    this.dgvAreasProduccion.Enabled = false;

                    return;
                
                case "C":
                    this.txtArea.TabStop = false;
                    this.txtArea.Enabled = false;
                    this.dgvAreasProduccion.TabStop = true && (dgvAreasProduccion.RowCount > 0);
                    this.dgvAreasProduccion.Enabled = true && (dgvAreasProduccion.RowCount > 0);

                    return;
            }
        }
        #endregion

        #region Método para limpiar los controles del formulario
        //LIMPIA LA PROPIEDAD TEXT DE LOS CONTROLES DEL FORMULARIO. N.
        private void LimpiarControlesForm()
        {
            this.txtArea.Text = "";
        }
        #endregion

        #region Método para cargar la grilla

        private void CargarAreas()
        {
            // Variable para la cadena SQL
            string myCadena = "";
            // Cadena SQL 
            myCadena = "select * from AreasProduccion where Activo = 1";

            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            mDtTable = clsDataBD.GetSql(myCadena);
            // Evito que el dgv genere columnas automáticas
            dgvAreasProduccion.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvAreasProduccion.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvAreasProduccion.Rows.Count;
            // Posiciono la grilla en la última fila, si hay
            if (Filas > 0)
            {
                //Posicionamiento grilla
                int r = dgvAreasProduccion.CurrentCell.RowIndex;
                int c = dgvAreasProduccion.CurrentCell.ColumnIndex;
                dgvAreasProduccion.CurrentCell = dgvAreasProduccion.Rows[r].Cells[c];
                //Mostrar datos  
                this.txtArea.Text = dgvAreasProduccion.CurrentRow.Cells["AreaProduccion"].Value.ToString();
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
        }

        #endregion

        #endregion

        #region Eventos del formulario

        #region Evento Load

        private void frmArticulosAreasProduccion_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 
            // Cargo la grilla
            CargarAreas();
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

        private void dgvAreasProduccion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvAreasProduccion.RowCount == 0)
            {
                return;
            }
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvAreasProduccion.CurrentRow;
            // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
            //TRAER LOS DATOS. H.
            txtArea.Text = row.Cells["AreaProduccion"].Value.ToString();
        }

        #endregion

        #region Evento SelectionChanged de la grilla

        private void dgvAreasProduccion_SelectionChanged(object sender, EventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvAreasProduccion.RowCount == 0)
            {
                return;
            }
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvAreasProduccion.CurrentRow;
            // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
            //TRAER LOS DATOS. H.
            txtArea.Text = row.Cells["AreaProduccion"].Value.ToString();
        }

        #endregion

        #region Evento KeyDown de la grilla

        private void dgvAreasProduccion_KeyDown(object sender, KeyEventArgs e)
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
                txtArea.Focus();
            }
            // Pregunto si el usuario actual tiene nivel menor o igual a 2, No lo dejo agregar. G.
            else
            {
                // vuelvo el formulario al estado anterior. G.
                this.myEstado = "C";
                // El usuario no es de nivel 3, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para crear una nueva Área de Producción", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento Click del botón Modificar

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvAreasProduccion.CurrentRow;
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvAreasProduccion.RowCount == 0)
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
                txtArea.Text = row.Cells["AreaProduccion"].Value.ToString();
                // Posiciono el foco sobre el primer textbox
                txtArea.Focus();
            }
            else
            {
                // Pregunto si el usuario actual tiene nivel menor o igual a 2, No lo dejo agregar. G.
                this.myEstado = "C";
                // El usuario no es de nivel 3, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para modificar una Área de Produción", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                DataGridViewRow row = dgvAreasProduccion.CurrentRow;
                // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
                int Id = Convert.ToInt32(row.Cells["IdAreaProduccion"].Value);
                string Area = row.Cells["AreaProduccion"].Value.ToString();
                int Niv = clsGlobales.UsuarioLogueado.Nivel;

               if (Niv < clsGlobales.cParametro.NivelBaja)
                {
                    // El usuario no es de nivel 5, por lo que no puede crear un nuevo usuario. G.
                    MessageBox.Show("Usted no tiene los permisos para Eliminar esta Área de Producción", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Ejecuto el botón cancelar. G.
                    this.btnCancelar.PerformClick();
                }
                else
                {
                    // Confirma eliminar el registro ?
                    DialogResult dlResult = MessageBox.Show("Desea Eliminar el Área de Producción " + Area + " ?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    // Si confirma salir de la aplicación....
                    if (dlResult == DialogResult.Yes)
                    {
                        string myCadena = "UPDATE AreasProduccion SET Activo = 0 WHERE IdAreaProduccion =" + Id;
                        clsDataBD.GetSql(myCadena);
                        // Refresco la grilla
                        CargarAreas();
                    }
                }

            }
            else
            {
                // El usuario no es de nivel 5, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para Eliminar un Área de Producción", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento Click del botón Aceptar

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Si la grilla no contiene ninguna fila, salgo del evento
            if (dgvAreasProduccion.RowCount == 0)
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
            clsArticulosAreasProduccion NuevaArea = new clsArticulosAreasProduccion();
            // Tomo la línea actual de la grilla. G.
            DataGridViewRow row = dgvAreasProduccion.CurrentRow;
            // Verifico el estado del formulario para saber si estoy creando o modificando una Localidad. G.
            // Paso los datos del formulario a la nueva Localidad
            NuevaArea.IdAreaProduccion = Convert.ToInt32(row.Cells["IdAreaProduccion"].Value);
            NuevaArea.AreasProduccion = txtArea.Text; //N

            //Vector Errores
            string[] cErrores = NuevaArea.cValidaArea();
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
                    myCadena = "INSERT INTO AreasProduccion (AreaProduccion, Activo) values ('" + NuevaArea.AreasProduccion +
                                      "', " + "1)";
                    // Ejecuto la consulta SQL
                    clsDataBD.GetSql(myCadena);
                    // Lleno nuevamente la grilla
                    CargarAreas();
                    // Regreso el formulario a su estado inicial
                    this.btnCancelar.PerformClick();
                    return;

                case "M":
                    // Creo la cadena para grabar las Modificaciones de la Localidad
                    myCadena = "UPDATE AreasProduccion SET AreaProduccion = '" + NuevaArea.AreasProduccion +
                                "' WHERE IdAreaProduccion = " + NuevaArea.IdAreaProduccion;
                    // Ejecuto la consulta SQL
                    clsDataBD.GetSql(myCadena);
                    // Lleno nuevamente la grilla
                    CargarAreas();
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
            CargarAreas();
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

        #region Evento Leave del txtArea

        private void txtArea_Leave(object sender, EventArgs e)
        {
            // Pongo en mayúsculas el texto del campo
            this.txtArea.Text = txtArea.Text.ToUpper();
        }

        #endregion

        #region Evento KeyPress del txtRubro

        private void txtArea_KeyPress(object sender, KeyPressEventArgs e)
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
            dsReportes oDsArea = new dsReportes();

            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = dgvAreasProduccion.Rows.Count;

            for (int i = 0; i < dgvFilas; i++)
            {
                oDsArea.Tables["DtAreas"].Rows.Add
                (new object[] { dgvAreasProduccion[0,i].Value.ToString(),
                dgvAreasProduccion[1,i].Value.ToString() });

            }

            //Objeto Reporte
            rptAreaProd oRepAreas = new rptAreaProd();
            //Cargar Reporte            
            oRepAreas.Load(Application.StartupPath + "\\rptAreaProd.rpt");
            //Establecer el DataSet como DataSource
            oRepAreas.SetDataSource(oDsArea);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepAreas;
            oRepAreas.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepAreas.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
            oRepAreas.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
            oRepAreas.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
            oRepAreas.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
            oRepAreas.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepAreas.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
            oRepAreas.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();
        }

        #endregion

        #endregion

    }
}

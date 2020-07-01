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
    public partial class frmLocalidades : Form
    {
        
        #region Declaracion Variables Nivel Formulario

        //VARIABLES
        string myEstado = "C";
        int indexFila = 0;
        bool bSearch=false;
        int IdLoc = 0;
        int x_Val = 0;

        #endregion

        public frmLocalidades(int p_Val = 0)
        {
            InitializeComponent();
            x_Val = p_Val;
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
                    this.btnModificar.TabStop = true && (dgvLocalidades.RowCount != 0);
                    this.btnModificar.Visible = true && (dgvLocalidades.RowCount != 0);
                    this.btnBuscar.TabStop = true;
                    this.btnBuscar.Visible = true;
                    this.btnAceptar.TabStop = false;
                    this.btnAceptar.Visible = false;
                    this.btnCancelar.TabStop = false;
                    this.btnCancelar.Visible = false;
                    this.btnSalir.TabStop = true;
                    this.btnSalir.Visible = true;
                    // Si el nivel del usuario es el 5, habilito el botón borrar
                    if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja)
                    {
                        this.btnBorrar.TabStop = true && (dgvLocalidades.RowCount != 0);
                        this.btnBorrar.Visible = true && (dgvLocalidades.RowCount != 0);
                    }
                    else
                    {
                        this.btnBorrar.TabStop = false;
                        this.btnBorrar.Visible = false;
                    }
                    this.btnImprimir.TabStop = true && (dgvLocalidades.RowCount != 0);
                    this.btnImprimir.Visible = true && (dgvLocalidades.RowCount != 0);
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
                    this.txtLocalidad.TabStop = true;
                    this.txtLocalidad.Enabled = true;
                    this.txtCP.TabStop = true;
                    this.txtCP.Enabled = true;
                    this.txtBuscarLocalidad.TabStop = false;
                    this.txtBuscarLocalidad.Enabled = false;
                    this.txtBuscarCP.TabStop = false;
                    this.txtBuscarCP.Enabled = false;
                    this.cboProvincias.TabStop = true;
                    this.cboProvincias.Enabled = true;
                    this.dgvLocalidades.TabStop = false;
                    this.dgvLocalidades.Enabled = false;
                    this.dgvLocalidades.Height = 240;
                    this.gpbBusquedas.Visible = false;
                    return;
                case "B":
                    this.txtLocalidad.TabStop = false;
                    this.txtLocalidad.Enabled = false;
                    this.txtCP.TabStop = false;
                    this.txtCP.Enabled = false;
                    this.txtBuscarLocalidad.TabStop = true;
                    this.txtBuscarLocalidad.Enabled = true;
                    this.txtBuscarCP.TabStop = true;
                    this.txtBuscarCP.Enabled = true;
                    this.cboProvincias.TabStop = false;
                    this.cboProvincias.Enabled = false;
                    this.dgvLocalidades.TabStop = true && (dgvLocalidades.RowCount > 0);
                    this.dgvLocalidades.Enabled = true && (dgvLocalidades.RowCount > 0);
                    this.dgvLocalidades.Height = 180;
                    this.gpbBusquedas.Visible = true;
                    return;
                case "C":
                    this.txtLocalidad.TabStop = false;
                    this.txtLocalidad.Enabled = false;
                    this.txtCP.TabStop = false;
                    this.txtCP.Enabled = false;
                    this.txtBuscarLocalidad.TabStop = false;
                    this.txtBuscarLocalidad.Enabled = false;
                    this.txtBuscarCP.TabStop = false;
                    this.txtBuscarCP.Enabled = false;
                    this.cboProvincias.TabStop = false;
                    this.cboProvincias.Enabled = false;
                    this.dgvLocalidades.TabStop = true && (dgvLocalidades.RowCount > 0);
                    this.dgvLocalidades.Enabled = true && (dgvLocalidades.RowCount > 0);
                    this.dgvLocalidades.Height = 240;
                    this.gpbBusquedas.Visible = false;
                    return;
            }
        }
        #endregion

        #region Método para limpiar los controles del formulario
        //LIMPIA LA PROPIEDAD TEXT DE LOS CONTROLES DEL FORMULARIO. N.
        private void LimpiarControlesForm()
        {
            this.txtLocalidad.Text = "";
            this.txtCP.Text = "";
            this.cboProvincias.SelectedIndex = -1;
        }
        #endregion

        #region Método para limpiar los campos de Búsqueda
        // Limpia los controles de búsqueda del form
        private void LimpiarControlesBusqueda()
        {
            this.txtBuscarLocalidad.Text = "";
            this.txtBuscarCP.Text="";
        }

        #endregion

        #region Método para cargar las Provincias al cboProvincias

        private void CargarProvincias()
        {
            // Cargo el combo de las provincias
            clsDataBD.CargarCombo(cboProvincias, "Provincias", "Provincia", "IdProvincia");
            /*
            // Declaro la variable y armo la cadema para la consulta SQL
            string myCadena = "SELECT Provincias.IdProvincia, Provincias.Provincia FROM Provincias ORDER BY Provincia";
            DataTable mDtTable = new DataTable();
            mDtTable = clsDataBD.GetSql(myCadena);
            // Asigno los datos que me devuelve el método Consultar al combo del formulario. G.
            cboProvincias.DataSource = mDtTable;
            */
        }
        #endregion

        #region Método para cargar la grilla

        private void CargarLocalidades(string Buscar, string Campo)
        {
            // Variable para la cadena SQL
            string myCadena = "";
            if (Buscar == "")
            {
                // Cadena SQL 
                myCadena = "select * from Vista_Localidades_Provincias ";
            }
            else
            {
                // Cadena SQL 
                myCadena = "select * from Vista_Localidades_Provincias where " + Campo + " like '" + Buscar + "%' order by " + Campo ;
                
                //Cambiar .T.
                bSearch = true;
            }
           
            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            mDtTable = clsDataBD.GetSql(myCadena);
            // Evito que el dgv genere columnas automáticas
            dgvLocalidades.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvLocalidades.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvLocalidades.Rows.Count;
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
            this.dgvLocalidades.CurrentCell = dgvLocalidades[1, this.indexFila];

            // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
            EventArgs ea = new EventArgs();
            this.dgvLocalidades_SelectionChanged(this.dgvLocalidades, ea);
        }

        #endregion

        #endregion

        #region Eventos del Formulario

        #region Evento Load del formulario

        private void frmLocalidades_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            this.myEstado = "C";
            // Cargo el combo con las provincias
            CargarProvincias();
            // Llamo al método cargar Localidades para rellenar la grilla. G.
            CargarLocalidades("","");
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

        #region Eventos de la Grilla

        #region Evento CellContentClick de la grilla

        private void dgvLocalidades_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvLocalidades.RowCount == 0)
            {
                return;
            }
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvLocalidades.CurrentRow;
            // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
            //TRAER LOS DATOS. H.
            txtLocalidad.Text = row.Cells["Localidad"].Value.ToString();
            txtCP.Text = row.Cells["CP"].Value.ToString();
            cboProvincias.SelectedValue = Convert.ToInt32(row.Cells["IdProvincia"].Value);
        }

        #endregion

        #region Evento SelectionChanged de la grilla

        private void dgvLocalidades_SelectionChanged(object sender, EventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvLocalidades.RowCount == 0)
            {
                return;
            }
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvLocalidades.CurrentRow;
            // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
            //TRAER LOS DATOS. H.
            txtLocalidad.Text = row.Cells["Localidad"].Value.ToString();
            txtCP.Text = row.Cells["CP"].Value.ToString();
            cboProvincias.SelectedValue = Convert.ToInt32(row.Cells["IdProvincia"].Value);
        }

        #endregion

        #region Evento KeyDown de la grilla

        private void dgvLocalidades_KeyDown(object sender, KeyEventArgs e)
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
            if (dgvLocalidades.Rows.Count >0)
            {
                // Capturo la posición de la fila desde donde se llamo
                this.indexFila = dgvLocalidades.CurrentRow.Index;            
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
                txtLocalidad.Focus();
            }
            // Pregunto si el usuario actual tiene nivel menor o igual a 2, No lo dejo agregar. G.
            else
            {
                // vuelvo el formulario al estado anterior. G.
                this.myEstado = "C";
                // El usuario no es de nivel 3, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para crear una nueva Localidad", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento Click del botón Modificar

        private void btnModificar_Click(object sender, EventArgs e)
        {
            // Capturo la posición de la fila desde donde se llamo
            this.indexFila = dgvLocalidades.CurrentRow.Index;

            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvLocalidades.CurrentRow;
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvLocalidades.RowCount == 0)
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
                txtLocalidad.Text = row.Cells["Localidad"].Value.ToString();
                txtCP.Text = row.Cells["CP"].Value.ToString();
                cboProvincias.Text = row.Cells["Provincia"].Value.ToString();
                // Posiciono el foco sobre el primer textbox
                txtLocalidad.Focus();
            }
            else
            {
                // Pregunto si el usuario actual tiene nivel menor o igual a 2, No lo dejo agregar. G.
                this.myEstado = "C";
                // El usuario no es de nivel 3, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para modificar una Localidad", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento Click del botón Borrar

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            // Capturo la posición de la fila desde donde se llamo
            this.indexFila = dgvLocalidades.CurrentRow.Index;

            // Pregunto si el usuario actual es el mismo que se quiere modificar. G.
            if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja)
            {
                //TRAER LA FILA ACTUAL. N.
                DataGridViewRow row = dgvLocalidades.CurrentRow;
                // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
                int Id = Convert.ToInt32(row.Cells["IdLocalidad"].Value);
                string Local = row.Cells["Localidad"].Value.ToString();
                int Niv = clsGlobales.UsuarioLogueado.Nivel;

               if (Niv < clsGlobales.cParametro.NivelBaja)
                {
                    // El usuario no es de nivel 5, por lo que no puede crear un nuevo usuario. G.
                    MessageBox.Show("Usted no tiene los permisos para Eliminar esta Localidad", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Ejecuto el botón cancelar. G.
                    this.btnCancelar.PerformClick();
                }
                else
                {
                    // Confirma eliminar el registro ?
                    DialogResult dlResult = MessageBox.Show("Desea Eliminar la Localidad " + Local + " ?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    // Si confirma salir de la aplicación....
                    if (dlResult == DialogResult.Yes)
                    {
                        string myCadena = "UPDATE Localidades SET Activo = 0 WHERE Localidades.IdLocalidad =" + Id;
                        clsDataBD.GetSql(myCadena);
                        // Refresco la grilla
                        CargarLocalidades("", "");
                    }
                }

            }
            else
            {
                // El usuario no es de nivel 5, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para Eliminar una Localidad", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento Click del botón Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            // Capturo la posición de la fila desde donde se llamo
            this.indexFila = dgvLocalidades.CurrentRow.Index;
            // Cambio mi estado a Búsqueda
            this.myEstado = "B";
            // Habilito los botones según mi estado
            ActivarBotones();
            // Habilito los campos de búsqueda
            HabilitarControles();
            // Limpio los campos de búsqueda
            LimpiarControlesBusqueda();
            // Pongo el foco en el primer control de búsqueda
            txtBuscarLocalidad.Focus();
        }

        #endregion

        #region Evento Click del botón Aceptar

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Si la grilla no contiene ninguna fila, salgo del evento
            if (dgvLocalidades.RowCount == 0)
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
            clsLocalidades NuevaLocalidad = new clsLocalidades();
            // Tomo la línea actual de la grilla. G.
            DataGridViewRow row = dgvLocalidades.CurrentRow;
            // Verifico el estado del formulario para saber si estoy creando o modificando una Localidad. G.
            // Paso los datos del formulario a la nueva Localidad
            NuevaLocalidad.IdLocalidad = Convert.ToInt32(row.Cells["IdLocalidad"].Value);
            NuevaLocalidad.CP = txtCP.Text; //N
            NuevaLocalidad.Localidad = txtLocalidad.Text;


            NuevaLocalidad.IdProvincia = Convert.ToInt32(cboProvincias.SelectedValue);

            //Vector Errores
            string[] cErrores = NuevaLocalidad.cValidaLocalidad();
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
                    myCadena = "INSERT INTO Localidades (Localidad,CP,IdProvincia, Activo) values ('" + NuevaLocalidad.Localidad +
                                      "','" + NuevaLocalidad.CP + "'," + NuevaLocalidad.IdProvincia + ",1)";
                    // Ejecuto la consulta SQL
                    clsDataBD.GetSql(myCadena);
                    // Lleno nuevamente la grilla
                    CargarLocalidades("", "");
                    // Regreso el formulario a su estado inicial
                    this.btnCancelar.PerformClick();
                    return;

                case "B":
                    //Tomar el Id
                    IdLoc = Convert.ToInt32(dgvLocalidades.CurrentRow.Cells["IdLocalidad"].Value.ToString());
                    // Cambio mi estado
                    this.myEstado = "C";
                    // Lleno nuevamente la grilla
                    this.CargarLocalidades("","");
                    // Activo todos los botones
                    ActivarBotones();
                    // Habilito los controles
                    HabilitarControles();
                    //Id >0? Solo cuando busca reposiciona por ID
                    if (!(IdLoc == 0 && bSearch))
                    {
                        ReposicionarById();
                        IdLoc = 0;
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
                    // Creo la cadena para grabar las Modificaciones de la Localidad
                    myCadena = "UPDATE Localidades SET Localidad = '" + NuevaLocalidad.Localidad + "', IdProvincia = " +
                               NuevaLocalidad.IdProvincia + ", CP = '" + NuevaLocalidad.CP + "' WHERE IdLocalidad = " + NuevaLocalidad.IdLocalidad;
                    // Ejecuto la consulta SQL
                    clsDataBD.GetSql(myCadena);
                    // Lleno nuevamente la grilla
                    CargarLocalidades("", "");
                    // Regreso el formulario a su estado inicial
                    this.btnCancelar.PerformClick();
                    return;

            }


        }

        #endregion

        #region Evento Click del botón Cancelar

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Recargar
            if (this.myEstado == "B" && bSearch)
            {
                //Cargo las localidades
                CargarLocalidades("", "");
                // Cambio el estado del formulario a agregar. G.
                this.myEstado = "C";
                // Limpio los controles del formulario. G.    
                ActivarBotones();
                // Habilito los controles para este estado. G.
                HabilitarControles();
                // Posiciono el foco en la fila desde donde se llamo
                PosicionarFocoFila();
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

        #region Reposicionar Grilal por Id

        //REPOSICIONA GRILLA POR ID
        private void ReposicionarById()        
        {
            //recorrer
            foreach (DataGridViewRow myRow in this.dgvLocalidades.Rows)            
            {
                if (Convert.ToInt32(myRow.Cells["IdLocalidad"].Value.ToString()) == IdLoc)
                {
                    // Devuelvo el foco a la fila de la grilla desde donde se llamó
                    this.dgvLocalidades.CurrentCell = dgvLocalidades[1, myRow.Index];
                   
                    // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                    EventArgs ea = new EventArgs();
                    this.dgvLocalidades_SelectionChanged(this.dgvLocalidades, ea);

                    //Salir
                    break;
                }
            }
        }

        #endregion

        #endregion


        #region Evento Click del botón Salir

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //Si viene con valor 1 en el constructor, preguntar sino cerrar.
            if (x_Val == 1)
            {
                DialogResult dlResult = MessageBox.Show("Esta seguro de querer utilizar la Localidad: " + dgvLocalidades.CurrentRow.Cells["Localidad"].Value.ToString() + ", Provincia: " + dgvLocalidades.CurrentRow.Cells["Provincia"].Value.ToString() + ", CP: " + dgvLocalidades.CurrentRow.Cells["CP"].Value.ToString() + " ?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Si confirma salir de la aplicación....
                if (dlResult == DialogResult.Yes)
                {

                    //Para manipular datos grilla
                    DataGridViewRow row = dgvLocalidades.CurrentRow;

                    //Tomo datos de la grilla
                    clsGlobales.IdProv = Convert.ToInt32(row.Cells["IdProvincia"].Value.ToString());
                    clsGlobales.IdLoc = Convert.ToInt32(row.Cells["IdLocalidad"].Value.ToString());
                    clsGlobales.CP = row.Cells["CP"].Value.ToString();
                }
            }

            // Cierro el formulario. G.
            this.Close();
        }

        #endregion

        #endregion

        #region Eventos de los controles

        #region Eventos Leave

        // txtLocalidad
        private void txtLocalidad_Leave(object sender, EventArgs e)
        {
            this.txtLocalidad.Text = txtLocalidad.Text.ToUpper();
        }

        #endregion

        #region Eventos KeyPress

        // txtLocalidad
        private void txtLocalidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                this.txtCP.Focus();
            }
        }

        // txtCP
        private void txtCP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                this.cboProvincias.Focus();
            }
        }

        #endregion

        #region Eventos TextChanged de los textbox

        private void txtBuscarLocalidad_TextChanged(object sender, EventArgs e)
        {
            if (!(this.txtBuscarLocalidad.Text == ""))
            {
                // Cargo las localidades filtradas por la búsqueda
                CargarLocalidades(txtBuscarLocalidad.Text.ToUpper(), "Localidad");
            }
        }

        private void txtBuscarCP_TextChanged(object sender, EventArgs e)
        {
            if (!(this.txtBuscarCP.Text == ""))
            {
                // Cargo las localidades filtradas por la búsqueda
                CargarLocalidades(txtBuscarCP.Text, "CP");
            }
        }

        #endregion

        #region Eventos Enter de los TextBox del formulario

        private void txtBuscarLocalidad_Enter(object sender, EventArgs e)
        {
            // Si el otro campo de búsqueda tiene algo, lo borro
            if (!(txtBuscarCP.Text == ""))
            {
                // Limpio el ptro campo de búsqueda
                txtBuscarCP.Text = "";
            }
        }

        private void txtBuscarCP_Enter(object sender, EventArgs e)
        {
            // Si el otro campo de búsqueda tiene algo, lo borro
            if (!(txtBuscarLocalidad.Text == ""))
            {
                // Limpio el otro campo de búsqueda
                txtBuscarLocalidad.Text = "";
            }
        }

        #endregion

        #region Evento Click BtnImprimir

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //Data Set
            dsReportes oDsLpc = new dsReportes();

            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = dgvLocalidades.Rows.Count;

            for (int i = 0; i < dgvFilas; i++)
            {
                oDsLpc.Tables["DtLoc"].Rows.Add
                (new object[] { dgvLocalidades[1,i].Value.ToString(),
                dgvLocalidades[2,i].Value.ToString(),
                dgvLocalidades[4,i].Value.ToString() });

            }

            //Objeto Reporte
            rptLoc oRepLoc = new rptLoc();
            //Cargar Reporte            
            //    oRepTipoCli.Load(Application.StartupPath + "\\rptTipoCli.rpt");
            oRepLoc.Load(Application.StartupPath + "\\rptLoc.rpt");
            //Establecer el DataSet como DataSource
            oRepLoc.SetDataSource(oDsLpc);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepLoc;
            oRepLoc.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepLoc.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepLoc.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
            oRepLoc.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
            oRepLoc.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
            oRepLoc.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
            oRepLoc.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepLoc.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
            oRepLoc.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";

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

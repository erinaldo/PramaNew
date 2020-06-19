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
    public partial class frmCajaImputaciones : Form
    {
        #region Declaracion Variables Nivel Formulario

        //VARIABLES
        string myEstado = "C";
        int indexFila = 0;
        bool bSearch = false;
        int IdLoc = 0;

        #endregion

        #region Constructor del formulario

        public frmCajaImputaciones()
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
                    this.btnModificar.TabStop = true && (dgvImputaciones.RowCount != 0);
                    this.btnModificar.Visible = true && (dgvImputaciones.RowCount != 0);
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
                        this.btnBorrar.TabStop = true && (dgvImputaciones.RowCount != 0);
                        this.btnBorrar.Visible = true && (dgvImputaciones.RowCount != 0);
                    }
                    else
                    {
                        this.btnBorrar.TabStop = false;
                        this.btnBorrar.Visible = false;
                    }
                    this.btnImprimir.TabStop = false;
                    this.btnImprimir.Visible = false;
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
                    this.txtImputacion.TabStop = true;
                    this.txtImputacion.Enabled = true;
                    this.txtCodBalance.TabStop = true;
                    this.txtCodBalance.Enabled = true;
                    this.txtBuscarImputacion.TabStop = false;
                    this.txtBuscarImputacion.Enabled = false;
                    this.txtBuscarCodInterno.TabStop = false;
                    this.txtBuscarCodInterno.Enabled = false;
                    this.txtCodInterno.TabStop = true;
                    this.txtCodInterno.Enabled = true;
                    
                    this.dgvImputaciones.TabStop = false;
                    this.dgvImputaciones.Enabled = false;
                    this.dgvImputaciones.Height = 240;
                    this.gpbBusquedas.Visible = false;
                    return;
                case "B":
                    this.txtImputacion.TabStop = false;
                    this.txtImputacion.Enabled = false;
                    this.txtCodBalance.TabStop = false;
                    this.txtCodBalance.Enabled = false;
                    this.txtBuscarImputacion.TabStop = true;
                    this.txtBuscarImputacion.Enabled = true;
                    this.txtBuscarCodInterno.TabStop = true;
                    this.txtBuscarCodInterno.Enabled = true;
                    this.txtCodInterno.TabStop = false;
                    this.txtCodInterno.Enabled = false;
                    
                    this.dgvImputaciones.TabStop = true && (dgvImputaciones.RowCount > 0);
                    this.dgvImputaciones.Enabled = true && (dgvImputaciones.RowCount > 0);
                    this.dgvImputaciones.Height = 180;
                    this.gpbBusquedas.Visible = true;
                    return;
                case "C":
                    this.txtImputacion.TabStop = false;
                    this.txtImputacion.Enabled = false;
                    this.txtCodBalance.TabStop = false;
                    this.txtCodBalance.Enabled = false;
                    this.txtBuscarImputacion.TabStop = false;
                    this.txtBuscarImputacion.Enabled = false;
                    this.txtBuscarCodInterno.TabStop = false;
                    this.txtBuscarCodInterno.Enabled = false;
                    this.txtCodInterno.TabStop = false;
                    this.txtCodInterno.Enabled = false;
                    
                    this.dgvImputaciones.TabStop = true && (dgvImputaciones.RowCount > 0);
                    this.dgvImputaciones.Enabled = true && (dgvImputaciones.RowCount > 0);
                    this.dgvImputaciones.Height = 240;
                    this.gpbBusquedas.Visible = false;
                    return;
            }
        }
        #endregion

        #region Método para limpiar los controles del formulario
        //LIMPIA LA PROPIEDAD TEXT DE LOS CONTROLES DEL FORMULARIO. N.
        private void LimpiarControlesForm()
        {
            this.txtImputacion.Text = "";
            this.txtCodBalance.Text = "";
            this.txtCodInterno.Text = "";
            
        }
        #endregion

        #region Método para limpiar los campos de Búsqueda
        // Limpia los controles de búsqueda del form
        private void LimpiarControlesBusqueda()
        {
            this.txtBuscarImputacion.Text = "";
            this.txtBuscarCodInterno.Text = "";
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
                myCadena = "select * from Imputaciones where Activo = 1";
            }
            else
            {
                // Cadena SQL 
                myCadena = "select * from Imputaciones where Activo = 1 and " + Campo + " like '" + Buscar + "%' order by " + Campo;

                //Cambiar .T.
                bSearch = true;
            }

            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            mDtTable = clsDataBD.GetSql(myCadena);
            // Evito que el dgv genere columnas automáticas
            dgvImputaciones.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvImputaciones.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvImputaciones.Rows.Count;
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
            this.dgvImputaciones.CurrentCell = dgvImputaciones[1, this.indexFila];

            // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
            EventArgs ea = new EventArgs();
            this.dgvImputaciones_SelectionChanged(this.dgvImputaciones, ea);
        }

        #endregion
       
        #endregion

        #region Evento Load del formulario

        private void frmCajaImputaciones_Load(object sender, EventArgs e)
        {
			
			//icon
            clsFormato.SetIconForm(this); 
			//Variable Accion
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

        #region Eventos de la grilla

        #region Evento CellContentClick dela grilla

        private void dgvImputaciones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvImputaciones.RowCount == 0)
            {
                return;
            }
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvImputaciones.CurrentRow;
            // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
            //TRAER LOS DATOS. H.
            txtImputacion.Text = row.Cells["Imputacion"].Value.ToString();
            txtCodBalance.Text = row.Cells["CodigoImputacion"].Value.ToString();
            txtCodInterno.Text = row.Cells["CodigoInterno"].Value.ToString();
            
        }

        #endregion

        #region Evento KeyDown de la grilla

        private void dgvImputaciones_KeyDown(object sender, KeyEventArgs e)
        {
            //PRESIONO DEL?
            if (e.KeyCode == Keys.Delete)
            {
                this.btnBorrar.PerformClick(); //LLAMAR A EVENTO CLICK DEL BOTON BORRAR
            }
        }

        #endregion

        #region Evento SelectionChanged de la grilla

        private void dgvImputaciones_SelectionChanged(object sender, EventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvImputaciones.RowCount == 0)
            {
                return;
            }
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvImputaciones.CurrentRow;
            // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
            //TRAER LOS DATOS. H.
            txtImputacion.Text = row.Cells["Imputacion"].Value.ToString();
            txtCodBalance.Text = row.Cells["CodigoImputacion"].Value.ToString();
            txtCodInterno.Text = row.Cells["CodigoInterno"].Value.ToString();
            
        }

        #endregion

        #endregion

        #region Evento Click del botón Nuevo

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (dgvImputaciones.Rows.Count > 0)
            {
                // Capturo la posición de la fila desde donde se llamo
                this.indexFila = dgvImputaciones.CurrentRow.Index;
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
                txtImputacion.Focus();
            }
            // Pregunto si el usuario actual tiene nivel menor o igual a 2, No lo dejo agregar. G.
            else
            {
                // vuelvo el formulario al estado anterior. G.
                this.myEstado = "C";
                // El usuario no es de nivel 3, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para crear una nueva Imputación", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento Click del botón Editar

        private void btnModificar_Click(object sender, EventArgs e)
        {
            // Capturo la posición de la fila desde donde se llamo
            this.indexFila = dgvImputaciones.CurrentRow.Index;

            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvImputaciones.CurrentRow;
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvImputaciones.RowCount == 0)
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
                //TRAER LOS DATOS. H.
                txtImputacion.Text = row.Cells["Imputacion"].Value.ToString();
                txtCodBalance.Text = row.Cells["CodigoImputacion"].Value.ToString();
                txtCodInterno.Text = row.Cells["CodigoInterno"].Value.ToString();
                
                // Posiciono el foco sobre el primer textbox
                txtImputacion.Focus();
            }
            else
            {
                // Pregunto si el usuario actual tiene nivel menor o igual a 2, No lo dejo agregar. G.
                this.myEstado = "C";
                // El usuario no es de nivel 3, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para modificar una Imputación", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento Click del botón Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Capturo la posición de la fila desde donde se llamo
            this.indexFila = dgvImputaciones.CurrentRow.Index;
            // Cambio mi estado a Búsqueda
            this.myEstado = "B";
            // Habilito los botones según mi estado
            ActivarBotones();
            // Habilito los campos de búsqueda
            HabilitarControles();
            // Limpio los campos de búsqueda
            LimpiarControlesBusqueda();
            // Pongo el foco en el primer control de búsqueda
            txtBuscarImputacion.Focus();
        }

        #endregion

        #region Evento Click del botón Borrar

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            // Capturo la posición de la fila desde donde se llamo
            this.indexFila = dgvImputaciones.CurrentRow.Index;

            // Pregunto si el usuario actual es el mismo que se quiere modificar. G.
            if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja)
            {
                //TRAER LA FILA ACTUAL. N.
                DataGridViewRow row = dgvImputaciones.CurrentRow;
                // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
                int Id = Convert.ToInt32(row.Cells["IdImputacion"].Value);
                string Local = row.Cells["Imputacion"].Value.ToString();
                int Niv = clsGlobales.UsuarioLogueado.Nivel;

                if (Niv < clsGlobales.cParametro.NivelBaja)
                {
                    // El usuario no es de nivel 5, por lo que no puede crear un nuevo usuario. G.
                    MessageBox.Show("Usted no tiene los permisos para Eliminar esta Imputación", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Ejecuto el botón cancelar. G.
                    this.btnCancelar.PerformClick();
                }
                else
                {
                    // Confirma eliminar el registro ?
                    DialogResult dlResult = MessageBox.Show("Desea Eliminar la Imputación " + Local + " ?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    // Si confirma salir de la aplicación....
                    if (dlResult == DialogResult.Yes)
                    {
                        string myCadena = "UPDATE Imputaciones SET Activo = 0 WHERE IdImputacion =" + Id;
                        clsDataBD.GetSql(myCadena);
                        // Refresco la grilla
                        CargarGrilla("", "");
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

        #region Evento Click del botón Imprimir

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Evento Click del botón Aceptar

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Si la grilla no contiene ninguna fila, salgo del evento
            if (dgvImputaciones.RowCount == 0)
            {
                // Muestro un mensaje indicando que no se encontraron los datos
                MessageBox.Show("No se encontraron coincidencias", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Regreso el formulario a su estado inicial
                this.btnBuscar.PerformClick();
                return;
            }
            // Creo la cadena para grabar las Modificaciones de la Localidad
            string myCadena = "";
            
            // Tomo la línea actual de la grilla. G.
            DataGridViewRow row = dgvImputaciones.CurrentRow;
            // Verifico el estado del formulario para saber si estoy creando o modificando una Localidad. G.
            // Paso los datos del formulario a la nueva Localidad
            int iId = Convert.ToInt32(row.Cells["IdLocalidad"].Value);
            string sImputacion = txtImputacion.Text;
            string sCodBalance = txtCodBalance.Text; //N
            string sCodInterno = txtCodInterno.Text;
            

            switch (this.myEstado)
            {
                case "A":

                    // Creo la cadena para grabar el alta de la Localidad
                    myCadena = "INSERT INTO Imputaciones (Imputacion,CodigoImputacion,CodigoInterno, Activo) values ('"
                                + sImputacion +
                                "','" + sCodBalance +
                                "'," + sCodInterno + ",1)";
                    // Ejecuto la consulta SQL
                    clsDataBD.GetSql(myCadena);
                    // Lleno nuevamente la grilla
                    CargarGrilla("", "");
                    // Regreso el formulario a su estado inicial
                    this.btnCancelar.PerformClick();
                    return;

                case "B":
                    //Tomar el Id
                    IdLoc = Convert.ToInt32(dgvImputaciones.CurrentRow.Cells["IdImputacion"].Value.ToString());
                    // Cambio mi estado
                    this.myEstado = "C";
                    // Lleno nuevamente la grilla
                    this.CargarGrilla("", "");
                    // Activo todos los botones
                    ActivarBotones();
                    // Habilito los controles
                    HabilitarControles();
                    //Id >0? Solo cuando busca reposiciona por ID
                    if (!(IdLoc == 0 && bSearch))
                    {
                        PosicionarFocoFila();
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
                    myCadena = "UPDATE Imputaciones SET Imputacion = '" + sImputacion + "', CodigoImputacion = '" +
                               sCodBalance + "', CodigoInterno = '" + sCodInterno + "' WHERE IdImputacion = " + iId;
                    // Ejecuto la consulta SQL
                    clsDataBD.GetSql(myCadena);
                    // Lleno nuevamente la grilla
                    CargarGrilla("", "");
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
                CargarGrilla("", "");
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

        #endregion

        #region Evento Click del botón Salir

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Cierro el formulario. G.
            this.Close();
        }

        #endregion

    }
}

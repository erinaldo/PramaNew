using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Prama.Clases;

namespace Prama.Formularios.Auxiliares
{
    public partial class frmCostosEnvios : Form
    {
        
        //Variables nivel formulario
        string myEstado = "C";
        int indexFila = 0;

        public frmCostosEnvios()
        {
            InitializeComponent();
        }

        private void frmCostosEnvios_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            // Cargo la grilla
            CargarCostos();
            // Llamo al método activar los botones del formulario. G.
            ActivarBotones();
            // Llamo al método habilitar controles del formulario. G.
            HabilitarControles();
            // Cargar ToolTips
            CargarToolTips();

            //Titulo
            this.Text = clsGlobales.cParametro.NombreFantasia + this.Text;
        }

        #region Método para cargar la grilla

        private void CargarCostos()
        {
            // Variable para la cadena SQL
            string myCadena = "";
            // Cadena SQL 
            myCadena = "select * from CostosEnvios";

            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            mDtTable = clsDataBD.GetSql(myCadena);
            // Evito que el dgv genere columnas automáticas
            dgvCostos.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvCostos.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvCostos.Rows.Count;
            // Posiciono la grilla en la última fila, si hay
            if (Filas > 0)
            {
                //Posicionamiento grilla
                int r = dgvCostos.CurrentCell.RowIndex;
                int c = dgvCostos.CurrentCell.ColumnIndex;
                dgvCostos.CurrentCell = dgvCostos.Rows[r].Cells[c];
                //Mostrar datos  
                this.txtDescripcion.Text = dgvCostos.CurrentRow.Cells["Descripcion"].Value.ToString();
            }

        }

        #endregion

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
                    return;

                case "C":
                    this.btnAgregar.TabStop = true;
                    this.btnAgregar.Visible = true;
                    this.btnModificar.TabStop = true && (dgvCostos.RowCount != 0);
                    this.btnModificar.Visible = true && (dgvCostos.RowCount != 0);
                    this.btnAceptar.TabStop = false;
                    this.btnAceptar.Visible = false;
                    this.btnCancelar.TabStop = false;
                    this.btnCancelar.Visible = false;
                    this.btnSalir.TabStop = true;
                    this.btnSalir.Visible = true;
                    // Si el nivel del usuario es el 5, habilito el botón borrar
                    if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja)
                    {
                        this.btnBorrar.TabStop = true && (dgvCostos.RowCount != 0);
                        this.btnBorrar.Visible = true && (dgvCostos.RowCount != 0);
                    }
                    else
                    {
                        this.btnBorrar.TabStop = false;
                        this.btnBorrar.Visible = false;
                    }
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
                    this.txtDescripcion.TabStop = true;
                    this.txtDescripcion.Enabled = true;
                    this.txtSuc.TabStop = true;
                    this.txtSuc.Enabled = true;
                    this.txtDom.TabStop = true;
                    this.txtDom.Enabled = true;
                    this.dgvCostos.TabStop = false;
                    this.dgvCostos.Enabled = false;
                    return;

                case "C":
                    this.txtDescripcion.TabStop = false;
                    this.txtDescripcion.Enabled = false;
                    this.txtSuc.TabStop = false;
                    this.txtSuc.Enabled = false;
                    this.txtDom.TabStop = false;
                    this.txtDom.Enabled = false;
                    this.dgvCostos.TabStop = true && (dgvCostos.RowCount > 0);
                    this.dgvCostos.Enabled = true && (dgvCostos.RowCount > 0);
                    return;
            }
        }
        #endregion

        #region CargarToolTips

        private void CargarToolTips()
        {
            toolTip1.SetToolTip(this.btnAgregar, "Agregar");
            toolTip2.SetToolTip(this.btnModificar, "Modificar");
            toolTip3.SetToolTip(this.btnBorrar, "Borrar");
            toolTip4.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip5.SetToolTip(this.btnCancelar, "Cancelar");
            toolTip6.SetToolTip(this.btnSalir, "Salir");
        }

        #endregion

        #region Método para limpiar los controles del formulario
        //LIMPIA LA PROPIEDAD TEXT DE LOS CONTROLES DEL FORMULARIO. N.
        private void LimpiarControlesForm()
        {
            this.txtDescripcion.Text = "";
            this.txtSuc.Text = "";
            this.txtDom.Text = "";
        }
        #endregion

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Pregunto si el usuario actual tiene nivel mayor a 2, lo dejo agregar. G.
            if (clsGlobales.UsuarioLogueado.Nivel > 2)
            {
                if (dgvCostos.Rows.Count >0)
                {
                    // Tomo la posición actual de la fila con foco
                    this.indexFila = dgvCostos.CurrentRow.Index;
                }
                // Cambio el estado del formulario a agregar. G.
                this.myEstado = "A";
                // Limpio los controles del formulario. G.    
                LimpiarControlesForm();
                // Activo los botones para este estado. G.
                ActivarBotones();
                // Habilito los controles para este estado. G.
                HabilitarControles();
                // Posiciono el foco sobre el primer textbox
                txtDescripcion.Focus();
            }
            // Pregunto si el usuario actual tiene nivel menor o igual a 2, No lo dejo agregar. G.
            else
            {
                // vuelvo el formulario al estado anterior. G.
                this.myEstado = "C";
                // El usuario no es de nivel 3, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para crear un nuevo Costo de Envío!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvCostos.CurrentRow;
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvCostos.RowCount == 0)
            {
                // Salgo de la rutina. G.
                return;
            }
            // // Pregunto si el usuario actual tiene nivel mayor a 2, lo dejo agregar. G.
            if (clsGlobales.UsuarioLogueado.Nivel > 2)
            {
                // Tomo la posición actual de la fila con foco
                this.indexFila = dgvCostos.CurrentRow.Index;

                // Cambio el estado del formulario a Modificar. G.
                this.myEstado = "M";
                // Activo los botones para este estado. G.
                ActivarBotones();
                // Habilito los controles para este estado. G.
                HabilitarControles();
                // Cargo los datos de la contraseña en sus correspondientes textbox. G.
                txtDescripcion.Text = row.Cells["Descripcion"].Value.ToString();
                txtSuc.Text = row.Cells["Sucursal"].Value.ToString();
                txtDom.Text = row.Cells["Domicilio"].Value.ToString();
                // Pongo el foco en el primer texbox
                txtDescripcion.Focus();
            }
            else
            {
                // Pregunto si el usuario actual tiene nivel menor o igual a 2, No lo dejo agregar. G.
                this.myEstado = "C";
                // El usuario no es de nivel 3, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para modificar un Costo de Envío!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Si la grilla no contiene ninguna fila, salgo del evento
            if (dgvCostos.RowCount == 0)
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
            clsCostosEnvios NuevoCostoEnvio = new clsCostosEnvios();
            // Tomo la línea actual de la grilla. G.
            DataGridViewRow row = dgvCostos.CurrentRow;
            // Verifico el estado del formulario para saber si estoy creando o modificando una Localidad. G.
            // Paso los datos del formulario a la nueva Localidad
            NuevoCostoEnvio.IdCostoEnvio = Convert.ToInt32(row.Cells["IdCostoEnvio"].Value);
            NuevoCostoEnvio.Descripcion = txtDescripcion.Text;

            if (string.IsNullOrEmpty(txtSuc.Text))
            { NuevoCostoEnvio.Sucursal = 0; }
            else { NuevoCostoEnvio.Sucursal = Convert.ToDouble(txtSuc.Text); }

            if (string.IsNullOrEmpty(txtDom.Text))
            { NuevoCostoEnvio.Domicilio = 0; }
            else { NuevoCostoEnvio.Domicilio = Convert.ToDouble(txtDom.Text); }

            //Vector Errores
            string[] cErrores = NuevoCostoEnvio.cValidaCoeficiente();
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

                    //Alta, activar
                    NuevoCostoEnvio.Activo = 1;

                    // Creo la cadena para grabar el alta de la Localidad
                    myCadena = "INSERT INTO CostosEnvios (Descripcion," +
                                                                  "Sucursal," +
                                                                  "Domicilio," +
                                                                  "Activo) values ('"
                                                                                    + NuevoCostoEnvio.Descripcion + "', "
                                                                                    + NuevoCostoEnvio.Sucursal.ToString().Replace(",", ".") + ", "
                                                                                    + NuevoCostoEnvio.Domicilio.ToString().Replace(",", ".") + ", "
                                                                                    + NuevoCostoEnvio.Activo + ")";

                    // Ejecuto la consulta SQL
                    clsDataBD.GetSql(myCadena);
                    // Lleno nuevamente la grilla
                    CargarCostos();
                    // Regreso el formulario a su estado inicial
                    this.btnCancelar.PerformClick();
                    return;

                case "M":
                    // Creo la cadena para grabar las Modificaciones de la Localidad
                    myCadena = "UPDATE CostosEnvios SET Descripcion = '" + NuevoCostoEnvio.Descripcion + "'," +
                                                                 "Sucursal = " + NuevoCostoEnvio.Sucursal.ToString().Replace(",", ".") + ", " +
                                                                 "Domicilio = " + NuevoCostoEnvio.Domicilio.ToString().Replace(",", ".") +
                                                                 " WHERE IdCostoEnvio = " + NuevoCostoEnvio.IdCostoEnvio;
                    // Ejecuto la consulta SQL
                    clsDataBD.GetSql(myCadena);
                    // Lleno nuevamente la grilla
                    CargarCostos();
                    // Regreso el formulario a su estado inicial
                    this.btnCancelar.PerformClick();
                    // Actualizo los campos en base a mi posición de la grilla
                    this.dgvCostos_CellContentClick(dgvCostos, new DataGridViewCellEventArgs(0, 0));
                    //Reposicionar grilla 
                    PosicionarFocoFila();
                    // Salgo del case
                    return;
            }


        }

        private void dgvCostos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvCostos.RowCount == 0)
            {
                return;
            }
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvCostos.CurrentRow;
            // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
            //TRAER LOS DATOS. H.
            txtDescripcion.Text = row.Cells["Descripcion"].Value.ToString();
            txtSuc.Text = row.Cells["Sucursal"].Value.ToString();
            txtDom.Text = row.Cells["Domicilio"].Value.ToString();
        }

        private void dgvCostos_KeyDown(object sender, KeyEventArgs e)
        {
            //PRESIONO DEL?
            if (e.KeyCode == Keys.Delete)
            {
                this.btnBorrar.PerformClick(); //LLAMAR A EVENTO CLICK DEL BOTON BORRAR
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Cambio el estado del formulario a agregar. G.
            this.myEstado = "C";
            // Cargo las localidades
            this.CargarCostos();
            // Limpio los controles del formulario. G.    
            ActivarBotones();
            // Habilito los controles para este estado. G.
            HabilitarControles();
            // Actualizo los campos en base a mi posición de la grilla
            this.dgvCostos_CellContentClick(dgvCostos, new DataGridViewCellEventArgs(0, 0));
            //Reposicionar grilla 
            PosicionarFocoFila();
        }

        private void dgvCostos_SelectionChanged(object sender, EventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvCostos.RowCount == 0)
            {
                return;
            }
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvCostos.CurrentRow;
            // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
            //TRAER LOS DATOS. H.
            txtDescripcion.Text = row.Cells["Descripcion"].Value.ToString();
            txtSuc.Text = row.Cells["Sucursal"].Value.ToString();
            txtDom.Text = row.Cells["Domicilio"].Value.ToString();
        }

        #region Método que devuelve el foco a la fila desde donde se presionó algún botón

        private void PosicionarFocoFila()
        {
            if (dgvCostos.Rows.Count != 0 && dgvCostos.Rows.Count > this.indexFila)
            {
                // Devuelvo el foco a la fila de la grilla desde donde se llamó
                this.dgvCostos.CurrentCell = dgvCostos[2, this.indexFila];

                // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                EventArgs ea = new EventArgs();
                this.dgvCostos_SelectionChanged(this.dgvCostos, ea);
            }
        }

        #endregion

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            // Capturo la posición de la fila desde donde se llamo
            this.indexFila = dgvCostos.CurrentRow.Index;

            // Pregunto si el usuario actual es el mismo que se quiere modificar. G.
            if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja)
            {
                //TRAER LA FILA ACTUAL. N.
                DataGridViewRow row = dgvCostos.CurrentRow;
                // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
                int Id = Convert.ToInt32(row.Cells["IdCostoEnvio"].Value);
                string Despcripcion = row.Cells["Descripcion"].Value.ToString();
                // Confirma eliminar el registro ?
                DialogResult dlResult = MessageBox.Show("Desea Eliminar el 'Costo de Envío': " + Descripcion + " ?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Si confirma salir de la aplicación....
                if (dlResult == DialogResult.Yes)
                {
                    string myCadena = "UPDATE CostosEnvios SET Activo = 0 WHERE IdCostoEnvio =" + Id;
                    clsDataBD.GetSql(myCadena);
                    // Refresco la grilla
                    this.CargarCostos();
                    //Foco
                    PosicionarFocoFila();
                }
            }
            else
            {
                // El usuario no es de nivel 5, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para Eliminar un 'Costo de Envío'", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        private void txtSuc_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtDom_KeyPress(object sender, KeyPressEventArgs e)
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

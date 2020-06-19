using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Prama.Clases.Prama;

namespace Prama.Formularios.Auxiliares
{
    public partial class frmTransporte : Form
    {

        #region Variables Nivel Formulario

        //Variables nivel formulario
        string myEstado = "C";
        int indexFila = 0;
        int Id = 0; //Para reposicionar
        bool bSearch = false;

        TextBox pmyTextBox;
        
        #endregion

        public frmTransporte(TextBox myTextBox = null)
        {
            InitializeComponent();

            //Asignar

            pmyTextBox = myTextBox;
        }

        private void frmTransporte_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            // Cargo la grilla
            CargarTransporte();
            // Llamo al método activar los botones del formulario. G.
            ActivarBotones();
            // Llamo al método habilitar controles del formulario. G.
            HabilitarControles();
            // Cargar ToolTips
            CargarToolTips();
            //Titulo Ventana
            this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;
        }

        #region Método para cargar la grilla

        private void CargarTransporte(string Buscar="",string Campo="")
        {
            // Variable para la cadena SQL
            string myCadena = "";
            if (Buscar == "")
            { myCadena = "select * from Transportes WHERE Activo = 1";}
            else
            {
                if (Campo == "RazonSocial")
                {
                    myCadena = "select * from Transportes WHERE " + Campo + " like '" + Buscar + "%' AND Activo = 1 order by " + Campo;
                }
                else
                {
                    myCadena = "select * from Transportes WHERE " + Campo + "=" + Buscar + " AND Activo = 1 order by " + Campo; 
                }

                //.T. Se hizo busqueda
                bSearch = true;
            }

            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            mDtTable = clsDataBD.GetSql(myCadena);
            // Evito que el dgv genere columnas automáticas
            dgvTransportes.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvTransportes.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvTransportes.Rows.Count;
            // Posiciono la grilla en la última fila, si hay
            if (Filas > 0)
            {
                //Posicionamiento grilla
                int r = dgvTransportes.CurrentCell.RowIndex;
                int c = dgvTransportes.CurrentCell.ColumnIndex;
                dgvTransportes.CurrentCell = dgvTransportes.Rows[r].Cells[c];
                //Mostrar datos  
                this.txtTransporte.Text = dgvTransportes.CurrentRow.Cells["RazonSocial"].Value.ToString();
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
                    this.btnImprimir.TabStop = false;
                    this.btnImprimir.Visible = false;
                    this.btnBuscar.TabStop = false;
                    this.btnBuscar.Visible = false;
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
                    this.btnModificar.TabStop = true && (dgvTransportes.RowCount != 0);
                    this.btnModificar.Visible = true && (dgvTransportes.RowCount != 0);
                    this.btnModificar.Enabled = true && (dgvTransportes.RowCount != 0);
                    this.btnAceptar.TabStop = false;
                    this.btnAceptar.Visible = false;
                    this.btnCancelar.TabStop = false;
                    this.btnCancelar.Visible = false;
                    this.btnSalir.TabStop = true;
                    this.btnSalir.Visible = true;
                    this.btnBuscar.TabStop = true && (dgvTransportes.RowCount != 0);
                    this.btnBuscar.Visible = true && (dgvTransportes.RowCount != 0);
                    // Si el nivel del usuario es el 5, habilito el botón borrar
                    if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja)
                    {
                        this.btnBorrar.TabStop = true && (dgvTransportes.RowCount != 0);
                        this.btnBorrar.Visible = true && (dgvTransportes.RowCount != 0);
                        this.btnBorrar.Enabled = true && (dgvTransportes.RowCount != 0);
                    }
                    else
                    {
                        this.btnBorrar.TabStop = false;
                        this.btnBorrar.Visible = false;
                        this.btnBorrar.Enabled = false;
                    }
                    this.btnImprimir.TabStop = true && (dgvTransportes.RowCount != 0);
                    this.btnImprimir.Visible = true && (dgvTransportes.RowCount != 0);
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
                    this.txtTransporte.TabStop = true;
                    this.txtTransporte.Enabled = true;
                    this.dgvTransportes.TabStop = false;
                    this.dgvTransportes.Enabled = false;

                    this.gpbBusquedas.Visible = false;

                    break;

                case "B":
                    this.txtTransporte.TabStop = false;
                    this.txtTransporte.Enabled = false;
                    this.txtBuscaRs.TabStop = true;
                    this.txtBuscaRs.Enabled = true;
                    this.txtBuscaCod.TabStop = true;
                    this.txtBuscaCod.Enabled = true;
                    this.dgvTransportes.TabStop = true && (dgvTransportes.RowCount > 0);
                    this.dgvTransportes.Enabled = true && (dgvTransportes.RowCount > 0);
                    this.gpbBusquedas.Visible = true;

                    break;

                case "C":
                    this.txtTransporte.TabStop = false;
                    this.txtTransporte.Enabled = false;
                    this.dgvTransportes.TabStop = true && (dgvTransportes.RowCount > 0);
                    this.dgvTransportes.Enabled = true && (dgvTransportes.RowCount > 0);

                    this.gpbBusquedas.Visible = false;


                    break;
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

        #region Método para limpiar los controles del formulario
        //LIMPIA LA PROPIEDAD TEXT DE LOS CONTROLES DEL FORMULARIO. N.
        private void LimpiarControlesForm()
        {
            this.txtTransporte.Text = "";
        }
        #endregion

        private void dgvTransportes_SelectionChanged(object sender, EventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvTransportes.RowCount == 0)
            {
                return;
            }
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvTransportes.CurrentRow;
            // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
            //TRAER LOS DATOS. H.
            txtTransporte.Text = row.Cells["RazonSocial"].Value.ToString();
        }

        private void dgvTransportes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvTransportes.RowCount == 0)
            {
                return;
            }
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvTransportes.CurrentRow;
            // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
            //TRAER LOS DATOS. H.
            txtTransporte.Text = row.Cells["RazonSocial"].Value.ToString();
        }

        private void dgvTransportes_KeyDown(object sender, KeyEventArgs e)
        {
            //PRESIONO DEL?
            if (e.KeyCode == Keys.Delete)
            {
                this.btnBorrar.PerformClick(); //LLAMAR A EVENTO CLICK DEL BOTON BORRAR
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (this.dgvTransportes.Rows.Count > 0)
            {
                // Capturo la posición de la fila desde donde se llamo
                this.indexFila = dgvTransportes.CurrentRow.Index;
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
            txtTransporte.Focus();
            }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            // Capturo la posición de la fila desde donde se llamo
            this.indexFila = dgvTransportes.CurrentRow.Index;

            // Cambio el estado del formulario a Modificar. G.
            this.myEstado = "M";
            // Activo los botones para este estado. G.
            ActivarBotones();
            // Habilito los controles para este estado. G.
            HabilitarControles();
            // Posiciono el foco sobre el primer textbox
            txtTransporte.Focus();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            // Capturo la posición de la fila desde donde se llamo
            this.indexFila = dgvTransportes.CurrentRow.Index;

            // Pregunto si el usuario actual es el mismo que se quiere modificar. G.
            if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja)
            {
                //TRAER LA FILA ACTUAL. N.
                DataGridViewRow row = dgvTransportes.CurrentRow;
                // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
                int Id = Convert.ToInt32(row.Cells["IdTransporte"].Value);
                string Transporte = row.Cells["RazonSocial"].Value.ToString();
                // Confirma eliminar el registro ?
                DialogResult dlResult = MessageBox.Show("Desea Eliminar el Transporte: " + Transporte + " ?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Si confirma salir de la aplicación....
                if (dlResult == DialogResult.Yes)
                {
                    string myCadena = "UPDATE Transportes SET Activo = 0 WHERE IdTransporte =" + Id;
                    clsDataBD.GetSql(myCadena);
                    // Refresco la grilla
                    this.CargarTransporte();
                    //Foco
                    PosicionarFocoFila();
                }
            }
            else
            {
                // El usuario no es de nivel 5, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para Eliminar un Transporte", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Creo la cadena para grabar las Modificaciones de la Localidad
            string myCadena = "";
            // Creo una nueva Localidad. G.
            clsTransporte NvoTransp = new clsTransporte();
            // Tomo la línea actual de la grilla. G.
            DataGridViewRow row = this.dgvTransportes.CurrentRow;
            // Verifico el estado del formulario para saber si estoy creando o modificando una Localidad. G.
            // Paso los datos del formulario a la nueva Localidad
            NvoTransp.IdTransporte = Convert.ToInt32(row.Cells["IdTransporte"].Value);
            NvoTransp.RazoSocial = txtTransporte.Text; //N

            //Vector Errores
            string[] cErrores = NvoTransp.cValidaTransp();
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
                    myCadena = "INSERT INTO Transportes (RazonSocial, Activo) values ('" + NvoTransp.RazoSocial +
                                      "', " + "1)";
                    // Ejecuto la consulta SQL
                    clsDataBD.GetSql(myCadena);
                    // Lleno nuevamente la grilla
                    this.CargarTransporte();
                    // Regreso el formulario a su estado inicial
                    this.btnCancelar.PerformClick();
                    return;

                case "B":
                    //Tomar el Id
                    Id = Convert.ToInt32(dgvTransportes.CurrentRow.Cells["IdTransporte"].Value.ToString());
                    // Cambio mi estado
                    this.myEstado = "C";
                    // Lleno nuevamente la grilla
                    this.CargarTransporte();
                    // Activo todos los botones
                    ActivarBotones();
                    // Habilito los controles
                    HabilitarControles();
                    //Id >0? Solo cuando busca reposiciona por ID
                    if (!(Id == 0 && bSearch))
                    {
                        ReposicionarById();
                        Id = 0;
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
                    myCadena = "UPDATE Transportes SET RazonSocial = '" + NvoTransp.RazoSocial +
                                "' WHERE IdTransporte = " + NvoTransp.IdTransporte;
                    // Ejecuto la consulta SQL
                    clsDataBD.GetSql(myCadena);
                    // Lleno nuevamente la grilla
                    this.CargarTransporte();
                    // Regreso el formulario a su estado inicial
                    this.btnCancelar.PerformClick();
                    return;
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Recargar
            if (this.myEstado == "B" && bSearch)
            {
                // Cargo las localidades
                this.CargarTransporte();
                // Cambio el estado del formulario a agregar. G.
                this.myEstado = "C";
                // Limpio los controles del formulario. G.    
                ActivarBotones();
                // Habilito los controles para este estado. G.
                HabilitarControles();
                //Foco
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
            foreach (DataGridViewRow myRow in this.dgvTransportes.Rows)            
            {
                if (Convert.ToInt32(myRow.Cells["IdTransporte"].Value.ToString()) == Id)
                {
                    // Devuelvo el foco a la fila de la grilla desde donde se llamó
                    this.dgvTransportes.CurrentCell = dgvTransportes[1, myRow.Index];
                   
                    // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                    EventArgs ea = new EventArgs();
                    this.dgvTransportes_SelectionChanged(this.dgvTransportes, ea);

                    //Salir
                    break;
                }
            }
        }

        #endregion

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //Sino es null
            if (!(pmyTextBox == null))
            {
                //Id
                pmyTextBox.Text = dgvTransportes.CurrentRow.Cells["IdTransporte"].Value.ToString();
            }
            //Cerrar
            this.Close();
        }

        #region Método que devuelve el foco a la fila desde donde se presionó algún botón

        private void PosicionarFocoFila()
        {
            if (dgvTransportes.Rows.Count != 0 && dgvTransportes.Rows.Count > this.indexFila)
            {
                // Devuelvo el foco a la fila de la grilla desde donde se llamó
                this.dgvTransportes.CurrentCell = dgvTransportes[1, this.indexFila];

                // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                EventArgs ea = new EventArgs();
                this.dgvTransportes_SelectionChanged(this.dgvTransportes, ea);
            }
        }

        #endregion

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Capturo la posición de la fila desde donde se llamo
            this.indexFila = dgvTransportes.CurrentRow.Index;
            // Cambio mi estado a Búsqueda
            this.myEstado = "B";
            // Habilito los botones según mi estado
            ActivarBotones();
            // Habilito los campos de búsqueda
            HabilitarControles();
            // Limpio los campos de búsqueda
            LimpiarControlesBusqueda();
            // Pongo el foco en el primer control de búsqueda
            txtBuscaRs.Focus();
        }

        #region Método para limpiar los campos de Búsqueda
        // Limpia los controles de búsqueda del form
        private void LimpiarControlesBusqueda()
        {
            this.txtBuscaRs.Text = "";
            this.txtBuscaCod.Text = "";
        }

        #endregion

        private void txtBuscaCod_TextChanged(object sender, EventArgs e)
        {
            if (!(txtBuscaCod.Text == ""))
            {
                // Cargo las localidades filtradas por la búsqueda
                CargarTransporte(txtBuscaCod.Text, "IdTransporte");
            }
        }

        private void txtBuscaRs_TextChanged(object sender, EventArgs e)
        {
            if (!(txtBuscaRs.Text == ""))
            {
                // Cargo las localidades filtradas por la búsqueda
                CargarTransporte(txtBuscaRs.Text, "RazonSocial");
            }
        }

        private void txtBuscaCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Solo enteros
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }    
        }
    }
}

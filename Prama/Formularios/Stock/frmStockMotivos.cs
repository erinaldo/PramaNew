using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prama.Formularios.Stock
{
    public partial class frmStockMotivos : Form
    {
        string myEstado = "C";
        int indexFila = 0;

        public frmStockMotivos()
        {
            InitializeComponent();
        }

        #region Eventos del Formulario

        #region Evento Load del Formulario

        private void frmStockMotivos_Load(object sender, EventArgs e)
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
            // Si el usuario es de vivel 5, cambio el ancho de la columna porque se pierde información
            if (clsGlobales.UsuarioLogueado.Nivel >=clsGlobales.cParametro.NivelStock)
            {
                this.dgvMotivos.Columns[1].Width = 255;
            }
            
        }

        #endregion

        #region Eventos de los botones del Formulario

        #region Evento Click del botón Agregar

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (dgvMotivos.Rows.Count > 0)
            {
                // Paso a la variable el index de la fila
                this.indexFila = dgvMotivos.CurrentRow.Index;
            }
            
            // Pregunto si el usuario actual tiene nivel mayor a 2, lo dejo agregar. G.
            if (clsGlobales.UsuarioLogueado.Nivel > 4)
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
                txtMotivo.Focus();
            }
            // Pregunto si el usuario actual tiene nivel menor o igual a 4, No lo dejo agregar. G.
            else
            {
                // vuelvo el formulario al estado anterior. G.
                this.myEstado = "C";
                // El usuario no es de nivel 5, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para crear un nuevo Motivo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento Click del botón Borrar

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (dgvMotivos.Rows.Count > 0)
            {
                // Paso a la variable el index de la fila
                this.indexFila = dgvMotivos.CurrentRow.Index;
            }
            
            //Validar el nivel del usuario para ver si tiene permisos
            if (!(clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja))
            {
                // El usuario no es de nivel 5, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para Eliminar un Motivo de Movimiento de Stock!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Fin
                return;
            }

            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvMotivos.CurrentRow;
            //TRAER LOS DATOS. H.
            int IdStockMot = Convert.ToInt32(row.Cells["IdStockMotivo"].Value);
            string sMotivo = row.Cells["StockMotivo"].Value.ToString();
            // Confirmo la eliminación
            DialogResult myConfirmacion = MessageBox.Show("Desea eliminar el Motivo " + sMotivo, "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Si confirma
            if (myConfirmacion == DialogResult.Yes)
            {
                // Primero verifico que el motivo no se haya usado ya en movimientos de artículos
                string myCadenaSql = "select * from StockMovimientos where IdStockMotivo = " + IdStockMot;
                // Ejecuto la consulta y paso los datos a una tabla
                DataTable myTabla = clsDataBD.GetSql(myCadenaSql);
                // Si la tabla tiene filas
                if (myTabla.Rows.Count > 0)
                {
                    // Informo que no se puede eliminar por tener registros asociados
                    MessageBox.Show("El Motivo " + sMotivo + " tiene movimientos registrados, por lo que no se puede eliminar", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // regreso al formulario
                    return;
                }
                else
                {
                    // Armo la cadena SQL
                    myCadenaSql = "update StockMotivos set Activo = 0 where IdStockMotivo = " + IdStockMot;
                    // Ejecuto la consulta
                    clsDataBD.GetSql(myCadenaSql);
                    // Recargo el formulario
                    RecargarFormulario();
                }

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
            // Verifico que haya datos
            if (txtMotivo.Text == "")
            {
                // Muestro un mensaje indicando que no se encontraron los datos
                MessageBox.Show("El campo Motivo no puede estar vacío", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // pongo el foco en el control
                txtMotivo.Focus();
                // Regreso el formulario a su estado inicial
                return;
            }
            
            // Paso a variables los datos de los campos
            string sMotivo = txtMotivo.Text.ToUpper();
            int iEntrada = Convert.ToInt32(chkEntrada.CheckState);
            int iShow = Convert.ToInt32(chkShow.CheckState);
            
            // Armo la cadena SQL
            string myCadenaSql = "insert into StockMotivos (StockMotivo,Entrada,Activo,Show) values ('" +
                                sMotivo + "', " +
                                iEntrada + ", 1, " +
                                iShow + ")";
            // Ejecuto la consulta SQL
            clsDataBD.GetSql(myCadenaSql);

            // Recargo el formulario
            RecargarFormulario();

        }

        #endregion

        #region Evento Click del botón Cancelar

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Recargo el formulario
            RecargarFormulario();
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

        #region Eventos de la Grilla

        #region Evento CellContentClick de la Grilla

        private void dgvMotivos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvMotivos.RowCount == 0)
            {
                return;
            }
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvMotivos.CurrentRow;
            // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
            //TRAER LOS DATOS. H.
            txtMotivo.Text = row.Cells["StockMotivo"].Value.ToString();
            // Si Entrada es true, marco la casilla
            if (Convert.ToBoolean(row.Cells["Entrada"].Value))
            {
                chkEntrada.Checked = true;
            }
            else
            {
                chkEntrada.Checked = false;
            }
            // Si Show es true, marco la casilla
            if (Convert.ToBoolean(row.Cells["Show"].Value))
            {
                chkShow.Checked = true;
            }
            else
            {
                chkShow.Checked = false;
            }

        }

        #endregion

        #region Evento SelectionChanged de la Grilla

        private void dgvMotivos_SelectionChanged(object sender, EventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvMotivos.RowCount == 0)
            {
                return;
            }
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvMotivos.CurrentRow;
            // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
            //TRAER LOS DATOS. H.
            txtMotivo.Text = row.Cells["StockMotivo"].Value.ToString();
            // Si Entrada es true, marco la casilla
            if (Convert.ToBoolean(row.Cells["Entrada"].Value))
            {
                chkEntrada.Checked = true;
            }
            else
            {
                chkEntrada.Checked = false;
            }
            // Si Show es true, marco la casilla
            if (Convert.ToBoolean(row.Cells["Show"].Value))
            {
                chkShow.Checked = true;
            }
            else
            {
                chkShow.Checked = false;
            }
        }

        #endregion

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
                    this.btnAceptar.TabStop = false;
                    this.btnAceptar.Visible = false;
                    this.btnCancelar.TabStop = false;
                    this.btnCancelar.Visible = false;
                    this.btnSalir.TabStop = true;
                    this.btnSalir.Visible = true;
                    // Si el nivel del usuario es el 5, habilito el botón borrar
                    if (clsGlobales.UsuarioLogueado.Nivel >=5)
                    {
                        this.btnBorrar.TabStop = true && (dgvMotivos.RowCount != 0);
                        this.btnBorrar.Visible = true && (dgvMotivos.RowCount != 0);
                    }
                    else
                    {
                        this.btnBorrar.TabStop = false;
                        this.btnBorrar.Visible = false;
                    }
                    this.btnImprimir.TabStop = true && (dgvMotivos.RowCount != 0);
                    this.btnImprimir.Visible = true && (dgvMotivos.RowCount != 0);
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
                    this.txtMotivo.TabStop = true;
                    this.txtMotivo.Enabled = true;
                    this.chkEntrada.TabStop = true;
                    this.chkEntrada.Enabled = true;
                    if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelStock)
                    {
                        this.lblShow.Visible = true;
                        this.chkShow.TabStop = true;
                        this.chkShow.Enabled = true;
                        this.chkShow.Visible = true;
                    }
                    else
                    {
                        this.lblShow.Visible = false;
                        this.chkShow.TabStop = false;
                        this.chkShow.Enabled = false;
                        this.chkShow.Visible = false;
                    }
                    this.dgvMotivos.TabStop = false;
                    this.dgvMotivos.Enabled = false;

                    return;
                case "B":
                    this.txtMotivo.TabStop = false;
                    this.txtMotivo.Enabled = false;
                    this.dgvMotivos.TabStop = true && (dgvMotivos.RowCount > 0);
                    this.dgvMotivos.Enabled = true && (dgvMotivos.RowCount > 0);

                    return;
                case "C":
                    this.txtMotivo.TabStop = false;
                    this.txtMotivo.Enabled = false;
                    this.chkEntrada.TabStop = false;
                    this.chkEntrada.Enabled = false;
                    if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelStock)
                    {
                        this.lblShow.Visible = true;
                        this.chkShow.TabStop = false;
                        this.chkShow.Enabled = false;
                        this.chkShow.Visible = true;
                    }
                    else
                    {
                        this.lblShow.Visible = false;
                        this.chkShow.TabStop = false;
                        this.chkShow.Enabled = false;
                        this.chkShow.Visible = false;
                    }
                    this.dgvMotivos.TabStop = true && (dgvMotivos.RowCount > 0);
                    this.dgvMotivos.Enabled = true && (dgvMotivos.RowCount > 0);

                    

                    return;
            }
        }
        #endregion

        #region Método para limpiar los controles del formulario
        //LIMPIA LA PROPIEDAD TEXT DE LOS CONTROLES DEL FORMULARIO. N.
        private void LimpiarControlesForm()
        {
            this.txtMotivo.Text = "";
            this.chkEntrada.Checked = false;
            this.chkShow.Checked = false;
        }
        #endregion

        #region Método para cargar la grilla

        private void CargarRubros()
        {
            // Variable para la cadena SQL
            string myCadena = "";
            // Cadena SQL 
            if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelStock)
            {
                myCadena = "select * from StockMotivos where Activo = 1";
            }
            else
            {
                myCadena = "select * from StockMotivos where Activo = 1 and Show = 1";
            }
            
            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            mDtTable = clsDataBD.GetSql(myCadena);
            // Evito que el dgv genere columnas automáticas
            dgvMotivos.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvMotivos.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvMotivos.Rows.Count;
            // Posiciono la grilla en la última fila
            //dgvLocalidades.CurrentCell = dgvLocalidades[1, Filas - 1];

        }

        #endregion

        #region CargarToolTips

        private void CargarToolTips()
        {
            toolTip1.SetToolTip(this.btnAgregar, "Agregar");
            toolTip3.SetToolTip(this.btnBorrar, "Borrar");
            toolTip4.SetToolTip(this.btnImprimir, "Imprimir");
            toolTip5.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip6.SetToolTip(this.btnCancelar, "Cancelar");
            toolTip7.SetToolTip(this.btnSalir, "Salir");
        }

        #endregion

        #region Método que recarga el formulario

        private void RecargarFormulario()
        {
            // Cambio el estado del formulario a agregar. G.
            this.myEstado = "C";
            // Cargo las localidades
            CargarRubros();
            // Limpio los controles del formulario. G.    
            ActivarBotones();
            // Habilito los controles para este estado. G.
            HabilitarControles();

            // Si tengo filas en la grilla
            if (dgvMotivos.Rows.Count > 0)
            {
                // Posiciono el foco en la fila desde donde se llamo
                PosicionarFocoFila();
            }

            // Si el usuario es de vivel 5, cambio el ancho de la columna porque se pierde información
            if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelStock)
            {
                this.dgvMotivos.Columns[1].Width = 255;
            }
        }

        #endregion

        #region Método que devuelve el foco a la fila desde donde se presionó algún botón

        private void PosicionarFocoFila()
        {
            if (dgvMotivos.Rows.Count != 0 && dgvMotivos.Rows.Count > this.indexFila)
            {
                // Devuelvo el foco a la fila de la grilla desde donde se llamó
                this.dgvMotivos.CurrentCell = dgvMotivos[2, this.indexFila];

                // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                EventArgs ea = new EventArgs();
                this.dgvMotivos_SelectionChanged(this.dgvMotivos, ea);
            }

        }

        #endregion

        #endregion
    }
}

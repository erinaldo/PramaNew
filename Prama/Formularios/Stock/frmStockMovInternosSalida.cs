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
    public partial class frmStockMovInternosSalida : Form
    {
        
        #region Variables del formulario
        
        // Declaro la variable que va a almacenar la cadena SQL
        string myCadena = "";
        // Bandera que controla la carga de la segunda grilla
        bool yaCargado = false;
        //Estado
        string myEstado = "C";
        // Bandera que controla el combo para búsquedas
        bool bBanderaCombo = false;

        #endregion

        #region Constructor del formulario

        public frmStockMovInternosSalida()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos del formulario

        #region Evento Load

        private void frmStockMovInternosSalida_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 
			
            // Cargo los datos a la grilla y formateo todo
            RecargarForm();

            // Cargo los combos
            CargarCombos();
            // Cambio el estado de la bandera para habilitar el combo para búsquedas
            bBanderaCombo = true;
        }

        #endregion

        #region Eventos de las grillas

        #region Evento SelectionChanged de la grilla dgvComprobantes

        private void dgvComprobantes_SelectionChanged(object sender, EventArgs e)
        {
            if (yaCargado && dgvComprobantes.Rows.Count > 0)
            {
                // Vacío la grilla
                dgvDetalle.DataSource = null;
                // Evito que el dgvUsuarios genere columnas automáticas
                dgvDetalle.AutoGenerateColumns = false;
                // Declaro una variable que va a guardar el Id del comprobante
                int IdMovimiento = Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdMovimientoInterno"].Value);
                // Armo la cadena SQL para ejecutar el procedieminto almacenado que trae el detalle del comprobante
                string newMyCadenaSql = "exec StockMID " + IdMovimiento;
                // Creo un datatable y le paso los datos de la consulta SQL
                DataTable myTabla = new DataTable();

                myTabla = clsDataBD.GetSql(newMyCadenaSql);

                // Asigno a la grilla el source
                dgvDetalle.DataSource = myTabla;
                // Si tengo filas en la grilla, calculo el total del artículo
                if (dgvDetalle.Rows.Count > 0)
                {
                    AgregarItem();
                }

            }
        }

        #endregion

        #region Evento Click de la grilla dgvComprobantes

        private void dgvComprobantes_Click(object sender, EventArgs e)
        {

            if (dgvComprobantes.Rows.Count > 0)
            {
                // Cambio el estado de la bandera para que se ejecute el selection changed
                yaCargado = true;
                // Vacío la grilla
                dgvDetalle.DataSource = null;
                // Evito que el dgvUsuarios genere columnas automáticas
                dgvDetalle.AutoGenerateColumns = false;

                // Declaro una variable que va a guardar el Id del comprobante
                int IdMovimiento = Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdMovimientoInterno"].Value);
                // Armo la cadena SQL para ejecutar el procedieminto almacenado que trae el detalle del comprobante
                string newMyCadenaSql = "exec StockMID " + IdMovimiento;
                // Creo una tabla
                DataTable myTabla = new DataTable();

                // Creo un datatable y le paso los datos de la consulta SQL
                myTabla = clsDataBD.GetSql(newMyCadenaSql);

                // Asigno a la grilla el source
                dgvDetalle.DataSource = myTabla;
                // Si tengo filas en la grilla, calculo el total del artículo
                if (dgvDetalle.Rows.Count > 0)
                {
                    AgregarItem();
                }
            }
            
        }

        #endregion

        #endregion

        #region Eventos de los campos de búsqueda

        #region Eventos Enter de los campos de búsqueda

        private void txtNumero_Enter(object sender, EventArgs e)
        {
            // Si el otro campo de búsqueda tiene algo, lo borro
            if (!(txtFecha.Text == ""))
            {
                // Limpio el ptro campo de búsqueda
                txtFecha.Text = "";
            }
            // SI el combo tiene algo seleccionado
            if (cboMotivos.SelectedIndex != -1)
            {
                // Lo vacío
                cboMotivos.SelectedIndex = -1;
            }
        }


        private void txtFecha_Enter(object sender, EventArgs e)
        {
            // Si el otro campo de búsqueda tiene algo, lo borro
            if (!(txtNumero.Text == ""))
            {
                // Limpio el ptro campo de búsqueda
                txtNumero.Text = "";
            }
            // SI el combo tiene algo seleccionado
            if (cboMotivos.SelectedIndex != -1)
            {
                // Lo vacío
                cboMotivos.SelectedIndex = -1;
            }
        }

        private void cboMotivos_Enter(object sender, EventArgs e)
        {
            // Si el otro campo de búsqueda tiene algo, lo borro
            if (!(txtFecha.Text == ""))
            {
                // Limpio el ptro campo de búsqueda
                txtFecha.Text = "";
            }
            // Si el otro campo de búsqueda tiene algo, lo borro
            if (!(txtNumero.Text == ""))
            {
                // Limpio el ptro campo de búsqueda
                txtNumero.Text = "";
            }
        }

        #endregion

        #region Evento KeyPress del campo txtNumero

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Solo enteros
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        #endregion

        #region Eventos TextChanged de los campos de búsqueda

        private void txtFecha_TextChanged(object sender, EventArgs e)
        {
            // Cargo los comprobantes filtrados por la búsqueda
            CargarGrilla(txtFecha.Text.ToUpper(), "Fecha");
        }

        private void txtNumero_TextChanged(object sender, EventArgs e)
        {
            // Cargo los comprobantes filtrados por la búsqueda
            CargarGrilla(txtNumero.Text.ToUpper(), "Numero");
        }

        private void cboMotivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bBanderaCombo)
            {
                // Cargo los comprobantes filtrados por la búsqueda
                CargarGrilla(Convert.ToInt32(cboMotivos.SelectedValue).ToString(), "IdStockMotivo");
            }
        }

        #endregion

        #endregion

        #region Eventos de los botones

        #region Evento Click del botón Agregar

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            
            // capturo la posición de la fila
            if (dgvComprobantes.Rows.Count > 0)
            {
                clsGlobales.indexFila = this.dgvComprobantes.CurrentRow.Index;
            }

            // Cambio el estado en el que estoy
            this.myEstado = "A";

            // Creo el formulario y hago la llamada
            Prama.Formularios.Stock.frmStockMovInternosSalidasABM myForm = new frmStockMovInternosSalidasABM();
            myForm.ShowDialog();
            
            // Cargo la grilla
            CargarGrilla("", "");
            // Pongo el foco de la fila
            PosicionarFocoFila();
            // Cambio el estado a en espera
            this.myEstado = "C";
        }

        #endregion

        #region Evento Click del botón Eliminar

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            
            // Si es la última fila de la grilla
            if (dgvComprobantes.CurrentRow.Index == dgvComprobantes.Rows.Count - 1)
            {
                // capturo la posición de la fila
                clsGlobales.indexFila = this.dgvComprobantes.CurrentRow.Index - 1;
            }
            else
            {
                // capturo la posición de la fila
                clsGlobales.indexFila = this.dgvComprobantes.CurrentRow.Index;
            }


            // Tomo el Id del comprobante que se quiere eliminar
            int IdMovimientoInterno = Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdMovimientoInterno"].Value);
            // Pregunt si realmente quiere borrar el comprobante
            DialogResult Respuesta = MessageBox.Show("Desea eliminar el comprobante Nro: " + dgvComprobantes.CurrentRow.Cells["Numero"].Value +
                                                    "?", "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Respuesta == DialogResult.Yes)
            {
                // Armo la cadena
                myCadena = "update StockMovimientosInternos set Activo = 0 where IdMovimientoInterno = " + IdMovimientoInterno;
                // Ejecuto la consulta
                clsDataBD.GetSql(myCadena);

                // Actualizo el stock del artículo
                ActualizarStock();

            }

            // Recargo el formulario
            RecargarForm();
        }

        #endregion

        #region Evento Click del botón Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.myEstado = "B";
            // Achico la grilla
            dgvDetalle.Height = 90;
            // Muestro los campos de búsqueda
            gpbBusquedas.Visible = true;
            // Limpio los campos de búsqueda
            LimpiarCamposBusqueda();
            // Pongo el foco en el campo Número
            txtNumero.Focus();
            // Habilito los controles
            HabilitarControles();
            // Activo los botones
            ActivarBotones();
        }

        #endregion

        #region Evento Click del botón Cancelar

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Recargo el formulario
            RecargarForm();
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

        #endregion

        #region Métodos del formulario

        #region Método que carga la grilla de los comprobantes

        private void CargarGrilla(string Buscar, string Campo)
        {
            // Variable para la cadena SQL
            if (Buscar == "")
            {
                // Cadena SQL 
                myCadena = "SELECT * FROM Vista_StockMovimientoInterno WHERE Salida = 1 and Activo = 1";
            }
            else
            {
                // Cadena SQL 
                myCadena = "SELECT * FROM Vista_StockMovimientoInterno WHERE Salida = 1 and Activo = 1 and " + Campo + " like '" + Buscar + "%' order by " + Campo;
            }

            // Evito que el dgv genere columnas automáticas
            dgvComprobantes.AutoGenerateColumns = false;
            // Creo un nuevo DataTable
            DataTable mDtTable = new DataTable();
                
            // Le asigno al nuevo DataTable los datos de la consulta SQL
            mDtTable = clsDataBD.GetSql(myCadena);
            
            // Asigno el source de la grilla
            dgvComprobantes.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvComprobantes.Rows.Count;
            // Si hay filas
            if (Filas > 0)
            {
                // Cambio el estado de la bandera para que ejecute el selection changed de la grilla
                yaCargado = true;

                // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                EventArgs ea = new EventArgs();
                this.dgvComprobantes_SelectionChanged(this.dgvComprobantes, ea);
                // Habilito los botones que puedan estar deshabilitados
                
                this.btnBorrar.Enabled = true;
                this.btnImprimir.Enabled = true;
                this.btnBuscar.Enabled = true;
            }
            else
            {
                // Vacío la grilla de detalle
                dgvDetalle.DataSource = null;
                // Deshabilito los botones que generarían error al no tener datos la grilla Comprobantes
                
                this.btnBorrar.Enabled = false;
                this.btnImprimir.Enabled = false;
                this.btnBuscar.Enabled = false;
            }
        }

        #endregion

        #region Método que carga los ToolsTip del formulario

        private void CargarToolsTip()
        {
            toolTip1.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip2.SetToolTip(this.btnCancelar, "Cancelar");
            toolTip3.SetToolTip(this.btnSalir, "Salir");
            toolTip4.SetToolTip(this.btnAgregar, "Nuevo");
            //toolTip5.SetToolTip(this.btnModificar, "Editar");
            toolTip6.SetToolTip(this.btnBorrar, "Eliminar");
            toolTip7.SetToolTip(this.btnBuscar, "Buscar");
            toolTip8.SetToolTip(this.btnImprimir, "Imprimir");
        }

        #endregion

        #region Método que deshabilita el reordenamiento de las grilla desde sus cabeceras

        private void DeshabilitarOrdenGrillas()
        {
            // Deshabilito la reordenación de las columnas de las grillas
            foreach (DataGridViewColumn dgvCol in dgvComprobantes.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn dgvCol in dgvDetalle.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        #endregion

        #region Método que devuelve el foco a la fila desde donde se presionó algún botón

        private void PosicionarFocoFila()
        {
            if (dgvComprobantes.Rows.Count != 0 && dgvComprobantes.Rows.Count > clsGlobales.indexFila)
            {
                if (!(clsGlobales.indexFila < 0))
                {
                    // Devuelvo el foco a la fila de la grilla desde donde se llamó
                    this.dgvComprobantes.CurrentCell = dgvComprobantes[1, clsGlobales.indexFila];
                }
                // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                EventArgs ea = new EventArgs();
                this.dgvComprobantes_SelectionChanged(this.dgvComprobantes, ea);
            }

        }

        #endregion

        #region Método que agrega el número de Item a las filas

        private void AgregarItem()
        {
            // Contador que me va a poner el número de item en la grilla
            int myFila = 1;
            // Recorro la grilla y hago el calculo
            foreach (DataGridViewRow row in dgvDetalle.Rows)
            {
                row.Cells["Item"].Value = myFila;
                myFila++;
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
                    // this.btnModificar.TabStop = false;
                    // this.btnModificar.Visible = false;
                    this.btnBuscar.TabStop = false;
                    this.btnBuscar.Visible = false;
                    return;
                case "B":
                    this.btnAgregar.TabStop = false;
                    this.btnAgregar.Visible = false;
                    this.btnBuscar.TabStop = false;
                    this.btnBuscar.Visible = false;
                    this.btnAceptar.TabStop = false;
                    this.btnAceptar.Visible = false;
                    this.btnCancelar.TabStop = true;
                    this.btnCancelar.Visible = true;
                    this.btnSalir.TabStop = true;
                    this.btnSalir.Visible = true;
                    this.btnBorrar.TabStop = false;
                    this.btnBorrar.Visible = false;
                    this.btnImprimir.TabStop = true && (dgvComprobantes.RowCount != 0);
                    this.btnImprimir.Visible = true && (dgvComprobantes.RowCount != 0);
                    // this.btnModificar.TabStop = false;
                    // this.btnModificar.Visible = false;
                    this.btnBuscar.TabStop = false;
                    this.btnBuscar.Visible = false;
                    return;
                case "C":
                    this.btnAgregar.TabStop = true;
                    this.btnAgregar.Visible = true;
                    this.btnBuscar.TabStop = true;
                    this.btnBuscar.Visible = true;
                    this.btnAceptar.TabStop = false;
                    this.btnAceptar.Visible = false;
                    this.btnCancelar.TabStop = false;
                    this.btnCancelar.Visible = false;
                    this.btnSalir.TabStop = true;
                    this.btnSalir.Visible = true;
                    // Si el nivel del usuario es el 5, habilito el botón borrar
                    if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelStock)
                    {
                        this.btnBorrar.TabStop = true && (dgvComprobantes.RowCount != 0);
                        this.btnBorrar.Visible = true && (dgvComprobantes.RowCount != 0);
                    }
                    else
                    {
                        this.btnBorrar.TabStop = false;
                        this.btnBorrar.Visible = false;
                    }
                    this.btnImprimir.TabStop = true && (dgvComprobantes.RowCount != 0);
                    this.btnImprimir.Visible = true && (dgvComprobantes.RowCount != 0);

                    // this.btnModificar.TabStop = true && (dgvComprobantes.RowCount != 0);
                    // this.btnModificar.Visible = true && (dgvComprobantes.RowCount != 0);
                    this.btnBuscar.TabStop = true && (dgvComprobantes.RowCount != 0);
                    this.btnBuscar.Visible = true && (dgvComprobantes.RowCount != 0);
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
                    this.gpbBusquedas.Visible = false;

                    return;
                case "B":
                    this.gpbBusquedas.Visible = true;

                    return;
                case "C":
                    this.gpbBusquedas.Visible = false;

                    return;
            }
        }
        #endregion

        #region Método que carga el Combo del formulario

        private void CargarCombos()
        {
            // Cargo el combo de los Motivos que se pueden mostrar
            clsDataBD.CargarCombo(cboMotivos, "StockMotivos", "StockMotivo", "IdStockMotivo", "Show = 1");
            // Dejo vacía la selección del combo
            cboMotivos.SelectedIndex = -1;
        }

        #endregion

        #region Método que limpia los campos de búsqueda del formulario

        private void LimpiarCamposBusqueda()
        {
            txtNumero.Text = "";
            txtFecha.Text = "";
            cboMotivos.SelectedIndex = -1;
        }

        #endregion

        #region Método que graba los cambios  a los stock de los artículos

        private void ActualizarStock()
        {
            // Verifico si es entrada
            bool bEntrada = RetornarEntrada(Convert.ToInt32(cboMotivos.SelectedValue));
            // Variable auxiliares para cálculos
            double stockOriginal = 0;
            double dCantMov = 0;
            double dCant = 0;

            // Recorro la grilla
            foreach (DataGridViewRow NewRow in dgvDetalle.Rows)
            {
                if (NewRow.Cells["Cantidad"].Value != null)
                {
                    // Variable que almacena la cantidad en formato double
                    dCant = Convert.ToDouble(NewRow.Cells["Cantidad"].Value) * -1;
                }
                else
                {
                    dCant = 0;
                }

                dCantMov = dCant;
                // Busco el artículo para sumarle el stock
                string myCadenaSQL = "select * from Articulos where IdArticulo = " + Convert.ToInt32(NewRow.Cells["IdArticulo"].Value);
                // Ejecuto la consulta y paso los datos a una tabla
                DataTable myTablaArticulos = clsDataBD.GetSql(myCadenaSQL);
                // Recorro la tabla y tomo el valor del stock original
                foreach (DataRow row in myTablaArticulos.Rows)
                {
                    // Paso a la variable el stock actual
                    stockOriginal = Convert.ToDouble(row["Stock"]);
                }
                // Si el motivo del movimiento es entrada
                if (bEntrada)
                {
                    // al stock original le sumo la cantidad del movimiento
                    stockOriginal = stockOriginal + dCant;
                }
                // Si el motivo del movimiento no es entrada
                else
                {
                    // al stock original le resto la cantidad del movimiento
                    stockOriginal = stockOriginal - dCant;
                    //dCantMov = dCantMov * -1;
                }

                // Actualizo el artículo con el nuevo stock ( BASE 1)
                myCadenaSQL = "update Articulos set Stock = " + stockOriginal + " where IdArticulo = " + Convert.ToInt32(NewRow.Cells["IdArticulo"].Value);
                // Ejecuto la consulta
                clsDataBD.GetSql(myCadenaSQL);

                // Grabo el movimiento de stock
                GrabarMovimientoStock(Convert.ToInt32(NewRow.Cells["IdArticulo"].Value), Convert.ToInt32(cboMotivos.SelectedValue), DateTime.Now, dCantMov);
            }
        }

        #endregion

        #region Método que retorna si el Motivo elegido es entrada (ALTA)

        private bool RetornarEntrada(int IdMot)
        {
            // Variable de retorno
            bool aux = false; ;
            // Armo la cadena SQL
            string myCadenaSql = "select * from StockMotivos where IdStockMotivo = " + IdMot;
            // Ejecuto la consulta y lleno la tabla
            DataTable myTabla = clsDataBD.GetSql(myCadenaSql);
            // Reccorro la tabla y toma el valor en la variabe
            foreach (DataRow row in myTabla.Rows)
            {
                // Si es entrada
                if (Convert.ToBoolean(row["Entrada"]))
                {
                    // Paso el valor a la variable de retorno
                    aux = true;
                }
            }
            // Devuelvo el valor
            return aux;
        }


        #endregion

        #region Método que graba el movimiento de Stock en la tabla StockMovimientos

        private void GrabarMovimientoStock(int IdArt, int IdMot, DateTime Fec, double Cant)
        {
            // Variable que almacena la cadena SQL
            string myCadenaSql = "insert into StockMovimientos (IdArticulo, IdStockMotivo, Fecha, sFecha, Cantidad, IdUsuario, Activo) values ("
                                + IdArt + ", "
                                + IdMot + ", '"
                                + Fec + "', '"
                                + clsValida.ConvertirFecha(Fec) + "', "
                                + Cant + ", "
                                + clsGlobales.UsuarioLogueado.IdUsuario + ", 1)";
            // Ejecuto la consulta
            clsDataBD.GetSql(myCadenaSql);
        }

        #endregion

        #region Método que recarga el formulario después de alguna actualización

        private void RecargarForm()
        {
            // Cargo los toolstip de los botones
            CargarToolsTip();
            // Cargo la grilla
            CargarGrilla("", "");
            // Deshabilito el ordenamiento de las cabeceras de las grillas.
            DeshabilitarOrdenGrillas();
            // Cambio el alto de la grilla detalle para que tape los campos de búsqueda
            dgvDetalle.Height = 150;
            // Cuento la cantidad de filas de la grilla
            int filas = dgvComprobantes.Rows.Count;
            // Actualizo el valor de la fila para que sea la última de la grilla
            clsGlobales.indexFila = filas - 1;
            // Pongo el foco de la fila
            PosicionarFocoFila();
            // Cambio mi estado a en espera
            this.myEstado = "C";
            // Activo los botones
            ActivarBotones();
            // Habilito los controles
            HabilitarControles();
            
        }

        #endregion

        #endregion

    }
}

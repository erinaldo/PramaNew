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
    public partial class frmStockABM : Form
    {

        #region Variables del Formulario

        // Variable que almacena el Id del Artículo, Producto
        double dIdArticulo = 0;
        // Variable que almacena el tipo de articulo
        string Tipo = "";
        // BAndera que controla la habilitación de los eventos de la grilla
        bool BanderaGrilla = false;
        // Bandera que controla el comportamiento de la búsqueda
        bool BanderaBuscar = false;
        //Estado
        string myEstado = "C";
        //IndexFila
        int indexFila = 0;

        #endregion

        #region Constructor del Formulario

        public frmStockABM(string Tip)
        {
            // Inicializo los componentes del formulario
            InitializeComponent();
            // Paso el tipo de artículo a la variable del formulario
            Tipo = Tip;
        }

        #endregion

        #region Eventos del Formulario

        #region Evento Load del Formulario

        private void frmStockABM_Load(object sender, EventArgs e)
        {
            this.myEstado = "C";
			//icon
            clsFormato.SetIconForm(this); 
            // Deshabilito el reordenamiento de las grillas desde su cabecera
            DeshabilitarOrdenGrillas();
            // Cargo el combo del formulario con sus datos
            CargarCombos();
            // Cargo los datos de la tabla en la grilla
            CargarGrilla(Tipo, "Tabla");
            // Llamo al método activar los botones del formulario. G.
            ActivarBotones();
            // Llamo al método habilitar controles del formulario. G.
            HabilitarControles();
            // Cargar ToolTips
            CargarToolTips();
            //Titulo Ventana
            this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;
            // Si la grilla tiene datos
            if (dgvMovStock.Rows.Count > 0)
            {
                // Cambio el estado de la bandera
                BanderaGrilla = true;
                // Paso a la variable global el valor del Index
                this.indexFila = dgvMovStock.Rows.Count - 1;
                // Me posiciono en la última fila
                PosicionarFocoFila();
            }
            // Si el tipo de artículo es Insumo, cambio el label del txt
            if (Tipo == "INSUMOS")
            {
                lblCodigo.Text = "Código Ins.";
            }
            else
            {
                lblCodigo.Text = "Código Prod.";
            }
        }

        #endregion

        #region Eventos de los botones

        #region Evento Click del botón Agregar

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (dgvMovStock.Rows.Count > 0)
            {
                // Paso a la variable el index de la fila
                this.indexFila = dgvMovStock.CurrentRow.Index;
            }
            // Cambio el estado del formulario
            this.myEstado = "A";
            // limpio los controles
            LimpiarControlesForm();
            // Cambio el estado de la bandera de búswueda
            BanderaBuscar = false;
            // Habilito los controles
            HabilitarControles();
            // Habilito los botones
            ActivarBotones();
            // Posiciono el foco en el dtp de la fecha
            dtpFecha.Focus();
        }

        #endregion

        #region Evento Click del botón Borrar

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            //Validar el nivel del usuario para ver si tiene permisos
            if (!(clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja))
            {
                // El usuario no es de nivel 5, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para Eliminar el Movimiento de Stock!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Fin
                return;
            }
                        
            // Tomo los datos de la fila actual
            DataGridViewRow rowActual = dgvMovStock.CurrentRow;

            // Actualizo el Id del artículo para los movimientos de stock
            dIdArticulo = Convert.ToDouble(rowActual.Cells["IdArticulo"].Value);
            // Tomo el Id del movimiento de la tabla para pasarlo a inactivo
            int iIdStockMov = Convert.ToInt32(rowActual.Cells["IdStockMovimiento"].Value);
            // Valor del Id del motivo del movimiento
            int iIdMotivo = Convert.ToInt32(rowActual.Cells["IdStockMotivo"].Value);

            // Variables con los datos para el mensaje
            string Cant = rowActual.Cells["Cantidad"].Value.ToString();
            string Prod = rowActual.Cells["Articulo"].Value.ToString();

             // Confirmo la eliminación
            DialogResult myConfirmacion = MessageBox.Show("Desea eliminar el Movimiento de " + Cant + " " + Prod, "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Si confirma
            if (myConfirmacion == DialogResult.Yes)
            {
                // Variable para el valor del stock original
                double stockOriginal = 0;
                // Variable que almacena la cantidad convertida
                double dCant = Convert.ToDouble(Cant);

                // Verifico si el movimiento original era de entrada
                bool bEntrada = RetornarEntrada(iIdMotivo);

                //**********************************************//
                // ACTUALIZACIÓN DEL STOCK DEL PRODUCTO
                //**********************************************//

                // Busco el artículo para sumarle el stock
                string myCadenaSQL = "select * from Articulos where IdArticulo = " + dIdArticulo;
                // Ejecuto la consulta y paso los datos a una tabla
                DataTable myTablaArticulos = clsDataBD.GetSql(myCadenaSQL);
                // Recorro la tabla y tomo el valor del stock original
                foreach (DataRow row in myTablaArticulos.Rows)
                {
                    // Paso a la variable el stock actual
                    stockOriginal = Convert.ToDouble(row["Stock"]);
                }
                // Si el valor que viene es negativo, lo paso a positivo
                if (dCant < 0)
                {
                    dCant = dCant * -1;
                }

                // Si el valor original era una entrada, ahora lo resto
                if (bEntrada)
                {
                    // Al stock actual le sumo lo fabricado
                    stockOriginal = stockOriginal - dCant;
                }
                else
                {
                    // Al stock actual le sumo lo fabricado
                    stockOriginal = stockOriginal + dCant;
                }
                
                // Actualizo el artículo con el nuevo stock
                myCadenaSQL = "update Articulos set Stock = " + stockOriginal + " where IdArticulo = " + dIdArticulo;
                // Ejecuto la consulta
                clsDataBD.GetSql(myCadenaSQL);

                // Paso a Inactivo el movimiento
                myCadenaSQL = "update StockMovimientos set Activo = 0 where IdStockMovimiento = " + iIdStockMov;
                // Ejecuto la consulta
                clsDataBD.GetSql(myCadenaSQL);

                // Cierro el formulario
                RecargarFormulario();
            }
        }

        #endregion

        #region Evento Click del botón Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Cambio el estado del formulario
            this.myEstado = "B";
            // Habilito los botones
            ActivarBotones();
            // Habilito los controles
            HabilitarControles();
            // limpio los campos de texto
            LimpiarControlesForm();
            // Cambio el estado de la bandera de búswueda
            BanderaBuscar = true;
            // Cambio el estado de la bandera que controla la grilla
            BanderaGrilla = false;
            // Pongo el foco en el primer control
            txtCodigo.Focus();
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
            // Variables con los datos de los txt para grabar
            string Cant = txtCantidad.Text;
            string Prod = txtProducto.Text;

            // Si alguno de los tres campos está vacío
            if (Cant == "" || Prod == "" || cboMotivo.SelectedIndex==-1)
            {
                // Lo informo con un mensaje
                MessageBox.Show("Debe seleccionar un Producto, seleccionar un motivo y llenar el campo cantidad", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Salgo del evento
                return;
            }

            // Variable auxiliares para cálculos
            double stockOriginal = 0;
            // Variable que almacena la cantidad en formato double
            double dCant = Convert.ToDouble(Cant);
            double dCantMov = dCant;

            // Mensaje de confirmación
            DialogResult myRespuesta = MessageBox.Show("Confirma el Movimiento " + cboMotivo.Text + " de " + Cant + " " + Prod + " ?", 
                                        "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Si confirma el movimiento
            if (myRespuesta == DialogResult.Yes)
            {
                // Verifico si es entrada
                bool bEntrada = RetornarEntrada(Convert.ToInt32(cboMotivo.SelectedValue));

                //**************************************//
                // ACTUALIZACIÓN DEL STOCK DEL PRODUCTO //
                //**************************************//

                // Busco el artículo para sumarle el stock
                string myCadenaSQL = "select * from Articulos where IdArticulo = " + dIdArticulo;
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
                
                // Actualizo el artículo con el nuevo stock
                myCadenaSQL = "update Articulos set Stock = " + stockOriginal + " where IdArticulo = " + dIdArticulo;
                // Ejecuto la consulta
                clsDataBD.GetSql(myCadenaSQL);

                // Grabo el movimiento de stock
                GrabarMovimientoStock(Convert.ToInt32(dIdArticulo), Convert.ToInt32(cboMotivo.SelectedValue), DateTime.Now, dCantMov);

                // Cierro el formulario
                RecargarFormulario();
            }
        }

        #endregion

        #region Evento Click del botón Cancelar

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Cierro el formulario
            RecargarFormulario();
        }

        #endregion

        #region Evento Click del botón Salir

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #endregion

        #region Eventos de los controles

        #region Eventos de la Grilla

        #region Evento SelectionChanged de la Grilla

        private void dgvMovStock_SelectionChanged(object sender, EventArgs e)
        {
            // Si ya se hizo click en la grilla
            if (BanderaGrilla)
            {
                // Paso los datos de la grilla a los controles
                TraerDatosGrilla();
                // Si el movimiento es interno, no se puede eliminar
                OpcionEliminarVisible();
            }
        }

        #endregion

        #region Evento CellContentClick de la Grilla

        private void dgvMovStock_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Si ya se hizo click en la grilla
            if (BanderaGrilla)
            {
                // Paso los datos de la grilla a los controles
                TraerDatosGrilla();
                // Si el movimiento es interno, no se puede eliminar
                OpcionEliminarVisible();
            }
        }

        #endregion

        #region Evento Click de la Grilla

        private void dgvMovStock_Click(object sender, EventArgs e)
        {
            // Cambio el estado de la bandera de la grilla
            BanderaGrilla = true;
        }

        #endregion

        #endregion

        #region Eventos de los TextBox

        #region Evento DoubleClick del txtCodigo

        private void txtCodigo_DoubleClick(object sender, EventArgs e)
        {
            // if (BanderaBuscar==true)
            {
                // Vacío de datos el vector de los Productos
                clsGlobales.ProductosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.ProductosSeleccionados, new int[] { 0, 2 });
                // Vacío de datos el vector de los Productos
                clsGlobales.InsumosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.InsumosSeleccionados, new int[] { 0, 2 });

                // Si la llamada viene desde Insumos
                if (Tipo == "INSUMOS")
                {
                    frmArticulosBuscar myForm = new frmArticulosBuscar(true, false);
                    myForm.ShowDialog();

                    int LargoInsumos = clsGlobales.InsumosSeleccionados.GetLength(0);

                    // si hay cargados productos o insumos en los vectores
                    if (!(clsGlobales.InsumosSeleccionados.Length / 2 == 0) && !((clsGlobales.InsumosSeleccionados.Length / 2) > 1))
                    {
                        // Paso a string el Id del proveedor y lo ingreso a la cadena
                        dIdArticulo = clsGlobales.InsumosSeleccionados[0, 0];

                        // Armo la cadena SQL
                        string myCadenaSQL = "select * from Articulos_Insumos_Productos where IdArticulo = " + dIdArticulo;
                        // Creo una tabla que me va a almacenar el resultado de la consulta
                        DataTable myTablaAux = clsDataBD.GetSql(myCadenaSQL);
                        // Recorro la tabla y paso los datos del Artículo a un datarow
                        foreach (DataRow row in myTablaAux.Rows)
                        {
                            txtCodigo.Text = row["CodigoArticulo"].ToString();
                            txtProducto.Text = row["Articulo"].ToString();
                        }

                        // Pongo el foco en el campo cantidad
                        cboMotivo.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionarse un Artículo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    frmArticulosBuscar myForm = new frmArticulosBuscar(false, true);
                    myForm.ShowDialog();

                    int LargoProductos = clsGlobales.ProductosSeleccionados.GetLength(0);

                    // si hay cargados productos o insumos en los vectores
                    if (!(clsGlobales.ProductosSeleccionados.Length / 2 == 0) && !((clsGlobales.ProductosSeleccionados.Length / 2) > 1))
                    {
                        // Paso a string el Id del proveedor y lo ingreso a la cadena
                        dIdArticulo = clsGlobales.ProductosSeleccionados[0, 0];

                        // Armo la cadena SQL
                        string myCadenaSQL = "select * from Articulos_Insumos_Productos where IdArticulo = " + dIdArticulo;
                        // Creo una tabla que me va a almacenar el resultado de la consulta
                        DataTable myTablaAux = clsDataBD.GetSql(myCadenaSQL);
                        // Recorro la tabla y paso los datos del Artículo a un datarow
                        foreach (DataRow row in myTablaAux.Rows)
                        {
                            txtCodigo.Text = row["CodigoArticulo"].ToString();
                            txtProducto.Text = row["Articulo"].ToString();
                        }

                        // Pongo el foco en el campo cantidad
                        cboMotivo.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionarse un Artículo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            
            
        }

        #endregion

        #region Evento TextChanged del txtCodigo

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            if (BanderaBuscar)
            {
                // Cargo la grilla por el campo buscado
                CargarGrilla(txtCodigo.Text.ToUpper(), "CodigoArticulo");
            }
            
        }

        #endregion

        #region Evento KeyPress del txtCantidad

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 44)
            {
                e.KeyChar = Convert.ToChar(46);
                ch = e.KeyChar;

            }
            //PUNTO DECIMAL. N.
            if (ch == 46 && base.Text.IndexOf('.') != -1)
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

            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                this.btnAceptar.Focus();
            }
        }

        #endregion

        #region Evento Leave del txtCantidad

        private void txtCantidad_Leave(object sender, EventArgs e)
        {
            // Si el txt no está vacío, lo formateo con dos decimales
            if (!(this.txtCantidad.Text == ""))
            {
                this.txtCantidad.Text = Convert.ToDouble(this.txtCantidad.Text).ToString("#0.00000");
            }
        }

        #endregion
                
        #endregion

        #endregion

        #endregion

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
                    this.btnImprimir.TabStop = true && (dgvMovStock.RowCount != 0);
                    this.btnImprimir.Visible = true && (dgvMovStock.RowCount != 0);
                    return;
                case "C":
                    this.btnAgregar.TabStop = true;
                    this.btnAgregar.Visible = true;
                    this.btnBuscar.TabStop = true && (dgvMovStock.RowCount != 0);;;
                    this.btnBuscar.Visible = true && (dgvMovStock.RowCount != 0);;;
                    this.btnAceptar.TabStop = false;
                    this.btnAceptar.Visible = false;
                    this.btnCancelar.TabStop = false;
                    this.btnCancelar.Visible = false;
                    this.btnSalir.TabStop = true;
                    this.btnSalir.Visible = true;
                    // Si el nivel del usuario es el 5, habilito el botón borrar
                    if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelStock)
                    {
                        this.btnBorrar.TabStop = true && (dgvMovStock.RowCount != 0);
                        this.btnBorrar.Visible = true && (dgvMovStock.RowCount != 0);
                    }
                    else
                    {
                        this.btnBorrar.TabStop = false;
                        this.btnBorrar.Visible = false;
                    }
                    this.btnImprimir.TabStop = true && (dgvMovStock.RowCount != 0);
                    this.btnImprimir.Visible = true && (dgvMovStock.RowCount != 0);
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
                    this.dtpFecha.TabStop = true;
                    this.dtpFecha.Enabled = true;
                    this.txtCodigo.TabStop = true;
                    this.txtCodigo.Enabled = true;
                    this.txtProducto.TabStop = false;
                    this.txtProducto.Enabled = false;
                    this.txtCantidad.TabStop = true;
                    this.txtCantidad.Enabled = true;
                    this.cboMotivo.TabStop = true;
                    this.cboMotivo.Enabled = true;
                    this.dgvMovStock.TabStop = false;
                    this.dgvMovStock.Enabled = false;

                    this.lblMotivo.Visible = true;
                    this.lblCantidad.Visible = true;
                    this.txtCantidad.Visible = true;
                    this.cboMotivo.Visible = true;

                    return;
                case "B":
                    this.dtpFecha.TabStop = false;
                    this.dtpFecha.Enabled = false;
                    this.txtCodigo.TabStop = true;
                    this.txtCodigo.Enabled = true;
                    this.txtProducto.TabStop = false;
                    this.txtProducto.Enabled = false;
                    this.cboMotivo.TabStop = false;
                    this.cboMotivo.Enabled = false;
                    this.txtCantidad.TabStop = false;
                    this.txtCantidad.Enabled = false;
                    this.dgvMovStock.TabStop = true;
                    this.dgvMovStock.Enabled = true;

                    return;
                case "C":
                    this.dtpFecha.TabStop = false;
                    this.dtpFecha.Enabled = false;
                    this.txtCodigo.TabStop = false;
                    this.txtCodigo.Enabled = false;
                    this.txtProducto.TabStop = false;
                    this.txtProducto.Enabled = false;
                    this.cboMotivo.TabStop = false;
                    this.cboMotivo.Enabled = false;
                    this.txtCantidad.TabStop = false;
                    this.txtCantidad.Enabled = false;
                    this.dgvMovStock.TabStop = true;
                    this.dgvMovStock.Enabled = true;

                    return;
            }
        }
        #endregion

        #region Método para limpiar los controles del formulario
        //LIMPIA LA PROPIEDAD TEXT DE LOS CONTROLES DEL FORMULARIO. N.
        private void LimpiarControlesForm()
        {
            this.dtpFecha.Value = DateTime.Today;
            this.txtCodigo.Text = "";
            this.txtProducto.Text = "";
            this.cboMotivo.SelectedIndex = -1;
            this.txtCantidad.Text = "";
        }
        #endregion

        #region Método que carga los ToolTips

        private void CargarToolTips()
        {
            toolTip1.SetToolTip(this.btnAgregar, "Agregar");
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
            if (dgvMovStock.Rows.Count != 0 && dgvMovStock.Rows.Count > this.indexFila)
            {
                // Devuelvo el foco a la fila de la grilla desde donde se llamó
                this.dgvMovStock.CurrentCell = dgvMovStock[2, this.indexFila];

                // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                EventArgs ea = new EventArgs();
                this.dgvMovStock_SelectionChanged(this.dgvMovStock, ea);
            }
            
        }

        #endregion

        #region Método que carga la grilla

        private void CargarGrilla(string Buscar, string Campo)
        {
            // Variable qie almacena la cadena SQL
            string myCadenaSQL = "";
            // Si no estoy buscando
            if (Buscar == "")
            {
                // Armo la cadena SQL
                myCadenaSQL = "select * from Vista_StockMovimientos order by Fecha";
            }
            else
            {
                // Cadena SQL 
                myCadenaSQL = "select * from Vista_StockMovimientos where " + Campo + " like '" + Buscar + "%' order by Fecha";
            }
            
            // Ejecuto la consulta y paso los datos a una tabla
            DataTable myTabla = clsDataBD.GetSql(myCadenaSQL);
            // No dejo que la grilla genere de manera automática las columnas
            dgvMovStock.AutoGenerateColumns = false;
            // Asigno el source a la grilla
            dgvMovStock.DataSource = myTabla;

        }

        #endregion

        #region Método que carga el Combo del formulario

        private void CargarCombos()
        {
            // Cargo el combo de los Motivos que se pueden mostrar
            clsDataBD.CargarCombo(cboMotivo, "StockMotivos", "StockMotivo", "IdStockMotivo", "Show = 1");
            // Dejo vacía la selección del combo
            cboMotivo.SelectedIndex = -1;
        }

        #endregion

        #region Método que recarga el formulario

        private void RecargarFormulario()
        {
            // Cambio el estado del formulario a agregar. G.
            this.myEstado = "C";
            // Cambio el estado de la bandera de búesqueda
            BanderaBuscar = false;
            // Cambio el estado de la bandera de la grilla
            BanderaGrilla = true;
            // Cargo la grilla
            CargarGrilla(Tipo, "Tabla");
            // Limpio los controles del formulario. G.    
            ActivarBotones();
            // Habilito los controles para este estado. G.
            HabilitarControles();
            // Si tengo filas en la grilla
            if (dgvMovStock.Rows.Count > 0)
            {
                // Posiciono el foco en la fila desde donde se llamo
                PosicionarFocoFila();
            }
        }

        #endregion

        #region Método que trae los datos de la grilla a los controles

        private void TraerDatosGrilla()
        {
            if (BanderaBuscar == false)
            {
                // Paso los datos de la fila actual a una variable
                DataGridViewRow row = dgvMovStock.CurrentRow;
                // Paso los datos de la variable a los campos del formulario
                dtpFecha.Value = Convert.ToDateTime(row.Cells["Fecha"].Value);
                txtCodigo.Text = row.Cells["CodigoArticulo"].Value.ToString();
                txtProducto.Text = row.Cells["Articulo"].Value.ToString();
                cboMotivo.SelectedValue = Convert.ToInt32(row.Cells["IdStockMotivo"].Value);
                txtCantidad.Text = row.Cells["Cantidad"].Value.ToString();
            }
        }

        #endregion

        #region Método que deshabilita el reordenamiento de las grilla desde sus cabeceras

        private void DeshabilitarOrdenGrillas()
        {
            // Deshabilito la reordenación de las columnas de las grillas
            foreach (DataGridViewColumn dgvCol in dgvMovStock.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        #endregion

        #region Método que controla el botón Eliminar

        private void OpcionEliminarVisible()
        {
            // Si el movimiento es interno, no se puede eliminar
            if (Convert.ToInt32(dgvMovStock.CurrentRow.Cells["IdStockMotivo"].Value) < 7)
            {
                // Botón Borrar
                btnBorrar.TabStop = false;
                btnBorrar.Visible = false;
                // Etiquetas
                lblMotivo.Visible = false;
                lblCantidad.Visible = false;
                // Controles
                cboMotivo.Visible = false;
                txtCantidad.Visible = false;
            }
            else
            {
                // Botón Borrar
                btnBorrar.TabStop = true;
                btnBorrar.Visible = true;
                // Etiquetas
                lblMotivo.Visible = true;
                lblCantidad.Visible = true;
                // Controles
                cboMotivo.Visible = true;
                txtCantidad.Visible = true;
            }
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

        #endregion

    }
}

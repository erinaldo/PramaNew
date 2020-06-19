using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Prama.Clases;
using Prama.Formularios.Auxiliares;

namespace Prama.Formularios.Ventas
{
    public partial class frmRemitos : Form
    {

        #region VARIABLES

        //Estado y Cargado
        string myEstado = "C";
        //para guardar IdRemito y Cliente
        int IdRemito = 0;
        int IdRemitoSearch = 0;
        int IdCli = 0;
        //Objetos de Clase
        clsCLientes myCliente = new clsCLientes();
        clsRemitos myRemito = new clsRemitos();
        //IndexFila
        int indexFila = 0;
        bool bSearch = false;

        #endregion

        #region Constructor

        public frmRemitos()
        {
            InitializeComponent();
        }

        #endregion

        #region Metodo Load

        private void frmRemitos_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 
			
            // Cargo los toolstip de los botones
            CargarToolsTip();
           
            //Condicion Compra
            clsDataBD.CargarCombo(this.cboPagoFlete, "CondicionesCompra", "CondicionCompra", "IdCondicionCompra");
            cboPagoFlete.SelectedIndex = -1;

            //Condicion Compra Mercaderia
            clsDataBD.CargarCombo(this.cboMercaderia, "CondicionesCompra", "CondicionCompra", "IdCondicionCompra");
            cboMercaderia.SelectedValue = -1;

            //Transporte
            clsDataBD.CargarCombo(this.cboTransporte, "Transportes", "RazonSocial", "IdTransporte");
            cboTransporte.SelectedIndex = -1;
           
            //Cargar la Grilla
            this.CargarGrilla("", "");

            //Ordenamiento
            DeshabilitarOrdenGrillas();

            //Habilitar COntroles
            this.HabilitarControles();
            //Activar Botones
            this.ActivarBotones();

            // Cuento la cantidad de filas de la grilla
            int filas = dgvRemitos.Rows.Count;
            //Foco
            dgvRemitos.Focus();
            // Actualizo el valor de la fila para que sea la última de la grilla
            this.indexFila = filas - 1;
            // Pongo el foco de la fila
            PosicionarFocoFila();
            //Titulo
            this.Text = clsGlobales.cParametro.NombreFantasia + this.Text;
        }

        #endregion

        #region Método que deshabilita el reordenamiento de las grilla desde sus cabeceras

        private void DeshabilitarOrdenGrillas()
        {
            // Deshabilito la reordenación de las columnas de las grillas
            foreach (DataGridViewColumn dgvCol in this.dgvRemitos.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;

            }
        }

        #endregion

        #region Metodo: CargarGrilla

        // Método que carga la grilla del formulario
        private void CargarGrilla(string Buscar, string Campo)
        {
            // Variable para la cadena SQL
            string myCadena = "";
            if (Buscar == "")
            {
                // Cadena SQL
                myCadena = "SELECT * FROM Vista_Remitos WHERE Activo = 1 order by IdRemito";
            }
            else
            {
               //Cadena SQL 
                myCadena = "select * from Vista_Remitos where " + Campo + " like '" + Buscar + "%' and Activo = 1  order by IdRemito";
               //.T. Modo 'B'
               bSearch = true;
            }

            // Armo la cadena SQL

            // Evito que el dgvUsuarios genere columnas automáticas
            dgvRemitos.AutoGenerateColumns = false;
            // Creo un nuevo DataTable
            DataTable mDtTable = new DataTable();
            // Le asigno al nuevo DataTable los datos de la consulta SQL
            mDtTable = clsDataBD.GetSql(myCadena);
            // Asigno el source de la grilla
            dgvRemitos.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvRemitos.Rows.Count;
            // Si hay filas
            if (Filas > 0)
            {                
                //Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                EventArgs ea = new EventArgs();
                this.dgvRemitos_SelectionChanged(this.dgvRemitos, ea);

            }
        }

        #endregion

        #region Método que devuelve el foco a la fila desde donde se presionó algún botón

        private void PosicionarFocoFila()
        {
            if (dgvRemitos.Rows.Count != 0 && dgvRemitos.Rows.Count > this.indexFila)
            {
                // Devuelvo el foco a la fila de la grilla desde donde se llamó
                if (this.indexFila == -1)
                {
                    this.indexFila = 0;
                }
                this.dgvRemitos.CurrentCell = dgvRemitos[1, this.indexFila];
                // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                EventArgs ea = new EventArgs();
                this.dgvRemitos_SelectionChanged(this.dgvRemitos, ea);
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
            toolTip5.SetToolTip(this.btnModificar, "Editar");
            toolTip6.SetToolTip(this.btnImprimirRotulo, "Imprimir Rótulos");
            toolTip7.SetToolTip(this.btnBuscar, "Buscar");
            toolTip8.SetToolTip(this.btnImprimir, "Imprimir Remito");
            toolTip10.SetToolTip(this.btnCli, "Buscar Cliente");
            toolTip11.SetToolTip(this.btnTransporte, "Transporte");
            toolTip12.SetToolTip(this.btnComprobantes, "Comprobantes emitidos!");

        }

        #endregion

        #region Metodo de la Grilla SelectionChanged

        private void dgvRemitos_SelectionChanged(object sender, EventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvRemitos.RowCount == 0)
            {
                return;
            }

            // Traigo los datos de la grilla
            this.TraerDatosGrilla();
        }

        #endregion

        #region Metodo: TraerDatosGrilla()

        //************************************************************
        //Metodo    : TraerDatosGrilla 
        //Fecha     : 12/03/2017
        //Autor     : N
        //Proposito : Mostrar los datos de la grilla en los controles
        //************************************************************
        private void TraerDatosGrilla()
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvRemitos.Rows.Count == 0)
            {
                this.LimpiarControlesForm();
                return;
            }
            else
            {
                //TRAER LA FILA ACTUAL. N.
                DataGridViewRow row = dgvRemitos.CurrentRow;

                this.IdRemito = Convert.ToInt32(row.Cells["IdCodRemito"].Value.ToString());
                this.IdCli = Convert.ToInt32(row.Cells["IdCliente"].Value.ToString());

                cboTransporte.SelectedValue = Convert.ToInt32(row.Cells["IdTransporte"].Value.ToString());
                cboPagoFlete.SelectedValue = Convert.ToInt32(row.Cells["IdFormaPago"].Value.ToString());
                cboMercaderia.SelectedValue = Convert.ToInt32(row.Cells["IdCondicionCompraMerc"].Value.ToString());

                this.txtCuit.Text = row.Cells["CUIT"].Value.ToString();

                this.txtRazonSocial.Text = row.Cells["RazonSocial"].Value.ToString();
                this.txtDir.Text = row.Cells["Direccion"].Value.ToString() + ", " + row.Cells["Localidad"].Value.ToString() + " (" + row.Cells["CP"].Value.ToString() + ") - " + row.Cells["Provincia"].Value.ToString();
                this.txtCantBultos.Text = row.Cells["Bultos"].Value.ToString();
                this.txtValSeg.Text = row.Cells["ValorSeguro"].Value.ToString();

                this.txtPunto.Text = Convert.ToInt32(row.Cells["Punto"].Value.ToString()).ToString("D4");
                this.txtNro.Text = Convert.ToInt32(row.Cells["Nro"].Value.ToString()).ToString("D8");

                this.txtNroComp.Text = clsGlobales.Left(row.Cells["Comprobante"].Value.ToString(), 4);
                this.txtComp.Text = clsGlobales.Right(row.Cells["Comprobante"].Value.ToString(), 8);
                
                //Anulado
                chkAnulado.Checked = false;
                if (Convert.ToInt32(row.Cells["Anulado"].Value) == 1)
                {
                    chkAnulado.Checked = true;
                }

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
                    this.btnAgregar.Enabled = false;
                    this.btnModificar.TabStop = false;
                    this.btnModificar.Visible = false;
                    this.btnModificar.Enabled = true;
                    this.btnBuscar.TabStop = false;
                    this.btnBuscar.Visible = false;
                    this.btnBuscar.Enabled = false;
                    this.btnAceptar.TabStop = true;
                    this.btnAceptar.Visible = true;
                    this.btnAceptar.Enabled = true;
                    this.btnCancelar.TabStop = true;
                    this.btnCancelar.Visible = true;
                    this.btnCancelar.Enabled = true;
                    this.btnSalir.TabStop = false;
                    this.btnSalir.Visible = false;
                    this.btnSalir.Enabled = false;
                    this.btnImprimir.TabStop = false;
                    this.btnImprimir.Visible = false;
                    this.btnImprimir.Enabled = false;
                    this.btnImprimirRotulo.TabStop = false;
                    this.btnImprimirRotulo.Visible = false;
                    this.btnImprimirRotulo.Enabled = false;
                    break;

                case "B":
                    this.btnAgregar.TabStop = false;
                    this.btnAgregar.Visible = false;
                    this.btnAgregar.Enabled = false;
                    this.btnModificar.TabStop = false;
                    this.btnModificar.Visible = false;
                    this.btnModificar.Enabled = true;
                    this.btnBuscar.TabStop = false;
                    this.btnBuscar.Visible = false;
                    this.btnBuscar.Enabled = false;
                    this.btnAceptar.TabStop = true;
                    this.btnAceptar.Visible = true;
                    this.btnAceptar.Enabled = true;
                    this.btnCancelar.TabStop = true;
                    this.btnCancelar.Visible = true;
                    this.btnCancelar.Enabled = true;
                    this.btnSalir.TabStop = false;
                    this.btnSalir.Visible = false;
                    this.btnSalir.Enabled = false;
                    this.btnImprimir.TabStop = false;
                    this.btnImprimir.Visible = false;
                    this.btnImprimir.Enabled = false;
                    this.btnImprimirRotulo.TabStop = false;
                    this.btnImprimirRotulo.Visible = false;
                    this.btnImprimirRotulo.Enabled = false;
                    break;

                case "C":
                    this.btnAgregar.TabStop = true;
                    this.btnAgregar.Enabled = true;
                    this.btnAgregar.Visible = true;

                    this.btnModificar.TabStop = true && (dgvRemitos.RowCount != 0);
                    this.btnModificar.Enabled = true && (dgvRemitos.RowCount != 0);
                    this.btnModificar.Visible = true && (dgvRemitos.RowCount != 0);

                    this.btnBuscar.TabStop = true && (dgvRemitos.RowCount != 0);
                    this.btnBuscar.Enabled = true && (dgvRemitos.RowCount != 0);
                    this.btnBuscar.Visible = true && (dgvRemitos.RowCount != 0);

                    this.btnAceptar.TabStop = false;
                    this.btnAceptar.Enabled = false;
                    this.btnAceptar.Visible = false;

                    this.btnCancelar.TabStop = false;
                    this.btnCancelar.Enabled = false;
                    this.btnCancelar.Visible = false;

                    this.btnSalir.TabStop = true;
                    this.btnSalir.Enabled = true;
                    this.btnSalir.Visible = true;
                     
                    //Si hay registros... verificar si esta anulado.
                     if (dgvRemitos.RowCount != 0)
                     {
                         DataGridViewRow row = dgvRemitos.CurrentRow;
                         if (Convert.ToInt32(row.Cells["Anulado"].Value) != 1)
                         {
                             this.btnImprimirRotulo.TabStop = true;
                             this.btnImprimirRotulo.Enabled = true;
                             this.btnImprimirRotulo.Visible = true;
                             this.btnImprimir.TabStop = true; 
                             this.btnImprimir.Enabled = true;
                             this.btnImprimir.Visible = true;

                             //Move 
                             this.btnImprimirRotulo.Location = new System.Drawing.Point(214, 9);
                         }
                         else
                         {
                             this.btnImprimirRotulo.TabStop = true;
                             this.btnImprimirRotulo.Enabled = true;
                             this.btnImprimirRotulo.Visible = true;
                             this.btnImprimir.TabStop = false;
                             this.btnImprimir.Enabled = false;
                             this.btnImprimir.Visible = false;

                             //Move 
                             this.btnImprimirRotulo.Location = new System.Drawing.Point(163, 9);
                         }
                     }
                     else
                     {
                       this.btnImprimirRotulo.TabStop = true;
                       this.btnImprimirRotulo.Enabled = true;
                       this.btnImprimirRotulo.Visible = true;
                       this.btnImprimir.TabStop = false;
                       this.btnImprimir.Enabled = false;
                       this.btnImprimir.Visible = false;
                       
                       //Move 
                       this.btnImprimirRotulo.Location = new System.Drawing.Point(61, 9);

                     }

                    return;
            }
        }

        #endregion

        #region Método para limpiar los controles del formulario
        //LIMPIA LA PROPIEDAD TEXT DE LOS CONTROLES DEL FORMULARIO. N.
        private void LimpiarControlesForm()
        {
            //Remito
            foreach (Control c in this.grpRemito.Controls)
            {

                //Textbox  
                if (c is TextBox) { c.Text = ""; }

                //CheckBox
                if (c is CheckBox)
                {
                    CheckBox cb = (CheckBox)c;
                    cb.Checked = false;
                }

                //ComboBox       
                if (c is ComboBox)
                {
                    ComboBox cb = (ComboBox)c;
                    if (cb != null)
                    {
                        cb.SelectedIndex = -1;
                    }
                }

                //DateTimePicker
                if (c is DateTimePicker)
                {
                    DateTimePicker dtp = (DateTimePicker)c;
                    if (dtp != null)
                    {
                        dtp.Text = "1950/01/01";
                    }
                }
            }

            //Cliente
            foreach (Control c in this.grpCliente.Controls)
            {

                //Textbox  
                if (c is TextBox) { c.Text = ""; }

                //CheckBox
                if (c is CheckBox)
                {
                    CheckBox cb = (CheckBox)c;
                    cb.Checked = false;
                }

                //ComboBox       
                if (c is ComboBox)
                {
                    ComboBox cb = (ComboBox)c;
                    if (cb != null)
                    {
                        cb.SelectedIndex = -1;
                    }
                }

                //DateTimePicker
                if (c is DateTimePicker)
                {
                    DateTimePicker dtp = (DateTimePicker)c;
                    if (dtp != null)
                    {
                        dtp.Text = "1950/01/01";
                    }
                }
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
                    this.txtCuit.TabStop = false;
                    this.txtCuit.Enabled = false;

                    this.txtPunto.TabStop = false;
                    this.txtPunto.Enabled = false;

                    this.txtNro.TabStop = false;
                    this.txtNro.Enabled = false;

                    this.cboTransporte.TabStop = true;
                    this.cboTransporte.Enabled = true;

                    this.txtNroComp.TabStop = false;
                    this.txtNroComp.Enabled = false;

                    this.txtComp.TabStop = false;
                    this.txtComp.Enabled = false;

                    this.cboPagoFlete.TabStop = true;
                    this.cboPagoFlete.Enabled = true;

                    this.cboMercaderia.TabStop = true;
                    this.cboMercaderia.Enabled = true;

                    this.txtCantBultos.TabStop = true;
                    this.txtCantBultos.Enabled = true;

                    this.txtValSeg.TabStop = true;
                    this.txtValSeg.Enabled = true;

                    this.btnCli.TabStop = true;
                    this.btnCli.Enabled = true;

                    this.btnEditCli.TabStop = false;
                    this.btnEditCli.Enabled = false;

                    this.btnTransporte.TabStop = true;
                    this.btnTransporte.Enabled = true;

                    this.btnComprobantes.TabStop = true;
                    this.btnComprobantes.Enabled = true;

                    this.chkAnulado.TabStop = false;
                    this.chkAnulado.Enabled = false;

                    this.gpbBusquedas.Visible = false;

                    this.dgvRemitos.TabStop = false;
                    this.dgvRemitos.Enabled=false;
                    
                    break;

                case "M":
                    
                    this.txtCuit.TabStop = false;
                    this.txtCuit.Enabled = false;

                    this.txtPunto.TabStop = false;
                    this.txtPunto.Enabled = false;

                    this.txtNro.TabStop = false;
                    this.txtNro.Enabled = false;

                    this.txtNroComp.TabStop = false;
                    this.txtNroComp.Enabled = false;

                    this.txtComp.TabStop = false;
                    this.txtComp.Enabled = false;
                    
                    this.cboTransporte.TabStop = false;
                    this.cboTransporte.Enabled = false;

                    this.cboPagoFlete.TabStop = false;
                    this.cboPagoFlete.Enabled = false;

                    this.cboMercaderia.TabStop = false;
                    this.cboMercaderia.Enabled = false;

                    this.txtCantBultos.TabStop = false;
                    this.txtCantBultos.Enabled = false;

                    this.txtValSeg.TabStop = false;
                    this.txtValSeg.Enabled = false;

                    this.btnCli.TabStop = false;
                    this.btnCli.Enabled = false;

                    this.btnEditCli.TabStop = false;
                    this.btnEditCli.Enabled = false;

                    this.btnTransporte.TabStop = false;
                    this.btnTransporte.Enabled = false;

                    this.btnComprobantes.TabStop = false;
                    this.btnComprobantes.Enabled = false;

                    this.chkAnulado.TabStop = true;
                    this.chkAnulado.Enabled = true;

                    this.gpbBusquedas.Visible = false;

                    this.dgvRemitos.Enabled=false;

                    break;

                case "B":

                    this.txtCuit.TabStop = false;
                    this.txtCuit.Enabled = false;
 
                    this.txtPunto.TabStop = false;
                    this.txtPunto.Enabled = false;

                    this.txtNro.TabStop = false;
                    this.txtNro.Enabled = false;

                    this.txtNroComp.TabStop = false;
                    this.txtNroComp.Enabled = false;

                    this.txtComp.TabStop = false;
                    this.txtComp.Enabled = false;

                    this.cboTransporte.TabStop = false;
                    this.cboTransporte.Enabled = false;

                    this.cboPagoFlete.TabStop = false;
                    this.cboPagoFlete.Enabled = false;

                    this.cboMercaderia.TabStop = false;
                    this.cboMercaderia.Enabled = false;

                    this.btnCli.TabStop = false;
                    this.btnCli.Enabled = false;

                    this.btnEditCli.TabStop = false;
                    this.btnEditCli.Enabled = false;

                    this.btnTransporte.TabStop = false;
                    this.btnTransporte.Enabled = false;

                    this.btnComprobantes.TabStop = false;
                    this.btnComprobantes.Enabled = false;

                    this.txtCantBultos.TabStop = false;
                    this.txtCantBultos.Enabled = false;

                    this.txtValSeg.TabStop = false;
                    this.txtValSeg.Enabled = false;

                    this.dgvRemitos.TabStop = true && (dgvRemitos.RowCount > 0);
                    this.dgvRemitos.Enabled = true && (dgvRemitos.RowCount > 0);

                    this.chkAnulado.TabStop = false;
                    this.chkAnulado.Enabled = false;

                    this.gpbBusquedas.Visible = true;

                    break;

                case "C":

                    this.txtCuit.TabStop = false;
                    this.txtCuit.Enabled = false;

                    this.cboTransporte.TabStop = false;
                    this.cboTransporte.Enabled = false;

                    this.cboPagoFlete.TabStop = false;
                    this.cboPagoFlete.Enabled = false;

                    this.cboMercaderia.TabStop = false;
                    this.cboMercaderia.Enabled = false;

                    this.txtPunto.TabStop = false;
                    this.txtPunto.Enabled = false;

                    this.txtNro.TabStop = false;
                    this.txtNro.Enabled = false;

                    this.txtNroComp.TabStop = false;
                    this.txtNroComp.Enabled = false;

                    this.txtComp.TabStop = false;
                    this.txtComp.Enabled = false;

                    this.btnCli.TabStop = false;
                    this.btnCli.Enabled = false;

                    this.btnEditCli.TabStop = false;
                    this.btnEditCli.Enabled = false;

                    this.btnTransporte.TabStop = false;
                    this.btnTransporte.Enabled = false;

                    this.btnComprobantes.TabStop = false;
                    this.btnComprobantes.Enabled = false;

                    this.txtCantBultos.TabStop = false;
                    this.txtCantBultos.Enabled = false;

                    this.txtValSeg.TabStop = false;
                    this.txtValSeg.Enabled = false;

                    this.dgvRemitos.TabStop = true && (dgvRemitos.RowCount > 0);
                    this.dgvRemitos.Enabled = true && (dgvRemitos.RowCount > 0);

                    this.chkAnulado.TabStop = false;
                    this.chkAnulado.Enabled = false;

                    this.gpbBusquedas.Visible = false;

                    break;

            }
        }

        #endregion

        #region Método que trae el Cliente para una nueva factura

        private void CargarClienteNuevo(int p_Cliente = 0)
        {
            //Vino un numero de cliente mayor a 0 en el parametro?....
            if (p_Cliente == 0)
            {
                // Si el vector tiene más de un Cliente seleccionado
                if (clsGlobales.ClientesSeleccionados.GetLength(0) > 1)
                {
                    // Informo que solo se puede seleccionar un proveedor
                    MessageBox.Show("Solo puede seleccionar un Cliente!", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Vuelvo a abrir el formulario de búsqueda de proveedores
                    // LLamo al formulario que busca los Clientes
                    frmBuscarCliente myForm = new frmBuscarCliente();
                    myForm.ShowDialog();
                }
            }
            else
            {
                // Redimensiono el tamaño de la matriz
                clsGlobales.ClientesSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ClientesSeleccionados, 1);
                // A la posición creada le asigno el Id seleccionado
                clsGlobales.ClientesSeleccionados[0] = p_Cliente;
            }

            //**
            // Si hay algún Cliente seleccionado
            if (clsGlobales.ClientesSeleccionados.GetLength(0) > 0)
            {
                // Recorro el vector hasta que encuentro un Id de Cliente y lo paso a los controles del formulario
                for (int i = 0; i <= clsGlobales.ClientesSeleccionados.GetLength(0); i++)
                {
                    // Si la posición tiene un ID de Cliente, busco los datos del mismo
                    if (clsGlobales.ClientesSeleccionados[0] > 0)
                    {
                        // Cargo los datos del Cliente
                        CargarClientes(clsGlobales.ClientesSeleccionados[0]);
                        // Los paso al formulario
                        PasarDatosAlFormulario();
                    }
                }

            }
        }

        #endregion

        #region PasarDatosAlFormulario

        //PASA LOS DATOS DEL CLIENTE AL FORMULARIO
        private void PasarDatosAlFormulario()
        {

            //Paso los datos del proveedor al formulario  
             this.IdCli = this.myCliente.Codigo;
             this.txtCuit.Text = this.myCliente.Cuit;
             this.txtRazonSocial.Text = this.myCliente.RazonSocial;
             this.txtDir.Text = this.myCliente.Direccion + ", " + this.myCliente.Localidad + " (" + this.myCliente.CP + ") - " + this.myCliente.Provincia;
             this.cboTransporte.SelectedValue = this.myCliente.IdTransporte;
             this.cboMercaderia.SelectedValue = this.myCliente.IdCondicionCompra;
             this.cboPagoFlete.SelectedValue = this.myCliente.IdCondicionCompra;
             //Flete FormaPago
             cboPagoFlete.SelectedValue = 1;
        }

        #endregion

        #region Metodo EliminarClienteSeleccionado

        //Eliminar el cliente selecionado anteriormente
        private void EliminarClienteSeleccionado()
        {
            // Recorro el vector
            for (int i = 0; i < clsGlobales.ClientesSeleccionados.GetLength(0); i++)
            {
                // Si el Cliente que quiero borrar está en el vector
                if (clsGlobales.ClientesSeleccionados[i] == myCliente.Codigo)
                {
                    // Elimino el proveedor del vector
                    clsGlobales.ClientesSeleccionados[i] = 0;
                    //Limpiar vector Cliente
                    clsGlobales.ClientesSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ClientesSeleccionados, 0);
                    // Salgo del for
                    break;
                }
            }

            // Si ya no queda Cliente seleccionado, vaciar datos.
            if (clsGlobales.ClientesSeleccionados.GetLength(0) == 0)
            {
                this.HabilitarControles();
            /*    this.txtCuit.Text = "";
                this.txtRazonSocial.Text = "";
                this.txtDir.Text = "";*/
            }
        }

        #endregion

        #region Método que carga los datos de los Clientes a la clase

        private void CargarClientes(int Id)
        {
            // Armo la cadena SQL
            string myCadenaSQL = "select * from Vista_Clientes where Codigo = " + Id;
            // Creo una tabla que me va a almacenar el resultado de la consulta
            DataTable myTablaClientes = clsDataBD.GetSql(myCadenaSQL);
            // Recorro la tabla y le asigo los datos del proveedor al objeto
            foreach (DataRow rowCli in myTablaClientes.Rows)
            {
                myCliente.Codigo = Convert.ToInt32(rowCli["Codigo"]);

                myCliente.RazonSocial = rowCli["RazonSocial"].ToString();
                myCliente.Cuit = rowCli["Cuit"].ToString();
                myCliente.Direccion = rowCli["Direccion"].ToString();
                myCliente.IdCondicionIva = Convert.ToInt32(rowCli["IdTipo"].ToString());
                myCliente.CP = rowCli["CP"].ToString();

                myCliente.IdTransporte = Convert.ToInt32(rowCli["IdTransporte"].ToString());
                myCliente.IdCondicionCompra = Convert.ToInt32(rowCli["IdCondicionCompra"].ToString());

                if (!(string.IsNullOrEmpty(rowCli["Barrio"].ToString())))
                {
                    myCliente.Barrio = rowCli["Barrio"].ToString();
                }
                else { myCliente.Barrio = ""; }

                myCliente.IdLocalidad = Convert.ToInt32(rowCli["IdLocalidad"].ToString());
                myCliente.Localidad = rowCli["Localidad"].ToString();

                myCliente.IdProvincia = Convert.ToInt32(rowCli["IdProvincia"].ToString());
                myCliente.Provincia = rowCli["Provincia"].ToString();

                myCliente.Telefono = rowCli["Telefono"].ToString();


                if (!(string.IsNullOrEmpty(rowCli["Celular"].ToString())))
                {
                    myCliente.Celular = rowCli["Celular"].ToString();
                }
                else { myCliente.Celular = ""; }


                if (!(string.IsNullOrEmpty(rowCli["Fax"].ToString())))
                {
                    myCliente.Fax = rowCli["Fax"].ToString();
                }
                else { myCliente.Fax = ""; }


                if (!(string.IsNullOrEmpty(rowCli["Mail"].ToString())))
                {
                    myCliente.Mail = rowCli["Mail"].ToString();
                }
                else { myCliente.Mail = ""; }


                if (!(string.IsNullOrEmpty(rowCli["Web"].ToString())))
                {
                    myCliente.Web = rowCli["Web"].ToString();
                }
                else { myCliente.Web = ""; }


                myCliente.IdTipoCliente = Convert.ToInt32(rowCli["IdTipo"].ToString());

                myCliente.IdCondicionIva = Convert.ToInt32(rowCli["IdCondicionIva"].ToString());

                if (!(string.IsNullOrEmpty(rowCli["Observaciones"].ToString())))
                {
                    myCliente.Observaciones = rowCli["Observaciones"].ToString();
                }
                else { myCliente.Observaciones = ""; }


                if (clsGlobales.cValida.EsFecha(rowCli["Nacimiento"].ToString()))
                {
                    myCliente.Nacimiento = rowCli["Nacimiento"].ToString();
                }
                else { myCliente.Nacimiento = ""; }


                if (clsGlobales.cValida.EsFecha(rowCli["Alta"].ToString()))
                {
                    myCliente.Alta = rowCli["Alta"].ToString();
                }
                else { myCliente.Alta = ""; }


            }
        }

        #endregion

        #region Botones

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //Eliminar
            this.EliminarClienteSeleccionado();
            //Cerrar
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            string myCadena = "";

            if (this.myEstado == "B")
            {
                //Tomar el Id
                IdRemitoSearch = Convert.ToInt32(this.dgvRemitos.CurrentRow.Cells["IdCodRemito"].Value.ToString());
                // Cambio mi estado
                this.myEstado = "C";
                // Lleno nuevamente la grilla
                this.CargarGrilla("","");
                // Activo todos los botones
                ActivarBotones();
                // Habilito los controles
                HabilitarControles();
                //Id >0? Solo cuando busca reposiciona por ID
                if (!(IdRemitoSearch == 0 && bSearch))
                {
                    ReposicionarById();
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
            }

            //VALIDAR DATOS DEL REMITO
            if (!(ValidarRemito()))
            {
                //Error? salir...
                return;
            }

            //Cargar Datos al Objeto            
            myRemito.IdCliente = IdCli;
            myRemito.IdFormaPago = Convert.ToInt32(cboPagoFlete.SelectedValue);
            myRemito.IdFormaPagoMerc = Convert.ToInt32(cboMercaderia.SelectedValue);

            if (this.myEstado == "A")
            {
                myRemito.IdRemito = clsDataBD.RetornarUltimoId("Remitos", "IdRemito") + 1;
                myRemito.NroRemito = (clsDataBD.getUltComp("Ult_Remito", clsGlobales.cParametro.PtoVtaPorDefecto, 0) + 1).ToString("D8");
                myRemito.Nro = (clsDataBD.getUltComp("Ult_Remito", clsGlobales.cParametro.PtoVtaPorDefecto, 0) + 1);
            }
            else
            {
                myRemito.IdRemito = IdRemito;
                myRemito.Nro = Convert.ToInt32(txtNro.Text);

            }

            myRemito.IdTransporte = Convert.ToInt32(cboTransporte.SelectedValue);
            myRemito.Punto = Convert.ToInt32(txtPunto.Text);
            myRemito.Seguro = Convert.ToDouble(txtValSeg.Text);
            myRemito.Bultos = Convert.ToInt32(txtCantBultos.Text);
            myRemito.CUIT = txtCuit.Text;
            myRemito.Activo = 1;

            //SI ESTA VACIO SE COMPLETA CON 0...
            if (string.IsNullOrEmpty(txtComp.Text))
            {
                txtComp.Text = "0";
            }

            myRemito.Comprobante = Convert.ToInt32(txtNroComp.Text).ToString("D4") + "-" + Convert.ToInt32(txtComp.Text).ToString("D8");            
            
            //Anulado?
            if (this.chkAnulado.Checked == true) { myRemito.Anulado = 1; } else { myRemito.Anulado = 0; }

            //Nuevo o Editar
            if (this.myEstado == "A")
            {

                //Actualizar el numero de presupuesto en Tabla AFIP
                string mySQL = "UPDATE PuntosVentaAFIP SET Ult_Remito = " + myRemito.Nro + " WHERE Punto = " + clsGlobales.cParametro.PtoVtaPorDefecto;
                clsDataBD.GetSql(mySQL);

                //Tomar el Id
                IdRemitoSearch = myRemito.IdRemito;

                //Cargar el producto en la tabla
                myCadena = "INSERT INTO Remitos (IdRemito," +
                                                            " Punto, " +
                                                            " Nro," +
                                                            " NroRemito," +
                                                            " IdCliente," +
                                                            " CUIT," +
                                                            " IdTransporte," +
                                                            " Comprobante," +
                                                            " IdFormaPago," +
                                                            " IdFormaPagoMerc," +
                                                            " Bultos," +
                                                            " Seguro," + 
                                                            " Anulado," + 
                                                            " Activo)" +
                                                            " values (" + myRemito.IdRemito + "," +
                                                                           myRemito.Punto + "," +
                                                                           myRemito.Nro + ",'" +
                                                                           myRemito.NroRemito + "'," +
                                                                           myRemito.IdCliente + ",'" +
                                                                           myRemito.CUIT + "'," +
                                                                           myRemito.IdTransporte + ",'" +
                                                                           myRemito.Comprobante + "'," +
                                                                           myRemito.IdFormaPago + "," +
                                                                           myRemito.IdFormaPagoMerc + "," +
                                                                           myRemito.Bultos + "," +
                                                                           myRemito.Seguro.ToString().Replace(",", ".") + "," +
                                                                           myRemito.Anulado + "," + 
                                                                           myRemito.Activo + ")";
            }

            if (this.myEstado == "M")
            {
                    myCadena = "UPDATE Remitos SET IdRemito =  " + myRemito.IdRemito + "," +
                                                                " Punto = " + myRemito.Punto + "," +
                                                                " Nro = " + myRemito.Nro + "," +
                                                                " NroRemito = '" + myRemito.NroRemito + "'," +
                                                                " IdCliente = " + myRemito.IdCliente + "," +
                                                                " CUIT ='" + myRemito.CUIT + "'," +
                                                                " IdTransporte = " + myRemito.IdTransporte + "," +
                                                                " Comprobante = '" + myRemito.Comprobante + "'," +
                                                                " IdFormaPago = " + myRemito.IdFormaPago + "," +
                                                                " IdFormaPagoMerc = " + myRemito.IdFormaPagoMerc + "," +
                                                                " Bultos = " + myRemito.Bultos + "," +
                                                                " Seguro = " + myRemito.Seguro.ToString().Replace(",", ".") + "," +
                                                                " Anulado = " + myRemito.Anulado + "," +
                                                                " Activo = " + myRemito.Activo +
                                                                " WHERE IdRemito = " + myRemito.IdRemito;

            }

            //Estado
            this.myEstado = "C";
            //Ejecuto la consulta SQL
            clsDataBD.GetSql(myCadena);
            //Lleno nuevamente la grilla
            CargarGrilla("", "");
            //Botones
            this.ActivarBotones();
            //Controles
            this.HabilitarControles();
            //Posiciono el foco en la fila desde donde se llamó
            PosicionarFocoFila();
            //Volver por ID
            if (!(IdRemitoSearch == 0))
            {
                ReposicionarById();
            }

        }

        #region Reposicionar Grilal por Id

        //REPOSICIONA GRILLA POR ID
        private void ReposicionarById()
        {
            //recorrer
            foreach (DataGridViewRow myRow in this.dgvRemitos.Rows)
            {
                if (Convert.ToInt32(myRow.Cells["IdCodRemito"].Value.ToString()) == IdRemitoSearch)
                {
                    // Devuelvo el foco a la fila de la grilla desde donde se llamó
                    this.dgvRemitos.CurrentCell = dgvRemitos[1, myRow.Index];

                    // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                    EventArgs ea = new EventArgs();
                    this.dgvRemitos_SelectionChanged(this.dgvRemitos, ea);

                    //Salir
                    break;
                }
            }
        }

        #endregion

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            int myPtoRemito = 3; 

            DialogResult dlResult = MessageBox.Show("¿Desea confeccionar un Remito Manual?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Si confirma... cambiar estado
            if (dlResult == DialogResult.Yes)
            {
                //Llamar al formulario manual
                frmRemitoManual myRemito = new frmRemitoManual();
                myRemito.ShowDialog();

                //Finalizar
                return;
            }

            // Tomo la posición actual de la fila con foco
            if (!(dgvRemitos.Rows.Count == 0))
            {
                this.indexFila = dgvRemitos.CurrentRow.Index;
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
                //Punto
                this.txtPunto.Text = myPtoRemito.ToString("D4");
                this.txtNroComp.Text = clsGlobales.cParametro.PtoVtaPorDefecto.ToString("D4");
                txtNro.Text = (clsDataBD.getUltComp("Ult_Remito", clsGlobales.cParametro.PtoVtaPorDefecto, 0) + 1).ToString("D8");
                // Posiciono el foco sobre el primer textbox
                txtNro.Focus();
            }
            // Pregunto si el usuario actual tiene nivel menor o igual a 2, No lo dejo agregar. G.
            else
            {
                // vuelvo el formulario al estado anterior. G.
                this.myEstado = "C";
                // El usuario no es de nivel 3, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para generar un nueve Remito", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            // Tomo la posición actual de la fila con foco
            if (!(dgvRemitos.Rows.Count == 0))
            {
                this.indexFila = dgvRemitos.CurrentRow.Index;
            }

            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvRemitos.CurrentRow;
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvRemitos.RowCount == 0)
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
                // Posiciono el foco sobre el primer textbox
                txtNro.Focus();
            }
            else
            {
                // Pregunto si el usuario actual tiene nivel menor o igual a 2, No lo dejo agregar. G.
                this.myEstado = "C";
                // El usuario no es de nivel 3, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para modificar un Remito", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //// Cambio el estado del formulario a agregar. G.
            //this.myEstado = "C";
            ////Limpiar Controles
            //this.LimpiarControlesForm();
            //// Cargo las localidades
            //this.CargarGrilla("", "");
            //// Botones
            //this.ActivarBotones();
            //// Habilito los controles para este estado. G.
            //this.HabilitarControles();
            ////Foco
            //PosicionarFocoFila();
            //// Para evitar que los controles queden vacíos cuando cancelo, 
            //// Llamo al evento SelectionChanged de la grilla. G.
            //this.dgvRemitos_SelectionChanged(sender, e);


            //Recargar
            if (this.myEstado == "B" && bSearch)
            {
                //Limpiar Controles
                this.LimpiarControlesForm();
                // Cargo las localidades
                this.CargarGrilla("", "");
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Tomo la posición actual de la fila con foco
            if (!(dgvRemitos.Rows.Count == 0))
            {
                this.indexFila = dgvRemitos.CurrentRow.Index;
            }

            //Preparo todo para habilitar la busqueda
            this.myEstado = "B";
            //Botones
            this.ActivarBotones();
            //Controles
            this.HabilitarControles();

            //Clean Contrls
            this.txtBuscarRM.Text = "";
            this.txtBuscarCUIT.Text = "";
            this.txtBuscarRM.Focus();
        }

        private void btnCli_Click(object sender, EventArgs e)
        {
            //Quitar el cliente actualmente selecionado
            EliminarClienteSeleccionado();
            //Buscar Cliente
            frmBuscarCliente myForm = new frmBuscarCliente();
            myForm.ShowDialog();
            //Cliente Nuevo
            this.CargarClienteNuevo();
            //Retorna
            if (clsGlobales.ClientesSeleccionados.GetLength(0) > 0)
            {
                //Inhabilitar Boton
                this.btnEditCli.Enabled = true;
                //this.btnCli.Enabled = false;
            }
        }

        #endregion

        #region Eventos KeyPress

        private void txtNro_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Solo enteros
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }         
        }

        private void txtComp_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Solo enteros
            /*if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            } */        
        }

        private void txtCantBultos_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Solo enteros
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }         
        }

        private void txtValSeg_KeyPress(object sender, KeyPressEventArgs e)
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

        #endregion

        #region Eventos TextChanged

        private void txtBuscarRM_TextChanged(object sender, EventArgs e)
        {
            if (!(txtBuscarRM.Text == ""))
            {
                CargarGrilla(this.txtBuscarRM.Text, "NroRemito");
            }
        }

        private void txtBuscarCUIT_TextChanged(object sender, EventArgs e)
        {
            if (!(txtBuscarCUIT.Text == ""))
            {
                CargarGrilla(this.txtBuscarCUIT.Text, "CUIT");
            }
        }

        #endregion

        #region Metodo AltaRemito

        private void AltaRemito()        
        { 
                
        }

        #endregion

        #region EditaRemito

        private void EditaRemito()
        { 
            
        }

        #endregion

        #region Metodo ValidarRemito

        //VALIDAR REMITO
        private bool ValidarRemito()
        {
            bool bValida = true;

            //Validar Nro Remito
            if (string.IsNullOrEmpty(txtNro.Text))
            {
                MessageBox.Show("Debe completar el N° de Remito!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bValida = false;
                txtNro.Focus();
                return bValida;
            }
            else if (Convert.ToInt32(txtNro.Text) == 0)
            {
                MessageBox.Show("El N° de Remito no puede ser 0!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bValida = false;
                txtNro.Focus();
                return bValida;
            }

            //Validar Datos Cliente
            if (string.IsNullOrEmpty(txtCuit.Text))
            {
               MessageBox.Show("Debe seleccionar un Cliente!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
               bValida = false;
               btnCli.Focus();
               return bValida;
            }

            //Razon Social Cliente
            if (string.IsNullOrEmpty(txtRazonSocial.Text))
            {
                MessageBox.Show("Debe seleccionar un Cliente!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bValida = false;
                btnCli.Focus();
                return bValida;
            }

            //Direccion Cliente
            if (string.IsNullOrEmpty(txtDir.Text))
            {
                MessageBox.Show("Debe seleccionar un Cliente!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bValida = false;
                btnCli.Focus();
                return bValida;
            }

            //Transporte
            if (cboTransporte.Text == "")
            {
                MessageBox.Show("Debe seleccionar el Transporte!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bValida = false;
                cboTransporte.Focus();
                return bValida;
            }

            //Pago Mercaderia
            if (cboMercaderia.Text == "")
            {
                MessageBox.Show("Debe seleccionar la Forma de Pago de la Mercadería!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bValida = false;
                cboMercaderia.Focus();
                return bValida;
            }

            //txtNroComp y txtComp
     /*       if (string.IsNullOrEmpty(txtComp.Text))
            {
                MessageBox.Show("Debe completar el N° de Comprobante!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bValida = false;
                txtComp.Focus();
                return bValida;
            }
            else if (Convert.ToInt32(txtComp.Text) == 0)
            {
                MessageBox.Show("El N° de Comprobante no puede ser 0!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bValida = false;
                txtComp.Focus();
                return bValida;
            }*/

            //Pago Flete
            if (cboPagoFlete.Text == "")
            {
                MessageBox.Show("Debe seleccionar la Forma de Pago del Flete!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bValida = false;
                cboPagoFlete.Focus();
                return bValida;
            }

            //Cantidad de Bultos
            if (txtCantBultos.Text == "")
            {
                MessageBox.Show("Debe completar la Cantidad de Bultos!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bValida = false;
                txtCantBultos.Focus();
                return bValida;
            }

            //Valor Seguro <> ""
            if (txtValSeg.Text == "")
            {
                MessageBox.Show("Debe completar el Valor del Seguro!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bValida = false;
                txtValSeg.Focus();
                return bValida;
            }

            //Valor Seguro == 0 ?
            if (Convert.ToDouble(txtValSeg.Text) == 0)
            {
                MessageBox.Show("El Valor del Seguro debe ser mayor que 0!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bValida = false;
                txtValSeg.Focus();
                return bValida;
            }

            //Retornar Valor
            return bValida;
        }

        #endregion

        private void btnImprimir_Click(object sender, EventArgs e)
        {

            //PREGUNTAR SI ESTA CONFIGURADO EN PARAMETROS
            if (clsGlobales.cParametro.Imprimir)
            {
                DialogResult dlResult = MessageBox.Show("¿Desea imprimir el Remito N° " + txtPunto.Text + "-" + txtNro.Text + " ?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Si confirma... cambiar estado
                if (dlResult == DialogResult.No)
                {
                    return;
                }
            }

            //Data Set
            dsReportes oDsRemito = new dsReportes();

            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = dgvRemitos.Rows.Count;

            //Indice de Fila Actual
            int i = dgvRemitos.CurrentRow.Index;

           /* for (int i = 0; i < dgvFilas; i++)
            {*/
                oDsRemito.Tables["dtRemito"].Rows.Add
                (new object[] { dgvRemitos[6,i].Value.ToString(),
                    dgvRemitos[15,i].Value.ToString(),
                    dgvRemitos[16,i].Value.ToString() + " (" + dgvRemitos[17,i].Value.ToString() + ")",
                    dgvRemitos[9,i].Value.ToString(),
                    dgvRemitos[21,i].Value.ToString(),
                    dgvRemitos[11,i].Value.ToString(),
                    dgvRemitos[12,i].Value.ToString(),
                    dgvRemitos[13,i].Value.ToString(),
                    DateTime.Now.ToString("dd/MM/yyyy"),
                    dgvRemitos[4,i].Value.ToString(),
                    dgvRemitos[19,i].Value.ToString(),
                    dgvRemitos[8,i].Value.ToString()});

         /*   }*/

            //Objeto Reporte
            rptRemito oRepRemito = new rptRemito();

            //Cargar Reporte                                    
            oRepRemito.Load(Application.StartupPath + "\\rptRemito.rpt");
            
            //Establecer el DataSet como DataSource
            oRepRemito.SetDataSource(oDsRemito);

            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepRemito;

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports(2);
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog(); 


        }

        private void btnImprimirRotulo_Click(object sender, EventArgs e)
        {

            //No hay remitos, rotulador en blanco
            if (this.dgvRemitos.Rows.Count == 0)
            {
                //Confirmar si quiere imprimir en blanco
                DialogResult dlResult = MessageBox.Show("No hay ningún Remito cargado! ¿Desea rotular en forma manual?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //Si es asi, llamar al rotulador en blanco
                if (dlResult == DialogResult.Yes)
                {
                    frmRotuloSnRemito myForm = new frmRotuloSnRemito(); //Estan en auxiliares el frm
                    myForm.Show();
                }

            }
            else
            {
                //Confirmar si quiere imprimir en blanco
                DialogResult dlResult = MessageBox.Show("¿Desea rotular en forma manual?. Si elige < No >, se procederá con el remito seleccionado.", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //Si es asi, llamar al rotulador en blanco
                if (dlResult == DialogResult.Yes)
                {
                    frmRotuloSnRemito myForm = new frmRotuloSnRemito(); //Estan en auxiliares el frm
                    myForm.Show();
                }
                else
                {
                    dsReportes oDsRemito = new dsReportes();

                    //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
                    int Hasta = Convert.ToInt32(txtCantBultos.Text);

                    DataGridViewRow row = dgvRemitos.CurrentRow;

                    for (int i = 1; i <= Hasta; i++)
                    {
                        oDsRemito.Tables["dtRotulo"].Rows.Add
                        (new object[] { i,
                                    Hasta,
                                    row.Cells["RazonSocial"].Value.ToString(),
                                    row.Cells["Direccion"].Value.ToString(),
                                    row.Cells["Localidad"].Value.ToString() + " (" + row.Cells["CP"].Value.ToString() + ")" + " - " + row.Cells["Provincia"].Value.ToString(),
                                    row.Cells["Telefono"].Value.ToString(),
                                    row.Cells["Transporte"].Value.ToString(),
                                    ""});
                    }

                    //Objeto Reporte
                    rptRotulo2 oRepRotulo = new rptRotulo2();

                    //Cargar Reporte                                    
                    oRepRotulo.Load(Application.StartupPath + "\\rptRotulo2.rpt");

                    //Establecer el DataSet como DataSource
                    oRepRotulo.SetDataSource(oDsRemito);

                    //Pasar como parámetro nombre del reporte
                    clsGlobales.myRptDoc = oRepRotulo;

                    //Mostrar el reporte  
                    frmShowReports myReportForm = new frmShowReports();
                    myReportForm.Text = this.Text;
                    myReportForm.ShowDialog();
                }
            }
        }

        private void btnComprobantes_Click(object sender, EventArgs e)
        {

            string[] myVector = new string[5];

            //Llamar a Ventas Comprobantes
            frmVentasComprobantes myForm = new frmVentasComprobantes(0, clsGlobales.cParametro.PtoVtaPorDefecto, myVector);
            myForm.ShowDialog();

            //Null?
            if (!(myVector == null))
            {
                //Nro Comprobante
                txtComp.Text = myVector[0].ToString();
                //Mercaderia Forma Pago
                cboMercaderia.SelectedValue = Convert.ToInt32(myVector[1].ToString());
                //Flete FormaPago
                cboPagoFlete.SelectedValue = Convert.ToInt32(myVector[1].ToString());
                //Transporte
                cboTransporte.SelectedValue = Convert.ToInt32(myVector[3].ToString());
                //Cliente Nuevo
                this.CargarClienteNuevo(Convert.ToInt32(myVector[2].ToString())); 
                //Valor Seguro
                this.txtValSeg.Text = Convert.ToDouble(myVector[4].ToString()).ToString("#0.00");
                //Flete FormaPago
                cboPagoFlete.SelectedValue = 1;
                //Foco
                txtCantBultos.Focus();

            }
        }

        private void btnTransporte_Click(object sender, EventArgs e)
        {
            //Llamar al Formulario de Transporte
            frmTransporte myForm = new frmTransporte(txtIdTransporte);
            myForm.ShowDialog();
            
            //Asginar
            if (!(string.IsNullOrEmpty(txtIdTransporte.Text)))
            {
                cboTransporte.SelectedValue = txtIdTransporte.Text;
            }
        }

        private void txtCantBultos_Click(object sender, EventArgs e)
        {
            txtCantBultos.SelectionStart = 0;
            txtCantBultos.SelectionLength = txtCantBultos.Text.Length;
        }

        private void txtCantBultos_Enter(object sender, EventArgs e)
        {
            txtCantBultos.SelectionStart = 0;
            txtCantBultos.SelectionLength = txtCantBultos.Text.Length;
        }

        private void txtValSeg_Enter(object sender, EventArgs e)
        {
            txtValSeg.SelectionStart = 0;
            txtValSeg.SelectionLength = txtValSeg.Text.Length;
        }

        private void txtValSeg_Click(object sender, EventArgs e)
        {
            txtValSeg.SelectionStart = 0;
            txtValSeg.SelectionLength = txtValSeg.Text.Length;
        }

        private void btnEditCli_Click(object sender, EventArgs e)
        {
            //hay cliente seleccionado
            if (string.IsNullOrEmpty(txtCuit.Text))
            {
                MessageBox.Show("No hay ningún Cliente seleccionado!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Lamar al formulario de clientes con el cliente seleccionado
            frmClientesABM frmModiCli = new frmClientesABM(myCliente.Codigo);
            frmModiCli.ShowDialog();
            // Cargo los datos del proveedor
            CargarClientes(this.myCliente.Codigo);
            // Los paso al formulario
            PasarDatosAlFormulario();
            //Inhabilitar Boton
            //this.btnCli.Enabled = false;
        }
    }    
    
}

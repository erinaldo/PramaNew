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
    public partial class frmGastos : Form
    {
     
     // Instancio un objeto de la calse proveedores para pasarle los datos que me devuleve la consulta
        clsProveedores myProveedor = new clsProveedores();

        public frmGastos()
        {
            InitializeComponent();
        }

        #region Metodo: LOAD del Formulario

        private void frmGastos_Load(object sender, EventArgs e)
        {
			
			//icon
				clsFormato.SetIconForm(this); 	
				
             //Cargar Combos
                this.CargarCombos();

             //Punto de compra / venta y Almacen
                this.cboPunto.SelectedValue = Convert.ToInt32(clsGlobales.cParametro.PtoVtaPorDefecto);
                this.cboAlmacen.SelectedValue = Convert.ToInt32(clsGlobales.cParametro.AlmacenPorDefecto);

             // Pongo en su correspondiente textbox al comprador (Usuario logueado)
                txtComprador.Text = clsGlobales.UsuarioLogueado.Usuario;

            //Clean
                setLimpiarControlesForm(); 

            //Cargar Grilla
                getCargarGrilla();

            //Botones
                this.ActivarBotones();          

            //Controles
                this.HabilitarControles();

            //Cargar Tooltips
                this.CargarToolTips();

            //Titulo Ventana
                this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;        
        }

        #endregion

        #region Método para cargar la grilla

        private void getCargarGrilla()
        {
            // Cadena SQL 
            string myCadena = "select * from GastosFijos";
            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            mDtTable = clsDataBD.GetSql(myCadena);
            // Evito que el dgv genere columnas automáticas
            dgvGastos.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvGastos.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvGastos.Rows.Count;
            // Posiciono la grilla en la última fila, si hay
            if (Filas > 0)
            {
                //Posicionamiento grilla
                int r = dgvGastos.CurrentCell.RowIndex;
                int c = dgvGastos.CurrentCell.ColumnIndex;
                dgvGastos.CurrentCell = dgvGastos.Rows[r].Cells[c];
             //Mostrar datos  
                getMostrarDatos();
            }
        }

        #endregion

        #region Metodo: getMostrarDatos()

        //************************************************************
        //Metodo    : getMostrarDatos 
        //Fecha     : 22-09-2016
        //Autor     : N
        //Proposito : Mostrar los datos de la grilla en los controles
        //************************************************************
        private void getMostrarDatos()
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvGastos.Rows.Count == 0)
            {
                this.setLimpiarControlesForm();
                return;
            }
            else
            {
              //TRAER LA FILA ACTUAL. N.
                DataGridViewRow row = dgvGastos.CurrentRow;

                this.cboAlmacen.SelectedValue = Convert.ToInt32(row.Cells["IdAlmacen"].Value.ToString());
                this.cboPunto.SelectedValue = Convert.ToInt32(row.Cells["IdPunto"].Value.ToString());
                this.txtComprador.Text = row.Cells["Comprador"].Value.ToString();

                this.dtFecha.Value = Convert.ToDateTime(row.Cells["Fecha"].Value.ToString());
                this.txtCodigo.Text = row.Cells["Codigo"].Value.ToString();

                this.txtDescrip.Text = row.Cells["Descripcion"].Value.ToString();
                this.cboTipoGasto.SelectedValue = Convert.ToInt32(row.Cells["Tipo_Gasto"].Value.ToString());

                this.nudAño.Value = Convert.ToInt32(row.Cells["Año"].Value.ToString());
                this.nudPeriodo.Value = Convert.ToInt32(row.Cells["Periodo"].Value.ToString());

                this.txtPto.Text = row.Cells["Punto"].Value.ToString();
                this.txtNro.Text = row.Cells["Numero"].Value.ToString();

                this.txtImporte.Text = row.Cells["Importe"].Value.ToString();
                this.txtDtoIVA.Text = row.Cells["DtoIVA"].Value.ToString();
                this.txtUnidades.Text = row.Cells["Unidades"].Value.ToString();

                this.txtCosto.Text = row.Cells["Costo"].Value.ToString();

                this.cboCondCompra.SelectedValue = Convert.ToInt32(row.Cells["IdCondicionCompra"].Value.ToString());

                // Cargo los datos del proveedor
                CargarProveedores(Convert.ToInt32(row.Cells["IdProv"].Value.ToString()));
                // Los paso al formulario
                PasarDatosAlFormulario();
      
            }
        }

        #endregion

        #region Metodo: CargarGrillaBusqueda

        private void CargarGrillaBusqueda(string Buscar, string Campo)
        {
            // Variable para la cadena SQL
            string myCadena = "";
            if (Buscar == "")
            {
                // Cadena SQL 
                myCadena = "select * from GastosFijos";
            }
            else
            {
                // Cadena SQL 
                myCadena = "select * from GastosFijos where " + Campo + " like '" + Buscar + "%' order by " + Campo;
            }

            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            mDtTable = clsDataBD.GetSql(myCadena);
            // Evito que el dgv genere columnas automáticas
            dgvGastos.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvGastos.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvGastos.Rows.Count;
            // Posiciono la grilla en la última fila, si hay
            if (Filas > 0)
            {
                //Posicionamiento grilla
                int r = dgvGastos.CurrentCell.RowIndex;
                int c = dgvGastos.CurrentCell.ColumnIndex;
                dgvGastos.CurrentCell = dgvGastos.Rows[r].Cells[c];
                //Mostrar datos  
                  getMostrarDatos();
            }

        }

        #endregion

        #region Método para limpiar los campos de Búsqueda
        // Limpia los controles de búsqueda del form
        private void LimpiarControlesBusqueda()
        {
            this.txtBuscarGd.Text = "";
            this.txtBuscarGc.Text = "";
        }

        #endregion

        #region Método para limpiar los controles del formulario
        //LIMPIA LA PROPIEDAD TEXT DE LOS CONTROLES DEL FORMULARIO. N.
        private void setLimpiarControlesForm()
        {

            //Proveedores
            foreach (Control c in gpbProveedores.Controls)
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

            //Gastos
            foreach (Control c in this.grpDetGasto.Controls)
            {
                //NumericUpDown
                if (c is NumericUpDown)
                {
                    NumericUpDown cn = (NumericUpDown)c;
                    if (cn.Name == "nudPeriodo") { cn.Value = 1; }
                    if (cn.Name == "nudAño") { cn.Value = DateTime.Now.Year; }
                }
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

        #region Método para activar los botones del formulario
        //--------------------------------------------------------------
        //ACTIVAR BOTONES  
        //SEGUN EL ESTADO (A, M, C, B) - MUESTRA U OCULTA BOTONES
        //--------------------------------------------------------------
        private void ActivarBotones()
        {
            switch (clsGlobales.myEstado)
            {
                case "A":
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
                    this.btnImprimir.TabStop = false;
                    this.btnImprimir.Visible = false;
                    this.btnBorrar.TabStop = false;
                    this.btnBorrar.Visible = false;
                    this.btnSearchProvider.TabStop = true;
                    this.btnSearchProvider.Enabled = true;
                    break;

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
                    this.btnImprimir.TabStop = false;
                    this.btnImprimir.Visible = false;
                    this.btnBorrar.TabStop = false;
                    this.btnBorrar.Visible = false;
                    this.btnSearchProvider.TabStop = false;
                    this.btnSearchProvider.Enabled = false;
                    break;

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
                    this.btnImprimir.TabStop = false;
                    this.btnImprimir.Visible = false;
                    this.btnBorrar.TabStop = false;
                    this.btnBorrar.Visible = false;
                    this.btnSearchProvider.TabStop = false;
                    this.btnSearchProvider.Enabled = false;

                    break;
                case "C":
                    this.btnAgregar.TabStop = true;
                    this.btnAgregar.Visible = true;
                    this.btnModificar.TabStop = true && (dgvGastos.Rows.Count != 0);
                    this.btnModificar.Visible = true && (dgvGastos.Rows.Count != 0);
                    this.btnBuscar.TabStop = true && (dgvGastos.Rows.Count != 0);
                    this.btnBuscar.Visible = true && (dgvGastos.Rows.Count != 0);
                    this.btnAceptar.TabStop = false;
                    this.btnAceptar.Visible = false;
                    this.btnCancelar.TabStop = false;
                    this.btnCancelar.Visible = false;
                    this.btnBorrar.TabStop = true && (dgvGastos.Rows.Count != 0);
                    this.btnBorrar.Visible = true && (dgvGastos.Rows.Count != 0);
                    this.btnSalir.TabStop = true;
                    this.btnSalir.Visible = true;
                    this.btnSearchProvider.TabStop = false;
                    this.btnSearchProvider.Enabled = false;

                    this.btnImprimir.TabStop = true && (dgvGastos.Rows.Count != 0);
                    this.btnImprimir.Visible = true && (dgvGastos.Rows.Count != 0);

                    break;
            }
        }
        #endregion

        #region Metodo: cCalcularImporteFinal

        private void cCalcularImporteFinal()
        { 
                        //Si los campos estan completos
            if (string.IsNullOrEmpty(txtImporte.Text)==false  && string.IsNullOrEmpty(txtDtoIVA.Text)==false && string.IsNullOrEmpty(this.txtUnidades.Text)==false)
            {
                double dImporteBase = 0;
                double dCostoFinal = 0;

                dImporteBase = Convert.ToDouble(txtImporte.Text) - Convert.ToDouble(txtDtoIVA.Text);
                
                if (Convert.ToDouble(txtUnidades.Text)==0)
                {
                    dCostoFinal = dImporteBase;
                }
                else
                {
                    dCostoFinal = dImporteBase / Convert.ToDouble(this.txtUnidades.Text);
                }

                this.txtCosto.Text=dCostoFinal.ToString("#0.00000");

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
            switch (clsGlobales.myEstado)
            {
                case "A":
                    dgvGastos.TabStop = false;
                    dgvGastos.Enabled = false;

                    this.gpbProveedores.Enabled = true;
                    this.grpDetGasto.Enabled = true;

                    this.txtCodigoProv.TabStop = false;
                    this.txtCodigoProv.Enabled = false;
                    this.txtRSoc.TabStop = false;
                    this.txtRSoc.Enabled = false;
                    this.txtCUIT.TabStop = false;
                    this.txtCUIT.Enabled = false;

                    cboPunto.TabStop = true;
                    cboPunto.Enabled = true;
                    cboAlmacen.TabStop = true;
                    cboAlmacen.Enabled = true;
                    cboCondCompra.TabStop = true;
                    cboCondCompra.Enabled = true;

                    this.txtCodigo.TabStop = true;
                    this.txtCodigo.Enabled = true;
                    this.cboTipoGasto.TabStop = true;
                    this.cboTipoGasto.Enabled = true;
                    this.dtFecha.TabStop = true;
                    this.dtFecha.Enabled = true;
                    this.txtDescrip.TabStop = true;
                    this.txtDescrip.Enabled = true;
                    this.cboTipoGasto.TabStop = true;
                    this.cboTipoGasto.Enabled = true;
                    this.nudPeriodo.TabStop = true;
                    this.nudPeriodo.Enabled = true;
                    this.nudAño.TabStop = true;
                    this.nudAño.Enabled = true;
                    this.txtPto.TabStop = true;
                    this.txtPto.Enabled = true;
                    this.txtNro.TabStop = true;
                    this.txtNro.Enabled = true;
                    this.txtImporte.TabStop = true;
                    this.txtImporte.Enabled = true;
                    this.txtDtoIVA.TabStop = true;
                    this.txtDtoIVA.Enabled = true;
                    this.txtUnidades.TabStop = true;
                    this.txtUnidades.Enabled = true;
                    this.txtCosto.TabStop = false;
                    this.txtCosto.Enabled = false;

                    this.grpDetGasto.Height = 149;
                    this.gpbBusquedas.Visible = false;

                    break;

                case "M":
                    dgvGastos.TabStop = false;
                    dgvGastos.Enabled = false;

                    this.gpbProveedores.Enabled = true;
                    this.grpDetGasto.Enabled = true;

                    this.txtCodigoProv.TabStop = false;
                    this.txtCodigoProv.Enabled = false;
                    this.txtRSoc.TabStop = false;
                    this.txtRSoc.Enabled = false;
                    this.txtCUIT.TabStop = false;
                    this.txtCUIT.Enabled = false;

                    cboPunto.TabStop = true;
                    cboPunto.Enabled = true;
                    cboAlmacen.TabStop = true;
                    cboAlmacen.Enabled = true;
                    cboCondCompra.TabStop = true;
                    cboCondCompra.Enabled = true;

                    txtCodigo.TabStop = false;
                    txtCodigo.Enabled = false;
                    this.txtCosto.TabStop = false;
                    this.txtCosto.Enabled = false;
                    cboTipoGasto.TabStop = false;
                    cboTipoGasto.Enabled = false;

                    this.dtFecha.TabStop = true;
                    this.dtFecha.Enabled = true;
                    this.txtDescrip.TabStop = true;
                    this.txtDescrip.Enabled = true;                    
                    this.nudPeriodo.TabStop = false;
                    this.nudPeriodo.Enabled = false;
                    this.nudAño.TabStop = false;
                    this.nudAño.Enabled = false;
                    this.txtPto.TabStop = true;
                    this.txtPto.Enabled = true;
                    this.txtNro.TabStop = true;
                    this.txtNro.Enabled = true;
                    this.txtImporte.TabStop = true;
                    this.txtImporte.Enabled = true;
                    this.txtDtoIVA.TabStop = true;
                    this.txtDtoIVA.Enabled = true;
                    this.txtUnidades.TabStop = true;
                    this.txtUnidades.Enabled = true;
                    this.grpDetGasto.Height = 149;
                    this.gpbBusquedas.Visible = false;

                    break;
                case "B":

                    this.gpbProveedores.Enabled = false;
                    this.grpDetGasto.Enabled = false;

                    this.dgvGastos.TabStop = true && (dgvGastos.Rows.Count > 0);
                    this.dgvGastos.Enabled = true && (dgvGastos.Rows.Count > 0);

                    this.grpDetGasto.Height = 65;
                    this.gpbBusquedas.Visible = true;

                    break;
                case "C":

                    this.gpbProveedores.Enabled = false;
                    this.grpDetGasto.Enabled = false;

                    this.dgvGastos.TabStop = true && (dgvGastos.Rows.Count > 0);
                    this.dgvGastos.Enabled = true && (dgvGastos.Rows.Count > 0);

                    this.grpDetGasto.Height = 110;
                    this.gpbBusquedas.Visible = false;

                    break;
            }
        }
        #endregion

        #region Método que carga los combos del formulario

        private void CargarCombos()
        {
            // Cargo el combo de los puntos de venta
            clsDataBD.CargarCombo(cboPunto, "PuntosVentas", "PuntoVenta", "IdPuntoVenta", "Activo = 1");
            // Cargo el combo de los almacenes
            clsDataBD.CargarCombo(cboAlmacen, "Almacenes", "Almacen", "IdAlmacen", "Activo = 1");
            // Cargo el combo de las condiciones de compra
            clsDataBD.CargarCombo(cboCondCompra, "CondicionesCompra", "CondicionCompra", "IdCondicionCompra", "Activo = 1");
            // Cargo el combo de Tipo de Gasto
            clsDataBD.CargarCombo(this.cboTipoGasto, "TiposGastos", "TipoGasto", "IdTipoGasto");

        }

        #endregion

        #region Método que vacía los vectores globales para nuevo uso

        private void VaciarVectoresGlobales()
        {
            // Vacío de datos el vector de los proveedores
            clsGlobales.ProveedoresSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ProveedoresSeleccionados, 0);
        }

        #endregion

        #region Método que trae el proveedor para un nuevo Gasto

        private void CargarProveedorNuevo()
        {
            // Si el vector tiene ,ás de un proveedor seleccionado
            if (clsGlobales.ProveedoresSeleccionados.GetLength(0) > 1)
            {
                // Informo que solo se puede seleccionar un proveedor
                MessageBox.Show("Solo puede seleccionar un Proveedor!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Vuelvo a abrir el formulario de búsqueda de proveedores
                // LLamo al formulario que busca los proveedores
                frmProveedoresBuscar myForm = new frmProveedoresBuscar(true, true);
                // Lo muestro
                myForm.ShowDialog();
            }
            // Si hay algún proveedor seleccionado
            if (clsGlobales.ProveedoresSeleccionados.GetLength(0) > 0)
            {
                // Recorro el vector hasta que encuentro un Id de proveedor y lo paso a los controles del formulario
                for (int i = 0; i <= clsGlobales.ProveedoresSeleccionados.GetLength(0); i++)
                {
                    // Si la posición tiene un ID de proveedor, busco los datos del mismo
                    if (clsGlobales.ProveedoresSeleccionados[0] > 0)
                    {
                        // Cargo los datos del proveedor
                        CargarProveedores(clsGlobales.ProveedoresSeleccionados[0]);
                        // Los paso al formulario
                        PasarDatosAlFormulario();
                    }
                }
            }

        }

        #endregion

        #region Metodo: PasarDatosAlFormulario

        private void PasarDatosAlFormulario()
        {
            // Paso los datos del proveedor al formulario
            this.txtCodigoProv.Text = myProveedor.IdProveedor.ToString();
            this.txtRSoc.Text = myProveedor.RazonSocial;
            this.txtCUIT.Text = myProveedor.CUIT;
            this.txtCondicionIva.Text = myProveedor.CondicionIva;
            this.txtTel.Text = myProveedor.Telefono;
            this.txtFax.Text = myProveedor.Fax;
            this.txtDir.Text = myProveedor.Direccion;
            this.txtCp.Text = myProveedor.Cp;
            this.txtLocalidad.Text = myProveedor.Localidad;
            this.txtProvincia.Text = myProveedor.Provincia;

            cboCondCompra.SelectedValue = myProveedor.IdCondicionCompra;

        }

        #endregion

        #region Método que carga los datos de los proveedores a la clase

        private void CargarProveedores(int Id)
        {
            // Armo la cadena SQL
            string myCadenaSQL = "select * from Vista_Proveedores where IdProveedor = " + Id;
            // Creo una tabla que me va a almacenar el resultado de la consulta
            DataTable myTablaProveedores = clsDataBD.GetSql(myCadenaSQL);
            // Recorro la tabla y le asigo los datos del proveedor al objeto
            foreach (DataRow rowProv in myTablaProveedores.Rows)
            {
                // Primero los Int
                myProveedor.IdProveedor = Convert.ToInt32(rowProv["IdProveedor"]);
                myProveedor.IdCondicionIva = Convert.ToInt32(rowProv["IdCondicionIva"]);
                myProveedor.IdCondicionCompra = Convert.ToInt32(rowProv["IdCondicionCompra"]);
                myProveedor.IdProvincia = Convert.ToInt32(rowProv["IdProvincia"]);
                myProveedor.IdLocalidad = Convert.ToInt32(rowProv["IdLocalidad"]);
                myProveedor.ProvIns = Convert.ToInt32(rowProv["Insumos"]);
                myProveedor.ProvProd = Convert.ToInt32(rowProv["Productos"]);

                // Los string
                myProveedor.NombreFantasia = rowProv["NombreFantasia"].ToString();
                myProveedor.RazonSocial = rowProv["RazonSocial"].ToString();
                myProveedor.CondicionIva = rowProv["CondicionIva"].ToString();
                myProveedor.CUIT = rowProv["CUIT"].ToString();
                myProveedor.IngresosBrutos = rowProv["IngresosBrutos"].ToString();
                myProveedor.FechaInicioActividad = rowProv["FechaInicioActividad"].ToString();
                myProveedor.Direccion = rowProv["Direccion"].ToString();
                myProveedor.Localidad = rowProv["Localidad"].ToString();
                myProveedor.Provincia = rowProv["Provincia"].ToString();
                myProveedor.Cp = rowProv["CP"].ToString();
                myProveedor.Telefono = rowProv["Telefono"].ToString();
                myProveedor.Fax = rowProv["Fax"].ToString();
                myProveedor.Celular = rowProv["Celular"].ToString();
                myProveedor.MailEmpresa = rowProv["MailEmpresa"].ToString();
                myProveedor.Web = rowProv["Web"].ToString();
                myProveedor.Contacto = rowProv["Contacto"].ToString();
                myProveedor.MailContacto = rowProv["MailContacto"].ToString();
                myProveedor.CelularContacto = rowProv["CelularContacto"].ToString();
                myProveedor.Observaciones = rowProv["Observaciones"].ToString();
            }
        }

        #endregion

        #region CargarToolTips

        private void CargarToolTips()
        {
            toolTip1.SetToolTip(this.btnAgregar, "Agregar");
            toolTip2.SetToolTip(this.btnModificar, "Modificar");
            toolTip3.SetToolTip(this.btnImprimir, "Imprimir");
            toolTip4.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip5.SetToolTip(this.btnCancelar, "Cancelar");
            toolTip6.SetToolTip(this.btnSalir, "Salir");
            toolTip7.SetToolTip(this.btnBuscar, "Buscar");
            toolTip8.SetToolTip(this.btnSearchProvider, "Buscar Proveedor");
        }

        #endregion

        #region Eventos de Botones

        #region btnBorrar: Click

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            string CadSQL = "";

            //IdGastoFijo esta en uso? No puede eliminar si esta siendo utilizado en una composiciòn.
            CadSQL = "Select count(*) as nElementos from ProductosGastosFijos Where IdGastoFijo = " + Convert.ToInt32(dgvGastos.CurrentRow.Cells["IdGastoFijo"].Value.ToString());
            if (!(clsGlobales.cValida.ExisteElemento(CadSQL) == 0))
            {
                MessageBox.Show("No puede eliminar el Gasto '" + dgvGastos.CurrentRow.Cells["Codigo"].Value.ToString() +
                                "' porque se encuentra referenciado en otras tablas del Sistema", "Información!", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            //Eliminar Gasto sino esta referenciado en composicion.
            CadSQL = "DELETE FROM GastosFijos WHERE IdGastoFijo = "
                            + Convert.ToInt32(dgvGastos.CurrentRow.Cells["IdGastoFijo"].Value);

            DialogResult dialogResult = MessageBox.Show("¿Desea Eliminar el Gasto '"
                                        + dgvGastos.CurrentRow.Cells["Codigo"].Value.ToString()
                                        + "' ? ", "Confirmar!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                clsDataBD.GetSql(CadSQL);
                
            }

            // Cargo las localidades
            CargarGrillaBusqueda("", "");
            //Limpiar Controles
            this.setLimpiarControlesForm();
            // Botones
            this.ActivarBotones();
            // Habilito los controles para este estado. G.
            this.HabilitarControles();
            // Para evitar que los controles queden vacíos cuando cancelo, 
            // Llamo al evento SelectionChanged de la grilla. G.
            this.dgvGastos_SelectionChanged(sender, e); 


        }

        #endregion

        #region btnAceptar: Click

        private void btnAceptar_Click(object sender, EventArgs e)
        {

          //Busqueda Activa?...
            if (clsGlobales.myEstado == "B")
            {
                // Cambio mi estado
                clsGlobales.myEstado = "C";
                // Activo todos los botones
                ActivarBotones();
                // Habilito los controles
                HabilitarControles();
                //Boton Cancelar Visible
                this.btnCancelar.Visible = true;
                //Retornar
                return;
            }

          //Validar complete datos Proveedor
            if (clsGlobales.myEstado == "A")
            {
                if (clsGlobales.ProveedoresSeleccionados.GetLength(0) == 0)
                {
                    MessageBox.Show("Debe cargar los datos del Proveedor!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            clsGastos myGasto = new clsGastos();

            myGasto.Codigo = this.txtCodigo.Text;
            myGasto.Fecha = this.dtFecha.Value; 
            myGasto.Descrip = this.txtDescrip.Text;

            myGasto.Año = Convert.ToInt32(this.nudAño.Value);
            myGasto.Periodo = Convert.ToInt32(this.nudPeriodo.Value);

            
            myGasto.Punto = this.txtPto.Text;

            if (string.IsNullOrEmpty(txtPto.Text)) 
            { myGasto.Punto = "0"; } else {myGasto.Numero = this.txtNro.Text; }
           
            if (string.IsNullOrEmpty(txtImporte.Text)) 
            { myGasto.Monto = 0; } else {myGasto.Monto = Double.Parse(txtImporte.Text);}

            if (string.IsNullOrEmpty(txtDtoIVA.Text)) 
            { myGasto.DescuentoIVA = 0; } else {myGasto.DescuentoIVA = Double.Parse(txtDtoIVA.Text);}
                         
            if (string.IsNullOrEmpty(txtUnidades.Text)) 
            { myGasto.Unidades = 0; } else {myGasto.Unidades = Double.Parse(txtUnidades.Text);}
            
            if (Convert.ToInt32(cboAlmacen.SelectedValue) == 0) 
            { myGasto.IdAlmacen = 0; } else {myGasto.IdAlmacen = Convert.ToInt32(cboAlmacen.SelectedValue);}

            if (Convert.ToInt32(cboPunto.SelectedValue) == 0) 
            { myGasto.IdPunto = 0; } else {myGasto.IdPunto = Convert.ToInt32(this.cboPunto.SelectedValue);}

            if (Convert.ToInt32(cboTipoGasto.SelectedValue) == 0) 
            { myGasto.IdTipoGasto = 0; } else {myGasto.IdTipoGasto = Convert.ToInt32(cboTipoGasto.SelectedValue);}
            
            if (Convert.ToInt32(cboCondCompra.SelectedValue) == 0) 
            { myGasto.IdCondicionCompra = 0; } else {myGasto.IdCondicionCompra = Convert.ToInt32(cboCondCompra.SelectedValue);}

            myGasto.Comprador = this.txtComprador.Text;

            myGasto.IdUnidadMedida = ObtenerUnidadMedida(myGasto.IdTipoGasto);

            if (string.IsNullOrEmpty(txtUnidades.Text))
            {  myGasto.Costo = 0;   } else { myGasto.Costo = Convert.ToDouble(txtCosto.Text); }

            //IdProveedor
            if (clsGlobales.myEstado == "A")
            {
                myGasto.IdGastoFijo = clsDataBD.RetornarUltimoId("GastosFijos", "IdGastoFijo") + 1;
                myGasto.IdProveedor = this.myProveedor.IdProveedor;
            }
            else
            {
                myGasto.IdProveedor = Convert.ToInt32(dgvGastos.CurrentRow.Cells["IdProv"].Value.ToString());
            }                

            //Vector Errores
            string[] cErrores = myGasto.cValidaGasto();

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

         //Si es alta 
            if (clsGlobales.myEstado == "A")
            {
                this.GuardarNuevoGasto(myGasto);
            }
            else if (clsGlobales.myEstado == "M")
            {
                this.UpdateGasto(myGasto);
            }

            // Cambio mi estado
            clsGlobales.myEstado = "C";
            // Lleno nuevamente la grilla
            getCargarGrilla();            
            // Activo todos los botones
            ActivarBotones();
            // Habilito los controles
            HabilitarControles();
        }

        #endregion

        #region Metodo: GuardarNuevoGasto

        private void GuardarNuevoGasto(clsGastos NuevoGasto)
        {
            string myCadena = "";

            //Cadena SQL 
            myCadena = "INSERT INTO GastosFijos (IdGastoFijo, Fecha, Codigo, IdTipoGasto, Descrip, IdProveedor, IdAlmacen, IdPunto, Comprador, IdUnidadMedida, Periodo, Año, Monto, DescuentoIVA, Unidades, Costo, Punto, Numero, IdCondicionCompra)" +
                                                 " values ("  + NuevoGasto.IdGastoFijo + ",'"
                                                              + NuevoGasto.Fecha + "','"
                                                              + NuevoGasto.Codigo + "',"
                                                              + NuevoGasto.IdTipoGasto + ",'"
                                                              + NuevoGasto.Descrip + "',"
                                                              + NuevoGasto.IdProveedor + ","
                                                              + NuevoGasto.IdAlmacen + ","
                                                              + NuevoGasto.IdPunto + ",'"
                                                              + NuevoGasto.Comprador + "',"
                                                              + NuevoGasto.IdUnidadMedida + ","
                                                              + NuevoGasto.Periodo + ","
                                                              + NuevoGasto.Año + ","
                                                              + NuevoGasto.Monto.ToString().Replace(",", ".") + "," 
                                                              + NuevoGasto.DescuentoIVA.ToString().Replace(",", ".") + "," 
                                                              + NuevoGasto.Unidades.ToString().Replace(",", ".") + ","
                                                              + NuevoGasto.Costo.ToString().Replace(",", ".") + ",'"
                                                              + NuevoGasto.Punto + "','"
                                                              + NuevoGasto.Numero.ToString() + "',"
                                                              + NuevoGasto.IdCondicionCompra + ")";
            //Guardar en Gasto
            clsDataBD.GetSql(myCadena);
        }

        #endregion

        #region Metodo: UpdateGasto

        private void UpdateGasto(clsGastos NuevoGasto)
        {
            string myCadena = "";

            //Cadena SQL 
            myCadena = "UPDATE GastosFijos SET " +
                                                  " Fecha = '" + NuevoGasto.Fecha + "'," +
                                                  " Codigo = '" + NuevoGasto.Codigo + "'," +
                                                  " IdTipoGasto = " + NuevoGasto.IdTipoGasto + "," +
                                                  " Descrip = '" + NuevoGasto.Descrip + "'," +
                                                  " IdProveedor = " + NuevoGasto.IdProveedor + "," +
                                                  " IdAlmacen = " + NuevoGasto.IdAlmacen + "," +
                                                  " IdPunto = " + NuevoGasto.IdPunto + "," +
                                                  " Comprador = '" + NuevoGasto.Comprador + "'," +
                                                  " IdUnidadMedida = " + NuevoGasto.IdUnidadMedida + "," +
                                                  " Periodo = " + NuevoGasto.Periodo + "," +
                                                  " Año = " + NuevoGasto.Año + "," +
                                                  " Monto = " + NuevoGasto.Monto.ToString().Replace(",", ".") + "," +
                                                  " DescuentoIVA = " + NuevoGasto.DescuentoIVA.ToString().Replace(",", ".") + "," +
                                                  " Unidades = " + NuevoGasto.Unidades.ToString().Replace(",", ".") + "," +
                                                  " Costo = " + NuevoGasto.Costo.ToString().Replace(",", ".") + "," +
                                                  " Punto = '" + NuevoGasto.Punto + "'," +
                                                  " Numero = '" + NuevoGasto.Numero + "'," +
                                                  " IdCondicionCompra = " + NuevoGasto.IdCondicionCompra + 
                                                  " WHERE IdGastoFijo = " + NuevoGasto.IdGastoFijo; 

            //Update
            clsDataBD.GetSql(myCadena);
        }

        #endregion

        #region  Metodo para Obtener la Unidad de Medida

        private int ObtenerUnidadMedida(int p_IdTipoGasto)
        {
            int Id_UM = 0;

            string CadSQL = "Select IdUnidadMedida from TiposGastos Where IdTipoGasto = " + p_IdTipoGasto;

            DataTable mDtTable = clsDataBD.GetSql(CadSQL);

            foreach (DataRow row in mDtTable.Rows)
            {
                Id_UM =  Convert.ToInt32(row["IdUnidadMedida"].ToString());
            }


            return Id_UM;
        }

        #endregion

        #region btnModificar: Click

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvGastos.CurrentRow;
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvGastos.Rows.Count == 0)
            {
                // Salgo de la rutina. G.
                return;
            }
            // // Pregunto si el usuario actual tiene nivel mayor a 2, lo dejo agregar. G.
            if (clsGlobales.UsuarioLogueado.Nivel > 2)
            {
                // Cambio el estado del formulario a Modificar. G.
                    clsGlobales.myEstado = "M";
                // Activo los botones para este estado. G.
                    ActivarBotones();
                // Habilito los controles para este estado. G.
                    HabilitarControles();
                // Posiciono el foco sobre el primer textbox
                    txtDescrip.Focus();
            }
            else
            {
                // Pregunto si el usuario actual tiene nivel menor o igual a 2, No lo dejo agregar. G.
                clsGlobales.myEstado = "C";
                // El usuario no es de nivel 3, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para Modificar un Gasto cargado!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region btnBuscar: Click

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Preparo todo para habilitar la busqueda
            clsGlobales.myEstado = "B";
            this.ActivarBotones();
            this.HabilitarControles();
            LimpiarControlesBusqueda();
            this.txtBuscarGc.Focus();
        }

        #endregion

        #region btnAgregar: Click

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //Accion
                clsGlobales.myEstado = "A";
            //Botones
                this.ActivarBotones();
            //Clean
                this.setLimpiarControlesForm();

            //Punto de compra / venta y Almacen
                this.cboPunto.SelectedValue = Convert.ToInt32(clsGlobales.cParametro.PtoVtaPorDefecto);
                this.cboAlmacen.SelectedValue = Convert.ToInt32(clsGlobales.cParametro.AlmacenPorDefecto);

            // Pongo en su correspondiente textbox al comprador (Usuario logueado)
                txtComprador.Text = clsGlobales.UsuarioLogueado.Usuario;

            //Controles
                this.HabilitarControles();
            //Codigo Proveedor Focus
                this.txtCodigoProv.Focus();
        }

        #endregion

        #region btnSalir: Click

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Cierro el formulario. G.
            this.Close();
        }

        #endregion

        #region btnBCancelar: Click

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Cambio el estado del formulario a agregar. G.
            clsGlobales.myEstado = "C";
            // Cargo las localidades
            CargarGrillaBusqueda("", "");
            //Limpiar Controles
            this.setLimpiarControlesForm();    
            // Botones
            this.ActivarBotones();
            // Habilito los controles para este estado. G.
            this.HabilitarControles();
            // Para evitar que los controles queden vacíos cuando cancelo, 
            // Llamo al evento SelectionChanged de la grilla. G.
            this.dgvGastos_SelectionChanged(sender, e);


        }

        #endregion

        #endregion

        #region Eventos Campos de Busqueda

        private void txtBuscarGc_TextChanged(object sender, EventArgs e)
        {
            this.CargarGrillaBusqueda(this.txtBuscarGc.Text, "Codigo");
        }

        private void txtBuscarGd_TextChanged(object sender, EventArgs e)
        {
            this.CargarGrillaBusqueda(this.txtBuscarGd.Text, "Descrip");

        }

        #endregion

        #region Eventos KeyPress 

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 44)
            {
                e.KeyChar = Convert.ToChar(46);
                ch = e.KeyChar;

            }
            //PUNTO DECIMAL. N.
            if (ch == 46 && txtImporte.Text.IndexOf('.') != -1)
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
            if (!char.IsControl(e.KeyChar))
            {
                if (txtImporte.Text.IndexOf('.') > -1 &&
                    txtImporte.Text.Substring(txtImporte.Text.IndexOf('.')).Length >= (2 + 1))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtDtoIVA_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 44)
            {
                e.KeyChar = Convert.ToChar(46);
                ch = e.KeyChar;

            }
            //PUNTO DECIMAL. N.
            if (ch == 46 && txtDtoIVA.Text.IndexOf('.') != -1)
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
            if (!char.IsControl(e.KeyChar))
            {
                if (txtDtoIVA.Text.IndexOf('.') > -1 &&
                    txtDtoIVA.Text.Substring(txtDtoIVA.Text.IndexOf('.')).Length >= (2 + 1))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtUnidades_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 44)
            {
                e.KeyChar = Convert.ToChar(46);
                ch = e.KeyChar;

            }
            //PUNTO DECIMAL. N.
            if (ch == 46 && txtUnidades.Text.IndexOf('.') != -1)
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
            if (!char.IsControl(e.KeyChar))
            {
                if (txtUnidades.Text.IndexOf('.') > -1 &&
                    txtUnidades.Text.Substring(txtUnidades.Text.IndexOf('.')).Length >= (2 + 1))
                {
                    e.Handled = true;
                }
            }
        }

        #endregion
            
        #region Eventos Leave

        private void txtDtoIVA_Leave(object sender, EventArgs e)
        {
            this.cCalcularImporteFinal();
        }

        private void txtUnidades_Leave(object sender, EventArgs e)
        {
            this.cCalcularImporteFinal();
        }

        private void txtCodigo_Leave(object sender, EventArgs e)
        {
            this.txtCodigo.Text = txtCodigo.Text.ToUpper();
        }

        private void txtDescrip_Leave(object sender, EventArgs e)
        {
            this.txtDescrip.Text = txtDescrip.Text.ToUpper();
        }

        private void txtComprador_Leave(object sender, EventArgs e)
        {
            this.txtComprador.Text = this.txtComprador.Text.ToUpper();
        }

        private void txtImporte_Leave(object sender, EventArgs e)
        {
                this.cCalcularImporteFinal();
        }    

        #endregion

        #region Eventos Double Click

        private void txtRSoc_DoubleClick(object sender, EventArgs e)
        {
            // Vacío el vector de los proveedores
            VaciarVectoresGlobales();
            // LLamo al formulario que busca los proveedores
            frmProveedoresBuscar myForm = new frmProveedoresBuscar(true, true);
            // Lo muestro
            myForm.ShowDialog();
            // Cargo los datos del proveedor en el formulario
            CargarProveedorNuevo();
        }

        private void txtCodigoProv_DoubleClick(object sender, EventArgs e)
        {
            // Vacío el vector de los proveedores
            VaciarVectoresGlobales();
            // LLamo al formulario que busca los proveedores
            frmProveedoresBuscar myForm = new frmProveedoresBuscar(true, true);
            // Lo muestro
            myForm.ShowDialog();
            // Cargo los datos del proveedor en el formulario
            CargarProveedorNuevo();
        }

        private void txtCUIT_DoubleClick(object sender, EventArgs e)
        {
            // Vacío el vector de los proveedores
            VaciarVectoresGlobales();
            // LLamo al formulario que busca los proveedores
            frmProveedoresBuscar myForm = new frmProveedoresBuscar(true, true);
            // Lo muestro
            myForm.ShowDialog();
            // Cargo los datos del proveedor en el formulario
            CargarProveedorNuevo();
            // Inhabilito los controles
        }

        #endregion

        #region Eventos de la Grilla

        private void dgvGastos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvGastos.Rows.Count == 0)
            {
                return;
            }
            else
            {
                //Mostrar los datos
                  getMostrarDatos();
            }
        }

        private void dgvGastos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvGastos.Rows.Count == 0)
            {
                return;
            }
            else
            {
                //Mostrar los datos
                getMostrarDatos();
            }
        }

        #endregion      

        #region Evento Click btnSearchProvider

        private void btnSearchProvider_Click(object sender, EventArgs e)
        {
            // Vacío el vector de los proveedores
            VaciarVectoresGlobales();
            // LLamo al formulario que busca los proveedores
            frmProveedoresBuscar myForm = new frmProveedoresBuscar(true, true);
            // Lo muestro
            myForm.ShowDialog();
            // Cargo los datos del proveedor en el formulario
            CargarProveedorNuevo();
        }

        #endregion

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidad disponible para pròxima Beta!","Informaciòn!",MessageBoxButtons.OK, MessageBoxIcon.Information);            
        }
      }
    }


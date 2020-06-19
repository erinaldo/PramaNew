using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Prama.Clases;
using Prama.Formularios.Articulos;

namespace Prama
{
    public partial class frmABM_PedPresu : Form
    {

        //Variable que pasa por referencia para obtener numero de comprobante FEAFIP
        public int IdCliente = 0;
        public int Id = 0;
        public bool bPresupuesto = false;
        public string myEstado = "";

        //Objetos 
        clsCLientes myCliente = new clsCLientes();
        clsPedidos myPedido;
        clsDetallePedidos myDetPedido;
        
        clsPresupuestos myPresu;
        clsDetallePresupuestos myDetPresu;


        #region CONSTRUCTOR

        public frmABM_PedPresu(string p_Estado, int p_IdCliente, int p_Id, bool pPresupuesto)
        {
            InitializeComponent();
            myEstado = p_Estado;
            IdCliente = p_IdCliente;
            Id = p_Id;
            bPresupuesto = pPresupuesto;
            //PEDIDO O PRESUPUESTO, INSTANCIAR OBJETOS
            if (!(bPresupuesto))
            {
                myPedido = new clsPedidos();
                myDetPedido = new clsDetallePedidos();
            }
            else
            {
                myPresu = new clsPresupuestos();
                myDetPresu = new clsDetallePresupuestos();
            }
        }

        #endregion

        #region Evento LOAD del Formulario

        private void frmABM_PedPresu_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			

            //Tooltips
            this.CargarToolTips();

            //Habilitar
            this.HabilitarControles();

            //Cargar Combos
            CargarCombosFormulario();

            // Vacío de datos el vector de Clientes
            clsGlobales.ClientesSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ClientesSeleccionados, 0);
            // Vacío de datos el vector de los insumos
            clsGlobales.ProductosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.ProductosSeleccionados, new int[] { 0, 2 });
            
            //Cargar Punto Venta
            this.lblPunto.Text = clsGlobales.cParametro.PtoVtaPorDefecto.ToString("D4");

            //Si es un presupuesto ocultar o mostrar controles..
            if (bPresupuesto)
            {
                //Titulo ventana
                if (this.myEstado == "A")
                {
                  this.Text = clsGlobales.cParametro.NombreFantasia + " - ALTA PRESUPUESTOS ";
                }
                else
                {
                  this.Text = clsGlobales.cParametro.NombreFantasia + " - EDICION PRESUPUESTOS ";
                }

                //Controles
                HabilitarControlesPresupuesto();
            }
            else
            {
                //Titulo ventana
                if (this.myEstado == "A")
                {
                  this.Text = clsGlobales.cParametro.NombreFantasia + " - ALTA PEDIDOS ";
                }
                else
                {
                  this.Text = clsGlobales.cParametro.NombreFantasia + " - EDICION PEDIDOS ";
                }
            }
            //Esta en Alta
            if (this.myEstado == "A")
            {
                //Recibe
                if (!bPresupuesto)
                {
                  txtRecibe.Text = clsGlobales.UsuarioLogueado.Usuario;
                }

                //Fechas
                lblPunto.Text = Convert.ToInt32(lblPunto.Text).ToString("D4");
                nroComp.Text = (clsDataBD.RetornarMax("Pedidos", "Nro") + 1).ToString("D8");
                this.dtFecha.Value = DateTime.Now;

            }
            else if (this.myEstado == "M")
            {

                //Cambiar botones respecto a Clientes
                this.btnSearchClient.Enabled = false;
                this.btnQuitarCli.Enabled = false;

                //Generar el resto de la vista para poder modificar
                GenerarVistaModificacion();
            }

            //CONTROLAR COLUMNAS A MOSTRAR U OCULTAR
            if ((Convert.ToInt32(cboTipoCliente.SelectedValue) == 1))
            {
                setOtrosItems(1); 
            }                
            else
            { 
                setOtrosItems(2); 
            }

        }

        #endregion    
   
        #region Metodo: HabilitarControlesPresupuesto

        //HABILITA O INHABILITA CONTROLES SI SE TRATA DE UN PRESUPUESTO
        private void HabilitarControlesPresupuesto()
        {
            //Ocultar Recibio
            lblRecibio.Visible = false;
            txtRecibe.TabStop = false;
            txtRecibe.Visible = false;

            //Entrada
            lblEntrada.Visible = false;
            txtEntrada.TabStop = false;
            txtEntrada.Visible = false;

            //

        }

        #endregion

        #region METODOS


        #region Metodo: setOtrosItems

        //MUESTRA U OCULA COLUMNAS SEGUN CLIENTE IVA RESP INSCRIPTO O NO
        private void setOtrosItems(int p_Tipo)
        {
            switch (p_Tipo)
            {
                case 1: //Insumos / Ingredientes
                    this.dgvArt.Columns["IVA"].Visible = true;
                    this.dgvArt.Columns["SubTotalIva"].Visible = true;
                    this.dgvArt.Columns["Articulo"].Width = 150;
                    break;
                case 2: //Precio - Productos
                    this.dgvArt.Columns["IVA"].Visible = false;
                    this.dgvArt.Columns["SubTotalIva"].Visible = false;
                    this.dgvArt.Columns["Articulo"].Width = 250;
                    break;
            }
        }

        #endregion

        #region Metodo: CargarCombosFormulario

        //CARGA LOS COMBOS DEL FORMULARIO
        private void CargarCombosFormulario()
        {
            //Cliente Tipo Responsable
            clsDataBD.CargarComboTipoResponsable(this.cboTipoCliente, "TipoResponsables", "TipoResponsable", "IdTipoResponsable");
            cboTipoCliente.SelectedIndex = -1;

            //Condicion Compra
            clsDataBD.CargarCombo(this.cboCondicionCompra, "CondicionesCompra", "CondicionCompra", "IdCondicionCompra");
            cboCondicionCompra.SelectedIndex = -1;

            //Transporte
            clsDataBD.CargarCombo(this.cboTransporte, "Transportes", "RazonSocial", "IdTransporte");
            cboTransporte.SelectedIndex = -1;

            //Tipo Cliente
            clsDataBD.CargarCombo(this.cboTipo, "TiposClientes", "TipoCliente", "IdTipoCliente");
            cboTipo.SelectedIndex = -1;
        }

        #endregion
        
        #region Metodo: GenerarVistaModificacion

        private void GenerarVistaModificacion()
        { 
          //Traer datos del cliente
            this.CargarClientes(IdCliente);
            this.PasarDatosAlFormulario();

          //NO ES PRESUPUESTO?
            if (!(bPresupuesto))
            {
                //PEDIDO
                this.CargarPedido();
                //PASAR LOS DATOS AL FORMULARIO
                this.PasarPedidoAlFormulario();
                //TRAER DETALLE PEDIDO
                CargarDetallePedido();
            }
            else
            {
                //CARGAR PRESUPUESTO
                this.CargarPresu();
                //PASAR LOS DATOS AL FORMULARIO
                this.PasarPresuAFormulario();
                //DETALLE PRESUPUESTO
                this.CargarDetallePresu();
            }
        }

        #endregion

        #region Metodo: CargarDetallePresu

        //CARGA EL DETALLE DEL PEDIDO A LA GRILLA
        private void CargarDetallePresu()
        {

            //Variable para contador de filas grilla
            int filas = 0;

            // Vacío el source de la grilla
            dgvArt.DataSource = null;

            // Armo la cadena SQL
            string myCadenaSQL = "Select * from Vista_Detalle_Presu_ABM Where IdPresupuesto =  " + Id;
            // Creo una tabla que me va a almacenar el resultado de la consulta
            DataTable myTabla = clsDataBD.GetSql(myCadenaSQL);
            // Evito que el dgv genere columnas automáticas
            dgvArt.AutoGenerateColumns = false;

            //Contador
            int Item = 1;

            //Mostrar Datos
            foreach (DataRow fila in myTabla.Rows)
            {
                /*Agregar Fila*/
                dgvArt.Rows.Add();

                // Cuento las filas de la grilla
                filas = dgvArt.Rows.Count;

                // Si la grilla no está vacía
                if (filas > 0)
                {
                    //Posiciono la grilla en la última fila
                    dgvArt.CurrentCell = dgvArt[2, filas - 1];
                }

                //Cargar Datos a la fila                
                dgvArt.CurrentRow.Cells["Item"].Value = Item;
                dgvArt.CurrentRow.Cells["IdArticulo"].Value = fila["IdArticulo"].ToString();
                dgvArt.CurrentRow.Cells["CodigoArticulo"].Value = fila["CodigoArticulo"].ToString();
                dgvArt.CurrentRow.Cells["Cantidad"].Value = fila["Cantidad"].ToString();
                dgvArt.CurrentRow.Cells["Articulo"].Value = fila["Articulo"].ToString();
                dgvArt.CurrentRow.Cells["Precio"].Value = fila["Precio"].ToString();
                dgvArt.CurrentRow.Cells["Tabla"].Value = fila["Tabla"].ToString();
                dgvArt.CurrentRow.Cells["IdCoeficiente"].Value = fila["Coeficiente"].ToString();
                dgvArt.CurrentRow.Cells["Pub"].Value = fila["Pub"].ToString();
                dgvArt.CurrentRow.Cells["Dist"].Value = fila["Dist"].ToString();
                dgvArt.CurrentRow.Cells["Rev"].Value = fila["Rev"].ToString();

                //Contador
                Item++;

            }

            //REARMAR VECTOR
            // Cantidad de productos para el vector
            int productos = 0;
            // Recorro la grilla para cargar el item
            foreach (DataGridViewRow row in dgvArt.Rows)
            {
                // Redimensiono el tamaño de la matriz de Insumos
                clsGlobales.ProductosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.ProductosSeleccionados, new int[] { productos + 1, 2 });
                // A la posición creada le asigno el Id seleccionado y la cantidad cargada
                clsGlobales.ProductosSeleccionados[productos, 0] = Convert.ToDouble(row.Cells["IdArticulo"].Value);
                clsGlobales.ProductosSeleccionados[productos, 1] = Convert.ToDouble(row.Cells["Cantidad"].Value);
                // Aumento el contador de insumos
                productos++;
            }
            //


            // Habilito el botón aceptar
            btnAceptar.Enabled = true;
            // Paso el foco a la grilla
            dgvArt.Focus();
            // Pongo el foco en el campo cantidad
            dgvArt.CurrentCell = dgvArt["Cantidad", 0];

            //VERIFICAR CAMPOS Y CALCULOS
            this.VerificarCampos();

        }

        #endregion

        #region Metodo: CargarDetallePedido

        //CARGA EL DETALLE DEL PEDIDO A LA GRILLA
        private void CargarDetallePedido()
        {

              //Variable para contador de filas grilla
                int filas = 0;

             // Vacío el source de la grilla
                dgvArt.DataSource = null;

              // Armo la cadena SQL
                string myCadenaSQL = "Select * from Vista_Detalle_Pedido_ABM Where IdPedido =  " + Id;
                // Creo una tabla que me va a almacenar el resultado de la consulta
                DataTable myTabla = clsDataBD.GetSql(myCadenaSQL);
                // Evito que el dgv genere columnas automáticas
                dgvArt.AutoGenerateColumns = false;
                
                //Contador
                int Item = 1;

                //Mostrar Datos
                foreach (DataRow fila in myTabla.Rows)
                {
                    /*Agregar Fila*/
                    dgvArt.Rows.Add();

                    // Cuento las filas de la grilla
                    filas = dgvArt.Rows.Count;

                    // Si la grilla no está vacía
                    if (filas > 0)
                    {
                        //Posiciono la grilla en la última fila
                        dgvArt.CurrentCell = dgvArt[2, filas - 1];
                    }

                    //Cargar Datos a la fila                
                    dgvArt.CurrentRow.Cells["Item"].Value = Item;
                    dgvArt.CurrentRow.Cells["IdArticulo"].Value = fila["IdArticulo"].ToString();
                    dgvArt.CurrentRow.Cells["CodigoArticulo"].Value = fila["CodigoArticulo"].ToString();
                    dgvArt.CurrentRow.Cells["Cantidad"].Value = fila["Cantidad"].ToString();
                    dgvArt.CurrentRow.Cells["Articulo"].Value = fila["Articulo"].ToString();
                    dgvArt.CurrentRow.Cells["Precio"].Value = fila["Precio"].ToString();
                    dgvArt.CurrentRow.Cells["Tabla"].Value = fila["Tabla"].ToString();
                    dgvArt.CurrentRow.Cells["IdCoeficiente"].Value = fila["Coeficiente"].ToString();
                    dgvArt.CurrentRow.Cells["Pub"].Value = fila["Pub"].ToString();
                    dgvArt.CurrentRow.Cells["Dist"].Value = fila["Dist"].ToString();
                    dgvArt.CurrentRow.Cells["Rev"].Value = fila["Rev"].ToString();

                    //Contador
                    Item++;
            
                }
                
                //REARMAR VECTOR
                // Cantidad de productos para el vector
                int productos = 0;
                // Recorro la grilla para cargar el item
                foreach (DataGridViewRow row in dgvArt.Rows)
                {
                  // Redimensiono el tamaño de la matriz de Insumos
                  clsGlobales.ProductosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.ProductosSeleccionados, new int[] { productos + 1, 2 });
                  // A la posición creada le asigno el Id seleccionado y la cantidad cargada
                  clsGlobales.ProductosSeleccionados[productos, 0] = Convert.ToDouble(row.Cells["IdArticulo"].Value);
                  clsGlobales.ProductosSeleccionados[productos, 1] = Convert.ToDouble(row.Cells["Cantidad"].Value);
                  // Aumento el contador de insumos
                  productos++;    
                }
                //


                // Habilito el botón aceptar
                btnAceptar.Enabled = true;
                // Paso el foco a la grilla
                dgvArt.Focus();
                // Pongo el foco en el campo cantidad
                dgvArt.CurrentCell = dgvArt["Cantidad", 0];

               //VERIFICAR CAMPOS Y CALCULOS
                this.VerificarCampos();

        }

        #endregion

        #region Metodo: CargarPedido

        //CARGAR EL PEDIDO ACTUAL AL OBJETO PARA PODER MOSTRAR LOS DATOS EN EL FORMULARIO
        private void CargarPedido()
        {
            // Armo la cadena SQL
            string myCadenaSQL = "select * from Vista_Pedidos where IdPedido = " + Id;
            // Creo una tabla que me va a almacenar el resultado de la consulta
            DataTable myTablaPedido = clsDataBD.GetSql(myCadenaSQL);
            // Recorro la tabla y le asigo los datos del proveedor al objeto
            foreach (DataRow row in myTablaPedido.Rows)
            {
                //CARGAR AL OBJETO
                myPedido.IdPedido = Convert.ToInt32(row["IdPedido"]);
                myPedido.IdCliente = Convert.ToInt32(row["IdCliente"]);
                myPedido.IdFormaPago = Convert.ToInt32(row["IdFormaPago"]);
                myPedido.IdTransporte = Convert.ToInt32(row["IdTransporte"]);
                myPedido.Recibio = row["Recibio"].ToString();
                myPedido.Entrada = row["Entrada"].ToString();
                myPedido.Fecha = Convert.ToDateTime(row["Fecha"]);
                myPedido.Finalizado = Convert.ToDateTime(row["Finalizado"]);
                myPedido.Flete = Convert.ToDouble(row["Flete"]);
                myPedido.Dto = Convert.ToDouble(row["Dto"]);
                myPedido.Comentario = row["Comentario"].ToString();
                myPedido.Punto = Convert.ToInt32(row["Punto"]);
                myPedido.Nro = Convert.ToInt32(row["Nro"]);
                myPedido.PuntoNro = row["PuntoNro"].ToString();
                myPedido.Cerrado = Convert.ToInt32(row["Cerrado"]);
                myPedido.Activo = Convert.ToInt32(row["Activo"]);
            }
        
        }

        #endregion

        #region Metodo: CargarPresu

        //CARGAR EL PRESUPUESTO ACTUAL AL OBJETO PARA PODER MOSTRAR LOS DATOS EN EL FORMULARIO
        private void CargarPresu()
        {
            // Armo la cadena SQL
            string myCadenaSQL = "select * from Vista_Presupuestos where IdPresupuesto = " + Id;
            // Creo una tabla que me va a almacenar el resultado de la consulta
            DataTable myTablaPresu = clsDataBD.GetSql(myCadenaSQL);
            // Recorro la tabla y le asigo los datos del proveedor al objeto
            foreach (DataRow row in myTablaPresu.Rows)
            {
                //CARGAR AL OBJETO
                myPresu.IdPresupuesto = Convert.ToInt32(row["IdPresupuesto"]);
                myPresu.IdCliente = Convert.ToInt32(row["IdCliente"]);
                myPresu.IdFormaPago = Convert.ToInt32(row["IdFormaPago"]);
                myPresu.IdTransporte = Convert.ToInt32(row["IdTransporte"]);
                myPresu.Fecha = Convert.ToDateTime(row["Fecha"]);
                myPresu.Flete = Convert.ToDouble(row["Flete"]);
                myPresu.Dto = Convert.ToDouble(row["Dto"]);
                myPresu.Punto = Convert.ToInt32(row["Punto"]);
                myPresu.Nro = Convert.ToInt32(row["Nro"]);
                myPresu.PuntoNro = row["PuntoNro"].ToString();
                myPresu.Activo = Convert.ToInt32(row["Activo"]);
            }

        }

        #endregion

        #region Metodo: PasarPedidoAlFormulario

        //CARGA AL FORMULARIO LOS DATOS DEL PEDIDO ACTUAL
        private void PasarPedidoAlFormulario()
        {

          //Paso los datos del proveedor al formulario
            dtFecha.Value = myPedido.Fecha;
            txtComentario.Text = myPedido.Comentario;
            txtRecibe.Text = myPedido.Recibio;
            txtEntrada.Text = myPedido.Entrada;
            lblPunto.Text = myPedido.Punto.ToString("D4");
            nroComp.Text = myPedido.Nro.ToString("D8");
            txtDto.Text = myPedido.Dto.ToString("#0.00");
            txtCostoFlete.Text = myPedido.Flete.ToString("#0.00");
            cboTransporte.SelectedValue = myPedido.IdTransporte;
            cboCondicionCompra.SelectedValue = myPedido.IdFormaPago;

        }

        #endregion

        #region Metodo: PasarPresuAFormulario

        //CARGA AL FORMULARIO LOS DATOS DEL PEDIDO ACTUAL
        private void PasarPresuAFormulario()
        {

            //Paso los datos del proveedor al formulario
            dtFecha.Value = myPresu.Fecha;
            lblPunto.Text = myPresu.Punto.ToString("D4");
            nroComp.Text = myPresu.Nro.ToString("D8");
            txtDto.Text = myPresu.Dto.ToString("#0.00");
            txtCostoFlete.Text = myPresu.Flete.ToString("#0.00");
            cboTransporte.SelectedValue = myPresu.IdTransporte;
            cboCondicionCompra.SelectedValue = myPresu.IdFormaPago;

        }

        #endregion

        #region Metodo: HabilitarControles

        private void HabilitarControles()
        {
            //Solo si es un Alta...
            if (myEstado == "A")
            {
                this.btnSearchClient.Enabled = true;
                this.btnQuitarCli.Enabled = false;

                this.btnAgregaArtExcl.Enabled = false;
                this.btnAgregarArt.Enabled = false;
                if (clsGlobales.ClientesSeleccionados.GetLength(0) > 0)
                {
                    //habilitar articulos
                    this.btnAgregaArtExcl.Enabled = true;
                    this.btnAgregarArt.Enabled = true;
                    this.btnQuitarCli.Enabled = true;
                    this.btnSearchClient.Enabled = false;
                }
            }
            else
            { 

            }

            //Controlar boton quitar
            this.btnQuitarArt.Enabled = true;
            this.btnAceptar.Enabled = true;
            if (dgvArt.Rows.Count == 0)
            {
                this.btnQuitarArt.Enabled = false;
                this.btnAceptar.Enabled = false;
            }
           
            //Solo si es un Alta
            if (myEstado == "A")
            {
                if (dgvArt.Rows.Count == 0 && clsGlobales.ClientesSeleccionados.GetLength(0) == 0)
                {
                    this.btnAgregaArtExcl.Enabled = false;
                    this.btnAgregarArt.Enabled = false;
                    this.btnQuitarArt.Enabled = false;

                    this.btnAceptar.Enabled = false;
                }
            
                if ( clsGlobales.ClientesSeleccionados.GetLength(0) == 0)
                {
                    this.btnAceptar.Enabled = false;
                }
            }
        }

        #endregion

        #region Método que trae el Cliente para una nueva factura

        private void CargarClienteNuevo()
        {
            // Si el vector tiene ,ás de un proveedor seleccionado
            if (clsGlobales.ClientesSeleccionados.GetLength(0) > 1)
            {
                // Informo que solo se puede seleccionar un proveedor
                MessageBox.Show("Solo puede seleccionar un Cliente!", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Vuelvo a abrir el formulario de búsqueda de proveedores
                // LLamo al formulario que busca los Clientes
                frmBuscarCliente myForm = new frmBuscarCliente();
                myForm.ShowDialog();
            }
            // Si hay algún proveedor seleccionado
            if (clsGlobales.ClientesSeleccionados.GetLength(0) > 0)
            {
                // Recorro el vector hasta que encuentro un Id de proveedor y lo paso a los controles del formulario
                for (int i = 0; i <= clsGlobales.ClientesSeleccionados.GetLength(0); i++)
                {
                    // Si la posición tiene un ID de proveedor, busco los datos del mismo
                    if (clsGlobales.ClientesSeleccionados[0] > 0)
                    {
                        // Cargo los datos del proveedor
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
            this.txtCuit.Text = this.myCliente.Cuit;
            this.txtRazonSocial.Text = this.myCliente.RazonSocial;
            this.txtDir.Text = this.myCliente.Direccion;
            this.txtLoc.Text = this.myCliente.Localidad;

            this.cboTipoCliente.SelectedValue = this.myCliente.IdCondicionIva;
            this.cboTipo.SelectedValue = this.myCliente.IdTipoCliente;

            this.IdCliente = this.myCliente.Codigo;
            
            this.cboCondicionCompra.SelectedValue = this.myCliente.IdCondicionCompra;
            this.cboTransporte.SelectedValue = this.myCliente.IdTransporte;

        }

        #endregion

        #region Metodo: LimpiarCliente

        private void LimpiarCliente()
        {
            this.txtCuit.Text = "";
            this.txtRazonSocial.Text = "";
            this.txtDir.Text = "";
            this.txtLoc.Text = "";
            cboTipoCliente.SelectedIndex = -1;
            this.IdCliente = 0;
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

        #endregion

        #region Botones Eventos

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            //VALIDAR PANTALLA
            bool bValida = ValidaPantalla();
            if (!(bValida))
            {
                return;
            }

            //GUARDAR
            if (!(bPresupuesto))
            {
                if (this.myEstado == "A")
                {
                    this.GuardarPedido();
                    this.GuardarDetallePedido();
                }
                else
                {
                    this.ModificarPedido();
                    this.ModificarDetallePedido();
                }
            }
            else
            {
                this.ModificarPresu();
                this.ModificarDetallePresu();
            }

            //FINALIZASR
            this.Close();
        }

        #endregion

        #region Metodo: GuardarDetallePedido

        private void GuardarDetallePedido()
        {
            int LastIdDetalle = 0;

            clsDetallePedidos myDetalle = new clsDetallePedidos();

            //Recorrer la grilla
            foreach (DataGridViewRow row in dgvArt.Rows)
            {
                //Nuevo IdDetalle
                LastIdDetalle = clsDataBD.RetornarUltimoId("DetallePedidos", "IdDetallePedido") + 1;

                //IdArticulo Clave Principal y relacion con factura
                myDetalle.IdDetallePedido = LastIdDetalle;
                myDetalle.IdPedido = myPedido.IdPedido;
                myDetalle.IdArticulo = Convert.ToInt32(row.Cells["IdArticulo"].Value);
                myDetalle.Descripcion = row.Cells["Articulo"].Value.ToString();
                myDetalle.Cantidad = Convert.ToDouble(row.Cells["Cantidad"].Value);
                myDetalle.PrecioUnitario = Convert.ToDouble(row.Cells["Precio"].Value);
                myDetalle.Codigo_Articulo = row.Cells["CodigoArticulo"].Value.ToString();
                myDetalle.Activo = 1;
                myDetalle.Excel = 0;

                //Cargar el producto en la tabla
                //Guardar la factura
                string myCadSQL = "INSERT INTO DetallePedidos (IdDetallePedido, " +
                                                           " IdPedido," +
                                                           " IdArticulo," +
                                                           " Codigo_Articulo," + 
                                                           " Cantidad," +
                                                           " Descripcion," +
                                                           " PrecioUnitario," + 
                                                           " Activo," +  
                                                           " Excel)" +
                                                           "  values (" + myDetalle.IdDetallePedido + "," +
                                                                             myDetalle.IdPedido + "," +
                                                                             myDetalle.IdArticulo + ",'" +
                                                                             myDetalle.Codigo_Articulo + "'," +
                                                                             myDetalle.Cantidad.ToString().Replace(",", ".") + ",'" +
                                                                             myDetalle.Descripcion + "'," +
                                                                             myDetalle.PrecioUnitario.ToString().Replace(",", ".") + "," +                                                                             
                                                                             myDetalle.Activo + "," +
                                                                             myDetalle.Excel + ")";


                clsDataBD.GetSql(myCadSQL);

            }
        }

        #endregion

        #region Metodo: ModificarDetallePedido

        private void ModificarDetallePedido()
        {
            int LastIdDetalle = 0;

            //Eliminar el detalle actual
            string myCad = "Delete from DetallePedidos Where IdPedido = " + Id;
            clsDataBD.GetSql(myCad);

            //RE GRABAR EL DETALLE DE PEDIDO
            foreach (DataGridViewRow row in dgvArt.Rows)
            {
                //Nuevo IdDetalle
                LastIdDetalle = clsDataBD.RetornarUltimoId("DetallePedidos", "IdDetallePedido") + 1;

                //IdArticulo Clave Principal y relacion con factura
                myDetPedido.IdDetallePedido = LastIdDetalle;
                myDetPedido.IdPedido = Id;
                myDetPedido.IdArticulo = Convert.ToInt32(row.Cells["IdArticulo"].Value);
                myDetPedido.Descripcion = row.Cells["Articulo"].Value.ToString();
                myDetPedido.Cantidad = Convert.ToDouble(row.Cells["Cantidad"].Value);
                myDetPedido.PrecioUnitario = Convert.ToDouble(row.Cells["Precio"].Value);
                myDetPedido.Codigo_Articulo = row.Cells["CodigoArticulo"].Value.ToString();
                myDetPedido.Activo = 1;
                myDetPedido.Excel = 0;

                //Cargar el producto en la tabla
                //Guardar la factura
                string myCadSQL = "INSERT INTO DetallePedidos (IdDetallePedido, " +
                                                           " IdPedido," +
                                                           " IdArticulo," +
                                                           " Codigo_Articulo," +
                                                           " Cantidad," +
                                                           " Descripcion," +
                                                           " PrecioUnitario," +
                                                           " Activo, " +
                                                           " Excel)" +
                                                           "  values (" + myDetPedido.IdDetallePedido + "," +
                                                                             myDetPedido.IdPedido + "," +
                                                                             myDetPedido.IdArticulo + ",'" +
                                                                             myDetPedido.Codigo_Articulo + "'," +
                                                                             myDetPedido.Cantidad.ToString().Replace(",", ".") + ",'" +
                                                                             myDetPedido.Descripcion + "'," +
                                                                             myDetPedido.PrecioUnitario.ToString().Replace(",", ".") + "," +
                                                                             myDetPedido.Activo + "," +
                                                                             myDetPedido.Excel + ")";


                clsDataBD.GetSql(myCadSQL);

            }
        }

        #endregion

        #region Metodo: ModificarDetallePresu

        private void ModificarDetallePresu()
        {
            int LastIdDetalle = 0;

            //Eliminar el detalle actual
            string myCad = "Delete from DetallePresupuestos Where IdPresupuesto = " + Id;
            clsDataBD.GetSql(myCad);

            //RE GRABAR EL DETALLE DE PEDIDO
            foreach (DataGridViewRow row in dgvArt.Rows)
            {
                //Nuevo IdDetalle
                LastIdDetalle = clsDataBD.RetornarUltimoId("DetallePresupuestos", "IdDetPresupuesto") + 1;

                //IdArticulo Clave Principal y relacion con factura
                myDetPresu.IdDetPresupuesto = LastIdDetalle;
                myDetPresu.IdPresupuesto = Id;
                myDetPresu.IdArticulo = Convert.ToInt32(row.Cells["IdArticulo"].Value);
                myDetPresu.Descripcion = row.Cells["Articulo"].Value.ToString();
                myDetPresu.Cantidad = Convert.ToDouble(row.Cells["Cantidad"].Value);
                myDetPresu.PrecioUnitario = Convert.ToDouble(row.Cells["Precio"].Value);
                myDetPresu.Codigo_Articulo = row.Cells["CodigoArticulo"].Value.ToString();
                myDetPresu.Excel = Convert.ToInt32(row.Cells["Excel"].Value.ToString());
                myDetPresu.Activo = 1;

                //Cargar el producto en la tabla
                //Guardar la factura
                string myCadSQL = "INSERT INTO DetallePresupuestos (IdDetPresupuesto, " +
                                                           " IdPresupuesto," +
                                                           " IdArticulo," +
                                                           " Codigo_Articulo," +
                                                           " Cantidad," +
                                                           " Descripcion," +
                                                           " PrecioUnitario," +
                                                           " Activo," +
                                                           " Excel)" +
                                                           "  values (" + myDetPresu.IdDetPresupuesto + "," +
                                                                             myDetPresu.IdPresupuesto + "," +
                                                                             myDetPresu.IdArticulo + ",'" +
                                                                             myDetPresu.Codigo_Articulo + "'," +
                                                                             myDetPresu.Cantidad.ToString().Replace(",", ".") + ",'" +
                                                                             myDetPresu.Descripcion + "'," +
                                                                             myDetPresu.PrecioUnitario.ToString().Replace(",", ".") + "," +
                                                                             myDetPresu.Activo + "," + 
                                                                             myDetPresu.Excel + ")";


                clsDataBD.GetSql(myCadSQL);

            }
        }

        #endregion

        #region Eventos Click

        private void btnQuitarCli_Click(object sender, EventArgs e)
        {
            // Recorro el vector
            for (int i = 0; i < clsGlobales.ClientesSeleccionados.GetLength(0); i++)
            {
                // Si el Cliente que quiero borrar está en el vector
                if (clsGlobales.ClientesSeleccionados[i] == IdCliente)
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
                this.LimpiarCliente();
            }

        }

        private void btnQuitarArt_Click(object sender, EventArgs e)
        {
            int LargoProductos = clsGlobales.ProductosSeleccionados.GetLength(0);

            // Recorro el vector
            for (int i = 0; i < LargoProductos; i++)
            {
                // Si el producto que quiero borrar está en el vector
                if (clsGlobales.ProductosSeleccionados[i, 0] == Convert.ToDouble(dgvArt.CurrentRow.Cells["IdArticulo"].Value))
                {
                    // Elimino el prodcuto del vector
                    clsGlobales.ProductosSeleccionados[i, 0] = 0;
                    // Elimino su cantidad
                    clsGlobales.ProductosSeleccionados[i, 1] = 0;
                    // Salgo del for
                    break;
                }
            }

            // Elimino la fila de la grilla
            dgvArt.Rows.RemoveAt(dgvArt.CurrentRow.Index);
            //Controles
            this.HabilitarControles();
            // Recalculo el total de la orden de compra
            CalcularTotal();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Elimino el proveedor del vector
            btnQuitarCli.PerformClick();
            //Cerrar
            this.Close();
        }

        private void btnAgregarArt_Click(object sender, EventArgs e)
        {

            int LargoProductos = clsGlobales.ProductosSeleccionados.GetLength(0);
                 // Si la grilla tiene filas, cargo a las matrices las cantidades cargadas
            if (!(dgvArt.Rows.Count == 0))
            {


                // Comienzo a recorrer la grilla
                foreach (DataGridViewRow row in dgvArt.Rows)
                {
                    // Si el campo Cantidad no es nulo o 0, asigo el valor al elemento que corresponde en la matriz de ese elemento
                    if (!(row.Cells["Cantidad"].Value == null || Convert.ToDecimal(row.Cells["Cantidad"].Value) == 0))
                    {

                        // Si el artículo es de la tabla insumos
                        if (row.Cells["Tabla"].Value.Equals("PRODUCTOS"))
                        {
                            // Recorro la matriz y cuando el Id del artículo coincida, le paso la cantidad
                            for (int i = 0; i < LargoProductos; i++)
                            {
                                // Si encuentro el Id de la fila en la matriz
                                if (Convert.ToDouble(row.Cells["IdArticulo"].Value) == clsGlobales.ProductosSeleccionados[i, 0])
                                {
                                    // Asigo en valor de cantidad del vector a la columna de la grilla
                                    clsGlobales.ProductosSeleccionados[i, 1] = Convert.ToDouble(row.Cells["Cantidad"].Value);
                                    // salgo del for
                                    break;
                                }
                            }
                        }

                    }

                }
            }

            // Vacío el source de la grilla
            dgvArt.DataSource = null;

            // Creo un nuevo formulario de la clase y lo instancio
            frmArticulosOtraBusqueda myForm = new frmArticulosOtraBusqueda(false, true);
            // Muestro el formulario
            myForm.ShowDialog();

            // Creo una variable que va a almacenar los Id de los proveedores seleccionados
            string sArt = "";
            // Creo una variable que guarda el largo del vector de Ids           
            LargoProductos = clsGlobales.ProductosSeleccionados.GetLength(0);


            if (!(LargoProductos == 0))
            {

                // Recorro el vector y le paso los datos a mi string de Ids
                for (int i = 0; i < LargoProductos; i++)
                {
                    // Si no es el último
                    if (!(i == LargoProductos - 1))
                    {
                        // Paso a string el Id del proveedor y lo ingreso a la cadena
                        sArt = sArt + clsGlobales.ProductosSeleccionados[i, 0].ToString() + ",";
                    }
                    // Si es el último
                    else
                    {
                        // Paso a string el Id del proveedor y lo ingreso a la cadena
                        sArt = sArt + clsGlobales.ProductosSeleccionados[i, 0].ToString();
                    }

                }

            }

            // si hay cargados productos o insumos en los vectores
            if (!(clsGlobales.InsumosSeleccionados.Length == 0) || !(clsGlobales.ProductosSeleccionados.Length == 0))
            {
                // Armo la cadena SQL
                string myCadenaSQL = "select * from Articulos_Productos where IdArticulo in (" + sArt + ")";
                // Creo una tabla que me va a almacenar el resultado de la consulta
                DataTable myTabla = clsDataBD.GetSql(myCadenaSQL);
                // Evito que el dgv genere columnas automáticas
                dgvArt.AutoGenerateColumns = false;
                // Asigno la tabla al source de la grilla de proveedores
                dgvArt.DataSource = myTabla;

                // Si la grilla tiene artículos
                if (dgvArt.RowCount > 0)
                {
                    // Habilito el botón quitar
                    btnQuitarArt.Enabled = true;
                    // Creo un contador
                    int fila = 1;
                    // Recorro la grilla
                    foreach (DataGridViewRow row in dgvArt.Rows)
                    {
                        // Asigno el valor del contador al Item de la fila
                        row.Cells["Item"].Value = fila;

                        // Si el artículo es de la tabla productos
                        if (row.Cells["Tabla"].Value.Equals("PRODUCTOS"))
                        {
                            // Recorro el vector de insumos
                            for (int i = 0; i < LargoProductos; i++)
                            {
                                // Si el Id del articulo coincide con el del vector, le cargo la cartidad
                                if (Convert.ToDouble(row.Cells["IdArticulo"].Value) == clsGlobales.ProductosSeleccionados[i, 0])
                                {
                                    // Asigo en valor de cantidad del vector a la columna de la grilla
                                    row.Cells["Cantidad"].Value = clsGlobales.ProductosSeleccionados[i, 1].ToString("#0.00");
                                    // salgo del for
                                    break;
                                }
                            }
                        }

                        // Incremento el contador
                        fila++;
                    }
                    //calcular
                    this.CalcularSubtotal();
                    //calcular total
                    this.CalcularTotal();
                    // Habilito el botón aceptar
                    btnAceptar.Enabled = true;
                    // Paso el foco a la grilla
                    dgvArt.Focus();
                    // Pongo el foco en el campo cantidad
                    dgvArt.CurrentCell = dgvArt["Cantidad", 0];
                }
            }
         
        }
    
        private void btnSearchClient_Click(object sender, EventArgs e)
        {
          //Buscar Cliente
            frmBuscarCliente myForm = new frmBuscarCliente();
            myForm.ShowDialog();
         //Cliente Nuevo
            this.CargarClienteNuevo();                 
         //Habilitar el gpbArticulos y controlar botones
            this.HabilitarControles();
         //Calculos
            this.CalcularSubtotal();
            this.CalcularTotal();
        }

        private void btnAgregaArtExcl_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidad a implementar en pròxima Beta", "Informaciòn!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;

            //abrimos el dialogo para poder obtener el nombre la ubicacion del archivo
            /*ofdAbrirArchivo.Filter = "Archivos Excel (.xls)|*.xls|All Files (*.*)|*.*";
            ofdAbrirArchivo.FilterIndex = 1;

            //Abrir cuadro de dialog
            ofdAbrirArchivo.ShowDialog();

            //Tomar el nombre del archivo 
            string sArchivo = ofdAbrirArchivo.FileName;

            //Leer Archivo xls
            clsGlobales.cFormato.LeerExcel(sArchivo); */
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region CargarToolTips

        private void CargarToolTips()
        {
            toolTip1.SetToolTip(this.btnAgregarArt, "Agregar Artículo");
            toolTip2.SetToolTip(this.btnQuitarArt, "Quitar Artículo");
            toolTip3.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip4.SetToolTip(this.btnSalir, "Salir");
            toolTip5.SetToolTip(this.btnSearchClient, "Buscar Cliente");
            toolTip6.SetToolTip(this.btnAgregaArtExcl, "Agregar desde Excel");
            
        }

        #endregion

        #region EVENTOS GRILLA     

        private void dgvArt_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            //Declaracion de Variables (Costo Unitario y Cantidad)
            DataGridViewRow row = dgvArt.CurrentRow;

            //Null? salir....
            if (row.Cells["Cantidad"].Value == null)
            {
                //Establecer null al costo final
                dgvArt.CurrentRow.Cells["SubTotal"].Value = null;
                //Volver     
                return;
            }

            //Verificar
            this.VerificarCampos();

        }

        #endregion

        #region Metodo: VerificarCampos

        private void VerificarCampos()
        {
            //Verificar
            this.txtDto.TabStop = false;
            this.txtDto.Enabled = false;
            this.txtCostoFlete.TabStop = false;
            this.txtCostoFlete.Enabled = false;

            double SubTotal = RetornarSubTotal();
            if (SubTotal > 0)
            {
                this.txtDto.TabStop = true;
                this.txtDto.Enabled = true;
                this.txtCostoFlete.TabStop = false;
                this.txtCostoFlete.Enabled = false;
            }

            if (dgvArt.Rows.Count > 0 && this.myEstado == "M")
            {
                this.btnAgregaArtExcl.Enabled = true;
                this.btnAgregarArt.Enabled  =true;
                this.btnQuitarArt.Enabled=true;
            }

            //Calcular Subtotal
            CalcularSubtotal();
            //Calcular el total
            CalcularTotal();
        }

        #endregion

        #region Método que calcula el subtotal de los artículos por fila

        private void CalcularSubtotal()
        {
            // Variable que guarda el resultado de la multiplicación
            double SubTotal = 0;
            double Coef = 0;
            // Recorro la grilla y hago el cálculo
            foreach (DataGridViewRow row in dgvArt.Rows)
            {
                
                //CALCULAR EL COEFICIENTE
                switch (Convert.ToInt32(cboTipo.SelectedValue))
                { 
                    case 28:
                        SubTotal = Convert.ToDouble(row.Cells["Precio"].Value) * Convert.ToDouble(row.Cells["Pub"].Value);
                        Coef = Convert.ToDouble(row.Cells["Pub"].Value);
                        break;
                    case 29:
                        SubTotal = Convert.ToDouble(row.Cells["Precio"].Value) * Convert.ToDouble(row.Cells["Dist"].Value);
                        Coef = Convert.ToDouble(row.Cells["Dist"].Value);
                        break;
                    case 30:
                        SubTotal = Convert.ToDouble(row.Cells["Precio"].Value) * Convert.ToDouble(row.Cells["Rev"].Value);
                        Coef = Convert.ToDouble(row.Cells["Rev"].Value);
                        break;
                }

                //MULTIPLICAR POR LA CANTIDAD
                SubTotal = SubTotal * Convert.ToDouble(row.Cells["Cantidad"].Value);

                //PASAR A VARIABLES PARA PODER FORMATEAR
                double Cant = Convert.ToDouble(row.Cells["Cantidad"].Value);
                double Pre = Convert.ToDouble(row.Cells["Precio"].Value) * Coef;


                //FORMATEO DE COLUMNAS
                row.Cells["Cantidad"].Value = Cant.ToString("#0");
                // Asigno el valor a la celda
                row.Cells["SubTotal"].Value = SubTotal.ToString("#0.00");
                row.Cells["PrecioCoef"].Value = Pre.ToString("#0.00000");
            }
        }

        #endregion

        #region Método que retorna el SubtOTAL

        private double RetornarSubTotal()
        {
            // Variable que va a almacenar la suma de los subtotales
            double SubTotalAcumulado = 0;
            double SubTotalFactura = 0;

            // Vaiable que guarda el número de la fila
            int fila = 1;
            // Recorro la grilla y sumo los subtotales de lo artículos
            foreach (DataGridViewRow row in dgvArt.Rows)
            {
                // Pongo el número del item
                row.Cells["Item"].Value = fila;
                // Acumulo los subtotales
                SubTotalAcumulado = SubTotalAcumulado + Convert.ToDouble(row.Cells["SubTotal"].Value);
                //Aumento el enumerador de los items
                fila++;

            }
            // Muestro el resultado en el textbox
            SubTotalFactura = SubTotalAcumulado;

            return SubTotalFactura;
        }

        #endregion

        #region Método que calcula Total

        private void CalcularTotal()
        {
            // Variable que va a almacenar la suma de los subtotales
            double SubTotalAcumulado = 0;
            double SubTotalFactura = 0;
            double TotalFactura = 0;
            double Descuento = 0;

            // Vaiable que guarda el número de la fila
            int fila = 1;
            // Recorro la grilla y sumo los subtotales de lo artículos
            foreach (DataGridViewRow row in dgvArt.Rows)
            {
                // Pongo el número del item
                row.Cells["Item"].Value = fila;
                // Acumulo los subtotales
                SubTotalAcumulado = SubTotalAcumulado + Convert.ToDouble(row.Cells["SubTotal"].Value);
                //Aumento el enumerador de los items
                fila++;

            }
            // Muestro el resultado en el textbox
            SubTotalFactura = SubTotalAcumulado;

            //Mostrar Subtotal
            this.txtSubTotal.Text = SubTotalFactura.ToString("#0.00");
            
            //Verificar

            if (SubTotalFactura > 0)
            {
               this.txtDto.TabStop = true;
               this.txtDto.Enabled = true;
               this.txtCostoFlete.TabStop = true;
               this.txtCostoFlete.Enabled = true;

            }
            else
            {
                this.txtDto.TabStop = false;
                this.txtDto.Enabled = false;
                this.txtCostoFlete.TabStop = false;
                this.txtCostoFlete.Enabled = false;
            }


            //Descuento
            if (!(string.IsNullOrEmpty(txtDto.Text)))
            { 
                if (clsGlobales.cValida.IsNumeric(txtDto.Text))
                {
                    //Calcular descuento
                    Descuento = (SubTotalFactura * Convert.ToDouble(txtDto.Text)) / 100;
                    //Aplicar Descuento
                    SubTotalFactura = SubTotalFactura - Descuento;
                    //Mostrar el descuento
                }
            }

            TotalFactura = SubTotalFactura;
            
            //Suma flete
            if (!(string.IsNullOrEmpty(txtCostoFlete.Text)))
            {
                if (clsGlobales.cValida.IsNumeric(txtCostoFlete.Text))
                {
                    TotalFactura = TotalFactura + Convert.ToDouble(txtCostoFlete.Text);
                }
            }
                        
            //Mostrar Subtotal
            this.txtTotal.Text = TotalFactura.ToString("#0.00");
            //Con Iva
            this.txtTotalIVA.Text = (Convert.ToDouble(txtTotal.Text) * Convert.ToDouble("1." + clsGlobales.cParametro.Iva)).ToString("#0.00");


        }

        #endregion

        #region Eventos KeyPress

        private void txtDto_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCostoFlete_KeyPress(object sender, KeyPressEventArgs e)
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

        #region Eventos Click

        private void txtCostoFlete_Click(object sender, EventArgs e)
        {
            txtCostoFlete.SelectAll();
        }

        private void txtDto_Click(object sender, EventArgs e)
        {
            txtDto.SelectAll();
        }

        #endregion

        #region Eventos TextChanged

        private void txtCostoFlete_TextChanged(object sender, EventArgs e)
        {
            this.CalcularTotal();
        }

        private void txtDto_TextChanged(object sender, EventArgs e)
        {
            this.CalcularTotal();
        }

        #endregion

        #region Eventos Leave

        private void txtEntrada_Leave(object sender, EventArgs e)
        {
            this.txtEntrada.Text = txtEntrada.Text.ToUpper();
        }

        private void txtDto_Leave(object sender, EventArgs e)
        {

         /*   if (!(string.IsNullOrEmpty(txtDto.Text)))
            {
                double dto = Convet.ToDouble(this.txtDto.Text);
                txtDto.Text = dto.ToString("#0.00");
            }
            else
            {
                txtDto.Text = "0.00";
                   
            }*/
        }

        private void txtCostoFlete_Leave(object sender, EventArgs e)
        {
            /*if (!(string.IsNullOrEmpty(txtCostoFlete.Text)))
            {
                double CostoFlete = Convert.ToDouble(this.txtCostoFlete.Text);
                txtCostoFlete.Text = CostoFlete.ToString("#0.00");
            }
            else
            {
                txtCostoFlete.Text = "0.00";
                   
            }*/
        }

        #endregion

        #region Metodo: ValidarPantalla

        private bool ValidaPantalla()
        {
            bool cValida = true;

            //VALIDACIONES
            if (this.cboCondicionCompra.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, completar 'Condiciòn Compra'!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboCondicionCompra.Focus();
                cValida = false;
                return cValida;
            }
            if (this.cboTransporte.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, completar 'Transporte'!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboTransporte.Focus();
                cValida = false;
                return cValida;
            }

            if (dgvArt.Rows.Count == 0)
            {
                MessageBox.Show("Por favor, agregue Artículos a la lista!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cValida = false;
                return cValida;
            }
            //
            if (clsGlobales.cValida.IsNumeric(txtTotal.Text))
            {
                if (Convert.ToDouble(txtTotal.Text) == 0)
                {
                    MessageBox.Show("Por favor, VERIFIQUE haber cargado el dato 'Cantidad' para todos los artìculos del pedido!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvArt.Focus();
                    cValida = false;
                    return cValida;
                }
            }

            //SINO ES PRESUPUESTO
            if (!(bPresupuesto))
            {
                //ENTRADA
                if (string.IsNullOrEmpty(txtEntrada.Text))
                {
                    MessageBox.Show("Por favor, ingrese el dato para el campo 'Entrada'!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEntrada.Focus();
                    cValida = false;
                    return cValida;
                }
            }


            //VALIDANDO FECHAS
            if (Convert.ToDateTime(dtFecha.Value.ToShortDateString()) > DateTime.Now.Date)
            {
               MessageBox.Show("La Fecha NO puede ser mayor a la actual!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Error);
               dtFecha.Focus();
               cValida = false;
               return cValida;
            }

            //VALIDAR PORCENTAJE DE DESCUENTO
            if (clsGlobales.cValida.IsNumeric(txtDto.Text))
            {
                if (Convert.ToDouble(txtDto.Text) > 100.00)
                {
                   MessageBox.Show("El Porcentaje de Descuento no pude ser mayor a 100!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   dtFecha.Focus();
                   cValida = false;
                   return cValida;
                }
            }


            //retornar
            return cValida;
        }
        
        #endregion

        #region Metodo: ModificarPedido

        private void ModificarPedido()
        {
            //GRABAR PEDIDO
            
            myPedido.IdPedido = this.Id;
            myPedido.IdCliente = this.IdCliente;
            myPedido.IdFormaPago = Convert.ToInt32(cboCondicionCompra.SelectedValue);
            myPedido.IdTransporte = Convert.ToInt32(cboTransporte.SelectedValue);
            myPedido.Recibio = this.txtRecibe.Text;
            myPedido.Entrada = txtEntrada.Text;
            myPedido.Fecha = dtFecha.Value;

            //Flete
            if (string.IsNullOrEmpty(txtCostoFlete.Text))
            {
                myPedido.Flete = 0;
            }
            else
            {
                myPedido.Flete = Convert.ToDouble(txtCostoFlete.Text);
            }

            //Dto
            if (string.IsNullOrEmpty(txtDto.Text))
            {
                myPedido.Dto = 0;
            }
            else
            {
                myPedido.Dto = Convert.ToDouble(txtDto.Text);
            }

            //Comentario
            myPedido.Comentario = txtComentario.Text;
            myPedido.Punto = Convert.ToInt32(lblPunto.Text);
            myPedido.Nro = Convert.ToInt32(nroComp.Text); ;
            myPedido.PuntoNro = myPedido.Punto.ToString("D4") + "-" + myPedido.Nro.ToString("D8");
            myPedido.Cerrado = 0;
            myPedido.Activo = 1;

            //UPDATE A LA TABLA DE PEDIDOS
            string myCadena = "";

            try
            {
                myCadena = "UPDATE Pedidos SET IdPedido =  " + myPedido.IdPedido + "," +
                                                            " IdCliente = " + myPedido.IdCliente + "," +
                                                            " IdFormaPago = " + myPedido.IdFormaPago + "," +
                                                            " Punto = " + myPedido.Punto + "," +
                                                            " Nro = " + myPedido.Nro + "," +
                                                            " PuntoNro ='" + myPedido.PuntoNro + "'," +
                                                            " Entrada = '" + myPedido.Entrada + "'," +
                                                            " Recibio = '" + myPedido.Recibio + "'," +
                                                            " IdTransporte = " + myPedido.IdTransporte + "," +
                                                            " Fecha = '" + myPedido.Fecha.ToShortDateString() + "'," +
                                                            " Comentario = '" + myPedido.Comentario + "'," +
                                                            " Finalizado = '" + myPedido.Finalizado.ToShortDateString() + "'," +
                                                            " Dto = " + myPedido.Dto.ToString().Replace(",", ".") + "," +
                                                            " Flete = " + myPedido.Flete.ToString().Replace(",", ".") + "," +
                                                            " Cerrado = " + myPedido.Cerrado + "," +
                                                            " Activo = " + myPedido.Activo + 
                                                            " WHERE IdPedido = " + myPedido.IdPedido;

                //GUARDAR EN PEDIDOS
                clsDataBD.GetSql(myCadena);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }                
        }

        #endregion

        #region Metodo: ModificarPresu

        private void ModificarPresu()
        {
            //GRABAR PRESUPUESTO
            myPresu.IdPresupuesto = this.Id;
            myPresu.IdCliente = this.IdCliente;
            myPresu.IdFormaPago = Convert.ToInt32(cboCondicionCompra.SelectedValue);
            myPresu.IdTransporte = Convert.ToInt32(cboTransporte.SelectedValue);
            myPresu.Fecha = dtFecha.Value;
            myPresu.Excel = 0;

            //Flete
            if (string.IsNullOrEmpty(txtCostoFlete.Text))
            {
                myPresu.Flete = 0;
            }
            else
            {
                myPresu.Flete = Convert.ToDouble(txtCostoFlete.Text);
            }

            //Dto
            if (string.IsNullOrEmpty(txtDto.Text))
            {
                myPresu.Dto = 0;
            }
            else
            {
                myPresu.Dto = Convert.ToDouble(txtDto.Text);
            }

            //Comentario
            myPresu.Punto = Convert.ToInt32(lblPunto.Text);
            myPresu.Nro = Convert.ToInt32(nroComp.Text); ;
            myPresu.PuntoNro = myPresu.Punto.ToString("D4") + "-" + myPresu.Nro.ToString("D8");
            myPresu.Activo = 1;

            //INSERT A LA TABLA DE PEDIDOS
            string myCadena = "";

            try
            {
                myCadena = "UPDATE Presupuestos SET IdPresupuesto =  " + myPresu.IdPresupuesto + "," +
                                                            " IdCliente = " + myPresu.IdCliente + "," +
                                                            " IdFormaPago = " + myPresu.IdFormaPago + "," +
                                                            " Punto = " + myPresu.Punto + "," +
                                                            " Nro = " + myPresu.Nro + "," +
                                                            " PuntoNro = '" + myPresu.PuntoNro + "'," +
                                                            " IdTransporte = " + myPresu.IdTransporte + "," +
                                                            " Fecha = '" + myPresu.Fecha.ToShortDateString() + "'," +
                                                            " Dto = " + myPresu.Dto.ToString().Replace(",", ".") + "," +
                                                            " Flete = " + myPresu.Flete.ToString().Replace(",", ".") + "," +
                                                            " Activo = " + myPresu.Activo + "," +
                                                            " Excel = " + myPresu.Excel +
                                                            " WHERE IdPresupuesto = " + myPresu.IdPresupuesto;

                //GUARDAR EN PEDIDOS
                clsDataBD.GetSql(myCadena);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        #endregion

        #region Metodo: GuardarPedido

        private void GuardarPedido()
        { 

               //GRABAR PEDIDO
                myPedido.IdPedido = clsDataBD.RetornarUltimoId("Pedidos", "IdPedido") + 1;
                myPedido.IdCliente = this.IdCliente;
                myPedido.IdFormaPago = Convert.ToInt32(cboCondicionCompra.SelectedValue);
                myPedido.IdTransporte = Convert.ToInt32(cboTransporte.SelectedValue);
                myPedido.Recibio = this.txtRecibe.Text;
                myPedido.Entrada = txtEntrada.Text;
                myPedido.Fecha = dtFecha.Value;
                myPedido.Excel = 0;

                //Flete
                if (string.IsNullOrEmpty(txtCostoFlete.Text))
                {
                    myPedido.Flete = 0;
                }
                else
                {
                    myPedido.Flete = Convert.ToDouble(txtCostoFlete.Text);
                }
                
                //Dto
                if (string.IsNullOrEmpty(txtDto.Text))
                {
                    myPedido.Dto = 0;
                }
                else
                {
                    myPedido.Dto = Convert.ToDouble(txtDto.Text);
                }

                //Comentario
                myPedido.Comentario = txtComentario.Text;
                myPedido.Punto = Convert.ToInt32(lblPunto.Text);               
                myPedido.Nro = clsDataBD.RetornarMax("Pedidos", "Nro")+1;
                myPedido.PuntoNro = myPedido.Punto.ToString("D4") + "-" + myPedido.Nro.ToString("D8");
                myPedido.Cerrado = 0;
                myPedido.Activo = 1;

               //INSERT A LA TABLA DE PEDIDOS
                string myCadena = "";

                try
                {

                    //Alta de Articulos
                    myCadena = "INSERT INTO Pedidos (IdPedido," +
                                                    " IdCliente," +
                                                    " IdFormaPago," +
                                                    " Punto," +
                                                    " Nro," +
                                                    " PuntoNro," +
                                                    " Entrada," +
                                                    " Recibio," +
                                                    " IdTransporte," +
                                                    " Fecha," +
                                                    " Comentario," +
                                                    " Finalizado," +
                                                    " Dto," +
                                                    " Flete," +
                                                    " Cerrado," +
                                                    " Activo," +
                                                    " Excel" +
                                                     ") values (" + myPedido.IdPedido + ","
                                                                    + myPedido.IdCliente + ","
                                                                    + myPedido.IdFormaPago + ","
                                                                    + myPedido.Punto + ","
                                                                    + myPedido.Nro + ",'"
                                                                    + myPedido.PuntoNro + "','"
                                                                    + myPedido.Entrada + "','"
                                                                    + myPedido.Recibio + "',"
                                                                    + myPedido.IdTransporte + ",'"
                                                                    + myPedido.Fecha.ToShortDateString() + "','"
                                                                    + myPedido.Comentario + "','"
                                                                    + myPedido.Finalizado.ToShortDateString() + "',"
                                                                    + myPedido.Dto.ToString().Replace(",", ".") + ","
                                                                    + myPedido.Flete.ToString().Replace(",", ".") + ","
                                                                    + myPedido.Cerrado + "," 
                                                                    + myPedido.Activo + ","
                                                                    + myPedido.Excel + ")";

                    //GUARDAR EN PEDIDOS
                    clsDataBD.GetSql(myCadena);

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.StackTrace, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }                
        }

        #endregion

        #region Eventos Grilla

        private void dgvArt_SelectionChanged(object sender, EventArgs e)
        {
            // Si la grilla tiene artículos
            if (dgvArt.RowCount > 0)
            {
                // Almaceno én una variable la posición de fila en la que me encuentro
                int fila = dgvArt.CurrentRow.Index;
                // Pongo el foco de la fila en la columna cantidad
                dgvArt.CurrentCell = dgvArt["Cantidad", fila];
            }
        }

        #endregion

        private void txtDto_Enter(object sender, EventArgs e)
        {
            txtDto.SelectionStart = 0;
            txtDto.SelectionLength = txtDto.Text.Length;
        }

        private void txtCostoFlete_Enter(object sender, EventArgs e)
        {
            txtCostoFlete.SelectionStart = 0;
            txtCostoFlete.SelectionLength = txtCostoFlete.Text.Length;
        }

        private void txtComentario_Leave(object sender, EventArgs e)
        {
            this.txtComentario.Text = txtComentario.Text.ToUpper();
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Prama.Clases;
using System.Diagnostics;

namespace Prama
{
    public partial class frmVentasFacturacionE : Form
    {


        #region DECLARACION VARIABLES

        //Variable que pasa por referencia para obtener numero de comprobante FEAFIP
        public long nroComprobante = 0;
        public int IdCliente = 0;
        public int IdPresupuesto = 0;

        //PARA CALCULOS FACTURACION
        public double acumulaExento = 0;
        public double acumula21 = 0;
        public double acumula10 = 0;
        public double acumula21Dto = 0;
        public double acumula10Dto = 0;
        public double acumulaExentoDto = 0;
        public double dto = 0;
        public double Iva21 = 0;
        public double Iva10 = 0;
        public double acumulaNetoFB = 0;
        public double acumulaNetoFBDto = 0;
        public double acumulaExentoFB = 0;
        public double acumulaExentoFBDto = 0;

        // Instancio un objeto de la calse proveedores para pasarle los datos que me devuleve la consulta
        clsCLientes myCliente = new clsCLientes();
        clsPresupuestos myPresu = new clsPresupuestos();
      
        //Objeto FEAFIP
        private FEAFIPLib.BIWSFEV1 wsfev1 = new FEAFIPLib.BIWSFEV1();

        public DataGridView Presu;
        public DataGridView DetallePresu;

        public double MontoSubTot = 0;
        public double dCantidad = 0; //GUARDA LA CANTIDAD ACTUAL PARA ESE PRODUCTO EN LA GRILLA

        #endregion
        
        #region Constructor

        public frmVentasFacturacionE(int p_IdCliente, int p_IdPresupuesto, DataGridView p_DgvComprobante, DataGridView p_DgvDetalle, double p_SubTot)
        {
            InitializeComponent();
            IdCliente = p_IdCliente;
            IdPresupuesto = p_IdPresupuesto;

            //Viene de ventana presupuestos
            Presu = p_DgvComprobante;
            DetallePresu = p_DgvDetalle;
            MontoSubTot = p_SubTot;
        }

        #endregion

        #region Boton Salir Click

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Vacío de datos el vector de los proveedores
            clsGlobales.ClientesSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ClientesSeleccionados, 0);
            // Vacío de datos el vector de los insumos
            clsGlobales.ProductosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.ProductosSeleccionados, new int[] { 0, 2 });


            //Cerrar
            this.Close();
        }

        #endregion

        #region Evento Load del Formulario

        private void frmFacturacion_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 
			
            //TOOLTIPS
            this.CargarToolTips();

            //CARGAR COMBO TIPO DE COMPROBANTE
            clsDataBD.CargarComboTipoComprobanteVenta(this.cboComprobante, "TipoComprobantes", "Comprobante", "IdTipoComprobante");
           // cboComprobante.SelectedIndex = -1;

            //CARGAR COMBO TIPO DE REPSONSABLE ( CLIENTE )
            clsDataBD.CargarComboTipoResponsable(this.cboTipoCliente, "TipoResponsables", "TipoResponsable", "IdTipoResponsable");
            cboTipoCliente.SelectedIndex = -1;

            //CARGAR COMBO FORMA DE PAGO
            clsDataBD.CargarComboFormaPago(this.cboFormaPago, "FormasPago", "FormaPago", "IdFormaPago");
            cboFormaPago.SelectedValue = 4;            

            //TITULO DE LA VENTANA
            this.Text = clsGlobales.cParametro.NombreFantasia + this.Text;

            //Punto de venta
            this.nroPunto.Text = clsGlobales.cParametro.PtoVtaPorDefecto.ToString("D4");

            //MODO FACTURACION  ***********************************
            if (clsGlobales.cParametro.ModoFactura == 0) //CONFIGURADO EN MODO TEST?
            {
                wsfev1.ModoProduccion = false; //PRODUCCION .F.
            }
            else //SINO
            {
                wsfev1.ModoProduccion = true; //PRODUCCION .T.
            }
            //******************************************************
            
            //TRAER DATOS DEL CLIENTE Y PASARLOS AL FORMULARIO
            this.CargarClientes(IdCliente);
            PasarDatosAlFormulario();

            //ESTABLECER EL TIPO DE COMPROBANTE ( A, B )
            EstablecerTipoComprobante();

            //TRAER LOS DATOS DEL PRESUPUESTO
            this.TraerDatosPresu(IdPresupuesto);

            //TRAER DATOS A FACTURAR
            CargarGrilla();

            //ESTABLECER FECHAS
            this.dtFechaComp.Value = DateTime.Now;
            this.dtVtoPago.Value = DateTime.Now;
            this.dtVencCAE.Value = DateTime.Now;

            //FOCO
            txtCuit.Focus();
        }

        #endregion

        #region Metodo: TraerDatosPresu

        //METODO QUE TRAE EL PRESUPUESTO
        private void TraerDatosPresu(int Id)
        {
            // Armo la cadena SQL
            string myCadenaSQL = "select * from Vista_Presupuestos where IdPresupuesto = " + Id;
            // Creo una tabla que me va a almacenar el resultado de la consulta
            DataTable myTablaPresu = clsDataBD.GetSql(myCadenaSQL);
            // Recorro la tabla y le asigo los datos del proveedor al objeto
            foreach (DataRow rowCli in myTablaPresu.Rows)
            {
                myPresu.IdCliente = Convert.ToInt32(rowCli["IdCliente"].ToString());
                myPresu.IdPresupuesto = Id;             
                myPresu.IdFormaPago	= Convert.ToInt32(rowCli["IdFormaPago"].ToString());
                myPresu.Punto = Convert.ToInt32(rowCli["Punto"].ToString());
                myPresu.Nro =	Convert.ToInt32(rowCli["Nro"].ToString());
                myPresu.PuntoNro = rowCli["PuntoNro"].ToString();
                myPresu.IdTransporte = Convert.ToInt32(rowCli["IdTransporte"].ToString());	
                myPresu.Fecha = Convert.ToDateTime(rowCli["Fecha"].ToString());	
                myPresu.Dto = Convert.ToDouble(rowCli["Dto"].ToString());	
                myPresu.Flete = Convert.ToDouble(rowCli["Flete"].ToString());
                myPresu.Excel = Convert.ToInt32(rowCli["Excel"].ToString());
                myPresu.Pendiente = Convert.ToInt32(rowCli["Pendiente"].ToString());
                myPresu.Facturado = Convert.ToInt32(rowCli["facturado"].ToString());
                myPresu.Codigo_Correo = rowCli["Codigo_Correo"].ToString();

            }

            //Show Dto y Flete
            this.txtDto.Text = myPresu.Dto.ToString("#0.00");
            this.txtFlete.Text = myPresu.Flete.ToString("#0.00");
            this.txtCodigoCorreo.Text = myPresu.Codigo_Correo;
        }

        #endregion

        #region Metodo: CargarGrilla

        //CARGA LA GRILLA CON LOS ARTICULOS DEL PRESUPUESTO
        private void CargarGrilla()
        {
            //Variable para contador de filas grilla
            int filas = 0;

            // Vacío el source de la grilla
            dgvArt.DataSource = null;

            // Armo la cadena SQL
            string myCadenaSQL = "Select * from Vista_Detalle_Presu_ABM Where IdPresupuesto =  " + IdPresupuesto;
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
                dgvArt.CurrentRow.Cells["Articulo"].Value = fila["Articulo"].ToString();
                dgvArt.CurrentRow.Cells["Unidad"].Value = fila["AbreviaturaUnidad"].ToString();
                dgvArt.CurrentRow.Cells["Cantidad"].Value = fila["Cantidad"].ToString();
                dgvArt.CurrentRow.Cells["Precio"].Value = fila["Precio"].ToString();
                dgvArt.CurrentRow.Cells["Dsto"].Value = fila["Dto"].ToString();
                dgvArt.CurrentRow.Cells["Alicuota"].Value = fila["IVA"].ToString();
                dgvArt.CurrentRow.Cells["Coeficiente"].Value = fila["Coeficiente"].ToString();
                dgvArt.CurrentRow.Cells["Pub"].Value = fila["Pub"].ToString();
                dgvArt.CurrentRow.Cells["Dist"].Value = fila["Dist"].ToString();
                dgvArt.CurrentRow.Cells["Rev"].Value = fila["Rev"].ToString();
                dgvArt.CurrentRow.Cells["IdPropio"].Value = fila["IdPropio"].ToString();
                dgvArt.CurrentRow.Cells["Excel"].Value = fila["Excel"].ToString();

                //Contador
                Item++;

            }

            //Ocultar Columnas que no van
            if (Convert.ToInt32(this.cboComprobante.SelectedValue) == 1)
            {
                //Ocultar y mostrar comidas
                dgvArt.Columns[8].Visible = true;
                dgvArt.Columns[9].Visible = false;
                dgvArt.Columns[11].Visible = true;
                dgvArt.Columns[12].Visible = true;
                dgvArt.Columns[13].Visible = true;
                dgvArt.Columns[14].Visible = false;
                dgvArt.Columns[15].Visible = true;
                //Columna Articulo redimensionar
                dgvArt.Columns[4].Width = 180;
                //Mostrar valor iva
                lblIVA.Visible = true;
                txtIVA.Visible = true;
                lblIVA10.Visible = true;
                txtIVA10.Visible = true;
                //Ocultar linea
                LineaIva.Visible = false;
                LineaIVA10.Visible = false;

            }
            else
            {
                //Ocultar y mostrar comidas
                dgvArt.Columns[8].Visible = false;
                dgvArt.Columns[9].Visible = false;
                dgvArt.Columns[10].Visible = true;
                dgvArt.Columns[11].Visible = false;
                dgvArt.Columns[12].Visible = false;
                dgvArt.Columns[13].Visible = false;
                dgvArt.Columns[14].Visible = true;
                dgvArt.Columns[15].Visible = false;
                //Columna Articulo redimensionar
                dgvArt.Columns[4].Width = 380;
                //Mostrar valor iva
                lblIVA.Visible = false;
                txtIVA.Visible = false;
                lblIVA10.Visible = false;
                txtIVA10.Visible = false;
                //Ocultar linea
                LineaIva.Visible = true;
                LineaIVA10.Visible = true;
            }

            // Si la grilla tiene artículos
            if (dgvArt.RowCount > 0)
            {
                // Almaceno én una variable la posición de fila en la que me encuentro
                int fila = dgvArt.CurrentRow.Index;
                // Pongo el foco de la fila en la columna cantidad
                dgvArt.CurrentCell = dgvArt["Cantidad", fila];

            }

            //Calcular SubTotal
            CalcularSubtotal();
            //Calcular Total
            CalcularTotal();
        }

        #endregion

        #region Metodo: EstablecerTipoComprobante

        //ESTABLECER EL TIPO DE COMPROBANTE EN FUNCION DEL TIPO DE CLIENTE ( IVA )
        private void EstablecerTipoComprobante()
        { 
            //Verificar
            if (Convert.ToInt32(this.cboTipoCliente.SelectedValue) == 1) // RESPONSABLE INSCRIPTO
            {
                cboComprobante.SelectedValue = 1; //A
            }
            else
            {
                cboComprobante.SelectedValue = 6; //B
            }

            string sClave = "";

            if (clsGlobales.cParametro.ModoFactura == 0)
            {
                sClave = "feafip";
            }
            else
            {
                sClave = "itvd";
            }

            //SOLO SI ESTA EN BLANCO
            if (wsfev1.login(Application.StartupPath + "\\" + clsGlobales.CertificadoAFIP, sClave))
            {
                if (wsfev1.recuperaLastCMP(Convert.ToInt32(nroPunto.Text), Convert.ToInt32(cboComprobante.SelectedValue), ref nroComprobante))
                {
                    nroComprobante += 1;
                    this.nroComp.Text = nroComprobante.ToString("D8");
                    wsfev1.reset();
                }
            }
            else
            {
                MessageBox.Show("Error al intentar la conexión con el Servidor de la AFIP. Reintente en unos instantes!.","Error!", MessageBoxButtons.OK,MessageBoxIcon.Error); 
                MessageBox.Show(wsfev1.ErrorDesc);
                this.Close();
            }
        }


        #endregion

        #region CargarToolTips

        private void CargarToolTips()
        {
            toolTip3.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip4.SetToolTip(this.btnSalir, "Salir!");
        }

        #endregion
       
        #region PasarDatosAlFormulario

        private void PasarDatosAlFormulario()
        {
            //Paso los datos del proveedor al formulario
            //Validar la CUIT                
            clsCUIT oCUITvalido = new clsCUIT(this.myCliente.Cuit);
            if (oCUITvalido.EsValido)
            {
                this.txtCuit.Text = clsGlobales.cFormato.CUITGuiones(this.myCliente.Cuit, 2);
            }
            else            
            {
                this.txtCuit.Text = this.myCliente.Cuit;
            }

            this.txtRazonSocial.Text = this.myCliente.RazonSocial;
            this.txtDir.Text = this.myCliente.Direccion + ", " + this.myCliente.Localidad + "," + this.myCliente.Provincia + " (" + this.myCliente.CP + ")";
            this.txtMail.Text = this.myCliente.Mail;
            cboTipoCliente.SelectedValue = this.myCliente.IdCondicionIva;
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

                myCliente.CP = rowCli["CP"].ToString();

                if (clsGlobales.cValida.EsFecha(rowCli["Alta"].ToString()))
                {
                    myCliente.Alta = rowCli["Alta"].ToString();
                }
                else { myCliente.Alta = ""; }


            }
        }

        #endregion

        #region Evento Boton Agregar Click

        private void btnAgregar_Click(object sender, EventArgs e)
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
            frmArticulosBuscar myForm = new frmArticulosBuscar(false, true);
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
                string myCadenaSQL = "select * from Articulos_Insumos_Productos where IdArticulo in (" + sArt + ")";
                // Creo una tabla que me va a almacenar el resultado de la consulta
                DataTable myTabla = clsDataBD.GetSql(myCadenaSQL);
                // Evito que el dgv genere columnas automáticas
                dgvArt.AutoGenerateColumns = false;
                // Asigno la tabla al source de la grilla de proveedores
                dgvArt.DataSource = myTabla;

                // Si la grilla tiene artículos
                if (dgvArt.RowCount > 0)
                {
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

        #endregion

        #region Evento Click Boton Aceptar

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //VALIDACIONES
            if (!(ValidaFactura()))
            {
                //Hubo errores, salir
                return;
            }
            else
            {
                //PREGUNTAR SI ESTA CONFIGURADO EN PARAMETROS
                if (clsGlobales.cParametro.Imprimir)
                {
                    DialogResult dlResult = MessageBox.Show("¿Desea Emitir el Comprobante?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    // Si confirma... cambiar estado
                    if (dlResult == DialogResult.No)
                    {
                        return;
                    }
                }
            }

            //VERIFICAR TIPO COMPROBANTE Y EN VIRTUD DE ESO PROCESAR FACTURA QUE CORRESPONDE
            if (Convert.ToInt32(cboComprobante.SelectedValue) == 1)
            {
                this.ProcesarFacturaA();
            }
            else
            {
                this.ProcesarFacturaB();
            }
            //
            
        }

        #endregion

        #region Metodo ProcesarFacturaA

        //METODO QUE PROCESA LA FACTURA A
        private void ProcesarFacturaA()
        {
            //SI ESTOY EN B/N Y ES PENDIENTE, SE FACTURA EN NEGRO DIRECTO
            if (clsGlobales.ConB != null && myPresu.Pendiente == 1)
            {
                //PRESUPUESTO NO SE PUEDE EDITAR, NO SALE PENDIENTE DE AQUI.
                //GUARDAR FACTURA
                GuardarFactura(1);

                //GUARDAR DETALLE FACTURA
                GuardarFacturaDet(1);

                //DESCONTAR STOCK
                DescuentaStock(1); // Si es nota de crédito, debe reponer el stock de los productos que devuelve

                //ACTUALIZA SALDO
                ActualizarSaldoCli(myCliente.Codigo, Convert.ToDouble(txtTotal.Text), 1);

                //ELIMINAR PRESUPUESTO Y DETALLE DEL BLANCO
                string sCad = "DELETE FROM Presupuestos WHERE IdPresupuesto = " + this.IdPresupuesto;
                clsDataBD.GetSql(sCad);

                sCad = "DELETE FROM DetallePresupuestos WHERE IdPresupuesto = " + this.IdPresupuesto;
                clsDataBD.GetSql(sCad);

                //imprimir presu
                this.ImprimePresu();

                //Cerrar
                this.Close();

            }
            else
            {
                //Parametros
                DateTime FechaComp = this.dtFechaComp.Value; //Fecha Comprobante

                //otros
                string cae = "";
                DateTime vencimiento = default(System.DateTime);
                string resultado = "";
                string sIva = "";


                //SI HAY DESCUENTO
                if (this.dto > 0)
                {
                    
                    //Agregar Factura
                    wsfev1.agregaFactura(1, 80, long.Parse(txtCuit.Text), Convert.ToInt32(nroComp.Text), Convert.ToInt32(nroComp.Text),
                    FechaComp, Convert.ToDouble(txtTotal.Text), 0, Convert.ToDouble(txtSubTotal.Text), Convert.ToDouble(txtExento.Text), null, null, null, "PES", 1);

                    //Agregar IVA 21 y 10.5
                    if (!(acumula21Dto == 0))
                    {
                        sIva = acumula21Dto.ToString("#0.00");
                        wsfev1.agregaIVA(5, Convert.ToDouble(sIva), Convert.ToDouble(txtIVA.Text));
                    }
                    if (!(acumula10Dto == 0))
                    {
                        sIva = acumula10Dto.ToString("#0.00");
                        wsfev1.agregaIVA(4, Convert.ToDouble(sIva), Convert.ToDouble(txtIVA10.Text));
                    }
                }
                else
                {

                    //Agregar Factura
                    wsfev1.agregaFactura(1, 80, long.Parse(txtCuit.Text), Convert.ToInt32(nroComp.Text),
                    Convert.ToInt32(nroComp.Text), FechaComp, Convert.ToDouble(txtTotal.Text), 0, Convert.ToDouble(txtSubTotal.Text), Convert.ToDouble(txtExento.Text), null, null, null, "PES", 1);

                    //Agregar IVA 21 y 10.5
                    if (!(acumula21 == 0))
                    {
                        sIva = acumula21.ToString("#0.00");
                        wsfev1.agregaIVA(5, Convert.ToDouble(sIva), Convert.ToDouble(txtIVA.Text));
                    }
                    if (!(acumula10 == 0))
                    {
                        sIva = acumula10.ToString("#0.00");
                        wsfev1.agregaIVA(4, Convert.ToDouble(sIva), Convert.ToDouble(txtIVA10.Text));
                    }
                }

                if (wsfev1.autorizar(Convert.ToInt32(this.nroPunto.Text), Convert.ToInt32(cboComprobante.SelectedValue)))
                {
                    //Autorizar respuesta AFIP
                    wsfev1.autorizarRespuesta(0, ref cae, ref vencimiento, ref resultado);
                    if (resultado == "A")
                    {
                        //Mostrar CACE, vencimiento y setear 'Resultado'
                        this.txtCAE.Text = cae;
                        this.dtVencCAE.Value = Convert.ToDateTime(vencimiento.ToString());
                        this.rdoOk.Checked = true;
                        this.rdoRech.Checked = false;

                        //GUARDAR FACTURA
                        GuardarFactura(0);

                        //GUARDAR DETALLE FACTURA
                        GuardarFacturaDet(0);

                        //MARCAR PRESUPUESTO COMO FACTURADO
                        setPresuFacturado();

                        //MENSAJE
                        MessageBox.Show("La Factura ha sido Autorizada por la AFIP! - CAE: " + cae + " - Vencimiento: " + vencimiento.ToShortDateString(), "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //DESCONTAR STOCK
                        DescuentaStock(0);

                        //IMPRIMIR LA FACTURA
                        ImprimirFactura();

                        //SI HAY DIFERENCIAS ENTRE LO PRESUPUESTADO Y FACTURADO, GENERAR PRESUPUESTO
                        //PENDIENTE PARA FACTURAR EN NEGRO                        
                        if (bCambioPresupuesto() == true)
                        {
                            //GENERAR PRESUPUESTO NUEVO
                            this.GuardarPresupuesto(this.myPresu);

                            //BORRAR EL PRESPUESTO ORIGINAL?
                            this.BorrarPresuOriginal();

                        }
                        //***************************************************************************

                        //ACTUALIZA SALDO
                        ActualizarSaldoCli(myCliente.Codigo, Convert.ToDouble(txtTotal.Text));

                        //CERRAR
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(wsfev1.autorizarRespuestaObs(0), "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show(wsfev1.ErrorDesc, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Metodo: GuardarDetallePresu

        //GUARDA EL DETALLE DEL PRESPUESTO EN UNO NUEVO PENDIENTE
        private void GuardarDetallePresu(int p_IdPresupuesto)
        {
            // Armo la cadena SQL para ejecutar el procedieminto almacenado que trae el detalle del comprobante
            string newMyCadenaSql = "Select * from Vista_Detalle_Presu_ABM where IdPresupuesto = " + IdPresupuesto;
            // Creo un datatable y le paso los datos de la consulta SQL
            DataTable myTabla = clsDataBD.GetSql(newMyCadenaSql);
            
            clsDetallePresupuestos myNvoDet = new clsDetallePresupuestos();

            double Cantidad = 0;
            double nvaCantidad = 0;
            double CantidadOriginal = 0;
            int iOrdenNuevo = 0;

            //RECORRER LA GRILLA
            foreach (DataGridViewRow row in dgvArt.Rows)
            {
                //Cantidad en la grilla
                Cantidad = Convert.ToDouble(row.Cells["Cantidad"].Value.ToString());
                CantidadOriginal = Convert.ToDouble(row.Cells["CantOriginal"].Value.ToString());

                //Nueva Cantidad
                nvaCantidad = CantidadOriginal - Cantidad; 

                //VERIFICAR SI SE MODIFICO O NO LA CANTIDAD PARA EL PRODUCTO ACTUAL
                if (Cantidad <  CantidadOriginal)
                {
                  //CARGAR OBJETO
                   myNvoDet.IdDetPresupuesto = clsDataBD.RetornarUltimoId("DetallePresupuestos","IdDetPresupuesto")+1;
                   myNvoDet.IdPresupuesto = p_IdPresupuesto;
                   myNvoDet.IdArticulo = Convert.ToInt32(row.Cells["IdArticulo"].Value.ToString());
                   myNvoDet.Codigo_Articulo = row.Cells["CodigoArticulo"].Value.ToString();
                   myNvoDet.Cantidad = nvaCantidad;
                   myNvoDet.Descripcion = row.Cells["Articulo"].Value.ToString();
                    
                   myNvoDet.PrecioUnitario = Convert.ToDouble(row.Cells["Precio"].Value.ToString());

                   myNvoDet.Activo = 1;
                   myNvoDet.Excel = Convert.ToInt32(row.Cells["Excel"].Value.ToString());
                   iOrdenNuevo++;
                   myNvoDet.Orden = iOrdenNuevo;
                   
                   //PASAR EL OBJETO NUEVO Y GUARDAR EN LA TABLA
                   this.GuardarDetallePS(myNvoDet);

                   //UPDATE PRESUPUESTO FACTURADO EN CUANTO A CANTIDAD
                   newMyCadenaSql = "UPDATE DetallePresupuestos SET Cantidad = " + Cantidad + " WHERE IdPresupuesto = " + this.IdPresupuesto + " AND IdArticulo = " + myNvoDet.IdArticulo;
                   // Creo un datatable y le paso los datos de la consulta SQL
                   clsDataBD.GetSql(newMyCadenaSql);

                }
            }
        }

        #endregion

        #region Metodo: GuardarDetallePS

        private void GuardarDetallePS(clsDetallePresupuestos pDetPresu)
        {

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
                                                       " Excel," + 
                                                       " Orden)" +
                                                       "  values (" + pDetPresu.IdDetPresupuesto + "," +
                                                                         pDetPresu.IdPresupuesto + "," +
                                                                         pDetPresu.IdArticulo + ",'" +
                                                                         pDetPresu.Codigo_Articulo + "'," +
                                                                         pDetPresu.Cantidad.ToString().Replace(",", ".") + ",'" +
                                                                         pDetPresu.Descripcion + "'," +
                                                                         pDetPresu.PrecioUnitario.ToString().Replace(",", ".") + "," +
                                                                         pDetPresu.Activo + "," +
                                                                         pDetPresu.Excel + "," +
                                                                         pDetPresu.Orden + ")";


            clsDataBD.GetSql(myCadSQL);


        }

        #endregion

        #region Metodo: GuardarPresupuesto

        //Guarda un presupuesto que recibe como parametro
        private void GuardarPresupuesto(clsPresupuestos pPresupuesto)
        {

            //INSERT A LA TABLA DE PEDIDOS
            string myCadena = "";

            try
            {
                pPresupuesto.IdPresupuesto = clsDataBD.RetornarUltimoId("Presupuestos", "IdPresupuesto") + 1;
                // ** 29/05
                // myPresu.Nro = clsDataBD.RetornarMax("Presupuestos","Nro")+1;
                // **
                pPresupuesto.Nro = clsDataBD.getUltComp("Ult_Presupuesto", clsGlobales.cParametro.PtoVtaPorDefecto, 0) + 1; //
                // **
                pPresupuesto.PuntoNro = pPresupuesto.Punto.ToString("D4") + "-" + myPresu.Nro.ToString("D8");
                pPresupuesto.Pendiente = 1;
                pPresupuesto.Fecha = DateTime.Now;
                pPresupuesto.Activo = 1;

                //Alta de Articulos
                myCadena = "INSERT INTO Presupuestos (IdPresupuesto," +
                                                " IdCliente," +
                                                " IdFormaPago," +
                                                " Punto," +
                                                " Nro," +
                                                " PuntoNro," +
                                                " IdTransporte," +
                                                " Fecha," +
                                                " Comentario," +
                                                " Dto," +
                                                " Flete," +
                                                " Activo," +
                                                " Facturado," +
                                                " Excel," +
                                                " Pendiente," +
                                                " Codigo_Correo" +
                                                 ") values (" + pPresupuesto.IdPresupuesto + ","
                                                                + pPresupuesto.IdCliente + ","
                                                                + pPresupuesto.IdFormaPago + ","
                                                                + pPresupuesto.Punto + ","
                                                                + pPresupuesto.Nro + ",'"
                                                                + pPresupuesto.PuntoNro + "',"
                                                                + pPresupuesto.IdTransporte + ",'"
                                                                + pPresupuesto.Fecha.ToShortDateString() + "','"
                                                                + pPresupuesto.Comentario + "',"
                                                                + pPresupuesto.Dto.ToString().Replace(",", ".") + ","
                                                                + pPresupuesto.Flete.ToString().Replace(",", ".") + ","
                                                                + pPresupuesto.Activo + ","
                                                                + pPresupuesto.Facturado + ","
                                                                + pPresupuesto.Excel + "," 
                                                                + pPresupuesto.Pendiente + ", '"
                                                                + pPresupuesto.Codigo_Correo + "')";

                //GUARDAR EN PRESUPUESTOS
                clsDataBD.GetSql(myCadena);

                //Actualizar el numero de presupuesto en Tabla AFIP
                string mySQL = "UPDATE PuntosVentaAFIP SET Ult_Presupuesto = " + pPresupuesto.Nro + " WHERE Punto = " + clsGlobales.cParametro.PtoVtaPorDefecto;
                clsDataBD.GetSql(mySQL);

                //DETALLE
                GuardarDetallePresu(pPresupuesto.IdPresupuesto);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        #endregion

        #region Metodo CambioPresupuesto

        //VERIFICA SI SE CAMBIARON LAS CANTIDADES
        private bool bCambioPresupuesto()
        {
            //FLAG
            bool bCambio = false;

            //RECORRER LA GRILLA
            foreach (DataGridViewRow row in dgvArt.Rows)
            {
               //SE MODIFICO ALGUNA CANTIDAD?.... 
               if (Convert.ToDouble(row.Cells["Cantidad"].Value) < Convert.ToDouble(row.Cells["CantOriginal"].Value))
               {
                   //.T. y SALIR DEL FOR EACH
                   bCambio = true;
                   //SALE
                   break;
               }
            }

            //RETORNAR .F. O .T.
            return bCambio;

        }

        #endregion

        #region Metodo ProcesarFacturaB

        //METODO QUE PROCESA LA FACTURA B
        private void ProcesarFacturaB()
        {

            //SI ESTOY EN B/N Y ES PENDIENTE, SE FACTURA EN NEGRO DIRECTO
            if (clsGlobales.ConB != null && myPresu.Pendiente==1)
            {
                //PRESUPUESTO NO SE PUEDE EDITAR, NO SALE PENDIENTE DE AQUI.
                //GUARDAR FACTURA
                GuardarFactura(1);

                //GUARDAR DETALLE FACTURA
                GuardarFacturaDet(1);

                //DESCONTAR STOCK
                DescuentaStock(1);

                //imprimir presu
                this.ImprimePresu();

                //ACTUALIZA SALDO
                ActualizarSaldoCli(myCliente.Codigo, Convert.ToDouble(txtTotal.Text), 1);

                //ELIMINAR PRESUPUESTO Y DETALLE DEL BLANCO
                string sCad = "DELETE FROM Presupuestos WHERE IdPresupuesto = " + this.IdPresupuesto;
                clsDataBD.GetSql(sCad);

                sCad = "DELETE FROM DetallePresupuestos WHERE IdPresupuesto = " + this.IdPresupuesto;
                clsDataBD.GetSql(sCad);

                //Cerrar
                this.Close();

            }
            else
            {
                //Parametros
                DateTime FechaComp = dtFechaComp.Value; //Fecha Comprobante
            
                //otros
                string cae = "";
                DateTime vencimiento = default(System.DateTime);
                string resultado = "";
                string bImpo = (Convert.ToDouble(txtSubTotal.Text) - Convert.ToDouble(txtIVA.Text)).ToString("#0.00");
                string sNeto = "";
                string sIva = "";

                //SI HAY DESCUENTO
                if (this.dto > 0)
                {
                    sNeto = (acumula21Dto + acumula10Dto).ToString("#0.00");

                    //Agregar Factura
                    wsfev1.agregaFactura(1, 80, long.Parse(txtCuit.Text), Convert.ToInt32(nroComp.Text), Convert.ToInt32(nroComp.Text),
                    FechaComp, Convert.ToDouble(txtTotal.Text), 0, Convert.ToDouble(sNeto), Convert.ToDouble(txtExento.Text), null, null, null, "PES", 1);

                    //Agregar IVA 21 y 10.5
                    if (!(acumula21Dto == 0))
                    {
                        sIva = acumula21Dto.ToString("#0.00");
                        wsfev1.agregaIVA(5, Convert.ToDouble(sIva), Convert.ToDouble(txtIVA.Text));
                    }
                    if (!(acumula10Dto == 0))
                    {
                        sIva = acumula10Dto.ToString("#0.00");
                        wsfev1.agregaIVA(4, Convert.ToDouble(sIva), Convert.ToDouble(txtIVA10.Text));
                    }
                }
                else
                {
                    sNeto = (acumula21 + acumula10).ToString("#0.00");

                    //Agregar Factura
                    wsfev1.agregaFactura(1, 80, long.Parse(txtCuit.Text), Convert.ToInt32(nroComp.Text),
                    Convert.ToInt32(nroComp.Text), FechaComp, Convert.ToDouble(txtTotal.Text), 0, Convert.ToDouble(sNeto), Convert.ToDouble(txtExento.Text), null, null, null, "PES", 1);

                    //Agregar IVA 21 y 10.5
                    if (!(acumula21 == 0))
                    {
                        sIva = acumula21.ToString("#0.00");
                        wsfev1.agregaIVA(5, Convert.ToDouble(sIva), Convert.ToDouble(txtIVA.Text));
                    }
                    if (!(acumula10 == 0))
                    {
                        sIva = acumula10.ToString("#0.00");
                        wsfev1.agregaIVA(4, Convert.ToDouble(sIva), Convert.ToDouble(txtIVA10.Text));
                    }
                }

                if (wsfev1.autorizar(Convert.ToInt32(this.nroPunto.Text), Convert.ToInt32(cboComprobante.SelectedValue)))
                {
                    //Autorizar respuesta AFIP
                    wsfev1.autorizarRespuesta(0, ref cae, ref vencimiento, ref resultado);
                    if (resultado == "A")
                    {
                        //Mostrar CACE, vencimiento y setear 'Resultado'
                        this.txtCAE.Text = cae;
                        this.dtVencCAE.Value = Convert.ToDateTime(vencimiento.ToString());
                        this.rdoOk.Checked = true;
                        this.rdoRech.Checked = false;

                        //GUARDAR FACTURA
                        GuardarFactura(0);

                        //GUARDAR DETALLE FACTURA
                        GuardarFacturaDet(0);

                        //MARCAR PRESUPUESTO COMO FACTURADO
                        setPresuFacturado();

                        //MENSAJE
                        MessageBox.Show("La Factura ha sido Autorizada por la AFIP! - CAE y Vencimiento: " + cae + " " + vencimiento.ToShortDateString(), "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //DESCONTAR STOCK
                        DescuentaStock(0);

                        //IMPRIMIR LA FACTURA
                        ImprimirFactura();

                        //SI HAY DIFERENCIAS ENTRE LO PRESUPUESTADO Y FACTURADO, GENERAR PRESUPUESTO
                        //PENDIENTE PARA FACTURAR EN NEGRO                        
                        if (bCambioPresupuesto() == true)
                        {
                            //GENERAR PRESUPUESTO NUEVO
                            this.GuardarPresupuesto(this.myPresu);

                            //BORRAR EL PRESPUESTO ORIGINAL?
                            this.BorrarPresuOriginal();
    
                            //
                        }
                        //***************************************************************************

                        //ACTUALIZA SALDO
                        ActualizarSaldoCli(myCliente.Codigo, Convert.ToDouble(txtTotal.Text));

                        //Cerrar
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(wsfev1.autorizarRespuestaObs(0), "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show(wsfev1.ErrorDesc, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }   
        }

        #endregion

        #region Metodo BorrarPresuOriginal

        //ELIMINA EL PRESUPUESTO ORIGINAL LUEGO DE HABERSE GENERADO UN PENDIENTE
        private void BorrarPresuOriginal()
        {

            //DELETE PRESUPUESTO
            string myCad = "DELETE FROM Presupuestos WHERE IdPresupuesto = " + this.IdPresupuesto;
            clsDataBD.GetSql(myCad);

            //DELETE DETALLE
            myCad = "DELETE FROM DetallePresupuestos WHERE IdPresupuesto = " + this.IdPresupuesto;
            clsDataBD.GetSql(myCad);

        }

        #endregion

        #region Metodo Imprimir Presupuesto

        private void ImprimePresu()
        {
            int dgvFilas = 0;

            //Data Set
            dsReportes oDsFactura = new dsReportes();

            //FACTURA A
            if (Convert.ToInt32(cboComprobante.SelectedValue) == 1)
            {

                //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
                dgvFilas = dgvArt.Rows.Count;

                for (int i = 0; i < dgvFilas; i++)
                {
                    oDsFactura.Tables["dtFacturaVenta"].Rows.Add
                    (new object[] { 
                    dgvArt[2,i].Value.ToString(),
                    dgvArt[4,i].Value.ToString(),
                    dgvArt[6,i].Value.ToString(),
                    dgvArt[5,i].Value.ToString(),
                    dgvArt[9,i].Value.ToString(),
                    dgvArt[8,i].Value.ToString(),
                    dgvArt[11,i].Value.ToString(),
                    dgvArt[12,i].Value.ToString(),
                    dgvArt[15,i].Value.ToString()});

                }

            }
            else
            {

                //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
                dgvFilas = dgvArt.Rows.Count;

                for (int i = 0; i < dgvFilas; i++)
                {
                    oDsFactura.Tables["dtFacturaVenta"].Rows.Add
                    (new object[] { 
                    dgvArt[2,i].Value.ToString(),
                    dgvArt[4,i].Value.ToString(),
                    dgvArt[6,i].Value.ToString(),
                    dgvArt[5,i].Value.ToString(),
                    dgvArt[10,i].Value.ToString(),
                    "",
                    "",
                    "",
                    (Convert.ToDouble(dgvArt[10,i].Value.ToString()) * Convert.ToDouble(dgvArt[6,i].Value.ToString())).ToString("#0.00")});

                }

            }

            //Factura A
            if (Convert.ToInt32(cboComprobante.SelectedValue) == 1)
            {
                //Objeto Reporte
                rptPedPresuC oRepFacturaVta = new rptPedPresuC();

                //Cargar Reporte                                    
                oRepFacturaVta.Load(Application.StartupPath + "\\rptPedPresu(C).rpt");

                //Tipo Comprobante
                oRepFacturaVta.DataDefinition.FormulaFields["TipoComprobante"].Text = "'" + "X" + "'";
                oRepFacturaVta.DataDefinition.FormulaFields["CodigoComp"].Text = "'" + " " + "'";

                //Presupuesto
                oRepFacturaVta.DataDefinition.FormulaFields["DescComp"].Text = "'" + " Presupuesto N°: " + "'";
               
                //TRANSPORTE
                oRepFacturaVta.DataDefinition.FormulaFields["Transporte"].Text = "'" + ObtenerTransporte(myPresu.IdTransporte) + "'";


                //CARGAR FORMULAS Y MOSTRAR REPORTE
                ShowPedPresuA(oRepFacturaVta, oDsFactura);

            } //FActura B
            else
            {

                //Objeto Reporte
                rptPedPresuD oRepFacturaVta = new rptPedPresuD();

                //Cargar Reporte                                    
                oRepFacturaVta.Load(Application.StartupPath + "\\rptFacturaVtaB.rpt");

                oRepFacturaVta.DataDefinition.FormulaFields["TipoComprobante"].Text = "'" + "X" + "'";
                oRepFacturaVta.DataDefinition.FormulaFields["CodigoComp"].Text = "'" + " " + "'";

                //TRANSPORTE
                oRepFacturaVta.DataDefinition.FormulaFields["Transporte"].Text = "'" + ObtenerTransporte(myPresu.IdTransporte) + "'";

                //Presupuesto
                oRepFacturaVta.DataDefinition.FormulaFields["DescComp"].Text = "'" + " Presupuesto N°: " + "'"; 

                //CARGAR FORMULAS Y MOSTRAR REPORTE
                ShowPedPresuB(oRepFacturaVta, oDsFactura);

            }

        }

        #endregion

        #region Metodo ObtenerTransporte

        private string ObtenerTransporte(int p_IdTransporte)
        {
            string sTransporte = "";

            string myCad = "Select * from Transportes WHERE IdTransporte = " + p_IdTransporte;
            DataTable myDataVal = clsDataBD.GetSql(myCad);

            foreach (DataRow row in myDataVal.Rows)
            {
                sTransporte = row["RazonSocial"].ToString();
                break;
            }

            //Retornar valor obtenido
            return sTransporte;
        }

        #endregion

        #region Metodo GuardarFactura

        //Guarda la factura autorizada con CAE y Vencimiento de CAE
        private void GuardarFactura(int p_Con)
        {
           clsEFactura myFact = new clsEFactura();

            myFact.CUIT = txtCuit.Text;
            myFact.CAE = txtCAE.Text;
            myFact.Punto = Convert.ToInt32(nroPunto.Text);
            
            //Verificar
            if (p_Con == 0)
            {
                myFact.Nro = Convert.ToInt32(nroComp.Text);
                myFact.PuntoNro = myFact.Punto.ToString("D4") + "-" + myFact.Nro.ToString("D8");
            }
            else
            {
                myFact.Nro = myPresu.Nro;
                myFact.PuntoNro = myFact.Punto.ToString("D4") + "-" + myPresu.Nro.ToString("D8");
            }

            myFact.Fecha = dtFechaComp.Value.Date;
            myFact.FechaVencPago = dtVtoPago.Value.Date;
            myFact.VecCAE = dtVencCAE.Value.Date;
            myFact.Resultado = 1;
            myFact.Total = Convert.ToDouble(txtTotal.Text);
            myFact.IVA21 = Convert.ToDouble(txtIVA.Text);
            myFact.IVA10 = Convert.ToDouble(txtIVA10.Text);
            myFact.Subtotal = Convert.ToDouble(txtSubTotal.Text);
            myFact.Exento = Convert.ToDouble(txtExento.Text);
            myFact.Neto = Convert.ToDouble(txtNeto.Text);
            myFact.IdTransporte = myPresu.IdTransporte;
            myFact.Dto = myPresu.Dto;
            myFact.Flete = myPresu.Flete;

            myFact.Saldo = myFact.Total;
                        
            if (chkProd.Checked) { myFact.IncluyeProd = 1; } else { myFact.IncluyeProd = 0; }
            if (chkServ.Checked) { myFact.IncluyeServ = 1;} else { myFact.IncluyeServ = 0;}

            myFact.IdTipoComprobante = Convert.ToInt32(cboComprobante.SelectedValue);
            myFact.IdFormaPago = Convert.ToInt32(cboFormaPago.SelectedValue);
            if (p_Con == 0)
            {
                myFact.IdFactura = clsDataBD.RetornarUltimoId("eFactura", "IdFactura") + 1;
            }
            else
            {
                myFact.IdFactura = clsDataBD.RetornarUltimoIdB("eFactura", "IdFactura") + 1;
            }
            myFact.IdCliente = this.IdCliente;
            myFact.FechaVencPago = dtFechaComp.Value;

            //NETO IVA VENTA!
            if (myFact.IdTipoComprobante == 6) // Factura B
            {
               myFact.NetoIvaVta = myFact.Total - (myFact.IVA21 + myFact.IVA10 + myFact.Exento);
            }
            else //Factura A
            {
                myFact.NetoIvaVta = myFact.Neto;
            }

            myFact.Codigo_Correo = txtCodigoCorreo.Text;

            //Guardar la factura
            string myCadSQL = "INSERT INTO eFactura (IdFactura," +
                                                    " Fecha, " +
                                                    " IdTipoComprobante," +
                                                    " Punto," +
                                                    " Nro," +
                                                    " PuntoNro," +
                                                    " IdFormaPago," +
                                                    " FechaVencPago," +
                                                    " IdCliente," +
                                                    " CUIT," +
                                                    " IncluyeProd," +
                                                    " IncluyeServ," +
                                                    " CAE," +
                                                    " VecCAE," +
                                                    " Resultado," +
                                                    " OtrosTributos," +
                                                    " IdTransporte," +
                                                    " Neto, " + 
                                                    " Dto," + 
                                                    " Flete, " +
                                                    " SubTotal, " + 
                                                    " Exento," + 
                                                    " IVA21," + 
                                                    " IVA10," + 
                                                    " Total," +
                                                    " NetoIvaVta," +
                                                    " Saldo," +
                                                    " Codigo_Correo" + ") values (" + myFact.IdFactura + ",'" +
                                                                         myFact.Fecha.ToShortDateString() + "'," +
                                                                         myFact.IdTipoComprobante + "," +
                                                                         myFact.Punto + "," +
                                                                         myFact.Nro + ",'" +
                                                                         myFact.PuntoNro + "'," +
                                                                         myFact.IdFormaPago + ",'" +
                                                                         myFact.FechaVencPago.ToShortDateString() + "'," +
                                                                         myFact.IdCliente + ",'" +
                                                                         myFact.CUIT + "'," +
                                                                         myFact.IncluyeProd + "," +
                                                                         myFact.IncluyeServ + ",'" +
                                                                         myFact.CAE + "','" +
                                                                         myFact.VecCAE.ToShortDateString() + "'," +
                                                                         myFact.Resultado + "," +
                                                                         myFact.OtrosTributos.ToString().Replace(",", ".") + "," +
                                                                         myFact.IdTransporte + "," +
                                                                         myFact.Neto.ToString().Replace(",", ".") + "," +
                                                                         myFact.Dto.ToString().Replace(",", ".") + "," +
                                                                         myFact.Flete.ToString().Replace(",", ".") + "," + 
                                                                         myFact.Subtotal.ToString().Replace(",", ".") + "," +
                                                                         myFact.Exento.ToString().Replace(",", ".") + "," + 
                                                                         myFact.IVA21.ToString().Replace(",", ".") + "," +
                                                                         myFact.IVA10.ToString().Replace(",", ".") + "," +
                                                                         myFact.Total.ToString().Replace(",", ".") + "," + 
                                                                         myFact.NetoIvaVta.ToString().Replace(",",".") + "," + 
                                                                         myFact.Saldo.ToString().Replace(",", ".") + ",'" + 
                                                                         myFact.Codigo_Correo + "')";

            //Verificar
            if (p_Con == 0)
            {
               clsDataBD.GetSql(myCadSQL);
            }
            else
            {
               clsDataBD.GetSqlB(myCadSQL);
            }
        }

        #endregion

        #region DescuentaStock

        //Descuenta stock en los productos
        private void DescuentaStock(int p_Con)
        {

            string myCadSQL = "";

             //Recorrer la grilla
            foreach (DataGridViewRow row in dgvArt.Rows)
            {
                //Updatear stock en articulos
                myCadSQL = "Update Articulos Set Stock = Stock - " + Convert.ToDouble(row.Cells["Cantidad"].Value) + " where IdArticulo = " + Convert.ToInt32(row.Cells["IdArticulo"].Value);
                if (p_Con == 0)
                {
                    clsDataBD.GetSql(myCadSQL);
                   
                    //Grabar el movimiento del stock
                    GrabarMovimientoStock(Convert.ToInt32(row.Cells["IdArticulo"].Value), 4, dtFechaComp.Value.Date, Convert.ToDouble(row.Cells["Cantidad"].Value),0);
                }
                else
                {
                    clsDataBD.GetSql(myCadSQL);
                    //Grabar el movimiento del stock
                    GrabarMovimientoStock(Convert.ToInt32(row.Cells["IdArticulo"].Value), 4, dtFechaComp.Value.Date, Convert.ToDouble(row.Cells["Cantidad"].Value), 1);
                }

            }

        }

        #endregion

        #region Método que graba el movimiento de Stock en la tabla StockMovimientos

        private void GrabarMovimientoStock(int IdArt, int IdMot, DateTime Fec, double Cant, int p_Con)
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
            if (p_Con == 0)
            {
                clsDataBD.GetSql(myCadenaSql);
            }
            else
            {
                clsDataBD.GetSqlB(myCadenaSql);
            }
        }

        #endregion

        #region Metodo: setPresuFacturado

        //Establece a .T. la marca de Facturado para el presupuesto
        private void setPresuFacturado()
        {

            string myCadSQL = "";
            myCadSQL = "Update Presupuestos Set Facturado = 1 WHERE IdPresupuesto = " + IdPresupuesto;
            clsDataBD.GetSql(myCadSQL);

        }

        #endregion

        #region Metodo GuardarFacturaDet

        //GUARDA EL DETALLE DE LA FACTURA EN LA TABLE eFacturaDetalle

        private void GuardarFacturaDet(int p_Con)
        {
            //Obtener Ultimo Id Factura y Nuevo Id Factura Detalle
          //int LastIdFactura = clsDataBD.RetornarUltimoId("eFactura", "IdFactura");
            int LastIdFactura = 0;
            int LastIdDetalle = 0;

            clsEFacturaDet myFactDet = new clsEFacturaDet();

            if (p_Con == 0)
            {
                LastIdFactura = clsDataBD.RetornarUltimoId("eFactura", "IdFactura");
            }
            else
            {
                LastIdFactura= clsDataBD.RetornarUltimoIdB("eFactura", "IdFactura");
            }

            //Recorrer la grilla
            foreach (DataGridViewRow row in dgvArt.Rows)
            {
                //Nuevo IdDetalle
                if (p_Con == 0)
                {
                    LastIdDetalle = clsDataBD.RetornarUltimoId("eFacturaDetalle", "IdFacturaDetalle") + 1;
                }
                else
                {
                    LastIdDetalle = clsDataBD.RetornarUltimoIdB("eFacturaDetalle", "IdFacturaDetalle") + 1;
                }
                //IdArticulo Clave Principal y relacion con factura
                myFactDet.IdFacturaDetalle = LastIdDetalle;
                myFactDet.IdFactura = LastIdFactura;

                //Datos del producto
                myFactDet.Cantidad = Convert.ToDouble(row.Cells["Cantidad"].Value);
                myFactDet.Alicuota = Convert.ToDouble(row.Cells["Alicuota"].Value);
                myFactDet.IdArticulo = Convert.ToInt32(row.Cells["IdArticulo"].Value);
                myFactDet.IdProducto = Convert.ToInt32(row.Cells["IdPropio"].Value);
                if (Convert.ToInt32(cboComprobante.SelectedValue) == 1)
                {
                   myFactDet.Precio = Convert.ToDouble(row.Cells["PrecioCoef"].Value);
                   myFactDet.Subtotal = Convert.ToDouble(row.Cells["SubTotalIva"].Value);
                }
                else
                {
                   myFactDet.Precio = Convert.ToDouble(row.Cells["PrecioConIva"].Value);
                   myFactDet.Subtotal = Convert.ToDouble(row.Cells["SubTotal"].Value);
                }
                myFactDet.Dto = Convert.ToDouble(row.Cells["Dsto"].Value);
                myFactDet.SubTotalDto = Convert.ToDouble(row.Cells["SubTotalDto"].Value);

                myFactDet.IVA = Convert.ToDouble(row.Cells["IVA"].Value);



                //Cargar el producto en la tabla
                //Guardar la factura
                string myCadSQL = "INSERT INTO eFacturaDetalle (IdFacturaDetalle," +
                                                            " IdFactura, " +
                                                            " Cantidad," +
                                                            " Alicuota," +
                                                            " IdArticulo," +
                                                            " IdProducto," +
                                                            " Precio," +
                                                            " Dto," +
                                                            " SubTotalDto," + 
                                                            " IVA," +
                                                            " Subtotal)" +
                                                            " values (" + myFactDet.IdFacturaDetalle + "," +
                                                                           myFactDet.IdFactura + "," +
                                                                           myFactDet.Cantidad.ToString().Replace(",", ".") + "," +
                                                                           myFactDet.Alicuota + "," +
                                                                           myFactDet.IdArticulo + "," +
                                                                           myFactDet.IdProducto + "," +
                                                                           myFactDet.Precio.ToString().Replace(",", ".") + "," +
                                                                           myFactDet.Dto.ToString().Replace(",", ".") + "," +
                                                                           myFactDet.SubTotalDto.ToString().Replace(",", ".") + "," +
                                                                           myFactDet.IVA.ToString().Replace(",", ".") + "," +
                                                                           myFactDet.Subtotal.ToString().Replace(",", ".") + ")";

                //VERIFICAR
                if (p_Con == 0)
                {
                    clsDataBD.GetSql(myCadSQL);
                }
                else
                {
                    clsDataBD.GetSqlB(myCadSQL);
                }

            }
        }

        #endregion
        
        #region Metodo: ImprimirFactura

        private void ImprimirFactura()
        {
            // capturo la posición de la fila
            clsGlobales.indexFila = this.dgvArt.CurrentRow.Index;

            int dgvFilas = 0;
                    
            //Data Set
            dsReportes oDsFactura = new dsReportes();

            //FACTURA A
            if (Convert.ToInt32(cboComprobante.SelectedValue) == 1)
            {


                //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
                dgvFilas = dgvArt.Rows.Count;

                //for (int i = 0; i < dgvFilas; i++)
                //{
                //    oDsFactura.Tables["dtFacturaVenta"].Rows.Add
                //    (new object[] { dgvArt[4,i].Value.ToString(),
                //    dgvArt[6,i].Value.ToString(),
                //    dgvArt[5,i].Value.ToString(),
                //    dgvArt[8,i].Value.ToString(),
                //    dgvArt[10,i].Value.ToString(),
                //    dgvArt[11,i].Value.ToString(),
                //    dgvArt[12,i].Value.ToString(),
                //    dgvArt[15,i].Value.ToString()});

                //}


                for (int i = 0; i < dgvFilas; i++)
                {
                    oDsFactura.Tables["dtFacturaVenta"].Rows.Add
                    (new object[] { 
                    dgvArt[2,i].Value.ToString(),
                    dgvArt[4,i].Value.ToString(),
                    dgvArt[6,i].Value.ToString(),
                    dgvArt[5,i].Value.ToString(),
                    dgvArt[9,i].Value.ToString(),
                    dgvArt[8,i].Value.ToString(),
                    dgvArt[11,i].Value.ToString(),
                    dgvArt[12,i].Value.ToString(),
                    dgvArt[15,i].Value.ToString()});

                }

            }
            else
            {

                //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
                dgvFilas = dgvArt.Rows.Count;

                for (int i = 0; i < dgvFilas; i++)
                {
                    oDsFactura.Tables["dtFacturaVenta"].Rows.Add
                    (new object[] { 
                    dgvArt[2,i].Value.ToString(),
                    dgvArt[4,i].Value.ToString(),
                    dgvArt[6,i].Value.ToString(),
                    dgvArt[5,i].Value.ToString(),
                    dgvArt[10,i].Value.ToString(),
                    "",
                    "",
                    "",
                    dgvArt[14,i].Value.ToString()});

                }

            }
            //ELIMINAR ARCHIVO
            BorrarArchivo(@"C:\Temp\AFIP.jpg");
            //Factura A
            if (Convert.ToInt32(cboComprobante.SelectedValue) == 1)
            {
                //Objeto Reporte
                rptFacturaVta oRepFacturaVta = new rptFacturaVta();

                //Cargar Reporte                                    
                oRepFacturaVta.Load(Application.StartupPath + "\\rptFacturaVta.rpt");

                //Tipo Comprobante
                oRepFacturaVta.DataDefinition.FormulaFields["TipoComprobante"].Text = "'" + "A" + "'";
                oRepFacturaVta.DataDefinition.FormulaFields["CodigoComp"].Text = "'" + "COD. 01" + "'";

                //CARGAR FORMULAS Y MOSTRAR REPORTE
                ShowReportA(oRepFacturaVta, oDsFactura);

            } //FActura B
            else
            {

                //Objeto Reporte
                rptFacturaVtaB oRepFacturaVta = new rptFacturaVtaB();

                //Cargar Reporte                                    
                oRepFacturaVta.Load(Application.StartupPath + "\\rptFacturaVtaB.rpt");

                oRepFacturaVta.DataDefinition.FormulaFields["TipoComprobante"].Text = "'" + "B" + "'";
                oRepFacturaVta.DataDefinition.FormulaFields["CodigoComp"].Text = "'" + "COD. 06" + "'";

                //CARGAR FORMULAS Y MOSTRAR REPORTE
                ShowReportB(oRepFacturaVta, oDsFactura);

            }

            //ELIMINAR ARCHIVO
            BorrarArchivo(@"C:\Temp\AFIP.jpg");
        }

        #endregion

        #region Metodo ShowReportA

        //METODO ShowReportB, para mostrar la Factura A unicamente.
        private void ShowReportA(rptFacturaVta oRepFacturaVta, dsReportes oDsFactura)
        {
            //Establecer el DataSet como DataSource
            oRepFacturaVta.SetDataSource(oDsFactura);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepFacturaVta;

            oRepFacturaVta.DataDefinition.FormulaFields["NroComp"].Text = "'" + this.nroPunto.Text + "-" + nroComp.Text + "'";

            //Otras
            oRepFacturaVta.DataDefinition.FormulaFields["Fecha"].Text = "'" + dtFechaComp.Value.ToString("dd/MM/yyyy") + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["RazonSocial"].Text = "'" + txtRazonSocial.Text.ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["Domic"].Text = "'" + (txtDir.Text).ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["IVA"].Text = "'" + cboTipoCliente.Text.ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["CondicionVenta"].Text = "'" + cboFormaPago.Text.ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["CUIT"].Text = "'" + txtCuit.Text + "'";

            oRepFacturaVta.DataDefinition.FormulaFields["CAE"].Text = "'" + txtCAE.Text + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["VencCAE"].Text = "'" + dtVencCAE.Value.ToString("dd/MM/yyyy") + "'";

            double dtoImpo = (Convert.ToDouble(txtNeto.Text) * Convert.ToDouble(txtDto.Text)) / 100;
            oRepFacturaVta.DataDefinition.FormulaFields["Dto"].Text = "'" + txtDto.Text + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["DtoImpo"].Text = "'" + dtoImpo.ToString("#0.00") + "'";

            //SUBTOTAL            
            oRepFacturaVta.DataDefinition.FormulaFields["Neto"].Text = "'" + txtNeto.Text + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["Subtotal"].Text = "'" + txtSubTotal.Text + "'";

            //MUESTRO LOS 2 IVA ( 21 Y 10.5)
            if (Convert.ToInt32(cboComprobante.SelectedValue) == 1)
            {
                oRepFacturaVta.DataDefinition.FormulaFields["IVA10"].Text = "'" + txtIVA10.Text + "'";
                oRepFacturaVta.DataDefinition.FormulaFields["TotIVA"].Text = "'" + txtIVA.Text + "'";
            }


            //FLETE Y EXENTO
            oRepFacturaVta.DataDefinition.FormulaFields["Flete"].Text = "'" + txtFlete.Text + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["Exento"].Text = "'" + txtExento.Text + "'";
            
            //TOTAL
            oRepFacturaVta.DataDefinition.FormulaFields["Total"].Text = "'" + txtTotal.Text + "'";
            
            //Comprobante y pie
            oRepFacturaVta.DataDefinition.FormulaFields["linea-01"].Text = "' Razón Social: " + clsGlobales.cParametro.RazonSocial + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-02"].Text = "' Domicilio:'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-03"].Text = "'" + clsGlobales.cParametro.Direccion + "-" + clsGlobales.cParametro.Localidad + ", Córdoba" + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-04"].Text = "' Condición frente al Iva : " + clsGlobales.cParametro.CondicionIva + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-06"].Text = "' Ingresos Brutos: " + "285118832" + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-07"].Text = "' Fecha de Inicio Actividades : " + "01/09/2019" + "'";

            //Llamar a BCAafip
            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo(Application.StartupPath + "\\BCAfip.exe");
            psi.Arguments = " -mod 2 -raz 2.00 -vis -2 -alt 100 -ali 0 -fno Tahoma -fsz 12 -cui " + txtCuit.Text + " -tip 06 -pto 01 -cae " + txtCAE.Text + " -vto " + this.dtVencCAE.Value.ToString("yyyyMMdd") + " -out " + @"C:\Temp\AFIP.jpg";
            p.StartInfo = psi;
            p.Start();

            string path = @"C:\Temp\AFIP.jpg";
            oRepFacturaVta.SetParameterValue("picturePath", path);
            
            //Llamar al reporte
            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports(2);
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog(); 
        }
        
        #endregion

        #region Metodo ShowReportB
        
        //METODO ShowReportB, para mostrar la Factura B unicamente.
        private void ShowReportB(rptFacturaVtaB oRepFacturaVta, dsReportes oDsFactura)
        {
            //Establecer el DataSet como DataSource
            oRepFacturaVta.SetDataSource(oDsFactura);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepFacturaVta;

            oRepFacturaVta.DataDefinition.FormulaFields["NroComp"].Text = "'" + this.nroPunto.Text + "-" + nroComp.Text + "'";

            //Otras
            oRepFacturaVta.DataDefinition.FormulaFields["Fecha"].Text = "'" + dtFechaComp.Value.ToString("dd/MM/yyyy") + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["RazonSocial"].Text = "'" + txtRazonSocial.Text.ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["Domic"].Text = "'" + (txtDir.Text).ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["IVA"].Text = "'" + cboTipoCliente.Text.ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["CondicionVenta"].Text = "'" + cboFormaPago.Text.ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["CUIT"].Text = "'" + txtCuit.Text + "'";

            oRepFacturaVta.DataDefinition.FormulaFields["CAE"].Text = "'" + txtCAE.Text + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["VencCAE"].Text = "'" + dtVencCAE.Value.ToString("dd/MM/yyyy") + "'";

            double dtoImpo = (Convert.ToDouble(txtNeto.Text) * Convert.ToDouble(txtDto.Text)) / 100;
            oRepFacturaVta.DataDefinition.FormulaFields["Dto"].Text = "'" + txtDto.Text + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["DtoImpo"].Text = "'" + dtoImpo.ToString("#0.00") + "'";

            //SUBTOTAL            
            oRepFacturaVta.DataDefinition.FormulaFields["Neto"].Text = "'" + txtNeto.Text + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["Subtotal"].Text = "'" + txtSubTotal.Text + "'";

            if (Convert.ToInt32(cboComprobante.SelectedValue) == 1)
            {
                oRepFacturaVta.DataDefinition.FormulaFields["IVA10"].Text = "'" + txtIVA10.Text + "'";
                oRepFacturaVta.DataDefinition.FormulaFields["TotIVA"].Text = "'" + txtIVA.Text + "'";
            }

            //FLETE Y EXENTO
            oRepFacturaVta.DataDefinition.FormulaFields["Flete"].Text = "'" + txtFlete.Text + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["Exento"].Text = "'" + txtExento.Text + "'";

            oRepFacturaVta.DataDefinition.FormulaFields["Total"].Text = "'" + txtTotal.Text + "'";

            //Comprobante y pie
            oRepFacturaVta.DataDefinition.FormulaFields["linea-01"].Text = "' Razón Social: " + clsGlobales.cParametro.RazonSocial + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-02"].Text = "' Domicilio:'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-03"].Text = "'" + clsGlobales.cParametro.Direccion + "-" + clsGlobales.cParametro.Localidad + ", Córdoba" + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-04"].Text = "' Condición frente al Iva : " + clsGlobales.cParametro.CondicionIva + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-06"].Text = "' Ingresos Brutos: " + "285118832" + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-07"].Text = "' Fecha de Inicio Actividades : " + "01/09/2019" + "'";

            //Llamar a BCAafip
            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo(Application.StartupPath + "\\BCAfip.exe");
            psi.Arguments = " -mod 2 -raz 2.00 -vis -2 -alt 100 -ali 0 -fno Tahoma -fsz 12 -cui " + txtCuit.Text + " -tip 06 -pto 01 -cae " + txtCAE.Text + " -vto " + this.dtVencCAE.Value.ToString("yyyyMMdd") + " -out " + @"C:\Temp\AFIP.jpg";
            p.StartInfo = psi;
            p.Start();

            string path = @"C:\Temp\AFIP.jpg";
            oRepFacturaVta.SetParameterValue("picturePath", path);


            //Llamar al reporte
            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports(2);
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog(); 

        }

        #endregion        
                  
        #region Metodo BorrarArchvo

        //ELIMINAR ARCHIVO
        public void BorrarArchivo(String archivo)
        {
            if (System.IO.File.Exists(@archivo))
            {
                try
                {
                    System.IO.File.Delete(@archivo);
                }
                catch (System.IO.IOException e)
                {
                    return;
                }
            }
        }

        #endregion
        
        #region ValidaFactura

        //Valida los datos del form antes de solicitar CAE y emitir factura
        private bool ValidaFactura()
        {
            bool bRetorno = true;

            //Selecciono cliente?
            if (clsGlobales.ClientesSeleccionados.GetLength(0) == 0 && txtCuit.Text == "")
            {
                MessageBox.Show("Debe Seleccionar un Cliente!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtCuit.Focus();
                bRetorno = false;
                return bRetorno;
            }

            //Validar la CUIT                
            clsCUIT oCUITvalido = new clsCUIT(this.txtCuit.Text);

            if (!(oCUITvalido.EsValido))
            {
                MessageBox.Show("CUIT del Cliente es incorrecto!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtCuit.Focus();
                bRetorno = false;
                return bRetorno;
            }

            //Comprobante
            if (!(this.cboComprobante.SelectedIndex != -1))
            {
                MessageBox.Show("Debe seleccionar el Tipo de Comprobante!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.cboComprobante.Focus();
                bRetorno = false;
                return bRetorno;
            }

            //Forma de Pago
            if (!(this.cboFormaPago.SelectedIndex != -1))
            {
                MessageBox.Show("Debe seleccionar la Forma de Pago!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.cboFormaPago.Focus();
                bRetorno = false;
                return bRetorno;
            }

            //Fecha Comprobante mayor a la actual
            if (dtFechaComp.Value.Date > DateTime.Now.Date) 
            {
                MessageBox.Show("La Fecha Comprobante debe ser igual a la fecha del dìa de emisiòn!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtFechaComp.Focus();
                bRetorno = false;
                return bRetorno;
            }

            //Fecha Comprobante mayor a la actual
            if (dtFechaComp.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("La Fecha Comprobante debe ser igual a la fecha del dìa de emisiòn!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtFechaComp.Focus();
                bRetorno = false;
                return bRetorno;
            }

            //Fecha Pago debe ser posterior o igual a la fecha de emision del comprobante
            if (dtFechaComp.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("La Fecha de Vencimiento del Pago debe ser igual o posterior a la fecha del dìa de emisiòn!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtVtoPago.Focus();
                bRetorno = false;
                return bRetorno;
            }

            //Cargo productos
            if (dgvArt.Rows.Count == 0)
            {
                MessageBox.Show("Debe cargar los Productos a la Factura!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dgvArt.Focus();
                bRetorno = false;
                return bRetorno;
            }

            //Si no ha completado cantidades para todos los casos, mensaje y salir
            foreach (DataGridViewRow row in dgvArt.Rows)
            {

                if (row.Cells["Cantidad"].Value == null)
                {
                    MessageBox.Show("Debe completar, para todos los casos, la Cantidad!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgvArt.Focus();
                    bRetorno = false;
                    return bRetorno;
                }

            }
            
            //Retornar
            return bRetorno;
        }

        #endregion

        #region Eventos Grilla

        private void dgvArt_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            //Declaracion de Variables (Costo Unitario y Cantidad)
            DataGridViewRow row = dgvArt.CurrentRow;

            dCantidad = Convert.ToInt32(row.Cells["CantOriginal"].Value);

            //SI ES PENDIENTE NO SE PUEDE EDITAR!
            if (myPresu.Pendiente == 1)
            {
                //Devolver la cantidad
                dgvArt.CurrentRow.Cells["Cantidad"].Value = dCantidad;

                return;
            }

            
            if (row.Cells["Cantidad"].Value == null)
            {
                //Establecer null al costo final
                dgvArt.CurrentRow.Cells["Cantidad"].Value = dCantidad;
            }
            else
            {
                if (Convert.ToInt32(row.Cells["Cantidad"].Value) == 0)
                {
                   dgvArt.CurrentRow.Cells["Cantidad"].Value = dCantidad;
                }
                else if ((Convert.ToInt32(row.Cells["Cantidad"].Value) > (Convert.ToInt32(row.Cells["CantOriginal"].Value.ToString()))))
                {
                   dgvArt.CurrentRow.Cells["Cantidad"].Value = dCantidad;
                }
            }

            // Calculo el subtotal de la fila
            CalcularSubtotal();
            // Calculo el total neto
            CalcularTotal();
        }

        #endregion

        #region Método que calcula el subtotal de los artículos por fila

        private void CalcularSubtotal()
        {
            // Subtotal y coeficiente
            double subTotal = 0;
            string auxSubTotal = "";
            double coef = 0;

            //Precio Coeficiente y Precio con Iva
            double PreCf = 0;
            double PreIva = 0;

            //Calcular IVA
            double CalcIva = 0;

            //Descuento y Subtotales
            double Dto = 0;
            double Sub1 = 0;
            double Sub2 = 0;
            double Sub3 = 0;

            double Cant = 0;
            double Pre = 0;


            //RECORRER LA GRILLA
            foreach (DataGridViewRow row in dgvArt.Rows)
            {
                //SINO ES UN ARTICULO TRAIDO DE EXCEL...AGREGAR COEFICIENTE
                if (Convert.ToInt32(row.Cells["Excel"].Value.ToString()) == 0)
                {
                    //CALCULAR EL PRECIO CON EL COEFICIENTE
                    switch (Convert.ToInt32(this.myCliente.IdTipoCliente))
                    {
                        case 28:
                            subTotal = Convert.ToDouble(row.Cells["Precio"].Value) * Convert.ToDouble(row.Cells["Pub"].Value);
                            coef = Convert.ToDouble(row.Cells["Pub"].Value);
                            break;
                        case 29:
                            subTotal = Convert.ToDouble(row.Cells["Precio"].Value) * Convert.ToDouble(row.Cells["Dist"].Value);
                            coef = Convert.ToDouble(row.Cells["Dist"].Value);
                            break;
                        case 30:
                            subTotal = Convert.ToDouble(row.Cells["Precio"].Value) * Convert.ToDouble(row.Cells["Rev"].Value);
                            coef = Convert.ToDouble(row.Cells["Rev"].Value);
                            break;
                    }

                    //subTotal = Convert.ToDouble(row.Cells["Precio"].Value);
                    auxSubTotal = subTotal.ToString("#0.00");
                    subTotal = Convert.ToDouble(auxSubTotal);
                }
                else
                {
                    auxSubTotal = subTotal.ToString("#0.00");
                    subTotal = Convert.ToDouble(auxSubTotal);
                    subTotal = Convert.ToDouble(row.Cells["Precio"].Value);
                }


                //PRECIO COEFICIENTE ( FACTURA A )  y PRECIO COEFICIENTE + IVA ( FACTURA B )
                PreCf = subTotal;

                //PRECIO CON IVA ( PARA FACTURA B)
                PreIva = subTotal + (subTotal * Convert.ToDouble(row.Cells["Alicuota"].Value)/100);
                row.Cells["PrecioConIva"].Value = PreIva.ToString("#0.00");

                //CARGAR PRECIO CON COEFICIENTE
                row.Cells["PrecioCoef"].Value = PreCf.ToString("#0.00");
                
                //PRECIO CON EL COEFICIENTE * LA CANTIDAD Y APLICAR DESCUENTO SI CORRESPONDE
                Dto = Convert.ToDouble(row.Cells["Dsto"].Value);

                if (!(Convert.ToDouble(row.Cells["Dsto"].Value) == 0))
                {
                    //CALCULAR EL SUBTOTAL CON EL DESCUENTO ( FACTURA A )
                    Sub1 = Convert.ToDouble(row.Cells["Cantidad"].Value) * PreCf;
                    Sub1 = Sub1 - ((Sub1 * Dto) / 100);
                    
                    //PARA EL CASO DE PRECIO CON IVA ( FACTURA B )
                    Sub3 = Convert.ToDouble(row.Cells["Cantidad"].Value) * PreIva;
                    Sub3 = Sub3 - ((Sub3 * Dto) / 100);

                }
                else 
                {
                    //PARA FACTURA A
                    Sub1 = Convert.ToDouble(row.Cells["Cantidad"].Value) * PreCf;

                    //PARA FACTURA B
                    Sub3 = Convert.ToDouble(row.Cells["Cantidad"].Value) * PreIva;
                }

                //CALCULAR EL IVA ( PARA FACTURA A )                
                CalcIva = Sub1 * Convert.ToDouble(row.Cells["Alicuota"].Value) / 100;
                row.Cells["IVA"].Value = CalcIva.ToString("#0.00");

                //APLICAR IVA ( PARA FACTURA A)
                Sub2 = Sub1 + (Sub1 * Convert.ToDouble(row.Cells["Alicuota"].Value.ToString())) / 100;

                //VARIABLES PARA FORMATEO
                Cant = Convert.ToDouble(row.Cells["Cantidad"].Value);
                Pre = Convert.ToDouble(row.Cells["Precio"].Value);
                double Ali = Convert.ToDouble(row.Cells["Alicuota"].Value);
              
                //FORMATEO DE COLUMNAS GENERAL
                row.Cells["Cantidad"].Value = Cant.ToString("#0");

                //SI LA PRIMERA VEZ
                if (row.Cells["CantOriginal"].Value == null)
                {
                   row.Cells["CantOriginal"].Value = Cant.ToString("#0");
                }   

                row.Cells["Alicuota"].Value = Ali.ToString("#0.00");
                row.Cells["IVA"].Value = CalcIva.ToString("#0.00");

                //FORMATEO DE PRECIOS GENERAL
                row.Cells["Precio"].Value = Pre.ToString("#0.00000");
                row.Cells["PrecioCoef"].Value = PreCf.ToString("#0.00");
                row.Cells["PreIvaAux"].Value = PreIva;

                //ASIGNAR SUBTOTAL ( CON DTO ) FACTURA A
                row.Cells["SubTotalDto"].Value = Sub1.ToString("#0.00");

                //ASIGNAR EL SUBTOTAL ( A UTILIZAR PARA FACTURA B )
                row.Cells["SubTotal"].Value = Sub3.ToString("#0.00");

                //ASIGNAR EL SUBTOTAL ( CON IVA ) FACTURA A
                row.Cells["SubTotalIva"].Value = Sub2.ToString("#0.00");
            }
        }


        #endregion

        #region Método que calcula el Total del comprobante y enumera las filas

        private void CalcularTotal()
        {
            //Variables para Factura A
            double NetoFA = 0;
            double SubTotalFA = 0;
            double TotalFacturaFA = 0;
            double NetoFB = 0;

            acumulaExento = 0;
            acumula21 = 0;
            acumula10 = 0;
            acumula21Dto = 0;
            acumula10Dto = 0;
            acumulaExentoDto = 0;
            dto = 0;
            Iva21 = 0;
            Iva10 = 0;
            acumulaNetoFB = 0;
            acumulaNetoFBDto = 0;
            acumulaExentoFB = 0;
            acumulaExentoFBDto = 0;

            // Vaiable que guarda el número de la fila
            int fila = 1;

            // Acumuladores de prueba

            // Recorro la grilla y sumo los subtotales de lo artículos
            foreach (DataGridViewRow row in dgvArt.Rows)
            {
                // Pongo el número del item
                    row.Cells["Item"].Value = fila;
                

                // ACUMULADORES DE IVA 
                    if (Convert.ToDouble(row.Cells["Alicuota"].Value) == 21)
                    {
                        acumula21 = acumula21 + (Convert.ToDouble(row.Cells["Cantidad"].Value) * Convert.ToDouble(row.Cells["PrecioCoef"].Value));

                      //  acumula21 = acumula21 + Convert.ToDouble(row.Cells["SubtotalDto"].Value);

                        Iva21 = Iva21 + Convert.ToDouble(row.Cells["IVA"].Value);
                    }
                    else if (Convert.ToDouble(row.Cells["Alicuota"].Value) == 10.5)
                    {
                        acumula10 = acumula10 + (Convert.ToDouble(row.Cells["Cantidad"].Value) * Convert.ToDouble(row.Cells["PrecioCoef"].Value));

                     //   acumula10 = acumula10 + Convert.ToDouble(row.Cells["SubtotalDto"].Value);

                        Iva10 = Iva10 + Convert.ToDouble(row.Cells["IVA"].Value);
                    }
                    else if (Convert.ToDouble(row.Cells["Alicuota"].Value) == 0)
                    {
                       acumulaExento = acumulaExento + (Convert.ToDouble(row.Cells["Cantidad"].Value) * Convert.ToDouble(row.Cells["PrecioCoef"].Value));

                       acumulaExentoFB = acumulaExentoFB + (Convert.ToDouble(row.Cells["Cantidad"].Value) * Convert.ToDouble(row.Cells["PreIvaAux"].Value));


                     //   acumulaExento = acumulaExento + Convert.ToDouble(row.Cells["SubtotalDto"].Value);
                    }

               
                //HAY DTO?
                if (Convert.ToDouble(row.Cells["Dsto"].Value) >0)
                {
                    dto = 1 - (Convert.ToDouble(row.Cells["Dsto"].Value) / 100);
                    acumula21Dto = acumula21 * dto;
                    acumula10Dto = acumula10 * dto;
                    acumulaExentoDto = acumulaExento * dto;
                }

                //FACTURA B

                //ACUMULAR NETO FB
                 
                 if (!(Convert.ToDouble(row.Cells["Alicuota"].Value)==0))
                 {
                     acumulaNetoFB = acumulaNetoFB + (Convert.ToDouble(row.Cells["Cantidad"].Value) * Convert.ToDouble(row.Cells["PreIvaAux"].Value));
                 }

                //HAY DTO
                if (Convert.ToDouble(row.Cells["Dsto"].Value) > 0)
                {
                    dto = 1 - (Convert.ToDouble(row.Cells["Dsto"].Value) / 100);
                    acumulaNetoFBDto = acumulaNetoFB * dto;
                    acumulaExentoFBDto = acumulaExentoFB * dto;
                }

                //AUMENTAR CONTADOR FILAS
                    fila++;

            }

            //VERIFICAR TIPO COMPROBANTE
            if (Convert.ToInt32(cboComprobante.SelectedValue) == 1)
            {

                //NETO
                    NetoFA = acumula10 + acumula21;
                    this.txtNeto.Text = NetoFA.ToString("#0.00");

                //SUBTOTAL
                    if (acumula21Dto > 0 || acumula10Dto > 0 || acumulaExentoDto > 0)
                    {
                        SubTotalFA = acumula21Dto + acumula10Dto;
                        this.txtSubTotal.Text = SubTotalFA.ToString("#0.00");
                        //EXENTO
                        this.txtExento.Text = acumulaExentoDto.ToString("#0.00");

                        //HAY FLETE?
                        if (Convert.ToDouble(txtFlete.Text) > 0)
                        {
                            //EXENTO
                            this.txtExento.Text = (acumulaExentoDto + Convert.ToDouble(txtFlete.Text)).ToString("#0.00");
                        }

                        txtIVA10.Text = ((acumula10Dto * 1.105) - acumula10Dto).ToString("#0.00");
                        txtIVA.Text = ((acumula21Dto * 1.21) - acumula21Dto).ToString("#0.00");
                    }
                    else
                    {
                        this.txtSubTotal.Text = NetoFA.ToString("#0.00");
                        //EXENTO
                        this.txtExento.Text = acumulaExento.ToString("#0.00");

                        //HAY FLETE?
                        if (Convert.ToDouble(txtFlete.Text) > 0)
                        {
                            //EXENTO
                            this.txtExento.Text = (acumulaExento + Convert.ToDouble(txtFlete.Text)).ToString("#0.00");
                        }

                        txtIVA10.Text = ((acumula10 * 1.105) - acumula10).ToString("#0.00");
                        txtIVA.Text = ((acumula21 * 1.21) - acumula21).ToString("#0.00");

                    }

                //IVA
                  /*  txtIVA10.Text = Iva10.ToString("#0.00");
                     txtIVA.Text = Iva21.ToString("#0.00");*/

                   
                  
                //TOTAL FACTURA
                    TotalFacturaFA = Convert.ToDouble(txtSubTotal.Text) +  
                                  Convert.ToDouble(txtExento.Text) + Convert.ToDouble(txtIVA.Text) 
                                  + Convert.ToDouble(txtIVA10.Text);

                    txtTotal.Text = TotalFacturaFA.ToString("#0.00");
                    
            }
            else
            {


                //EXENTO DTO
                if (dto > 0)
                {
                    //NETO              
                    NetoFB = acumulaNetoFB;
                    this.txtNeto.Text = NetoFB.ToString("#0.00");

                    txtSubTotal.Text =acumulaNetoFBDto.ToString("#0.00");

                    //HAY FLETE, EXENTO
                    if (Convert.ToDouble(txtFlete.Text) > 0)
                    {
                        acumulaExentoFBDto += Convert.ToDouble(txtFlete.Text);
                        txtExento.Text = acumulaExentoFBDto.ToString("#0.00");

                    }
                    else
                    {
                        txtExento.Text = acumulaExentoFBDto.ToString("#0.00");
                    }

                    txtIVA10.Text = ((acumula10Dto * 1.105) - acumula10Dto).ToString("#0.00");
                    txtIVA.Text = ((acumula21Dto * 1.21) - acumula21Dto).ToString("#0.00");

                }
                else
                {
                    //NETO              
                    NetoFB = acumulaNetoFB;
                    this.txtNeto.Text = NetoFB.ToString("#0.00");



                    txtSubTotal.Text = NetoFB.ToString("#0.00");

                    //HAY FLETE, EXENTO
                    if (Convert.ToDouble(txtFlete.Text) > 0)
                    {
                        acumulaExentoFB += Convert.ToDouble(txtFlete.Text);
                        txtExento.Text = acumulaExentoFB.ToString("#0.00");
                    }
                    else
                    {
                        txtExento.Text = acumulaExentoFB.ToString("#0.00");
                    }

                    txtIVA10.Text = ((acumula10 * 1.105) - acumula10).ToString("#0.00");
                    txtIVA.Text = ((acumula21 * 1.21) - acumula21).ToString("#0.00");
                }

                //TOTAL
                txtTotal.Text = (Convert.ToDouble(txtSubTotal.Text) + Convert.ToDouble(txtExento.Text)).ToString("#0.00");

                //IVA
                /*txtIVA10.Text = Iva10.ToString("#0.00");
                txtIVA.Text = Iva21.ToString("#0.00");*/
            }
        }

        #endregion

        #region Eventos KeyPress

        private void txtNeto_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 44)
            {
                e.KeyChar = Convert.ToChar(46);
                ch = e.KeyChar;

            }
            //PUNTO DECIMAL. N.
            if (ch == 46 && txtNeto.Text.IndexOf('.') != -1)
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
        }

        
        private void txtIVA_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 44)
            {
                e.KeyChar = Convert.ToChar(46);
                ch = e.KeyChar;

            }
            //PUNTO DECIMAL. N.
            if (ch == 46 && txtIVA.Text.IndexOf('.') != -1)
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
        }

        #endregion

        #region Metodo ShowPedPresuA

        private void ShowPedPresuA(rptPedPresuC oRepFacturaVta, dsReportes oDsFactura)
        {
            //Establecer el DataSet como DataSource
            oRepFacturaVta.SetDataSource(oDsFactura);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepFacturaVta;

            oRepFacturaVta.DataDefinition.FormulaFields["NroComp"].Text = "'" + this.nroPunto.Text + "-" + this.myPresu.Nro.ToString("D8") + "'";

            //Otras
            oRepFacturaVta.DataDefinition.FormulaFields["Fecha"].Text = "'" + dtFechaComp.Value.ToString("dd/MM/yyyy") + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["RazonSocial"].Text = "'" + txtRazonSocial.Text.ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["Domic"].Text = "'" + (txtDir.Text).ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["IVA"].Text = "'" + cboTipoCliente.Text.ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["CondicionVenta"].Text = "'" + cboFormaPago.Text.ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["CUIT"].Text = "'" + txtCuit.Text + "'";

            double dtoImpo = (Convert.ToDouble(txtNeto.Text) * Convert.ToDouble(txtDto.Text)) / 100;
            oRepFacturaVta.DataDefinition.FormulaFields["Dto"].Text = "'" + txtDto.Text + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["DtoImpo"].Text = "'" + dtoImpo.ToString("#0.00") + "'";

            //SUBTOTAL            
            oRepFacturaVta.DataDefinition.FormulaFields["Neto"].Text = "'" + txtNeto.Text + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["Subtotal"].Text = "'" + txtSubTotal.Text + "'";

            //MUESTRO LOS 2 IVA ( 21 Y 10.5)
            if (Convert.ToInt32(cboComprobante.SelectedValue) == 1)
            {
                oRepFacturaVta.DataDefinition.FormulaFields["IVA10"].Text = "'" + txtIVA10.Text + "'";
                oRepFacturaVta.DataDefinition.FormulaFields["TotIVA"].Text = "'" + txtIVA.Text + "'";
            }


            //FLETE Y EXENTO
            oRepFacturaVta.DataDefinition.FormulaFields["Flete"].Text = "'" + txtFlete.Text + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["Exento"].Text = "'" + txtExento.Text + "'";

            //TOTAL
            oRepFacturaVta.DataDefinition.FormulaFields["Total"].Text = "'" + txtTotal.Text + "'";

            //Comprobante y pie
            oRepFacturaVta.DataDefinition.FormulaFields["linea-01"].Text = "' Razón Social: " + clsGlobales.cParametro.RazonSocial + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-02"].Text = "' Domicilio:'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-03"].Text = "'" + clsGlobales.cParametro.Direccion + "-" + clsGlobales.cParametro.Localidad + ", Córdoba" + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-04"].Text = "' Condición frente al Iva : " + clsGlobales.cParametro.CondicionIva + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-06"].Text = "' Ingresos Brutos: " + "285118832" + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-07"].Text = "' Fecha de Inicio Actividades : " + "01/09/2019" + "'";

            //Llamar al reporte
            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();
        }

        #endregion

        #region Metodo ShowPedPresuB

        //METODO ShowReportB, para mostrar la Factura B unicamente.
        private void ShowPedPresuB(rptPedPresuD oRepFacturaVta, dsReportes oDsFactura)
        {
            //Establecer el DataSet como DataSource
            oRepFacturaVta.SetDataSource(oDsFactura);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepFacturaVta;

            oRepFacturaVta.DataDefinition.FormulaFields["NroComp"].Text = "'" + this.nroPunto.Text + "-" + this.myPresu.Nro.ToString("D8") + "'";

            //Otras
            oRepFacturaVta.DataDefinition.FormulaFields["Fecha"].Text = "'" + dtFechaComp.Value.ToString("dd/MM/yyyy") + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["RazonSocial"].Text = "'" + txtRazonSocial.Text.ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["Domic"].Text = "'" + (txtDir.Text).ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["IVA"].Text = "'" + cboTipoCliente.Text.ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["CondicionVenta"].Text = "'" + cboFormaPago.Text.ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["CUIT"].Text = "'" + txtCuit.Text + "'";

            double dtoImpo = (Convert.ToDouble(txtNeto.Text) * Convert.ToDouble(txtDto.Text)) / 100;
            oRepFacturaVta.DataDefinition.FormulaFields["Dto"].Text = "'" + txtDto.Text + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["DtoImpo"].Text = "'" + dtoImpo.ToString("#0.00") + "'";

            //SUBTOTAL            
            oRepFacturaVta.DataDefinition.FormulaFields["Neto"].Text = "'" + txtNeto.Text + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["Subtotal"].Text = "'" + txtSubTotal.Text + "'";

            if (Convert.ToInt32(cboComprobante.SelectedValue) == 1)
            {
                oRepFacturaVta.DataDefinition.FormulaFields["IVA10"].Text = "'" + txtIVA10.Text + "'";
                oRepFacturaVta.DataDefinition.FormulaFields["TotIVA"].Text = "'" + txtIVA.Text + "'";
            }

            //FLETE Y EXENTO
            oRepFacturaVta.DataDefinition.FormulaFields["Flete"].Text = "'" + txtFlete.Text + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["Exento"].Text = "'" + txtExento.Text + "'";

            oRepFacturaVta.DataDefinition.FormulaFields["Total"].Text = "'" + txtTotal.Text + "'";

            //Comprobante y pie
            oRepFacturaVta.DataDefinition.FormulaFields["linea-01"].Text = "' Razón Social: " + clsGlobales.cParametro.RazonSocial + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-02"].Text = "' Domicilio:'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-03"].Text = "'" + clsGlobales.cParametro.Direccion + "-" + clsGlobales.cParametro.Localidad + ", Córdoba" + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-04"].Text = "' Condición frente al Iva : " + clsGlobales.cParametro.CondicionIva + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-06"].Text = "' Ingresos Brutos: " + "285118832" + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-07"].Text = "' Fecha de Inicio Actividades : " + "01/09/2019" + "'";

            //Llamar al reporte
            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();
        }

        #endregion        

        private void dgvArt_SelectionChanged(object sender, EventArgs e)
        {
            /*  string sSQL = "";
              DataTable mData = new DataTable();*/

            // Si la grilla tiene artículos
            if (dgvArt.RowCount > 0)
            {
                // Almaceno én una variable la posición de fila en la que me encuentro
                int fila = dgvArt.CurrentRow.Index;
                // Pongo el foco de la fila en la columna cantidad
                dgvArt.CurrentCell = dgvArt["Cantidad", fila];

            }
        }

        #region Método que actualiza el saldo del Clientes

        private void ActualizarSaldoCli(int IdCli, double Tot, int p_Dest = 0)
        {
            // Variable que almacena el saldo del proveedor
            double SaldoCli = 0;
            //Si viene 0, guarda en Clientes
            if (p_Dest == 0)
            {
                // Armo la cadena SQl para traer el saldo anterior del proveedor
                string myCadenaSQLSaldo = "select * from Clientes where IdCliente = " + IdCli;
                // Ejecuto la consulta y paso los datos a la tabla
                DataTable mySaldoProveedor = clsDataBD.GetSql(myCadenaSQLSaldo);
                // Recorro la tabla para obtener el saldo inicial del proveedor
                foreach (DataRow rowCli in mySaldoProveedor.Rows)
                {
                    // Paso a la variable el saldo anterior
                    SaldoCli = Convert.ToDouble(rowCli["Saldo"]);
                    // Actualizo el saldo
                    SaldoCli = SaldoCli + Tot;
                }
                // Armo la consulta para actualizar el dato
                myCadenaSQLSaldo = "UPDATE Clientes SET Saldo = " + SaldoCli + " where IdCliente = " + IdCli;
                // Actualizo el saldo
                clsDataBD.GetSql(myCadenaSQLSaldo);
            }
            else            
            {
                string myCadenaSQL = "";
                // Armo la cadena SQl para traer el saldo anterior del proveedor
                string myCadenaSQLSaldo = "select * from SaldoCliProv where IdCliente = " + IdCli;
                // Ejecuto la consulta y paso los datos a la tabla
                DataTable mySaldoCliente = clsDataBD.GetSqlB(myCadenaSQLSaldo);
                // Si la tabla no tiene registros o es null
                if (mySaldoCliente.Rows.Count == 0 || mySaldoCliente == null)
                {
                    myCadenaSQL = "insert into SaldoCliProv (IdCliente, SaldoCli, IdProveedor, SaldoProv, SaldoInicial, SaldoAFavor) values (" +
                                    IdCli + ",0,0,0,0,0)";
                    // Ejecuto la consulta que me crea el saldo inicial del proveedor
                    clsDataBD.GetSqlB(myCadenaSQL);
                    // Vuelvo a cargar la tabla con el saldo en 0 del proveedor
                    mySaldoCliente = clsDataBD.GetSqlB(myCadenaSQLSaldo);
                }

                // Recorro la tabla para obtener el saldo inicial del proveedor
                foreach (DataRow rowProv in mySaldoCliente.Rows)
                {
                    // Paso a la variable el saldo anterior
                    SaldoCli = Convert.ToDouble(rowProv["SaldoCli"]);
                    // Actualizo el saldo
                    SaldoCli = SaldoCli + Tot;
                }
                // Armo la consulta para actualizar el dato
                myCadenaSQL = "update SaldoCliProv set SaldoCli = " + SaldoCli + " where IdCliente = " + IdCli;
                // Actualizo el saldo
                clsDataBD.GetSqlB(myCadenaSQL);
            }
        }

        #endregion

    }

}

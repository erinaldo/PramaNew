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
using Prama.Formularios.Auxiliares;

namespace Prama.Formularios.Ventas
{

    public partial class frmABM_PedPresu_Excel : Form
    {
        #region DECLARACION DE VARIABLES

        //VARIABLES GLOBALES
        public int IdCliente = 0;           //ID DEL CLIENTE
        public int Id = 0;                  //ID DE PEDIDO / PRESUPUESTO
        public bool bPresupuesto = false;   //SI SE TRATA DE UN PRESUPUESTO ( SE CARGA EN CONSTRUCTOR )
        public string myEstado = "";        //ESTADO ( NO USO EL GLOBAL )
        public string Tabla = "";           //PARA CUANDO SE AGREGA O EDITA DATOS, TOMAR EL NOMBRE DE LA TABLA EN FORMA GENERICA
        public bool bExisten = false;       //PARA CONTROLAR SI SE CARGA UN ARTICULO + DE 1 VEZ
        DataGridView val_dgvCli = null;
        public bool bPrintF = false;

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

        //OBJETOS GLOBALES
        //CLIENTE
        clsCLientes myCliente = new clsCLientes();
        //PEDIDOS
        clsPedidos myPedido;
        clsDetallePedidos myDetPedido;
        //PRESUPUESTOS
        clsPresupuestos myPresu;
        clsDetallePresupuestos myDetPresu;

        #endregion

        #region CONSTRUCTOR

        public frmABM_PedPresu_Excel(string p_Estado, int p_IdCliente, int p_Id, bool p_Presupuesto, DataGridView p_DgvCli = null, bool bPrintFast=false)
        {
            InitializeComponent();
            myEstado = p_Estado;
            Id = p_Id;
            IdCliente = p_IdCliente;
            bPresupuesto = p_Presupuesto;
            val_dgvCli = p_DgvCli;
            bPrintF = bPrintFast;

            //PEDIDO O PRESUPUESTO, INSTANCIAR OBJETOS
            if (!(bPresupuesto))
            {
                myPedido = new clsPedidos();
                myDetPedido = new clsDetallePedidos();
                Tabla = "Temporal_CargaDetPedido";
            }
            else
            {
                myPresu = new clsPresupuestos();
                myDetPresu = new clsDetallePresupuestos();
                Tabla = "Temporal_CargaDetPresu";
            }

        }

        #endregion

        #region EVENTO LOAD

        private void frmABM_PedPresu_Excel_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            //Tooltips
            this.CargarToolTips();

            //Eliminar tabla temporal
            string mySQL = "";
            if (!(bPresupuesto))
            {
                mySQL = "Delete from " + Tabla + " where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario;
            }
            else
            {
                mySQL = "Delete from " + Tabla;
            }
            clsDataBD.GetSql(mySQL);

            //VACIAR VECTOR PRODUCTOS SELECCIONADOS
            clsGlobales.ProductosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.ProductosSeleccionados, new int[] { 0, 2 });

            //Cargar Combos
            CargarCombosFormulario();

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

                //Cambiar tamaño comentario
                this.txtComentario.Size = new System.Drawing.Size(223, 81);
                this.chkPendiente.Location = new Point(162, 373);


                //Controles
                HabilitarControlesPresupuesto();
            }
            else
            {
                //Titulo ventana
                if (this.myEstado == "A")
                {
                    this.Text = clsGlobales.cParametro.NombreFantasia + " - ALTA PEDIDOS ";
                    //Por Defecto
                    cboEntrada.SelectedIndex = 0;
                }
                else
                {
                    this.Text = clsGlobales.cParametro.NombreFantasia + " - EDICION PEDIDOS ";
                }

                //Cambiar tamaño comentario
                this.txtComentario.Size = new System.Drawing.Size(430, 81);
                this.chkPendiente.Location = new Point(369, 373);

            }

            //Esta en Alta
            if (this.myEstado == "A")
            {

                //Boton imprimir visible
                this.btnImprimir.TabStop = false;
                this.btnImprimir.Visible = false;

                //Boton imprimir visible
                this.btnPrintFast.TabStop = false;
                this.btnPrintFast.Visible = false;

                //Recibe
                if (!bPresupuesto)
                {
                    txtRecibe.Text = clsGlobales.UsuarioLogueado.Usuario;
                }

                //Fechas
                lblPunto.Text = Convert.ToInt32(lblPunto.Text).ToString("D4");
                //** Tomamos el ultimo numero de pedido de la tabla puntos de venta **

                //nroComp.Text = (clsDataBD.RetornarMax("Pedidos", "Nro") + 1).ToString("D8"); // Old
                nroComp.Text = (clsDataBD.getUltComp("Ult_Pedido", clsGlobales.cParametro.PtoVtaPorDefecto, 0) + 1).ToString("D8"); //.ToString("D8");

                //Fecha del dia
                this.dtFecha.Value = DateTime.Now;

                //Boton
                this.btnEditCli.Enabled = false;
                this.btnCostoFlete.Enabled = false;
                if (val_dgvCli == null)
                {
                    this.btnAddCli.Enabled = true && (clsGlobales.UsuarioLogueado.Nivel >= 5);
                }
            }
            else if (this.myEstado == "M")
            {

                //Cambiar botones respecto a Clientes
                this.btnCli.Enabled = false;
                //Boton
                this.btnEditCli.Enabled = false;
                if (val_dgvCli == null)
                {
                    this.btnEditCli.Enabled = true;
                }
                
                this.btnCostoFlete.Enabled = true;
                this.btnAddCli.Enabled = false;

                //Generar el resto de la vista para poder modificar
                GenerarVistaModificacion();

                //Boton imprimir visible
                this.btnImprimir.TabStop = true;
                this.btnImprimir.Visible = true;

                //Boton imprimir visible
                this.btnPrintFast.TabStop = true;
                this.btnPrintFast.Visible = true;
            }

            //Ocultar o mostrar otros datos
            ShowIvaStuff();
            
            //Resize Vector Excel
            clsGlobales.ArtExcel = (string[,])clsValida.ResizeMatriz(clsGlobales.ProductoComposicion, new int[] {0, 13 });

            //Habilitar Botones
            this.HabilitarOtros();

            //Tipo Cliente
            if (Convert.ToInt32(cboTipoCliente.SelectedValue) == 1)
            {
                setOtrosItems(1); //FACT. A
            }
            else
            {
                setOtrosItems(2); //FACT. B
            }

            //Ordenamiento
            DeshabilitarOrdenGrillas();

            //Controlar si viene de Clientes
            if (!(this.val_dgvCli == null))
            {
                //Quitar el cliente actualmente selecionado
                EliminarClienteSeleccionado();
                //Buscar Cliente
                // Redimensiono el tamaño de la matriz
                clsGlobales.ClientesSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ClientesSeleccionados, 1);
                // A la posición creada le asigno el Id seleccionado
                clsGlobales.ClientesSeleccionados[clsGlobales.ClientesSeleccionados.Length - 1] = Convert.ToInt32(val_dgvCli.CurrentRow.Cells["Codigo"].Value);
                //Cliente Nuevo
                this.CargarClienteNuevo();
                //Habilitar
                this.HabilitarOtros();
                //Calculos
                this.CalcularSubtotal();
                this.CalcularTotal();
                //Retorna
                if (clsGlobales.ClientesSeleccionados.GetLength(0) > 0)
                {
                    //Inhabilitar Boton
                    this.btnEditCli.Enabled = true;
                    this.btnCli.Enabled = false;
                    this.btnCostoFlete.Enabled = true;

                    //Ocultar o mostrar otros datos
                    ShowIvaStuff();
                }
                
                this.btnAddCli.Enabled = false;

            }

            //Impresión
            if (bPrintF)
            {
                btnPrintFast.PerformClick();
            }



        }

        #endregion

        #region  Metodo LimpiarDatosPantalla

        // Luego de dar de alta un nuevo cliente si se lo va a utilizar, limpia pantalla, tablas, etc.

        private void LimpiarDatosPantalla()
        {
            //Eliminar tabla temporal
            string mySQL = "";

            //Limpiar Grilla
            dgvArt.DataSource = null;

            //Verificar
            this.txtDto.TabStop = false;
            this.txtDto.Text = "0.00";
            this.txtDto.Enabled = false;
            this.txtCostoFlete.TabStop = false;
            this.txtCostoFlete.Text = "0.00";
            this.txtCostoFlete.Enabled = false;

            // Recalculo el total de la orden de compra
            CalcularTotal();

            //Pedido o Presupuesto?...
            if (!(bPresupuesto))
            {
                mySQL = "Delete from " + Tabla + " where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario;
            }
            else
            {
                mySQL = "Delete from " + Tabla;
            }
            clsDataBD.GetSql(mySQL);

            //VACIAR VECTOR PRODUCTOS SELECCIONADOS
            clsGlobales.ProductosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.ProductosSeleccionados, new int[] { 0, 2 });

            //Cargar Combos
            CargarCombosFormulario();

            //Cargar Punto Venta
            this.lblPunto.Text = clsGlobales.cParametro.PtoVtaPorDefecto.ToString("D4");

            //Esta en Alta
            if (this.myEstado == "A")
            {

                //Boton imprimir visible
                this.btnImprimir.TabStop = false;
                this.btnImprimir.Visible = false;

                //Boton imprimir visible
                this.btnPrintFast.TabStop = false;
                this.btnPrintFast.Visible = false;

                //Recibe
                if (!bPresupuesto)
                {
                    txtRecibe.Text = clsGlobales.UsuarioLogueado.Usuario;
                }

                //Fechas
                lblPunto.Text = Convert.ToInt32(lblPunto.Text).ToString("D4");
                //** Tomamos el ultimo numero de pedido de la tabla puntos de venta **

                //nroComp.Text = (clsDataBD.RetornarMax("Pedidos", "Nro") + 1).ToString("D8"); // Old
                nroComp.Text = (clsDataBD.getUltComp("Ult_Pedido", clsGlobales.cParametro.PtoVtaPorDefecto, 0) + 1).ToString("D8"); //.ToString("D8");

                //Fecha del dia
                this.dtFecha.Value = DateTime.Now;

                //Boton
                this.btnEditCli.Enabled = false;
                this.btnCostoFlete.Enabled = false;
                if (val_dgvCli == null)
                {
                    this.btnAddCli.Enabled = true && (clsGlobales.UsuarioLogueado.Nivel >= 5);
                }
            }
            else if (this.myEstado == "M")
            {

                //Cambiar botones respecto a Clientes
                this.btnCli.Enabled = false;
                //Boton
                this.btnEditCli.Enabled = false;
                if (val_dgvCli == null)
                {
                    this.btnEditCli.Enabled = true;
                }

                this.btnCostoFlete.Enabled = true;
                this.btnAddCli.Enabled = false;

                //Generar el resto de la vista para poder modificar
                GenerarVistaModificacion();

                //Boton imprimir visible
                this.btnImprimir.TabStop = true;
                this.btnImprimir.Visible = true;

                //Boton imprimir visible
                this.btnPrintFast.TabStop = true;
                this.btnPrintFast.Visible = true;
            }

            //Ocultar o mostrar otros datos
            ShowIvaStuff();

            //Resize Vector Excel
            clsGlobales.ArtExcel = (string[,])clsValida.ResizeMatriz(clsGlobales.ProductoComposicion, new int[] { 0, 13 });

            //Habilitar Botones
            this.HabilitarOtros();

            //Tipo Cliente
            if (Convert.ToInt32(cboTipoCliente.SelectedValue) == 1)
            {
                setOtrosItems(1); //FACT. A
            }
            else
            {
                setOtrosItems(2); //FACT. B
            }

            //Ordenamiento
            DeshabilitarOrdenGrillas();
        
        }

        #endregion

        #region Método que deshabilita el reordenamiento de las grilla desde sus cabeceras

        private void DeshabilitarOrdenGrillas()
        {
            // Deshabilito la reordenación de las columnas de las grillas
            foreach (DataGridViewColumn dgvCol in this.dgvArt.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;

            }
        }

        #endregion

        #region Metodo: setOtrosItems

        private void setOtrosItems(int p_Tipo)
        {
            switch (p_Tipo)
            {
                case 1: 
                    this.dgvArt.Columns["IVA"].Visible = true;
                    this.dgvArt.Columns["SubTotalIva"].Visible = true;

                    this.dgvArt.Columns["PrecioCoef"].Visible = true;
                    this.dgvArt.Columns["PrecioIva"].Visible = false;
                    this.dgvArt.Columns["Alicuota"].Visible = true;
                    this.dgvArt.Columns["Subtotal"].Visible = true;

                    this.dgvArt.Columns["Articulo"].Width = 150;
                    break;
                case 2: //Precio - Productos
                    this.dgvArt.Columns["Precio"].Visible = false;
                    this.dgvArt.Columns["PrecioCoef"].Visible = false;
                    this.dgvArt.Columns["PrecioIva"].Visible = true;
                    this.dgvArt.Columns["PrecioIva"].HeaderText = "Precio Unit.";
                    this.dgvArt.Columns["SubTotal"].Visible = true;
                    this.dgvArt.Columns["Alicuota"].Visible = true;
                    this.dgvArt.Columns["IVA"].Visible = false;
                    this.dgvArt.Columns["SubTotalIva"].Visible = false;
                    this.dgvArt.Columns["SubTotalIva"].HeaderText = "SubTotal";

                    this.dgvArt.Columns["Articulo"].Width = 400;
                    break;
            }
        }

        #endregion

        #region Metodo: ShowIvaStuff ( muestra o no IVA10.5 o IVA 21 segun tipo cliente) 

        private void ShowIvaStuff()
        {
            //Ocultar Datos que no van
            if (Convert.ToInt32(this.cboTipoCliente.SelectedValue) == 1) // RESPONSABLE INSCRIPTO
            {
                //Mostrar valor iva
                lblIVA.Visible = true;
                txtIVA.Visible = true;
                lblIVA10.Visible = true;
                txtIVA10.Visible = true;
                //Ocultar linea
                LineaIVA.Visible = false;
                LineaIVA10.Visible = false;

            }
            else
            {
                //Mostrar valor iva
                lblIVA.Visible = false;
                txtIVA.Visible = false;
                lblIVA10.Visible = false;
                txtIVA10.Visible = false;
                //Ocultar linea
                LineaIVA.Visible = true;
                LineaIVA10.Visible = true;
            } 
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
                myPresu.Comentario = row["Comentario"].ToString();
                myPresu.Pendiente = Convert.ToInt32(row["Pendiente"]);
                myPresu.Codigo_Correo = row["Codigo_Correo"].ToString();
            }

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
            txtComentario.Text = myPresu.Comentario;
            cboTransporte.SelectedValue = myPresu.IdTransporte;
            cboCondicionCompra.SelectedValue = myPresu.IdFormaPago;

            //if(!(myPresu.Codigo_Correo.ToUpper()==""))
            //{
                if (clsGlobales.Left(myPresu.Codigo_Correo.ToUpper(), 2) == "CP")
                {
                    rbnCP.Checked = true;
                }
                if (clsGlobales.Left(myPresu.Codigo_Correo.ToUpper(), 2) == "CO")
                {
                    rbnCO.Checked = true;
                }
            //}
           
            txtCodigoCorreo.Text = myPresu.Codigo_Correo;

            if (myPresu.Pendiente == 1)
            { chkPendiente.Checked = true; }
            else { chkPendiente.Checked = false; }
        }

        #endregion

        #region Metodo: CargarDetallePresu

        //CARGA EL DETALLE DEL PRESUPUESTO
        private void CargarDetallePresu()
        {

            //VARIABLES PARA CADENA SQL
            string mySQL = "";
            string myCadenaSQL = "";

            //VACIAR DATASOURCE
            dgvArt.DataSource = null;

            //ARMAR CADENA SQL
            myCadenaSQL = "Select * from Vista_Detalle_Presu_ABM Where IdPresupuesto =  " + Id;

            //EJECUTAR SQL Y A DATATABLE
            DataTable myTabla = clsDataBD.GetSql(myCadenaSQL);

            //DATAGRIDVIEW SIN GENERACION AUTOMATICA DE COLUMNAS
            dgvArt.AutoGenerateColumns = false;

            //VARIABLES NUMERICAS
            double PrecioCoef = 0;
            double Precio = 0;

            //PASAR DATA A TABLA TEMPORAL
            foreach (DataRow row in myTabla.Rows)
            {

                // Si es Excel
                if (Convert.ToInt32(row["Excel"].ToString()) == 1)
                {
                    PrecioCoef = Convert.ToDouble(row["Precio"].ToString());
                    Precio = 0;
                }
                else
                {
                    PrecioCoef = 0;
                    Precio = Convert.ToDouble(row["Precio"].ToString());
                }

                //Si el codigo no existe, insertar
                mySQL = "INSERT INTO " + Tabla + " (Item," +
                                                "IdArticulo," +
                                                "CodigoArticulo," +
                                                "Cantidad," +
                                                "Articulo," +
                                                "Precio," +
                                                "PrecioCoef," +
                                                "SubTotal," +
                                                "IdCoeficiente," +
                                                "Pub," +
                                                "Dist," +
                                                "Rev," +
                                                "Excel," + 
                                                "Orden) values (" + (clsDataBD.RetornarMax(Tabla, "Item") + 1).ToString() + ","
                                                                    + row["IdArticulo"].ToString() + ",'"
                                                                    + row["CodigoArticulo"].ToString() + "',"
                                                                    + Convert.ToInt32(row["Cantidad"].ToString()) + ",'"
                                                                    + row["Articulo"].ToString() + "',"
                                                                    + Precio.ToString("#0.00000") + ","
                                                                    + PrecioCoef.ToString("#0.00000") + ","
                                                                    + Convert.ToDouble("0").ToString("#0.00") + ","
                                                                    + row["Coeficiente"].ToString() + ","
                                                                    + Convert.ToDouble(row["Pub"]).ToString("#0.00") + ","
                                                                    + Convert.ToDouble(row["Dist"]).ToString("#0.00") + ","
                                                                    + Convert.ToDouble(row["Rev"]).ToString("#0.00") + ","
                                                                    + Convert.ToInt32(row["Excel"].ToString()).ToString() + ","
                                                                    + Convert.ToInt32(row["Orden"].ToString()).ToString() + ")"; 
                                                                    //+ clsGlobales.UsuarioLogueado.IdUsuario 

                clsDataBD.GetSql(mySQL);

            }

            //ARMAR SQL DE LA VISTA
            mySQL = "Select * from Vista_" + Tabla + " order by Orden ASC";

            //CARGAR DATABLE A LA GRILLA
            DataTable myTablaExcel = clsDataBD.GetSql(mySQL);

            //CARGAR AL DATATABLE
            dgvArt.AutoGenerateColumns = false;
            dgvArt.DataSource = myTablaExcel;

            //VACIAR VECTOR PRODUCTOS SELECCIONADOS
            clsGlobales.ProductosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.ProductosSeleccionados, new int[] { 0, 2 });

            //REARMAR VECTOR
            int productos = 0;
            //RECORRER GRILLA
            foreach (DataGridViewRow row in dgvArt.Rows)
            {
                //SI EL ARTICULO DE LA GRILLA NO ES EXCEL
                if (Convert.ToInt32(row.Cells["Excel"].Value.ToString()) == 0)
                {
                    // Redimensiono el tamaño de la matriz de Insumos
                    clsGlobales.ProductosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.ProductosSeleccionados, new int[] { productos + 1, 2 });
                    // A la posición creada le asigno el Id seleccionado y la cantidad cargada
                    clsGlobales.ProductosSeleccionados[productos, 0] = Convert.ToDouble(row.Cells["IdArticulo"].Value);
                    clsGlobales.ProductosSeleccionados[productos, 1] = Convert.ToDouble(row.Cells["Cantidad"].Value);
                    // Aumento el contador de insumos
                    productos++;
                }
            }


            //HABILITAR BOTON ACEPTAR
            btnAceptar.Enabled = true;

            //SI HAY DATOS
            if (dgvArt.Rows.Count > 0)
            {
                //FOCO EN LA GRILLA
                dgvArt.Focus();
                //PARARSE EN CAMPO CANTIDAD
                dgvArt.CurrentCell = dgvArt["Cantidad", 0];
            }

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
                myPedido.IdUsuario = clsGlobales.UsuarioLogueado.IdUsuario;
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
            cboEntrada.Text = myPedido.Entrada;
            lblPunto.Text = myPedido.Punto.ToString("D4");
            nroComp.Text = myPedido.Nro.ToString("D8");
            txtDto.Text = myPedido.Dto.ToString("#0.00");
            txtCostoFlete.Text = myPedido.Flete.ToString("#0.00");
            cboTransporte.SelectedValue = myPedido.IdTransporte;
            cboCondicionCompra.SelectedValue = myPedido.IdFormaPago;
        }

        #endregion

        #region Metodo: CargarDetallePedido

        //CARGA EL DETALLE DEL PEDIDO A LA GRILLA
        private void CargarDetallePedido()
        {
        
            //VARIABLES PARA CADENA SQL
            string mySQL = "";
            string myCadenaSQL = "";

            //VACIAR DATASOURCE
            dgvArt.DataSource = null;

            //ARMAR CADENA SQL
             myCadenaSQL = "Select * from Vista_Detalle_Pedido_ABM Where IdPedido =  " + Id;

            //EJECUTAR SQL Y A DATATABLE
            DataTable myTabla = clsDataBD.GetSql(myCadenaSQL);

            //DATAGRIDVIEW SIN GENERACION AUTOMATICA DE COLUMNAS
            dgvArt.AutoGenerateColumns = false;

            //VARIABLES NUMERICAS
            double PrecioCoef = 0;
            double Precio = 0;

            //PASAR DATA A TABLA TEMPORAL
            foreach (DataRow row in myTabla.Rows)
            {

               // Si es Excel
                if (Convert.ToInt32(row["Excel"].ToString()) == 1)
                {
                    PrecioCoef = Convert.ToDouble(row["Precio"].ToString());
                    Precio = 0;
                }
                else
                {
                    PrecioCoef = 0;
                    Precio = Convert.ToDouble(row["Precio"].ToString());
                }

               //Si el codigo no existe, insertar
                 mySQL = "INSERT INTO " + Tabla + " (Item," +
                                                 "IdArticulo," +
                                                 "CodigoArticulo," +
                                                 "Cantidad," +
                                                 "Articulo," +
                                                 "Precio," +
                                                 "PrecioCoef," +
                                                 "SubTotal," +
                                                 "IdCoeficiente," +
                                                 "Pub," +
                                                 "Dist," +
                                                 "Rev," +
                                                 "Excel," + 
                                                 "Orden," +
                                                "IdUsuario) values (" + (clsDataBD.RetornarMax(Tabla, "Item") + 1).ToString() + ","
                                                                    + row["IdArticulo"].ToString() + ",'"
                                                                    + row["CodigoArticulo"].ToString() + "',"
                                                                    + Convert.ToInt32(row["Cantidad"].ToString()) + ",'"
                                                                    + row["Articulo"].ToString() + "',"
                                                                    + Precio.ToString("#0.00000") + ","
                                                                    + PrecioCoef.ToString("#0.00000") + ","
                                                                    + Convert.ToDouble("0").ToString("#0.00") + ","
                                                                    + row["Coeficiente"].ToString() + ","
                                                                    + Convert.ToDouble(row["Pub"]).ToString("#0.00") + ","
                                                                    + Convert.ToDouble(row["Dist"]).ToString("#0.00") + ","
                                                                    + Convert.ToDouble(row["Rev"]).ToString("#0.00") + ","
                                                                    + Convert.ToInt32(row["Excel"].ToString()).ToString() + ","
                                                                    + Convert.ToInt32(row["Orden"].ToString()).ToString() + ","
                                                                    + clsGlobales.UsuarioLogueado.IdUsuario + ")";

                 clsDataBD.GetSql(mySQL);

             }

             //ARMAR SQL DE LA VISTA
              mySQL = "Select * from Vista_" + Tabla + " Where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario + " ORDER BY Orden ASC";

             //CARGAR DATABLE A LA GRILLA
             DataTable myTablaExcel = clsDataBD.GetSql(mySQL);

             //CARGAR AL DATATABLE
             dgvArt.AutoGenerateColumns = false;
             dgvArt.DataSource = myTablaExcel;

            //VACIAR VECTOR PRODUCTOS SELECCIONADOS
             clsGlobales.ProductosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.ProductosSeleccionados, new int[] {0, 2 });

            //REARMAR VECTOR
            int productos = 0;
            //RECORRER GRILLA
            foreach (DataGridViewRow row in dgvArt.Rows)
            {
                //SI EL ARTICULO DE LA GRILLA NO ES EXCEL
                if (Convert.ToInt32(row.Cells["Excel"].Value.ToString()) == 0)
                {
                    // Redimensiono el tamaño de la matriz de Insumos
                    clsGlobales.ProductosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.ProductosSeleccionados, new int[] { productos + 1, 2 });
                    // A la posición creada le asigno el Id seleccionado y la cantidad cargada
                    clsGlobales.ProductosSeleccionados[productos, 0] = Convert.ToDouble(row.Cells["IdArticulo"].Value);
                    clsGlobales.ProductosSeleccionados[productos, 1] = Convert.ToDouble(row.Cells["Cantidad"].Value);
                    // Aumento el contador de insumos
                    productos++;
                }
            }
            

            //HABILITAR BOTON ACEPTAR
            btnAceptar.Enabled = true;

            //SI HAY DATOS
            if (dgvArt.Rows.Count > 0)
            {
                //FOCO EN LA GRILLA
                dgvArt.Focus();
                //PARARSE EN CAMPO CANTIDAD
                dgvArt.CurrentCell = dgvArt["Cantidad", 0];
            }

            //VERIFICAR CAMPOS Y CALCULOS
            this.VerificarCampos();

        }

        #endregion

        #region CargarToolTips

        private void CargarToolTips()
        {
            toolTip1.SetToolTip(this.btnAgregarArt, "Agregar Artículo");
            toolTip2.SetToolTip(this.btnQuitarArt, "Quitar Artículo");
            toolTip3.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip4.SetToolTip(this.btnSalir, "Salir");
            toolTip5.SetToolTip(this.btnCli, "Clientes (Ver o Añadir)!");
            toolTip6.SetToolTip(this.btnAgregaArtExcl, "Agregar Artículo (Excel)");
            toolTip7.SetToolTip(this.btnEditCli, "Cliente (Editar)!");
            //VERIFICAR POR UNO U OTRO
            if (!(bPresupuesto))
            { toolTip8.SetToolTip(this.btnImprimir, "Imprimir Pedido (Vista Previa)!"); }
            else
            { toolTip8.SetToolTip(this.btnImprimir, "Imprimir Presupuesto (Vista Previa)!");}
            toolTip9.SetToolTip(this.btnCostoFlete, "Calcular!");
            toolTip10.SetToolTip(this.btnDeshacer, "Deshacer");
            toolTip11.SetToolTip(this.btnAddCli, "Nuevo Cliente!");
            //VERIFICAR POR UNO U OTRO
            if (!(bPresupuesto))
            { toolTip12.SetToolTip(this.btnPrintFast, "Imprimir Pedido (Rápido)!"); }
            else
            { toolTip12.SetToolTip(this.btnPrintFast, "Imprimir Presupuesto (Rápido)!"); }
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
            cboEntrada.TabStop = false;
            cboEntrada.Visible = false;

            ////Marca Pendiente  
            //if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja)
            //{
            //   this.chkPendiente.Visible = true;
            //   this.chkPendiente.Enabled = true;
            //   this.chkPendiente.TabStop = true;
            //}

            this.chkPendiente.Visible = true;
            this.chkPendiente.Enabled = true;
            this.chkPendiente.TabStop = true;
        }

        #endregion

        #region Eventos Click Botones

        private void btnAgregarArt_Click(object sender, EventArgs e)
        {
            int LargoProductos = clsGlobales.ProductosSeleccionados.GetLength(0);

            bool bHayData = false;

            if (this.dgvArt.Rows.Count > 0) { bHayData = true; }

            //LLAMAR AL FORMULARIO DE BUSQUEDA DE ARTICULOS
            frmArticulosOtraBusqueda myForm = new frmArticulosOtraBusqueda(false, true, this.Tabla, bHayData);
            // Muestro el formulario
            myForm.ShowDialog();

            //VARIABLES QUE VA A ALMACENAR LOS ID'S DE PRODUCTOS
            string sArt = "";

            //VARIABLE QUE CONTIENE VECTOR DE PRODUCTOS
            LargoProductos = clsGlobales.ProductosSeleccionados.GetLength(0);
            
            //SI EL HAU DATOS
            if (!(LargoProductos == 0))
            {
                //RECORRER EL VECTOR PARA CARGAR LOS ID'S.
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
            else
            {
              //SALIR
                return;
            }


            //Vaciar grilla
            dgvArt.DataSource = null;
            
            //Variable para guardar el precio con Iva
            double dPrecioIva =  0;
            int p_ParamUser = 0;

            if (!(bPresupuesto))
            { p_ParamUser = clsGlobales.UsuarioLogueado.IdUsuario;  }
            else
            { p_ParamUser = 0; }

            // si hay cargados productos en el vector
            if (!(clsGlobales.ProductosSeleccionados.Length == 0))
            {
                
                //Variables
                string mySQL = "";

                //ARMAR SQL DE LOS ARTICULOS NO EXCEL
                mySQL = "select * from Articulos_Productos where IdArticulo in (" + sArt + ")";

                // Creo una tabla que me va a almacenar el resultado de la consulta
                DataTable myTabla = clsDataBD.GetSql(mySQL);

                //RECORRER DATATABLE Y CARGAR A LA GRILLA
                foreach (DataRow row in myTabla.Rows)
                {
                  //SOLO INSERTAR SI EL ARTICULO NO EXISTE!
                    if (!(NoExisteEnTabla("IdArticulo", Convert.ToInt32(row["IdArticulo"]), p_ParamUser)))
                    {

                        //Precio Iva
                        dPrecioIva  = 0;

                        if (!(bPresupuesto))
                        {
                            //Si el codigo no existe, insertar
                            mySQL = "INSERT INTO " + Tabla + " (Item," +
                                                        "IdArticulo," +
                                                        "CodigoArticulo," +
                                                        "Cantidad," +
                                                        "Articulo," +
                                                        "Precio," +
                                                        "PrecioCoef," +
                                                        "PrecioIva," +
                                                        "SubTotal," +
                                                        "IdCoeficiente," +
                                                        "Pub," +
                                                        "Dist," +
                                                        "Rev," +
                                                        "Excel," + 
                                                        "Orden," + 
                                                        "IdUsuario)"; 
                            
                        }
                        else
                        {
                                                        //Si el codigo no existe, insertar
                            mySQL = "INSERT INTO " + Tabla + " (Item," +
                                                        "IdArticulo," +
                                                        "CodigoArticulo," +
                                                        "Cantidad," +
                                                        "Articulo," +
                                                        "Precio," +
                                                        "PrecioCoef," +
                                                        "PrecioIva," +
                                                        "SubTotal," +
                                                        "IdCoeficiente," +
                                                        "Pub," +
                                                        "Dist," +
                                                        "Rev," +
                                                        "Excel," + 
                                                        "Orden)";
                        }


                        //añadir *************************************************************************************************
                                                         mySQL+= " values (" + (clsDataBD.RetornarMax(Tabla, "Item") + 1).ToString() + ","
                                                                            + row["IdArticulo"].ToString() + ",'"
                                                                            + row["CodigoArticulo"].ToString() + "',"
                                                                            + RetornarCantidad(Convert.ToInt32(row["IdArticulo"].ToString())) + ",'"
                                                                            + row["Articulo"].ToString() + "',"
                                                                            + Convert.ToDouble(row["Precio"]).ToString("#0.00000") + ","
                                                                            + Convert.ToDouble("0").ToString("#0.00000") + ","
                                                                            + dPrecioIva.ToString("#0.00000") + ","
                                                                            + Convert.ToDouble("0").ToString("#0.00") + ","
                                                                            + row["Coeficiente"].ToString() + ","
                                                                            + Convert.ToDouble(row["Pub"]).ToString("#0.00") + ","
                                                                            + Convert.ToDouble(row["Dist"]).ToString("#0.00") + ","
                                                                            + Convert.ToDouble(row["Rev"]).ToString("#0.00") + ","
                                                                            + Convert.ToInt32("0").ToString() + ","
                                                                            + RetornarOrden(Convert.ToInt32(row["IdArticulo"].ToString()));

                                                         if (!(bPresupuesto))
                                                         {
                                                             mySQL += "," + clsGlobales.UsuarioLogueado.IdUsuario + ")";
                                                         }
                                                         else
                                                         {
                                                             mySQL += ")";
                                                         }
                                                                                            

                        clsDataBD.GetSql(mySQL);
                    }
                
                }

                //ARMAR SQL DE LA VISTA
                if (!(bPresupuesto))
                {
                    mySQL = "select * from Vista_" + Tabla + " Where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario + " ORDER BY Orden ASC";
                }
                else
                {
                    mySQL = "select * from Vista_" + Tabla + " ORDER BY Orden ASC";
                }

                //CARGAR DATABLE A LA GRILLA
                DataTable myTablaExcel = clsDataBD.GetSql(mySQL);

                //CARGAR AL DATATABLE
                dgvArt.AutoGenerateColumns = false;
                dgvArt.DataSource = myTablaExcel;

                //VERIFICAR CAMPOS ( ACTIVACIONES, VISIBILIDADES )
                this.VerificarCampos();

                //CALCULAR
                this.CalcularSubtotal();

                //CALCULAR TOTAL
                this.CalcularTotal();

                //HABILITAR OTROS BOTONES
                btnAceptar.TabStop = true;
                btnAceptar.Enabled = true;
                btnQuitarArt.TabStop = true;
                btnQuitarArt.Enabled = true;

                //FOCO GRILLA SI HAY DATOS
                dgvArt.Focus();

                //PONER FOCO EN CANTIDAD
                if (myTablaExcel.Rows.Count > 0)
                {
                   dgvArt.CurrentCell = dgvArt["Cantidad", 0];
                }
            }

        }

        #region Metodo RetornarOrden

        //RETORNA ORDEN DE UBICACION DE CARGA DEL ARTICULO ID
        private int RetornarOrden(int p_Id)
        {
            int Retorno = 0;
            
            //VARIABLE QUE CONTIENE VECTOR DE PRODUCTOS
            int LargoProductos = clsGlobales.ProductosSeleccionados.GetLength(0);

            if (this.myEstado == "M")
            {
                //Traer la vista con los datos ya cargados
                string myCadena = "";

                if (!(bPresupuesto)) //SINO ES PRESUPUESTO
                {
                    myCadena = "select * from Vista_" + Tabla + " WHERE IdArticulo = " + p_Id + " and IdUsuario= " + clsGlobales.UsuarioLogueado.IdUsuario +
                    " Order by Orden ASC";
                }
                else
                {
                    myCadena = "select * from Vista_" + Tabla + " WHERE IdArticulo = " + p_Id + " Order by Orden ASC";
                }
                DataTable mData = new DataTable();
                mData = clsDataBD.GetSql(myCadena);

                //Recorrer y subir al vector
                foreach (DataRow myRow in mData.Rows)
                {
                    //Traer el Nro Orden
                    Retorno = Convert.ToInt32(myRow["Orden"].ToString());
                }

                //NO EXISTE? Nuevo Orden
                if (Retorno == 0)
                {
                    if (!(bPresupuesto)) //SINO ES PRESUPUESTO
                    {
                        Retorno = clsDataBD.RetornarMax("Vista_" + Tabla, "Orden", clsGlobales.UsuarioLogueado.IdUsuario) + 1;
                    }
                    else
                    {
                        Retorno = clsDataBD.RetornarMax("Vista_" + Tabla, "Orden") + 1;
                    }
                }
            }
            else if (this.myEstado=="A")
            {
                //RECORRER EL VECTOR PARA CARGAR LOS ID'S.
                for (int i = 0; i < LargoProductos; i++)
                {
                    // Si no es el último
                    if (p_Id == clsGlobales.ProductosSeleccionados[i, 0])
                    {

                        if (!(bPresupuesto)) //SINO ES PRESUPUESTO
                        {
                            //Retornar posicion
                            Retorno = clsDataBD.RetornarMax("Vista_" + Tabla, "Orden", clsGlobales.UsuarioLogueado.IdUsuario) + 1;
                            break;
                        }
                        else
                        {
                            Retorno = clsDataBD.RetornarMax("Vista_" + Tabla, "Orden") + 1;
                            break;
                        }
                       
                    }
                }
            }

            //RETORNAR VALOR
            return Retorno;
        }

        #endregion 

        #region Metodo RetornarCantidad

        //RETORNA ORDEN DE UBICACION DE CARGA DEL ARTICULO ID
        private int RetornarCantidad(int p_Id)
        {
            int Retorno = 0;

            //VARIABLE QUE CONTIENE VECTOR DE PRODUCTOS
            int LargoProductos = clsGlobales.ProductosSeleccionados.GetLength(0);

            //RECORRER EL VECTOR PARA CARGAR LOS ID'S.
            for (int i = 0; i < LargoProductos; i++)
            {
                if (p_Id == Convert.ToInt32(clsGlobales.ProductosSeleccionados[i, 0].ToString()))
                {
                    Retorno = Convert.ToInt32(clsGlobales.ProductosSeleccionados[i, 1].ToString());
                    break;
                }
            }

            return Retorno;
        }

        #endregion 

        private void btnAgregaArtExcl_Click(object sender, EventArgs e)
        {
            //Variables
            int CantColumnExcel = 0;

            //abrimos el dialogo para poder obtener el nombre la ubicacion del archivo
            ofdAbrirArchivo.Filter = "Archivos Excel (*.xls) |*.xls|All Files (*.*)|*.*";
            ofdAbrirArchivo.FilterIndex = 1;

            //Leer Archivo xls
            if (ofdAbrirArchivo.ShowDialog() == DialogResult.OK)
            {
                //OBTENER NOMBRE ARCHIVO EXCEL 
                string sArchivo = ofdAbrirArchivo.FileName;

                //Variable para controlar actualizacion de precios
                bool bActualizar = false;

                //VARIABLE PARA GUARDAR RESULTADO
                bool bResult = false;
              
                //En .F. porque no se sabe que tipo de archivo viene.
                clsGlobales.bNuevoExcel = false;

                //CAMBIAR PUNTERO MOUSE
                Cursor.Current = Cursors.WaitCursor;

                //Fecha Archivo? +15 días?
                //DateTime FechaPub = Convert.ToDateTime("12/26/2017");
                DateTime FechaPub = clsGlobales.cFormato.RetornarFechaExcel(sArchivo);

                //Calcular Diferencia en dìas
                TimeSpan Ts = DateTime.Now - FechaPub;
                int DifDays = Ts.Days;

                //Obtener la cantidad de columnas del archivo excel
                CantColumnExcel = clsGlobales.cFormato.RetornarCantColumnExcel(sArchivo);


                //Preguntar? /////
                if (DifDays >= 15)
                {
                    DialogResult dlResult = MessageBox.Show("El Presupuesto que se intenta cargar tiene 15 o más días de antigüo, quiere <Actualizar> los precios con los del Sistema?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    // Si confirma... cambiar estado
                    if (dlResult == DialogResult.Yes)
                    {
                        //Actualiza Precios Excel
                        bActualizar = true;
                    }
                }

               
                 //LEER EL EXCEL

                if (Convert.ToInt32(this.cboTipo.SelectedValue) == 28) // Publico
                {
                   bResult = clsGlobales.cFormato.LeerExcel(sArchivo, CantColumnExcel, false, false);
                }
                else if (Convert.ToInt32(this.cboTipo.SelectedValue) == 29) //Es Distribuidor
                {
                    bResult = clsGlobales.cFormato.LeerExcel(sArchivo, CantColumnExcel, false, true);
                }
                else if (Convert.ToInt32(this.cboTipo.SelectedValue) == 30) //Es Revendedor
                {
                    bResult = clsGlobales.cFormato.LeerExcel(sArchivo, CantColumnExcel, true, false);
                }

                //RETORNAR CURSOR (PUNTERO MOUSE)
                Cursor.Current = Cursors.Default;

                //SI TODO SALIO BIEN...
                if (bResult)
                {
                    if (!(bActualizar))
                    {
                        //CARGAR VECTOR EXCEL A TABLA TEMPORAL
                        CargarVecExcelATabla();

                        //CARGAR GRLLA
                        CargarGrilla();

                        //VERIFICAR CAMPOS ( ACTIVACIONES, VISIBILIDADES )
                        this.VerificarCampos();

                        //CALCULAR SUBTOTAL
                        this.CalcularSubtotal();

                        //CALCULAR EL TOTAL
                        this.CalcularTotal();

                        //MENSAJE POR SI HABIA DUPLICADOS
                        if (bExisten)
                        {
                            MessageBox.Show("La Importación desde el Archivo Excel ha finalizado correctamente!. Algunos productos NO se ingresaron dado que ya se encuentran cargados!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("La Importación desde el Archivo Excel ha finalizado correctamente!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        //Apagar Flag
                        clsGlobales.bNuevoExcel = false;

                    }
                    else
                    {
                        //CARGAR VECTOR EXCEL A VECTOR DE PRODUCTOS
                        CargarVecExcelAVec();

                        //VARIABLE CON ID DE ARTICULOS
                        string sArt = "";

                        //VARIABLE CON LARGO VECTOR PRODUCTOS
                        int LargoProductos = clsGlobales.ProductosSeleccionados.GetLength(0);

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

                        //Vaciar grilla
                        dgvArt.DataSource = null;

                        //Variable para guardar el precio con Iva
                        double dPrecioIva = 0;

                        // si hay cargados productos en el vector
                        if (!(clsGlobales.ProductosSeleccionados.Length == 0))
                        {

                            //Variables
                            string mySQL = "";

                            //ARMAR SQL DE LOS ARTICULOS NO EXCEL
                            mySQL = "select * from Articulos_Productos where IdArticulo in (" + sArt + ")";

                            // Creo una tabla que me va a almacenar el resultado de la consulta
                            DataTable myTabla = clsDataBD.GetSql(mySQL);

                            //RECORRER DATATABLE Y CARGAR A LA GRILLA
                            foreach (DataRow row in myTabla.Rows)
                            {
                                //SOLO INSERTAR SI EL ARTICULO NO EXISTE!
                                if (!(NoExisteEnTabla("IdArticulo", Convert.ToInt32(row["IdArticulo"]))))
                                {

                                    //Precio Iva
                                    dPrecioIva = 0;

                                    if (!(bPresupuesto))
                                    {
                                        //Si el codigo no existe, insertar
                                        mySQL = "INSERT INTO " + Tabla + " (Item," +
                                                                        "IdArticulo," +
                                                                        "CodigoArticulo," +
                                                                        "Cantidad," +
                                                                        "Articulo," +
                                                                        "Precio," +
                                                                        "PrecioCoef," +
                                                                        "PrecioIva," +
                                                                        "SubTotal," +
                                                                        "IdCoeficiente," +
                                                                        "Pub," +
                                                                        "Dist," +
                                                                        "Rev," +
                                                                        "Excel," +
                                                                        "Orden," +
                                                                        "IdUsuario)";
                                    }
                                    else
                                    {
                                        //Si el codigo no existe, insertar
                                        mySQL = "INSERT INTO " + Tabla + " (Item," +
                                                                        "IdArticulo," +
                                                                        "CodigoArticulo," +
                                                                        "Cantidad," +
                                                                        "Articulo," +
                                                                        "Precio," +
                                                                        "PrecioCoef," +
                                                                        "PrecioIva," +
                                                                        "SubTotal," +
                                                                        "IdCoeficiente," +
                                                                        "Pub," +
                                                                        "Dist," +
                                                                        "Rev," +
                                                                        "Excel," +
                                                                        "Orden)";
                                    }
                                   
                                   //AÑADIR *******************************************************************************************
                                    mySQL+= " values (" + (clsDataBD.RetornarMax(Tabla, "Item") + 1).ToString() + ","
                                                        + row["IdArticulo"].ToString() + ",'"
                                                        + row["CodigoArticulo"].ToString() + "',"
                                                        + RetornarCantidad(Convert.ToInt32(row["IdArticulo"].ToString())) + ",'"
                                                        + row["Articulo"].ToString() + "',"
                                                        + Convert.ToDouble(row["Precio"]).ToString("#0.00000") + ","
                                                        + Convert.ToDouble("0").ToString("#0.00000") + ","
                                                        + dPrecioIva.ToString("#0.00000") + ","
                                                        + Convert.ToDouble("0").ToString("#0.00") + ","
                                                        + row["Coeficiente"].ToString() + ","
                                                        + Convert.ToDouble(row["Pub"]).ToString("#0.00") + ","
                                                        + Convert.ToDouble(row["Dist"]).ToString("#0.00") + ","
                                                        + Convert.ToDouble(row["Rev"]).ToString("#0.00") + ","
                                                        + Convert.ToInt32("0").ToString() + ","
                                                        + RetornarOrden(Convert.ToInt32(row["IdArticulo"].ToString()));


                                                        if (!(bPresupuesto))
                                                        {
                                                            mySQL+= "," + clsGlobales.UsuarioLogueado.IdUsuario + ")";
                                                        }
                                                        else
                                                        {
                                                            mySQL += ")";
                                                        }


                                    clsDataBD.GetSql(mySQL);
                                }

                            }

                            //ARMAR SQL DE LA VISTA
                            mySQL = "select * from Vista_" + Tabla + " Where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario + " ORDER BY Orden ASC";

                            //CARGAR DATABLE A LA GRILLA
                            DataTable myTablaExcel = clsDataBD.GetSql(mySQL);

                            //CARGAR AL DATATABLE
                            dgvArt.AutoGenerateColumns = false;
                            dgvArt.DataSource = myTablaExcel;

                            //VERIFICAR CAMPOS ( ACTIVACIONES, VISIBILIDADES )
                            this.VerificarCampos();

                            //CALCULAR
                            this.CalcularSubtotal();

                            //CALCULAR TOTAL
                            this.CalcularTotal();

                            //HABILITAR OTROS BOTONES
                            btnAceptar.TabStop = true;
                            btnAceptar.Enabled = true;
                            btnQuitarArt.TabStop = true;
                            btnQuitarArt.Enabled = true;

                            //FOCO GRILLA SI HAY DATOS
                            dgvArt.Focus();

                            //PONER FOCO EN CANTIDAD
                            if (myTablaExcel.Rows.Count > 0)
                            {
                                dgvArt.CurrentCell = dgvArt["Cantidad", 0];
                            }
                        }                      
                    }

                    //Apagar Flag
                    clsGlobales.bNuevoExcel = false;
                }
                else
                {
                    //Apagar Flag
                    clsGlobales.bNuevoExcel = false;
                }
                             
                //SI HAY DATOS REPOSICIONAR GRILA
                if (dgvArt.Rows.Count > 0)
                {
                    // Paso el foco a la grilla
                    dgvArt.Focus();
                    // Pongo el foco en el campo cantidad
                    dgvArt.CurrentCell = dgvArt["Cantidad", 0];

                    //HABILITAR OTROS BOTONES
                    btnAceptar.TabStop = true;
                    btnAceptar.Enabled = true;
                    btnQuitarArt.TabStop = true;
                    btnQuitarArt.Enabled = true;
                }
            }
        }

        #region Metodo: CargarVecExcelAVec

        //METODO QUE CARGA EL VECTOR DE EXCEL A LA GRILLA
        private void CargarVecExcelAVec()
        {
            // Redimensiono el tamaño de la matriz
            clsGlobales.ProductosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.ProductosSeleccionados, new int[] { clsGlobales.ArtExcel.GetLength(0), 2 });

            //Mostrar Datos
            for (int iterador = 0; iterador < clsGlobales.ArtExcel.GetLength(0); iterador++)
            {

                // A la posición creada le asigno el Id seleccionado
                clsGlobales.ProductosSeleccionados[iterador, 0] = Convert.ToDouble(clsGlobales.ArtExcel[iterador, 1].ToString());
                clsGlobales.ProductosSeleccionados[iterador, 1] = Convert.ToDouble(clsGlobales.ArtExcel[iterador, 3].ToString());
            }
        }

        #endregion

        private void btnQuitarArt_Click(object sender, EventArgs e)
        {
            int LargoProductos = clsGlobales.ProductosSeleccionados.GetLength(0);

            // Recorro el vector y quitar de productos seleccionados
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

            //Obtener fila actual
            DataGridViewRow row = dgvArt.CurrentRow;          
  
            //Quitar de la tabla tamporal tambien

            string mySQL = "";

            if (!(bPresupuesto))
            {
                mySQL = "Delete from " + Tabla + " Where IdArticulo = " + Convert.ToInt32(row.Cells["IdArticulo"].Value) + " And IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario;
            }
            else
            {
                mySQL = "Delete from " + Tabla + " Where IdArticulo = " + Convert.ToInt32(row.Cells["IdArticulo"].Value);
            }
            clsDataBD.GetSql(mySQL);

            // Elimino la fila de la grilla
            dgvArt.Rows.RemoveAt(dgvArt.CurrentRow.Index);           
            
            //Controles
            this.HabilitarControles();
            // Recalculo el total de la orden de compra
            CalcularTotal();

            //Recorrer la grilla y renumerar en tabla temporal
            UpdateItemNumber();

            //Si hay datos, reposicionarse
            if (dgvArt.Rows.Count > 0)
            {
                // Paso el foco a la grilla
                dgvArt.Focus();
                // Pongo el foco en el campo cantidad
                dgvArt.CurrentCell = dgvArt["Cantidad", 0]; 
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //Eliminar tabla temporal
            string mySQL = "";
            if (!(bPresupuesto))
            { 
                mySQL="Delete from " + Tabla + " Where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario; 
            }
            else
            { 
                mySQL="Delete from " + Tabla; 
            }
            clsDataBD.GetSql(mySQL);
            //Eliminar
            this.EliminarClienteSeleccionado();
            //Cerrar
            this.Close();
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
            //Habilitar
            this.HabilitarOtros();
            //Calculos
            this.CalcularSubtotal();
            this.CalcularTotal();
            //Retorna
            if (clsGlobales.ClientesSeleccionados.GetLength(0) > 0)
            {
                //Inhabilitar Boton
                this.btnEditCli.Enabled = true;
                this.btnCli.Enabled = false;
                this.btnCostoFlete.Enabled = true;

                //Ocultar o mostrar otros datos
                ShowIvaStuff();
            }
        }

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

                    //Actualizar el numero de pedido en la tabla Puntos de Venta afip
                    int myUltPedido = clsDataBD.RetornarMax("PuntosVentaAFIP", "Ult_Pedido") + 1;
                    string mySQL = "UPDATE PuntosVentaAFIP SET Ult_Pedido = " + myUltPedido + " WHERE Punto = " + clsGlobales.cParametro.PtoVtaPorDefecto;
                    clsDataBD.GetSql(mySQL);
                    //

                    //PrintFast
                    ImprimirPedPresu();
                }
                else
                {
                    this.ModificarPedido();
                    this.ModificarDetallePedido();
                    //PrintFast
                    ImprimirPedPresu();
                }
            }
            else
            {
                  this.ModificarPresu();          
                  this.ModificarDetallePresu();
                  //PrintFast
                  ImprimirPedPresu();
            }

            //Salir
            btnSalir.PerformClick();    
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
            myPedido.Entrada = cboEntrada.Text;
            myPedido.Fecha = dtFecha.Value;
            myPedido.Excel = 1;
            myPedido.IdUsuario = clsGlobales.UsuarioLogueado.IdUsuario;

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
            myPedido.Nro = clsDataBD.getUltComp("Ult_Pedido", clsGlobales.cParametro.PtoVtaPorDefecto, 0) + 1; //
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
                                                " Excel," + 
                                                " IdUsuario," +
                                                " EditMode) values (" + myPedido.IdPedido + ","
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
                                                                    + myPedido.Excel + "," 
                                                                    + myPedido.IdUsuario + "," 
                                                                    + 0 + ")";                

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
                myDetalle.Codigo_Articulo = row.Cells["CodigoArticulo"].Value.ToString();
                myDetalle.Excel = Convert.ToInt32(row.Cells["Excel"].Value);
                myDetalle.Activo = 1;
                if (row.Cells["nOrden"].Value is DBNull)
                {
                    myDetalle.Orden = 0;

                }
                else
                {
                    myDetalle.Orden = Convert.ToInt32(row.Cells["nOrden"].Value);
                }

                //VALIDAR SI ES EXCEL O NO EL ARTICULO CARGADO PARA VER QUE PRECIO
                //CARGAR
                if (myDetalle.Excel == 1)
                {
                   myDetalle.PrecioUnitario = Convert.ToDouble(row.Cells["PrecioCoef"].Value);
                }
                else
                {
                   myDetalle.PrecioUnitario = Convert.ToDouble(row.Cells["Precio"].Value); 
                }


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
                                                           " Excel," +
                                                           " Orden)" +
                                                           "  values (" + myDetalle.IdDetallePedido + "," +
                                                                          myDetalle.IdPedido + "," +
                                                                          myDetalle.IdArticulo + ",'" +
                                                                          myDetalle.Codigo_Articulo + "'," +
                                                                          myDetalle.Cantidad.ToString().Replace(",", ".") + ",'" +
                                                                          myDetalle.Descripcion + "'," +
                                                                          myDetalle.PrecioUnitario.ToString().Replace(",", ".") + "," +
                                                                          myDetalle.Activo + "," + 
                                                                          myDetalle.Excel + "," +
                                                                          myDetalle.Orden + ")";


                clsDataBD.GetSql(myCadSQL);

            }
        }

        #endregion

        #region Metodo: ModificarPresu

        private void ModificarPresu()
        {
            string sAux = "";

            //GRABAR PRESUPUESTO
            myPresu.IdPresupuesto = this.Id;
            myPresu.IdCliente = this.IdCliente;
            myPresu.IdFormaPago = Convert.ToInt32(cboCondicionCompra.SelectedValue);
            myPresu.IdTransporte = Convert.ToInt32(cboTransporte.SelectedValue);
            myPresu.Fecha = dtFecha.Value;
            myPresu.Excel = 1;
            myPresu.IdUsuario = clsGlobales.UsuarioLogueado.IdUsuario;

            //No se puede deshacer...
            if (this.chkPendiente.Checked)
            {
               myPresu.Pendiente = 1;
            }
            else
            {
               myPresu.Pendiente = 0;
            }

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
           
            //Codigo Correo
            if (string.IsNullOrEmpty(this.txtCodigoCorreo.Text))
            {
                myPresu.Codigo_Correo = "No establecido";
            }
            else
            {
                if (!(clsGlobales.Left(txtCodigoCorreo.Text.ToUpper(), 2) == "CP" || clsGlobales.Left(txtCodigoCorreo.Text.ToUpper(), 2) == "CO"))
                {
                    if (rbnCP.Checked) { sAux = "CP"; }
                    if (rbnCO.Checked) { sAux = "CO"; }
                     myPresu.Codigo_Correo = sAux + txtCodigoCorreo.Text;
                }
                else
                {
                    myPresu.Codigo_Correo = txtCodigoCorreo.Text; 
                }
            }

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
                                                            " Excel = " + myPresu.Excel + "," +
                                                            " Pendiente = " + myPresu.Pendiente + "," +
                                                            " Codigo_Correo  = '" + myPresu.Codigo_Correo + "' " + 
//                                                            " IdUsuario = " + myPresu.IdUsuario + "," +
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
                //LastIdDetalle = clsDataBD.RetornarUltimoId("DetallePresupuestos", "IdDetPresupuesto") + 1;
                LastIdDetalle = clsDataBD.RetornarMax("DetallePresupuestos", "IdDetPresupuesto") + 1;

                //IdArticulo Clave Principal y relacion con factura
                myDetPresu.IdDetPresupuesto = LastIdDetalle;
                myDetPresu.IdPresupuesto = Id;
                myDetPresu.IdArticulo = Convert.ToInt32(row.Cells["IdArticulo"].Value);
                myDetPresu.Descripcion = row.Cells["Articulo"].Value.ToString();
                myDetPresu.Cantidad = Convert.ToDouble(row.Cells["Cantidad"].Value);
                myDetPresu.Excel = Convert.ToInt32(row.Cells["Excel"].Value.ToString());
                myDetPresu.Orden = Convert.ToInt32(row.Cells["nOrden"].Value);

                //VALIDAR SI ES EXCEL O NO EL ARTICULO CARGADO PARA VER QUE PRECIO
                //CARGAR
                if (myDetPresu.Excel == 1)
                {
                    myDetPresu.PrecioUnitario = Convert.ToDouble(row.Cells["PrecioCoef"].Value);
                }
                else
                {
                    myDetPresu.PrecioUnitario = Convert.ToDouble(row.Cells["Precio"].Value);
                }

                myDetPresu.Codigo_Articulo = row.Cells["CodigoArticulo"].Value.ToString();
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
                                                           " Excel," + 
                                                           " Orden)" +
                                                           "  values (" + myDetPresu.IdDetPresupuesto + "," +
                                                                             myDetPresu.IdPresupuesto + "," +
                                                                             myDetPresu.IdArticulo + ",'" +
                                                                             myDetPresu.Codigo_Articulo + "'," +
                                                                             myDetPresu.Cantidad.ToString().Replace(",", ".") + ",'" +
                                                                             myDetPresu.Descripcion + "'," +
                                                                             myDetPresu.PrecioUnitario.ToString().Replace(",", ".") + "," +
                                                                             myDetPresu.Activo + "," +
                                                                             myDetPresu.Excel + "," + 
                                                                             myDetPresu.Orden + ")";


                clsDataBD.GetSql(myCadSQL);

            }
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
            myPedido.Entrada = cboEntrada.Text;
            myPedido.Fecha = dtFecha.Value;
            myPedido.IdUsuario = clsGlobales.UsuarioLogueado.IdUsuario;

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
            myPedido.Excel = 1;
            
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
                                                            " Activo = " + myPedido.Activo + "," +
                                                            " Excel = " + myPedido.Excel + "," +
                                                            " IdUsuario = " + myPedido.IdUsuario +
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
                myDetPedido.Excel = Convert.ToInt32(row.Cells["Excel"].Value.ToString());

                if (myDetPedido.Excel == 1) //SI ES EXCEL, VA PRECIO CON COEFICIENTE
                {
                    myDetPedido.PrecioUnitario = Convert.ToDouble(row.Cells["PrecioCoef"].Value);
                }
                else //SINO VA EL PRECIO UNITARIO SIN MAS
                {
                   myDetPedido.PrecioUnitario = Convert.ToDouble(row.Cells["Precio"].Value);
                }

                myDetPedido.Codigo_Articulo = row.Cells["CodigoArticulo"].Value.ToString();
                myDetPedido.Orden = Convert.ToInt32(row.Cells["nOrden"].Value);

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
                                                           " Excel," + 
                                                           " Orden)" +
                                                           "  values (" + myDetPedido.IdDetallePedido + "," +
                                                                             myDetPedido.IdPedido + "," +
                                                                             myDetPedido.IdArticulo + ",'" +
                                                                             myDetPedido.Codigo_Articulo + "'," +
                                                                             myDetPedido.Cantidad.ToString().Replace(",", ".") + ",'" +
                                                                             myDetPedido.Descripcion + "'," +
                                                                             myDetPedido.PrecioUnitario.ToString().Replace(",", ".") + "," +
                                                                             myDetPedido.Activo + "," +
                                                                             myDetPedido.Excel + "," + 
                                                                             myDetPedido.Orden + ")";


                clsDataBD.GetSql(myCadSQL);

            }
        }

        #endregion

        #region Metodo UpdateItemNumber

        private void UpdateItemNumber()
        {
            //VARIABLE PARA CADENA SQL             
            string mySQL = "";

            //ACTUALIZAR CAMPO ITEM EN TABLA TEMPORAL (ORDEN)
            foreach (DataGridViewRow row in dgvArt.Rows)
            {
                if (!(bPresupuesto))
                {
                    mySQL = "UPDATE " + Tabla + " SET Item = " + Convert.ToInt32(row.Cells["Item"].Value) + " Where IdArticulo = " + Convert.ToInt32(row.Cells["IdArticulo"].Value) + " And IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario;
                }
                else
                {
                    mySQL = "UPDATE " + Tabla + " SET Item = " + Convert.ToInt32(row.Cells["Item"].Value); //+ " Where IdArticulo = " + Convert.ToInt32(row.Cells["IdArticulo"].Value) + " And IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario;
                }
                clsDataBD.GetSql(mySQL);
            }

        }

        #endregion

        #region Metodo: CargarGrilla()

        private void CargarGrilla()
        {
            //SQL para carga de grilla
            string mySQL = "";

            if (!(bPresupuesto))
            {
               mySQL = "Select * from Vista_" + Tabla + " Where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario + " order by Item ASC";
            }
            else
            {
               mySQL = "Select * from Vista_" + Tabla + " order by Item ASC";
            }
            this.dgvArt.AutoGenerateColumns = false;
            //Cargar el datasource de la grilla
            this.dgvArt.DataSource = clsDataBD.GetSql(mySQL);

            // Variable que almacena el item que le corresponde a la fila de la grilla
            int filas = 1;
            // Recorro la grilla y asigno el número de item
            if (dgvArt.Rows.Count > 0)
            {
                // Recorro la grilla 
                foreach (DataGridViewRow row in dgvArt.Rows)
                {
                    // Asigno el número de item
                    row.Cells["Item"].Value = filas;
                    // Aumento el contador
                    filas++;
                }
            }
        }

        #endregion

        #region Metodo: CargarVecExcelAGrilla

        //METODO QUE CARGA EL VECTOR DE EXCEL A LA GRILLA
        private void CargarVecExcelATabla()
        {
            //Contador
            int Item = clsDataBD.RetornarMax(Tabla, "Item") + 1;
            string myCadena = "";
            double dPrecioIva = 0;

            //FLAG A .F.
            bExisten = false;

            string strCampo = "";

            //Verificar campo por si es el viejo excel o el nuevo
            if (clsGlobales.bNuevoExcel)
            {
                strCampo = "IdArticulo";
            }
            else
            {
                strCampo = "CodigoAnt";
            }

            int myUserIdTemp = 0;

            //VERIFICAR
            if (!(bPresupuesto))
            {
                myUserIdTemp = clsGlobales.UsuarioLogueado.IdUsuario;
                
            }
            else
            {

                myUserIdTemp = 0;
            }

            //Mostrar Datos
            for (int iterador = 0; iterador < clsGlobales.ArtExcel.GetLength(0); iterador++)
            {

                //SOLO INSERTAR SI EL ARTICULO NO EXISTE!
                if (!(NoExisteEnTabla(strCampo, Convert.ToInt32(clsGlobales.ArtExcel[iterador, 1]), myUserIdTemp)))
                {
                    //Precio Iva
                    dPrecioIva = Convert.ToDouble(clsGlobales.ArtExcel[iterador, 6]) + Convert.ToDouble(clsGlobales.ArtExcel[iterador, 7]);
                    if (clsGlobales.bNuevoExcel)
                    {

                        if (!(bPresupuesto))
                        {
                            //Si el codigo no existe, insertar
                            myCadena = "INSERT INTO " + Tabla + " (Item," +
                                                            "IdArticulo," +
                                                            "CodigoAnt," +
                                                            "CodigoArticulo," +
                                                            "Cantidad," +
                                                            "Articulo," +
                                                            "Precio," +
                                                            "PrecioCoef," +
                                                            "PrecioIva," +
                                                            "SubTotal," +
                                                            "IdCoeficiente," +
                                                            "Pub," +
                                                            "Dist," +
                                                            "Rev," +
                                                            "Excel," +
                                                            "IdUsuario)";
                        }
                        else
                        {
                            //Si el codigo no existe, insertar
                            myCadena = "INSERT INTO " + Tabla + " (Item," +
                                                            "IdArticulo," +
                                                            "CodigoAnt," +
                                                            "CodigoArticulo," +
                                                            "Cantidad," +
                                                            "Articulo," +
                                                            "Precio," +
                                                            "PrecioCoef," +
                                                            "PrecioIva," +
                                                            "SubTotal," +
                                                            "IdCoeficiente," +
                                                            "Pub," +
                                                            "Dist," +
                                                            "Rev," +
                                                            "Excel)";
                        }
                        
                        //AÑADIR ************************************************************************************************
                                                                    myCadena+=" values (" + Item.ToString() + ","
                                                                            + clsGlobales.ArtExcel[iterador, 1].ToString() + ","
                                                                            + Convert.ToInt32("0").ToString() + ","
                                                                            + clsGlobales.ArtExcel[iterador, 2].ToString() + ","
                                                                            + clsGlobales.ArtExcel[iterador, 3].ToString() + ",'"
                                                                            + clsGlobales.ArtExcel[iterador, 4].ToString() + "',"
                                                                            + Convert.ToDouble(clsGlobales.ArtExcel[iterador, 5]).ToString("#0.00000") + ","
                                                                            + Convert.ToDouble(clsGlobales.ArtExcel[iterador, 6]).ToString("#0.00000") + ","
                                                                            + dPrecioIva.ToString("#0.00") + ","
                                                                            + Convert.ToDouble(clsGlobales.ArtExcel[iterador, 7]).ToString("#0.00") + ","
                                                                            + Convert.ToInt32(clsGlobales.ArtExcel[iterador, 8]).ToString() + ","
                                                                            + Convert.ToDouble(clsGlobales.ArtExcel[iterador, 9]).ToString("#0.00") + ","
                                                                            + Convert.ToDouble(clsGlobales.ArtExcel[iterador, 10]).ToString("#0.00") + ","
                                                                            + Convert.ToDouble(clsGlobales.ArtExcel[iterador, 11]).ToString("#0.00") + ","
                                                                            + Convert.ToInt32(clsGlobales.ArtExcel[iterador, 12]).ToString();

                                                                    if (!(bPresupuesto))
                                                                    {
                                                                        myCadena += "," + clsGlobales.UsuarioLogueado.IdUsuario + ")";
                                                                    }
                    }
                    else
                    {

                        if (!(bPresupuesto))
                        {
                            //Si el codigo no existe, insertar
                            myCadena = "INSERT INTO " + Tabla + " (Item," +
                                                            "IdArticulo," +
                                                            "CodigoAnt," +
                                                            "CodigoArticulo," +
                                                            "Cantidad," +
                                                            "Articulo," +
                                                            "Precio," +
                                                            "PrecioCoef," +
                                                            "PrecioIva," +
                                                            "SubTotal," +
                                                            "IdCoeficiente," +
                                                            "Pub," +
                                                            "Dist," +
                                                            "Rev," +
                                                             "Excel," +
                                                            "IdUsuario)";
                        }
                        else
                        {
                            //Si el codigo no existe, insertar
                            myCadena = "INSERT INTO " + Tabla + " (Item," +
                                                            "IdArticulo," +
                                                            "CodigoAnt," +
                                                            "CodigoArticulo," +
                                                            "Cantidad," +
                                                            "Articulo," +
                                                            "Precio," +
                                                            "PrecioCoef," +
                                                            "PrecioIva," +
                                                            "SubTotal," +
                                                            "IdCoeficiente," +
                                                            "Pub," +
                                                            "Dist," +
                                                            "Rev," +
                                                            "Excel)";
                        }
                             
                                                          
                        //AÑADIR ************************************************************************************************
                        myCadena += " values (" + Item.ToString() + ","
                                        + clsGlobales.ArtExcel[iterador, 1].ToString() + ","
                                        + Convert.ToInt32("0").ToString() + ","
                                        + clsGlobales.ArtExcel[iterador, 2].ToString() + ","
                                        + clsGlobales.ArtExcel[iterador, 3].ToString() + ",'"
                                        + clsGlobales.ArtExcel[iterador, 4].ToString() + "',"
                                        + Convert.ToDouble(clsGlobales.ArtExcel[iterador, 5]).ToString("#0.00000") + ","
                                        + Convert.ToDouble(clsGlobales.ArtExcel[iterador, 6]).ToString("#0.00000") + ","
                                        + dPrecioIva.ToString("#0.00") + ","
                                        + Convert.ToDouble(clsGlobales.ArtExcel[iterador, 7]).ToString("#0.00") + ","
                                        + Convert.ToInt32(clsGlobales.ArtExcel[iterador, 8]).ToString() + ","
                                        + Convert.ToDouble(clsGlobales.ArtExcel[iterador, 9]).ToString("#0.00") + ","
                                        + Convert.ToDouble(clsGlobales.ArtExcel[iterador, 10]).ToString("#0.00") + ","
                                        + Convert.ToDouble(clsGlobales.ArtExcel[iterador, 11]).ToString("#0.00") + ","
                                        + Convert.ToInt32(clsGlobales.ArtExcel[iterador, 12]).ToString();
                                                                
                                        if (!(bPresupuesto))
                                        {
                                            myCadena += ","  + clsGlobales.UsuarioLogueado.IdUsuario + ")";
                                        }
                    }

                    clsDataBD.GetSql(myCadena);

                    //Contador
                    Item++;
                }
            }

            //UPDATE. Asigna el nuevo IdArticulo y Codigo de Producto a los articulos
            //importados desde Excel.
            if (!(clsGlobales.bNuevoExcel))
            {
                myCadena = "UPDATE " + Tabla + " SET IdArticulo = M.IdArticulo, " +
                        " CodigoArticulo = M.CodigoNuevo, Articulo = A.Articulo From " +
                        Tabla + " T INNER JOIN MigrarCodigos M " +
                        " ON T.CodigoAnt =  M.CodigoAnt " +
                        " INNER JOIN Articulos A " +
                        " ON A.IdArticulo = M.IdArticulo ";


            }
            else
            {
                myCadena = "UPDATE " + Tabla + " SET CodigoArticulo = A.CodigoArticulo " +
                        " FROM " + Tabla + " T INNER JOIN Articulos A " +
                        " ON T.IdArticulo =  A.IdArticulo ";
            }

            //Ejecutar SQL
            clsDataBD.GetSql(myCadena);

        }

        #endregion

        #region Metodo NoExisteEnTabla

        private bool NoExisteEnTabla(string pCampo, int pCodigoAnt, int p_Id = 0)
        {
            string mySQL = "";

            if (p_Id == 0)
            {
                mySQL = "Select count(*) as TOTAL from " + Tabla + " Where " + pCampo + " = " + pCodigoAnt;
            }
            else
            {
                mySQL = "Select count(*) as TOTAL from " + Tabla + " Where IdUsuario = " + p_Id + " and " + pCampo + " = " + pCodigoAnt;
            }
            int total = 0;

            DataTable myDataTable = clsDataBD.GetSql(mySQL);

            //RE GRABAR EL DETALLE DE PEDIDO
            foreach (DataRow row in myDataTable.Rows)
            {
                total = Convert.ToInt32(row["TOTAL"]);
            }

            //EXISTE?
            if (total > 0)
            {
                //FLAG A .T.
                bExisten = true;

                //RETORNAR
                return true;
            }
            else
            {
                return false;
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
            this.IdCliente = this.myCliente.Codigo;
            if (this.myCliente.Cuit.Length >=11 && this.myCliente.Cuit.Length <=13)
            {
                this.txtCuit.Text = clsGlobales.cFormato.CUITGuiones(this.myCliente.Cuit, 2);
            }
           else
            {
                this.txtCuit.Text = this.myCliente.Cuit;
            }   
            this.txtRazonSocial.Text = this.myCliente.RazonSocial;
            this.txtDir.Text = this.myCliente.Direccion + ", " + this.myCliente.Localidad + ", B°: " +  this.myCliente.Barrio + ", CP (" + this.myCliente.CP + ") - " + this.myCliente.Provincia;
;
            this.cboTipo.SelectedValue = this.myCliente.IdTipoCliente;
            this.cboTipoCliente.SelectedValue = this.myCliente.IdCondicionIva;
            this.cboCondicionCompra.SelectedValue = this.myCliente.IdCondicionCompra;
            
            //Idem Forma de Pago
            if (!(bPresupuesto))
            {
                if (this.myCliente.IdTransporte != myPedido.IdTransporte)
                {
                    this.cboTransporte.SelectedValue = this.myCliente.IdTransporte;
                }
                else
                {
                    this.cboTransporte.SelectedValue = this.myPedido.IdTransporte;
                }
            }
            else
            {
                if (this.myCliente.IdTransporte != myPresu.IdTransporte)
                {
                    this.cboTransporte.SelectedValue = this.myCliente.IdTransporte;
                }
                else
                {
                    this.cboTransporte.SelectedValue = this.myPresu.IdTransporte;
                }
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
        }

        #endregion

        #region Metodo: ValidarPantalla

        private bool ValidaPantalla()
        {
            bool cValida = true;

            //Hay alguna cantidad en 0??...
            //Recorrer la grilla para verificar
            //ACTUALIZAR CAMPO ITEM EN TABLA TEMPORAL (ORDEN)
            foreach (DataGridViewRow row in dgvArt.Rows)
            {
                if (Convert.ToInt32(row.Cells["Cantidad"].Value) == 0)
                {
                    //Error
                    MessageBox.Show("Debe completar la 'Cantidad' para todos los Artículos cargados!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvArt.Focus();
                    cValida = false;
                    return cValida;
                }
            }

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
            if (clsGlobales.cValida.IsNumeric(txtSubTotal.Text))
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
                if (string.IsNullOrEmpty(cboEntrada.Text))
                {
                    MessageBox.Show("Por favor, ingrese el dato para el campo 'Entrada'!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboEntrada.Focus();
                    cValida = false;
                    return cValida;
                }
            }

            //VALIDAR PORCENTAJE DE DESCUENTO
            if (clsGlobales.cValida.IsNumeric(txtDto.Text))
            {
                if (Convert.ToDouble(txtDto.Text) > 100.00)
                {
                    MessageBox.Show("El Porcentaje de Descuento no pude ser mayor a 100!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDto.Focus();
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

            //retornar
            return cValida;
        }

        #endregion

        #region Metodo: HabilitarOtros

        private void HabilitarOtros()
        {
            //Si CUIT no esta vacio
            if (!(string.IsNullOrEmpty(txtCuit.Text)))
            {
                this.btnAgregaArtExcl.TabStop = true;
                this.btnAgregaArtExcl.Enabled = true;
                this.btnAgregarArt.TabStop = true;
                this.btnAgregarArt.Enabled = true;

                this.cboCondicionCompra.TabStop = true;
                this.cboCondicionCompra.Enabled = true;

                this.cboTransporte.TabStop = true;
                this.cboTransporte.Enabled = true;

            }
            else 
            {
                this.btnAgregaArtExcl.TabStop = false;
                this.btnAgregaArtExcl.Enabled = false;
                this.btnAgregarArt.TabStop = false;
                this.btnAgregarArt.Enabled = false;

                this.cboCondicionCompra.TabStop = false;
                this.cboCondicionCompra.Enabled = false;

                this.cboTransporte.TabStop = false;
                this.cboTransporte.Enabled = false;               
            }

            //Grilla
            if (!(dgvArt.Rows.Count == 0))
            {
                this.btnQuitarArt.TabStop = true;
                this.btnQuitarArt.Enabled = true;
                this.btnAceptar.TabStop = true;
                this.btnAceptar.Enabled = true;
            }
            else
            {
                this.btnQuitarArt.TabStop = false;
                this.btnQuitarArt.Enabled = false;
                this.btnAceptar.TabStop = false;
                this.btnAceptar.Enabled = false;
            }
        }

        #endregion

        #region Metodo: HabilitarArticulos

        private void HabilitarControles()
        {
            //Solo si es un Alta...
            if (myEstado == "A")
            {
                this.btnCli.Enabled = true;
                this.btnAgregaArtExcl.Enabled = false;
                this.btnAgregarArt.Enabled = false;
                if (!(txtCuit.Text==""))
                {
                    //habilitar articulos
                    this.btnAgregaArtExcl.Enabled = true;
                    this.btnAgregarArt.Enabled = true;
                    this.btnCli.Enabled = true;
                }
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
                if (dgvArt.Rows.Count == 0 && txtCuit.Text == "")
                {
                    this.btnAgregaArtExcl.Enabled = false;
                    this.btnAgregarArt.Enabled = false;
                    this.btnQuitarArt.Enabled = false;
                    this.btnAceptar.Enabled = false;
                }

                if (txtCuit.Text == "")
                {
                    this.btnAceptar.Enabled = false;
                }
            }
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

            if (dgvArt.Rows.Count > 0 && this.myEstado == "M")
            {
                this.btnAgregaArtExcl.Enabled = true;
                this.btnAgregarArt.Enabled = true;
                this.btnQuitarArt.Enabled = true;
            }

            //Calcular Subtotal
            CalcularSubtotal();
            //Calcular el total
            CalcularTotal();

            if (Convert.ToDouble(txtTotal.Text) > 0)
            {
                this.txtDto.TabStop = true;
                this.txtDto.Enabled = true;
                this.txtCostoFlete.TabStop = true;
                this.txtCostoFlete.Enabled = true;
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
                if (!(string.IsNullOrEmpty(txtDto.Text)))
                {
                    if (Convert.ToDouble(this.txtDto.Text) > 0)
                    {
                        dto = 1 - (Convert.ToDouble(this.txtDto.Text) / 100);
                        acumula21Dto = acumula21 * dto;
                        acumula10Dto = acumula10 * dto;
                        acumulaExentoDto = acumulaExento * dto;
                    }
                }

                //FACTURA B

                //ACUMULAR NETO FB

                if (!(Convert.ToDouble(row.Cells["Alicuota"].Value) == 0))
                {
                    acumulaNetoFB = acumulaNetoFB + (Convert.ToDouble(row.Cells["Cantidad"].Value) * Convert.ToDouble(row.Cells["PreIvaAux"].Value));
                }

                //HAY DTO
                if (!(string.IsNullOrEmpty(txtDto.Text)))
                {
                    if (Convert.ToDouble(this.txtDto.Text) > 0)
                    {
                        dto = 1 - (Convert.ToDouble(this.txtDto.Text) / 100);
                        acumulaNetoFBDto = acumulaNetoFB * dto;
                        acumulaExentoFBDto = acumulaExentoFB * dto;
                    }
                }
    
                //AUMENTAR CONTADOR FILAS
                fila++;

            }

            //VERIFICAR TIPO COMPROBANTE
            if (Convert.ToInt32(cboTipoCliente.SelectedValue) == 1)
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
                    if (Convert.ToDouble(txtCostoFlete.Text) > 0)
                    {
                        //EXENTO
                        this.txtExento.Text = (acumulaExentoDto + Convert.ToDouble(txtCostoFlete.Text)).ToString("#0.00");
                    }

                }
                else
                {
                    this.txtSubTotal.Text = NetoFA.ToString("#0.00");
                    //EXENTO
                    this.txtExento.Text = acumulaExento.ToString("#0.00");

                    //HAY FLETE?
                    if (Convert.ToDouble(txtCostoFlete.Text) > 0)
                    {
                        //EXENTO
                        this.txtExento.Text = (acumulaExento + Convert.ToDouble(txtCostoFlete.Text)).ToString("#0.00");
                    }

                }

                //IVA
                txtIVA10.Text = Iva10.ToString("#0.00");
                txtIVA.Text = Iva21.ToString("#0.00");

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

                    txtSubTotal.Text = acumulaNetoFBDto.ToString("#0.00");

                    //HAY FLETE, EXENTO
                    if (Convert.ToDouble(txtCostoFlete.Text) > 0)
                    {
                        acumulaExentoFBDto += Convert.ToDouble(txtCostoFlete.Text);
                        txtExento.Text = acumulaExentoFBDto.ToString("#0.00");

                    }
                    else
                    {
                        txtExento.Text = acumulaExentoFBDto.ToString("#0.00");
                    }

                }
                else
                {
                    //NETO              
                    NetoFB = acumulaNetoFB;
                    this.txtNeto.Text = NetoFB.ToString("#0.00");



                    txtSubTotal.Text = NetoFB.ToString("#0.00");

                    //HAY FLETE, EXENTO
                    if (Convert.ToDouble(txtCostoFlete.Text) > 0)
                    {
                        acumulaExentoFB += Convert.ToDouble(txtCostoFlete.Text);
                        txtExento.Text = acumulaExentoFB.ToString("#0.00");
                    }
                    else
                    {
                        txtExento.Text = acumulaExentoFB.ToString("#0.00");
                    }
                }

                //TOTAL
                txtTotal.Text = (Convert.ToDouble(txtSubTotal.Text) + Convert.ToDouble(txtExento.Text)).ToString("#0.00");

                //IVA
                txtIVA10.Text = Iva10.ToString("#0.00");
                txtIVA.Text = Iva21.ToString("#0.00");
            }                       
        }

        #endregion

        #region Método que calcula el SubTotal de los Artículos por Fila

        private void CalcularSubtotal()
        {
            // Variable que guarda el resultado de la multiplicación
            double SubTotal = 0;
            double Coef = 0;
            double Cant = 0;
            double Pre = 0;
            string sCadSQL = "";
            double dIva = 0;
            double calculoiva = 0;
            double CalcIva = 0;
            double SubTotIva = 0;
            double Sub1 = 0;
            double Sub3 = 0;
            double PreIva = 0;
            string auxSubTot = "";

                // Recorro la grilla y hago el cálculo
                foreach (DataGridViewRow row in dgvArt.Rows)
                {
                   if (Convert.ToInt32(row.Cells["Excel"].Value)==0)
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

                        auxSubTot = SubTotal.ToString("#0.00");
                        SubTotal = Convert.ToDouble(auxSubTot);

                        //PASAR A VARIABLES PARA PODER FORMATEAR
                        Pre = SubTotal;

                        //MULTIPLICAR POR LA CANTIDAD
                        if (!(row.Cells["Cantidad"].Value.ToString() == ""))
                        {
                            SubTotal = SubTotal * Convert.ToDouble(row.Cells["Cantidad"].Value);
                            row.Cells["SubTotal"].Value = SubTotal.ToString("#0.00");

                            Cant = Convert.ToDouble(row.Cells["Cantidad"].Value);
                            row.Cells["Cantidad"].Value = Cant.ToString("#0");
                        }

                        //FORMATEO DE COLUMNAS

                        // Asigno el valor a la celda
                        row.Cells["PrecioCoef"].Value = Pre.ToString("#0.00");

                        //Buscar el Porcentaje de IVA
                        sCadSQL = "Select * from Articulos Where IdArticulo = " + Convert.ToInt32(row.Cells["IdArticulo"].Value);
                        System.Data.DataTable myData = clsDataBD.GetSql(sCadSQL);

                        //Guardar Coeficientes en variables
                        foreach (DataRow rows in myData.Rows)
                        {
                            dIva = Convert.ToDouble(rows["PorcentajeIva"]);
                        }

                        // Asigno el valor a la celda
                        if (dIva > 0)
                        {
                            calculoiva = 1 + (dIva / 100);
                            PreIva = (Pre * calculoiva);
                            row.Cells["PrecioIva"].Value = PreIva.ToString("#0.00");
                            row.Cells["PreIvaAux"].Value = PreIva;
                            //Guardo Alicuota
                            row.Cells["Alicuota"].Value = (dIva).ToString("#0.00");
                        }
                        else
                        {
                            PreIva = Pre;
                            row.Cells["PrecioIva"].Value = PreIva.ToString("#0.00");
                            row.Cells["PreIvaAux"].Value = PreIva;
                            //Guardo Alicuota
                            row.Cells["Alicuota"].Value = (dIva).ToString("#0.00");
                        }

                        if (!(Convert.ToDouble(txtDto.Text) == 0))
                        {
                            //CALCULAR EL SUBTOTAL CON EL DESCUENTO ( FACTURA A )
                            Sub1 = Convert.ToDouble(row.Cells["Cantidad"].Value) * Pre;
                            Sub1 = Sub1 - ((Sub1 * Convert.ToDouble(txtDto.Text)) / 100);

                            //PARA EL CASO DE PRECIO CON IVA ( FACTURA B )
                            Sub3 = Convert.ToDouble(row.Cells["Cantidad"].Value) * PreIva;
                            Sub3 = Sub3 - ((Sub3 * Convert.ToDouble(txtDto.Text)) / 100);

                        }
                        else
                        {
                            //PARA FACTURA A
                            Sub1 = Convert.ToDouble(row.Cells["Cantidad"].Value) * Pre;

                            //PARA FACTURA B
                            Sub3 = Convert.ToDouble(row.Cells["Cantidad"].Value) * PreIva;
                        }


                        //SubTotalIva
                        if (Convert.ToInt32(cboTipoCliente.SelectedValue) == 1)
                        {
 
                            //CALCULAR EL IVA ( PARA FACTURA A )                
                            CalcIva = Sub1 * Convert.ToDouble(row.Cells["Alicuota"].Value) / 100;
                            row.Cells["IVA"].Value = CalcIva.ToString("#0.00");

                            //APLICAR IVA ( PARA FACTURA A)
                            SubTotIva = Sub1 + (Sub1 * Convert.ToDouble(row.Cells["Alicuota"].Value)) / 100;

                            //Calcular IVA
                        /*    CalcIva = Sub1 + (sUB * Convert.ToDouble(row.Cells["Alicuota"].Value) / 100;
                            row.Cells["IVA"].Value = CalcIva.ToString("#0.00");

                            SubTotIva = Sub1 + Convert.ToDouble(row.Cells["IVA"].Value);*/

                        }
                        else
                        {
                            //Calcular IVA
                            CalcIva = Sub3 * Convert.ToDouble(row.Cells["Alicuota"].Value) / 100;
                            row.Cells["IVA"].Value = CalcIva.ToString("#0.00");

                            SubTotIva = Sub3;
                        }
                       
                       //SubTotal IVA
                        row.Cells["SubTotalIva"].Value = SubTotIva.ToString("#0.00");

                        //Buscar el Porcentaje de IVA
                        sCadSQL = "Select a.IdArticulo,a.IdUnidadMedida, um.AbreviaturaUnidad from Articulos a, UnidadesMedida um Where IdArticulo = " + Convert.ToInt32(row.Cells["IdArticulo"].Value) +
                                   " And um.IdUnidadMedida = a.IdUnidadMedida";
                        myData = clsDataBD.GetSql(sCadSQL);

                        //Guardar Coeficientes en variables
                        foreach (DataRow rows in myData.Rows)
                        {
                            row.Cells["AbrevUnidad"].Value = rows["AbreviaturaUnidad"].ToString();
                        }

                   }
                   else //ES UNA LINEA DE EXCEL ( YA INCLUYE COEFICIENTE )
                   {
                        //MULTIPLICAR POR LA CANTIDAD
                        SubTotal =  Convert.ToDouble(row.Cells["PrecioCoef"].Value) * Convert.ToDouble(row.Cells["Cantidad"].Value);

                        auxSubTot = SubTotal.ToString("#0.00");
                        SubTotal = Convert.ToDouble(auxSubTot);

                        //PASAR A VARIABLES PARA PODER FORMATEAR
                        Cant = Convert.ToDouble(row.Cells["Cantidad"].Value);
                        Pre = Convert.ToDouble(row.Cells["PrecioCoef"].Value);


                        //FORMATEO DE COLUMNAS
                        row.Cells["Cantidad"].Value = Cant.ToString("#0");

                        //ASIGNAR LOS VALORES A LA CELDA
                        row.Cells["SubTotal"].Value = SubTotal.ToString("#0.00");
                        row.Cells["PrecioCoef"].Value = Pre.ToString("#0.00");  

                       //
                        //Buscar el Porcentaje de IVA
                        sCadSQL = "Select * from Articulos Where IdArticulo = " + Convert.ToInt32(row.Cells["IdArticulo"].Value);
                        System.Data.DataTable myData = clsDataBD.GetSql(sCadSQL);

                        //Guardar Coeficientes en variables
                        foreach (DataRow rows in myData.Rows)
                        {
                            dIva = Convert.ToDouble(rows["PorcentajeIva"]);
                        }

                        // Asigno el valor a la celda
                        if (dIva > 0)
                        {
                            calculoiva = 1 + (dIva / 100);
                            row.Cells["PrecioIva"].Value = (Pre * calculoiva).ToString("#0.00");
                            row.Cells["PreIvaAux"].Value = (Pre * calculoiva);
                            //Guardo Alicuota
                            row.Cells["Alicuota"].Value = (dIva).ToString("#0.00");
                            //Subtotal Iva 
                        }
                        else
                        {
                            row.Cells["PrecioIva"].Value = (Pre).ToString("#0.00");
                            row.Cells["PreIvaAux"].Value = (Pre);
                            //Guardo Alicuota
                            row.Cells["Alicuota"].Value = (dIva).ToString("#0.00");
                            //Subtotal Iva 
                        }

                        if (!(Convert.ToDouble(txtDto.Text) == 0))
                        {
                            //CALCULAR EL SUBTOTAL CON EL DESCUENTO ( FACTURA A )
                            Sub1 = Convert.ToDouble(row.Cells["Cantidad"].Value) * Pre;
                            Sub1 = Sub1 - ((Sub1 * Convert.ToDouble(txtDto.Text)) / 100);

                            //PARA EL CASO DE PRECIO CON IVA ( FACTURA B )
                            Sub3 = Convert.ToDouble(row.Cells["Cantidad"].Value) * PreIva;
                            Sub3 = Sub3 - ((Sub3 * Convert.ToDouble(txtDto.Text)) / 100);

                        }
                        else
                        {
                            //PARA FACTURA A
                            Sub1 = Convert.ToDouble(row.Cells["Cantidad"].Value) * Pre;

                            //PARA FACTURA B
                            Sub3 = Convert.ToDouble(row.Cells["Cantidad"].Value) * PreIva;
                        }                   

                       //SubTotalIva
                        if (Convert.ToInt32(cboTipoCliente.SelectedValue) == 1)
                        {


                            //Calcular IVA
                            CalcIva = Sub1 * Convert.ToDouble(row.Cells["Alicuota"].Value) / 100;
                            row.Cells["IVA"].Value = CalcIva.ToString("#0.00");

                            SubTotIva = Sub1 + Convert.ToDouble(row.Cells["IVA"].Value); ;
                        }
                        else
                        {


                            //Calcular IVA
                            CalcIva = Sub3 * Convert.ToDouble(row.Cells["Alicuota"].Value) / 100;
                            row.Cells["IVA"].Value = CalcIva.ToString("#0.00");

                            SubTotIva = Sub3 + Convert.ToDouble(row.Cells["IVA"].Value); ;

                        }

                        row.Cells["SubTotalIva"].Value = SubTotIva.ToString("#0.00");

                        //Buscar el Porcentaje de IVA
                        sCadSQL = "Select a.IdArticulo,a.IdUnidadMedida, um.AbreviaturaUnidad from Articulos a, UnidadesMedida um Where IdArticulo = " + Convert.ToInt32(row.Cells["IdArticulo"].Value) +
                                   " And um.IdUnidadMedida = a.IdUnidadMedida";
                        myData = clsDataBD.GetSql(sCadSQL);

                        //Guardar Coeficientes en variables
                        foreach (DataRow rows in myData.Rows)
                        {
                            row.Cells["AbrevUnidad"].Value = rows["AbreviaturaUnidad"].ToString();
                        }
                   }

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

        #region EVENTOS GRILLA

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

            //Verificar habilitacion de campos y calculos
            this.VerificarCampos();

            //Si cambio cantidad, modificar en tabla temporal
            ActualizaRegistro();

        }

        #endregion

        #region Metodo ActualizaRegistro

        //METODO QUE ACTUALIZA EL REGISTRO ACTUAL SI SE CAMBIO CANTIDAD
        private void ActualizaRegistro()
        {

            //Declaracion de Variables (Costo Unitario y Cantidad) 
            DataGridViewRow row = dgvArt.CurrentRow;

            //Update
            string mySQL = "";

            if (!(bPresupuesto))
            {
                mySQL = "UPDATE " + Tabla + " SET Cantidad =  " + Convert.ToInt32(dgvArt.CurrentRow.Cells["Cantidad"].Value) + "," +
                                                    " SubTotal = " + Convert.ToDouble(dgvArt.CurrentRow.Cells["SubTotal"].Value) + "," +
                                                    " PrecioCoef = " + Convert.ToDouble(dgvArt.CurrentRow.Cells["PrecioCoef"].Value) +
                                                    " WHERE IdArticulo = " + Convert.ToDouble(dgvArt.CurrentRow.Cells["IdArticulo"].Value) + " And IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario;
            }
            else            
            {
                mySQL = "UPDATE " + Tabla + " SET Cantidad =  " + Convert.ToInt32(dgvArt.CurrentRow.Cells["Cantidad"].Value) + "," +
                                                    " SubTotal = " + Convert.ToDouble(dgvArt.CurrentRow.Cells["SubTotal"].Value) + "," +
                                                    " PrecioCoef = " + Convert.ToDouble(dgvArt.CurrentRow.Cells["PrecioCoef"].Value) +
                                                    " WHERE IdArticulo = " + Convert.ToDouble(dgvArt.CurrentRow.Cells["IdArticulo"].Value);
            }
            //Ejecutar
            clsDataBD.GetSql(mySQL);

        }

        #endregion

        #region Eventos TextChanged y KeyPress

        private void txtDto_TextChanged(object sender, EventArgs e)
        {
            //Calcular Subtotal
            CalcularSubtotal();
            //Calcular el total
            CalcularTotal();
        }

        private void txtCostoFlete_TextChanged(object sender, EventArgs e)
        {
            //Calcular Subtotal
            CalcularSubtotal();
            //Calcular el total
            CalcularTotal();
        }

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

        private void txtCostoFlete_Enter(object sender, EventArgs e)
        {
            txtCostoFlete.SelectionStart = 0;
            txtCostoFlete.SelectionLength = txtCostoFlete.Text.Length;
        }

        private void txtDto_Enter(object sender, EventArgs e)
        {
            txtDto.SelectionStart = 0;
            txtDto.SelectionLength = txtDto.Text.Length;
        }

        #endregion

        #region Eventos Leave

        private void txtEntrada_Leave(object sender, EventArgs e)
        {
            this.txtEntrada.Text = txtEntrada.Text.ToUpper();
        }

        private void txtComentario_Leave(object sender, EventArgs e)
        {
            this.txtComentario.Text = txtComentario.Text.ToUpper();
        }

        #endregion

        #region OTROS 05-06-2017

        private void btnEditCli_Click(object sender, EventArgs e)
        {
            //hay cliente seleccionado
            if (string.IsNullOrEmpty(txtCuit.Text))
            {
                MessageBox.Show("No hay ningún Cliente seleccionado!","Advertencia!",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Lamar al formulario de clientes con el cliente seleccionado
            frmClientesABM frmModiCli = new frmClientesABM(myCliente.Codigo);
            frmModiCli.ShowDialog();
            // Cargo los datos del proveedor
            CargarClientes(this.myCliente.Codigo);
            // Los paso al formulario
            PasarDatosAlFormulario();
            //Habilitar
            this.HabilitarOtros();
            //Calculos
            this.CalcularSubtotal();
            this.CalcularTotal();
            //Inhabilitar Boton
            this.btnCli.Enabled = false;
            //Ocultar o mostrar otros datos
            ShowIvaStuff();
        }

        private void txtDto_Click(object sender, EventArgs e)
        {
            txtDto.SelectionStart = 0;
            txtDto.SelectionLength = txtDto.Text.Length;
        }

        private void txtCostoFlete_Click(object sender, EventArgs e)
        {
            txtCostoFlete.SelectionStart = 0;
            txtCostoFlete.SelectionLength = txtCostoFlete.Text.Length;
        }

        private void txtEntrada_Click(object sender, EventArgs e)
        {
            txtEntrada.SelectionStart = 0;
            txtEntrada.SelectionLength = txtEntrada.Text.Length;
        }

        private void cboTipoCliente_SelectedValueChanged(object sender, EventArgs e)
        {
            //Tipo Cliente
            if (Convert.ToInt32(cboTipoCliente.SelectedValue) == 1)
            {
                setOtrosItems(1); //FACT. A
            }
            else
            {
                setOtrosItems(2); //FACT. B
            }
        }

        #endregion

        private void btnImprimir_Click(object sender, EventArgs e)
        {

            string sPedPresu = "";

            //Que imprimo?
            if (!(bPresupuesto))
            { sPedPresu = "Pedido"; }
            else
            { sPedPresu = "Presupuesto"; }
            
            //PREGUNTAR SI ESTA CONFIGURADO EN PARAMETROS
            if (clsGlobales.cParametro.Imprimir)
            {
                DialogResult dlResult = MessageBox.Show("¿Desea imprimir el " + sPedPresu + " N° " + lblPunto.Text + "-" + nroComp.Text + " ?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Si confirma... cambiar estado
                if (dlResult == DialogResult.No)
                {
                    return;
                }
            }

            //Imprimir Pedido o Presupuesto
            this.ImprimirPedPresu();
        }


        #region Metodo: Imprimir Pedido o Presupuesto

        private void ImprimirPedPresu(bool bDialog =false)
        {

            int dgvFilas = 0;

            //Data Set
            dsReportes oDsFactura = new dsReportes();

            //FACTURA A
            if (Convert.ToInt32(cboTipoCliente.SelectedValue) == 1)
            {


                //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
                dgvFilas = dgvArt.Rows.Count;

                for (int i = 0; i < dgvFilas; i++)
                {
                    oDsFactura.Tables["dtFacturaVenta"].Rows.Add
                    (new object[] { 
                    dgvArt[2,i].Value.ToString(),
                    dgvArt[4,i].Value.ToString(),
                    dgvArt[3,i].Value.ToString(),
                    dgvArt[19,i].Value.ToString(),
                    dgvArt[6,i].Value.ToString(),
                    Convert.ToDouble(txtDto.Text).ToString("#0.00"),
                    dgvArt[7,i].Value.ToString(),
                    dgvArt[8,i].Value.ToString(),
                    dgvArt[11,i].Value.ToString()});
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
                    dgvArt[3,i].Value.ToString(),
                    dgvArt[19,i].Value.ToString(),
                    dgvArt[9,i].Value.ToString(),
                    " ",
                    " ",
                    " ",
                    (Convert.ToDouble(dgvArt[9,i].Value.ToString()) * Convert.ToDouble(dgvArt[3,i].Value.ToString())).ToString("#0.00")});

                }

            }

            //PEDIDO O PRESUPUESTO COMO FACTURA A
            if (Convert.ToInt32(cboTipoCliente.SelectedValue) == 1)
            {
                //Objeto Reporte
                rptPedPresuA oRepPedPresu = new rptPedPresuA();

                //Cargar Reporte                                    
                oRepPedPresu.Load(Application.StartupPath + "\\rptPedPresu(A).rpt");

                //Tipo Comprobante
                oRepPedPresu.DataDefinition.FormulaFields["TipoComprobante"].Text = "'" + "X" + "'";
                oRepPedPresu.DataDefinition.FormulaFields["CodigoComp"].Text = "'" + " " + "'";

                //Pedido o Presupuesto?
                if (!(bPresupuesto))
                { oRepPedPresu.DataDefinition.FormulaFields["DescComp"].Text = "'" + " Pedido N°: " + "'"; }
                else
                { oRepPedPresu.DataDefinition.FormulaFields["DescComp"].Text = "'" + " Presupuesto N°: " + "'"; }

                //CARGAR FORMULAS Y MOSTRAR REPORTE
                ShowReportPredPresuA(oRepPedPresu, oDsFactura, bDialog);

            } //PEDIDO O PRESUPUESTO COMO FACTURA B
            else
            {

                //Objeto Reporte
                rptPedPresuB oRepPedPresu = new rptPedPresuB();

                //Cargar Reporte                                    
                oRepPedPresu.Load(Application.StartupPath + "\\rptPedPresu(B).rpt");

                oRepPedPresu.DataDefinition.FormulaFields["TipoComprobante"].Text = "'" + "X" + "'";
                oRepPedPresu.DataDefinition.FormulaFields["CodigoComp"].Text = "'" + " " + "'";

                //Pedido o Presupuesto?
                if (!(bPresupuesto))
                { oRepPedPresu.DataDefinition.FormulaFields["DescComp"].Text = "'" + " Pedido N°: " + "'"; }
                else
                { oRepPedPresu.DataDefinition.FormulaFields["DescComp"].Text = "'" + " Presupuesto N°: " + "'"; }


                //CARGAR FORMULAS Y MOSTRAR REPORTE
                ShowReportPredPresuB(oRepPedPresu, oDsFactura, bDialog);
            }
        }

        #endregion

        #region Metodo ShowReportA

        //METODO ShowReportPedPresuA, muestra la factura A como presupuesto
        private void ShowReportPredPresuA(rptPedPresuA oRepPedPresuA, dsReportes oDsFactura, bool bPrintDialog = false)
        {
            //Establecer el DataSet como DataSource
            oRepPedPresuA.SetDataSource(oDsFactura);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepPedPresuA;

            oRepPedPresuA.DataDefinition.FormulaFields["NroComp"].Text = "'" + this.lblPunto.Text + "-" + nroComp.Text + "'";

            //Otras
            oRepPedPresuA.DataDefinition.FormulaFields["Fecha"].Text = "'" + dtFecha.Value.ToString("dd/MM/yyyy") + "'";
            oRepPedPresuA.DataDefinition.FormulaFields["RazonSocial"].Text = "'" + txtRazonSocial.Text.ToUpper() + "'";
            oRepPedPresuA.DataDefinition.FormulaFields["Domic"].Text = "'" + (txtDir.Text).ToUpper() + "'";
            oRepPedPresuA.DataDefinition.FormulaFields["IVA"].Text = "'" + cboTipoCliente.Text.ToUpper() + "'";
            oRepPedPresuA.DataDefinition.FormulaFields["CondicionVenta"].Text = "'" + cboCondicionCompra.Text.ToUpper() + "'";
            oRepPedPresuA.DataDefinition.FormulaFields["CUIT"].Text = "'" + txtCuit.Text + "'";

            double dtoImpo = (Convert.ToDouble(txtNeto.Text) * Convert.ToDouble(txtDto.Text)) / 100;
            oRepPedPresuA.DataDefinition.FormulaFields["Dto"].Text = "'" + txtDto.Text + "'";
            oRepPedPresuA.DataDefinition.FormulaFields["DtoImpo"].Text = "'" + dtoImpo.ToString("#0.00") + "'";

            //SUBTOTAL            
            oRepPedPresuA.DataDefinition.FormulaFields["Neto"].Text = "'" + txtNeto.Text + "'";
            oRepPedPresuA.DataDefinition.FormulaFields["Subtotal"].Text = "'" + txtSubTotal.Text + "'";

            //TRANSPORTE
            oRepPedPresuA.DataDefinition.FormulaFields["Transporte"].Text = "'" + cboTransporte.Text + "'";


            //MUESTRO LOS 2 IVA ( 21 Y 10.5)
            if (Convert.ToInt32(cboTipoCliente.SelectedValue) == 1)
            {
                oRepPedPresuA.DataDefinition.FormulaFields["IVA10"].Text = "'" + txtIVA10.Text + "'";
                oRepPedPresuA.DataDefinition.FormulaFields["TotIVA"].Text = "'" + txtIVA.Text + "'";
            }


            //FLETE Y EXENTO
            oRepPedPresuA.DataDefinition.FormulaFields["Flete"].Text = "'" + txtCostoFlete.Text + "'";
            oRepPedPresuA.DataDefinition.FormulaFields["Exento"].Text = "'" + txtExento.Text + "'";

            //TOTAL
            oRepPedPresuA.DataDefinition.FormulaFields["Total"].Text = "'" + txtTotal.Text + "'";

            //Comprobante y pie
            oRepPedPresuA.DataDefinition.FormulaFields["linea-01"].Text = "' Razón Social: " + clsGlobales.cParametro.RazonSocial + "'";
            oRepPedPresuA.DataDefinition.FormulaFields["linea-02"].Text = "' Domicilio:'";
            oRepPedPresuA.DataDefinition.FormulaFields["linea-03"].Text = "'" + clsGlobales.cParametro.Direccion + "-" + clsGlobales.cParametro.Localidad + ", Córdoba" + "'";
            oRepPedPresuA.DataDefinition.FormulaFields["linea-04"].Text = "' Condición frente al Iva : " + clsGlobales.cParametro.CondicionIva + "'";
            oRepPedPresuA.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepPedPresuA.DataDefinition.FormulaFields["linea-06"].Text = "' Ingresos Brutos: " + "904/30-71658372-0" + "'";
            oRepPedPresuA.DataDefinition.FormulaFields["linea-07"].Text = "' Fecha de Inicio Actividades : " + "01/10/2019" + "'";
            oRepPedPresuA.DataDefinition.FormulaFields["Entrada"].Text = "'" + cboEntrada.Text + "'";

            if (!(bPrintDialog))
            {
                //Mostrar el reporte  
                frmShowReports myReportForm = new frmShowReports();
                myReportForm.Text = this.Text;
                myReportForm.ShowDialog();
            }
            else
            {
                PrintDialog dialog1 = new PrintDialog();
                
                int CopiesToPrinter = clsGlobales.cParametro.Pedidos;

                dialog1.AllowSomePages = true;
                dialog1.AllowPrintToFile = false;
                dialog1.PrinterSettings.Copies = (short)CopiesToPrinter;

                if (dialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    int copies = dialog1.PrinterSettings.Copies;
                    int fromPage = dialog1.PrinterSettings.FromPage;
                    int toPage = dialog1.PrinterSettings.ToPage;
                    bool collate = dialog1.PrinterSettings.Collate;

                    clsGlobales.myRptDoc.PrintOptions.PrinterName = dialog1.PrinterSettings.PrinterName;
                    clsGlobales.myRptDoc.PrintToPrinter(copies, collate, fromPage, toPage);
                }

                clsGlobales.myRptDoc.Dispose();
                dialog1.Dispose();
            }

        }

        #endregion

        #region Metodo ShowReportB

        //METODO ShowReportB, para mostrar la Factura B unicamente.
        private void ShowReportPredPresuB(rptPedPresuB oRepPedPresuB, dsReportes oDsFactura, bool bPrintDialog = false)
        {
            //Establecer el DataSet como DataSource
            oRepPedPresuB.SetDataSource(oDsFactura);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepPedPresuB;

            oRepPedPresuB.DataDefinition.FormulaFields["NroComp"].Text = "'" + this.lblPunto.Text + "-" + nroComp.Text + "'";

            //Otras
            oRepPedPresuB.DataDefinition.FormulaFields["Fecha"].Text = "'" + dtFecha.Value.ToString("dd/MM/yyyy") + "'";
            oRepPedPresuB.DataDefinition.FormulaFields["RazonSocial"].Text = "'" + txtRazonSocial.Text.ToUpper() + "'";
            oRepPedPresuB.DataDefinition.FormulaFields["Domic"].Text = "'" + (txtDir.Text).ToUpper() + "'";
            oRepPedPresuB.DataDefinition.FormulaFields["IVA"].Text = "'" + cboTipoCliente.Text.ToUpper() + "'";
            oRepPedPresuB.DataDefinition.FormulaFields["CondicionVenta"].Text = "'" + cboCondicionCompra.Text.ToUpper() + "'";
            oRepPedPresuB.DataDefinition.FormulaFields["CUIT"].Text = "'" + txtCuit.Text + "'";

            double dtoImpo = (Convert.ToDouble(txtNeto.Text) * Convert.ToDouble(txtDto.Text)) / 100;
            oRepPedPresuB.DataDefinition.FormulaFields["Dto"].Text = "'" + txtDto.Text + "'";
            oRepPedPresuB.DataDefinition.FormulaFields["DtoImpo"].Text = "'" + dtoImpo.ToString("#0.00") + "'";

            //SUBTOTAL            
            oRepPedPresuB.DataDefinition.FormulaFields["Neto"].Text = "'" + txtNeto.Text + "'";
            oRepPedPresuB.DataDefinition.FormulaFields["Subtotal"].Text = "'" + txtSubTotal.Text + "'";
            
            //TRANSPORTE
            oRepPedPresuB.DataDefinition.FormulaFields["Transporte"].Text = "'" + cboTransporte.Text + "'";  

            if (Convert.ToInt32(cboTipoCliente.SelectedValue) == 1)
            {
                oRepPedPresuB.DataDefinition.FormulaFields["IVA10"].Text = "'" + txtIVA10.Text + "'";
                oRepPedPresuB.DataDefinition.FormulaFields["TotIVA"].Text = "'" + txtIVA.Text + "'";
            }

            //FLETE Y EXENTO
            oRepPedPresuB.DataDefinition.FormulaFields["Flete"].Text = "'" + txtCostoFlete.Text + "'";
            oRepPedPresuB.DataDefinition.FormulaFields["Exento"].Text = "'" + txtExento.Text + "'";

            oRepPedPresuB.DataDefinition.FormulaFields["Total"].Text = "'" + txtTotal.Text + "'";

            //Comprobante y pie
            oRepPedPresuB.DataDefinition.FormulaFields["linea-01"].Text = "' Razón Social: " + clsGlobales.cParametro.RazonSocial + "'";
            oRepPedPresuB.DataDefinition.FormulaFields["linea-02"].Text = "' Domicilio:'";
            oRepPedPresuB.DataDefinition.FormulaFields["linea-03"].Text = "'" + clsGlobales.cParametro.Direccion + "-" + clsGlobales.cParametro.Localidad + ", Córdoba" + "'";
            oRepPedPresuB.DataDefinition.FormulaFields["linea-04"].Text = "' Condición frente al Iva : " + clsGlobales.cParametro.CondicionIva + "'";
            oRepPedPresuB.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepPedPresuB.DataDefinition.FormulaFields["linea-06"].Text = "' Ingresos Brutos: " + "904/30-71658372-0" + "'";
            oRepPedPresuB.DataDefinition.FormulaFields["linea-07"].Text = "' Fecha de Inicio Actividades : " + "01/10/2019" + "'";
            oRepPedPresuB.DataDefinition.FormulaFields["Entrada"].Text = "'" + cboEntrada.Text + "'";

            if (!(bPrintDialog))
            {
                //Mostrar el reporte  
                frmShowReports myReportForm = new frmShowReports();
                myReportForm.Text = this.Text;
                myReportForm.ShowDialog();
            }
            else
            {
                PrintDialog dialog1 = new PrintDialog();

                int CopiesToPrinter = clsGlobales.cParametro.Pedidos;

                dialog1.AllowSomePages = true;
                dialog1.AllowPrintToFile = false;
                dialog1.PrinterSettings.Copies = (short)CopiesToPrinter;

                if (dialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    int copies = dialog1.PrinterSettings.Copies;
                    int fromPage = dialog1.PrinterSettings.FromPage;
                    int toPage = dialog1.PrinterSettings.ToPage;
                    bool collate = dialog1.PrinterSettings.Collate;

                    clsGlobales.myRptDoc.PrintOptions.PrinterName = dialog1.PrinterSettings.PrinterName;
                    clsGlobales.myRptDoc.PrintToPrinter(copies, collate, fromPage, toPage);
                }

                clsGlobales.myRptDoc.Dispose();
                dialog1.Dispose();
            }
        }

        #endregion        

        private void btnCostoFlete_Click(object sender, EventArgs e)
        {
            frmCalculoEnvio myForm = new frmCalculoEnvio(txtCostoFlete);
            myForm.ShowDialog();
        }

        private void rbnCP_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnCP.Checked)
            {
                txtCodigoCorreo.Text = "";
                txtCodigoCorreo.Enabled = true;
                txtCodigoCorreo.Focus();
            }
        }

        private void rbnCO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnCO.Checked)
            {
                txtCodigoCorreo.Text = "";
                txtCodigoCorreo.Enabled = true;
                txtCodigoCorreo.Focus();
            }
        }

        private void rbnNinguno_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnNinguno.Checked)
            {
                txtCodigoCorreo.Text = "";
                txtCodigoCorreo.Text = "No establecido";
                txtCodigoCorreo.Enabled = false;
            }
        }

        private void txtCodigoCorreo_Enter(object sender, EventArgs e)
        {
            txtCodigoCorreo.SelectionStart = 0;
            txtCodigoCorreo.SelectionLength = txtCodigoCorreo.Text.Length;
        }

        private void btnDeshacer_Click(object sender, EventArgs e)
        {
            //Todo a 0...
            if (clsGlobales.Left(myPresu.Codigo_Correo.ToUpper(), 2) == "CP")
            {
                rbnNinguno.Checked = false;
                rbnCO.Checked = false;
                rbnCP.Checked = true;
                txtCodigoCorreo.Enabled = true;

            }
            else if (clsGlobales.Left(myPresu.Codigo_Correo.ToUpper(), 2) == "CO")
            {
                rbnNinguno.Checked = false;
                rbnCP.Checked = false;
                rbnCO.Checked = true;
                txtCodigoCorreo.Enabled = true;
            }
            else
            {
                rbnCO.Checked = false;
                rbnCP.Checked = false;
                rbnNinguno.Checked = true;
                txtCodigoCorreo.Enabled = false;
            }
            
            //Set
            txtCodigoCorreo.Text = myPresu.Codigo_Correo;
        }

        private void txtCodigoCorreo_Click(object sender, EventArgs e)
        {
            txtCodigoCorreo.SelectionStart = 0;
            txtCodigoCorreo.SelectionLength = txtCodigoCorreo.Text.Length;
        }

        private void btnAddCli_Click(object sender, EventArgs e)
        {
            //Variables
            int iLastIdCli = 0;
            int iNewIdCli = 0;

            // Armo la cadena SQL
            string myCadenaSQL = "select max(IdCliente) as LastId from Clientes";
            // Creo una tabla que me va a almacenar el resultado de la consulta
            DataTable myLastDt = clsDataBD.GetSql(myCadenaSQL);
            
            // Recorro la tabla y le asigo los datos del proveedor al objeto
            foreach (DataRow row in myLastDt.Rows)
            {
              //Cargar el máximo valor de Id para Clientes
               iLastIdCli = Convert.ToInt32(row["LastId"]);
            }
            
            //----------------------------------------------------------------
            
            //Llamar a Clientes
            frmClientesABM myForm = new frmClientesABM(0, false, true, false);
            myForm.ShowDialog();

            //----------------------------------------------------------------

            //Vuelvo a tomar el último Id
            DataTable myNewDt = clsDataBD.GetSql(myCadenaSQL);

            // Recorro la tabla y le asigo los datos del proveedor al objeto
            foreach (DataRow row in myNewDt.Rows)
            {
                //Cargar el máximo valor de Id para Clientes
                iNewIdCli = Convert.ToInt32(row["LastId"]);
            }

            //Verificar si agregó o no, si agrego uno nuevo cargarlo a la pantalla
            if (iLastIdCli < iNewIdCli)
            {
                DialogResult dlResult = MessageBox.Show("Usted ha cargado un nuevo cliente, desea utilizarlo para un nuevo pedido? (se descartará el actual)?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Si confirma... cambiar estado
                if (dlResult == DialogResult.Yes)
                {
                    //Limpiar todo (pantalla, tablas temporales, etc.) casi el Load de nuevo.
                    this.LimpiarDatosPantalla();

                    //Quitar el cliente actualmente selecionado
                    EliminarClienteSeleccionado();
                    //Buscar Cliente
                    // Redimensiono el tamaño de la matriz
                    clsGlobales.ClientesSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ClientesSeleccionados, 1);
                    // A la posición creada le asigno el Id seleccionado
                    clsGlobales.ClientesSeleccionados[clsGlobales.ClientesSeleccionados.Length - 1] = iNewIdCli;
                    //Cliente Nuevo
                    this.CargarClienteNuevo();
                    //Habilitar
                    this.HabilitarOtros();
                    //Calculos
                    this.CalcularSubtotal();
                    this.CalcularTotal();
                    //Retorna
                    if (clsGlobales.ClientesSeleccionados.GetLength(0) > 0)
                    {
                        //Inhabilitar Boton
                        this.btnEditCli.Enabled = true;
                        this.btnCli.Enabled = false;
                        this.btnCostoFlete.Enabled = true;

                        //Ocultar o mostrar otros datos
                        ShowIvaStuff();
                    }                 
                }                
            }


        }

        private void btnPrintFast_Click(object sender, EventArgs e)
        {

            ImprimirPedPresu(true);

            if (this.bPrintF)
            {
                btnSalir.PerformClick();
            }
        }

     
    }
}

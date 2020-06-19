using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Prama.Clases;
using System.Windows.Forms;
using System.Diagnostics;

namespace Prama.Formularios.Ventas
{
    public partial class frmNotaCredito : Form
    {

        #region Declaracion Variables

        DataGridView dgvCompOrig;
        DataGridView dgvDetOrig;
        int IdTransporte = 0;
        int IdCliente = 0;
        int IdFormaPago = 0;

        double cNeto21 = 0;
        double cNeto10 = 0;
        double cNetoExento = 0;
        double cNeto = 0;
        double cNetoSinIva21 = 0;
        double cNetoSinIva10 = 0;

        double cNetoConIva21 = 0;
        double cNetoConIva10 = 0;

        public long nroComprobante = 0;

        //Objeto FEAFIP
        private FEAFIPLib.BIWSFEV1 wsfev1 = new FEAFIPLib.BIWSFEV1();

        public double dCantidad = 0; //GUARDA LA CANTIDAD ACTUAL PARA ESE PRODUCTO EN LA GRILLA

        #endregion

        #region Constructor

        public frmNotaCredito(DataGridView p_dgvCompOrig, DataGridView p_dgvDetOrig, int p_IdTransporte = 0, int p_IdCliente = 0, int p_IdFormaPago = 0)
        {
            InitializeComponent();

            //Traear datos de Origen ( Comprobante original y detalle articulos )
            dgvCompOrig = p_dgvCompOrig;
            dgvDetOrig = p_dgvDetOrig;
            IdTransporte = p_IdTransporte;
            IdCliente = p_IdCliente;
            IdFormaPago = p_IdFormaPago;
        }

        #endregion

        #region Metodo ShowDataCli

        private void ShowDataCli()
        {
            this.txtCuit.Text = dgvCompOrig.CurrentRow.Cells["CUIT"].Value.ToString();
            this.txtRazonSocial.Text = dgvCompOrig.CurrentRow.Cells["RazonSocial"].Value.ToString();
            this.txtDir.Text = dgvCompOrig.CurrentRow.Cells["Direccion"].Value.ToString() + ", " + dgvCompOrig.CurrentRow.Cells["Localidad"].Value.ToString() + "," + dgvCompOrig.CurrentRow.Cells["Provincia"].Value.ToString() + " (" + dgvCompOrig.CurrentRow.Cells["CP"].Value.ToString() + ")";
            this.txtTipoResponsable.Text = dgvCompOrig.CurrentRow.Cells["TipoResponsable"].Value.ToString();

            this.lblCompOrigen.Text = dgvCompOrig.CurrentRow.Cells["nComprobante"].Value.ToString();

            this.cboFormaPago.SelectedValue = IdFormaPago;

            //Tipo Comprobante
            if (Convert.ToInt32(dgvCompOrig.CurrentRow.Cells["IdTipoComprobante"].Value.ToString()) == 1)
            {
                cboComprobante.SelectedValue = 3;
            }
            else if (Convert.ToInt32(dgvCompOrig.CurrentRow.Cells["IdTipoComprobante"].Value.ToString()) == 6)
            {
                cboComprobante.SelectedValue = 8;

                //Ocultar y redimensionar Columnas
                dgvDet.Columns["Bonif"].Visible = false;
                dgvDet.Columns["SubtotalDto"].Visible = false;
                dgvDet.Columns["ValorIva"].Visible = false;
                dgvDet.Columns["SubTotalDet"].HeaderText = "SubTotal";
                dgvDet.Columns["Articulo"].Width = 450;   
            }

        }

        #endregion

        #region Metodo ShowDataDetalle

        private void ShowDataDetalle()
        {
            dgvDet.AutoGenerateColumns = false;
            //dgvDet.DataSource = dgvDetOrig.DataSource;


            //Contador
         //   int Item = 1;
            int filas = 0;

            //Mostrar Datos
            foreach (DataGridViewRow fila in dgvDetOrig.Rows)
            {
                /*Agregar Fila*/
                dgvDet.Rows.Add();

                // Cuento las filas de la grilla
                filas = dgvDet.Rows.Count;

                // Si la grilla no está vacía
                if (filas > 0)
                {
                    //Posiciono la grilla en la última fila
                    dgvDet.CurrentCell = dgvDet[2, filas - 1];
                }

                //Cargar Datos a la fila                
              //dgvDet.CurrentRow.Cells["Item"].Value = Item;
                dgvDet.CurrentRow.Cells["CodigoArticulo"].Value = fila.Cells["CodigoArticulo"].Value.ToString();
                dgvDet.CurrentRow.Cells["Articulo"].Value = fila.Cells["Articulo"].Value.ToString();
                dgvDet.CurrentRow.Cells["CantArt"].Value = Convert.ToInt32(fila.Cells["CantArt"].Value).ToString("#0");
                dgvDet.CurrentRow.Cells["PrecioArt"].Value = fila.Cells["PrecioArt"].Value.ToString();
                dgvDet.CurrentRow.Cells["AbreviaturaUnidad"].Value = fila.Cells["AbreviaturaUnidad"].Value.ToString();
                dgvDet.CurrentRow.Cells["Alicuota"].Value = fila.Cells["Alicuota"].Value.ToString();
                dgvDet.CurrentRow.Cells["Bonif"].Value = fila.Cells["Bonif"].Value.ToString();
                dgvDet.CurrentRow.Cells["SubtotalDto"].Value = fila.Cells["SubtotalDto"].Value.ToString();
                dgvDet.CurrentRow.Cells["ValorIva"].Value = fila.Cells["ValorIva"].Value.ToString();
                dgvDet.CurrentRow.Cells["SubTotalDet"].Value = fila.Cells["SubTotalDet"].Value.ToString();
                dgvDet.CurrentRow.Cells["CantOriginal"].Value = Convert.ToInt32(fila.Cells["CantArt"].Value).ToString("#0");
                dgvDet.CurrentRow.Cells["IdArticulo"].Value = fila.Cells["CodArt"].Value.ToString();
                dgvDet.CurrentRow.Cells["IdPropio"].Value = fila.Cells["IdPropio"].Value.ToString();
                
                //Contador
                //Item++;

            }


        }

        #endregion

        #region Metodo ShowStatics

        private void ShowStatics()
        {
            this.txtNeto.Text = dgvCompOrig.CurrentRow.Cells["Neto"].Value.ToString();
            this.txtDto.Text = dgvCompOrig.CurrentRow.Cells["Dto"].Value.ToString();
            this.txtFlete.Text = dgvCompOrig.CurrentRow.Cells["Flete"].Value.ToString();
            this.txtSubTotal.Text = dgvCompOrig.CurrentRow.Cells["SubtotaleF"].Value.ToString();
            this.txtExento.Text = dgvCompOrig.CurrentRow.Cells["Exento"].Value.ToString();
            this.txtIVA10.Text = dgvCompOrig.CurrentRow.Cells["IVA10"].Value.ToString();
            this.txtIVA.Text = dgvCompOrig.CurrentRow.Cells["fIVA"].Value.ToString();
            this.txtTotal.Text = dgvCompOrig.CurrentRow.Cells["TotalFactura"].Value.ToString();

            //Hide or Show components
            switch(Convert.ToInt32(cboComprobante.SelectedValue))
            {
                case 3:
                    this.txtIVA10.Visible=true;
                    this.txtIVA.Visible=true;
                    this.lblIVA.Visible = true;
                    this.lblIVA10.Visible = true;
                    break;
                case 8:
                    // this.txtIVA10.Visible=false;
                    // this.txtIVA.Visible=false;
                    // this.lblIVA.Visible = false;
                    // this.lblIVA10.Visible = false;
                    break;
            }
        }

        #endregion 

        #region Metodo LOAD

        private void frmNotaCredito_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 
			
            //TOOLTIPS
            this.CargarToolTips();

            //CARGAR COMBO TIPO DE COMPROBANTE
            clsDataBD.CargarComboTipoComprobanteVenta(this.cboComprobante, "TipoComprobantes", "Comprobante", "IdTipoComprobante");
            cboComprobante.SelectedIndex = -1;

            //CARGAR COMBO FORMA DE PAGO
            clsDataBD.CargarComboFormaPago(this.cboFormaPago, "FormasPago", "FormaPago", "IdFormaPago");
            cboFormaPago.SelectedValue = -1;   

            //Mostrar datos Cliente y Datos Detalle
            ShowDataCli();
            ShowDataDetalle();
            ShowStatics();

            //Limpiar
            cNeto21 = 0;
            cNeto10 = 0;
            cNetoExento = 0;
            cNeto = 0;
            cNetoSinIva21 = 0;
            cNetoSinIva10 = 0;

            //Fecha Comprobante
            dtFechaComp.Value = DateTime.Today;
            dtVtoPago.Value = DateTime.Today;
            dtVencCAE.Value = DateTime.Today;

            nroPunto.Text = clsGlobales.cParametro.PtoVtaPorDefecto.ToString();

            if (clsGlobales.ConB == null)
            {
                //Obtiene el ultimo nro de comprobante para Nota de Credito
                ObtenerLastNC();
            }
            else            
            {
                this.nroComp.Text = "";
            }
            //Recalcular
            CalcularTotal();

            //TITULO DE LA VENTANA
            this.Text = clsGlobales.cParametro.NombreFantasia + this.Text;

        }

        #endregion


        #region CalcularSubTotal

        private void CalcularSubTotal()
        {

            double cSubTotal = 0;

            //Mostrar Datos
            foreach (DataGridViewRow fila in dgvDet.Rows)
            {

                //Cantidad por Precio Unitario
                cSubTotal = Convert.ToDouble(fila.Cells["CantArt"].Value) * Convert.ToDouble(fila.Cells["PrecioArt"].Value);

                //Asignacion es para Nota de Credito B
                if (Convert.ToInt32(cboComprobante.SelectedValue) == 8)
                {
                    fila.Cells["SubTotalDet"].Value = cSubTotal.ToString("#0.00");
                }
                else
                {

                    fila.Cells["SubtotalDto"].Value = cSubTotal.ToString("#0.00");
                    fila.Cells["ValorIva"].Value = (cSubTotal * Convert.ToDouble(Convert.ToDouble(fila.Cells["Alicuota"].Value )/ 100)).ToString("#0.00");
                    fila.Cells["SubtotalDet"].Value = (cSubTotal + Convert.ToDouble(fila.Cells["ValorIva"].Value)).ToString("#0.00");
                }
                //                

            }
        }

        #endregion

        #region CalcularTotal

        private void CalcularTotal()
        {
            cNeto21 = 0;
            cNeto10 = 0;
            cNetoConIva21 = 0;
            cNetoConIva10 = 0;
            cNetoExento = 0;
            cNeto = 0;

            //Mostrar Datos
            foreach (DataGridViewRow fila in dgvDet.Rows)
            {
                //sim ES A
                if (Convert.ToInt32(cboComprobante.SelectedValue) == 3)
                {
                    if (Convert.ToDouble(fila.Cells["Alicuota"].Value) == 21)
                    {
                        cNeto21 += Convert.ToDouble(fila.Cells["CantArt"].Value) * Convert.ToDouble(fila.Cells["PrecioArt"].Value);
                    }
                    else if (Convert.ToDouble(fila.Cells["Alicuota"].Value) == 10.5)
                    {
                        cNeto10 += Convert.ToDouble(fila.Cells["CantArt"].Value) * Convert.ToDouble(fila.Cells["PrecioArt"].Value);
                    }
                    else
                    {
                        cNetoExento += Convert.ToDouble(fila.Cells["CantArt"].Value) * Convert.ToDouble(fila.Cells["PrecioArt"].Value);
                    }
                }
                //SINO
                else
                {
                    if (Convert.ToDouble(fila.Cells["Alicuota"].Value) == 21)
                    {
                        cNetoSinIva21 += Convert.ToDouble(((Convert.ToDouble(fila.Cells["CantArt"].Value) * Convert.ToDouble(fila.Cells["PrecioArt"].Value)) / 1.21).ToString("#0.00"));
                        cNetoConIva21 += (Convert.ToDouble(fila.Cells["CantArt"].Value) * Convert.ToDouble(fila.Cells["PrecioArt"].Value));
                    }
                    else if (Convert.ToDouble(fila.Cells["Alicuota"].Value) == 10.5)
                    {
                        cNetoSinIva10 += Convert.ToDouble(((Convert.ToDouble(fila.Cells["CantArt"].Value) * Convert.ToDouble(fila.Cells["PrecioArt"].Value)) / 1.05).ToString("#0.00"));
                        cNetoConIva10 += (Convert.ToDouble(fila.Cells["CantArt"].Value) * Convert.ToDouble(fila.Cells["PrecioArt"].Value));
                    }
                    else
                    {
                        cNetoExento += Convert.ToDouble(fila.Cells["CantArt"].Value) * Convert.ToDouble(fila.Cells["PrecioArt"].Value);
                    }
                }

            }

            //A
            if (Convert.ToInt32(cboComprobante.SelectedValue) == 3)
            {
                cNeto = cNeto21 + cNeto10;

                txtNeto.Text = cNeto.ToString("#0.00");

                txtSubTotal.Text = cNeto.ToString("#0.00");

                txtExento.Text = cNetoExento.ToString("#0.00");

                txtIVA.Text = Convert.ToDouble(cNeto21 * 0.21).ToString("#0.00");

                txtIVA10.Text = Convert.ToDouble(cNeto10 * 0.105).ToString("#0.00");

                cNetoExento += Convert.ToDouble(txtFlete.Text);

                double cTotal = cNeto + cNetoExento + Convert.ToDouble(cNeto21 * 0.21) + Convert.ToDouble(cNeto10 * 0.105);

                txtTotal.Text = cTotal.ToString("#0.00");
            }
            else //B 
            {
                 //CALCLULAR
                    // cNeto = (cNetoSinIva21*1.21)+(cNetoSinIva10*1.105);
                    double cNeto2 = cNetoConIva21 + cNetoConIva10;
                 //NETO              
                    // this.txtNeto.Text = cNeto.ToString("#0.00");
                    this.txtNeto.Text = cNeto2.ToString("#0.00");
                 //SUBTOTAL
                    // txtSubTotal.Text = cNeto.ToString("#0.00");
                    txtSubTotal.Text = cNeto2.ToString("#0.00");
                 //EXENTO           
                    txtExento.Text = cNetoExento.ToString("#0.00");
                 //IVA
                    // txtIVA.Text = Convert.ToDouble(cNetoSinIva21 * 0.21).ToString("#0.00");
                    // txtIVA10.Text = Convert.ToDouble(cNetoSinIva10 * 0.105).ToString("#0.00");

                    txtIVA.Text = Convert.ToDouble(cNetoConIva21 - (cNetoConIva21 / 1.21)).ToString("#0.00");
                    txtIVA10.Text = Convert.ToDouble(cNetoConIva10 - (cNetoConIva10 / 1.105)).ToString("#0.00");

                //TOTAL
                    txtTotal.Text = (cNeto2 + cNetoExento).ToString("#0.00");
            }

        }
                

        #endregion

        private void ObtenerLastNC()
        {

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
            //if (wsfev1.login("H:\\PRAMA_ULTIMATE" + "\\" + clsGlobales.CertificadoAFIP, sClave))
            {
                if (wsfev1.recuperaLastCMP(clsGlobales.cParametro.PtoVtaPorDefecto, Convert.ToInt32(cboComprobante.SelectedValue), ref nroComprobante))
                {
                    nroComprobante += 1;
                    this.nroComp.Text = nroComprobante.ToString("D8");
                    wsfev1.reset();
                }
            }
            else
            {
                MessageBox.Show("Error al intentar la conexión con el Servidor de la AFIP. Reintente en unos instantes!.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error); //(wsfev1.ErrorDesc);
                this.Close();
            }
        }

        #region CargarToolTips

        private void CargarToolTips()
        {
            toolTip1.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip2.SetToolTip(this.btnSalir, "Salir!");
        }

        #endregion

        private void btnQuitarArt_Click(object sender, EventArgs e)
        {

                // Elimino la fila de la grilla
                dgvDet.Rows.RemoveAt(dgvDet.CurrentRow.Index);

                //RECALCULAR
                this.CalcularSubTotal();
                this.CalcularTotal();

           
        }

        private void dgvDet_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvDet.CurrentRow;

            dCantidad = Convert.ToDouble(row.Cells["CantOriginal"].Value);

            if (row.Cells["CantArt"].Value == null)
            {
                //Establecer null al costo final
                dgvDet.CurrentRow.Cells["CantArt"].Value = dCantidad;
            }
            else
            {
                if (Convert.ToDouble(row.Cells["CantArt"].Value) == 0)
                {
                    dgvDet.CurrentRow.Cells["CantArt"].Value = dCantidad;
                }
                else if ((Convert.ToDouble(row.Cells["CantArt"].Value) > (Convert.ToDouble(row.Cells["CantOriginal"].Value.ToString()))))
                {
                    dgvDet.CurrentRow.Cells["CantArt"].Value = dCantidad;
                }
            }

            CalcularSubTotal();

            CalcularTotal();
        }

        private void dgvDet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDet.Rows.Count > 0)
            {
                // Armo un switch para las columnas de la grilla
                switch (this.dgvDet.Columns[this.dgvDet.CurrentCell.ColumnIndex].Index)
                {
                    // Para todas las columnas menos para cantidad y pecio
                    case 0:
                    case 1:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        // Pongo el foco en la columna Canidad    
                        this.dgvDet.CurrentCell = dgvDet["CantArt", dgvDet.CurrentRow.Index];
                        // Salgo del switch
                        break;
                }
            }
        }

        private void dgvDet_KeyUp(object sender, KeyEventArgs e)
        {
            if (dgvDet.Rows.Count > 0)
            {
                // Armo un switch para las columnas de la grilla
                switch (this.dgvDet.Columns[this.dgvDet.CurrentCell.ColumnIndex].Index)
                {
                    // Para todas las columnas menos para cantidad y pecio
                    case 0:
                    case 1:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        // Pongo el foco en la columna Canidad    
                        this.dgvDet.CurrentCell = dgvDet["CantArt", dgvDet.CurrentRow.Index];
                        // Salgo del switch
                        break;
                }
            }
        }

        private void dgvDet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDet.Rows.Count > 0)
            {
                // Armo un switch para las columnas de la grilla
                switch (this.dgvDet.Columns[this.dgvDet.CurrentCell.ColumnIndex].Index)
                {
                    // Para todas las columnas menos para cantidad y pecio
                    case 0:
                    case 1:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        // Pongo el foco en la columna Canidad    
                        this.dgvDet.CurrentCell = dgvDet["CantArt", dgvDet.CurrentRow.Index];
                        // Salgo del switch
                        break;
                }
            }
        }

        private void dgvDet_SelectionChanged(object sender, EventArgs e)
        {
                        // Si la grilla tiene artículos
            if (dgvDet.RowCount > 0)
            {
                // Almaceno én una variable la posición de fila en la que me encuentro
                int fila = dgvDet.CurrentRow.Index;
                // Pongo el foco de la fila en la columna cantidad
                dgvDet.CurrentCell = dgvDet["CantArt", fila];
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
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


            //VERIFICAR TIPO COMPROBANTE Y EN VIRTUD DE ESO PROCESAR FACTURA QUE CORRESPONDE
            if (Convert.ToInt32(cboComprobante.SelectedValue) == 3)
            {
                this.ProcesarNotaCreditoA();
            }
            else
            {
                //Procesar
                this.ProcesarNotaCreditoB();
            }
            //
        }


        #region Metodo ProcesarNotaCreditoB

        //METODO QUE PROCESA LA FACTURA B
        private void ProcesarNotaCreditoB()
        {

            //SI ESTOY EN B/N Y ES PENDIENTE, SE FACTURA EN NEGRO DIRECTO
            if (clsGlobales.ConB != null)
            {
                //GUARDAR FACTURA
                GuardarNotaCredito(1);

                //GUARDAR DETALLE FACTURA
                GuardarNotaCreditoDet(1);

                //DESCONTAR STOCK
                DevuelveStock(1);

                //ACTUALIZA SALDO
                ActualizarSaldoCli(IdCliente, Convert.ToDouble(txtTotal.Text), 1);

                //IMPRIMIR NOTA DE CREDITO
                ImprimirNC(1);

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
                //string bImpo = (Convert.ToDouble(txtSubTotal.Text) - Convert.ToDouble(txtIVA.Text)).ToString("#0.00");

                if (Convert.ToInt32(cboComprobante.SelectedValue) == 3)
                {
                    //Agregar Factura A
                    wsfev1.agregaFactura(1, 80, long.Parse(txtCuit.Text), Convert.ToInt32(nroComp.Text),
                    Convert.ToInt32(nroComp.Text), FechaComp, Convert.ToDouble(txtTotal.Text), 0, (cNetoSinIva21 + cNetoSinIva10), Convert.ToDouble(txtExento.Text), null, null, null, "PES", 1);

                    //Agregar IVA 21 y 10.5
                    if (!(Convert.ToDouble(txtIVA.Text) == 0))
                    {
                        wsfev1.agregaIVA(5, Convert.ToDouble(cNetoSinIva21.ToString("#0.00")), Convert.ToDouble(txtIVA.Text));
                    }
                    if (!(Convert.ToDouble(txtIVA10.Text) == 0))
                    {
                        wsfev1.agregaIVA(4, Convert.ToDouble(cNetoSinIva10.ToString("#0.00")), Convert.ToDouble(txtIVA10.Text));
                    }
                }
                else // AGREGAR FACTURA B
                {
                    // Neto de la factura
                    double dNetoTipoB = (cNetoConIva21 / 1.21) + (cNetoConIva10 / 1.105);
                    dNetoTipoB = Convert.ToDouble(dNetoTipoB.ToString("#0.00"));
                    //Agregar Factura B
                    wsfev1.agregaFactura(1, 80, long.Parse(txtCuit.Text), Convert.ToInt32(nroComp.Text),
                    Convert.ToInt32(nroComp.Text), FechaComp, Convert.ToDouble(txtTotal.Text), 0, (dNetoTipoB), Convert.ToDouble(txtExento.Text), null, null, null, "PES", 1);

                    //Agregar IVA 21 y 10.5
                    if (!(Convert.ToDouble(txtIVA.Text) == 0))
                    {
                        wsfev1.agregaIVA(5, Convert.ToDouble((cNetoConIva21/1.21).ToString("#0.00")), Convert.ToDouble(txtIVA.Text));
                    }
                    if (!(Convert.ToDouble(txtIVA10.Text) == 0))
                    {
                        wsfev1.agregaIVA(4, Convert.ToDouble((cNetoConIva10 / 1.105).ToString("#0.00")), Convert.ToDouble(txtIVA10.Text));
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
                        GuardarNotaCredito(0);

                        //GUARDAR DETALLE FACTURA
                        GuardarNotaCreditoDet(0);

                        //MENSAJE
                        MessageBox.Show("La Nota de Crédito B, ha sido Autorizada por la AFIP! - CAE y Vencimiento: " + cae + " " + vencimiento.ToShortDateString(), "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //DESCONTAR STOCK
                        DevuelveStock(0);

                        //ACTUALIZA SALDO
                        ActualizarSaldoCli(IdCliente, Convert.ToDouble(txtTotal.Text));

                        //IMPRIMIR NOTA DE CREDITO
                        ImprimirNC(0);
                        
                        //ACTUALIZAR SALDO FACTURA


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

        #region Metodo ProcesarNotaCreditoA

        //METODO QUE PROCESA LA NOTA CREDITO A
        private void ProcesarNotaCreditoA()
        {
            //NOTA CREDITO NEGRO
            if (clsGlobales.ConB != null)
            {
                //PRESUPUESTO NO SE PUEDE EDITAR, NO SALE PENDIENTE DE AQUI.
                //GUARDAR FACTURA
                GuardarNotaCredito(1);

                //GUARDAR DETALLE FACTURA
                GuardarNotaCreditoDet(1);

                //DESCONTAR STOCK
                DevuelveStock(1); // Si es nota de crédito, debe reponer el stock de los productos que devuelve

                //ACTUALIZA SALDO
                ActualizarSaldoCli(IdCliente, Convert.ToDouble(txtTotal.Text), 1);

                //IMPRIMIR NOTA DE CREDITO
                ImprimirNC(1);

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

                //Agregar Factura
                wsfev1.agregaFactura(1, 80, long.Parse(txtCuit.Text), Convert.ToInt32(nroComp.Text),
                Convert.ToInt32(nroComp.Text), FechaComp, Convert.ToDouble(txtTotal.Text), 0, Convert.ToDouble(txtSubTotal.Text), Convert.ToDouble(txtExento.Text), null, null, null, "PES", 1);

                
                //Agregar IVA 21 y 10.5
                if (!(Convert.ToDouble(txtIVA.Text) == 0))
                {
                    wsfev1.agregaIVA(5, Convert.ToDouble(cNeto21.ToString("#0.00")), Convert.ToDouble(txtIVA.Text));
                }
                if (!(Convert.ToDouble(txtIVA10.Text) == 0))
                {
                    wsfev1.agregaIVA(4, Convert.ToDouble(cNeto10.ToString("#0.00")), Convert.ToDouble(txtIVA10.Text));
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
                        GuardarNotaCredito(0); ;

                        //GUARDAR DETALLE FACTURA
                        GuardarNotaCreditoDet(0);

                        //MENSAJE
                        MessageBox.Show("La Nota de Crédito A, ha sido Autorizada por la AFIP! - CAE: " + cae + " - Vencimiento: " + vencimiento.ToShortDateString(), "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //DESCONTAR STOCK
                        DevuelveStock(0);

                        //ACTUALIZA SALDO
                        ActualizarSaldoCli(IdCliente, Convert.ToDouble(txtTotal.Text));
                        
                        //IMPRIMIR NOTA DE CREDITO
                        ImprimirNC(0);
                   
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

        #region Método que actualiza el saldo del Clientes

        private void ActualizarSaldoCli(int IdCli, double Tot, int p_Dest = 0)
        {
            // Variable que almacena el saldo del cliente
            
            double NewSaldo = 0;
            //Si viene 0, guarda en Clientes
            if (p_Dest == 0)
            {
                // Armo la cadena SQl para traer el saldo anterior del cliente
                string myCadenaSQLSaldo = "select * from Clientes where IdCliente = " + IdCli;
                // Ejecuto la consulta y paso los datos a la tabla
                DataTable mySaldoProveedor = clsDataBD.GetSql(myCadenaSQLSaldo);
                // Recorro la tabla para obtener el saldo inicial del cliente
                foreach (DataRow rowCli in mySaldoProveedor.Rows)
                {


                 //   NewSaldo = Convert.ToDouble(rowCli["SaldoAFavor"]) + Convert.ToDouble(txtTotal.Text);
                    //Le sumo el total de la nota de credito al saldo a favor
                 //   myCadenaSQLSaldo = "UPDATE Clientes SET SaldoAFavor = SaldoAFavor + " + NewSaldo + "  where IdCliente = " + IdCli;

                 //   NewSaldo = Convert.ToDouble(rowCli["SaldoAFavor"]) + Convert.ToDouble(txtTotal.Text);
                 //Le sumo el total de la nota de credito al saldo a favor
                    myCadenaSQLSaldo = "UPDATE Clientes SET SaldoAFavor = SaldoAFavor + " + Convert.ToDouble(txtTotal.Text) + "  where IdCliente = " + IdCli;
                   
                }

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

                    //Traigo el Saldo A Favor del Cliente
                    //NewSaldo = Convert.ToDouble(rowProv["SaldoAFavor"]) + Convert.ToDouble(txtTotal.Text);
                    //Le sumo el total de la nota de credito al saldo a favor
                    myCadenaSQL = "UPDATE SaldoCliProv SET SaldoAFavor = SaldoAFavor + " + Convert.ToDouble(txtTotal.Text) + "  where IdCliente = " + IdCli;
                }

                // Actualizo el saldo
                clsDataBD.GetSqlB(myCadenaSQL);
            }
        }

        #endregion

        #region DevuelveStock

        //Descuenta stock en los productos
        private void DevuelveStock(int p_Con)
        {

            string myCadSQL = "";

            //Recorrer la grilla
            foreach (DataGridViewRow row in dgvDet.Rows)
            {
                //Updatear stock en articulos
                myCadSQL = "Update Articulos Set Stock = Stock + " + Convert.ToDouble(row.Cells["CantArt"].Value) + " where IdArticulo = " + Convert.ToInt32(row.Cells["IdArticulo"].Value);
                if (p_Con == 0)
                {
                    clsDataBD.GetSql(myCadSQL);

                    //Grabar el movimiento del stock
                    GrabarMovimientoStock(Convert.ToInt32(row.Cells["IdArticulo"].Value), 3, dtFechaComp.Value.Date, Convert.ToDouble(row.Cells["CantArt"].Value), 0);
                }
                else
                {
                    clsDataBD.GetSql(myCadSQL);
                    //Grabar el movimiento del stock
                    GrabarMovimientoStock(Convert.ToInt32(row.Cells["IdArticulo"].Value), 3, dtFechaComp.Value.Date, Convert.ToDouble(row.Cells["CantArt"].Value), 1);
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

        #region Metodo GuardarNotaCreditoDet

        //GUARDA EL DETALLE DE LA FACTURA EN LA TABLE eFacturaDetalle

        private void GuardarNotaCreditoDet(int p_Con)
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
                LastIdFactura = clsDataBD.RetornarUltimoIdB("eFactura", "IdFactura");
            }

            //Recorrer la grilla
            foreach (DataGridViewRow row in dgvDet.Rows)
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
                myFactDet.Cantidad = Convert.ToDouble(row.Cells["CantArt"].Value);
                myFactDet.Alicuota = Convert.ToDouble(row.Cells["Alicuota"].Value);
                myFactDet.IdArticulo = Convert.ToInt32(row.Cells["IdArticulo"].Value);
                myFactDet.IdProducto = Convert.ToInt32(row.Cells["IdPropio"].Value);
                if (Convert.ToInt32(cboComprobante.SelectedValue) == 3)
                {
                    myFactDet.Precio = Convert.ToDouble(row.Cells["PrecioArt"].Value);
                    myFactDet.Subtotal = Convert.ToDouble(row.Cells["SubTotalDet"].Value);
                }
                else
                {
                    myFactDet.Precio = Convert.ToDouble(row.Cells["PrecioArt"].Value);
                    myFactDet.Subtotal = Convert.ToDouble(row.Cells["SubTotalDet"].Value);
                }
                myFactDet.Dto = Convert.ToDouble(row.Cells["Bonif"].Value);
                myFactDet.SubTotalDto = Convert.ToDouble(row.Cells["SubTotalDto"].Value);

                myFactDet.IVA = Convert.ToDouble(row.Cells["ValorIva"].Value);



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

        #region Metodo GuardarNotaCredito

        //Guarda la factura autorizada con CAE y Vencimiento de CAE
        private void GuardarNotaCredito(int p_Con)
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

                myFact.Nro = clsDataBD.getUltComp("Ult_NCN", clsGlobales.cParametro.PtoVtaPorDefecto, 1); ;
                myFact.PuntoNro = myFact.Punto.ToString("D4") + "-" + myFact.Nro.ToString("D8");

                //Update Codigo Automatico
                string mySQL = "UPDATE PuntosVentaAFIP SET Ult_NCN = " + (myFact.Nro + 1) + " WHERE Punto = " + clsGlobales.cParametro.PtoVtaPorDefecto;
                clsDataBD.GetSqlB(mySQL);
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
            myFact.IdTransporte = IdTransporte;
            myFact.Dto = 0;
            myFact.Flete = 0;
            myFact.IdTipoComprobante = Convert.ToInt32(cboComprobante.SelectedValue);

            if (myFact.IdTipoComprobante == 3 || myFact.IdTipoComprobante == 8)
            { 
                myFact.Saldo = 0; 
            }
            else
            {
                myFact.Saldo = myFact.Total;
            }
            if (chkProd.Checked) { myFact.IncluyeProd = 1; } else { myFact.IncluyeProd = 0; }
            if (chkServ.Checked) { myFact.IncluyeServ = 1; } else { myFact.IncluyeServ = 0; }

            
            myFact.IdFormaPago = Convert.ToInt32(cboFormaPago.SelectedValue);
            if (p_Con == 0)
            {
                myFact.IdFactura = clsDataBD.RetornarUltimoId("eFactura", "IdFactura") + 1;
            }
            else
            {
                myFact.IdFactura = clsDataBD.RetornarUltimoIdB("eFactura", "IdFactura") + 1;
            }
            myFact.IdCliente = IdCliente;
            myFact.FechaVencPago = dtFechaComp.Value;

            //NETO IVA VENTA!
            if (myFact.IdTipoComprobante == 8) // NOTA CREDITO B
            {
                myFact.NetoIvaVta = myFact.Total - (myFact.IVA21 + myFact.IVA10 + myFact.Exento);
            }
            else //NOTA CREDITO A
            {
                myFact.NetoIvaVta = myFact.Neto;
            }

            myFact.Codigo_Correo = "No Establecido";
            myFact.PuntoNrOrig = lblCompOrigen.Text;
            myFact.IdMotivo = 2; //TIPO MOTIVO NOTA CREDITO 2 -> DEVOLUCION

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
                                                    " Codigo_Correo," + 
                                                    " PuntoNrOrig," +
                                                    " IdMotivo " + ") values (" + myFact.IdFactura + ",'" +
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
                                                                         myFact.NetoIvaVta.ToString().Replace(",", ".") + "," +
                                                                         myFact.Saldo.ToString().Replace(",", ".") + ",'" +
                                                                         myFact.Codigo_Correo + "','" + 
                                                                         myFact.PuntoNrOrig + "'," +
                                                                         myFact.IdMotivo + ")";

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

        #region Metodo: ImprimirNC

        private void ImprimirNC(int p_Tipo = 0)
        {
            // capturo la posición de la fila
            clsGlobales.indexFila = this.dgvDet.CurrentRow.Index;

            int dgvFilas = 0;

            //Data Set
            dsReportes oDsFactura = new dsReportes();
            
            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            dgvFilas = dgvDet.Rows.Count;

            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            dgvFilas = dgvDet.Rows.Count;

            for (int i = 0; i < dgvFilas; i++)
            {
                oDsFactura.Tables["dtFacturaVenta"].Rows.Add
                (new object[] { 
                    dgvDet[0,i].Value.ToString(),    
                    dgvDet[1,i].Value.ToString(),
                    dgvDet[2,i].Value.ToString(),
                    dgvDet[4,i].Value.ToString(),
                    dgvDet[3,i].Value.ToString(),
                    dgvDet[6,i].Value.ToString(),
                    dgvDet[7,i].Value.ToString(),
                    dgvDet[5,i].Value.ToString(),
                    dgvDet[9,i].Value.ToString()});
            }           

   
            //ELIMINAR ARCHIVO
            BorrarArchivo(Application.StartupPath + "\\AFIP.jpg");

            //Factura A
            if (Convert.ToInt32(cboComprobante.SelectedValue) == 1 || Convert.ToInt32(cboComprobante.SelectedValue) == 3)
            {


                //Cargar Reporte        
                if (p_Tipo == 0)
                {
                  //Objeto Reporte
                  rptFacturaVta oRepFacturaVta = new rptFacturaVta();

                  oRepFacturaVta.Load(Application.StartupPath + "\\rptNCVta.rpt");

                  //Tipo Comprobante
                  oRepFacturaVta.DataDefinition.FormulaFields["TipoComprobante"].Text = "'" + "A" + "'";
                  oRepFacturaVta.DataDefinition.FormulaFields["CodigoComp"].Text = "'" + "COD. 03" + "'";

                  //CARGAR FORMULAS Y MOSTRAR REPORTE
                  ShowReportA(oRepFacturaVta, oDsFactura, 0);

                }
                else
                {

                  //Objeto Reporte
                  rptFacturaVta_1 oRepFacturaVta = new rptFacturaVta_1();

                  oRepFacturaVta.Load(Application.StartupPath + "\\rptFacturaVta-1.rpt");

                  //Tipo Comprobante
                  oRepFacturaVta.DataDefinition.FormulaFields["TipoComprobante"].Text = "'" + "X" + "'";
                  oRepFacturaVta.DataDefinition.FormulaFields["CodigoComp"].Text = "'" + "" + "'";

                  int NroComp = clsDataBD.getUltComp("Ult_NCN", clsGlobales.cParametro.PtoVtaPorDefecto, 1);
                  oRepFacturaVta.DataDefinition.FormulaFields["NroComp"].Text = "'" + clsGlobales.cParametro.PtoVtaPorDefecto.ToString("D4") + "-" + NroComp.ToString("D8") + "'";

                  //CARGAR FORMULAS Y MOSTRAR REPORTE
                  ShowReportA1(oRepFacturaVta, oDsFactura, 1);
                }
                

            } //FActura B
            else
            {         

                //Cargar Reporte        
                if (p_Tipo == 0)
                {
                    //Objeto Reporte
                    rptFacturaVtaB oRepFacturaVta = new rptFacturaVtaB();

                    oRepFacturaVta.Load(Application.StartupPath + "\\rptNCVtaB.rpt");

                    oRepFacturaVta.DataDefinition.FormulaFields["TipoComprobante"].Text = "'" + "B" + "'";
                    oRepFacturaVta.DataDefinition.FormulaFields["CodigoComp"].Text = "'" + "COD. 08" + "'";

                    //CARGAR FORMULAS Y MOSTRAR REPORTE
                    ShowReportB(oRepFacturaVta, oDsFactura, 0);
                }
                else
                {

                    //Objeto Reporte
                    rptFacturaVtaB_1 oRepFacturaVta = new rptFacturaVtaB_1();

                    oRepFacturaVta.Load(Application.StartupPath + "\\rptFacturaVtaB-1.rpt");

                    oRepFacturaVta.DataDefinition.FormulaFields["TipoComprobante"].Text = "'" + "X" + "'";
                    oRepFacturaVta.DataDefinition.FormulaFields["CodigoComp"].Text = "'" + "" + "'";

                    int NroComprobante = clsDataBD.getUltComp("Ult_NCN", clsGlobales.cParametro.PtoVtaPorDefecto, 1);
                    oRepFacturaVta.DataDefinition.FormulaFields["NroComp"].Text = "'" + clsGlobales.cParametro.PtoVtaPorDefecto.ToString("D4") + "-" + NroComprobante.ToString("D8") + "'";

                    //CARGAR FORMULAS Y MOSTRAR REPORTE
                    ShowReportB1(oRepFacturaVta, oDsFactura,1);
                }
               
            }

            //ELIMINAR ARCHIVO
            BorrarArchivo(Application.StartupPath + "\\AFIP.jpg");
        }

        #endregion
        
        #region Metodo ShowReportA1

        //METODO ShowReportB, para mostrar la Factura B unicamente.
        private void ShowReportA1(rptFacturaVta_1 oRepFacturaVta, dsReportes oDsFactura, int p_Tipo = 0)
        {
            //Establecer el DataSet como DataSource
            oRepFacturaVta.SetDataSource(oDsFactura);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepFacturaVta;

            //oRepFacturaVta.DataDefinition.FormulaFields["NroComp"].Text = "'" + this.nroPunto.Text + "-" + nroComp.Text + "'";

            //Otras
            oRepFacturaVta.DataDefinition.FormulaFields["Fecha"].Text = "'" + dtFechaComp.Value.ToString("dd/MM/yyyy") + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["RazonSocial"].Text = "'" + txtRazonSocial.Text.ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["Domic"].Text = "'" + (txtDir.Text).ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["IVA"].Text = "'" + this.txtTipoResponsable.Text.ToUpper() + "'";
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

            if (Convert.ToInt32(cboComprobante.SelectedValue) == 1 || Convert.ToInt32(cboComprobante.SelectedValue) == 3)
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
            oRepFacturaVta.DataDefinition.FormulaFields["linea-06"].Text = "' Ingresos Brutos: " + "904/30-71658372-0" + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-07"].Text = "' Fecha de Inicio Actividades : " + "01/10/2019" + "'";

            ////B
            //if (p_Tipo == 0)
            //{
            //    //Llamar a BCAafip
            //    Process p = new Process();
            //    ProcessStartInfo psi = new ProcessStartInfo(Application.StartupPath + "\\BCAfip.exe");
            //    psi.Arguments = " -mod 2 -raz 2.00 -vis -2 -alt 100 -ali 0 -fno Tahoma -fsz 12 -cui " + txtCuit.Text + " -tip 08 -pto " + clsGlobales.cParametro.PtoVtaPorDefecto.ToString("00") + " -cae " + txtCAE.Text + " -vto " + this.dtVencCAE.Value.ToString("yyyyMMdd") + " -out " + Application.StartupPath + "\\AFIP.jpg";
            //    p.StartInfo = psi;
            //    p.Start();

            //    string path = Application.StartupPath + "\\AFIP.jpg";
            //    oRepFacturaVta.SetParameterValue("picturePath", path);
            //}

            //Llamar al reporte
            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports(2);
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();

        }

        #endregion

        #region Metodo ShowReportB1

        //METODO ShowReportB, para mostrar la Factura B unicamente.
        private void ShowReportB1(rptFacturaVtaB_1 oRepFacturaVta, dsReportes oDsFactura, int p_Tipo = 0)
        {
            //Establecer el DataSet como DataSource
            oRepFacturaVta.SetDataSource(oDsFactura);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepFacturaVta;

            //oRepFacturaVta.DataDefinition.FormulaFields["NroComp"].Text = "'" + this.nroPunto.Text + "-" + nroComp.Text + "'";

            //Otras
            oRepFacturaVta.DataDefinition.FormulaFields["Fecha"].Text = "'" + dtFechaComp.Value.ToString("dd/MM/yyyy") + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["RazonSocial"].Text = "'" + txtRazonSocial.Text.ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["Domic"].Text = "'" + (txtDir.Text).ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["IVA"].Text = "'" + this.txtTipoResponsable.Text.ToUpper() + "'";
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

            if (Convert.ToInt32(cboComprobante.SelectedValue) == 1 || Convert.ToInt32(cboComprobante.SelectedValue) == 3)
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
            oRepFacturaVta.DataDefinition.FormulaFields["linea-06"].Text = "' Ingresos Brutos: " + "904/30-71658372-0" + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-07"].Text = "' Fecha de Inicio Actividades : " + "01/10/2019" + "'";

            ////B
            //if (p_Tipo == 0)
            //{
            //    //Llamar a BCAafip
            //    Process p = new Process();
            //    ProcessStartInfo psi = new ProcessStartInfo(Application.StartupPath + "\\BCAfip.exe");
            //    psi.Arguments = " -mod 2 -raz 2.00 -vis -2 -alt 100 -ali 0 -fno Tahoma -fsz 12 -cui " + txtCuit.Text + " -tip 08 -pto " + clsGlobales.cParametro.PtoVtaPorDefecto.ToString("00") + " -cae " + txtCAE.Text + " -vto " + this.dtVencCAE.Value.ToString("yyyyMMdd") + " -out " + Application.StartupPath + "\\AFIP.jpg";
            //    p.StartInfo = psi;
            //    p.Start();

            //    string path = Application.StartupPath + "\\AFIP.jpg";
            //    oRepFacturaVta.SetParameterValue("picturePath", path);
            //}

            //Llamar al reporte
            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports(2);
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();

        }

        #endregion
        
        #region Metodo ShowReportA

        //METODO ShowReportB, para mostrar la Factura A unicamente.
        private void ShowReportA(rptFacturaVta oRepFacturaVta, dsReportes oDsFactura, int p_Tipo=0)
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
            oRepFacturaVta.DataDefinition.FormulaFields["IVA"].Text = "'" + this.txtTipoResponsable.Text.ToUpper() + "'";
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
            if (Convert.ToInt32(cboComprobante.SelectedValue) == 1 || Convert.ToInt32(cboComprobante.SelectedValue) == 3)
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
            oRepFacturaVta.DataDefinition.FormulaFields["linea-06"].Text = "' Ingresos Brutos: " + "904/30-71658372-0" + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-07"].Text = "' Fecha de Inicio Actividades : " + "01/10/2019" + "'";

            //B
            if (p_Tipo == 0)
            {
                //Llamar a BCAafip
                Process p = new Process();
                ProcessStartInfo psi = new ProcessStartInfo(Application.StartupPath + "\\BCAfip.exe");
                psi.Arguments = " -mod 2 -raz 2.00 -vis -2 -alt 100 -ali 0 -fno Tahoma -fsz 12 -cui " + txtCuit.Text + " -tip 03 -pto " + clsGlobales.cParametro.PtoVtaPorDefecto.ToString("00")  + " -cae " + txtCAE.Text + " -vto " + this.dtVencCAE.Value.ToString("yyyyMMdd") + " -out " + Application.StartupPath + "\\AFIP.jpg";
                p.StartInfo = psi;
                p.Start();
         
                string path = Application.StartupPath + "\\AFIP.jpg";
                oRepFacturaVta.SetParameterValue("picturePath", path);  
            } 
  
            //Llamar al reporte
            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports(2);
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();
        }

        #endregion

        #region Metodo ShowReportB

        //METODO ShowReportB, para mostrar la Factura B unicamente.
        private void ShowReportB(rptFacturaVtaB oRepFacturaVta, dsReportes oDsFactura, int p_Tipo = 0)
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
            oRepFacturaVta.DataDefinition.FormulaFields["IVA"].Text = "'" + this.txtTipoResponsable.Text.ToUpper() + "'";
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

            if (Convert.ToInt32(cboComprobante.SelectedValue) == 1 || Convert.ToInt32(cboComprobante.SelectedValue) == 3)
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
            oRepFacturaVta.DataDefinition.FormulaFields["linea-06"].Text = "' Ingresos Brutos: " + "904/30-71658372-0" + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-07"].Text = "' Fecha de Inicio Actividades : " + "01/10/2019" + "'";

            //B
            if (p_Tipo == 0)
            {
                //Llamar a BCAafip
                Process p = new Process();
                ProcessStartInfo psi = new ProcessStartInfo(Application.StartupPath + "\\BCAfip.exe");
                psi.Arguments = " -mod 2 -raz 2.00 -vis -2 -alt 100 -ali 0 -fno Tahoma -fsz 12 -cui " + txtCuit.Text + " -tip 08 -pto " + clsGlobales.cParametro.PtoVtaPorDefecto.ToString("00") + " -cae " + txtCAE.Text + " -vto " + this.dtVencCAE.Value.ToString("yyyyMMdd") + " -out " + Application.StartupPath + "\\AFIP.jpg";
                p.StartInfo = psi;
                p.Start();

                string path = Application.StartupPath + "\\AFIP.jpg";
                oRepFacturaVta.SetParameterValue("picturePath", path);
            }

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

    }
}

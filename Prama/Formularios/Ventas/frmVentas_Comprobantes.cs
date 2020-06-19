using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Diagnostics;
using Prama.Formularios.Ventas;

namespace Prama
{
    public partial class frmVentasComprobantes : Form
    {

        int indexFila = 0;
        int x_Con = 0;
        int x_Punto = 0;
        int IdFact = 0;
        bool bSearch = false;
        string bFiltro = "";
        bool yaCargado = false;

        string[] mxVector = new string[5];

        public frmVentasComprobantes(int p_Con, int p_Punto = 0, string [] mVector = null)
        {
            InitializeComponent();
            
            //VERIFICAR
            x_Con = p_Con; //Conexion Tipo 0,1
            x_Punto = p_Punto;  //Punto Opcional viene de remitos
            mxVector = mVector; //DataGridViewRow
        }

        #region Evento load

        private void frmVentasComprobantes_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 
			
            //Titulo
            this.Text = clsGlobales.cParametro.NombreFantasia + this.Text;
            
            //ToolTips
            CargarToolsTip();
            
            Cursor.Current = Cursors.WaitCursor;

            //Cargar Grilla Facturas
            CargarGrillaComprobantes("","");

            Cursor.Current = Cursors.Default;

            //Deshabilitar Oden
            this.DeshabilitarOrdenGrillas();

            //Posicionamiento
            int filas = this.dgvComprobantes.Rows.Count;
            //Count
            if (filas > 0)
            {
                // Actualizo el valor de la fila para que sea la última de la grilla
                this.indexFila = filas - 1;
                // Pongo el foco de la fila
                PosicionarFocoFila();
            }

        }

        #endregion


        #region Método que deshabilita el reordenamiento de las grilla desde sus cabeceras

        private void DeshabilitarOrdenGrillas()
        {
            // Deshabilito la reordenación de las columnas de las grillas
            foreach (DataGridViewColumn dgvCol in this.dgvComprobantes.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;

            }
        }

        #endregion

        #region Método que carga los ToolsTip del formulario

        private void CargarToolsTip()
        {
            toolTip1.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip2.SetToolTip(this.btnCancelar, "Cancelar");
            toolTip3.SetToolTip(this.btnSalir, "Salir");
            toolTip4.SetToolTip(this.btnEmitir, "Emite Nota Crédito Devolución");
            toolTip5.SetToolTip(this.btnCtaCteCli, "Ver Cta.Cte.");
            toolTip7.SetToolTip(this.btnBuscar, "Buscar");
            toolTip8.SetToolTip(this.btnImprimir, "Imprimir");
        }

        #endregion

        #region Metodo: CargarGrillaComprobantes

        private void CargarGrillaComprobantes(string Buscar = "", string Campo = "")
        {
            string myCadena = "";
            string strTabla = "";

            dgvComprobantes.DataSource = null;

            //VERIFICAR VISTA A UTILIZAR SEGUN CONEXION
            if (x_Con == 0) { strTabla = "Vista_eFactura"; } else { strTabla = "Vista_eFactura_2"; }

            //VERIFICAR PARAMETROS
            if (!(Buscar == ""))
            {
                // Cadena SQL, ver si viene con el Punto de Venta establecido o no
                if (x_Punto == 0)
                {
                    if (Campo == "Codigo")
                    {
                        myCadena = "select * from " + strTabla + " WHERE " + Campo + "=" + Buscar + " " + bFiltro + " ORDER BY IdFactura ASC";
                    }
                    else
                    {
                        myCadena = "select * from " + strTabla + " WHERE " + Campo + " like '" + Buscar + "%'" + bFiltro + " ORDER BY IdFactura ASC";
                    }                    
                    //.T.
                    bSearch = true;
                }
                else
                {
                    if (Campo == "Codigo")
                    {
                        myCadena = "select * from " + strTabla + " WHERE Punto = " + clsGlobales.cParametro.PtoVtaPorDefecto + " AND " + Campo + "=" + Buscar + " " + bFiltro + " ORDER BY IdFactura ASC";
                    }
                    else
                    {
                        myCadena = "select * from " + strTabla + " WHERE Punto = " + clsGlobales.cParametro.PtoVtaPorDefecto + " AND " + Campo + " like '" + Buscar + "%'" + bFiltro + " ORDER BY IdFactura ASC";
                    }  
                    //.T.
                    bSearch = false;
                }

            }
            else
            {
                // Cadena SQL, ver si viene con el Punto de Venta establecido o no
                if (x_Punto == 0)
                {
                    myCadena = "select * from " + strTabla + " WHERE Resultado = 1 " + bFiltro + " order by IdFactura ASC";
                }
                else
                {
                    myCadena = "select * from " + strTabla + " WHERE Punto = " + clsGlobales.cParametro.PtoVtaPorDefecto + " " + bFiltro + " order by IdFactura ASC";
                }

                //.F.
                bSearch = false;
            }
                // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            if (x_Con == 0)
            {
                dgvComprobantes.Columns[17].Visible = false;
                mDtTable = clsDataBD.GetSql(myCadena);
            }
            else
            {
                dgvComprobantes.Columns["CAE"].Visible = false;
                dgvComprobantes.Columns[17].Visible = true;
                mDtTable = clsDataBD.GetSqlB(myCadena);
            }
            // Evito que el dgv genere columnas automáticas
            dgvComprobantes.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvComprobantes.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvComprobantes.Rows.Count;
            // Posiciono la grilla en la última fila, si hay
            if (Filas > 0)
            {
                //Cambio el estado de la bandera para que ejecute el selection changed de la grilla
                yaCargado = true;

                //Posicionar Grilla       
                int r = dgvComprobantes.CurrentCell.RowIndex;
                int c = dgvComprobantes.CurrentCell.ColumnIndex;
                dgvComprobantes.CurrentCell = dgvComprobantes.Rows[r].Cells[c];


                //Posicionamiento
                dgvComprobantes.CurrentCell = dgvComprobantes[3, Filas - 1];

                //Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                EventArgs ea = new EventArgs();
                this.dgvComprobantes_SelectionChanged(this.dgvComprobantes, ea);

                if (!(bSearch))
                {
                    this.btnBuscar.Visible = true;
                    this.btnImprimir.Visible = true;
                }
            }
            else
            {
                dgvDet.DataSource = null;

                this.btnBuscar.Visible = false;
                this.btnImprimir.Visible = false;
            }
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
        
        #region Evento Grilla

        private void dgvComprobantes_SelectionChanged(object sender, EventArgs e)
        {
            if (yaCargado)
            {
                // Vacío la grilla
                dgvDet.DataSource = null;
                // Evito que el dgvUsuarios genere columnas automáticas
                dgvDet.AutoGenerateColumns = false;
                // Declaro una variable que va a guardar el Id del comprobante
                int IdComprobante = Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdFactura"].Value);
                // Armo la cadena SQL para ejecutar el procedieminto almacenado que trae el detalle del comprobante
                string newMyCadenaSql = "";
                // Creo un datatable y le paso los datos de la consulta SQL
                DataTable myTabla = null;
                //Verifico Conexion
                if (!(x_Con == 0))
                {
                    newMyCadenaSql = "Select * from Vista_eFactura_Detalle_2 Where IdFactura =  " + IdComprobante;
                    myTabla = clsDataBD.GetSqlB(newMyCadenaSql);
                }
                else
                {
                    newMyCadenaSql = "Select * from Vista_eFactura_Detalle Where IdFactura =  " + IdComprobante;
                    myTabla = clsDataBD.GetSql(newMyCadenaSql);
                }

                //Verificar si es Factura B o Nota de Credito B
                if (Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdTipoComprobante"].Value) == 6 || Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdTipoComprobante"].Value) == 8)
                {
                    dgvDet.Columns["Bonif"].Visible = false;
                    dgvDet.Columns["SubtotalDto"].Visible = false;
                    dgvDet.Columns["ValorIva"].Visible = false;
                    dgvDet.Columns["SubTotalDet"].HeaderText = "SubTotal";
                    dgvDet.Columns["Articulo"].Width = 500;
                }
                else
                {
                    dgvDet.Columns["Bonif"].Visible = true;
                    dgvDet.Columns["SubtotalDto"].Visible = true;
                    dgvDet.Columns["ValorIva"].Visible = true;
                    dgvDet.Columns["SubTotalDet"].HeaderText = "Subtotal c/Iva";
                    dgvDet.Columns["Articulo"].Width = 270;
                }

                //Controlar boton EMITIR NOTA CREDITO
                if (Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdTipoComprobante"].Value) == 3 || Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdTipoComprobante"].Value) == 8)
                {
                    this.btnEmitir.TabStop = false;
                    this.btnEmitir.Enabled = false;
                }
                else
                {
                    this.btnEmitir.TabStop = true;
                    this.btnEmitir.Enabled = true;
                }
                // Asigno a la grilla el source
                dgvDet.DataSource = myTabla;
            }

        }
  

        #endregion

        #region Eventos Botones

        private void btnBuscar_Click(object sender, EventArgs e)
        {
         //   MessageBox.Show("Funcionalidad a implementar en pròxima beta", "Informacion!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Get Index
            this.indexFila = dgvComprobantes.CurrentRow.Index;
            //Set
            this.grpSearch.Visible = true;
            this.btnAceptar.Visible = true;
            this.btnCancelar.Visible = true;
            this.btnBuscar.Visible = false;
            this.btnImprimir.Visible = false;
            this.btnCtaCteCli.Visible = false;
            this.btnSalir.Visible = false;
            this.btnEmitir.Visible = false;
            this.gpbDetalle.Size = new System.Drawing.Size(993, 170);
            this.dgvDet.Size = new System.Drawing.Size(977, 133);
            //Clean
            this.txtComprobante.Text = "";
            this.txtCodigo.Text = "";
            this.txtRs.Text = "";
            //Pos
            txtCodigo.Focus();

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

            string sTipoComprobante = "";
            int nTipoComp = Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdTipoComprobante"].Value);

            //Factura o Remito?....
            switch(Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdTipoComprobante"].Value))
            {
                case 1:
                case 6:
                      sTipoComprobante = "Factura N°";
                      break;
                case 3:
                case 8:
                      sTipoComprobante = "Nota de Crédito N° ";
                    break;
            }
            //PREGUNTAR SI ESTA CONFIGURADO EN PARAMETROS
            if (clsGlobales.cParametro.Imprimir)
            {
                DialogResult dlResult = MessageBox.Show("¿Desea imprimir la " + sTipoComprobante + dgvComprobantes.CurrentRow.Cells["nComprobante"].Value.ToString() 
                                + " ?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Si confirma... cambiar estado
                if (dlResult == DialogResult.No)
                {
                    return;
                }
            }

            // capturo la posición de la fila
            this.indexFila = this.dgvDet.CurrentRow.Index;

            int dgvFilas = 0;

            //Data Set
            dsReportes oDsFactura = new dsReportes();

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


            //FACTURA A
            if (nTipoComp == 1 || nTipoComp == 3)
            {
                if (!(clsGlobales.ConB == null))
                {
                    rptFacturaVta_1 oRepFacturaVta = new rptFacturaVta_1();

                    //Cargar Reporte                                    
                    oRepFacturaVta.Load(Application.StartupPath + "\\rptFacturaVta-1.rpt");

                    //Tipo Comprobante
                    oRepFacturaVta.DataDefinition.FormulaFields["TipoComprobante"].Text = "'" + "X" + "'";
                    oRepFacturaVta.DataDefinition.FormulaFields["CodigoComp"].Text = "'" + " " + "'";

                    //MOSTRAR REPORTE FACTURA A
                    ShowReportFA1(oRepFacturaVta, oDsFactura);
                }
                else
                {
                    //Objeto Reporte
                    rptFacturaVta oRepFacturaVta = new rptFacturaVta();

                    //Cargar Reporte                                    
                    oRepFacturaVta.Load(Application.StartupPath + "\\rptFacturaVta.rpt");

                    //Tipo Comprobante
                    oRepFacturaVta.DataDefinition.FormulaFields["TipoComprobante"].Text = "'" + "A" + "'";
                    if (nTipoComp == 1)
                    {
                        oRepFacturaVta.DataDefinition.FormulaFields["CodigoComp"].Text = "'" + "COD. 01" + "'";
                    }
                    else
                    {
                        oRepFacturaVta.DataDefinition.FormulaFields["CodigoComp"].Text = "'" + "COD. 03" + "'";
                    }
                    //MOSTRAR REPORTE FACTURA A
                    ShowReportFA(oRepFacturaVta, oDsFactura,nTipoComp);
                }

            } //FACTURA B
            else if (nTipoComp == 6 || nTipoComp == 8)
            {
                //IMPRIME PRESUPUESTO
                if (!(clsGlobales.ConB == null))
                {
                    //Objeto Reporte
                    rptFacturaVtaB_1 oRepFacturaVta = new rptFacturaVtaB_1();

                    //Cargar Reporte                                    
                    oRepFacturaVta.Load(Application.StartupPath + "\\rptFacturaVtaB-1.rpt");

                    oRepFacturaVta.DataDefinition.FormulaFields["TipoComprobante"].Text = "'" + "X" + "'";
                    oRepFacturaVta.DataDefinition.FormulaFields["CodigoComp"].Text = "'" + " " + "'";

                    //MOSTRAR REPORTE FACTURA B
                    ShowReportFB1(oRepFacturaVta, oDsFactura);
                }
                else
                {
                    //Objeto Reporte
                    rptFacturaVtaB oRepFacturaVta = new rptFacturaVtaB();

                    //Cargar Reporte                                    
                    oRepFacturaVta.Load(Application.StartupPath + "\\rptFacturaVtaB.rpt");

                    oRepFacturaVta.DataDefinition.FormulaFields["TipoComprobante"].Text = "'" + "B" + "'";
                    if (nTipoComp == 6)
                    {
                        oRepFacturaVta.DataDefinition.FormulaFields["CodigoComp"].Text = "'" + "COD. 06" + "'";
                    }
                    else
                    {
                        oRepFacturaVta.DataDefinition.FormulaFields["CodigoComp"].Text = "'" + "COD. 08" + "'";
                    }
                    

                    //MOSTRAR REPORTE FACTURA B
                    ShowReportFB(oRepFacturaVta, oDsFactura, nTipoComp);
                }
            }

            //ELIMINAR ARCHIVO
            BorrarArchivo(Application.StartupPath + "\\AFIP.jpg");
        }
        
        private void btnSalir_Click(object sender, EventArgs e)
        {

            //Retornar Grilla DataGridView
            if (dgvComprobantes.Rows.Count > 0 && x_Punto!=0)
            {
                this.mxVector[0] = Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["Nro"].Value).ToString("D8");
                this.mxVector[1] = dgvComprobantes.CurrentRow.Cells["IdFormaPago"].Value.ToString();
                this.mxVector[2] = dgvComprobantes.CurrentRow.Cells["IdCliente"].Value.ToString();
                this.mxVector[3] = Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdTransporte"].Value).ToString();
                this.mxVector[4] = Convert.ToDouble(dgvComprobantes.CurrentRow.Cells["TotalFactura"].Value).ToString("#0.00");
            }
            
            
            //ELIMINAR ARCHIVO
            BorrarArchivo(Application.StartupPath +  "\\AFIP.jpg");

            //Cerrar
            this.Close();
        }

        #endregion

        #region Método que devuelve el foco a la fila desde donde se presionó algún botón

        private void PosicionarFocoFila()
        {
            if (dgvComprobantes.Rows.Count != 0 && dgvComprobantes.Rows.Count > this.indexFila)
            {
                // Devuelvo el foco a la fila de la grilla desde donde se llamó
                if (this.indexFila == -1)
                {
                    this.indexFila = 0;
                }
                this.dgvComprobantes.CurrentCell = dgvComprobantes[1, this.indexFila];
                // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                EventArgs ea = new EventArgs();
                this.dgvComprobantes_SelectionChanged(this.dgvComprobantes, ea);
            }
        }

        #endregion

        #region Metodo ShowReportFA

        //METODO ShowReportFA ( Para Factura A )
        private void ShowReportFA(rptFacturaVta oRepFacturaVta, dsReportes oDsFactura, int p_Tipo)
        { 
            
            //Establecer el DataSet como DataSource
            oRepFacturaVta.SetDataSource(oDsFactura);
            
            //PASAR REPORTE COMO PARAMETRO
            clsGlobales.myRptDoc = oRepFacturaVta;

            //PUNTO DE VENTA Y NRO COMPROBANTE
            oRepFacturaVta.DataDefinition.FormulaFields["NroComp"].Text = "'" + dgvComprobantes.CurrentRow.Cells["nComprobante"].Value.ToString() + "'";

            //DATOS GENERALES
            oRepFacturaVta.DataDefinition.FormulaFields["Fecha"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Fecha"].Value.ToString() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["RazonSocial"].Text = "'" + dgvComprobantes.CurrentRow.Cells["RazonSocial"].Value.ToString().ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["Domic"].Text = "'" + (dgvComprobantes.CurrentRow.Cells["Direccion"].Value.ToString() + ", " + dgvComprobantes.CurrentRow.Cells["Localidad"].Value.ToString().ToUpper() + ", " + dgvComprobantes.CurrentRow.Cells["Provincia"].Value.ToString().ToUpper() +  " (" + dgvComprobantes.CurrentRow.Cells["CP"].Value.ToString()).ToUpper() + ")'";
            oRepFacturaVta.DataDefinition.FormulaFields["IVA"].Text = "'" + dgvComprobantes.CurrentRow.Cells["TipoResponsable"].Value.ToString().ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["CondicionVenta"].Text = "'" + dgvComprobantes.CurrentRow.Cells["FormaPago"].Value.ToString().ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["CUIT"].Text = "'" + dgvComprobantes.CurrentRow.Cells["CUIT"].Value.ToString() + "'";
            
            //CAE Y VENCIMINETO CAE
            oRepFacturaVta.DataDefinition.FormulaFields["CAE"].Text = "'" + dgvComprobantes.CurrentRow.Cells["CAE"].Value.ToString() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["VencCAE"].Text = "'" + dgvComprobantes.CurrentRow.Cells["VencCAE"].Value.ToString() + "'";

            //NETO
            oRepFacturaVta.DataDefinition.FormulaFields["Neto"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Neto"].Value.ToString() + "'";
           
            //DTO
            oRepFacturaVta.DataDefinition.FormulaFields["Dto"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Dto"].Value.ToString() + "'";
            double DtoImpo = (Convert.ToDouble(dgvComprobantes.CurrentRow.Cells["Neto"].Value) * Convert.ToDouble(dgvComprobantes.CurrentRow.Cells["Dto"].Value)) / 100;
            oRepFacturaVta.DataDefinition.FormulaFields["DtoImpo"].Text = "'" + DtoImpo.ToString("#0.00") + "'";

            //FLETE
            oRepFacturaVta.DataDefinition.FormulaFields["Flete"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Flete"].Value.ToString() + "'";

            //SUBTOTAL
            oRepFacturaVta.DataDefinition.FormulaFields["Subtotal"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Subtotalef"].Value.ToString() + "'";

            //EXENTO
            oRepFacturaVta.DataDefinition.FormulaFields["Exento"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Exento"].Value.ToString() + "'";
            
            //IVA --
            if (Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdTipoComprobante"].Value) == 1 || Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdTipoComprobante"].Value) == 3)
            {
              oRepFacturaVta.DataDefinition.FormulaFields["TotIVA"].Text = "'" + dgvComprobantes.CurrentRow.Cells["fIVA"].Value.ToString() + "'";
              oRepFacturaVta.DataDefinition.FormulaFields["IVA10"].Text = "'" + dgvComprobantes.CurrentRow.Cells["IVA10"].Value.ToString() + "'";
            }
            //------

            //TOTAL FACTURA
            oRepFacturaVta.DataDefinition.FormulaFields["Total"].Text = "'" + dgvComprobantes.CurrentRow.Cells["TotalFactura"].Value.ToString() + "'";

            //DATOS DE LA CABECERA
            oRepFacturaVta.DataDefinition.FormulaFields["linea-01"].Text = "' Razón Social: " + clsGlobales.cParametro.RazonSocial + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-02"].Text = "' Domicilio:'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-03"].Text = "'" + clsGlobales.cParametro.Direccion + "-" + clsGlobales.cParametro.Localidad + ", Córdoba" + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-04"].Text = "' Condición frente al Iva : " + clsGlobales.cParametro.CondicionIva + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-06"].Text = "' Ingresos Brutos: " + "904/30-71658372-0" + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-07"].Text = "' Fecha de Inicio Actividades : " + "01/10/2019" + "'";

            ////LLAMAR A COMPONENTE BCAFIP PARA OBTENER CODIGO DE BARRA ( PASAR PARAMETROS )
            //Process p = new Process();
            //ProcessStartInfo psi = new ProcessStartInfo(Application.StartupPath + "\\BCAfip.exe");
            //psi.Arguments = " -mod 2 -raz 2.00 -vis -2 -alt 100 -ali 0 -fno Tahoma -fsz 12 -cui " + dgvComprobantes.CurrentRow.Cells["CUIT"].Value.ToString() + " -tip 01 -pto " + dgvComprobantes.CurrentRow.Cells["Punto"].Value.ToString() + " -cae 01234567890123 -vto " + Convert.ToDateTime(dgvComprobantes.CurrentRow.Cells["VecCAE"].Value).ToString("yyyyMMdd") + " -out " + Application.StartupPath + "\\AFIP.jpg";
            //p.StartInfo = psi;
            //p.Start();

            //LLAMAR A COMPONENTE BCAFIP PARA OBTENER CODIGO DE BARRA ( PASAR PARAMETROS )
            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo(Application.StartupPath + "\\BCAfip.exe");
            psi.Arguments = " -mod 2 -raz 2.00 -vis -2 -alt 100 -ali 0 -fno Tahoma -fsz 12 -cui " + dgvComprobantes.CurrentRow.Cells["CUIT"].Value.ToString() + " -tip " + p_Tipo.ToString("00") + " -pto " + Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["Punto"].Value).ToString("00") + " -cae " + dgvComprobantes.CurrentRow.Cells["CAE"].Value.ToString() + " -vto " + Convert.ToDateTime(dgvComprobantes.CurrentRow.Cells["VecCAE"].Value).ToString("yyyyMMdd") + " -out " + Application.StartupPath + "\\AFIP.jpg";
            p.StartInfo = psi;
            p.Start();

            string path = Application.StartupPath + "\\AFIP.jpg";
            oRepFacturaVta.SetParameterValue("picturePath", path);

            //MOSTRAR REPORTE  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();
        }
        
        #endregion

        #region Metodo ShowReportFB

        //METODO ShowReportFA ( Para Factura A )
        private void ShowReportFB(rptFacturaVtaB oRepFacturaVta, dsReportes oDsFactura, int p_Tipo)
        {

            //Establecer el DataSet como DataSource
            oRepFacturaVta.SetDataSource(oDsFactura);

            //PASAR REPORTE COMO PARAMETRO
            clsGlobales.myRptDoc = oRepFacturaVta;

            //PUNTO DE VENTA Y NRO COMPROBANTE
            oRepFacturaVta.DataDefinition.FormulaFields["NroComp"].Text = "'" + dgvComprobantes.CurrentRow.Cells["nComprobante"].Value.ToString() + "'";

            //DATOS GENERALES
            oRepFacturaVta.DataDefinition.FormulaFields["Fecha"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Fecha"].Value.ToString() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["RazonSocial"].Text = "'" + dgvComprobantes.CurrentRow.Cells["RazonSocial"].Value.ToString().ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["Domic"].Text = "'" + (dgvComprobantes.CurrentRow.Cells["Direccion"].Value.ToString() + ", " + dgvComprobantes.CurrentRow.Cells["Localidad"].Value.ToString().ToUpper() + ", " + dgvComprobantes.CurrentRow.Cells["Provincia"].Value.ToString().ToUpper() + " (" + dgvComprobantes.CurrentRow.Cells["CP"].Value.ToString()).ToUpper() + ")'";
            oRepFacturaVta.DataDefinition.FormulaFields["IVA"].Text = "'" + dgvComprobantes.CurrentRow.Cells["TipoResponsable"].Value.ToString().ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["CondicionVenta"].Text = "'" + dgvComprobantes.CurrentRow.Cells["FormaPago"].Value.ToString().ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["CUIT"].Text = "'" + dgvComprobantes.CurrentRow.Cells["CUIT"].Value.ToString() + "'";

            //CAE Y VENCIMINETO CAE
            oRepFacturaVta.DataDefinition.FormulaFields["CAE"].Text = "'" + dgvComprobantes.CurrentRow.Cells["CAE"].Value.ToString() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["VencCAE"].Text = "'" + dgvComprobantes.CurrentRow.Cells["VencCAE"].Value.ToString() + "'";

            //NETO
            oRepFacturaVta.DataDefinition.FormulaFields["Neto"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Neto"].Value.ToString() + "'";

            //DTO
            oRepFacturaVta.DataDefinition.FormulaFields["Dto"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Dto"].Value.ToString() + "'";
            double DtoImpo = (Convert.ToDouble(dgvComprobantes.CurrentRow.Cells["Neto"].Value) * Convert.ToDouble(dgvComprobantes.CurrentRow.Cells["Dto"].Value)) / 100;
            oRepFacturaVta.DataDefinition.FormulaFields["DtoImpo"].Text = "'" + DtoImpo.ToString("#0.00") + "'";

            //FLETE
            oRepFacturaVta.DataDefinition.FormulaFields["Flete"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Flete"].Value.ToString() + "'";

            //SUBTOTAL
            oRepFacturaVta.DataDefinition.FormulaFields["Subtotal"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Subtotalef"].Value.ToString() + "'";

            //EXENTO
            oRepFacturaVta.DataDefinition.FormulaFields["Exento"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Exento"].Value.ToString() + "'";

            //IVA --
            if (Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdTipoComprobante"].Value) == 1 || Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdTipoComprobante"].Value) == 3)
            {
                oRepFacturaVta.DataDefinition.FormulaFields["TotIVA"].Text = "'" + dgvComprobantes.CurrentRow.Cells["fIVA"].Value.ToString() + "'";
                oRepFacturaVta.DataDefinition.FormulaFields["IVA10"].Text = "'" + dgvComprobantes.CurrentRow.Cells["IVA10"].Value.ToString() + "'";
            }
            //------

            //TOTAL FACTURA
            oRepFacturaVta.DataDefinition.FormulaFields["Total"].Text = "'" + dgvComprobantes.CurrentRow.Cells["TotalFactura"].Value.ToString() + "'";

            //DATOS DE LA CABECERA
            oRepFacturaVta.DataDefinition.FormulaFields["linea-01"].Text = "' Razón Social: " + clsGlobales.cParametro.RazonSocial + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-02"].Text = "' Domicilio:'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-03"].Text = "'" + clsGlobales.cParametro.Direccion + "-" + clsGlobales.cParametro.Localidad + ", Córdoba" + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-04"].Text = "' Condición frente al Iva : " + clsGlobales.cParametro.CondicionIva + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-06"].Text = "' Ingresos Brutos: " + "904/30-71658372-0" + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-07"].Text = "' Fecha de Inicio Actividades : " + "01/10/2019" + "'";

            //LLAMAR A COMPONENTE BCAFIP PARA OBTENER CODIGO DE BARRA ( PASAR PARAMETROS )
            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo(Application.StartupPath + "\\BCAfip.exe");
          //psi.Arguments = " -mod 2 -raz 2.00 -vis -2 -alt 100 -ali 0 -fno Tahoma -fsz 12 -cui " + dgvComprobantes.CurrentRow.Cells["CUIT"].Value.ToString() + " -tip " + p_Tipo.ToString("00") + " -pto " + Convert.ToDouble(dgvComprobantes.CurrentRow.Cells["Punto"].Value).ToString("00") + " -cae 01234567890123 -vto " + Convert.ToDateTime(dgvComprobantes.CurrentRow.Cells["VecCAE"].Value).ToString("yyyyMMdd") + " -out " + Application.StartupPath + "\\AFIP.jpg";
            psi.Arguments = " -mod 2 -raz 2.00 -vis -2 -alt 100 -ali 0 -fno Tahoma -fsz 12 -cui " + dgvComprobantes.CurrentRow.Cells["CUIT"].Value.ToString() + " -tip " + p_Tipo.ToString("00") + " -pto " + Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["Punto"].Value).ToString("00") + " -cae " + dgvComprobantes.CurrentRow.Cells["CAE"].Value.ToString() + " -vto " + Convert.ToDateTime(dgvComprobantes.CurrentRow.Cells["VecCAE"].Value).ToString("yyyyMMdd") + " -out " + Application.StartupPath + "\\AFIP.jpg";

            p.StartInfo = psi;
            p.Start();

            string path =  Application.StartupPath + "\\AFIP.jpg";
            oRepFacturaVta.SetParameterValue("picturePath", path);

            //MOSTRAR REPORTE  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();
        }

        #endregion
                
        #region Reposicionar Grilla por Id

        //REPOSICIONA GRILLA POR ID
        private void ReposicionarById()
        {
            //recorrer
            foreach (DataGridViewRow myRow in this.dgvComprobantes.Rows)
            {
                if (Convert.ToInt32(myRow.Cells["IdFactura"].Value.ToString()) == IdFact)
                {
                    // Devuelvo el foco a la fila de la grilla desde donde se llamó
                    this.dgvComprobantes.CurrentCell = dgvComprobantes[1, myRow.Index];

                    // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                    EventArgs ea = new EventArgs();
                    this.dgvComprobantes_SelectionChanged(this.dgvComprobantes, ea);

                    //Salir
                    break;
                }
            }
        }

        #endregion

        #region Metodo ShowReportFA1

        //METODO ShowReportFA ( Para Factura A )
        private void ShowReportFA1(rptFacturaVta_1 oRepFacturaVta, dsReportes oDsFactura)
        {

            //Establecer el DataSet como DataSource
            oRepFacturaVta.SetDataSource(oDsFactura);

            //PASAR REPORTE COMO PARAMETRO
            clsGlobales.myRptDoc = oRepFacturaVta;

            //PUNTO DE VENTA Y NRO COMPROBANTE
            oRepFacturaVta.DataDefinition.FormulaFields["NroComp"].Text = "'" + dgvComprobantes.CurrentRow.Cells["nComprobante"].Value.ToString() + "'";

            //DATOS GENERALES
            oRepFacturaVta.DataDefinition.FormulaFields["Fecha"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Fecha"].Value.ToString() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["RazonSocial"].Text = "'" + dgvComprobantes.CurrentRow.Cells["RazonSocial"].Value.ToString().ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["Domic"].Text = "'" + (dgvComprobantes.CurrentRow.Cells["Direccion"].Value.ToString() + ", " + dgvComprobantes.CurrentRow.Cells["Localidad"].Value.ToString().ToUpper() + ", " + dgvComprobantes.CurrentRow.Cells["Provincia"].Value.ToString().ToUpper() + " (" + dgvComprobantes.CurrentRow.Cells["CP"].Value.ToString()).ToUpper() + ")'";
            oRepFacturaVta.DataDefinition.FormulaFields["IVA"].Text = "'" + dgvComprobantes.CurrentRow.Cells["TipoResponsable"].Value.ToString().ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["CondicionVenta"].Text = "'" + dgvComprobantes.CurrentRow.Cells["FormaPago"].Value.ToString().ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["CUIT"].Text = "'" + dgvComprobantes.CurrentRow.Cells["CUIT"].Value.ToString() + "'";

            //CAE Y VENCIMINETO CAE
            oRepFacturaVta.DataDefinition.FormulaFields["CAE"].Text = "'" + dgvComprobantes.CurrentRow.Cells["CAE"].Value.ToString() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["VencCAE"].Text = "'" + dgvComprobantes.CurrentRow.Cells["VencCAE"].Value.ToString() + "'";

            //NETO
            oRepFacturaVta.DataDefinition.FormulaFields["Neto"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Neto"].Value.ToString() + "'";

            //DTO
            oRepFacturaVta.DataDefinition.FormulaFields["Dto"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Dto"].Value.ToString() + "'";
            double DtoImpo = (Convert.ToDouble(dgvComprobantes.CurrentRow.Cells["Neto"].Value) * Convert.ToDouble(dgvComprobantes.CurrentRow.Cells["Dto"].Value)) / 100;
            oRepFacturaVta.DataDefinition.FormulaFields["DtoImpo"].Text = "'" + DtoImpo.ToString("#0.00") + "'";

            //FLETE
            oRepFacturaVta.DataDefinition.FormulaFields["Flete"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Flete"].Value.ToString() + "'";

            //SUBTOTAL
            oRepFacturaVta.DataDefinition.FormulaFields["Subtotal"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Subtotalef"].Value.ToString() + "'";

            //EXENTO
            oRepFacturaVta.DataDefinition.FormulaFields["Exento"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Exento"].Value.ToString() + "'";

            //IVA --
            if (Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdTipoComprobante"].Value) == 1 || Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdTipoComprobante"].Value) == 3)
            {
                oRepFacturaVta.DataDefinition.FormulaFields["TotIVA"].Text = "'" + dgvComprobantes.CurrentRow.Cells["fIVA"].Value.ToString() + "'";
                oRepFacturaVta.DataDefinition.FormulaFields["IVA10"].Text = "'" + dgvComprobantes.CurrentRow.Cells["IVA10"].Value.ToString() + "'";
            }
            //------

            //TOTAL FACTURA
            oRepFacturaVta.DataDefinition.FormulaFields["Total"].Text = "'" + dgvComprobantes.CurrentRow.Cells["TotalFactura"].Value.ToString() + "'";

            //DATOS DE LA CABECERA
            oRepFacturaVta.DataDefinition.FormulaFields["linea-01"].Text = "' Razón Social: " + clsGlobales.cParametro.RazonSocial + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-02"].Text = "' Domicilio:'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-03"].Text = "'" + clsGlobales.cParametro.Direccion + "-" + clsGlobales.cParametro.Localidad + ", Córdoba" + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-04"].Text = "' Condición frente al Iva : " + clsGlobales.cParametro.CondicionIva + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-06"].Text = "' Ingresos Brutos: " + "904/30-71658372-0" + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-07"].Text = "' Fecha de Inicio Actividades : " + "01/10/2019" + "'";

            //LLAMAR A COMPONENTE BCAFIP PARA OBTENER CODIGO DE BARRA ( PASAR PARAMETROS )
        /*  Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo(Application.StartupPath + "\\BCAfip.exe");
            psi.Arguments = " -mod 2 -raz 2.00 -vis -2 -alt 100 -ali 0 -fno Tahoma -fsz 12 -cui " + dgvComprobantes.CurrentRow.Cells["CUIT"].Value.ToString() + " -tip 01 -pto " + dgvComprobantes.CurrentRow.Cells["Punto"].Value.ToString() + " -cae 01234567890123 -vto " + Convert.ToDateTime(dgvComprobantes.CurrentRow.Cells["VecCAE"].Value).ToString("yyyyMMdd") + " -out " + Application.StartupPath + "\\AFIP.jpg";
            p.StartInfo = psi;
            p.Start();

            string path = Application.StartupPath + "\\AFIP.jpg";
            oRepFacturaVta.SetParameterValue("picturePath", path);*/

            //MOSTRAR REPORTE  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();
        }

        #endregion

        #region Metodo ShowReportFB1

        //METODO ShowReportFA ( Para Factura A )
        private void ShowReportFB1(rptFacturaVtaB_1 oRepFacturaVta, dsReportes oDsFactura)
        {

            //Establecer el DataSet como DataSource
            oRepFacturaVta.SetDataSource(oDsFactura);

            //PASAR REPORTE COMO PARAMETRO
            clsGlobales.myRptDoc = oRepFacturaVta;

            //PUNTO DE VENTA Y NRO COMPROBANTE
            oRepFacturaVta.DataDefinition.FormulaFields["NroComp"].Text = "'" + dgvComprobantes.CurrentRow.Cells["nComprobante"].Value.ToString() + "'";

            //DATOS GENERALES
            oRepFacturaVta.DataDefinition.FormulaFields["Fecha"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Fecha"].Value.ToString() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["RazonSocial"].Text = "'" + dgvComprobantes.CurrentRow.Cells["RazonSocial"].Value.ToString().ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["Domic"].Text = "'" + (dgvComprobantes.CurrentRow.Cells["Direccion"].Value.ToString() + ", " + dgvComprobantes.CurrentRow.Cells["Localidad"].Value.ToString().ToUpper() + ", " + dgvComprobantes.CurrentRow.Cells["Provincia"].Value.ToString().ToUpper() + " (" + dgvComprobantes.CurrentRow.Cells["CP"].Value.ToString()).ToUpper() + ")'";
            oRepFacturaVta.DataDefinition.FormulaFields["IVA"].Text = "'" + dgvComprobantes.CurrentRow.Cells["TipoResponsable"].Value.ToString().ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["CondicionVenta"].Text = "'" + dgvComprobantes.CurrentRow.Cells["FormaPago"].Value.ToString().ToUpper() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["CUIT"].Text = "'" + dgvComprobantes.CurrentRow.Cells["CUIT"].Value.ToString() + "'";

            //CAE Y VENCIMINETO CAE
            oRepFacturaVta.DataDefinition.FormulaFields["CAE"].Text = "'" + dgvComprobantes.CurrentRow.Cells["CAE"].Value.ToString() + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["VencCAE"].Text = "'" + dgvComprobantes.CurrentRow.Cells["VencCAE"].Value.ToString() + "'";

            //NETO
            oRepFacturaVta.DataDefinition.FormulaFields["Neto"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Neto"].Value.ToString() + "'";

            //DTO
            oRepFacturaVta.DataDefinition.FormulaFields["Dto"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Dto"].Value.ToString() + "'";
            double DtoImpo = (Convert.ToDouble(dgvComprobantes.CurrentRow.Cells["Neto"].Value) * Convert.ToDouble(dgvComprobantes.CurrentRow.Cells["Dto"].Value)) / 100;
            oRepFacturaVta.DataDefinition.FormulaFields["DtoImpo"].Text = "'" + DtoImpo.ToString("#0.00") + "'";

            //FLETE
            oRepFacturaVta.DataDefinition.FormulaFields["Flete"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Flete"].Value.ToString() + "'";

            //SUBTOTAL
            oRepFacturaVta.DataDefinition.FormulaFields["Subtotal"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Subtotalef"].Value.ToString() + "'";

            //EXENTO
            oRepFacturaVta.DataDefinition.FormulaFields["Exento"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Exento"].Value.ToString() + "'";

            //IVA --
            if (Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdTipoComprobante"].Value) == 1 || Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdTipoComprobante"].Value) == 3)
            {
                oRepFacturaVta.DataDefinition.FormulaFields["TotIVA"].Text = "'" + dgvComprobantes.CurrentRow.Cells["fIVA"].Value.ToString() + "'";
                oRepFacturaVta.DataDefinition.FormulaFields["IVA10"].Text = "'" + dgvComprobantes.CurrentRow.Cells["IVA10"].Value.ToString() + "'";
            }
            //------

            //TOTAL FACTURA
            oRepFacturaVta.DataDefinition.FormulaFields["Total"].Text = "'" + dgvComprobantes.CurrentRow.Cells["TotalFactura"].Value.ToString() + "'";

            //DATOS DE LA CABECERA
            oRepFacturaVta.DataDefinition.FormulaFields["linea-01"].Text = "' Razón Social: " + clsGlobales.cParametro.RazonSocial + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-02"].Text = "' Domicilio:'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-03"].Text = "'" + clsGlobales.cParametro.Direccion + "-" + clsGlobales.cParametro.Localidad + ", Córdoba" + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-04"].Text = "' Condición frente al Iva : " + clsGlobales.cParametro.CondicionIva + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-06"].Text = "' Ingresos Brutos: " + "904/30-71658372-0" + "'";
            oRepFacturaVta.DataDefinition.FormulaFields["linea-07"].Text = "' Fecha de Inicio Actividades : " + "01/10/2019" + "'";

            //LLAMAR A COMPONENTE BCAFIP PARA OBTENER CODIGO DE BARRA ( PASAR PARAMETROS )
     /*       Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo(Application.StartupPath + "\\BCAfip.exe");
            psi.Arguments = " -mod 2 -raz 2.00 -vis -2 -alt 100 -ali 0 -fno Tahoma -fsz 12 -cui " + dgvComprobantes.CurrentRow.Cells["CUIT"].Value.ToString() + " -tip 01 -pto " + dgvComprobantes.CurrentRow.Cells["Punto"].Value.ToString() + " -cae 01234567890123 -vto " + Convert.ToDateTime(dgvComprobantes.CurrentRow.Cells["VecCAE"].Value).ToString("yyyyMMdd") + " -out " + Application.StartupPath + "\\AFIP.jpg";
            p.StartInfo = psi;
            p.Start();

            string path = Application.StartupPath + "\\AFIP.jpg";
            oRepFacturaVta.SetParameterValue("picturePath", path);*/

            //MOSTRAR REPORTE  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();
        }

        #endregion

        #region Eventos Botones ( Click, TextChanged )

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Tomar el Id
            IdFact = Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdFactura"].Value.ToString());
            // Lleno nuevamente la grilla
            this.CargarGrillaComprobantes();
            //Botones
            this.grpSearch.Visible = false;
            this.btnAceptar.Visible = false;
            this.btnCancelar.Visible = false;
            this.btnBuscar.Visible = true;
            this.btnImprimir.Visible = true;
            this.btnCtaCteCli.Visible = true;
            this.btnSalir.Visible = true;
            this.btnEmitir.Visible = true;
            this.gpbDetalle.Size = new System.Drawing.Size(993, 218);
            this.dgvDet.Size = new System.Drawing.Size(977, 184);
            //IdFact >0? Solo cuando busca reposiciona por ID
            if (!(IdFact == 0 && bSearch))
            {
                //Reposicionar
                ReposicionarById();
                //Reset
                IdFact = 0;
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Recargar
            if (bSearch)
            {
                //Cargo las localidades
                this.CargarGrillaComprobantes();
                //Botones
                this.grpSearch.Visible = false;
                this.btnAceptar.Visible = false;
                this.btnCancelar.Visible = false;;
                this.btnBuscar.Visible = true;
                this.btnImprimir.Visible = true;
                this.btnCtaCteCli.Visible = true;
                this.btnSalir.Visible = true;
                this.btnEmitir.Visible = true;
                this.gpbDetalle.Size = new System.Drawing.Size(993, 233);
                this.dgvDet.Size = new System.Drawing.Size(977, 184);
                //Foco
                PosicionarFocoFila();
            }
            else
            {
                //Botones
                this.grpSearch.Visible = false;
                this.btnAceptar.Visible = false;
                this.btnCancelar.Visible = false;
                this.btnBuscar.Visible = true;
                this.btnImprimir.Visible = true;
                this.btnSalir.Visible = true;
                this.btnEmitir.Visible = true;
                this.btnCtaCteCli.Visible = true;
                this.gpbDetalle.Size = new System.Drawing.Size(993, 233);
                this.dgvDet.Size = new System.Drawing.Size(977, 184);
                //Foco
                PosicionarFocoFila();
            }

            //.F.
            bSearch = false;
        }

        private void txtComprobante_TextChanged(object sender, EventArgs e)
        {
            if (!(txtComprobante.Text == ""))
            {
                this.CargarGrillaComprobantes(this.txtComprobante.Text, "PuntoNro");
            }
        }

        private void txtRs_TextChanged(object sender, EventArgs e)
        {
            if (!(txtRs.Text == ""))
            {
                this.CargarGrillaComprobantes(this.txtRs.Text, "RazonSocial");
            }
        }

        #endregion

        private void rdbT_CheckedChanged(object sender, EventArgs e)
        {
            this.bFiltro = "";
            this.CargarGrillaComprobantes("", "");
        }

        private void rdbA_CheckedChanged(object sender, EventArgs e)
        {
            this.bFiltro = "AND IdTipoComprobante=1";
            this.CargarGrillaComprobantes("", "");

        }

        private void rdbB_CheckedChanged(object sender, EventArgs e)
        {
            this.bFiltro = "AND IdTipoComprobante=6";
            this.CargarGrillaComprobantes("", "");

        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            if (!(txtCodigo.Text == ""))
            {
                this.CargarGrillaComprobantes(this.txtCodigo.Text, "IdCliente");
            }
        }

        private void txtComprobante_Click(object sender, EventArgs e)
        {
            this.txtComprobante.Text = "";
            this.txtCodigo.Text = "";
            this.txtRs.Text = "";

        }

        private void txtCodigo_Click(object sender, EventArgs e)
        {
            this.txtComprobante.Text = "";
            this.txtCodigo.Text = "";
            this.txtRs.Text = "";
        }

        private void txtRs_Click(object sender, EventArgs e)
        {
            this.txtComprobante.Text = "";
            this.txtCodigo.Text = "";
            this.txtRs.Text = "";
        }

        private void btnMovimientos_Click(object sender, EventArgs e)
        {
            frmClientesCC myForm = new frmClientesCC(Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdCliente"].Value));
            myForm.ShowDialog();
        }

        private void txtCodigoCorreo_TextChanged(object sender, EventArgs e)
        {
            if (!(txtCodigoCorreo.Text == ""))
            {
                this.CargarGrillaComprobantes(this.txtCodigoCorreo.Text, "Codigo_Correo");
            }
        }

        private void btnEmitir_Click(object sender, EventArgs e)
        {
            //ID GRILLA
            if (dgvComprobantes.Rows.Count > 0)
            {
                //Tomar el Id
                IdFact = Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdFactura"].Value.ToString());
            }
            
            frmNotaCredito myNotaC = new frmNotaCredito(this.dgvComprobantes, this.dgvDet, 
                Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdTransporte"].Value), 
                Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdCliente"].Value),
                Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdFormaPago"].Value));
            myNotaC.ShowDialog();

            Cursor.Current = Cursors.WaitCursor;

            //Cargar Grilla Facturas
            CargarGrillaComprobantes("", "");

            Cursor.Current = Cursors.Default;

            if (IdFact != 0)
            {
                ReposicionarById();
            }
            else
            {
                //Posicionamiento
                int filas = this.dgvComprobantes.Rows.Count;
                //Count
                if (filas > 0)
                {
                    // Actualizo el valor de la fila para que sea la última de la grilla
                    this.indexFila = filas - 1;
                    // Pongo el foco de la fila
                    PosicionarFocoFila();
                }
            }
        }

        private void rbNcA_CheckedChanged(object sender, EventArgs e)
        {
            this.bFiltro = "AND IdTipoComprobante=3";
            this.CargarGrillaComprobantes("", "");
        }

        private void rbNcB_CheckedChanged(object sender, EventArgs e)
        {
            this.bFiltro = "AND IdTipoComprobante=8";
            this.CargarGrillaComprobantes("", "");
        }

    }
 }


        

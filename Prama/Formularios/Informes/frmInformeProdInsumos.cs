using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prama.Formularios.Informes
{
    public partial class frmInformeProdInsumos : Form
    {
        public frmInformeProdInsumos()
        {
            InitializeComponent();
        }

        #region Metodo Load

        private void frmInformeProdInsumos_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 
			
            //Al iniciar formulario, grilla productos visible
            this.dtProductos.TabStop = true;
            this.dtProductos.Visible = true;
            this.dtInsumos.TabStop = false;
            this.dtInsumos.Visible = false;
            this.grpOrder.Visible = true;
            this.grpTipo.Visible = true;

            //Ordenamiento grillas a .F.
            DeshabilitarOrdenGrillas();

            //Cargar Grilla
            CargarGrilla(0,"","");

            //Titulo Ventana
            this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;
        }

        #endregion

        #region Método que deshabilita el reordenamiento de las grilla desde sus cabeceras

        private void DeshabilitarOrdenGrillas()
        {
            // Deshabilito la reordenación de las columnas de las grillas
            foreach (DataGridViewColumn dgvCol in dtProductos.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        #endregion

        #region Metodo CargarGrilla

        private void CargarGrilla(int p_Param, string s_Param, string s_Tipo)
        {
            string sCadSQL = "";

            DataTable myDataTable = new DataTable();

            switch (p_Param)
            {
                case 0: //Productos
                    if (string.IsNullOrEmpty(s_Param) && string.IsNullOrEmpty(s_Tipo))
                    {
                        sCadSQL = "Select * from Vista_Listado_Precios_Productos order by CodigoArticulo Asc";
                    }
                    else
                    {
                        sCadSQL = "Select * from Vista_Listado_Precios_Productos order by " + s_Param + " " + s_Tipo;
                    }
                    myDataTable = clsDataBD.GetSql(sCadSQL);
                    dtProductos.AutoGenerateColumns = false;
                    dtProductos.DataSource = myDataTable;
                    break;

                case 1: //Insumos
                    if (string.IsNullOrEmpty(s_Param))
                    {
                        sCadSQL = "Select * from Insumos_Articulos";
                    }
                    else
                    {
                        sCadSQL = "Select * from Insumos_Articulos order by " + s_Param;
                    }
                    myDataTable = clsDataBD.GetSql(sCadSQL);
                    dtInsumos.AutoGenerateColumns = false;
                    dtInsumos.DataSource = myDataTable;
                    break;
            }

            //Calculos de coeficientes e iva
            CalcularItems();
        }

        #endregion

        #region Método que calcula datos para la grilla

        private void CalcularItems()
        {
            // Variable que guarda el resultado de la multiplicación
            double pPub = 0;
            double pDist = 0;
            double pRev = 0;
            double dIva=0;
            double calculoiva=0;

            // Recorro la grilla y hago el cálculo
            foreach (DataGridViewRow row in dtProductos.Rows)
            {
                    pPub = Convert.ToDouble(row.Cells["Precio"].Value) * Convert.ToDouble(row.Cells["CoeficientePublico"].Value);
                    pDist = Convert.ToDouble(row.Cells["Precio"].Value) * Convert.ToDouble(row.Cells["CoeficienteDistribuidor"].Value);
                    pRev = Convert.ToDouble(row.Cells["Precio"].Value) * Convert.ToDouble(row.Cells["CoeficienteRevendedor"].Value);
           
                    //FORMATEO DE COLUMNAS

                    // Asigno el valor a la celda
                    row.Cells["Publico"].Value = pPub.ToString("#0.00");
                    row.Cells["Distribuidor"].Value = pDist.ToString("#0.00");
                    row.Cells["Revendedor"].Value = pRev.ToString("#0.00");

                    dIva = Convert.ToDouble(row.Cells["PorcentajeIva"].Value);
                    
                    // Asigno el valor a la celda     
                    calculoiva = 1 + (dIva / 100);
                    row.Cells["PubIva"].Value = (pPub * calculoiva).ToString("#0.00");
                    row.Cells["DistIva"].Value = (pDist * calculoiva).ToString("#0.00");
                    row.Cells["RevIva"].Value = (pRev * calculoiva).ToString("#0.00");
            }
        }

        #endregion

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //Cerrer el formulario
            this.Close();
        }

        #region Eventos CheckedChanged

        private void rbIns_CheckedChanged(object sender, EventArgs e)
        {
            if (rbIns.Checked)
            {
                //cambiar vista
                this.dtProductos.TabStop = false;
                this.dtProductos.Visible = false;
                this.dtInsumos.TabStop = true;
                this.dtInsumos.Visible = true;
                this.grpOrder.Visible = false;
                this.grpTipo.Visible = false;

                //Grilla
                CargarGrilla(1, "", "");
            }

        }

        private void rbProd_CheckedChanged(object sender, EventArgs e)
        {
            if (rbProd.Checked)
            {
                //Cambiar vista
                this.dtProductos.TabStop = true;
                this.dtProductos.Visible = true;
                this.dtInsumos.TabStop = false;
                this.dtInsumos.Visible = false;

                this.grpOrder.Visible = true;
                this.grpTipo.Visible = true;

                this.rbOrderCod.Checked = true;
                this.rbOrderArt.Checked = false;
                this.rbOrderTipoProd.Checked = false;
                this.rbOrderPrecio.Checked = false;

                this.rbAsc.Checked = true;
                this.rbDesc.Checked = false;

                //Grilla
                CargarGrilla(0, "", "");
            }
        }

        private void rbOrderCod_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbAsc.Checked)
            {
                this.CargarGrilla(0, "CodigoArticulo","Asc");
            }
            else
            {
                this.CargarGrilla(0, "CodigoArticulo","Desc");
            }
        }

        private void rbOrderArt_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbAsc.Checked)
            {
                this.CargarGrilla(0, "Articulo", "Asc");
            }
            else
            {
                this.CargarGrilla(0, "Articulo", "Desc");
            }
        }

        private void rbOrderTipoProd_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbAsc.Checked)
            {
                this.CargarGrilla(0, "CoeficienteArticulo","Asc");
            }
            else
            {
                this.CargarGrilla(0, "CoeficienteArticulo", "Desc");
            }
        }

        private void rbOrderPrecio_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbAsc.Checked)
            {
                this.CargarGrilla(0, "Precio","Asc");
            }
            else
            {
                this.CargarGrilla(0, "Precio","Desc");
            }
        }

        private void rbAsc_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOrderCod.Checked)
            {
                this.CargarGrilla(0, "CodigoArticulo", "Asc");
            }

            if (rbOrderArt.Checked)
            {
                this.CargarGrilla(0, "Articulo", "Asc");
            }

            if (rbOrderTipoProd.Checked)
            {
                this.CargarGrilla(0, "CoeficienteArticulo", "Asc");
            }

            if (rbOrderPrecio.Checked)
            {
                this.CargarGrilla(0, "Precio", "Asc");
            }
        }

        private void rbDesc_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOrderCod.Checked)
            {
                this.CargarGrilla(0, "CodigoArticulo", "Desc");
            }

            if (rbOrderArt.Checked)
            {
                this.CargarGrilla(0, "Articulo", "Desc");
            }

            if (rbOrderTipoProd.Checked)
            {
                this.CargarGrilla(0, "CoeficienteArticulo", "Desc");
            }

            if (rbOrderPrecio.Checked)
            {
                this.CargarGrilla(0, "Precio", "Desc");
            }
        }

        #endregion

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //Data Set
            dsReportes oDsArt = new dsReportes();
            int dgvFilas = 0;

            //Productos?
            if (rbProd.Checked)
            {

                //Objeto Reporte
                rptInfoProductos oRepArt = new rptInfoProductos();

                //Cargar Reporte            
                oRepArt.Load(Application.StartupPath + "\\rptInfoProductos.rpt");

                //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
                 dgvFilas = this.dtProductos.Rows.Count;

                for (int i = 0; i < dgvFilas; i++)
                {
                    oDsArt.Tables["InfoProductos"].Rows.Add
                    (new object[] { dtProductos[0,i].Value.ToString(),
                dtProductos[1,i].Value.ToString(),
                dtProductos[3,i].Value.ToString(),
                dtProductos[4,i].Value.ToString(),
                dtProductos[5,i].Value.ToString(),
                dtProductos[6,i].Value.ToString(),
                dtProductos[7,i].Value.ToString(),
                dtProductos[8,i].Value.ToString(),
                dtProductos[9,i].Value.ToString(),
                dtProductos[10,i].Value.ToString(),
                dtProductos[11,i].Value.ToString(),
                });

                }

                //Establecer el DataSet como DataSource
                oRepArt.SetDataSource(oDsArt);
                //Pasar como parámetro nombre del reporte
                clsGlobales.myRptDoc = oRepArt;
                if (this.rbIns.Checked)
                {
                    oRepArt.DataDefinition.FormulaFields["InsProd"].Text = "'" + " - INSUMOS / INGREDIENTES" + "'";
                }
                else
                {
                    oRepArt.DataDefinition.FormulaFields["InsProd"].Text = "'" + " - PRODUCTOS" + "'";
                }

                oRepArt.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
                oRepArt.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
                oRepArt.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
                oRepArt.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
                oRepArt.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
                oRepArt.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
                oRepArt.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
                oRepArt.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";

            }
            else
            {

                //Objeto Reporte
                rptInfoInsumos oRepArt = new rptInfoInsumos();

                //Cargar Reporte            
                oRepArt.Load(Application.StartupPath + "\\rptInfoInsumos.rpt");

                //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
                dgvFilas = this.dtInsumos.Rows.Count;

                for (int i = 0; i < dgvFilas; i++)
                {
                    oDsArt.Tables["InfoInsumos"].Rows.Add
                    (new object[] { dtInsumos[0,i].Value.ToString(),
                dtInsumos[1,i].Value.ToString(),
                dtInsumos[2,i].Value.ToString(),
                dtInsumos[3,i].Value.ToString(),
                dtInsumos[4,i].Value.ToString(),
                });

                }

                //Establecer el DataSet como DataSource
                oRepArt.SetDataSource(oDsArt);
                //Pasar como parámetro nombre del reporte
                clsGlobales.myRptDoc = oRepArt;
                if (this.rbIns.Checked)
                {
                    oRepArt.DataDefinition.FormulaFields["InsProd"].Text = "'" + " - INSUMOS / INGREDIENTES" + "'";
                }
                else
                {
                    oRepArt.DataDefinition.FormulaFields["InsProd"].Text = "'" + " - PRODUCTOS" + "'";
                }

                oRepArt.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
                oRepArt.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
                oRepArt.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
                oRepArt.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
                oRepArt.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
                oRepArt.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
                oRepArt.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
                oRepArt.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";

            }


            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();
        }


    }
}

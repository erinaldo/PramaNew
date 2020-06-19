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
    public partial class frmInformeVtasTipoCli : Form
    {
        public frmInformeVtasTipoCli()
        {
            InitializeComponent();
        }

        private void frmInformeVtasTipoCli_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            //Estado
            clsGlobales.myEstado = "C";
            //Tooltips
            CargarToolsTip();
            //Cargar Grilla
            CargarGrilla();
            //Dates
            // Variable que almacena el mes en curso
            int iMesActual = DateTime.Now.Month;
            // Variable que almacena el año en curso
            int iAnoActual = DateTime.Now.Year;

            // Seteo el DTP de fecha incial
            dtDesde.Value = new DateTime(iAnoActual, iMesActual, 1);
            // Seteo el DTP de fecha Final
            if (!(iMesActual == 2))
            {
                dtHasta.Value = new DateTime(iAnoActual, iMesActual, 30);
            }
            else
            {
                dtHasta.Value = new DateTime(iAnoActual, iMesActual, 28);
            }

            //Titulo
            this.Text = clsGlobales.cFormato.getTituloVentana() + " - DETALLE DE VENTAS POR TIPO DE CLIENTE ";
        }

        #region Método que carga los ToolsTip del formulario

        private void CargarToolsTip()
        {
            toolTip1.SetToolTip(this.btnImprimir, "Imprimir");
            toolTip2.SetToolTip(this.btnSalir, "Salir");

        }

        #endregion


        #region CargarGrilla()


        //Carga la grilla con los datos de movimientos en la cta cte del cliente
        private void CargarGrilla()
        {
            //Auxiliar String
            string myCadSQL = "";

            //CAMBIAR PUNTERO MOUSE
            Cursor.Current = Cursors.WaitCursor;

            //Cargar SQL
            DataTable myData = null;

            //Check Connection
            if (clsGlobales.ConB == null)
            {
                //Tipo Cliente
                if (this.rbnAll.Checked) { myCadSQL = "exec getVtasTipoCli 0,'" + this.dtDesde.Value.ToShortDateString() + "','" + this.dtHasta.Value.ToShortDateString() + "',0"; }
                if (this.rbnPub.Checked) { myCadSQL = "exec getVtasTipoCli 28,'" + this.dtDesde.Value.ToShortDateString() + "','" + this.dtHasta.Value.ToShortDateString() + "',0"; }
                if (this.rbnDist.Checked){ myCadSQL = "exec getVtasTipoCli 29,'" + this.dtDesde.Value.ToShortDateString() + "','" + this.dtHasta.Value.ToShortDateString() + "',0"; }

                if (this.rbnRev.Checked)
                {
                    myCadSQL = "exec getVtasTipoCli 30,'" + this.dtDesde.Value.ToShortDateString() + "','" + this.dtHasta.Value.ToShortDateString() + "', 0";
                }
                else if (this.rbnCRsal.Checked)
                {
                    myCadSQL = "exec getVtasTipoCli 30,'" + this.dtDesde.Value.ToShortDateString() + "','" + this.dtHasta.Value.ToShortDateString() + "', 1"; 
                }

                myData = clsDataBD.GetSql(myCadSQL);
            }
            else
            {
                //Tipo Cliente
                if (this.rbnAll.Checked) { myCadSQL = "exec getVtasTipoCli2 0,'" + this.dtDesde.Value.ToShortDateString() + "','" + this.dtHasta.Value.ToShortDateString() + "',0"; }
                if (this.rbnPub.Checked) { myCadSQL = "exec getVtasTipoCli2 28,'" + this.dtDesde.Value.ToShortDateString() + "','" + this.dtHasta.Value.ToShortDateString() + "',0"; }
                if (this.rbnDist.Checked){ myCadSQL = "exec getVtasTipoCl2 29,'" + this.dtDesde.Value.ToShortDateString() + "','" + this.dtHasta.Value.ToShortDateString() + "',0"; }
                
                
                if (this.rbnRev.Checked)                
                { 
                    myCadSQL = "exec getVtasTipoCli2 30,'" + this.dtDesde.Value.ToShortDateString() + "','" + this.dtHasta.Value.ToShortDateString() + "', 0"; 
                }
                else if (this.rbnCRsal.Checked)
                {
                    myCadSQL = "exec getVtasTipoCli 30,'" + this.dtDesde.Value.ToShortDateString() + "','" + this.dtHasta.Value.ToShortDateString() + "', 1"; 
                }

                myData = clsDataBD.GetSqlB(myCadSQL);

            }

            //Otras configuraciones
            this.dvgData.AutoGenerateColumns = false;
            this.dvgData.DataSource = myData;

            //Hay datos?
            double totcant = 0;
            double totgral = 0;

            foreach (DataRow row in myData.Rows)
            {
               totcant+= Convert.ToDouble(row["Cantidad Vendida"]);
               totgral+= Convert.ToDouble(row["Total"]);
            }
            
            //Show
            this.txtCantTotal.Text = totcant.ToString("#0");
            this.txtTotGral.Text = totgral.ToString("###,###,##0.00");

            //CAMBIAR PUNTERO MOUSE
            Cursor.Current = Cursors.Default;
        }

        #endregion

        private void rbnAll_CheckedChanged(object sender, EventArgs e)
        {
            this.CargarGrilla();
        }

        private void rbnPub_CheckedChanged(object sender, EventArgs e)
        {
            this.CargarGrilla();
        }

        private void rbnDist_CheckedChanged(object sender, EventArgs e)
        {
            this.CargarGrilla();
        }

        private void rbnRev_CheckedChanged(object sender, EventArgs e)
        {
            this.CargarGrilla();
        }

        private void dtDesde_ValueChanged(object sender, EventArgs e)
        {
            if (clsGlobales.cValida.EsFecha(this.dtDesde.Value.ToShortDateString()))
            {
                this.CargarGrilla();
            }
        }

        private void dtHasta_ValueChanged(object sender, EventArgs e)
        {
            if (clsGlobales.cValida.EsFecha(this.dtHasta.Value.ToShortDateString()))
            {
                this.CargarGrilla();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string strTipoCli = "";

            //Hay datos?
            if (!(dvgData.Rows.Count > 0))
            {
                MessageBox.Show("No hay datos para imprimir para la consulta especificada!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //PREGUNTAR SI ESTA CONFIGURADO EN PARAMETROS
            if (clsGlobales.cParametro.Imprimir)
            {
                DialogResult dlResult = MessageBox.Show("¿Desea imprimir el Informe de Ventas por Tipo de Cliente y Período?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Si confirma... cambiar estado
                if (dlResult == DialogResult.No)
                {
                    return;
                }
            }

            //Data Set
            dsReportes oDsMovCtaCte = new dsReportes();

            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = this.dvgData.Rows.Count;

            for (int i = 0; i < dgvFilas; i++)
            {
                oDsMovCtaCte.Tables["dtVtasTipoCli"].Rows.Add
                (new object[] { dvgData[1,i].Value.ToString(),
                    dvgData[2,i].Value.ToString(), 
                    dvgData[3,i].Value.ToString(),
                    dvgData[4,i].Value.ToString(),
                    dvgData[5,i].Value.ToString()});
            }

            //Objeto Reporte
            rptVtasTipoCli oRepVtasTipoCli = new rptVtasTipoCli();

            //Cargar Reporte                                    
            oRepVtasTipoCli.Load(Application.StartupPath + "\\rptVtasTipoCli.rpt");

            //Establecer el DataSet como DataSource
            oRepVtasTipoCli.SetDataSource(oDsMovCtaCte);

            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepVtasTipoCli;

            oRepVtasTipoCli.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepVtasTipoCli.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
            oRepVtasTipoCli.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
            oRepVtasTipoCli.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
            oRepVtasTipoCli.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
            oRepVtasTipoCli.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepVtasTipoCli.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
            oRepVtasTipoCli.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";

            //Data Cliente
            if (this.rbnAll.Checked) { strTipoCli = "TODOS"; }
            if (this.rbnPub.Checked) { strTipoCli = "PUBLICO"; }
            if (this.rbnDist.Checked) { strTipoCli = "DISTRIBUIDOR"; }
            if (this.rbnRev.Checked) { strTipoCli = "REVENDEDOR"; }

            oRepVtasTipoCli.DataDefinition.FormulaFields["cliente"].Text = "' Tipo de Cliente : " + strTipoCli + ", Período DESDE: " + this.dtDesde.Value.ToShortDateString() + " -  HASTA: " + this.dtHasta.Value.ToShortDateString() + "'";
            oRepVtasTipoCli.DataDefinition.FormulaFields["totalcant"].Text = "'" + this.txtCantTotal.Text + "'";
            oRepVtasTipoCli.DataDefinition.FormulaFields["total"].Text = "'" + this.txtTotGral.Text + "'";

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports(2);
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog(); 
        }

        private void rbnCRsal_CheckedChanged(object sender, EventArgs e)
        {
            this.CargarGrilla();
        }

    }
}

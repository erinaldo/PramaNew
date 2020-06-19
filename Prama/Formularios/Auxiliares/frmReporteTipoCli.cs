using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prama.Formularios.Auxiliares
{
    public partial class frmReporteTipoCli : Form
    {
        public frmReporteTipoCli()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReporteTipoCli_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            //Inicio
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
            this.Text = clsGlobales.cFormato.getTituloVentana() + " - REPORTE REVISION REVENDEDORES ";

            //Setear Columnas Grilla
            SeteaColumnas();
        }

        #region SeteaColumnas

        private void SeteaColumnas()
        {
            //Formateo para la grilla en blanco
            if (clsGlobales.ConB == null)
            {
                // Headers de columnas
                dvgData.Columns["Total_N"].Visible = false;
                dvgData.Columns["TotalGral"].Visible = false;

                dvgData.Columns["RazonSocial"].Width = 230;
                dvgData.Columns["Mail"].Width = 190;

            }
            // Formateo para el negro
            else
            {
                // Headers de columnas
                dvgData.Columns["Total_N"].Visible = true;
                dvgData.Columns["TotalGral"].Visible = true;

                dvgData.Columns["RazonSocial"].Width = 180;
                dvgData.Columns["Mail"].Width = 130;
            }

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
                if (this.rbnAll.Checked) { myCadSQL = "exec getCliFact 0,'" + this.dtDesde.Value.ToShortDateString() + "','" + this.dtHasta.Value.ToShortDateString() + "',0"; }
                if (this.rbnPub.Checked) { myCadSQL = "exec getCliFact 28,'" + this.dtDesde.Value.ToShortDateString() + "','" + this.dtHasta.Value.ToShortDateString() + "',0"; }
                if (this.rbnDist.Checked) { myCadSQL = "exec getCliFact 29,'" + this.dtDesde.Value.ToShortDateString() + "','" + this.dtHasta.Value.ToShortDateString() + ",0'"; }
             //   if (this.rbnRev.Checked) { myCadSQL = "exec getCliFact 30,'" + this.dtDesde.Value.ToShortDateString() + "','" + this.dtHasta.Value.ToShortDateString() + "',1"; }

                if (this.rbnRev.Checked)
                {
                    myCadSQL = "exec getCliFact 30,'" + this.dtDesde.Value.ToShortDateString() + "','" + this.dtHasta.Value.ToShortDateString() + "', 0";
                }
                else if (this.rbnCRsal.Checked)
                {
                    myCadSQL = "exec getCliFact 30,'" + this.dtDesde.Value.ToShortDateString() + "','" + this.dtHasta.Value.ToShortDateString() + "', 1";
                }

                myData = clsDataBD.GetSql(myCadSQL);
            }
            else
            {
                //Tipo Cliente
                if (this.rbnAll.Checked) { myCadSQL = "exec getCliFact2 0,'" + this.dtDesde.Value.ToShortDateString() + "','" + this.dtHasta.Value.ToShortDateString() + "',0"; }
                if (this.rbnPub.Checked) { myCadSQL = "exec getCliFact2 28,'" + this.dtDesde.Value.ToShortDateString() + "','" + this.dtHasta.Value.ToShortDateString() + "',0"; }
                if (this.rbnDist.Checked) { myCadSQL = "exec getCliFact2 29,'" + this.dtDesde.Value.ToShortDateString() + "','" + this.dtHasta.Value.ToShortDateString() + "',0"; }
                //  if (this.rbnRev.Checked) { myCadSQL = "exec getCliFact2 30,'" + this.dtDesde.Value.ToShortDateString() + "','" + this.dtHasta.Value.ToShortDateString() + "'" }

                if (this.rbnRev.Checked)
                {
                    myCadSQL = "exec getCliFact2 30,'" + this.dtDesde.Value.ToShortDateString() + "','" + this.dtHasta.Value.ToShortDateString() + "', 0";
                }
                else if (this.rbnCRsal.Checked)
                {
                    myCadSQL = "exec getCliFact2 30,'" + this.dtDesde.Value.ToShortDateString() + "','" + this.dtHasta.Value.ToShortDateString() + "', 1";
                }

              //myCadSQL = " Select * from getCliFactView_N";

              myData = clsDataBD.GetSqlB(myCadSQL);

            }

            //Otras configuraciones
            this.dvgData.AutoGenerateColumns = false;
            this.dvgData.DataSource = myData;

            //Hay datos?
            //double totcant = 0;
            //double totgral = 0;

            //foreach (DataRow row in myData.Rows)
            //{
            //   totcant+= Convert.ToDouble(row["Cantidad Vendida"]);
            //   totgral+= Convert.ToDouble(row["Total"]);
            //}
            
            //Show
            //this.txtCantTotal.Text = totcant.ToString("#0");
            //this.txtTotGral.Text = totgral.ToString("###,###,##0.00");

            double totgral = 0;
            double Suma = 0;
  
            foreach (DataRow row in myData.Rows)
            {
               totgral += Convert.ToDouble(row["Total"]);
            }
            

            //TOTAL GRAL ( Solo N )
            if (clsGlobales.ConB !=null)
            {
                foreach (DataGridViewRow rows in this.dvgData.Rows)
                {
                  Suma = Convert.ToDouble(rows.Cells["Total"].Value) + Convert.ToDouble(rows.Cells["Total_N"].Value);
                  rows.Cells["TotalGral"].Value = Suma.ToString("###,###,##0.00");
                  totgral += Convert.ToDouble(rows.Cells["Total_N"].Value);
                }
            }

            //Show
            this.txtTotGral.Text = totgral.ToString("###,###,##0.00");

            //CAMBIAR PUNTERO MOUSE
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region Método que carga los ToolsTip del formulario

        private void CargarToolsTip()
        {
            toolTip2.SetToolTip(this.btnSalir, "Salir");

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

        private void rbnCRsal_CheckedChanged(object sender, EventArgs e)
        {

            this.CargarGrilla();
        }
    }
}

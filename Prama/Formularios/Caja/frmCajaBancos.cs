using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prama.Formularios.Caja
{
    public partial class frmCajaBancos : Form
    {

        int IdCajaAs = 0;
        string sWhere = "";
        double sTotal = 0;

        public frmCajaBancos(double p_Total)
        {
            InitializeComponent();
            sTotal = p_Total;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCajaBancos_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			

            this.Text = clsGlobales.cFormato.getTituloVentana() + " - " + this.Text;

            clsDataBD.CargarCombo(cboCajaAs, "CajaAsociacionesCuentas", "CajaAsociaciones", "IdCajaAsociaciones","",1);
            cboCajaAs.SelectedIndex = 0;

            this.txtBancos.Text = sTotal.ToString("#0.00");

            if (!(clsGlobales.bView))
            {
                this.btnImprimir.Enabled = false;
                this.lblTotalBcos.Visible = false;
                this.txtBancos.Visible=false;
                lblSaldo.Visible = false;
                txtSaldo.Visible = false;
                
            }

            
        }

        private void cboCajaAs_SelectedIndexChanged(object sender, EventArgs e)
        {
            IdCajaAs = Convert.ToInt32(cboCajaAs.SelectedValue.ToString());
            
            //double dSaldo = 0;

            //string myCadena = "Select * from CajaAsociacionesCuentas Where IdCajaAsociaciones = " + IdCajaAs;

            //DataTable myDataCajaAs;                           
            
            //if (clsGlobales.ConB == null) 
            //{ 
            //    myDataCajaAs = clsDataBD.GetSql(myCadena); 
            //} 
            //else
            //{
            //    myDataCajaAs = clsDataBD.GetSqlB(myCadena);
            //}

            //foreach (DataRow myRow in myDataCajaAs.Rows)
            //{
            //    dSaldo = Convert.ToDouble(myRow["Saldo"].ToString());
            //}

            //txtSaldo.Text = dSaldo.ToString("#0.00");

            //CARGAR GRILLA MOVIMINENTOS CAJA
            CargarGrilla();
            //TOTALES
            CalcularTotales();

        }

        /// <summary>
        /// Metodo que carga detalle mov caja
        /// </summary>
        private void CargarGrilla(string pWhere = "")
        {
            DataTable myDataCaja = new DataTable();

            string sSQL = "select * from Vista_CajaBancos Where IdCajaAsociaciones = " + IdCajaAs + pWhere; //+ " ORDER BY CA.IdCaja";

            if (clsGlobales.ConB == null)
            {
                myDataCaja = clsDataBD.GetSql(sSQL);
            }
            else
            {
                myDataCaja = clsDataBD.GetSqlB(sSQL);
            }

            dgvCaja.AutoGenerateColumns = false;
            dgvCaja.DataSource = myDataCaja;

        }

        private void txtSaldo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtFecha_TextChanged(object sender, EventArgs e)
        {
            sWhere="";
            if (txtFecha.Text!="")
            {
               sWhere += " AND FechaS ='" + txtFecha.Text + "' ";               
            }
            if (optTransfer.Checked)
            {
                sWhere += " AND Transferencia <>0";
            }
            if (optMP.Checked)
            {
                sWhere += " AND MP <>0";
            }
            if (optDeb.Checked)
            {
                sWhere += " AND Debito <>0";
            }
            if (optCred.Checked)
            {
                sWhere += " AND Credito <>0";
            }

            CargarGrilla(sWhere);
        }

        private void optTransfer_CheckedChanged(object sender, EventArgs e)
        {
                sWhere = "";
                if (txtFecha.Text != "")
                {
                    sWhere += " AND FechaS ='" + txtFecha.Text + "' ";
                }

                if (optTransfer.Checked)
                {
                   sWhere += " AND Transferencia <>0";
                }

                CargarGrilla(sWhere);
        }

        private void optMP_CheckedChanged(object sender, EventArgs e)
        {
                sWhere = "";
                if (txtFecha.Text != "")
                {
                    sWhere += " AND FechaS ='" + txtFecha.Text + "' ";
                }

                if (optMP.Checked)
                {
                    sWhere += " AND MP <>0";
                }

                CargarGrilla(sWhere);

        }

        private void optDeb_CheckedChanged(object sender, EventArgs e)
        {
                sWhere = "";
                if (txtFecha.Text != "")
                {
                    sWhere += " AND FechaS ='" + txtFecha.Text + "' ";
                }

                if (optDeb.Checked)
                {
                    sWhere += " AND Debito <>0";
                }

                CargarGrilla(sWhere);

        }

        private void optCred_CheckedChanged(object sender, EventArgs e)
        {
            
                sWhere = "";

                if (txtFecha.Text != "")
                {
                    sWhere += " AND FechaS ='" + txtFecha.Text + "' ";
                }

                if (optCred.Checked)
                {
                    sWhere += " AND Credito <>0";
                }

                CargarGrilla(sWhere);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtFecha.Text = "";
            CargarGrilla();
        }


        #region Método que calcula los totales de la caja

        private void CalcularTotales()
        {
            // Variables del método
            double dSaldo = 0;

            if (dgvCaja.Rows.Count == 0)
            {
                txtSaldo.Text = dSaldo.ToString("#0.00");
                return;
            }

            //// Traigo los datos de la tabla que contiene los saldos de las cajas
            //string myCadenaSaldos = "select * from CajaSaldos";
            //// Paso los datos a una tabla
            //DataTable myTable = clsDataBD.GetSql(myCadenaSaldos);
            // recorro la tabla y paso los dato a las variables
            foreach (DataGridViewRow myRow in dgvCaja.Rows)
            {
                dSaldo += Convert.ToDouble(myRow.Cells["Total"].Value);
            }

            txtSaldo.Text = dSaldo.ToString("#0.00");

            // Color de las letras
            if (dSaldo < 0)
            {
                txtSaldo.ForeColor = Color.Red;
            }
            else
            {
                txtSaldo.ForeColor = Color.Black;
            }
        }

        #endregion

        private void btnImprimir_Click(object sender, EventArgs e)
        {

            //No hay datos, salir
            if (dgvCaja.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para impresión!", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Data Set
            dsReportes oDsCajaBco = new dsReportes();

            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = dgvCaja.Rows.Count;

            for (int i = 0; i < dgvFilas; i++)
            {
                oDsCajaBco.Tables["dtCajaBancos"].Rows.Add
                (new object[] { dgvCaja[0,i].Value.ToString(),
                dgvCaja[1,i].Value.ToString(),
                dgvCaja[2,i].Value.ToString(),
                dgvCaja[3,i].Value.ToString(),
                dgvCaja[4,i].Value.ToString(),
                dgvCaja[5,i].Value.ToString(),
                dgvCaja[6,i].Value.ToString(),
                dgvCaja[7,i].Value.ToString()
                });

            }

            //Objeto Reporte
            rptCajaBancos oRepCajaBco = new rptCajaBancos();
            //Cargar Reporte            
            oRepCajaBco.Load(Application.StartupPath + "\\rptCajaBancos.rpt");
            //Establecer el DataSet como DataSource
            oRepCajaBco.SetDataSource(oDsCajaBco);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepCajaBco;
            oRepCajaBco.DataDefinition.FormulaFields["TipoCaja"].Text = "'" + " CAJA DE BANCOS - CUENTA: " + cboCajaAs.Text + "'";
            oRepCajaBco.DataDefinition.FormulaFields["Total"].Text = "'" + Convert.ToDouble(txtBancos.Text).ToString("0.00##") + "'";
            oRepCajaBco.DataDefinition.FormulaFields["SldoCta"].Text = "'" + Convert.ToDouble(txtSaldo.Text).ToString("0.00##") + "'";
            oRepCajaBco.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepCajaBco.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
            oRepCajaBco.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
            oRepCajaBco.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
            oRepCajaBco.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
            oRepCajaBco.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepCajaBco.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
            oRepCajaBco.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();
        }

        private void optOtros_CheckedChanged(object sender, EventArgs e)
        {
            sWhere = "";

            if (txtFecha.Text != "")
            {
                sWhere += " AND FechaS ='" + txtFecha.Text + "' ";
            }

            if (optOtros.Checked)
            {
                sWhere += " AND Transferencia = 0 AND MP = 0 AND Debito = 0 AND Credito = 0";
            }

            CargarGrilla(sWhere);
        }
    }
}

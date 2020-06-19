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
    public partial class frmCajaCheques : Form
    {
        public frmCajaCheques()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCajaCheques_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 
			
            this.Text = clsGlobales.cFormato.getTituloVentana() + " - " + this.Text;
            CargarGrilla();
        }

        #region Método que calcula los totales de la caja

        private void CalcularTotales()
        {
            // Variables del método
            double dSaldo = 0;

            //// Traigo los datos de la tabla que contiene los saldos de las cajas
            //string myCadenaSaldos = "select * from CajaSaldos";
            //// Paso los datos a una tabla
            //DataTable myTable = clsDataBD.GetSql(myCadenaSaldos);
            // recorro la tabla y paso los dato a las variables
            foreach (DataGridViewRow myRow in dgvCajaCH.Rows)
            {
                dSaldo += Convert.ToDouble(myRow.Cells["Importe"].Value);
            }

            txtEfectivoGral.Text = dSaldo.ToString("#0.00");

            // Color de las letras
            if (dSaldo < 0)
            {
                txtEfectivoGral.ForeColor = Color.Red;
            }
            else
            {
                txtEfectivoGral.ForeColor = Color.Black;
            }
        }

        #endregion

        #region Método que carga la grilla

        private void CargarGrilla(string sWhere = "")
        {
            DataTable mDtTable = new DataTable();
            string myCadenaSQL = "";

            if (clsGlobales.ConB == null)
            {        
                myCadenaSQL = "select * from Cheques" + sWhere;
                mDtTable = clsDataBD.GetSql(myCadenaSQL);
            }
            else
            {
                myCadenaSQL = "select * from Cheques" + sWhere;
                mDtTable = clsDataBD.GetSqlB(myCadenaSQL);
            }

            // Evito que el dgv genere columnas automáticas
            dgvCajaCH.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvCajaCH.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvCajaCH.Rows.Count;
            // Posiciono la grilla en la última fila, si hay
            if (Filas > 0)
            {
                //Posicionamiento grilla
                int r = Filas - 1;
                int c = dgvCajaCH.CurrentCell.ColumnIndex;
                dgvCajaCH.CurrentCell = dgvCajaCH.Rows[r].Cells[c];
                // Paso los datos a los controles
            }

            // Muestro el saldo de la caja
            CalcularTotales();

            if (!(clsGlobales.bView))
            {
                this.btnImprimir.Enabled = false;
                this.lblSaldo.Visible = false;
                this.txtEfectivoGral.Visible = false;
            }

        }

        #endregion

        private void chkCHCartera_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCHCartera.Checked)
            {
                CargarGrilla(" WHERE EnCartera = 1");
            }
            else
            {
                CargarGrilla();
            }

        }

        private void txtFC_TextChanged(object sender, EventArgs e)
        {
            if (!(this.txtFC.Text == ""))
            {
                // Cargo las localidades filtradas por la búsqueda
                CargarGrilla(" WHERE FechaCobro = '" + txtFC.Text + "'");
            }
            else
            {
                CargarGrilla();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtFC.Text = "";
            txtFE.Text = "";
            txtNro.Text = "";
            CargarGrilla();
        }

        private void txtFE_TextChanged(object sender, EventArgs e)
        {
            if (!(this.txtFE.Text == ""))
            {
                // Cargo las localidades filtradas por la búsqueda
                CargarGrilla(" WHERE FechaEmision = '" + txtFE.Text + "'");
            }
            else
            {
                CargarGrilla();
            }
        }

        private void txtFE_Click(object sender, EventArgs e)
        {
            txtFC.Text = "";
            txtNro.Text = "";
        }

        private void txtFC_Click(object sender, EventArgs e)
        {
            txtFE.Text = "";
            txtNro.Text = "";
        }

        private void txtNro_Click(object sender, EventArgs e)
        {
            txtFC.Text = "";
            txtFE.Text = "";
        }

        private void txtNro_TextChanged(object sender, EventArgs e)
        {
            if (!(this.txtNro.Text == ""))
            {
                // Cargo las localidades filtradas por la búsqueda
                CargarGrilla(" WHERE Numero = " + Convert.ToInt32(txtNro.Text));
            }
            else
            {
                CargarGrilla();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

            //No hay datos, salir
            if (dgvCajaCH.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para impresión!", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            //Data Set
            dsReportes oDsCheques = new dsReportes();

            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = dgvCajaCH.Rows.Count;

            for (int i = 0; i < dgvFilas; i++)
            {
                oDsCheques.Tables["dtCajaCH"].Rows.Add
                (new object[] { dgvCajaCH[0,i].Value.ToString(),
                dgvCajaCH[1,i].Value.ToString(),
                dgvCajaCH[2,i].Value.ToString(),
                dgvCajaCH[3,i].Value.ToString(),
                dgvCajaCH[4,i].Value.ToString(),
                dgvCajaCH[5,i].Value.ToString() });

            }

            //Objeto Reporte
            rptCajaCH oRepCajaCH = new rptCajaCH();
            //Cargar Reporte            
            oRepCajaCH.Load(Application.StartupPath + "\\rptCajaCH.rpt");
            //Establecer el DataSet como DataSource
            oRepCajaCH.SetDataSource(oDsCheques);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepCajaCH;
            oRepCajaCH.DataDefinition.FormulaFields["TipoCaja"].Text = "'" + " - DETALLE DE CAJA CHEQUES " + "'";
            oRepCajaCH.DataDefinition.FormulaFields["Total"].Text = "'" + Convert.ToDouble(txtEfectivoGral.Text).ToString("0.00##") + "'";
            oRepCajaCH.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepCajaCH.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
            oRepCajaCH.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
            oRepCajaCH.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
            oRepCajaCH.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
            oRepCajaCH.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepCajaCH.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
            oRepCajaCH.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();
        }
    }
}

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
    public partial class frmMostrarCajas : Form
    {
        int iTipoCaja = 0;
        string sTituloReporte = "";

        public frmMostrarCajas(string sTitulo, int p_TipoCaja)
        {
            InitializeComponent();
            iTipoCaja = p_TipoCaja;
            sTituloReporte = sTitulo;

            this.Text = clsGlobales.cFormato.getTituloVentana() + " - " + sTitulo;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMostrarCajas_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 
			
            CargarGrilla();

            if (dgvCaja.Rows.Count > 0)
            {
                gpbBusquedas.Enabled = true;
                button1.Enabled = true;
                btnImprimir.Enabled = true;
            }
            else
            {
                gpbBusquedas.Enabled = false;
                button1.Enabled = false;
                btnImprimir.Enabled = false;
            }

            if (!(clsGlobales.bView))
            {
                this.btnImprimir.Enabled = false;
                lblSaldoCaja.Visible = false;
                txtEfectivoGral.Visible = false;
            }


        }

        #region Método que carga la grilla

        private void CargarGrilla(string sWhere = "")
        {
            DataTable mDtTable = new DataTable();
            string myCadenaSQL = "";
            string sSaldo = "";

            if (clsGlobales.ConB == null)
            {
                switch (iTipoCaja)
                {
                    case 2:
                        // Cadena SQL 
                        myCadenaSQL = "select * from Vista_CajaTransferencias " + sWhere;
                        sSaldo = "SaldoTransferencias";
                        break;
                    case 3:
                        // Cadena SQL 
                        myCadenaSQL = "select * from Vista_CajaMP " + sWhere;
                        sSaldo = "SaldoMP";
                        break;
                    case 4:
                        // Cadena SQL 
                        myCadenaSQL = "select * from Vista_CajaDebito " + sWhere;
                        sSaldo = "SaldoDebito";
                        break;
                    case 5:
                        // Cadena SQL 
                        myCadenaSQL = "select * from Vista_CajaCredito " + sWhere;
                        sSaldo = "SaldoCredito";
                        break;
                }

                mDtTable = clsDataBD.GetSql(myCadenaSQL);
            }
            else
            {

                switch (iTipoCaja)
                {
                    case 2:
                        // Cadena SQL 
                        myCadenaSQL = "select * from Vista_CajaTransferencias " + sWhere;
                        sSaldo = "SaldoTransferencias";
                        break;
                    case 3:
                        // Cadena SQL 
                        myCadenaSQL = "select * from Vista_CajaMP" + sWhere;
                        sSaldo = "SaldoMP";
                        break;
                    case 4:
                        // Cadena SQL 
                        myCadenaSQL = "select * from Vista_CajaDebito" + sWhere;
                        sSaldo = "SaldoDebito";
                        break;
                    case 5:
                        // Cadena SQL 
                        myCadenaSQL = "select * from Vista_CajaCredito" + sWhere;
                        sSaldo = "SaldoCredito";
                        break;
                }

                mDtTable = clsDataBD.GetSqlB(myCadenaSQL);
            }

            // Evito que el dgv genere columnas automáticas
            dgvCaja.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvCaja.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvCaja.Rows.Count;
            // Posiciono la grilla en la última fila, si hay
            if (Filas > 0)
            {
                //Posicionamiento grilla
                int r = Filas - 1;
                int c = dgvCaja.CurrentCell.ColumnIndex;
                dgvCaja.CurrentCell = dgvCaja.Rows[r].Cells[c];
                // Paso los datos a los controles
            }

            // Muestro el saldo de la caja
            CalcularTotales(sSaldo);
        }

        #endregion

        #region Método que calcula los totales de la caja

        private void CalcularTotales(string p_TipoSaldo)
        {
            // Variables del método
            double dSaldo = 0;

            //// Traigo los datos de la tabla que contiene los saldos de las cajas
            //string myCadenaSaldos = "select * from CajaSaldos";
            //// Paso los datos a una tabla
            //DataTable myTable = clsDataBD.GetSql(myCadenaSaldos);
            // recorro la tabla y paso los dato a las variables
            foreach (DataGridViewRow myRow in dgvCaja.Rows)
            {
                dSaldo+= Convert.ToDouble(myRow.Cells["Importe"].Value);
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

        private void btnImprimir_Click(object sender, EventArgs e)
        {

            //No hay datos, salir
            if (dgvCaja.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para impresión!", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            //Data Set
            dsReportes oDsCaja = new dsReportes();

            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = dgvCaja.Rows.Count;

            for (int i = 0; i < dgvFilas; i++)
            {
                oDsCaja.Tables["dtCaja"].Rows.Add
                (new object[] { dgvCaja[0,i].Value.ToString(),
                dgvCaja[1,i].Value.ToString(),
                dgvCaja[2,i].Value.ToString(),
                dgvCaja[3,i].Value.ToString(),
                dgvCaja[4,i].Value.ToString(),
                dgvCaja[5,i].Value.ToString() });

            }

            //Objeto Reporte
            rptCaja oRepCaja = new rptCaja();
            //Cargar Reporte            
            //    oRepTipoCli.Load(Application.StartupPath + "\\rptTipoCli.rpt");
            oRepCaja.Load(Application.StartupPath + "\\rptCaja.rpt");
            //Establecer el DataSet como DataSource
            oRepCaja.SetDataSource(oDsCaja);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepCaja;
            oRepCaja.DataDefinition.FormulaFields["TipoCaja"].Text = "'" + sTituloReporte + "'";
            oRepCaja.DataDefinition.FormulaFields["Total"].Text = "'" + Convert.ToDouble(txtEfectivoGral.Text).ToString("0.00##") + "'";
            oRepCaja.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepCaja.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
            oRepCaja.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
            oRepCaja.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
            oRepCaja.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
            oRepCaja.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepCaja.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
            oRepCaja.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtFecha.Text = "";
            txtRazon.Text = "";
            CargarGrilla();
        }

        private void txtFecha_TextChanged(object sender, EventArgs e)
        {

            if (!(this.txtFecha.Text == ""))
            {
                // Cargo las localidades filtradas por la búsqueda
                CargarGrilla(" WHERE FechaS = '" + txtFecha.Text + "'");
            }
            else
            {
                CargarGrilla();
            }
        }

        private void txtRazon_TextChanged(object sender, EventArgs e)
        {
            if (!(this.txtRazon.Text == ""))
            {
                // Cargo las localidades filtradas por la búsqueda
                CargarGrilla(" WHERE Movimiento Like '" + txtRazon.Text + "%'");
            }
            else
            {
                CargarGrilla();
            }
        }

        private void txtFecha_Click(object sender, EventArgs e)
        {
            txtRazon.Text = "";
        }

        private void txtRazon_Click(object sender, EventArgs e)
        {
            txtFecha.Text = "";
        }

        #region METODO EsFecha (VALIDA FECHA)
        public static Boolean EsFecha(String fecha)
        {
            try
            {
                DateTime.Parse(fecha);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}

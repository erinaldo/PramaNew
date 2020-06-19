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
    public partial class frmCajaEfectivo : Form
    {
        public frmCajaEfectivo()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Cierro el formulario
            this.Close();
        }

        #region Método que carga la grilla

        private void CargarGrilla(string sWhere = "")
        {
            DataTable mDtTable = new DataTable();
            string myCadenaSQL ="";

            // Cadena SQL 
            myCadenaSQL = "select * from Vista_CajaEfectivo" + sWhere;

            if (clsGlobales.ConB == null)
            {
                mDtTable = clsDataBD.GetSql(myCadenaSQL);
            }
            else
            {
                mDtTable = clsDataBD.GetSqlB(myCadenaSQL);
            }
            
            // Evito que el dgv genere columnas automáticas
            dgvCajaEfectivo.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvCajaEfectivo.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvCajaEfectivo.Rows.Count;
            // Posiciono la grilla en la última fila, si hay
            if (Filas > 0)
            {
                //Posicionamiento grilla
                int r = Filas - 1;
                int c = dgvCajaEfectivo.CurrentCell.ColumnIndex;
                dgvCajaEfectivo.CurrentCell = dgvCajaEfectivo.Rows[r].Cells[c];
                // Paso los datos a los controles
            }

            // Muestro el saldo de la caja
            CalcularTotales();
        }

        #endregion

        private void frmCajaEfectivo_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 
			//Titulo		
            this.Text = clsGlobales.cFormato.getTituloVentana() + " - DETALLE DE LA CAJA EFECTIVO ";
            
            CargarGrilla();

            if (!(clsGlobales.bView))
            {
                this.btnImprimir.Enabled = false;
                this.lblSaldo.Visible = false;
                this.txtEfectivoGral.Visible = false;
            }

        }

        #region Método que calcula los totales de la caja

        private void CalcularTotales()
        {
            // Variables del método
            double dEfectivo = 0;

            // Traigo los datos de la tabla que contiene los saldos de las cajas
            string myCadenaSaldos = "select * from CajaSaldos";
            // Paso los datos a una tabla
            DataTable myTable = clsDataBD.GetSql(myCadenaSaldos);
            if (clsGlobales.ConB == null) { myTable = clsDataBD.GetSql(myCadenaSaldos); } else { myTable = clsDataBD.GetSqlB(myCadenaSaldos); }
            // recorro la tabla y paso los dato a las variables
            foreach (DataRow row in myTable.Rows)
            {
                dEfectivo = Convert.ToDouble(row["SaldoEfectivo"]);
            }
            
            txtEfectivoGral.Text = dEfectivo.ToString("#0.00");
            
            // COlor de las letras

            if (dEfectivo < 0)
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
            if (dgvCajaEfectivo.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para impresión!", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Data Set
            dsReportes oDsCaja = new dsReportes();

            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = dgvCajaEfectivo.Rows.Count;

            for (int i = 0; i < dgvFilas; i++)
            {
                oDsCaja.Tables["dtCaja"].Rows.Add
                (new object[] { dgvCajaEfectivo[0,i].Value.ToString(),
                dgvCajaEfectivo[1,i].Value.ToString(),
                dgvCajaEfectivo[2,i].Value.ToString(),
                dgvCajaEfectivo[3,i].Value.ToString(),
                dgvCajaEfectivo[4,i].Value.ToString(),
                dgvCajaEfectivo[5,i].Value.ToString() });

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
            oRepCaja.DataDefinition.FormulaFields["TipoCaja"].Text = "'" + " - DETALLE CAJA EFECTIVO " + "'";
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            txtFecha.Text = "";
            txtRazon.Text = "";
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
    }
}

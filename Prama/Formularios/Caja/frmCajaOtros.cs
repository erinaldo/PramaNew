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
    public partial class frmCajaOtros : Form
    {
        public frmCajaOtros()
        {
            InitializeComponent();
        }

        #region Método que carga la grilla

        private void CargarGrilla()
        {
            DataTable mDtTable = new DataTable();
            string myCadenaSQL = "";

            if (clsGlobales.ConB == null)
            {
                // Cadena SQL 
                myCadenaSQL = "select * from Vista_CajaOtros";

                mDtTable = clsDataBD.GetSql(myCadenaSQL);
            }
            else
            {
                // Cadena SQL 
                myCadenaSQL = "select * from Vista_CajaOtros2";

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

        private void frmCajaOtros_Load(object sender, EventArgs e)
        {
			
			//icon
            clsFormato.SetIconForm(this); 
			
            this.Text = clsGlobales.cFormato.getTituloVentana() + " - DETALLE DE LA CAJA OTROS ";

            CargarGrilla();
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
            // recorro la tabla y paso los dato a las variables
            foreach (DataRow row in myTable.Rows)
            {
                dEfectivo = Convert.ToDouble(row["SaldoOtros"]);
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

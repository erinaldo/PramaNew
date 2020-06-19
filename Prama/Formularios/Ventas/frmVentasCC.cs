using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prama.Formularios.Ventas
{
    public partial class frmVentasCC : Form
    {
        #region Variables del formulario

        // Variable que almacena la fila desde donde se hace la llamada
        int indexGrilla = 0;
        // Variable que almacena las cadenas SQL
        string myCadenaSQL = "";

        #endregion
        
        public frmVentasCC()
        {
            InitializeComponent();
        }

        private void frmVentasCC_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 
			
            CargarGrilla("","");
        }

        private void CargarGrilla(string sCampo, string sBuscar)
        {
            if (rdbTodos.Checked == true)
            {
                if (sBuscar == "")
                {
                    // Armo la cadena SQL para taer los datos de los proveedores
                    myCadenaSQL = "select * from Vista_Clientes";
                }
                else
                {
                    // Armo la cadena SQL para taer los datos de los proveedores
                    myCadenaSQL = "select * from Vista_Clientes where " + sCampo + " like '" + sBuscar + "%'";
                }
            }

            if (rdbDeudor.Checked == true)
            {
                if (sBuscar == "")
                {
                    // Armo la cadena SQL para taer los datos de los proveedores
                    myCadenaSQL = "select * from Vista_Proveedores where Saldo > 0";
                }
                else
                {
                    // Armo la cadena SQL para taer los datos de los proveedores
                    myCadenaSQL = "select * from Vista_Proveedores where Saldo > 0 and " + sCampo + " like '" + sBuscar + "%'";
                }
            }

            if (rdbAcreedor.Checked == true)
            {
                if (sBuscar == "")
                {
                    // Armo la cadena SQL para taer los datos de los proveedores
                    myCadenaSQL = "select * from Vista_Proveedores where Saldo < 0";
                }
                else
                {
                    // Armo la cadena SQL para taer los datos de los proveedores
                    myCadenaSQL = "select * from Vista_Proveedores where Saldo < 0 and " + sCampo + " like '" + sBuscar + "%'";
                }
            }

            // Ejecuto la consulta y paso los datos a una tabla
            DataTable myTablaClie = clsDataBD.GetSql(myCadenaSQL);
            // Evito que el dgv genere columnas automáticas
            dgvCLie.AutoGenerateColumns = false;
            // Asigno el source de la grilla de los proveedores a la nueva tabla
            dgvCLie.DataSource = myTablaClie;

            // Si la grilla de los proveedores no tiene dato y la de detalle sí, vacío la de detalle.
            if (dgvCLie.Rows.Count == 0 && dgvComprobantes.Rows.Count > 0)
            {
                dgvComprobantes.Rows.RemoveAt(0);
            }

            // Si está abierta la conexion B
            if (!(clsGlobales.ConB == null))
            {
                // Armo la nueva cadena
                string myCadenaSQLB = "select * from SaldoCliProv";
                // Creo una nueva tabla y le paso los datos de la consulta
                DataTable mySaldos = clsDataBD.GetSqlB(myCadenaSQLB);

                // Recorro la grilla
                foreach (DataGridViewRow row in dgvCLie.Rows)
                {
                    // Paso el Id del proveedor a una variable, lo mismo que su saldo
                    int IdClieA = Convert.ToInt32(row.Cells["IdCliente"].Value);
                    double SaldoA = Convert.ToDouble(row.Cells["SaldoClie"].Value);

                    // Recorro la tabla con los saldos de los proveedores en negro
                    foreach (DataRow rowB in mySaldos.Rows)
                    {
                        // Si existe el proveedor en esta tabla, sumo los saldos
                        if (IdClieA == Convert.ToInt32(rowB["idCliente"]))
                        {
                            SaldoA = SaldoA + Convert.ToDouble(rowB["SaldoProv"]);
                        }
                    }

                    row.Cells["SaldoTotal"].Value = SaldoA.ToString("#0.00");
                }

                // Oculto la columna del saldo en blanco y muestro la del acumulado
                dgvCLie.Columns["SaldoClie"].Visible = false;
                dgvCLie.Columns["SaldoTotal"].Visible = true;
            }

        }
            
    }
}

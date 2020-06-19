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
    public partial class frmBugsClientes : Form
    {
        public frmBugsClientes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sCadSQLCli = "select * from Clientes";
            int iIdCliente = 0;
            int iIdLocalidad = 0;
            int iIdProvincia = 0;
            int iContador = 0;
            DataTable myCliente = new DataTable();
            DataTable myLocalidad = new DataTable();

            // Le asigno al nuevo DataTable los datos de la consulta SQL
            myCliente = clsDataBD.GetSql(sCadSQLCli);
            

            foreach (DataRow row in myCliente.Rows)
            {
                iIdLocalidad = Convert.ToInt32(row["IdLocalidad"]);
                iIdCliente = Convert.ToInt32(row["IdCliente"]);

                sCadSQLCli = "select * from Localidades where IdLocalidad = " + iIdLocalidad;

                myLocalidad = clsDataBD.GetSql(sCadSQLCli);

                foreach (DataRow rowLoc in myLocalidad.Rows)
                {
                    if (Convert.ToInt32(rowLoc["IdLocalidad"]) == iIdLocalidad)
                    {
                        iIdProvincia = Convert.ToInt32(rowLoc["IdProvincia"]);

                        if(!(Convert.ToInt32(row["IdProvincia"])== iIdProvincia))
                        {
                            sCadSQLCli = "update Clientes set IdProvincia = " + iIdProvincia + " where IdCliente = " + iIdCliente;
                            clsDataBD.GetSql(sCadSQLCli);

                            iContador++;
                        }
                    }
                }

            }

            MessageBox.Show("Corregidos " + iContador + " Clientes", "RESULTADO", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }
    }
}

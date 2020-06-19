using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Prama
{
    public partial class frmSplash : Form
    {
       /* string myCadenaFecha;*/
        
        int PtoVta = 0;

        public frmSplash(int p_PtoVta)
        {
            InitializeComponent();
            //HABILITAR TIMER
            tmr.Enabled = true;
            PtoVta = p_PtoVta;

            clsGlobales.SqlCadConexion = ConfigurationManager.ConnectionStrings["myCadCon3"].ToString(); // Cadena PC Gabi
            //clsGlobales.SqlCadConexion = ConfigurationManager.ConnectionStrings["PramaSAS"].ToString();
            clsGlobales.SqlCadConexion2 = ConfigurationManager.ConnectionStrings["PramaSAS II"].ToString();
            clsGlobales.Con = new SqlConnection(clsGlobales.SqlCadConexion);
            
        }


        private void tmr_Tick(object sender, EventArgs e)
        {
            // String de la cadena SQL
            string sMyCadenaSQL = "select * from Usuarios";

            //Traer los presupuestos
            DataTable mDtTable = clsDataBD.GetSql(sMyCadenaSQL);
            
            if (mDtTable.Rows.Count > 0)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }
        }

        private void lblPramaBlack_DoubleClick(object sender, EventArgs e)
        {
            
            //Abrir conexion resguardo
            clsGlobales.ConB = new SqlConnection(clsGlobales.SqlCadConexion2);
            //No abrio?....
            if (!(clsGlobales.ConB == null))
            {
                MessageBox.Show("Se ha producido un error no controlado por el sistema. Consulta al Administrador!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Recuerde realizar un respaldo diario de la información!", "Tip del Dìa!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



    }
}

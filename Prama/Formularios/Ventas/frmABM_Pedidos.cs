using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prama
{
    public partial class frmABM_Pedidos : Form
    {
        public frmABM_Pedidos()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregarArt_Click(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 
			
          //abrimos el dialogo para poder obtener el nombre la ubicacion del archivo
            ofdAbrirArchivo.Filter = "Archivos Excel (.xls)|*.xls|All Files (*.*)|*.*";
            ofdAbrirArchivo.FilterIndex = 1;

           //Abrir cuadro de dialog
             ofdAbrirArchivo.ShowDialog();

           //Tomar el nombre del archivo 
             string sArchivo = ofdAbrirArchivo.FileName;

          //Leer Archivo xls
             clsGlobales.cFormato.LeerExcel(sArchivo);

         

        }
    }
}

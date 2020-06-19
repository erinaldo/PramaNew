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
    public partial class frmVentasFacturacionE : Form
    {
        public frmVentasFacturacionE()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmFacturacion_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 
			
            this.CargarToolTips();
			
            //Titulo
            this.Text = clsGlobales.cParametro.NombreFantasia + this.Text;
        }


        #region CargarToolTips

        private void CargarToolTips()
        {
            toolTip1.SetToolTip(this.btnAgregar, "Agregar");
            toolTip2.SetToolTip(this.btnQuitar, "Quitar");
            toolTip3.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip4.SetToolTip(this.btnSalir, "Salir!");
            toolTip4.SetToolTip(this.btnSearchClient, "Buscar Cliente!");
        }

        #endregion

        private void btnSearchClient_Click(object sender, EventArgs e)
        {
            frmBuscarCliente myForm = new frmBuscarCliente();
            myForm.ShowDialog();
        }
    }
}

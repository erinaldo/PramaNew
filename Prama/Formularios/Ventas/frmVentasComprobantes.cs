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
    public partial class frmVentasComprobantes : Form
    {
        public frmVentasComprobantes()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmVentasFacturacionE myForm = new frmVentasFacturacionE();
            myForm.ShowDialog();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            frmVentasFacturacionE myForm = new frmVentasFacturacionE();
            myForm.ShowDialog();
        }

        private void frmVentasComprobantes_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            //Titulo
            this.Text = clsGlobales.cParametro.NombreFantasia + this.Text;
        }

        
    }
}

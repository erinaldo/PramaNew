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
using Microsoft.Reporting.WinForms;

namespace Prama
{
    public partial class frmShowReports : Form
    {
        int p_CopiesToPrinter = 0;

        public frmShowReports(int CopiesToPrinter = 1)
        {
            InitializeComponent();

            p_CopiesToPrinter = CopiesToPrinter;
        }

        private void frmShowReports_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 
			
            this.rpvReportes.ReportSource = clsGlobales.myRptDoc;
            this.rpvReportes.Zoom(75);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            PrintDialog dialog1 = new PrintDialog();
            
            dialog1.AllowSomePages = true;
            dialog1.AllowPrintToFile = false;
            dialog1.PrinterSettings.Copies = (short)p_CopiesToPrinter;

            if (dialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int copies = dialog1.PrinterSettings.Copies;
                int fromPage = dialog1.PrinterSettings.FromPage;
                int toPage = dialog1.PrinterSettings.ToPage;
                bool collate = dialog1.PrinterSettings.Collate;

                clsGlobales.myRptDoc.PrintOptions.PrinterName = dialog1.PrinterSettings.PrinterName;
                clsGlobales.myRptDoc.PrintToPrinter(copies, collate, fromPage, toPage);
            }

            clsGlobales.myRptDoc.Dispose();
            dialog1.Dispose();
        }

        #region CargarToolTips

        private void CargarToolTips()
        {
            toolTip1.SetToolTip(this.btnSalir, "Salir");
            toolTip2.SetToolTip(this.btnImprimir, "Imprimir");
        }

        #endregion

    }
}

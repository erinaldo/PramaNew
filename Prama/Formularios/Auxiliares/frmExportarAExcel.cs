using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Prama.Clases;

namespace Prama.Formularios.Auxiliares
{
    public partial class frmExportarAExcel : Form
    {
        public frmExportarAExcel()
        {
            InitializeComponent();
        }
        
        #region Evento Load

        private void frmExportarAExcel_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            // Cargar ToolTips
            CargarToolTips();
            //Path por defecto
            //this.txtFolder.Text = Application.StartupPath + "\\";
            //Title
            this.Text = clsGlobales.cParametro.NombreFantasia + this.Text;        
        }

        #endregion
        
        #region Método que carga los ToolTips

        private void CargarToolTips()
        {
            toolTip1.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip2.SetToolTip(this.btnSalir, "Salir");
        }

        #endregion

        #region Metodo: CrearDirectorioDest

        private bool CrearDirectorioDest(string path)
        {
            bool bResult = false;

            try
            {
                if (!Directory.Exists(path))
                {
                    // Crear Directorio
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    //.T.
                    bResult = true;
                }
                else
                {
                    bResult = true;
                }
            }
            catch (IOException ioex)
            {
                //Mensaje y .F.
                MessageBoxTemporal.Show(ioex.Message + "- (Directorio inválido)","Error!",5,true);
                bResult = false;
            }
            //Retornar
            return bResult;
        }

        #endregion

        #region Boton Aceptar Evento Click

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            string sFolder = clsGlobales.sDestinoFs;

            //CAMBIAR PUNTERO MOUSE
            Cursor.Current = Cursors.WaitCursor;
            
          /*  bool bResult = false;
            
            //Crear directorio Destino, si todo .T. ok, sino Quit
           /* bResult = this.CrearDirectorioDest(sFolder);
            if (bResult == false) { return; }*/

            this.btnAceptar.Enabled = false;
            this.btnSalir.Enabled = false;

            //VERIFICAR QUE LISTADO SE IMPRIME
            if (this.rdbMostrador.Checked)
            {

             //Exportar a Excel
                clsGlobales.cFormato.ExportToExcel(sFolder, this.pBar, this.lblPorc, 0, lblCurrent);
            }
            else if (this.rdbRevendedor.Checked)
            {
              //Exportar a Excel
                clsGlobales.cFormato.ExportToExcel(sFolder, this.pBar, this.lblPorc, 1, lblCurrent);
 
            }
            else if (this.rdbRev10.Checked)
            {
                //Exportar a Excel
                clsGlobales.cFormato.ExportToExcel(sFolder, this.pBar, this.lblPorc, 1, lblCurrent, 4);

            }
            else if (this.rdbRev20.Checked)
            {
                //Exportar a Excel
                clsGlobales.cFormato.ExportToExcel(sFolder, this.pBar, this.lblPorc, 1, lblCurrent, 5);

            }
            else if (this.rdbDist.Checked)
            {
                //Exportar a Excel
                clsGlobales.cFormato.ExportToExcel(sFolder, this.pBar, this.lblPorc, 1, lblCurrent, 3);
            }
            else if (this.rdbAmbos.Checked)
            {
                //Exportar a Excel
                clsGlobales.cFormato.ExportToExcel(sFolder, this.pBar, this.lblPorc, 0, lblCurrent);
                clsGlobales.cFormato.ExportToExcel(sFolder, this.pBar, this.lblPorc, 1, lblCurrent);
                clsGlobales.cFormato.ExportToExcel(sFolder, this.pBar, this.lblPorc, 1, lblCurrent, 3);
                clsGlobales.cFormato.ExportToExcel(sFolder, this.pBar, this.lblPorc, 1, lblCurrent, 4);
                clsGlobales.cFormato.ExportToExcel(sFolder, this.pBar, this.lblPorc, 1, lblCurrent, 5);
                clsGlobales.cFormato.ExportToExcel(sFolder, this.pBar, this.lblPorc, 1, lblCurrent, 1);
                clsGlobales.cFormato.ExportToExcel(sFolder, this.pBar, this.lblPorc, 1, lblCurrent, 2);
            }
            else if (this.rdbLimitCdba.Checked)
            {
                //Exportar a Excel Limitrofe Cordoba
                clsGlobales.cFormato.ExportToExcel(sFolder, this.pBar, this.lblPorc, 1, lblCurrent, 1);
            }
            else if (this.rdbLimitLt.Checked)
            {
                //Exportar a Excel Limitrofe de Limitrofe
                clsGlobales.cFormato.ExportToExcel(sFolder, this.pBar, this.lblPorc, 1, lblCurrent, 2);
            }
            
            
            this.btnAceptar.Enabled = true;
            this.btnSalir.Enabled = true;

            //CAMBIAR PUNTERO MOUSE
            Cursor.Current = Cursors.Default;

            //Mensaje
            MessageBoxTemporal.Show("Los Archivos se han generado exitosamente!", "Información!", 3, true);

            //Cerrar
            this.Close();

        }

        #endregion

        #region Boton Salir Click

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}

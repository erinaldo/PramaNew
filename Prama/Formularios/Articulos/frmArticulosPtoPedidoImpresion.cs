using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prama.Formularios.Articulos
{
    public partial class frmArticulosPtoPedidoImpresion : Form
    {
        public frmArticulosPtoPedidoImpresion()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            clsGlobales.bBandera = false;
            // Cierro el formulario
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Reinicio las variables globales por si tienen datos;
            clsGlobales.FechaDesde = DateTime.Now;
            clsGlobales.FechaHasta = DateTime.Now;
            clsGlobales.EmpleadoFabricado = "";
            clsGlobales.IdEmpleadoFabricado = 0;
            // Paso los datos del formulario a las vaiables globales
            clsGlobales.FechaDesde = dtpDesde.Value;
            clsGlobales.FechaHasta = dtpHasta.Value;
            clsGlobales.EmpleadoFabricado = cboEmpleado.Text;
            // Paso los datos para el filtro
            if (chkGlobal.Checked)
            {
                clsGlobales.Individual = false;
            }
            else
            {
                clsGlobales.Individual = true;
            }
            if (chkCumplidos.Checked)
            {
                clsGlobales.Programa = false;
            }
            else
            {
                clsGlobales.Programa = true;
            }

            if (cboEmpleado.SelectedIndex != -1)
            {
                clsGlobales.IdEmpleadoFabricado = Convert.ToInt32(cboEmpleado.SelectedValue);
            }

            clsGlobales.bBandera = true;

            // Cierro el formulario
            this.Close();
        }

        private void frmArticulosPtoPedidoImpresion_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            // Cargo el combo con los empleados
            clsDataBD.CargarCombo(cboEmpleado, "Empleados", "Empleado", "IdEmpleado", "Activo=1");
            // Vacío la selección del combo
            cboEmpleado.SelectedIndex = -1;
            // Cargo los toolstips
            CargarToolTips();
        }

        private void chkCumplidos_CheckedChanged(object sender, EventArgs e)
        {
            /*
            if (chkCumplidos.Checked)
            {
                label3.Visible = true;
                cboEmpleado.Visible = true;
            }
            else
            {
                label3.Visible = false;
                cboEmpleado.Visible = false;
            }
            */
        }

        #region CargarToolTips

        private void CargarToolTips()
        {
            // Botones
            toolTip1.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip2.SetToolTip(this.btnCancelar, "Cancelar");
            // DTPs
            toolTip3.SetToolTip(this.dtpDesde, "Fecha inicial del filtro para el reporte");
            toolTip4.SetToolTip(this.dtpHasta, "Fecha final del filtro para el reporte");
            // Checkbox
            toolTip5.SetToolTip(this.chkGlobal, "Marque la casilla para el listado general");
            toolTip6.SetToolTip(this.chkCumplidos, "Marque la casilla para listar los productos fabricados");
            // CBO
            toolTip7.SetToolTip(this.cboEmpleado, "Despliegue y seleccione para ver solo un empleado");
        }

        #endregion
    }
}

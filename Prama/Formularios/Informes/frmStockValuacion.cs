using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prama.Formularios.Informes
{
    public partial class frmStockValuacion : Form
    {
        // Variable que controla los combos
        bool bYaCargado = false;
        
        
        public frmStockValuacion()
        {
            InitializeComponent();
        }

        private void frmStockValuacion_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            // Seteo el radio de los insumos
            rdbInsumos.Checked = true;
            // Cargo la grilla
            CargarGrilla(0,0,0);
            // Cargo los rubros
            CargarRubros();
            // Formulario ya cargado
            bYaCargado = true;
            //Titulo Ventana
            this.Text = clsGlobales.cFormato.getTituloVentana() + " - VALUACION STOCK";
        }

        #region Método que carga los datos a la grilla

        private void CargarGrilla(double val, int iIdRubro, int iIdSubRubro)
        {
            // Variables locales
            string myView = "";
            string sTipo = "";
            double dValuacion = 0;

            if (rdbInsumos.Checked)
            {
                //Setter
                myView = "Select * from Vista_ValuacionInsumos";
            }
            else
            {
                //Setter
                myView = "Select * from Vista_ValuacionProductos";
            }

            if (val != 0 || iIdRubro != 0 || iIdSubRubro != 0)
            {
                if (val > 0)
                {
                    myView += " where Valuacion > " + val;

                    if (iIdRubro > 0)
                    {
                        myView += " and IdRubroArticulo = " + iIdRubro;

                        if (iIdSubRubro > 0)
                        {
                            myView += " and IdSubrubroArticulo = " + iIdSubRubro;
                        }
                    }
                }
                else
                {
                    if (iIdRubro > 0)
                    {
                        myView += " where IdRubroArticulo = " + iIdRubro;

                        if (iIdSubRubro > 0)
                        {
                            myView += " and IdSubrubroArticulo = " + iIdSubRubro;
                        }
                    }
                }
            }

            //Datos a la tabla
            DataTable myTableVal = clsDataBD.GetSql(myView);
            // Datos a la grilla
            dgvData.AutoGenerateColumns = false;
            dgvData.DataSource = myTableVal;
            // Calculo la valuación total
            CalcularTotal();


        }

        #endregion

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            // Varuables de los combos
            double dVal = 0;
            int iRubro = 0;
            int iSubRubro = 0;
            
            if (string.IsNullOrEmpty(txtValuacion.Text) == false)
            {
                if (!(Convert.ToDouble(txtValuacion.Text) == 0))
                {
                    dVal= Convert.ToDouble(txtValuacion.Text);
                }
            }

            if (cboRubros.SelectedIndex != -1)
            {
                iRubro = Convert.ToInt32(cboRubros.SelectedValue);
            }

            if (cboSubRubros.SelectedIndex != 0)
            {
                iSubRubro = Convert.ToInt32(cboSubRubros.SelectedValue);
            }

            CargarGrilla(dVal, iRubro, iSubRubro);
            
        }

        private void rdbInsumos_CheckedChanged(object sender, EventArgs e)
        {
            ReiniciarFiltro();
            CargarGrilla(0, 0, 0);
        }

        private void rdbProductos_CheckedChanged(object sender, EventArgs e)
        {
            ReiniciarFiltro();
            CargarGrilla(0, 0, 0);

        }

        #region Método que reinicia el filtro

        private void ReiniciarFiltro()
        {
            cboRubros.SelectedIndex = -1;
            cboSubRubros.SelectedIndex = -1;

            cboSubRubros.Enabled = false;
            txtValuacion.Text = "0.00";
        }

        #endregion

        #region Método que calcula la valuación total

        private void CalcularTotal()
        {
            // Variable y acumuladores
            double dValuacion = 0;
            double dTotal = 0;
            
            // Recorro la grilla y sumo los montos de la valuación individual
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                dValuacion = Convert.ToDouble(row.Cells["Valuacion"].Value);
                dTotal += dValuacion;
            }

            txtTotal.Text = dTotal.ToString("###,###,##0.00");
        }

        #endregion

        #region Método que carga los combos

        private void CargarRubros()
        {
            // Cargo el combo de los puntos de venta
            clsDataBD.CargarCombo(cboRubros,"RubrosArticulos", "RubroArticulo", "IdRubroArticulo");
            // Dejo vacía la selección
            cboRubros.SelectedIndex = -1;
        }

        #endregion

        #region Método que carga los subrubros

        private void CargarSubRubros(int Rub)
        {
            // Cargo el combo de los puntos de venta
            clsDataBD.CargarCombo(cboSubRubros, "Vista_SubRubros_Rubros", "SubrubroArticulo", "IdSubrubroArticulo", "Activo = 1 and IdRubroArticulo = " + Rub);
            // Dejo vacía la selección
            cboSubRubros.SelectedIndex = -1;
        }

        #endregion

        private void cboRubros_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bYaCargado)
            {
                cboSubRubros.Enabled = true;
                CargarSubRubros(Convert.ToInt32(cboRubros.SelectedValue));
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string strTipoCli = "";

            //Hay datos?
            if (!(dgvData.Rows.Count > 0))
            {
                MessageBox.Show("No hay datos para imprimir para la consulta especificada!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //PREGUNTAR SI ESTA CONFIGURADO EN PARAMETROS
            if (clsGlobales.cParametro.Imprimir)
            {
                DialogResult dlResult = MessageBox.Show("¿Desea imprimir el Informe de Valuación de Stock?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Si confirma... cambiar estado
                if (dlResult == DialogResult.No)
                {
                    return;
                }
            }

            //Data Set
            dsReportes oDsValStock = new dsReportes();

            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = this.dgvData.Rows.Count;

            for (int i = 0; i < dgvFilas; i++)
            {
                oDsValStock.Tables["dtValStock"].Rows.Add
                (new object[] { dgvData[1,i].Value.ToString(),
                    dgvData[2,i].Value.ToString(), 
                    dgvData[7,i].Value.ToString(),
                    dgvData[8,i].Value.ToString(),
                    dgvData[9,i].Value.ToString()});
            }

            //Objeto Reporte
            rptValStock oRepValStock = new rptValStock();

            //Cargar Reporte                                    
            oRepValStock.Load(Application.StartupPath + "\\rptValStock.rpt");

            //Establecer el DataSet como DataSource
            oRepValStock.SetDataSource(oDsValStock);

            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepValStock;

            oRepValStock.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepValStock.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
            oRepValStock.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
            oRepValStock.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
            oRepValStock.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
            oRepValStock.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepValStock.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
            oRepValStock.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";
            oRepValStock.DataDefinition.FormulaFields["totalcant"].Text = "'" + Convert.ToDouble(this.txtValuacion.Text).ToString("###,###,##0.00") + "'";
            oRepValStock.DataDefinition.FormulaFields["total"].Text = "'" + this.txtTotal.Text + "'";

            oRepValStock.DataDefinition.FormulaFields["Rubro"].Text = "'" + cboRubros.Text + "'";
            oRepValStock.DataDefinition.FormulaFields["SubRubro"].Text = "'" + cboSubRubros.Text + "'";

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports(2);
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog(); 
        }

        private void txtValuacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 44)
            {
                e.KeyChar = Convert.ToChar(46);
                ch = e.KeyChar;

            }
            //PUNTO DECIMAL. N.
            if (ch == 46 && base.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            //NUMEROS. N.
            if (!char.IsDigit(ch) && ch != 8 && ch != 46 && ch != 13 && ch != 9)
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                btnAplicar.Focus();
            }
        }
    }
}

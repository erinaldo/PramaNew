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
    public partial class frmInformeIvaVentas : Form
    {
        #region Variables del formulario

        // Variable que almacena la cadena SQL
        string myCadenaSQL = "";

        // Declaro las variables que van a acumular los importes
        double dNeto = 0;
        double dNeto21 = 0;
        double dNeto10 = 0;
        double dIva105 = 0;
        double dIva210 = 0;
        double dExento = 0;
        double dTotalIvaVentas = 0;
        // Declaro las variables para las fechas
        int iMesActual = 0;
        int iAnoActual = 0;
        // Días del mes
        int iDiasMes;

        // Control de combos
        bool bCboCon = false;
        bool bCboPro = false;

        #endregion
        
        public frmInformeIvaVentas()
        {
            InitializeComponent();
        }

        private void frmInformeIvaVentas_Load(object sender, EventArgs e)
        {
            // Variable que almacena el mes en curso
            iMesActual = DateTime.Now.Month;
            
			// Variable que almacena el año en curso
            iAnoActual = DateTime.Now.Year;

			//icon
            clsFormato.SetIconForm(this); 
			
            // Seteo el DTP de fecha incial
            dtpDesde.Value = new DateTime(iAnoActual, iMesActual, 1);
            // Seteo el DTP de fecha Final
            if (!(iMesActual == 2))
            {
                dtpHasta.Value = new DateTime(iAnoActual, iMesActual, 30);
            }
            else
            {
                dtpHasta.Value = new DateTime(iAnoActual, iMesActual, 28);
            }

            clsDataBD.CargarCombo(cboCondicion, "TipoResponsables", "TipoResponsable", "IdTipoResponsable");
            clsDataBD.CargarCombo(cboProvincias, "Provincias", "Provincia", "IdProvincia");

            cboCondicion.SelectedIndex = -1;
            cboProvincias.SelectedIndex = -1;
            
            // Cargo la grilla
            CargarGrilla(0,0);

            this.Text = clsGlobales.cFormato.getTituloVentana() + " - IVA VENTAS";
        }

        #region Método que carga la grilla

        private void CargarGrilla(int iIdCond, int iIdPro)
        {
            myCadenaSQL = "";
            // Armo la fecha inicial
            string sFechaDesde = dtpDesde.Value.ToShortDateString();
            // Armo la fecha final
            string sFechaHasta = dtpHasta.Value.ToShortDateString();
            //DataTable 
            DataTable myIvaCompra;
            // Ejecuto la consulta y la paso a la grilla
            if (clsGlobales.ConB == null)
            {
                // Armo la cadena SQL
                myCadenaSQL = "select * from Vista_eFactura_IVAVta where Fecha >= '" + sFechaDesde
                            + "' and Fecha <= '" + sFechaHasta + "'";
                
                if (iIdCond == 0 & iIdPro == 0)
                {
                    
                }

                if (!(iIdCond == 0))
                {
                    myCadenaSQL += " and IdTipoResponsable = " + iIdCond;
                }

                if (!(iIdPro == 0))
                {
                    myCadenaSQL += " and IdProvincia = " + iIdPro; 
                }

                myCadenaSQL += " order by IdFactura";

                myIvaCompra = clsDataBD.GetSql(myCadenaSQL);
            }
            else
            {
                if (iIdCond == 0 & iIdPro == 0)
                {
                    // Armo la cadena SQL
                    myCadenaSQL = "select * from Vista_eFactura_IVAVta_2 where Fecha >= '" + sFechaDesde
                                + "' and Fecha <= '" + sFechaHasta ;
                }

                myCadenaSQL += "' order by IdFactura";

                myIvaCompra = clsDataBD.GetSqlB(myCadenaSQL);
            }
            dgvIvaVentas.AutoGenerateColumns = false;
            dgvIvaVentas.DataSource = myIvaCompra;
            // Calculo los totales
            CalcularTotales();
            // Paso los datos al pie
            PasarDatosPie();
        }

        #endregion

        #region Método que calcula los totales

        private void CalcularTotales()
        {
            // Reinicio las variables para los cálculos
            dNeto = 0;
            dIva105 = 0;
            dIva210 = 0;
            dExento = 0;
            dTotalIvaVentas = 0;
            // Recorro la grilla
            foreach (DataGridViewRow row in dgvIvaVentas.Rows)
            {
                dNeto = dNeto + Convert.ToDouble(row.Cells["Neto"].Value);
                dIva105 = dIva105 + Convert.ToDouble(row.Cells["Iva10"].Value);
                dIva210 = dIva210 + Convert.ToDouble(row.Cells["Iva21"].Value);
                dExento = dExento + Convert.ToDouble(row.Cells["Exento"].Value);
            }

            // Calculo el total general
            dTotalIvaVentas = dNeto +  dIva105 + dIva210 + dExento;
        }

        #endregion

        #region Método que pasa los datos de los acumuladores al pie del formulario

        private void PasarDatosPie()
        {
            // Paso los datos a los texbox del pie del formulario
            this.txtNeto.Text = dNeto.ToString("###,###,##0.00");
            this.txtIva105.Text = dIva105.ToString("###,###,##0.00");
            this.txtIva210.Text = dIva210.ToString("###,###,##0.00");
            this.txtExento.Text = dExento.ToString("###,###,##0.00");
            this.txtTotal.Text = dTotalIvaVentas.ToString("###,###,##0.00");
        }

        #endregion

        #region Método que devuleve el mes en texto

        private string MesATexto(int mes)
        {
            string aux = "";

            switch (mes)
            {
                case 1:
                    aux = "ENERO";
                    break;
                case 2:
                    aux = "FEBRERO";
                    break;
                case 3:
                    aux = "MARZO";
                    break;
                case 4:
                    aux = "ABRIL";
                    break;
                case 5:
                    aux = "MAYO";
                    break;
                case 6:
                    aux = "JUNIO";
                    break;
                case 7:
                    aux = "JULIO";
                    break;
                case 8:
                    aux = "AGOSTO";
                    break;
                case 9:
                    aux = "SEPTIEMBRE";
                    break;
                case 10:
                    aux = "OCTUBRE";
                    break;
                case 11:
                    aux = "NOVIEMBRE";
                    break;
                case 12:
                    aux = "DICIEMBRE";
                    break;
            }

            return aux;
        }

        #endregion

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            int iCboCon = Convert.ToInt32(cboCondicion.SelectedValue);
            int iCboPro = Convert.ToInt32(cboProvincias.SelectedValue);

            if (iCboCon == -1 & iCboPro == -1)
            {
                // Recargo la grilla
                CargarGrilla(0, 0);
            }

            if (iCboCon == -1 & !(iCboPro == -1))
            {
                // Recargo la grilla
                CargarGrilla(0, iCboPro);
            }

            if ((!(iCboCon == -1)) & iCboPro == -1)
            {
                // Recargo la grilla
                CargarGrilla(iCboCon, 0);
            }

            if((!(iCboCon == -1)) & (!(iCboPro == -1)))
            {
                CargarGrilla(iCboCon, iCboPro);
            }
            
        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            int iCboCon = Convert.ToInt32(cboCondicion.SelectedValue);
            int iCboPro = Convert.ToInt32(cboProvincias.SelectedValue);

            if (iCboCon == -1 & iCboPro == -1)
            {
                // Recargo la grilla
                CargarGrilla(0, 0);
            }

            if (iCboCon == -1 & !(iCboPro == -1))
            {
                // Recargo la grilla
                CargarGrilla(0, iCboPro);
            }

            if ((!(iCboCon == -1)) & iCboPro == -1)
            {
                // Recargo la grilla
                CargarGrilla(iCboCon, 0);
            }

            if ((!(iCboCon == -1)) & (!(iCboPro == -1)))
            {
                CargarGrilla(iCboCon, iCboPro);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            // Variable que toma el mes del DTP desde
            int iMesDesde = dtpDesde.Value.Month;
            //Data Set
            dsReportes oDsArt = new dsReportes();
            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = dgvIvaVentas.Rows.Count;

            for (int i = 0; i < dgvFilas; i++)
            {

                oDsArt.Tables["dtIvaVentas"].Rows.Add
                (new object[] 
                        { 
                            dgvIvaVentas["Fecha",i].Value.ToString(),
                            dgvIvaVentas["IdCliente",i].Value.ToString(),
                            dgvIvaVentas["RazonSocial",i].Value.ToString(),
                            dgvIvaVentas["CUIT",i].Value.ToString(),
                            dgvIvaVentas["Comprobante",i].Value.ToString(),
                            dgvIvaVentas["Numero",i].Value.ToString(),
                            dgvIvaVentas["Neto",i].Value.ToString(),
                            dgvIvaVentas["Iva10",i].Value.ToString(),
                            dgvIvaVentas["Iva21",i].Value.ToString(),
                            dgvIvaVentas["Exento",i].Value.ToString(),
                            dgvIvaVentas["Total",i].Value.ToString(),
                            dgvIvaVentas["Provincia",i].Value.ToString(),
                            dgvIvaVentas["TipoResponsable",i].Value.ToString(),
                        }
                );
            }

            //Objeto Reporte
            rptIvaVentas oRepArt = new rptIvaVentas();
            //Cargar Reporte            
            //    oRepTipoCli.Load(Application.StartupPath + "\\rptTipoCli.rpt");
            oRepArt.Load(Application.StartupPath + "\\rptIvaVentas.rpt");
            //Establecer el DataSet como DataSource
            oRepArt.SetDataSource(oDsArt);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepArt;

            oRepArt.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepArt.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
            oRepArt.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
            oRepArt.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
            oRepArt.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
            oRepArt.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepArt.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
            oRepArt.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";
            oRepArt.DataDefinition.FormulaFields["Neto"].Text = "'" + dNeto + "'";
            oRepArt.DataDefinition.FormulaFields["Iva105"].Text = "'" + dIva105 + "'";
            oRepArt.DataDefinition.FormulaFields["Iva210"].Text = "'" + dIva210 + "'";
            oRepArt.DataDefinition.FormulaFields["Exento"].Text = "'" + dExento + "'";
            oRepArt.DataDefinition.FormulaFields["Total"].Text = "'" + dTotalIvaVentas + "'";
            oRepArt.DataDefinition.FormulaFields["Mes"].Text = "'" + MesATexto(iMesDesde) + "'";
            oRepArt.DataDefinition.FormulaFields["Ano"].Text = "'" + iAnoActual + "'";

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();
        }

        private void rdbTodosCond_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbTodosCond.Checked)
            {
                cboCondicion.SelectedIndex = -1;
                cboCondicion.Enabled = false;
                
            }
        }

        private void rdbTodosPro_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbTodosPro.Checked)
            {
                cboProvincias.SelectedIndex = -1;
                cboProvincias.Enabled = false;
                
            }
        }

        private void cboCondicion_Click(object sender, EventArgs e)
        {
            bCboCon = true;
        }

        private void cboProvincias_Click(object sender, EventArgs e)
        {
            bCboPro = true;
        }

        private void cboCondicion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iIdCond = 0;
            int iIdProv = 0;
            
            if (bCboCon)
            {
                iIdCond = Convert.ToInt32(cboCondicion.SelectedValue);

                if (!(cboProvincias.SelectedIndex == -1))
                {
                    iIdProv = Convert.ToInt32(cboProvincias.SelectedValue);
                }
            }

            CargarGrilla(iIdCond, iIdProv);
        }

        private void cboProvincias_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iIdCond = 0;
            int iIdProv = 0;

            if (bCboPro)
            {
                iIdProv = Convert.ToInt32(cboProvincias.SelectedValue);


                if (!(cboCondicion.SelectedIndex == -1))
                {
                    iIdCond = Convert.ToInt32(cboCondicion.SelectedValue);
                }
            }

            CargarGrilla(iIdCond, iIdProv);
        }

        private void rdbFiltroCond_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbFiltroCond.Checked)
            {
                cboCondicion.Enabled = true;
            }
            
        }

        private void rdbFiltroPro_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbFiltroPro.Checked)
            {
                cboProvincias.Enabled = true;
            }
        }
    }
}

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
    public partial class frmInformeIvaCompras : Form
    {

        #region Variables del formulario

        // Variable que almacena la cadena SQL
        string myCadenaSQL = "";

        // Declaro las variables que van a acumular los importes
        double dNeto = 0;
        double dIva25 = 0;
        double dIva50 = 0;
        double dIva105 = 0;
        double dIva210 = 0;
        double dIva270 = 0;
        double dExento = 0;
        double dTotalIvaCompras = 0;
        // Declaro las variables para las fechas
        int iMesActual = 0;
        int iAnoActual = 0;
        // Días del mes
        int iDiasMes;
        
        #endregion

        #region Constructor del formulario

        public frmInformeIvaCompras()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos del formulario

        #region Eventos de los botones

        #region Evento Click del botón Salir

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Cierro el formulario
            this.Close();
        }

        #endregion

        #endregion

        #region Evento Load

        private void frmInformeIvaCompras_Load(object sender, EventArgs e)
        {
            // Variable que almacena el mes en curso
            iMesActual = DateTime.Now.Month;
            // Variable que almacena el año en curso
            iAnoActual = DateTime.Now.Year;
            
			//icon
            clsFormato.SetIconForm(this); 
			
            // Seteo el DTP de fecha incial
            dtpDesde.Value = new DateTime(iAnoActual, iMesActual, 1);
            // Seteo el DTP de fecha incial
            if (!(iMesActual == 2))
            {
                dtpHasta.Value = new DateTime(iAnoActual, iMesActual, 30);
            }
            else
            {
                dtpHasta.Value = new DateTime(iAnoActual, iMesActual, 28);
            }

            // Cargo la grilla
            CargarGrilla();

            //Titulo
            //Titulo Ventana
            this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;

        }

        #endregion

        #endregion 

        #region Métodos del Formulario

        #region Método que carga la grilla

        private void CargarGrilla()
        {
            // Armo la fecha inicial
            string sFechaDesde = dtpDesde.Value.ToShortDateString();
            // Armo la fecha final
            string sFechaHasta = dtpHasta.Value.ToShortDateString();
            // Armo la cadena SQL
            myCadenaSQL = "select * from Vista_ComprobantesCompras where FechaReal >= '" + sFechaDesde 
                        + "' and FechaReal <= '" + sFechaHasta + "'";
            // Ejecuto la consulta y la paso a la grilla
            DataTable myIvaCompra = clsDataBD.GetSql(myCadenaSQL);
            dgvIvaCompras.AutoGenerateColumns = false;
            dgvIvaCompras.DataSource = myIvaCompra;
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
            dIva25 = 0;
            dIva50 = 0;
            dIva105 = 0;
            dIva210 = 0;
            dIva270 = 0;
            dExento = 0;
            dTotalIvaCompras = 0;

            // Recorro la grilla
            foreach (DataGridViewRow row in dgvIvaCompras.Rows)
            {
                // Acumulo en las variables los importes de la columna de la grilla
                if (!(row.Cells["Neto"].Value==null))
                { dNeto = dNeto + Convert.ToDouble(row.Cells["Neto"].Value.ToString());}

                if (!(row.Cells["Iva25"].Value is DBNull))
                { dIva25 = dIva25 + Convert.ToDouble(row.Cells["Iva25"].Value);}

                if (!(row.Cells["Iva50"].Value is DBNull))
                { dIva50 = dIva50 + Convert.ToDouble(row.Cells["Iva50"].Value);}

                if (!(row.Cells["Iva105"].Value is DBNull))
                { dIva105 = dIva105 + Convert.ToDouble(row.Cells["Iva105"].Value);}

                if (!(row.Cells["Iva210"].Value is DBNull))
                { dIva210 = dIva210 + Convert.ToDouble(row.Cells["Iva210"].Value);}

                if (!(row.Cells["Iva270"].Value is DBNull))
                {dIva270 = dIva270 + Convert.ToDouble(row.Cells["Iva270"].Value);}

                if (!(row.Cells["Exento"].Value is DBNull))
                { dExento = dExento + Convert.ToDouble(row.Cells["Exento"].Value);}
            }

            // Calculo el total general
            dTotalIvaCompras = dNeto + dIva25 + dIva50 + dIva105 + dIva210 + dIva270 + dExento;
        }

        #endregion

        #region Método que pasa los datos de los acumuladores al pie del formulario

        private void PasarDatosPie()
        {
            // Paso los datos a los texbox del pie del formulario
            this.txtNeto.Text = dNeto.ToString("###,###,##0.00");
            this.txtIva25.Text = dIva25.ToString("###,###,##0.00");
            this.txtIva50.Text = dIva50.ToString("###,###,##0.00");
            this.txtIva105.Text = dIva105.ToString("###,###,##0.00");
            this.txtIva210.Text = dIva210.ToString("###,###,##0.00");
            this.txtIva270.Text = dIva270.ToString("###,###,##0.00");
            this.txtExento.Text = dExento.ToString("###,###,##0.00");
            this.txtTotal.Text = dTotalIvaCompras.ToString("###,###,##0.00");
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

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //Data Set
            dsReportes oDsArt = new dsReportes();
            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = dgvIvaCompras.Rows.Count;
            

            for (int i = 0; i < dgvFilas; i++)
            {
                
                oDsArt.Tables["dtIvaCompras"].Rows.Add
                (new object[] 
                        { 
                            dgvIvaCompras["Fecha",i].Value.ToString(),
                            dgvIvaCompras["IdProveedor",i].Value.ToString(),
                            dgvIvaCompras["Proveedor",i].Value.ToString(),
                            dgvIvaCompras["Numero",i].Value.ToString(),
                            dgvIvaCompras["Neto",i].Value.ToString(),
                            dgvIvaCompras["Iva25",i].Value.ToString(),
                            dgvIvaCompras["Iva50",i].Value.ToString(),
                            dgvIvaCompras["Iva105",i].Value.ToString(),
                            dgvIvaCompras["Iva210",i].Value.ToString(),
                            dgvIvaCompras["Iva270",i].Value.ToString(),
                            dgvIvaCompras["Exento",i].Value.ToString(),
                            dgvIvaCompras["Total",i].Value.ToString(),
                        }
                );
            }

            //Objeto Reporte
            rptIvaCompras oRepArt = new rptIvaCompras();
            //Cargar Reporte            
            //    oRepTipoCli.Load(Application.StartupPath + "\\rptTipoCli.rpt");
            oRepArt.Load(Application.StartupPath + "\\rptIvaCompras.rpt");
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
            oRepArt.DataDefinition.FormulaFields["Iva25"].Text = "'" + dIva25 + "'";
            oRepArt.DataDefinition.FormulaFields["Iva50"].Text = "'" + dIva50 + "'";
            oRepArt.DataDefinition.FormulaFields["Iva105"].Text = "'" + dIva105 + "'";
            oRepArt.DataDefinition.FormulaFields["Iva210"].Text = "'" + dIva210 + "'";
            oRepArt.DataDefinition.FormulaFields["Iva270"].Text = "'" + dIva270 + "'";
            oRepArt.DataDefinition.FormulaFields["Exento"].Text = "'" + dExento + "'";
            oRepArt.DataDefinition.FormulaFields["Total"].Text = "'" + dTotalIvaCompras + "'";
            oRepArt.DataDefinition.FormulaFields["Mes"].Text = "'" + MesATexto(iMesActual) + "'";
            oRepArt.DataDefinition.FormulaFields["Ano"].Text = "'" + iAnoActual + "'";

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();
        }

        #endregion

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            // Recargo la grilla
            CargarGrilla();
        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            // Recargo la grilla
            CargarGrilla();
        }
    }
}

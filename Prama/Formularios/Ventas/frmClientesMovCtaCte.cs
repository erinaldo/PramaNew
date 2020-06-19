using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prama.Formularios.Ventas
{
    public partial class frmClientesMovCtaCte : Form
    {
        int valIdCliente = 0;
        double valSaldo = 0;
        DataGridView val_dgvCli = null;

        public frmClientesMovCtaCte(int p_IdCliente, double p_Saldo, DataGridView p_DgvCli)
        {
            InitializeComponent();

            valIdCliente = p_IdCliente;
            val_dgvCli = p_DgvCli;

            if (!(clsGlobales.ConB == null))
            {
                if (Convert.ToDouble(val_dgvCli.CurrentRow.Cells["Saldo_N"].Value.ToString()) <= 0 || val_dgvCli.CurrentRow.Cells["Saldo_N"].Value.ToString() == null)
                {
                    valSaldo = 0;
                }
                else
                {
                    if (Convert.ToDouble(val_dgvCli.CurrentRow.Cells["AFavor_N"].Value.ToString()) < 0)
                    {
                        valSaldo = Convert.ToDouble(val_dgvCli.CurrentRow.Cells["Saldo_N"].Value.ToString()) + (Convert.ToDouble(val_dgvCli.CurrentRow.Cells["AFavor_N"].Value.ToString())); // + Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoAFB"].Value.ToString()));
                    }
                    else
                    {
                        valSaldo = Convert.ToDouble(val_dgvCli.CurrentRow.Cells["Saldo_N"].Value.ToString()) - (Convert.ToDouble(val_dgvCli.CurrentRow.Cells["AFavor_N"].Value.ToString())); // + Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoAFB"].Value.ToString()));
                    }
                }
            }
            else
            {
                valSaldo = Convert.ToDouble(val_dgvCli.CurrentRow.Cells["SaldoCli"].Value.ToString());
            }

            
        }

        private void frmClientesMovCtaCte_Load(object sender, EventArgs e)
        {
            //Inicio
			//icon
            clsFormato.SetIconForm(this); 			
            //Estado
            clsGlobales.myEstado = "C";
            //Tooltips
            CargarToolsTip();
            //Cargar Grilla
            CargarGrilla();
            //Datos Cliente
            this.lblCliData.Text = "Id Cliente: " + val_dgvCli.CurrentRow.Cells["IdCliente"].Value.ToString() + 
                                                  ",Razón Social: " + val_dgvCli.CurrentRow.Cells["RazonSocial"].Value.ToString() + 
                                                  ",CUIT: " + val_dgvCli.CurrentRow.Cells["CUIT"].Value.ToString() + 
                                                  ",IVA: " +  val_dgvCli.CurrentRow.Cells["CondicionIVA"].Value.ToString() + 
                                                  "TEL: " +  val_dgvCli.CurrentRow.Cells["Telefono"].Value.ToString(); 
            //Titulo
            this.Text = clsGlobales.cFormato.getTituloVentana() + " - CUENTAS CORRIENTES DE CLIENTES ";

            setearDTPs();
            CalcularMovimientos();
        }

        #region CargarGrilla()


        //Carga la grilla con los datos de movimientos en la cta cte del cliente
        private void CargarGrilla()
        {
            DataTable myData;

            if (clsGlobales.ConB == null)
            {
                myData = clsDataBD.GetSql("exec getCtaCteCliente " + this.valIdCliente);
            }
            else
            {
                myData = clsDataBD.GetSqlB("exec getCtaCteCliente2 " + this.valIdCliente); 
            }

            this.dvgMovimientos.AutoGenerateColumns = false;
            this.dvgMovimientos.DataSource = myData;

            //Calcular Saldo
            CalcularSaldo();

            //Mostrar Datos
            double cTotal = 0;

            //Verificar Conexion
             if (!(clsGlobales.ConB == null))
            {
                this.txtTotal.Text = Convert.ToDouble(val_dgvCli.CurrentRow.Cells["Saldo_N"].Value).ToString("###,###,##0.00");
                this.txtAFavor.Text = Convert.ToDouble(val_dgvCli.CurrentRow.Cells["AFavor_N"].Value).ToString("###,###,##0.00");

                cTotal = Convert.ToDouble(txtTotal.Text) - Convert.ToDouble(txtAFavor.Text);

                this.txtSaldo.Text = cTotal.ToString("###,###,##0.00");
            }
            else
            {
                this.txtTotal.Text = Convert.ToDouble(val_dgvCli.CurrentRow.Cells["SaldoCli"].Value).ToString("###,###,##0.00");
                this.txtAFavor.Text = Convert.ToDouble(val_dgvCli.CurrentRow.Cells["SaldoAFavor"].Value).ToString("###,###,##0.00");

                cTotal = Convert.ToDouble(txtTotal.Text) - Convert.ToDouble(txtAFavor.Text);

                this.txtSaldo.Text = cTotal.ToString("###,###,##0.00");
            }

            //Saldo
            //double dSaldoFin = 0;
             
            //this.txtTotal.Text = valSaldo.ToString("###,###,##0.00");
            
            //this.txtSaldo.Text = valSaldo.ToString("###,###,##0.00");

            //GetAFavorCliente(valIdCliente);

            /*if (Convert.ToDouble(txtAFavor.Text) < 0)
            {
                dSaldoFin = Convert.ToDouble(txtTotal.Text) + Convert.ToDouble(txtAFavor.Text);
            }
            else
            {
                dSaldoFin = Convert.ToDouble(txtTotal.Text) - Convert.ToDouble(txtAFavor.Text);
            }*/
            
           //Show
           //txtSaldo.Text = dSaldoFin.ToString("###,###,##0.00");

        }

        #endregion

        #region Método que carga los ToolsTip del formulario

        private void CargarToolsTip()
        {
            toolTip1.SetToolTip(this.btnImprimir, "Imprimir");
            toolTip2.SetToolTip(this.btnSalir, "Salir");

        }

        #endregion

        private void btnSalir_Click(object sender, EventArgs e)
        {
          //Cerrar
            this.Close();

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //No hay datos? mensaje y volver
            if (!(dvgMovimientos.Rows.Count > 0))
            {
                MessageBox.Show("El Cliente no posee movimientos, para imprimir, en su cuenta corriente", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            //PREGUNTAR SI ESTA CONFIGURADO EN PARAMETROS
            if (clsGlobales.cParametro.Imprimir)
            {
                DialogResult dlResult = MessageBox.Show("¿Desea imprimir el Detalle de Movimientos de la CtaCte del Cliente?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Si confirma... cambiar estado
                if (dlResult == DialogResult.No)
                {
                    return;
                }
            }

            //Data Set
            dsReportes oDsMovCtaCte = new dsReportes();

            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = this.dvgMovimientos.Rows.Count;

            for (int i = 0; i < dgvFilas; i++)
            {
                oDsMovCtaCte.Tables["dtMovCtaCte"].Rows.Add
                (new object[] { dvgMovimientos[0,i].Value.ToString(),
                    dvgMovimientos[2,i].Value.ToString(), 
                    dvgMovimientos[3,i].Value.ToString(),
                    dvgMovimientos[4,i].Value.ToString(),
                    Convert.ToDouble(dvgMovimientos[5,i].Value).ToString("###,###,##0.00"),
                    Convert.ToDouble(dvgMovimientos[6,i].Value).ToString("###,###,##0.00"),
                    Convert.ToDouble(dvgMovimientos[7,i].Value).ToString("###,###,##0.00")});
            }

            //Objeto Reporte
            rptMovCtaCteCli oRepMovCtaCteCli = new rptMovCtaCteCli();

            //Cargar Reporte                                    
            oRepMovCtaCteCli.Load(Application.StartupPath + "\\rptMovCtaCte.rpt");

            //Establecer el DataSet como DataSource
            oRepMovCtaCteCli.SetDataSource(oDsMovCtaCte);

            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepMovCtaCteCli;

            oRepMovCtaCteCli.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepMovCtaCteCli.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
            oRepMovCtaCteCli.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
            oRepMovCtaCteCli.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
            oRepMovCtaCteCli.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
            oRepMovCtaCteCli.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepMovCtaCteCli.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
            oRepMovCtaCteCli.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";

            //Data Cliente
            oRepMovCtaCteCli.DataDefinition.FormulaFields["Id"].Text = "' Código: " + val_dgvCli.CurrentRow.Cells["IdCliente"].Value.ToString() + "'";
            oRepMovCtaCteCli.DataDefinition.FormulaFields["Rs"].Text = "' Razón Social: " + val_dgvCli.CurrentRow.Cells["RazonSocial"].Value.ToString() + "'";
            oRepMovCtaCteCli.DataDefinition.FormulaFields["Condicion"].Text = "' Condición IVA: " + val_dgvCli.CurrentRow.Cells["CondicionIVA"].Value.ToString() + "'";
            oRepMovCtaCteCli.DataDefinition.FormulaFields["Telefono"].Text = "' Teléfono: " + val_dgvCli.CurrentRow.Cells["Telefono"].Value.ToString() + "'";
            oRepMovCtaCteCli.DataDefinition.FormulaFields["CUIT"].Text = "' CUIT: " + val_dgvCli.CurrentRow.Cells["CUIT"].Value.ToString() + "'";

            oRepMovCtaCteCli.DataDefinition.FormulaFields["total"].Text = "'" + this.txtSaldo.Text + "'";

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports(2);
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();
        }

        #region Método que calcula el saldo linea por linea de la grilla

        private void CalcularSaldo()
        {
            if (dvgMovimientos.Rows.Count > 0)
            {
                // Variables para el debe, haber y saldo
                double dDebe = 0;
                double dHaber = 0;
                double dSaldo = 0;

                // Recorro la grilla para actualizar el saldo linea por linea
                foreach (DataGridViewRow row in dvgMovimientos.Rows)
                {
                    dDebe = Convert.ToDouble(row.Cells["Debe"].Value);
                    dHaber = Convert.ToDouble(row.Cells["Haber"].Value);

                    if (row.Index == 0)
                    {
                        dSaldo = dDebe - dHaber;
                        row.Cells["Saldo"].Value = dSaldo.ToString("###,###,##0.00");
                    }
                    else
                    {
                        dSaldo = dSaldo + dDebe - dHaber;
                        row.Cells["Saldo"].Value = dSaldo.ToString("###,###,##0.00");
                    }

                }
            }
            
        }

        #endregion

        #region Método que devuelve el saldo a favor del cliente

        private void GetAFavorCliente(int IdClie)
        {

            if (clsGlobales.ConB == null)
            {
                string myCadena = "select SaldoAFavor from Clientes where IdCliente = " + IdClie;

                DataTable myTabla = clsDataBD.GetSql(myCadena);

                foreach (DataRow row in myTabla.Rows)
                {
                    txtAFavor.Text = (Convert.ToDouble(row["SaldoAFavor"])).ToString("#0.00");
                }
            }
            else
            {
         /*       string myCadena = "select AFavor_N from Clientes where IdCliente = " + IdClie;

                DataTable myTabla = clsDataBD.GetSqlB(myCadena);

                foreach (DataRow row in myTabla.Rows)
                {
                    txtAFavor.Text = (Convert.ToDouble(row["AFavor_N"])).ToString("#0.00");
                }*/

                txtAFavor.Text = Convert.ToDouble(val_dgvCli.CurrentRow.Cells["AFavor_N"].Value.ToString()).ToString("#0.00");
            }

        }

        #endregion

        #region Método que setea los DateTimePicker

        private void setearDTPs()
        {
            if (dvgMovimientos.Rows.Count > 0)
            {
                // Variable que almacenan la fecha del primer movimiento y del último
                DateTime dInicial = Convert.ToDateTime(DateTime.Now);
                DateTime dFinal = Convert.ToDateTime(DateTime.Now);
                
                foreach (DataGridViewRow row in dvgMovimientos.Rows)
                {
                    if (row.Index == 0)
                    {
                        dInicial = Convert.ToDateTime(row.Cells["FechaS"].Value);
                    }
                    else
                    {
                        dFinal = Convert.ToDateTime(row.Cells["FechaS"].Value);
                    }
                }

                dtpDesde.Value = dInicial;
                dtpHasta.Value = dFinal;
            }
            
        }

        #endregion

        #region Método que calcula los importes filtrados por fecha desde la grilla

        private void CalcularMovimientos()
        {
            double dVentas = 0;
            double dCobros = 0;
            double dSaldo = 0;

            if (dvgMovimientos.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dvgMovimientos.Rows)
                {
                    if (Convert.ToDateTime(row.Cells["FechaS"].Value) >= dtpDesde.Value && Convert.ToDateTime(row.Cells["FechaS"].Value) <= dtpHasta.Value)
                    {
                        dVentas += Convert.ToDouble(row.Cells["Debe"].Value);
                        dCobros += Convert.ToDouble(row.Cells["Haber"].Value);
                    }
                }

                dSaldo = dVentas - dCobros;
            }

            txtVendidoPer.Text = dVentas.ToString("###,###,##0.00");
            txtCobradoPer.Text = dCobros.ToString("###,###,##0.00");
            txtSaldoPer.Text = dSaldo.ToString("###,###,##0.00");
        }

        #endregion

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            CalcularMovimientos();
        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            CalcularMovimientos();
        }
    }
}

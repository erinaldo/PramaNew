using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prama.Formularios.Compras
{
    public partial class frmComprasCCMov : Form
    {

        int valIdProv = 0;
        double valSaldo = 0;
        DataGridView val_dgvProv = null;

        public frmComprasCCMov(int p_IdProv, double p_Saldo, DataGridView p_DgvProv)
        {
            InitializeComponent();

            valIdProv = p_IdProv;
            val_dgvProv = p_DgvProv;

            if (!(clsGlobales.ConB == null))
            {
                if (Convert.ToDouble(val_dgvProv.CurrentRow.Cells["Saldo_N"].Value.ToString()) <= 0 || val_dgvProv.CurrentRow.Cells["Saldo_N"].Value.ToString() == null)
                {
                    valSaldo = 0;
                }
                else
                {
                    if (Convert.ToDouble(val_dgvProv.CurrentRow.Cells["AFavor_N"].Value.ToString()) < 0)
                    {
                        valSaldo = Convert.ToDouble(val_dgvProv.CurrentRow.Cells["Saldo_N"].Value.ToString()) + (Convert.ToDouble(val_dgvProv.CurrentRow.Cells["AFavor_N"].Value.ToString())); // + Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoAFB"].Value.ToString()));
                    }
                    else
                    {
                        valSaldo = Convert.ToDouble(val_dgvProv.CurrentRow.Cells["Saldo_N"].Value.ToString()) - (Convert.ToDouble(val_dgvProv.CurrentRow.Cells["AFavor_N"].Value.ToString())); // + Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoAFB"].Value.ToString()));
                    }
                }
            }
            else
            {
                valSaldo = Convert.ToDouble(val_dgvProv.CurrentRow.Cells["Saldo_B"].Value.ToString());
            }
        }

        private void frmComprasCCMov_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            //Inicio
            //Estado
            clsGlobales.myEstado = "C";
            //Tooltips
            CargarToolsTip();
            //Cargar Grilla
            CargarGrilla();
            //Datos Cliente
            this.lblCliData.Text = "Razón Social: " + val_dgvProv.CurrentRow.Cells["RazonSocial"].Value.ToString() + ", CUIT: " +
                                                      val_dgvProv.CurrentRow.Cells["CUIT"].Value.ToString() + ", IVA: " +
                                                      val_dgvProv.CurrentRow.Cells["CondicionIVA"].Value.ToString() + ", TEL: " +
                                                      val_dgvProv.CurrentRow.Cells["Telefono"].Value.ToString() + ", ";
            //Titulo
            this.Text = clsGlobales.cFormato.getTituloVentana() + " - CUENTAS CORRIENTES DE PROVEEDORES ";

            setearDTPs();
            CalcularMovimientos();
        }

        #region Método que carga los ToolsTip del formulario

        private void CargarToolsTip()
        {
            toolTip1.SetToolTip(this.btnImprimir, "Imprimir");
            toolTip2.SetToolTip(this.btnSalir, "Salir");

        }

        #endregion

        #region Método que setea los DateTimePicker

        private void setearDTPs()
        {
            if (dvgMovimientos.Rows.Count > 0)
            {
                // Variable que almacenan la fecha del primer movimiento y del último
                DateTime dInicial = Convert.ToDateTime("01/01/1950");
                DateTime dFinal = Convert.ToDateTime("01/01/1950");

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
                if (dtpHasta.Value < dtpDesde.Value)
                {
                    dtpHasta.Value = dtpDesde.Value;
                }
   
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

        #region CargarGrilla

        //Carga la grilla con los datos de movimientos en la cta cte del cliente
        private void CargarGrilla()
        {
            DataTable myData;

            if (clsGlobales.ConB == null)
            {
                myData = clsDataBD.GetSql("exec getCtaCteProv " + this.valIdProv);
            }
            else
            {
                myData = clsDataBD.GetSqlB("exec getCtaCteProv2 " + this.valIdProv); 
            }

            this.dvgMovimientos.AutoGenerateColumns = false;
            this.dvgMovimientos.DataSource = myData;

            CalcularSaldo();

            //Saldo
            //double dSaldoFin = 0;
             
            //this.txtTotal.Text = valSaldo.ToString("###,###,##0.00");

            GetAFavorProveedor(valIdProv);

            //if (Convert.ToDouble(txtAFavor.Text) < 0)
            //{
            //    dSaldoFin = Convert.ToDouble(txtTotal.Text) + Convert.ToDouble(txtAFavor.Text);
            //}
            //else
            //{
            //    dSaldoFin = Convert.ToDouble(txtTotal.Text) - Convert.ToDouble(txtAFavor.Text);
            //}
            
            //Show
            //txtSaldo.Text = dSaldoFin.ToString("###,###,##0.00");

            double cTotal = 0;

            //Verificar Conexion
            if (!(clsGlobales.ConB == null))
            {
                this.txtTotal.Text = Convert.ToDouble(val_dgvProv.CurrentRow.Cells["Saldo_N"].Value).ToString("###,###,##0.00");
                this.txtAFavor.Text = Convert.ToDouble(val_dgvProv.CurrentRow.Cells["AFavor_N"].Value).ToString("###,###,##0.00");

                cTotal = Convert.ToDouble(txtTotal.Text) - Convert.ToDouble(txtAFavor.Text);

                this.txtSaldo.Text = cTotal.ToString("###,###,##0.00");
            }
            else
            {
                this.txtTotal.Text = Convert.ToDouble(val_dgvProv.CurrentRow.Cells["Saldo_B"].Value).ToString("###,###,##0.00");
                this.txtAFavor.Text = Convert.ToDouble(val_dgvProv.CurrentRow.Cells["AFavor_B"].Value).ToString("###,###,##0.00");

                cTotal = Convert.ToDouble(txtTotal.Text) - Convert.ToDouble(txtAFavor.Text);

                this.txtSaldo.Text = cTotal.ToString("###,###,##0.00");
            }

        }

        #endregion

        #region Método que devuelve el saldo a favor del cliente

        private void GetAFavorProveedor(int IdProv)
        {

            if (clsGlobales.ConB == null)
            {
                string myCadena = "select SaldoAFavor from Proveedores where IdProveedor = " + IdProv;

                DataTable myTabla = clsDataBD.GetSql(myCadena);

                foreach (DataRow row in myTabla.Rows)
                {
                    txtAFavor.Text = (Convert.ToDouble(row["SaldoAFavor"])).ToString("#0.00");
                }
            }
            else
            {
                txtAFavor.Text = Convert.ToDouble(val_dgvProv.CurrentRow.Cells["AFavor_N"].Value.ToString()).ToString("#0.00");
            }

        }

        #endregion

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
                        row.Cells["Saldo"].Value = dSaldo.ToString("#0,0.00");
                    }
                    else
                    {
                        dSaldo = dSaldo + dDebe - dHaber;
                        row.Cells["Saldo"].Value = dSaldo.ToString("#0,0.00");
                    }

                }
            }

        }

        #endregion

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

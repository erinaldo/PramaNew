using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prama.Formularios.Caja
{
    public partial class frmCajaMovimientos : Form
    {
        // Bandera que controla el comportamiento del método que pasa los datos a los controles
        bool bYaCargado = false;
        
        public frmCajaMovimientos()
        {
            InitializeComponent();
        }

        #region Método que carga la grilla

        private void CargarGrilla()
        {
            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            // Cadena SQL 
            string myCadena = "select * from Vista_Caja";

            if (clsGlobales.ConB == null)
            { mDtTable = clsDataBD.GetSql(myCadena); }
            else
            { mDtTable = clsDataBD.GetSqlB(myCadena);}
            
            // Evito que el dgv genere columnas automáticas
            dgvCaja.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvCaja.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvCaja.Rows.Count;
            // Posiciono la grilla en la última fila, si hay
            if (Filas > 0)
            {
                //Posicionamiento grilla
                int r = Filas - 1;
                int c = dgvCaja.CurrentCell.ColumnIndex;
                dgvCaja.CurrentCell = dgvCaja.Rows[r].Cells[c];
                // Paso los datos a los controles
                PasarDatosControles();
                // Método que calcula los totales de la caja
                CalcularTotales();
            }
        }

        #endregion

        #region Método para cargar las Imputaciones al combo

        private void CargarImputaciones()
        {
            // Cargo el combo de las provincias
            clsDataBD.CargarCombo(cboImputaciones, "CajaImputaciones", "Imputacion", "IdCajaImputacion");
            // Dejo vacío el combo
            cboImputaciones.SelectedIndex = -1;

        }
        #endregion

        #region Metodo: PasarDatosControles()
        
        private void PasarDatosControles()
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvCaja.RowCount == 0)
            {
                // this.LimpiarControlesForm();
                return;
            }
            else
            {
                //TRAER LA FILA ACTUAL. N.
                DataGridViewRow row = dgvCaja.CurrentRow;

                txtFecha.Text = row.Cells["FechaS"].Value.ToString();
                cboImputaciones.SelectedValue = Convert.ToInt32(row.Cells["IdCajaImputacion"].Value.ToString());

                txtDetalle.Text = row.Cells["Movimiento"].Value.ToString();
                txtComprobante.Text = row.Cells["Comprobante"].Value.ToString();

                txtTotal.Text = Convert.ToDouble(row.Cells["MontoTotal"].Value).ToString("#0.00");
                txtEfectivo.Text = Convert.ToDouble(row.Cells["Efectivo"].Value).ToString("#0.00");
                txtTransferencia.Text = Convert.ToDouble(row.Cells["Transferencia"].Value).ToString("#0.00");
                txtMercado.Text = Convert.ToDouble(row.Cells["MP"].Value).ToString("#0.00");
                txtContra.Text = Convert.ToDouble(row.Cells["Contra"].Value).ToString("#0.00");
                txtDebito.Text = Convert.ToDouble(row.Cells["Debito"].Value).ToString("#0.00");
                txtCredito.Text = Convert.ToDouble(row.Cells["Credito"].Value).ToString("#0.00");
                txtCheque.Text = Convert.ToDouble(row.Cells["Cheques"].Value).ToString("#0.00");
                txtBancos.Text = Convert.ToDouble(row.Cells["Bancos"].Value).ToString("#0.00");

            }
                        
        }

        #endregion

        #region Método que calcula los totales de la caja

        private void CalcularTotales()
        {
            // Variables del método
            double dTotal = 0;
            double dEfectivo = 0;
            double dTrans = 0;
            double dCheque = 0;
            double dMP = 0;
            double dDeb = 0;
            double dCred = 0;
            double dBco = 0;

            // Traigo los datos de la tabla que contiene los saldos de las cajas
            string myCadenaSaldos = "select * from CajaSaldos";
            // Paso los datos a una tabla
            DataTable myTable = clsDataBD.GetSql(myCadenaSaldos);
            if (clsGlobales.ConB == null)
            { myTable = clsDataBD.GetSql(myCadenaSaldos); }
            else
            { myTable = clsDataBD.GetSqlB(myCadenaSaldos); }
            // recorro la tabla y paso los dato a las variables
            foreach (DataRow row in myTable.Rows)
            {
                dEfectivo = Convert.ToDouble(row["SaldoEfectivo"]);
                dTrans = Convert.ToDouble(row["SaldoTransferencias"]);
                dCheque = Convert.ToDouble(row["SaldoCheques"]);
                dMP = Convert.ToDouble(row["SaldoMP"]);
                dDeb = Convert.ToDouble(row["SaldoDebito"]);
                dCred = Convert.ToDouble(row["SaldoCredito"]);
                dBco = Convert.ToDouble(row["SaldoBancos"]);

            }

            // Calculo el total general de las cajas
            dTotal = dEfectivo + dCheque + dBco;
            
            // Paso los datos de las variables al formulario
            txtTotalGral.Text = dTotal.ToString("#0.00");
            txtEfectivoGral.Text = dEfectivo.ToString("#0.00");
            txtTransferenciaGral.Text = dTrans.ToString("#0.00");
            txtChGral.Text = dCheque.ToString("#0.00");
            txtMPGeneral.Text = dMP.ToString("#0.00");
            txtDebitoGeneral.Text = dDeb.ToString("#0.00");
            txtCreditoGeneral.Text = dCred.ToString("#0.00");
            txtBancosGral.Text = dBco.ToString("#0.00");

            // COlor de las letras
            if (dTotal < 0)
            {
                txtTotalGral.ForeColor = Color.Red;
            }
            else
            {
                txtTotalGral.ForeColor = Color.Black;
            }

            if (dEfectivo < 0)
            {
                txtEfectivoGral.ForeColor = Color.Red;
            }
            else
            {
                txtEfectivoGral.ForeColor = Color.Black;
            }

            if (dTrans < 0)
            {
                txtTransferenciaGral.ForeColor = Color.Red;
            }
            else
            {
                txtTransferenciaGral.ForeColor = Color.Black;
            }

            if (dCheque < 0)
            {
                txtChGral.ForeColor = Color.Red;
            }
            else
            {
                txtChGral.ForeColor = Color.Black;
            }

            if (dMP < 0)
            {
                txtMPGeneral.ForeColor = Color.Red;
            }
            else
            {
                txtMPGeneral.ForeColor = Color.Black;
            }

            if (dBco < 0)
            {
                txtBancosGral.ForeColor = Color.Red;
            }
            else
            {
                txtBancosGral.ForeColor = Color.Black;
            }
        }

        #endregion

        #region Evento Load del formulario

        private void frmCajaMovimientos_Load(object sender, EventArgs e)
        {
			
			//icon
            clsFormato.SetIconForm(this); 
			
            this.Text = clsGlobales.cFormato.getTituloVentana() + " - MOVIMIENTOS DE CAJA ";
            // Cargo el combo
            CargarImputaciones();
            // Cargo los datos a la grilla
            CargarGrilla();
          
            if (txtBancos.Text=="")
            {
                this.btnTransfer.Enabled = false;
            }

            string sMySQL = "Select * from CajaCuentas";
            DataTable myTable = new DataTable();
            if (clsGlobales.ConB == null) { myTable = clsDataBD.GetSql(sMySQL); } else { myTable = clsDataBD.GetSqlB(sMySQL); }

            if (myTable.Rows.Count == 0)
            {
                this.btnTransfer.Enabled = false;
            }

            sMySQL = "Select * from CajaAsociacionesCuentas";
            myTable = new DataTable();
            if (clsGlobales.ConB == null) { myTable = clsDataBD.GetSql(sMySQL); } else { myTable = clsDataBD.GetSqlB(sMySQL); }

            if (myTable.Rows.Count == 0)
            {
                this.btnTransfer.Enabled = false;
            }


            if (!(clsGlobales.bView))
            {
                this.btnImprimir.Enabled = false;
                txtEfectivoGral.Visible = false;
                txtChGral.Visible = false;
                txtBancosGral.Visible = false;
                lblBancos.Visible = false;
                lblCheques.Visible = false;
                lblEfvo.Visible = false;
                lblTotal.Visible = false;
                txtTotalGral.Visible = false;
                btnTransfer.Visible = false;
            }
            else
            {
                btnTransfer.Visible = true; 
            }
        }

        #endregion

        #region Eventos de la grilla

        private void dgvCaja_Click(object sender, EventArgs e)
        {
            // Cambio el estado de la bandera
            bYaCargado = true;
            // Paso los datos de la fila a los controles
            PasarDatosControles();
        }

        private void dgvCaja_SelectionChanged(object sender, EventArgs e)
        {
            if (bYaCargado)
            {
                // Paso los datos de la fila a los controles
                PasarDatosControles();
            }
        }

        #endregion

        #region Eventos del botón Salir

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Evento Doble click de los textBox

        private void txtEfectivoGral_DoubleClick(object sender, EventArgs e)
        {
            frmCajaEfectivo myForm = new frmCajaEfectivo();
            myForm.ShowDialog();
        }

        #endregion

        #region Eventos KeyPress de los textBox

        // Solo están habilitadas las teclas Enter y Tab

        private void txtEfectivoGral_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (ch != 13 && ch != 9)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtTransferenciaGral_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (ch != 13 && ch != 9)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtChTercerosGral_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (ch != 13 && ch != 9)
            {
                e.Handled = true;
                return;
            }
        }

        private void txtOtrosGral_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (ch != 13 && ch != 9)
            {
                e.Handled = true;
                return;
            }
        }

        #endregion

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //Prama.Formularios.Caja.frmCajaABM myForm = new Formularios.Caja.frmCajaABM();
            //myForm.ShowDialog();
            //// Cargo el combo
            //CargarImputaciones();
            //// Cargo los datos a la grilla
            //CargarGrilla();

        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {

                    
            Prama.Formularios.Caja.frmCajaBancos myForm = new Formularios.Caja.frmCajaBancos(Convert.ToDouble(this.txtBancosGral.Text));
            myForm.ShowDialog();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

            //No hay datos, salir
            if (dgvCaja.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para impresión!", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            //Data Set
            dsReportes oDsCaja2 = new dsReportes();

            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = dgvCaja.Rows.Count;

            for (int i = 0; i < dgvFilas; i++)
            {
                oDsCaja2.Tables["dtCaja2"].Rows.Add
                (new object[] { dgvCaja["FechaS",i].Value.ToString(),
                dgvCaja["Imputacion",i].Value.ToString(),
                dgvCaja["Movimiento",i].Value.ToString(),
                dgvCaja["Comprobante",i].Value.ToString(),
                dgvCaja["MontoTotal",i].Value.ToString() });

            }

            //Objeto Reporte
            rptCaja2 oRepCaja2 = new rptCaja2();
            //Cargar Reporte            
            //    oRepTipoCli.Load(Application.StartupPath + "\\rptTipoCli.rpt");
            oRepCaja2.Load(Application.StartupPath + "\\rptCaja2.rpt");
            //Establecer el DataSet como DataSource
            oRepCaja2.SetDataSource(oDsCaja2);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepCaja2;
            oRepCaja2.DataDefinition.FormulaFields["TipoCaja"].Text = "'" + " DETALE DE CAJA GENERAL " + "'";
            oRepCaja2.DataDefinition.FormulaFields["Efvo"].Text = "'" + Convert.ToDouble(txtEfectivoGral.Text).ToString("0.00##") + "'";
            oRepCaja2.DataDefinition.FormulaFields["CH"].Text = "'" + Convert.ToDouble(txtChGral.Text).ToString("0.00##") + "'";
            oRepCaja2.DataDefinition.FormulaFields["Bco"].Text = "'" + Convert.ToDouble(txtBancos.Text).ToString("0.00##") + "'";
            oRepCaja2.DataDefinition.FormulaFields["Total"].Text = "'" + Convert.ToDouble(txtTotalGral.Text).ToString("0.00##") + "'";
            oRepCaja2.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepCaja2.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
            oRepCaja2.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
            oRepCaja2.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
            oRepCaja2.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
            oRepCaja2.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepCaja2.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
            oRepCaja2.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();
        }

        private void txtBancosGral_TextChanged(object sender, EventArgs e)
        {
            if (txtBancosGral.Text != "")
            {
                this.btnTransfer.Enabled = true;
            }

        }

        private void txtBancosGral_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (ch != 13 && ch != 9)
            {
                e.Handled = true;
                return;
            }
        }
        
    }
}

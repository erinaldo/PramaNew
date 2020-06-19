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
    public partial class frmVtasPendTransportes : Form
    {
        #region Variables del formulario

        int iIdTipoCliente = 0;
        string sTipoSaldo = "";
        int iIdTransporte = 0;
        int iIdCondicionCompra = 0;

        bool yaCargado = false;

        #endregion

        public frmVtasPendTransportes()
        {
            InitializeComponent();
        }

        private void frmVtasPendTransportes_Load(object sender, EventArgs e)
        {
            // Variable que almacena el mes en curso
            int iMesActual = DateTime.Now.Month;
            // Variable que almacena el año en curso
            int iAnoActual = DateTime.Now.Year;
			//icon
            clsFormato.SetIconForm(this); 
            // Seteo el DTP de fecha incial
            dtDesde.Value = new DateTime(iAnoActual, iMesActual, 1);
            // Seteo el DTP de fecha Final
            if (!(iMesActual == 2))
            {
                dtHasta.Value = new DateTime(iAnoActual, iMesActual, 30);
            }
            else
            {
                dtHasta.Value = new DateTime(iAnoActual, iMesActual, 28);
            }

            //Rubro
            setCargarCombos();
            //Clear
            cboTransportes.SelectedIndex = -1;
            //Grilla
            CargarGrilla();
            //Totalizar
            Totalizar();
            // Deshabilito el ordenamiento de las columnas
            DeshabilitarOrdenGrillas();

            //Titulo
            this.Text = clsGlobales.cFormato.getTituloVentana() + "  - DETALLE DE VENTAS POR TIPO CLIENTE, RUBRO-SUBRUBRO Y PERIODO ";
            yaCargado = true;
        }

        #region Metodo Totalizador

        private void Totalizar()
        {
            double Total = 0;
            double Pendiente = 0;
                
            foreach (DataGridViewRow row in this.dgvData.Rows)
            {
                Total += Convert.ToDouble(row.Cells["Total"].Value);
                Pendiente += Convert.ToDouble(row.Cells["Saldo"].Value);                                       
            }

            this.txtTotal.Text = Total.ToString("###,###,##0.00");
            this.txtPendiente.Text = Pendiente.ToString("###,###,##0.00"); 
        }

        #endregion

        #region Método para cargar Rubro

        private void setCargarCombos()
        {
            // Cargo el combo de las provincias
            clsDataBD.CargarCombo(cboTransportes, "Transportes", "RazonSocial", "IdTransporte", "Activo=1");
            // Dejo vacío el combo
            cboTransportes.SelectedIndex = -1;
            // Cargo el combo de las provincias
            clsDataBD.CargarCombo(cboFormaPago, "CondicionesCompra", "CondicionCompra", "IdCondicionCompra", "Activo=1");
            // Dejo vacío el combo
            cboFormaPago.SelectedIndex = -1;
            // Cargo el combo de las provincias
            clsDataBD.CargarCombo(cboProvincia, "Provincias", "Provincia", "IdProvincia");
            // Dejo vacío el combo
            cboProvincia.SelectedIndex = -1;

        }
        #endregion

        #region Método que carga los datos a la grilla

        private void CargarGrilla()
        {
            //Auxiliar String
            string myCadSQL = "";
            string myView = "";
            string myTot = "";

            //CAMBIAR PUNTERO MOUSE
            Cursor.Current = Cursors.WaitCursor;

            //Check Conn
            if (clsGlobales.ConB == null)
            {
                //Setter
                this.gpbOrigen.Visible = false;
                this.dgvData.Columns["Origen"].Visible = false;
                myView = "Vista_VtasPendTransportes";
            }
            else
            {
                //Setter
                this.gpbOrigen.Visible = true;
                this.dgvData.Columns["Origen"].Visible = true;
                myView = "Vista_VtasPendTransportes2";
            }

            //Filtro Tipo Vista ByN, N, B
            if (gpbOrigen.Visible)
            {
                if (this.rbtTodos.Checked)
                {
                    myView = "Vista_VtasPendTransportes2"; //ByN
                }
            }

            //Cad Connection
            myCadSQL = "select * from " + myView + " where Fecha >='" + this.dtDesde.Value.ToShortDateString() +
            "' and Fecha <= '" + this.dtHasta.Value.ToShortDateString() + "'";

            if (iIdTipoCliente != 0)
            {
                myCadSQL += " and IdTipoCliente = " + iIdTipoCliente;
            }

            //IdCliente N.
            if (string.IsNullOrEmpty(txtCodigoBs.Text) == false)
            {
                if (!(Convert.ToInt32(txtCodigoBs.Text)==0))
                {
                    myCadSQL += " and IdCliente = " + Convert.ToInt32(txtCodigoBs.Text);
                }
            }

            if (sTipoSaldo != "")
            {
                myCadSQL += " and " + sTipoSaldo; 
            }

            if (iIdTransporte != 0)
            {
                myCadSQL += " and IdTransporte = " + iIdTransporte;
            }

            if (iIdCondicionCompra != 0)
            {
                myCadSQL += " and IdCondicionCompra = " + iIdCondicionCompra;
            }

            if (gpbOrigen.Visible)
            {
                if (this.rbtN.Checked)
                {
                    myCadSQL += " and Origen = 'N'"; //N
                }
            }

            if (cboProvincia.SelectedIndex!=-1)
            {
                myCadSQL += " and IdProvincia = " + Convert.ToInt32(cboProvincia.SelectedValue);
            }

            myCadSQL += " order by Fecha";

            //Con
            DataTable myTable;

            if (clsGlobales.ConB == null)
            {
                myTable = clsDataBD.GetSql(myCadSQL);
            }
            else
            {
                myTable = clsDataBD.GetSqlB(myCadSQL);
            }
            this.dgvData.AutoGenerateColumns = false;
            dgvData.DataSource = myTable;

            // Convierto la fecha al formato D/M/Y
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Cells["FechaS"].Value = clsValida.ConvertirFecha(Convert.ToDateTime(row.Cells["Fecha"].Value));
            }

            //Totalizador
            Totalizar();

            //CAMBIAR PUNTERO MOUSE
            Cursor.Current = Cursors.Default;

        }

        #endregion

        private void dtDesde_ValueChanged(object sender, EventArgs e)
        {
            if (yaCargado)
            {
                CargarGrilla();
            }
        }

        private void dtHasta_ValueChanged(object sender, EventArgs e)
        {
            if (yaCargado)
            {
                CargarGrilla();
            }
        }

        private void rbnAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnAll.Checked)
            {
                iIdTipoCliente = 0;
            }

            if (yaCargado)
            {
                CargarGrilla();
            }
        }

        private void rbnPub_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnPub.Checked)
            {
                iIdTipoCliente = 28;
            }

            if (yaCargado)
            {
                CargarGrilla();
            }
        }

        private void rbnDist_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnDist.Checked)
            {
                iIdTipoCliente = 29;
            }

            if (yaCargado)
            {
                CargarGrilla();
            }
        }

        private void rbnRev_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnRev.Checked)
            {
                iIdTipoCliente = 30;
            }

            if (yaCargado)
            {
                CargarGrilla();
            }
        }

        private void rbnSaldosTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnSaldosTodos.Checked)
            {
                sTipoSaldo = "";
            }
            if (yaCargado)
            {
                CargarGrilla();
            }
        }

        private void rbnSaldosDeudor_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnSaldosDeudor.Checked)
            {
                sTipoSaldo = "Saldo > 0";
            }
            if (yaCargado)
            {
                CargarGrilla();
            }
        }

        private void rbnSaldosAFavor_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnSaldosAFavor.Checked)
            {
                sTipoSaldo = "SaldoAFavor < 0";
            }
            if (yaCargado)
            {
                CargarGrilla();
            }
        }

        private void rbnSaldoCancelados_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnSaldoCancelados.Checked)
            {
                sTipoSaldo = "Saldo = 0 and SaldoAFAvor = 0";
            }
            if (yaCargado)
            {
                CargarGrilla();
            }
        }

        private void cboTransportes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (yaCargado)
            {
                if (!(cboTransportes.SelectedIndex == -1))
                {
                    iIdTransporte = Convert.ToInt32(cboTransportes.SelectedValue);
                }
                else
                {
                    iIdTransporte = 0;
                }
                CargarGrilla();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cboTransportes.SelectedIndex = -1;
            cboFormaPago.SelectedIndex = -1;
        }

        #region Método que deshabilita el reordenamiento de las grilla desde sus cabeceras

        private void DeshabilitarOrdenGrillas()
        {
            // Deshabilito la reordenación de las columnas de las grillas
            foreach (DataGridViewColumn dgvCol in dgvData.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;

            }
        }

        #endregion

        private void txtCodigoBs_TextChanged(object sender, EventArgs e)
        {
            this.CargarGrilla();
        }

        private void rbtN_CheckedChanged(object sender, EventArgs e)
        {
            this.CargarGrilla();
        }

        private void rbtTodos_CheckedChanged(object sender, EventArgs e)
        {
            this.CargarGrilla();
        }

        private void cboFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (yaCargado)
            {
                if (!(cboFormaPago.SelectedIndex == -1))
                {
                    iIdCondicionCompra = Convert.ToInt32(cboFormaPago.SelectedValue);
                }
                else
                {
                    iIdCondicionCompra = 0;
                }
                CargarGrilla();
            }
        }

        private void cboProvincia_SelectedValueChanged(object sender, EventArgs e)
        {
            this.CargarGrilla();
        }
        
    }
}

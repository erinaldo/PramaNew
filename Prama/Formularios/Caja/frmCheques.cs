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
    public partial class frmCheques : Form
    {
        public frmCheques()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            //Validar Carga de Datos -----------------------
            if (txtBanco.Text == "")
            {
                MessageBox.Show("Por favor, complete el dato 'Banco'!", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBanco.Focus();
                return;
            }

            //Numero
            if (txtNumero.Text == "")
            {
                MessageBox.Show("Por favor, complete el dato 'Número'!", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumero.Focus();
                return;
            }
            else if (Convert.ToInt32(txtNumero.Text) == 0)
            {
                MessageBox.Show("El 'Número' de Cheque no puede ser 0!", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumero.Focus();
                return;
            }

            //Validar Fechas
            //Fecha Emision no puede ser mayor que la de cobro
            if (dtpEmision.Value > dtpCobro.Value)
            {
                MessageBox.Show("La 'Fecha de Emisión' no puede ser mayor a la de 'Fecha de Cobro'!", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpEmision.Focus();
                return;
            }
            //Fecha Cobro no puede ser menor a la de emision
            if (dtpCobro.Value < dtpEmision.Value)
            {
                MessageBox.Show("La 'Fecha de Cobro' no puede ser menor a la de 'Fecha de Emisión'!", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpCobro.Focus();
                return;
            }

            //La fecha de emisión no puede ser mayor a la fecha actual
            if (dtpEmision.Value > DateTime.Now)
            {
                MessageBox.Show("La 'Fecha de Emisión' no puede ser mayor a la actual!", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpEmision.Focus();
                return;
            }
            //------------------------------------

            //Importe Cheque
            if (txtImporte.Text == "")
            {
                MessageBox.Show("Por favor, complete el dato 'Importe Cheque'!", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtImporte.Focus();
                return;
            }
            else if (Convert.ToDouble(txtImporte.Text) == 0)
            {
                MessageBox.Show("El 'Importe' del Cheque debe ser mayor que 0!", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtImporte.Focus();
                return;
            }
            
            if (txtImporte.Text != "")
            {
                // Agrego una fila a la grilla
                dgvCheques.Rows.Add(1);
                // Cuento las filas de la grilla
                int filas = dgvCheques.Rows.Count;
                // Me posiciono en la nueva fila
                dgvCheques.CurrentCell = dgvCheques[1, filas - 1];
                // Paso a la variable del formulario el index de la fila actual
                int iFilaActual = dgvCheques.CurrentRow.Index;


                dgvCheques.CurrentRow.Cells["Numero"].Value = txtNumero.Text;
                dgvCheques.CurrentRow.Cells["FechaEmision"].Value = dtpEmision.Value.ToShortDateString();
                dgvCheques.CurrentRow.Cells["FechaCobro"].Value = dtpCobro.Value.ToShortDateString();
                dgvCheques.CurrentRow.Cells["Importe"].Value = Convert.ToDouble(txtImporte.Text).ToString("#0.00");
                //dgvCheques.CurrentRow.Cells["EnCartera"].Value = Convert.ToDouble(txtImporte.Text).ToString("#0.00");
                dgvCheques.CurrentRow.Cells["Banco"].Value = txtBanco.Text;

                CalcularTotal();

                txtBanco.Text = "";
                txtNumero.Text = "";
                dtpEmision.Value = DateTime.Now;
                dtpCobro.Value = DateTime.Now;

                txtImporte.Text = "";

                btnQuitar.Visible = true;
                //btnAgregar.Visible = false;
                btnAceptar.Enabled = true;

            }
            else
            {
                MessageBox.Show("Debe seleccionar una imputación e ingresar un importe", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //cboImputacion.Focus();
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgvCheques.Rows.Count > 0)
            {
                dgvCheques.Rows.Remove(dgvCheques.CurrentRow);

                if (dgvCheques.Rows.Count == 0)
                {
                    btnQuitar.Visible = false;
                    btnAgregar.Visible = true;
                    btnAceptar.Enabled = false;

                }
                else
                {
                    btnAceptar.Enabled = true;
                }

                CalcularTotal();

            }
            else
            {
                MessageBox.Show("No quedan filas para eliminar", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnQuitar.Visible = false;
                btnAceptar.Enabled = false;

                CalcularTotal();
            }
        }

        //CALCULAR TOTAL
        private void CalcularTotal()
        {
            double dSubtotal = 0;

            foreach (DataGridViewRow row in dgvCheques.Rows)
            {
                dSubtotal += Convert.ToDouble(row.Cells["Importe"].Value);
            }

            txtTotal.Text = dSubtotal.ToString("0.00##");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //ELIMINAR TODOs LOS MOVIMIENTOS SEGUN TIPO Y USUARIO
            string sSQLCad = "DELETE FROM Temporal_DetalleCheques Where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario;
            if (clsGlobales.ConB == null) { clsDataBD.GetSql(sSQLCad); } else { clsDataBD.GetSqlB(sSQLCad); }

            //TOTAL ACREDITAR 0
            clsGlobales.dTotalAAcreditar = 0;

            //CERRAR FORMULARIO
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

           

            //Hay datos, guardar y salir    
            if (!(dgvCheques.Rows.Count == 0))
            {
                clsGlobales.dTotalAAcreditar = Convert.ToDouble(txtTotal.Text);
                GrabarTemporal();
                this.Close();
            }
            else
            {
                return;
            }


        }

        //METODO QUE GRABA EN TABLA TEMPORAL G.
        private void GrabarTemporal()
        {

            //Eliminar registros cargados para ese usuario y tipo de movimiento
            if (clsGlobales.ConB == null)
            {
                clsDataBD.GetSql("Delete from Temporal_DetalleCheques Where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario);
            }
            else
            {
                clsDataBD.GetSqlB("Delete from Temporal_DetalleCheques Where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario);
            }
            //Volver a guardar N. 08-11
            foreach (DataGridViewRow row in dgvCheques.Rows)
            {
                int iNumero = Convert.ToInt32(row.Cells["Numero"].Value);
                string sFechaEmision = row.Cells["FechaEmision"].Value.ToString();
                string sFechaCobro = row.Cells["FechaCobro"].Value.ToString();
                double dImporte = Convert.ToDouble(row.Cells["Importe"].Value);
                string sBanco = row.Cells["Banco"].Value.ToString();

                string sMyCadenaSQL = "insert into Temporal_DetalleCheques (Numero, FechaEmision, FechaCobro, Importe, Activo, Banco, EnCartera, IdUsuario) values (" +
                                        iNumero + ", '" +
                                        sFechaEmision + "', '" +
                                        sFechaCobro + "', " +
                                        dImporte + ", 1, '" +
                                        sBanco + "', 1 , " +
                                        clsGlobales.UsuarioLogueado.IdUsuario + ")";

                //Controlar tipo de conexion
                if (clsGlobales.ConB == null)
                {
                    clsDataBD.GetSql(sMyCadenaSQL);
                }
                else
                {
                    clsDataBD.GetSqlB(sMyCadenaSQL);
                }
            }

        }

        private void frmCheques_Load(object sender, EventArgs e)
        {
            //string sWhereCombo = "";
            string sMyCadenaSQL = "";

			//icon
            clsFormato.SetIconForm(this); 
			
            //Recuperar data temporal
            sMyCadenaSQL = "select * from Temporal_DetalleCheques where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario ;
            // Traigo los datos de la tabla que contiene los saldos de las cajas
            DataTable myTable = new DataTable();
            if (clsGlobales.ConB == null) { myTable = clsDataBD.GetSql(sMyCadenaSQL); } else { myTable=clsDataBD.GetSqlB(sMyCadenaSQL); }
            //dgvImputacion.DataSource = myTable;

            int filas = 0;
            double total = 0;

            //VER SI HAY DATOS
            if (myTable.Rows.Count > 0)
            {
                foreach (DataRow myRow in myTable.Rows)
                {
                    dgvCheques.Rows.Add();

                    // Cuento las filas de la grilla
                    filas = dgvCheques.Rows.Count;

                    // Si la grilla no está vacía
                    if (filas > 0)
                    {
                        // Posiciono la grilla en la última fila
                        dgvCheques.CurrentCell = dgvCheques[1, filas - 1];
                    }

                    //Sumatoria Total
                    total += Convert.ToDouble(myRow["Importe"].ToString());

                    dgvCheques.CurrentRow.Cells["Numero"].Value = myRow["Numero"].ToString();
                    dgvCheques.CurrentRow.Cells["FechaEmision"].Value = myRow["FechaEmision"].ToString();
                    dgvCheques.CurrentRow.Cells["FechaCobro"].Value = myRow["FechaCobro"].ToString();
                    dgvCheques.CurrentRow.Cells["Importe"].Value = Convert.ToDouble(myRow["Importe"]).ToString("#0.00");

                    dgvCheques.CurrentRow.Cells["Banco"].Value = myRow["Banco"].ToString();
                   
                }

                txtTotal.Text = total.ToString("#0.00");
            }

            //Controlar botones
            if (dgvCheques.Rows.Count > 0)
            {
                this.btnAgregar.Visible = false;
                this.btnQuitar.Visible = true;
                btnAceptar.Enabled = true;
            }
            else
            {
                btnAceptar.Enabled = false;
            }
        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            string senderText = (sender as TextBox).Text;
            string senderName = (sender as TextBox).Name;
            string[] splitByDecimal = senderText.Split('.');
            int cursorPosition = (sender as TextBox).SelectionStart;

            char ch = e.KeyChar;

            if (ch == 44)
            {
                e.KeyChar = Convert.ToChar(46);
                ch = e.KeyChar;

            }
            //PUNTO DECIMAL. N.
            if (ch == 46 && senderText.IndexOf('.') != -1)
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

            //CONTROLAR CANTIDAD DE DECIMALES LUEGO DEL SEPARADOR DECIMAL. N.
            if (!char.IsControl(e.KeyChar)
                && senderText.IndexOf('.') < cursorPosition
                && splitByDecimal.Length > 1
                && splitByDecimal[1].Length == 2)
            {
                e.Handled = true;
            }
        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Sólo se permiten números!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                txtNumero.Focus();
                return;
            }
        }

        private void txtBanco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Sólo se permiten letras!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                txtBanco.Focus();
                return;
            }
        }

        private void txtTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}

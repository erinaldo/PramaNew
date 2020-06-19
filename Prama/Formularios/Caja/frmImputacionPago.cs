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
    public partial class frmImputacionPago : Form
    {
        // Par'ametro de entrada
        // 1 Transferencias
        // 2 Mercado Pago
        // 3 Débito
        // 4 Credito
        
        
        // Parámetro que indica el tio de movimiento de caja que hay que hacer
        int iTipoMovimiento = 0;
        
        
        public frmImputacionPago(int iTipoMov, string sTitulo)
        {
            InitializeComponent();
            iTipoMovimiento = iTipoMov;
            this.Text = sTitulo;
        }

        private void frmImputacionPago_Load(object sender, EventArgs e)
        {
            string sWhereCombo = "";
            string sMyCadenaSQL = "";

			//icon
            clsFormato.SetIconForm(this); 
			
            switch (iTipoMovimiento)
            {
                case 1: // Transferencias

                    sWhereCombo = "Transferencias = 1";
                    this.txtOp.Enabled = false;
                    this.txtOp.TabStop = false;
                    this.txtTarjeta.Enabled = false;
                    this.txtTarjeta.TabStop = false;

                    break;
                case 2: // Mercado Pago

                    sWhereCombo = "MercadoPago = 1";
                    this.txtOp.Enabled = true;
                    this.txtOp.TabStop = true;
                    this.txtTarjeta.Enabled = true;
                    this.txtTarjeta.TabStop = true;
                    break;
                case 3: // Débito

                    sWhereCombo = "Debito = 1";
                    this.txtOp.Enabled = false;
                    this.txtOp.TabStop = false;
                    this.txtTarjeta.Enabled = false;
                    this.txtTarjeta.TabStop = false;
                    break;
                case 4: // Crédito

                    sWhereCombo = "Credito = 1";
                    this.txtOp.Enabled = true;
                    this.txtOp.TabStop = true;
                    this.txtTarjeta.Enabled = true;
                    this.txtTarjeta.TabStop = true;
                    break;
            }

            //Cargar Combo
            clsDataBD.CargarCombo(cboImputacion, "CajaAsociacionesCuentas", "CajaAsociaciones", "IdCajaAsociaciones", sWhereCombo, 1);
            cboImputacion.SelectedIndex = -1;

            //Recuperar data temporal
            sMyCadenaSQL = "select TD.*,CC.CajaAsociaciones from Temporal_DetallePagoCaja As TD, CajaAsociacionesCuentas AS CC where TD.TipoMov = " + iTipoMovimiento +
            " and TD.IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario + " AND CC.IdCajaAsociaciones = TD.IdCajaAsociaciones";
            
            // Traigo los datos de la tabla que contiene los saldos de las cajas
            DataTable myTable;
            if (clsGlobales.ConB == null) 
            { myTable = clsDataBD.GetSql(sMyCadenaSQL); }
            else
            { myTable = clsDataBD.GetSqlB(sMyCadenaSQL); }

            //dgvImputacion.DataSource = myTable;

            int filas = 0;
            double total = 0;

            //VER SI HAY DATOS
            if (myTable.Rows.Count >0)
            {
                foreach (DataRow myRow in myTable.Rows)
                {
                   dgvImputacion.Rows.Add();

                   // Cuento las filas de la grilla
                   filas = dgvImputacion.Rows.Count;

                   // Si la grilla no está vacía
                   if (filas > 0)
                   {
                       // Posiciono la grilla en la última fila
                       dgvImputacion.CurrentCell = dgvImputacion[1, filas - 1];
                   }

                   //Sumatoria Total
                   total+=Convert.ToDouble(myRow["Importe"].ToString());

                   dgvImputacion.CurrentRow.Cells["IdCajaAsociaciones"].Value = Convert.ToDouble(myRow["IdCajaAsociaciones"].ToString());
                   dgvImputacion.CurrentRow.Cells["CajaAsociaciones"].Value =  myRow["CajaAsociaciones"].ToString();
                   dgvImputacion.CurrentRow.Cells["Importe"].Value = Convert.ToDouble(myRow["Importe"].ToString());
                   dgvImputacion.CurrentRow.Cells["Cupon"].Value =  myRow["Cupon"].ToString();
                   dgvImputacion.CurrentRow.Cells["Tarjeta"].Value = myRow["Tarjeta"].ToString();
                }

                txtTotal.Text = total.ToString("0.00##");
            }

            //Controlar botones
            if (dgvImputacion.Rows.Count > 0)
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //Validar que se carguen los datos correspondientes
            if (txtImporte.Text != "" && cboImputacion.SelectedIndex != -1)
            {
                //Verificar cupon y tarjeta
                if (iTipoMovimiento == 2 || iTipoMovimiento == 4)
                {
                    if (txtOp.Text == "")
                    {
                        MessageBox.Show("Debe completar el Dato 'N° de Cupón' de la operación!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                        txtOp.Focus();
                        return;
                    }

                    if (txtTarjeta.Text == "")
                    {
                        MessageBox.Show("Debe completar el Dato de la 'Tarjeta' con la que realizó la operación!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtOp.Focus();
                        return;
                    }
                }

                //Validar Importe Mayor a 0
                if (Convert.ToDouble(txtImporte.Text) == 0)
                {
                    MessageBox.Show("Por favor, ingrese un importe mayor a 0!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtImporte.Text = "";
                    txtImporte.Focus();
                    return;
                }

                // Agrego una fila a la grilla
                dgvImputacion.Rows.Add(1);
                // Cuento las filas de la grilla
                int filas = dgvImputacion.Rows.Count;
                // Me posiciono en la nueva fila
                dgvImputacion.CurrentCell = dgvImputacion[1, filas - 1];
                // Paso a la variable del formulario el index de la fila actual
                int iFilaActual = dgvImputacion.CurrentRow.Index;


                dgvImputacion.CurrentRow.Cells["IdCajaAsociaciones"].Value = cboImputacion.SelectedValue.ToString();
                dgvImputacion.CurrentRow.Cells["CajaAsociaciones"].Value = cboImputacion.Text;
                dgvImputacion.CurrentRow.Cells["Importe"].Value = Convert.ToDouble(txtImporte.Text).ToString("#0.00");
                dgvImputacion.CurrentRow.Cells["Cupon"].Value = txtOp.Text;
                dgvImputacion.CurrentRow.Cells["Tarjeta"].Value = txtTarjeta.Text;

                double dSubtotal = 0;

                foreach (DataGridViewRow row in dgvImputacion.Rows)
                {
                    dSubtotal += Convert.ToDouble(row.Cells["Importe"].Value);
                }

                txtImporte.Text = "";
                txtOp.Text = "";
                txtTarjeta.Text = "";
                txtTotal.Text = dSubtotal.ToString("#0.00");
                
                btnQuitar.Visible = true;
                btnAgregar.Visible = false;
                btnAceptar.Enabled = true;

            }
            else
            {
                MessageBox.Show("Debe seleccionar una imputación e ingresar un importe", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboImputacion.Focus();
            }
        }

        //CALCULAR TOTAL
        private void CalcularTotal()
        {
            double dSubtotal = 0;

            foreach (DataGridViewRow row in dgvImputacion.Rows)
            {
                dSubtotal += Convert.ToDouble(row.Cells["Importe"].Value);
            }

            txtTotal.Text = dSubtotal.ToString("0.00##");
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
        

         //vALIDAR     
            if (!(dgvImputacion.Rows.Count == 0))
            {
                clsGlobales.dTotalAAcreditar = Convert.ToDouble(txtTotal.Text);
                GrabarTemporal();
                this.Close();
            }
            else
            {
                MessageBox.Show("No hay datos cargados! Verifique!", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //ELIMINAR TODOs LOS MOVIMIENTOS SEGUN TIPO Y USUARIO
                string sSQLCad = "DELETE FROM Temporal_DetallePagoCaja Where TipoMov = " + iTipoMovimiento + " AND IDUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario;
                if (clsGlobales.ConB == null) { clsDataBD.GetSql(sSQLCad); } else { clsDataBD.GetSqlB(sSQLCad); }
            
            //TOTAL ACREDITAR 0
                clsGlobales.dTotalAAcreditar = 0;         
            
            //CERRAR FORMULARIO
                this.Close();
            
        }

        private void txtImporte_Leave(object sender, EventArgs e)
        {
            try
            {
                double dValidarDato = Convert.ToDouble(txtImporte.Text);
            }
            catch
            {
                MessageBox.Show("Debe ingresar un importe válido", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgvImputacion.Rows.Count > 0)
            {
                dgvImputacion.Rows.Remove(dgvImputacion.CurrentRow);

                if (dgvImputacion.Rows.Count == 0)
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

        private void GrabarTemporal()
        {
            
            //Eliminar registros cargados para ese usuario y tipo de movimiento
            if (clsGlobales.ConB == null)
            { clsDataBD.GetSql("Delete from Temporal_DetallePagoCaja Where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario + " AND TipoMov = " + iTipoMovimiento); } 
            else
            { clsDataBD.GetSqlB("Delete from Temporal_DetallePagoCaja Where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario + " AND TipoMov = " + iTipoMovimiento); }
            //Volver a guardar N. 08-11
            foreach (DataGridViewRow row in dgvImputacion.Rows)
            {
                int iId = Convert.ToInt32(row.Cells["IdCajaAsociaciones"].Value);
                double dImporte = Convert.ToDouble(row.Cells["Importe"].Value);

                string Op = "";
                string Tj = "";

                //Cadena vacia
                if (iTipoMovimiento == 2 || iTipoMovimiento == 4)
                {
                    Op = row.Cells["Cupon"].Value.ToString();
                    Tj = row.Cells["Tarjeta"].Value.ToString();
                }

                string sMyCadenaSQL = "insert into Temporal_DetallePagoCaja (IdUsuario, IdCajaAsociaciones, Importe, TipoMov, Cupon, Tarjeta) Values (" +
                                clsGlobales.UsuarioLogueado.IdUsuario + ", " +
                                iId + ", " +
                                dImporte + ", " +
                                iTipoMovimiento + ",'" +
                                Op + "','" +
                                Tj + "')";

                //Controlar tipo de conexionfTemporal_DetallePagoCaja
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Space))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}

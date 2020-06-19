using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prama.Formularios.Auxiliares
{
    public partial class frmCalculoEnvio : Form
    {
        TextBox AuxTextBox;

        public frmCalculoEnvio(TextBox myTextBox = null)
        {
            InitializeComponent();

            AuxTextBox = myTextBox;
        }

        #region Evento Load

        private void frmCalculoEnvio_Load(object sender, EventArgs e)
        {

			//icon
            clsFormato.SetIconForm(this); 		
            //Peso
            clsDataBD.CargarComboOrden(this.cboDestino, "DestinosEnvios", "Descripcion", "IdDestinoEnvio","IdDestinoEnvio");
            cboDestino.SelectedIndex = -1;

            //Titulo
            this.Text = clsGlobales.cParametro.NombreFantasia + this.Text;

        }

        #endregion

        #region Metodo CalcularEnvio

        private void CalcularEnvio()
        {
            //Si el Importe es empty o 0, contrareembolso es 0
            if (string.IsNullOrEmpty(txtImpo.Text))
            { txtCtraRB.Text = "0.00"; }
            else if (Convert.ToDouble(txtImpo.Text) == 0)
            { txtCtraRB.Text = "0.00"; }

            //Validar Importe
            if (string.IsNullOrEmpty(txtImpo.Text)) { txtImpo.Text = "0"; }
            
            //Tomar importe y convertir a double
            double Importe = Convert.ToDouble(txtImpo.Text);

            //Si importe == 0
            if (Importe == 0)
            {
                this.txtCtraRB.Text = "0.00";
            }
            else if (Importe <= 50)
            {
                this.txtCtraRB.Text = Convert.ToDouble("3").ToString("#0.00");
            }
            else
            {
                if (Importe <= 300)
                {
                    this.txtCtraRB.Text = (Importe * 0.06).ToString("#0.00");
                }
                else
                {
                    if (Importe <= 700)
                    {
                        this.txtCtraRB.Text = (Importe * 0.05).ToString("#0.00");
                    }
                    else if (Importe > 700)
                    {
                        this.txtCtraRB.Text = (Importe * 0.04).ToString("#0.00");
                    }
                }
            }

            //Gasto Envio
            this.txtGtoEnvio.Text = (Convert.ToDouble(this.txtEEstp.Text) + Convert.ToDouble(txtCtraRB.Text)).ToString("#0.00");

            this.txtTotal.Text = (Importe + Convert.ToDouble(txtGtoEnvio.Text)).ToString("#0.00");
        }

        #endregion


        #region Método que carga los ToolsTip del formulario

        private void CargarToolsTip()
        {
            toolTip1.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip2.SetToolTip(this.btnSalir, "Salir");

        }

        #endregion

        #region Eventos Botones

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtImpo_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtImpo_Enter(object sender, EventArgs e)
        {
            txtImpo.SelectionStart = 0;
            txtImpo.SelectionLength = txtImpo.Text.Length;
        }

        private void txtImpo_Click(object sender, EventArgs e)
        {
            txtImpo.SelectionStart = 0;
            txtImpo.SelectionLength = txtImpo.Text.Length;
        }

        #endregion

        private void cboPeso_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(cboPeso.SelectedIndex == -1))
            {
                this.txtEEstp.Text = Convert.ToDouble(cboPeso.SelectedValue.ToString()).ToString("#0.00");
                
            }
            else
            {
                this.txtEEstp.Text = "0.00";
            }

            this.CalcularEnvio();
        }

        private void txtEEstp_KeyPress(object sender, KeyPressEventArgs e)
        {
                e.Handled = true;
        }

        private void txtCtraRB_KeyPress(object sender, KeyPressEventArgs e)
        {
                e.Handled = true;
        }

        private void txtGtoEnvio_KeyPress(object sender, KeyPressEventArgs e)
        {
                e.Handled = true;
        }

        private void txtEstamp_KeyPress(object sender, KeyPressEventArgs e)
        {
                e.Handled = true;
        }

        private void txtCtraReemb_KeyPress(object sender, KeyPressEventArgs e)
        {
                e.Handled = true;
        }

        private void txtGtEnvio_KeyPress(object sender, KeyPressEventArgs e)
        {
                e.Handled = true;
        }

        private void txtTot_KeyPress(object sender, KeyPressEventArgs e)
        {
                e.Handled = true;
        }

        private void txtTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
                e.Handled = true;
        }

        private void cboDestino_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(cboDestino.SelectedIndex == -1))
            {
                if (Convert.ToInt32(cboDestino.SelectedValue) == 1)
                {
                    //Peso
                    clsDataBD.CargarComboOrden(this.cboPeso, "CostosEnvios", "Descripcion", "Sucursal", "IdCostoEnvio");
                    cboPeso.SelectedIndex = -1;
                }
                else
                {
                    //Peso
                    clsDataBD.CargarComboOrden(this.cboPeso, "CostosEnvios", "Descripcion", "Domicilio","IdCostoEnvio");
                    cboPeso.SelectedIndex = -1;
 
                }
            }
        }

        private void txtImpo_TextChanged(object sender, EventArgs e)
        {
              //Recalcular envio
                CalcularEnvio();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Si importe > 0
            if (Convert.ToDouble(txtGtoEnvio.Text) > 0)
            {
                AuxTextBox.Text = this.txtGtoEnvio.Text;
            }

            //Cerrar
            this.Close();
        }
    }
}

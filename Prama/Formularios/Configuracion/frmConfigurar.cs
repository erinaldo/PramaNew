using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prama
{
    public partial class frmConfigurar : Form
    {

        #region Constructor

        public frmConfigurar()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos Botones

        #region btnCancelar: Click

        private void btnCancelar_Click(object sender, EventArgs e)
        {
          //Fin
            this.Close();
        }

        #endregion

        #region btnAceptar: Click

        private void btnAceptar_Click(object sender, EventArgs e)
        {

          //Caregar los datos al objeto
            clsGlobales.cParametro.Presupuestos = Convert.ToInt32(this.numPresu.Value);
            clsGlobales.cParametro.Facturas = Convert.ToInt32(this.numFactu.Value);
            clsGlobales.cParametro.Remitos = Convert.ToInt32(this.numRemi.Value);
            clsGlobales.cParametro.Recibos = Convert.ToInt32(this.numRecibos.Value);
            clsGlobales.cParametro.Pedidos = Convert.ToInt32(this.tabNum.Value);
            clsGlobales.cParametro.NivelBaja = Convert.ToInt32(this.txtNivel.Value);
            clsGlobales.cParametro.NivelStock = Convert.ToInt32(this.txtNivelStock.Value);

            if (string.IsNullOrEmpty(txtMesPresu.Text)) { txtMesPresu.Text = "0"; }
            clsGlobales.cParametro.CaducidadPresupuestos = Convert.ToInt32(this.txtMesPresu.Text);

            if (string.IsNullOrEmpty(txtMesPed.Text)) { txtMesPed.Text = "0"; }
            clsGlobales.cParametro.CaducidadPedidos = Convert.ToInt32(this.txtMesPed.Text);

            if (string.IsNullOrEmpty(txtPorcIVA.Text)) { txtPorcIVA.Text = "0"; }
            clsGlobales.cParametro.Iva = Convert.ToDouble(txtPorcIVA.Text);

            clsGlobales.cParametro.Imprimir = (bool)this.chkImpresion.Checked;

            clsGlobales.cParametro.PtoVtaPorDefecto = Convert.ToInt32(cboPvAFIP.SelectedValue);

            clsGlobales.cParametro.AlmacenPorDefecto = Convert.ToInt32(cboAlmacen.SelectedValue);

            clsGlobales.cParametro.CantMinRev = Convert.ToInt32(this.nCantMin.Value);

            clsGlobales.cParametro.UltIns = Convert.ToInt32(this.nroIns.Value);

            clsGlobales.cParametro.UltProd = Convert.ToInt32(this.nroProd.Value);

            if (string.IsNullOrEmpty(txtPorcLimitCba.Text)) { txtPorcLimitCba.Text = "0.00"; }
            clsGlobales.cParametro.PorcLimitCdba = Convert.ToDouble(this.txtPorcLimitCba.Text);

            if (string.IsNullOrEmpty(txtLimitCdbaLimit.Text)) { txtLimitCdbaLimit.Text = "0.00"; }
            clsGlobales.cParametro.PorcLimitCbaLimit = Convert.ToDouble(this.txtLimitCdbaLimit.Text);
            
            //Vector Errores
            string[] cErrores = clsGlobales.cParametro.cValidaParametros();
            //VALIDAR EL OBJETO Y VER SI HAY ERRORES. N.           
            if (!(cErrores[0] == null))
            {
                frmShowErrores myForm = new frmShowErrores();
                myForm.myTitulo = this.Text;
                myForm.miserrores = cErrores.Length;
                myForm.myVector = cErrores;
                myForm.CargarVector();
                myForm.CargarTitulo();
                myForm.ShowDialog();
                return;
            }

          //Update
            clsGlobales.cParametro.UpdateParametros();

         //Salir
            this.Close();
        }

        #endregion

        #endregion

        #region Eventos KeyPress

        private void txtPorcIVA_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 44)
            {
                e.KeyChar = Convert.ToChar(46);
                ch = e.KeyChar;

            }
            //PUNTO DECIMAL. N.
            if (ch == 46 && txtPorcIVA.Text.IndexOf('.') != -1)
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
            if (!char.IsControl(e.KeyChar))
            {
                if (txtPorcIVA.Text.IndexOf('.') > -1 &&
                    txtPorcIVA.Text.Substring(txtPorcIVA.Text.IndexOf('.')).Length >= (2 + 1))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtMesPresu_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Solo enteros
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }    
        }

        private void txtMesPed_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Solo enteros
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        #endregion

        #region Eventos Enter

        private void txtMesPed_Enter(object sender, EventArgs e)
        {
            txtMesPed.SelectAll();
        }
        

        private void txtMesPresu_Enter(object sender, EventArgs e)
        {
            txtMesPresu.SelectAll();
        }

      
        private void txtPorcIVA_Enter(object sender, EventArgs e)
        {
            txtPorcIVA.SelectAll();
        }

        #endregion

        #region Evento Load 

        private void frmConfigurar_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            // Llamo al método cargar CargarCambo para cboAlmacen N.
            clsDataBD.CargarCombo(this.cboAlmacen, "Almacenes", "Almacen", "IdAlmacen");

            // Llamo al método cargar CargarCambo para cboAlmacen N.
            clsDataBD.CargarCombo(this.cboPvAFIP, "PuntosVentaAFIP", "Punto", "IdPuntoVenta", " IdSistema = 3");

            // Cargar Configuracion **************************************************************

            chkImpresion.Checked = clsGlobales.cParametro.Imprimir;

            this.txtMesPresu.Text = clsGlobales.cParametro.CaducidadPresupuestos.ToString();
            this.txtMesPed.Text = clsGlobales.cParametro.CaducidadPedidos.ToString();

            this.numPresu.Value = clsGlobales.cParametro.Presupuestos;
            this.numRecibos.Value = clsGlobales.cParametro.Recibos;
            this.numRemi.Value = clsGlobales.cParametro.Remitos;
            this.numFactu.Value = clsGlobales.cParametro.Facturas;

            this.numPresu.Value = clsGlobales.cParametro.Presupuestos;

            this.cboAlmacen.SelectedValue = clsGlobales.cParametro.AlmacenPorDefecto;
            this.cboPvAFIP.SelectedValue = clsGlobales.cParametro.PtoVtaPorDefecto;

            this.nCantMin.Value = clsGlobales.cParametro.CantMinRev;

            this.nroIns.Value = clsGlobales.cParametro.UltIns;

            this.nroProd.Value = clsGlobales.cParametro.UltProd;

            this.txtNivel.Value = clsGlobales.cParametro.NivelBaja;

            this.txtNivelStock.Value = clsGlobales.cParametro.NivelStock;

            this.txtNivelFact.Value = clsGlobales.cParametro.NivelFact;

            this.txtPorcLimitCba.Text = clsGlobales.cParametro.PorcLimitCdba.ToString("#0.00");

            this.txtLimitCdbaLimit.Text = clsGlobales.cParametro.PorcLimitCbaLimit.ToString("#0.00");


            //************************************************************************************

            this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;
        }

        #endregion

        private void txtPorcLimitCba_Click(object sender, EventArgs e)
        {
            txtPorcLimitCba.SelectionStart = 0;
            txtPorcLimitCba.SelectionLength = txtPorcLimitCba.Text.Length;
        }

        private void txtPorcLimitCba_Enter(object sender, EventArgs e)
        {
            txtPorcLimitCba.SelectionStart = 0;
            txtPorcLimitCba.SelectionLength = txtPorcLimitCba.Text.Length;
        }

        private void txtLimitCdbaLimit_Enter(object sender, EventArgs e)
        {
            txtLimitCdbaLimit.SelectionStart = 0;
            txtLimitCdbaLimit.SelectionLength = txtLimitCdbaLimit.Text.Length;
        }

        private void txtLimitCdbaLimit_Click(object sender, EventArgs e)
        {
            txtLimitCdbaLimit.SelectionStart = 0;
            txtLimitCdbaLimit.SelectionLength = txtLimitCdbaLimit.Text.Length;
        }
    }
}

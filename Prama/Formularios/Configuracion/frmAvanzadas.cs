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
    public partial class frmAvanzadas : Form
    {
        public frmAvanzadas()
        {
            InitializeComponent();
        }

        #region btnCancelar: Click

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Evento Load del Formulario

        private void frmAvanzadas_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 
			
            // Cargar Configuracion **************************************************************

                this.txtCUIT.Text = clsGlobales.cParametro.CUIT;
                this.txtRS.Text = clsGlobales.cParametro.RazonSocial;
                this.txtNF.Text = clsGlobales.cParametro.NombreFantasia;
                this.txtMail.Text = clsGlobales.cParametro.Mail;
                this.txtWeb.Text = clsGlobales.cParametro.Web;
                this.txtTel.Text = clsGlobales.cParametro.Telefono;
                this.txtDom.Text = clsGlobales.cParametro.Direccion;
                this.txtCondicionIVA.Text = clsGlobales.cParametro.CondicionIva;
                this.txtLocalidad.Text = clsGlobales.cParametro.Localidad;

                chkControlUser.Checked = Convert.ToBoolean(clsGlobales.cParametro.ControlLoginOff);
                this.chkIconTaskBar.Checked = Convert.ToBoolean(clsGlobales.cParametro.IconInTaskBar);
                this.chkAutoLoad.Checked = Convert.ToBoolean(clsGlobales.cParametro.AutoLoad);

            //************************************************************************************
                if (Convert.ToBoolean(clsGlobales.cParametro.ModoFactura) == false)
                {
                    this.rdoTest.Checked = true;
                    this.rdoProd.Checked = false;
                    clsGlobales.CertificadoAFIP = "certificado.pfx";
                }
                else
                {
                    this.rdoProd.Checked = true;
                    this.rdoTest.Checked = false;
                    clsGlobales.CertificadoAFIP = "crtPMA.pfx";
                }

            //Titulo Ventana
                this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;

            //Foco
                txtRS.Focus();
        }

        #endregion

        #region Eventos Enter

        private void txtRS_Enter(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtRS.Text))
            {
                txtRS.SelectionStart = 0;
                txtRS.SelectionLength = txtRS.Text.Length;
            }
        }

        private void txtNF_Enter(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtNF.Text))
            {
                txtNF.SelectionStart = 0;
                txtNF.SelectionLength = txtNF.Text.Length;
            }
        }

        private void txtDom_Enter(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDom.Text))
            {
                txtDom.SelectionStart = 0;
                txtDom.SelectionLength = txtDom.Text.Length;
            }
        }

        private void txtTel_Enter(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtTel.Text))
            {
                txtTel.SelectionStart = 0;
                txtTel.SelectionLength = txtTel.Text.Length;
            }
        }

        private void txtMail_Enter(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtMail.Text))
            {
                txtMail.SelectionStart = 0;
                txtMail.SelectionLength = txtMail.Text.Length;
            }
        }

        private void txtWeb_Enter(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtWeb.Text))
            {
                txtWeb.SelectionStart = 0;
                txtWeb.SelectionLength = txtWeb.Text.Length;
            }
        }

        private void txtCUIT_Enter(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCUIT.Text))
            {
                txtCUIT.SelectionStart = 0;
                txtCUIT.SelectionLength = txtCUIT.Text.Length;
            }
        }

        private void txtLocalidad_Enter(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtLocalidad.Text))
            {
                txtLocalidad.SelectionStart = 0;
                txtLocalidad.SelectionLength = txtLocalidad.Text.Length;
            }
        }

        private void txtCondicionIVA_Enter(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCondicionIVA.Text))
            {
                txtCondicionIVA.SelectionStart = 0;
                txtCondicionIVA.SelectionLength = txtCondicionIVA.Text.Length;
            }
        }

        #endregion

        #region Eventos Botones

        #region btnAceptar: Click

        private void btnAceptar_Click(object sender, EventArgs e)
        {

           //Cargar Objeto
                clsGlobales.cParametro.RazonSocial = this.txtRS.Text;
                clsGlobales.cParametro.NombreFantasia = this.txtNF.Text;
                clsGlobales.cParametro.Direccion = this.txtDom.Text;
                clsGlobales.cParametro.Telefono = this.txtTel.Text;
                clsGlobales.cParametro.Mail = this.txtMail.Text;
                clsGlobales.cParametro.CUIT = this.txtCUIT.Text;
                clsGlobales.cParametro.Localidad = this.txtLocalidad.Text;
                clsGlobales.cParametro.Web = this.txtWeb.Text;
                clsGlobales.cParametro.CondicionIva = this.txtCondicionIVA.Text;
                clsGlobales.cParametro.ControlLoginOff = Convert.ToInt32(chkControlUser.Checked);
                clsGlobales.cParametro.IconInTaskBar = Convert.ToInt32(this.chkIconTaskBar.Checked);
                clsGlobales.cParametro.AutoLoad = Convert.ToInt32(this.chkAutoLoad.Checked);
                if (rdoTest.Checked)
                {
                    clsGlobales.cParametro.ModoFactura = 0;
                    clsGlobales.CertificadoAFIP = "certificado.pfx";
                }
                else
                {
                    clsGlobales.cParametro.ModoFactura = 1;
                    clsGlobales.CertificadoAFIP = "crtPMA.pfx";
                }

            //VECTOR DE ERRORES
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

            //Condicion de Control Off?
                if (clsGlobales.cParametro.ControlLoginOff == 0)
                {
                    //Resetea el registro de logueo
                    clsGlobales.UsuarioLogueado.ControlLoginUserOff();
                }
                else
                {
                  //Registrar al usuario logueado
                    clsGlobales.UsuarioLogueado.UpdateUserLogin(1);
                }

            //Registra Inicio?
                if (clsGlobales.cParametro.AutoLoad == 1)
                {
                    clsSystem oSys = new clsSystem();
                    bool bRI = oSys.RegistrarInicio();
                    
                }
                else
                {
                    clsSystem oSys = new clsSystem();
                    oSys.AnularInicio();
                }

            //Salir
                this.Close();
        }

        #endregion

        #region btnCancelar: Click

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void rdoTest_CheckedChanged(object sender, EventArgs e)
        {

        }

        #endregion
    }
}

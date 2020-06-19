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
    public partial class frmComprasCCProv : Form
    {
        
       // Variable que almacena el Id del Cliente
        int IdProveedor = 0;
        clsProveedores myProveedor = new clsProveedores();
        string myCadenaSQL = "";
        double cSaldoAplicar = 0;
        double cSaldoAFB = 0;

        public frmComprasCCProv(int IdProv, double p_SaldoAFB = 0)
        {
            InitializeComponent();

            IdProveedor = IdProv;
            cSaldoAFB = p_SaldoAFB;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmComprasCCProv_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            // Cargo los datos del proveedor seleccionado al objeto
            CargarProveedor(IdProveedor);
            // Paso los datos del proveedor al formulario
            SetearTextBoxProv();
            // Cargo el detalle de la CC del proveedor
            CargarGrilla(IdProveedor);
            //Titulo
            this.Text = clsGlobales.cParametro.NombreFantasia + this.Text;
            //SaldoAFavor            
            if (clsGlobales.ConB == null)
            {
                if (myProveedor.SaldoAFavor > 0)
                {
                    this.txtSaldoAFavor.Text = myProveedor.SaldoAFavor.ToString("#0.00");
                }
            }
            else
            {
                this.txtSaldoAFavor.Text = this.cSaldoAFB.ToString("#0.00");
            }

            clsDataBD.CargarCombo(cboBancos, "CajaAsociacionesCuentas", "CajaAsociaciones", "IdCajaAsociaciones", "", 1);
            cboBancos.SelectedIndex = -1;

            string sMySQL = "Select * from CajaCuentas";
            DataTable myTable = new DataTable();
            if (clsGlobales.ConB == null) { myTable = clsDataBD.GetSql(sMySQL); } else { myTable = clsDataBD.GetSqlB(sMySQL); }

            if (myTable.Rows.Count == 0)
            {
                this.ckBancos.Enabled = false;
                this.cboBancos.Enabled = false;
                this.txtBancos.Enabled = false;
            }

            sMySQL = "Select * from CajaAsociacionesCuentas";
            myTable = new DataTable();
            if (clsGlobales.ConB == null) { myTable = clsDataBD.GetSql(sMySQL); } else { myTable = clsDataBD.GetSqlB(sMySQL); }

            if (myTable.Rows.Count == 0)
            {
                this.ckBancos.Enabled = false;
                this.cboBancos.Enabled = false;
                this.txtBancos.Enabled = false;
            }            
        }

        #region Método que carga los datos del proveedor a los TextBox del formulario

        private void SetearTextBoxProv()
        {
            // Paso los datos de la clase a los TextBox
            txtCodigoProv.Text = myProveedor.IdProveedor.ToString();
            txtRSoc.Text = myProveedor.RazonSocial;
            txtCUIT.Text = myProveedor.CUIT;
            txtCondicionIva.Text = myProveedor.CondicionIva;
            txtComprador.Text = clsGlobales.UsuarioLogueado.Usuario;

        }

        #endregion

        #region Método que carga los datos a la grilla

        private void CargarGrilla(int p_IdProv)
        {
            // Creo un nuevo DataTable
            DataTable mDtTable = new DataTable();
            // Pregunto por el tipo de conexión
            if (clsGlobales.ConB == null)
            {
                // Armo la cadena SQL
                myCadenaSQL = "Select * from Vista_ComprobantesCompras WHERE IdProveedor = " + p_IdProv + " AND Saldo <>0";
                // Le asigno al nuevo DataTable los datos de la consulta SQL en blanco
                mDtTable = clsDataBD.GetSql(myCadenaSQL);
            }
            else
            {
                // Armo la cadena SQL
                myCadenaSQL = "Select * from Vista_ComprobantesCompras2 WHERE IdProveedor = " + p_IdProv + " AND Saldo <>0";
                // Le asigno al nuevo DataTable los datos de la consulta SQL en negro
                mDtTable = clsDataBD.GetSqlB(myCadenaSQL);
            }
            // Evito que el dgvUsuarios genere columnas automáticas
            dgvComprobantes.AutoGenerateColumns = false;
            // Asigno el source de la grilla
            dgvComprobantes.DataSource = mDtTable;
            // Variable que almacena el item que le corresponde a la fila de la grilla
            int filas = 1;
            // Variable que acumula el saldo del proveedor
            double Saldo = 0;
            // Recorro la grilla y asigno el número de item
            if (dgvComprobantes.Rows.Count > 0)
            {
                // Recorro la grilla 
                foreach (DataGridViewRow row in dgvComprobantes.Rows)
                {
                    Saldo = Saldo + Convert.ToDouble(row.Cells["Saldo"].Value);
                    // Asigno el número de item
                    row.Cells["Item"].Value = filas;
                    row.Cells["Resto"].Value = Convert.ToDouble(row.Cells["Saldo"].Value);
                    // Aumento el contador
                    filas++;
                }
                // Asigno el saldo total al TextBox del pie
                txtSaldo.Text = Saldo.ToString("#0.00");
                // Si el saldo es mayor a 0, las letras son rojas, si no, azul
                if (Saldo > 0)
                {
                    txtSaldo.ForeColor = Color.Red;
                }
                else
                {
                    txtSaldo.ForeColor = Color.Blue;
                }
            }
        }

        #endregion

        //Sumar Restos
        private void SumarRestos()        
        {
            // Variable que acumula el saldo del proveedor
            double cResto = 0;
            // Recorro la grilla 
            foreach (DataGridViewRow row in dgvComprobantes.Rows)
            {
                cResto = cResto + Convert.ToDouble(row.Cells["Resto"].Value);
            }
            // Asigno el saldo total al TextBox del pie
            txtSaldo.Text = cResto.ToString("#0.00");
            // Si el saldo es mayor a 0, las letras son rojas, si no, azul
            if (cResto > 0)
            {
                txtSaldo.ForeColor = Color.Red;
            }
            else
            {
                txtSaldo.ForeColor = Color.Blue;
            }
        }

        #region Método que carga los datos de los Clientes a la clase

        private void CargarProveedor(int Id)
        {
            // Armo la cadena SQL
            string myCadenaSQL = "select * from Vista_Proveedores where IdProveedor = " + Id;
            // Creo una tabla que me va a almacenar el resultado de la consulta
            DataTable myTablaProveedor = clsDataBD.GetSql(myCadenaSQL);
            // Recorro la tabla y le asigo los datos del proveedor al objeto
            foreach (DataRow rowProv in myTablaProveedor.Rows)
            {
                myProveedor.IdProveedor = Convert.ToInt32(rowProv["IdProveedor"].ToString());

                myProveedor.RazonSocial = rowProv["RazonSocial"].ToString();
                myProveedor.NombreFantasia = rowProv["NombreFantasia"].ToString();
                myProveedor.Direccion = rowProv["Direccion"].ToString();
                myProveedor.IdLocalidad = Convert.ToInt32(rowProv["IdLocalidad"].ToString());
                myProveedor.FechaInicioActividad = rowProv["FechaInicioActividad"].ToString();
                myProveedor.IngresosBrutos = rowProv["IngresosBrutos"].ToString();
                myProveedor.CUIT = rowProv["CUIT"].ToString();
                /*Añadir guiones si hace falta*/
                if (myProveedor.CUIT.Length == 11)
                {
                    myProveedor.CUIT = clsGlobales.cFormato.CUITFormateado(rowProv["CUIT"].ToString());
                }
                else
                {
                    myProveedor.CUIT = rowProv["CUIT"].ToString();
                }
                myProveedor.CondicionIva = rowProv["CondicionIva"].ToString();
                myProveedor.CondicionCompra = rowProv["CondicionCompra"].ToString();
                myProveedor.IdCondicionIva = Convert.ToInt32(rowProv["IdCondicionIva"].ToString());
                myProveedor.IdCondicionCompra = Convert.ToInt32(rowProv["IdCondicionCompra"].ToString());
                myProveedor.IdProvincia = Convert.ToInt32(rowProv["IdProvincia"].ToString());
                myProveedor.IdLocalidad = Convert.ToInt32(rowProv["IdLocalidad"].ToString());
                myProveedor.Telefono = rowProv["Telefono"].ToString();
                myProveedor.Fax = rowProv["Fax"].ToString();
                myProveedor.Celular = rowProv["Celular"].ToString();
                myProveedor.MailEmpresa = rowProv["MailEmpresa"].ToString();
                myProveedor.Web = rowProv["Web"].ToString();
                myProveedor.Contacto = rowProv["Contacto"].ToString();
                myProveedor.MailContacto = rowProv["MailContacto"].ToString();
                myProveedor.CelularContacto = rowProv["CelularContacto"].ToString();
                myProveedor.Observaciones = rowProv["Observaciones"].ToString();

                
                if (!(Convert.ToDouble(rowProv["AFavor_B"].ToString()) == 0))
                {
                    myProveedor.SaldoAFavor = Convert.ToDouble(rowProv["AFavor_B"].ToString());
                }
                else
                {
                    myProveedor.SaldoAFavor = 0;
                }

                if (!(Convert.ToDouble(rowProv["Saldo_B"].ToString())==0))
                {
                    myProveedor.Saldo = Convert.ToDouble(rowProv["Saldo_B"].ToString());
                }
                else
                {
                    myProveedor.Saldo = 0;
                }

                if (Convert.ToBoolean(rowProv["Insumos"].ToString()))
                {
                    myProveedor.ProvIns = 1;
                }

                if (Convert.ToBoolean(rowProv["Productos"].ToString()))
                {
                    myProveedor.ProvProd = 1;
                }


            }
        }

        #endregion

        private void cCalcularTotalRC()
        {
            double cTotal = 0;

            if (!(string.IsNullOrEmpty(txtEfectivo.Text)))
            {
                if (!(Convert.ToDouble(txtEfectivo.Text)==0))
                {
                    cTotal += Convert.ToDouble(txtEfectivo.Text);
                }
            }

            if (!(string.IsNullOrEmpty(txtBancos.Text)))
            {
                if (!(Convert.ToDouble(txtBancos.Text) == 0))
                {
                    cTotal += Convert.ToDouble(txtBancos.Text);
                }
            }

          
            if (!(string.IsNullOrEmpty(txtCheques.Text)))
            {
                if (!(Convert.ToDouble(txtCheques.Text) == 0))
                {
                    cTotal += Convert.ToDouble(txtCheques.Text);
                }
            }

            if (!(string.IsNullOrEmpty(this.txtSaldoAFavor.Text)))
            {
                if (!(Convert.ToDouble(txtSaldoAFavor.Text) == 0))
                {
                    cTotal += Convert.ToDouble(txtSaldoAFavor.Text);
                }
            }

            this.txtTotalRC.Text = cTotal.ToString("#0.00");

        }

        private void dgvComprobantes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Me fui
            if (!(Convert.ToDouble(txtTotalRC.Text) > 0))
            { return; }

            if (cSaldoAplicar == 0)
            { return; }
            
            //**

            if (cSaldoAplicar > Convert.ToDouble(this.dgvComprobantes.CurrentRow.Cells["Saldo"].Value))
            {
                //Aplico el monto
                this.dgvComprobantes.CurrentRow.Cells["Resto"].Value = 0;
                //Reasignar
                cSaldoAplicar = Convert.ToDouble(txtSaldoApli.Text) - Convert.ToDouble(this.dgvComprobantes.CurrentRow.Cells["Saldo"].Value);
                txtSaldoApli.Text = cSaldoAplicar.ToString("#0.00");

                this.dgvComprobantes.CurrentRow.Cells["Aplicado"].Value = Convert.ToDouble(this.dgvComprobantes.CurrentRow.Cells["Saldo"].Value);
            }
            else
            {
                //Aplico el monto
                this.dgvComprobantes.CurrentRow.Cells["Resto"].Value = (Convert.ToDouble(this.dgvComprobantes.CurrentRow.Cells["Saldo"].Value) - cSaldoAplicar).ToString("#0.00");
                //Lo resto al saldo a aplicar
                txtSaldoApli.Text = (Convert.ToDouble(txtSaldoApli.Text) - cSaldoAplicar).ToString("#0.00");
                //Reasignar
                cSaldoAplicar = Convert.ToDouble(txtSaldoApli.Text);

                this.dgvComprobantes.CurrentRow.Cells["Aplicado"].Value = (Convert.ToDouble(this.dgvComprobantes.CurrentRow.Cells["Saldo"].Value) - Convert.ToDouble(this.dgvComprobantes.CurrentRow.Cells["Resto"].Value.ToString())).ToString("#0.00");
            }

            //Actualizar
            SumarRestos();

            if (Convert.ToInt32(this.dgvComprobantes.CurrentRow.Cells["Elegido"].Value) == 1)
            {
                dgvComprobantes.CurrentRow.Cells["Elegido"].Value = false;
            }
            else
            {
                dgvComprobantes.CurrentRow.Cells["Elegido"].Value = true;
            }
        }

        private void txtEfectivo_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtTransf_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtPropios_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtTerceros_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtMP_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCR_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtOtros_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtEfectivo_TextChanged(object sender, EventArgs e)
        {
            cCalcularTotalRC();
        }

        private void txtTransf_TextChanged(object sender, EventArgs e)
        {
            cCalcularTotalRC();
        }

        private void txtPropios_TextChanged(object sender, EventArgs e)
        {
            cCalcularTotalRC();
        }

        private void txtTerceros_TextChanged(object sender, EventArgs e)
        {
            cCalcularTotalRC();
        }

        private void txtMP_TextChanged(object sender, EventArgs e)
        {
            cCalcularTotalRC();
        }

        private void txtCR_TextChanged(object sender, EventArgs e)
        {
            cCalcularTotalRC();
        }

        private void txtOtros_TextChanged(object sender, EventArgs e)
        {
            cCalcularTotalRC();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            this.gpbDetalle.Enabled=false;
            this.gpbTotal.Enabled = false;
            cSaldoAplicar = Convert.ToDouble(this.txtTotalRC.Text);
            this.txtSaldoApli.Text = cSaldoAplicar.ToString("#0.00");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Variable que valida que se selecionó alguna factura
            bool bSeleccionada = false;
            // Recorro la grilla para comprobar si seleccionó alguna factura
            foreach (DataGridViewRow row in dgvComprobantes.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Elegido"].Value) == true)
                {
                    bSeleccionada = true;
                    break;
                }
            }
            
            //Validar saldo a favor sin nada seleccionado
           /* if (bSeleccionada == false && Convert.ToDouble(txtSaldoAFavor.Text) > 0)
            {
                MessageBox.Show("No se puede emitir recibo porque no se ha aplicado el Saldo a ningún comprobante!", "Informaciòn!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //*/


            // Si no se seleccionó ninguna factura lo informo
            if (!(bSeleccionada))
            {
                // No hay recibos comprobantes seleccionados
                DialogResult myRespuesta = MessageBox.Show("No ha seleccionado ninguna factura. ¿Desea Continuar?", "CONFIRMACIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Si desea continuar
                if (myRespuesta == DialogResult.Yes)
                {
                    // Si seleccionó alguna fatura
                    GenerarOrdenPago(bSeleccionada);
                    // Cierro el formulario
                    this.Close();
                }
            }
            else
            {
                // Si seleccionó alguna fatura
                GenerarOrdenPago(bSeleccionada);
                // Cierro el formulario
                this.Close();
            }
            
        }

        private void txtSaldoAFavor_TextChanged(object sender, EventArgs e)
        {
            cCalcularTotalRC();
        }

        #region Método que genera el recibo

        private void GenerarOrdenPago(bool p_bSeleccionada = false)
        {
            //Variables
            //int LastIdRecibo = 0;
            double cSumaResto = 0;

            double dEfvo = 0;
            double dCheques = 0;
            double dBancos = 0;
            double dSaldofav = 0;
            int IdBanco = 0;
            int OrdenPagoNro = 0;

            //Validar
            if (clsGlobales.cValida.IsNumeric(txtEfectivo.Text))
            { dEfvo = Convert.ToDouble(txtEfectivo.Text);}
            
            if (clsGlobales.cValida.IsNumeric(this.txtCheques.Text))
            { dCheques = Convert.ToDouble(this.txtCheques.Text); }
            
            if (clsGlobales.cValida.IsNumeric(this.txtBancos.Text))
            { dBancos = Convert.ToDouble(this.txtBancos.Text); }

            if (clsGlobales.cValida.IsNumeric(this.txtSaldoAFavor.Text))
            { dSaldofav = Convert.ToDouble(this.txtSaldoAFavor.Text); }

            // Pido al operador la confirmación
            DialogResult myRespuesta = MessageBox.Show("¿Desea generar la Orden de Pago en base al Detalle de Pago y/o Comprobantes especificados?", "Confirmar!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Si la respuesta es si
            if (myRespuesta == DialogResult.Yes)
            {

                string myCad = "";

                // Monto total sin saldo a favor para la caja
                double dMonto = dCheques + dEfvo + dBancos;
                
                if (clsGlobales.ConB == null)
                {
                    string myFecha = clsValida.ConvertirFecha(DateTime.Now).ToString();

                    //Tomar Numero Orden Pago Ultima + 1
                    OrdenPagoNro = RetornarNro(5)+1;

                    // Limpio los datos de la caja
                    Prama.Clases.clsCaja.LimpiarCaja();

                    // Paso el número del recibo a la clase de la caja
                    Prama.Clases.clsCaja.Entrada = false;
                    Prama.Clases.clsCaja.Fecha = DateTime.Today;
                    Prama.Clases.clsCaja.Debito = 0;
                    Prama.Clases.clsCaja.Credito = 0;
                    Prama.Clases.clsCaja.Efectivo = dEfvo;
                    Prama.Clases.clsCaja.Contra = 0;
                    Prama.Clases.clsCaja.Monto = dMonto;
                    Prama.Clases.clsCaja.Movimiento = txtRSoc.Text;
                    Prama.Clases.clsCaja.MP = 0;
                    Prama.Clases.clsCaja.Cheques = dCheques;
                    Prama.Clases.clsCaja.Transferencia = 0;
                    Prama.Clases.clsCaja.IdCajaImputacion = 2;
                    Prama.Clases.clsCaja.Comprobante = clsGlobales.cParametro.PtoVtaPorDefecto.ToString("D4") + "-" + OrdenPagoNro.ToString("D8");

                    //Insert del Recibo
                     myCad = "INSERT INTO OrdenesPago (IdProveedor,Fecha,FechaReal,Numero,Efectivo,Cheques,Bancos,Total,Activo,Usuario,SaldoAFavor) VALUES " +                         
                                   "(" + txtCodigoProv.Text + ",'" 
                                   + myFecha + "','" 
                                   + DateTime.Now.ToShortDateString() + "', "
                                   + OrdenPagoNro.ToString() + ", "
                                   + dEfvo + ","
                                   + dCheques + ","
                                   + dBancos + ","
                                   + (Convert.ToDouble(txtTotalRC.Text) - dSaldofav) 
                                   + ",1,'" 
                                   + clsGlobales.UsuarioLogueado.Usuario.ToString() + "', "
                                   + dSaldofav + ")";

                    //Ejecutar consulta
                    clsDataBD.GetSql(myCad);

                    //UpdateNro
                    UpdateNro(OrdenPagoNro, 5);

                    if (cboBancos.Text != "")
                    {
                        IdBanco = Convert.ToInt32(cboBancos.SelectedValue.ToString());
                    }
                                   
                    // Grabo el movimiento de la caja
                    Prama.Clases.clsCaja.CargarCajaOp(OrdenPagoNro, dBancos, IdBanco);
                    
                }
                else
                {

                    string myFecha = clsValida.ConvertirFecha(DateTime.Now).ToString();

                    OrdenPagoNro = clsDataBD.RetornarUltimoIdB("OrdenesPago", "IdOrdenPago") +1;

                    // Limpio los datos de la caja
                    Prama.Clases.clsCaja.LimpiarCaja();

                    // Paso el número del recibo a la clase de la caja
                    Prama.Clases.clsCaja.Entrada = false;
                    Prama.Clases.clsCaja.Fecha = DateTime.Today;
                    Prama.Clases.clsCaja.Debito = 0;
                    Prama.Clases.clsCaja.Credito = 0;
                    Prama.Clases.clsCaja.Efectivo = dEfvo;
                    Prama.Clases.clsCaja.Contra = 0;
                    Prama.Clases.clsCaja.Monto = dMonto;
                    Prama.Clases.clsCaja.Movimiento = txtRSoc.Text;
                    Prama.Clases.clsCaja.MP = 0;
                    Prama.Clases.clsCaja.Cheques = dCheques;
                    Prama.Clases.clsCaja.Transferencia = 0;
                    Prama.Clases.clsCaja.IdCajaImputacion = 2;
                    Prama.Clases.clsCaja.Comprobante = clsGlobales.cParametro.PtoVtaPorDefecto.ToString("D4") + "-" + OrdenPagoNro.ToString("D8");

                    //Insert del Recibo
                    myCad = "INSERT INTO OrdenesPago (IdProveedor,Fecha,FechaReal,Numero,Efectivo,Cheques,Bancos,Total,Activo,Usuario,SaldoAFavor) VALUES " +
                                  "(" + txtCodigoProv.Text + ",'"
                                  + myFecha + "','"
                                  + DateTime.Now.ToShortDateString() + "', "
                                  + OrdenPagoNro.ToString() + ", "
                                  + dEfvo + ","
                                  + dCheques + "," 
                                  + dBancos + ","
                                  + (Convert.ToDouble(txtTotalRC.Text) - dSaldofav) 
                                  + ",1,'" 
                                  + clsGlobales.UsuarioLogueado.Usuario.ToString() + "', "
                                  + dSaldofav + ")";
                    
                    //Ejecutar consulta
                    clsDataBD.GetSqlB(myCad);

                    //UpdateNro
                    //UpdateNro(OrdenPagoNro, 5);

                    if (cboBancos.Text != "")
                    {
                        IdBanco = Convert.ToInt32(cboBancos.SelectedValue.ToString());
                    }

                    // Grabo el movimiento de la caja
                    Prama.Clases.clsCaja.CargarCajaOp(OrdenPagoNro, dBancos, IdBanco);
                    
                }

                //Actualizar
                //Recorrer la grilla y ver facturas seleccionadas
                //Updatear saldo de la factura
                foreach (DataGridViewRow myRow in this.dgvComprobantes.Rows)
                {
                    if (Convert.ToBoolean(myRow.Cells["Elegido"].Value) == true)
                    {

                        //Updatear saldo con resto en la factura
                        myCad = "UPDATE ComprobantesCompras SET Saldo = " + Convert.ToDouble(myRow.Cells["Resto"].Value.ToString()) +
                                " WHERE IdComprobanteCompra = " + Convert.ToInt32(myRow.Cells["IdComprobante"].Value.ToString());

                        if (clsGlobales.ConB == null)
                        {
                            clsDataBD.GetSql(myCad);
                        }
                        else
                        {
                            clsDataBD.GetSqlB(myCad);
                        }

                        //Guardar el detalle del recibo

                        if (clsGlobales.ConB == null)
                        {
                            myCad = "INSERT INTO OrdenesPagoComprobantes (IdComprobanteCompra, IdOrdenPago, Importe, Activo) VALUES ("
                                    + myRow.Cells["IdComprobante"].Value + ","
                                    + clsDataBD.RetornarUltimoId("OrdenesPago", "IdOrdenPago") + ","
                                    + Convert.ToDouble(myRow.Cells["Aplicado"].Value.ToString()) + ",1)";

                            clsDataBD.GetSql(myCad);
                        }
                        else
                        {
                            myCad = "INSERT INTO OrdenesPagoComprobantes (IdComprobanteCompra, IdOrdenPago, Importe, Activo) VALUES ("
                                    + myRow.Cells["IdComprobante"].Value + ","
                                    + clsDataBD.RetornarUltimoIdB("OrdenesPago", "IdOrdenPago") + ","
                                    + Convert.ToDouble(myRow.Cells["Aplicado"].Value.ToString()) + ",1)";

                            clsDataBD.GetSqlB(myCad);
                        }
                    }

                    //Suma Restos
                    cSumaResto += Convert.ToDouble(myRow.Cells["Resto"].Value.ToString());
                }

                double cSaldoApliUpdate = 0;

                //Saldo Cliente
                cSaldoApliUpdate = Convert.ToDouble(txtSaldo.Text) - Convert.ToDouble(txtSaldoApli.Text);
                
                //Validar lo que quedo del saldo a aplicar...               
                if (cSaldoApliUpdate < 0)
                {
                    cSaldoApliUpdate = cSaldoApliUpdate * -1;
                }

                //VER SALDO PROVEEDOR!!! Y SaldoCliProv!!!
                if (clsGlobales.ConB == null)
                {
                    myCad = "UPDATE Proveedores SET SaldoAFavor = 0 WHERE IdProveedor = " + myProveedor.IdProveedor;
                    clsDataBD.GetSql(myCad);

                    if (p_bSeleccionada == false && Convert.ToDouble(txtSaldoApli.Text) > 0)
                    {
                        myCad = "UPDATE Proveedores SET SaldoAFavor = " + Convert.ToDouble(txtSaldoApli.Text) + " WHERE IdProveedor = " + myProveedor.IdProveedor;
                        clsDataBD.GetSql(myCad);
                    }
                    else if (p_bSeleccionada == true && Convert.ToDouble(txtSaldoApli.Text) >0)
                    {
                        ///ojo
                        myCad = "UPDATE Proveedores SET SaldoAFavor = " + Convert.ToDouble(txtSaldoApli.Text) + " WHERE IdProveedor = " + myProveedor.IdProveedor;
                        clsDataBD.GetSql(myCad);
                        //

                        myCad = "UPDATE Proveedores SET Saldo = " + cSumaResto + " WHERE IdProveedor = " + myProveedor.IdProveedor;
                        clsDataBD.GetSql(myCad); 
                    }
                    else if (p_bSeleccionada == true && Convert.ToDouble(txtSaldoApli.Text)==0)
                    {
                        myCad = "UPDATE Proveedores SET Saldo = " + cSaldoApliUpdate + " WHERE IdProveedor = " + myProveedor.IdProveedor;
                        clsDataBD.GetSql(myCad); 
                    }
                }
                else
                {
                    bool bEncontrado = false;
                    // Traigo los datos de los saldos de clientes de la tabla intermedia
                    myCad = "Select * from SaldoCliProv";
                    // Guardo los saldos de los comprobanbtes de la tabla intermedia en la tabal
                    DataTable myDtSaldos = clsDataBD.GetSqlB(myCad);
                    // Paso a la variable el saldo de la tabla intermedia si es que hay un
                    foreach (DataRow rowSaldos in myDtSaldos.Rows)
                    {
                        if (Convert.ToInt32(txtCodigoProv.Text) == Convert.ToInt32(rowSaldos["IdProveedor"]))
                        {
                            bEncontrado = true;
                            break;
                        }
                    }

                    if (!(bEncontrado))
                    {
                        myCad = "INSERT INTO SaldoCliProv (IdCliente, SaldoCli, IdProveedor, SaldoProv, SaldoInicial, SaldoAFavor) values (" +
                                        "0, 0, " + myProveedor.IdProveedor + ",0, 0, 0)"; 
                        // Ejecuto la consulta 
                        clsDataBD.GetSqlB(myCad); 
                    }

                    myCad = "UPDATE SaldoCliProv SET SaldoAFavor = 0 WHERE IdProveedor = " + myProveedor.IdProveedor;
                    clsDataBD.GetSqlB(myCad);
                    
                    bEncontrado = false;


                    //Casos de Uso
                    if (p_bSeleccionada == false && Convert.ToDouble(txtSaldoApli.Text) > 0)
                    {
                        myCad = "UPDATE SaldoCliProv SET SaldoAFavor = " + Convert.ToDouble(txtSaldoApli.Text) + " WHERE IdProveedor = " + myProveedor.IdProveedor;
                        clsDataBD.GetSqlB(myCad);
                    }
                    else if (p_bSeleccionada == true && Convert.ToDouble(txtSaldoApli.Text) > 0)
                    {
                        //OJO
                        myCad = "UPDATE SaldoCliProv SET SaldoAFavor = " + Convert.ToDouble(txtSaldoApli.Text) + " WHERE IdProveedor = " + myProveedor.IdProveedor;
                        clsDataBD.GetSqlB(myCad);
                        //

                        myCad = "UPDATE SaldoCliProv SET SaldoProv = " + cSumaResto + " WHERE IdProveedor = " + myProveedor.IdProveedor;
                        clsDataBD.GetSqlB(myCad);
                    }
                    else if (p_bSeleccionada == true && Convert.ToDouble(txtSaldoApli.Text) == 0)
                    {
                        myCad = "UPDATE SaldoCliProv SET SaldoProv = " + cSaldoApliUpdate + " WHERE IdProveedor = " + myProveedor.IdProveedor;
                        clsDataBD.GetSqlB(myCad);
                    }
                    //**

                }


                //CHEQUES
                if (Convert.ToDouble(txtCheques.Text) != 0)
                {
                    //int iIdCheque = 0;
                    myCadenaSQL = "select * from Temporal_DetalleCheques where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario;
                    DataTable myDt = new DataTable();
                    // Ejecuto la cadena para grabar el movimiento en la Caja
                    if (clsGlobales.ConB == null) { myDt = clsDataBD.GetSql(myCadenaSQL); } else { myDt = clsDataBD.GetSqlB(myCadenaSQL); }
                    // Recorro la temporal y grabo el cheque en la tabla
                    foreach (DataRow myRow in myDt.Rows)
                    {
                        int iNumero = Convert.ToInt32(myRow["Numero"]);

                        if (Convert.ToBoolean(myRow["EnCartera"].ToString())) //Tildado?....
                        {
                            myCadenaSQL = "update Cheques set EnCartera = 0 where Numero = " + iNumero;

                            //Controlar tipo de conexion
                            if (clsGlobales.ConB == null)
                            {
                                clsDataBD.GetSql(myCadenaSQL);
                            }
                            else
                            {
                                clsDataBD.GetSqlB(myCadenaSQL);
                            }
                        }
                    }

                    /********************************************
                    * Actualizo el saldo de la caja de efectivo *
                    ********************************************/
                    clsDataBD.ActualizarSaldoPorCaja("SaldoCheques", Convert.ToDouble(txtCheques.Text)*-1);


                    // Elimino el detalle temporal de cheques
                    string sMyCadenaSQL = "delete from Temporal_DetalleCheques where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario;
                    // Ejecuto la cadena para grabar el movimiento en la Caja
                    if (clsGlobales.ConB == null) { clsDataBD.GetSql(sMyCadenaSQL); } else { clsDataBD.GetSqlB(sMyCadenaSQL); }
                }

                //Fin
                MessageBox.Show("El proceso ha finalizado con exito!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Close
                this.Close();
            }
        }


        #endregion

        #region Metodos RetornarNro y UpdateNro

        private void UpdateNro(int p_Valor, int p_Tipo)
        {
            string myCad = "UPDATE TiposComprobantesCompras SET Numero = " + p_Valor + " Where IdTipoComprobanteCompra = " + p_Tipo;

            if (!(clsGlobales.ConB == null))
            {
                clsDataBD.GetSqlB(myCad);
            }
            else
            {
                clsDataBD.GetSql(myCad);
            }
        }

        private int RetornarNro(int p_Tipo)
        {

            string myCad = "Select Numero from TiposComprobantesCompras Where IdTipoComprobanteCompra = " + p_Tipo;

            DataTable myData = new DataTable();
 
            int nNro = 0;

            if (!(clsGlobales.ConB == null))
            {
               myData = clsDataBD.GetSqlB(myCad);
            }
            else
            {
               myData = clsDataBD.GetSql(myCad);
            }

            //Retornar Valor
            foreach (DataRow myRow in myData.Rows)
            {
                nNro = Convert.ToInt32(myRow["Numero"]);
            }

            return(nNro);
        }

        #endregion

        private void btnCh_Click(object sender, EventArgs e)
        {
            Prama.Formularios.Caja.frmChequesEnCartera myForm = new Formularios.Caja.frmChequesEnCartera();
            myForm.ShowDialog();

            if (clsGlobales.dTotalAAcreditar != 0)
            {
                txtCheques.Text = clsGlobales.dTotalAAcreditar.ToString("#0.00");
            }
        }

        private void chkCheques_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCheques.Checked)
            {

                string sMySQL = "select * from Cheques where Activo = 1 and EnCartera = 1";
                DataTable myTable = new DataTable();
                if (clsGlobales.ConB == null) { myTable = clsDataBD.GetSql(sMySQL); } else { myTable = clsDataBD.GetSqlB(sMySQL); }

                if (myTable.Rows.Count == 0)
                {
                    this.btnCh.Enabled = false;
                }
                else
                {
                    this.btnCh.Enabled = true;
                }

            }
            else
            {
                this.btnCh.Enabled = false;
            }
        }

        private void ckBancos_CheckedChanged(object sender, EventArgs e)
        {
            if (ckBancos.Checked == true)
            {
                cboBancos.Enabled = true;
                txtBancos.Enabled = true;
            }
            else
            {
                cboBancos.Enabled = false;
                txtBancos.Enabled = false;
            }
        }

        private void chkEfectivo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEfectivo.Checked == true)
            {
                txtEfectivo.Enabled = true;
            }
            else
            {
                txtEfectivo.Enabled = false;
            }
        }

        private void txtEfectivo_TextChanged_1(object sender, EventArgs e)
        {
            cCalcularTotalRC();
        }

        private void txtCheques_TextChanged(object sender, EventArgs e)
        {
            cCalcularTotalRC();
        }

        private void txtBancos_TextChanged(object sender, EventArgs e)
        {
            cCalcularTotalRC();
        }

        private void txtSaldoAFavor_TextChanged_1(object sender, EventArgs e)
        {
            cCalcularTotalRC();
        }

       



    }
}

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
    public partial class frmClienteCtaCte : Form
    {   
        // Variable que almacena el Id del Cliente
        int IdCliente = 0;
        clsCLientes myCliente = new clsCLientes();
        string myCadenaSQL = "";
        double cSaldoAplicar = 0;
        double cSaldoAFB = 0;

        public frmClienteCtaCte(int IdCli, double p_SaldoAFB = 0)
        {
            InitializeComponent();

            IdCliente = IdCli;
            cSaldoAFB = p_SaldoAFB;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmClienteCtaCte_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 
            string sMyCadenaSQL = "delete Temporal_DetallePagoCaja where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario;
            if (clsGlobales.ConB == null) { clsDataBD.GetSql(sMyCadenaSQL); } else { clsDataBD.GetSqlB(sMyCadenaSQL); }

            sMyCadenaSQL = "delete Temporal_DetalleCheques where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario;
            if (clsGlobales.ConB == null) { clsDataBD.GetSql(sMyCadenaSQL); } else { clsDataBD.GetSqlB(sMyCadenaSQL); }

            // Cargo los datos del proveedor seleccionado al objeto
            CargarClientes(IdCliente);
            // Paso los datos del proveedor al formulario
            SetearTextBoxCli();
            // Cargo el detalle de la CC del proveedor
            CargarGrilla(IdCliente);
            //Titulo
            this.Text = clsGlobales.cParametro.NombreFantasia + this.Text;
            //SaldoAFavor            
            if (clsGlobales.ConB == null)
            {
                if (myCliente.SaldoAFavor > 0)
                {
                  this.txtSaldoAFavor.Text = myCliente.SaldoAFavor.ToString("#0.00");
                }
            }
            else
            {
                this.txtSaldoAFavor.Text = this.cSaldoAFB.ToString("#0.00");
            }
        }

        #region Método que carga los datos del proveedor a los TextBox del formulario

        private void SetearTextBoxCli()
        {
            // Paso los datos de la clase a los TextBox
            txtCodigoProv.Text = myCliente.Codigo.ToString();
            txtRSoc.Text = myCliente.RazonSocial;
            txtCUIT.Text = myCliente.Cuit;
            txtCondicionIva.Text = myCliente.Condicion;
            txtComprador.Text = clsGlobales.UsuarioLogueado.Usuario;

        }

        #endregion

        #region Método que carga los datos a la grilla

        private void CargarGrilla(int p_IdCli)
        {
            // Creo un nuevo DataTable
            DataTable mDtTable = new DataTable();
            // Pregunto por el tipo de conexión
            if (clsGlobales.ConB == null)
            {
                // Armo la cadena SQL
                myCadenaSQL = "Select * from Vista_eFactura WHERE IdCliente = " + p_IdCli + " AND Saldo >0 AND IdTipoComprobante not in (3,8)";
                // Le asigno al nuevo DataTable los datos de la consulta SQL en blanco
                mDtTable = clsDataBD.GetSql(myCadenaSQL);
            }
            else
            {
                // Armo la cadena SQL
                myCadenaSQL = "Select * from Vista_eFactura_2 WHERE IdCliente = " + p_IdCli + " AND Saldo >0";
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

        private void CargarClientes(int Id)
        {
            // Armo la cadena SQL
            string myCadenaSQL = "select * from Vista_Clientes where Codigo = " + Id;
            // Creo una tabla que me va a almacenar el resultado de la consulta
            DataTable myTablaClientes = clsDataBD.GetSql(myCadenaSQL);
            // Recorro la tabla y le asigo los datos del proveedor al objeto
            foreach (DataRow rowCli in myTablaClientes.Rows)
            {
                myCliente.Codigo = Convert.ToInt32(rowCli["Codigo"]);

                myCliente.RazonSocial = rowCli["RazonSocial"].ToString();
                myCliente.Cuit = rowCli["Cuit"].ToString();
                myCliente.Direccion = rowCli["Direccion"].ToString();
                myCliente.IdCondicionIva = Convert.ToInt32(rowCli["IdTipo"].ToString());
                myCliente.CP = rowCli["CP"].ToString();

                myCliente.Saldo = Convert.ToDouble(rowCli["Saldo"].ToString());
                myCliente.SaldoAFavor = Convert.ToDouble(rowCli["SaldoAFavor"].ToString());
                myCliente.SaldoInicial = Convert.ToDouble(rowCli["SaldoInicial"].ToString());

                myCliente.IdTransporte = Convert.ToInt32(rowCli["IdTransporte"].ToString());
                myCliente.IdCondicionCompra = Convert.ToInt32(rowCli["IdCondicionCompra"].ToString());

                if (!(string.IsNullOrEmpty(rowCli["Barrio"].ToString())))
                {
                    myCliente.Barrio = rowCli["Barrio"].ToString();
                }
                else { myCliente.Barrio = ""; }

                myCliente.Condicion = rowCli["CondicionIva"].ToString();
                myCliente.IdLocalidad = Convert.ToInt32(rowCli["IdLocalidad"].ToString());
                myCliente.Localidad = rowCli["Localidad"].ToString();

                myCliente.IdProvincia = Convert.ToInt32(rowCli["IdProvincia"].ToString());
                myCliente.Provincia = rowCli["Provincia"].ToString();

                myCliente.Telefono = rowCli["Telefono"].ToString();


                if (!(string.IsNullOrEmpty(rowCli["Celular"].ToString())))
                {
                    myCliente.Celular = rowCli["Celular"].ToString();
                }
                else { myCliente.Celular = ""; }


                if (!(string.IsNullOrEmpty(rowCli["Fax"].ToString())))
                {
                    myCliente.Fax = rowCli["Fax"].ToString();
                }
                else { myCliente.Fax = ""; }


                if (!(string.IsNullOrEmpty(rowCli["Mail"].ToString())))
                {
                    myCliente.Mail = rowCli["Mail"].ToString();
                }
                else { myCliente.Mail = ""; }


                if (!(string.IsNullOrEmpty(rowCli["Web"].ToString())))
                {
                    myCliente.Web = rowCli["Web"].ToString();
                }
                else { myCliente.Web = ""; }


                myCliente.IdTipoCliente = Convert.ToInt32(rowCli["IdTipo"].ToString());

                myCliente.IdCondicionIva = Convert.ToInt32(rowCli["IdCondicionIva"].ToString());

                if (!(string.IsNullOrEmpty(rowCli["Observaciones"].ToString())))
                {
                    myCliente.Observaciones = rowCli["Observaciones"].ToString();
                }
                else { myCliente.Observaciones = ""; }


                if (clsGlobales.cValida.EsFecha(rowCli["Nacimiento"].ToString()))
                {
                    myCliente.Nacimiento = rowCli["Nacimiento"].ToString();
                }
                else { myCliente.Nacimiento = ""; }


                if (clsGlobales.cValida.EsFecha(rowCli["Alta"].ToString()))
                {
                    myCliente.Alta = rowCli["Alta"].ToString();
                }
                else { myCliente.Alta = ""; }


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

            if (!(string.IsNullOrEmpty(txtTransf.Text)))
            {
                if (!(Convert.ToDouble(txtTransf.Text) == 0))
                {
                    cTotal += Convert.ToDouble(txtTransf.Text);
                }
            }

            if (!(string.IsNullOrEmpty(txtCredito.Text)))
            {
                if (!(Convert.ToDouble(txtCredito.Text) == 0))
                {
                    cTotal += Convert.ToDouble(txtCredito.Text);
                }
            }
            if (!(string.IsNullOrEmpty(txtDebito.Text)))
            {
                if (!(Convert.ToDouble(txtDebito.Text) == 0))
                {
                    cTotal += Convert.ToDouble(txtDebito.Text);
                }
            }
            if (!(string.IsNullOrEmpty(txtCheques.Text)))
            {
                if (!(Convert.ToDouble(txtCheques.Text) == 0))
                {
                    cTotal += Convert.ToDouble(txtCheques.Text);
                }
            }
            if (!(string.IsNullOrEmpty(txtMP.Text)))
            {
                if (!(Convert.ToDouble(txtMP.Text) == 0))
                {
                    cTotal += Convert.ToDouble(txtMP.Text);
                }
            }
            if (!(string.IsNullOrEmpty(txtCR.Text)))
            {
                if (!(Convert.ToDouble(txtCR.Text) == 0))
                {
                    cTotal += Convert.ToDouble(txtCR.Text);
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
            cSaldoAplicar = Convert.ToDouble(this.txtTotalRC.Text);
            if (cSaldoAplicar == 0)
            {
                return;
            }
            this.gpbDetalle.Enabled=false;
            this.gpbTotal.Enabled = false;
            this.txtSaldoApli.Text = cSaldoAplicar.ToString("#0.00");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string sMyCadenaSQL = "delete from Temporal_DetallePagoCaja where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario;
            if (clsGlobales.ConB == null) { clsDataBD.GetSql(sMyCadenaSQL); } else { clsDataBD.GetSqlB(sMyCadenaSQL); }
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Variable que valida que se selecionó alguna factura
            bool bSeleccionada = false;

            ////Preguntar si hay saldo a aplicar
            //if (cSaldoAplicar == 0)
            //{
            //    MessageBox.Show("No se puede emitir recibo porque el 'Saldo a Aplicar' es 0!", "Informaciòn!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

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
                    GenerarRecibo(bSeleccionada);

                    string sMyCadenaSQL = "delete from Temporal_DetallePagoCaja where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario;
                    if (clsGlobales.ConB == null) { clsDataBD.GetSql(sMyCadenaSQL); } else { clsDataBD.GetSqlB(sMyCadenaSQL); }

                    sMyCadenaSQL = "delete from Temporal_DetalleCheques where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario;
                    if (clsGlobales.ConB == null) { clsDataBD.GetSql(sMyCadenaSQL); } else { clsDataBD.GetSqlB(sMyCadenaSQL); }
                    // Cierro el formulario
                    this.Close();
                }
            }
            else
            {
                // Si seleccionó alguna fatura
                GenerarRecibo(bSeleccionada);

                string sMyCadenaSQL = "delete from Temporal_DetallePagoCaja where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario;
                if (clsGlobales.ConB == null) { clsDataBD.GetSql(sMyCadenaSQL); } else { clsDataBD.GetSqlB(sMyCadenaSQL); }
                sMyCadenaSQL = "delete from Temporal_DetalleCheques where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario;
                if (clsGlobales.ConB == null) { clsDataBD.GetSql(sMyCadenaSQL); } else { clsDataBD.GetSqlB(sMyCadenaSQL); }
                // Cierro el formulario
                this.Close();
            }

            
            
        }

        private void txtSaldoAFavor_TextChanged(object sender, EventArgs e)
        {
            cCalcularTotalRC();
        }

        #region Método que genera el recibo

        private void GenerarRecibo(bool p_bSeleccionada = false)
        {
            //Variables
            int LastIdRecibo = 0;
            double cSumaResto = 0;

            double efvo = 0;
            double mp = 0;
            double transf = 0;
            double Cheques = 0;
            double contra = 0;
            double Debito = 0;
            double Credito = 0;
            double saldofav = 0;

            //Validar
            if (clsGlobales.cValida.IsNumeric(txtEfectivo.Text))
            { efvo = Convert.ToDouble(txtEfectivo.Text);}

            if (clsGlobales.cValida.IsNumeric(this.txtMP.Text))
            { mp = Convert.ToDouble(this.txtMP.Text); }
            
            if (clsGlobales.cValida.IsNumeric(this.txtTransf.Text))
            { transf = Convert.ToDouble(this.txtTransf.Text); }
            
            if (clsGlobales.cValida.IsNumeric(this.txtCheques.Text))
            { Cheques = Convert.ToDouble(this.txtCheques.Text); }
            
            if (clsGlobales.cValida.IsNumeric(this.txtCR.Text))
            { contra = Convert.ToDouble(this.txtCR.Text); }
            
            if (clsGlobales.cValida.IsNumeric(this.txtDebito.Text))
            { Debito = Convert.ToDouble(this.txtDebito.Text); }

            if (clsGlobales.cValida.IsNumeric(this.txtCredito.Text))
            { Credito = Convert.ToDouble(this.txtCredito.Text); }

            if (clsGlobales.cValida.IsNumeric(this.txtSaldoAFavor.Text))
            { saldofav = Convert.ToDouble(this.txtSaldoAFavor.Text); }

            // Pido al operador la confirmación
            DialogResult myRespuesta = MessageBox.Show("¿Desea generar el Recibo en base al Detalle de Pago y/o Comprobantes especificados?", "Confirmar!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Si la respuesta es si
            if (myRespuesta == DialogResult.Yes)
            {
                // Monto total sin saldo a favor para la caja
                double dMonto = Debito + Credito + efvo + contra + mp + Cheques + transf;
                // Paso los datos a la clase para generar el movimiento de caja
                
                
                
                string myCad = "";

                if (clsGlobales.ConB == null)
                {

                    //Insert del Recibo
                    myCad = "INSERT INTO Recibos (Fecha, IdCliente, Punto, Nro, PuntoNro, Total, Activo, Efvo, Transf, Debito, Credito, MP, Contra, Cheques, SaldoFavor) VALUES ('" + DateTime.Now.ToShortDateString() + "'," +
                                   txtCodigoProv.Text + "," + clsGlobales.cParametro.PtoVtaPorDefecto + "," +
                                   (clsDataBD.getUltComp("Ult_Recibo", clsGlobales.cParametro.PtoVtaPorDefecto, 0) + 1) + ",'" +
                                   clsGlobales.cParametro.PtoVtaPorDefecto.ToString("D4") + "-" + (clsDataBD.getUltComp("Ult_Recibo", clsGlobales.cParametro.PtoVtaPorDefecto, 0) + 1).ToString("D8") + "'," +
                                   (Convert.ToDouble(txtTotalRC.Text)-saldofav) + ",1," + 
                                   + efvo + "," +
                                   + transf + "," +
                                   + Debito + "," +
                                   + Credito + "," +
                                   + mp + "," +
                                   + contra + "," +
                                   + Cheques + "," +
                                   + saldofav + ")";

                    //Ejecutar consulta
                    clsDataBD.GetSql(myCad);

                    //Ultimo Id Recibo
                    LastIdRecibo = clsDataBD.RetornarMax("Recibos", "IdRecibo");

                    // Limpio los datos de la caja
                    Prama.Clases.clsCaja.LimpiarCaja();
                    // Paso el número del recibo a la clase de la caja
                    Prama.Clases.clsCaja.Entrada = true;
                    Prama.Clases.clsCaja.Fecha = DateTime.Today;
                    Prama.Clases.clsCaja.Debito = Debito;
                    Prama.Clases.clsCaja.Credito = Credito;
                    Prama.Clases.clsCaja.Efectivo = efvo;
                    Prama.Clases.clsCaja.Contra = contra;
                    Prama.Clases.clsCaja.Monto = dMonto;
                    Prama.Clases.clsCaja.Movimiento = txtRSoc.Text;
                    Prama.Clases.clsCaja.MP = mp;
                    Prama.Clases.clsCaja.Cheques = Cheques;
                    Prama.Clases.clsCaja.Transferencia = transf;
                    Prama.Clases.clsCaja.IdCajaImputacion = 1;
                    Prama.Clases.clsCaja.Comprobante = clsGlobales.cParametro.PtoVtaPorDefecto.ToString("D4") + "-" + (clsDataBD.getUltComp("Ult_Recibo", clsGlobales.cParametro.PtoVtaPorDefecto, 0) + 1).ToString("D8");


                    //Actualizar el numero de Recibo en Tabla AFIP
                    string mySQL = "UPDATE PuntosVentaAFIP SET Ult_Recibo = " + (clsDataBD.getUltComp("Ult_Recibo", clsGlobales.cParametro.PtoVtaPorDefecto, 0) + 1) + " WHERE Punto = " + clsGlobales.cParametro.PtoVtaPorDefecto;
                    clsDataBD.GetSql(mySQL);

                }
                else
                {

                    // Limpio los datos de la caja
                    Prama.Clases.clsCaja.LimpiarCaja();
                    // Paso el número del recibo a la clase de la caja
                    Prama.Clases.clsCaja.Entrada = true;
                    Prama.Clases.clsCaja.Fecha = DateTime.Today;
                    Prama.Clases.clsCaja.Debito = Debito;
                    Prama.Clases.clsCaja.Credito = Credito;
                    Prama.Clases.clsCaja.Efectivo = efvo;
                    Prama.Clases.clsCaja.Contra = contra;
                    Prama.Clases.clsCaja.Monto = dMonto;
                    Prama.Clases.clsCaja.Movimiento = txtRSoc.Text;
                    Prama.Clases.clsCaja.MP = mp;
                    Prama.Clases.clsCaja.Cheques = Cheques;
                    Prama.Clases.clsCaja.Transferencia = transf;
                    Prama.Clases.clsCaja.IdCajaImputacion = 1;
                    Prama.Clases.clsCaja.Comprobante = clsGlobales.cParametro.PtoVtaPorDefecto.ToString("D4") + "-" + (clsDataBD.getUltComp("Ult_Recibo", clsGlobales.cParametro.PtoVtaPorDefecto, 0) + 1).ToString("D8");

                    
                    //Insert del Recibo
                    myCad = "INSERT INTO Recibos (Fecha, IdCliente, Punto, Nro, PuntoNro, Total, Activo, Efvo, Transf, Debito, Credito, MP, Contra, Cheques, SaldoFavor) VALUES ('" + DateTime.Now.ToShortDateString() + "'," +
                                  txtCodigoProv.Text + "," + clsGlobales.cParametro.PtoVtaPorDefecto + "," +
                                  (clsDataBD.RetornarUltimoIdB("Recibos","Nro") + 1) + ",'" +
                                  clsGlobales.cParametro.PtoVtaPorDefecto.ToString("D4") + "-" + (clsDataBD.RetornarUltimoIdB("Recibos", "Nro") + 1).ToString("D8") + "'," +
                                   (Convert.ToDouble(txtTotalRC.Text) - saldofav) + ",1," +
                                   +efvo + "," +
                                   +transf + "," +
                                   +Debito + "," +
                                   +Credito + "," +
                                   +mp + "," +
                                   +contra + "," +
                                   +Cheques + "," +
                                   +saldofav + ")";
                    
                    //Ejecutar consulta
                    clsDataBD.GetSqlB(myCad);

                    //Ultimo Id Recibo
                    LastIdRecibo = clsDataBD.RetornarUltimoIdB("Recibos", "IdRecibo");

                }

                //Recorrer la grilla y ver facturas seleccionadas
                //Updatear saldo de la factura
                foreach (DataGridViewRow myRow in this.dgvComprobantes.Rows)
                {
                    if (Convert.ToBoolean(myRow.Cells["Elegido"].Value) == true)
                    {

                        //Updatear saldo con resto en la factura
                        myCad = "UPDATE eFactura SET Saldo = " + Convert.ToDouble(myRow.Cells["Resto"].Value.ToString()) +
                                " WHERE IdFactura = " + Convert.ToInt32(myRow.Cells["IdFactura"].Value.ToString());

                        if (clsGlobales.ConB == null)
                        {
                            clsDataBD.GetSql(myCad);
                        }
                        else
                        {
                            clsDataBD.GetSqlB(myCad);
                        }

                        //Guardar el detalle del recibo
                        myCad = "INSERT INTO RecibosDetalle (IdRecibo, Fecha,Punto,Nro,Comprobante,IdTipoComprobante,Importe) VALUES (" + LastIdRecibo + ",'" +
                                myRow.Cells["FechaR"].Value + "'," + Convert.ToInt32(myRow.Cells["Punto"].Value.ToString()) + "," +
                                +Convert.ToInt32(myRow.Cells["Nro"].Value.ToString()) + ",'" + myRow.Cells["Numero"].Value.ToString() + "'," +
                                +Convert.ToInt32(myRow.Cells["IdTipoComprobante"].Value.ToString()) + "," + Convert.ToDouble(myRow.Cells["Aplicado"].Value.ToString()) + ")";

                        if (clsGlobales.ConB == null)
                        {
                            clsDataBD.GetSql(myCad);
                        }
                        else
                        {
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


                if (clsGlobales.ConB == null)
                {
                     myCad = "UPDATE Clientes SET SaldoAFavor = 0 WHERE IdCliente = " + myCliente.Codigo;
                    clsDataBD.GetSql(myCad);

                    if (p_bSeleccionada == false && Convert.ToDouble(txtSaldoApli.Text) > 0)
                    {
                        myCad = "UPDATE Clientes SET SaldoAFavor = " + Convert.ToDouble(txtSaldoApli.Text) + " WHERE IdCliente = " + myCliente.Codigo;
                        clsDataBD.GetSql(myCad);
                    }
                    else if (p_bSeleccionada == true && Convert.ToDouble(txtSaldoApli.Text) >0)
                    {
                        ///ojo
                        myCad = "UPDATE Clientes SET SaldoAFavor = " + Convert.ToDouble(txtSaldoApli.Text) + " WHERE IdCliente = " + myCliente.Codigo;
                        clsDataBD.GetSql(myCad);
                        //

                        myCad = "UPDATE Clientes SET Saldo = " + cSumaResto + " WHERE IdCliente = " + myCliente.Codigo;
                        clsDataBD.GetSql(myCad); 
                    }
                    else if (p_bSeleccionada == true && Convert.ToDouble(txtSaldoApli.Text)==0)
                    {
                        myCad = "UPDATE Clientes SET Saldo = " + cSaldoApliUpdate + " WHERE IdCliente = " + myCliente.Codigo;
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
                        if (Convert.ToInt32(txtCodigoProv.Text) == Convert.ToInt32(rowSaldos["idCliente"]))
                        {
                            bEncontrado = true;
                            break;
                        }
                    }

                    if (!(bEncontrado))
                    {
                        myCad = "insert into SaldoCliProv (IdCliente, SaldoCli, IdProveedor, SaldoProv, SaldoInicial, SaldoAFavor) values (" +
                                        myCliente.Codigo + ",0, 0, 0, 0, 0)"; 
                        // Ejecuto la consulta 
                        clsDataBD.GetSqlB(myCad); 
                    }

                    myCad = "UPDATE SaldoCliProv SET SaldoAFavor = 0 WHERE IdCliente = " + myCliente.Codigo;
                    clsDataBD.GetSqlB(myCad);
                    
                    bEncontrado = false;


                    //Casos de Uso
                    if (p_bSeleccionada == false && Convert.ToDouble(txtSaldoApli.Text) > 0)
                    {
                        myCad = "UPDATE SaldoCliProv SET SaldoAFavor = " + Convert.ToDouble(txtSaldoApli.Text) + " WHERE IdCliente = " + myCliente.Codigo;
                        clsDataBD.GetSqlB(myCad);
                    }
                    else if (p_bSeleccionada == true && Convert.ToDouble(txtSaldoApli.Text) > 0)
                    {
                        //OJO
                        myCad = "UPDATE SaldoCliProv SET SaldoAFavor = " + Convert.ToDouble(txtSaldoApli.Text) + " WHERE IdCliente = " + myCliente.Codigo;
                        clsDataBD.GetSqlB(myCad);
                        //

                        myCad = "UPDATE SaldoCliProv SET SaldoCli = " + cSumaResto + " WHERE IdCliente = " + myCliente.Codigo;
                        clsDataBD.GetSqlB(myCad);
                    }
                    else if (p_bSeleccionada == true && Convert.ToDouble(txtSaldoApli.Text) == 0)
                    {
                        myCad = "UPDATE SaldoCliProv SET SaldoCli = " + cSaldoApliUpdate + " WHERE IdCliente = " + myCliente.Codigo;
                        clsDataBD.GetSqlB(myCad);
                    }
                    //**

                }

                // Grabo el movimiento de la caja
                Prama.Clases.clsCaja.CargarCaja(LastIdRecibo);

                //Fin
                MessageBox.Show("El proceso ha finalizado con exito!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Close
                this.Close();
            }
        }


        #endregion

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            // Par'ametro de entrada
            // 1 Transferencias
            // 2 Mercado Pago
            // 3 Débito
            // 4 Credito
            
            Prama.Formularios.Caja.frmImputacionPago myForm = new Caja.frmImputacionPago(1, "TRANSFERENCIAS");
            myForm.ShowDialog();

            txtTransf.Text = clsGlobales.dTotalAAcreditar.ToString("#0.00");
        }

        private void btnMP_Click(object sender, EventArgs e)
        {
            // Par'ametro de entrada
            // 1 Transferencias
            // 2 Mercado Pago
            // 3 Débito
            // 4 Credito

            Prama.Formularios.Caja.frmImputacionPago myForm = new Caja.frmImputacionPago(2, "MERCADO PAGO");
            myForm.ShowDialog();

            txtMP.Text = clsGlobales.dTotalAAcreditar.ToString("#0.00");
        }

        private void btnDebito_Click(object sender, EventArgs e)
        {
            // Par'ametro de entrada
            // 1 Transferencias
            // 2 Mercado Pago
            // 3 Débito
            // 4 Credito

            Prama.Formularios.Caja.frmImputacionPago myForm = new Caja.frmImputacionPago(3, "DÉBITO");
            myForm.ShowDialog();

            txtDebito.Text = clsGlobales.dTotalAAcreditar.ToString("#0.00");
        }

        private void btnCredito_Click(object sender, EventArgs e)
        {
            // Par'ametro de entrada
            // 1 Transferencias
            // 2 Mercado Pago
            // 3 Débito
            // 4 Credito

            Prama.Formularios.Caja.frmImputacionPago myForm = new Caja.frmImputacionPago(4, "CRÉDITO");
            myForm.ShowDialog();

            txtCredito.Text = clsGlobales.dTotalAAcreditar.ToString("#0.00");
        }

        private void btnCheques_Click(object sender, EventArgs e)
        {
            Prama.Formularios.Caja.frmCheques myForm = new Caja.frmCheques();
            myForm.ShowDialog();

            txtCheques.Text = clsGlobales.dTotalAAcreditar.ToString("#0.00");
        }
    }
}

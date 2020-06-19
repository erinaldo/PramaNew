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
    public partial class frmCajaABM : Form
    {
        string myCadenaSQL = "";
        // Bandera que controla el comportamiento de los combos
        bool bBandera = false;

        double Efectivo = 0;
        double Cheques = 0;
        double Banco = 0;
        double dTotal = 0;
        bool Entrada = false;

        int iAsocioacion = 0;
        // Variable que almacena si el movimiento es una entrada
        int iEsEntrada = 0;
        // Variable que almacena el id de la caja
        int iIdCaja = 0;
        //Bancos
        double bancos = 0;
        // Variabe que almacena el tipo de movimiento de caja // 3 para un ingreso, 4 para un egreso
        int iIdTipoMovCaja = 0;
        // Variable para la fecha
        DateTime Fecha = DateTime.Now;
        // Variable para la descroción del movimiento
        string Movimiento = "";
            
        
        public frmCajaABM()
        {
            InitializeComponent();
        }

        private void frmCajaABM_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 
			
            txtUsuario.Text = clsGlobales.UsuarioLogueado.Usuario;

            clsDataBD.CargarCombo(cboBancos, "CajaAsociacionesCuentas", "CajaAsociaciones", "IdCajaAsociaciones","",1);
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

        private void cboTipoMov_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboTipoMov.Text == "")
            {
                this.cboImputacion.Enabled = false;
                cboImputacion.SelectedIndex = -1;
            }
            else
            {
                this.cboImputacion.Enabled = true;
                cboImputacion.SelectedIndex = -1;
            }

            if (cboTipoMov.Text == "Ingresos")
            {
                // Cargo el combo de imputaciones
                clsDataBD.CargarCombo(cboImputacion, "Imputaciones", "Imputacion", "IdImputacion", "CodigoInterno > 40000 and CodigoInterno < 50000 and Activo=1");
                // Dejo vacía la selección
                cboImputacion.SelectedIndex = -1;
            }
            else
            {
                // Cargo el combo de imputaciones
                clsDataBD.CargarCombo(cboImputacion, "Imputaciones", "Imputacion", "IdImputacion", "CodigoInterno > 50000 and Activo=1");
                // Dejo vacía la selección
                cboImputacion.SelectedIndex = -1;
            }
        }

        private void cboImputacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bBandera)
            {
                if (cboImputacion.Text == "")
                {
                    txtImputacion.Text = "";
                    return;
                }

                // Armo la cadena SQL
                myCadenaSQL = "select * from Imputaciones where IdImputacion = " + cboImputacion.SelectedValue;
                // Paso los datos de la consulta a una tabla
                DataTable DtMyImputacion = clsDataBD.GetSql(myCadenaSQL);
                // Recorro la tabla para pasar el código de la imputación al textbox
                foreach (DataRow row in DtMyImputacion.Rows)
                {
                    txtImputacion.Text = row["CodigoImputacion"].ToString();
                }
            }
        }

        private void cboImputacion_Click(object sender, EventArgs e)
        {
            bBandera = true;
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            bool bValida;

            bValida = bEsValido();

            if (!(bValida))
            {
                return;
            }

            //GRABAR

            // No hay recibos comprobantes seleccionados
            DialogResult myRespuesta = MessageBox.Show("Va a realizar un movimiento de caja. ¿Desea Continuar?", "CONFIRMACIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Si desea continuar
            if (myRespuesta == DialogResult.Yes)
            {
                // Tomos los importes finales del formulario en las variables
                Efectivo = Convert.ToDouble(txtEfectivo.Text);
                Banco = Convert.ToDouble(txtBancos.Text);
                Cheques = Convert.ToDouble(txtCheques.Text);

                // Valido que la acumulación de importes sea igual al ingresado en el textBox
                if ((Efectivo + Banco + Cheques) != Convert.ToDouble(txtTotal.Text))
                {
                    MessageBox.Show("La suma de los fondos debe ser igual al total del movimiento. Rectifique por favor !", "REVISAR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Paso a la variable si es un Ingreso o un Egreso
                if (cboTipoMov.Text == "Ingresos")
                {
                    Entrada = true;
                    iIdTipoMovCaja = 3;
                }
                else
                {
                    Entrada = false;
                    iIdTipoMovCaja = 4;
                }

                // Si es entrada, le asigno el uno para la cadena SQL
                if (Entrada)
                {
                    iEsEntrada = 1;
                }

                // Si no es una entrada
                if (!(Entrada))
                {
                    // Paso a todos los montos a negativo
                    Efectivo = Efectivo * (-1);
                    Banco = Banco * (-1);
                    Cheques = Cheques * (-1);
                }

                // Acumulo el total del movimiento
                dTotal = Efectivo + Banco + Cheques;
                // Paso la descripción del movimiento
                Movimiento = txtDetalle.Text.ToUpper();


                // Grabar caja
                // Armo la cadena SQL
                string myCadenaSQL = "insert into Caja(Entrada, MontoTotal, IdCajaImputacion, FechaS, Fecha, Efectivo, " +
                                      "Cheques, Movimiento, Bancos, Comprobante, Debito, Credito, Transferencia, MP, Contra) values (" +
                                        iEsEntrada + ", " +
                                        dTotal + ", " +
                                        iIdTipoMovCaja + ", '" +
                                        clsValida.ConvertirFecha(Fecha) + "', '" +
                                        Fecha.ToShortDateString() + "', " +
                                        Efectivo + ", " +
                                        Cheques + ", '" +
                                        Movimiento + "', " +
                                        Banco + ", 0, 0, 0, 0, 0, 0)";
                // Ejecuto la cadena para grabar el movimiento en la Caja
                if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaSQL); } else { clsDataBD.GetSqlB(myCadenaSQL); }

                // Averiguo el Id del movimiento de caja recién creado
                if (clsGlobales.ConB == null)
                { iIdCaja = clsDataBD.RetornarUltimoId("Caja", "IdCaja"); }
                else { iIdCaja = clsDataBD.RetornarUltimoIdB("Caja", "IdCaja"); }

                /********************************************
                * Cargo los movimientos a la tabla Efectivo *
                ********************************************/
                // Si Hay efectivo 
                if (Efectivo != 0)
                {
                    // Armo la cadena SQL
                    myCadenaSQL = "insert into CajaEfectivo (IdCaja, IdCajaEfectivoTipo, Importe) values (" +
                                    iIdCaja + ", 1," +
                                    Efectivo + ")";
                    // Ejecuto la cadena para grabar el movimiento en la Caja
                    if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaSQL); } else { clsDataBD.GetSqlB(myCadenaSQL); }

                    /********************************************
                    * Actualizo el saldo de la caja de efectivo *
                    ********************************************/
                    clsDataBD.ActualizarSaldoPorCaja("SaldoEfectivo", Efectivo);

                }

                // Si es contrareembolso el Id de CajaEfectivoTipo es 2
                if (Banco != 0)
                {
                    iAsocioacion = Convert.ToInt32(cboBancos.SelectedValue);

                    // CAJA BANCOS
                    myCadenaSQL = "insert into CajaBancos (IdCajaAsociacion, IdCaja, Importe) values (" +
                                    iAsocioacion + ", " + iIdCaja + ", " +
                                    Banco + ")";
                    // Ejecuto la cadena para grabar el movimiento en la Caja
                    if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaSQL); } else { clsDataBD.GetSqlB(myCadenaSQL); }

                    /********************************************
                    * Actualizo el saldo de la caja de BAncos *
                    ********************************************/
                    clsDataBD.ActualizarSaldoPorCaja("SaldoBancos", Banco);
                }

                //CAJA ASOCIACION
                myCadenaSQL = "Update CajaAsociacionesCuentas SET Saldo+= " + bancos + " WHERE IdCajaAsociaciones = " + iAsocioacion;
                // Ejecuto la cadena para grabar el movimiento en la Caja
                if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaSQL); } else { clsDataBD.GetSqlB(myCadenaSQL); }

                //CHEQUES
                if (Cheques != 0)
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

                    /********************************************
                    * Actualizo el saldo de la caja de efectivo *
                    ********************************************/
                    clsDataBD.ActualizarSaldoPorCaja("SaldoCheques", Cheques);


                    // Elimino el detalle temporal de cheques
                    string sMyCadenaSQL = "delete from Temporal_DetalleCheques where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario;
                    // Ejecuto la cadena para grabar el movimiento en la Caja
                    if (clsGlobales.ConB == null) { clsDataBD.GetSql(sMyCadenaSQL); } else { clsDataBD.GetSqlB(sMyCadenaSQL); }
                    
                }
                // Cierro el formulario
                this.Close();
            }
        }

        private bool bEsValido()
        {
            bool bVal = true;

            //Fecha
            if (dtpFecha.Value > DateTime.Now)
            {
                MessageBox.Show("La 'Fecha del Movimiento' no puede ser mayor a la actual!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpFecha.Focus();
                bVal = false;
                return bVal;
            }

            //TipoMovimiento
            if (cboTipoMov.Text == "")
            {
                MessageBox.Show("Debe completar el dato 'Tipo de Movimiento'!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTipoMov.Focus();
                bVal = false;
                return bVal;            
            }

            //Imputacion
            if (cboImputacion.Text == "")
            {
                MessageBox.Show("Debe completar el dato 'Imputación'!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboImputacion.Focus();
                bVal = false;
                return bVal;
            }

            //TOTAL
            if (txtTotal.Text == "")
            {
                MessageBox.Show("Debe completar el dato 'Total'!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTotal.Focus();
                bVal = false;
                return bVal;
            }
            else if (Convert.ToDouble(txtTotal.Text) == 0)
            {
                MessageBox.Show("El importe para el dato 'TOTAL' debe ser distinto de 0!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTotal.Focus();
                bVal = false;
                return bVal;
            }

            //Descripcion
            if (txtDetalle.Text == "")
            {
                MessageBox.Show("Debe completar el dato 'Descripcion'!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDetalle.Focus();
                bVal = false;
                return bVal;
            }

            //Origen y Destino de los Fondos
            if (!(chkEfectivo.Checked || chkCheques.Checked || ckBancos.Checked))
            {
                MessageBox.Show("Debe elegir 'Origen o Destino de los Fondos'!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                chkEfectivo.Focus();
                bVal = false;
                return bVal; 
            }

            //Marco efectivo y no cargo nada?
            if (chkEfectivo.Checked && txtEfectivo.Text == "")
            {
                MessageBox.Show("Debe completar el importe para 'Efectivo'!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEfectivo.Focus();
                bVal = false;
                return bVal;
            }
            else if (chkEfectivo.Checked && Convert.ToDouble(txtEfectivo.Text) == 0)
            {
                MessageBox.Show("El importe para 'Efectivo' debe ser distinto de 0!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEfectivo.Focus();
                bVal = false;
                return bVal;
            }

            //Marco Bancos y no cargo nada?
            if (ckBancos.Checked && cboBancos.Text == "")
            {
                MessageBox.Show("Debe seleccionar la cuenta para 'Bancos'!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboBancos.Focus();
                bVal = false;
                return bVal;
            }
            else if (ckBancos.Checked  && txtBancos.Text == "")
            {
                MessageBox.Show("Debe completar el importe para 'Bancos'!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBancos.Focus();
                bVal = false;
                return bVal;
            }
            else if (ckBancos.Checked  && Convert.ToDouble(txtBancos.Text) == 0)
            {
                MessageBox.Show("El importe para 'Bancos' debe ser distinto de 0!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBancos.Focus();
                bVal = false;
                return bVal;
            }

            //Marco Cheques y no hay detalle?
            if (chkCheques.Checked && txtCheques.Text == "")
            {
                MessageBox.Show("Debe completar el detalle para 'Cheques'!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnCh.Focus();
                bVal = false;
                return bVal;
            }


            //Retornar Valor
            return bVal;
        }

        private void btnCh_Click(object sender, EventArgs e)
        {
            Prama.Formularios.Caja.frmChequesEnCartera myForm = new Formularios.Caja.frmChequesEnCartera();
            myForm.ShowDialog();

            if (clsGlobales.dTotalAAcreditar != 0)
            {
                txtCheques.Text = clsGlobales.dTotalAAcreditar.ToString("#0.00");
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

        private void txtBancos_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCheques_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtTotal_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}

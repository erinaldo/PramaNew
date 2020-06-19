using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prama.Formularios.Compras
{
    public partial class frmComprasProvPagos : Form
    {
        
        #region Variables del formulario

        // Variables que almacenas los datos que vienen por parámetro
        int IdProveedor = 0;
        int IdComprobante = 0;

        // Varioable para las cadenas SQL
        string myCadenaSQL = "";

        // Nueva orden de pago
        Prama.Clases.clsProveedoresOP nuevaOrden = new Clases.clsProveedoresOP();
        // Nuevo comprobante
        Prama.Clases.clsGastosComunes myComprobanteViejo = new Prama.Clases.clsGastosComunes();
        // Nuevo Proveedor
        clsProveedores myProveedor = new clsProveedores();

        // Variable que almacena el tipo de conexion
        int iConex = 0;

        #endregion

        #region Constructor

        public frmComprasProvPagos(int IdProv, int IdCompr)
        {
            // Paso a las variables del form los datos de los parámetros
            IdProveedor = IdProv;
            IdComprobante = IdCompr;
            // Inicializo los componentes del formulario
            InitializeComponent();

            // Si es negro
            if (!(clsGlobales.ConB == null))
            {
                // La variable vale 1
                iConex = 1;
            }
            else
            {
                iConex = 0;
            }
        }

        #endregion

        #region Eventos de Formulario

        #region Evento Load

        private void frmComprasProvPagos_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            // Paso los datos del comprobante al formulario
            CargarComprobante();
            // Deshabilito el botón aceptgar
            HabilitarAceptar();

            this.Text = clsGlobales.cFormato.getTituloVentana() + " - NUEVA ORDEN DE PAGO A PROVEEDORES ";

        }

        #endregion

        #region Eventos de los botones

        #region Evento Click del botón Cancelar

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Cierro el formulario
            this.Close();
        }

        #endregion

        #region Evento Click del botón Aceptar

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Valido los datos
            PasarDatosAClase();

            // Si el pago es parcial, pido confirmación
            if (Convert.ToDouble(txtSaldo.Text) > Convert.ToDouble(txtTotalOP.Text))
            {
                // Pregunto si quiere hacer un pago parcial
                DialogResult RespuestaParcial = MessageBox.Show("Desea hacer un pago parcial ?", "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (RespuestaParcial == DialogResult.Yes)
                {
                    // Grabo el pago a cuenta
                    GrabarPago();
                }
                else
                {
                    return;
                }
            }
            else
            {
                // Grabo el pago a cuenta
                GrabarPago();
            }
        }

        #endregion

        #endregion

        #region Eventos KeyPress y Leave de los TextBox

        private void txtEfectivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 44)
            {
                e.KeyChar = Convert.ToChar(46);
                ch = e.KeyChar;

            }
            //PUNTO DECIMAL. N.
            if (ch == 46 && base.Text.IndexOf('.') != -1)
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

            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                this.txtTransf.Focus();
            }
        }

        private void txtEfectivo_Leave(object sender, EventArgs e)
        {
            // Si está vacío, le pongo 0
            if (txtEfectivo.Text == "")
            {
                txtEfectivo.Text = "0.00";
            }
            // Doy formato al número
            this.txtEfectivo.Text = Convert.ToDouble(this.txtEfectivo.Text).ToString("#0.00");
            // Calculo el nuevo total
            CalcularTotal();
            // Habilito el botón aceptar
            HabilitarAceptar();
        }

        private void txtTransf_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 44)
            {
                e.KeyChar = Convert.ToChar(46);
                ch = e.KeyChar;

            }
            //PUNTO DECIMAL. N.
            if (ch == 46 && base.Text.IndexOf('.') != -1)
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

            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                this.txtPropios.Focus();
            }
        }

        private void txtTransf_Leave(object sender, EventArgs e)
        {
            // Si está vacío, le pongo 0
            if (txtTransf.Text == "")
            {
                txtTransf.Text = "0.00";
            }
            
            // Doy formato al número
            this.txtTransf.Text = Convert.ToDouble(this.txtTransf.Text).ToString("#0.00");
            // Calculo el nuevo total
            CalcularTotal();
            // Habilito el botón aceptar
            HabilitarAceptar();
        }

        private void txtPropios_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 44)
            {
                e.KeyChar = Convert.ToChar(46);
                ch = e.KeyChar;

            }
            //PUNTO DECIMAL. N.
            if (ch == 46 && base.Text.IndexOf('.') != -1)
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

            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                this.txtTerceros.Focus();
            }
        }

        private void txtPropios_Leave(object sender, EventArgs e)
        {
            // Si está vacío, le pongo 0
            if (txtPropios.Text == "")
            {
                txtPropios.Text = "0.00";
            }
            
            // Doy formato al número
            this.txtPropios.Text = Convert.ToDouble(this.txtPropios.Text).ToString("#0.00");
            // Calculo el nuevo total
            CalcularTotal();
            // Habilito el botón aceptar
            HabilitarAceptar();
        }

        private void txtTerceros_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 44)
            {
                e.KeyChar = Convert.ToChar(46);
                ch = e.KeyChar;

            }
            //PUNTO DECIMAL. N.
            if (ch == 46 && base.Text.IndexOf('.') != -1)
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

            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                this.btnAceptar.Focus();
            }
        }

        private void txtTerceros_Leave(object sender, EventArgs e)
        {
            // Si está vacío, le pongo 0
            if (txtTerceros.Text == "")
            {
                txtTerceros.Text = "0.00";
            }
            
            // Doy formato al número
            this.txtTerceros.Text = Convert.ToDouble(this.txtTerceros.Text).ToString("#0.00");
            // Calculo el nuevo total
            CalcularTotal();
            // Habilito el botón aceptar
            HabilitarAceptar();
        }

        #endregion

        #endregion

        #region Métodos del formulario

        #region Método que pasa los datos del Form a la clase

        private void PasarDatosAClase()
        {
            nuevaOrden.Activo = 1;
            nuevaOrden.ChequesPropios = Convert.ToDouble(txtPropios.Text);
            nuevaOrden.ChequesTerceros = Convert.ToDouble(txtTerceros.Text);
            nuevaOrden.Efectivo = Convert.ToDouble(txtEfectivo.Text);
            nuevaOrden.Fecha = clsValida.ConvertirFecha(dtpFechaPago.Value);
            nuevaOrden.FechaReal = dtpFechaPago.Value;
            nuevaOrden.IdProveedor = IdProveedor;
            nuevaOrden.Numero = txtNumero.Text;
            nuevaOrden.Total = Convert.ToDouble(txtTotalOP.Text);
            nuevaOrden.Transferencia = Convert.ToDouble(txtTransf.Text);
            nuevaOrden.Usuario = txtPagador.Text;

        }

        #endregion

        #region Método que pasa los datos del comprobante al formulario

        private void CargarComprobante()
        {
            // Armo la cadena SQL para traer los datos del comprobante
            myCadenaSQL = "select * from Vista_ComprobantesCompras where Id = " + IdComprobante;

            // Creo un nuevo DataTable
            DataTable myComprobante = new DataTable();
            // Pregunto por el tipo de conexión
            if (iConex == 0)
            {
                // Le asigno al nuevo DataTable los datos de la consulta SQL en blanco
                myComprobante = clsDataBD.GetSql(myCadenaSQL);
            }
            else
            {
                // Le asigno al nuevo DataTable los datos de la consulta SQL en negro
                myComprobante = clsDataBD.GetSqlB(myCadenaSQL);
            }
            
            // Recorro la tabla y paso los datos a la clase
            foreach (DataRow rowComp in myComprobante.Rows)
            {

                myComprobanteViejo.IdTipoComprobanteCompra = Convert.ToInt32(rowComp["IdTipo"]);
                myComprobanteViejo.IdProveedor = Convert.ToInt32(rowComp["IdProveedor"]);

                myComprobanteViejo.Fecha = (rowComp["Fecha"]).ToString();
                myComprobanteViejo.FechaReal = Convert.ToDateTime(rowComp["FechaReal"]);
                myComprobanteViejo.Numero = (rowComp["Numero"]).ToString();

                myComprobanteViejo.Activo = Convert.ToInt32(rowComp["Activo"]);
                myComprobanteViejo.Descripcion = (rowComp["Descripcion"]).ToString();
                myComprobanteViejo.Usuario = (rowComp["Usuario"]).ToString();
                // Campos exclusivos de la factura de compras
                myComprobanteViejo.Total = Convert.ToDouble(rowComp["Total"]);
                myComprobanteViejo.Saldo = Convert.ToDouble(rowComp["Saldo"]);

                if (Convert.ToBoolean(rowComp["Compra"]))
                {
                    myComprobanteViejo.Descripcion = "FACTURA DE COMPRA";
                }
                else
                {
                    myComprobanteViejo.Descripcion = "FACTURA DE GASTO";
                }

            }

            // Paso los datos de la clase al form
            this.dtpFecha.Value = myComprobanteViejo.FechaReal;
            this.txtNumero.Text = myComprobanteViejo.Numero;
            this.txtTotalFactura.Text = myComprobanteViejo.Total.ToString("#0.00");
            this.txtSaldo.Text = myComprobanteViejo.Saldo.ToString("#0.00");
            this.txtDetalle.Text = myComprobanteViejo.Descripcion;
            this.txtComprador.Text = myComprobanteViejo.Usuario;

            this.dtpFechaPago.Value = DateTime.Now;
            this.txtPagador.Text = clsGlobales.UsuarioLogueado.Usuario;

            this.txtEfectivo.Focus();

        }

        #endregion

        #region Método que calcula el total de la OP

        private void CalcularTotal()
        {
            // Variable que va acumulando el contenido de los textbox
            double dAcumulaTotal = 0;
            // Voy recorriendo los txt y acumulo los valores si hay
            // Efectivo
            if (!(string.IsNullOrEmpty(txtEfectivo.Text)) && (clsGlobales.cValida.IsNumeric(txtEfectivo.Text)))
            {
                dAcumulaTotal = dAcumulaTotal + Convert.ToDouble(txtEfectivo.Text);
            }

            // Transferencia
            if (!(string.IsNullOrEmpty(txtTransf.Text)) && (clsGlobales.cValida.IsNumeric(txtTransf.Text)))
            {
                dAcumulaTotal = dAcumulaTotal + Convert.ToDouble(txtTransf.Text);
            }

            // Cheuqes propios
            if (!(string.IsNullOrEmpty(txtPropios.Text)) && (clsGlobales.cValida.IsNumeric(txtPropios.Text)))
            {
                dAcumulaTotal = dAcumulaTotal + Convert.ToDouble(txtPropios.Text);
            }

            // Cheques de terceros
            if (!(string.IsNullOrEmpty(txtTerceros.Text)) && (clsGlobales.cValida.IsNumeric(txtTerceros.Text)))
            {
                dAcumulaTotal = dAcumulaTotal + Convert.ToDouble(txtTerceros.Text);
            }

            // Muestro en el textbox el resultado
            txtTotalOP.Text = dAcumulaTotal.ToString("#0.00");

            // Mientras el pago sea menor o igual al importe de la factura
            if (Convert.ToDouble(txtTotalFactura.Text) >= Convert.ToDouble(txtTotalOP.Text))
            {
                btnAceptar.Enabled = true;
            }
            else
            {
                btnAceptar.Enabled = false;
            }

        }

        #endregion

        #region Método que graba el pago a cuenta en la base

        private void GrabarPago()
        {

            // Registro la orden de pago en la base de datos
            // Armo la cadena SQL
            myCadenaSQL = "insert into OrdenesPago (IdProveedor, Fecha, FechaReal, Numero, ChequesPropios, ChequesTerceros, " +
                           "Efectivo, Transferencia, Total, Activo, Usuario) values (" +
                           nuevaOrden.IdProveedor + ", '" +
                           nuevaOrden.Fecha + "', '" +
                           nuevaOrden.FechaReal + "', '" +
                           nuevaOrden.Numero + "', " +
                           nuevaOrden.ChequesPropios + ", " +
                           nuevaOrden.ChequesTerceros + ", " +
                           nuevaOrden.Efectivo + ", " +
                           nuevaOrden.Transferencia + ", " +
                           nuevaOrden.Total + ", " +
                           nuevaOrden.Activo + ", '" +
                           nuevaOrden.Usuario + "')";

            int orden = 0;

            if (iConex == 0)
            {
                // Ejecuto la consulta
                clsDataBD.GetSql(myCadenaSQL);
                // Guardo en una variable el Id de la orden de pago recién generada
                orden = clsDataBD.RetornarUltimoId("OrdenesPago", "IdOrdenPago");
            }
            else
            {
                // Ejecuto la consulta
                clsDataBD.GetSqlB(myCadenaSQL);
                // Guardo en una variable el Id de la orden de pago recién generada
                orden = clsDataBD.RetornarUltimoIdB("OrdenesPago", "IdOrdenPago");
            }

            // Registro el detalle de la OP en la base
            // Armo la cadena SQL
            myCadenaSQL = "insert into OrdenesPagoComprobantes (IdComprobanteCompra, IdOrdenPago, Importe, Activo) values (" +
                            IdComprobante + ", " +
                            orden + ", " +
                            nuevaOrden.Total + ", 1)";
            if (iConex == 0)
            {
                // Ejecuto la consulta
                clsDataBD.GetSql(myCadenaSQL);
            }
            else
            {
                // Ejecuto la consulta
                clsDataBD.GetSqlB(myCadenaSQL);
            }

            // Actualizo el saldo de la factura
            ActualizarSaldoComprobante();

            // Actualizar el saldo del proveedor
            ActualizarSaldoProveedor();

            if (iConex == 0)
            {
                // Genero la orden de pago
                GenerarOrden();
            }
                        
            // Salgo del formulario
            this.btnCancelar.PerformClick();

        }

        #endregion

        #region Método que actualiza el saldo del comprobante de compra

        private void ActualizarSaldoComprobante()
        {
            // Actualizo el saldo de la factura
            double saldoAGrabar = Convert.ToDouble(txtSaldo.Text) - nuevaOrden.Total;
            // Armo la consulta
            myCadenaSQL = "update ComprobantesCompras set Saldo = " + saldoAGrabar + " where IdComprobanteCompra = " + IdComprobante;

            if (iConex == 0)
            {
                // Ejecuto la consulta
                clsDataBD.GetSql(myCadenaSQL);
            }
            else
            {
                // Ejecuto la consulta
                clsDataBD.GetSqlB(myCadenaSQL);
            }
        }

        #endregion

        #region Método que actualiza el saldo del proveedor

        private void ActualizarSaldoProveedor()
        {
            // Variable para almacenar el saldo del proveedor
            double saldoProv = 0;
            // Armo la cadena SQL
            myCadenaSQL = "select * from Proveedores where IdProveedor = " + IdProveedor;

            DataTable myProveedor = new DataTable();
            if (iConex == 0)
            {
                // Ejecuto la consulta
                myProveedor = clsDataBD.GetSql(myCadenaSQL);
            }
            else
            {
                // Ejecuto la consulta
                myProveedor = clsDataBD.GetSqlB(myCadenaSQL);
            }
            
            // Recorro la tabla
            foreach (DataRow row in myProveedor.Rows)
            {
                saldoProv = Convert.ToDouble(row["Saldo"]);
            }
            // Al saldo del proveedor, le resto el importe pagado
            saldoProv = saldoProv - nuevaOrden.Total;
            // Armo la cadena SQL
            myCadenaSQL = "update Proveedores set Saldo = " + saldoProv + " where IdProveedor = " + IdProveedor;

            if (iConex == 0)
            {
                // Ejecuto la consulta
                clsDataBD.GetSql(myCadenaSQL);
            }
            else
            {
                // Ejecuto la consulta
                clsDataBD.GetSqlB(myCadenaSQL);
            }

        }

        #endregion

        #region Método que genera la Orden de Pago

        private void GenerarOrden()
        {
            int iNumeroOrdenPago = clsDataBD.RetornarUltimoId("OrdenesPago", "IdOrdenPago");
            string sNumeroOrdenPago = clsValida.ConvertirNumeroComprobante(iNumeroOrdenPago.ToString(), iNumeroOrdenPago.ToString().Length);
            double dTotalPagado = nuevaOrden.Total;
            string sNumALetra = "Pesos " + clsNumalet.Convertir(Convert.ToDecimal(dTotalPagado), 2, " con ", "#0", true, false, false, true, true) + "/100";
            string sFechaOrden = nuevaOrden.Fecha.ToString();
            
            // Busco en la base los datos del proveedor y los paso a la clase
            CargarProveedores(nuevaOrden.IdProveedor);

            //Data Set
            dsReportes oDsArt = new dsReportes();

            oDsArt.Tables["dtOrdenPagoProv"].Rows.Add
                (new object[] 
                        { 
                            sFechaOrden.ToString(), // Fecha de la orden
                            nuevaOrden.IdProveedor.ToString(), // Id del proveedor
                            myProveedor.RazonSocial.ToString(), // Razon social del proveedor
                            myProveedor.CUIT.ToString(), // Cuit del proveedor
                            myComprobanteViejo.Fecha.ToString(), // Fecha del comprobante
                            myComprobanteViejo.Numero.ToString(), // Numero del comprobante
                            myComprobanteViejo.Descripcion.ToString(), // Compra o gasto
                            myComprobanteViejo.Total.ToString("#0.00#"), // Total original del comrpbante
                            sNumALetra.ToString(), // Texto del importe pagado
                            myComprobanteViejo.Saldo.ToString("#0.00#"), // Saldo del comprobante
                            myComprobanteViejo.Usuario.ToString(), // Comprador
                            dTotalPagado.ToString(), // Total de la orden de pago

                        }
                );
                     

            //Objeto Reporte
            rptOrdenPagoProveedores oRepArt = new rptOrdenPagoProveedores();
            //Cargar Reporte            
            //    oRepTipoCli.Load(Application.StartupPath + "\\rptTipoCli.rpt");
            oRepArt.Load(Application.StartupPath + "\\rptOrdenPagoProveedores.rpt");
            //Establecer el DataSet como DataSource
            oRepArt.SetDataSource(oDsArt);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepArt;

            oRepArt.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepArt.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
            oRepArt.DataDefinition.FormulaFields["linea-02"].Text = "'Dirección: " + clsGlobales.cParametro.Direccion + "'";
            oRepArt.DataDefinition.FormulaFields["linea-03"].Text = "'Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
            oRepArt.DataDefinition.FormulaFields["linea-04"].Text = "'Teléfono : " + clsGlobales.cParametro.Telefono + "'";
            oRepArt.DataDefinition.FormulaFields["linea-05"].Text = "'CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepArt.DataDefinition.FormulaFields["linea-06"].Text = "'Mail: " + clsGlobales.cParametro.Mail + "'";
            oRepArt.DataDefinition.FormulaFields["linea-07"].Text = "'Web : " + clsGlobales.cParametro.Web + "'";

            oRepArt.DataDefinition.FormulaFields["Efectivo"].Text = "'" + nuevaOrden.Efectivo.ToString("#0.00") + "'";
            oRepArt.DataDefinition.FormulaFields["Transferencia"].Text = "'" + nuevaOrden.Transferencia.ToString("#0.00") + "'";
            oRepArt.DataDefinition.FormulaFields["Terceros"].Text = "'" + nuevaOrden.ChequesTerceros.ToString("#0.00") + "'";
            oRepArt.DataDefinition.FormulaFields["Propios"].Text = "'" + nuevaOrden.ChequesPropios.ToString("#0.00") + "'";

            oRepArt.DataDefinition.FormulaFields["NumeroOrden"].Text = "'" + sNumeroOrdenPago.ToString() + "'";

            
            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();

        }

        #endregion

        #region Método que carga los datos de los proveedores a la clase

        private void CargarProveedores(int Id)
        {
            // Armo la cadena SQL
            myCadenaSQL = "select * from Vista_Proveedores where IdProveedor = " + Id;
            // Creo una tabla que me va a almacenar el resultado de la consulta
            DataTable myTablaProveedores = clsDataBD.GetSql(myCadenaSQL);
            // Recorro la tabla y le asigo los datos del proveedor al objeto
            foreach (DataRow rowProv in myTablaProveedores.Rows)
            {
                // Primero los Int
                myProveedor.IdProveedor = Convert.ToInt32(rowProv["IdProveedor"]);
                myProveedor.IdCondicionIva = Convert.ToInt32(rowProv["IdCondicionIva"]);
                myProveedor.IdCondicionCompra = Convert.ToInt32(rowProv["IdCondicionCompra"]);
                myProveedor.IdProvincia = Convert.ToInt32(rowProv["IdProvincia"]);
                myProveedor.IdLocalidad = Convert.ToInt32(rowProv["IdLocalidad"]);
                myProveedor.ProvIns = Convert.ToInt32(rowProv["Insumos"]);
                myProveedor.ProvProd = Convert.ToInt32(rowProv["Productos"]);

                // Los string
                myProveedor.NombreFantasia = rowProv["NombreFantasia"].ToString();
                myProveedor.RazonSocial = rowProv["RazonSocial"].ToString();
                myProveedor.CondicionIva = rowProv["CondicionIva"].ToString();
                myProveedor.CUIT = rowProv["CUIT"].ToString();
                myProveedor.IngresosBrutos = rowProv["IngresosBrutos"].ToString();
                myProveedor.FechaInicioActividad = rowProv["FechaInicioActividad"].ToString();
                myProveedor.Direccion = rowProv["Direccion"].ToString();
                myProveedor.Localidad = rowProv["Localidad"].ToString();
                myProveedor.Provincia = rowProv["Provincia"].ToString();
                myProveedor.Cp = rowProv["CP"].ToString();
                myProveedor.Telefono = rowProv["Telefono"].ToString();
                myProveedor.Fax = rowProv["Fax"].ToString();
                myProveedor.Celular = rowProv["Celular"].ToString();
                myProveedor.MailEmpresa = rowProv["MailEmpresa"].ToString();
                myProveedor.Web = rowProv["Web"].ToString();
                myProveedor.Contacto = rowProv["Contacto"].ToString();
                myProveedor.MailContacto = rowProv["MailContacto"].ToString();
                myProveedor.CelularContacto = rowProv["CelularContacto"].ToString();
                myProveedor.Observaciones = rowProv["Observaciones"].ToString();
            }
        }

        #endregion

        #region Método que habilita el botón aceptar si hay algún importe cargado

        private void HabilitarAceptar()
        {
            // Paso los datos de los txt a variables para que sea más prolijo el IF
            double dEfectivo = Convert.ToDouble(txtEfectivo.Text);
            double dTransf = Convert.ToDouble(txtTransf.Text);
            double dPropios = Convert.ToDouble(txtPropios.Text);
            double dTerceros = Convert.ToDouble(txtTerceros.Text);

            if (dEfectivo > 0 || dTransf > 0 || dPropios > 0 || dTerceros > 0)
            {
                this.btnAceptar.Enabled = true;
            }
            else
            {
                this.btnAceptar.Enabled = false;
            }
        }

        #endregion

        #endregion
    }
}

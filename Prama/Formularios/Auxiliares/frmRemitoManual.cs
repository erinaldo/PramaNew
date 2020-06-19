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
    public partial class frmRemitoManual : Form
    {
        //Objetos de Clase
        clsCLientes myCliente = new clsCLientes();
        int IdCli = 0;

        public frmRemitoManual()
        {
            InitializeComponent();
        }
        
        #region Método que trae el Cliente para una nueva factura

        private void CargarClienteNuevo(int p_Cliente = 0)
        {
            //Vino un numero de cliente mayor a 0 en el parametro?....
            if (p_Cliente == 0)
            {
                // Si el vector tiene más de un Cliente seleccionado
                if (clsGlobales.ClientesSeleccionados.GetLength(0) > 1)
                {
                    // Informo que solo se puede seleccionar un proveedor
                    MessageBox.Show("Solo puede seleccionar un Cliente!", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Vuelvo a abrir el formulario de búsqueda de proveedores
                    // LLamo al formulario que busca los Clientes
                    frmBuscarCliente myForm = new frmBuscarCliente();
                    myForm.ShowDialog();
                }
            }
            else
            {
                // Redimensiono el tamaño de la matriz
                clsGlobales.ClientesSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ClientesSeleccionados, clsGlobales.ClientesSeleccionados.Length + 1);
                // A la posición creada le asigno el Id seleccionado
                clsGlobales.ClientesSeleccionados[clsGlobales.ClientesSeleccionados.Length - 1] = p_Cliente;
            }

            //**
            // Si hay algún Cliente seleccionado
            if (clsGlobales.ClientesSeleccionados.GetLength(0) > 0)
            {
                // Recorro el vector hasta que encuentro un Id de Cliente y lo paso a los controles del formulario
                for (int i = 0; i <= clsGlobales.ClientesSeleccionados.GetLength(0); i++)
                {
                    // Si la posición tiene un ID de Cliente, busco los datos del mismo
                    if (clsGlobales.ClientesSeleccionados[0] > 0)
                    {
                        // Cargo los datos del Cliente
                        CargarClientes(clsGlobales.ClientesSeleccionados[0]);
                        // Los paso al formulario
                        PasarDatosAlFormulario();
                    }
                }

            }
        }

        #endregion

        #region PasarDatosAlFormulario

        //PASA LOS DATOS DEL CLIENTE AL FORMULARIO
        private void PasarDatosAlFormulario()
        {

            //Paso los datos del proveedor al formulario  
            this.IdCli = this.myCliente.Codigo;
            this.txtCuit.Text = this.myCliente.Cuit;
            this.txtRazonSocial.Text = this.myCliente.RazonSocial;
            this.txtDir.Text = this.myCliente.Direccion + ", " + this.myCliente.Localidad + " (" + this.myCliente.CP + ") - " + this.myCliente.Provincia;
            this.cboTransporte.SelectedValue = this.myCliente.IdTransporte;
            this.cboMercaderia.SelectedValue = this.myCliente.IdCondicionCompra;
            this.cboPagoFlete.SelectedValue = this.myCliente.IdCondicionCompra;
            //Flete FormaPago
            cboPagoFlete.SelectedValue = 1;
        }

        #endregion

        #region Metodo EliminarClienteSeleccionado

        //Eliminar el cliente selecionado anteriormente
        private void EliminarClienteSeleccionado()
        {
            // Recorro el vector
            for (int i = 0; i < clsGlobales.ClientesSeleccionados.GetLength(0); i++)
            {
                // Si el Cliente que quiero borrar está en el vector
                if (clsGlobales.ClientesSeleccionados[i] == myCliente.Codigo)
                {
                    // Elimino el proveedor del vector
                    clsGlobales.ClientesSeleccionados[i] = 0;
                    //Limpiar vector Cliente
                    clsGlobales.ClientesSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ClientesSeleccionados, 0);
                    // Salgo del for
                    break;
                }
            }
        }

        #endregion

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

                myCliente.IdTransporte = Convert.ToInt32(rowCli["IdTransporte"].ToString());
                myCliente.IdCondicionCompra = Convert.ToInt32(rowCli["IdCondicionCompra"].ToString());

                if (!(string.IsNullOrEmpty(rowCli["Barrio"].ToString())))
                {
                    myCliente.Barrio = rowCli["Barrio"].ToString();
                }
                else { myCliente.Barrio = ""; }

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

        #region Botones

        private void btnEditCli_Click(object sender, EventArgs e)
        {
            //hay cliente seleccionado
            if (string.IsNullOrEmpty(txtCuit.Text))
            {
                MessageBox.Show("No hay ningún Cliente seleccionado!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Lamar al formulario de clientes con el cliente seleccionado
            frmClientesABM frmModiCli = new frmClientesABM(myCliente.Codigo);
            frmModiCli.ShowDialog();
            // Cargo los datos del proveedor
            CargarClientes(this.myCliente.Codigo);
            // Los paso al formulario
            PasarDatosAlFormulario();
            //Inhabilitar Boton
            //this.btnCli.Enabled = false;
        }

        private void btnTransporte_Click(object sender, EventArgs e)
        {
            //Llamar al Formulario de Transporte
            frmTransporte myForm = new frmTransporte(txtIdTransporte);
            myForm.ShowDialog();

            //Asginar
            if (!(string.IsNullOrEmpty(txtIdTransporte.Text)))
            {
                cboTransporte.SelectedValue = txtIdTransporte.Text;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            EliminarClienteSeleccionado();
            this.Close();
        }

        private void btnCli_Click(object sender, EventArgs e)
        {
            //Quitar el cliente actualmente selecionado
            EliminarClienteSeleccionado();
            //Buscar Cliente
            frmBuscarCliente myForm = new frmBuscarCliente();
            myForm.ShowDialog();
            //Cliente Nuevo
            this.CargarClienteNuevo();
            //Retorna
            if (clsGlobales.ClientesSeleccionados.GetLength(0) > 0)
            {
                //Inhabilitar Boton
                this.btnEditCli.Enabled = true;
                //this.btnCli.Enabled = false;
            }

            //Foco
            txtCantBultos.Focus();
        }

        #endregion

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //Si falta completar... salir
            if (!(ValidarRemito()))
            { return ;}

            //Data Set
            dsReportes oDsRemito = new dsReportes();

            oDsRemito.Tables["dtRemito"].Rows.Add
            (new object[] { myCliente.RazonSocial,
                    myCliente.Direccion,
                    myCliente.Localidad + " (" + myCliente.CP + ")",
                    cboTransporte.Text,
                    cboMercaderia.Text,
                    cboPagoFlete.Text,
                    txtCantBultos.Text,
                    Convert.ToDouble(txtValSeg.Text).ToString("#0.00"),
                    DateTime.Now.ToString("dd/MM/yyyy"),
                    myCliente.Cuit,
                    myCliente.Telefono,
                    ""});

            //Objeto Reporte
            rptRemito oRepRemito = new rptRemito();

            //Cargar Reporte                                    
            oRepRemito.Load(Application.StartupPath + "\\rptRemito.rpt");

            //Establecer el DataSet como DataSource
            oRepRemito.SetDataSource(oDsRemito);

            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepRemito;

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports(2);
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog(); 

        }

        #region Metodo ValidarRemito

        //VALIDAR REMITO
        private bool ValidarRemito()
        {
            bool bValida = true;

            //Validar Datos Cliente
            if (string.IsNullOrEmpty(txtCuit.Text))
            {
                MessageBox.Show("Debe seleccionar un Cliente!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bValida = false;
                btnCli.Focus();
                return bValida;
            }

            //Razon Social Cliente
            if (string.IsNullOrEmpty(txtRazonSocial.Text))
            {
                MessageBox.Show("Debe seleccionar un Cliente!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bValida = false;
                btnCli.Focus();
                return bValida;
            }

            //Direccion Cliente
            if (string.IsNullOrEmpty(txtDir.Text))
            {
                MessageBox.Show("Debe seleccionar un Cliente!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bValida = false;
                btnCli.Focus();
                return bValida;
            }

            //Transporte
            if (cboTransporte.Text == "")
            {
                MessageBox.Show("Debe seleccionar el Transporte!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bValida = false;
                cboTransporte.Focus();
                return bValida;
            }

            //Pago Mercaderia
            if (cboMercaderia.Text == "")
            {
                MessageBox.Show("Debe seleccionar la Forma de Pago de la Mercadería!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bValida = false;
                cboMercaderia.Focus();
                return bValida;
            }

            //Pago Flete
            if (cboPagoFlete.Text == "")
            {
                MessageBox.Show("Debe seleccionar la Forma de Pago del Flete!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bValida = false;
                cboPagoFlete.Focus();
                return bValida;
            }

            //Cantidad de Bultos
            if (txtCantBultos.Text == "")
            {
                MessageBox.Show("Debe completar la Cantidad de Bultos!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bValida = false;
                txtCantBultos.Focus();
                return bValida;
            }

            //Valor Seguro <> ""
            if (txtValSeg.Text == "")
            {
                MessageBox.Show("Debe completar el Valor del Seguro!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bValida = false;
                txtValSeg.Focus();
                return bValida;
            }

            //Valor Seguro == 0 ?
            if (Convert.ToDouble(txtValSeg.Text) == 0)
            {
                MessageBox.Show("El Valor del Seguro debe ser mayor que 0!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bValida = false;
                txtValSeg.Focus();
                return bValida;
            }

            //Retornar Valor
            return bValida;
        }

        #endregion

        private void frmRemitoManual_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            //Condicion Compra
            clsDataBD.CargarCombo(this.cboPagoFlete, "CondicionesCompra", "CondicionCompra", "IdCondicionCompra");
            cboPagoFlete.SelectedIndex = -1;

            //Condicion Compra Mercaderia
            clsDataBD.CargarCombo(this.cboMercaderia, "CondicionesCompra", "CondicionCompra", "IdCondicionCompra");
            cboMercaderia.SelectedValue = -1;

            //Transporte
            clsDataBD.CargarCombo(this.cboTransporte, "Transportes", "RazonSocial", "IdTransporte");
            cboTransporte.SelectedIndex = -1;

            //Tooltips
            this.CargarToolsTip();

            //Titulo
            this.Text = clsGlobales.cParametro.NombreFantasia + this.Text;
        }


        #region Método que carga los ToolsTip del formulario

        private void CargarToolsTip()
        {
            toolTip1.SetToolTip(this.btnSalir, "Salir");
            toolTip2.SetToolTip(this.btnImprimir, "Imprimir Remito");

        }

        #endregion

        private void txtValSeg_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCantBultos_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Solo enteros
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }         
        }
    }
}

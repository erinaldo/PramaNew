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
    public partial class frmRotuloSnRemito : Form
    {
        #region Variables publicas

        //VARIABLES
        int IdCli = 0;
        //Objetos de Clase
        clsCLientes myCliente = new clsCLientes();
        
        #endregion

        #region Constructor

        public frmRotuloSnRemito()
        {
            InitializeComponent();
        }

        #endregion

        #region Boton Cliente Click

        private void btnCli_Click(object sender, EventArgs e)
        {
            //Quitar el cliente actualmente selecionado
            EliminarClientesSeleccionados();
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
            //fOCO
            txtCantBultos.Focus();

        }

        #endregion
       
        #region Metodo EliminarclsGlobales.ClientesSeleccionado

        //Eliminar el cliente selecionado anteriormente
        private void EliminarClientesSeleccionados()
        {
            //Limpiar vector Cliente
            clsGlobales.ClientesSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ClientesSeleccionados, 0);
           
            // Salgo del for

            // Recorro el vector
            /*for (int i = 0; i < clsGlobales.ClientesSeleccionados.GetLength(0); i++)
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
            }*/
        }

        #endregion

        #region Método que trae el Cliente para una nueva factura

        private void CargarClienteNuevo()
        {
            // Si el vector tiene ,ás de un proveedor seleccionado
            if (clsGlobales.ClientesSeleccionados.GetLength(0) > 1)
            {
                // Informo que solo se puede seleccionar un proveedor
                MessageBox.Show("Solo puede seleccionar un Cliente!", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Vuelvo a abrir el formulario de búsqueda de proveedores
                // LLamo al formulario que busca los Clientes
                frmBuscarCliente myForm = new frmBuscarCliente();
                myForm.ShowDialog();
            }
            // Si hay algún proveedor seleccionado
            if (clsGlobales.ClientesSeleccionados.GetLength(0) > 0)
            {
                // Recorro el vector hasta que encuentro un Id de proveedor y lo paso a los controles del formulario
                for (int i = 0; i <= clsGlobales.ClientesSeleccionados.GetLength(0); i++)
                {
                    // Si la posición tiene un ID de proveedor, busco los datos del mismo
                    if (clsGlobales.ClientesSeleccionados[0] > 0)
                    {
                        // Cargo los datos del proveedor
                        CargarClientes(clsGlobales.ClientesSeleccionados[0]);
                        // Los paso al formulario
                        PasarDatosAlFormulario();
                    }
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

        #region Boton Salir Click

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //Eliminar
            this.EliminarClientesSeleccionados();
            //Cerrar
            this.Close();
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
            this.cboTransporte.SelectedValue = myCliente.IdTransporte;
        }

        #endregion

        private void frmRotuloSnRemito_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            //Transporte
            clsDataBD.CargarCombo(this.cboTransporte, "Transportes", "RazonSocial", "IdTransporte");
            cboTransporte.SelectedIndex = -1;

            // Cargar ToolTips
            CargarToolTips();
            //Titulo
            this.Text = clsGlobales.cParametro.NombreFantasia + this.Text;
        }

        #region CargarToolTips

        private void CargarToolTips()
        {
            toolTip1.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip2.SetToolTip(this.btnSalir, "Salir");
            toolTip3.SetToolTip(this.btnTransporte, "Transporte");
        }

        #endregion

        private void txtCantBultos_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Solo enteros
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }        
        }

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

        #region Botones

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Validar CUIT
            if (string.IsNullOrEmpty(txtCuit.Text))
            {
                MessageBox.Show("Debe completar 'CUIT' del Cliente!", "Falta completar!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            //Validar Transporte
            if (cboTransporte.SelectedIndex == -1)
            {
                MessageBox.Show("Debe elegir el 'Transporte'!", "Falta completar!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            //Validar Cantidad de Bultos
            if (string.IsNullOrEmpty(txtCantBultos.Text))
            {
                MessageBox.Show("Debe completar 'Cantidad de Bultos'!", "Falta completar!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            else
            {
                if (Convert.ToInt32(txtCantBultos.Text) == 0)
                {
                    MessageBox.Show("La 'Cantidad de Bultos' debe ser mayor a 0!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //Validar Valor Seguro
            if (string.IsNullOrEmpty(txtValSeg.Text))
            {
                MessageBox.Show("Debe completar 'Valor de Seguro'!", "Falta completar!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            else
            {
                if (Convert.ToDouble(txtValSeg.Text) > 999999)
                {
                    MessageBox.Show("El 'Valor de Seguro' ingresado es incorrecto!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            dsReportes oDsRemito = new dsReportes();

            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int Hasta = Convert.ToInt32(txtCantBultos.Text);

            for (int i = 1; i <= Hasta; i++)
            {
                oDsRemito.Tables["dtRotulo"].Rows.Add
                (new object[] { i,
                                    Hasta,
                                    txtRazonSocial.Text,
                                    myCliente.Direccion,
                                    myCliente.Localidad + " (" + myCliente.CP + ")" + " - " + myCliente.Provincia,
                                    myCliente.Telefono,
                                    cboTransporte.Text,
                                    ""});
            }

            //Objeto Reporte
            rptRotulo2 oRepRotulo = new rptRotulo2();

            //Cargar Reporte                                    
            oRepRotulo.Load(Application.StartupPath + "\\rptRotulo2.rpt");

            //Establecer el DataSet como DataSource
            oRepRotulo.SetDataSource(oDsRemito);

            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepRotulo;

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();
        }

        private void btnTransporte_Click(object sender, EventArgs e)
        {
            //Llamar al Formulario de Transporte
            frmTransporte myForm = new frmTransporte(txtIdTransporte);
            myForm.ShowDialog();

            //Asginar
            if (!(string.IsNullOrEmpty(txtIdTransporte.Text)))
            {
                cboTransporte.SelectedValue = Convert.ToInt32(txtIdTransporte.Text);
            }
        }

        #endregion

        private void txtCantBultos_Enter(object sender, EventArgs e)
        {
            txtCantBultos.SelectionStart = 0;
            txtCantBultos.SelectionLength = txtCantBultos.Text.Length;
        }

        private void txtCantBultos_Click(object sender, EventArgs e)
        {
            txtCantBultos.SelectionStart = 0;
            txtCantBultos.SelectionLength = txtCantBultos.Text.Length;
        }

        private void txtValSeg_Enter(object sender, EventArgs e)
        {
            txtValSeg.SelectionStart = 0;
            txtValSeg.SelectionLength = txtValSeg.Text.Length;
        }

        private void txtValSeg_Click(object sender, EventArgs e)
        {
            txtValSeg.SelectionStart = 0;
            txtValSeg.SelectionLength = txtValSeg.Text.Length;
        }

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

    }
}

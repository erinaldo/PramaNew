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
    public partial class frmComprasGastosFijos : Form
    {

        #region Variables del formulario

        // Variable que almacena el Id de la cotización para cuando se hace el llamado desde las cotizaciones
        int IdFactura = 0;
        // Variable que almacena el proveedor
        int IdProveedor = 0;
        // Bandera que controla el comportamiento de los combos
        bool bBandera = false;
        // Bandera que valida que los datos del proveedor se cargaron (proveedor seleccionado)
        bool bProveedor = false;
        // Variable que almacena la cadena SQL
        string myCadenaSQL = "";
        // Instancio un objeto de la calse proveedores para pasarle los datos que me devuleve la consulta
        clsProveedores myProveedor = new clsProveedores();
        // Instancio un objeto de la clase clsGgastosComunes
        Prama.Clases.clsGastosComunes myComprobanteNuevo = new Prama.Clases.clsGastosComunes();
        Prama.Clases.clsGastosComunes myComprobanteViejo = new Prama.Clases.clsGastosComunes();

        #endregion

        #region Constructor

        public frmComprasGastosFijos(int[] Comprobantes, int IdFact, int IdProv)
        {
            // Paso a la variable el Id de la cotización que viene por parámetro
            clsGlobales.ComprobantesSeleccionados = Comprobantes;
            // Paso a la variable el Id de la factura a modificar
            IdFactura = IdFact;
            // Paso a la variable el Id del proveedor
            IdProveedor = IdProv;
            // Inicializo los controles del formulario
            InitializeComponent();
            
        }

        #endregion

        #region Eventos del Formulario

        #region Evento Load del Formulario

        private void frmComprasGastos_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            // Cargo los combos con sus datos
            CargarCombos();
            cboTC.SelectedIndex = 0;
            // Pongo en su correspondiente textbox al comprador (Usuario logueado)
            txtComprador.Text = clsGlobales.UsuarioLogueado.Usuario;
            //Punto de compra / venta y Almacen
            this.cboPunto.SelectedValue = Convert.ToInt32(clsGlobales.cParametro.PtoVtaPorDefecto);
            this.cboAlmacen.SelectedValue = Convert.ToInt32(clsGlobales.cParametro.AlmacenPorDefecto);
            //Titulo Ventana
            this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;

            // Si estoy modificando una factura
            if (!(IdFactura == 0))
            {
                // Cargo los datos de la factura a editar
                CargarFacturaEditar();
                // deshabilito todo
                InhabilitarTodo();

            }
        }

        #endregion
        
        #region Eventos de los combos

        #region Evento Click del combo cboPunto

        private void cboPunto_Click(object sender, EventArgs e)
        {
            // Cambio el estado de la bandera de los combos
            bBandera = true;

        }

        #endregion

        #region Evento SelectIndexChanged del cboPunto

        private void cboPunto_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Si ya hice click sobre el combo punto de compra
            if (bBandera)
            {

            }
        }

        #endregion

        #region Evento Click del cboImputacion

        private void cboImputacion_Click(object sender, EventArgs e)
        {
            // Cambio el estado de la bandera de los combos
            bBandera = true;
        }

        #endregion

        #region Evento SelectIndexChanged del cbo Imputación

        private void cboImputacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bBandera)
            {
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

        #endregion

        #endregion

        #region Eventos de los botones

        #region Evento Click del botón Buscar Proveedor

        private void btnSearchProvider_Click(object sender, EventArgs e)
        {
            // Vacío el vector de los proveedores
            VaciarVectoresGlobales();
            // LLamo al formulario que busca los proveedores
            frmProveedoresBuscar myForm = new frmProveedoresBuscar(true, true);
            // Lo muestro
            myForm.ShowDialog();
            // Cargo los datos del proveedor en el formulario
            CargarProveedorNuevo();
        }

        #endregion

        #region Evento Click del botón Aceptar

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Paso los datos del formulario a la clase para validarlos
            PasarDatosALaClase();

            //VECTOR DE ERRORES
            string[] cErrores = myComprobanteNuevo.cValidaComprobantes();
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
            // Armo la cadena SQL
            myCadenaSQL = "insert into ComprobantesCompras (IdTipoComprobanteCompra, IdProveedor, IdPuntoVenta, IdAlmacen," +
                            " IdCondicionCompra, Fecha, FechaReal, Numero, CantidadArticulos, Descripcion, Neto, Iva25, " + 
                            " Iva50, Iva105, Iva210, Iva270, Total, Saldo, Activo, PercepcionesVarias, PercepcionesIva, " + 
                            " PercepcionesIB, NoGravados, Flete, Usuario, IdImputacion, Compra) values (" 
                                                            + myComprobanteNuevo.IdTipoComprobanteCompra + ", "
                                                            + myComprobanteNuevo.IdProveedor + ", "
                                                            + myComprobanteNuevo.IdPuntoVenta + ", "
                                                            + myComprobanteNuevo.IdAlmacen + ", "
                                                            + myComprobanteNuevo.IdCondicionCompra + ", '"
                                                            + myComprobanteNuevo.Fecha + "', '"
                                                            + myComprobanteNuevo.FechaReal + "', '"
                                                            + myComprobanteNuevo.Numero + "', "
                                                            + myComprobanteNuevo.CantidadArticulos + ", '"
                                                            + myComprobanteNuevo.Descripcion + "', "
                                                            + myComprobanteNuevo.Neto + ", "
                                                            + myComprobanteNuevo.Iva25 + ", "
                                                            + myComprobanteNuevo.Iva50 + ", "
                                                            + myComprobanteNuevo.Iva105 + ", "
                                                            + myComprobanteNuevo.Iva210 + ", "
                                                            + myComprobanteNuevo.Iva270 + ", "
                                                            + myComprobanteNuevo.Total + ", "
                                                            + myComprobanteNuevo.Saldo + ", "
                                                            + myComprobanteNuevo.Activo + ", "
                                                            + myComprobanteNuevo.PercepcionesVarias + ", "
                                                            + myComprobanteNuevo.PercepcionesIva + ", "
                                                            + myComprobanteNuevo.PercepcionesIB + ", "
                                                            + myComprobanteNuevo.Exento + ", "
                                                            + myComprobanteNuevo.Flete + ", '"
                                                            + myComprobanteNuevo.Usuario + "', "
                                                            + myComprobanteNuevo.IdImputacion + ", "
                                                            + myComprobanteNuevo.Compra + ")";

            if (clsGlobales.ConB == null)
            {
                // Ejecuto la consulta para almacenarlo en la base
                clsDataBD.GetSql(myCadenaSQL);
                // Actualizo el saldo del proveedor
                ActualizarSaldoProveedor(myComprobanteNuevo.IdProveedor, myComprobanteNuevo.Total);
                //Mensaje
                MessageBox.Show("La carga de la Factura de Gasto se ha realizado con exito!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Cierro el formulario
                btnCancelar.PerformClick();
            }
            else
            {
                // Ejecuto la consulta para almacenarlo en la base
                clsDataBD.GetSqlB(myCadenaSQL);
                // Actualizo el saldo del proveedor
                ActualizarSaldoProveedorB(myComprobanteNuevo.IdProveedor, myComprobanteNuevo.Total);
                //Mensaje
                MessageBox.Show("La carga de la Factura de Gasto se ha realizado con exito!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Cierro el formulario
                btnCancelar.PerformClick();
            }
                
        }

        #endregion

        #region Evento Click del botón Cancelar

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Cierro el formulario
            this.Close();
        }

        #endregion
               
        #endregion

        #region Eventos de los textBox

        #region Eventos Leave y KeyPress del txtNeto

        private void txtNeto_Leave(object sender, EventArgs e)
        {
            // Doy formato al número
            this.txtNeto.Text = Convert.ToDouble(this.txtNeto.Text).ToString("#0.00");
            // Calculo el nuevo total
            CalcularTotal();
        }
        private void txtNeto_KeyPress(object sender, KeyPressEventArgs e)
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
                this.txtIva25.Focus();
            }
        }

        #endregion

        #region Eventos Leave y KeyPress del txtIva25

        private void txtIva25_Leave(object sender, EventArgs e)
        {
            // Doy formato al número
            this.txtIva25.Text = Convert.ToDouble(this.txtIva25.Text).ToString("#0.00");
            // Calculo el nuevo total
            CalcularTotal();
        }

        private void txtIva25_KeyPress(object sender, KeyPressEventArgs e)
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
                this.txtIva50.Focus();
            }
        }

        #endregion

        #region Eventos Leave y KeyPress del txtIva50

        private void txtIva50_Leave(object sender, EventArgs e)
        {
            // Doy formato al número
            this.txtIva50.Text = Convert.ToDouble(this.txtIva50.Text).ToString("#0.00");
            // Calculo el nuevo total
            CalcularTotal();
        }

        private void txtIva50_KeyPress(object sender, KeyPressEventArgs e)
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
                this.txtIva105.Focus();
            }
        }

        #endregion

        #region Eventos Leave y KeyPress del txtIva105

        private void txtIva105_Leave(object sender, EventArgs e)
        {
            // Doy formato al número
            this.txtIva105.Text = Convert.ToDouble(this.txtIva105.Text).ToString("#0.00");
            // Calculo el nuevo total
            CalcularTotal();
        }

        private void txtIva105_KeyPress(object sender, KeyPressEventArgs e)
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
                this.txtIva210.Focus();
            }
        }

        #endregion

        #region Eventos Leave y KeyPress del txtIva210

        private void txtIva210_Leave(object sender, EventArgs e)
        {
            // Doy formato al número
            this.txtIva210.Text = Convert.ToDouble(this.txtIva210.Text).ToString("#0.00");
            // Calculo el nuevo total
            CalcularTotal();
        }

        private void txtIva210_KeyPress(object sender, KeyPressEventArgs e)
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
                this.txtIva270.Focus();
            }
        }

        #endregion

        #region Eventos Leave y KeyPress del txtIva270

        private void txtIva270_Leave(object sender, EventArgs e)
        {
            // Doy formato al número
            this.txtIva270.Text = Convert.ToDouble(this.txtIva270.Text).ToString("#0.00");
            // Calculo el nuevo total
            CalcularTotal();
        }

        private void txtIva270_KeyPress(object sender, KeyPressEventArgs e)
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
                this.txtPercVarias.Focus();
            }
        }

        #endregion

        #region Eventos Leave y KeyPress del txtPercVarias

        private void txtPercVarias_Leave(object sender, EventArgs e)
        {
            // Doy formato al número
            this.txtPercVarias.Text = Convert.ToDouble(this.txtPercVarias.Text).ToString("#0.00");
            // Calculo el nuevo total
            CalcularTotal();
        }

        private void txtPercVarias_KeyPress(object sender, KeyPressEventArgs e)
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
                this.txtPercIva.Focus();
            }
        }

        #endregion

        #region Eventos Leave y KeyPress del txtPercIva

        private void txtPercIva_Leave(object sender, EventArgs e)
        {
            // Doy formato al número
            this.txtPercIva.Text = Convert.ToDouble(this.txtPercIva.Text).ToString("#0.00");
            // Calculo el nuevo total
            CalcularTotal();
        }

        private void txtPercIva_KeyPress(object sender, KeyPressEventArgs e)
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
                this.txtPercIB.Focus();
            }
        }

        #endregion

        #region Eventos Leave y KeyPress del txtPercIB

        private void txtPercIB_Leave(object sender, EventArgs e)
        {
            // Doy formato al número
            this.txtPercIB.Text = Convert.ToDouble(this.txtPercIB.Text).ToString("#0.00");
            // Calculo el nuevo total
            CalcularTotal();
        }

        private void txtPercIB_KeyPress(object sender, KeyPressEventArgs e)
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
                this.txtNoGravados.Focus();
            }
        }

        #endregion

        #region Eventos Leave y KeyPress del txtNoGravados

        private void txtNoGravados_Leave(object sender, EventArgs e)
        {
            // Doy formato al número
            this.txtNoGravados.Text = Convert.ToDouble(this.txtNoGravados.Text).ToString("#0.00");
            // Calculo el nuevo total
            CalcularTotal();
        }

        private void txtNoGravados_KeyPress(object sender, KeyPressEventArgs e)
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

        #endregion

        #region Eventos Leave y KeyPress del txtTotalFact

        private void txtTotalFact_Leave(object sender, EventArgs e)
        {
            // Doy formato al número
            this.txtTotalFact.Text = Convert.ToDouble(this.txtTotalFact.Text).ToString("#0.00");

            HabilitaCampos();
        }

        private void txtTotalFact_KeyPress(object sender, KeyPressEventArgs e)
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
                HabilitaCampos();
            }
        }

        #endregion

        #region Metodo HabilitaCampos

        private void HabilitaCampos()
        {
            // Si se cargó el proveedor y se caró el total de la factura, habilito el cierre
            if (bProveedor && (!(string.IsNullOrEmpty(txtNeto.Text))) && Convert.ToDouble(txtTotalFact.Text) > 0)
            {
                // Habilito el grupo de cierre
                gpbCierre.Enabled = true;
                // Pongo el foco en el neto
                this.txtNeto.Focus();
            }
            else
            {
                if (!(bProveedor))
                {
                    // Muestro el mensaje y pongo el foco en el botón del proveedor
                    MessageBox.Show("Debe seleccionar un proveedor", "COMPLETAR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // Pongo el foco en el botón Buscar proveedor
                    btnSearchProvider.Focus();
                    return;
                }
                else
                {
                    // Muestro el mensaje y pongo el foco en el botón del proveedor
                    MessageBox.Show("Debe Ingresar un importe total de la Factura", "COMPLETAR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // Pongo el foco en el neto
                    this.txtNeto.Focus();
                    return;
                }
            }
        }

        #endregion

        #region Evento Leave del txtDetalle

        private void txtDetalle_Leave(object sender, EventArgs e)
        {
            txtDetalle.Text = txtDetalle.Text.ToUpper();
        }

        #endregion

        #endregion

        #endregion

        #region Métodos del formulario

        #region Método que carga los combos del formulario

        private void CargarCombos()
        {
            // Cargo el combo de los puntos de venta
            clsDataBD.CargarCombo(cboPunto, "PuntosVentas", "PuntoVenta", "IdPuntoVenta", "Activo = 1");
            // Cargo el combo de los almacenes
            clsDataBD.CargarCombo(cboAlmacen, "Almacenes", "Almacen", "IdAlmacen", "Activo = 1");
            // Cargo el combo de las condiciones de compra
            clsDataBD.CargarCombo(cboCondCompra, "CondicionesCompra", "CondicionCompra", "IdCondicionCompra", "Activo = 1");
            // Dejo vacía la selección
            cboCondCompra.SelectedIndex = -1;
            // Cargo el combo de imputaciones
            clsDataBD.CargarCombo(cboImputacion, "Imputaciones", "Imputacion", "IdImputacion", "CodigoInterno > 51000 and CodigoInterno != 52000 and Activo=1");
            // Dejo vacía la selección
            cboImputacion.SelectedIndex = -1;

            //Punto de compra / venta y Almacen
            this.cboPunto.SelectedValue = Convert.ToInt32(clsGlobales.cParametro.PtoVtaPorDefecto);
            this.cboAlmacen.SelectedValue = Convert.ToInt32(clsGlobales.cParametro.AlmacenPorDefecto);

        }

        #endregion

        #region Método que vacía los vectores globales para nuevo uso

        private void VaciarVectoresGlobales()
        {
            // Vacío de datos el vector de los proveedores
            clsGlobales.ProveedoresSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ProveedoresSeleccionados, 0);
        }

        #endregion

        #region Método que trae el proveedor para un nuevo Gasto

        private void CargarProveedorNuevo()
        {
            // Si el vector tiene ,ás de un proveedor seleccionado
            if (clsGlobales.ProveedoresSeleccionados.GetLength(0) > 1)
            {
                // Informo que solo se puede seleccionar un proveedor
                MessageBox.Show("Solo puede seleccionar un Proveedor!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Vuelvo a abrir el formulario de búsqueda de proveedores
                // LLamo al formulario que busca los proveedores
                frmProveedoresBuscar myForm = new frmProveedoresBuscar(true, true);
                // Lo muestro
                myForm.ShowDialog();
            }
            // Si hay algún proveedor seleccionado
            if (clsGlobales.ProveedoresSeleccionados.GetLength(0) > 0)
            {
                // Recorro el vector hasta que encuentro un Id de proveedor y lo paso a los controles del formulario
                for (int i = 0; i <= clsGlobales.ProveedoresSeleccionados.GetLength(0); i++)
                {
                    // Si la posición tiene un ID de proveedor, busco los datos del mismo
                    if (clsGlobales.ProveedoresSeleccionados[0] > 0)
                    {
                        // Cargo los datos del proveedor
                        CargarProveedores(clsGlobales.ProveedoresSeleccionados[0]);
                        // Los paso al formulario
                        PasarDatosAlFormulario();
                        // Cambio el estado de la bandera que indica que se seleccionó un proveedor
                        bProveedor = true;
                    }
                }
            }

        }

        #endregion

        #region Método que carga los datos de los proveedores a la clase

        private void CargarProveedores(int Id)
        {
            // Armo la cadena SQL
            string myCadenaSQL = "select * from Vista_Proveedores where IdProveedor = " + Id;
            // Creo una tabla que me va a almacenar el resultado de la consulta
            DataTable myTablaProveedores = clsDataBD.GetSql(myCadenaSQL);
            // Recorro la tabla y le asigo los datos del proveedor al objeto
            foreach (DataRow rowProv in myTablaProveedores.Rows)
            {
                // Primero los Int
                myProveedor.IdProveedor = Convert.ToInt32(rowProv["IdProveedor"]);
                myProveedor.IdCondicionIva = Convert.ToInt32(rowProv["IdCondicionIva"]);
                myProveedor.PorcentajeIva = Convert.ToDouble(rowProv["Porcentaje"]);
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

        #region Método que calcula el total de la factura

        private void CalcularTotal()
        {
            // Variable que va acumulando el contenido de los textbox
            double dAcumulaTotal = 0;
            // Voy recorriendo los txt y acumulo los valores si hay
            // NETO
            if (!(string.IsNullOrEmpty(txtNeto.Text)) && (clsGlobales.cValida.IsNumeric(txtNeto.Text)))
            {
                dAcumulaTotal = dAcumulaTotal + Convert.ToDouble(txtNeto.Text);
            }
            //IVA 2.5
            if (!(string.IsNullOrEmpty(txtIva25.Text)) && (clsGlobales.cValida.IsNumeric(txtIva25.Text)))
            {
                dAcumulaTotal = dAcumulaTotal + Convert.ToDouble(txtIva25.Text);
            }
            //IVA 5
            if (!(string.IsNullOrEmpty(txtIva50.Text)) && (clsGlobales.cValida.IsNumeric(txtIva50.Text)))
            {
                dAcumulaTotal = dAcumulaTotal + Convert.ToDouble(txtIva50.Text);
            }
            //IVA 10.5
            if (!(string.IsNullOrEmpty(txtIva105.Text)) && (clsGlobales.cValida.IsNumeric(txtIva105.Text)))
            {
                dAcumulaTotal = dAcumulaTotal + Convert.ToDouble(txtIva105.Text);
            }
            //IVA 21
            if (!(string.IsNullOrEmpty(txtIva210.Text)) && (clsGlobales.cValida.IsNumeric(txtIva210.Text)))
            {
                dAcumulaTotal = dAcumulaTotal + Convert.ToDouble(txtIva210.Text);
            }
            //IVA 27
            if (!(string.IsNullOrEmpty(txtIva270.Text)) && (clsGlobales.cValida.IsNumeric(txtIva270.Text)))
            {
                dAcumulaTotal = dAcumulaTotal + Convert.ToDouble(txtIva270.Text);
            }
            //PERC. VARIAS
            if (!(string.IsNullOrEmpty(txtPercVarias.Text)) && (clsGlobales.cValida.IsNumeric(txtPercVarias.Text)))
            {
                dAcumulaTotal = dAcumulaTotal + Convert.ToDouble(txtPercVarias.Text);
            }
            //PERC. IVA
            if (!(string.IsNullOrEmpty(txtPercIva.Text)) && (clsGlobales.cValida.IsNumeric(txtPercIva.Text)))
            {
                dAcumulaTotal = dAcumulaTotal + Convert.ToDouble(txtPercIva.Text);
            }
            //PERC. IB
            if (!(string.IsNullOrEmpty(txtPercIB.Text)) && (clsGlobales.cValida.IsNumeric(txtPercIB.Text)))
            {
                dAcumulaTotal = dAcumulaTotal + Convert.ToDouble(txtPercIB.Text);
            }
            //EXENTOS
            if (!(string.IsNullOrEmpty(txtNoGravados.Text)) && (clsGlobales.cValida.IsNumeric(txtNoGravados.Text)))
            {
                dAcumulaTotal = dAcumulaTotal + Convert.ToDouble(txtNoGravados.Text);
            }

            // Muestro en el textbox el resultado
            txtTotalAcumulado.Text = dAcumulaTotal.ToString("#0.00");
            // Si el total acumulado es igual el total de la factura, habilito el botón aceptar
            if (Convert.ToDouble(txtTotalFact.Text) == Convert.ToDouble(dAcumulaTotal.ToString("#0.00"))) 
            {
                btnAceptar.Enabled = true;
            }
            else
            {
                btnAceptar.Enabled = false;
            }
        }

        #endregion

        #region Metodo: PasarDatosAlFormulario

        private void PasarDatosAlFormulario()
        {
            // Paso los datos del proveedor al formulario
            this.txtCodigoProv.Text = myProveedor.IdProveedor.ToString();
            this.txtRSoc.Text = myProveedor.RazonSocial;
            this.txtCUIT.Text = myProveedor.CUIT;
            this.txtCondicionIva.Text = myProveedor.CondicionIva;
            this.txtTel.Text = myProveedor.Telefono;
            this.txtFax.Text = myProveedor.Fax;

            this.cboCondCompra.SelectedValue = myProveedor.IdCondicionCompra;

            if (!(IdFactura == 0))
            {
                this.cboCondCompra.SelectedValue = myComprobanteViejo.IdCondicionCompra;
                this.txtComprador.Text = myComprobanteViejo.Usuario;
                this.dtpFecha.Value = myComprobanteViejo.FechaReal;
                this.txtNumero.Text = myComprobanteViejo.Numero;
                bBandera = true;
                this.cboImputacion.SelectedValue = myComprobanteViejo.IdImputacion;

                this.txtDetalle.Text = myComprobanteViejo.Descripcion;

                this.txtNeto.Text = myComprobanteViejo.Neto.ToString("#0.00");

                this.txtNeto.Text = myComprobanteViejo.Neto.ToString("#0.00");
                this.txtIva25.Text = myComprobanteViejo.Iva25.ToString("#0.00");
                this.txtIva50.Text = myComprobanteViejo.Iva50.ToString("#0.00");
                this.txtIva105.Text = myComprobanteViejo.Iva105.ToString("#0.00");
                this.txtIva210.Text = myComprobanteViejo.Iva210.ToString("#0.00");
                this.txtIva270.Text = myComprobanteViejo.Iva270.ToString("#0.00");
                this.txtPercVarias.Text = myComprobanteViejo.PercepcionesVarias.ToString("#0.00");
                this.txtPercIva.Text = myComprobanteViejo.PercepcionesIva.ToString("#0.00");
                this.txtPercIB.Text = myComprobanteViejo.PercepcionesIB.ToString("#0.00");
                this.txtNoGravados.Text = myComprobanteViejo.Exento.ToString("#0.00");
                this.txtTotalAcumulado.Text = myComprobanteViejo.Total.ToString("#0.00");
            }
                       

        }

        #endregion

        #region Método que pasa los datos a la clase

        private void PasarDatosALaClase()
        {

            //Evaluar tipo de comprobante
            switch (cboTC.SelectedIndex)
            {
                case 0:
                    myComprobanteNuevo.IdTipoComprobanteCompra = 3;
                    break;
                case 1:                
                    myComprobanteNuevo.IdTipoComprobanteCompra = 4;
                    break;
                case 2:
                    myComprobanteNuevo.IdTipoComprobanteCompra = 7;
                    break; 
            }

            // Paso los datos de los txt a las propiedades
            myComprobanteNuevo.IdProveedor = Convert.ToInt32(txtCodigoProv.Text);
            myComprobanteNuevo.IdPuntoVenta = Convert.ToInt32(cboPunto.SelectedValue);
            myComprobanteNuevo.IdAlmacen = Convert.ToInt32(cboAlmacen.SelectedValue);
            myComprobanteNuevo.IdCondicionCompra = Convert.ToInt32(cboCondCompra.SelectedValue);
            myComprobanteNuevo.Fecha = clsValida.ConvertirFecha(dtpFecha.Value);
            myComprobanteNuevo.FechaReal = dtpFecha.Value;
            myComprobanteNuevo.Numero = txtNumero.Text;
            myComprobanteNuevo.CantidadArticulos = 0;

            if (string.IsNullOrEmpty(txtDetalle.Text))
            { myComprobanteNuevo.Descripcion = ""; }
            else
            { myComprobanteNuevo.Descripcion = txtDetalle.Text; }

            if (Convert.ToDouble(txtNeto.Text) == null)
            { myComprobanteNuevo.Neto = 0;}
            else
            { myComprobanteNuevo.Neto = Convert.ToDouble(txtNeto.Text); }
            
            if (Convert.ToDouble(txtIva25.Text) == null)
            {myComprobanteNuevo.Iva25 = 0;}
            else
            {myComprobanteNuevo.Iva25 = Convert.ToDouble(txtIva25.Text);}

            if (Convert.ToDouble(txtIva50.Text) == null)
            { myComprobanteNuevo.Iva50 = 0;}
            else
            { myComprobanteNuevo.Iva50 = Convert.ToDouble(txtIva50.Text); }
            
            if (Convert.ToDouble(txtIva105.Text)==null)
            {myComprobanteNuevo.Iva105 = 0;}
            else
            {myComprobanteNuevo.Iva105 = Convert.ToDouble(txtIva105.Text);}
            
            if (Convert.ToDouble(txtIva210.Text)==null)
            {myComprobanteNuevo.Iva210 = 0;}
            else
            {myComprobanteNuevo.Iva210 = Convert.ToDouble(txtIva210.Text);}

            if (Convert.ToDouble(txtIva270.Text) == null)
            { myComprobanteNuevo.Iva270 = 0; }
            else
            { myComprobanteNuevo.Iva270 = Convert.ToDouble(txtIva270.Text); }

            if (Convert.ToDouble(txtTotalAcumulado.Text) == null)
            { myComprobanteNuevo.Total = 0; }
            else
            { myComprobanteNuevo.Total = Convert.ToDouble(txtTotalAcumulado.Text); }

            if (Convert.ToDouble(txtTotalAcumulado.Text) == null)
            { myComprobanteNuevo.Saldo = 0; }
            else
            { myComprobanteNuevo.Saldo = Convert.ToDouble(txtTotalAcumulado.Text); }
            
            myComprobanteNuevo.Activo = 1;

            if (Convert.ToDouble(txtPercVarias.Text) == null)
            { myComprobanteNuevo.PercepcionesVarias = 0; }
            else
            { myComprobanteNuevo.PercepcionesVarias = Convert.ToDouble(txtPercVarias.Text); }

            if (Convert.ToDouble(txtPercIva.Text) == null)
            { myComprobanteNuevo.PercepcionesIva = 0; }
            else
            { myComprobanteNuevo.PercepcionesIva = Convert.ToDouble(txtPercIva.Text); }

            if (Convert.ToDouble(txtPercIB.Text) == null)
            { myComprobanteNuevo.PercepcionesIB = 0; }
            else
            { myComprobanteNuevo.PercepcionesIB = Convert.ToDouble(txtPercIB.Text); }

            if (Convert.ToDouble(txtNoGravados.Text) == null)
            { myComprobanteNuevo.Exento = 0; }
            else
            { myComprobanteNuevo.Exento = Convert.ToDouble(txtNoGravados.Text); }
            

            myComprobanteNuevo.Flete = 0;
            
            myComprobanteNuevo.Usuario = clsGlobales.UsuarioLogueado.Usuario;
            
            myComprobanteNuevo.IdImputacion = Convert.ToInt32(cboImputacion.SelectedValue);
            
            myComprobanteNuevo.Compra = 0;
            
            myComprobanteNuevo.Usuario = clsGlobales.UsuarioLogueado.Usuario;

            if (cboTC.SelectedIndex == 1) //NC            
            { 
                myComprobanteNuevo.Neto =myComprobanteNuevo.Neto * (-1);
                myComprobanteNuevo.Iva25 = myComprobanteNuevo.Iva25 * (-1);
                myComprobanteNuevo.Iva50 = myComprobanteNuevo.Iva50 * (-1);
                myComprobanteNuevo.Iva105 = myComprobanteNuevo.Iva105 * (-1);
                myComprobanteNuevo.Iva210 = myComprobanteNuevo.Iva210 * (-1);
                myComprobanteNuevo.Iva270 = myComprobanteNuevo.Iva270 * (-1);
                myComprobanteNuevo.Total = myComprobanteNuevo.Total * (-1);
                myComprobanteNuevo.Saldo = myComprobanteNuevo.Saldo * (-1);
                myComprobanteNuevo.PercepcionesVarias = myComprobanteNuevo.PercepcionesVarias * (-1);
                myComprobanteNuevo.PercepcionesIva = myComprobanteNuevo.PercepcionesIva * (-1);
                myComprobanteNuevo.PercepcionesIB = myComprobanteNuevo.PercepcionesIB * (-1);
                myComprobanteNuevo.Exento = myComprobanteNuevo.Exento * (-1);
            }
            
        }

        #endregion

        #region Método que carga los datos de la factura a modificar

        private void CargarFacturaEditar()
        {
            // Armo la cadena SQL
            myCadenaSQL = "select * from Vista_ComprobantesCompras where Id = " + IdFactura;
            // Creo la tabla para la grilla

            // le paso los datos de la consulta SQL
            DataTable myTablaComprobante = clsDataBD.GetSql(myCadenaSQL);

            foreach (DataRow rowComp in myTablaComprobante.Rows)
            {
                
                myComprobanteViejo.IdTipoComprobanteCompra = Convert.ToInt32(rowComp["IdTipo"]);
                myComprobanteViejo.IdProveedor = Convert.ToInt32(rowComp["IdProveedor"]);
                myComprobanteViejo.IdPuntoVenta = Convert.ToInt32(rowComp["IdPunto"]);
                myComprobanteViejo.IdAlmacen = Convert.ToInt32(rowComp["IdAlmacen"]);
                myComprobanteViejo.IdCondicionCompra = Convert.ToInt32(rowComp["IdCondicion"]);
                
                myComprobanteViejo.CantidadArticulos = Convert.ToInt32(rowComp["Cantidad"]);
                myComprobanteViejo.Fecha = (rowComp["Fecha"]).ToString();
                myComprobanteViejo.FechaReal = Convert.ToDateTime(rowComp["FechaReal"]);
                myComprobanteViejo.Numero = (rowComp["Numero"]).ToString();
                myComprobanteViejo.Vence = (rowComp["Vence"]).ToString();
                myComprobanteViejo.Neto = Convert.ToDouble(rowComp["Neto"]);
                myComprobanteViejo.Activo = Convert.ToInt32(rowComp["Activo"]);
                // Campos exclusivos de la factura de compras
                myComprobanteViejo.Iva25 = Convert.ToDouble(rowComp["IVA25"]);
                myComprobanteViejo.Iva50 = Convert.ToDouble(rowComp["IVA50"]);
                myComprobanteViejo.Iva105 = Convert.ToDouble(rowComp["IVA105"]);
                myComprobanteViejo.Iva210 = Convert.ToDouble(rowComp["IVA210"]);
                myComprobanteViejo.Iva270 = Convert.ToDouble(rowComp["IVA270"]);
                myComprobanteViejo.Total = Convert.ToDouble(rowComp["Total"]);
                myComprobanteViejo.Saldo = Convert.ToDouble(rowComp["Saldo"]);
                myComprobanteViejo.PercepcionesVarias = Convert.ToDouble(rowComp["PercepcionesVarias"]);
                myComprobanteViejo.PercepcionesIva = Convert.ToDouble(rowComp["PercepcionesIva"]);
                myComprobanteViejo.PercepcionesIB = Convert.ToDouble(rowComp["PercepcionesIB"]);
                myComprobanteViejo.Exento = Convert.ToDouble(rowComp["NoGravados"]);
                myComprobanteViejo.IdImputacion = Convert.ToInt32(rowComp["IdImputacion"]);
                myComprobanteViejo.Flete = Convert.ToDouble(rowComp["Flete"]);

            }
            // Busco los datos del proveedor en la tabla proveedores
            CargarProveedores(myComprobanteViejo.IdProveedor);
            // Paso los datos de las clases al formulario
            PasarDatosAlFormulario();
        }

        #endregion

        #region Método que inhabilita todos los controles del formulario btnEditar

        private void InhabilitarTodo()
        {
            this.gpbProveedores.Enabled = false;
            this.gpbCabecera.Enabled = false;
            this.gpbFlete.Enabled = false;
            this.gpbDetalle.Enabled = false;
            this.gpbCierre.Enabled = false;
            this.btnAceptar.Enabled = false;
        }

        #endregion

        #region Método que actualiza el saldo del proveedor

        private void ActualizarSaldoProveedor(int IdProv, double Tot)
        {
            // Variable que almacena el saldo del proveedor
            double SaldoProv = 0;
            // Armo la cadena SQl para traer el saldo anterior del proveedor
            string myCadenaSQLSaldo = "select * from Proveedores where IdProveedor = " + IdProv;
            // Ejecuto la consulta y paso los datos a la tabla
            DataTable mySaldoProveedor = clsDataBD.GetSql(myCadenaSQLSaldo);
            // Recorro la tabla para obtener el saldo inicial del proveedor
            foreach (DataRow rowProv in mySaldoProveedor.Rows)
            {
                // Paso a la variable el saldo anterior
                SaldoProv = Convert.ToDouble(rowProv["Saldo"]);
                // Actualizo el saldo
                SaldoProv = SaldoProv + Tot;
            }
            // Armo la consulta para actualizar el dato
            myCadenaSQLSaldo = "update Proveedores set Saldo = " + SaldoProv + " where IdProveedor = " + IdProv;
            // Actualizo el saldo
            clsDataBD.GetSql(myCadenaSQLSaldo);
        }

        #endregion

        #endregion

                #region Método que actualiza el saldo del proveedor B

        private void ActualizarSaldoProveedorB(int IdProv, double Tot)
        {
            // Variable que almacena el saldo del proveedor
            double SaldoProv = 0;
            // Armo la cadena SQl para traer el saldo anterior del proveedor
            string myCadenaSQLSaldo = "select * from SaldoCliProv where IdProveedor = " + IdProv;
            string myCad = "";
            // Ejecuto la consulta y paso los datos a la tabla
            DataTable mySaldoProveedor = clsDataBD.GetSqlB(myCadenaSQLSaldo);

            //No existe... lo crea
            if (mySaldoProveedor.Rows.Count==0)
            {
                myCad = "insert into SaldoCliProv (IdCliente, SaldoCli, IdProveedor, SaldoProv, SaldoInicial, SaldoAFavor) values (" +
                                "0, 0," + IdProv + ",0, 0, 0)";
                // Ejecuto la consulta 
                clsDataBD.GetSqlB(myCad);
                
                //Update saldo
                myCadenaSQLSaldo = "update SaldoCliProv set SaldoProv = " + Tot + " where IdProveedor = " + IdProv;
                // Actualizo el saldo
                clsDataBD.GetSqlB(myCadenaSQLSaldo);

            }
            else //Recorre y updatea
            {
                // Recorro la tabla para obtener el saldo inicial del proveedor
                foreach (DataRow rowProv in mySaldoProveedor.Rows)
                {
                    // Paso a la variable el saldo anterior
                    SaldoProv = Convert.ToDouble(rowProv["SaldoProv"]);
                    // Actualizo el saldo
                    SaldoProv = SaldoProv + Tot;
                }
                // Armo la consulta para actualizar el dato
                myCadenaSQLSaldo = "update SaldoCliProv set SaldoProv = " + SaldoProv + " where IdProveedor = " + IdProv;
                // Actualizo el saldo
                clsDataBD.GetSqlB(myCadenaSQLSaldo);
            }
        }

        #endregion

    }
}

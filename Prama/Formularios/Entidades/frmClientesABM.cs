using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Prama.Formularios.Ventas;

namespace Prama
{
    public partial class frmClientesABM : Form
    {

        #region Declaración de elementos usados a nivel del formulario
        // Variable que va a controlar el comportamiento de los combos
        bool myBanderaCombos = false;
        // Creo una nuevo Cliente. G.
        clsCLientes NuevoCliente = new clsCLientes();
        // Creo Otro cliente para guardar los datos originales del Cliente
        clsCLientes ViejoCliente = new clsCLientes();
        //Otras
        string myEstado = "C";        
        int indexFila = 0;      //Reposicionamiento        
        bool bSearch = false;   //Para Reposicionamiento ( pos busqueda )        
        int IdCliente = 0;      //Guarda Id Cliente    
        bool bABMPresu = false; //Cuando se busca solo editar un cliente unico
        int LastIdCli = 0;      //Almacenar, pos alta, ultimo Id Cliente para reposicionar
        bool bAddCli = false;   //Almacenar por si viene para nueva alta desde busqueda de cliente en pedidos
        bool bShowPed = false;  //Almacenar por si debe mostrar boton de ir a Pedidos o no
        #endregion

        #region CONSTRUCTOR

        public frmClientesABM(int p_CodCli = 0, bool p_ABMPresu =false, bool p_AddCli = false, bool p_ShowPed=false)
        {
            InitializeComponent();

            //VIENE DE PEDIDO O PRESUPUESTO?
            if (!(p_CodCli == 0 && p_ABMPresu == false))
            {
                IdCliente = p_CodCli;
                bABMPresu = true;
            }
            //Invocar boton agregar automaticamente ( Add Cliente )?
                bAddCli = p_AddCli;

            //Mostrar Boton de Ir a Pedidos?
                bShowPed = p_ShowPed;
        }   

        #endregion


        #region Eventos del formulario

        #region Evento Load del Formulario

        private void frmClientesABM_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            // LLeno los combos del formulario
            LlenarCombos();
            // Cargo la grilla con los datos de los clientes
            CargarGrilla();
            // Llamo al método activar los botones del formulario. G.
            ActivarBotones();
            // Llamo al método habilitar controles del formulario. G.
            HabilitarControles();
            // Cargar ToolTips
            CargarToolTips();
            //Titulo Ventana
            this.Text = clsGlobales.cFormato.getTituloVentana() + " - CLIENTES";
            //Limpiar Variables globales 
            clsGlobales.IdProv = 0;
            clsGlobales.IdLoc = 0;
            clsGlobales.CP = "";
            //Es un alta directa?
            if (bAddCli)
            {
                btnAgregar.PerformClick();
            }
        }

        #endregion

        #region Evento CellContentClick de la Grilla DGVClientes

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvClientes.RowCount == 0)
            {
                return;
            }

            //Vaciar ---- NUEVO.... 28-05-2020
            if (this.myEstado == "M")
            {
                //Limpiar Variables globales 
                clsGlobales.IdProv = 0;
                clsGlobales.IdLoc = 0;
                clsGlobales.CP = "";            
            }

            // Traigo los datos de la grilla
            TraerDatosGrilla();
        }

        #endregion

        #region Evento SelectionChanged de la grilla DGVClientes

        private void dgvClientes_SelectionChanged(object sender, EventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvClientes.RowCount == 0)
            {
                return;
            }
            // Traigo los datos de la grilla
            TraerDatosGrilla();
        }

        #endregion

        #region Eventos de los botones

        #region Evento Click del botón Salir

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //Limpiar Variables globales 
            clsGlobales.IdProv = 0;
            clsGlobales.IdLoc = 0;
            clsGlobales.CP = "";

            // Cierro el formulario
            this.Close();
        }

        #endregion

        #region Evento Click del botón Agregar

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Capturo la posición de la grilla desde donde se presionó el botón
            if (dgvClientes.Rows.Count > 0)
            {
                this.indexFila = this.dgvClientes.CurrentRow.Index;
            }

            // Pregunto si el usuario actual tiene nivel mayor a 2, lo dejo agregar. G.
            if (clsGlobales.UsuarioLogueado.Nivel > 2)
            {
                // Cambio el estado del formulario a agregar. G.
                this.myEstado = "A";
                // Limpio los controles del formulario. G.    
                LimpiarControlesForm();
                // Activo los botones para este estado. G.
                ActivarBotones();
                // Habilito los controles para este estado. G.
                HabilitarControles();
                // Muestro el próximo Número de Cliente en el txtCodigo
               //txtCodigo.Text = (clsDataBD.RetornarUltimoId("Clientes", "IdCliente") + 1).ToString();
                //Correo automatico
                this.cboTransporte.SelectedValue = 1;
                this.cboCompra.SelectedValue = 3;
                this.cboTipoCliente.SelectedValue = 28;
                this.cboIva.SelectedValue = 5;
                this.
                // Posiciono el foco sobre el primer textbox
                txtRazonSocial.Focus();
            }
        }

        #endregion

        #region Evento Click del botón Modificar

        private void btnModificar_Click(object sender, EventArgs e)
        {
            // Capturo la posición de la grilla desde donde se presionó el botón
            if (dgvClientes.Rows.Count > 0)
            {
                this.indexFila = this.dgvClientes.CurrentRow.Index;
            }
            
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvClientes.CurrentRow;
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvClientes.RowCount == 0)
            {
                // Salgo de la rutina. G.
                return;
            }
            // // Pregunto si el usuario actual tiene nivel mayor a 2, lo dejo agregar. G.
            if (clsGlobales.UsuarioLogueado.Nivel > 2)
            {
                // Cambio el estado del formulario a Modificar. G.
                this.myEstado = "M";
                // Traigo los datos de la grilla
                TraerDatosGrilla();
                // Activo los botones para este estado. G.
                ActivarBotones();
                // Habilito los controles para este estado. G.
                HabilitarControles();
                // Combo Localidades N.
                //setComboLocalidad();
                // Posiciono el foco sobre el primer textbox
                txtRazonSocial.Focus();
            }

            else
            {
                // Pregunto si el usuario actual tiene nivel menor o igual a 2, No lo dejo agregar. G.
                this.myEstado = "C";
                // El usuario no es de nivel 3, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para modificar un Cliente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento Click del botón Cancelar

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Tomo datos de la grilla
            clsGlobales.IdProv = 0;
            clsGlobales.IdLoc = 0;
            clsGlobales.CP = "";

            //Recargar
            if (this.myEstado == "B" && bSearch)
            {
                // Cargo las localidades
                this.CargarGrilla();
                // Cambio el estado del formulario a agregar. G.
                this.myEstado = "C";
                // Limpio los controles del formulario. G.    
                ActivarBotones();
                // Habilito los controles para este estado. G.
                HabilitarControles();
                //Foco
                PosicionarFocoFila();
            }
            else
            {
                // Cambio el estado del formulario a agregar. G.
                this.myEstado = "C";
                // Limpio los controles del formulario. G.    
                ActivarBotones();
                // Habilito los controles para este estado. G.
                HabilitarControles();
                //Foco
                PosicionarFocoFila();
            }

            //.F.
            bSearch = false;
            
        }

        #endregion

        #region Evento CLick del botón Borrar

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            // Capturo la posición de la grilla desde donde se presionó el botón
            this.indexFila = this.dgvClientes.CurrentRow.Index;

            // Pregunto si el usuario actual es el mismo que se quiere modificar. G.
            if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja)
            {
                //TRAER LA FILA ACTUAL. N.
                DataGridViewRow row = dgvClientes.CurrentRow;
                // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
                int Id = Convert.ToInt32(row.Cells["Codigo"].Value);
                string Clie = row.Cells["RazonSocial"].Value.ToString();
                int Niv = clsGlobales.UsuarioLogueado.Nivel;

               if (Niv < clsGlobales.cParametro.NivelBaja)
                {
                    // El usuario no es de nivel 5, por lo que no puede crear un nuevo usuario. G.
                    MessageBox.Show("Usted no tiene los permisos para Eliminar este CLiente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Ejecuto el botón cancelar. G.
                    this.btnCancelar.PerformClick();
                }
                else
                {
                    // Confirma salir de la aplicación ?
                    DialogResult dlResult = MessageBox.Show("Desea Eliminar el CLiente " + Clie + " ?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    // Si confirma salir de la aplicación....
                    if (dlResult == DialogResult.Yes)
                    {
                        string myCadena = "UPDATE Clientes SET Activo = 0 WHERE Clientes.IdCLiente =" + Id;
                        clsDataBD.GetSql(myCadena);
                        // Refresco la grilla
                        CargarGrilla();
                    }
                }

            }
            else
            {
                // El usuario no es de nivel 5, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para Eliminar un usuario", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento Click del botón Aceptar

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Creo un string que me va a almacenar la sentencia SQL a Ejecutar
            string myCadena = "";
            
            //Liberar
            this.LastIdCli = 0;

            DataGridViewRow row = null;

            if (this.myEstado == "M") //????????????????
            {
                // Tomo la línea actual de la grilla. G.
                row = dgvClientes.CurrentRow;
                // Guardo en la variable la posicion de la fila seleccionada
                //myPosicion = row.Index;

                // Cargo los datos de la fila seleccionada en las propiedades del Viejo Cliente. G.
                ViejoCliente.Codigo = Convert.ToInt32(row.Cells["Codigo"].Value);
                ViejoCliente.RazonSocial = row.Cells["RazonSocial"].Value.ToString().ToUpper();

                if (!(row.Cells["ApeNom"].Value is DBNull))
                {
                    ViejoCliente.ApeNom = "";
                }
                else
                {
                    ViejoCliente.ApeNom = row.Cells["ApeNom"].Value.ToString().ToUpper();
                }
                

                ViejoCliente.Cuit = row.Cells["Cuit"].Value.ToString();
                ViejoCliente.Direccion = row.Cells["Direccion"].Value.ToString().ToUpper();
                ViejoCliente.Barrio = row.Cells["Barrio"].Value.ToString().ToUpper();
                ViejoCliente.CP = row.Cells["CP"].Value.ToString().ToUpper();
                ViejoCliente.Telefono = row.Cells["Telefono"].Value.ToString();
                ViejoCliente.Celular = row.Cells["Celular"].Value.ToString();
                ViejoCliente.Fax = row.Cells["Fax"].Value.ToString();
                ViejoCliente.Mail = row.Cells["Mail"].Value.ToString();
                ViejoCliente.Web = row.Cells["Web"].Value.ToString();
                ViejoCliente.Observaciones = row.Cells["Observaciones"].Value.ToString().ToUpper();
                // Cargo los datos de la fila en los correspondientes ComboBox
                ViejoCliente.IdTipoCliente = Convert.ToInt32(row.Cells["IdTipo"].Value);
                ViejoCliente.IdLocalidad = Convert.ToInt32(row.Cells["IdLocalidad"].Value);
                ViejoCliente.IdProvincia = Convert.ToInt32(row.Cells["IdProvincia"].Value);
                ViejoCliente.IdCondicionIva = Convert.ToInt32(row.Cells["IdCondicionIva"].Value);
                ViejoCliente.IdTransporte = Convert.ToInt32(row.Cells["IdTransporte"].Value);
                ViejoCliente.IdCondicionCompra = Convert.ToInt32(row.Cells["IdCondicionCompra"].Value);
                // Cargo los datos de la fila (fechas) a los DTP
                ViejoCliente.Alta = row.Cells["Alta"].Value.ToString();
                ViejoCliente.Nacimiento = row.Cells["Nacimiento"].Value.ToString();

                if (Convert.ToBoolean(row.Cells["CRsal"].Value.ToString())==true)
                {
                    ViejoCliente.chkCRsal = 1;
                } else { ViejoCliente.chkCRsal = 0;}
               
            }

            // Cargo los datos de los texbox al nuevo cliente para Guardarlo en la tabla. G.
            NuevoCliente.RazonSocial = txtRazonSocial.Text.ToUpper();
            NuevoCliente.ApeNom = txtRazonSocial.Text.ToUpper();
            NuevoCliente.Cuit = txtCuit.Text.ToUpper();
            NuevoCliente.Direccion = txtDireccion.Text.ToUpper();
            NuevoCliente.Barrio = txtBarrio.Text.ToUpper();
            NuevoCliente.CP = txtCP.Text.ToUpper();
            NuevoCliente.Telefono = txtTelefono.Text.ToUpper();
            NuevoCliente.Celular = txtCelular.Text.ToUpper();
            NuevoCliente.Fax = txtFax.Text.ToUpper();
            NuevoCliente.Mail = txtMail.Text;
            NuevoCliente.Web = txtWeb.Text;
            NuevoCliente.Observaciones = txtObservaciones.Text.ToUpper();
            if (cboIva.SelectedIndex == -1)
            {
                NuevoCliente.IdCondicionIva = 0;
            }
            else
            {
                NuevoCliente.IdCondicionIva = Convert.ToInt32(cboIva.SelectedValue);
            }
            if (cboTransporte.SelectedIndex == -1)
            {
                NuevoCliente.IdTransporte = 0;
            }
            else
            {
                NuevoCliente.IdTransporte = Convert.ToInt32(cboTransporte.SelectedValue);
            }
            if (cboCompra.SelectedIndex == -1)
            {
                NuevoCliente.IdCondicionCompra = 0;
            }
            else
            {
                NuevoCliente.IdCondicionCompra = Convert.ToInt32(cboCompra.SelectedValue);
            }
        
            // Cargo los datos de la fila en los correspondientes ComboBox
            NuevoCliente.IdTipoCliente = Convert.ToInt32(cboTipoCliente.SelectedValue);
            NuevoCliente.Saldo = Convert.ToDouble(txtSaldo.Text);

            if (chkCRsal.Checked)
            { NuevoCliente.chkCRsal = 1; }
            else { NuevoCliente.chkCRsal = 0; }
                
            // Cargo los datos de la fila (fechas) a los DTP
            DateTime fAlta = dtpAlta.Value;
            string fAltaDia = fAlta.Day.ToString("00");
            string fAltaMes = fAlta.Month.ToString("00");
            string fAltaAño = fAlta.Year.ToString("0000");

            DateTime fNacimineto = dtpFechaNacimiento.Value;
            string fNaciminetoDia = fNacimineto.Day.ToString();
            string fNaciminetoMes = fNacimineto.Month.ToString();
            string fNaciminetoAño = fNacimineto.Year.ToString();
            // Concateno los el día, mes y año.
            NuevoCliente.Alta = fAltaMes + "/" + fAltaDia + "/" + fAltaAño;
            NuevoCliente.Nacimiento = fNaciminetoMes + "/" + fNaciminetoDia + "/" + fNaciminetoAño;

            //VERIFICAR SI SE MODIFICO O NO PROV Y LOC, CP. **
                if (this.myEstado == "A")
                {
                    NuevoCliente.SaldoAFavor = 0;
                    NuevoCliente.SaldoInicial = 0;
                    NuevoCliente.IdLocalidad = clsGlobales.IdLoc;
                    NuevoCliente.IdProvincia = clsGlobales.IdProv;
                    NuevoCliente.CP = clsGlobales.CP.ToUpper();
                }
                else if (this.myEstado=="M")
                {
                    if (!(clsGlobales.IdLoc == 0 || clsGlobales.IdProv == 0))
                    {
                        NuevoCliente.IdLocalidad = clsGlobales.IdLoc;
                        NuevoCliente.IdProvincia = clsGlobales.IdProv;
                        NuevoCliente.CP = clsGlobales.CP.ToUpper();
                    }
                    else
                    {
                        NuevoCliente.IdLocalidad = Convert.ToInt32(row.Cells["IdLocalidad"].Value.ToString());
                        NuevoCliente.IdProvincia = Convert.ToInt32(row.Cells["IdProvincia"].Value.ToString());
                        NuevoCliente.CP = row.Cells["CP"].Value.ToString().ToUpper();
                    }
                }

            //************************************************

            // SI ESTOY BUSCANDO, NO VERIFICO ERRORES DE CARGA
            if (!(this.myEstado == "B"))
            {
                //Vector Errores
                string[] cErrores = NuevoCliente.cValidaCLiente();
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
            }
            
            // ESTABAMOS MODIFICANDO?
            if (this.myEstado == "M")
            {

                // Valido los datos cargados
                ValidarCliente();
                // Le paso a la clase cliente el Codigo del cliente que estoy modificando
                NuevoCliente.Codigo = Convert.ToInt32(row.Cells["Codigo"].Value);
                // Creo la cadena para grabar las Modificaciones del cliente
                myCadena = "exec proc_frmClientesABM_Modificar_CLiente " +
                     NuevoCliente.Codigo + ", " + NuevoCliente.IdCondicionIva + "," + NuevoCliente.IdCondicionCompra + "," + NuevoCliente.IdTransporte + ",'" + NuevoCliente.RazonSocial + "','" + NuevoCliente.ApeNom + "','" + NuevoCliente.Cuit +
                    "','" + NuevoCliente.Direccion + "', '" + NuevoCliente.Barrio + "', " + NuevoCliente.IdLocalidad + ", " + NuevoCliente.IdProvincia +
                    ", '" + NuevoCliente.CP + "', '" + NuevoCliente.Telefono + "', '" + NuevoCliente.Celular + "', '" + NuevoCliente.Fax +
                    "', '" + NuevoCliente.Mail + "', '" + NuevoCliente.Web + "', '" + NuevoCliente.Nacimiento +
                    "', '" + NuevoCliente.Observaciones + "', '" + NuevoCliente.Alta + "', " + NuevoCliente.IdTipoCliente 
                    + "," + NuevoCliente.Saldo + "," + NuevoCliente.chkCRsal;

                // Ejecuto la consulta SQL
                clsDataBD.GetSql(myCadena);
                // Lleno nuevamente la grilla
                CargarGrilla();
                // Regreso el formulario a su estado inicial
                this.btnCancelar.PerformClick();
            }

            //ESTABAMOS EN MODO ALTA?
            if (this.myEstado == "A")
            {

                //DATOS DE PROVINCIA, LOCALIDAD Y CP
                NuevoCliente.SaldoAFavor = 0;
                NuevoCliente.SaldoInicial = 0;
                NuevoCliente.IdLocalidad = clsGlobales.IdLoc;
                NuevoCliente.IdProvincia = clsGlobales.IdProv;
                NuevoCliente.CP = clsGlobales.CP.ToUpper();
                NuevoCliente.Activo = 1;
                

                // Valido los datos cargados
                ValidarCliente();
                // Creo la cadena para grabar el nuevo Cliente
                myCadena = "exec proc_frmClientesABM_Alta_CLiente " + NuevoCliente.IdCondicionIva + "," + NuevoCliente.IdCondicionCompra + "," + NuevoCliente.IdTransporte +
                    ",'" + NuevoCliente.RazonSocial + "','" + NuevoCliente.ApeNom + "','" + NuevoCliente.Cuit +
                    "', '" + NuevoCliente.Direccion + "', '" + NuevoCliente.Barrio + "', " + NuevoCliente.IdLocalidad + ", " + NuevoCliente.IdProvincia +
                    ", '" + NuevoCliente.CP + "', '" + NuevoCliente.Telefono + "', '" + NuevoCliente.Celular + "', '" + NuevoCliente.Fax +
                    "', '" + NuevoCliente.Mail + "', '" + NuevoCliente.Web + "', '" + NuevoCliente.Nacimiento +
                    "', '" + NuevoCliente.Observaciones + "', '" + NuevoCliente.Alta + "', " + NuevoCliente.IdTipoCliente + "," + NuevoCliente.Saldo + "," + NuevoCliente.Activo 
                    + "," + NuevoCliente.SaldoAFavor + ","+ NuevoCliente.SaldoInicial + "," + NuevoCliente.chkCRsal;

                // Ejecuto la consulta SQL
                clsDataBD.GetSql(myCadena);
                // Lleno nuevamente la grilla
                CargarGrilla();
                // Regreso el formulario a su estado inicial
                this.btnCancelar.PerformClick();
                //Obtener Ultimo Id Cliente generado ( Codigo )
                LastIdCli = clsDataBD.RetornarMax("Clientes", "IdCliente");
                //Si fue un Alta...
                if (LastIdCli > 0)
                {
                   //Reposicionarse en la lista
                   this.ReposicionarById(LastIdCli);
                   //Liberar
                   this.LastIdCli = 0;
                }

            }

            //ESTABAMOS EN MODO BUSQUEDA?
            if (this.myEstado == "B")
            {
                //Tomar el Id
                IdCliente = Convert.ToInt32(dgvClientes.CurrentRow.Cells["Codigo"].Value.ToString());
                // Cambio mi estado
                this.myEstado = "C";
                // Lleno nuevamente la grilla
                this.CargarGrilla();
                // Activo todos los botones
                ActivarBotones();
                // Habilito los controles
                HabilitarControles();
                //Id >0? Solo cuando busca reposiciona por ID
                if (!(IdCliente == 0 && bSearch))
                {
                    ReposicionarById();
                    IdCliente = 0;
                }
                else
                {
                    //Foco
                    PosicionarFocoFila();
                }
                //.F.
                bSearch = false;
                //Retornar
                return;
            }

        }

        #endregion

        #region Reposicionar Grilal por Id

        //REPOSICIONA GRILLA POR ID
        private void ReposicionarById(int p_IdCli = 0)
        {
            //Auxiliar
            int Id_Cliente = 0;

            //Evaluar si reposiciona un Id determinado o el Id Cliente del Selected Change de la grlla
            if (p_IdCli == 0)
            { 
                Id_Cliente = IdCliente; 
            }
            else 
            { 
                Id_Cliente = p_IdCli;   
            }

            //recorrer
            foreach (DataGridViewRow myRow in this.dgvClientes.Rows)
            {
                if (Convert.ToInt32(myRow.Cells["Codigo"].Value.ToString()) == Id_Cliente)
                {
                    // Devuelvo el foco a la fila de la grilla desde donde se llamó
                    this.dgvClientes.CurrentCell = dgvClientes[1, myRow.Index];

                    // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                    EventArgs ea = new EventArgs();
                    this.dgvClientes_SelectionChanged(this.dgvClientes, ea);

                    //Salir
                    break;
                }
            }
        }

        #endregion

        #region Evento Click del botón Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Cambio la variable global myEstado a Buscar
            this.myEstado = "B";
            // Capturo la posición de la grilla desde donde se presionó el botón
            if (dgvClientes.Rows.Count > 0)
            {
                this.indexFila = this.dgvClientes.CurrentRow.Index;
            }
            // Habilito los controles del form
            HabilitarControles();
            // Habilito los botones que correspondan
            ActivarBotones();
            // Pongo el foco en el texbox del código
            txtRazonSocialBs.Text="";
            txtCodigoBs.Text = "";
            // Vacío la selección del combo
            cboProvincia.SelectedIndex = -1;
            txtCodigoBs.Focus();
        }

        #endregion

        #region Evento Click Boton Imprimir

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            // Capturo la posición de la grilla desde donde se presionó el botón
            if (dgvClientes.Rows.Count > 0)
            {
                this.indexFila = this.dgvClientes.CurrentRow.Index;
            }

            //Data Set
            dsReportes oDsCli = new dsReportes();
  

            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = dgvClientes.Rows.Count;

            for (int i = 0; i < dgvFilas; i++)
            {
                oDsCli.Tables["DtClientes"].Rows.Add
                (new object[] { dgvClientes[0,i].Value.ToString(),
                dgvClientes[1,i].Value.ToString(),
                dgvClientes[4,i].Value.ToString(), 
                dgvClientes[8,i].Value.ToString(),
                dgvClientes[11,i].Value.ToString(),
                dgvClientes[12,i].Value.ToString(),
                dgvClientes[13,i].Value.ToString(),
                dgvClientes[19,i].Value.ToString(),
                dgvClientes[16,i].Value.ToString()});

            }

            //Objeto Reporte
            rptClientes oRepCli = new rptClientes();
            //Cargar Reporte            
            //    oRepTipoCli.Load(Application.StartupPath + "\\rptTipoCli.rpt");
            oRepCli.Load(Application.StartupPath + "\\rptClientes.rpt");
            //Establecer el DataSet como DataSource
            oRepCli.SetDataSource(oDsCli);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepCli;
            oRepCli.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepCli.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
            oRepCli.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
            oRepCli.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
            oRepCli.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
            oRepCli.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepCli.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
            oRepCli.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();

            // Posiciono el foco en la fila desde donde se llamó
            PosicionarFocoFila();
        }

        #endregion

        #endregion

        #region Eventos KeyPress y Leave de los textbox del formulario

        #region Evento KeyPress del txtCodigo

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Si presiono Enter o Tab
            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                // Si estoy realizando una búsqueda
                if (this.myEstado == "B")
                {
                    btnAceptar.Focus();
                }

            }
        }

        #endregion

        #region Eventos KeyPress y Leave del txtRazonSocial

        private void txtRazonSocial_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Si presiono Enter o TAB
            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                // Si estoy realizando una búsqueda
                if (this.myEstado == "B")
                {
                    btnAceptar.Focus();
                }
                else
                {
                    this.txtCuit.Focus();
                }
            }
        }

        private void txtRazonSocial_Leave(object sender, EventArgs e)
        {
            // Cuando pierdo el foco pongo el campo con mayúsculas
            this.txtRazonSocial.Text = txtRazonSocial.Text.ToUpper();
        }

        #endregion

        #region Eventos KeyPress y Leave del txtCuit

        private void txtCuit_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Si presiono Enter o TAB
            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                cboTipoCliente.Focus();
            }
        }

        #endregion

        #region Eventos KeyPress y Leave del txtDireccion

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                txtBarrio.Focus();
            }
        }

        private void txtDireccion_Leave(object sender, EventArgs e)
        {
            this.txtDireccion.Text = txtDireccion.Text.ToUpper();
        }

        #endregion

        #region Eventos KeyPress y Leave del txtBarrio

        private void txtBarrio_Leave(object sender, EventArgs e)
        {
            this.txtBarrio.Text = txtBarrio.Text.ToUpper();
        }

        #endregion

        #region Eventos KeyPress y Leave del txtTelefono

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                txtCelular.Focus();
            }
        }

        #endregion

        #region Eventos KeyPress y Leave del txtCelular

        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                txtMail.Focus();
            }
        }

        #endregion

        #region Eventos KeyPress y Leave del txtFax

        private void txtFax_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 13 || e.KeyChar == 9)
            //{
            //    txtMail.Focus();
            //}
        }

        #endregion

        #region Eventos KeyPress y Leave del txtMail

        private void txtMail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                txtWeb.Focus();
            }
        }

        #endregion

        #region Eventos KeyPress y Leave del txtWeb

        private void txtWeb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                dtpAlta.Focus();
            }
        }

        #endregion

        #region Eventos KeyPress y Leave del txtObservaciones

        private void txtObservaciones_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 9)
            {
                btnAceptar.Focus();
            }
        }

        private void txtObservaciones_Leave(object sender, EventArgs e)
        {
            this.txtObservaciones.Text = txtObservaciones.Text.ToUpper();
            this.AcceptButton = this.btnAceptar;
        }

        #endregion

        #endregion

        #region Eventos de los combos del formulario

        #region Eventos del Combo cboCondicion

        private void cboCondicion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (myBanderaCombos == true)
            {
                txtCuit.Focus();
                myBanderaCombos = false;
            }
        }

        private void cboCondicion_Click(object sender, EventArgs e)
        {
            myBanderaCombos = true;
        }

        #endregion

        #region Eventos del combo cboLocalidad

        private void cboLocalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (myBanderaCombos == true)
            {
                txtCP.Focus();
                myBanderaCombos = false;
            }
        }

        private void cboLocalidad_Click(object sender, EventArgs e)
        {
            myBanderaCombos = true;
        }

        #endregion

        #region Eventos del combo cboTipoCliente

        private void cboTipoCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (myBanderaCombos == true)
            {
                txtDireccion.Focus();
                myBanderaCombos = false;
            }

            if (this.myEstado == "A" || this.myEstado == "M")
            {
                if (cboTipoCliente.SelectedIndex != -1)
                {
                    if (Convert.ToInt32(cboTipoCliente.SelectedValue.ToString()) == 30)
                    {
                        this.chkCRsal.TabStop = true;
                        this.chkCRsal.Enabled = true;
                    }
                    else
                    {
                        this.chkCRsal.Checked = false;
                        this.chkCRsal.TabStop = false;
                        this.chkCRsal.Enabled = false;
                    }
                }
            }
           
        }

        private void cboTipoCliente_Click(object sender, EventArgs e)
        {
            myBanderaCombos = true;
        }

        #endregion

        #endregion

        #region Evento CloseUp de los Dtp del form

        private void dtpAlta_CloseUp(object sender, EventArgs e)
        {
            dtpFechaNacimiento.Focus();
        }

        private void dtpFechaNacimiento_CloseUp(object sender, EventArgs e)
        {
            txtObservaciones.Focus();
        }

        #endregion

        #region Evento TextChanged txtCodigoBs

        private void txtCodigoBs_TextChanged(object sender, EventArgs e)
        {
            if (!(this.txtCodigoBs.Text==""))
            {
                this.CargarGrillaBusqueda(this.txtCodigoBs.Text, "Codigo");
            }
        }

        #endregion

        #region Evento TextChanged txtRazonSocialBs

        private void txtRazonSocialBs_TextChanged(object sender, EventArgs e)
        {
            if (!(this.txtRazonSocialBs.Text == ""))
            {
                this.CargarGrillaBusqueda(this.txtRazonSocialBs.Text, "RazonSocial");
            }
        }

        #endregion

        #endregion
        
        #region Métodos del formulario

        #region Metodo: CargarGrillaBusqueda

        private void CargarGrillaBusqueda(string Buscar, string Campo)
        {
            // Variable para la cadena SQL
            string myCadena = "";
            if (Buscar == "")
            {
                // Cadena SQL 
                myCadena = "SELECT * from Vista_Clientes WHERE Activo = 1"; ;
                //.F.
                bSearch = false;
            }
            else
            {
                switch(Campo)
                {
                    case "RazonSocial":
                        // Cadena SQL 
                        myCadena = "select * from Vista_Clientes where " + Campo + " like '" + Buscar + "%' AND Activo = 1 order by " + Campo;
                         break;
                    case "Mail":
                         // Cadena SQL 
                         myCadena = "select * from Vista_Clientes where " + Campo + " like '" + Buscar + "%' AND Activo = 1 order by " + Campo;
                         break;
                    case "Codigo":
                         // Cadena SQL 
                         myCadena = "select * from Vista_Clientes where " + Campo + " = " + Buscar + " AND Activo = 1 order by " + Campo;
                         break;
                    case "IdProvincia":
                         // Cadena SQL 
                         myCadena = "select * from Vista_Clientes where " + Campo + " = " + Buscar + " AND Activo = 1 order by RazonSocial";
                         break;

                }
                //.T.
                bSearch = true;
            }

            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            mDtTable = clsDataBD.GetSql(myCadena);
            // Evito que el dgv genere columnas automáticas
            this.dgvClientes.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            this.dgvClientes.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = this.dgvClientes.Rows.Count;
            // Posiciono la grilla en la última fila, si hay
            if (Filas > 0)
            {
                //Posicionamiento grilla
                int r = this.dgvClientes.CurrentCell.RowIndex;
                int c = this.dgvClientes.CurrentCell.ColumnIndex;
                this.dgvClientes.CurrentCell = this.dgvClientes.Rows[r].Cells[c];
 
            }

            //Mostrar datos  
            this.TraerDatosGrilla();
        }

        #endregion

        #region Método que llena la grilla DGVClientes del formulario

        private void CargarGrilla()
        {
            string myCadena = "";

            //SI HAY CODIGO DE CLIENTE Y VIENE DE PEDIDO O PRESUPUESTO
            if (IdCliente != 0 && this.bABMPresu == true)
            {
                // LLeno la grilla con los datos de la Vista de Clientes
                myCadena = "SELECT * from Vista_Clientes WHERE Codigo = " + IdCliente + " AND Activo = 1 ORDER BY RazonSocial"; 
            }
            else
            {
                // LLeno la grilla con los datos de la Vista de Clientes
                myCadena = "SELECT * from Vista_Clientes WHERE Activo = 1 order by RazonSocial";
            }
 
            // Evito que el dgvUsuarios genere columnas automáticas
            dgvClientes.AutoGenerateColumns = false;
            // Creo un nuevo DataTable
            DataTable mDtTable = new DataTable();
            // Le asigno al nuevo DataTable los datos de la consulta SQL
            mDtTable = clsDataBD.GetSql(myCadena);
            // Asigno el source de la grilla
            dgvClientes.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvClientes.Rows.Count;
            // Posiciono la grilla en la última fila, si hay
            if (Filas > 0)
            {
                //Posicionamiento grilla
                int r = dgvClientes.CurrentCell.RowIndex;
                int c = dgvClientes.CurrentCell.ColumnIndex;
                dgvClientes.CurrentCell = dgvClientes.Rows[r].Cells[c];
                //Mostrar datos  
                TraerDatosGrilla();
            }
        }

        #endregion

        #region Método que llena los combos del formulario

        private void LlenarCombos()
        {
            // Cargo el combo de las Localidades
            //clsDataBD.CargarCombo(cboLocalidad, "Localidades", "Localidad", "IdLocalidad");
            // Dejo Vacío lo que me muestra el combo 
            //cboLocalidad.SelectedIndex = -1;

            // Cargo el combo de los Tipos de clientes
            clsDataBD.CargarCombo(cboTipoCliente, "TiposClientes", "TipoCliente", "IdTipoCliente");
            // Dejo Vacío lo que me muestra el combo 
            cboTipoCliente.SelectedIndex = -1;

            // Cargo el combo de las Provincias
            clsDataBD.CargarCombo(cboProvincia, "Provincias", "Provincia", "IdProvincia");
            // Dejo Vacío lo que me muestra el combo 
            cboProvincia.SelectedIndex = -1;

            //Transporte
            clsDataBD.CargarCombo(this.cboTransporte, "Transportes", "RazonSocial", "IdTransporte");
            cboTransporte.SelectedIndex = -1;
            //Condiciones IVA
            clsDataBD.CargarCombo(this.cboIva, "CondicionesIva", "CondicionIva", "IdCondicionIva");
            cboTransporte.SelectedIndex = -1;
            //Transporte
            clsDataBD.CargarCombo(this.cboCompra, "CondicionesCompra", "CondicionCompra", "IdCondicionCompra");
            cboTransporte.SelectedIndex = -1;
        }

        #endregion

        #region Método para activar los botones del formulario
        //--------------------------------------------------------------
        //ACTIVAR BOTONES  
        //SEGUN EL ESTADO (A, M, C) - MUESTRA U OCULTA BOTONES
        //--------------------------------------------------------------
        private void ActivarBotones()
        {
            // Verifico que quiero hacer en el formulario
            switch (this.myEstado)
            {
                // Para una Alta, búsqueda y Modificación
                case "A":
                    this.btnAgregar.TabStop = false;
                    this.btnAgregar.Visible = false;
                    this.btnModificar.TabStop = false;
                    this.btnModificar.Visible = false;
                    this.btnAceptar.TabStop = true;
                    this.btnAceptar.Visible = true;
                    this.btnCancelar.TabStop = true;
                    this.btnCancelar.Visible = true;
                    this.btnSalir.TabStop = false;
                    this.btnSalir.Visible = false;
                    this.btnBorrar.TabStop = false;
                    this.btnBorrar.Visible = false;
                    this.btnBuscar.TabStop = false;
                    this.btnBuscar.Visible = false;
                    this.btnImprimir.TabStop = false;
                    this.btnImprimir.Visible = false;
                    this.btnPedido.TabStop = false;
                    this.btnPedido.Visible = false;
                    this.btnExcel.TabStop = false;
                    this.btnExcel.Visible = false;
                    break;
                case "B":
                    this.btnAgregar.TabStop = false;
                    this.btnAgregar.Visible = false;
                    this.btnModificar.TabStop = false;
                    this.btnModificar.Visible = false;
                    this.btnAceptar.TabStop = true;
                    this.btnAceptar.Visible = true;
                    this.btnCancelar.TabStop = true;
                    this.btnCancelar.Visible = true;
                    this.btnSalir.TabStop = false;
                    this.btnSalir.Visible = false;
                    this.btnBorrar.TabStop = false;
                    this.btnBorrar.Visible = false;
                    this.btnBuscar.TabStop = false;
                    this.btnBuscar.Visible = false;
                    this.btnImprimir.TabStop = false;
                    this.btnImprimir.Visible = false;
                    this.btnPedido.TabStop = false;
                    this.btnPedido.Visible = false;
                    this.btnExcel.TabStop = false;
                    this.btnExcel.Visible = false;
                    break;
                case "M":
                    this.btnAgregar.TabStop = false;
                    this.btnAgregar.Visible = false;
                    this.btnModificar.TabStop = false;
                    this.btnModificar.Visible = false;
                    this.btnAceptar.TabStop = true;
                    this.btnAceptar.Visible = true;
                    this.btnCancelar.TabStop = true;
                    this.btnCancelar.Visible = true;
                    this.btnSalir.TabStop = false;
                    this.btnSalir.Visible = false;
                    this.btnBorrar.TabStop = false;
                    this.btnBorrar.Visible = false;
                    this.btnBuscar.TabStop = false;
                    this.btnBuscar.Visible = false;
                    this.btnImprimir.TabStop = false;
                    this.btnImprimir.Visible = false;
                    this.btnPedido.TabStop = false;
                    this.btnPedido.Visible = false;
                    this.btnExcel.TabStop = false;
                    this.btnExcel.Visible = false;
                    break;
                // Modo de espera
                case "C":
                    this.btnAgregar.TabStop = true && (!(bABMPresu));
                    this.btnAgregar.Visible = true && (!(bABMPresu));

                    this.btnModificar.TabStop = true && (dgvClientes.RowCount != 0);
                    this.btnModificar.Visible = true && (dgvClientes.RowCount != 0);
                    //Si viene de ABM Presu, habilitar edicion unicamente
                    if (bABMPresu)
                    { this.btnModificar.Location = new System.Drawing.Point(12, 8);}
                    else
                    { this.btnModificar.Location = new System.Drawing.Point(63, 8);}

                    this.btnAceptar.TabStop = false;
                    this.btnAceptar.Visible = false;
                    this.btnCancelar.TabStop = false;
                    this.btnCancelar.Visible = false;
                    this.btnSalir.TabStop = true;
                    this.btnSalir.Visible = true;
                    this.btnExcel.TabStop = true && (dgvClientes.RowCount != 0) && (clsGlobales.UsuarioLogueado.Nivel >= 5);
                    this.btnExcel.Visible = true && (dgvClientes.RowCount != 0) && (clsGlobales.UsuarioLogueado.Nivel >= 5);

                    // Si el nivel del usuario es el 5, habilito el botón borrar
                    if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja)
                    {
                        this.btnBorrar.TabStop = true && (dgvClientes.RowCount != 0) && (!(bABMPresu));
                        this.btnBorrar.Visible = true && (dgvClientes.RowCount != 0) && (!(bABMPresu));
                    }
                    else
                    {
                        this.btnBorrar.TabStop = false;
                        this.btnBorrar.Visible = false;
                    }
                    this.btnImprimir.TabStop = true && (dgvClientes.RowCount != 0) && (!(bABMPresu));
                    this.btnImprimir.Visible = true && (dgvClientes.RowCount != 0) && (!(bABMPresu));
                    this.btnBuscar.TabStop = true && (dgvClientes.RowCount != 0) && (!(bABMPresu));
                    this.btnBuscar.Visible = true && (dgvClientes.RowCount != 0) && (!(bABMPresu));

                    //Ir a Pedidos

                    this.btnPedido.TabStop = bShowPed && (clsGlobales.UsuarioLogueado.Nivel >=5) && (dgvClientes.RowCount != 0) && (!(bABMPresu));
                    this.btnPedido.Enabled = bShowPed && (clsGlobales.UsuarioLogueado.Nivel >= 5) && (dgvClientes.RowCount != 0) && (!(bABMPresu));
                    this.btnPedido.Visible = bShowPed && (clsGlobales.UsuarioLogueado.Nivel >= 5) && (dgvClientes.RowCount != 0) && (!(bABMPresu));
             
                    
                    break;
            }
        }
        #endregion

        #region Método para habilitar los Controles del formulario
        //--------------------------------------------------------------
        //SEGUN EL ESTADO (A, ALTA, M, MODIFICACION O C, EN ESPERA.
        //HABILITA O INHABILITA LOS CONTROLES DEL FORMULARIO
        //(PUEDE SER PROPIEDAD ENABLED (C# / VB) O READONLY (C#)
        //--------------------------------------------------------------
        private void HabilitarControles()
        {
            //Usamos un switch para evaluar en que estado estamos 
            //A = Alta, M = Modificacion, "C" = En espera
            switch (this.myEstado)
            {
                // Para una Alta y Modificación
                case "A":
                    this.txtCodigo.TabStop = false;
                    this.txtCodigo.Enabled = false;
                    this.txtRazonSocial.TabStop = true;
                    this.txtRazonSocial.Enabled = true;
                    this.txtCuit.TabStop = true;
                    this.txtCuit.Enabled = true;
                    this.txtDireccion.TabStop = true;
                    this.txtDireccion.Enabled = true;
                    this.txtBarrio.TabStop = true;
                    this.txtBarrio.Enabled = true;
                    this.txtCP.TabStop = false;
                    this.txtCP.Enabled = false;
                    this.txtTelefono.TabStop = true;
                    this.txtTelefono.Enabled = true;
                    this.txtCelular.TabStop = true;
                    this.txtCelular.Enabled = true;
                    this.txtFax.TabStop = false;
                    this.txtFax.Enabled = false;
                    this.txtMail.TabStop = true;
                    this.txtMail.Enabled = true;
                    this.txtWeb.TabStop = true;
                    this.txtWeb.Enabled = true;
                    this.txtObservaciones.TabStop = true;
                    this.txtObservaciones.Enabled = true;
                    
                    this.cboTipoCliente.TabStop = true;
                    this.cboTipoCliente.Enabled = true;

                    this.cboIva.TabStop = true;
                    this.cboIva.Enabled = true;

                    this.cboTransporte.TabStop = true;
                    this.cboTransporte.Enabled = true;
                    
                    this.cboCompra.TabStop = true;
                    this.cboCompra.Enabled = true;
                    
                    this.btnLoc.TabStop = true;
                    this.btnLoc.Enabled = true;

                    this.txtLocalidad.TabStop = false; 
                    this.txtLocalidad.Enabled = false;
                    
                    this.txtProvincia.TabStop = false;
                    this.txtProvincia.Enabled = false;

                    this.txtSaldo.TabStop = false;
                    this.txtSaldo.Enabled = false;

                    this.chkCRsal.TabStop = false;
                    this.chkCRsal.Enabled = false;

                    this.dtpAlta.TabStop = true;
                    this.dtpAlta.Enabled = true;
                    this.dtpFechaNacimiento.TabStop = true;
                    this.dtpFechaNacimiento.Enabled = true;

                    this.dgvClientes.TabStop = false;
                    this.dgvClientes.Enabled = false;

                    this.gpbBusquedas.Visible = false;



                    break;
                case "M":
                    this.txtCodigo.TabStop = false;
                    this.txtCodigo.Enabled = false;
                    this.txtRazonSocial.TabStop = true;
                    this.txtRazonSocial.Enabled = true;
                    this.txtCuit.TabStop = true;
                    this.txtCuit.Enabled = true;
                    this.txtDireccion.TabStop = true;
                    this.txtDireccion.Enabled = true;
                    this.txtBarrio.TabStop = true;
                    this.txtBarrio.Enabled = true;
                    this.txtCP.TabStop = false;
                    this.txtCP.Enabled = false;
                    this.txtTelefono.TabStop = true;
                    this.txtTelefono.Enabled = true;
                    this.txtCelular.TabStop = true;
                    this.txtCelular.Enabled = true;
                    this.txtFax.TabStop = false;
                    this.txtFax.Enabled = false;
                    this.txtMail.TabStop = true;
                    this.txtMail.Enabled = true;
                    this.txtWeb.TabStop = true;
                    this.txtWeb.Enabled = true;
                    this.txtObservaciones.TabStop = true;
                    this.txtObservaciones.Enabled = true;
                    
                    this.cboTipoCliente.TabStop = true;
                    this.cboTipoCliente.Enabled = true;

                    this.cboIva.TabStop = true;
                    this.cboIva.Enabled = true;

                    this.cboTransporte.TabStop = true;
                    this.cboTransporte.Enabled = true;

                    this.cboCompra.TabStop = true;
                    this.cboCompra.Enabled = true;

                    this.btnLoc.TabStop = true;
                    this.btnLoc.Enabled = true;

                    this.txtLocalidad.TabStop = false; 
                    this.txtLocalidad.Enabled = false;
                    
                    this.txtProvincia.TabStop = false;
                    this.txtProvincia.Enabled = false;

                    this.txtSaldo.TabStop = false;
                    this.txtSaldo.Enabled = false;

                    if (Convert.ToInt32(cboTipoCliente.SelectedValue.ToString()) == 30)
                    {
                        this.chkCRsal.TabStop = true;
                        this.chkCRsal.Enabled = true;
                    }
                    else
                    {
                        this.chkCRsal.TabStop = false;
                        this.chkCRsal.Enabled = false;
                    }
                    this.dtpAlta.TabStop = false;
                    this.dtpAlta.Enabled = false;
                    this.dtpFechaNacimiento.TabStop = true;
                    this.dtpFechaNacimiento.Enabled = true;

                    this.dgvClientes.TabStop = false;
                    this.dgvClientes.Enabled = false;

                    this.gpbBusquedas.Visible = false;

                    break;
                // Para una Búsqueda
                case "B":
                    this.txtCodigo.TabStop = false;
                    this.txtCodigo.Enabled = false;
                    this.txtRazonSocial.TabStop = false;
                    this.txtRazonSocial.Enabled = false;
                    this.txtCuit.TabStop = false;
                    this.txtCuit.Enabled = false;
                    this.txtDireccion.TabStop = false;
                    this.txtDireccion.Enabled = false;
                    this.txtBarrio.TabStop = false;
                    this.txtBarrio.Enabled = false;
                    this.txtCP.TabStop = false;
                    this.txtCP.Enabled = false;
                    this.txtTelefono.TabStop = false;
                    this.txtTelefono.Enabled = false;
                    this.txtCelular.TabStop = false;
                    this.txtCelular.Enabled = false;
                    this.txtFax.TabStop = false;
                    this.txtFax.Enabled = false;
                    this.txtMail.TabStop = false;
                    this.txtMail.Enabled = false;
                    this.txtWeb.TabStop = false;
                    this.txtWeb.Enabled = false;
                    this.txtObservaciones.TabStop = false;
                    this.txtObservaciones.Enabled = false;

                    this.txtSaldo.TabStop = false;
                    this.txtSaldo.Enabled = false;

                    this.cboTipoCliente.TabStop = false;
                    this.cboTipoCliente.Enabled = false;

                    this.cboIva.TabStop = false;
                    this.cboIva.Enabled = false;

                    this.cboTransporte.TabStop = false;
                    this.cboTransporte.Enabled = false;
                    
                    this.cboCompra.TabStop = false;
                    this.cboCompra.Enabled = false;

                    this.btnLoc.TabStop = false;
                    this.btnLoc.Enabled = false;

                    this.txtLocalidad.TabStop = false; 
                    this.txtLocalidad.Enabled = false;
                    
                    this.txtProvincia.TabStop = false;
                    this.txtProvincia.Enabled = false;

                    this.chkCRsal.TabStop = false;
                    this.chkCRsal.Enabled = false;

                    this.dtpAlta.TabStop = false;
                    this.dtpAlta.Enabled = false;
                    this.dtpFechaNacimiento.TabStop = false;
                    this.dtpFechaNacimiento.Enabled = false;

                    this.dgvClientes.TabStop = true && (dgvClientes.RowCount > 0);
                    this.dgvClientes.Enabled = true && (dgvClientes.RowCount > 0);

                    this.gpbBusquedas.Visible = true;

                    return;
                // Modo de espera
                case "C":
                    this.txtCodigo.TabStop = false;
                    this.txtCodigo.Enabled = false;
                    this.txtRazonSocial.TabStop = false;
                    this.txtRazonSocial.Enabled = false;
                    this.txtCuit.TabStop = false;
                    this.txtCuit.Enabled = false;
                    this.txtDireccion.TabStop = false;
                    this.txtDireccion.Enabled = false;
                    this.txtBarrio.TabStop = false;
                    this.txtBarrio.Enabled = false;
                    this.txtCP.TabStop = false;
                    this.txtCP.Enabled = false;
                    this.txtTelefono.TabStop = false;
                    this.txtTelefono.Enabled = false;
                    this.txtCelular.TabStop = false;
                    this.txtCelular.Enabled = false;
                    this.txtFax.TabStop = false;
                    this.txtFax.Enabled = false;
                    this.txtMail.TabStop = false;
                    this.txtMail.Enabled = false;
                    this.txtWeb.TabStop = false;
                    this.txtWeb.Enabled = false;
                    this.txtObservaciones.TabStop = false;
                    this.txtObservaciones.Enabled = false;

                    this.cboTipoCliente.TabStop = false;
                    this.cboTipoCliente.Enabled = false;

                    this.btnLoc.TabStop = false;
                    this.btnLoc.Enabled = false;

                    this.txtSaldo.TabStop = false;
                    this.txtSaldo.Enabled = false;

                    this.txtLocalidad.TabStop = false; 
                    this.txtLocalidad.Enabled = false;
                    
                    this.txtProvincia.TabStop = false;
                    this.txtProvincia.Enabled = false;
                    
                    this.cboIva.TabStop = false;
                    this.cboIva.Enabled = false;

                    this.cboTransporte.TabStop = false;
                    this.cboTransporte.Enabled = false;

                    this.cboCompra.TabStop = false;
                    this.cboCompra.Enabled = false;

                    this.chkCRsal.TabStop = false;
                    this.chkCRsal.Enabled = false;

                    this.dtpAlta.TabStop = false;
                    this.dtpAlta.Enabled = false;
                    this.dtpFechaNacimiento.TabStop = false;
                    this.dtpFechaNacimiento.Enabled = false;

                    this.dgvClientes.TabStop = true && (dgvClientes.RowCount > 0);
                    this.dgvClientes.Enabled = true && (dgvClientes.RowCount > 0);

                    this.gpbBusquedas.Visible = false;

                    break;
            }
        }
        #endregion

        #region Método para limpiar los controles del formulario
        //LIMPIA LA PROPIEDAD TEXT DE LOS CONTROLES DEL FORMULARIO. N.
        private void LimpiarControlesForm()
        {
            // Todos los TextBox Vacíos
            this.txtRazonSocial.Text = "";
            this.txtCuit.Text = "";
            this.txtDireccion.Text = "";
            this.txtBarrio.Text = "";
            this.txtCP.Text = "";
            this.txtTelefono.Text = "";
            this.txtCelular.Text = "";
            this.txtFax.Text = "";
            this.txtMail.Text = "";
            this.txtWeb.Text = "";
            this.txtObservaciones.Text = "";
            // Todos los Combos Vacíos
            this.cboTipoCliente.SelectedIndex = -1;
            this.txtLocalidad.Text = "";
            this.txtProvincia.Text = "";
            // Todos los dtpPicker a la fecha de hoy
            this.dtpAlta.Value = DateTime.Now;
            this.dtpFechaNacimiento.Value = DateTime.Now;
        }
        #endregion

        #region Método que trae los datos de la grilla a los componentes deo formulario

        private void TraerDatosGrilla()
        {
            //Check if...
            if (dgvClientes.Rows.Count == 0)
            {
                this.LimpiarControlesForm();
                return;
            }

            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvClientes.CurrentRow;
            // Cargo los datos de la fila seleccionada en sus correspondientes textbox. G.
            this.txtCodigo.Text = row.Cells["Codigo"].Value.ToString();
            this.txtRazonSocial.Text = row.Cells["RazonSocial"].Value.ToString();
            this.txtCuit.Text = row.Cells["Cuit"].Value.ToString();
            this.txtDireccion.Text = row.Cells["Direccion"].Value.ToString();
            this.txtBarrio.Text = row.Cells["Barrio"].Value.ToString();
            this.txtCP.Text = row.Cells["CP"].Value.ToString();
            this.txtTelefono.Text = row.Cells["Telefono"].Value.ToString();
            this.txtCelular.Text = row.Cells["Celular"].Value.ToString();
            this.txtFax.Text = row.Cells["Fax"].Value.ToString();
            this.txtMail.Text = row.Cells["Mail"].Value.ToString();
            this.txtWeb.Text = row.Cells["Web"].Value.ToString();
            this.txtObservaciones.Text = row.Cells["Observaciones"].Value.ToString();
            this.txtSaldo.Text = Convert.ToDouble(row.Cells["Saldo"].Value.ToString()).ToString("#0.00");

            // Cargo los datos de la fila en los correspondientes ComboBox            
            this.cboTipoCliente.SelectedValue = Convert.ToInt32(row.Cells["IdTipo"].Value);            
            this.cboTransporte.SelectedValue = Convert.ToInt32(row.Cells["IdTransporte"].Value);
            this.cboCompra.SelectedValue = Convert.ToInt32(row.Cells["IdCondicionCompra"].Value);
           
            // this.cboProvincias.SelectedValue = Convert.ToInt32(row.Cells["IdProvincia"].Value);

            this.cboIva.SelectedValue = Convert.ToInt32(row.Cells["IdCondicionIva"].Value);

            if (Convert.ToInt32(row.Cells["CRsal"].Value)==1)
            { this.chkCRsal.Checked = true; }
            else { this.chkCRsal.Checked = false;}

            //Ocultar Combo Localidad y mostrar localidad
    /*        if (Convert.ToInt32(cboProvincias.SelectedValue) > 0)
            {
                //Cargar las localidades de la provincia
                setComboLocalidad();
                //Mostrarla
                this.cboLocalidad.SelectedValue = Convert.ToInt32(row.Cells["IdLocalidad"].Value);
            }*/

            txtProvincia.Text = row.Cells["Provincia"].Value.ToString();
            txtLocalidad.Text = row.Cells["Localidad"].Value.ToString();

            // Cargo los datos de la fila (fechas) a los DTP
            this.dtpAlta.Value = Convert.ToDateTime(row.Cells["Alta"].Value);
            this.dtpFechaNacimiento.Value = Convert.ToDateTime(row.Cells["Nacimiento"].Value);
        }

        #endregion

        #region Método que valida los datos del ingresados al formulario

        private void ValidarCliente()
        {
            //VALIDAR EL OBJETO Y VER SI HAY ERRORES. N.
            // Todo esto se reemplaza por la ventana de errores.

            string[] cErrores = NuevoCliente.cValidaCLiente();
            if (String.IsNullOrEmpty(cErrores[0]) == false)
            {
                MessageBox.Show(cErrores[0], "ERROR!", MessageBoxButtons.OK);
                this.txtCodigo.Focus();
                return;
            }
            if (String.IsNullOrEmpty(cErrores[1]) == false)
            {
                MessageBox.Show(cErrores[1], "ERROR!", MessageBoxButtons.OK);
                this.txtRazonSocial.Focus();
                return;
            }
            if (String.IsNullOrEmpty(cErrores[2]) == false)
            {
                MessageBox.Show(cErrores[2], "ERROR!", MessageBoxButtons.OK);
                this.cboTipoCliente.Focus();
                return;
            }
            if (String.IsNullOrEmpty(cErrores[3]) == false)
            {
                MessageBox.Show(cErrores[3], "ERROR!", MessageBoxButtons.OK);
                this.txtCuit.Focus();
                return;
            }
            if (String.IsNullOrEmpty(cErrores[4]) == false)
            {
                MessageBox.Show(cErrores[4], "ERROR!", MessageBoxButtons.OK);
                this.cboTipoCliente.Focus();
                return;
            }
            // Agregado por solicitud de Mariano 09/03/2016
            if (String.IsNullOrEmpty(cErrores[5]) == false)
            {
                MessageBox.Show(cErrores[5], "ERROR!", MessageBoxButtons.OK);
                this.txtTelefono.Focus();
                return;
            }
        }

        #endregion

        #region CargarToolTips

        private void CargarToolTips()
        {
            toolTip1.SetToolTip(this.btnAgregar, "Agregar");
            toolTip2.SetToolTip(this.btnModificar, "Modificar");
            toolTip3.SetToolTip(this.btnBorrar, "Borrar");
            toolTip4.SetToolTip(this.btnImprimir, "Imprimir");
            toolTip5.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip6.SetToolTip(this.btnCancelar, "Cancelar");
            toolTip7.SetToolTip(this.btnSalir, "Salir");
            toolTip8.SetToolTip(this.btnBuscar, "Buscar");
            toolTip9.SetToolTip(this.btnLoc, "Buscar...");
            toolTip10.SetToolTip(this.btnPedido, "Pedido!");
            toolTip11.SetToolTip(this.btnExcel, "Exportar (Excel)!");
            toolTip11.SetToolTip(this.btnInterrupt, "Cancelar Exportación!");
        }

        #endregion

        //#region Metodo: setComboLocalidad()
        ///***********************************
        // * Metodo       : setComboLocalidad
        // * Proposito    : Cargar el combo de localidades 
        // * Retorna      : Nada
        // * Parametros   : Ninguno
        // * Autor        : N.
        // * ********************************/
        //private void setComboLocalidad()
        //{
        //    //Clean Combobox
        //    cboLocalidad.DataSource = null;
        //    cboLocalidad.DataBindings.Clear();

        //    // Cargo el combo de las Localidades N.
        //    int iProvSel = Convert.ToInt32(cboProvincias.SelectedValue);
        //    clsDataBD.CargarComboStoreProcedure(cboLocalidad, "CargarLocalidades", iProvSel, "Localidad", "IdLocalidad");

        //    //Establecer el valor                       
        //    DataGridViewRow row = this.dgvClientes.CurrentRow;
        //    cboLocalidad.SelectedValue = Convert.ToInt32(row.Cells["IdLocalidad"].Value.ToString());
        //}
        //#endregion

        #region Metodo SelectedValueChanged de la grilla

        private void cboProvincias_SelectedValueChanged(object sender, EventArgs e)
        {
            //Solo si estoy editando o modificando N.
        /*    if (!(this.myEstado == "C"))
            {
                if (cboProvincias.SelectedValue == null) { return; }

                //Clean Combobox
                cboLocalidad.DataSource = null;
                cboLocalidad.DataBindings.Clear();

                // Cargo el combo de las Localidades N.
                string strSQL = " IdProvincia = " + cboProvincias.SelectedValue;
                clsDataBD.CargarCombo(cboLocalidad, "Localidades", "Localidad", "IdLocalidad", strSQL);

                // Establezco el combo localidad a la primera opcion N.
                cboLocalidad.TabStop = true;
                cboLocalidad.Enabled = true;
                //No mostrar dato alguno
                cboLocalidad.SelectedIndex = -1;
            }
            else
            {

                //Clean Combobox
                cboLocalidad.DataSource = null;
                cboLocalidad.DataBindings.Clear();
                //Desactivar
                cboLocalidad.TabStop = false;
                cboLocalidad.Enabled = false;
                //No mostrar dato alguno
                cboLocalidad.SelectedIndex = -1;
            }*/
        }

        #endregion

        #region Método que devuelve el foco a la fila desde donde se presionó algún botón

        private void PosicionarFocoFila()
        {
            // Devuelvo el foco a la fila de la grilla desde donde se llamó
            this.dgvClientes.CurrentCell = dgvClientes[0, this.indexFila];

            // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
            EventArgs ea = new EventArgs();
            this.dgvClientes_SelectionChanged(this.dgvClientes, ea);
        }

        #endregion

        private void txtCodigoBs_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Solo enteros
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }     
        }

        #endregion

        private void btnLoc_Click(object sender, EventArgs e)
        {
            frmLocalidades myLocalidad = new frmLocalidades();
            myLocalidad.ShowDialog();

            string mySQL = "exec RetornarProvLoc " + clsGlobales.IdProv + "," + clsGlobales.IdLoc;

            DataTable myData = clsDataBD.GetSql(mySQL);

            foreach (DataRow row in myData.Rows)
            {
                this.txtProvincia.Text = row["Provincia"].ToString();
                this.txtLocalidad.Text = row["Localidad"].ToString();
            }
         
         //Establecer localidad y CP
            txtCP.Text = clsGlobales.CP;

        }

        private void txtSaldo_KeyPress(object sender, KeyPressEventArgs e)
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

        private void cboProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (myEstado=="B")
            {
                if (!(this.cboProvincia.SelectedIndex == -1))
                {
                    
                    this.CargarGrillaBusqueda(cboProvincia.SelectedValue.ToString(), "IdProvincia");
                }
            }
        }

        private void txtCodigoBs_Click(object sender, EventArgs e)
        {
            txtRazonSocialBs.Text = "";
            cboProvincia.SelectedIndex = -1;
        }

        private void txtRazonSocialBs_Click(object sender, EventArgs e)
        {
            txtCodigoBs.Text = "";
            cboProvincia.SelectedIndex = -1;
        }

        private void cboProvincia_Click(object sender, EventArgs e)
        {
            txtRazonSocialBs.Text = "";
            txtCodigoBs.Text = "";
        }

        private void txtMailBs_TextChanged(object sender, EventArgs e)
        {
            if (!(this.txtMailBs.Text == ""))
            {
                this.CargarGrillaBusqueda(this.txtMailBs.Text, "Mail");
            }
        }

        private void txtObservaciones_Enter(object sender, EventArgs e)
        {
            this.AcceptButton = null;

        }

        private void btnPedido_Click(object sender, EventArgs e)
        {
            //Llamar al Formulario de Alta
            frmABM_PedPresu_Excel myForm = new frmABM_PedPresu_Excel("A", 0, 0, false, this.dgvClientes);
            myForm.ShowDialog();

        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            //Directorio Destino 
               string sFolder = clsGlobales.sDestinoFs;

            //CAMBIAR PUNTERO MOUSE
                Cursor.Current = Cursors.WaitCursor;

            // Confirma salir de la aplicación ?
                DialogResult dlResult = MessageBox.Show("Desea Exportar a Excel la base de Clientes?. El archivo se guardará en Mis Documentos.", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Si confirma salir de la aplicación....
                if (dlResult == DialogResult.Yes)
                {

                    //Panel
                    this.btnPanel.Enabled = false;
                    this.dgvClientes.Enabled = false;
                    this.grpExportacion.Visible = true;
                    this.pBar.Value = 0;
                    this.lblPorciento.Text = "% 0";
                    this.lblActual.Text = "0";

                    //Exportar a Excel
                    clsGlobales.cFormato.ExportToExcelCli(sFolder, this.dgvClientes, pBar, lblPorciento, lblActual);

                    if (!(clsGlobales.bInterrupt))
                    {
                        //Mensaje
                        MessageBox.Show("El Proceso de exportación ha finalizado!", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //Reset Panel
                    this.btnPanel.Enabled = true;
                    this.dgvClientes.Enabled = true;
                    this.pBar.Value = 0;
                    this.lblPorciento.Text = "% 0";
                    this.grpExportacion.Visible = false;
                    clsGlobales.bInterrupt = false;
                }
        }

        private void btnInterrupt_Click(object sender, EventArgs e)
        {
            clsGlobales.bInterrupt = true;
        }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Prama.Formularios.Ventas;
using Prama.Formularios.Auxiliares;
using Prama.Formularios.Stock;
using System.Diagnostics;
using Prama.Formularios.Compras;
using Prama.Formularios.Informes;
using Prama.Formularios.Articulos;
using System.Data.SqlClient;
using Prama.Formularios.Configuracion;
using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Management;

namespace Prama
{
    public partial class frmMenuPrincipal : Form
    {
        // Variable que almacena la cantidad de lineas que tiene el archivo plano
        int iCantLineasPlano = 0;

        #region Constructor

        public frmMenuPrincipal()
        {
            InitializeComponent();
            //Asigno fecha 2016-09-09
            lblFecha.Text += clsValida.ConvertirFecha(DateTime.Today);
            lblHora.Text += DateTime.Now.ToLongTimeString();
            //Pto Venta

        }

        #endregion

        #region Proveedores Menu Item

        private void proveedoresToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmProveedores myForm = new frmProveedores();
            myForm.ShowDialog();
        }

        #endregion

        #region Evento Menu Clientes

        private void clientesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmClientesABM myForm = new frmClientesABM(0,false,false,true);
            myForm.ShowDialog();
        }

        #endregion
        
        #region SalirToolStrip Menu Item

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Confirma salir de la aplicación ?
            DialogResult dlResult = MessageBox.Show("Desea Salir del Sistema?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Si confirma salir de la aplicación....
            if (dlResult == DialogResult.Yes)
            {
                // Cierro la aplicación. G.
                clsGlobales.bFlag = false;
                Application.Exit();
            }
        }

        #endregion

        #region Eventos FormClosed y FormClosing

        private void frmMenuPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!(clsGlobales.bFlag == false))
            {
                // Confirma salir de la aplicación ? N. 04-10-2015
                DialogResult dlResult = MessageBox.Show("Desea Salir del Sistema?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Si confirma salir de la aplicación....
                if (!(dlResult == DialogResult.Yes))
                {
                    e.Cancel = true;
                }
            }
        }

        private void frmMenuPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Cierro la aplicación. G.
            clsGlobales.bFlag = false;

            if (clsGlobales.cParametro.ControlLoginOff != 0)
            { 
                clsGlobales.UsuarioLogueado.UpdateUserLogin(0); 
            }
            
            Application.Exit();
        }

        #endregion

        #region Método que dasctiva el Menú del sistema

        public void DesactivarMenu()
        {
            this.mnuPrincipal.Enabled = false;

        }
        #endregion

        #region Método que llama al formulario UsuariosControl

        private void LLamarFormularioControlUsuarios()
        {
            // llamada al formulario de control de usuarios. G.
            frmUsuariosControl Frm = new frmUsuariosControl();
            // Inicializo el formulario como dialogo para que me suspenda el Load hasta que se realice el control del usuario. G.
            Frm.ShowDialog();
            // Ejecuto la activación del menú dependiendo del nivel asignado al usuario. G.
            if (clsGlobales.UsuarioLogueado.Nivel > 0)
            {
                //Obtener usuarios logueados
                string myCad = "Select * from Usuarios WHERE logged!=0";
                DataTable myDataTB = clsDataBD.GetSql(myCad);
                string myCadAux = " Ninguno ";

                //HAY OTROS USUARIOS?
                if (myDataTB.Rows.Count > 0)
                {
                    myCadAux = "";
                    myCadAux = " ";

                    foreach (DataRow Row in myDataTB.Rows)
                    {
                        if (!(Convert.ToInt32(Row["IdUsuario"].ToString()) == clsGlobales.UsuarioLogueado.IdUsuario))
                        {
                            myCadAux += Row["Usuario"].ToString() + " , ";
                        }
                    }

                    if ((myDataTB.Rows.Count - 1) != 0)
                    {
                        //Tomar la cadena
                        myCadAux = "(" + clsGlobales.Left(myCadAux, myCadAux.Length - 2) + " ) ";
                    }
                    else
                    {
                        myCadAux = "Ninguno";
                    }
                }


                // Activo el menú. G.
                ActivarMenu();

                // Asigno el nombre del usuario al label Usuario. G. / N. 2016-09-09
                this.lblUserLoguin.Text = clsGlobales.UsuarioLogueado.Usuario.ToString() + "  ";
                this.lblNivel.Text = "Nivel: " + clsGlobales.UsuarioLogueado.Nivel.ToString();
                this.lblOtrosUser.Text = myCadAux;

            }
        }
        #endregion

        #region Metodo ActualizarUsuariosLoguin

        private void ActualizarUsuariosLoguin() 
        {
            if (clsGlobales.UsuarioLogueado == null)
            {
                return;
            }
            
            //Obtener usuarios logueados
            string myCad = "Select * from Usuarios WHERE logged!=0";
            DataTable myDataTB = clsDataBD.GetSql(myCad);
            string myCadAux = " Ninguno ";

            //HAY OTROS USUARIOS?
            if (myDataTB.Rows.Count > 0)
            {
                myCadAux = " ";

                foreach (DataRow Row in myDataTB.Rows)
                {
                    if (!(Convert.ToInt32(Row["IdUsuario"].ToString()) == clsGlobales.UsuarioLogueado.IdUsuario))
                    {
                        myCadAux += Row["Usuario"].ToString() + " , ";
                    }
                }

                if ((myDataTB.Rows.Count - 1) != 0)
                {
                    //Tomar la cadena
                    myCadAux = " ( " + clsGlobales.Left(myCadAux, myCadAux.Length - 2) + " ) ";
                }
                else
                {
                    myCadAux = "Ninguno";
                }
            }
            
            // Ejecuto la activación del menú dependiendo del nivel asignado al usuario. G.
            if (clsGlobales.UsuarioLogueado.Nivel > 0)
            {
                // Asigno el nombre del usuario al label Usuario. G. / N. 2016-09-09
                this.lblUserLoguin.Text = clsGlobales.UsuarioLogueado.Usuario.ToString() + "  ";
            }
            
            this.lblNivel.Text = "Nivel: " + clsGlobales.UsuarioLogueado.Nivel.ToString();
            this.lblOtrosUser.Text = myCadAux;

        }

        #endregion

        #region Método que controla el funcionamiento del Menú dependiendo del nivel del Usuario

        public void ActivarMenu()
        {
            // Verifico que se haya pasado por el formulario de control de usuarios. G.
            if (clsGlobales.bBandera == true)
            {

                //Controlar Menu
                ControlaMenuByUserId();
   
            }

        }

        #endregion

        #region Metodo ControlaMenuByUserId

        private void ControlaMenuByUserId()
        {
            int IdUser = clsGlobales.UsuarioLogueado.IdUsuario;
            string sQuery = "exec ObtenerMenuOpcUser " + IdUser;

            //Rehabilita Menu
            this.MainMenuStrip.Enabled = true;


            // string sQuery = " "
            DataTable myData = clsDataBD.GetSql(sQuery);

            //RECORRER EL USUARIO E INHABILITAR O NO SEGUN CORRESPONDA
            //PARA SABER QUE ES CADA VALOR IR A LA TABLA "MENUOPCIONES"
            foreach (DataRow fila in myData.Rows)
            {
                //switch (Convert.ToInt32(fila["IdDetMenu"].ToString()))
                //{
                //    case 1:
                //        cotizacionesToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 2:
                //        pedidosToolStripMenuItem1.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 3:
                //        facturasToolStripMenuItem1.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 4:
                //        paraCostosToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 5:
                //        comunesToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 6:
                //        notaDeDébitoNotaDeCréditoToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 7:
                //        órdenesDePagoToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 8:
                //        actualizarInsumosToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 9:
                //        pedidosToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 10:
                //        presupuestosToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 11:
                //        facturasToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 12:
                //        remitosToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 14:
                //        rubrosToolStripMenuItem1.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 15:
                //        subRubrosToolStripMenuItem1.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 16:
                //        toolStripMenuItem1.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 17:
                //        insumosToolStripMenuItem1.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 18:
                //        composiciónToolStripMenuItem1.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 19:
                //        actualizaciónDePreciosToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 20:
                //        fabricaciónToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 21:
                //        insumosToolStripMenuItem2.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 22:
                //        productosToolStripMenuItem1.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 23:
                //        clientesToolStripMenuItem1.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 24:
                //        recibosToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 25:
                //        listadosToolStripMenuItem2.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 26:
                //        proveedoresToolStripMenuItem1.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 27:
                //        listadosToolStripMenuItem3.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 28:
                //        reportesToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 29:
                //        estadístocasToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 30:
                //        generadorFormularioDePedidos.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 31:
                //        CambiarUsuario.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 32:
                //        Gestionar.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 33:
                //        toolStripMenuItem2.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        if (!(clsGlobales.UsuarioLogueado.Nivel >= 5))
                //        {
                //            toolStripMenuItem2.Enabled = false;
                //        }

                //        break;
                //    case 34:
                //        TablasSistema.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 35:
                //        opcionesToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 36:
                //        avanzadasToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 37:
                //        PuntosPedido.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 38:
                //        puntosPedidoToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 39:
                //        ValidarSaldos.Enabled = true; // Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 40:
                //        if (clsGlobales.ConB == null)
                //        {
                //            ajustesToolStripMenuItem.Enabled = false;
                //        }
                //        else
                //        {
                //            ajustesToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        }
                //        break;
                //    case 41:
                //        exportaciónParaCITIComprasVentasToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 42:
                //        GeneradorArchivosED.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 43:
                //        ActualizarPreciosED.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 44: //Caja
                //        clsGlobales.bView = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 45: //Gneral
                //        movimientosToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 46:
                //        efectivoToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 47:
                //        Movimientos.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;
                //    case 48:
                //        Configuracion.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                //        break;                   
                //}

                switch (Convert.ToInt32(fila["IdDetMenu"].ToString()))
                {
                    case 1:
                        cotizacionesToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 2:
                        pedidosToolStripMenuItem1.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 3:
                        facturasToolStripMenuItem1.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 4:
                        paraCostosToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 5:
                        comunesToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    //case 6:
                    //    notaDeDébitoNotaDeCréditoToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                    //    break;
                    case 7:
                        órdenesDePagoToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 8:
                        actualizarInsumosToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 9:
                        pedidosToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 10:
                        presupuestosToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 11:
                        facturasToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 12:
                        remitosToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    //case 13:
                    //    NotaCredito.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                    //    break;
                    case 14:
                        rubrosToolStripMenuItem1.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 15:
                        subRubrosToolStripMenuItem1.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 16:
                        toolStripMenuItem1.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 17:
                        insumosToolStripMenuItem1.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 18:
                        composiciónToolStripMenuItem1.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    // case 19:
                    //     actualizacionesToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                    //     break;
                    case 20:
                        fabricaciónToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 21:
                        insumosToolStripMenuItem2.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 22:
                        productosToolStripMenuItem1.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 23:
                        clientesToolStripMenuItem1.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 24:
                        recibosToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 25:
                        listadosToolStripMenuItem2.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 26:
                        proveedoresToolStripMenuItem1.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 27:
                        listadosToolStripMenuItem3.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 28:
                        reportesToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 29:
                        estadístocasToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 30:
                        generadorFormularioDePedidos.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 31:
                        CambiarUsuario.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 32:
                        Gestionar.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 33:
                        toolStripMenuItem2.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 34:
                        TablasSistema.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 35:
                        opcionesToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 36:
                        avanzadasToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 37:
                        PuntosPedido.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 38:
                        puntosPedidoToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 39:
                        ValidarSaldos.Enabled = true; // Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 40:
                        if (clsGlobales.ConB == null)
                        {
                            ajustesToolStripMenuItem.Enabled = false;
                        }
                        else
                        {
                            ajustesToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        }
                        break;
                    case 41:
                        exportaciónParaCITIComprasVentasToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 42:
                        GeneradorArchivosED.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 43:
                        ActualizarPreciosED.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 44:
                        movInternosToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 45: //Caja
                        movimientosToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 46: //Gneral
                        efectivoToolStripMenuItem.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 47:
                        Movimientos.Enabled = Convert.ToBoolean(fila["Habilitado"]);
                        break;
                    case 48:
                        Configuracion.Enabled = Convert.ToBoolean(fila["Habilitado"]);                     
                        break;
                    case 49:
                        clsGlobales.bView = Convert.ToBoolean(fila["Habilitado"]);
                        break;  
                }
            }
        }

        #endregion

        #region Eventos y Metodos Menu

        private void frmMenuPrincipal_Load(object sender, EventArgs e)
        {
                
            clsGlobales.bFlag = true;
            
            //icon
            clsFormato.SetIconForm(this);

            //Cargar Configuracion
                clsGlobales.cParametro.ObtenerParametros();
            
            //Titulo
                this.Text = " PRAMA S.A.S " + this.Text;
  
            // Desactivo el menú esperando el nivel del usuario. G.
                DesactivarMenu();

            // Llamo al método que me instancia un formulario de control de usuarios. G.
                LLamarFormularioControlUsuarios();

            //Titulo Ventana
             //   this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;

            //Drop Allow            
                this.txt.AllowDrop = true;

            //Timer
                tmr.Enabled = true;



        }

        private void localidadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Creo un nuevo formulario de la clase frmLocalidades
            frmLocalidades myForm = new frmLocalidades();
            // Lo muestro
            myForm.ShowDialog();
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            lblHora.Text = "Hora: " + DateTime.Now.ToLongTimeString();

            //Verificar mayusculas
            if (Control.IsKeyLocked(Keys.CapsLock))
            {
                this.lblMayusValue.ForeColor = System.Drawing.Color.Red;
                this.lblMayusValue.Text = "SI";

            }
            else
            {
                this.lblMayusValue.ForeColor = System.Drawing.Color.Black;
                this.lblMayusValue.Text = "NO";
            }

            //Modo facturacion
            if (clsGlobales.cParametro.ModoFactura == 0)
            { this.strModoFacturacion.Text = "Testing"; }
            else
            { this.strModoFacturacion.Text = "Producción"; }
        }

        private void localidadesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Creo un nuevo formulario de la clase frmLocalidades
            frmLocalidades myForm = new frmLocalidades();
            // Muestro el formulario
            myForm.ShowDialog();
        }


        private void proveedoresToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            frmProveedores myForm = new frmProveedores();
            myForm.ShowDialog();
        }

        private void cotizacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmComprasComprobantes myForm = new frmComprasComprobantes("Cotizaciones");
            myForm.ShowDialog();
        }

        private void pedidosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmComprasComprobantes myForm = new frmComprasComprobantes("Ordenes");
            myForm.ShowDialog();
        }

        private void facturasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmComprasComprobantes myForm = new frmComprasComprobantes("Facturas");
            myForm.ShowDialog();
        }

        private void insumosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmArticulos myForm = new frmArticulos(1);
            myForm.ShowDialog();
        }

        private void composiciónToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmArticulos myForm = new frmArticulos(2);
            myForm.ShowDialog();
        }

        private void gastosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void rubrosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmArticulosRubros myForm = new frmArticulosRubros();
            myForm.ShowDialog();
        }

        private void subRubrosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmArticulosSubRubros myForm = new frmArticulosSubRubros();
            myForm.ShowDialog();
        }

        private void tiposDeArtículosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmArticulosCoeficientes myForm = new frmArticulosCoeficientes(0);
            myForm.ShowDialog();
        }

        private void áreasDeProducciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmArticulosAreasProduccion myForm = new frmArticulosAreasProduccion();
            myForm.ShowDialog();
        }

        #endregion

        #region Evento Menu Tipos de Cliente

        private void tiposDeClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Eventos MenuContextual de Iconificaciòn

        private void tsmRestaurar_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Maximized;
            this.Activate();
            this.Visible = true;
            this.notifyIcon1.Visible = false;
        }

        private void tsmSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMenuPrincipal_Resize(object sender, EventArgs e)
        {
            //Mostrar Icono en Area Notificacion? Parametros
            if (clsGlobales.cParametro.IconInTaskBar == 1)
            {
                //Parametrizar
                this.notifyIcon1.Icon = this.Icon;
                this.notifyIcon1.ContextMenuStrip = this.MenuContextual;
                this.notifyIcon1.Text = Application.ProductName;
                this.notifyIcon1.Visible = true;
                this.Visible = false;
                //Tipo de icono a mostrar el el globo informativo (Info, Error, None, Warning)
                this.notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                //Título del balón informativo (el nombre de la aplicación)
                this.notifyIcon1.BalloonTipTitle = Application.ProductName;
                //Texto del balón informativo
                this.notifyIcon1.BalloonTipText = "La aplicación ha quedado oculta " +
                    "en el área de notificación. Para mostrarla haga " +
                    "doble clic sobre el icono o click con botòn derecho " + 
                    "para ver opciones del menú contextual!";
                //Tiempo que aparecerá hasta ocultarse automáticamente
                this.notifyIcon1.ShowBalloonTip(8);
            }
        }

        #endregion

        #region Cuentas Corrientes Proveedores Menú Item

        private void listadosToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            frmComprasCC myForm = new frmComprasCC();
            myForm.ShowDialog();
        }

        #endregion

        #region Menu

        private void órdenesDePagoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmComprasOP myForm = new frmComprasOP();
            myForm.ShowDialog();
        }

        private void electrónicaAFIPWsfve1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVentasComprobantes myForm = new frmVentasComprobantes(0);
            myForm.ShowDialog();
        }

        private void actualizacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmArticulosActualizacion myForm = new frmArticulosActualizacion();
            myForm.ShowDialog();
        }

        private void pedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLista_Pedidos myForm = new frmLista_Pedidos();
            myForm.ShowDialog();
        }

        private void presupuestosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLista_Presupuestos myForm = new frmLista_Presupuestos();
            myForm.ShowDialog();
        }

        private void paraCostosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGastos myForm = new frmGastos();
            myForm.ShowDialog();
        }

        private void comunesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmComprasGastosFijos myForm = new frmComprasGastosFijos(clsGlobales.ComprobantesSeleccionados, 0, 0);
            myForm.ShowDialog();
        }

        private void listaDeProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExportarAExcel myForm = new frmExportarAExcel();
            myForm.ShowDialog();

        }

        private void fabricaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStockFabricacion myForm = new Formularios.Stock.frmStockFabricacion();
            myForm.ShowDialog();
        }

        private void facturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (!(clsGlobales.ConB == null))
            {
                frmVentasComprobantes myForm = new frmVentasComprobantes(1);
                myForm.ShowDialog();
            }
            else
            {
                frmVentasComprobantes myForm = new frmVentasComprobantes(0);
                myForm.ShowDialog();
            }
        }

        private void insumosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prama.Formularios.Stock.frmStockABM myForm = new frmStockABM("INSUMOS");
            myForm.ShowDialog();
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prama.Formularios.Stock.frmStockABM myForm = new frmStockABM("PRODUCTOS");
            myForm.ShowDialog();
        }

        private void insumosToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Prama.Formularios.Stock.frmStockABM myForm = new frmStockABM("INSUMOS");
            myForm.ShowDialog();
        }

        private void productosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Prama.Formularios.Stock.frmStockABM myForm = new frmStockABM("PRODUCTOS");
            myForm.ShowDialog();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
 /*           Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo("E:\\Developer\\PRAMA_ULTIMATE\\BCAfip.exe");
            //psi.Arguments = " -mod 2 -raz 3.00 -vis -3 -alt 80 -ali 0 -fno Tahoma -fsz 10 -cui " + dgvComprobantes.CurrentRow.Cells["CUIT"].Value.ToString() + " -tip  01 -pto " + dgvComprobantes.CurrentRow.Cells["Punto"].Value.ToString() + " -cae 01234567890123 -vto " + Convert.ToDateTime(dgvComprobantes.CurrentRow.Cells["VecCAE"].Value).ToString("yyyyMMdd") + " -out E:\\AFIP.jpg";
            psi.Arguments = " -mod 2 -raz 3.00 -vis -3 -alt 100 -ali 0 -fno Tahoma -fsz 12 -cui 20247548522 -tip 01 -pto 01 -cae 01234567890123 -vto 20170220 -out E:\\AFIP.jpg";
            p.StartInfo = psi;
            p.Start();*/

        }

        private void remitosToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //VERIFICAR ID 09-06-2017
            switch (clsGlobales.UsuarioLogueado.IdUsuario)
            {
                case 9:
                    frmRotuloSnRemito myForm = new frmRotuloSnRemito();
                    myForm.ShowDialog();
                    break;
                default:
                    //LLAMAR A REMITOS
                    frmRemitos myRemito = new frmRemitos();
                    myRemito.ShowDialog();
                    break;
            }

            //frmRemitos myRemito = new frmRemitos();
            //myRemito.ShowDialog();

        }

        private void actualizarInsumosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmComprasActualInsumos myForm = new frmComprasActualInsumos();
            myForm.ShowDialog();
        }

        private void informeDeProductosEInsumosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInformeProdInsumos myForm = new frmInformeProdInsumos();
            myForm.ShowDialog();

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmArticulosRubros_SubRubros myForm = new frmArticulosRubros_SubRubros();
            myForm.ShowDialog();
        }

        private void generadorFormularioDePedidos_Click(object sender, EventArgs e)
        {
            frmExportarAExcel myForm = new frmExportarAExcel();
            myForm.ShowDialog();

        }

        private void btn_MouseDown(object sender, MouseEventArgs e)
        {
            btn.DoDragDrop(btn.Text, DragDropEffects.Copy | DragDropEffects.Move);
        }

        private void txt_DragDrop(object sender, DragEventArgs e)
        {
            //OBTENER NUMERO PROCESADOR

            //string sNroProc = VerificarProcesador();

            //if (clsGlobales.sNumeroProcesador != sNroProc)
            //{
            //    MessageBox.Show("Operación no autorizada!. Comuníquese con soporte.", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            ///


            //VALIDAR SI LA CONEXION B ESTA NULL
            if (clsGlobales.ConB != null)
            {
                //MENSAJE Y CERRAR POR LAS DUDAS
                MessageBox.Show("Recuerde realizar un respaldo diario de la información!", "Tip del día!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                clsGlobales.ConB = null;
            }
            else
            {
                //Abrir conexion resguardo
                clsGlobales.ConB = new SqlConnection(clsGlobales.SqlCadConexion2);

                //Mensaje segun corresponda.
                if (!(clsGlobales.ConB == null))
                {
                    MessageBox.Show("Se ha producido un error no controlado por el sistema. Consulta al Administrador!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Recuerde realizar un respaldo diario de la información!", "Tip del Dìa!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            //Menu
            ControlaMenuByUserId();
        }

        #region Método que verifica el número de procesador de la PC

        public static string VerificarProcesador()
        {
            string sProcesadorId = "";
            string sQuery = "select ProcessorId FROM Win32_Processor";

            ManagementObjectSearcher oManagmentObjectSearcher = new ManagementObjectSearcher(sQuery);
            ManagementObjectCollection oCollection = oManagmentObjectSearcher.Get();

            foreach (ManagementObject oManagmentObjet in oCollection)
            {
                sProcesadorId = (string)oManagmentObjet["ProcessorId"];
            }
            return sProcesadorId;
        }

        private void txt_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void opcionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfigurar myForm = new frmConfigurar();
            myForm.ShowDialog();
        }

        private void avanzadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAvanzadas myForm = new frmAvanzadas();
            myForm.ShowDialog();
        }

        private void cambiarUsuarioToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // llamada al formulario de control de usuarios. G.
            frmUsuariosControl Frm = new frmUsuariosControl();

            //Cambiar bandera a .T. por cambio de usuario 12-05-2016
            clsGlobales.bChangeUser = true;

            // Llamo al método que me instancia un formulario de control de usuarios. G.
            LLamarFormularioControlUsuarios();

            //Controlar Menu
            ControlaMenuByUserId();
        }

        private void gestionarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Creo un nuevo formulario de la clase frmUsuariosABM
            frmUsuariosABM myForm = new frmUsuariosABM();
            // Muestro el formulario
            myForm.ShowDialog();
        }

        private void localidadesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Creo un nuevo formulario de la clase frmLocalidades
            frmLocalidades myForm = new frmLocalidades();
            // Muestro el formulario
            myForm.ShowDialog();
        }

        private void salirDelSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Confirma salir de la aplicación ?
            DialogResult dlResult = MessageBox.Show("Desea Salir del Sistema?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Si confirma salir de la aplicación....
            if (dlResult == DialogResult.Yes)
            {

                //login Off
                if (clsGlobales.cParametro.ControlLoginOff != 0)
                {
                    //Quitar marca de logueado para usuario viejo
                    clsGlobales.UsuarioLogueado.UpdateUserLogin(0);
                }

                // Cierro la aplicación. G.
                clsGlobales.bFlag = false;                
                Application.Exit();
            }
        }

        private void Calculadora_Click(object sender, EventArgs e)
        {
            clsSystem oSys = new clsSystem();
            if (!(oSys.getCalcEnEjecucion()))
            {
                System.Diagnostics.Process calc = new System.Diagnostics.Process { StartInfo = { FileName = @"calc.exe" } };
                calc.Start();
            }
            else
            {
                MessageBox.Show("La Calculadora de Windows ya está en ejecución!. Verifique!.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tiposDeClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTipoClientes myForm = new frmTipoClientes();
            myForm.ShowDialog();
        }

        private void áreasDeProducciónToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmArticulosAreasProduccion myForm = new frmArticulosAreasProduccion();
            myForm.ShowDialog();

        }

        private void tiposDeArtículosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmArticulosCoeficientes myForm = new frmArticulosCoeficientes(0);
            myForm.ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmPermisos myForm = new frmPermisos();
            myForm.ShowDialog();

            //Controlar Menu
            ControlaMenuByUserId();
        }

        private void Transportes_Click(object sender, EventArgs e)
        {
            frmTransporte myTransporte = new frmTransporte();
            myTransporte.ShowDialog();
        }

        private void tmrUsers_Tick(object sender, EventArgs e)
        {
            ActualizarUsuariosLoguin();
        }

        private void tiposDeMovimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prama.Formularios.Stock.frmStockMotivos myForm = new frmStockMotivos();
            myForm.ShowDialog();
        }

        private void iVAComprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prama.Formularios.Informes.frmInformeIvaCompras myForm = new frmInformeIvaCompras();
            myForm.ShowDialog();
        }

        private void iVAVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prama.Formularios.Informes.frmInformeIvaVentas myForm = new frmInformeIvaVentas();
            myForm.ShowDialog();
        }

        private void puntosPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmArticulosPtoPedido myForm = new frmArticulosPtoPedido(false);
            myForm.ShowDialog();
        }

        private void ventasDeProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInformeVtaArticulos myForm = new frmInformeVtaArticulos();
            myForm.ShowDialog();

        }

        private void CostosEnvios_Click(object sender, EventArgs e)
        {
            frmCostosEnvios myForm = new frmCostosEnvios();
            myForm.ShowDialog();
        }

        private void ValidarSldosClientes_Click(object sender, EventArgs e)
        {
            frmGeneradorSaldos mySaldo = new frmGeneradorSaldos();
            mySaldo.ShowDialog();
        }

        private void listadosToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            
            frmClientesCC myForm = new frmClientesCC();
            myForm.ShowDialog();

        }

        private void recibosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRecibos myRecibo = new frmRecibos();
            myRecibo.ShowDialog();
        }

        private void ajustesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStockAjustes myForm = new frmStockAjustes();
            myForm.ShowDialog();
        }

        private void NotaCredito_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Disponible en proxima version");
        }

        private void exportaciónParaCITIComprasVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExportarCITI myExportacion = new frmExportarCITI();
            myExportacion.ShowDialog();
        }

        private void salidasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prama.Formularios.Stock.frmStockMovInternosSalida myform = new frmStockMovInternosSalida();
            myform.ShowDialog();
        }

        private void ventasPorTipoDeClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInformeVtasTipoCli myForm = new frmInformeVtasTipoCli();
            myForm.ShowDialog();

        }

        private void ventasPorRubroSubRubroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVtasRubSubProdCli myForm = new frmVtasRubSubProdCli();
            myForm.ShowDialog();
        }

        private void ingresosToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                string myCadenaSQL = "";

                // Se crea el OpenFileDialog
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Archivos SQL (.sql)|*.sql|All Files (*.*)|*.*";
                // Se muestra al usuario esperando una acción
                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {


                    //Leer el archivo
                    myCadenaSQL = LeerArchivoPlano(dialog.FileName);

                    if (myCadenaSQL != "")
                    {
                        DialogResult myRespuesta = MessageBox.Show("Confirma la actualización de stock de " + iCantLineasPlano + " Artículos ???", "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (myRespuesta == DialogResult.Yes)
                        {
                            clsDataBD.GetSql(myCadenaSQL);
                            MessageBox.Show("Archivo procesado con ÉXITO !!!", "PROCESADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("No se pudo actualizar el stock", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            
        }

        #endregion

        #region Menu II

        private void corregirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCorregidorSaldosCtaCteClientes myForm = new frmCorregidorSaldosCtaCteClientes();
            myForm.ShowDialog();
        }

        private void ventasACobrarPorTransporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVtasPendTransportes myForm = new frmVtasPendTransportes();
            myForm.ShowDialog();
        }

        private void generadorDeArchivosParaEspacioDepurativoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Generar el Script
            GrabarArchivoPlano();
        }

        #endregion

        #region Método que lee los datos del archivo plano para actualizar el stock

        private string LeerArchivoPlano(string p_FN = "")
        {
            string aux = "";
            string line = "";
            iCantLineasPlano = 0;

            try
            {
                StreamReader sr = new StreamReader(p_FN);
                line = sr.ReadLine();
                aux = line;

                while (line != null)
                {
                    line = sr.ReadLine();
                    aux = aux + " " + line;
                    iCantLineasPlano++;
                }
            }
            catch
            {
                MessageBox.Show("ERROR al intentar leer el archivo","Información!",MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("Verifique que el archivo con el movimiento exista","Información!",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return aux;
        }

        #endregion

        #region Método que escribe los datos en un texto plano para actualizar el stock del espacio

        private void GrabarArchivoPlano()
        {
            string sFolder = "";

            DataTable myData = new DataTable();

            //Elegir carpeta destino
            if (FolderBrowser.ShowDialog() == DialogResult.OK)
            {
                sFolder = FolderBrowser.SelectedPath;
            }

            //Empty?
            if (sFolder == "") { return; }


            //Generar
            try
            {
              //Abrir Archivo Script  
                StreamWriter sw = new StreamWriter(sFolder + "\\sSqlScriptInsPrama.sql");
              
                //Articulos
                WriteArticulos(sw);
                //Insumos
                WriteInsumos(sw);
                //InsumosCompuestos
                WriteInsumosCompuestos(sw);
                //Productos
                WriteProductos(sw);
                //Productos Compuestos
                WriteProductosCompuestos(sw);
                //Productos Gastos Fijos
                WriteProductosGastosFijo(sw);
                //Productos Insumos
                WriteProductosInsumos(sw);
               //Cerrar
                sw.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR " + e.Message);
                return;
            }

            //Fin
            MessageBox.Show("El Proceso ha finalizado con exito!. Busque el archivo: sSqlScriptInsPrama.sql en " + sFolder, "Informaciòn!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        #endregion

        #region Metodo WriteArticulos

        private void WriteArticulos(StreamWriter sw)
        {

            string myCadInicial = "SET IDENTITY_INSERT [dbo].[Articulos] ON";
            string myCadFija = "INSERT [dbo].[Articulos] ([IdArticulo], [IdSubrubroArticulo], [IdUnidadMedida], [CodigoArticulo], [Articulo], [Unidades], [Precio], [PrecioAnterior], [UltimoCostoCompra], [UltimoProveedor], [UltimaCompra], [LlevaStock], [Facturable], [Stock], [StockMinimo], [StockMaximo], [StockPuntoPedido], [PorcentajeIva], [Activo], [IncListaPre], [IncListaRes], [chkSombreado], [rbtColor], [chkSProd], [CompIns]) VALUES ";
            string myCadActual = "";
            string myCadFinal = "SET IDENTITY_INSERT [dbo].[Articulos] OFF";

            sw.WriteLine("USE PramaSQL");
            sw.WriteLine("GO");
            sw.WriteLine(myCadInicial);

            DataTable myData = clsDataBD.GetSql("Select * from Articulos Order by IdArticulo");

            try
            {
                foreach (DataRow row in myData.Rows)
                {
                    myCadActual = "(" + row["IdArticulo"].ToString() + "," +
                                        row["IdSubrubroArticulo"].ToString() + "," +
                                        row["IdUnidadMedida"].ToString() + ",'" +
                                        row["CodigoArticulo"].ToString() + "','" +
                                        row["Articulo"].ToString() + "'," +
                                        "CAST(" + row["Unidades"].ToString() + " AS Decimal(11, 2))," +
                                        "CAST(" + row["Precio"].ToString() + " AS Decimal(11, 5))," +
                                        "CAST(" + row["PrecioAnterior"].ToString() + " AS Decimal(11, 5))," +
                                        "CAST(" + row["UltimoCostoCompra"].ToString() + " AS Decimal(11, 5)),'" +
                                        row["UltimoProveedor"].ToString() + "','" +
                                        row["UltimaCompra"].ToString() + "'," +
                                        Convert.ToInt32(row["LlevaStock"]).ToString() + "," +
                                        Convert.ToInt32(row["Facturable"]).ToString() + "," +
                                        "CAST(" + row["Stock"].ToString() + " AS Decimal(11, 2))," +
                                        "CAST(" + row["StockMinimo"].ToString() + " AS Decimal(11, 2))," +
                                        "CAST(" + row["StockMaximo"].ToString() + " AS Decimal(11, 2))," +
                                        "CAST(" + row["StockPuntoPedido"].ToString() + " AS Decimal(11, 2))," +
                                        "CAST(" + row["PorcentajeIva"].ToString() + " AS Decimal(5, 2))," +
                                        Convert.ToInt32(row["Activo"]).ToString() + "," +
                                        row["IncListaPre"].ToString() + "," +
                                        row["IncListaRes"].ToString() + "," +
                                        row["chkSombreado"].ToString() + "," +
                                        row["rbtColor"].ToString() + "," +
                                        row["chkSProd"].ToString() + "," +
                                        Convert.ToInt32(row["CompIns"]).ToString() + ")";

                    sw.WriteLine(myCadFija + myCadActual);
                }

                sw.WriteLine(myCadFinal);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ". Archivo icompleto o generado con errores!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

        }

        #endregion

        #region Metodo WriteInsumos

        private void WriteInsumos(StreamWriter sw)
        {

            string myCadInicial = "SET IDENTITY_INSERT [dbo].[Insumos] ON";
            string myCadFija = "INSERT [dbo].[Insumos] ([IdInsumo], [IdArticulo], [Costo]) VALUES ";
            string myCadActual = "";
            string myCadFinal = "SET IDENTITY_INSERT [dbo].[Insumos] OFF";

            sw.WriteLine("GO");
            sw.WriteLine(myCadInicial);

            DataTable myData = clsDataBD.GetSql("Select * from Insumos Order by IdInsumo");

            foreach (DataRow row in myData.Rows)
            {
                myCadActual = "(" + Convert.ToInt32(row["IdInsumo"]).ToString() + "," +
                                    Convert.ToInt32(row["IdArticulo"]).ToString() + "," +
                                   "CAST(" + row["Costo"].ToString() + " AS Decimal(11, 5)))";

                sw.WriteLine(myCadFija + myCadActual);
            }

            sw.WriteLine(myCadFinal);
        }

        #endregion

        #region Metodo WriteInsumosCompuestos

        private void WriteInsumosCompuestos(StreamWriter sw)
        {

          //  string myCadInicial = "SET IDENTITY_INSERT [dbo].[InsumosCompuestos] ON";
            string myCadFija = "INSERT [dbo].[InsumosCompuestos] ([IdDetInsComp], [IdInsOrigen], [IdInsCompone], [Cantidad], [Costo], [cTanda]) VALUES ";
            string myCadActual = "";
            string myCadFinal = "SET IDENTITY_INSERT  [dbo].[InsumosCompuestos] OFF";

            sw.WriteLine("GO");
       //     sw.WriteLine(myCadInicial);

            DataTable myData = clsDataBD.GetSql("Select * from InsumosCompuestos Order by IdDetInsComp");

            foreach (DataRow row in myData.Rows)
            {
                myCadActual = "(" + Convert.ToInt32(row["IdDetInsComp"]).ToString() + "," +
                                    Convert.ToInt32(row["IdInsOrigen"]).ToString() + "," +
                                    Convert.ToInt32(row["IdInsCompone"]).ToString() + "," +
                                    Convert.ToInt32(row["Cantidad"]).ToString() + "," +
                                   "CAST(" + row["Costo"].ToString() + " AS Decimal(11, 5))" + "," +
                                   Convert.ToInt32(row["cTanda"]).ToString() + ")";

                sw.WriteLine(myCadFija + myCadActual);
            }

        //    sw.WriteLine(myCadFinal);

        }

        #endregion

        #region Metodo WriteProductos

        private void WriteProductos(StreamWriter sw)
        {

            string myCadInicial = "SET IDENTITY_INSERT [dbo].[Productos] ON";
            string myCadFija = "INSERT [dbo].[Productos] ([IdProducto], [IdArticulo], [CostoAcumulado], [CostoInsumos], [CostoGastos], [IdAreaProduccion], [IdCoeficienteArticulo], [Tanda]) VALUES ";
            string myCadActual = "";
            string myCadFinal = "SET IDENTITY_INSERT [dbo].[Productos] OFF";

            sw.WriteLine("GO");
            sw.WriteLine(myCadInicial);

            DataTable myData = clsDataBD.GetSql("Select * from Productos Order by IdProducto");

            foreach (DataRow row in myData.Rows)
            {
                myCadActual = "(" + Convert.ToInt32(row["IdProducto"]).ToString() + "," +
                                    Convert.ToInt32(row["IdArticulo"]).ToString() + "," +
                                    "CAST(" + row["CostoAcumulado"].ToString() + " AS Decimal(18, 5))" + "," +
                                    "CAST(" + row["CostoInsumos"].ToString() + " AS Decimal(18, 5))" + "," +
                                    "CAST(" + row["CostoGastos"].ToString() + " AS Decimal(18, 5))" + "," +
                                    Convert.ToInt32(row["IdAreaProduccion"]).ToString() + "," +
                                    Convert.ToInt32(row["IdCoeficienteArticulo"]).ToString() + "," +
                                    Convert.ToInt32(row["Tanda"]).ToString() + ")";
                 
                sw.WriteLine(myCadFija + myCadActual);
            }

            sw.WriteLine(myCadFinal);

        }

        #endregion

        #region Metodo WriteProductosCompuestos

        private void WriteProductosCompuestos(StreamWriter sw)
        {

           // string myCadInicial = "SET IDENTITY_INSERT [dbo].[ProductosCompuestos] ON";
            string myCadFija = "INSERT [dbo].[ProductosCompuestos] ([IdDetProductoCompuesto], [IdProdOrigen], [IdProdCompone], [Cantidad]) VALUES ";
            string myCadActual = "";
            string myCadFinal = "SET IDENTITY_INSERT [dbo].[ProductosCompuestos] OFF";

            sw.WriteLine("GO");
         //   sw.WriteLine(myCadInicial);

            DataTable myData = clsDataBD.GetSql("Select * from ProductosCompuestos Order by IdDetProductoCompuesto");

            foreach (DataRow row in myData.Rows)
            {
                myCadActual = "(" + Convert.ToInt32(row["IdDetProductoCompuesto"]).ToString() + "," +
                                    Convert.ToInt32(row["IdProdOrigen"]).ToString() + "," +
                                    Convert.ToInt32(row["IdProdCompone"]).ToString() + "," +
                                    Convert.ToInt32(row["Cantidad"]).ToString() + ")";

                sw.WriteLine(myCadFija + myCadActual);
            }

         //   sw.WriteLine(myCadFinal);
        }

        #endregion

        #region Metodo WriteProductosGastosFijo

        private void WriteProductosGastosFijo(StreamWriter sw)
        {

            string myCadInicial = "SET IDENTITY_INSERT [dbo].[ProductosGastosFijos] ON";
            string myCadFija = "INSERT [dbo].[ProductosGastosFijos] ([IdProductoGastoFijo], [IdProducto], [IdGastoFijo], [Cantidad], [Costo], [Activo]) VALUES ";
            string myCadActual = "";
            string myCadFinal = "SET IDENTITY_INSERT [dbo].[ProductosGastosFijos] OFF";

            sw.WriteLine("GO");
            sw.WriteLine(myCadInicial);

            DataTable myData = clsDataBD.GetSql("Select * from ProductosGastosFijos Order by IdProductoGastoFijo");

            foreach (DataRow row in myData.Rows)
            {
                myCadActual = "(" + Convert.ToInt32(row["IdProductoGastoFijo"]).ToString() + "," +
                                    Convert.ToInt32(row["IdProducto"]).ToString() + "," +
                                    Convert.ToInt32(row["IdGastoFijo"]).ToString() + "," +
                                    "CAST(" + row["Cantidad"].ToString() + " AS Decimal(11, 5))" + "," +
                                    "CAST(" + row["Costo"].ToString() + " AS Decimal(11, 5))" +  "," + 
                                     Convert.ToInt32(row["Activo"]).ToString() + ")";

                sw.WriteLine(myCadFija + myCadActual);
            }

            sw.WriteLine(myCadFinal);

        }

        #endregion

        #region Metodo WriteProductosGastosFijo

        private void WriteProductosInsumos(StreamWriter sw)
        {

            string myCadInicial = "SET IDENTITY_INSERT [dbo].[ProductosInsumos] ON";
            string myCadFija = "INSERT [dbo].[ProductosInsumos] ([IdProductoInsumo], [IdProducto], [IdInsumo], [Cantidad], [Costo], [Activo]) VALUES ";
            string myCadActual = "";
            string myCadFinal = "SET IDENTITY_INSERT [dbo].[ProductosInsumos] OFF";

            sw.WriteLine("GO");
            sw.WriteLine(myCadInicial);

            DataTable myData = clsDataBD.GetSql("Select * from ProductosInsumos Order by IdProductoInsumo");

            foreach (DataRow row in myData.Rows)
            {
                myCadActual = "(" + Convert.ToInt32(row["IdProductoInsumo"]).ToString() + "," +
                                    Convert.ToInt32(row["IdProducto"]).ToString() + "," +
                                    Convert.ToInt32(row["IdInsumo"]).ToString() + "," +
                                    "CAST(" + row["Cantidad"].ToString() + " AS Decimal(18, 5))" + "," +
                                    "CAST(" + row["Costo"].ToString() + " AS Decimal(18, 5))" + "," +
                                     Convert.ToInt32(row["Activo"]).ToString() + ")";

                sw.WriteLine(myCadFija + myCadActual);
            }

            sw.WriteLine(myCadFinal);

        }

        #endregion

        private void ActualizarPreciosED_Click(object sender, EventArgs e)
        {
            string sFolder = "";

            //Guardar Stock Actual en Vector
            DataTable myDataTB = clsDataBD.GetSql("Select * from Articulos Order by IdArticulo");

            int filas = myDataTB.Rows.Count + 1; //Cantidad Articulos

            double[,] Articulos_Stock = new double[0, 2] { }; //Matriz

            Articulos_Stock = (double[,])clsValida.ResizeMatriz(Articulos_Stock, new int[] { filas, 2 });

            int iterador = 0; //Iterador

            //Almacenar Stock
            foreach (DataRow row in myDataTB.Rows)
            {
                Articulos_Stock[iterador, 0] = Convert.ToDouble(row["IdArticulo"].ToString());
                Articulos_Stock[iterador, 1] = Convert.ToDouble(row["Stock"].ToString());

                //Siguiente
                iterador++;
            }


            //Elegir carpeta destino
            if (FolderBrowser.ShowDialog() == DialogResult.OK)
            {
                sFolder = FolderBrowser.SelectedPath;
            }

            Cursor.Current = Cursors.WaitCursor;


            //Verificar
            if (!(System.IO.File.Exists(sFolder + "\\sSqlScriptInsPrama.sql")))
            {
                MessageBox.Show("No se encontraron uno o más archivos requeridos para esta operación!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            //Backup de Seguridad
            //CreateBkpPramaEspacio();

            //..........................

            string script = File.ReadAllText(Application.StartupPath + "\\sSqlScriptPrama.sql");

            // split script on GO command
            IEnumerable<string> commandStrings = Regex.Split(script, @"^\s*GO\s*$",
                                     RegexOptions.Multiline | RegexOptions.IgnoreCase);
            //Execute
            //Open
            clsGlobales.Con.Open();
            foreach (string commandString in commandStrings)
            {
                if (commandString.Trim() != "")
                {
                    using (var command = new SqlCommand(commandString, clsGlobales.Con))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            //Close
            clsGlobales.Con.Close();


            //Generar el otro Script
            string Script1 = File.ReadAllText(sFolder + "\\sSqlScriptInsPrama.sql");

            // split script on GO command
            IEnumerable<string> cStrings1 = Regex.Split(Script1, @"^\s*GO\s*$",
                                     RegexOptions.Multiline | RegexOptions.IgnoreCase);
            //Execute

            //Open
            clsGlobales.Con.Open();
            foreach (string cString in cStrings1)
            {
                if (cString.Trim() != "")
                {
                    using (var commando = new SqlCommand(cString, clsGlobales.Con))
                    {
                        commando.ExecuteNonQuery();
                    }
                }
            }
            //Close
            clsGlobales.Con.Close();

            //CAMBIAR PUNTERO MOUSE
            Cursor.Current = Cursors.Default;

            //Leer Vector y Actualizar Stock con datos vector
            string cadSQL = "";
            for (int iTerar = 0; iTerar < Articulos_Stock.GetLength(0) - 1; iTerar++)
            {
                cadSQL = "UPDATE Articulos SET Stock = " + Convert.ToDouble(Articulos_Stock[iTerar, 1].ToString()) +
                         " WHERE IdArticulo = " + Convert.ToInt32(Articulos_Stock[iTerar, 0].ToString());
                clsDataBD.GetSql(cadSQL);
            }

            //Reactivar Constraint
            cadSQL = "EXEC sp_msforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all'";
            //
            clsDataBD.GetSql(cadSQL);
            //Fin
            MessageBox.Show("El proceso de <Actualización> ha finalizado con exito!", "Informacion!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void productosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmArticulosPtoPedido myForm = new frmArticulosPtoPedido(true);
            myForm.ShowDialog();
        }

        private void insumosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmArticulosPtoPedido myForm = new frmArticulosPtoPedido(false);
            myForm.ShowDialog();
        }

        private void informeControlDeRevendedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReporteTipoCli myForm = new frmReporteTipoCli();
            myForm.ShowDialog();
        }

        private void valuaciónDeStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStockValuacion myForm = new frmStockValuacion();
            myForm.ShowDialog();
        }

        private void movimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prama.Formularios.Caja.frmCajaMovimientos myForm = new Formularios.Caja.frmCajaMovimientos();
            myForm.ShowDialog();
        }

        private void efectivoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Prama.Formularios.Caja.frmCajaEfectivo myForm = new Formularios.Caja.frmCajaEfectivo();
            myForm.ShowDialog();
        }

        private void composiciónToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            frmArticulos myForm = new frmArticulos(2);
            myForm.ShowDialog();
        }

        private void Transferencias_Click(object sender, EventArgs e)
        {
            Prama.Formularios.Caja.frmMostrarCajas myForm = new Formularios.Caja.frmMostrarCajas(" - DETALLE CAJA TRANSFERENCIAS ",2);
            myForm.ShowDialog();
        }

        private void MercadoPago_Click(object sender, EventArgs e)
        {
            Prama.Formularios.Caja.frmMostrarCajas myForm = new Formularios.Caja.frmMostrarCajas(" - DETALLE CAJA MERCADO-PAGO ", 3);
            myForm.ShowDialog();

        }

        private void Debito_Click(object sender, EventArgs e)
        {
            Prama.Formularios.Caja.frmMostrarCajas myForm = new Formularios.Caja.frmMostrarCajas(" - DETALLE CAJA DEBITO ", 4);
            myForm.ShowDialog();
        }

        private void Credito_Click(object sender, EventArgs e)
        {
            Prama.Formularios.Caja.frmMostrarCajas myForm = new Formularios.Caja.frmMostrarCajas(" - DETALLE CAJA CREDITO ", 5);
            myForm.ShowDialog();

        }

        private void Cuentas_Click(object sender, EventArgs e)
        {

            Prama.Formularios.Caja.frmCuentasCaja myForm = new Formularios.Caja.frmCuentasCaja();
            myForm.ShowDialog();
        }

        private void Asociaciones_Click(object sender, EventArgs e)
        {
            Prama.Formularios.Caja.frmAsociacionesCuentas myForm = new Formularios.Caja.frmAsociacionesCuentas();
            myForm.ShowDialog();

        }

        private void Cheques_Click(object sender, EventArgs e)
        {
            Prama.Formularios.Caja.frmCajaCheques myForm = new Formularios.Caja.frmCajaCheques();
            myForm.ShowDialog();
        }

        private void bancosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Prama.Formularios.Caja.frmCajaBancos myForm = new Formularios.Caja.frmCajaBancos();
            //myForm.ShowDialog();
        }

        private void actualizaciónDePreciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmArticulosActualizacion myForm = new frmArticulosActualizacion();
            myForm.ShowDialog();
        }

        private void Movimientos_Click(object sender, EventArgs e)
        {
            Prama.Formularios.Caja.frmCajaABM myForm = new Formularios.Caja.frmCajaABM();
            myForm.ShowDialog();
        }


        #endregion

        private void mercadopagoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prama.Formularios.Caja.frmCajaBcoPendientes myForm = new Formularios.Caja.frmCajaBcoPendientes(2);
            myForm.ShowDialog();
        }

        private void créditoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prama.Formularios.Caja.frmCajaBcoPendientes myForm = new Formularios.Caja.frmCajaBcoPendientes(4);
            myForm.ShowDialog();
        }

        private void imputacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prama.Formularios.Caja.frmCajaImputaciones myForm = new Formularios.Caja.frmCajaImputaciones();
            myForm.ShowDialog();
        }

        private void validarProvinciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsGlobales.UsuarioLogueado.IdUsuario == 1)
            {
                frmBugsClientes myForm = new frmBugsClientes();
                myForm.ShowDialog();
            }
        }

    }
}

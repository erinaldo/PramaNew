using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;


namespace Prama
{
    public partial class frmUsuariosControl : Form
    {
        // Variable que me indica si ya pasó por el Load el formulario
        bool yaPaso = false;
        // Tabla del formulario
        DataTable mDtTable = new DataTable();
        
        public frmUsuariosControl()
        {
            InitializeComponent();
            clsGlobales.bBandera = false;

        }

        #region Evento Load Formulario

        private void frmUsuariosControl_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 		
            // Cargo los usuarios en en Combo. G.
            CargarUsuarios();
            // Dejo el combo vacío. G.
            cboUsuario.SelectedIndex = -1;
            // Modifico la variable indicando que ya pasó por acá la rutina
            yaPaso = true;
            // Cargar ToolTips
            CargarToolTips();
            //Titulo Ventana
            this.Text = "PRAMA S.A.S " + this.Text;
        }

        #endregion

        #region cboUsuario: SelectedIndexChanged

        private void cboUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verificamos que ya se cargó el formulario
            if (yaPaso)
            {
                // Cargo la imagen del Usuario en el Pic del formulario
              //  CargarImagenUsuario();
                txtContra.Focus();
            }
        }

        #endregion

        #region Método para cargar los usuarios al combobox del formulario

        private void CargarUsuarios()
        {

            string myCadena = "";

            // Cadena SQL 
         /*   if (clsGlobales.cParametro.ControlLoginOff!=0)
            {
                myCadena = "SELECT Usuarios.IdUsuario, Usuarios.Usuario, Usuarios.Clave," +
                          "Usuarios.Nivel, Usuarios.Activo, Usuarios.Imagen FROM Usuarios WHERE ((Usuarios.Activo)=1) AND logged=0 order by Usuarios.Usuario";
            }
            else
            {*
          * 
          * }*/
                myCadena = "SELECT Usuarios.IdUsuario, Usuarios.Usuario, Usuarios.Clave," +
                          "Usuarios.Nivel, Usuarios.Activo, Usuarios.Imagen FROM Usuarios WHERE ((Usuarios.Activo)=1) order by Usuarios.Usuario";
            

            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.                
            mDtTable = clsDataBD.GetSql(myCadena);

            //Cargamos el Recordset de Usuarios a la propiedad DataSource del Combo.
            cboUsuario.DataSource = mDtTable;
        }

        #endregion

        #region Metodo Bytes2Image para cargar imagen en Picture (desde base SQL)
        /// <summary>
        /// el método Byte2Image recibe un array de bytes como parámetro,
        /// lo asigna a un objeto del tipo MemoryStream y ese "stream" lo utiliza para crear un objeto del tipo Bitmap,
        /// finalmente devuelve ese objeto que en el fondo es un objeto de tipo Image.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private Image Bytes2Image(byte[] bytes)
        {
            if (bytes == null) return null;

            MemoryStream ms = new MemoryStream(bytes);
            Bitmap bm = null;

            try
            {
                bm = new Bitmap(ms);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return bm;
        }
        #endregion

        #region Metodo CargarImagenUsuario

        private void CargarImagenUsuario()
        {
            // Declaro una variable entera y le paso el Id del Usuario seleccionado en el combo
            int IdUsuario = Convert.ToInt32(cboUsuario.SelectedValue);
            // Recorro la tabla
            foreach (DataRow row in mDtTable.Rows)
            {

                if (Convert.ToInt32(row["IdUsuario"]) == IdUsuario)
                {
                    //Cargar Imagen del Elector al PictureBox, si esta cargada                               
                    try
                    {
                        byte[] img = (byte[])row["Imagen"];
                        this.picFoto.Image = Bytes2Image(img);
                    }
                    catch
                    {
                        this.picFoto.Image = null;
                        return;
                    }
                }

            }
        }
        #endregion

        #region CargarToolTips

        private void CargarToolTips()
        {
            toolTip1.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip2.SetToolTip(this.btnSalir, "Salir");
        }

        #endregion

        #region Eventos Botones

        #region btnAceptar: Click

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            int OldUserId = 0;

            //Null
            if (!(clsGlobales.UsuarioLogueado==null))
            {
               //Usuario logueado
                 OldUserId = clsGlobales.UsuarioLogueado.IdUsuario;
            }

            //Flag                        
            clsGlobales.bBandera = true; // Activa la bandera. G.
            // Creo un nuevo usuario y le paso los datos del formulario. G.
            clsUsuarios NuevoUsuario = new clsUsuarios();
            // Asigno al nuevo usuario los datos del formulario. G.
            NuevoUsuario.Usuario = cboUsuario.Text;
            NuevoUsuario.Clave = txtContra.Text;
            //Declaramos una variable que nos va a almacenar la consulta
            string query;
            // Creamos la consulta SQL. G.
            /*if (clsGlobales.cParametro.ControlLoginOff != 0)
            {
                query = "Select * From Usuarios where logged=0";
            }
            else
            {
              query = "Select * From Usuarios";
            }*/

            query = "Select * From Usuarios";

           // Cargamos los datos en la tabla. G.            
            DataTable mDataTableUser = new DataTable();
            mDataTableUser = clsDataBD.GetSql(query);
            // Empezamos a recorrer la tabla. G.
            foreach (DataRow row in mDataTableUser.Rows)
            {
                // Buscamos el usuario. G.
                if (Convert.ToString(row["Usuario"]) == NuevoUsuario.Usuario.ToString())
                {
                    // Comparamos si la contraseña del usuario es correcta. G.
                    if (Convert.ToString(row["Clave"]) == NuevoUsuario.Clave.ToString())
                    {
                        // Si la contraseña es correcta, paso los datos que faltan al nuevo usuario. G.
                        NuevoUsuario.IdUsuario = Convert.ToInt32(row["IdUsuario"]);
                        NuevoUsuario.Nivel = Convert.ToInt32(row["Nivel"]);
                        NuevoUsuario.Activo = Convert.ToBoolean(row["Activo"]);;
                    }
                    // Si la contraseña es incorrecta... G.
                    else
                    {
                        // Muestro el mensaje de que la contraseña es incorrecta. G.
                        MessageBox.Show("La contraseña ingresada es incorrecta", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Pongo en blanco el texbox de la contraseña. G.
                        txtContra.Text = "";
                        // Pongo el foco en el txt de la contraseña. G.
                        txtContra.Focus();
                        //Cerrar conexion
                        //DataBD.Desconectar();
                        // Salgo de la rutina esperando el reingreso de la crontraseña. G.
                        return;
                    }

                  //Verificar usuario logueado
                    if (clsGlobales.cParametro.ControlLoginOff != 0)
                    {

                        if (OldUserId != 0)
                        {
                            //Quitar marca de logueado para usuario viejo
                            clsGlobales.UsuarioLogueado.UpdateUserLogin(0);
                        }

                    } 

                    // Asigno al Usuario global los datos del nuevo usuario. G.
                    clsGlobales.UsuarioLogueado = NuevoUsuario;

                    // Cargo los datos del Usuario a variables string
                    string MovIdUsuario = NuevoUsuario.IdUsuario.ToString();
                    string MovUsuario = NuevoUsuario.Usuario;
                    string movNivel = NuevoUsuario.Nivel.ToString();
                    string MovClave = NuevoUsuario.Clave.ToString();
                    string MovActivo = NuevoUsuario.Activo.ToString();
                    string c = " - ";

                    // Creo la cadena que va a almacenar los datos
                    string NuevoAcceso = MovIdUsuario + c + MovUsuario + c + movNivel + c + MovClave + c + MovActivo;
                    //Cerrar Conexion
                    // DataBD.Desconectar();
                    // Agrego el movimiento a La Tabla Movimientos de la BD
                    //DataBD.AgregarMovimientos("frmUsuariosControlAcceso", "ACCESO", NuevoAcceso, " ");
                    // Cierro el formulario   . G.


                    //Añadir marca de logueo! 2016-11-22 N.***********************
                     if (clsGlobales.cParametro.ControlLoginOff != 0)
                     {
                        clsGlobales.UsuarioLogueado.UpdateUserLogin(1);
                     }
                    /************************************************************/

                     

                    //Cerrar
                    this.Close();
                    return;
                }
            }
        }

        #endregion

        #region btnSalir: Click

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (clsGlobales.bChangeUser == false)
            {
                // Si se desea salir sin seleccionar ningún usuario en el combo o sin contraseña... G.
                if (cboUsuario.SelectedIndex == -1 || txtContra.Text == "")
                {
                    // Creo un nuevo usuario. G.
                    clsUsuarios NuevoUsuario = new clsUsuarios();
                    // Le paso cualquier valor para que la aplicación no me de ningún error. G.
                    NuevoUsuario.Usuario = "Vacío";
                    NuevoUsuario.Clave = "Vacío";
                    NuevoUsuario.Nivel = 0;
                    // Paso los datos al usario global. G.
                    clsGlobales.UsuarioLogueado = NuevoUsuario;
                }
                clsGlobales.bFlag = false;
                clsGlobales.bBandera = true;
                // Cierro la aplicación. G.
                Application.Exit();
            }
            else
            {
                //CERRAR FORMULARIO
                this.Close();
            }
        }

        #endregion

        #endregion
    }
}

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
    public partial class frmUsuariosABM : Form
    {
        int indexFila = 0;

        public frmUsuariosABM()
        {
            InitializeComponent();
        }

        #region Métodos del Formulario

        #region Método para cargar los usuarios al DataGrid

        private void CargarUsuarios(string Buscar, string Campo)
        {
            // Declaro una variable string para almacenar la cadena de la consulta SQL de ACCESS.
            string myCadena = "";
            if (Buscar == "")
            {
                // Averiguo si el usuario actual es administrador
                if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja)
                {
                    // Todos los Usuarios
                    myCadena = "SELECT * FROM Usuarios order by Usuario";
                }
                else
                {
                    // Solo el usuario actual con nivel de baja puede ver todo
                    myCadena = "SELECT * FROM Usuarios WHERE (((Usuarios.IdUsuario)=" + clsGlobales.UsuarioLogueado.IdUsuario + ")) order by Usuario";
                }
            }
            else
            {
                // Averiguo si el usuario actual es administrador
                if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja)
                {
                    // Todos los Usuarios
                    myCadena = "SELECT * FROM Usuarios where " + Campo + " like '" + Buscar + "%' order by Usuario";
                }
                else
                {
                    // Solo el usuario actual al no ser de nivel 5
                    myCadena = "SELECT * FROM Usuarios WHERE (((Usuarios.IdUsuario)=" + clsGlobales.UsuarioLogueado.IdUsuario + ")) and " + Campo + " like '" + Buscar + "%' order by Usuario";
                }
            }
            
            // Evito que el dgvUsuarios genere columnas automáticas
            dgvUsuarios.AutoGenerateColumns = false;
            // Creo un nuevo DataTable
            DataTable mDtTable = new DataTable();
            // Le asigno al nuevo DataTable los datos de la consulta SQL
            mDtTable = clsDataBD.GetSql(myCadena);
            // Asigno el source de la grilla
            dgvUsuarios.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvUsuarios.Rows.Count;
            // Posiciono la grilla en la última fila
            //dgvUsuarios.CurrentCell = dgvUsuarios[1, Filas - 1];
        }

        #endregion

        #region Método para limpiar los controles del formulario
        //LIMPIA LA PROPIEDAD TEXT DE LOS CONTROLES DEL FORMULARIO. N.
        private void LimpiarControlesForm()
        {
            this.txtUsuario.Text = "";
            this.txtContra.Text = "";
            this.txtContraConfirma.Text = "";
            this.txtNivel.Text = "0";
            this.chkActivo.Checked = false;
            //this.picFoto.Image = null;
        }

        #endregion

        #region Metodo que limpia los campos de busqueda

        private void LimpiarControlesBusqueda()
        {
            txtBuscarUsuario.Text = "";
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
            //A = Alta, M = Modificacion, "C" = En espera, "E" = Especial
            switch (clsGlobales.myEstado)
            {
                case "A":
                    this.txtUsuario.TabStop = true;
                    this.txtUsuario.Enabled = true;
                    this.txtNivel.TabStop = true;
                    this.txtNivel.Enabled = true;
                    this.chkActivo.TabStop = true;
                    this.chkActivo.Enabled = true;
                    this.lblContra.Visible = true;
                    this.txtContra.TabStop = true;
                    this.txtContra.Visible = true;
                    this.lblContraConfirma.Visible = true;
                    this.txtContraConfirma.TabStop = true;
                    this.txtContraConfirma.Visible = true;
                    this.dgvUsuarios.TabStop = false;
                    this.dgvUsuarios.Enabled = false;
                    //this.picFoto.Enabled = false;
                    //this.picFoto.Visible = false;
                    this.btnBuscarFoto.Enabled = false;
                    this.btnBuscarFoto.Visible = false;
                    this.dgvUsuarios.Height = 240;
                    this.gpbBusquedas.Visible = false;
                    return;
                case "M":
                    this.txtUsuario.TabStop = true;
                    this.txtUsuario.Enabled = true;
                    this.txtNivel.TabStop = true;
                    this.txtNivel.Enabled = true;
                    this.chkActivo.TabStop = true;
                    this.chkActivo.Enabled = true;
                    this.lblContra.Visible = true;
                    this.txtContra.TabStop = true;
                    this.txtContra.Visible = true;
                    this.lblContraConfirma.Visible = true;
                    this.txtContraConfirma.TabStop = true;
                    this.txtContraConfirma.Visible = true;
                    this.dgvUsuarios.TabStop = false;
                    this.dgvUsuarios.Enabled = false;
                    //this.picFoto.Enabled = true;
                    //this.picFoto.Visible = true;
                    this.dgvUsuarios.Height = 240;
                    this.gpbBusquedas.Visible = false;

                    return;
                case "C":
                    this.txtUsuario.TabStop = false;
                    this.txtUsuario.Enabled = false;
                    this.txtNivel.TabStop = false;
                    this.txtNivel.Enabled = false;
                    this.chkActivo.TabStop = false;
                    this.chkActivo.Enabled = false;
                    this.lblContra.Visible = false;
                    this.txtContra.TabStop = false;
                    this.txtContra.Visible = false;
                    this.lblContraConfirma.Visible = false;
                    this.txtContraConfirma.TabStop = false;
                    this.txtContraConfirma.Visible = false;
                    this.dgvUsuarios.TabStop = true && (dgvUsuarios.RowCount > 0);
                    this.dgvUsuarios.Enabled = true && (dgvUsuarios.RowCount > 0);
                    //this.picFoto.Enabled = true && (dgvUsuarios.RowCount > 0);
                    //this.picFoto.Visible = true && (dgvUsuarios.RowCount > 0);
                    this.dgvUsuarios.Height = 240;
                    this.gpbBusquedas.Visible = false;

                    return;

                case "B":
                    this.txtUsuario.TabStop = false;
                    this.txtUsuario.Enabled = false;
                    this.txtNivel.TabStop = false;
                    this.txtNivel.Enabled = false;
                    this.chkActivo.TabStop = false;
                    this.chkActivo.Enabled = false;
                    this.lblContra.Visible = false;
                    this.txtContra.TabStop = false;
                    this.txtContra.Visible = false;
                    this.lblContraConfirma.Visible = false;
                    this.txtContraConfirma.TabStop = false;
                    this.txtContraConfirma.Visible = false;
                    this.dgvUsuarios.TabStop = true && (dgvUsuarios.RowCount > 0);
                    this.dgvUsuarios.Enabled = true && (dgvUsuarios.RowCount > 0);
                    //this.picFoto.Enabled = true && (dgvUsuarios.RowCount > 0);
                    //this.picFoto.Visible = true && (dgvUsuarios.RowCount > 0);
                    this.dgvUsuarios.Height = 180;
                    this.gpbBusquedas.Visible = true;

                    return;
            }
        }
        #endregion

        #region Método para activar los botones del formulario
        //--------------------------------------------------------------
        //ACTIVAR BOTONES  
        //SEGUN EL ESTADO (A, M, C) - MUESTRA U OCULTA BOTONES
        //--------------------------------------------------------------
        private void ActivarBotones()
        {
            switch (clsGlobales.myEstado)
            {
                case "A":
                case "B":
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
                    //this.btnBuscarFoto.TabStop = true;
                    //this.btnBuscarFoto.Visible = true;
                    this.btnBuscar.TabStop = false;
                    this.btnBuscar.Visible = false;
                    this.btnBorrar.TabStop = false;
                    this.btnBorrar.Visible = false;
                    this.btnImprimir.TabStop = false;
                    this.btnImprimir.Visible = false;
                    return;
                case "C":
                    this.btnAgregar.TabStop = true;
                    this.btnAgregar.Visible = true;
                    this.btnModificar.TabStop = true && (dgvUsuarios.RowCount != 0);
                    this.btnModificar.Visible = true && (dgvUsuarios.RowCount != 0);
                    this.btnBuscar.TabStop = true;
                    this.btnBuscar.Visible = true;
                    this.btnAceptar.TabStop = false;
                    this.btnAceptar.Visible = false;
                    this.btnCancelar.TabStop = false;
                    this.btnCancelar.Visible = false;
                    this.btnSalir.TabStop = true;
                    this.btnSalir.Visible = true;
                    this.btnBuscarFoto.TabStop = false;
                    this.btnBuscarFoto.Visible = false;
                    // Si el nivel del usuario es el 5, habilito el botón borrar
                    if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja)
                    {
                        this.btnBorrar.TabStop = true && (dgvUsuarios.RowCount != 0);
                        this.btnBorrar.Visible = true && (dgvUsuarios.RowCount != 0);
                    }
                    else
                    {
                        this.btnBorrar.TabStop = false;
                        this.btnBorrar.Visible = false;
                    }
                  //  this.btnImprimir.TabStop = true && (dgvUsuarios.RowCount != 0);
                  //  this.btnImprimir.Visible = true && (dgvUsuarios.RowCount != 0);
                    return;
            }
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

        private void CargarImagenUsuario(int Id)
        {
            // Todos los Usuarios
            string myCadena = "SELECT * FROM Usuarios";
            // Creo una nueva tabla
            DataTable mDtTable = new DataTable();
            // Le asigno al nuevo DataTable los datos de la consulta SQL
            mDtTable = clsDataBD.GetSql(myCadena);
            // Busco el usuario en la tabla
            foreach (DataRow row in mDtTable.Rows)
            {
                // Cuando lo encuentro
                if (Convert.ToInt32(row["IdUsuario"]) == Id)
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

      //  #region Método que graba las imágenes en la BD
      //  // Método que se produce al presionar el botón grabar
      //  private void GrabarImagen(int Id)
      //  {
      //      DataGridViewRow FilaSeleccionada = dgvUsuarios.CurrentRow;
      //      // Declaro una variable entera y le paso el Id del Usuario seleccionado en el combo
      //      int IdUsuario = Id;

      //      // Establezco las propiedades de la conexión pasando los parámetros
      //      clsGlobales.cmd.Connection = clsGlobales.Con;

      ///*      if (clsGlobales.myEstado == "M")
      //      {
      //          clsGlobales.cmd.CommandText = "UPDATE Usuarios SET Imagen = (@image) WHERE IdUsuario " + "= " + IdUsuario;
      //      }*/

      //      // Asignando el valor de la imagen
      //      // Stream usado como buffer
      //      System.IO.MemoryStream ms = new System.IO.MemoryStream();
      //      // Se guarda la imagen en el buffer
      //      //picFoto.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
      //      // Se extraen los bytes del buffer para asignarlos como valor para el parámetro.
      //      clsGlobales.cmd.Parameters["@image"].Value = ms.GetBuffer();
      //      // Abro la conexión
      //      clsGlobales.Con.Open();
      //      // Ejecuto la consulta
      //      clsGlobales.cmd.ExecuteNonQuery();
      //      // Cierro la conexión
      //      clsGlobales.Con.Close();

      //      // Informo que la imagen se grabó correctamente
      //      //MessageBox.Show("La imagen se agregó correctamente", "ÉXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);

      //      // Deshabilitamos los botones del formulario
      //      //cmdEliminar.Enabled = false;
      //      //cmdGrabar.Enabled = false;

      //      // Refresco la grilla
      //      CargarUsuarios("","");
      //  }
      //  #endregion

        #region Método que pasa los datos de la grilla a la clase Usuarios

        private void PasarDatosUsuario()
        {
            // Hacer método para simplificar el código, usando la clase Usuarios
        }

        #endregion

        #region Método que carga los ToolTips

        private void CargarToolTips()
        {
            toolTip1.SetToolTip(this.btnAgregar, "Agregar");
            toolTip2.SetToolTip(this.btnModificar, "Modificar");
            toolTip3.SetToolTip(this.btnBorrar, "Borrar");
            toolTip4.SetToolTip(this.btnImprimir, "Imprimir");
            toolTip5.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip6.SetToolTip(this.btnCancelar, "Cancelar");
            toolTip7.SetToolTip(this.btnSalir, "Salir");
            toolTip8.SetToolTip(this.btnBuscarFoto, "Buscar Imagen");
        }

        #endregion

        #region Método que pasa los datos de la grilla a los controles

        private void AlCambiarUsuarioEnGrilla()
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvUsuarios.RowCount == 0)
            {
                return;
            }
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvUsuarios.CurrentRow;
            // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
            bool UsuarioActivo = false;
            // Almaceno el Id del Usuario
            int Id = Convert.ToInt32(row.Cells["Codigo"].Value);
            //TRAER LOS DATOS. H.
            txtUsuario.Text = row.Cells["Usuario"].Value.ToString();
            txtNivel.Text = row.Cells["Nivel"].Value.ToString();
            txtContra.Text = row.Cells["Clave"].Value.ToString();
            txtContraConfirma.Text = row.Cells["Clave"].Value.ToString();
            //Convierto el dato del campo Activo del DGV en bool. G.
            UsuarioActivo = Convert.ToBoolean(row.Cells["Activo"].Value);
            // Si el usuario está activo, tildo el checked. G.
            if (UsuarioActivo == true)
            {
                chkActivo.Checked = true;
            }
            // Si el usuario está inactivo, no tildo el checked. G.
            else
            {
                chkActivo.Checked = false;
            }
            // Cargo la imagen del Usuario, pasando al método, el Id del Usuario
            //CargarImagenUsuario(Id);
        }

        #endregion

        #region Método que devuelve el foco a la fila desde donde se presionó algún botón

        private void PosicionarFocoFila()
        {
            if (dgvUsuarios.Rows.Count != 0 && dgvUsuarios.Rows.Count > this.indexFila)
            {
                // Devuelvo el foco a la fila de la grilla desde donde se llamó
                this.dgvUsuarios.CurrentCell = dgvUsuarios[1, this.indexFila];

                // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                EventArgs ea = new EventArgs();
                this.dgvUsuarios_SelectionChanged(this.dgvUsuarios, ea);
            }

        }

        #endregion

        #endregion

        #region Eventos del Formulario

        #region Evento Load del formulario

        private void frmUsuariosABM_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            // Agrego el parámetro Imagen al cmd
            clsGlobales.cmd.Parameters.Add("@image", System.Data.SqlDbType.Image);
            // Llamo al método cargar usuarios para rellenar la grilla. G.
            CargarUsuarios("", "");
            // Llamo al método activar los botones del formulario. G.
            ActivarBotones();
            // Llamo al método habilitar controles del formulario. G.
            HabilitarControles();
            // Cargar ToolTips
            CargarToolTips();
            this.Text = clsGlobales.cParametro.NombreFantasia + this.Text;
        }

        #endregion

        #region Eventos de la grilla

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AlCambiarUsuarioEnGrilla();
        }

        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            AlCambiarUsuarioEnGrilla();
        }

        #endregion

        #region Eventos de los botones

        #region Evento Click del botón Borrar

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            // Pregunto si el usuario actual es el mismo que se quiere modificar. G.
            if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja)
            {

                //Hay control de usuarios?....
                if (!(clsGlobales.cParametro.ControlLoginOff==1))
                {
                    if (clsGlobales.UsuarioLogueado.Logged)
                    {
                       MessageBox.Show("El Usuario a eliminar se encuentra logueado en el Sistema!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                       return;
                    }
                }

                //TRAER LA FILA ACTUAL. N.
                DataGridViewRow row = dgvUsuarios.CurrentRow;
                // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
                int Id = Convert.ToInt32(row.Cells["Codigo"].Value);
                string User = row.Cells["Usuario"].Value.ToString();
                int Niv = Convert.ToInt32(row.Cells["Nivel"].Value);

                //ATENCION
               if (Niv < clsGlobales.cParametro.NivelBaja)
                {
                    // El usuario no es de nivel 5, por lo que no puede crear un nuevo usuario. G.
                    MessageBox.Show("Usted no tiene los permisos para Eliminar este usuario", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Ejecuto el botón cancelar. G.
                    this.btnCancelar.PerformClick();
                }
                else
                {
                    // Confirma salir de la aplicación ?
                    DialogResult dlResult = MessageBox.Show("Desea Eliminar el Usuario " + User + " ?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    // Si confirma salir de la aplicación....
                    if (dlResult == DialogResult.Yes)
                    {
                        // Cambio a inactivo
                        string myCadena = "UPDATE Usuarios set Activo = 0 WHERE Usuarios.IdUsuario=" + Id;
                        clsDataBD.GetSql(myCadena);
                        // Refresco la grilla
                        CargarUsuarios("","");
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

        #region Evento Click del botón BuscarFoto

        private void btnBuscarFoto_Click(object sender, EventArgs e)
        {
            // Se crea el OpenFileDialog
            OpenFileDialog dialog = new OpenFileDialog();
            // Se muestra al usuario esperando una acción
            DialogResult result = dialog.ShowDialog();

            // Si seleccionó un archivo (asumiendo que es una imagen lo que seleccionó)
            // la mostramos en el PictureBox del formulario
           // if (result == DialogResult.OK)
            //{
                //picFoto.Image = Image.FromFile(dialog.FileName);
           // }
            // Habilito el botón para grabar la imagen en la base
            //cmdGrabar.Enabled = true;

        }

        #endregion

        #region Evento Click del botón Agregar

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Tomo la posición de la fila desde donde se llamo
            indexFila = dgvUsuarios.CurrentRow.Index;
            
            // Pregunto si el usuario actual es el mismo que se quiere modificar. G.
            if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja)
            {
                // Cambio el estado del formulario a agregar. G.
                clsGlobales.myEstado = "A";
                // Limpio los controles del formulario. G.    
                LimpiarControlesForm();
                // Activo los botones para este estado. G.
                ActivarBotones();
                // Habilito los controles para este estado. G.
                HabilitarControles();
                // Posiciono el foco sobre el primer textbox
                txtUsuario.Focus();
            }
            else
            {
                // El usuario no es de nivel 5, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene permisos para 'Agregar' un usuario!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
               // this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento Click del botón Modificar

        private void btnModificar_Click(object sender, EventArgs e)
        {
            // Tomo la posición de la fila desde donde se llamo
            indexFila = dgvUsuarios.CurrentRow.Index;
            
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvUsuarios.CurrentRow;
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvUsuarios.RowCount == 0)
            {
                // Salgo de la rutina. G.
                return;
            }
            // Pregunto si el usuario actual es el mismo que se quiere modificar. G.
            if (row.Cells["Usuario"].Value.ToString() == clsGlobales.UsuarioLogueado.Usuario)
            {
                // Cambio el estado del formulario a Modificar. G.
                clsGlobales.myEstado = "M";
                // Activo los botones para este estado. G.
                ActivarBotones();
                // Habilito los controles para este estado. G.
                HabilitarControles();
                // Averiguo si el usuario no es de nivel 5, solo lo dejo modificar el usuario y la contraseña
                if (clsGlobales.UsuarioLogueado.Nivel < clsGlobales.cParametro.NivelBaja)
                {
                    txtNivel.Enabled = false;
                    chkActivo.Enabled = false;
                }
                // Cargo los datos de la contraseña en sus correspondientes textbox. G.
                txtContra.Text = row.Cells["Clave"].Value.ToString();
                txtContraConfirma.Text = row.Cells["Clave"].Value.ToString();
                // Posiciono el foco sobre el primer textbox
                txtUsuario.Focus();
            }
            else
            {
                // Si el usuario es de nivel 5, le permitimos modificar solo su el usuario es activo o no
                if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja)
                {
                    // Cambio el estado del formulario a Modificar. G.
                    clsGlobales.myEstado = "M";
                    // Activo los botones para este estado. G.
                    ActivarBotones();
                    // Habilitamos el control. G.
                    HabilitarControles();
                    // Ocultamos los datos de la contraseña
                    txtContra.Visible = true;
                    lblContra.Visible = true;
                    txtContraConfirma.Visible = true;
                    lblContraConfirma.Visible = true;
                }
                else
                {
                    // Al no ser el usuario actual el mismo que se quiere modificar, lo informo y 
                    // vuelvo el formulario al estado anterior. G.
                    MessageBox.Show("Un usuario solo puede ser modificado por el mismo usuario", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Ejecuto el botón cancelar. G.
                    this.btnCancelar.PerformClick();
                }
            }
        }

        #endregion

        #region Evento Click del botón Aceptar

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Declaro la variable para la cadena SQL
            string myCadenaSQL = "";
            
            // controlo que la confirmación de la contraseña este correcta
            if (txtContra.Text == txtContraConfirma.Text)
            {
                // Creo un nuevo usuario de la clase usuario
                clsUsuarios NuevoUsuario = new clsUsuarios();
                // Creo un nuevo Usuario de la clase Usuario para los datos del CurrentRow.
                clsUsuarios UsuarioSeleccionado = new clsUsuarios();
                // Tomo la fila actual del dgvUsuarios
                DataGridViewRow row = dgvUsuarios.CurrentRow;
                // Si estoy modificanco un usuario le paso al nuevo usuario los datos del ID del
                // Usuario que estoy modificando
                NuevoUsuario.IdUsuario = Convert.ToInt32(row.Cells["Codigo"].Value);
                NuevoUsuario.Usuario = txtUsuario.Text.ToUpper();
                NuevoUsuario.Nivel = Convert.ToInt32(txtNivel.Text);
                NuevoUsuario.Clave = txtContra.Text;
                // Verifico si el nuevo usuario es activo o inactivo. G.
                if (chkActivo.Checked == true)
                {
                    NuevoUsuario.Activo = true;
                }
                else
                {
                    NuevoUsuario.Activo = false;
                }
                //VALIDAR EL OBJETO Y VER SI HAY ERRORES. N.
                string[] cErrores = NuevoUsuario.cValidaUsuario();
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

                // No se puede cargar un nuevo usuario inactivo
                if (clsGlobales.myEstado == "A" && NuevoUsuario.Activo == false)
                {
                    // Informo que no se puede cargar un usuario inactivo
                    MessageBox.Show("No se puede cargar un nuevo usuario inactivo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Pongo el foco en en chekbox
                    chkActivo.Focus();
                    // Salgo del evento
                    return;
                }
                else
                {
                    // Cambio el valor True o False a 0 o 1 para pasar el string a la BD
                    int Act = 0;
                    if (NuevoUsuario.Activo)
                    {
                        Act = 1;
                    }
                    //TEST
                    switch (clsGlobales.myEstado)
                    {
                        case "A":
                            
                            // Cadena que me inserta los datos del nuevo usuario en la tabla
                            myCadenaSQL = "INSERT INTO Usuarios (Usuario,Clave,Nivel,Activo)" +
                                               "values ('" + NuevoUsuario.Usuario + "','" + NuevoUsuario.Clave + "'," +
                                               NuevoUsuario.Nivel + "," + Act + ")";
                            // Ejecuto la consulta
                            clsDataBD.GetSql(myCadenaSQL);
                            // Lleno nuevamente la grilla
                            CargarUsuarios("", "");
                            //Cambiar Estado
                            clsGlobales.myEstado = "C";
                            // Activo todos los botones
                            ActivarBotones();
                            // Habilito los controles
                            HabilitarControles();

                            //*** EJECUTO STORE PROCEDURE QUE ME CREA LA CONFIGURACION EN BLANCO
                            //*** PARA EL NUEVO USUARIO
                            // Cadena que me inserta los datos del nuevo usuario en la tabla
                            myCadenaSQL = "exec NuevoUserPermisos " + clsDataBD.RetornarMax("Usuarios", "IdUsuario");
                            // Ejecuto la consulta
                            clsDataBD.GetSql(myCadenaSQL);
                            
                            //Retorno*/
                            break;

                        case "B":
                            // Cambio mi estado
                            clsGlobales.myEstado = "C";
                            // Activo todos los botones
                            ActivarBotones();
                            // Habilito los controles
                            HabilitarControles();
                            this.btnCancelar.Visible = true;
                            break;

                        case "M":
                            // Cadena que me inserta los datos del usuario mofificado a la tabla
                            myCadenaSQL = "UPDATE Usuarios SET Usuario= '" + NuevoUsuario.Usuario +
                                              "', Clave= '" + NuevoUsuario.Clave + "', Nivel= " + NuevoUsuario.Nivel +
                                              ", Activo = " + Act + " WHERE IdUsuario = " + NuevoUsuario.IdUsuario;
                            // Ejecuto la consulta
                            clsDataBD.GetSql(myCadenaSQL);
                            //Cambiar Estado
                            clsGlobales.myEstado = "C";
                            // Grabo la nueva Imagen del Usuario en la tabla, pasando el Id del Usuario 
                            // GrabarImagen(NuevoUsuario.IdUsuario);
                            // Lleno nuevamente la grilla
                            CargarUsuarios("", "");
                            // Activo todos los botones
                            ActivarBotones();
                            // Habilito los controles
                            HabilitarControles();

                            break;
                    }
                    
                }

                //clsGlobales.gbBandera = true;
            }

            else
            {
                // Informo que la confirmación de la contraseña es incorrecta
                MessageBox.Show("La verificación de la contraseña es incorrecta. Intentelo de nuevo.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Dejo en blanco los campos de las contraseñas
                this.txtContra.Text = "";
                this.txtContraConfirma.Text = "";
                // Pongo el foco en el texbox de la contraseña
                this.txtContra.Focus();
                // Salgo de evento
                return;
            }
        }

        #endregion

        #region Evento Click del botón Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Tomo la posición de la fila desde donde se llamo
            indexFila = dgvUsuarios.CurrentRow.Index;
            // Cambio mi estado a búsqueda
            clsGlobales.myEstado = "B";
            // Habilito los controles
            HabilitarControles();
            // Activo los botones
            ActivarBotones();
            // Limpio los campos de búsqueda
            LimpiarControlesBusqueda();
            // Pongo el foco en el primer control de búsqueda
            txtBuscarUsuario.Focus();

        }

        #endregion

        #region Evento Click del botón Salir

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Cierro el formulario
            this.Close();
        }

        #endregion

        #endregion

        #region Eventos de los controles

        private void txtBuscarUsuario_TextChanged(object sender, EventArgs e)
        {
            //QUE NO ESTE VACIO
            if (!(txtBuscarUsuario.Text == ""))
            {
                CargarUsuarios(txtBuscarUsuario.Text.ToUpper(), "Usuario");
            }
        }

        #endregion

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Cambio el estado del formulario a agregar. G.
            clsGlobales.myEstado = "C";
            // Limpio los controles del formulario. G.    
            ActivarBotones();
            // Habilito los controles para este estado. G.
            HabilitarControles();
            // Cargo de nuevo los usuarios
            CargarUsuarios("", "");
            // Para evitar que los controles queden vacíos cuando cancelo, 
            AlCambiarUsuarioEnGrilla();
            // Posiciono el foco en la fila desde donde se llamo
            PosicionarFocoFila();
        }

        #endregion



        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            // Cambio el estado del formulario a agregar. G.
            clsGlobales.myEstado = "C";
            // Limpio los controles del formulario. G.    
            ActivarBotones();
            // Habilito los controles para este estado. G.
            HabilitarControles();
            // Cargo de nuevo los usuarios
            CargarUsuarios("", "");
            // Para evitar que los controles queden vacíos cuando cancelo, 
            AlCambiarUsuarioEnGrilla();
            // Posiciono el foco en la fila desde donde se llamo
            PosicionarFocoFila();
        }

    }
}

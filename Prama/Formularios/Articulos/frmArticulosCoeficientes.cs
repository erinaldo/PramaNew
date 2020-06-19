using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace Prama
{
    public partial class frmArticulosCoeficientes : Form
    {
        int nSnapShot = 0; //0 ABM, 1 Solo Vista

        public frmArticulosCoeficientes(int p_Snapshot)
        {
            InitializeComponent();
            nSnapShot = p_Snapshot;
        }

        #region Métodos del Formulario

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
                    this.btnImprimir.TabStop = false;
                    this.btnImprimir.Visible = false;
                    return;
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
                    this.btnImprimir.TabStop = false;
                    this.btnImprimir.Visible = false;
                    return;
                case "C":
                    this.btnAgregar.TabStop = true;
                    this.btnAgregar.Visible = true;
                    this.btnModificar.TabStop = true && (dgvCoeficientes.RowCount != 0);
                    this.btnModificar.Visible = true && (dgvCoeficientes.RowCount != 0);
                    this.btnAceptar.TabStop = false;
                    this.btnAceptar.Visible = false;
                    this.btnCancelar.TabStop = false;
                    this.btnCancelar.Visible = false;
                    this.btnSalir.TabStop = true;
                    this.btnSalir.Visible = true;
                    // Si el nivel del usuario es el 5, habilito el botón borrar
                    if (clsGlobales.UsuarioLogueado.Nivel >=clsGlobales.cParametro.NivelBaja)
                    {
                        this.btnBorrar.TabStop = true && (dgvCoeficientes.RowCount != 0);
                        this.btnBorrar.Visible = true && (dgvCoeficientes.RowCount != 0);
                    }
                    else
                    {
                        this.btnBorrar.TabStop = false;
                        this.btnBorrar.Visible = false;
                    }
                    this.btnImprimir.TabStop = true && (dgvCoeficientes.RowCount != 0);
                    this.btnImprimir.Visible = true && (dgvCoeficientes.RowCount != 0);

                    if (this.nSnapShot == 1) //Solo Vista
                    {
                        this.btnAgregar.TabStop = false;
                        this.btnAgregar.Visible = false;
                        this.btnModificar.TabStop = false;
                        this.btnModificar.Visible = false;
                        this.btnImprimir.TabStop = false;
                        this.btnImprimir.Visible = false;
                        this.btnBorrar.TabStop = false;
                        this.btnBorrar.Visible = false;
                    }

                    return;
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
            switch (clsGlobales.myEstado)
            {
                case "A":
                case "M":
                    this.txtCoeficiente.TabStop = true;
                    this.txtCoeficiente.Enabled = true;
                    this.txtPublico.TabStop = true;
                    this.txtPublico.Enabled = true;
                    this.txtDistribuidor.TabStop = true;
                    this.txtDistribuidor.Enabled = true;
                    this.txtRevendedor.TabStop = true;
                    this.txtRevendedor.Enabled = true;
                    this.dgvCoeficientes.TabStop = false;
                    this.dgvCoeficientes.Enabled = false;
                    return;
                
                case "C":
                    this.txtCoeficiente.TabStop = false;
                    this.txtCoeficiente.Enabled = false;
                    this.txtPublico.TabStop = false;
                    this.txtPublico.Enabled = false;
                    this.txtDistribuidor.TabStop = false;
                    this.txtDistribuidor.Enabled = false;
                    this.txtRevendedor.TabStop = false;
                    this.txtRevendedor.Enabled = false;
                    this.dgvCoeficientes.TabStop = true && (dgvCoeficientes.RowCount > 0);
                    this.dgvCoeficientes.Enabled = true && (dgvCoeficientes.RowCount > 0);
                    return;
            }
        }
        #endregion

        #region Método para limpiar los controles del formulario
        //LIMPIA LA PROPIEDAD TEXT DE LOS CONTROLES DEL FORMULARIO. N.
        private void LimpiarControlesForm()
        {
            this.txtCoeficiente.Text = "";
            this.txtPublico.Text = "";
            this.txtDistribuidor.Text = "";
            this.txtRevendedor.Text = "";
        }
        #endregion

        #region Método para cargar la grilla

        private void CargarCoeficientes()
        {
            // Variable para la cadena SQL
            string myCadena = "";
            // Cadena SQL 
            myCadena = "select * from CoeficientesArticulos where Activo = 1";
            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            mDtTable = clsDataBD.GetSql(myCadena);
            // Evito que el dgv genere columnas automáticas
            dgvCoeficientes.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvCoeficientes.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvCoeficientes.Rows.Count;
            // Posiciono la grilla en la última fila
            // dgvCoeficientes.CurrentCell = dgvCoeficientes[1, Filas - 1];

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
        }

        #endregion

        #endregion

        #region Eventos del Formulario

        #region Evento Load del formulario

        private void frmArticulosCoeficientes_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 
			
            clsGlobales.myEstado = "C";
            // Llamo al método cargar Localidades para rellenar la grilla. G.
            CargarCoeficientes();
            // Llamo al método activar los botones del formulario. G.
            ActivarBotones();
            // Llamo al método habilitar controles del formulario. G.
            HabilitarControles();
            // Cargar ToolTips
            CargarToolTips();
            //Titulo Ventana
            this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;
        }

        #endregion

        #region Eventos de la Grilla

        #region Evento CellContentClick de la grilla

        private void dgvCoeficientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvCoeficientes.RowCount == 0)
            {
                return;
            }
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvCoeficientes.CurrentRow;
            // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
            //TRAER LOS DATOS. H.
            txtCoeficiente.Text = row.Cells["CoeficienteArticulo"].Value.ToString();
            txtPublico.Text = row.Cells["CoeficientePublico"].Value.ToString();
            txtDistribuidor.Text = row.Cells["CoeficienteDistribuidor"].Value.ToString();
            txtRevendedor.Text = row.Cells["CoeficienteRevendedor"].Value.ToString();
        }

        #endregion

        #region Evento SelectionChanged de la grilla

        private void dgvCoeficientes_SelectionChanged(object sender, EventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvCoeficientes.RowCount == 0)
            {
                return;
            }
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvCoeficientes.CurrentRow;
            // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
            //TRAER LOS DATOS. H.
            txtCoeficiente.Text = row.Cells["CoeficienteArticulo"].Value.ToString();
            txtPublico.Text = row.Cells["CoeficientePublico"].Value.ToString();
            txtDistribuidor.Text = row.Cells["CoeficienteDistribuidor"].Value.ToString();
            txtRevendedor.Text = row.Cells["CoeficienteRevendedor"].Value.ToString();
        }

        #endregion

        #region Evento KeyDown de la grilla

        private void dgvCoeficientes_KeyDown(object sender, KeyEventArgs e)
        {
            //PRESIONO DEL?
            if (e.KeyCode == Keys.Delete)
            {
                this.btnBorrar.PerformClick(); //LLAMAR A EVENTO CLICK DEL BOTON BORRAR
            }
        }

        #endregion

        #endregion

        #region Eventos de los botones

        #region Evento Click del botón Agregar

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Pregunto si el usuario actual tiene nivel mayor a 2, lo dejo agregar. G.
            if (clsGlobales.UsuarioLogueado.Nivel > 2)
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
                txtCoeficiente.Focus();
            }
            // Pregunto si el usuario actual tiene nivel menor o igual a 2, No lo dejo agregar. G.
            else
            {
                // vuelvo el formulario al estado anterior. G.
                clsGlobales.myEstado = "C";
                // El usuario no es de nivel 3, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para crear un nuevo Coeficiente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento Click del botón Modificar

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //TRAER LA FILA ACTUAL. N.
            DataGridViewRow row = dgvCoeficientes.CurrentRow;
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvCoeficientes.RowCount == 0)
            {
                // Salgo de la rutina. G.
                return;
            }
            // // Pregunto si el usuario actual tiene nivel mayor a 2, lo dejo agregar. G.
            if (clsGlobales.UsuarioLogueado.Nivel > 2)
            {
                // Cambio el estado del formulario a Modificar. G.
                clsGlobales.myEstado = "M";
                // Activo los botones para este estado. G.
                ActivarBotones();
                // Habilito los controles para este estado. G.
                HabilitarControles();
                // Cargo los datos de la contraseña en sus correspondientes textbox. G.
                txtCoeficiente.Text = row.Cells["CoeficienteArticulo"].Value.ToString();
                txtPublico.Text = row.Cells["CoeficientePublico"].Value.ToString();
                txtDistribuidor.Text = row.Cells["CoeficienteDistribuidor"].Value.ToString();
                txtRevendedor.Text = row.Cells["CoeficienteRevendedor"].Value.ToString();
                // Pongo el foco en el primer texbox
                txtCoeficiente.Focus();
            }
            else
            {
                // Pregunto si el usuario actual tiene nivel menor o igual a 2, No lo dejo agregar. G.
                clsGlobales.myEstado = "C";
                // El usuario no es de nivel 3, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para modificar un Coeficiente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento Click del botón Borrar

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            // Pregunto si el usuario actual es el mismo que se quiere modificar. G.
            if (clsGlobales.UsuarioLogueado.Nivel >=clsGlobales.cParametro.NivelBaja)
            {
                //TRAER LA FILA ACTUAL. N.
                DataGridViewRow row = dgvCoeficientes.CurrentRow;
                // Creo una variable local para que me almacene el estado Activo o inactivo del Usuario. G.
                int Id = Convert.ToInt32(row.Cells["IdCoeficienteArticulo"].Value);
                string Coef = row.Cells["CoeficienteArticulo"].Value.ToString();
                int Niv = clsGlobales.UsuarioLogueado.Nivel;

               if (Niv < clsGlobales.cParametro.NivelBaja)
                {
                    // El usuario no es de nivel 5, por lo que no puede crear un nuevo usuario. G.
                    MessageBox.Show("Usted no tiene los permisos para Eliminar este Coeficiente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Ejecuto el botón cancelar. G.
                    this.btnCancelar.PerformClick();
                }
                else
                {
                    // Confirma eliminar el registro ?
                    DialogResult dlResult = MessageBox.Show("Desea Eliminar el Coeficiente " + Coef + " ?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    // Si confirma salir de la aplicación....
                    if (dlResult == DialogResult.Yes)
                    {
                        string myCadena = "UPDATE CoeficientesArticulos SET Activo = 0 WHERE CoeficientesArticulos.IdCoeficienteArticulo =" + Id;
                        clsDataBD.GetSql(myCadena);
                        // Refresco la grilla
                        CargarCoeficientes();
                    }
                }

            }
            else
            {
                // El usuario no es de nivel 5, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para Eliminar un Coeficiente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ejecuto el botón cancelar. G.
                this.btnCancelar.PerformClick();
            }
        }

        #endregion

        #region Evento Click del botón Aceptar

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Si la grilla no contiene ninguna fila, salgo del evento
            if (dgvCoeficientes.RowCount == 0)
            {
                // Muestro un mensaje indicando que no se encontraron los datos
                MessageBox.Show("No se encontraron coincidencias", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Regreso el formulario a su estado inicial
                this.btnCancelar.PerformClick();
                return;
            }
            // Creo la cadena para grabar las Modificaciones de la Localidad
            string myCadena = "";
            // Creo una nueva Localidad. G.
            clsArticulosCoeficientes NuevoCoeficiente = new clsArticulosCoeficientes();
            // Tomo la línea actual de la grilla. G.
            DataGridViewRow row = dgvCoeficientes.CurrentRow;
            // Verifico el estado del formulario para saber si estoy creando o modificando una Localidad. G.
            // Paso los datos del formulario a la nueva Localidad
            NuevoCoeficiente.IdCoeficienteArticulo = Convert.ToInt32(row.Cells["IdCoeficienteArticulo"].Value);
            NuevoCoeficiente.CoeficienteArticulo = txtCoeficiente.Text;
            NuevoCoeficiente.CoeficientePublico = Convert.ToDouble(txtPublico.Text);
            NuevoCoeficiente.CoeficienteDistribuidor = Convert.ToDouble(txtDistribuidor.Text);
            NuevoCoeficiente.CoeficienteRevendedor = Convert.ToDouble(txtRevendedor.Text);


            //Vector Errores
            string[] cErrores = NuevoCoeficiente.cValidaCoeficiente();
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

            switch (clsGlobales.myEstado)
            {
                case "A":

                    //Alta, activar
                    NuevoCoeficiente.Activo = 1;

                    // Creo la cadena para grabar el alta de la Localidad
                    myCadena = "INSERT INTO CoeficientesArticulos (CoeficienteArticulo," +
                                                                  "CoeficientePublico," +
                                                                  "CoeficienteDistribuidor," +
                                                                  "CoeficienteRevendedor," +
                                                                  "Activo) values ('"
                                                                                    + NuevoCoeficiente.CoeficienteArticulo + "', "
                                                                                    + NuevoCoeficiente.CoeficientePublico.ToString().Replace(",", ".") + ", "
                                                                                    + NuevoCoeficiente.CoeficienteDistribuidor.ToString().Replace(",", ".") + ", "
                                                                                    + NuevoCoeficiente.CoeficienteRevendedor.ToString().Replace(",", ".") + ", "
                                                                                    + NuevoCoeficiente.Activo + ")";

                    // Ejecuto la consulta SQL
                    clsDataBD.GetSql(myCadena);
                    // Lleno nuevamente la grilla
                    CargarCoeficientes();
                    // Regreso el formulario a su estado inicial
                    this.btnCancelar.PerformClick();
                    return;

                case "M":
                    // Creo la cadena para grabar las Modificaciones de la Localidad
                    myCadena = "UPDATE CoeficientesArticulos SET CoeficienteArticulo = '" + NuevoCoeficiente.CoeficienteArticulo + "'," +
                                                                 "CoeficientePublico = " + NuevoCoeficiente.CoeficientePublico.ToString().Replace(",", ".") + ", " +
                                                                 "CoeficienteDistribuidor = " + NuevoCoeficiente.CoeficienteDistribuidor.ToString().Replace(",", ".") + ", " +
                                                                 "CoeficienteRevendedor= " + NuevoCoeficiente.CoeficienteRevendedor.ToString().Replace(",", ".") +
                                                                 " WHERE IdCoeficienteArticulo = " + NuevoCoeficiente.IdCoeficienteArticulo;
                    // Ejecuto la consulta SQL
                    clsDataBD.GetSql(myCadena);
                    // Lleno nuevamente la grilla
                    CargarCoeficientes();
                    // Regreso el formulario a su estado inicial
                    this.btnCancelar.PerformClick();
                    // Actualizo los campos en base a mi posición de la grilla
                    this.dgvCoeficientes_CellContentClick(dgvCoeficientes, new DataGridViewCellEventArgs(0, 0));
                    // Salgo del case
                    return;
            }
        }

        #endregion

        #region Evento Click del botón Cancelar

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Cambio el estado del formulario a agregar. G.
            clsGlobales.myEstado = "C";
            // Cargo las localidades
            CargarCoeficientes();
            // Limpio los controles del formulario. G.    
            ActivarBotones();
            // Habilito los controles para este estado. G.
            HabilitarControles();
            // Actualizo los campos en base a mi posición de la grilla
            this.dgvCoeficientes_CellContentClick(dgvCoeficientes, new DataGridViewCellEventArgs(0, 0));
        }

        #endregion

        #region Evento Click del botón Salir

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Cierro el formulario
            this.Close();
        }

        #endregion

        #region Evento Click BtnImprimir

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //Data Set
            dsReportes oDsCoef = new dsReportes();

            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = dgvCoeficientes.Rows.Count;

            for (int i = 0; i < dgvFilas; i++)
            {
                oDsCoef.Tables["DtCoef"].Rows.Add
                (new object[] { dgvCoeficientes[1,i].Value.ToString(),
                dgvCoeficientes[2,i].Value.ToString(),
                dgvCoeficientes[3,i].Value.ToString(),
                dgvCoeficientes[4,i].Value.ToString()});

            }

            //Objeto Reporte
            rptCoeficientes oRepCoef = new rptCoeficientes();
            //Cargar Reporte            
            //    oRepTipoCli.Load(Application.StartupPath + "\\rptTipoCli.rpt");
            oRepCoef.Load(Application.StartupPath + "\\rptCoeficientes.rpt");
            //Establecer el DataSet como DataSource
            oRepCoef.SetDataSource(oDsCoef);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepCoef;
            oRepCoef.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepCoef.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepCoef.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
            oRepCoef.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
            oRepCoef.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
            oRepCoef.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
            oRepCoef.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepCoef.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
            oRepCoef.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();
        }

        #endregion
        
        #endregion

        #region Eventos de los controles

        #region Eventos Leave
        
        // txtCoeficiente
        private void txtCoeficiente_Leave(object sender, EventArgs e)
        {
            // Pongo en matúsculas el contenido del textbox
            txtCoeficiente.Text = txtCoeficiente.Text.ToUpper();
        }

        // txtPublico
        private void txtPublico_Leave(object sender, EventArgs e)
        {
            /*this.txtPublico.Text = txtPublico.Text.Replace(",", ".");
            this.txtPublico.Text = Convert.ToDouble(txtPublico.Text, CultureInfo.InvariantCulture).ToString("#0.00", CultureInfo.InvariantCulture);*/
        }

        // txtDistribuidor
        private void txtDistribuidor_Leave(object sender, EventArgs e)
        {
            //this.txtDistribuidor.Text = txtDistribuidor.Text.Replace(",", ".");
            //this.txtDistribuidor.Text = Convert.ToDouble(txtDistribuidor.Text, CultureInfo.InvariantCulture).ToString("#0.00", CultureInfo.InvariantCulture);
        }

        // txtRevendedor
        private void txtRevendedor_Leave(object sender, EventArgs e)
        {
            //this.txtRevendedor.Text = txtRevendedor.Text.Replace(",", ".");
            //this.txtRevendedor.Text = Convert.ToDouble(txtRevendedor.Text, CultureInfo.InvariantCulture).ToString("#0.00", CultureInfo.InvariantCulture);
        }

        #endregion

        #endregion

        #region Eventos KeyPress

        #region txtPublico

        private void txtPublico_KeyPress(object sender, KeyPressEventArgs e)
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
                // Pongo el foco en el siguiente control
                this.txtDistribuidor.Focus();
            }
        }

        #endregion

        #region txtDistribuidor

        private void txtDistribuidor_KeyPress(object sender, KeyPressEventArgs e)
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
                // Pongo el foco en el siguiente control
                this.txtRevendedor.Focus();
            }
        }

        #endregion

        #region txtRevendedor

        private void txtRevendedor_KeyPress(object sender, KeyPressEventArgs e)
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
                // Pongo el foco en el siguiente control
                this.btnAceptar.Focus();
            }
        }

        #endregion

        #endregion

        #endregion        

    }

    
}

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
    public partial class frmArticulosActualizacion : Form
    {

        #region Variables del Formulario

        // Variable que almacena las cadenas de conexión SQL
        string myCadenaSQL = "";
        // Variable que almacena el estado del formulario
        string myEstado = "";
        // Bandera que controla el comportamiento de los combos
        bool myBanderaCombos = false;
        // Bandera que controla el comportamiento de la grilla
        bool myBanderaGrilla = false;
        // Bandera que controla si es una nueva actualización o un deshacer una actualización anterior
        bool myBanderaNueva = false;

        // Instancio un objeto de la clase ArticulosCotizacion
        clsArticulosActualizacion myActualizacion = new clsArticulosActualizacion();

        #endregion

        #region Contructor del formulario

        public frmArticulosActualizacion()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos del Formulario

        #region Eventos de los botones

        #region Evento Click del botón Agregar

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Cambio el estado del formulario
            myEstado = "A";
            // Habilito los controles
            HabilitarControles();
            // Habilito los botones
            HabilitarBotones();
            // Pongo el foco en el primer combo
            cboRubros.Focus();

        }

        #endregion

        #region Evento CLick del botón deshacer

        private void btnDeshacer_Click(object sender, EventArgs e)
        {
            // Seteo el valor de la bandera que controla si es una nueva actualización
            myBanderaNueva = false;
            // Paso los datos a la clase
            SetearClase();

            // Variable que almacena el cálculo que hay que hacer sobre los precios dependiendo si es aumento o descuento
            double myOperacion = 0;
            // Variable que almacena el símbolo de la operación a realizar
            bool mySimbolo = false;
            // verifico si la actualización es un aumento 
            if (!(myActualizacion.Aumento == 0))
            {
                // Paso a la variable el valor por el que hay que operar el Precio
                myOperacion = 1 + myActualizacion.Aumento / 100;
                // Paso a la variable la opración que hay que realizar 
                mySimbolo = false;
            }
            else
            {
                // Paso a la variable el valor por el que hay que operar el Precio
                myOperacion = 1 + myActualizacion.Descuento / 100;
                // Paso a la variable la opración que hay que realizar 
                mySimbolo = true;
            }


            // Armo la cadena SQL Básica
            myCadenaSQL = "select * from Articulos where IdSubrubroArticulo in ";
            // Si existe un subrubro, armo la cadena SQL
            if (!(myActualizacion.IdSubrubroArticulo == 0))
            {
                myCadenaSQL = myCadenaSQL + "(" + myActualizacion.IdSubrubroArticulo + ")";
            }
            else
            {
                string mySubRubros = RetornarSubrubros(myActualizacion.IdRubroArticulo);

                myCadenaSQL = myCadenaSQL + "(" + mySubRubros + ")";
            }
            // Paso los datos de los artículos afectados por la actualización a una tabla en memoria
            DataTable myTabla = clsDataBD.GetSql(myCadenaSQL);

            // Pido al operador la confirmación de deshacer la actualización
            DialogResult myRespuesta = MessageBox.Show("Esta seguro que desea deshacer la actualización de " + myTabla.Rows.Count + " Artículos ?", "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Si la respuesta es si
            if (myRespuesta == DialogResult.Yes)
            {
                // Variable que almacena la cadena para ctualizar el pecio de los artículos
                string myCadenaActualizar = "";
                // Variable que almacena el nuevo precio
                double NuevoPrecio = 0;
                // Recorro la tabla y comienzo a deshacer la actualización de precios
                foreach (DataRow row in myTabla.Rows)
                {
                    if (mySimbolo)
                    {
                        // Tomo en una variable el precio actual del artículo y ejecuto la operación
                        NuevoPrecio = Convert.ToDouble((Convert.ToDouble(row["Precio"]) * myOperacion).ToString("#0.00000"));
                    }
                    else
                    {
                        // Tomo en una variable el precio actual del artículo y ejecuto la operación
                        NuevoPrecio = Convert.ToDouble((Convert.ToDouble(row["Precio"]) / myOperacion).ToString("#0.00000"));
                    }
                   
                    // Armo la cadena SQL para actualizar los precios
                    myCadenaActualizar = "Update Articulos set Precio = " + NuevoPrecio + " where IdArticulo = " + Convert.ToInt32(row["IdArticulo"]);
                    // Ejecuto la cadena SQL para actualizar al precio anterior
                    clsDataBD.GetSql(myCadenaActualizar);
                    // Armo la cadena SQL para actualizar los precios
                    myCadenaActualizar = "Update Insumos set Costo = " + NuevoPrecio + " where IdArticulo = " + Convert.ToInt32(row["IdArticulo"]);
                    // Ejecuto la cadena SQL para actualizar al precio anterior
                    clsDataBD.GetSql(myCadenaActualizar);

                }
                // Variable que almacena la cadena para pasar a inactivo la actualización después de deshacer
                string aInactivo = "update ArticulosActualizaciones set Activo = 0 where IdArticuloActualizacion = " + myActualizacion.IdArticuloActualizacion;
                // Paso a inactivo la actualización que se deshizo
                clsDataBD.GetSql(aInactivo);
                // Limpio y cargo todo de nuevo
                CancelarTodo();
                // Ejecuto el evento Click de la grilla para que se controle el estado del botón deshacer
                this.dgvActualizaciones_Click(sender, e);
            }
            else
            {
                // Limpio y cargo todo de nuevo
                CancelarTodo();
                // Ejecuto el evento Click de la grilla para que se controle el estado del botón deshacer
                this.dgvActualizaciones_Click(sender, e);
            }

        }

        #endregion

        #region Evento Click del botón Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Cambio el estado del formulario
            myEstado = "B";
            // Habilito los controles
            HabilitarControles();
            // Habilito los botones
            HabilitarBotones();
            // seteo la fecha máxima del dtp
            dtpFecha.MaxDate = DateTime.Now;
            // Pongo el foco en el dtp
            dtpFecha.Focus();
        }

        #endregion

        #region Evento Click del botón Aceptar

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Valido que los datos estén cargados
            if (cboRubros.SelectedIndex == -1 || (Convert.ToDouble(txtAumento.Text) == 0 && Convert.ToDouble(txtDescuento.Text) == 0))
            {
                // Informo que faltan datos
                MessageBox.Show("Debe seleccionar un rubro y los campos Aumento y descuento deben ser mayor a 0", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Pongo el foco en el primer combo
                cboRubros.Focus();
            }
            else
            {
                // Cambio el estado de la bandera que controla si es una nueva actualización
                myBanderaNueva = true;
                // Cargo los datos a la clase
                SetearClase();
                // Variable que almacena el cálculo que hay que hacer sobre los precios dependiendo si es aumento o descuento
                double myOperacion = 0;
                // Variable que almacena el símbolo de la operación a realizar
                bool mySimbolo = false;
                // verifico si la actualización es un aumento 
                if (!(myActualizacion.Aumento == 0))
                {
                    // Paso a la variable el valor por el que hay que operar el Precio
                    myOperacion = 1 + myActualizacion.Aumento / 100;
                    // Paso a la variable la opración que hay que realizar 
                    mySimbolo = true;
                }
                else
                {
                    // Paso a la variable el valor por el que hay que operar el Precio
                    myOperacion = 1 + myActualizacion.Descuento / 100;
                    // Paso a la variable la opración que hay que realizar 
                    mySimbolo = false;
                }

                // Armo la cadena SQL Básica
                myCadenaSQL = "select * from Articulos where IdSubrubroArticulo in ";
                // Si existe un subrubro, armo la cadena SQL
                if (!(myActualizacion.IdSubrubroArticulo == 0))
                {
                    myCadenaSQL = myCadenaSQL + "(" + myActualizacion.IdSubrubroArticulo + ")";
                }
                else
                {
                    string mySubRubros = RetornarSubrubros(myActualizacion.IdRubroArticulo);

                    myCadenaSQL = myCadenaSQL + "(" + mySubRubros + ")";
                }
                // Paso los datos de los artículos afectados por la actualización a una tabla en memoria
                DataTable myTabla = clsDataBD.GetSql(myCadenaSQL);

                // Pido al operador la confirmación de deshacer la actualización
                DialogResult myRespuesta = MessageBox.Show("Esta seguro que desea Actualizar " + myTabla.Rows.Count + " Artículos ?", "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Si la respuesta es si
                if (myRespuesta == DialogResult.Yes)
                {
                    // Variable que almacena la cadena para ctualizar el pecio de los artículos
                    string myCadenaActualizar = "";
                    // Variable que almacena el nuevo precio
                    double NuevoPrecio = 0;
                    // Recorro la tabla y comienzo a deshacer la actualización de precios
                    foreach (DataRow row in myTabla.Rows)
                    {
                        if (mySimbolo)
                        {
                            // Tomo en una variable el precio actual del artículo y ejecuto la operación
                            NuevoPrecio = Convert.ToDouble((Convert.ToDouble(row["Precio"]) * myOperacion).ToString("#0.00000"));
                        }
                        else
                        {
                            // Tomo en una variable el precio actual del artículo y ejecuto la operación
                            NuevoPrecio = Convert.ToDouble((Convert.ToDouble(row["Precio"]) / myOperacion).ToString("#0.00000"));
                        }

                        // Armo la cadena SQL para actualizar los precios
                        myCadenaActualizar = "Update Articulos set Precio = " + NuevoPrecio + " where IdArticulo = " + Convert.ToInt32(row["IdArticulo"]);
                        // Ejecuto la cadena SQL para actualizar al precio anterior
                        clsDataBD.GetSql(myCadenaActualizar);
                        // Armo la cadena SQL para actualizar los precios
                        myCadenaActualizar = "Update Insumos set Costo = " + NuevoPrecio + " where IdArticulo = " + Convert.ToInt32(row["IdArticulo"]);
                        // Ejecuto la cadena SQL para actualizar al precio anterior
                        clsDataBD.GetSql(myCadenaActualizar);

                    }

                    // Variable que almacena la cadena para insertar los datos de la nueva actualización
                    string NuevaActualizacion = "";
                    // Si se seleccionó un subrubro
                    if (!(myActualizacion.IdSubrubroArticulo.ToString() == ""))
                    {
                        // Variable que almacena la cadena para pasar a inactivo la actualización después de deshacer
                        NuevaActualizacion = "insert into ArticulosActualizaciones" +
                        " (Fecha, IdRubroArticulo, IdSubrubroArticulo, Aumento, Descuento, IdUsuario, Activo) values ('" +
                        myActualizacion.Fecha + "'," +
                        myActualizacion.IdRubroArticulo + "," +
                        myActualizacion.IdSubrubroArticulo + "," +
                        myActualizacion.Aumento + "," +
                        myActualizacion.Descuento + "," +
                        myActualizacion.IdUsuario + "," +
                        myActualizacion.Activo + ")";
                    }
                    // Si no se seleccionó subRubo
                    else
                    {
                        // Variable que almacena la cadena para pasar a inactivo la actualización después de deshacer
                        NuevaActualizacion = "insert into ArticulosActualizaciones" +
                        " (Fecha, IdRubroArticulo, Aumento, Descuento, IdUsuario, Activo) values ('" +
                        myActualizacion.Fecha + "'," +
                        myActualizacion.IdRubroArticulo + "," +
                        myActualizacion.Aumento + "," +
                        myActualizacion.Descuento + "," +
                        myActualizacion.IdUsuario + "," +
                        myActualizacion.Activo + ")";
                    }
                                               
                    // Paso a inactivo la actualización que se deshizo
                    clsDataBD.GetSql(NuevaActualizacion);
                    // Limpio y cargo todo de nuevo
                    CancelarTodo();
                    // Ejecuto el evento Click de la grilla para que se controle el estado del botón deshacer
                    this.dgvActualizaciones_Click(sender, e);
                }
                else
                {
                    // Limpio y cargo todo de nuevo
                    CancelarTodo();
                    // Ejecuto el evento Click de la grilla para que se controle el estado del botón deshacer
                    this.dgvActualizaciones_Click(sender, e);
                }
            }

        }

        #endregion

        #region Evento CLick del botón Cancelar

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CancelarTodo();
            // Ejecuto el evento Click de la grilla para que se controle el estado del botón deshacer
            this.dgvActualizaciones_Click(sender, e);
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

        #region Evento Load del Formualrio

        private void frmArticulosActualizacion_Load(object sender, EventArgs e)
        {
            //icon
            clsFormato.SetIconForm(this);   
			// Cambio el estado del formulario a en espera
            myEstado = "C";
            // Cargo los toolstip
            CargarToolTips();
            // Habilito los controles
            HabilitarControles();
            // Habilito los botones
            HabilitarBotones();
            // Cargo los combos
            CargarComboRubro();
            // Cargo la grilla con los datos de las actualizaciones
            CargarGrilla();
            // Ejecuto el evento Click de la grilla para que se controle el estado del botón deshacer
            this.dgvActualizaciones_Click(sender, e);
            //Titulo Ventana
            this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;
        }

        #endregion

        #region Eventos de los combos

        #region Evento Click del combo cboRubros

        private void cboRubros_Click(object sender, EventArgs e)
        {
            myBanderaCombos = true;
        }

        #endregion

        #region Evento SelectionIndexChanger del combo cboRubros

        private void cboRubros_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (myBanderaCombos)
            {
                CargarComboSubRubros(Convert.ToInt32(cboRubros.SelectedValue));
                // Vacío la selección del combo
                cboSubrubros.SelectedValue = -1;
            }
        }

        #endregion

        #endregion

        #region Eventos de los controles

        #region Evento KeyPress del txtAumento

        private void txtAumento_KeyPress(object sender, KeyPressEventArgs e)
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

            // Si se presionó Enter o Tab...
            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                // Pongo el foco en el botón aceptar
                btnAceptar.Focus();
            }
        }

        #endregion

        #region Evento KeyPress del txtDescuento

        private void txtDescuento_KeyPress(object sender, KeyPressEventArgs e)
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

            // Si se presionó Enter o Tab...
            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                // Pongo el foco en el botón aceptar
                btnAceptar.Focus();
            }
        }

        #endregion

        #region Evento Leave del txtAumento

        private void txtAumento_Leave(object sender, EventArgs e)
        {
            // pongo en una variable el valor ingresado
            double dAumento = Convert.ToDouble(txtAumento.Text);
            // Valido que el valor ingresado no sea mayor a 100 ni menor a 0
            if (!(dAumento > 100))
            {
                // Formateo el campo a la salida del mismo
                txtAumento.Text = Convert.ToDouble(txtAumento.Text).ToString("#0.00");
            }
            else
            {
                //muestro un mensaje indicando la condición de validación
                MessageBox.Show("El aumento no puede ser mayor a 100 %", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Vacío el campo
                txtAumento.Text = "0.00";
                // Devuelvo el foco al control
                txtAumento.Focus();
            }
            
        }

        #endregion

        #region Evento Leave del txtDescuento

        private void txtDescuento_Leave(object sender, EventArgs e)
        {
            // pongo en una variable el valor ingresado
            double dDescuento = Convert.ToDouble(txtDescuento.Text);
            // Valido que el valor ingresado no sea mayor a 100 ni menor a 0
            if (!(dDescuento > 100))
            {
                // Formateo el campo a la salida del mismo
                txtDescuento.Text = Convert.ToDouble(txtDescuento.Text).ToString("#0.00");
            }
            else
            {
                //muestro un mensaje indicando la condición de validación
                MessageBox.Show("El descuento no puede ser mayor a 100 % ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Vacío el campo
                txtDescuento.Text = "0.00";
                // Devuelvo el foco al control
                txtDescuento.Focus();
            }
        }

        #endregion

        #region Evento Enter del txtAumento

        private void txtAumento_Enter(object sender, EventArgs e)
        {
            // Pongo a 0 el campo Descuento
            txtDescuento.Text = "0.00";
        }

        #endregion

        #region Evento Enter del txtDescuento

        private void txtDescuento_Enter(object sender, EventArgs e)
        {
            // Pongo a 0 el campo Aumento
            txtAumento.Text = "0.00";
        }

        #endregion


        #endregion

        #region Evento ValueChanged del dtpFecha

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            // Variable que almacena la fecha seleccionada
            string myFecha = clsValida.ConvertirFecha(dtpFecha.Value);
            // Armo la cadena SQL para filtrar la búsqueda
            myCadenaSQL = "select * from Vista_ArticulosActualizaciones where Fecha like '" + myFecha + "'";
            // Ejecuto la consulta y la almaceno en una tabla
            DataTable myTabla = clsDataBD.GetSql(myCadenaSQL);
            // Asigno el source de la grilla
            dgvActualizaciones.DataSource = myTabla;
            // si la grilla tiene datos, muestro el botón deshacer
            if (dgvActualizaciones.Rows.Count > 0)
            {
                btnDeshacer.Visible = true;
                btnDeshacer.TabStop = true;
            }
            else
            {
                btnDeshacer.Visible = false;
                btnDeshacer.TabStop = false;
            }
        }

        #endregion

        #region Eventos de la grilla

        #region Evento SelectionChanged de la grilla

        private void dgvActualizaciones_SelectionChanged(object sender, EventArgs e)
        {
            // COntrolo si el botón Deshacer debe estar visible
            ControlarDeshacer();
        }

        #endregion

        #region Evento Click de la Grilla

        private void dgvActualizaciones_Click(object sender, EventArgs e)
        {
            // COntrolo si el botón Deshacer debe estar visible
            ControlarDeshacer();
        }

        #endregion

        #endregion

        #endregion

        #region Métodos del formulario

        #region Método que carga el combo de los Rubros

        private void CargarComboRubro()
        {
            // Cargo el combo de los rubros
            clsDataBD.CargarCombo(cboRubros, "RubrosArticulos", "RubroArticulo", "IdRubroArticulo");
            // Vacío la selección del combo
            cboRubros.SelectedIndex = -1;
        }

        #endregion

        #region Método que carga el combo de los subrubros en función de los Rubros

        private void CargarComboSubRubros(int rub)
        {
            // Si se seleccionó algún rubro
            if (!(rub == -1) || !(rub==0))
            {
                // Cargo el combo de subRubros en función del rubro
                clsDataBD.CargarCombo(cboSubrubros, "SubrubrosArticulos", "SubrubroArticulo", "IdSubrubroArticulo", "IdRubroArticulo = " + rub);
                
            }
        }

        #endregion

        #region Método que carga la grilla

        private void CargarGrilla()
        {
            // Armo la cadena SQL
            myCadenaSQL = "select * from Vista_ArticulosActualizaciones where Activo = 1 order by IdArticuloActualizacion";
            // Ejecuto la consulta y la almaceno en una tabla
            DataTable myTabla = clsDataBD.GetSql(myCadenaSQL);
            // Asigno al source de la grilla la tabla con los datos
            dgvActualizaciones.DataSource = myTabla;

            if (dgvActualizaciones.Rows.Count > 0)
            {
                // Posiciono el foco en la última fila de la grilla
                dgvActualizaciones.CurrentCell = dgvActualizaciones[1, dgvActualizaciones.Rows.Count - 1];
            }
        }

        #endregion

        #region Método que Carga los ToolTips

        private void CargarToolTips()
        {
            
            toolTip1.SetToolTip(this.btnAgregar, "Agregar");
            toolTip2.SetToolTip(this.btnDeshacer, "Deshacer");
            toolTip3.SetToolTip(this.btnBuscar, "Buscar");
            toolTip4.SetToolTip(this.btnImprimir, "Imprimir");
            toolTip5.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip6.SetToolTip(this.btnCancelar, "Cancelar");
            toolTip7.SetToolTip(this.btnSalir, "Salir");

        }

        #endregion

        #region Método que deshabilita los controles del formulario

        private void HabilitarControles()
        {
            switch (myEstado)
            {
                // En espera
                case "C":

                    // Grilla
                    dgvActualizaciones.Enabled = true;
                    dgvActualizaciones.TabStop = true;
                    dgvActualizaciones.Height = 195;
                    // Combos
                    cboRubros.Enabled = false;
                    cboRubros.TabStop = false;
                    cboSubrubros.Enabled = false;
                    cboSubrubros.TabStop = false;
                    // Txt
                    txtAumento.Enabled = false;
                    txtAumento.TabStop = false;
                    txtDescuento.Enabled = false;
                    txtDescuento.TabStop = false;
                    // dtp
                    dtpFecha.Visible = false;
                    dtpFecha.TabStop = false;
                    lblFecha.Visible = false;

                    break;

                // Alta
                case "A":

                    // Grilla
                    dgvActualizaciones.Enabled = false;
                    dgvActualizaciones.TabStop = false;
                    // Combos
                    cboRubros.Enabled = true;
                    cboRubros.TabStop = true;
                    cboSubrubros.Enabled = true;
                    cboSubrubros.TabStop = true;
                    // Txt
                    txtAumento.Enabled = true;
                    txtAumento.TabStop = true;
                    txtDescuento.Enabled = true;
                    txtDescuento.TabStop = true;
                    // dtp
                    dtpFecha.Visible = false;
                    dtpFecha.TabStop = false;
                    lblFecha.Visible = false;

                    break;
                // Búsqueda
                case "B":
                    // Cambio el tamaño de la grilla
                    dgvActualizaciones.Height = 168;
                    // dtp
                    dtpFecha.Visible = true;
                    dtpFecha.TabStop = true;
                    lblFecha.Visible = true;

                    break;
            }

        }

        #endregion

        #region Método que deshabilita los botones en función del estado del formulario

        private void HabilitarBotones()
        {
            switch (myEstado)
            {
                // En espera
                case "C":
                    // Habilito
                    btnAgregar.Visible = true;
                    btnAgregar.TabStop = true;
                    btnDeshacer.Visible = true && (dgvActualizaciones.Rows.Count >0);
                    btnDeshacer.TabStop = true && (dgvActualizaciones.Rows.Count > 0);
                    btnBuscar.Visible = true && (dgvActualizaciones.Rows.Count > 0);
                    btnBuscar.TabStop = true && (dgvActualizaciones.Rows.Count > 0);
                    btnImprimir.Visible = false;
                    btnImprimir.TabStop = false;
                    btnSalir.Visible = true;
                    btnSalir.TabStop = true;
                   // Deshabilito
                    btnAceptar.Visible = false;
                    btnAceptar.TabStop = false;
                    btnCancelar.Visible = false;
                    btnCancelar.TabStop = false;

                    break;

                // Alta
                case "A":
                    // Habilito
                    btnAceptar.Visible = true;
                    btnAceptar.TabStop = true;
                    btnCancelar.Visible = true;
                    btnCancelar.TabStop = true;
                    // Deshabilito
                    btnAgregar.Visible = false;
                    btnAgregar.TabStop = false;
                    btnDeshacer.Visible = false;
                    btnDeshacer.TabStop = false;
                    btnBuscar.Visible = false;
                    btnBuscar.TabStop = false;
                    btnImprimir.Visible = false;
                    btnImprimir.TabStop = false;
                    btnSalir.Visible = false;
                    btnSalir.TabStop = false;

                    break;

                // Búsqueda
                case "B":
                    // Habilito
                    btnAgregar.Visible = false;
                    btnAgregar.TabStop = false;
                    btnDeshacer.Visible = false;
                    btnDeshacer.TabStop = false;
                    btnBuscar.Visible = false;
                    btnBuscar.TabStop = false;
                    btnImprimir.Visible = false;
                    btnImprimir.TabStop = false;
                    btnSalir.Visible = false;
                    btnSalir.TabStop = false;
                    btnCancelar.Visible = true;
                    btnCancelar.TabStop = true;
                    // Deshabilito
                    btnAceptar.Visible = false;
                    btnAceptar.TabStop = false;

                    break;
            }
        }

        #endregion

        #region Método que Controla el botón Deshacer en función de la posición de la fila seleccionada

        private void ControlarDeshacer()
        {

            if (dgvActualizaciones.Rows.Count == 0)
            {
                return;
            }

            // Cambio el estado de la bandera que controla el comportamiento de la grilla
            myBanderaGrilla = true;
            // Cuento las filas de la grilla y lo almaceno en una variable
            int CantFilas = dgvActualizaciones.Rows.Count;
            // Si la posición actual de la grilla es la última fila ?
            if (dgvActualizaciones.CurrentRow.Index == CantFilas - 1)
            {
                btnDeshacer.Visible = true;
            }
            else
            {
                btnDeshacer.Visible = false;
            }
        
        }

        #endregion

        #region Método que devuelve los subrubros en función del rubro

        private string RetornarSubrubros(int IdRubro)
        {
            // Variable de retorno
            string aux = "";
            // Cadena para armar la consulta SQL
            string BuscarSubrubros = "select * from Vista_SubRubros_Rubros where IdRubroArticulo = " + IdRubro;
            // Ejecuto la consulta y paso los datos a una tabla
            DataTable myTabla = clsDataBD.GetSql(BuscarSubrubros);
            // Cuento la cantidad de subrubros y lo almaceno en una variable
            int filas = myTabla.Rows.Count;
            // Contador de filas de la tabla
            int CuentaFilas = 1;
            // Recorro la tabla y paso los datos de los subrubros del rubro a la variable
            foreach (DataRow row in myTabla.Rows)
            {
                // si tengo una sola fila
                if (filas == 1)
                {
                    aux = row["IdSubrubroArticulo"].ToString();
                }
                else
                {
                    if (!(filas == CuentaFilas))
                    {
                        aux = aux + row["IdSubrubroArticulo"].ToString() + ",";
                    }
                    else
                    {
                        aux = aux + row["IdSubrubroArticulo"].ToString();
                    }
                }

                CuentaFilas++;
            }
            // Cadena retornada
            return aux;
        }

        #endregion

        #region Método que se usa cuando se cancela

        private void CancelarTodo()
        {
            // Cambio el estado del formulario
            myEstado = "C";
            // Habilito los controles
            HabilitarControles();
            // Habilito los botones
            HabilitarBotones();
            // Cargo la grilla
            CargarGrilla();
            // Vacío la seleccíon del combo rubros
            cboRubros.SelectedIndex = -1;
        }

        #endregion

        #region Método que pasa los datos a la clase

        private void SetearClase()
        {
            if (myBanderaNueva)
            {

                myActualizacion.Fecha = clsValida.ConvertirFecha(DateTime.Now);
                myActualizacion.IdRubroArticulo = Convert.ToInt32(cboRubros.SelectedValue);
                if (!(cboSubrubros.SelectedIndex==-1))
                {
                    myActualizacion.IdSubrubroArticulo = Convert.ToInt32(cboSubrubros.SelectedValue);
                }
                myActualizacion.Aumento = Convert.ToDouble(txtAumento.Text);
                myActualizacion.Descuento = Convert.ToDouble(txtDescuento.Text);
                myActualizacion.IdUsuario = clsGlobales.UsuarioLogueado.IdUsuario;
                myActualizacion.Activo = 1;
                
            }
            else
            {
                // Tomo los datos de la fila seleccionada y los paso al objeto
                // Los Int
                myActualizacion.IdArticuloActualizacion = Convert.ToInt32(dgvActualizaciones.CurrentRow.Cells["IdArticuloActualizacion"].Value);
                myActualizacion.IdRubroArticulo = Convert.ToInt32(dgvActualizaciones.CurrentRow.Cells["IdRubroArticulo"].Value);
                // Valido que la grilla contenga datos de Subrubros
                if (!(dgvActualizaciones.CurrentRow.Cells["IdSubrubroArticulo"].Value.ToString() == ""))
                {
                    myActualizacion.IdSubrubroArticulo = Convert.ToInt32(dgvActualizaciones.CurrentRow.Cells["IdSubrubroArticulo"].Value);
                }
                myActualizacion.IdUsuario = Convert.ToInt32(dgvActualizaciones.CurrentRow.Cells["IdUsuario"].Value);
                myActualizacion.Activo = Convert.ToInt32(dgvActualizaciones.CurrentRow.Cells["Activo"].Value);
                // Los String
                myActualizacion.Fecha = dgvActualizaciones.CurrentRow.Cells["Fecha"].Value.ToString();
                myActualizacion.RubroArticulo = dgvActualizaciones.CurrentRow.Cells["RubroArticulo"].Value.ToString();
                myActualizacion.SubrubroArticulo = dgvActualizaciones.CurrentRow.Cells["SubRubroArticulo"].Value.ToString();
                myActualizacion.Usuario = dgvActualizaciones.CurrentRow.Cells["Usuario"].Value.ToString();
                // Los Double
                myActualizacion.Aumento = Convert.ToDouble(dgvActualizaciones.CurrentRow.Cells["Aumento"].Value);
                myActualizacion.Descuento = Convert.ToDouble(dgvActualizaciones.CurrentRow.Cells["Descuento"].Value);
            }
            
        }

        #endregion

        #endregion

    }
}

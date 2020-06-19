using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace Prama.Formularios.Articulos
{
    public partial class frmArticulosPtoPedido : Form
    {

        #region Variables del formulario

        // Variables de los radio 
        bool rProductos;
        bool rCodigo;
        // Bandera que habilita los eventos de la grilla
        bool myBanderaGrilla;
        // Bandera que controla la búsqueda
        bool myBanderaBuscar;
        // Bandera desde donde se hace la llamada
        bool esProducto;
        // Variable que almacena la cadena de conexion
        string myCadenaSQL;

        // Creo un nuevo objeto de la clase
        Prama.Clases.clsArticulosFaltantes myArticulo;

        // Creo una tabla que va a almacenar el programa guardado de la semana
        DataTable dtProgramaSemana;
        // Creo una tabla que va a almacenar lo fabricado
        DataTable dtFabricado;

        // Variable que almacena la semana en curso
        int iSemanaActual;
        int iSemana;
        // Variable que almacena el Id del artículo para las búsquedas
        int IdArtPosicion;


        #endregion

        #region Constructor del formulario

        public frmArticulosPtoPedido(bool Prod)
        {
            // Si muestro productos
            esProducto = Prod;
            
            InitializeComponent();
        }

        #endregion

        #region Eventos del formulario

        #region Evento Load del formulario

        private void frmArticulosPtoPedido_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 
            // Inicializo la tabla
            dtProgramaSemana = new DataTable();
            // Establezco el estado del formulario
            clsGlobales.myEstado = "C";
            // Paso el estado a los radio
            rProductos = true;
            rCodigo = true;
            // Deshabilito el reordenamiento de las cabeceras
            DeshabilitarOrdenGrillas();
            // Cargo la grilla
            CargarGrilla("","");

            if (dgvArt.Rows.Count > 0)
            {
                // Cuento las filas de la grilla y se asigno el valor a la variable para posicionar el foco
                IdArtPosicion = dgvArt.Rows.Count - 1;
            }
            
            // Activo los botones
            ActivarBotones();
            // Activo los controles del formulario
            ActivarControles();
            //Titulo Ventana
            this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;
            // Cambio el estado de la bandera que activa los eventos de la grilla
            myBanderaGrilla = true;
            // Pongo el foco en la grilla
            PosicionarFocoGrilla();
            // Paso los datos de la grilla a la clase
            DatosAClase();
            // Paso los datos de la grilla a los controles
            DatosAFormulario();
            // Cargo los toolstips
            CargarToolTips();
            
        }

        #endregion

        #region Eventos de los botones

        #region Evento Click del botón Agregar

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Tomo en la variable desde donde se apretó el botón
            IdArtPosicion = Convert.ToInt32(dgvArt.CurrentRow.Cells["IdArticulo"].Value);
            // Cambio mi estado a alta
            clsGlobales.myEstado = "A";
            // Activo lo botones
            ActivarBotones();
            // Activo los controles
            ActivarControles();
            // Como es una nueva fila, paso los parámetros en blanco
            // Variables para pasar de parámetros
            int CantidadPedida = 0;
            DateTime FechaPedida = DateTime.Now;
            int IdArt = Convert.ToInt32(dgvArt.CurrentRow.Cells["IdArticulo"].Value);
            int IdIns = Convert.ToInt32(dgvArt.CurrentRow.Cells["IdInsumo"].Value);
            int IdProd = Convert.ToInt32(dgvArt.CurrentRow.Cells["IdProducto"].Value);
            string Art = dgvArt.CurrentRow.Cells["Articulo"].Value.ToString();

            Prama.Formularios.Articulos.frmArticulosPtoPedidoConf myForm = new Prama.Formularios.Articulos.frmArticulosPtoPedidoConf(CantidadPedida, FechaPedida, IdArt, IdIns, IdProd, Art,0 );
            myForm.ShowDialog();

            if (clsGlobales.bBandera)
            {
                // Grabo el registro nuevo en la base
                GrabarNuevoPrograma(clsGlobales.FechaFabricada);
                // Cambio el estado de la bandera
                clsGlobales.bBandera = false;
            }
           
            // REcargo el formulario
            RecargarFormulario();

        }

        #endregion

        #region Evento Click del botón Cancelar

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Recargar el formulario
            RecargarFormulario();
        }

        #endregion

        #region Evento Click del botón Modificar

        private void btnModificar_Click(object sender, EventArgs e)
        {
            // Tomo en la variable desde donde se apretó el botón
            IdArtPosicion = Convert.ToInt32(dgvArt.CurrentRow.Cells["IdArticulo"].Value);
            // Cambio mi estado a alta
            clsGlobales.myEstado = "M";
            // Activo lo botones
            ActivarBotones();
            // Activo los controles
            ActivarControles();
            // Variables para pasar de parámetros
            int CantidadPedida = Convert.ToInt32(dgvPrograma.CurrentRow.Cells["Pedido"].Value);
            DateTime FechaPedida = Convert.ToDateTime(dgvPrograma.CurrentRow.Cells["FechaReal_AF"].Value);
            int IdArt = Convert.ToInt32(dgvArt.CurrentRow.Cells["IdArticulo"].Value);
            int IdIns = Convert.ToInt32(dgvArt.CurrentRow.Cells["IdInsumo"].Value);
            int IdProd = Convert.ToInt32(dgvArt.CurrentRow.Cells["IdProducto"].Value);
            string Art = dgvArt.CurrentRow.Cells["Articulo"].Value.ToString();


            Prama.Formularios.Articulos.frmArticulosPtoPedidoConf myForm = new Prama.Formularios.Articulos.frmArticulosPtoPedidoConf(CantidadPedida, FechaPedida, IdArt, IdIns, IdProd, Art,0);
            myForm.ShowDialog();

            // Si fecha no es nulo, lo paso a la clase
            if (!(dgvPrograma.CurrentRow.Cells["FechaReal_AF"].Value == null || dgvPrograma.CurrentRow.Cells["FechaReal_AF"].Value == ""))
            {
                DateTime dFechaAConvertir = Convert.ToDateTime(dgvPrograma.CurrentRow.Cells["FechaReal_AF"].Value);

                myArticulo.Fecha = clsValida.ConvertirFecha(dFechaAConvertir);
            }

            // Si fecha real no es nulo, lo paso a la clase
            if (!(dgvPrograma.CurrentRow.Cells["FechaReal_AF"].Value == null || dgvPrograma.CurrentRow.Cells["FechaReal_AF"].Value == ""))
            {
                myArticulo.FechaReal = Convert.ToDateTime(dgvPrograma.CurrentRow.Cells["FechaReal_AF"].Value);
            }

            if (clsGlobales.bBandera)
            {
                // Armo la cedena SQL
                myCadenaSQL = "delete ArticulosProgramaCompra where IdArticulo = " + myArticulo.IdArticuloFaltante + " and Fecha = '" + myArticulo.FechaReal.ToShortDateString() + "'";
                // Ejecuto la consulta
                clsDataBD.GetSql(myCadenaSQL);

                // Grabo el registro nuevo en la base
                GrabarNuevoPrograma(clsGlobales.FechaFabricada);

                clsGlobales.bBandera = false;
            }

            // REcargo el formulario
            RecargarFormulario();
        }

        #endregion

        #region Evento Click del botón Aceptar

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            switch (clsGlobales.myEstado)
            {
                case "A":

                    // Grabo el registro nuevo en la base
                    GrabarNuevoPrograma(clsGlobales.FechaFabricada);

                    break;

                case "M":

                    // Armo la cedena SQL
                    myCadenaSQL = "delete ArticulosProgramaCompra where IdArticulo = " + myArticulo.IdArticuloFaltante + " and Fecha = '" + myArticulo.FechaReal.ToShortDateString() + "'";
                    // Ejecuto la consulta
                    clsDataBD.GetSql(myCadenaSQL);

                    // Grabo el registro nuevo en la base
                    GrabarNuevoPrograma(clsGlobales.FechaFabricada);

                    break;

                case "B":

                    // Cambio el estado de la bandera de la búsqueda
                    myBanderaBuscar = false;
                    // Tomo en la variable desde donde se apretó el botón
                    IdArtPosicion = Convert.ToInt32(dgvArt.CurrentRow.Cells["IdArticulo"].Value);
                    
                    break;
            }

            RecargarFormulario();
        }

        #endregion

        #region Evento Click del botón Borrar

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            // Tomo en la variable desde donde se apretó el botón
            IdArtPosicion = Convert.ToInt32(dgvArt.CurrentRow.Cells["IdArticulo"].Value); ;

            // Pongo en la variable el Id del programa a borrar
            int iProgramaBorrar = Convert.ToInt32(dgvPrograma.CurrentRow.Cells["idArticuloProgramaCompra"].Value);
            
            // Pregunto
            DialogResult myRespuesta = MessageBox.Show("Desea eliminar el programa seleccionado de " + myArticulo.Articulo, "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // if desea eliminar la fabricación
            if (myRespuesta == DialogResult.Yes)
            {
                dgvArt.CurrentRow.Cells["Fabricar"].Value = "";

                txtFabricar.Text = "";

                // Elimino de la tabla
                // Armo la cedena SQL
                myCadenaSQL = "delete ArticulosProgramaCompra where idArticuloProgramaCompra = " + iProgramaBorrar;
                // Ejecuto la consulta
                clsDataBD.GetSql(myCadenaSQL);

            }

            RecargarFormulario();
           
        }

        #endregion

        #region Evento Click del botón Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Tomo en la variable desde donde se apretó el botón
            IdArtPosicion = Convert.ToInt32(dgvArt.CurrentRow.Cells["IdArticulo"].Value);
            // Posicionamiento
            this.txtArt.Text = "";
            this.txtCodArt.Text = "";
            myBanderaBuscar = true;
            // Cambio mi estado
            clsGlobales.myEstado = "B";
            // Activo lo botones
            ActivarBotones();
            // Activo los controles
            ActivarControles();
            // Vacío los campos de búsqueda
            LimpiarControles();
            //Foco
            this.txtArt.Focus();
        }

        #endregion

        #region Evento Click del botón Imprimir

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            // Hago la llamada para el formulario que gestiona la impresión
            Prama.Formularios.Articulos.frmArticulosPtoPedidoImpresion myForm = new Prama.Formularios.Articulos.frmArticulosPtoPedidoImpresion();
            myForm.ShowDialog();

            if (clsGlobales.bBandera)
            {
                // Imprimo 
                ImprimirPrograma();

                clsGlobales.bBandera = false;
            }
            
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

        #region eventos de la grilla

        #region Evento Click de la grilla

        private void dgvArt_Click(object sender, EventArgs e)
        {
            // Cambio el estado de la bandera que activa los eventos de la grilla
            myBanderaGrilla = true;
        }

        #endregion

        #region Evento SelectionChanged de la grilla

        private void dgvArt_SelectionChanged(object sender, EventArgs e)
        {
            // Si ya se hizo clik en la grilla
            if (myBanderaGrilla)
            {
                // y si no estoy en una búsqueda
                if (!(myBanderaBuscar))
                {
                    // Paso los datos a la clase
                    DatosAClase();
                    // Paso los datos al formulario
                    DatosAFormulario();
                    

                }
            }
        }

        #endregion

        #region Evento CellClick de la grilla

        private void dgvArt_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Cambio el estado de la bandera que activa los eventos de la grilla
            myBanderaGrilla = true;
        }

        #endregion

        #endregion

        #region Eventos de los controles

        #region Eventos de los radios

        private void rdbProductos_CheckedChanged(object sender, EventArgs e)
        {
            rProductos = true;
            CargarGrilla("","");
        }

        private void rdbInsumos_CheckedChanged(object sender, EventArgs e)
        {
            rProductos = false;
            CargarGrilla("","");
        }

        private void rdbCodigo_CheckedChanged(object sender, EventArgs e)
        {
            rCodigo = true;
            CargarGrilla("","");
        }

        private void rdbArticulo_CheckedChanged(object sender, EventArgs e)
        {
            rCodigo = false;
            CargarGrilla("","");
        }

        #endregion

        #region Eventos de los textBox

        private void txtCodArt_TextChanged(object sender, EventArgs e)
        {
            if (myBanderaBuscar)
            {
                CargarGrilla("CodigoArticulo", txtCodArt.Text);
            }
        }

        private void txtCodArt_Click(object sender, EventArgs e)
        {
            txtArt.Text = "";
            myBanderaBuscar = true;
        }

        private void txtArt_Click(object sender, EventArgs e)
        {
            txtCodArt.Text = "";
            myBanderaBuscar = true;
        }

        private void txtArt_TextChanged(object sender, EventArgs e)
        {
            if (myBanderaBuscar)
            {
                CargarGrilla("Articulo", txtArt.Text);
            }
        }

        private void txtFabricar_KeyPress(object sender, KeyPressEventArgs e)
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

        #region Evento ValueChanged del dtpSemana

        private void dtpSemana_ValueChanged(object sender, EventArgs e)
        {
            // Paso a la variable el valor de la semana seleccionada
            // iSemanaActual = getWeekOfYear(dtpSemana.Value);
            // Ejecuto el perform click del cancelar
            RecargarFormulario();
        }

        #endregion

        #endregion

        #endregion

        #region Métodos del formulario

        #region Método que carga la grilla

        private void CargarGrilla(string Campo, string Buscar)
        {

            if (Campo == "")
            {
                if (esProducto)
                {
                    // Armo la cadena SQL para taer los datos de los proveedores
                    myCadenaSQL = "select * from Productos_Articulos";
                }
                else
                {
                    // Armo la cadena SQL para taer los datos de los proveedores
                    myCadenaSQL = "select * from InsumosCompuestos_Articulos";
                }
            }
            else
            {
                if (esProducto)
                {
                    // Armo la cadena SQL para taer los datos de los proveedores
                    myCadenaSQL = "select * from Productos_Articulos where " + Campo + "  like '" + Buscar + "%' ";
                }
                else
                {
                    // Armo la cadena SQL para taer los datos de los proveedores
                    myCadenaSQL = "select * from InsumosCompuestos_Articulos where " + Campo + "  like '" + Buscar + "%' ";
                }
            }

            if (rCodigo)
            {
                myCadenaSQL = myCadenaSQL + " order by CodigoArticulo";
            }
            else
            {
                myCadenaSQL = myCadenaSQL + " order by Articulo";
            }

            // Ejecuto la consulta y paso los datos a una tabla
            DataTable myTablaProv = clsDataBD.GetSql(myCadenaSQL);
            // Evito que el dgv genere columnas automáticas
            dgvArt.AutoGenerateColumns = false;
            // Asigno el source de la grilla de los proveedores a la nueva tabla
            dgvArt.DataSource = myTablaProv;
            // Si son Insumos, cambio el heder de la columna Fabricar por pedir
            if (!(esProducto))
            {
                dgvArt.Columns["Fabricar"].HeaderText = "Comprar";
            }
                        
            // Calculo los faltantes
            CalcularFaltantes();
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
                case "M":
                    this.btnAgregar.TabStop = false;
                    this.btnAgregar.Visible = false;
                    this.btnModificar.TabStop = false;
                    this.btnModificar.Visible = false;
                    this.btnBuscar.TabStop = false;
                    this.btnBuscar.Visible = false;
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
                    this.btnBuscar.TabStop = false;
                    this.btnBuscar.Visible = false;
                    this.btnAceptar.TabStop = true;
                    this.btnAceptar.Visible = true;
                    this.btnCancelar.TabStop = true;
                    this.btnCancelar.Visible = true;
                    this.btnSalir.TabStop = false;
                    this.btnSalir.Visible = false;
                    this.btnImprimir.TabStop = false;
                    this.btnImprimir.Visible = false;
                    this.btnBorrar.TabStop = false;
                    this.btnBorrar.Visible = false;
                    break;
                case "C":
                    this.btnAgregar.TabStop = true;
                    this.btnAgregar.Visible = true;
                    this.btnModificar.TabStop = true && (dgvArt.RowCount != 0);
                    this.btnModificar.Visible = true && (dgvArt.RowCount != 0);
                    this.btnBuscar.TabStop = true && (dgvArt.RowCount != 0);
                    this.btnBuscar.Visible = true && (dgvArt.RowCount != 0);
                    this.btnAceptar.TabStop = false;
                    this.btnAceptar.Visible = false;
                    this.btnCancelar.TabStop = false;
                    this.btnCancelar.Visible = false;
                    this.btnSalir.TabStop = true;
                    this.btnSalir.Visible = true;
                    // Si el nivel del usuario es el 5, habilito el botón borrar
                    if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja)
                    {
                        this.btnBorrar.TabStop = true && (dgvArt.RowCount != 0);
                        this.btnBorrar.Visible = true && (dgvArt.RowCount != 0);
                    }
                    else
                    {
                        this.btnBorrar.TabStop = false;
                        this.btnBorrar.Visible = false;
                    }
                    this.btnImprimir.TabStop = true && (dgvArt.RowCount != 0);
                    this.btnImprimir.Visible = true && (dgvArt.RowCount != 0);
                    return;
            }
        }
        #endregion

        #region Método que activa los controles del formulario

        private void ActivarControles()
        {
            switch (clsGlobales.myEstado)
            {
                case "A":
                case "M":
                    
                    // this.gpbProductos.Enabled = true;
                    this.lblFabricar.Visible = true;
                    this.txtFabricar.Visible = true;
                    this.txtFabricar.TabStop = true;

                    this.txtUnidades.Visible = true;
                    this.txtUnidades.TabStop = true;
                    this.txtStock.Visible = true;
                    this.txtStock.TabStop = true;
                    this.txtPtoPedido.Visible = true;
                    this.txtPtoPedido.TabStop = true;
                    this.txtFabricar.Visible = true;
                    this.txtFabricar.TabStop = true;
                    this.txtFaltante.Visible = true;
                    this.txtFaltante.TabStop = true;

                    this.txtCodArt.Enabled = false;
                    this.txtCodArt.TabStop = false;
                    this.txtArt.Enabled = false;
                    this.txtArt.TabStop = false;

                    dgvArt.Enabled = false;

                    break;

                case "B":
                    // this.gpbProductos.Enabled = true;
                    this.txtCodArt.Enabled = true;
                    this.txtCodArt.TabStop = true;
                    this.txtArt.Enabled = true;
                    this.txtArt.TabStop = true;

                    this.lblUnidades.Visible = false;
                    this.txtUnidades.Visible = false;
                    this.txtUnidades.TabStop = false;
                    this.lblStock.Visible = false;
                    this.txtStock.Visible = false;
                    this.txtStock.TabStop = false;
                    this.lblPtoPedido.Visible = false;
                    this.txtPtoPedido.Visible = false;
                    this.txtPtoPedido.TabStop = false;
                    this.lblFabricar.Visible = false;
                    this.txtFabricar.Visible = false;
                    this.txtFabricar.TabStop = false;
                    this.lblFaltante.Visible = false;
                    this.txtFaltante.Visible = false;
                    this.txtFaltante.TabStop = false;

                    dgvArt.Enabled = true;

                    break;

                case "C":
                    // this.gpbProductos.Enabled = false;
                    this.txtCodArt.Enabled = false;
                    this.txtCodArt.TabStop = false;
                    this.txtArt.Enabled = false;
                    this.txtArt.TabStop = false;

                    this.lblUnidades.Visible = true;
                    this.txtUnidades.Visible = true;
                    this.txtUnidades.TabStop = false;
                    this.lblStock.Visible = true;
                    this.txtStock.Visible = true;
                    this.txtStock.TabStop = false;
                    this.lblPtoPedido.Visible = true;
                    this.txtPtoPedido.Visible = true;
                    this.txtPtoPedido.TabStop = false;
                    this.lblFabricar.Visible = true;
                    this.txtFabricar.Visible = true;
                    this.txtFabricar.TabStop = false;
                    this.lblFaltante.Visible = true;
                    this.txtFaltante.Visible = true;
                    this.txtFaltante.TabStop = false;
                    
                    this.lblFabricar.Visible = true;
                    this.txtFabricar.Visible = true;
                    this.txtFabricar.TabStop = true;

                    dgvArt.Enabled = true;

                    break;

            }

        }

        #endregion

        #region Método que deshabilita el reordenamiento de las grilla desde sus cabeceras

        private void DeshabilitarOrdenGrillas()
        {
            // Deshabilito la reordenación de las columnas de las grillas
            foreach (DataGridViewColumn dgvCol in dgvArt.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;

            }
        }

        #endregion

        #region Método que calcula los faltantes

        private void CalcularFaltantes()
        {
            // Variables para stock y el punto de pedido
            double dStock = 0;
            double dPto = 0;
            // Variable que guarda el resultado de la resta
            double dSaldoStock = 0;
            // SI hay artículos con faltantes
            if (dgvArt.Rows.Count > 0)
            {
                // Recorro la grilla y hago la resta entre punto de stock y el stock real
                foreach (DataGridViewRow row in dgvArt.Rows)
                {
                    // Paso a las variables los valores
                    dStock = Convert.ToDouble(row.Cells["Stock"].Value);
                    dPto = Convert.ToDouble(row.Cells["StockPuntoPedido"].Value);
                    // Hago la resta
                    dSaldoStock = dStock - dPto;
                    // SI el resultado es menor a 0
                    if (dSaldoStock < 0)
                    {
                        // Al resultado lo paso a la grilla
                        row.Cells["Faltante"].Value = dSaldoStock.ToString("#0.00");
                    }

                    //**********************************************************
                    // Cargo a la fila lo programado si hay algo
                    if (dtProgramaSemana.Rows.Count > 0)
                    {
                        // recorro la tabla con el programa
                        foreach (DataRow rowPrograma in dtProgramaSemana.Rows)
                        {
                            if (Convert.ToInt32(rowPrograma["idArticulo"]) == Convert.ToInt32(row.Cells["IdArticulo"].Value))
                            {
                                row.Cells["Fabricar"].Value = rowPrograma["cantidad"];
                                // row.Cells["FechaReal"].Value = rowPrograma["Fecha"];
                                // row.Cells["Fecha"].Value = clsValida.ConvertirFecha((Convert.ToDateTime(rowPrograma["Fecha"])));
                                row.Cells["Semana"].Value = rowPrograma["semana"];
                                row.Cells["Dia"].Value = rowPrograma["dia"];
                            }
                        }
                    }

                    
                }
            }
        }

        #endregion

        #region Método que pasa los datos a la clase

        private void DatosAClase()
        {
            // Tomo el Id de la posicion en la grilla
            // IdArtPosicion = Convert.ToInt32(dgvArt.CurrentRow.Cells["IdArticulo"].Value);
            // Creo un nuevo objeto de la clase
            myArticulo = new Clases.clsArticulosFaltantes();
            // Le paso los datos de la grilla
            myArticulo.IdArticuloFaltante = Convert.ToInt32(dgvArt.CurrentRow.Cells["IdArticulo"].Value);

            myArticulo.CodigoArticulo = dgvArt.CurrentRow.Cells["CodigoArticulo"].Value.ToString();
            myArticulo.Articulo = dgvArt.CurrentRow.Cells["Articulo"].Value.ToString();
            myArticulo.Unidades = dgvArt.CurrentRow.Cells["Unidades"].Value.ToString();
            myArticulo.Stock = Convert.ToDouble(dgvArt.CurrentRow.Cells["Stock"].Value);
            myArticulo.StockPuntoPedido = Convert.ToDouble(dgvArt.CurrentRow.Cells["StockPuntoPedido"].Value);
            myArticulo.Faltante = Convert.ToDouble(dgvArt.CurrentRow.Cells["Faltante"].Value);
            // Si Fabricar no es nulo
            if (!(dgvArt.CurrentRow.Cells["Fabricar"].Value == null || dgvArt.CurrentRow.Cells["Fabricar"].Value ==""))
            {
                myArticulo.Fabricar = Convert.ToDouble(dgvArt.CurrentRow.Cells["Fabricar"].Value);
            }
            else
            {
                myArticulo.Fabricar = 0;
            }
           
            myArticulo.Activo = Convert.ToInt32(dgvArt.CurrentRow.Cells["Activo"].Value);
            myArticulo.IdInsumo = Convert.ToInt32(dgvArt.CurrentRow.Cells["IdInsumo"].Value);
            myArticulo.IdProducto = Convert.ToInt32(dgvArt.CurrentRow.Cells["IdProducto"].Value);

            

        }

        #endregion

        #region Método que pasa los datos de la clase al formulario

        private void DatosAFormulario()
        {
            // Datos de la clase a la grilla
            txtCodArt.Text = myArticulo.CodigoArticulo;
            txtArt.Text = myArticulo.Articulo;
            txtUnidades.Text = myArticulo.Unidades;
            txtStock.Text = myArticulo.Stock.ToString();
            txtPtoPedido.Text = myArticulo.StockPuntoPedido.ToString();
            txtFaltante.Text = myArticulo.Faltante.ToString();

            // Paso los datos a las grillas
            CargarPrograma(Convert.ToInt32(dgvArt.CurrentRow.Cells["IdArticulo"].Value));
            // Evito que la grille genere columnas automáticas.
            dgvPrograma.AutoGenerateColumns = false;
            // Asigno los datos a la grilla
            dgvPrograma.DataSource = dtProgramaSemana;
            // Evito que la grille genere columnas automáticas.
            dgvFabricado.AutoGenerateColumns = false;
            // Asigno los datos a la grilla
            dgvFabricado.DataSource = dtFabricado;
            // Variable que uso para contar todo lo pendiente de fabricar programado
            double dPendientes = 0;
            // Convierto las fechas
            foreach (DataGridViewRow r in dgvPrograma.Rows)
            {
                r.Cells["Fecha_AF"].Value = clsValida.ConvertirFecha((Convert.ToDateTime(r.Cells["FechaReal_AF"].Value)));
                dPendientes += Convert.ToDouble(r.Cells["Pedido"].Value);
            }
            foreach (DataGridViewRow s in dgvFabricado.Rows)
            {
                if (!(s.Cells["FechaReal_F"].Value == DBNull.Value || s.Cells["FechaReal_F"].Value == ""))
                {
                    s.Cells["Fecha_F"].Value = clsValida.ConvertirFecha((Convert.ToDateTime(s.Cells["FechaReal_F"].Value)));
                }

            }
            myArticulo.Fabricar = dPendientes;
            txtFabricar.Text = myArticulo.Fabricar.ToString();

            int filasGrillas = 0;

            filasGrillas = dgvPrograma.Rows.Count;

            // Si hay algo programado
            if (filasGrillas > 0)
            {
                btnModificar.Visible = true;
                btnAceptarFabricado.Visible = true;
                btnBorrar.Visible = true;
                btnImprimir.Visible = true;

                dgvPrograma.CurrentCell = dgvPrograma[5, 0];
            }
            else
            {
                btnModificar.Visible = false;
                btnAceptarFabricado.Visible = false;
                btnBorrar.Visible = false;
                // btnImprimir.Visible = false;
            }

            filasGrillas = dgvFabricado.Rows.Count;

            // SI hay algo fabricado
            if (filasGrillas > 0)
            {
                dgvFabricado.CurrentCell = dgvFabricado[4, (dgvFabricado.Rows.Count - 1)];
                btnImprimir.Visible = true;
            }
            

        }

        #endregion

        #region Metodo ObtenerPermiso

        private bool ObtenerPermiso(int p_Det = 0, int p_Menu = 0, int p_IdUser = 0)
        {
            bool bRetorno = false;

            string myCad = "Select Habilitado from MenuOpcionesUser Where IdDetMenu = " + p_Det + " AND IdMenu = " + p_Menu
                            + " AND IdUser = " + p_IdUser;
            DataTable myDataVal = clsDataBD.GetSql(myCad);

            foreach (DataRow row in myDataVal.Rows)
            {
                if (Convert.ToInt32(row["Habilitado"].ToString()) == 1)
                { bRetorno = true; }
                else { bRetorno = false; }

            }

            //Retornar valor obtenido
            return bRetorno;
        }

        #endregion
        
        #region Método que carga el programa de la tabla

        private void CargarPrograma(int IdArt)
        {
            // Armo la cadena
            myCadenaSQL = "Select * from ArticulosProgramaCompra where IdArticulo = " + IdArt + " and Cumplido = " + 0 + " order by Fecha";
            // Paso a la tabla los datos de la semana
            dtProgramaSemana = clsDataBD.GetSql(myCadenaSQL);

            // Armo la cadena
            myCadenaSQL = "Select * from ArticulosProgramaCompra where IdArticulo = " + IdArt + " and Cumplido = " + 1 + " order by Fecha";
            // Paso a la tabla los datos de la semana
            dtFabricado = clsDataBD.GetSql(myCadenaSQL);
        }

        #endregion

        #region Método que devuelve la semana del año

        private int getWeekOfYear(DateTime dFecha)
        {
            int aux = 0;
            
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;

            aux = cal.GetWeekOfYear(dFecha, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);

            return aux;
        }

        #endregion

        #region Método que devuleve el número de día de la semana

        private int DevolverDia(string dFecha)
        {
            int aux = 0;

            switch (dFecha)
            {
                case "Sunday":

                    aux = 1;

                    break;

                case "Monday":

                    aux = 2;

                    break;

                case "Tuesday":

                    aux = 3;

                    break;

                case "Wednesday":

                    aux = 4;

                    break;

                case "Thursday":

                    aux = 5;

                    break;

                case "Friday":

                    aux = 6;

                    break;

                case "Saturday":

                    aux = 7;

                    break;
            }

            return aux;
        }

        #endregion

        #region Método Que limpia los campos de búsqueda

        private void LimpiarControles()
        {
            txtCodArt.Text = "";
            txtArt.Text = "";
                       
        }

        #endregion

        #region Método que posiciona el foco en la grilla

        private void PosicionarFocoGrilla()
        {
            int Index = 0;

            if (dgvArt.Rows.Count > 0)
            {
                // Recorro la grilla para encontrar el Id del articulo
                foreach (DataGridViewRow row in dgvArt.Rows)
                {
                    if (Convert.ToInt32(row.Cells["IdArticulo"].Value) == IdArtPosicion)
                    {
                        Index = row.Index;
                    }
                }
                dgvArt.CurrentCell = dgvArt[1, Index];
            }
        }
        
        #endregion

        #region Método que inserta un nuevo registro en la tabla ArticulosProgramaCompra

        private void GrabarNuevoPrograma(DateTime dFecha)
        {
            myArticulo.semana = getWeekOfYear(dFecha);
            myArticulo.Fabricar = clsGlobales.CantFabricada;
            myArticulo.dia = DevolverDia(dFecha.DayOfWeek.ToString());
            myArticulo.FechaReal = dFecha;
            myArticulo.Fecha = clsValida.ConvertirFecha(dFecha);
            // Grabo los datos en la tabla
            // Armo la cadena SQL
            myCadenaSQL = "Insert into ArticulosProgramaCompra (idArticulo, idProducto, idInsumo, semana, cantidad, fecha, sFecha, dia, cumplido," 
                           + "Fabricado, FechaCumplido, Empleado, IdEmpleado,Usuario, IdUsuario) values (" +
                          myArticulo.IdArticuloFaltante + ", " +
                          myArticulo.IdProducto + ", " +
                          myArticulo.IdInsumo + ", " +
                          myArticulo.semana + ", " +
                          myArticulo.Fabricar + ", '" +
                          Convert.ToString(myArticulo.FechaReal) + "', '" +
                          Convert.ToString(myArticulo.Fecha) + "', " +
                          myArticulo.dia + ", " + 0 + ", " +
                          "0,'" +  Convert.ToString(myArticulo.FechaReal) + "', '" +
                          clsGlobales.EmpleadoFabricado + "'," +
                          clsGlobales.IdEmpleadoFabricado + ", '" +
                          clsGlobales.UsuarioLogueado.Usuario + "'," +
                          clsGlobales.UsuarioLogueado.IdUsuario + ")";

            // Ejecuto la consulta
            clsDataBD.GetSql(myCadenaSQL);
        }

        #endregion

        #region Método que recarga el formulario

        private void RecargarFormulario()
        {
            // Cambio mi estado a en espera
            clsGlobales.myEstado = "C";
            // Cambio el estado de la bandera de búsqueda
            myBanderaBuscar = false;
            // Activo lo botones
            ActivarBotones();
            // Activo los controles
            ActivarControles();
            // Cargo la grilla
            CargarGrilla("", "");
            // Posiciono el foco desde donde se lo llamó
            PosicionarFocoGrilla();
            // Paso los datos de la grilla a la clase
            DatosAClase();
            // Paso los datos de la grilla a los controles
            DatosAFormulario();
        }

        #endregion

        private void btnFabricado_Click(object sender, EventArgs e)
        {
            Prama.Formularios.Stock.frmStockFabricacion myForm = new Prama.Formularios.Stock.frmStockFabricacion();
            myForm.ShowDialog();
        }

        #endregion

        private void brnAceptarFabricado_Click(object sender, EventArgs e)
        {
            // Tomo en la variable desde donde se apretó el botón
            IdArtPosicion = Convert.ToInt32(dgvArt.CurrentRow.Cells["IdArticulo"].Value);
            
            clsGlobales.myEstado = "F";
            
            // Variables para pasar de parámetros
            int CantidadPedida = Convert.ToInt32(dgvPrograma.CurrentRow.Cells["Pedido"].Value);
            
            DateTime FechaPedida = Convert.ToDateTime(dgvPrograma.CurrentRow.Cells["FechaReal_AF"].Value);
        
            int IdArt = Convert.ToInt32(dgvArt.CurrentRow.Cells["IdArticulo"].Value);
            int IdIns = Convert.ToInt32(dgvArt.CurrentRow.Cells["IdInsumo"].Value);
            int IdProd = Convert.ToInt32(dgvArt.CurrentRow.Cells["IdProducto"].Value);
            string Art = dgvArt.CurrentRow.Cells["Articulo"].Value.ToString();
            int iIdEmpleado = Convert.ToInt32(dgvPrograma.CurrentRow.Cells["IdEmpleado"].Value);

            Prama.Formularios.Articulos.frmArticulosPtoPedidoConf myForm = new Prama.Formularios.Articulos.frmArticulosPtoPedidoConf(CantidadPedida, FechaPedida, IdArt, IdIns, IdProd, Art, iIdEmpleado);
            myForm.ShowDialog();

            if (clsGlobales.CantFabricada != 0 && clsGlobales.bBandera != false)
            {
                // cre la cadena SQL
                myCadenaSQL = "update ArticulosProgramaCompra set Cumplido = 1, Fabricado =" + clsGlobales.CantFabricada +
                              ", FechaCumplido = '" + Convert.ToString(clsGlobales.FechaFabricada) +
                              "', Empleado = '" + clsGlobales.EmpleadoFabricado + 
                              "', IdEmpleado = " + clsGlobales.IdEmpleadoFabricado + 
                              " where IdArticulo = " + IdArt + " and Fecha = '" + FechaPedida + "'";

                // Ejecuto la cadena
                clsDataBD.GetSql(myCadenaSQL);
            }

            RecargarFormulario();
        }

        #region Método que gestiona la impresión del documento

        private void ImprimirPrograma()
        {
            // Tomo en la variable desde donde se apretó el botón
            IdArtPosicion = Convert.ToInt32(dgvArt.CurrentRow.Cells["IdArticulo"].Value);
            string Codigo = dgvArt.CurrentRow.Cells["CodigoArticulo"].Value.ToString();

            // Armo la cadena sql general dependiendo de los parámetros globales seleccionados de fecha
            myCadenaSQL = "select * from Vista_ArticulosProgramaCompra where Fecha >= '" + clsGlobales.FechaDesde.ToShortDateString() +
                        "' and Fecha <= '" + clsGlobales.FechaHasta.ToShortDateString() +"'";
            // Agrego los condicionales según corresponda

            if (clsGlobales.Individual)
            {
                myCadenaSQL += " and idArticulo = " + IdArtPosicion;
            }
            
            if (clsGlobales.Programa)
            {
                myCadenaSQL += " and Cumplido = 0";
            }
            else
            {
                myCadenaSQL += " and Cumplido = 1";
            }

            if (clsGlobales.IdEmpleadoFabricado != 0)
            {
                myCadenaSQL += " and IdEmpleado = " + clsGlobales.IdEmpleadoFabricado;
            }

            myCadenaSQL += " order by fecha";

            // Ejecuto la cadena SQL y la paso a una tabla
            DataTable myTable = new DataTable();
            myTable = clsDataBD.GetSql(myCadenaSQL);

            //Data Set
            dsReportes oDsArt = new dsReportes();
            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = myTable.Rows.Count;
            // Variable que acumula el porcentaje general de eficiencia
            double dAcumulaPorcentaje = 0;

            if (!(clsGlobales.Programa))
            {
                foreach (DataRow r in myTable.Rows)
                {
                    double dPorcentaje = 0;
                    
                    dPorcentaje = (Convert.ToDouble(r["Fabricado"]) / Convert.ToDouble(r["Cantidad"]))*100;

                    dAcumulaPorcentaje += dPorcentaje;
                    
                    oDsArt.Tables["dtProgramaProd"].Rows.Add
                   (new object[] 
                        { 
                            r["CodigoArticulo"].ToString(),
                            r["Articulo"].ToString(),
                            r["Unidades"].ToString(),
                            r["cantidad"].ToString(),
                            r["sFecha"].ToString(),
                            "SI",
                            r["Fabricado"].ToString(),
                            r["Empleado"].ToString(),
                            dPorcentaje.ToString("#0.00"),
                        }
                   );
                }
            }

            else
            {
                foreach (DataRow r in myTable.Rows)
                {
                    oDsArt.Tables["dtProgramaProd"].Rows.Add
                   (new object[] 
                        { 
                            r["CodigoArticulo"].ToString(),
                            r["Articulo"].ToString(),
                            r["Unidades"].ToString(),
                            r["cantidad"].ToString(),
                            r["sFecha"].ToString(),
                        }
                   );
                }
            }

            

            //Objeto Reporte
            rptProgramaFab oRepArt = new rptProgramaFab();
            //Cargar Reporte            
            //    oRepTipoCli.Load(Application.StartupPath + "\\rptTipoCli.rpt");
            oRepArt.Load(Application.StartupPath + "\\rptProgramaFab.rpt");
            //Establecer el DataSet como DataSource
            oRepArt.SetDataSource(oDsArt);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepArt;

            oRepArt.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepArt.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
            oRepArt.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
            oRepArt.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
            oRepArt.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
            oRepArt.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepArt.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
            oRepArt.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";

            if (clsGlobales.Programa)
            {
                oRepArt.DataDefinition.FormulaFields["Programa"].Text = "'PROGRAMA DE FABRICACIÓN'";
                oRepArt.DataDefinition.FormulaFields["textoColumna"].Text = "'Pedido'";
            }
            else
            {
                oRepArt.DataDefinition.FormulaFields["Programa"].Text = "'FABRICADO'";
                oRepArt.DataDefinition.FormulaFields["textoColumna"].Text = "'Pedido'";
            }

            oRepArt.DataDefinition.FormulaFields["Semana"].Text = "'Desde " + clsValida.ConvertirFecha(clsGlobales.FechaDesde).ToString() + " Hasta " +
                                                                clsValida.ConvertirFecha(clsGlobales.FechaHasta).ToString() + "'";

            if (!(dgvFilas == 0))
            {
                oRepArt.DataDefinition.FormulaFields["Porcentaje"].Text = (dAcumulaPorcentaje / dgvFilas).ToString("#0.00");
            }
            
            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();

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

            toolTip9.SetToolTip(this.btnAFabricacion, "Abre el formulario de fabricación");
            toolTip10.SetToolTip(this.btnAceptarFabricado, "Permite confirmar la fabricación del producto programado");
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Prama.Formularios.Auxiliares;
using Prama.Clases;

namespace Prama.Formularios.Ventas
{
    public partial class frmLista_Presupuestos : Form
    {

        // Variable que almacena el Id del tipo de comprobante
        int IdPresupuesto = 0;
        string myEstado = "C";
        int indexFila = 0;
        bool bSearch = true;        
        int IdPresuSearch = 0;      //Guarda Id Presupuesto para busquedas
        string bFiltro = "";        //Filtro

      // Bandera que controla la carga de la segunda grilla
        bool yaCargado = false;

        public frmLista_Presupuestos()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region EVENTO LOAD

        private void frmLista_Presupuestos_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            // Cargo los toolstip de los botones
            CargarToolsTip();
            //User 9
            if (clsGlobales.UsuarioLogueado.IdUsuario == 9) //DESPENSA
            {
                this.rdbT.Enabled = false;
                this.rdbT.Checked = false;
                this.rbtF.Enabled = false;
                this.rbtF.Checked = false;
                this.rbtSFact.Enabled = false;
                this.rbtSFact.Checked = true;
                this.rbtFact.Enabled = false;
                this.rbtFact.Checked = true;
                bFiltro = "AND Facturado = 0 AND Pendiente = 0";
            }
            // Cargo la grilla
            CargarGrilla("", "");
            // Cambio el alto de la grilla detalle para que tape los campos de búsqueda
            dgvDetalle.Height = 232;
            //Ordenamiento
            DeshabilitarOrdenGrillas();
            // Cuento la cantidad de filas de la grilla
            int filas = dgvComprobantes.Rows.Count;
            //Foco
            dgvComprobantes.Focus();
            // Actualizo el valor de la fila para que sea la última de la grilla
            this.indexFila = filas - 1;
            // Pongo el foco de la fila
            PosicionarFocoFila();
            //Titulo
            this.Text = clsGlobales.cParametro.NombreFantasia + this.Text; 

        }

        #endregion

        #region Método que deshabilita el reordenamiento de las grilla desde sus cabeceras

        private void DeshabilitarOrdenGrillas()
        {
            // Deshabilito la reordenación de las columnas de las grillas
            foreach (DataGridViewColumn dgvCol in this.dgvComprobantes.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;

            }
        }

        #endregion

        #region Método que devuelve el foco a la fila desde donde se presionó algún botón

        private void PosicionarFocoFila()
        {
            if (dgvComprobantes.Rows.Count != 0 && dgvComprobantes.Rows.Count > this.indexFila)
            {
                // Devuelvo el foco a la fila de la grilla desde donde se llamó
                this.dgvComprobantes.CurrentCell = dgvComprobantes[3, this.indexFila];

                // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                EventArgs ea = new EventArgs();
                this.dgvComprobantes_SelectionChanged(this.dgvComprobantes, ea);
            }
        }

        #endregion

        #region Método que limpia los campos de búsqueda del formulario

        private void LimpiarCamposBusqueda()
        {
            txtCliente.Text = "";
            txtNro.Text = "";
        }

        #endregion

        #region Metodo: CargarGrilla

        // Método que carga la grilla del formulario
        private void CargarGrilla(string Buscar, string Campo)
        {
            // Variable para la cadena SQL
            string myCadena = "";
            if (Buscar == "")
            {
                // Cadena SQL
                myCadena = "SELECT * FROM Vista_Presupuestos WHERE Activo = 1 " + bFiltro + " ORDER BY Fecha, Punto, Nro ASC";
            }
            else
            {
                if (Campo == "RazonSoCli")
                {
                  //Cadena SQL 
                    myCadena = "select * from Vista_Presupuestos where " + Campo + " like '" + Buscar + "%' and Activo = 1 " + bFiltro + " ORDER BY Fecha, Punto, Nro ASC";
                    bSearch = true;
                }                    
                else
                {

                    //Cadena SQL  
                    myCadena = "select * from Vista_Presupuestos Where " + Campo + " = " + Buscar + " and Activo = 1 " + bFiltro  + " ORDER BY Fecha, Punto, Nro ASC";
                    bSearch = true;
                }
            }
            
            // Armo la cadena SQL
            
            // Evito que el dgvUsuarios genere columnas automáticas
            dgvComprobantes.AutoGenerateColumns = false;
            // Creo un nuevo DataTable
            DataTable mDtTable = new DataTable();
            // Le asigno al nuevo DataTable los datos de la consulta SQL
            mDtTable = clsDataBD.GetSql(myCadena);
            // Asigno el source de la grilla
            dgvComprobantes.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvComprobantes.Rows.Count;
            // Si hay filas
            if (Filas > 0)
            {
              //Cambio el estado de la bandera para que ejecute el selection changed de la grilla
                yaCargado = true;


               if (!(this.myEstado=="B"))
               {
              //Habilito los botones que puedan estar deshabilitados
                this.btnImprimir.Enabled = true;
                this.btnBuscar.Enabled = true;
                this.btnBorrar.Enabled = true;

                //CONTROLAR NIVEL PARA LA BAJA
                if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja)
                {
                    this.btnBorrar.TabStop = true;
                    this.btnBorrar.Enabled = true;
                }
                else
                {
                    this.btnBorrar.TabStop = false;
                    this.btnBorrar.Enabled = false;
                }

                //CONTROLAR NIVEL PARA PODER FACTURAR
                if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelFact)
                {
                    this.btnFacturar.Visible = true;
                    this.btnFacturar.TabStop = true;
                    this.btnFacturar.Enabled = true;
                }
                else
                {
                    this.btnFacturar.Visible = true;
                    this.btnFacturar.TabStop = false;
                    this.btnFacturar.Enabled = false;
                }

                //Boton Remitos
                this.btnRemitos.Visible = true;
                this.btnRemitos.TabStop = false;
                this.btnRemitos.Enabled = ObtenerPermiso(12,1,clsGlobales.UsuarioLogueado.IdUsuario);
               }

                //Posicionamiento
               dgvComprobantes.CurrentCell = dgvComprobantes[3, Filas - 1];

                //Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                EventArgs ea = new EventArgs();
                this.dgvComprobantes_SelectionChanged(this.dgvComprobantes, ea);
            }
            else
            {
              //Vacío la grilla de detalle
                dgvDetalle.DataSource = null;

              //Deshabilito los botones que generarían error al no tener datos la grilla Comprobantes
                this.btnModificar.Enabled = false;
                this.btnImprimir.Enabled = false;
                this.btnFacturar.Enabled = false;
                this.btnBuscar.Enabled = false;
                this.btnBorrar.Enabled = false;
                this.btnRemitos.Enabled = false;
            }

        }

        #endregion

        #region Metodo ObtenerPermiso

        private bool ObtenerPermiso(int p_Det = 0, int p_Menu = 0, int p_IdUser=0)
        {
            bool bRetorno = false;

            string myCad = "Select Habilitado from MenuOpcionesUser Where IdDetMenu = " + p_Det + " AND IdMenu = " + p_Menu
                            + " AND IdUser = " + p_IdUser ;
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

        #region Método que carga los ToolsTip del formulario

        private void CargarToolsTip()
        {
            toolTip1.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip2.SetToolTip(this.btnCancelar, "Cancelar");
            toolTip3.SetToolTip(this.btnSalir, "Salir");
            toolTip5.SetToolTip(this.btnModificar, "Editar");
            toolTip7.SetToolTip(this.btnBuscar, "Buscar");
            toolTip8.SetToolTip(this.btnImprimir, "Imprimir");
            toolTip9.SetToolTip(this.btnFacturar, "Facturar");
            toolTip10.SetToolTip(this.btnRemitos, "Remitos y Rótulos");
            
        }

        #endregion

        #region Método que calcula el subtotal de los artículos por fila

        private void CalcularSubtotal()
        {
            // Variable que guarda el resultado de la multiplicación
            double Total = 0;
            double coef = 0;
            double Cant = 0;
            double Pre = 0;
            double PreCoef = 0;
            double dIva = 0;
            string sCadSQL = "";
            double calculoiva = 0;
            string auxTotal = "";

            // Recorro la grilla y hago el cálculo
            foreach (DataGridViewRow row in dgvDetalle.Rows)
            {
                if (Convert.ToInt32(row.Cells["bExcel"].Value) == 0)
                {

                    //CALCULAR EL COEFICIENTE
                    switch (Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdTipo"].Value))
                    {
                        case 28:
                            Total = Convert.ToDouble(row.Cells["PrecioUnitario"].Value) * Convert.ToDouble(row.Cells["Pub"].Value);
                            coef = Convert.ToDouble(row.Cells["Pub"].Value);
                            break;
                        case 29:
                            Total = Convert.ToDouble(row.Cells["PrecioUnitario"].Value) * Convert.ToDouble(row.Cells["Dist"].Value);
                            coef = Convert.ToDouble(row.Cells["Dist"].Value);
                            break;
                        case 30:
                            Total = Convert.ToDouble(row.Cells["PrecioUnitario"].Value) * Convert.ToDouble(row.Cells["Rev"].Value);
                            coef = Convert.ToDouble(row.Cells["Rev"].Value);
                            break;
                    }

                    auxTotal = Total.ToString("#0.00");
                    Total = Convert.ToDouble(auxTotal);

                    PreCoef = Total;

                    // Paso a variables los datos de la fila para poder formatearlos
                    Cant = Convert.ToDouble(row.Cells["Cantidad"].Value);
                    Pre = Convert.ToDouble(row.Cells["PrecioUnitario"].Value);
                    
                    //MULTIPLICAR POR LA CANTIDAD
                    Total = Total * Convert.ToDouble(row.Cells["Cantidad"].Value);

                    // Formtaeo los valores de las columnas
                    row.Cells["Cantidad"].Value = Cant.ToString("#0");
                    // Asigno el valor a la celda
                    row.Cells["SubTotal"].Value = Total.ToString("#0.00");
                    // Asigno el valor a la celda
                    row.Cells["PrecioCoef"].Value = PreCoef.ToString("#0.00");

                    //Buscar el Porcentaje de IVA
                    sCadSQL = "Select * from Articulos Where IdArticulo = " + Convert.ToInt32(row.Cells["IdArticulo"].Value);
                    System.Data.DataTable myData = clsDataBD.GetSql(sCadSQL);

                    //Guardar Coeficientes en variables
                    foreach (DataRow rows in myData.Rows)
                    {
                        dIva = Convert.ToDouble(rows["PorcentajeIva"]);
                    }

                    // Asigno el valor a la celda
                    if (dIva > 0)
                    {
                        calculoiva = 1 + (dIva / 100);
                        row.Cells["PrecioUnitIva"].Value = (PreCoef * calculoiva).ToString("#0.00");
                    }
                    else
                    {
                        row.Cells["PrecioUnitIva"].Value = (0).ToString("#0.00");
                    }

                }
                else
                {

                    Total = Convert.ToDouble(row.Cells["PrecioUnitario"].Value) * Convert.ToDouble(row.Cells["Cantidad"].Value);

                    // Paso a variables los datos de la fila para poder formatearlos
                    Cant = Convert.ToDouble(row.Cells["Cantidad"].Value);
                    Pre = Convert.ToDouble(row.Cells["PrecioUnitario"].Value);
                    PreCoef = Convert.ToDouble(row.Cells["PrecioUnitario"].Value);

                    // Formtaeo los valores de las columnas
                    row.Cells["Cantidad"].Value = Cant.ToString("#0");
                    // Asigno el valor a la celda
                    row.Cells["SubTotal"].Value = Total.ToString("#0.00");
                    // Asigno el valor a la celda
                    row.Cells["PrecioCoef"].Value = PreCoef.ToString("#0.00");

                    //Buscar el Porcentaje de IVA
                    sCadSQL = "Select * from Articulos Where IdArticulo = " + Convert.ToInt32(row.Cells["IdArticulo"].Value);
                    System.Data.DataTable myData = clsDataBD.GetSql(sCadSQL);

                    //Guardar Coeficientes en variables
                    foreach (DataRow rows in myData.Rows)
                    {
                        dIva = Convert.ToDouble(rows["PorcentajeIva"]);
                    }

                    // Asigno el valor a la celda
                    if (dIva > 0)
                    {
                        calculoiva = 1 + (dIva / 100);
                        row.Cells["PrecioUnitIva"].Value = (PreCoef * calculoiva).ToString("#0.00");
                    }
                    else
                    {
                        row.Cells["PrecioUnitIva"].Value = (0).ToString("#0.00");
                    }

                }
            }
        }

        #endregion

        #region Método RetornarSubTotal

        //RETORNA SUBTOTAL DE PRODUCTOS EN EL DETALLE SIN DESCUENTO Y SIN FLETE
        private Double RetornarSubtotal()
        {
            // Variable que guarda el resultado de la multiplicación
            double SubTotal = 0;
            // Recorro la grilla y hago el cálculo
            foreach (DataGridViewRow row in dgvDetalle.Rows)
            {
                //MULTIPLICAR POR LA CANTIDAD
                SubTotal = SubTotal + Convert.ToDouble(row.Cells["SubTotal"].Value);
            }

            return SubTotal;
        }

        #endregion

        #region Evento SelectionChanged

        private void dgvComprobantes_SelectionChanged(object sender, EventArgs e)
        {
            if (yaCargado)
            {
                // Vacío la grilla
                dgvDetalle.DataSource = null;
                // Evito que el dgvUsuarios genere columnas automáticas
                dgvDetalle.AutoGenerateColumns = false;
                // Declaro una variable que va a guardar el Id del comprobante
                IdPresupuesto = Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["Id"].Value);                
                // Armo la cadena SQL para ejecutar el procedieminto almacenado que trae el detalle del comprobante
                string newMyCadenaSql = "Select * from Vista_Detalle_Presu_ABM where IdPresupuesto = " + IdPresupuesto + " Order by Orden ASC"; ;
                // Creo un datatable y le paso los datos de la consulta SQL
                DataTable myTabla = clsDataBD.GetSql(newMyCadenaSql);
                // Asigno a la grilla el source
                dgvDetalle.DataSource = myTabla;
                //Calcular SubTotal
                this.CalcularSubtotal();
                //Facturado? ocultar boton
                if (!(dgvComprobantes.Rows.Count == 0))
                {
                    if (Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["Facturado"].Value) ==1 )
                    {
                        btnFacturar.Enabled = false;
                        btnModificar.Enabled = false;
                    }
                    else
                    {
                        //Controlar nivel de facturacion
                        if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelFact)
                        {
                            btnFacturar.Enabled = true;
                        }
                        else
                        {
                            btnFacturar.Enabled = false;
                        }
                        btnModificar.Enabled = true;
                    }
                    //ES PENDIENTE?
                    if (Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["Pendiente"].Value) == 1)
                    {
                        if (clsGlobales.ConB == null)
                        {
                            btnFacturar.Enabled = false;
                            btnModificar.Enabled = false;
                        }
                        else
                        {
                            //Controlar nivel de facturacion
                            if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelFact)
                            {
                                btnFacturar.Enabled = true;
                            }
                            else
                            {
                                btnFacturar.Enabled = false;
                            }
                            btnModificar.Enabled = true; //ACA MODIFIQUE POR ULTIMA VEZ
                        }
                    }

                }
            }
        }

        #endregion

        #region Eventos Click

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            myEstado = "A";
            //Guardar posicion grilla
            if (dgvComprobantes.Rows.Count > 0)
            {
                // Tomo la posición actual de la fila con foco
                this.indexFila = dgvComprobantes.CurrentRow.Index;
            }
            //Llamar al Formulario de Alta
            frmABM_PedPresu myForm = new frmABM_PedPresu(this.myEstado, 0, 0, true);
            myForm.ShowDialog();
            //Cargar Grilla Nuevamente
            this.CargarGrilla("", "");
            //Reposicionar
            this.PosicionarFocoFila();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //Estadp
            myEstado = "M";
            //Guardar posicion grilla
            if (dgvComprobantes.Rows.Count > 0)
            {
                // Tomo la posición actual de la fila con foco
                this.indexFila = dgvComprobantes.CurrentRow.Index;
            }

            //¿PRESUPUESTO CON ARTICULOS DE EXCEL?....
            if (Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["Excel"].Value.ToString()) == 0)
            {
                //Formulario Modificacion
                frmABM_PedPresu myForm = new frmABM_PedPresu(this.myEstado, Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdCliente"].Value), Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["Id"].Value),true);
                myForm.ShowDialog();
            }
            else
            {
                //Formulario Modificacion
                frmABM_PedPresu_Excel myForm = new frmABM_PedPresu_Excel(this.myEstado, Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdCliente"].Value), Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["Id"].Value), true);
                myForm.ShowDialog();
            }

          //Cargar Grilla Nuevamente
           this.CargarGrilla("", "");
          //Reposicionar grilla 
           PosicionarFocoFila();

        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {

            string PresuNro = dgvComprobantes.CurrentRow.Cells["Numero"].Value.ToString();

            string myCadena = "";

   
            // Si es la última fila de la grilla
            if (dgvComprobantes.CurrentRow.Index == dgvComprobantes.Rows.Count - 1)
            {
                // capturo la posición de la fila
                this.indexFila = this.dgvComprobantes.CurrentRow.Index - 1;
            }
            else
            {
                // capturo la posición de la fila
                this.indexFila = this.dgvComprobantes.CurrentRow.Index;
            }

           //Validar el nivel del usuario para ver si tiene permisos
            if (clsGlobales.UsuarioLogueado.Nivel >=clsGlobales.cParametro.NivelBaja)
            {
                    // Confirma eliminacion? cambio de estado Activo = .F.
                    DialogResult dlResult = MessageBox.Show("Desea Eliminar el PRESUPUESTO N° " + PresuNro + " ?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    // Si confirma... cambiar estado
                    if (dlResult == DialogResult.Yes)
                    {
                        //Activo = .F. para el pedido 
                        myCadena = "DELETE FROM Presupuestos WHERE IdPresupuesto =" + IdPresupuesto;
                        clsDataBD.GetSql(myCadena);
                        //Idem para el detalle
                        myCadena = "DELETE FROM DetallePresupuestos WHERE IdPresupuesto = " + IdPresupuesto;
                        clsDataBD.GetSql(myCadena);
                        //Refrescar grilla
                        this.CargarGrilla("", "");
                    }
            }

            //Foco
            PosicionarFocoFila();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //"B"
            this.myEstado = "B";            
            // Tomo la posición actual de la fila con foco
            this.indexFila = dgvComprobantes.CurrentRow.Index;            
            // Muestro el botón cancelar
            this.btnAceptar.Visible = true;
            this.btnCancelar.Visible = true;
            this.btnModificar.Visible = false;
            this.btnBorrar.Visible = false;
            this.btnBuscar.Visible = false;
            this.btnImprimir.Visible = false;
            this.btnRemitos.Visible = false;
            this.btnFacturar.Visible = false;
            this.btnSalir.Visible = false;
            // Achico el tamañp de la grilla para mostrar los campos de búesuqeda
            dgvDetalle.Height = 171;
            // Muestro el contenedor de los campos de búsqueda
            gpbBusquedas.Visible = true;
            // Limpio los campos de búsqueda
            LimpiarCamposBusqueda();
            // Pongo el foco en el campo Número
            txtNro.Focus();

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //string myCad = "";

            //bool bUpdate = false;

            ////Traer Parametros
            //clsGlobales.cParametro.ObtenerParametros();
            ////Marcar
            //if (!(clsGlobales.cParametro.IsModPresu == 1))
            //{
            //    myCad = "UPDATE Parametros SET IsModPresu = 1, IsModPresuIdUser = " + clsGlobales.UsuarioLogueado.IdUsuario;
            //    clsDataBD.GetSql(myCad);
            //    //.T.
            //    bUpdate = true;
            //}
            //else
            //{
            //    //SI ESTA TOMADA LA TABLA POR OTRO USUARIO AVISAR, SINO CONTINUAR
            //    if (!(clsGlobales.cParametro.IsModPresuIdUser == clsGlobales.UsuarioLogueado.IdUsuario))
            //    {
            //        //Traer quien esta modificando?!
            //        myCad = "Select * from Usuarios WHERE IdUsuario = " + clsGlobales.cParametro.IsModPresuIdUser;
            //        DataTable myDataTable = clsDataBD.GetSql(myCad);
            //        //Mensaje
            //        foreach (DataRow Row in myDataTable.Rows)
            //        {
            //            MessageBox.Show("El Usuario: " + Row["Usuario"].ToString() + " esta realizando acciones!. Reintente en un momento!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            return;
            //        }

            //        bUpdate = false;
            //    }
            //    else
            //    {
            //        bUpdate = true;
            //    }
            //}

            //Estadp
            myEstado = "M";
            //Guardar posicion grilla
            if (dgvComprobantes.Rows.Count > 0)
            {
                // Tomo la posición actual de la fila con foco
                this.indexFila = dgvComprobantes.CurrentRow.Index;
            }

            //¿PRESUPUESTO CON ARTICULOS DE EXCEL?....
            if (Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["Excel"].Value) == 0)
            {
                //Formulario Modificacion
                frmABM_PedPresu myForm = new frmABM_PedPresu(this.myEstado, Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdCliente"].Value), Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["Id"].Value), true);
                myForm.ShowDialog();
            }
            else
            {
                //Formulario Modificacion
                frmABM_PedPresu_Excel myForm = new frmABM_PedPresu_Excel(this.myEstado, Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdCliente"].Value), Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["Id"].Value), true);
                myForm.ShowDialog();
            }

            ////SI CAMBIE PARAMETROS...RETORNAR A 0.
            //if (bUpdate)
            //{
            //    //Desmarcar (liberar la pantalla)
            //    myCad = "UPDATE Parametros SET IsModPresu = 0, IsModPresuIdUser = 0";
            //    clsDataBD.GetSql(myCad);
            //}

            //Cargar Grilla Nuevamente
            this.CargarGrilla("", "");
            //Reposicionar grilla 
            PosicionarFocoFila();


            ////PREGUNTAR SI ESTA CONFIGURADO EN PARAMETROS
            //if (clsGlobales.cParametro.Imprimir)
            //{
            //    DialogResult dlResult = MessageBox.Show("¿Desea imprimir el Presupuesto N° " + dgvComprobantes.CurrentRow.Cells["Numero"].Value.ToString() + " ?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    // Si confirma... cambiar estado
            //    if (dlResult == DialogResult.No)
            //    {
            //        return;
            //    }
            //}

            //int dgvFilas = 0;

            ////Data Set
            //dsReportes oDsPresu = new dsReportes();

            ////Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            //dgvFilas = dgvDetalle.Rows.Count;

            //for (int i = 0; i < dgvFilas; i++)
            //{
            //    oDsPresu.Tables["dtPedidos"].Rows.Add
            //    (new object[] { dgvDetalle[1,i].Value.ToString(),
            //            dgvDetalle[2,i].Value.ToString(),
            //            dgvDetalle[4,i].Value.ToString(),
            //            dgvDetalle[6,i].Value.ToString()});

            //}

            ////Objeto Reporte
            //rptPresu oRepPresu = new rptPresu();
            ////Cargar Reporte                                    
            //oRepPresu.Load(Application.StartupPath + "\\rptPresu.rpt");
            ////Establecer el DataSet como DataSource
            //oRepPresu.SetDataSource(oDsPresu);
            ////Pasar como parámetro nombre del reporte
            //clsGlobales.myRptDoc = oRepPresu;

            //oRepPresu.DataDefinition.FormulaFields["TipoComprobante"].Text = "'" + "X" + "'";

            //oRepPresu.DataDefinition.FormulaFields["NroComp"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Numero"].Value.ToString() + "'";

            ////Otras
            //oRepPresu.DataDefinition.FormulaFields["Fecha"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Fecha"].Value.ToString() + "'";

            ////Comprobante y pie
            //oRepPresu.DataDefinition.FormulaFields["linea-01"].Text = "' Cliente: " + dgvComprobantes.CurrentRow.Cells["Cliente"].Value.ToString() + "'";
            //oRepPresu.DataDefinition.FormulaFields["linea-02"].Text = "' Domicilio: " + dgvComprobantes.CurrentRow.Cells["Direccion"].Value.ToString() + "'";
            //oRepPresu.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + "( " + dgvComprobantes.CurrentRow.Cells["CP"].Value.ToString() + " ) " + dgvComprobantes.CurrentRow.Cells["Localidad"].Value.ToString() + ", " + dgvComprobantes.CurrentRow.Cells["Provincia"].Value.ToString() + "'";
            //oRepPresu.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + "(" + dgvComprobantes.CurrentRow.Cells["CP"].Value.ToString() + ") " + dgvComprobantes.CurrentRow.Cells["Localidad"].Value.ToString() + " ( " + dgvComprobantes.CurrentRow.Cells["Provincia"].Value.ToString() + " )'"; 
            //oRepPresu.DataDefinition.FormulaFields["linea-04"].Text = "' CUIT: " + dgvComprobantes.CurrentRow.Cells["CUIT"].Value.ToString() + "'";
            //oRepPresu.DataDefinition.FormulaFields["linea-05"].Text = "' Condición Compra: " + dgvComprobantes.CurrentRow.Cells["FormaPago"].Value.ToString() + "'";

            //oRepPresu.DataDefinition.FormulaFields["linea-06"].Text = "' Telefono: " + dgvComprobantes.CurrentRow.Cells["Telefono"].Value.ToString() + "'";
            //oRepPresu.DataDefinition.FormulaFields["linea-07"].Text = "' Transporte: " + dgvComprobantes.CurrentRow.Cells["Transporte"].Value.ToString() + "'";

            //oRepPresu.DataDefinition.FormulaFields["Dto"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Dto"].Value.ToString() + "'";


            //oRepPresu.DataDefinition.FormulaFields["Flete"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Flete"].Value.ToString() + "'";

            //double SubTotal = RetornarSubtotal();
            //double Total = 0;
            //oRepPresu.DataDefinition.FormulaFields["SubTotal"].Text = "'" + SubTotal.ToString("#0.00") + "'";

            ////Asignamos y luego vemos descuento y flete
            //Total = SubTotal;

            ////Aplicar Descuento y Sumar Flete
            //if (Convert.ToDouble(dgvComprobantes.CurrentRow.Cells["Dto"].Value) != 0)
            //{
            //    Total = SubTotal - ((SubTotal * Convert.ToDouble(dgvComprobantes.CurrentRow.Cells["Dto"].Value) / 100));
            //}

            ////Suma Flete
            //if (Convert.ToDouble(dgvComprobantes.CurrentRow.Cells["Flete"].Value) != 0)
            //{
            //    Total = Total + Convert.ToDouble(dgvComprobantes.CurrentRow.Cells["Flete"].Value);
            //}

            //double dtoimpo = (SubTotal * Convert.ToDouble(dgvComprobantes.CurrentRow.Cells["Dto"].Value)) / 100;
            //oRepPresu.DataDefinition.FormulaFields["DtoImpo"].Text = "'" + dtoimpo.ToString("#0.00") + "'";
            //oRepPresu.DataDefinition.FormulaFields["SubTotDto"].Text = "'" + (SubTotal - dtoimpo).ToString("#0.00") + "'";

            //oRepPresu.DataDefinition.FormulaFields["SubTotal"].Text = "'" + SubTotal.ToString("#0.00") + "'";
            //oRepPresu.DataDefinition.FormulaFields["TotalImpo"].Text = "'" + Total.ToString("#0.00") + "'";

            ////Mostrar el reporte  
            //frmShowReports myReportForm = new frmShowReports();
            //myReportForm.Text = this.Text;
            //myReportForm.ShowDialog();           
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
   
            // Tomo la posición actual de la fila con foco
            this.indexFila = dgvComprobantes.CurrentRow.Index; 
            //Obtener Id Cliente
            int IdCliente = Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdCliente"].Value);
            //Llamar al formulario de facturacion
            frmVentasFacturacionE myFactu = new frmVentasFacturacionE(IdCliente, IdPresupuesto, this.dgvComprobantes,this.dgvDetalle,RetornarSubtotal());
            myFactu.ShowDialog();
            // Cargo la grilla
            this.CargarGrilla("", "");
            //Foco
            PosicionarFocoFila();
            //ELIMINAR ARCHIVO
            BorrarArchivo(Application.StartupPath + "\\AFIP.jpg");

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Recargar
            if (this.myEstado == "B" && bSearch)
            {
                //Limpiar Controles
                this.LimpiarCamposBusqueda();
                // Cargo las localidades
                this.CargarGrilla("", "");
                // Oculto los campos de búsqueda
                gpbBusquedas.Visible = false;
                // Estiro la grilla detalle para que tape los campos de búesuqeda
                dgvDetalle.Height = 232;
                // Cambio el estado del formulario a agregar. G.
                this.myEstado = "C";
                //// Oculto el botón cancelar
                this.btnCancelar.Visible = false;
                this.btnAceptar.Visible = false;
                this.btnModificar.Visible = true;
                this.btnBorrar.Visible = true;
                this.btnBuscar.Visible = true;
                this.btnImprimir.Visible = true;
                this.btnRemitos.Visible = true;
                this.btnFacturar.Visible = true;
                this.btnSalir.Visible = true;
                //Foco
                PosicionarFocoFila();
            }
            else
            {
                // Cambio el estado del formulario a agregar. G.
                this.myEstado = "C";
                // Oculto los campos de búsqueda
                gpbBusquedas.Visible = false;
                // Estiro la grilla detalle para que tape los campos de búesuqeda
                dgvDetalle.Height = 232;
                // Oculto el botón cancelar
                this.btnCancelar.Visible = false;
                this.btnAceptar.Visible = false;
                this.btnModificar.Visible = true;
                this.btnBorrar.Visible = true;
                this.btnBuscar.Visible = true;
                this.btnImprimir.Visible = true;
                this.btnRemitos.Visible = true;
                this.btnFacturar.Visible = true;
                this.btnSalir.Visible = true;
                // Pongo el foco de la fila
                PosicionarFocoFila();
            }

            //.F.
            bSearch = false;
        }

        #endregion

        #region Metodo BorrarArchvo

        //ELIMINAR ARCHIVO
        public void BorrarArchivo(String archivo)
        {
            if (System.IO.File.Exists(@archivo))
            {
                try
                {
                    System.IO.File.Delete(@archivo);
                }
                catch (System.IO.IOException e)
                {
                    return;
                }
            }
        }

        #endregion

        #region Eventos TextChanged

        private void txtNro_TextChanged(object sender, EventArgs e)
        {
            if (!(txtNro.Text == ""))
            {
                CargarGrilla(this.txtNro.Text, "Nro");
            }

        }

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
            if (!(txtCliente.Text == ""))
            {
                CargarGrilla(this.txtCliente.Text, "RazonSoCli");
            }
        }

        #endregion

        #region Eventos Enter

        private void txtCliente_Enter(object sender, EventArgs e)
        {
            // Si el otro campo de búsqueda tiene algo, lo borro
            if (!(txtNro.Text == ""))
            {
                // Limpio el otro campo de búsqueda
                txtNro.Text = "";
            }
        }

        private void txtNro_Enter(object sender, EventArgs e)
        {
            // Si el otro campo de búsqueda tiene algo, lo borro
            if (!(txtCliente.Text == ""))
            {
                // Limpio el otro campo de búsqueda
                txtCliente.Text = "";
            }
        }

        #endregion

        private void btnRemitos_Click(object sender, EventArgs e)
        {

            //string myCad = "";

            ////Traer Parametros
            //clsGlobales.cParametro.ObtenerParametros();
            ////Marcar
            //if (clsGlobales.cParametro.IsModPresu == 1 && clsGlobales.cParametro.IsModPresuIdUser != clsGlobales.UsuarioLogueado.IdUsuario)
            //{
            //    //Traer quien esta modificando?!
            //    myCad = "Select * from Usuarios WHERE IdUsuario = " + clsGlobales.cParametro.IsModPresuIdUser;
            //    DataTable myDataTable = clsDataBD.GetSql(myCad);
            //    //Mensaje
            //    foreach (DataRow Row in myDataTable.Rows)
            //    {
            //        MessageBox.Show("El Usuario: " + Row["Usuario"].ToString() + " esta realizando acciones!. Reintente en un momento!", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }
            //}

            //VERIFICAR ID
            switch(clsGlobales.UsuarioLogueado.IdUsuario)
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
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            //Tomar el Id
            if (this.dgvComprobantes.Rows.Count > 0)
            {
                IdPresuSearch = Convert.ToInt32(this.dgvComprobantes.CurrentRow.Cells["Id"].Value.ToString());
            }
            // Cambio mi estado
            this.myEstado = "C";
            // Lleno nuevamente la grilla
            this.CargarGrilla("", "");
            // Oculto los campos de búsqueda
            gpbBusquedas.Visible = false;
            // Estiro la grilla detalle para que tape los campos de búesuqeda
            dgvDetalle.Height = 232;
            //// Oculto el botón cancelar
            this.btnCancelar.Visible = false;
            this.btnAceptar.Visible = false;
            this.btnModificar.Visible = true;
            this.btnBorrar.Visible = true;
            this.btnBuscar.Visible = true;
            this.btnImprimir.Visible = true;
            this.btnRemitos.Visible = true;
            this.btnSalir.Visible = true;
            //Id >0? Solo cuando busca reposiciona por ID
            if (!(IdPresuSearch == 0 && bSearch))
            {
                ReposicionarById();
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

        #region Reposicionar Grilal por Id

        //REPOSICIONA GRILLA POR ID
        private void ReposicionarById()
        {
            //recorrer
            foreach (DataGridViewRow myRow in this.dgvComprobantes.Rows)
            {
                if (Convert.ToInt32(myRow.Cells["Id"].Value.ToString()) == IdPresuSearch)
                {
                    // Devuelvo el foco a la fila de la grilla desde donde se llamó
                    this.dgvComprobantes.CurrentCell = dgvComprobantes[3, myRow.Index];

                    // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                    EventArgs ea = new EventArgs();
                    this.dgvComprobantes_SelectionChanged(this.dgvComprobantes, ea);

                    //Salir
                    break;
                }
            }
        }

        #endregion

        private void rdbT_CheckedChanged(object sender, EventArgs e)
        {
            this.bFiltro = "";
            this.CargarGrilla("", "");
        }

        private void rbtF_CheckedChanged(object sender, EventArgs e)
        {
            this.bFiltro = "AND Facturado = 1";
            this.CargarGrilla("", "");
        }

        private void rbtFact_CheckedChanged(object sender, EventArgs e)
        {
            this.bFiltro = "AND Pendiente = 1";
            this.CargarGrilla("", "");
        }

        private void rbtSFact_CheckedChanged(object sender, EventArgs e)
        {
            this.bFiltro = "AND Facturado =  0";
            this.CargarGrilla("", "");
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Prama.Clases;

namespace Prama.Formularios.Ventas
{
    public partial class frmLista_Pedidos : Form
    {

     // Variable que almacena el Id del tipo de comprobante
       int IdPedido = 0;
        string myEstado = "C";
        int indexFila = 0;

      // Bandera que controla la carga de la segunda grilla
        bool yaCargado = false;

        #region CONSTRUCTOR

        public frmLista_Pedidos()
        {
            InitializeComponent();
        }

        #endregion

        #region EVENTO LOAD

        private void frmLista_Pedidos_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            // Cargo los toolstip de los botones
            CargarToolsTip();
            // Cargo la grilla
             // Cambio el alto de la grilla detalle para que tape los campos de búsqueda
            dgvDetalle.Height = 232;
            //Ordenamiento .F.
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
            //boton
            this.CargarGrilla("", "");

            //Timer
            TriggerTime.Start();
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

        #region Método que limpia los campos de búsqueda del formulario

        private void LimpiarCamposBusqueda()
        {
            txtCliente.Text = "";
            txtNro.Text = "";
        }

        #endregion

        #region Método que devuelve el foco a la fila desde donde se presionó algún botón

        private void PosicionarFocoFila()
        {
            if (dgvComprobantes.Rows.Count != 0 && dgvComprobantes.Rows.Count > this.indexFila)
            {
                // Devuelvo el foco a la fila de la grilla desde donde se llamó
                if (this.indexFila == -1)
                {
                    this.indexFila = 0;
                }
                this.dgvComprobantes.CurrentCell = dgvComprobantes[3, this.indexFila];
                // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                EventArgs ea = new EventArgs();
                this.dgvComprobantes_SelectionChanged(this.dgvComprobantes, ea);
            }
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
                myCadena = "SELECT * FROM Vista_Pedidos WHERE Activo = 1";
            }
            else
            {
                if (Campo == "RazonSoCli")
                {
                  //Cadena SQL 
                    myCadena = "select * from Vista_Pedidos where " + Campo + " like '" + Buscar + "%' and Activo = 1  order by " + Campo;
                }
                else
                {
                    //Cadena SQL 
                    myCadena = "select * from Vista_Pedidos Where " + Campo + " = " +  Buscar + " and Activo = 1";
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

              //Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                EventArgs ea = new EventArgs();
                this.dgvComprobantes_SelectionChanged(this.dgvComprobantes, ea);

              //Habilito los botones que puedan estar deshabilitados
                this.btnModificar.Enabled = true;
                this.btnPresu.Enabled = true;
                this.btnImprimir.Enabled = true;
                this.btnBuscar.Enabled = true;
                this.btnBorrar.Enabled = true;                
            }
            else
            {
              //Vacío la grilla de detalle
                dgvDetalle.DataSource = null;

              //Deshabilito los botones que generarían error al no tener datos la grilla Comprobantes
                this.btnModificar.Enabled = false;
                this.btnPresu.Enabled = false;
                this.btnImprimir.Enabled = false;
                this.btnBuscar.Enabled = false;
                this.btnBorrar.Enabled = false;
            }

        }

        #endregion

        #region Método que carga los ToolsTip del formulario

        private void CargarToolsTip()
        {
            toolTip1.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip2.SetToolTip(this.btnCancelar, "Cancelar");
            toolTip3.SetToolTip(this.btnSalir, "Salir");
            toolTip4.SetToolTip(this.btnAgregar, "Nuevo");
            toolTip5.SetToolTip(this.btnModificar, "Editar");
            toolTip6.SetToolTip(this.btnPresu, "Cerrar Pedido");
            toolTip7.SetToolTip(this.btnBuscar, "Buscar");
            toolTip8.SetToolTip(this.btnImprimir, "Imprimir");
            toolTip9.SetToolTip(this.btnBorrar, "Borrar");
            toolTip10.SetToolTip(this.btnExcel, "Nuevo (Origen Excel)");
        }

        #endregion

        #region Metodo: GuardarPresupuesto

        private void GuardarPresupuesto(clsPresupuestos pPresupuesto)
        {


            //INSERT A LA TABLA DE PEDIDOS
            string myCadena = "";

            try
            {

                //Alta de Articulos
                myCadena = "INSERT INTO Presupuestos (IdPresupuesto," +
                                                " IdCliente," +
                                                " IdFormaPago," +
                                                " Punto," +
                                                " Nro," +
                                                " PuntoNro," +
                                                " IdTransporte," +
                                                " Fecha," +
                                                " Comentario," + 
                                                " Dto," +
                                                " Flete," +
                                                " Activo," +
                                                " Facturado," +
                                                " Excel," +
                                                " Pendiente," +
                                                " Codigo_Correo" +
                                                 ") values (" + pPresupuesto.IdPresupuesto + ","
                                                                + pPresupuesto.IdCliente + ","
                                                                + pPresupuesto.IdFormaPago + ","
                                                                + pPresupuesto.Punto + ","
                                                                + pPresupuesto.Nro + ",'"
                                                                + pPresupuesto.PuntoNro + "',"
                                                                + pPresupuesto.IdTransporte + ",'"
                                                                + pPresupuesto.Fecha.ToShortDateString() + "','"
                                                                + pPresupuesto.Comentario + "'," 
                                                                + pPresupuesto.Dto.ToString().Replace(",", ".") + ","
                                                                + pPresupuesto.Flete.ToString().Replace(",", ".") + ","
                                                                + pPresupuesto.Activo + ","
                                                                + pPresupuesto.Facturado + "," 
                                                                + pPresupuesto.Excel + ","
                                                                + pPresupuesto.Pendiente + ",'"  
                                                                + pPresupuesto.Codigo_Correo + "')";

                //GUARDAR EN PRESUPUESTOS
                clsDataBD.GetSql(myCadena);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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
            string sCadSQL =""; 
            double calculoiva=0;
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

                    //MULTIPLICAR POR LA CANTIDAD
                    Total = Total * Convert.ToDouble(row.Cells["Cantidad"].Value);

                    // Paso a variables los datos de la fila para poder formatearlos
                    Cant = Convert.ToDouble(row.Cells["Cantidad"].Value);
                    Pre = Convert.ToDouble(row.Cells["PrecioUnitario"].Value);


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
                       row.Cells["PreUnitIva"].Value = (PreCoef * calculoiva).ToString("#0.00");
                    }
                    else
                    {
                        row.Cells["PreUnitIva"].Value = (0).ToString("#0.00");
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
                        row.Cells["PreUnitIva"].Value = (PreCoef * calculoiva).ToString("#0.00");
                    }
                    else
                    {
                        row.Cells["PreUnitIva"].Value = (0).ToString("#0.00");
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
        
        #region Eventos de la Grilla

        private void dgvComprobantes_SelectionChanged(object sender, EventArgs e)
        {
            if (yaCargado)
            {
                //VACIO EL DATAGRIDVIEW? VOLVER. N.
                if (dgvComprobantes.RowCount == 0)
                {
                    return;
                }

                // Vacío la grilla
                dgvDetalle.DataSource = null;
                // Evito que el dgvUsuarios genere columnas automáticas
                dgvDetalle.AutoGenerateColumns = false;
                // Declaro una variable que va a guardar el Id del comprobante
                IdPedido = Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["Id"].Value);
                // Armo la cadena SQL para ejecutar el procedieminto almacenado que trae el detalle del comprobante
                string newMyCadenaSql = "Select * from Vista_Detalle_Pedido_ABM where IdPedido = " + IdPedido + " Order by Orden ASC";
                // Creo un datatable y le paso los datos de la consulta SQL
                DataTable myTabla = clsDataBD.GetSql(newMyCadenaSql);
                // Asigno a la grilla el source
                dgvDetalle.DataSource = myTabla;
                //Calcular SubTotal
                this.CalcularSubtotal();
            }
        }

        #endregion

        #region Eventos de los botones

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //Timer
            TriggerTime.Stop();
            TriggerTime.Enabled = false;
            //Cerrar
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //Stop Trigger
            TriggerTime.Stop();

            /*  myEstado = "A";
            // Tomo la posición actual de la fila con foco
            if (!(dgvComprobantes.Rows.Count == 0))
            {
               this.indexFila = dgvComprobantes.CurrentRow.Index;
            }
  
            //Llamar al Formulario de Alta
            frmABM_PedPresu myForm = new frmABM_PedPresu(this.myEstado, 0, 0, false);
            myForm.ShowDialog();
            //Cargar Grilla Nuevamente
            this.CargarGrilla("", "");
            //Reposicionar
            PosicionarFocoFila();*/

            myEstado = "A";

            // Tomo la posición actual de la fila con foco
            if (!(dgvComprobantes.Rows.Count == 0))
            {
                this.indexFila = dgvComprobantes.CurrentRow.Index;
            }
            //Llamar al Formulario de Alta
            frmABM_PedPresu_Excel myForm = new frmABM_PedPresu_Excel(this.myEstado, 0, 0, false, null);
            myForm.ShowDialog();

            //Cargar Grilla Nuevamente
            this.CargarGrilla("", "");
            //Reposicionar
            PosicionarFocoFila();

            //Stop Trigger
            TriggerTime.Start();

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Timer
            TriggerTime.Stop();
            // Tomo la posición actual de la fila con foco
            this.indexFila = dgvComprobantes.CurrentRow.Index;            
            // Muestro el botón cancelar
            this.btnCancelar.Visible = true;
            // Achico el tamañp de la grilla para mostrar los campos de búesuqeda
            dgvDetalle.Height = 171;
            // Muestro el contenedor de los campos de búsqueda
            gpbBusquedas.Visible = true;
            // Limpio los campos de búsqueda
            LimpiarCamposBusqueda();
            // Pongo el foco en el campo Número
            txtNro.Focus();
            //Timer
            TriggerTime.Start();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Timer
            TriggerTime.Stop();
            // Limpio los campos de búsqueda
            LimpiarCamposBusqueda();
            // Cargo la grilla con todos los datos
            CargarGrilla("", "");
            // Oculto los campos de búsqueda
            gpbBusquedas.Visible = false;
            // Estiro la grilla detalle para que tape los campos de búesuqeda
            dgvDetalle.Height = 232;
            // Oculto el botón cancelar
            this.btnCancelar.Visible = false;
            //Timer
            TriggerTime.Start();
            // Pongo el foco de la fila
            PosicionarFocoFila();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //Estadp
                myEstado = "M";
           
            //Stop Trigger
                TriggerTime.Stop();

            //Set IdUsuario en EditMode   
                bool bEstado = EstaEditable(); 
                if (!(bEstado))
                {
                    IdPedido = Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["Id"].Value);
                    clsDataBD.GetSql("UPDATE Pedidos SET EditMode = " + clsGlobales.UsuarioLogueado.IdUsuario + " WHERE IdPedido = " + IdPedido);
                }
                else
                {
                    MessageBox.Show("El Pedido esta siendo editado por otro usuario. Verifique e intente luego...", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            // Tomo la posición actual de la fila con foco
                this.indexFila = dgvComprobantes.CurrentRow.Index;

            //ES PEDIDO EXCEL?
                if (Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["Excel"].Value) == 0)
                {
                  //Modo Edicion
                    frmABM_PedPresu myForm = new frmABM_PedPresu(this.myEstado, Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdCliente"].Value), Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["Id"].Value), false);
                    myForm.ShowDialog();
                }
                else
                {
                  //Modo Edicion
                    frmABM_PedPresu_Excel myForm = new frmABM_PedPresu_Excel(this.myEstado, Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdCliente"].Value), Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["Id"].Value), false, null, false);
                    myForm.ShowDialog();
                }
            //UPDATE Pedidos                
                clsDataBD.GetSql("UPDATE Pedidos SET EditMode = 0 WHERE IdPedido = " + IdPedido);
            //Timer
                TriggerTime.Start();
            //Cargar Grilla Nuevamente
                this.CargarGrilla("", "");
            //Reposicionar
                PosicionarFocoFila();


        }

        //FUNCION ESTAEDITABLE
        private bool EstaEditable()
        {
            //Al iniciar .F.
            bool retorno = false;

            string mysql = "Select EditMode from Pedidos Where IdPedido = " + IdPedido;
            DataTable myData = clsDataBD.GetSql(mysql);

            foreach (DataRow myRow in myData.Rows)
            {
                if (Convert.ToInt32(myRow["EditMode"])!=0)
                {
                    //Cambia a .T.
                    retorno = true;
                }
            }

            //Retorna variable
            return retorno;
 
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

            //Estadp
            myEstado = "M";
            //Timer
            TriggerTime.Stop();

            // Tomo la posición actual de la fila con foco
            this.indexFila = dgvComprobantes.CurrentRow.Index;

            //ES PEDIDO EXCEL?
            if (Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["Excel"].Value) == 0)
            {
                //Modo Edicion
                frmABM_PedPresu myForm = new frmABM_PedPresu(this.myEstado, Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdCliente"].Value), Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["Id"].Value), false);
                myForm.ShowDialog();
            }
            else
            {
                //Modo Edicion
                frmABM_PedPresu_Excel myForm = new frmABM_PedPresu_Excel(this.myEstado, Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdCliente"].Value), Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["Id"].Value), false, null, true);
                myForm.ShowDialog();
            }

            //Timer
            TriggerTime.Start();
            //Cargar Grilla Nuevamente
            this.CargarGrilla("", "");
            //Reposicionar
            PosicionarFocoFila();

        }

        private void btnPresu_Click(object sender, EventArgs e)
        {
            DataTable myDataPresu = new DataTable();
            DataTable myDataDetalle = new DataTable();

            string PedNro = dgvComprobantes.CurrentRow.Cells["Numero"].Value.ToString();
            string myCadena = "";
            //Timer
            TriggerTime.Stop();
            //CERRAR EL PRESUPUESTO ACTUAL
            DialogResult dlResult = MessageBox.Show("Desea CERRAR el PEDIDO N° " + PedNro + " ?. Esta acción < NO > se pude deshacer!", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Si confirma... cambiar estado
            if (dlResult == DialogResult.Yes)
            {
                //Pasar Pedido a Presupuestos //// ....
                myCadena = "Select * from Pedidos Where IdPedido = " + IdPedido;
                myDataPresu = clsDataBD.GetSql(myCadena);

                //NUEVO OBJETO PRESUPUESTOS
                clsPresupuestos myPresu = new clsPresupuestos();

                //RECORRER GRILLA Y GUARDAR EN EL OBJETO
                foreach (DataRow filas in myDataPresu.Rows)
                {
                    /*Guardar en Matriz*/
                     myPresu.IdPresupuesto = clsDataBD.RetornarUltimoId("Presupuestos","IdPresupuesto")+1;
                     myPresu.IdCliente  = Convert.ToInt32(filas["IdCliente"].ToString());
                     myPresu.IdFormaPago = Convert.ToInt32(filas["IdFormaPago"].ToString());
                     myPresu.Punto = Convert.ToInt32(filas["Punto"].ToString());
                     // ** 29/05
                     // myPresu.Nro = clsDataBD.RetornarMax("Presupuestos","Nro")+1;
                     // **
                     myPresu.Nro = clsDataBD.getUltComp("Ult_Presupuesto", clsGlobales.cParametro.PtoVtaPorDefecto, 0) + 1; //
                     // **
                     myPresu.PuntoNro = myPresu.Punto.ToString("D4") + "-" + myPresu.Nro.ToString("D8");
                     myPresu.IdTransporte = Convert.ToInt32(filas["IdTransporte"].ToString());
                     myPresu.Fecha = DateTime.Now;
                     myPresu.Comentario = filas["Comentario"].ToString();
                     myPresu.Dto  = Convert.ToDouble(filas["Dto"].ToString());
                     myPresu.Flete  = Convert.ToDouble(filas["Flete"].ToString());
                     myPresu.Facturado = 0;
                     myPresu.Activo = 1;
                     myPresu.Excel = Convert.ToInt32(filas["Excel"].ToString());
                     myPresu.Pendiente = 0;
                     myPresu.Codigo_Correo = "No establecido";
                }

                //Guardar el Presupuesto
                GuardarPresupuesto(myPresu);

                //Actualizar el numero de presupuesto en Tabla AFIP
                string mySQL = "UPDATE PuntosVentaAFIP SET Ult_Presupuesto = " + myPresu.Nro + " WHERE Punto = " + clsGlobales.cParametro.PtoVtaPorDefecto;
                clsDataBD.GetSql(mySQL);
                
                //GUARDAR EL DETALLE
                myCadena = "Select * from Vista_Detalle_Pedido_ABM where IdPedido = " + IdPedido;
                myDataDetalle = clsDataBD.GetSql(myCadena);

                clsDetallePresupuestos myDetallePresupuesto = new clsDetallePresupuestos();

                foreach (DataRow filas in myDataDetalle.Rows)
                {                    
                        myDetallePresupuesto.IdDetPresupuesto = clsDataBD.RetornarUltimoId("DetallePresupuestos","IdDetPresupuesto")+1;
                        myDetallePresupuesto.IdPresupuesto = myPresu.IdPresupuesto;
                        myDetallePresupuesto.IdArticulo = Convert.ToInt32(filas["IdArticulo"].ToString());
                        myDetallePresupuesto.Codigo_Articulo = filas["CodigoArticulo"].ToString();
                        myDetallePresupuesto.Cantidad = Convert.ToInt32(filas["Cantidad"].ToString());
                        myDetallePresupuesto.Descripcion = filas["Articulo"].ToString(); 
                        myDetallePresupuesto.PrecioUnitario = Convert.ToDouble(filas["Precio"].ToString());
                        myDetallePresupuesto.Activo = 1;
                        myDetallePresupuesto.Excel = Convert.ToInt32(filas["Excel"].ToString());
                        myDetallePresupuesto.Orden = Convert.ToInt32(filas["Orden"].ToString());
  
                        //GUARDAR DETALLE DEL PRESUPUESTO
                        GuardarDetallePresupuesto(myDetallePresupuesto);
                }

                //Quitar el PEDIDO
                myCadena = "DELETE FROM Pedidos WHERE IdPedido =" + IdPedido;
                clsDataBD.GetSql(myCadena);
                //Quitar el DETALLE
                myCadena = "DELETE FROM DetallePedidos WHERE IdPedido =" + IdPedido;
                clsDataBD.GetSql(myCadena);
                //Timer
                TriggerTime.Start();
                //Refrescar grilla
                this.CargarGrilla("", "");
                //Posicionar en grilla
                this.PosicionarFocoFila();

            }
        }

        #region Metodo: GuardarDetallePresupuesto

        private void GuardarDetallePresupuesto(clsDetallePresupuestos pDetPresu)
        {

                //Cargar el producto en la tabla
                //Guardar la factura
                string myCadSQL = "INSERT INTO DetallePresupuestos (IdDetPresupuesto, " +
                                                           " IdPresupuesto," +
                                                           " IdArticulo," +
                                                           " Codigo_Articulo," +
                                                           " Cantidad," +
                                                           " Descripcion," +
                                                           " PrecioUnitario," +
                                                           " Activo," + 
                                                           " Excel," + 
                                                           " Orden)" +
                                                           "  values (" + pDetPresu.IdDetPresupuesto + "," +
                                                                             pDetPresu.IdPresupuesto + "," +
                                                                             pDetPresu.IdArticulo + ",'" +
                                                                             pDetPresu.Codigo_Articulo + "'," +
                                                                             pDetPresu.Cantidad.ToString().Replace(",", ".") + ",'" +
                                                                             pDetPresu.Descripcion + "'," +
                                                                             pDetPresu.PrecioUnitario.ToString().Replace(",", ".") + "," +
                                                                             pDetPresu.Activo + "," +
                                                                             pDetPresu.Excel + "," +
                                                                             pDetPresu.Orden + ")";


                clsDataBD.GetSql(myCadSQL);

            
        }

        #endregion

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
            CargarGrilla(this.txtCliente.Text, "RazonSoCli");
        }

        private void txtNro_TextChanged(object sender, EventArgs e)
        {
            CargarGrilla(this.txtNro.Text, "Nro");
        }

        private void txtNro_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Solo enteros
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }         
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            string PedNro = dgvComprobantes.CurrentRow.Cells["Numero"].Value.ToString();
            string myCadena = "";
            //Timer
            TriggerTime.Stop();
           //Validar el nivel del usuario para ver si tiene permisos
            if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja)
            {
              //SI ESTA ABIERTO EL PEDIDO NO LO PUEDE ELIMINAR
                    // Confirma eliminacion? cambio de estado Activo = .F.
                    DialogResult dlResult = MessageBox.Show("Desea Eliminar el PEDIDO N° " + PedNro + " ?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    // Si confirma... cambiar estado
                    if (dlResult == DialogResult.Yes)
                    {
                        //Activo = .F. para el pedido 
                        myCadena = "DELETE FROM Pedidos WHERE IdPedido =" + IdPedido;
                        clsDataBD.GetSql(myCadena);
                        //Idem para el detalle
                        myCadena = "DELETE FROM DetallePedidos WHERE IdPedido = " + IdPedido;
                        clsDataBD.GetSql(myCadena);
                        //Refrescar grilla
                        this.CargarGrilla("", "");
                        //Posicionar en grilla
                        this.PosicionarFocoFila();
                    }
            }
            else
            {
                // El usuario no es de nivel 5, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para Eliminar un Pedido!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Timer
            TriggerTime.Start();
        }

        #endregion

        #region Evento Boton Excel Click

        private void btnExcel_Click(object sender, EventArgs e)
        {
            myEstado = "A";
            // Tomo la posición actual de la fila con foco
            if (!(dgvComprobantes.Rows.Count == 0))
            {
                this.indexFila = dgvComprobantes.CurrentRow.Index;
            }
            //Llamar al Formulario de Alta
            frmABM_PedPresu_Excel myForm = new frmABM_PedPresu_Excel(this.myEstado, 0, 0, false);
            myForm.ShowDialog();
            //Cargar Grilla Nuevamente
            this.CargarGrilla("", "");
            //Reposicionar
            PosicionarFocoFila();
        }

        #endregion

        private void TriggerTime_Tick(object sender, EventArgs e)
        {
   
           this.CargarGrilla("","");
           // Pongo el foco de la fila
           if (!(IdPedido == 0))
           {
               //Reposicionar
               ReposicionarById();
           }
           else
           {
               //Foco
               PosicionarFocoFila();
           }


        }

        #region Reposicionar Grilla por Id

        //REPOSICIONA GRILLA POR ID
        private void ReposicionarById()
        {
            //recorrer
            foreach (DataGridViewRow myRow in this.dgvComprobantes.Rows)
            {
                if (Convert.ToInt32(myRow.Cells["Id"].Value.ToString()) == IdPedido)
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

        private void frmLista_Pedidos_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Timer
            TriggerTime.Stop();
            TriggerTime.Enabled = false;
        }

      }


}

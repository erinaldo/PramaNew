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
    public partial class frmComprasComprobantes : Form
    {

        #region Variables del formulario

        // Declaro la variable que almacena desde que formulario se hizo la llamada
        string Desde = "";
        // Declaro la variable que va a almacenar la cadena SQL
        string myCadena = "";
        // Variable que almacena el Id del tipo de comprobante
        int TipoComprobante = 0;
        // Bandera que controla la carga de la segunda grilla
        bool yaCargado = false;

        #endregion

        #region Contructor del formulario

        public frmComprasComprobantes(string Llamada)
        {
            // Le paso a la variable el formulario desde el que se hizo la llamada
            Desde = Llamada;
            
            InitializeComponent();
        }

        #endregion

        #region Eventos del formulario

        #region Evento Load del formulario

        private void frmComprasComprobantes_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            //CAMBIAR PUNTERO MOUSE
            Cursor.Current = Cursors.WaitCursor;
            // Cargo los toolstip de los botones
            CargarToolsTip();
            // Formateo el form por donde se llamó
            DesdeDonde();
            // Cargo la grilla
            CargarGrilla("", "");
            // Deshabilito el ordenamiento de las cabeceras de las grillas.
            DeshabilitarOrdenGrillas();
            // Cambio el alto de la grilla detalle para que tape los campos de búsqueda
            dgvDetalle.Height = 150;
            // Cuento la cantidad de filas de la grilla
            int filas = dgvComprobantes.Rows.Count;
            // Actualizo el valor de la fila para que sea la última de la grilla
            clsGlobales.indexFila = filas - 1;
            // Pongo el foco de la fila
            PosicionarFocoFila();
            
            //CAMBIAR PUNTERO MOUSE
            Cursor.Current = Cursors.Default;

        }

        #endregion

        #region Eventos de los botones

        #region Evento Click del botón btnAgregar

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // capturo la posición de la fila
            if (dgvComprobantes.Rows.Count != 0)
            {
                clsGlobales.indexFila = this.dgvComprobantes.CurrentRow.Index;
            }
            // Armo el switch para dar el formato dependiendo de la llamada
            switch (Desde)
            {
                // Desde Cotizaciones
                case "Cotizaciones":

                    // Creo el formulario y hago la llamada
                    frmComprasCotizaciones myFormCot = new frmComprasCotizaciones(0);
                    myFormCot.ShowDialog();
                    // Formateo el form por donde se llamó
                    DesdeDonde();
                    // Cargo la grilla
                    CargarGrilla("", "");
                    // Pongo el foco de la fila
                    PosicionarFocoFila();

                    return;

                // Desde Órdenes de compra
                case "Ordenes":

                    // Creo el formulario y hago la llamada
                    frmComprasOrdenes myFormCom = new frmComprasOrdenes(0);
                    myFormCom.ShowDialog();
                    // Formateo el form por donde se llamó
                    DesdeDonde();
                    // Cargo la grilla
                    CargarGrilla("", "");
                    // Pongo el foco de la fila
                    PosicionarFocoFila();

                    return;

                // Desde facturas de compra
                case "Facturas":
                    // Redimensiono el tamaño del vector de comprobantes
                    clsGlobales.ComprobantesSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ComprobantesSeleccionados, 1);
                    // A la posición creada le asigno el Id seleccionado y la cantidad cargada
                    clsGlobales.ComprobantesSeleccionados[0] = 0;
                    // Creo el formulario y hago la llamada
                    frmComprasFacturas myFormFac = new frmComprasFacturas(clsGlobales.ComprobantesSeleccionados, 0,0);
                    myFormFac.ShowDialog();
                    // Cargo la grilla
                    CargarGrilla("", "");
                    // Pongo el foco de la fila
                    PosicionarFocoFila();

                    return;

                // Desde facturas de compra
                case "NC/ND":

                    // Creo el formulario y hago la llamada
                    frmComprasFacturas myFormCD = new frmComprasFacturas(clsGlobales.ComprobantesSeleccionados, 0,0);
                    myFormCD.ShowDialog();
                    // Pongo el foco de la fila
                    PosicionarFocoFila();

                    return;
            }
        }

        #endregion

        #region Evento Click del botón btnPedir

        private void btnPedir_Click(object sender, EventArgs e)
        {
            // capturo la posición de la fila
            clsGlobales.indexFila = this.dgvComprobantes.CurrentRow.Index;
            // Variable que almacena el Id de la cotización para pasarlo por parámetro a la orden de compra
            int IdComprobante = Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdComprobante"].Value);
            // Variable que almacena el Número de la orden de compra para mostrar el mensaje
            string NumeroOrden = dgvComprobantes.CurrentRow.Cells["Numero"].Value.ToString();
            // Variable que almacena el Id del Proveedor
            int IdProv = Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdProveedorComprobante"].Value);
            // Variable que almacena la razón social del proveedor
            string NombreProveedor = dgvComprobantes.CurrentRow.Cells["RazonSocial"].Value.ToString();
            // Cambio el estado de la bandera global de compras a true, para controlar que se precionó este botón al memento de grabar
            clsGlobales.bCompras = true;

            // Si la llamada se hace desde cotizaciones muestro el formulario de ordenes de compra
            if (Desde == "Cotizaciones")
            {
                // Creo el formulario y hago la llamada
                frmComprasOrdenes myFormCom = new frmComprasOrdenes(IdComprobante);
                myFormCom.ShowDialog();
            }
            // La llamada se hace desde ordenes y muestro el formulario de facturas de compra vacío.
            if (Desde == "Ordenes")
            {
                // Pregunto si la factura de compras corresponde solo a esta orden
                DialogResult respuesta = MessageBox.Show("La factura a cargar es solo de la O/C " + NumeroOrden + " del Proveedor " + NombreProveedor + "?", "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    // Redimensiono el tamaño de la matriz de Insumos
                    clsGlobales.ComprobantesSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ComprobantesSeleccionados, 1);
                    // A la posición creada le asigno el Id seleccionado y la cantidad cargada
                    clsGlobales.ComprobantesSeleccionados[0] = IdComprobante;
                    // Creo el formulario y hago la llamada
                    frmComprasFacturas myFormFac = new frmComprasFacturas(clsGlobales.ComprobantesSeleccionados, 0,0);
                    myFormFac.ShowDialog();
                }
                else
                {
                    // Acá va el formulario de búsqueda de comprobantes
                    frmComprasBusquedaComprobantes myForm = new frmComprasBusquedaComprobantes(IdProv, NombreProveedor, TipoComprobante);
                    myForm.ShowDialog();

                    //Controlar que haya seleccionado algo 
                    if (clsGlobales.ComprobantesSeleccionados.GetLength(0) == 0)
                    {
                        MessageBox.Show("No ha seleccionado ningún comprobante para poder cargar la Factura de Compra!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                      //Creo el formulario y hago la llamada
                        frmComprasFacturas myFormFac = new frmComprasFacturas(clsGlobales.ComprobantesSeleccionados, 0,0);
                        myFormFac.ShowDialog();
                    }

                }
            }
            // Pongo el foco de la fila
            PosicionarFocoFila();
        }

        #endregion

        #region Evento Click del botón btnModificar

        private void btnModificar_Click(object sender, EventArgs e)
        {
            // capturo la posición de la fila
            clsGlobales.indexFila = this.dgvComprobantes.CurrentRow.Index;

            // Armo el switch para dar el formato dependiendo de la llamada
            switch (Desde)
            {
                // Desde Cotizaciones
                case "Cotizaciones":

                    // Almaceno en una variable el Id de la cotización y la paso por parámetro al formulario siguiente
                    int IdCotizacion = Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdComprobante"].Value);
                    // Creo el formulario y hago la llamada
                    frmComprasCotizaciones myFormCot = new frmComprasCotizaciones(IdCotizacion);
                    myFormCot.ShowDialog();
                    // Formateo el form por donde se llamó
                    DesdeDonde();
                    // Cargo la grilla
                    CargarGrilla("", "");

                    // Pongo el foco de la fila
                    PosicionarFocoFila();

                    return;

                // Desde Órdenes de compra
                case "Ordenes":

                    // Almaceno en una variable el Id de la cotización y la paso por parámetro al formulario siguiente
                    int IdOrden = Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdComprobante"].Value);
                    // Creo el formulario y hago la llamada
                    frmComprasOrdenes myFormCom = new frmComprasOrdenes(IdOrden);
                    myFormCom.ShowDialog();
                    // Formateo el form por donde se llamó
                    DesdeDonde();
                    // Cargo la grilla
                    CargarGrilla("", "");

                    // Pongo el foco de la fila
                    PosicionarFocoFila();

                    return;

                // Desde facturas de compra
                case "Facturas":

                    // Almaceno en una variable el Id de la cotización y la paso por parámetro al formulario siguiente
                    int IdFac = Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdComprobante"].Value);
                    // Redimensiono el tamaño de la matriz de Insumos
                    clsGlobales.ComprobantesSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ComprobantesSeleccionados, 1);
                    // A la posición creada le asigno el Id seleccionado y la cantidad cargada
                    clsGlobales.ComprobantesSeleccionados[0] = IdFac;
                    // Si la factura es una compra
                    if (Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["Compra"].Value) == 1)
                    {
                        // Creo el formulario y hago la llamada
                        frmComprasFacturas myFormFac = new frmComprasFacturas(clsGlobales.ComprobantesSeleccionados, IdFac, 0);
                        myFormFac.ShowDialog();
                    }
                    // Si es un gasto
                    else
                    {
                        // Creo el formulario y hago la llamada
                        frmComprasGastosFijos myForm = new frmComprasGastosFijos(clsGlobales.ComprobantesSeleccionados, IdFac, 0);
                        myForm.ShowDialog();
                        
                    }
                    
                    // Formateo el form por donde se llamó
                    DesdeDonde();
                    // Cargo la grilla
                    CargarGrilla("", "");
                    // Pongo el foco de la fila
                    PosicionarFocoFila();

                    return;

                // Desde facturas de compra
                case "NC/ND":

                    // Creo el formulario y hago la llamada
                    frmComprasFacturas myFormCD = new frmComprasFacturas(clsGlobales.ComprobantesSeleccionados, 0,0);
                    myFormCD.ShowDialog();

                    // Pongo el foco de la fila
                    PosicionarFocoFila();

                    return;
            }
        }

        #endregion

        #region Evento Click del botón btnBorrar

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            // Si es la última fila de la grilla
            if (dgvComprobantes.CurrentRow.Index == dgvComprobantes.Rows.Count - 1)
            {
                // capturo la posición de la fila
                clsGlobales.indexFila = this.dgvComprobantes.CurrentRow.Index - 1;
            }
            else
            {
                // capturo la posición de la fila
                clsGlobales.indexFila = this.dgvComprobantes.CurrentRow.Index;
            }
            
            
            // Tomo el Id del comprobante que se quiere eliminar
            int IdComprobante = Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdComprobante"].Value);
            // Pregunt si realmente quiere borrar el comprobante
            DialogResult Respuesta = MessageBox.Show("Desea eliminar el comprobante Nro: " + dgvComprobantes.CurrentRow.Cells["Numero"].Value +
                                                    "?", "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Respuesta == DialogResult.Yes)
            {
                // Si es una factura la que se anula
                if (TipoComprobante == 3)
                {
                    // Tomo el Id del almacen en una variable para descontar el stock del mismo
                    int IdStockAlmacen = Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdAlmacen"].Value);
                    // Descuento el stock del comprobante
                    DescontarStock(IdComprobante, IdStockAlmacen);
                    // Descuento la factura del saldo del proveedor
                    DescontarSaldoProv(IdComprobante);
                }

                if (clsGlobales.ConB == null)
                {
                    // Armo la cadena SQL para borrar el detalle primero y luego el comprobante en sí
                    myCadena = "update DetallesComprobantesCompras set Activo = 0 where IdComprobanteCompra = " + IdComprobante;
                    // Ejecuto la consulta
                    clsDataBD.GetSql(myCadena);
                    // Armo la cadena SQL para borrar el detalle primero y luego el comprobante en sí
                    myCadena = "update ComprobantesCompras set Activo = 0 where IdComprobanteCompra = " + IdComprobante;
                    // Ejecuto la consulta
                    clsDataBD.GetSql(myCadena);
                }
                else
                {
                    // Armo la cadena SQL para borrar el detalle primero y luego el comprobante en sí
                    myCadena = "update DetallesComprobantesCompras set Activo = 0 where IdComprobanteCompra = " + IdComprobante;
                    // Ejecuto la consulta
                    clsDataBD.GetSqlB(myCadena);
                    // Armo la cadena SQL para borrar el detalle primero y luego el comprobante en sí
                    myCadena = "update ComprobantesCompras set Activo = 0 where IdComprobanteCompra = " + IdComprobante;
                    // Ejecuto la consulta
                    clsDataBD.GetSqlB(myCadena);
                }
                // Formateo el form por donde se llamó
                DesdeDonde();
                // Cargo la grilla
                CargarGrilla("", "");
                // Pongo el foco de la fila
                PosicionarFocoFila();
                                
            }
        }

        #endregion

        #region Evento Click del botón btnBuscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Muestro el botón cancelar
            this.btnCancelar.Visible = true;
            // Achico el tamañp de la grilla para mostrar los campos de búesuqeda
            dgvDetalle.Height = 90;
            // Muestro el contenedor de los campos de búsqueda
            gpbBusquedas.Visible = true;
            // Limpio los campos de búsqueda
            LimpiarCamposBusqueda();
            // Pongo el foco en el campo Número
            txtNumero.Focus();

        }

        #endregion

        #region Evento Click del botón btnCancelar

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Limpio los campos de búsqueda
            LimpiarCamposBusqueda();
            // Cargo la grilla con todos los datos
            CargarGrilla("", "");
            // Oculto los campos de búsqueda
            gpbBusquedas.Visible = false;
            // Estiro la grilla detalle para que tape los campos de búesuqeda
            dgvDetalle.Height = 150;
            // Oculto el botón cancelar
            this.btnCancelar.Visible = false;

            // Pongo el foco de la fila
            PosicionarFocoFila();
        }

        #endregion

        #region Evento Click del botón btnImprimir

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            // capturo la posición de la fila
            clsGlobales.indexFila = this.dgvComprobantes.CurrentRow.Index;

            int dgvFilas = 0;
            double TotGral = 0;

            //Tipo Comprobante
            switch(this.TipoComprobante)
            {
                case 1:
                    string strTitulo = "Solicitud de Cotización ";
                    
                    //Data Set
                    dsReportes oDsCompraCotiz = new dsReportes();

                    //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
                    dgvFilas = dgvDetalle.Rows.Count;

                    for (int i = 0; i < dgvFilas; i++)
                    {
                        oDsCompraCotiz.Tables["DtCompraCotiz"].Rows.Add
                        (new object[] { dgvDetalle[2,i].Value.ToString(),
                        dgvDetalle[3,i].Value.ToString(),
                        dgvDetalle[5,i].Value.ToString(),
                        dgvDetalle[6,i].Value.ToString(),
                        dgvDetalle[7,i].Value.ToString(),
                        dgvDetalle[8,i].Value.ToString(),
                        dgvDetalle[9,i].Value.ToString(),
                        dgvDetalle[10,i].Value.ToString()});

                    }

                    //Objeto Reporte
                    rptCotizacion oRepCompraCotiz = new rptCotizacion();
                    //Cargar Reporte                                    
                    oRepCompraCotiz.Load(Application.StartupPath + "\\rptCotizacion.rpt");
                    //Establecer el DataSet como DataSource
                    oRepCompraCotiz.SetDataSource(oDsCompraCotiz);
                    //Pasar como parámetro nombre del reporte
                    clsGlobales.myRptDoc = oRepCompraCotiz;

                    oRepCompraCotiz.DataDefinition.FormulaFields["TipoComprobante"].Text = "'" + strTitulo + "'";
                    oRepCompraCotiz.DataDefinition.FormulaFields["NroComprobante"].Text = "'" + this.dgvComprobantes.CurrentRow.Cells["Numero"].Value.ToString() + "'";
                    oRepCompraCotiz.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
                    //oRepCompraCotiz.DataDefinition.FormulaFields["Descripcion"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Descripcion"].Value.ToString() + "'";

                    //Comprobante y pie
                    oRepCompraCotiz.DataDefinition.FormulaFields["Fecha"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Fecha"].Value.ToString() + "'";
                    oRepCompraCotiz.DataDefinition.FormulaFields["Rs"].Text = "'" + dgvComprobantes.CurrentRow.Cells["RazonSocial"].Value.ToString() + "'";
                    oRepCompraCotiz.DataDefinition.FormulaFields["PVta"].Text = "'" + dgvComprobantes.CurrentRow.Cells["PuntoVenta"].Value.ToString() + "'";
                    oRepCompraCotiz.DataDefinition.FormulaFields["CantArt"].Text = "'" + dgvComprobantes.CurrentRow.Cells["CantidadArticulos"].Value.ToString() + "'";
                    oRepCompraCotiz.DataDefinition.FormulaFields["Almcn"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Almacen"].Value.ToString() + "'";
                    oRepCompraCotiz.DataDefinition.FormulaFields["CondVta"].Text = "'" + dgvComprobantes.CurrentRow.Cells["CondicionCompra"].Value.ToString() + "'";
                    oRepCompraCotiz.DataDefinition.FormulaFields["Vence"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Vence"].Value.ToString() + "'";
                    oRepCompraCotiz.DataDefinition.FormulaFields["Neto"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Neto"].Value.ToString() + "'";

                    oRepCompraCotiz.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
                    oRepCompraCotiz.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
                    oRepCompraCotiz.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
                    oRepCompraCotiz.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
                    oRepCompraCotiz.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
                    oRepCompraCotiz.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
                    oRepCompraCotiz.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";


                    break;

                case 2:
                    strTitulo = "Orden de Compra ";
                    TotGral = 0;

                    //Data Set
                    dsReportes oDsOrden = new dsReportes();

                    //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
                    dgvFilas = dgvDetalle.Rows.Count;

                    for (int i = 0; i < dgvFilas; i++)
                    {
                        oDsOrden.Tables["DtCompraCotiz"].Rows.Add
                        (new object[] { dgvDetalle[2,i].Value.ToString(),
                        dgvDetalle[3,i].Value.ToString(),
                        dgvDetalle[5,i].Value.ToString(),
                        dgvDetalle[6,i].Value.ToString(),
                        dgvDetalle[7,i].Value.ToString(),
                        dgvDetalle[8,i].Value.ToString(),
                        dgvDetalle[9,i].Value.ToString(),
                        dgvDetalle[10,i].Value.ToString()});

                        TotGral += Convert.ToDouble(dgvDetalle[10, i].Value.ToString());

                    }


                    //Objeto Reporte
                    rptOrden oRepOrden = new rptOrden();
                    //Cargar Reporte                                    
                    oRepOrden.Load(Application.StartupPath + "\\rptOrden.rpt");
                    //Establecer el DataSet como DataSource
                    oRepOrden.SetDataSource(oDsOrden);
                    //Pasar como parámetro nombre del reporte
                    clsGlobales.myRptDoc = oRepOrden;

                    oRepOrden.DataDefinition.FormulaFields["TipoComprobante"].Text = "'" + strTitulo + "'";
                    oRepOrden.DataDefinition.FormulaFields["NroComprobante"].Text = "'" + this.dgvComprobantes.CurrentRow.Cells["Numero"].Value.ToString() + "'";
                    oRepOrden.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
                    //oRepOrden.DataDefinition.FormulaFields["Descripcion"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Descripcion"].Value.ToString() + "'";

                    //Comprobante y pie
                    oRepOrden.DataDefinition.FormulaFields["Fecha"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Fecha"].Value.ToString() + "'";
                    oRepOrden.DataDefinition.FormulaFields["Rs"].Text = "'" + dgvComprobantes.CurrentRow.Cells["RazonSocial"].Value.ToString() + "'";
                    oRepOrden.DataDefinition.FormulaFields["PVta"].Text = "'" + dgvComprobantes.CurrentRow.Cells["PuntoVenta"].Value.ToString() + "'";
                    oRepOrden.DataDefinition.FormulaFields["Almcn"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Almacen"].Value.ToString() + "'";
                    oRepOrden.DataDefinition.FormulaFields["CantArt"].Text = "'" + dgvComprobantes.CurrentRow.Cells["CantidadArticulos"].Value.ToString() + "'";
                    oRepOrden.DataDefinition.FormulaFields["CondVta"].Text = "'" + dgvComprobantes.CurrentRow.Cells["CondicionCompra"].Value.ToString() + "'";
                    oRepOrden.DataDefinition.FormulaFields["Vence"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Vence"].Value.ToString() + "'";
                    oRepOrden.DataDefinition.FormulaFields["Neto"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Neto"].Value.ToString() + "'";

                    oRepOrden.DataDefinition.FormulaFields["TotGral"].Text = "'" + TotGral.ToString("#0.00") + "'";
                                
                    oRepOrden.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
                    oRepOrden.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
                    oRepOrden.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
                    oRepOrden.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
                    oRepOrden.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
                    oRepOrden.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
                    oRepOrden.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";


                    break;

                case 3:
                    strTitulo = "Factura de Compra ";


                    //Data Set
                    dsReportes oDsFactu = new dsReportes();

                    //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
                    dgvFilas = dgvDetalle.Rows.Count;

                    for (int i = 0; i < dgvFilas; i++)
                    {
                        oDsFactu.Tables["DtCompraCotiz"].Rows.Add
                        (new object[] { dgvDetalle[2,i].Value.ToString(),
                        dgvDetalle[3,i].Value.ToString(),
                        dgvDetalle[5,i].Value.ToString(),
                        dgvDetalle[6,i].Value.ToString(),
                        dgvDetalle[7,i].Value.ToString(),
                        dgvDetalle[8,i].Value.ToString(),
                        dgvDetalle[9,i].Value.ToString(),
                        dgvDetalle[10,i].Value.ToString()});

                    }


                    //Objeto Reporte
                    rptFactura oRepFactu = new rptFactura();
                    //Cargar Reporte                                    
                    oRepFactu.Load(Application.StartupPath + "\\rptFactura.rpt");
                    //Establecer el DataSet como DataSource
                    oRepFactu.SetDataSource(oDsFactu);
                    //Pasar como parámetro nombre del reporte
                    clsGlobales.myRptDoc = oRepFactu;

                    oRepFactu.DataDefinition.FormulaFields["TipoComprobante"].Text = "'" + strTitulo + "'";
                    oRepFactu.DataDefinition.FormulaFields["NroComprobante"].Text = "'" + this.dgvComprobantes.CurrentRow.Cells["Numero"].Value.ToString() + "'";
                    oRepFactu.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
                    oRepFactu.DataDefinition.FormulaFields["Descripcion"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Descripcion"].Value.ToString() + "'";

                    //Comprobante y pie
                    oRepFactu.DataDefinition.FormulaFields["Fecha"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Fecha"].Value.ToString() + "'";
                    oRepFactu.DataDefinition.FormulaFields["Rs"].Text = "'" + dgvComprobantes.CurrentRow.Cells["RazonSocial"].Value.ToString() + "'";
                    oRepFactu.DataDefinition.FormulaFields["PVta"].Text = "'" + dgvComprobantes.CurrentRow.Cells["PuntoVenta"].Value.ToString() + "'";
                    oRepFactu.DataDefinition.FormulaFields["Almcn"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Almacen"].Value.ToString() + "'";
                    oRepFactu.DataDefinition.FormulaFields["CantArt"].Text = "'" + dgvComprobantes.CurrentRow.Cells["CantidadArticulos"].Value.ToString() + "'";
                    oRepFactu.DataDefinition.FormulaFields["CondVta"].Text = "'" + dgvComprobantes.CurrentRow.Cells["CondicionCompra"].Value.ToString() + "'";
                    oRepFactu.DataDefinition.FormulaFields["Vence"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Vence"].Value.ToString() + "'";
                    
                    oRepFactu.DataDefinition.FormulaFields["Neto"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Neto"].Value.ToString() + "'";
                    oRepFactu.DataDefinition.FormulaFields["TotGral"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Total"].Value.ToString() + "'";
                    oRepFactu.DataDefinition.FormulaFields["Saldo"].Text = "'" + dgvComprobantes.CurrentRow.Cells["Saldo"].Value.ToString() + "'";

                    oRepFactu.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
                    oRepFactu.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
                    oRepFactu.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
                    oRepFactu.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
                    oRepFactu.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
                    oRepFactu.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
                    oRepFactu.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";


                    break;
            }

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();

            // Pongo el foco de la fila
            PosicionarFocoFila();
        }

        #endregion

        #endregion

        #region Eventos de la grilla

        #region Evento SelectionChanged de la grilla dgvComprobantes

        private void dgvComprobantes_SelectionChanged(object sender, EventArgs e)
        {
            if (yaCargado)
            {
                // Vacío la grilla
                dgvDetalle.DataSource = null;
                // Evito que el dgvUsuarios genere columnas automáticas
                dgvDetalle.AutoGenerateColumns = false;
                // Declaro una variable que va a guardar el Id del comprobante
                int IdComprobante = Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdComprobante"].Value);
                
                // Creo un datatable y le paso los datos de la consulta SQL
                DataTable myTabla = new DataTable();
                // Si no está abierta la conexion B
                if (clsGlobales.ConB == null)
                {
                    // Armo la cadena SQL para ejecutar el procedieminto almacenado que trae el detalle del comprobante
                    string newMyCadenaSql = "exec ComprasDetalleComprobantes " + IdComprobante + ", " + TipoComprobante;
                    myTabla = clsDataBD.GetSql(newMyCadenaSql);
                }
                else
                {
                    if (TipoComprobante < 3)
                    {
                        // Armo la cadena SQL para ejecutar el procedieminto almacenado que trae el detalle del comprobante
                        string newMyCadenaSql = "exec ComprasDetalleComprobantes " + IdComprobante + ", " + TipoComprobante;
                        myTabla = clsDataBD.GetSql(newMyCadenaSql);
                    }
                    else
                    {
                        // Armo la cadena SQL para ejecutar el procedieminto almacenado que trae el detalle del comprobante
                        string newMyCadenaSql = "exec ComprasDetalleComprobantes2 " + IdComprobante + ", " + TipoComprobante;
                        myTabla = clsDataBD.GetSqlB(newMyCadenaSql);
                    }
                    
                }
                 
                // Asigno a la grilla el source
                dgvDetalle.DataSource = myTabla;
                // Si tengo filas en la grilla, calculo el total del artículo
                if (dgvDetalle.Rows.Count > 0)
                {
                    AgregarItem();
                }

            }
        }

        #endregion

        #region Evento Click de la grilla dgvComprobantes

        private void dgvComprobantes_Click(object sender, EventArgs e)
        {
            // Cambio el estado de la bandera para que se ejecute el selection changed
            yaCargado = true;
            // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
            EventArgs ea = new EventArgs();
            this.dgvComprobantes_SelectionChanged(this.dgvComprobantes, ea);
        }

        #endregion

        #endregion

        #region Eventos del los TextBox

        #region Evento KeyPress txtNumero

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Solo enteros
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        #endregion

        #region Evento TextChanged del txtNumero

        private void txtNumero_TextChanged(object sender, EventArgs e)
        {
            // Cargo los comprobantes filtrados por la búsqueda
            CargarGrilla(txtNumero.Text.ToUpper(), "Numero");
        }

        #endregion

        #region Evento Enter del txtNumero

        private void txtNumero_Enter(object sender, EventArgs e)
        {
            // Si el otro campo de búsqueda tiene algo, lo borro
            if (!(txtFecha.Text == ""))
            {
                // Limpio el ptro campo de búsqueda
                txtFecha.Text = "";
            }
            // Si el otro campo de búsqueda tiene algo, lo borro
            if (!(txtRazon.Text == ""))
            {
                // Limpio el ptro campo de búsqueda
                txtRazon.Text = "";
            }
            // Si el otro campo de búsqueda tiene algo, lo borro
            if (!(txtVence.Text == ""))
            {
                // Limpio el ptro campo de búsqueda
                txtVence.Text = "";
            }
        }

        #endregion

        #region Evento TextXhanged del txtFecha

        private void txtFecha_TextChanged(object sender, EventArgs e)
        {
            // Cargo los comprobantes filtrados por la búsqueda
            CargarGrilla(txtFecha.Text.ToUpper(), "Fecha");
        }

        #endregion

        #region Evento Enter del txtFecha

        private void txtFecha_Enter(object sender, EventArgs e)
        {
            // Si el otro campo de búsqueda tiene algo, lo borro
            if (!(txtNumero.Text == ""))
            {
                // Limpio el ptro campo de búsqueda
                txtNumero.Text = "";
            }
            // Si el otro campo de búsqueda tiene algo, lo borro
            if (!(txtRazon.Text == ""))
            {
                // Limpio el ptro campo de búsqueda
                txtRazon.Text = "";
            }
            // Si el otro campo de búsqueda tiene algo, lo borro
            if (!(txtVence.Text == ""))
            {
                // Limpio el ptro campo de búsqueda
                txtVence.Text = "";
            }
        }

        #endregion

        #region Evento TextChanged del txtRazon

        private void txtRazon_TextChanged(object sender, EventArgs e)
        {
            // Cargo los comprobantes filtrados por la búsqueda
            CargarGrilla(txtRazon.Text.ToUpper(), "Proveedor");
        }

        #endregion

        #region Evento Enter del txtRazon

        private void txtRazon_Enter(object sender, EventArgs e)
        {
            // Si el otro campo de búsqueda tiene algo, lo borro
            if (!(txtNumero.Text == ""))
            {
                // Limpio el ptro campo de búsqueda
                txtNumero.Text = "";
            }
            // Si el otro campo de búsqueda tiene algo, lo borro
            if (!(txtFecha.Text == ""))
            {
                // Limpio el ptro campo de búsqueda
                txtFecha.Text = "";
            }
            // Si el otro campo de búsqueda tiene algo, lo borro
            if (!(txtVence.Text == ""))
            {
                // Limpio el ptro campo de búsqueda
                txtVence.Text = "";
            }
        }

        #endregion

        #region Evento TextChanged del txtVence

        private void txtVence_TextChanged(object sender, EventArgs e)
        {
            // Cargo los comprobantes filtrados por la búsqueda
            CargarGrilla(txtVence.Text.ToUpper(), "Vence");
        }

        #endregion

        #region Evento Enter del txtVence

        private void txtVence_Enter(object sender, EventArgs e)
        {
            // Si el otro campo de búsqueda tiene algo, lo borro
            if (!(txtNumero.Text == ""))
            {
                // Limpio el ptro campo de búsqueda
                txtNumero.Text = "";
            }
            // Si el otro campo de búsqueda tiene algo, lo borro
            if (!(txtFecha.Text == ""))
            {
                // Limpio el ptro campo de búsqueda
                txtFecha.Text = "";
            }
            // Si el otro campo de búsqueda tiene algo, lo borro
            if (!(txtRazon.Text == ""))
            {
                // Limpio el ptro campo de búsqueda
                txtRazon.Text = "";
            }
        }

        #endregion
        
        #endregion

        #endregion

        #region Métodos del formulario

        #region Método que da formato a los controles del formulario dependiendo desde donde se llama

        // Método que le da el formato general al form
        private void DesdeDonde()
        {
            // Cambio el texto del formulario
            this.Text = Desde;
            
            // Armo el switch para dar el formato dependiendo de la llamada
            switch (Desde)
            {
                // Desde Cotizaciones
                case "Cotizaciones":
                    // Tipo de comprobante 1
                    TipoComprobante = 1;
                    // Cambio de posición el GPB comprobantes y cambio los tamaños
                    gpbComprobantes.Location= new Point(10,12);
                    gpbComprobantes.Size = new Size(972, 308);
                    dgvComprobantes.Size = new Size(958, 283);
                    
                    
                    // Oculto las columnas Neto y total
                    this.dgvComprobantes.Columns["Neto"].Visible = false;
                    this.dgvComprobantes.Columns["Total"].Visible = false;
                    this.dgvDetalle.Columns["TotalArt"].Visible = false;
                    this.dgvDetalle.Columns["Articulo"].Width = 523;
                    this.dgvDetalle.Columns["PrecioArt"].Visible = false;
                    // Agrando la columna de razon social para completar el tamaño de la grilla
                    this.dgvComprobantes.Columns["RazonSocial"].Width = 395;
                    // Muestro el botón pedir
                    this.btnPedir.Visible = true;
                    this.Text = clsGlobales.cFormato.getTituloVentana() + " - SOLICITUDES DE COTIZACIONES";
                    return;

                // Desde Órdenes de compra
                case "Ordenes":
                    // Tipo de comprobante 2
                    TipoComprobante = 2;
                    // Cambio de posición el GPB comprobantes y cambio los tamaños
                    gpbComprobantes.Location = new Point(10, 12);
                    gpbComprobantes.Size = new Size(972, 308);
                    dgvComprobantes.Size = new Size(958, 283);
                    // Agrando la columna de razon social para completar el tamaño de la grilla
                    this.dgvComprobantes.Columns["RazonSocial"].Width = 325;
                    this.dgvComprobantes.Columns["Neto"].Width = 70;
                    this.dgvComprobantes.Columns["Total"].Visible = false;
                    this.dgvDetalle.Columns["Articulo"].Width = 385;
                    // Muestro el botón pedir
                    this.btnPedir.Visible = true;
                    this.Text = clsGlobales.cFormato.getTituloVentana() + " - ORDENES DE COMPRA";
                    return;

                // Desde facturas de compra
                case "Facturas":
                    // Tipo de comprobante 3
                    TipoComprobante = 3;
                    // Muestro Los filtros
                /*    lblFiltros.Visible = true;
                    rdbTodas.Visible = true;
                    rdbAdeudadas.Visible = true;
                    rdbCanceladas.Visible = true;*/
                    // Hago visible la columna del saldo.
                    this.dgvComprobantes.Columns["Saldo"].Visible = true;
                    // Achico la columna de la condición de compra para que se agregue la columna del saldo.
                    this.dgvComprobantes.Columns["CondicionCompra"].Width = 100;
                    // Agrando la columna de número
                    this.dgvComprobantes.Columns["Numero"].Width = 90;
                    this.dgvComprobantes.Columns["RazonSocial"].Width = 210;
                    this.dgvDetalle.Columns["Articulo"].Width = 385;
                    // Oculto el botón pedir
                    this.btnPedir.Visible = false;
                    // Pongo el título a la ventana
                    this.Text = clsGlobales.cFormato.getTituloVentana() + " - FACTURAS DE COMPRA";
                    // Cambio la imagen del botón Modificar
                    this.btnModificar.Image = Prama.Recursos.Ver;
                    // Cambio el toolTip del botón Modificar
                    toolTip5.SetToolTip(this.btnModificar, "Ver");

                    return;

                // Desde facturas de compra
                case "NC/ND":
                    // Tipo de comprobante 4
                    TipoComprobante = 4;
                    // Hago visible la columna del saldo.
                    this.dgvComprobantes.Columns["Saldo"].Visible = true;
                    // Achico la columna de la condición de compra para que se agregue la columna del saldo.
                    this.dgvComprobantes.Columns["CondicionCompra"].Width = 95;
                    // Oculto el botón pedir
                    this.btnPedir.Visible = false;
                    this.Text = clsGlobales.cFormato.getTituloVentana() + " - NOTAS DE CREDITO / DEBITO";
                    return;
            }
            
        }

        #endregion

        #region Método que carga la grilla

        // Método que carga la grilla del formulario
        private void CargarGrilla(string Buscar, string Campo)
        {
            // Variable para la cadena SQL
            string myCadena = "";
            
            
            // Armo la cadena SQL
            
            // Evito que el dgvUsuarios genere columnas automáticas
            dgvComprobantes.AutoGenerateColumns = false;
            // Creo un nuevo DataTable
            DataTable mDtTable = new DataTable();
            // Si la conexion B es NULL
            if (clsGlobales.ConB == null)
            {
                if (Buscar == "")
                {
                    // Cadena SQL 
                    myCadena = "SELECT * FROM Vista_ComprobantesCompras WHERE IdTipo = " + TipoComprobante + " order by FechaReal";
                }
                else
                {
                    // Cadena SQL 
                    myCadena = "select * from Vista_ComprobantesCompras WHERE IdTipo = " + TipoComprobante + " and " + Campo + " like '" + Buscar + "%' order by " + Campo;
                }
                // Le asigno al nuevo DataTable los datos de la consulta SQL
                mDtTable = clsDataBD.GetSql(myCadena);
            }
            else
            {
                if (Buscar == "")
                {
                    // Si el tipo de comprobante es Cotizacion u orden de compra
                    if (TipoComprobante < 3)
                    {
                        // Cadena SQL 
                        myCadena = "SELECT * FROM Vista_ComprobantesCompras WHERE IdTipo = " + TipoComprobante + " order by FechaReal"; 
                        // Le asigno al nuevo DataTable los datos de la consulta SQL
                        mDtTable = clsDataBD.GetSql(myCadena);
                    }
                    else
                    {
                        // Cadena SQL 
                        myCadena = "SELECT * FROM Vista_ComprobantesCompras2 WHERE IdTipo = " + TipoComprobante + " order by FechaReal"; 
                        // Le asigno al nuevo DataTable los datos de la consulta SQL
                        mDtTable = clsDataBD.GetSqlB(myCadena);
                    }
                    
                }
                else
                {
                    if (TipoComprobante < 3)
                    {
                        // Cadena SQL 
                        myCadena = "select * from Vista_ComprobantesCompras WHERE IdTipo = " + TipoComprobante + " and " + Campo + " like '" + Buscar + "%' order by " + Campo;
                        // Le asigno al nuevo DataTable los datos de la consulta SQL
                        mDtTable = clsDataBD.GetSql(myCadena);
                    }
                    else
                    {
                        // Cadena SQL 
                        myCadena = "select * from Vista_ComprobantesCompras2 WHERE IdTipo = " + TipoComprobante + " and " + Campo + " like '" + Buscar + "%' order by " + Campo;
                        // Le asigno al nuevo DataTable los datos de la consulta SQL
                        mDtTable = clsDataBD.GetSqlB(myCadena);
                    }
                }
                
            }
                        
            // Asigno el source de la grilla
            dgvComprobantes.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvComprobantes.Rows.Count;
            // Si hay filas
            if (Filas > 0)
            {
                // Cambio el estado de la bandera para que ejecute el selection changed de la grilla
                yaCargado = true;

                // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                EventArgs ea = new EventArgs();
                this.dgvComprobantes_SelectionChanged(this.dgvComprobantes, ea);
                // Habilito los botones que puedan estar deshabilitados
                this.btnModificar.Enabled = true;
                this.btnBorrar.Enabled = true;
                this.btnImprimir.Enabled = true;
                this.btnPedir.Enabled = true;
                this.btnBuscar.Enabled = true;
            }
            else
            {
                // Vacío la grilla de detalle
                dgvDetalle.DataSource = null;
                // Deshabilito los botones que generarían error al no tener datos la grilla Comprobantes
                this.btnModificar.Enabled = false;
                this.btnBorrar.Enabled = false;
                this.btnImprimir.Enabled = false;
                this.btnPedir.Enabled = false;
                this.btnBuscar.Enabled = false;
            }

        }

        #endregion

        #region Método que agrega el número de Item a las filas

        private void AgregarItem()
        {
            // Contador que me va a poner el número de item en la grilla
            int myFila = 1;
            // Recorro la grilla y hago el calculo
            foreach (DataGridViewRow row in dgvDetalle.Rows)
            {
                row.Cells["Item"].Value = myFila;
                myFila++;
            }
        }

        #endregion

        #region Método que deshabilita el reordenamiento de las grilla desde sus cabeceras

        private void DeshabilitarOrdenGrillas()
        {
            // Deshabilito la reordenación de las columnas de las grillas
            foreach (DataGridViewColumn dgvCol in dgvComprobantes.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn dgvCol in dgvDetalle.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        #endregion

        #region Método que limpia los campos de búsqueda del formulario

        private void LimpiarCamposBusqueda()
        {
            txtNumero.Text = "";
            txtRazon.Text = "";
            txtFecha.Text = "";
            txtVence.Text = "";
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
            toolTip6.SetToolTip(this.btnBorrar, "Eliminar");
            toolTip7.SetToolTip(this.btnBuscar, "Buscar");
            toolTip8.SetToolTip(this.btnImprimir, "Imprimir");
            toolTip9.SetToolTip(this.btnPedir, "Confirmar");
        }

        #endregion

        #region Método que devuelve el foco a la fila desde donde se presionó algún botón

        private void PosicionarFocoFila()
        {
            if (dgvComprobantes.Rows.Count != 0 && dgvComprobantes.Rows.Count > clsGlobales.indexFila)
            {
                if (!(clsGlobales.indexFila < 0))
                {
                    // Devuelvo el foco a la fila de la grilla desde donde se llamó
                    this.dgvComprobantes.CurrentCell = dgvComprobantes[1, clsGlobales.indexFila];
                }
                // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                EventArgs ea = new EventArgs();
                this.dgvComprobantes_SelectionChanged(this.dgvComprobantes, ea);
            }

        }

        #endregion

        #region Método que descuenta el Stock de las facturas Anuladas

        private void DescontarStock(int IdFactura, int IdAlmacen)
        {
            // Variable que almacena el sotck por almacen
            double StockAlmacen = 0;
            // Armo la cadena SQL con el viejo detalle de la factura de compra
            string myCadenaSQL = "select * from DetallesComprobantesCompras where IdComprobanteCompra = " + IdFactura + " and Activo = 1";
            DataTable myTablaDetalleViejo;

            if (clsGlobales.ConB == null)
            {
                // Paso a una tabla el detalle viejo de la factura
                myTablaDetalleViejo = clsDataBD.GetSql(myCadenaSQL);
            }
            else
            {
                // Paso a una tabla el detalle viejo de la factura
                myTablaDetalleViejo = clsDataBD.GetSqlB(myCadenaSQL);
            }
            // Recorro la tabla 
            foreach (DataRow row in myTablaDetalleViejo.Rows)
            {
                // Asigno a una variable el Id del artículo
                int IdArt = Convert.ToInt32(row["IdArticulo"]);
                // Asigno a una Variable la cantidad del Artículo comprado
                double CantArt = Convert.ToDouble(row["Cantidad"]);
                // Variable que va a contener el stock actual del artículo
                double StockActual = 0;
                // Cargo a una tabla el Artículo para tomar el valor actual del stock
                string myCadenaSQLArticulo = "select * from Articulos where IdArticulo = " + IdArt;
                // Ejecuto la consulta y paso los datos a una tabla
                DataTable myArticuloViejo = clsDataBD.GetSql(myCadenaSQLArticulo);
                // Recorro la tabla que tiene el artículo y tomo el valor de su stock actual
                foreach (DataRow rowArticulo in myArticuloViejo.Rows)
                {
                    // Si el artículo lleva stock
                    if (Convert.ToInt32(rowArticulo["LlevaStock"]) == 1)
                    {
                        // Paso a la variable el stock actual
                        StockActual = Convert.ToDouble(rowArticulo["Stock"]);
                        // Le resto al stock actual la cantidad comprada
                        StockActual = StockActual - CantArt;
                        // Armo la cadena SQL para actualizar el stock del artículo
                        myCadenaSQLArticulo = "update Articulos set Stock = " + StockActual + " where IdArticulo = " + IdArt;
                        // Ejecuto la consulta
                        clsDataBD.GetSql(myCadenaSQLArticulo);

                        /**********************************************************************
                             Sector que graba el stock del artículo por almacen
                        **********************************************************************/ 

                        // Recorro la tabla par ver si ya el artículo para ese almacen tiene cargado stock
                        myCadenaSQLArticulo = "select * from ArticulosAlmacenesStock where IdArticulo = " + IdArt + " and IdAlmacen = " + IdAlmacen;
                        // Ejecuto la consulta y paso los datos a una tabla
                        DataTable myTablaAlmacen = clsDataBD.GetSql(myCadenaSQLArticulo);
                        // Si la tabla tiene el artículo, lo modifico, si no ingreso un registro nuevo
                        if (myTablaAlmacen.Rows.Count > 0)
                        {
                            // Variable que almacena el Id del registro
                            int IdArticulosAlmacenesStock = 0;
                            // Recorro la tabla
                            foreach (DataRow rowAlmacen in myTablaAlmacen.Rows)
                            {
                                // Tomo el valor del Stock actual y le sumo el comprado
                                IdArticulosAlmacenesStock = Convert.ToInt32(rowAlmacen["IdArticuloAlmacenStock"]);
                                // Tomo el valor del Stock actual y le sumo el comprado
                                StockAlmacen = Convert.ToDouble(rowAlmacen["Stock"]);
                            }
                            // Al stock del artículo le resto lo devuelto
                            StockAlmacen = StockAlmacen - CantArt;
                            // Armo la cadena SQL
                            myCadenaSQLArticulo = "update ArticulosAlmacenesStock set Stock = " + StockAlmacen
                                                + " where IdArticuloAlmacenStock = " + IdArticulosAlmacenesStock;
                        }
                        else
                        {
                            // Armo la cadena SQL para insertar un nuevo registro
                            myCadenaSQLArticulo = "insert into ArticulosAlmacenesStock (IdArticulo, IdAlmacen, Stock) values (" +
                                                   IdArt + ", " +
                                                   Almacen + ", " +
                                                   (CantArt * -1) + ")";
                        }
                        // Ejecuto la consulta
                        clsDataBD.GetSql(myCadenaSQLArticulo);
                    }

                }
            }
        }

        #endregion

        #region Método que descuenta el Saldo del Proveedor de las facturas Anuladas

        private void DescontarSaldoProv(int IdComp)
        {
            // Variable que almacena el Id del Proveedor
            int IdProv = 0;
            // Variable que almacena el saldo del proveedor
            double SaldoProv = 0;
            // Variable que almacena el total del la factura
            double TotalFactura = 0;
            // Armo la cadena SQL
            string MyCadenaSQL = "Select * from ComprobantesCompras where IdComprobanteCompra = " + IdComp;

            DataTable myTablaComp;

            if (clsGlobales.ConB == null)
            {
                // Ejecuto la consulta y guardo los datos en una tabla
                myTablaComp = clsDataBD.GetSql(MyCadenaSQL);
            }
            else
            {
                // Ejecuto la consulta y guardo los datos en una tabla
                myTablaComp = clsDataBD.GetSqlB(MyCadenaSQL);
            }
            
            // Recorro la tabla que tiene los datos del comprobante
            foreach (DataRow rowComp in myTablaComp.Rows)
            {
                // Paso a la variable el valor del total del comprobante
                TotalFactura = Convert.ToDouble(rowComp["Total"]);
                // Paso a la variable el Id del proveedor
                IdProv = Convert.ToInt32(rowComp["IdProveedor"]);
                // Armo la cadena SQL para encontrar al proveedor
                string myCadenaSqlProv = "select * from Proveedores where IdProveedor = " + IdProv;
                // Ejecuto la consulta y cargo los datos del proveedor en una tabla
                DataTable myProv = clsDataBD.GetSql(myCadenaSqlProv);
                // recorro la tabla y tomo el saldo actual del proveedor
                foreach (DataRow rowProv in myProv.Rows)
                {
                    // Paso el saldo actual del proveedor a la variable
                    SaldoProv = Convert.ToDouble(rowProv["Saldo"]);
                }
                // Al saldo del proveedor le resto el total de la factura
                SaldoProv = SaldoProv - TotalFactura;
                // Actualizo el saldo del proveedor
                string myCadenaSQLSaldo = "update Proveedores set Saldo = " + SaldoProv + " where IdProveedor = " + IdProv;
                // Ejecuto la consulta
                clsDataBD.GetSql(myCadenaSQLSaldo);
            }
        }

        #endregion

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}

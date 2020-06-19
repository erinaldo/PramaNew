using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prama.Formularios.Stock
{
    public partial class frmStockFabricacion : Form
    {
        
        #region Variables del formulario

        // BAndera que controla la habilitación de los eventos de la grilla
        bool BanderaGrilla = false;
        // Bandera que controla el comportamiento de la búsqueda
        bool BanderaBuscar = false;
        // Bandera que me indica si esta compuesto por productos
        bool bComDeProd = false;
        // Bandera que me indica si esta compuesto por Insumos
        bool bComDeIns = false;
        // Variable que almacena el Id del Artículo, Producto
        double dIdArticulo = 0;
        double dIdProducto = 0;
        double dIdInsumo = 0;
        //Estado
        string myEstado = "C";
        //indexFila
        int indexFila = 0;

        #endregion

        #region Constructor del formulario

        public frmStockFabricacion()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos del Formulario

        #region Evento Load del formulario

        private void frmStockFabricacion_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            this.myEstado = "C";
            // Deshabilito el reordenamiento de las grillas desde su cabecera
            DeshabilitarOrdenGrillas();
            // Cargo los datos a la grilla
            CargarGrilla("", "");
            // Llamo al método activar los botones del formulario. G.
            ActivarBotones();
            // Llamo al método habilitar controles del formulario. G.
            HabilitarControles();
            // Cargar ToolTips
            CargarToolTips();
            //Titulo Ventana
            this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;
            // Si la grilla tiene datos
            if (dgvFabricacion.Rows.Count > 0)
            {
                // Cambio el estado de la bandera
                BanderaGrilla = true;
                // Paso a la variable global el valor del Index
                this.indexFila = dgvFabricacion.Rows.Count - 1;
                // Me posiciono en la última fila
                PosicionarFocoFila();
            }

        }

        #endregion

        #region Eventos de los Botones

        #region Evento Click del botón Agregar

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (dgvFabricacion.Rows.Count > 0)
            {
                // Paso a la variable el index de la fila
                this.indexFila = dgvFabricacion.CurrentRow.Index;
            }
            // Cambio el estado del formulario
            this.myEstado = "A";
            // limpio los controles
            LimpiarControlesForm();
            // Habilito los controles
            HabilitarControles();
            // Habilito los botones
            ActivarBotones();
            // Posiciono el foco en el dtp de la fecha
            dtpFecha.Focus();
        }

        #endregion

        #region Evento Click del botón Eliminar

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            //Si hay datos...
            if (dgvFabricacion.Rows.Count > 0)
            {
                // Paso a la variable el index de la fila
                this.indexFila = dgvFabricacion.CurrentRow.Index;
            }

            //Declaracion variables
            string sProd = txtProducto.Text;
            //Variable que almacena la cantidad de la tanda fabricada
            double dTanda = 0;

            //Validar el nivel del usuario para ver si tiene permisos
            if (!(clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja))
            {
                // El usuario no es de nivel 5, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para Eliminar la Fabricación del Producto!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Fin
                return;
            }

            // Confirmo la eliminación
            DialogResult myConfirmacion = MessageBox.Show("Desea eliminar la fabricacion del Producto " + sProd, "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Si confirma
            if (myConfirmacion == DialogResult.Yes)
            {
                // Paso a variables los datos del producto y de la cantidad
                dIdArticulo = Convert.ToDouble(dgvFabricacion.CurrentRow.Cells["IdArticulo"].Value);
                double dCant = Convert.ToDouble(dgvFabricacion.CurrentRow.Cells["Cantidad"].Value);
                // Variable para el valor del stock original
                double stockOriginal = 0;

                //**********************************************//
                // ACTUALIZACIÓN DEL STOCK DEL PRODUCTO
                //**********************************************//

                // Busco el artículo para sumarle el stock
                string myCadenaSQL = "select * from Articulos where IdArticulo = " + dIdArticulo;
                // Ejecuto la consulta y paso los datos a una tabla
                DataTable myTablaArticulos = clsDataBD.GetSql(myCadenaSQL);
                // Recorro la tabla y tomo el valor del stock original
                foreach (DataRow row in myTablaArticulos.Rows)
                {
                    // Paso a la variable el stock actual
                    stockOriginal = Convert.ToDouble(row["Stock"]);
                }
                // Al stock actual le sumo lo fabricado
                stockOriginal = stockOriginal - dCant;
                // Actualizo el artículo con el nuevo stock
                myCadenaSQL = "update Articulos set Stock = " + stockOriginal + " where IdArticulo = " + dIdArticulo;
                // Ejecuto la consulta
                clsDataBD.GetSql(myCadenaSQL);

                // Grabo el movimiento de stock
                GrabarMovimientoStock(Convert.ToInt32(dIdArticulo), 6, DateTime.Now, (dCant)*-1);

                //****************************************//
                // ACTUALIZACIÓN DEL STOCK DE LOS INSUMOS //
                //****************************************//
                // Busco el Id del articulo en la tabla y verifico si es compuesto de productos
                myCadenaSQL = "select * from Articulos where IdArticulo = " + dIdArticulo;
                DataTable myTablaArtic = clsDataBD.GetSql(myCadenaSQL);
                foreach (DataRow row in myTablaArtic.Rows)
                {
                    // Paso a la variable si es producto de productos
                    bComDeProd = Convert.ToBoolean(row["chkSProd"]);
                }
                // Busco el Id del producto usando el Id del Artículo
                myCadenaSQL = "select * from Productos where IdArticulo = " + dIdArticulo;
                // Ejecuto la consulta y paso los datos a una tabla
                DataTable myTablaProductos = clsDataBD.GetSql(myCadenaSQL);
                // Recorro la tabla para tomar el dato del Id del producto
                foreach (DataRow row in myTablaProductos.Rows)
                {
                    // Paso a la variable el stock actual
                    dIdProducto = Convert.ToDouble(row["IdProducto"]);

                    // Almaceno la cantidad de la tanda
                    if (!(row["Tanda"].ToString() == ""))
                    {
                        dTanda = Convert.ToDouble(row["Tanda"]);
                    }
                    if (bComDeProd)
                    {
                        // Busco los datos de la composicion del producto
                        myCadenaSQL = "select * from ProductosCompuestos where IdProdOrigen = " + dIdArticulo;
                    }
                    else
                    {
                        // Busco los datos de la composicion del producto
                        myCadenaSQL = "select * from ProductosInsumos where IdProducto = " + dIdProducto + " and Activo = 1";
                    }
                }

                // Ejecuto la consulta y paso los datos a una tabla
                DataTable myTablaComposicion = clsDataBD.GetSql(myCadenaSQL);
                // Si el producto tiene composicion
                if (myTablaComposicion.Rows.Count > 0)
                {
                    // Si está compuesto por insumos
                    if (!(bComDeProd))
                    {
                        // Variable que almacena la cantidad del insumo en la composicion
                        double CantComp = 0;
                        // Recorro la tabla que tiene la composición del producto
                        foreach (DataRow rowComp in myTablaComposicion.Rows)
                        {
                            // Tomo en la variable el Id del insumo
                            dIdInsumo = Convert.ToDouble(rowComp["IdInsumo"]);
                            // Tomo en la variable la cantidad del insumo y lo divido por la tanda para sacar el stock individual
                            CantComp = Convert.ToDouble(rowComp["Cantidad"]) / dTanda;
                            // Busco el Id del Articulo en la tabla Insumos
                            myCadenaSQL = "select * from Insumos where IdInsumo = " + dIdInsumo;
                            // Ejecuto la consulta y paso los datos a una tabla
                            DataTable myTablaInsumos = clsDataBD.GetSql(myCadenaSQL);
                            // Recorro la tabla para tomar el Id
                            foreach (DataRow rowInsumos in myTablaInsumos.Rows)
                            {
                                // Tomo el Id del artículo en la variable
                                dIdArticulo = Convert.ToDouble(rowInsumos["IdArticulo"]);
                                // Variable que almacena si el artículo lleva stock
                                bool bLlevaStock = false;
                                // Busco el artículo para sumarle el stock
                                string myCadenaSQLComp = "select * from Articulos where IdArticulo = " + dIdArticulo;
                                // Ejecuto la consulta y paso los datos a una tabla
                                DataTable myTablaArticulosComp = clsDataBD.GetSql(myCadenaSQLComp);
                                // Recorro la tabla y tomo el valor del stock original
                                foreach (DataRow row in myTablaArticulosComp.Rows)
                                {
                                    // Paso a la variable el stock actual
                                    stockOriginal = Convert.ToDouble(row["Stock"]);
                                    // Paso a la variable si el artículo lleva stock
                                    bLlevaStock = Convert.ToBoolean(row["LlevaStock"]);
                                }
                                // Al stock actual le sumo lo fabricado
                                stockOriginal = stockOriginal + (dCant * CantComp);
                                // Actualizo el artículo con el nuevo stock
                                myCadenaSQL = "update Articulos set Stock = " + stockOriginal + " where IdArticulo = " + dIdArticulo;
                                // si el artúclo lleva stock, lo actualizo
                                if (bLlevaStock)
                                {
                                    // Ejecuto la consulta
                                    clsDataBD.GetSql(myCadenaSQL);

                                    // Grabo el movimiento de stock
                                    GrabarMovimientoStock(Convert.ToInt32(dIdArticulo), 6, DateTime.Now, (dCant * CantComp) * -1);
                                }

                            }
                        }
                    }
                    // Si está compuesto por productos
                    else
                    {
                        // Variable que almacena la cantidad del producto en la composicion
                        double CantComp = 0;
                        // Recorro la tabla que tiene la composición del producto
                        foreach (DataRow rowComp in myTablaComposicion.Rows)
                        {
                            // Tomo en la variable el Id del Producto
                            dIdInsumo = Convert.ToDouble(rowComp["IdProdCompone"]);
                            // Tomo en la variable la cantidad del insumo y lo divido por la tanda para sacar el stock individual
                            CantComp = Convert.ToDouble(rowComp["Cantidad"]);
                            // Busco el Id del Articulo en la tabla Insumos
                            myCadenaSQL = "select * from Productos where IdArticulo = " + dIdInsumo;
                            // Ejecuto la consulta y paso los datos a una tabla
                            DataTable myTablaProds = clsDataBD.GetSql(myCadenaSQL);

                            // Recorro la tabla para tomar el Id
                            foreach (DataRow rowInsumos in myTablaProds.Rows)
                            {
                                // Tomo el Id del artículo en la variable
                                dIdArticulo = Convert.ToDouble(rowInsumos["IdArticulo"]);
                                // Variable que almacena si el artículo lleva stock
                                bool bLlevaStock = false;
                                // Busco el artículo para sumarle el stock
                                string myCadenaSQLComp = "select * from Articulos where IdArticulo = " + dIdArticulo;
                                // Ejecuto la consulta y paso los datos a una tabla
                                DataTable myTablaArticulosComp = clsDataBD.GetSql(myCadenaSQLComp);
                                // Recorro la tabla y tomo el valor del stock original
                                foreach (DataRow row in myTablaArticulosComp.Rows)
                                {
                                    // Paso a la variable el stock actual
                                    stockOriginal = Convert.ToDouble(row["Stock"]);
                                    // Paso a la variable si el artículo lleva stock
                                    bLlevaStock = Convert.ToBoolean(row["LlevaStock"]);
                                }
                                // Al stock actual le resto lo fabricado
                                stockOriginal = stockOriginal + (dCant * CantComp);
                                // Actualizo el artículo con el nuevo stock
                                myCadenaSQL = "update Articulos set Stock = " + stockOriginal + " where IdArticulo = " + dIdArticulo;
                                // si el artúclo lleva stock, lo actualizo
                                if (bLlevaStock)
                                {
                                    // Ejecuto la consulta
                                    clsDataBD.GetSql(myCadenaSQL);

                                    // Grabo el movimiento de stock
                                    GrabarMovimientoStock(Convert.ToInt32(dIdArticulo), 5, DateTime.Now, (dCant * CantComp));
                                }

                            }
                        }
                    }

                }
                else
                {
                    // Informo que el producto no tiene composición
                    MessageBox.Show("El producto no tiene una composición asociada, por lo que no se puede descontar el stock de los insumos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                //**********************************************//
                // ACTUALIZACIÓN DE LA TABLA DE STOCKFABRICACION
                //**********************************************//

                // Tomo en una variable el Id de la fabricacion desde la grilla
                int iIdFabricacion = Convert.ToInt32(dgvFabricacion.CurrentRow.Cells["IdFabricacion"].Value);
                // Armo la cadena SQL
                myCadenaSQL = "update StockFabricacion set Activo = 0 where IdFabricacion = " + iIdFabricacion;
                // Ejecuto la consulta
                clsDataBD.GetSql(myCadenaSQL);

            }

            // Cierro el formulario
            RecargarFormulario();

            // Limpio los controles
            LimpiarControlesForm();

        }

        #endregion

        #region Evento Click del botón Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Cambio el estado del formulario
            this.myEstado = "B";
            // Habilito los botones
            ActivarBotones();
            // Habilito los controles
            HabilitarControles();
            // limpio los campos de texto
            LimpiarControlesForm();
            // Cambio el estado de la bandera de búswueda
            BanderaBuscar = true;
            // Cambio el estado de la bandera que controla la grilla
            BanderaGrilla = false;
            // Pongo el foco en el primer control
            txtCodigo.Focus();

        }

        #endregion

        #region Evento Click del botón Imprimir

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Evento Click del botón Aceptar

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Variables con los datos de los txt para grabar
            string Cant = txtCantidad.Text;
            string Prod = txtProducto.Text;

            // Si alguno de los dos campos está vacío
            if (Cant == "" || Prod == "")
            {
                // Lo informo con un mensaje
                MessageBox.Show("Debe seleccionar un Producto y llenar el campo cantidad", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Salgo del evento
                return;
            }

            // Variable auxiliares para cálculos
            double stockOriginal = 0;
            // Variable que almacena la cantidad de la tanda fabricada
            double dTanda = 0;
            // Variable que almacena la cantidad en formato double
            double dCant = Convert.ToDouble(Cant);

            // Mensaje de confirmación
            DialogResult myRespuesta = MessageBox.Show("Confirma el alta de " + Cant + " " + Prod + " ?", "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (myRespuesta == DialogResult.Yes)
            {

                //**************************************//
                // ACTUALIZACIÓN DEL STOCK DEL PRODUCTO //
                //**************************************//

                // Busco el artículo para sumarle el stock
                string myCadenaSQL = "select * from Articulos where IdArticulo = " + dIdArticulo;
                // Ejecuto la consulta y paso los datos a una tabla
                DataTable myTablaArticulos = clsDataBD.GetSql(myCadenaSQL);
                // Recorro la tabla y tomo el valor del stock original
                foreach (DataRow row in myTablaArticulos.Rows)
                {
                    // Paso a la variable el stock actual
                    stockOriginal = Convert.ToDouble(row["Stock"]);
                }
                // Al stock actual le sumo lo fabricado
                stockOriginal = stockOriginal + dCant;
                // Actualizo el artículo con el nuevo stock
                myCadenaSQL = "update Articulos set Stock = " + stockOriginal + " where IdArticulo = " + dIdArticulo;
                // Ejecuto la consulta
                clsDataBD.GetSql(myCadenaSQL);

                // Grabo el movimiento de stock
                GrabarMovimientoStock(Convert.ToInt32(dIdArticulo), 5, DateTime.Now, dCant);

                //****************************************//
                // ACTUALIZACIÓN DEL STOCK DE LOS INSUMOS //
                //****************************************//
                // Busco el Id del articulo en la tabla y verifico si es compuesto de productos
                myCadenaSQL = "select * from Articulos where IdArticulo = " + dIdArticulo;
                DataTable myTablaArtic = clsDataBD.GetSql(myCadenaSQL);
                foreach (DataRow row in myTablaArtic.Rows)
                {
                    // Paso a la variable si es producto de productos
                    bComDeProd = Convert.ToBoolean(row["chkSProd"]);
                    // Paso a la variable si es insumo de insumos
                    bComDeIns = Convert.ToBoolean(row["CompIns"]);
                }

                // SI NO es insumo compuesto
                if (!(bComDeIns))
                {
                    // Busco el Id del producto usando el Id del Artículo
                    myCadenaSQL = "select * from Productos where IdArticulo = " + dIdArticulo;
                    // Ejecuto la consulta y paso los datos a una tabla
                    DataTable myTablaProductos = clsDataBD.GetSql(myCadenaSQL);
                    // Recorro la tabla para tomar el dato del Id del producto
                    foreach (DataRow row in myTablaProductos.Rows)
                    {
                        // Paso a la variable el stock actual
                        dIdProducto = Convert.ToDouble(row["IdProducto"]);

                        // Almaceno la cantidad de la tanda
                        if (!(row["Tanda"].ToString() == ""))
                        {
                            dTanda = Convert.ToDouble(row["Tanda"]);
                        }
                        if (bComDeProd)
                        {
                            // Busco los datos de la composicion del producto
                            myCadenaSQL = "select * from ProductosCompuestos where IdProdOrigen = " + dIdArticulo;
                        }
                        else
                        {
                            // Busco los datos de la composicion del producto
                            myCadenaSQL = "select * from ProductosInsumos where IdProducto = " + dIdProducto + " and Activo = 1";
                        }
                    }

                    // Ejecuto la consulta y paso los datos a una tabla
                    DataTable myTablaComposicion = clsDataBD.GetSql(myCadenaSQL);
                    // Si el producto tiene composicion
                    if (myTablaComposicion.Rows.Count > 0)
                    {
                        // Si está compuesto por insumos
                        if (!(bComDeProd))
                        {
                            // Variable que almacena la cantidad del insumo en la composicion
                            double CantComp = 0;
                            // Recorro la tabla que tiene la composición del producto
                            foreach (DataRow rowComp in myTablaComposicion.Rows)
                            {

                                // Tomo en la variable el Id del insumo
                                dIdInsumo = Convert.ToDouble(rowComp["IdInsumo"]);
                                // Tomo en la variable la cantidad del insumo y lo divido por la tanda para sacar el stock individual
                                CantComp = Convert.ToDouble(rowComp["Cantidad"]) / dTanda;
                                // Busco el Id del Articulo en la tabla Insumos
                                myCadenaSQL = "select * from Insumos where IdInsumo = " + dIdInsumo;
                                // Ejecuto la consulta y paso los datos a una tabla
                                DataTable myTablaInsumos = clsDataBD.GetSql(myCadenaSQL);
                                // Recorro la tabla para tomar el Id
                                foreach (DataRow rowInsumos in myTablaInsumos.Rows)
                                {
                                    // Tomo el Id del artículo en la variable
                                    dIdArticulo = Convert.ToDouble(rowInsumos["IdArticulo"]);
                                    // Variable que almacena si el artículo lleva stock
                                    bool bLlevaStock = false;
                                    bool bInsComp = false;
                                    // Busco el artículo para sumarle el stock
                                    string myCadenaSQLComp = "select * from Articulos where IdArticulo = " + dIdArticulo;
                                    // Ejecuto la consulta y paso los datos a una tabla
                                    DataTable myTablaArticulosComp = clsDataBD.GetSql(myCadenaSQLComp);
                                    // Recorro la tabla y tomo el valor del stock original
                                    foreach (DataRow row in myTablaArticulosComp.Rows)
                                    {
                                        // Paso a la variable el stock actual
                                        stockOriginal = Convert.ToDouble(row["Stock"]);
                                        // Paso a la variable si el artículo lleva stock
                                        bLlevaStock = Convert.ToBoolean(row["LlevaStock"]);
                                        // Paso a variable si es Insumo Compuesto
                                        bInsComp = Convert.ToBoolean(row["CompIns"]);
                                    }
                                    // Al stock actual le resto lo fabricado
                                    stockOriginal = stockOriginal - (dCant * CantComp);
                                    // Actualizo el artículo con el nuevo stock
                                    myCadenaSQL = "update Articulos set Stock = " + stockOriginal + " where IdArticulo = " + dIdArticulo;
                                    // si el artúclo lleva stock, lo actualizo
                                    if (bLlevaStock)
                                    {
                                        // Ejecuto la consulta
                                        clsDataBD.GetSql(myCadenaSQL);

                                        // Grabo el movimiento de stock
                                        GrabarMovimientoStock(Convert.ToInt32(dIdArticulo), 6, DateTime.Now, (dCant * CantComp) * -1);
                                    }
                                }
                            }
                        }
                        // Si está compuesto por productos
                        else
                        {
                            // Variable que almacena la cantidad del producto en la composicion
                            double CantComp = 0;
                            // Recorro la tabla que tiene la composición del producto
                            foreach (DataRow rowComp in myTablaComposicion.Rows)
                            {
                                // Tomo en la variable el Id del Producto
                                dIdInsumo = Convert.ToDouble(rowComp["IdProdCompone"]);
                                // Tomo en la variable la cantidad del insumo y lo divido por la tanda para sacar el stock individual
                                CantComp = Convert.ToDouble(rowComp["Cantidad"]);
                                // Busco el Id del Articulo en la tabla Insumos
                                myCadenaSQL = "select * from Productos where IdArticulo = " + dIdInsumo;
                                // Ejecuto la consulta y paso los datos a una tabla
                                DataTable myTablaProds = clsDataBD.GetSql(myCadenaSQL);

                                // Recorro la tabla para tomar el Id
                                foreach (DataRow rowInsumos in myTablaProds.Rows)
                                {
                                    // Tomo el Id del artículo en la variable
                                    dIdArticulo = Convert.ToDouble(rowInsumos["IdArticulo"]);
                                    // Variable que almacena si el artículo lleva stock
                                    bool bLlevaStock = false;
                                    // Busco el artículo para sumarle el stock
                                    string myCadenaSQLComp = "select * from Articulos where IdArticulo = " + dIdArticulo;
                                    // Ejecuto la consulta y paso los datos a una tabla
                                    DataTable myTablaArticulosComp = clsDataBD.GetSql(myCadenaSQLComp);
                                    // Recorro la tabla y tomo el valor del stock original
                                    foreach (DataRow row in myTablaArticulosComp.Rows)
                                    {
                                        // Paso a la variable el stock actual
                                        stockOriginal = Convert.ToDouble(row["Stock"]);
                                        // Paso a la variable si el artículo lleva stock
                                        bLlevaStock = Convert.ToBoolean(row["LlevaStock"]);
                                    }
                                    // Al stock actual le resto lo fabricado
                                    stockOriginal = stockOriginal - (dCant * CantComp);
                                    // Actualizo el artículo con el nuevo stock
                                    myCadenaSQL = "update Articulos set Stock = " + stockOriginal + " where IdArticulo = " + dIdArticulo;
                                    // si el artúclo lleva stock, lo actualizo
                                    if (bLlevaStock)
                                    {
                                        // Ejecuto la consulta
                                        clsDataBD.GetSql(myCadenaSQL);

                                        // Grabo el movimiento de stock
                                        GrabarMovimientoStock(Convert.ToInt32(dIdArticulo), 6, DateTime.Now, (dCant * CantComp) * -1);
                                    }

                                }
                            }
                        }

                    }
                    else
                    {
                        // Informo que el producto no tiene composición
                        MessageBox.Show("El producto no tiene una composición asociada, por lo que no se puede descontar el stock de los insumos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //**********************************************//
                    // ACTUALIZACIÓN DE LA TABLA DE STOCKFABRICACION PARA LOS PRODUCTOS
                    //**********************************************//

                    // Paso a variables los datos de los textbox
                    DateTime dFecha = dtpFecha.Value;
                    double dCantidad = Convert.ToDouble(Cant);

                    // Armo la cadena SQL
                    myCadenaSQL = "insert into StockFabricacion (Fecha, sFecha, IdProducto, IdInsumo, Cantidad, IdUsuario, Activo) values ('" +
                                    dFecha + "', '" +
                                    clsValida.ConvertirFecha(dFecha) + "', " +
                                    dIdProducto + ", 0, " +
                                    dCantidad + ", " +
                                    clsGlobales.UsuarioLogueado.IdUsuario + ", 1)";
                    // Ejecuto la consulta
                    clsDataBD.GetSql(myCadenaSQL);

                }
                // Si ES insumo compuesto
                else
                {
                     // Busco el Id del insumo usando el Id del Artículo
                    myCadenaSQL = "select * from Insumos where IdArticulo = " + dIdArticulo;
                    // Ejecuto la consulta y paso los datos a una tabla
                    DataTable myTablaProductos = clsDataBD.GetSql(myCadenaSQL);
                    // Recorro la tabla para tomar el dato del Id del producto
                    foreach (DataRow row in myTablaProductos.Rows)
                    {
                        // Paso a la variable el stock actual
                        dIdProducto = Convert.ToDouble(row["IdInsumo"]);

                    }

                    //Descontar composicion Insumos Compuestos
                    DescontarInsumoCompuesto(dIdArticulo, dCant);

                    //**********************************************//
                    // ACTUALIZACIÓN DE LA TABLA DE STOCKFABRICACION
                    //**********************************************//

                    // Paso a variables los datos de los textbox
                    DateTime dFecha = dtpFecha.Value;
                    double dCantidad = Convert.ToDouble(Cant);

                    // Armo la cadena SQL
                    myCadenaSQL = "insert into StockFabricacion (Fecha, sFecha, IdProducto, IdInsumo, Cantidad, IdUsuario, Activo) values ('" +
                                    dFecha + "', '" +
                                    clsValida.ConvertirFecha(dFecha) + "', 0,  " +
                                    dIdProducto + ", " +
                                    dCantidad + ", " +
                                    clsGlobales.UsuarioLogueado.IdUsuario + ", 1)";
                    // Ejecuto la consulta
                    clsDataBD.GetSql(myCadenaSQL);
                }

                

            }

            // Cierro el formulario
            btnCancelar.PerformClick();
        }

        #endregion

        #region MNetodo: DescontarInsumoCompuesto

        private void DescontarInsumoCompuesto(double IdArticulo, double dCantFab)
        {
            //Variables.
            double dCantInsComp = 0;
            int IdInsCompone = 0;
            
            // Busco el artículo para sumarle el stock
            string myCadenaSQLComp = "select * from InsumosCompuestos where IdInsOrigen = " + IdArticulo;
            // Ejecuto la consulta y paso los datos a una tabla
            DataTable myTablaArticulosComp = clsDataBD.GetSql(myCadenaSQLComp);
            // Recorro la tabla y tomo el valor del stock original
            foreach (DataRow row in myTablaArticulosComp.Rows)
            {
                // Paso a la variable el stock actual
                dCantInsComp = Convert.ToDouble(row["Cantidad"]) / Convert.ToDouble(row["cTanda"]);
                // Paso a la variable si el artículo lleva stock
                IdInsCompone = Convert.ToInt32(row["IdInsCompone"]);
                //Update
                myCadenaSQLComp = "UPDATE Articulos SET Stock = Stock - " + Convert.ToInt32((dCantInsComp * dCantFab))
                                  + " Where IdArticulo = " + DevolverId(IdInsCompone); //.n.g. 13-06-2018

                // Ejecuto la consulta
                clsDataBD.GetSql(myCadenaSQLComp);

                // Grabo el movimiento de stock
                GrabarMovimientoStock(Convert.ToInt32(IdInsCompone), 6, DateTime.Now, (dCantInsComp * dCantFab) * -1);
            }
        }

        #endregion

        #region Evento Click del botón Cancelar

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Cierro el formulario
            RecargarFormulario();
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

        #region Eventos de la grilla

        #region Evento SelectionChanged de la grilla

        private void dgvFabricacion_SelectionChanged(object sender, EventArgs e)
        {
            // Si ya se hizo click en la grilla
            if (BanderaGrilla)
            {
                // Paso los datos de la grilla a los controles
                TraerDatosGrilla();
            }
        }

        #endregion

        #region Evento CellContentClick de la grilla

        private void dgvFabricacion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Si ya se hizo click en la grilla
            if (BanderaGrilla)
            {
                // Paso los datos de la grilla a los controles
                TraerDatosGrilla();
            }
        }

        #endregion

        #region Evento Click de la grilla

        private void dgvFabricacion_Click(object sender, EventArgs e)
        {
            // Cambio el estado de la bandera de la grilla
            BanderaGrilla = true;
        }

        #endregion

        #endregion

        #region Eventos de los controles

        #region Eventos del txtCodigo

        private void txtCodigo_DoubleClick(object sender, EventArgs e)
        {
            // Variable que controla la selección
            bool bSeleccionado = false;
            // Vacío de datos el vector de los Productos
            clsGlobales.ProductosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.ProductosSeleccionados, new int[] { 0, 2 });
            clsGlobales.InsumosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.InsumosSeleccionados, new int[] { 0, 2 });

            frmArticulosBuscar myForm = new frmArticulosBuscar(true, true);
            myForm.ShowDialog();

            int LargoProductos = clsGlobales.ProductosSeleccionados.GetLength(0);
            int LargoInsumos = clsGlobales.InsumosSeleccionados.GetLength(0);

            // si hay cargados productos o insumos en los vectores
            if (!(clsGlobales.ProductosSeleccionados.Length / 2 == 0) && !((clsGlobales.ProductosSeleccionados.Length / 2) > 1))
            {
                // Cambio el estado de la bandera de selección
                bSeleccionado = true;
                
                // Paso a string el Id del proveedor y lo ingreso a la cadena
                dIdArticulo = clsGlobales.ProductosSeleccionados[0, 0];

                // Armo la cadena SQL
                string myCadenaSQL = "select * from Articulos_Insumos_Productos_Fab where IdArticulo = " + dIdArticulo;
                // Creo una tabla que me va a almacenar el resultado de la consulta
                DataTable myTablaAux = clsDataBD.GetSql(myCadenaSQL);
                // Recorro la tabla y paso los datos del Artículo a un datarow
                foreach (DataRow row in myTablaAux.Rows)
                {
                    txtCodigo.Text = row["CodigoArticulo"].ToString();
                    txtProducto.Text = row["Articulo"].ToString();
                }

                // Pongo el foco en el campo cantidad
                txtCantidad.Focus();
                
            }
            // si hay cargados productos o insumos en los vectores
            if (!(clsGlobales.InsumosSeleccionados.Length / 2 == 0) && !((clsGlobales.InsumosSeleccionados.Length / 2) > 1))
            {
                // Cambio el estado de la bandera de selección
                bSeleccionado = true;
                
                // Paso a string el Id del proveedor y lo ingreso a la cadena
                dIdArticulo = clsGlobales.InsumosSeleccionados[0, 0];

                // Armo la cadena SQL
                string myCadenaSQL = "select * from Articulos_Insumos_Productos_Fab where IdArticulo = " + dIdArticulo;
                // Creo una tabla que me va a almacenar el resultado de la consulta
                DataTable myTablaAux = clsDataBD.GetSql(myCadenaSQL);
                // Verifico que el insumo tenga composición
                int iFilas = myTablaAux.Rows.Count;
                // Si no tengo filas, el insumo no es compuesto
                if (iFilas > 0)
                {
                    // Recorro la tabla y paso los datos del Artículo a un datarow
                    foreach (DataRow row in myTablaAux.Rows)
                    {
                        txtCodigo.Text = row["CodigoArticulo"].ToString();
                        txtProducto.Text = row["Articulo"].ToString();
                    }

                    // Pongo el foco en el campo cantidad
                    txtCantidad.Focus();
                }
                else
                {
                    MessageBox.Show("El insumo seleccionado no es compuesto", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
            if (!(bSeleccionado))
            {
                MessageBox.Show("Debe seleccionarse un producto", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            if (BanderaBuscar)
            {
                // Cargo la grilla por el campo buscado
                CargarGrilla(txtCodigo.Text.ToUpper(), "CodigoArticulo");
            }
        }

        private void txtCodigo_Enter(object sender, EventArgs e)
        {
            if (BanderaBuscar)
            {
                // Si pongo el foco en Codigo, vacío el campo producto
                txtProducto.Text = "";
            }
        }

        #endregion

        #region Eventos del txtProducto

        private void txtProducto_TextChanged(object sender, EventArgs e)
        {
            if (BanderaBuscar)
            {
                // Cargo la grilla por el campo buscado
                CargarGrilla(txtProducto.Text.ToUpper(), "Articulo");
            }
        }

        private void txtProducto_Enter(object sender, EventArgs e)
        {
            if (BanderaBuscar)
            {
                // Si pongo el foco en producto, vacío el campo Codigo
                txtCodigo.Text = "";
            }

        }

        #endregion

        #region Eventos del txtCantidad

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
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
                this.btnAceptar.Focus();
            }
        }

        private void txtCantidad_Leave(object sender, EventArgs e)
        {
            // Si el txt no está vacío, lo formateo con dos decimales
            if (!(this.txtCantidad.Text == ""))
            {
                this.txtCantidad.Text = Convert.ToDouble(this.txtCantidad.Text).ToString("#0.00");
            }
        }

        #endregion

        #endregion

        #endregion

        #region Métodos del Formulario

        #region Método para activar los botones del formulario
        //--------------------------------------------------------------
        //ACTIVAR BOTONES  
        //SEGUN EL ESTADO (A, M, C) - MUESTRA U OCULTA BOTONES
        //--------------------------------------------------------------
        private void ActivarBotones()
        {
            switch (this.myEstado)
            {
                case "A":
                case "M":
                    this.btnAgregar.TabStop = false;
                    this.btnAgregar.Visible = false;
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
                    this.btnBuscar.TabStop = false;
                    this.btnBuscar.Visible = false;
                    this.btnAceptar.TabStop = false;
                    this.btnAceptar.Visible = false;
                    this.btnCancelar.TabStop = true;
                    this.btnCancelar.Visible = true;
                    this.btnSalir.TabStop = true;
                    this.btnSalir.Visible = true;
                    this.btnBorrar.TabStop = false;
                    this.btnBorrar.Visible = false;
                    this.btnImprimir.TabStop = false;
                    this.btnImprimir.Visible = false;
                    return;
                case "C":
                    this.btnAgregar.TabStop = true;
                    this.btnAgregar.Visible = true;
                    this.btnBuscar.TabStop = true;
                    this.btnBuscar.Visible = true;
                    this.btnAceptar.TabStop = false;
                    this.btnAceptar.Visible = false;
                    this.btnCancelar.TabStop = false;
                    this.btnCancelar.Visible = false;
                    this.btnSalir.TabStop = true;
                    this.btnSalir.Visible = true;
                    // Si el nivel del usuario es el 5, habilito el botón borrar
                    if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelStock)
                    {
                        this.btnBorrar.TabStop = true && (dgvFabricacion.RowCount != 0);
                        this.btnBorrar.Visible = true && (dgvFabricacion.RowCount != 0);
                    }
                    else
                    {
                        this.btnBorrar.TabStop = false;
                        this.btnBorrar.Visible = false;
                    }
                    this.btnImprimir.TabStop = true && (dgvFabricacion.RowCount != 0);
                    this.btnImprimir.Visible = true && (dgvFabricacion.RowCount != 0);
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
            switch (this.myEstado)
            {
                case "A":
                case "M":
                    this.dtpFecha.TabStop = true;
                    this.dtpFecha.Enabled = true;
                    this.txtCodigo.TabStop = true;
                    this.txtCodigo.Enabled = true;
                    this.txtProducto.TabStop = false;
                    this.txtProducto.Enabled = false;
                    this.txtCantidad.TabStop = true;
                    this.txtCantidad.Enabled = true;
                    this.dgvFabricacion.TabStop = false;
                    this.dgvFabricacion.Enabled = false;
                    
                    return;
                case "B":
                    this.dtpFecha.TabStop = false;
                    this.dtpFecha.Enabled = false;
                    this.txtCodigo.TabStop = true;
                    this.txtCodigo.Enabled = true;
                    this.txtProducto.TabStop = true;
                    this.txtProducto.Enabled = true;
                    this.txtCantidad.TabStop = false;
                    this.txtCantidad.Enabled = false;
                    this.dgvFabricacion.TabStop = false;
                    this.dgvFabricacion.Enabled = false;
                    
                    return;
                case "C":
                    this.dtpFecha.TabStop = false;
                    this.dtpFecha.Enabled = false;
                    this.txtCodigo.TabStop = false;
                    this.txtCodigo.Enabled = false;
                    this.txtProducto.TabStop = false;
                    this.txtProducto.Enabled = false;
                    this.txtCantidad.TabStop = false;
                    this.txtCantidad.Enabled = false;
                    this.dgvFabricacion.TabStop = true;
                    this.dgvFabricacion.Enabled = true;
                    
                    return;
            }
        }
        #endregion

        #region Método para limpiar los controles del formulario
        //LIMPIA LA PROPIEDAD TEXT DE LOS CONTROLES DEL FORMULARIO. N.
        private void LimpiarControlesForm()
        {
            this.dtpFecha.Value = DateTime.Today;
            this.txtCodigo.Text = "";
            this.txtProducto.Text = "";
            this.txtCantidad.Text = "";
        }
        #endregion

        #region Método que carga los ToolTips

        private void CargarToolTips()
        {
            toolTip1.SetToolTip(this.btnAgregar, "Agregar");
            toolTip3.SetToolTip(this.btnBorrar, "Borrar");
            toolTip4.SetToolTip(this.btnImprimir, "Imprimir");
            toolTip5.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip6.SetToolTip(this.btnCancelar, "Cancelar");
            toolTip7.SetToolTip(this.btnSalir, "Salir");
            toolTip8.SetToolTip(this.btnBuscar, "Buscar");
        }

        #endregion

        #region Método que devuelve el foco a la fila desde donde se presionó algún botón

        private void PosicionarFocoFila()
        {
            if (dgvFabricacion.Rows.Count != 0 && dgvFabricacion.Rows.Count > this.indexFila)
            {
                // Devuelvo el foco a la fila de la grilla desde donde se llamó
                this.dgvFabricacion.CurrentCell = dgvFabricacion[2, this.indexFila];

                // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                EventArgs ea = new EventArgs();
                this.dgvFabricacion_SelectionChanged(this.dgvFabricacion, ea);
            }
            
        }

        #endregion

        #region Método que carga la grilla

        private void CargarGrilla(string Buscar, string Campo)
        {
            // Variable qie almacena la cadena SQL
            string myCadenaSQL = "";
            // Si no estoy buscando
            if (Buscar == "")
            {
                // Armo la cadena SQL
                myCadenaSQL = "select * from Vista_StockFabricacion_Insumos where Activo = 1 order by IdFabricacion";
            }
            else
            {
                // Cadena SQL 
                myCadenaSQL = "select * from Vista_StockFabricacion_Insumos where " + Campo + " like '" + Buscar + "%' order by " + Campo;
            }
            
            // Ejecuto la consulta y paso los datos a una tabla
            DataTable myTabla = clsDataBD.GetSql(myCadenaSQL);
            // No dejo que la grilla genere de manera automática las columnas
            dgvFabricacion.AutoGenerateColumns = false;
            // Asigno el source a la grilla
            dgvFabricacion.DataSource = myTabla;

        }

        #endregion

        #region Método que recarga el formulario

        private void RecargarFormulario()
        {
            // Cambio el estado del formulario a agregar. G.
            this.myEstado = "C";
            // Cambio el estado de la bandera de búesqueda
            BanderaBuscar = false;
            // Cambio el estado de la bandera de la grilla
            BanderaGrilla = true;
            // Cargo la grilla
            CargarGrilla("", "");
            // Limpio los controles del formulario. G.    
            ActivarBotones();
            // Habilito los controles para este estado. G.
            HabilitarControles();
            // Si tengo filas en la grilla
            if (dgvFabricacion.Rows.Count > 0)
            {
                // Posiciono el foco en la fila desde donde se llamo
                PosicionarFocoFila();
            }
        }

        #endregion

        #region Método que trae los datos de la grilla a los controles

        private void TraerDatosGrilla()
        {
            // Paso los datos de la fila actual a una variable
            DataGridViewRow row = dgvFabricacion.CurrentRow;

            // Paso los datos de la variable a los campos del formulario
            dtpFecha.Value = Convert.ToDateTime(row.Cells["Fecha"].Value);
            txtCodigo.Text = row.Cells["CodigoArticulo"].Value.ToString();
            txtProducto.Text = row.Cells["Producto"].Value.ToString();
            txtCantidad.Text = row.Cells["Cantidad"].Value.ToString();

        }

        #endregion

        #region Método que graba el movimiento de Stock en la tabla StockMovimientos

        private void GrabarMovimientoStock(int IdArt, int IdMot, DateTime Fec, double Cant)
        {
            // Variable que almacena la cadena SQL
            string myCadenaSql = "insert into StockMovimientos (IdArticulo, IdStockMotivo, Fecha, sFecha, Cantidad, IdUsuario, Activo) values (" 
                                + IdArt + ", "
                                + IdMot + ", '"
                                + Fec + "', '"
                                + clsValida.ConvertirFecha(Fec) + "', "
                                + Cant + ", "
                                + clsGlobales.UsuarioLogueado.IdUsuario + ", 1)";
            // Ejecuto la consulta
            clsDataBD.GetSql(myCadenaSql);
        }

        #endregion

        #region Método que deshabilita el reordenamiento de las grilla desde sus cabeceras

        private void DeshabilitarOrdenGrillas()
        {
            // Deshabilito la reordenación de las columnas de las grillas
            foreach (DataGridViewColumn dgvCol in dgvFabricacion.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        #endregion

        #endregion

        #region

        private int DevolverId(int idInsumo)
        {
            int aux = 0;

            string myCadena = "select * from Insumos where IdInsumo = " + idInsumo;
            // Ejecuto la consulta
            DataTable myData = clsDataBD.GetSql(myCadena);
            //
            foreach (DataRow row in myData.Rows)
            {
                aux = Convert.ToInt32(row["IdArticulo"]);
            }

            return aux;
        }

        #endregion

    }
}

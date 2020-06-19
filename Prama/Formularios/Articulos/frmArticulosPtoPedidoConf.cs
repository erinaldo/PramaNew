using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prama.Formularios.Articulos
{
    public partial class frmArticulosPtoPedidoConf : Form
    {
        // Variables globales
        int CantidadPedida;
        DateTime FechaOriginal;
        double dIdArticulo;
        double dIdInsumo;
        double dIdProducto;
        int iIdEmpleado;
        
        string Articulo;

        // Bandera que me indica si esta compuesto por productos
        bool bComDeProd = false;
        // Bandera que indica si es insumo compuesto
        bool bComDeIns = false;
        
        public frmArticulosPtoPedidoConf(int Cant, DateTime Fech, int IdArt, int IdIns, int IdProd, string Art, int IdEmpl)
        {
            // Constructor del formulario
            InitializeComponent();
            // Pasaje a las variables globales de los parámetros de entrada
            CantidadPedida = Cant;
            FechaOriginal = Fech;
            dIdArticulo = IdArt;
            dIdProducto = IdProd;
            dIdInsumo = IdIns;
            Articulo = Art;
            iIdEmpleado = IdEmpl;
        }

        private void frmArticulosPtoPedidoConf_Load(object sender, EventArgs e)
        {

			//icon
            clsFormato.SetIconForm(this); 
			
            if (clsGlobales.myEstado == "A" || clsGlobales.myEstado =="M")
            {
                label1.Text = "Pedido";
                
                cboEmpleado.Visible = true;
                label3.Visible = true;
            }

            // Pongo el valor de la cantidad pedida
            txtFabricados.Text = CantidadPedida.ToString();
            // Pongo la fecha original
            dtpFecha.Value = FechaOriginal;
            // Cargo el combo con los empleados
            clsDataBD.CargarCombo(cboEmpleado, "Empleados", "Empleado", "IdEmpleado","Activo=1");
            if (iIdEmpleado == 0)
            {
                cboEmpleado.SelectedIndex = -1;
                cboEmpleado.Enabled = true;
            }
            else
            {
                // Vacío la selección del combo
                cboEmpleado.SelectedValue = iIdEmpleado;
                // Deshabilito el combo
                cboEmpleado.Enabled = false;
            }
            
            // cargo los toolstips
            CargarToolTips();
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            clsGlobales.bBandera = false;
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            if (clsGlobales.myEstado == "M" || clsGlobales.myEstado == "F")
            {
                string sFechaDtp = dtpFecha.Value.ToShortDateString();
                string sFechaActual = DateTime.Now.ToShortDateString();

               /* if (Convert.ToDateTime(sFechaDtp) < Convert.ToDateTime(sFechaActual))
                {
                    MessageBox.Show("No puede programar para un día anterior. Vuelva a intentarlo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtFabricados.Focus();
                    return;
                }
                else
                {
                    Fabricar();
                    this.Close();
                }*/

                Fabricar();
                this.Close();
            }
            else if (clsGlobales.myEstado == "A")
            {
                if (dtpFecha.Value >= FechaOriginal)
                {
                    Fabricar();
                }
                else
                {
                    MessageBox.Show("Datos incorrectos. Vuelva a intentarlo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtFabricados.Focus();
                    return;
                }
            }
            
            this.Close();
        }

        private void Fabricar()
        {
            if(!(txtFabricados.Text == "" || Convert.ToDouble(txtFabricados.Text) <= 0) && Convert.ToInt32(cboEmpleado.SelectedValue) !=0)
            {
                clsGlobales.CantFabricada = Convert.ToInt32(txtFabricados.Text);
                clsGlobales.FechaFabricada = dtpFecha.Value;
                clsGlobales.EmpleadoFabricado = cboEmpleado.Text;
                clsGlobales.IdEmpleadoFabricado = Convert.ToInt32(cboEmpleado.SelectedValue);

                clsGlobales.bBandera = true;

                if (clsGlobales.myEstado == "F")
                {
                    if (Convert.ToInt32(cboEmpleado.SelectedIndex) != -1)
                    {
                        ActualizarStock();
                    }
                    else
                    {
                        MessageBox.Show("Datos incorrectos. Vuelva a intentarlo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtFabricados.Focus();
                        return;
                    }

                }
            }
            
        }

        private void ActualizarStock()
        {
            // Variables con los datos de los txt para grabar
            string Cant = clsGlobales.CantFabricada.ToString();
            string Prod = Articulo;

            bool bLlevaStock = false;

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
            // DialogResult myRespuesta = MessageBox.Show("Confirma el alta de " + Cant + " " + Prod + " ?", "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

             // if (myRespuesta == DialogResult.Yes)
            // {

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
                bLlevaStock = Convert.ToBoolean(row["LlevaStock"]);
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
                // Paso a la variable si es insumo compuesto
                bComDeIns = Convert.ToBoolean(row["CompIns"]);
            }

            // Si no es un insumo compuestos
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
                // ACTUALIZACIÓN DE LA TABLA DE STOCKFABRICACION
                //**********************************************//

                // Paso a variables los datos de los textbox
                DateTime dFecha = dtpFecha.Value;
                double dCantidad = Convert.ToDouble(Cant);

                // Armo la cadena SQL
                myCadenaSQL = "insert into StockFabricacion (Fecha, sFecha, IdProducto, Cantidad, IdUsuario, Activo, IdInsumo) values ('" +
                                dFecha + "', '" +
                                clsValida.ConvertirFecha(dFecha) + "', " +
                                dIdProducto + ", " +
                                dCantidad + ", " +
                                clsGlobales.UsuarioLogueado.IdUsuario + ", 1, 0)";
                // Ejecuto la consulta
                clsDataBD.GetSql(myCadenaSQL);
            }
            else
            {
                // Armo la cadena para buscar la composición del insumo
                myCadenaSQL = "select * from InsumosCompuestos where IdInsOrigen = " + dIdArticulo;
                // Ejecuto la consulta y paso los datos a una tabla
                DataTable myTablaComposicion = clsDataBD.GetSql(myCadenaSQL);
                // Si el insumo tiene composición
                if (myTablaComposicion.Rows.Count > 0)
                {
                    // Variable que almacena la cantidad del insumo en la composicion
                    double CantComp = 0;
                    int iIdInsCompone = 0;
                    int iIdArtComp = 0;
                    // Recorro la tabla que tiene la composición del producto
                    foreach (DataRow rowComp in myTablaComposicion.Rows)
                    {
                        iIdInsCompone = Convert.ToInt32(rowComp["IdInsCompone"]);
                        CantComp = Convert.ToDouble(rowComp["Cantidad"]) / Convert.ToDouble(rowComp["cTanda"]);

                        // Busco en la tabla de insumos el Id del Articulo que le corresponde
                        myCadenaSQL = "select * from Insumos where IdInsumo = " + iIdInsCompone;
                        // Paso los datos del insumo a una tabla
                        DataTable myInsumo = clsDataBD.GetSql(myCadenaSQL);
                        // Recorro la tabla para obtener el id del articulo
                        foreach (DataRow rowArt in myInsumo.Rows)
                        {
                            // Paso a la variable el id del articulo
                            iIdArtComp = Convert.ToInt32(rowArt["IdArticulo"]);
                            // Paso a la variable si el artículo lleva stock
                            
                            double dCantDescontar = (CantComp * clsGlobales.CantFabricada);
                            // Armo la cadena para actualizar el stock del insumo
                            myCadenaSQL = "update Articulos set Stock = Stock - " + Convert.ToInt32(dCantDescontar) + " where IdArticulo = " + iIdArtComp;
                            if (bLlevaStock)
                            {
                                // Ejecuto la consulta
                                clsDataBD.GetSql(myCadenaSQL);

                                // Grabo el movimiento de stock
                                GrabarMovimientoStock(Convert.ToInt32(iIdArtComp), 6, DateTime.Now, dCantDescontar * -1);
                            }
                        }
                    }
                }
            }

            //**********************************************//
            // ACTUALIZACIÓN DE LA TABLA DE STOCKFABRICACION
            //**********************************************//

            // Paso a variables los datos de los textbox
            DateTime dFechaIns = dtpFecha.Value;
            double dCantidadIns = Convert.ToDouble(Cant);

            // Armo la cadena SQL
            myCadenaSQL = "insert into StockFabricacion (Fecha, sFecha, IdProducto, Cantidad, IdUsuario, Activo, IdInsumo) values ('" +
                            dFechaIns + "', '" +
                            clsValida.ConvertirFecha(dFechaIns) + "', " +
                            0 + ", " +
                            dCantidadIns + ", " +
                            clsGlobales.UsuarioLogueado.IdUsuario + ", 1, " +
                            dIdInsumo + ")";
            // Ejecuto la consulta
            clsDataBD.GetSql(myCadenaSQL);
                
        }

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

        #region CargarToolTips

        private void CargarToolTips()
        {
            toolTip1.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip2.SetToolTip(this.btnCancelar, "Cancelar");
        }

        #endregion
    }
}

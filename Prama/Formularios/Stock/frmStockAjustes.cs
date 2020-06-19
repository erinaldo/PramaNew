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
    public partial class frmStockAjustes : Form
    {
        // Variable que almacena todas las cadenas string para consultas a la base
        string myCadenaSQL;
        //Estado
        string myEstado = "";
        
        public frmStockAjustes()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmStockAjustes_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            //Cargar Form
            CargarFormulario();
            //Titulo Ventana
            this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;
        }

        private void CargarGrilla()
        {
            // Si está abierta la conexión 2
            if (clsGlobales.ConB != null)
            {
                try
                {
                    // Armo la cadena SQL y paso los datos a la tabla
                    myCadenaSQL = "select * from Vista_StockAjustes_2 where Activo = 1";
                    DataTable myTabla = clsDataBD.GetSqlB(myCadenaSQL);
                    // Evito que la rilla genera sus propias columnas
                    dgvAjustes.AutoGenerateColumns = false;
                    // Asigno los datos de la tabla a la grilla
                    dgvAjustes.DataSource = myTabla;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                // Mensaje de confirmación
                MessageBox.Show("Error al cargar los datos. Reintente por favor...", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Cierro la ventana (ConB is null)
                this.Close();
            
            }
            
        }

        private void CargarDetalle(int Id)
        {
             try
             {
                    // Armo la cadena SQL y paso los datos a la tabla
                    myCadenaSQL = "select * from Vista_StockAjusteDetalles_2 where Activo = 1 and IdStockAjuste = " + Id;
                    DataTable myTabla = clsDataBD.GetSqlB(myCadenaSQL);
                    // Evito que la rilla genera sus propias columnas
                    dgvDetalle.AutoGenerateColumns = false;
                    // Asigno los datos de la tabla a la grilla
                    dgvDetalle.DataSource = myTabla;

                    int fila = 1;
                    // Recorro la grilla
                    foreach (DataGridViewRow row in dgvDetalle.Rows)
                    {
                        // Asigno el valor del contador al Item de la fila
                        row.Cells["Item"].Value = fila;
                        // Incremento el contador
                        fila++;
                    }
             }
             catch (Exception e)
             {
                 MessageBox.Show(e.Message);
             }
        }

        private void dgvAjustes_SelectionChanged(object sender, EventArgs e)
        {
            CargarDetalle(Convert.ToInt32(dgvAjustes.CurrentRow.Cells["IdStockAjuste"].Value));
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmStockAjustesABM myForm = new frmStockAjustesABM();
            myForm.ShowDialog();

            CargarFormulario();

            if (clsGlobales.ConB != null)
            {
                //Grilla
                CargarGrilla();
                //Hay datos?
                if (dgvAjustes.Rows.Count > 0)
                {
                  //Detalle
                  CargarDetalle(Convert.ToInt32(dgvAjustes.CurrentRow.Cells["IdStockAjuste"].Value));
                }
            }
        }

        #region Método que deshabilita el reordenamiento de las grilla desde sus cabeceras

        private void DeshabilitarOrdenGrillas()
        {
            // Deshabilito la reordenación de las columnas de las grillas
            foreach (DataGridViewColumn dgvCol in dgvAjustes.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            // Deshabilito la reordenación de las columnas de las grillas
            foreach (DataGridViewColumn dgvCol in dgvDetalle.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;
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
            switch (this.myEstado)
            {
                case "A":
                case "M":
                    this.btnAgregar.TabStop = false;
                    this.btnAgregar.Visible = false;
                    this.btnBuscar.TabStop = false;
                    this.btnBuscar.Visible = false;
                    // this.btnAceptar.TabStop = true;
                    // this.btnAceptar.Visible = true;
                    // this.btnCancelar.TabStop = true;
                    // this.btnCancelar.Visible = true;
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
                    // this.btnAceptar.TabStop = false;
                    // this.btnAceptar.Visible = false;
                    // this.btnCancelar.TabStop = true;
                    // this.btnCancelar.Visible = true;
                    this.btnSalir.TabStop = true;
                    this.btnSalir.Visible = true;
                    this.btnBorrar.TabStop = false;
                    this.btnBorrar.Visible = false;
                    this.btnImprimir.TabStop = true && (dgvAjustes.RowCount != 0);
                    this.btnImprimir.Visible = true && (dgvAjustes.RowCount != 0);
                    return;
                case "C":
                    this.btnAgregar.TabStop = true;
                    this.btnAgregar.Visible = true;
                    this.btnBuscar.TabStop = true && (dgvAjustes.RowCount != 0);
                    this.btnBuscar.Visible = true && (dgvAjustes.RowCount != 0);
                    // this.btnAceptar.TabStop = false;
                    // this.btnAceptar.Visible = false;
                    // this.btnCancelar.TabStop = false;
                    // this.btnCancelar.Visible = false;
                    this.btnSalir.TabStop = true;
                    this.btnSalir.Visible = true;
                    // Si el nivel del usuario es el 5, habilito el botón borrar
                    if (clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelStock)
                    {
                        this.btnBorrar.TabStop = true && (dgvAjustes.RowCount != 0);
                        this.btnBorrar.Visible = true && (dgvAjustes.RowCount != 0);
                    }
                    else
                    {
                        this.btnBorrar.TabStop = false;
                        this.btnBorrar.Visible = false;
                    }
                    this.btnImprimir.TabStop = true && (dgvAjustes.RowCount != 0);
                    this.btnImprimir.Visible = true && (dgvAjustes.RowCount != 0);
                    return;
            }
        }
        #endregion

        #region Método que carga los ToolTips

        private void CargarToolTips()
        {
            toolTip1.SetToolTip(this.btnAgregar, "Agregar");
            toolTip3.SetToolTip(this.btnBorrar, "Borrar");
            toolTip4.SetToolTip(this.btnBuscar, "Buscar");
            toolTip5.SetToolTip(this.btnImprimir, "Imprimir");
            toolTip6.SetToolTip(this.btnSalir, "Salir");
        }

        #endregion

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            // Variable que almacena el Id del ajustge
            int iIdAjuste = 0;
            // Variable que almacena el Id del motivo del ajuste
            int iIdMotivo = 0;
            
            //Validar el nivel del usuario para ver si tiene permisos
            if (!(clsGlobales.UsuarioLogueado.Nivel >= clsGlobales.cParametro.NivelBaja))
            {
                // El usuario no es de nivel 5, por lo que no puede crear un nuevo usuario. G.
                MessageBox.Show("Usted no tiene los permisos para Eliminar el Ajuste de Stock!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Fin
                return;
            }
            else
            {
                // Tomo los datos de la fila actual
                DataGridViewRow rowActual = dgvAjustes.CurrentRow;

                // Actualizo el Id del artículo para los movimientos de stock
                iIdAjuste = Convert.ToInt32(rowActual.Cells["IdStockAjuste"].Value);
                iIdMotivo = Convert.ToInt32(rowActual.Cells["IdStockMotivo"].Value);

                // Confirmo la eliminación
                DialogResult myConfirmacion = MessageBox.Show("Desea eliminar el Ajuste ?", "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Si confirma
                if (myConfirmacion == DialogResult.Yes)
                {
                    // Verifico si el movimiento original era de entrada
                    bool bEntrada = RetornarEntrada(iIdMotivo);

                    // Variable auxiliares para cálculos
                    double stockOriginal = 0;
                    double dCantMov = 0;
                    double dCant = 0;

                    // Busco el detalle del ajuste en su tabla
                    // Armo la cadena SQL y paso los datos a la tabla
                    myCadenaSQL = "select * from Vista_StockAjusteDetalles_2 where IdStockAjuste = " + iIdAjuste;
                    DataTable myTablaDetalles = clsDataBD.GetSqlB(myCadenaSQL);
                    // Recorro la tabla
                    foreach (DataRow NewRow in myTablaDetalles.Rows)
                    {
                        // Variable que almacena la cantidad en formato double
                        dCant = Convert.ToDouble(NewRow["Cantidad"]);

                        dCantMov = dCant;
                        // Busco el artículo para sumarle el stock
                        myCadenaSQL = "select * from Articulos where IdArticulo = " + Convert.ToInt32(NewRow["IdArticulo"]);
                        // Ejecuto la consulta y paso los datos a una tabla
                        DataTable myTablaArticulos = clsDataBD.GetSql(myCadenaSQL);
                        // Recorro la tabla y tomo el valor del stock original
                        foreach (DataRow row in myTablaArticulos.Rows)
                        {
                            // Paso a la variable el stock actual
                            stockOriginal = Convert.ToDouble(row["Stock"]);
                        }
                        // Si el motivo del movimiento es entrada
                        if (bEntrada)
                        {
                            // al stock original le sumo la cantidad del movimiento
                            stockOriginal = stockOriginal - dCant;
                        }
                        // Si el motivo del movimiento no es entrada
                        else
                        {
                            // al stock original le resto la cantidad del movimiento
                            stockOriginal = stockOriginal + dCant;
                            
                        }

                        // Actualizo el artículo con el nuevo stock ( BASE 1)
                        myCadenaSQL = "update Articulos set Stock = " + stockOriginal + " where IdArticulo = " + Convert.ToInt32(NewRow["IdArticulo"]);
                        // Ejecuto la consulta
                        clsDataBD.GetSql(myCadenaSQL);

                        // Grabo el movimiento de stock
                        GrabarMovimientoStock(Convert.ToInt32(NewRow["IdArticulo"]), iIdMotivo, DateTime.Now, dCantMov);
                    }
                   
                    // Elimino el detalle del ajuste
                    myCadenaSQL = "update StockAjusteDetalles set Activo = 0 where IdStockAjuste = " + iIdAjuste;
                    // Ejecuto la consulta
                    clsDataBD.GetSqlB(myCadenaSQL);
                    // Elimino el ajuste
                    myCadenaSQL = "update StockAjustes set activo = 0 where IdStockAjuste = " + iIdAjuste;
                    // Ejecuto la consulta
                    clsDataBD.GetSqlB(myCadenaSQL);
                    // Recargo el formulario
                    CargarFormulario();
                }
            }

        }

        #region Método que retorna si el Motivo elegido es entrada (ALTA)

        private bool RetornarEntrada(int IdMot)
        {
            // Variable de retorno
            bool aux = false; ;
            // Armo la cadena SQL
            string myCadenaSql = "select * from StockMotivos where IdStockMotivo = " + IdMot;
            // Ejecuto la consulta y lleno la tabla
            DataTable myTabla = clsDataBD.GetSql(myCadenaSql);
            // Reccorro la tabla y toma el valor en la variabe
            foreach (DataRow row in myTabla.Rows)
            {
                // Si es entrada
                if (Convert.ToBoolean(row["Entrada"]))
                {
                    // Paso el valor a la variable de retorno
                    aux = true;
                }
            }
            // Devuelvo el valor
            return aux;
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
            clsDataBD.GetSqlB(myCadenaSql);
        }

        #endregion

        private void CargarFormulario()
        {
            // Cambio mi estado a en espera
            myEstado = "C";
            // Deshabilito el ordenamiento de las grillas desde la cabecera
            DeshabilitarOrdenGrillas();
            // Cargo la grilla
            CargarGrilla();
            // Llamo al método activar los botones del formulario. G.
            ActivarBotones();
            // Cargar ToolTips
            CargarToolTips();
        }
        
    }
}

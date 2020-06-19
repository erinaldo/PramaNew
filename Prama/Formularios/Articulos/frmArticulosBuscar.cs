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
    public partial class frmArticulosBuscar : Form
    {
        // Declaro las variables que me permiten filtrar por el tipo de proveedor
        bool bInsumosChk;
        bool bProductosChk;
        
        
        public frmArticulosBuscar(bool ins, bool Prod)
        {
            InitializeComponent();
            // Asigno los parámetros a las variables
            bInsumosChk = ins;
            bProductosChk = Prod;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Cierro el formulario
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Cierro el formulario con los datos de los proveedores en el vector
            this.Close();
        }

        #region Métodos del Formulario

        #region Método para cargar la grilla

        private void CargarArticulos(string Buscar, string Campo)
        {
            // Variable para la cadena SQL
            string myCadena = "";
            if (Buscar == "")
            {
                if (bInsumosChk && bProductosChk)
                {
                    // Cadena SQL 
                    myCadena = "select * from Articulos_Insumos_Productos";
                }
                else
                {
                    if (bInsumosChk)
                    {
                        // Cadena SQL 
                        myCadena = "select * from Articulos_Insumos_Productos where Tabla = 'INSUMOS'";
                    }
                    if (bProductosChk)
                    {
                        // Cadena SQL 
                        myCadena = "select * from Articulos_Insumos_Productos where Tabla = 'PRODUCTOS'";
                    }
                }
            }
            else
            {
                if (bInsumosChk && bProductosChk)
                {
                    // Cadena SQL 
                    myCadena = "select * from Articulos_Insumos_Productos where " + Campo + " like '" + Buscar + "%' order by " + Campo;
                }
                else
                {
                    if (bInsumosChk)
                    {
                        // Cadena SQL 
                        myCadena = "select * from Articulos_Insumos_Productos where Tabla = 'INSUMOS' and " + Campo + " like '" + Buscar + "%' order by " + Campo;
                    }
                    if (bProductosChk)
                    {
                        // Cadena SQL 
                        myCadena = "select * from Articulos_Insumos_Productos where Tabla = 'PRODUCTOS' and " + Campo + " like '" + Buscar + "%' order by " + Campo;
                    }
                }
            }
            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            mDtTable = clsDataBD.GetSql(myCadena);
            // Evito que el dgv genere columnas automáticas
            dgvArticulos.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvArticulos.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvArticulos.Rows.Count;
            // Posiciono la grilla en la última fila
            //dgvLocalidades.CurrentCell = dgvLocalidades[1, Filas - 1];
            int filasMatrizInsumosSeleccionados = clsGlobales.InsumosSeleccionados.Length / 2;
            int filasMatrizProductosSeleccionados = clsGlobales.ProductosSeleccionados.Length / 2;
            // Recorro la grilla para marcar los articulos ya seleccionados
            foreach (DataGridViewRow row in dgvArticulos.Rows)
            {
                if (dgvArticulos.CurrentRow.Cells["Tabla"].Value.ToString() == "INSUMOS")
                {
                    // Verifico si el id del insumo ya está en el vector
                    for (int i = 0; i < filasMatrizInsumosSeleccionados; i++)
                    {
                        if (Convert.ToDouble(row.Cells["IdArticulo"].Value) == clsGlobales.InsumosSeleccionados[i,0])
                        {
                            row.Cells["chkElegido"].Value = true;
                            break;
                        }
                    }
                }
                if (dgvArticulos.CurrentRow.Cells["Tabla"].Value.ToString() == "PRODUCTOS")
                {
                    // Verifico si el id del producto ya está en el vector
                    for (int i = 0; i < filasMatrizProductosSeleccionados; i++)
                    {
                        if (Convert.ToDouble(row.Cells["IdArticulo"].Value) == clsGlobales.ProductosSeleccionados[i,0])
                        {
                            row.Cells["chkElegido"].Value = true;
                            break;
                        }
                    }
                }
                
            }

        }

        #endregion

        #region CargarToolTips

        private void CargarToolTips()
        {
            toolTip1.SetToolTip(this.btnAgregar, "Agregar Artículo");
            toolTip2.SetToolTip(this.btnQuitar, "Quitar Artículo");
            toolTip3.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip4.SetToolTip(this.btnSalir, "Salir");

        }

        #endregion

        private void frmArticulosBuscar_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            // Llamo al método cargar Proveedores para rellenar la grilla. G.
            CargarArticulos("", "");
            // Cargo los ToolTips
            CargarToolTips();
            // Deshabilito la reordenación de las columnas de la grilla
            foreach (DataGridViewColumn dgvCol in dgvArticulos.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //Titulo Ventana
            this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;
        }

        #endregion

        #region Eventos de los TextBox

        #region Eventos Enter de los TextBox del formulario

        private void txtCodigo_Enter(object sender, EventArgs e)
        {
            // Limpio los demás textbox
            txtArticulo.Text = "";
        }

        private void txtArticulo_Enter(object sender, EventArgs e)
        {
            // Limpio los demás textbox
            txtCodigo.Text = "";
        }

        #endregion

        #region Eventos TextChanged de los TextBox del formulario

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            // Cargo los Proveedores filtradas por la búsqueda
            CargarArticulos(txtCodigo.Text, "CodigoArticulo");
        }

        private void txtArticulo_TextChanged(object sender, EventArgs e)
        {
            // Cargo los Proveedores filtradas por la búsqueda
            CargarArticulos(txtArticulo.Text, "Articulo");
        }

        #endregion

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Creo una bandera para detectar si el artículo ya se cargó
            bool myBandera = false;
            // Variables que guardan los largos de los vectores
            int filasMatrizInsumosSeleccionados = clsGlobales.InsumosSeleccionados.Length / 2;
            int filasMatrizProductosSeleccionados = clsGlobales.ProductosSeleccionados.Length / 2;
            // Verifico si es insumo
            if (dgvArticulos.CurrentRow.Cells["Tabla"].Value.ToString() == "INSUMOS")
            {
                // Verifico si el vector ya tiene datos cargados
                if (filasMatrizInsumosSeleccionados > 0)
                {
                    // Recorro el vector para verificar que el datos no se duplique
                    for (int i = 0; i < filasMatrizInsumosSeleccionados; i++)
                    {
                        // SI el insumo ya fue seleccionado
                        if (clsGlobales.InsumosSeleccionados[i,0]== Convert.ToDouble(dgvArticulos.CurrentRow.Cells["IdArticulo"].Value))
                        {
                            // Informo que el insumo ya fue seleccionado
                            MessageBox.Show("El Articulo ya fué seleccionado", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Cambio el estado de la bandera
                            myBandera = true;
                        }
                    }

                    // Si el proveedor no está cargado
                    if (myBandera == false)
                    {
                        // Redimensiono el tamaño de la matriz de Insumos
                        clsGlobales.InsumosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.InsumosSeleccionados, new int[] { filasMatrizInsumosSeleccionados + 1, 2 });
                        // A la posición creada le asigno el Id seleccionado y la cantidad cargada
                        clsGlobales.InsumosSeleccionados[filasMatrizInsumosSeleccionados, 0] = Convert.ToDouble(dgvArticulos.CurrentRow.Cells["IdArticulo"].Value);
                    }

                }
                // Si no tiene todavía ningún ID
                else
                {
                    // Redimensiono el tamaño de la matriz
                    clsGlobales.InsumosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.InsumosSeleccionados, new int[] { 1, 2 });
                    // A la posición creada le asigno el Id seleccionado
                    clsGlobales.InsumosSeleccionados[filasMatrizInsumosSeleccionados, 0] = Convert.ToDouble(dgvArticulos.CurrentRow.Cells["IdArticulo"].Value);
                }
            }
            // Verifico si es producto
            if (dgvArticulos.CurrentRow.Cells["Tabla"].Value.ToString() == "PRODUCTOS")
            {
                // Verifico si el vector ya tiene datos cargados
                if (filasMatrizProductosSeleccionados > 0)
                {
                    // Recorro el vector para verificar que el datos no se duplique
                    for (int i = 0; i < filasMatrizProductosSeleccionados; i++)
                    {
                        // SI el proveedor ya fue seleccionado
                        if (clsGlobales.ProductosSeleccionados[i,0] == Convert.ToDouble(dgvArticulos.CurrentRow.Cells["IdArticulo"].Value))
                        {
                            // Informo que el proveedor ya fue seleccionado
                            MessageBox.Show("El Articulo ya fué seleccionado", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Cambio el estado de la bandera
                            myBandera = true;
                        }
                    }

                    // Si el proveedor no está cargado
                    if (myBandera == false)
                    {
                        // Redimensiono el tamaño de la matriz
                        clsGlobales.ProductosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.ProductosSeleccionados, new int[] { filasMatrizProductosSeleccionados + 1, 2 });
                        // A la posición creada le asigno el Id seleccionado
                        clsGlobales.ProductosSeleccionados[filasMatrizProductosSeleccionados, 0] = Convert.ToDouble(dgvArticulos.CurrentRow.Cells["IdArticulo"].Value);
                    }

                }
                // Si no tiene todavía ningún ID
                else
                {
                    // Redimensiono el tamaño de la matriz
                    clsGlobales.ProductosSeleccionados = (double[,])clsValida.ResizeMatriz(clsGlobales.ProductosSeleccionados, new int[] { 1, 2 });
                    // A la posición creada le asigno el Id seleccionado
                    clsGlobales.ProductosSeleccionados[filasMatrizProductosSeleccionados, 0] = Convert.ToDouble(dgvArticulos.CurrentRow.Cells["IdArticulo"].Value);
                }
            }

            // Marco el check
            dgvArticulos.CurrentRow.Cells["chkElegido"].Value = true;
            // Inhabilito el botón agregar y habilito el quitar
            btnAgregar.Enabled = false;
            btnQuitar.Enabled = true;
            
        }

        #endregion

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            // Variables que guardan los largos de los vectores
            int filasMatrizInsumosSeleccionados = clsGlobales.InsumosSeleccionados.Length / 2;
            int filasMatrizProductosSeleccionados = clsGlobales.ProductosSeleccionados.Length / 2;
            
            // Desmarco el check
            dgvArticulos.CurrentRow.Cells["chkElegido"].Value = false;
            // Verifico si es insumo
            if (dgvArticulos.CurrentRow.Cells["Tabla"].Value.ToString() == "INSUMOS")
            {
                // Elimino el del vector
                for (int i = 0; i < filasMatrizInsumosSeleccionados; i++)
                {
                    // Si el proveedor quitado es el del vector
                    if (clsGlobales.InsumosSeleccionados[i,0] == Convert.ToDouble(dgvArticulos.CurrentRow.Cells["IdArticulo"].Value))
                    {
                        // Pongo su valor a 0 para que al pasar los datos al otro formulario no lo cargue a su grilla de proveedores
                        clsGlobales.InsumosSeleccionados[i,0] = 0;
                    }
                }
            }
            // Verifico si es Producto
            if (dgvArticulos.CurrentRow.Cells["Tabla"].Value.ToString() == "PRODUCTOS")
            {
                // Elimino el del vector
                for (int i = 0; i < filasMatrizProductosSeleccionados; i++)
                {
                    // Si el proveedor quitado es el del vector
                    if (clsGlobales.ProductosSeleccionados[i,0] == Convert.ToDouble(dgvArticulos.CurrentRow.Cells["IdArticulo"].Value))
                    {
                        // Pongo su valor a 0 para que al pasar los datos al otro formulario no lo cargue a su grilla de proveedores
                        clsGlobales.ProductosSeleccionados[i,0] = 0;
                    }
                }
            }
            
            // Inhabilito el botón agregar y habilito el quitar
            btnAgregar.Enabled = true;
            btnQuitar.Enabled = false;
            
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            // SI el articulo esta o no seleccionado cambio el enabled de los botones
            if (Convert.ToBoolean(dgvArticulos.CurrentRow.Cells["chkElegido"].Value) == true)
            {
                btnAgregar.Enabled = false;
                btnQuitar.Enabled = true;
            }
            else
            {
                btnQuitar.Enabled = false;
                btnAgregar.Enabled = true;
            }
        }

    }
}

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
    public partial class frmProveedoresBuscar : Form
    {
        // Declaro las variables que me permiten filtrar por el tipo de proveedor
        bool bInsumosChk;
        bool bProductosChk;
        
        public frmProveedoresBuscar(bool ins, bool Prod)
        {
            InitializeComponent();
            // Asigno los parámetros a las variables
            bInsumosChk = ins;
            bProductosChk = Prod;
           
        }

        #region Métodos del Formulario

        #region Método para cargar la grilla

        private void CargarProveedores(string Buscar, string Campo)
        {
            // Variable para la cadena SQL
            string myCadena = "";
            if (Buscar == "")
            {
                if (bInsumosChk && bProductosChk)
                {
                    // Cadena SQL 
                    myCadena = "select * from Vista_Proveedores ";
                }
                else
                {
                    if (bInsumosChk)
                    {
                        // Cadena SQL 
                        myCadena = "select * from Vista_Proveedores where Insumos = 1";
                    }
                    if (bProductosChk)
                    {
                        // Cadena SQL 
                        myCadena = "select * from Vista_Proveedores where Productos = 1";
                    }
                }
            }
            else
            {
                if (bInsumosChk && bProductosChk)
                {
                    // Cadena SQL 
                    myCadena = "select * from Vista_Proveedores where " + Campo + " like '" + Buscar + "%' order by " + Campo;
                }
                else
                {
                    if (bInsumosChk)
                    {
                        // Cadena SQL 
                        myCadena = "select * from Vista_Proveedores where Insumos = 1 and " + Campo + " like '" + Buscar + "%' order by " + Campo;
                    }
                    if (bProductosChk)
                    {
                        // Cadena SQL 
                        myCadena = "select * from Vista_Proveedores where Productos = 1 and " + Campo + " like '" + Buscar + "%' order by " + Campo;
                    }
                }
            }
            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            mDtTable = clsDataBD.GetSql(myCadena);
            // Evito que el dgv genere columnas automáticas
            dgvProveedores.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvProveedores.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvProveedores.Rows.Count;
            // Posiciono la grilla en la última fila
            //dgvLocalidades.CurrentCell = dgvLocalidades[1, Filas - 1];

            // Recorro la grilla para marcar los proveedores ya seleccionados
            foreach (DataGridViewRow row in dgvProveedores.Rows)
            {
                // Verifico si el id del proveedor ya está en el vector
                for (int i = 0; i < clsGlobales.ProveedoresSeleccionados.Length; i++)
                {
                    if (Convert.ToInt32(row.Cells["IdProveedor"].Value) == clsGlobales.ProveedoresSeleccionados[i])
                    {
                        row.Cells["chkElegido"].Value = true;
                    }
                }
            }

        }

        #endregion

        #region CargarToolTips

        private void CargarToolTips()
        {
            toolTip1.SetToolTip(this.btnAgregar, "Agregar Proveedor");
            toolTip2.SetToolTip(this.btnQuitar, "Quitar Proveedor");
            toolTip3.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip4.SetToolTip(this.btnSalir, "Salir");
            toolTip5.SetToolTip(this.btnSalir, "Abrir Proveedores");            
        }

        #endregion

        #endregion

        #region Eventos del Formulario

        #region Evento Load

        private void frmProveedoresBuscar_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            // Llamo al método cargar Proveedores para rellenar la grilla. G.
            CargarProveedores("", "");
            // Cargo los ToolTips
            CargarToolTips();
            // Deshabilito la reordenación de las columnas de la grilla
            foreach (DataGridViewColumn dgvCol in dgvProveedores.Columns)
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
            txtRazon.Text = "";
            txtFantasia.Text = "";
            txtCuit.Text = "";
        }

        private void txtRazon_Enter(object sender, EventArgs e)
        {
            // Limpio los demás textbox
            txtCodigo.Text = "";
            txtFantasia.Text = "";
            txtCuit.Text = "";
        }

        private void txtFantasia_Enter(object sender, EventArgs e)
        {
            // Limpio los demás textbox
            txtCodigo.Text = "";
            txtRazon.Text = "";
            txtCuit.Text = "";
        }

        private void txtCuit_Enter(object sender, EventArgs e)
        {
            // Limpio los demás textbox
            txtCodigo.Text = "";
            txtRazon.Text = "";
            txtFantasia.Text = "";
        }

        #endregion

        #region Eventos TextChanged de los TextBox del formulario

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            // Cargo los Proveedores filtradas por la búsqueda
            CargarProveedores(txtCodigo.Text, "IdProveedor");
        }

        private void txtRazon_TextChanged(object sender, EventArgs e)
        {
            // Cargo los Proveedores filtradas por la búsqueda
            CargarProveedores(txtRazon.Text, "RazonSocial");
        }

        private void txtFantasia_TextChanged(object sender, EventArgs e)
        {
            // Cargo los Proveedores filtradas por la búsqueda
            CargarProveedores(txtFantasia.Text, "NombreFantasia");
        }

        private void txtCuit_TextChanged(object sender, EventArgs e)
        {
            // Cargo los Proveedores filtradas por la búsqueda
            CargarProveedores(txtCuit.Text, "Cuit");
        }

        #endregion

        #endregion

        #region Eventos de los Botones

        #region Evento Click del botón Salir

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Vacío el vector para nuevos usos
            clsGlobales.ProveedoresSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ProveedoresSeleccionados, 0);
            // Cierro el formulario
            this.Close();
            
        }

        #endregion

        #region Evento Click del botón Agregar

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Creo una bandera para detectar si el proveedor ya se cargó
            bool myBandera = false;
            // Verifico si el vector ya tiene datos cargados
            if (clsGlobales.ProveedoresSeleccionados.Length > 0)
            {
                // Recorro el vector para verificar que el datos no se duplique
                for (int i = 0; i < clsGlobales.ProveedoresSeleccionados.Length; i++)
                {
                    // SI el proveedor ya fue seleccionado
                    if (clsGlobales.ProveedoresSeleccionados[i] == Convert.ToInt32(dgvProveedores.CurrentRow.Cells["IdProveedor"].Value))
                    {
                        // Informo que el proveedor ya fue seleccionado
                        MessageBox.Show("El proveedor ya fué seleccionado", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Cambio el estado de la bandera
                        myBandera = true;
                    }
                }

                // Si el proveedor no está cargado
                if (myBandera == false)
                {
                    // Redimensiono el tamaño de la matriz
                    clsGlobales.ProveedoresSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ProveedoresSeleccionados, clsGlobales.ProveedoresSeleccionados.Length + 1);
                    // A la posición creada le asigno el Id seleccionado
                    clsGlobales.ProveedoresSeleccionados[clsGlobales.ProveedoresSeleccionados.Length - 1] = Convert.ToInt32(dgvProveedores.CurrentRow.Cells["IdProveedor"].Value);
                }
               
            }
            // Si no tiene todavía ningún ID
            else
            {
                // Redimensiono el tamaño de la matriz
                clsGlobales.ProveedoresSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ProveedoresSeleccionados, 1);
                // A la posición creada le asigno el Id seleccionado
                clsGlobales.ProveedoresSeleccionados[clsGlobales.ProveedoresSeleccionados.Length - 1] = Convert.ToInt32(dgvProveedores.CurrentRow.Cells["IdProveedor"].Value);
            }

            // Marco el check
            dgvProveedores.CurrentRow.Cells["chkElegido"].Value = true;
            // Cambio el enabled de los botones
            btnAgregar.Enabled = false;
            btnQuitar.Enabled = true;
        }

        #endregion

        #region Evento Click del botón Quitar

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            // Desmarco el check
            dgvProveedores.CurrentRow.Cells["chkElegido"].Value = false;
            // Elimino el proveedor del vector
            for (int i = 0; i < clsGlobales.ProveedoresSeleccionados.Length; i++)
            {
                // Si el proveedor quitado es el del vector
                if (clsGlobales.ProveedoresSeleccionados[i] == Convert.ToInt32(dgvProveedores.CurrentRow.Cells["IdProveedor"].Value))
                {
                    // Pongo su valor a 0 para que al pasar los datos al otro formulario no lo cargue a su grilla de proveedores
                    clsGlobales.ProveedoresSeleccionados[i] = 0;
                }
            }
            // Cambio el enabled de los botones
            btnAgregar.Enabled = true;
            btnQuitar.Enabled = false;
        }

        #endregion

        #region Evento Click del botón Aceptar

        private void btnAceptar_Click(object sender, EventArgs e)
        {
         // Cierro el formulario con los datos de los proveedores en el vector
            this.Close();
        }

        #endregion

        #endregion

        #region Evento SelectionChanged de la grilla

        private void dgvProveedores_SelectionChanged(object sender, EventArgs e)
        {
            // Controlo el estado de los botones según si el proveedor está elegido o no
            // Elegido
            if (Convert.ToBoolean(dgvProveedores.CurrentRow.Cells["chkElegido"].Value) == true)
            {
                btnAgregar.Enabled = false;
                btnQuitar.Enabled = true;
            }
            // No elegido
            else
            {
                btnAgregar.Enabled = true;
                btnQuitar.Enabled = false;
            }
        }

        #endregion

        private void btnClientes_Click(object sender, EventArgs e)
        {
            frmProveedores myProv = new frmProveedores();
            myProv.ShowDialog();
        }

        #endregion

    }
}

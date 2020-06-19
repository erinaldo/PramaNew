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
    public partial class frmBuscarCliente : Form
    {
        public frmBuscarCliente()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

            // Vacío el vector para nuevos usos
            clsGlobales.ClientesSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ClientesSeleccionados, 0);
            // Cierro el formulario
            this.Close();
        }

        private void frmBuscarCliente_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            // Llamo al método cargar Proveedores para rellenar la grilla. G.
            CargarClientes("","");
            // Cargo los ToolTips
            CargarToolTips();
            // Deshabilito la reordenación de las columnas de la grilla
            foreach (DataGridViewColumn dgvCol in dgvCli.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //Titulo Ventana
            this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;
        }

        #region CargarToolTips

        private void CargarToolTips()
        {
            toolTip1.SetToolTip(this.btnAgregar, "Agregar Cliente");
            toolTip2.SetToolTip(this.btnQuitar, "Quitar Cliente");
            toolTip3.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip4.SetToolTip(this.btnClientes, "Clientes");
            toolTip5.SetToolTip(this.btnAddCli, "Añadir Cliente!");
        }

        #endregion

        #region Método para cargar la grilla

        private void CargarClientes(string Buscar, string Campo)
        {
            string myCadena = "";
            if (Buscar == "")
            {
                //Cadena SQL 
                myCadena = "select * from Vista_Clientes Order by RazonSocial";
            }
            else
            {
                    if (Campo == "Codigo")
                    {
                        // Cadena SQL 
                        myCadena = "select * from Vista_Clientes where Codigo = " + Buscar + " order by " + Campo;
                    }
                    if (Campo == "RazonSocial")
                    {
                        // Cadena SQL 
                        myCadena = "select * from Vista_Clientes where " + Campo + " like '" + Buscar + "%' order by " + Campo;
                    }
            }
            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            mDtTable = clsDataBD.GetSql(myCadena);
            // Evito que el dgv genere columnas automáticas
            dgvCli.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvCli.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvCli.Rows.Count;
            // Posiciono la grilla en la última fila
            //dgvLocalidades.CurrentCell = dgvLocalidades[1, Filas - 1];

            // Recorro la grilla para marcar los proveedores ya seleccionados
            foreach (DataGridViewRow row in dgvCli.Rows)
            {
                // Verifico si el id del proveedor ya está en el vector
                for (int i = 0; i < clsGlobales.ClientesSeleccionados.Length; i++)
                {
                    if (Convert.ToInt32(row.Cells["Codigo"].Value) == clsGlobales.ClientesSeleccionados[i])
                    {
                        row.Cells["chkElegido"].Value = true;
                    }
                }
            }

        }

        #endregion

        #region Eventos Botones

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Cierro el formulario con los datos de los proveedores en el vector
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Creo una bandera para detectar si el proveedor ya se cargó
            bool myBandera = false;
            // Verifico si el vector ya tiene datos cargados
            if (clsGlobales.ClientesSeleccionados.Length > 0)
            {
                // Recorro el vector para verificar que el datos no se duplique
                for (int i = 0; i < clsGlobales.ClientesSeleccionados.Length; i++)
                {
                    // SI el proveedor ya fue seleccionado
                    if (clsGlobales.ClientesSeleccionados[i] == Convert.ToInt32(dgvCli.CurrentRow.Cells["Codigo"].Value))
                    {
                        // Informo que el proveedor ya fue seleccionado
                        MessageBox.Show("El Cliente ya fué seleccionado", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Cambio el estado de la bandera
                        myBandera = true;
                    }
                }

                // Si el proveedor no está cargado
                if (myBandera == false)
                {
                    // Redimensiono el tamaño de la matriz
                    clsGlobales.ClientesSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ClientesSeleccionados, clsGlobales.ClientesSeleccionados.Length + 1);
                    // A la posición creada le asigno el Id seleccionado
                    clsGlobales.ClientesSeleccionados[clsGlobales.ClientesSeleccionados.Length - 1] = Convert.ToInt32(dgvCli.CurrentRow.Cells["Codigo"].Value);
                }

            }
            // Si no tiene todavía ningún ID
            else
            {
                // Redimensiono el tamaño de la matriz
                clsGlobales.ClientesSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ClientesSeleccionados, 1);
                // A la posición creada le asigno el Id seleccionado
                clsGlobales.ClientesSeleccionados[clsGlobales.ClientesSeleccionados.Length - 1] = Convert.ToInt32(dgvCli.CurrentRow.Cells["Codigo"].Value);
            }

            // Marco el check
            dgvCli.CurrentRow.Cells["chkElegido"].Value = true;
            // Cambio el enabled de los botones
            btnAgregar.Enabled = false;
            btnQuitar.Enabled = true;
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            // Desmarco el check
            dgvCli.CurrentRow.Cells["chkElegido"].Value = false;
            // Elimino el proveedor del vector
            for (int i = 0; i < clsGlobales.ClientesSeleccionados.Length; i++)
            {
                // Si el proveedor quitado es el del vector
                if (clsGlobales.ClientesSeleccionados[i] == Convert.ToInt32(dgvCli.CurrentRow.Cells["Codigo"].Value))
                {
                 //Pongo su valor a 0 para que al pasar los datos al otro formulario no lo cargue a su grilla de proveedores
                   clsGlobales.ClientesSeleccionados[i] = 0;
                 //Limpiar vector Cliente
                 //clsGlobales.ClientesSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ClientesSeleccionados, 0);
                }
            }
            // Cambio el enabled de los botones
            btnAgregar.Enabled = true;
            btnQuitar.Enabled = false;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
          
            if (!(txtCodigo.Text==""))
            {
                //Cargo los Proveedores filtradas por la búsqueda
                 CargarClientes(txtCodigo.Text, "Codigo");
            }

        }

        #endregion

        #region Evento TextChanged txtRazon

        private void txtRazon_TextChanged(object sender, EventArgs e)
        {
            if (!(txtRazon.Text == ""))
            {
                //Cargo los Proveedores filtradas por la búsqueda
                CargarClientes(txtRazon.Text, "RazonSocial");
            }
        }

        #endregion

        private void dgvCli_SelectionChanged(object sender, EventArgs e)
        {
            // Controlo el estado de los botones según si el proveedor está elegido o no
            // Elegido
            if (Convert.ToBoolean(dgvCli.CurrentRow.Cells["chkElegido"].Value) == true)
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

        private void btnClientes_Click(object sender, EventArgs e)
        {
            //Id
            int Id = Convert.ToInt32(dgvCli.CurrentRow.Cells["Codigo"].Value);

            //Clean
            txtRazon.Text = "";
            txtCodigo.Text = "";

            //Llamar a Clientes
            frmClientesABM myForm = new frmClientesABM();
            myForm.ShowDialog();
            
            //Refresh
            this.CargarClientes("", "");

            //Reposicionar By Id
            ReposicionarById(Id);

            //Foco
            txtRazon.Focus();

        }

        #region Reposicionar Grilal por Id

        //REPOSICIONA GRILLA POR ID
        private void ReposicionarById(int p_Id)
        {
            //recorrer
            foreach (DataGridViewRow myRow in this.dgvCli.Rows)
            {
                if (Convert.ToInt32(myRow.Cells["Codigo"].Value.ToString()) == p_Id)
                {
                    // Devuelvo el foco a la fila de la grilla desde donde se llamó
                    this.dgvCli.CurrentCell = dgvCli[1, myRow.Index];

                    // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                    EventArgs ea = new EventArgs();
                    this.dgvCli_SelectionChanged(this.dgvCli, ea);

                    //Salir
                    break;
                }
            }
        }

        #endregion

        private void btnAddCli_Click(object sender, EventArgs e)
        {
            //Id
            int Id = Convert.ToInt32(dgvCli.CurrentRow.Cells["Codigo"].Value);

            //Clean
            txtRazon.Text = "";
            txtCodigo.Text = "";

            //Llamar a Clientes
            frmClientesABM myForm = new frmClientesABM(0,false,true,false);
            myForm.ShowDialog();

            //Refresh
            this.CargarClientes("", "");

            //Reposicionar By Id
            ReposicionarById(Id);

            //Foco
            txtRazon.Focus();
        }
    }
}

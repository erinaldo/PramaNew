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
    public partial class frmComprasBusquedaComprobantes : Form
    {

        #region Variables del formulario

        // Variable que almacena el Id del proveedor a buscar
        int myProveedor = 0;
        // Variable que almacena la razón social del proveedor
        string myRazonSocial = "";
        // Variable que almacena el tipo de comprobante
        int myTipoComprobante = 0;
        // Variable que almacena la cadena SQL
        string myCadenaSQL = "";

        #endregion

        public frmComprasBusquedaComprobantes(int IdProveedor, string NomProv, int IdTipoComprobante)
        {
            // Paso a las variable los datos de los parámetros
            myProveedor = IdProveedor;
            myTipoComprobante = IdTipoComprobante;
            myRazonSocial = NomProv;

            InitializeComponent();

            this.Text = clsGlobales.cFormato.getTituloVentana() + " - BÚSQUEDA DE COMPROBANTES DEL PROVEEDOR : " + myRazonSocial;
        }

        #region Evento Load del Formulario

        private void frmComprasBusquedaComprobantes_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            // Llamo al método cargar Proveedores para rellenar la grilla. G.
            CargarComprobantes(myProveedor, myTipoComprobante);
            // Cargo los ToolTips
            CargarToolTips();
            // Deshabilito la reordenación de las columnas de la grilla
            foreach (DataGridViewColumn dgvCol in dgvComprobantes.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        #endregion

        #region Método que carga lo ToolTips

        private void CargarToolTips()
        {
            toolTip1.SetToolTip(this.btnAgregar, "Agregar Comrpbante");
            toolTip2.SetToolTip(this.btnQuitar, "Quitar Comrpbante");
            toolTip3.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip4.SetToolTip(this.btnSalir, "Salir");
        }

        #endregion

        #region Método que carga los comprobantes a la grilla

        private void CargarComprobantes(int IdProv, int IdTipo)
        {
            // Armo la cadena SQL
            myCadenaSQL = "select * from Vista_ComprobantesCompras where IdProveedor = " + IdProv + " and IdTipo = " + IdTipo;
            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            mDtTable = clsDataBD.GetSql(myCadenaSQL);
            // Evito que el dgv genere columnas automáticas
            dgvComprobantes.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvComprobantes.DataSource = mDtTable;
        }

        #endregion

        #region Evento Click del botón btnAgregar

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Creo una bandera para detectar si el proveedor ya se cargó
            bool myBandera = false;
            // Verifico si el vector ya tiene datos cargados
            if (clsGlobales.ComprobantesSeleccionados.Length > 0)
            {
                // Recorro el vector para verificar que el datos no se duplique
                for (int i = 0; i < clsGlobales.ComprobantesSeleccionados.Length; i++)
                {
                    // SI el proveedor ya fue seleccionado
                    if (clsGlobales.ComprobantesSeleccionados[i] == Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdComprobante"].Value))
                    {
                        // Informo que el proveedor ya fue seleccionado
                        MessageBox.Show("El comprobante ya fué seleccionado", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Cambio el estado de la bandera
                        myBandera = true;
                    }
                }

                // Si el proveedor no está cargado
                if (myBandera == false)
                {
                    // Redimensiono el tamaño de la matriz
                    clsGlobales.ComprobantesSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ComprobantesSeleccionados, clsGlobales.ComprobantesSeleccionados.Length + 1);
                    // A la posición creada le asigno el Id seleccionado
                    clsGlobales.ComprobantesSeleccionados[clsGlobales.ComprobantesSeleccionados.Length - 1] = Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdComprobante"].Value);
                }

            }
            // Si no tiene todavía ningún ID
            else
            {
                // Redimensiono el tamaño de la matriz
                clsGlobales.ComprobantesSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ComprobantesSeleccionados, 1);
                // A la posición creada le asigno el Id seleccionado
                clsGlobales.ComprobantesSeleccionados[clsGlobales.ComprobantesSeleccionados.Length - 1] = Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdComprobante"].Value);
            }

            // Marco el check
            dgvComprobantes.CurrentRow.Cells["chkElegido"].Value = true;
            // Cambio el enabled de los botones
            btnAgregar.Enabled = false;
            btnQuitar.Enabled = true;
        }

        #endregion

        #region Evento Click del botón btnQuitar

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            // Desmarco el check
            dgvComprobantes.CurrentRow.Cells["chkElegido"].Value = false;
            // Elimino el proveedor del vector
            for (int i = 0; i < clsGlobales.ComprobantesSeleccionados.Length; i++)
            {
                // Si el proveedor quitado es el del vector
                if (clsGlobales.ComprobantesSeleccionados[i] == Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdComprobante"].Value))
                {
                    // Pongo su valor a 0 para que al pasar los datos al otro formulario no lo cargue a su grilla de proveedores
                    clsGlobales.ComprobantesSeleccionados[i] = 0;
                }
            }
            // Cambio el enabled de los botones
            btnAgregar.Enabled = true;
            btnQuitar.Enabled = false;
        }

        #endregion

        #region Evento Click del botón btnSalir

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Vacío el vector para nuevos usos
            clsGlobales.ComprobantesSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ComprobantesSeleccionados, 0);
            // Cierro el formulario
            this.Close();
        }

        #endregion

        #region Evento Click del botón btnAceptar

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Cierro el formulario con los datos de los proveedores en el vector
            this.Close();
        }

        #endregion

        #region Evento SelectionChanged de la grilla

        private void dgvComprobantes_SelectionChanged(object sender, EventArgs e)
        {
            // Controlo el estado de los botones según si el proveedor está elegido o no
            // Elegido
            if (Convert.ToBoolean(dgvComprobantes.CurrentRow.Cells["chkElegido"].Value) == true)
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
    }
}

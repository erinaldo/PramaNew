using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Prama.Clases;

namespace Prama.Formularios.Articulos
{
    public partial class frmArticulosRubros_SubRubros : Form
    {
        //Publica
        int IdRubro = 0;
        int IndexFila = 0;

        public frmArticulosRubros_SubRubros()
        {
            InitializeComponent();
        }

        private void frmArticulosRubros_SubRubros_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            //Cargar Grilla Rubros
            CargarGrillaRubros();
            //Changed
            EventArgs ea = new EventArgs();
            this.dgvRubros_SelectionChanged(this.dgvRubros, ea);
            // Cargar ToolTips
            CargarToolTips();
            //Titulo Ventana
            this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;
        }

        #region CargarToolTips

        private void CargarToolTips()
        {
            toolTip1.SetToolTip(this.btnAceptar, "Guardar Orden SubRubros");
            toolTip1.SetToolTip(this.btnAccept, "Guardar Orden Rubros");
            toolTip2.SetToolTip(this.btnSalir, "Salir");
            toolTip3.SetToolTip(this.btnUp, "Subir Rubro");
            toolTip4.SetToolTip(this.btnDown, "Bajar Rubro");
            toolTip5.SetToolTip(this.btnUpSR, "Subir SubRubro");
            toolTip6.SetToolTip(this.btnDownSR, "Bajar SubRubro");


        }

        #endregion

        #region Metodo CargarGrillaRubros

        private void CargarGrillaRubros()
        {
            string myCadena = "Select * from RubrosArticulos where Activo = 1 order by Orden";
            //DataTable
            DataTable mDtTable = new DataTable();
            mDtTable = clsDataBD.GetSql(myCadena);
            // Evito que el dgv genere columnas automáticas
            //dgvRubros.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            //dgvRubros.DataSource = mDtTable;


            // Evito que el dgv genere columnas automáticas
            dgvRubros.AutoGenerateColumns = false;

            //Contador
            int Item = 1;
            int Filas = 0;

            //Mostrar Datos
            foreach (DataRow fila in mDtTable.Rows)
            {
                /*Agregar Fila*/
                dgvRubros.Rows.Add();

                // Cuento las filas de la grilla
                Filas = dgvRubros.Rows.Count;

                // Si la grilla no está vacía
                if (Filas > 0)
                {
                    //Posiciono la grilla en la última fila
                    dgvRubros.CurrentCell = dgvRubros[1, Filas - 1];
                }

                //Cargar Datos a la fila                
                dgvRubros.CurrentRow.Cells["IdRubroArticulo"].Value = fila["IdRubroArticulo"].ToString();
                dgvRubros.CurrentRow.Cells["RubroArticulo"].Value = fila["RubroArticulo"].ToString();

                //Contador
                Item++;

            }
        }

        #endregion

        private void btnUp_Click(object sender, EventArgs e)
        {
            DataGridView dgv = this.dgvRubros;
            // Tomo la posición actual de la fila con foco
            
            try
            {
                int totalRows = dgv.Rows.Count;
                // Obtener el indiec de la fila para la celda seleccionada
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                IndexFila = rowIndex;
                if (rowIndex == 0)
                    return;
                // Tomar el indice de la columna de la celda seleccionada
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.Rows.Remove(selectedRow);
                dgv.Rows.Insert(rowIndex - 1, selectedRow);
                dgv.ClearSelection();

                dgv.Rows[rowIndex - 1].Selected = true;
                dgv.CurrentCell = dgv.Rows[rowIndex - 1].Cells[1];
                
                EventArgs ea = new EventArgs();
                this.dgvRubros_SelectionChanged(this.dgvRubros, ea);

            }
            catch { }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            DataGridView dgv = this.dgvRubros;
            try
            {
                int totalRows = dgv.Rows.Count;
                // Obtener el indiec de la fila para la celda seleccionada
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == totalRows - 1)
                    return;
                // Tomar el indice de la columna de la celda seleccionada
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.Rows.Remove(selectedRow);
                dgv.Rows.Insert(rowIndex + 1, selectedRow);
                dgv.ClearSelection();

                dgv.Rows[rowIndex + 1].Selected = true;
                dgv.CurrentCell = dgv.Rows[rowIndex + 1].Cells[1];
                
                EventArgs ea = new EventArgs();
                this.dgvRubros_SelectionChanged(this.dgvRubros, ea);

            }
            catch { }
        }

        private void dgvRubros_SelectionChanged(object sender, EventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvRubros.RowCount == 0)
            {
                return;
            }
            //Clean
            dgvSubRubro.Rows.Clear();
            // Evito que el dgvUsuarios genere columnas automáticas
            dgvSubRubro.AutoGenerateColumns = false;
            // Declaro una variable que va a guardar el Id del comprobante
            IdRubro = Convert.ToInt32(dgvRubros.CurrentRow.Cells["IdRubroArticulo"].Value);
            // Armo la cadena SQL para ejecutar el procedieminto almacenado que trae el detalle del comprobante
            string newMyCadenaSql = "select * from Vista_SubRubros_Rubros where IdRubroArticulo = " + IdRubro + " order by Orden";
            // Creo un datatable y le paso los datos de la consulta SQL
            DataTable myTabla = clsDataBD.GetSql(newMyCadenaSql);

            //Contador
            int Item = 1;
            int Filas = 0;

            //Mostrar Datos
            foreach (DataRow fila in myTabla.Rows)
            {
                /*Agregar Fila*/
                dgvSubRubro.Rows.Add();

                // Cuento las filas de la grilla
                Filas = dgvSubRubro.Rows.Count;

                // Si la grilla no está vacía
                if (Filas > 0)
                {
                    //Posiciono la grilla en la última fila
                    dgvSubRubro.CurrentCell = dgvSubRubro[2, Filas - 1];
                }

                //Cargar Datos a la fila                
                dgvSubRubro.CurrentRow.Cells["IdSubrubroArticulo"].Value = fila["IdSubrubroArticulo"].ToString();
                dgvSubRubro.CurrentRow.Cells["IdRA"].Value = fila["IdRubroArticulo"].ToString();
                dgvSubRubro.CurrentRow.Cells["SubrubroArticulo"].Value = fila["SubrubroArticulo"].ToString();

                //Contador
                Item++;

            }
        }

        private void btnUpSR_Click(object sender, EventArgs e)
        {
            DataGridView dgv = this.dgvSubRubro;
            try
            {
                int totalRows = dgv.Rows.Count;
                // Obtener el indiec de la fila para la celda seleccionada
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == 0)
                    return;
                // Tomar el indice de la columna de la celda seleccionada
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.Rows.Remove(selectedRow);
                dgv.Rows.Insert(rowIndex - 1, selectedRow);
                dgv.ClearSelection();
                dgv.Rows[rowIndex - 1].Selected = true;
                dgv.CurrentCell = dgv.Rows[rowIndex - 1].Cells[1];
                
            }
            catch { }
        }

        private void btnDownSR_Click(object sender, EventArgs e)
        {
            DataGridView dgv = this.dgvSubRubro;
            try
            {
                int totalRows = dgv.Rows.Count;
                // Obtener el indiec de la fila para la celda seleccionada
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == totalRows - 1)
                    return;
                // Tomar el indice de la columna de la celda seleccionada
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.Rows.Remove(selectedRow);
                dgv.Rows.Insert(rowIndex + 1, selectedRow);
                dgv.ClearSelection();
                dgv.Rows[rowIndex + 1].Selected = true;
                dgv.CurrentCell = dgv.Rows[rowIndex + 1].Cells[1];
            }
            catch { }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            //Variable que contempla el orden
            int Item = 1;

            //Recorrer Rubros y guardar Orden
            foreach (DataGridViewRow row in dgvRubros.Rows)
            {
                //Cargar el producto en la tabla
                //Guardar la factura
                string myCadSQL = "UPDATE RubrosArticulos SET Orden = " + Item + " WHERE IdRubroArticulo = " + Convert.ToInt32(row.Cells["IdRubroArticulo"].Value); 

                clsDataBD.GetSql(myCadSQL);

                Item++;

            }

            MessageBoxTemporal.Show("Los cambios se han guardado correctamente!", "Información!", 3, false);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Variable que contempla el orden
            int Item = 1;

            //Recorrer Rubros y guardar Orden
            foreach (DataGridViewRow row in dgvSubRubro.Rows)
            {
                //Cargar el producto en la tabla
                //Guardar la factura
                string myCadSQL = "UPDATE SubrubrosArticulos SET Orden = " + Item + " WHERE IdSubrubroArticulo = " + Convert.ToInt32(row.Cells["IdSubrubroArticulo"].Value);

                clsDataBD.GetSql(myCadSQL);

                Item++;

            }

            //Mensaje final
            MessageBoxTemporal.Show("Los cambios se han guardado correctamente!", "Información!", 3, false);
        }



    }
}

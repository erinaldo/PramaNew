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
    public partial class frmComprasOP : Form
    {
        #region Variables

        bool bSearch = false;
        bool yaCargado = false;
        int indexFila = 0;
        string myEstado = "C";

        #endregion
        
        public frmComprasOP()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

        }

        private void frmComprasOP_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            //Titulo
            this.Text = clsGlobales.cParametro.NombreFantasia + this.Text;

            //ToolTips
            CargarToolsTip();

            //Cargar Grilla Facturas
            CargarGrillaComprobantes("", "");

            //Deshabilitar Oden
            this.DeshabilitarOrdenGrillas();

            //Posicionamiento
            int filas = dgvOPs.Rows.Count;
            //Count
            if (filas > 0)
            {
                // Actualizo el valor de la fila para que sea la última de la grilla
                this.indexFila = filas - 1;
                // Pongo el foco de la fila
                PosicionarFocoFila();
            }
        }

        #region Método que carga los ToolsTip del formulario

        private void CargarToolsTip()
        {
            toolTip1.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip2.SetToolTip(this.btnCancelar, "Cancelar");
            toolTip3.SetToolTip(this.btnSalir, "Salir");
            toolTip4.SetToolTip(this.btnBuscar, "Buscar");
            toolTip5.SetToolTip(this.btnImprimir, "Imprimir");
        }

        #endregion

        #region Metodo: CargarGrillaComprobantes

        private void CargarGrillaComprobantes(string Buscar = "", string Campo = "")
        {
            string myCadena = "";

            dgvOPs.DataSource = null;

            

            if (clsGlobales.ConB == null)
            {
                if (!(Buscar == ""))
                {
                    myCadena = "select * from Vista_OrdenesPago where " + Campo + " like '" + Buscar + "%' order by IdOrdenPago";
                    //.T.
                    bSearch = true;
                }
                else
                {
                    myCadena = "select * from Vista_OrdenesPago order by IdOrdenPago";
                    //.F.
                    bSearch = false;
                }
            }
            else
            {
                if (!(Buscar == ""))
                {
                    myCadena = "select * from Vista_OrdenesPago2 where " + Campo + " like '" + Buscar + "%' order by IdOrdenPago";
                    //.T.
                    bSearch = true;
                }
                else
                {
                    myCadena = "select * from Vista_OrdenesPago2 order by IdOrdenPago";
                    //.F.
                    bSearch = false;
                }
            }


            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            if (clsGlobales.ConB == null)
            {
                mDtTable = clsDataBD.GetSql(myCadena);
            }
            else
            {
                mDtTable = clsDataBD.GetSqlB(myCadena);
            }
            // Evito que el dgv genere columnas automáticas
            dgvOPs.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvOPs.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvOPs.Rows.Count;
            // Posiciono la grilla en la última fila, si hay
            if (Filas > 0)
            {
                //Cambio el estado de la bandera para que ejecute el selection changed de la grilla
                yaCargado = true;

                //Posicionar Grilla       
                int r = dgvOPs.CurrentCell.RowIndex;
                int c = dgvOPs.CurrentCell.ColumnIndex;
                dgvOPs.CurrentCell = dgvOPs.Rows[r].Cells[c];

                //Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                EventArgs ea = new EventArgs();
                this.dgvOPs_SelectionChanged(this.dgvOPs, ea);

                if (!(this.myEstado == "B"))
                {
                    this.btnBuscar.Visible = true;
                    this.btnImprimir.Visible = true;
                }
            }
            else
            {
                this.dgvDetalle.DataSource = null;

                this.btnBuscar.Visible = false;
                this.btnImprimir.Visible = false;
            }
        }

        #endregion

        #region Método que deshabilita el reordenamiento de las grilla desde sus cabeceras

        private void DeshabilitarOrdenGrillas()
        {
            // Deshabilito la reordenación de las columnas de las grillas
            foreach (DataGridViewColumn dgvCol in this.dgvOPs.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;

            }
        }

        #endregion

        #region Método que devuelve el foco a la fila desde donde se presionó algún botón

        private void PosicionarFocoFila()
        {
            if (dgvOPs.Rows.Count != 0 && dgvOPs.Rows.Count > this.indexFila)
            {
                // Devuelvo el foco a la fila de la grilla desde donde se llamó
                if (this.indexFila == -1)
                {
                    this.indexFila = 0;
                }
                this.dgvOPs.CurrentCell = dgvOPs[1, this.indexFila];
                // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                EventArgs ea = new EventArgs();
                this.dgvOPs_SelectionChanged(this.dgvOPs, ea);
            }
        }

        #endregion

        private void dgvOPs_SelectionChanged(object sender, EventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvOPs.RowCount == 0)
            {
                return;
            }

            if (yaCargado)
            {
                // Vacío la grilla
                this.dgvDetalle.DataSource = null;
                // Evito que el dgvUsuarios genere columnas automáticas
                this.dgvDetalle.AutoGenerateColumns = false;
                // Declaro una variable que va a guardar el Id del comprobante
                int IdOrdenPago = Convert.ToInt32(this.dgvOPs.CurrentRow.Cells["IdOrdenPago"].Value);
                // Armo la cadena SQL para ejecutar el procedieminto almacenado que trae el detalle del comprobante
                string newMyCadenaSql = "Select * from Vista_OrdenPagoDetalle Where IdOrdenPago =  " + IdOrdenPago + " ORDER BY IdOrdenPago ASC";
                // Creo un datatable y le paso los datos de la consulta SQL
                DataTable myTabla = null;
                // Creo un datatable y le paso los datos de la consulta SQL
                if (!(clsGlobales.ConB == null))
                {
                    myTabla = clsDataBD.GetSqlB(newMyCadenaSql);
                }
                else
                {
                    myTabla = clsDataBD.GetSql(newMyCadenaSql);
                }

                // Asigno a la grilla el source
                this.dgvDetalle.DataSource = myTabla;
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //PREGUNTAR SI ESTA CONFIGURADO EN PARAMETROS
            if (clsGlobales.cParametro.Imprimir)
            {
                DialogResult dlResult = MessageBox.Show("¿Desea imprimir la Orden de Pago N° " + dgvOPs.CurrentRow.Cells["Numero"].Value.ToString() + " ?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Si confirma... cambiar estado
                if (dlResult == DialogResult.No)
                {
                    return;
                }
            }

            //Data Set
            dsReportes oDsOp = new dsReportes();

            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    


            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = dgvDetalle.Rows.Count;

            for (int i = 0; i < dgvFilas; i++)
            {
                oDsOp.Tables["dtRecibo"].Rows.Add
                (new object[] { dgvDetalle[3,i].Value.ToString(),
                    dgvDetalle[4,i].Value.ToString(),
                    dgvDetalle[6,i].Value.ToString()});
            }

            //Objeto Reporte
            rptReciboOp oRepOp = new rptReciboOp();

            //Cargar Reporte                                    
            oRepOp.Load(Application.StartupPath + "\\rptReciboOp.rpt");

            //Tipo Comprobante
            oRepOp.DataDefinition.FormulaFields["DescComp"].Text = "'" + "ORDEN DE PAGO" + "'";
            oRepOp.DataDefinition.FormulaFields["NroComp"].Text = "'" + dgvOPs.CurrentRow.Cells["Numero"].Value.ToString() + "'";
            oRepOp.DataDefinition.FormulaFields["Fecha"].Text = "'" + dgvOPs.CurrentRow.Cells["Fecha"].Value.ToString() + "'";

            oRepOp.DataDefinition.FormulaFields["CUIT"].Text = "'" + dgvOPs.CurrentRow.Cells["CUIT"].Value.ToString() + "'";
            oRepOp.DataDefinition.FormulaFields["RazonSocial"].Text = "'" + dgvOPs.CurrentRow.Cells["RazonSocial"].Value.ToString() + "'";

            oRepOp.DataDefinition.FormulaFields["TotalGral"].Text = "'" + dgvOPs.CurrentRow.Cells["Total"].Value.ToString() + "'";

            oRepOp.DataDefinition.FormulaFields["IdCliente"].Text = "'" + dgvOPs.CurrentRow.Cells["IdProveedor"].Value.ToString() + "'";

            oRepOp.DataDefinition.FormulaFields["Efvo"].Text = "'" + dgvOPs.CurrentRow.Cells["Efectivo"].Value.ToString() + "'";
            oRepOp.DataDefinition.FormulaFields["Transf"].Text = "'" + dgvOPs.CurrentRow.Cells["Bancos"].Value.ToString() + "'";
            oRepOp.DataDefinition.FormulaFields["CH_Prop"].Text = "'" + dgvOPs.CurrentRow.Cells["Cheques"].Value.ToString() + "'";
            oRepOp.DataDefinition.FormulaFields["SaldoFavor"].Text = "'" + dgvOPs.CurrentRow.Cells["SaldoAFavor"].Value.ToString() + "'";

            double total = Convert.ToDouble(dgvOPs.CurrentRow.Cells["Total"].Value.ToString());

            string sNumALetra = "Pesos " + clsNumalet.Convertir(Convert.ToDecimal(total), 2, " con ", "#0", true, false, false, true, true) + "/100";

            oRepOp.DataDefinition.FormulaFields["ImporteTexto"].Text = "'" + sNumALetra + "'";

            //Establecer el DataSet como DataSource
            oRepOp.SetDataSource(oDsOp);

            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepOp;

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports(2);
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog(); 
        }
    }
}

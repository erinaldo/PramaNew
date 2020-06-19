using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prama.Formularios.Ventas
{
    public partial class frmRecibos : Form
    {
        #region Variables

        bool bSearch = false;
        bool yaCargado = false;
        int indexFila = 0;
        string myEstado = "C";

        #endregion

        public frmRecibos()
        {
            InitializeComponent();
        }

        private void frmRecibos_Load(object sender, EventArgs e)
        {
            //Titulo
            this.Text = clsGlobales.cParametro.NombreFantasia + this.Text;

            //ToolTips
            CargarToolsTip();

            //Cargar Grilla Facturas
            CargarGrillaComprobantes("", "");

            //Deshabilitar Oden
            this.DeshabilitarOrdenGrillas();

            //Posicionamiento
            int filas = dgvRecibos.Rows.Count;
            //Count
            if (filas > 0)
            {
              // Actualizo el valor de la fila para que sea la última de la grilla
              this.indexFila = filas - 1;
              // Pongo el foco de la fila
              PosicionarFocoFila();
            }
        }

        #region Método que deshabilita el reordenamiento de las grilla desde sus cabeceras

        private void DeshabilitarOrdenGrillas()
        {
            // Deshabilito la reordenación de las columnas de las grillas
            foreach (DataGridViewColumn dgvCol in this.dgvRecibos.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;

            }
        }

        #endregion

        #region Método que carga los ToolsTip del formulario

        private void CargarToolsTip()
        {
            toolTip1.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip2.SetToolTip(this.btnCancelar, "Cancelar");
            toolTip3.SetToolTip(this.btnSalir, "Salir");
            toolTip7.SetToolTip(this.btnBuscar, "Buscar");
            toolTip8.SetToolTip(this.btnImprimir, "Imprimir");
        }

        #endregion


        #region Metodo: CargarGrillaComprobantes

        private void CargarGrillaComprobantes(string Buscar = "", string Campo = "")
        {
            string myCadena = "";

            dgvRecibos.DataSource = null;

            if (clsGlobales.ConB == null)
            {
                if (!(Buscar == ""))
                {
                    if (Campo == "RazonSocial")
                    {
                        myCadena = "select * from Vista_eRecibo WHERE " + Campo + " like '" + Buscar + "%' ORDER BY Fecha, PuntoNro ASC";
                    }
                    else
                    {
                        myCadena = "select * from Vista_eRecibo WHERE " + Campo + " = " + Buscar + " ORDER BY Fecha, PuntoNro ASC";
                    }
                    //.T.
                    bSearch = true;
                }
                else
                {
                    myCadena = "select * from Vista_eRecibo order by Fecha, PuntoNro ASC";
                    //.F.
                    bSearch = false;
                }
            }
            else
            {
                if (!(Buscar == ""))
                {
                    if (Campo == "RazonSocial")
                    {
                        myCadena = "select * from Vista_eRecibo_2 WHERE " + Campo + " like '" + Buscar + "%' ORDER BY Fecha ASC";
                    }
                    else
                    {
                        myCadena = "select * from Vista_eRecibo_2 WHERE " + Campo + " = " + Buscar + " ORDER BY Fecha ASC";
                    }
                    //.T.
                    bSearch = true;
                }
                else
                {
                    myCadena = "select * from Vista_eRecibo_2 order by Fecha ASC";
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
            dgvRecibos.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvRecibos.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvRecibos.Rows.Count;
            // Posiciono la grilla en la última fila, si hay
            if (Filas > 0)
            {
                //Cambio el estado de la bandera para que ejecute el selection changed de la grilla
                yaCargado = true;

                //Posicionar Grilla       
                int r = dgvRecibos.CurrentCell.RowIndex;
                int c = dgvRecibos.CurrentCell.ColumnIndex;
                dgvRecibos.CurrentCell = dgvRecibos.Rows[r].Cells[c];

                //Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                EventArgs ea = new EventArgs();
                this.dgvRecibos_SelectionChanged(this.dgvRecibos, ea);

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

        private void dgvRecibos_SelectionChanged(object sender, EventArgs e)
        {
            //VACIO EL DATAGRIDVIEW? VOLVER. N.
            if (dgvRecibos.RowCount == 0)
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
                int IdRecibo = Convert.ToInt32(this.dgvRecibos.CurrentRow.Cells["IdRecibo"].Value);
                // Armo la cadena SQL para ejecutar el procedieminto almacenado que trae el detalle del comprobante
                string newMyCadenaSql = "Select * from Vista_eReciboDetalle Where IdRecibo =  " + IdRecibo + " ORDER BY Fecha ASC";
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Tomo la posición actual de la fila con foco
            if (!(dgvRecibos.Rows.Count == 0))
            {
                this.indexFila = dgvRecibos.CurrentRow.Index;
            }

            //Preparo todo para habilitar la busqueda
            this.myEstado = "B";
            //Set
            this.gpbBusquedas.Visible = true;
            this.btnAceptar.Visible = true;
            this.btnCancelar.Visible = true;
            this.btnBuscar.Visible = false;
            this.btnImprimir.Visible = false;
            this.btnSalir.Visible = false;
            this.gpbDetalle.Size = new System.Drawing.Size(708, 182);
            this.dgvDetalle.Size = new System.Drawing.Size(692, 131);
            //Clean
            this.txtBuscarCLI.Text = "";
            this.txtBuscarIdCli.Text = "";
            //Pos
            txtBuscarIdCli.Focus();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Tomar el Id
            int Id = Convert.ToInt32(dgvRecibos.CurrentRow.Cells["IdRecibo"].Value.ToString());
            // Lleno nuevamente la grilla
            this.CargarGrillaComprobantes("","");
            //Botones
            this.gpbBusquedas.Visible = false;
            this.btnAceptar.Visible = false;
            this.btnCancelar.Visible = false;
            this.btnBuscar.Visible = true;
            this.btnImprimir.Visible = true;
            this.btnSalir.Visible = true;
            this.gpbDetalle.Size = new System.Drawing.Size(708, 242);
            this.dgvDetalle.Size = new System.Drawing.Size(692, 195);
            //Id >0? Solo cuando busca reposiciona por ID
            if (!(Id == 0 && bSearch))
            {
                //Reposicionar
                ReposicionarById(Id);
                //Reset
                Id = 0;
            }
            else
            {
                //Foco
                PosicionarFocoFila();
            }
            //.F.
            bSearch = false;
            //Retornar
            return;
        }

        #region Reposicionar Grilla por Id

        //REPOSICIONA GRILLA POR ID
        private void ReposicionarById(int p_Id)
        {
            //recorrer
            foreach (DataGridViewRow myRow in this.dgvRecibos.Rows)
            {
                if (Convert.ToInt32(myRow.Cells["IdRecibo"].Value.ToString()) == p_Id)
                {
                    // Devuelvo el foco a la fila de la grilla desde donde se llamó
                    this.dgvRecibos.CurrentCell = dgvRecibos[1, myRow.Index];

                    // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                    EventArgs ea = new EventArgs();
                    this.dgvRecibos_SelectionChanged(this.dgvRecibos, ea);

                    //Salir
                    break;
                }
            }
        }

        #endregion

        #region Método que devuelve el foco a la fila desde donde se presionó algún botón

        private void PosicionarFocoFila()
        {
            if (dgvRecibos.Rows.Count != 0 && dgvRecibos.Rows.Count > this.indexFila)
            {
                // Devuelvo el foco a la fila de la grilla desde donde se llamó
                if (this.indexFila == -1)
                {
                    this.indexFila = 0;
                }
                this.dgvRecibos.CurrentCell = dgvRecibos[1, this.indexFila];
                // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                EventArgs ea = new EventArgs();
                this.dgvRecibos_SelectionChanged(this.dgvRecibos, ea);
            }
        }

        #endregion

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Recargar
            if (bSearch)
            {
                //Cargo las localidades
                this.CargarGrillaComprobantes();
                //Botones
                this.gpbBusquedas.Visible = false;
                this.btnAceptar.Visible = false;
                this.btnCancelar.Visible = false; ;
                this.btnBuscar.Visible = true;
                this.btnImprimir.Visible = true;
                this.btnSalir.Visible = true;
                this.gpbDetalle.Size = new System.Drawing.Size(708, 242);
                this.dgvDetalle.Size = new System.Drawing.Size(692, 195);
                //Foco
                PosicionarFocoFila();
            }
            else
            {
                //Botones
                this.gpbBusquedas.Visible = false;
                this.btnAceptar.Visible = false;
                this.btnCancelar.Visible = false;
                this.btnBuscar.Visible = true;
                this.btnImprimir.Visible = true;
                this.btnSalir.Visible = true;
                this.gpbDetalle.Size = new System.Drawing.Size(708, 242);
                this.dgvDetalle.Size = new System.Drawing.Size(692, 195);
                //Foco
                PosicionarFocoFila();
            }

            //.F.
            bSearch = false;
        }

        private void txtBuscarCLI_TextChanged(object sender, EventArgs e)
        {
            if (!(txtBuscarCLI.Text == ""))
            {
                this.CargarGrillaComprobantes(this.txtBuscarCLI.Text, "RazonSocial");
            }
            else
            {
               //Cargo las localidades
                this.CargarGrillaComprobantes();
                //Posicionar Foco Grilla
                this.PosicionarFocoFila();
            }

        }
        private void txtBuscarIdCli_TextChanged(object sender, EventArgs e)
        {
            if (!(txtBuscarIdCli.Text == ""))
            {
                this.CargarGrillaComprobantes(this.txtBuscarIdCli.Text, "IdCliente");
            }
            else            
            {
                //Cargo las localidades
                this.CargarGrillaComprobantes();
                //Posicionar Foco Grilla
                this.PosicionarFocoFila();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //PREGUNTAR SI ESTA CONFIGURADO EN PARAMETROS
            if (clsGlobales.cParametro.Imprimir)
            {
                DialogResult dlResult = MessageBox.Show("¿Desea imprimir el Recibo N° " + dgvRecibos.CurrentRow.Cells["Recibo"].Value.ToString() + " ?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Si confirma... cambiar estado
                if (dlResult == DialogResult.No)
                {
                    return;
                }
            }

            //Data Set
            dsReportes oDsRecibo = new dsReportes();

            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    


            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = dgvDetalle.Rows.Count;

            for (int i = 0; i < dgvFilas; i++)
            {
                oDsRecibo.Tables["dtRecibo"].Rows.Add
                (new object[] { dgvDetalle[0,i].Value.ToString(),
                    dgvDetalle[2,i].Value.ToString(),
                    dgvDetalle[3,i].Value.ToString()});
            }

            //Objeto Reporte
            rptRecibo oRepRecibo = new rptRecibo();

            //Cargar Reporte                                    
            oRepRecibo.Load(Application.StartupPath + "\\rptRecibo.rpt");

            //Tipo Comprobante
            oRepRecibo.DataDefinition.FormulaFields["DescComp"].Text = "'" + "RECIBO" + "'";
            oRepRecibo.DataDefinition.FormulaFields["NroComp"].Text = "'" + dgvRecibos.CurrentRow.Cells["Recibo"].Value.ToString() + "'";
            oRepRecibo.DataDefinition.FormulaFields["Fecha"].Text = "'" + dgvRecibos.CurrentRow.Cells["Fecha"].Value.ToString() + "'";

            oRepRecibo.DataDefinition.FormulaFields["CUIT"].Text = "'" + dgvRecibos.CurrentRow.Cells["CUIT"].Value.ToString() + "'";
            oRepRecibo.DataDefinition.FormulaFields["RazonSocial"].Text = "'" + dgvRecibos.CurrentRow.Cells["Cliente"].Value.ToString() + "'";

            oRepRecibo.DataDefinition.FormulaFields["Domic"].Text = "'" + dgvRecibos.CurrentRow.Cells["Direccion"].Value.ToString() + ", " + dgvRecibos.CurrentRow.Cells["Localidad"].Value.ToString() + " (" + dgvRecibos.CurrentRow.Cells["CP"].Value.ToString() + ")" + " - " + dgvRecibos.CurrentRow.Cells["Provincia"].Value.ToString() + "'";

            oRepRecibo.DataDefinition.FormulaFields["IVA"].Text = "'" + dgvRecibos.CurrentRow.Cells["CondicionIva"].Value.ToString() + "'";

            oRepRecibo.DataDefinition.FormulaFields["TotalGral"].Text = "'" + dgvRecibos.CurrentRow.Cells["Total"].Value.ToString() + "'";

            oRepRecibo.DataDefinition.FormulaFields["IdCliente"].Text = "'" + dgvRecibos.CurrentRow.Cells["IdCliente"].Value.ToString() + "'";

            oRepRecibo.DataDefinition.FormulaFields["Efvo"].Text = "'" + dgvRecibos.CurrentRow.Cells["Efvo"].Value.ToString() + "'";
            oRepRecibo.DataDefinition.FormulaFields["Transf"].Text = "'" + dgvRecibos.CurrentRow.Cells["Transf"].Value.ToString() + "'";
            oRepRecibo.DataDefinition.FormulaFields["CH_Prop"].Text = "'" + dgvRecibos.CurrentRow.Cells["CH_Propio"].Value.ToString() + "'";
            oRepRecibo.DataDefinition.FormulaFields["CH_Terc"].Text = "'" + dgvRecibos.CurrentRow.Cells["CH_Tercero"].Value.ToString() + "'";
            oRepRecibo.DataDefinition.FormulaFields["MP"].Text = "'" + dgvRecibos.CurrentRow.Cells["MP"].Value.ToString() + "'";
            oRepRecibo.DataDefinition.FormulaFields["Otros"].Text = "'" + dgvRecibos.CurrentRow.Cells["Otros"].Value.ToString() + "'";
            oRepRecibo.DataDefinition.FormulaFields["Contrar"].Text = "'" + dgvRecibos.CurrentRow.Cells["Contrar"].Value.ToString() + "'";
            oRepRecibo.DataDefinition.FormulaFields["SaldoFavor"].Text = "'" + dgvRecibos.CurrentRow.Cells["SaldoFavor"].Value.ToString() + "'";

            double total = Convert.ToDouble(dgvRecibos.CurrentRow.Cells["Total"].Value.ToString());

            string sNumALetra = "Pesos " + clsNumalet.Convertir(Convert.ToDecimal(total), 2, " con ", "#0", true, false, false, true, true) + "/100";

            oRepRecibo.DataDefinition.FormulaFields["ImporteTexto"].Text = "'" + sNumALetra + "'";

            //Establecer el DataSet como DataSource
            oRepRecibo.SetDataSource(oDsRecibo);

            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepRecibo;

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports(2);
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog(); 
        }

        private void txtBuscarIdCli_Click(object sender, EventArgs e)
        {
            // Vacío los otros combos de busqueda
            txtBuscarIdCli.Text = "";
            txtBuscarCLI.Text = "";
        }

        private void txtBuscarCLI_Click(object sender, EventArgs e)
        {
            // Vacío los otros combos de busqueda
            txtBuscarIdCli.Text = "";
            txtBuscarCLI.Text = "";
        }

    }
}

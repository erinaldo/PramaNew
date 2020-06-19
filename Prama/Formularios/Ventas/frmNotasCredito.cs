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
    public partial class frmNotasCredito : Form
    {

        int indexFila = 0;
        int x_Con = 0;
        int x_Punto = 0;
        int IdFact = 0;
        bool bSearch = false;
        string bFiltro = "AND IdTipoComprobante in (3,8)";
        bool yaCargado = false;

        public frmNotasCredito(int p_Con, int p_Punto = 0)
        {
            InitializeComponent();
            //VERIFICAR
            x_Con = p_Con; //Conexion Tipo 0,1
            x_Punto = p_Punto;  //Punto Opcional por si hay que filtrar un punto determinado

        }

        private void frmNotasCredito_Load(object sender, EventArgs e)
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
        }

        #region Método que deshabilita el reordenamiento de las grilla desde sus cabeceras

        private void DeshabilitarOrdenGrillas()
        {
            // Deshabilito la reordenación de las columnas de las grillas
            foreach (DataGridViewColumn dgvCol in this.dgvComprobantes.Columns)
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
            string strTabla = "";

            dgvComprobantes.DataSource = null;

            //VERIFICAR VISTA A UTILIZAR SEGUN CONEXION
            if (x_Con == 0) { strTabla = "Vista_eFactura"; } else { strTabla = "Vista_eFactura_2"; }

            //VERIFICAR PARAMETROS
            if (!(Buscar == ""))
            {
                // Cadena SQL, ver si viene con el Punto de Venta establecido o no
                if (x_Punto == 0)
                {
                    myCadena = "select * from " + strTabla + " WHERE " + Campo + " like '" + Buscar + "%'" + bFiltro + " ORDER BY Fecha ASC";
                    //.T.
                    bSearch = true;
                }
                else
                {
                    myCadena = "select * from " + strTabla + " WHERE Punto = " + clsGlobales.cParametro.PtoVtaPorDefecto + " AND " + Campo + " like '" + Buscar + "%' " + bFiltro + " order by Fecha ASC";
                    //.T.
                    bSearch = false;
                }

            }
            else
            {
                // Cadena SQL, ver si viene con el Punto de Venta establecido o no
                if (x_Punto == 0)
                {
                    myCadena = "select * from " + strTabla + " WHERE Resultado = 1 " + bFiltro + " order by Fecha ASC";
                }
                else
                {
                    myCadena = "select * from " + strTabla + " WHERE Punto = " + clsGlobales.cParametro.PtoVtaPorDefecto + " " + bFiltro + " order by Fecha ASC";
                }

                //.F.
                bSearch = false;
            }
            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            if (x_Con == 0)
            {
                dgvComprobantes.Columns[17].Visible = false;
                mDtTable = clsDataBD.GetSql(myCadena);
            }
            else
            {
                dgvComprobantes.Columns["CAE"].Visible = false;
                dgvComprobantes.Columns[17].Visible = true;
                mDtTable = clsDataBD.GetSqlB(myCadena);
            }
            // Evito que el dgv genere columnas automáticas
            dgvComprobantes.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvComprobantes.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvComprobantes.Rows.Count;
            // Posiciono la grilla en la última fila, si hay
            if (Filas > 0)
            {
                //Cambio el estado de la bandera para que ejecute el selection changed de la grilla
                yaCargado = true;

                //Posicionar Grilla       
                int r = dgvComprobantes.CurrentCell.RowIndex;
                int c = dgvComprobantes.CurrentCell.ColumnIndex;
                dgvComprobantes.CurrentCell = dgvComprobantes.Rows[r].Cells[c];


                //Posicionamiento
                dgvComprobantes.CurrentCell = dgvComprobantes[3, Filas - 1];

                //Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                EventArgs ea = new EventArgs();
                this.dgvComprobantes_SelectionChanged(this.dgvComprobantes, ea);

                this.btnBuscar.Visible = true;
                this.btnImprimir.Visible = true;
            }
            else
            {
                dgvDet.DataSource = null;

                this.btnBuscar.Visible = false;
                this.btnImprimir.Visible = false;
            }
        }

        #endregion

        #region SelectionChanged Grid

        private void dgvComprobantes_SelectionChanged(object sender, EventArgs e)
        {
            if (yaCargado)
            {
                // Vacío la grilla
                dgvDet.DataSource = null;
                // Evito que el dgvUsuarios genere columnas automáticas
                dgvDet.AutoGenerateColumns = false;
                // Declaro una variable que va a guardar el Id del comprobante
                int IdComprobante = Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdFactura"].Value);
                // Armo la cadena SQL para ejecutar el procedieminto almacenado que trae el detalle del comprobante
                string newMyCadenaSql = "";
                // Creo un datatable y le paso los datos de la consulta SQL
                DataTable myTabla = null;
                //Verifico Conexion
                if (!(x_Con == 0))
                {
                    newMyCadenaSql = "Select * from Vista_eFactura_Detalle_2 Where IdFactura =  " + IdComprobante;
                    myTabla = clsDataBD.GetSqlB(newMyCadenaSql);
                }
                else
                {
                    newMyCadenaSql = "Select * from Vista_eFactura_Detalle Where IdFactura =  " + IdComprobante;
                    myTabla = clsDataBD.GetSql(newMyCadenaSql);
                }

                //Verificar si es Nota de Credito B
                if (Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdTipoComprobante"].Value) == 8) //8 Nota Credito B 
                {
                    dgvDet.Columns["Bonif"].Visible = false;
                    dgvDet.Columns["SubtotalDto"].Visible = false;
                    dgvDet.Columns["ValorIva"].Visible = false;
                    dgvDet.Columns["SubTotalDet"].HeaderText = "SubTotal";
                    dgvDet.Columns["Articulo"].Width = 500;
                }

                // Asigno a la grilla el source
                dgvDet.DataSource = myTabla;
            }
        }

        #endregion

        #region CheckedChanged

        private void rdbT_CheckedChanged(object sender, EventArgs e)
        {
            this.bFiltro = "AND IdTipoComprobante in (3,8)";
            this.CargarGrillaComprobantes("", "");
        }

        private void rdbA_CheckedChanged(object sender, EventArgs e)
        {
            this.bFiltro = "AND IdTipoComprobante=3";
            this.CargarGrillaComprobantes("", "");
        }

        private void rdbB_CheckedChanged(object sender, EventArgs e)
        {
            this.bFiltro = "AND IdTipoComprobante=8";
            this.CargarGrillaComprobantes("", "");
        }

        #endregion

        #region Metodo BorrarArchvo

        //ELIMINAR ARCHIVO
        public void BorrarArchivo(String archivo)
        {
            if (System.IO.File.Exists(@archivo))
            {
                try
                {
                    System.IO.File.Delete(@archivo);
                }
                catch (System.IO.IOException e)
                {
                    return;
                }
            }
        }

        #endregion

        #region Botones

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //ELIMINAR ARCHIVO
            BorrarArchivo(Application.StartupPath + "\\AFIP.jpg");

            //Cerrar
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Get Index
            this.indexFila = dgvComprobantes.CurrentRow.Index;
            //Set
            this.grpSearch.Visible = true;
            this.btnAceptar.Visible = true;
            this.btnCancelar.Visible = true;
            this.btnBuscar.Visible = false;
            this.btnImprimir.Visible = false;
            this.btnSalir.Visible = false;
            this.gpbDetalle.Size = new System.Drawing.Size(993, 170);
            this.dgvDet.Size = new System.Drawing.Size(977, 133);
            //Clean
            this.txtComprobante.Text = "";
            this.txtCodigo.Text = "";
            this.txtRs.Text = "";
            //Pos
            txtCodigo.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Recargar
            if (bSearch)
            {
                //Cargo las localidades
                this.CargarGrillaComprobantes();
                //Botones
                this.grpSearch.Visible = false;
                this.btnAceptar.Visible = false;
                this.btnCancelar.Visible = false; ;
                this.btnBuscar.Visible = true;
                this.btnImprimir.Visible = true;
                this.btnSalir.Visible = true;
                this.gpbDetalle.Size = new System.Drawing.Size(993, 233);
                this.dgvDet.Size = new System.Drawing.Size(977, 184);
                //Foco
                PosicionarFocoFila();
            }
            else
            {
                //Botones
                this.grpSearch.Visible = false;
                this.btnAceptar.Visible = false;
                this.btnCancelar.Visible = false;
                this.btnBuscar.Visible = true;
                this.btnImprimir.Visible = true;
                this.btnSalir.Visible = true;
                this.gpbDetalle.Size = new System.Drawing.Size(993, 233);
                this.dgvDet.Size = new System.Drawing.Size(977, 184);
                //Foco
                PosicionarFocoFila();
            }

            //.F.
            bSearch = false;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Tomar el Id
            IdFact = Convert.ToInt32(dgvComprobantes.CurrentRow.Cells["IdFactura"].Value.ToString());
            // Lleno nuevamente la grilla
            this.CargarGrillaComprobantes();
            //Botones
            this.grpSearch.Visible = false;
            this.btnAceptar.Visible = false;
            this.btnCancelar.Visible = false;
            this.btnBuscar.Visible = true;
            this.btnImprimir.Visible = true;
            this.btnSalir.Visible = true;
            this.gpbDetalle.Size = new System.Drawing.Size(993, 218);
            this.dgvDet.Size = new System.Drawing.Size(977, 184);
            //IdFact >0? Solo cuando busca reposiciona por ID
            if (!(IdFact == 0 && bSearch))
            {
                //Reposicionar
                ReposicionarById();
                //Reset
                IdFact = 0;
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

        #endregion

        #region Reposicionar Grilla por Id

        //REPOSICIONA GRILLA POR ID
        private void ReposicionarById()
        {
            //recorrer
            foreach (DataGridViewRow myRow in this.dgvComprobantes.Rows)
            {
                if (Convert.ToInt32(myRow.Cells["IdFactura"].Value.ToString()) == IdFact)
                {
                    // Devuelvo el foco a la fila de la grilla desde donde se llamó
                    this.dgvComprobantes.CurrentCell = dgvComprobantes[1, myRow.Index];

                    // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                    EventArgs ea = new EventArgs();
                    this.dgvComprobantes_SelectionChanged(this.dgvComprobantes, ea);

                    //Salir
                    break;
                }
            }
        }

        #endregion

        #region Método que devuelve el foco a la fila desde donde se presionó algún botón

        private void PosicionarFocoFila()
        {
            if (dgvComprobantes.Rows.Count != 0 && dgvComprobantes.Rows.Count > this.indexFila)
            {
                // Devuelvo el foco a la fila de la grilla desde donde se llamó
                if (this.indexFila == -1)
                {
                    this.indexFila = 0;
                }
                this.dgvComprobantes.CurrentCell = dgvComprobantes[1, this.indexFila];
                // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                EventArgs ea = new EventArgs();
                this.dgvComprobantes_SelectionChanged(this.dgvComprobantes, ea);
            }
        }

        #endregion

        private void txtComprobante_TextChanged(object sender, EventArgs e)
        {
            if (!(txtComprobante.Text == ""))
            {
                this.CargarGrillaComprobantes(this.txtComprobante.Text, "PuntoNro");
            }
        }

        private void txtRs_TextChanged(object sender, EventArgs e)
        {
            if (!(txtRs.Text == ""))
            {
                this.CargarGrillaComprobantes(this.txtRs.Text, "RazonSocial");
            }

        }

        private void btnEmitir_Click(object sender, EventArgs e)
        {
           
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            
            if (!(txtCodigo.Text == ""))
            {
                this.CargarGrillaComprobantes(this.txtCodigo.Text, "IdCliente");
            }
            
        }
    }
}

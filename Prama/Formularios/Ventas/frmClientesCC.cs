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
    public partial class frmClientesCC : Form
    {
        #region Variables del formulario

        // Variable que almacena las cadenas SQL
        string myCadenaSQL = "";
        //.F.
        bool bSearch = false;
        //IndexFila
        int indexFila = 0;
        //Bandera
        bool yaCargado = false;
        //Filtro
        string bFiltro = "";
        //Id Cliente
        int Id_Cli = 0;

        #endregion

        public frmClientesCC(int p_Id_Cli = 0)
        {
            InitializeComponent();

            //Id Cliente, sòlo cuando se llama al formulario desde Ventana de Comprobantes
            //facturacion
            Id_Cli = p_Id_Cli;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Cierro el formulario
            this.Close();
        }

        private void frmClientesCC_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            //CAMBIAR PUNTERO MOUSE
            Cursor.Current = Cursors.WaitCursor;
            //Estado
            clsGlobales.myEstado = "C";
            //Tooltips
            CargarToolsTip();
            // Activo los botones
            ActivarBotones();
            // Cargo la grilla de los proveedores
            CargarGrilla("", "");
            // Deshabilito el ordenamiento de las cabeceras de las grillas.
            DeshabilitarOrdenGrillas();
            // Formateo la grilla desde donde se hizo la llamada
            SetearGrilla();
            // Agrando el gpb que contiene la grilla del detalle
            gpbCC.Height = 246;
            // Agrando la grilla para que tape los campos de búsqueda
            dgvComprobantes.Height = 205;
            //Titulo
            this.Text = clsGlobales.cFormato.getTituloVentana() + " - CUENTAS CORRIENTES DE CLIENTES ";
            //CAMBIAR PUNTERO MOUSE
            Cursor.Current = Cursors.Default;
            //Reposicionarse
            if (!(Id_Cli == 0))
            {
              this.ReposicionarById(Id_Cli);
            }
        }

        #region Método que carga los ToolsTip del formulario

        private void CargarToolsTip()
        {
            toolTip1.SetToolTip(this.btnRecibos, "Ver Recibos");
            toolTip2.SetToolTip(this.btnCancelar, "Cancelar");
            toolTip3.SetToolTip(this.btnSalir, "Salir");
            toolTip4.SetToolTip(this.btnAceptar, "Aceptar");
            toolTip5.SetToolTip(this.btnVer, "Generar Recibo");
            toolTip7.SetToolTip(this.btnBuscar, "Buscar");
            toolTip8.SetToolTip(this.btnImprimir, "Imprimir");
            toolTip9.SetToolTip(this.btnMovimientos, "Ver Cta.Cte.");
        }

        #endregion

        #region Método que devuelve el foco a la fila desde donde se presionó algún botón

        private void PosicionarFocoFila()
        {
            if (dgvCli.Rows.Count != 0 && dgvCli.Rows.Count > this.indexFila)
            {
                // Devuelvo el foco a la fila de la grilla desde donde se llamó
                if (this.indexFila == -1)
                {
                    this.indexFila = 0;
                }
                this.dgvCli.CurrentCell = dgvCli[1, this.indexFila];
                // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                EventArgs ea = new EventArgs();
                this.dgvCli_SelectionChanged(this.dgvCli, ea);
            }
        }

        #endregion

        #region Reposicionar Grilal por Id

        //REPOSICIONA GRILLA POR ID
        private void ReposicionarById(int p_Id_Cliente)
        {
            //recorrer
            foreach (DataGridViewRow myRow in this.dgvCli.Rows)
            {
                if (Convert.ToInt32(myRow.Cells["IdCliente"].Value.ToString()) == p_Id_Cliente)
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
              
        #region Método que activa los botones

        private void ActivarBotones()
        {
            switch (clsGlobales.myEstado)
            {
                case "B":
                    // Si estoy buscando
                    btnBuscar.Visible = false;
                    btnBuscar.TabStop = false;
                    btnImprimir.Visible = false;
                    btnImprimir.TabStop = false;
                    btnRecibos.Visible = false;
                    btnRecibos.TabStop = false;
                    btnAceptar.Visible = true;
                    btnAceptar.TabStop = true;
                    btnCancelar.Visible = true;
                    btnCancelar.TabStop = true;
                    btnVer.Visible = false;
                    btnVer.TabStop = false;
                    btnSalir.Visible = false;
                    btnSalir.TabStop = false;
                    btnMovimientos.TabStop = false;
                    btnMovimientos.Visible = false;

                    break;

                case "C":
                    // Si estoy en espera
                    btnBuscar.Visible = true;
                    btnBuscar.TabStop = true;
                    btnImprimir.Visible = true;
                    btnImprimir.TabStop = true;
                    btnRecibos.Visible = true;
                    btnRecibos.TabStop = true;
                    btnAceptar.Visible = false;
                    btnAceptar.TabStop = false;
                    btnCancelar.Visible = false;
                    btnCancelar.TabStop = false;
                    btnVer.Visible = true;
                    btnVer.TabStop = true;
                    btnSalir.Visible = true;
                    btnSalir.TabStop = true;
                    btnMovimientos.TabStop = true;
                    btnMovimientos.Visible = true;
                    break;
            }
        }

        #endregion

        #region Método que limpia los campos de búesueda

        private void LimpiarCamposBusqueda()
        {
            txtCodigo.Text = "";
            txtCUIT.Text = "";
            txtRazon.Text = "";
        }

        #endregion

        #region Método que deshabilita el reordenamiento de las grilla desde sus cabeceras

        private void DeshabilitarOrdenGrillas()
        {
            // Deshabilito la reordenación de las columnas de las grillas
            foreach (DataGridViewColumn dgvCol in dgvComprobantes.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn dgvCol in this.dgvCli.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        #endregion

        #region Método que carga la grilla de los Clientes

        private void CargarGrilla(string sCampo, string sBuscar)
        {
            
            myCadenaSQL = "";

            if (clsGlobales.ConB == null)
            {
                if (sBuscar == "")
                {

                    if (!(bFiltro == ""))
                    {
                        // Armo la cadena SQL para taer los datos de los proveedores
                        myCadenaSQL = "select * from Vista_Clientes WHERE " + bFiltro;
                    }
                    else
                    {
                        // Armo la cadena SQL para taer los datos de los proveedores
                        myCadenaSQL = "select * from Vista_Clientes";
                    }

                    bSearch = false;
                }
                else
                {
                    if (sCampo == "RazonSocial" || sCampo == "Cuit")
                    {
                        if (!(bFiltro == ""))
                        {
                            myCadenaSQL = "select * from Vista_Clientes where " + sCampo + " like '" + sBuscar + "%' AND " + bFiltro;
                        }
                        else
                        {
                            myCadenaSQL = "select * from Vista_Clientes where " + sCampo + " like '" + sBuscar + "%'" + bFiltro; 
                        }
                        bSearch = true;
                    }
                    else if (sCampo == "Codigo")
                    {
                        if (!(bFiltro == ""))
                        {
                            myCadenaSQL = "select * from Vista_Clientes where " + sCampo + "=" + sBuscar + " AND " + bFiltro;
                        }
                        else
                        {
                            myCadenaSQL = "select * from Vista_Clientes where " + sCampo + "=" + sBuscar + " " + bFiltro; 
                        }
                        bSearch = true;
                    }
                }

                try
                {
                    //Clean
                   // this.dgvCli.DataSource = null;
                    // Ejecuto la consulta y paso los datos a una tabla
                    DataTable myTablaCli = clsDataBD.GetSql(myCadenaSQL);
                    // Evito que el dgv genere columnas automáticas
                    this.dgvCli.AutoGenerateColumns = false;
                    // Asigno el source de la grilla de los proveedores a la nueva tabla
                    this.dgvCli.DataSource = myTablaCli;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

            else
            {
                if (sBuscar == "")
                {
                    if (!(bFiltro == ""))
                    {
                        // Armo la cadena SQL para taer los datos de los proveedores
                        myCadenaSQL = "select * from Vista_SaldosClientes WHERE " + bFiltro;
                    }
                    else
                    {
                        // Armo la cadena SQL para taer los datos de los proveedores
                        myCadenaSQL = "select * from Vista_SaldosClientes";
                    }
                    bSearch = false;
                }
                else
                {
                    if (sCampo == "RazonSocial" || sCampo == "Cuit")
                    {
                        if (!(bFiltro == ""))
                        {
                          myCadenaSQL = "select * from Vista_SaldosClientes where " + sCampo + " like '" + sBuscar + "%' AND " + bFiltro;
                        }
                        else
                        {
                            myCadenaSQL = "select * from Vista_SaldosClientes where " + sCampo + " like '" + sBuscar + "%'" + bFiltro; 
                        }
                        bSearch = true;
                    }
                    else if (sCampo == "Codigo")
                    {

                        if (!(bFiltro == ""))
                        {
                            myCadenaSQL = "select * from Vista_SaldosClientes where " + sCampo + "=" + sBuscar + " AND " + bFiltro;
                        }
                        else
                        {
                            myCadenaSQL = "select * from Vista_SaldosClientes where " + sCampo + "=" + sBuscar + " " + bFiltro; 
                        }
                        bSearch = true;
                    }
                }

                //Clean
                //dgvCli.DataSource = null;
                // Ejecuto la consulta y paso los datos a una tabla
                DataTable myTablaCli2 = clsDataBD.GetSqlB(myCadenaSQL);
                // Evito que el dgv genere columnas automáticas
                dgvCli.AutoGenerateColumns = false;
                // Asigno el source de la grilla de los proveedores a la nueva tabla
                dgvCli.DataSource = myTablaCli2;

                //try
                //{
                //    //Clean
                //    //dgvCli.DataSource = null;
                //    // Ejecuto la consulta y paso los datos a una tabla
                //    DataTable myTablaCli2 = clsDataBD.GetSqlB(myCadenaSQL);
                //    // Evito que el dgv genere columnas automáticas
                //    dgvCli.AutoGenerateColumns = false;
                //    // Asigno el source de la grilla de los proveedores a la nueva tabla
                //    dgvCli.DataSource = myTablaCli2;
                //}
                //catch (Exception Ex)
                //{
                //    MessageBox.Show(Ex.Message);
                //}
            }

            if (this.dgvCli.Rows.Count > 0)
            {
                yaCargado = true;

                double saldo = 0;

                if (clsGlobales.ConB == null)
                {
                    if (Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoAFavor"].Value.ToString()) < 0)
                    {
                        saldo = Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoCli"].Value.ToString()) + Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoAFavor"].Value.ToString());
                    }
                    else
                    {
                        saldo = Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoCli"].Value.ToString()) - Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoAFavor"].Value.ToString());
                    }
                   // saldo = Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoCli"].Value.ToString()) - Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoAFavor"].Value.ToString());
                    this.txtSaldo.Text = saldo.ToString("###,###,##0.00");
                }
                else
                {

                    if (Convert.ToDouble(dgvCli.CurrentRow.Cells["AFavor_ByN"].Value.ToString()) < 0)
                    {
                        saldo = Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoTotal"].Value.ToString()) + (Convert.ToDouble(dgvCli.CurrentRow.Cells["AFavor_ByN"].Value.ToString())); // + Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoAFB"].Value.ToString()));
                    }
                    else
                    {
                        saldo = Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoTotal"].Value.ToString()) - (Convert.ToDouble(dgvCli.CurrentRow.Cells["AFavor_ByN"].Value.ToString())); // + Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoAFB"].Value.ToString()));
                    }
                    //saldo = Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoTotal"].Value.ToString()) - (Convert.ToDouble(dgvCli.CurrentRow.Cells["AFavor_ByN"].Value.ToString())); // + Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoAFB"].Value.ToString()));
                    this.txtSaldo.Text = saldo.ToString("###,###,##0.00");
                }

                //Calcular Deuda Total
                CalcularDeudaTotal();

            }
                       
        }

        #endregion

        private void dgvCli_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCli.Rows.Count >0)
            {
                CargarCtaCte();       
            }


        }

        #region Método que carga la cuenta corriente del Cliente seleccionado

        private void CargarCtaCte()
        {
            // Tomo el id del proveedor de la fila seleccionada
            int Id = Convert.ToInt32(this.dgvCli.CurrentRow.Cells["IdCliente"].Value);
            // Armo la cadena SQl que trae los comprobantes de compras pendientes
            string myCadSQL = ""; //+ " AND Saldo > 0";
            // Evito que el dgvUsuarios genere columnas automáticas
            dgvComprobantes.AutoGenerateColumns = false;
            // Creo un nuevo DataTable
            DataTable mDtTable = new DataTable();
            // Pregunto por el tipo de conexión
            if (clsGlobales.ConB == null)
            {
                myCadSQL = "SELECT * FROM Vista_eFactura WHERE IdCliente = " + Id + " Order by Fecha ASC";
                // Le asigno al nuevo DataTable los datos de la consulta SQL en blanco
                mDtTable = clsDataBD.GetSql(myCadSQL);
            }
            else
            {
                myCadSQL = "SELECT * FROM Vista_eFactura_2 WHERE IdCliente = " + Id + " Order by Fecha ASC";
                // Le asigno al nuevo DataTable los datos de la consulta SQL en negro
                mDtTable = clsDataBD.GetSqlB(myCadSQL);
            }

            // Asigno el source de la grilla
            dgvComprobantes.DataSource = mDtTable;
            // Variable que almacena el item que le corresponde a la fila de la grilla
            int filas = 1;
            // Recorro la grilla y asigno el número de item
            if (dgvComprobantes.Rows.Count > 0)
            {
                // Recorro la grilla 
                foreach (DataGridViewRow row in dgvComprobantes.Rows)
                {
                    // Asigno el número de item
                    row.Cells["Item"].Value = filas;
                    // Aumento el contador
                    filas++;
                }
            }

            double saldo = 0;
       
            //Saldo           
            if (yaCargado)
            {
                if (clsGlobales.ConB == null)
                {
                    if (Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoAFavor"].Value.ToString()) < 0)
                    {
                        saldo = Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoCli"].Value.ToString()) + Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoAFavor"].Value.ToString());
                    }
                    else
                    {
                        saldo = Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoCli"].Value.ToString()) - Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoAFavor"].Value.ToString());
                    }
                    this.txtSaldo.Text = saldo.ToString("###,###,##0.00");
                }
                else
                {
                 /*   if (dgvCli.CurrentRow.Cells["SaldoTotal"].Value != null)
                    {
                        if (Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoAFavor"].Value.ToString()) < 0)
                        {
                            saldo = Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoTotal"].Value.ToString()) + (Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoAFavor"].Value.ToString())); // + Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoAFB"].Value.ToString()));
                        }
                        else
                        {
                            saldo = Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoTotal"].Value.ToString()) - (Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoAFavor"].Value.ToString())); // + Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoAFB"].Value.ToString()));
                        }
                        this.txtSaldo.Text = saldo.ToString("#0.00");
                    }
                    else
                    {
                        this.txtSaldo.Text = saldo.ToString("#0.00");
                    }*/

                    if (dgvCli.CurrentRow.Cells["Saldo_N"].Value != null)
                    {
                        if (Convert.ToDouble(dgvCli.CurrentRow.Cells["AFavor_N"].Value.ToString()) < 0)
                        {
                            saldo = Convert.ToDouble(dgvCli.CurrentRow.Cells["Saldo_N"].Value.ToString()) + (Convert.ToDouble(dgvCli.CurrentRow.Cells["AFavor_N"].Value.ToString())); // + Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoAFB"].Value.ToString()));
                        }
                        else
                        {
                            saldo = Convert.ToDouble(dgvCli.CurrentRow.Cells["Saldo_N"].Value.ToString()) - (Convert.ToDouble(dgvCli.CurrentRow.Cells["AFavor_N"].Value.ToString())); // + Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoAFB"].Value.ToString()));
                        }
                        this.txtSaldo.Text = saldo.ToString("###,###,##0.00");
                    }
                    else
                    {
                        this.txtSaldo.Text = saldo.ToString("###,###,##0.00");
                    }

                }
            }
        }

        #endregion

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Tomo la posición actual de la fila con foco
            if (!(dgvCli.Rows.Count == 0))
            {
                this.indexFila = dgvCli.CurrentRow.Index;
            }
            // Cambio mi estado
            clsGlobales.myEstado = "B";
            // Activo los botones
            ActivarBotones();
            // Cambio la altura de la grilla del detalle
            dgvComprobantes.Height = 141;
            // Cambio el tamaño del gpb que contiene la grilla del detalle
            gpbCC.Height = 185;
            // Muestro le groupbox de búsqueda
            gpbBusquedas.Visible = true;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            // Cargo la grilla pasando como parámetro lo escrito en el txt
            CargarGrilla("Codigo", txtCodigo.Text);
        }

        private void txtCUIT_TextChanged(object sender, EventArgs e)
        {

            if (!(string.IsNullOrEmpty(txtCUIT.Text)))
            {
                // Cargo la grilla pasando como parámetro lo escrito en el txt
                CargarGrilla("Cuit", txtCUIT.Text);
            }

        }

        private void txtRazon_TextChanged(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(txtRazon.Text)))
            {
                // Cargo la grilla pasando como parámetro lo escrito en el txt
                CargarGrilla("RazonSocial", this.txtRazon.Text);
            }
        }

        private void txtCodigo_Click(object sender, EventArgs e)
        {
            // Vacío los otros combos de busqueda
            txtRazon.Text = "";
            txtCUIT.Text = "";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Cambio mi estado a en espera
            clsGlobales.myEstado = "C";
            // Limpio los campos de búesuqeda
            LimpiarCamposBusqueda();
            // Activo los botnes para este estado
            ActivarBotones();
            // Cambio la altura de la grilla del detalle
            dgvComprobantes.Height = 205;
            // Cambio el tamaño del gpb que contiene la grilla del detalle
            gpbCC.Height = 246;
            // Muestro le groupbox de búsqueda
            gpbBusquedas.Visible = false;
            // Vuelvo a cargar la grilla
            CargarGrilla("", "");

        }

        private void btnVer_Click(object sender, EventArgs e)
        {  
            // Tomo la posición actual de la fila con foco
            if (!(dgvCli.Rows.Count == 0))
            {
                this.indexFila = dgvCli.CurrentRow.Index;
            }

            frmClienteCtaCte myForm = new frmClienteCtaCte(Convert.ToInt32(this.dgvCli.CurrentRow.Cells["IdCliente"].Value), Convert.ToDouble(this.dgvCli.CurrentRow.Cells["AFavor_N"].Value));
            myForm.ShowDialog();
            //Cambio mi estado a en espera
            clsGlobales.myEstado = "C";
            // Limpio los campos de búesuqeda
            LimpiarCamposBusqueda();
            // Activo los botnes para este estado
            ActivarBotones();
            // Cambio la altura de la grilla del detalle
            dgvComprobantes.Height = 205;
            // Cambio el tamaño del gpb que contiene la grilla del detalle
            gpbCC.Height = 246;
            // Muestro le groupbox de búsqueda
            gpbBusquedas.Visible = false;
            // Vuelvo a cargar la grilla
            CargarGrilla("", "");
            //Foco
            PosicionarFocoFila();

        }

        private void btnRecibos_Click(object sender, EventArgs e)
        {
            frmRecibos misRecibos = new frmRecibos();
            misRecibos.ShowDialog();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            if (clsGlobales.myEstado == "B")
            {
                //Tomar el Id
                int Id_Cliente = Convert.ToInt32(this.dgvCli.CurrentRow.Cells["IdCliente"].Value.ToString());
                // Cambio mi estado
                clsGlobales.myEstado = "C";
                // Limpio los campos de búesuqeda
                LimpiarCamposBusqueda();
                // Activo los botnes para este estado
                ActivarBotones();
                // Cambio la altura de la grilla del detalle
                dgvComprobantes.Height = 205;
                // Cambio el tamaño del gpb que contiene la grilla del detalle
                gpbCC.Height = 246;
                // Muestro le groupbox de búsqueda
                gpbBusquedas.Visible = false;
                // Lleno nuevamente la grilla
                this.CargarGrilla("", "");
                //Id >0? Solo cuando busca reposiciona por ID
                if (!(Id_Cliente == 0 && bSearch))
                {
                    ReposicionarById(Id_Cliente);
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
        }

        #region Método que setea la grilla de clientes

        private void SetearGrilla()
        {
            //Formateo para la grilla en blanco
            if (clsGlobales.ConB == null)
            {
                // Headers de columnas
                dgvCli.Columns["SaldoCli"].HeaderText = "Saldo";
                dgvCli.Columns["SaldoAFavor"].HeaderText = "A Favor";

                // Visibilidad de columnas
                dgvCli.Columns["Telefono"].Visible = true;
                dgvCli.Columns["Mail"].Visible = true;
                dgvCli.Columns["CUIT"].Visible = true;
                dgvCli.Columns["SaldoCli"].Visible = true;
                dgvCli.Columns["Saldo_N"].Visible = false;
                dgvCli.Columns["SaldoTotal"].Visible = false;
                dgvCli.Columns["SaldoAFavor"].Visible = true;
                dgvCli.Columns["AFavor_N"].Visible = false;
                dgvCli.Columns["AFavor_ByN"].Visible = false;

            }
            // Formateo para el negro
            else
            {
                // Headers de columnas
                dgvCli.Columns["SaldoCli"].HeaderText = "Saldo(B)";
                dgvCli.Columns["SaldoAFavor"].HeaderText = "A Favor(B)";
                // Visibilidad de columnas
                dgvCli.Columns["Telefono"].Visible = false;
                dgvCli.Columns["Mail"].Visible = false;
                dgvCli.Columns["CUIT"].Visible = false;
                dgvCli.Columns["SaldoCli"].Visible = true;
                dgvCli.Columns["Saldo_N"].Visible = true;
                dgvCli.Columns["SaldoTotal"].Visible = true;
                dgvCli.Columns["SaldoAFavor"].Visible = true;
                dgvCli.Columns["AFavor_N"].Visible = true;
                dgvCli.Columns["AFavor_ByN"].Visible = true;
            }
             
        }

        #endregion

        private void rdbT_CheckedChanged(object sender, EventArgs e)
        {
            this.bFiltro = "";
            CargarGrilla("", "");
        }

        private void rdbSD_CheckedChanged(object sender, EventArgs e)
        {
            if (!(clsGlobales.ConB == null))
            {
                this.bFiltro = " Saldo_ByN > 0 ";
            }
            else
            {
                this.bFiltro = " Saldo > 0 "; 
            }
            CargarGrilla("", "");
        }

        private void rdbSA_CheckedChanged(object sender, EventArgs e)
        {

            if (!(clsGlobales.ConB == null))
            {
                this.bFiltro = " AFavor_ByN >0 ";
            }
            else
            {
                this.bFiltro = " SaldoAFavor > 0 ";
            }
            CargarGrilla("", "");
        }

        private void btnMovimientos_Click(object sender, EventArgs e)
        {
            // Tomo la posición actual de la fila con foco
            if (!(dgvCli.Rows.Count == 0))
            {
                this.indexFila = dgvCli.CurrentRow.Index;
            }

            frmClientesMovCtaCte myForm = new frmClientesMovCtaCte(Convert.ToInt32(this.dgvCli.CurrentRow.Cells["IdCliente"].Value), 0, this.dgvCli);
            myForm.ShowDialog();
            //Cambio mi estado a en espera
            clsGlobales.myEstado = "C";
            // Limpio los campos de búesuqeda
            LimpiarCamposBusqueda();
            // Activo los botnes para este estado
            ActivarBotones();
            // Cambio la altura de la grilla del detalle
            dgvComprobantes.Height = 205;
            // Cambio el tamaño del gpb que contiene la grilla del detalle
            gpbCC.Height = 246;
            // Muestro le groupbox de búsqueda
            gpbBusquedas.Visible = false;
            // Vuelvo a cargar la grilla
            CargarGrilla("", "");
            //Foco
            PosicionarFocoFila();
        }

        #region Método que calcula el Saldo total a cobrar general

        private void CalcularDeudaTotal()
        {
            double dTotal = 0;

            if (clsGlobales.ConB == null)
            {
                foreach (DataGridViewRow row in dgvCli.Rows)
                {
                    dTotal += Convert.ToDouble(row.Cells["SaldoCli"].Value);
                }
            }
            else
            {
                foreach (DataGridViewRow row in dgvCli.Rows)
                {
                    dTotal += Convert.ToDouble(row.Cells["SaldoTotal"].Value);
                }
            }

            txtSaldoTotal.Text = dTotal.ToString("###,###,##0.00");
            
        }

        #endregion
    }
}

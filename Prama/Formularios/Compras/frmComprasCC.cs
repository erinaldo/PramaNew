using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Prama.Formularios.Compras;

namespace Prama
{
    public partial class frmComprasCC : Form
    {
        #region Variables del formulario

        // Variable que almacena la fila desde donde se hace la llamada
        int indexGrilla = 0;
        // Variable que almacena las cadenas SQL
        string myCadenaSQL = "";
        // Variable que almacena los filtros
        string bFiltro = "";
        // Bandera que indica que ya se cargó el formulario
        bool yaCargado = false;
        // IndeFila
        int indexFila = 0;
        //Id
        int Id_Prov = 0;
        #endregion

        #region Contructor del formulario

        public frmComprasCC()
        {
            InitializeComponent();
                       
        }

        #endregion

        #region Eventos del formulario

        #region Evento Load del Formulario

        private void frmComprasCtasCtes_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            //CAMBIAR PUNTERO MOUSE
            Cursor.Current = Cursors.WaitCursor;
            //Estado
            clsGlobales.myEstado = "C";
            // Activo los botones
            ActivarBotones();
            // Cargo la grilla de los proveedores
            CargarGrilla("","");
            // Deshabilito el ordenamiento de las cabeceras de las grillas.
            DeshabilitarOrdenGrillas();
            // Agrando el gpb que contiene la grilla del detalle
            gpbCC.Height = 256;
            // Agrando la grilla para que tape los campos de búsqueda
            dgvComprobantes.Height = 230;
            // ya cargado
            yaCargado = true;
            this.Text = clsGlobales.cFormato.getTituloVentana() + " - CUENTAS CORRIENTES PROVEEDORES ";
            //CAMBIAR PUNTERO MOUSE
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region Eventos de los botones

        #region Evento Click del botón Ver

        private void btnVer_Click(object sender, EventArgs e)
        {
            // Tomo el index de la fila desde donde se hace la llamada
            indexGrilla = dgvProv.CurrentRow.Index;
            // Variable que almacena el Id del proveedor seleccionado
            int IdProv = Convert.ToInt32(dgvProv.CurrentRow.Cells["IdProveedor"].Value);
            frmComprasCCProv myForm = new frmComprasCCProv(IdProv);
            myForm.ShowDialog();

            // Cuando vuelvo actualizo las dos grillas
            CargarGrilla("", "");

            //Pongo el foco de la grilla de proveedores desde donde se hixo la llamada
            dgvProv.CurrentCell = dgvProv[0, indexGrilla];
            CargarCtaCte();

        }

        #endregion

        #region Evento Click del botón Buscar

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Cambio mi estado
            clsGlobales.myEstado = "B";
            // Activo los botones
            ActivarBotones();
            // Cambio la altura de la grilla del detalle
            dgvComprobantes.Height = 160;
            // Cambio el tamaño del gpb que contiene la grilla del detalle
            gpbCC.Height = 185;
            // Muestro le groupbox de búsqueda
            gpbBusquedas.Visible = true;

        }

        #endregion

        #region Evento Click del botón Imprimir

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //Data Set
            dsReportes oDsArt = new dsReportes();
            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = dgvProv.Rows.Count;
            double SaldoTotal = 0;

            for (int i = 0; i < dgvFilas; i++)
            {
                if (Convert.ToDouble(dgvProv["SaldoProv", i].Value) != 0)
                {
                    SaldoTotal = SaldoTotal + Convert.ToDouble(dgvProv["SaldoProv", i].Value);
                    oDsArt.Tables["dtCtaCteProvs"].Rows.Add
                    (new object[] 
                        { 
                            dgvProv["IdProveedor",i].Value.ToString(),
                            dgvProv["RazonSocial",i].Value.ToString(),
                            dgvProv["CondicionIva",i].Value.ToString(),
                            dgvProv["CUIT",i].Value.ToString(),
                            dgvProv["Telefono",i].Value.ToString(),
                            dgvProv["MailEmpresa",i].Value.ToString(),
                            dgvProv["SaldoProv",i].Value.ToString(),
                        }
                    );
                }
            }

            //Objeto Reporte
            rptCtaCteProvs oRepArt = new rptCtaCteProvs();
            //Cargar Reporte            
            //    oRepTipoCli.Load(Application.StartupPath + "\\rptTipoCli.rpt");
            oRepArt.Load(Application.StartupPath + "\\rptCtaCteProvs.rpt");
            //Establecer el DataSet como DataSource
            oRepArt.SetDataSource(oDsArt);
            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepArt;

            oRepArt.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepArt.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
            oRepArt.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
            oRepArt.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
            oRepArt.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
            oRepArt.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepArt.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
            oRepArt.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";
            oRepArt.DataDefinition.FormulaFields["SaldoTotal"].Text = "'" + SaldoTotal + "'";

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports();
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();
        }

        #endregion

        #region Evento Click del botón cancelar

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Cambio mi estado a en espera
            clsGlobales.myEstado = "C";
            // Limpio los campos de búesuqeda
            LimpiarCamposBusqueda();
            // Activo los botnes para este estado
            ActivarBotones();
            // Cambio la altura de la grilla del detalle
            dgvComprobantes.Height = 221;
            // Cambio el tamaño del gpb que contiene la grilla del detalle
            gpbCC.Height = 256;
            // Vuelvo a cargar la grilla
            CargarGrilla("", "");
            // Pongo el foco de la fila
            PosicionarFocoFila();
        }

        #endregion

        #region Evento Click del botón btnSalir

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Cierro el formulario
            this.Close();
        }

        #endregion

        #endregion

        #region Eventos TextChanged y Click de los TextBox

        private void txtRazon_TextChanged(object sender, EventArgs e)
        {
            // Cargo la grilla pasando como parámetro lo escrito en el txt
            CargarGrilla("RazonSocial", txtRazon.Text);
        }

        private void txtRazon_Click(object sender, EventArgs e)
        {
            // Vacío los otros combos de busqueda
            txtCodigo.Text = "";
            txtCUIT.Text = "";
        }

        private void txtCUIT_Click(object sender, EventArgs e)
        {
            // Vacío los otros combos de busqueda
            txtCodigo.Text = "";
            txtRazon.Text = "";
        }

        private void txtCUIT_TextChanged(object sender, EventArgs e)
        {
            // Cargo la grilla pasando como parámetro lo escrito en el txt
            CargarGrilla("CUIT", txtCUIT.Text);
        }

        private void txtCodigo_Click(object sender, EventArgs e)
        {
            // Vacío los otros combos de busqueda
            txtRazon.Text = "";
            txtCUIT.Text = "";
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            // Cargo la grilla pasando como parámetro lo escrito en el txt
            CargarGrilla("IdProveedor", txtCodigo.Text);
        }

        #endregion

        #region Evento SelectionChanged de la Grilla

        private void dgvProv_SelectionChanged(object sender, EventArgs e)
        {
            // Cargo los datos de las facturas pendientes de pago del proveedor
            CargarCtaCte();
        }

        #endregion

        #endregion

        #region Métodos del formulario

        #region Método que carga la grilla de los proveedores

        private void CargarGrilla(string sCampo, string sBuscar)
        {
            DataTable myTablaProv;
            
            if (clsGlobales.ConB == null)
            {
                if (sBuscar == "")
                {
                    if (bFiltro == "")
                    {
                        // Armo la cadena SQL para taer los datos de los proveedores
                        myCadenaSQL = "select * from Vista_Proveedores";
                    }
                    else
                    {
                        // Armo la cadena SQL para taer los datos de los proveedores
                        myCadenaSQL = "select * from Vista_Proveedores where " + bFiltro;
                    }
                    
                }
                else
                {
                    if (bFiltro == "")
                    {
                        // Armo la cadena SQL para taer los datos de los proveedores
                        myCadenaSQL = "select * from Vista_Proveedores where " + sCampo + " like '" + sBuscar + "%'";
                    }
                    else
                    {
                        // Armo la cadena SQL para taer los datos de los proveedores
                        myCadenaSQL = "select * from Vista_Proveedores where " + sCampo + " like '" + sBuscar + "%' and " + bFiltro;
                    }
                }

                // Ejecuto la consulta y paso los datos a una tabla
                myTablaProv = clsDataBD.GetSql(myCadenaSQL);
                
            }
                
            else
            {
                if (sBuscar == "")
                {
                    if (bFiltro == "")
                    {
                        // Armo la cadena SQL para taer los datos de los proveedores
                        myCadenaSQL = "select * from Vista_SaldoProveedores";
                    }
                    else
                    {
                        // Armo la cadena SQL para taer los datos de los proveedores
                        myCadenaSQL = "select * from Vista_SaldoProveedores where " + bFiltro;
                    }
                    
                }
                else
                {
                    if (bFiltro == "")
                    {
                        // Armo la cadena SQL para taer los datos de los proveedores
                        myCadenaSQL = "select * from Vista_SaldoProveedores where " + sCampo + " like '" + sBuscar + "%'";
                    }
                    else
                    {
                        // Armo la cadena SQL para taer los datos de los proveedores
                        myCadenaSQL = "select * from Vista_SaldoProveedores where " + sCampo + " like '" + sBuscar + "%' and " + bFiltro;
                    }
                }

                // Ejecuto la consulta y paso los datos a una tabla
                myTablaProv = clsDataBD.GetSqlB(myCadenaSQL);
                
            }

            // Evito que el dgv genere columnas automáticas
            dgvProv.AutoGenerateColumns = false;
            // Asigno el source de la grilla de los proveedores a la nueva tabla
            dgvProv.DataSource = myTablaProv;
            // Formateo la grilla
            SetearGrilla();


        }

        #endregion

        #region Método que carga la cuenta corriente del proveedor seleccionado

        private void CargarCtaCte()
        {
            // Tomo el id del proveedor de la fila seleccionada
            int IdProv = Convert.ToInt32(dgvProv.CurrentRow.Cells["IdProveedor"].Value);
            
            // Evito que el dgvUsuarios genere columnas automáticas
            dgvComprobantes.AutoGenerateColumns = false;
            // Creo un nuevo DataTable
            DataTable mDtTable = new DataTable();
            // Pregunto por el tipo de conexión
            if (clsGlobales.ConB == null)
            {
                // Armo la cadena SQl que trae los comprobantes de compras pendientes
                //myCadenaSQL = "SELECT * FROM Vista_ComprobantesCompras WHERE IdTipo = " + 3 + " and IdProveedor = " + IdProv;
                myCadenaSQL = "SELECT * FROM Vista_ComprobantesCompras WHERE IdProveedor = " + IdProv;
                // Le asigno al nuevo DataTable los datos de la consulta SQL en blanco
                mDtTable = clsDataBD.GetSql(myCadenaSQL);
            }
            else
            {
                // Armo la cadena SQl que trae los comprobantes de compras pendientes
                //myCadenaSQL = "SELECT * FROM Vista_ComprobantesCompras2 WHERE IdTipo = " + 3 + " and IdProveedor = " + IdProv;
                myCadenaSQL = "SELECT * FROM Vista_ComprobantesCompras2 WHERE IdProveedor = " + IdProv;
                // Le asigno al nuevo DataTable los datos de la consulta SQL en negro
                mDtTable = clsDataBD.GetSqlB(myCadenaSQL);
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
                    if (Convert.ToDouble(dgvProv.CurrentRow.Cells["AFavor_B"].Value.ToString()) < 0)
                    {
                        saldo = Convert.ToDouble(dgvProv.CurrentRow.Cells["Saldo_B"].Value.ToString()) + Convert.ToDouble(dgvProv.CurrentRow.Cells["AFavor_B"].Value.ToString());
                    }
                    else
                    {
                        saldo = Convert.ToDouble(dgvProv.CurrentRow.Cells["Saldo_B"].Value.ToString()) - Convert.ToDouble(dgvProv.CurrentRow.Cells["AFavor_B"].Value.ToString());
                    }
                    this.txtSaldo.Text = saldo.ToString("#0.00");
                }
                else
                {
                    if (dgvProv.CurrentRow.Cells["Saldo_N"].Value != null)
                    {
                        if (Convert.ToDouble(dgvProv.CurrentRow.Cells["AFavor_N"].Value.ToString()) < 0)
                        {
                            saldo = Convert.ToDouble(dgvProv.CurrentRow.Cells["Saldo_N"].Value.ToString()) + (Convert.ToDouble(dgvProv.CurrentRow.Cells["AFavor_N"].Value.ToString())); // + Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoAFB"].Value.ToString()));
                        }
                        else
                        {
                            saldo = Convert.ToDouble(dgvProv.CurrentRow.Cells["Saldo_N"].Value.ToString()) - (Convert.ToDouble(dgvProv.CurrentRow.Cells["AFavor_N"].Value.ToString())); // + Convert.ToDouble(dgvCli.CurrentRow.Cells["SaldoAFB"].Value.ToString()));
                        }
                        this.txtSaldo.Text = saldo.ToString("#0.00");
                    }
                    else
                    {
                        this.txtSaldo.Text = saldo.ToString("#0.00");
                    }

                }
            }
            
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
            foreach (DataGridViewColumn dgvCol in dgvProv.Columns)
            {
                dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;
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

                    btnAceptar.Visible = true;
                    btnAceptar.TabStop = true;

                    btnBuscar.Visible = false;
                    btnBuscar.TabStop = false;
                    btnImprimir.Visible = false;
                    btnImprimir.TabStop = false;

                    btnSalir.Visible = true;
                    btnSalir.TabStop = true;

                    btnCancelar.Visible = true;
                    btnCancelar.TabStop = true;

                    btnRecibos.Visible = false;
                    btnRecibos.TabStop = false;

                    btnVer.Visible = false;
                    btnVer.TabStop = false;

                    btnMovimientos.Visible = false;
                    btnMovimientos.TabStop = false;

                    break;

                case "C":
                    // Si estoy en espera
                    btnBuscar.Visible = true;
                    btnBuscar.TabStop = true;
                    
                    btnImprimir.Visible = true;
                    btnImprimir.TabStop = true;

                    btnAceptar.Visible = false;
                    btnAceptar.TabStop = false;
                    btnCancelar.Visible = false;
                    btnCancelar.TabStop = false;

                    btnRecibos.Visible = true;
                    btnRecibos.TabStop = true;
                    btnVer.Visible = true;
                    btnVer.TabStop = true;
                    btnMovimientos.Visible = true;
                    btnMovimientos.TabStop = true;

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

        #region Método que da formato a la grilla

        private void SetearGrilla()
        {
            if (clsGlobales.ConB == null)
            {
                dgvProv.Columns["MailEmpresa"].Visible = true;
                dgvProv.Columns["Telefono"].Visible = true;
                dgvProv.Columns["CUIT"].Visible = true;

                dgvProv.Columns["Saldo_B"].Visible = true;
                dgvProv.Columns["Saldo_N"].Visible = false;
                dgvProv.Columns["Saldo_BN"].Visible = false;

                dgvProv.Columns["AFavor_B"].Visible = true;
                dgvProv.Columns["AFavor_N"].Visible = false;
                dgvProv.Columns["AFavor_BN"].Visible = false;
            }
            else
            {
                dgvProv.Columns["MailEmpresa"].Visible = false;
                dgvProv.Columns["Telefono"].Visible = false;
                dgvProv.Columns["CUIT"].Visible = false;

                dgvProv.Columns["Saldo_B"].Visible = true;
                dgvProv.Columns["Saldo_N"].Visible = true;
                dgvProv.Columns["Saldo_BN"].Visible = true;

                dgvProv.Columns["AFavor_B"].Visible = true;
                dgvProv.Columns["AFavor_N"].Visible = true;
                dgvProv.Columns["AFavor_BN"].Visible = true;
            }
        }

        #endregion

        private void rdbT_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbT.Checked)
            {
                bFiltro = "";
                CargarGrilla("", "");
            }
        }

        #endregion

        private void rdbSD_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbSD.Checked)
            {
                if (!(clsGlobales.ConB == null))
                {
                    this.bFiltro = " Saldo_BN > 0 ";
                }
                else
                {
                    this.bFiltro = " Saldo_B > 0 ";
                }
                CargarGrilla("", "");
            }
        }

        private void rdbSA_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbSA.Checked)
            {
                if (!(clsGlobales.ConB == null))
                {
                    this.bFiltro = " AFavor_BN < 0 ";
                }
                else
                {
                    this.bFiltro = " AFavor_B < 0 ";
                }
                CargarGrilla("", "");
            }
        }

        private void btnRecibos_Click(object sender, EventArgs e)
        {
            frmComprasOP myForm = new frmComprasOP();
            myForm.ShowDialog();
        }

        private void btnVer_Click_1(object sender, EventArgs e)
        {
            // Tomo el Id del proveedor - G - 2019-09-30
            Id_Prov = Convert.ToInt32(dgvProv.CurrentRow.Cells["IdProveedor"].Value);

            frmComprasCCProv myForm = new frmComprasCCProv(Convert.ToInt32(dgvProv.CurrentRow.Cells["IdProveedor"].Value), Convert.ToDouble(this.dgvProv.CurrentRow.Cells["AFavor_N"].Value));
            myForm.ShowDialog();

            // Vuelvo a cargar la grilla
            CargarGrilla("", "");

            // Pongo el foco al index desde donde se hizo la llamada
            ReposicionarById();
            
        }

        private void btnMovimientos_Click(object sender, EventArgs e)
        {
            // Tomo la posición actual de la fila con foco
            if (!(dgvProv.Rows.Count == 0))
            {
                this.indexGrilla = dgvProv.CurrentRow.Index;
            }

            frmComprasCCMov myForm = new frmComprasCCMov(Convert.ToInt32(this.dgvProv.CurrentRow.Cells["IdProveedor"].Value), 0, this.dgvProv);
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

        #region Método que devuelve el foco a la fila desde donde se presionó algún botón

        private void PosicionarFocoFila()
        {
            if (dgvProv.Rows.Count != 0 && dgvProv.Rows.Count > this.indexGrilla)
            {
                // Devuelvo el foco a la fila de la grilla desde donde se llamó
                if (this.indexGrilla == -1)
                {
                    this.indexGrilla = 0;
                }
                this.dgvProv.CurrentCell = dgvProv[2, this.indexGrilla];
                // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                EventArgs ea = new EventArgs();
                this.dgvProv_SelectionChanged(this.dgvProv, ea);
            }
        }

        #endregion

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            // Cambio la variable global myEstado a Buscar
            clsGlobales.myEstado = "B";
            // Capturo la posición de la grilla desde donde se presionó el botón
            if (dgvProv.Rows.Count > 0)
            {
                this.indexGrilla = this.dgvProv.CurrentRow.Index;
            }
            //gpb busquedas visible
            this.gpbBusquedas.Visible = true;
            // Habilito los botones que correspondan
            ActivarBotones();
            // Cambio la altura de la grilla del detalle
            dgvComprobantes.Height = 141;
            // Cambio el tamaño del gpb que contiene la grilla del detalle
            gpbCC.Height = 185;
            // Pongo el foco en el texbox del código
            txtRazon.Text = "";
            txtCodigo.Text = "";
            txtCUIT.Text = "";
            //Foco
            txtRazon.Focus();

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            
            //Id
            if (dgvProv.Rows.Count > 0)            
            {
                Id_Prov = Convert.ToInt32(dgvProv.CurrentRow.Cells["IdProveedor"].Value);
            }
            // Cambio mi estado a en espera
            clsGlobales.myEstado = "C";
            // Limpio los campos de búesuqeda
            LimpiarCamposBusqueda();
            // Activo los botnes para este estado
            ActivarBotones();
            // Cambio la altura de la grilla del detalle
            dgvComprobantes.Height = 221;
            // Cambio el tamaño del gpb que contiene la grilla del detalle
            gpbCC.Height = 256;
            // Vuelvo a cargar la grilla
            CargarGrilla("", "");
            //Reposicionar
            if (Id_Prov == 0)
            {
                // Pongo el foco de la fila
                PosicionarFocoFila();
            }
            else
            {
                this.ReposicionarById();
            }
        }

        #region Reposicionar Grilla por Id

        //REPOSICIONA GRILLA POR ID
        private void ReposicionarById()
        {
            //recorrer
            foreach (DataGridViewRow myRow in this.dgvProv.Rows)
            {
                if (Convert.ToInt32(myRow.Cells["IdProveedor"].Value.ToString()) == Id_Prov)
                {
                    // Devuelvo el foco a la fila de la grilla desde donde se llamó
                    this.dgvProv.CurrentCell = dgvProv[2, myRow.Index];

                    // Ejecuto el evento Selection Changed de la grilla para que me pase el detalle del comprobante a la grilla de abajo
                    EventArgs ea = new EventArgs();
                    this.dgvProv_SelectionChanged(this.dgvProv, ea);

                    //Salir
                    break;
                }
            }
        }

        #endregion

        private void btnImprimir_Click_1(object sender, EventArgs e)
        {
            //No hay datos? mensaje y volver
            if (!(dgvComprobantes.Rows.Count > 0))
            {
                MessageBox.Show("El Proveedor no posee movimientos, para imprimir, en su cuenta corriente", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            //PREGUNTAR SI ESTA CONFIGURADO EN PARAMETROS
            if (clsGlobales.cParametro.Imprimir)
            {
                DialogResult dlResult = MessageBox.Show("¿Desea imprimir el Detalle de Movimientos de la CtaCte del Proveedor?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Si confirma... cambiar estado
                if (dlResult == DialogResult.No)
                {
                    return;
                }
            }

            //Data Set
            dsReportes oDsProvComp = new dsReportes();

            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = this.dgvComprobantes.Rows.Count;

            for (int i = 0; i < dgvFilas; i++)
            {
                oDsProvComp.Tables["dtProvComp"].Rows.Add
                (new object[] { dgvComprobantes[1,i].Value.ToString(),
                    dgvComprobantes[2,i].Value.ToString(), 
                    dgvComprobantes[6,i].Value.ToString(),
                    dgvComprobantes[8,i].Value.ToString(),
                    dgvComprobantes[9,i].Value.ToString(),
                    Convert.ToDouble(dgvComprobantes[10,i].Value).ToString("###,###,##0.00"),
                    Convert.ToDouble(dgvComprobantes[11,i].Value).ToString("###,###,##0.00"),
                    Convert.ToDouble(dgvComprobantes[12,i].Value).ToString("###,###,##0.00"),
                    Convert.ToDouble(dgvComprobantes[13,i].Value).ToString("###,###,##0.00")
                });
            }

            //Objeto Reporte
            rptMovCtaCteProv oRepMovCtaCteProv = new rptMovCtaCteProv();

            //Cargar Reporte                                    
            oRepMovCtaCteProv.Load(Application.StartupPath + "\\rptMovCtaCteProv.rpt");

            //Establecer el DataSet como DataSource
            oRepMovCtaCteProv.SetDataSource(oDsProvComp);

            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepMovCtaCteProv;

            oRepMovCtaCteProv.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepMovCtaCteProv.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
            oRepMovCtaCteProv.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
            oRepMovCtaCteProv.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
            oRepMovCtaCteProv.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
            oRepMovCtaCteProv.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepMovCtaCteProv.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
            oRepMovCtaCteProv.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";

            //Data Cliente
            oRepMovCtaCteProv.DataDefinition.FormulaFields["Id"].Text = "' Código: " + dgvProv.CurrentRow.Cells["IdProveedor"].Value.ToString() + "'";
            oRepMovCtaCteProv.DataDefinition.FormulaFields["Rs"].Text = "' Razón Social: " + dgvProv.CurrentRow.Cells["RazonSocial"].Value.ToString() + "'";
            oRepMovCtaCteProv.DataDefinition.FormulaFields["Condicion"].Text = "' Condición IVA: " + dgvProv.CurrentRow.Cells["CondicionIVA"].Value.ToString() + "'";
            oRepMovCtaCteProv.DataDefinition.FormulaFields["Telefono"].Text = "' Teléfono: " + dgvProv.CurrentRow.Cells["Telefono"].Value.ToString() + "'";
            oRepMovCtaCteProv.DataDefinition.FormulaFields["CUIT"].Text = "' CUIT: " + dgvProv.CurrentRow.Cells["CUIT"].Value.ToString() + "'";

            oRepMovCtaCteProv.DataDefinition.FormulaFields["total"].Text = "'" + this.txtSaldo.Text + "'";

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports(2);
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog();
        }

    }
}

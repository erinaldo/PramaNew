using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Prama.Clases;

namespace Prama.Formularios.Informes
{
    public partial class frmInformeVtaArticulos : Form
    {

        #region Declaracion Variables

        // Declaro las variables para las fechas
        int iMesActual = 0;
        int iAnoActual = 0;
        clsCLientes myCliente = new clsCLientes();

        #endregion

        #region Constructor
        
        public frmInformeVtaArticulos()
        {
            InitializeComponent();
        }

        #endregion

        #region Evento Load

        private void frmInformeVtaArticulos_Load(object sender, EventArgs e)
        {

            // Variable que almacena el mes en curso
            iMesActual = DateTime.Now.Month;
            // Variable que almacena el año en curso
            iAnoActual = DateTime.Now.Year;
			//icon
            clsFormato.SetIconForm(this); 
			
            // Seteo el DTP de fecha incial
            dtpDesde.Value = new DateTime(iAnoActual, iMesActual, 1);
            // Seteo el DTP de fecha Final
            if (!(iMesActual == 2))
            {
                dtpHasta.Value = new DateTime(iAnoActual, iMesActual, 30);
            }
            else
            {
                dtpHasta.Value = new DateTime(iAnoActual, iMesActual, 28);
            }


            //Titulo Ventana
            this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;

            //Cargar Rubro
            setCargarRubro();

            //Cargar Grilla
            CargarGrilla();

            // Formateo la grilla
            SetearGrilla();

        }

        #region Metodo setCantidadTotal

        private void setCantidadTotal()
        {
            double cTotal = 0;

            if (this.dgvArtVtas.Rows.Count == 0)            
            {
                this.txtTotal.Text = cTotal.ToString("#0.00");
                return; } 

            foreach (DataGridViewRow row in this.dgvArtVtas.Rows)
            {
                cTotal += Convert.ToDouble(row.Cells["CantidadVendida"].Value.ToString());
            }

            this.txtTotal.Text = cTotal.ToString("#0.00");

        }

        #endregion

        #region Método que carga la grilla

        private void CargarGrilla()
        {
            // Armo la fecha inicial
            string sFechaDesde = dtpDesde.Value.ToShortDateString();
            // Armo la fecha final
            string sFechaHasta = dtpHasta.Value.ToShortDateString();
            //Cadena SQL
            string myCadenaSQL = "";

            //CAMBIAR PUNTERO MOUSE
            Cursor.Current = Cursors.WaitCursor;

            if (clsGlobales.ConB == null)
            {
                if (this.rdbVsitaGral.Checked)
                {
                    // Armo la cadena SQL
                    myCadenaSQL = "exec VtaArticulosGral '" + sFechaDesde
                                + "','" + sFechaHasta + "',0";
                }
                else if (this.rdbRubSub.Checked)
                {
                    if (Convert.ToInt32(cboSubRubro.SelectedValue) != 0)
                    {
                        // Armo la cadena SQL
                        myCadenaSQL = "exec VtaArticulosRubroSubRubro '" + sFechaDesde
                                    + "','" + sFechaHasta + "'," + Convert.ToInt32(cboRubro.SelectedValue.ToString()) + "," + Convert.ToInt32(cboSubRubro.SelectedValue.ToString());
                    }
                    else
                    {
                        // Armo la cadena SQL
                        myCadenaSQL = "exec VtaArticulosRubroSubRubro '" + sFechaDesde
                                    + "','" + sFechaHasta + "'," + Convert.ToInt32(cboRubro.SelectedValue.ToString()) + "," + Convert.ToInt32("0");
                    }
                }
                else if (this.rdbCli.Checked)
                {
                    //Si el CUIT esta vacío...
                    if (!(string.IsNullOrEmpty(txtCuit.Text)))
                    {
                        //Armo la cadena SQL
                        myCadenaSQL = "exec VtaArticulosGral '" + sFechaDesde
                              + "','" + sFechaHasta + "'," + myCliente.Codigo;
                    }
                }

                // Ejecuto la consulta y la paso a la grilla
                DataTable myIvaCompra = clsDataBD.GetSql(myCadenaSQL);
                dgvArtVtas.AutoGenerateColumns = false;
                dgvArtVtas.DataSource = myIvaCompra;

                this.setCantidadTotal();

                //CAMBIAR PUNTERO MOUSE
                Cursor.Current = Cursors.Default;
            }
            else
            {

                if (this.rdbVsitaGral.Checked)
                {
                    // Armo la cadena SQL
                    myCadenaSQL = "exec VentaArticulosCompleto '" + sFechaDesde
                                + "','" + sFechaHasta + "'";
                }

                else if (this.rdbRubSub.Checked)
                {
                    if (Convert.ToInt32(cboSubRubro.SelectedValue) != 0)
                    {
                        // Armo la cadena SQL
                        myCadenaSQL = "exec VentaArticulosCompleto_RS_SR '" + sFechaDesde
                                    + "','" + sFechaHasta + "'," + Convert.ToInt32(cboRubro.SelectedValue.ToString()) + "," + Convert.ToInt32(cboSubRubro.SelectedValue.ToString());
                    }
                    else
                    {
                        // Armo la cadena SQL
                        myCadenaSQL = "exec VentaArticulosCompleto_RS_SR '" + sFechaDesde
                                    + "','" + sFechaHasta + "'," + Convert.ToInt32(cboRubro.SelectedValue.ToString()) + "," + Convert.ToInt32("0");
                    }
                }

                else if (this.rdbCli.Checked)
                {
                    //Si el CUIT esta vacío...
                    if (!(string.IsNullOrEmpty(txtCuit.Text)))
                    {
                        //Armo la cadena SQL
                        myCadenaSQL = "exec VentaArticulosCompletoCliente '" + sFechaDesde
                              + "','" + sFechaHasta + "'," + myCliente.Codigo;
                    }
                }

                // Ejecuto la consulta y la paso a la grilla
                DataTable myIvaCompra = clsDataBD.GetSqlB(myCadenaSQL);
                dgvArtVtas.AutoGenerateColumns = false;
                dgvArtVtas.DataSource = myIvaCompra;

                this.setCantidadTotal();
            }

            
        }

        #endregion

        #endregion

        #region Método para cargar Rubro

        private void setCargarRubro()
        {
            // Cargo el combo de las provincias
            clsDataBD.CargarCombo(cboRubro, "RubrosArticulos", "RubroArticulo", "IdRubroArticulo","Activo=1");
            // Dejo vacío el combo
            cboRubro.SelectedIndex = -1;

        }
        #endregion

        #region Evento SelectedValueChanged de la grilla

        private void cboRubro_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(Convert.ToInt32(cboRubro.SelectedValue) > 0))             
            { 
                return; 
            }

                //Clean Combobox
                cboSubRubro.DataSource = null;
                cboSubRubro.DataBindings.Clear();

                //Cargar la Grilla
                CargarGrilla();

                // Cargo el combo de las Localidades N.
                string strSQL = " IdRubroArticulo = " + cboRubro.SelectedValue + " AND Activo=1";
                clsDataBD.CargarCombo(cboSubRubro, "SubrubrosArticulos", "SubrubroArticulo", "IdSubrubroArticulo", strSQL);

                // Establezco el combo localidad a la primera opcion N.
                cboSubRubro.TabStop = true;
                cboSubRubro.Enabled = true;
                //No mostrar dato alguno
                cboSubRubro.SelectedIndex = -1;
        }

        #endregion

        #region Evento Click Boton Cliente

        private void btnCli_Click(object sender, EventArgs e)
        {
            //Quitar el cliente actualmente selecionado
            EliminarClienteSeleccionado();
            //Buscar Cliente
            frmBuscarCliente myForm = new frmBuscarCliente();
            myForm.ShowDialog();
            //Cliente Nuevo
            this.CargarClienteNuevo();            
            //Cargar Grilla
            this.CargarGrilla();
        }

        #endregion

        #region Metodo EliminarClienteSeleccionado

        //Eliminar el cliente selecionado anteriormente
        private void EliminarClienteSeleccionado()
        {
            // Recorro el vector
            for (int i = 0; i < clsGlobales.ClientesSeleccionados.GetLength(0); i++)
            {
                // Si el Cliente que quiero borrar está en el vector
                if (clsGlobales.ClientesSeleccionados[i] == myCliente.Codigo)
                {
                    // Elimino el proveedor del vector
                    clsGlobales.ClientesSeleccionados[i] = 0;
                    //Limpiar vector Cliente
                    clsGlobales.ClientesSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ClientesSeleccionados, 0);
                    // Salgo del for
                    break;
                }
            }

            // Si ya no queda Cliente seleccionado, vaciar datos.
            if (clsGlobales.ClientesSeleccionados.GetLength(0) == 0)
            {
                this.txtCuit.Text = "";

            }
        }

        #endregion

        #region Método que trae el Cliente para una nueva factura

        private void CargarClienteNuevo(int p_Cliente = 0)
        {
            //Vino un numero de cliente mayor a 0 en el parametro?....
            if (p_Cliente == 0)
            {
                // Si el vector tiene más de un Cliente seleccionado
                if (clsGlobales.ClientesSeleccionados.GetLength(0) > 1)
                {
                    // Informo que solo se puede seleccionar un proveedor
                    MessageBox.Show("Solo puede seleccionar un Cliente!", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Vuelvo a abrir el formulario de búsqueda de proveedores
                    // LLamo al formulario que busca los Clientes
                    frmBuscarCliente myForm = new frmBuscarCliente();
                    myForm.ShowDialog();
                }
            }
            else
            {
                // Redimensiono el tamaño de la matriz
                clsGlobales.ClientesSeleccionados = (int[])clsValida.ResizeVector(clsGlobales.ClientesSeleccionados, clsGlobales.ClientesSeleccionados.Length + 1);
                // A la posición creada le asigno el Id seleccionado
                clsGlobales.ClientesSeleccionados[clsGlobales.ClientesSeleccionados.Length - 1] = p_Cliente;
            }

            //**
            // Si hay algún Cliente seleccionado
            if (clsGlobales.ClientesSeleccionados.GetLength(0) > 0)
            {
                // Recorro el vector hasta que encuentro un Id de Cliente y lo paso a los controles del formulario
                for (int i = 0; i <= clsGlobales.ClientesSeleccionados.GetLength(0); i++)
                {
                    // Si la posición tiene un ID de Cliente, busco los datos del mismo
                    if (clsGlobales.ClientesSeleccionados[0] > 0)
                    {
                        // Cargo los datos del Cliente
                        CargarClientes(clsGlobales.ClientesSeleccionados[0]);
                        // Los paso al formulario
                        PasarDatosAlFormulario();
                    }
                }

            }
        }

        #endregion

        #region PasarDatosAlFormulario

        //PASA LOS DATOS DEL CLIENTE AL FORMULARIO
        private void PasarDatosAlFormulario()
        {
            //Paso los datos del proveedor al formulario  

            clsCUIT oCUIT = new clsCUIT(myCliente.Cuit);

            if (oCUIT.EsValido)
            {
                this.txtCuit.Text = clsGlobales.cFormato.CUITGuiones(this.myCliente.Cuit, 1);
            }
            else
            {
                this.txtCuit.Text = myCliente.Cuit; 
            }
        }

        #endregion

        #region Método que carga los datos de los Clientes a la clase

        private void CargarClientes(int Id)
        {
            // Armo la cadena SQL
            string myCadenaSQL = "select * from Vista_Clientes where Codigo = " + Id;
            // Creo una tabla que me va a almacenar el resultado de la consulta
            DataTable myTablaClientes = clsDataBD.GetSql(myCadenaSQL);
            // Recorro la tabla y le asigo los datos del proveedor al objeto
            foreach (DataRow rowCli in myTablaClientes.Rows)
            {
                myCliente.Codigo = Convert.ToInt32(rowCli["Codigo"]);

                myCliente.RazonSocial = rowCli["RazonSocial"].ToString();
                myCliente.Cuit = rowCli["Cuit"].ToString();
                myCliente.Direccion = rowCli["Direccion"].ToString();
                myCliente.IdCondicionIva = Convert.ToInt32(rowCli["IdTipo"].ToString());
                myCliente.CP = rowCli["CP"].ToString();

                myCliente.IdTransporte = Convert.ToInt32(rowCli["IdTransporte"].ToString());
                myCliente.IdCondicionCompra = Convert.ToInt32(rowCli["IdCondicionCompra"].ToString());

                if (!(string.IsNullOrEmpty(rowCli["Barrio"].ToString())))
                {
                    myCliente.Barrio = rowCli["Barrio"].ToString();
                }
                else { myCliente.Barrio = ""; }

                myCliente.IdLocalidad = Convert.ToInt32(rowCli["IdLocalidad"].ToString());
                myCliente.Localidad = rowCli["Localidad"].ToString();

                myCliente.IdProvincia = Convert.ToInt32(rowCli["IdProvincia"].ToString());
                myCliente.Provincia = rowCli["Provincia"].ToString();

                myCliente.Telefono = rowCli["Telefono"].ToString();


                if (!(string.IsNullOrEmpty(rowCli["Celular"].ToString())))
                {
                    myCliente.Celular = rowCli["Celular"].ToString();
                }
                else { myCliente.Celular = ""; }


                if (!(string.IsNullOrEmpty(rowCli["Fax"].ToString())))
                {
                    myCliente.Fax = rowCli["Fax"].ToString();
                }
                else { myCliente.Fax = ""; }


                if (!(string.IsNullOrEmpty(rowCli["Mail"].ToString())))
                {
                    myCliente.Mail = rowCli["Mail"].ToString();
                }
                else { myCliente.Mail = ""; }


                if (!(string.IsNullOrEmpty(rowCli["Web"].ToString())))
                {
                    myCliente.Web = rowCli["Web"].ToString();
                }
                else { myCliente.Web = ""; }


                myCliente.IdTipoCliente = Convert.ToInt32(rowCli["IdTipo"].ToString());

                myCliente.IdCondicionIva = Convert.ToInt32(rowCli["IdCondicionIva"].ToString());

                if (!(string.IsNullOrEmpty(rowCli["Observaciones"].ToString())))
                {
                    myCliente.Observaciones = rowCli["Observaciones"].ToString();
                }
                else { myCliente.Observaciones = ""; }


                if (clsGlobales.cValida.EsFecha(rowCli["Nacimiento"].ToString()))
                {
                    myCliente.Nacimiento = rowCli["Nacimiento"].ToString();
                }
                else { myCliente.Nacimiento = ""; }


                if (clsGlobales.cValida.EsFecha(rowCli["Alta"].ToString()))
                {
                    myCliente.Alta = rowCli["Alta"].ToString();
                }
                else { myCliente.Alta = ""; }


            }
        }

        #endregion

        #region Evento ChekedChanged del Radio Rubros-SubRubros

        private void rdbRubSub_CheckedChanged(object sender, EventArgs e)
        {
            //Si esta activo el check...
            if (rdbRubSub.Checked)
            {
                this.grpRubroSubRubro.Enabled=true;
                cboRubro.Focus(); 
            }
            else
            {
                cboRubro.SelectedIndex = -1;
                cboSubRubro.SelectedIndex = -1;
                this.grpRubroSubRubro.Enabled=false;           
            }
        }

        #endregion

        #region Evento ChekedChanged del Radio Clientes

        private void rdbCli_CheckedChanged(object sender, EventArgs e)
        {
            //Si esta activo el check...
            if (rdbCli.Checked)
            { 
                this.grpClientes.Enabled = true;
                btnCli.Focus();
            }
            else
            {
                this.txtCuit.Text ="";
                this.grpClientes.Enabled = false;
            }
        }

        #endregion

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            if (clsGlobales.cValida.EsFecha(dtpDesde.Value.ToString()))
            {
                this.CargarGrilla();
            }
        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
         
            if (clsGlobales.cValida.EsFecha(dtpDesde.Value.ToString()))
            {
                this.CargarGrilla();
            }
        }

        private void cboSubRubro_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboSubRubro.SelectedIndex != -1)
            {
                this.CargarGrilla();
            }
        }

        private void rdbVsitaGral_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdbVsitaGral.Checked)
            {
                this.CargarGrilla();
            }

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            // Armo la fecha inicial
            string sFechaDesde = clsValida.ConvertirFecha(dtpDesde.Value);
            // Armo la fecha final
            string sFechaHasta = clsValida.ConvertirFecha(dtpHasta.Value);

            //Data Set
            dsReportes oDsArt = new dsReportes();
            
            if (clsGlobales.ConB == null)
            {
                //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
                int dgvFilas = dgvArtVtas.Rows.Count;

                for (int i = 0; i < dgvFilas; i++)
                {

                    oDsArt.Tables["dtInfVtasArt"].Rows.Add
                    (new object[] 
                        { 
                            dgvArtVtas["CodArt",i].Value.ToString(),
                            dgvArtVtas["Descripcion",i].Value.ToString(),
                            dgvArtVtas["Rubro",i].Value.ToString(),
                            dgvArtVtas["SubRubro",i].Value.ToString(),
                            "0",
                            "0",
                            dgvArtVtas["CantidadVendida",i].Value.ToString(),
                            
                        }
                    );
                }

                //Objeto Reporte
                rptInfoVtaArt1 oRepArt = new rptInfoVtaArt1();
                //Cargar Reporte            
                //    oRepTipoCli.Load(Application.StartupPath + "\\rptTipoCli.rpt");
                oRepArt.Load(Application.StartupPath + "\\rptInfoVtaArt1.rpt");
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
                oRepArt.DataDefinition.FormulaFields["Total"].Text = "'" + txtTotal.Text + "'";
                oRepArt.DataDefinition.FormulaFields["Desde"].Text = "'" + sFechaDesde + "'";
                oRepArt.DataDefinition.FormulaFields["Hasta"].Text = "'" + sFechaHasta + "'";

                if (this.rdbVsitaGral.Checked)
                {
                    oRepArt.DataDefinition.FormulaFields["Titulo"].Text = "'GENERAL'";
                }
                else if (this.rdbRubSub.Checked)
                {
                    if (Convert.ToInt32(cboSubRubro.SelectedValue) != 0)
                    {
                        oRepArt.DataDefinition.FormulaFields["Titulo"].Text = "'POR SUBRUBROS'";
                    }
                    else
                    {
                        oRepArt.DataDefinition.FormulaFields["Titulo"].Text = "'POR RUBROS'";
                    }
                }

                //Si el CUIT esta vacío...
                if (!(string.IsNullOrEmpty(txtCuit.Text)))
                {
                    oRepArt.DataDefinition.FormulaFields["Cliente"].Text = "'" + myCliente.Codigo + " - " + myCliente.RazonSocial + "'";
                    oRepArt.DataDefinition.FormulaFields["Titulo"].Text = "'POR CLIENTE'";
                }
                else
                {
                    oRepArt.DataDefinition.FormulaFields["Cliente"].Text = "'No seleccionado'";
                }

                //Mostrar el reporte  
                frmShowReports myReportForm = new frmShowReports();
                myReportForm.Text = this.Text;
                myReportForm.ShowDialog();
            }
            else
            {
                
                //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
                int dgvFilas = dgvArtVtas.Rows.Count;

                for (int i = 0; i < dgvFilas; i++)
                {

                    oDsArt.Tables["dtInfVtasArt"].Rows.Add
                    (new object[] 
                        { 
                            dgvArtVtas["CodArt",i].Value.ToString(),
                            dgvArtVtas["Descripcion",i].Value.ToString(),
                            dgvArtVtas["Rubro",i].Value.ToString(),
                            dgvArtVtas["SubRubro",i].Value.ToString(),
                            dgvArtVtas["Cantidad_B",i].Value.ToString(),
                            dgvArtVtas["Cantidad_N",i].Value.ToString(),
                            dgvArtVtas["CantidadVendida",i].Value.ToString(),
                            
                        }
                    );
                }

                //Objeto Reporte
                rptInfoVtaArt2 oRepArt = new rptInfoVtaArt2();
                //Cargar Reporte            
                //    oRepTipoCli.Load(Application.StartupPath + "\\rptTipoCli.rpt");
                oRepArt.Load(Application.StartupPath + "\\rptInfoVtaArt2.rpt");
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
                oRepArt.DataDefinition.FormulaFields["Total"].Text = "'" + txtTotal.Text + "'";
                oRepArt.DataDefinition.FormulaFields["Desde"].Text = "'" + sFechaDesde + "'";
                oRepArt.DataDefinition.FormulaFields["Hasta"].Text = "'" + sFechaHasta + "'";

                if (this.rdbVsitaGral.Checked)
                {
                    oRepArt.DataDefinition.FormulaFields["Titulo"].Text = "'GENERAL'";
                }
                else if (this.rdbRubSub.Checked)
                {
                    if (Convert.ToInt32(cboSubRubro.SelectedValue) != 0)
                    {
                        oRepArt.DataDefinition.FormulaFields["Titulo"].Text = "'POR SUBRUBROS'";
                    }
                    else
                    {
                        oRepArt.DataDefinition.FormulaFields["Titulo"].Text = "'POR RUBROS'";
                    }
                }

                //Si el CUIT esta vacío...
                if (!(string.IsNullOrEmpty(txtCuit.Text)))
                {
                    oRepArt.DataDefinition.FormulaFields["Cliente"].Text = "'" + myCliente.Codigo + " - " + myCliente.RazonSocial + "'";
                    oRepArt.DataDefinition.FormulaFields["Titulo"].Text = "'POR CLIENTE'";
                }
                else
                {
                    oRepArt.DataDefinition.FormulaFields["Cliente"].Text = "'No seleccionado'";
                }

                //Mostrar el reporte  
                frmShowReports myReportForm = new frmShowReports();
                myReportForm.Text = this.Text;
                myReportForm.ShowDialog();
            }
            
            
        }

        #region Método que setea la grilla según desde donde se llama

        private void SetearGrilla()
        {
            // Formateo para la grilla en blanco
            if (clsGlobales.ConB == null)
            {
                // Anchos de columnas
                dgvArtVtas.Columns["Descripcion"].Width = 340;
                dgvArtVtas.Columns["Rubro"].Width = 200;
                dgvArtVtas.Columns["SubRubro"].Width = 200;
                dgvArtVtas.Columns["CantidadVendida"].Width = 80;
                // Visibilidad
                dgvArtVtas.Columns["Cantidad_B"].Visible = false;
                dgvArtVtas.Columns["Cantidad_N"].Visible = false;


            }
            // Formateo para el negro
            else
            {
                // Anchos de columnas
                dgvArtVtas.Columns["Descripcion"].Width = 260;
                dgvArtVtas.Columns["Rubro"].Width = 170;
                dgvArtVtas.Columns["SubRubro"].Width = 170;
                dgvArtVtas.Columns["Cantidad_B"].Width = 75;
                dgvArtVtas.Columns["Cantidad_N"].Width = 75;
                dgvArtVtas.Columns["CantidadVendida"].Width = 75;
                // Visibilidad
                dgvArtVtas.Columns["Cantidad_B"].Visible = true;
                dgvArtVtas.Columns["Cantidad_N"].Visible = true;
            }
        }

        #endregion

        #region Evento Click del botón Salir

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Vacío el vector global de clientes seleccionados
            EliminarClienteSeleccionado();

            // Cierro el form
            this.Close();
        }

        #endregion
    }
}

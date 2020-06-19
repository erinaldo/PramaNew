using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prama.Formularios.Informes
{
    public partial class frmVtasRubSubProdCli : Form
    {
        public frmVtasRubSubProdCli()
        {
            InitializeComponent();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string strTipoCli = "";

            //Hay datos?
            if (!(dvgData.Rows.Count > 0))
            {
                MessageBox.Show("No hay datos para imprimir para la consulta especificada!", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //PREGUNTAR SI ESTA CONFIGURADO EN PARAMETROS
            if (clsGlobales.cParametro.Imprimir)
            {
                DialogResult dlResult = MessageBox.Show("¿Desea imprimir el Informe de Ventas por Tipo de Cliente, Rubro-SubRubro y Período?", "Confirmar!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Si confirma... cambiar estado
                if (dlResult == DialogResult.No)
                {
                    return;
                }
            }

            //Data Set
            dsReportes oDsVtaSubRubCli = new dsReportes();

            //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
            int dgvFilas = this.dvgData.Rows.Count;

            for (int i = 0; i < dgvFilas; i++)
            {
                oDsVtaSubRubCli.Tables["dtVtaSubRubCli"].Rows.Add
                (new object[] { dvgData[0,i].Value.ToString(),
                    dvgData[4,i].Value.ToString(), 
                    dvgData[6,i].Value.ToString(),
                    dvgData[7,i].Value.ToString(),
                    dvgData[9,i].Value.ToString(),
                    dvgData[10,i].Value.ToString(),
                    dvgData[11,i].Value.ToString()});
            }

            //Objeto Reporte
            rptVtasRubSubTipoCli oRepVtaSubRubCli = new rptVtasRubSubTipoCli();

            //Cargar Reporte                                    
            oRepVtaSubRubCli.Load(Application.StartupPath + "\\rptVtasRubSubTipoCli.rpt");

            //Establecer el DataSet como DataSource
            oRepVtaSubRubCli.SetDataSource(oDsVtaSubRubCli);

            //Pasar como parámetro nombre del reporte
            clsGlobales.myRptDoc = oRepVtaSubRubCli;

            oRepVtaSubRubCli.DataDefinition.FormulaFields["vendedor"].Text = "'" + clsGlobales.UsuarioLogueado.Usuario + "'";
            oRepVtaSubRubCli.DataDefinition.FormulaFields["linea-01"].Text = "'" + clsGlobales.cParametro.NombreFantasia + "'";
            oRepVtaSubRubCli.DataDefinition.FormulaFields["linea-02"].Text = "' Dirección: " + clsGlobales.cParametro.Direccion + "'";
            oRepVtaSubRubCli.DataDefinition.FormulaFields["linea-03"].Text = "' Localidad: " + clsGlobales.cParametro.Localidad + "'"; ;
            oRepVtaSubRubCli.DataDefinition.FormulaFields["linea-04"].Text = "' Teléfono : " + clsGlobales.cParametro.Telefono + "'";
            oRepVtaSubRubCli.DataDefinition.FormulaFields["linea-05"].Text = "' CUIT: " + clsGlobales.cParametro.CUIT + "'";
            oRepVtaSubRubCli.DataDefinition.FormulaFields["linea-06"].Text = "' Mail: " + clsGlobales.cParametro.Mail + "'";
            oRepVtaSubRubCli.DataDefinition.FormulaFields["linea-07"].Text = "' Web : " + clsGlobales.cParametro.Web + "'";

            //Data Cliente
            if (this.rbnAll.Checked) { strTipoCli = "TODOS"; }
            if (this.rbnPub.Checked) { strTipoCli = "PUBLICO"; }
            if (this.rbnDist.Checked) { strTipoCli = "DISTRIBUIDOR"; }
            if (this.rbnRev.Checked) { strTipoCli = "REVENDEDOR"; }

            string sRubro = cboRubro.Text;
            string sSubRubro = cboSubRubro.Text;

            oRepVtaSubRubCli.DataDefinition.FormulaFields["cliente"].Text = "' Tipo de Cliente : " + strTipoCli + ", Período DESDE: " + this.dtDesde.Value.ToString("dd/MM/yyyy") + " -  HASTA: " + this.dtHasta.Value.ToString("dd/MM/yyyy") + " - RUBRO: " + sRubro + " - SUBRUBRO: " + sSubRubro + " - ID CLIENTE: " + this.txtCodigoBs.Text + "'";
            oRepVtaSubRubCli.DataDefinition.FormulaFields["totalcant"].Text = "'" + this.txtCantTotal.Text + "'";
            oRepVtaSubRubCli.DataDefinition.FormulaFields["total"].Text = "'" + this.txtTotGral.Text + "'";

            //Mostrar el reporte  
            frmShowReports myReportForm = new frmShowReports(2);
            myReportForm.Text = this.Text;
            myReportForm.ShowDialog(); 
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmVtasRubSubProdCli_Load(object sender, EventArgs e)
        {
            // Variable que almacena el mes en curso
            int iMesActual = DateTime.Now.Month;
            // Variable que almacena el año en curso
            int iAnoActual = DateTime.Now.Year;

			//icon
            clsFormato.SetIconForm(this); 
			
            // Seteo el DTP de fecha incial
            dtDesde.Value = new DateTime(iAnoActual, iMesActual, 1);
            // Seteo el DTP de fecha Final
            if (!(iMesActual == 2))
            {
                dtHasta.Value = new DateTime(iAnoActual, iMesActual, 30);
            }
            else
            {
                dtHasta.Value = new DateTime(iAnoActual, iMesActual, 28);
            }

            //Rubro
            setCargarRubro();

            //Clear
            cboRubro.SelectedIndex = -1;
            cboSubRubro.SelectedIndex = -1;
            cboProvincia.SelectedIndex = -1;
            
            //Grilla
            CargarGrilla();

            //Titulo
            this.Text = clsGlobales.cFormato.getTituloVentana() + "  - DETALLE DE VENTAS POR TIPO CLIENTE, RUBRO-SUBRUBRO Y PERIODO ";
        }

        #region Método para cargar Rubro

        private void setCargarRubro()
        {
            // Cargo el combo de las provincias
            clsDataBD.CargarCombo(cboRubro, "RubrosArticulos", "RubroArticulo", "IdRubroArticulo", "Activo=1");
            // Dejo vacío el combo
            cboRubro.SelectedIndex = -1;
            // Cargo el combo de las provincias
            clsDataBD.CargarCombo(cboProvincia, "Provincias", "Provincia", "IdProvincia");
            // Dejo vacío el combo
            cboProvincia.SelectedIndex = -1;

        }
        #endregion

        #region CargarGrilla()


        //Carga la grilla con los datos de movimientos en la cta cte del cliente
        private void CargarGrilla()
        {
            //Auxiliar String
            string myCadSQL = "";

            DataTable myData = new DataTable();

            int IdCli = 0;
            int valCboRubro = 0;
            int valCboSubRubro = 0;


            //CAMBIAR PUNTERO MOUSE
            Cursor.Current = Cursors.WaitCursor;

            //Validar
            if (clsGlobales.cValida.IsNumeric(this.txtCodigoBs.Text))
            {
                IdCli = Convert.ToInt32(txtCodigoBs.Text);
            }
            else if (string.IsNullOrEmpty(this.txtCodigoBs.Text))
            {
                IdCli = 0;
            }


            if (cboRubro.SelectedIndex != -1)
            {
                valCboRubro = Convert.ToInt32(cboRubro.SelectedValue);
            }

            if (cboSubRubro.SelectedIndex != -1)
            {
                valCboSubRubro = Convert.ToInt32(cboSubRubro.SelectedValue);
            }

            ////

            //Llamada al procedimiento
            if (clsGlobales.ConB == null)
            {
                //if (this.rbnAll.Checked) { myCadSQL = "exec getVtasRubSubProdCli 0," + valCboRubro.ToString() + "," + valCboSubRubro.ToString() + ",'" + this.dtDesde.Value.ToShortDateString() + "','" + this.dtHasta.Value.ToShortDateString() + "'," + IdCli.ToString(); }
                //if (this.rbnPub.Checked) { myCadSQL = "exec getVtasRubSubProdCli 28," + valCboRubro.ToString() + "," + valCboSubRubro.ToString() + ",'" + this.dtDesde.Value.ToShortDateString() + "','" + this.dtHasta.Value.ToShortDateString() + "'," + IdCli.ToString(); }
                //if (this.rbnDist.Checked) { myCadSQL = "exec getVtasRubSubProdCli 29," + valCboRubro.ToString() + "," + valCboSubRubro.ToString() + ",'" + this.dtDesde.Value.ToShortDateString() + "','" + this.dtHasta.Value.ToShortDateString() + "'," + IdCli.ToString(); }
                //if (this.rbnRev.Checked) { myCadSQL = "exec getVtasRubSubProdCli 30," + valCboRubro.ToString() + "," + valCboSubRubro.ToString() + ",'" + this.dtDesde.Value.ToShortDateString() + "','" + this.dtHasta.Value.ToShortDateString() + "'," + IdCli.ToString(); }


                string sFiltro = "";

                //Tipo Cliente
                if (!(this.rbnAll.Checked))
                {
                    if (this.rbnPub.Checked) { sFiltro = sFiltro + " TipoCliente = 28 "; }
                    if (this.rbnDist.Checked) { sFiltro = sFiltro + " TipoCliente = 29 "; }
                    if (this.rbnRev.Checked) { sFiltro = sFiltro + " TipoCliente = 30 "; }
                }
                else
                {
                    sFiltro = sFiltro + " TipoCliente in (28,29,30) ";
                }

                //Rubro
                if (!(valCboRubro == 0))
                {
                    sFiltro = sFiltro + " AND IdRubroArticulo = " + valCboRubro;
                }

                //SubRubro
                if (!(valCboSubRubro == 0))
                {
                    sFiltro = sFiltro + " AND IdSubRubroArticulo = " + valCboSubRubro;
                }

                //Fechas
                sFiltro = sFiltro + " AND (Fecha_Original>= '" + this.dtDesde.Value.ToShortDateString() + "') AND (Fecha_Original<='" + this.dtHasta.Value.ToShortDateString() + "') ";

                //Cliente
                if (!(IdCli == 0))
                {
                    sFiltro = sFiltro + " AND IdCliente = " + IdCli;
                }

                if (cboProvincia.SelectedIndex != -1)
                {
                    sFiltro += " and IdProvincia = " + Convert.ToInt32(cboProvincia.SelectedValue);
                }

                myCadSQL = "Select * from Vista_VtasRubSubProdCli_W WHERE " + sFiltro;

                //Cargar SQL            
                myData = clsDataBD.GetSql(myCadSQL);
            
            }
            else
            {

                string sFiltro = "";

                //Tipo Cliente
                if (!(this.rbnAll.Checked))
                {
                    if (this.rbnPub.Checked) { sFiltro = sFiltro + " TipoCliente = 28 "; }
                    if (this.rbnDist.Checked) { sFiltro = sFiltro + " TipoCliente = 29 "; }
                    if (this.rbnRev.Checked) { sFiltro = sFiltro + " TipoCliente = 30 "; }
                }
                else
                {
                    sFiltro = sFiltro + " TipoCliente in (28,29,30) ";
                }

                //Rubro
                if (!(valCboRubro == 0))
                {
                    sFiltro = sFiltro + " AND IdRubroArticulo = " + valCboRubro;
                }

                //SubRubro
                if (!(valCboSubRubro == 0))
                {
                    sFiltro = sFiltro + " AND IdSubRubroArticulo = " + valCboSubRubro;
                }

                //Fechas
                sFiltro = sFiltro + " AND (Fecha_Original>= '" + this.dtDesde.Value.ToShortDateString() + "') AND (Fecha_Original<='" + this.dtHasta.Value.ToShortDateString() + "') ";

                //Cliente
                if (!(IdCli == 0))
                {
                    sFiltro = sFiltro + " AND IdCliente = " + IdCli;
                }

                if (cboProvincia.SelectedIndex != -1)
                {
                    sFiltro += " and IdProvincia = " + Convert.ToInt32(cboProvincia.SelectedValue);
                }

                myCadSQL = "Select * from Vista_VtasRubSubProdCli WHERE " + sFiltro;
                
                //Cargar SQL            
                myData = clsDataBD.GetSqlB(myCadSQL);

            }

            //Properties
            this.dvgData.AutoGenerateColumns = false;
            this.dvgData.DataSource = myData;

            //Hay datos?
            double totcant = 0;
            double totgral = 0;

            foreach (DataRow row in myData.Rows)
            {
                totcant += Convert.ToDouble(row["Cantidad"]);
                totgral += Convert.ToDouble(row["Total"]);
            }

            //Show
            this.txtCantTotal.Text = totcant.ToString("#0");
            this.txtTotGral.Text = totgral.ToString("###,###,##0.00");

            //CAMBIAR PUNTERO MOUSE
            Cursor.Current = Cursors.Default;
        }

        #endregion

        private void dtDesde_ValueChanged(object sender, EventArgs e)
        {
            if (clsGlobales.cValida.EsFecha(this.dtDesde.Value.ToShortDateString()))
            {
                this.CargarGrilla();
            }
        }

        private void dtHasta_ValueChanged(object sender, EventArgs e)
        {
            if (clsGlobales.cValida.EsFecha(this.dtHasta.Value.ToShortDateString()))
            {
                this.CargarGrilla();
            }
        }

        private void cboRubro_SelectedIndexChanged(object sender, EventArgs e)
        {
                this.CargarGrilla();
        }

        private void cboSubRubro_SelectedValueChanged(object sender, EventArgs e)
        {
                this.CargarGrilla();
        }

        private void rbnPub_CheckedChanged(object sender, EventArgs e)
        {
                this.CargarGrilla();
        }

        private void rbnDist_CheckedChanged(object sender, EventArgs e)
        {
                this.CargarGrilla();
        }

        private void rbnRev_CheckedChanged(object sender, EventArgs e)
        {
                this.CargarGrilla();
        }

        private void txtCodigoBs_TextChanged(object sender, EventArgs e)
        {
             this.CargarGrilla();
        }

        private void cboRubro_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!(Convert.ToInt32(cboRubro.SelectedValue) > 0))
            {

                //Clean Combobox
                cboSubRubro.DataSource = null;
                cboSubRubro.DataBindings.Clear();

                //No mostrar dato alguno
                cboSubRubro.SelectedIndex = -1;

                // Establezco el combo localidad a la primera opcion N.
                cboSubRubro.TabStop = false;
                cboSubRubro.Enabled = false;

                //Retornar
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

        private void btnCli_Click(object sender, EventArgs e)
        {
            //Clear
            this.cboRubro.SelectedIndex = -1;
            this.cboSubRubro.SelectedIndex = -1;
            this.cboProvincia.SelectedIndex = -1;
            //Re cargar
            this.CargarGrilla();

        }

        private void cboProvincia_SelectedValueChanged(object sender, EventArgs e)
        {
            this.CargarGrilla();
        }
    }
}

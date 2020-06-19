using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prama.Formularios.Caja
{
    public partial class frmChequesEnCartera : Form
    {
        public frmChequesEnCartera()
        {
            InitializeComponent();
        }

        private void CargarGrilla()
        {
            string sCadena = "select * from Cheques where Activo = 1 and EnCartera = 1";
            DataTable myDT = new DataTable();

            if (clsGlobales.ConB == null)
            {
                myDT = clsDataBD.GetSql(sCadena);
            }
            else
            {
                myDT = clsDataBD.GetSqlB(sCadena);
            }

            dgvCheques.DataSource = myDT;
        }

        private void frmChequesEnCartera_Load(object sender, EventArgs e)
        {
			
			//icon
            clsFormato.SetIconForm(this); 
			//Grilla
			dgvCheques.AutoGenerateColumns = false;
            CargarGrilla();

            //string sWhereCombo = "";
            string sMyCadenaSQL = "";

            //Recuperar data temporal
            sMyCadenaSQL = "select * from Temporal_DetalleCheques where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario;
            // Traigo los datos de la tabla que contiene los saldos de las cajas
            DataTable myTable = new DataTable(); 
            if (clsGlobales.ConB == null) { myTable = clsDataBD.GetSql(sMyCadenaSQL); } else { myTable = clsDataBD.GetSqlB(sMyCadenaSQL); }
            //dgvImputacion.DataSource = myTable;

            int filas = 0;

            //VER SI HAY DATOS
            if (myTable.Rows.Count > 0)
            {
                foreach (DataRow myRow in myTable.Rows)
                {
                    // Cuento las filas de la grilla
                    filas = dgvCheques.Rows.Count;

                    //Buscar el Id de Cheque temporal en la grilla cargada y marcarlo
                    foreach (DataGridViewRow myRows in dgvCheques.Rows)
                    {
                        if (Convert.ToInt32(myRow["Numero"].ToString())==Convert.ToInt32(myRows.Cells["Numero"].Value.ToString()))
                        {
                            myRows.Cells["Elegido"].Value = true;
                            break;
                        } 
                    }
                }
            }

            //Calcular Total
            CalcularTotal();

            //Controlar botones
            if (dgvCheques.Rows.Count > 0)
            {
                this.btnAgregar.Enabled = true;
                this.btnQuitar.Enabled = true;
                btnAceptar.Enabled = true;
            }
            else
            {
                btnAceptar.Enabled = false;
            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            dgvCheques.CurrentRow.Cells["Elegido"].Value = true;
            CalcularTotal();
            // Inhabilito el botón agregar y habilito el quitar
            btnAgregar.Enabled = false;
            btnQuitar.Enabled = true;
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            dgvCheques.CurrentRow.Cells["Elegido"].Value = false;
            CalcularTotal();
            // Inhabilito el botón agregar y habilito el quitar
            btnAgregar.Enabled = true;
            btnQuitar.Enabled = false;
        }

        private void CalcularTotal()
        {
            double dTotal = 0;

            foreach (DataGridViewRow row in dgvCheques.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Elegido"].Value))
                {
                    dTotal += Convert.ToDouble(row.Cells["Importe"].Value);
                }
                
            }

            txtTotal.Text = dTotal.ToString("#0.00");
        }

        private void dgvCheques_SelectionChanged(object sender, EventArgs e)
        {
            // SI el articulo esta o no seleccionado cambio el enabled de los botones
            if (Convert.ToBoolean(dgvCheques.CurrentRow.Cells["Elegido"].Value) == true)
            {
                btnAgregar.Enabled = false;
                btnQuitar.Enabled = true;
            }
            else
            {
                btnQuitar.Enabled = false;
                btnAgregar.Enabled = true;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            clsGlobales.dTotalAAcreditar = Convert.ToDouble(txtTotal.Text);
            GrabarTemporal();
            this.Close();
        }

        private void GrabarTemporal()
        {

            //Eliminar registros cargados para ese usuario y tipo de movimiento

            if (clsGlobales.ConB == null)
            { clsDataBD.GetSql("Delete from Temporal_DetalleCheques Where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario); }
            else
            { clsDataBD.GetSqlB("Delete from Temporal_DetalleCheques Where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario); }


            //Volver a guardar N. 08-11
            foreach (DataGridViewRow row in dgvCheques.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Elegido"].Value))
                {
                    int iNumero = Convert.ToInt32(row.Cells["Numero"].Value);
                    string sFechaEmision = row.Cells["FechaEmision"].Value.ToString();
                    string sFechaCobro = row.Cells["FechaCobro"].Value.ToString();
                    double dImporte = Convert.ToDouble(row.Cells["Importe"].Value);
                    string sBanco = row.Cells["Banco"].Value.ToString();

                    string sMyCadenaSQL = "insert into Temporal_DetalleCheques (Numero, FechaEmision, FechaCobro, Importe, Activo, Banco, EnCartera, IdUsuario) values (" +
                                            iNumero + ", '" +
                                            sFechaEmision + "', '" +
                                            sFechaCobro + "', " +
                                            dImporte + ", 1, '" +
                                            sBanco + "', 1 , " +
                                            clsGlobales.UsuarioLogueado.IdUsuario + ")";

                    //Controlar tipo de conexion
                    if (clsGlobales.ConB == null)
                    {
                        clsDataBD.GetSql(sMyCadenaSQL);
                    }
                    else
                    {
                        clsDataBD.GetSqlB(sMyCadenaSQL);
                    }
                }
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //ELIMINAR TODOs LOS MOVIMIENTOS SEGUN TIPO Y USUARIO
            string sSQLCad = "DELETE FROM Temporal_DetalleCheques Where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario;
            if (clsGlobales.ConB == null) { clsDataBD.GetSql(sSQLCad); } else { clsDataBD.GetSqlB(sSQLCad); }
            //TOTAL ACREDITAR 0
            clsGlobales.dTotalAAcreditar = 0;

            //CERRAR FORMULARIO
            this.Close();
        }

        private void txtTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

    }
}

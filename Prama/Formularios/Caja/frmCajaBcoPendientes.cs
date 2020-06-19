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
    public partial class frmCajaBcoPendientes : Form
    {
        int iTipoMov = 0;

        public frmCajaBcoPendientes(int p_iTipoMov)
        {
            InitializeComponent();
            iTipoMov = p_iTipoMov;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void frmCajaBcoPendientes_Load(object sender, EventArgs e)		
        {
			//icon
            clsFormato.SetIconForm(this); 
			
            this.Text = clsGlobales.cFormato.getTituloVentana() + " - DETALLE CAJA BANCOS PENDIENTES ";

            CargarGrilla(" WHERE IdTipoMovimiento = " + iTipoMov);
        }

         private void CargarGrilla(string sWhere = "")
        {
            DataTable mDtTable = new DataTable();
            string myCadenaSQL = "";

            myCadenaSQL = "select * from Vista_CajaBcosPendientes " + sWhere;
            if (clsGlobales.ConB == null)
            {
               mDtTable = clsDataBD.GetSql(myCadenaSQL);
            }
            else
            {
               mDtTable = clsDataBD.GetSqlB(myCadenaSQL);
            }

            // Evito que el dgv genere columnas automáticas
            dgvCaja.AutoGenerateColumns = false;
            // Asigno los datos que me devuelve el método GetSql a la grilla del formulario. G.
            dgvCaja.DataSource = mDtTable;
            // Cuento la cantidad de filas de la grilla
            int Filas = dgvCaja.Rows.Count;
            // Posiciono la grilla en la última fila, si hay
            if (Filas > 0)
            {
                //Posicionamiento grilla
                int r = Filas - 1;
                int c = dgvCaja.CurrentCell.ColumnIndex;
                dgvCaja.CurrentCell = dgvCaja.Rows[r].Cells[c];
                
                //Botones
                btnQuitar.Enabled = true;
                btnAgregar.Enabled = true;
                btnImprimir.Enabled = true;
                btnAceptar.Enabled = true;
            }
            else
            {
                //Botones
                btnQuitar.Enabled = false;
                btnAgregar.Enabled = false;
                btnImprimir.Enabled = false;
                btnAceptar.Enabled = false;
            }

            //LABEL
            if (iTipoMov == 2)
            { this.lblSdoAcred.Text = "Saldo a acreditar (MP): "; }
            else
            { this.lblSdoAcred.Text = "Saldo a acreditar (Crédito): "; }

            //TOTALES
            this.CalcularTotales();
            this.CalcularTotAcred();
        }

         #region Método que calcula los totales de la caja

         private void CalcularTotales()
         {
             // Variables del método
             double dSaldo = 0;

             //// Traigo los datos de la tabla que contiene los saldos de las cajas
             //string myCadenaSaldos = "select * from CajaSaldos";
             //// Paso los datos a una tabla
             //DataTable myTable = clsDataBD.GetSql(myCadenaSaldos);
             // recorro la tabla y paso los dato a las variables
             foreach (DataGridViewRow myRow in dgvCaja.Rows)
             {
                 dSaldo += Convert.ToDouble(myRow.Cells["Importe"].Value);
             }

             txtSaldoBcoPendiente.Text = dSaldo.ToString("#0.00");

             // Color de las letras
             if (dSaldo < 0)
             {
                 txtSaldoBcoPendiente.ForeColor = Color.Red;
             }
             else
             {
                 txtSaldoBcoPendiente.ForeColor = Color.Black;
             }
         }

         #endregion

        //METODO CALCULAR TOTAL A ACREDITAR
         private void CalcularTotAcred()
         {
             //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
             int dgvFilas = dgvCaja.Rows.Count;
             double dSdoAcred = 0;
   
             //VERIFICAR SI SE HA DESELECCIONADO ALGO DE LA GRILLA
             for (int i = 0; i < dgvFilas; i++)
             {
                 if ((Convert.ToBoolean(dgvCaja[8, i].Value.ToString()) == false))
                 {
                    dSdoAcred+= Convert.ToDouble(dgvCaja[7, i].Value.ToString()); 
                 }
             }

             txtSdoAcred.Text = dSdoAcred.ToString("#0.00");
         }


         private void btnAceptar_Click(object sender, EventArgs e)
         {
             //Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.    
             int dgvFilas = dgvCaja.Rows.Count;
             bool bPaseACaja = false;

             //VERIFICAR SI SE HA DESELECCIONADO ALGO DE LA GRILLA
             for (int i = 0; i < dgvFilas; i++)
             {
                 if (Convert.ToBoolean(dgvCaja[8, i].Value.ToString()) == false)
                 {
                     bPaseACaja = true;
                     break; 
                 }
             }

             //NADA DESELECCIONADO DE LOS CHEQUES EN CARTERA?
             if (bPaseACaja == false)
             {
                MessageBox.Show("No ha destilado ninguno de los Movimientos que esta 'Pendiente' para enviarlo a la Caja correspondiente!", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
             }

             int iAsocioacion = 0;
             string myCadenaCaja = "";
             string myCadenaSQL = "";
             int iIdCaja = 0;

             double Monto = Convert.ToDouble(txtSdoAcred.Text);
             double Debito = 0;
             double Credito = 0;
             double Efectivo = 0;
             double Transferencia = 0;
             double MP = 0;
             double Contra = 0;
             double Cheques = 0;
             double bancos = 0;

             DateTime Fecha = DateTime.Now;

             if (iTipoMov == 2)
             { MP = Monto; }
             else
             { Credito = Monto; }

             dgvFilas = dgvCaja.Rows.Count;


             //VERIFICAR SI SE HA DESELECCIONADO ALGO DE LA GRILLA
             for (int i = 0; i < dgvFilas; i++)
             {
                 if (Convert.ToBoolean(dgvCaja[8, i].Value.ToString()) == false)
                 {

                     if (Convert.ToInt32(dgvCaja[6, i].Value.ToString()) == 2) //MP
                     {

                         //MOVIMIENTO DE CAJA
                         myCadenaSQL = "insert into Caja(Entrada, MontoTotal, IdCajaImputacion, FechaS, Fecha, Debito, Credito, Efectivo, Transferencia," +
                                               "MP, Contra, Cheques, Movimiento, Comprobante, Bancos) values (" +
                                                 "1" + ", " +
                                                 Monto + ", " +
                                                 "3" + ", '" +
                                                 clsValida.ConvertirFecha(Fecha) + "', '" +
                                                 Fecha.ToShortDateString() + "', " +
                                                 Debito + ", " +
                                                 Credito + ", " +
                                                 Efectivo + ", " +
                                                 Transferencia + ", " +
                                                 MP + ", " +
                                                 Contra + ", " +
                                                 Cheques + ", '" +
                                                 "ACREDITACION POR MP" + "', '" +
                                                 dgvCaja[2, i].Value.ToString() + "', " +
                                                 bancos + ")";

                         // Ejecuto la cadena para grabar el movimiento en la Caja
                         if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaSQL); } else { clsDataBD.GetSqlB(myCadenaSQL); }
                         // Averiguo el Id del movimiento de caja recién creado
                         if (clsGlobales.ConB == null) { iIdCaja = clsDataBD.RetornarUltimoId("Caja", "IdCaja"); } else { iIdCaja = clsDataBD.RetornarUltimoIdB("Caja", "IdCaja"); }

                         iAsocioacion = Convert.ToInt32(dgvCaja[10, i].Value.ToString());

                         // Armo la cadena SQL
                         myCadenaCaja = "insert into CajaMP (IdCaja, IdCajaAsociaciones, Importe) values (" +
                                         iIdCaja + ", " + iAsocioacion + ", " +
                                         MP + ")";
                         // Ejecuto la cadena para grabar el movimiento en la Caja
                         if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaCaja); } else { clsDataBD.GetSqlB(myCadenaCaja); }

                         /********************************************
                         * Actualizo el saldo de la caja de efectivo *
                         ********************************************/
                         clsDataBD.ActualizarSaldoPorCaja("SaldoBancos", MP);

                         // CAJA BANCOS
                         myCadenaCaja = "insert into CajaBancos (IdCajaAsociacion, IdCaja, Importe) values (" +
                                         iAsocioacion + ", " + iIdCaja + ", " +
                                         MP + ")";
                         // Ejecuto la cadena para grabar el movimiento en la Caja
                         if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaCaja); } else { clsDataBD.GetSqlB(myCadenaCaja); }
                     }

                     //CREDITO
                     if (Convert.ToInt32(dgvCaja[6, i].Value.ToString()) == 4) //CREDITO
                     {


                         //MOVIMIENTO DE CAJA
                         myCadenaSQL = "insert into Caja(Entrada, MontoTotal, IdCajaImputacion, FechaS, Fecha, Debito, Credito, Efectivo, Transferencia," +
                                               "MP, Contra, Cheques, Movimiento, Comprobante, Bancos) values (" +
                                                 "1" + ", " +
                                                 Monto + ", " +
                                                 "3" + ", '" +
                                                 clsValida.ConvertirFecha(Fecha) + "', '" +
                                                 Fecha.ToShortDateString() + "', " +
                                                 Debito + ", " +
                                                 Credito + ", " +
                                                 Efectivo + ", " +
                                                 Transferencia + ", " +
                                                 MP + ", " +
                                                 Contra + ", " +
                                                 Cheques + ", '" +
                                                 "ACREDITACION POR CREDITO" + "', '" +
                                                 dgvCaja[2, i].Value.ToString() + "', " +
                                                 bancos + ")";

                         // Ejecuto la cadena para grabar el movimiento en la Caja
                         if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaSQL); } else { clsDataBD.GetSqlB(myCadenaSQL); }
                         // Averiguo el Id del movimiento de caja recién creado
                         if (clsGlobales.ConB == null) { iIdCaja = clsDataBD.RetornarUltimoId("Caja", "IdCaja"); } else { iIdCaja = clsDataBD.RetornarUltimoIdB("Caja", "IdCaja"); }

                         iAsocioacion = Convert.ToInt32(dgvCaja[10, i].Value.ToString());

                         // Armo la cadena SQL
                         myCadenaCaja = "insert into CajaCredito (IdCaja, IdCajaAsociaciones, Importe) values (" +
                                         iIdCaja + ", " + iAsocioacion + ", " +
                                         Credito + ")";
                         // Ejecuto la cadena para grabar el movimiento en la Caja
                         if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaCaja); } else { clsDataBD.GetSqlB(myCadenaCaja); }

                         /********************************************
                         * Actualizo el saldo de la caja de efectivo *
                         ********************************************/
                         clsDataBD.ActualizarSaldoPorCaja("SaldoBancos", Credito);

                         // CAJA BANCOS
                         myCadenaCaja = "insert into CajaBancos (IdCajaAsociacion, IdCaja, Importe) values (" +
                                         iAsocioacion + ", " + iIdCaja + ", " +
                                         Credito + ")";
                         // Ejecuto la cadena para grabar el movimiento en la Caja
                         if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaCaja); } else { clsDataBD.GetSqlB(myCadenaCaja); }
                     }

                     //ELIMINAR PENDIENTE DE LA CAJA BANCOS PENDIENTES
                     myCadenaSQL = "DELETE FROM CajaBcoPendientes WHERE IdCajaBcoPendiente = " + Convert.ToInt32(dgvCaja[9, i].Value.ToString());
                     if (clsGlobales.ConB==null)
                     { clsDataBD.GetSql(myCadenaSQL); }
                     else
                     { clsDataBD.GetSqlB(myCadenaSQL); }

                 }
             }

             //MENSAJE FINAL
             MessageBox.Show("El proceso ha finalizado con exito!", "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Information);
             this.Close();

         }

         private void btnQuitar_Click(object sender, EventArgs e)
         {
             if (dgvCaja.Rows.Count > 0)
             {
                 if (Convert.ToBoolean(dgvCaja.CurrentRow.Cells["Pendiente"].Value.ToString()) == true)
                 {
                     dgvCaja.CurrentRow.Cells["Pendiente"].Value = false;
                     btnQuitar.Enabled = false;
                     btnAgregar.Enabled = true;
                 }
                 else
                 {
                     btnQuitar.Enabled = true;
                     btnAgregar.Enabled = false;
                 }

                 CalcularTotAcred();
             }
         }

         private void btnAgregar_Click(object sender, EventArgs e)
         {
             if (dgvCaja.Rows.Count > 0)
             {
                 if (Convert.ToBoolean(dgvCaja.CurrentRow.Cells["Pendiente"].Value.ToString()) == false)
                 {
                     dgvCaja.CurrentRow.Cells["Pendiente"].Value = true;
                     btnQuitar.Enabled = true;
                     btnAgregar.Enabled = false;
                 }
                 else
                 {
                     btnQuitar.Enabled = false;
                     btnAgregar.Enabled = true;
                 }

                 CalcularTotAcred();
             }
         }

         private void dgvCaja_SelectionChanged(object sender, EventArgs e)
         {
             if (dgvCaja.Rows.Count > 0)
             {
                 if (Convert.ToBoolean(dgvCaja.CurrentRow.Cells["Pendiente"].Value.ToString()) == false)
                 {
                     btnAgregar.TabStop = true;
                     btnAgregar.Enabled = true;
                     btnQuitar.TabStop = false;
                     btnQuitar.Enabled = false;
                 }
                 else
                 {
                     btnAgregar.TabStop = false;
                     btnAgregar.Enabled = false;
                     btnQuitar.TabStop = true;
                     btnQuitar.Enabled = true;
                 }
             }
         }

         private void txtFecha_TextChanged(object sender, EventArgs e)
         {
             if (!(this.txtFecha.Text == ""))
             {
                 // Cargo las localidades filtradas por la búsqueda
                 CargarGrilla(" AND FechaMov = '" + txtFecha.Text + "'");
             }
             else
             {
                 CargarGrilla(" WHERE IdTipoMovimiento = " + iTipoMov);
             }
         }

         private void txtCupon_TextChanged(object sender, EventArgs e)
         {
             if (!(this.txtCupon.Text == ""))
             {
                 // Cargo las localidades filtradas por la búsqueda
                 CargarGrilla(" AND sNroOp = '" + txtCupon.Text + "'");
             }
             else
             {
                 CargarGrilla(" WHERE IdTipoMovimiento = " + iTipoMov);
             }
         }

         private void txtFecha_Click(object sender, EventArgs e)
         {
             txtCupon.Text = "";
             txtFecha.Text = "";
         }

         private void txtCupon_Click(object sender, EventArgs e)
         {
             txtCupon.Text = "";
             txtFecha.Text = "";
         }

    }
}

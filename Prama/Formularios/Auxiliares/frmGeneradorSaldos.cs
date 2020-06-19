using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prama.Formularios.Auxiliares
{
    public partial class frmGeneradorSaldos : Form
    {
        #region Variables de formulario

        // Variable que toman los datos del cliente
        int iIdCliente = 0;
        string sRazon = "";
        string sCuit = "";
        // Variable que almacena el saldo del cliente de la tabla Clientes
        double dSaldoClie = 0;
        // Variable que almacena el saldo del cliente de la tabla efactura
        double dSaldoFAct = 0;

        // Variable que almacena el string de la consulta SQL
        string myCadenaSQL = "";
        string myCadenaSQLFacturas = "";
        string myCadenaSaldos = "";
        // Tabla que almacena los datos de los clientes
        DataTable myDtClientes = new DataTable();
        // Tabla que almacena los datos de las facturas
        DataTable myDtFacturas = new DataTable();
        // Tabla que almacena los datos finales para el formulario
        DataTable myDtSaldos = new DataTable();

        #endregion

        #region Constructor del Formulario

        public frmGeneradorSaldos()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos del Formulario

        #region Evento Load del Formulario

        private void frmGeneradorSaldos_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            // Oculto los botones
            this.btnAceptar.Visible = false;
            this.btnCancelar.Visible = false;

            //Title
            this.Text = clsGlobales.cParametro.NombreFantasia + this.Text;   
        }

        #endregion

        #region eventos de los botones

        #region Evento Click del botón Agregar

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Vacío la grilla si tiene datos
            RecargarFormulario();
            // Inicializo la barra de progreso
            pgbAvance.Value = 0;
            // Variable que da el index de la grilla
            int iFilaGrilla = 0;

            // Oculto los botones que corresponden
            this.btnAgregar.Visible = false;
            this.btnSalir.Visible = false;
            // Habilito los que corresponden
            this.btnAceptar.Visible = true;
            this.btnCancelar.Visible = true;

            // Recorro la tabla de clientes y tomo su saldo
            myCadenaSQL = "select * from Clientes";
            // Recorro la tabla de eFactura mediante la vista y traigo la suma de los comprobantes
            myCadenaSQLFacturas = "select * from Vista_AcumuladorSaldosFacturas";
            // Traigo los datos de los saldos de clientes de la tabla intermedia
            myCadenaSaldos = "Select * from SaldoCliProv";

            // Ejecuto la consulta y guardo los resultados en una tabla
            myDtClientes = clsDataBD.GetSql(myCadenaSQL);
            
            // Verifico el estado de la conexion B
            if (clsGlobales.ConB == null)
            {
                // Ejecuto la consulta y guardo los resultados en una tabla
                myDtFacturas = clsDataBD.GetSql(myCadenaSQLFacturas);
            }
            else
            {
                // Ejecuto la consulta y guardo los resultados en una tabla
                myDtFacturas = clsDataBD.GetSqlB(myCadenaSQLFacturas);
                // Guardo los saldos de los comprobanbtes de la tabla intermedia en la tabal
                myDtSaldos = clsDataBD.GetSqlB(myCadenaSaldos);
            }

            // Seteo la barra de progreso con la cantidad de clientes
            pgbAvance.Maximum = myDtFacturas.Rows.Count;

            // Evito que el dgv genere columnas automáticas
            dgvClientes.AutoGenerateColumns = false;
            
            // Recorro la tabla que tiene la suma de los comprobantes
            foreach (DataRow rFacturas in myDtFacturas.Rows)
            {
                // Tomo en una variable el Id del cliente
                iIdCliente = Convert.ToInt32(rFacturas["IdCliente"]);
                // Tomo en la variable el saldo acumulado de las facturas
                dSaldoFAct = Convert.ToDouble(rFacturas["Acumulado"]);
                // Reinicio la variable
                dSaldoClie = 0;
                // Recorro la tabla de los clientes y tomos sus datos
                foreach (DataRow rClientes in myDtClientes.Rows)
                {
                    if (Convert.ToInt32(rClientes["IdCliente"]) == iIdCliente)
                    {
                        // Paso los datos del cliente a las variables
                        sRazon = rClientes["RazonSocial"].ToString();
                        sCuit = rClientes["Cuit"].ToString();
                        // Si la conexion 2 es nula
                        if (clsGlobales.ConB == null)
                        {
                            // Paso a la variable el saldo del scliente de la tabla clientes
                            dSaldoClie = Convert.ToDouble(rClientes["Saldo"]);
                        }
                        // Si no
                        else
                        {
                            // Paso a la variable el saldo de la tabla intermedia si es que hay un
                            foreach (DataRow rowSaldos in myDtSaldos.Rows)
                            {
                                if (iIdCliente == Convert.ToInt32(rowSaldos["idCliente"]))
                                {
                                    dSaldoClie = Convert.ToDouble(rowSaldos["SaldoCli"]);
                                }
                            }
                        }

                        // Si hay alguna diferencia, agrego la fila
                        if (dSaldoClie - dSaldoFAct != 0)
                        {
                            // Agrego a la grilla la fila
                            dgvClientes.Rows.Add(1);
                            // Le paso los datos a la nueva fila
                            dgvClientes.Rows[iFilaGrilla].Cells["IdCliente"].Value = iIdCliente;
                            dgvClientes.Rows[iFilaGrilla].Cells["RazonSocial"].Value = sRazon;
                            dgvClientes.Rows[iFilaGrilla].Cells["CUIT"].Value = sCuit;
                            dgvClientes.Rows[iFilaGrilla].Cells["SaldoActual"].Value = dSaldoClie.ToString("#0.00");
                            dgvClientes.Rows[iFilaGrilla].Cells["SaldoReal"].Value = dSaldoFAct.ToString("#0.00");
                            dgvClientes.Rows[iFilaGrilla].Cells["Diferencia"].Value = (dSaldoClie - dSaldoFAct).ToString("#0.00");

                            // Aumento el contador de filas
                            iFilaGrilla++;
                        }
                        // Aumento el valor de la barra
                        pgbAvance.Value++;
                    }
                }

            }

            if (dgvClientes.Rows.Count == 0)
            {
                // Muestro el mensaje de que los saldos están correctos
                MessageBox.Show("Los saldos de los clientes son correctos", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Recargo el formulario
                RecargarFormulario();
            }
            
        }

        #endregion

        #region Evento Click del botón Aceptar

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            bool bEncontrado = false;
            // Variable que cuenta la cantidad de correcciones realizadas
            // Pido confirmación de la operación 
            DialogResult myRespuesta = MessageBox.Show("Desea corregir los saldos ???", "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Si la respuesta es si
            if (myRespuesta == DialogResult.Yes)
            {
                // Reinicio la barra de progreso
                pgbAvance.Value = 0;
                // Seteo el máximo de la misma
                pgbAvance.Maximum = dgvClientes.Rows.Count;

                for (int i = 0; i < dgvClientes.Rows.Count; i++)
                {
                    dgvClientes.CurrentCell = dgvClientes[0, i];

                    // Paso a la variable el Id del cliente
                    iIdCliente = Convert.ToInt32(dgvClientes.CurrentRow.Cells["IdCLiente"].Value);
                    // Paso a la variable el saldo de los comprobantes
                    dSaldoClie = Convert.ToDouble(dgvClientes.CurrentRow.Cells["SaldoReal"].Value);
                    // Armo la cadena SQL
                    myCadenaSQL = "update Clientes set Saldo = " + dSaldoClie + " where IdCliente = " + iIdCliente;
                    
                    // Verifico el estado de la conexion B
                    if (clsGlobales.ConB == null)
                    {
                        // Ejecuto la consulta 
                        clsDataBD.GetSql(myCadenaSQL);
                    }
                    else
                    {
                        // Traigo los datos de los saldos de clientes de la tabla intermedia
                        myCadenaSaldos = "Select * from SaldoCliProv";
                        // Guardo los saldos de los comprobanbtes de la tabla intermedia en la tabal
                        myDtSaldos = clsDataBD.GetSqlB(myCadenaSaldos);
                        // Paso a la variable el saldo de la tabla intermedia si es que hay un
                        foreach (DataRow rowSaldos in myDtSaldos.Rows)
                        {

                            if (iIdCliente == Convert.ToInt32(rowSaldos["idCliente"]))
                            {
                                bEncontrado = true;
                                break;
                            }
                        }

                        if (bEncontrado)
                        {
                            myCadenaSaldos = "update SaldoCliProv set SaldoCli = " + dSaldoClie + " where IdCliente = " + iIdCliente;
                            bEncontrado = false;
                        }
                        else
                        {
                            myCadenaSaldos = "insert into SaldoCliProv (IdCliente, SaldoCli, IdProveedor, SaldoProv, SaldoInicial, SaldoAFavor) values (" +
                                            iIdCliente + ", " + dSaldoClie + ", 0, 0, 0, 0)";
                        }

                        // Ejecuto la consulta 
                        clsDataBD.GetSqlB(myCadenaSaldos);
                    }
                    
                    // Actualizo el valor de la barra de progreso
                    pgbAvance.Value++;
                }
            }

            btnCancelar.PerformClick();
        }

        #endregion

        #region Evento Click del botón Cancelar

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Recargo el formulario
            RecargarFormulario();
        }

        #endregion

        #region Evento Click del botón Salir

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Cierro el formulario
            this.Close();
        }

        #endregion

        #endregion

        #endregion

        #region Métodos del Formulario

        #region Método que vacía la grilla

        private void VaciarGrilla()
        {
            if (dgvClientes.Rows.Count > 0)
            {
                dgvClientes.DataSource = null;
                dgvClientes.Rows.Clear();
                
            }
        }

        #endregion

        #region Método que reinicia el formulario

        private void RecargarFormulario()
        {
            // Vacío la grilla si tiene datos
            VaciarGrilla();
            // Inicializo la barra de progreso
            pgbAvance.Value = 0;
            // Oculto los botones
            this.btnAceptar.Visible = false;
            this.btnCancelar.Visible = false;
            // Habilito los botones
            this.btnAgregar.Visible = true;
            this.btnSalir.Visible = true;
        }

        #endregion

        #endregion

    }
}

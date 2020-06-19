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
    public partial class frmCorregidorSaldosCtaCteClientes : Form
    {
        public frmCorregidorSaldosCtaCteClientes()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnRecibos_Click(object sender, EventArgs e)
        {
            // Contadores de registros
            int iProcesado = 0;
            int iCorrecto = 0;
            int iCorregido = 0;
            
            // Armo la cadena sql para traer a una tabla todos los clientes
            string myCadenaSQL = "select * from Clientes";
            // Ejecuto la consulta
            DataTable myTabla = clsDataBD.GetSql(myCadenaSQL);
            // Almaceno en una variable la cantidad de clientes que hay en la tabla
            int iFilas = myTabla.Rows.Count;
            // Asigno a la barra de avance el máximo de la misma en base a la cantidad de clientes
            proRegistros.Maximum = iFilas;

            if (clsGlobales.ConB == null)
            {
                // Recorro la tabla clientes
                foreach (DataRow row in myTabla.Rows)
                {
                    // Traigo a una tabla los datos de los recibos del cliente a una tabla
                    myCadenaSQL = "select * from recibos where IdCliente = " + Convert.ToInt32(row["IdCliente"]);
                    // Cargo la tabla con los datos
                    DataTable myRecibos = clsDataBD.GetSql(myCadenaSQL);
                    // Paso a una variable la cantidad de recibos que hay en la tabla para este cliente
                    int iCantRecibos = myRecibos.Rows.Count;
                    // Si tiene algún recibo
                    if (iCantRecibos > 0)
                    {
                        // Ejecuto el procedimiento que actualiza el total del recibo
                        myCadenaSQL = "exec UpdateTotalRecCli " + Convert.ToInt32(row["IdCliente"]);
                        clsDataBD.GetSql(myCadenaSQL);
                        // Incremento el contador
                        iCorregido++;
                    }
                    else
                    {
                        // Incremento el contador
                        iCorrecto++;
                    }
                    // Incremento el contador
                    iProcesado++;
                    // Paso a los texbox y a la barra los datos de los contadores
                    txtProcesados.Text = iProcesado.ToString();
                    proRegistros.Value = iProcesado;
                    txtCorrectos.Text = iCorrecto.ToString();
                    txtCorregidos.Text = iCorregido.ToString();
                }
            }
            else
            {

                // Recorro la tabla clientes
                foreach (DataRow row in myTabla.Rows)
                {
                    // Traigo a una tabla los datos de los recibos del cliente a una tabla
                    myCadenaSQL = "select * from recibos where IdCliente = " + Convert.ToInt32(row["IdCliente"]);
                    // Cargo la tabla con los datos
                    DataTable myRecibos = clsDataBD.GetSqlB(myCadenaSQL);
                    // Paso a una variable la cantidad de recibos que hay en la tabla para este cliente
                    int iCantRecibos = myRecibos.Rows.Count;
                    // Si tiene algún recibo
                    if (iCantRecibos > 0)
                    {
                        // Ejecuto el procedimiento que actualiza el total del recibo
                        myCadenaSQL = "exec UpdateTotalRecCli " + Convert.ToInt32(row["IdCliente"]);
                        clsDataBD.GetSqlB(myCadenaSQL);
                        // Incremento el contador
                        iCorregido++;
                    }
                    else
                    {
                        // Incremento el contador
                        iCorrecto++;
                    }
                    // Incremento el contador
                    iProcesado++;
                    // Paso a los texbox y a la barra los datos de los contadores
                    txtProcesados.Text = iProcesado.ToString();
                    proRegistros.Value = iProcesado;
                    txtCorrectos.Text = iCorrecto.ToString();
                    txtCorregidos.Text = iCorregido.ToString();
                }
            }

            // Habilito el boton Clientes, para que se pueda seguir con el proceso
            btnClientes.Enabled = true;
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            txtProcesados.Text = "";
            txtCorrectos.Text = "";
            txtCorregidos.Text = "";
            
            // Contadores de registros
            int iProcesado = 0;
            int iCorrecto = 0;
            int iCorregido = 0;

            int iIdCliente = 0;
            
            // Variable que almacena el saldo de la consulta
            double dSaldoCli = 0;
            // Armo la cadena sql para traer a una tabla todos los clientes
            string myCadenaSQL = "select * from Clientes";
            // Ejecuto la consulta
            DataTable myTabla = clsDataBD.GetSql(myCadenaSQL);


            // Almaceno en una variable la cantidad de clientes que hay en la tabla
            int iFilas = myTabla.Rows.Count;
            // Asigno a la barra de avance el máximo de la misma en base a la cantidad de clientes
            proRegistros.Maximum = iFilas;


            // Recorro la tabla clientes
            foreach (DataRow row in myTabla.Rows)
            {
                // Paso el Id del cliente a la variable
                iIdCliente = Convert.ToInt32(row["IdCliente"]);
                
                if (clsGlobales.ConB == null)
                {

                    // Traigo a una tabla los datos de los recibos del cliente a una tabla
                    myCadenaSQL = "exec getMontoFactRec " + iIdCliente;
                    // Cargo la tabla con los datos
                    DataTable mySaldo = clsDataBD.GetSql(myCadenaSQL);

                    // Recorro la tabla para pasar el saldo a la variable
                    foreach (DataRow rowSaldo in mySaldo.Rows)
                    {
                        dSaldoCli = Convert.ToDouble(rowSaldo["Saldo"]);
                        // Si el saldo del cliente es > 0, quiere decir que está debiendo
                        if (dSaldoCli > 0)
                        {
                            // Actualizo el saldo deudor del cliente y elimino el saldo a favor si lo hubiera
                            myCadenaSQL = "update Clientes set Saldo = " + dSaldoCli + ", SaldoAFavor = 0 where IdCliente = " + iIdCliente;
                            // Ejecuto la consulta
                            clsDataBD.GetSql(myCadenaSQL);
                        }
                        // Si el saldo del cliente es = 0, quiere decir que no debe ni tiene saldo a favor
                        if (dSaldoCli == 0)
                        {
                            // Actualizo el saldo deudor del cliente
                            myCadenaSQL = "update Clientes set Saldo = 0, SaldoAFavor = 0 where IdCliente = " + iIdCliente;
                            // Ejecuto la consulta
                            clsDataBD.GetSql(myCadenaSQL);
                        }

                        // Si el saldo del cliente es > 0, quiere decir que está debiendo
                        if (dSaldoCli < 0)
                        {
                            // Actualizo el saldo deudor del cliente
                            myCadenaSQL = "update Clientes set Saldo = 0, SaldoAFavor = " + dSaldoCli + " where IdCliente = " + iIdCliente; ;
                            // Ejecuto la consulta
                            clsDataBD.GetSql(myCadenaSQL);
                        }
                    }

                }
                else
                {

                    // Traigo a una tabla los datos de los recibos del cliente a una tabla
                    myCadenaSQL = "exec getMontoFactRec " + iIdCliente;
                    // Cargo la tabla con los datos
                    DataTable mySaldo = clsDataBD.GetSqlB(myCadenaSQL);

                    // Recorro la tabla para pasar el saldo a la variable
                    foreach (DataRow rowSaldo in mySaldo.Rows)
                    {
                        dSaldoCli = Convert.ToDouble(rowSaldo["Saldo"]);
                        // Si el saldo del cliente es > 0, quiere decir que está debiendo
                        if (dSaldoCli > 0)
                        {
                            // Actualizo el saldo deudor del cliente y elimino el saldo a favor si lo hubiera
                            myCadenaSQL = "Update SaldoCliProv Set SaldoCli = " + dSaldoCli + ", SaldoAFavor = 0 where IdCliente = " + iIdCliente;
                            // Ejecuto la consulta
                            clsDataBD.GetSqlB(myCadenaSQL);
                        }
                        // Si el saldo del cliente es = 0, quiere decir que no debe ni tiene saldo a favor
                        if (dSaldoCli == 0)
                        {
                            // Actualizo el saldo deudor del cliente
                            myCadenaSQL = "update SaldoCliProv set SaldoCli = 0, SaldoAFavor = 0 where IdCliente = " + iIdCliente;
                            // Ejecuto la consulta
                            clsDataBD.GetSqlB(myCadenaSQL);
                        }

                        // Si el saldo del cliente es > 0, quiere decir que está debiendo
                        if (dSaldoCli < 0)
                        {
                            // Actualizo el saldo deudor del cliente
                            myCadenaSQL = "update SaldoCliProv Set SaldoCli = 0, SaldoAFavor = " + dSaldoCli + " where IdCliente = " + iIdCliente; ;
                            // Ejecuto la consulta
                            clsDataBD.GetSqlB(myCadenaSQL);
                        }
                    }

                }


                // Incremento el contador
                iProcesado++;
                // Incremento el contador
                iCorregido++;
                // Incremento el contador
                iCorrecto++;
                // Paso a los texbox y a la barra los datos de los contadores
                txtProcesados.Text = iProcesado.ToString();
                proRegistros.Value = iProcesado;
                txtCorrectos.Text = iCorrecto.ToString();
                txtCorregidos.Text = iCorregido.ToString();
                

            }

            btnFacturas.Enabled = true;
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {
            txtProcesados.Text = "";
            txtCorrectos.Text = "";
            txtCorregidos.Text = "";

            // Contadores de registros
            int iProcesado = 0;
            int iCorrecto = 0;
            int iCorregido = 0;

            int iIdCliente = 0;

            // Variable que almacena lo pagado por el cliente
            double dPagadoTotal = 0;

            // Armo la cadena sql para traer a una tabla todos los clientes
            string myCadenaSQL = "select * from Clientes";
            // Ejecuto la consulta
            DataTable myTabla = clsDataBD.GetSql(myCadenaSQL);

            //WHITE -->
            if (clsGlobales.ConB == null)
            {
  
                // Almaceno en una variable la cantidad de clientes que hay en la tabla
                int iFilas = myTabla.Rows.Count;
                // Asigno a la barra de avance el máximo de la misma en base a la cantidad de clientes
                proRegistros.Maximum = iFilas;
                // Recorro la tabla clientes
                foreach (DataRow row in myTabla.Rows)
                {
                    // Paso el Id del cliente a la variable
                    iIdCliente = Convert.ToInt32(row["IdCliente"]);

                    // Traigo a una tabla los datos de los recibos del cliente a una tabla
                    myCadenaSQL = "exec getMontoFactRec " + iIdCliente;
                    // Cargo la tabla con los datos
                    DataTable myPagado = clsDataBD.GetSql(myCadenaSQL);

                    // Recorro la tabla para pasar el saldo a la variable
                    foreach (DataRow myRowPagado in myPagado.Rows)
                    {
                        // Paso a la variable el total de lo pagado por el cliente
                        dPagadoTotal = Convert.ToDouble(myRowPagado["Recibos $"]);
                        // Si el cliente pagó algo, continuo
                        if (dPagadoTotal > 0)
                        {
                            // Busco las facturas que pueda tener el cliente en la tabla efactura
                            myCadenaSQL = "select * from eFactura where IdCliente = " + iIdCliente;
                            // Paso los datos a una tabla
                            DataTable myFacturas = clsDataBD.GetSql(myCadenaSQL);
                            // verifico que el cliente tenga facturas
                            int iCantFact = myFacturas.Rows.Count;
                            // Si tiene facturas, continuo
                            if (iCantFact > 0)
                            {
                                // Recorro la tabla que tiene los datos de las facturas
                                foreach (DataRow rowFact in myFacturas.Rows)
                                {
                                    // Variable que contiene el id de la factura
                                    int iIdFactura = Convert.ToInt32(rowFact["IdFactura"]);
                                    // Paso a una variable el total de la factura
                                    double dTotalFactura = Convert.ToDouble(rowFact["Total"]);
                                    // Si lo almacenado en la variable que contiene lo pagado por el cliente, 
                                    // es mayor al total de la factura
                                    if (dTotalFactura <= dPagadoTotal)
                                    {
                                        // Seteo el saldo de la factura a 0
                                        myCadenaSQL = "update eFactura set Saldo = 0 where IdFactura = " + iIdFactura;
                                        // Ejecuto la consulta
                                        clsDataBD.GetSql(myCadenaSQL);
                                        // al total pagado por el cliente, le resto el importe de la factura cancelada
                                        dPagadoTotal = dPagadoTotal - dTotalFactura;
                                        // Le quito decimales a la variable
                                        dPagadoTotal = Convert.ToDouble(dPagadoTotal.ToString("#0.00"));
                                    }
                                    else
                                    {
                                        // Si lo que queda pagado por el cliente es menor al monto de la factura, lo pongo a cuenta
                                        double dSaldoParcial = Convert.ToDouble((dTotalFactura - dPagadoTotal).ToString("#0.00"));
                                        // Armo la consulta
                                        myCadenaSQL = "update eFactura set saldo = " + dSaldoParcial + " where IdFactura = " + iIdFactura;

                                        // Ejecuto la consulta
                                        clsDataBD.GetSql(myCadenaSQL);
                                        // Dejo en 0 lo pagado por el cliente porque ya lo acreditamos a todo
                                        dPagadoTotal = 0;
                                    }

                                    iCorregido++;
                                }
                            }


                        }
                        else
                        {
                            iCorrecto++;
                        }

                    }
                    iProcesado++;

                }
            }
            else //BLACK -->
            {
                // Almaceno en una variable la cantidad de clientes que hay en la tabla
                int iFilas = myTabla.Rows.Count;
                // Asigno a la barra de avance el máximo de la misma en base a la cantidad de clientes
                proRegistros.Maximum = iFilas;
                // Recorro la tabla clientes
                foreach (DataRow row in myTabla.Rows)
                {
                    // Paso el Id del cliente a la variable
                    iIdCliente = Convert.ToInt32(row["IdCliente"]);

                    // Traigo a una tabla los datos de los recibos del cliente a una tabla
                    myCadenaSQL = "exec getMontoFactRec " + iIdCliente;
                    // Cargo la tabla con los datos
                    DataTable myPagado = clsDataBD.GetSqlB(myCadenaSQL);

                    // Recorro la tabla para pasar el saldo a la variable
                    foreach (DataRow myRowPagado in myPagado.Rows)
                    {
                        // Paso a la variable el total de lo pagado por el cliente
                        dPagadoTotal = Convert.ToDouble(myRowPagado["Recibos $"]);
                        // Si el cliente pagó algo, continuo
                        if (dPagadoTotal > 0)
                        {
                            // Busco las facturas que pueda tener el cliente en la tabla efactura
                            myCadenaSQL = "select * from eFactura where IdCliente = " + iIdCliente;
                            // Paso los datos a una tabla
                            DataTable myFacturas = clsDataBD.GetSqlB(myCadenaSQL);
                            // verifico que el cliente tenga facturas
                            int iCantFact = myFacturas.Rows.Count;
                            // Si tiene facturas, continuo
                            if (iCantFact > 0)
                            {
                                // Recorro la tabla que tiene los datos de las facturas
                                foreach (DataRow rowFact in myFacturas.Rows)
                                {
                                    // Variable que contiene el id de la factura
                                    int iIdFactura = Convert.ToInt32(rowFact["IdFactura"]);
                                    // Paso a una variable el total de la factura
                                    double dTotalFactura = Convert.ToDouble(rowFact["Total"]);
                                    // Si lo almacenado en la variable que contiene lo pagado por el cliente, 
                                    // es mayor al total de la factura
                                    if (dTotalFactura <= dPagadoTotal)
                                    {
                                        // Seteo el saldo de la factura a 0
                                        myCadenaSQL = "update eFactura set Saldo = 0 where IdFactura = " + iIdFactura;
                                        // Ejecuto la consulta
                                        clsDataBD.GetSqlB(myCadenaSQL);
                                        // al total pagado por el cliente, le resto el importe de la factura cancelada
                                        dPagadoTotal = dPagadoTotal - dTotalFactura;
                                        // Le quito decimales a la variable
                                        dPagadoTotal = Convert.ToDouble(dPagadoTotal.ToString("#0.00"));
                                    }
                                    else
                                    {
                                        // Si lo que queda pagado por el cliente es menor al monto de la factura, lo pongo a cuenta
                                        double dSaldoParcial = Convert.ToDouble((dTotalFactura - dPagadoTotal).ToString("#0.00"));
                                        // Armo la consulta
                                        myCadenaSQL = "update eFactura set saldo = " + dSaldoParcial + " where IdFactura = " + iIdFactura;

                                        // Ejecuto la consulta
                                        clsDataBD.GetSqlB(myCadenaSQL);
                                        // Dejo en 0 lo pagado por el cliente porque ya lo acreditamos a todo
                                        dPagadoTotal = 0;
                                    }

                                    iCorregido++;
                                }
                            }


                        }
                        else
                        {
                            iCorrecto++;
                        }

                    }
                    iProcesado++;

                }
                        
            }


            // Paso a los texbox y a la barra los datos de los contadores
            txtProcesados.Text = iProcesado.ToString();
            proRegistros.Value = iProcesado;
            txtCorrectos.Text = iCorrecto.ToString();
            txtCorregidos.Text = iCorregido.ToString();
        }
    }
}

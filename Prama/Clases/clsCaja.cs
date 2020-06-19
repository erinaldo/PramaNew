using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Prama.Clases
{
    class clsCaja
    {
        
        
        #region propiedades de la clase

        public static bool Entrada { set; get; }
        public static double Monto { set; get; }
        public static int IdCajaImputacion { set; get; }
        public static DateTime Fecha { set; get; }
        public static double Debito { set; get; }
        public static double Credito { set; get; }
        public static double Efectivo { set; get; }
        public static double Transferencia { set; get; }
        public static double MP { set; get; }
        public static double Contra { set; get; }
        public static double Cheques { set; get; }
        public static string Movimiento { set; get; }
        public static string Comprobante { set; get; }

        #endregion

        #region Contructor

        public clsCaja()
        {

        }

        #endregion

        #region Método que Carga los datos a la caja

        public static void CargarCaja(int iLastRecibo = 0)

            // Par'ametro de entrada
        // 1 Transferencias
        // 2 Mercado Pago
        // 3 Débito
        // 4 Credito
        {
            /*****************************************
             * Cargo los movimientos a la tabla caja *
             ****************************************/
            int iAsocioacion = 0;
            // Variable que almacena si el movimiento es una entrada
            int iEsEntrada = 0;
            // Variable que almacena el id de la caja
            int iIdCaja = 0;
            //Bancos
            double bancos = 0;
            // Si es entrada, le asigno el uno para la cadena SQL
            if (Entrada)
            {
                iEsEntrada = 1;
            }
            // Si no es una entrada
            if (!(Entrada))
            {
                // Paso a todos los montos a negativo
                Monto = Monto * (-1);
                Debito = Debito * (-1);
                Credito = Credito * (-1);
                Efectivo = Efectivo * (-1);
                Transferencia = Transferencia * (-1);
                MP = MP * (-1);
                Contra = Contra * (-1);
                Cheques = Cheques * (-1);
            }
            //Bancos
            bancos = Debito + Transferencia;
            // Armo la cadena SQL
            string myCadenaSQL = "insert into Caja(Entrada, MontoTotal, IdCajaImputacion, FechaS, Fecha, Debito, Credito, Efectivo, Transferencia," +
                                  "MP, Contra, Cheques, Movimiento, Comprobante, Bancos) values (" +
                                    iEsEntrada + ", " +
                                    Monto + ", " +
                                    IdCajaImputacion + ", '" +
                                    clsValida.ConvertirFecha(Fecha) + "', '" +
                                    Fecha.ToShortDateString() + "', " +
                                    Debito + ", " +
                                    Credito + ", " +
                                    Efectivo + ", " +
                                    Transferencia + ", " +
                                    MP + ", " +
                                    Contra + ", " +
                                    Cheques + ", '" +
                                    Movimiento + "', '" +
                                    Comprobante + "', " +
                                    bancos + ")";
            // Ejecuto la cadena para grabar el movimiento en la Caja
            if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaSQL); } else { clsDataBD.GetSqlB(myCadenaSQL); }
            // Averiguo el Id del movimiento de caja recién creado
            if (clsGlobales.ConB == null) {iIdCaja = clsDataBD.RetornarUltimoId("Caja", "IdCaja");} else { iIdCaja = clsDataBD.RetornarUltimoIdB("Caja", "IdCaja"); }
            
            // variable para la cadena SQL de los diferentes tipos de caja
            string myCadenaCaja = "";

            /********************************************
            * Cargo los movimientos a la tabla Efectivo *
            ********************************************/ 
            // Si Hay efectivo o contrareembolso
            if (Efectivo != 0 || Contra != 0)
            {
                // Si es efectivo el Id de CajaEfectivoTipo es 1
                if (Efectivo != 0)
                {
                    // Armo la cadena SQL
                    myCadenaCaja = "insert into CajaEfectivo (IdCaja, IdCajaEfectivoTipo, Importe) values (" +
                                    iIdCaja + ", 1," +
                                    Efectivo + ")";
                    // Ejecuto la cadena para grabar el movimiento en la Caja
                    if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaCaja); } else { clsDataBD.GetSqlB(myCadenaCaja); }

                    /********************************************
                    * Actualizo el saldo de la caja de efectivo *
                    ********************************************/
                    clsDataBD.ActualizarSaldoPorCaja("SaldoEfectivo", Efectivo);

                }
                // Si es contrareembolso el Id de CajaEfectivoTipo es 2
                if (Contra != 0)
                {
                    // Armo la cadena SQL
                    myCadenaCaja = "insert into CajaEfectivo (IdCaja, IdCajaEfectivoTipo, Importe) values (" +
                                    iIdCaja + ", 2," +
                                    Contra + ")";
                    // Ejecuto la cadena para grabar el movimiento en la Caja
                    if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaCaja); } else { clsDataBD.GetSqlB(myCadenaCaja); }

                    /********************************************
                    * Actualizo el saldo de la caja de efectivo *
                    ********************************************/
                    clsDataBD.ActualizarSaldoPorCaja("SaldoEfectivo", Contra);
                }
                
            }

            /*************************************************
            * Cargo los movimientos a la tabla Trasferencias *
            *************************************************/
            // Si Hay Otros
            if (Transferencia != 0 || MP !=0)
            {
                // Tengo que trae los datos de la temporal para tomar de ahí los datos antes de pasarlos a la caja

                iAsocioacion = RetornarDatosTemporal(1);

                // Si es efectivo el Id de CajaEfectivoTipo es 1
                if (Transferencia != 0)
                {
                    // Armo la cadena SQL
                    myCadenaCaja = "insert into CajaTransferencias (IdCaja, IdCajaAsociaciones, Importe) values (" +
                                    iIdCaja + ", " + iAsocioacion + ", " +
                                    Transferencia + ")";
                    // Ejecuto la cadena para grabar el movimiento en la Caja
                    if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaCaja); } else { clsDataBD.GetSqlB(myCadenaCaja); }

                    /********************************************
                    * Actualizo el saldo de la caja de Transferencia *
                    ********************************************/
                    clsDataBD.ActualizarSaldoPorCaja("SaldoTransferencias", Transferencia);


                    // CAJA BANCOS
                    myCadenaCaja = "insert into CajaBancos (IdCajaAsociacion, IdCaja, Importe) values (" +
                                    iAsocioacion + ", " + iIdCaja + ", " +
                                    Transferencia + ")";
                    // Ejecuto la cadena para grabar el movimiento en la Caja
                    if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaCaja); } else { clsDataBD.GetSqlB(myCadenaCaja); }

                }
                // Si es contrareembolso el Id de CajaEfectivoTipo es 2
                if (MP != 0)
                {
                    iAsocioacion = RetornarDatosTemporal(2);
                    
                    //// Armo la cadena SQL
                    //myCadenaCaja = "insert into CajaMP (IdCaja, IdCajaAsociaciones, Importe) values (" +
                    //                iIdCaja + ", " + iAsocioacion + ", " +
                    //                MP + ")";
                    //// Ejecuto la cadena para grabar el movimiento en la Caja
                    //if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaCaja); } else { clsDataBD.GetSqlB(myCadenaCaja); }

                    ///********************************************
                    //* Actualizo el saldo de la caja de efectivo *
                    //********************************************/
                    //clsDataBD.ActualizarSaldoPorCaja("SaldoMP", MP);

                    //// CAJA BANCOS
                    //myCadenaCaja = "insert into CajaBancos (IdCajaAsociacion, IdCaja, Importe) values (" +
                    //                iAsocioacion + ", " + iIdCaja + ", " +
                    //                MP + ")";
                    //// Ejecuto la cadena para grabar el movimiento en la Caja
                    //if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaCaja); } else { clsDataBD.GetSqlB(myCadenaCaja); }

                    //TOMAR LOS DATOS DE LA TEMPORAL CORRESPONDIENTES A MERCADOPAGO
                    myCadenaSQL = "select * from Temporal_DetallePagoCaja where TipoMov = 2"
                                    + " AND IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario;
                    DataTable myDtPC = new DataTable();
                    // Ejecuto la cadena para grabar el movimiento en la Caja
                    if (clsGlobales.ConB == null) { myDtPC = clsDataBD.GetSql(myCadenaSQL); } else { myDtPC=clsDataBD.GetSqlB(myCadenaSQL); }
                    // Recorro la temporal y grabo el cheque en la tabla
                    string Cupon = "";
                    string Tarjeta = "";
                    //TRAER DATOS DE LA TEMPORAL DE CUPON Y TARJETA
                    foreach (DataRow myRow in myDtPC.Rows)
                    {
                        Cupon = myRow["Cupon"].ToString();
                        Tarjeta = myRow["Tarjeta"].ToString();
                    }
                    //GRABO EN LA CAJA PENDIENTES
                    myCadenaCaja = "insert into CajaBcoPendientes (IdRecibo, IdUsuario, iTipoOp, FechaMov, sNroOp, sTarjeta, Importe, Pendiente, IdCajaAsociacion) values (" +
                                    iLastRecibo + "," + clsGlobales.UsuarioLogueado.IdUsuario + ",2,'" + clsValida.ConvertirFecha(DateTime.Now) + "','" +
                                    Cupon + "','" + Tarjeta + "'," + MP + ",1," + iAsocioacion + ")";
                    // Ejecuto la cadena para grabar el movimiento en la Caja
                    if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaCaja); } else { clsDataBD.GetSqlB(myCadenaCaja); }
                }
            }

            //DEBITO 
            if (Debito != 0)
            {
                iAsocioacion = RetornarDatosTemporal(3);

                // Armo la cadena SQL
                myCadenaCaja = "insert into CajaDebito (IdCaja, IdCajaAsociaciones, Importe) values (" +
                                iIdCaja + ", " + iAsocioacion + ", " +
                                Debito + ")";
                // Ejecuto la cadena para grabar el movimiento en la Caja
                if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaCaja); } else { clsDataBD.GetSqlB(myCadenaCaja); }

                /********************************************
                * Actualizo el saldo de la caja de efectivo *
                ********************************************/
                clsDataBD.ActualizarSaldoPorCaja("SaldoDebito", Debito);

                // CAJA BANCOS
                myCadenaCaja = "insert into CajaBancos (IdCajaAsociacion, IdCaja, Importe) values (" +
                                iAsocioacion + ", " + iIdCaja + ", " +
                                Debito + ")";
                // Ejecuto la cadena para grabar el movimiento en la Caja
                if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaCaja); } else { clsDataBD.GetSqlB(myCadenaCaja); }
             }

            //CREDITO
            if (Credito != 0)
            {    
                //TOMAR LOS DATOS DE LA TEMPORAL CORRESPONDIENTES A MERCADOPAGO
                myCadenaSQL = "select * from Temporal_DetallePagoCaja where TipoMov = 4"
                                + " AND IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario;
                DataTable myDtPC = new DataTable();
                // Ejecuto la cadena para grabar el movimiento en la Caja
                if (clsGlobales.ConB == null) { myDtPC = clsDataBD.GetSql(myCadenaSQL); } else { myDtPC=clsDataBD.GetSqlB(myCadenaSQL); }
                // Recorro la temporal y grabo el cheque en la tabla
                string Cupon = "";
                string Tarjeta = "";
                //TRAER DATOS DE LA TEMPORAL DE CUPON Y TARJETA
                foreach (DataRow myRow in myDtPC.Rows)
                {
                    Cupon = myRow["Cupon"].ToString();
                    Tarjeta = myRow["Tarjeta"].ToString();
                }
                //GRABO EN LA CAJA PENDIENTES
                myCadenaCaja = "insert into CajaBcoPendientes (IdRecibo, IdUsuario, iTipoOp, FechaMov, sNroOp, sTarjeta, Importe, Pendiente, IdCajaAsociacion) values (" +
                                iLastRecibo + "," + clsGlobales.UsuarioLogueado.IdUsuario + ",4,'" + clsValida.ConvertirFecha(DateTime.Now) + "','" +
                                    Cupon + "','" + Tarjeta + "'," + Credito + ",1," + iAsocioacion + ")";

                // Ejecuto la cadena para grabar el movimiento en la Caja
                if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaCaja); } else { clsDataBD.GetSqlB(myCadenaCaja); }
            }


            //UPDATE CAJA SALDOS (BANCOS)
            // CAJA BANCOS
            myCadenaCaja = "Update CajaSaldos SET SaldoBancos+= " + bancos;
            // Ejecuto la cadena para grabar el movimiento en la Caja
            if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaCaja); } else { clsDataBD.GetSqlB(myCadenaCaja); }

            //CAJA ASOCIACION
            myCadenaCaja = "Update CajaAsociacionesCuentas SET Saldo+= " + bancos + " WHERE IdCajaAsociaciones = " + iAsocioacion;
            // Ejecuto la cadena para grabar el movimiento en la Caja
            if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaCaja); } else { clsDataBD.GetSqlB(myCadenaCaja); }

            //CHEQUES
            if (Cheques != 0)
            {
                int iIdCheque = 0;
                myCadenaSQL = "select * from Temporal_DetalleCheques where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario;
                DataTable myDt = new DataTable();
                // Ejecuto la cadena para grabar el movimiento en la Caja
                if (clsGlobales.ConB == null) { myDt = clsDataBD.GetSql(myCadenaSQL); } else { myDt=clsDataBD.GetSqlB(myCadenaSQL); }
                // Recorro la temporal y grabo el cheque en la tabla
                foreach (DataRow myRow in myDt.Rows)
                {
                    int iNumero = Convert.ToInt32(myRow["Numero"]);
                    string sFechaEmision = myRow["FechaEmision"].ToString();
                    string sFechaCobro = myRow["FechaCobro"].ToString();
                    double dImporte = Convert.ToDouble(myRow["Importe"]);
                    string sBanco = myRow["Banco"].ToString();

                    myCadenaSQL = "insert into Cheques (Numero, FechaEmision, FechaCobro, Importe, Activo, Banco, EnCartera) values (" +
                                            iNumero + ", '" +
                                            sFechaEmision + "', '" +
                                            sFechaCobro + "', " +
                                            dImporte + ", 1, '" +
                                            sBanco + "', 1 )";
                                            

                    //Controlar tipo de conexion
                    if (clsGlobales.ConB == null)
                    {
                        clsDataBD.GetSql(myCadenaSQL);
                         // Tomo el Id del cheque grabado para grabar en la intermedia ChequesRecibos
                        iIdCheque = clsDataBD.RetornarUltimoId("Cheques", "IdCheque");
                        // Grabo en la intermedia
                        myCadenaSQL = "insert into ChequesRecibos(IdRecibo, IdCheque) values (" +
                                        iLastRecibo + ", " +
                                        iIdCheque + ")";
                        clsDataBD.GetSql(myCadenaSQL);
                   
                    }
                    else
                    {
                        clsDataBD.GetSqlB(myCadenaSQL);

                        iIdCheque = clsDataBD.RetornarUltimoIdB("Cheques", "IdCheque");

                        // Grabo en la intermedia
                        myCadenaSQL = "insert into ChequesRecibos(IdRecibo, IdCheque) values (" +
                                        iLastRecibo + ", " +
                                        iIdCheque + ")";
                        clsDataBD.GetSqlB(myCadenaSQL);
                    }

                    
                }

              /********************************************
              * Actualizo el saldo de la caja de efectivo *
              ********************************************/
                clsDataBD.ActualizarSaldoPorCaja("SaldoCheques", Cheques);
                
            }
        }

        #endregion

        #region CargarCajaOp - Metodo que carga los datos de la caja (Ordenes de Pago)

        public static void CargarCajaOp(int iLastRecibo = 0, double p_dBancos=0, int p_IdAsociacion = 0)

        // Par'ametro de entrada
        // 1 Transferencias
        // 2 Mercado Pago
        // 3 Débito
        // 4 Credito
        {
            /*****************************************
             * Cargo los movimientos a la tabla caja *
             ****************************************/
            int iAsocioacion = p_IdAsociacion;
            // Variable que almacena si el movimiento es una entrada
            int iEsEntrada = 0;
            // Variable que almacena el id de la caja
            int iIdCaja = 0;
            //Bancos
            double bancos = p_dBancos;
            // Si es entrada, le asigno el uno para la cadena SQL
            if (Entrada)
            {
                iEsEntrada = 1;
            }
            // Si no es una entrada
            if (!(Entrada))
            {
                // Paso a todos los montos a negativo
                Monto = Monto * (-1);
                Debito = Debito * (-1);
                Credito = Credito * (-1);
                Efectivo = Efectivo * (-1);
                Transferencia = Transferencia * (-1);
                MP = MP * (-1);
                Contra = Contra * (-1);
                bancos = bancos * (-1);
            }
            // Armo la cadena SQL
            string myCadenaSQL = "insert into Caja(Entrada, MontoTotal, IdCajaImputacion, FechaS, Fecha, Debito, Credito, Efectivo, Transferencia," +
                                  "MP, Contra, Cheques, Movimiento, Comprobante, Bancos) values (" +
                                    iEsEntrada + ", " +
                                    Monto + ", " +
                                    IdCajaImputacion + ", '" +
                                    clsValida.ConvertirFecha(Fecha) + "', '" +
                                    Fecha.ToShortDateString() + "', " +
                                    Debito + ", " +
                                    Credito + ", " +
                                    Efectivo + ", " +
                                    Transferencia + ", " +
                                    MP + ", " +
                                    Contra + ", " +
                                    Cheques + ", '" +
                                    Movimiento + "', '" +
                                    Comprobante + "', " +
                                    bancos + ")";
            // Ejecuto la cadena para grabar el movimiento en la Caja
            if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaSQL); } else { clsDataBD.GetSqlB(myCadenaSQL); }
            // Averiguo el Id del movimiento de caja recién creado
            if (clsGlobales.ConB == null) {iIdCaja = clsDataBD.RetornarUltimoId("Caja", "IdCaja");} else { iIdCaja = clsDataBD.RetornarUltimoIdB("Caja", "IdCaja"); }
            
            // variable para la cadena SQL de los diferentes tipos de caja
            string myCadenaCaja = "";

            /********************************************
            * Cargo los movimientos a la tabla Efectivo *
            ********************************************/ 
            // Si Hay efectivo o contrareembolso
            if (Efectivo != 0)
            {
                // Si es efectivo el Id de CajaEfectivoTipo es 1
                if (Efectivo != 0)
                {
                    // Armo la cadena SQL
                    myCadenaCaja = "insert into CajaEfectivo (IdCaja, IdCajaEfectivoTipo, Importe) values (" +
                                    iIdCaja + ", 1," +
                                    Efectivo + ")";
                    // Ejecuto la cadena para grabar el movimiento en la Caja
                    if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaCaja); } else { clsDataBD.GetSqlB(myCadenaCaja); }

                    /********************************************
                    * Actualizo el saldo de la caja de efectivo *
                    ********************************************/
                    clsDataBD.ActualizarSaldoPorCaja("SaldoEfectivo", Efectivo);

                }               
            }

            /*************************************************
            * Cargo los movimientos a la tabla Trasferencias *
            *************************************************/
            // Bancos
            if (bancos != 0)
            {

                    // CAJA BANCOS
                    myCadenaCaja = "insert into CajaBancos (IdCajaAsociacion, IdCaja, Importe) values (" +
                                    iAsocioacion + ", " + iIdCaja + ", " +
                                    bancos + ")";
                    // Ejecuto la cadena para grabar el movimiento en la Caja
                    if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaCaja); } else { clsDataBD.GetSqlB(myCadenaCaja); }              
            }

                      
            //UPDATE CAJA SALDOS (BANCOS)
            // CAJA BANCOS
            myCadenaCaja = "Update CajaSaldos SET SaldoBancos+= " + bancos;
            // Ejecuto la cadena para grabar el movimiento en la Caja
            if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaCaja); } else { clsDataBD.GetSqlB(myCadenaCaja); }

            //CAJA ASOCIACION
            myCadenaCaja = "Update CajaAsociacionesCuentas SET Saldo+= " + bancos + " WHERE IdCajaAsociaciones = " + iAsocioacion;
            // Ejecuto la cadena para grabar el movimiento en la Caja
            if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaCaja); } else { clsDataBD.GetSqlB(myCadenaCaja); }

            //CHEQUES
            if (Cheques != 0)
            {
                int iIdCheque = 0;
                myCadenaSQL = "select * from Temporal_DetalleCheques where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario;
                DataTable myDt = new DataTable();
                // Ejecuto la cadena para grabar el movimiento en la Caja
                if (clsGlobales.ConB == null) { myDt = clsDataBD.GetSql(myCadenaSQL); } else { myDt=clsDataBD.GetSqlB(myCadenaSQL); }
                // Recorro la temporal y grabo el cheque en la tabla
                foreach (DataRow myRow in myDt.Rows)
                {
                    int iNumero = Convert.ToInt32(myRow["Numero"]);
                    string sFechaEmision = myRow["FechaEmision"].ToString();
                    string sFechaCobro = myRow["FechaCobro"].ToString();
                    double dImporte = Convert.ToDouble(myRow["Importe"]);
                    string sBanco = myRow["Banco"].ToString();

                    myCadenaSQL = "insert into Cheques (Numero, FechaEmision, FechaCobro, Importe, Activo, Banco, EnCartera) values (" +
                                            iNumero + ", '" +
                                            sFechaEmision + "', '" +
                                            sFechaCobro + "', " +
                                            dImporte + ", 1, '" +
                                            sBanco + "', 1 )";
                                            

                    //Controlar tipo de conexion
                    if (clsGlobales.ConB == null)
                    {
                        clsDataBD.GetSql(myCadenaSQL);
                         // Tomo el Id del cheque grabado para grabar en la intermedia ChequesRecibos
                        iIdCheque = clsDataBD.RetornarUltimoId("Cheques", "IdCheque");
                        // Grabo en la intermedia
                        myCadenaSQL = "insert into ChequesRecibos(IdRecibo, IdCheque) values (" +
                                        iLastRecibo + ", " +
                                        iIdCheque + ")";
                        clsDataBD.GetSql(myCadenaSQL);
                   
                    }
                    else
                    {
                        clsDataBD.GetSqlB(myCadenaSQL);

                        iIdCheque = clsDataBD.RetornarUltimoIdB("Cheques", "IdCheque");

                        // Grabo en la intermedia
                        myCadenaSQL = "insert into ChequesRecibos(IdRecibo, IdCheque) values (" +
                                        iLastRecibo + ", " +
                                        iIdCheque + ")";
                        clsDataBD.GetSqlB(myCadenaSQL);
                    }

                    
                }

              /********************************************
              * Actualizo el saldo de la caja de efectivo *
              ********************************************/
                clsDataBD.ActualizarSaldoPorCaja("SaldoCheques", Cheques);
                
            }
        }

        #endregion

        #region RetornarDatosTemporal()

        private static int RetornarDatosTemporal(int pTipoMov)
        {
            int IdCajaAsociacion = 0;

            string sSQL = "Select IdCajaAsociaciones from Temporal_DetallePagoCaja Where IdUsuario = " + clsGlobales.UsuarioLogueado.IdUsuario + " AND TipoMov = " + pTipoMov;

            DataTable myData = new DataTable();
            // Ejecuto la cadena para grabar el movimiento en la Caja
            if (clsGlobales.ConB == null) { myData = clsDataBD.GetSql(sSQL); } else { myData = clsDataBD.GetSqlB(sSQL); }

            foreach (DataRow row in myData.Rows)
            {
                if (row["IdCajaAsociaciones"] == DBNull.Value)
                {
                    IdCajaAsociacion = 0;
                }
                else
                {
                    IdCajaAsociacion = Convert.ToInt32(row["IdCajaAsociaciones"]);
                }
                break;
            }
            
            //Retornar
            return IdCajaAsociacion;
        }
        
        #endregion  

        #region Método que limpia las propiedades de la clase

        public static void LimpiarCaja()
        {
            Entrada=false;
            Monto= 0;
            IdCajaImputacion = 0;
            Fecha = DateTime.Now;
            Debito = 0;
            Credito=0;
            Efectivo=0;
            Transferencia=0;
            MP=0;
            Contra=0;
            Cheques=0;
            Movimiento="";
            Comprobante="";
        }

        #endregion

    }
}

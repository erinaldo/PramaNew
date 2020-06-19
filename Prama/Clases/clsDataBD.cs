using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace Prama
{
    class clsDataBD
    {
        #region Metodo para cargar la consulta SQL en DataAdapter

        public static DataTable GetSql(string mySql)
        {
            //DataTable para retorno
            DataTable myDataTable = new DataTable();
            //Abro la conexión a la base de datos
            clsGlobales.Con.Open();
            //Declaro el comando y le pasos los parámetros            
            SqlCommand cmd = new SqlCommand(mySql, clsGlobales.Con);
            // Declaro el Adaptador y lo instancio
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //Paso los datos de la consulta al DataAdapter
            adapter.Fill(myDataTable);
            //Cierro la Conexión
            clsGlobales.Con.Close();
            //Retornar DataTable
            return myDataTable;
        }

        #endregion

        #region Metodo para cargar la consulta SQL en DataAdapter

        public static DataTable GetSqlB(string mySql)
        {
            //DataTable para retorno
            DataTable myDataTable = new DataTable();
            // Creo un datatable y le paso los datos de la consulta SQL
            clsGlobales.ConB.Open();
            //Declaro el comando y le pasos los parámetros            
            SqlCommand cmd = new SqlCommand(mySql, clsGlobales.ConB);
            // Declaro el Adaptador y lo instancio
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //Paso los datos de la consulta al DataAdapter
            adapter.Fill(myDataTable);
            //Cierro la Conexión
            clsGlobales.ConB.Close();
            //Retornar DataTable
            return myDataTable;
        }

        #endregion
        
        #region Método para cargar los combos

        public static void CargarComboStoreProcedure(ComboBox myCombo, string Procedimiento, int Parametro, string strMiembro, string strValor)
        {
            // Creo la tabla
            DataTable myDataTable = new DataTable();
            // Llamo al método que lee la base de datos y cargo las filas a la tabla
            myDataTable = GetSql("exec " + Procedimiento + " " + Parametro);
            // Asigno los valores recibidos de la consulta al combo
            myCombo.DisplayMember = strMiembro;
            myCombo.ValueMember = strValor;
            myCombo.DataSource = myDataTable;
        }
        
        public static void CargarCombo(ComboBox myCombo, string strTabla, string strMiembro, string strValor)
        {
            // Creo la tabla
            DataTable myDataTable = new DataTable();
            // Llamo al método que lee la base de datos y cargo las filas a la tabla
            myDataTable = GetSql("SELECT * FROM " + strTabla + " ORDER BY " + strMiembro);
            // Asigno los valores recibidos de la consulta al combo
            myCombo.DisplayMember = strMiembro;
            myCombo.ValueMember = strValor;
            myCombo.DataSource = myDataTable;
            
        }


        public static void CargarComboOrden(ComboBox myCombo, string strTabla, string strMiembro, string strValor, string strValOrden)
        {
            // Creo la tabla
            DataTable myDataTable = new DataTable();
            // Llamo al método que lee la base de datos y cargo las filas a la tabla
            myDataTable = GetSql("SELECT * FROM " + strTabla + " ORDER BY " + strValOrden);
            // Asigno los valores recibidos de la consulta al combo
            myCombo.DisplayMember = strMiembro;
            myCombo.ValueMember = strValor;
            myCombo.DataSource = myDataTable;

        }

        public static void CargarComboTipoComprobanteVenta(ComboBox myCombo, string strTabla, string strMiembro, string strValor)
        {
            // Creo la tabla
            DataTable myDataTable = new DataTable();
            // Llamo al método que lee la base de datos y cargo las filas a la tabla
            myDataTable = GetSql("SELECT * FROM " + strTabla + " WHERE IdTipoComprobante IN (1,3,6,8) ORDER BY " + strValor);
            // Asigno los valores recibidos de la consulta al combo
            myCombo.DisplayMember = strMiembro;
            myCombo.ValueMember = strValor;
            myCombo.DataSource = myDataTable;

        }

        public static void CargarComboTipoResponsable(ComboBox myCombo, string strTabla, string strMiembro, string strValor)
        {
            // Creo la tabla
            DataTable myDataTable = new DataTable();
            // Llamo al método que lee la base de datos y cargo las filas a la tabla
            myDataTable = GetSql("SELECT * FROM " + strTabla + " ORDER BY " + strValor);
            // Asigno los valores recibidos de la consulta al combo
            myCombo.DisplayMember = strMiembro;
            myCombo.ValueMember = strValor;
            myCombo.DataSource = myDataTable;

        }

        public static void CargarComboFormaPago(ComboBox myCombo, string strTabla, string strMiembro, string strValor)
        {
            // Creo la tabla
            DataTable myDataTable = new DataTable();
            // Llamo al método que lee la base de datos y cargo las filas a la tabla
            myDataTable = GetSql("SELECT * FROM " + strTabla + " ORDER BY " + strValor);
            // Asigno los valores recibidos de la consulta al combo
            myCombo.DisplayMember = strMiembro;
            myCombo.ValueMember = strValor;
            myCombo.DataSource = myDataTable;

        }

        public static void CargarCombo(ComboBox myCombo, string strTabla, string strMiembro, string strValor, string sql_where="", int p_Con=0)
        {
            // Creo la tabla
            DataTable myDataTable = new DataTable();
            // Llamo al método que lee la base de datos y cargo las filas a la tabla
            if (p_Con==0)
            {
                if (sql_where != "")
                {
                    myDataTable = GetSql("SELECT * FROM " + strTabla + " WHERE " + sql_where + " ORDER BY " + strMiembro);
                }
                else
                {
                    myDataTable = GetSql("SELECT * FROM " + strTabla + " ORDER BY " + strMiembro);
                }

            }
            else
            {
                if (clsGlobales.ConB == null)
                {
                    if (sql_where != "")
                    {
                        myDataTable = GetSql("SELECT * FROM " + strTabla + " WHERE " + sql_where + " ORDER BY " + strMiembro);
                    }
                    else
                    {
                        myDataTable = GetSql("SELECT * FROM " + strTabla + " ORDER BY " + strMiembro);
                    }
                }
                else
                {
                    if (sql_where != "")
                    {
                        myDataTable = GetSqlB("SELECT * FROM " + strTabla + " WHERE " + sql_where + " ORDER BY " + strMiembro);
                    }
                    else
                    {
                        myDataTable = GetSqlB("SELECT * FROM " + strTabla + " ORDER BY " + strMiembro);
                    }
                }
            }
            // Asigno los valores recibidos de la consulta al combo
            myCombo.DisplayMember = strMiembro;
            myCombo.ValueMember = strValor;
            myCombo.DataSource = myDataTable;
        }


        #endregion

        #region Método que me devuelve el último Id de una tabla

        public static int RetornarUltimoId(string Tabla, string CampoId)
        {
            // Variable que almacena el ID
            int myId = 0;
            // Cadena SQL 
            string myCadena = "select * from " + Tabla;
            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            mDtTable = clsDataBD.GetSql(myCadena);
            // Recorro la tabla para buscar el mayor número de ID
            foreach (DataRow row in mDtTable.Rows)
            {
                // SI el valor de la fila actual es mayor que el almacenado, lo reemplazo y sigo con el próximo
                if (Convert.ToInt32(row[CampoId]) > myId)
                {
                    myId = Convert.ToInt32(row[CampoId]);
                }
            }
            // Devuelvo el valor máximo encontrado
            return myId;
        }

        #endregion

        #region Método que me devuelve el último Id (b) de una tabla

        public static int RetornarUltimoIdB(string Tabla, string CampoId)
        {
            // Variable que almacena el ID
            int myId = 0;
            // Cadena SQL 
            string myCadena = "select * from " + Tabla;
            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            mDtTable = clsDataBD.GetSqlB(myCadena);
            // Recorro la tabla para buscar el mayor número de ID
            foreach (DataRow row in mDtTable.Rows)
            {
                // SI el valor de la fila actual es mayor que el almacenado, lo reemplazo y sigo con el próximo
                if (Convert.ToInt32(row[CampoId]) > myId)
                {
                    myId = Convert.ToInt32(row[CampoId]);
                }
            }
            // Devuelvo el valor máximo encontrado
            return myId;
        }

        #endregion

        #region Método que me devuelve el maximo valor en un campo dado 

        public static int RetornarMax(string Tabla, string Campo, int p_id = 0)
        {
            // Variable que almacena el valor
            int myValor = 0;
            string myCadena = "";
            // Cadena SQL 
            if (p_id == 0)
            {
                myCadena = "select max(" + Campo + ") as maximo from " + Tabla;
            }
            else
            {
                myCadena = "select max(" + Campo + ") as maximo from " + Tabla + " Where IdUsuario = " + p_id; 
            }
            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            mDtTable = clsDataBD.GetSql(myCadena);
            // Recorro la tabla para buscar el mayor número de ID
            foreach (DataRow row in mDtTable.Rows)
            {
                if (row["maximo"] == DBNull.Value)
                {
                    myValor = 0;
                }
                else
                {
                    myValor = Convert.ToInt32(row["maximo"]);
                }
                break;
            }
            // Devuelvo el valor máximo encontrado
            return myValor;
        }

        #endregion

        #region Método que me devuelve el maximo valor en un campo dado

        public static int RetornarMax(string Tabla, string Campo, string sWhere)
        {
            // Variable que almacena el valor
            int myValor = 0;
            // Cadena SQL 
            string myCadena = "select max(" + Campo + ") as maximo from " + Tabla + " WHERE " + sWhere;
            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            mDtTable = clsDataBD.GetSql(myCadena);
            // Recorro la tabla para buscar el mayor número de ID
            foreach (DataRow row in mDtTable.Rows)
            {
                if (row["maximo"] == DBNull.Value)
                {
                    myValor = 0;
                }
                else
                {
                    myValor = Convert.ToInt32(row["maximo"]);
                }
                break;
            }
            // Devuelvo el valor máximo encontrado
            return myValor;
        }

        #endregion

        #region Método que me devuelve el último numero de comprobante para Punto Venta dado
               
        public static int getUltComp(string p_Campo, int p_Punto, int p_Con)
        {
            // Variable que almacena el valor
            int myValor = 0;

            string myCadena = "select " + p_Campo + " from PuntosVentaAFIP where Punto = " + p_Punto;
            // Llamamos al método para cargar los datos en el Source, pasando los parámetros de la consulta. G.
            DataTable mDtTable = new DataTable();
            if (p_Con == 0)
            {
                mDtTable = clsDataBD.GetSql(myCadena);
            }
            else
            {
                mDtTable = clsDataBD.GetSqlB(myCadena);
            }
            // Recorro la tabla para buscar el mayor número de ID
            foreach (DataRow row in mDtTable.Rows)
            {
               myValor = Convert.ToInt32(row[p_Campo]);
            }
            // Devuelvo el valor encontrado
            return myValor;
        }

        #endregion

        #region Método que actualiza los saldos de las cajas

        public static void ActualizarSaldoPorCaja(string sCaja, double dImporte)
        {
            // Armo la cadena SQL
            string myCadenaSQL = "update CajaSaldos set " + sCaja + " = " + sCaja + " + " + dImporte;
            // Ejecuto la cadena para grabar el movimiento en la Caja
            if (clsGlobales.ConB == null) { clsDataBD.GetSql(myCadenaSQL); } else { clsDataBD.GetSqlB(myCadenaSQL); }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;

namespace Prama
{
    class clsValida
    {
     

        #region Metodo: IsNumeric
        public bool IsNumeric(object Expression)
        {

            bool isNum;

            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

            return isNum;

        }
        #endregion 
        
        #region METODO EsFecha (VALIDA FECHA)
        public  Boolean EsFecha(String fecha)
        {
            try
            {
                DateTime.Parse(fecha);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Metodo: AccesoInternet
        //***********************************************
        // Metodo       : AccesoInternet
        // Parametros   : Ninguno
        // Autor        : Developer
        // Fecha        : 2016-10-30
        // Proposito:   : Valida si hay acceso a internet
        //***********************************************
        public bool AccesoInternet() {

            try
            {
                System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry("www.prama.com.ar");
                return true;

            }
            catch 
            {
               return false;
            }

        }

        #endregion

        #region Método: Convierte fecha a string para SQL

        public static string ConvertirFecha(DateTime fecha)
        {
            // Variable que va a retornar la fecha para SQL
            string FechaConvertida = "";
            // El día
            string Dia = fecha.Day.ToString();
            // Si el día tiene un solo caracter, le agrego un 0 adelante
            if (Dia.Length == 1)
            {
                Dia = "0" + Dia;
            }
            // El mes
            string Mes = fecha.Month.ToString();
            // Si el Mes tiene un solo caracter, le agrego un 0 adelante
            if (Mes.Length == 1)
            {
                Mes = "0" + Mes;
            }
            // El año
            string Ano = fecha.Year.ToString();
            // Armo la fecha en formato M/D/A
            FechaConvertida = Dia + "/" + Mes + "/" + Ano;

            return FechaConvertida;
        }

        #endregion

        #region Método: Convierte entero a String de 8 posiciones para grabar Nro de comprobante

        public static string ConvertirNumeroComprobante(string Numero,int posicionesNumero)
        {
            string numeroConvertido = "";

            switch (posicionesNumero)
            {
                case 1:
                    numeroConvertido = "0000000";
                    break;
                case 2:
                    numeroConvertido = "000000";
                    break;
                case 3:
                    numeroConvertido = "00000";
                    break;
                case 4:
                    numeroConvertido = "0000";
                    break;
                case 5:
                    numeroConvertido = "000";
                    break;
                case 6:
                    numeroConvertido = "00";
                    break;
                case 7:
                    numeroConvertido = "0";
                    break;
                case 8:
                    numeroConvertido = "";
                    break;
            }
            numeroConvertido = numeroConvertido + Numero;
            return numeroConvertido;
        }

        #endregion

        #region Método: Convierte a string fecha de vencimiento del comprobante por la condición de Compra-Venta

        public static string ConvertirFechaVencimiento(string Tabla, int IdCondicion)
        {
            // Creo la variable que va a almacenar la fecha de vencimiento convertida
            string fechaConvertida = "";
            // Creo la variable que va a almacenar la cantidad de días de plazo de la condición
            int cantDias = 0;
            // Si es la de compras
            if (Tabla == "Compras")
            {
                // Armo la cadena SQL
                string myCadenaSQL = "Select * from CondicionesCompra where IdCondicionCompra = " + IdCondicion;
                // Creo una tabla que me va a almacenar el resultado de la consulta
                DataTable myTabla = clsDataBD.GetSql(myCadenaSQL);
                // Recorro la tabla para almacenar el último numero usado y sumarle 1
                foreach (DataRow row in myTabla.Rows)
                {
                    // Almaceno en la variable el número que corresponde a la próxima cotización
                    cantDias = ((Convert.ToInt32(row["PlazoPago"])));
                }
                // Le asigno a la variable de retorno el valor de la fecha ya convertida sumando al día de hoy, la cantidad de días de la condición
                fechaConvertida = ConvertirFecha(DateTime.Now.AddDays(cantDias));
            }

            return fechaConvertida;
        }

        #endregion

        #region Método que redimenciona un vector

        public static System.Array ResizeVector(System.Array oldArray, int newSize)
        {
            int oldSize = oldArray.Length;
            System.Type elementType = oldArray.GetType().GetElementType();
            System.Array newArray = System.Array.CreateInstance(elementType, newSize);
            int preserveLength = System.Math.Min(oldSize, newSize);
            if (preserveLength > 0)
                System.Array.Copy(oldArray, newArray, preserveLength);
            return newArray;
        }

        public static Array ResizeMatriz(Array arr, int[] newSizes)
        {
            if (newSizes.Length != arr.Rank)
                throw new ArgumentException("arr must have the same number of dimensions " +
                                            "as there are elements in newSizes", "newSizes");

            var temp = Array.CreateInstance(arr.GetType().GetElementType(), newSizes);
            int length = arr.Length <= temp.Length ? arr.Length : temp.Length;
            Array.ConstrainedCopy(arr, 0, temp, 0, length);
            return temp;
        }

        #endregion

        #region Metodo: ExisteElemento

        public int ExisteElemento(string p_cadSQL)
        {
            //Variable de retorno
            int nElementos = 0;

            //Crear el DataTable con la consulta.
            DataTable mDataTable = clsDataBD.GetSql(p_cadSQL);

            //Verificar si hay elementos en la consulta.
            foreach (DataRow row in mDataTable.Rows)
            {
                nElementos = Convert.ToInt32(row["nElementos"]);
            }

            return nElementos;
        }

        #endregion

    }
}

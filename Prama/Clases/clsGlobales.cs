using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
// using Microsoft.SqlServer.Management.Smo;
// using Microsoft.SqlServer.Management.Common;
using System.Drawing.Text;
using System.Drawing;
using System.ComponentModel;
using System.Globalization;
using System.Configuration;

namespace Prama
{
    class clsGlobales
    {

        #region Elementos de la conexión a la base de datos
        
        // Cadena de conexionon
        public static string SqlCadConexion = ""; //ConfigurationManager.ConnectionStrings["myCadCon3"].ToString(); //Prama
        // Cadena de conexion resguardo
        public static string SqlCadConexion2 = ""; //ConfigurationManager.ConnectionStrings["myCadCon5"].ToString(); 
        //Command        
        public static SqlCommand cmd = new SqlCommand();
        // Indicamos que conexión usaremos para Sql.
        public static SqlConnection Con; //= new SqlConnection(SqlCadConexion);
        //Conexion resguardo
        public static SqlConnection ConB = null; 

        #endregion

        #region Variables Públicas de la aplicación

        //VARIABLE TIPO DOCUMENTO CRYSTAL
        public static CrystalDecisions.CrystalReports.Engine.ReportDocument myRptDoc;
        
        //VARIABLES PARA CLIENTES EN BUSQUEDA DE LOCALIDAD, PROVINCIA Y CP
        public static int IdProv = 0;
        public static int IdLoc = 0;
        public static string CP = "";

        // Variable de la clase clsUsuario que va a almacenar el usuario logeado al sistema
        public static clsUsuarios UsuarioLogueado;
        // Validaciones 
        public static clsValida cValida = new clsValida();
        // Formatos
        public static clsFormato cFormato = new clsFormato();
        
        // Parametros
        public static clsParametros cParametro = new clsParametros();
        public const int gParametro = 1;

        //Para verificar si se llama a formulario de usuarios para cambiar usuario N. 12-05-2016
        public static bool bChangeUser = false;
        //para controlar salida de la app. N. 04-10-2015
        public static bool bFlag = false;
        // Variable a nivel aplicación para actuar como bandera en todos los formularios. G.
        public static bool bBandera = false;
        // Variable para los formularios de compra que controla cuando se preciona el botón btnPedir
        public static bool bCompras = false;
        // Variable a nivel aplicación que me indica el tipo de estado para los formularios ABM.
        // Puede tomar los siguientes valores: A ALTA, M Modificación, C ESPERA, B Búsqueda N.
        public static string myEstado = "C";
        //Revendedor Flag Lectura Excel
        public static bool bRevCambio = false;
        //Flag Nuevo Excel
        public static bool bNuevoExcel = false;
        //Para composicion compuesta
        public static bool bCargoProdCompto = false;
        //Otros Vectores
        public static int[] ClientesSeleccionados = new int[] { };
        public static int[] ProveedoresSeleccionados = new int[] { };
        
        // Declaro los vectores que voy a usa en la carga de los artículos a la grilla
        public static double[,] ProdSelCompuesto = new double[0, 3] { };
        public static double[,] ProdSelPedPresu = new double[0, 3] { };
        public static double[,] ProductosSeleccionados = new double[0,2] {};
        public static double[,] InsumosSeleccionados = new double[0,2] {};
        
        // Declaro un vector para los datos de los comprobantes a cargar para la selección múltiple (Id de Comprobante y Id de Tipo)
        public static int[] ComprobantesSeleccionados = new int[] { };

        /////////////////////////////////////////////////////////////////////
        //Declaro la matriz y vectores que voy a usar para la composicion N.
        public static string[,] ProductoComposicion = new string[0, 9]{}; //para grilla
        
        public static double[] ProductoDatos = new double[6];   //para datos de producto
        public static string[] ProductoDatosST = new string[6];
    
        public static string[,] ProductosEliminados = new string[0, 2]{} ; //para eliminados
        //********************************************************************/

        public static bool bCambio;   //Flag para ver si estando modificando, realizo un cambio en la composicion.

        // Variable que almacena en todos los formularios con grilla la posición de la fila seleccionada
        public static int indexFila = 0;

        //Certificado
        public static string CertificadoAFIP = "";
        
        //Matriz para Articulos desde Excel
        public static string[,] ArtExcel = new string[0, 13] { }; //para grilla

        // Variables de fabricación 
        public static int CantFabricada;
        public static DateTime FechaFabricada;
        public static string EmpleadoFabricado;

        public static DateTime FechaDesde;
        public static DateTime FechaHasta;
        public static bool Individual;
        public static bool Programa;
        public static int IdEmpleadoFabricado;

        public static bool bView =true;

        public static bool bInterrupt = false; //para exportacion de clientes
   
        //Destino para archivos Excel
        public static string sDestinoFs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        // Variable para los pagos que involucren cuentas de bancos
        public static double dTotalAAcreditar;

        // Mi PC de escritorio
        public static string sNumeroProcesador = "178BFBFF00630F82"; //"178BFBFF00630F81";

        #endregion

        #region Método para leer archivos planos

        public static string LeerTXT(string sArchivo)
        {
            // Creo la variable que almacena la línea leída
            string sLineaLeida;
            // Leo la línea
            System.IO.StreamReader file = new System.IO.StreamReader(sArchivo);
            // Asigno a la variable el contenido de la línea
            sLineaLeida = file.ReadLine();
            // Cierro el archivo
            file.Close();
            // Devuelvo la línea 
            return sLineaLeida;
        }

        #endregion

        #region Funcion MID (equivalente a la de VB)
        public static string Mid(string param, int startIndex)
        {
            //start at the specified index and return all characters after it
            //and assign it to a variable
            string result = param.Substring(startIndex);
            //return the result of the operation
            return result;
        }
        #endregion

        #region Funcion MID Reloaded
        public static string Mid(string param, int startIndex, int length)
        {
            //start at the specified index in the string ang get N number of
            //characters depending on the lenght and assign it to a variable
            string result = param.Substring(startIndex, length);
            //return the result of the operation
            return result;
        }
        #endregion

        #region Function LEFT ( para cadena, idem VB )
        public static string Left(string param, int length)
        {
            //we start at 0 since we want to get the characters starting from the
            //left and with the specified lenght and assign it to a variable
            string result = param.Substring(0, length);
            //return the result of the operation
            return result;
        }
        #endregion

        #region Funcion RIGHT ( para cadena, idem VB )
        public static string Right(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }
        #endregion
    }
}

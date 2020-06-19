using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using Prama.Clases;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Prama
{
    class clsFormato
    {

        #region Metodo: CUITFormateado
        /*
         * Metodo       : CUITFormateado
         * Proposito    : Retornar la CUIT con guiones
         * Retorna      : String
         * Parametros   : CUIT Vàlida
         * Autor        : N.
         * */
        public string CUITFormateado(string CUIT)
        {
                if (CUIT.Length == 0) return string.Empty;
                return CUIT.Substring(0, 2) + "-" +
                       CUIT.Substring(2, 8) + "-" +
                       CUIT.Substring(10);
        }
        #endregion

        #region Metodo CUITGuiones

        public string CUITGuiones(string CUIT, int p_Guion)
        {
                string myRetorno = CUIT;

                if (CUIT.Length == 0) return string.Empty;

                switch(p_Guion)
                {
                    case 1: //guiones

                       if (CUIT.Length == 13) return CUIT;
     
                       myRetorno = clsGlobales.Left(CUIT,2) + "-" +
                       clsGlobales.Mid(CUIT,3,8) + "-" +
                       clsGlobales.Right(CUIT,1);
                       break;

                    case 2: //sin guiones

                       if (CUIT.Length == 11) return CUIT;
 
                       myRetorno = clsGlobales.Left(CUIT,2) + 
                       clsGlobales.Mid(CUIT,3,8) + 
                       clsGlobales.Right(CUIT,1);
                       break;
                }

                //retornar
                return myRetorno;
        }

        #endregion

        #region Metodo : getTituloVentana

        //Retorna Nombre Fantasia para titulo de ventanas
        public string getTituloVentana()
        {
          return clsGlobales.cParametro.NombreFantasia;           
        }

        #endregion

        #region Metodo: LeerExcel

        //METODO QUE LEE EL ARCHIVO EXCEL CON LOS PRODUCTOS Y LOS TRAE AL VECTOR
        public bool LeerExcel(string p_Archivo, int CantCol, bool p_Rev = false, bool p_Dist = false)
        {
            int filas = 0;
            int fila = 0;
            string sCadSQL = "";
            double pub = 0;
            double rev = 0;
            double sUnitConIva = 0;
            double total = 0;
            double iva = 0;
            bool bRetorno = true;

            //abrimos el dialogo para poder obtener el nombre la ubicacion del archivo
            string sArchivo = p_Archivo;

            //declaramos las variables que necesitamos para poder abrir un archivo excel
            _Application exlApp;
            Workbook exlWbook;
            Worksheet exlWsheet;

            //Abrir Excel App
            exlApp = new Microsoft.Office.Interop.Excel.Application();

            //Validar que este todo ok con excel.
            if (exlApp == null)
            {
                MessageBox.Show("Microsoft Excel no se encuentra correctamente instalado!!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bRetorno = false;
                return bRetorno;
            }

            try
            {

                //Asignamos el libro que sera abierot
              /*exlWbook = exlApp.Workbooks.Open(sArchivo, 0, true,5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, true, 0, true, 1, 0);*/ //ORIGINAL
                exlWbook = exlApp.Workbooks.Open(sArchivo, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,"\t", false, false, 0, true, 1, 0);
                //NEW TO TEST
                exlWsheet = (Worksheet)exlWbook.Worksheets.get_Item(1);
                Range exlRange;
                string sCant, sCod, sDesc, sFracc, sUnit, sTotal;
                string strTitTot = "";

                double sUnitSinIva = 0;
                double sDif = 0;

                //Definimos el el rango de celdas que seran leidas
                exlRange = exlWsheet.UsedRange;

                //Cantidad Columnas, tomar leyenda celda correspondiente para ver si es nuestro excel.
                if (CantCol == 6)
                {
                    strTitTot = (exlRange.Cells[1, 5] as Range).Value;
                }
                else
                { strTitTot = (exlRange.Cells[1, 6] as Range).Value; }

                //Verificar leyenda obtenida
                if (strTitTot == "TOTAL $")
                {
                    //.T. es un excel generado por nuestro sistema               
                    clsGlobales.bNuevoExcel = true;

                    //SI EL CLIENTE ES PUBLICO Y EL ARCHIVO ES REVENDEDOR, MENSAJE Y SALIR
                    if (p_Rev == false && p_Dist == false && CantCol == 7)
                    {
                        MessageBoxTemporal.Show("El cliente es 'PUBLICO' y el archivo que intenta cargar es de 'REVENDEDOR' o 'DISTRIBUIDOR'!", "Error!", 5, true);
                        bRetorno = false;
                        return bRetorno;
                    }
                    else if (p_Rev && p_Dist == false && CantCol == 6)
                    {
                        MessageBoxTemporal.Show("El cliente es 'REVENDEDOR' y el archivo que intenta cargar es de 'MOSTRADOR'!", "Error!", 5, true);
                        bRetorno = false;
                        return bRetorno;
                    }
                    else if (p_Rev == false && p_Dist == true && CantCol == 6)
                    {
                        MessageBoxTemporal.Show("El cliente es 'DISTRIBUIDOR' y el archivo que intenta cargar es de 'MOSTRADOR'!", "Error!", 5, true);
                        bRetorno = false;
                        return bRetorno;
                    }
                    else if (p_Rev && p_Dist == false && CantCol == 7)
                    {
                        //Ver sino es el archivo de revendedor
                        if ((exlRange.Cells[2, 6] as Range).Value == "Dist")
                        {
                            MessageBoxTemporal.Show("El cliente es 'REVENDEDOR' y el archivo que intenta cargar es de 'DISTRIBUIDOR'!", "Error!", 5, true);
                            bRetorno = false;
                            return bRetorno;
                        }
                    }
                    else if (p_Rev == false && p_Dist && CantCol == 7)
                    {
                        //Ver sino es el archivo de revendedor
                        if ((exlRange.Cells[2, 6] as Range).Value == "Rev")
                        {
                            MessageBoxTemporal.Show("El cliente es 'DISTRIBUIDOR' y el archivo que intenta cargar es de 'REVENDEDOR'!", "Error!", 5, true);
                            bRetorno = false;
                            return bRetorno;
                        }
                    }

                }

                //  MessageBox.Show(exlRange.Columns.Count.ToString(),"Mensaje");

                //Recorremos el archivo excel como si fuera una matriz
                for (int i = 1; i <= exlRange.Rows.Count; i++)
                {
                    sCant = "";

                    //TRAEMOS LA COLUMNA CANTIDAD
                    sCant = (exlRange.Cells[i, 1] as Range).Value + "";
                    if (!(clsGlobales.cValida.IsNumeric(sCant))) //NO ES UN NUMERO, EN BLANCO
                    {
                        sCant = "";
                    }
                    else
                    {
                        if (Convert.ToInt32(sCant) == 0) //ES CERO, EN BLANCO
                        {
                            sCant = "";
                        }
                    }

                    //CONTAMOS LA FILA SOLO SINO ESTA VACIA LA CANTIDAD
                    if (sCant != "")
                    {
                        //Nueva fila
                        filas++;
                    }
                }

                //Resize Vector Excel acorde a la cantidad de filas que se van a precisar
                 clsGlobales.ArtExcel = (string[,])clsValida.ResizeMatriz(clsGlobales.ArtExcel, new int[] { filas, 13 });

                //CARGAMOS LOS DATOS AL VECTOR SABIEN QUE CANTIDAD DE FILAS NECESITAMOS
                for (int i = 1; i <= exlRange.Rows.Count; i++)
                {
                    sCant = "";
                    sCod = "";
                    sDesc = "";
                    sFracc = "";
                    sUnit = "";
                    sTotal = "";

                    sDif = 0;

                    //Traer los datos de las columnas
                    sCant = (exlRange.Cells[i, 1] as Range).Value + "";
                    if (!(clsGlobales.cValida.IsNumeric(sCant)))
                    {
                        sCant = "";
                    }
                    else
                    {
                        //ES 0? NO SIRVE, EN BLANCO.
                        if (Convert.ToInt32(sCant) == 0)
                        {
                            sCant = "";
                        }
                        else
                        {
                            //ALMACENAR LOS DATOS DE LAS CELDAS EN VARIABLES AUXILIARES
                            sCod = (exlRange.Cells[i, 2] as Range).Value + "";
                            sDesc = (exlRange.Cells[i, 3] as Range).Value + "";
                            sFracc = (exlRange.Cells[i, 4] as Range).Value + "";

                            //SINO ES UNO DE LOS ARCHIVOS EXCEL GENERADOS POR NOSOTROS, CONTROLAR
                            //SI VIENE ARCHIVO ERRONEO
                            if (!(clsGlobales.bNuevoExcel))
                            {
                                //SI ES REVENDEDOR Y CANTIDAD COLUMNAS ES 7...
                                if (p_Rev && CantCol == 7)
                                {
                                    //Cantidad igual o mayor a 6, entonces tomar columna 6 del excel 'Rev'
                                    if (Convert.ToInt32(sCant) >= 6)
                                    {
                                        sUnit = (exlRange.Cells[i, 6] as Range).Value + "";
                                        sTotal = (exlRange.Cells[i, 7] as Range).Value + "";
                                    }
                                    else //Sino tomar columna 5 'Mos'
                                    {
                                        sUnit = (exlRange.Cells[i, 5] as Range).Value + "";
                                        sTotal = (exlRange.Cells[i, 7] as Range).Value + "";
                                    }
                                }

                                //SI ES REVENDEDOR Y CANTIDAD DE COLUMNAS ES < 7
                                if (p_Rev && CantCol < 7)
                                {
                                    //Flag
                                    clsGlobales.bRevCambio = true;

                                    //Tomar columnas columna 5 'mos' y 6 archivo mostrador
                                    sUnit = (exlRange.Cells[i, 5] as Range).Value + "";
                                    sTotal = (exlRange.Cells[i, 6] as Range).Value + "";
                                }

                                //*******************************************************//

                                //SINO ES REVENDEDOR Y CANTIDAD DE COLUMNAS ES 7...
                                if (p_Rev == false && CantCol == 7)
                                {
                                    //Tomar columna 5 'mos'
                                    sUnit = (exlRange.Cells[i, 5] as Range).Value + "";
                                    sTotal = (exlRange.Cells[i, 7] as Range).Value + "";
                                }


                                //SINO ES REVENDEDOR Y CANTIDAD DE COLUMNAS ES < 7...
                                if (p_Rev == false && CantCol < 7)
                                {
                                    //Tomar columna 5 'mos'
                                    sUnit = (exlRange.Cells[i, 5] as Range).Value + "";
                                    sTotal = (exlRange.Cells[i, 6] as Range).Value + "";
                                }
                            }
                            else
                            {
                                if (p_Rev == true || p_Dist == true)
                                {
                                    sUnit = (exlRange.Cells[i, 6] as Range).Value + "";
                                    sTotal = (exlRange.Cells[i, 7] as Range).Value + "";
                                }
                                else
                                {
                                    sUnit = (exlRange.Cells[i, 5] as Range).Value + "";
                                    sTotal = (exlRange.Cells[i, 6] as Range).Value + "";
                                }

                            }
                        }
                    }

                    //Si la cantidad esta Ok, no esta vacìa ni es 0... la contamos
                    if (sCant != "")
                    {

                        if (clsGlobales.bRevCambio == false)
                        {
                            //Traer coeficientes cliente ( por defecto como si fuera el excel viejo )
                            if (clsGlobales.bNuevoExcel) // Si es nuestro excel, cambiamos
                            {
                                //Traer coeficientes cliente ( para nuevo excel )
                                sCadSQL = "exec ObtenerCoeficientesNuevoExcel " + Convert.ToInt32(sCod);
                            }
                            else
                            {
                                sCadSQL = "exec ObtenerCoeficientes " + Convert.ToInt32(sCod);
                            }
                            System.Data.DataTable myData = clsDataBD.GetSql(sCadSQL);

                            //Guardar Coeficientes en variables
                            foreach (DataRow row in myData.Rows)
                            {
                                //Coeficiente publico y revendedor
                                pub = Convert.ToDouble(row["pub"]);
                                rev = Convert.ToDouble(row["rev"]);
                                iva = Convert.ToDouble(row["iva"]);
                            }

                            //Unitario Sin Iva y Total descontando el IVA
                            if (!(iva == 0))
                            {
                                //Armar iva
                                iva = 1 + (iva / 100);
                                //Calculos 
                                sUnitSinIva = Convert.ToDouble(sUnit) / iva;
                                sDif = Convert.ToDouble(sUnit) - sUnitSinIva;
                            }
                            else
                            {
                                sUnitSinIva = Convert.ToDouble(sUnit);
                                sDif = 0;
                            }
                        }
                        else
                        {
                            //Traer coeficientes cliente ( por defecto como si fuera el excel viejo )
                            if (clsGlobales.bNuevoExcel) // Si es nuestro excel, cambiamos
                            {
                                //Traer coeficientes cliente ( para nuevo excel )
                                sCadSQL = "exec ObtenerCoeficientesNuevoExcel " + Convert.ToInt32(sCod);
                            }
                            else
                            {
                                sCadSQL = "exec ObtenerCoeficientes " + Convert.ToInt32(sCod);
                            }

                            System.Data.DataTable myData = clsDataBD.GetSql(sCadSQL);

                            //Guardar Coeficientes en variables
                            foreach (DataRow row in myData.Rows)
                            {
                                //Coeficiente publico y revendedor
                                pub = Convert.ToDouble(row["pub"]);
                                rev = Convert.ToDouble(row["rev"]);
                                iva = Convert.ToDouble(row["iva"]);
                            }

                            if (!(iva == 0))
                            {
                                //Armar iva
                                iva = 1 + (iva / 100);

                                //Quito IVA
                                sUnitSinIva = Convert.ToDouble(sUnit) / iva;

                                //Quitar coeficiente mostrador (publico) y asignar coef revendedor
                                sUnitSinIva = sUnitSinIva / pub;
                                sUnitSinIva = sUnitSinIva * rev;

                                //Sumar IVA y calcular nuevo tal en base a cantidad.
                                sUnitConIva = sUnitSinIva * iva;

                                //Cambiar el total
                                total = Convert.ToInt32(sCant) * sUnitConIva;
                                sDif = sUnitConIva - sUnitSinIva;
                            }
                            else
                            {
                                sUnitSinIva = Convert.ToDouble(sUnit);

                                //Quitar coeficiente mostrador (publico) y asignar coef revendedor
                                sUnitSinIva = sUnitSinIva / pub;
                                sUnitSinIva = sUnitSinIva * rev;

                                //Sumar IVA y calcular nuevo tal en base a cantidad.
                                sUnitConIva = sUnitSinIva;

                                //Cambiar el total
                                total = Convert.ToInt32(sCant) * sUnitConIva;
                                sDif = sUnitConIva - sUnitSinIva;
                            }
                        }

                        //Cargar al Vector
                        clsGlobales.ArtExcel[fila, 0] = "0";    //Item
                        clsGlobales.ArtExcel[fila, 1] = sCod;   //IdArticulo
                        clsGlobales.ArtExcel[fila, 2] = "0";    //Codigo
                        clsGlobales.ArtExcel[fila, 3] = sCant;  //Cantidad
                        clsGlobales.ArtExcel[fila, 4] = sDesc;  //Descripcion
                        clsGlobales.ArtExcel[fila, 5] = "0";    //Precio Sin Coeficiente
                        clsGlobales.ArtExcel[fila, 6] = sUnitSinIva.ToString("#0.00");  //Unitario sin iva
                        clsGlobales.ArtExcel[fila, 7] = Convert.ToDouble(sDif).ToString("#0.00"); //Subtotal sin iva
                        clsGlobales.ArtExcel[fila, 8] = "0";    //Coeficiente
                        clsGlobales.ArtExcel[fila, 9] = "0";    //Pub
                        clsGlobales.ArtExcel[fila, 10] = "0";   //Dist
                        clsGlobales.ArtExcel[fila, 11] = "0";   //Rev
                        clsGlobales.ArtExcel[fila, 12] = "1";   //Es Excel!

                        //Nueva fila
                        fila++;
                    }

                }

                //CERRAR LIBRO Y APLICACION
                exlWbook.Close(false); //.F., SIN GUARDAR
                exlApp.Quit(); //SALIR DE EXCEL

                //Variable Retorno
                return bRetorno;
            }
            catch
            {
                //Mensaje Error
                MessageBoxTemporal.Show("Se ha producido un Error al intentar leer el archivo expecificado. Consulta al Administrador del Sistema!", "Error!", 4, true);

                //.F. bandera excel nuevo
                clsGlobales.bNuevoExcel = false;

                //Setear variable retorno
                bRetorno = false;

                //CERRAR LIBRO Y APLICACION
                exlApp.Quit(); //SALIR DE EXCEL

            }

            //RETORNAR
            return bRetorno;
            
        }

        #endregion

        #region Metodo RetornarCantColumnExcel

        public  int RetornarCantColumnExcel(string p_Archivo)
        {

            //abrimos el dialogo para poder obtener el nombre la ubicacion del archivo
            string sArchivo = p_Archivo;

            //declaramos las variables que necesitamos para poder abrir un archivo excel
            _Application exlApp;
            Workbook exlWbook;
            Worksheet exlWsheet;

            exlApp = new Microsoft.Office.Interop.Excel.Application();
            
            //Asignamos el libro que sera abierot
            exlWbook = exlApp.Workbooks.Open(sArchivo, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            exlWsheet = (Worksheet)exlWbook.Worksheets.get_Item(1);
            Range exlRange;            

            //Definimos el el rango de celdas que seran leidas
            exlRange = exlWsheet.UsedRange;

            int cantidad = exlRange.Columns.Count;

            //CERRAR LIBRO Y APLICACION
            exlWbook.Close(false); //.F., SIN GUARDAR
            exlApp.Quit(); //SALIR DE EXCEL
            
            //Retornar
            return cantidad;
        }

        #endregion

        #region Metodo RetornarFechaExcel

        public DateTime RetornarFechaExcel(string p_Archivo)
        {

            //abrimos el dialogo para poder obtener el nombre la ubicacion del archivo
            string sArchivo = p_Archivo;

            //declaramos las variables que necesitamos para poder abrir un archivo excel
            _Application exlApp;
            Workbook exlWbook;
            Worksheet exlWsheet;

            exlApp = new Microsoft.Office.Interop.Excel.Application();

            //Asignamos el libro que sera abierot
            exlWbook = exlApp.Workbooks.Open(sArchivo, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            exlWsheet = (Worksheet)exlWbook.Worksheets.get_Item(1);
            Range exlRange;

            //Definimos el el rango de celdas que seran leidas
            exlRange = exlWsheet.UsedRange;

            DateTime dFecha = (exlRange.Cells[1, 4] as Range).Value;

            //CERRAR LIBRO Y APLICACION
            exlWbook.Close(false); //.F., SIN GUARDAR
            exlApp.Quit(); //SALIR DE EXCEL

            //Retornar
            return dFecha;
        }

        #endregion

        #region Metodo: ExportToExcel


        /// <summary>
        /// Export DataTable to Excel file
        /// </summary>
        /// <param name="DataTable">Source DataTable</param>
        /// <param name="ExcelFilePath">Path to result file name</param>
        public void ExportToExcel(string ExcelFilePath = null, ProgressBar p_bar = null, System.Windows.Forms.Label lblPorc = null, int p_Form = 0, System.Windows.Forms.Label p_lblCurrent = null, int p_TypeLimit = 0)
        {

            //Objeto DATATABLE
            System.Data.DataTable myDataRubro = new System.Data.DataTable();
            System.Data.DataTable myDataSubRubro = new System.Data.DataTable();
            System.Data.DataTable myDataProducto = new System.Data.DataTable();
            System.Data.DataTable myDataAux = new System.Data.DataTable();
            
            //Objeto SQL
            string myCad ="";
            string nFile = "";
            string sCampo = "";

            //Calculos
            double Porcentaje = 0;
            int cCantTot = 0;

            //Flag
            bool bSetRubro = false;

            //Objeto EXCEL
            Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();

            //Validar por Excel
            if (Excel == null)
            {
                MessageBox.Show("Microsoft Excel no se encuentra correctamente instalado!!","Error!",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }         

            try
            {

                //Task Show
                p_lblCurrent.Text = "Procesando... puede demorar, aguarde por favor.";

                //Inicializar
                if (p_Form == 0)
                {
                    myCad = "select COUNT(*) as TOTAL from Vista_Productos_Excel WHERE IncListaPre = 1";
                    sCampo = "IncListaPre";
                }
                else
                {
                    myCad = "select COUNT(*) as TOTAL from Vista_Productos_Excel WHERE IncListaRes = 1";
                    sCampo = "IncListaRes";
                }

                
                myDataAux = clsDataBD.GetSql(myCad);
                //Recorrer Rubros
                foreach (DataRow nFila in myDataAux.Rows)
                {
                    if (!(nFila["TOTAL"] == null))
                    {
                        cCantTot = Convert.ToInt32(nFila["TOTAL"].ToString());
                    }
                }
               
                //Crear libro Excel
                Microsoft.Office.Interop.Excel.Workbook myWorkBook = Excel.Workbooks.Add();

                //Activar Hoja Excel
                Microsoft.Office.Interop.Excel._Worksheet Worksheet = Excel.ActiveSheet;

                //Locked .F.
                Worksheet.Cells.Locked = false;

                //Header ( Mostrador o Revendedor o Distribuidor o Limitrofe? )
                if (p_Form == 0) 
                { MakeHeader(Worksheet); }
                else if (p_Form == 1 && p_TypeLimit == 0 || p_TypeLimit == 4 || p_TypeLimit == 5) 
                { MakeHeaderRev(Worksheet);}
                else if (p_Form == 1 && p_TypeLimit == 3)
                { MakeHeaderDist(Worksheet);}
                else if (p_Form == 1 && p_TypeLimit != 0 && p_TypeLimit != 3 && p_TypeLimit != 4 && p_TypeLimit != 5)
                { MakeHeaderLimitrofe(Worksheet, p_TypeLimit); }

                //Traer Rubros
                myCad = "Select * from RubrosArticulos WHERE Activo=1 ORDER BY Orden ASC";
                myDataRubro = clsDataBD.GetSql(myCad);

                //Cuenta Filas
                int countFila = 3;
                //Cuenta Productos
                int countProducto = 0;
                
                //Recorrer Rubros
                foreach (DataRow row in myDataRubro.Rows)
                {
                    //SubRubros
                    myCad = "select * from SubrubrosArticulos where IdRubroArticulo = " + Convert.ToInt32(row["IdRubroArticulo"].ToString()) + " order by Orden ASC";
                    myDataSubRubro = clsDataBD.GetSql(myCad);
                
                    //Flag
                    bSetRubro = false;

                    //Recorrer SubRubros
                    foreach (DataRow rows in myDataSubRubro.Rows)
                    {
                        //Traer e imprimir productos del SubRubro
                        if (p_Form == 0)
                        {
                            myCad = "exec ObtenerRubroSubRubroExcel " + Convert.ToInt32(row["IdRubroArticulo"].ToString()) + "," + Convert.ToInt32(rows["IdSubrubroArticulo"].ToString()) + "," + Convert.ToInt32(0);
                        }
                        else
                        {
                            myCad = "exec ObtenerRubroSubRubroExcel " + Convert.ToInt32(row["IdRubroArticulo"].ToString()) + "," + Convert.ToInt32(rows["IdSubrubroArticulo"].ToString()) + "," + Convert.ToInt32(1);
                        }
                        myDataProducto = clsDataBD.GetSql(myCad);                       

                        //Hay productos?
                        if (!(myDataProducto.Rows.Count == 0))
                        {
                           //Si .F.
                            if (!(bSetRubro))
                            {
                                //Imprimir Header Rubro
                                MakeHeaderRubro(Worksheet, row["RubroArticulo"].ToString(), countFila, p_Form, p_TypeLimit);
                                //Incrementar
                                countFila++;
                                //Flag
                                bSetRubro = true;
                            }

                            //Header del SubRubro
                            this.MakeHeaderSubRubro(Worksheet, rows["SubrubroArticulo"].ToString(), countFila, p_Form, p_TypeLimit);
                            countFila++;
                            
                            //Traer Productos
                            foreach (DataRow fila in myDataProducto.Rows)
                            {
                               //Si tiene marca de inclusion, imprmir en el excel.
                               if (Convert.ToInt32(fila[sCampo].ToString())==1)
                               {
                                   //Contar Producto
                                   countProducto++;

                                   if (p_Form == 0) //Mostrador
                                   {
                                       MakeItMostrador(Worksheet, countFila, fila);
                                   }
                                   else if (p_Form == 1 && p_TypeLimit==0) // Revendedor
                                   {
                                       MakeItRevendedor(Worksheet, countFila, fila); 
                                   }
                                   else if (p_Form == 1 && p_TypeLimit == 4) // Revendedor + 10
                                   {
                                       MakeItRevendedor(Worksheet, countFila, fila, 4);
                                   }
                                   else if (p_Form == 1 && p_TypeLimit == 5) // Revendedor + 20
                                   {
                                       MakeItRevendedor(Worksheet, countFila, fila, 5);
                                   }
                                   else if (p_Form == 1 && p_TypeLimit == 3) // Distribuidor
                                   {
                                       MakeItDistribuidor(Worksheet, countFila, fila, p_TypeLimit);
                                   }
                                   else if (p_Form == 1 && p_TypeLimit ==1) // Limitrofes Córdoba
                                   {
                                       MakeItLimitrofe(Worksheet, countFila, fila,p_TypeLimit);
                                   }
                                   else if (p_Form == 1 && p_TypeLimit == 2) // Limitrofes de Limitrofes Córdoba
                                   {
                                       MakeItLimitrofe(Worksheet, countFila, fila, p_TypeLimit);
                                   }

                                    //Incrementar
                                    countFila++;

                                   //Actualizar Porcentaje
                                    Porcentaje = (countProducto * 100) / cCantTot;

                                   //Actualizar Porcentaje en pantalla
                                    lblPorc.Text = Porcentaje.ToString("#0") + " %";
                                    p_bar.Value = Convert.ToInt32(Porcentaje);
                               }
                        } 
                           
                      }
                   }
                }

                //Al 100% si quedo con decimales.
                lblPorc.Text = "100 %";
                //Task Show
                p_lblCurrent.Text = "Es Espera...";
                p_bar.Value = 100;

                // Verificar Path
                if (ExcelFilePath != null && ExcelFilePath != "")
                {
                    try
                    {
                        if (p_Form == 0 || p_Form ==1 && p_TypeLimit !=0 && p_TypeLimit !=3) //Mostrador
                        {
                            //Total en 6
                            Worksheet.Cells[1, 6] = "=SUM(F5:" + "F" + countFila + ")";
                        }
                        else if (p_Form == 1 && p_TypeLimit == 0 || p_TypeLimit == 3) //Revendedor // Distribuidor
                        {
                            //Total en 7
                            Worksheet.Cells[1, 7] = "=SUM(G5:" + "G" + countFila + ")";
                        }

                       //Proteger la planilla con la clave
                         Worksheet.Protect("NP*PAV");                        
                    
                       //Others
                         myWorkBook.CheckCompatibility = false;
                         myWorkBook.DoNotPromptForConvert = true;
                         Excel.DisplayAlerts = false; 
     
                        //Guardar
                        if (p_Form == 0)
                        {
                            //Adios Warning
                            Excel.Application.DisplayAlerts = false;
                            //Name
                            nFile = "prama_mostrador";                            
                            
                            //OPCION A, ANDUVO SIEMPRE
                            Worksheet.SaveAs(ExcelFilePath + "\\" + nFile);
                            
                            //Mensaje
                            //MessageBoxTemporal.Show("El Listado de Pedido para 'Mostrador' ha sido generado exitosamente!", "Información!", 4, true);

                        }
                        else if (p_Form == 1 && p_TypeLimit == 0)
                        {
                            //Adios Warning
                            Excel.Application.DisplayAlerts = false;
                            //Nombre Archivo
                            nFile = "prama_revendedor";

                            //OPCION A, ANDUVO SIEMPRE
                            Worksheet.SaveAs(ExcelFilePath + "\\" + nFile);

                            //Mensaje
                            //MessageBoxTemporal.Show("El Listado de Pedido para 'Revendedor' ha sido generado exitosamente!", "Información!", 4, true);

                        }

                        else if (p_Form == 1 && p_TypeLimit == 4)
                        {
                            //Adios Warning
                            Excel.Application.DisplayAlerts = false;
                            //Nombre Archivo
                            nFile = "prama_revendedor_10";

                            //OPCION A, ANDUVO SIEMPRE
                            Worksheet.SaveAs(ExcelFilePath + "\\" + nFile);

                            //Mensaje
                            //MessageBoxTemporal.Show("El Listado de Pedido para 'Revendedor' ha sido generado exitosamente!", "Información!", 4, true);

                        }

                        else if (p_Form == 1 && p_TypeLimit == 5)
                        {
                            //Adios Warning
                            Excel.Application.DisplayAlerts = false;
                            //Nombre Archivo
                            nFile = "prama_revendedor_20";

                            //OPCION A, ANDUVO SIEMPRE
                            Worksheet.SaveAs(ExcelFilePath + "\\" + nFile);

                            //Mensaje
                            //MessageBoxTemporal.Show("El Listado de Pedido para 'Revendedor' ha sido generado exitosamente!", "Información!", 4, true);

                        }

                        else if (p_Form == 1 && p_TypeLimit == 3)
                        {
                            //Adios Warning
                            Excel.Application.DisplayAlerts = false;
                            //Nombre Archivo
                            nFile = "prama_distribuidor";

                            //OPCION A, ANDUVO SIEMPRE
                            Worksheet.SaveAs(ExcelFilePath + "\\" + nFile);

                            //Mensaje
                            // MessageBoxTemporal.Show("El Listado de Pedido para 'Distribuidor' ha sido generado exitosamente!", "Información!", 4, true);

                        }

                        else if (p_Form == 1 && p_TypeLimit == 1)
                        {
                            //Adios Warning
                            Excel.Application.DisplayAlerts = false;
                            //Nombre Archivo
                            nFile = "prama_provincias_limitrofes";

                            //OPCION A, ANDUVO SIEMPRE
                            Worksheet.SaveAs(ExcelFilePath + "\\" + nFile);

                            //Mensaje
                            //MessageBoxTemporal.Show("El Listado de Pedido para 'Provincias Limítrofes de Córdoba' ha sido generado exitosamente!", "Información!", 4, true);
                        }
                        else if (p_Form == 1 && p_TypeLimit == 2)
                        {
                            //Adios Warning
                            Excel.Application.DisplayAlerts = false;
                            //Nombre Archivo
                            nFile = "prama_provincias_limitrofes_de_limitrofes_cba";

                            //OPCION A, ANDUVO SIEMPRE
                            Worksheet.SaveAs(ExcelFilePath + "\\" + nFile);

                            //Mensaje
                            //MessageBoxTemporal.Show("El Listado de Pedido para 'Provincias Limítrofes de Limítrofes de Córdoba' ha sido generado exitosamente!", "Información!", 4, true);
                        }

                        //Actualizar Procentaje
                        p_bar.Value = 0;
                        lblPorc.Text = "0.00 %";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Exportar Lista de Productos: No se pudo guardar el archivo! Verifique el Path!.\n"
                            + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exportando Lista de Productos a Excel: \n" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Excel.Visible = true;
            Excel.Application.Quit();               
            Marshal.FinalReleaseComObject(Excel);
        }

        #endregion

        #region Metodo MakeItMostrador()

        private void MakeItMostrador(Microsoft.Office.Interop.Excel._Worksheet Worksheet, int countFila, DataRow fila)
        {
            double cPrecioCF = 0;
            double cIva = 0;
            Color bgColor;

            //Range
            Microsoft.Office.Interop.Excel.Range formatRange;

            //Verificar Color
            if (Convert.ToInt32(fila["chkSombreado"].ToString()) == 1)
            {
                //Verificar Color
                if (Convert.ToInt32(fila["rbtColor"].ToString()) == 0)
                {
                    // bgColor = Color.FromArgb(255, 153, 102);
                    // bgColor = Color.FromArgb(255, 255, 185);

                    bgColor = Color.FromArgb(169, 233, 169);
                }
                else
                {
                    bgColor = Color.FromArgb(255, 153, 102);
                    // bgColor = Color.FromArgb(169, 233, 169);
                }
            }
            else
            {
                bgColor = System.Drawing.Color.White;
            }

            //Columnas con datos y formateadas
            formatRange = Worksheet.get_Range("a" + countFila, "a" + countFila);
            formatRange.NumberFormat = "#,###,###"; ;
            formatRange.Font.Size = 8;
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignRight;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(System.Drawing.Color.White);


            formatRange = Worksheet.get_Range("b" + countFila, "b" + countFila);
            formatRange.NumberFormat = "#,###,###";
            formatRange.Font.Size = 8;
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignRight;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(bgColor);

            formatRange = Worksheet.get_Range("c" + countFila, "c" + countFila);
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(bgColor);

            formatRange = Worksheet.get_Range("d" + countFila, "d" + countFila);
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(bgColor);

            formatRange = Worksheet.get_Range("e" + countFila, "e" + countFila);
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignRight;
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(bgColor);
            formatRange.NumberFormat = "#,###,###0.00";

            formatRange = Worksheet.get_Range("f" + countFila, "f" + countFila);
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignRight;
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(System.Drawing.Color.White);
            formatRange.NumberFormat = "#,###,###0.00";

            //Escribir los datos
            Worksheet.Cells[countFila, 1] = "";
            Worksheet.Cells[countFila, 2] = fila["Id Articulo"].ToString();
            Worksheet.Cells[countFila, 3] = fila["Descripcion"].ToString();
            Worksheet.Cells[countFila, 4] = Convert.ToDouble(fila["Unidades"]).ToString("#0") + " " + fila["Abrev. Unidad"].ToString();

            //Agregar Coeficiente
            cPrecioCF = Convert.ToDouble(fila["Precio Unitario"]) * Convert.ToDouble(fila["CoeficientePublico"]);

            //Agregar IVA, si corresponde
            if (Convert.ToDouble(fila["PorcentajeIva"]) != 0)
            {
                cIva = 1 + (Convert.ToDouble(fila["PorcentajeIva"]) / 100);
                cPrecioCF = cPrecioCF * cIva;
            }

            //Precio Final con Coeficiente e Iva
            Worksheet.Cells[countFila, 5] = cPrecioCF.ToString("#0.00");

            //Formula para Total
            Worksheet.Cells[countFila, 6] = "=E" + countFila + "*" + "A" + countFila;

            //Locked a Range
            Worksheet.get_Range("A" + countFila, "A" + countFila).Locked = false;
            //Borders
            Worksheet.get_Range("A" + countFila + ":F" + countFila).Cells.Borders.LineStyle = XlLineStyle.xlContinuous;

            //Locked a Range
            Worksheet.get_Range("B" + countFila, "F" + countFila).Locked = true;

            //Borders
            Worksheet.get_Range("B" + countFila + ":F" + countFila).Cells.Borders.LineStyle = XlLineStyle.xlContinuous; 

        }

        #endregion

        #region Metodo MakeItRevendedor

        private void MakeItRevendedor(Microsoft.Office.Interop.Excel._Worksheet Worksheet, int countFila, DataRow fila, int p_TypeLimit = 0)
        {
            double cPrecioCFR = 0;
            double cIva = 0;
            string condA = "";
            string condB = "";
            double porciento = 0;
            System.Drawing.Color bgColor;

            //Range
            Microsoft.Office.Interop.Excel.Range formatRange;

            //Verificar Color
            if (Convert.ToInt32(fila["chkSombreado"].ToString()) == 1)
            {
                //Verificar Color
                if (Convert.ToInt32(fila["rbtColor"].ToString()) == 0)
                {
                    // bgColor = Color.FromArgb(255, 153, 102);
                    // bgColor = Color.FromArgb(255, 255, 185);

                    bgColor = Color.FromArgb(169, 233, 169);
                }
                else
                {
                    bgColor = Color.FromArgb(255, 153, 102);
                    // bgColor = Color.FromArgb(169, 233, 169);
                }
            }
            else
            {
                bgColor = System.Drawing.Color.White;
            }

            //Columnas con datos y formateadas
            formatRange = Worksheet.get_Range("a" + countFila, "a" + countFila);
            formatRange.NumberFormat = "#,###,###"; ;
            formatRange.Font.Size = 8;
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignRight;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(System.Drawing.Color.White);
                        formatRange.HorizontalAlignment = XlHAlign.xlHAlignRight;
            formatRange.Cells.Validation.Delete();
            formatRange.Cells.Validation.Add(
                      XlDVType.xlValidateWholeNumber,
                      XlDVAlertStyle.xlValidAlertStop,
                      XlFormatConditionOperator.xlGreaterEqual,
                      clsGlobales.cParametro.CantMinRev.ToString());
            formatRange.Cells.Validation.ErrorMessage = "La cantidad mínima debe ser " + clsGlobales.cParametro.CantMinRev.ToString();
            formatRange.Cells.Validation.ErrorTitle = "Error!";
            formatRange.Cells.Validation.ShowError = true;

            formatRange = Worksheet.get_Range("b" + countFila, "b" + countFila);
            formatRange.NumberFormat = "#,###,###";
            formatRange.Font.Size = 8;
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignRight;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(bgColor);

            formatRange = Worksheet.get_Range("c" + countFila, "c" + countFila);
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(bgColor);

            formatRange = Worksheet.get_Range("d" + countFila, "d" + countFila);
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(bgColor);

            formatRange = Worksheet.get_Range("e" + countFila, "e" + countFila);
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignRight;
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(bgColor);
            formatRange.NumberFormat = "#,###,###0.00";

            formatRange = Worksheet.get_Range("f" + countFila, "f" + countFila);
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignRight;
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(bgColor);
            formatRange.NumberFormat = "#,###,###0.00";

            formatRange = Worksheet.get_Range("g" + countFila, "g" + countFila);
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignRight;
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(System.Drawing.Color.White);
            formatRange.NumberFormat = "#,###,###0.00";

            //Escribir los datos
            Worksheet.Cells[countFila, 1] = ""; 
            Worksheet.Cells[countFila, 2] = fila["Id Articulo"].ToString();
            Worksheet.Cells[countFila, 3] = fila["Descripcion"].ToString();
            Worksheet.Cells[countFila, 4] = Convert.ToDouble(fila["Unidades"]).ToString("#0") + " " + fila["Abrev. Unidad"].ToString();

            //Precio Final con Coeficiente e Iva (Mos)
            Worksheet.Cells[countFila, 5] = " ";

            //Recalcular para Revendedor
            cPrecioCFR = Convert.ToDouble(fila["Precio Unitario"]) * Convert.ToDouble(fila["CoeficienteRevendedor"]);

            //Agregar IVA, si corresponde
            if (Convert.ToDouble(fila["PorcentajeIva"]) != 0)
            {
                cIva = 1 + (Convert.ToDouble(fila["PorcentajeIva"]) / 100);
            }

            //AGREGAR EL TEMA PORCENTAJE 
            switch (p_TypeLimit)
            {
                case 0:
                    if (!(cIva == 0))
                    { cPrecioCFR = cPrecioCFR * cIva; }
                    break;
                case 4:
                    //Preparo % viene por parametro
                    porciento = 1 + (Convert.ToDouble(10) / 100);
                    //Agrego %
                    cPrecioCFR = cPrecioCFR * porciento;
                    //Calculo Iva si corresponde
                    if (!(cIva == 0))
                    { cPrecioCFR = cPrecioCFR * cIva; }

                    break;
                case 5:
                    //Preparo % viene por parametro
                    porciento = 1 + (Convert.ToDouble(20) / 100);
                    //Agrego % si corresponde
                    cPrecioCFR = cPrecioCFR * porciento;
                    //Agrego Iva si corresponde
                    if (!(cIva == 0))
                    { cPrecioCFR = cPrecioCFR * cIva; }

                    break;
            }

            //Precio Final con Coeficiente e Iva (Rev)
            Worksheet.Cells[countFila, 6] = cPrecioCFR.ToString("#0.00");

            //Formula para Total
            condA = "A" + countFila + "*" + "F" + countFila;
            //condB = "A" + countFila + "*" + "E" + countFila;
            condB = "0";
            //EL MINIMO POR DEFECTO ES >=6... OBVIAMENTE QUE ESTA VEZ NO CABLEE, ESTA PARAMETRIZADO ;) ///
            Worksheet.Cells[countFila, 7].FormulaLocal = "=SI(A"+countFila+">="
                                                +clsGlobales.cParametro.CantMinRev+";"+condA+";"+condB+")";
            
            //Locked a Range
            Worksheet.get_Range("A" + countFila, "A" + countFila).Locked = false;
            //Borders
            Worksheet.get_Range("A" + countFila + ":G" + countFila).Cells.Borders.LineStyle = XlLineStyle.xlContinuous;

            //Locked a Range
            Worksheet.get_Range("B" + countFila, "G" + countFila).Locked = true;

            //Borders
            Worksheet.get_Range("B" + countFila + ":G" + countFila).Cells.Borders.LineStyle = XlLineStyle.xlContinuous;

        }

        #endregion  

        #region Metodo MakeItDistribuidor

        private void MakeItDistribuidor(Microsoft.Office.Interop.Excel._Worksheet Worksheet, int countFila, DataRow fila, int p_TypeLimit = 0)
        {

            double cPrecioCFR = 0;
            double cIva = 0;
            string condA = "";
            string condB = "";
            System.Drawing.Color bgColor;

            //Range
            Microsoft.Office.Interop.Excel.Range formatRange;

            //Verificar Color
            if (Convert.ToInt32(fila["chkSombreado"].ToString()) == 1)
            {
                //Verificar Color
                if (Convert.ToInt32(fila["rbtColor"].ToString()) == 0)
                {
                    // bgColor = Color.FromArgb(255, 153, 102);
                    // bgColor = Color.FromArgb(255, 255, 185);

                    bgColor = Color.FromArgb(169, 233, 169);
                }
                else
                {
                    bgColor = Color.FromArgb(255, 153, 102);
                    // bgColor = Color.FromArgb(169, 233, 169);
                }
            }
            else
            {
                bgColor = System.Drawing.Color.White;
            }

            //Columnas con datos y formateadas
            formatRange = Worksheet.get_Range("a" + countFila, "a" + countFila);
            formatRange.NumberFormat = "#,###,###"; ;
            formatRange.Font.Size = 8;
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignRight;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(System.Drawing.Color.White);
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignRight;
            formatRange.Cells.Validation.Delete();
            formatRange.Cells.Validation.Add(
                      XlDVType.xlValidateWholeNumber,
                      XlDVAlertStyle.xlValidAlertStop,
                      XlFormatConditionOperator.xlGreaterEqual,
                      clsGlobales.cParametro.CantMinRev.ToString());
            formatRange.Cells.Validation.ErrorMessage = "La cantidad mínima debe ser " + clsGlobales.cParametro.CantMinRev.ToString();
            formatRange.Cells.Validation.ErrorTitle = "Error!";
            formatRange.Cells.Validation.ShowError = true;

            formatRange = Worksheet.get_Range("b" + countFila, "b" + countFila);
            formatRange.NumberFormat = "#,###,###";
            formatRange.Font.Size = 8;
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignRight;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(bgColor);

            formatRange = Worksheet.get_Range("c" + countFila, "c" + countFila);
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(bgColor);

            formatRange = Worksheet.get_Range("d" + countFila, "d" + countFila);
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(bgColor);

            formatRange = Worksheet.get_Range("e" + countFila, "e" + countFila);
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignRight;
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(bgColor);
            formatRange.NumberFormat = "#,###,###0.00";

            formatRange = Worksheet.get_Range("f" + countFila, "f" + countFila);
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignRight;
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(bgColor);
            formatRange.NumberFormat = "#,###,###0.00";

            formatRange = Worksheet.get_Range("g" + countFila, "g" + countFila);
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignRight;
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(System.Drawing.Color.White);
            formatRange.NumberFormat = "#,###,###0.00";

            //Escribir los datos
            Worksheet.Cells[countFila, 1] = "";
            Worksheet.Cells[countFila, 2] = fila["Id Articulo"].ToString();
            Worksheet.Cells[countFila, 3] = fila["Descripcion"].ToString();
            Worksheet.Cells[countFila, 4] = Convert.ToDouble(fila["Unidades"]).ToString("#0") + " " + fila["Abrev. Unidad"].ToString();

            //Precio Final con Coeficiente e Iva (Mos)
            Worksheet.Cells[countFila, 5] = " ";

            //Recalcular para Revendedor
            cPrecioCFR = Convert.ToDouble(fila["Precio Unitario"]) * Convert.ToDouble(fila["CoeficienteDistribuidor"]);

            //Agregar IVA, si corresponde
            if (Convert.ToDouble(fila["PorcentajeIva"]) != 0)
            {
                cIva = 1 + (Convert.ToDouble(fila["PorcentajeIva"]) / 100);
                cPrecioCFR = cPrecioCFR * cIva;
            }

            //Precio Final con Coeficiente e Iva (Rev)
            Worksheet.Cells[countFila, 6] = cPrecioCFR.ToString("#0.00");

            //Formula para Total
            condA = "A" + countFila + "*" + "F" + countFila;
            //condB = "A" + countFila + "*" + "E" + countFila;
            condB = "0";
            //EL MINIMO POR DEFECTO ES >=6... OBVIAMENTE QUE ESTA VEZ NO CABLEE, ESTA PARAMETRIZADO ;) ///
            Worksheet.Cells[countFila, 7].FormulaLocal = "=SI(A" + countFila + ">="
                                                + clsGlobales.cParametro.CantMinRev + ";" + condA + ";" + condB + ")";

            //Locked a Range
            Worksheet.get_Range("A" + countFila, "A" + countFila).Locked = false;
            //Borders
            Worksheet.get_Range("A" + countFila + ":G" + countFila).Cells.Borders.LineStyle = XlLineStyle.xlContinuous;

            //Locked a Range
            Worksheet.get_Range("B" + countFila, "G" + countFila).Locked = true;

            //Borders
            Worksheet.get_Range("B" + countFila + ":G" + countFila).Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
        }

        #endregion  

        #region Metodo MakeItLimitrofe

        private void MakeItLimitrofe(Microsoft.Office.Interop.Excel._Worksheet Worksheet, int countFila, DataRow fila, int p_TypeLimit = 0)
        {
            double cPrecioCF = 0;
            double cIva = 0;
            Color bgColor;
            double porciento = 0;

            //Range
            Microsoft.Office.Interop.Excel.Range formatRange;

            //Verificar Color
            if (Convert.ToInt32(fila["chkSombreado"].ToString()) == 1)
            {
                //Verificar Color
                if (Convert.ToInt32(fila["rbtColor"].ToString()) == 0)
                {
                 // bgColor = Color.FromArgb(255, 153, 102);
                 // bgColor = Color.FromArgb(255, 255, 185);

                    bgColor = Color.FromArgb(169, 233, 169);
                }
                else
                {
                    bgColor = Color.FromArgb(255, 153, 102);
                 // bgColor = Color.FromArgb(169, 233, 169);
                }
            }
            else
            {
                bgColor = System.Drawing.Color.White;
            }

            //Columnas con datos y formateadas
            formatRange = Worksheet.get_Range("a" + countFila, "a" + countFila);
            formatRange.NumberFormat = "#,###,###"; ;
            formatRange.Font.Size = 8;
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignRight;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(System.Drawing.Color.White);


            formatRange = Worksheet.get_Range("b" + countFila, "b" + countFila);
            formatRange.NumberFormat = "#,###,###";
            formatRange.Font.Size = 8;
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignRight;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(bgColor);

            formatRange = Worksheet.get_Range("c" + countFila, "c" + countFila);
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(bgColor);

            formatRange = Worksheet.get_Range("d" + countFila, "d" + countFila);
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(bgColor);

            formatRange = Worksheet.get_Range("e" + countFila, "e" + countFila);
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignRight;
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(bgColor);
            formatRange.NumberFormat = "#,###,###0.00";

            formatRange = Worksheet.get_Range("f" + countFila, "f" + countFila);
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignRight;
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(System.Drawing.Color.White);
            formatRange.NumberFormat = "#,###,###0.00";

            //Escribir los datos
            Worksheet.Cells[countFila, 1] = "";
            Worksheet.Cells[countFila, 2] = fila["Id Articulo"].ToString();
            Worksheet.Cells[countFila, 3] = fila["Descripcion"].ToString();
            Worksheet.Cells[countFila, 4] = Convert.ToDouble(fila["Unidades"]).ToString("#0") + " " + fila["Abrev. Unidad"].ToString();

            //Agregar Coeficiente
            cPrecioCF = Convert.ToDouble(fila["Precio Unitario"]) * Convert.ToDouble(fila["CoeficientePublico"]);

            //Agregar IVA, si corresponde
            if (Convert.ToDouble(fila["PorcentajeIva"]) != 0)
            {
                cIva = 1 + (Convert.ToDouble(fila["PorcentajeIva"]) / 100);
            }

            //AGREGAR EL TEMA PORCENTAJE 
            switch (p_TypeLimit)
            {
                case 1:
                    //Preparo % viene por parametro
                    porciento = 1 + (clsGlobales.cParametro.PorcLimitCdba / 100);
                    //Agrego %
                    cPrecioCF = cPrecioCF * porciento;
                    //Calculo Iva si corresponde
                    if (!(cIva == 0))
                    { cPrecioCF = cPrecioCF * cIva; }

                    break;
                case 2:
                    //Preparo % viene por parametro
                    porciento = 1 + (clsGlobales.cParametro.PorcLimitCbaLimit / 100);
                    //Agrego % si corresponde
                    cPrecioCF = cPrecioCF * porciento;
                    //Agrego Iva si corresponde
                    if (!(cIva == 0))
                    { cPrecioCF = cPrecioCF * cIva; }

                    break;
            }

            //Precio Final con Coeficiente e Iva
            Worksheet.Cells[countFila, 5] = cPrecioCF.ToString("#0.00");

            //Formula para Total
            Worksheet.Cells[countFila, 6] = "=E" + countFila + "*" + "A" + countFila;

            //Locked a Range
            Worksheet.get_Range("A" + countFila, "A" + countFila).Locked = false;
            //Borders
            Worksheet.get_Range("A" + countFila + ":F" + countFila).Cells.Borders.LineStyle = XlLineStyle.xlContinuous;

            //Locked a Range
            Worksheet.get_Range("B" + countFila, "F" + countFila).Locked = true;

            //Borders
            Worksheet.get_Range("B" + countFila + ":F" + countFila).Cells.Borders.LineStyle = XlLineStyle.xlContinuous; 

        }

        #endregion  

        #region Metodo: MakeHeaderRubro()

        //Genera el Header para cada Rubro segun corresponda
        private void MakeHeaderRubro(Microsoft.Office.Interop.Excel._Worksheet Worksheet, string p_strRubro, int p_Fila, int p_Form, int p_TypeLimit = 0)
        {
            //Range
            Microsoft.Office.Interop.Excel.Range formatRange;
            
            //Mos o Rev o Prov Limit?
            string sColumna = "";            
            if (p_Form == 0) { sColumna = "F"; }
            if (p_Form == 1 && p_TypeLimit != 0 && p_TypeLimit >= 1 && p_TypeLimit <= 2) { sColumna = "F"; }
            if (p_Form == 1 && p_TypeLimit == 0 || p_TypeLimit == 3 || p_TypeLimit == 4 || p_TypeLimit == 5) { sColumna = "G"; } //Cambie el 21/06/2018

            //Formato 
            formatRange = Worksheet.get_Range("A" + p_Fila, sColumna + p_Fila);
            formatRange.EntireRow.Font.Bold = true;
            formatRange.Font.Size = 10;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(System.Drawing.Color.DimGray);
            //Escribir Titulo Rubro
            Worksheet.Cells[p_Fila, 1] = p_strRubro;

            //Locked a Range
            Worksheet.get_Range("A" + p_Fila, sColumna + p_Fila).Locked = true;
        }

        #endregion

        #region Metodo: MakeHeaderSubRubro()

        //Genera el Header para cada Rubro segun corresponda
        private void MakeHeaderSubRubro(Microsoft.Office.Interop.Excel._Worksheet Worksheet, string p_strSubRubro, int p_Fila, int p_Form, int p_TypeLimit = 0)
        {
            //Range
            Microsoft.Office.Interop.Excel.Range formatRange;

            string sColumna = "";
            //if (p_Form == 0) { sColumna = "F"; }
            //if (p_Form == 1 && p_TypeLimit != 0 && p_TypeLimit != 3) { sColumna = "F"; }
            //if (p_Form == 1 && p_TypeLimit == 0 || p_TypeLimit == 3) { sColumna = "G"; } //Cambie el 12/02/2018
            if (p_Form == 0) { sColumna = "F"; }
            if (p_Form == 1 && p_TypeLimit != 0 && p_TypeLimit >= 1 && p_TypeLimit <= 2) { sColumna = "F"; }
            if (p_Form == 1 && p_TypeLimit == 0 || p_TypeLimit == 3 || p_TypeLimit == 4 || p_TypeLimit == 5) { sColumna = "G"; } //Cambie el 21/06/2018

            
            //Formato 
            formatRange = Worksheet.get_Range("a" + p_Fila, sColumna + p_Fila);
            formatRange.EntireRow.Font.Bold = true;
            formatRange.Font.Size = 10;            
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(System.Drawing.Color.DarkGray);
            //Escribir Titulo Rubro
            Worksheet.Cells[p_Fila, 1] = p_strSubRubro;

            //Locked a Range
            Worksheet.get_Range("A" + p_Fila, sColumna + p_Fila).Locked = true;
        }

        #endregion

        #region Metodo: MakeHeader()

        //Genera el Header del Excel
        private void MakeHeader(Microsoft.Office.Interop.Excel._Worksheet Worksheet)        
        {
            //Range
            Microsoft.Office.Interop.Excel.Range formatRange;

            //Ancho de columnas
            formatRange = Worksheet.get_Range("a1", "a1");
            formatRange.EntireColumn.ColumnWidth = 8;
            //Ancho de columnas
            formatRange = Worksheet.get_Range("b1", "b1");
            formatRange.EntireColumn.ColumnWidth = 8;
            //Ancho de columnas
            formatRange = Worksheet.get_Range("c1", "c1");
            formatRange.EntireColumn.ColumnWidth = 66;
            //Ancho de columnas
            formatRange = Worksheet.get_Range("d1", "d1");
            formatRange.EntireColumn.ColumnWidth = 12;
            //Ancho de columnas
            formatRange = Worksheet.get_Range("e1", "e1");
            formatRange.EntireColumn.ColumnWidth = 10;
            //Ancho de columnas
            formatRange = Worksheet.get_Range("f1", "f1");
            formatRange.EntireColumn.ColumnWidth = 10;

            //Titulo
            formatRange = Worksheet.get_Range("a1", "f1");
            formatRange.EntireRow.Font.Bold = true;
            formatRange.Font.Name = "Tahoma";
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(System.Drawing.Color.Green);

            //Header                
            Worksheet.Cells[1, 1] = "Productos Saludables PRAMA - Precios Mostrador (IVA INCLUIDO - NO INCLUYE COSTO DE ENVÍO)";
            Worksheet.Cells[1, 5] = "TOTAL $";

            //Fecha
            formatRange = Worksheet.get_Range("d1", "d1");
            formatRange.Font.Bold = true;
            formatRange.Font.Name = "Tahoma";
            formatRange.Font.Size = 8;
            formatRange.NumberFormat = "dd-mm-yyyy";
            Worksheet.Cells[1, 4] = DateTime.Now.ToShortDateString();

            //Segunda Linea
            formatRange = Worksheet.get_Range("a2", "f2");
            formatRange.EntireRow.Font.Bold = true;
            formatRange.Font.Name = "Tahoma";
            formatRange.Font.Size = 8;
            formatRange.HorizontalAlignment = 3;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(System.Drawing.Color.White);
            formatRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
            Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
            Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);
            Worksheet.Cells[2, 1] = "Cantidad";
            Worksheet.Cells[2, 2] = "Artículo";
            Worksheet.Cells[2, 3] = "Descripción";
            Worksheet.Cells[2, 4] = "Fracc";
            Worksheet.Cells[2, 5] = "Unitario";
            Worksheet.Cells[2, 6] = "Total";

            //Locked a Range
            Worksheet.get_Range("A1", "F2").Locked = true;

        }

        #endregion

        #region Metodo: MakeHeaderRev()

        //Genera el Header del Excel
        private void MakeHeaderRev(Microsoft.Office.Interop.Excel._Worksheet Worksheet)
        {
            //Range
            Microsoft.Office.Interop.Excel.Range formatRange;

            //Ancho de columnas
            formatRange = Worksheet.get_Range("a1", "a1");
            formatRange.EntireColumn.ColumnWidth = 8;
            //Ancho de columnas
            formatRange = Worksheet.get_Range("b1", "b1");
            formatRange.EntireColumn.ColumnWidth = 8;
            //Ancho de columnas
            formatRange = Worksheet.get_Range("c1", "c1");
            formatRange.EntireColumn.ColumnWidth = 66;

            //Ancho de columnas
            formatRange = Worksheet.get_Range("d1", "d1");
            formatRange.EntireColumn.ColumnWidth = 12;
            //Ancho de columnas
            formatRange = Worksheet.get_Range("e1", "e1");
            formatRange.EntireColumn.ColumnWidth = 5;
            formatRange.EntireColumn.Hidden = true;

            //Ancho de columnas
            formatRange = Worksheet.get_Range("f1", "f1");
            formatRange.EntireColumn.ColumnWidth = 10;
            //Ancho de columnas
            formatRange = Worksheet.get_Range("g1", "g1");
            formatRange.EntireColumn.ColumnWidth = 10;

            //Titulo
            formatRange = Worksheet.get_Range("a1", "g1");
            formatRange.EntireRow.Font.Bold = true;
            formatRange.Font.Name = "Tahoma";
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(System.Drawing.Color.Green);

            //Header                
            Worksheet.Cells[1, 1] = "Productos Saludables PRAMA - Precios Reventa (IVA INCLUIDO - NO INCLUYE COSTO DE ENVÍO)";
            Worksheet.Cells[1, 6] = "TOTAL $";

            //Fecha
            formatRange = Worksheet.get_Range("d1", "d1");
            formatRange.Font.Bold = true;
            formatRange.Font.Name = "Tahoma";
            formatRange.Font.Size = 8;
            formatRange.NumberFormat = "dd-mm-yyyy";
            Worksheet.Cells[1, 4] = DateTime.Now.ToShortDateString();

            //Segunda Linea
            formatRange = Worksheet.get_Range("a2", "g2");
            formatRange.EntireRow.Font.Bold = true;
            formatRange.Font.Name = "Tahoma";
            formatRange.Font.Size = 8;
            formatRange.HorizontalAlignment = 3;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(System.Drawing.Color.White);
            formatRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
            Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
            Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);
            Worksheet.Cells[2, 1] = "Cantidad";
            Worksheet.Cells[2, 2] = "Artículo";
            Worksheet.Cells[2, 3] = "Descripción";
            Worksheet.Cells[2, 4] = "Fracc";
            Worksheet.Cells[2, 5] = "     ";
            Worksheet.Cells[2, 6] = "Rev";
            Worksheet.Cells[2, 7] = "Total";

            //Locked a Range
            Worksheet.get_Range("A1", "G2").Locked = true;

        }

        #endregion

        #region Metodo: MakeHeaderDist()

        //Genera el Header del Excel
        private void MakeHeaderDist(Microsoft.Office.Interop.Excel._Worksheet Worksheet)
        {
            //Range
            Microsoft.Office.Interop.Excel.Range formatRange;

            //Ancho de columnas
            formatRange = Worksheet.get_Range("a1", "a1");
            formatRange.EntireColumn.ColumnWidth = 8;
            //Ancho de columnas
            formatRange = Worksheet.get_Range("b1", "b1");
            formatRange.EntireColumn.ColumnWidth = 8;
            //Ancho de columnas
            formatRange = Worksheet.get_Range("c1", "c1");
            formatRange.EntireColumn.ColumnWidth = 66;

            //Ancho de columnas
            formatRange = Worksheet.get_Range("d1", "d1");
            formatRange.EntireColumn.ColumnWidth = 12;
            //Ancho de columnas
            formatRange = Worksheet.get_Range("e1", "e1");
            formatRange.EntireColumn.ColumnWidth = 5;
            formatRange.EntireColumn.Hidden = true;

            //Ancho de columnas
            formatRange = Worksheet.get_Range("f1", "f1");
            formatRange.EntireColumn.ColumnWidth = 10;
            //Ancho de columnas
            formatRange = Worksheet.get_Range("g1", "g1");
            formatRange.EntireColumn.ColumnWidth = 10;

            //Titulo
            formatRange = Worksheet.get_Range("a1", "g1");
            formatRange.EntireRow.Font.Bold = true;
            formatRange.Font.Name = "Tahoma";
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(System.Drawing.Color.Green);

            //Header                
            Worksheet.Cells[1, 1] = "Productos Saludables PRAMA - Precios Distribuidor (IVA INCLUIDO - NO INCLUYE COSTO DE ENVÍO)";
            Worksheet.Cells[1, 6] = "TOTAL $";

            //Fecha
            formatRange = Worksheet.get_Range("d1", "d1");
            formatRange.Font.Bold = true;
            formatRange.Font.Name = "Tahoma";
            formatRange.Font.Size = 8;
            formatRange.NumberFormat = "dd-mm-yyyy";
            Worksheet.Cells[1, 4] = DateTime.Now.ToShortDateString();

            //Segunda Linea
            formatRange = Worksheet.get_Range("a2", "g2");
            formatRange.EntireRow.Font.Bold = true;
            formatRange.Font.Name = "Tahoma";
            formatRange.Font.Size = 8;
            formatRange.HorizontalAlignment = 3;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(System.Drawing.Color.White);
            formatRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
            Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
            Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);
            Worksheet.Cells[2, 1] = "Cantidad";
            Worksheet.Cells[2, 2] = "Artículo";
            Worksheet.Cells[2, 3] = "Descripción";
            Worksheet.Cells[2, 4] = "Fracc";
            Worksheet.Cells[2, 5] = "     ";
            Worksheet.Cells[2, 6] = "Dist";
            Worksheet.Cells[2, 7] = "Total";

            //Locked a Range
            Worksheet.get_Range("A1", "G2").Locked = true;

        }

        #endregion

        #region MakeHeaderLimitrofe()

        //Genera el Header del Excel
        private void MakeHeaderLimitrofe(Microsoft.Office.Interop.Excel._Worksheet Worksheet, int p_TypeLimit = 0)        
        {
            //Range
            Microsoft.Office.Interop.Excel.Range formatRange;

            //Ancho de columnas
            formatRange = Worksheet.get_Range("a1", "a1");
            formatRange.EntireColumn.ColumnWidth = 8;
            //Ancho de columnas
            formatRange = Worksheet.get_Range("b1", "b1");
            formatRange.EntireColumn.ColumnWidth = 8;
            //Ancho de columnas
            formatRange = Worksheet.get_Range("c1", "c1");
            formatRange.EntireColumn.ColumnWidth = 70;
            //Ancho de columnas
            formatRange = Worksheet.get_Range("d1", "d1");
            formatRange.EntireColumn.ColumnWidth = 12;
            //Ancho de columnas
            formatRange = Worksheet.get_Range("e1", "e1");
            formatRange.EntireColumn.ColumnWidth = 10;
            //Ancho de columnas
            formatRange = Worksheet.get_Range("f1", "f1");
            formatRange.EntireColumn.ColumnWidth = 10;

            //Titulo
            formatRange = Worksheet.get_Range("a1", "f1");
            formatRange.EntireRow.Font.Bold = true;
            formatRange.Font.Name = "Tahoma";
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(System.Drawing.Color.Green);

            //Header                
            if (p_TypeLimit == 1)
            { Worksheet.Cells[1, 1] = "Productos Saludables PRAMA - Precios Sugeridos Público (Provincias Limítrofes de Córdoba)"; }
            else
            { Worksheet.Cells[1, 1] = "Productos Saludables PRAMA - Precios Sugeridos Público (Provincias Limítrofes de Limítrofes de Córdoba)"; }
            
            Worksheet.Cells[1, 5] = "TOTAL $";

            //Fecha
            formatRange = Worksheet.get_Range("d1", "d1");
            formatRange.Font.Bold = true;
            formatRange.Font.Name = "Tahoma";
            formatRange.Font.Size = 8;
            formatRange.NumberFormat = "dd-mm-yyyy";
            Worksheet.Cells[1, 4] = DateTime.Now.ToShortDateString();

            //Segunda Linea
            formatRange = Worksheet.get_Range("a2", "f2");
            formatRange.EntireRow.Font.Bold = true;
            formatRange.Font.Name = "Tahoma";
            formatRange.Font.Size = 8;
            formatRange.HorizontalAlignment = 3;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(System.Drawing.Color.White);
            formatRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
            Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
            Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);
            Worksheet.Cells[2, 1] = "Cantidad";
            Worksheet.Cells[2, 2] = "Artículo";
            Worksheet.Cells[2, 3] = "Descripción";
            Worksheet.Cells[2, 4] = "Fracc";
            Worksheet.Cells[2, 5] = "Unitario";
            Worksheet.Cells[2, 6] = "Total";

            //Locked a Range
            Worksheet.get_Range("A1", "F2").Locked = true;

        }

        #endregion

        #region Metodo: ExportarAExcel

        public void ExportarAExcel(DataGridView dgView, ProgressBar pBar)
        {

            try
            {
                if (pBar != null)
                {
                    pBar.Maximum = dgView.RowCount;
                    pBar.Value = 0;
                    if (!pBar.Visible) pBar.Visible = true;
                }
                string sFont = "Verdana";
                int iSize = 8;
                //CREACIÓN DE LOS OBJETOS DE EXCEL
                Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Worksheet xlsSheet;
                Microsoft.Office.Interop.Excel.Workbook xlsBook;
                //AGREGAMOS EL LIBRO Y HOJA DE EXCEL
                xlsBook = xlsApp.Workbooks.Add(true);
                xlsSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsBook.ActiveSheet;
                //ESPECIFICAMOS EL TIPO DE LETRA Y TAMAÑO DE LA LETRA DEL LIBRO
                xlsSheet.Rows.Cells.Font.Size = iSize;
                xlsSheet.Rows.Cells.Font.Name = sFont;
                //AGREGAMOS LOS ENCABEZADOS
                int iFil = 0, iCol = 0;

                foreach (DataGridViewColumn column in dgView.Columns)
                    if (column.Visible)
                        xlsSheet.Cells[1, ++iCol] = column.HeaderText;
                //MARCAMOS LAS CELDAS DEL ENCABEZADO EN NEGRITA Y EN COLOR DE RELLENO GRIS
                xlsSheet.get_Range(xlsSheet.Cells[1, 1], xlsSheet.Cells[1, dgView.ColumnCount]).Font.Bold = true;
                xlsSheet.get_Range(xlsSheet.Cells[1, 1], xlsSheet.Cells[1, dgView.ColumnCount]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver);
                //RECORRIDO DE LAS FILAS Y COLUMNAS (PINTADO DE CELDAS) 
                Microsoft.Office.Interop.Excel.Range r;
                Color c;
                for (iFil = 0; iFil < dgView.RowCount; iFil++)
                {
                    for (iCol = 0; iCol < dgView.ColumnCount; iCol++)
                    {
                        xlsSheet.Cells[iFil + 2, iCol + 1] = dgView.Rows[iFil].Cells[iCol].Value.ToString();
                        c = dgView.Rows[iFil].Cells[iCol].Style.BackColor;
                        if (!c.IsEmpty)
                        {// COMPARAMOS SI ESTÁ PINTADA LA CELDA (SI ES VERDADERO PINTAMOS LA CELDA)
                           r = (Microsoft.Office.Interop.Excel.Range)xlsSheet.Cells[iFil + 2, iCol + 1];
                           xlsSheet.get_Range(r, r).Interior.Color = System.Drawing.ColorTranslator.ToOle(dgView.Rows[iFil].Cells[iCol].Style.BackColor);
                        }
                    }
                    pBar.Value += 1;
                }
                xlsSheet.Columns.AutoFit();
                xlsSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;
                xlsSheet.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperLetter;
                xlsSheet.PageSetup.Zoom = 80;

                Microsoft.Office.Interop.Excel.Range rango = xlsSheet.get_Range(xlsSheet.Cells[1, 1], xlsSheet.Cells[dgView.RowCount + 1, dgView.ColumnCount]);
                rango.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                rango.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);
                //rango.Cells.AutoFormat(Excel.XlRangeAutoFormat.xlRangeAutoFormatList1, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                xlsApp.Visible = true;
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                if (pBar != null)
                {
                    pBar.Value = 0;
                    pBar.Visible = false;
                }
            }
        }

        #endregion

        #region Metodo: AddDataValidation

        /// <summary>
        /// Adds a small Infobox and a Validation with restriction (only these values will be selectable) to the specified cell.
        /// </summary>
        /// <param name="worksheet">The excel-sheet</param>
        /// <param name="rowNr">1-based row index of the cell that will contain the validation</param>
        /// <param name="columnNr">1-based column index of the cell that will contain the validation</param>
        /// <param name="title">Title of the Infobox</param>
        /// <param name="message">Message in the Infobox</param>
        /// <param name="validationValues">List of available values for selection of the cell. No other value, than this list is allowed to be used.</param>
        /// <exception cref="Exception">Thrown, if an error occurs, or the worksheet was null.</exception>
        public static void AddDataValidation(Worksheet worksheet, int rowNr, int columnNr, string title, string message, List<string> validationValues)
        {
            //If the message-string is too long (more than 255 characters, prune it)
            if (message.Length > 255)
                message = message.Substring(0, 254);

            try
            {
                //The validation requires a ';'-separated list of values, that goes as the restrictions-parameter.
                //Fold the list, so you can add it as restriction. (Result is "Value1;Value2;Value3")
                //If you use another separation-character (e.g in US) change the ; appropriately (e.g. to the ,)
                string values = string.Join(";", validationValues);
                //Select the specified cell
                Range cell = worksheet.Cells[rowNr, columnNr];
                //Delete any previous validation
                cell.Validation.Delete();
                //Add the validation, that only allowes selection of provided values.
                cell.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertStop, XlFormatConditionOperator.xlBetween, values, Type.Missing);
                cell.Validation.IgnoreBlank = true;
                //Optional put a message there
                cell.Validation.InputTitle = title;
                cell.Validation.InputMessage = message;

            }
            catch (Exception exception)
            {
                //This part should not be reached, but is used for stability-reasons
                throw new Exception(String.Format("Error when adding a Validation with restriction to the specified cell Row:{0}, Column:{1}, Message: {2}", rowNr, columnNr, message), exception);

            }
        }

        #endregion

        #region Metodo: SetIconForm

        public static void SetIconForm(Form myForm)
        {
            System.Drawing.Icon icono = new System.Drawing.Icon(System.Windows.Forms.Application.StartupPath + "\\Prama.ico");
            myForm.Icon=icono;
        }

        #endregion

        #region Metodo ExportToExcelCli

        /// <summary>
        /// Export DataTable to Excel file
        /// </summary>
        /// <param name="DataTable">Source DataTable</param>
        /// <param name="ExcelFilePath">Path to result file name</param>
        public void ExportToExcelCli(string ExcelFilePath = null, DataGridView p_myGridCli = null, ProgressBar p_Bar = null, System.Windows.Forms.Label p_lblPorc = null, System.Windows.Forms.Label p_Actual = null)
        {

            //Objeto EXCEL
            Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();

            //Validar por Excel
            if (Excel == null)
            {
                MessageBox.Show("Microsoft Excel no se encuentra correctamente instalado!!","Error!",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }         

            try
            {
               
                //Crear libro Excel
                Microsoft.Office.Interop.Excel.Workbook myWorkBook = Excel.Workbooks.Add();

                //Activar Hoja Excel
                Microsoft.Office.Interop.Excel._Worksheet Worksheet = Excel.ActiveSheet;

                //Locked .F.
                Worksheet.Cells.Locked = false;

                //Header 
                MakeHeaderCli(Worksheet); 

                //Cuenta Filas
                int countFila = 3;

                int TotalFilas = p_myGridCli.Rows.Count;

                int Porciento = 0;

                //Recorrer Rubros
                foreach (DataGridViewRow row in p_myGridCli.Rows)
                {

                    //Guardar Cliente
                    MakeItCliente(Worksheet, countFila, row);
 
                    //Porcentaje
                    Porciento = (countFila * 100) / TotalFilas;
                    p_Bar.Value = Porciento;
                    p_lblPorc.Text = " % " + Porciento.ToString("#0");
                    p_Actual.Text = countFila.ToString();

                    //Si cancela...
                    if (clsGlobales.bInterrupt)
                    {
                        MessageBox.Show("Proceso de Exportación ha sido cancelado por el Usuario. No se generó el archivo de datos.", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    System.Windows.Forms.Application.DoEvents();

                    //Incrementar
                    countFila++;

                }
            

                // Verificar Path
                if (ExcelFilePath != null && ExcelFilePath != "")
                {
                    try
                    {
                       //Proteger la planilla con la clave
                         Worksheet.Protect("FP*PSAS");                        
                    
                       //Others
                         myWorkBook.CheckCompatibility = false;
                         myWorkBook.DoNotPromptForConvert = true;
                         Excel.DisplayAlerts = false; 
      
                        //Adios Warning
                         Excel.Application.DisplayAlerts = false;

                        //Name
                         string nFile = "PramaSAS_Clientes";                            
                            
                        //OPCION A, ANDUVO SIEMPRE
                         Worksheet.SaveAs(ExcelFilePath + "\\" + nFile);

                    }       
                    catch (Exception ex)
                    {
                        MessageBox.Show("Exportar Base de Clientes: No se pudo guardar el archivo! Verifique el Path!.\n"
                                            + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exportando Base de Clientes a Excel: \n" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Excel.Visible = true;
            Excel.Application.Quit();               
            Marshal.FinalReleaseComObject(Excel);
        }

        #endregion

        #region Metodo: MakeHeaderCli()

        //Genera el Header del Excel
        private void MakeHeaderCli(Microsoft.Office.Interop.Excel._Worksheet Worksheet)
        {
            //Range
            Microsoft.Office.Interop.Excel.Range formatRange;

            //Ancho de columnas
            formatRange = Worksheet.get_Range("a1", "a1");
            formatRange.EntireColumn.ColumnWidth = 8;
            //Ancho de columnas
            formatRange = Worksheet.get_Range("b1", "b1");
            formatRange.EntireColumn.ColumnWidth = 66;
            //Ancho de columnas
            formatRange = Worksheet.get_Range("c1", "c1");
            formatRange.EntireColumn.ColumnWidth = 20;
            //Ancho de columnas
            formatRange = Worksheet.get_Range("d1", "d1");
            formatRange.EntireColumn.ColumnWidth = 35;
            //Ancho de columnas
            formatRange = Worksheet.get_Range("e1", "e1");
            formatRange.EntireColumn.ColumnWidth = 35;
            //Ancho de columnas
            formatRange = Worksheet.get_Range("f1", "f1");
            formatRange.EntireColumn.ColumnWidth = 20;
            //Ancho de columnas
            formatRange = Worksheet.get_Range("g1", "g1");
            formatRange.EntireColumn.ColumnWidth = 20;
            //Ancho de columnas
            formatRange = Worksheet.get_Range("h1", "h1");
            formatRange.EntireColumn.ColumnWidth = 20;
            //Ancho de columnas
            formatRange = Worksheet.get_Range("i1", "i1");
            formatRange.EntireColumn.ColumnWidth = 25;
            //Ancho de columnas
            formatRange = Worksheet.get_Range("j1", "j1");
            formatRange.EntireColumn.ColumnWidth = 15;

            //Fecha
            formatRange = Worksheet.get_Range("c1", "c1");
            formatRange.Font.Bold = true;
            formatRange.Font.Name = "Tahoma";
            formatRange.Font.Size = 8;
            formatRange.NumberFormat = "dd-mm-yyyy";
            Worksheet.Cells[1, 3] = DateTime.Now.ToShortDateString();

            //Titulo
            formatRange = Worksheet.get_Range("a1", "j1");
            formatRange.EntireRow.Font.Bold = true;
            formatRange.Font.Name = "Tahoma";
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(System.Drawing.Color.Green);

            //Header                
            Worksheet.Cells[1, 1] = "PRAMA SAS - BASE DE CLIENTES";

            //Segunda Linea
            formatRange = Worksheet.get_Range("a2", "j2");
            formatRange.EntireRow.Font.Bold = true;
            formatRange.Font.Name = "Tahoma";
            formatRange.Font.Size = 8;
            formatRange.HorizontalAlignment = 3;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(System.Drawing.Color.White);
            formatRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
            Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
            Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);
            Worksheet.Cells[2, 1] = "Código";
            Worksheet.Cells[2, 2] = "Razón Social";
            Worksheet.Cells[2, 3] = "CUIT";
            Worksheet.Cells[2, 4] = "Localidad";
            Worksheet.Cells[2, 5] = "Provincia";
            Worksheet.Cells[2, 6] = "Teléfono";
            Worksheet.Cells[2, 7] = "Celular";
            Worksheet.Cells[2, 8] = "Tipo Cliente";
            Worksheet.Cells[2, 9] = "Mail";
            Worksheet.Cells[2, 10] = "Saldo $";

            //Locked a Range
            Worksheet.get_Range("A1", "J2").Locked = true;

        }

        #endregion


        #region Metodo MakeItCliente()

        private void MakeItCliente(Microsoft.Office.Interop.Excel._Worksheet Worksheet, int countFila, DataGridViewRow fila)
        {
            Color bgColor;

            //Range
            Microsoft.Office.Interop.Excel.Range formatRange;

     
            bgColor = System.Drawing.Color.White;

            //Columnas con datos y formateadas
            formatRange = Worksheet.get_Range("a" + countFila, "a" + countFila);
            formatRange.NumberFormat = "#,###,###"; ;
            formatRange.Font.Size = 8;
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignRight;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(System.Drawing.Color.White);


            formatRange = Worksheet.get_Range("b" + countFila, "b" + countFila);
            formatRange.NumberFormat = "#,###,###";
            formatRange.Font.Size = 8;
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(bgColor);

            formatRange = Worksheet.get_Range("c" + countFila, "c" + countFila);
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignRight;
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(bgColor);

            formatRange = Worksheet.get_Range("d" + countFila, "d" + countFila);
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(bgColor);

            formatRange = Worksheet.get_Range("e" + countFila, "e" + countFila);
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(bgColor);

            formatRange = Worksheet.get_Range("f" + countFila, "f" + countFila);
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(bgColor);

            formatRange = Worksheet.get_Range("g" + countFila, "g" + countFila);
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(bgColor);

            formatRange = Worksheet.get_Range("h" + countFila, "h" + countFila);
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(bgColor);

            formatRange = Worksheet.get_Range("i" + countFila, "i" + countFila);
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(bgColor);

            formatRange = Worksheet.get_Range("j" + countFila, "j" + countFila);
            formatRange.HorizontalAlignment = XlHAlign.xlHAlignRight;
            formatRange.Font.Size = 8;
            formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(System.Drawing.Color.White);
            formatRange.NumberFormat = "#,###,###0.00";

            //Escribir los datos
            Worksheet.Cells[countFila, 1] = fila.Cells["Codigo"].Value.ToString();
            Worksheet.Cells[countFila, 2] = fila.Cells["RazonSocial"].Value.ToString();
            Worksheet.Cells[countFila, 3] = fila.Cells["Cuit"].Value.ToString();
            Worksheet.Cells[countFila, 4] = fila.Cells["Localidad"].Value.ToString();
            Worksheet.Cells[countFila, 5] = fila.Cells["Provincia"].Value.ToString();
            Worksheet.Cells[countFila, 6] = fila.Cells["Telefono"].Value.ToString();
            Worksheet.Cells[countFila, 7] = fila.Cells["Celular"].Value.ToString();
            Worksheet.Cells[countFila, 8] = fila.Cells["Tipo"].Value.ToString();
            Worksheet.Cells[countFila, 9] = fila.Cells["Mail"].Value.ToString();
            Worksheet.Cells[countFila, 10] = Convert.ToDouble(fila.Cells["Saldo"].Value).ToString(("#0.00"));

            //Locked a Range
            Worksheet.get_Range("A" + countFila, "A" + countFila).Locked = false;
            //Borders
            Worksheet.get_Range("A" + countFila + ":J" + countFila).Cells.Borders.LineStyle = XlLineStyle.xlContinuous;

            //Locked a Range
            Worksheet.get_Range("A" + countFila, "J" + countFila).Locked = true;

            //Borders
            Worksheet.get_Range("A" + countFila + ":J" + countFila).Cells.Borders.LineStyle = XlLineStyle.xlContinuous;

        }

        #endregion

    }
}





using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Prama
{
    class clsArticulos
    {
        #region Descripción de la clase y su funcionalidad

        // Clase  que almacena en memoria datos de Articulos. N.
        // Test
        #endregion

        #region Propiedades de la clase

        public int IdArticulo  { set; get; }
	    public int IdSubrubroArticulo { set; get; }
	    public int IdTipoArticulo { set; get; }
	    public int IdUnidadMedida { set; get; }
	    public string CodigoArticulo { set; get; }
	    public string Articulo { set; get; }
	    public double Unidades { set; get; }
	    public double Precio { set; get; }
        public double PrecioAnterior { set; get; }
	    public double UltimoCostoCompra { set; get; }
	    public string UltimoProveedor { set; get; }
	    public string UltimaCompra { set; get; }
	    public int LlevaStock { set; get; }
	    public int Facturable { set; get; }
	    public double Stock { set; get; }
	    public double StockMinimo { set; get; }
	    public double StockMaximo { set; get; }
	    public double StockPuntoPedido { set; get; }
	    public double PorcentajeIva { set; get; }
        public int Activo { set; get; }
        public int incListaPre { set; get; }
        public int incListaRes { set; get; }
        public int chkSombreado { set; get; }
        public int rbtColor { set; get; }
        public int chkSProd { set; get; }
        public int CompIns { set; get; }

        #endregion

        #region Constructores

        /*Sin parametros */
        public clsArticulos()
        { }

        /*Con Parametros*/
        public clsArticulos(int p_IdSubrubroArticulo, int p_IdTipoArticulo, int p_IdUnidadMedida, string p_CodigoArticulo, 
                        string p_Articulo, double p_Unidades, double p_Precio, double p_UltimoCostoCompra, string p_UltimoProveedor,        
                        string p_UltimaCompra, int p_LlevaStock, int p_Facturable, double p_Stock, double p_StockMinimo,
                        double p_StockMaximo, double p_StockPuntoPedido, double p_PorcentajeIva,int p_Activo) 
        {
	                    
	        IdSubrubroArticulo= p_IdSubrubroArticulo;
	        IdTipoArticulo= p_IdTipoArticulo;
	        IdUnidadMedida= p_IdUnidadMedida;	
	        CodigoArticulo= p_CodigoArticulo;
	        Articulo= p_Articulo;
	        Unidades= p_Unidades;
	        Precio= p_Precio;
	        UltimoCostoCompra= p_UltimoCostoCompra;
	        UltimoProveedor= p_UltimoProveedor;
	        UltimaCompra= p_UltimaCompra;
	        LlevaStock= p_LlevaStock;
	        Facturable= p_Facturable;
	        Stock= p_Stock;
	        StockMinimo= p_StockMinimo;
	        StockMaximo= p_StockMaximo;
	        StockPuntoPedido= p_StockPuntoPedido;
	        PorcentajeIva= p_PorcentajeIva;
            Activo = p_Activo;
        }

        #endregion

        #region Método que valida el nuevo Articulo
        //Metodo que valida los datos del articulo cargados al objeto
        public string[] cValidaArticulo()
        {
            string[] mValida = new string[40];
            int cantError = 0;
            
            //VALIDAR Código de Artìculo
            if (string.IsNullOrEmpty(this.CodigoArticulo.ToString()))
            {
                mValida[cantError] = "EL CAMPO 'CODIGO DE ARTICULO' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.CodigoArticulo == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO 'CODIGO DE ARTICULO'";
                cantError += 1;
            }
            else 
            {
                if (ExisteCodigo(this.CodigoArticulo) && (clsGlobales.myEstado == "A"))
                {
                    mValida[cantError] = "EL 'CODIGO DE ARTICULO' ESPECIFICADO YA FUE UTILIZADO! CAMBIELO!";
                    cantError += 1;
                }
            }

            //Validar ARTICULO
            if (string.IsNullOrEmpty(this.Articulo.ToString()))
            {
                mValida[cantError] = "EL CAMPO 'ARTICULO' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.Articulo == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO 'ARTICULO'";
                cantError += 1;
            }


            //Validar SubRubro
            if (string.IsNullOrEmpty(this.IdSubrubroArticulo.ToString()))
            {
                mValida[cantError] = "EL CAMPO 'SUB-RUBRO' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.IdSubrubroArticulo == 0)
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO 'SUB-RUBRO'";
                cantError += 1;
            }

            //Validar UNIDADES            
            if (string.IsNullOrEmpty(this.Unidades.ToString()))
            {
                mValida[cantError] = "EL CAMPO 'UNIDADES' ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.Unidades > 99999999999.99)
            {
                mValida[cantError] = "EL CAMPO 'UNIDADES' NO PUEDE SER MAYOR A 99999999999.99";
                cantError += 1;
            }
            else if (this.Unidades == 0)
            {
                mValida[cantError] = "EL CAMPO 'UNIDADES' DEBER SER MAYOR A CERO!";
                cantError += 1;
            }

            //Validar PRECIO
            if (string.IsNullOrEmpty(this.Precio.ToString()))
            {
                mValida[cantError] = "EL CAMPO 'PRECIO' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.Precio > 99999999999.99)
            {
                mValida[cantError] = "EL CAMPO 'PRECIO' NO PUEDE SER MAYOR A 99999999999.99";
                cantError += 1;
            }
            else if (this.Precio == 0)
            {
                mValida[cantError] = "EL CAMPO 'PRECIO' DEBER SER MAYOR A CERO!";
                cantError += 1;
            }


            //Validar ULTIMO COSTO COMPRA, solo si esta cargado
            if (!(this.UltimoCostoCompra == 0))
            {
                if (this.UltimoCostoCompra > 99999999999.99)
                {
                    mValida[cantError] = "EL CAMPO 'ULTIMO COSTO COMPRA' NO PUEDE SER MAYOR A 99999999999.99!";
                    cantError += 1;
                }
            }

            //Validar UNIDAD DE MEDIDA
            if (string.IsNullOrEmpty(this.IdUnidadMedida.ToString()))
            {
                mValida[cantError] = "EL CAMPO 'UNIDAD DE MEDIDA' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.IdUnidadMedida == 0)
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO 'UNIDAD DE MEDIDA'";
                cantError += 1;
            }

            //SI LLEVA STOCK, VALIDAR LOS DATOS DE STOCK
            if (this.LlevaStock == 1)
            {
                //Validar STOCK
               /* if (string.IsNullOrEmpty(this.Stock.ToString()))
                {
                    mValida[cantError] = "EL CAMPO 'STOCK' NO PUEDE ESTAR VACIO!";
                    cantError += 1;
                }
               */

                if (this.Stock > 99999999999.99)
                {
                    mValida[cantError] = "EL CAMPO 'STOCK' NO PUEDE SER MAYOR A 99999999999.99";
                    cantError += 1;
                }
                
                /*if (this.Stock == 0)
                {
                    mValida[cantError] = "EL CAMPO 'STOCK' DEBER SER MAYOR A CERO!";
                    cantError += 1;
                }*/

                //Validar STOCK MIN
                if (string.IsNullOrEmpty(this.StockMinimo.ToString()))
                {
                    mValida[cantError] = "EL CAMPO 'STOCK MINIMO' NO PUEDE ESTAR VACIO!";
                    cantError += 1;
                }
                else if (this.StockMinimo > 99999999999.99)
                {
                    mValida[cantError] = "EL CAMPO 'STOCK MINIMO' NO PUEDE SER MAYOR A 99999999999.99";
                    cantError += 1;
                }
              /*  else if (this.StockMinimo == 0)
                {
                    mValida[cantError] = "EL CAMPO 'STOCK MINIMO' DEBER SER MAYOR A CERO!";
                    cantError += 1;
                }*/

                //Validar STOCK MAX
                if (string.IsNullOrEmpty(this.StockMaximo.ToString()))
                {
                    mValida[cantError] = "EL CAMPO 'STOCK MAXIMO' NO PUEDE ESTAR VACIO!";
                    cantError += 1;
                }
                else if (this.StockMaximo > 99999999999.99)
                {
                    mValida[cantError] = "EL CAMPO 'STOCK MAXIMO' NO PUEDE SER MAYOR A 99999999999.99";
                    cantError += 1;
                }
               /* else if (this.StockMaximo == 0)
                {
                    mValida[cantError] = "EL CAMPO 'STOCK MAXIMO' DEBER SER MAYOR A CERO!";
                    cantError += 1;
                }*/

                //Validar STOCK PUNTO PEDIDO
                if (string.IsNullOrEmpty(this.StockPuntoPedido.ToString()))
                {
                    mValida[cantError] = "EL CAMPO 'STOCK PUNTO PEDIDO' NO PUEDE ESTAR VACIO!";
                    cantError += 1;
                }
                else if (this.StockPuntoPedido > 99999999999.99)
                {
                    mValida[cantError] = "EL CAMPO 'STOCK PUNTO PEDIDO' NO PUEDE SER MAYOR A 99999999999.99";
                    cantError += 1;
                }
                /*else if (this.StockPuntoPedido == 0)
                {
                    mValida[cantError] = "EL CAMPO 'STOCK PUNTO PEDIDO' DEBER SER MAYOR A CERO!";
                    cantError += 1;
                }*/

                //STOCK DEBE ESTAR ENTRE MINIMO Y MAXIMO
              /*  if (this.Stock < this.StockMinimo || this.Stock > this.StockMaximo)
                {
                    mValida[cantError] = "EL CAMPO 'STOCK' DEBE ESTAR EN EL MINIMO Y MAXIMO!";
                    cantError += 1;
                }*/

                //STOCK MINIMO < A MAXIMO y STOCK MAXIMO > A MINIMO
                if (this.StockMinimo > this.StockMaximo)
                {
                    mValida[cantError] = "EL CAMPO 'STOCK MINIMO' DEBE SER MENOR AL STOCK MAXIMO!";
                    cantError += 1;
                }
                else if (this.StockMaximo < this.StockMinimo)                
                {
                    mValida[cantError] = "EL CAMPO 'STOCK MAXIMO' DEBE SER MAYOR AL STOCK MINIMO!";
                    cantError += 1;
                }

                //CAMPO STOCK PUNTO PEDIDO < MINIMO?
              /*  if (this.StockPuntoPedido > this.StockMinimo)
                {
                    mValida[cantError] = "EL CAMPO 'STOCK PUNTO PEDIDO' DEBE SER MENOR AL STOCK MINIMO!";
                    cantError += 1;
                }*/
            }

            
            //VALIDAR PORCENTAJE IVA
            if (string.IsNullOrEmpty(this.PorcentajeIva.ToString()))
            {
                mValida[cantError] = "EL CAMPO 'PORCENTAJE IVA' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.PorcentajeIva > 100.00)
            {
                mValida[cantError] = "EL CAMPO 'PORCENTAJE IVA' NO PUEDE SER MAYOR A 100%";
                cantError += 1;
            }
           /* else if (this.PorcentajeIva == 0)
            {
                mValida[cantError] = "EL CAMPO 'PORCENTAJE IVA' DEBER SER MAYOR A CERO!";
                cantError += 1;
            }*/

            //Validar Fecha de Ultima Compra
            if (Convert.ToDateTime(this.UltimaCompra) > DateTime.Now.Date)
            {
               mValida[cantError] = "LA FECHA DE ULTIMA COMPRA NO PUEDE SER MAYOR A LA ACTUAL!";
               cantError += 1;
            }



            return mValida;
        }
        #endregion

        #region Metodo: ExisteCodigo

        private bool ExisteCodigo(string p_Codigo)
        {
            bool bResult = false;

            int nElementos = 0;

            string CadSQL = "Select count(*) as nElementos from Articulos where CodigoArticulo = '" + p_Codigo.ToUpper() + "'";

            //Crear el DataTable con la consulta.
                DataTable mDataTable = clsDataBD.GetSql(CadSQL);

            //Verificar si hay elementos en la consulta.
                foreach (DataRow row in mDataTable.Rows)
                {
                    nElementos = Convert.ToInt32(row["nElementos"]);
                }

                if (!(nElementos == 0))
                {
                    bResult = true;        
                }

                return bResult;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prama
{
    class clsGastos
    {
        
        #region propiedades

            public int IdGastoFijo {get;set;}
            public string Codigo {get;set;}
            public int IdTipoGasto {get;set;}
            public int Periodo {get;set;}
            public int Año {get;set;}
            public int IdUnidadMedida {get;set;}
            public double DescuentoIVA {get;set;}
            public double Unidades {get;set;}
            public double Monto {get;set;}
            public double Costo {get;set;}
            public string Descrip {get;set;}
            public string Comprador { get; set; }
            public int IdAlmacen { get; set; }
            public int IdPunto { get; set; }
            public string Punto { get; set; }
            public string Numero { get; set; }
            public DateTime Fecha { get; set;}
            public int IdCondicionCompra { get; set; }
            public int IdProveedor { get; set; }

        #endregion
        
        #region Constructor

        public clsGastos()
        {

        }

        #endregion

        #region Método que valida la nueva Localidad
        //METODO QUE VALIDA EL OBJETO GASTO (VALOR DE PROPIEDADES CARGADAS). N.
        public string[] cValidaGasto()
        {
            string[] mValida = new string[15];
            int cantError = 0;

            //VALIDAR Codigo
            if (string.IsNullOrEmpty(this.Codigo))
            {
                mValida[cantError] = "EL CAMPO 'CODIGO' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.Codigo == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO 'CODIGO'";
                cantError += 1;
            }
            //VALIDAR Descripcion
            if (string.IsNullOrEmpty(this.Descrip))
            {
                mValida[cantError] = "EL CAMPO 'DESCRIPCION' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.Descrip == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO 'CODIGO'";
                cantError += 1;
            }
            //VALIDAR Tipo Gasto
            if (string.IsNullOrEmpty(this.IdTipoGasto.ToString()))
            {
                mValida[cantError] = "DEBE SELECCIONAR EL 'TIPO DE GASTO'!";
                cantError += 1;
            }
            else if (this.IdTipoGasto == 0)
            {
                mValida[cantError] = "DEBE COMPLETAR EL 'TIPO DE GASTO'!";
                cantError += 1;
            }

            //VALIDAR Punto
            if (string.IsNullOrEmpty(this.Punto))
            {
                mValida[cantError] = "EL CAMPO 'PUNTO' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.Punto  == "0")
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO 'PUNTO'!";
                cantError += 1;
            }

            //VALIDAR Numero
            if (string.IsNullOrEmpty(this.Numero))
            {
                mValida[cantError] = "EL CAMPO 'NUMERO' NO PUEDE ESTAR VACIO!!";
                cantError += 1;
            }
            else if (this.Numero == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO 'NUMERO'!";
                cantError += 1;
            }

            //Validar Monto            
            if (string.IsNullOrEmpty(this.Monto.ToString()))
            {
                mValida[cantError] = "EL CAMPO 'IMPORTE' ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.Monto > 99999999999.99999)
            {
                mValida[cantError] = "EL CAMPO 'IMPORTE' NO PUEDE SER MAYOR A 99999999999.99999";
                cantError += 1;
            }
            else if (this.Monto == 0)
            {
                mValida[cantError] = "EL CAMPO 'IMPORTE' DEBER SER MAYOR A CERO!";
                cantError += 1;
            }

            //Validar Descuento por IVA            
            if (string.IsNullOrEmpty(this.DescuentoIVA.ToString()))
            {
                mValida[cantError] = "EL CAMPO 'DESCUENTO IVA' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.Unidades > 99999999999.99999)
            {
                mValida[cantError] = "EL CAMPO 'DESCUENTO IVA' NO PUEDE SER MAYOR A 99999999999.99999";
                cantError += 1;
            }

            //Validar UNIDADES            
            if (string.IsNullOrEmpty(this.Unidades.ToString()))
            {
                mValida[cantError] = "EL CAMPO 'UNIDADES' ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.Unidades > 999999.99999)
            {
                mValida[cantError] = "EL CAMPO 'UNIDADES' NO PUEDE SER MAYOR A 999999.99999";
                cantError += 1;
            }
            else if (this.Unidades == 0)
            {
                mValida[cantError] = "EL CAMPO 'UNIDADES' DEBER SER MAYOR A CERO!";
                cantError += 1;
            }

            //Validar UNIDADES            
            if (string.IsNullOrEmpty(this.Costo.ToString()))
            {
                mValida[cantError] = "EL CAMPO 'COSTO' ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.Costo > 999999.99999)
            {
                mValida[cantError] = "EL CAMPO 'COSTO' NO PUEDE SER MAYOR A 999999.99999";
                cantError += 1;
            }
            else if (this.Costo == 0)
            {
                mValida[cantError] = "EL CAMPO 'COSTO' DEBER SER MAYOR A CERO!";
                cantError += 1;
            }
            //VALIDAR Condicion Compra
            if (string.IsNullOrEmpty(this.IdCondicionCompra.ToString()))
            {
                mValida[cantError] = "DEBE SELECCIONAR EL CAMPO 'CONDICION COMPRA'!";
                cantError += 1;
            }
            else if (this.IdCondicionCompra == 0)
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO 'CONDICION COMPRA'!";
                cantError += 1;
            }

            return mValida;
        }
        #endregion

    }
}

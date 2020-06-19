using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prama.Clases
{
    class clsGastosComunes
    {
        #region Propiedades de la clase

        public int IdTipoComprobanteCompra { set; get; }
        public int IdProveedor { set; get; }
        public int IdPuntoVenta { set; get; }
        public int IdAlmacen { set; get; }
        public int IdCondicionCompra { set; get; }
        public string Fecha { set; get; }
        public DateTime FechaReal { set; get; }
        public string Numero { set; get; }
        public int CantidadArticulos { set; get; }
        public string Vence { set; get; }
        public string Descripcion { set; get; }
        public double Neto { set; get; }
        public double Iva25 { set; get; }
        public double Iva50 { set; get; }
        public double Iva105 { set; get; }
        public double Iva210 { set; get; }
        public double Iva270 { set; get; }
        public double Total { set; get; }
        public double Saldo { set; get; }
        public int Activo { set; get; }
        public double PercepcionesVarias { set; get; }
        public double PercepcionesIva { set; get; }
        public double PercepcionesIB { set; get; }
        public double Exento { set; get; }
        public double Flete { set; get; }
        public string Usuario { set; get; }
        public int IdImputacion { set; get; }
        public int Compra { set; get; }

        #endregion

        #region Constructor de la clase

        public clsGastosComunes()
        {

        }

        #endregion

        #region Método que valida los nuevos parametros

        public string[] cValidaComprobantes()
        {
            string[] mValida = new string[30];
            int cantError = 0;

            //VALIDAR IdProveedor
            if (string.IsNullOrEmpty(this.IdProveedor.ToString()))
            {
                mValida[cantError] = "DEBE SELECCIONAR UN 'PROVEEDOR'!";
                cantError += 1;
            }
            else if (this.IdProveedor.ToString() == " ")
            {
                mValida[cantError] = "DEBE SELECCIONAR UN 'PROVEEDOR'!";
                cantError += 1;
            }

            //VALIDAR Punto de venta
            if (string.IsNullOrEmpty(this.IdPuntoVenta.ToString()))
            {
                mValida[cantError] = "EL CAMPO 'PUNTO DE COMPRA / VENTA' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.IdPuntoVenta == 0)
            {
                mValida[cantError] = "DEBE SELECCIONAR UN 'PUNTO DE COMPRA / VENTA'";
                cantError += 1;
            }

            //VALIDAR Almacen
            if (string.IsNullOrEmpty(this.IdAlmacen.ToString()))
            {
                mValida[cantError] = "EL CAMPO 'ALMACEN' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.IdAlmacen == 0)
            {
                mValida[cantError] = "DEBE SELECCIONAR UN 'ALMACEN'!";
                cantError += 1;
            }

            //VALIDAR Condicion de compra
            if (string.IsNullOrEmpty(this.IdCondicionCompra.ToString()))
            {
                mValida[cantError] = "EL CAMPO 'CONDICIÓN DE COMPRA' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.IdCondicionCompra == 0)
            {
                mValida[cantError] = "DEBE SELECCIONAR LA 'CONDICIÓN DE COMPRA'!";
                cantError += 1;
            }

            //VALIDAR Fecha
            if (string.IsNullOrEmpty(this.Fecha))
            {
                mValida[cantError] = "EL CAMPO 'FECHA' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.Fecha == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO 'FECHA'!";
                cantError += 1;
            }

            //VALIDAR Fecha
            if (string.IsNullOrEmpty(this.Numero))
            {
                mValida[cantError] = "EL CAMPO 'PUNTO' Y 'NÚMERO' NO PUEDEN ESTAR VACIOS!";
                cantError += 1;
            }
            else if (this.Numero == "-")
            {
                mValida[cantError] = "DEBE COMPLETAR LOS CAMPOS 'PUNTO' Y 'NÚMERO'!";
                cantError += 1;
            }

            //VALIDAR Fecha
            if (string.IsNullOrEmpty(this.Descripcion))
            {
                mValida[cantError] = "EL CAMPO 'DESCRIPCIÓN' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.Descripcion == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO 'DESCRIPCIÓN'!";
                cantError += 1;
            }

            //VALIDAR Fecha
            if (string.IsNullOrEmpty(this.Neto.ToString()))
            {
                mValida[cantError] = "EL CAMPO 'NETO' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.Neto == 0)
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO 'NETO'!";
                cantError += 1;
            }

            //VALIDAR Fecha
            if (string.IsNullOrEmpty(this.IdImputacion.ToString()))
            {
                mValida[cantError] = "EL CAMPO 'NETO' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.IdImputacion == 0)
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO 'NETO'!";
                cantError += 1;
            }

            //RETORNAR VECTOR
            return mValida;

        }


        #endregion

    }
}

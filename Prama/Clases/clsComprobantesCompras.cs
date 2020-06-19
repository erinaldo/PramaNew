using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prama
{
    class clsComprobantesCompras
    {
        
        #region Propiedades de la clase
        
        public int IdComprasCotizaciones { set; get; }
        public int IdTipoComprobanteCompra { set; get; }
        public int IdProveedor { set; get; }
        public int IdPuntoVenta { set; get; }
        public int IdAlmacen { set; get; }
        public int IdCondicionCompra { set; get; }
        public string Usuario { set; get; }
        public string NumReferencia { set; get; }
        public string Fecha { set; get; }
        public DateTime FechaReal { set; get; }
        public string Numero { set; get; }
        public string Vence { set; get; }
        public int CantidadArticulos { set; get; }
        public double Neto { set; get; }
        public double Iva105 { set; get; }
        public double Iva210 { set; get; }
        public double Total { set; get; }
        public double Saldo { set; get; }
        public int Activo { set; get; }
        public double PercepcionesVarias { set; get; }
        public double PercepcionesIva { set; get; }
        public double PercepcionesIB { set; get; }
        public double Exento { set; get; }
        public double Flete { set; get; }
        public int IdImputacion { set; get; }
        public int Compra { set; get; }

        #endregion

        #region Métodos de la clase

        #region Método Contructor

        public clsComprobantesCompras()
        {
            IdComprasCotizaciones = 0;
            IdTipoComprobanteCompra = 0;
            IdProveedor = 0;
            IdPuntoVenta = 0;
            IdAlmacen = 0;
            IdCondicionCompra = 0;
            Usuario = "";
            NumReferencia = "";
            Fecha = "";
            FechaReal = DateTime.Now;
            Numero = "";
            Vence = "";
            CantidadArticulos = 0;
            Neto = 0;
            Iva105 = 0;
            Iva210 = 0;
            Total = 0;
            Saldo = 0;
            Activo = 1;
            PercepcionesVarias = 0;
            PercepcionesIva = 0;
            PercepcionesIB = 0;
            Exento = 0;
            Flete = 0;
            IdImputacion = 0;
            Compra = 0;
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

            //VALIDAR CantidadArticulos
            if (string.IsNullOrEmpty(this.CantidadArticulos.ToString()))
            {
                mValida[cantError] = "DEBE CARGAR ARTÍCULOS!";
                cantError += 1;
            }
            else if (this.CantidadArticulos == 0)
            {
                mValida[cantError] = "DEBE CARGAR ARTÍCULOS A LA GRILLA!";
                cantError += 1;
            }

            //RETORNAR VECTOR
            return mValida;

        }


        #endregion

        #endregion

    }
}

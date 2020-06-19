using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prama.Clases;

namespace Prama
{
    class clsProveedores
    {

        #region Descripción de la clase y su funcionalidad
        // Clase  que almacena en memoria datos de Proveedores. N.
        // Métodos: cValidaProveedor, controla que los datos del Proveedor sean correctos mediante un vector de
        //          errores. N.
        //          GrabarProveedor: Graba los datos del nuevo proveedor en la BD. G.
        //          toStringProveedor: Devuelve un string con los datos del Proveedor, menos el ID. N.
        //          RetornarUltimoID: Devuelve un int con el último IdProveedor de la tabla Localidades de la BD. N.
        #endregion

        #region Propiedades de la clase Proveedores
        // Creo las propiedades de la clase. G.
            public int IdProveedor { set; get; }
            public string NombreFantasia { set; get; }
            public string RazonSocial { set; get; }
            public int IdCondicionIva { set; get; }
            public string CondicionIva { set; get; }
            public double PorcentajeIva { set; get; }
            public string CUIT { set; get; }
            public string IngresosBrutos { set; get; }
            public string FechaInicioActividad { set; get; }
            public int  IdCondicionCompra{ set; get; }
            public string CondicionCompra { set; get; }
            public string Direccion{ set; get; }
            public int IdProvincia { set; get; }
            public string Provincia { set; get; }
            public string Cp { set; get; }
            public int IdLocalidad{ set; get; }
            public string Localidad { set; get; }
            public string Telefono{ set; get; }
            public string Fax{ set; get; }
            public string Celular{ set; get; }
            public string MailEmpresa{ set; get; }
            public string Web{ set; get; }
            public string Contacto{ set; get; }
            public string MailContacto{ set; get; }
            public string CelularContacto{ set; get; }
            public string Observaciones { set; get; }
            public int ProvIns { set; get; }
            public int ProvProd { set; get; }
            public double Saldo {set;get;}
            public double SaldoAFavor { set; get; }
            public double SaldoInicial { set; get; }
            
       
      #endregion

        #region Métodos contructores de la clase Proveedores
        // Creo los métodos constructores. G.
        // Sin parámetros
        public clsProveedores()
        {

        }
        // Con parámetros
        public clsProveedores(int p_IdProveedor, string p_NombreFantasia, string p_RazonSocial, int p_IdCondicionIva, string p_CUIT, string p_IngresosBrutos,
                              string p_FechaInicioActividad, int p_IdCondicionCompra, string p_Direccion,int p_IdProvincia, int p_IdLocalidad, string p_Telefono,
                              string p_Fax, string p_Celular, string p_MailEmpresa, string p_Web, string p_Contacto, string p_MailContacto,
                              string p_CelularContacto, string p_Observaciones, int p_ProvIns, int p_ProvProd, double p_Saldo)
        {
            IdProveedor = p_IdProveedor;
            NombreFantasia = p_NombreFantasia;
            RazonSocial= p_RazonSocial;
            IdCondicionIva = p_IdCondicionIva;
            CUIT = p_CUIT;
            IngresosBrutos = p_IngresosBrutos;
            FechaInicioActividad = p_FechaInicioActividad;
            IdCondicionCompra = p_IdCondicionCompra;
            Direccion = p_Direccion;
            IdProvincia = p_IdProvincia;
            IdLocalidad = p_IdLocalidad;
            Telefono = p_Telefono;
            Fax = p_Fax;
            Celular = p_Celular;
            MailEmpresa = p_MailEmpresa;
            Web = p_Web;
            Contacto = p_Contacto;
            MailContacto = p_MailContacto;
            CelularContacto  =p_CelularContacto;
            Observaciones = p_Observaciones;
            ProvIns = p_ProvIns;
            ProvProd = p_ProvProd;
            Saldo = p_Saldo;

        }

        #endregion

        #region Método que valida el nuevo Proveedor
        //METODO QUE VALIDA EL OBJETO PROVEEDOR (VALOR DE PROPIEDADES CARGADAS). N.
        public string[] cValidaProveedor()
        {
            string[] mValida = new string[20];
            int cantError = 0;
            //VALIDAR Razon Social
            if (string.IsNullOrEmpty(RazonSocial.ToString()))
            {
                mValida[cantError] = "EL CAMPO RAZON SOCIAL NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (RazonSocial == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO RAZON SOCIAL";
                cantError += 1;
            }

            //Validar Condicion de IVA
            if (string.IsNullOrEmpty(this.IdCondicionIva.ToString()))
            {
                mValida[cantError] = "EL CAMPO CONDICION DE IVA NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.IdCondicionIva == 0)
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO CONDICION DE IVA";
                cantError += 1;
            }


            //Validar CUIT            
            if (string.IsNullOrEmpty(this.CUIT.ToString()))
            {
                mValida[cantError] = "EL CAMPO CUIT NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.CUIT == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO CUIT";
                cantError += 1;
            }
            else {
                //Validar la CUIT                
                clsCUIT oCUITvalido = new clsCUIT(this.CUIT);

                if (!(oCUITvalido.EsValido))
                {
                    mValida[cantError] = "EL CUIT INGRESADO ES INCORRECTO. VERIFIQUE!";
                    cantError += 1;
                }
            }


            //Validar Condicion de Compra
            if (string.IsNullOrEmpty(this.IdCondicionCompra.ToString()))
            {
                mValida[cantError] = "EL CAMPO CONDICION DE COMPRA NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.IdCondicionCompra == 0)
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO CONDICION DE COMPRA";
                cantError += 1;
            }


            //Validar Direccion
            if (string.IsNullOrEmpty(this.Direccion.ToString()))
            {
                mValida[cantError] = "EL CAMPO DIRECCION NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.Direccion == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO DIRECCION";
                cantError += 1;
            }

            //Validar Provincia
            if (string.IsNullOrEmpty(this.IdProvincia.ToString()))
            {
                mValida[cantError] = "EL CAMPO PROVINCIA NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.IdProvincia == 0)
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO PROVINCIA";
                cantError += 1;
            }

            //Validar Localidad
            if (string.IsNullOrEmpty(this.IdLocalidad.ToString()))
            {
                mValida[cantError] = "EL CAMPO LOCALIDAD NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.IdLocalidad == 0)
            {
                    mValida[cantError] = "DEBE COMPLETAR EL CAMPO LOCALIDAD";
                cantError += 1;
            }

           //Validar Fecha Inicio Actividades no sea superior a la fecha actual N.
            if (Convert.ToDateTime(this.FechaInicioActividad) > DateTime.Now.Date)
            {
                mValida[cantError] = "LA FECHA DE INICIO DE ACTIVIDAD NO PUEDE SER MAYOR A LA ACTUAL!";
                cantError += 1;
            }

            return mValida;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Prama.Clases;

namespace Prama
{
    class clsCLientes
    {
        public int Codigo {set; get;}
        public string RazonSocial {set; get;}
        public string ApeNom { set; get; }
        public int IdCondicionIva { set; get; }
        public int IdCondicionCompra { set; get; }
        public int IdTransporte { set; get; }
        public string Condicion { set; get; }
        public string Cuit {set; get;}
        public string Direccion {set; get;}
        public string Barrio {set; get;}
        public int IdLocalidad { set; get; }
        public string Localidad {set; get;}
        public string CP {set; get;}
        public int IdProvincia { set; get; }
        public string Provincia { set; get; }
        public string Telefono {set; get;}
        public string Celular {set; get;}
        public string Fax {set; get;}
        public string Mail {set; get;}
        public string Web {set; get;}
        public string Nacimiento {set; get;}
        public int IdTipoCliente { set; get; }
        public string Tipo {set; get;}
        public string Observaciones {set; get;}
        public string Alta { set; get; }
        public double Saldo { set; get; }
        public double SaldoAFavor { set; get; }
        public double SaldoInicial { set; get; }
        public int Activo { set; get; }
        public int chkCRsal { set; get; }
 
        public clsCLientes()
        {
            Codigo = 0;
            RazonSocial = "";
            IdCondicionIva = 0;
            IdCondicionCompra = 0;
            Condicion = "";
            Cuit = "";
            Direccion = "";
            Barrio = "";
            IdLocalidad = 0;
            Localidad = "";
            CP = "";
            IdProvincia = 0;
            Provincia = "";
            Telefono = "";
            Celular = "";
            Fax = "";
            Mail = "";
            Web = "";
            Nacimiento = DateTime.Now.ToString();
            IdTipoCliente = 0;
            Tipo = "";
            Observaciones = "";
            Alta = DateTime.Now.ToString();
            IdTransporte = 0;
            Saldo = 0;
            Activo = 0;
            chkCRsal = 0;
        }

        #region METODO EsFecha (VALIDA FECHA)
        public static Boolean EsFecha(String fecha)
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

        #region Método que valida el nuevo Cliente
        //METODO QUE VALIDA EL OBJETO CLIENTE (VALOR DE PROPIEDADES CARGADAS). N.
        public string[] cValidaCLiente()
        {
            string[] mValida = new string[22];
            int cantError = 0;

            //VALIDAR CÓDIGO
            if (string.IsNullOrEmpty(Codigo.ToString()))
            {
                mValida[cantError] = "EL CAMPO CÓDIGO NO PUEDE ESTAR VACÍO!!";
                cantError += 1;
            }
            else if (Codigo.ToString() == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO CÓDIGO!!";
                cantError += 1; 
            }

            //VALIDAR RAZÓN SOCIAL
            if (string.IsNullOrEmpty(RazonSocial.ToString()))
            {
                mValida[cantError] = "EL CAMPO RAZÓN SOCIAL NO PUEDE ESTAR VACÍO!!";
                cantError += 1; 
            }
            else if (RazonSocial == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO RAZÓN SOCIAL!!";
                cantError += 1; 
            }

            //VALIDAR TIPO DE CLIENTE
            if (string.IsNullOrEmpty(this.IdTipoCliente.ToString()))
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO 'TIPO DE CLIENTE'!!";
                cantError += 1;
            }
            else if (IdTipoCliente == 0)
            {
                mValida[cantError] = "DEBE SELECCIONAR EL 'TIPO DE CLIENTE'!!";
                cantError += 1;
            }

            //VALIDAR EL CUIT
            if (string.IsNullOrEmpty(Cuit.ToString()))
            {
                mValida[cantError] = "EL CAMPO CUIT NO PUEDE ESTAR VACÍO!!";
                cantError += 1;
            }
            else if (Cuit == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO CUIT!!";
                cantError += 1;
            }
            //else
            //{
            //    //Validar la CUIT                
            //    clsCUIT oCUITvalido = new clsCUIT(Cuit);

            //    if (!(oCUITvalido.EsValido))
            //    {
            //        mValida[cantError] = "EL CUIT INGRESADO ES INCORRECTO. VERIFIQUE!";
            //        cantError += 1;
            //    }
            //}

            //VALIDAR CONDICON IVA
            if (string.IsNullOrEmpty(IdCondicionIva.ToString()))
            {
                mValida[cantError] = "DEBE SELECCIONAR LA CONDICION FRENTE AL IVA!!";
                cantError += 1;
            }
            else if (IdCondicionIva == 0)
            {
                mValida[cantError] = "DEBE SELECCIONAR LA CONDICION FRENTE AL IVA!!";
                cantError += 1;
            }

            //LOCALIDAD
            if (string.IsNullOrEmpty(IdLocalidad.ToString()))
            {
                mValida[cantError] = "DEBE SELECCIONAR UNA LOCALIDAD!!";
                cantError += 1;
            }
            else if (IdLocalidad == 0)
            {
                mValida[cantError] = "DEBE ESPECIFICAR UNA LOCALIDAD!!";
                cantError += 1;
            }

            //PROVINCIA
            if (string.IsNullOrEmpty(IdProvincia.ToString()))
            {
                mValida[cantError] = "DEBE SELECCIONAR UNA PROVINCIA!!";
                cantError += 1;
            }
            else if (IdProvincia == 0)
            {
                mValida[cantError] = "DEBE ESPECIFICAR UNA PROVINCIA!!";
                cantError += 1;
            }
            //TRANSPORTE
            if (string.IsNullOrEmpty(IdTransporte.ToString()))
            {
                mValida[cantError] = "DEBE SELECCIONAR UN TRANSPORTE!!";
                cantError += 1;
            }
            else if (IdTransporte == 0)
            {
                mValida[cantError] = "DEBE ESPECIFICAR UN TRANSPORTE!!";
                cantError += 1;
            }
            //Se agregó la validación del teléfono a pedido de Mariano el 09/03/2016
          /*  if (string.IsNullOrEmpty(Telefono.ToString()))
            {
                mValida[cantError] = "EL CAMPO TELÉFONO NO PUEDE ESTAR VACÍO!!";
                cantError += 1;
            }
            else if (Telefono == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO TELÉFONO!!";
                cantError += 1;
            }*/

            // No funciona el tema de las fechas VER
            /*string dia = "";
            string mes = "";
            string ano = "";

            dia = clsGlobales.Mid(Alta, 3,2);
            mes = clsGlobales.Left(Alta, 2);
            ano = clsGlobales.Right(Alta, 4);

            string AltaF = Convert.ToDateTime(dia + "/" + mes + "/" + ano).ToString(); */

            //VALIDA FECHA ALTA
            if (!(EsFecha(Alta)))
            {
                mValida[cantError] = "DEBE ESPECIFICAR UNA FECHA DE ALTA VALIDA!!";
                cantError += 1;
            }
            else
            {
                if (clsGlobales.myEstado == "A")
                {
                    //SI LA FECHA ES MAYOR A LA DEL DIA EN CURSO
                    if (Convert.ToDateTime(Alta) > DateTime.Today)
                    {
                        mValida[cantError] = "LA FECHA DE ALTA NO PUEDE SER POSTERIOR A LA DEL DÍA EN CURSO!!";
                        cantError += 1;
                    }
                    if (Convert.ToDateTime(Alta) < DateTime.Today)
                    {
                        mValida[cantError] = "LA FECHA DE ALTA NO PUEDE SER ANTERIOR A LA DEL DÍA EN CURSO!!";
                        cantError += 1;
                    }
               }
            } 
            

            return mValida;
        }
        #endregion
    }
}

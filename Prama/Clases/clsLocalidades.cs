using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prama
{
    class clsLocalidades
    {
        // Clase Localidades - Líneas de código al 2015/07/27 ** 122 **
        //                   - Líneas de código al 2015/08/03 ** 160 **
        // Creada por Gabriel Ambrosich 
        // 2015 / 08 / 03
        #region Descripción de la clase y su funcionalidad
        // Clase  que almacena en memoria datos de las Localidades. G.
        // Métodos: cValidaLocalidad, controla que los datos de la Localidad sean correctos mediante un vector de
        //          errores. G.
        #endregion

        #region Propiedades de la clase Localidades
        // Creo las propiedades de la clase. G.
        public int IdLocalidad { set; get; }
        public string Localidad { set; get; }
        public string CP { set; get; }
        public int IdProvincia { set; get; }
        public string Provincia { set; get; }
        #endregion

        #region Métodos contructores de la clase Localidades
        // Creo los métodos constructores. G.
        // Sin parámetros
        public clsLocalidades()
        {
            IdLocalidad = 0;
            Localidad = "";
            CP = "";
            IdProvincia = 0;
            Provincia = "";
        }
        // Con parámetros
        public clsLocalidades(int IdLoc, string Loc, string cp, int IdPro, string Prov)
        {
            IdLocalidad = IdLoc;
            Localidad = Loc;
            CP = cp;
            IdProvincia = IdPro;
            Provincia = Prov;
        }
        #endregion

        #region Método que valida la nueva Localidad
        //METODO QUE VALIDA EL OBJETO LOCALIDAD (VALOR DE PROPIEDADES CARGADAS). N.
        public string[] cValidaLocalidad()
        {
            string[] mValida = new string[3];
            int cantError = 0;

            //VALIDAR Localidad
            if (string.IsNullOrEmpty(Localidad.ToString()))
            {
                mValida[cantError] = "EL CAMPO LOCALIDAD NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (Localidad == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO LOCALIDAD";
                cantError += 1;
            }
            //VALIDAR Codigo Postal
            if (string.IsNullOrEmpty(CP.ToString()))
            {
                mValida[cantError] = "EL CAMPO CÓDIGO POSTAL NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (CP == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO CÓDIGO POSTAL";
                cantError += 1;
            }
            //VALIDAR PROVINCIA
            if (string.IsNullOrEmpty(IdProvincia.ToString()))
            {
                mValida[cantError] = "DEBE SELECCIONAR UNA PROVINCIA!";
                cantError += 1;
            }
            else if (IdProvincia == 0)
            {
                mValida[cantError] = "DEBE SELECCIONAR UNA PROVINCIA!";
                cantError += 1;
            }
            return mValida;
        }
        #endregion
    }
}

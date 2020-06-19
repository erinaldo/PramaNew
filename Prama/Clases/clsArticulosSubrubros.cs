using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prama
{
    class clsArticulosSubrubros
    {
        // Clase Localidades - Líneas de código al 2015/07/27 ** 122 **
        //                   - Líneas de código al 2015/08/03 ** 160 **
        // Creada por Gabriel Ambrosich 
        // 2015 / 08 / 03
        #region Descripción de la clase y su funcionalidad
        // Clase  que almacena en memoria datos de las Localidades. G.
        // Métodos: cValidaSubrubro, controla que los datos de la Localidad sean correctos mediante un vector de
        //          errores. G.
        #endregion

        #region Propiedades de la clase SubrubrosArticulos
        // Creo las propiedades de la clase. G.
        public int IdSubRubroArticulo { set; get; }
        public string SubrRubroArticulo { set; get; }
        public int Activo { set; get; }
        public int IdRubroArticulo { set; get; }
        public string RubroArticulo { set; get; }
        public int Orden { set; get; }

        #endregion

        #region Métodos contructores de la clase Localidades
        // Creo los métodos constructores. G.
        // Sin parámetros
        public clsArticulosSubrubros()
        {
            IdSubRubroArticulo = 0;
            SubrRubroArticulo = "";
            Activo = 0;
            IdRubroArticulo = 0;
            RubroArticulo = "";
            Orden = 0;
        }
        // Con parámetros
        public clsArticulosSubrubros(int IdSub, string Sub, int Act, int IdRub, string Rub, int p_Orden)
        {
            IdSubRubroArticulo = IdSub;
            SubrRubroArticulo = Sub;
            Activo = Act;
            IdRubroArticulo = IdRub;
            RubroArticulo = Rub;
            Orden = p_Orden;
        }
        #endregion

        #region Método que valida el nuevo SubRubro
        //METODO QUE VALIDA EL OBJETO LOCALIDAD (VALOR DE PROPIEDADES CARGADAS). N.
        public string[] cValidaSubRubros()
        {
            string[] mValida = new string[2];
            int cantError = 0;

            //VALIDAR Localidad
            if (string.IsNullOrEmpty(SubrRubroArticulo.ToString()))
            {
                mValida[cantError] = "EL CAMPO SUBRUBRO NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (SubrRubroArticulo == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO SUBRUBRO";
                cantError += 1;
            }
            
            //VALIDAR RUBRO
            if (string.IsNullOrEmpty(IdRubroArticulo.ToString()))
            {
                mValida[cantError] = "DEBE SELECCIONAR UN RUBRO!";
                cantError += 1;
            }
            else if (IdRubroArticulo == 0)
            {
                mValida[cantError] = "DEBE SELECCIONAR UN RUBRO!";
                cantError += 1;
            }
            return mValida;
        }
        #endregion
    }
}

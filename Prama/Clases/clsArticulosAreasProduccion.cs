using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prama
{
    class clsArticulosAreasProduccion
    {
        #region Propiedades de la clase

        public int IdAreaProduccion { set; get; }
        public string AreasProduccion { set; get; }
        public int Activo { set; get; }

        #endregion

        #region Constructores de la clase

        public clsArticulosAreasProduccion()
        {
            IdAreaProduccion = 0;
            AreasProduccion = "";
            Activo = 0;
        }

        public clsArticulosAreasProduccion(int IdA, string Are, int Act)
        {
            IdAreaProduccion = IdA;
            AreasProduccion = Are;
            Activo = Act;
        }

        #endregion

        #region Método que valida la nueva Area de producción
        //METODO QUE VALIDA EL OBJETO (VALOR DE PROPIEDADES CARGADAS). N.
        public string[] cValidaArea()
        {
            string[] mValida = new string[1];
            int cantError = 0;

            //VALIDAR Localidad
            if (string.IsNullOrEmpty(AreasProduccion.ToString()))
            {
                mValida[cantError] = "EL CAMPO ÁREA NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (AreasProduccion == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO ÁREA";
                cantError += 1;
            }

            return mValida;
        }
        #endregion
    }
}

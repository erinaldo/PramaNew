using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prama
{
    class clsArticulosCoeficientes
    {
        #region Propiedades de la clase

        public int IdCoeficienteArticulo { set; get; }
        public string CoeficienteArticulo { set; get; }
        public double CoeficientePublico { set; get; }
        public double CoeficienteDistribuidor { set; get; }
        public double CoeficienteRevendedor { set; get; }
        public int Activo { set; get; }

        #endregion

        #region Constructores de la clase

        public clsArticulosCoeficientes()
        {
            IdCoeficienteArticulo =0;
            CoeficienteArticulo ="";
            CoeficientePublico =0;
            CoeficienteDistribuidor =0;
            CoeficienteRevendedor =0;
            Activo = 0;
        }

        public clsArticulosCoeficientes(int IdC, string Coef, double CoefP, double CoefD, double CoefR)
        {
            IdCoeficienteArticulo = IdC;
            CoeficienteArticulo = Coef;
            CoeficientePublico = CoefP;
            CoeficienteDistribuidor = CoefD;
            CoeficienteRevendedor = CoefR;
            Activo = 0;
        }

        #endregion

        #region Método que valida el nuevo Coeficiente
        //METODO QUE VALIDA EL OBJETO Coeficiente (VALOR DE PROPIEDADES CARGADAS). N.
        public string[] cValidaCoeficiente()
        {
            string[] mValida = new string[4];
            int cantError = 0;

            //VALIDAR Coeficiente
            if (string.IsNullOrEmpty(CoeficienteArticulo.ToString()))
            {
                mValida[cantError] = "EL CAMPO COEFICIENTE NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (CoeficienteArticulo == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO COEFICIENTE";
                cantError += 1;
            }
            //VALIDAR Público
            if (string.IsNullOrEmpty(CoeficientePublico.ToString()))
            {
                mValida[cantError] = "EL CAMPO PÚBLICO NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (CoeficientePublico == 0)
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO PÚBLICO";
                cantError += 1;
            }
            //VALIDAR Distribuidor
            if (string.IsNullOrEmpty(CoeficienteDistribuidor.ToString()))
            {
                mValida[cantError] = "EL CAMPO DISTRIBUIDOR NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (CoeficienteDistribuidor == 0)
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO DISTRIBUIDOR";
                cantError += 1;
            }
            //VALIDAR Revendedor
            if (string.IsNullOrEmpty(CoeficienteRevendedor.ToString()))
            {
                mValida[cantError] = "EL CAMPO REVENDEDOR NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (CoeficienteRevendedor == 0)
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO REVENDEDOR";
                cantError += 1;
            }
            return mValida;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prama
{
    class clsArticulosRubros
    {

        #region Propiedades de la clase

        public int IdRubroArticulo { set; get; }
        public string RubroArticulo { set; get; }
        public int Orden { set; get; }

        #endregion

        #region Métodos constructores

        public clsArticulosRubros()
        {
            IdRubroArticulo = 0;
            RubroArticulo = "";
            Orden = 0;

        }

        public clsArticulosRubros(int Id, string Rub, int p_Orden)
        {
            IdRubroArticulo = Id;
            RubroArticulo = Rub;
            Orden = p_Orden;
        }

        #endregion

        #region Método que valida el nuevo Rubro
        //METODO QUE VALIDA EL OBJETO (VALOR DE PROPIEDADES CARGADAS). N.
        public string[] cValidaRubro()
        {
            string[] mValida = new string[1];
            int cantError = 0;

            //VALIDAR Localidad
            if (string.IsNullOrEmpty(RubroArticulo.ToString()))
            {
                mValida[cantError] = "EL CAMPO RUBRO NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (RubroArticulo == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO RUBRO";
                cantError += 1;
            }
            
            return mValida;
        }
        #endregion
    }
}

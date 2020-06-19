using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prama.Clases
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    namespace Prama
    {
        class clsTransporte
        {
            #region Propiedades de la clase

            public int IdTransporte { set; get; }
            public string RazoSocial { set; get; }
            public int Activo { set; get; }

            #endregion

            #region Constructores de la clase

            public clsTransporte()
            {
                IdTransporte = 0;
                RazoSocial = "";
                Activo = 0;
            }

            public clsTransporte(int IdA, string Are, int Act)
            {
                IdTransporte = IdA;
                RazoSocial = Are;
                Activo = Act;
            }

            #endregion

            #region Método que valida la nuevo Transporte
            //METODO QUE VALIDA EL OBJETO (VALOR DE PROPIEDADES CARGADAS). N.
            public string[] cValidaTransp()
            {
                string[] mValida = new string[1];
                int cantError = 0;

                //VALIDAR Localidad
                if (string.IsNullOrEmpty(RazoSocial.ToString()))
                {
                    mValida[cantError] = "EL CAMPO RAZON SOCIAL NO PUEDE ESTAR VACIO!";
                    cantError += 1;
                }
                else if (RazoSocial == " ")
                {
                    mValida[cantError] = "DEBE COMPLETAR EL CAMPO RAZON SOCIAL";
                    cantError += 1;
                }

                return mValida;
            }
            #endregion
        }
    }

}

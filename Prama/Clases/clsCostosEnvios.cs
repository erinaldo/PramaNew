using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prama.Clases
{

    class clsCostosEnvios
    {

        #region Propiedades de la clase

        public int IdCostoEnvio { set; get; }
        public string Descripcion { set; get; }
        public double Sucursal { set; get; }
        public double Domicilio { set; get; }
        public int Activo { set; get; }

        #endregion

        #region Método que valida el nuevo Coeficiente
        //METODO QUE VALIDA EL OBJETO Coeficiente (VALOR DE PROPIEDADES CARGADAS). N.
        public string[] cValidaCoeficiente()
        {
            string[] mValida = new string[4];
            int cantError = 0;

            //VALIDAR Coeficiente
            if (string.IsNullOrEmpty(Descripcion.ToString()))
            {
                mValida[cantError] = "EL CAMPO 'DESCRIPCION' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (Descripcion == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO 'DESCRIPCION'";
                cantError += 1;
            }

            //VALIDAR Sucursal
            if (string.IsNullOrEmpty(Sucursal.ToString()))
            {
                mValida[cantError] = "EL CAMPO 'SUCURSAL $' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (Sucursal == 0)
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO 'SUCURSAL $'";
                cantError += 1;
            }


            //VALIDAR Domicilio
            if (string.IsNullOrEmpty(Domicilio.ToString()))
            {
                mValida[cantError] = "EL CAMPO 'DOMICILO $' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (Domicilio == 0)
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO 'DOMICILIO $'";
                cantError += 1;
            }

            return mValida;
        }
        #endregion
    }
}

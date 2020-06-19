using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prama
{
    class clsTiposClientes
    {

        #region Propiedades de la clase

        public int IdTipoCliente { set; get; }
        public string TipoCliente { set; get; }

        #endregion

        #region Constructores de la clase


        public clsTiposClientes()
        {
            IdTipoCliente = 0;
            TipoCliente = "";
        }

        public clsTiposClientes(int IdT, string Tipo)
        {
            IdTipoCliente = IdT;
            TipoCliente = Tipo;
        }

        #endregion

        #region Método que valida la nueva Area de producción
        //METODO QUE VALIDA EL OBJETO (VALOR DE PROPIEDADES CARGADAS). N.
        public string[] cValidaTipo()
        {
            string[] mValida = new string[1];
            int cantError = 0;

            //VALIDAR Localidad
            if (string.IsNullOrEmpty(this.TipoCliente.ToString()))
            {
                mValida[cantError] = "EL CAMPO 'TIPO DE CLIENTE0' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (TipoCliente == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO 'TIPO DE CLIENTE'";
                cantError += 1;
            }

            return mValida;
        }

        #endregion

    }
}

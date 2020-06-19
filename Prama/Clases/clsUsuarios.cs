using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prama
{
    class clsUsuarios
    {
        #region Propiedades de la clase Usuario
        // Creo las propiedades del Usuario. G.
        public int IdUsuario { set; get; }
        public string Usuario { set; get; }
        public string Clave { set; get; }
        public int Nivel { set; get; }
        public bool Activo { set; get; }
        public bool Logged { set; get; }
        #endregion

        #region Métodos constructores de la clase Usuarios
        // Método constructor sin parámetros. G.
        public clsUsuarios()
        {
            IdUsuario = 0;
            Usuario = "";
            Clave = "";
            Nivel = 0;
            Activo = false;
            Logged = false;
        }
        // Método constructor con parámetros. G.
        public clsUsuarios(int i, string u, string c, int n, bool a, byte[] im, bool l)
        {
            IdUsuario = i;
            Usuario = u;
            Clave = c;
            Nivel = n;
            Activo = a;
            Logged = l;
        }
        #endregion

        #region Método que valida el nuevo Usuario
        //METODO QUE VALIDA EL OBJETO AUTOR (VALOR DE PROPIEDADES CARGADAS)
        public string[] cValidaUsuario()
        {
            string[] mValida = new string[3];
            int cantError = 0;
            //VALIDAR CODIGO USUARIO
            if (string.IsNullOrEmpty(Usuario.ToString()))
            {
                mValida[cantError] = "EL CAMPO USUARIO NO PUEDE ESTAR VACIO!";
                cantError++;
            }
            else if (Usuario == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO USUARIO";
                cantError++;
            }
            //VALIDAR CLAVE
            if (string.IsNullOrEmpty(Clave.ToString()))
            {
                mValida[cantError] = "EL CAMPO CLAVE NO PUEDE ESTAR VACIO!";
                cantError++;
            }
            else if (Clave.ToString() == "")
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO CLAVE!";
                cantError++;
            }
            //VALIDA NIVEL
            if (string.IsNullOrEmpty(Nivel.ToString()))
            {
                mValida[cantError] = "EL CAMPO NIVEL NO PUEDE ESTAR VACIO!";
                cantError++;
            }
            else if (Nivel == 0)
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO NIVEL!";
                cantError++;
            }
            return mValida;
        }
        #endregion

        #region Metodo: UpdateUserLogin

       /************************************************/
       /* Metodo    : UpdateUserLogin
        * Proposito : guarda estado de login para el 
        *             usuario actual.
        * Fecha     : 2016-11-22
        * Autor     : Developer
        * Retorna   : Nada
        **********************************************/
        public void UpdateUserLogin(int p_state)
        {
            string CadSQL = "";

            CadSQL = "UPDATE Usuarios SET Logged = " + p_state + " WHERE IdUsuario = " + this.IdUsuario;

            clsDataBD.GetSql(CadSQL);
        }

        #endregion

        #region Metodo: ControlLoginUserOff

        /************************************************/
        /* Metodo    : ControlLoginUserOff
         * Proposito : Remueve la marca de logueo a todos
         *             los usuarios
         * Fecha     : 2016-11-22
         * Autor     : Developer
         * Retorna   : Nada
         **********************************************/

        public void ControlLoginUserOff()
        {
            string CadSQL = "";

            CadSQL = "UPDATE Usuarios SET Logged = 0";

            clsDataBD.GetSql(CadSQL);
        }

        #endregion
    }
}

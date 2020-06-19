using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prama.Clases
{
    /// <summary>
    /// Description of CUIT.
    /// </summary>
    public class clsCUIT
    {
        private string _CUIT = string.Empty;
        private bool _Valido = false;

        public clsCUIT()
        {
        }

        public clsCUIT(string CadenaCuit)
        {
            _CUIT = CadenaCuit;
            _Valido = CUITValido();
        }

        public string CUITSinFormato
        {
            get
            {
                return _CUIT;
            }
            set
            {
                _CUIT = value;
                _Valido = CUITValido();
            }
        }

        public string CUITFormateado
        {
            get
            {
                if (!_Valido) return string.Empty;
                if (_CUIT.Length == 0) return string.Empty;
                return _CUIT.Substring(0, 2) + "-" +
                       _CUIT.Substring(2, 8) + "-" +
                       _CUIT.Substring(10);
            }
        }

        public bool EsValido
        {
            get
            {
                return _Valido;
            }
        }

        private bool CUITValido()
        {
            if (_CUIT.Length == 0) return true;
            string CUITValidado = string.Empty;
            bool Valido = false;
            char Ch;
            for (int i = 0; i < _CUIT.Length; i++)
            {
                Ch = _CUIT[i];
                if ((Ch > 47) && (Ch < 58))
                {
                    CUITValidado = CUITValidado + Ch;
                }
            }

            _CUIT = CUITValidado;
            Valido = (_CUIT.Length == 11);
            if (Valido)
            {
                int Verificador = EncontrarVerificador(_CUIT);
                Valido = (_CUIT[10].ToString() == Verificador.ToString());
            }

            return Valido;
        }

        private int EncontrarVerificador(string CUIT)
        {
            int Sumador = 0;
            int Producto = 0;
            int Coeficiente = 0;
            int Resta = 5;
            for (int i = 0; i < 10; i++)
            {
                if (i == 4) Resta = 11;
                Producto = CUIT[i];
                Producto -= 48;
                Coeficiente = Resta - i;
                Producto = Producto * Coeficiente;
                Sumador = Sumador + Producto;
            }

            int Resultado = Sumador - (11 * (Sumador / 11));
            Resultado = 11 - Resultado;

            if (Resultado == 11) return 0;
            else return Resultado;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prama
{
    class clsArticulosActualizacion
    {
        #region Propiedades de la clase

        public int IdArticuloActualizacion { set ; get ; }
        public string Fecha { set ; get ; }
        public int IdRubroArticulo { set ; get ; }
        public string RubroArticulo { set ; get ; }
        public int IdSubrubroArticulo { set ; get ; }
        public string SubrubroArticulo { set ; get ; }
        public double Aumento { set ; get ; }
        public double Descuento { set ; get ; }
        public int IdUsuario { set ; get ; }
        public string Usuario { set ; get ; }
        public int Activo { set; get; }

        #endregion

        #region Métodos constructores de la clase

        public clsArticulosActualizacion()
        {
            IdArticuloActualizacion = 0;
            Fecha = "";
            IdRubroArticulo = 0;
            RubroArticulo = "";
            IdSubrubroArticulo = 0;
            SubrubroArticulo = "";
            Aumento = 0;
            Descuento = 0;
            IdUsuario = 0;
            Usuario = "";
            Activo = 0;

        }

        #endregion
    }
}

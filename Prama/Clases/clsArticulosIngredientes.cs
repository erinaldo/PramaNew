using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prama
{
    class clsArticulosIngredientes
    {

        public int IdIngrediente { get; set; }
        public int IdArticulo { get; set; }
        public double Costo { get; set; }

        #region Constructores
        
        public clsArticulosIngredientes()
        {

        }

        public clsArticulosIngredientes(int p_IdIngrediente, int p_IdArticulo, double p_Costo)
        {
            this.IdArticulo = p_IdArticulo;
            this.IdIngrediente = p_IdIngrediente;
            this.Costo = p_Costo;
        }


        #endregion
    }
}

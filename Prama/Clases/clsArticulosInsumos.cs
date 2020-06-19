using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prama
{
    class clsArticulosInsumos
    {

        public int IdInsumo { get; set; }
        public int IdArticulo { get; set; }
        public double Costo { get; set; }

        #region Constructores
        
        public clsArticulosInsumos()
        {

        }

        public clsArticulosInsumos(int p_IdInsumo, int p_IdArticulo, double p_Costo)
        {
            this.IdArticulo = p_IdArticulo;
            this.IdInsumo = p_IdInsumo;
            this.Costo = p_Costo;
        }


        #endregion

    }
}

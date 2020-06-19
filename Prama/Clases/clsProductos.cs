using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prama
{
    class clsProductos
    {
        public int IdProducto { get; set; }
        public int IdArticulo { get; set; }
        public double CostoAcumulado { get; set; }
        public double CostoInsumos { get; set; }
        public double CostoGastos { get; set; }
        public int IdAreaProduccion { get; set; }
        public int IdCoeficienteArticulo { get; set; }
        public int Tanda { get; set; }


        public clsProductos()
        { }


        public clsProductos(int p_IdProducto, int p_IdArticulo, double p_CostoAcum, double p_CostoIns, double p_CostoGas, int p_IdArea, int p_IdCoef, int p_Tanda)
        {
            this.IdProducto = p_IdProducto;
            this.IdArticulo = p_IdArticulo;
            this.CostoAcumulado = p_CostoAcum;
            this.CostoGastos = p_CostoGas;
            this.CostoInsumos = p_CostoIns;
            this.IdAreaProduccion = p_IdArea;
            this.IdCoeficienteArticulo = p_IdCoef;
            this.Tanda = p_Tanda; 
        }


    }
}

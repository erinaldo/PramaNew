using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prama.Clases
{
    class clsArticulosFaltantes
    {
        public int IdArticuloFaltante { set; get; }
        public string CodigoArticulo { set; get; }
        public string Articulo { set; get; }
        public string Unidades { set; get; }
        public double Stock { set; get; }
        public double StockPuntoPedido { set; get; }
        public double Faltante { set; get; }
        public double Fabricar { set; get; }
        public int Activo { set; get; }
        public int IdInsumo { set; get; }
        public int IdProducto { set; get; }
        public string Fecha { set; get; }
        public DateTime FechaReal { set; get; }
        public int dia { set; get; }
        public int semana { set; get; }

        public clsArticulosFaltantes()
        {

        }

    }
}

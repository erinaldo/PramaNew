using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prama.Clases
{
    class clsRecibosDetalle
    {
        public int IdDetalleRecibo { get; set; }
        public int IdRecibo { get; set; }
        public DateTime Fecha { get; set; }
        public int Punto {get;set;}
        public int Nro {get;set;}
        public int IdTipComprobante { get; set; }
        public string Comprobante {get;set;}
        public double Importe { get; set; }

        //Constructor vacìo
        public clsRecibosDetalle()
        {
     
        }
    }
}

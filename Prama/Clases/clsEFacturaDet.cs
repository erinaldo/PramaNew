using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prama.Clases
{
    class clsEFacturaDet
    {

        public int IdFacturaDetalle {get;set;}
        public int IdFactura {get;set;}
        public double Cantidad {get;set;}
        public int IdArticulo { get; set; }
        public int IdProducto {get;set;}
        public double Precio {get;set;}
        public double Dto { get; set; }
        public double SubTotalDto { get; set; }
        public double Alicuota { get; set; }
        public double IVA {get;set;}
        public double Subtotal { get; set; }


        public clsEFacturaDet()
        {

        }

    }
}

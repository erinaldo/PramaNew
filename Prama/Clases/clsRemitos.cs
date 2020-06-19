using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prama.Clases
{
    class clsRemitos
    {

        public int IdRemito {get;set;}
        public int Punto {get;set;}
        public int Nro {get;set;}
        public string NroRemito {get;set;}
        public int IdCliente {get;set;}
        public string CUIT {get;set;}
        public int IdTransporte {get;set;}
        public string Comprobante {get;set;}
        public int IdFormaPago {get;set;}
        public int IdFormaPagoMerc { get; set; }
        public int Bultos {get;set;}
        public double Seguro {get;set;}
        public int Anulado {get;set;}
        public int Activo { get; set; }

        //Constructor vacìo
        public clsRemitos()
        {
 
        }
    }
}

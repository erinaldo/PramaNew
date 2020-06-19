using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prama.Clases
{
    class clsRecibos
    {
        public int IdRecibo { get; set; }
        public DateTime Fecha { get; set; }
        public int Punto {get;set;}
        public int Nro {get;set;}
        public string PuntoNro {get;set;}
        public int IdCliente {get;set;}
        public double Total { get; set; }
        public int Activo { get; set; }

        //Constructor vacìo
        public clsRecibos()
        {
 
        }

    }
}

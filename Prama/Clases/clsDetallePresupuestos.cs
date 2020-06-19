using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prama.Clases
{
    class clsDetallePresupuestos
    {

        public int IdDetPresupuesto {get;set;}
        public int IdPresupuesto {get;set;}
        public int IdArticulo { get; set; }
        public string Codigo_Articulo { get; set; }
        public double Cantidad {get;set;}
        public string Descripcion {get;set;}
        public double PrecioUnitario {get;set;}
        public int Activo { get; set; }
        public int Excel  { get; set; }
        public int Orden { get; set; }

        public clsDetallePresupuestos()
        {

        }
    }

}

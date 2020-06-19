using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prama.Clases
{
    class clsPresupuestos
    {

        public int IdPresupuesto { get; set; }
        public int IdCliente { get; set; }
        public int IdFormaPago { get; set; }
        public int Punto { get; set; }
        public int Nro { get; set; }
        public string PuntoNro { get; set; }
        public int IdTransporte { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentario { get; set; }
        public double Dto { get; set; }
        public double Flete { get; set; }
        public int Activo { get; set; }
        public int Facturado { get; set; }
        public int Excel { get; set; }
        public int Pendiente { get; set; }
        public string Codigo_Correo { get; set; }
        public int IdUsuario { set; get; }

        public clsPresupuestos()
        {

        }
    }
}

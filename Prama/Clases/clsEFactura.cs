using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prama.Clases
{
    class clsEFactura
    {
        public int IdFactura {get;set;}
        public DateTime Fecha {get;set;}
        public int IdTipoComprobante {get;set;}
        public int Punto {get;set;}
        public int Nro {get;set;}
        public string PuntoNro { get; set; }
        public string Comprobante {get;set;}
        public int IdFormaPago {get;set;}
        public DateTime FechaVencPago {get;set;}
        public int IdCliente {get;set;}
        public string CUIT {get;set;}
        public int IncluyeProd {get;set;}
        public int IncluyeServ {get;set;}
        public string CAE {get;set;}
        public DateTime VecCAE {get;set;}
        public int Resultado {get;set;}
        public double OtrosTributos {get;set;}
        public double IVA21 {get;set;}
        public double IVA10 { get; set; }
        public double Total {get;set;}
        public double Neto { get; set; }
        public double NetoIvaVta { get; set; }
        public double Subtotal { get; set; }
        public double Exento { get; set; }
        public double Dto { get; set; }
        public double Flete { get; set; }
        public int IdTransporte { get; set; }
        public double Saldo { get; set; }
        public string Codigo_Correo { get; set; }
        public string PuntoNrOrig { get; set; }
        public int IdMotivo { get; set; }

        public clsEFactura()
        { }

    }
}

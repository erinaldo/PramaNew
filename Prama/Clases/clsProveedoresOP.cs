using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prama.Clases
{
    class clsProveedoresOP
    {
        #region Propiedades de la clase

        public int IdOrdenPago { set; get; }
        public int IdProveedor { set; get; }
        public string Fecha { set; get; }
        public DateTime FechaReal { set; get; }
        public string Numero { set; get; }
        public double Efectivo { set; get; }
        public double Transferencia { set; get; }
        public double ChequesPropios { set; get; }
        public double ChequesTerceros { set; get; }
        public double Total { set; get; }
        public int Activo { set; get; }
        public string Usuario { set; get; }

        #endregion

        #region Constructor

        public clsProveedoresOP()
        {

        }
        
        #endregion
    }
}

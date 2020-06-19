using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Prama.Clases;

//*************************************************************
//Clase     : clsParametros
//Fecha     : 2016-11-22
//Autor     : Ignacio
//Proposito : almacenar en memoria la configuraciòn de la app.
//Metodos   : ObtenerParametros y UpdateParametros
//************************************************************
namespace Prama
{
    class clsParametros
    {

        #region Propiedades

           public int IdParametro {get;set;}
           public string RazonSocial {get;set;}
           public string NombreFantasia {get;set;}
           public string Direccion {get;set;}
           public string Telefono {get;set;}
           public string Mail {get;set;}
           public string Web {get;set;}
           public string CUIT {get;set;}
           public string Localidad {get;set;}
           public string CondicionIva {get;set;}
           public double Iva {get;set;}
           public string Fondo {get;set;}
           public string Icono {get;set;}
           public string Splash {get;set;}
           public bool Impresion {get;set;}
           public int Presupuestos {get;set;}
           public int Facturas {get;set;}
           public int Remitos {get;set;}
           public int Recibos {get;set;}
           public int Pedidos {get;set;}
           public int CaducidadPresupuestos {get;set;}
           public int CaducidadPedidos {get;set;}
           public bool Imprimir {get;set;}
           public int PtoVtaPorDefecto {get;set;}
           public int AlmacenPorDefecto { get; set; }
           public int ControlLoginOff { get; set; }
           public int IconInTaskBar { get; set; }
           public int AutoLoad { get; set; }
           public int ModoFactura { get; set; } //0 Modo Testing 1 Modo Produccion
           public int CantMinRev { get; set; }
           public int UltIns { get; set; } //Ultimo Nro Insumo Codif. Automatica
           public int UltProd { get; set; } //Ultimo Nro Producto Codif. Automatica
           public int NivelBaja { get; set; } //Nivel Baja 
           public int NivelStock { get; set; } //Nivel para modificar stock
           public int NivelFact { get; set; } //Nivel para modificar stock
           public int IdModPresu { get; set; } //Nivel para modificar presupuestos concurrencia  

           public double PorcLimitCdba { get; set; }
           public double PorcLimitCbaLimit { get; set; }

           public int Block { get; set; }

         #endregion

        #region Constructor sin parametros
           //Constructor sin parametros
           public clsParametros()
           {

           }

        #endregion

        #region Metodos

        #region Metodo: ObtenerParametros

        /*******************************/
       //Metodo    : ObtenerParametros
       //Fecha     : 2016-11-22
       //Proposito : Obtiene configuracion
       //Retorna   : Nada
       /******************************/
        public void ObtenerParametros()
        {
            string CadSQL = "Select * from Parametros where IdParametro = " + clsGlobales.gParametro;

            DataTable myDataTable = new DataTable();

            myDataTable = clsDataBD.GetSql(CadSQL);

            foreach (DataRow row in myDataTable.Rows)
            {
                   this.IdParametro = Convert.ToInt32(row["IdParametro"].ToString());
                   this.RazonSocial = row["RazonSocial"].ToString();
                   this.NombreFantasia = row["NombreFantasia"].ToString();
                   this.Direccion = row["Direccion"].ToString();
                   this.Telefono = row["Telefono"].ToString();
                   this.Mail = row["Mail"].ToString();
                   this.CUIT = row["CUIT"].ToString();
                   this.Localidad = row["Localidad"].ToString();
                   this.Web = row["Web"].ToString();
                   this.CondicionIva = row["CondicionIva"].ToString();
                   this.Iva = Convert.ToDouble(row["Iva"].ToString());
                   this.Imprimir = Convert.ToBoolean(row["Imprimir"].ToString());
                   this.Presupuestos = (int)row["Presupuestos"];
                   this.Facturas = (int)row["Facturas"];
                   this.Recibos = (int)row["Recibos"];
                   this.Remitos = (int)row["Remitos"];
                   this.CaducidadPresupuestos = (int)row["CaducidadPresupuestos"];
                   this.CaducidadPedidos = (int)row["CaducidadPedidos"];
                   this.PtoVtaPorDefecto = (int)row["PtoVtaPorDefecto"];
                   this.AlmacenPorDefecto = (int)row["AlmacenPorDefecto"];
                   this.ControlLoginOff = (int)row["ControlLoginOff"];
                   this.IconInTaskBar = (int)row["IconInTaskBar"];
                   this.AutoLoad = (int)row["AutoLoad"];
                   this.CantMinRev = (int)row["CantMinRev"];
                   this.UltIns = (int)row["UltInst"]; 
                   this.UltProd = (int)row["UltProd"];
                   this.NivelBaja = (int)row["NivelBaja"];
                   this.NivelStock = (int)row["NivelStock"];
                   this.NivelFact = (int)row["NivelFact"];
                   this.PorcLimitCdba = Convert.ToDouble(row["PorcLimitCba"].ToString());
                   this.PorcLimitCbaLimit = Convert.ToDouble(row["PorcLimitCbaLimit"].ToString());
                   this.Block = (int)row["Block"];
               
                   if (Convert.ToBoolean(row["ModoFactura"])==false)
                   {
                       this.ModoFactura = 0;
                       clsGlobales.CertificadoAFIP = "certificado.pfx";
                   }
                   else
                   {
                       this.ModoFactura = 1;
                       clsGlobales.CertificadoAFIP = "crtPMA.pfx";
                   }
            }

        }

        #endregion

        #region Metodo: UpdateParametros
        /**********************************/
        //Metodo    : UpdateParametros
        //Proposito : Guarda la configuracion
        //Fecha     : 2016-11-22
        //Autor     : N.
        //Retorna   : Nada
        //********************************/
        public void UpdateParametros()
        {
            try
            {
                string CadSQL = "UPDATE Parametros SET IdParametro = " + clsGlobales.gParametro + "," +
                                                  "RazonSocial='" + this.RazonSocial + "'," +
                                                  "NombreFantasia='" + this.NombreFantasia + "'," +
                                                  "Direccion='" + this.Direccion + "'," +
                                                  "Telefono='" + this.Telefono + "'," +
                                                  "Mail='" + this.Mail + "'," +
                                                  "CUIT='" + this.CUIT + "'," +
                                                  "CondicionIva='" + this.CondicionIva + "'," +
                                                  "Iva=" + this.Iva.ToString().Replace(",", ".") + "," +
                                                  "Imprimir=" + Convert.ToInt32(this.Imprimir) + "," +
                                                  "Presupuestos=" + this.Presupuestos + "," +
                                                  "Facturas=" + this.Facturas + "," +
                                                  "Recibos=" + this.Recibos + "," +
                                                  "Remitos=" + this.Remitos + "," +
                                                  "CaducidadPresupuestos=" + this.CaducidadPresupuestos + "," +
                                                  "CaducidadPedidos=" + this.CaducidadPedidos + "," +
                                                  "PtoVtaPorDefecto=" + this.PtoVtaPorDefecto + "," +
                                                  "AlmacenPorDefecto=" + this.AlmacenPorDefecto + "," +
                                                  "ControlLoginOff=" + this.ControlLoginOff + "," +
                                                  "IconInTaskBar=" + this.IconInTaskBar + "," +
                                                  "AutoLoad=" + this.AutoLoad + "," +
                                                  "ModoFactura=" + this.ModoFactura + "," +
                                                  "CantMinRev=" + this.CantMinRev + "," +
                                                  "UltInst=" + this.UltIns + "," +
                                                  "UltProd=" + this.UltProd + "," +
                                                  "NivelBaja=" + this.NivelBaja + "," +
                                                  "NivelStock=" + this.NivelStock + "," +
                                                  "NivelFact=" + this.NivelFact + "," +
                                                  "PorcLimitCba=" + this.PorcLimitCdba + "," +
                                                  "PorcLimitCbaLimit=" + this.PorcLimitCbaLimit + "," +
                                                  "Block=" + this.Block;


            clsDataBD.GetSql(CadSQL);

          }  
          catch (Exception e)
          {

             MessageBox.Show(e.Message,"Error!",MessageBoxButtons.OK,MessageBoxIcon.Error);
             return;
          }          
         
        }

        #endregion

        #region Método que valida los nuevos parametros
        
        public string[] cValidaParametros()
        {
            string[] mValida = new string[32];
            int cantError = 0;

            //VALIDAR Razon Social
            if (string.IsNullOrEmpty(this.RazonSocial))
            {
                mValida[cantError] = "EL CAMPO 'RAZON SOCIAL' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.RazonSocial == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO 'RAZON SOCIAL'";
                cantError += 1;
            }

            //VALIDAR Nombre Fantasia
            if (string.IsNullOrEmpty(this.NombreFantasia))
            {
                mValida[cantError] = "EL CAMPO 'NOMBRE FANTASIA' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.NombreFantasia == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR EL CAMPO 'NOMBRE FANTASIA'";
                cantError += 1;
            }

            //VALIDAR Direccion
            if (string.IsNullOrEmpty(this.Direccion))
            {
                mValida[cantError] = "EL CAMPO 'DIRECCION' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.Direccion == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR CAMPO 'DIRECCION'!";
                cantError += 1;
            }

            //VALIDAR Telefono
            if (string.IsNullOrEmpty(this.Telefono))
            {
                mValida[cantError] = "EL CAMPO 'TELEFONO' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.Telefono == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR CAMPO 'TELEFONO'!";
                cantError += 1;
            }

            //VALIDAR Mail
            if (string.IsNullOrEmpty(this.Mail))
            {
                mValida[cantError] = "EL CAMPO 'MAIL' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.Mail == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR CAMPO 'MAIL'!";
                cantError += 1;
            }

            //VALIDAR Web
            if (string.IsNullOrEmpty(this.Web))
            {
                mValida[cantError] = "EL CAMPO 'WEB' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.Web == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR CAMPO 'WEB'!";
                cantError += 1;
            }

            //VALIDAR CUIT
            if (string.IsNullOrEmpty(this.CUIT))
            {
                mValida[cantError] = "EL CAMPO 'CUIT' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.CUIT == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR CAMPO 'CUIT'!";
                cantError += 1;
            }
            else
            {
                //Validar la CUIT                
                clsCUIT oCUITvalido = new clsCUIT(this.CUIT);

                if (!(oCUITvalido.EsValido))
                {
                    mValida[cantError] = "EL CUIT INGRESADO ES INCORRECTO. VERIFIQUE!";
                    cantError += 1;
                }
            }


            //VALIDAR Localidad
            if (string.IsNullOrEmpty(this.Localidad))
            {
                mValida[cantError] = "EL CAMPO 'LOCALIDAD' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.Localidad == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR CAMPO 'LOCALIDAD'!";
                cantError += 1;
            }


            //VALIDAR Condicion IVA
            if (string.IsNullOrEmpty(this.CondicionIva))
            {
                mValida[cantError] = "EL CAMPO 'CONDICION IVA' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.CondicionIva == " ")
            {
                mValida[cantError] = "DEBE COMPLETAR CAMPO 'CONDICION IVA'!";
                cantError += 1;
            }

            //VALIDAR % IVA
            if (string.IsNullOrEmpty(this.Iva.ToString()))
            {
                mValida[cantError] = "EL CAMPO '% IVA' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.Iva > 100.00)
            {
                mValida[cantError] = "EL CAMPO '% IVA' NO PUEDE SER MAYOR A 100%";
                cantError += 1;
            }
            else if (this.Iva == 0)
            {
                mValida[cantError] = "EL CAMPO '% IVA' DEBER SER MAYOR A CERO!";
                cantError += 1;
            }


            //VALIDAR % PROV LIMIT CDBA
            if (string.IsNullOrEmpty(this.PorcLimitCdba.ToString()))
            {
                mValida[cantError] = "EL CAMPO '% A APLICAR EN LISTA PROV. LIMITROFES CDBA' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.PorcLimitCdba > 100.00)
            {
                mValida[cantError] = "EL CAMPO '% A APLICAR EN LISTA PROV. LIMITROFES CDBA' NO PUEDE SER MAYOR A 100%";
                cantError += 1;
            }
            else if (this.PorcLimitCdba == 0)
            {
                mValida[cantError] = "EL CAMPO '% A APLICAR EN LISTA PROV. LIMITROFES CDBA' DEBER SER MAYOR A CERO!";
                cantError += 1;
            }



            //VALIDAR % PROV LIMIT CDBA LIMIT
            if (string.IsNullOrEmpty(this.PorcLimitCbaLimit.ToString()))
            {
                mValida[cantError] = "EL CAMPO '% A APLICAR EN LISTA PROV. LIMITROFES DE LIMITROFES CDBA' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.PorcLimitCbaLimit > 100.00)
            {
                mValida[cantError] = "EL CAMPO'% A APLICAR EN LISTA PROV. LIMITROFES DE LIMITROFES CDBA' NO PUEDE SER MAYOR A 100%";
                cantError += 1;
            }
            else if (this.PorcLimitCbaLimit == 0)
            {
                mValida[cantError] = "EL CAMPO '% A APLICAR EN LISTA PROV. LIMITROFES DE LIMITROFES CDBA' DEBER SER MAYOR A CERO!";
                cantError += 1;
            }

            //VALIDAR CADUCIDAD PRESUPUESTOS
            if (string.IsNullOrEmpty(this.CaducidadPresupuestos.ToString()))
            {
                mValida[cantError] = "EL CAMPO 'MESES PRESUPUESTOS PENDIENTES' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.CaducidadPresupuestos > 99)
            {
                mValida[cantError] = "EL CAMPO 'MESES PRESUPUESTOS PENDIENTES' NO PUEDE SER MAYOR A 99";
                cantError += 1;
            }
            else if (this.CaducidadPresupuestos == 0)
            {
                mValida[cantError] = "EL CAMPO 'MESES PRESUPUESTOS PENDIENTES' DEBE SER MAYOR A 0!";
                cantError += 1;
            }

            //VALIDAR CADUCIDAD PRESUPUESTOS
            if (string.IsNullOrEmpty(this.CaducidadPedidos.ToString()))
            {
                mValida[cantError] = "EL CAMPO 'MESES PEDIDOS PENDIENTES' NO PUEDE ESTAR VACIO!";
                cantError += 1;
            }
            else if (this.CaducidadPedidos > 99)
            {
                mValida[cantError] = "EL CAMPO 'MESES PEDIDOS PENDIENTES' NO PUEDE SER MAYOR A 99";
                cantError += 1;
            }
            else if (this.CaducidadPedidos == 0)
            {
                mValida[cantError] = "EL CAMPO 'MESES PEDIDOS PENDIENTES' DEBE SER MAYOR A 0!";
                cantError += 1;
            }
            
            //RETORNAR VECTOR
            return mValida;

        }


        #endregion

        #endregion

    }
}

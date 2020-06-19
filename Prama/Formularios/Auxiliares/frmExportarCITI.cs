using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Prama.Clases;

namespace Prama.Formularios.Auxiliares
{
    public partial class frmExportarCITI : Form
    {
        public frmExportarCITI()
        {
            InitializeComponent();
        }

        private void rbtEspecifica_CheckedChanged(object sender, EventArgs e)
        {
            this.nMes.TabStop = true;
            this.nMes.Enabled = true;
            this.nAno.TabStop = true;
            this.nAno.Enabled = true;
        }

        private void rbtMesCurso_CheckedChanged(object sender, EventArgs e)
        {
            this.nMes.TabStop = false;
            this.nMes.Enabled = false;
            this.nAno.TabStop = false;
            this.nAno.Enabled = false;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            string FileName = "";
            string FileNameAlic = "";
            int Mes = 0;
            int Ano = 0;
            
            //Verificar compras o ventas...
            if (this.rbtVentas.Checked)
            {
                //Nombre Archivo
                if (this.rbtMesCurso.Checked)
                {
                    FileName = "CITI VENTAS CBTES -" + DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + ".txt";
                    FileNameAlic = "CITI VENTAS ALIC -" + DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + ".txt";
                    Mes = Convert.ToInt32(DateTime.Today.Month);
                    Ano = Convert.ToInt32(DateTime.Today.Year);
                }
                else
                {
                    FileName = "CITI VENTAS CBTES -" + this.nAno.Value.ToString() + "-" + this.nMes.Value.ToString() + ".txt";
                    FileNameAlic = "CITI VENTAS ALIC -" + this.nAno.Value.ToString() + "-" + this.nMes.Value.ToString() + ".txt";
                    Mes = Convert.ToInt32(this.nMes.Value);
                    Ano = Convert.ToInt32(this.nAno.Value);
                }


                //Hay DATOS?
                string sCadSQL = "Select * from Vista_eFactura WHERE MONTH(Fecha) = " + Mes + " AND YEAR(Fecha) = " + Ano + " ORDER BY Fecha ASC";
                DataTable myDataTable = clsDataBD.GetSql(sCadSQL);
                if (!(myDataTable.Rows.Count > 0))                
                {
                    //Mensaje Final
                    MessageBoxTemporal.Show("No hay datos a exportar para el Período indicado!", "Información!", 4, true);
                }
                else
                {

                    //Elegir carpeta destino
                    if (FolderBrowser.ShowDialog() == DialogResult.OK)
                    {
                        lblDest.Text = FolderBrowser.SelectedPath;
                    }

                    //Generar archivo Comprobantes
                    MakeVentas(FileName, Mes, Ano);

                    //Mensaje Final
                    MessageBoxTemporal.Show("El Archivo CITI Ventas Comprobantes ha sido generado exitosamente!", "Información!", 4, true);
                    
                    //Clean
                    lblPorc.Text = "0 %";
                    pBar.Value = 0;

                    //Generar archivo Alicuotas
                    MakeVentasAlic(FileNameAlic, Mes, Ano);

                    //Mensaje Final
                    MessageBoxTemporal.Show("El Archivo CITI Ventas Alicuotas ha sido generado exitosamente!", "Información!", 4, true);

                }

                //Clean
                    lblDest.Text = "Sin especificar...";
                    lblPorc.Text = "0 %";
                    pBar.Value = 0;

            }
            else if (this.rbtCompras.Checked)
            {
                //VER GABI

                //MakeCompras();
            }

        }

        #region Metodo: MakeVentas

        private void MakeVentas(string p_FileName, int p_Mes, int p_Year)
        {
            string sCadSQL = "Select * from Vista_eFactura WHERE MONTH(Fecha) = " + p_Mes + " AND YEAR(Fecha) = " + p_Year  + " ORDER BY Fecha ASC";
            DataTable myDataTable = clsDataBD.GetSql(sCadSQL);

            string myCad = ""; //Cadena que almacena los registros
            int Total = myDataTable.Rows.Count;
            int Item = 1;
            double Porcentaje = 0;

            //Recorrer DataTable de Comprobantes
            foreach (DataRow myRow in myDataTable.Rows)
            {
                //Armar Cadena...
                myCad += Convert.ToDateTime(myRow["Fecha"]).ToString("yyyyMMdd");
                myCad += Convert.ToInt32(myRow["IdTipoComprobante"]).ToString("D3");
                myCad += Convert.ToInt32(myRow["Punto"]).ToString("D5");
                myCad += Convert.ToInt32(myRow["Nro"]).ToString("D20");
                myCad += Convert.ToInt32(myRow["Nro"]).ToString("D20");
                myCad += Convert.ToInt32("80").ToString("D2");
                myCad += myRow["CUIT"].ToString().PadLeft(20,'0');

                //Razon Social
                if (myRow["RazonSocial"].ToString().Length > 30)
                {
                   myCad += clsGlobales.Left(myRow["RazonSocial"].ToString(), 30).PadRight(30, ' ');
                }
                else
                {
                   myCad += myRow["RazonSocial"].ToString().PadRight(30, ' '); 
                }

                //Importe Total
                string[] myArr = myRow["Total"].ToString().Split('.');
                string Total_Entero = myArr[0];
                string Total_Decimal = myArr[1];
                myCad += (Total_Entero + Total_Decimal).PadLeft(15, '0');
                
                //Importe Total de Conceptos que no integran el precio neto gravado
                myCad += Convert.ToInt32("0").ToString("D15");
                //Percepcion a no categorizados
                myCad += Convert.ToInt32("0").ToString("D15");

                //Importe Operaciones Exentas                
                string[] myArr1 = myRow["Exento"].ToString().Split('.');
                string Ex_Entero = myArr1[0];
                string Ex_Decimal = myArr1[1];
                myCad += (Ex_Entero + Ex_Decimal).PadLeft(15, '0');

                //Pagos a Cuenta
                myCad += Convert.ToInt32("0").ToString("D15");
                //Ingresos Brutos Percepciones
                myCad += Convert.ToInt32("0").ToString("D15");
                //Impuestos municipales
                myCad += Convert.ToInt32("0").ToString("D15");
                //Impuestos internos
                myCad += Convert.ToInt32("0").ToString("D15");
                //Codigo Moneda
                myCad += "PES";
                //Tipo de Cambio
                myCad += "0001000000";
                //Cantidad Alicuotas IVA
                if (Convert.ToDouble(myRow["IVA21"].ToString()) != 0 && Convert.ToDouble(myRow["IVA10"].ToString()) != 0)
                {
                    myCad += Convert.ToInt32("2").ToString();
                }
                else                
                {
                    myCad += Convert.ToInt32("1").ToString();
                }
                //Codigo Operacion
                myCad += Convert.ToInt32("0").ToString();
                //Otros Tributos
                myCad += Convert.ToInt32("0").ToString("D15");
                //Fecha Vencimiento Pago
                myCad += Convert.ToDateTime(myRow["Fecha"]).ToString("yyyyMMdd");

                if (!(Item == Total))
                {
                   //Marca de linea nueva
                   myCad += Environment.NewLine;
                }

                //Mostrar Progreso
                Porcentaje = (Item * 100) / Total;
                this.pBar.Value = Convert.ToInt32(Porcentaje);
                lblPorc.Text = Porcentaje.ToString("#0") + " %"; 
                //Siguiente
                Item++;

            }

            //Guardar en archivo de Texto
            System.IO.StreamWriter file = new System.IO.StreamWriter(lblDest.Text + "\\" + p_FileName);
            file.WriteLine(myCad);

            //Cerrar el archivo
            file.Close();

        }

        #endregion


        #region Metodo: MakeVentasAlic

        private void MakeVentasAlic(string p_FileName, int p_Mes, int p_Year)
        {
            string sCadSQL = "Select * from Vista_eFactura WHERE MONTH(Fecha) = " + p_Mes + " AND YEAR(Fecha) = " + p_Year + " ORDER BY Fecha ASC";
            DataTable myDataTable = clsDataBD.GetSql(sCadSQL);

            string myCad = ""; //Cadena que almacena los registros
            int Total = myDataTable.Rows.Count;
            int Item = 1;
            double Porcentaje = 0;

            //Recorrer DataTable de Comprobantes
            foreach (DataRow myRow in myDataTable.Rows)
            {

                if (!(Convert.ToDouble(myRow["IVA21"]) == 0 && Convert.ToDouble(myRow["IVA10"]) == 0))
                {
                    //Armar Cadena...
                    myCad += Convert.ToInt32(myRow["IdTipoComprobante"]).ToString("D3");
                    myCad += Convert.ToInt32(myRow["Punto"]).ToString("D5");
                    myCad += Convert.ToInt32(myRow["Nro"]).ToString("D20");

                    //Importe Total
                    string[] myArr = myRow["NetoIvaVta"].ToString().Split('.');
                    string NetoIva_Entero = myArr[0];
                    string NetoIva_Decimal = myArr[1];
                    myCad += (NetoIva_Entero + NetoIva_Decimal).PadLeft(15, '0');

                    //Alicuota
                    if (Convert.ToDouble(myRow["IVA21"]) != 0)
                    {
                        myCad += Convert.ToInt32("5").ToString("D4");

                        //Importe Operaciones Exentas                
                        string[] myArrIva21 = myRow["IVA21"].ToString().Split('.');
                        string Iva21_Entero = myArrIva21[0];
                        string Iva21_Decimal = myArrIva21[1];
                        myCad += (Iva21_Entero + Iva21_Decimal).PadLeft(15, '0');

                    }
                    else
                    {
                        myCad += Convert.ToInt32("4").ToString("D4");

                        //Importe Operaciones Exentas                
                        string[] myArrIva10 = myRow["IVA10"].ToString().Split('.');
                        string Iva10_Entero = myArrIva10[0];
                        string Iva10_Decimal = myArrIva10[1];
                        myCad += (Iva10_Entero + Iva10_Decimal).PadLeft(15, '0');

                    }

                    //Es el ultimo?
                    if (!(Item == Total))
                    {
                        //Marca de linea nueva
                        myCad += Environment.NewLine;
                    }
                }

                //Mostrar Progreso
                Porcentaje = (Item * 100) / Total;
                this.pBar.Value = Convert.ToInt32(Porcentaje);
                lblPorc.Text = Porcentaje.ToString("#0") + " %";

                //Siguiente
                Item++;

            }

            //Guardar en archivo de Texto
            System.IO.StreamWriter file = new System.IO.StreamWriter(lblDest.Text + "\\" + p_FileName);
            file.WriteLine(myCad);

            //Cerrar el archivo
            file.Close();

        }

        #endregion

        private void frmExportarCITI_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            //Titulo
            this.Text = clsGlobales.cParametro.NombreFantasia + this.Text;
        }
    }
}

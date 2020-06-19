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
    public partial class frmGeneraFilesActED : Form
    {
        public frmGeneraFilesActED()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Elegir carpeta destino
            if (FolderBrowser.ShowDialog() == DialogResult.OK)
            {
                lblDest.Text = FolderBrowser.SelectedPath;
            }

            //Articulos
            MakeArticulos();

            //Mensaje Final
            MessageBoxTemporal.Show("Los Archivos han sido generados exitosamente!", "Información!", 4, true);

            //Clean
            lblPorc.Text = "0 %";
            pBar.Value = 0;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #region Metodo: MakeArticulos

        private void MakeArticulos()
        {
            string sCadSQL = "Select * from Articulos Order by IdArticulo";
            DataTable myDataTable = clsDataBD.GetSql(sCadSQL);

            string myCad = ""; //Cadena que almacena los registros
            int Total = myDataTable.Rows.Count;
            int Item = 1;
            double Porcentaje = 0;

            //Recorrer DataTable de Comprobantes
            foreach (DataRow myRow in myDataTable.Rows)
            {
                //Armar Cadena...
                //myCad += Convert.ToDateTime(myRow["Fecha"]).ToString("yyyyMMdd");
                myCad += Convert.ToInt32(myRow["IdArticulo"]).ToString("D4");
                myCad += Convert.ToInt32(myRow["IdSubRubroArticulo"]).ToString("D4");
                myCad += Convert.ToInt32(myRow["IdUnidadMedida"]).ToString("D2");

                //Razon Social
                if (myRow["CodigoArticulo"].ToString().Length > 10)
                {
                    myCad += clsGlobales.Left(myRow["CodigoArticulo"].ToString(), 8).PadRight(10, ' ');
                }
                else
                {
                    myCad += myRow["CodigoArticulo"].ToString().PadRight(10, ' ');
                }

                //Articulo
                if (myRow["Articulo"].ToString().Length > 70)
                {
                    myCad += clsGlobales.Left(myRow["Articulo"].ToString(), 70).PadRight(70, ' ');
                }
                else
                {
                    myCad += myRow["Articulo"].ToString().PadRight(70, ' ');
                }

                
                //Unidades
                string[] myArr = myRow["Unidades"].ToString().Split('.');
                string Total_Entero = myArr[0];
                string Total_Decimal = myArr[1];
                myCad += (Total_Entero + Total_Decimal).PadLeft(15, '0');

                //Precio
                string[] myArr1 = myRow["Precio"].ToString().Split('.');
                string TE1 = myArr1[0];
                string TD1 = myArr1[1];
                myCad += (TE1 + TD1).PadLeft(15, '0');

                //PrecioAnterior
                string[] myArr2 = myRow["PrecioAnterior"].ToString().Split('.');
                string TE2 = myArr2[0];
                string TD2 = myArr2[1];
                myCad += (TE2 + TD2).PadLeft(15, '0');

                //UltimoCostoCompra
                string[] myArr3 = myRow["UltimoCostoCompra"].ToString().Split('.');
                string TE3 = myArr3[0];
                string TD3 = myArr3[1];
                myCad += (TE3 + TD3).PadLeft(15, '0');

                //Ultimo Proveedor
                //Razon Social
                if (myRow["UltimoProveedor"].ToString().Length > 70)
                {
                    myCad += clsGlobales.Left(myRow["UltimoProveedor"].ToString(), 70).PadRight(70, ' ');
                }
                else
                {
                    myCad += myRow["UltimoProveedor"].ToString().PadRight(70, ' ');
                }
                
                //Fecha Ult. Compra
                myCad += myRow["UltimaCompra"].ToString().PadRight(10, ' ');

                //Lleva Stock  
                if (Convert.ToBoolean(myRow["LlevaStock"].ToString()) == true)
                {
                    myCad += Convert.ToInt32("1").ToString("D1");
                }
                else
                {
                    myCad += Convert.ToInt32("0").ToString("D1");
                }

                //Facturable
                if (Convert.ToBoolean(myRow["Facturable"].ToString()) == true)
                {
                    myCad += Convert.ToInt32("1").ToString("D1");
                }
                else
                {
                    myCad += Convert.ToInt32("0").ToString("D1");
                }

                //StockMinimo
                string[] myArr4 = myRow["StockMinimo"].ToString().Split('.');
                string TE4 = myArr4[0];
                string TD4 = myArr4[1];
                myCad += (TE4 + TD4).PadLeft(15, '0');

                //StockMaximo
                string[] myArr5 = myRow["StockMaximo"].ToString().Split('.');
                string TE5 = myArr5[0];
                string TD5 = myArr5[1];
                myCad += (TE5 + TD5).PadLeft(15, '0');

                //Stock Punto Pedido
                string[] myArr6 = myRow["StockPuntoPedido"].ToString().Split('.');
                string TE6 = myArr6[0];
                string TD6 = myArr6[1];
                myCad += (TE6 + TD6).PadLeft(15, '0');

                //Porcentaje IVA
                string[] myArr7 = myRow["PorcentajeIVA"].ToString().Split('.');
                string TE7 = myArr7[0];
                string TD7 = myArr[1];
                myCad += (TE7 + TD7).PadLeft(15, '0');

                //Activo
                if (Convert.ToBoolean(myRow["Activo"].ToString()) == true)
                {
                    myCad += Convert.ToInt32("1").ToString("D1");
                }
                else
                {
                    myCad += Convert.ToInt32("0").ToString("D1");
                }
                
                myCad += Convert.ToInt32(myRow["IncListaPre"].ToString()).ToString("D1");
                myCad += Convert.ToInt32(myRow["IncListaRes"].ToString()).ToString("D1");
                myCad += Convert.ToInt32(myRow["chkSombreado"].ToString()).ToString("D1");
                myCad += Convert.ToInt32(myRow["rbtColor"].ToString()).ToString("D1");
                myCad += Convert.ToInt32(myRow["chkSProd"].ToString()).ToString("D1");

                //CompIns
                if (Convert.ToBoolean(myRow["CompIns"].ToString()) == true)
                {
                    myCad += Convert.ToInt32("1").ToString("D1");
                }
                else
                {
                    myCad += Convert.ToInt32("0").ToString("D1");
                }

                //Ultimo?
                if (!(Item == Total))
                {
                    //Marca de linea nueva
                    myCad += Environment.NewLine;
                }

                //Mostrar Progreso
                Porcentaje = (Item * 100) / Total;
                this.pBar.Value = Convert.ToInt32(Porcentaje);
                this.lblPorc.Text = Porcentaje.ToString("#0") + " %";
                this.lblPorc.Refresh();

                //Siguiente
                Item++;

            }

            //Guardar en archivo de Texto
            System.IO.StreamWriter file = new System.IO.StreamWriter(lblDest.Text + "\\Articulos.txt");
            file.WriteLine(myCad);

            //Cerrar el archivo
            file.Close();

        }

        #endregion

        private void frmGeneraFilesActED_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 
            //Titulo
            this.Text = clsGlobales.cParametro.NombreFantasia + this.Text;
        }
    }
}

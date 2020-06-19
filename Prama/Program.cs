using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;

namespace Prama
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //cargo cultura a utilizar
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            
            // Verificar que se este ejecutando una unica instancia
            // de la aplicación. N. 04-10-2015
            string processName = Process.GetCurrentProcess().ProcessName;
            Process[] instances = Process.GetProcessesByName(processName);
            
            

            //Controlar el número de instancias
            if (instances.Length > 1)
            {
                //Mensaje de aviso y fin
                MessageBox.Show("La Aplicación ya está ejecutándose!", "PRAMA S.A.S. S.A.S. v1.0 Release 0 - Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
                return;
            }
            else
            {
                ////DIRECTORIO MIS DOCUMENTOS
                //string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.Windows);

                //if (!(File.Exists(mdoc + "\\PRAMA-CONFIG.txt")))
                //{
                //    MessageBox.Show("Error al iniciar la Aplicación!. Contacte al Administrador del Sistema", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    Application.Exit();
                //}
                //else
                //{
                    ////Leer el archivo 
                    //string sFile = mdoc + "\\PRAMA-CONFIG.txt";
                    //int ConfigPrama = Convert.ToInt32(LeerArchivoPlano(sFile));

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    //Llamada al formulario 'Splash'
                    frmSplash frm = new frmSplash(3);
                    if (frm.ShowDialog() == DialogResult.OK) //Si todo ok...
                    {
                        Application.Run(new frmMenuPrincipal()); //Llamar al frmPrincipal
                    }
                
            }
        }

        #region Método que lee los datos del archivo plano para actualizar el stock

        static string LeerArchivoPlano(string p_FN = "")
        {
            string aux = "";
            string line = "";
            int iCantLineasPlano = 0;

            try
            {
                StreamReader sr = new StreamReader(p_FN);
                line = sr.ReadLine();
                aux = line;

                while (line != null)
                {
                    line = sr.ReadLine();
                    aux = aux + " " + line;
                    iCantLineasPlano++;
                }
            }
            catch
            {
                MessageBox.Show("ERROR al intentar leer el archivo", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("Verifique que el archivo con el movimiento exista", "Información!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return aux;
        }

        #endregion

    }

}

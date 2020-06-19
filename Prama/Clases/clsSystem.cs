using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;
using System.Windows.Forms;

namespace Prama
{
    class clsSystem
    {
        #region Metodo getCalcEnEjecucion()
        /******************************************************
        ' Nombre: getCalcEnEjecucion() 
        ' Finalidad: Verifica si esta corriendo CALC.EXE        
        ' Entradas: Ninguna                
        ' Resultados: .T. si encuentra proceso en memoria                
        ' Si no se encuentra proceso, devuelve .F. 
        '*******************************************************/         
        public bool getCalcEnEjecucion()
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName == "calc" || p.ProcessName == "Calculator")
                {
                  //Ya se está ejecutando
                    return true;
                }
            }
        
          //No se encontró en la lista de procesos activos
            return false;
        }

        #endregion

        #region Metodo que registra que la App inicie con Windows        
        
        public bool RegistrarInicio()
        {
            //Parmetros
                string lbl = Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().
                GetName().CodeBase);
                string path = Path.GetDirectoryName(System.Reflection.Assembly.
                GetExecutingAssembly().GetName().CodeBase).Remove(0, 6) + "\\" + lbl;
                string nombreApp = Application.ProductName;

                RegistryKey rkHKLM = Registry.CurrentUser; 

                RegistryKey rkRun;

                rkRun =rkHKLM.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);

                rkRun.CreateSubKey(nombreApp);

                rkRun.SetValue(nombreApp,path);                

                Registry.CurrentUser.Flush();

                return true;
       
        }

        #endregion

        #region Metodo que anula el registro de la App para que inicie con Windows

        public void AnularInicio()
        { 
            //Parmetros
                string lbl = Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().
                GetName().CodeBase);
                string path = Path.GetDirectoryName(System.Reflection.Assembly.
                GetExecutingAssembly().GetName().CodeBase).Remove(0, 6) + "\\" + lbl;
                string nombreApp = Application.ProductName;
            
                RegistryKey rkHKLM = Registry.CurrentUser; 

                RegistryKey rkRun;

                rkRun =rkHKLM.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);

                rkRun.SetValue(nombreApp, "");

                Registry.CurrentUser.Flush();
        }

        #endregion
    }

}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prama
{
    public partial class frmShowErrores : Form
    {
        #region Variables Publicas

        public string myTitulo = ""; //Para titulo del Formulario 
        public int miserrores = 0; //Cantidad de errores
        public string[] myVector;  //Arreglo

        #endregion

        public frmShowErrores()
        {
            InitializeComponent();
            //Instanciar el vector
            InicializarVector();
        }

        #region Métodos del Formulario

        #region Metodo que Inicializa el Vector de Errores
        //----------------------------------------
        //Metodo    : InicializarVector
        //Proposito : Inicializar el vector para la cantidad de errores detectados
        //Parametros: Nada
        //Retorna   : Nada
        //Autor     : N.
        //----------------------------------------

        //
        private void InicializarVector()
        {
            //Inicializar vector
            myVector = new string[miserrores];
        }

        #endregion

        #region Metodo que cambia el titulo al Formulario de Errores
        //----------------------------------------
        //Metodo    : CargarTitulo
        //Proposito : Cargar Titulo al Formulario
        //Parametros: Nada
        //Retorna   : Nada
        //Autor     : N.
        //----------------------------------------
        public void CargarTitulo()
        {
            this.Text = myTitulo;
        }

        #endregion

        #region Metodo que Carga el Vector de Errores a la Grilla

        //------------------------------------------------------
        //Metodo    : CargarVector
        //Proposito : Carga a la grilla los elementos del vector
        //Retorna   : Nada
        //Autor     : N.
        //------------------------------------------------------
        public void CargarVector()
        {
            for (int i = 0; i < myVector.Length; i++)
            {
                if (!(myVector[i] == null)) //Verificar si el elemento esta null
                {
                    dtGridError.Rows.Add(i + 1, myVector[i]); //agregar a la grilla
                }
            }
        }

        #endregion

        #region CargarToolTips

        private void CargarToolTips()
        {
            toolTip1.SetToolTip(this.btnSalir, "Salir");

        }

        #endregion

        #endregion

        #region Eventos del Formulario

        #region Eventos de los botones

        #region Evento Click del botón Salir

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //Cerrar el formulario
            this.Close();
        }

        #endregion

        #endregion

        #region Evento Load del formulario

        private void frmShowErrores_Load(object sender, EventArgs e)
        {	
		
			//icon
            clsFormato.SetIconForm(this); 
			
            // Cargo los tooltips
            CargarToolTips();

            //Titulo Ventana
            this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;
        }

        #endregion

        #endregion

    }
}

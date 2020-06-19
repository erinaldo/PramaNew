using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Prama.Clases;

namespace Prama.Formularios.Configuracion
{
    public partial class frmPermisos : Form
    {

        public bool bPrimera = false;

        public frmPermisos()
        {
            InitializeComponent();
        }

        private void frmPermisos_Load(object sender, EventArgs e)
        {
			//icon
            clsFormato.SetIconForm(this); 			
            // Llamo al método cargar usuarios para rellenar la grilla. G.
            CargarUsuarios("", "");
            //Tooltips
            setCargarToolTips();
            //Titulo
            this.Text = clsGlobales.cFormato.getTituloVentana() + this.Text;
        }

        #region Método para cargar los usuarios al DataGrid

        private void CargarUsuarios(string Buscar, string Campo)
        {
            // Declaro una variable string para almacenar la cadena de la consulta SQL de ACCESS.
            string myCadena = "";
            if (Buscar == "")
            {
                myCadena = "SELECT * FROM Usuarios Where Activo = 1 order by Usuario";
                DataTable myData = clsDataBD.GetSql(myCadena);
                dgvUsuarios.AutoGenerateColumns = false;

                //Contador
                int filas = 0;

                //Clear
                dgvUsuarios.Rows.Clear();

                //Mostrar Datos
                foreach (DataRow fila in myData.Rows)
                {
                    /*Agregar Fila*/
                    dgvUsuarios.Rows.Add();

                    // Cuento las filas de la grilla
                    filas = dgvUsuarios.Rows.Count;

                    // Si la grilla no está vacía
                    if (filas > 0)
                    {
                        //Posiciono la grilla en la última fila
                        dgvUsuarios.CurrentCell = dgvUsuarios[1, filas - 1];


                        //Cargar Datos a la fila                
                        dgvUsuarios.CurrentRow.Cells["Codigo"].Value = fila["IdUsuario"].ToString();
                        dgvUsuarios.CurrentRow.Cells["Usuario"].Value = fila["Usuario"].ToString();

                    }

                    //Posiciono la grilla en la última fila
                    dgvUsuarios.CurrentCell = dgvUsuarios[1, 0];

                    EventArgs ea = new EventArgs();
                    this.dgvUsuarios_SelectionChanged(this.dgvUsuarios, ea);
                }
            }
        }

        #endregion

        #region Metodo CargarMenuOpciones

        private void CargarMenuOpciones(int p_IdUser)
        {
            int IdUser = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["Codigo"].Value.ToString());
            string sQuery = "exec ObtenerMenuOpcUser " + IdUser;
            dgvFrmAcc.AutoGenerateColumns = false;

            // string sQuery = " "
            DataTable myData = clsDataBD.GetSql(sQuery);

            //Contador
            int filas = 0;

            //Clear
            dgvFrmAcc.Rows.Clear();

            //Mostrar Datos
            foreach (DataRow fila in myData.Rows)
            {
                /*Agregar Fila*/
                dgvFrmAcc.Rows.Add();

                // Cuento las filas de la grilla
                filas = dgvFrmAcc.Rows.Count;

                // Si la grilla no está vacía
                if (filas > 0)
                {
                    //Posiciono la grilla en la última fila
                    dgvFrmAcc.CurrentCell = dgvFrmAcc[2, filas - 1];
                }

                //Cargar Datos a la fila                
                dgvFrmAcc.CurrentRow.Cells["Item"].Value = fila["strOpcion"].ToString();
                dgvFrmAcc.CurrentRow.Cells["Habilitado"].Value = Convert.ToBoolean(fila["Habilitado"]);
                dgvFrmAcc.CurrentRow.Cells["IdDetMenu"].Value = fila["IdDetMenu"].ToString();
                dgvFrmAcc.CurrentRow.Cells["IdMenu"].Value = fila["IdMenuSistema"].ToString();

            }

            //Posiciono la grilla en la última fila
            dgvFrmAcc.CurrentCell = dgvFrmAcc[1, 0];            
            

        }

        #endregion

        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (!(dgvUsuarios.Rows.Count==0))
            {
                if (dgvUsuarios.CurrentRow.Cells["Codigo"].Value != null)
                {
                    // Cargo los datos de la fila seleccionada en sus correspondientes textbox. G.
                    int vUser = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["Codigo"].Value.ToString());
                    //Cargar Seteo Opciones Menu
                    CargarMenuOpciones(vUser);
                }
            }
        }

        #region  ChkAll Checked Change

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            bool bTildado = false;

            //Tildar Todos?
            if (chkAll.Checked)
            { bTildado = true;}            
            else
            { bTildado = false;}             

            //DESTILDAR TODO LOS ITEMS DE LA DERECHA
            foreach (DataGridViewRow row in dgvFrmAcc.Rows)
            {
                row.Cells["Habilitado"].Value = bTildado;
            } 

        }

        #endregion

        #region  CellDoubleClick DgvFrmAcc

        private void dgvFrmAcc_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToInt32(this.dgvFrmAcc.CurrentRow.Cells["Habilitado"].Value) == 1)
            {
                dgvFrmAcc.CurrentRow.Cells["Habilitado"].Value = false;
            }
            else
            {
                dgvFrmAcc.CurrentRow.Cells["Habilitado"].Value = true;
            }
        }

        #endregion

        #region Metodo : Boton Aceptar Click

        private void btnAccept_Click(object sender, EventArgs e)
        {
            string mySQL = "";
            int iTilde = 0;
            int IdMenu = 0;
            int IdDetMenu = 0;

            //DESTILDAR TODO LOS ITEMS DE LA DERECHA
            foreach (DataGridViewRow row in dgvFrmAcc.Rows)            
            {
               //Detalle Menu 
               IdDetMenu = Convert.ToInt32(row.Cells["IdDetMenu"].Value.ToString());
               //Usuario
               IdMenu =   Convert.ToInt32(row.Cells["IdMenu"].Value.ToString());
               //Tilde
                
               iTilde = Convert.ToInt32(row.Cells["Habilitado"].Value);
               mySQL = "UPDATE MenuOpcionesUser SET Habilitado = " + iTilde + " WHERE IdDetMenu = " + IdDetMenu 
                     + " AND IdMenu = " + IdMenu +
                       " AND IdUser = " + Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["Codigo"].Value.ToString());
               clsDataBD.GetSql(mySQL);

            }

            //MessageBox
            MessageBoxTemporal.Show("Los cambios se guardaron con exito!", "Información!", 4, true);
        }

        #endregion

        #region Metodo: setCargarToolTips

        private void setCargarToolTips()
        {
 
            toolTip1.SetToolTip(this.btnAccept, "Aceptar");
            toolTip2.SetToolTip(this.btnSalir, "Salir");

        }

        #endregion

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

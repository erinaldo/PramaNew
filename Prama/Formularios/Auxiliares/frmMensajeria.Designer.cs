namespace Prama
{
    partial class frmMensajeria
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMensajeria));
            this.dtgMensajes = new System.Windows.Forms.DataGridView();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Asunto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prioridad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remitente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Destinatario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lectura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cboBandeja = new System.Windows.Forms.ComboBox();
            this.cboMensajes = new System.Windows.Forms.ComboBox();
            this.lblMensajes = new System.Windows.Forms.Label();
            this.lblBandeja = new System.Windows.Forms.Label();
            this.grpBusqueda = new System.Windows.Forms.GroupBox();
            this.lblTexto = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtTexto = new System.Windows.Forms.TextBox();
            this.lblCriterio = new System.Windows.Forms.Label();
            this.cboCriterio = new System.Windows.Forms.ComboBox();
            this.btnPanel = new System.Windows.Forms.Panel();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnBorrar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgMensajes)).BeginInit();
            this.grpBusqueda.SuspendLayout();
            this.btnPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtgMensajes
            // 
            this.dtgMensajes.AllowUserToAddRows = false;
            this.dtgMensajes.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dtgMensajes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgMensajes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgMensajes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Fecha,
            this.Asunto,
            this.Prioridad,
            this.Estado,
            this.Remitente,
            this.Destinatario,
            this.Lectura});
            this.dtgMensajes.Location = new System.Drawing.Point(12, 66);
            this.dtgMensajes.Name = "dtgMensajes";
            this.dtgMensajes.ReadOnly = true;
            this.dtgMensajes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgMensajes.Size = new System.Drawing.Size(609, 174);
            this.dtgMensajes.StandardTab = true;
            this.dtgMensajes.TabIndex = 2;
            // 
            // Fecha
            // 
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            this.Fecha.Width = 50;
            // 
            // Asunto
            // 
            this.Asunto.HeaderText = "Asunto";
            this.Asunto.Name = "Asunto";
            this.Asunto.ReadOnly = true;
            // 
            // Prioridad
            // 
            this.Prioridad.HeaderText = "Prioridad";
            this.Prioridad.Name = "Prioridad";
            this.Prioridad.ReadOnly = true;
            this.Prioridad.Width = 50;
            // 
            // Estado
            // 
            this.Estado.HeaderText = "Estado";
            this.Estado.Name = "Estado";
            this.Estado.ReadOnly = true;
            this.Estado.Width = 50;
            // 
            // Remitente
            // 
            this.Remitente.HeaderText = "Remitente";
            this.Remitente.Name = "Remitente";
            this.Remitente.ReadOnly = true;
            // 
            // Destinatario
            // 
            this.Destinatario.HeaderText = "Destinatario";
            this.Destinatario.Name = "Destinatario";
            this.Destinatario.ReadOnly = true;
            // 
            // Lectura
            // 
            this.Lectura.HeaderText = "Lectura";
            this.Lectura.Name = "Lectura";
            this.Lectura.ReadOnly = true;
            // 
            // cboBandeja
            // 
            this.cboBandeja.AutoCompleteCustomSource.AddRange(new string[] {
            "Recibidos",
            "Enviados"});
            this.cboBandeja.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBandeja.FormattingEnabled = true;
            this.cboBandeja.Items.AddRange(new object[] {
            "Recibidos",
            "Enviados"});
            this.cboBandeja.Location = new System.Drawing.Point(81, 21);
            this.cboBandeja.Name = "cboBandeja";
            this.cboBandeja.Size = new System.Drawing.Size(126, 21);
            this.cboBandeja.TabIndex = 0;
            // 
            // cboMensajes
            // 
            this.cboMensajes.AutoCompleteCustomSource.AddRange(new string[] {
            "Todos",
            "Leídos",
            "No Leídos"});
            this.cboMensajes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMensajes.FormattingEnabled = true;
            this.cboMensajes.Items.AddRange(new object[] {
            "Todos",
            "No Leídos",
            "Leídos"});
            this.cboMensajes.Location = new System.Drawing.Point(282, 21);
            this.cboMensajes.Name = "cboMensajes";
            this.cboMensajes.Size = new System.Drawing.Size(133, 21);
            this.cboMensajes.TabIndex = 1;
            // 
            // lblMensajes
            // 
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.Location = new System.Drawing.Point(224, 21);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(52, 21);
            this.lblMensajes.TabIndex = 3;
            this.lblMensajes.Text = "Mensajes: ";
            // 
            // lblBandeja
            // 
            this.lblBandeja.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBandeja.Location = new System.Drawing.Point(12, 21);
            this.lblBandeja.Name = "lblBandeja";
            this.lblBandeja.Size = new System.Drawing.Size(63, 21);
            this.lblBandeja.TabIndex = 4;
            this.lblBandeja.Text = "Bandeja: ";
            // 
            // grpBusqueda
            // 
            this.grpBusqueda.Controls.Add(this.lblTexto);
            this.grpBusqueda.Controls.Add(this.btnBuscar);
            this.grpBusqueda.Controls.Add(this.txtTexto);
            this.grpBusqueda.Controls.Add(this.lblCriterio);
            this.grpBusqueda.Controls.Add(this.cboCriterio);
            this.grpBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBusqueda.Location = new System.Drawing.Point(12, 258);
            this.grpBusqueda.Name = "grpBusqueda";
            this.grpBusqueda.Size = new System.Drawing.Size(584, 60);
            this.grpBusqueda.TabIndex = 5;
            this.grpBusqueda.TabStop = false;
            this.grpBusqueda.Text = " Búsqueda: ";
            // 
            // lblTexto
            // 
            this.lblTexto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTexto.Location = new System.Drawing.Point(234, 26);
            this.lblTexto.Name = "lblTexto";
            this.lblTexto.Size = new System.Drawing.Size(41, 21);
            this.lblTexto.TabIndex = 8;
            this.lblTexto.Text = "Texto: ";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::Prama.Recursos.find;
            this.btnBuscar.Location = new System.Drawing.Point(514, 14);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(45, 40);
            this.btnBuscar.TabIndex = 5;
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnBuscar.UseVisualStyleBackColor = true;
            // 
            // txtTexto
            // 
            this.txtTexto.Location = new System.Drawing.Point(281, 26);
            this.txtTexto.Name = "txtTexto";
            this.txtTexto.Size = new System.Drawing.Size(202, 20);
            this.txtTexto.TabIndex = 4;
            // 
            // lblCriterio
            // 
            this.lblCriterio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCriterio.Location = new System.Drawing.Point(15, 26);
            this.lblCriterio.Name = "lblCriterio";
            this.lblCriterio.Size = new System.Drawing.Size(48, 21);
            this.lblCriterio.TabIndex = 6;
            this.lblCriterio.Text = "Criterio:";
            // 
            // cboCriterio
            // 
            this.cboCriterio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCriterio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCriterio.FormattingEnabled = true;
            this.cboCriterio.Items.AddRange(new object[] {
            "Fecha",
            "Asunto",
            "Prioridad",
            "Remitente",
            "Destinatario"});
            this.cboCriterio.Location = new System.Drawing.Point(69, 26);
            this.cboCriterio.Name = "cboCriterio";
            this.cboCriterio.Size = new System.Drawing.Size(149, 21);
            this.cboCriterio.TabIndex = 3;
            // 
            // btnPanel
            // 
            this.btnPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPanel.Controls.Add(this.btnImprimir);
            this.btnPanel.Controls.Add(this.btnBorrar);
            this.btnPanel.Controls.Add(this.btnSalir);
            this.btnPanel.Controls.Add(this.btnModificar);
            this.btnPanel.Controls.Add(this.btnAgregar);
            this.btnPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPanel.Location = new System.Drawing.Point(0, 352);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(633, 58);
            this.btnPanel.TabIndex = 42;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = global::Prama.Recursos.printer;
            this.btnImprimir.Location = new System.Drawing.Point(165, 9);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(45, 40);
            this.btnImprimir.TabIndex = 9;
            this.btnImprimir.UseVisualStyleBackColor = true;
            // 
            // btnBorrar
            // 
            this.btnBorrar.Image = global::Prama.Recursos.Borrar;
            this.btnBorrar.Location = new System.Drawing.Point(114, 9);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(45, 40);
            this.btnBorrar.TabIndex = 8;
            this.btnBorrar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnBorrar.UseVisualStyleBackColor = true;
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Image = global::Prama.Recursos.Salir;
            this.btnSalir.Location = new System.Drawing.Point(576, 9);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(45, 40);
            this.btnSalir.TabIndex = 10;
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalir.UseVisualStyleBackColor = true;
            // 
            // btnModificar
            // 
            this.btnModificar.Image = global::Prama.Recursos.Editar;
            this.btnModificar.Location = new System.Drawing.Point(63, 9);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(45, 40);
            this.btnModificar.TabIndex = 7;
            this.btnModificar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnModificar.UseVisualStyleBackColor = true;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Image = global::Prama.Recursos.Agregar;
            this.btnAgregar.Location = new System.Drawing.Point(12, 9);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(45, 40);
            this.btnAgregar.TabIndex = 6;
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAgregar.UseVisualStyleBackColor = true;
            // 
            // frmMensajeria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 410);
            this.Controls.Add(this.btnPanel);
            this.Controls.Add(this.grpBusqueda);
            this.Controls.Add(this.lblBandeja);
            this.Controls.Add(this.lblMensajes);
            this.Controls.Add(this.cboMensajes);
            this.Controls.Add(this.cboBandeja);
            this.Controls.Add(this.dtgMensajes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMensajeria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " - Mensajería Interna";
            this.Load += new System.EventHandler(this.frmMensajeria_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgMensajes)).EndInit();
            this.grpBusqueda.ResumeLayout(false);
            this.grpBusqueda.PerformLayout();
            this.btnPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgMensajes;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Asunto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Prioridad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remitente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Destinatario;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lectura;
        private System.Windows.Forms.ComboBox cboBandeja;
        private System.Windows.Forms.ComboBox cboMensajes;
        private System.Windows.Forms.Label lblMensajes;
        private System.Windows.Forms.Label lblBandeja;
        private System.Windows.Forms.GroupBox grpBusqueda;
        private System.Windows.Forms.Label lblTexto;
        private System.Windows.Forms.TextBox txtTexto;
        private System.Windows.Forms.Label lblCriterio;
        private System.Windows.Forms.ComboBox cboCriterio;
        private System.Windows.Forms.Panel btnPanel;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnBorrar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnAgregar;
    }
}
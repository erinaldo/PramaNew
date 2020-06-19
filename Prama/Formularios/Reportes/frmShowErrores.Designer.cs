namespace Prama
{
    partial class frmShowErrores
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShowErrores));
            this.btnPanel = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.grpErrores = new System.Windows.Forms.GroupBox();
            this.dtGridError = new System.Windows.Forms.DataGridView();
            this.Error = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Desripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnPanel.SuspendLayout();
            this.grpErrores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridError)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPanel
            // 
            this.btnPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPanel.Controls.Add(this.btnSalir);
            this.btnPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPanel.Location = new System.Drawing.Point(0, 251);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(605, 58);
            this.btnPanel.TabIndex = 42;
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Image = global::Prama.Recursos.Salir;
            this.btnSalir.Location = new System.Drawing.Point(545, 6);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(45, 40);
            this.btnSalir.TabIndex = 16;
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // grpErrores
            // 
            this.grpErrores.Controls.Add(this.dtGridError);
            this.grpErrores.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpErrores.Location = new System.Drawing.Point(12, 8);
            this.grpErrores.Name = "grpErrores";
            this.grpErrores.Size = new System.Drawing.Size(578, 225);
            this.grpErrores.TabIndex = 43;
            this.grpErrores.TabStop = false;
            this.grpErrores.Text = " Lista de Errores ";
            // 
            // dtGridError
            // 
            this.dtGridError.AllowUserToAddRows = false;
            this.dtGridError.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dtGridError.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dtGridError.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtGridError.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Error,
            this.Desripcion});
            this.dtGridError.Location = new System.Drawing.Point(6, 32);
            this.dtGridError.Name = "dtGridError";
            this.dtGridError.ReadOnly = true;
            this.dtGridError.RowHeadersVisible = false;
            this.dtGridError.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtGridError.Size = new System.Drawing.Size(561, 187);
            this.dtGridError.StandardTab = true;
            this.dtGridError.TabIndex = 1;
            // 
            // Error
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "000000";
            dataGridViewCellStyle2.NullValue = null;
            this.Error.DefaultCellStyle = dataGridViewCellStyle2;
            this.Error.Frozen = true;
            this.Error.HeaderText = "N° Error";
            this.Error.MinimumWidth = 6;
            this.Error.Name = "Error";
            this.Error.ReadOnly = true;
            this.Error.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Error.Width = 90;
            // 
            // Desripcion
            // 
            this.Desripcion.HeaderText = "Descripcion del Error";
            this.Desripcion.Name = "Desripcion";
            this.Desripcion.ReadOnly = true;
            this.Desripcion.Width = 465;
            // 
            // frmShowErrores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnSalir;
            this.ClientSize = new System.Drawing.Size(605, 309);
            this.Controls.Add(this.grpErrores);
            this.Controls.Add(this.btnPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmShowErrores";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " -  ERRORES";
            this.Load += new System.EventHandler(this.frmShowErrores_Load);
            this.btnPanel.ResumeLayout(false);
            this.grpErrores.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGridError)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel btnPanel;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.GroupBox grpErrores;
        private System.Windows.Forms.DataGridView dtGridError;
        private System.Windows.Forms.DataGridViewTextBoxColumn Error;
        private System.Windows.Forms.DataGridViewTextBoxColumn Desripcion;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
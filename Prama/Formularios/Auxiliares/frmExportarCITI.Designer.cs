namespace Prama.Formularios.Auxiliares
{
    partial class frmExportarCITI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExportarCITI));
            this.btnPanel = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.grpProgress = new System.Windows.Forms.GroupBox();
            this.lblDest = new System.Windows.Forms.Label();
            this.lblFolderDest = new System.Windows.Forms.Label();
            this.lblTask = new System.Windows.Forms.Label();
            this.lblPorc = new System.Windows.Forms.Label();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtVentas = new System.Windows.Forms.RadioButton();
            this.rbtCompras = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nAno = new System.Windows.Forms.NumericUpDown();
            this.nMes = new System.Windows.Forms.NumericUpDown();
            this.rbtMesCurso = new System.Windows.Forms.RadioButton();
            this.rbtEspecifica = new System.Windows.Forms.RadioButton();
            this.FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.btnPanel.SuspendLayout();
            this.grpProgress.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nAno)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMes)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPanel
            // 
            this.btnPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPanel.Controls.Add(this.btnSalir);
            this.btnPanel.Controls.Add(this.btnAceptar);
            this.btnPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPanel.Location = new System.Drawing.Point(0, 283);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(570, 58);
            this.btnPanel.TabIndex = 7;
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Image = global::Prama.Recursos.Salir;
            this.btnSalir.Location = new System.Drawing.Point(503, 6);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(45, 40);
            this.btnSalir.TabIndex = 9;
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalir.UseVisualStyleBackColor = true;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Image = global::Prama.Recursos.Aceptar;
            this.btnAceptar.Location = new System.Drawing.Point(452, 6);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(45, 40);
            this.btnAceptar.TabIndex = 8;
            this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // grpProgress
            // 
            this.grpProgress.Controls.Add(this.lblDest);
            this.grpProgress.Controls.Add(this.lblFolderDest);
            this.grpProgress.Controls.Add(this.lblTask);
            this.grpProgress.Controls.Add(this.lblPorc);
            this.grpProgress.Controls.Add(this.pBar);
            this.grpProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpProgress.Location = new System.Drawing.Point(16, 147);
            this.grpProgress.Name = "grpProgress";
            this.grpProgress.Size = new System.Drawing.Size(539, 122);
            this.grpProgress.TabIndex = 8;
            this.grpProgress.TabStop = false;
            this.grpProgress.Text = " Progreso de Tarea: ";
            // 
            // lblDest
            // 
            this.lblDest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDest.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblDest.Location = new System.Drawing.Point(118, 93);
            this.lblDest.Name = "lblDest";
            this.lblDest.Size = new System.Drawing.Size(402, 24);
            this.lblDest.TabIndex = 9;
            this.lblDest.Text = "Sin especificar...";
            // 
            // lblFolderDest
            // 
            this.lblFolderDest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFolderDest.Location = new System.Drawing.Point(8, 93);
            this.lblFolderDest.Name = "lblFolderDest";
            this.lblFolderDest.Size = new System.Drawing.Size(104, 18);
            this.lblFolderDest.TabIndex = 8;
            this.lblFolderDest.Text = "Carpeta Destino: ";
            // 
            // lblTask
            // 
            this.lblTask.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTask.Location = new System.Drawing.Point(8, 28);
            this.lblTask.Name = "lblTask";
            this.lblTask.Size = new System.Drawing.Size(94, 18);
            this.lblTask.TabIndex = 6;
            this.lblTask.Text = "Progreso:";
            // 
            // lblPorc
            // 
            this.lblPorc.Location = new System.Drawing.Point(464, 52);
            this.lblPorc.Name = "lblPorc";
            this.lblPorc.Size = new System.Drawing.Size(38, 18);
            this.lblPorc.TabIndex = 6;
            this.lblPorc.Text = "0 %";
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(11, 51);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(447, 19);
            this.pBar.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtVentas);
            this.groupBox1.Controls.Add(this.rbtCompras);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(19, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(189, 113);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Exportar Datos de ... ";
            // 
            // rbtVentas
            // 
            this.rbtVentas.AutoSize = true;
            this.rbtVentas.Checked = true;
            this.rbtVentas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtVentas.Location = new System.Drawing.Point(34, 36);
            this.rbtVentas.Name = "rbtVentas";
            this.rbtVentas.Size = new System.Drawing.Size(65, 19);
            this.rbtVentas.TabIndex = 0;
            this.rbtVentas.TabStop = true;
            this.rbtVentas.Text = "Ventas ";
            this.rbtVentas.UseVisualStyleBackColor = true;
            // 
            // rbtCompras
            // 
            this.rbtCompras.AutoSize = true;
            this.rbtCompras.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtCompras.Location = new System.Drawing.Point(34, 70);
            this.rbtCompras.Name = "rbtCompras";
            this.rbtCompras.Size = new System.Drawing.Size(75, 19);
            this.rbtCompras.TabIndex = 1;
            this.rbtCompras.Text = "Compras";
            this.rbtCompras.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.nAno);
            this.groupBox2.Controls.Add(this.nMes);
            this.groupBox2.Controls.Add(this.rbtMesCurso);
            this.groupBox2.Controls.Add(this.rbtEspecifica);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(230, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(325, 113);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Período?";
            // 
            // nAno
            // 
            this.nAno.Enabled = false;
            this.nAno.Location = new System.Drawing.Point(189, 70);
            this.nAno.Maximum = new decimal(new int[] {
            2099,
            0,
            0,
            0});
            this.nAno.Minimum = new decimal(new int[] {
            2017,
            0,
            0,
            0});
            this.nAno.Name = "nAno";
            this.nAno.Size = new System.Drawing.Size(79, 21);
            this.nAno.TabIndex = 5;
            this.nAno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nAno.Value = new decimal(new int[] {
            2017,
            0,
            0,
            0});
            // 
            // nMes
            // 
            this.nMes.Enabled = false;
            this.nMes.Location = new System.Drawing.Point(140, 70);
            this.nMes.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nMes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nMes.Name = "nMes";
            this.nMes.Size = new System.Drawing.Size(43, 21);
            this.nMes.TabIndex = 4;
            this.nMes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nMes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // rbtMesCurso
            // 
            this.rbtMesCurso.AutoSize = true;
            this.rbtMesCurso.Checked = true;
            this.rbtMesCurso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtMesCurso.Location = new System.Drawing.Point(34, 36);
            this.rbtMesCurso.Name = "rbtMesCurso";
            this.rbtMesCurso.Size = new System.Drawing.Size(99, 19);
            this.rbtMesCurso.TabIndex = 2;
            this.rbtMesCurso.TabStop = true;
            this.rbtMesCurso.Text = "Mes en curso";
            this.rbtMesCurso.UseVisualStyleBackColor = true;
            this.rbtMesCurso.CheckedChanged += new System.EventHandler(this.rbtMesCurso_CheckedChanged);
            // 
            // rbtEspecifica
            // 
            this.rbtEspecifica.AutoSize = true;
            this.rbtEspecifica.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtEspecifica.Location = new System.Drawing.Point(34, 70);
            this.rbtEspecifica.Name = "rbtEspecifica";
            this.rbtEspecifica.Size = new System.Drawing.Size(85, 19);
            this.rbtEspecifica.TabIndex = 3;
            this.rbtEspecifica.Text = "Especificar";
            this.rbtEspecifica.UseVisualStyleBackColor = true;
            this.rbtEspecifica.CheckedChanged += new System.EventHandler(this.rbtEspecifica_CheckedChanged);
            // 
            // frmExportarCITI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 341);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpProgress);
            this.Controls.Add(this.btnPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExportarCITI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " - EXPORTAR DATOS PARA CITI COMPRAS - VENTAS";
            this.Load += new System.EventHandler(this.frmExportarCITI_Load);
            this.btnPanel.ResumeLayout(false);
            this.grpProgress.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nAno)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel btnPanel;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.GroupBox grpProgress;
        private System.Windows.Forms.Label lblTask;
        private System.Windows.Forms.Label lblPorc;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtVentas;
        private System.Windows.Forms.RadioButton rbtCompras;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbtMesCurso;
        private System.Windows.Forms.RadioButton rbtEspecifica;
        private System.Windows.Forms.NumericUpDown nAno;
        private System.Windows.Forms.NumericUpDown nMes;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowser;
        private System.Windows.Forms.Label lblDest;
        private System.Windows.Forms.Label lblFolderDest;
    }
}
namespace Prama.Formularios.Auxiliares
{
    partial class frmGeneraFilesActED
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGeneraFilesActED));
            this.grpProgress = new System.Windows.Forms.GroupBox();
            this.lblDest = new System.Windows.Forms.Label();
            this.lblFolderDest = new System.Windows.Forms.Label();
            this.lblTask = new System.Windows.Forms.Label();
            this.lblPorc = new System.Windows.Forms.Label();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.btnPanel = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.grpProgress.SuspendLayout();
            this.btnPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpProgress
            // 
            this.grpProgress.Controls.Add(this.lblDest);
            this.grpProgress.Controls.Add(this.lblFolderDest);
            this.grpProgress.Controls.Add(this.lblTask);
            this.grpProgress.Controls.Add(this.lblPorc);
            this.grpProgress.Controls.Add(this.pBar);
            this.grpProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpProgress.Location = new System.Drawing.Point(12, 25);
            this.grpProgress.Name = "grpProgress";
            this.grpProgress.Size = new System.Drawing.Size(539, 122);
            this.grpProgress.TabIndex = 10;
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
            this.lblPorc.Size = new System.Drawing.Size(56, 18);
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
            // btnPanel
            // 
            this.btnPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPanel.Controls.Add(this.btnSalir);
            this.btnPanel.Controls.Add(this.btnAceptar);
            this.btnPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPanel.Location = new System.Drawing.Point(0, 166);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(567, 58);
            this.btnPanel.TabIndex = 9;
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
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
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
            // frmGeneraFilesActED
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 224);
            this.Controls.Add(this.grpProgress);
            this.Controls.Add(this.btnPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGeneraFilesActED";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " - GENERADOR ARCHIVOS PARA ESPACIO DEPURATIVO";
            this.Load += new System.EventHandler(this.frmGeneraFilesActED_Load);
            this.grpProgress.ResumeLayout(false);
            this.btnPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpProgress;
        private System.Windows.Forms.Label lblDest;
        private System.Windows.Forms.Label lblFolderDest;
        private System.Windows.Forms.Label lblTask;
        private System.Windows.Forms.Label lblPorc;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.Panel btnPanel;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowser;
    }
}
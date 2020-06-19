namespace Prama.Formularios.Auxiliares
{
    partial class frmExportarAExcel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExportarAExcel));
            this.btnPanel = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.grpProgress = new System.Windows.Forms.GroupBox();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.picFoto = new System.Windows.Forms.PictureBox();
            this.lblTask = new System.Windows.Forms.Label();
            this.lblPorc = new System.Windows.Forms.Label();
            this.folderDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbDist = new System.Windows.Forms.RadioButton();
            this.rdbLimitLt = new System.Windows.Forms.RadioButton();
            this.rdbLimitCdba = new System.Windows.Forms.RadioButton();
            this.rdbAmbos = new System.Windows.Forms.RadioButton();
            this.rdbRevendedor = new System.Windows.Forms.RadioButton();
            this.rdbMostrador = new System.Windows.Forms.RadioButton();
            this.rdbRev10 = new System.Windows.Forms.RadioButton();
            this.rdbRev20 = new System.Windows.Forms.RadioButton();
            this.btnPanel.SuspendLayout();
            this.grpProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPanel
            // 
            this.btnPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPanel.Controls.Add(this.btnSalir);
            this.btnPanel.Controls.Add(this.btnAceptar);
            this.btnPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPanel.Location = new System.Drawing.Point(0, 280);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(564, 58);
            this.btnPanel.TabIndex = 6;
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Image = global::Prama.Recursos.Salir;
            this.btnSalir.Location = new System.Drawing.Point(503, 6);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(45, 40);
            this.btnSalir.TabIndex = 8;
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
            this.btnAceptar.TabIndex = 7;
            this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(11, 51);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(430, 19);
            this.pBar.TabIndex = 5;
            // 
            // grpProgress
            // 
            this.grpProgress.Controls.Add(this.lblCurrent);
            this.grpProgress.Controls.Add(this.picFoto);
            this.grpProgress.Controls.Add(this.lblTask);
            this.grpProgress.Controls.Add(this.lblPorc);
            this.grpProgress.Controls.Add(this.pBar);
            this.grpProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpProgress.Location = new System.Drawing.Point(12, 173);
            this.grpProgress.Name = "grpProgress";
            this.grpProgress.Size = new System.Drawing.Size(539, 92);
            this.grpProgress.TabIndex = 3;
            this.grpProgress.TabStop = false;
            this.grpProgress.Text = " Progreso de Tarea: ";
            // 
            // lblCurrent
            // 
            this.lblCurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrent.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblCurrent.Location = new System.Drawing.Point(70, 28);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(388, 18);
            this.lblCurrent.TabIndex = 7;
            this.lblCurrent.Text = "En Espera...";
            // 
            // picFoto
            // 
            this.picFoto.Image = global::Prama.Recursos.excel;
            this.picFoto.Location = new System.Drawing.Point(500, 44);
            this.picFoto.Name = "picFoto";
            this.picFoto.Size = new System.Drawing.Size(26, 28);
            this.picFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picFoto.TabIndex = 27;
            this.picFoto.TabStop = false;
            // 
            // lblTask
            // 
            this.lblTask.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTask.Location = new System.Drawing.Point(8, 28);
            this.lblTask.Name = "lblTask";
            this.lblTask.Size = new System.Drawing.Size(56, 18);
            this.lblTask.TabIndex = 6;
            this.lblTask.Text = "Estado: ";
            // 
            // lblPorc
            // 
            this.lblPorc.Location = new System.Drawing.Point(447, 52);
            this.lblPorc.Name = "lblPorc";
            this.lblPorc.Size = new System.Drawing.Size(38, 18);
            this.lblPorc.TabIndex = 5;
            this.lblPorc.Text = "0 %";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbRev20);
            this.groupBox1.Controls.Add(this.rdbRev10);
            this.groupBox1.Controls.Add(this.rdbDist);
            this.groupBox1.Controls.Add(this.rdbLimitLt);
            this.groupBox1.Controls.Add(this.rdbLimitCdba);
            this.groupBox1.Controls.Add(this.rdbAmbos);
            this.groupBox1.Controls.Add(this.rdbRevendedor);
            this.groupBox1.Controls.Add(this.rdbMostrador);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(539, 144);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Generar Formulario de Pedido para ... ";
            // 
            // rdbDist
            // 
            this.rdbDist.AutoSize = true;
            this.rdbDist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbDist.Location = new System.Drawing.Point(24, 108);
            this.rdbDist.Name = "rdbDist";
            this.rdbDist.Size = new System.Drawing.Size(88, 19);
            this.rdbDist.TabIndex = 6;
            this.rdbDist.Text = "Distribuidor";
            this.rdbDist.UseVisualStyleBackColor = true;
            // 
            // rdbLimitLt
            // 
            this.rdbLimitLt.AutoSize = true;
            this.rdbLimitLt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbLimitLt.Location = new System.Drawing.Point(163, 58);
            this.rdbLimitLt.Name = "rdbLimitLt";
            this.rdbLimitLt.Size = new System.Drawing.Size(366, 19);
            this.rdbLimitLt.TabIndex = 3;
            this.rdbLimitLt.Text = "Precios Sugeridos Público ( Prov. Limitrofes de Limitrofes Cba)";
            this.rdbLimitLt.UseVisualStyleBackColor = true;
            // 
            // rdbLimitCdba
            // 
            this.rdbLimitCdba.AutoSize = true;
            this.rdbLimitCdba.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbLimitCdba.Location = new System.Drawing.Point(163, 33);
            this.rdbLimitCdba.Name = "rdbLimitCdba";
            this.rdbLimitCdba.Size = new System.Drawing.Size(363, 19);
            this.rdbLimitCdba.TabIndex = 1;
            this.rdbLimitCdba.Text = "Precios Sugeridos Público ( Provincias Limítrofes de Córdoba)";
            this.rdbLimitCdba.UseVisualStyleBackColor = true;
            // 
            // rdbAmbos
            // 
            this.rdbAmbos.AutoSize = true;
            this.rdbAmbos.Checked = true;
            this.rdbAmbos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbAmbos.Location = new System.Drawing.Point(163, 108);
            this.rdbAmbos.Name = "rdbAmbos";
            this.rdbAmbos.Size = new System.Drawing.Size(62, 19);
            this.rdbAmbos.TabIndex = 7;
            this.rdbAmbos.TabStop = true;
            this.rdbAmbos.Text = " Todos";
            this.rdbAmbos.UseVisualStyleBackColor = true;
            // 
            // rdbRevendedor
            // 
            this.rdbRevendedor.AutoSize = true;
            this.rdbRevendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbRevendedor.Location = new System.Drawing.Point(24, 58);
            this.rdbRevendedor.Name = "rdbRevendedor";
            this.rdbRevendedor.Size = new System.Drawing.Size(92, 19);
            this.rdbRevendedor.TabIndex = 2;
            this.rdbRevendedor.Text = "Revendedor";
            this.rdbRevendedor.UseVisualStyleBackColor = true;
            // 
            // rdbMostrador
            // 
            this.rdbMostrador.AutoSize = true;
            this.rdbMostrador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbMostrador.Location = new System.Drawing.Point(24, 33);
            this.rdbMostrador.Name = "rdbMostrador";
            this.rdbMostrador.Size = new System.Drawing.Size(81, 19);
            this.rdbMostrador.TabIndex = 0;
            this.rdbMostrador.Text = "Mostrador";
            this.rdbMostrador.UseVisualStyleBackColor = true;
            // 
            // rdbRev10
            // 
            this.rdbRev10.AutoSize = true;
            this.rdbRev10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbRev10.Location = new System.Drawing.Point(24, 83);
            this.rdbRev10.Name = "rdbRev10";
            this.rdbRev10.Size = new System.Drawing.Size(116, 19);
            this.rdbRev10.TabIndex = 4;
            this.rdbRev10.Text = "Revendedor +10";
            this.rdbRev10.UseVisualStyleBackColor = true;
            // 
            // rdbRev20
            // 
            this.rdbRev20.AutoSize = true;
            this.rdbRev20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbRev20.Location = new System.Drawing.Point(163, 83);
            this.rdbRev20.Name = "rdbRev20";
            this.rdbRev20.Size = new System.Drawing.Size(116, 19);
            this.rdbRev20.TabIndex = 5;
            this.rdbRev20.Text = "Revendedor +20";
            this.rdbRev20.UseVisualStyleBackColor = true;
            // 
            // frmExportarAExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnSalir;
            this.ClientSize = new System.Drawing.Size(564, 338);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpProgress);
            this.Controls.Add(this.btnPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExportarAExcel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " -  GENERAR FORMULARIO DE PEDIDO ";
            this.Load += new System.EventHandler(this.frmExportarAExcel_Load);
            this.btnPanel.ResumeLayout(false);
            this.grpProgress.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel btnPanel;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.GroupBox grpProgress;
        private System.Windows.Forms.Label lblPorc;
        private System.Windows.Forms.FolderBrowserDialog folderDlg;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.ToolTip toolTip3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbAmbos;
        private System.Windows.Forms.RadioButton rdbRevendedor;
        private System.Windows.Forms.RadioButton rdbMostrador;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.Label lblTask;
        private System.Windows.Forms.PictureBox picFoto;
        private System.Windows.Forms.RadioButton rdbLimitLt;
        private System.Windows.Forms.RadioButton rdbLimitCdba;
        private System.Windows.Forms.RadioButton rdbDist;
        private System.Windows.Forms.RadioButton rdbRev20;
        private System.Windows.Forms.RadioButton rdbRev10;
    }
}
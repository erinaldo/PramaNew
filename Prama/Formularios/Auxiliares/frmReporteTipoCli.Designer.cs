namespace Prama.Formularios.Auxiliares
{
    partial class frmReporteTipoCli
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReporteTipoCli));
            this.gpbRango = new System.Windows.Forms.GroupBox();
            this.dtHasta = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtDesde = new System.Windows.Forms.DateTimePicker();
            this.lblDesde = new System.Windows.Forms.Label();
            this.gpbTipoCli = new System.Windows.Forms.GroupBox();
            this.rbnCRsal = new System.Windows.Forms.RadioButton();
            this.rbnDist = new System.Windows.Forms.RadioButton();
            this.rbnRev = new System.Windows.Forms.RadioButton();
            this.rbnPub = new System.Windows.Forms.RadioButton();
            this.rbnAll = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.dvgData = new System.Windows.Forms.DataGridView();
            this.IdCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RazonSocial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new Prama.NumericGridColumn();
            this.Total_N = new Prama.NumericGridColumn();
            this.TotalGral = new Prama.NumericGridColumn();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTotGral = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.gpbRango.SuspendLayout();
            this.gpbTipoCli.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgData)).BeginInit();
            this.SuspendLayout();
            // 
            // gpbRango
            // 
            this.gpbRango.Controls.Add(this.dtHasta);
            this.gpbRango.Controls.Add(this.label1);
            this.gpbRango.Controls.Add(this.dtDesde);
            this.gpbRango.Controls.Add(this.lblDesde);
            this.gpbRango.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbRango.Location = new System.Drawing.Point(427, 21);
            this.gpbRango.Name = "gpbRango";
            this.gpbRango.Size = new System.Drawing.Size(318, 62);
            this.gpbRango.TabIndex = 15;
            this.gpbRango.TabStop = false;
            this.gpbRango.Text = " Desde-Hasta ";
            // 
            // dtHasta
            // 
            this.dtHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtHasta.Location = new System.Drawing.Point(208, 28);
            this.dtHasta.MaxDate = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
            this.dtHasta.MinDate = new System.DateTime(2016, 1, 1, 0, 0, 0, 0);
            this.dtHasta.Name = "dtHasta";
            this.dtHasta.Size = new System.Drawing.Size(104, 20);
            this.dtHasta.TabIndex = 9;
            this.dtHasta.ValueChanged += new System.EventHandler(this.dtHasta_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(164, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Hasta:";
            // 
            // dtDesde
            // 
            this.dtDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDesde.Location = new System.Drawing.Point(54, 28);
            this.dtDesde.MaxDate = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
            this.dtDesde.MinDate = new System.DateTime(2016, 1, 1, 0, 0, 0, 0);
            this.dtDesde.Name = "dtDesde";
            this.dtDesde.Size = new System.Drawing.Size(104, 20);
            this.dtDesde.TabIndex = 7;
            this.dtDesde.ValueChanged += new System.EventHandler(this.dtDesde_ValueChanged);
            // 
            // lblDesde
            // 
            this.lblDesde.AutoSize = true;
            this.lblDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesde.Location = new System.Drawing.Point(7, 30);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(41, 13);
            this.lblDesde.TabIndex = 6;
            this.lblDesde.Text = "Desde:";
            // 
            // gpbTipoCli
            // 
            this.gpbTipoCli.Controls.Add(this.rbnCRsal);
            this.gpbTipoCli.Controls.Add(this.rbnDist);
            this.gpbTipoCli.Controls.Add(this.rbnRev);
            this.gpbTipoCli.Controls.Add(this.rbnPub);
            this.gpbTipoCli.Controls.Add(this.rbnAll);
            this.gpbTipoCli.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbTipoCli.Location = new System.Drawing.Point(12, 21);
            this.gpbTipoCli.Name = "gpbTipoCli";
            this.gpbTipoCli.Size = new System.Drawing.Size(409, 62);
            this.gpbTipoCli.TabIndex = 14;
            this.gpbTipoCli.TabStop = false;
            this.gpbTipoCli.Text = " Tipo Cliente ";
            // 
            // rbnCRsal
            // 
            this.rbnCRsal.AutoSize = true;
            this.rbnCRsal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnCRsal.Location = new System.Drawing.Point(314, 28);
            this.rbnCRsal.Name = "rbnCRsal";
            this.rbnCRsal.Size = new System.Drawing.Size(86, 17);
            this.rbnCRsal.TabIndex = 18;
            this.rbnCRsal.Text = "Corresponsal";
            this.rbnCRsal.UseVisualStyleBackColor = true;
            this.rbnCRsal.CheckedChanged += new System.EventHandler(this.rbnCRsal_CheckedChanged);
            // 
            // rbnDist
            // 
            this.rbnDist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnDist.Location = new System.Drawing.Point(135, 28);
            this.rbnDist.Name = "rbnDist";
            this.rbnDist.Size = new System.Drawing.Size(83, 17);
            this.rbnDist.TabIndex = 4;
            this.rbnDist.Text = "Distribuidor";
            this.rbnDist.UseVisualStyleBackColor = true;
            this.rbnDist.CheckedChanged += new System.EventHandler(this.rbnDist_CheckedChanged);
            // 
            // rbnRev
            // 
            this.rbnRev.AutoSize = true;
            this.rbnRev.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnRev.Location = new System.Drawing.Point(224, 28);
            this.rbnRev.Name = "rbnRev";
            this.rbnRev.Size = new System.Drawing.Size(84, 17);
            this.rbnRev.TabIndex = 3;
            this.rbnRev.Text = "Revendedor";
            this.rbnRev.UseVisualStyleBackColor = true;
            this.rbnRev.CheckedChanged += new System.EventHandler(this.rbnRev_CheckedChanged);
            // 
            // rbnPub
            // 
            this.rbnPub.AutoSize = true;
            this.rbnPub.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnPub.Location = new System.Drawing.Point(69, 28);
            this.rbnPub.Name = "rbnPub";
            this.rbnPub.Size = new System.Drawing.Size(60, 17);
            this.rbnPub.TabIndex = 2;
            this.rbnPub.Text = "Público";
            this.rbnPub.UseVisualStyleBackColor = true;
            this.rbnPub.CheckedChanged += new System.EventHandler(this.rbnPub_CheckedChanged);
            // 
            // rbnAll
            // 
            this.rbnAll.AutoSize = true;
            this.rbnAll.Checked = true;
            this.rbnAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnAll.Location = new System.Drawing.Point(6, 28);
            this.rbnAll.Name = "rbnAll";
            this.rbnAll.Size = new System.Drawing.Size(58, 17);
            this.rbnAll.TabIndex = 1;
            this.rbnAll.TabStop = true;
            this.rbnAll.Text = " Todos";
            this.rbnAll.UseVisualStyleBackColor = true;
            this.rbnAll.CheckedChanged += new System.EventHandler(this.rbnAll_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.btnSalir);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 387);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(757, 58);
            this.panel1.TabIndex = 17;
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Image = global::Prama.Recursos.Salir;
            this.btnSalir.Location = new System.Drawing.Point(700, 9);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(45, 40);
            this.btnSalir.TabIndex = 15;
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // dvgData
            // 
            this.dvgData.AllowUserToAddRows = false;
            this.dvgData.AllowUserToDeleteRows = false;
            this.dvgData.AllowUserToResizeColumns = false;
            this.dvgData.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dvgData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dvgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdCliente,
            this.RazonSocial,
            this.Mail,
            this.TipoCliente,
            this.Total,
            this.Total_N,
            this.TotalGral});
            this.dvgData.Location = new System.Drawing.Point(12, 102);
            this.dvgData.Name = "dvgData";
            this.dvgData.RowHeadersVisible = false;
            this.dvgData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dvgData.Size = new System.Drawing.Size(737, 225);
            this.dvgData.StandardTab = true;
            this.dvgData.TabIndex = 16;
            // 
            // IdCliente
            // 
            this.IdCliente.DataPropertyName = "IdCliente";
            this.IdCliente.HeaderText = "Código";
            this.IdCliente.Name = "IdCliente";
            this.IdCliente.Width = 70;
            // 
            // RazonSocial
            // 
            this.RazonSocial.DataPropertyName = "RazonSocial";
            this.RazonSocial.HeaderText = "RazonSocial";
            this.RazonSocial.Name = "RazonSocial";
            this.RazonSocial.Width = 180;
            // 
            // Mail
            // 
            this.Mail.DataPropertyName = "Mail";
            this.Mail.HeaderText = "Mail";
            this.Mail.Name = "Mail";
            this.Mail.Width = 130;
            // 
            // TipoCliente
            // 
            this.TipoCliente.DataPropertyName = "TipoCliente";
            this.TipoCliente.HeaderText = "Tipo Cliente";
            this.TipoCliente.Name = "TipoCliente";
            // 
            // Total
            // 
            this.Total.DataPropertyName = "Total";
            this.Total.DecimalDigits = 2;
            this.Total.DecimalSeparator = ".";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Total.DefaultCellStyle = dataGridViewCellStyle2;
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            this.Total.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Total.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Total.Width = 80;
            // 
            // Total_N
            // 
            this.Total_N.DataPropertyName = "Total_N";
            this.Total_N.DecimalDigits = 2;
            this.Total_N.DecimalSeparator = ".";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            this.Total_N.DefaultCellStyle = dataGridViewCellStyle3;
            this.Total_N.HeaderText = "Total_N";
            this.Total_N.Name = "Total_N";
            this.Total_N.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Total_N.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Total_N.Visible = false;
            this.Total_N.Width = 80;
            // 
            // TotalGral
            // 
            this.TotalGral.DecimalDigits = 2;
            this.TotalGral.DecimalSeparator = ".";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TotalGral.DefaultCellStyle = dataGridViewCellStyle4;
            this.TotalGral.HeaderText = "TotalGral";
            this.TotalGral.Name = "TotalGral";
            this.TotalGral.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TotalGral.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.TotalGral.Visible = false;
            this.TotalGral.Width = 80;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "IdCliente";
            this.dataGridViewTextBoxColumn1.HeaderText = "Código";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 70;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "RazonSocial";
            this.dataGridViewTextBoxColumn2.HeaderText = "RazonSocial";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Mail";
            this.dataGridViewTextBoxColumn3.HeaderText = "Mail";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "TipoCliente";
            this.dataGridViewTextBoxColumn4.HeaderText = "Tipo Cliente";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Total";
            this.dataGridViewTextBoxColumn5.HeaderText = "Total";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 80;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Total_N";
            this.dataGridViewTextBoxColumn6.HeaderText = "Total_N";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Visible = false;
            this.dataGridViewTextBoxColumn6.Width = 80;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "TotalGral";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Visible = false;
            // 
            // txtTotGral
            // 
            this.txtTotGral.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotGral.Location = new System.Drawing.Point(613, 344);
            this.txtTotGral.MaxLength = 13;
            this.txtTotGral.Name = "txtTotGral";
            this.txtTotGral.ReadOnly = true;
            this.txtTotGral.Size = new System.Drawing.Size(134, 22);
            this.txtTotGral.TabIndex = 19;
            this.txtTotGral.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(516, 345);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(91, 21);
            this.lblTotal.TabIndex = 18;
            this.lblTotal.Text = "Total Gral. $:";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmReporteTipoCli
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 445);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.txtTotGral);
            this.Controls.Add(this.gpbRango);
            this.Controls.Add(this.gpbTipoCli);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dvgData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReporteTipoCli";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "  - REPORTE REVISION REVENDEDORES ";
            this.Load += new System.EventHandler(this.frmReporteTipoCli_Load);
            this.gpbRango.ResumeLayout(false);
            this.gpbRango.PerformLayout();
            this.gpbTipoCli.ResumeLayout(false);
            this.gpbTipoCli.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gpbRango;
        private System.Windows.Forms.DateTimePicker dtHasta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtDesde;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.GroupBox gpbTipoCli;
        private System.Windows.Forms.RadioButton rbnDist;
        private System.Windows.Forms.RadioButton rbnRev;
        private System.Windows.Forms.RadioButton rbnPub;
        private System.Windows.Forms.RadioButton rbnAll;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.DataGridView dvgData;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.RadioButton rbnCRsal;
        private System.Windows.Forms.TextBox txtTotGral;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn RazonSocial;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mail;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoCliente;
        private NumericGridColumn Total;
        private NumericGridColumn Total_N;
        private NumericGridColumn TotalGral;
        private System.Windows.Forms.Label lblTotal;
    }
}
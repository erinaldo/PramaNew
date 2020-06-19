namespace Prama.Formularios.Informes
{
    partial class frmInformeVtasTipoCli
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInformeVtasTipoCli));
            this.dvgData = new System.Windows.Forms.DataGridView();
            this.IdTipoCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Año = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CantidadVendida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtTotGral = new System.Windows.Forms.TextBox();
            this.gpbTipoCli = new System.Windows.Forms.GroupBox();
            this.rbnDist = new System.Windows.Forms.RadioButton();
            this.rbnRev = new System.Windows.Forms.RadioButton();
            this.rbnPub = new System.Windows.Forms.RadioButton();
            this.rbnAll = new System.Windows.Forms.RadioButton();
            this.gpbRango = new System.Windows.Forms.GroupBox();
            this.dtHasta = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtDesde = new System.Windows.Forms.DateTimePicker();
            this.lblDesde = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCantTotal = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rbnCRsal = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dvgData)).BeginInit();
            this.panel1.SuspendLayout();
            this.gpbTipoCli.SuspendLayout();
            this.gpbRango.SuspendLayout();
            this.SuspendLayout();
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
            this.IdTipoCliente,
            this.TipoCliente,
            this.Mes,
            this.Año,
            this.CantidadVendida,
            this.Total});
            this.dvgData.Location = new System.Drawing.Point(12, 93);
            this.dvgData.Name = "dvgData";
            this.dvgData.RowHeadersVisible = false;
            this.dvgData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dvgData.Size = new System.Drawing.Size(737, 267);
            this.dvgData.StandardTab = true;
            this.dvgData.TabIndex = 10;
            // 
            // IdTipoCliente
            // 
            this.IdTipoCliente.DataPropertyName = "IdTipoCliente";
            this.IdTipoCliente.HeaderText = "IdTipoCliente";
            this.IdTipoCliente.Name = "IdTipoCliente";
            this.IdTipoCliente.Visible = false;
            // 
            // TipoCliente
            // 
            this.TipoCliente.DataPropertyName = "TipoCliente";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.TipoCliente.DefaultCellStyle = dataGridViewCellStyle2;
            this.TipoCliente.HeaderText = "Tipo Cliente";
            this.TipoCliente.Name = "TipoCliente";
            this.TipoCliente.Width = 250;
            // 
            // Mes
            // 
            this.Mes.DataPropertyName = "Mes";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Mes.DefaultCellStyle = dataGridViewCellStyle3;
            this.Mes.HeaderText = "Mes";
            this.Mes.Name = "Mes";
            // 
            // Año
            // 
            this.Año.DataPropertyName = "Año";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Año.DefaultCellStyle = dataGridViewCellStyle4;
            this.Año.HeaderText = "Año";
            this.Año.Name = "Año";
            // 
            // CantidadVendida
            // 
            this.CantidadVendida.DataPropertyName = "Cantidad Vendida";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CantidadVendida.DefaultCellStyle = dataGridViewCellStyle5;
            this.CantidadVendida.HeaderText = "Cantidad Vendida";
            this.CantidadVendida.Name = "CantidadVendida";
            this.CantidadVendida.Width = 130;
            // 
            // Total
            // 
            this.Total.DataPropertyName = "Total";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Total.DefaultCellStyle = dataGridViewCellStyle6;
            this.Total.HeaderText = "Total $";
            this.Total.Name = "Total";
            this.Total.Width = 130;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.btnImprimir);
            this.panel1.Controls.Add(this.btnSalir);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 419);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(763, 58);
            this.panel1.TabIndex = 13;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = global::Prama.Recursos.printer;
            this.btnImprimir.Location = new System.Drawing.Point(12, 9);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(45, 40);
            this.btnImprimir.TabIndex = 14;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Image = global::Prama.Recursos.Salir;
            this.btnSalir.Location = new System.Drawing.Point(705, 9);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(45, 40);
            this.btnSalir.TabIndex = 15;
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalir.UseVisualStyleBackColor = true;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(536, 376);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(73, 21);
            this.lblTotal.TabIndex = 11;
            this.lblTotal.Text = "Total $:";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTotGral
            // 
            this.txtTotGral.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotGral.Location = new System.Drawing.Point(615, 375);
            this.txtTotGral.MaxLength = 13;
            this.txtTotGral.Name = "txtTotGral";
            this.txtTotGral.ReadOnly = true;
            this.txtTotGral.Size = new System.Drawing.Size(134, 22);
            this.txtTotGral.TabIndex = 12;
            this.txtTotGral.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // gpbTipoCli
            // 
            this.gpbTipoCli.Controls.Add(this.rbnCRsal);
            this.gpbTipoCli.Controls.Add(this.rbnDist);
            this.gpbTipoCli.Controls.Add(this.rbnRev);
            this.gpbTipoCli.Controls.Add(this.rbnPub);
            this.gpbTipoCli.Controls.Add(this.rbnAll);
            this.gpbTipoCli.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbTipoCli.Location = new System.Drawing.Point(12, 12);
            this.gpbTipoCli.Name = "gpbTipoCli";
            this.gpbTipoCli.Size = new System.Drawing.Size(401, 62);
            this.gpbTipoCli.TabIndex = 0;
            this.gpbTipoCli.TabStop = false;
            this.gpbTipoCli.Text = " Tipo Cliente ";
            // 
            // rbnDist
            // 
            this.rbnDist.AutoSize = true;
            this.rbnDist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnDist.Location = new System.Drawing.Point(136, 28);
            this.rbnDist.Name = "rbnDist";
            this.rbnDist.Size = new System.Drawing.Size(80, 17);
            this.rbnDist.TabIndex = 4;
            this.rbnDist.Text = " Distribuidor";
            this.rbnDist.UseVisualStyleBackColor = true;
            this.rbnDist.CheckedChanged += new System.EventHandler(this.rbnDist_CheckedChanged);
            // 
            // rbnRev
            // 
            this.rbnRev.AutoSize = true;
            this.rbnRev.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnRev.Location = new System.Drawing.Point(221, 28);
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
            this.rbnPub.Location = new System.Drawing.Point(70, 28);
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
            // gpbRango
            // 
            this.gpbRango.Controls.Add(this.dtHasta);
            this.gpbRango.Controls.Add(this.label1);
            this.gpbRango.Controls.Add(this.dtDesde);
            this.gpbRango.Controls.Add(this.lblDesde);
            this.gpbRango.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbRango.Location = new System.Drawing.Point(419, 12);
            this.gpbRango.Name = "gpbRango";
            this.gpbRango.Size = new System.Drawing.Size(330, 62);
            this.gpbRango.TabIndex = 5;
            this.gpbRango.TabStop = false;
            this.gpbRango.Text = " Desde-Hasta ";
            // 
            // dtHasta
            // 
            this.dtHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtHasta.Location = new System.Drawing.Point(216, 28);
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
            this.label1.Location = new System.Drawing.Point(172, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Hasta:";
            // 
            // dtDesde
            // 
            this.dtDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDesde.Location = new System.Drawing.Point(62, 28);
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
            this.lblDesde.Location = new System.Drawing.Point(15, 30);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(41, 13);
            this.lblDesde.TabIndex = 6;
            this.lblDesde.Text = "Desde:";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(249, 377);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 21);
            this.label2.TabIndex = 14;
            this.label2.Text = "Total Cantidad:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCantTotal
            // 
            this.txtCantTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantTotal.Location = new System.Drawing.Point(373, 376);
            this.txtCantTotal.MaxLength = 13;
            this.txtCantTotal.Name = "txtCantTotal";
            this.txtCantTotal.ReadOnly = true;
            this.txtCantTotal.Size = new System.Drawing.Size(134, 22);
            this.txtCantTotal.TabIndex = 15;
            this.txtCantTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "IdTipoCliente";
            this.dataGridViewTextBoxColumn1.HeaderText = "IdTipoCliente";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "TipoCliente";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn2.HeaderText = "Tipo Cliente";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 250;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Mes";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn3.HeaderText = "Mes";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 50;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Año";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn4.HeaderText = "Año";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 50;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Cantidad Vendida";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn5.HeaderText = "Cantidad Vendida";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 130;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Total";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewTextBoxColumn6.HeaderText = "Total $";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 130;
            // 
            // rbnCRsal
            // 
            this.rbnCRsal.AutoSize = true;
            this.rbnCRsal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnCRsal.Location = new System.Drawing.Point(311, 28);
            this.rbnCRsal.Name = "rbnCRsal";
            this.rbnCRsal.Size = new System.Drawing.Size(86, 17);
            this.rbnCRsal.TabIndex = 5;
            this.rbnCRsal.Text = "Corresponsal";
            this.rbnCRsal.UseVisualStyleBackColor = true;
            this.rbnCRsal.CheckedChanged += new System.EventHandler(this.rbnCRsal_CheckedChanged);
            // 
            // frmInformeVtasTipoCli
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 477);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCantTotal);
            this.Controls.Add(this.gpbRango);
            this.Controls.Add(this.gpbTipoCli);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.txtTotGral);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dvgData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInformeVtasTipoCli";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " - DETALLE DE VENTAS POR TIPO DE CLIENTE";
            this.Load += new System.EventHandler(this.frmInformeVtasTipoCli_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dvgData)).EndInit();
            this.panel1.ResumeLayout(false);
            this.gpbTipoCli.ResumeLayout(false);
            this.gpbTipoCli.PerformLayout();
            this.gpbRango.ResumeLayout(false);
            this.gpbRango.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dvgData;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtTotGral;
        private System.Windows.Forms.GroupBox gpbTipoCli;
        private System.Windows.Forms.RadioButton rbnDist;
        private System.Windows.Forms.RadioButton rbnRev;
        private System.Windows.Forms.RadioButton rbnPub;
        private System.Windows.Forms.RadioButton rbnAll;
        private System.Windows.Forms.GroupBox gpbRango;
        private System.Windows.Forms.DateTimePicker dtHasta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtDesde;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCantTotal;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdTipoCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mes;
        private System.Windows.Forms.DataGridViewTextBoxColumn Año;
        private System.Windows.Forms.DataGridViewTextBoxColumn CantidadVendida;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.RadioButton rbnCRsal;
    }
}
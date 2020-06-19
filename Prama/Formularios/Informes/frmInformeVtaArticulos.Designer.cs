namespace Prama.Formularios.Informes
{
    partial class frmInformeVtaArticulos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInformeVtaArticulos));
            this.grpOrder = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.btnPanel = new System.Windows.Forms.Panel();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.dgvArtVtas = new System.Windows.Forms.DataGridView();
            this.CodArt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rubro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubRubro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad_B = new Prama.NumericGridColumn();
            this.Cantidad_N = new Prama.NumericGridColumn();
            this.CantidadVendida = new Prama.NumericGridColumn();
            this.grpRubroSubRubro = new System.Windows.Forms.GroupBox();
            this.cboSubRubro = new System.Windows.Forms.ComboBox();
            this.cboRubro = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRubro = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numericGridColumn1 = new Prama.NumericGridColumn();
            this.rdbVsitaGral = new System.Windows.Forms.RadioButton();
            this.rdbRubSub = new System.Windows.Forms.RadioButton();
            this.rdbCli = new System.Windows.Forms.RadioButton();
            this.btnCli = new System.Windows.Forms.Button();
            this.txtCuit = new System.Windows.Forms.TextBox();
            this.lblCUIT = new System.Windows.Forms.Label();
            this.rdbPub = new System.Windows.Forms.RadioButton();
            this.grpClientes = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.grpOrder.SuspendLayout();
            this.btnPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArtVtas)).BeginInit();
            this.grpRubroSubRubro.SuspendLayout();
            this.grpClientes.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpOrder
            // 
            this.grpOrder.Controls.Add(this.label2);
            this.grpOrder.Controls.Add(this.dtpHasta);
            this.grpOrder.Controls.Add(this.label1);
            this.grpOrder.Controls.Add(this.dtpDesde);
            this.grpOrder.Location = new System.Drawing.Point(12, 12);
            this.grpOrder.Name = "grpOrder";
            this.grpOrder.Size = new System.Drawing.Size(168, 88);
            this.grpOrder.TabIndex = 58;
            this.grpOrder.TabStop = false;
            this.grpOrder.Text = "Fechas de filtrado : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Hasta :";
            // 
            // dtpHasta
            // 
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(56, 49);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(97, 20);
            this.dtpHasta.TabIndex = 2;
            this.dtpHasta.ValueChanged += new System.EventHandler(this.dtpHasta_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Desde :";
            // 
            // dtpDesde
            // 
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(56, 18);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(97, 20);
            this.dtpDesde.TabIndex = 1;
            this.dtpDesde.ValueChanged += new System.EventHandler(this.dtpDesde_ValueChanged);
            // 
            // btnPanel
            // 
            this.btnPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPanel.Controls.Add(this.btnImprimir);
            this.btnPanel.Controls.Add(this.btnSalir);
            this.btnPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPanel.Location = new System.Drawing.Point(0, 513);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(942, 58);
            this.btnPanel.TabIndex = 15;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = global::Prama.Recursos.printer;
            this.btnImprimir.Location = new System.Drawing.Point(12, 9);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(45, 40);
            this.btnImprimir.TabIndex = 13;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Image = global::Prama.Recursos.Salir;
            this.btnSalir.Location = new System.Drawing.Point(887, 9);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(45, 40);
            this.btnSalir.TabIndex = 14;
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // dgvArtVtas
            // 
            this.dgvArtVtas.AllowUserToAddRows = false;
            this.dgvArtVtas.AllowUserToDeleteRows = false;
            this.dgvArtVtas.AllowUserToResizeColumns = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvArtVtas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvArtVtas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvArtVtas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CodArt,
            this.Descripcion,
            this.Rubro,
            this.SubRubro,
            this.Cantidad_B,
            this.Cantidad_N,
            this.CantidadVendida});
            this.dgvArtVtas.Location = new System.Drawing.Point(14, 118);
            this.dgvArtVtas.MultiSelect = false;
            this.dgvArtVtas.Name = "dgvArtVtas";
            this.dgvArtVtas.ReadOnly = true;
            this.dgvArtVtas.RowHeadersVisible = false;
            this.dgvArtVtas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvArtVtas.Size = new System.Drawing.Size(918, 337);
            this.dgvArtVtas.StandardTab = true;
            this.dgvArtVtas.TabIndex = 12;
            // 
            // CodArt
            // 
            this.CodArt.DataPropertyName = "CodigoArticulo";
            this.CodArt.HeaderText = "Código";
            this.CodArt.Name = "CodArt";
            this.CodArt.ReadOnly = true;
            this.CodArt.Width = 70;
            // 
            // Descripcion
            // 
            this.Descripcion.DataPropertyName = "Articulo";
            this.Descripcion.HeaderText = "Descripcion";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            this.Descripcion.Width = 260;
            // 
            // Rubro
            // 
            this.Rubro.DataPropertyName = "RubroArticulo";
            this.Rubro.HeaderText = "Rubro";
            this.Rubro.Name = "Rubro";
            this.Rubro.ReadOnly = true;
            this.Rubro.Width = 170;
            // 
            // SubRubro
            // 
            this.SubRubro.DataPropertyName = "SubRubroArticulo";
            this.SubRubro.HeaderText = "SubRubro";
            this.SubRubro.Name = "SubRubro";
            this.SubRubro.ReadOnly = true;
            this.SubRubro.Width = 170;
            // 
            // Cantidad_B
            // 
            this.Cantidad_B.DataPropertyName = "Cantidad_B";
            this.Cantidad_B.DecimalDigits = 2;
            this.Cantidad_B.DecimalSeparator = ".";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Cantidad_B.DefaultCellStyle = dataGridViewCellStyle2;
            this.Cantidad_B.HeaderText = "Cantidad(B)";
            this.Cantidad_B.Name = "Cantidad_B";
            this.Cantidad_B.ReadOnly = true;
            this.Cantidad_B.Width = 75;
            // 
            // Cantidad_N
            // 
            this.Cantidad_N.DataPropertyName = "Cantidad_N";
            this.Cantidad_N.DecimalDigits = 2;
            this.Cantidad_N.DecimalSeparator = ".";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Cantidad_N.DefaultCellStyle = dataGridViewCellStyle3;
            this.Cantidad_N.HeaderText = "Cantidad(N)";
            this.Cantidad_N.Name = "Cantidad_N";
            this.Cantidad_N.ReadOnly = true;
            this.Cantidad_N.Width = 75;
            // 
            // CantidadVendida
            // 
            this.CantidadVendida.DataPropertyName = "Cantidad_Total";
            this.CantidadVendida.DecimalDigits = 0;
            this.CantidadVendida.DecimalSeparator = ".";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CantidadVendida.DefaultCellStyle = dataGridViewCellStyle4;
            this.CantidadVendida.HeaderText = "Total Vendido";
            this.CantidadVendida.Name = "CantidadVendida";
            this.CantidadVendida.ReadOnly = true;
            this.CantidadVendida.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CantidadVendida.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.CantidadVendida.Width = 75;
            // 
            // grpRubroSubRubro
            // 
            this.grpRubroSubRubro.Controls.Add(this.cboSubRubro);
            this.grpRubroSubRubro.Controls.Add(this.cboRubro);
            this.grpRubroSubRubro.Controls.Add(this.label3);
            this.grpRubroSubRubro.Controls.Add(this.lblRubro);
            this.grpRubroSubRubro.Enabled = false;
            this.grpRubroSubRubro.Location = new System.Drawing.Point(186, 12);
            this.grpRubroSubRubro.Name = "grpRubroSubRubro";
            this.grpRubroSubRubro.Size = new System.Drawing.Size(396, 88);
            this.grpRubroSubRubro.TabIndex = 4;
            this.grpRubroSubRubro.TabStop = false;
            this.grpRubroSubRubro.Text = " Rubro / SubRubro ";
            // 
            // cboSubRubro
            // 
            this.cboSubRubro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubRubro.Enabled = false;
            this.cboSubRubro.FormattingEnabled = true;
            this.cboSubRubro.Location = new System.Drawing.Point(73, 49);
            this.cboSubRubro.Name = "cboSubRubro";
            this.cboSubRubro.Size = new System.Drawing.Size(309, 21);
            this.cboSubRubro.TabIndex = 4;
            this.cboSubRubro.SelectionChangeCommitted += new System.EventHandler(this.cboSubRubro_SelectionChangeCommitted);
            // 
            // cboRubro
            // 
            this.cboRubro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRubro.FormattingEnabled = true;
            this.cboRubro.Location = new System.Drawing.Point(73, 19);
            this.cboRubro.Name = "cboRubro";
            this.cboRubro.Size = new System.Drawing.Size(309, 21);
            this.cboRubro.TabIndex = 3;
            this.cboRubro.SelectedValueChanged += new System.EventHandler(this.cboRubro_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "SubRubro:";
            // 
            // lblRubro
            // 
            this.lblRubro.AutoSize = true;
            this.lblRubro.Location = new System.Drawing.Point(28, 23);
            this.lblRubro.Name = "lblRubro";
            this.lblRubro.Size = new System.Drawing.Size(39, 13);
            this.lblRubro.TabIndex = 5;
            this.lblRubro.Text = "Rubro:";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Producto";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Descripcion";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 250;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Rubro";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 175;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "SubRubro";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 175;
            // 
            // numericGridColumn1
            // 
            this.numericGridColumn1.DecimalDigits = 0;
            this.numericGridColumn1.DecimalSeparator = ".";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.numericGridColumn1.DefaultCellStyle = dataGridViewCellStyle5;
            this.numericGridColumn1.HeaderText = "Cantidad Vendida";
            this.numericGridColumn1.Name = "numericGridColumn1";
            this.numericGridColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.numericGridColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.numericGridColumn1.Width = 120;
            // 
            // rdbVsitaGral
            // 
            this.rdbVsitaGral.AutoSize = true;
            this.rdbVsitaGral.Checked = true;
            this.rdbVsitaGral.Location = new System.Drawing.Point(777, 24);
            this.rdbVsitaGral.Name = "rdbVsitaGral";
            this.rdbVsitaGral.Size = new System.Drawing.Size(88, 17);
            this.rdbVsitaGral.TabIndex = 9;
            this.rdbVsitaGral.TabStop = true;
            this.rdbVsitaGral.Text = "Vista General";
            this.rdbVsitaGral.UseVisualStyleBackColor = true;
            this.rdbVsitaGral.CheckedChanged += new System.EventHandler(this.rdbVsitaGral_CheckedChanged);
            // 
            // rdbRubSub
            // 
            this.rdbRubSub.AutoSize = true;
            this.rdbRubSub.Location = new System.Drawing.Point(777, 51);
            this.rdbRubSub.Name = "rdbRubSub";
            this.rdbRubSub.Size = new System.Drawing.Size(107, 17);
            this.rdbRubSub.TabIndex = 10;
            this.rdbRubSub.Text = "Rubro/SubRubro";
            this.rdbRubSub.UseVisualStyleBackColor = true;
            this.rdbRubSub.CheckedChanged += new System.EventHandler(this.rdbRubSub_CheckedChanged);
            // 
            // rdbCli
            // 
            this.rdbCli.AutoSize = true;
            this.rdbCli.Location = new System.Drawing.Point(777, 79);
            this.rdbCli.Name = "rdbCli";
            this.rdbCli.Size = new System.Drawing.Size(57, 17);
            this.rdbCli.TabIndex = 11;
            this.rdbCli.Text = "Cliente";
            this.rdbCli.UseVisualStyleBackColor = true;
            this.rdbCli.CheckedChanged += new System.EventHandler(this.rdbCli_CheckedChanged);
            // 
            // btnCli
            // 
            this.btnCli.Image = global::Prama.Recursos.vista;
            this.btnCli.Location = new System.Drawing.Point(112, 31);
            this.btnCli.Name = "btnCli";
            this.btnCli.Size = new System.Drawing.Size(25, 25);
            this.btnCli.TabIndex = 6;
            this.btnCli.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCli.UseVisualStyleBackColor = true;
            this.btnCli.Click += new System.EventHandler(this.btnCli_Click);
            // 
            // txtCuit
            // 
            this.txtCuit.Enabled = false;
            this.txtCuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCuit.Location = new System.Drawing.Point(19, 36);
            this.txtCuit.MaxLength = 13;
            this.txtCuit.Name = "txtCuit";
            this.txtCuit.Size = new System.Drawing.Size(87, 20);
            this.txtCuit.TabIndex = 5;
            this.txtCuit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCUIT
            // 
            this.lblCUIT.AutoSize = true;
            this.lblCUIT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCUIT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCUIT.Location = new System.Drawing.Point(16, 20);
            this.lblCUIT.Name = "lblCUIT";
            this.lblCUIT.Size = new System.Drawing.Size(38, 13);
            this.lblCUIT.TabIndex = 11;
            this.lblCUIT.Text = "CUIT :";
            // 
            // rdbPub
            // 
            this.rdbPub.AutoSize = true;
            this.rdbPub.Checked = true;
            this.rdbPub.Location = new System.Drawing.Point(19, 65);
            this.rdbPub.Name = "rdbPub";
            this.rdbPub.Size = new System.Drawing.Size(60, 17);
            this.rdbPub.TabIndex = 7;
            this.rdbPub.TabStop = true;
            this.rdbPub.Text = "Público";
            this.rdbPub.UseVisualStyleBackColor = true;
            this.rdbPub.Visible = false;
            // 
            // grpClientes
            // 
            this.grpClientes.Controls.Add(this.radioButton1);
            this.grpClientes.Controls.Add(this.rdbPub);
            this.grpClientes.Controls.Add(this.lblCUIT);
            this.grpClientes.Controls.Add(this.txtCuit);
            this.grpClientes.Controls.Add(this.btnCli);
            this.grpClientes.Enabled = false;
            this.grpClientes.Location = new System.Drawing.Point(588, 12);
            this.grpClientes.Name = "grpClientes";
            this.grpClientes.Size = new System.Drawing.Size(179, 88);
            this.grpClientes.TabIndex = 9;
            this.grpClientes.TabStop = false;
            this.grpClientes.Text = " Cliente: ";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(85, 65);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(84, 17);
            this.radioButton1.TabIndex = 8;
            this.radioButton1.Text = "Revendedor";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Visible = false;
            // 
            // txtTotal
            // 
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(821, 471);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(107, 20);
            this.txtTotal.TabIndex = 60;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(774, 474);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 59;
            this.label9.Text = "Total :";
            // 
            // frmInformeVtaArticulos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 571);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.rdbCli);
            this.Controls.Add(this.rdbRubSub);
            this.Controls.Add(this.rdbVsitaGral);
            this.Controls.Add(this.grpClientes);
            this.Controls.Add(this.grpRubroSubRubro);
            this.Controls.Add(this.dgvArtVtas);
            this.Controls.Add(this.btnPanel);
            this.Controls.Add(this.grpOrder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInformeVtaArticulos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " - INFORME VENTAS DE ARTICULOS ";
            this.Load += new System.EventHandler(this.frmInformeVtaArticulos_Load);
            this.grpOrder.ResumeLayout(false);
            this.grpOrder.PerformLayout();
            this.btnPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvArtVtas)).EndInit();
            this.grpRubroSubRubro.ResumeLayout(false);
            this.grpRubroSubRubro.PerformLayout();
            this.grpClientes.ResumeLayout(false);
            this.grpClientes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpOrder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Panel btnPanel;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.DataGridView dgvArtVtas;
        private System.Windows.Forms.GroupBox grpRubroSubRubro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRubro;
        private System.Windows.Forms.ComboBox cboRubro;
        private System.Windows.Forms.ComboBox cboSubRubro;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private NumericGridColumn numericGridColumn1;
        private System.Windows.Forms.RadioButton rdbVsitaGral;
        private System.Windows.Forms.RadioButton rdbRubSub;
        private System.Windows.Forms.RadioButton rdbCli;
        private System.Windows.Forms.Button btnCli;
        private System.Windows.Forms.TextBox txtCuit;
        private System.Windows.Forms.Label lblCUIT;
        private System.Windows.Forms.RadioButton rdbPub;
        private System.Windows.Forms.GroupBox grpClientes;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodArt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rubro;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubRubro;
        private NumericGridColumn Cantidad_B;
        private NumericGridColumn Cantidad_N;
        private NumericGridColumn CantidadVendida;
    }
}
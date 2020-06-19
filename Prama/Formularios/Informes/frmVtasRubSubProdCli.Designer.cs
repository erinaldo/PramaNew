namespace Prama.Formularios.Informes
{
    partial class frmVtasRubSubProdCli
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVtasRubSubProdCli));
            this.gpbRango = new System.Windows.Forms.GroupBox();
            this.dtHasta = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtDesde = new System.Windows.Forms.DateTimePicker();
            this.lblDesde = new System.Windows.Forms.Label();
            this.gpbTipoCli = new System.Windows.Forms.GroupBox();
            this.rbnDist = new System.Windows.Forms.RadioButton();
            this.rbnRev = new System.Windows.Forms.RadioButton();
            this.rbnPub = new System.Windows.Forms.RadioButton();
            this.rbnAll = new System.Windows.Forms.RadioButton();
            this.dvgData = new System.Windows.Forms.DataGridView();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdRubroArticulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RubroArticulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdSubRubroArticulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubRubroArticulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Articulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RazonSocial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new Prama.NumericGridColumn();
            this.Total = new Prama.NumericGridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.grpRubroSubRubro = new System.Windows.Forms.GroupBox();
            this.btnCli = new System.Windows.Forms.Button();
            this.cboSubRubro = new System.Windows.Forms.ComboBox();
            this.cboRubro = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRubro = new System.Windows.Forms.Label();
            this.gpbClienteId = new System.Windows.Forms.GroupBox();
            this.txtCodigoBs = new System.Windows.Forms.TextBox();
            this.lblIdCli = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCantTotal = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtTotGral = new System.Windows.Forms.TextBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numericGridColumn1 = new Prama.NumericGridColumn();
            this.numericGridColumn2 = new Prama.NumericGridColumn();
            this.cboProvincia = new System.Windows.Forms.ComboBox();
            this.lblProvincia = new System.Windows.Forms.Label();
            this.gpbRango.SuspendLayout();
            this.gpbTipoCli.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgData)).BeginInit();
            this.panel1.SuspendLayout();
            this.grpRubroSubRubro.SuspendLayout();
            this.gpbClienteId.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpbRango
            // 
            this.gpbRango.Controls.Add(this.dtHasta);
            this.gpbRango.Controls.Add(this.label1);
            this.gpbRango.Controls.Add(this.dtDesde);
            this.gpbRango.Controls.Add(this.lblDesde);
            this.gpbRango.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbRango.Location = new System.Drawing.Point(711, 12);
            this.gpbRango.Name = "gpbRango";
            this.gpbRango.Size = new System.Drawing.Size(174, 84);
            this.gpbRango.TabIndex = 7;
            this.gpbRango.TabStop = false;
            this.gpbRango.Text = " Desde-Hasta ";
            // 
            // dtHasta
            // 
            this.dtHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtHasta.Location = new System.Drawing.Point(59, 51);
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
            this.label1.Location = new System.Drawing.Point(15, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Hasta:";
            // 
            // dtDesde
            // 
            this.dtDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDesde.Location = new System.Drawing.Point(59, 28);
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
            this.lblDesde.Location = new System.Drawing.Point(12, 30);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(41, 13);
            this.lblDesde.TabIndex = 6;
            this.lblDesde.Text = "Desde:";
            // 
            // gpbTipoCli
            // 
            this.gpbTipoCli.Controls.Add(this.rbnDist);
            this.gpbTipoCli.Controls.Add(this.rbnRev);
            this.gpbTipoCli.Controls.Add(this.rbnPub);
            this.gpbTipoCli.Controls.Add(this.rbnAll);
            this.gpbTipoCli.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbTipoCli.Location = new System.Drawing.Point(12, 12);
            this.gpbTipoCli.Name = "gpbTipoCli";
            this.gpbTipoCli.Size = new System.Drawing.Size(185, 84);
            this.gpbTipoCli.TabIndex = 6;
            this.gpbTipoCli.TabStop = false;
            this.gpbTipoCli.Text = " Tipo Cliente ";
            // 
            // rbnDist
            // 
            this.rbnDist.AutoSize = true;
            this.rbnDist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnDist.Location = new System.Drawing.Point(83, 28);
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
            this.rbnRev.Location = new System.Drawing.Point(83, 51);
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
            this.rbnPub.Location = new System.Drawing.Point(17, 51);
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
            this.rbnAll.Location = new System.Drawing.Point(17, 28);
            this.rbnAll.Name = "rbnAll";
            this.rbnAll.Size = new System.Drawing.Size(58, 17);
            this.rbnAll.TabIndex = 1;
            this.rbnAll.TabStop = true;
            this.rbnAll.Text = " Todos";
            this.rbnAll.UseVisualStyleBackColor = true;
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
            this.Fecha,
            this.IdFactura,
            this.IdProducto,
            this.IdRubroArticulo,
            this.RubroArticulo,
            this.IdSubRubroArticulo,
            this.SubRubroArticulo,
            this.Articulo,
            this.IdCliente,
            this.RazonSocial,
            this.Cantidad,
            this.Total});
            this.dvgData.Location = new System.Drawing.Point(12, 113);
            this.dvgData.Name = "dvgData";
            this.dvgData.RowHeadersVisible = false;
            this.dvgData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dvgData.Size = new System.Drawing.Size(872, 268);
            this.dvgData.StandardTab = true;
            this.dvgData.TabIndex = 11;
            // 
            // Fecha
            // 
            this.Fecha.DataPropertyName = "Fecha";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Fecha.DefaultCellStyle = dataGridViewCellStyle2;
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.Width = 80;
            // 
            // IdFactura
            // 
            this.IdFactura.DataPropertyName = "IdFactura";
            this.IdFactura.HeaderText = "IdFactura";
            this.IdFactura.Name = "IdFactura";
            this.IdFactura.Visible = false;
            // 
            // IdProducto
            // 
            this.IdProducto.DataPropertyName = "IdProducto";
            this.IdProducto.HeaderText = "IdProducto";
            this.IdProducto.Name = "IdProducto";
            this.IdProducto.Visible = false;
            // 
            // IdRubroArticulo
            // 
            this.IdRubroArticulo.DataPropertyName = "IdRubroArticulo";
            this.IdRubroArticulo.HeaderText = "IdRubroArticulo";
            this.IdRubroArticulo.Name = "IdRubroArticulo";
            this.IdRubroArticulo.Visible = false;
            // 
            // RubroArticulo
            // 
            this.RubroArticulo.DataPropertyName = "RubroArticulo";
            this.RubroArticulo.HeaderText = "Rubro Articulo";
            this.RubroArticulo.Name = "RubroArticulo";
            this.RubroArticulo.Width = 130;
            // 
            // IdSubRubroArticulo
            // 
            this.IdSubRubroArticulo.DataPropertyName = "IdSubrubroArticulo";
            this.IdSubRubroArticulo.HeaderText = "IdSubRubroArticulo";
            this.IdSubRubroArticulo.Name = "IdSubRubroArticulo";
            this.IdSubRubroArticulo.Visible = false;
            // 
            // SubRubroArticulo
            // 
            this.SubRubroArticulo.DataPropertyName = "SubrubroArticulo";
            this.SubRubroArticulo.HeaderText = "SubRubroArticulo";
            this.SubRubroArticulo.Name = "SubRubroArticulo";
            // 
            // Articulo
            // 
            this.Articulo.DataPropertyName = "Articulo";
            this.Articulo.HeaderText = "Articulo";
            this.Articulo.Name = "Articulo";
            this.Articulo.Width = 150;
            // 
            // IdCliente
            // 
            this.IdCliente.DataPropertyName = "IdCliente";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.IdCliente.DefaultCellStyle = dataGridViewCellStyle3;
            this.IdCliente.HeaderText = "IdCliente";
            this.IdCliente.Name = "IdCliente";
            this.IdCliente.Width = 75;
            // 
            // RazonSocial
            // 
            this.RazonSocial.DataPropertyName = "RazonSocial";
            this.RazonSocial.HeaderText = "Razon Social";
            this.RazonSocial.Name = "RazonSocial";
            this.RazonSocial.Width = 120;
            // 
            // Cantidad
            // 
            this.Cantidad.DataPropertyName = "Cantidad";
            this.Cantidad.DecimalDigits = 2;
            this.Cantidad.DecimalSeparator = ".";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Cantidad.DefaultCellStyle = dataGridViewCellStyle4;
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Cantidad.Width = 90;
            // 
            // Total
            // 
            this.Total.DataPropertyName = "Total";
            this.Total.DecimalDigits = 2;
            this.Total.DecimalSeparator = ".";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Total.DefaultCellStyle = dataGridViewCellStyle5;
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            this.Total.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Total.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Total.Width = 90;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.btnImprimir);
            this.panel1.Controls.Add(this.btnSalir);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 454);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(897, 58);
            this.panel1.TabIndex = 14;
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
            this.btnSalir.Location = new System.Drawing.Point(840, 9);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(45, 40);
            this.btnSalir.TabIndex = 15;
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // grpRubroSubRubro
            // 
            this.grpRubroSubRubro.Controls.Add(this.btnCli);
            this.grpRubroSubRubro.Controls.Add(this.cboSubRubro);
            this.grpRubroSubRubro.Controls.Add(this.cboRubro);
            this.grpRubroSubRubro.Controls.Add(this.label3);
            this.grpRubroSubRubro.Controls.Add(this.lblRubro);
            this.grpRubroSubRubro.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpRubroSubRubro.Location = new System.Drawing.Point(203, 12);
            this.grpRubroSubRubro.Name = "grpRubroSubRubro";
            this.grpRubroSubRubro.Size = new System.Drawing.Size(502, 84);
            this.grpRubroSubRubro.TabIndex = 15;
            this.grpRubroSubRubro.TabStop = false;
            this.grpRubroSubRubro.Text = " Rubro / SubRubro ";
            // 
            // btnCli
            // 
            this.btnCli.Image = ((System.Drawing.Image)(resources.GetObject("btnCli.Image")));
            this.btnCli.Location = new System.Drawing.Point(451, 23);
            this.btnCli.Name = "btnCli";
            this.btnCli.Size = new System.Drawing.Size(45, 44);
            this.btnCli.TabIndex = 8;
            this.btnCli.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCli.UseVisualStyleBackColor = true;
            this.btnCli.Click += new System.EventHandler(this.btnCli_Click);
            // 
            // cboSubRubro
            // 
            this.cboSubRubro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubRubro.Enabled = false;
            this.cboSubRubro.FormattingEnabled = true;
            this.cboSubRubro.Location = new System.Drawing.Point(73, 49);
            this.cboSubRubro.Name = "cboSubRubro";
            this.cboSubRubro.Size = new System.Drawing.Size(364, 21);
            this.cboSubRubro.TabIndex = 4;
            this.cboSubRubro.SelectedValueChanged += new System.EventHandler(this.cboSubRubro_SelectedValueChanged);
            // 
            // cboRubro
            // 
            this.cboRubro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRubro.FormattingEnabled = true;
            this.cboRubro.Location = new System.Drawing.Point(73, 19);
            this.cboRubro.Name = "cboRubro";
            this.cboRubro.Size = new System.Drawing.Size(364, 21);
            this.cboRubro.TabIndex = 3;
            this.cboRubro.SelectedValueChanged += new System.EventHandler(this.cboRubro_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "SubRubro:";
            // 
            // lblRubro
            // 
            this.lblRubro.AutoSize = true;
            this.lblRubro.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRubro.Location = new System.Drawing.Point(28, 23);
            this.lblRubro.Name = "lblRubro";
            this.lblRubro.Size = new System.Drawing.Size(39, 13);
            this.lblRubro.TabIndex = 5;
            this.lblRubro.Text = "Rubro:";
            // 
            // gpbClienteId
            // 
            this.gpbClienteId.Controls.Add(this.txtCodigoBs);
            this.gpbClienteId.Controls.Add(this.lblIdCli);
            this.gpbClienteId.Location = new System.Drawing.Point(12, 381);
            this.gpbClienteId.Name = "gpbClienteId";
            this.gpbClienteId.Size = new System.Drawing.Size(163, 67);
            this.gpbClienteId.TabIndex = 16;
            this.gpbClienteId.TabStop = false;
            // 
            // txtCodigoBs
            // 
            this.txtCodigoBs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoBs.Location = new System.Drawing.Point(17, 35);
            this.txtCodigoBs.MaxLength = 10;
            this.txtCodigoBs.Name = "txtCodigoBs";
            this.txtCodigoBs.Size = new System.Drawing.Size(124, 20);
            this.txtCodigoBs.TabIndex = 22;
            this.txtCodigoBs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCodigoBs.TextChanged += new System.EventHandler(this.txtCodigoBs_TextChanged);
            // 
            // lblIdCli
            // 
            this.lblIdCli.BackColor = System.Drawing.SystemColors.Control;
            this.lblIdCli.Location = new System.Drawing.Point(16, 13);
            this.lblIdCli.Name = "lblIdCli";
            this.lblIdCli.Size = new System.Drawing.Size(61, 16);
            this.lblIdCli.TabIndex = 17;
            this.lblIdCli.Text = "Id Cliente: ";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(581, 390);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 21);
            this.label2.TabIndex = 19;
            this.label2.Text = "Total Cantidad:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCantTotal
            // 
            this.txtCantTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantTotal.Location = new System.Drawing.Point(584, 414);
            this.txtCantTotal.MaxLength = 13;
            this.txtCantTotal.Name = "txtCantTotal";
            this.txtCantTotal.ReadOnly = true;
            this.txtCantTotal.Size = new System.Drawing.Size(134, 22);
            this.txtCantTotal.TabIndex = 20;
            this.txtCantTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(736, 390);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(109, 21);
            this.lblTotal.TabIndex = 17;
            this.lblTotal.Text = "Total Gral. $:";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTotGral
            // 
            this.txtTotGral.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotGral.Location = new System.Drawing.Point(739, 414);
            this.txtTotGral.MaxLength = 13;
            this.txtTotGral.Name = "txtTotGral";
            this.txtTotGral.ReadOnly = true;
            this.txtTotGral.Size = new System.Drawing.Size(134, 22);
            this.txtTotGral.TabIndex = 18;
            this.txtTotGral.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "IdFactura";
            this.dataGridViewTextBoxColumn1.HeaderText = "IdFactura";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "IdProducto";
            this.dataGridViewTextBoxColumn2.HeaderText = "IdProducto";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "IdRubroArticulo";
            this.dataGridViewTextBoxColumn3.HeaderText = "IdRubroArticulo";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Visible = false;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "RubroArticulo";
            this.dataGridViewTextBoxColumn4.HeaderText = "Rubro Articulo";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "IdSubrubroArticulo";
            this.dataGridViewTextBoxColumn5.HeaderText = "IdSubRubroArticulo";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Visible = false;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "SubrubroArticulo";
            this.dataGridViewTextBoxColumn6.HeaderText = "SubRubroArticulo";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 130;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Articulo";
            this.dataGridViewTextBoxColumn7.HeaderText = "Articulo";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 150;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "IdCliente";
            this.dataGridViewTextBoxColumn8.HeaderText = "IdCliente";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "RazonSocial";
            this.dataGridViewTextBoxColumn9.HeaderText = "Razon Social";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "Mes";
            this.dataGridViewTextBoxColumn10.HeaderText = "Mes";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.Visible = false;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "Año";
            this.dataGridViewTextBoxColumn11.HeaderText = "Año";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.Visible = false;
            // 
            // numericGridColumn1
            // 
            this.numericGridColumn1.DataPropertyName = "Cantidad";
            this.numericGridColumn1.DecimalDigits = 2;
            this.numericGridColumn1.DecimalSeparator = ".";
            this.numericGridColumn1.HeaderText = "Cantidad";
            this.numericGridColumn1.Name = "numericGridColumn1";
            this.numericGridColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.numericGridColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // numericGridColumn2
            // 
            this.numericGridColumn2.DataPropertyName = "Total";
            this.numericGridColumn2.DecimalDigits = 2;
            this.numericGridColumn2.DecimalSeparator = ".";
            this.numericGridColumn2.HeaderText = "Total";
            this.numericGridColumn2.Name = "numericGridColumn2";
            this.numericGridColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.numericGridColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // cboProvincia
            // 
            this.cboProvincia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProvincia.FormattingEnabled = true;
            this.cboProvincia.Location = new System.Drawing.Point(181, 416);
            this.cboProvincia.Name = "cboProvincia";
            this.cboProvincia.Size = new System.Drawing.Size(255, 21);
            this.cboProvincia.TabIndex = 27;
            this.cboProvincia.SelectedValueChanged += new System.EventHandler(this.cboProvincia_SelectedValueChanged);
            // 
            // lblProvincia
            // 
            this.lblProvincia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProvincia.Location = new System.Drawing.Point(178, 391);
            this.lblProvincia.Name = "lblProvincia";
            this.lblProvincia.Size = new System.Drawing.Size(58, 21);
            this.lblProvincia.TabIndex = 28;
            this.lblProvincia.Text = "Provincia:";
            this.lblProvincia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmVtasRubSubProdCli
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 512);
            this.Controls.Add(this.cboProvincia);
            this.Controls.Add(this.lblProvincia);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCantTotal);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.txtTotGral);
            this.Controls.Add(this.gpbClienteId);
            this.Controls.Add(this.grpRubroSubRubro);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dvgData);
            this.Controls.Add(this.gpbRango);
            this.Controls.Add(this.gpbTipoCli);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVtasRubSubProdCli";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " - DETALLE DE VENTAS POR TIPO CLIENTE, RUBRO-SUBRUBRO Y PERIODO";
            this.Load += new System.EventHandler(this.frmVtasRubSubProdCli_Load);
            this.gpbRango.ResumeLayout(false);
            this.gpbRango.PerformLayout();
            this.gpbTipoCli.ResumeLayout(false);
            this.gpbTipoCli.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgData)).EndInit();
            this.panel1.ResumeLayout(false);
            this.grpRubroSubRubro.ResumeLayout(false);
            this.grpRubroSubRubro.PerformLayout();
            this.gpbClienteId.ResumeLayout(false);
            this.gpbClienteId.PerformLayout();
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
        private System.Windows.Forms.DataGridView dvgData;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.GroupBox grpRubroSubRubro;
        private System.Windows.Forms.ComboBox cboSubRubro;
        private System.Windows.Forms.ComboBox cboRubro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRubro;
        private System.Windows.Forms.GroupBox gpbClienteId;
        private System.Windows.Forms.Label lblIdCli;
        private System.Windows.Forms.TextBox txtCodigoBs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCantTotal;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtTotGral;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private NumericGridColumn numericGridColumn1;
        private NumericGridColumn numericGridColumn2;
        private System.Windows.Forms.Button btnCli;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdFactura;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdRubroArticulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn RubroArticulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdSubRubroArticulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubRubroArticulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Articulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn RazonSocial;
        private NumericGridColumn Cantidad;
        private NumericGridColumn Total;
        private System.Windows.Forms.ComboBox cboProvincia;
        private System.Windows.Forms.Label lblProvincia;
    }
}
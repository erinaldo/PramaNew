namespace Prama.Formularios.Caja
{
    partial class frmCajaBancos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCajaBancos));
            this.cboCajaAs = new System.Windows.Forms.ComboBox();
            this.btnPanel = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.lblCajaAs = new System.Windows.Forms.Label();
            this.lblSaldo = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numericGridColumn1 = new Prama.NumericGridColumn();
            this.numericGridColumn2 = new Prama.NumericGridColumn();
            this.numericGridColumn3 = new Prama.NumericGridColumn();
            this.numericGridColumn4 = new Prama.NumericGridColumn();
            this.numericGridColumn5 = new Prama.NumericGridColumn();
            this.gpbBusquedas = new System.Windows.Forms.GroupBox();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.optOtros = new System.Windows.Forms.RadioButton();
            this.optCred = new System.Windows.Forms.RadioButton();
            this.optDeb = new System.Windows.Forms.RadioButton();
            this.optMP = new System.Windows.Forms.RadioButton();
            this.optTransfer = new System.Windows.Forms.RadioButton();
            this.txtBancos = new System.Windows.Forms.TextBox();
            this.lblTotalBcos = new System.Windows.Forms.Label();
            this.txtSaldo = new System.Windows.Forms.TextBox();
            this.dsReportes1 = new Prama.dsReportes();
            this.dgvCaja = new System.Windows.Forms.DataGridView();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cuenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Movimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Transferencia = new Prama.NumericGridColumn();
            this.MercadoPago = new Prama.NumericGridColumn();
            this.Debito = new Prama.NumericGridColumn();
            this.Credito = new Prama.NumericGridColumn();
            this.Total = new Prama.NumericGridColumn();
            this.btnPanel.SuspendLayout();
            this.gpbBusquedas.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsReportes1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCaja)).BeginInit();
            this.SuspendLayout();
            // 
            // cboCajaAs
            // 
            this.cboCajaAs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCajaAs.FormattingEnabled = true;
            this.cboCajaAs.Location = new System.Drawing.Point(65, 12);
            this.cboCajaAs.Name = "cboCajaAs";
            this.cboCajaAs.Size = new System.Drawing.Size(276, 21);
            this.cboCajaAs.TabIndex = 5;
            this.cboCajaAs.SelectedIndexChanged += new System.EventHandler(this.cboCajaAs_SelectedIndexChanged);
            // 
            // btnPanel
            // 
            this.btnPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPanel.Controls.Add(this.btnSalir);
            this.btnPanel.Controls.Add(this.btnImprimir);
            this.btnPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPanel.Location = new System.Drawing.Point(0, 492);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(884, 58);
            this.btnPanel.TabIndex = 65;
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Image = global::Prama.Recursos.Salir;
            this.btnSalir.Location = new System.Drawing.Point(830, 9);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(45, 40);
            this.btnSalir.TabIndex = 31;
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = global::Prama.Recursos.printer;
            this.btnImprimir.Location = new System.Drawing.Point(12, 9);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(45, 40);
            this.btnImprimir.TabIndex = 15;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // lblCajaAs
            // 
            this.lblCajaAs.AutoSize = true;
            this.lblCajaAs.Location = new System.Drawing.Point(15, 15);
            this.lblCajaAs.Name = "lblCajaAs";
            this.lblCajaAs.Size = new System.Drawing.Size(44, 13);
            this.lblCajaAs.TabIndex = 6;
            this.lblCajaAs.Text = "Cuenta:";
            // 
            // lblSaldo
            // 
            this.lblSaldo.AutoSize = true;
            this.lblSaldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaldo.Location = new System.Drawing.Point(565, 419);
            this.lblSaldo.Name = "lblSaldo";
            this.lblSaldo.Size = new System.Drawing.Size(110, 17);
            this.lblSaldo.TabIndex = 8;
            this.lblSaldo.Text = "Saldo Cuenta:";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "FechaS";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn1.HeaderText = "Fecha";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "CajaAsociaciones";
            this.dataGridViewTextBoxColumn2.HeaderText = "Cuenta";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // numericGridColumn1
            // 
            this.numericGridColumn1.DataPropertyName = "Transferencia";
            this.numericGridColumn1.DecimalDigits = 2;
            this.numericGridColumn1.DecimalSeparator = ".";
            this.numericGridColumn1.HeaderText = "Transferencia";
            this.numericGridColumn1.Name = "numericGridColumn1";
            this.numericGridColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.numericGridColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // numericGridColumn2
            // 
            this.numericGridColumn2.DataPropertyName = "MP";
            this.numericGridColumn2.DecimalDigits = 2;
            this.numericGridColumn2.DecimalSeparator = ".";
            this.numericGridColumn2.HeaderText = "MercadoPago";
            this.numericGridColumn2.Name = "numericGridColumn2";
            this.numericGridColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.numericGridColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // numericGridColumn3
            // 
            this.numericGridColumn3.DataPropertyName = "Debito";
            this.numericGridColumn3.DecimalDigits = 2;
            this.numericGridColumn3.DecimalSeparator = ".";
            this.numericGridColumn3.HeaderText = "Debito";
            this.numericGridColumn3.Name = "numericGridColumn3";
            this.numericGridColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.numericGridColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // numericGridColumn4
            // 
            this.numericGridColumn4.DataPropertyName = "Credito";
            this.numericGridColumn4.DecimalDigits = 2;
            this.numericGridColumn4.DecimalSeparator = ".";
            this.numericGridColumn4.HeaderText = "Credito";
            this.numericGridColumn4.Name = "numericGridColumn4";
            this.numericGridColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.numericGridColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // numericGridColumn5
            // 
            this.numericGridColumn5.DataPropertyName = "Total";
            this.numericGridColumn5.DecimalDigits = 2;
            this.numericGridColumn5.DecimalSeparator = ".";
            this.numericGridColumn5.HeaderText = "Total";
            this.numericGridColumn5.Name = "numericGridColumn5";
            this.numericGridColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.numericGridColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // gpbBusquedas
            // 
            this.gpbBusquedas.Controls.Add(this.txtFecha);
            this.gpbBusquedas.Controls.Add(this.lblFecha);
            this.gpbBusquedas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbBusquedas.Location = new System.Drawing.Point(9, 415);
            this.gpbBusquedas.Name = "gpbBusquedas";
            this.gpbBusquedas.Size = new System.Drawing.Size(157, 62);
            this.gpbBusquedas.TabIndex = 68;
            this.gpbBusquedas.TabStop = false;
            this.gpbBusquedas.Text = "Búsquedas :";
            // 
            // txtFecha
            // 
            this.txtFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFecha.Location = new System.Drawing.Point(55, 24);
            this.txtFecha.MaxLength = 25;
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(88, 20);
            this.txtFecha.TabIndex = 47;
            this.txtFecha.TextChanged += new System.EventHandler(this.txtFecha_TextChanged);
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFecha.Location = new System.Drawing.Point(6, 27);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(43, 13);
            this.lblFecha.TabIndex = 46;
            this.lblFecha.Text = "Fecha: ";
            // 
            // button1
            // 
            this.button1.Image = global::Prama.Recursos.cancel;
            this.button1.Location = new System.Drawing.Point(498, 437);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(45, 40);
            this.button1.TabIndex = 32;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.optOtros);
            this.groupBox1.Controls.Add(this.optCred);
            this.groupBox1.Controls.Add(this.optDeb);
            this.groupBox1.Controls.Add(this.optMP);
            this.groupBox1.Controls.Add(this.optTransfer);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(176, 415);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(316, 62);
            this.groupBox1.TabIndex = 69;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Tipo Movimineto";
            // 
            // optOtros
            // 
            this.optOtros.AutoSize = true;
            this.optOtros.Location = new System.Drawing.Point(255, 29);
            this.optOtros.Name = "optOtros";
            this.optOtros.Size = new System.Drawing.Size(53, 17);
            this.optOtros.TabIndex = 4;
            this.optOtros.Text = "Mov.";
            this.optOtros.UseVisualStyleBackColor = true;
            this.optOtros.CheckedChanged += new System.EventHandler(this.optOtros_CheckedChanged);
            // 
            // optCred
            // 
            this.optCred.AutoSize = true;
            this.optCred.Location = new System.Drawing.Point(195, 29);
            this.optCred.Name = "optCred";
            this.optCred.Size = new System.Drawing.Size(55, 17);
            this.optCred.TabIndex = 3;
            this.optCred.Text = "Créd.";
            this.optCred.UseVisualStyleBackColor = true;
            this.optCred.CheckedChanged += new System.EventHandler(this.optCred_CheckedChanged);
            // 
            // optDeb
            // 
            this.optDeb.AutoSize = true;
            this.optDeb.Location = new System.Drawing.Point(137, 29);
            this.optDeb.Name = "optDeb";
            this.optDeb.Size = new System.Drawing.Size(52, 17);
            this.optDeb.TabIndex = 2;
            this.optDeb.Text = "Déb.";
            this.optDeb.UseVisualStyleBackColor = true;
            this.optDeb.CheckedChanged += new System.EventHandler(this.optDeb_CheckedChanged);
            // 
            // optMP
            // 
            this.optMP.AutoSize = true;
            this.optMP.Location = new System.Drawing.Point(88, 29);
            this.optMP.Name = "optMP";
            this.optMP.Size = new System.Drawing.Size(43, 17);
            this.optMP.TabIndex = 1;
            this.optMP.Text = "MP";
            this.optMP.UseVisualStyleBackColor = true;
            this.optMP.CheckedChanged += new System.EventHandler(this.optMP_CheckedChanged);
            // 
            // optTransfer
            // 
            this.optTransfer.AutoSize = true;
            this.optTransfer.Checked = true;
            this.optTransfer.Location = new System.Drawing.Point(6, 29);
            this.optTransfer.Name = "optTransfer";
            this.optTransfer.Size = new System.Drawing.Size(76, 17);
            this.optTransfer.TabIndex = 0;
            this.optTransfer.TabStop = true;
            this.optTransfer.Text = "Transfer.";
            this.optTransfer.UseVisualStyleBackColor = true;
            this.optTransfer.CheckedChanged += new System.EventHandler(this.optTransfer_CheckedChanged);
            // 
            // txtBancos
            // 
            this.txtBancos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBancos.Location = new System.Drawing.Point(717, 439);
            this.txtBancos.MaxLength = 70;
            this.txtBancos.Name = "txtBancos";
            this.txtBancos.Size = new System.Drawing.Size(143, 22);
            this.txtBancos.TabIndex = 80;
            this.txtBancos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotalBcos
            // 
            this.lblTotalBcos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBcos.Location = new System.Drawing.Point(714, 419);
            this.lblTotalBcos.Name = "lblTotalBcos";
            this.lblTotalBcos.Size = new System.Drawing.Size(110, 17);
            this.lblTotalBcos.TabIndex = 81;
            this.lblTotalBcos.Text = "Total Bancos:";
            this.lblTotalBcos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSaldo
            // 
            this.txtSaldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaldo.Location = new System.Drawing.Point(568, 439);
            this.txtSaldo.MaxLength = 70;
            this.txtSaldo.Name = "txtSaldo";
            this.txtSaldo.Size = new System.Drawing.Size(143, 22);
            this.txtSaldo.TabIndex = 82;
            this.txtSaldo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dsReportes1
            // 
            this.dsReportes1.DataSetName = "dsReportes";
            this.dsReportes1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dgvCaja
            // 
            this.dgvCaja.AllowUserToAddRows = false;
            this.dgvCaja.AllowUserToDeleteRows = false;
            this.dgvCaja.AllowUserToResizeColumns = false;
            this.dgvCaja.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvCaja.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCaja.ColumnHeadersHeight = 21;
            this.dgvCaja.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCaja.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Fecha,
            this.Cuenta,
            this.Movimiento,
            this.Transferencia,
            this.MercadoPago,
            this.Debito,
            this.Credito,
            this.Total});
            this.dgvCaja.Location = new System.Drawing.Point(9, 51);
            this.dgvCaja.Name = "dgvCaja";
            this.dgvCaja.ReadOnly = true;
            this.dgvCaja.RowHeadersVisible = false;
            this.dgvCaja.RowHeadersWidth = 20;
            this.dgvCaja.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCaja.Size = new System.Drawing.Size(866, 336);
            this.dgvCaja.StandardTab = true;
            this.dgvCaja.TabIndex = 4;
            // 
            // Fecha
            // 
            this.Fecha.DataPropertyName = "FechaS";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Fecha.DefaultCellStyle = dataGridViewCellStyle3;
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            this.Fecha.Width = 80;
            // 
            // Cuenta
            // 
            this.Cuenta.DataPropertyName = "CajaAsociaciones";
            this.Cuenta.HeaderText = "Cuenta";
            this.Cuenta.Name = "Cuenta";
            this.Cuenta.ReadOnly = true;
            this.Cuenta.Width = 155;
            // 
            // Movimiento
            // 
            this.Movimiento.DataPropertyName = "Movimiento";
            this.Movimiento.HeaderText = "Movimiento";
            this.Movimiento.Name = "Movimiento";
            this.Movimiento.ReadOnly = true;
            this.Movimiento.Width = 155;
            // 
            // Transferencia
            // 
            this.Transferencia.DataPropertyName = "Transferencia";
            this.Transferencia.DecimalDigits = 2;
            this.Transferencia.DecimalSeparator = ".";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Transferencia.DefaultCellStyle = dataGridViewCellStyle4;
            this.Transferencia.HeaderText = "Transf.";
            this.Transferencia.Name = "Transferencia";
            this.Transferencia.ReadOnly = true;
            this.Transferencia.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Transferencia.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Transferencia.Width = 80;
            // 
            // MercadoPago
            // 
            this.MercadoPago.DataPropertyName = "MP";
            this.MercadoPago.DecimalDigits = 2;
            this.MercadoPago.DecimalSeparator = ".";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.MercadoPago.DefaultCellStyle = dataGridViewCellStyle5;
            this.MercadoPago.HeaderText = "MP";
            this.MercadoPago.Name = "MercadoPago";
            this.MercadoPago.ReadOnly = true;
            this.MercadoPago.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MercadoPago.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.MercadoPago.Width = 80;
            // 
            // Debito
            // 
            this.Debito.DataPropertyName = "Debito";
            this.Debito.DecimalDigits = 2;
            this.Debito.DecimalSeparator = ".";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Debito.DefaultCellStyle = dataGridViewCellStyle6;
            this.Debito.HeaderText = "Debito";
            this.Debito.Name = "Debito";
            this.Debito.ReadOnly = true;
            this.Debito.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Debito.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Debito.Width = 80;
            // 
            // Credito
            // 
            this.Credito.DataPropertyName = "Credito";
            this.Credito.DecimalDigits = 2;
            this.Credito.DecimalSeparator = ".";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Credito.DefaultCellStyle = dataGridViewCellStyle7;
            this.Credito.HeaderText = "Credito";
            this.Credito.Name = "Credito";
            this.Credito.ReadOnly = true;
            this.Credito.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Credito.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Credito.Width = 80;
            // 
            // Total
            // 
            this.Total.DataPropertyName = "MontoTotal";
            this.Total.DecimalDigits = 2;
            this.Total.DecimalSeparator = ".";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Total.DefaultCellStyle = dataGridViewCellStyle8;
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            this.Total.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Total.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // frmCajaBancos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 550);
            this.Controls.Add(this.txtSaldo);
            this.Controls.Add(this.txtBancos);
            this.Controls.Add(this.lblTotalBcos);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gpbBusquedas);
            this.Controls.Add(this.btnPanel);
            this.Controls.Add(this.lblSaldo);
            this.Controls.Add(this.lblCajaAs);
            this.Controls.Add(this.cboCajaAs);
            this.Controls.Add(this.dgvCaja);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCajaBancos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " DETALLE DE CUENTAS BANCARIAS";
            this.Load += new System.EventHandler(this.frmCajaBancos_Load);
            this.btnPanel.ResumeLayout(false);
            this.gpbBusquedas.ResumeLayout(false);
            this.gpbBusquedas.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsReportes1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCaja)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboCajaAs;
        private System.Windows.Forms.Panel btnPanel;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Label lblCajaAs;
        private System.Windows.Forms.Label lblSaldo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private NumericGridColumn numericGridColumn1;
        private NumericGridColumn numericGridColumn2;
        private NumericGridColumn numericGridColumn3;
        private NumericGridColumn numericGridColumn4;
        private NumericGridColumn numericGridColumn5;
        private System.Windows.Forms.GroupBox gpbBusquedas;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtFecha;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBancos;
        private System.Windows.Forms.Label lblTotalBcos;
        private System.Windows.Forms.RadioButton optCred;
        private System.Windows.Forms.RadioButton optDeb;
        private System.Windows.Forms.RadioButton optMP;
        private System.Windows.Forms.RadioButton optTransfer;
        private System.Windows.Forms.TextBox txtSaldo;
        private dsReportes dsReportes1;
        private System.Windows.Forms.DataGridView dgvCaja;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cuenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Movimiento;
        private NumericGridColumn Transferencia;
        private NumericGridColumn MercadoPago;
        private NumericGridColumn Debito;
        private NumericGridColumn Credito;
        private NumericGridColumn Total;
        private System.Windows.Forms.RadioButton optOtros;
    }
}
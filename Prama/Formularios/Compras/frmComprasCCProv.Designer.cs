namespace Prama
{
    partial class frmComprasCCProv
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmComprasCCProv));
            this.btnPanel = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.gpbProveedores = new System.Windows.Forms.GroupBox();
            this.txtComprador = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCondicionIva = new System.Windows.Forms.TextBox();
            this.lblCondIVA = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCodigoProv = new System.Windows.Forms.TextBox();
            this.lblCUIT = new System.Windows.Forms.Label();
            this.txtCUIT = new System.Windows.Forms.TextBox();
            this.lblRSoc = new System.Windows.Forms.Label();
            this.txtRSoc = new System.Windows.Forms.TextBox();
            this.gpbCC = new System.Windows.Forms.GroupBox();
            this.lblAplicado = new System.Windows.Forms.Label();
            this.txtSaldoApli = new System.Windows.Forms.TextBox();
            this.lblSadoCC = new System.Windows.Forms.Label();
            this.txtSaldo = new System.Windows.Forms.TextBox();
            this.dgvComprobantes = new System.Windows.Forms.DataGridView();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdComprobante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoComprobante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Numero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comprador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new Prama.NumericGridColumn();
            this.Saldo = new Prama.NumericGridColumn();
            this.Resto = new Prama.NumericGridColumn();
            this.Elegido = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Aplicado = new Prama.NumericGridColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numericGridColumn1 = new Prama.NumericGridColumn();
            this.numericGridColumn2 = new Prama.NumericGridColumn();
            this.numericGridColumn3 = new Prama.NumericGridColumn();
            this.gpbTotal = new System.Windows.Forms.GroupBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.txtTotalRC = new System.Windows.Forms.TextBox();
            this.gpbDetalle = new System.Windows.Forms.GroupBox();
            this.lblSaldoAFavor = new System.Windows.Forms.Label();
            this.txtSaldoAFavor = new System.Windows.Forms.TextBox();
            this.btnCh = new System.Windows.Forms.Button();
            this.txtCheques = new System.Windows.Forms.TextBox();
            this.chkCheques = new System.Windows.Forms.CheckBox();
            this.txtBancos = new System.Windows.Forms.TextBox();
            this.cboBancos = new System.Windows.Forms.ComboBox();
            this.ckBancos = new System.Windows.Forms.CheckBox();
            this.chkEfectivo = new System.Windows.Forms.CheckBox();
            this.txtEfectivo = new System.Windows.Forms.TextBox();
            this.btnPanel.SuspendLayout();
            this.gpbProveedores.SuspendLayout();
            this.gpbCC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComprobantes)).BeginInit();
            this.gpbTotal.SuspendLayout();
            this.gpbDetalle.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPanel
            // 
            this.btnPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPanel.Controls.Add(this.btnCancelar);
            this.btnPanel.Controls.Add(this.btnAceptar);
            this.btnPanel.Controls.Add(this.btnSalir);
            this.btnPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPanel.Location = new System.Drawing.Point(0, 513);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(894, 58);
            this.btnPanel.TabIndex = 44;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = global::Prama.Recursos.cancel;
            this.btnCancelar.Location = new System.Drawing.Point(545, 9);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(45, 40);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Image = global::Prama.Recursos.Aceptar;
            this.btnAceptar.Location = new System.Drawing.Point(494, 9);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(45, 40);
            this.btnAceptar.TabIndex = 5;
            this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Image = global::Prama.Recursos.Salir;
            this.btnSalir.Location = new System.Drawing.Point(837, 9);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(45, 40);
            this.btnSalir.TabIndex = 7;
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // gpbProveedores
            // 
            this.gpbProveedores.Controls.Add(this.txtComprador);
            this.gpbProveedores.Controls.Add(this.label6);
            this.gpbProveedores.Controls.Add(this.txtCondicionIva);
            this.gpbProveedores.Controls.Add(this.lblCondIVA);
            this.gpbProveedores.Controls.Add(this.label1);
            this.gpbProveedores.Controls.Add(this.txtCodigoProv);
            this.gpbProveedores.Controls.Add(this.lblCUIT);
            this.gpbProveedores.Controls.Add(this.txtCUIT);
            this.gpbProveedores.Controls.Add(this.lblRSoc);
            this.gpbProveedores.Controls.Add(this.txtRSoc);
            this.gpbProveedores.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbProveedores.Location = new System.Drawing.Point(12, 12);
            this.gpbProveedores.Name = "gpbProveedores";
            this.gpbProveedores.Size = new System.Drawing.Size(869, 71);
            this.gpbProveedores.TabIndex = 46;
            this.gpbProveedores.TabStop = false;
            this.gpbProveedores.Text = "Datos del Proveedor y Condiciones";
            // 
            // txtComprador
            // 
            this.txtComprador.Enabled = false;
            this.txtComprador.Location = new System.Drawing.Point(671, 39);
            this.txtComprador.MaxLength = 70;
            this.txtComprador.Name = "txtComprador";
            this.txtComprador.Size = new System.Drawing.Size(192, 20);
            this.txtComprador.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(671, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 13);
            this.label6.TabIndex = 33;
            this.label6.Text = "Comprador : ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCondicionIva
            // 
            this.txtCondicionIva.Enabled = false;
            this.txtCondicionIva.Location = new System.Drawing.Point(462, 39);
            this.txtCondicionIva.MaxLength = 70;
            this.txtCondicionIva.Name = "txtCondicionIva";
            this.txtCondicionIva.Size = new System.Drawing.Size(203, 20);
            this.txtCondicionIva.TabIndex = 3;
            // 
            // lblCondIVA
            // 
            this.lblCondIVA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCondIVA.Location = new System.Drawing.Point(459, 23);
            this.lblCondIVA.Name = "lblCondIVA";
            this.lblCondIVA.Size = new System.Drawing.Size(80, 13);
            this.lblCondIVA.TabIndex = 13;
            this.lblCondIVA.Text = "Condición IVA: ";
            this.lblCondIVA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Código :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCodigoProv
            // 
            this.txtCodigoProv.Enabled = false;
            this.txtCodigoProv.Location = new System.Drawing.Point(6, 39);
            this.txtCodigoProv.MaxLength = 13;
            this.txtCodigoProv.Name = "txtCodigoProv";
            this.txtCodigoProv.Size = new System.Drawing.Size(47, 20);
            this.txtCodigoProv.TabIndex = 0;
            // 
            // lblCUIT
            // 
            this.lblCUIT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCUIT.Location = new System.Drawing.Point(331, 23);
            this.lblCUIT.Name = "lblCUIT";
            this.lblCUIT.Size = new System.Drawing.Size(41, 13);
            this.lblCUIT.TabIndex = 8;
            this.lblCUIT.Text = "CUIT:";
            this.lblCUIT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCUIT
            // 
            this.txtCUIT.Enabled = false;
            this.txtCUIT.Location = new System.Drawing.Point(334, 39);
            this.txtCUIT.MaxLength = 13;
            this.txtCUIT.Name = "txtCUIT";
            this.txtCUIT.Size = new System.Drawing.Size(122, 20);
            this.txtCUIT.TabIndex = 2;
            // 
            // lblRSoc
            // 
            this.lblRSoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRSoc.Location = new System.Drawing.Point(53, 23);
            this.lblRSoc.Name = "lblRSoc";
            this.lblRSoc.Size = new System.Drawing.Size(76, 13);
            this.lblRSoc.TabIndex = 3;
            this.lblRSoc.Text = "Razón Social:";
            this.lblRSoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRSoc
            // 
            this.txtRSoc.Enabled = false;
            this.txtRSoc.Location = new System.Drawing.Point(59, 39);
            this.txtRSoc.MaxLength = 70;
            this.txtRSoc.Name = "txtRSoc";
            this.txtRSoc.Size = new System.Drawing.Size(269, 20);
            this.txtRSoc.TabIndex = 1;
            // 
            // gpbCC
            // 
            this.gpbCC.Controls.Add(this.lblAplicado);
            this.gpbCC.Controls.Add(this.txtSaldoApli);
            this.gpbCC.Controls.Add(this.lblSadoCC);
            this.gpbCC.Controls.Add(this.txtSaldo);
            this.gpbCC.Controls.Add(this.dgvComprobantes);
            this.gpbCC.Location = new System.Drawing.Point(12, 193);
            this.gpbCC.Name = "gpbCC";
            this.gpbCC.Size = new System.Drawing.Size(868, 314);
            this.gpbCC.TabIndex = 47;
            this.gpbCC.TabStop = false;
            this.gpbCC.Text = "Detalle de la Cuenta Corriente";
            // 
            // lblAplicado
            // 
            this.lblAplicado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAplicado.Location = new System.Drawing.Point(404, 288);
            this.lblAplicado.Name = "lblAplicado";
            this.lblAplicado.Size = new System.Drawing.Size(124, 21);
            this.lblAplicado.TabIndex = 52;
            this.lblAplicado.Text = "Saldo a Aplicar:";
            this.lblAplicado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSaldoApli
            // 
            this.txtSaldoApli.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaldoApli.Location = new System.Drawing.Point(534, 286);
            this.txtSaldoApli.MaxLength = 13;
            this.txtSaldoApli.Name = "txtSaldoApli";
            this.txtSaldoApli.ReadOnly = true;
            this.txtSaldoApli.Size = new System.Drawing.Size(87, 22);
            this.txtSaldoApli.TabIndex = 51;
            this.txtSaldoApli.Text = "0.00";
            this.txtSaldoApli.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSadoCC
            // 
            this.lblSadoCC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSadoCC.Location = new System.Drawing.Point(655, 288);
            this.lblSadoCC.Name = "lblSadoCC";
            this.lblSadoCC.Size = new System.Drawing.Size(112, 21);
            this.lblSadoCC.TabIndex = 50;
            this.lblSadoCC.Text = "Saldo Cta Cte :";
            this.lblSadoCC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSaldo
            // 
            this.txtSaldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaldo.Location = new System.Drawing.Point(773, 287);
            this.txtSaldo.MaxLength = 13;
            this.txtSaldo.Name = "txtSaldo";
            this.txtSaldo.ReadOnly = true;
            this.txtSaldo.Size = new System.Drawing.Size(87, 22);
            this.txtSaldo.TabIndex = 49;
            this.txtSaldo.Text = "0.00";
            this.txtSaldo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dgvComprobantes
            // 
            this.dgvComprobantes.AllowUserToAddRows = false;
            this.dgvComprobantes.AllowUserToDeleteRows = false;
            this.dgvComprobantes.AllowUserToResizeColumns = false;
            this.dgvComprobantes.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvComprobantes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvComprobantes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComprobantes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Item,
            this.IdComprobante,
            this.Fecha,
            this.TipoComprobante,
            this.Numero,
            this.Comprador,
            this.Total,
            this.Saldo,
            this.Resto,
            this.Elegido,
            this.Aplicado});
            this.dgvComprobantes.Location = new System.Drawing.Point(6, 19);
            this.dgvComprobantes.Name = "dgvComprobantes";
            this.dgvComprobantes.ReadOnly = true;
            this.dgvComprobantes.RowHeadersVisible = false;
            this.dgvComprobantes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvComprobantes.Size = new System.Drawing.Size(854, 259);
            this.dgvComprobantes.TabIndex = 46;
            this.dgvComprobantes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvComprobantes_CellDoubleClick);
            // 
            // Item
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Item.DefaultCellStyle = dataGridViewCellStyle2;
            this.Item.HeaderText = "Item";
            this.Item.Name = "Item";
            this.Item.ReadOnly = true;
            this.Item.Width = 40;
            // 
            // IdComprobante
            // 
            this.IdComprobante.DataPropertyName = "Id";
            this.IdComprobante.HeaderText = "IdComprobante";
            this.IdComprobante.Name = "IdComprobante";
            this.IdComprobante.ReadOnly = true;
            this.IdComprobante.Visible = false;
            // 
            // Fecha
            // 
            this.Fecha.DataPropertyName = "Fecha";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Fecha.DefaultCellStyle = dataGridViewCellStyle3;
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            this.Fecha.Width = 70;
            // 
            // TipoComprobante
            // 
            this.TipoComprobante.DataPropertyName = "Comprobante";
            this.TipoComprobante.HeaderText = "Tipo Comprobante";
            this.TipoComprobante.Name = "TipoComprobante";
            this.TipoComprobante.ReadOnly = true;
            this.TipoComprobante.Width = 170;
            // 
            // Numero
            // 
            this.Numero.DataPropertyName = "Numero";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Numero.DefaultCellStyle = dataGridViewCellStyle4;
            this.Numero.HeaderText = "Número";
            this.Numero.Name = "Numero";
            this.Numero.ReadOnly = true;
            this.Numero.Width = 110;
            // 
            // Comprador
            // 
            this.Comprador.DataPropertyName = "Usuario";
            this.Comprador.HeaderText = "Comprador";
            this.Comprador.Name = "Comprador";
            this.Comprador.ReadOnly = true;
            this.Comprador.Width = 180;
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
            this.Total.ReadOnly = true;
            this.Total.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Total.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Total.Width = 85;
            // 
            // Saldo
            // 
            this.Saldo.DataPropertyName = "Saldo";
            this.Saldo.DecimalDigits = 2;
            this.Saldo.DecimalSeparator = ".";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Saldo.DefaultCellStyle = dataGridViewCellStyle6;
            this.Saldo.HeaderText = "Saldo";
            this.Saldo.Name = "Saldo";
            this.Saldo.ReadOnly = true;
            this.Saldo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Saldo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Saldo.Visible = false;
            this.Saldo.Width = 80;
            // 
            // Resto
            // 
            this.Resto.DecimalDigits = 2;
            this.Resto.DecimalSeparator = ".";
            this.Resto.HeaderText = "Resto";
            this.Resto.Name = "Resto";
            this.Resto.ReadOnly = true;
            this.Resto.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Resto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Resto.Width = 80;
            // 
            // Elegido
            // 
            this.Elegido.HeaderText = "Elegido";
            this.Elegido.Name = "Elegido";
            this.Elegido.ReadOnly = true;
            this.Elegido.Width = 60;
            // 
            // Aplicado
            // 
            this.Aplicado.DecimalDigits = 2;
            this.Aplicado.DecimalSeparator = ".";
            this.Aplicado.HeaderText = "Aplicado";
            this.Aplicado.Name = "Aplicado";
            this.Aplicado.ReadOnly = true;
            this.Aplicado.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn1.HeaderText = "Item";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 40;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "IdComprobante";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn3.HeaderText = "Fecha";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 80;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Tipo Comprobante";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 180;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn5.HeaderText = "Número";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Comprador";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 180;
            // 
            // numericGridColumn1
            // 
            this.numericGridColumn1.DecimalDigits = 2;
            this.numericGridColumn1.DecimalSeparator = ".";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.numericGridColumn1.DefaultCellStyle = dataGridViewCellStyle10;
            this.numericGridColumn1.HeaderText = "Debe";
            this.numericGridColumn1.Name = "numericGridColumn1";
            this.numericGridColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.numericGridColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.numericGridColumn1.Width = 85;
            // 
            // numericGridColumn2
            // 
            this.numericGridColumn2.DecimalDigits = 2;
            this.numericGridColumn2.DecimalSeparator = ".";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.numericGridColumn2.DefaultCellStyle = dataGridViewCellStyle11;
            this.numericGridColumn2.HeaderText = "Haber";
            this.numericGridColumn2.Name = "numericGridColumn2";
            this.numericGridColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.numericGridColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.numericGridColumn2.Width = 85;
            // 
            // numericGridColumn3
            // 
            this.numericGridColumn3.DecimalDigits = 2;
            this.numericGridColumn3.DecimalSeparator = ".";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.numericGridColumn3.DefaultCellStyle = dataGridViewCellStyle12;
            this.numericGridColumn3.HeaderText = "Saldo";
            this.numericGridColumn3.Name = "numericGridColumn3";
            this.numericGridColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.numericGridColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // gpbTotal
            // 
            this.gpbTotal.Controls.Add(this.btnAgregar);
            this.gpbTotal.Controls.Add(this.txtTotalRC);
            this.gpbTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbTotal.Location = new System.Drawing.Point(707, 92);
            this.gpbTotal.Name = "gpbTotal";
            this.gpbTotal.Size = new System.Drawing.Size(175, 95);
            this.gpbTotal.TabIndex = 193;
            this.gpbTotal.TabStop = false;
            this.gpbTotal.Text = "Total del O.P.:";
            // 
            // btnAgregar
            // 
            this.btnAgregar.Image = global::Prama.Recursos.Agregar;
            this.btnAgregar.Location = new System.Drawing.Point(114, 28);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(45, 40);
            this.btnAgregar.TabIndex = 187;
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // txtTotalRC
            // 
            this.txtTotalRC.Enabled = false;
            this.txtTotalRC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalRC.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtTotalRC.Location = new System.Drawing.Point(16, 42);
            this.txtTotalRC.Name = "txtTotalRC";
            this.txtTotalRC.ReadOnly = true;
            this.txtTotalRC.Size = new System.Drawing.Size(92, 20);
            this.txtTotalRC.TabIndex = 186;
            this.txtTotalRC.Text = "0.00";
            this.txtTotalRC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // gpbDetalle
            // 
            this.gpbDetalle.Controls.Add(this.lblSaldoAFavor);
            this.gpbDetalle.Controls.Add(this.txtSaldoAFavor);
            this.gpbDetalle.Controls.Add(this.btnCh);
            this.gpbDetalle.Controls.Add(this.txtCheques);
            this.gpbDetalle.Controls.Add(this.chkCheques);
            this.gpbDetalle.Controls.Add(this.txtBancos);
            this.gpbDetalle.Controls.Add(this.cboBancos);
            this.gpbDetalle.Controls.Add(this.ckBancos);
            this.gpbDetalle.Controls.Add(this.chkEfectivo);
            this.gpbDetalle.Controls.Add(this.txtEfectivo);
            this.gpbDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbDetalle.Location = new System.Drawing.Point(12, 101);
            this.gpbDetalle.Name = "gpbDetalle";
            this.gpbDetalle.Size = new System.Drawing.Size(689, 86);
            this.gpbDetalle.TabIndex = 194;
            this.gpbDetalle.TabStop = false;
            this.gpbDetalle.Text = " Origen o Destino de los Fondos ";
            // 
            // lblSaldoAFavor
            // 
            this.lblSaldoAFavor.AutoSize = true;
            this.lblSaldoAFavor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaldoAFavor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblSaldoAFavor.Location = new System.Drawing.Point(530, 61);
            this.lblSaldoAFavor.Name = "lblSaldoAFavor";
            this.lblSaldoAFavor.Size = new System.Drawing.Size(65, 13);
            this.lblSaldoAFavor.TabIndex = 198;
            this.lblSaldoAFavor.Text = "Sldo. A Fav.";
            // 
            // txtSaldoAFavor
            // 
            this.txtSaldoAFavor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaldoAFavor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtSaldoAFavor.Location = new System.Drawing.Point(602, 58);
            this.txtSaldoAFavor.Name = "txtSaldoAFavor";
            this.txtSaldoAFavor.ReadOnly = true;
            this.txtSaldoAFavor.Size = new System.Drawing.Size(75, 20);
            this.txtSaldoAFavor.TabIndex = 197;
            this.txtSaldoAFavor.Text = "0.00";
            this.txtSaldoAFavor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSaldoAFavor.TextChanged += new System.EventHandler(this.txtSaldoAFavor_TextChanged_1);
            // 
            // btnCh
            // 
            this.btnCh.Enabled = false;
            this.btnCh.Image = global::Prama.Recursos.vista;
            this.btnCh.Location = new System.Drawing.Point(195, 54);
            this.btnCh.Name = "btnCh";
            this.btnCh.Size = new System.Drawing.Size(25, 25);
            this.btnCh.TabIndex = 196;
            this.btnCh.UseVisualStyleBackColor = true;
            this.btnCh.Click += new System.EventHandler(this.btnCh_Click);
            // 
            // txtCheques
            // 
            this.txtCheques.Enabled = false;
            this.txtCheques.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheques.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtCheques.Location = new System.Drawing.Point(96, 57);
            this.txtCheques.MaxLength = 150;
            this.txtCheques.Name = "txtCheques";
            this.txtCheques.Size = new System.Drawing.Size(93, 20);
            this.txtCheques.TabIndex = 13;
            this.txtCheques.Text = "0.00";
            this.txtCheques.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCheques.TextChanged += new System.EventHandler(this.txtCheques_TextChanged);
            // 
            // chkCheques
            // 
            this.chkCheques.AutoSize = true;
            this.chkCheques.Location = new System.Drawing.Point(9, 57);
            this.chkCheques.Name = "chkCheques";
            this.chkCheques.Size = new System.Drawing.Size(83, 17);
            this.chkCheques.TabIndex = 12;
            this.chkCheques.Text = "Cheques :";
            this.chkCheques.UseVisualStyleBackColor = true;
            this.chkCheques.CheckedChanged += new System.EventHandler(this.chkCheques_CheckedChanged);
            // 
            // txtBancos
            // 
            this.txtBancos.Enabled = false;
            this.txtBancos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBancos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtBancos.Location = new System.Drawing.Point(298, 57);
            this.txtBancos.MaxLength = 150;
            this.txtBancos.Name = "txtBancos";
            this.txtBancos.Size = new System.Drawing.Size(112, 20);
            this.txtBancos.TabIndex = 11;
            this.txtBancos.Text = "0.00";
            this.txtBancos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBancos.TextChanged += new System.EventHandler(this.txtBancos_TextChanged);
            // 
            // cboBancos
            // 
            this.cboBancos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBancos.Enabled = false;
            this.cboBancos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBancos.FormattingEnabled = true;
            this.cboBancos.Location = new System.Drawing.Point(298, 23);
            this.cboBancos.Name = "cboBancos";
            this.cboBancos.Size = new System.Drawing.Size(259, 21);
            this.cboBancos.TabIndex = 10;
            // 
            // ckBancos
            // 
            this.ckBancos.AutoSize = true;
            this.ckBancos.Location = new System.Drawing.Point(216, 27);
            this.ckBancos.Name = "ckBancos";
            this.ckBancos.Size = new System.Drawing.Size(76, 17);
            this.ckBancos.TabIndex = 9;
            this.ckBancos.Text = "Bancos :";
            this.ckBancos.UseVisualStyleBackColor = true;
            this.ckBancos.CheckedChanged += new System.EventHandler(this.ckBancos_CheckedChanged);
            // 
            // chkEfectivo
            // 
            this.chkEfectivo.AutoSize = true;
            this.chkEfectivo.Location = new System.Drawing.Point(9, 28);
            this.chkEfectivo.Name = "chkEfectivo";
            this.chkEfectivo.Size = new System.Drawing.Size(81, 17);
            this.chkEfectivo.TabIndex = 7;
            this.chkEfectivo.Text = "Efectivo :";
            this.chkEfectivo.UseVisualStyleBackColor = true;
            this.chkEfectivo.CheckedChanged += new System.EventHandler(this.chkEfectivo_CheckedChanged);
            // 
            // txtEfectivo
            // 
            this.txtEfectivo.Enabled = false;
            this.txtEfectivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEfectivo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtEfectivo.Location = new System.Drawing.Point(96, 25);
            this.txtEfectivo.MaxLength = 150;
            this.txtEfectivo.Name = "txtEfectivo";
            this.txtEfectivo.Size = new System.Drawing.Size(93, 20);
            this.txtEfectivo.TabIndex = 8;
            this.txtEfectivo.Text = "0.00";
            this.txtEfectivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtEfectivo.TextChanged += new System.EventHandler(this.txtEfectivo_TextChanged_1);
            // 
            // frmComprasCCProv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 571);
            this.Controls.Add(this.gpbDetalle);
            this.Controls.Add(this.gpbTotal);
            this.Controls.Add(this.gpbCC);
            this.Controls.Add(this.gpbProveedores);
            this.Controls.Add(this.btnPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmComprasCCProv";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " - CUENTA CORRIENTE DE PROVEEDORES - PAGO Y GENERACIÓN DE ÓRDEN DE PAGO";
            this.Load += new System.EventHandler(this.frmComprasCCProv_Load);
            this.btnPanel.ResumeLayout(false);
            this.gpbProveedores.ResumeLayout(false);
            this.gpbProveedores.PerformLayout();
            this.gpbCC.ResumeLayout(false);
            this.gpbCC.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComprobantes)).EndInit();
            this.gpbTotal.ResumeLayout(false);
            this.gpbTotal.PerformLayout();
            this.gpbDetalle.ResumeLayout(false);
            this.gpbDetalle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel btnPanel;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.GroupBox gpbProveedores;
        private System.Windows.Forms.TextBox txtComprador;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCondicionIva;
        private System.Windows.Forms.Label lblCondIVA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCodigoProv;
        private System.Windows.Forms.Label lblCUIT;
        private System.Windows.Forms.TextBox txtCUIT;
        private System.Windows.Forms.Label lblRSoc;
        private System.Windows.Forms.TextBox txtRSoc;
        private System.Windows.Forms.GroupBox gpbCC;
        private System.Windows.Forms.DataGridView dgvComprobantes;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private NumericGridColumn numericGridColumn1;
        private NumericGridColumn numericGridColumn2;
        private NumericGridColumn numericGridColumn3;
        private System.Windows.Forms.GroupBox gpbTotal;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.TextBox txtTotalRC;
        private System.Windows.Forms.Label lblAplicado;
        private System.Windows.Forms.TextBox txtSaldoApli;
        private System.Windows.Forms.Label lblSadoCC;
        private System.Windows.Forms.TextBox txtSaldo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdComprobante;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoComprobante;
        private System.Windows.Forms.DataGridViewTextBoxColumn Numero;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comprador;
        private NumericGridColumn Total;
        private NumericGridColumn Saldo;
        private NumericGridColumn Resto;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Elegido;
        private NumericGridColumn Aplicado;
        private System.Windows.Forms.GroupBox gpbDetalle;
        private System.Windows.Forms.Button btnCh;
        private System.Windows.Forms.TextBox txtCheques;
        private System.Windows.Forms.CheckBox chkCheques;
        private System.Windows.Forms.TextBox txtBancos;
        private System.Windows.Forms.ComboBox cboBancos;
        private System.Windows.Forms.CheckBox ckBancos;
        private System.Windows.Forms.CheckBox chkEfectivo;
        private System.Windows.Forms.TextBox txtEfectivo;
        private System.Windows.Forms.Label lblSaldoAFavor;
        private System.Windows.Forms.TextBox txtSaldoAFavor;
    }
}
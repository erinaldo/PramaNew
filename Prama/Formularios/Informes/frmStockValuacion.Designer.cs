namespace Prama.Formularios.Informes
{
    partial class frmStockValuacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStockValuacion));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.IdArticulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoArticulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Articulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdSubrubroArticulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubrubroArticulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdRubroArticulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RubroArticulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Costo = new Prama.NumericGridColumn();
            this.Stock = new Prama.NumericGridColumn();
            this.Valuacion = new Prama.NumericGridColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbProductos = new System.Windows.Forms.RadioButton();
            this.rdbInsumos = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.txtValuacion = new System.Windows.Forms.TextBox();
            this.lblIdCli = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numericGridColumn1 = new Prama.NumericGridColumn();
            this.numericGridColumn2 = new Prama.NumericGridColumn();
            this.numericGridColumn3 = new Prama.NumericGridColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboSubRubros = new System.Windows.Forms.ComboBox();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.cboRubros = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.btnImprimir);
            this.panel1.Controls.Add(this.btnSalir);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 463);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(801, 58);
            this.panel1.TabIndex = 16;
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
            this.btnSalir.Location = new System.Drawing.Point(744, 9);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(45, 40);
            this.btnSalir.TabIndex = 15;
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AllowUserToResizeColumns = false;
            this.dgvData.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdArticulo,
            this.CodigoArticulo,
            this.Articulo,
            this.IdSubrubroArticulo,
            this.SubrubroArticulo,
            this.IdRubroArticulo,
            this.RubroArticulo,
            this.Costo,
            this.Stock,
            this.Valuacion});
            this.dgvData.Location = new System.Drawing.Point(12, 102);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(777, 313);
            this.dgvData.StandardTab = true;
            this.dgvData.TabIndex = 17;
            // 
            // IdArticulo
            // 
            this.IdArticulo.DataPropertyName = "IdArticulo";
            this.IdArticulo.HeaderText = "IdArticulo";
            this.IdArticulo.Name = "IdArticulo";
            this.IdArticulo.Visible = false;
            // 
            // CodigoArticulo
            // 
            this.CodigoArticulo.DataPropertyName = "CodigoArticulo";
            this.CodigoArticulo.HeaderText = "Código";
            this.CodigoArticulo.Name = "CodigoArticulo";
            // 
            // Articulo
            // 
            this.Articulo.DataPropertyName = "Articulo";
            this.Articulo.HeaderText = "Artículo";
            this.Articulo.Name = "Articulo";
            this.Articulo.Width = 350;
            // 
            // IdSubrubroArticulo
            // 
            this.IdSubrubroArticulo.DataPropertyName = "IdSubrubroArticulo";
            this.IdSubrubroArticulo.HeaderText = "IdSubrubroArticulo";
            this.IdSubrubroArticulo.Name = "IdSubrubroArticulo";
            this.IdSubrubroArticulo.Visible = false;
            // 
            // SubrubroArticulo
            // 
            this.SubrubroArticulo.DataPropertyName = "SubrubroArticulo";
            this.SubrubroArticulo.HeaderText = "SubrubroArticulo";
            this.SubrubroArticulo.Name = "SubrubroArticulo";
            this.SubrubroArticulo.Visible = false;
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
            this.RubroArticulo.HeaderText = "RubroArticulo";
            this.RubroArticulo.Name = "RubroArticulo";
            this.RubroArticulo.Visible = false;
            // 
            // Costo
            // 
            this.Costo.DataPropertyName = "Precio";
            this.Costo.DecimalDigits = 2;
            this.Costo.DecimalSeparator = ".";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Costo.DefaultCellStyle = dataGridViewCellStyle2;
            this.Costo.HeaderText = "Costo";
            this.Costo.Name = "Costo";
            this.Costo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Costo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Costo.Width = 80;
            // 
            // Stock
            // 
            this.Stock.DataPropertyName = "Stock";
            this.Stock.DecimalDigits = 2;
            this.Stock.DecimalSeparator = ".";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Stock.DefaultCellStyle = dataGridViewCellStyle3;
            this.Stock.HeaderText = "Stock";
            this.Stock.Name = "Stock";
            this.Stock.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Stock.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Valuacion
            // 
            this.Valuacion.DataPropertyName = "Valuacion";
            this.Valuacion.DecimalDigits = 2;
            this.Valuacion.DecimalSeparator = ".";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Valuacion.DefaultCellStyle = dataGridViewCellStyle4;
            this.Valuacion.HeaderText = "Valuación";
            this.Valuacion.Name = "Valuacion";
            this.Valuacion.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Valuacion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Valuacion.Width = 120;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbProductos);
            this.groupBox1.Controls.Add(this.rdbInsumos);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(175, 83);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo de Artículo";
            // 
            // rdbProductos
            // 
            this.rdbProductos.AutoSize = true;
            this.rdbProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbProductos.Location = new System.Drawing.Point(88, 38);
            this.rdbProductos.Name = "rdbProductos";
            this.rdbProductos.Size = new System.Drawing.Size(73, 17);
            this.rdbProductos.TabIndex = 1;
            this.rdbProductos.Text = "Productos";
            this.rdbProductos.UseVisualStyleBackColor = true;
            this.rdbProductos.CheckedChanged += new System.EventHandler(this.rdbProductos_CheckedChanged);
            // 
            // rdbInsumos
            // 
            this.rdbInsumos.AutoSize = true;
            this.rdbInsumos.Checked = true;
            this.rdbInsumos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbInsumos.Location = new System.Drawing.Point(18, 38);
            this.rdbInsumos.Name = "rdbInsumos";
            this.rdbInsumos.Size = new System.Drawing.Size(64, 17);
            this.rdbInsumos.TabIndex = 0;
            this.rdbInsumos.TabStop = true;
            this.rdbInsumos.Text = "Insumos";
            this.rdbInsumos.UseVisualStyleBackColor = true;
            this.rdbInsumos.CheckedChanged += new System.EventHandler(this.rdbInsumos_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(422, 435);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 21);
            this.label2.TabIndex = 23;
            this.label2.Text = "Valuación Global : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTotal
            // 
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(565, 428);
            this.txtTotal.MaxLength = 13;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(224, 29);
            this.txtTotal.TabIndex = 24;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtValuacion
            // 
            this.txtValuacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValuacion.Location = new System.Drawing.Point(408, 50);
            this.txtValuacion.MaxLength = 10;
            this.txtValuacion.Name = "txtValuacion";
            this.txtValuacion.Size = new System.Drawing.Size(101, 20);
            this.txtValuacion.TabIndex = 22;
            this.txtValuacion.Text = "0.00";
            this.txtValuacion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtValuacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValuacion_KeyPress);
            // 
            // lblIdCli
            // 
            this.lblIdCli.BackColor = System.Drawing.SystemColors.Control;
            this.lblIdCli.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdCli.Location = new System.Drawing.Point(405, 23);
            this.lblIdCli.Name = "lblIdCli";
            this.lblIdCli.Size = new System.Drawing.Size(117, 21);
            this.lblIdCli.TabIndex = 17;
            this.lblIdCli.Text = "Valuación mayor que: ";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "IdArticulo";
            this.dataGridViewTextBoxColumn1.HeaderText = "IdArticulo";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "CodigoArticulo";
            this.dataGridViewTextBoxColumn2.HeaderText = "Código";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Articulo";
            this.dataGridViewTextBoxColumn3.HeaderText = "Artículo";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 350;
            // 
            // numericGridColumn1
            // 
            this.numericGridColumn1.DataPropertyName = "Precio";
            this.numericGridColumn1.DecimalDigits = 2;
            this.numericGridColumn1.DecimalSeparator = ".";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.numericGridColumn1.DefaultCellStyle = dataGridViewCellStyle5;
            this.numericGridColumn1.HeaderText = "Costo";
            this.numericGridColumn1.Name = "numericGridColumn1";
            this.numericGridColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.numericGridColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.numericGridColumn1.Width = 80;
            // 
            // numericGridColumn2
            // 
            this.numericGridColumn2.DataPropertyName = "Stock";
            this.numericGridColumn2.DecimalDigits = 2;
            this.numericGridColumn2.DecimalSeparator = ".";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.numericGridColumn2.DefaultCellStyle = dataGridViewCellStyle6;
            this.numericGridColumn2.HeaderText = "Stock";
            this.numericGridColumn2.Name = "numericGridColumn2";
            this.numericGridColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.numericGridColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // numericGridColumn3
            // 
            this.numericGridColumn3.DataPropertyName = "Valuacion";
            this.numericGridColumn3.DecimalDigits = 2;
            this.numericGridColumn3.DecimalSeparator = ".";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.numericGridColumn3.DefaultCellStyle = dataGridViewCellStyle7;
            this.numericGridColumn3.HeaderText = "Valuación";
            this.numericGridColumn3.Name = "numericGridColumn3";
            this.numericGridColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.numericGridColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.numericGridColumn3.Width = 120;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Costo";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn4.HeaderText = "Costo";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 80;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Stock";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn5.HeaderText = "Stock";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Valuacion";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn6.HeaderText = "Valuación";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 120;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtValuacion);
            this.groupBox2.Controls.Add(this.cboSubRubros);
            this.groupBox2.Controls.Add(this.lblIdCli);
            this.groupBox2.Controls.Add(this.btnAplicar);
            this.groupBox2.Controls.Add(this.cboRubros);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(193, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(596, 84);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filtrar por Rubros, Sub-Rubros y valuación del artículo";
            // 
            // cboSubRubros
            // 
            this.cboSubRubros.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubRubros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSubRubros.FormattingEnabled = true;
            this.cboSubRubros.Location = new System.Drawing.Point(24, 50);
            this.cboSubRubros.Name = "cboSubRubros";
            this.cboSubRubros.Size = new System.Drawing.Size(342, 21);
            this.cboSubRubros.TabIndex = 9;
            // 
            // btnAplicar
            // 
            this.btnAplicar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAplicar.Image = global::Prama.Recursos.Filtro;
            this.btnAplicar.Location = new System.Drawing.Point(537, 27);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(45, 40);
            this.btnAplicar.TabIndex = 8;
            this.btnAplicar.UseVisualStyleBackColor = true;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // cboRubros
            // 
            this.cboRubros.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRubros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboRubros.FormattingEnabled = true;
            this.cboRubros.Location = new System.Drawing.Point(24, 23);
            this.cboRubros.Name = "cboRubros";
            this.cboRubros.Size = new System.Drawing.Size(342, 21);
            this.cboRubros.TabIndex = 7;
            this.cboRubros.SelectedIndexChanged += new System.EventHandler(this.cboRubros_SelectedIndexChanged);
            // 
            // frmStockValuacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 521);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStockValuacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " - VALUACION STOCK";
            this.Load += new System.EventHandler(this.frmStockValuacion_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbProductos;
        private System.Windows.Forms.RadioButton rdbInsumos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.TextBox txtValuacion;
        private System.Windows.Forms.Label lblIdCli;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private NumericGridColumn numericGridColumn1;
        private NumericGridColumn numericGridColumn2;
        private NumericGridColumn numericGridColumn3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cboSubRubros;
        private System.Windows.Forms.Button btnAplicar;
        private System.Windows.Forms.ComboBox cboRubros;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdArticulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoArticulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Articulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdSubrubroArticulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubrubroArticulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdRubroArticulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn RubroArticulo;
        private NumericGridColumn Costo;
        private NumericGridColumn Stock;
        private NumericGridColumn Valuacion;
    }
}
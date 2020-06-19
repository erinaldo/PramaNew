namespace Prama.Formularios.Caja
{
    partial class frmCajaCheques
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCajaCheques));
            this.dgvCajaCH = new System.Windows.Forms.DataGridView();
            this.Numero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaEmision = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaCobro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Importe = new Prama.NumericGridColumn();
            this.Banco = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EnCartera = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.txtEfectivoGral = new System.Windows.Forms.TextBox();
            this.lblSaldo = new System.Windows.Forms.Label();
            this.btnPanel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.chkCHCartera = new System.Windows.Forms.CheckBox();
            this.gpbBusquedas = new System.Windows.Forms.GroupBox();
            this.txtNro = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFC = new System.Windows.Forms.TextBox();
            this.lblFC = new System.Windows.Forms.Label();
            this.txtFE = new System.Windows.Forms.TextBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numericGridColumn1 = new Prama.NumericGridColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCajaCH)).BeginInit();
            this.btnPanel.SuspendLayout();
            this.gpbBusquedas.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvCajaCH
            // 
            this.dgvCajaCH.AllowUserToAddRows = false;
            this.dgvCajaCH.AllowUserToDeleteRows = false;
            this.dgvCajaCH.AllowUserToResizeColumns = false;
            this.dgvCajaCH.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvCajaCH.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCajaCH.ColumnHeadersHeight = 21;
            this.dgvCajaCH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCajaCH.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Numero,
            this.FechaEmision,
            this.FechaCobro,
            this.Importe,
            this.Banco,
            this.EnCartera});
            this.dgvCajaCH.Location = new System.Drawing.Point(12, 31);
            this.dgvCajaCH.Name = "dgvCajaCH";
            this.dgvCajaCH.ReadOnly = true;
            this.dgvCajaCH.RowHeadersVisible = false;
            this.dgvCajaCH.RowHeadersWidth = 20;
            this.dgvCajaCH.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCajaCH.Size = new System.Drawing.Size(631, 396);
            this.dgvCajaCH.StandardTab = true;
            this.dgvCajaCH.TabIndex = 4;
            // 
            // Numero
            // 
            this.Numero.DataPropertyName = "Numero";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Numero.DefaultCellStyle = dataGridViewCellStyle2;
            this.Numero.HeaderText = "Numero";
            this.Numero.Name = "Numero";
            this.Numero.ReadOnly = true;
            // 
            // FechaEmision
            // 
            this.FechaEmision.DataPropertyName = "FechaEmision";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.FechaEmision.DefaultCellStyle = dataGridViewCellStyle3;
            this.FechaEmision.HeaderText = "Fecha Emisión";
            this.FechaEmision.Name = "FechaEmision";
            this.FechaEmision.ReadOnly = true;
            // 
            // FechaCobro
            // 
            this.FechaCobro.DataPropertyName = "FechaCobro";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.FechaCobro.DefaultCellStyle = dataGridViewCellStyle4;
            this.FechaCobro.HeaderText = "Fecha Cobro";
            this.FechaCobro.Name = "FechaCobro";
            this.FechaCobro.ReadOnly = true;
            // 
            // Importe
            // 
            this.Importe.DataPropertyName = "Importe";
            this.Importe.DecimalDigits = 2;
            this.Importe.DecimalSeparator = ".";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Importe.DefaultCellStyle = dataGridViewCellStyle5;
            this.Importe.HeaderText = "Importe";
            this.Importe.Name = "Importe";
            this.Importe.ReadOnly = true;
            this.Importe.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Importe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Banco
            // 
            this.Banco.DataPropertyName = "Banco";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Banco.DefaultCellStyle = dataGridViewCellStyle6;
            this.Banco.HeaderText = "Banco";
            this.Banco.Name = "Banco";
            this.Banco.ReadOnly = true;
            // 
            // EnCartera
            // 
            this.EnCartera.DataPropertyName = "EnCartera";
            this.EnCartera.HeaderText = "En Cartera";
            this.EnCartera.Name = "EnCartera";
            this.EnCartera.ReadOnly = true;
            this.EnCartera.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.EnCartera.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // txtEfectivoGral
            // 
            this.txtEfectivoGral.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEfectivoGral.Location = new System.Drawing.Point(513, 459);
            this.txtEfectivoGral.MaxLength = 70;
            this.txtEfectivoGral.Name = "txtEfectivoGral";
            this.txtEfectivoGral.Size = new System.Drawing.Size(130, 26);
            this.txtEfectivoGral.TabIndex = 68;
            this.txtEfectivoGral.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSaldo
            // 
            this.lblSaldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaldo.Location = new System.Drawing.Point(510, 433);
            this.lblSaldo.Name = "lblSaldo";
            this.lblSaldo.Size = new System.Drawing.Size(79, 23);
            this.lblSaldo.TabIndex = 69;
            this.lblSaldo.Text = "Saldo Caja:";
            this.lblSaldo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPanel
            // 
            this.btnPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPanel.Controls.Add(this.button1);
            this.btnPanel.Controls.Add(this.btnSalir);
            this.btnPanel.Controls.Add(this.btnImprimir);
            this.btnPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPanel.Location = new System.Drawing.Point(0, 506);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(656, 58);
            this.btnPanel.TabIndex = 67;
            // 
            // button1
            // 
            this.button1.Image = global::Prama.Recursos.cancel;
            this.button1.Location = new System.Drawing.Point(63, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(45, 40);
            this.button1.TabIndex = 32;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Image = global::Prama.Recursos.Salir;
            this.btnSalir.Location = new System.Drawing.Point(601, 9);
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
            // chkCHCartera
            // 
            this.chkCHCartera.AutoSize = true;
            this.chkCHCartera.Location = new System.Drawing.Point(12, 8);
            this.chkCHCartera.Name = "chkCHCartera";
            this.chkCHCartera.Size = new System.Drawing.Size(119, 17);
            this.chkCHCartera.TabIndex = 70;
            this.chkCHCartera.Text = "Cheques en cartera";
            this.chkCHCartera.UseVisualStyleBackColor = true;
            this.chkCHCartera.CheckedChanged += new System.EventHandler(this.chkCHCartera_CheckedChanged);
            // 
            // gpbBusquedas
            // 
            this.gpbBusquedas.Controls.Add(this.txtNro);
            this.gpbBusquedas.Controls.Add(this.label1);
            this.gpbBusquedas.Controls.Add(this.txtFC);
            this.gpbBusquedas.Controls.Add(this.lblFC);
            this.gpbBusquedas.Controls.Add(this.txtFE);
            this.gpbBusquedas.Controls.Add(this.lblFecha);
            this.gpbBusquedas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbBusquedas.Location = new System.Drawing.Point(12, 433);
            this.gpbBusquedas.Name = "gpbBusquedas";
            this.gpbBusquedas.Size = new System.Drawing.Size(482, 55);
            this.gpbBusquedas.TabIndex = 71;
            this.gpbBusquedas.TabStop = false;
            this.gpbBusquedas.Text = "Búsquedas :";
            // 
            // txtNro
            // 
            this.txtNro.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNro.Location = new System.Drawing.Point(368, 24);
            this.txtNro.MaxLength = 8;
            this.txtNro.Name = "txtNro";
            this.txtNro.Size = new System.Drawing.Size(108, 20);
            this.txtNro.TabIndex = 53;
            this.txtNro.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNro.Click += new System.EventHandler(this.txtNro_Click);
            this.txtNro.TextChanged += new System.EventHandler(this.txtNro_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(315, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "Número:";
            // 
            // txtFC
            // 
            this.txtFC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFC.Location = new System.Drawing.Point(230, 24);
            this.txtFC.MaxLength = 10;
            this.txtFC.Name = "txtFC";
            this.txtFC.Size = new System.Drawing.Size(74, 20);
            this.txtFC.TabIndex = 51;
            this.txtFC.Click += new System.EventHandler(this.txtFC_Click);
            this.txtFC.TextChanged += new System.EventHandler(this.txtFC_TextChanged);
            // 
            // lblFC
            // 
            this.lblFC.AutoSize = true;
            this.lblFC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFC.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFC.Location = new System.Drawing.Point(153, 27);
            this.lblFC.Name = "lblFC";
            this.lblFC.Size = new System.Drawing.Size(71, 13);
            this.lblFC.TabIndex = 50;
            this.lblFC.Text = "Fecha Cobro:";
            // 
            // txtFE
            // 
            this.txtFE.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFE.Location = new System.Drawing.Point(80, 24);
            this.txtFE.MaxLength = 10;
            this.txtFE.Name = "txtFE";
            this.txtFE.Size = new System.Drawing.Size(66, 20);
            this.txtFE.TabIndex = 47;
            this.txtFE.Click += new System.EventHandler(this.txtFE_Click);
            this.txtFE.TextChanged += new System.EventHandler(this.txtFE_TextChanged);
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFecha.Location = new System.Drawing.Point(6, 27);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(68, 13);
            this.lblFecha.TabIndex = 46;
            this.lblFecha.Text = "Fecha Emis.:";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Numero";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn1.HeaderText = "Numero";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "FechaEmision";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn2.HeaderText = "Fecha Emisión";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "FechaCobro";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn3.HeaderText = "Fecha Cobro";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // numericGridColumn1
            // 
            this.numericGridColumn1.DataPropertyName = "Importe";
            this.numericGridColumn1.DecimalDigits = 2;
            this.numericGridColumn1.DecimalSeparator = ".";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.numericGridColumn1.DefaultCellStyle = dataGridViewCellStyle10;
            this.numericGridColumn1.HeaderText = "Importe";
            this.numericGridColumn1.Name = "numericGridColumn1";
            this.numericGridColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.numericGridColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Banco";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewTextBoxColumn4.HeaderText = "Banco";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // frmCajaCheques
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 564);
            this.Controls.Add(this.gpbBusquedas);
            this.Controls.Add(this.chkCHCartera);
            this.Controls.Add(this.txtEfectivoGral);
            this.Controls.Add(this.lblSaldo);
            this.Controls.Add(this.btnPanel);
            this.Controls.Add(this.dgvCajaCH);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCajaCheques";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " DETALLE - DE LA CAJA DE CHEQUES";
            this.Load += new System.EventHandler(this.frmCajaCheques_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCajaCH)).EndInit();
            this.btnPanel.ResumeLayout(false);
            this.gpbBusquedas.ResumeLayout(false);
            this.gpbBusquedas.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCajaCH;
        private System.Windows.Forms.TextBox txtEfectivoGral;
        private System.Windows.Forms.Label lblSaldo;
        private System.Windows.Forms.Panel btnPanel;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.CheckBox chkCHCartera;
        private System.Windows.Forms.DataGridViewTextBoxColumn Numero;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaEmision;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaCobro;
        private NumericGridColumn Importe;
        private System.Windows.Forms.DataGridViewTextBoxColumn Banco;
        private System.Windows.Forms.DataGridViewCheckBoxColumn EnCartera;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox gpbBusquedas;
        private System.Windows.Forms.TextBox txtFC;
        private System.Windows.Forms.Label lblFC;
        private System.Windows.Forms.TextBox txtFE;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.TextBox txtNro;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private NumericGridColumn numericGridColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}
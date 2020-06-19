namespace Prama.Formularios.Caja
{
    partial class frmCajaOtros
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCajaOtros));
            this.btnPanel = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtEfectivoGral = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvCajaEfectivo = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numericGridColumn1 = new Prama.NumericGridColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Imputacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Movimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comprobante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CajaEfectivoTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Importe = new Prama.NumericGridColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdCajaOtros = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdCaja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdCajaImputacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdCajaOtrosTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCajaEfectivo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPanel
            // 
            this.btnPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPanel.Controls.Add(this.btnSalir);
            this.btnPanel.Controls.Add(this.btnImprimir);
            this.btnPanel.Controls.Add(this.btnBuscar);
            this.btnPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPanel.Location = new System.Drawing.Point(0, 513);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(894, 58);
            this.btnPanel.TabIndex = 51;
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Image = global::Prama.Recursos.Salir;
            this.btnSalir.Location = new System.Drawing.Point(837, 9);
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
            this.btnImprimir.Location = new System.Drawing.Point(62, 9);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(45, 40);
            this.btnImprimir.TabIndex = 15;
            this.btnImprimir.UseVisualStyleBackColor = true;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::Prama.Recursos.find;
            this.btnBuscar.Location = new System.Drawing.Point(12, 9);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(45, 40);
            this.btnBuscar.TabIndex = 14;
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnBuscar.UseVisualStyleBackColor = true;
            // 
            // txtEfectivoGral
            // 
            this.txtEfectivoGral.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEfectivoGral.Location = new System.Drawing.Point(752, 481);
            this.txtEfectivoGral.MaxLength = 70;
            this.txtEfectivoGral.Name = "txtEfectivoGral";
            this.txtEfectivoGral.Size = new System.Drawing.Size(130, 26);
            this.txtEfectivoGral.TabIndex = 65;
            this.txtEfectivoGral.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(650, 487);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 17);
            this.label6.TabIndex = 66;
            this.label6.Text = "Total de caja :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvCajaEfectivo
            // 
            this.dgvCajaEfectivo.AllowUserToAddRows = false;
            this.dgvCajaEfectivo.AllowUserToDeleteRows = false;
            this.dgvCajaEfectivo.AllowUserToResizeColumns = false;
            this.dgvCajaEfectivo.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvCajaEfectivo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCajaEfectivo.ColumnHeadersHeight = 21;
            this.dgvCajaEfectivo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCajaEfectivo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FechaS,
            this.Imputacion,
            this.Movimiento,
            this.Comprobante,
            this.CajaEfectivoTipo,
            this.Importe,
            this.Fecha,
            this.IdCajaOtros,
            this.IdCaja,
            this.IdCajaImputacion,
            this.IdCajaOtrosTipo});
            this.dgvCajaEfectivo.Location = new System.Drawing.Point(12, 12);
            this.dgvCajaEfectivo.Name = "dgvCajaEfectivo";
            this.dgvCajaEfectivo.ReadOnly = true;
            this.dgvCajaEfectivo.RowHeadersVisible = false;
            this.dgvCajaEfectivo.RowHeadersWidth = 20;
            this.dgvCajaEfectivo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCajaEfectivo.Size = new System.Drawing.Size(870, 463);
            this.dgvCajaEfectivo.StandardTab = true;
            this.dgvCajaEfectivo.TabIndex = 64;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "FechaS";
            this.dataGridViewTextBoxColumn1.HeaderText = "Fecha";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 70;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Imputacion";
            this.dataGridViewTextBoxColumn2.HeaderText = "Concepto";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Movimiento";
            this.dataGridViewTextBoxColumn3.HeaderText = "Movimiento";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 320;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Comprobante";
            this.dataGridViewTextBoxColumn4.HeaderText = "Comprobante";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "CajaEfectivoTipo";
            this.dataGridViewTextBoxColumn5.HeaderText = "Tipo Movimiento";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 125;
            // 
            // numericGridColumn1
            // 
            this.numericGridColumn1.DataPropertyName = "Importe";
            this.numericGridColumn1.DecimalDigits = 2;
            this.numericGridColumn1.DecimalSeparator = ".";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.numericGridColumn1.DefaultCellStyle = dataGridViewCellStyle3;
            this.numericGridColumn1.HeaderText = "Importe";
            this.numericGridColumn1.Name = "numericGridColumn1";
            this.numericGridColumn1.Width = 80;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Fecha";
            this.dataGridViewTextBoxColumn6.HeaderText = "FechaReal";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Visible = false;
            this.dataGridViewTextBoxColumn6.Width = 70;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "IdCajaEfectivo";
            this.dataGridViewTextBoxColumn7.HeaderText = "IdCajaEfectivo";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Visible = false;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "IdCaja";
            this.dataGridViewTextBoxColumn8.HeaderText = "IdCaja";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Visible = false;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "IdCajaImputacion";
            this.dataGridViewTextBoxColumn9.HeaderText = "IdCajaImputacion";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Visible = false;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "IdCajaEfectivoTipo";
            this.dataGridViewTextBoxColumn10.HeaderText = "IdCajaEfectivoTipo";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.Visible = false;
            // 
            // FechaS
            // 
            this.FechaS.DataPropertyName = "FechaS";
            this.FechaS.HeaderText = "Fecha";
            this.FechaS.Name = "FechaS";
            this.FechaS.ReadOnly = true;
            this.FechaS.Width = 70;
            // 
            // Imputacion
            // 
            this.Imputacion.DataPropertyName = "Imputacion";
            this.Imputacion.HeaderText = "Concepto";
            this.Imputacion.Name = "Imputacion";
            this.Imputacion.ReadOnly = true;
            this.Imputacion.Width = 150;
            // 
            // Movimiento
            // 
            this.Movimiento.DataPropertyName = "Movimiento";
            this.Movimiento.HeaderText = "Movimiento";
            this.Movimiento.Name = "Movimiento";
            this.Movimiento.ReadOnly = true;
            this.Movimiento.Width = 320;
            // 
            // Comprobante
            // 
            this.Comprobante.DataPropertyName = "Comprobante";
            this.Comprobante.HeaderText = "Comprobante";
            this.Comprobante.Name = "Comprobante";
            this.Comprobante.ReadOnly = true;
            // 
            // CajaEfectivoTipo
            // 
            this.CajaEfectivoTipo.DataPropertyName = "CajaOtrosTipo";
            this.CajaEfectivoTipo.HeaderText = "Tipo Movimiento";
            this.CajaEfectivoTipo.Name = "CajaEfectivoTipo";
            this.CajaEfectivoTipo.ReadOnly = true;
            this.CajaEfectivoTipo.Width = 125;
            // 
            // Importe
            // 
            this.Importe.DataPropertyName = "Importe";
            this.Importe.DecimalDigits = 2;
            this.Importe.DecimalSeparator = ".";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Importe.DefaultCellStyle = dataGridViewCellStyle2;
            this.Importe.HeaderText = "Importe";
            this.Importe.Name = "Importe";
            this.Importe.ReadOnly = true;
            this.Importe.Width = 80;
            // 
            // Fecha
            // 
            this.Fecha.DataPropertyName = "Fecha";
            this.Fecha.HeaderText = "FechaReal";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            this.Fecha.Visible = false;
            this.Fecha.Width = 70;
            // 
            // IdCajaOtros
            // 
            this.IdCajaOtros.DataPropertyName = "IdCajaOtros";
            this.IdCajaOtros.HeaderText = "IdCajaEfectivo";
            this.IdCajaOtros.Name = "IdCajaOtros";
            this.IdCajaOtros.ReadOnly = true;
            this.IdCajaOtros.Visible = false;
            // 
            // IdCaja
            // 
            this.IdCaja.DataPropertyName = "IdCaja";
            this.IdCaja.HeaderText = "IdCaja";
            this.IdCaja.Name = "IdCaja";
            this.IdCaja.ReadOnly = true;
            this.IdCaja.Visible = false;
            // 
            // IdCajaImputacion
            // 
            this.IdCajaImputacion.DataPropertyName = "IdCajaImputacion";
            this.IdCajaImputacion.HeaderText = "IdCajaImputacion";
            this.IdCajaImputacion.Name = "IdCajaImputacion";
            this.IdCajaImputacion.ReadOnly = true;
            this.IdCajaImputacion.Visible = false;
            // 
            // IdCajaOtrosTipo
            // 
            this.IdCajaOtrosTipo.DataPropertyName = "IdCajaOtrosTipo";
            this.IdCajaOtrosTipo.HeaderText = "IdCajaEfectivoTipo";
            this.IdCajaOtrosTipo.Name = "IdCajaOtrosTipo";
            this.IdCajaOtrosTipo.ReadOnly = true;
            this.IdCajaOtrosTipo.Visible = false;
            // 
            // frmCajaOtros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 571);
            this.Controls.Add(this.txtEfectivoGral);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dgvCajaEfectivo);
            this.Controls.Add(this.btnPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCajaOtros";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCajaOtros";
            this.Load += new System.EventHandler(this.frmCajaOtros_Load);
            this.btnPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCajaEfectivo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel btnPanel;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtEfectivoGral;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvCajaEfectivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private NumericGridColumn numericGridColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaS;
        private System.Windows.Forms.DataGridViewTextBoxColumn Imputacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Movimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comprobante;
        private System.Windows.Forms.DataGridViewTextBoxColumn CajaEfectivoTipo;
        private NumericGridColumn Importe;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdCajaOtros;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdCaja;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdCajaImputacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdCajaOtrosTipo;
    }
}
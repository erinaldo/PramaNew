namespace Prama.Formularios.Caja
{
    partial class frmCajaTransferencias
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
            this.txtEfectivoGral = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnPanel = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dgvCajaEfectivo = new System.Windows.Forms.DataGridView();
            this.FechaS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Imputacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Movimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comprobante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CajaEfectivoTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Importe = new Prama.NumericGridColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdCajaEfectivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdCaja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdCajaImputacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdCajaEfectivoTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCajaEfectivo)).BeginInit();
            this.SuspendLayout();
            // 
            // txtEfectivoGral
            // 
            this.txtEfectivoGral.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEfectivoGral.Location = new System.Drawing.Point(752, 481);
            this.txtEfectivoGral.MaxLength = 70;
            this.txtEfectivoGral.Name = "txtEfectivoGral";
            this.txtEfectivoGral.Size = new System.Drawing.Size(130, 26);
            this.txtEfectivoGral.TabIndex = 66;
            this.txtEfectivoGral.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(650, 481);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 20);
            this.label6.TabIndex = 67;
            this.label6.Text = "Total de caja :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.btnPanel.TabIndex = 65;
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
            this.IdCajaEfectivo,
            this.IdCaja,
            this.IdCajaImputacion,
            this.IdCajaEfectivoTipo});
            this.dgvCajaEfectivo.Location = new System.Drawing.Point(12, 12);
            this.dgvCajaEfectivo.Name = "dgvCajaEfectivo";
            this.dgvCajaEfectivo.ReadOnly = true;
            this.dgvCajaEfectivo.RowHeadersVisible = false;
            this.dgvCajaEfectivo.RowHeadersWidth = 20;
            this.dgvCajaEfectivo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCajaEfectivo.Size = new System.Drawing.Size(870, 456);
            this.dgvCajaEfectivo.StandardTab = true;
            this.dgvCajaEfectivo.TabIndex = 64;
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
            this.CajaEfectivoTipo.DataPropertyName = "CajaEfectivoTipo";
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
            // IdCajaEfectivo
            // 
            this.IdCajaEfectivo.DataPropertyName = "IdCajaEfectivo";
            this.IdCajaEfectivo.HeaderText = "IdCajaEfectivo";
            this.IdCajaEfectivo.Name = "IdCajaEfectivo";
            this.IdCajaEfectivo.ReadOnly = true;
            this.IdCajaEfectivo.Visible = false;
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
            // IdCajaEfectivoTipo
            // 
            this.IdCajaEfectivoTipo.DataPropertyName = "IdCajaEfectivoTipo";
            this.IdCajaEfectivoTipo.HeaderText = "IdCajaEfectivoTipo";
            this.IdCajaEfectivoTipo.Name = "IdCajaEfectivoTipo";
            this.IdCajaEfectivoTipo.ReadOnly = true;
            this.IdCajaEfectivoTipo.Visible = false;
            // 
            // frmCajaTransferencias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 571);
            this.Controls.Add(this.txtEfectivoGral);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnPanel);
            this.Controls.Add(this.dgvCajaEfectivo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCajaTransferencias";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " - DETALLE DE LA CAJA TRANSFERENCIAS";
            this.btnPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCajaEfectivo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtEfectivoGral;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel btnPanel;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dgvCajaEfectivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaS;
        private System.Windows.Forms.DataGridViewTextBoxColumn Imputacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Movimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comprobante;
        private System.Windows.Forms.DataGridViewTextBoxColumn CajaEfectivoTipo;
        private NumericGridColumn Importe;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdCajaEfectivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdCaja;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdCajaImputacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdCajaEfectivoTipo;
    }
}
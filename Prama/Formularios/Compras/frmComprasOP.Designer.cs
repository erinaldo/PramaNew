namespace Prama
{
    partial class frmComprasOP
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmComprasOP));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvOPs = new System.Windows.Forms.DataGridView();
            this.gpbBusquedas = new System.Windows.Forms.GroupBox();
            this.txtBuscarIdCli = new System.Windows.Forms.TextBox();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.txtBuscarCLI = new System.Windows.Forms.TextBox();
            this.lblBuscaCLI = new System.Windows.Forms.Label();
            this.gpbDetalle = new System.Windows.Forms.GroupBox();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.IdOrdenPagoComprobante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdOrdenPagoD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdComprobanteCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Imputacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Importe = new Prama.NumericGridColumn();
            this.IdImputacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPanel = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip4 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip5 = new System.Windows.Forms.ToolTip(this.components);
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
            this.numericGridColumn1 = new Prama.NumericGridColumn();
            this.IdOrdenPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Numero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdProveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RazonSocial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaReal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new Prama.NumericGridColumn();
            this.Usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Efectivo = new Prama.NumericGridColumn();
            this.Cheques = new Prama.NumericGridColumn();
            this.Bancos = new Prama.NumericGridColumn();
            this.SaldoAFavor = new Prama.NumericGridColumn();
            this.CUIT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOPs)).BeginInit();
            this.gpbBusquedas.SuspendLayout();
            this.gpbDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.btnPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvOPs);
            this.groupBox1.Location = new System.Drawing.Point(10, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(708, 196);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Órdenes de pago";
            // 
            // dgvOPs
            // 
            this.dgvOPs.AllowUserToAddRows = false;
            this.dgvOPs.AllowUserToDeleteRows = false;
            this.dgvOPs.AllowUserToResizeColumns = false;
            this.dgvOPs.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvOPs.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOPs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvOPs.ColumnHeadersHeight = 21;
            this.dgvOPs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvOPs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdOrdenPago,
            this.Fecha,
            this.Numero,
            this.IdProveedor,
            this.RazonSocial,
            this.FechaReal,
            this.Total,
            this.Usuario,
            this.Efectivo,
            this.Cheques,
            this.Bancos,
            this.SaldoAFavor,
            this.CUIT});
            this.dgvOPs.Location = new System.Drawing.Point(6, 19);
            this.dgvOPs.MultiSelect = false;
            this.dgvOPs.Name = "dgvOPs";
            this.dgvOPs.ReadOnly = true;
            this.dgvOPs.RowHeadersVisible = false;
            this.dgvOPs.RowHeadersWidth = 20;
            this.dgvOPs.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvOPs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOPs.Size = new System.Drawing.Size(692, 171);
            this.dgvOPs.StandardTab = true;
            this.dgvOPs.TabIndex = 36;
            this.dgvOPs.SelectionChanged += new System.EventHandler(this.dgvOPs_SelectionChanged);
            // 
            // gpbBusquedas
            // 
            this.gpbBusquedas.Controls.Add(this.txtBuscarIdCli);
            this.gpbBusquedas.Controls.Add(this.lblCodigo);
            this.gpbBusquedas.Controls.Add(this.txtBuscarCLI);
            this.gpbBusquedas.Controls.Add(this.lblBuscaCLI);
            this.gpbBusquedas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbBusquedas.Location = new System.Drawing.Point(10, 413);
            this.gpbBusquedas.Name = "gpbBusquedas";
            this.gpbBusquedas.Size = new System.Drawing.Size(706, 54);
            this.gpbBusquedas.TabIndex = 46;
            this.gpbBusquedas.TabStop = false;
            this.gpbBusquedas.Text = "Búsquedas :";
            this.gpbBusquedas.Visible = false;
            // 
            // txtBuscarIdCli
            // 
            this.txtBuscarIdCli.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarIdCli.Location = new System.Drawing.Point(83, 18);
            this.txtBuscarIdCli.MaxLength = 8;
            this.txtBuscarIdCli.Name = "txtBuscarIdCli";
            this.txtBuscarIdCli.Size = new System.Drawing.Size(84, 20);
            this.txtBuscarIdCli.TabIndex = 23;
            this.txtBuscarIdCli.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCodigo.Location = new System.Drawing.Point(34, 21);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(43, 13);
            this.lblCodigo.TabIndex = 22;
            this.lblCodigo.Text = "Código:";
            // 
            // txtBuscarCLI
            // 
            this.txtBuscarCLI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarCLI.Location = new System.Drawing.Point(228, 18);
            this.txtBuscarCLI.MaxLength = 100;
            this.txtBuscarCLI.Name = "txtBuscarCLI";
            this.txtBuscarCLI.Size = new System.Drawing.Size(257, 20);
            this.txtBuscarCLI.TabIndex = 25;
            // 
            // lblBuscaCLI
            // 
            this.lblBuscaCLI.AutoSize = true;
            this.lblBuscaCLI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscaCLI.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblBuscaCLI.Location = new System.Drawing.Point(180, 21);
            this.lblBuscaCLI.Name = "lblBuscaCLI";
            this.lblBuscaCLI.Size = new System.Drawing.Size(42, 13);
            this.lblBuscaCLI.TabIndex = 24;
            this.lblBuscaCLI.Text = "Cliente:";
            // 
            // gpbDetalle
            // 
            this.gpbDetalle.Controls.Add(this.dgvDetalle);
            this.gpbDetalle.Location = new System.Drawing.Point(10, 225);
            this.gpbDetalle.Name = "gpbDetalle";
            this.gpbDetalle.Size = new System.Drawing.Size(708, 242);
            this.gpbDetalle.TabIndex = 45;
            this.gpbDetalle.TabStop = false;
            this.gpbDetalle.Text = "Comprobantes incluídos en la Órden de Pago ";
            // 
            // dgvDetalle
            // 
            this.dgvDetalle.AllowUserToAddRows = false;
            this.dgvDetalle.AllowUserToDeleteRows = false;
            this.dgvDetalle.AllowUserToResizeColumns = false;
            this.dgvDetalle.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvDetalle.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetalle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvDetalle.ColumnHeadersHeight = 21;
            this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdOrdenPagoComprobante,
            this.IdOrdenPagoD,
            this.IdComprobanteCompra,
            this.FechaD,
            this.NumeroD,
            this.Imputacion,
            this.Importe,
            this.IdImputacion});
            this.dgvDetalle.Location = new System.Drawing.Point(6, 19);
            this.dgvDetalle.MultiSelect = false;
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.ReadOnly = true;
            this.dgvDetalle.RowHeadersVisible = false;
            this.dgvDetalle.RowHeadersWidth = 20;
            this.dgvDetalle.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalle.Size = new System.Drawing.Size(692, 205);
            this.dgvDetalle.StandardTab = true;
            this.dgvDetalle.TabIndex = 36;
            // 
            // IdOrdenPagoComprobante
            // 
            this.IdOrdenPagoComprobante.DataPropertyName = "IdOrdenPagoComprobante";
            this.IdOrdenPagoComprobante.HeaderText = "IdOrdenPagoComprobante";
            this.IdOrdenPagoComprobante.Name = "IdOrdenPagoComprobante";
            this.IdOrdenPagoComprobante.ReadOnly = true;
            this.IdOrdenPagoComprobante.Visible = false;
            // 
            // IdOrdenPagoD
            // 
            this.IdOrdenPagoD.DataPropertyName = "IdOrdenPago";
            this.IdOrdenPagoD.HeaderText = "IdOrdenPago";
            this.IdOrdenPagoD.Name = "IdOrdenPagoD";
            this.IdOrdenPagoD.ReadOnly = true;
            this.IdOrdenPagoD.Visible = false;
            // 
            // IdComprobanteCompra
            // 
            this.IdComprobanteCompra.DataPropertyName = "IdComprobanteCompra";
            this.IdComprobanteCompra.HeaderText = "IdComprobanteCompra";
            this.IdComprobanteCompra.Name = "IdComprobanteCompra";
            this.IdComprobanteCompra.ReadOnly = true;
            this.IdComprobanteCompra.Visible = false;
            // 
            // FechaD
            // 
            this.FechaD.DataPropertyName = "Fecha";
            this.FechaD.HeaderText = "Fecha";
            this.FechaD.Name = "FechaD";
            this.FechaD.ReadOnly = true;
            this.FechaD.Width = 70;
            // 
            // NumeroD
            // 
            this.NumeroD.DataPropertyName = "Numero";
            this.NumeroD.HeaderText = "Numero";
            this.NumeroD.Name = "NumeroD";
            this.NumeroD.ReadOnly = true;
            // 
            // Imputacion
            // 
            this.Imputacion.DataPropertyName = "Imputacion";
            this.Imputacion.HeaderText = "Imputacion";
            this.Imputacion.Name = "Imputacion";
            this.Imputacion.ReadOnly = true;
            this.Imputacion.Width = 400;
            // 
            // Importe
            // 
            this.Importe.DataPropertyName = "Importe";
            this.Importe.DecimalDigits = 2;
            this.Importe.DecimalSeparator = ".";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Importe.DefaultCellStyle = dataGridViewCellStyle8;
            this.Importe.HeaderText = "Importe";
            this.Importe.Name = "Importe";
            this.Importe.ReadOnly = true;
            this.Importe.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Importe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // IdImputacion
            // 
            this.IdImputacion.DataPropertyName = "IdImputacion";
            this.IdImputacion.HeaderText = "IdImputacion";
            this.IdImputacion.Name = "IdImputacion";
            this.IdImputacion.ReadOnly = true;
            this.IdImputacion.Visible = false;
            // 
            // btnPanel
            // 
            this.btnPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPanel.Controls.Add(this.btnCancelar);
            this.btnPanel.Controls.Add(this.btnAceptar);
            this.btnPanel.Controls.Add(this.btnImprimir);
            this.btnPanel.Controls.Add(this.btnBuscar);
            this.btnPanel.Controls.Add(this.btnSalir);
            this.btnPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPanel.Location = new System.Drawing.Point(0, 483);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(730, 58);
            this.btnPanel.TabIndex = 47;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Image = global::Prama.Recursos.cancel;
            this.btnCancelar.Location = new System.Drawing.Point(355, 9);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(45, 40);
            this.btnCancelar.TabIndex = 12;
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Visible = false;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Image = global::Prama.Recursos.Aceptar;
            this.btnAceptar.Location = new System.Drawing.Point(304, 9);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(45, 40);
            this.btnAceptar.TabIndex = 11;
            this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = global::Prama.Recursos.printer;
            this.btnImprimir.Location = new System.Drawing.Point(61, 9);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(45, 40);
            this.btnImprimir.TabIndex = 10;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::Prama.Recursos.find;
            this.btnBuscar.Location = new System.Drawing.Point(10, 9);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(45, 40);
            this.btnBuscar.TabIndex = 9;
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnBuscar.UseVisualStyleBackColor = true;
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Image = global::Prama.Recursos.Salir;
            this.btnSalir.Location = new System.Drawing.Point(673, 9);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(45, 40);
            this.btnSalir.TabIndex = 13;
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalir.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "FechaF";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn1.HeaderText = "Fecha";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Tipo";
            this.dataGridViewTextBoxColumn2.HeaderText = "Tipo Comprobante";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Comprobante";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn3.HeaderText = "Comprobante";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 300;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Importe";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewTextBoxColumn4.HeaderText = "Total";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "IdOrdenPago";
            this.dataGridViewTextBoxColumn5.HeaderText = "IdOrdenPago";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Visible = false;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "IdProveedor";
            this.dataGridViewTextBoxColumn6.HeaderText = "IdProveedor";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "RazonSocial";
            this.dataGridViewTextBoxColumn7.HeaderText = "RazonSocial";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "Fecha";
            this.dataGridViewTextBoxColumn8.HeaderText = "Fecha";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "FechaReal";
            this.dataGridViewTextBoxColumn9.HeaderText = "FechaReal";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "Numero";
            this.dataGridViewTextBoxColumn10.HeaderText = "Numero";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // numericGridColumn1
            // 
            this.numericGridColumn1.DataPropertyName = "Total";
            this.numericGridColumn1.DecimalDigits = 2;
            this.numericGridColumn1.DecimalSeparator = ".";
            this.numericGridColumn1.HeaderText = "Total";
            this.numericGridColumn1.Name = "numericGridColumn1";
            // 
            // IdOrdenPago
            // 
            this.IdOrdenPago.DataPropertyName = "IdOrdenPago";
            this.IdOrdenPago.HeaderText = "IdOrdenPago";
            this.IdOrdenPago.Name = "IdOrdenPago";
            this.IdOrdenPago.ReadOnly = true;
            this.IdOrdenPago.Visible = false;
            // 
            // Fecha
            // 
            this.Fecha.DataPropertyName = "Fecha";
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            this.Fecha.Width = 70;
            // 
            // Numero
            // 
            this.Numero.DataPropertyName = "Numero";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Numero.DefaultCellStyle = dataGridViewCellStyle3;
            this.Numero.HeaderText = "Numero";
            this.Numero.Name = "Numero";
            this.Numero.ReadOnly = true;
            this.Numero.Width = 60;
            // 
            // IdProveedor
            // 
            this.IdProveedor.DataPropertyName = "IdProveedor";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IdProveedor.DefaultCellStyle = dataGridViewCellStyle4;
            this.IdProveedor.HeaderText = "Código";
            this.IdProveedor.Name = "IdProveedor";
            this.IdProveedor.ReadOnly = true;
            this.IdProveedor.Width = 60;
            // 
            // RazonSocial
            // 
            this.RazonSocial.DataPropertyName = "RazonSocial";
            this.RazonSocial.HeaderText = "RazonSocial";
            this.RazonSocial.Name = "RazonSocial";
            this.RazonSocial.ReadOnly = true;
            this.RazonSocial.Width = 260;
            // 
            // FechaReal
            // 
            this.FechaReal.DataPropertyName = "FechaReal";
            this.FechaReal.HeaderText = "FechaReal";
            this.FechaReal.Name = "FechaReal";
            this.FechaReal.ReadOnly = true;
            this.FechaReal.Visible = false;
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
            this.Total.Width = 70;
            // 
            // Usuario
            // 
            this.Usuario.DataPropertyName = "Usuario";
            this.Usuario.HeaderText = "Usuario";
            this.Usuario.Name = "Usuario";
            this.Usuario.ReadOnly = true;
            this.Usuario.Width = 150;
            // 
            // Efectivo
            // 
            this.Efectivo.DataPropertyName = "Efectivo";
            this.Efectivo.DecimalDigits = 2;
            this.Efectivo.DecimalSeparator = ".";
            this.Efectivo.HeaderText = "Efvo";
            this.Efectivo.Name = "Efectivo";
            this.Efectivo.ReadOnly = true;
            this.Efectivo.Visible = false;
            // 
            // Cheques
            // 
            this.Cheques.DataPropertyName = "Cheques";
            this.Cheques.DecimalDigits = 2;
            this.Cheques.DecimalSeparator = ".";
            this.Cheques.HeaderText = "Cheques";
            this.Cheques.Name = "Cheques";
            this.Cheques.ReadOnly = true;
            this.Cheques.Visible = false;
            // 
            // Bancos
            // 
            this.Bancos.DataPropertyName = "Bancos";
            this.Bancos.DecimalDigits = 2;
            this.Bancos.DecimalSeparator = ".";
            this.Bancos.HeaderText = "Bancos";
            this.Bancos.Name = "Bancos";
            this.Bancos.ReadOnly = true;
            this.Bancos.Visible = false;
            // 
            // SaldoAFavor
            // 
            this.SaldoAFavor.DataPropertyName = "SaldoAFavor";
            this.SaldoAFavor.DecimalDigits = 2;
            this.SaldoAFavor.DecimalSeparator = ".";
            this.SaldoAFavor.HeaderText = "SaldoAFavor";
            this.SaldoAFavor.Name = "SaldoAFavor";
            this.SaldoAFavor.ReadOnly = true;
            this.SaldoAFavor.Visible = false;
            // 
            // CUIT
            // 
            this.CUIT.DataPropertyName = "CUIT";
            this.CUIT.HeaderText = "CUIT";
            this.CUIT.Name = "CUIT";
            this.CUIT.ReadOnly = true;
            this.CUIT.Visible = false;
            // 
            // frmComprasOP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnSalir;
            this.ClientSize = new System.Drawing.Size(730, 541);
            this.Controls.Add(this.btnPanel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gpbBusquedas);
            this.Controls.Add(this.gpbDetalle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmComprasOP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " - ÓRDENES DE PAGO";
            this.Load += new System.EventHandler(this.frmComprasOP_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOPs)).EndInit();
            this.gpbBusquedas.ResumeLayout(false);
            this.gpbBusquedas.PerformLayout();
            this.gpbDetalle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.btnPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvOPs;
        private System.Windows.Forms.GroupBox gpbBusquedas;
        private System.Windows.Forms.TextBox txtBuscarIdCli;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.TextBox txtBuscarCLI;
        private System.Windows.Forms.Label lblBuscaCLI;
        private System.Windows.Forms.GroupBox gpbDetalle;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.Panel btnPanel;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.ToolTip toolTip3;
        private System.Windows.Forms.ToolTip toolTip4;
        private System.Windows.Forms.ToolTip toolTip5;
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
        private NumericGridColumn numericGridColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdOrdenPagoComprobante;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdOrdenPagoD;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdComprobanteCompra;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaD;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroD;
        private System.Windows.Forms.DataGridViewTextBoxColumn Imputacion;
        private NumericGridColumn Importe;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdImputacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdOrdenPago;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Numero;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdProveedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn RazonSocial;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaReal;
        private NumericGridColumn Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn Usuario;
        private NumericGridColumn Efectivo;
        private NumericGridColumn Cheques;
        private NumericGridColumn Bancos;
        private NumericGridColumn SaldoAFavor;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUIT;
    }
}
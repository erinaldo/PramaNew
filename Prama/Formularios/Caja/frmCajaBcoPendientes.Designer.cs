namespace Prama.Formularios.Caja
{
    partial class frmCajaBcoPendientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCajaBcoPendientes));
            this.btnPanel = new System.Windows.Forms.Panel();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.gpbBusquedas = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtCupon = new System.Windows.Forms.TextBox();
            this.lblMovimiento = new System.Windows.Forms.Label();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.txtSaldoBcoPendiente = new System.Windows.Forms.TextBox();
            this.lblSaldoCaja = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSdoAcred = new System.Windows.Forms.TextBox();
            this.lblSdoAcred = new System.Windows.Forms.Label();
            this.dgvCaja = new System.Windows.Forms.DataGridView();
            this.PuntoNro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaMov = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sNroOp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sTarjeta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RazonSocial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdTipoMovimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pendiente = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IdCajaBcoPendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdCajaAsociacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPanel.SuspendLayout();
            this.gpbBusquedas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCaja)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPanel
            // 
            this.btnPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPanel.Controls.Add(this.btnAceptar);
            this.btnPanel.Controls.Add(this.btnSalir);
            this.btnPanel.Controls.Add(this.btnImprimir);
            this.btnPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPanel.Location = new System.Drawing.Point(0, 523);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(891, 58);
            this.btnPanel.TabIndex = 65;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Image = global::Prama.Recursos.Aceptar;
            this.btnAceptar.Location = new System.Drawing.Point(782, 9);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(45, 40);
            this.btnAceptar.TabIndex = 74;
            this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Image = global::Prama.Recursos.Salir;
            this.btnSalir.Location = new System.Drawing.Point(833, 9);
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
            this.btnImprimir.Visible = false;
            // 
            // gpbBusquedas
            // 
            this.gpbBusquedas.Controls.Add(this.button1);
            this.gpbBusquedas.Controls.Add(this.txtCupon);
            this.gpbBusquedas.Controls.Add(this.lblMovimiento);
            this.gpbBusquedas.Controls.Add(this.txtFecha);
            this.gpbBusquedas.Controls.Add(this.lblFecha);
            this.gpbBusquedas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbBusquedas.Location = new System.Drawing.Point(12, 439);
            this.gpbBusquedas.Name = "gpbBusquedas";
            this.gpbBusquedas.Size = new System.Drawing.Size(357, 69);
            this.gpbBusquedas.TabIndex = 70;
            this.gpbBusquedas.TabStop = false;
            this.gpbBusquedas.Text = "Búsquedas :";
            // 
            // button1
            // 
            this.button1.Image = global::Prama.Recursos.cancel;
            this.button1.Location = new System.Drawing.Point(298, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(45, 40);
            this.button1.TabIndex = 32;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // txtCupon
            // 
            this.txtCupon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCupon.Location = new System.Drawing.Point(197, 24);
            this.txtCupon.MaxLength = 100;
            this.txtCupon.Name = "txtCupon";
            this.txtCupon.Size = new System.Drawing.Size(95, 20);
            this.txtCupon.TabIndex = 51;
            this.txtCupon.Click += new System.EventHandler(this.txtCupon_Click);
            this.txtCupon.TextChanged += new System.EventHandler(this.txtCupon_TextChanged);
            // 
            // lblMovimiento
            // 
            this.lblMovimiento.AutoSize = true;
            this.lblMovimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMovimiento.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblMovimiento.Location = new System.Drawing.Point(135, 27);
            this.lblMovimiento.Name = "lblMovimiento";
            this.lblMovimiento.Size = new System.Drawing.Size(56, 13);
            this.lblMovimiento.TabIndex = 50;
            this.lblMovimiento.Text = "N° Cupón:";
            // 
            // txtFecha
            // 
            this.txtFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFecha.Location = new System.Drawing.Point(55, 24);
            this.txtFecha.MaxLength = 25;
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(74, 20);
            this.txtFecha.TabIndex = 47;
            this.txtFecha.Click += new System.EventHandler(this.txtFecha_Click);
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
            // txtSaldoBcoPendiente
            // 
            this.txtSaldoBcoPendiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaldoBcoPendiente.Location = new System.Drawing.Point(388, 465);
            this.txtSaldoBcoPendiente.MaxLength = 70;
            this.txtSaldoBcoPendiente.Name = "txtSaldoBcoPendiente";
            this.txtSaldoBcoPendiente.Size = new System.Drawing.Size(151, 26);
            this.txtSaldoBcoPendiente.TabIndex = 68;
            this.txtSaldoBcoPendiente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSaldoCaja
            // 
            this.lblSaldoCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaldoCaja.Location = new System.Drawing.Point(385, 439);
            this.lblSaldoCaja.Name = "lblSaldoCaja";
            this.lblSaldoCaja.Size = new System.Drawing.Size(165, 23);
            this.lblSaldoCaja.TabIndex = 69;
            this.lblSaldoCaja.Text = "Saldo Bancos Pendientes:";
            this.lblSaldoCaja.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(891, 581);
            this.shapeContainer1.TabIndex = 71;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 759;
            this.lineShape1.X2 = 759;
            this.lineShape1.Y1 = 428;
            this.lineShape1.Y2 = 514;
            // 
            // btnQuitar
            // 
            this.btnQuitar.Image = global::Prama.Recursos.Borrar;
            this.btnQuitar.Location = new System.Drawing.Point(783, 451);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(45, 40);
            this.btnQuitar.TabIndex = 73;
            this.btnQuitar.UseVisualStyleBackColor = true;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Image = global::Prama.Recursos.Agregar;
            this.btnAgregar.Location = new System.Drawing.Point(834, 451);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(45, 40);
            this.btnAgregar.TabIndex = 72;
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "PuntoNro";
            this.dataGridViewTextBoxColumn1.HeaderText = "Recibo N°";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "FechaMov";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn2.HeaderText = "Fecha";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 80;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "sNroOp";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn3.HeaderText = "N° Cupón";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 80;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "sTarjeta";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn4.HeaderText = "Tarjeta";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 110;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "RazonSocial";
            this.dataGridViewTextBoxColumn5.HeaderText = "RazonSocial";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 150;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Descripcion";
            this.dataGridViewTextBoxColumn6.HeaderText = "Descripcion";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 150;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "IdTipoMovimiento";
            this.dataGridViewTextBoxColumn7.HeaderText = "IdTipoMovimiento";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Visible = false;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "Importe";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn8.HeaderText = "Importe";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 90;
            // 
            // txtSdoAcred
            // 
            this.txtSdoAcred.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSdoAcred.Location = new System.Drawing.Point(560, 465);
            this.txtSdoAcred.MaxLength = 70;
            this.txtSdoAcred.Name = "txtSdoAcred";
            this.txtSdoAcred.Size = new System.Drawing.Size(151, 26);
            this.txtSdoAcred.TabIndex = 74;
            this.txtSdoAcred.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSdoAcred
            // 
            this.lblSdoAcred.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSdoAcred.Location = new System.Drawing.Point(557, 439);
            this.lblSdoAcred.Name = "lblSdoAcred";
            this.lblSdoAcred.Size = new System.Drawing.Size(165, 23);
            this.lblSdoAcred.TabIndex = 75;
            this.lblSdoAcred.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvCaja
            // 
            this.dgvCaja.AllowUserToAddRows = false;
            this.dgvCaja.AllowUserToDeleteRows = false;
            this.dgvCaja.AllowUserToResizeColumns = false;
            this.dgvCaja.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvCaja.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvCaja.ColumnHeadersHeight = 21;
            this.dgvCaja.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCaja.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PuntoNro,
            this.FechaMov,
            this.sNroOp,
            this.sTarjeta,
            this.RazonSocial,
            this.Descripcion,
            this.IdTipoMovimiento,
            this.Importe,
            this.Pendiente,
            this.IdCajaBcoPendiente,
            this.IdCajaAsociacion});
            this.dgvCaja.Location = new System.Drawing.Point(12, 12);
            this.dgvCaja.Name = "dgvCaja";
            this.dgvCaja.ReadOnly = true;
            this.dgvCaja.RowHeadersVisible = false;
            this.dgvCaja.RowHeadersWidth = 20;
            this.dgvCaja.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCaja.Size = new System.Drawing.Size(866, 407);
            this.dgvCaja.StandardTab = true;
            this.dgvCaja.TabIndex = 4;
            this.dgvCaja.SelectionChanged += new System.EventHandler(this.dgvCaja_SelectionChanged);
            // 
            // PuntoNro
            // 
            this.PuntoNro.DataPropertyName = "PuntoNro";
            this.PuntoNro.HeaderText = "Recibo N°";
            this.PuntoNro.Name = "PuntoNro";
            this.PuntoNro.ReadOnly = true;
            // 
            // FechaMov
            // 
            this.FechaMov.DataPropertyName = "FechaMov";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.FechaMov.DefaultCellStyle = dataGridViewCellStyle6;
            this.FechaMov.HeaderText = "Fecha";
            this.FechaMov.Name = "FechaMov";
            this.FechaMov.ReadOnly = true;
            this.FechaMov.Width = 80;
            // 
            // sNroOp
            // 
            this.sNroOp.DataPropertyName = "sNroOp";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.sNroOp.DefaultCellStyle = dataGridViewCellStyle7;
            this.sNroOp.HeaderText = "N° Cupón";
            this.sNroOp.Name = "sNroOp";
            this.sNroOp.ReadOnly = true;
            this.sNroOp.Width = 80;
            // 
            // sTarjeta
            // 
            this.sTarjeta.DataPropertyName = "sTarjeta";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.sTarjeta.DefaultCellStyle = dataGridViewCellStyle8;
            this.sTarjeta.HeaderText = "Tarjeta";
            this.sTarjeta.Name = "sTarjeta";
            this.sTarjeta.ReadOnly = true;
            this.sTarjeta.Width = 110;
            // 
            // RazonSocial
            // 
            this.RazonSocial.DataPropertyName = "RazonSocial";
            this.RazonSocial.HeaderText = "RazonSocial";
            this.RazonSocial.Name = "RazonSocial";
            this.RazonSocial.ReadOnly = true;
            this.RazonSocial.Width = 150;
            // 
            // Descripcion
            // 
            this.Descripcion.DataPropertyName = "Descripcion";
            this.Descripcion.HeaderText = "Descripcion";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            this.Descripcion.Width = 150;
            // 
            // IdTipoMovimiento
            // 
            this.IdTipoMovimiento.DataPropertyName = "IdTipoMovimiento";
            this.IdTipoMovimiento.HeaderText = "IdTipoMovimiento";
            this.IdTipoMovimiento.Name = "IdTipoMovimiento";
            this.IdTipoMovimiento.ReadOnly = true;
            this.IdTipoMovimiento.Visible = false;
            // 
            // Importe
            // 
            this.Importe.DataPropertyName = "Importe";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Importe.DefaultCellStyle = dataGridViewCellStyle9;
            this.Importe.HeaderText = "Importe";
            this.Importe.Name = "Importe";
            this.Importe.ReadOnly = true;
            this.Importe.Width = 90;
            // 
            // Pendiente
            // 
            this.Pendiente.DataPropertyName = "Pendiente";
            this.Pendiente.HeaderText = "Pendiente";
            this.Pendiente.Name = "Pendiente";
            this.Pendiente.ReadOnly = true;
            this.Pendiente.Width = 70;
            // 
            // IdCajaBcoPendiente
            // 
            this.IdCajaBcoPendiente.DataPropertyName = "IdCajaBcoPendiente";
            this.IdCajaBcoPendiente.HeaderText = "IdCajaBcoPendiente";
            this.IdCajaBcoPendiente.Name = "IdCajaBcoPendiente";
            this.IdCajaBcoPendiente.ReadOnly = true;
            this.IdCajaBcoPendiente.Visible = false;
            // 
            // IdCajaAsociacion
            // 
            this.IdCajaAsociacion.DataPropertyName = "IdCajaAsociacion";
            this.IdCajaAsociacion.HeaderText = "IdCajaAsociacion";
            this.IdCajaAsociacion.Name = "IdCajaAsociacion";
            this.IdCajaAsociacion.ReadOnly = true;
            this.IdCajaAsociacion.Visible = false;
            // 
            // frmCajaBcoPendientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 581);
            this.Controls.Add(this.lblSdoAcred);
            this.Controls.Add(this.txtSdoAcred);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.gpbBusquedas);
            this.Controls.Add(this.txtSaldoBcoPendiente);
            this.Controls.Add(this.lblSaldoCaja);
            this.Controls.Add(this.btnPanel);
            this.Controls.Add(this.dgvCaja);
            this.Controls.Add(this.shapeContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCajaBcoPendientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " - DETALLE CAJA BANCOS PENDIENTES";
            this.Load += new System.EventHandler(this.frmCajaBcoPendientes_Load);
            this.btnPanel.ResumeLayout(false);
            this.gpbBusquedas.ResumeLayout(false);
            this.gpbBusquedas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCaja)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel btnPanel;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.GroupBox gpbBusquedas;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtCupon;
        private System.Windows.Forms.Label lblMovimiento;
        private System.Windows.Forms.TextBox txtFecha;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.TextBox txtSaldoBcoPendiente;
        private System.Windows.Forms.Label lblSaldoCaja;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.TextBox txtSdoAcred;
        private System.Windows.Forms.Label lblSdoAcred;
        private System.Windows.Forms.DataGridView dgvCaja;
        private System.Windows.Forms.DataGridViewTextBoxColumn PuntoNro;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaMov;
        private System.Windows.Forms.DataGridViewTextBoxColumn sNroOp;
        private System.Windows.Forms.DataGridViewTextBoxColumn sTarjeta;
        private System.Windows.Forms.DataGridViewTextBoxColumn RazonSocial;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdTipoMovimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Importe;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Pendiente;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdCajaBcoPendiente;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdCajaAsociacion;
    }
}
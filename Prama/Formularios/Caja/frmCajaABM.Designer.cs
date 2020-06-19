namespace Prama.Formularios.Caja
{
    partial class frmCajaABM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCajaABM));
            this.gpbCabecera = new System.Windows.Forms.GroupBox();
            this.txtImputacion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboTipoMov = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label21 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.cboImputacion = new System.Windows.Forms.ComboBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPanel = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.gpbDetalle = new System.Windows.Forms.GroupBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDetalle = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCh = new System.Windows.Forms.Button();
            this.txtCheques = new System.Windows.Forms.TextBox();
            this.chkCheques = new System.Windows.Forms.CheckBox();
            this.txtBancos = new System.Windows.Forms.TextBox();
            this.cboBancos = new System.Windows.Forms.ComboBox();
            this.ckBancos = new System.Windows.Forms.CheckBox();
            this.chkEfectivo = new System.Windows.Forms.CheckBox();
            this.txtEfectivo = new System.Windows.Forms.TextBox();
            this.dsReportes1 = new Prama.dsReportes();
            this.gpbCabecera.SuspendLayout();
            this.btnPanel.SuspendLayout();
            this.gpbDetalle.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsReportes1)).BeginInit();
            this.SuspendLayout();
            // 
            // gpbCabecera
            // 
            this.gpbCabecera.Controls.Add(this.txtImputacion);
            this.gpbCabecera.Controls.Add(this.label2);
            this.gpbCabecera.Controls.Add(this.cboTipoMov);
            this.gpbCabecera.Controls.Add(this.label5);
            this.gpbCabecera.Controls.Add(this.dtpFecha);
            this.gpbCabecera.Controls.Add(this.label21);
            this.gpbCabecera.Controls.Add(this.label23);
            this.gpbCabecera.Controls.Add(this.cboImputacion);
            this.gpbCabecera.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbCabecera.Location = new System.Drawing.Point(12, 12);
            this.gpbCabecera.Name = "gpbCabecera";
            this.gpbCabecera.Size = new System.Drawing.Size(293, 278);
            this.gpbCabecera.TabIndex = 0;
            this.gpbCabecera.TabStop = false;
            this.gpbCabecera.Text = " Datos Generales ";
            // 
            // txtImputacion
            // 
            this.txtImputacion.Enabled = false;
            this.txtImputacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImputacion.Location = new System.Drawing.Point(9, 231);
            this.txtImputacion.MaxLength = 30;
            this.txtImputacion.Name = "txtImputacion";
            this.txtImputacion.Size = new System.Drawing.Size(99, 20);
            this.txtImputacion.TabIndex = 3;
            this.txtImputacion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(12, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 168;
            this.label2.Text = "Tipo de movimiento :";
            // 
            // cboTipoMov
            // 
            this.cboTipoMov.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoMov.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTipoMov.FormattingEnabled = true;
            this.cboTipoMov.Items.AddRange(new object[] {
            "Ingresos",
            "Egresos"});
            this.cboTipoMov.Location = new System.Drawing.Point(9, 115);
            this.cboTipoMov.Name = "cboTipoMov";
            this.cboTipoMov.Size = new System.Drawing.Size(188, 21);
            this.cboTipoMov.TabIndex = 1;
            this.cboTipoMov.SelectedIndexChanged += new System.EventHandler(this.cboTipoMov_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 37;
            this.label5.Text = "Fecha :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpFecha
            // 
            this.dtpFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(9, 61);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(99, 20);
            this.dtpFecha.TabIndex = 0;
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(12, 212);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(75, 13);
            this.label21.TabIndex = 160;
            this.label21.Text = "CTA. CTBLE.:";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label23.Location = new System.Drawing.Point(12, 149);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(65, 13);
            this.label23.TabIndex = 157;
            this.label23.Text = "Imputación :";
            // 
            // cboImputacion
            // 
            this.cboImputacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboImputacion.Enabled = false;
            this.cboImputacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboImputacion.FormattingEnabled = true;
            this.cboImputacion.Location = new System.Drawing.Point(9, 165);
            this.cboImputacion.Name = "cboImputacion";
            this.cboImputacion.Size = new System.Drawing.Size(275, 21);
            this.cboImputacion.TabIndex = 2;
            this.cboImputacion.SelectedIndexChanged += new System.EventHandler(this.cboImputacion_SelectedIndexChanged);
            this.cboImputacion.Click += new System.EventHandler(this.cboImputacion_Click);
            // 
            // txtUsuario
            // 
            this.txtUsuario.Enabled = false;
            this.txtUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuario.Location = new System.Drawing.Point(204, 41);
            this.txtUsuario.MaxLength = 30;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(142, 20);
            this.txtUsuario.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(144, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 162;
            this.label1.Text = "Usuario :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPanel
            // 
            this.btnPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPanel.Controls.Add(this.btnCancelar);
            this.btnPanel.Controls.Add(this.btnAceptar);
            this.btnPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPanel.Location = new System.Drawing.Point(0, 305);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(802, 58);
            this.btnPanel.TabIndex = 6;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = global::Prama.Recursos.cancel;
            this.btnCancelar.Location = new System.Drawing.Point(409, 6);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(45, 40);
            this.btnCancelar.TabIndex = 15;
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Image = global::Prama.Recursos.Aceptar;
            this.btnAceptar.Location = new System.Drawing.Point(358, 6);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(45, 40);
            this.btnAceptar.TabIndex = 14;
            this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // gpbDetalle
            // 
            this.gpbDetalle.Controls.Add(this.txtUsuario);
            this.gpbDetalle.Controls.Add(this.txtTotal);
            this.gpbDetalle.Controls.Add(this.label3);
            this.gpbDetalle.Controls.Add(this.label1);
            this.gpbDetalle.Controls.Add(this.label4);
            this.gpbDetalle.Controls.Add(this.txtDetalle);
            this.gpbDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbDetalle.Location = new System.Drawing.Point(311, 12);
            this.gpbDetalle.Name = "gpbDetalle";
            this.gpbDetalle.Size = new System.Drawing.Size(479, 136);
            this.gpbDetalle.TabIndex = 1;
            this.gpbDetalle.TabStop = false;
            this.gpbDetalle.Text = "Detalle del Movimineto ";
            // 
            // txtTotal
            // 
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(9, 41);
            this.txtTotal.MaxLength = 30;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(105, 20);
            this.txtTotal.TabIndex = 4;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTotal_KeyPress);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 164;
            this.label3.Text = "TOTAL :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(6, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 159;
            this.label4.Text = "Descripción :";
            // 
            // txtDetalle
            // 
            this.txtDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDetalle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtDetalle.Location = new System.Drawing.Point(9, 88);
            this.txtDetalle.MaxLength = 150;
            this.txtDetalle.Name = "txtDetalle";
            this.txtDetalle.Size = new System.Drawing.Size(446, 20);
            this.txtDetalle.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCh);
            this.groupBox1.Controls.Add(this.txtCheques);
            this.groupBox1.Controls.Add(this.chkCheques);
            this.groupBox1.Controls.Add(this.txtBancos);
            this.groupBox1.Controls.Add(this.cboBancos);
            this.groupBox1.Controls.Add(this.ckBancos);
            this.groupBox1.Controls.Add(this.chkEfectivo);
            this.groupBox1.Controls.Add(this.txtEfectivo);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(311, 164);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(479, 126);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Origen o Destino de los Fondos ";
            // 
            // btnCh
            // 
            this.btnCh.Enabled = false;
            this.btnCh.Image = global::Prama.Recursos.vista;
            this.btnCh.Location = new System.Drawing.Point(195, 97);
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
            this.txtCheques.Location = new System.Drawing.Point(96, 100);
            this.txtCheques.MaxLength = 150;
            this.txtCheques.Name = "txtCheques";
            this.txtCheques.Size = new System.Drawing.Size(93, 20);
            this.txtCheques.TabIndex = 13;
            this.txtCheques.Text = "0.00";
            this.txtCheques.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCheques.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCheques_KeyPress);
            // 
            // chkCheques
            // 
            this.chkCheques.AutoSize = true;
            this.chkCheques.Location = new System.Drawing.Point(9, 100);
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
            this.txtBancos.Location = new System.Drawing.Point(361, 60);
            this.txtBancos.MaxLength = 150;
            this.txtBancos.Name = "txtBancos";
            this.txtBancos.Size = new System.Drawing.Size(112, 20);
            this.txtBancos.TabIndex = 11;
            this.txtBancos.Text = "0.00";
            this.txtBancos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBancos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBancos_KeyPress);
            // 
            // cboBancos
            // 
            this.cboBancos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBancos.Enabled = false;
            this.cboBancos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBancos.FormattingEnabled = true;
            this.cboBancos.Location = new System.Drawing.Point(96, 60);
            this.cboBancos.Name = "cboBancos";
            this.cboBancos.Size = new System.Drawing.Size(259, 21);
            this.cboBancos.TabIndex = 10;
            // 
            // ckBancos
            // 
            this.ckBancos.AutoSize = true;
            this.ckBancos.Location = new System.Drawing.Point(9, 64);
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
            this.txtEfectivo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEfectivo_KeyPress);
            // 
            // dsReportes1
            // 
            this.dsReportes1.DataSetName = "dsReportes";
            this.dsReportes1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // frmCajaABM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 363);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gpbDetalle);
            this.Controls.Add(this.btnPanel);
            this.Controls.Add(this.gpbCabecera);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCajaABM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MOVIMIENTOS DE CAJA";
            this.Load += new System.EventHandler(this.frmCajaABM_Load);
            this.gpbCabecera.ResumeLayout(false);
            this.gpbCabecera.PerformLayout();
            this.btnPanel.ResumeLayout(false);
            this.gpbDetalle.ResumeLayout(false);
            this.gpbDetalle.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsReportes1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpbCabecera;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtImputacion;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cboImputacion;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Panel btnPanel;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.GroupBox gpbDetalle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDetalle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCheques;
        private System.Windows.Forms.CheckBox chkCheques;
        private System.Windows.Forms.TextBox txtBancos;
        private System.Windows.Forms.ComboBox cboBancos;
        private System.Windows.Forms.CheckBox ckBancos;
        private System.Windows.Forms.CheckBox chkEfectivo;
        private System.Windows.Forms.TextBox txtEfectivo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboTipoMov;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCh;
        private dsReportes dsReportes1;
    }
}
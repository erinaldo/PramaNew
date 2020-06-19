namespace Prama.Formularios.Compras
{
    partial class frmComprasProvPagos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmComprasProvPagos));
            this.btnPanel = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtTotalFactura = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtSaldo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEfectivo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTransf = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPropios = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTerceros = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTotalOP = new System.Windows.Forms.TextBox();
            this.gpbComprobante = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtComprador = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDetalle = new System.Windows.Forms.TextBox();
            this.gpbDetalle = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPagador = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.btnPanel.SuspendLayout();
            this.gpbComprobante.SuspendLayout();
            this.gpbDetalle.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPanel
            // 
            this.btnPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPanel.Controls.Add(this.btnCancelar);
            this.btnPanel.Controls.Add(this.btnAceptar);
            this.btnPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPanel.Location = new System.Drawing.Point(0, 275);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(634, 58);
            this.btnPanel.TabIndex = 45;
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
            // lblFecha
            // 
            this.lblFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.Location = new System.Drawing.Point(8, 30);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(43, 13);
            this.lblFecha.TabIndex = 171;
            this.lblFecha.Text = "Fecha :";
            this.lblFecha.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpFecha
            // 
            this.dtpFecha.Enabled = false;
            this.dtpFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(84, 23);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(99, 20);
            this.dtpFecha.TabIndex = 170;
            // 
            // txtNumero
            // 
            this.txtNumero.Enabled = false;
            this.txtNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumero.Location = new System.Drawing.Point(238, 25);
            this.txtNumero.MaxLength = 30;
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(114, 20);
            this.txtNumero.TabIndex = 172;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(189, 30);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(47, 13);
            this.label19.TabIndex = 173;
            this.label19.Text = "Número:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTotalFactura
            // 
            this.txtTotalFactura.Enabled = false;
            this.txtTotalFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalFactura.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtTotalFactura.Location = new System.Drawing.Point(401, 25);
            this.txtTotalFactura.Name = "txtTotalFactura";
            this.txtTotalFactura.Size = new System.Drawing.Size(75, 20);
            this.txtTotalFactura.TabIndex = 174;
            this.txtTotalFactura.Text = "0.00";
            this.txtTotalFactura.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label18.Location = new System.Drawing.Point(358, 30);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(37, 13);
            this.label18.TabIndex = 175;
            this.label18.Text = "Total :";
            // 
            // txtSaldo
            // 
            this.txtSaldo.Enabled = false;
            this.txtSaldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaldo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtSaldo.Location = new System.Drawing.Point(525, 26);
            this.txtSaldo.Name = "txtSaldo";
            this.txtSaldo.Size = new System.Drawing.Size(75, 20);
            this.txtSaldo.TabIndex = 176;
            this.txtSaldo.Text = "0.00";
            this.txtSaldo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(482, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 177;
            this.label1.Text = "Saldo :";
            // 
            // txtEfectivo
            // 
            this.txtEfectivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEfectivo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtEfectivo.Location = new System.Drawing.Point(82, 25);
            this.txtEfectivo.Name = "txtEfectivo";
            this.txtEfectivo.Size = new System.Drawing.Size(75, 20);
            this.txtEfectivo.TabIndex = 178;
            this.txtEfectivo.Text = "0.00";
            this.txtEfectivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtEfectivo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEfectivo_KeyPress);
            this.txtEfectivo.Leave += new System.EventHandler(this.txtEfectivo_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(6, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 179;
            this.label2.Text = "Efectivo :";
            // 
            // txtTransf
            // 
            this.txtTransf.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTransf.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtTransf.Location = new System.Drawing.Point(215, 25);
            this.txtTransf.Name = "txtTransf";
            this.txtTransf.Size = new System.Drawing.Size(75, 20);
            this.txtTransf.TabIndex = 180;
            this.txtTransf.Text = "0.00";
            this.txtTransf.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTransf.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTransf_KeyPress);
            this.txtTransf.Leave += new System.EventHandler(this.txtTransf_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(163, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 181;
            this.label3.Text = "Transf. :";
            // 
            // txtPropios
            // 
            this.txtPropios.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPropios.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtPropios.Location = new System.Drawing.Point(370, 25);
            this.txtPropios.Name = "txtPropios";
            this.txtPropios.Size = new System.Drawing.Size(75, 20);
            this.txtPropios.TabIndex = 182;
            this.txtPropios.Text = "0.00";
            this.txtPropios.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPropios.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPropios_KeyPress);
            this.txtPropios.Leave += new System.EventHandler(this.txtPropios_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(296, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 183;
            this.label4.Text = "CH/Propios :";
            // 
            // txtTerceros
            // 
            this.txtTerceros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTerceros.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtTerceros.Location = new System.Drawing.Point(523, 25);
            this.txtTerceros.Name = "txtTerceros";
            this.txtTerceros.Size = new System.Drawing.Size(75, 20);
            this.txtTerceros.TabIndex = 184;
            this.txtTerceros.Text = "0.00";
            this.txtTerceros.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTerceros.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTerceros_KeyPress);
            this.txtTerceros.Leave += new System.EventHandler(this.txtTerceros_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(451, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 185;
            this.label5.Text = "CH/Tercer. :";
            // 
            // txtTotalOP
            // 
            this.txtTotalOP.Enabled = false;
            this.txtTotalOP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalOP.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtTotalOP.Location = new System.Drawing.Point(73, 29);
            this.txtTotalOP.Name = "txtTotalOP";
            this.txtTotalOP.Size = new System.Drawing.Size(92, 20);
            this.txtTotalOP.TabIndex = 186;
            this.txtTotalOP.Text = "0.00";
            this.txtTotalOP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // gpbComprobante
            // 
            this.gpbComprobante.Controls.Add(this.label8);
            this.gpbComprobante.Controls.Add(this.txtComprador);
            this.gpbComprobante.Controls.Add(this.label7);
            this.gpbComprobante.Controls.Add(this.txtDetalle);
            this.gpbComprobante.Controls.Add(this.lblFecha);
            this.gpbComprobante.Controls.Add(this.dtpFecha);
            this.gpbComprobante.Controls.Add(this.label19);
            this.gpbComprobante.Controls.Add(this.txtNumero);
            this.gpbComprobante.Controls.Add(this.label18);
            this.gpbComprobante.Controls.Add(this.txtTotalFactura);
            this.gpbComprobante.Controls.Add(this.label1);
            this.gpbComprobante.Controls.Add(this.txtSaldo);
            this.gpbComprobante.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbComprobante.Location = new System.Drawing.Point(12, 12);
            this.gpbComprobante.Name = "gpbComprobante";
            this.gpbComprobante.Size = new System.Drawing.Size(610, 105);
            this.gpbComprobante.TabIndex = 188;
            this.gpbComprobante.TabStop = false;
            this.gpbComprobante.Text = "Datos del comprobante :";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(8, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 181;
            this.label8.Text = "Comprador :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtComprador
            // 
            this.txtComprador.Enabled = false;
            this.txtComprador.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComprador.Location = new System.Drawing.Point(84, 77);
            this.txtComprador.MaxLength = 30;
            this.txtComprador.Name = "txtComprador";
            this.txtComprador.Size = new System.Drawing.Size(268, 20);
            this.txtComprador.TabIndex = 180;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 179;
            this.label7.Text = "Descripción :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDetalle
            // 
            this.txtDetalle.Enabled = false;
            this.txtDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDetalle.Location = new System.Drawing.Point(84, 51);
            this.txtDetalle.MaxLength = 30;
            this.txtDetalle.Name = "txtDetalle";
            this.txtDetalle.Size = new System.Drawing.Size(516, 20);
            this.txtDetalle.TabIndex = 178;
            // 
            // gpbDetalle
            // 
            this.gpbDetalle.Controls.Add(this.label6);
            this.gpbDetalle.Controls.Add(this.dtpFechaPago);
            this.gpbDetalle.Controls.Add(this.label9);
            this.gpbDetalle.Controls.Add(this.txtPagador);
            this.gpbDetalle.Controls.Add(this.txtEfectivo);
            this.gpbDetalle.Controls.Add(this.label2);
            this.gpbDetalle.Controls.Add(this.label3);
            this.gpbDetalle.Controls.Add(this.txtTransf);
            this.gpbDetalle.Controls.Add(this.txtTerceros);
            this.gpbDetalle.Controls.Add(this.label4);
            this.gpbDetalle.Controls.Add(this.label5);
            this.gpbDetalle.Controls.Add(this.txtPropios);
            this.gpbDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbDetalle.Location = new System.Drawing.Point(12, 123);
            this.gpbDetalle.Name = "gpbDetalle";
            this.gpbDetalle.Size = new System.Drawing.Size(608, 85);
            this.gpbDetalle.TabIndex = 189;
            this.gpbDetalle.TabStop = false;
            this.gpbDetalle.Text = "Detalle del pago :";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 189;
            this.label6.Text = "Fecha Pago :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpFechaPago
            // 
            this.dtpFechaPago.Enabled = false;
            this.dtpFechaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaPago.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaPago.Location = new System.Drawing.Point(84, 51);
            this.dtpFechaPago.Name = "dtpFechaPago";
            this.dtpFechaPago.Size = new System.Drawing.Size(99, 20);
            this.dtpFechaPago.TabIndex = 188;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(189, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 187;
            this.label9.Text = "Emite OP:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPagador
            // 
            this.txtPagador.Enabled = false;
            this.txtPagador.Location = new System.Drawing.Point(250, 51);
            this.txtPagador.MaxLength = 30;
            this.txtPagador.Name = "txtPagador";
            this.txtPagador.Size = new System.Drawing.Size(348, 20);
            this.txtPagador.TabIndex = 186;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTotalOP);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(447, 214);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(175, 55);
            this.groupBox1.TabIndex = 190;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Total de Orden de Pago :";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(634, 333);
            this.shapeContainer1.TabIndex = 191;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 13;
            this.lineShape1.X2 = 442;
            this.lineShape1.Y1 = 239;
            this.lineShape1.Y2 = 239;
            // 
            // frmComprasProvPagos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 333);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gpbDetalle);
            this.Controls.Add(this.gpbComprobante);
            this.Controls.Add(this.btnPanel);
            this.Controls.Add(this.shapeContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmComprasProvPagos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "- ORDEN DE PAGO";
            this.Load += new System.EventHandler(this.frmComprasProvPagos_Load);
            this.btnPanel.ResumeLayout(false);
            this.gpbComprobante.ResumeLayout(false);
            this.gpbComprobante.PerformLayout();
            this.gpbDetalle.ResumeLayout(false);
            this.gpbDetalle.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel btnPanel;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtTotalFactura;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtSaldo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEfectivo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTransf;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPropios;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTerceros;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTotalOP;
        private System.Windows.Forms.GroupBox gpbComprobante;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtComprador;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDetalle;
        private System.Windows.Forms.GroupBox gpbDetalle;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPagador;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpFechaPago;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
    }
}
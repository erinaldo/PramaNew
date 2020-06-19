namespace Prama.Formularios.Auxiliares
{
    partial class frmRotuloSnRemito
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRotuloSnRemito));
            this.label2 = new System.Windows.Forms.Label();
            this.txtValSeg = new System.Windows.Forms.TextBox();
            this.txtCantBultos = new System.Windows.Forms.TextBox();
            this.lblCantBultos = new System.Windows.Forms.Label();
            this.grpCliente = new System.Windows.Forms.GroupBox();
            this.btnEditCli = new System.Windows.Forms.Button();
            this.btnCli = new System.Windows.Forms.Button();
            this.lblRs = new System.Windows.Forms.Label();
            this.txtRazonSocial = new System.Windows.Forms.TextBox();
            this.lblCUIT = new System.Windows.Forms.Label();
            this.txtDir = new System.Windows.Forms.TextBox();
            this.txtCuit = new System.Windows.Forms.TextBox();
            this.lblDir = new System.Windows.Forms.Label();
            this.btnPanel = new System.Windows.Forms.Panel();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.lblTransporte = new System.Windows.Forms.Label();
            this.cboTransporte = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.btnTransporte = new System.Windows.Forms.Button();
            this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
            this.txtIdTransporte = new System.Windows.Forms.TextBox();
            this.grpCliente.SuspendLayout();
            this.btnPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(18, 255);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 21);
            this.label2.TabIndex = 22;
            this.label2.Text = "Valor seguro:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtValSeg
            // 
            this.txtValSeg.Location = new System.Drawing.Point(111, 255);
            this.txtValSeg.MaxLength = 6;
            this.txtValSeg.Name = "txtValSeg";
            this.txtValSeg.Size = new System.Drawing.Size(119, 20);
            this.txtValSeg.TabIndex = 7;
            this.txtValSeg.Text = "0.00";
            this.txtValSeg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtValSeg.Click += new System.EventHandler(this.txtValSeg_Click);
            this.txtValSeg.Enter += new System.EventHandler(this.txtValSeg_Enter);
            this.txtValSeg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValSeg_KeyPress);
            // 
            // txtCantBultos
            // 
            this.txtCantBultos.Location = new System.Drawing.Point(111, 222);
            this.txtCantBultos.MaxLength = 2;
            this.txtCantBultos.Name = "txtCantBultos";
            this.txtCantBultos.Size = new System.Drawing.Size(58, 20);
            this.txtCantBultos.TabIndex = 6;
            this.txtCantBultos.Text = "0";
            this.txtCantBultos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCantBultos.Click += new System.EventHandler(this.txtCantBultos_Click);
            this.txtCantBultos.Enter += new System.EventHandler(this.txtCantBultos_Enter);
            this.txtCantBultos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantBultos_KeyPress);
            // 
            // lblCantBultos
            // 
            this.lblCantBultos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantBultos.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCantBultos.Location = new System.Drawing.Point(18, 222);
            this.lblCantBultos.Name = "lblCantBultos";
            this.lblCantBultos.Size = new System.Drawing.Size(92, 21);
            this.lblCantBultos.TabIndex = 20;
            this.lblCantBultos.Text = "Cantidad bultos:";
            this.lblCantBultos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpCliente
            // 
            this.grpCliente.Controls.Add(this.btnEditCli);
            this.grpCliente.Controls.Add(this.btnCli);
            this.grpCliente.Controls.Add(this.lblRs);
            this.grpCliente.Controls.Add(this.txtRazonSocial);
            this.grpCliente.Controls.Add(this.lblCUIT);
            this.grpCliente.Controls.Add(this.txtDir);
            this.grpCliente.Controls.Add(this.txtCuit);
            this.grpCliente.Controls.Add(this.lblDir);
            this.grpCliente.Location = new System.Drawing.Point(21, 12);
            this.grpCliente.Name = "grpCliente";
            this.grpCliente.Size = new System.Drawing.Size(374, 164);
            this.grpCliente.TabIndex = 24;
            this.grpCliente.TabStop = false;
            this.grpCliente.Text = " Datos del Cliente";
            // 
            // btnEditCli
            // 
            this.btnEditCli.Enabled = false;
            this.btnEditCli.Image = global::Prama.Recursos.edicion;
            this.btnEditCli.Location = new System.Drawing.Point(244, 30);
            this.btnEditCli.Name = "btnEditCli";
            this.btnEditCli.Size = new System.Drawing.Size(25, 25);
            this.btnEditCli.TabIndex = 194;
            this.btnEditCli.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEditCli.UseVisualStyleBackColor = true;
            this.btnEditCli.Click += new System.EventHandler(this.btnEditCli_Click);
            // 
            // btnCli
            // 
            this.btnCli.Image = global::Prama.Recursos._1488758204_clients;
            this.btnCli.Location = new System.Drawing.Point(213, 30);
            this.btnCli.Name = "btnCli";
            this.btnCli.Size = new System.Drawing.Size(25, 25);
            this.btnCli.TabIndex = 1;
            this.btnCli.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCli.UseVisualStyleBackColor = true;
            this.btnCli.Click += new System.EventHandler(this.btnCli_Click);
            // 
            // lblRs
            // 
            this.lblRs.AutoSize = true;
            this.lblRs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRs.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRs.Location = new System.Drawing.Point(5, 66);
            this.lblRs.Name = "lblRs";
            this.lblRs.Size = new System.Drawing.Size(73, 13);
            this.lblRs.TabIndex = 3;
            this.lblRs.Text = "Razón Social:";
            // 
            // txtRazonSocial
            // 
            this.txtRazonSocial.Enabled = false;
            this.txtRazonSocial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRazonSocial.Location = new System.Drawing.Point(84, 63);
            this.txtRazonSocial.MaxLength = 70;
            this.txtRazonSocial.Name = "txtRazonSocial";
            this.txtRazonSocial.ReadOnly = true;
            this.txtRazonSocial.Size = new System.Drawing.Size(280, 20);
            this.txtRazonSocial.TabIndex = 2;
            // 
            // lblCUIT
            // 
            this.lblCUIT.AutoSize = true;
            this.lblCUIT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCUIT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCUIT.Location = new System.Drawing.Point(40, 35);
            this.lblCUIT.Name = "lblCUIT";
            this.lblCUIT.Size = new System.Drawing.Size(38, 13);
            this.lblCUIT.TabIndex = 0;
            this.lblCUIT.Text = "CUIT :";
            // 
            // txtDir
            // 
            this.txtDir.Enabled = false;
            this.txtDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDir.Location = new System.Drawing.Point(84, 94);
            this.txtDir.MaxLength = 100;
            this.txtDir.Multiline = true;
            this.txtDir.Name = "txtDir";
            this.txtDir.ReadOnly = true;
            this.txtDir.Size = new System.Drawing.Size(280, 56);
            this.txtDir.TabIndex = 3;
            this.txtDir.Text = " ";
            // 
            // txtCuit
            // 
            this.txtCuit.Enabled = false;
            this.txtCuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCuit.Location = new System.Drawing.Point(84, 32);
            this.txtCuit.MaxLength = 13;
            this.txtCuit.Name = "txtCuit";
            this.txtCuit.Size = new System.Drawing.Size(121, 20);
            this.txtCuit.TabIndex = 0;
            this.txtCuit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDir
            // 
            this.lblDir.AutoSize = true;
            this.lblDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDir.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDir.Location = new System.Drawing.Point(21, 97);
            this.lblDir.Name = "lblDir";
            this.lblDir.Size = new System.Drawing.Size(55, 13);
            this.lblDir.TabIndex = 5;
            this.lblDir.Text = "Dirección:";
            // 
            // btnPanel
            // 
            this.btnPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPanel.Controls.Add(this.btnAceptar);
            this.btnPanel.Controls.Add(this.btnSalir);
            this.btnPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPanel.Location = new System.Drawing.Point(0, 289);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(403, 58);
            this.btnPanel.TabIndex = 26;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Image = global::Prama.Recursos.Aceptar;
            this.btnAceptar.Location = new System.Drawing.Point(300, 9);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(45, 40);
            this.btnAceptar.TabIndex = 8;
            this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Image = global::Prama.Recursos.Salir;
            this.btnSalir.Location = new System.Drawing.Point(350, 9);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(45, 40);
            this.btnSalir.TabIndex = 9;
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // lblTransporte
            // 
            this.lblTransporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransporte.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTransporte.Location = new System.Drawing.Point(42, 188);
            this.lblTransporte.Name = "lblTransporte";
            this.lblTransporte.Size = new System.Drawing.Size(61, 16);
            this.lblTransporte.TabIndex = 27;
            this.lblTransporte.Text = "Transporte:";
            // 
            // cboTransporte
            // 
            this.cboTransporte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTransporte.FormattingEnabled = true;
            this.cboTransporte.Location = new System.Drawing.Point(109, 188);
            this.cboTransporte.Name = "cboTransporte";
            this.cboTransporte.Size = new System.Drawing.Size(236, 21);
            this.cboTransporte.TabIndex = 4;
            // 
            // btnTransporte
            // 
            this.btnTransporte.Image = global::Prama.Recursos._1488757956_truck;
            this.btnTransporte.Location = new System.Drawing.Point(351, 184);
            this.btnTransporte.Name = "btnTransporte";
            this.btnTransporte.Size = new System.Drawing.Size(25, 25);
            this.btnTransporte.TabIndex = 5;
            this.btnTransporte.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTransporte.UseVisualStyleBackColor = true;
            this.btnTransporte.Click += new System.EventHandler(this.btnTransporte_Click);
            // 
            // txtIdTransporte
            // 
            this.txtIdTransporte.Location = new System.Drawing.Point(287, 215);
            this.txtIdTransporte.Name = "txtIdTransporte";
            this.txtIdTransporte.ReadOnly = true;
            this.txtIdTransporte.Size = new System.Drawing.Size(58, 20);
            this.txtIdTransporte.TabIndex = 28;
            this.txtIdTransporte.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtIdTransporte.Visible = false;
            // 
            // frmRotuloSnRemito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 347);
            this.Controls.Add(this.txtIdTransporte);
            this.Controls.Add(this.btnTransporte);
            this.Controls.Add(this.lblTransporte);
            this.Controls.Add(this.cboTransporte);
            this.Controls.Add(this.btnPanel);
            this.Controls.Add(this.grpCliente);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtValSeg);
            this.Controls.Add(this.txtCantBultos);
            this.Controls.Add(this.lblCantBultos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRotuloSnRemito";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " - ROTULADOR MANUAL";
            this.Load += new System.EventHandler(this.frmRotuloSnRemito_Load);
            this.grpCliente.ResumeLayout(false);
            this.grpCliente.PerformLayout();
            this.btnPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtValSeg;
        private System.Windows.Forms.TextBox txtCantBultos;
        private System.Windows.Forms.Label lblCantBultos;
        private System.Windows.Forms.GroupBox grpCliente;
        private System.Windows.Forms.Button btnCli;
        private System.Windows.Forms.Label lblRs;
        private System.Windows.Forms.TextBox txtRazonSocial;
        private System.Windows.Forms.Label lblCUIT;
        private System.Windows.Forms.TextBox txtDir;
        private System.Windows.Forms.TextBox txtCuit;
        private System.Windows.Forms.Label lblDir;
        private System.Windows.Forms.Panel btnPanel;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Label lblTransporte;
        private System.Windows.Forms.ComboBox cboTransporte;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.Button btnTransporte;
        private System.Windows.Forms.ToolTip toolTip3;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.TextBox txtIdTransporte;
        private System.Windows.Forms.Button btnEditCli;
    }
}
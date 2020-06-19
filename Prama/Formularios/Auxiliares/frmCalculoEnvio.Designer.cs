namespace Prama.Formularios.Auxiliares
{
    partial class frmCalculoEnvio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCalculoEnvio));
            this.btnPanel = new System.Windows.Forms.Panel();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.grpCalcularEnvio = new System.Windows.Forms.GroupBox();
            this.cboDestino = new System.Windows.Forms.ComboBox();
            this.lblDestino = new System.Windows.Forms.Label();
            this.cboPeso = new System.Windows.Forms.ComboBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCtraRB = new System.Windows.Forms.Label();
            this.lblEnvEstamp = new System.Windows.Forms.Label();
            this.lblPeso = new System.Windows.Forms.Label();
            this.lblImporte = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.txtGtoEnvio = new System.Windows.Forms.TextBox();
            this.txtCtraRB = new System.Windows.Forms.TextBox();
            this.txtEEstp = new System.Windows.Forms.TextBox();
            this.txtImpo = new System.Windows.Forms.TextBox();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.btnPanel.SuspendLayout();
            this.grpCalcularEnvio.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPanel
            // 
            this.btnPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPanel.Controls.Add(this.btnAceptar);
            this.btnPanel.Controls.Add(this.btnSalir);
            this.btnPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPanel.Location = new System.Drawing.Point(0, 295);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(320, 58);
            this.btnPanel.TabIndex = 27;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Image = global::Prama.Recursos.Aceptar;
            this.btnAceptar.Location = new System.Drawing.Point(214, 6);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(45, 40);
            this.btnAceptar.TabIndex = 6;
            this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Image = global::Prama.Recursos.Salir;
            this.btnSalir.Location = new System.Drawing.Point(263, 6);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(45, 40);
            this.btnSalir.TabIndex = 7;
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // grpCalcularEnvio
            // 
            this.grpCalcularEnvio.Controls.Add(this.cboDestino);
            this.grpCalcularEnvio.Controls.Add(this.lblDestino);
            this.grpCalcularEnvio.Controls.Add(this.cboPeso);
            this.grpCalcularEnvio.Controls.Add(this.lblTotal);
            this.grpCalcularEnvio.Controls.Add(this.label2);
            this.grpCalcularEnvio.Controls.Add(this.lblCtraRB);
            this.grpCalcularEnvio.Controls.Add(this.lblEnvEstamp);
            this.grpCalcularEnvio.Controls.Add(this.lblPeso);
            this.grpCalcularEnvio.Controls.Add(this.lblImporte);
            this.grpCalcularEnvio.Controls.Add(this.txtTotal);
            this.grpCalcularEnvio.Controls.Add(this.txtGtoEnvio);
            this.grpCalcularEnvio.Controls.Add(this.txtCtraRB);
            this.grpCalcularEnvio.Controls.Add(this.txtEEstp);
            this.grpCalcularEnvio.Controls.Add(this.txtImpo);
            this.grpCalcularEnvio.Controls.Add(this.shapeContainer1);
            this.grpCalcularEnvio.Location = new System.Drawing.Point(12, 12);
            this.grpCalcularEnvio.Name = "grpCalcularEnvio";
            this.grpCalcularEnvio.Size = new System.Drawing.Size(297, 264);
            this.grpCalcularEnvio.TabIndex = 28;
            this.grpCalcularEnvio.TabStop = false;
            this.grpCalcularEnvio.Text = " Calcular Envíos de Correo ";
            // 
            // cboDestino
            // 
            this.cboDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDestino.FormattingEnabled = true;
            this.cboDestino.Location = new System.Drawing.Point(120, 36);
            this.cboDestino.Name = "cboDestino";
            this.cboDestino.Size = new System.Drawing.Size(126, 21);
            this.cboDestino.TabIndex = 14;
            this.cboDestino.SelectedValueChanged += new System.EventHandler(this.cboDestino_SelectedValueChanged);
            // 
            // lblDestino
            // 
            this.lblDestino.Location = new System.Drawing.Point(70, 36);
            this.lblDestino.Name = "lblDestino";
            this.lblDestino.Size = new System.Drawing.Size(43, 20);
            this.lblDestino.TabIndex = 15;
            this.lblDestino.Text = "Destino:";
            // 
            // cboPeso
            // 
            this.cboPeso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPeso.FormattingEnabled = true;
            this.cboPeso.Location = new System.Drawing.Point(120, 92);
            this.cboPeso.Name = "cboPeso";
            this.cboPeso.Size = new System.Drawing.Size(126, 21);
            this.cboPeso.TabIndex = 1;
            this.cboPeso.SelectedValueChanged += new System.EventHandler(this.cboPeso_SelectedValueChanged);
            // 
            // lblTotal
            // 
            this.lblTotal.ForeColor = System.Drawing.Color.Black;
            this.lblTotal.Location = new System.Drawing.Point(64, 224);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(49, 20);
            this.lblTotal.TabIndex = 13;
            this.lblTotal.Text = "TOTAL:";
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(33, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Gasto Envío $:";
            // 
            // lblCtraRB
            // 
            this.lblCtraRB.ForeColor = System.Drawing.Color.Gray;
            this.lblCtraRB.Location = new System.Drawing.Point(20, 144);
            this.lblCtraRB.Name = "lblCtraRB";
            this.lblCtraRB.Size = new System.Drawing.Size(93, 20);
            this.lblCtraRB.TabIndex = 11;
            this.lblCtraRB.Text = "Contrareembolso:";
            // 
            // lblEnvEstamp
            // 
            this.lblEnvEstamp.ForeColor = System.Drawing.Color.Gray;
            this.lblEnvEstamp.Location = new System.Drawing.Point(20, 118);
            this.lblEnvEstamp.Name = "lblEnvEstamp";
            this.lblEnvEstamp.Size = new System.Drawing.Size(93, 20);
            this.lblEnvEstamp.TabIndex = 10;
            this.lblEnvEstamp.Text = "Envío ( estamp ):";
            // 
            // lblPeso
            // 
            this.lblPeso.Location = new System.Drawing.Point(70, 92);
            this.lblPeso.Name = "lblPeso";
            this.lblPeso.Size = new System.Drawing.Size(43, 20);
            this.lblPeso.TabIndex = 9;
            this.lblPeso.Text = "Peso:";
            // 
            // lblImporte
            // 
            this.lblImporte.Location = new System.Drawing.Point(67, 66);
            this.lblImporte.Name = "lblImporte";
            this.lblImporte.Size = new System.Drawing.Size(46, 20);
            this.lblImporte.TabIndex = 8;
            this.lblImporte.Text = "Importe:";
            // 
            // txtTotal
            // 
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(119, 224);
            this.txtTotal.MaxLength = 15;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(128, 20);
            this.txtTotal.TabIndex = 5;
            this.txtTotal.Text = "0.00";
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTotal_KeyPress);
            // 
            // txtGtoEnvio
            // 
            this.txtGtoEnvio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGtoEnvio.ForeColor = System.Drawing.Color.Red;
            this.txtGtoEnvio.Location = new System.Drawing.Point(119, 170);
            this.txtGtoEnvio.Name = "txtGtoEnvio";
            this.txtGtoEnvio.Size = new System.Drawing.Size(128, 20);
            this.txtGtoEnvio.TabIndex = 4;
            this.txtGtoEnvio.Text = "0.00";
            this.txtGtoEnvio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtGtoEnvio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGtoEnvio_KeyPress);
            // 
            // txtCtraRB
            // 
            this.txtCtraRB.ForeColor = System.Drawing.Color.DimGray;
            this.txtCtraRB.Location = new System.Drawing.Point(119, 144);
            this.txtCtraRB.MaxLength = 15;
            this.txtCtraRB.Name = "txtCtraRB";
            this.txtCtraRB.Size = new System.Drawing.Size(128, 20);
            this.txtCtraRB.TabIndex = 3;
            this.txtCtraRB.Text = "0.00";
            this.txtCtraRB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCtraRB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCtraRB_KeyPress);
            // 
            // txtEEstp
            // 
            this.txtEEstp.ForeColor = System.Drawing.Color.DimGray;
            this.txtEEstp.Location = new System.Drawing.Point(119, 118);
            this.txtEEstp.MaxLength = 15;
            this.txtEEstp.Name = "txtEEstp";
            this.txtEEstp.Size = new System.Drawing.Size(128, 20);
            this.txtEEstp.TabIndex = 2;
            this.txtEEstp.Text = "0.00";
            this.txtEEstp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtEEstp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEEstp_KeyPress);
            // 
            // txtImpo
            // 
            this.txtImpo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImpo.Location = new System.Drawing.Point(119, 66);
            this.txtImpo.MaxLength = 15;
            this.txtImpo.Name = "txtImpo";
            this.txtImpo.Size = new System.Drawing.Size(128, 20);
            this.txtImpo.TabIndex = 0;
            this.txtImpo.Text = "0.00";
            this.txtImpo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtImpo.Click += new System.EventHandler(this.txtImpo_Click);
            this.txtImpo.TextChanged += new System.EventHandler(this.txtImpo_TextChanged);
            this.txtImpo.Enter += new System.EventHandler(this.txtImpo_Enter);
            this.txtImpo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtImpo_KeyPress);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(3, 16);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(291, 245);
            this.shapeContainer1.TabIndex = 16;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 9;
            this.lineShape1.X2 = 280;
            this.lineShape1.Y1 = 190;
            this.lineShape1.Y2 = 190;
            // 
            // frmCalculoEnvio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 353);
            this.Controls.Add(this.grpCalcularEnvio);
            this.Controls.Add(this.btnPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCalculoEnvio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " - CALCULAR";
            this.Load += new System.EventHandler(this.frmCalculoEnvio_Load);
            this.btnPanel.ResumeLayout(false);
            this.grpCalcularEnvio.ResumeLayout(false);
            this.grpCalcularEnvio.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel btnPanel;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.GroupBox grpCalcularEnvio;
        private System.Windows.Forms.ComboBox cboPeso;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCtraRB;
        private System.Windows.Forms.Label lblEnvEstamp;
        private System.Windows.Forms.Label lblPeso;
        private System.Windows.Forms.Label lblImporte;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.TextBox txtGtoEnvio;
        private System.Windows.Forms.TextBox txtCtraRB;
        private System.Windows.Forms.TextBox txtEEstp;
        private System.Windows.Forms.TextBox txtImpo;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.ComboBox cboDestino;
        private System.Windows.Forms.Label lblDestino;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
    }
}
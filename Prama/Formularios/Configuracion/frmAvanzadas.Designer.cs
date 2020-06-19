namespace Prama
{
    partial class frmAvanzadas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAvanzadas));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.tabConfig = new System.Windows.Forms.TabControl();
            this.lblNF = new System.Windows.Forms.TabPage();
            this.txtCondicionIVA = new System.Windows.Forms.TextBox();
            this.txtLocalidad = new System.Windows.Forms.TextBox();
            this.txtCUIT = new System.Windows.Forms.TextBox();
            this.txtWeb = new System.Windows.Forms.TextBox();
            this.txtMail = new System.Windows.Forms.TextBox();
            this.txtTel = new System.Windows.Forms.TextBox();
            this.txtDom = new System.Windows.Forms.TextBox();
            this.txtNF = new System.Windows.Forms.TextBox();
            this.txtRS = new System.Windows.Forms.TextBox();
            this.lblCondIVA = new System.Windows.Forms.Label();
            this.lblLoc = new System.Windows.Forms.Label();
            this.lblCUIT = new System.Windows.Forms.Label();
            this.lblWeb = new System.Windows.Forms.Label();
            this.lblMail = new System.Windows.Forms.Label();
            this.lblTel = new System.Windows.Forms.Label();
            this.lblDom = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRS = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdoProd = new System.Windows.Forms.RadioButton();
            this.rdoTest = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblNota = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkAutoLoad = new System.Windows.Forms.CheckBox();
            this.chkIconTaskBar = new System.Windows.Forms.CheckBox();
            this.chkControlUser = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.tabConfig.SuspendLayout();
            this.lblNF.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.btnCancelar);
            this.panel1.Controls.Add(this.btnAceptar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 343);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(531, 68);
            this.panel1.TabIndex = 20;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Image = global::Prama.Recursos.cancel;
            this.btnCancelar.Location = new System.Drawing.Point(471, 14);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(45, 40);
            this.btnCancelar.TabIndex = 15;
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click_1);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Image = global::Prama.Recursos.Aceptar;
            this.btnAceptar.Location = new System.Drawing.Point(420, 14);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(45, 40);
            this.btnAceptar.TabIndex = 14;
            this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // tabConfig
            // 
            this.tabConfig.Controls.Add(this.lblNF);
            this.tabConfig.Controls.Add(this.tabPage1);
            this.tabConfig.Location = new System.Drawing.Point(12, 12);
            this.tabConfig.Name = "tabConfig";
            this.tabConfig.SelectedIndex = 0;
            this.tabConfig.Size = new System.Drawing.Size(508, 320);
            this.tabConfig.TabIndex = 21;
            // 
            // lblNF
            // 
            this.lblNF.Controls.Add(this.txtCondicionIVA);
            this.lblNF.Controls.Add(this.txtLocalidad);
            this.lblNF.Controls.Add(this.txtCUIT);
            this.lblNF.Controls.Add(this.txtWeb);
            this.lblNF.Controls.Add(this.txtMail);
            this.lblNF.Controls.Add(this.txtTel);
            this.lblNF.Controls.Add(this.txtDom);
            this.lblNF.Controls.Add(this.txtNF);
            this.lblNF.Controls.Add(this.txtRS);
            this.lblNF.Controls.Add(this.lblCondIVA);
            this.lblNF.Controls.Add(this.lblLoc);
            this.lblNF.Controls.Add(this.lblCUIT);
            this.lblNF.Controls.Add(this.lblWeb);
            this.lblNF.Controls.Add(this.lblMail);
            this.lblNF.Controls.Add(this.lblTel);
            this.lblNF.Controls.Add(this.lblDom);
            this.lblNF.Controls.Add(this.label1);
            this.lblNF.Controls.Add(this.lblRS);
            this.lblNF.Location = new System.Drawing.Point(4, 22);
            this.lblNF.Name = "lblNF";
            this.lblNF.Padding = new System.Windows.Forms.Padding(3);
            this.lblNF.Size = new System.Drawing.Size(500, 294);
            this.lblNF.TabIndex = 0;
            this.lblNF.Text = "Datos de la Empresa";
            this.lblNF.UseVisualStyleBackColor = true;
            // 
            // txtCondicionIVA
            // 
            this.txtCondicionIVA.Location = new System.Drawing.Point(149, 246);
            this.txtCondicionIVA.Name = "txtCondicionIVA";
            this.txtCondicionIVA.Size = new System.Drawing.Size(192, 20);
            this.txtCondicionIVA.TabIndex = 8;
            this.txtCondicionIVA.Enter += new System.EventHandler(this.txtCondicionIVA_Enter);
            // 
            // txtLocalidad
            // 
            this.txtLocalidad.Location = new System.Drawing.Point(149, 216);
            this.txtLocalidad.Name = "txtLocalidad";
            this.txtLocalidad.Size = new System.Drawing.Size(192, 20);
            this.txtLocalidad.TabIndex = 7;
            this.txtLocalidad.Enter += new System.EventHandler(this.txtLocalidad_Enter);
            // 
            // txtCUIT
            // 
            this.txtCUIT.Location = new System.Drawing.Point(149, 190);
            this.txtCUIT.Name = "txtCUIT";
            this.txtCUIT.Size = new System.Drawing.Size(133, 20);
            this.txtCUIT.TabIndex = 6;
            this.txtCUIT.Enter += new System.EventHandler(this.txtCUIT_Enter);
            // 
            // txtWeb
            // 
            this.txtWeb.Location = new System.Drawing.Point(149, 161);
            this.txtWeb.Name = "txtWeb";
            this.txtWeb.Size = new System.Drawing.Size(321, 20);
            this.txtWeb.TabIndex = 5;
            this.txtWeb.Enter += new System.EventHandler(this.txtWeb_Enter);
            // 
            // txtMail
            // 
            this.txtMail.Location = new System.Drawing.Point(149, 133);
            this.txtMail.Name = "txtMail";
            this.txtMail.Size = new System.Drawing.Size(229, 20);
            this.txtMail.TabIndex = 4;
            this.txtMail.Enter += new System.EventHandler(this.txtMail_Enter);
            // 
            // txtTel
            // 
            this.txtTel.Location = new System.Drawing.Point(149, 104);
            this.txtTel.Name = "txtTel";
            this.txtTel.Size = new System.Drawing.Size(158, 20);
            this.txtTel.TabIndex = 3;
            this.txtTel.Enter += new System.EventHandler(this.txtTel_Enter);
            // 
            // txtDom
            // 
            this.txtDom.Location = new System.Drawing.Point(149, 77);
            this.txtDom.Name = "txtDom";
            this.txtDom.Size = new System.Drawing.Size(321, 20);
            this.txtDom.TabIndex = 2;
            this.txtDom.Enter += new System.EventHandler(this.txtDom_Enter);
            // 
            // txtNF
            // 
            this.txtNF.Location = new System.Drawing.Point(149, 51);
            this.txtNF.Name = "txtNF";
            this.txtNF.Size = new System.Drawing.Size(321, 20);
            this.txtNF.TabIndex = 1;
            this.txtNF.Enter += new System.EventHandler(this.txtNF_Enter);
            // 
            // txtRS
            // 
            this.txtRS.Location = new System.Drawing.Point(149, 22);
            this.txtRS.Name = "txtRS";
            this.txtRS.Size = new System.Drawing.Size(321, 20);
            this.txtRS.TabIndex = 0;
            this.txtRS.Enter += new System.EventHandler(this.txtRS_Enter);
            // 
            // lblCondIVA
            // 
            this.lblCondIVA.Location = new System.Drawing.Point(18, 246);
            this.lblCondIVA.Name = "lblCondIVA";
            this.lblCondIVA.Size = new System.Drawing.Size(125, 16);
            this.lblCondIVA.TabIndex = 8;
            this.lblCondIVA.Text = "Condición frente al IVA: ";
            this.lblCondIVA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLoc
            // 
            this.lblLoc.Location = new System.Drawing.Point(18, 216);
            this.lblLoc.Name = "lblLoc";
            this.lblLoc.Size = new System.Drawing.Size(125, 16);
            this.lblLoc.TabIndex = 7;
            this.lblLoc.Text = "Localidad: ";
            this.lblLoc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCUIT
            // 
            this.lblCUIT.Location = new System.Drawing.Point(18, 190);
            this.lblCUIT.Name = "lblCUIT";
            this.lblCUIT.Size = new System.Drawing.Size(125, 16);
            this.lblCUIT.TabIndex = 6;
            this.lblCUIT.Text = "CUIT: ";
            this.lblCUIT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblWeb
            // 
            this.lblWeb.Location = new System.Drawing.Point(18, 161);
            this.lblWeb.Name = "lblWeb";
            this.lblWeb.Size = new System.Drawing.Size(125, 16);
            this.lblWeb.TabIndex = 5;
            this.lblWeb.Text = "Web: ";
            this.lblWeb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMail
            // 
            this.lblMail.Location = new System.Drawing.Point(18, 133);
            this.lblMail.Name = "lblMail";
            this.lblMail.Size = new System.Drawing.Size(125, 16);
            this.lblMail.TabIndex = 4;
            this.lblMail.Text = "Mail: ";
            this.lblMail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTel
            // 
            this.lblTel.Location = new System.Drawing.Point(18, 104);
            this.lblTel.Name = "lblTel";
            this.lblTel.Size = new System.Drawing.Size(125, 16);
            this.lblTel.TabIndex = 3;
            this.lblTel.Text = "Teléfono: ";
            this.lblTel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDom
            // 
            this.lblDom.Location = new System.Drawing.Point(18, 77);
            this.lblDom.Name = "lblDom";
            this.lblDom.Size = new System.Drawing.Size(125, 16);
            this.lblDom.TabIndex = 2;
            this.lblDom.Text = "Dirección: ";
            this.lblDom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(18, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre de Fantasía: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRS
            // 
            this.lblRS.Location = new System.Drawing.Point(18, 27);
            this.lblRS.Name = "lblRS";
            this.lblRS.Size = new System.Drawing.Size(125, 15);
            this.lblRS.TabIndex = 0;
            this.lblRS.Text = "Razón Social:";
            this.lblRS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(500, 294);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Facturación";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdoProd);
            this.groupBox3.Controls.Add(this.rdoTest);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(22, 74);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(442, 125);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " Modo Facturaciòn ";
            // 
            // rdoProd
            // 
            this.rdoProd.AutoSize = true;
            this.rdoProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoProd.Location = new System.Drawing.Point(219, 56);
            this.rdoProd.Name = "rdoProd";
            this.rdoProd.Size = new System.Drawing.Size(87, 19);
            this.rdoProd.TabIndex = 10;
            this.rdoProd.Text = "Producción";
            this.rdoProd.UseVisualStyleBackColor = true;
            // 
            // rdoTest
            // 
            this.rdoTest.AutoSize = true;
            this.rdoTest.Checked = true;
            this.rdoTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoTest.Location = new System.Drawing.Point(110, 56);
            this.rdoTest.Name = "rdoTest";
            this.rdoTest.Size = new System.Drawing.Size(65, 19);
            this.rdoTest.TabIndex = 9;
            this.rdoTest.TabStop = true;
            this.rdoTest.Text = "Testing";
            this.rdoTest.UseVisualStyleBackColor = true;
            this.rdoTest.CheckedChanged += new System.EventHandler(this.rdoTest_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblNota);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.chkControlUser);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(22, 340);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(442, 125);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Login ";
            this.groupBox1.Visible = false;
            // 
            // lblNota
            // 
            this.lblNota.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNota.Location = new System.Drawing.Point(18, 71);
            this.lblNota.Name = "lblNota";
            this.lblNota.Size = new System.Drawing.Size(411, 35);
            this.lblNota.TabIndex = 18;
            this.lblNota.Text = "Atención! : desactivar el control de login de usuarios eliminarà en forma automàt" +
                "ica el registro de login actual.";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkAutoLoad);
            this.groupBox2.Controls.Add(this.chkIconTaskBar);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(442, 125);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Comportamiento ";
            this.groupBox2.Visible = false;
            // 
            // chkAutoLoad
            // 
            this.chkAutoLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoLoad.Location = new System.Drawing.Point(19, 70);
            this.chkAutoLoad.Name = "chkAutoLoad";
            this.chkAutoLoad.Size = new System.Drawing.Size(410, 35);
            this.chkAutoLoad.TabIndex = 13;
            this.chkAutoLoad.Text = "Ejecutar la Aplicaciòn Automàticamente al Iniciar Windows.";
            this.chkAutoLoad.UseVisualStyleBackColor = true;
            // 
            // chkIconTaskBar
            // 
            this.chkIconTaskBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIconTaskBar.Location = new System.Drawing.Point(20, 29);
            this.chkIconTaskBar.Name = "chkIconTaskBar";
            this.chkIconTaskBar.Size = new System.Drawing.Size(410, 35);
            this.chkIconTaskBar.TabIndex = 12;
            this.chkIconTaskBar.Text = "Permencer en el Area de Notificaciòn al minimizar la Aplicaciòn (Barra de Tareas)" +
                "";
            this.chkIconTaskBar.UseVisualStyleBackColor = true;
            // 
            // chkControlUser
            // 
            this.chkControlUser.AutoSize = true;
            this.chkControlUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkControlUser.Location = new System.Drawing.Point(20, 29);
            this.chkControlUser.Name = "chkControlUser";
            this.chkControlUser.Size = new System.Drawing.Size(179, 17);
            this.chkControlUser.TabIndex = 11;
            this.chkControlUser.Text = "Controlar el login de los Usuarios";
            this.chkControlUser.UseVisualStyleBackColor = true;
            // 
            // frmAvanzadas
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(531, 411);
            this.Controls.Add(this.tabConfig);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAvanzadas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " - OPCIONES AVANZADAS";
            this.Load += new System.EventHandler(this.frmAvanzadas_Load);
            this.panel1.ResumeLayout(false);
            this.tabConfig.ResumeLayout(false);
            this.lblNF.ResumeLayout(false);
            this.lblNF.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabConfig;
        private System.Windows.Forms.TabPage lblNF;
        private System.Windows.Forms.TextBox txtCUIT;
        private System.Windows.Forms.TextBox txtWeb;
        private System.Windows.Forms.TextBox txtMail;
        private System.Windows.Forms.TextBox txtTel;
        private System.Windows.Forms.TextBox txtDom;
        private System.Windows.Forms.TextBox txtNF;
        private System.Windows.Forms.TextBox txtRS;
        private System.Windows.Forms.Label lblCondIVA;
        private System.Windows.Forms.Label lblLoc;
        private System.Windows.Forms.Label lblCUIT;
        private System.Windows.Forms.Label lblWeb;
        private System.Windows.Forms.Label lblMail;
        private System.Windows.Forms.Label lblTel;
        private System.Windows.Forms.Label lblDom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRS;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.TextBox txtCondicionIVA;
        private System.Windows.Forms.TextBox txtLocalidad;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdoProd;
        private System.Windows.Forms.RadioButton rdoTest;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblNota;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkAutoLoad;
        private System.Windows.Forms.CheckBox chkIconTaskBar;
        private System.Windows.Forms.CheckBox chkControlUser;

    }
}
namespace Prama
{
    partial class frmSplash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSplash));
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblInit = new System.Windows.Forms.Label();
            this.tmr = new System.Windows.Forms.Timer(this.components);
            this.lblPramaBlack = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCopyright
            // 
            this.lblCopyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopyright.ForeColor = System.Drawing.Color.Blue;
            this.lblCopyright.Location = new System.Drawing.Point(263, 145);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(177, 18);
            this.lblCopyright.TabIndex = 3;
            this.lblCopyright.Text = "(c) PRAMA S.A.S";
            this.lblCopyright.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblInit
            // 
            this.lblInit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInit.Location = new System.Drawing.Point(245, 63);
            this.lblInit.Name = "lblInit";
            this.lblInit.Size = new System.Drawing.Size(238, 60);
            this.lblInit.TabIndex = 2;
            this.lblInit.Text = "Conectando con la Base de Datos \r\ndel Sistema \r\n( puede demorar.... )";
            this.lblInit.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tmr
            // 
            this.tmr.Interval = 7000;
            this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
            // 
            // lblPramaBlack
            // 
            this.lblPramaBlack.Location = new System.Drawing.Point(446, 145);
            this.lblPramaBlack.Name = "lblPramaBlack";
            this.lblPramaBlack.Size = new System.Drawing.Size(10, 10);
            this.lblPramaBlack.TabIndex = 5;
            this.lblPramaBlack.Text = " ";
            this.lblPramaBlack.DoubleClick += new System.EventHandler(this.lblPramaBlack_DoubleClick);
            // 
            // picLogo
            // 
            this.picLogo.Image = ((System.Drawing.Image)(resources.GetObject("picLogo.Image")));
            this.picLogo.Location = new System.Drawing.Point(12, 12);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(227, 213);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 6;
            this.picLogo.TabStop = false;
            // 
            // frmSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(488, 237);
            this.ControlBox = false;
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.lblPramaBlack);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblInit);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmSplash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bienvenido a PRAMA S.A.S - v1.0 RELEASE 0";
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Label lblInit;
        private System.Windows.Forms.Timer tmr;
        private System.Windows.Forms.Label lblPramaBlack;
        private System.Windows.Forms.PictureBox picLogo;
    }
}
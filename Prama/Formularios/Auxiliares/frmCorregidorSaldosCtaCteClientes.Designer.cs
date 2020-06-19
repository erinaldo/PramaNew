namespace Prama.Formularios.Auxiliares
{
    partial class frmCorregidorSaldosCtaCteClientes
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
            this.btnRecibos = new System.Windows.Forms.Button();
            this.btnClientes = new System.Windows.Forms.Button();
            this.btnFacturas = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.proRegistros = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProcesados = new System.Windows.Forms.TextBox();
            this.txtCorrectos = new System.Windows.Forms.TextBox();
            this.txtCorregidos = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnRecibos
            // 
            this.btnRecibos.Location = new System.Drawing.Point(12, 112);
            this.btnRecibos.Name = "btnRecibos";
            this.btnRecibos.Size = new System.Drawing.Size(75, 23);
            this.btnRecibos.TabIndex = 0;
            this.btnRecibos.Text = "Recibos";
            this.btnRecibos.UseVisualStyleBackColor = true;
            this.btnRecibos.Click += new System.EventHandler(this.btnRecibos_Click);
            // 
            // btnClientes
            // 
            this.btnClientes.Location = new System.Drawing.Point(94, 112);
            this.btnClientes.Name = "btnClientes";
            this.btnClientes.Size = new System.Drawing.Size(75, 23);
            this.btnClientes.TabIndex = 1;
            this.btnClientes.Text = "Clientes";
            this.btnClientes.UseVisualStyleBackColor = true;
            this.btnClientes.Click += new System.EventHandler(this.btnClientes_Click);
            // 
            // btnFacturas
            // 
            this.btnFacturas.Location = new System.Drawing.Point(175, 112);
            this.btnFacturas.Name = "btnFacturas";
            this.btnFacturas.Size = new System.Drawing.Size(75, 23);
            this.btnFacturas.TabIndex = 2;
            this.btnFacturas.Text = "Facturas";
            this.btnFacturas.UseVisualStyleBackColor = true;
            this.btnFacturas.Click += new System.EventHandler(this.btnFacturas_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(256, 112);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 3;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // proRegistros
            // 
            this.proRegistros.Location = new System.Drawing.Point(13, 25);
            this.proRegistros.Name = "proRegistros";
            this.proRegistros.Size = new System.Drawing.Size(318, 23);
            this.proRegistros.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Procesados :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(268, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Corregidos :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(143, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Correctos :";
            // 
            // txtProcesados
            // 
            this.txtProcesados.Enabled = false;
            this.txtProcesados.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProcesados.Location = new System.Drawing.Point(12, 75);
            this.txtProcesados.Name = "txtProcesados";
            this.txtProcesados.Size = new System.Drawing.Size(70, 20);
            this.txtProcesados.TabIndex = 8;
            this.txtProcesados.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCorrectos
            // 
            this.txtCorrectos.Enabled = false;
            this.txtCorrectos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCorrectos.Location = new System.Drawing.Point(137, 75);
            this.txtCorrectos.Name = "txtCorrectos";
            this.txtCorrectos.Size = new System.Drawing.Size(70, 20);
            this.txtCorrectos.TabIndex = 9;
            this.txtCorrectos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCorregidos
            // 
            this.txtCorregidos.Enabled = false;
            this.txtCorregidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCorregidos.Location = new System.Drawing.Point(261, 75);
            this.txtCorregidos.Name = "txtCorregidos";
            this.txtCorregidos.Size = new System.Drawing.Size(70, 20);
            this.txtCorregidos.TabIndex = 10;
            this.txtCorregidos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(114, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Registros Procesados :";
            // 
            // frmCorregidorSaldosCtaCteClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 143);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCorregidos);
            this.Controls.Add(this.txtCorrectos);
            this.Controls.Add(this.txtProcesados);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.proRegistros);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnFacturas);
            this.Controls.Add(this.btnClientes);
            this.Controls.Add(this.btnRecibos);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCorregidorSaldosCtaCteClientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CONCILIAR SALDOS CLIENTES";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRecibos;
        private System.Windows.Forms.Button btnClientes;
        private System.Windows.Forms.Button btnFacturas;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.ProgressBar proRegistros;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProcesados;
        private System.Windows.Forms.TextBox txtCorrectos;
        private System.Windows.Forms.TextBox txtCorregidos;
        private System.Windows.Forms.Label label4;
    }
}
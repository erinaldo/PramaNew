namespace Prama
{
    partial class frmArticulosCoeficientes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmArticulosCoeficientes));
            this.btnPanel = new System.Windows.Forms.Panel();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnBorrar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.txtPublico = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCoeficiente = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvCoeficientes = new System.Windows.Forms.DataGridView();
            this.IdCoeficienteArticulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CoeficienteArticulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CoeficientePublico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CoeficienteDistribuidor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CoeficienteRevendedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtDistribuidor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRevendedor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip4 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip5 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip6 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip7 = new System.Windows.Forms.ToolTip(this.components);
            this.btnPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCoeficientes)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPanel
            // 
            this.btnPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPanel.Controls.Add(this.btnImprimir);
            this.btnPanel.Controls.Add(this.btnBorrar);
            this.btnPanel.Controls.Add(this.btnSalir);
            this.btnPanel.Controls.Add(this.btnCancelar);
            this.btnPanel.Controls.Add(this.btnAceptar);
            this.btnPanel.Controls.Add(this.btnModificar);
            this.btnPanel.Controls.Add(this.btnAgregar);
            this.btnPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPanel.Location = new System.Drawing.Point(0, 253);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(644, 58);
            this.btnPanel.TabIndex = 40;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = global::Prama.Recursos.printer;
            this.btnImprimir.Location = new System.Drawing.Point(114, 8);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(45, 40);
            this.btnImprimir.TabIndex = 4;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnBorrar
            // 
            this.btnBorrar.Image = global::Prama.Recursos.Borrar;
            this.btnBorrar.Location = new System.Drawing.Point(114, 8);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(45, 40);
            this.btnBorrar.TabIndex = 2;
            this.btnBorrar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnBorrar.UseVisualStyleBackColor = true;
            this.btnBorrar.Click += new System.EventHandler(this.btnBorrar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Image = global::Prama.Recursos.Salir;
            this.btnSalir.Location = new System.Drawing.Point(587, 8);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(45, 40);
            this.btnSalir.TabIndex = 7;
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = global::Prama.Recursos.cancel;
            this.btnCancelar.Location = new System.Drawing.Point(437, 8);
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
            this.btnAceptar.Location = new System.Drawing.Point(386, 8);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(45, 40);
            this.btnAceptar.TabIndex = 5;
            this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Image = global::Prama.Recursos.Editar;
            this.btnModificar.Location = new System.Drawing.Point(63, 8);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(45, 40);
            this.btnModificar.TabIndex = 1;
            this.btnModificar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Image = global::Prama.Recursos.Agregar;
            this.btnAgregar.Location = new System.Drawing.Point(12, 8);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(45, 40);
            this.btnAgregar.TabIndex = 0;
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // txtPublico
            // 
            this.txtPublico.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPublico.Location = new System.Drawing.Point(441, 83);
            this.txtPublico.Name = "txtPublico";
            this.txtPublico.Size = new System.Drawing.Size(62, 20);
            this.txtPublico.TabIndex = 54;
            this.txtPublico.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPublico.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPublico_KeyPress);
            this.txtPublico.Leave += new System.EventHandler(this.txtPublico_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(438, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 53;
            this.label3.Text = "Público :";
            // 
            // txtCoeficiente
            // 
            this.txtCoeficiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCoeficiente.Location = new System.Drawing.Point(441, 31);
            this.txtCoeficiente.Name = "txtCoeficiente";
            this.txtCoeficiente.Size = new System.Drawing.Size(195, 20);
            this.txtCoeficiente.TabIndex = 52;
            this.txtCoeficiente.Leave += new System.EventHandler(this.txtCoeficiente_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(438, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Coeficiente :";
            // 
            // dgvCoeficientes
            // 
            this.dgvCoeficientes.AllowUserToAddRows = false;
            this.dgvCoeficientes.AllowUserToDeleteRows = false;
            this.dgvCoeficientes.AllowUserToOrderColumns = true;
            this.dgvCoeficientes.AllowUserToResizeColumns = false;
            this.dgvCoeficientes.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvCoeficientes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCoeficientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCoeficientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCoeficientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdCoeficienteArticulo,
            this.CoeficienteArticulo,
            this.CoeficientePublico,
            this.CoeficienteDistribuidor,
            this.CoeficienteRevendedor});
            this.dgvCoeficientes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvCoeficientes.Location = new System.Drawing.Point(12, 12);
            this.dgvCoeficientes.MultiSelect = false;
            this.dgvCoeficientes.Name = "dgvCoeficientes";
            this.dgvCoeficientes.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCoeficientes.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvCoeficientes.RowHeadersVisible = false;
            this.dgvCoeficientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCoeficientes.Size = new System.Drawing.Size(420, 235);
            this.dgvCoeficientes.StandardTab = true;
            this.dgvCoeficientes.TabIndex = 48;
            this.dgvCoeficientes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCoeficientes_CellContentClick);
            this.dgvCoeficientes.SelectionChanged += new System.EventHandler(this.dgvCoeficientes_SelectionChanged);
            this.dgvCoeficientes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvCoeficientes_KeyDown);
            // 
            // IdCoeficienteArticulo
            // 
            this.IdCoeficienteArticulo.DataPropertyName = "IdCoeficienteArticulo";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.IdCoeficienteArticulo.DefaultCellStyle = dataGridViewCellStyle3;
            this.IdCoeficienteArticulo.HeaderText = "Código";
            this.IdCoeficienteArticulo.Name = "IdCoeficienteArticulo";
            this.IdCoeficienteArticulo.ReadOnly = true;
            this.IdCoeficienteArticulo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IdCoeficienteArticulo.Visible = false;
            this.IdCoeficienteArticulo.Width = 50;
            // 
            // CoeficienteArticulo
            // 
            this.CoeficienteArticulo.DataPropertyName = "CoeficienteArticulo";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.CoeficienteArticulo.DefaultCellStyle = dataGridViewCellStyle4;
            this.CoeficienteArticulo.HeaderText = "Coeficiente";
            this.CoeficienteArticulo.Name = "CoeficienteArticulo";
            this.CoeficienteArticulo.ReadOnly = true;
            this.CoeficienteArticulo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CoeficienteArticulo.Width = 205;
            // 
            // CoeficientePublico
            // 
            this.CoeficientePublico.DataPropertyName = "CoeficientePublico";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CoeficientePublico.DefaultCellStyle = dataGridViewCellStyle5;
            this.CoeficientePublico.HeaderText = "Público";
            this.CoeficientePublico.Name = "CoeficientePublico";
            this.CoeficientePublico.ReadOnly = true;
            this.CoeficientePublico.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CoeficientePublico.Width = 70;
            // 
            // CoeficienteDistribuidor
            // 
            this.CoeficienteDistribuidor.DataPropertyName = "CoeficienteDistribuidor";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CoeficienteDistribuidor.DefaultCellStyle = dataGridViewCellStyle6;
            this.CoeficienteDistribuidor.HeaderText = "Distribuidor";
            this.CoeficienteDistribuidor.Name = "CoeficienteDistribuidor";
            this.CoeficienteDistribuidor.ReadOnly = true;
            this.CoeficienteDistribuidor.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CoeficienteDistribuidor.Width = 70;
            // 
            // CoeficienteRevendedor
            // 
            this.CoeficienteRevendedor.DataPropertyName = "CoeficienteRevendedor";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CoeficienteRevendedor.DefaultCellStyle = dataGridViewCellStyle7;
            this.CoeficienteRevendedor.HeaderText = "Revendedor";
            this.CoeficienteRevendedor.Name = "CoeficienteRevendedor";
            this.CoeficienteRevendedor.ReadOnly = true;
            this.CoeficienteRevendedor.Width = 70;
            // 
            // txtDistribuidor
            // 
            this.txtDistribuidor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDistribuidor.Location = new System.Drawing.Point(441, 140);
            this.txtDistribuidor.Name = "txtDistribuidor";
            this.txtDistribuidor.Size = new System.Drawing.Size(62, 20);
            this.txtDistribuidor.TabIndex = 56;
            this.txtDistribuidor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDistribuidor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDistribuidor_KeyPress);
            this.txtDistribuidor.Leave += new System.EventHandler(this.txtDistribuidor_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(438, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 55;
            this.label1.Text = "Distribuidor :";
            // 
            // txtRevendedor
            // 
            this.txtRevendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRevendedor.Location = new System.Drawing.Point(441, 200);
            this.txtRevendedor.Name = "txtRevendedor";
            this.txtRevendedor.Size = new System.Drawing.Size(62, 20);
            this.txtRevendedor.TabIndex = 58;
            this.txtRevendedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRevendedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRevendedor_KeyPress);
            this.txtRevendedor.Leave += new System.EventHandler(this.txtRevendedor_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(438, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 57;
            this.label4.Text = "Revendedor :";
            // 
            // frmArticulosCoeficientes
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnSalir;
            this.ClientSize = new System.Drawing.Size(644, 311);
            this.Controls.Add(this.txtRevendedor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDistribuidor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPublico);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCoeficiente);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvCoeficientes);
            this.Controls.Add(this.btnPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmArticulosCoeficientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " - COEFICIENTES DE ARTICULOS";
            this.Load += new System.EventHandler(this.frmArticulosCoeficientes_Load);
            this.btnPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCoeficientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel btnPanel;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnBorrar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.TextBox txtPublico;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCoeficiente;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvCoeficientes;
        private System.Windows.Forms.TextBox txtDistribuidor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRevendedor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.ToolTip toolTip3;
        private System.Windows.Forms.ToolTip toolTip4;
        private System.Windows.Forms.ToolTip toolTip5;
        private System.Windows.Forms.ToolTip toolTip6;
        private System.Windows.Forms.ToolTip toolTip7;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdCoeficienteArticulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CoeficienteArticulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CoeficientePublico;
        private System.Windows.Forms.DataGridViewTextBoxColumn CoeficienteDistribuidor;
        private System.Windows.Forms.DataGridViewTextBoxColumn CoeficienteRevendedor;
    }
}
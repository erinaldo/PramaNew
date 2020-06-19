namespace Prama.Formularios.Articulos
{
    partial class frmArticulosRubros_SubRubros
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmArticulosRubros_SubRubros));
            this.btnPanel = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.gpbRubros = new System.Windows.Forms.GroupBox();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.dgvRubros = new System.Windows.Forms.DataGridView();
            this.IdRubroArticulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RubroArticulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Orden = new Prama.NumericGridColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUpSR = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnDownSR = new System.Windows.Forms.Button();
            this.dgvSubRubro = new System.Windows.Forms.DataGridView();
            this.IdSubrubroArticulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdRA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubrubroArticulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numericGridColumn2 = new Prama.NumericGridColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numericGridColumn1 = new Prama.NumericGridColumn();
            this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip4 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip5 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip6 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip7 = new System.Windows.Forms.ToolTip(this.components);
            this.btnPanel.SuspendLayout();
            this.gpbRubros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRubros)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubRubro)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPanel
            // 
            this.btnPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPanel.Controls.Add(this.btnSalir);
            this.btnPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPanel.Location = new System.Drawing.Point(0, 432);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(766, 58);
            this.btnPanel.TabIndex = 53;
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Image = global::Prama.Recursos.Salir;
            this.btnSalir.Location = new System.Drawing.Point(709, 6);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(45, 40);
            this.btnSalir.TabIndex = 8;
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalir.UseVisualStyleBackColor = true;
            // 
            // gpbRubros
            // 
            this.gpbRubros.Controls.Add(this.btnAccept);
            this.gpbRubros.Controls.Add(this.btnUp);
            this.gpbRubros.Controls.Add(this.btnDown);
            this.gpbRubros.Controls.Add(this.dgvRubros);
            this.gpbRubros.Location = new System.Drawing.Point(12, 21);
            this.gpbRubros.Name = "gpbRubros";
            this.gpbRubros.Size = new System.Drawing.Size(364, 392);
            this.gpbRubros.TabIndex = 54;
            this.gpbRubros.TabStop = false;
            this.gpbRubros.Text = " Rubros  ( Orden )";
            // 
            // btnAccept
            // 
            this.btnAccept.Image = global::Prama.Recursos.Aceptar;
            this.btnAccept.Location = new System.Drawing.Point(6, 332);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(45, 40);
            this.btnAccept.TabIndex = 1;
            this.btnAccept.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnUp
            // 
            this.btnUp.Image = global::Prama.Recursos.blue_at_up;
            this.btnUp.Location = new System.Drawing.Point(57, 332);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(45, 40);
            this.btnUp.TabIndex = 2;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Image = global::Prama.Recursos.blue_at_down;
            this.btnDown.Location = new System.Drawing.Point(108, 332);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(45, 40);
            this.btnDown.TabIndex = 3;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // dgvRubros
            // 
            this.dgvRubros.AllowUserToAddRows = false;
            this.dgvRubros.AllowUserToDeleteRows = false;
            this.dgvRubros.AllowUserToResizeColumns = false;
            this.dgvRubros.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvRubros.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRubros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRubros.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdRubroArticulo,
            this.RubroArticulo,
            this.Orden});
            this.dgvRubros.Location = new System.Drawing.Point(6, 29);
            this.dgvRubros.MultiSelect = false;
            this.dgvRubros.Name = "dgvRubros";
            this.dgvRubros.ReadOnly = true;
            this.dgvRubros.RowHeadersVisible = false;
            this.dgvRubros.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvRubros.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRubros.Size = new System.Drawing.Size(350, 297);
            this.dgvRubros.StandardTab = true;
            this.dgvRubros.TabIndex = 0;
            this.dgvRubros.SelectionChanged += new System.EventHandler(this.dgvRubros_SelectionChanged);
            // 
            // IdRubroArticulo
            // 
            this.IdRubroArticulo.DataPropertyName = "IdRubroArticulo";
            this.IdRubroArticulo.HeaderText = "IdRubroArticulo";
            this.IdRubroArticulo.Name = "IdRubroArticulo";
            this.IdRubroArticulo.ReadOnly = true;
            this.IdRubroArticulo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.IdRubroArticulo.Visible = false;
            // 
            // RubroArticulo
            // 
            this.RubroArticulo.DataPropertyName = "RubroArticulo";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.RubroArticulo.DefaultCellStyle = dataGridViewCellStyle2;
            this.RubroArticulo.HeaderText = "Rubro";
            this.RubroArticulo.Name = "RubroArticulo";
            this.RubroArticulo.ReadOnly = true;
            this.RubroArticulo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RubroArticulo.Width = 320;
            // 
            // Orden
            // 
            this.Orden.DecimalDigits = 0;
            this.Orden.DecimalSeparator = ".";
            this.Orden.HeaderText = "Orden";
            this.Orden.Name = "Orden";
            this.Orden.ReadOnly = true;
            this.Orden.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Orden.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnUpSR);
            this.groupBox1.Controls.Add(this.btnAceptar);
            this.groupBox1.Controls.Add(this.btnDownSR);
            this.groupBox1.Controls.Add(this.dgvSubRubro);
            this.groupBox1.Location = new System.Drawing.Point(390, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(364, 392);
            this.groupBox1.TabIndex = 55;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " SubRubros ( Orden )";
            // 
            // btnUpSR
            // 
            this.btnUpSR.Image = global::Prama.Recursos._81_at_blue_canvas_up;
            this.btnUpSR.Location = new System.Drawing.Point(202, 332);
            this.btnUpSR.Name = "btnUpSR";
            this.btnUpSR.Size = new System.Drawing.Size(45, 40);
            this.btnUpSR.TabIndex = 5;
            this.btnUpSR.UseVisualStyleBackColor = true;
            this.btnUpSR.Click += new System.EventHandler(this.btnUpSR_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Image = global::Prama.Recursos.Aceptar;
            this.btnAceptar.Location = new System.Drawing.Point(304, 332);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(45, 40);
            this.btnAceptar.TabIndex = 7;
            this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnDownSR
            // 
            this.btnDownSR.Image = global::Prama.Recursos._81_at_blue_canvas_down;
            this.btnDownSR.Location = new System.Drawing.Point(253, 332);
            this.btnDownSR.Name = "btnDownSR";
            this.btnDownSR.Size = new System.Drawing.Size(45, 40);
            this.btnDownSR.TabIndex = 6;
            this.btnDownSR.UseVisualStyleBackColor = true;
            this.btnDownSR.Click += new System.EventHandler(this.btnDownSR_Click);
            // 
            // dgvSubRubro
            // 
            this.dgvSubRubro.AllowUserToAddRows = false;
            this.dgvSubRubro.AllowUserToDeleteRows = false;
            this.dgvSubRubro.AllowUserToResizeColumns = false;
            this.dgvSubRubro.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvSubRubro.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSubRubro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSubRubro.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdSubrubroArticulo,
            this.IdRA,
            this.SubrubroArticulo,
            this.numericGridColumn2});
            this.dgvSubRubro.Location = new System.Drawing.Point(6, 29);
            this.dgvSubRubro.MultiSelect = false;
            this.dgvSubRubro.Name = "dgvSubRubro";
            this.dgvSubRubro.ReadOnly = true;
            this.dgvSubRubro.RowHeadersVisible = false;
            this.dgvSubRubro.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvSubRubro.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSubRubro.Size = new System.Drawing.Size(350, 297);
            this.dgvSubRubro.StandardTab = true;
            this.dgvSubRubro.TabIndex = 4;
            // 
            // IdSubrubroArticulo
            // 
            this.IdSubrubroArticulo.DataPropertyName = "IdSubrubroArticulo";
            this.IdSubrubroArticulo.HeaderText = "IdSubrubroArticulo";
            this.IdSubrubroArticulo.Name = "IdSubrubroArticulo";
            this.IdSubrubroArticulo.ReadOnly = true;
            this.IdSubrubroArticulo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IdSubrubroArticulo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.IdSubrubroArticulo.Visible = false;
            this.IdSubrubroArticulo.Width = 30;
            // 
            // IdRA
            // 
            this.IdRA.DataPropertyName = "IdRubroArticulo";
            this.IdRA.HeaderText = "IdRubroArticulo";
            this.IdRA.Name = "IdRA";
            this.IdRA.ReadOnly = true;
            this.IdRA.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IdRA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.IdRA.Visible = false;
            // 
            // SubrubroArticulo
            // 
            this.SubrubroArticulo.DataPropertyName = "SubrubroArticulo";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.SubrubroArticulo.DefaultCellStyle = dataGridViewCellStyle4;
            this.SubrubroArticulo.HeaderText = "SubRubro";
            this.SubrubroArticulo.Name = "SubrubroArticulo";
            this.SubrubroArticulo.ReadOnly = true;
            this.SubrubroArticulo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SubrubroArticulo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SubrubroArticulo.Width = 320;
            // 
            // numericGridColumn2
            // 
            this.numericGridColumn2.DecimalDigits = 0;
            this.numericGridColumn2.DecimalSeparator = ".";
            this.numericGridColumn2.HeaderText = "Orden";
            this.numericGridColumn2.Name = "numericGridColumn2";
            this.numericGridColumn2.ReadOnly = true;
            this.numericGridColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.numericGridColumn2.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "IdRubroArticulo";
            this.dataGridViewTextBoxColumn1.HeaderText = "IdRubroArticulo";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "RubroArticulo";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn2.HeaderText = "Rubro";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 250;
            // 
            // numericGridColumn1
            // 
            this.numericGridColumn1.DecimalDigits = 0;
            this.numericGridColumn1.DecimalSeparator = ".";
            this.numericGridColumn1.HeaderText = "Orden";
            this.numericGridColumn1.Name = "numericGridColumn1";
            this.numericGridColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.numericGridColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.numericGridColumn1.Visible = false;
            // 
            // frmArticulosRubros_SubRubros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnSalir;
            this.ClientSize = new System.Drawing.Size(766, 490);
            this.Controls.Add(this.gpbRubros);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmArticulosRubros_SubRubros";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " - RUBROS -> SUBRUBROS";
            this.Load += new System.EventHandler(this.frmArticulosRubros_SubRubros_Load);
            this.btnPanel.ResumeLayout(false);
            this.gpbRubros.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRubros)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubRubro)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel btnPanel;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.GroupBox gpbRubros;
        private System.Windows.Forms.DataGridView dgvRubros;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private NumericGridColumn numericGridColumn1;
        private System.Windows.Forms.Button btnUpSR;
        private System.Windows.Forms.Button btnDownSR;
        private System.Windows.Forms.DataGridView dgvSubRubro;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdRubroArticulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn RubroArticulo;
        private NumericGridColumn Orden;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdSubrubroArticulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdRA;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubrubroArticulo;
        private NumericGridColumn numericGridColumn2;
        private System.Windows.Forms.ToolTip toolTip3;
        private System.Windows.Forms.ToolTip toolTip4;
        private System.Windows.Forms.ToolTip toolTip5;
        private System.Windows.Forms.ToolTip toolTip6;
        private System.Windows.Forms.ToolTip toolTip7;
    }
}
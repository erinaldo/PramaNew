namespace Prama.Formularios.Stock
{
    partial class frmStockMovInternosSalidasABM
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStockMovInternosSalidasABM));
            this.btnPanel = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboAlmacen = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboPunto = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboMotivos = new System.Windows.Forms.ComboBox();
            this.lblCondCompra = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtObs = new System.Windows.Forms.TextBox();
            this.txtNeto = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.gpbArticulos = new System.Windows.Forms.GroupBox();
            this.btnAgregaArtExcl = new System.Windows.Forms.Button();
            this.btnQuitarArt = new System.Windows.Forms.Button();
            this.btnAgregarArt = new System.Windows.Forms.Button();
            this.dgvDetalleOrden = new System.Windows.Forms.DataGridView();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdArticulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Articulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new Prama.NumericGridColumn();
            this.Tabla = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioFinal = new Prama.NumericGridColumn();
            this.TotalArt = new Prama.NumericGridColumn();
            this.shapeContainer2 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip4 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip5 = new System.Windows.Forms.ToolTip(this.components);
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.ofdAbrirArchivo = new System.Windows.Forms.OpenFileDialog();
            this.FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.btnPanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gpbArticulos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleOrden)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPanel
            // 
            this.btnPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPanel.Controls.Add(this.btnCancelar);
            this.btnPanel.Controls.Add(this.btnAceptar);
            this.btnPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPanel.Location = new System.Drawing.Point(0, 513);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(894, 58);
            this.btnPanel.TabIndex = 45;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Image = global::Prama.Recursos.cancel;
            this.btnCancelar.Location = new System.Drawing.Point(831, 8);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(45, 40);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Image = global::Prama.Recursos.Aceptar;
            this.btnAceptar.Location = new System.Drawing.Point(780, 8);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(45, 40);
            this.btnAceptar.TabIndex = 0;
            this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // txtUsuario
            // 
            this.txtUsuario.Enabled = false;
            this.txtUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuario.Location = new System.Drawing.Point(660, 25);
            this.txtUsuario.MaxLength = 70;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(222, 20);
            this.txtUsuario.TabIndex = 50;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(660, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 13);
            this.label6.TabIndex = 53;
            this.label6.Text = "Usuario : ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboAlmacen
            // 
            this.cboAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAlmacen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAlmacen.FormattingEnabled = true;
            this.cboAlmacen.Location = new System.Drawing.Point(228, 25);
            this.cboAlmacen.Name = "cboAlmacen";
            this.cboAlmacen.Size = new System.Drawing.Size(210, 21);
            this.cboAlmacen.TabIndex = 47;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(225, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 52;
            this.label3.Text = "Almacén: ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboPunto
            // 
            this.cboPunto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPunto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPunto.FormattingEnabled = true;
            this.cboPunto.Location = new System.Drawing.Point(12, 25);
            this.cboPunto.Name = "cboPunto";
            this.cboPunto.Size = new System.Drawing.Size(210, 21);
            this.cboPunto.TabIndex = 46;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 51;
            this.label2.Text = "Punto Venta: ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboMotivos
            // 
            this.cboMotivos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMotivos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMotivos.FormattingEnabled = true;
            this.cboMotivos.Location = new System.Drawing.Point(444, 25);
            this.cboMotivos.Name = "cboMotivos";
            this.cboMotivos.Size = new System.Drawing.Size(210, 21);
            this.cboMotivos.TabIndex = 49;
            // 
            // lblCondCompra
            // 
            this.lblCondCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCondCompra.Location = new System.Drawing.Point(441, 9);
            this.lblCondCompra.Name = "lblCondCompra";
            this.lblCondCompra.Size = new System.Drawing.Size(100, 13);
            this.lblCondCompra.TabIndex = 48;
            this.lblCondCompra.Text = "Motivo : ";
            this.lblCondCompra.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtObs);
            this.groupBox2.Controls.Add(this.txtNeto);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 427);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(870, 80);
            this.groupBox2.TabIndex = 121;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pie del Formulario";
            // 
            // txtObs
            // 
            this.txtObs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtObs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObs.Location = new System.Drawing.Point(6, 19);
            this.txtObs.MaxLength = 500;
            this.txtObs.Multiline = true;
            this.txtObs.Name = "txtObs";
            this.txtObs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObs.Size = new System.Drawing.Size(765, 55);
            this.txtObs.TabIndex = 0;
            this.txtObs.Text = "Observaciones :";
            // 
            // txtNeto
            // 
            this.txtNeto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNeto.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtNeto.Location = new System.Drawing.Point(780, 36);
            this.txtNeto.Name = "txtNeto";
            this.txtNeto.Size = new System.Drawing.Size(80, 22);
            this.txtNeto.TabIndex = 1;
            this.txtNeto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label18.Location = new System.Drawing.Point(777, 16);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(37, 13);
            this.label18.TabIndex = 79;
            this.label18.Text = "Total :";
            // 
            // gpbArticulos
            // 
            this.gpbArticulos.Controls.Add(this.btnAgregaArtExcl);
            this.gpbArticulos.Controls.Add(this.btnQuitarArt);
            this.gpbArticulos.Controls.Add(this.btnAgregarArt);
            this.gpbArticulos.Controls.Add(this.dgvDetalleOrden);
            this.gpbArticulos.Controls.Add(this.shapeContainer2);
            this.gpbArticulos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbArticulos.Location = new System.Drawing.Point(12, 78);
            this.gpbArticulos.Name = "gpbArticulos";
            this.gpbArticulos.Size = new System.Drawing.Size(870, 343);
            this.gpbArticulos.TabIndex = 120;
            this.gpbArticulos.TabStop = false;
            this.gpbArticulos.Text = "Artículos a enviar";
            // 
            // btnAgregaArtExcl
            // 
            this.btnAgregaArtExcl.Image = global::Prama.Recursos.excel;
            this.btnAgregaArtExcl.Location = new System.Drawing.Point(819, 19);
            this.btnAgregaArtExcl.Name = "btnAgregaArtExcl";
            this.btnAgregaArtExcl.Size = new System.Drawing.Size(45, 40);
            this.btnAgregaArtExcl.TabIndex = 123;
            this.btnAgregaArtExcl.UseVisualStyleBackColor = true;
            this.btnAgregaArtExcl.Click += new System.EventHandler(this.btnAgregaArtExcl_Click);
            // 
            // btnQuitarArt
            // 
            this.btnQuitarArt.Image = global::Prama.Recursos.Borrar;
            this.btnQuitarArt.Location = new System.Drawing.Point(819, 121);
            this.btnQuitarArt.Name = "btnQuitarArt";
            this.btnQuitarArt.Size = new System.Drawing.Size(45, 40);
            this.btnQuitarArt.TabIndex = 166;
            this.btnQuitarArt.UseVisualStyleBackColor = true;
            this.btnQuitarArt.Click += new System.EventHandler(this.btnQuitarArt_Click);
            // 
            // btnAgregarArt
            // 
            this.btnAgregarArt.Image = global::Prama.Recursos.Agregar;
            this.btnAgregarArt.Location = new System.Drawing.Point(819, 75);
            this.btnAgregarArt.Name = "btnAgregarArt";
            this.btnAgregarArt.Size = new System.Drawing.Size(45, 40);
            this.btnAgregarArt.TabIndex = 165;
            this.btnAgregarArt.UseVisualStyleBackColor = true;
            this.btnAgregarArt.Click += new System.EventHandler(this.btnAgregarArt_Click);
            // 
            // dgvDetalleOrden
            // 
            this.dgvDetalleOrden.AllowUserToAddRows = false;
            this.dgvDetalleOrden.AllowUserToResizeColumns = false;
            this.dgvDetalleOrden.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvDetalleOrden.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetalleOrden.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDetalleOrden.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalleOrden.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Item,
            this.IdArticulo,
            this.Codigo,
            this.Articulo,
            this.Unidad,
            this.Cantidad,
            this.Tabla,
            this.PrecioFinal,
            this.TotalArt});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDetalleOrden.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvDetalleOrden.Location = new System.Drawing.Point(6, 19);
            this.dgvDetalleOrden.Name = "dgvDetalleOrden";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetalleOrden.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvDetalleOrden.RowHeadersVisible = false;
            this.dgvDetalleOrden.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvDetalleOrden.Size = new System.Drawing.Size(807, 318);
            this.dgvDetalleOrden.TabIndex = 8;
            this.dgvDetalleOrden.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalleOrden_CellClick);
            this.dgvDetalleOrden.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalleOrden_CellEndEdit);
            this.dgvDetalleOrden.Click += new System.EventHandler(this.dgvDetalleOrden_Click);
            this.dgvDetalleOrden.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvDetalleOrden_KeyUp);
            // 
            // Item
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Item.DefaultCellStyle = dataGridViewCellStyle3;
            this.Item.HeaderText = "Item";
            this.Item.Name = "Item";
            this.Item.ReadOnly = true;
            this.Item.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Item.Width = 40;
            // 
            // IdArticulo
            // 
            this.IdArticulo.DataPropertyName = "IdArticulo";
            this.IdArticulo.HeaderText = "IdArticulo";
            this.IdArticulo.Name = "IdArticulo";
            this.IdArticulo.ReadOnly = true;
            this.IdArticulo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IdArticulo.Visible = false;
            // 
            // Codigo
            // 
            this.Codigo.DataPropertyName = "CodigoArticulo";
            this.Codigo.HeaderText = "Cód. Art.";
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            this.Codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Codigo.Width = 80;
            // 
            // Articulo
            // 
            this.Articulo.DataPropertyName = "Articulo";
            this.Articulo.HeaderText = "Artículo";
            this.Articulo.Name = "Articulo";
            this.Articulo.ReadOnly = true;
            this.Articulo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Articulo.Width = 385;
            // 
            // Unidad
            // 
            this.Unidad.DataPropertyName = "AbreviaturaUnidad";
            this.Unidad.HeaderText = "Unidad";
            this.Unidad.Name = "Unidad";
            this.Unidad.ReadOnly = true;
            this.Unidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Unidad.Width = 70;
            // 
            // Cantidad
            // 
            this.Cantidad.DataPropertyName = "Cantidad";
            this.Cantidad.DecimalDigits = 2;
            this.Cantidad.DecimalSeparator = ".";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Cantidad.DefaultCellStyle = dataGridViewCellStyle4;
            this.Cantidad.HeaderText = "Cant.";
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Cantidad.Width = 70;
            // 
            // Tabla
            // 
            this.Tabla.DataPropertyName = "Tabla";
            this.Tabla.HeaderText = "Tabla";
            this.Tabla.Name = "Tabla";
            this.Tabla.Visible = false;
            // 
            // PrecioFinal
            // 
            this.PrecioFinal.DataPropertyName = "Precio";
            this.PrecioFinal.DecimalDigits = 2;
            this.PrecioFinal.DecimalSeparator = ".";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.PrecioFinal.DefaultCellStyle = dataGridViewCellStyle5;
            this.PrecioFinal.HeaderText = "P.Unit./Cos.";
            this.PrecioFinal.Name = "PrecioFinal";
            this.PrecioFinal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PrecioFinal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.PrecioFinal.Width = 70;
            // 
            // TotalArt
            // 
            this.TotalArt.DataPropertyName = "Total";
            this.TotalArt.DecimalDigits = 2;
            this.TotalArt.DecimalSeparator = ".";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TotalArt.DefaultCellStyle = dataGridViewCellStyle6;
            this.TotalArt.HeaderText = "Sub Total";
            this.TotalArt.Name = "TotalArt";
            this.TotalArt.ReadOnly = true;
            this.TotalArt.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TotalArt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.TotalArt.Width = 80;
            // 
            // shapeContainer2
            // 
            this.shapeContainer2.Location = new System.Drawing.Point(3, 16);
            this.shapeContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer2.Name = "shapeContainer2";
            this.shapeContainer2.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer2.Size = new System.Drawing.Size(864, 324);
            this.shapeContainer2.TabIndex = 167;
            this.shapeContainer2.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 816;
            this.lineShape1.X2 = 859;
            this.lineShape1.Y1 = 51;
            this.lineShape1.Y2 = 51;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.Location = new System.Drawing.Point(12, 52);
            this.txtDescripcion.MaxLength = 500;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescripcion.Size = new System.Drawing.Size(870, 20);
            this.txtDescripcion.TabIndex = 122;
            this.txtDescripcion.Text = "Descripción :";
            // 
            // frmStockMovInternosSalidasABM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 571);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gpbArticulos);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboAlmacen);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboPunto);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboMotivos);
            this.Controls.Add(this.lblCondCompra);
            this.Controls.Add(this.btnPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStockMovInternosSalidasABM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "- MOVIMIENTOS INTERNOS DE STOCK - SALIDAS - ABM";
            this.Load += new System.EventHandler(this.frmStockMovInternosSalidasABM_Load);
            this.btnPanel.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gpbArticulos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleOrden)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel btnPanel;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboAlmacen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboPunto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboMotivos;
        private System.Windows.Forms.Label lblCondCompra;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtObs;
        private System.Windows.Forms.TextBox txtNeto;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox gpbArticulos;
        private System.Windows.Forms.Button btnQuitarArt;
        private System.Windows.Forms.Button btnAgregarArt;
        private System.Windows.Forms.DataGridView dgvDetalleOrden;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.ToolTip toolTip3;
        private System.Windows.Forms.ToolTip toolTip4;
        private System.Windows.Forms.ToolTip toolTip5;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdArticulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Articulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unidad;
        private NumericGridColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tabla;
        private NumericGridColumn PrecioFinal;
        private NumericGridColumn TotalArt;
        private System.Windows.Forms.Button btnAgregaArtExcl;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer2;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.OpenFileDialog ofdAbrirArchivo;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowser;
    }
}
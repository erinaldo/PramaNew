namespace Prama
{
    partial class frmConfigurar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfigurar));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabComp = new System.Windows.Forms.TabPage();
            this.grpCantImp = new System.Windows.Forms.GroupBox();
            this.tabNum = new System.Windows.Forms.NumericUpDown();
            this.lblPedidos = new System.Windows.Forms.Label();
            this.numRecibos = new System.Windows.Forms.NumericUpDown();
            this.lblRecibo = new System.Windows.Forms.Label();
            this.numRemi = new System.Windows.Forms.NumericUpDown();
            this.numFactu = new System.Windows.Forms.NumericUpDown();
            this.numPresu = new System.Windows.Forms.NumericUpDown();
            this.lblRemitos = new System.Windows.Forms.Label();
            this.lblFacturas = new System.Windows.Forms.Label();
            this.lblPresupuestos = new System.Windows.Forms.Label();
            this.txtNivelFact = new System.Windows.Forms.NumericUpDown();
            this.lblFacturacion = new System.Windows.Forms.Label();
            this.txtNivelStock = new System.Windows.Forms.NumericUpDown();
            this.lblNivelStock = new System.Windows.Forms.Label();
            this.txtNivel = new System.Windows.Forms.NumericUpDown();
            this.lblNivelBajas = new System.Windows.Forms.Label();
            this.chkImpresion = new System.Windows.Forms.CheckBox();
            this.lblPorcIVA = new System.Windows.Forms.Label();
            this.txtPorcIVA = new System.Windows.Forms.TextBox();
            this.txtMesPresu = new System.Windows.Forms.TextBox();
            this.txtMesPed = new System.Windows.Forms.TextBox();
            this.lblMesPresu = new System.Windows.Forms.Label();
            this.lblMesPed = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape5 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape4 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.tabOtros = new System.Windows.Forms.TabPage();
            this.grbCodificacion = new System.Windows.Forms.GroupBox();
            this.nroProd = new System.Windows.Forms.NumericUpDown();
            this.lblUltProducto = new System.Windows.Forms.Label();
            this.nroIns = new System.Windows.Forms.NumericUpDown();
            this.lblUltInsumo = new System.Windows.Forms.Label();
            this.cboPvAFIP = new System.Windows.Forms.ComboBox();
            this.grpListaPedido = new System.Windows.Forms.GroupBox();
            this.txtLimitCdbaLimit = new System.Windows.Forms.TextBox();
            this.txtPorcLimitCba = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblLimitCdba = new System.Windows.Forms.Label();
            this.nCantMin = new System.Windows.Forms.NumericUpDown();
            this.lblCantMin = new System.Windows.Forms.Label();
            this.cboAlmacen = new System.Windows.Forms.ComboBox();
            this.lblAlmacen = new System.Windows.Forms.Label();
            this.lblPtoVta = new System.Windows.Forms.Label();
            this.shapeContainer2 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape3 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabComp.SuspendLayout();
            this.grpCantImp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRecibos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRemi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFactu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPresu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNivelFact)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNivelStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNivel)).BeginInit();
            this.tabOtros.SuspendLayout();
            this.grbCodificacion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nroProd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nroIns)).BeginInit();
            this.grpListaPedido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nCantMin)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabComp);
            this.tabControl1.Controls.Add(this.tabOtros);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(430, 439);
            this.tabControl1.TabIndex = 0;
            // 
            // tabComp
            // 
            this.tabComp.Controls.Add(this.grpCantImp);
            this.tabComp.Controls.Add(this.txtNivelFact);
            this.tabComp.Controls.Add(this.lblFacturacion);
            this.tabComp.Controls.Add(this.txtNivelStock);
            this.tabComp.Controls.Add(this.lblNivelStock);
            this.tabComp.Controls.Add(this.txtNivel);
            this.tabComp.Controls.Add(this.lblNivelBajas);
            this.tabComp.Controls.Add(this.chkImpresion);
            this.tabComp.Controls.Add(this.lblPorcIVA);
            this.tabComp.Controls.Add(this.txtPorcIVA);
            this.tabComp.Controls.Add(this.txtMesPresu);
            this.tabComp.Controls.Add(this.txtMesPed);
            this.tabComp.Controls.Add(this.lblMesPresu);
            this.tabComp.Controls.Add(this.lblMesPed);
            this.tabComp.Controls.Add(this.shapeContainer1);
            this.tabComp.Location = new System.Drawing.Point(4, 22);
            this.tabComp.Name = "tabComp";
            this.tabComp.Padding = new System.Windows.Forms.Padding(3);
            this.tabComp.Size = new System.Drawing.Size(422, 413);
            this.tabComp.TabIndex = 0;
            this.tabComp.Text = "Inicial";
            this.tabComp.UseVisualStyleBackColor = true;
            // 
            // grpCantImp
            // 
            this.grpCantImp.Controls.Add(this.tabNum);
            this.grpCantImp.Controls.Add(this.lblPedidos);
            this.grpCantImp.Controls.Add(this.numRecibos);
            this.grpCantImp.Controls.Add(this.lblRecibo);
            this.grpCantImp.Controls.Add(this.numRemi);
            this.grpCantImp.Controls.Add(this.numFactu);
            this.grpCantImp.Controls.Add(this.numPresu);
            this.grpCantImp.Controls.Add(this.lblRemitos);
            this.grpCantImp.Controls.Add(this.lblFacturas);
            this.grpCantImp.Controls.Add(this.lblPresupuestos);
            this.grpCantImp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCantImp.Location = new System.Drawing.Point(22, 197);
            this.grpCantImp.Name = "grpCantImp";
            this.grpCantImp.Size = new System.Drawing.Size(355, 124);
            this.grpCantImp.TabIndex = 9;
            this.grpCantImp.TabStop = false;
            this.grpCantImp.Text = " Cantidad de Impresiones ";
            // 
            // tabNum
            // 
            this.tabNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabNum.Location = new System.Drawing.Point(285, 65);
            this.tabNum.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.tabNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tabNum.Name = "tabNum";
            this.tabNum.Size = new System.Drawing.Size(38, 20);
            this.tabNum.TabIndex = 4;
            this.tabNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblPedidos
            // 
            this.lblPedidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPedidos.Location = new System.Drawing.Point(204, 67);
            this.lblPedidos.Name = "lblPedidos";
            this.lblPedidos.Size = new System.Drawing.Size(49, 18);
            this.lblPedidos.TabIndex = 8;
            this.lblPedidos.Text = "Pedidos: ";
            // 
            // numRecibos
            // 
            this.numRecibos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRecibos.Location = new System.Drawing.Point(285, 39);
            this.numRecibos.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numRecibos.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRecibos.Name = "numRecibos";
            this.numRecibos.Size = new System.Drawing.Size(38, 20);
            this.numRecibos.TabIndex = 3;
            this.numRecibos.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblRecibo
            // 
            this.lblRecibo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecibo.Location = new System.Drawing.Point(202, 41);
            this.lblRecibo.Name = "lblRecibo";
            this.lblRecibo.Size = new System.Drawing.Size(77, 18);
            this.lblRecibo.TabIndex = 6;
            this.lblRecibo.Text = "Recibo: ";
            // 
            // numRemi
            // 
            this.numRemi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRemi.Location = new System.Drawing.Point(109, 91);
            this.numRemi.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numRemi.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRemi.Name = "numRemi";
            this.numRemi.Size = new System.Drawing.Size(38, 20);
            this.numRemi.TabIndex = 2;
            this.numRemi.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // numFactu
            // 
            this.numFactu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numFactu.Location = new System.Drawing.Point(109, 65);
            this.numFactu.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numFactu.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFactu.Name = "numFactu";
            this.numFactu.Size = new System.Drawing.Size(38, 20);
            this.numFactu.TabIndex = 1;
            this.numFactu.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // numPresu
            // 
            this.numPresu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numPresu.Location = new System.Drawing.Point(109, 39);
            this.numPresu.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numPresu.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPresu.Name = "numPresu";
            this.numPresu.Size = new System.Drawing.Size(38, 20);
            this.numPresu.TabIndex = 0;
            this.numPresu.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblRemitos
            // 
            this.lblRemitos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemitos.Location = new System.Drawing.Point(25, 93);
            this.lblRemitos.Name = "lblRemitos";
            this.lblRemitos.Size = new System.Drawing.Size(77, 13);
            this.lblRemitos.TabIndex = 2;
            this.lblRemitos.Text = "Remitos: ";
            // 
            // lblFacturas
            // 
            this.lblFacturas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFacturas.Location = new System.Drawing.Point(25, 67);
            this.lblFacturas.Name = "lblFacturas";
            this.lblFacturas.Size = new System.Drawing.Size(77, 13);
            this.lblFacturas.TabIndex = 1;
            this.lblFacturas.Text = "Facturas: ";
            // 
            // lblPresupuestos
            // 
            this.lblPresupuestos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPresupuestos.Location = new System.Drawing.Point(26, 41);
            this.lblPresupuestos.Name = "lblPresupuestos";
            this.lblPresupuestos.Size = new System.Drawing.Size(77, 21);
            this.lblPresupuestos.TabIndex = 0;
            this.lblPresupuestos.Text = "Presupuestos: ";
            // 
            // txtNivelFact
            // 
            this.txtNivelFact.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNivelFact.Location = new System.Drawing.Point(323, 151);
            this.txtNivelFact.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.txtNivelFact.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtNivelFact.Name = "txtNivelFact";
            this.txtNivelFact.Size = new System.Drawing.Size(54, 20);
            this.txtNivelFact.TabIndex = 8;
            this.txtNivelFact.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNivelFact.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lblFacturacion
            // 
            this.lblFacturacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFacturacion.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblFacturacion.Location = new System.Drawing.Point(38, 153);
            this.lblFacturacion.Name = "lblFacturacion";
            this.lblFacturacion.Size = new System.Drawing.Size(279, 20);
            this.lblFacturacion.TabIndex = 7;
            this.lblFacturacion.Text = "Nivel mín. para emitir comprobante factura";
            // 
            // txtNivelStock
            // 
            this.txtNivelStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNivelStock.Location = new System.Drawing.Point(323, 125);
            this.txtNivelStock.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.txtNivelStock.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtNivelStock.Name = "txtNivelStock";
            this.txtNivelStock.Size = new System.Drawing.Size(54, 20);
            this.txtNivelStock.TabIndex = 6;
            this.txtNivelStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNivelStock.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lblNivelStock
            // 
            this.lblNivelStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNivelStock.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblNivelStock.Location = new System.Drawing.Point(38, 127);
            this.lblNivelStock.Name = "lblNivelStock";
            this.lblNivelStock.Size = new System.Drawing.Size(279, 20);
            this.lblNivelStock.TabIndex = 5;
            this.lblNivelStock.Text = "Nivel mín. para manejo Stock  (Fab, Mov, etc.)";
            // 
            // txtNivel
            // 
            this.txtNivel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNivel.Location = new System.Drawing.Point(323, 100);
            this.txtNivel.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.txtNivel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtNivel.Name = "txtNivel";
            this.txtNivel.Size = new System.Drawing.Size(54, 20);
            this.txtNivel.TabIndex = 4;
            this.txtNivel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNivel.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lblNivelBajas
            // 
            this.lblNivelBajas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNivelBajas.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblNivelBajas.Location = new System.Drawing.Point(35, 102);
            this.lblNivelBajas.Name = "lblNivelBajas";
            this.lblNivelBajas.Size = new System.Drawing.Size(282, 20);
            this.lblNivelBajas.TabIndex = 3;
            this.lblNivelBajas.Text = " Nivel mín. de Usuario requerido para \'Bajas\'";
            // 
            // chkImpresion
            // 
            this.chkImpresion.Checked = true;
            this.chkImpresion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkImpresion.Location = new System.Drawing.Point(39, 64);
            this.chkImpresion.Name = "chkImpresion";
            this.chkImpresion.Size = new System.Drawing.Size(348, 18);
            this.chkImpresion.TabIndex = 2;
            this.chkImpresion.Text = "Preguntar si quiero imprimir cualquier comprobante antes de guardar";
            this.chkImpresion.UseVisualStyleBackColor = true;
            // 
            // lblPorcIVA
            // 
            this.lblPorcIVA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPorcIVA.Location = new System.Drawing.Point(38, 29);
            this.lblPorcIVA.Name = "lblPorcIVA";
            this.lblPorcIVA.Size = new System.Drawing.Size(163, 18);
            this.lblPorcIVA.TabIndex = 0;
            this.lblPorcIVA.Text = "Porcentaje por defecto de I.V.A: ";
            // 
            // txtPorcIVA
            // 
            this.txtPorcIVA.Location = new System.Drawing.Point(216, 27);
            this.txtPorcIVA.MaxLength = 6;
            this.txtPorcIVA.Name = "txtPorcIVA";
            this.txtPorcIVA.Size = new System.Drawing.Size(51, 20);
            this.txtPorcIVA.TabIndex = 1;
            this.txtPorcIVA.Text = "21.00";
            this.txtPorcIVA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPorcIVA.Enter += new System.EventHandler(this.txtPorcIVA_Enter);
            this.txtPorcIVA.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPorcIVA_KeyPress);
            // 
            // txtMesPresu
            // 
            this.txtMesPresu.Location = new System.Drawing.Point(326, 364);
            this.txtMesPresu.MaxLength = 2;
            this.txtMesPresu.Name = "txtMesPresu";
            this.txtMesPresu.Size = new System.Drawing.Size(51, 20);
            this.txtMesPresu.TabIndex = 6;
            this.txtMesPresu.Text = "1";
            this.txtMesPresu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMesPresu.Enter += new System.EventHandler(this.txtMesPresu_Enter);
            this.txtMesPresu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMesPresu_KeyPress);
            // 
            // txtMesPed
            // 
            this.txtMesPed.Location = new System.Drawing.Point(326, 338);
            this.txtMesPed.MaxLength = 2;
            this.txtMesPed.Name = "txtMesPed";
            this.txtMesPed.Size = new System.Drawing.Size(51, 20);
            this.txtMesPed.TabIndex = 5;
            this.txtMesPed.Text = "3";
            this.txtMesPed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMesPed.Enter += new System.EventHandler(this.txtMesPed_Enter);
            this.txtMesPed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMesPed_KeyPress);
            // 
            // lblMesPresu
            // 
            this.lblMesPresu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMesPresu.Location = new System.Drawing.Point(18, 364);
            this.lblMesPresu.Name = "lblMesPresu";
            this.lblMesPresu.Size = new System.Drawing.Size(310, 18);
            this.lblMesPresu.TabIndex = 2;
            this.lblMesPresu.Text = "Meses que se tendran en cuenta los presupuestos pendientes: ";
            // 
            // lblMesPed
            // 
            this.lblMesPed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMesPed.Location = new System.Drawing.Point(47, 341);
            this.lblMesPed.Name = "lblMesPed";
            this.lblMesPed.Size = new System.Drawing.Size(281, 18);
            this.lblMesPed.TabIndex = 1;
            this.lblMesPed.Text = "Meses que se tendran en cuenta los pedidos pendientes: ";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(3, 3);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape5,
            this.lineShape4,
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(416, 407);
            this.shapeContainer1.TabIndex = 8;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape5
            // 
            this.lineShape5.BorderColor = System.Drawing.Color.Silver;
            this.lineShape5.Name = "lineShape5";
            this.lineShape5.X1 = 18;
            this.lineShape5.X2 = 373;
            this.lineShape5.Y1 = 182;
            this.lineShape5.Y2 = 184;
            // 
            // lineShape4
            // 
            this.lineShape4.BorderColor = System.Drawing.Color.Silver;
            this.lineShape4.Name = "lineShape4";
            this.lineShape4.X1 = 18;
            this.lineShape4.X2 = 376;
            this.lineShape4.Y1 = 85;
            this.lineShape4.Y2 = 85;
            // 
            // lineShape1
            // 
            this.lineShape1.BorderColor = System.Drawing.Color.Silver;
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 18;
            this.lineShape1.X2 = 376;
            this.lineShape1.Y1 = 54;
            this.lineShape1.Y2 = 54;
            // 
            // tabOtros
            // 
            this.tabOtros.Controls.Add(this.grbCodificacion);
            this.tabOtros.Controls.Add(this.cboPvAFIP);
            this.tabOtros.Controls.Add(this.grpListaPedido);
            this.tabOtros.Controls.Add(this.cboAlmacen);
            this.tabOtros.Controls.Add(this.lblAlmacen);
            this.tabOtros.Controls.Add(this.lblPtoVta);
            this.tabOtros.Controls.Add(this.shapeContainer2);
            this.tabOtros.Location = new System.Drawing.Point(4, 22);
            this.tabOtros.Name = "tabOtros";
            this.tabOtros.Padding = new System.Windows.Forms.Padding(3);
            this.tabOtros.Size = new System.Drawing.Size(422, 413);
            this.tabOtros.TabIndex = 1;
            this.tabOtros.Text = "Otros Datos";
            this.tabOtros.UseVisualStyleBackColor = true;
            // 
            // grbCodificacion
            // 
            this.grbCodificacion.Controls.Add(this.nroProd);
            this.grbCodificacion.Controls.Add(this.lblUltProducto);
            this.grbCodificacion.Controls.Add(this.nroIns);
            this.grbCodificacion.Controls.Add(this.lblUltInsumo);
            this.grbCodificacion.ForeColor = System.Drawing.Color.Blue;
            this.grbCodificacion.Location = new System.Drawing.Point(15, 96);
            this.grbCodificacion.Name = "grbCodificacion";
            this.grbCodificacion.Size = new System.Drawing.Size(383, 119);
            this.grbCodificacion.TabIndex = 20;
            this.grbCodificacion.TabStop = false;
            this.grbCodificacion.Text = " Codificación Automática ( Productos )";
            // 
            // nroProd
            // 
            this.nroProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nroProd.Location = new System.Drawing.Point(322, 34);
            this.nroProd.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nroProd.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nroProd.Name = "nroProd";
            this.nroProd.Size = new System.Drawing.Size(55, 20);
            this.nroProd.TabIndex = 24;
            this.nroProd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nroProd.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblUltProducto
            // 
            this.lblUltProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUltProducto.ForeColor = System.Drawing.Color.Black;
            this.lblUltProducto.Location = new System.Drawing.Point(8, 36);
            this.lblUltProducto.Name = "lblUltProducto";
            this.lblUltProducto.Size = new System.Drawing.Size(310, 14);
            this.lblUltProducto.TabIndex = 23;
            this.lblUltProducto.Text = "Ultimo N° de Producto para asignación automática del Sistema:";
            // 
            // nroIns
            // 
            this.nroIns.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nroIns.Location = new System.Drawing.Point(320, 80);
            this.nroIns.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nroIns.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nroIns.Name = "nroIns";
            this.nroIns.Size = new System.Drawing.Size(55, 20);
            this.nroIns.TabIndex = 22;
            this.nroIns.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nroIns.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nroIns.Visible = false;
            // 
            // lblUltInsumo
            // 
            this.lblUltInsumo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUltInsumo.ForeColor = System.Drawing.Color.Black;
            this.lblUltInsumo.Location = new System.Drawing.Point(6, 82);
            this.lblUltInsumo.Name = "lblUltInsumo";
            this.lblUltInsumo.Size = new System.Drawing.Size(310, 14);
            this.lblUltInsumo.TabIndex = 21;
            this.lblUltInsumo.Text = "Ultimo N° de Producto para asignación automática del Sistema:";
            // 
            // cboPvAFIP
            // 
            this.cboPvAFIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPvAFIP.FormattingEnabled = true;
            this.cboPvAFIP.Location = new System.Drawing.Point(169, 12);
            this.cboPvAFIP.Name = "cboPvAFIP";
            this.cboPvAFIP.Size = new System.Drawing.Size(43, 21);
            this.cboPvAFIP.TabIndex = 10;
            // 
            // grpListaPedido
            // 
            this.grpListaPedido.Controls.Add(this.txtLimitCdbaLimit);
            this.grpListaPedido.Controls.Add(this.txtPorcLimitCba);
            this.grpListaPedido.Controls.Add(this.label2);
            this.grpListaPedido.Controls.Add(this.lblLimitCdba);
            this.grpListaPedido.Controls.Add(this.nCantMin);
            this.grpListaPedido.Controls.Add(this.lblCantMin);
            this.grpListaPedido.ForeColor = System.Drawing.Color.Blue;
            this.grpListaPedido.Location = new System.Drawing.Point(17, 237);
            this.grpListaPedido.Name = "grpListaPedido";
            this.grpListaPedido.Size = new System.Drawing.Size(383, 155);
            this.grpListaPedido.TabIndex = 13;
            this.grpListaPedido.TabStop = false;
            this.grpListaPedido.Text = "  Precios ";
            // 
            // txtLimitCdbaLimit
            // 
            this.txtLimitCdbaLimit.Location = new System.Drawing.Point(318, 111);
            this.txtLimitCdbaLimit.MaxLength = 5;
            this.txtLimitCdbaLimit.Name = "txtLimitCdbaLimit";
            this.txtLimitCdbaLimit.Size = new System.Drawing.Size(56, 20);
            this.txtLimitCdbaLimit.TabIndex = 19;
            this.txtLimitCdbaLimit.Text = "20.00";
            this.txtLimitCdbaLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLimitCdbaLimit.Click += new System.EventHandler(this.txtLimitCdbaLimit_Click);
            this.txtLimitCdbaLimit.Enter += new System.EventHandler(this.txtLimitCdbaLimit_Enter);
            // 
            // txtPorcLimitCba
            // 
            this.txtPorcLimitCba.Location = new System.Drawing.Point(320, 63);
            this.txtPorcLimitCba.MaxLength = 5;
            this.txtPorcLimitCba.Name = "txtPorcLimitCba";
            this.txtPorcLimitCba.Size = new System.Drawing.Size(56, 20);
            this.txtPorcLimitCba.TabIndex = 17;
            this.txtPorcLimitCba.Text = "10.00";
            this.txtPorcLimitCba.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPorcLimitCba.Click += new System.EventHandler(this.txtPorcLimitCba_Click);
            this.txtPorcLimitCba.Enter += new System.EventHandler(this.txtPorcLimitCba_Enter);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(6, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(311, 18);
            this.label2.TabIndex = 18;
            this.label2.Text = "Porcentaje a aplicar en la Lista de Precios Limitrofes (Limit.)";
            // 
            // lblLimitCdba
            // 
            this.lblLimitCdba.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLimitCdba.ForeColor = System.Drawing.Color.Black;
            this.lblLimitCdba.Location = new System.Drawing.Point(6, 66);
            this.lblLimitCdba.Name = "lblLimitCdba";
            this.lblLimitCdba.Size = new System.Drawing.Size(311, 18);
            this.lblLimitCdba.TabIndex = 16;
            this.lblLimitCdba.Text = "Porcentaje a aplicar en la Lista de Precios Limítrofes (Cdba)";
            // 
            // nCantMin
            // 
            this.nCantMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nCantMin.Location = new System.Drawing.Point(320, 23);
            this.nCantMin.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nCantMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nCantMin.Name = "nCantMin";
            this.nCantMin.Size = new System.Drawing.Size(57, 20);
            this.nCantMin.TabIndex = 15;
            this.nCantMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nCantMin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblCantMin
            // 
            this.lblCantMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantMin.ForeColor = System.Drawing.Color.Black;
            this.lblCantMin.Location = new System.Drawing.Point(6, 25);
            this.lblCantMin.Name = "lblCantMin";
            this.lblCantMin.Size = new System.Drawing.Size(311, 18);
            this.lblCantMin.TabIndex = 14;
            this.lblCantMin.Text = "Cantidad mínima para el cálculo del precio para Revendedores ";
            // 
            // cboAlmacen
            // 
            this.cboAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAlmacen.Enabled = false;
            this.cboAlmacen.FormattingEnabled = true;
            this.cboAlmacen.Location = new System.Drawing.Point(169, 46);
            this.cboAlmacen.Name = "cboAlmacen";
            this.cboAlmacen.Size = new System.Drawing.Size(229, 21);
            this.cboAlmacen.TabIndex = 12;
            // 
            // lblAlmacen
            // 
            this.lblAlmacen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlmacen.Location = new System.Drawing.Point(52, 46);
            this.lblAlmacen.Name = "lblAlmacen";
            this.lblAlmacen.Size = new System.Drawing.Size(113, 11);
            this.lblAlmacen.TabIndex = 11;
            this.lblAlmacen.Text = " Almacen por defecto: ";
            // 
            // lblPtoVta
            // 
            this.lblPtoVta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPtoVta.Location = new System.Drawing.Point(24, 15);
            this.lblPtoVta.Name = "lblPtoVta";
            this.lblPtoVta.Size = new System.Drawing.Size(141, 18);
            this.lblPtoVta.TabIndex = 9;
            this.lblPtoVta.Text = "Punto de Venta por defecto:";
            // 
            // shapeContainer2
            // 
            this.shapeContainer2.Location = new System.Drawing.Point(3, 3);
            this.shapeContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer2.Name = "shapeContainer2";
            this.shapeContainer2.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape3,
            this.lineShape2});
            this.shapeContainer2.Size = new System.Drawing.Size(416, 407);
            this.shapeContainer2.TabIndex = 12;
            this.shapeContainer2.TabStop = false;
            // 
            // lineShape3
            // 
            this.lineShape3.BorderColor = System.Drawing.Color.Silver;
            this.lineShape3.Name = "lineShape3";
            this.lineShape3.Visible = false;
            this.lineShape3.X1 = 12;
            this.lineShape3.X2 = 393;
            this.lineShape3.Y1 = 174;
            this.lineShape3.Y2 = 173;
            // 
            // lineShape2
            // 
            this.lineShape2.BorderColor = System.Drawing.Color.Silver;
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.X1 = 11;
            this.lineShape2.X2 = 392;
            this.lineShape2.Y1 = 77;
            this.lineShape2.Y2 = 76;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.btnCancelar);
            this.panel1.Controls.Add(this.btnAceptar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 461);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(456, 62);
            this.panel1.TabIndex = 25;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Image = global::Prama.Recursos.cancel;
            this.btnCancelar.Location = new System.Drawing.Point(377, 10);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(45, 40);
            this.btnCancelar.TabIndex = 27;
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Image = global::Prama.Recursos.Aceptar;
            this.btnAceptar.Location = new System.Drawing.Point(326, 10);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(45, 40);
            this.btnAceptar.TabIndex = 26;
            this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // frmConfigurar
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(456, 523);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfigurar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " - CONFIGURACION";
            this.Load += new System.EventHandler(this.frmConfigurar_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabComp.ResumeLayout(false);
            this.tabComp.PerformLayout();
            this.grpCantImp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRecibos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRemi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFactu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPresu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNivelFact)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNivelStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNivel)).EndInit();
            this.tabOtros.ResumeLayout(false);
            this.grbCodificacion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nroProd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nroIns)).EndInit();
            this.grpListaPedido.ResumeLayout(false);
            this.grpListaPedido.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nCantMin)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabComp;
        private System.Windows.Forms.TabPage tabOtros;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkImpresion;
        private System.Windows.Forms.Label lblPorcIVA;
        private System.Windows.Forms.TextBox txtPorcIVA;
        private System.Windows.Forms.TextBox txtMesPresu;
        private System.Windows.Forms.TextBox txtMesPed;
        private System.Windows.Forms.Label lblMesPresu;
        private System.Windows.Forms.Label lblMesPed;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.Label lblAlmacen;
        private System.Windows.Forms.Label lblPtoVta;
        private System.Windows.Forms.ComboBox cboAlmacen;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.GroupBox grpListaPedido;
        private System.Windows.Forms.NumericUpDown nCantMin;
        private System.Windows.Forms.Label lblCantMin;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer2;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private System.Windows.Forms.ComboBox cboPvAFIP;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape3;
        private System.Windows.Forms.GroupBox grbCodificacion;
        private System.Windows.Forms.NumericUpDown nroProd;
        private System.Windows.Forms.Label lblUltProducto;
        private System.Windows.Forms.NumericUpDown nroIns;
        private System.Windows.Forms.Label lblUltInsumo;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape4;
        private System.Windows.Forms.NumericUpDown txtNivel;
        private System.Windows.Forms.Label lblNivelBajas;
        private System.Windows.Forms.NumericUpDown txtNivelStock;
        private System.Windows.Forms.Label lblNivelStock;
        private System.Windows.Forms.NumericUpDown txtNivelFact;
        private System.Windows.Forms.Label lblFacturacion;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblLimitCdba;
        private System.Windows.Forms.TextBox txtLimitCdbaLimit;
        private System.Windows.Forms.TextBox txtPorcLimitCba;
        private System.Windows.Forms.GroupBox grpCantImp;
        private System.Windows.Forms.NumericUpDown tabNum;
        private System.Windows.Forms.Label lblPedidos;
        private System.Windows.Forms.NumericUpDown numRecibos;
        private System.Windows.Forms.Label lblRecibo;
        private System.Windows.Forms.NumericUpDown numRemi;
        private System.Windows.Forms.NumericUpDown numFactu;
        private System.Windows.Forms.NumericUpDown numPresu;
        private System.Windows.Forms.Label lblRemitos;
        private System.Windows.Forms.Label lblFacturas;
        private System.Windows.Forms.Label lblPresupuestos;        
    }
}
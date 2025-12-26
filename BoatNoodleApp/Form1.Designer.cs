namespace BoatNoodleApp
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lbCustomerName = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.lbTableNumber = new System.Windows.Forms.Label();
            this.cbxTableNumber = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxMainMenu = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxNoodleType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxSpiciness = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxPortionType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pbMenuImage = new System.Windows.Forms.PictureBox();
            this.btnAddOrder = new System.Windows.Forms.Button();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.MenuName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoodleType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SpicinessLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PortionType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.btnConfirmOrder = new System.Windows.Forms.Button();
            this.cbxMenu = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.btnOpenAdmin = new System.Windows.Forms.Button();
            this.btnOpenAbout = new System.Windows.Forms.Button();
            this.lblSubTotal = new System.Windows.Forms.Label();
            this.lblVat = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblGrandTotalForm1 = new System.Windows.Forms.Label();
            this.btnSwitchUser = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbMenuImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // lbCustomerName
            // 
            this.lbCustomerName.AutoSize = true;
            this.lbCustomerName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(181)))));
            this.lbCustomerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCustomerName.Location = new System.Drawing.Point(534, 139);
            this.lbCustomerName.Name = "lbCustomerName";
            this.lbCustomerName.Size = new System.Drawing.Size(78, 24);
            this.lbCustomerName.TabIndex = 0;
            this.lbCustomerName.Text = "ชื่อลูกค้า :";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtCustomerName.Location = new System.Drawing.Point(618, 137);
            this.txtCustomerName.MaxLength = 10;
            this.txtCustomerName.Multiline = true;
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(105, 27);
            this.txtCustomerName.TabIndex = 1;
            this.txtCustomerName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCustomerName_KeyPress);
            // 
            // lbTableNumber
            // 
            this.lbTableNumber.AutoSize = true;
            this.lbTableNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(181)))));
            this.lbTableNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTableNumber.Location = new System.Drawing.Point(745, 138);
            this.lbTableNumber.Name = "lbTableNumber";
            this.lbTableNumber.Size = new System.Drawing.Size(112, 24);
            this.lbTableNumber.TabIndex = 2;
            this.lbTableNumber.Text = "หมายเลขโต๊ะ :";
            // 
            // cbxTableNumber
            // 
            this.cbxTableNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbxTableNumber.FormattingEnabled = true;
            this.cbxTableNumber.Location = new System.Drawing.Point(863, 137);
            this.cbxTableNumber.Name = "cbxTableNumber";
            this.cbxTableNumber.Size = new System.Drawing.Size(74, 26);
            this.cbxTableNumber.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(99, 207);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 4;
            // 
            // cbxMainMenu
            // 
            this.cbxMainMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbxMainMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbxMainMenu.FormattingEnabled = true;
            this.cbxMainMenu.Location = new System.Drawing.Point(173, 191);
            this.cbxMainMenu.Name = "cbxMainMenu";
            this.cbxMainMenu.Size = new System.Drawing.Size(116, 26);
            this.cbxMainMenu.TabIndex = 5;
            this.cbxMainMenu.SelectedIndexChanged += new System.EventHandler(this.cbxMainMenu_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(181)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(314, 224);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "ชื่อเมนู :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(181)))));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(570, 225);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "เลือกเส้น :";
            // 
            // cbxNoodleType
            // 
            this.cbxNoodleType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbxNoodleType.FormattingEnabled = true;
            this.cbxNoodleType.Location = new System.Drawing.Point(647, 224);
            this.cbxNoodleType.Name = "cbxNoodleType";
            this.cbxNoodleType.Size = new System.Drawing.Size(78, 26);
            this.cbxNoodleType.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(181)))));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(314, 270);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "ระดับความเผ็ด :";
            // 
            // cbxSpiciness
            // 
            this.cbxSpiciness.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbxSpiciness.FormattingEnabled = true;
            this.cbxSpiciness.Location = new System.Drawing.Point(444, 271);
            this.cbxSpiciness.Name = "cbxSpiciness";
            this.cbxSpiciness.Size = new System.Drawing.Size(78, 26);
            this.cbxSpiciness.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(181)))));
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(771, 223);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "ขนาด :";
            // 
            // cbxPortionType
            // 
            this.cbxPortionType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbxPortionType.FormattingEnabled = true;
            this.cbxPortionType.Location = new System.Drawing.Point(828, 221);
            this.cbxPortionType.Name = "cbxPortionType";
            this.cbxPortionType.Size = new System.Drawing.Size(78, 26);
            this.cbxPortionType.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(181)))));
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(570, 269);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "จำนวน :";
            // 
            // pbMenuImage
            // 
            this.pbMenuImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbMenuImage.Location = new System.Drawing.Point(62, 223);
            this.pbMenuImage.Name = "pbMenuImage";
            this.pbMenuImage.Size = new System.Drawing.Size(227, 137);
            this.pbMenuImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbMenuImage.TabIndex = 15;
            this.pbMenuImage.TabStop = false;
            // 
            // btnAddOrder
            // 
            this.btnAddOrder.BackColor = System.Drawing.Color.MediumAquamarine;
            this.btnAddOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnAddOrder.Location = new System.Drawing.Point(318, 319);
            this.btnAddOrder.Name = "btnAddOrder";
            this.btnAddOrder.Size = new System.Drawing.Size(141, 32);
            this.btnAddOrder.TabIndex = 16;
            this.btnAddOrder.Text = "เพิ่มไปยังตะกร้า 🛒";
            this.btnAddOrder.UseVisualStyleBackColor = false;
            this.btnAddOrder.Click += new System.EventHandler(this.btnAddOrder_Click);
            // 
            // dgvCart
            // 
            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Transparent;
            this.dgvCart.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvCart.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvCart.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCart.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCart.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MenuName,
            this.NoodleType,
            this.SpicinessLevel,
            this.PortionType,
            this.Quantity,
            this.UnitPrice,
            this.Subtotal});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.InactiveBorder;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCart.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvCart.Location = new System.Drawing.Point(12, 372);
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.ReadOnly = true;
            this.dgvCart.RowHeadersWidth = 50;
            this.dgvCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size = new System.Drawing.Size(768, 202);
            this.dgvCart.TabIndex = 18;
            this.dgvCart.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvCart_RowPostPaint);
            // 
            // MenuName
            // 
            this.MenuName.DataPropertyName = "MenuName";
            this.MenuName.HeaderText = "เมนู";
            this.MenuName.Name = "MenuName";
            this.MenuName.ReadOnly = true;
            this.MenuName.Width = 180;
            // 
            // NoodleType
            // 
            this.NoodleType.DataPropertyName = "NoodleType";
            this.NoodleType.HeaderText = "เส้น";
            this.NoodleType.Name = "NoodleType";
            this.NoodleType.ReadOnly = true;
            // 
            // SpicinessLevel
            // 
            this.SpicinessLevel.DataPropertyName = "SpicinessLevel";
            this.SpicinessLevel.HeaderText = "ความเผ็ด";
            this.SpicinessLevel.Name = "SpicinessLevel";
            this.SpicinessLevel.ReadOnly = true;
            // 
            // PortionType
            // 
            this.PortionType.DataPropertyName = "PortionType";
            this.PortionType.HeaderText = "ขนาด";
            this.PortionType.Name = "PortionType";
            this.PortionType.ReadOnly = true;
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "Quantity";
            this.Quantity.HeaderText = "จำนวน";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            this.Quantity.Width = 60;
            // 
            // UnitPrice
            // 
            this.UnitPrice.DataPropertyName = "UnitPrice";
            this.UnitPrice.HeaderText = "ราคาจริง/หน่วย";
            this.UnitPrice.Name = "UnitPrice";
            this.UnitPrice.ReadOnly = true;
            // 
            // Subtotal
            // 
            this.Subtotal.DataPropertyName = "Subtotal";
            this.Subtotal.HeaderText = "ราคารวม";
            this.Subtotal.Name = "Subtotal";
            this.Subtotal.ReadOnly = true;
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.BackColor = System.Drawing.Color.LightCoral;
            this.btnRemoveItem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemoveItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnRemoveItem.Location = new System.Drawing.Point(837, 540);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(100, 34);
            this.btnRemoveItem.TabIndex = 19;
            this.btnRemoveItem.Text = "ลบรายการ";
            this.btnRemoveItem.UseVisualStyleBackColor = false;
            this.btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click);
            // 
            // btnConfirmOrder
            // 
            this.btnConfirmOrder.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnConfirmOrder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConfirmOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnConfirmOrder.Location = new System.Drawing.Point(837, 503);
            this.btnConfirmOrder.Name = "btnConfirmOrder";
            this.btnConfirmOrder.Size = new System.Drawing.Size(100, 31);
            this.btnConfirmOrder.TabIndex = 20;
            this.btnConfirmOrder.Text = "ยืนยัน";
            this.btnConfirmOrder.UseVisualStyleBackColor = false;
            this.btnConfirmOrder.Click += new System.EventHandler(this.btnConfirmOrder_Click);
            // 
            // cbxMenu
            // 
            this.cbxMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbxMenu.FormattingEnabled = true;
            this.cbxMenu.Location = new System.Drawing.Point(389, 223);
            this.cbxMenu.Name = "cbxMenu";
            this.cbxMenu.Size = new System.Drawing.Size(107, 26);
            this.cbxMenu.TabIndex = 21;
            this.cbxMenu.SelectedIndexChanged += new System.EventHandler(this.cbxMenu_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(181)))));
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(54, 191);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 24);
            this.label7.TabIndex = 22;
            this.label7.Text = "เลือกเมนูหลัก :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(181)))));
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label8.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label8.Location = new System.Drawing.Point(-19, 166);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1027, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = resources.GetString("label8.Text");
            // 
            // nudQuantity
            // 
            this.nudQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.nudQuantity.Location = new System.Drawing.Point(647, 271);
            this.nudQuantity.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(51, 26);
            this.nudQuantity.TabIndex = 24;
            this.nudQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnOpenAdmin
            // 
            this.btnOpenAdmin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnOpenAdmin.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOpenAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnOpenAdmin.Location = new System.Drawing.Point(31, 132);
            this.btnOpenAdmin.Name = "btnOpenAdmin";
            this.btnOpenAdmin.Size = new System.Drawing.Size(100, 30);
            this.btnOpenAdmin.TabIndex = 25;
            this.btnOpenAdmin.Text = "▶ Admin";
            this.btnOpenAdmin.UseVisualStyleBackColor = false;
            this.btnOpenAdmin.Click += new System.EventHandler(this.btnOpenAdmin_Click);
            // 
            // btnOpenAbout
            // 
            this.btnOpenAbout.BackColor = System.Drawing.Color.LightSalmon;
            this.btnOpenAbout.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOpenAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnOpenAbout.Location = new System.Drawing.Point(152, 132);
            this.btnOpenAbout.Name = "btnOpenAbout";
            this.btnOpenAbout.Size = new System.Drawing.Size(113, 30);
            this.btnOpenAbout.TabIndex = 26;
            this.btnOpenAbout.Text = "♥ About us";
            this.btnOpenAbout.UseVisualStyleBackColor = false;
            this.btnOpenAbout.Click += new System.EventHandler(this.btnOpenAbout_Click);
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubTotal.AutoSize = true;
            this.lblSubTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(181)))));
            this.lblSubTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblSubTotal.Location = new System.Drawing.Point(791, 372);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(125, 20);
            this.lblSubTotal.TabIndex = 27;
            this.lblSubTotal.Text = "ยอดรวมย่อย : 0.00";
            this.lblSubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblVat
            // 
            this.lblVat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVat.AutoSize = true;
            this.lblVat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(181)))));
            this.lblVat.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblVat.Location = new System.Drawing.Point(796, 409);
            this.lblVat.Name = "lblVat";
            this.lblVat.Size = new System.Drawing.Size(110, 18);
            this.lblVat.TabIndex = 28;
            this.lblVat.Text = "VAT (7%) : 0.00";
            this.lblVat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(181)))));
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label9.Location = new System.Drawing.Point(798, 436);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(118, 18);
            this.label9.TabIndex = 29;
            this.label9.Text = "----------------------";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGrandTotalForm1
            // 
            this.lblGrandTotalForm1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGrandTotalForm1.AutoSize = true;
            this.lblGrandTotalForm1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(181)))));
            this.lblGrandTotalForm1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblGrandTotalForm1.Location = new System.Drawing.Point(795, 465);
            this.lblGrandTotalForm1.Name = "lblGrandTotalForm1";
            this.lblGrandTotalForm1.Size = new System.Drawing.Size(114, 20);
            this.lblGrandTotalForm1.TabIndex = 30;
            this.lblGrandTotalForm1.Text = "ยอดรวม : 0.00";
            this.lblGrandTotalForm1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSwitchUser
            // 
            this.btnSwitchUser.BackColor = System.Drawing.Color.LightCoral;
            this.btnSwitchUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSwitchUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSwitchUser.Location = new System.Drawing.Point(836, 73);
            this.btnSwitchUser.Name = "btnSwitchUser";
            this.btnSwitchUser.Size = new System.Drawing.Size(112, 37);
            this.btnSwitchUser.TabIndex = 31;
            this.btnSwitchUser.Text = "ออกจากระบบ";
            this.btnSwitchUser.UseVisualStyleBackColor = false;
            this.btnSwitchUser.Click += new System.EventHandler(this.btnSwitchUser_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(960, 586);
            this.Controls.Add(this.btnSwitchUser);
            this.Controls.Add(this.lblGrandTotalForm1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblVat);
            this.Controls.Add(this.lblSubTotal);
            this.Controls.Add(this.btnOpenAbout);
            this.Controls.Add(this.btnOpenAdmin);
            this.Controls.Add(this.nudQuantity);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbxMenu);
            this.Controls.Add(this.btnConfirmOrder);
            this.Controls.Add(this.btnRemoveItem);
            this.Controls.Add(this.dgvCart);
            this.Controls.Add(this.btnAddOrder);
            this.Controls.Add(this.pbMenuImage);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbxPortionType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbxSpiciness);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbxNoodleType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxMainMenu);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxTableNumber);
            this.Controls.Add(this.lbTableNumber);
            this.Controls.Add(this.txtCustomerName);
            this.Controls.Add(this.lbCustomerName);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbMenuImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbCustomerName;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.Label lbTableNumber;
        private System.Windows.Forms.ComboBox cbxTableNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxMainMenu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxNoodleType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxSpiciness;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxPortionType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pbMenuImage;
        private System.Windows.Forms.Button btnAddOrder;
        private System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Button btnConfirmOrder;
        private System.Windows.Forms.ComboBox cbxMenu;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Button btnOpenAdmin;
        private System.Windows.Forms.DataGridViewTextBoxColumn MenuName;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoodleType;
        private System.Windows.Forms.DataGridViewTextBoxColumn SpicinessLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn PortionType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subtotal;
        private System.Windows.Forms.Button btnOpenAbout;
        private System.Windows.Forms.Label lblSubTotal;
        private System.Windows.Forms.Label lblVat;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblGrandTotalForm1;
        private System.Windows.Forms.Button btnSwitchUser;
    }
}


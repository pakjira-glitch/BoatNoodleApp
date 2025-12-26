namespace BoatNoodleApp
{
    partial class FormPayment
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle45 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle46 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPayment));
            this.lblCustomerInfo = new System.Windows.Forms.Label();
            this.dgvPaymentItems = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.pbQRCode = new System.Windows.Forms.PictureBox();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.btnUploadSlip = new System.Windows.Forms.Button();
            this.txtSlipPath = new System.Windows.Forms.TextBox();
            this.lblPaymentDateTime = new System.Windows.Forms.Label();
            this.btnConfirmPayment = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaymentItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbQRCode)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCustomerInfo
            // 
            this.lblCustomerInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCustomerInfo.AutoSize = true;
            this.lblCustomerInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(181)))));
            this.lblCustomerInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblCustomerInfo.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblCustomerInfo.Location = new System.Drawing.Point(712, 199);
            this.lblCustomerInfo.Name = "lblCustomerInfo";
            this.lblCustomerInfo.Size = new System.Drawing.Size(222, 24);
            this.lblCustomerInfo.TabIndex = 0;
            this.lblCustomerInfo.Text = "ลูกค้า : [ชื่อลูกค้า]   โต๊ะ : [เลข]";
            this.lblCustomerInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvPaymentItems
            // 
            this.dgvPaymentItems.AllowUserToAddRows = false;
            this.dgvPaymentItems.AllowUserToDeleteRows = false;
            this.dgvPaymentItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPaymentItems.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle45.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle45.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle45.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle45.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle45.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle45.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle45.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPaymentItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle45;
            this.dgvPaymentItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle46.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle46.BackColor = System.Drawing.SystemColors.InactiveCaption;
            dataGridViewCellStyle46.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle46.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle46.SelectionBackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle46.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle46.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPaymentItems.DefaultCellStyle = dataGridViewCellStyle46;
            this.dgvPaymentItems.Location = new System.Drawing.Point(355, 230);
            this.dgvPaymentItems.Name = "dgvPaymentItems";
            this.dgvPaymentItems.ReadOnly = true;
            this.dgvPaymentItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPaymentItems.Size = new System.Drawing.Size(567, 226);
            this.dgvPaymentItems.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(181)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(74, 203);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "สแกน QR Code เพื่อชำระเงิน";
            // 
            // pbQRCode
            // 
            this.pbQRCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbQRCode.BackgroundImage")));
            this.pbQRCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbQRCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbQRCode.Location = new System.Drawing.Point(57, 230);
            this.pbQRCode.Name = "pbQRCode";
            this.pbQRCode.Size = new System.Drawing.Size(227, 236);
            this.pbQRCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbQRCode.TabIndex = 3;
            this.pbQRCode.TabStop = false;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(181)))));
            this.lblTotalAmount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblTotalAmount.Location = new System.Drawing.Point(62, 495);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(222, 24);
            this.lblTotalAmount.TabIndex = 4;
            this.lblTotalAmount.Text = "ยอดรวมทั้งหมด: 0.00 บาท";
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnUploadSlip
            // 
            this.btnUploadSlip.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnUploadSlip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUploadSlip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnUploadSlip.Location = new System.Drawing.Point(357, 477);
            this.btnUploadSlip.Name = "btnUploadSlip";
            this.btnUploadSlip.Size = new System.Drawing.Size(119, 33);
            this.btnUploadSlip.TabIndex = 5;
            this.btnUploadSlip.Text = "อัปโหลดสลิป";
            this.btnUploadSlip.UseVisualStyleBackColor = false;
            this.btnUploadSlip.Click += new System.EventHandler(this.btnUploadSlip_Click);
            // 
            // txtSlipPath
            // 
            this.txtSlipPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtSlipPath.Location = new System.Drawing.Point(494, 479);
            this.txtSlipPath.Name = "txtSlipPath";
            this.txtSlipPath.ReadOnly = true;
            this.txtSlipPath.Size = new System.Drawing.Size(174, 29);
            this.txtSlipPath.TabIndex = 6;
            // 
            // lblPaymentDateTime
            // 
            this.lblPaymentDateTime.AutoSize = true;
            this.lblPaymentDateTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(181)))));
            this.lblPaymentDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblPaymentDateTime.Location = new System.Drawing.Point(353, 527);
            this.lblPaymentDateTime.Name = "lblPaymentDateTime";
            this.lblPaymentDateTime.Size = new System.Drawing.Size(148, 24);
            this.lblPaymentDateTime.TabIndex = 7;
            this.lblPaymentDateTime.Text = "เวลาชำระเงิน : N/A";
            // 
            // btnConfirmPayment
            // 
            this.btnConfirmPayment.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnConfirmPayment.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConfirmPayment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnConfirmPayment.Location = new System.Drawing.Point(783, 475);
            this.btnConfirmPayment.Name = "btnConfirmPayment";
            this.btnConfirmPayment.Size = new System.Drawing.Size(139, 36);
            this.btnConfirmPayment.TabIndex = 8;
            this.btnConfirmPayment.Text = "ยืนยันการชำระเงิน";
            this.btnConfirmPayment.UseVisualStyleBackColor = false;
            this.btnConfirmPayment.Click += new System.EventHandler(this.btnConfirmPayment_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnBack.Location = new System.Drawing.Point(783, 527);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(139, 36);
            this.btnBack.TabIndex = 9;
            this.btnBack.Text = "ย้อนกลับ";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // FormPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(960, 586);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnConfirmPayment);
            this.Controls.Add(this.lblPaymentDateTime);
            this.Controls.Add(this.txtSlipPath);
            this.Controls.Add(this.btnUploadSlip);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.pbQRCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvPaymentItems);
            this.Controls.Add(this.lblCustomerInfo);
            this.Name = "FormPayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormPayment";
            this.Load += new System.EventHandler(this.FormPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaymentItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbQRCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCustomerInfo;
        private System.Windows.Forms.DataGridView dgvPaymentItems;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbQRCode;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Button btnUploadSlip;
        private System.Windows.Forms.TextBox txtSlipPath;
        private System.Windows.Forms.Label lblPaymentDateTime;
        private System.Windows.Forms.Button btnConfirmPayment;
        private System.Windows.Forms.Button btnBack;
    }
}
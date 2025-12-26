namespace BoatNoodleApp
{
    partial class FormForgotPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForgotPassword));
            this.label1 = new System.Windows.Forms.Label();
            this.txtResetPhone = new System.Windows.Forms.TextBox();
            this.btnConfirmReset = new System.Windows.Forms.Button();
            this.btcancle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(223)))), ((int)(((byte)(181)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(147, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "\"กรุณากรอกเบอร์โทรศัพท์เพื่อรีเซ็ตรหัสผ่าน\"";
            // 
            // txtResetPhone
            // 
            this.txtResetPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtResetPhone.Location = new System.Drawing.Point(220, 168);
            this.txtResetPhone.MaxLength = 10;
            this.txtResetPhone.Name = "txtResetPhone";
            this.txtResetPhone.Size = new System.Drawing.Size(178, 31);
            this.txtResetPhone.TabIndex = 1;
            // 
            // btnConfirmReset
            // 
            this.btnConfirmReset.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnConfirmReset.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConfirmReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnConfirmReset.Location = new System.Drawing.Point(179, 249);
            this.btnConfirmReset.Name = "btnConfirmReset";
            this.btnConfirmReset.Size = new System.Drawing.Size(103, 35);
            this.btnConfirmReset.TabIndex = 2;
            this.btnConfirmReset.Text = "ยืนยัน";
            this.btnConfirmReset.UseVisualStyleBackColor = false;
            this.btnConfirmReset.Click += new System.EventHandler(this.btnConfirmReset_Click);
            // 
            // btcancle
            // 
            this.btcancle.BackColor = System.Drawing.Color.Salmon;
            this.btcancle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btcancle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btcancle.Location = new System.Drawing.Point(331, 249);
            this.btcancle.Name = "btcancle";
            this.btcancle.Size = new System.Drawing.Size(103, 35);
            this.btcancle.TabIndex = 3;
            this.btcancle.Text = "ยกเลิก";
            this.btcancle.UseVisualStyleBackColor = false;
            this.btcancle.Click += new System.EventHandler(this.btcancle_Click);
            // 
            // FormForgotPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(620, 380);
            this.Controls.Add(this.btcancle);
            this.Controls.Add(this.btnConfirmReset);
            this.Controls.Add(this.txtResetPhone);
            this.Controls.Add(this.label1);
            this.Name = "FormForgotPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormForgotPassword";
            this.Load += new System.EventHandler(this.FormForgotPassword_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtResetPhone;
        private System.Windows.Forms.Button btnConfirmReset;
        private System.Windows.Forms.Button btcancle;
    }
}
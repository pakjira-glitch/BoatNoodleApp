using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoatNoodleApp
{
    public partial class FormAdminLogin : Form
    {
        // "กระดาษโน้ต" หรือ Flag ที่จะส่งสถานะกลับไปให้ FormRoleSelection
        public bool LoginSuccessful { get; private set; } = false;

        public FormAdminLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string inputPassword = txtPassword.Text;

            // ตรวจสอบว่ารหัสผ่านคือ "12345" หรือไม่
            if (inputPassword == "12345")
            {
                // ถ้ารหัสถูกต้อง
                MessageBox.Show("เข้าสู่ระบบสำเร็จ!", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // *** จุดแก้ไขสำคัญ: ตั้ง Flag ว่าสำเร็จ แล้วปิดตัวเอง ***
                this.LoginSuccessful = true;
                this.Close();
            }
            else
            {
                // ถ้ารหัสผิด
                MessageBox.Show("รหัสผ่านไม่ถูกต้อง กรุณาลองใหม่", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Clear(); // ล้างช่องรหัสผ่าน
                txtPassword.Focus(); // ย้ายเคอร์เซอร์กลับไปที่ช่องเดิม
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); // ปิดหน้านี้ เพื่อกลับไปหน้าเดิม
        }

        private void FormAdminLogin_Load(object sender, EventArgs e)
        {

        }
    }
}

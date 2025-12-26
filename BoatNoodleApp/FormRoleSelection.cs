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
    public partial class FormRoleSelection : Form
    {
        /// "กระดาษโน้ต" ที่จะส่งกลับไปให้ Form1
        public bool IsAdminRole { get; private set; } = false;
        public bool LoginCancelled { get; private set; } = true; // ตั้งค่าเริ่มต้นว่า "ยกเลิก"
        public string LoggedInCustomerName { get; private set; } = string.Empty;

        public FormRoleSelection()
        {
            InitializeComponent();
        }

        private void FormRoleSelection_Load(object sender, EventArgs e)
        {

        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            FormCustomerLogin customerLogin = new FormCustomerLogin();
            this.Hide();
            customerLogin.ShowDialog();

            // อ่าน "กระดาษโน้ต" จากหน้าล็อกอินลูกค้า
            if (customerLogin.LoginSuccessful)
            {
                this.LoginCancelled = false; // ตั้งค่าว่า "ไม่ได้ยกเลิก"
                this.IsAdminRole = false;
                this.LoggedInCustomerName = customerLogin.LoggedInCustomerName;
                this.Close(); // ปิดตัวเองเพื่อกลับไปที่ Form1
            }
            else
            {
                this.Show(); // ถ้าลูกค้ากดย้อนกลับ ให้กลับมาแสดงหน้านี้
            }
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            FormAdminLogin adminLogin = new FormAdminLogin();
            this.Hide(); // ซ่อนหน้าเลือก Role
            adminLogin.ShowDialog(); // เปิดหน้าล็อกอิน Admin

            // --- โค้ดส่วนนี้จะทำงาน "หลังจาก" ที่หน้า AdminLogin ถูกปิด ---

            // อ่าน "กระดาษโน้ต" จากหน้าล็อกอินแอดมิน
            if (adminLogin.LoginSuccessful)
            {
                // ถ้าล็อกอินสำเร็จ, ให้จดโน้ตว่า "ผู้ใช้เลือกบทบาทแอดมิน"
                this.LoginCancelled = false;
                this.IsAdminRole = true;
                this.Close(); // ปิดตัวเองเพื่อกลับไปที่ Form1
            }
            else
            {
                // ถ้าแอดมินกดย้อนกลับในหน้าล็อกอิน, ให้กลับมาแสดงหน้านี้
                this.Show();
            }
        }

        private void btnSwitchUser_Click(object sender, EventArgs e)
        {

        }
    }
}

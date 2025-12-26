using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic; // สำหรับ InputBox


namespace BoatNoodleApp
{
    public partial class FormForgotPassword : Form
    {
        private string connectionString = "server=localhost;database=user;uid=root;password=;";

        public FormForgotPassword()
        {
            InitializeComponent();
        }

        private void FormForgotPassword_Load(object sender, EventArgs e)
        {

        }

        private void btnConfirmReset_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtResetPhone.Text))
            {
                MessageBox.Show("กรุณากรอกเบอร์โทรศัพท์");
                return;
            }

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    // ตรวจสอบว่ามีเบอร์นี้ในระบบหรือไม่
                    string checkUserQuery = "SELECT COUNT(*) FROM customers WHERE PhoneNumber = @PhoneNumber";
                    MySqlCommand checkCmd = new MySqlCommand(checkUserQuery, con);
                    checkCmd.Parameters.AddWithValue("@PhoneNumber", txtResetPhone.Text);
                    if ((long)checkCmd.ExecuteScalar() == 0)
                    {
                        MessageBox.Show("ไม่พบเบอร์โทรศัพท์นี้ในระบบ");
                        return;
                    }

                    // ใช้ InputBox ของ VB เพื่อความรวดเร็ว
                    string newPassword = Interaction.InputBox("กรุณาตั้งรหัสผ่านใหม่:", "รีเซ็ตรหัสผ่าน", "");
                    if (string.IsNullOrWhiteSpace(newPassword)) return; // ถ้าผู้ใช้กดยกเลิก

                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);

                    string updateQuery = "UPDATE customers SET PasswordHash = @Hash WHERE PhoneNumber = @PhoneNumber";
                    MySqlCommand updateCmd = new MySqlCommand(updateQuery, con);
                    updateCmd.Parameters.AddWithValue("@Hash", passwordHash);
                    updateCmd.Parameters.AddWithValue("@PhoneNumber", txtResetPhone.Text);
                    updateCmd.ExecuteNonQuery();

                    MessageBox.Show("เปลี่ยนรหัสผ่านสำเร็จ!", "สำเร็จ");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
                }
            }
        }

        private void btcancle_Click(object sender, EventArgs e)
        {
            this.Close(); // สั่งให้ฟอร์มนี้ปิดตัวเอง
        }
    }
}

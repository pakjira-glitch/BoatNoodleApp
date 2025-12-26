using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic;
using System.Linq;
using BCrypt.Net;
using Mysqlx.Notice;
using Mysqlx;

namespace BoatNoodleApp
{
    public partial class FormCustomerLogin : Form
    {
        private string connectionString = "server=localhost;database=user;uid=root;password=;";

        // "กระดาษโน้ต" หรือ Flags ที่จะส่งสถานะกลับไปให้ FormRoleSelection
        public bool LoginSuccessful { get; private set; } = false;
        public string LoggedInCustomerName { get; private set; } = string.Empty;

        public FormCustomerLogin()
        {
            InitializeComponent();
        }

        private void FormCustomerLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text) ||
                string.IsNullOrWhiteSpace(txtPhoneNumber.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("กรุณากรอกข้อมูล ชื่อ, เบอร์โทรศัพท์, และรหัสผ่านให้ครบทุกช่อง", "ข้อมูลไม่ครบถ้วน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    // 1. ตรวจสอบว่าเบอร์โทรนี้ถูกใช้ไปแล้วหรือยัง
                    string checkUserQuery = "SELECT COUNT(*) FROM customers WHERE PhoneNumber = @PhoneNumber";
                    MySqlCommand checkCmd = new MySqlCommand(checkUserQuery, con);
                    checkCmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text);
                    long userExists = (long)checkCmd.ExecuteScalar();

                    if (userExists > 0)
                    {
                        MessageBox.Show("เบอร์โทรศัพท์นี้ถูกลงทะเบียนแล้ว กรุณาใช้เบอร์อื่น", "ลงทะเบียนซ้ำ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 2. เข้ารหัสผ่านด้วย Bcrypt
                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text);

                    // 3. บันทึกข้อมูลลูกค้าใหม่
                    string insertQuery = "INSERT INTO customers (CustomerName, PhoneNumber, PasswordHash) VALUES (@Name, @Phone, @Hash)";
                    MySqlCommand insertCmd = new MySqlCommand(insertQuery, con);
                    insertCmd.Parameters.AddWithValue("@Name", txtCustomerName.Text);
                    insertCmd.Parameters.AddWithValue("@Phone", txtPhoneNumber.Text);
                    insertCmd.Parameters.AddWithValue("@Hash", passwordHash);
                    insertCmd.ExecuteNonQuery();

                    MessageBox.Show("ลงทะเบียนสำเร็จ! กรุณาใช้เบอร์โทรศัพท์และรหัสผ่านเพื่อเข้าสู่ระบบ", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // ล้างข้อมูลเพื่อให้พร้อมล็อกอิน
                    txtCustomerName.Clear();
                    txtPassword.Clear();
                    txtPhoneNumber.Focus(); // ย้ายเคอร์เซอร์ไปรอที่เบอร์โทร
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการลงทะเบียน: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPhoneNumber.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("กรุณากรอกเบอร์โทรศัพท์และรหัสผ่าน", "ข้อมูลไม่ครบถ้วน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT CustomerName, PasswordHash FROM customers WHERE PhoneNumber = @PhoneNumber";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string customerName = reader["CustomerName"].ToString();
                            string storedHash = reader["PasswordHash"].ToString();

                            // ตรวจสอบรหัสผ่านที่เข้ารหัสไว้
                            if (BCrypt.Net.BCrypt.Verify(txtPassword.Text, storedHash))
                            {
                                // *** จุดแก้ไขสำคัญ: ตั้งค่า Flags แล้วปิดตัวเอง ***
                                this.LoginSuccessful = true;
                                this.LoggedInCustomerName = customerName;
                                this.Close(); // ปิดฟอร์มนี้เพื่อกลับไปที่ FormRoleSelection
                            }
                            else
                            {
                                MessageBox.Show("รหัสผ่านไม่ถูกต้อง", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("ไม่พบบัญชีผู้ใช้ที่ลงทะเบียนด้วยเบอร์โทรศัพท์นี้", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการเข้าสู่ระบบ: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void llblForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // เปิดหน้าลืมรหัสผ่านขึ้นมาทับ
            FormForgotPassword forgotForm = new FormForgotPassword();
            forgotForm.ShowDialog();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); // สั่งให้ฟอร์มนี้ปิดตัวเอง
        }

        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // อนุญาตให้ใช้ปุ่มควบคุมได้ (เช่น Backspace)
            if (char.IsControl(e.KeyChar))
            {
                return;
            }
            // ตรวจสอบว่าปุ่มที่กด "ไม่ใช่" ตัวเลข
            if (!char.IsDigit(e.KeyChar))
            {
                // ถ้าไม่ใช่ตัวเลข ให้ยกเลิกการพิมพ์
                e.Handled = true;
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            // อนุญาต Backspace
            if (char.IsControl(e.KeyChar))
            {
                return;
            }
            // อนุญาตเฉพาะตัวอักษร (a-z, A-Z) และตัวเลข (0-9)
            if (!char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}

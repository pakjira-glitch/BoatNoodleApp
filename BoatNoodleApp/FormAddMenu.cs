using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BoatNoodleApp
{
    public partial class FormAddMenu : Form
    {
        private string connectionString = "server=localhost;database=user;uid=root;password=;";
        private string _selectedImagePath = string.Empty;
        public bool MenuAdded { get; private set; } = false;

        public FormAddMenu()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); // สั่งให้ปิดฟอร์มนี้
        }

        private void btnSaveNewMenu_Click_1(object sender, EventArgs e)
        {
            // --- ส่วนที่ 1: ตรวจสอบข้อมูล (เพิ่มการตรวจสอบ Category) ---
            if (string.IsNullOrWhiteSpace(txtNewMenuName.Text) ||
                nudNewPrice.Value <= 0 ||
                string.IsNullOrWhiteSpace(txtNewMenuCategory.Text))
            {
                MessageBox.Show("กรุณากรอกชื่อเมนู, ราคา, และหมวดหมู่ให้ครบถ้วนและถูกต้อง", "ข้อมูลไม่ครบถ้วน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- ส่วนที่ 2: จัดการรูปภาพ (เหมือนเดิม) ---
            string finalImagePath = string.Empty;
            if (!string.IsNullOrEmpty(_selectedImagePath))
            {
                try
                {
                    string imageFolder = Path.Combine(Application.StartupPath, "images");
                    if (!Directory.Exists(imageFolder)) Directory.CreateDirectory(imageFolder);
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(_selectedImagePath);
                    finalImagePath = Path.Combine(imageFolder, fileName);
                    File.Copy(_selectedImagePath, finalImagePath, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการบันทึกรูปภาพ: " + ex.Message);
                    return; // หยุดทำงานถ้าบันทึกรูปไม่ได้
                }
            }

            // --- ส่วนที่ 3: บันทึกข้อมูลลงฐานข้อมูล (แก้ไข Bug) ---
            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    string query = "INSERT INTO menus (MenuName, Price, ImagePath, Category) VALUES (@MenuName, @Price, @ImagePath, @Category)";
                    MySqlCommand cmd = new MySqlCommand(query, con);

                    // *** จุดแก้ไข Bug: ส่งค่าให้ถูกพารามิเตอร์ ***
                    cmd.Parameters.AddWithValue("@MenuName", txtNewMenuName.Text); // <--- แก้ไขแล้ว
                    cmd.Parameters.AddWithValue("@Price", nudNewPrice.Value);
                    cmd.Parameters.AddWithValue("@ImagePath", finalImagePath);
                    cmd.Parameters.AddWithValue("@Category", txtNewMenuCategory.Text); // <--- แก้ไขแล้ว

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("เพิ่มเมนูใหม่เรียบร้อยแล้ว!", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.MenuAdded = true;

                // --- ส่วนที่ 4: ล้างฟอร์มเพื่อเตรียมรับข้อมูลใหม่ (ที่คุณต้องการ) ---
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการบันทึกข้อมูลลงฐานข้อมูล: " + ex.Message);
            }
        }

        private void ClearForm()
        {
            txtNewMenuName.Clear();
            txtNewMenuCategory.Clear();
            nudNewPrice.Value = 0; // หรือราคาเริ่มต้นอื่นๆ
            pbNewMenuImage.Image = null;
            _selectedImagePath = string.Empty;
            txtNewMenuName.Focus(); // ย้ายเคอร์เซอร์ไปรอที่ช่องชื่อเมนู
        }

        private void btnSelectImage_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pbNewMenuImage.ImageLocation = openFileDialog.FileName;
                _selectedImagePath = openFileDialog.FileName;
            }
        }

        private void FormAddMenu_Load(object sender, EventArgs e)
        {

        }

        private void txtNewMenuName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 1. ตรวจสอบว่าปุ่มที่กดเป็น "Control Character" หรือไม่ (เช่น Backspace, Enter)
            //    ถ้าใช่, ให้ปล่อยผ่านไปเลย
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            // 2. ตรวจสอบว่าปุ่มที่กดเป็น "ตัวอักษร" (พยัญชนะ), "ช่องว่าง", 
            //    หรือ "เครื่องหมายที่ไม่มีช่องว่าง" (สระลอย/วรรณยุกต์) หรือไม่
            if (char.IsLetter(e.KeyChar) ||
                char.IsWhiteSpace(e.KeyChar) ||
                System.Globalization.CharUnicodeInfo.GetUnicodeCategory(e.KeyChar) == System.Globalization.UnicodeCategory.NonSpacingMark)
            {
                // ถ้าใช่, ก็ไม่ต้องทำอะไร (ปล่อยให้พิมพ์ได้)
            }
            else
            {
                // 3. ถ้าไม่ใช่ทั้งหมดที่กล่าวมา (เช่น เป็นตัวเลข, สัญลักษณ์พิเศษ)
                //    ให้ยกเลิกการพิมพ์
                e.Handled = true;
            }
        }
    }
}

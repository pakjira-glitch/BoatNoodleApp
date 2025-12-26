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
using System.IO;

namespace BoatNoodleApp
{
    public partial class Form1 : Form
    {
        // *** เปลี่ยน YOUR_SERVER, YOUR_DATABASE, YOUR_UID, YOUR_PASSWORD ให้เป็นข้อมูล MySQL ของคุณ ***
        private string connectionString = "server=localhost;database=user;uid=root;password=;";
        private DataTable cartTable;
        private const decimal EXTRA_PRICE_FOR_SPECIAL = 10.00M; // ราคาเพิ่มสำหรับ "พิเศษ"
        private readonly string _tableConfigPath = Path.Combine(Application.StartupPath, "table_config.txt");

        // --- Constructor ที่ 1: สำหรับลูกค้าที่ล็อกอินเข้ามา ---
        // (จะถูกเรียกใช้จาก FormCustomerLogin)
        public Form1(string customerName)
        {
            InitializeComponent();

            // เรียกใช้เมธอดตั้งค่าเริ่มต้นทั้งหมด
            InitializeCartTable();
            SetupNoodleAndSpicinessComboBoxes();
            SetupMainMenuComboBox();
            SetupTableNumberComboBox();

            // กรอกชื่อลูกค้าให้อัตโนมัติและตั้งค่าเป็น ReadOnly
            if (!string.IsNullOrEmpty(customerName))
            {
                txtCustomerName.Text = customerName;
                txtCustomerName.ReadOnly = true;
            }
        }

        // --- Constructor ที่ 2: สำหรับตอนเริ่มต้นโปรแกรม ---
        // (จะถูกเรียกใช้จาก Program.cs)
        public Form1()
        {
            InitializeComponent();

            // เรียกใช้เมธอดตั้งค่าเริ่มต้นทั้งหมด
            InitializeCartTable();
            SetupNoodleAndSpicinessComboBoxes();
            SetupMainMenuComboBox();
            SetupTableNumberComboBox();
        }

        private void SetupTableNumberComboBox()
        {
            // ล้างค่าเก่าใน ComboBox ก่อน
            cbxTableNumber.Items.Clear();

            int tableCount = 10; // กำหนดค่าเริ่มต้นไว้ที่ 10 โต๊ะ เผื่อกรณีที่หาไฟล์ไม่เจอ
            try
            {
                // ตรวจสอบว่ามีไฟล์ config อยู่หรือไม่
                if (File.Exists(_tableConfigPath))
                {
                    // อ่านข้อความ (ตัวเลข) จากไฟล์
                    string countStr = File.ReadAllText(_tableConfigPath);
                    // พยายามแปลงข้อความที่อ่านได้เป็นตัวเลข
                    if (int.TryParse(countStr, out int savedCount) && savedCount > 0)
                    {
                        // ถ้าแปลงสำเร็จและมากกว่า 0 ให้ใช้ค่าที่อ่านได้
                        tableCount = savedCount;
                    }
                }
            }
            catch
            {
                // ถ้าเกิด Error ระหว่างการอ่านไฟล์ (เช่น ไฟล์ถูกล็อค)
                // ก็ไม่ต้องทำอะไร ให้ใช้ค่าเริ่มต้น 10 โต๊ะไปก่อน
                tableCount = 10;
            }

            // วนลูปเพื่อเติมหมายเลขโต๊ะลงใน ComboBox ตามจำนวนที่ได้มา
            for (int i = 1; i <= tableCount; i++)
            {
                cbxTableNumber.Items.Add(i);
            }

            // ตรวจสอบว่ามีรายการใน ComboBox หรือไม่ แล้วเลือกรายการแรกเป็นค่าเริ่มต้น
            if (cbxTableNumber.Items.Count > 0)
            {
                cbxTableNumber.SelectedIndex = 0;
            }
        }

        private void InitializeCartTable()
        {
            cartTable = new DataTable();
            cartTable.Columns.Add("MenuID", typeof(int));
            cartTable.Columns.Add("MenuName", typeof(string));
            cartTable.Columns.Add("NoodleType", typeof(string));
            cartTable.Columns.Add("SpicinessLevel", typeof(string));
            cartTable.Columns.Add("PortionType", typeof(string)); // เพิ่มคอลัมน์ PortionType
            cartTable.Columns.Add("Quantity", typeof(int));
            cartTable.Columns.Add("UnitPrice", typeof(decimal)); // ราคาต่อหน่วย (ราคาเมนูเริ่มต้น)
            cartTable.Columns.Add("Subtotal", typeof(decimal)); // ราคารวมของรายการนี้ (รวมส่วนเพิ่มพิเศษแล้ว)

            dgvCart.AutoGenerateColumns = false;
            dgvCart.DataSource = cartTable;
        }

        private void SetupMainMenuComboBox()
        {
            DataTable dtCategories = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                // ดึงรายชื่อหมวดหมู่ที่ไม่ซ้ำกันทั้งหมดจากตาราง menus
                string query = "SELECT DISTINCT Category FROM menus ORDER BY Category";
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                da.Fill(dtCategories);
            }

            // ล้างค่าเก่า
            cbxMainMenu.DataSource = null;
            cbxMainMenu.Items.Clear();

            // เติมหมวดหมู่ที่ได้จากฐานข้อมูลลงไป
            foreach (DataRow row in dtCategories.Rows)
            {
                cbxMainMenu.Items.Add(row["Category"].ToString());
            }

            if (cbxMainMenu.Items.Count > 0)
            {
                cbxMainMenu.SelectedIndex = 0; // เลือกหมวดหมู่แรกเป็นค่าเริ่มต้น
            }
            LoadMenus(); // โหลดเมนูย่อยครั้งแรก
        }

        private void LoadMenus()
        {
            cbxMenu.DataSource = null;
            cbxMenu.Items.Clear();

            cbxMenu.DisplayMember = "MenuName";
            cbxMenu.ValueMember = "MenuID";
            DataTable dt = new DataTable();

            // ถ้าไม่มีการเลือกเมนูหลัก ให้หยุดทำงาน
            if (cbxMainMenu.SelectedItem == null) return;

            string selectedCategory = cbxMainMenu.SelectedItem.ToString();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                // *** จุดแก้ไขสำคัญ: เปลี่ยนมาใช้ WHERE Category = @Category ***
                string query = "SELECT MenuID, MenuName, Price, ImagePath FROM menus WHERE Category = @Category";
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    // ใช้ Parameter เพื่อความปลอดภัยและรองรับภาษาไทย
                    cmd.Parameters.AddWithValue("@Category", selectedCategory);

                    con.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    cbxMenu.DataSource = dt;
                }
            }

            if (cbxMenu.Items.Count > 0)
            {
                cbxMenu.SelectedIndex = 0;
            }
            else
            {
                pbMenuImage.Image = null;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ซ่อนฟอร์มหลักไปก่อน
            this.Hide();

            // สร้างและเปิดหน้าเลือก Role
            FormRoleSelection roleSelectionForm = new FormRoleSelection();
            roleSelectionForm.ShowDialog();

            // --- โค้ดส่วนนี้จะทำงาน "หลังจาก" ที่ FormRoleSelection ถูกปิด ---

            // ตรวจสอบว่าผู้ใช้กดยกเลิก (ปิดหน้าต่าง) หรือไม่
            if (roleSelectionForm.LoginCancelled)
            {
                // ถ้าใช่, ให้ปิดโปรแกรมไปเลย
                this.Close();
                return; // ออกจากเมธอด
            }

            // ตรวจสอบว่าเป็นบทบาท Admin หรือไม่
            if (roleSelectionForm.IsAdminRole)
            {
                // ถ้าใช่, ให้รีเซ็ตฟอร์มเป็นค่าเริ่มต้น (สำหรับ Admin)
                ResetOrderForm();
            }
            else
            {
                // ถ้าเป็นลูกค้า, ให้กรอกชื่อและล็อก TextBox
                txtCustomerName.Text = roleSelectionForm.LoggedInCustomerName;
                txtCustomerName.ReadOnly = true;
            }

            // แสดงฟอร์มหลัก (Form1) ที่ถูกตั้งค่าเรียบร้อยแล้ว
            this.Show();
        }

        private void ResetOrderForm()
        {
            // ล้างตะกร้า
            if (cartTable != null)
            {
                cartTable.Clear();
            }
            UpdateTotals(); // อัปเดต Label ยอดรวมให้เป็น 0

            // ล้าง/รีเซ็ต Control อื่นๆ
            txtCustomerName.Clear();
            txtCustomerName.ReadOnly = false; // ปลดล็อกให้กรอกได้
            cbxTableNumber.SelectedIndex = 0;
            nudQuantity.Value = 1;

            // ไม่จำเป็นต้องรีเซ็ต ComboBox เมนู เพราะมันจะโหลดตามปกติ
        }

        private void cbxMainMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMenus(); // โหลดเมนูย่อยใหม่ตามเมนูหลักที่เลือก
        }

        private void SetupNoodleAndSpicinessComboBoxes()
        {
            cbxNoodleType.Items.Add("เส้นเล็ก");
            cbxNoodleType.Items.Add("เส้นใหญ่");
            cbxNoodleType.Items.Add("เส้นหมี่");
            cbxNoodleType.Items.Add("บะหมี่");
            cbxNoodleType.Items.Add("มาม่า");
            cbxNoodleType.SelectedIndex = 0;

            cbxSpiciness.Items.Add("ไม่เผ็ด");
            cbxSpiciness.Items.Add("เผ็ดน้อย");
            cbxSpiciness.Items.Add("เผ็ดกลาง");
            cbxSpiciness.Items.Add("เผ็ดมาก");
            cbxSpiciness.SelectedIndex = 0;

            cbxPortionType.Items.Add("ธรรมดา");
            cbxPortionType.Items.Add("พิเศษ");
            cbxPortionType.SelectedIndex = 0;
        }

        private void cbxMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxMenu.SelectedItem != null)
            {
                DataRowView drv = cbxMenu.SelectedItem as DataRowView;
                if (drv != null)
                {
                    string imagePath = drv["ImagePath"].ToString();
                    // ตรวจสอบว่า imagePath เป็น path สัมพัทธ์หรือไม่
                    if (!Path.IsPathRooted(imagePath))
                    {
                        // ถ้าเป็น path สัมพัทธ์ ให้รวมกับ Application.StartupPath
                        imagePath = Path.Combine(Application.StartupPath, imagePath);
                    }

                    if (File.Exists(imagePath))
                    {
                        pbMenuImage.ImageLocation = imagePath;
                        pbMenuImage.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    else
                    {
                        pbMenuImage.Image = null;
                    }
                }
            }
            else
            {
                pbMenuImage.Image = null; // ถ้าไม่มีเมนูให้เลือก
            }
        }

     
        private void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            // 1. ตรวจสอบว่ามีสินค้าในตะกร้าหรือไม่
            if (cartTable.Rows.Count == 0)
            {
                MessageBox.Show("ไม่มีรายการในตะกร้า กรุณาเพิ่มเมนูก่อน", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. ตรวจสอบว่ากรอกข้อมูลลูกค้าครบถ้วนหรือไม่
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text) || cbxTableNumber.SelectedItem == null)
            {
                MessageBox.Show("กรุณากรอกชื่อลูกค้าและเลือกหมายเลขโต๊ะก่อนยืนยันการสั่งซื้อ", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. ดึงข้อมูลหมายเลขโต๊ะ
            int tableNumber = Convert.ToInt32(cbxTableNumber.SelectedItem);

            // 4. คำนวณยอดรวมทั้งหมด (รวม VAT) เพื่อส่งไปหน้าชำระเงิน
            decimal subTotal = cartTable.AsEnumerable().Sum(row => row.Field<decimal>("Subtotal"));
            decimal vat = subTotal * 0.07m;
            decimal grandTotal = subTotal + vat;

            // 5. สร้างและเปิดหน้าชำระเงิน
            FormPayment paymentForm = new FormPayment(connectionString, txtCustomerName.Text, tableNumber, cartTable, grandTotal);
            paymentForm.ShowDialog();

            // --- โค้ดส่วนนี้จะทำงาน "หลังจาก" ที่หน้าชำระเงินและใบเสร็จถูกปิดไปแล้ว ---
            if (paymentForm.OrderConfirmed)
            {
                // *** จุดแก้ไขสำคัญ: เราจะล้างแค่ตะกร้าและส่วนเลือกเมนูเท่านั้น ***

                // 6. ล้างเฉพาะตะกร้าสินค้า
                cartTable.Clear();

                // (เราจะไม่ล้าง txtCustomerName และ cbxTableNumber อีกต่อไป เพื่อให้ชื่อลูกค้าคงอยู่)

                // 7. รีเซ็ตแค่ส่วนที่ใช้เลือกเมนู เพื่อเตรียมรับออเดอร์ใหม่สำหรับลูกค้ารายเดิม
                nudQuantity.Value = 1;
                cbxNoodleType.SelectedIndex = 0;
                cbxSpiciness.SelectedIndex = 0;
                cbxPortionType.SelectedIndex = 0;

                // การโหลดเมนูหลักใหม่ จะช่วยรีเฟรชเมนูย่อยและรูปภาพไปในตัว
                SetupMainMenuComboBox();

                // 8. อัปเดต Label ยอดรวมต่างๆ ให้กลับเป็น 0.00
                UpdateTotals();
            }

        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count > 0)
            {
                int rowIndex = dgvCart.SelectedRows[0].Index;
                cartTable.Rows.RemoveAt(rowIndex);
            }
            else
            {
                MessageBox.Show("กรุณาเลือกรายการที่ต้องการลบ", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            UpdateTotals();
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            // --- ส่วนที่ 1: ตรวจสอบข้อมูลเบื้องต้น (เหมือนเดิม) ---
            if (cbxMenu.SelectedItem == null)
            {
                MessageBox.Show("กรุณาเลือกเมนูก่อน", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text) || cbxTableNumber.SelectedItem == null)
            {
                MessageBox.Show("กรุณากรอกชื่อลูกค้าและเลือกหมายเลขโต๊ะให้ครบถ้วน", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if ((int)nudQuantity.Value <= 0)
            {
                MessageBox.Show("กรุณาระบุจำนวนอย่างน้อย 1", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- ส่วนที่เพิ่มเข้ามา: ตรวจสอบการจำกัดจำนวนแถว ---

            // ดึงข้อมูลเมนูที่กำลังจะเพิ่มมาตรวจสอบก่อน
            DataRowView drvTemp = cbxMenu.SelectedItem as DataRowView;
            string menuNameTemp = drvTemp["MenuName"].ToString();
            string noodleTypeTemp = cbxNoodleType.SelectedItem.ToString();
            string spicinessTemp = cbxSpiciness.SelectedItem.ToString();
            string portionTypeTemp = cbxPortionType.SelectedItem.ToString();

            // ค้นหาว่ารายการนี้มีอยู่ในตะกร้าแล้วหรือยัง
            bool isExistingItem = false;
            foreach (DataRow row in cartTable.Rows)
            {
                if (row["MenuName"].ToString().Equals(menuNameTemp) &&
                    row["NoodleType"].ToString().Equals(noodleTypeTemp) &&
                    row["SpicinessLevel"].ToString().Equals(spicinessTemp) &&
                    row["PortionType"].ToString().Equals(portionTypeTemp))
                {
                    isExistingItem = true;
                    break;
                }
            }

            // ถ้า "ไม่ใช่" รายการที่มีอยู่แล้ว และ "จำนวนแถวในตะกร้าเต็ม 10 แล้ว"
            if (!isExistingItem && cartTable.Rows.Count >= 10)
            {
                // ให้แสดงข้อความแจ้งเตือนและหยุดการทำงาน
                MessageBox.Show("ไม่สามารถเพิ่มรายการได้ เกินจำนวนสูงสุด (10 รายการ) ต่อหนึ่งออเดอร์", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // ออกจากเมธอด ไม่ทำอะไรต่อ
            }

            // --- จบส่วนที่เพิ่มเข้ามา ---

            // --- ส่วนที่ 2: ดึงข้อมูลและคำนวณราคา (เหมือนเดิม) ---
            DataRowView drv = cbxMenu.SelectedItem as DataRowView;
            int menuId = Convert.ToInt32(drv["MenuID"]);
            string menuName = drv["MenuName"].ToString();
            decimal baseUnitPrice = Convert.ToDecimal(drv["Price"]);
            string noodleType = cbxNoodleType.SelectedItem.ToString();
            string spiciness = cbxSpiciness.SelectedItem.ToString();
            string portionType = cbxPortionType.SelectedItem.ToString();
            int quantityToSet = (int)nudQuantity.Value;

            decimal actualUnitPrice = baseUnitPrice;
            if (portionType == "พิเศษ")
            {
                actualUnitPrice += EXTRA_PRICE_FOR_SPECIAL;
            }

            // --- ส่วนที่ 3: ตรรกะการค้นหาและเพิ่ม/อัปเดตรายการ (เหมือนเดิม) ---
            bool itemFound = false;

            foreach (DataRow row in cartTable.Rows)
            {
                bool isSameMenu = row["MenuName"].ToString().Equals(menuName);
                bool isSameNoodle = row["NoodleType"].ToString().Equals(noodleType);
                bool isSameSpiciness = row["SpicinessLevel"].ToString().Equals(spiciness);
                bool isSamePortion = row["PortionType"].ToString().Equals(portionType);

                if (isSameMenu && isSameNoodle && isSameSpiciness && isSamePortion)
                {
                    row["Quantity"] = quantityToSet;
                    row["Subtotal"] = Convert.ToDecimal(row["UnitPrice"]) * quantityToSet;
                    itemFound = true;
                    break;
                }
            }

            if (!itemFound)
            {
                decimal subtotal = actualUnitPrice * quantityToSet;
                cartTable.Rows.Add(menuId, menuName, noodleType, spiciness, portionType, quantityToSet, actualUnitPrice, subtotal);
            }

            // --- ส่วนที่ 4: รีเซ็ตค่าและอัปเดตยอดรวม (เหมือนเดิม) ---
            nudQuantity.Value = 1;
            UpdateTotals();
        }

        private void btnOpenAdmin_Click(object sender, EventArgs e)
        {
            // 1. ซ่อนหน้าปัจจุบัน (Form1)
            this.Hide();

            // 2. สร้างและเปิดหน้าล็อกอินของ Admin
            FormAdminLogin adminLogin = new FormAdminLogin();
            adminLogin.ShowDialog();

            // --- โค้ดส่วนนี้จะทำงาน "หลังจาก" ที่หน้า AdminLogin ถูกปิด ---

            // 3. ตรวจสอบว่าล็อกอินสำเร็จหรือไม่
            if (adminLogin.LoginSuccessful)
            {
                // 4. ถ้าสำเร็จ, ค่อยสร้างและเปิดหน้า Admin Dashboard
                FormAdminDashboard adminDashboard = new FormAdminDashboard();
                adminDashboard.ShowDialog();

                // --- โค้ดส่วนนี้จะทำงาน "หลังจาก" ที่หน้า AdminDashboard ถูกปิด ---
                // (ณ จุดนี้ คือการกด "กลับ" จากหน้า Dashboard)

                // 5. ตรวจสอบว่ามีการอัปเดตเมนู/โต๊ะ หรือไม่
                if (adminDashboard.MenusWereUpdated)
                {
                    SetupMainMenuComboBox();
                    SetupTableNumberComboBox();
                    MessageBox.Show("ข้อมูลเมนูและจำนวนโต๊ะถูกรีเฟรชแล้ว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            // 6. "ไม่ว่าล็อกอินจะสำเร็จหรือไม่" (เช่น กดยกเลิก) ให้กลับมาแสดง Form1 เสมอ
            this.Show();
        }

        private void btnOpenAbout_Click(object sender, EventArgs e)
        {
            // 1. สร้างหน้าต่าง About Us ขึ้นมาใหม่
            FormAboutUs aboutForm = new FormAboutUs();

            // 2. สั่งให้เปิดหน้าต่างนั้นขึ้นมาแบบ Dialog
            // (ผู้ใช้ต้องปิดหน้านี้ก่อน ถึงจะกลับไปใช้หน้าหลักได้)
            aboutForm.ShowDialog();
        }

        private void txtCustomerName_KeyPress(object sender, KeyPressEventArgs e)
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

        private void UpdateTotals()
        {
            decimal subTotal = 0;
            foreach (DataRow row in cartTable.Rows)
            {
                subTotal += Convert.ToDecimal(row["Subtotal"]);
            }

            decimal vat = subTotal * 0.07m; // ใช้ 'm' เพื่อบอกว่าเป็นชนิด decimal
            decimal grandTotal = subTotal + vat;

            lblSubTotal.Text = $"ยอดรวมย่อย: {subTotal:N2}";
            lblVat.Text = $"VAT (7%): {vat:N2}";
            lblGrandTotalForm1.Text = $"ยอดสุทธิ: {grandTotal:N2}";
        }

        private void dgvCart_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // 1. สร้างข้อความที่จะแสดง (e.RowIndex เริ่มจาก 0 เราจึงต้อง +1)
            string rowNumber = (e.RowIndex + 1).ToString();

            // 2. สร้าง "เครื่องมือจัดตำแหน่ง" เพื่อให้ตัวเลขอยู่กึ่งกลาง
            StringFormat centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            // 3. กำหนดพื้นที่ที่จะวาดตัวเลข (คือพื้นที่ทั้งหมดของหัวแถว)
            Rectangle headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, this.dgvCart.RowHeadersWidth, e.RowBounds.Height);

            // 4. สั่ง "วาด" ข้อความลงไปบนหัวแถว
            e.Graphics.DrawString(
                rowNumber,                  // ข้อความที่จะวาด
                this.Font,                  // ใช้ฟอนต์เดียวกันกับฟอร์ม
                SystemBrushes.ControlText,  // ใช้สีข้อความมาตรฐาน
                headerBounds,               // วาดลงในพื้นที่ที่กำหนด
                centerFormat                // จัดตำแหน่งตามที่ตั้งค่าไว้
            );
        }

        private void btnSwitchUser_Click(object sender, EventArgs e)
        {
            this.Hide();

            FormRoleSelection roleSelectionForm = new FormRoleSelection();
            roleSelectionForm.ShowDialog();

            if (roleSelectionForm.LoginCancelled)
            {
                this.Close();
                return;
            }

            // ก่อนจะแสดงฟอร์มอีกครั้ง ให้รีเซ็ตข้อมูลเก่าทิ้งก่อน
            ResetOrderForm();
            if (roleSelectionForm.IsAdminRole)
            {
                // ถ้าเป็น Admin ก็ไม่ต้องทำอะไรเพิ่ม
            }
            else
            {
                // ถ้าเป็นลูกค้า ให้กรอกชื่อ
                txtCustomerName.Text = roleSelectionForm.LoggedInCustomerName;
                txtCustomerName.ReadOnly = true;
            }

            this.Show();
        }
    }   
}

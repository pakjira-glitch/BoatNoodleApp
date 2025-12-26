using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices.ComTypes;

namespace BoatNoodleApp
{
    public partial class FormAdminDashboard : Form
    {
        private readonly string _tableConfigPath = Path.Combine(Application.StartupPath, "table_config.txt");
        // วาง connection string ไว้ตรงนี้เพื่อให้ใช้ได้ทั้งฟอร์ม
        private string connectionString = "server=localhost;database=user;uid=root;password=;";

        private string _newImagePath = string.Empty; // เก็บ Path รูปใหม่ที่เลือก
        public bool MenusWereUpdated { get; private set; } = false; // Flag สำหรับบอก Form1

        public FormAdminDashboard()
        {
            InitializeComponent();
            LoadAllMenusForEditing(); // <<-- เพิ่มบรรทัดนี้
        }
        private void LoadOrderHistory(DateTime startDate, DateTime endDate, string customerNameToSearch)
        {
            try
            {
                DataTable dt = new DataTable();
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    // --- ส่วนที่แก้ไขสำคัญ: สร้าง Query แบบไดนามิก ---

                    // 1. เริ่มต้น Query ด้วยการกรองวันที่เสมอ
                    string query = "SELECT `ลำดับ`, `ชื่อลูกค้า`, `หมายเลขโต๊ะ`, `วันที่`, `ยอดรวม`, `สลิป` FROM orders " +
                                   "WHERE `วันที่` BETWEEN @StartDate AND @EndDate ";

                    // 2. ตรวจสอบว่าผู้ใช้ "ได้กรอก" ชื่อลูกค้าในช่องค้นหาหรือไม่
                    if (!string.IsNullOrWhiteSpace(customerNameToSearch))
                    {
                        // 3. ถ้ากรอก, ให้ "ต่อท้าย" Query ด้วยเงื่อนไขการกรองชื่อ
                        //    เราใช้ LIKE '%...%' เพื่อให้สามารถค้นหาแค่บางส่วนของชื่อได้
                        query += "AND `ชื่อลูกค้า` LIKE @CustomerName ";
                    }

                    // 4. ต่อท้ายด้วยการเรียงลำดับ
                    query += "ORDER BY `วันที่` DESC";

                    // --- จบส่วนแก้ไข Query ---

                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        // 5. เพิ่ม Parameters ทั้งหมด
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);

                        // 6. เพิ่ม Parameter ของชื่อลูกค้า "เฉพาะในกรณีที่มีการค้นหา"
                        if (!string.IsNullOrWhiteSpace(customerNameToSearch))
                        {
                            cmd.Parameters.AddWithValue("@CustomerName", "%" + customerNameToSearch + "%");
                        }

                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }

                dgvOrders.DataSource = dt;
                CalculateTotalRevenue(dt);

                // (ทางเลือก) แสดงข้อความบอกว่าเจอข้อมูลกี่รายการ
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("ไม่พบข้อมูลที่ตรงกับเงื่อนไขการค้นหา", "ไม่พบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการค้นหาข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            // ตรวจสอบก่อนว่ามีแถวที่ถูกเลือกจริงๆ หรือไม่ (ป้องกัน Error)
            if (dgvOrders.SelectedRows.Count > 0)
            {
                // ดึงข้อมูลจากแถวที่ผู้ใช้กำลังเลือกอยู่
                DataGridViewRow selectedRow = dgvOrders.SelectedRows[0];

                // --- แสดงข้อมูลพื้นฐานใน TextBoxes ---
                int orderId = Convert.ToInt32(selectedRow.Cells["ลำดับ"].Value);
                txtCustomerName.Text = selectedRow.Cells["ชื่อลูกค้า"].Value.ToString();
                txtTableNumber.Text = selectedRow.Cells["หมายเลขโต๊ะ"].Value.ToString();

                // แปลงค่าเงินให้มีทศนิยม 2 ตำแหน่งเสมอ
                txtTotalAmount.Text = Convert.ToDecimal(selectedRow.Cells["ยอดรวม"].Value).ToString("#,##0.00");

                // แปลงค่าวันที่และเวลา แล้วแยกแสดงผล
                DateTime orderDateTime = Convert.ToDateTime(selectedRow.Cells["วันที่"].Value);
                txtOrderDate.Text = orderDateTime.ToString("dd MMMM yyyy"); // รูปแบบ: 08 October 2025
                txtOrderTime.Text = orderDateTime.ToString("HH:mm:ss");   // รูปแบบ: 14:50:04

                // --- แสดงรูปภาพสลิป ---
                string slipPath = selectedRow.Cells["สลิป"].Value.ToString();
                if (!string.IsNullOrEmpty(slipPath) && File.Exists(slipPath))
                {
                    pbSlipImage.ImageLocation = slipPath;
                }
                else
                {
                    pbSlipImage.Image = null;
                }

                // --- โหลดและแสดงรายการอาหารของออเดอร์นี้ --
                LoadOrderItems(orderId);
            }
        }
        private void LoadOrderItems(int orderId)
        {
            txtOrderItems.Clear();
            StringBuilder itemsBuilder = new StringBuilder();
            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM order_items WHERE OrderID = @OrderID";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@OrderID", orderId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string itemName = reader["MenuName"].ToString();
                                string noodle = reader["NoodleType"].ToString();
                                string spicy = reader["SpicinessLevel"].ToString();
                                string portion = reader["PortionType"].ToString();
                                int qty = Convert.ToInt32(reader["Quantity"]);
                                decimal subtotal = Convert.ToDecimal(reader["Subtotal"]);

                                itemsBuilder.AppendLine($"▶ {itemName} ({portion})");
                                itemsBuilder.AppendLine($"   เส้น: {noodle}, ความเผ็ด: {spicy}");
                                itemsBuilder.AppendLine($"   จำนวน: {qty}  ราคารวม: {subtotal:N2} บาท");
                                itemsBuilder.AppendLine("------------------------------");
                            }
                        }
                    }
                }
                txtOrderItems.Text = itemsBuilder.ToString();
            }
            catch (Exception ex)
            {
                txtOrderItems.Text = "เกิดข้อผิดพลาดในการโหลดรายการอาหาร:\n" + ex.Message;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 1. ดึงค่าวันที่เริ่มต้นและสิ้นสุด (เหมือนเดิม)
            DateTime startDate = dtpStartDate.Value.Date;
            DateTime endDate = dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1);

            // 2. "เพิ่ม" การดึงข้อความค้นหาจาก TextBox
            string customerNameSearch = txtSearchCustomer.Text;

            // 3. เรียกใช้เมธอด LoadOrderHistory โดยส่ง "ชื่อลูกค้า" ไปด้วย
            LoadOrderHistory(startDate, endDate, customerNameSearch);
        }

        private void CalculateTotalRevenue(DataTable dt)
        {
            decimal total = 0;
            foreach (DataRow row in dt.Rows)
            {
                total += Convert.ToDecimal(row["ยอดรวม"]);
            }
            lblTotalRevenue.Text = $"ยอดรวม: {total:#,##0.00} บาท";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // ตั้งค่าวันที่กลับไปเป็นค่าปัจจุบัน
            dtpStartDate.Value = DateTime.Now;
            dtpEndDate.Value = DateTime.Now;
            txtSearchCustomer.Clear();

            // ล้างข้อมูลในตารางทิ้ง
            dgvOrders.DataSource = null;

            // ล้างข้อมูลในกล่องข้อความรายละเอียด
            ClearDetails();
        }

        private void ClearDetails()
        {
            txtCustomerName.Clear();
            txtTableNumber.Clear();
            txtTotalAmount.Clear();
            txtOrderDate.Clear();
            txtOrderTime.Clear();
            txtOrderItems.Clear();
            pbSlipImage.Image = null;
            lblTotalRevenue.Text = "ยอดรวม: 0.00 บาท";
        }

        private void btnSearchSummary_Click(object sender, EventArgs e)
        {
            DateTime startDate = dtpSummaryStartDate.Value.Date;
            DateTime endDate = dtpSummaryEndDate.Value.Date.AddDays(1).AddSeconds(-1);
            LoadSalesSummary(startDate, endDate);

            // เรียกใช้ CalculateGrandTotal เวอร์ชันใหม่
            CalculateGrandTotal(startDate, endDate);
        }

        private void LoadSalesSummary(DateTime startDate, DateTime endDate)
        {
            try
            {
                DataTable dt = new DataTable();
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    // นี่คือ SQL Query ที่ใช้ JOIN สองตาราง, GROUP BY, และ SUM ข้อมูล
                    string query = @"
                     SELECT 
                        oi.MenuName AS 'เมนู',
                        oi.NoodleType AS 'ประเภทเส้น',
                        AVG(oi.UnitPrice) AS 'ราคาเฉลี่ยต่อหน่วย',
                        SUM(oi.Quantity) AS 'จำนวนที่ขายได้',
                        SUM(oi.Subtotal) AS 'ราคารวม'
                    FROM order_items oi
                    INNER JOIN orders o ON oi.OrderID = o.ลำดับ
                    WHERE o.วันที่ BETWEEN @StartDate AND @EndDate
                    GROUP BY oi.MenuName, oi.NoodleType
                    ORDER BY SUM(oi.Quantity) DESC, oi.MenuName;";

                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }

                dgvSalesSummary.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการสรุปยอดขาย: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateGrandTotal(DateTime startDate, DateTime endDate)
        {
            decimal grandTotal = 0;
            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();
                    // Query ใหม่นี้จะหาผลรวมของ total_amount ทั้งหมดในช่วงวันที่ที่เลือก
                    string query = "SELECT SUM(`ยอดรวม`) FROM orders WHERE `วันที่` BETWEEN @StartDate AND @EndDate";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);

                        // ใช้ ExecuteScalar เพราะเราต้องการแค่ค่าเดียวกลับมา
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            grandTotal = Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการคำนวณยอดรวมทั้งหมด: " + ex.Message);
            }

            lblGrandTotal.Text = $"ยอดรวมทั้งหมด: {grandTotal:N2} บาท";
        }

        private void btnResetSummary_Click(object sender, EventArgs e)
        {
            DateTime startDate = dtpSummaryStartDate.Value.Date;
            DateTime endDate = dtpSummaryEndDate.Value.Date.AddDays(1).AddSeconds(-1);

            LoadSalesSummary(startDate, endDate);

            CalculateGrandTotal(startDate, endDate); // <--- เพิ่มบรรทัดนี้เข้าไป
        }

        private void LoadAllMenusForEditing()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string query = "SELECT MenuID, MenuName FROM menus ORDER BY MenuName";
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                da.Fill(dt);
            }
            cbxEditMenuSelect.DataSource = dt;
            cbxEditMenuSelect.DisplayMember = "MenuName";
            cbxEditMenuSelect.ValueMember = "MenuID";
        }

        private void cbxEditMenuSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxEditMenuSelect.SelectedValue == null) return;

            DataRowView drv = (DataRowView)cbxEditMenuSelect.SelectedItem;
            int selectedMenuId = Convert.ToInt32(drv["MenuID"]);

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM menus WHERE MenuID = @MenuID";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@MenuID", selectedMenuId);
                con.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtEditMenuName.Text = reader["MenuName"].ToString();
                        nudEditPrice.Value = Convert.ToDecimal(reader["Price"]);

                        string imagePath = reader["ImagePath"].ToString();
                        if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                        {
                            pbEditMenuImage.ImageLocation = imagePath;
                        }
                        else
                        {
                            pbEditMenuImage.Image = null;
                        }
                    }
                }
            }
            _newImagePath = string.Empty; // รีเซ็ต Path รูปใหม่ทุกครั้งที่เลือกเมนู
        }

        private void btnChangeImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // แสดงรูปที่เลือกใน PictureBox ทันที
                pbEditMenuImage.ImageLocation = openFileDialog.FileName;
                _newImagePath = openFileDialog.FileName; // เก็บ Path ของรูปใหม่ไว้
            }
        }

        private void btnUpdateMenu_Click(object sender, EventArgs e)
        {
            if (cbxEditMenuSelect.SelectedValue == null)
            {
                MessageBox.Show("กรุณาเลือกเมนูที่จะแก้ไขก่อน", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedMenuId = Convert.ToInt32(cbxEditMenuSelect.SelectedValue);
            string newMenuName = txtEditMenuName.Text;
            decimal newPrice = nudEditPrice.Value;

            // เราจะคัดลอกไฟล์รูปใหม่ไปยังโฟลเดอร์ของโปรแกรม
            // เพื่อให้ Path ที่เก็บในฐานข้อมูลเป็น Path ที่ถูกต้อง
            string finalImagePath = pbEditMenuImage.ImageLocation; // ใช้ Path เดิมเป็นค่าเริ่มต้น
            if (!string.IsNullOrEmpty(_newImagePath))
            {
                string imageFolder = Path.Combine(Application.StartupPath, "images"); // สมมติว่าเก็บรูปในโฟลเดอร์ images
                if (!Directory.Exists(imageFolder)) Directory.CreateDirectory(imageFolder);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(_newImagePath);
                finalImagePath = Path.Combine(imageFolder, fileName);
                File.Copy(_newImagePath, finalImagePath, true);
            }

            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    string query = "UPDATE menus SET MenuName = @MenuName, Price = @Price, ImagePath = @ImagePath WHERE MenuID = @MenuID";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@MenuID", selectedMenuId);
                    cmd.Parameters.AddWithValue("@MenuName", newMenuName);
                    cmd.Parameters.AddWithValue("@Price", newPrice);
                    cmd.Parameters.AddWithValue("@ImagePath", finalImagePath);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("อัปเดตข้อมูลเมนูเรียบร้อยแล้ว!", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.MenusWereUpdated = true; // ตั้ง Flag ว่ามีการอัปเดต
                    LoadAllMenusForEditing(); // โหลดข้อมูลใน ComboBox ใหม่
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการอัปเดตข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteMenu_Click(object sender, EventArgs e)
        {
            if (cbxEditMenuSelect.SelectedValue == null)
            {
                MessageBox.Show("กรุณาเลือกเมนูที่จะลบก่อน", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedMenuName = cbxEditMenuSelect.Text;
            // ถามเพื่อยืนยันการลบ
            DialogResult confirm = MessageBox.Show($"คุณแน่ใจหรือไม่ว่าต้องการลบเมนู '{selectedMenuName}'?\nการกระทำนี้ไม่สามารถย้อนกลับได้",
                                                 "ยืนยันการลบ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                int selectedMenuId = Convert.ToInt32(cbxEditMenuSelect.SelectedValue);
                try
                {
                    using (MySqlConnection con = new MySqlConnection(connectionString))
                    {
                        string query = "DELETE FROM menus WHERE MenuID = @MenuID";
                        MySqlCommand cmd = new MySqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@MenuID", selectedMenuId);

                        con.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("ลบเมนูเรียบร้อยแล้ว!", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.MenusWereUpdated = true; // ตั้ง Flag ว่ามีการอัปเดต
                        LoadAllMenusForEditing(); // โหลดข้อมูลใน ComboBox ใหม่
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการลบข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAddNewMenu_Click(object sender, EventArgs e)
        {
            FormAddMenu addMenuForm = new FormAddMenu();
            addMenuForm.ShowDialog();

            // ตรวจสอบว่ามีการเพิ่มเมนูสำเร็จหรือไม่
            if (addMenuForm.MenuAdded)
            {
                // *** ตรวจสอบว่ามีบรรทัดนี้อยู่ ***
                this.MenusWereUpdated = true; // ตั้ง Flag บอก FormAdminLogin และ Form1

                // รีเฟรช ComboBox ในหน้าแก้ไขเมนู
                LoadAllMenusForEditing();
            }
        }

        private void btnBack_C_Click(object sender, EventArgs e)
        {
            // ทำหน้าที่แค่ปิดตัวเอง เพื่อกลับไปหา FormAdminLogin ที่เปิดมันขึ้นมา
            this.Close();
        }

        private void FormAdminDashboard_Load(object sender, EventArgs e)
        {
            LoadTableCount();
        }

        private void LoadTableCount()
        {
            try
            {
                int tableCount = 10; // ค่าเริ่มต้นถ้ายังไม่มีไฟล์
                if (File.Exists(_tableConfigPath))
                {
                    string countStr = File.ReadAllText(_tableConfigPath);
                    if (int.TryParse(countStr, out int savedCount) && savedCount > 0)
                    {
                        tableCount = savedCount;
                    }
                }
                lblCurrentTableCount.Text = $"จำนวนโต๊ะปัจจุบัน: {tableCount} โต๊ะ";
                txtNewTableCount.Text = tableCount.ToString(); // แก้จาก .Value เป็น .Text
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการโหลดจำนวนโต๊ะ: " + ex.Message);
                lblCurrentTableCount.Text = "จำนวนโต๊ะปัจจุบัน: ไม่สามารถโหลดได้";
            }
        }

        private void SaveTableCount()
        {
            // แปลงข้อความใน TextBox เป็นตัวเลข, ตรวจสอบว่าไม่เป็น 0 และไม่เป็นค่าว่าง
            if (int.TryParse(txtNewTableCount.Text, out int newCount) && newCount > 0)
            {
                try
                {
                    File.WriteAllText(_tableConfigPath, newCount.ToString());
                    MessageBox.Show("บันทึกจำนวนโต๊ะเรียบร้อยแล้ว!", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTableCount(); // โหลดข้อมูลมาแสดงใหม่

                    // ตั้ง Flag เพื่อบอก Form1 ว่าต้องโหลดจำนวนโต๊ะใหม่
                    // เราใช้ Flag เดิมกับตอนแก้ไขเมนูได้เลย
                    this.MenusWereUpdated = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการบันทึกไฟล์: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("กรุณากรอกจำนวนโต๊ะที่ถูกต้อง (ต้องมากกว่า 0 และไม่เกิน 99)", "ข้อมูลผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtEditMenuName_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnSaveTableCount_Click(object sender, EventArgs e)
        {
            SaveTableCount();
        }

        private void txtNewTableCount_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtNewTableCount_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewTableCount.Text))
            {
                return;
            }

            string filteredText = "";
            foreach (char c in txtNewTableCount.Text)
            {
                if (char.IsDigit(c))
                {
                    filteredText += c;
                }
            }

            if (txtNewTableCount.Text != filteredText)
            {
                txtNewTableCount.Text = filteredText;
                txtNewTableCount.SelectionStart = txtNewTableCount.Text.Length;
            }
        }
    }
}

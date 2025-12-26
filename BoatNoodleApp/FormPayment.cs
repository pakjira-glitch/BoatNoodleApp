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
    public partial class FormPayment : Form
    {
            private string _connectionString;
            private string _customerName;
            private int _tableNumber;
            private DataTable _cartTable;
            private decimal _grandTotal;
            private string _uploadedSlipPath; // เก็บ path ของสลิปที่อัปโหลด

            public bool OrderConfirmed { get; private set; } = false; // Property เพื่อบอกว่ามีการยืนยันการสั่งซื้อหรือไม่

        public FormPayment(string connectionString, string customerName, int tableNumber, DataTable cartTable, decimal grandTotal)
        {
            InitializeComponent();
            _connectionString = connectionString;
            _customerName = customerName;
            _tableNumber = tableNumber;
            _cartTable = cartTable;
            _grandTotal = grandTotal; // รับค่ามาเก็บไว้
            _uploadedSlipPath = string.Empty;

            InitializePaymentForm();
        }
        

        private void InitializePaymentForm()
        {
            lblCustomerInfo.Text = $" ลูกค้า : {_customerName}    โต๊ะ : {_tableNumber}";

            dgvPaymentItems.DataSource = _cartTable;
            // กำหนดชื่อ Header ของ DataGridView
            dgvPaymentItems.Columns["MenuID"].Visible = false;
            dgvPaymentItems.Columns["MenuName"].HeaderText = "เมนู";
            dgvPaymentItems.Columns["NoodleType"].HeaderText = "เส้น";
            dgvPaymentItems.Columns["SpicinessLevel"].HeaderText = "ความเผ็ด";
            dgvPaymentItems.Columns["PortionType"].HeaderText = "ขนาด";
            dgvPaymentItems.Columns["Quantity"].HeaderText = "จำนวน";
            dgvPaymentItems.Columns["UnitPrice"].HeaderText = "ราคา/หน่วย";
            dgvPaymentItems.Columns["Subtotal"].HeaderText = "ราคารวม";

            lblTotalAmount.Text = $"ยอดรวมทั้งหมด: {_grandTotal:N2} บาท";
            pbQRCode.SizeMode = PictureBoxSizeMode.Zoom;
            lblPaymentDateTime.Text = "เวลาชำระเงิน: N/A"; // ตั้งค่าเริ่มต้น
        }


        private void FormPayment_Load(object sender, EventArgs e)
        {

        }

        private void btnUploadSlip_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // บันทึกไฟล์สลิปไปยังโฟลเดอร์ที่เรากำหนด (เช่น 'Slips' ในโฟลเดอร์ของโปรแกรม)
                string slipsFolder = Path.Combine(Application.StartupPath, "Slips");
                if (!Directory.Exists(slipsFolder))
                {
                    Directory.CreateDirectory(slipsFolder);
                }
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(openFileDialog.FileName); // สร้างชื่อไฟล์ที่ไม่ซ้ำกัน
                string destPath = Path.Combine(slipsFolder, fileName);
                File.Copy(openFileDialog.FileName, destPath, true);

                _uploadedSlipPath = destPath;
                txtSlipPath.Text = _uploadedSlipPath; // แสดง path ใน TextBox (สามารถซ่อนได้)
                MessageBox.Show("อัปโหลดสลิปเรียบร้อยแล้ว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnConfirmPayment_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่ามีการอัปโหลดสลิปแล้วหรือยัง
            if (string.IsNullOrEmpty(_uploadedSlipPath) || !File.Exists(_uploadedSlipPath))
            {
                MessageBox.Show("กรุณาอัปโหลดรูปสลิปก่อนยืนยันการชำระเงิน", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                long orderId; // ประกาศ orderId ไว้นอก using block เพื่อให้ใช้ได้ตอนเปิดใบเสร็จ

                using (MySqlConnection con = new MySqlConnection(_connectionString))
                {
                    con.Open();

                    // 1. บันทึกข้อมูลการสั่งซื้อหลักลงในตาราง orders
                    string insertOrderQuery = "INSERT INTO orders (`ชื่อลูกค้า`, `หมายเลขโต๊ะ`, `ยอดรวม`, `สลิป`, `payment_status`, `วันที่`) " +
                                              "VALUES (@CustomerName, @TableNumber, @TotalAmount, @ReceiptImagePath, @PaymentStatus, @OrderDate); " +
                                              "SELECT LAST_INSERT_ID();";

                    using (MySqlCommand cmd = new MySqlCommand(insertOrderQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@CustomerName", _customerName);
                        cmd.Parameters.AddWithValue("@TableNumber", _tableNumber);
                        cmd.Parameters.AddWithValue("@TotalAmount", _grandTotal); // ใช้ _grandTotal ที่รวม VAT แล้ว
                        cmd.Parameters.AddWithValue("@ReceiptImagePath", _uploadedSlipPath);
                        cmd.Parameters.AddWithValue("@PaymentStatus", "Paid");
                        cmd.Parameters.AddWithValue("@OrderDate", DateTime.Now);

                        // รับ OrderID ล่าสุดที่เพิ่งสร้างกลับมา
                        orderId = Convert.ToInt64(cmd.ExecuteScalar());
                    }

                    // 2. บันทึกรายการสินค้าแต่ละชิ้นลงในตาราง order_items
                    string insertOrderItemQuery = "INSERT INTO order_items (OrderID, MenuName, NoodleType, SpicinessLevel, PortionType, Quantity, UnitPrice, Subtotal) " +
                                                  "VALUES (@OrderID, @MenuName, @NoodleType, @SpicinessLevel, @PortionType, @Quantity, @UnitPrice, @Subtotal)";

                    foreach (DataRow row in _cartTable.Rows)
                    {
                        using (MySqlCommand cmdItem = new MySqlCommand(insertOrderItemQuery, con))
                        {
                            cmdItem.Parameters.AddWithValue("@OrderID", orderId);
                            cmdItem.Parameters.AddWithValue("@MenuName", row["MenuName"].ToString());
                            cmdItem.Parameters.AddWithValue("@NoodleType", row["NoodleType"].ToString());
                            cmdItem.Parameters.AddWithValue("@SpicinessLevel", row["SpicinessLevel"].ToString());
                            cmdItem.Parameters.AddWithValue("@PortionType", row["PortionType"].ToString());
                            cmdItem.Parameters.AddWithValue("@Quantity", Convert.ToInt32(row["Quantity"]));
                            cmdItem.Parameters.AddWithValue("@UnitPrice", Convert.ToDecimal(row["UnitPrice"]));
                            cmdItem.Parameters.AddWithValue("@Subtotal", Convert.ToDecimal(row["Subtotal"]));
                            cmdItem.ExecuteNonQuery();
                        }
                    }
                } // สิ้นสุดการเชื่อมต่อฐานข้อมูล

                // --- ส่วนที่แก้ไข: การแสดงใบเสร็จ ---

                // แสดงข้อความยืนยันว่าบันทึกสำเร็จ
                MessageBox.Show("บันทึกการสั่งซื้อและสลิปเรียบร้อยแล้ว!", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ตั้งค่าสถานะเพื่อบอก Form1 ว่าการสั่งซื้อสำเร็จ
                OrderConfirmed = true;

                // ซ่อนหน้าชำระเงินปัจจุบันไปก่อน (เพื่อไม่ให้หน้าจอกระพริบ)
                this.Hide();

                // สร้างและเปิดหน้าใบเสร็จ โดยส่งข้อมูลที่จำเป็นไปด้วย
                FormReceipt receiptForm = new FormReceipt(orderId, _customerName, _tableNumber, _cartTable, _grandTotal);

                // *** จุดที่แก้ไขสำคัญ: สั่งให้หน้าใบเสร็จแสดงขึ้นมา ***
                // โปรแกรมจะหยุดรอที่บรรทัดนี้จนกว่าผู้ใช้จะปิดหน้าใบเสร็จ
                receiptForm.ShowDialog();

                // หลังจากที่หน้าใบเสร็จถูกปิดแล้ว ค่อยสั่งปิดหน้าชำระเงินนี้ตามไป
                this.Close();
            }
            catch (Exception ex)
            {
                // ในกรณีที่เกิดข้อผิดพลาด ให้แสดง Error message
                MessageBox.Show("เกิดข้อผิดพลาดในการบันทึกข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    
}

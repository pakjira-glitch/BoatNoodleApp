using System.Data;
using System.Drawing.Printing;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System;

namespace BoatNoodleApp
{
    public partial class FormReceipt : Form
    {
        // --- ตัวแปรสำหรับรับข้อมูลจาก FormPayment ---
        private long _orderId;
        private string _customerName;
        private int _tableNumber;
        private DataTable _cartTable;
        private decimal _grandTotal;

        // --- Constructor ---
        public FormReceipt(long orderId, string customerName, int tableNumber, DataTable cartTable, decimal grandTotal)
        {
            InitializeComponent();

            // รับค่ามาเก็บไว้ในตัวแปรของฟอร์มนี้
            _orderId = orderId;
            _customerName = customerName;
            _tableNumber = tableNumber;
            _cartTable = cartTable;
            _grandTotal = grandTotal;
        }

        // --- Event ที่ทำงานเมื่อฟอร์มโหลด ---
        private void FormReceipt_Load(object sender, EventArgs e)
        {
            // สร้างข้อความใบเสร็จแล้วนำไปใส่ใน TextBox
            txtReceipt.Text = GenerateReceiptText();
        }

        // --- Event ที่ทำงานหลังจากฟอร์มแสดงผลแล้ว (ใช้แก้ปัญหาแถบสีน้ำเงิน) ---
        private void FormReceipt_Shown(object sender, EventArgs e)
        {
            txtReceipt.Select(0, 0);
            txtReceipt.ScrollToCaret();
            btnClose.Focus();
        }

        // --- เมธอดหลักในการสร้างข้อความใบเสร็จ (ฉบับสมบูรณ์ล่าสุด) ---
        private string GenerateReceiptText()
        {
            StringBuilder sb = new StringBuilder();
            const int receiptWidth = 42;

            // --- ส่วนหัว (ไม่มีการเปลี่ยนแปลง) ---
            sb.AppendLine(CenterText("ร้านก๋วยเตี๋ยวเรือมอขอ", receiptWidth));
            sb.AppendLine(CenterText("123 หมู่ 16 ถนนมิตรภาพ ตำบลในเมือง อำเภอเมือง", receiptWidth));
            sb.AppendLine(CenterText("ขอนแก่น 40002", receiptWidth));
            sb.AppendLine(CenterText("เบอร์โทร: 000-888-999", receiptWidth));
            sb.AppendLine(new string('-', receiptWidth));
            sb.AppendLine(CenterText($"ลูกค้า: {_customerName}   โต๊ะ: {_tableNumber}", receiptWidth));
            sb.AppendLine(CenterText($"เลขที่ออเดอร์: {_orderId}", receiptWidth));
            sb.AppendLine(new string('-', receiptWidth));
            sb.AppendLine();
            sb.AppendLine(CenterText("*** ใบเสร็จอย่างย่อ ***", receiptWidth));
            sb.AppendLine();

            // --- ส่วนหัวตารางและรายการอาหาร (ใช้ string.Format) ---
            int nameWidth = 20;
            int qtyWidth = 5;
            int priceWidth = 8;
            int totalWidth = 8;
            string itemFormat = "{0,-" + nameWidth + "}{1," + qtyWidth + "}{2," + priceWidth + ":N2}{3," + totalWidth + ":N2}";

            sb.AppendLine(string.Format(itemFormat, "รายการ", "จำนวน", "ราคา", "รวม"));
            sb.AppendLine(new string('-', receiptWidth));

            foreach (DataRow row in _cartTable.Rows)
            {
                // ... (โค้ดแสดงรายการอาหารเหมือนเดิม) ...
                string menuName = row["MenuName"].ToString();
                int quantity = Convert.ToInt32(row["Quantity"]);
                decimal unitPrice = Convert.ToDecimal(row["UnitPrice"]);
                decimal subtotal = Convert.ToDecimal(row["Subtotal"]);
                string displayName = (menuName.Length > nameWidth) ? menuName.Substring(0, nameWidth) : menuName;
                sb.AppendLine(string.Format(itemFormat, displayName, quantity, unitPrice, subtotal));
            }

            // --- ส่วนสรุปท้ายใบเสร็จ (แก้ไขใหม่ทั้งหมด) ---
            decimal subTotalBeforeVat = _grandTotal / 1.07m;
            decimal vatAmount = _grandTotal - subTotalBeforeVat;

            sb.AppendLine(new string('-', receiptWidth));

            // *** จุดแก้ไขสำคัญ: พิมพ์รูปแบบลงไปตรงๆ และใช้ PadRight ***
            sb.AppendLine($"ยอดรวม ---------------------- {subTotalBeforeVat.ToString("N2").PadLeft(8)}");
            sb.AppendLine($"ภาษีมูลค่าเพิ่ม (7%) ------------ {vatAmount.ToString("N2").PadLeft(8)}");

            sb.AppendLine(new string('=', receiptWidth));
            sb.AppendLine($"ยอดสุทธิ ====================== {_grandTotal.ToString("N2").PadLeft(8)}");
            sb.AppendLine(new string('=', receiptWidth));

            // --- ส่วนท้ายใบเสร็จ (ไม่มีการเปลี่ยนแปลง) ---
            sb.AppendLine();
            sb.AppendLine(CenterText($"พนักงาน: Admin", receiptWidth));
            sb.AppendLine(CenterText($"วันที่: {DateTime.Now:dd-MM-yyyy HH:mm:ss}", receiptWidth));
            sb.AppendLine();
            sb.AppendLine(CenterText("ขอบคุณที่ใช้บริการครับ/ค่ะ", receiptWidth));

            return sb.ToString();
        }        

        // --- ฟังก์ชันตัวช่วยสำหรับจัดข้อความกึ่งกลาง ---
        private string CenterText(string text, int totalWidth)
        {
            if (string.IsNullOrEmpty(text))
                return new string(' ', totalWidth);

            int spaces = totalWidth - text.Length;
            int padLeft = spaces / 2 + text.Length;
            return text.PadLeft(padLeft).PadRight(totalWidth);
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // --- Event Click ของปุ่ม "พิมพ์" ---
        private void btnPrint_Click_1(object sender, EventArgs e)
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);

            PrintPreviewDialog printPreview = new PrintPreviewDialog();
            printPreview.Document = printDoc;
            ((Form)printPreview).WindowState = FormWindowState.Maximized;
            printPreview.ShowDialog();
        }

        // --- เมธอดที่ใช้ "วาด" ใบเสร็จลงบนหน้ากระดาษ (เวอร์ชันล่าสุด) ---
        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            string[] lines = txtReceipt.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            Font printFont = new Font("Consolas", 18); // <-- สามารถปรับขนาดฟอนต์ตอนพิมพ์ได้ที่นี่
            float yPos = e.MarginBounds.Top;
            float leftMargin = e.MarginBounds.Left;

            foreach (string line in lines)
            {
                e.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos);
                yPos += printFont.GetHeight(e.Graphics);
            }

            printFont.Dispose();
        }

        // --- Event ว่างๆ (อาจถูกสร้างขึ้นโดยอัตโนมัติ) ---
        private void txtReceipt_TextChanged(object sender, EventArgs e)
        {
            // ไม่ต้องทำอะไรในนี้
        }
    }
}


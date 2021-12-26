using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GiaoDienBaiTap
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        OpenFileDialog ofd = new OpenFileDialog();
        private bool ktra = false;
        private int p = 1;
        private int q = 1;
        private int N = 1;
        private int n = 1;
        private int khoabimat = 1;
        private int da = 1;
        private string str = "";
        private string str1 = "";
        private string str3 = "";
        private string str12 = "";
        private int s1 = 0;
        private int s2 = 0;
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox4.Text = "";
            bool kt = false;
            Random ran = new Random();
            while (kt != true)
            {
                p = ran.Next(2000, 10000);
                q = ran.Next(2000, 10000);
                kt = true;
                for (int i = 2; i <= Math.Sqrt(p); i++)
                {
                    if (p % i == 0)
                    {
                        kt = false;
                        break;
                    }
                }
                for (int i = 2; i <= Math.Sqrt(q); i++)
                {
                    if (q % i == 0)
                    {
                        kt = false;
                        break;
                    }
                }
            }
            textBox1.Text = p.ToString();
            textBox2.Text = q.ToString();
            button2.Focus();
        }
        private int SinhKhoa(int ea, int m) // ea là eA , m là giá trị modulo
        {
            int m2 = m;
            int xa = 1;
            int xm = 0;
            while (m != 0)
            {
                int k = (ea / m);
                int xr = xa - k * xm;
                xa = xm;
                xm = xr;
                int r = (ea % m);
                ea = m;
                m = r;
            }
            if (xa < 0) { return m2 + xa; }
            else return xa;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ktra = false;
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            Random ran = new Random();
            n = p * q;
            N = (p - 1) * (q - 1);
            khoabimat = 1;
            int a = 1;
            while (a < N)
            {
                int uscln = 1;
                a = ran.Next(1, N);
                for (int i = 2; i <= a; i++)
                {
                    if (a % i == 0 && N % i == 0)
                    {
                        uscln = i;
                        break;
                    }
                }
                if (uscln == 1)
                {
                    khoabimat = a;
                    break;
                }
            }
            textBox4.Text = khoabimat.ToString();
            da = SinhKhoa(khoabimat, N);
            textBox3.Text = da.ToString();
            textBox9.Focus();

        }
        private void button3_Click(object sender, EventArgs e)
        {
            textBox4.UseSystemPasswordChar = !(textBox4.UseSystemPasswordChar);
        }
        ///Đọc file 
        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chỉ sử dụng file text và chỉ chứa số dương! Có thể thay đổi nội dung file tại mục văn bản theo yêu cầu.", "Lưu ý");
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Chọn tệp";
            ofd.Filter = "Text (*txt)|*.txt";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                StreamReader read = new StreamReader(File.OpenRead(ofd.FileName));
                string sang = read.ReadToEnd();
                int i = 0;
                for (i = 0; i < sang.Length; i++)
                {
                    if (Char.IsLetter(sang[i]) == true || Char.IsSymbol(sang[i]) == true)
                    {
                        MessageBox.Show("Trong file bạn chọn có giá trị âm hoặc ký tự chữ, xin vui lòng chọn file khác", "Chú ý");
                        break;
                    }
                }
                if (i == sang.Length)
                {
                    textBox5.Text = ofd.FileName;
                    textBox9.Text = sang;
                }
                else
                    textBox9.Text = "";
                read.Dispose();
            }


        /// Mã hóa
        }
        private long mahoa(int n1, int d1)
        {
            long kq;
            if (d1 == 0) return 1 % n;
            else
                kq = mahoa(n1, d1 / 2);
            if (d1 % 2 == 0) return (kq * kq) % n;
            else
                return (((kq * kq) % n) * n1) % n;
        }
        ///     


        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox9.Text != "")
            {
                str = textBox9.Text;
                str1 = "";
                str = str.Trim();
                str = str + " ";
                string sang = "";
                string str2 = "";
                for (int i = 0; i < str.Length; i++)
                {
                    str2 = str2 + str[i];
                    if (str[i] == ' ' && str2 != "")
                    {
                        if (str2.Length > 9)
                        {
                            MessageBox.Show("Có giá trị lớn hơn 10000000 trong văn bản", "Lưu ý");
                            break;
                        }
                        int ss = Int32.Parse(str2);
                        if (ss > 10000000)
                        {
                            MessageBox.Show("Có giá trị lớn hơn 10000000 trong văn bản","Lưu ý");
                            break;
                        }
                        sang = sang + ss.ToString() + " ";
                        long sig = (mahoa(ss, khoabimat)) % n;
                        str1 = str1 + sig.ToString() + " ";
                        str2 = "";
                    }
                }
                str = str.Trim();
                textBox9.Text = sang.Trim();
                textBox6.Text = str1;
            }
            else
                MessageBox.Show("Bạn chưa có văn bản để ký!");
            button5.Focus();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "") { MessageBox.Show("Bạn chưa ký văn bản!", "Chú ý"); }
            else
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Text files (*.txt)|*.txt";
                if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    StreamWriter write = new StreamWriter(File.Create(save.FileName));
                    write.Write(str1);
                    write.Dispose();
                    MessageBox.Show("Đã lưu kết quả", "Thông báo");
                    ktra = true;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (ktra == false) { if (MessageBox.Show("Bạn có văn bản đã ký? Nếu chưa xin vui lòng khởi tạo văn bản cần mã hóa phía trên", "Lưu ý", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) { ktra = true; } else ktra = false; }
            if (ktra == true)
            {
                OpenFileDialog ofd1 = new OpenFileDialog();
                ofd1.Title = "Chọn tệp";
                ofd1.Filter = "Text (*txt)|*.txt";
                if (ofd1.ShowDialog() == DialogResult.OK)
                {
                    textBox7.Text = ofd1.FileName;
                    StreamReader read1 = new StreamReader(File.OpenRead(ofd1.FileName));
                    textBox10.Text = read1.ReadToEnd();
                    read1.Dispose();
                }
            }
            ktra = false;

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (textBox10.Text != "")
            {
                str12 = textBox10.Text;
                str12 = str12.Trim();
                str12 = str12 + " ";
                str3 = "";
                string str22 = "";
                for (int i = 0; i < str12.Length; i++)
                {
                    str22 = str22 + str12[i];
                    if (str12[i] == ' ' && str22 != "")
                    { 
                        int ss = Int32.Parse(str22);
                        long sig = (mahoa(ss, da)) % n;
                        str3 = str3 + sig.ToString() + " ";
                        str22 = "";
                    }
                }
                str12 = str12.Trim();
                textBox8.Text = str3.Trim();
                if (textBox8.Text == textBox9.Text) { MessageBox.Show("Trùng khớp với văn bản đã được ký số", "Thông báo"); } else
                    if (textBox8.Text != "") { MessageBox.Show("Đã mã hóa xin hãy so sánh với file bạn đã có", "Thông báo"); } else
                    MessageBox.Show("Không trùng khớp với văn bản đã được ký số", "Thông báo");
            }
            else MessageBox.Show("Bạn chưa có văn bản để xác thực", "Chú ý");
            button8.Focus();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox8.Text == "") { MessageBox.Show("Chưa xác thực văn bản", "Chú ý"); }
            else
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Text files (*.txt)|*.txt";
                if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    StreamWriter write = new StreamWriter(File.Create(save.FileName));
                    write.Write(str3);
                    write.Dispose();
                    MessageBox.Show("Đã lưu kết quả", "Thông báo");
                }
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc rằng mình đã hoàn tất?",
                      "Lưu ý",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox5.Text = "";
            if (!char.IsWhiteSpace(e.KeyChar) && !char.IsDigit(e.KeyChar)
                && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text != "")
            {
                button5.Enabled = true;
            }
            else button5.Enabled = false;

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (textBox8.Text != "")
            {
                button8.Enabled = true;
            }
            else button8.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                button2.Enabled = true;
            }
            else button2.Enabled = false;
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                button6.Enabled = true;
                textBox6.Text = "";
            }
            else button6.Enabled = false;
        }
        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (textBox10.Text != "")
            {
                button9.Enabled = true;
            }
            else button9.Enabled = false;
        }

        private void textBox9_Click(object sender, EventArgs e)
        {
            if (textBox9.Text == "")
            {
                MessageBox.Show("Văn này chỉ có thể đáp ứng các SỐ có giá trị <= 10000000, kiểu số 0xx sẽ bị bỏ số 0 đầu tiên", "Lưu ý");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {   textBox5.Enabled = true;
                textBox6.Enabled = true;
                textBox7.Enabled = true;
                textBox8.Enabled = true;
                textBox9.Enabled = true;
                textBox10.Enabled = true;
                button4.Enabled = true;
                button7.Enabled = true;
            }
            else
            {
                textBox5.Enabled = false;
                textBox6.Enabled = false;
                textBox7.Enabled = false;
                textBox8.Enabled = false;
                textBox9.Enabled = false;
                textBox10.Enabled = false;
                button4.Enabled = false;
                button7.Enabled = false;
            }
        }
    }
}

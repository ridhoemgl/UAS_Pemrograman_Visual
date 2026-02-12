using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace UAS_Pemrograman_Visual
{
    public partial class FormLogin : Form
    {
        private SqlCommand cmd;
        private DataSet ds;
        private SqlDataAdapter da;
        private SqlDataReader rd;

        Koneksi Konn = new Koneksi();

        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validasi Bila Username / Password Kosong
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "")
            {
                MessageBox.Show("Username atau Password Kosong!");
                return;
            }
            
            SqlConnection conn = Konn.GetConn();
            conn.Open();
            cmd = new SqlCommand("SELECT * FROM tbl_user WHERE username='" + textBox1.Text + "' AND password='" + textBox2.Text + "'", conn);
            rd = cmd.ExecuteReader();
            rd.Read();

            // Kondisi Apabila Username & Password Dimasukkan Benar
            if (rd.HasRows)
            {
                FormMainMenu fmm = new FormMainMenu();
                fmm.Show();
                this.Hide();
                conn.Close();
            }
            // Kondisi Apabila Username & Password Dimasukkan Salah
            else
            {
                MessageBox.Show("Username atau Password Salah!");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

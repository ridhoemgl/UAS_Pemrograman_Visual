using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UAS_Pemrograman_Visual
{
    public partial class FormMainMenu : Form
    {
        private SqlCommand cmd;
        private DataSet ds;
        private SqlDataAdapter da;
        private SqlDataReader rd;

        Koneksi Konn = new Koneksi();

        public FormMainMenu()
        {
            InitializeComponent();
        }

        void ShowAllData()
        {
            SqlConnection conn = Konn.GetConn();
            try
            {
                conn.Open();
                cmd = new SqlCommand("SELECT * FROM tbl_mhs", conn);
                ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds, "tbl_mhs");

                DataTable dt = ds.Tables["tbl_mhs"];

                foreach (DataRow row in dt.Rows)
                {
                    string nama = row["namaMahasiswa"].ToString();
                  

                    row["namaMahasiswa"] = char.ToUpper(nama[0]) + nama.Substring(1);
              
                }
                
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["namaMahasiswa"].HeaderText = "Nama";
                dataGridView1.Columns["nimMahasiswa"].HeaderText = "NIM";
                dataGridView1.Columns["jurusanMahasiswa"].HeaderText = "Jurusan";
                dataGridView1.Columns["nilaiMahasiswa"].HeaderText = "Nilai";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        void ClearAll()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "0";
        }

        void AddData()
        {
            int.TryParse(textBox4.Text, out int nilai);

            // Validasi Form
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || textBox4.Text.Trim() == "")
            {
                MessageBox.Show("Mohon Lengkapi Data!");
            }
            else if (nilai > 100 || nilai < 0)
            {
                MessageBox.Show("Nilai harus antara 0 dan 100!");
            }
            else
            {
                SqlConnection conn = Konn.GetConn();

                try
                {
                    cmd = new SqlCommand("INSERT INTO tbl_mhs VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "')", conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Add Data Berhasil!");
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        void UpdateData()
        {
            int.TryParse(textBox4.Text, out int nilai);

            // Validasi Form
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || textBox4.Text.Trim() == "")
            {
                MessageBox.Show("Mohon Lengkapi Data!");
            }
            else if (nilai > 100 || nilai < 0)
            {
                MessageBox.Show("Nilai harus antara 0 dan 100!");
            }
            else
            {
                SqlConnection conn = Konn.GetConn();
                try
                {
                    cmd = new SqlCommand("UPDATE tbl_mhs SET namaMahasiswa='" + textBox1.Text + "', jurusanMahasiswa='" + textBox3.Text + "', nilaiMahasiswa='" + textBox4.Text + "' WHERE nimMahasiswa='" + textBox2.Text + "'", conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Update Data Berhasil!");
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }

            }
        }

        void DeleteData()
        {
            if (MessageBox.Show("Anda Yakin Menghapus Data [" + textBox1.Text + "] ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SqlConnection conn = Konn.GetConn();
                cmd = new SqlCommand("DELETE tbl_mhs WHERE nimMahasiswa='" + textBox2.Text + "'", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Delete Data Berhasil!");
                conn.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormMainMenu_Load(object sender, EventArgs e)
        {
            ShowAllData();
            ClearAll();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            textBox1.Text = row.Cells["namaMahasiswa"].Value.ToString();
            textBox2.Text = row.Cells["nimMahasiswa"].Value.ToString();
            textBox3.Text = row.Cells["jurusanMahasiswa"].Value.ToString();
            textBox4.Text = row.Cells["nilaiMahasiswa"].Value.ToString();
        }

        // Button Trigger Tambah Data
        private void button1_Click(object sender, EventArgs e)
        {
            AddData();
            ShowAllData();
            ClearAll();
        }

        // Button Trigger Update Data
        private void button2_Click(object sender, EventArgs e)
        {
            UpdateData();
            ShowAllData();
            ClearAll();
        }

        // Button Trigger Hapus Data
        private void button3_Click(object sender, EventArgs e)
        {
            DeleteData();
            ShowAllData();
            ClearAll();
        }
    }
}

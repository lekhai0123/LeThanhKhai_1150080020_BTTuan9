using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LeThanhKhai_1150080020_BTTuan9
{
    public partial class Form1 : Form
    {
        private string strCon =
            @"Data Source=DESKTOP-DJ7K5TD;Initial Catalog=QuanLyBanSach;Integrated Security=True;Encrypt=False";
        private SqlConnection sqlCon = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void MoKetNoi()
        {
            if (sqlCon == null)
                sqlCon = new SqlConnection(strCon);
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
        }

        private void DongKetNoi()
        {
            if (sqlCon != null && sqlCon.State == ConnectionState.Open)
                sqlCon.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                MoKetNoi();
                string sql = "SELECT * FROM NhaXuatBan";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, sqlCon);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "tblNhaXuatBan");
                dgvDanhSach.DataSource = ds.Tables["tblNhaXuatBan"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
            finally
            {
                DongKetNoi();
            }
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
        }

        private void dgvDanhSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

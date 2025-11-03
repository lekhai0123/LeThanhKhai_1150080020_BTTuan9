using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LeThanhKhai_1150080020_BTTuan9
{
    public partial class Form2 : Form
    {
        private string strCon =
            @"Data Source=DESKTOP-DJ7K5TD;Initial Catalog=QuanLyBanSach;Integrated Security=True;Encrypt=False";
        private SqlConnection sqlCon = null;
        private SqlDataAdapter adapter = null;
        private DataSet ds = null;

        public Form2()
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

        private void XoaDuLieuForm()
        {
            txtMaNXB.Text = txtTenNXB.Text = txtDiaChi.Text = "";
        }

        private void HienThiDuLieu()
        {
            MoKetNoi();
            string query = "SELECT * FROM NhaXuatBan";
            adapter = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            ds = new DataSet();
            adapter.Fill(ds, "tblNhaXuatBan");
            dgvDanhSach.DataSource = ds.Tables["tblNhaXuatBan"];
            DongKetNoi();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            HienThiDuLieu();
            XoaDuLieuForm();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row = ds.Tables["tblNhaXuatBan"].NewRow();
                row["MaNXB"] = txtMaNXB.Text.Trim();
                row["TenNXB"] = txtTenNXB.Text.Trim();
                row["DiaChi"] = txtDiaChi.Text.Trim();
                ds.Tables["tblNhaXuatBan"].Rows.Add(row);

                int kq = adapter.Update(ds.Tables["tblNhaXuatBan"]);
                if (kq > 0)
                {
                    MessageBox.Show("Thêm dữ liệu thành công!");
                    HienThiDuLieu();
                    XoaDuLieuForm();
                }
                else
                    MessageBox.Show("Thêm dữ liệu không thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LeThanhKhai_1150080020_BTTuan9
{
    public partial class Form3 : Form
    {
        private string strCon =
            @"Data Source=DESKTOP-DJ7K5TD;Initial Catalog=QuanLyBanSach;Integrated Security=True;Encrypt=False";
        private SqlConnection sqlCon = null;
        private SqlDataAdapter adapter = null;
        private DataSet ds = null;
        private int vt = -1;

        public Form3()
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

        private void Form3_Load(object sender, EventArgs e)
        {
            HienThiDuLieu();
            XoaDuLieuForm();
        }

        private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            vt = e.RowIndex;
            if (vt == -1) return;
            DataRow row = ds.Tables["tblNhaXuatBan"].Rows[vt];
            txtMaNXB.Text = row["MaNXB"].ToString().Trim();
            txtTenNXB.Text = row["TenNXB"].ToString().Trim();
            txtDiaChi.Text = row["DiaChi"].ToString().Trim();
        }

        private void btnChinhSua_Click(object sender, EventArgs e)
        {
            if (vt == -1)
            {
                MessageBox.Show("Bạn chưa chọn dữ liệu để chỉnh sửa!");
                return;
            }

            try
            {
                DataRow row = ds.Tables["tblNhaXuatBan"].Rows[vt];
                row.BeginEdit();
                row["MaNXB"] = txtMaNXB.Text.Trim();
                row["TenNXB"] = txtTenNXB.Text.Trim();
                row["DiaChi"] = txtDiaChi.Text.Trim();
                row.EndEdit();

                int kq = adapter.Update(ds.Tables["tblNhaXuatBan"]);
                if (kq > 0)
                {
                    MessageBox.Show("Chỉnh sửa dữ liệu thành công!");
                    HienThiDuLieu();
                    XoaDuLieuForm();
                }
                else
                    MessageBox.Show("Chỉnh sửa không thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void dgvDanhSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

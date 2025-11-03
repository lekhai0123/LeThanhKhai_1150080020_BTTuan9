using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LeThanhKhai_1150080020_BTTuan9
{
    public partial class Form4 : Form
    {
        private string strCon =
            @"Data Source=DESKTOP-DJ7K5TD;Initial Catalog=QuanLyBanSach;Integrated Security=True;Encrypt=False";
        private SqlConnection sqlCon = null;
        private SqlDataAdapter adapter = null;
        private DataSet ds = null;
        private int vt = -1;

        public Form4()
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

        private void Form4_Load(object sender, EventArgs e)
        {
            HienThiDuLieu();
        }

        private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            vt = e.RowIndex;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (vt == -1)
            {
                MessageBox.Show("Bạn chưa chọn dữ liệu để xóa!");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có thực sự muốn xóa bản ghi này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DataRow row = ds.Tables["tblNhaXuatBan"].Rows[vt];
                    row.Delete();
                    int kq = adapter.Update(ds.Tables["tblNhaXuatBan"]);
                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công!");
                        HienThiDuLieu();
                        vt = -1;
                    }
                    else
                        MessageBox.Show("Xóa dữ liệu không thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
    }
}

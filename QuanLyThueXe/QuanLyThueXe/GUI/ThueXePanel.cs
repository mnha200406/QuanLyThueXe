using QuanLyThueXe.DAO;
using QuanLyThueXe.DTO;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyThueXe.GUI
{
    public partial class ThueXePanel : UserControl
    {
        ThueXeDAO thueXeDAO = new ThueXeDAO();

        string _maXeDaChon = "";

        public ThueXePanel()
        {
            InitializeComponent();
        }

        void LoadXe()
        {
            dgvXe.DataSource = null;
            dgvXe.Columns.Clear();

            dgvXe.DataSource = thueXeDAO.LoadXe();

            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Name = "ChonXe";
            btn.HeaderText = "Chọn?";
            btn.Text = "Chọn";
            btn.UseColumnTextForButtonValue = true;
            dgvXe.Columns.Add(btn); // ✅ Add() tự đặt ở cuối
        }

        void LoadKhachHang()
        {
            DataTable dt = thueXeDAO.LoadKhachHang();

            cboTenKhachHang.DataSource = null;
            cboTenKhachHang.DisplayMember = "TenKhachHang";
            cboTenKhachHang.ValueMember = "MaKhachHang";
            cboTenKhachHang.DataSource = dt;

            cboTenKhachHang.SelectedIndex = -1;
        }

        void HienThiMaThueTiepTheo()
        {
            txtMaThue.Text = thueXeDAO.LayMaThueTiepTheo().ToString();
        }

        /// <summary>
        /// Tính số ngày dự kiến = (NgayTraDuKien - NgayThue) + 1
        /// </summary>
        private void TinhSoNgay()
        {
            DateTime ngayThue = dtpNgayThue.Value.Date;
            DateTime ngayTra = dtpNgayTraDuKien.Value.Date;

            if (ngayTra < ngayThue)
            {
                dtpNgayTraDuKien.Value = dtpNgayThue.Value;
            }
            else
            {
                int soNgay = (int)(ngayTra - ngayThue).TotalDays + 1;
                txtSoNgayThue.Text = soNgay.ToString();
            }
        }

        /// <summary>
        /// Tiền cọc = số ngày dự kiến × giá thuê/ngày
        /// </summary>
        private void TinhTienCoc()
        {
            lblTienCoc.Text = (2_000_000).ToString("N0");
        }

        private void LamMoi()
        {
            txtTenXe.Clear();
            txtLoaiXe.Clear();
            txtBienSo.Clear();
            txtGiaThueTheoNgay.Clear();
            txtCCCD.Clear();
            txtSDT.Clear();
            lblTienCoc.Text = "0";
            txtSoNgayThue.Text = "1";

            _maXeDaChon = "";

            cboTenKhachHang.SelectedIndex = -1;

            LoadXe();
        }

        private void ThueXePanel_Load(object sender, EventArgs e)
        {
            dtpNgayThue.Value = DateTime.Now;

            LoadXe();
            LoadKhachHang();
            HienThiMaThueTiepTheo();
            TinhSoNgay();
        }

        private void btnLapPhieu_Click(object sender, EventArgs e)
        {
            if (cboTenKhachHang.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng!");
                return;
            }

            if (string.IsNullOrEmpty(_maXeDaChon))
            {
                MessageBox.Show("Vui lòng chọn xe!");
                return;
            }

            decimal tienCoc = 0;
            if (!decimal.TryParse(lblTienCoc.Text.Replace(",", "").Replace(".", ""), out tienCoc))
            {
                MessageBox.Show("Tiền cọc không hợp lệ!");
                return;
            }

            ThueXeDTO thueXe = new ThueXeDTO()
            {
                MaKhachHang = cboTenKhachHang.SelectedValue.ToString(),
                MaXe = _maXeDaChon,
                NgayThue = dtpNgayThue.Value,
                NgayTraDuKien = dtpNgayTraDuKien.Value,
                TienCoc = tienCoc
            };

            bool result = thueXeDAO.LapPhieuThue(thueXe);

            if (result)
            {
                MessageBox.Show("Lập phiếu thuê thành công!");
                LamMoi();
                HienThiMaThueTiepTheo();
            }
            else
            {
                MessageBox.Show("Lập phiếu thất bại!");
            }
        }

        private void dgvXe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvXe.Columns[e.ColumnIndex].Name == "ChonXe")
            {
                DataGridViewRow row = dgvXe.Rows[e.RowIndex];

                _maXeDaChon = row.Cells["Mã xe"].Value.ToString();
                txtTenXe.Text = row.Cells["Tên xe"].Value.ToString();
                txtLoaiXe.Text = row.Cells["Loại xe"].Value.ToString();
                txtBienSo.Text = row.Cells["Biển số"].Value.ToString();
                txtGiaThueTheoNgay.Text = row.Cells["Giá thuê/ngày"].Value.ToString();

                // ✅ Cập nhật tiền cọc sau khi chọn xe
                TinhTienCoc();
            }
        }

        private void cboTenKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTenKhachHang.SelectedIndex == -1 || cboTenKhachHang.SelectedValue == null)
            {
                txtSDT.Clear();
                txtCCCD.Clear();
                return;
            }

            DataTable dt = thueXeDAO.LayThongTinKhachHang(cboTenKhachHang.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                txtSDT.Text = dt.Rows[0]["SDT"].ToString();
                txtCCCD.Text = dt.Rows[0]["CCCD"].ToString();
            }
        }

        private void dtpNgayTraDuKien_ValueChanged(object sender, EventArgs e)
        {
            TinhSoNgay();
            // ✅ Cập nhật tiền cọc khi ngày trả thay đổi
            TinhTienCoc();
        }

        private void dtpNgayThue_ValueChanged(object sender, EventArgs e)
        {
            dtpNgayTraDuKien.MinDate = dtpNgayThue.Value;
            TinhSoNgay();
            // ✅ Cập nhật tiền cọc khi ngày thuê thay đổi
            TinhTienCoc();
        }

        private void txtSoNgayThue_TextChanged(object sender, EventArgs e)
        {
            TinhTienCoc();
        }

        private void txtGiaThueTheoNgay_TextChanged(object sender, EventArgs e)
        {
            TinhTienCoc();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void CHON_XE_Enter(object sender, EventArgs e) { }
    }
}
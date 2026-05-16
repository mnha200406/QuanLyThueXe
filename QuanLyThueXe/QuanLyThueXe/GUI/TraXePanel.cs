using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using QuanLyThueXe.DAO;
using QuanLyThueXe.DTO;

namespace QuanLyThueXe.GUI
{
    public partial class TraXePanel : UserControl
    {
        TraXeDAO traXeDAO = new TraXeDAO();
        private static readonly CultureInfo viVN = CultureInfo.GetCultureInfo("vi-VN");

        public int soNgayThucTe { get; private set; } = 0;
        public decimal DoanhThu { get; private set; } = 0;

        public TraXePanel()
        {
            InitializeComponent();
            this.Load += TraXePanel_Load;
            dgvDanhSachXeDangThue.CellClick += dgvDanhSachXeDangThue_CellClick;
            // Reload khi tab được chọn
            this.VisibleChanged += (s, e) => { if (this.Visible) LoadData(); };
        }

        private void TraXePanel_Load(object sender, EventArgs e)
        {
            dgvDanhSachXeDangThue.AutoGenerateColumns = false;
            dgvDanhSachXeDangThue.Columns["colMaThue"].DataPropertyName = "MaPhieuThue";
            dgvDanhSachXeDangThue.Columns["colTenKhachHang"].DataPropertyName = "TenKhachHang";
            dgvDanhSachXeDangThue.Columns["colSDT"].DataPropertyName = "SDT";
            dgvDanhSachXeDangThue.Columns["colTenXe"].DataPropertyName = "TenXe";
            dgvDanhSachXeDangThue.Columns["colBienSo"].DataPropertyName = "BienSo";
            dgvDanhSachXeDangThue.Columns["colNgayThue"].DataPropertyName = "NgayThue";
            dgvDanhSachXeDangThue.Columns["colGiaThue"].DataPropertyName = "GiaThue";
            LoadData();
        }

        private void LoadData()
        {
            dgvDanhSachXeDangThue.DataSource = null; // ✅ reset trước
            dgvDanhSachXeDangThue.DataSource = traXeDAO.GetDanhSachXeDangThue();
        }

        private void dgvDanhSachXeDangThue_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dgvDanhSachXeDangThue.Rows[e.RowIndex].DataBoundItem as TraXeDTO;
                if (selectedRow != null)
                {
                    txtMaThue.Text = selectedRow.MaPhieuThue.ToString();
                    txtTenKhachHang.Text = selectedRow.TenKhachHang;
                    txtSoDienThoai.Text = selectedRow.SDT;
                    txtCCCD.Text = selectedRow.CCCD;
                    txtXe.Text = selectedRow.TenXe;
                    txtGiaThue.Text = selectedRow.GiaThue.ToString("N0", viVN);
                    txtNgayBatDauThue.Text = selectedRow.NgayThue.ToString("dd/MM/yyyy");
                    txtNgayTraDuKien.Text = selectedRow.NgayTraDuKien.ToString("dd/MM/yyyy");
                    txtTienCoc.Text = selectedRow.TienCoc.ToString("N0", viVN);
                    txtNgayTraThucTe.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
        }

        private void btnTinhTien_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaThue.Text))
            {
                MessageBox.Show("Chọn phiếu thuê trước khi tính tiền");
                return;
            }

            DateTime ngayThue = DateTime.ParseExact(txtNgayBatDauThue.Text, "dd/MM/yyyy", null);
            DateTime ngayDuKien = DateTime.ParseExact(txtNgayTraDuKien.Text, "dd/MM/yyyy", null);
            DateTime ngayThucTe = DateTime.Now;

            decimal giaThue = decimal.Parse(txtGiaThue.Text, NumberStyles.Any, viVN);
            decimal tienCoc = decimal.Parse(txtTienCoc.Text, NumberStyles.Any, viVN);

            int soNgay = (ngayThucTe.Date - ngayThue.Date).Days;
            if (soNgay <= 0) soNgay = 1;

            this.soNgayThucTe = soNgay;
            this.DoanhThu = soNgay * giaThue;

            // ✅ Tính phạt TRƯỚC
            decimal tienPhat = 0;
            if (ngayThucTe.Date > ngayDuKien.Date)
            {
                int soNgayTre = (ngayThucTe.Date - ngayDuKien.Date).Days;
                tienPhat = soNgayTre * (giaThue * 0.5m);
            }

            // ✅ Tổng tiền = Doanh thu + Phạt - Cọc
            decimal tongTien = this.DoanhThu + tienPhat - tienCoc;

            txtTienPhat.Text = tienPhat.ToString("N0", viVN);
            txtTongTien.Text = tongTien.ToString("N0", viVN);
        }

        private void btnTraXe_Click(object sender, EventArgs e)
        {
            var selectedRow = dgvDanhSachXeDangThue.CurrentRow?.DataBoundItem as TraXeDTO;
            if (selectedRow == null)
            {
                MessageBox.Show("Chọn phiếu thuê trước khi trả xe");
                return;
            }

            if (string.IsNullOrEmpty(txtTongTien.Text))
            {
                MessageBox.Show("Tính tiền trước khi trả xe");
                return;
            }

            int maPT = int.Parse(txtMaThue.Text);
            int maXe = selectedRow.MaXe;
            int soNgay = this.soNgayThucTe;
            decimal tong = this.DoanhThu;
            // ✅ Parse có CultureInfo
            decimal phat = decimal.Parse(txtTienPhat.Text, NumberStyles.Any, viVN);

            if (traXeDAO.ThucHienTraXe(maPT, maXe, soNgay, tong, phat))
            {
                MessageBox.Show("Trả xe thành công");
                btnHuy_Click(null, null);
                LoadData();
            }
            else
            {
                MessageBox.Show("Trả xe lỗi");
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMaThue.Clear();
            txtTenKhachHang.Clear();
            txtSoDienThoai.Clear();
            txtCCCD.Clear();
            txtXe.Clear();
            txtGiaThue.Clear();
            txtNgayBatDauThue.Clear();
            txtNgayTraDuKien.Clear();
            txtTienCoc.Clear();
            txtNgayTraThucTe.Clear();
            txtTongTien.Clear();
            txtTienPhat.Clear();
            this.soNgayThucTe = 0;
            this.DoanhThu = 0;
        }

        private void khachHangPanel2_Load(object sender, EventArgs e) { }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) { }
        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e) { }
        private void label15_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void groupBox2_Enter(object sender, EventArgs e) { }
        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void groupBox1_Enter_1(object sender, EventArgs e) { }

        private void txtNgayBatDauThue_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
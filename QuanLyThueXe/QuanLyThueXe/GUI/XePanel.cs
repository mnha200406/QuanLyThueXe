using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using QuanLyThueXe.DAO;
using QuanLyThueXe.DTO;

namespace QuanLyThueXe.GUI
{
    public partial class XePanel : UserControl
    {
        private XeDAO xeDAO = new XeDAO();
        private bool dangCapNhat = false; // false = thêm mới, true = cập nhật

        public XePanel()
        {
            InitializeComponent();
            LoadDanhSachXe();
            SetMaXeMoi();
            DangKySuKien();
        }

        // ============================================================
        // KHỞI TẠO
        // ============================================================

        private void DangKySuKien()
        {
            dgvXe.CellClick += DgvXe_CellClick;
            txtTimKiemXe.TextChanged += TxtTimKiemXe_TextChanged;
            button1.Click += BtnLuu_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += BtnLamMoi_Click;

            // Chặn ký tự không hợp lệ ở txtGiaThue (chỉ cho nhập số và dấu chấm)
            txtGiaThue.KeyPress += TxtGiaThue_KeyPress;
        }

        // ============================================================
        // LOAD DỮ LIỆU
        // ============================================================

        private void LoadDanhSachXe(List<XeDTO> data = null)
        {
            dgvXe.Rows.Clear();

            List<XeDTO> danhSach = data ?? xeDAO.GetAllXe();

            foreach (XeDTO xe in danhSach)
            {
                dgvXe.Rows.Add(
                    xe.MaXe,
                    xe.LoaiXe,
                    xe.TenXe,
                    xe.BienSo,
                    xe.GiaThue.ToString("N0") + " đ",
                    xe.TrangThaiText
                );
            }
        }

        // Đặt MaXe mới = Max hiện có + 1
        private void SetMaXeMoi()
        {
            int maxMa = xeDAO.GetMaxMaXe();
            txtMaXe.Text = (maxMa + 1).ToString();
            dangCapNhat = false;
        }

        // ============================================================
        // SỰ KIỆN TÌM KIẾM
        // ============================================================

        private void TxtTimKiemXe_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiemXe.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
                LoadDanhSachXe();
            else
                LoadDanhSachXe(xeDAO.TimKiemXe(keyword));
        }

        // ============================================================
        // SỰ KIỆN CHỌN DÒNG TRÊN LƯỚI
        // ============================================================

        private void DgvXe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvXe.Rows[e.RowIndex];
            txtMaXe.Text = row.Cells["colMaXe"].Value?.ToString();
            cbLoaiXe.Text = row.Cells["Column1"].Value?.ToString();
            txtTenXe.Text = row.Cells["colTenXe"].Value?.ToString();
            txtBienSo.Text = row.Cells["colBienSo"].Value?.ToString();

            // Giá thuê: bỏ " đ" và dấu phân cách để lấy số thuần
            string giaRaw = row.Cells["colGiaThue"].Value?.ToString()
                               .Replace("đ", "").Replace(",", "").Replace(".", "").Trim();
            txtGiaThue.Text = giaRaw;

            // Trạng thái: map text → index combobox
            string trangThaiText = row.Cells["colTrangThai"].Value?.ToString();
            cbTrangThai.SelectedIndex = MapTrangThaiTextToIndex(trangThaiText);

            dangCapNhat = true;
        }

        // ============================================================
        // SỰ KIỆN NÚT LƯU (Thêm mới / Cập nhật)
        // ============================================================

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            XeDTO xe = GetXeFromForm();

            if (dangCapNhat)
            {
                // Cập nhật
                bool ok = xeDAO.CapNhatXe(xe);
                if (ok)
                {
                    MessageBox.Show("Cập nhật xe thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LamMoi();
                }
                else
                    MessageBox.Show("Cập nhật thất bại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Thêm mới — kiểm tra trùng MaXe
                if (xeDAO.IsMaXeExists(xe.MaXe))
                {
                    MessageBox.Show("Mã xe đã tồn tại! Vui lòng kiểm tra lại.",
                        "Trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaXe.Focus();
                    return;
                }

                bool ok = xeDAO.ThemXe(xe);
                if (ok)
                {
                    MessageBox.Show("Thêm xe thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LamMoi();
                }
                else
                    MessageBox.Show("Thêm xe thất bại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // SỰ KIỆN NÚT XÓA
        // ============================================================

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaXe.Text))
            {
                MessageBox.Show("Vui lòng chọn xe cần xóa từ danh sách.",
                    "Chưa chọn xe", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maXe = int.Parse(txtMaXe.Text);

            // Kiểm tra xe có đang được thuê không
            if (xeDAO.IsXeDangThue(maXe))
            {
                MessageBox.Show("Không thể xóa! Xe này đang có hợp đồng thuê.",
                    "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa xe mã {maXe} không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                bool ok = xeDAO.XoaXe(maXe);
                if (ok)
                {
                    MessageBox.Show("Xóa xe thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LamMoi();
                }
                else
                    MessageBox.Show("Xóa thất bại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // SỰ KIỆN NÚT LÀM MỚI
        // ============================================================

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        // ============================================================
        // VALIDATION
        // ============================================================

        private bool ValidateInput()
        {
            // Mã xe
            if (string.IsNullOrWhiteSpace(txtMaXe.Text) ||
                !int.TryParse(txtMaXe.Text, out int maXe) || maXe <= 0)
            {
                MessageBox.Show("Mã xe phải là số nguyên dương và không để trống!",
                    "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaXe.Focus();
                return false;
            }

            // Loại xe
            if (cbLoaiXe.SelectedIndex < 0 || string.IsNullOrWhiteSpace(cbLoaiXe.Text))
            {
                MessageBox.Show("Vui lòng chọn loại xe!",
                    "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbLoaiXe.Focus();
                return false;
            }

            // Tên xe
            if (string.IsNullOrWhiteSpace(txtTenXe.Text))
            {
                MessageBox.Show("Tên xe không được để trống!",
                    "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenXe.Focus();
                return false;
            }

            // Biển số: định dạng ví dụ 29A-123.45
            string bienSo = txtBienSo.Text.Trim();
            if (string.IsNullOrWhiteSpace(bienSo))
            {
                MessageBox.Show("Biển số xe không được để trống!",
                    "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBienSo.Focus();
                return false;
            }

            Regex regexBienSo = new Regex(@"^\d{2}[A-Z]\d?-\d{3,5}(\.\d{1,2})?$");
            if (!regexBienSo.IsMatch(bienSo))
            {
                MessageBox.Show("Biển số xe không đúng định dạng (ví dụ: 29A-123.45)!",
                    "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBienSo.Focus();
                return false;
            }

            // Kiểm tra biển số trùng với xe khác
            int excludeMa = dangCapNhat ? int.Parse(txtMaXe.Text) : -1;
            if (xeDAO.IsBienSoExists(bienSo, excludeMa))
            {
                MessageBox.Show("Biển số xe đã tồn tại trong hệ thống!",
                    "Trùng biển số", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBienSo.Focus();
                return false;
            }

            // Giá thuê
            if (!decimal.TryParse(txtGiaThue.Text.Trim(), out decimal giaThue) || giaThue <= 0)
            {
                MessageBox.Show("Giá thuê phải là số dương lớn hơn 0!",
                    "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaThue.Focus();
                return false;
            }

            // Trạng thái
            if (cbTrangThai.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn trạng thái xe!",
                    "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbTrangThai.Focus();
                return false;
            }

            return true;
        }

        // ============================================================
        // HÀM TIỆN ÍCH
        // ============================================================

        // Lấy thông tin từ form thành XeDTO
        private XeDTO GetXeFromForm()
        {
            return new XeDTO(
                int.Parse(txtMaXe.Text.Trim()),
                cbLoaiXe.Text.Trim(),
                txtTenXe.Text.Trim(),
                txtBienSo.Text.Trim(),
                decimal.Parse(txtGiaThue.Text.Trim()),
                cbTrangThai.SelectedIndex  // 0=Sẵn Sàng, 1=Đang thuê, 2=Đang bảo trì
            );
        }

        // Reset form về trạng thái ban đầu
        private void LamMoi()
        {
            txtTenXe.Clear();
            txtBienSo.Clear();
            txtGiaThue.Clear();
            txtTimKiemXe.Clear();
            cbLoaiXe.SelectedIndex = -1;
            cbTrangThai.SelectedIndex = -1;
            LoadDanhSachXe();
            SetMaXeMoi();
        }

        // Map chuỗi trạng thái → index combobox
        private int MapTrangThaiTextToIndex(string text)
        {
            switch (text)
            {
                case "Sẵn Sàng": return 0;
                case "Đang thuê": return 1;
                case "Đang bảo trì": return 2;
                default: return -1;
            }
        }

        // Chặn nhập ký tự không phải số và dấu chấm vào txtGiaThue
        private void TxtGiaThue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
            // Chỉ cho phép 1 dấu chấm
            if (e.KeyChar == '.' && txtGiaThue.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        // ============================================================
        // CÁC EVENT STUB TỪ DESIGNER (giữ để không lỗi build)
        // ============================================================

        private void groupBox1_Enter(object sender, EventArgs e) { }

        private void label8_Click(object sender, EventArgs e) { }

        private void label7_Click(object sender, EventArgs e) { }

        private void label5_Click(object sender, EventArgs e) { }

        private void txtTenXe_TextChanged(object sender, EventArgs e) { }

        private void cbTrangThai_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}

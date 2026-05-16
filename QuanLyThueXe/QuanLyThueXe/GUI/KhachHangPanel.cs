using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QuanLyThueXe.DAO;
using QuanLyThueXe.DTO;

namespace QuanLyThueXe.GUI
{
    public partial class KhachHangPanel : UserControl
    {
        private KhachHangDAO khachHangDAO = new KhachHangDAO();
        // true = đang thêm mới, false = đang cập nhật
        private bool dangThem = true;

        public KhachHangPanel()
        {
            InitializeComponent();
        }

        // ===================== LOAD =====================
        private void KhachHangPanel_Load(object sender, EventArgs e)
        {
            // Wire events thủ công vì Designer chưa có Click handler
            btnLuu.Click += btnLuu_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            dgvKhachHang.CellClick += dgvKhachHang_CellClick;

            SetupTimKiem();
            SetupValidation();

            LoadDanhSach();
            // Xóa sạch form nhập ngay khi mở — không cần ấn Làm mới
            XoaForm();
        }

        // ===================== DỮ LIỆU =====================
        private void LoadDanhSach(string keyword = "")
        {
            try
            {
                List<KhachHangDTO> list = string.IsNullOrWhiteSpace(keyword)
                    ? khachHangDAO.GetAll()
                    : khachHangDAO.Search(keyword);

                dgvKhachHang.Rows.Clear();
                foreach (var kh in list)
                    dgvKhachHang.Rows.Add(kh.MaKhachHang, kh.TenKhachHang, kh.CCCD, kh.SDT, kh.DiaChi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===================== TÌM KIẾM =====================
        private void SetupTimKiem()
        {
            txtTimKiemKhachHang.TextChanged += (s, e) =>
                LoadDanhSach(txtTimKiemKhachHang.Text.Trim());
        }

        // ===================== VALIDATION =====================
        private void SetupValidation()
        {
            // Chặn nhập chữ ở ô SĐT
            txtSDT.KeyPress += (s, e) =>
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                    e.Handled = true;
            };

            // Chặn nhập chữ ở ô CCCD
            txtCCCD.KeyPress += (s, e) =>
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                    e.Handled = true;
            };

            // Tự động viết hoa chữ cái đầu mỗi từ khi rời ô Tên KH
            txtTenKhachHang.Leave += (s, e) =>
            {
                if (!string.IsNullOrWhiteSpace(txtTenKhachHang.Text))
                    txtTenKhachHang.Text = ToTitleCase(txtTenKhachHang.Text.Trim());
            };
        }

        private string ToTitleCase(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            string[] words = input.Split(' ');
            for (int i = 0; i < words.Length; i++)
                if (words[i].Length > 0)
                    words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
            return string.Join(" ", words);
        }

        // ===================== NÚT LƯU =====================
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                KhachHangDTO kh = new KhachHangDTO
                {
                    TenKhachHang = txtTenKhachHang.Text.Trim(),
                    SDT = txtSDT.Text.Trim(),
                    CCCD = txtCCCD.Text.Trim(),
                    DiaChi = txtDiaChi.Text.Trim()
                };

                if (dangThem)
                {
                    // Chế độ THÊM MỚI: không truyền MaKhachHang, DB tự tăng IDENTITY
                    if (khachHangDAO.Insert(kh))
                        MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Thêm thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // Chế độ CẬP NHẬT: lấy mã từ ô (đã được điền khi click hàng)
                    kh.MaKhachHang = int.Parse(txtMaKhachHang.Text);
                    if (khachHangDAO.Update(kh))
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                LoadDanhSach();
                XoaForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===================== NÚT LÀM MỚI =====================
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            XoaForm();
            LoadDanhSach();
        }

        private void XoaForm()
        {
            try { txtMaKhachHang.Text = khachHangDAO.GetNextMa().ToString(); } catch { txtMaKhachHang.Text = "1"; }
            txtTenKhachHang.Clear();
            txtSDT.Clear();
            txtCCCD.Clear();
            txtDiaChi.Clear();
            txtTimKiemKhachHang.Clear();
            dangThem = true;
            txtTenKhachHang.Focus(); // Focus vào ô đầu tiên cần nhập
        }

        // ===================== CHỌN HÀNG TRÊN LƯỚI =====================
        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
            txtMaKhachHang.Text = row.Cells["colMaKhachHang"].Value?.ToString();
            txtTenKhachHang.Text = row.Cells["colTenKhachHang"].Value?.ToString();
            txtCCCD.Text = row.Cells["colCCCD"].Value?.ToString();
            txtSDT.Text = row.Cells["colSDT"].Value?.ToString();
            txtDiaChi.Text = row.Cells["colDiaChi"].Value?.ToString();

            dangThem = false; // Chế độ cập nhật
        }

        // Handler Designer wire sẵn cho txtCCCD
        private void txtCCCD_TextChanged(object sender, EventArgs e) { }

        // ===================== VALIDATE =====================
        private bool ValidateInput()
        {
            string ten = txtTenKhachHang.Text.Trim();
            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng nhập Tên Khách Hàng!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKhachHang.Focus();
                return false;
            }
            foreach (char c in ten)
            {
                if (char.IsDigit(c) || (!char.IsLetter(c) && c != ' '))
                {
                    MessageBox.Show("Tên không được chứa số hoặc ký tự đặc biệt!", "Không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenKhachHang.Focus();
                    return false;
                }
            }

            string sdt = txtSDT.Text.Trim();
            if (string.IsNullOrEmpty(sdt) || sdt.Length != 10)
            {
                MessageBox.Show("SĐT phải đúng 10 chữ số!", "Không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }

            string cccd = txtCCCD.Text.Trim();
            if (string.IsNullOrEmpty(cccd) || cccd.Length != 12)
            {
                MessageBox.Show("CCCD phải đúng 12 số!", "Không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCCCD.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Vui lòng nhập Địa Chỉ!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return false;
            }

            return true;
        }
    }
}
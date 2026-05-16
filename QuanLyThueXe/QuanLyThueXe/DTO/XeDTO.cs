using System;

namespace QuanLyThueXe.DTO
{
    public class XeDTO
    {
        public int MaXe { get; set; }
        public string LoaiXe { get; set; }
        public string TenXe { get; set; }
        public string BienSo { get; set; }
        public decimal GiaThue { get; set; }
        public int TrangThai { get; set; }

        // Trả về chuỗi trạng thái để hiển thị trên giao diện
        public string TrangThaiText
        {
            get
            {
                switch (TrangThai)
                {
                    case 0: return "Sẵn Sàng";
                    case 1: return "Đang thuê";
                    case 2: return "Đang bảo trì";
                    default: return "Không xác định";
                }
            }
        }

        public XeDTO() { }

        public XeDTO(int maXe, string loaiXe, string tenXe, string bienSo, decimal giaThue, int trangThai)
        {
            MaXe = maXe;
            LoaiXe = loaiXe;
            TenXe = tenXe;
            BienSo = bienSo;
            GiaThue = giaThue;
            TrangThai = trangThai;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueXe.DTO
{
    internal class TraXeDTO
    {
        public int MaPhieuThue { get; set; }
        public DateTime NgayThue { get; set; }
        public DateTime NgayTraDuKien { get; set; }
        public decimal TienCoc { get; set; }
        public int MaKhachHang { get; set; }
        public string TenKhachHang { get; set; }
        public string SDT { get; set; }
        public string CCCD { get; set; }
        public int MaXe { get; set; }
        public string TenXe { get; set; }
        public string BienSo { get; set; }
        public decimal GiaThue { get; set; }
        public int MaPhieuTra { get; set; }
        public DateTime NgayTraThucTe { get; set; }
        public int SoNgayThucTe { get; set; }
        public decimal TongTien { get; set; }
        public decimal TienPhat { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueXe.DTO
{
    internal class ThueXeDTO
    {
        public int MaPhieuThue { get; set; }
        public string MaKhachHang { get; set; }
        public string MaXe { get; set; }
        public DateTime NgayThue { get; set; }
        public DateTime NgayTraDuKien { get; set; }
        public decimal TienCoc { get; set; }
    }
}

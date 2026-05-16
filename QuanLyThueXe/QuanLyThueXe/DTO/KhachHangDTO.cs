namespace QuanLyThueXe.DTO
{
    public class KhachHangDTO
    {
        public int MaKhachHang { get; set; }
        public string TenKhachHang { get; set; }
        public string SDT { get; set; }
        public string CCCD { get; set; }
        public string DiaChi { get; set; }

        public KhachHangDTO() { }

        public KhachHangDTO(int maKhachHang, string tenKhachHang, string sdt, string cccd, string diaChi)
        {
            MaKhachHang = maKhachHang;
            TenKhachHang = tenKhachHang;
            SDT = sdt;
            CCCD = cccd;
            DiaChi = diaChi;
        }
    }
}
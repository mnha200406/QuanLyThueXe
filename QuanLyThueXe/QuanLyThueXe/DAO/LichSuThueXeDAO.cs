using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyThueXe.DTO;

namespace QuanLyThueXe.DAO
{
    public class LichSuThueXeDAO
    {
        private string query = @"
            SELECT 
                t.MaPhieuThue,
                tr.MaPhieuTra,
                kh.TenKhachHang,
                kh.SDT,
                kh.CCCD,
                x.TenXe,
                x.BienSo,
                t.NgayThue,
                tr.NgayTraThucTe,
                tr.TongTien,
                tr.TienPhat
            FROM ThueXe t
            JOIN KhachHang kh ON t.MaKhachHang = kh.MaKhachHang
            JOIN Xe x         ON t.MaXe         = x.MaXe
            JOIN TraXe tr     ON t.MaPhieuThue   = tr.MaPhieuThue";

        private LichSuThueXeDTO DocDuLieu(SqlDataReader reader)
        {
            return new LichSuThueXeDTO
            {
                MaPhieuThue = Convert.ToInt32(reader["MaPhieuThue"]),
                MaPhieuTra = Convert.ToInt32(reader["MaPhieuTra"]),  
                TenKhachHang = reader["TenKhachHang"].ToString(),
                SDT = reader["SDT"].ToString(),
                CCCD = reader["CCCD"].ToString(),
                TenXe = reader["TenXe"].ToString(),
                BienSo = reader["BienSo"].ToString(),
                NgayThue = Convert.ToDateTime(reader["NgayThue"]),
                NgayTraThucTe = Convert.ToDateTime(reader["NgayTraThucTe"]),
                TongTien = Convert.ToDecimal(reader["TongTien"]),
                TienPhat = Convert.ToDecimal(reader["TienPhat"]),
            };
        }

        public List<LichSuThueXeDTO> GetAll()
        {
            var danhSach = new List<LichSuThueXeDTO>();

            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand(query + " ORDER BY tr.NgayTraThucTe ASC", conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        danhSach.Add(DocDuLieu(reader));
            }

            return danhSach;
        }

        public List<LichSuThueXeDTO> TimKiem(string keyword)
        {
            var danhSach = new List<LichSuThueXeDTO>();

            string queryTimKiem = query + @"
                WHERE
                    t.MaPhieuThue       LIKE @kw OR
                    kh.TenKhachHang     LIKE @kw OR
                    kh.SDT              LIKE @kw OR
                    kh.CCCD             LIKE @kw OR
                    x.TenXe             LIKE @kw OR
                    x.BienSo            LIKE @kw
                ORDER BY tr.NgayTraThucTe ASC";

            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand(queryTimKiem, conn))
            {
                cmd.Parameters.AddWithValue("@kw", $"%{keyword.Trim()}%");
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        danhSach.Add(DocDuLieu(reader));
            }

            return danhSach;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QuanLyThueXe.DTO;

namespace QuanLyThueXe.DAO
{
    internal class TraXeDAO
    {
        private string query = @"
            SELECT 
                t.MaPhieuThue, k.TenKhachHang, k.SDT, x.TenXe, x.BienSo,
                t.NgayThue, x.GiaThue, t.NgayTraDuKien, t.TienCoc, k.CCCD, x.MaXe 
            FROM ThueXe t
            JOIN KhachHang k ON t.MaKhachHang = k.MaKhachHang
            JOIN Xe x        ON t.MaXe         = x.MaXe
            WHERE x.TrangThai = 1 
            AND t.MaPhieuThue NOT IN (SELECT MaPhieuThue FROM TraXe)";

        private TraXeDTO DocDuLieu(SqlDataReader reader)
        {
            return new TraXeDTO
            {
                MaPhieuThue = Convert.ToInt32(reader["MaPhieuThue"]),
                TenKhachHang = reader["TenKhachHang"].ToString(),
                SDT = reader["SDT"].ToString(),
                TenXe = reader["TenXe"].ToString(),
                BienSo = reader["BienSo"].ToString(),
                NgayThue = Convert.ToDateTime(reader["NgayThue"]),
                GiaThue = Convert.ToDecimal(reader["GiaThue"]),
                NgayTraDuKien = Convert.ToDateTime(reader["NgayTraDuKien"]),
                TienCoc = Convert.ToDecimal(reader["TienCoc"]),
                CCCD = reader["CCCD"].ToString(),
                MaXe = Convert.ToInt32(reader["MaXe"])
            };
        }

        public List<TraXeDTO> GetDanhSachXeDangThue()
        {
            var danhSach = new List<TraXeDTO>();

            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        danhSach.Add(DocDuLieu(reader));
            }
            Console.WriteLine(danhSach.Count);
            return danhSach;
        }

        public bool ThucHienTraXe(int maPhieuThue, int maXe, int soNgayThuc, decimal tongTien, decimal tienPhat)
        {
            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    string queryTra = @"
                        INSERT INTO TraXe (MaPhieuThue, NgayTraThucTe, SoNgayThucTe, TongTien, TienPhat) 
                        VALUES (@maPT, GETDATE(), @soNgay, @tong, @phat)";

                    using (var cmdTra = new SqlCommand(queryTra, conn, trans))
                    {
                        cmdTra.Parameters.AddWithValue("@maPT", maPhieuThue);
                        cmdTra.Parameters.AddWithValue("@soNgay", soNgayThuc);
                        cmdTra.Parameters.AddWithValue("@tong", tongTien);
                        cmdTra.Parameters.AddWithValue("@phat", tienPhat);
                        cmdTra.ExecuteNonQuery();
                    }

                    string queryXe = "UPDATE Xe SET TrangThai = 0 WHERE MaXe = @maXe";
                    using (var cmdXe = new SqlCommand(queryXe, conn, trans))
                    {
                        cmdXe.Parameters.AddWithValue("@maXe", maXe);
                        cmdXe.ExecuteNonQuery();
                    }

                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
            }
        }
    }
}
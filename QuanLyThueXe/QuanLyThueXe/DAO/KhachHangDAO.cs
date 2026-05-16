using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyThueXe.DTO;

namespace QuanLyThueXe.DAO
{
    public class KhachHangDAO
    {
        // Lấy tất cả khách hàng
        public List<KhachHangDTO> GetAll()
        {
            List<KhachHangDTO> list = new List<KhachHangDTO>();
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string sql = "SELECT MaKhachHang, TenKhachHang, SDT, CCCD, DiaChi FROM KhachHang";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new KhachHangDTO(
                        Convert.ToInt32(reader["MaKhachHang"]),
                        reader["TenKhachHang"].ToString(),
                        reader["SDT"].ToString(),
                        reader["CCCD"].ToString(),
                        reader["DiaChi"].ToString()
                    ));
                }
            }
            return list;
        }

        // Tìm kiếm theo tên hoặc SĐT hoặc CCCD
        public List<KhachHangDTO> Search(string keyword)
        {
            List<KhachHangDTO> list = new List<KhachHangDTO>();
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string sql = @"SELECT MaKhachHang, TenKhachHang, SDT, CCCD, DiaChi 
                               FROM KhachHang 
                               WHERE TenKhachHang LIKE @kw 
                                  OR SDT LIKE @kw 
                                  OR CCCD LIKE @kw";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new KhachHangDTO(
                        Convert.ToInt32(reader["MaKhachHang"]),
                        reader["TenKhachHang"].ToString(),
                        reader["SDT"].ToString(),
                        reader["CCCD"].ToString(),
                        reader["DiaChi"].ToString()
                    ));
                }
            }
            return list;
        }

        // Kiểm tra mã khách hàng đã tồn tại chưa
        public bool IsMaExists(int maKhachHang)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM KhachHang WHERE MaKhachHang = @ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ma", maKhachHang);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        // Lấy mã khách hàng lớn nhất (để tự động tăng)
        public int GetNextMa()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string sql = "SELECT ISNULL(MAX(MaKhachHang), 0) + 1 FROM KhachHang";
                SqlCommand cmd = new SqlCommand(sql, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // Thêm khách hàng mới (MaKhachHang là IDENTITY - DB tự tăng)
        public bool Insert(KhachHangDTO kh)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string sql = @"INSERT INTO KhachHang (TenKhachHang, SDT, CCCD, DiaChi)
                               VALUES (@ten, @sdt, @cccd, @diachi)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ten", kh.TenKhachHang);
                cmd.Parameters.AddWithValue("@sdt", kh.SDT);
                cmd.Parameters.AddWithValue("@cccd", kh.CCCD);
                cmd.Parameters.AddWithValue("@diachi", kh.DiaChi);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Cập nhật thông tin khách hàng
        public bool Update(KhachHangDTO kh)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string sql = @"UPDATE KhachHang 
                               SET TenKhachHang = @ten, SDT = @sdt, CCCD = @cccd, DiaChi = @diachi
                               WHERE MaKhachHang = @ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ten", kh.TenKhachHang);
                cmd.Parameters.AddWithValue("@sdt", kh.SDT);
                cmd.Parameters.AddWithValue("@cccd", kh.CCCD);
                cmd.Parameters.AddWithValue("@diachi", kh.DiaChi);
                cmd.Parameters.AddWithValue("@ma", kh.MaKhachHang);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
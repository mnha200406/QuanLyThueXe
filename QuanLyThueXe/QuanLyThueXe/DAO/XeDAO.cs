using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using QuanLyThueXe.DTO;

namespace QuanLyThueXe.DAO
{
    public class XeDAO
    {
        // Lấy toàn bộ danh sách xe
        public List<XeDTO> GetAllXe()
        {
            List<XeDTO> list = new List<XeDTO>();
            string query = "SELECT MaXe, LoaiXe, TenXe, BienSo, GiaThue, TrangThai FROM Xe ORDER BY MaXe";

            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new XeDTO(
                        Convert.ToInt32(reader["MaXe"]),
                        reader["LoaiXe"].ToString(),
                        reader["TenXe"].ToString(),
                        reader["BienSo"].ToString(),
                        Convert.ToDecimal(reader["GiaThue"]),
                        Convert.ToInt32(reader["TrangThai"])
                    ));
                }
            }
            return list;
        }

        // Tìm kiếm xe theo tên hoặc biển số
        public List<XeDTO> TimKiemXe(string keyword)
        {
            List<XeDTO> list = new List<XeDTO>();
            string query = @"SELECT MaXe, LoaiXe, TenXe, BienSo, GiaThue, TrangThai 
                             FROM Xe 
                             WHERE TenXe LIKE @kw OR BienSo LIKE @kw OR LoaiXe LIKE @kw
                             ORDER BY MaXe";

            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new XeDTO(
                        Convert.ToInt32(reader["MaXe"]),
                        reader["LoaiXe"].ToString(),
                        reader["TenXe"].ToString(),
                        reader["BienSo"].ToString(),
                        Convert.ToDecimal(reader["GiaThue"]),
                        Convert.ToInt32(reader["TrangThai"])
                    ));
                }
            }
            return list;
        }

        // Lấy xe theo MaXe
        public XeDTO GetXeByMa(int maXe)
        {
            string query = "SELECT MaXe, LoaiXe, TenXe, BienSo, GiaThue, TrangThai FROM Xe WHERE MaXe = @MaXe";
            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaXe", maXe);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new XeDTO(
                        Convert.ToInt32(reader["MaXe"]),
                        reader["LoaiXe"].ToString(),
                        reader["TenXe"].ToString(),
                        reader["BienSo"].ToString(),
                        Convert.ToDecimal(reader["GiaThue"]),
                        Convert.ToInt32(reader["TrangThai"])
                    );
                }
            }
            return null;
        }

        // Kiểm tra MaXe đã tồn tại chưa
        public bool IsMaXeExists(int maXe)
        {
            string query = "SELECT COUNT(*) FROM Xe WHERE MaXe = @MaXe";
            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaXe", maXe);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        // Kiểm tra BienSo đã tồn tại chưa (trừ xe hiện tại khi cập nhật)
        public bool IsBienSoExists(string bienSo, int excludeMaXe = -1)
        {
            string query = "SELECT COUNT(*) FROM Xe WHERE BienSo = @BienSo AND MaXe != @MaXe";
            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BienSo", bienSo);
                cmd.Parameters.AddWithValue("@MaXe", excludeMaXe);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        // Thêm xe mới
        public bool ThemXe(XeDTO xe)
        {
            string query = @"INSERT INTO Xe (LoaiXe, TenXe, BienSo, GiaThue, TrangThai)
                             VALUES (@LoaiXe, @TenXe, @BienSo, @GiaThue, @TrangThai)";
            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@LoaiXe", xe.LoaiXe);
                cmd.Parameters.AddWithValue("@TenXe", xe.TenXe);
                cmd.Parameters.AddWithValue("@BienSo", xe.BienSo);
                cmd.Parameters.AddWithValue("@GiaThue", xe.GiaThue);
                cmd.Parameters.AddWithValue("@TrangThai", xe.TrangThai);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Cập nhật thông tin xe
        public bool CapNhatXe(XeDTO xe)
        {
            string query = @"UPDATE Xe 
                             SET LoaiXe = @LoaiXe, TenXe = @TenXe, BienSo = @BienSo, 
                                 GiaThue = @GiaThue, TrangThai = @TrangThai
                             WHERE MaXe = @MaXe";
            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@LoaiXe", xe.LoaiXe);
                cmd.Parameters.AddWithValue("@TenXe", xe.TenXe);
                cmd.Parameters.AddWithValue("@BienSo", xe.BienSo);
                cmd.Parameters.AddWithValue("@GiaThue", xe.GiaThue);
                cmd.Parameters.AddWithValue("@TrangThai", xe.TrangThai);
                cmd.Parameters.AddWithValue("@MaXe", xe.MaXe);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Xóa xe theo MaXe
        public bool XoaXe(int maXe)
        {
            string query = "DELETE FROM Xe WHERE MaXe = @MaXe";
            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaXe", maXe);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Lấy MaXe lớn nhất hiện có (để gợi ý mã mới)
        public int GetMaxMaXe()
        {
            string query = "SELECT ISNULL(MAX(MaXe), 0) FROM Xe";
            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // Kiểm tra xe có đang được thuê không (tránh xóa nhầm)
        public bool IsXeDangThue(int maXe)
        {
            string query = "SELECT COUNT(*) FROM ThueXe WHERE MaXe = @MaXe";
            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaXe", maXe);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }
    }
}
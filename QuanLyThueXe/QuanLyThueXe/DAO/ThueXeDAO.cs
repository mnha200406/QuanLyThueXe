using QuanLyThueXe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThueXe.DAO
{
    internal class ThueXeDAO
    {

        public DataTable LoadXe()
        {
            using (var conn = DBConnection.GetConnection())
            {
                string query = @"
                    SELECT MaXe        AS [Mã xe],
                           TenXe       AS [Tên xe],
                           LoaiXe      AS [Loại xe],
                           BienSo      AS [Biển số],
                           GiaThue     AS [Giá thuê/ngày]
                    FROM Xe
                    WHERE TrangThai = 0
                    ORDER BY MaXe";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);

                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

        public DataTable LoadKhachHang()
        {
            using (var conn = DBConnection.GetConnection())
            {
                string query =
                    "SELECT MaKhachHang, TenKhachHang FROM KhachHang";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);

                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

        public DataTable LayThongTinKhachHang(string maKH)
        {
            using (var conn = DBConnection.GetConnection())
            {
                string query =
                    "SELECT SDT, CCCD FROM KhachHang WHERE MaKhachHang = @MaKH";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@MaKH", maKH);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

        public int LayMaThueTiepTheo()
        {
            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();

                string query =
                    "SELECT ISNULL(MAX(MaPhieuThue),0) + 1 FROM ThueXe";

                SqlCommand cmd = new SqlCommand(query, conn);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public bool LapPhieuThue(ThueXeDTO thueXe)
        {
            using (var conn = DBConnection.GetConnection())
            {
                conn.Open();

                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    string sqlInsert = @"
                        INSERT INTO ThueXe
                        (
                            MaKhachHang,
                            MaXe,
                            NgayThue,
                            NgayTraDuKien,
                            TienCoc
                        )
                        VALUES
                        (
                            @maKhachHang,
                            @maXe,
                            @ngayThue,
                            @ngayTra,
                            @tienCoc
                        )";

                    SqlCommand cmd =new SqlCommand(sqlInsert, conn, trans);

                    cmd.Parameters.AddWithValue("@maKhachHang",thueXe.MaKhachHang);

                    cmd.Parameters.AddWithValue("@maXe",thueXe.MaXe);

                    cmd.Parameters.AddWithValue( "@ngayThue",thueXe.NgayThue);

                    cmd.Parameters.AddWithValue("@ngayTra",thueXe.NgayTraDuKien);

                    cmd.Parameters.AddWithValue("@tienCoc",thueXe.TienCoc);

                    cmd.ExecuteNonQuery();

                    string sqlUpdateXe ="UPDATE Xe SET TrangThai = 1 WHERE MaXe = @maXe";

                    SqlCommand cmdUpdate =new SqlCommand(sqlUpdateXe, conn, trans);

                    cmdUpdate.Parameters.AddWithValue("@maXe",thueXe.MaXe);

                    cmdUpdate.ExecuteNonQuery();

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

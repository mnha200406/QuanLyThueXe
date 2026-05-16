using QuanLyThueXe.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;  
using QuanLyThueXe.DTO;   


namespace QuanLyThueXe.GUI
{
    public partial class LichSuThueXePanel : UserControl
    {
        private LichSuThueXeDAO dao = new LichSuThueXeDAO();
        public LichSuThueXePanel()
        {
            InitializeComponent();
            dgvLichSuChoThueXe.AutoGenerateColumns = false;
            TaiDuLieu();
            this.VisibleChanged += LichSuThueXePanel_VisibleChanged;
        }
        private void LichSuThueXePanel_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible) 
                TaiDuLieu();
        }

        public void TaiDuLieu()
        {
            HienThiDuLieu(dao.GetAll());
        }
        private void HienThiDuLieu(List<LichSuThueXeDTO> ds)
        {
            dgvLichSuChoThueXe.DataSource = null;
            dgvLichSuChoThueXe.DataSource = ds;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dgvLichSuChoThueXe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
                TaiDuLieu();
            else
                HienThiDuLieu(dao.TimKiem(txtTimKiem.Text));
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}

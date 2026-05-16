using QuanLyThueXe.GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThueXe
{
    public partial class HomePanel : Form
    {
        public HomePanel()
        {
            InitializeComponent();
        }

        private void showUC(Control uc)
        {
            panelMainContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelMainContent.Controls.Add(uc);
            uc.BringToFront();
        }

        private void buttonKhachHang_Click(object sender, EventArgs e)
        {
            showUC(new KhachHangPanel());
        }

        private void buttonXe_Click(object sender, EventArgs e)
        {
            showUC(new XePanel());
        }

        private void buttonThueXe_Click(object sender, EventArgs e)
        {
            showUC(new ThueXePanel());
        }

        private void buttonTraXe_Click(object sender, EventArgs e)
        {
            showUC(new TraXePanel());
        }

        private void buttonLichSuThueXe_Click(object sender, EventArgs e)
        {
            showUC(new LichSuThueXePanel());
        }

        private void panelMainContent_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

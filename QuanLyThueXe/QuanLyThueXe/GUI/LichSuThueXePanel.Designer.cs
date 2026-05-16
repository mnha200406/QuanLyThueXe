namespace QuanLyThueXe.GUI
{
    partial class LichSuThueXePanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvLichSuChoThueXe = new System.Windows.Forms.DataGridView();
            this.colMaPhieuThue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaPhieuTra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenKhachHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCCCD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenXe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBienSo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayThue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayTraThucTe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTongTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTienPhat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSuChoThueXe)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTimKiem);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dgvLichSuChoThueXe);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1298, 521);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "LỊCH SỬ THUÊ XE";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(165, 42);
            this.txtTimKiem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(423, 30);
            this.txtTimKiem.TabIndex = 7;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(7, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Tìm kiếm:";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // dgvLichSuChoThueXe
            // 
            this.dgvLichSuChoThueXe.AllowUserToAddRows = false;
            this.dgvLichSuChoThueXe.AllowUserToDeleteRows = false;
            this.dgvLichSuChoThueXe.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLichSuChoThueXe.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLichSuChoThueXe.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.dgvLichSuChoThueXe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLichSuChoThueXe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaPhieuThue,
            this.colMaPhieuTra,
            this.colTenKhachHang,
            this.colSDT,
            this.colCCCD,
            this.colTenXe,
            this.colBienSo,
            this.colNgayThue,
            this.colNgayTraThucTe,
            this.colTongTien,
            this.colTienPhat});
            this.dgvLichSuChoThueXe.Location = new System.Drawing.Point(11, 92);
            this.dgvLichSuChoThueXe.MultiSelect = false;
            this.dgvLichSuChoThueXe.Name = "dgvLichSuChoThueXe";
            this.dgvLichSuChoThueXe.ReadOnly = true;
            this.dgvLichSuChoThueXe.RowHeadersVisible = false;
            this.dgvLichSuChoThueXe.RowHeadersWidth = 51;
            this.dgvLichSuChoThueXe.RowTemplate.Height = 24;
            this.dgvLichSuChoThueXe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLichSuChoThueXe.Size = new System.Drawing.Size(1281, 375);
            this.dgvLichSuChoThueXe.TabIndex = 3;
            this.dgvLichSuChoThueXe.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLichSuChoThueXe_CellContentClick);
            // 
            // colMaPhieuThue
            // 
            this.colMaPhieuThue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colMaPhieuThue.DataPropertyName = "MaPhieuThue";
            this.colMaPhieuThue.FillWeight = 352.9412F;
            this.colMaPhieuThue.HeaderText = "Mã Phiếu Thuê";
            this.colMaPhieuThue.MinimumWidth = 6;
            this.colMaPhieuThue.Name = "colMaPhieuThue";
            this.colMaPhieuThue.ReadOnly = true;
            this.colMaPhieuThue.Width = 60;
            // 
            // colMaPhieuTra
            // 
            this.colMaPhieuTra.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colMaPhieuTra.DataPropertyName = "MaPhieuTra";
            this.colMaPhieuTra.FillWeight = 74.70588F;
            this.colMaPhieuTra.HeaderText = "Mã Phiếu Trả";
            this.colMaPhieuTra.MinimumWidth = 6;
            this.colMaPhieuTra.Name = "colMaPhieuTra";
            this.colMaPhieuTra.ReadOnly = true;
            this.colMaPhieuTra.Width = 60;
            // 
            // colTenKhachHang
            // 
            this.colTenKhachHang.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colTenKhachHang.DataPropertyName = "TenKhachHang";
            this.colTenKhachHang.FillWeight = 74.70588F;
            this.colTenKhachHang.HeaderText = "Tên Khách Hàng";
            this.colTenKhachHang.MinimumWidth = 6;
            this.colTenKhachHang.Name = "colTenKhachHang";
            this.colTenKhachHang.ReadOnly = true;
            this.colTenKhachHang.Width = 150;
            // 
            // colSDT
            // 
            this.colSDT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colSDT.DataPropertyName = "SDT";
            this.colSDT.FillWeight = 74.70588F;
            this.colSDT.HeaderText = "SDT";
            this.colSDT.MinimumWidth = 6;
            this.colSDT.Name = "colSDT";
            this.colSDT.ReadOnly = true;
            this.colSDT.Width = 110;
            // 
            // colCCCD
            // 
            this.colCCCD.DataPropertyName = "CCCD";
            this.colCCCD.FillWeight = 74.70588F;
            this.colCCCD.HeaderText = "CCCD";
            this.colCCCD.MinimumWidth = 6;
            this.colCCCD.Name = "colCCCD";
            this.colCCCD.ReadOnly = true;
            // 
            // colTenXe
            // 
            this.colTenXe.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colTenXe.DataPropertyName = "TenXe";
            this.colTenXe.FillWeight = 74.70588F;
            this.colTenXe.HeaderText = "Tên Xe";
            this.colTenXe.MinimumWidth = 6;
            this.colTenXe.Name = "colTenXe";
            this.colTenXe.ReadOnly = true;
            this.colTenXe.Width = 150;
            // 
            // colBienSo
            // 
            this.colBienSo.DataPropertyName = "BienSo";
            this.colBienSo.FillWeight = 74.70588F;
            this.colBienSo.HeaderText = "Biển Số";
            this.colBienSo.MinimumWidth = 6;
            this.colBienSo.Name = "colBienSo";
            this.colBienSo.ReadOnly = true;
            // 
            // colNgayThue
            // 
            this.colNgayThue.DataPropertyName = "NgayThue";
            this.colNgayThue.FillWeight = 74.70588F;
            this.colNgayThue.HeaderText = "Ngày Thuê";
            this.colNgayThue.MinimumWidth = 6;
            this.colNgayThue.Name = "colNgayThue";
            this.colNgayThue.ReadOnly = true;
            // 
            // colNgayTraThucTe
            // 
            this.colNgayTraThucTe.DataPropertyName = "NgayTraThucTe";
            this.colNgayTraThucTe.FillWeight = 74.70588F;
            this.colNgayTraThucTe.HeaderText = "Ngày Trả Thực Tế";
            this.colNgayTraThucTe.MinimumWidth = 6;
            this.colNgayTraThucTe.Name = "colNgayTraThucTe";
            this.colNgayTraThucTe.ReadOnly = true;
            // 
            // colTongTien
            // 
            this.colTongTien.DataPropertyName = "TongTien";
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            this.colTongTien.DefaultCellStyle = dataGridViewCellStyle1;
            this.colTongTien.FillWeight = 74.70588F;
            this.colTongTien.HeaderText = "Tổng Tiền";
            this.colTongTien.MinimumWidth = 6;
            this.colTongTien.Name = "colTongTien";
            this.colTongTien.ReadOnly = true;
            // 
            // colTienPhat
            // 
            this.colTienPhat.DataPropertyName = "TienPhat";
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.colTienPhat.DefaultCellStyle = dataGridViewCellStyle2;
            this.colTienPhat.FillWeight = 74.70588F;
            this.colTienPhat.HeaderText = "Tiền Phạt";
            this.colTienPhat.MinimumWidth = 6;
            this.colTienPhat.Name = "colTienPhat";
            this.colTienPhat.ReadOnly = true;
            // 
            // LichSuThueXePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "LichSuThueXePanel";
            this.Size = new System.Drawing.Size(1298, 521);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSuChoThueXe)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvLichSuChoThueXe;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaPhieuThue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaPhieuTra;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenKhachHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSDT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCCCD;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenXe;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBienSo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayThue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayTraThucTe;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTongTien;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTienPhat;
    }
}

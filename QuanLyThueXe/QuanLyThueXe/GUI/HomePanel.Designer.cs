namespace QuanLyThueXe
{
    partial class HomePanel
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelSideBar = new System.Windows.Forms.Panel();
            this.buttonLichSuThueXe = new System.Windows.Forms.Button();
            this.buttonTraXe = new System.Windows.Forms.Button();
            this.buttonThueXe = new System.Windows.Forms.Button();
            this.buttonXe = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panelMainContent = new System.Windows.Forms.Panel();
            this.buttonKhachHang = new System.Windows.Forms.Button();
            this.panelSideBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSideBar
            // 
            this.panelSideBar.BackColor = System.Drawing.SystemColors.Highlight;
            this.panelSideBar.Controls.Add(this.buttonLichSuThueXe);
            this.panelSideBar.Controls.Add(this.buttonTraXe);
            this.panelSideBar.Controls.Add(this.buttonThueXe);
            this.panelSideBar.Controls.Add(this.buttonXe);
            this.panelSideBar.Controls.Add(this.buttonKhachHang);
            this.panelSideBar.Controls.Add(this.label1);
            this.panelSideBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSideBar.Location = new System.Drawing.Point(0, 0);
            this.panelSideBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelSideBar.Name = "panelSideBar";
            this.panelSideBar.Size = new System.Drawing.Size(150, 625);
            this.panelSideBar.TabIndex = 0;
            // 
            // buttonLichSuThueXe
            // 
            this.buttonLichSuThueXe.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonLichSuThueXe.Location = new System.Drawing.Point(0, 163);
            this.buttonLichSuThueXe.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonLichSuThueXe.Name = "buttonLichSuThueXe";
            this.buttonLichSuThueXe.Size = new System.Drawing.Size(150, 32);
            this.buttonLichSuThueXe.TabIndex = 5;
            this.buttonLichSuThueXe.Text = "Lịch sử thuê xe";
            this.buttonLichSuThueXe.UseVisualStyleBackColor = true;
            this.buttonLichSuThueXe.Click += new System.EventHandler(this.buttonLichSuThueXe_Click);
            // 
            // buttonTraXe
            // 
            this.buttonTraXe.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonTraXe.Location = new System.Drawing.Point(0, 131);
            this.buttonTraXe.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonTraXe.Name = "buttonTraXe";
            this.buttonTraXe.Size = new System.Drawing.Size(150, 32);
            this.buttonTraXe.TabIndex = 4;
            this.buttonTraXe.Text = "Trả xe";
            this.buttonTraXe.UseVisualStyleBackColor = true;
            this.buttonTraXe.Click += new System.EventHandler(this.buttonTraXe_Click);
            // 
            // buttonThueXe
            // 
            this.buttonThueXe.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonThueXe.Location = new System.Drawing.Point(0, 99);
            this.buttonThueXe.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonThueXe.Name = "buttonThueXe";
            this.buttonThueXe.Size = new System.Drawing.Size(150, 32);
            this.buttonThueXe.TabIndex = 3;
            this.buttonThueXe.Text = "Thuê xe";
            this.buttonThueXe.UseVisualStyleBackColor = true;
            this.buttonThueXe.Click += new System.EventHandler(this.buttonThueXe_Click);
            // 
            // buttonXe
            // 
            this.buttonXe.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonXe.Location = new System.Drawing.Point(0, 67);
            this.buttonXe.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonXe.Name = "buttonXe";
            this.buttonXe.Size = new System.Drawing.Size(150, 32);
            this.buttonXe.TabIndex = 2;
            this.buttonXe.Text = "Quản lý xe";
            this.buttonXe.UseVisualStyleBackColor = true;
            this.buttonXe.Click += new System.EventHandler(this.buttonXe_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Highlight;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "MENU";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelMainContent
            // 
            this.panelMainContent.AutoSize = true;
            this.panelMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContent.Location = new System.Drawing.Point(150, 0);
            this.panelMainContent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelMainContent.Name = "panelMainContent";
            this.panelMainContent.Size = new System.Drawing.Size(853, 625);
            this.panelMainContent.TabIndex = 1;
            this.panelMainContent.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMainContent_Paint);
            // 
            // buttonKhachHang
            // 
            this.buttonKhachHang.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonKhachHang.Location = new System.Drawing.Point(0, 35);
            this.buttonKhachHang.Margin = new System.Windows.Forms.Padding(2);
            this.buttonKhachHang.Name = "buttonKhachHang";
            this.buttonKhachHang.Size = new System.Drawing.Size(150, 32);
            this.buttonKhachHang.TabIndex = 1;
            this.buttonKhachHang.Text = "Quản lý khách hàng";
            this.buttonKhachHang.UseVisualStyleBackColor = true;
            this.buttonKhachHang.Click += new System.EventHandler(this.buttonKhachHang_Click);
            // 
            // HomePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 625);
            this.Controls.Add(this.panelMainContent);
            this.Controls.Add(this.panelSideBar);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "HomePanel";
            this.Text = "Hệ thống quản lý thuê xe";
            this.panelSideBar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelSideBar;
        private System.Windows.Forms.Panel panelMainContent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonLichSuThueXe;
        private System.Windows.Forms.Button buttonTraXe;
        private System.Windows.Forms.Button buttonThueXe;
        private System.Windows.Forms.Button buttonXe;
        private System.Windows.Forms.Button buttonKhachHang;
    }
}


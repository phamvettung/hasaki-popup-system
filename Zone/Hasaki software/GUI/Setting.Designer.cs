namespace Intech_software.GUI
{
    partial class FrmSetting
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbBaudRate = new System.Windows.Forms.ComboBox();
            this.txtDatamanPassword = new System.Windows.Forms.TextBox();
            this.cbComPort = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbCamDevice = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.Zone001 = new System.Windows.Forms.Label();
            this.clBoxDvvcZone1 = new System.Windows.Forms.CheckedListBox();
            this.clBoxDvvcZone2 = new System.Windows.Forms.CheckedListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.clBoxDvvcZone3 = new System.Windows.Forms.CheckedListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.clBoxDvvcZone4 = new System.Windows.Forms.CheckedListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.clBoxDvvcZone5 = new System.Windows.Forms.CheckedListBox();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1202, 486);
            this.panel1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(653, 438);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(143, 32);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Green;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSave.Location = new System.Drawing.Point(802, 438);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(143, 32);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.cbBaudRate);
            this.panel2.Controls.Add(this.txtDatamanPassword);
            this.panel2.Controls.Add(this.cbComPort);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.cbCamDevice);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(19, 45);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(291, 249);
            this.panel2.TabIndex = 6;
            // 
            // cbBaudRate
            // 
            this.cbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBaudRate.FormattingEnabled = true;
            this.cbBaudRate.Items.AddRange(new object[] {
            "75",
            "110",
            "134",
            "300",
            "600",
            "1200",
            "1800",
            "2400",
            "4800",
            "7200",
            "9600",
            "14400",
            "19200",
            "38400"});
            this.cbBaudRate.Location = new System.Drawing.Point(122, 209);
            this.cbBaudRate.Name = "cbBaudRate";
            this.cbBaudRate.Size = new System.Drawing.Size(157, 29);
            this.cbBaudRate.TabIndex = 4;
            // 
            // txtDatamanPassword
            // 
            this.txtDatamanPassword.Location = new System.Drawing.Point(122, 89);
            this.txtDatamanPassword.Name = "txtDatamanPassword";
            this.txtDatamanPassword.Size = new System.Drawing.Size(157, 29);
            this.txtDatamanPassword.TabIndex = 4;
            this.txtDatamanPassword.WordWrap = false;
            // 
            // cbComPort
            // 
            this.cbComPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbComPort.FormattingEnabled = true;
            this.cbComPort.Location = new System.Drawing.Point(122, 168);
            this.cbComPort.Name = "cbComPort";
            this.cbComPort.Size = new System.Drawing.Size(157, 29);
            this.cbComPort.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 21);
            this.label5.TabIndex = 3;
            this.label5.Text = "Baud rate";
            this.toolTip1.SetToolTip(this.label5, "\r\nBaud rate");
            // 
            // cbCamDevice
            // 
            this.cbCamDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCamDevice.FormattingEnabled = true;
            this.cbCamDevice.Location = new System.Drawing.Point(122, 49);
            this.cbCamDevice.Name = "cbCamDevice";
            this.cbCamDevice.Size = new System.Drawing.Size(157, 29);
            this.cbCamDevice.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(10, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 21);
            this.label6.TabIndex = 0;
            this.label6.Text = "Scale";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "Password";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(28, 171);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 21);
            this.label7.TabIndex = 2;
            this.label7.Text = "Com port";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Camera";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Device";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(15, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 21);
            this.label4.TabIndex = 5;
            this.label4.Text = "Setting";
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipTitle = "Baud rate";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel3.Controls.Add(this.clBoxDvvcZone5);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.clBoxDvvcZone4);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.clBoxDvvcZone3);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.clBoxDvvcZone2);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.clBoxDvvcZone1);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.Zone001);
            this.panel3.Location = new System.Drawing.Point(333, 45);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(854, 387);
            this.panel3.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(10, 12);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 21);
            this.label12.TabIndex = 0;
            this.label12.Text = "Dvvc";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Zone001
            // 
            this.Zone001.AutoSize = true;
            this.Zone001.Location = new System.Drawing.Point(28, 52);
            this.Zone001.Name = "Zone001";
            this.Zone001.Size = new System.Drawing.Size(60, 21);
            this.Zone001.TabIndex = 2;
            this.Zone001.Text = "Cổng 1";
            // 
            // clBoxDvvcZone1
            // 
            this.clBoxDvvcZone1.FormattingEnabled = true;
            this.clBoxDvvcZone1.Items.AddRange(new object[] {
            "EMS (POST)",
            "Nhất Tín Logistics (POST)",
            "Shopee Express (POST)",
            "Aha Move",
            "DHL",
            "GiaoHangNhanh",
            "GiaoHangTietKiem",
            "Lazada",
            "Lazada AhaMove",
            "Lazada B2B VN",
            "Lazada Cainiao",
            "Lazada GHN",
            "Lazada GRAB",
            "Lazada I Logic VN",
            "Lazada J&amp;T VN",
            "Lazada LEX VN",
            "Lazada LGS",
            "Lazada Logisthai VN",
            "Lazada NinjavanVN",
            "Lazada PickmeeVN",
            "Lazada Ship60",
            "Lazada SOFP_VN",
            "Lazada Vinacapital",
            "Ninja VAN",
            "Shopee BEST Express",
            "Shopee Express",
            "Shopee Express Instant",
            "Shopee GHN",
            "Shopee GHTK",
            "Shopee J&T Express",
            "Shopee Now",
            "Shopee Viettel Post",
            "Shopee VNpost Nhanh",
            "Shopee VNPost Tiết Kiệm",
            "Tiktok BEST Express",
            "Tiktok GHTK",
            "Tiktok J&T Express",
            "Viettel",
            "Shopee Ninja Van"});
            this.clBoxDvvcZone1.Location = new System.Drawing.Point(103, 52);
            this.clBoxDvvcZone1.Name = "clBoxDvvcZone1";
            this.clBoxDvvcZone1.Size = new System.Drawing.Size(265, 76);
            this.clBoxDvvcZone1.TabIndex = 6;
            // 
            // clBoxDvvcZone2
            // 
            this.clBoxDvvcZone2.FormattingEnabled = true;
            this.clBoxDvvcZone2.Items.AddRange(new object[] {
            "EMS (POST)",
            "Nhất Tín Logistics (POST)",
            "Shopee Express (POST)",
            "Aha Move",
            "DHL",
            "GiaoHangNhanh",
            "GiaoHangTietKiem",
            "Lazada",
            "Lazada AhaMove",
            "Lazada B2B VN",
            "Lazada Cainiao",
            "Lazada GHN",
            "Lazada GRAB",
            "Lazada I Logic VN",
            "Lazada J&amp;T VN",
            "Lazada LEX VN",
            "Lazada LGS",
            "Lazada Logisthai VN",
            "Lazada NinjavanVN",
            "Lazada PickmeeVN",
            "Lazada Ship60",
            "Lazada SOFP_VN",
            "Lazada Vinacapital",
            "Ninja VAN",
            "Shopee BEST Express",
            "Shopee Express",
            "Shopee Express Instant",
            "Shopee GHN",
            "Shopee GHTK",
            "Shopee J&T Express",
            "Shopee Now",
            "Shopee Viettel Post",
            "Shopee VNpost Nhanh",
            "Shopee VNPost Tiết Kiệm",
            "Tiktok BEST Express",
            "Tiktok GHTK",
            "Tiktok J&T Express",
            "Viettel",
            "Shopee Ninja Van"});
            this.clBoxDvvcZone2.Location = new System.Drawing.Point(103, 156);
            this.clBoxDvvcZone2.Name = "clBoxDvvcZone2";
            this.clBoxDvvcZone2.Size = new System.Drawing.Size(265, 76);
            this.clBoxDvvcZone2.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(28, 156);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 21);
            this.label8.TabIndex = 7;
            this.label8.Text = "Cổng 2";
            // 
            // clBoxDvvcZone3
            // 
            this.clBoxDvvcZone3.FormattingEnabled = true;
            this.clBoxDvvcZone3.Items.AddRange(new object[] {
            "EMS (POST)",
            "Nhất Tín Logistics (POST)",
            "Shopee Express (POST)",
            "Aha Move",
            "DHL",
            "GiaoHangNhanh",
            "GiaoHangTietKiem",
            "Lazada",
            "Lazada AhaMove",
            "Lazada B2B VN",
            "Lazada Cainiao",
            "Lazada GHN",
            "Lazada GRAB",
            "Lazada I Logic VN",
            "Lazada J&amp;T VN",
            "Lazada LEX VN",
            "Lazada LGS",
            "Lazada Logisthai VN",
            "Lazada NinjavanVN",
            "Lazada PickmeeVN",
            "Lazada Ship60",
            "Lazada SOFP_VN",
            "Lazada Vinacapital",
            "Ninja VAN",
            "Shopee BEST Express",
            "Shopee Express",
            "Shopee Express Instant",
            "Shopee GHN",
            "Shopee GHTK",
            "Shopee J&T Express",
            "Shopee Now",
            "Shopee Viettel Post",
            "Shopee VNpost Nhanh",
            "Shopee VNPost Tiết Kiệm",
            "Tiktok BEST Express",
            "Tiktok GHTK",
            "Tiktok J&T Express",
            "Viettel",
            "Shopee Ninja Van"});
            this.clBoxDvvcZone3.Location = new System.Drawing.Point(103, 263);
            this.clBoxDvvcZone3.Name = "clBoxDvvcZone3";
            this.clBoxDvvcZone3.Size = new System.Drawing.Size(265, 76);
            this.clBoxDvvcZone3.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(28, 263);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 21);
            this.label9.TabIndex = 9;
            this.label9.Text = "Cổng 3";
            // 
            // clBoxDvvcZone4
            // 
            this.clBoxDvvcZone4.FormattingEnabled = true;
            this.clBoxDvvcZone4.Items.AddRange(new object[] {
            "EMS (POST)",
            "Nhất Tín Logistics (POST)",
            "Shopee Express (POST)",
            "Aha Move",
            "DHL",
            "GiaoHangNhanh",
            "GiaoHangTietKiem",
            "Lazada",
            "Lazada AhaMove",
            "Lazada B2B VN",
            "Lazada Cainiao",
            "Lazada GHN",
            "Lazada GRAB",
            "Lazada I Logic VN",
            "Lazada J&amp;T VN",
            "Lazada LEX VN",
            "Lazada LGS",
            "Lazada Logisthai VN",
            "Lazada NinjavanVN",
            "Lazada PickmeeVN",
            "Lazada Ship60",
            "Lazada SOFP_VN",
            "Lazada Vinacapital",
            "Ninja VAN",
            "Shopee BEST Express",
            "Shopee Express",
            "Shopee Express Instant",
            "Shopee GHN",
            "Shopee GHTK",
            "Shopee J&T Express",
            "Shopee Now",
            "Shopee Viettel Post",
            "Shopee VNpost Nhanh",
            "Shopee VNPost Tiết Kiệm",
            "Tiktok BEST Express",
            "Tiktok GHTK",
            "Tiktok J&T Express",
            "Viettel",
            "Shopee Ninja Van"});
            this.clBoxDvvcZone4.Location = new System.Drawing.Point(499, 52);
            this.clBoxDvvcZone4.Name = "clBoxDvvcZone4";
            this.clBoxDvvcZone4.Size = new System.Drawing.Size(265, 76);
            this.clBoxDvvcZone4.TabIndex = 12;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(424, 52);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 21);
            this.label10.TabIndex = 11;
            this.label10.Text = "Cổng 4";
            // 
            // clBoxDvvcZone5
            // 
            this.clBoxDvvcZone5.FormattingEnabled = true;
            this.clBoxDvvcZone5.Items.AddRange(new object[] {
            "EMS (POST)",
            "Nhất Tín Logistics (POST)",
            "Shopee Express (POST)",
            "Aha Move",
            "DHL",
            "GiaoHangNhanh",
            "GiaoHangTietKiem",
            "Lazada",
            "Lazada AhaMove",
            "Lazada B2B VN",
            "Lazada Cainiao",
            "Lazada GHN",
            "Lazada GRAB",
            "Lazada I Logic VN",
            "Lazada J&amp;T VN",
            "Lazada LEX VN",
            "Lazada LGS",
            "Lazada Logisthai VN",
            "Lazada NinjavanVN",
            "Lazada PickmeeVN",
            "Lazada Ship60",
            "Lazada SOFP_VN",
            "Lazada Vinacapital",
            "Ninja VAN",
            "Shopee BEST Express",
            "Shopee Express",
            "Shopee Express Instant",
            "Shopee GHN",
            "Shopee GHTK",
            "Shopee J&T Express",
            "Shopee Now",
            "Shopee Viettel Post",
            "Shopee VNpost Nhanh",
            "Shopee VNPost Tiết Kiệm",
            "Tiktok BEST Express",
            "Tiktok GHTK",
            "Tiktok J&T Express",
            "Viettel",
            "Shopee Ninja Van"});
            this.clBoxDvvcZone5.Location = new System.Drawing.Point(499, 156);
            this.clBoxDvvcZone5.Name = "clBoxDvvcZone5";
            this.clBoxDvvcZone5.Size = new System.Drawing.Size(265, 76);
            this.clBoxDvvcZone5.TabIndex = 14;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(424, 156);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 21);
            this.label11.TabIndex = 13;
            this.label11.Text = "Cổng 5";
            // 
            // FrmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1226, 510);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmSetting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSetting_FormClosing);
            this.Load += new System.EventHandler(this.FrmSetting_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbCamDevice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDatamanPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbComPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cbBaudRate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label Zone001;
        private System.Windows.Forms.CheckedListBox clBoxDvvcZone2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckedListBox clBoxDvvcZone1;
        private System.Windows.Forms.CheckedListBox clBoxDvvcZone5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckedListBox clBoxDvvcZone4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckedListBox clBoxDvvcZone3;
        private System.Windows.Forms.Label label9;
    }
}
namespace Intech_software.GUI
{
    partial class frmDvvc
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.picResultImage = new System.Windows.Forms.PictureBox();
            this.lbReadString = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.uiDataScannedOrder = new Sunny.UI.UIDataGridView();
            this.uiDataTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrackingNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShippingUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.listBoxLog = new Sunny.UI.UIListBox();
            this.timerCheckPlc = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picResultImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiDataScannedOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // picResultImage
            // 
            this.picResultImage.Location = new System.Drawing.Point(853, 35);
            this.picResultImage.Name = "picResultImage";
            this.picResultImage.Size = new System.Drawing.Size(416, 265);
            this.picResultImage.TabIndex = 1;
            this.picResultImage.TabStop = false;
            // 
            // lbReadString
            // 
            this.lbReadString.AutoSize = true;
            this.lbReadString.Location = new System.Drawing.Point(853, 303);
            this.lbReadString.Name = "lbReadString";
            this.lbReadString.Size = new System.Drawing.Size(59, 13);
            this.lbReadString.TabIndex = 2;
            this.lbReadString.Text = "labelResult";
            this.lbReadString.Click += new System.EventHandler(this.lbReadString_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.BackColor = System.Drawing.SystemColors.Control;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Image = global::Intech_software.Properties.Resources.Stop_icon;
            this.btnStop.Location = new System.Drawing.Point(1063, 422);
            this.btnStop.Margin = new System.Windows.Forms.Padding(2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 32);
            this.btnStop.TabIndex = 35;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.BackColor = System.Drawing.SystemColors.Control;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.Black;
            this.btnStart.Image = global::Intech_software.Properties.Resources.Start_icon;
            this.btnStart.Location = new System.Drawing.Point(949, 422);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 32);
            this.btnStart.TabIndex = 34;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // uiDataScannedOrder
            // 
            this.uiDataScannedOrder.AllowUserToAddRows = false;
            this.uiDataScannedOrder.AllowUserToDeleteRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiDataScannedOrder.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.uiDataScannedOrder.BackgroundColor = System.Drawing.Color.White;
            this.uiDataScannedOrder.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.uiDataScannedOrder.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.uiDataScannedOrder.ColumnHeadersHeight = 32;
            this.uiDataScannedOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.uiDataScannedOrder.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.uiDataTime,
            this.TrackingNumber,
            this.ShippingUnit});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.uiDataScannedOrder.DefaultCellStyle = dataGridViewCellStyle8;
            this.uiDataScannedOrder.EnableHeadersVisualStyles = false;
            this.uiDataScannedOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiDataScannedOrder.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.uiDataScannedOrder.Location = new System.Drawing.Point(0, 35);
            this.uiDataScannedOrder.Name = "uiDataScannedOrder";
            this.uiDataScannedOrder.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.uiDataScannedOrder.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiDataScannedOrder.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.uiDataScannedOrder.SelectedIndex = -1;
            this.uiDataScannedOrder.Size = new System.Drawing.Size(847, 419);
            this.uiDataScannedOrder.StripeOddColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiDataScannedOrder.TabIndex = 36;
            // 
            // uiDataTime
            // 
            this.uiDataTime.HeaderText = "Thời gian";
            this.uiDataTime.Name = "uiDataTime";
            this.uiDataTime.ReadOnly = true;
            this.uiDataTime.Width = 200;
            // 
            // TrackingNumber
            // 
            this.TrackingNumber.HeaderText = "Mã vận đơn";
            this.TrackingNumber.Name = "TrackingNumber";
            this.TrackingNumber.ReadOnly = true;
            this.TrackingNumber.Width = 200;
            // 
            // ShippingUnit
            // 
            this.ShippingUnit.HeaderText = "Đơn vị vận chuyển";
            this.ShippingUnit.Name = "ShippingUnit";
            this.ShippingUnit.ReadOnly = true;
            this.ShippingUnit.Width = 400;
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel1.Location = new System.Drawing.Point(549, 9);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(204, 23);
            this.uiLabel1.TabIndex = 37;
            this.uiLabel1.Text = "Chia đơn vị vận chuyển";
            // 
            // listBoxLog
            // 
            this.listBoxLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listBoxLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.listBoxLog.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.listBoxLog.ItemSelectForeColor = System.Drawing.Color.White;
            this.listBoxLog.Location = new System.Drawing.Point(0, 460);
            this.listBoxLog.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBoxLog.MinimumSize = new System.Drawing.Size(1, 1);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Padding = new System.Windows.Forms.Padding(2);
            this.listBoxLog.ShowText = false;
            this.listBoxLog.Size = new System.Drawing.Size(1289, 140);
            this.listBoxLog.TabIndex = 38;
            this.listBoxLog.Text = "listBoxLog";
            // 
            // timerCheckPlc
            // 
            this.timerCheckPlc.Enabled = true;
            this.timerCheckPlc.Interval = 1000;
            this.timerCheckPlc.Tick += new System.EventHandler(this.timerCheckPlc_Tick);
            // 
            // frmDvvc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1289, 600);
            this.Controls.Add(this.listBoxLog);
            this.Controls.Add(this.uiLabel1);
            this.Controls.Add(this.uiDataScannedOrder);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lbReadString);
            this.Controls.Add(this.picResultImage);
            this.Name = "frmDvvc";
            this.Text = "frmDvvc";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDvvc_FormClosing);
            this.Load += new System.EventHandler(this.frmDvvc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picResultImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiDataScannedOrder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.PictureBox picResultImage;
        private System.Windows.Forms.Label lbReadString;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private Sunny.UI.UIDataGridView uiDataScannedOrder;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIListBox listBoxLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn uiDataTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrackingNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShippingUnit;
        private System.Windows.Forms.Timer timerCheckPlc;
    }
}
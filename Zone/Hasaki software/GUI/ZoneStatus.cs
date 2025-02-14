using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intech_software.GUI
{
    public partial class ZoneStatus : Form
    {
        private int Start_X_C1 = 10;
        private int Start_Y_C1 = 100;
        private int Start_X_C2 = 10;
        private int Start_Y_C2 = 350;
        private int Scale = 12;

        List<VerticalZone> verticalZonePanels = null;
        List<HorizontalZone> horizontalZonePanels = null;
        Timer tmrShowStatusOfZone = null;

        bool[] ZoneStatusReceived = null;
        bool[] PopupError = null;
        bool[] PopupErrorActived = null;

        public ZoneStatus()
        {
            InitializeComponent();
            InitialControls();

            ZoneStatusReceived = new bool[41];
            PopupError = new bool[40];
            PopupErrorActived = new bool[40];

            tmrShowStatusOfZone = new System.Windows.Forms.Timer();
            tmrShowStatusOfZone.Interval = 500;
            tmrShowStatusOfZone.Tick += TmrShowStatusOfZone_Tick;
            tmrShowStatusOfZone.Start();
        }

        private void TmrShowStatusOfZone_Tick(object sender, EventArgs e)
        {
            DisplayZone();
        }

        private void InitialControls()
        {
            verticalZonePanels = new List<VerticalZone>();
            horizontalZonePanels = new List<HorizontalZone>();

            int step = 768 / Scale + 432 / Scale;
            for (int i = 0; i < 5; i++) //L1...L5
            {
                VerticalZone verticalZone = new VerticalZone();
                verticalZone.Location = new Point(Start_X_C1 + 1225 / Scale + step * i, Start_Y_C1 - 1000 / Scale);
                verticalZone.LbZoneName.Text = "Cửa L" + (i + 1);
                this.Controls.Add(verticalZone);
                verticalZonePanels.Add(verticalZone);
            }
            for (int i = 20; i < 25; i++) //R1...R5
            {
                VerticalZone verticalZone = new VerticalZone();
                verticalZone.Location = new Point(Start_X_C1 + 1225 / Scale + step * (i - 20), Start_Y_C1 + 700 / Scale);
                verticalZone.LbZoneName.Text = "Cửa R" + (i - 19);
                this.Controls.Add(verticalZone);
                verticalZonePanels.Add(verticalZone);

            }


            for (int i = 5; i < 10; i++) //L6...L10
            {
                VerticalZone verticalZone = new VerticalZone();
                verticalZone.Location = new Point(Start_X_C1 + 1225 / Scale + step * i, Start_Y_C1 - 1000 / Scale);
                verticalZone.LbZoneName.Text = "Cửa L" + (i + 1);
                this.Controls.Add(verticalZone);
                verticalZonePanels.Add(verticalZone);
            }
            for (int i = 25; i < 30; i++) //R6...R10
            {
                VerticalZone verticalZone = new VerticalZone();
                verticalZone.Location = new Point(Start_X_C1 + 1225 / Scale + step * (i - 20), Start_Y_C1 + 700 / Scale);
                verticalZone.LbZoneName.Text = "Cửa R" + (i - 19);
                this.Controls.Add(verticalZone);
                verticalZonePanels.Add(verticalZone);

            }




            for (int i = 10; i < 15; i++) //L11..L15
            {
                VerticalZone verticalZone = new VerticalZone();
                verticalZone.Location = new Point(Start_X_C2 + 1225 / Scale + step * (i - 10), Start_Y_C2 - 1000 / Scale);
                verticalZone.LbZoneName.Text = "Cửa L" + (i + 1);
                this.Controls.Add(verticalZone);
                verticalZonePanels.Add(verticalZone);

            }
            for (int i = 30; i < 35; i++) //R11...R15
            {
                VerticalZone verticalZone = new VerticalZone();
                verticalZone.Location = new Point(Start_X_C2 + 1225 / Scale + step * (i - 30), Start_Y_C2 + 700 / Scale);
                verticalZone.LbZoneName.Text = "Cửa R" + (i - 19);
                this.Controls.Add(verticalZone);
                verticalZonePanels.Add(verticalZone);
            }



            for (int i = 15; i < 20; i++) //L16..L20
            {
                VerticalZone verticalZone = new VerticalZone();
                verticalZone.Location = new Point(Start_X_C2 + 1225 / Scale + step * (i - 10), Start_Y_C2 - 1000 / Scale);
                verticalZone.LbZoneName.Text = "Cửa L" + (i + 1);
                this.Controls.Add(verticalZone);
                verticalZonePanels.Add(verticalZone);

            }
            for (int i = 35; i < 40; i++) //R16...R20
            {
                VerticalZone verticalZone = new VerticalZone();
                verticalZone.Location = new Point(Start_X_C2 + 1225 / Scale + step * (i - 30), Start_Y_C2 + 700 / Scale);
                verticalZone.LbZoneName.Text = "Cửa R" + (i - 19);
                this.Controls.Add(verticalZone);
                verticalZonePanels.Add(verticalZone);
            }


            HorizontalZone horizontalZone = new HorizontalZone();
            horizontalZone.Location = new Point(Start_X_C2 + 13500 / Scale, Start_Y_C2 - 4);
            horizontalZone.LbZoneName.Text = "Cửa NG";
            this.Controls.Add(horizontalZone);
            horizontalZonePanels.Add(horizontalZone);
        }

        /// <summary>
        /// Hàm để hiển thị trạng thái của các cửa lên giao diện
        /// </summary>
        int numOfZone = 40;
        private void DisplayZone()
        {
            this.ZoneStatusReceived = frmInBound.plc.ReadZoneStatus(12242);
            this.ZoneStatusReceived = frmInBound.plc.ReadZoneStatus(11992);
            for (int i = 0; i <= numOfZone; i++)
            {
                if (i < numOfZone)
                {
                    if (this.ZoneStatusReceived[i])
                    {
                        verticalZonePanels[i].BackColor = Color.DarkOrange;
                    }
                    else
                    {
                        verticalZonePanels[i].BackColor = SystemColors.ButtonHighlight;
                    }

                    if (this.PopupError[i] == true && this.PopupErrorActived[i] == false)
                    {
                        this.PopupErrorActived[i] = true;
                        Debug.WriteLine(string.Format("{0} {1} Popup error", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), verticalZonePanels[i].LbZoneName));
                    }
                    else if (this.PopupError[i] == false)
                    {
                        this.PopupErrorActived[i] = false;
                    }

                }
                else if (i == numOfZone)
                {
                    if (this.ZoneStatusReceived[i])
                    {
                        horizontalZonePanels[0].BackColor = Color.DarkOrange;
                    }
                    else
                    {
                        horizontalZonePanels[0].BackColor = SystemColors.ButtonHighlight;
                    }
                }
            }
        }
    }
}

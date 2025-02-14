using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intech_software.GUI
{
    public class HorizontalZone : Panel
    {
        public int Length = 1000;
        public int Width = 768;
        public int Scale = 12;

        public Label LbZoneName { get; set; }

        public HorizontalZone()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Size = new System.Drawing.Size(Length / Scale, Width / Scale);
            this.BackColor = SystemColors.ButtonHighlight;
            this.BorderStyle = BorderStyle.FixedSingle;

            this.LbZoneName = new Label();
            LbZoneName.Font = new Font("Arial", 9, FontStyle.Regular);
            LbZoneName.Text = "Zone L20";
            LbZoneName.Dock = DockStyle.Fill;
            LbZoneName.BackColor = Color.Transparent;
            LbZoneName.AutoSize = false;
            LbZoneName.TextAlign = ContentAlignment.MiddleCenter;
            LbZoneName.Location = new Point(2, 2);
            this.Controls.Add(LbZoneName);
        }
    }
}

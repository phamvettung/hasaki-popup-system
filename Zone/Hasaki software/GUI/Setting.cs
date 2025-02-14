using Cognex.DataMan.SDK.Discovery;
using Intech_software.Constants;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Intech_software.GUI
{
    public partial class FrmSetting : Form
    {
        [Browsable(true)]
        public Color _BackColor { get; set; }
        private EthSystemDiscoverer _ethSystemDiscoverer = null;
        private SynchronizationContext syncContext = null;
        public FrmSetting()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Settings.PortName = cbComPort.SelectedIndex >= 0 ? cbComPort.SelectedItem.ToString() : "COM3";
            Settings.BaudRate = Convert.ToInt32(cbBaudRate.SelectedItem);
            if (cbCamDevice.Items.Count > 0)
            {
                Settings.CameraDevice = cbCamDevice.SelectedItem as EthSystemDiscoverer.SystemInfo;
            }
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmSetting_Load(object sender, EventArgs e)
        {
            cbBaudRate.SelectedIndex = 12;
            _ethSystemDiscoverer = new EthSystemDiscoverer();
            _ethSystemDiscoverer.SystemDiscovered += new EthSystemDiscoverer.SystemDiscoveredHandler(OnEthSystemDiscovered);
            _ethSystemDiscoverer.Discover();
        }
        private void OnEthSystemDiscovered(EthSystemDiscoverer.SystemInfo systemInfo)
        {
            syncContext.Post(
                new SendOrPostCallback(
                    delegate
                    {
                        cbCamDevice.Items.Add(systemInfo);
                    }),
                    null);
        }

        private void FrmSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_ethSystemDiscoverer != null)
                _ethSystemDiscoverer.Dispose();
            _ethSystemDiscoverer = null;
        }
    }
}

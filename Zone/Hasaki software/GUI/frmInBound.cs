using Cognex.DataMan.SDK;
using Cognex.DataMan.SDK.Discovery;
using Cognex.DataMan.SDK.Utils;
using Intech_software.BUS;
using Intech_software.Constants;
using Intech_software.DTO;
using Intech_software.Kafka;
using Intech_software.Models;
using Intech_software.Models.WmsModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Intech_software.GUI
{
    public partial class frmInBound : Form
    {
        //Khởi tạo các lớp
        HasakiSystemBus hasakiSystemBus = new HasakiSystemBus();
        HasakiSystemDTO hasakiSystemDTO = new HasakiSystemDTO();
        InBoundsBus inBoundsBus = new InBoundsBus();
        public static string revAccountName = string.Empty;
        InboundState _state = new InboundState();
        InboundSelection _defaultSelection = new InboundSelection();

        //DataTable
        DataTable dtHasakiSystem;
        DataTable dtInBound;

        //DataMan
        private ResultCollector _results;
        EthSystemConnector myConn = null;
        private EthSystemDiscoverer _ethSystemDiscoverer = null;
        DataManSystem mySystem = null;
        private object _currentResultInfoSyncLock = new object();
        private ISystemConnector _connector = null;
        private SynchronizationContext syncContext = null;
        private bool _closing = false;
        private bool _autoconnect = false;
        private object _listAddItemLock = new object();
        private object _listHasakinowLock = new object();
        private GuiLogger _logger;
        int count = 0;
        int countNG = 0;
        string maKH = string.Empty;


        //Dimension
        private const int BUFFER_SIZE = 1024;
        private string IP_ADDRESS_DIM = "192.168.3.218";
        private const int PORT_NUMBER_DIM = 23;
        private static Socket client;
        private static byte[] data = new byte[BUFFER_SIZE];
        string strDimValue = string.Empty;
        string lengthValue = string.Empty;
        string widthValue = string.Empty;
        string heightValue = string.Empty;

        //PLC
        public static PLC plc = null;
        private string IP_ADDRESS_PLC = "192.168.3.250";
        private const int PORT_NUMBER_PLC = 24;
        string zone = string.Empty;
        string state = string.Empty;
        int readConveyorState; // doc trang thai chay dung
        int readInputRegister = 0;
        bool readSensor = false;
        bool checkConnectPLC = false;// kiem tra ket noi plc

        //Scale
        int byte11 = 0;
        int byte12 = 0;
        double weightValue = 0.0;

        // map to process packed label
        Dictionary<string, Inbound> scannedPkl = new Dictionary<string, Inbound>();

        // kafka producer
        private readonly Producer _kafkaProducer = new Producer();


        Queue<Dimension> dimensions = new Queue<Dimension>();
        Queue<Parcel> parcels = new Queue<Parcel>();
        Queue<double> weights = new Queue<double>();


        public frmInBound()
        {
            InitializeComponent();

            syncContext = SynchronizationContext.Current;
            cbLiveDisplay.CheckedChanged += new EventHandler(this.cbLiveDisplay_CheckedChanged);
            FormClosing += new FormClosingEventHandler(this.frmInBound_FormClosing);
            btnTrigger.MouseDown += new MouseEventHandler(this.btnTrigger_MouseDown);
            btnTrigger.MouseUp += new MouseEventHandler(this.btnTrigger_MouseUp);

            timerDateTime.Start();

            DefaulSetting();

            InitializeCamera();
            RegisterEvents();
        }
        private void frmInBound_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'masterDataSet.User' table. You can move, or remove it, as needed.
            this.userTableAdapter.Fill(this.masterDataSet.User);
            try
            {
                txtQuantity.Text = count.ToString();
                txtNG.Text = countNG.ToString() + "/" + count.ToString();
                RefreshDgvInBound();

                #region default selection
                _defaultSelection.LoadSelection(Constants.Constants.DEFAULT_ZONE_SELECTION_FILE);
                cbSelectZone0.SelectedIndex = cbSelectZone0.FindStringExact(_defaultSelection.Zone0Selection) == -1 ? 0 : cbSelectZone0.FindStringExact(_defaultSelection.Zone0Selection);
                cbSelectZone1.SelectedIndex = cbSelectZone1.FindStringExact(_defaultSelection.Zone1Selection) == -1 ? 0 : cbSelectZone1.FindStringExact(_defaultSelection.Zone1Selection);
                cbSelectZone2.SelectedIndex = cbSelectZone2.FindStringExact(_defaultSelection.Zone2Selection) == -1 ? 0 : cbSelectZone2.FindStringExact(_defaultSelection.Zone2Selection);
                cbSelectZone3.SelectedIndex = cbSelectZone3.FindStringExact(_defaultSelection.Zone3Selection) == -1 ? 0 : cbSelectZone3.FindStringExact(_defaultSelection.Zone3Selection);
                cbSelectZone4.SelectedIndex = cbSelectZone4.FindStringExact(_defaultSelection.Zone4Selection) == -1 ? 0 : cbSelectZone4.FindStringExact(_defaultSelection.Zone4Selection);
                cbSelectZone5.SelectedIndex = cbSelectZone5.FindStringExact(_defaultSelection.Zone5Selection) == -1 ? 0 : cbSelectZone5.FindStringExact(_defaultSelection.Zone5Selection);
                cbSelectZone6.SelectedIndex = cbSelectZone6.FindStringExact(_defaultSelection.Zone6Selection) == -1 ? 0 : cbSelectZone6.FindStringExact(_defaultSelection.Zone6Selection);
                cbSelectZone7.SelectedIndex = cbSelectZone7.FindStringExact(_defaultSelection.Zone7Selection) == -1 ? 0 : cbSelectZone7.FindStringExact(_defaultSelection.Zone7Selection);
                cbSelectZone8.SelectedIndex = cbSelectZone8.FindStringExact(_defaultSelection.Zone8Selection) == -1 ? 0 : cbSelectZone8.FindStringExact(_defaultSelection.Zone8Selection);
                cbSelectZone9.SelectedIndex = cbSelectZone9.FindStringExact(_defaultSelection.Zone9Selection) == -1 ? 0 : cbSelectZone9.FindStringExact(_defaultSelection.Zone9Selection);
                cbSelectZoneError.SelectedIndex = cbSelectZoneError.FindStringExact(_defaultSelection.Zone99Selection) == -1 ? 0 : cbSelectZoneError.FindStringExact(_defaultSelection.Zone99Selection);
                cbSelectShop22.SelectedIndex = cbSelectShop22.FindStringExact(_defaultSelection.Shop22Selection) == -1 ? 0 : cbSelectShop22.FindStringExact(_defaultSelection.Shop22Selection);
                cbSelectZone1A.SelectedIndex = cbSelectZone1A.FindStringExact(_defaultSelection.Zone1ASelection) == -1 ? 0 : cbSelectZone1A.FindStringExact(_defaultSelection.Zone1ASelection);
                cbSelectZone2A.SelectedIndex = cbSelectZone2A.FindStringExact(_defaultSelection.Zone2ASelection) == -1 ? 0 : cbSelectZone2A.FindStringExact(_defaultSelection.Zone2ASelection);
                cbSelectZone3A.SelectedIndex = cbSelectZone3A.FindStringExact(_defaultSelection.Zone3ASelection) == -1 ? 0 : cbSelectZone3A.FindStringExact(_defaultSelection.Zone3ASelection);
                cbSelectZone4A.SelectedIndex = cbSelectZone4A.FindStringExact(_defaultSelection.Zone4ASelection) == -1 ? 0 : cbSelectZone4A.FindStringExact(_defaultSelection.Zone4ASelection);
                cbSelectZone5A.SelectedIndex = cbSelectZone5A.FindStringExact(_defaultSelection.Zone5ASelection) == -1 ? 0 : cbSelectZone5A.FindStringExact(_defaultSelection.Zone5ASelection);
                cbSelectZone6A.SelectedIndex = cbSelectZone6A.FindStringExact(_defaultSelection.Zone6ASelection) == -1 ? 0 : cbSelectZone6A.FindStringExact(_defaultSelection.Zone6ASelection);
                cbSelectZone7A.SelectedIndex = cbSelectZone7A.FindStringExact(_defaultSelection.Zone7ASelection) == -1 ? 0 : cbSelectZone7A.FindStringExact(_defaultSelection.Zone7ASelection);
                cbSelectZone8A.SelectedIndex = cbSelectZone8A.FindStringExact(_defaultSelection.Zone8ASelection) == -1 ? 0 : cbSelectZone8A.FindStringExact(_defaultSelection.Zone8ASelection);
                cbSelectZone9A.SelectedIndex = cbSelectZone9A.FindStringExact(_defaultSelection.Zone9ASelection) == -1 ? 0 : cbSelectZone9A.FindStringExact(_defaultSelection.Zone9ASelection);
                cbSelectZone6B.SelectedIndex = cbSelectZone6B.FindStringExact(_defaultSelection.Zone6BSelection) == -1 ? 0 : cbSelectZone6B.FindStringExact(_defaultSelection.Zone6BSelection);
                cbSelectZoneKT.SelectedIndex = cbSelectZoneKT.FindStringExact(_defaultSelection.ZoneKTSelection) == -1 ? 0 : cbSelectZoneKT.FindStringExact(_defaultSelection.ZoneKTSelection);
                cbSelectZoneGift.SelectedIndex = cbSelectZoneGift.FindStringExact(_defaultSelection.ZoneGiftSelection) == -1 ? 0 : cbSelectZoneGift.FindStringExact(_defaultSelection.ZoneGiftSelection);
                #endregion

                #region DataMan Connection

                _ethSystemDiscoverer = new EthSystemDiscoverer();
                _ethSystemDiscoverer.SystemDiscovered += new EthSystemDiscoverer.SystemDiscoveredHandler(OnEthSystemDiscovered);
                _ethSystemDiscoverer.Discover();
                RefreshGui();

                #endregion

                #region Dim Connection

                listBoxStatus.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " Dim: Connecting...");
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint iep = new IPEndPoint(IPAddress.Parse(IP_ADDRESS_DIM), PORT_NUMBER_DIM);
                //client.BeginConnect(iep, new AsyncCallback(Connected), client);

                #endregion

                #region PLC Connection

                plc = new PLC(IP_ADDRESS_PLC, PORT_NUMBER_PLC);
                Thread thConnect = new Thread(() =>
                {
                    ConnectPLC();
                })
                { IsBackground = true };
                thConnect.Start();
                timer1.Start();
                timer2.Start();

                #endregion

                #region Scale Connection                             

                serialPort1.PortName = Settings.PortName;
                serialPort1.BaudRate = Settings.BaudRate;
                serialPort1.DataBits = Settings.DataBits;
                serialPort1.StopBits = Settings.StopBit;
                serialPort1.Parity = Settings.Parity;

                if (serialPort1.IsOpen == false)
                {
                    try
                    {
                        serialPort1.Open();
                    }
                    catch
                    { }
                }
                if (serialPort1.IsOpen)
                {
                    progressBarScale.Value = 100;
                }
                else
                    Focus();
                #endregion

                ConnectToCamera();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmInBound_FormClosing(object sender, FormClosingEventArgs e)
        {
            _closing = true;
            _autoconnect = false;

            if (null != mySystem && mySystem.State == global::Cognex.DataMan.SDK.ConnectionState.Connected)
                mySystem.Disconnect();
            _ethSystemDiscoverer?.Dispose();
            _ethSystemDiscoverer = null;

            if (client != null && client.Connected == true)
                client.Close();
            if (plc != null)
                DisconnectPLC();
            if (serialPort1 != null && serialPort1.IsOpen)
                serialPort1.Close();

            client = null;
            plc = null;
            serialPort1 = null;

            //Application.Exit();
        }

        private void Results_ComplexResultCompleted(object sender, ComplexResult e)
        {
            syncContext.Post(
                delegate
                {
                    ShowResult(e);
                },
                null);
        }

        private void Results_SimpleResultDropped(object sender, SimpleResult e)
        {
            syncContext.Post(
                delegate
                {
                    ReportDroppedResult(e);
                },
                null);
        }

        private void ReportDroppedResult(SimpleResult result)
        {
            AddListItem(string.Format("Partial result dropped: {0}, id={1}", result.Id.Type.ToString(), result.Id.Id));
        }

        private void RefreshGui()
        {
            bool system_connected = mySystem != null && mySystem.State == global::Cognex.DataMan.SDK.ConnectionState.Connected;

            btnTrigger.Enabled = system_connected;
            cbLiveDisplay.Enabled = system_connected;
        }



        #region Dim Events
        void Connected(IAsyncResult iar)
        {
            try
            {
                client.EndConnect(iar);

                listBoxStatus.Invoke(new Action(() =>
                {
                    listBoxStatus.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " Dim: System is online: " + client.RemoteEndPoint.ToString());
                    progressBarDIM.Value = 100;
                }));

                Thread receive = new Thread(() =>
                {
                    ReceiveData();
                });
                receive.Start();
            }
            catch (Exception ex)
            {
                try
                {
                    listBoxStatus.Invoke(new Action(() =>
                    {
                        listBoxStatus.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " Dim: Error Connecting");
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        progressBarDIM.Value = 0;
                    }));
                }
                catch { }
            }
        }
        void ReceiveData()
        {
            try
            {
                int recv;

                while (true)
                {
                    recv = client.Receive(data);
                    strDimValue = Encoding.ASCII.GetString(data, 0, recv);
                    string[] arrListStr = strDimValue.Split(',');
                    if (strDimValue == "q")
                    {
                        break;
                    }

                    this.lengthValue = arrListStr[0];
                    this.widthValue = arrListStr[1];
                    this.heightValue = arrListStr[2];

                    //listBoxStatus.Invoke(new Action(() =>
                    //{
                    //    listBoxStatus.Items.Add(strDimValue);
                    //}));
                    txtLength.Invoke(new Action(() =>
                    {
                        txtLength.Text = arrListStr[0];
                    }));
                    txtWidth.Invoke(new Action(() =>
                    {
                        txtWidth.Text = arrListStr[1];
                    }));
                    txtHeight.Invoke(new Action(() =>
                    {
                        txtHeight.Text = arrListStr[2];
                    }));
                }
                strDimValue = "q";
                byte[] message = Encoding.ASCII.GetBytes(strDimValue);
                client.Close();
                listBoxStatus.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " Dim: Connection stopped");
                return;
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                try
                {
                    listBoxStatus.Invoke(new Action(() =>
                    {
                        listBoxStatus.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " - Socket Exception: " + ex.Message);
                    }));
                    progressBarDIM.Invoke(new Action(() =>
                    {
                        progressBarDIM.Value = 0;
                    }));

                }
                catch { }

            }
            catch (Exception ex)
            {
                try
                {
                    listBoxStatus.Invoke(new Action(() =>
                    {
                        listBoxStatus.Items.Add(DateTime.Now.ToString("HH:mm:ss") + "Socket Exception: " + ex.Message);
                    }));

                }
                catch { }

            }
        }
        #endregion


        #region PLC Events
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {

                string message = "Vui lòng nhấn thiết lập cân nặng về 0";
                string title = "Reset cân";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    if (plc != null)
                    {
                        plc.WriteSingleRegister(1002, 1);
                    }
                    else
                        MessageBox.Show("PLC not connected");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (plc != null)
                {
                    plc.WriteSingleRegister(1002, 2);
                }
                else
                    MessageBox.Show("PLC not connected");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void ConnectPLC()
        {
            try
            {
                checkConnectPLC = plc.Connect();
                if (checkConnectPLC)
                {
                    listBoxStatus.Invoke(new Action(() =>
                    {
                        listBoxStatus.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " PLC: Connected.");
                        progressBarPLC.Value = 100;
                    }));
                }
            }
            catch (EasyModbus.Exceptions.ConnectionException ex)
            {
                try
                {
                    listBoxStatus.Invoke(new Action(() =>
                    {
                        listBoxStatus.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " PLC: " + ex.Message);
                    }));
                }
                catch { }
            }
            catch (Exception ex)
            {
                try
                {
                    listBoxStatus.Invoke(new Action(() =>
                    {
                        listBoxStatus.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " PLC: " + ex.Message);
                    }));
                }
                catch { }
            }
        }
        void DisconnectPLC()
        {
            try
            {
                checkConnectPLC = plc.Disconnect();
                if (checkConnectPLC)
                {
                    listBoxStatus.Invoke(new Action(() =>
                    {
                        listBoxStatus.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " PLC: Disconnected.");
                        progressBarPLC.Value = 0;
                    }));
                }
            }
            catch (EasyModbus.Exceptions.ConnectionException ex)
            {
                listBoxStatus.Invoke(new Action(() =>
                {
                    listBoxStatus.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " PLC: " + ex.Message);
                }));
            }
            catch (Exception ex)
            {
                listBoxStatus.Invoke(new Action(() =>
                {
                    listBoxStatus.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " PLC: " + ex.Message);
                }));
            }
        }

        #endregion


        #region DataMan Events
        private void Autoconnect()
        {
            if (Settings.CameraDevice == null)
                return;
            _autoconnect = false;

            try
            {
                EthSystemDiscoverer.SystemInfo eth_system_info = Settings.CameraDevice;

                if (eth_system_info.IPAddress.ToString() != "192.168.3.90")
                {
                    return;
                }

                myConn = new EthSystemConnector(eth_system_info.IPAddress, eth_system_info.Port);

                _connector = myConn;

                myConn.UserName = Settings.CameraUsername;
                myConn.Password = Settings.CameraPassword;


                if (mySystem != null && mySystem.State == global::Cognex.DataMan.SDK.ConnectionState.Connected)
                {
                    return;
                    mySystem.Disconnect();
                    mySystem = null;
                }
                mySystem = new DataManSystem(_connector)
                {
                    DefaultTimeout = 5000
                };

                // Subscribe to events that are signalled when the system is connected / disconnected.
                mySystem.SystemConnected += new SystemConnectedHandler(OnSystemConnected);
                mySystem.SystemDisconnected += new SystemDisconnectedHandler(OnSystemDisconnected);
                mySystem.SystemWentOnline += new SystemWentOnlineHandler(OnSystemWentOnline);
                mySystem.SystemWentOffline += new SystemWentOfflineHandler(OnSystemWentOffline);
                mySystem.KeepAliveResponseMissed += new KeepAliveResponseMissedHandler(OnKeepAliveResponseMissed);
                mySystem.BinaryDataTransferProgress += new BinaryDataTransferProgressHandler(OnBinaryDataTransferProgress);
                mySystem.OffProtocolByteReceived += new OffProtocolByteReceivedHandler(OffProtocolByteReceived);
                mySystem.AutomaticResponseArrived += new AutomaticResponseArrivedHandler(AutomaticResponseArrived);

                ResultTypes requested_result_types = ResultTypes.ReadXml | ResultTypes.Image | ResultTypes.ImageGraphics;
                _results = new ResultCollector(mySystem, requested_result_types);
                _results.ComplexResultCompleted += Results_ComplexResultCompleted;
                _results.SimpleResultDropped += Results_SimpleResultDropped;


                mySystem.Connect();
                try
                {
                    mySystem.SetResultTypes(requested_result_types);
                }
                catch
                { }

                if (mySystem.State == global::Cognex.DataMan.SDK.ConnectionState.Connected)
                {
                    //MessageBox.Show("DataMan: Connected", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AddListItem(DateTime.Now.ToString("HH:mm:ss") + " DataMan: Connected: ip:  " + eth_system_info.IPAddress);
                    progressBarDataMan.Value = 100;
                }
            }
            catch (Exception ex)
            {
                CleanupConnection();
                progressBarDataMan.Value = 0;
                AddListItem(DateTime.Now.ToString("HH:mm:ss") + " DataMan: Failed to connect: " + ex.ToString());
            }
        }

        //private void listBoxDetectedSystems_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (listBoxDetectedSystems.SelectedIndex != -1 && listBoxDetectedSystems.Items.Count > listBoxDetectedSystems.SelectedIndex)
        //    {
        //        var system_info = listBoxDetectedSystems.Items[listBoxDetectedSystems.SelectedIndex];

        //        if (system_info is EthSystemDiscoverer.SystemInfo)
        //        {
        //            EthSystemDiscoverer.SystemInfo eth_system_info = system_info as EthSystemDiscoverer.SystemInfo;

        //            txtDeviceIP.Text = eth_system_info.IPAddress.ToString();
        //        }
        //    }
        //    RefreshGui();
        //}

        private void btnTrigger_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                mySystem.SendCommand("TRIGGER OFF");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to send TRIGGER OFF command: " + ex.ToString(), "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTrigger_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                mySystem.SendCommand("TRIGGER ON");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to send TRIGGER ON command: " + ex.ToString(), "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbLiveDisplay_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbLiveDisplay.Checked)
                {
                    btnTrigger.Enabled = false;

                    mySystem.SendCommand("SET LIVEIMG.MODE 2");
                    mySystem.BeginGetLiveImage(
                        ImageFormat.jpeg,
                        ImageSize.Sixteenth,
                        ImageQuality.Medium,
                        OnLiveImageArrived,
                        null);

                }
                else
                {
                    btnTrigger.Enabled = true;
                    mySystem.SendCommand("SET LIVEIMG.MODE 0");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to set live image mode: " + ex.ToString(), "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


        #region Scale Events        

        int baudRate;
        string parity;
        uint dataBit;
        string stopBit;
        public void DefaulSetting()
        {
            try
            {
                //foreach (var item in SerialPort.GetPortNames())
                //{
                //    cboComPort.Items.Add(item);
                //}
                //cboComPort.SelectedIndex = 0;

                //int[] baudRateArr = new int[] { 75, 110, 134, 300, 600, 1200, 1800, 2400, 4800, 7200, 9600, 14400, 19200, 38400 };
                //foreach (var item in baudRateArr)
                //{
                //    cboBaudRate.Items.Add(item);
                //}
                //cboBaudRate.Text = baudRateArr[12].ToString();
                //baudRate = baudRateArr[12];
                baudRate = Settings.BaudRate;

                string[] parityArr = new string[] { "Even", "Odd", "None", "Mark", "Space" };
                //foreach (var item in parityArr)
                //{
                //    cboParityBits.Items.Add(item);
                //}
                //cboParityBits.Text = parityArr[2];
                parity = parityArr[2];

                uint[] dataBitArr = new uint[] { 4, 5, 6, 7, 8, 9 };
                //foreach (var item in dataBitArr)
                //{
                //    cboDataBits.Items.Add(item);
                //}
                //cboDataBits.Text = dataBitArr[4].ToString();
                dataBit = dataBitArr[4];

                string[] stopBitArr = new string[] { "One", "Two" };
                //foreach (var item in stopBitArr)
                //{
                //    cboStopBits.Items.Add(item);
                //}
                //cboStopBits.Text = stopBitArr[0];
                stopBit = stopBitArr[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


            List<string> gateSelection = new List<string>
            {
               "Cổng R1",
                "Cổng R2",
                "Cổng R3",
                "Cổng R4",
                "Cổng R5",
                "Cổng R6",
                "Cổng R7",
                "Cổng R8",
                "Cổng R9",
                "Cổng R10",
                "Cổng R11",
                "Cổng R12",
                "Cổng R13",
                "Cổng R14",
                "Cổng R15",
                "Cổng R16",
                "Cổng R17",
                "Cổng R18",
                "Cổng R19",
                "Cổng R20",
                "Cổng L1",
                "Cổng L2",
                "Cổng L3",
                "Cổng L4",
                "Cổng L5",
                "Cổng L6",
                "Cổng L7",
                "Cổng L8",
                "Cổng L9",
                "Cổng L10",
                "Cổng L11",
                "Cổng L12",
                "Cổng L13",
                "Cổng L14",
                "Cổng L15",
                "Cổng L16",
                "Cổng L17",
                "Cổng L18",
                "Cổng L19",
                "Cổng L20",
                "Cổng Lỗi"
            };
            cbSelectZone0.DataSource = gateSelection.ToArray();
            cbSelectZone1.DataSource = gateSelection.ToArray();
            cbSelectZone2.DataSource = gateSelection.ToArray();
            cbSelectZone3.DataSource = gateSelection.ToArray();
            cbSelectZone4.DataSource = gateSelection.ToArray();
            cbSelectZone5.DataSource = gateSelection.ToArray();
            cbSelectZone6.DataSource = gateSelection.ToArray();
            cbSelectZone7.DataSource = gateSelection.ToArray();
            cbSelectZone8.DataSource = gateSelection.ToArray();
            cbSelectZone9.DataSource = gateSelection.ToArray();
            cbSelectShop22.DataSource = gateSelection.ToArray();
            cbSelectZoneError.DataSource = gateSelection.ToArray();
            cbSelectZone1A.DataSource = gateSelection.ToArray();
            cbSelectZone2A.DataSource = gateSelection.ToArray();
            cbSelectZone3A.DataSource = gateSelection.ToArray();
            cbSelectZone4A.DataSource = gateSelection.ToArray();
            cbSelectZone5A.DataSource = gateSelection.ToArray();
            cbSelectZone6A.DataSource = gateSelection.ToArray();
            cbSelectZone7A.DataSource = gateSelection.ToArray();
            cbSelectZone8A.DataSource = gateSelection.ToArray();
            cbSelectZone9A.DataSource = gateSelection.ToArray();
            cbSelectZone6B.DataSource = gateSelection.ToArray();
            cbSelectZoneKT.DataSource = gateSelection.ToArray();
            cbSelectZoneGift.DataSource = gateSelection.ToArray();
        }

        //private void btnOpen_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (serialPort1.IsOpen == true)
        //        {
        //            serialPort1.Close();
        //        }
        //        else
        //        {
        //            //serialPort1.PortName = cboComPort.Text;
        //            //serialPort1.BaudRate = Convert.ToInt32(cboBaudRate.Text);
        //            //serialPort1.DataBits = Convert.ToInt32(cboDataBits.Text);
        //            //serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cboStopBits.Text);
        //            //serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cboParityBits.Text);

        //            serialPort1.PortName = cboComPort.Text;
        //            serialPort1.BaudRate = Convert.ToInt32(cboBaudRate.Text);
        //            serialPort1.DataBits = Convert.ToInt32(dataBit);
        //            serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopBit);
        //            serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), parity);
        //            serialPort1.Open();
        //            btnOpen.Enabled = false;
        //            btnClose.Enabled = true;
        //            progressBarScale.Value = 100;
        //        }

        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    catch (UnauthorizedAccessException ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    catch (IOException ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void btnClose_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (serialPort1.IsOpen)
        //        {
        //            serialPort1.Close();
        //            btnOpen.Enabled = true;
        //            btnClose.Enabled = false;
        //            progressBarScale.Value = 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(50);
            try
            {
                int bytes = serialPort1.BytesToRead;
                byte[] buffer = new byte[bytes];
                if (serialPort1.BytesToRead > 1)
                {
                    serialPort1.Read(buffer, 0, bytes);
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        if (i == 11)
                        {
                            byte11 = buffer[i];
                        }
                        if (i == 12)
                        {
                            byte12 = buffer[i];
                        }
                        this.weightValue = ((double)byte11 * 256 + (double)byte12) / 100;
                    }
                    this.Invoke(new EventHandler(displayData_event));

                }
            }
            catch (Exception ex)
            {
                listBoxStatus.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " - Scale: " + ex.Message);
            }
        }
        private void displayData_event(object sender, EventArgs e)
        {
            //txtWeight.Text = weightValue.ToString();
        }
        #endregion


        private void Log(string function, string message)
        {
            if (_logger != null)
                _logger.Log(function, message);
        }

        #region Device Discovery Events
        private void OnEthSystemDiscovered(EthSystemDiscoverer.SystemInfo systemInfo)
        {
            syncContext.Post(
                new SendOrPostCallback(
                    delegate
                    {
                        Settings.CameraDevice = systemInfo;
                        Autoconnect();
                    }),
                    null);
        }
        #endregion   

        #region Device Events

        private void OnSystemConnected(object sender, EventArgs args)
        {
            syncContext.Post(
                delegate
                {
                    AddListItem("System connected");
                    RefreshGui();
                },
                null);
        }

        private void OnSystemDisconnected(object sender, EventArgs args)
        {
            syncContext.Post(
                delegate
                {
                    AddListItem("System disconnected");
                    bool reset_gui = false;

                    if (!_closing && _autoconnect && false)
                    {
                        frmReconnecting frm = new frmReconnecting(this, mySystem);

                        if (frm.ShowDialog() == DialogResult.Cancel)
                            reset_gui = true;
                    }
                    else
                    {
                        reset_gui = true;
                    }

                    if (reset_gui)
                    {
                        btnTrigger.Enabled = false;
                        cbLiveDisplay.Enabled = false;

                        picResultImage.Image = null;
                        lbReadString.Text = "";
                    }
                },
                null);
        }

        private void OnKeepAliveResponseMissed(object sender, EventArgs args)
        {
            syncContext.Post(
                delegate
                {
                    AddListItem("Keep-alive response missed");
                },
                null);
        }

        private void OnSystemWentOnline(object sender, EventArgs args)
        {
            syncContext.Post(
                delegate
                {
                    AddListItem("System went online");
                },
                null);
        }

        private void OnSystemWentOffline(object sender, EventArgs args)
        {
            syncContext.Post(
                delegate
                {
                    AddListItem("System went offline");
                },
                null);
        }

        private void OnBinaryDataTransferProgress(object sender, BinaryDataTransferProgressEventArgs args)
        {
            Log("OnBinaryDataTransferProgress", string.Format("{0}: {1}% of {2} bytes (Type={3}, Id={4})", args.Direction == TransferDirection.Incoming ? "Receiving" : "Sending", args.TotalDataSize > 0 ? (int)(100 * (args.BytesTransferred / (double)args.TotalDataSize)) : -1, args.TotalDataSize, args.ResultType.ToString(), args.ResponseId));
        }

        private void OffProtocolByteReceived(object sender, OffProtocolByteReceivedEventArgs args)
        {
            Log("OffProtocolByteReceived", string.Format("{0}", (char)args.Byte));
        }

        private void AutomaticResponseArrived(object sender, AutomaticResponseArrivedEventArgs args)
        {
            Log("AutomaticResponseArrived", string.Format("Type={0}, Id={1}, Data={2} bytes", args.DataType.ToString(), args.ResponseId, args.Data != null ? args.Data.Length : 0));
        }

        #endregion

        #region  Intech 

        Camera _DM363 = null;
        Camera _A10003D = null;

        private void InitializeCamera()
        {
            _DM363 = new Camera();
            _A10003D = new Camera();
        }

        private void RegisterEvents()
        {
            _DM363.OnDataReceived += DM363_OnDataReceived;
            _A10003D.OnDataReceived += _A10003D_OnDataReceived;
            btnReconnectScale.Click += BtnReconnectScale_Click;
        }

        private void BtnReconnectScale_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();
            Thread.Sleep(100);
            if (!serialPort1.IsOpen) serialPort1.Open();
            if (serialPort1.IsOpen)
                MessageBox.Show("Đã kết nối lại");
            else
                MessageBox.Show("Chưa được kết nối");
        }

        private void _A10003D_OnDataReceived(string result)
        {
            if (result != string.Empty && result != null)
            {
                string[] resultArr = result.Split(',');

                this.lengthValue = resultArr[0];
                this.widthValue = resultArr[1];
                this.heightValue = resultArr[2];

                Dimension dimension = new Dimension();
                dimension.Length = resultArr[0];
                dimension.Width = resultArr[1];
                dimension.Height = resultArr[2];
                dimensions.Enqueue(dimension);

                txtLength.Invoke(new Action(() =>
                {
                    txtLength.Text = resultArr[0];
                }));
                txtWidth.Invoke(new Action(() =>
                {
                    txtWidth.Text = resultArr[1];
                }));
                txtHeight.Invoke(new Action(() =>
                {
                    txtHeight.Text = resultArr[2];
                }));
            }
            else
                Debug.WriteLine(string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "the size null or empty"));
        }

        private void DM363_OnDataReceived(string result)
        {
            if (result != string.Empty && result != null)
            {
                Invoke(new Action(() =>
                {
                    ProcessScan(result);
                }));
                lbReadString.Invoke(new Action(() =>
                {
                    lbReadString.Text = result;
                }));
            }

        }

        private void ConnectToCamera()
        {
            //_DM363.Connect("192.168.3.90", 23);
            //if (_DM363.IsConnected)
            //{
            //    MessageBox.Show("DM363 succes");
            //}
            _A10003D.Connect("192.168.3.218", 23);
            if (_A10003D.IsConnected)
            {
                MessageBox.Show("A10003D succes");
            }
        }

        private void DisconnectToCamera()
        {
            _DM363.Disconnect();
            _A10003D.Disconnect();
        }


        #endregion

        #region Auxiliary Methods

        private void CleanupConnection()
        {
            if (null != mySystem)
            {
                mySystem.SystemConnected -= OnSystemConnected;
                mySystem.SystemDisconnected -= OnSystemDisconnected;
                mySystem.SystemWentOnline -= OnSystemWentOnline;
                mySystem.SystemWentOffline -= OnSystemWentOffline;
                mySystem.KeepAliveResponseMissed -= OnKeepAliveResponseMissed;
                mySystem.BinaryDataTransferProgress -= OnBinaryDataTransferProgress;
                mySystem.OffProtocolByteReceived -= OffProtocolByteReceived;
                mySystem.AutomaticResponseArrived -= AutomaticResponseArrived;
            }

            _connector = null;
            mySystem = null;
        }
        private void OnLiveImageArrived(IAsyncResult result)
        {
            try
            {
                Image image = mySystem.EndGetLiveImage(result);

                syncContext.Post(
                    delegate
                    {
                        Size image_size = Gui.FitImageInControl(image.Size, picResultImage.Size);
                        Image fitted_image = Gui.ResizeImageToBitmap(image, image_size);
                        picResultImage.Image = fitted_image;
                        picResultImage.Invalidate();

                        if (cbLiveDisplay.Checked)
                        {
                            mySystem.BeginGetLiveImage(
                                ImageFormat.jpeg,
                                ImageSize.Sixteenth,
                                ImageQuality.Medium,
                                OnLiveImageArrived,
                                null);
                        }
                    },
                null);
            }
            catch
            {
            }
        }

        private string GetReadStringFromResultXml(string resultXml)
        {
            try
            {
                XmlDocument doc = new XmlDocument();

                doc.LoadXml(resultXml);

                XmlNode full_string_node = doc.SelectSingleNode("result/general/full_string");

                if (full_string_node != null && mySystem != null && mySystem.State == global::Cognex.DataMan.SDK.ConnectionState.Connected)
                {
                    XmlAttribute encoding = full_string_node.Attributes["encoding"];
                    if (encoding != null && encoding.InnerText == "base64")
                    {
                        if (!string.IsNullOrEmpty(full_string_node.InnerText))
                        {
                            byte[] code = Convert.FromBase64String(full_string_node.InnerText);
                            return mySystem.Encoding.GetString(code, 0, code.Length);
                        }
                        else
                        {
                            return "";
                        }
                    }

                    return full_string_node.InnerText;
                }
            }
            catch
            {
            }

            return "";
        }

        private void ShowResult(ComplexResult complexResult)
        {
            List<Image> images = new List<Image>();
            List<string> image_graphics = new List<string>();
            string read_result = null;
            int result_id = -1;
            ResultTypes collected_results = ResultTypes.None;

            // Take a reference or copy values from the locked result info object. This is done
            // so that the lock is used only for a short period of time.
            lock (_currentResultInfoSyncLock)
            {
                foreach (var simple_result in complexResult.SimpleResults)
                {
                    collected_results |= simple_result.Id.Type;

                    switch (simple_result.Id.Type)
                    {
                        case ResultTypes.Image:
                            Image image = ImageArrivedEventArgs.GetImageFromImageBytes(simple_result.Data);
                            if (image != null)
                                images.Add(image);
                            break;

                        case ResultTypes.ImageGraphics:
                            image_graphics.Add(simple_result.GetDataAsString());
                            break;

                        case ResultTypes.ReadXml:
                            read_result = GetReadStringFromResultXml(simple_result.GetDataAsString());
                            result_id = simple_result.Id.Id;
                            break;

                        case ResultTypes.ReadString:
                            read_result = simple_result.GetDataAsString();
                            result_id = simple_result.Id.Id;
                            break;
                    }

                }
            }
            if (read_result != string.Empty && read_result != null)
            {
                ProcessScan(read_result);

                //try
                //{
                //    var supportUsers = string.Join(",", UserLogin.SupportUsers);
                //    _kafkaProducer.PushMessage(new Inbound
                //    {
                //        PackageCode = read_result,
                //        CreatedAt = DateTime.Now,
                //        Note = "",
                //        UserId = UserLogin.UserId,
                //        SupportUsers = supportUsers,
                //        Status = "1",
                //    }, KafkaTopic.TOPIC_SCANNED_ORDER, read_result);
                //}
                //catch { }

            }

            Log("Complex result contains", string.Format("{0}", collected_results.ToString()));

            if (images.Count > 0)
            {
                Image first_image = images[0];

                Size image_size = Gui.FitImageInControl(first_image.Size, picResultImage.Size);
                Image fitted_image = Gui.ResizeImageToBitmap(first_image, image_size);

                if (image_graphics.Count > 0)
                {
                    using (Graphics g = Graphics.FromImage(fitted_image))
                    {
                        foreach (var graphics in image_graphics)
                        {
                            ResultGraphics rg = GraphicsResultParser.Parse(graphics, new Rectangle(0, 0, image_size.Width, image_size.Height));
                            ResultGraphicsRenderer.PaintResults(g, rg);
                        }
                    }
                }

                if (picResultImage.Image != null)
                {
                    var image = picResultImage.Image;
                    picResultImage.Image = null;
                    image.Dispose();
                }

                picResultImage.Image = fitted_image;
                picResultImage.Invalidate();
            }

            if (read_result != null)
                lbReadString.Text = read_result;
        }

        private void AddListItem(object item)
        {
            lock (_listAddItemLock)
            {
                listBoxStatus.Items.Add(item);

                if (listBoxStatus.Items.Count > 500)
                    listBoxStatus.Items.RemoveAt(0);

                if (listBoxStatus.Items.Count > 0)
                    listBoxStatus.SelectedIndex = listBoxStatus.Items.Count - 1;
            }
        }

        private void AddListHasakinow(object item)
        {
            lock (_listHasakinowLock)
            {
                listHasakinow.Items.Add(item);

                if (listHasakinow.Items.Count > 500)
                    listHasakinow.Items.RemoveAt(0);

                if (listHasakinow.Items.Count > 0)
                    listHasakinow.SelectedIndex = listHasakinow.Items.Count - 1;
            }
        }

        #endregion


        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                RefreshDgvInBound();
                return;
            }
            dtInBound = new DataTable();
            dtInBound = inBoundsBus.Search(txtSearch.Text);

            DataTable reversedDt = dtInBound.Clone();
            for (var row = dtInBound.Rows.Count - 1; row >= 0; row--)
                reversedDt.ImportRow(dtInBound.Rows[row]);
            dgvInBound.DataSource = reversedDt;
            dgvInBound.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvInBound.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
        }

        SaveFileDialog saveFileDialog = new SaveFileDialog();
        private void btnExportToExcell_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ToExcel(dgvInBound, saveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        OpenFileDialog dlg = null;
        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application application;
                Microsoft.Office.Interop.Excel.Workbook workbook;
                Microsoft.Office.Interop.Excel.Worksheet worksheet;
                Microsoft.Office.Interop.Excel.Range range;

                int row;
                string strFileName;
                dlg = new OpenFileDialog();

                dlg.Filter = "Excel Office |*.xls; *xlsx";
                dlg.ShowDialog();
                strFileName = dlg.FileName;

                if (strFileName != "")
                {
                    application = new Microsoft.Office.Interop.Excel.Application();
                    workbook = application.Workbooks.Open(strFileName);
                    worksheet = workbook.Worksheets["Sheet1"];
                    range = worksheet.UsedRange;

                    for (row = 2; row <= range.Rows.Count; row++)
                    {
                        if (range.Cells[row, 1].Text != "")
                        {
                            hasakiSystemBus.SetTable(range.Cells[row, 1].Text, range.Cells[row, 2].Text, range.Cells[row, 3].Text, range.Cells[row, 4].Text);
                        }
                    }
                    MessageBox.Show("Đã thêm dữ liệu vào bảng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    workbook.Close();
                    application.Quit();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefreshDgv_Click(object sender, EventArgs e)
        {
            RefreshDgvInBound();
            txtSearch.Text = string.Empty;
        }


        #region function helper
        void RefreshDgvInBound()
        {
            dtInBound = new DataTable();
            dtInBound = inBoundsBus.GetTable();
            DataTable reversedDt = dtInBound.Clone();
            for (var row = dtInBound.Rows.Count - 1; row >= 0; row--)
                reversedDt.ImportRow(dtInBound.Rows[row]);
            dgvInBound.DataSource = reversedDt;
            dgvInBound.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvInBound.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);

        }

        void RefreshDgvInBoundDate()
        {
            dtInBound = new DataTable();
            dtInBound = inBoundsBus.GetTableDate(DateTime.Now.ToString("M/d/yyyy"));
            DataTable reversedDt = dtInBound.Clone();
            for (var row = dtInBound.Rows.Count - 1; row >= 0; row--)
                reversedDt.ImportRow(dtInBound.Rows[row]);
            dgvInBound.DataSource = reversedDt;
            dgvInBound.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvInBound.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
        }

        private void ToExcel(DataGridView dgv, string fileName)
        {
            //khai báo thư viện hỗ trợ Microsoft.Office.Interop.Excel
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook workbook;
            Microsoft.Office.Interop.Excel.Worksheet worksheet;
            try
            {
                //Tạo đối tượng COM.
                excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = false;
                excel.DisplayAlerts = false;
                //tạo mới một Workbooks bằng phương thức add()
                workbook = excel.Workbooks.Add(Type.Missing);
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet1"];
                //đặt tên cho sheet
                worksheet.Name = "Thống kê kiện hàng";

                // export header trong DataGridView
                for (int i = 0; i < dgv.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1] = dgv.Columns[i].HeaderText;
                }
                // export nội dung trong DataGridView
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    for (int j = 0; j < dgv.ColumnCount; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dgv.Rows[i].Cells[j].Value.ToString();
                    }
                }
                // sử dụng phương thức SaveAs() để lưu workbook với filename
                workbook.SaveAs(fileName);
                //đóng workbook
                workbook.Close();
                excel.Quit();
                MessageBox.Show("Đã xuất dữ liệu ra file Excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                workbook = null;
                worksheet = null;
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (checkConnectPLC == true)
                {
                    readConveyorState = plc.ReadHoldingRegisters(1004);
                    if (readConveyorState == 1)
                    {
                        btnStart.BackColor = Color.Lime;
                        btnStop.BackColor = Color.LightGray;
                        lblConveyorStatus.ForeColor = Color.LimeGreen;
                        lblConveyorStatus.Text = "Conveyor is running";
                        continous = true;
                    }
                    if (readConveyorState == 0)
                    {
                        btnStart.BackColor = Color.LightGray;
                        btnStop.BackColor = Color.Orange;
                        lblConveyorStatus.ForeColor = Color.Orange;
                        lblConveyorStatus.Text = "Conveyor has stopped";
                    }
                    if (readConveyorState == 2)
                    {
                        btnStart.BackColor = Color.LightGray;
                        btnStop.BackColor = Color.LightGray;
                        lblConveyorStatus.ForeColor = Color.Gray;
                        lblConveyorStatus.Text = "Conveyor is Paused";
                    }
                }
            }
            catch (Exception ex)
            {
                listBoxStatus.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " -  PLC: " + ex.Message);
                progressBarPLC.Value = 0;
                timer1.Stop();
            }
        }


        //Reconnect PLC and Dim
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (client != null && client.Connected == true)
                client.Close();
            if (plc != null)
                DisconnectPLC();

            client = null;
            plc = null;

            try
            {
                listBoxStatus.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " Dim: Connecting...");
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint iep = new IPEndPoint(IPAddress.Parse(IP_ADDRESS_DIM), PORT_NUMBER_DIM);
                //client.BeginConnect(iep, new AsyncCallback(Connected), client);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            plc = new PLC(IP_ADDRESS_PLC, PORT_NUMBER_PLC);
            Thread thConnect = new Thread(() =>
            {
                ConnectPLC();
            })
            { IsBackground = true };
            thConnect.Start();
            timer1.Start();
            timer2.Start();
        }

        private void picBoxResetQuantity_Click(object sender, EventArgs e)
        {
            count = 0;
            countNG = 0;
            txtQuantity.Text = count.ToString();
            txtNG.Text = countNG.ToString() + "/" + count.ToString();
        }
        #endregion

        #region external api

        public async Task<string> ApiSendScanPackageResult(ApiSendScanPackageResultRequest request)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                StringContent jsonContent;

                using (HttpClient client = new HttpClient())
                {
                    var endPoint = "";
                    var token = System.Configuration.ConfigurationSettings.AppSettings["HSK_TOKEN"];
                    var addHeaderSuccess = client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
                    endPoint = "/partner-party/hskwork/scan-temporary";
                    jsonContent = new StringContent(
                       JsonSerializer.Serialize(new
                       {
                           ma_don_khach = request.PackageCode,
                           email_nhan_vien = request.Email,
                           location_id = request.LocationId,
                           width = request.Width,
                           length = request.Length,
                           height = request.Height,
                           weight = request.Weight,
                       }),
                       Encoding.UTF8,
                       "application/json");

                    using (HttpResponseMessage res = await client.PostAsync(System.Configuration.ConfigurationSettings.AppSettings["HSK_URL"] + endPoint, jsonContent))
                    {
                        using (HttpContent content = res.Content)
                        {
                            if (data != null)
                            {
                                try
                                {
                                    string data = await content.ReadAsStringAsync();

                                    var result = JsonSerializer.Deserialize<RootObject>(data);
                                    if (result.status.error_code == 0)
                                    {
                                        inBoundsBus.SetErrorCode(request.PackageCode, "No error");
                                    }
                                    else
                                    {
                                        inBoundsBus.SetErrorCode(request.PackageCode, result.status.error_message);
                                        AddListHasakinow($"Package: {request.PackageCode} , Error: {result.status.alert_message}");
                                    }
                                    Console.WriteLine();
                                    return data;
                                }
                                catch (Exception ex)
                                {
                                    inBoundsBus.SetErrorCode(request.PackageCode, $"Unexpected error when call ApiSendScanPackageResult: {ex}");
                                    AddListHasakinow($"Package: {request.PackageCode} , Error: {ex}");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AddListHasakinow($"Package: {request.PackageCode} , Error: {ex}");
            }


            return string.Empty;

        }

        public class HasakiNowData
        {
            public int error_code { get; set; }
            public string alert_message { get; set; }
            public string error_message { get; set; }
        }

        public class RootObject
        {
            public HasakiNowData status { get; set; }
        }
        #endregion

        #region action buttons
        private void button1_Click(object sender, EventArgs e)
        {
            listHasakinow.Items.Clear();
        }

        int idleTime = 0; bool continous = true;
        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                Scanning();
                if (checkConnectPLC == true)
                {
                    readSensor = plc.ReadCoils(8212);
                    if (readSensor == true)
                    {
                        //if (this.maKH == string.Empty)
                        //{
                        //    plc.WriteSingleCoid(8213, true);
                        //    plc.WriteSingleCoid(8213, false);
                        //    return;
                        //}
                        //if (this.weightValue != 0 || this.lengthValue != string.Empty || this.widthValue != string.Empty || this.heightValue != string.Empty || this.zone != string.Empty || this.state != string.Empty)
                        //{
                        //    //Insert 
                        //    Inbound inbound = new Inbound
                        //    {
                        //        CreatedAt = DateTime.Now,
                        //        PackageCode = this.maKH == string.Empty ? "Empty" : this.maKH,
                        //        Weight = this.weightValue.ToString().Trim(),
                        //        Length = this.lengthValue.Trim(),
                        //        Width = this.widthValue.Trim(),
                        //        Height = this.heightValue.Trim(),
                        //        Zone = this.zone,
                        //        Status = this.maKH == "NG" || this.maKH == "NE" || this.maKH == "" ? string.Empty : this.state,
                        //        UpdatedBy = revAccountName
                        //    };

                        //    inBoundsBus.SetTable(
                        //            inbound.CreatedAt.ToString("M/d/yyyy"),
                        //            inbound.CreatedAt.ToString("HH:mm:ss"),
                        //            inbound.PackageCode,
                        //            inbound.Weight,
                        //            inbound.Length,
                        //            inbound.Width,
                        //            inbound.Height,
                        //            inbound.Zone,
                        //            inbound.Status,
                        //            inbound.UpdatedBy);


                        //    // call api update status
                        //    if (this.zone != "99")
                        //    {
                        //        ApiSendScanPackageResultRequest req = new ApiSendScanPackageResultRequest
                        //        {
                        //            PackageCode = inbound.PackageCode,
                        //            Email = Constants.Constants.API_EMAIL,
                        //            Weight = (float)(this.weightValue * 1000)
                        //        };

                        //        req.LocationId = Constants.Constants.LOCATION_ID_170_QUOC_LO;

                        //        int.TryParse(inbound.Height, out int height);
                        //        int.TryParse(inbound.Length, out int length);
                        //        int.TryParse(inbound.Width, out int width);
                        //        req.Height = height;
                        //        req.Length = length;
                        //        req.Width = width;
                        //        _ = ApiSendScanPackageResult(req);
                        //    }

                        //    this.weightValue = 0;
                        //    this.lengthValue = string.Empty;
                        //    this.widthValue = string.Empty;
                        //    this.heightValue = string.Empty;
                        //    this.maKH = string.Empty;
                        //    this.zone = string.Empty;
                        //    this.state = string.Empty;
                        //    RefreshDgvInBoundDate();
                        //}

                        plc.WriteSingleCoid(8213, true);
                        plc.WriteSingleCoid(8213, false);
                        idleTime = 0;
                        continous = true;
                    }
                    else if (continous == true)
                    {
                        idleTime++;
                        if (idleTime >= 4800) //10p
                        {
                            if (checkConnectPLC)
                                plc.WriteSingleRegister(1002, 2);
                            continous = false;
                            idleTime = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                listBoxStatus.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " - PLC: " + ex.Message);
            }
        }


        private void Scanning()
        {
            if (parcels.Count > 0)
            {
                Parcel parcel = parcels.Dequeue();
                Dimension dimension = new Dimension();

                int dimensionTimeout = 0;
                while (dimensions.Count == 0)
                {
                    dimensionTimeout++;
                    Thread.Sleep(10);
                    if (dimensionTimeout >= 30) break;
                }

                while (dimensions.Count > parcels.Count)
                {
                    dimension = dimensions.Dequeue();
                    if (dimensions.Count == parcels.Count) break;
                }

                if (parcel.ParcelCode == string.Empty) return;

                int weighingTimeout = 0;
                while (this.weightValue == 0)
                {
                    weighingTimeout++;
                    Thread.Sleep(10);
                    if (weighingTimeout >= 30)
                        break;
                }
                txtWeight.Text = weightValue.ToString();

                //Thread.Sleep(1000);
                Inbound inbound = new Inbound
                {
                    CreatedAt = DateTime.Now,
                    PackageCode = parcel.ParcelCode == string.Empty ? "Empty" : parcel.ParcelCode,
                    Weight = this.weightValue.ToString(),
                    Length = dimension.Length,
                    Width = dimension.Width,
                    Height = dimension.Height,
                    Zone = parcel.Zone,
                    Status = parcel.ParcelCode == "NG" || parcel.ParcelCode == "NE" || parcel.ParcelCode == "" ? string.Empty : parcel.Status,
                    UpdatedBy = revAccountName
                };

                inBoundsBus.SetTable(
                                    inbound.CreatedAt.ToString("M/d/yyyy"),
                                    inbound.CreatedAt.ToString("HH:mm:ss"),
                                    inbound.PackageCode,
                                    inbound.Weight,
                                    inbound.Length,
                                    inbound.Width,
                                    inbound.Height,
                                    inbound.Zone,
                                    inbound.Status,
                                    inbound.UpdatedBy);

                // call api update status
                if (parcel.Zone != "99")
                {
                    ApiSendScanPackageResultRequest req = new ApiSendScanPackageResultRequest
                    {
                        PackageCode = inbound.PackageCode,
                        Email = Constants.Constants.API_EMAIL,
                        Weight = (float)(this.weightValue * 1000)
                    };

                    req.LocationId = Constants.Constants.LOCATION_ID_170_QUOC_LO;

                    int.TryParse(inbound.Height, out int height);
                    int.TryParse(inbound.Length, out int length);
                    int.TryParse(inbound.Width, out int width);
                    req.Height = height;
                    req.Length = length;
                    req.Width = width;
                    _ = ApiSendScanPackageResult(req);
                }

                this.weightValue = 0;
                this.lengthValue = string.Empty;
                this.widthValue = string.Empty;
                this.heightValue = string.Empty;
                this.maKH = string.Empty;
                this.zone = string.Empty;
                this.state = string.Empty;
                RefreshDgvInBoundDate();
            }
        }

        private void btnClearInboundData_Click(object sender, EventArgs e)
        {
            try
            {
                string message = "Do you want to clear all data?";
                string title = "Clear all data";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    inBoundsBus.DeleteAllData();
                    RefreshDgvInBound();
                }
            }
            catch (Exception)
            {

            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            cbSelectZone0.Enabled = true;
            cbSelectZone1.Enabled = true;
            cbSelectZone2.Enabled = true;
            cbSelectZone3.Enabled = true;
            cbSelectZone4.Enabled = true;
            cbSelectZone5.Enabled = true;
            cbSelectZone6.Enabled = true;
            cbSelectZone7.Enabled = true;
            cbSelectZone8.Enabled = true;
            cbSelectZone9.Enabled = true;
            cbSelectShop22.Enabled = true;
            cbSelectZoneError.Enabled = true;
            cbSelectZone1A.Enabled = true;
            cbSelectZone2A.Enabled = true;
            cbSelectZone3A.Enabled = true;
            cbSelectZone4A.Enabled = true;
            cbSelectZone5A.Enabled = true;
            cbSelectZone6A.Enabled = true;
            cbSelectZone7A.Enabled = true;
            cbSelectZone8A.Enabled = true;
            cbSelectZone9A.Enabled = true;
            cbSelectZone6B.Enabled = true;
            cbSelectZoneKT.Enabled = true;
            cbSelectZoneGift.Enabled = true;

            formMapZoneToGate mapZoneToGate = new formMapZoneToGate(this);
            mapZoneToGate.ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            cbSelectZone0.Enabled = false;
            cbSelectZone1.Enabled = false;
            cbSelectZone2.Enabled = false;
            cbSelectZone3.Enabled = false;
            cbSelectZone4.Enabled = false;
            cbSelectZone5.Enabled = false;
            cbSelectZone6.Enabled = false;
            cbSelectZone7.Enabled = false;
            cbSelectZone8.Enabled = false;
            cbSelectZone9.Enabled = false;
            cbSelectShop22.Enabled = false;
            cbSelectZoneError.Enabled = false;
            cbSelectZone1A.Enabled = false;
            cbSelectZone2A.Enabled = false;
            cbSelectZone3A.Enabled = false;
            cbSelectZone4A.Enabled = false;
            cbSelectZone5A.Enabled = false;
            cbSelectZone6A.Enabled = false;
            cbSelectZone7A.Enabled = false;
            cbSelectZone8A.Enabled = false;
            cbSelectZone9A.Enabled = false;
            cbSelectZone6B.Enabled = false;
            cbSelectZoneKT.Enabled = false;
            cbSelectZoneGift.Enabled = false;
            _defaultSelection.Zone0Selection = cbSelectZone0.SelectedItem.ToString();
            _defaultSelection.Zone1Selection = cbSelectZone1.SelectedItem.ToString();
            _defaultSelection.Zone2Selection = cbSelectZone2.SelectedItem.ToString();
            _defaultSelection.Zone3Selection = cbSelectZone3.SelectedItem.ToString();
            _defaultSelection.Zone4Selection = cbSelectZone4.SelectedItem.ToString();
            _defaultSelection.Zone5Selection = cbSelectZone5.SelectedItem.ToString();
            _defaultSelection.Zone6Selection = cbSelectZone6.SelectedItem.ToString();
            _defaultSelection.Zone7Selection = cbSelectZone7.SelectedItem.ToString();
            _defaultSelection.Zone8Selection = cbSelectZone8.SelectedItem.ToString();
            _defaultSelection.Zone9Selection = cbSelectZone9.SelectedItem.ToString();
            _defaultSelection.Shop22Selection = cbSelectShop22.SelectedItem.ToString();
            _defaultSelection.Zone99Selection = cbSelectZoneError.SelectedItem.ToString();
            _defaultSelection.Zone1ASelection = cbSelectZone1A.SelectedItem.ToString();
            _defaultSelection.Zone2ASelection = cbSelectZone2A.SelectedItem.ToString();
            _defaultSelection.Zone3ASelection = cbSelectZone3A.SelectedItem.ToString();
            _defaultSelection.Zone4ASelection = cbSelectZone4A.SelectedItem.ToString();
            _defaultSelection.Zone5ASelection = cbSelectZone5A.SelectedItem.ToString();
            _defaultSelection.Zone6ASelection = cbSelectZone6A.SelectedItem.ToString();
            _defaultSelection.Zone7ASelection = cbSelectZone7A.SelectedItem.ToString();
            _defaultSelection.Zone8ASelection = cbSelectZone8A.SelectedItem.ToString();
            _defaultSelection.Zone9ASelection = cbSelectZone9A.SelectedItem.ToString();
            _defaultSelection.Zone6BSelection = cbSelectZone6B.SelectedItem.ToString();
            _defaultSelection.ZoneKTSelection = cbSelectZoneKT.SelectedItem.ToString();
            _defaultSelection.ZoneGiftSelection = cbSelectZoneGift.SelectedItem.ToString();

            _defaultSelection.SaveSelection(Constants.Constants.DEFAULT_ZONE_SELECTION_FILE);
        }
        #endregion

        private int PopGateByZone(string zone)
        {
            switch (zone)
            {
                case "0":
                case "Zone 0":
                    plc.PopGate(SelectGate(cbSelectZone0.SelectedIndex >= 0 ? cbSelectZone0.SelectedItem.ToString() : ""));
                    return 0;
                case "1":
                case "Zone 1":
                    plc.PopGate(SelectGate(cbSelectZone1.SelectedIndex >= 0 ? cbSelectZone1.SelectedItem.ToString() : ""));
                    return 1;
                case "2":
                case "Zone 2":
                    plc.PopGate(SelectGate(cbSelectZone2.SelectedIndex >= 0 ? cbSelectZone2.SelectedItem.ToString() : ""));
                    return 2;
                case "3":
                case "Zone 3":
                    plc.PopGate(SelectGate(cbSelectZone3.SelectedIndex >= 0 ? cbSelectZone3.SelectedItem.ToString() : ""));
                    return 3;
                case "4":
                case "Zone 4":
                    plc.PopGate(SelectGate(cbSelectZone4.SelectedIndex >= 0 ? cbSelectZone4.SelectedItem.ToString() : ""));
                    return 4;
                case "5":
                case "Zone 5":
                    plc.PopGate(SelectGate(cbSelectZone5.SelectedIndex >= 0 ? cbSelectZone5.SelectedItem.ToString() : ""));
                    return 5;
                case "6":
                case "Zone 6":
                    plc.PopGate(SelectGate(cbSelectZone6.SelectedIndex >= 0 ? cbSelectZone6.SelectedItem.ToString() : ""));
                    return 6;
                case "7":
                case "Zone 7":
                    plc.PopGate(SelectGate(cbSelectZone7.SelectedIndex >= 0 ? cbSelectZone7.SelectedItem.ToString() : ""));
                    return 7;
                case "8":
                case "Zone 8":
                    plc.PopGate(SelectGate(cbSelectZone8.SelectedIndex >= 0 ? cbSelectZone8.SelectedItem.ToString() : ""));
                    return 8;
                case "9":
                case "Zone 9":
                    plc.PopGate(SelectGate(cbSelectZone9.SelectedIndex >= 0 ? cbSelectZone9.SelectedItem.ToString() : ""));
                    return 9;
                case "SHOPEE":
                    plc.PopGate(SelectGate(cbSelectShop22.SelectedIndex >= 0 ? cbSelectShop22.SelectedItem.ToString() : ""));
                    return 10;
                case "99":
                    plc.PopGate(SelectGate(cbSelectZoneError.SelectedIndex >= 0 ? cbSelectZoneError.SelectedItem.ToString() : ""));
                    return 99;
                case "1A":
                case "Zone 1A":
                    plc.PopGate(SelectGate(cbSelectZone1A.SelectedIndex >= 0 ? cbSelectZone1A.SelectedItem.ToString() : ""));
                    return 1;
                case "2A":
                case "Zone 2A":
                    plc.PopGate(SelectGate(cbSelectZone2A.SelectedIndex >= 0 ? cbSelectZone2A.SelectedItem.ToString() : ""));
                    return 2;
                case "3A":
                case "Zone 3A":
                    plc.PopGate(SelectGate(cbSelectZone3A.SelectedIndex >= 0 ? cbSelectZone3A.SelectedItem.ToString() : ""));
                    return 3;
                case "4A":
                case "Zone 4A":
                    plc.PopGate(SelectGate(cbSelectZone4A.SelectedIndex >= 0 ? cbSelectZone4A.SelectedItem.ToString() : ""));
                    return 4;
                case "5A":
                case "Zone 5A":
                    plc.PopGate(SelectGate(cbSelectZone5A.SelectedIndex >= 0 ? cbSelectZone5A.SelectedItem.ToString() : ""));
                    return 5;
                case "6A":
                case "Zone 6A":
                    plc.PopGate(SelectGate(cbSelectZone6A.SelectedIndex >= 0 ? cbSelectZone6A.SelectedItem.ToString() : ""));
                    return 6;
                case "7A":
                case "Zone 7A":
                    plc.PopGate(SelectGate(cbSelectZone7A.SelectedIndex >= 0 ? cbSelectZone7A.SelectedItem.ToString() : ""));
                    return 7;
                case "8A":
                case "Zone 8A":
                    plc.PopGate(SelectGate(cbSelectZone8A.SelectedIndex >= 0 ? cbSelectZone8A.SelectedItem.ToString() : ""));
                    return 8;
                case "9A":
                case "Zone 9A":
                    plc.PopGate(SelectGate(cbSelectZone9A.SelectedIndex >= 0 ? cbSelectZone9A.SelectedItem.ToString() : ""));
                    return 9;
                case "6B":
                case "Zone 6B":
                    plc.PopGate(SelectGate(cbSelectZone6B.SelectedIndex >= 0 ? cbSelectZone6B.SelectedItem.ToString() : ""));
                    return 6;
                case "GIFT-NG-RETURN":
                    plc.PopGate(SelectGate(cbSelectZoneGift.SelectedIndex >= 0 ? cbSelectZoneGift.SelectedItem.ToString() : ""));
                    return 0;
                case "KT":
                case "Kaizen":
                    plc.PopGate(SelectGate(cbSelectZoneKT.SelectedIndex >= 0 ? cbSelectZoneKT.SelectedItem.ToString() : ""));
                    return 0;
                default:
                    if (_defaultSelection.Group1.ContainsKey(zone))
                    {
                        plc.PopGate(SelectGate(_defaultSelection.Group1[zone]));
                        return 0;
                    }
                    else if (_defaultSelection.Group2.ContainsKey(zone))
                    {
                        plc.PopGate(SelectGate(_defaultSelection.Group2[zone]));
                        return 0;
                    }
                    else if (_defaultSelection.Group3.ContainsKey(zone))
                    {
                        plc.PopGate(SelectGate(_defaultSelection.Group3[zone]));
                        return 0;
                    }
                    else if (_defaultSelection.Group4.ContainsKey(zone))
                    {
                        plc.PopGate(SelectGate(_defaultSelection.Group4[zone]));
                        return 0;
                    }
                    else if (_defaultSelection.Group5.ContainsKey(zone))
                    {
                        plc.PopGate(SelectGate(_defaultSelection.Group5[zone]));
                        return 0;
                    }
                    else if (_defaultSelection.Group6.ContainsKey(zone))
                    {
                        plc.PopGate(SelectGate(_defaultSelection.Group6[zone]));
                        return 0;
                    }
                    else if (_defaultSelection.Group7.ContainsKey(zone))
                    {
                        plc.PopGate(SelectGate(_defaultSelection.Group7[zone]));
                        return 0;
                    }
                    else if (_defaultSelection.Group8.ContainsKey(zone))
                    {
                        plc.PopGate(SelectGate(_defaultSelection.Group8[zone]));
                        return 0;
                    }
                    plc.PopGate(SelectGate(cbSelectZone0.SelectedIndex >= 0 ? cbSelectZone0.SelectedItem.ToString() : ""));
                    return 0;
            }
        }

        private int SelectGate(string gate)
        {
            string gateWithPrefix = gate.Replace("Cổng ", "");
            if (gateWithPrefix.Contains("R"))
            {
                var zoneStr = gateWithPrefix.Replace("R", "");
                bool isNumber = int.TryParse(zoneStr, out int zone);
                Console.WriteLine(maKH + ": " + (100 + zone).ToString());
                if (!isNumber)
                {
                    return 99;
                }

                // 101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120
                return 100 + zone;
            }
            else if (gateWithPrefix.Contains("L"))
            {
                var zoneStr = gateWithPrefix.Replace("L", "");
                bool isNumber = int.TryParse(zoneStr, out int zone);
                Console.WriteLine(maKH + ": " + (zone).ToString());
                if (!isNumber)
                {
                    return 99;
                }

                // 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20
                return zone;

            }

            return 99;

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInputTest.Text))
            {
                return;
            }
            ProcessScan(txtInputTest.Text);
            ApiSendScanPackageResultRequest req = new ApiSendScanPackageResultRequest
            {
                PackageCode = txtInputTest.Text,
                Email = Constants.Constants.API_EMAIL,
                Weight = (float)(0.21 * 1000)
            };

            req.LocationId = Constants.Constants.LOCATION_ID_170_QUOC_LO;
            req.Height = 141;
            req.Width = 159;
            req.Length = 182;

            _ = ApiSendScanPackageResult(req);
        }

        private void ProcessScan(string packedLabel)
        {
            try
            {
                Stopwatch st = new Stopwatch();
                st.Start();

                Parcel parcel = new Parcel();

                //đổ dữ liệu vào dtHasakiSystem
                if (packedLabel == "NG")
                {
                    if (plc != null)
                        plc.WriteSingleRegister(1000, 99);
                    maKH = packedLabel;
                    parcel.ParcelCode = packedLabel;
                    countNG++;
                    count++;

                    lblZone.Text = "99";
                    txtQuantity.Text = count.ToString();
                    txtNG.Text = countNG.ToString() + "/" + count.ToString();
                    listBoxStatus.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " NG");

                }
                else if (hasakiSystemBus.CheckCode(packedLabel) == false)
                {
                    if (plc != null)
                        plc.WriteSingleRegister(1000, 99);
                    // this.maKH = "NE"; //not exist
                    maKH = packedLabel;
                    parcel.ParcelCode = packedLabel;
                    countNG++;
                    count++;
                    state = string.Empty;
                    parcel.Status = string.Empty;
                    zone = "99";
                    lblZone.Text = "99";
                    txtQuantity.Text = count.ToString();
                    txtNG.Text = countNG.ToString() + "/" + count.ToString();
                    listBoxStatus.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " Not exist");

                }
                else
                {
                    maKH = packedLabel;
                    parcel.ParcelCode = packedLabel;
                    dtHasakiSystem = hasakiSystemBus.GetTable(packedLabel);
                    count++;

                    if (dtHasakiSystem != null)
                    {
                        hasakiSystemDTO.SetMaKienHang(dtHasakiSystem.Rows[0][1].ToString());
                        hasakiSystemDTO.SetMaZone(dtHasakiSystem.Rows[0][2].ToString());
                        hasakiSystemDTO.SetTrangThai(dtHasakiSystem.Rows[0][3].ToString());
                        if (dtHasakiSystem.Rows[0]["store_name"].ToString().Contains(" KT"))
                        {
                            hasakiSystemDTO.SetMaZone("KT");
                        }
                        else if (dtHasakiSystem.Rows[0]["store_name"].ToString().Contains("KZ - "))
                        {
                            hasakiSystemDTO.SetMaZone("Kaizen");
                        }
                        if (Int32.TryParse(dtHasakiSystem.Rows[0][4].ToString(), out int pickupId))
                        {
                            hasakiSystemDTO.PickupLocationId = pickupId;
                        }
                        else
                        {
                            MessageBox.Show("Error parseing id shop source : " + dtHasakiSystem.Rows[0][4].ToString(), "Lỗi dữ liệu");
                        }

                        if (Int32.TryParse(dtHasakiSystem.Rows[0][5].ToString(), out int receiveId))
                        {
                            hasakiSystemDTO.ReceiverLocationId = receiveId;
                        }
                        else
                        {
                            MessageBox.Show("Error parseing id shop dest : " + dtHasakiSystem.Rows[0][4].ToString(), "Lỗi dữ liệu");
                        }
                    }

                    zone = "99";
                    parcel.Zone = "99";
                    state = hasakiSystemDTO.GetTrangThai();
                    parcel.Status = hasakiSystemDTO.GetTrangThai();

                    if (Constants.Constants.TanTheNhatIds.Contains(hasakiSystemDTO.PickupLocationId)
                        && !Constants.Constants.TanTheNhatIds.Contains(hasakiSystemDTO.ReceiverLocationId))
                    {
                        if (state == Constants.Constants.PACKAGE_STATUS_CREATED ||
                            state == Constants.Constants.PACKAGE_STATUS_WAITING_IT ||
                            state == Constants.Constants.PACKAGE_STATUS_SHIPPING_IT ||
                            state == Constants.Constants.PACKAGE_STATUS_SHIPPED_IT)
                        {
                            zone = hasakiSystemDTO.GetMaZone();
                            parcel.Zone = hasakiSystemDTO.GetMaZone();
                        }
                        DataTable dtTnboundRow = inBoundsBus.FindByPackageCode(hasakiSystemDTO.GetMaKienHang());
                        if (dtTnboundRow != null)
                        {
                            if (dtTnboundRow.Rows.Count > 0)
                            {
                                AddListHasakinow($"Kiện hàng {hasakiSystemDTO.GetMaKienHang()} đã được nhập vào kho");

                            }
                        }
                    }
                    else if (!Constants.Constants.TanTheNhatIds.Contains(hasakiSystemDTO.PickupLocationId)
                        && Constants.Constants.TanTheNhatIds.Contains(hasakiSystemDTO.ReceiverLocationId))
                    {
                        //if (state == Constants.Constants.PACKAGE_STATUS_SHIPPING_IT )
                        if (state == Constants.Constants.PACKAGE_STATUS_SHIPPING_IT || state == Constants.Constants.PACKAGE_STATUS_WAITING_IT ||
                            state == Constants.Constants.PACKAGE_STATUS_SHIPPED_IT) // tạm  thờis
                        {
                            zone = hasakiSystemDTO.GetMaZone();
                            parcel.Zone = hasakiSystemDTO.GetMaZone();
                        }
                    }
                    else if (!Constants.Constants.TanTheNhatIds.Contains(hasakiSystemDTO.PickupLocationId)
                        && !Constants.Constants.TanTheNhatIds.Contains(hasakiSystemDTO.ReceiverLocationId))
                    {
                        if (state == Constants.Constants.PACKAGE_STATUS_SHIPPING_IT ||
                            state == Constants.Constants.PACKAGE_STATUS_SHIPPED_IT) // tam thoi
                        {
                            zone = hasakiSystemDTO.GetMaZone();
                            parcel.Zone = hasakiSystemDTO.GetMaZone();
                        }
                    }

                    if (zone == "99")
                    {
                        AddListHasakinow($"Trạng thái mã kiện hàng {hasakiSystemDTO.GetMaKienHang()} không thoả điều kiện. Details: shop_source={hasakiSystemDTO.PickupLocationId}; shop_dest={hasakiSystemDTO.ReceiverLocationId}; status={hasakiSystemDTO.GetTrangThai()}");
                    }
                    else
                    {
                        //_ = PushHasakiNow(1, read_result, zone);
                    }

                    //gửi Zone xuống PLC

                    if (plc != null)
                    {
                        int res = PopGateByZone(zone);
                        IncrLabelCount(zone);
                        Console.WriteLine($"pkl: {packedLabel} send signal: {res}");
                    }
                    else
                        listBoxStatus.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " PLC chưa được kết nối");

                    lblZone.Text = zone;
                    txtQuantity.Text = count.ToString();
                    txtNG.Text = countNG.ToString() + "/" + count.ToString();

                    st.Stop();
                    Debug.WriteLine(string.Format("{0} {1}", DateTime.Now.ToString("HH:mm:ss"), st.ElapsedMilliseconds.ToString()));
                }

                parcels.Enqueue(parcel);
            }
            catch (Exception ex)
            {
                listBoxStatus.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " " + ex.Message);
            }
            finally
            {

            }

        }

        private void IncrLabelCount(string zone)
        {
            _state.IncrZoneCount(zone);
            lbCountZone0.Text = _state.Zone0Count.ToString();
            lbCountZone1.Text = _state.Zone1Count.ToString();
            lbCountZone2.Text = _state.Zone2Count.ToString();
            lbCountZone3.Text = _state.Zone3Count.ToString();
            lbCountZone4.Text = _state.Zone4Count.ToString();
            lbCountZone5.Text = _state.Zone5Count.ToString();
            lbCountZone6.Text = _state.Zone6Count.ToString();
            lbCountZone7.Text = _state.Zone7Count.ToString();
            lbCountZone8.Text = _state.Zone8Count.ToString();
            lbCountZone9.Text = _state.Zone9Count.ToString();
            lbCountShop22.Text = _state.Shop22Count.ToString();
            lbCountZone99.Text = _state.Zone99Count.ToString();
            lbCountZone1A.Text = _state.Zone1ACount.ToString();
            lbCountZone2A.Text = _state.Zone2ACount.ToString();
            lbCountZone3A.Text = _state.Zone3ACount.ToString();
            lbCountZone4A.Text = _state.Zone4ACount.ToString();
            lbCountZone5A.Text = _state.Zone5ACount.ToString();
            lbCountZone6A.Text = _state.Zone5ACount.ToString();
            lbCountZone7A.Text = _state.Zone7ACount.ToString();
            lbCountZone8A.Text = _state.Zone8ACount.ToString();
            lbCountZone9A.Text = _state.Zone9ACount.ToString();
            lbCountZone6B.Text = _state.Zone6BCount.ToString();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void picClearZone1Count_Click(object sender, EventArgs e)
        {
            _state.Zone1Count = 0;
            lbCountZone1.Text = _state.Zone1Count.ToString();
        }

        private void picClearZone2Count_Click(object sender, EventArgs e)
        {
            _state.Zone2Count = 0;
            lbCountZone2.Text = _state.Zone2Count.ToString();
        }

        private void picClearZone3Count_Click(object sender, EventArgs e)
        {
            _state.Zone3Count = 0;
            lbCountZone3.Text = _state.Zone3Count.ToString();
        }

        private void picClearZone4Count_Click(object sender, EventArgs e)
        {
            _state.Zone4Count = 0;
            lbCountZone4.Text = _state.Zone4Count.ToString();
        }

        private void picClearZone5Count_Click(object sender, EventArgs e)
        {
            _state.Zone5Count = 0;
            lbCountZone5.Text = _state.Zone5Count.ToString();
        }

        private void picClearZone6Count_Click(object sender, EventArgs e)
        {
            _state.Zone6Count = 0;
            lbCountZone6.Text = _state.Zone6Count.ToString();
        }

        private void picClearZone7Count_Click(object sender, EventArgs e)
        {
            _state.Zone7Count = 0;
            lbCountZone7.Text = _state.Zone7Count.ToString();
        }

        private void picClearZone8Count_Click(object sender, EventArgs e)
        {
            _state.Zone8Count = 0;
            lbCountZone8.Text = _state.Zone8Count.ToString();
        }

        private void picClearZone9Count_Click(object sender, EventArgs e)
        {
            _state.Zone9Count = 0;
            lbCountZone9.Text = _state.Zone9Count.ToString();
        }

        private void picClearZone0Count_Click(object sender, EventArgs e)
        {
            _state.Zone0Count = 0;
            lbCountZone0.Text = _state.Zone0Count.ToString();
        }

        private void picClearShop22Count_Click(object sender, EventArgs e)
        {
            _state.Shop22Count = 0;
            lbCountShop22.Text = _state.Shop22Count.ToString();
        }

        private void picClearZone99Count_Click(object sender, EventArgs e)
        {
            _state.Zone99Count = 0;
            lbCountZone99.Text = _state.Zone99Count.ToString();
        }

        private void picClearZone7ACount_Click(object sender, EventArgs e)
        {
            _state.Zone7ACount = 0;
            lbCountZone7A.Text = _state.Zone7ACount.ToString();
        }

        private void picClearZone3ACount_Click(object sender, EventArgs e)
        {
            _state.Zone3ACount = 0;
            lbCountZone3A.Text = _state.Zone3ACount.ToString();
        }

        private void picClearZone4ACount_Click(object sender, EventArgs e)
        {
            _state.Zone4ACount = 0;
            lbCountZone4A.Text = _state.Zone4ACount.ToString();
        }
        private void picClearZone1ACount_Click(object sender, EventArgs e)
        {
            _state.Zone1ACount = 0;
            lbCountZone1A.Text = _state.Zone1ACount.ToString();
        }

        private void picClearZone8ACount_Click(object sender, EventArgs e)
        {
            _state.Zone8ACount = 0;
            lbCountZone8A.Text = _state.Zone8ACount.ToString();
        }

        private void picClearZone9ACount_Click(object sender, EventArgs e)
        {
            _state.Zone9ACount = 0;
            lbCountZone9A.Text = _state.Zone9ACount.ToString();
        }

        private void picClearZone5ACount_Click(object sender, EventArgs e)
        {
            _state.Zone5ACount = 0;
            lbCountZone5A.Text = _state.Zone5ACount.ToString();
        }

        private void picClearZone6ACount_Click(object sender, EventArgs e)
        {
            _state.Zone6ACount = 0;
            lbCountZone6A.Text = _state.Zone6ACount.ToString();
        }

        private void txtQRScan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(txtQRScan.Text))
            {
                try
                {
                    var jsonData = JObject.Parse(txtQRScan.Text);
                    var userId = jsonData["shipper_id"].Value<int>();
                    txtQRScan.Text = "";

                    var listZone = hasakiSystemBus.CountPackedLabelByUser(userId);
                    string msgZone = $"Shiper [{userId}]: ";
                    for (int i = 0; i < listZone.Count; i++)
                    {
                        if (i < 10)
                        {
                            msgZone += $"zone {listZone[i].Zone} -> {cbSelectZone1.Items[i]}; ";
                            UpdateZoneSelection(listZone[i].Zone, i);
                        }
                        else
                        {
                            msgZone += $"zone {listZone[i].Zone} -> {cbSelectZone1.Items[9]}; ";
                            UpdateZoneSelection(listZone[i].Zone, 9);
                        }
                    }
                    AddListHasakinow(msgZone);


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Vui long thu lai" + ex);
                }
            }
        }

        private void UpdateZoneSelection(string zone, int gate)
        {
            switch (zone)
            {
                case "0":
                    cbSelectZone0.SelectedIndex = gate;
                    break;
                case "1":
                    cbSelectZone1.SelectedIndex = gate;
                    break;
                case "2":
                    cbSelectZone2.SelectedIndex = gate;
                    break;
                case "3":
                    cbSelectZone3.SelectedIndex = gate;
                    break;
                case "4":
                    cbSelectZone4.SelectedIndex = gate;
                    break;
                case "5":
                    cbSelectZone5.SelectedIndex = gate;
                    break;
                case "6":
                    cbSelectZone6.SelectedIndex = gate;
                    break;
                case "7":
                    cbSelectZone7.SelectedIndex = gate;
                    break;
                case "8":
                    cbSelectZone8.SelectedIndex = gate;
                    break;
                case "9":
                    cbSelectZone9.SelectedIndex = gate;
                    break;
                case "10":
                    cbSelectShop22.SelectedIndex = gate;
                    break;
                case "1A":
                    cbSelectZone1A.SelectedIndex = gate;
                    break;
                case "2A":
                    cbSelectZone2A.SelectedIndex = gate;
                    break;
                case "3A":
                    cbSelectZone3A.SelectedIndex = gate;
                    break;
                case "4A":
                    cbSelectZone4A.SelectedIndex = gate;
                    break;
                case "5A":
                    cbSelectZone5A.SelectedIndex = gate;
                    break;
                case "6A":
                    cbSelectZone6A.SelectedIndex = gate;
                    break;
                case "7A":
                    cbSelectZone7A.SelectedIndex = gate;
                    break;
                case "8A":
                    cbSelectZone8A.SelectedIndex = gate;
                    break;
                case "9A":
                    cbSelectZone9A.SelectedIndex = gate;
                    break;
                case "SHOPEE":
                    cbSelectShop22.SelectedIndex = gate;
                    break;
                case "6B":
                    cbSelectZone6B.SelectedIndex = gate;
                    break;
            }
        }

        private void picClearZone2ACount_Click(object sender, EventArgs e)
        {
            _state.Zone2ACount = 0;
            lbCountZone2A.Text = _state.Zone2ACount.ToString();
        }

        private void picClearZone6BCount_Click(object sender, EventArgs e)
        {
            _state.Zone6BCount = 0;
            lbCountZone6B.Text = _state.Zone6BCount.ToString();
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.userTableAdapter.FillBy(this.masterDataSet.User);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void cbSelectUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DTO.User user = new DTO.User().ParseFromDataView((DataRowView)cbSelectUser.SelectedItem);
                for (int i = 0; i < listBoxSubUser.Items.Count; i++)
                {
                    if (listBoxSubUser.Items[i].ToString() == user.Email)
                        return;
                }
                listBoxSubUser.Items.Add(user);
                UserLogin.SupportUsers.Add(user.UserId.ToString());
            }
            catch { }
        }

        private void listBoxSubUser_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (listBoxSubUser.SelectedIndex >= 0 && listBoxSubUser.SelectedIndex < listBoxSubUser.Items.Count)
                {
                    DTO.User user = (DTO.User)listBoxSubUser.Items[listBoxSubUser.SelectedIndex];
                    UserLogin.SupportUsers.Remove(user.UserId.ToString());
                    listBoxSubUser.Items.RemoveAt(listBoxSubUser.SelectedIndex);
                }
            }
            catch { }
        }

        public void SetDefaultSelectionGroup1(Dictionary<string, string> group)
        {
            _defaultSelection.Group1 = group;
        }

        public void SetDefaultSelectionGroup2(Dictionary<string, string> group)
        {
            _defaultSelection.Group2 = group;
        }

        public void SetDefaultSelectionGroup3(Dictionary<string, string> group)
        {
            _defaultSelection.Group3 = group;
        }

        public void SetDefaultSelectionGroup4(Dictionary<string, string> group)
        {
            _defaultSelection.Group4 = group;
        }

        public void SetDefaultSelectionGroup5(Dictionary<string, string> group)
        {
            _defaultSelection.Group5 = group;
        }

        public void SetDefaultSelectionGroup6(Dictionary<string, string> group)
        {
            _defaultSelection.Group6 = group;
        }

        public void SetDefaultSelectionGroup7(Dictionary<string, string> group)
        {
            _defaultSelection.Group7 = group;
        }

        public void SetDefaultSelectionGroup8(Dictionary<string, string> group)
        {
            _defaultSelection.Group8 = group;
        }

        public void SaveSelection()
        {
            _defaultSelection.SaveSelection(Constants.Constants.DEFAULT_ZONE_SELECTION_FILE);
        }
        public Dictionary<string, string> GetGroupSelection(int group)
        {
            switch (group)
            {
                case 1:
                    return _defaultSelection.Group1;
                case 2:
                    return _defaultSelection.Group2;
                case 3:
                    return _defaultSelection.Group3;
                case 4:
                    return _defaultSelection.Group4;
                case 5:
                    return _defaultSelection.Group5;
                case 6:
                    return _defaultSelection.Group6;
                case 7:
                    return _defaultSelection.Group7;
                case 8:
                    return _defaultSelection.Group8;
                default:
                    return new Dictionary<string, string>();
            }
        }
    }
}

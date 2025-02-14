using Cognex.DataMan.SDK;
using Cognex.DataMan.SDK.Discovery;
using Cognex.DataMan.SDK.Utils;
using Intech_software.Constants;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace Intech_software.GUI
{
    public partial class frmDvvc : Form
    {
        public frmDvvc()
        {
            InitializeComponent();

            syncContext = SynchronizationContext.Current;
        }

        //Khởi tạo các lớp
        public static string revAccountName = string.Empty;

        //DataTable

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


        //PLC
        PLC plc = null;
        private string IP_ADDRESS_PLC = "192.168.3.250";
        private const int PORT_NUMBER_PLC = 24;
        int readConveyorState; // doc trang thai chay dung
        bool checkConnectPLC = false;// kiem tra ket noi plc


        private void frmDvvc_Load(object sender, EventArgs e)
        {
            try
            {
                #region DataMan Connection

                _ethSystemDiscoverer = new EthSystemDiscoverer();
                _ethSystemDiscoverer.SystemDiscovered += new EthSystemDiscoverer.SystemDiscoveredHandler(OnEthSystemDiscovered);
                _ethSystemDiscoverer.Discover();
                RefreshGui();

                #endregion

                #region PLC Connection

                plc = new PLC(IP_ADDRESS_PLC, PORT_NUMBER_PLC);
                Thread thConnect = new Thread(() =>
                {
                    ConnectPLC();
                })
                { IsBackground = true };
                thConnect.Start();

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmDvvc_FormClosing(object sender, FormClosingEventArgs e)
        {
            _closing = true;
            _autoconnect = false;

            if (null != mySystem && mySystem.State == global::Cognex.DataMan.SDK.ConnectionState.Connected)
                mySystem.Disconnect();
            _ethSystemDiscoverer?.Dispose();
            _ethSystemDiscoverer = null;

            if (plc != null)
                DisconnectPLC();
            if (serialPort1 != null && serialPort1.IsOpen)
                serialPort1.Close();

            plc = null;
            serialPort1 = null;
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

            //btnTrigger.Enabled = system_connected;
            //cbLiveDisplay.Enabled = system_connected;
        }

        #region PLC Events
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (plc != null)
                {
                    plc.StartPLC();
                }
                else
                    MessageBox.Show("PLC not connected");

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
                    plc.StopPLC();
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
                    listBoxLog.Invoke(new Action(() =>
                    {
                        listBoxLog.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " PLC: Connected.");
                    }));
                }
            }
            catch (EasyModbus.Exceptions.ConnectionException ex)
            {
                try
                {
                    listBoxLog.Invoke(new Action(() =>
                    {
                        listBoxLog.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " PLC: " + ex.Message);
                    }));
                }
                catch { }
            }
            catch (Exception ex)
            {
                try
                {
                    listBoxLog.Invoke(new Action(() =>
                    {
                        listBoxLog.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " PLC: " + ex.Message);
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
                    listBoxLog.Invoke(new Action(() =>
                    {
                        listBoxLog.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " PLC: Disconnected.");
                    }));
                }
            }
            catch (EasyModbus.Exceptions.ConnectionException ex)
            {
                listBoxLog.Invoke(new Action(() =>
                {
                    listBoxLog.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " PLC: " + ex.Message);
                }));
            }
            catch (Exception ex)
            {
                listBoxLog.Invoke(new Action(() =>
                {
                    listBoxLog.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " PLC: " + ex.Message);
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
                    mySystem.Disconnect();
                    mySystem = null;
                    return;
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
                //mySystem.BinaryDataTransferProgress += new BinaryDataTransferProgressHandler(OnBinaryDataTransferProgress);
                //mySystem.OffProtocolByteReceived += new OffProtocolByteReceivedHandler(OffProtocolByteReceived);
                //mySystem.AutomaticResponseArrived += new AutomaticResponseArrivedHandler(AutomaticResponseArrived);

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
                }
            }
            catch (Exception ex)
            {
                CleanupConnection();
                AddListItem(DateTime.Now.ToString("HH:mm:ss") + " DataMan: Failed to connect: " + ex.ToString());
            }
        }

        #endregion

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

        #endregion

        private void CleanupConnection()
        {
            if (null != mySystem)
            {
                mySystem.SystemConnected -= OnSystemConnected;
                mySystem.SystemDisconnected -= OnSystemDisconnected;
                mySystem.SystemWentOnline -= OnSystemWentOnline;
                mySystem.SystemWentOffline -= OnSystemWentOffline;
                mySystem.KeepAliveResponseMissed -= OnKeepAliveResponseMissed;
                //mySystem.BinaryDataTransferProgress -= OnBinaryDataTransferProgress;
                //mySystem.OffProtocolByteReceived -= OffProtocolByteReceived;
                //mySystem.AutomaticResponseArrived -= AutomaticResponseArrived;
            }

            _connector = null;
            mySystem = null;
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
            }

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
                listBoxLog.Items.Insert(0, item);

                if (listBoxLog.Items.Count > 500)
                    listBoxLog.Items.RemoveAt(listBoxLog.Items.Count - 1);
            }
        }

        private void ProcessScan(string trackingNumber)
        {
            try
            {
                uiDataScannedOrder.Rows.Insert(0);
                uiDataScannedOrder.Rows[0].Cells[0].Value = DateTime.Now.ToString("dd/MM hh:mm:ss");
                uiDataScannedOrder.Rows[0].Cells[1].Value = trackingNumber;

                if (trackingNumber.Length < 3)
                {
                    uiDataScannedOrder.Rows[0].Cells[2].Value = "Unknown";
                    return;
                }

                foreach (var prefix in Constants.ShippingUnit.PrefixZone1)
                {
                    if (trackingNumber.StartsWith(prefix))
                    {
                        uiDataScannedOrder.Rows[0].Cells[2].Value = "Shopee Express";
                        plc.PopGate(1);
                        return;
                    }
                }

                foreach (var prefix in Constants.ShippingUnit.PrefixZone2)
                {
                    if (trackingNumber.StartsWith(prefix))
                    {
                        uiDataScannedOrder.Rows[0].Cells[2].Value = "J&T";
                        plc.PopGate(2);
                        return;
                    }
                }

                foreach (var prefix in Constants.ShippingUnit.PrefixZone3)
                {
                    if (trackingNumber.StartsWith(prefix))
                    {
                        uiDataScannedOrder.Rows[0].Cells[2].Value = "Lazada";
                        plc.PopGate(3);
                        return;
                    }
                }

                foreach (var prefix in Constants.ShippingUnit.PrefixZone4)
                {
                    if (trackingNumber.StartsWith(prefix))
                    {
                        uiDataScannedOrder.Rows[0].Cells[2].Value = "Shopee";
                        plc.PopGate(4);
                        return;
                    }
                }

                foreach (var prefix in Constants.ShippingUnit.PrefixZone5)
                {
                    if (trackingNumber.StartsWith(prefix))
                    {
                        uiDataScannedOrder.Rows[0].Cells[2].Value = "TikTok";
                        plc.PopGate(5);
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                listBoxLog.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " " + ex.Message);
            }
        }
        int countClick = 0;
        private void lbReadString_Click(object sender, EventArgs e)
        {
            countClick++;
            AddListItem("lbReadString_Click" + countClick.ToString());
        }

        private void timerCheckPlc_Tick(object sender, EventArgs e)
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
                        //AddListItem("Conveyor is running");
                    }
                    if (readConveyorState == 0)
                    {
                        btnStart.BackColor = Color.LightGray;
                        btnStop.BackColor = Color.Orange;
                        //AddListItem("Conveyor has stopped");
                    }
                    if (readConveyorState == 2)
                    {
                        btnStart.BackColor = Color.LightGray;
                        btnStop.BackColor = Color.LightGray;
                        //AddListItem("Conveyor is paused");
                    }
                }
            }
            catch (Exception ex)
            {
                listBoxLog.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " -  PLC: " + ex.Message);
            }
        }

    }
}

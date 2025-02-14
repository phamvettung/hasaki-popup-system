using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Intech_software
{
    public class Dimension
    {
        public string Length { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
    }

    public class Parcel
    {
        public string ParcelCode { get; set; }
        public string Zone {  get; set; }
        public string Status { get; set; }
    }


    public class Camera
    {
        private Thread receivingThread;
        private NetworkStream networkStream;

        public TcpClient TcpClient { get; set; }
        public String Address { get; private set; }
        public int Port { get; private set; }
        public bool IsConnected { get; set; }

        public Camera()
        {
            IsConnected = false;
        }

        public void Connect(String address, int port)
        {
            try
            {
                Address = address;
                Port = port;
                TcpClient = new TcpClient();
                TcpClient.Connect(Address, Port);
                IsConnected = true;
                TcpClient.ReceiveBufferSize = 1024;
                TcpClient.SendBufferSize = 1024;

                networkStream = TcpClient.GetStream();

                receivingThread = new Thread(ReceivingMethod);
                receivingThread.IsBackground = true;
                receivingThread.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Disconnect()
        {
            IsConnected = true;
            TcpClient.Client.Disconnect(false);
            TcpClient.Close();
        }

        private void ReceivingMethod()
        {
            string msgQueue = string.Empty;
            byte[] buffer = new byte[TcpClient.ReceiveBufferSize];
            string msgReceiv = string.Empty;
            int ret = 0;
            while (IsConnected)
            {
                if (TcpClient.Available > 0)
                {
                    ret = networkStream.Read(buffer, 0, TcpClient.Available);
                    msgReceiv = Encoding.ASCII.GetString(buffer).Trim('\0');
                    msgQueue = string.Concat(msgQueue, msgReceiv);
                    Array.Clear(buffer, 0, ret);
                    HandleMessageQueue(ref msgQueue);

                }
                Thread.Sleep(30);
            }
        }

        private void HandleMessageQueue(ref string msgQueue)
        {
            int lastEtx = msgQueue.LastIndexOf('\n');
            if (lastEtx > 0)
            {
                string resMessage = msgQueue.Substring(0, lastEtx + 1);
                msgQueue = msgQueue.Remove(0, lastEtx + 1);
                string[] resMessageArr = resMessage.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    for (int i = 0; i < resMessageArr.Length; i++)
                    {
                        OnDataReceived(resMessageArr[i]);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }

        public delegate void DataReceivedDelegate(string result);
        public event DataReceivedDelegate OnDataReceived;
    }
}

using EasyModbus;
using System;
using System.Diagnostics;
using System.Threading;

namespace Intech_software
{
    public class PLC
    {
        private string ipAddress;
        private int port;
        private static readonly int START_STOP_ADDRESS = 1002;
        private static readonly int SIDE_GATE_ADDRESS = 1000;
        private static readonly int ACTION_START = 1;
        private static readonly int ACTION_STOP = 2;
        ModbusClient modbus;

        Thread readingThread = null;
        public delegate void DataControl();
        public static event DataControl OnDataControl;

        public string IPAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }

        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        public PLC()
        {
            this.ipAddress = "127.0.0.1";
            this.port = 0;
        }

        public PLC(string ipAddress, int port)
        {
            this.ipAddress = ipAddress;
            this.port = port;
        }
        public bool Connect()
        {
            try
            {
                modbus = new ModbusClient();
                modbus.IPAddress = this.ipAddress;
                modbus.Port = this.port;
                modbus.Connect();
                if (modbus.Connected == true)
                {
                    //readingThread = new Thread(ReadData);
                    //readingThread.IsBackground = true;
                    //readingThread.Start();
                    return true;
                }

                else
                    return false;
            }
            catch (EasyModbus.Exceptions.ConnectionException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        bool x15 = false, x15_active = false;
        private void ReadData()
        {
            while (modbus.Connected)
            {
                x15 = ReadCoils(8212);
                if (x15 == true && x15_active == false)
                {
                    x15_active = true;
                    //OnDataControl();
                }
                else if (x15 == false)
                {
                    x15_active = false;
                }
            }
        }

        public bool Disconnect()
        {
            try
            {
                modbus.Disconnect();
                if (modbus.Connected == true)
                    return true;
                else
                    return false;
            }
            catch (EasyModbus.Exceptions.ConnectionException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ReadHoldingRegisters(int startAdds)
        {
            try
            {
                if (modbus.Connected)
                {
                    return modbus.ReadHoldingRegisters(startAdds, 1)[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return 0;
        }

        public bool ReadCoils(int startAdds)
        {
            try
            {
                if (modbus.Connected)
                {
                    return modbus.ReadCoils(startAdds, 1)[0];
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool[] ReadZoneStatus(int startAdds)
        {
            try
            {
                if (modbus.Connected)
                {
                    return modbus.ReadCoils(startAdds, 41);
                }
                else
                    return new bool[41];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool[] ReadPopupError(int startAdds)
        {
            try
            {
                if (modbus.Connected)
                {
                    return modbus.ReadCoils(startAdds, 40);
                }
                else
                    return new bool[40];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ReadDiscreteInputs(int startAdds)
        {
            try
            {
                if (modbus.Connected)
                {
                    return modbus.ReadDiscreteInputs(startAdds, 1)[0];
                }
                else
                    return false;
            }
            catch (EasyModbus.Exceptions.ConnectionException ex)
            {
                throw ex;
            }
            catch (EasyModbus.Exceptions.CRCCheckFailedException ex)
            {
                throw ex;
            }
            catch (EasyModbus.Exceptions.FunctionCodeNotSupportedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void WriteSingleCoid(int startAdds, bool values)
        {
            try
            {
                if (modbus.Connected)
                {
                    modbus.WriteSingleCoil(startAdds, values);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void WriteSingleRegister(int startAdds, int value)
        {
            try
            {
                if (modbus.Connected)
                {
                    modbus.WriteSingleRegister(startAdds, value);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void StartPLC()
        {
            try
            {
                if (modbus.Connected)
                {
                    modbus.WriteSingleRegister(START_STOP_ADDRESS, ACTION_START);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void StopPLC()
        {
            try
            {
                if (modbus.Connected)
                {
                    modbus.WriteSingleRegister(START_STOP_ADDRESS, ACTION_STOP);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PopGate(int gate)
        {
            Console.WriteLine("send signal: " + gate);
            try
            {
                if (modbus.Connected)
                {
                    int d199 = 0;
                    Thread.Sleep(100);
                    d199 = ReadHoldingRegisters(199);
                    if (gate != d199)
                    {
                        modbus.WriteSingleRegister(SIDE_GATE_ADDRESS, gate);
                    }
                    else
                    {
                        Debug.WriteLine("trigger twice");
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
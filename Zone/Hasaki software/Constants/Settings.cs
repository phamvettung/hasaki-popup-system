using Cognex.DataMan.SDK.Discovery;
using System.IO.Ports;

namespace Intech_software.Constants
{
    static public class Settings
    {

        // Camera
        static public EthSystemDiscoverer.SystemInfo CameraDevice { get; set; } = null;
        static public string CameraUsername { get; set; } = "admin";
        static public string CameraPassword { get; set; } = string.Empty;

        // Scale
        static public string PortName { get; set; } = "COM3";
        static public int BaudRate { get; set; } = 19200;
        static public int DataBits { get; set; } = 8;
        static public StopBits StopBit { get; set; } = StopBits.One;
        static public Parity Parity { get; set; } = Parity.None;

        // PLC
        static public string IP_ADDRESS_PLC { get; set; } = "192.168.3.250";
        static public int PORT_PLC { get; set; } = 24;
    }
}

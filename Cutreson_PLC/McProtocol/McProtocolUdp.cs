using System.IO;
using System.Net.Sockets;
using System.Net;

namespace Cutreson_PLC.McProtocol
{
    public class McProtocolUdp : McProtocolApp
    {
        private UdpClient Client { get; set; }

        public McProtocolUdp(int iPortNumber)
            : this("", iPortNumber, "", "QSERISE")
        {
        }

        public McProtocolUdp(string iHostName, int iPortNumber, string connectiontype, string plctype)
            : base(iHostName, iPortNumber, connectiontype, plctype)
        {
            Client = new UdpClient(iPortNumber);
        }

        protected override bool IsConnected()
        {
            return false;
        }

        protected override void DoNetworkStreaminite()
        {
        }

        protected override bool DoConnect()
        {
            bool result = false;
            UdpClient client = Client;
            try
            {
                client.Connect(base.HostName, base.PortNumber);
                result = true;
            }
            catch
            {
            }
            return result;
        }

        protected override bool DoDisconnect()
        {
            return true;
        }

        protected override byte[] Execute(byte[] iCommand)
        {
            UdpClient client = Client;
            client.Send(iCommand, iCommand.Length);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                IPAddress address = IPAddress.Parse(base.HostName);
                IPEndPoint remoteEP = new IPEndPoint(address, base.PortNumber);
                do
                {
                    byte[] array = client.Receive(ref remoteEP);
                    memoryStream.Write(array, 0, array.Length);
                }
                while (0 < client.Available);
                return memoryStream.ToArray();
            }    
        }
    }
}

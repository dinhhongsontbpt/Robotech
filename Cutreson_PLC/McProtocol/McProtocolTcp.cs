using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;

namespace Cutreson_PLC.McProtocol
{
    public class McProtocolTcp : McProtocolApp
    {
        public delegate void PLCConnectionStatus(bool ConnectionFlag);

        private int iReceiveTimeout = 1000;

        private object obj = new object();

        private TcpClient client = null;

        private Stopwatch Watch = new Stopwatch();

        private bool PLCSendRecvTimeCheck = false;

        private NetworkStream nsStream { get; set; }

        public event PLCConnectionStatus UcPLCConnectionStatus;

        public McProtocolTcp()
            : this("", 0, "", "QSERISE")
        {
        }

        public McProtocolTcp(string iHostName, int iPortNumber, string connectiontype, string plctype, int ReceiveTimeout = 1000)
            : base(iHostName, iPortNumber, connectiontype, plctype)
        {
            client = new TcpClient();
            iReceiveTimeout = ReceiveTimeout;
        }

        protected override bool DoConnect()
        {
            if (!client.Connected)
            {
                List<byte> list = new List<byte>(12);
                list.AddRange(BitConverter.GetBytes(1u));
                list.AddRange(BitConverter.GetBytes(45000u));
                list.AddRange(BitConverter.GetBytes(5000u));
                client.Client.IOControl(IOControlCode.KeepAliveValues, list.ToArray(), null);
                try
                {
                    client.SendTimeout = 1000;
                    client.ReceiveTimeout = iReceiveTimeout;
                    client.Connect(base.HostName, base.PortNumber);
                    nsStream = client.GetStream();
                    if (this.UcPLCConnectionStatus != null)
                    {
                        this.UcPLCConnectionStatus(client.Connected);
                    }
                    //clsLog.WriteLog("McProtocolTcp", base.HostName + "[" + base.PortNumber + "] DoConnect() PLC 와 연결");
                }
                catch (Exception)
                {
                }
            }
            if (client == null)
            {
                if (this.UcPLCConnectionStatus != null)
                {
                    this.UcPLCConnectionStatus(ConnectionFlag: false);
                }
                return false;
            }
            return client.Connected;
        }

        protected override void DoNetworkStreaminite()
        {
            nsStream = null;
            if (client != null)
            {
                nsStream = client.GetStream();
            }
        }

        protected override bool DoDisconnect()
        {
            if (client != null)
            {
                try
                {
                    if (client.Connected)
                    {
                        //clsLog.WriteLog("McProtocolTcp", base.HostName + "[" + base.PortNumber + "] DoDisconnect() PLC 와 연결 종료");
                    }
                }
                catch
                {
                }
                client.Close();
                client = null;
                nsStream = null;
                if (this.UcPLCConnectionStatus != null)
                {
                    this.UcPLCConnectionStatus(ConnectionFlag: false);
                }
            }
            return true;
        }

        protected override byte[] Execute(byte[] iCommand)
        {
            if (client != null)
            {
                if (client.Connected)
                {
                    lock (this.obj)
                    {
                        NetworkStream networkStream = nsStream;
                        if (PLCSendRecvTimeCheck)
                        {
                            try
                            {
                                Watch.Restart();
                            }
                            catch
                            {
                            }
                        }
                        try
                        {
                            networkStream.Write(iCommand, 0, iCommand.Length);
                            networkStream.Flush();
                        }
                        catch (SocketException ex)
                        {
                            DoDisconnect();
                            //clsLog.WriteLog("McProtocolTcp", "Kefico_MES_PLC_McProtocol.Execute 명령어전송중 SocketException 발생 " + ex.ToString());
                            return null;
                        }
                        catch (Exception ex2)
                        {
                            //clsLog.WriteLog("McProtocolTcp", ex2.ToString());
                            try
                            {
                                DoDisconnect();
                               //clsLog.WriteLog("McProtocolTcp", "Kefico_MES_PLC_McProtocol.Execute  명령어전송중 Exception 발생으로인한 PLC 와 연결 종료");
                                return null;
                            }
                            catch (Exception ex3)
                            {
                                DoDisconnect();
                                //clsLog.WriteLog("McProtocolTcp", "Kefico_MES_PLC_McProtocol.Execute 명령어전송중 재접속Error 발생 " + ex3.ToString());
                                return null;
                            }
                        }
                        try
                        {
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                byte[] array = new byte[4096];
                                do
                                {
                                    try
                                    {
                                        int num = networkStream.Read(array, 0, array.Length);
                                        if (num == 0)
                                        {
                                            return null;
                                        }
                                        memoryStream.Write(array, 0, num);
                                    }
                                    catch (IOException ex4)
                                    {
                                        DoDisconnect();
                                        //clsLog.WriteLog("McProtocolTcp", "ReceiveTimeout: [" + networkStream.ReadTimeout + "] \n\r" + ex4.ToString());
                                        return null;
                                    }
                                    catch (Exception ex2)
                                    {
                                        DoDisconnect();
                                        //clsLog.WriteLog("McProtocolTcp", "[Receive] \n\r" + ex2.ToString());
                                        return null;
                                    }
                                }
                                while (networkStream.DataAvailable);
                                if (PLCSendRecvTimeCheck)
                                {
                                    try
                                    {
                                        Watch.Stop();
                                        //clsLog.WriteLog(base.HostName + "_" + base.PortNumber, "," + Watch.ElapsedMilliseconds);
                                    }
                                    catch
                                    {
                                    }
                                }
                                return memoryStream.ToArray();
                            }    
                            
                        }
                        catch (Exception ex2)
                        {
                            DoDisconnect();
                            //clsLog.WriteLog("McProtocolTcp", "Kefico_MES_PLC_McProtocol.Execute PLC 데이터 수신중 중 Exception   발생 " + ex2.ToString());
                            return null;
                        }
                    }
                }
                return null;
            }
            return null;
        }

        protected override bool IsConnected()
        {
            if (client != null)
            {
                return client.Connected;
            }
            return false;
        }
    }

}

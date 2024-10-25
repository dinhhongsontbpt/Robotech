using Cutreson_Utility;
using System;
using System.Collections.Generic;

namespace Cutreson_PLC.McProtocol
{
    public abstract class McProtocolApp
    {
        private object tlock = new object();

        public ConnectType ConnectedType { get; set; }

        public PLCType plcType { get; set; }

        public McFrame CommandFrame { get; set; }

        public string HostName { get; set; }

        public int PortNumber { get; set; }

        private McProtocol_BINARY_Command BINARY_Command { get; set; }

        private McProtocol_ASCII_Command ASCII_Command { get; set; }

        protected abstract void DoNetworkStreaminite();

        protected abstract bool DoConnect();

        protected abstract bool DoDisconnect();

        protected abstract byte[] Execute(byte[] iCommand);

        protected abstract bool IsConnected();

        protected McProtocolApp(string iHostName, int iPortNumber, string connectType, string plctype)
        {
            CommandFrame = McFrame.MC3E;
            HostName = iHostName;
            PortNumber = iPortNumber;
            if (connectType.ToUpper() == "ASCII")
            {
                ConnectedType = ConnectType.Ascii;
            }
            else
            {
                ConnectedType = ConnectType.Binary;
            }
            if (plctype.ToUpper() == "FSERISE")
            {
                plcType = PLCType.FSerise;
            }
            else
            {
                plcType = PLCType.QSerise;
            }
        }

        public void Dispose()
        {
            Close();
        }

        public bool IsConnect()
        {
            return IsConnected();
        }

        public bool Open()
        {
            bool flag = DoConnect();
            if (flag)
            {
                if (ConnectedType == ConnectType.Binary)
                {
                    BINARY_Command = new McProtocol_BINARY_Command(CommandFrame);
                }
                else
                {
                    ASCII_Command = new McProtocol_ASCII_Command();
                }
            }
            return flag;
        }

        public bool Close()
        {
            return DoDisconnect();
        }

        public void NetworkStreaminite()
        {
            DoNetworkStreaminite();
        }

        public bool SetBitDevice(PlcDeviceType iType, int iAddress, int iSize, int[] iData)
        {
            byte[] array = null;
            byte[] array2 = null;
            int num = -1;
            if (ConnectedType == ConnectType.Binary)
            {
                List<byte> list = new List<byte>(6);
                list.Add((byte)iAddress);
                list.Add((byte)(iAddress >> 8));
                list.Add((byte)(iAddress >> 16));
                list.Add((byte)iType);
                list.Add((byte)iSize);
                list.Add((byte)(iSize >> 8));
                List<byte> list2 = list;
                byte b = (byte)iData[0];
                int i;
                for (i = 0; i < iData.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        b = (byte)iData[i];
                        b = (byte)(b << 4);
                    }
                    else
                    {
                        b = (byte)(b | (byte)((uint)iData[i] & 1u));
                        list2.Add(b);
                    }
                }
                if (i % 2 != 0)
                {
                    list2.Add(b);
                }
                array = BINARY_Command.SetCommand(5121u, 1u, list2.ToArray());
                array2 = TryExecution(array);
                num = BINARY_Command.SetResponse(array2);
            }
            else
            {
                string strData = IntArrayToHex(iData, Utility.IsHexDevice(iType));
                string SendMsg = "";
                array = ASCII_Command.SetCommand("1401", iType.ToString(), iType, iAddress, iSize, out SendMsg, plcType, strData, "0001");
                array2 = TryExecution(array);
                byte[] Rbyte = null;
                num = ASCII_Command.SetResponse(array2, iType, iSize, SendMsg, out Rbyte, plcType);
            }
            if (num == 0)
            {
                return true;
            }
            return false;
        }

        public bool GetBitDevice(PlcDeviceType iType, int iAddress, int iSize, out int[] oData)
        {
            oData = new int[iSize];
            byte[] array = null;
            byte[] array2 = null;
            int num = -1;
            if (ConnectedType == ConnectType.Binary)
            {
                List<byte> list = new List<byte>(6);
                list.Add((byte)iAddress);
                list.Add((byte)(iAddress >> 8));
                list.Add((byte)(iAddress >> 16));
                list.Add((byte)iType);
                list.Add((byte)iSize);
                list.Add((byte)(iSize >> 8));
                List<byte> list2 = list;
                array = BINARY_Command.SetCommand(1025u, 1u, list2.ToArray());
                array2 = TryExecution(array);
                num = BINARY_Command.SetResponse(array2);
                byte[] response = BINARY_Command.Response;
                for (int i = 0; i < iSize; i++)
                {
                    if (i % 2 == 0)
                    {
                        oData[i] = ((num == 0) ? ((response[i / 2] >> 4) & 1) : 0);
                    }
                    else
                    {
                        oData[i] = ((num == 0) ? (response[i / 2] & 1) : 0);
                    }
                }
            }
            else
            {
                string SendMsg = "";
                array = ASCII_Command.SetCommand("0401", iType.ToString(), iType, iAddress, iSize, out SendMsg, plcType);
                array2 = TryExecution(array);
                if (array2 != null)
                {
                    if (plcType == PLCType.FSerise)
                    {
                        if (array2.Length >= 4)
                        {
                            byte[] Rbyte = null;
                            num = ASCII_Command.SetResponse(array2, iType, iSize, SendMsg, out Rbyte, plcType);
                        }
                    }
                    else if (array2.Length >= 22)
                    {
                        byte[] Rbyte = null;
                        num = ASCII_Command.SetResponse(array2, iType, iSize, SendMsg, out Rbyte, plcType);
                    }
                }
                oData = ASCII_Command.iResponse;
            }
            if (num == 0)
            {
                return true;
            }
            return false;
        }

        public bool WriteDeviceBlock(PlcDeviceType iType, int iAddress, int iSize, int[] iData)
        {
            if (BINARY_Command == null && ASCII_Command == null)
            {
                return false;
            }
            byte[] array = null;
            byte[] array2 = null;
            int num = -1;
            if (ConnectedType == ConnectType.Binary)
            {
                List<byte> list = new List<byte>(6);
                list.Add((byte)iAddress);
                list.Add((byte)(iAddress >> 8));
                list.Add((byte)(iAddress >> 16));
                list.Add((byte)iType);
                list.Add((byte)iSize);
                list.Add((byte)(iSize >> 8));
                List<byte> list2 = list;
                foreach (int num2 in iData)
                {
                    list2.Add((byte)num2);
                    list2.Add((byte)(num2 >> 8));
                }
                array = BINARY_Command.SetCommand(5121u, 0u, list2.ToArray());
                array2 = TryExecution(array);
                num = BINARY_Command.SetResponse(array2);
            }
            else
            {
                string strData = IntArrayToHex(iData, Utility.IsHexDevice(iType));
                string SendMsg = "";
                array = ASCII_Command.SetCommand("1401", iType.ToString(), iType, iAddress, iSize, out SendMsg, plcType, strData);
                array2 = TryExecution(array);
                byte[] Rbyte = null;
                num = ASCII_Command.SetResponse(array2, iType, iSize, SendMsg, out Rbyte, plcType);
            }
            if (num == 0)
            {
                return true;
            }
            return false;
        }

        private string IntArrayToHex(int[] iData, bool flag)
        {
            string text = "";
            string text2 = "";
            int num = 4;
            if (!flag)
            {
                for (int i = 0; i < iData.Length; i++)
                {
                    text2 += iData[i];
                }
            }
            else
            {
                for (int i = 0; i < iData.Length; i++)
                {
                    text = clsRadixTransformation.DecimalToHex(int.Parse(iData[i].ToString()));
                    if (num > text.Length)
                    {
                        text = clsRadixTransformation.ToLeftLen0(text, num);
                    }
                    text2 += text;
                }
            }
            return text2;
        }

        public bool WriteDeviceBlock(PlcDeviceType iType, int iAddress, int iSize, string Data)
        {
            if (BINARY_Command == null && ASCII_Command == null)
            {
                return false;
            }
            byte[] array = null;
            byte[] array2 = null;
            int num = -1;
            if (ConnectedType == ConnectType.Binary)
            {
                List<byte> list = new List<byte>(6);
                list.Add((byte)iAddress);
                list.Add((byte)(iAddress >> 8));
                list.Add((byte)(iAddress >> 16));
                list.Add((byte)iType);
                list.Add((byte)iSize);
                list.Add((byte)(iSize >> 8));
                List<byte> list2 = list;
                try
                {
                    for (int i = 0; i < Data.Length; i += 4)
                    {
                        list2.Add((byte)Convert.ToInt16(Data.Substring(i + 2, 2), 16));
                        list2.Add((byte)Convert.ToInt16(Data.Substring(i, 2), 16));
                    }
                }
                catch
                {
                    return false;
                }
                array = BINARY_Command.SetCommand(5121u, 0u, list2.ToArray());
                array2 = TryExecution(array);
                num = BINARY_Command.SetResponse(array2);
            }
            else
            {
                string SendMsg = "";
                array = ASCII_Command.SetCommand("1401", iType.ToString(), iType, iAddress, iSize, out SendMsg, plcType, Data);
                array2 = TryExecution(array);
                byte[] Rbyte = null;
                num = ASCII_Command.SetResponse(array2, iType, iSize, SendMsg, out Rbyte, plcType);
            }
            if (num == 0)
            {
                return true;
            }
            return false;
        }

        public byte[] ReadDeviceBlock(PlcDeviceType iType, int iAddress, int iSize)
        {
            if (BINARY_Command == null && ASCII_Command == null)
            {
                //clsLog.WriteLog("clsPLC", "ReadDeviceBlock(  " + iType.ToString() + "," + iAddress + "," + iSize);
                return null;
            }
            byte[] array = null;
            byte[] array2 = null;
            int num = -1;
            if (ConnectedType == ConnectType.Binary)
            {
                List<byte> list = new List<byte>(6);
                list.Add((byte)iAddress);
                list.Add((byte)(iAddress >> 8));
                list.Add((byte)(iAddress >> 16));
                list.Add((byte)iType);
                list.Add((byte)iSize);
                list.Add((byte)(iSize >> 8));
                List<byte> list2 = list;
                array = BINARY_Command.SetCommand(1025u, 0u, list2.ToArray());
                array2 = TryExecution(array);
                if (array2 != null)
                {
                    if (array2.Length == 4)
                    {
                        //if (clsInfo.SystemLog)
                        //{
                        //    clsLog.WriteLog("clsPLC", "NG  :ReadDeviceBlock(  " + iType.ToString() + "," + iAddress + "," + iSize + ")");
                        //}
                        return null;
                    }
                    //if (clsInfo.SystemLog)
                    //{
                    //    clsLog.WriteLog("clsPLC", "OK  :ReadDeviceBlock(  " + iType.ToString() + "," + iAddress + "," + iSize + ")");
                    //}
                    num = BINARY_Command.SetResponse(array2);
                    return BINARY_Command.Response;
                }
            }
            else
            {
                string SendMsg = "";
                array = ASCII_Command.SetCommand("0401", iType.ToString(), iType, iAddress, iSize, out SendMsg, plcType);
                array2 = TryExecution(array);
                //if (clsInfo.SystemLog)
                //{
                //    clsLog.WriteLog("PLCDATASIZE", iSize.ToString(), (array2.Length / 2 - 22).ToString());
                //}
                byte[] Rbyte = null;
                if (array2 != null)
                {
                    if (plcType == PLCType.QSerise)
                    {
                        if (array2.Length >= 22)
                        {
                            try
                            {
                                num = ASCII_Command.SetResponse(array2, iType, iSize, SendMsg, out Rbyte, plcType);
                            }
                            catch
                            {
                            }
                        }
                    }
                    else if (array2.Length >= 4)
                    {
                        try
                        {
                            num = ASCII_Command.SetResponse(array2, iType, iSize, SendMsg, out Rbyte, plcType);
                        }
                        catch
                        {
                        }
                    }
                    return Rbyte;
                }
            }
            return null;
        }

        public bool SetDevice(string iDeviceName, int iData)
        {
            Utility.GetDeviceCode(iDeviceName, out var oType, out var oAddress);
            return SetDevice(oType, oAddress, iData);
        }

        public bool SetDevice(PlcDeviceType iType, int iAddress, int iData)
        {
            if (BINARY_Command == null && ASCII_Command == null)
            {
                return false;
            }
            byte[] array = null;
            byte[] recv = null;
            int num = -1;
            if (ConnectedType == ConnectType.Binary)
            {
                List<byte> list = new List<byte>(6);
                list.Add((byte)iAddress);
                list.Add((byte)(iAddress >> 8));
                list.Add((byte)(iAddress >> 16));
                list.Add((byte)iType);
                list.Add(1);
                list.Add(0);
                list.Add((byte)iData);
                list.Add((byte)(iData >> 8));
                List<byte> list2 = list;
                array = BINARY_Command.SetCommand(5121u, 0u, list2.ToArray());
                recv = TryExecution(array);
                num = BINARY_Command.SetResponse(recv);
            }
            else
            {
                string text = clsRadixTransformation.DecimalToHex(iData);
                text = text.PadLeft(4, '0');
                string SendMsg = "";
                array = ASCII_Command.SetCommand("1401", iType.ToString(), iType, iAddress, 1, out SendMsg, plcType, text);
                try
                {
                    recv = TryExecution(array);
                }
                catch
                {
                }
                byte[] Rbyte = null;
                num = ASCII_Command.SetResponse(recv, iType, 1, SendMsg, out Rbyte, plcType);
            }
            if (num == 0)
            {
                return true;
            }
            return false;
        }

        public bool SetDevice(PlcDeviceType iType, int iAddress, string Data)
        {
            if (BINARY_Command == null && ASCII_Command == null)
            {
                return false;
            }
            byte[] array = null;
            byte[] array2 = null;
            int num = -1;
            if (ConnectedType == ConnectType.Binary)
            {
                List<byte> list = new List<byte>(6);
                list.Add((byte)iAddress);
                list.Add((byte)(iAddress >> 8));
                list.Add((byte)(iAddress >> 16));
                list.Add((byte)iType);
                list.Add(1);
                list.Add(0);
                List<byte> list2 = list;
                try
                {
                    list2.Add((byte)int.Parse(Data.Substring(2, 2)));
                    list2.Add((byte)int.Parse(Data.Substring(0, 2)));
                }
                catch
                {
                    return false;
                }
                array = BINARY_Command.SetCommand(5121u, 0u, list2.ToArray());
                array2 = TryExecution(array);
                num = BINARY_Command.SetResponse(array2);
            }
            else
            {
                string SendMsg = "";
                array = ASCII_Command.SetCommand("1401", iType.ToString(), iType, iAddress, 1, out SendMsg, plcType, Data.Substring(2, 2) + Data.Substring(0, 2));
                array2 = TryExecution(array);
                byte[] Rbyte = null;
                num = ASCII_Command.SetResponse(array2, iType, 1, SendMsg, out Rbyte, plcType);
            }
            if (num == 0)
            {
                return true;
            }
            return false;
        }

        public byte[] GetDevice(string iDeviceName)
        {
            if (BINARY_Command == null && ASCII_Command == null)
            {
                return null;
            }
            Utility.GetDeviceCode(iDeviceName, out var oType, out var oAddress);
            return GetDevice(oType, oAddress);
        }

        public byte[] GetDevice(PlcDeviceType iType, int iAddress)
        {
            if (BINARY_Command == null && ASCII_Command == null)
            {
                return null;
            }
            byte[] array = null;
            byte[] array2 = null;
            int num = -1;
            if (ConnectedType == ConnectType.Binary)
            {
                List<byte> list = new List<byte>(6);
                list.Add((byte)iAddress);
                list.Add((byte)(iAddress >> 8));
                list.Add((byte)(iAddress >> 16));
                list.Add((byte)iType);
                list.Add(1);
                list.Add(0);
                List<byte> list2 = list;
                array = BINARY_Command.SetCommand(1025u, 0u, list2.ToArray());
                array2 = TryExecution(array);
                if (array2 != null)
                {
                    num = BINARY_Command.SetResponse(array2);
                    return BINARY_Command.Response;
                }
            }
            else
            {
                string SendMsg = "";
                array = ASCII_Command.SetCommand("0401", iType.ToString(), iType, iAddress, 1, out SendMsg, plcType);
                array2 = TryExecution(array);
                if (array2 != null && array2.Length >= 22)
                {
                    byte[] Rbyte = null;
                    num = ASCII_Command.SetResponse(array2, iType, 1, SendMsg, out Rbyte, plcType);
                    return Rbyte;
                }
            }
            return null;
        }

        private byte[] TryExecution(byte[] iCommand)
        {
            int num = 10;
            byte[] array;
            if (ConnectedType != ConnectType.Binary)
            {
                lock (tlock)
                {
                    do
                    {
                        array = Execute(iCommand);
                        if (array == null)
                        {
                            break;
                        }
                        num--;
                        if (num < 0)
                        {
                            throw new Exception("PLC Comunication Error");
                        }
                    }
                    while (ASCII_Command.IsIncorrectResponse(array, plcType));
                }
            }
            else
            {
                do
                {
                    array = Execute(iCommand);
                    if (array == null)
                    {
                        break;
                    }
                    num--;
                    if (num < 0)
                    {
                        throw new Exception("PLC Comunication Error");
                    }
                }
                while (BINARY_Command.IsIncorrectResponse(array));
            }
            return array;
        }
    }

}

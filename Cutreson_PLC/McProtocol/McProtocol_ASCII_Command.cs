using Cutreson_Utility;
using System.Text;

namespace Cutreson_PLC.McProtocol
{
    internal class McProtocol_ASCII_Command
    {
        private string SerialNumber { get; set; }

        private string NetwrokNumber { get; set; }

        private string PcNumber { get; set; }

        private string IoNumber { get; set; }

        private string ChannelNumber { get; set; }

        private string CpuTimer { get; set; }

        private int ResultCode { get; set; }

        public int[] iResponse { get; private set; }

        public static string DeviceCode { get; set; }

        public McProtocol_ASCII_Command()
        {
            SerialNumber = "????";
            NetwrokNumber = "00";
            PcNumber = "FF";
            IoNumber = "03E0";
            ChannelNumber = "00";
            CpuTimer = "0010";
            DeviceCode = "*";
        }

        public byte[] SetCommand(string iMainCommand, string field, PlcDeviceType type, int StartAddr, int iSize, out string SendMsg, PLCType plctype, string strData = "", string iSubCommand = "0000")
        {
            SendMsg = "";
            if (field == "C")
            {
                field = "ZR";
            }
            if (plctype == PLCType.QSerise)
            {
                if (!Utility.IsHexDevice(type))
                {
                    if (iMainCommand == "0401")
                    {
                        SendMsg = clsMelsecQ.ReadWord(field, StartAddr, StartAddr + iSize - 1);
                    }
                    else
                    {
                        SendMsg = clsMelsecQ.WriteWord(field, StartAddr, iSize, strData, iSubCommand);
                    }
                }
                else if (iMainCommand == "0401")
                {
                    SendMsg = clsMelsecQ.ReadWord(field, StartAddr, StartAddr + iSize - 1);
                }
                else
                {
                    SendMsg = clsMelsecQ.WriteWord(field, StartAddr, iSize, strData);
                }
            }
            else
            {
                string text = "00";
                if (iMainCommand == "0401")
                {
                    if (Utility.IsHexDevice(type))
                    {
                        text = "01";
                    }
                    SendMsg = text + "FF000A" + clsRadixTransformation.CharToHex(field) + "20" + clsRadixTransformation.ToLeftLen0(clsRadixTransformation.DecimalToHex(StartAddr), 8) + clsRadixTransformation.ToLeftLen0(clsRadixTransformation.DecimalToHex(iSize), 2) + "00";
                }
                else
                {
                    text = "03";
                    SendMsg = text + "FF000A" + clsRadixTransformation.CharToHex(field) + "20" + clsRadixTransformation.ToLeftLen0(clsRadixTransformation.DecimalToHex(StartAddr), 8) + clsRadixTransformation.ToLeftLen0(clsRadixTransformation.DecimalToHex(iSize), 2) + "00" + strData;
                }
            }
            return Encoding.ASCII.GetBytes(SendMsg);
        }

        public int SetResponse(byte[] recv, PlcDeviceType type, int size, string SendMsg, out byte[] Rbyte, PLCType plctype)
        {
            Rbyte = null;
            if (recv != null)
            {
                if (plctype == PLCType.QSerise)
                {
                    int num = 14;
                    string text = "";
                    try
                    {
                        text = Encoding.Default.GetString(recv, 0, recv.Length);
                    }
                    catch
                    {
                    }
                    if (text.Length < 22)
                    {
                        return -1;
                    }
                    if (text == "D00000FF03E00000040000")
                    {
                        return 0;
                    }
                    if (num <= recv.Length)
                    {
                        try
                        {
                            if (Utility.IsHexDevice(type))
                            {
                                string hex = text.Substring(14, 4);
                                int num2 = 0;
                                try
                                {
                                    num2 = clsRadixTransformation.HexToDecimal64(hex) - 4;
                                }
                                catch
                                {
                                }
                                if (num2 <= 0)
                                {
                                    return -1;
                                }
                                string text2 = text.Substring(22);
                                Rbyte = new byte[num2 / 2];
                                int num3 = 0;
                                int i = 0;
                                try
                                {
                                    for (; i < text2.Length; i += 2)
                                    {
                                        if (num3 == 0)
                                        {
                                            Rbyte[1] = (byte)clsRadixTransformation.HexToDecimal64(text2.Substring(i, 2));
                                        }
                                        else if (num3 % 2 == 1)
                                        {
                                            Rbyte[num3 - 1] = (byte)clsRadixTransformation.HexToDecimal64(text2.Substring(i, 2));
                                        }
                                        else
                                        {
                                            Rbyte[num3 + 1] = (byte)clsRadixTransformation.HexToDecimal64(text2.Substring(i, 2));
                                        }
                                        num3++;
                                    }
                                }
                                catch
                                {
                                    return -1;
                                }
                            }
                            else
                            {
                                string text2 = text.Substring(22);
                                if (text2 == "")
                                {
                                    return 0;
                                }
                                if (text2.Length != size)
                                {
                                    //if (clsInfo.SystemLog)
                                    //{
                                    //    clsLog.WriteLog("McProtocolTcp", "SetResponse", SendMsg + "    " + text);
                                    //}
                                    return -1;
                                }
                                try
                                {
                                    //if (clsInfo.SystemLog)
                                    //{
                                    //    clsLog.WriteLog("McProtocolTcp", "SetResponse", SendMsg + "    " + text);
                                    //}
                                    Rbyte = new byte[size];
                                    iResponse = new int[size];
                                }
                                catch
                                {
                                    return -1;
                                }
                                for (int i = 0; i < text2.Length; i++)
                                {
                                    Rbyte[i] = byte.Parse(text2.Substring(i, 1));
                                    iResponse[i] = Rbyte[i];
                                }
                            }
                        }
                        catch
                        {
                        }
                        ResultCode = 0;
                    }
                }
                else
                {
                    int num = 4;
                    string text = "";
                    try
                    {
                        text = Encoding.Default.GetString(recv, 0, recv.Length);
                    }
                    catch
                    {
                    }
                    if (text.Length < 4)
                    {
                        return -1;
                    }
                    if (text == "8300")
                    {
                        return 0;
                    }
                    if (num <= recv.Length)
                    {
                        try
                        {
                            if (Utility.IsHexDevice(type))
                            {
                                int num2 = size * 4;
                                if (num2 <= 0)
                                {
                                    return -1;
                                }
                                string text2 = text.Substring(4);
                                Rbyte = new byte[num2 / 2];
                                int num3 = 0;
                                int i = 0;
                                try
                                {
                                    for (; i < text2.Length; i += 2)
                                    {
                                        if (num3 == 0)
                                        {
                                            Rbyte[1] = (byte)clsRadixTransformation.HexToDecimal64(text2.Substring(i, 2));
                                        }
                                        else if (num3 % 2 == 1)
                                        {
                                            Rbyte[num3 - 1] = (byte)clsRadixTransformation.HexToDecimal64(text2.Substring(i, 2));
                                        }
                                        else
                                        {
                                            Rbyte[num3 + 1] = (byte)clsRadixTransformation.HexToDecimal64(text2.Substring(i, 2));
                                        }
                                        num3++;
                                    }
                                }
                                catch
                                {
                                    return -1;
                                }
                            }
                            else
                            {
                                string text2 = text.Substring(4);
                                if (text2 == "")
                                {
                                    return 0;
                                }
                                if (text2.Length != size + 1)
                                {
                                    if (text2.Length != size)
                                    {
                                        //if (clsInfo.SystemLog)
                                        //{
                                        //    clsLog.WriteLog("McProtocolTcp", "SetResponse", SendMsg + "    " + text);
                                        //}
                                        return -1;
                                    }
                                    //if (clsInfo.SystemLog)
                                    //{
                                    //    clsLog.WriteLog("McProtocolTcp", "SetResponse", SendMsg + "    " + text);
                                    //}
                                    Rbyte = new byte[size];
                                    iResponse = new int[size];
                                }
                                try
                                {
                                    //if (clsInfo.SystemLog)
                                    //{
                                    //    clsLog.WriteLog("McProtocolTcp", "SetResponse", SendMsg + "    " + text);
                                    //}
                                    Rbyte = new byte[size];
                                    iResponse = new int[size];
                                }
                                catch
                                {
                                    return -1;
                                }
                                for (int i = 0; i < text2.Length; i++)
                                {
                                    Rbyte[i] = byte.Parse(text2.Substring(i, 1));
                                    iResponse[i] = Rbyte[i];
                                }
                            }
                        }
                        catch
                        {
                        }
                        ResultCode = 0;
                    }
                }
                return ResultCode;
            }
            return -1;
        }

        private byte[] arrySwap(byte[] arry)
        {
            byte[] array = new byte[arry.Length];
            for (int i = 0; i < arry.Length; i++)
            {
                if (i == 0)
                {
                    array[1] = arry[0];
                }
                else if (i % 2 == 1)
                {
                    array[i - 1] = arry[i];
                }
                else
                {
                    array[i + 1] = arry[i];
                }
            }
            return array;
        }

        public bool IsIncorrectResponse(byte[] iResponse, PLCType plctype)
        {
            if (iResponse != null)
            {
                bool result = false;
                if (plctype == PLCType.QSerise)
                {
                    int num = 22;
                    byte[] array = new byte[4]
                    {
                    iResponse[num - 8],
                    iResponse[num - 7],
                    iResponse[num - 6],
                    iResponse[num - 5]
                    };
                    byte[] array2 = new byte[4]
                    {
                    iResponse[num - 4],
                    iResponse[num - 3],
                    iResponse[num - 2],
                    iResponse[num - 1]
                    };
                    string @string = Encoding.Default.GetString(array, 0, array.Length);
                    string string2 = Encoding.Default.GetString(array2, 0, array2.Length);
                    int num2 = clsRadixTransformation.HexToDecimal16(@string) - 4;
                    return clsRadixTransformation.HexToDecimal16(string2) == 0 && num2 != iResponse.Length - num;
                }
                try
                {
                    result = false;
                }
                catch
                {
                }
                return result;
            }
            return false;
        }
    }

}

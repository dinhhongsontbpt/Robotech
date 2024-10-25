using Cutreson_Utility;
using System.Data;

namespace Cutreson_PLC.McProtocol
{
    public enum PLCWriteType
    {
        ToHex,
        ToDec
    }
    public enum PLCConnectionType
    {
        Ascii,
        Binary
    }
    public class clsMelsecQ
    {
        private static PLCConnectionType PLCConType = PLCConnectionType.Ascii;

        private static string _header = "500000FF03E000";

        private static string _cpuTimer = "0010";

        private static string _devicecode = "*";

        public static PLCConnectionType UcPLCConnectType
        {
            get
            {
                return PLCConType;
            }
            set
            {
                PLCConType = value;
            }
        }

        public static string Header
        {
            get
            {
                return _header;
            }
            set
            {
                _header = value;
            }
        }

        public static string CPUTimer
        {
            get
            {
                return _cpuTimer;
            }
            set
            {
                _cpuTimer = value;
            }
        }

        public static string DeviceCode
        {
            get
            {
                return _devicecode;
            }
            set
            {
                _devicecode = value;
            }
        }

        public static string ReadWord(string field, int Address)
        {
            string text = "";
            string text2 = Address.ToString("000000");
            string text3 = "0401";
            string text4 = "0000";
            int num = 1;
            text = CPUTimer + text3 + text4 + field + DeviceCode + text2 + num.ToString("0000");
            string org_data = clsRadixTransformation.DecimalToHex(text.Length);
            return Header + clsRadixTransformation.ToLeftLen0(org_data, 4) + text;
        }

        public static string ReadWord(string field, string Address, int iSize)
        {
            string text = "";
            string text2 = clsRadixTransformation.ToLeftLen0(Address, 6);
            string text3 = "0401";
            string text4 = "0000";
            text = CPUTimer + text3 + text4 + field + DeviceCode + text2 + iSize.ToString("0000");
            string org_data = clsRadixTransformation.DecimalToHex(text.Length);
            return Header + clsRadixTransformation.ToLeftLen0(org_data, 4) + text;
        }

        public static string ReadWord(string field, int minAddr, int maxAddr)
        {
            string text = "";
            int num = 0;
            num = ((minAddr == maxAddr) ? 1 : (maxAddr - minAddr + 1));
            string text2 = clsRadixTransformation.DecimalToHex(num);
            if (text2.Length != 4)
            {
                text2 = clsRadixTransformation.ToLeftLen0(text2, 4);
            }
            string text3 = minAddr.ToString("000000");
            string text4 = "0401";
            string text5 = "0000";
            if (field == "M" || field == "L" || field == "B")
            {
                text5 = "0001";
            }
            if (field == "ZR")
            {
                text3 = clsRadixTransformation.DecimalToHex(minAddr);
                text = CPUTimer + text4 + text5 + field + text3.PadLeft(6, '0') + text2;
            }
            else
            {
                text = CPUTimer + text4 + text5 + field + DeviceCode + text3 + text2;
            }
            string org_data = clsRadixTransformation.DecimalToHex(text.Length);
            return Header + clsRadixTransformation.ToLeftLen0(org_data, 4) + text;
        }

        public static string ReadWord(DataRow dr)
        {
            string text = "";
            int num = 0;
            string text2 = int.Parse(dr[3].ToString()).ToString("000000");
            string text3 = "0401";
            string text4 = "0000";
            int num2 = 1;
            text = CPUTimer + text3 + text4 + "D " + text2 + num2.ToString("0000");
            string org_data = clsRadixTransformation.DecimalToHex(text.Length);
            return Header + clsRadixTransformation.ToLeftLen0(org_data, 4) + text;
        }

        public static string ReadWord(DataRow[] dr)
        {
            string text = "";
            int num = 0;
            int num2 = 0;
            num = int.Parse(dr[0][3].ToString());
            num2 = int.Parse(dr[dr.Length - 1][3].ToString());
            int num3 = 0;
            num3 = ((num == num2) ? 1 : (num2 - num + 1));
            string text2 = num.ToString("000000");
            string text3 = "0401";
            string text4 = "0000";
            text = CPUTimer + text3 + text4 + "D " + text2 + num3.ToString("0000");
            string org_data = clsRadixTransformation.DecimalToHex(text.Length);
            return Header + clsRadixTransformation.ToLeftLen0(org_data, 4) + text;
        }

        public static string WriteWord(string field, int iAddr, string WriteData, PLCWriteType plcwt)
        {
            string text = "";
            string text2 = "";
            if (plcwt == PLCWriteType.ToDec)
            {
                if (clsText.TextCheck(clsText.TextType.DecimalDigitNumber, WriteData))
                {
                    int @decimal = int.Parse(WriteData);
                    text2 = clsRadixTransformation.ToLeftLen0(clsRadixTransformation.DecimalToHex(@decimal), 4);
                }
            }
            else
            {
                text2 = WriteData;
            }
            string text3 = iAddr.ToString("000000");
            string text4 = "1401";
            string text5 = "0000";
            int num = 1;
            text = CPUTimer + text4 + text5 + field + DeviceCode + text3 + num.ToString("0000") + text2;
            string org_data = clsRadixTransformation.DecimalToHex(text.Length);
            return Header + clsRadixTransformation.ToLeftLen0(org_data, 4) + text;
        }

        public static string WriteWord(string field, int iAddr, int TotalAddr, string WriteData, string SubCommand = "0000")
        {
            string text = "";
            string text2 = iAddr.ToString("000000");
            string text3 = "1401";
            string org_data = clsRadixTransformation.DecimalToHex(TotalAddr);
            if (field == "ZR")
            {
                text2 = clsRadixTransformation.DecimalToHex(iAddr);
                text = CPUTimer + text3 + SubCommand + field + text2.PadLeft(6, '0') + clsRadixTransformation.ToLeftLen0(org_data, 4) + WriteData;
            }
            else
            {
                text = CPUTimer + text3 + SubCommand + field + DeviceCode + text2 + clsRadixTransformation.ToLeftLen0(org_data, 4) + WriteData;
            }
            string org_data2 = clsRadixTransformation.DecimalToHex(text.Length);
            return Header + clsRadixTransformation.ToLeftLen0(org_data2, 4) + text;
        }

        public static string WriteWord(string ReceiveData, DataRow dr)
        {
            string text = "";
            int num = 0;
            int num2 = int.Parse(dr[5].ToString());
            int num3 = int.Parse(dr[4].ToString());
            string text2 = "";
            string binary = "";
            string text3 = "";
            string text4 = "";
            num = int.Parse(dr[2].ToString());
            text2 = clsRadixTransformation.HexToBinary(ReceiveData.Substring(num3 - 1, 1));
            switch (num2)
            {
                case 1:
                    binary = "1" + text2.Substring(1);
                    break;
                case 2:
                    binary = text2.Substring(0, 1) + "1" + text2.Substring(2);
                    break;
                case 3:
                    binary = text2.Substring(0, 2) + "1" + text2.Substring(3);
                    break;
                case 4:
                    binary = text2.Substring(0, 3) + "1";
                    break;
            }
            text3 = clsRadixTransformation.BinaryToHex(binary);
            switch (num3)
            {
                case 1:
                    text4 = text3 + ReceiveData.Substring(1);
                    break;
                case 2:
                    text4 = ReceiveData.Substring(0, 1) + text3 + ReceiveData.Substring(2);
                    break;
                case 3:
                    text4 = ReceiveData.Substring(0, 2) + text3 + ReceiveData.Substring(3);
                    break;
                case 4:
                    text4 = ReceiveData.Substring(0, 3) + text3;
                    break;
            }
            string text5 = num.ToString("000000");
            string text6 = "1401";
            string text7 = "0000";
            int num4 = 1;
            text = CPUTimer + text6 + text7 + "D " + text5 + num4.ToString("0000") + text4;
            string org_data = clsRadixTransformation.DecimalToHex(text.Length);
            return Header + clsRadixTransformation.ToLeftLen0(org_data, 4) + text;
        }

        public static string WriteWord(string ReceiveData, DataRow[] dr)
        {
            string text = "";
            ReceiveData = ReceiveData.Substring(22);
            int num = ReceiveData.Length / 4;
            string[] array = new string[num];
            string[] array2 = new string[num];
            int num2 = int.Parse(dr[0][2].ToString());
            int num3 = 0;
            int num4 = 0;
            for (int i = 0; i < num; i++)
            {
                array[i] = num2 + "," + ReceiveData.Substring(num4, 4);
                array2[i] = num2 + "," + ReceiveData.Substring(num4, 4);
                num4 += 4;
                num2++;
            }
            for (int i = 0; i < dr.Length; i++)
            {
                num3 = int.Parse(dr[i][2].ToString());
                for (int j = 0; j < array2.Length; j++)
                {
                    if (num3.ToString() == array2[j].Substring(0, 4))
                    {
                        string[] array3 = array2[j].Split(',');
                        array3[1] = SetValue(array3[1], dr[i]);
                        array2[j] = num3 + "," + array3[1];
                        break;
                    }
                }
            }
            int num5 = 0;
            string text2 = "1401";
            string text3 = "0000";
            for (int k = 0; k < array2.Length; k++)
            {
                if (array[k] != array2[k])
                {
                    num5++;
                }
            }
            if (num5 == 0)
            {
                return "";
            }
            if (num5 > 1)
            {
                for (int k = 0; k < array2.Length; k++)
                {
                    if (array[k] != array2[k])
                    {
                        string[] array3 = array2[k].Split(',');
                        text = text + "D " + int.Parse(array3[0]).ToString("000000") + array3[1];
                    }
                }
                text2 = "1402";
                text = CPUTimer + text2 + text3 + clsRadixTransformation.ToLeftLen0(clsRadixTransformation.DecimalToHex(num5), 2) + "00" + text;
            }
            else
            {
                for (int k = 0; k < array2.Length; k++)
                {
                    if (array[k] != array2[k])
                    {
                        string[] array3 = array2[k].Split(',');
                        int num6 = int.Parse(array3[0]);
                        text = text + "D " + num6.ToString("000000") + num5.ToString("0000") + array3[1];
                    }
                }
                text2 = "1401";
                text = CPUTimer + text2 + text3 + text;
            }
            string org_data = clsRadixTransformation.DecimalToHex(text.Length);
            return Header + clsRadixTransformation.ToLeftLen0(org_data, 4) + text;
        }

        private static string SetValue(string StrValue, DataRow dr)
        {
            string text = "";
            int num = int.Parse(dr[5].ToString());
            int num2 = int.Parse(dr[4].ToString());
            string result = "";
            string binary = "";
            text = clsRadixTransformation.HexToBinary(StrValue.Substring(num2 - 1, 1));
            switch (num)
            {
                case 1:
                    binary = "1" + text.Substring(1);
                    break;
                case 2:
                    binary = text.Substring(0, 1) + "1" + text.Substring(2);
                    break;
                case 3:
                    binary = text.Substring(0, 2) + "1" + text.Substring(3);
                    break;
                case 4:
                    binary = text.Substring(0, 3) + "1";
                    break;
            }
            binary = clsRadixTransformation.BinaryToHex(binary);
            switch (num2)
            {
                case 1:
                    result = binary + StrValue.Substring(1);
                    break;
                case 2:
                    result = StrValue.Substring(0, 1) + binary + StrValue.Substring(2);
                    break;
                case 3:
                    result = StrValue.Substring(0, 2) + binary + StrValue.Substring(3);
                    break;
                case 4:
                    result = StrValue.Substring(0, 3) + binary;
                    break;
            }
            return result;
        }

        public static PLCState PLCStats(string ReceiveData)
        {
            PLCState result = PLCState.None;
            string text = clsRadixTransformation.HexToBinary(ReceiveData.Substring(3, 1));
            if (text.Substring(1, 1).ToString() == "1")
            {
                result = PLCState.OffLine;
            }
            if (text.Substring(2, 1).ToString() == "1")
            {
                result = PLCState.OnLine;
            }
            return result;
        }
    }
}

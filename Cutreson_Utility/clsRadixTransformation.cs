using System.Text;
using System;

namespace Cutreson_Utility
{
    public class clsRadixTransformation
    {
        public static string ToLeftLen0(string org_data, int len)
        {
            return org_data.PadLeft(len, '0');
        }

        public static string ToRightLen0(string org_data, int len)
        {
            return org_data.PadRight(len, '0');
        }

        public static int BinaryToDecimal(string binary)
        {
            int num = 0;
            return Convert.ToInt32(binary, 2);
        }

        public static string DecimalToBinary(int Decimal)
        {
            string text = "";
            text = Convert.ToString(Decimal, 2);
            return ToLeftLen0(text, 4);
        }

        public static string DecimalToHex(int Decimal)
        {
            string text = "";
            text = Convert.ToString(Decimal, 16);
            return text.ToUpper();
        }

        public static string DecimalToHex(long Decimal)
        {
            string text = "";
            text = Convert.ToString(Decimal, 16);
            return text.ToUpper();
        }

        public static char HexToString(string Hex)
        {
            return Convert.ToChar(HexToDecimal64(Hex));
        }

        private static string DecToHex(int num)
        {
            byte[] bytes = BitConverter.GetBytes(num);
            string text = "";
            return text + bytes[0].ToString("X2");
        }

        public static int HexToDecimal16(string Hex)
        {
            int num = 0;
            return Convert.ToInt16(Hex, 16);
        }

        public static int HexToDecimal64(string Hex)
        {
            int num = 0;
            return (int)Convert.ToInt64(Hex, 16);
        }

        public static int HexToDecimal64_PLC_ASCII(string Hex)
        {
            if (Hex.Length == 2)
            {
                Hex = Hex.Substring(1, 1) + Hex.Substring(0, 1);
            }
            int num = 0;
            return (int)Convert.ToInt64(Hex, 16);
        }

        public static string HexToBinary(string Hex)
        {
            int @decimal = HexToDecimal16(Hex);
            return DecimalToBinary(@decimal);
        }

        public static string BinaryToHex(string binary)
        {
            int @decimal = BinaryToDecimal(binary);
            return DecimalToHex(@decimal);
        }

        public static int CharToDec(string str)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(str);
            return Convert.ToInt16(bytes[0]);
        }

        public static int CharToDec_Default(string str)
        {
            byte[] bytes = Encoding.GetEncoding(1252).GetBytes(str);
            return Convert.ToInt16(bytes[0]);
        }

        public static string CharToHex(string str)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(str);
            return DecimalToHex(Convert.ToInt16(bytes[0]));
        }

        public static string StringToHex(string str)
        {
            string text = "";
            byte[] bytes = Encoding.Unicode.GetBytes(str);
            for (int i = 0; i < bytes.Length; i++)
            {
                if (i == 0 || i % 2 == 0)
                {
                    text += DecimalToHex(Convert.ToInt16(bytes[i]));
                }
            }
            return text;
        }

        public static string StringToPLCHEX(string str)
        {
            string text = "";
            string text2 = "";
            for (int i = 0; i < str.Length; i++)
            {
                byte[] bytes = Encoding.Unicode.GetBytes(str.Substring(i, 1));
                if (i == 0)
                {
                    text2 = DecimalToHex(Convert.ToInt16(bytes[0]));
                }
                else
                {
                    int num = i % 2;
                    text2 = ((num != 1) ? DecimalToHex(Convert.ToInt16(bytes[0])) : (DecimalToHex(Convert.ToInt16(bytes[0])) + text2));
                }
                if (text2.Length == 4)
                {
                    text += text2;
                    text2 = "";
                }
            }
            if (text2.Length != 0)
            {
                text = text + "00" + text2;
            }
            return text;
        }

        public static string PLCHEXToString(string str)
        {
            string text = "";
            string text2 = "";
            string text3 = "";
            string text4 = "";
            for (int i = 0; i < str.Length / 4; i++)
            {
                text2 = str.Substring(i * 4, 4);
                text3 = text2.Substring(0, 2);
                text4 = text2.Substring(2, 2);
                if (text4 != "00")
                {
                    text += Convert.ToString((char)HexToDecimal16(text4));
                }
                if (text3 != "00")
                {
                    text += Convert.ToString((char)HexToDecimal16(text3));
                }
            }
            return text.Trim();
        }

        public static int[] StringToIntArray(string input, int arrayLength)
        {
            int requiredLength = arrayLength * 2;
            if (input.Length < requiredLength)
            {
                input = input.PadRight(requiredLength);
            }
            else if (input.Length > requiredLength)
            {
                input = input.Substring(0, requiredLength);
            }

            int[] intArray = new int[arrayLength];

            for (int i = 0; i < requiredLength; i += 2)
            {
                string charPair = input.Substring(i, 2);
                int value = (charPair[1] << 8) | charPair[0];
                intArray[i / 2] = value;
            }
            return intArray;
        }

        public static string IntArrayToString(int[] intArray)
        {
            if (intArray == null || intArray.Length == 0)
            {
                return string.Empty;
            }
            StringBuilder result = new StringBuilder();

            foreach (int value in intArray)
            {
                char[] charPair = new char[2];
                charPair[0] = (char)(value & 0xFF);     
                charPair[1] = (char)((value >> 8) & 0xFF); 
                result.Append(charPair);
            }

            return result.ToString();
        }

        public static int[] ByteArrayToInt16Array(byte[] data)
        {
            if (data.Length % 2 != 0)
            {
                throw new ArgumentException("Byte array length must be a multiple of 4 to convert to int array.");
            }
            int[] intArray = new int[data.Length / 2];
            for (int i = 0; i < data.Length; i += 2)
            {
                intArray[i / 2] = BitConverter.ToInt16(data, i);
            }
            return intArray;
        }

        public static bool[] ByteArrayToBoolArray(byte[] byteArray)
        {
            bool[] boolArray = new bool[byteArray.Length * 8];
            for (int i = 0; i < byteArray.Length; i++)
            {
                for (int bit = 0; bit < 8; bit++)
                {
                    boolArray[i * 8 + bit] = (byteArray[i] & (1 << bit)) != 0;
                }
            }

            return boolArray;
        }

    }
}

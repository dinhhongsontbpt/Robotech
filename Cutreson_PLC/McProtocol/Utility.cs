using System;

namespace Cutreson_PLC.McProtocol
{
    public class Utility
    {
        private const int BlockSize = 16;

        public static bool IsHexDevice(PlcDeviceType type)
        {
            bool result = false;
            try
            {
                if (type == PlcDeviceType.D || type == PlcDeviceType.SD || type == PlcDeviceType.Z || type == PlcDeviceType.ZR || type == PlcDeviceType.R || type == PlcDeviceType.W)
                {
                    result = true;
                }
            }
            catch
            {
            }
            return result;
        }

        public static void GetDeviceCode(string iDeviceName, out PlcDeviceType oType, out int oAddress)
        {
            string text = iDeviceName.ToUpper();
            string text2 = text.Substring(0, 1);
            string value;
            switch (text2)
            {
                case "A":
                case "B":
                case "D":
                case "F":
                case "L":
                case "M":
                case "R":
                case "V":
                case "W":
                case "X":
                case "Y":
                    value = text.Substring(1);
                    break;
                case "C":
                    text2 = "ZR";
                    value = text.Substring((!text2.Equals("C")) ? 1 : 2);
                    break;
                case "ZR":
                    text2 = "ZR";
                    value = text.Substring((!text2.Equals("ZR")) ? 1 : 3);
                    break;
                default:
                    throw new Exception("Invalid format.");
            }
            oType = GetDeviceType(text2);
            oAddress = Convert.ToInt32(value);
        }

        public static PlcDeviceType GetDeviceType(string s)
        {
            switch (s)
            {
                case "A": return PlcDeviceType.A;
                case "CM": return PlcDeviceType.CM;
                case "CT": return PlcDeviceType.CT;
                case "TM": return PlcDeviceType.TM;
                case "TT": return PlcDeviceType.TT;
                case "Z": return PlcDeviceType.Z;
                case "SN": return PlcDeviceType.SN;
                case "SS": return PlcDeviceType.SS;
                case "SC": return PlcDeviceType.SC;
                case "CN": return PlcDeviceType.CN;
                case "CS": return PlcDeviceType.CS;
                case "CC": return PlcDeviceType.CC;
                case "TN": return PlcDeviceType.TN;
                case "TS": return PlcDeviceType.TS;
                case "TC": return PlcDeviceType.TC;
                case "SW": return PlcDeviceType.SW;
                case "W": return PlcDeviceType.W;
                case "ZR": return PlcDeviceType.ZR;
                case "R": return PlcDeviceType.R;
                case "SD": return PlcDeviceType.SD;
                case "D": return PlcDeviceType.D;
                case "DY": return PlcDeviceType.DY;
                case "DX": return PlcDeviceType.DX;
                case "SB": return PlcDeviceType.SB;
                case "B": return PlcDeviceType.B;
                case "Y": return PlcDeviceType.Y;
                case "X": return PlcDeviceType.X;
                case "S": return PlcDeviceType.S;
                case "V": return PlcDeviceType.V;
                case "F": return PlcDeviceType.F;
                case "L": return PlcDeviceType.L;
                case "SM": return PlcDeviceType.SM;
                case "M": return PlcDeviceType.M;
                default: return PlcDeviceType.Max;
            }
        }

    }
}

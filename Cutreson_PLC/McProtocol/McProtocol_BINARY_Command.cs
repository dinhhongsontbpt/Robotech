using System.Collections.Generic;
using System;

namespace Cutreson_PLC.McProtocol
{
    internal class McProtocol_BINARY_Command
    {
        private McFrame FrameType { get; set; }

        private uint SerialNumber { get; set; }

        private uint NetwrokNumber { get; set; }

        private uint PcNumber { get; set; }

        private uint IoNumber { get; set; }

        private uint ChannelNumber { get; set; }

        private uint CpuTimer { get; set; }

        private int ResultCode { get; set; }

        public byte[] Response { get; private set; }

        public McProtocol_BINARY_Command(McFrame iFrame)
        {
            FrameType = iFrame;
            SerialNumber = 1u;
            NetwrokNumber = 0u;
            PcNumber = 255u;
            IoNumber = 1023u;
            ChannelNumber = 0u;
            CpuTimer = 16u;
        }

        public byte[] SetCommand(uint iMainCommand, uint iSubCommand, byte[] iData)
        {
            List<byte> list = new List<byte>(iData.Length + 20);
            uint num = (uint)(iData.Length + 6);
            uint num2 = ((FrameType == McFrame.MC3E) ? 80u : ((FrameType == McFrame.MC4E) ? 84u : 0u));
            list.Add((byte)num2);
            list.Add((byte)(num2 >> 8));
            if (FrameType == McFrame.MC4E)
            {
                list.Add((byte)SerialNumber);
                list.Add((byte)(SerialNumber >> 8));
                list.Add(0);
                list.Add(0);
            }
            list.Add((byte)NetwrokNumber);
            list.Add((byte)PcNumber);
            list.Add((byte)IoNumber);
            list.Add((byte)(IoNumber >> 8));
            list.Add((byte)ChannelNumber);
            list.Add((byte)num);
            list.Add((byte)(num >> 8));
            list.Add((byte)CpuTimer);
            list.Add((byte)(CpuTimer >> 8));
            list.Add((byte)iMainCommand);
            list.Add((byte)(iMainCommand >> 8));
            list.Add((byte)iSubCommand);
            list.Add((byte)(iSubCommand >> 8));
            list.AddRange(iData);
            return list.ToArray();
        }

        public int SetResponse(byte[] iResponse)
        {
            if (iResponse != null)
            {
                int num = ((FrameType == McFrame.MC3E) ? 11 : 15);
                if (num <= iResponse.Length)
                {
                    byte[] value = new byte[2]
                    {
                    iResponse[num - 4],
                    iResponse[num - 3]
                    };
                    byte[] value2 = new byte[2]
                    {
                    iResponse[num - 2],
                    iResponse[num - 1]
                    };
                    int num2 = BitConverter.ToUInt16(value, 0);
                    ResultCode = BitConverter.ToUInt16(value2, 0);
                    Response = new byte[num2 - 2];
                    Buffer.BlockCopy(iResponse, num, Response, 0, Response.Length);
                }
                return ResultCode;
            }
            return -1;
        }

        public bool IsIncorrectResponse(byte[] iResponse)
        {
            if (iResponse != null)
            {
                try
                {
                    int num = ((FrameType == McFrame.MC3E) ? 11 : 15);
                    byte[] value = new byte[2]
                    {
                    iResponse[num - 4],
                    iResponse[num - 3]
                    };
                    byte[] value2 = new byte[2]
                    {
                    iResponse[num - 2],
                    iResponse[num - 1]
                    };
                    int num2 = BitConverter.ToUInt16(value, 0) - 2;
                    ushort num3 = BitConverter.ToUInt16(value2, 0);
                    if (num3 == 0)
                    {
                    }
                    return num3 == 0 && num2 != iResponse.Length - num;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
    }
}

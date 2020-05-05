using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace XBee
{
    public class EscapedPacketReader : PacketReader
    {
        protected override void ProcessReceivedData()
        {
            stream = EscapeData(stream.ToArray());
            base.ProcessReceivedData();

        }

        public Queue<byte> EscapeData(byte[] data)
        {
            var escapeNext = false;
            Queue<byte> escapedData = new Queue<byte>();
            foreach (var b in data) {
                if (IsSpecialByte(b)) {
                    if (b == (byte) XBeeSpecialBytes.EscapeByte) {
                        escapeNext = true;
                        continue;
                    }
                    if (b == (byte) XBeeSpecialBytes.StartByte) {
                        continue;
                    }
                }

                if (escapeNext) {
                    escapedData.Enqueue(EscapeByte(b));
                    escapeNext = false;
                } else {
                    escapedData.Enqueue(b);
                }
            }
            return escapedData;
        }

        public bool IsSpecialByte(byte b)
        {
            return Enum.IsDefined(typeof(XBeeSpecialBytes), b);
        }

        public byte EscapeByte(byte b)
        {
            return (byte) (0x20 ^ b);
        }
    }
}
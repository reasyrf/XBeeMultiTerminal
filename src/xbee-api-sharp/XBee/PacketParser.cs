using System;
using System.IO;
using System.Text;
using XBee.Frames;

namespace XBee
{
    public class PacketParser
    {
        private readonly MemoryStream packetStream;

        public PacketParser(byte[] packet)
        {
            packetStream = new MemoryStream(packet);
        }

        public PacketParser(MemoryStream packetStream)
        {
            this.packetStream = packetStream;
        }

        public XBeeAddress64 ReadAddress64()
        {
            var addr = new byte[8];
            packetStream.Read(addr, 0, 8);
            Array.Reverse(addr);

            return new XBeeAddress64(BitConverter.ToUInt64(addr, 0));
        }

        public XBeeAddress16 ReadAddress16()
        {
            var addr = (ushort) ((packetStream.ReadByte() << 8) | packetStream.ReadByte());
            return new XBeeAddress16(addr);
        }

        public AT ReadATCommand()
        {
            var cmd = new char[2];

            cmd[0] = (char) packetStream.ReadByte();
            cmd[1] = (char) packetStream.ReadByte();

            return ATUtil.Parse(new String(cmd));
        }

        public int ReadByte()
        {
            return packetStream.ReadByte();
        }

        public ushort ReadUInt16()
        {
            var value = new byte[2];
            packetStream.Read(value, 0, 2);
            Array.Reverse(value);

            return BitConverter.ToUInt16(value, 0);
        }

        public uint ReadUInt32()
        {
            var value = new byte[4];
            packetStream.Read(value, 0, 4);
            Array.Reverse(value);

            return BitConverter.ToUInt32(value, 0);
        }

        public bool HasMoreData()
        {
            return (packetStream.Position != packetStream.Length);
        }

        public byte[] ReadData()
        {
            var readLength = (int)(packetStream.Length - packetStream.Position);
           
            var data = new byte[readLength];

            packetStream.Read(data, 0, readLength);
            return data;
        }

        public string ReadString()
        {
            var sb = new StringBuilder();
            char b;
            while((b = (char)packetStream.ReadByte()) != 0x00) {
                sb.Append(b);
            }
            return sb.ToString();
        }
    }
}
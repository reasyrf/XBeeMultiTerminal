using System;
using System.IO;
using XBee.Exceptions;

namespace XBee.Frames
{
    public class ExplicitAddressingTransmit : XBeeFrame
    {
        [Flags]
        public enum OptionValues : byte
        {
            DisableAck = 0x01,
            EnableAPSEncryption = 0x20,
            ExtendedTimeout = 0x40
        }

        private XBeeNode destination;
        private byte[] rfData;
        private readonly PacketParser parser;

        public byte? SourceEndpoint { get; set; }
        public byte? DestinationEndpoint { get; set; }
        public UInt16? ClusterId { get; set; }
        public UInt16? ProfileId { get; set; }

        public byte BroadcastRadius { get; set; }
        public OptionValues Options { get; set; }

        public ExplicitAddressingTransmit(PacketParser parser)
        {
            this.parser = parser;
            CommandId = XBeeAPICommandId.EXPLICIT_ADDR_REQUEST;
        }

        public ExplicitAddressingTransmit(XBeeNode destination)
        {
            CommandId = XBeeAPICommandId.EXPLICIT_ADDR_REQUEST;
            this.destination = destination;
            BroadcastRadius = 0;
            Options = 0;

            SourceEndpoint = null;
            DestinationEndpoint = null;
            ClusterId = null;
            ProfileId = null;

            rfData = null;
        }

        public void SetRFData(byte[] rfData)
        {
            this.rfData = rfData;
        }

        public override byte[] ToByteArray()
        {
            var stream = new MemoryStream();

            stream.WriteByte((byte) CommandId);
            stream.WriteByte(FrameId);

            stream.Write(destination.Address64.GetAddress(), 0, 8);
            stream.Write(destination.Address16.GetAddress(), 0, 2);

            if (!SourceEndpoint.HasValue)
                throw new XBeeFrameException("Missing Source Endpoint");
            stream.WriteByte(SourceEndpoint.Value);

            if (!DestinationEndpoint.HasValue)
                throw new XBeeFrameException("Missing Destination Endpoint");
            stream.WriteByte(DestinationEndpoint.Value);

            if (!ClusterId.HasValue)
                throw new XBeeFrameException("Missing Cluster ID");
            var clusterIdMsb = BitConverter.GetBytes(ClusterId.Value);
            Array.Reverse(clusterIdMsb);
            stream.Write(clusterIdMsb, 0, 2);

            if (!ProfileId.HasValue)
                throw new XBeeFrameException("Missing Profile ID");
            var profileIdMsb = BitConverter.GetBytes(ProfileId.Value);
            Array.Reverse(profileIdMsb);
            stream.Write(profileIdMsb, 0, 2);

            stream.WriteByte(BroadcastRadius);
            stream.WriteByte((byte) Options);

            if (rfData != null) {
                stream.Write(rfData, 0, rfData.Length);
            }

            return stream.ToArray();
        }

        public override void Parse()
        {
            FrameId = (byte) parser.ReadByte();

            destination = new XBeeNode { Address64 = parser.ReadAddress64(), Address16 = parser.ReadAddress16() };

            SourceEndpoint = (byte?) parser.ReadByte();
            DestinationEndpoint = (byte?) parser.ReadByte();
            ClusterId = parser.ReadUInt16();
            ProfileId = parser.ReadUInt16();

            BroadcastRadius = (byte) parser.ReadByte();
            Options = (OptionValues) parser.ReadByte();

            if (parser.HasMoreData()) {
                Console.WriteLine("TODO: has data!");
            }
        }
    }
}


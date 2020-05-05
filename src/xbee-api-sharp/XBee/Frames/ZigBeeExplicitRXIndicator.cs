using System;

namespace XBee.Frames
{
    public class ZigBeeExplicitRXIndicator : XBeeFrame
    {
        private readonly PacketParser parser;

        public XBeeNode Source { get; private set; }
        public byte? SourceEndpoint { get; private set; }
        public byte? DestinationEndpoint { get; private set; }
        public UInt16? ClusterId { get; private set; }
        public UInt16? ProfileId { get; private set; }

        public ReceiveOptionsType ReceiveOptions { get; private set; }
        public byte[] Data { get; private set; }

        public ZigBeeExplicitRXIndicator(PacketParser parser)
        {
            this.parser = parser;
            CommandId = XBeeAPICommandId.EXPLICIT_RX_INDICATOR_RESPONSE;
        }

        public override byte[] ToByteArray()
        {
            return new byte[] { };
        }

        public override void Parse()
        {
            Source = new XBeeNode { Address64 = parser.ReadAddress64(), Address16 = parser.ReadAddress16() };

            SourceEndpoint = (byte?) parser.ReadByte();
            DestinationEndpoint = (byte?) parser.ReadByte();
            ClusterId = parser.ReadUInt16();
            ProfileId = parser.ReadUInt16();

            ReceiveOptions = (ReceiveOptionsType) parser.ReadByte();
            Data = parser.ReadData();
        }
    }

}

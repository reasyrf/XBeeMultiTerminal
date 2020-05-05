using System;

namespace XBee.Frames
{
    public class NodeIdentification : XBeeFrame
    {
        public enum DeviceType
        {
            Coordinator = 0x00,
            Router = 0x01,
            EndDevice = 0x02
        }

        public enum SourceEventType
        {
            PushButtonEvent = 0x01,
            JoinEvent = 0x02,
            PowerCycleEvent = 0x03
        }

        private readonly PacketParser parser;

        public XBeeNode Source { get; private set; }
        public ReceiveOptionsType ReceiveOptions { get; private set; }
        public XBeeNode Source2 { get; private set; }
        public string NodeIdentifier { get; private set; }
        public XBeeAddress16 ParentAddress { get; private set; }
        public DeviceType Type { get; private set; }
        public SourceEventType SourceEvent { get; private set; }
        public UInt16 ProfileId { get; private set; }
        public UInt16 ManufacturerId { get; private set; }

        public NodeIdentification(PacketParser parser)
        {
            this.parser = parser;
            CommandId = XBeeAPICommandId.NODE_IDENTIFIER_RESPONSE;
        }

        public override byte[] ToByteArray()
        {
            return new byte[] { };
        }

        public override void Parse()
        {
            Source = new XBeeNode { Address64 = parser.ReadAddress64(), Address16 = parser.ReadAddress16() };
            ReceiveOptions = (ReceiveOptionsType) parser.ReadByte();
            Source2 = new XBeeNode { Address16 = parser.ReadAddress16(), Address64 = parser.ReadAddress64() };
            NodeIdentifier = parser.ReadString();
            ParentAddress = parser.ReadAddress16();
            Type = (DeviceType) parser.ReadByte();
            SourceEvent = (SourceEventType) parser.ReadByte();
            ProfileId = parser.ReadUInt16();
            ManufacturerId = parser.ReadUInt16();
        }
    }
}

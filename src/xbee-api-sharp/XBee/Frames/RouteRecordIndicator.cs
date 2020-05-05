using System.Collections.Generic;

namespace XBee.Frames
{
    public class RouteRecordIndicator : XBeeFrame
    {
        private readonly PacketParser parser;

        public XBeeNode Source { get; private set; }
        public ReceiveOptionsType ReceiveOptions { get; private set; }
        public int NumberOfHops { get; private set; }
        public List<XBeeAddress16> Addresses { get; private set; }

        public RouteRecordIndicator(PacketParser parser)
        {
            this.parser = parser;
            CommandId = XBeeAPICommandId.ROUTE_RECORD_INDICATOR;
        }

        public override byte[] ToByteArray()
        {
            return new byte[] { };
        }

        public override void Parse()
        {
            Source = new XBeeNode { Address64 = parser.ReadAddress64(), Address16 = parser.ReadAddress16() };
            ReceiveOptions = (ReceiveOptionsType)parser.ReadByte();
            NumberOfHops = parser.ReadByte();

            var list = new List<XBeeAddress16>();
            for (var i = 0; i < NumberOfHops; i++) {
                var hop = parser.ReadAddress16();
                list.Add(hop);
            }
            Addresses = list;
        }
    }
}

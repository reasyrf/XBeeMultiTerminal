namespace XBee.Frames
{
    public class ManyToOneRouteRequest : XBeeFrame
    {
        private readonly PacketParser parser;
        private int reserved;

        public XBeeNode Source { get; private set; }

        public ManyToOneRouteRequest(PacketParser parser)
        {
            this.parser = parser;
            CommandId = XBeeAPICommandId.MANYTOONE_ROUTE_REQUEST_INDICATOR;
        }

        public override byte[] ToByteArray()
        {
            return new byte[] { };
        }

        public override void Parse()
        {
            Source = new XBeeNode {Address64 = parser.ReadAddress64(), Address16 = parser.ReadAddress16()};

            reserved = parser.ReadByte();
        }
    }
}

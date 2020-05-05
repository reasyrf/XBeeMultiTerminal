namespace XBee.Frames
{

    public class OverAirUpdateStatus : XBeeFrame
    {
        public enum BootloaderMessageType
        {
            Ack = 0x06,
            NAck = 0x15,
            NoMacAck = 0x40,
            Query = 0x51,
            QueryResponse = 0x52
        }

        private readonly PacketParser parser;

        public XBeeNode Source { get; private set; }
        public ReceiveOptionsType ReceiveOptions { get; private set; }
        public BootloaderMessageType BootloaderMessage { get; private set; }
        public int BlockNumber { get; private set; }
        public XBeeAddress64 Target { get; private set; }

        public OverAirUpdateStatus(PacketParser parser)
        {
            this.parser = parser;
            CommandId = XBeeAPICommandId.FIRMWARE_UPDATE_STATUS;
        }

        public override byte[] ToByteArray()
        {
            return new byte[] { };
        }

        public override void Parse()
        {
            Source = new XBeeNode { Address64 = parser.ReadAddress64(), Address16 = parser.ReadAddress16() };
            ReceiveOptions = (ReceiveOptionsType) parser.ReadByte();
            BootloaderMessage = (BootloaderMessageType) parser.ReadByte();
            BlockNumber = parser.ReadByte();
            Target = parser.ReadAddress64();
        }
    }
}

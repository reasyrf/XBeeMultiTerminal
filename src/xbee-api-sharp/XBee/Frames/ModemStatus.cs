namespace XBee.Frames
{
    public class ModemStatus : XBeeFrame
    {
        public enum StatusType
        {
            HardwareReset = 0,
            WatchdogReset = 1,
            JoinedNetwork = 2,
            Disassociated = 3,
            CoordinatorStarted = 6,
            NetworkSecurityKeyUpdated = 7,
            VoltageExceeded = 0x0D,
            ConfigurationChangedOnJoin = 0x11,
            StackError = 0x80
        }

        private readonly PacketParser parser;

        public StatusType Status { get; private set; }

        public ModemStatus(PacketParser parser)
        {
            this.parser = parser;
            CommandId = XBeeAPICommandId.MODEM_STATUS_RESPONSE;
        }

        public override byte[] ToByteArray()
        {
            return new byte[] { };
        }

        public override void Parse()
        {
            Status = (StatusType) parser.ReadByte();
        }
    }
}

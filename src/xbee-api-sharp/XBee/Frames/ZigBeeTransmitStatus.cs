using System;

namespace XBee.Frames
{
    public class ZigBeeTransmitStatus : XBeeFrame
    {

        public enum DeliveryStatusType
        {
            Success = 0x00,
            MACAckFailure = 0x01,
            CCAFailure = 0x02,
            InvalidDestinationEndpoint = 0x15,
            NetworkAckFailure = 0x21,
            NotJoined = 0x22,
            SelfAddressed = 0x23,
            AddressNotFound = 0x24,
            RouteNotFound = 0x25,
            BroadcastFailedRelay = 0x26,
            InvalidBindingIndex = 0x2B,
            ResourceError = 0x2C,
            AttemptedBroadcast = 0x2D,
            AttemptedUnicast = 0x2E,
            ResourceError2 = 0x32,
            DataPayloadTooLarge = 0x74,
            IndirectMessageUnrequested = 0x75
        }

        [Flags]
        public enum DiscoveryStatusType
        {
            NoDiscoveryOverheard = 0x00,
            AddressDiscovery = 0x01,
            RouteDiscovery = 0x02,
            ExtendedTimeoutDiscovery = 0x40
        }

        private readonly PacketParser parser;

        public XBeeAddress16 Address { get; private set; }
        public int TransmitRetryCount { get; private set; }
        public DeliveryStatusType DeliveryStatus;
        public DiscoveryStatusType DiscoveryStatus;

        public ZigBeeTransmitStatus(PacketParser parser)
        {
            this.parser = parser;
            CommandId = XBeeAPICommandId.TRANSMIT_STATUS_RESPONSE;
        }

        public override byte[] ToByteArray()
        {
            return new byte[] { };
        }

        public override void Parse()
        {
            FrameId = (byte) parser.ReadByte();
            Address = parser.ReadAddress16();

            TransmitRetryCount = parser.ReadByte();
            DeliveryStatus = (DeliveryStatusType) parser.ReadByte();
            DiscoveryStatus = (DiscoveryStatusType) parser.ReadByte();
        }
    }
}

using System;

namespace XBee.Frames
{
    [Flags]
    public enum ReceiveOptionsType
    {
        Acknowledged = 0x01,
        BroadcastPacket = 0x02,
        APSEncrypted = 0x20,
        FromEndDevice = 0x40
    }
}

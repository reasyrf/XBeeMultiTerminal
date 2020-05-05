using System.Collections.Generic;
using System.IO;
using XBee.Exceptions;

namespace XBee.Frames
{
    public class CreateSourceRoute : XBeeFrame
    {
        private readonly PacketParser parser;
        private XBeeNode destination;

        public byte RouteOptions { get; set; }
        public List<XBeeAddress16> Hops { get; set; }

        public CreateSourceRoute(PacketParser parser)
        {
            this.parser = parser;
            CommandId = XBeeAPICommandId.CREATE_SOURCE_ROUTE;
            RouteOptions = 0x00;
            FrameId = 0x00;
        }

        public CreateSourceRoute(XBeeNode destination)
        {
            CommandId = XBeeAPICommandId.CREATE_SOURCE_ROUTE;
            this.destination = destination;
            RouteOptions = 0x00;
            FrameId = 0x00;
        }

        public override byte[] ToByteArray()
        {
            if (Hops.Count == 0)
                throw new XBeeFrameException("Missing Hops List");

            var frame = new MemoryStream();

            frame.WriteByte((byte) CommandId);
            frame.WriteByte(FrameId);

            frame.Write(destination.Address64.GetAddress(), 0, 8);
            frame.Write(destination.Address16.GetAddress(), 0, 2);

            frame.WriteByte(RouteOptions);
            frame.WriteByte((byte)Hops.Count);

            foreach(var addr in Hops) {
                frame.Write(addr.GetAddress(), 0, 2);
            }

            return frame.ToArray();
        }

        public override void Parse()
        {
            destination = new XBeeNode { Address64 = parser.ReadAddress64(), Address16 = parser.ReadAddress16() };

        }
    }
}

using XBee.Exceptions;

namespace XBee
{
    public class PacketReaderFactory
    {
        public static IPacketReader GetReader(ApiTypeValue apiType)
        {
            IPacketReader reader;
            switch(apiType)
            {
                case ApiTypeValue.Disabled:
                case ApiTypeValue.Enabled:
                    reader = new PacketReader();
                    break;
                case ApiTypeValue.EnabledWithEscape:
                    reader = new EscapedPacketReader();
                    break;
                default:
                    throw new XBeeException("Invalid API Type");
            }

            return reader;
        }
    }
}

using XBee.Utils;

namespace XBee
{
    public abstract class XBeeAddress
    {
        public abstract byte[] GetAddress();

        public override string ToString()
        {
            return ByteUtils.ToBase16(GetAddress());
        }
    }
}

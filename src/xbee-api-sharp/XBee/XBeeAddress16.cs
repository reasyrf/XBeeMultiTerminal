using System;
using System.Linq;

namespace XBee
{
    public class XBeeAddress16 : XBeeAddress
    {
        public static XBeeAddress16 BROADCAST = new XBeeAddress16(0xFFFF);
        public static XBeeAddress16 ZNET_BROADCAST = new XBeeAddress16(0xFFFE);

        private readonly byte[] address;

        public XBeeAddress16(ushort address)
        {
            var addressLittleEndian = BitConverter.GetBytes(address);
            Array.Reverse(addressLittleEndian);
            this.address = addressLittleEndian;
        }

        public override byte[] GetAddress()
        {
            return address;
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            if ((obj == null) || (typeof(XBeeAddress16) != obj.GetType()))
                return false;

            var addr = (XBeeAddress16) obj;

            return GetAddress().SequenceEqual(addr.GetAddress());
        }

        public override int GetHashCode()
        {
            return address.GetHashCode();
        }

    }
}

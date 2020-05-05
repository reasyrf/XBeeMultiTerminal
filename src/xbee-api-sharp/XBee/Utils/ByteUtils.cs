using System;
using System.Text;

namespace XBee.Utils
{
    public class ByteUtils
    {
        public static string ToBase16(byte[] data)
        {
            if (data == null)
                return "";

            var sb = new StringBuilder();
            foreach (var b in data) {
                sb.Append(String.Format("0x{0:X2} ", b));
            }
            return sb.ToString().TrimEnd();
        }
    }
}

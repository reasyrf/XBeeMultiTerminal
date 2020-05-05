using System;
using System.Text;

namespace XBee.Frames
{
    public abstract class ATValue
    {
        public abstract ATValue FromByteArray(byte[] value);
        public abstract byte[] ToByteArray();
    }

    public class ATStringValue : ATValue
    {
        public string Value { get; set; }

        public ATStringValue()
        { }

        public ATStringValue(string v)
        {
            Value = v;
        }

        public override ATValue FromByteArray(byte[] value)
        {
            var atValue = new ATStringValue(Encoding.UTF8.GetString(value, 0, value.Length));
            return atValue;
        }

        public override byte[] ToByteArray()
        {
            return Encoding.UTF8.GetBytes(Value);
        }
    }

    public class ATLongValue : ATValue
    {
        public ulong Value { get; set; }

        public ATLongValue()
        { }

        public ATLongValue(ulong v)
        {
            Value = v;
        }

        public override ATValue FromByteArray(byte[] value)
        {
            Array.Reverse(value);
            return new ATLongValue(ToInt(value));
        }

        private ulong ToInt(byte[] value)
        {
            switch (value.Length) {
                case 1:
                    return value[0];
                case 2:
                    return BitConverter.ToUInt16(value, 0);
                case 4:
                    return BitConverter.ToUInt32(value, 0);
                case 8:
                    return BitConverter.ToUInt64(value, 0);
                default:
                    throw new InvalidCastException("Value has more bytes than a 64 bits integer.");
            }
        }

        public override byte[] ToByteArray()
        {
            byte[] longArray;
            if (Value <= 0xFF)
                longArray = new[] { (byte)Value };
            else if (Value <= 0xFFFF)
                longArray = BitConverter.GetBytes((ushort)Value);
            else if (Value <= 0xFFFFFFFF)
                longArray = BitConverter.GetBytes((uint)Value);
            else
                longArray = BitConverter.GetBytes(Value);
            Array.Reverse(longArray);
            return longArray;
        }
    }
}

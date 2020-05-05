using System;

namespace XBee.Frames
{
    public class ZigBeeIODataSample : XBeeFrame
    {
        private readonly PacketParser parser;

        public XBeeNode Source { get; private set; }
        public ReceiveOptionsType ReceiveOptions { get; private set; }

        public uint NumberOfSamples { get; private set; }
        public uint DigitalChannelMask { get; private set; }
        public byte AnalogChannelMask { get; private set; }
        public uint DigitalSamples { get; private set; }
        public uint[] AnalogSamples { get; private set; }
        public uint SupplyVoltage { get; private set; }

        public ZigBeeIODataSample(PacketParser parser)
        {
            this.parser = parser;
            AnalogSamples = new uint[4];
            CommandId = XBeeAPICommandId.IO_SAMPLE_RESPONSE;
        }

        public override byte[] ToByteArray()
        {
            return new byte[] { };
        }

        public override void Parse()
        {
            Source = new XBeeNode { Address64 = parser.ReadAddress64(), Address16 = parser.ReadAddress16() };
            ReceiveOptions = (ReceiveOptionsType) parser.ReadByte();
            NumberOfSamples = (uint) parser.ReadByte();

            DigitalChannelMask = parser.ReadUInt16();
            AnalogChannelMask = (byte) parser.ReadByte();

            if (DigitalChannelMask > 0) {
                DigitalSamples = parser.ReadUInt16();
            }

            for (var i = 0; i < 4; i++) {
                if (IsBitSet(AnalogChannelMask, i)) {
                    AnalogSamples[i] = parser.ReadUInt16();
                }
            }

            if (HasSupplyVoltage()) {
                SupplyVoltage = parser.ReadUInt16();
            }
        }

        private static bool IsBitSet(byte bitmap, int bit)
        {
            if ((bit < 0) || (bit > 7))
                throw new ArgumentOutOfRangeException("bit", "Argument must be between 0 and 7");

            if (bitmap > 0xFF)
                throw new ArgumentOutOfRangeException("bitmap", "Bitmap must be an unsigned char.");

            return ((bitmap >> bit) & 0x01) == 0x01;
        }

        private bool HasSupplyVoltage()
        {
            return IsBitSet(AnalogChannelMask, 7);
        }
    }
}

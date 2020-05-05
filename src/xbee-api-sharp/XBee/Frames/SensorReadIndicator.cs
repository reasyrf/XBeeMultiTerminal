using System;

namespace XBee.Frames
{
    public class SensorReadIndicator : XBeeFrame
    {
        [Flags]
        public enum SensorType
        {
            ADSensor = 0x01,
            TemperatureSensor = 0x02,
            WaterPresent = 0x60
        }

        private readonly PacketParser parser;

        public XBeeNode Source { get; private set; }
        public ReceiveOptionsType ReceiveOptions { get; private set; }
        public SensorType Sensors { get; private set; }
        public ushort[] ADValues { get; private set; }
        public ushort Temperature { get; private set; }

        public SensorReadIndicator(PacketParser parser)
        {
            this.parser = parser;
            CommandId = XBeeAPICommandId.SENSOR_READ_INDICATOR;
            ADValues = new ushort[] { 0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF };
        }

        public override byte[] ToByteArray()
        {
            return new byte[] { };
        }

        public override void Parse()
        {
            Source = new XBeeNode { Address64 = parser.ReadAddress64(), Address16 = parser.ReadAddress16() };
            ReceiveOptions = (ReceiveOptionsType) parser.ReadByte();
            Sensors = (SensorType) parser.ReadByte();
            for (var i = 0; i < 4; i++) {
                ADValues[i] = parser.ReadUInt16();
            }
            Temperature = parser.ReadUInt16();
        }

        public double GetCelciusTemperature()
        {
            if (Temperature < 0x800)
                return Temperature / 16.0;
            return -(Temperature & 0x7FF) / 16.0;
        }
    }
}

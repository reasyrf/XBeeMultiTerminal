using NUnit.Framework;
using XBee.Frames;

namespace XBee.Test.Frames
{
    [TestFixture]
    public class ZigBeeIODataSampleTest
    {
        [Test]
        public void TestSensorReadIndicatorParse()
        {
            var packet = new byte[] { 0x00, 0x14, 0x92, 0x00, 0x13, 0xA2, 0x00, 0x40, 0x52, 0x2B, 0xAA, 0x7D, 0x84, 0x01, 0x01, 0x00, 0x1C, 0x02, 0x00, 0x14, 0x02, 0x25, 0xF5 };

            var frame = XBeePacketUnmarshaler.Unmarshal(packet);
            Assert.That(frame, Is.InstanceOf<ZigBeeIODataSample>());

            var cmd = (ZigBeeIODataSample) frame;

            Assert.That(cmd.Source, Is.TypeOf<XBeeNode>());
            Assert.That(cmd.Source.Address16, Is.EqualTo(new XBeeAddress16(0x7D84)));
            Assert.That(cmd.Source.Address64, Is.EqualTo(new XBeeAddress64(0x0013A20040522BAA)));
            Assert.That(cmd.ReceiveOptions, Is.EqualTo(ReceiveOptionsType.Acknowledged));

            Assert.That(cmd.NumberOfSamples, Is.EqualTo(1));
            Assert.That(cmd.DigitalChannelMask, Is.EqualTo(0x001C));
            Assert.That(cmd.AnalogChannelMask, Is.EqualTo(0x02));
            Assert.That(cmd.DigitalSamples, Is.EqualTo(0x0014));
            Assert.That(cmd.AnalogSamples[1], Is.EqualTo(0x0225));
        }
    }
}

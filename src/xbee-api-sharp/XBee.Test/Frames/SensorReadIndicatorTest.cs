using NUnit.Framework;
using XBee.Frames;

namespace XBee.Test.Frames
{
    [TestFixture]
    public class SensorReadIndicatorTest
    {
        [Test]
        public void TestSensorReadIndicatorParse()
        {
            var packet = new byte[] { 0x00, 0x17, 0x94, 0x00, 0x13, 0xA2, 0x00, 0x40, 0x52, 0x2B, 0xAA, 0xDD, 0x6C, 0x01, 0x03, 0x00, 0x02, 0x00, 0xCE, 0x00, 0xEA, 0x00, 0x52, 0x01, 0x6A, 0x8B };

            var frame = XBeePacketUnmarshaler.Unmarshal(packet);
            Assert.That(frame, Is.InstanceOf<SensorReadIndicator>());

            var cmd = (SensorReadIndicator) frame;

            Assert.That(cmd.Source, Is.TypeOf<XBeeNode>());
            Assert.That(cmd.Source.Address16, Is.EqualTo(new XBeeAddress16(0xDD6C)));
            Assert.That(cmd.Source.Address64, Is.EqualTo(new XBeeAddress64(0x0013A20040522BAA)));

            Assert.That(cmd.ReceiveOptions, Is.EqualTo(ReceiveOptionsType.Acknowledged));
            Assert.That(cmd.ADValues[0], Is.EqualTo(0x0002));
            Assert.That(cmd.ADValues[1], Is.EqualTo(0x00CE));
            Assert.That(cmd.ADValues[2], Is.EqualTo(0x00EA));
            Assert.That(cmd.ADValues[3], Is.EqualTo(0x0052));
            Assert.That(cmd.Temperature, Is.EqualTo(0x016A));
            Assert.That(cmd.GetCelciusTemperature(), Is.EqualTo(22.625));
        }
    }
}


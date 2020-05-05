using NUnit.Framework;
using XBee.Frames;

namespace XBee.Test.Frames
{
    [TestFixture]
    public class ZigBeeTransmitStatusTest
    {
        [Test]
        public void TestTransmitStatusParse()
        {
            var packet = new byte[] { 0x00, 0x07, 0x8B, 0x01, 0x7D, 0x84, 0x00, 0x00, 0x01, 0x71 };
            var frame = XBeePacketUnmarshaler.Unmarshal(packet);
            Assert.That(frame, Is.InstanceOf<ZigBeeTransmitStatus>());

            var cmd = (ZigBeeTransmitStatus) frame;
            Assert.That(cmd.Address, Is.EqualTo(new XBeeAddress16(0x7d84)));
            Assert.That(cmd.TransmitRetryCount, Is.EqualTo(0));
            Assert.That(cmd.DeliveryStatus, Is.EqualTo(ZigBeeTransmitStatus.DeliveryStatusType.Success));
            Assert.That(cmd.DiscoveryStatus, Is.EqualTo(ZigBeeTransmitStatus.DiscoveryStatusType.AddressDiscovery));
        }
    }
}

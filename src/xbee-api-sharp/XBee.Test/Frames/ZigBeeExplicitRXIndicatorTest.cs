using System.Text;
using NUnit.Framework;
using XBee.Frames;

namespace XBee.Test.Frames
{
    [TestFixture]
    public class ZigBeeExplicitRXIndicatorTest
    {
        [Test]
        public void TestZigBeeExplicitRXIndicatorParse()
        {
            var packet = new byte[] { 0x00, 0x18, 0x91, 0x00, 0x13, 0xA2, 0x00, 0x40, 0x52, 0x2B, 0xAA, 0x7D, 0x84, 0xE0, 0xE0, 0x22, 0x11, 0xC1, 0x05, 0x02, 0x52, 0x78, 0x44, 0x61, 0x74, 0x61, 0x52 };
            var frame = XBeePacketUnmarshaler.Unmarshal(packet);
            Assert.That(frame, Is.InstanceOf<ZigBeeExplicitRXIndicator>());

            var cmd = (ZigBeeExplicitRXIndicator) frame;
            Assert.That(cmd.Source.Address16, Is.EqualTo(new XBeeAddress16(0x7D84)));
            Assert.That(cmd.Source.Address64, Is.EqualTo(new XBeeAddress64(0x0013A20040522BAA)));

            Assert.That(cmd.SourceEndpoint, Is.EqualTo(0xE0));
            Assert.That(cmd.DestinationEndpoint, Is.EqualTo(0xE0));
            Assert.That(cmd.ClusterId, Is.EqualTo(0x2211));
            Assert.That(cmd.ProfileId, Is.EqualTo(0xC105));

            Assert.That(cmd.ReceiveOptions, Is.EqualTo(ReceiveOptionsType.BroadcastPacket));
            Assert.That(new UTF8Encoding().GetString(cmd.Data), Is.EqualTo("RxData"));
        }
    }
}

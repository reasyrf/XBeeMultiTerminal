using NUnit.Framework;
using XBee.Frames;

namespace XBee.Test.Frames
{
    [TestFixture]
    public class ManyToOneRouteRequestTest
    {
        [Test]
        public void TestManyToOneRouteRequestParse()
        {
            var packet = new byte[] { 0x00, 0x0C, 0xA3, 0x00, 0x13, 0xA2, 0x00, 0x40, 0x40, 0x11, 0x22, 0x00, 0x00, 0x00, 0xF4 };
            var frame = XBeePacketUnmarshaler.Unmarshal(packet);
            Assert.That(frame, Is.InstanceOf<ManyToOneRouteRequest>());

            var cmd = (ManyToOneRouteRequest)frame;
            Assert.That(cmd.Source.Address16, Is.EqualTo(new XBeeAddress16(0x0000)));
            Assert.That(cmd.Source.Address64, Is.EqualTo(new XBeeAddress64(0x0013A20040401122)));
        }
    }
}

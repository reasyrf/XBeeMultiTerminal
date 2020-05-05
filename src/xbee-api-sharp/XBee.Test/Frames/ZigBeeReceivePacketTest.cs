using NUnit.Framework;
using XBee.Frames;

namespace XBee.Test.Frames
{
    [TestFixture]
    public class ZigBeeReceivePacketTest
    {
        [Test]
        public void TestZigBeeReceivePacketParse()
        {
            var packet = new byte[] { 0x00, 0x12, 0x90, 0x00, 0x13, 0xA2, 0x00, 0x40, 0x52, 0x2B, 0xAA, 0x7D, 0x84, 0x01, 0x52, 0x78, 0x44, 0x61, 0x74, 0x61, 0x0D };
            var frame = XBeePacketUnmarshaler.Unmarshal(packet);
            Assert.That(frame, Is.InstanceOf<ZigBeeReceivePacket>());

            var cmd = (ZigBeeReceivePacket) frame;
            Assert.That(cmd.Source.Address16, Is.EqualTo(new XBeeAddress16(0x7D84)));
            Assert.That(cmd.Source.Address64, Is.EqualTo(new XBeeAddress64(0x0013A20040522BAA)));
            Assert.That(cmd.ReceiveOptions, Is.EqualTo(ReceiveOptionsType.Acknowledged));
            Assert.That(new System.Text.UTF8Encoding().GetString(cmd.Data), Is.EqualTo("RxData"));
        }
    }
}

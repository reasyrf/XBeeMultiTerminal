using NUnit.Framework;
using XBee.Frames;

namespace XBee.Test.Frames
{
    [TestFixture]
    public class OverAirUpdateStatusTest
    {
        [Test]
        public void TestATCommandResponseParse()
        {
            var packet = new byte[] { 0x00, 0x16, 0xA0, 0x00, 0x13, 0xA2, 0x00, 0x40, 0x3E, 0x07, 0x50, 0x00, 0x00, 0x01, 0x52, 0x00, 0x00, 0x13, 0xA2, 0x00, 0x40, 0x52, 0x2B, 0xAA, 0x66 };

            var frame = XBeePacketUnmarshaler.Unmarshal(packet);
            Assert.That(frame, Is.InstanceOf<OverAirUpdateStatus>());

            var cmd = (OverAirUpdateStatus)frame;

            Assert.That(cmd.Source, Is.TypeOf<XBeeNode>());
            Assert.That(cmd.Source.Address16, Is.EqualTo(new XBeeAddress16(0x0000)));
            Assert.That(cmd.Source.Address64, Is.EqualTo(new XBeeAddress64(0x0013A200403E0750)));

            Assert.That(cmd.ReceiveOptions, Is.EqualTo(ReceiveOptionsType.Acknowledged));
            Assert.That(cmd.BootloaderMessage, Is.EqualTo(OverAirUpdateStatus.BootloaderMessageType.QueryResponse));
            Assert.That(cmd.BlockNumber, Is.EqualTo(0));
            Assert.That(cmd.Target, Is.EqualTo(new XBeeAddress64(0x0013A20040522BAA)));
        }
    }
}

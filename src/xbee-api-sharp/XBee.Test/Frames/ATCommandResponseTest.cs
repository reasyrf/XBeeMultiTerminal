using NUnit.Framework;
using XBee.Frames;

namespace XBee.Test.Frames
{
    [TestFixture]
    public class ATCommandResponseTest
    {
        [Test]
        public void TestATCommandResponseParse()
        {
            var packet = new byte[] { 0x00, 0x05, 0x88, 0x01, 0x42, 0x44, 0x00, 0xF0 };

            var frame = XBeePacketUnmarshaler.Unmarshal(packet);
            Assert.That(frame, Is.InstanceOf<ATCommandResponse>());

            var cmd = (ATCommandResponse) frame;
            Assert.That(cmd.FrameId, Is.EqualTo(0x01));
            Assert.That(cmd.Command, Is.EqualTo(AT.BaudRate));
            Assert.That(cmd.CommandStatus, Is.EqualTo(0));
        }

        [Test]
        public void TestNetworkDiscoveryParsing()
        {
            var packet = new byte[]
                {
                    0x00, 0x19, 0x88, 0x01, 0x4E, 0x44, 0x00, 0x00, 0x00, 0x00, 0x13, 0xA2, 0x00,
                    0x40, 0x47, 0x81, 0x4F, 0x20, 0x00, 0xFF, 0xFE, 0x00, 0x00, 0xC1, 0x05, 0x10,
                    0x1E, 0xC7
                };

            var frame = XBeePacketUnmarshaler.Unmarshal(packet);
            Assert.That(frame, Is.InstanceOf<ATCommandResponse>());
        }
    }
}

using NUnit.Framework;
using XBee.Frames;

namespace XBee.Test.Frames
{
    [TestFixture]
    public class RemoteCommandResponseTest
    {
        [Test]
        public void TestATCommandResponseParse()
        {
            var packet = new byte[] { 0x00, 0x13, 0x97, 0x55, 0x00, 0x13, 0xA2, 0x00, 0x40, 0x52, 0x2B, 0xAA, 0x7D, 0x84, 0x53, 0x4C, 0x00, 0x40, 0x52, 0x2B, 0xAA, 0xF0 };

            var frame = XBeePacketUnmarshaler.Unmarshal(packet);
            Assert.That(frame, Is.InstanceOf<RemoteCommandResponse>());

            var cmd = (RemoteCommandResponse)frame;

            Assert.That(cmd.FrameId, Is.EqualTo(0x55));
            Assert.That(cmd.Source, Is.TypeOf<XBeeNode>());
            Assert.That(cmd.Source.Address16, Is.EqualTo(new XBeeAddress16(0x7D84)));
            Assert.That(cmd.Source.Address64, Is.EqualTo(new XBeeAddress64(0x0013A20040522BAA)));

            Assert.That(cmd.Command, Is.EqualTo(AT.SerialNumberLow));
            Assert.That(cmd.CommandStatus, Is.EqualTo(RemoteCommandResponse.CommandStatusType.Ok));
            Assert.That(((ATLongValue)cmd.Value).Value, Is.EqualTo(new ATLongValue(0x40522BAA).Value));
        }
    }
}

using NUnit.Framework;
using XBee.Frames;

namespace XBee.Test.Frames
{
    [TestFixture]
    public class NodeIdentificationTest
    {
        [Test]
        public void TestATCommandResponseParse()
        {
            var packet = new byte[] { 0x00, 0x20, 0x95, 0x00, 0x13, 0xA2, 0x00, 0x40, 0x52, 0x2B, 0xAA, 0x7D, 0x84, 0x02, 0x7D, 0x84, 0x00, 0x13, 0xA2, 0x00, 0x40, 0x52, 0x2B, 0xAA, 0x20, 0x00, 0xFF, 0xFE, 0x01, 0x01, 0xC1, 0x05, 0x10, 0x1E, 0x1B };

            var frame = XBeePacketUnmarshaler.Unmarshal(packet);
            Assert.That(frame, Is.InstanceOf<NodeIdentification>());

            var cmd = (NodeIdentification) frame;

            Assert.That(cmd.Source, Is.TypeOf<XBeeNode>());
            Assert.That(cmd.Source.Address16, Is.EqualTo(new XBeeAddress16(0x7D84)));
            Assert.That(cmd.Source.Address64, Is.EqualTo(new XBeeAddress64(0x0013A20040522BAA)));

            Assert.That(cmd.ReceiveOptions, Is.EqualTo(ReceiveOptionsType.BroadcastPacket));
            Assert.That(cmd.Source2, Is.TypeOf<XBeeNode>());
            Assert.That(cmd.Source2.Address16, Is.EqualTo(new XBeeAddress16(0x7D84)));
            Assert.That(cmd.Source2.Address64, Is.EqualTo(new XBeeAddress64(0x0013A20040522BAA)));

            Assert.That(cmd.NodeIdentifier, Is.EqualTo(" "));
            Assert.That(cmd.ParentAddress, Is.EqualTo(new XBeeAddress16(0xFFFE)));

            Assert.That(cmd.Type, Is.EqualTo(NodeIdentification.DeviceType.Router));
            Assert.That(cmd.SourceEvent, Is.EqualTo(NodeIdentification.SourceEventType.PushButtonEvent));
            Assert.That(cmd.ProfileId, Is.EqualTo(0xC105));
            Assert.That(cmd.ManufacturerId, Is.EqualTo(0x101E));
        }
    }
}

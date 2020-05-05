using NUnit.Framework;
using XBee.Frames;

namespace XBee.Test.Frames
{
    [TestFixture]
    class RemoteATCommandTest
    {
        [Test]
        public void TestRemoteATCommandNodeDiscovery()
        {
            var broadcast = new XBeeNode { Address16 = XBeeAddress16.ZNET_BROADCAST, Address64 = XBeeAddress64.BROADCAST };

            var cmd = new RemoteATCommand(AT.NodeDiscover, broadcast) { FrameId = 1 };

            Assert.AreEqual(new byte[] { 0x17, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFE, 0x00, (byte) 'N', (byte) 'D' }, cmd.ToByteArray());
        }

        [Test]
        public void TestRemoteATCommandDestinationHigh()
        {
            var broadcast = new XBeeNode { Address16 = XBeeAddress16.ZNET_BROADCAST, Address64 = XBeeAddress64.BROADCAST };

            var cmd = new RemoteATCommand(AT.DestinationHigh, broadcast) { FrameId = 1 };
            var v = new ATLongValue(0x11223300);
            cmd.SetValue(v);

            Assert.AreEqual(new byte[] { 0x17, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFE, 0x00, (byte) 'D', (byte) 'H', 0x11, 0x22, 0x33, 0x00 }, cmd.ToByteArray());
        }

        [Test]
        public void TestRemoteATCommandParse()
        {
            var packet = new byte[]
                             {
                                 0x00, 0x10, 0x17, 0x01, 0x00, 0x13, 0xA2, 0x00, 0x40, 0x40, 0x11, 0x22, 0xFF, 0xFE, 0x02,
                                 0x42, 0x48, 0x01, 0xF5
                             };

            var frame = XBeePacketUnmarshaler.Unmarshal(packet);
            Assert.That(frame, Is.InstanceOf<RemoteATCommand>());

            var cmd = (RemoteATCommand) frame;
            Assert.That(cmd.FrameId, Is.EqualTo(0x01));
            Assert.That(cmd.Destination.Address16, Is.EqualTo(XBeeAddress16.ZNET_BROADCAST));
            Assert.That(cmd.Destination.Address64, Is.EqualTo(new XBeeAddress64(0x0013A20040401122)));
            Assert.That(cmd.RemoteOptions, Is.EqualTo(0x02));
            Assert.That(cmd.Command, Is.EqualTo(AT.BroadcastHops));
        }
    }
}

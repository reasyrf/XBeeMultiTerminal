using NUnit.Framework;
using XBee.Frames;

namespace XBee.Test
{
    [TestFixture]
    class PacketReaderTest
    {
        private XBeeFrame frame = null;

        [Test]
        public void TestPacketReaderMultipleReceiveEvents()
        {
            var reader = new PacketReader();
            reader.FrameReceived += FrameReceivedEvent;

            var data = new byte[]
                        {   0x7E,
                            0x00, 0x16,
                            0x90, 
                            0x00, 0x13, 0xA2, 0x00, 0x40, 0x52, 0x2B, 0xAA, 
                            0x7D, 0x84, 
                            0x01
                        };
            var data2 = new byte[] { 0x7E, 0x69, 0x6e, 0x63, 0x6c, 0x75, 0x64, 0x65, 0x20, 0x00, 0xCF };
            
            reader.ReceiveData(data);
            reader.ReceiveData(data2);

            Assert.That(frame, Is.Not.Null);
            Assert.That(frame, Is.TypeOf<ZigBeeReceivePacket>());
        }

        private void FrameReceivedEvent(object sender, FrameReceivedArgs args)
        {
            frame = args.Response;
        }
    }
}

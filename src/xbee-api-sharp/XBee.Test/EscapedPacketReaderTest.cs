using NUnit.Framework;
using XBee.Frames;

namespace XBee.Test
{
    [TestFixture]
    class EscapedPacketReaderTest
    {
        private XBeeFrame frame;
        [Test]
        public void TestIsSpecialByteIsTrue()
        {
            var reader = new EscapedPacketReader();

            Assert.That(reader.IsSpecialByte(0x7E), Is.True);
            Assert.That(reader.IsSpecialByte(0x7D), Is.True);
            Assert.That(reader.IsSpecialByte(0x11), Is.True);
            Assert.That(reader.IsSpecialByte(0x13), Is.True);
        }

        [Test]
        public void TestIsSpecialByteIsFalse()
        {
            var reader = new EscapedPacketReader();

            Assert.That(reader.IsSpecialByte(0x77), Is.False);
            Assert.That(reader.IsSpecialByte(0xEE), Is.False);
            Assert.That(reader.IsSpecialByte(0x1D), Is.False);
            Assert.That(reader.IsSpecialByte(0xD2), Is.False);
        }

        [Test]
        public void TestEscapeData()
        {
            var reader = new EscapedPacketReader();
            reader.FrameReceived += FrameReceivedEvent;
            var data = new byte[]
                        {
                            0x7E, 0x00, 0x16, 0x10, 0x01, 0x00, 0x7D, 0x33, 0xA2, 0x00, 0x40, 0x0A, 0x01, 0x27, 0xFF,
                            0xFE, 0x00, 0x00, 0x54, 0x78, 0x44, 0x61, 0x74, 0x61, 0x30, 0x41, 0x7D, 0x33
                        };
            var expected = new byte[]
                        {
                            0x00, 0x16, 0x10, 0x01, 0x00, 0x13, 0xA2, 0x00, 0x40, 0x0A, 0x01, 0x27, 0xFF, 0xFE, 0x00, 0x00, 0x54, 0x78, 0x44,
                            0x61, 0x74, 0x61, 0x30, 0x41, 0x13
                        };
            var result = reader.EscapeData(data);
            Assert.That(result.ToArray(), Is.EqualTo(expected));
        }

        [Test]
        public void TestReceiveData()
        {
            var reader = new EscapedPacketReader();
            reader.FrameReceived += FrameReceivedEvent;
            var data = new byte[] { 0x7E, 0x00, 0x06, 0x88, 0x01, 0x41, 0x50, 0x00, 0x01, 0xE4 };
            frame = null;
            reader.ReceiveData(data);
            Assert.That(frame, Is.Not.Null);
            Assert.That(frame, Is.TypeOf<ATCommandResponse>());
        }

        [Test]
        public void TestReceiveEscapedData()
        {
            var data = new byte[]
                           {
                               0x7E, 0x00, 0x16, 0x90, 0x00, 0x13, 0xA2, 0x00, 0x40, 0x52, 0x2B, 0xAA,
                               0x7D, 0x5D, 0x84, 0x01, 0x7D, 0x5E, 0x69, 0x6e, 0x63, 0x6c, 0x75, 0x64, 0x65, 0x20, 0x00, 0xCF
                           };

            var reader = new EscapedPacketReader();
            frame = null;
            reader.FrameReceived += FrameReceivedEvent;

            reader.ReceiveData(data);
            Assert.That(frame, Is.Not.Null);
            Assert.That(frame, Is.TypeOf<ZigBeeReceivePacket>());
        }

        private void FrameReceivedEvent(object sender, FrameReceivedArgs args)
        {
            frame = args.Response;
        }
    }
}

using NUnit.Framework;

namespace XBee.Test
{
    [TestFixture]
    class XBeeChecksumTest
    {
        [Test]
        public void TestXBeeChecksumCalculate()
        {
            var packet = new byte[] { 0x83, 0x56, 0x78, 0x24, 0x00, 0x01, 0x02, 0x00, 0x03, 0xff };
            Assert.That(XBeeChecksum.Calculate(packet), Is.EqualTo(0x85));
        }

        [Test]
        public void TestXBeeChecksumVerify()
        {
            var packet = new byte[] { 0x83, 0x56, 0x78, 0x24, 0x00, 0x01, 0x02, 0x00, 0x03, 0xff, 0x85 };
            Assert.That(XBeeChecksum.Verify(packet), Is.True);
        }

        [Test]
        public void TestXBeeChecksumVerifyFail()
        {
            var packet = new byte[] { 0x83, 0x56, 0x78, 0x24, 0x00, 0x01, 0x02, 0x00, 0x03, 0xff, 0x83 };
            Assert.That(XBeeChecksum.Verify(packet), Is.False);
        }

    }
}

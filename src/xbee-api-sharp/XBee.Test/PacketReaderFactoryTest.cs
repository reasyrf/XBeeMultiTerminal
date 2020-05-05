using NUnit.Framework;

namespace XBee.Test
{
    [TestFixture]
    class PacketReaderFactoryTest
    {
        [Test]
        public void TestAPIPacketReaderFactory()
        {
            var reader = PacketReaderFactory.GetReader(ApiTypeValue.Enabled);
            Assert.That(reader, Is.InstanceOf<PacketReader>());
        }

        [Test]
        public void TestEscapedPacketReaderFactory()
        {
            var reader = PacketReaderFactory.GetReader(ApiTypeValue.EnabledWithEscape);
            Assert.That(reader, Is.InstanceOf<EscapedPacketReader>());
        }
    }
}

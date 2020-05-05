using Moq;
using NUnit.Framework;
using XBee.Frames;

namespace XBee.Test
{
    [TestFixture]
    public class XBeeConnectionTest
    {
        [Test]
        public void TestXBeeConnectionNoAPI()
        {
            var conn = new Mock<IXBeeConnection>();
            var xbee = new XBee();

            xbee.SetConnection(conn.Object);

            conn.Verify(connection => connection.SetPacketReader(It.IsAny<IPacketReader>()));
        }

        [Test]
        public void TestXBeeConnectionAPIEnabled()
        {
            var conn = new Mock<IXBeeConnection>();
            var xbee = new XBee { ApiType = ApiTypeValue.Enabled };

            xbee.SetConnection(conn.Object);

            conn.Verify(connection => connection.SetPacketReader(It.IsAny<IPacketReader>()));
        }

        [Test]
        public void TestXBeeConnectionExecute()
        {
            var conn = new Mock<IXBeeConnection>();
            byte[] result = null;

            conn.Setup(connection => connection.Write(It.IsAny<byte[]>())).Callback((byte[] b) => result = b);
            var xbee = new XBee();

            xbee.SetConnection(conn.Object);
            xbee.Execute(new ATCommand(AT.BaudRate) { FrameId = 1 });

            conn.Verify(connection => connection.SetPacketReader(It.IsAny<IPacketReader>()));
            Assert.That(result, Is.EqualTo(new byte[] { 0x7E, 0x00, 0x04, 0x08, 0x01, 0x42, 0x44, 0x70 }));
        }
    }
}

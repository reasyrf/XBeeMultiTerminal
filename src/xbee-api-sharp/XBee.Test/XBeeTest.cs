using NUnit.Framework;
using Moq;
using XBee.Frames;

namespace XBee.Test
{
    [TestFixture]
    class XBeeTest
    {
        [Test]
        public void TestXBeeExecuteQuery()
        {
            var xbee = new XBee() { ApiType = ApiTypeValue.Enabled };
            var conn = new Mock<IXBeeConnection>();
            var frame = new Mock<XBeeFrame>();
            conn.Setup(connection => connection.Write(It.IsAny<byte[]>())).Callback(() => xbee.FrameReceivedEvent(this, new FrameReceivedArgs(frame.Object)));

            xbee.SetConnection(conn.Object);
            var result = xbee.ExecuteQuery(new ATCommand(AT.BaudRate) { FrameId = 1 });

            conn.Verify(connection => connection.SetPacketReader(It.IsAny<IPacketReader>()));
            Assert.That(result, Is.InstanceOf<XBeeFrame>());
        }
    }
}

using NUnit.Framework;
using XBee.Frames;

namespace XBee.Test.Frames
{
    [TestFixture]
    class ATCommandTest
    {
        [Test]
        public void TestATCommandNodeDiscover()
        {
            var cmd = new ATCommand(AT.NodeDiscover);
            Assert.AreEqual(new byte[] { 0x08, 0x00, (byte)'N', (byte)'D' }, cmd.ToByteArray());
        }

        [Test]
        public void TestATCommandDestinationHigh()
        {
            var cmd = new ATCommand(AT.DestinationHigh);
            var v = new ATLongValue(0x11223300);
            cmd.SetValue(v);
            Assert.AreEqual(new byte[] { 0x08, 0x00, (byte)'D', (byte)'H', 0x11, 0x22, 0x33, 0x00 }, cmd.ToByteArray());
        }

        [Test]
        public void TestATCommandDestinationHighWithFrameId()
        {
            var cmd = new ATCommand(AT.DestinationHigh);
            cmd.FrameId = 0x02;
            Assert.AreEqual(new byte[] { 0x08, 0x02, (byte)'D', (byte)'H' }, cmd.ToByteArray());
        }

        [Test]
        public void TestATQueueCommandDestinationHigh()
        {
            var cmd = new ATQueueCommand(AT.DestinationHigh);
            var v = new ATLongValue(0x11223300);
            cmd.SetValue(v);
            Assert.AreEqual(new byte[] { 0x09, 0x00, (byte)'D', (byte)'H', 0x11, 0x22, 0x33, 0x00 }, cmd.ToByteArray());
        }
    }
}

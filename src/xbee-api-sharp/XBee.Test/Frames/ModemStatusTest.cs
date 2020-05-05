using NUnit.Framework;
using XBee.Frames;

namespace XBee.Test.Frames
{
    [TestFixture]
    public class ModemStatusTest
    {
        [Test]
        public void TestModemStatusParse()
        {
            var packet = new byte[] { 0x00, 0x02, 0x8A, 0x06, 0x6F };
            var frame = XBeePacketUnmarshaler.Unmarshal(packet);
            Assert.That(frame, Is.InstanceOf<ModemStatus>());

            var cmd = (ModemStatus) frame;
            Assert.That(cmd.Status, Is.EqualTo(ModemStatus.StatusType.CoordinatorStarted));
        }
    }
}

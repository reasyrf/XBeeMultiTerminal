using System.Collections.Generic;
using NUnit.Framework;
using XBee.Frames;

namespace XBee.Test.Frames
{
    [TestFixture]
    public class CreateSourceRouteTest
    {
        [Test]
        public void TestCreateSourceRoute()
        {
            var dest = new XBeeNode { Address16 = new XBeeAddress16(0x3344), Address64 = new XBeeAddress64(0x0013A20040401122) };
            var addrList = new List<XBeeAddress16> { new XBeeAddress16(0xEEFF), new XBeeAddress16(0xCCDD), new XBeeAddress16(0xAABB) };
            var frame = new CreateSourceRoute(dest) { Hops = addrList };

            Assert.That(frame.ToByteArray(), Is.EqualTo(new byte[] { 0x21, 0x00, 0x00, 0x13, 0xA2, 0x00, 0x40, 0x40, 0x11, 0x22, 0x33, 0x44, 0x00, 0x03, 0xEE, 0xFF, 0xCC, 0xDD, 0xAA, 0xBB }));
        }
    }
}

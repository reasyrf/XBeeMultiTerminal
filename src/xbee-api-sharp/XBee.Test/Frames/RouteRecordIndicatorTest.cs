using System.Collections.Generic;
using NUnit.Framework;
using XBee.Frames;

namespace XBee.Test.Frames
{
    [TestFixture]
    public class RouteRecordIndicatorTest
    {
        [Test]
        public void TestRouteRecordIndicatorParse()
        {
            var packet = new byte[] { 0x00, 0x13, 0xA1, 0x00, 0x13, 0xA2, 0x00, 0x40, 0x40, 0x11, 0x22, 0x33, 0x44, 0x01, 0x03, 0xEE, 0xFF, 0xCC, 0xDD, 0xAA, 0xBB, 0x80 };
            var frame = XBeePacketUnmarshaler.Unmarshal(packet);
            Assert.That(frame, Is.InstanceOf<RouteRecordIndicator>());

            var cmd = (RouteRecordIndicator)frame;
            Assert.That(cmd.Source.Address16, Is.EqualTo(new XBeeAddress16(0x3344)));
            Assert.That(cmd.Source.Address64, Is.EqualTo(new XBeeAddress64(0x0013A20040401122)));
            Assert.That(cmd.ReceiveOptions, Is.EqualTo(ReceiveOptionsType.Acknowledged));
            Assert.That(cmd.NumberOfHops, Is.EqualTo(3));

            Assert.That(cmd.Addresses, Is.TypeOf<List<XBeeAddress16>>());
            var list = cmd.Addresses;
            Assert.That(list[0], Is.EqualTo(new XBeeAddress16(0xEEFF)));
            Assert.That(list[2], Is.EqualTo(new XBeeAddress16(0xAABB)));
        }
    }
}

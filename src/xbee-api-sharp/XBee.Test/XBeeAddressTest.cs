using System;
using NUnit.Framework;

namespace XBee.Test
{
    [TestFixture]
    public class XBeeAddressTest
    {
        [Test]
        public void TestXBeeAddressBroadcast16()
        {
            Assert.That(XBeeAddress16.BROADCAST.GetAddress(), Is.EqualTo(new byte[] { 0xFF, 0xFF }));
        }

        [Test]
        public void TestXBeeAddressZNetBroadcast16()
        {
            Assert.That(XBeeAddress16.ZNET_BROADCAST.GetAddress(), Is.EqualTo(new byte[] { 0xFF, 0xFE }));
        }

        [Test]
        public void TestXBeeAddressBroadcast64()
        {
            Assert.That(XBeeAddress64.BROADCAST.GetAddress(), Is.EqualTo(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF }));
        }

        [Test]
        public void TestXBeeAddressZNetCoordinator64()
        {
            Assert.That(XBeeAddress64.ZNET_COORDINATOR.GetAddress(), Is.EqualTo(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }));
        }

        [Test]
        public void TestXBeeAddressZNetBroadcast16ToString()
        {
            Assert.That(XBeeAddress16.ZNET_BROADCAST.ToString(), Is.EqualTo("0xFF 0xFE"));
        }

        [Test]
        public void TestXBeeAddressBroadcast64ToString()
        {
            Assert.That(XBeeAddress64.BROADCAST.ToString(), Is.EqualTo("0x00 0x00 0x00 0x00 0x00 0x00 0xFF 0xFF"));
        }

        [Test]
        public void TestXBeeAddress16IsEqualSame()
        {
            var localAddress = new XBeeAddress16(0xAABB);
            Assert.That(localAddress.Equals(localAddress), Is.True);
        }

        [Test]
        public void TestXBeeAddress16IsEqualObject()
        {
            var localAddress = new XBeeAddress16(0xAABB);
            Assert.That(localAddress.Equals(new Object()), Is.False);
        }

        [Test]
        public void TestXBeeAddress16IsEqual()
        {
            var localAddress = new XBeeAddress16(0xFFFE);
            Assert.That(localAddress.Equals(XBeeAddress16.ZNET_BROADCAST), Is.True);
        }

        [Test]
        public void TestXBeeAddress16IsNotEqual()
        {
            var localAddress1 = new XBeeAddress16(0xFFFE);
            var localAddress2 = new XBeeAddress16(0xAABB);
            Assert.That(localAddress1.Equals(localAddress2), Is.False);
        }

        [Test]
        public void TestXBeeAddress64IsEqualSame()
        {
            var localAddress = new XBeeAddress64(0xAABB);
            Assert.That(localAddress.Equals(localAddress), Is.True);
        }

        [Test]
        public void TestXBeeAddress64IsEqualObject()
        {
            var localAddress = new XBeeAddress64(0xAABB);
            Assert.That(localAddress.Equals(new Object()), Is.False);
        }

        [Test]
        public void TestXBeeAddress64IsEqual()
        {
            var localAddress = new XBeeAddress64(0xFFFF);
            Assert.That(localAddress.Equals(XBeeAddress64.BROADCAST), Is.True);
        }

        [Test]
        public void TestXBeeAddress64IsNotEqual()
        {
            var localAddress1 = new XBeeAddress64(0xFFFE);
            var localAddress2 = new XBeeAddress64(0xAABB);
            Assert.That(localAddress1.Equals(localAddress2), Is.False);
        }
    }
}

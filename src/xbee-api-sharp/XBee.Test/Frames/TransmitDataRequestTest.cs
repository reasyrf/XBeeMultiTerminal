using NUnit.Framework;
using XBee.Frames;

namespace XBee.Test.Frames
{
    [TestFixture]
    class TransmitDataRequestTest
    {
        [Test]
        public void TestTransmitDataRequestBroadcast()
        {
            var broadcast = new XBeeNode { Address16 = XBeeAddress16.ZNET_BROADCAST, Address64 = XBeeAddress64.BROADCAST };

            var frame = new TransmitDataRequest(broadcast);
            Assert.AreEqual(new byte[] { 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFE, 0x00, 0x00 }, frame.ToByteArray());
        }

        [Test]
        public void TestTransmitDataRequestBroadcastFrameId()
        {
            var broadcast = new XBeeNode { Address16 = XBeeAddress16.ZNET_BROADCAST, Address64 = XBeeAddress64.BROADCAST };

            var frame = new TransmitDataRequest(broadcast) { FrameId = 1 };
            Assert.AreEqual(new byte[] { 0x10, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFE, 0x00, 0x00 }, frame.ToByteArray());
        }

        [Test]
        public void TestTransmitDataRequestBroadcastRadius()
        {
            var broadcast = new XBeeNode { Address16 = XBeeAddress16.ZNET_BROADCAST, Address64 = XBeeAddress64.BROADCAST };

            var frame = new TransmitDataRequest(broadcast) { BroadcastRadius = 2 };
            Assert.AreEqual(new byte[] { 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFE, 0x02, 0x00 }, frame.ToByteArray());
        }

        [Test]
        public void TestTransmitDataRequestBroadcastRadiusOptions()
        {
            var broadcast = new XBeeNode { Address16 = XBeeAddress16.ZNET_BROADCAST, Address64 = XBeeAddress64.BROADCAST };

            var frame = new TransmitDataRequest(broadcast) {
                BroadcastRadius = 2,
                Options =
                    TransmitDataRequest.OptionValues.DisableAck |
                    TransmitDataRequest.OptionValues.ExtendedTimeout
            };
            Assert.AreEqual(new byte[] { 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFE, 0x02, 0x41 }, frame.ToByteArray());
        }

        [Test]
        public void TestTransmitDataRequestBroadcastRadiusOptionsData()
        {
            var broadcast = new XBeeNode { Address16 = XBeeAddress16.ZNET_BROADCAST, Address64 = XBeeAddress64.BROADCAST };

            var frame = new TransmitDataRequest(broadcast) {
                BroadcastRadius = 2,
                Options =
                    TransmitDataRequest.OptionValues.DisableAck |
                    TransmitDataRequest.OptionValues.ExtendedTimeout
            };
            frame.SetRFData(new byte[] { 0x11, 0x22, 0x33, 0x00 });
            Assert.AreEqual(new byte[] { 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFE, 0x02, 0x41, 0x11, 0x22, 0x33, 0x00 }, frame.ToByteArray());
        }
    }
}

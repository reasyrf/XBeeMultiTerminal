using NUnit.Framework;
using XBee.Exceptions;
using XBee.Frames;

namespace XBee.Test.Frames
{
    [TestFixture]
    class ExplicitAddressingTransmitTest
    {
        [Test]
        public void TestExplicitAddressingRequestBroadcastRadiusOptions()
        {
            var broadcast = new XBeeNode { Address16 = XBeeAddress16.ZNET_BROADCAST, Address64 = XBeeAddress64.BROADCAST };

            var frame = new ExplicitAddressingTransmit(broadcast) {
                FrameId = 1,
                BroadcastRadius = 2,
                Options =
                    ExplicitAddressingTransmit.OptionValues.DisableAck |
                    ExplicitAddressingTransmit.OptionValues.ExtendedTimeout,
                SourceEndpoint = 0xA0,
                DestinationEndpoint = 0xA1,
                ClusterId = 0x1554,
                ProfileId = 0xC105
            };


            Assert.AreEqual(new byte[] { 0x11, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFE, 0xA0, 0xA1, 0x15, 0x54, 0xC1, 0x05, 0x02, 0x41 }, frame.ToByteArray());
        }

        [Test]
        public void TestExplicitAddressingRequestBroadcastRadiusOptionsParse()
        {
            var packet = new byte[]
                {
                    0x00, 0x14, 0x11, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFE, 0xA0, 0xA1, 0x15, 0x54, 0xC1,
                    0x05, 0x02, 0x41, 0x3F
                };

            var frame = XBeePacketUnmarshaler.Unmarshal(packet);
            Assert.That(frame, Is.InstanceOf<ExplicitAddressingTransmit>());
            var cmd = (ExplicitAddressingTransmit) frame;
            Assert.That(cmd.FrameId, Is.EqualTo(0x01));

            Assert.That(cmd.BroadcastRadius, Is.EqualTo(2));
            Assert.That(cmd.Options, Is.EqualTo(ExplicitAddressingTransmit.OptionValues.DisableAck | ExplicitAddressingTransmit.OptionValues.ExtendedTimeout));

            Assert.That(cmd.SourceEndpoint, Is.EqualTo(0xA0));
            Assert.That(cmd.DestinationEndpoint, Is.EqualTo(0xA1));
            Assert.That(cmd.ClusterId, Is.EqualTo(0x1554));
            Assert.That(cmd.ProfileId, Is.EqualTo(0xC105));
        }

        [Test]
        public void TestExplicitAddressingRequestBroadcastRadiusOptionsData()
        {
            var broadcast = new XBeeNode { Address16 = XBeeAddress16.ZNET_BROADCAST, Address64 = XBeeAddress64.BROADCAST };

            var frame = new ExplicitAddressingTransmit(broadcast) {
                FrameId = 1,
                BroadcastRadius = 2,
                Options =
                    ExplicitAddressingTransmit.OptionValues.DisableAck |
                    ExplicitAddressingTransmit.OptionValues.ExtendedTimeout,
                SourceEndpoint = 0xA0,
                DestinationEndpoint = 0xA1,
                ClusterId = 0x1554,
                ProfileId = 0xC105
            };


            frame.SetRFData(new byte[] { 0x11, 0x22, 0x33, 0x00 });

            Assert.AreEqual(new byte[] { 0x11, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFE, 0xA0, 0xA1, 0x15, 0x54, 0xC1, 0x05, 0x02, 0x41, 0x11, 0x22, 0x33, 0x00 }, frame.ToByteArray());
        }

        [Test]
        [ExpectedException(typeof(XBeeFrameException), ExpectedMessage = "Missing Profile ID")]
        public void TestExplicitAddressingRequestMissingProfile()
        {
            var broadcast = new XBeeNode { Address16 = XBeeAddress16.ZNET_BROADCAST, Address64 = XBeeAddress64.BROADCAST };

            var frame = new ExplicitAddressingTransmit(broadcast) {
                FrameId = 1,
                BroadcastRadius = 2,
                Options =
                    ExplicitAddressingTransmit.OptionValues.DisableAck |
                    ExplicitAddressingTransmit.OptionValues.ExtendedTimeout,
                SourceEndpoint = 0xA0,
                DestinationEndpoint = 0xA1,
                ClusterId = 0x1554
            };


            frame.SetRFData(new byte[] { 0x11, 0x22, 0x33, 0x00 });

            Assert.AreEqual(new byte[] { 0x11, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFE, 0xA0, 0xA1, 0x15, 0x54, 0xC1, 0x05, 0x02, 0x41, 0x11, 0x22, 0x33, 0x00 }, frame.ToByteArray());
        }

        [Test]
        [ExpectedException(typeof(XBeeFrameException), ExpectedMessage = "Missing Cluster ID")]
        public void TestExplicitAddressingRequestMissingCluster()
        {
            var broadcast = new XBeeNode { Address16 = XBeeAddress16.ZNET_BROADCAST, Address64 = XBeeAddress64.BROADCAST };

            var frame = new ExplicitAddressingTransmit(broadcast) {
                FrameId = 1,
                BroadcastRadius = 2,
                Options =
                    ExplicitAddressingTransmit.OptionValues.DisableAck |
                    ExplicitAddressingTransmit.OptionValues.ExtendedTimeout,
                SourceEndpoint = 0xA0,
                DestinationEndpoint = 0xA1,
                ProfileId = 0xC105
            };


            frame.SetRFData(new byte[] { 0x11, 0x22, 0x33, 0x00 });

            Assert.AreEqual(new byte[] { 0x11, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFE, 0xA0, 0xA1, 0x15, 0x54, 0xC1, 0x05, 0x02, 0x41, 0x11, 0x22, 0x33, 0x00 }, frame.ToByteArray());
        }

        [Test]
        [ExpectedException(typeof(XBeeFrameException), ExpectedMessage = "Missing Destination Endpoint")]
        public void TestExplicitAddressingRequestMissingDestination()
        {
            var broadcast = new XBeeNode { Address16 = XBeeAddress16.ZNET_BROADCAST, Address64 = XBeeAddress64.BROADCAST };

            var frame = new ExplicitAddressingTransmit(broadcast) {
                FrameId = 1,
                BroadcastRadius = 2,
                Options =
                    ExplicitAddressingTransmit.OptionValues.DisableAck |
                    ExplicitAddressingTransmit.OptionValues.ExtendedTimeout,
                SourceEndpoint = 0xA0,
                ClusterId = 0x1554,
                ProfileId = 0xC105
            };


            frame.SetRFData(new byte[] { 0x11, 0x22, 0x33, 0x00 });

            Assert.AreEqual(new byte[] { 0x11, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFE, 0xA0, 0xA1, 0x15, 0x54, 0xC1, 0x05, 0x02, 0x41, 0x11, 0x22, 0x33, 0x00 }, frame.ToByteArray());
        }

        [Test]
        [ExpectedException(typeof(XBeeFrameException), ExpectedMessage = "Missing Source Endpoint")]
        public void TestExplicitAddressingRequestMissingSource()
        {
            var broadcast = new XBeeNode { Address16 = XBeeAddress16.ZNET_BROADCAST, Address64 = XBeeAddress64.BROADCAST };

            var frame = new ExplicitAddressingTransmit(broadcast) {
                FrameId = 1,
                BroadcastRadius = 2,
                Options =
                    ExplicitAddressingTransmit.OptionValues.DisableAck |
                    ExplicitAddressingTransmit.OptionValues.ExtendedTimeout,
                DestinationEndpoint = 0xA1,
                ClusterId = 0x1554,
                ProfileId = 0xC105
            };


            frame.SetRFData(new byte[] { 0x11, 0x22, 0x33, 0x00 });

            Assert.AreEqual(new byte[] { 0x11, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFE, 0xA0, 0xA1, 0x15, 0x54, 0xC1, 0x05, 0x02, 0x41, 0x11, 0x22, 0x33, 0x00 }, frame.ToByteArray());
        }
    }
}

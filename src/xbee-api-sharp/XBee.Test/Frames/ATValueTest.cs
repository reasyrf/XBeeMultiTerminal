using NUnit.Framework;
using XBee.Frames;

namespace XBee.Test.Frames
{
    [TestFixture]
    class ATValueTest
    {

        [Test]
        public void TestATStringValue()
        {
            var value = new ATStringValue();
            Assert.That(value, Is.AssignableTo<ATValue>());
            Assert.That(value, Is.TypeOf<ATStringValue>());
        }

        [Test]
        public void TestATStringValueToByteArray()
        {
            var value = new ATStringValue("MeTesting123");
            Assert.That(value.ToByteArray(), Is.TypeOf<byte[]>());
            Assert.That(value.ToByteArray(), Is.EqualTo(new byte[] { 0x4D, 0x65, 0x54, 0x65, 0x73, 0x74, 0x69, 0x6E, 0x67, 0x31, 0x32, 0x33 }));
        }

        [Test]
        public void TestATStringValueFromByteArray()
        {
            var data = new byte[] { 0x4D, 0x65, 0x54, 0x65, 0x73, 0x74, 0x69, 0x6E, 0x67, 0x31, 0x32, 0x33 };
            var value = new ATStringValue().FromByteArray(data);
            Assert.That(value, Is.TypeOf<ATStringValue>());
            Assert.That(((ATStringValue)value).Value, Is.EqualTo("MeTesting123"));
        }

        [Test]
        public void TestATLongValue()
        {
            var value = new ATLongValue();
            Assert.That(value, Is.AssignableTo<ATValue>());
            Assert.That(value, Is.TypeOf<ATLongValue>());
        }

        [Test]
        public void TestATLongValueFromByte()
        {
            var value = new ATLongValue().FromByteArray(new byte[] { 0x11 });
            Assert.That(value, Is.TypeOf<ATLongValue>());
            Assert.That(((ATLongValue)value).Value, Is.EqualTo(0x11));
        }

        [Test]
        public void TestATLongValueToByte()
        {
            var value = new ATLongValue(0x11);
            Assert.That(value, Is.TypeOf<ATLongValue>());
            Assert.That(value.ToByteArray(), Is.EqualTo(new byte[] { 0x11 }));
        }

        [Test]
        public void TestATLongValueFromUInt16()
        {
            var value = new ATLongValue().FromByteArray(new byte[] { 0x11, 0x22 });
            Assert.That(value, Is.TypeOf<ATLongValue>());
            Assert.That(((ATLongValue)value).Value, Is.EqualTo(0x1122));
        }

        [Test]
        public void TestATLongValueToUInt16()
        {
            var value = new ATLongValue(0x1122);
            Assert.That(value, Is.TypeOf<ATLongValue>());
            Assert.That(value.ToByteArray(), Is.EqualTo(new byte[] { 0x11, 0x22 }));
        }

        [Test]
        public void TestATLongValueFromUInt32()
        {
            var value = new ATLongValue().FromByteArray(new byte[] { 0x11, 0x22, 0x33, 0x44 });
            Assert.That(value, Is.TypeOf<ATLongValue>());
            Assert.That(((ATLongValue)value).Value, Is.EqualTo(0x11223344));
        }

        [Test]
        public void TestATLongValueToUInt32()
        {
            var value = new ATLongValue(0x11223344);
            Assert.That(value, Is.TypeOf<ATLongValue>());
            Assert.That(value.ToByteArray(), Is.EqualTo(new byte[] { 0x11, 0x22, 0x33, 0x44 }));
        }

        [Test]
        public void TestATLongValueFromUInt64()
        {
            var value = new ATLongValue().FromByteArray(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x44, 0x33, 0x22, 0x11 });
            Assert.That(value, Is.TypeOf<ATLongValue>());
            Assert.That(((ATLongValue)value).Value, Is.EqualTo(0x1122334444332211));
        }

        [Test]
        public void TestATLongValueToUInt64()
        {
            var value = new ATLongValue(0x1122334444332211);
            Assert.That(value, Is.TypeOf<ATLongValue>());
            Assert.That(value.ToByteArray(), Is.EqualTo(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x44, 0x33, 0x22, 0x11 }));
        }

    }
}

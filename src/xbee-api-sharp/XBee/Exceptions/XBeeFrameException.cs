using System;
using System.Runtime.Serialization;

namespace XBee.Exceptions
{
    public class XBeeFrameException : Exception
    {
        public XBeeFrameException(String message)
            : base(message)
        { }

        public XBeeFrameException(String message, Exception inner)
            : base(message, inner)
        { }

        protected XBeeFrameException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

    }
}

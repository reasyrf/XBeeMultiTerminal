using System;
using System.Runtime.Serialization;

namespace XBee.Exceptions
{
    public class XBeeException : Exception
    {
        public XBeeException(String message)
            : base(message)
        { }

        public XBeeException(String message, Exception inner)
            : base(message, inner)
        { }

        protected XBeeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

    }
}

namespace XBee
{
    public class FrameReceivedArgs
    {
        public XBeeFrame Response { get; private set; }

        public FrameReceivedArgs(XBeeFrame response)
        {
            Response = response;
        }
    }
}
using XBee.Frames;

namespace XBee
{
    public class XBeeNode
    {
        public XBeeAddress16 Address16 { get; set; }
        public XBeeAddress64 Address64 { get; set; }

        public void SendData(byte[] data)
        {
            var frame = new TransmitDataRequest(this);
        }
    }
}

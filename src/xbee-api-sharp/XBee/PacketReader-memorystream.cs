using System;
using System.IO;
using System.Linq;
using XBee.Exceptions;

namespace XBee
{
    public class PacketReader : IPacketReader
    {
        public event FrameReceivedHandler FrameReceived;


        //This is handled more like a queue now.
        protected MemoryStream Stream = new MemoryStream();
        //private uint packetLength = 0;
        bool streamLock = false;

        public void ReceiveData(byte[] data)
        {
            //Ignore start byte new stream creation, because we can concurrent data streams from two devices.
            //Which we don't want to overwrite.
            /*if (packetLength == 0 && data[0] == (byte) XBeeSpecialBytes.StartByte) {
                
                Stream = new MemoryStream();
                packetLength = 0;
            }*/
            

            CopyAndProcessData(data);
        }

        private void CopyAndProcessData(byte[] data)
        {
            while (streamLock) { };
            foreach (var b in data.Where(b => Stream.Length != 0 || b != (byte) XBeeSpecialBytes.StartByte)) {
                Stream.WriteByte(b);
            }

            /*if (packetLength == 0 && Stream.Length > 2) {
                var packet = Stream.ToArray();
                packetLength = (uint) (packet[0] << 8 | packet[1]) + 3;
            }*/

            /*if (Stream.Length < 3)
                return;

            if (packetLength != 0 && Stream.Length < packetLength)
                return;*/

            ProcessReceivedData();
        }

        protected virtual void ProcessReceivedData()
        {
            try
            {
                Boolean readyToProcess = false;
                var sourceArray = Stream.ToArray();
                uint length = 0;


                //Check if there is a whole packet to process
                try
                {
                    //Only if we have enough data to process length will we actually look at it.
                    if (Stream.Length > 3)
                    {
                        length = (uint)(sourceArray[0] << 8 | sourceArray[1]) + 3;
                        // check to see if we should wait for more data before processing.

                        if (Stream.Length >= length)
                        {
                            readyToProcess = true;
                        }

                    }
                }
                catch
                {
                    readyToProcess = false;
                }

                if (readyToProcess)
                {
                    //If we have lots of data to process (more than a single packet size).
                    if (length < Stream.Length)
                    {


                        byte[] packet = new byte[length];

                        Array.Copy(sourceArray, packet, packet.Length);

                        var frame = XBeePacketUnmarshaler.Unmarshal(packet);
                        //packetLength = (uint)Stream.Length - length - 1;
                        var data = Stream.ToArray().Skip((int)length + 1);

                        streamLock = true;
                        Stream.SetLength(0);
                        foreach (var b in data)
                        {
                            Stream.WriteByte(b);
                        }
                        streamLock = false;

                        if (FrameReceived != null)
                            FrameReceived.Invoke(this, new FrameReceivedArgs(frame));

                        //Do another run until no data to process.
                        ProcessReceivedData();

                    }

                    else if (length == Stream.Length)
                    {
                        var frame = XBeePacketUnmarshaler.Unmarshal(Stream.ToArray());
                        //packetLength = 0;
                        if (FrameReceived != null)
                            FrameReceived.Invoke(this, new FrameReceivedArgs(frame));
                        Stream.SetLength(0);
                    }
                }



                //var frame = XBeePacketUnmarshaler.Unmarshal(Stream.ToArray());
                //packetLength = 0;
                //if (FrameReceived != null)
                //    FrameReceived.Invoke(this, new FrameReceivedArgs(frame));



            }
            catch (XBeeFrameException ex)
            {
                Stream.SetLength(0);
                //We have an exception when the length/checksum cannot be parsed (length is too long and the packet cannot be unmarshalled),
                //since we don't know where the start of the packet is.
                //This might be because we have a half filled buffer or the like. This exception catches this issue.
                //packetLength = 0;
                streamLock = false;
                throw new XBeeException("Unable to unmarshal packet.", ex);


            }
            catch (XBeeException ex)
            {
                Stream.SetLength(0);
                //packetLength = 0;
                streamLock = false;
                throw new XBeeException("Unable to unmarshal packet.", ex);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using XBee.Exceptions;

namespace XBee
{
    public class PacketReader : IPacketReader
    {
        public event FrameReceivedHandler FrameReceived;

        /* This class has been reimplemented as a queue, rather than a memory stream
         * so it is thread safe, as the original would reinitialise the stream,
         * during processing, which resulted in loss of data and out of order data.
         */

        protected Queue<byte> stream = new Queue<byte>();

        public void ReceiveData(byte[] data)
        {
             CopyAndProcessData(data);    
        }

        private void CopyAndProcessData(byte[] data)
        {
            //Queue incoming packets.

            foreach (var b in data)
            {
                stream.Enqueue(b);
            }

            ProcessReceivedData();
        }

        protected virtual void ProcessReceivedData()
        {
            try
            {
                Boolean readyToProcess = false;
                Boolean gotStartByte = false;


                uint length = 0;


                //Check if there is a whole packet to process
                try
                {

                    //Only if we have enough data to process length will we actually look at it.
                    while (stream.Count > 3 && !gotStartByte)
                    {
                        //Keep searching until we get a start byte.
                        //Sometimes we might have garbage in the queue, this processes it out.
                        //If for some reason the packet structure isn't correct,
                        //an exception will throw.
                        if (stream.Peek() != (byte)XBeeSpecialBytes.StartByte)
                        {
                            stream.Dequeue();
                            gotStartByte = false;
                        }
                        else
                        {
                            gotStartByte = true;
                            //Peek length (first element is still start byte).
                            length = (uint)(stream.ElementAt(1) << 8 | stream.ElementAt(2)) + 3;

                            //Check to see if we have enough data on queue to form packet
                            // of if we should wait for more data before processing.
                            //Length + 1 includes the start byte for this check, we remove
                            //start byte for further processing.
                            if (stream.Count >= length + 1)
                            {
                                //Remove start byte for processing 
                                stream.Dequeue();
                                readyToProcess = true;
                            }
                        }

                    }
                }
                catch
                {
                    readyToProcess = false;
                }

                if (readyToProcess)
                {

                    byte[] packet = new byte[length];

                    for (int i = 0; i < packet.Length; i++)
                    {
                        packet[i] = stream.Dequeue();
                    }

                    var frame = XBeePacketUnmarshaler.Unmarshal(packet);

                    if (FrameReceived != null)
                        FrameReceived.Invoke(this, new FrameReceivedArgs(frame));


                    //Do another run until no data to process.
                    if (stream.Count - length > 0)
                    {
                        //Unfortunately there might still be data in the receive
                        //thread that doesn't get flushed through to here. 
                        //It's still there, but it will only be processed on the next
                        //frame arrival. This is only a problem for large sets of data
                        //and a heatbeat style read will fix this to flush through data.
                        ProcessReceivedData();
                    }
                }

            }
            catch (XBeeFrameException ex)
            {
                if (ex.Message == "Invalid Frame Checksum.")
                {
                    //flush until next start byte or empty queue
                    while (stream.Peek() != (byte)XBeeSpecialBytes.StartByte && stream.Count > 0)
                    {
                        stream.Dequeue();
                    }
                }
                else
                {
                    //Just flush everything out, there is data loss (the nature of 
                    //serial communications; you should handle this), but the program
                    //won't get into an unkown state and stop working altogether.

                    stream.Clear();
                    throw new XBeeException("Unable to unmarshal packet.", ex);

                }
            }
            catch (XBeeException ex)
            {
                stream.Clear();
                throw new XBeeException("Unable to unmarshal packet.", ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

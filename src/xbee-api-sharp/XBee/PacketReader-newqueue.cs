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


        //This is handled more like a queue now.
        //protected MemoryStream Stream = new MemoryStream();
        Queue<byte[]> Stream = new Queue<byte[]>();
        //private uint packetLength = 0;
        bool streamLock = false;
        Queue<byte> previousPartPacket = new Queue<byte>();

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
            //while (streamLock) { Thread.Sleep(1); };
            //foreach (var b in data.Where(b => Stream.Count != 0 || b != (byte) XBeeSpecialBytes.StartByte)) {
            /*foreach (var b in data.Where(b => Stream.Count != 0 || b != (byte)XBeeSpecialBytes.StartByte))
            {
                // Stream.WriteByte(b);
                Stream.Enqueue(b);
            }*/
            Stream.Enqueue(data);

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
                Boolean moreToProcess = false;
                //var sourceArray = Stream.ToArray();
                uint length = 0;
                byte[] lengthBytes = { 0x00, 0x00 };
                //TODO flush all packets on startup (or rety connection twice?)
                byte[] peekPacket = { 0x00 };
                //Check if there is a whole packet to process
                try
                {
                    //

                    //Check previous packet first
                    if (previousPartPacket.Count > 3)
                    {
                        byte[] previousPacketLength = { 0x00, 0x00 };
                        peekPacket = previousPartPacket.ToArray();
                        Array.Copy(previousPartPacket.ToArray(), previousPacketLength, 2);
                        if ((uint)(previousPacketLength[0] << 8 | previousPacketLength[1]) + 3 >= previousPartPacket.Count)
                        {
                            peekPacket = (byte[])peekPacket.Take((int)(previousPacketLength[0] << 8 | previousPacketLength[1]) + 3);
                            for (int i = 0; i < (uint)(previousPacketLength[0] << 8 | previousPacketLength[1]) + 3; i++)
                                previousPartPacket.Dequeue();
                            readyToProcess = true;
                        }

                    }

                    if (!readyToProcess)
                    {
                        peekPacket = Stream.Peek();


                        //Remove start byte if exists
                        if (peekPacket[0] == (byte)XBeeSpecialBytes.StartByte)
                            peekPacket = peekPacket.Skip(1).ToArray();
                        //Only if we have enough data to process length will we actually look at it.
                        if (peekPacket.Length > 3)
                        {
                            streamLock = true;

                            length = (uint)(peekPacket[0] << 8 | peekPacket[1]) + 3;

                            // check to see if we should wait for more data before processing.
                            if (peekPacket.Length > length)
                            {
                                //Part packet stored

                                foreach (var b in peekPacket.Skip((int)length).ToArray())
                                {
                                    previousPartPacket.Enqueue(b);
                                }

                                //Rest of packet handled, we can deque
                                Stream.Dequeue();
                                moreToProcess = true;
                                readyToProcess = true;
                            }
                            else if (peekPacket.Length == length)
                            {
                                //whole packet, we can deque
                                Stream.Dequeue();
                                readyToProcess = true;
                            }
                            else
                            {
                                foreach (var b in peekPacket)
                                {
                                    previousPartPacket.Enqueue(b);
                                }
                                Stream.Dequeue();
                                moreToProcess = true;
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
                    //If we have lots of data to process (more than a single packet size).
                    /*if (length < peekPacket.Length)
                    {


                        byte[] packet = new byte[length];
                        //streamLock = true;
                        Array.Copy(peekPacket, packet, packet.Length);
                        
                        var frame = XBeePacketUnmarshaler.Unmarshal(packet);
                        //packetLength = (uint)Stream.Length - length - 1;
                        //var data = Stream.ToArray().Skip((int)length + 1);


                        //Stream.SetLength(0);
                        //foreach (var b in data)
                        //{
                        //    Stream.WriteByte(b);
                        //}
                        //streamLock = false;

                        if (FrameReceived != null)
                            FrameReceived.Invoke(this, new FrameReceivedArgs(frame));

                        //Do another run until no data to process.
                        ProcessReceivedData();

                    }*/

                    if (length == peekPacket.Length )
                    {
                     
                        var frame = XBeePacketUnmarshaler.Unmarshal(peekPacket);
                        //packetLength = 0;
                        if (FrameReceived != null)
                            FrameReceived.Invoke(this, new FrameReceivedArgs(frame));
                        //Stream.SetLength(0);
                        if (moreToProcess) ProcessReceivedData();
                    }
                }

                streamLock = false;

                //var frame = XBeePacketUnmarshaler.Unmarshal(Stream.ToArray());
                //packetLength = 0;
                //if (FrameReceived != null)
                //    FrameReceived.Invoke(this, new FrameReceivedArgs(frame));



            }
            catch (XBeeFrameException ex)
            {
                //Stream.SetLength(0);
                Stream.Clear();
                previousPartPacket.Clear();
                //packetLength = 0;
                //streamLock = false;
                throw new XBeeException("Unable to unmarshal packet.", ex);


            }
            catch (XBeeException ex)
            {
                Stream.Clear();
                previousPartPacket.Clear();

                //Stream.SetLength(0);
                //packetLength = 0;
                //streamLock = false;
                throw new XBeeException("Unable to unmarshal packet.", ex);
            }
        }
    }
}

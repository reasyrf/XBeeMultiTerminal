using System;
using System.Threading;
using NLog;
using XBee.Exceptions;
using System.Collections;
using System.Collections.Generic;

namespace XBee
{
    public enum ApiTypeValue : byte
    {
        Disabled = 0x00,
        Enabled = 0x01,
        EnabledWithEscape = 0x02,
        Unknown = 0xFF
    }

    public class XBee
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IXBeeConnection connection;

        //private Thread receiveThread;
        //private bool stopThread;

        private Queue<XBeeFrame> processedFrames = new Queue<XBeeFrame>();

        private byte frameId = byte.MinValue;

        //private bool frameReceived = false;
        //private XBeeFrame lastFrame = null;
        //private Guid frameGUID;

        private IPacketReader reader;
        private ApiTypeValue apiType;

        public ApiTypeValue ApiType
        {
            get { return apiType; }
            set
            {
                apiType = value;
                reader = PacketReaderFactory.GetReader(apiType);
                reader.FrameReceived += FrameReceivedEvent;
                
            }
        }

        /*public XBeeFrame GetLastFrame()
        {
            frameReceived = false;
            return lastFrame;
        }*/

        public XBeeFrame GetNextQueueFrame()
        {
            if (processedFrames.Count > 0) return processedFrames.Dequeue();
            else return null;
        }

        /*public Guid GetLastFrameGUID()
        {
            return frameGUID;
        }*/

        /*public bool GetFrameReceived()
        {
            return frameReceived;
        }*/

        public void SetConnection(IXBeeConnection connection)
        {
            this.connection = connection;
            this.connection.Open();
            this.connection.SetPacketReader(reader);

            //stopThread = false;
            /*receiveThread = new Thread(new ThreadStart(ReceiveData));
            receiveThread.Name = "Receive Data Thread";
            receiveThread.IsBackground = true;
            receiveThread.Start();*/
             
        }

        public void Execute(XBeeFrame frame)
        {
            if (frame.FrameId != 0) {
                if (frameId == byte.MaxValue)
                    frameId = byte.MinValue;

                frame.FrameId = ++frameId;
            }

            var packet = new XBeePacket(frame);
            packet.Assemble();
            connection.Write(packet.Data);
        }


        //Never handle synchronously, it makes for bad things
        /*public T ExecuteQuery<T>(XBeeFrame frame) where T : XBeeFrame
        {
            return (T) ExecuteQuery(frame);
        }

        public T ExecuteQuery<T>(XBeeFrame frame, int timeout) where T : XBeeFrame
        {
            return (T) ExecuteQuery(frame, timeout);
        }

        public XBeeFrame ExecuteQuery(XBeeFrame frame)
        {
            //return ExecuteQuery(frame, 3000);
            return ExecuteQuery(frame, 1000);
        }

        public XBeeFrame ExecuteQuery(XBeeFrame frame, int timeout)
        {
            if (frame.FrameId == 0)
                throw new XBeeFrameException("FrameId cannot be zero on a synchronous request.");

            //lastFrame = null;
            frameReceived = false;

            lock (this) {
                Execute(frame);
            }

            while (!frameReceived && timeout > 0) {
                Thread.Sleep(10);
                timeout -= 10;
            }

            return lastFrame;
        }*/

        /*public void StopReceiveDataThread()
        {
            try {
                if (receiveThread != null) {
                    stopThread = true;
                    receiveThread.Join(2000);
                    receiveThread.Abort();
                    receiveThread.Join(2000);
                }
            } catch (Exception e) {
                logger.Info(e);
            } finally {
                receiveThread = null;
            }
        }*/

        //Once a full frame is generated, an event is triggered.
        //The frames are queued for processing, to remove the chance of duplicate packets
        public void FrameReceivedEvent(object sender, FrameReceivedArgs args)
        {
            //frameReceived = true;
            processedFrames.Enqueue(args.Response);
            //lastFrame = args.Response;
            //frameGUID = Guid.NewGuid();
            logger.Debug(args.Response);
            //logger.Debug("Test");
            
        }

        /*private void ReceiveData()
        {
            try {
                while (!stopThread) {
                    if (frameReceived)
                    {
                        

                        frameReceived = false;
                        lastFrame = null;
                        Thread.Sleep(1);
                    }

                    Thread.Sleep(1);
                    
                }
            } catch {
            }
        }*/
    }
}

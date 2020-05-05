using System;
using System.Threading;
using XBee.Frames;

namespace XBee.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var bee = new XBee {ApiType = ApiTypeValue.Enabled};
            bee.SetConnection(new SerialConnection("COM8", 9600));

            var request = new ATCommand(AT.ApiEnable) { FrameId = 1 };
            var frame = bee.ExecuteQuery(request, 1000);
            var value = ((ATCommandResponse) frame).Value;
            Console.WriteLine(String.Format("API type: {0}", ((ATLongValue) value).Value));

            request = new ATCommand(AT.BaudRate) { FrameId = 1 };
            frame = bee.ExecuteQuery(request, 1000);
            value = ((ATCommandResponse) frame).Value;
            Console.WriteLine(String.Format("Baud rate: {0}", ((ATLongValue) value).Value));

            request = new ATCommand(AT.MaximumPayloadLenght) { FrameId = 1 };
            frame = bee.ExecuteQuery(request, 1000);
            value = ((ATCommandResponse) frame).Value;
            Console.WriteLine(String.Format("Maximum Payload is: {0}", ((ATLongValue) value).Value));

            request = new ATCommand(AT.FirmwareVersion) { FrameId = 1 };
            frame = bee.ExecuteQuery(request, 1000);
            value = ((ATCommandResponse) frame).Value;
            Console.WriteLine(String.Format("Firmware Version: {0:X4}", ((ATLongValue) value).Value));

            request = new ATCommand(AT.NodeDiscover) { FrameId = 1 };
            bee.Execute(request);

            while (true) {
                Thread.Sleep(100);
            }
        }
    }
}

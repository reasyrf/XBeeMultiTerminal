using System;
using System.Text;
using XBee.Utils;

namespace XBee.Frames
{
    public class RemoteCommandResponse : XBeeFrame
    {
        public enum CommandStatusType
        {
            Ok = 0x00,
            Error = 0x01,
            InvalidCommand = 0x02,
            InvalidParameter = 0x03,
            TransmissionFailed = 0x04
        }

        private readonly PacketParser parser;

        public XBeeNode Source { get; private set; }
        public AT Command { get; private set; }
        public CommandStatusType CommandStatus { get; private set; }
        public ATValue Value { get; private set; }

        public RemoteCommandResponse(PacketParser parser)
        {
            this.parser = parser;
            CommandId = XBeeAPICommandId.REMOTE_AT_COMMAND_RESPONSE;
        }

        public override byte[] ToByteArray()
        {
            return new byte[] { };
        }

        public override void Parse()
        {
            FrameId = (byte) parser.ReadByte();
            Source = new XBeeNode { Address64 = parser.ReadAddress64(), Address16 = parser.ReadAddress16() };

            Command = parser.ReadATCommand();
            CommandStatus = (CommandStatusType) parser.ReadByte();

            if (Command == AT.NodeDiscover)
                ParseNetworkDiscovery();

            var type = ((ATAttribute)Command.GetAttr()).ValueType;

            if ((type != ATValueType.None) && parser.HasMoreData()) {
                switch (type) {
                    case ATValueType.Number:
                        var vData = parser.ReadData();
                        Value = new ATLongValue().FromByteArray(vData);
                        break;
                    case ATValueType.HexString:
                        var hexData = parser.ReadData();
                        Value = new ATStringValue(ByteUtils.ToBase16(hexData));
                        break;
                    case ATValueType.String:
                        var str = parser.ReadData();
                        Value = new ATStringValue(Encoding.UTF8.GetString(str));
                        break;
                }
            }
        }

        private void ParseNetworkDiscovery()
        {
            throw new NotImplementedException();
        }
    }
}

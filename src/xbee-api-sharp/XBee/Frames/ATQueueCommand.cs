using System;

namespace XBee.Frames
{
    public class ATQueueCommand : ATCommand
    {
        public ATQueueCommand(AT atCommand) : base(atCommand)
        {
            CommandId = XBeeAPICommandId.AT_COMMAND_QUEUE_REQUEST;
        }

        public override void Parse()
        {
            throw new NotImplementedException();
        }
    }
}

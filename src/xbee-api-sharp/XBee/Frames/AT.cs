using System;
using XBee.Utils;

namespace XBee.Frames
{

    public class ATAttribute : EnumAttribute
    {
        public ATAttribute(string atCommand, string description, ATValueType type)
        {
            ATCommand = atCommand;
            Description = description;
            ValueType = type;
        }

        public ATAttribute(string atCommand, string description, ATValueType type, ulong maxValue)
        {
            ATCommand = atCommand;
            Description = description;
            MaximumValue = maxValue;
            ValueType = type;
        }

        public string ATCommand { get; private set; }
        public string Description { get; private set; }
        public ATValueType ValueType { get; private set; }
        public ulong MaximumValue { get; private set; }
    }

    public class ATUtil
    {
        public static AT Parse(string value)
        {
            var atCommands = (AT[])Enum.GetValues(typeof(AT));
            var cmd = Array.Find(atCommands, at => ((ATAttribute)at.GetAttr()).ATCommand == value);

            if (cmd == 0)
                return AT.Unknown;

            return cmd;
        }
    }

    public enum ATValueType
    {
        None,
        Number,
        String,
        HexString,
    }

    public enum AT
    {
        [AT("DH", "Destination Address High", ATValueType.Number, 0xFFFFFFFF)]
        DestinationHigh = 0x10000,
        [AT("DL", "Destination Address Low", ATValueType.Number, 0xFFFFFFFF)]
        DestinationLow,
        [AT("MY", "16-bit Network Address", ATValueType.Number, 0xFFFE)]
        MyNetworkAddress,
        [AT("MP", "16-bit Parent Network Address", ATValueType.Number, 0xFFFE)]
        ParentAddress,
        [AT("NC", "Number of Remaining Children", ATValueType.Number, 0)]
        RemainingChildren,
        [AT("SH", "Serial Number High", ATValueType.Number, 0xFFFFFFFF)]
        SerialNumberHigh,
        [AT("SL", "Serial Number Low", ATValueType.Number, 0xFFFFFFFF)]
        SerialNumberLow,
        [AT("NI", "Node Identifier", ATValueType.String, 20)]
        NodeIdentifier,
        [AT("SE", "Source Endpoint", ATValueType.Number, 0xFF)]
        SourceEndpoint,
        [AT("DE", "Destination Endpoint", ATValueType.Number, 0xFF)]
        DestinationEndpoint,
        [AT("CI", "Cluster Identifier", ATValueType.Number, 0xFFFF)]
        ClusterIdentifier,
        [AT("NP", "Maximum RF Payload Bytes", ATValueType.Number, 0xFFFF)]
        MaximumPayloadLength,
        [AT("DD", "Device Type Identifier", ATValueType.Number, 0xFFFFFFFF)]
        DeviceTypeIdentifier,
        [AT("CH", "Operating Channel", ATValueType.Number)]
        OperatingChannel,
        [AT("ID", "Extended PAN ID", ATValueType.Number, 0xFFFFFFFFFFFFFFFF)]
        ExtendedPanId,
        [AT("OP", "Operating Extended PAN ID", ATValueType.Number, 0xFFFFFFFFFFFFFFFF)]
        OperatingExtendedPanId,
        [AT("NH", "Maximum Unicast Hops", ATValueType.Number, 0xFF)]
        UnicastHops,
        [AT("BH", "Broadcast Hops", ATValueType.Number, 0x1E)]
        BroadcastHops,
        [AT("OI", "Operating 16-bit PAN ID", ATValueType.Number, 0xFFFF)]
        OperatingPanId,
        [AT("NT", "Node Discovery Timeout", ATValueType.Number, 0xFF)]
        NodeDiscoveryTimeout,
        [AT("NO", "Network Discovery options", ATValueType.Number, 0x03)]
        NetworkDiscovertOptions,
        [AT("SC", "Scan Channel", ATValueType.Number, 0x7FFF)]
        ScanChannel,
        [AT("SD", "Scan Duration", ATValueType.Number, 7)]
        ScanDuration,
        [AT("ZS", "ZigBee Stack Profile", ATValueType.Number, 2)]
        ZigBeeStackProfile,
        [AT("NJ", "Node Join Time", ATValueType.Number, 0xFF)]
        NodeJoinTime,
        [AT("JV", "Channel Verification", ATValueType.Number)]
        ChannelVerification,
        [AT("NW", "Network Watchdog Timeout", ATValueType.Number, 0x64FF)]
        NetworkWatchdogTimeout,
        [AT("JN", "Join Notification", ATValueType.Number, 1)]
        JoinNotification,
        [AT("AR", "Aggregate Routing Notification", ATValueType.Number, 0xFF)]
        AggregateRoutingNotification,
        [AT("EE", "Encryption Enable", ATValueType.Number, 1)]
        EncryptionEnable,
        [AT("EO", "Encryption Options", ATValueType.Number, 0xFF)]
        EncryptionOptions,
        [AT("NK", "Network Encryption Key", ATValueType.HexString, 0)]
        NetworkEncryptionKey,
        [AT("KY", "Link Encryption Key", ATValueType.HexString, 0)]
        LinkEncryptionKey,
        [AT("PL", "Power Level", ATValueType.Number, 4)]
        PowerLevel,
        [AT("PM", "Power Mode", ATValueType.Number, 1)]
        PowerMode,
        [AT("DB", "Received Signal Strength", ATValueType.Number, 0xFF)]
        ReceivedSignalStrength,
        [AT("PP", "Peak Power", ATValueType.Number, 0x12)]
        PeakPower,
        [AT("AP", "API Enable", ATValueType.Number, 2)]
        ApiEnable,
        [AT("AO", "API Options", ATValueType.Number, 3)]
        ApiOptions,
        [AT("CE", "Routing Mode", ATValueType.Number, 3)]
        RoutingMode,
        [AT("BD", "Interface Data Rate", ATValueType.Number, 0xE1000)]
        BaudRate,
        [AT("NB", "Serial Parity", ATValueType.Number, 3)]
        Parity,
        [AT("SB", "Stop Bits", ATValueType.Number, 1)]
        StopBits,
        [AT("RO", "Packetization Timeout", ATValueType.Number, 0xFF)]
        PacketizationTimeout,
        [AT("D7", "DIO7 Configuration", ATValueType.Number, 7)]
        DigitalIO7,
        [AT("D6", "DIO6 Configuration", ATValueType.Number, 5)]
        DigitalIO6,
        [AT("IR", "IO Sample Rate", ATValueType.Number, 0xFFFF)]
        IOSampleRate,
        [AT("IC", "IO Digital Change Detection", ATValueType.Number, 0xFFFF)]
        IOChangeDetection,
        [AT("P0", "PWM0 Configuration", ATValueType.Number, 5)]
        Pwm0Configuration,
        [AT("P1", "DIO11 Configuration", ATValueType.Number, 5)]
        DigitalIO11,
        [AT("P2", "DIO12 Configuration", ATValueType.Number, 5)]
        DigitalIO12,
        [AT("P3", "DIO13 Configuration", ATValueType.Number, 5)]
        DigitalIO13,
        [AT("D0", "AD0/DIO0 Configuration", ATValueType.Number, 5)]
        DigitalIO0,
        [AT("D1", "AD1/DIO1 Configuration", ATValueType.Number, 5)]
        DigitalIO1,
        [AT("D2", "AD2/DIO2 Configuration", ATValueType.Number, 5)]
        DigitalIO2,
        [AT("D3", "AD3/DIO3 Configuration", ATValueType.Number, 5)]
        DigitalIO3,
        [AT("D4", "DIO4 Configuration", ATValueType.Number, 5)]
        DigitalIO4,
        [AT("D5", "DIO5 Configuration", ATValueType.Number, 5)]
        DigitalIO5,
        [AT("D8", "DIO8 Configuration", ATValueType.Number, 5)]
        DigitalIO8,
        [AT("LT", "Assoc LED Blink Time", ATValueType.Number, 0xFF)]
        LedBlinkTime,
        [AT("PR", "Pull-up Resistor", ATValueType.Number, 0x3FFF)]
        PullUpResistor,
        [AT("RP", "RSSI PWM Timer", ATValueType.Number, 0xFF)]
        RSSITimer,
        [AT("%V", "Supply Voltage", ATValueType.Number, 0xFFFF)]
        SupplyVoltage,
        [AT("V+", "Voltage Supply Monitoring", ATValueType.Number, 0xFFFF)]
        VoltageMonitoring,
        [AT("TP", "Reads the module temperature in Degrees Celsius", ATValueType.Number, 0xFFFF)]
        Temperature,
        [AT("VR", "Firmware Version", ATValueType.Number, 0xFFFF)]
        FirmwareVersion,
        [AT("HV", "Hardware Version", ATValueType.Number, 0xFFFF)]
        HardwareVersion,
        [AT("AI", "Association Indication", ATValueType.Number, 0xFF)]
        AssociationIndication,
        [AT("CT", "Command Mode Timeout", ATValueType.Number, 0x028F)]
        CommandModeTimeout,
        [AT("CN", "Exit Command Mode", ATValueType.Number)]
        ExitCommandMode,
        [AT("GT", "Guard Times", ATValueType.Number, 0x0CE4)]
        GuardTimes,
        [AT("CC", "Command Sequence Character", ATValueType.Number, 0xFF)]
        CommandSequenceCharacter,
        [AT("SM", "Sleep Mode", ATValueType.Number, 5)]
        SleepMode,
        [AT("SN", "Number of Sleep Periods", ATValueType.Number, 0xFFFF)]
        NumberOfSleepPeriods,
        [AT("SP", "Sleep Period", ATValueType.Number, 0xAF0)]
        SleepPeriod,
        [AT("ST", "Time Before Sleep", ATValueType.Number, 0xFFFE)]
        TimeBeforeSleep,
        [AT("SO", "Sleep Options", ATValueType.Number, 0xFF)]
        SleepOptions,
        [AT("WH", "Wake Host", ATValueType.Number, 0xFFFF)]
        WakeHost,
        [AT("PO", "Polling Rate", ATValueType.Number, 0x3E8)]
        PollingRate,
        [AT("AC", "Apply Changes", ATValueType.None)]
        ApplyChanges,
        [AT("WR", "Write", ATValueType.None)]
        Write,
        [AT("RE", "Restore Defaults", ATValueType.None)]
        RestoreDefaults,
        [AT("FR", "Software Reset", ATValueType.None)]
        SoftwareReset,
        [AT("NR", "Network Reset", ATValueType.Number, 1)]
        NetworkReset,
        [AT("SI", "Sleep Immediately", ATValueType.None)]
        SleepImmediately,
        [AT("CB", "Commissioning Pushbutton", ATValueType.None)]
        CommissioningPushButton,
        [AT("ND", "Node Discover", ATValueType.Number)]
        NodeDiscover,
        [AT("DN", "Destination Node", ATValueType.Number)]
        DestinationNode,
        [AT("IS", "Force Sample", ATValueType.None)]
        ForceSample,
        [AT("1S", "XBee Sensor Sample", ATValueType.None)]
        SensorSample,
        [AT("", "Unknown AT Command", ATValueType.None)]
        Unknown
    }
}

using XBee.Utils;

namespace XBee
{

    public class XBeeHardwareAttribute : EnumAttribute
    {
        public string Name { get; private set; }

        public XBeeHardwareAttribute(string name)
        {
            Name = name;
        }
    }

    public enum XBeeSeriesId : byte
    {
        [XBeeHardware("XBee Series 1")]
        XBeeSeries1 = 0x17,
        [XBeeHardware("XBee Series 1 Pro")]
        XBeeSeries1Pro = 0x18,
        [XBeeHardware("XBee Series 2")]
        XBeeSeries2 = 0x19,
        [XBeeHardware("XBee Series 2 Pro")]
        XBeeSeries2Pro = 0x1A,
        [XBeeHardware("XBee Series 2B Pro")]
        XBeeSeries2BPro = 0x1E,
        [XBeeHardware("XBee Unknown")]
        XBeeSeriesUnknown = 0xFF
    };
}

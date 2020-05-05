using System;
using System.Reflection;

namespace XBee.Utils
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class EnumAttribute : Attribute
    {
        public EnumAttribute()
        {
        }
    }

    public static class EnumExtension
    {
        public static EnumAttribute GetAttr(this Enum value)
        {
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());
            var atts = (EnumAttribute[]) fieldInfo.GetCustomAttributes(typeof(EnumAttribute), false);
            return atts.Length > 0 ? atts[0] : null;
        }
    }
}

using System;
using System.Linq;
using System.Reflection;


namespace Example.Dapper.Handlers
{
    public static class ExtractAttributeEnum
    {
        internal static Nullable<T> GetEnumFromValue<T>(object value) where T : struct
        {
            var enumTypeInfo = typeof(T).GetFields();
            var listEnumMembers = enumTypeInfo.Where(x =>
            {
                var attribute = x.GetCustomAttribute<EnumValueAttribute>();
                if (attribute != null)
                {
                    return ((EnumValueAttribute)attribute).Value == (string)value;
                }
                return false;
            });

            if (listEnumMembers == null || listEnumMembers.Count() == 0)
                return null;
            else
            {
                string enumElementName = listEnumMembers.FirstOrDefault().Name;
                return (T)Enum.Parse(typeof(T), enumElementName);
            }

        }

        internal static string GetEnumValue<T>(T value)
        {
            var enumTypeInfo = typeof(T).GetField(value.ToString());
            var attribute = (EnumValueAttribute)enumTypeInfo.GetCustomAttribute<EnumValueAttribute>();
            if (attribute != null)
            {
                return attribute.Value;
            }
            else
                return null;
        }

        internal static UsesEnumHandlerAttribute GetUsesEnumHandler<T>()
        {
            return typeof(T).GetTypeInfo().GetCustomAttribute<UsesEnumHandlerAttribute>();
        }
    }
}

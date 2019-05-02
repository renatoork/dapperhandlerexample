using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Example.Dapper.Handlers.Exceptions;

namespace Example.Dapper.Handlers
{
    public static class SqlHandler
    {

        public static void AddBool()
        {
            SqlMapper.AddTypeHandler<bool>(new BoolHandler());
        }

        public static void AddGuid()
        {
            SqlMapper.AddTypeHandler<Guid>(new GuidHandler());
        }

        public static void AddEnumChar<T>() where T : struct, IConvertible
        {
            ValidateCharEnum<T>();

            SqlMapper.AddTypeHandler<T>(new EnumCharHandler<T>());
        }

        public static void AddEnumString<T>() where T : struct, IConvertible
        {
            ValidateStringEnum<T>();

            SqlMapper.AddTypeHandler<T>(new EnumStringHandler<T>());
        }

        private static void ValidateCharEnum<T>() where T : struct, IConvertible
        {
            Type genericType = typeof(T);

            bool hasItemNotMapped = Enum.GetValues(genericType).Cast<T>().Any(s => Convert.ToInt32(s) == 1);

            if (hasItemNotMapped)
                throw new EnumCharNotMappedException<T>();
        }
        private static void ValidateStringEnum<T>() where T : struct, IConvertible
        {
            var enumTypeInfo = typeof(T).GetFields().ToList();
            enumTypeInfo.ForEach(x =>
            {
                if (!x.FieldType.Name.Contains("Int32"))
                {
                    var attribute = x.GetCustomAttribute<EnumValueAttribute>();
                    if (attribute == null)
                    {
                        throw new EnumStringNotMappedException<T>();
                    }
                }
            });

        }


    }
}

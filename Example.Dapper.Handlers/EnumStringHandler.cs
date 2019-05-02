using Dapper;

using Example.Dapper.Handlers.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Dapper.Handlers
{
    public class EnumStringHandler<T> : SqlMapper.TypeHandler<T> where T : struct, IConvertible
    {
        private Dictionary<string, T> _dictionary;

        public EnumStringHandler()
        {
            _dictionary = new Dictionary<string, T>();
        }

        public override T Parse(object value)
        {
            string valueString = (string)value;

            if (_dictionary.TryGetValue(valueString, out T result))
                return result;
            else
            {
                try
                {
                    result = (T)ExtractAttributeEnum.GetEnumFromValue<T>(value);
                    _dictionary.Add(valueString, result);
                    return result;
                }
                catch (Exception)
                {
                    throw new EnumStringParseException<T>(value);
                }
            }
        }

        public override void SetValue(IDbDataParameter parameter, T value)
        {
            var result = ExtractAttributeEnum.GetEnumValue<T>(value);

            if (result != null)
            {
                if (string.IsNullOrEmpty(result))
                    parameter.Value = DBNull.Value;
                else
                    parameter.Value = result;
            }
            else
                throw new EnumStringNotMappedException<T>();


        }
    }
}

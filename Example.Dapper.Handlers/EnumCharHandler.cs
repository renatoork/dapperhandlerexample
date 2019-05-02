using System;
using System.Data;
using Dapper;
using Example.Dapper.Handlers.Exceptions;
using System.Collections.Generic;

namespace Example.Dapper.Handlers
{
    internal class EnumCharHandler<T> : SqlMapper.TypeHandler<T> where T : struct, IConvertible
    {
        private Dictionary<string, T> _dictionary;

        public EnumCharHandler()
        {
            _dictionary = new Dictionary<string, T>();
        }

        public override T Parse(object value)
        {
            try
            {
                string valueString = (string)value;
                if (_dictionary.TryGetValue(valueString, out T result))
                    return result;
                else
                {
                    result = (T)Enum.ToObject(typeof(T), ((string)value)[0]);
                    _dictionary.Add(valueString, result);
                    return result;
                }
            }
            catch (Exception)
            {
                throw new EnumCharParseException<T>(value);
            }
        }

        public override void SetValue(IDbDataParameter parameter, T value)
        {

            if (Enum.Parse(typeof(T), value.ToString()) is Enum enumerator)
            {
                char charValue = Convert.ToChar(enumerator);
                if (charValue != '\0')
                {
                    parameter.Value = charValue;
                }
                else
                {
                    parameter.Value = DBNull.Value;
                }
            }
            else
                new EnumCharNotMappedException<T>();
        }
    }
}
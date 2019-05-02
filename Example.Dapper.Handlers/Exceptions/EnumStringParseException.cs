using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Dapper.Handlers.Exceptions
{
    public class EnumStringParseException<T> : Exception where T : struct, IConvertible
    {       

        public EnumStringParseException(object value) : base(SetParameters(value))
        {

        }       

        public EnumStringParseException(object value, Exception innerException) : base(SetParameters(value), innerException)
        {

        }

        private static string SetParameters(object value)
        {
            return $"Valor '{(string)value}' não mapeado para o Enum  '{typeof(T).FullName}'";
        }

        private EnumStringParseException(string message) : base(message)
        {

        }

        private EnumStringParseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Dapper.Handlers.Exceptions
{
    public class EnumCharParseException<T> : Exception where T : struct, IConvertible
    {       

        public EnumCharParseException(object value) : base(SetParameters(value))
        {

        }       

        public EnumCharParseException(object value, Exception innerException) : base(SetParameters(value), innerException)
        {

        }

        private static string SetParameters(object value)
        {
            return $"O caracter '{(string)value}' não mapeado para o Enum  '{typeof(T).FullName}'";
        }

        private EnumCharParseException(string message) : base(message)
        {

        }

        private EnumCharParseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

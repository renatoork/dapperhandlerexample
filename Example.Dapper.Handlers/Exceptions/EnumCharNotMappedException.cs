using System;

namespace Example.Dapper.Handlers.Exceptions
{
    public class EnumCharNotMappedException<T> : Exception where T : struct, IConvertible
    {
        public EnumCharNotMappedException() : base(SetParameters())
        {

        }

        public EnumCharNotMappedException( Exception innerException) : base(SetParameters(), innerException)
        {

        }

        private static string SetParameters()
        {
            return $"Enum '{typeof(T).FullName}' sem mapeamento de caracteres";
        }

        private EnumCharNotMappedException(string message) : base(message)
        {

        }

        private EnumCharNotMappedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
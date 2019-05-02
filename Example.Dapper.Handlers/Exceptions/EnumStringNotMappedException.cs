using System;

namespace Example.Dapper.Handlers.Exceptions
{
    public class EnumStringNotMappedException<T> : Exception where T : struct, IConvertible
    {
        public EnumStringNotMappedException() : base(SetParameters())
        {

        }

        public EnumStringNotMappedException(Exception innerException) : base(SetParameters(), innerException)
        {

        }

        private static string SetParameters()
        {
            return $"Enum '{typeof(T).FullName}' sem mapeamento";
        }

        private EnumStringNotMappedException(string message) : base(message)
        {

        }

        private EnumStringNotMappedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
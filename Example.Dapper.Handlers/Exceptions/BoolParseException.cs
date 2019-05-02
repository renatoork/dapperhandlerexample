using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Dapper.Handlers.Exceptions
{
    public class BoolParseException: Exception
    {
        static string _errorMessage = "Valor do boolean deve estar no intervalo S ou N";

        public BoolParseException() : base(_errorMessage)
        {
        }

        public BoolParseException(Exception innerException) : base(_errorMessage, innerException)
        {
        }
    }
}

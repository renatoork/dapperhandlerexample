using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Dapper.Handlers.Exceptions
{
    public class GuidParseException: Exception
    {
        static string _errorMessage = "Valor do Guid inválido.";
        public GuidParseException() : base(_errorMessage)
        {            
        }
    }
}

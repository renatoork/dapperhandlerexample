using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Example.Dapper.Handlers.Exceptions;

namespace Example.Dapper.Handlers
{
    public class GuidHandler : SqlMapper.TypeHandler<Guid>
    {
        public override Guid Parse(object value)
        {
            string valueString = "";
            try
            {
                valueString = (string)value;
                return Guid.Parse(valueString);
            }
            catch (Exception)
            {
                throw new GuidParseException();
            }
        }

        public override void SetValue(IDbDataParameter parameter, Guid value)
        {
            parameter.Value = value.ToString();
        }


    }
}

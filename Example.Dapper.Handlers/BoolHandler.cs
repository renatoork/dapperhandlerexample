using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Example.Dapper.Handlers.Exceptions;

namespace Example.Dapper.Handlers
{
    public class BoolHandler : SqlMapper.TypeHandler<bool>
    {
        public override bool Parse(object value)
        {
            try
            {
                string valueString = (string)value;

                if (valueString == "S")
                    return true;
                else if (valueString == "N")
                    return false;
                else
                    throw new BoolParseException();
            }
            catch(Exception)
            {
                throw new BoolParseException();
            }
        }

        public override void SetValue(IDbDataParameter parameter, bool value)
        {
            if (value == true)
                parameter.Value = "S";
            else
                parameter.Value = "N";
        }

        
    }
}

using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Example.Dapper.Handlers.Test.Performance.Models;

namespace Example.Dapper.Handlers.Test.Performance
{
    public class EnumVaziaHandler : SqlMapper.TypeHandler<Vazia>
    {
        public override Vazia Parse(object value)
        {
            return Vazia.Valor;
        }

        public override void SetValue(IDbDataParameter parameter, Vazia value)
        {
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Dapper.Handlers.Test
{
    public enum StringEnumNotMapped
    {
        Nenhum,
        Feito,
        [EnumValue("CO")]
        Concluido,
        EmProducao,
        Testado
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Dapper.Handlers.Test
{
    public enum Status
    {
        [EnumValue("FE")]
       Feito,
        [EnumValue("CO")]
       Concluido,
        [EnumValue("EP")]
       EmProducao,
        [EnumValue("TE")]
       Testado
    }
}

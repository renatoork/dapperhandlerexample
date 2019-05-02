﻿using Example.Dapper.Handlers.Test.Performance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Dapper.Handlers.Test
{
    public class EnumBoolModelVazia
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public Vazia Entregue { get; set; }
        public Vazia Liberado { get; set; }
        public string Situacao { get; set; }
        public string Status { get; set; }
        public string SituacaoNula { get; set; }
        public string StatusNulo { get; set; }
        public double Valor { get; set; }

    }
}

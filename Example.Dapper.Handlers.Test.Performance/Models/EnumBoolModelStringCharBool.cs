using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Dapper.Handlers.Test
{
    public class EnumBoolModelStringCharBool
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public bool Entregue { get; set; }
        public bool? Liberado { get; set; }
        public Situacao Situacao { get; set; }
        public Status Status { get; set; }
        public Situacao? SituacaoNula { get; set; }
        public Status? StatusNulo { get; set; }
        public double Valor { get; set; }

    }
}

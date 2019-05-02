using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Dapper.Handlers.Test
{
    public class EnumBoolModelChar
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string Entregue { get; set; }
        public string Liberado { get; set; }
        public Situacao Situacao { get; set; }
        public string Status { get; set; }
        public Situacao? SituacaoNula { get; set; }
        public string StatusNulo { get; set; }
        public double Valor { get; set; }

    }
}

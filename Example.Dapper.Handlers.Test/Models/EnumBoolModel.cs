using System;

namespace Example.Dapper.Handlers.Test
{
    public class EnumBoolModel
    {
        public int? Id { get; set; }
        public string Descricao { get; set; }
        public bool BoolEnum { get; set; }
        public bool? NullBoolEnum { get; set; }
        public CharEnum CharEnum { get; set; }
        public StringEnum StringEnum { get; set; }
        public double Valor { get; set; }
        public Guid? Guid_Id { get; set; }
    }
}

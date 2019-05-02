using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Dapper.Handlers.Test.Models
{
    [Table("GLO_BOOLENUM")]
    public class DatabaseModel
    {
        [Key]
        public int? Id { get; set; }
        public string Descricao { get; set; }
        [Required]
        public string BoolEnum { get; set; }
        public string NullBoolEnum { get; set; }
        public string CharEnum { get; set; }
        public string StringEnum { get; set; }
        public double Valor { get; set; }
        public Guid Guid_Id { get; set; }
    }
}

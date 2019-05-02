using Dapper;
using FluentAssertions;
using Example.Dapper.Handlers.Exceptions;
using Example.Dapper.Handlers.Test.Models;
using Example.Test.MultiDbCreator;
using Example.Test.xUnit;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Example.Dapper.Handlers.Test
{
    public class TestHandlers
    {
        string _insert = @"insert
                             into GLO_BOOLENUM(Id,
                                               Descricao,
                                               BoolEnum,
                                               NullBoolEnum,
                                               CharEnum,
                                               StringEnum,
                                               Valor,
                                               Guid_Id)
                                        values(:Id,
                                               :Descricao,
                                               :BoolEnum,
                                               :NullBoolEnum,
                                               :CharEnum,
                                               :StringEnum,
                                               :Valor, 
                                               :Guid_Id)";

        string _select = @"select Id,
                                  Descricao,
                                  BoolEnum,
                                  NullBoolEnum,
                                  CharEnum,
                                  StringEnum,
                                  Valor,
                                  Guid_Id
                            from GLO_BOOLENUM
                           where Id = :id";

        public TestHandlers()
        {
            SqlHandler.AddBool();
            SqlHandler.AddEnumChar<CharEnum>();
            SqlHandler.AddEnumString<StringEnum>();
            SqlHandler.AddGuid();
            new DbCreator()
                .UseEntity<DatabaseModel>();
        }

        [Theory(DisplayName = "Write using string, Read as real types")]
        [DbData(1, "S", true, null, null, "A", CharEnum.Aprovado, "FE", StringEnum.Feito, "11D3AFCE-7F6A-4EE7-933E-2C45923B2EA2")]
        [DbData(2, "N", false, "S", true, "C", CharEnum.Cancelado, "EP", StringEnum.EmProducao, "11D3AFCE-7F6A-4EE7-933E-2C45923B2EA2")]
        [DbData(3, "S", true, "N", false, null, CharEnum.Nenhum, null, StringEnum.Nenhum, "11D3AFCE-7F6A-4EE7-933E-2C45923B2EA2")]
        public void WriteUsingStringReadAsRealTypes(
            DbConnection conn,
            int id,
            string sqlBool, bool expectedBool,
            string sqlNullBool, bool? expectedNullBool,
            string sqlChar, CharEnum expectedCharEnum,
            string sqlString, StringEnum expectedStringEnum,
            string guid_Id)
        {
            var expected = new EnumBoolModel
            {
                Id = id,
                Descricao = "Lorem ipsum dolor sit amet",
                BoolEnum = expectedBool,
                NullBoolEnum = expectedNullBool,
                StringEnum = expectedStringEnum,
                CharEnum = expectedCharEnum,
                Valor = 123456.78,
                Guid_Id = Guid.Parse(guid_Id)
            };

            conn.Execute(_insert,
                new
                {
                    Id = id,
                    Descricao = "Lorem ipsum dolor sit amet",
                    BoolEnum = sqlBool,
                    NullBoolEnum = sqlNullBool,
                    CharEnum = sqlChar,
                    StringEnum = sqlString,
                    Valor = 123456.78,
                    Guid_Id = Guid.Parse(guid_Id)
                });

            EnumBoolModel result = conn.QueryFirst<EnumBoolModel>(
                _select, new { id });

            result.ShouldBeEquivalentTo(expected);
        }


        [Theory(DisplayName = "Write using real types, Read as string")]
        [DbData(4, true, "S", true, "S", CharEnum.Aprovado, "A", StringEnum.EmProducao, "EP", "11D3AFCE-7F6A-4EE7-933E-2C45923B2EA2")]
        [DbData(5, false, "N", false, "N", CharEnum.Pendente, "P", StringEnum.Feito, "FE", "11D3AFCE-7F6A-4EE7-933E-2C45923B2EA2")]
        [DbData(6, true, "S", null, null, CharEnum.Nenhum, null, StringEnum.Nenhum, null, "11D3AFCE-7F6A-4EE7-933E-2C45923B2EA2")]
        public void WriteUsingRealTypesReadAsString(
            DbConnection conn,
            int id,
            bool boolValue, string expectedBool,
            bool? nullableBoolValue, string expectedNullableBool,
            CharEnum charEnumValue, string expectedCharEnum,
            StringEnum stringEnumValue, string expectedStringEnum,
            string guid_Id )
        {
            var expected = new DatabaseModel
            {
                Id = id,
                Descricao = "Lorem ipsum dolor sit amet",
                BoolEnum = expectedBool,
                NullBoolEnum = expectedNullableBool,
                CharEnum = expectedCharEnum,
                StringEnum = expectedStringEnum,
                Valor = 123456.78,
                Guid_Id = Guid.Parse(guid_Id)
            };

            conn.Execute(_insert,
                            new
                            {
                                Id =  id,
                                Descricao = "Lorem ipsum dolor sit amet",
                                BoolEnum = boolValue,
                                NullBoolEnum = nullableBoolValue,
                                CharEnum = charEnumValue,
                                StringEnum = stringEnumValue,
                                Valor = 123456.78,
                                Guid_Id = Guid.Parse(guid_Id)
                            });

            DatabaseModel result = conn.QueryFirst<DatabaseModel>(
                _select, new { id });

            result.ShouldBeEquivalentTo(expected);
        }

        [Fact(DisplayName ="Register a not mapped Char Enum")]
        public void RegisterHandlerofCharEnumNotMapped()
        {
            Action test = () => { SqlHandler.AddEnumChar<CharEnumNotMapped>(); };
            test.ShouldThrow<EnumCharNotMappedException<CharEnumNotMapped>>();
        }

        [Fact(DisplayName = "Register a not mapped String Enum")]
        public void RegisterHandlerStringEnumNotMapped()
        {
            Action test = () => { SqlHandler.AddEnumString<StringEnumNotMapped>(); };
            test.ShouldThrow<EnumStringNotMappedException<StringEnumNotMapped>>();
        }
    }
}

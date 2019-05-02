using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Example.Dapper.Handlers.Test
{
    public class EnumBoolRepositoryOracle : IDisposable, IEnumBoolRepository
    {
        private DbConnection _connect;

        public EnumBoolRepositoryOracle(DbConnection connect)
        {
            _connect = connect;
        }

        public void CreateTable()
        {
            // Todo
        }

        public List<EnumBoolModelStringCharBool> GetAllHandlerTodos(int qtd)
        {
            return _connect.Query<EnumBoolModelStringCharBool>(
                @"select *
                    from MGGLO.GLO_ENUMBOOLPERFORMANCE
                   where rownum <= :qtd", new { qtd }).AsList();
        }

        public List<EnumBoolModelString> GetAllHandlerString(int qtd)
        {
            return _connect.Query<EnumBoolModelString>(
                @"select *
                    from MGGLO.GLO_ENUMBOOLPERFORMANCE
                   where rownum <= :qtd", new { qtd }).AsList();
        }

        public List<EnumBoolModelChar> GetAllHandlerChar(int qtd)
        {
            return _connect.Query<EnumBoolModelChar>(
                @"select *
                    from MGGLO.GLO_ENUMBOOLPERFORMANCE
                   where rownum <= :qtd", new { qtd }).AsList();
        }

        public List<EnumBoolModelBool> GetAllHandlerBool(int qtd)
        {
            return _connect.Query<EnumBoolModelBool>(
                @"select *
                    from MGGLO.GLO_ENUMBOOLPERFORMANCE
                   where rownum <= :qtd", new { qtd }).AsList();
        }

        public List<EnumBoolModel> GetAllHandlerNenhum(int qtd)
        {
            return _connect.Query<EnumBoolModel>(
                @"select *
                    from MGGLO.GLO_ENUMBOOLPERFORMANCE
                   where rownum <= :qtd", new { qtd }).AsList();
        }

        public void DropTable()
        {
            // Todo
        }

        public List<EnumBoolModelVazia> GetAllHandlerVazia(int qtd)
        {
            return _connect.Query<EnumBoolModelVazia>(
                @"select *
                    from MGGLO.GLO_ENUMBOOLPERFORMANCE
                   where rownum <= :qtd", new { qtd }).AsList();
        }


        public void Dispose()
        {
        }
    }
}

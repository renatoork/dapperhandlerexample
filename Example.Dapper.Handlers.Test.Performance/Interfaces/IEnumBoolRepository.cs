using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Dapper.Handlers.Test
{
    public interface IEnumBoolRepository
    {
        void CreateTable();
        List<EnumBoolModelStringCharBool> GetAllHandlerTodos(int qtd);
        List<EnumBoolModelString> GetAllHandlerString(int qtd);
        List<EnumBoolModelChar> GetAllHandlerChar(int qtd);
        List<EnumBoolModelBool> GetAllHandlerBool(int qtd);
        List<EnumBoolModel> GetAllHandlerNenhum(int qtd);
        List<EnumBoolModelVazia> GetAllHandlerVazia(int qtd);
        void DropTable();

        void Dispose();
    }
}

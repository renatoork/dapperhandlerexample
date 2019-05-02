using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;
using Example.Dapper.Handlers;
using Dapper;
using Example.Dapper.Handlers.Test.Performance.Models;
using BenchmarkDotNet.Attributes.Jobs;
using Example.Dapper.Handlers.Test.Performance;

namespace Example.Dapper.Handlers.Test
{
    //[SimpleJob(launchCount: 1, warmupCount: 3, targetCount: 5)]
    public class TestPerformance
    {
        IEnumBoolRepository _repository;
        int _qtdRegistros = 100000;

        public TestPerformance()
        {
            string connectString = Environment.GetEnvironmentVariable("ORACLE_TEST");

            if (string.IsNullOrEmpty(connectString))
                throw new ArgumentNullException("Base de testes não definida nas variáveis de ambiente");

            DbConnection connect = new OracleConnection(connectString);
            _repository = new EnumBoolRepositoryOracle(connect);
        }

        [GlobalSetup]
        public void Inicializa()
        {
            try
            {
                _repository.DropTable();
            }
            catch
            {
            }
            _repository.CreateTable();
        }

        [Benchmark(Baseline = true)]
        public void TesteSemHandler()
        {
            List<EnumBoolModel> retorno = _repository.GetAllHandlerNenhum(_qtdRegistros);
        }

        [Benchmark]
        public void TesteHandlerTodos()
        {
            SqlHandler.AddBool();
            SqlHandler.AddEnumChar<Situacao>();
            SqlHandler.AddEnumString<Status>();
            List<EnumBoolModelStringCharBool> retorno = _repository.GetAllHandlerTodos(_qtdRegistros);
        }

        [Benchmark]
        public void TesteHandlerString()
        {
            SqlHandler.AddEnumString<Status>();
            List<EnumBoolModelString> retorno = _repository.GetAllHandlerString(_qtdRegistros);
        }

        [Benchmark]
        public void TesteHandlerChar()
        {
            SqlHandler.AddEnumChar<Situacao>();
            List<EnumBoolModelChar> retorno = _repository.GetAllHandlerChar(_qtdRegistros);
        }

        [Benchmark]
        public void TesteHandlerBool()
        {
            SqlHandler.AddBool();
            List<EnumBoolModelBool> retorno = _repository.GetAllHandlerBool(_qtdRegistros);
        }

        [Benchmark]
        public void TesteHandlerVazia()
        {
            SqlMapper.AddTypeHandler<Vazia>(new EnumVaziaHandler());
            List<EnumBoolModelVazia> retorno = _repository.GetAllHandlerVazia(_qtdRegistros);
        }

        [GlobalCleanup]
        public void ApagaTabela()
        {
            _repository.DropTable();
        }
    }
}

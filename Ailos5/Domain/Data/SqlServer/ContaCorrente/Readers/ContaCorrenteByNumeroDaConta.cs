using AilosInfra.Interfaces.DataBase.Dapper.UnitOfWorkFactory;
using AilosInfra.Settings.DataBases.Dapper.Settings;
using AilosInfra.Util.TransportsResults;
using Dapper;
using Domain.Data.SqlServer.ContaCorrente.Interfaces.Readers;
using Domain.Data.SqlServer.ContaCorrente.Parameters.Readers;
using Domain.Data.SqlServer.ContaCorrente.Queries;
using Entitie = Domain.Entities.Sql;

namespace Domain.Data.SqlServer.ContaCorrente.Readers
{
    public class ContaCorrenteByNumeroDaConta : IContaCorrenteByNumeroDaConta
    {
        private IUnitOfWorkFactory<Entitie.ContaCorrente> _Factory;
        private ConnectionSettings _ConnectionSettings;

        public ContaCorrenteByNumeroDaConta(
            IUnitOfWorkFactory<Entitie.ContaCorrente> factory,
            ConnectionSettings connectionSettings)
        {
            _Factory = factory;
            _ConnectionSettings = connectionSettings;
        }

        public async Task<TransportResult<Entitie.ContaCorrente>> GetByNumeroDaConta(ContaCorrenteByNumeroDaContaParameter item)
        {
            var fac = await _Factory.Create(_ConnectionSettings);
            var query = ContaCorrenteQueries.GetByNumeroDaConta();
            var parameter = new DynamicParameters();

            parameter.Add("NumeroDaConta", item.NumeroDaConta);

            //if (item.IncluirAtivas != null)
            //{
            //    query += " AND Ativo = @Ativo";
            //    parameter.Add("Ativo", item.IncluirAtivas);
            //}

            //if (item.IncluirDeletadas != null)
            //{
            //    query += " AND Deleted = @Deleted";
            //    parameter.Add("Deleted", item.IncluirDeletadas);
            //}

            query += " ORDER BY Id DESC";

            var result = await fac.GetAsync(
                new CommandSettings<Entitie.ContaCorrente>
                {
                    CommandType = TypeCommand.Query,
                    Entity = new Entitie.ContaCorrente(),
                    Parameters = parameter,
                    Query = query
                });

            return TransportResult<Entitie.ContaCorrente>.Create(result);
        }
    }
}
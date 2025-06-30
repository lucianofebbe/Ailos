using AilosInfra.Interfaces.DataBase.Dapper.UnitOfWorkFactory;
using AilosInfra.Settings.DataBases.Dapper.Settings;
using AilosInfra.Util.TransportsResults;
using Dapper;
using Domain.Data.SqlServer.Movimento.Interfaces.Readers;
using Domain.Data.SqlServer.Movimento.Parameters.Readers;
using Domain.Data.SqlServer.Movimento.Queries;
using Entitie = Domain.Entities.Sql;

namespace Domain.Data.SqlServer.Movimento.Readers
{
    public class MovimentoByIdContaCorrente : IMovimentoByIdContaCorrente
    {
        private IUnitOfWorkFactory<Entitie.Movimento> _Factory;
        private ConnectionSettings _ConnectionSettings;
        public MovimentoByIdContaCorrente(
            IUnitOfWorkFactory<Entitie.Movimento> factory,
            ConnectionSettings connectionSettings)
        {
            _Factory = factory;
            _ConnectionSettings = connectionSettings;
        }

        public async Task<TransportResult<List<Entitie.Movimento>>> GetByIdContaCorrente(MovimentoGetByIdContaCorrenteParameter item)
        {
            var fac = await _Factory.Create(_ConnectionSettings);
            var parameter = new DynamicParameters();
            parameter.Add("IdContaCorrente", item.IdContaCorrente);

            var result = await fac.GetAllAsync(
                new CommandSettings<Entitie.Movimento>
                {
                    CommandType = TypeCommand.Query,
                    Entity = new Entitie.Movimento(),
                    Parameters = parameter,
                    Query = MovimentoQueries.GetByIdContaCorrente()
                });

            return TransportResult<List<Entitie.Movimento>>.Create(result);
        }
    }
}

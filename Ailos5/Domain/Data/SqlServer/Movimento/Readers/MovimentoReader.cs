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
    public class MovimentoReader : IMovimentoReader
    {
        private IUnitOfWorkFactory<Entitie.Movimento> _Factory;
        private ConnectionSettings _ConnectionSettings;
        public MovimentoReader(
            IUnitOfWorkFactory<Entitie.Movimento> factory,
            ConnectionSettings connectionSettings)
        {
            _Factory = factory;
            _ConnectionSettings = connectionSettings;
        }

        public async Task<TransportResult<Entitie.Movimento>> GetByIdContaCorrente(MovimentoGetByIdContaCorrenteParameter item)
        {
            var guid = Guid.NewGuid();
            var fac = await _Factory.Create(_ConnectionSettings);
            var parameter = new DynamicParameters();
            parameter.Add("IdContaCorrente", item.IdContaCorrente);

            var result = await fac.GetAsync(
                new CommandSettings<Entitie.Movimento>
                {
                    CommandType = TypeCommand.Query,
                    Entity = new Entitie.Movimento(),
                    Parameters = parameter,
                    Query = MovimentoQueries.GetByIdContaCorrente()
                });

            if (result.Id > 0)
            {
                result.Guid = guid;
                return TransportResult<Entitie.Movimento>.Create(result);
            }
            return TransportResult<Entitie.Movimento>.Create(null);
        }
    }
}

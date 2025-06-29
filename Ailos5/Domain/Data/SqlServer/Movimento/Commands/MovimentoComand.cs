using AilosInfra.Interfaces.DataBase.Dapper.UnitOfWorkFactory;
using AilosInfra.Settings.DataBases.Dapper.Settings;
using AilosInfra.Util.TransportsResults;
using Dapper;
using Domain.Data.SqlServer.Movimento.Interfaces.Commands;
using Domain.Data.SqlServer.Movimento.Parameters.Commands;
using Domain.Data.SqlServer.Movimento.Queries;
using Entitie = Domain.Entities.Sql;

namespace Domain.Data.SqlServer.Movimento.Commands
{
    public class MovimentoComand: IMovimentoComand
    {
        private IUnitOfWorkFactory<Entitie.Movimento> _Factory;
        private ConnectionSettings _ConnectionSettings;

        public MovimentoComand(
            IUnitOfWorkFactory<Entitie.Movimento> factory,
            ConnectionSettings connectionSettings)
        {
            _Factory = factory;
            _ConnectionSettings = connectionSettings;
        }

        public async Task<TransportResult<Entitie.Movimento>> CreateAsync(MovimentoCreateParameter item)
        {
            var guid = Guid.NewGuid();
            var fac = await _Factory.Create(_ConnectionSettings);
            var parameter = new DynamicParameters();
            parameter.Add("IdFather", item.IdFather);
            parameter.Add("Guid", guid);
            parameter.Add("Created", DateTime.UtcNow);
            parameter.Add("Updated", DateTime.UtcNow);
            parameter.Add("Deleted", item.Deleted);
            parameter.Add("IdContaCorrente", item.IdContaCorrente);
            parameter.Add("DataMovimentacao", item.DataMovimento);
            parameter.Add("TipoMovimento", item.TipoMovimento);
            parameter.Add("Valor", item.Valor);

            var result = await fac.InsertAsync(
                new CommandSettings<Entitie.Movimento>
                {
                    CommandType = TypeCommand.Query,
                    Entity = new Entitie.Movimento(),
                    Parameters = parameter,
                    Query = MovimentoQueries.Create()
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

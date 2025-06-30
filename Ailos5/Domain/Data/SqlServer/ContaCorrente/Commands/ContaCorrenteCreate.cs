using AilosInfra.Interfaces.DataBase.Dapper.UnitOfWorkFactory;
using AilosInfra.Settings.DataBases.Dapper.Settings;
using AilosInfra.Util.TransportsResults;
using Dapper;
using Domain.Data.SqlServer.ContaCorrente.Interfaces.Commands;
using Domain.Data.SqlServer.ContaCorrente.Parameters.Commands;
using Domain.Data.SqlServer.ContaCorrente.Queries;
using Entitie = Domain.Entities.Sql;

namespace Domain.Data.SqlServer.ContaCorrente.Commands
{
    public class ContaCorrenteCreate : IContaCorrenteCreate
    {
        private IUnitOfWorkFactory<Entitie.ContaCorrente> _Factory;
        private ConnectionSettings _ConnectionSettings;

        public ContaCorrenteCreate(
            IUnitOfWorkFactory<Entitie.ContaCorrente> factory,
            ConnectionSettings connectionSettings)
        {
            _Factory = factory;
            _ConnectionSettings = connectionSettings;
        }

        public async Task<TransportResult<Entitie.ContaCorrente>> CreateAsync(ContaCorrenteCreateParameter item)
        {
            var guid = Guid.NewGuid();
            var numeroDaConta = Guid.NewGuid();
            var fac = await _Factory.Create(_ConnectionSettings);
            var parameter = new DynamicParameters();
            parameter.Add("IdFather", 0);
            parameter.Add("Guid", guid);
            parameter.Add("Created", DateTime.UtcNow);
            parameter.Add("Updated", DateTime.UtcNow);
            parameter.Add("Deleted", item.Deleted);
            parameter.Add("NumeroDaConta", numeroDaConta);
            parameter.Add("NomeDoCliente", item.NomeDoCliente);
            parameter.Add("Ativo", item.Ativo);

            var result = await fac.InsertAsync(
                new CommandSettings<Entitie.ContaCorrente>
                {
                    CommandType = TypeCommand.Query,
                    Entity = new Entitie.ContaCorrente(),
                    Parameters = parameter,
                    Query = ContaCorrenteQueries.Create()
                });

            if (result.Id > 0)
            {
                result.NumeroDaConta = numeroDaConta;
                return TransportResult<Entitie.ContaCorrente>.Create(result);
            }
            return TransportResult<Entitie.ContaCorrente>.Create(null);
        }
    }
}

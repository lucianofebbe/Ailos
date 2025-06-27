using AilosInfra.Interfaces.DataBase.Dapper.UnitOfWorkFactory;
using AilosInfra.Settings.DataBases.Dapper.Settings;
using AilosInfra.Util.TransportsResults;
using Dapper;
using Infrastructure.Data.Interfaces.Commands.Create;
using Infrastructure.Data.Parameters.Commands.Create;
using Infrastructure.Data.Querys;
using Infrastructure.EntitiesDataBases;

namespace Infrastructure.Data.Commands.Create
{
    public class CreateAccountCommand : ICreateAccountCommand
    {
        private IUnitOfWorkFactory<Accounts> _Factory;
        private ConnectionSettings _Settings;

        public CreateAccountCommand(
            IUnitOfWorkFactory<Accounts> factory,
            ConnectionSettings settings)
        {
            _Factory = factory;
            _Settings = settings;
        }

        public async Task<TransportResult<Accounts>> CreateAsync(CreateAccountParameter createAccountParameter)
        {
            var guid = Guid.NewGuid();
            var fac = await _Factory.Create(_Settings);
            var parameters = new DynamicParameters();
            parameters.Add("@CurrentBalance", createAccountParameter.CurrentBalance);
            parameters.Add("@IdBankAccount", createAccountParameter.IdBankAccount);
            parameters.Add("IdFather", createAccountParameter.IdFather);
            parameters.Add("@Guid", guid);
            parameters.Add("@Created", DateTime.UtcNow);
            parameters.Add("@Updated", DateTime.UtcNow);
            parameters.Add("@Deleted", true);
            var result = await fac.InsertAsync(new CommandSettings<Accounts>()
            {
                Entity = new Accounts(),
                CommandType = TypeCommand.Query,
                Query = AccountsQuerys.Create(),
                Parameters = parameters,
                ExecuteScalar = true
            });

            if (result.Id > 0)
                result.Guid = guid;

            return TransportResult<Accounts>.Create(result);
        }
    }
}

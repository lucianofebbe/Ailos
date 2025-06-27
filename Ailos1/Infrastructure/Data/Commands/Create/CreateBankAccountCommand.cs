using AilosInfra.Interfaces.DataBase.Dapper.UnitOfWorkFactory;
using AilosInfra.Settings.DataBases.Dapper.Settings;
using AilosInfra.Util.TransportsResults;
using Dapper;
using Infrastructure.Data.Interfaces.Commands.Create;
using Infrastructure.Data.Parameters.Commands.Create;
using Infrastructure.Data.Querys;
using Infrastructure.EntitiesDataBases;
using System.Text;

namespace Infrastructure.Data.Commands.Create
{
    public class CreateBankAccountCommand : ICreateBankAccountCommand
    {
        private IUnitOfWorkFactory<BankAccounts> _Factory;
        private ConnectionSettings _Settings;

        public CreateBankAccountCommand(
            IUnitOfWorkFactory<BankAccounts> factory,
            ConnectionSettings settings)
        {
            _Factory = factory;
            _Settings = settings;
        }

        public async Task<TransportResult<BankAccounts>> CreateAsync(CreateBankAccountParameter createBankAccountParameter)
        {
            var guid = Guid.NewGuid();
            var AccountNumber = Guid.NewGuid();
            var fac = await _Factory.Create(_Settings);
            var parameters = new DynamicParameters();
            parameters.Add("@Guid", guid);
            parameters.Add("IdFather", 0);
            parameters.Add("JointAccount", createBankAccountParameter.JointAccount);
            parameters.Add("AccountNumber", AccountNumber);
            parameters.Add("@Created", DateTime.UtcNow);
            parameters.Add("@Updated", DateTime.UtcNow);
            parameters.Add("@Deleted", true);
            var result = await fac.InsertAsync(new CommandSettings<BankAccounts>()
            {
                Entity = new BankAccounts(),
                CommandType = TypeCommand.Query,
                Query = BankAccountQuery.Create(),
                Parameters = parameters,
                ExecuteScalar = true
            });

            if (result.Id > 0)
            {
                result.Guid = guid;
                result.AccountNumber = AccountNumber;
            }

            return TransportResult<BankAccounts>.Create(result);
        }
    }
}

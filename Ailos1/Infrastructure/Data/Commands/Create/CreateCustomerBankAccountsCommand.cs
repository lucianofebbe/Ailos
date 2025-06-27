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
    public class CreateCustomerBankAccountsCommand : ICreateCustomerBankAccountsCommand
    {
        private IUnitOfWorkFactory<CustomerBankAccounts> _Factory;
        private ConnectionSettings _Settings;
        public CreateCustomerBankAccountsCommand(
            IUnitOfWorkFactory<CustomerBankAccounts> factory,
            ConnectionSettings settings)
        {
            _Factory = factory;
            _Settings = settings;
        }

        public async Task<TransportResult<CustomerBankAccounts>> CreateAsync(CreateCustomerBankAccountsParameter createCustomersBankAccountsParameter)
        {
            var guid = Guid.NewGuid();
            var fac = await _Factory.Create(_Settings);
            var parameters = new DynamicParameters();
            parameters.Add("@Guid", guid);
            parameters.Add("@IdBankAccount", createCustomersBankAccountsParameter.IdBankAccount);
            parameters.Add("@IdCustomer", createCustomersBankAccountsParameter.IdCustomer);
            parameters.Add("AccountHolder", createCustomersBankAccountsParameter.AccountHolder);
            parameters.Add("@IdFather", 0);
            parameters.Add("@Created", DateTime.UtcNow);
            parameters.Add("@Updated", DateTime.UtcNow);
            parameters.Add("@Deleted", true);

            var query = CustomerBankAccountsQuerys.Create();
            var result = await fac.InsertAsync(new CommandSettings<CustomerBankAccounts>()
            {
                Entity = new CustomerBankAccounts(),
                CommandType = TypeCommand.Query,
                Query = query,
                Parameters = parameters,
                ExecuteScalar = true
            });

            if (result.Id > 0)
                result.Guid = guid;

            return TransportResult<CustomerBankAccounts>.Create(result);
        }
    }
}

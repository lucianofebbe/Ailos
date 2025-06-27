using AilosInfra.Interfaces.DataBase.Dapper.UnitOfWorkFactory;
using AilosInfra.Settings.DataBases.Dapper.Settings;
using AilosInfra.Util.TransportsResults;
using Dapper;
using Infrastructure.Data.Interfaces.Readers.Get;
using Infrastructure.Data.Parameters.Readers.Get;
using Infrastructure.Data.Querys;
using Infrastructure.EntitiesDataBases;
using System.Text;

namespace Infrastructure.Data.Readers.Get
{
    public class GetCustomerBankAccountsReader : IGetCustomerBankAccountsReader
    {
        private IUnitOfWorkFactory<CustomerBankAccounts> _Factory;
        private ConnectionSettings _Settings;

        public GetCustomerBankAccountsReader(
            IUnitOfWorkFactory<CustomerBankAccounts> factory,
            ConnectionSettings Settings)
        {
            _Factory = factory;
            _Settings = Settings;
        }

        public async Task<TransportResult<CustomerBankAccounts>> GetAsync(GetCustomerBankAccountsParameter getCustomersBankAccountsParameter)
        {
            var fac = await _Factory.Create(_Settings);
            var parameters = new DynamicParameters();
            var result = await fac.GetAsync(new CommandSettings<CustomerBankAccounts>()
            {
                CommandType = TypeCommand.Query,
                Query = GetQueryBuilder(getCustomersBankAccountsParameter, parameters),
                Parameters = parameters
            });
            return TransportResult<CustomerBankAccounts>.Create(result);
        }

        public async Task<TransportResult<CustomerBankAccounts>> GetByIdBankAccountAsync(GetCustomerBankAccountsParameter getCustomersBankAccountsParameter)
        {
            var parameter = new GetCustomerBankAccountsParameter() { IdBankAccount = getCustomersBankAccountsParameter.IdBankAccount };
            var fac = await _Factory.Create(_Settings);
            var parameters = new DynamicParameters();
            var result = await fac.GetAsync(new CommandSettings<CustomerBankAccounts>()
            {
                CommandType = TypeCommand.Query,
                Query = GetQueryBuilder(parameter, parameters),
                Parameters = parameters
            });
            return TransportResult<CustomerBankAccounts>.Create(result);
        }

        public async Task<TransportResult<CustomerBankAccounts>> GetByIdCustomerAsync(GetCustomerBankAccountsParameter getCustomersBankAccountsParameter)
        {
            var parameter = new GetCustomerBankAccountsParameter() { IdCustomer = getCustomersBankAccountsParameter.IdCustomer };
            var fac = await _Factory.Create(_Settings);
            var parameters = new DynamicParameters();
            var result = await fac.GetAsync(new CommandSettings<CustomerBankAccounts>()
            {
                CommandType = TypeCommand.Query,
                Query = GetQueryBuilder(parameter, parameters),
                Parameters = parameters
            });
            return TransportResult<CustomerBankAccounts>.Create(result);
        }

        private string GetQueryBuilder(GetCustomerBankAccountsParameter getCustomersBankAccountsParameter, DynamicParameters parameters)
        {
            var queryBuilder = new StringBuilder(CustomerBankAccountsQuerys.Get());
            var conditions = new List<string>();

            if (getCustomersBankAccountsParameter.IdCustomer > 0)
            {
                conditions.Add("IdCustomer = @IdCustomer");
                parameters.Add("@IdCustomer", getCustomersBankAccountsParameter.IdCustomer);
            }
            if (getCustomersBankAccountsParameter.IdBankAccount > 0)
            {
                conditions.Add("IdBankAccount = @IdBankAccount");
                parameters.Add("@IdBankAccount", getCustomersBankAccountsParameter.IdBankAccount);
            }

            if (conditions.Any())
            {
                queryBuilder.Append(" WHERE ");
                queryBuilder.Append(string.Join(" AND ", conditions));
            }

            return queryBuilder.ToString();
        }
    }
}

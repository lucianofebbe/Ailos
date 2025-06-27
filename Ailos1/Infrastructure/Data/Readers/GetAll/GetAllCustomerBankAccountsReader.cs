using AilosInfra.Interfaces.DataBase.Dapper.UnitOfWorkFactory;
using AilosInfra.Settings.DataBases.Dapper.Settings;
using AilosInfra.Util.TransportsResults;
using Dapper;
using Infrastructure.Data.Interfaces.Readers.GetAll;
using Infrastructure.Data.Parameters.Readers.GetAll;
using Infrastructure.Data.Querys;
using Infrastructure.EntitiesDataBases;
using Infrastructure.EntitiesDataBases.Joins;

namespace Infrastructure.Data.Readers.GetAll
{
    public class GetAllCustomerBankAccountsReader : IGetAllCustomerBankAccountsReader
    {
        private IUnitOfWorkFactory<CustomersBankAccountsAndBankAccounts> _Factory;
        private ConnectionSettings _Settings;

        public GetAllCustomerBankAccountsReader(
            IUnitOfWorkFactory<CustomersBankAccountsAndBankAccounts> factory,
            ConnectionSettings Settings)
        {
            _Factory = factory;
            _Settings = Settings;
        }

        public async Task<TransportResult<List<CustomersBankAccountsAndBankAccounts>>> GetAllByIdCustomerAsync(GetAllCustomerParameter getCustomersBankAccountsParameter)
        {
            var fac = await _Factory.Create(_Settings);
            var parameters = new DynamicParameters();
            parameters.Add("@IdCustomer", getCustomersBankAccountsParameter.IdCustomer);
            var result = await fac.GetAllAsync(new CommandSettings<CustomersBankAccountsAndBankAccounts>()
            {
                Entity = new CustomersBankAccountsAndBankAccounts(),
                CommandType = TypeCommand.Query,
                Query = CustomerBankAccountsQuerys.GetCustomersBankAccountsBankAccounts(),
                Parameters = parameters
            });
            return TransportResult<List<CustomersBankAccountsAndBankAccounts>>.Create(result);
        }
    }
}

using AilosInfra.Util.TransportsResults;
using Infrastructure.Data.Parameters.Readers.Get;
using Infrastructure.EntitiesDataBases;

namespace Infrastructure.Data.Interfaces.Readers.Get
{
    public interface IGetCustomerBankAccountsReader
    {
        Task<TransportResult<CustomerBankAccounts>> GetAsync(GetCustomerBankAccountsParameter getCustomersBankAccountsParameter);
        Task<TransportResult<CustomerBankAccounts>> GetByIdBankAccountAsync(GetCustomerBankAccountsParameter getCustomersBankAccountsParameter);
        Task<TransportResult<CustomerBankAccounts>> GetByIdCustomerAsync(GetCustomerBankAccountsParameter getCustomersBankAccountsParameter);
    }
}

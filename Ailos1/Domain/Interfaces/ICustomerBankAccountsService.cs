using AilosInfra.Util.TransportsResults;
using Domain.EntitiesDomains.Joins;
using Domain.EntitiesDomains.Sigles;
using Domain.Filters.CustomerBankAccountsService;

namespace Domain.Interfaces
{
    public interface ICustomerBankAccountsService
    {
        Task<TransportResult<CustomerBankAccountsDomain>> CreateAsync(CreateCustomerBankAccountsFilter createCustomersBankAccountsFilter);
        Task<TransportResult<CustomerBankAccountsDomain>> GetAsync(GetCustomerBankAccountsFilter getCustomersBankAccountsFilter);
        Task<TransportResult<CustomerBankAccountsDomain>> GetByIdBankAccountAsync(GetCustomerBankAccountsFilter getCustomersBankAccountsFilter);
        Task<TransportResult<CustomerBankAccountsDomain>> GetByIdCustomerAsync(GetCustomerBankAccountsFilter getCustomersBankAccountsFilter);
        Task<TransportResult<List<CustomerBankAccountsAndBankAccountsDomain>>> GetAllByIdCustomerAsync(GetAllCustomerBankAccountsFilter getCustomersBankAccountsFilter);
    }
}

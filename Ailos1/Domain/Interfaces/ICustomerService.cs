using AilosInfra.Util.TransportsResults;
using Domain.EntitiesDomains.Sigles;
using Domain.Filters.CustomerService;

namespace Domain.Interfaces
{
    public interface ICustomerService
    {
        Task<TransportResult<CustomerDomain>> CreateAsync(CreateCustomerFilter createAccountFilter);
        Task<TransportResult<CustomerDomain>> GetAsync(GetCustomerFilter createAccountFilter);
        Task<TransportResult<CustomerDomain>> GetByGuidAsync(GetCustomerFilter createAccountFilter);
        Task<TransportResult<CustomerDomain>> GetByCPFAsync(GetCustomerFilter createAccountFilter);
    }
}

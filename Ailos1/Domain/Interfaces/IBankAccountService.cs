using AilosInfra.Util.TransportsResults;
using Domain.EntitiesDomains.Sigles;
using Domain.Filters.BankAccountsService;

namespace Domain.Interfaces
{
    public interface IBankAccountService
    {
        Task<TransportResult<BankAccountsDomain>> CreateAsync(CreateBankAccountFilter createBankAccountFilter);
        Task<TransportResult<BankAccountsDomain>> GetAsync(GetBankAccountFilter createBankAccountFilter);
        Task<TransportResult<BankAccountsDomain>> GetByIdAsync(GetBankAccountFilter createBankAccountFilter);
        Task<TransportResult<BankAccountsDomain>> GetByGuidAsync(GetBankAccountFilter createBankAccountFilter);
        Task<TransportResult<BankAccountsDomain>> GetByAccountNumberAsync(GetBankAccountFilter createBankAccountFilter);
    }
}

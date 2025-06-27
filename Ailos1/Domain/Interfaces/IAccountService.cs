using AilosInfra.Util.TransportsResults;
using Domain.EntitiesDomains.Sigles;
using Domain.Filters.AccountsService;

namespace Domain.Interfaces
{
    public interface IAccountService
    {
        Task<TransportResult<AccountsDomain>> CreateAsync(CreateAccountFilter createAccountFilter);
        Task<TransportResult<AccountsDomain>> DepositAsync(AccountsDomain account, CreateAccountFilter createAccountFilter);
        Task<TransportResult<AccountsDomain>> WithdrawAsync(AccountsDomain account, CreateAccountFilter createAccountFilter);
        Task<TransportResult<AccountsDomain>> GetAsync(GetAccountFilter createAccountFilter);
        Task<TransportResult<AccountsDomain>> GetByIdBankAccountAsync(GetAccountFilter createAccountFilter);
    }
}

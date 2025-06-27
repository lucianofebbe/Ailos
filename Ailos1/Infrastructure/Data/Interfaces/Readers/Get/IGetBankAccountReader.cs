using AilosInfra.Util.TransportsResults;
using Infrastructure.Data.Parameters.Readers.Get;
using Infrastructure.EntitiesDataBases;

namespace Infrastructure.Data.Interfaces.Readers.Get
{
    public interface IGetBankAccountReader
    {
        Task<TransportResult<BankAccounts>> GetAsync(GetBankAccountParameter getBankAccountParameter);
        Task<TransportResult<BankAccounts>> GetByIdAsync(GetBankAccountParameter getBankAccountParameter);
        Task<TransportResult<BankAccounts>> GetByGuidAsync(GetBankAccountParameter getBankAccountParameter);
        Task<TransportResult<BankAccounts>> GetByAccountNumberAsync(GetBankAccountParameter getBankAccountParameter);
    }
}

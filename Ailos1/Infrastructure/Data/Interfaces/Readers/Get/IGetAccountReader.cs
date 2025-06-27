using AilosInfra.Util.TransportsResults;
using Infrastructure.Data.Parameters.Readers.Get;
using Infrastructure.EntitiesDataBases;

namespace Infrastructure.Data.Interfaces.Readers.Get
{
    public interface IGetAccountReader
    {
        Task<TransportResult<Accounts>> GetAsync(GetAccountParameter getAccountParameter);
        Task<TransportResult<Accounts>> GetByIdBankAccountAsync(GetAccountParameter getAccountParameter);
    }
}

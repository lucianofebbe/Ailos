using AilosInfra.Util.TransportsResults;
using Infrastructure.Data.Parameters.Readers.Get;
using Infrastructure.EntitiesDataBases;

namespace Infrastructure.Data.Interfaces.Readers.Get
{
    public interface IGetCustomerReader
    {
        Task<TransportResult<Customers>> GetAsync(GetCustomerParameter Parameters);
        Task<TransportResult<Customers>> GetByGuidAsync(GetCustomerParameter getCustomerParameter);
        Task<TransportResult<Customers>> GetByCPFAsync(GetCustomerParameter getCustomerParameter);
    }
}

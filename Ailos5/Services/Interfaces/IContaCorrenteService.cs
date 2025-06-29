using AilosInfra.Util.TransportsResults;
using Services.Domain;
using Services.Filters.ContaCorrenteService;

namespace Services.Interfaces
{
    public interface IContaCorrenteService
    {
        Task<TransportResult<ContaCorrente>> CreateAsync(CreateFilter item);
    }
}

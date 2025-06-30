using AilosInfra.Util.TransportsResults;
using Services.Domain;
using Services.Filters.ContaCorrenteService;

namespace Services.Interfaces.ContaCorrenteService
{
    public interface IContaCorrenteService
    {
        Task<TransportResult<ContaCorrente>> CreateAsync(CreateFilter item);
        Task<TransportResult<ContaCorrente>> GetContaCorrenteAsync(GetContaCorrenteFilter item);
    }
}

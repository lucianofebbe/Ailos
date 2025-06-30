using AilosInfra.Util.TransportsResults;
using Services.Domain;
using Services.Filters.MovimentoService;

namespace Services.Interfaces.MovimentoService
{
    public interface IMovimentoService
    {
        Task<TransportResult<Movimento>> GetSaldoAtualAsync(GetSaldoAtualFilter item);
        Task<TransportResult<Movimento>> InitMovimentoAsync(InitMovimentoFilter item);
    }
}

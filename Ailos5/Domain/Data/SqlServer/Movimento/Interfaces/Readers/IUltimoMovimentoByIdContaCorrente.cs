using AilosInfra.Util.TransportsResults;
using Domain.Data.SqlServer.Movimento.Parameters.Readers;
using Entitie = Domain.Entities.Sql;

namespace Domain.Data.SqlServer.Movimento.Interfaces.Readers
{
    public interface IUltimoMovimentoByIdContaCorrente
    {
        Task<TransportResult<Entitie.Movimento>> GetByIdUltimoMovimentoContaCorrente(UltimoMovimentoGetByIdContaCorrenteParameter item);
    }
}

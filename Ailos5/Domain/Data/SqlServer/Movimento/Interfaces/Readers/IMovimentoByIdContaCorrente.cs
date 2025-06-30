using AilosInfra.Util.TransportsResults;
using Domain.Data.SqlServer.Movimento.Parameters.Readers;
using Entitie = Domain.Entities.Sql;

namespace Domain.Data.SqlServer.Movimento.Interfaces.Readers
{
    public interface IMovimentoByIdContaCorrente
    {
        Task<TransportResult<List<Entitie.Movimento>>> GetByIdContaCorrente(MovimentoGetByIdContaCorrenteParameter item);
    }
}

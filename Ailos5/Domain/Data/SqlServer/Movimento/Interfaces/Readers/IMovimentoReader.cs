using AilosInfra.Util.TransportsResults;
using Domain.Data.SqlServer.Movimento.Parameters.Readers;
using Entitie = Domain.Entities.Sql;

namespace Domain.Data.SqlServer.Movimento.Interfaces.Readers
{
    public interface IMovimentoReader
    {
        Task<TransportResult<Entitie.Movimento>> GetByIdContaCorrente(MovimentoGetByIdContaCorrenteParameter item);
    }
}

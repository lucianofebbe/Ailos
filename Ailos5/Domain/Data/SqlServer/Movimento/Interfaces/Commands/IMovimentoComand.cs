using AilosInfra.Util.TransportsResults;
using Domain.Data.SqlServer.Movimento.Parameters.Commands;
using Entitie = Domain.Entities.Sql;

namespace Domain.Data.SqlServer.Movimento.Interfaces.Commands
{
    public interface IMovimentoComand
    {
        Task<TransportResult<Entitie.Movimento>> CreateAsync(MovimentoCreateParameter item);
    }
}

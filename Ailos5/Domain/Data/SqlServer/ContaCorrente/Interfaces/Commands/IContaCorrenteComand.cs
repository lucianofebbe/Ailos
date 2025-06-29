using AilosInfra.Util.TransportsResults;
using Domain.Data.SqlServer.ContaCorrente.Parameters.Commands;
using Entitie = Domain.Entities.Sql;

namespace Domain.Data.SqlServer.ContaCorrente.Interfaces.Commands
{
    public interface IContaCorrenteComand
    {
        Task<TransportResult<Entitie.ContaCorrente>> CreateAsync(ContaCorrenteCreateParameter item);
    }
}

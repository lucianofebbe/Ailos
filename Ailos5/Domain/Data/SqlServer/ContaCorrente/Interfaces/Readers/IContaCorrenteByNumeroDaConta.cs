using AilosInfra.Util.TransportsResults;
using Domain.Data.SqlServer.ContaCorrente.Parameters.Readers;
using Entitie = Domain.Entities.Sql;

namespace Domain.Data.SqlServer.ContaCorrente.Interfaces.Readers
{
    public interface IContaCorrenteByNumeroDaConta
    {
        Task<TransportResult<Entitie.ContaCorrente>> GetByNumeroDaConta(ContaCorrenteByNumeroDaContaParameter item);
    }
}

using AilosInfra.Bases.Entities;
using AilosInfra.DataBases.Dapper.UnitOfWork;
using AilosInfra.Interfaces.DataBase.Dapper.UnitOfWork;
using AilosInfra.Interfaces.DataBase.Dapper.UnitOfWorkFactory;
using AilosInfra.Settings.DataBases.Dapper.Settings;

namespace AilosInfra.DataBases.Dapper.UnitOfWorkFactory
{
    public class UnitOfWorkFactory<T> : IUnitOfWorkFactory<T>
        where T : BaseEntitiesSql
    {
        // Cria UnitOfWork usando conexão fornecida e inicia transação se solicitado
        public virtual async Task<IUnitOfWork<T>> Create(ConnectionSettings connectionSettings)
        {
            IUnitOfWork<T> unit = new UnitOfWork<T>(connectionSettings);
            if (connectionSettings.EnableTransaction)
                await unit.BeginTransactionAsync();
            return unit;
        }
    }
}

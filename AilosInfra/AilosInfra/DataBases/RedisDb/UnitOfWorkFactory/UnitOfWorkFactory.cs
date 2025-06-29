using AilosInfra.Bases.Entities;
using AilosInfra.DataBases.RedisDb.UnitOfWork;
using AilosInfra.Interfaces.DataBase.RedisDb.UnitOfWork;
using AilosInfra.Interfaces.DataBase.RedisDb.UnitOfWorkFactory;
using AilosInfra.Settings.DataBases.RedisDb.Settings;

namespace AilosInfra.DataBases.RedisDb.UnitOfWorkFactory
{
    public class UnitOfWorkFactory<T> : IUnitOfWorkFactory<T> where T : BaseEntitiesRedisDb
    {
        // Cria uma instância de IUnitOfWork<T> usando as opções de configuração passadas diretamente
        public virtual async Task<IUnitOfWork<T>> Create(ConnectionSettings connectionSettings)
        {
            // Cria o UnitOfWork com conexão e parâmetros opcionais
            IUnitOfWork<T> redis = new UnitOfWork<T>(connectionSettings);
            return redis;
        }
    }
}

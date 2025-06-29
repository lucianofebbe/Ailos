using AilosInfra.Settings.DataBases.RedisDb.Settings;

namespace AilosInfra.Interfaces.DataBase.RedisDb.UnitOfWork
{
    // Interface para Unidade de Trabalho no RedisDb para entidades BaseEntitiesRedisDb
    public interface IUnitOfWork<T> where T : Bases.Entities.BaseEntitiesRedisDb
    {
        Task<Guid> InsertAsync(CommandSettings<T> commandSettings);
        Task<bool> UpdateAsync(CommandSettings<T> commandSettings);
        Task<bool> DeleteAsync(CommandSettings<T> commandSettings);

        // Obtém um único registro que satisfaça o predicado
        Task<T> GetAsync(CommandSettings<T> commandSettings);

        // Obtém uma lista de registros que satisfaçam o predicado com paginação
        Task<List<T>> GetAllAsync(CommandSettings<T> commandSettings);
    }
}

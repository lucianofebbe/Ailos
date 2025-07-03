using AilosInfra.Settings.DataBases.RedisDb.Settings;

namespace AilosInfra.Interfaces.DataBase.RedisDb.UnitOfWork
{
    public interface IUnitOfWork<T> where T : Bases.Entities.BaseEntitiesRedisDb
    {
        Task<Guid> InsertAsync(CommandSettings<T> commandSettings);
        Task<bool> UpdateAsync(CommandSettings<T> commandSettings);
        Task<bool> DeleteAsync(CommandSettings<T> commandSettings);

        Task<T> GetAsync(CommandSettings<T> commandSettings);
    }
}

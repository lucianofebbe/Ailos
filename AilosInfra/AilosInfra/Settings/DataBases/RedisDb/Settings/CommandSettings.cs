using AilosInfra.Bases.Entities;
using System.Linq.Expressions;

namespace AilosInfra.Settings.DataBases.RedisDb.Settings
{
    public record CommandSettings<T> where T: BaseEntitiesRedisDb
    {
        public T Entity { get; set; }
        public Expression<Func<T, bool>> Predicate { get; set; }
        public CancellationToken CancellationToken { get; set; }
        public TimeSpan? ExpireItem { get; set; }
        public TimeSpan? RenewItem { get; set; }
        public bool DeleteAfterReader { get; set; } = false;
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}

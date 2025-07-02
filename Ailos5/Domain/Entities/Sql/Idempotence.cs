using AilosInfra.Bases.Entities;

namespace Domain.Entities.Sql
{
    public class Idempotence : BaseEntitiesSql
    {
        public string Value { get; set; }
    }
}

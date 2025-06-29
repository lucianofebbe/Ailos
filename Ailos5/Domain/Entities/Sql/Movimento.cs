using AilosInfra.Bases.Entities;

namespace Domain.Entities.Sql
{
    public class Movimento : BaseEntitiesSql
    {
        public int IdContaCorrente { get; set; }
        public DateTime DataMovimento { get; set; }
        public char TipoMovimento { get; set; }
        public decimal Valor { get; set; }
    }
}

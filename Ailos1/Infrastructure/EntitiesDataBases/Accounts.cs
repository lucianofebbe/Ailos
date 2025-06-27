using AilosInfra.Bases.Entities;

namespace Infrastructure.EntitiesDataBases
{
    public class Accounts : BaseEntitiesSql
    {
        public int IdFather { get; set; }
        public int IdBankAccount { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}

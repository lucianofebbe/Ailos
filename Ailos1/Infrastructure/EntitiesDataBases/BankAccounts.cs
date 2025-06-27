using AilosInfra.Bases.Entities;

namespace Infrastructure.EntitiesDataBases
{
    public class BankAccounts : BaseEntitiesSql
    {
        public int IdFather { get; set; }
        public Guid AccountNumber { get; set; }
        public bool JointAccount { get; set; }
    }
}
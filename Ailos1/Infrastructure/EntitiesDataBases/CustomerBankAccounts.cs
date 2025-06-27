using AilosInfra.Bases.Entities;

namespace Infrastructure.EntitiesDataBases
{
    public class CustomerBankAccounts : BaseEntitiesSql
    {
        public int IdCustomer { get; set; }
        public int IdBankAccount { get; set; }
        public bool AccountHolder { get; set; }
    }
}

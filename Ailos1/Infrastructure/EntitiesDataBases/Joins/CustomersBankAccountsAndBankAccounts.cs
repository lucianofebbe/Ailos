using AilosInfra.Bases.Entities;

namespace Infrastructure.EntitiesDataBases.Joins
{
    public class CustomersBankAccountsAndBankAccounts : BaseEntitiesSql
    {
        public Guid GuidBankAccounts { get; set; }
        public Guid AccountNumber { get; set; }
        public bool JointAccount { get; set; }
        public Guid GuidCustomerBankAccounts { get; set; }
        public bool AccountHolder { get; set; }
    }
}

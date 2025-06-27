namespace Domain.EntitiesDomains.Sigles
{
    public class AccountsDomain
    {
        public int Id { get; set; }
        public Guid Guid { get; private set; }
        public decimal CurrentBalance { get; set; }

        public AccountsDomain(Guid guid, decimal currentBalance)
        {
            SetGuid(guid);
            SetCurrentBalance(currentBalance);
        }

        public AccountsDomain(int id, Guid guid, decimal currentBalance)
        {
            SetId(id);
            SetGuid(guid);
            SetCurrentBalance(currentBalance);
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public void SetGuid(Guid guid)
        {
            Guid = guid;
        }

        public void SetCurrentBalance(decimal currentBalance)
        {
            CurrentBalance = currentBalance;
        }
    }
}

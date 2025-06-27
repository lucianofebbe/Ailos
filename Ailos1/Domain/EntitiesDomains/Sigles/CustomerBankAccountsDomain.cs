namespace Domain.EntitiesDomains.Sigles
{
    public class CustomerBankAccountsDomain
    {
        public Guid Guid { get; private set; }
        public bool AccountHolder { get; set; }

        public CustomerBankAccountsDomain(Guid guid, bool accountHolder)
        {
            SetGuid(guid);
            SetAccountHolder(accountHolder);
        }

        public void SetGuid(Guid guid)
        {
            Guid = guid;
        }

        public void SetAccountHolder(bool accountHolder)
        {
            AccountHolder = accountHolder;
        }
    }
}

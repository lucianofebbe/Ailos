namespace Domain.EntitiesDomains.Sigles
{
    public class BankAccountsDomain
    {
        public int Id { get; private set; }
        public Guid Guid { get; private set; }
        public bool JointAccount { get; private set; }
        public Guid AccountNumber { get; private set; }

        public BankAccountsDomain(Guid guid, bool jointAccount, Guid accountNumber)
        {
            SetGuid(guid);
            SetJointAccount(jointAccount);
            SetAccountNumber(accountNumber);
        }

        public BankAccountsDomain(int id, Guid guid, bool jointAccount, Guid accountNumber)
        {
            SetId(id);
            SetGuid(guid);
            SetJointAccount(jointAccount);
            SetAccountNumber(accountNumber);
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public void SetGuid(Guid guid)
        {
            Guid = guid;
        }

        public void SetJointAccount(bool jointAccount)
        {
            JointAccount = jointAccount;
        }

        public void SetAccountNumber(Guid accountNumber)
        {
            AccountNumber = accountNumber;
        }
    }
}

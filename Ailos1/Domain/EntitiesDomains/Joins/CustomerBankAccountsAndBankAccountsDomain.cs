namespace Domain.EntitiesDomains.Joins
{
    public class CustomerBankAccountsAndBankAccountsDomain
    {
        public Guid GuidBankAccounts { get; private set; }
        public Guid AccountNumber { get; private set; }
        public bool JointAccount { get; private set; }
        public Guid GuidCustomerBankAccounts { get; private set; }
        public bool AccountHolder { get; private set; }

        public CustomerBankAccountsAndBankAccountsDomain(
            Guid guidBankAccounts,
            Guid AccountNumber,
            bool JointAccount,
            Guid GuidCustomerBankAccounts,
            bool AccountHolder)
        {
            SetGuidBankAccounts(guidBankAccounts);
            SetAccountNumber(AccountNumber);
            SetJointAccount(JointAccount);
            SetGuidCustomerBankAccounts(GuidCustomerBankAccounts);
            SetAccountHolder(AccountHolder);
        }

        public void SetGuidBankAccounts(Guid guidBankAccounts)
        {
            GuidBankAccounts = guidBankAccounts;
        }

        public void SetAccountNumber(Guid accountNumber)
        {
            AccountNumber = accountNumber;
        }

        public void SetJointAccount(bool jointAccount)
        {
            JointAccount = jointAccount;
        }

        public void SetGuidCustomerBankAccounts(Guid guidCustomerBankAccounts)
        {
            GuidCustomerBankAccounts = guidCustomerBankAccounts;
        }

        public void SetAccountHolder(bool accountHolder)
        {
            AccountHolder = accountHolder;
        }
    }
}

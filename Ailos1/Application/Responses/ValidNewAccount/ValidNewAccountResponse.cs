using AilosInfra.Bases.Dtos;

namespace Application.Responses.ValidNewAccount
{
    public record ValidNewAccountResponse : BaseResponse
    {
        public Guid Guid { get; private set; }
        public string NameCustomer { get; private set; }
        public string CPF { get; private set; }
        public List<CustomersBankAccountsResponse> BanksAccounts { get; private set; }

        public ValidNewAccountResponse()
        {
            SetGuid(Guid.Empty);
            SetNameCustomer(string.Empty);
            SetCpf(string.Empty);
            SetBanksAccounts(new List<CustomersBankAccountsResponse>());
        }

        public ValidNewAccountResponse(Guid guid, string nameCustomer, string cpf)
        {
            SetGuid(guid);
            SetNameCustomer(nameCustomer);
            SetCpf(cpf);
        }

        public ValidNewAccountResponse(Guid guid, string nameCustomer, string cpf, List<CustomersBankAccountsResponse> banksAccounts)
        {
            SetGuid(guid);
            SetNameCustomer(nameCustomer);
            SetCpf(cpf);
            SetBanksAccounts(banksAccounts);
        }

        public void SetGuid(Guid guid)
        {
            Guid = guid;
        }

        public void SetNameCustomer(string nameCustomer)
        {
            NameCustomer = nameCustomer;
        }

        public void SetCpf(string cpf)
        {
            CPF = cpf;
        }

        public void SetBanksAccounts(List<CustomersBankAccountsResponse> banksAccounts)
        {
            BanksAccounts = banksAccounts;
        }
    }

    public record CustomersBankAccountsResponse : BaseResponse
    {
        public Guid GuidBankAccounts { get; private set; }
        public Guid AccountNumber { get; private set; }
        public bool JointAccount { get; private set; }
        public Guid GuidCustomerBankAccounts { get; private set; }
        public bool AccountHolder { get; private set; }

        public CustomersBankAccountsResponse(
            Guid guidBankAccounts,
            Guid accountNumber,
            bool JointAccount,
            Guid GuidCustomerBankAccounts,
            bool AccountHolder)
        {
            SetGuidBankAccounts(guidBankAccounts);
            SetAccountNumber(accountNumber);
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

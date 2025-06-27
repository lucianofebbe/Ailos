namespace Domain.Filters.CustomerBankAccountsService
{
    public record CreateCustomerBankAccountsFilter
    {
        public int IdCustomer { get; set; }
        public int IdBankAccount { get; set; }
        public bool AccountHolder { get; set; }
    }
}

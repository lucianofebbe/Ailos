namespace Domain.Filters.CustomerBankAccountsService
{
    public record GetCustomerBankAccountsFilter
    {
        public int IdCustomer { get; set; }
        public int IdBankAccount { get; set; }
    }
}

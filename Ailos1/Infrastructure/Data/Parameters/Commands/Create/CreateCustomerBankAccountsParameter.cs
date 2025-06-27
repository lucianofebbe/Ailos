namespace Infrastructure.Data.Parameters.Commands.Create
{
    public record CreateCustomerBankAccountsParameter
    {
        public int IdCustomer { get; set; }
        public int IdBankAccount { get; set; }
        public bool AccountHolder { get; set; }
    }
}

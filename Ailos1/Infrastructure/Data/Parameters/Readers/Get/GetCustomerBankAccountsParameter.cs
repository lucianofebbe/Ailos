namespace Infrastructure.Data.Parameters.Readers.Get
{
    public record GetCustomerBankAccountsParameter
    {
        public int IdCustomer { get; set; }
        public int IdBankAccount { get; set; }
    }
}

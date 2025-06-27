namespace Domain.Filters.AccountsService
{
    public record CreateAccountFilter
    {
        public int IdBankAccount { get; set; }
        public decimal CurrentBalance { get; set; }
        public int IdFather { get; set; }
    }
}

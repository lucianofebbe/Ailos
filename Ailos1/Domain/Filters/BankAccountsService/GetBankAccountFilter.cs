namespace Domain.Filters.BankAccountsService
{
    public record GetBankAccountFilter
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public Guid AccountNumber { get; set; }
    }
}

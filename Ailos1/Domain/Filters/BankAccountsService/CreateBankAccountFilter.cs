namespace Domain.Filters.BankAccountsService
{
    public record CreateBankAccountFilter
    {
        public bool JointAccount { get; set; }
    }
}

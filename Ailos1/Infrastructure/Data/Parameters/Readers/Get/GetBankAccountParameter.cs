namespace Infrastructure.Data.Parameters.Readers.Get
{
    public record GetBankAccountParameter
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public Guid AccountNumber { get; set; }
    }
}

namespace Infrastructure.Data.Parameters.Readers.Get
{
    public record GetAccountParameter
    {
        public int IdBankAccount { get; set; }
        public int Id { get; set; }
        public Guid Guid { get; set; }
    }
}

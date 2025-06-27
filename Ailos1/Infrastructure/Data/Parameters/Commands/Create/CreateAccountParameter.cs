namespace Infrastructure.Data.Parameters.Commands.Create
{
    public record CreateAccountParameter
    {
        public int IdBankAccount { get; set; }
        public decimal CurrentBalance { get; set; }
        public int IdFather { get; set; }
    }
}

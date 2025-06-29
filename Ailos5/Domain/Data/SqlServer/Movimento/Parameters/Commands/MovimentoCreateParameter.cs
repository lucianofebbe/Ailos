namespace Domain.Data.SqlServer.Movimento.Parameters.Commands
{
    public record MovimentoCreateParameter
    {
        public int IdFather { get; set; }
        public bool Deleted { get; set; }
        public int IdContaCorrente { get; set; }
        public DateTime DataMovimento { get; set; }
        public char TipoMovimento { get; set; }
        public decimal Valor { get; set; }
    }
}

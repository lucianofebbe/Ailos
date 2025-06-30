namespace Domain.Data.SqlServer.ContaCorrente.Parameters.Readers
{
    public record ContaCorrenteByNumeroDaContaParameter
    {
        public Guid NumeroDaConta { get; set; }
        public bool? IncluirAtivas { get; set; } = false;
        public bool? IncluirDeletadas { get; set; } = false;
    }
}

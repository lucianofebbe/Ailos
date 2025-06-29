namespace Domain.Data.SqlServer.ContaCorrente.Parameters.Commands
{
    public record ContaCorrenteCreateParameter
    {
        public Guid NumeroDaConta { get; set; }
        public string NomeDoCliente { get; set; }
        public bool Ativo { get; set; }
        public bool Deleted { get; set; }
    }
}

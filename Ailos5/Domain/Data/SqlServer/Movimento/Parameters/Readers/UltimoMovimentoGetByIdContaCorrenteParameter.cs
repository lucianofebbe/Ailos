namespace Domain.Data.SqlServer.Movimento.Parameters.Readers
{
    public record class UltimoMovimentoGetByIdContaCorrenteParameter
    {
        public int IdContaCorrente { get; set; }
    }
}

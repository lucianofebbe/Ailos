namespace Domain.Data.SqlServer.Movimento.Parameters.Readers
{
    public record class MovimentoGetByIdContaCorrenteParameter
    {
        public int IdContaCorrente { get; set; }
    }
}

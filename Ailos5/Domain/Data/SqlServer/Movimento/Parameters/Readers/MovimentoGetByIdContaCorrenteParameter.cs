namespace Domain.Data.SqlServer.Movimento.Parameters.Readers
{
    public record class MovimentoGetByIdContaCorrenteParameter
    {
        public Guid IdContaCorrente { get; set; }
    }
}

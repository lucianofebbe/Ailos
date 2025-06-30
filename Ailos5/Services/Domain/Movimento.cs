namespace Services.Domain
{
    public class Movimento
    {
        public int Id { get; private set; }
        public Guid Guid { get; private set; }
        public DateTime DataMovimento { get; private set; }
        public char TipoMovimento { get; private set; }
        public decimal Valor { get; private set; }

        public Movimento(Guid guid, DateTime dataMovimento, char tipoMovimento, decimal valor)
        {
            SetGuid(guid);
            SetDataMovimento(dataMovimento);
            SetTipoMovimento(tipoMovimento);
            SetValor(valor);
        }

        public Movimento(int id, Guid guid, DateTime dataMovimento, char tipoMovimento, decimal valor)
        {
            SetId(id);
            SetGuid(guid);
            SetDataMovimento(dataMovimento);
            SetTipoMovimento(tipoMovimento);
            SetValor(valor);
        }

        public Movimento(DateTime dataMovimento, decimal valor)
        {
            SetDataMovimento(dataMovimento);
            SetValor(valor);
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public void SetGuid(Guid guid)
        {
            Guid = guid;
        }

        public void SetDataMovimento(DateTime dataMovimento)
        {
            DataMovimento = dataMovimento;
        }

        public void SetTipoMovimento(char tipoMovimento)
        {
            TipoMovimento = tipoMovimento;
        }

        public void SetValor(decimal valor)
        {
            Valor = valor;
        }
    }
}

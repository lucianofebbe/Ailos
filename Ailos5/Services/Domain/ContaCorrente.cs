namespace Services.Domain
{
    public class ContaCorrente
    {
        public int Id { get; set; }
        public Guid Guid { get; private set; }
        public Guid Conta { get; private set; }
        public string Cliente { get; private set; }
        public bool Ativo { get; private set; }

        public ContaCorrente(Guid guid, Guid conta, string cliente, bool ativo)
        {
            Guid = guid;
            Conta = conta;
            Cliente = cliente;
            Ativo = ativo;
        }

        public ContaCorrente(int id, Guid guid, Guid conta, string cliente, bool ativo)
        {
            SetId(id);
            Guid = guid;
            Conta = conta;
            Cliente = cliente;
            Ativo = ativo;
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public void SetGuid(Guid guid)
        {
            if (guid == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", nameof(guid));
            Guid = guid;
        }

        public void SetConta(Guid conta)
        {
            if (conta == Guid.Empty)
                throw new ArgumentException("Conta cannot be empty.", nameof(conta));

            Conta = conta;
        }

        public void SetCliente(string cliente)
        {
            if (!string.IsNullOrEmpty(cliente))
                throw new ArgumentException("Conta cannot be empty.", nameof(cliente));

            if(cliente.Length < 3 & cliente.Length > 100)
                throw new ArgumentException("Min 3 caracter and max 100 Caracter.", nameof(cliente));

            Cliente = cliente;
        }

        public void SetAtivo(bool ativo)
        {
            Ativo = ativo;
        }
    }
}

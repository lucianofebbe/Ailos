namespace Domain.EntitiesDomains.Sigles
{
    public class CustomerDomain
    {
        public int Id { get; set; }
        public Guid Guid { get; private set; }
        public string NameCustomer { get; private set; }
        public string CPF { get; private set; }

        public CustomerDomain(string nameCustomer, string cpf)
        {
            SetGuid(Guid.NewGuid());
            SetNameCustomer(nameCustomer);
            SetCpf(cpf);
        }

        public CustomerDomain(Guid guid, string nameCustomer, string cpf)
        {
            SetGuid(guid);
            SetNameCustomer(nameCustomer);
            SetCpf(cpf);
        }

        public CustomerDomain(int id, Guid guid, string nameCustomer, string cpf)
        {
            SetId(id);
            SetGuid(guid);
            SetNameCustomer(nameCustomer);
            SetCpf(cpf);
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public void SetGuid(Guid guid)
        {
            if (guid == Guid.Empty)
                throw new ArgumentException("Insira um Guid.");

            Guid = guid;
        }

        public void SetNameCustomer(string nameCustomer)
        {
            if (!string.IsNullOrEmpty(nameCustomer) && nameCustomer.Length < 3)
                throw new ArgumentException("Insira um Nome de Cliente.");
            NameCustomer = nameCustomer;
        }

        public void SetCpf(string cpf)
        {
            if (!string.IsNullOrEmpty(cpf) && cpf.Length < 3)
                throw new ArgumentException("Insira um cpf valida.");
            CPF = cpf;
        }
    }
}

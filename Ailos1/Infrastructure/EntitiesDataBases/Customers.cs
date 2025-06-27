using AilosInfra.Bases.Entities;

namespace Infrastructure.EntitiesDataBases
{
    public class Customers : BaseEntitiesSql
    {
        public int IdFather { get; set; }
        public string NameCustomer { get; set; }
        public string CPF { get; set; }
    }
}

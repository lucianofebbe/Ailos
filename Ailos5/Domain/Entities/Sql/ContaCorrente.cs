using AilosInfra.Bases.Entities;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Sql
{
    public class ContaCorrente : BaseEntitiesSql
    {
        public Guid NumeroDaConta { get; set; }

        [MaxLength(100)]
        public string NomeDoCliente { get; set; }
        public bool Ativo { get; set; }
    }
}

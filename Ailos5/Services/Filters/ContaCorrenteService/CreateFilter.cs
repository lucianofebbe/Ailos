using AilosInfra.Bases.Dtos;

namespace Services.Filters.ContaCorrenteService
{
    public record CreateFilter : BaseRequest
    {
        public Guid Conta { get; set; } = Guid.Empty;
        public string Cliente { get; set; }
        public bool Ativo { get; set; }
        public bool Deleted { get; set; }
    }
}

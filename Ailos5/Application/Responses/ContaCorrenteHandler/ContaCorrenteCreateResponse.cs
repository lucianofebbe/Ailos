using AilosInfra.Bases.Dtos;

namespace Application.Responses.ContaCorrente
{
    public record ContaCorrenteCreateResponse : BaseResponse
    {
        public Guid Conta { get; set; }
    }
}

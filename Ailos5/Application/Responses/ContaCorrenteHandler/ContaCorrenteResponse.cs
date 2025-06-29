using AilosInfra.Bases.Dtos;

namespace Application.Responses.ContaCorrente
{
    public record ContaCorrenteResponse : BaseResponse
    {
        public Guid Conta { get; set; }
    }
}

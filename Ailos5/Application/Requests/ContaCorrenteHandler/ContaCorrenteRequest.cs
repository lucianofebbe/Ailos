using AilosInfra.Bases.Dtos;
using Application.Responses.ContaCorrente;
using MediatR;

namespace Application.Requests.ContaCorrente
{
    public record ContaCorrenteRequest : BaseRequest, IRequest<ContaCorrenteResponse>
    {
        public string Cliente { get; set; }
        public bool Ativo { get; set; }
        public bool Deleted { get; set; } = false;
    }
}

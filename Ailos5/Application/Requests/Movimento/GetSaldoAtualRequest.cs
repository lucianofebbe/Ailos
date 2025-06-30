using AilosInfra.Bases.Dtos;
using Application.Responses.Movimento;
using MediatR;

namespace Application.Requests.Movimento
{
    public record GetSaldoAtualRequest : BaseRequest, IRequest<GetSaldoAtualResponse>
    {
        public Guid NumeroDaConta { get; set; }
    }
}

using AilosInfra.Bases.Dtos;
using Application.Responses.Movimento;
using MediatR;

namespace Application.Requests.Movimento
{
    public record InitMovimentoRequest : BaseRequest, IRequest<InitMovimentoResponse>
    {
        public Guid NumeroDaConta { get; set; }
        public char TipoMovimento { get; set; }
        public decimal Valor { get; set; }
    }
}

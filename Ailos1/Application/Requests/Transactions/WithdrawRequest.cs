using AilosInfra.Bases.Dtos;
using Application.Responses.Transactions;
using MediatR;

namespace Application.Requests.Transactions
{
    public record WithdrawRequest : BaseRequest, IRequest<WithdrawResponse>
    {
        public Guid AccountNumber { get; set; }
        public decimal Value { get; set; }
    }
}

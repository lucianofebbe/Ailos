using AilosInfra.Bases.Dtos;
using Application.Responses.NewDeposit;
using MediatR;

namespace Application.Requests.NewDeposit
{
    public record NewDepositRequest :  BaseRequest, IRequest<NewDepositResponse>
    {
        public Guid AccountNumber { get; set; }
        public decimal Value { get; set; }
    }
}

using AilosInfra.Bases.Dtos;

namespace Application.Responses.Transactions
{
    public record NewDepositResponse : BaseResponse
    {
        public bool Deposited { get; set; }
    }
}

using AilosInfra.Bases.Dtos;

namespace Application.Responses.NewDeposit
{
    public record NewDepositResponse : BaseResponse
    {
        public bool Deposited { get; set; }
    }
}

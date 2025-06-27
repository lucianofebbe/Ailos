using AilosInfra.Bases.Dtos;

namespace Application.Responses.Transactions
{
    public record WithdrawResponse : BaseResponse
    {
        public decimal CurrentBalance { get; set; }
    }
}

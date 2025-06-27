using AilosInfra.Bases.Dtos;

namespace Application.Responses.ValidNewAccount
{
    public record AccountCreateResponse : BaseResponse
    {
        public Guid AccountNumber { get; set; }
    }
}

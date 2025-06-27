using AilosInfra.Bases.Dtos;
using Application.Responses.ValidNewAccount;
using MediatR;

namespace Application.Requests.ValidNewAccount
{
    public record AccountCreateRequest : BaseRequest, IRequest<AccountCreateResponse>
    {
        public Guid GuidCustomer { get; set; }
        public bool AccountHolder { get; set; }
        public bool JointAccount { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}

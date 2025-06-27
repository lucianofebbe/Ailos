using AilosInfra.Bases.Dtos;
using Application.Responses.ValidNewAccount;
using MediatR;

namespace Application.Requests.ValidNewAccount
{
    public record ValidNewAccountRequest : BaseRequest, IRequest<ValidNewAccountResponse>
    {
        public string Cpf { get; set; }
    }
}

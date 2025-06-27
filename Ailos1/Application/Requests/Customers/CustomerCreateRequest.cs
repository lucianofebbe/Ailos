using AilosInfra.Bases.Dtos;
using Application.Responses.Customers;
using MediatR;

namespace Application.Requests.Customers
{
    public record CustomerCreateRequest : BaseRequest, IRequest<CustomerCreateResponse>
    {
        public Guid Guid { get; set; }
        public string NameCustomer { get; set; }
        public string CPF { get; set; }
    }
}

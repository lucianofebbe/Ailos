using AilosInfra.Bases.Dtos;

namespace Application.Responses.Customers
{
    public record CustomerCreateResponse : BaseResponse
    {
        public Guid Guid { get; set; }
        public string NameCustomer { get; set; }
        public string CPF { get; set; }
    }
}
